using Revue_Par_Les_Pairs_V2.Model;
using Revue_Par_Les_Pairs_V2.Model.Depot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.DAL_SQLServeur.DepotSQLServeur
{
    public class DepotSolutionSQLServeur : IDepotSolution
    {
        AppDbContext m_appDbContext;

        public DepotSolutionSQLServeur(AppDbContext appDbContext)
        {
            m_appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }

        public void AjouterSolution(Solution p_solution)
        {
            if (p_solution is null)
            {
                throw new ArgumentNullException(nameof(p_solution));
            }

            this.m_appDbContext.Solution.Add(new SolutionDTO(p_solution));
            this.m_appDbContext.SaveChanges();
        }

        public Solution CreerSolution(string p_pathDir, int p_travailID)
        {
            Solution solution = new Solution();
            solution.Liens = p_pathDir;
            solution.TravailID = p_travailID;
            return solution;

        }

        public void RetirerSolutionParID(int p_id)
        {
            if (p_id < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(p_id));
            }

            Solution solutionASupprimer = ChercherSolutionParSolutionID(p_id);

            if(solutionASupprimer is null)
            {
                throw new ArgumentNullException(nameof(solutionASupprimer));
            }else{
                SolutionDTO solutionDTO = new SolutionDTO(solutionASupprimer);
                this.m_appDbContext.Solution.Remove(solutionDTO);
                this.m_appDbContext.SaveChanges();
            }
        }

        public Solution ChercherSolutionParSolutionID(int p_solutionID)
        {
            return this.m_appDbContext.Solution.Where(s => s.Solution_id == p_solutionID).Select(s => s.VersEntite()).FirstOrDefault();
        }

        public List<Solution> ChercherSolutionParTravailID(int p_travailID)
        {
            return this.m_appDbContext.Solution.Where(s => s.Travail_id == p_travailID).Select(s => s.VersEntite()).ToList();
        }
    }
}
