using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Revue_Par_Les_Pairs_V2.Model;

namespace Revue_Par_Les_Pairs_V2.DAL_SQLServeur
{
    public class ProfesseurDTO
    {
        [Key]
        public int Professeur_id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public ProfesseurDTO()
        {
        }

        //Entite vers DTO
        public ProfesseurDTO(Professeur p_professeurs)
        {
            this.Professeur_id = p_professeurs.ProfesseurID;
            this.Nom = p_professeurs.Nom;
            this.Prenom = p_professeurs.Prenom;
        }

        //DTO vers entite
        public Professeur VersEntite()
        {
            return new Professeur(
                this.Professeur_id,
                this.Nom,
                this.Prenom
                );
        }
    }
}
