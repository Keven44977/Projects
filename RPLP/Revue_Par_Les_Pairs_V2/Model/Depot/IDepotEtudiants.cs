using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.Model.Depot
{
    public interface IDepotEtudiants
    {
        public void AjouterEtudiant(Etudiant p_etudiant);
        public void AjouterListeEtudiants(IEnumerable<Etudiant> p_listeEtudiants);
        public IEnumerable<Etudiant> ListerEtudiants();
        public Etudiant ChercherEtudiantParEtudiantID(string p_etudiantID);
    }
}
