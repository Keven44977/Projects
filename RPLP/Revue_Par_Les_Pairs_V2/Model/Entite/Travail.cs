using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.Model
{
    public class Travail
    {
        public int TravailID { get; set; }
        public string Nom { get; set; }
        public DateTime DateDeRemise { get; set; }
        public int NombresDeRevues { get; set; }
        public int CoursID { get; set; }

        public Travail()
        {
        }

        public Travail(int travailID, string nom, DateTime dateDeRemise,int nombreRevue ,int coursID)
        {
            TravailID = travailID;
            Nom = nom ?? throw new ArgumentNullException(nameof(nom));
            DateDeRemise = dateDeRemise;
            NombresDeRevues = nombreRevue;
            CoursID = coursID;
        }
    }
}
