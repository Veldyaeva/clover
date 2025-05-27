using DevExpress.DataAccess.Sql;
using SewingProduction.form;
using SewingProduction.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace SewingProduction.Models
{
    public class FurnitNView
    {

        public int n_z { get; set; }
        public DateTime? data_f_o { get; set; }
        public DateTime? data_f_z { get; set; }
        public string br { get; set; }
        public string kod_f { get; set; }
        public string prim { get; set; }
        public string v { get; set; }
        public int tab { get; set; }
        public string fio { get; set; }
        public int vidf { get; set; }
        public string name_comp { get; set; }
        public int nom_m15 { get; set; }
        public DateTime? data_plan { get; set; }
        public int KolPrint { get; set; }
        public int sklSource { get; set; }
        public int zayavKK { get; set; }
        public DateTime? dateAdd { get; set; }
        public int fnID { get; set; }
        public string nn_br { get; set; }
        public string nn_up { get; set; }
        public string komp_f_z { get; set; }
        public int usl { get; set; }
        public int u_f { get; set; }
        public int usl_up { get; set; }
        public int frmIdBr { get; set; }
        public int frmIdUp { get; set; }
        public string VidFName { get; set; }

    }
}
