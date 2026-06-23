using System;

namespace SewingProduction.Features.Yarn.Models
{
    public sealed class FabricSpreadingRow
    {
        public bool v { get; set; }
        public string karta { get; set; }
        public DateTime? data { get; set; }
        public string tkan { get; set; }
        public decimal? seb { get; set; }
        public string vid_tk { get; set; }
        public decimal? kol_m { get; set; }
        public decimal? rash_ost { get; set; }
        public decimal? nerash_ost { get; set; }
        public decimal? kl { get; set; }
        public decimal? pogr { get; set; }
        public decimal? konc { get; set; }
        public decimal? rash_m { get; set; }
        public decimal? percent { get; set; }
    }
}
