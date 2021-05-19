using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revue_Par_Les_Pairs_V2.Model
{
    public class Fichier
    {
        public int TravailID { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
