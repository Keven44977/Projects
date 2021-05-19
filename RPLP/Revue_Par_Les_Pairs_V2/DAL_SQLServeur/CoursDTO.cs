using Revue_Par_Les_Pairs_V2.Model;
using System.ComponentModel.DataAnnotations;

namespace Revue_Par_Les_Pairs_V2.DAL_SQLServeur
{
    public class CoursDTO
    {
        [Key]
        public int Cours_id { get; set; }
        public string Nom { get; set; }
        public int Professeur_id { get; set; }

        public CoursDTO()
        {
        }

        //Entite vers DTO
        public CoursDTO(Cours p_cours)
        {
            this.Cours_id = p_cours.CoursID;
            this.Nom = p_cours.Nom;
            this.Professeur_id = p_cours.ProfesseurID;
        }

        //DTO vers entite
        public Cours VersEntite()
        {
            return new Cours(
                this.Cours_id,
                this.Nom,
                this.Professeur_id
                );
        }
    }
}
