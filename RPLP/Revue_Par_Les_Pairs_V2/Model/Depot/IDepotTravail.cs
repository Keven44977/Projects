using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.Model.Depot
{
   public interface IDepotTravail
    {
        public void AjouterTravail(Travail p_travail);
        public void RetirerTravail(int p_index);
        public List<Travail> ListerTravaux();
        public Travail ChercherTravailParID(int p_travailID);
        public List<Travail> ChercherTravauxParCours(int p_coursID);
    }
}
