using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Articul.Models
{
    public class SpArtPreviewModel
    {
        public string Ko { get; set; }
        public string Kod { get; set; }
        [NotMapped]
        public string Kodd { get; set; }
        public string Grup { get; set; }
        public string Articul { get; set; }
        public string Mod { get; set; }
        public string Razm { get; set; }
        public string Sost { get; set; }
        public string Kle { get; set; }
        public string tmName { get; set; }


    }
}
