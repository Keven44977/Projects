using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.Model
{
    public class Solution
    {
        public int SolutionID { get; set; }
        public string Liens { get; set; }
        public int TravailID { get; set; }
        public int NombreDeDistrubtion { get; set; }

        public Solution()
        {
        }

        public Solution(string liens, int travailID, int solutionID)
        {
            SolutionID = solutionID;
            Liens = liens;
            TravailID = travailID;
        }
    }
}
