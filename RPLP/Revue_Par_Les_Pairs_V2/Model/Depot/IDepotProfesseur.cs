using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.Model.Depot
{
    public interface IDepotProfesseur
    {
        public void AJouterProfesseur(Professeur p_professeur);
        public IEnumerable<Professeur> ListerProfesseurs();
        public Etudiant ChercherProfesseurParProfesseurID(int p_professeurID);
    }
}
