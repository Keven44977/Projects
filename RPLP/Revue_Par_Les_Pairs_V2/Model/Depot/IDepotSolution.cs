using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.Model.Depot
{
    public interface IDepotSolution
    {
        public Solution CreerSolution(string p_NomFichierZip, int p_travailID);
        public void RetirerSolutionParID(int p_id);
        public List<Solution> ChercherSolutionParTravailID(int p_solutionID);
        public Solution ChercherSolutionParSolutionID(int p_solutionID);
        public void AjouterSolution(Solution p_solution);

    }
}
