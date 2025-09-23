using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using SewingProduction.Interfaces;


namespace SewingProduction.Core.Models
{
    public class ArticulModel :  INewable, IModifiable, IDeletable, INotifyPropertyChanged
    {
        //private int _kod;
        //private string _grup;
        //private string _articul;
        //private string _mod;
        //private string _razm;
        //private string _sost;
        //private string _po = " ";
        //private int _pr_po = 0;
        //private int _kod_v;
        //private string _kle;

        //public string grup
        //{
        //    get => _grup;
        //    set { if (_grup != value) { _grup = value; OnPropertyChanged(nameof(grup)); } }
        //}
        public string Kod { get; set; }
        [NotMapped]
        public string Kodd { get; set; }
        public string Grup { get; set; }
        public string Articul { get; set; }
        public string Mod { get; set; }
        public string Razm { get; set; }
        public string Sost { get; set; }
        public string Sost2_old { get; set; }
        public string Gost { get; set; }
        public decimal? Norm_t { get; set; }
        public decimal? Norm_r { get; set; }
        public decimal? Sek_shv { get; set; }
        public decimal? Sh_r { get; set; }
        public double? Seb_r { get; set; }
        public decimal? Norm_n { get; set; }
        public decimal? Kat_n { get; set; }
        public decimal? Seb_n { get; set; }
        public decimal? Seb_z { get; set; }
        public decimal? Koef { get; set; }
        public decimal? Seb_rekom { get; set; }
        public decimal? Seb_proizv { get; set; }
        public string Po { get; set; }
        public decimal? Seb_z_s { get; set; }
        public decimal? Sek { get; set; }
        public decimal? Sek_vyaz5 { get; set; }
        public decimal? Sek_vyaz7 { get; set; }
        public decimal? Sek_vyaz12 { get; set; }
        public decimal? Sek_vyaz { get; set; }
        public string Pict { get; set; }
        public string Kod_shtr { get; set; }
        public string Kod_shtr_k { get; set; }
        public string Gr { get; set; }
        public string X { get; set; }
        public string Text { get; set; }
        public string Kle { get; set; }
        public decimal? Grupp { get; set; }
        public decimal? P { get; set; }
        public decimal? V { get; set; }
        public decimal? S { get; set; }
        public string Kod_tov { get; set; }
        public string Gruppa { get; set; }
        public string P_gruppa { get; set; }
        public string Text_m { get; set; }
        public string Tkb { get; set; }
        public string Kod_t1 { get; set; }
        public string Tkb1 { get; set; }
        public decimal? Norm_t1 { get; set; }
        public string Opis_t1 { get; set; }
        public string Kod_t2 { get; set; }
        public string Tkb2 { get; set; }
        public decimal? Norm_t2 { get; set; }
        public string Opis_t2 { get; set; }
        public string Kod_t3 { get; set; }
        public string Tkb3 { get; set; }
        public decimal? Norm_t3 { get; set; }
        public string Opis_t3 { get; set; }
        public string Kod_t4 { get; set; }
        public string Tkb4 { get; set; }
        public decimal? Norm_t4 { get; set; }
        public string Opis_t4 { get; set; }
        public decimal? Baza { get; set; }
        public decimal? Kod_v { get; set; }
        public string Kod_t5 { get; set; }
        public string Tkb5 { get; set; }
        public decimal? Norm_t5 { get; set; }
        public string Opis_t5 { get; set; }
        public string Kod_t6 { get; set; }
        public string Tkb6 { get; set; }
        public decimal? Norm_t6 { get; set; }
        public string Opis_t6 { get; set; }
        public decimal? Seb_dop { get; set; }
        public decimal? Seb_t1 { get; set; }
        public decimal? Seb_t3 { get; set; }
        public decimal? Seb_t2 { get; set; }
        public decimal? Seb_t4 { get; set; }
        public decimal? Seb_t5 { get; set; }
        public decimal? Seb_t6 { get; set; }
        public decimal? Sek_vyaz6 { get; set; }
        public decimal? Sek_vyaz10 { get; set; }
        public decimal? Sek_vyazo { get; set; }
        public decimal? Normapryz { get; set; }
        public int? Id_gost { get; set; }
        public int? Id_svyaz { get; set; }
        public decimal? Koef_pr { get; set; }
        public decimal? Ob_izd { get; set; }
        public int? Stavka_nds { get; set; }
        public string Kod_t7 { get; set; }
        public string Tkb7 { get; set; }
        public decimal? Norm_t7 { get; set; }
        public decimal? Seb_t7 { get; set; }
        public string Opis_t7 { get; set; }
        public int? Sposob_up { get; set; }
        public int? Id_country { get; set; }
        public int? Old_prch { get; set; }
        public decimal? Sek_vyaz70 { get; set; }
        public decimal? Sek_vyaz3 { get; set; }
        public decimal? Koef_d { get; set; }
        public int? St_nds { get; set; }
        public int? Upd_razm { get; set; }
        public decimal? Gl_rekom { get; set; }
        public decimal? Sek_kr { get; set; }
        public decimal? Arh { get; set; }
        public int? Nds { get; set; }
        public string Ed_izm { get; set; }
        public decimal? Brak_t1 { get; set; }
        public decimal? Brak_t2 { get; set; }
        public decimal? Brak_t3 { get; set; }
        public decimal? Brak_t4 { get; set; }
        public decimal? Brak_t5 { get; set; }
        public decimal? Brak_t6 { get; set; }
        public decimal? Brak_t7 { get; set; }
        public decimal? Brak_avg { get; set; }
        public decimal? Cena_prdc { get; set; }
        public string Komb_det { get; set; }
        public string Komb_izd { get; set; }
        public string Sostav { get; set; }
        public string Kod_t { get; set; }
        public decimal? K_kg_m1 { get; set; }
        public decimal? K_kg_m2 { get; set; }
        public decimal? K_kg_m3 { get; set; }
        public decimal? K_kg_m4 { get; set; }
        public decimal? K_kg_m5 { get; set; }
        public decimal? K_kg_m6 { get; set; }
        public decimal? K_kg_m7 { get; set; }
        public string Sost2 { get; set; }
        public decimal Is_furnit { get; set; }
        public decimal? Is_upak { get; set; }
        public decimal? Sek_vyaz14 { get; set; }
        public int? gcg_id { get; set; }
        public int? Gbm_id { get; set; }
        public int? Gcgp_id { get; set; }
        public int? Gbt_id { get; set; }
        public decimal? Bus { get; set; }
        public decimal? Stra { get; set; }
        public decimal? P_pres { get; set; }
        public decimal? Seb_usl { get; set; }
        public decimal Art_segm { get; set; }
        public decimal Art_family { get; set; }
        public decimal Art_class { get; set; }
        public decimal Art_block { get; set; }
        public int Scid_n { get; set; }
        public string Kod_tnved { get; set; }
        public DateTime? DateOpis { get; set; }
        public string Mtrl_up { get; set; }
        public string Mtrl_pdkl { get; set; }
        public decimal? Vid_obuv { get; set; }
        public string Mtrl_down { get; set; }
        public int Sql_pr_add { get; set; }
        public int? Sql_pr_upd { get; set; }
        public string Komp_name { get; set; }
        public DateTime? Date_add { get; set; }
        public int? Sek_vyaz62 { get; set; }
        public int? Sek_vyaz71 { get; set; }
        public int? Sek_vyaz72 { get; set; }
        public string Sost3 { get; set; }
        public int? Ag_id { get; set; }
        public short? Kruj { get; set; }
        public string Kod_lv3 { get; set; }
        public decimal? Sek_cord { get; set; }
        public decimal? Norm_cord { get; set; }
        public int? Tgm_id_n { get; set; }
        public DateTime? Dateutvkk { get; set; }
        public decimal? Sum_zarpl { get; set; }
        public decimal? Sum_dopopl { get; set; }
        public decimal? Sum_strvznos { get; set; }
        public decimal? Sum_sebraskr { get; set; }
        public decimal? Sum_komplnum { get; set; }
        public decimal? Sek_vyaz57 { get; set; }
        public decimal? Sek_vyaz18 { get; set; }
        public int? Annid { get; set; }




        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        
        [NotMapped]
        public bool IsModified { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
        [NotMapped]
        public bool IsDeleted { get; set; } = false;
    }
}
