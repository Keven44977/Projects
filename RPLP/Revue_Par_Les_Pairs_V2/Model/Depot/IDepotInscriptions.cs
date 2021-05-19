using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.Model.Depot
{
  public interface IDepotInscriptions
    {
        public void AjoutInscription(Inscription p_inscription);
        public List<Etudiant> ListerEtudiantsParCoursID(int p_coursID);
        public List<Inscription> ListerInscriptionEtudiantId(string p_id);
    }
}
