using Revue_Par_Les_Pairs_V2.Model;
using Revue_Par_Les_Pairs_V2.Model.Depot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.DAL_SQLServeur.DepotSQLServeur
{
    public class DepotProfesseursSQLServeur : IDepotProfesseur
    {
        AppDbContext m_appDbContext;

        public DepotProfesseursSQLServeur(AppDbContext appDbContext)
        {
            m_appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }

        public void AJouterProfesseur(Professeur p_professeur)
        {
            throw new NotImplementedException();
        }

        public Etudiant ChercherProfesseurParProfesseurID(int p_professeurID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Professeur> ListerProfesseurs()
        {
            throw new NotImplementedException();
        }
    }
}
