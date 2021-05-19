using Revue_Par_Les_Pairs_V2.Model.Entite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.Model.Depot
{
    public interface IDepotCorrections
    {
        public void AjouterCorrection(Correction p_correction);
        public Correction CreerCorrection(string p_etudiantID, int p_solutionID);
        public List<Correction> ListerCorrections();
        public List<Correction> ChercherCorrectionsParEtudiantID(string p_etudiantID);
        public Correction ChercherCorrectionParSolutionID(int p_solutionID);
        public List<Correction> ChercherCorrectionsParSolutionID(int p_solutionID);
        public int NombreCopieCorrigeeParTravail(int p_travailID, string p_etudiantID);
        public void FinaliserCorrection(int p_solutionID, string p_etudiantID);
    }
}
