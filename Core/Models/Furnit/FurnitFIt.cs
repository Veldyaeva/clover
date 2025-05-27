using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.DataAccess.Sql;
using SewingProduction.form;
using SewingProduction.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace SewingProduction.Models
{
    public class FurnitFIt
    {

        public string kod_f { get; set; }
        public string kodd { get; set; }
        public string grup { get; set; }
        public string articul { get; set; }
        public string mod { get; set; }
        public int kol { get; set; }
        public string kod_dr { get; set; }
        public string art { get; set; }
        public string t_ed { get; set; }
        public decimal kol_f { get; set; }
        public decimal kol_f_o { get; set; }
        public decimal kol_f_br { get; set; }
        public decimal kol_f_up { get; set; }
        public decimal kol_f_ra { get; set; }
        public string kod_o { get; set; }
        public decimal seb { get; set; }
        public string kod_f_d { get; set; }
        public string kod_art { get; set; }
        public DateTime? data_f_z { get; set; }
        public string koord { get; set; }
        public DateTime? data_akt { get; set; }
        public int n_pp { get; set; }
        public DateTime? data_buh { get; set; }
        public decimal reestr_buh { get; set; }
        public DateTime? data_prov { get; set; }
        public int tabno { get; set; }
        public int korrekt { get; set; }
        public int id_f_it { get; set; }
        public decimal rezerv_ip { get; set; }
        public decimal rez8306 { get; set; }
        public decimal rez7666 { get; set; }
        public decimal rez5914 { get; set; }
        public decimal rez40195 { get; set; }
        public decimal rez16163 { get; set; }
        public decimal rez7152 { get; set; }
        public decimal rez63684 { get; set; }
        public int id_pokup { get; set; }
        public int id_m { get; set; }
        public DateTime? data_m { get; set; }
        public int day_m { get; set; }
        public int kol_m { get; set; }
        public int kol_m_all { get; set; }
        public int pr_set_add { get; set; }
        public int cl1_id { get; set; }
        public int cl2_id { get; set; }
        public int cl3_id { get; set; }
        public int fre_id { get; set; }
        public DateTime? dateadd { get; set; }
        public int ffiid { get; set; }
        public int id_un { get; set; }
        public string komp_f_z { get; set; }
        public int cl4_id { get; set; }
        public string ffikodizd { get; set; }
        public string ffirfsshtr { get; set; }
        public int ffirfid { get; set; }
        public int ffimainpos { get; set; }
        public int fficonrplid { get; set; }
        public int ffiid_par { get; set; }
        public DateTime? grup_prov_date { get; set; }
        public int grup_prov_tab { get; set; }
        public string grup_prov_komp { get; set; }
        public int ffisID { get; set; }
        public int tab_grup { get; set; }
        public int vozvrat { get; set; }

    }
}
