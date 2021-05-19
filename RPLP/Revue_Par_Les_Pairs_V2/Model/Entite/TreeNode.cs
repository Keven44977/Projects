using System;
using System.Collections.Generic;

namespace Revue_Par_Les_Pairs_V2.Model
{
    public class TreeNode
    {
        public string Nom { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public List<TreeNode> EnfantsNodes { get; set; }

        public TreeNode(string p_nom, string p_path, string p_extension)
        {
            this.Nom = p_nom ?? throw new ArgumentNullException(nameof(p_nom));
            this.Path = p_path ?? throw new ArgumentNullException(nameof(p_path));
            this.Extension = p_extension ?? throw new ArgumentNullException(nameof(p_extension));
            this.EnfantsNodes = new List<TreeNode>();
        }
    }
}
