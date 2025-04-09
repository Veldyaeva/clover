using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Models
{
   public class NormKont
    {
        public int AnnId { get; set; }
        public int Kod { get; set; }
        [Column("kod_o")]
        public string KodO { get; set; }
        public string Text { get; set; }
        public string Spec { get; set; }
        [Column("razryd")]
        public int Razryad { get; set; }
        public string Obor { get; set; }
        public int Sek { get; set; }
        public int Seb { get; set; }
        public int N { get; set; }
        [Column("n_ch")]
        public int NCh { get; set; }
        public int N1 { get; set; }
        public int SebS { get; set; }
    }
}
