using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.Model
{
    public class Commentaire
    {
        public int CommentaireID { get; set; }
        public int Numero_ligne { get; set; }
        public string Texte { get; set; }
        public string EtudiantID { get; set; }
        public int SolutionID { get; set; }
        public string Severite { get; set; }

        public Commentaire()
        {
        }

        public Commentaire(string texte, string etudiantID, string severite, int solutionID)
        {
            Texte = texte ?? throw new ArgumentNullException(nameof(texte));
            EtudiantID = etudiantID ?? throw new ArgumentNullException(nameof(etudiantID));
            Severite = severite ?? throw new ArgumentNullException(nameof(severite));
            SolutionID = solutionID;
        }

        public Commentaire(int commentaireID, int numero_ligne, string texte, string etudiantID, int solutionID, string severite)
        {
            CommentaireID = commentaireID;
            Numero_ligne = numero_ligne;
            Texte = texte ?? throw new ArgumentNullException(nameof(texte));
            EtudiantID = etudiantID ?? throw new ArgumentNullException(nameof(etudiantID));
            SolutionID = solutionID;
            Severite = severite ?? throw new ArgumentNullException(nameof(severite));
        }
    }
}
