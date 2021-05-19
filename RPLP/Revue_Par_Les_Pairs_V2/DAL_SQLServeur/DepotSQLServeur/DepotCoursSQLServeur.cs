using Revue_Par_Les_Pairs_V2.Model;
using Revue_Par_Les_Pairs_V2.Model.Depot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Revue_Par_Les_Pairs_V2.DAL_SQLServeur.DepotSQLServeur
{
    public class DepotCoursSQLServeur : IDepotCours
    {
        AppDbContext m_appDbContext;

        public DepotCoursSQLServeur(AppDbContext appDbContext)
        {
            m_appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }

        public void AjouterCours(Cours p_cours)
        {
            if (p_cours is null)
            {
                throw new ArgumentNullException(nameof(p_cours));
            }
            if (this.m_appDbContext.Cours.Any(c => c.Cours_id == p_cours.CoursID))
            {
                throw new InvalidOperationException("Deja dans le depot");
            }

            this.m_appDbContext.Cours.Add(new CoursDTO(p_cours));
            this.m_appDbContext.SaveChanges();
        }

        public List<Cours> ChercherCoursParCoursID(int p_coursID)
        {
            return this.m_appDbContext.Cours.Where(c => c.Cours_id == p_coursID).Select(c => c.VersEntite()).ToList();
        }

        public List<Cours> ChercherCoursParProfesseurID(int p_professeurID)
        {
            return this.m_appDbContext.Cours.Where(c => c.Professeur_id == p_professeurID).Select(c => c.VersEntite()).ToList();
        }

        public List<Cours> ListerCours()
        {
            return this.m_appDbContext.Cours.Select(c => c.VersEntite()).ToList();
        }
    }
}
