using System;

namespace SewingProduction.Features.Yarn.Models
{
    public sealed class YarnCostHistoryRow
    {
        public DateTime? data_izm { get; set; }
        public decimal? ss_do { get; set; }
        public decimal? ss_posle { get; set; }
        public decimal? norma { get; set; }
        public decimal? seb { get; set; }
        public string tkn_oboz { get; set; }
        public string tkn_naim { get; set; }
        public string tkn_kod { get; set; }
        public decimal? tkn_norma { get; set; }
        public decimal? tkn_cena { get; set; }
        public decimal? kol_sek { get; set; }
        public decimal? zatraty_zp { get; set; }
        public decimal? dop_rash { get; set; }
        public decimal? koef_pr_zatrat { get; set; }
        public decimal? koef { get; set; }
        public string komp_user { get; set; }
    }
}
