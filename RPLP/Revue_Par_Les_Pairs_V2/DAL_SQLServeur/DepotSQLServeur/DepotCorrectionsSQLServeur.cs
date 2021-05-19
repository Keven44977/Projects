using Revue_Par_Les_Pairs_V2.Model;
using Revue_Par_Les_Pairs_V2.Model.Depot;
using Revue_Par_Les_Pairs_V2.Model.Entite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.DAL_SQLServeur.DepotSQLServeur
{
    public class DepotCorrectionsSQLServeur : IDepotCorrections
    {
        AppDbContext m_appDbContext;

        public DepotCorrectionsSQLServeur(AppDbContext appDbContext)
        {
            m_appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }

        public void AjouterCorrection(Correction p_correction)
        {
            if (p_correction is null)
            {
                throw new ArgumentException(nameof(p_correction));
            }

            this.m_appDbContext.Correction.Add(new CorrectionDTO(p_correction));
            this.m_appDbContext.SaveChanges();
        }

        public Correction ChercherCorrectionParSolutionID(int p_solutionID)
        {
            return this.m_appDbContext.Correction.Where(c => c.Solution_id == p_solutionID).Select(c => c.VersEntite()).FirstOrDefault();
        }

        public List<Correction> ChercherCorrectionsParEtudiantID(string p_etudiantID)
        {
            return this.m_appDbContext.Correction.Where(c => c.Etudiant_id == p_etudiantID).Select(c => c.VersEntite()).ToList();
        }

        public List<Correction> ChercherCorrectionsParSolutionID(int p_id)
        {
             return this.m_appDbContext.Correction.Where(c => c.Solution_id == p_id).Select(c => c.VersEntite()).ToList();
        }

        public Correction CreerCorrection(string p_etudiantID, int p_solutionID)
        {
             return new Correction(p_solutionID,p_etudiantID);
        }

        public void FinaliserCorrection(int p_solutionID, string p_etudiantID)
        {
            CorrectionDTO correction = this.m_appDbContext.Correction.Where(c => c.Solution_id == p_solutionID && c.Etudiant_id==p_etudiantID).FirstOrDefault();

            if (correction != null)
            {
                if(correction.Finaliser)
                {
                    correction.Finaliser = false;
                }else{
                    correction.Finaliser = true;
                }

                this.m_appDbContext.Correction.Update(correction);
                this.m_appDbContext.SaveChanges();
            }
            
        }

        public int NombreCopieCorrigeeParTravail(int p_travailID, string p_etudiantID){
            int nombreCorrections = 0;

            List<Correction> listeCorrections = this.m_appDbContext.Correction.Where(c => c.Etudiant_id == p_etudiantID).Select(c => c.VersEntite()).ToList();
            List<Solution> listeSolutionsParTravail = this.m_appDbContext.Solution.Where(s => s.Travail_id == p_travailID).Select(s => s.VersEntite()).ToList();
            List<Correction> listeCorrectionsDuTravail = new List<Correction>();


            foreach (Solution solution in listeSolutionsParTravail)
            {
                foreach (Correction correction in listeCorrections)
                {
                    if(correction.SolutionID == solution.SolutionID)
                    {
                        listeCorrectionsDuTravail.Add(correction);
                    }
                }
            }

            nombreCorrections = listeCorrectionsDuTravail.Where(c=>c.Finaliser==true).Count();

            return nombreCorrections;
        }

        public List<Correction> ListerCorrections()
        {
            return this.m_appDbContext.Correction.Select(c => c.VersEntite()).ToList();
        }
    }
}
