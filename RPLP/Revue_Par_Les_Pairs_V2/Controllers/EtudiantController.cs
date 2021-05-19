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
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Revue_Par_Les_Pairs_V2.Controllers
{
    [Route("/api/etudiant")]
    [ApiController]
    public class EtudiantController : ControllerBase
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IDepotInscriptions m_depotInscription;
        private readonly IDepotTravail m_depotTravail;
        private readonly IDepotCorrections m_depotCorrections;
        private readonly IDepotSolution m_depotSolution;

        public EtudiantController(IWebHostEnvironment environment, IDepotInscriptions p_depotInscription, IDepotTravail p_depotTravail, IDepotCorrections p_depotCorrections, IDepotSolution p_depotsolution)
        {
            this.hostingEnvironment = environment;
            this.m_depotInscription = p_depotInscription;
            this.m_depotTravail = p_depotTravail;
            this.m_depotCorrections = p_depotCorrections;
            this.m_depotSolution = p_depotsolution;
        }

        [HttpGet]
        [Route("/api/etudiant/revue/{EtudiantID}/{solutionID}")]
        public Object RenvoiPathSolution(string EtudiantID, int solutionID)
        {
            try
            {
                string pathSolution = RenvoiPathSolutionDunEtudiant(EtudiantID, solutionID);

                return RenvoiRepertoire(pathSolution);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpGet]
        [Route("/api/etudiant/solution/{EtudiantID}")]
        public IEnumerable<Object> RetournerSolutionsEtudiant(string etudiantID)
        {
            if (string.IsNullOrEmpty(etudiantID))
            {
                throw new ArgumentNullException(nameof(etudiantID));
            }

            return RenvoiDesSolutionsDunEtudiant(etudiantID);
        }

        [HttpGet]
        [Route("/api/etudiant/solution/{EtudiantID}/{TravailID}")]
        public IEnumerable<Object> RetournerSolutionsEtudiantDunTravail(string etudiantID, int travailID)
        {
            if (string.IsNullOrEmpty(etudiantID))
            {
                throw new ArgumentNullException(nameof(etudiantID));
            }

            return RenvoiDesSolutionsDunEtudiantDunTravail(etudiantID, travailID);
        }

        [HttpGet]
        [Route("/api/etudiant/travail/{EtudiantID}")]
        public IEnumerable<Object> RetournerTravauxEtudiant(string etudiantID)
        {
            if (string.IsNullOrEmpty(etudiantID))
            {
                throw new ArgumentNullException(nameof(etudiantID));
            }

            return RenvoiDesTravauxDunEtudiant(etudiantID);
        }

        [HttpGet]
        [Route("/api/etudiant/nbcorrections/{TravailID}/{EtudiantID}")]
        public int NombreCorrectionsFinaliser(int travailID, string etudiantID)
        {
            return this.m_depotCorrections.NombreCopieCorrigeeParTravail(travailID, etudiantID);
        }

        [HttpPut]
        [Route("/api/etudiant/travail/finaliser/{SolutionID}/{EtudiantID}")]
        public IActionResult FinaliserSolution(int solutionID, string etudiantID)
        {
            try
            {
                this.m_depotCorrections.FinaliserCorrection(solutionID, etudiantID);
                return StatusCode(200);
            }
            catch
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

        }

        //Methodes

        // Renvoi le repertoire d'un path en format json 
        private Object RenvoiRepertoire(string p_path)
        {
            System.IO.DirectoryInfo RootDir = new System.IO.DirectoryInfo(p_path);

            //Output le repertoire dans un node
            TreeNode RootNode = RepertoireDeSortie(RootDir, null);

            var jsonNode = JsonConvert.SerializeObject(RootNode, Formatting.Indented);

            return jsonNode;
        }

        //  Cicle sur les nodes pour verifier les repertoires ce qui est existant 
        TreeNode RepertoireDeSortie(System.IO.DirectoryInfo p_repertoire, TreeNode nodeParent)
        {
            //Validation du parametre
            if (p_repertoire == null) return null;

            //Creation de node pour le repertoire
            TreeNode repertoireNode = new TreeNode(p_repertoire.Name, p_repertoire.FullName, p_repertoire.Extension);

            //Get tous les SubDirectories du reportoire
            System.IO.DirectoryInfo[] sousRépertoires = p_repertoire.GetDirectories();

            //Recursivite
            for (int repertoireCount = 0; repertoireCount < sousRépertoires.Length; repertoireCount++)
            {
                RepertoireDeSortie(sousRépertoires[repertoireCount], repertoireNode);
            }

            //Output les repertoires du fichier
            System.IO.FileInfo[] fichiers = p_repertoire.GetFiles();

            for (int FileCount = 0; FileCount < fichiers.Length; FileCount++)
            {
                repertoireNode.EnfantsNodes.Add(new TreeNode(fichiers[FileCount].Name, fichiers[FileCount].FullName, fichiers[FileCount].Extension));
            }

            //Si le parent est null, return le node courrant
            //Sinon ajouter le node au parent et retourner le parent
            if (nodeParent == null)
            {
                return repertoireNode;
            }
            else
            {
                nodeParent.EnfantsNodes.Add(repertoireNode);
                return nodeParent;
            }
        }

        // Retourne les solutions qu'un etudiant doit corriger 
        private List<Solution> RenvoiDesSolutionsDunEtudiant(string p_etudiantID)
        {
            List<Solution> solutions = new List<Solution>();
            List<Correction> corrections = this.m_depotCorrections.ChercherCorrectionsParEtudiantID(p_etudiantID);

            foreach (Correction correction in corrections)
            {
                if (correction.Finaliser == false)
                {
                    solutions.Add(this.m_depotSolution.ChercherSolutionParSolutionID(correction.SolutionID));
                }
            }
            return solutions;
        }

        // Retourne les solutions d'un etudiant et d'un travail qu'il doit corriger 
        private List<Solution> RenvoiDesSolutionsDunEtudiantDunTravail(string p_etudiantID, int p_travailID)
        {
            List<Correction> corrections = new List<Correction>();
            List<Solution> solutions = new List<Solution>();

            List<Solution> solutionsTravail = this.m_depotSolution.ChercherSolutionParTravailID(p_travailID);

            if (solutionsTravail != null)
            {
                foreach (Solution solution in solutionsTravail)
                {
                    List<Correction> correctionTemp = this.m_depotCorrections.ChercherCorrectionsParSolutionID(solution.SolutionID);
                    correctionTemp.ForEach(c => corrections.Add(c));
                }

                List<Correction> correctionsEtudiant = corrections.Where(c => c.EtudiantID == p_etudiantID).ToList();

                if (correctionsEtudiant != null)
                {
                    foreach (Correction correction in correctionsEtudiant)
                    {
                        if (correction.Finaliser == false)
                        {
                            solutions.Add(this.m_depotSolution.ChercherSolutionParSolutionID(correction.SolutionID));
                        }
                    }
                }
            }

            return solutions;
        }

        // Retourne les travaux d'un etudiant specifique 
        private List<Travail> RenvoiDesTravauxDunEtudiant(string p_etudiantID)
        {
            List<Travail> travaux = new List<Travail>();

            List<Etudiant> etudiants = this.m_depotInscription.ListerEtudiantsParCoursID(1);

            foreach (Etudiant etudiant in etudiants)
            {
                if (etudiant.EtudiantID == p_etudiantID)
                {
                    travaux = this.m_depotTravail.ChercherTravauxParCours(1);
                }
            }

            return travaux;
        }

        // Retourne le liens (en string) vers la solutions dont il doit corriger 
        private string RenvoiPathSolutionDunEtudiant(string p_etudiantID, int p_solutionID)
        {
            string pathSolution = "";

            List<Solution> solutions = RenvoiDesSolutionsDunEtudiant(p_etudiantID);

            foreach (Solution solution in solutions)
            {
                if (solution.SolutionID == p_solutionID)
                {
                    pathSolution = solution.Liens;
                }
            }

            return pathSolution;
        }
    }
}
