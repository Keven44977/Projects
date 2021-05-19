using Revue_Par_Les_Pairs_V2.Model;
using Revue_Par_Les_Pairs_V2.Model.Depot;
using Revue_Par_Les_Pairs_V2.Model.Entite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Revue_Par_Les_Pairs_V2.DAL_SQLServeur.DepotSQLServeur
{
    public class DepotTravailSQLServeur : IDepotTravail
    {
        AppDbContext m_appDbContext;

        public DepotTravailSQLServeur(AppDbContext appDbContext)
        {
            m_appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }

        public void AjouterTravail(Travail p_travail)
        {
            if (p_travail is null)
            {
                throw new ArgumentNullException(nameof(p_travail));
            }
            if (this.m_appDbContext.Travail.Any(t => t.Travail_id == p_travail.TravailID))
            {
                throw new ArgumentNullException(nameof(p_travail));
            }

            this.m_appDbContext.Travail.Add(new TravailDTO(p_travail));
            this.m_appDbContext.SaveChanges();
        }

        public List<Travail> ChercherTravauxParCours(int p_coursID)
        {
            return this.m_appDbContext.Travail.Where(t => t.Cours_id == p_coursID).Select(t => t.VersEntite()).ToList();
        }

        public Travail ChercherTravailParID(int p_travailID)
        {
            return this.m_appDbContext.Travail.Where(t => t.Travail_id == p_travailID).Select(t => t.VersEntite()).FirstOrDefault();
        }

        public List<Travail> ListerTravaux()
        {
            return this.m_appDbContext.Travail.Select(t => t.VersEntite()).ToList();
        }

        public void RetirerTravail(int p_index)
        {
            if (p_index < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(p_index));
            }

            Travail travailASupprimer = ChercherTravailParID(p_index);

            if (travailASupprimer is null)
            {
                throw new ArgumentNullException(nameof(p_index));
            }
            else
            {
                DepotSolutionSQLServeur depotSolution = new DepotSolutionSQLServeur(this.m_appDbContext);
                DepotCorrectionsSQLServeur depotCorrection = new DepotCorrectionsSQLServeur(this.m_appDbContext);

                List<Solution> listeSolutions = depotSolution.ChercherSolutionParTravailID(p_index);
                List<Correction> listeCorrections = new List<Correction>();

                if (listeSolutions != null)
                {
                    foreach (Solution solution in listeSolutions)
                    {
                        listeCorrections.AddRange(depotCorrection.ChercherCorrectionsParSolutionID(solution.SolutionID));
                    }

                    if (listeCorrections != null)
                    {
                        foreach (Correction correction in listeCorrections)
                        {
                            CorrectionDTO correctionDTO = new CorrectionDTO(correction);
                            this.m_appDbContext.Correction.Remove(correctionDTO);
                        }
                    }

                    foreach (Solution solution in listeSolutions)
                    {
                        this.m_appDbContext.Solution.Remove(new SolutionDTO(solution));
                    }
                }


                this.m_appDbContext.Travail.Remove(new TravailDTO(travailASupprimer));
                this.m_appDbContext.SaveChanges();
            }
        }

    }
}
