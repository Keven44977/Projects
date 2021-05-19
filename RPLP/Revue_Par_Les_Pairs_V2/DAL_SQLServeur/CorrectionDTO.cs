using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Revue_Par_Les_Pairs_V2.Model.Entite;

namespace Revue_Par_Les_Pairs_V2.DAL_SQLServeur
{
    public class CorrectionDTO
    {
        [Key]
        public int Correction_id { get; set; }
        public int Solution_id { get; set; }
        public string Etudiant_id { get; set; }
        public bool Finaliser { get; set; }

        public CorrectionDTO()
        {
        }

        //Entite vers DTO
        public CorrectionDTO(Correction p_corrections)
        {
            this.Correction_id = p_corrections.CorrectionID;
            this.Solution_id = p_corrections.SolutionID;
            this.Etudiant_id = p_corrections.EtudiantID;
            this.Finaliser = p_corrections.Finaliser;
        }

        //DTO vers entite
        public Correction VersEntite()
        {
            return new Correction(
                this.Correction_id,
                 this.Solution_id,
                 this.Etudiant_id,
                 this.Finaliser
                );
        }
    }
}
