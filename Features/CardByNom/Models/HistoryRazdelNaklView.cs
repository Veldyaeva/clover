using DevExpress.DataAccess.Sql;
using SewingProduction.form;
using SewingProduction.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SewingProduction.Features.CardByNom.Models
{
    public class HistoryRazdelNaklViewByIz
    {

        public DateTime? data_izm { get; set; }
        public int skl_otgr_b { get; set; }
        public int skl_otgr_c { get; set; }
        public string iz_b { get; set; }
        public string iz_c { get; set; }
        public int kol_b { get; set; }
        public int kol_c { get; set; }
        public int kol_new { get; set; }
        public string mod { get; set; }
        public string razm { get; set; }
        public int iz_ob_prch { get; set; }
        public string status { get; set; }
        public string komp_name { get; set; }
        public string komp_del { get; set; }
        public int id { get; set; }
        public int skl_id_1c_b { get; set; }
        public int skl_id_1c_c { get; set; }
        public string prich_sokr { get; set; }
        public string n_pach { get; set; }


    }
}
