using SewingProduction.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Models
{
   public class NormDopObr : INewable
    {
        public int AnnId { get; set; }
        public string Kod { get; set; }
        public int SekP { get; set; }
        public int SekV { get; set; }
        public int SekStra { get; set; }
        public int SekTamp { get; set; }
        [NotMapped]
        public bool IsNew { get; set; } = true;
    }
}
