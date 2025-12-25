using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Articul.Models
{
    public class ArtKomplektModel
    {
        public int Ak_id { get; set; }
        public string Parent_nn { get; set; }
        public string Sostav { get; set; }
        public int tk_id { get; set; }
        public int id_gost { get; set; }
        public int ag_id_grupgost { get; set; }
    }
}
