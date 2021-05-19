using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Revue_Par_Les_Pairs_V2.Model;

namespace Revue_Par_Les_Pairs_V2.DAL_SQLServeur
{
    public class TravailDTO
    {
        [Key]
        public int Travail_id { get; set; }
        public string Nom { get; set; }
        public DateTime DateDeRemise { get; set; }
        public int NombresDeRevues { get; set; }
        public int Cours_id { get; set; }

        public TravailDTO()
        {
        }

        //Entite vers DTO
        public TravailDTO(Travail p_travaux)
        {
            this.Travail_id = p_travaux.TravailID;
            this.Nom = p_travaux.Nom;
            this.DateDeRemise = p_travaux.DateDeRemise;
            this.NombresDeRevues = p_travaux.NombresDeRevues;
            this.Cours_id = p_travaux.CoursID;
        }

        //DTO vers entite
        public Travail VersEntite()
        {
            return new Travail(
                Travail_id,
                Nom,
                DateDeRemise,
                NombresDeRevues,
                Cours_id
                );
        }
    }
}
