using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Models
{
    public class NormRasz
    {
        public int nrId { get; set; }
        public int AnnId { get; set; }
        public int Kod { get; set; }
        public int Kod_o { get; set; }
        public string Text { get; set; }
        [Column("razryd")]
        public int Razryad { get; set; }
        public int N { get; set; }
        public int N1 { get; set; }
        public int Sek { get; set; }
        public string Obor { get; set; }
        public int Kod_podr { get; set; }
        public int Kod_proizv { get; set; }
        public string Spec { get; set; }
        public int Kod_ob { get; set; }
        [NotMapped]
        public string TextProizv { get; set; }
        [NotMapped]
        public string TextOb { get; set; }
        [NotMapped]
        public string TextVyaz { get; set; }
    }
}
