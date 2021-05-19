using Revue_Par_Les_Pairs_V2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.DAL_SQLServeur
{
    public class CommentaireDTO
    {
        [Key]
        public int Commentaire_id { get; set; }
        public string Texte { get; set; }
        public int Numero_ligne { get; set; }
        public string Severite { get; set; }
        public string Etudiant_id { get; set; }
        public int Solution_id { get; set; }

        public CommentaireDTO()
        {
        }

        //Entite vers DTO
        public CommentaireDTO(Commentaire p_commentaires)
        {
            this.Commentaire_id = p_commentaires.CommentaireID;
            this.Numero_ligne = p_commentaires.Numero_ligne;
            this.Texte = p_commentaires.Texte;
            this.Etudiant_id = p_commentaires.EtudiantID;
            this.Solution_id = p_commentaires.SolutionID;
            this.Severite = p_commentaires.Severite;
        }

        //DTO vers entite
        public Commentaire VersEntite()
        {
            return new Commentaire(
                 this.Commentaire_id,
                 this.Numero_ligne,
                 this.Texte,
                 this.Etudiant_id,
                 this.Solution_id,
                 this.Severite
                );
        }
    }
}
