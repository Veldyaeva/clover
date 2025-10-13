using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.CuttingProduction.Models
{
    public class appeZakrNewView
    {
        public string Nom { get; set; }

        public string Nn { get; set; }

        public DateTime? Data_za { get; set; }

        public string Prm { get; set; }

        public decimal? P { get; set; }

        public decimal? Kol_p { get; set; }

        public decimal? P_tamp { get; set; }

        public decimal? Poet { get; set; }

        public decimal? Kol_zv { get; set; }

        public decimal? V { get; set; }

        public decimal? Kol_v { get; set; }

        public decimal? Stra { get; set; }

        public decimal? Stir { get; set; }

        public int? Bus { get; set; }

        public int? Gofp { get; set; }

        public decimal? P_pres { get; set; }

        public decimal? Zvet_all { get; set; }

        public string Men { get; set; }

        public string Prn { get; set; }

        public string Tema { get; set; }

        public string Tb_id { get; set; }

        public DateTime? Data_zap { get; set; }

        public DateTime? Data_cd { get; set; }

        public string Kodd { get; set; }

        public string Grup { get; set; }

        public string Articul { get; set; }

        public string Mod_v { get; set; }

        public string Mod { get; set; }

        public string Kod1 { get; set; }

        public string Kod2 { get; set; }

        public string Kod3 { get; set; }

        public string Kod4 { get; set; }

        public string Kod5 { get; set; }

        public string K_p1 { get; set; }

        public string K_p2 { get; set; }

        public string K_p3 { get; set; }

        public string K_p4 { get; set; }

        public string K_p5 { get; set; }

        public decimal? Skl_otgr { get; set; }

        public int? Skl_id_1c { get; set; }

        public string Baza { get; set; }

        public int? Ts_id { get; set; }

        public decimal? Tm { get; set; }

        public string Rost { get; set; }

        public string Kod_zv { get; set; }

        public DateTime? Datecolor { get; set; }

        public string Mod_blok { get; set; }

        public decimal? Proz_all { get; set; }

        public decimal? Proz_base { get; set; }

        public int? Ta_id { get; set; }

        public decimal? Id_sbit { get; set; }

        public decimal? Printer { get; set; }

        public decimal? Nabiv_all { get; set; }

        public decimal? Laz { get; set; }

        public int? Nom_lekal { get; set; }

        public DateTime? Data_cd_n { get; set; }

        public decimal? Kod_stir { get; set; }

        public string Psa_tartpoln { get; set; }

        public int? Dtf_print { get; set; }

        public int? Bd { get; set; }
        public string nZvet {  get; set; }
        public int? tk_id { get; set; }
        public int? stk_id { get; set; }
        public string? parent_nn { get; set; }
        public bool? isKomplekt { get; set; }
        public bool? isNabor { get; set; }

    }
}
