using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Revue_Par_Les_Pairs_V2.Model;
using Revue_Par_Les_Pairs_V2.Model.Depot;
using Revue_Par_Les_Pairs_V2.Model.Entite;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Revue_Par_Les_Pairs_V2.Controllers
{
    [Route("api/professeur")]
    [ApiController]
    public class ProfesseurController : ControllerBase
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IDepotTravail m_depotTravail;
        private readonly IDepotSolution m_depotSolution;
        private readonly IDepotInscriptions m_depotInscription;
        private readonly IDepotCorrections m_depotCorrections;

        public ProfesseurController(IWebHostEnvironment environment, IDepotTravail p_depotTravail, IDepotSolution p_depotSolution, IDepotInscriptions p_depotInscription, IDepotCorrections p_depotCorrections)
        {
            this.hostingEnvironment = environment;
            this.m_depotTravail = p_depotTravail;
            this.m_depotSolution = p_depotSolution;
            this.m_depotInscription = p_depotInscription;
            this.m_depotCorrections = p_depotCorrections;
        }

        [HttpGet]
        [Route("/api/professeur/commentaires/{travailID}")]
        public Object EnvoyerCommentaires(int travailID)
        {
            return LireCommentairesJson(travailID);
        }

        [HttpGet]
        [Route("/api/professeur/envoyerTravaux")]
        public IEnumerable<Object> EnvoyerTravaux()
        {
            //Passe le cours 1 en parametre car on a seulement un cours pour l'instant
            return this.m_depotTravail.ChercherTravauxParCours(1);
        }

        [HttpPost]
        [Route("/api/professeur/ajouterSolution")]
        public ActionResult post([FromForm] Fichier p_fichier)
        {
            if (p_fichier == null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
            else
            {
                //obtenir le nom du fichier avec son extension
                string nomFichier = Path.GetFileName(p_fichier.FormFile.FileName);

                //obtenir le chemin du dossier temp dans wwwroot
                string pathTemp = Path.Combine(hostingEnvironment.WebRootPath, "temp");

                // Si le fichier existe déjà, on efface celui qui était présent
                if (System.IO.File.Exists(nomFichier))
                {
                    System.IO.File.Delete(nomFichier);
                }

                using (FileStream localfile = System.IO.File.OpenWrite(nomFichier))
                using (Stream stream = p_fichier.FormFile.OpenReadStream())
                {
                    p_fichier.FormFile.CopyTo(localfile);
                    string extension = Path.GetExtension(p_fichier.FormFile.FileName).ToLowerInvariant();
                    FileInfo fichierRecu = new FileInfo(p_fichier.FormFile.FileName);
                    localfile.Close();
                    stream.Close();

                    // Vérifie si l'extension est vide ou si n'est pas un .zip)
                    if (string.IsNullOrEmpty(extension) ||
                        (extension != ".zip") ||
                        (fichierRecu.FullName.Contains(".jsp")) ||
                        (fichierRecu.FullName.Contains(".exe")) ||
                        (fichierRecu.FullName.Contains(".msi")) ||
                        (fichierRecu.FullName.Contains(".bat")) ||
                        (fichierRecu.FullName.Contains(".php")) ||
                        (fichierRecu.FullName.Contains(".pht")) ||
                        (fichierRecu.FullName.Contains(".phtml")) ||
                        (fichierRecu.FullName.Contains(".asa")) ||
                        (fichierRecu.FullName.Contains(".cer")) ||
                        (fichierRecu.FullName.Contains(".asax")) ||
                        (fichierRecu.FullName.Contains(".swf")) ||
                        (fichierRecu.FullName.Contains(".com")) ||
                        (fichierRecu.FullName.Contains(".xap")))
                    {
                        try
                        {
                            fichierRecu.Delete();
                        }
                        catch (IOException)
                        {
                            fichierRecu.Delete();
                        }
                        catch (UnauthorizedAccessException)
                        {
                            fichierRecu.Delete();
                        }
                    }

                    if (fichierRecu.Exists)
                    {
                        FileInfo fichierDansTemp = new FileInfo(Path.Combine(pathTemp, fichierRecu.Name));

                        if (fichierDansTemp.Exists)
                        {
                            try
                            {
                                fichierDansTemp.Delete();
                            }
                            catch (IOException)
                            {
                                fichierDansTemp.Delete();
                            }
                            catch (UnauthorizedAccessException)
                            {
                                fichierDansTemp.Delete();
                            }
                        }

                        if (!Directory.Exists(pathTemp))
                        {
                            Directory.CreateDirectory(pathTemp);
                        }

                        // Déplacer le fichier dans le répetoire temp
                        fichierRecu.MoveTo(Path.Combine(pathTemp, fichierRecu.Name));

                        //decompresser le fichier dans le repertoire temp
                        Decompresser(Path.Combine(pathTemp, nomFichier), pathTemp);

                        //effacer le fichier zip dans le repertoire temp
                        fichierRecu.Delete();

                        //nettoyer le fichier dans le dossier temp
                        NettoyerLeFichierZip(nomFichier);

                        AjouterSolutionBD(Path.Combine(pathTemp, nomFichier), p_fichier.TravailID);

                        DistributionSolution(p_fichier.TravailID);
                    }

                    return StatusCode(StatusCodes.Status201Created);
                }
            }
        }

        [HttpPost]
        [Route("/api/professeur/ajouterTravail")]
        public IActionResult post([FromBody] Travail p_travail)
        {
            if (p_travail is null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
            try
            {
                // Ajouter le travail a la BD
                this.m_depotTravail.AjouterTravail(p_travail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost]
        [Route("/api/professeur/ajouterFichierCommentaire/{travailID}")]
        public IActionResult post(int travailID)
        {
            try
            {
                // Creation d'un nouveau fichier de commentaire chaque fois qu'un travail est cree (exemple: 2233.json)
                string nomFichier = Path.Combine(hostingEnvironment.WebRootPath, "commentaires", travailID + ".json");

                System.IO.File.Create(nomFichier).Close();

                var listeCommentairesVide = JsonConvert.SerializeObject(new List<Commentaire>());

                // Ecrire dans le fichier .json
                System.IO.File.WriteAllText(nomFichier, listeCommentairesVide);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete("{id}")]
        [Route("/api/professeur/supprimerTravail/{id}")]
        public void SupprimerTravail(int id)
        {
            try
            {
                this.m_depotTravail.RetirerTravail(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        //Methodes

        // Decompresser le fichier zip
        private void Decompresser(string p_nomFichier, string p_fichierDestination)
        {
            if (p_nomFichier != null)
            {
                ZipFile.ExtractToDirectory(p_nomFichier, p_fichierDestination);
            }
        }

        // Nettoyer tous les fichiers pas necesaires
        private void NettoyerLeFichierZip(string p_NomFichierZip)
        {
            //Recuperer le nom du fichier sans l'extension 
            string nomFichierSansExtention = Path.GetFileNameWithoutExtension(p_NomFichierZip);

            //Chemin du fichier a nettoyer 
            string cheminFichier = Path.Combine(hostingEnvironment.WebRootPath, "temp", nomFichierSansExtention);

            //Conversion de type string -> directoryInfo
            DirectoryInfo destination = new(cheminFichier);

            Regex regexValidationFolder = new("[A-Za-z]*_(?<numeroMatricule>(\\d{7,}))_[A-Za-z]*");

            foreach (DirectoryInfo dir in destination.GetDirectories())
            {
                Match resultat = regexValidationFolder.Match(dir.Name);

                //Prend l'index du R dans le nom
                int index = dir.FullName.LastIndexOf("R");

                if (index >= 0)
                {
                    string extracted = dir.FullName.Substring(0, index - 1);

                    dir.MoveTo(Path.Combine(dir.Parent.FullName, extracted));
                }

                if (resultat.Success)
                {
                    string[] RepetoireAEffacer = new string[] { ".vs", "bin", "obj", "build", ".settings", ".vscode", "wwwroot" };

                    foreach (string NomDirectoryAEffacer in RepetoireAEffacer)
                    {
                        DirectoryInfo[] repertoire = destination.GetDirectories(NomDirectoryAEffacer, SearchOption.AllDirectories);

                        foreach (DirectoryInfo di in repertoire)
                        {
                            try
                            {
                                di.Delete(true);
                            }
                            catch (IOException)
                            {
                                di.Delete(true);
                            }
                            catch (UnauthorizedAccessException)
                            {
                                di.Delete(true);
                            }
                        }
                    }
                }

                string[] TypeDeFichierAEffacer = new string[] { "*.suo", "*.user", "*.userosscache", "*.sln.docstates", ".vs", "bin", "obj", "build", "*.class", ".settings", ".classpath", ".project", "*.mdj", "*.svg", ".sln", ".DS_Store" };

                foreach (string type in TypeDeFichierAEffacer)
                {
                    {
                        FileInfo[] fichier = destination.GetFiles(type, SearchOption.AllDirectories);
                        foreach (FileInfo fi in fichier)
                        {
                            try
                            {
                                if (fi.Extension == type)
                                    fi.Delete();
                            }
                            catch (IOException)
                            {
                                fi.Delete();
                            }
                            catch (UnauthorizedAccessException)
                            {
                                fi.Delete();
                            }
                        }
                    }
                }
            }
        }

        // Lecture de commentaires dans le fichier json
        private Object LireCommentairesJson(int p_travailID)
        {
            // Chemin du fichier json pour les commentaires des etudiants
            string pathCommentaires = Path.Combine(hostingEnvironment.WebRootPath, "commentaires", p_travailID.ToString() + ".json");

            using (StreamReader stream = new StreamReader(pathCommentaires))
            {
                // Lire le json et le mettre dans une string 
                string json = stream.ReadToEnd();
                stream.Close();

                // Deserialization de la liste de commentaires dans le fichier commentaire.json
                return JsonConvert.DeserializeObject<List<Commentaire>>(json);
            }
        }

        // Ajouter une solution dans la table solution de la BD en donnnant le path et le travailID 
        private void AjouterSolutionBD(string p_path, int p_travailID)
        {
            string nomFichierSansExtention = Path.GetFileNameWithoutExtension(p_path);
            string cheminFichier = Path.Combine(hostingEnvironment.WebRootPath, "temp", nomFichierSansExtention);
            DirectoryInfo destination = new(cheminFichier);

            foreach (DirectoryInfo dir in destination.GetDirectories())
            {
                Solution solution = this.m_depotSolution.CreerSolution(dir.FullName, p_travailID);
                this.m_depotSolution.AjouterSolution(solution);
            }
        }

        // Distribuer au etudiants une solutions selon le id du travail passé en parametre
        private void DistributionSolution(int p_fichierID)
        {
            Travail travail = this.m_depotTravail.ChercherTravailParID(p_fichierID);
            List<Solution> listeSolution = this.m_depotSolution.ChercherSolutionParTravailID(p_fichierID);
            List<Etudiant> listeEtudiant = this.m_depotInscription.ListerEtudiantsParCoursID(travail.CoursID);
            int nombreSolutionDistribue = 0;

            Queue<Solution> fileSolutions = GenererQueueAleatoire(listeSolution);

            while (nombreSolutionDistribue < travail.NombresDeRevues)
            {
                foreach (Etudiant etudiant in listeEtudiant)
                {
                    Correction correction = this.m_depotCorrections.CreerCorrection(etudiant.EtudiantID, fileSolutions.Peek().SolutionID);
                    this.m_depotCorrections.AjouterCorrection(correction);

                    fileSolutions.Peek().NombreDeDistrubtion++;

                    if (fileSolutions.Peek().NombreDeDistrubtion == travail.NombresDeRevues)
                    {
                        Solution solution = fileSolutions.Dequeue();
                        fileSolutions.Enqueue(solution);
                    }
                }
                nombreSolutionDistribue++;
            }
        }

        // Generation de file de solutions aleatoire afin de distribuer a chaque etudiant une solution differente
        private Queue<Solution> GenererQueueAleatoire(List<Solution> p_listeSolution)
        {
            Solution[] arraySolution = new Solution[p_listeSolution.Count];
            Queue<Solution> fileSolutions = new Queue<Solution>();

            for (int indiceSolution = 0; indiceSolution < p_listeSolution.Count; indiceSolution++)
            {
                arraySolution[indiceSolution] = p_listeSolution[indiceSolution];
            }

            Random rnd = new Random();
            Solution[] arraySolutionRandom = arraySolution.OrderBy(x => rnd.Next()).ToArray();

            for (int indice = 0; indice < arraySolutionRandom.Length; indice++)
            {
                fileSolutions.Enqueue(arraySolutionRandom[indice]);
            }

            return fileSolutions;
        }
    }
}
