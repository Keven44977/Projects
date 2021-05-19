using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Revue_Par_Les_Pairs_V2.Model;

namespace Revue_Par_Les_Pairs_V2.DAL_SQLServeur
{
    public class SolutionDTO
    {
        [Key]
        public int Solution_id { get; set; }
        public string Liens { get; set; }
        public int Travail_id { get; set; }
 
        public SolutionDTO()
        {
        }

        //Entite vers DTO
        public SolutionDTO(Solution p_solution)
        {
            this.Solution_id = p_solution.SolutionID;
            this.Liens = p_solution.Liens;
            this.Travail_id = p_solution.TravailID;
        }

        //DTO vers entite
        public Solution VersEntite()
        {
            return new Solution(
                this.Liens,
                this.Travail_id,
                this.Solution_id
                );
        }
    }
}
