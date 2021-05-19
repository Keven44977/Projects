using Revue_Par_Les_Pairs_V2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.DAL_SQLServeur
{
    public class EtudiantDTO
    {
        [Key]
        public string Etudiant_id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public EtudiantDTO()
        {
        }

        //Entite vers DTO
        public EtudiantDTO(Etudiant p_etudiants)
        {
            this.Etudiant_id = p_etudiants.EtudiantID;
            this.Nom = p_etudiants.Nom;
            this.Prenom = p_etudiants.Prenom;
        }

        //DTO vers entite
        public Etudiant VersEntite()
        {
            return new Etudiant(
                this.Etudiant_id,
                this.Nom,
                this.Prenom
                );
        }
    }
}
