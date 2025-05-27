using DevExpress.DataAccess.Sql;
using SewingProduction.form;
using SewingProduction.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace SewingProduction.Models
{
    public class FurnitF
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
        public decimal kol_f_up { get; set; }
        public decimal kol_f_ra { get; set; }
        public string kod_o { get; set; }
        public decimal n { get; set; }
        public string kod_f_d { get; set; }
        public DateTime? data_f_z { get; set; }
        public string kod_fur { get; set; }
        public string art_fur { get; set; }
        public string kod_fur_ar { get; set; }
        public int n_pp { get; set; }
        public int cl1_id { get; set; }
        public int fre_id { get; set; }
        public DateTime? dateAdd { get; set; }
        public int ffID { get; set; }
        public int cl2_id { get; set; }
        public int cl3_id { get; set; }
        public int ffSpecRez { get; set; }

    }
}
