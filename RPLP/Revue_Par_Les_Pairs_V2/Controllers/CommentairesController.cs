using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Revue_Par_Les_Pairs_V2.DAL_SQLServeur;
using Revue_Par_Les_Pairs_V2.Model;
using Revue_Par_Les_Pairs_V2.Model.Depot;
using System.Collections.Generic;
using System.IO;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Revue_Par_Les_Pairs_V2.Controllers
{
    [Route("api/commentaires")]
    [ApiController]
    public class CommentairesController : ControllerBase
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IDepotCommentaires m_depotCommentaires;
        private readonly IDepotSolution m_depotSolution;
        private readonly IDepotTravail m_depotTravail;

        public CommentairesController(IWebHostEnvironment environment, IDepotCommentaires p_depotCommentaires, IDepotSolution p_depotSolution, IDepotTravail p_depotTravail)
        {
            this.hostingEnvironment = environment;
            this.m_depotCommentaires = p_depotCommentaires;
            this.m_depotSolution = p_depotSolution;
            this.m_depotTravail = p_depotTravail;
        }

        [HttpPost]
        [Route("/api/commentaires/ajouterCommentaire")]
        public IActionResult Post([FromBody] Commentaire p_commentaires)
        {
            if (p_commentaires != null)
            {
                // Enregistrer le commentaire dans la base de donnees
                this.m_depotCommentaires.AjouterCommentaire(p_commentaires);

                // Recuperation du nom du travail pour enregistrer le commentaire dans le bon fichier (selon le nom)
                int travailID = RecuperationDuNomDeTravail(p_commentaires.SolutionID);
                string nomFichier = travailID + ".json";

                // Enregistrer le commentaire dans un fichier json
                EnregistrerCommentaireJson(p_commentaires, nomFichier);

                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
        }

        //Methodes 
        private void EnregistrerCommentaireJson(Commentaire p_commentaire, string p_nomFichier)
        {
            // Chemin du fichier json pour les commentaires des etudiants
            string pathCommentaires = Path.Combine(hostingEnvironment.WebRootPath, "commentaires", p_nomFichier);

            using (StreamReader stream = new StreamReader(pathCommentaires))
            {
                // Lire le json et le mettre dans une string 
                string json = stream.ReadToEnd();
                stream.Close();

                // Deserialization de la liste de commentaires dans le fichier .json
                List<Commentaire> listeCommentaires = LireCommentairesJson(p_nomFichier);

                // Ajout d'un commentaire dans la liste
                listeCommentaires.Add(new Commentaire(p_commentaire.CommentaireID, p_commentaire.Numero_ligne, p_commentaire.Texte, p_commentaire.EtudiantID, p_commentaire.SolutionID, p_commentaire.Severite));

                // Serializer la liste avec le nouveau commentaire
                var listeCommentairesJson = JsonConvert.SerializeObject(listeCommentaires, Formatting.Indented);

                // Ecrire dans le fichier .json
                System.IO.File.WriteAllText(pathCommentaires, listeCommentairesJson);
            }
        }

        private List<Commentaire> LireCommentairesJson(string p_nomFichier)
        {
            // Chemin du fichier json pour les commentaires des etudiants
            string pathCommentaires = Path.Combine(hostingEnvironment.WebRootPath, "commentaires", p_nomFichier);

            using (StreamReader stream = new StreamReader(pathCommentaires))
            {
                // Lire le json et le mettre dans une string 
                string json = stream.ReadToEnd();
                stream.Close();

                // Deserialization de la liste de commentaires dans le fichier .json
                return JsonConvert.DeserializeObject<List<Commentaire>>(json);
            }
        }

        private int RecuperationDuNomDeTravail(int p_solutionID)
        {
            Solution solution = this.m_depotSolution.ChercherSolutionParSolutionID(p_solutionID);
            Travail travail = this.m_depotTravail.ChercherTravailParID(solution.TravailID);

            return travail.TravailID;
        }
    }
}
