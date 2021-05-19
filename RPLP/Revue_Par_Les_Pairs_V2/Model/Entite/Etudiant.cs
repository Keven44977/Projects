using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.Model
{
    public class Etudiant
    {
        public string EtudiantID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public Etudiant()
        {
        }

        public Etudiant(string etudiantID, string nom, string prenom)
        {
            EtudiantID = etudiantID ?? throw new ArgumentNullException(nameof(etudiantID));
            Nom = nom ?? throw new ArgumentNullException(nameof(nom));
            Prenom = prenom ?? throw new ArgumentNullException(nameof(prenom));
        }
    }
}
