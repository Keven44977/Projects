using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.Model.Depot
{
    public interface IDepotCours
    {
        public List<Cours> ListerCours();
        public List<Cours> ChercherCoursParCoursID(int p_coursID);
        public List<Cours> ChercherCoursParProfesseurID(int p_professeurID);
        public void AjouterCours(Cours p_cours);
    }
}
