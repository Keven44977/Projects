using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.Model
{
    public class Inscription
    {
        public int InscriptionID { get; set; }
        public string EtudiantID { get; set; }
        public int CoursID { get; set; }

        public Inscription()
        {
        }

        public Inscription(int inscriptionID, string etudiantID, int coursID)
        {
            InscriptionID = inscriptionID;
            EtudiantID = etudiantID;
            CoursID = coursID;
        }
    }
}
