using System;
using System.Collections.Generic;
using System.Linq;
using Revue_Par_Les_Pairs_V2.Model;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Revue_Par_Les_Pairs_V2.DAL_SQLServeur
{
    public class InscriptionDTO
    {
        [Key]
        public int Inscription_id { get; set; }
        public string Etudiant_id { get; set; }
        public int Cours_id { get; set; }

        public InscriptionDTO()
        {
        }

        //Entite vers DTO
        public InscriptionDTO(Inscription p_inscriptions)
        {
            this.Inscription_id = p_inscriptions.InscriptionID;
            this.Etudiant_id = p_inscriptions.EtudiantID;
            this.Cours_id = p_inscriptions.CoursID;
        }

        //DTO vers entite
        public Inscription VersEntite()
        {
            return new Inscription(
                this.Inscription_id,
                this.Etudiant_id,
                this.Cours_id
                );
        }
    }
}
