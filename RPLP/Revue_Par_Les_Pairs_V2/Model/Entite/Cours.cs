using System;

namespace Revue_Par_Les_Pairs_V2.Model
{
    public class Cours
    {
        public int CoursID { get; set; }
        public string Nom { get; set; }
        public int ProfesseurID { get; set; }

        public Cours()
        {
        }

        public Cours(int coursID, string nom, int professeurID)
        {
            CoursID = coursID;
            Nom = nom ?? throw new ArgumentNullException(nameof(nom));
            ProfesseurID = professeurID;
        }
    }
}
