using Revue_Par_Les_Pairs_V2.Model;
using Revue_Par_Les_Pairs_V2.Model.Depot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Revue_Par_Les_Pairs_V2.DAL_SQLServeur.DepotSQLServeur
{
    public class DepotInscriptionsSQLServeur : IDepotInscriptions
    {
        AppDbContext m_appDbContext;

        public DepotInscriptionsSQLServeur(AppDbContext appDbContext)
        {
            m_appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }

        public void AjoutInscription(Inscription p_inscription)
        {
            if (p_inscription is null)
            {
                throw new ArgumentNullException(nameof(p_inscription));
            }
            //Ajouter une fonction que l'etudiant ne peut pas etre inscrit deux fois
            if (this.m_appDbContext.Inscription.Any(i=> i.Cours_id == p_inscription.CoursID && i.Etudiant_id == p_inscription.EtudiantID))
            {
                return;
            }

            this.m_appDbContext.Inscription.Add(new InscriptionDTO(p_inscription));
            this.m_appDbContext.SaveChanges();
        }

        public List<Etudiant> ListerEtudiantsParCoursID(int p_coursID)
        {
            List<Etudiant> listeEtudiant = new List<Etudiant>();

            List<string> listeEtudiantID = this.m_appDbContext.Inscription.Where(i => i.Cours_id == p_coursID).Select(i => i.Etudiant_id).ToList();

            foreach(string etudiantID in listeEtudiantID)
            {
                Etudiant etudiant = this.m_appDbContext.Etudiant.Where(e => e.Etudiant_id == etudiantID).Select(e => e.VersEntite()).SingleOrDefault();
                listeEtudiant.Add(etudiant);
            }

            return listeEtudiant;
        }

        public List<Inscription> ListerInscriptionEtudiantId(string p_id)
        {
            return this.m_appDbContext.Inscription.Where(i => i.Etudiant_id == p_id).Select(i => i.VersEntite()).ToList();
        }
    }
}
