using System;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.Model
{
    public class Professeur
    {
        public int ProfesseurID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public Professeur()
        {
        }

        public Professeur(int professeurID, string nom, string prenom)
        {
            ProfesseurID = professeurID;
            Nom = nom ?? throw new ArgumentNullException(nameof(nom));
            Prenom = prenom ?? throw new ArgumentNullException(nameof(prenom));
        }
    }
}
