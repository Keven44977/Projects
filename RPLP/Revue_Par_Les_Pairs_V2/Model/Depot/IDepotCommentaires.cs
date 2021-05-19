using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.Model.Depot
{
    public interface IDepotCommentaires
    {
        public List<Commentaire> ListerCommentaires();
        public List<Commentaire> ChercherCommentairesParEtudiantID(string p_etudiantID);
        public void AjouterCommentaire(Commentaire p_commentaires);
    }
}
