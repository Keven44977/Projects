using Revue_Par_Les_Pairs_V2.Model;
using Revue_Par_Les_Pairs_V2.Model.Depot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.DAL_SQLServeur.DepotSQLServeur
{
    public class DepotEtudiantsSQLServeur : IDepotEtudiants
    {
        AppDbContext m_appDbContext;

        public DepotEtudiantsSQLServeur(AppDbContext appDbContext)
        {
            m_appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }

        public void AjouterEtudiant(Etudiant p_etudiant)
        {
            if (p_etudiant is null)
            {
                throw new ArgumentNullException(nameof(p_etudiant));
            }

            this.m_appDbContext.Etudiant.Add(new EtudiantDTO(p_etudiant));
            this.m_appDbContext.SaveChanges();
        }

        public void AjouterListeEtudiants(IEnumerable<Etudiant> p_listeEtudiants)
        {
            if (p_listeEtudiants is null)
            {
                throw new ArgumentNullException(nameof(p_listeEtudiants));
            }

            foreach (Etudiant etudiant in p_listeEtudiants)
            {
                this.m_appDbContext.Etudiant.Add(new EtudiantDTO(etudiant));
            }
            this.m_appDbContext.SaveChanges();
        }

        public Etudiant ChercherEtudiantParEtudiantID(string p_etudiantID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Etudiant> ListerEtudiants()
        {
            throw new NotImplementedException();
        }
    }
}
