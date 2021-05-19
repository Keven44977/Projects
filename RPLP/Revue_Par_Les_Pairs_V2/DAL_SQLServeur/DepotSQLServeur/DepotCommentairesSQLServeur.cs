using Revue_Par_Les_Pairs_V2.Model;
using Revue_Par_Les_Pairs_V2.Model.Depot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.DAL_SQLServeur
{
    public class DepotCommentairesSQLServeur : IDepotCommentaires
    {
        AppDbContext m_appDbContext;

        public DepotCommentairesSQLServeur(AppDbContext appDbContext)
        {
            m_appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }

        //Ajouter un commentaire dans la base de données 
        public void AjouterCommentaire(Commentaire p_commentaires)
        {
            if (p_commentaires is null)
            {
                throw new ArgumentNullException(nameof(p_commentaires));
            }

            this.m_appDbContext.Commentaire.Add(new CommentaireDTO(p_commentaires));
            this.m_appDbContext.SaveChanges();
        }

        //Chercher tous les commmentaires d'un etudiant spécifique
        public List<Commentaire> ChercherCommentairesParEtudiantID(string p_etudiantID)
        {
            return this.m_appDbContext.Commentaire.Where(c => c.Etudiant_id == p_etudiantID).Select(c => c.VersEntite()).ToList();
        }

        //Lister tous les commentaires dans la base de données
        public List<Commentaire> ListerCommentaires()
        {
            return this.m_appDbContext.Commentaire.Select(c => c.VersEntite()).ToList();
        }
    }
}
