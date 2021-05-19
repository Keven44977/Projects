using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.Model.Entite
{
    public class Correction
    {
        public int CorrectionID { get; set; }
        public int SolutionID { get; set; }
        public string EtudiantID { get; set; }
        public bool Finaliser { get; set; }

        public Correction()
        {
        }

        public Correction(int solutionID, string etudiantID)
        {
            SolutionID = solutionID;
            EtudiantID = etudiantID;
            Finaliser = false;
        }

        public Correction(int correctionID, int solutionID, string etudiantID, bool finaliser)
        {
            CorrectionID = correctionID;
            SolutionID = solutionID;
            EtudiantID = etudiantID;
            Finaliser = finaliser;
        }
    }
}
