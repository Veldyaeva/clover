using SewingProduction.Features.Articul;
using SewingProduction.form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.CuttingProduction.Models
{
    public class NastilList
    {
        public string tk { get; set; }
        public decimal kol { get; set; }
        public int tab1 { get; set; }
        public int tab2 { get; set; }
        public int tab3 { get; set; }
        public string v_n { get; set; }
        public int razr { get; set; }
        public string fio1 { get; set; }
        public string fio2 { get; set; }
        public string fio3 { get; set; }
        public string mg { get; set; }
        public long kart { get; set; }
        public int sek { get; set; }
        public int nakl_o { get; set; }
        public decimal chyl { get; set; }
        public string kod_pr { get; set; }
        public string kod { get; set; }
        public string t_articul { get; set; }
        public int nakl { get; set; }
        public string d_nn_nas { get; set; }
        public decimal seb_t_m { get; set; }
        public DateTime? date_r { get; set; }
        public decimal kol_or { get; set; }
        public decimal kol_onr { get; set; }
        public decimal kol_pog { get; set; }
        public string v_o { get; set; }
        public int kod_sez { get; set; }
        public string mg_kart { get; set; }
        public decimal kol_o { get; set; }
        public decimal proz_vipad { get; set; }
        public string tkanType { get; set; }
        public string kp { get; set; }
        public decimal tkanExpense { get; set; }
    }
        
    public class NastilGroupView
    {
        public string mg_kart { get; set; }
        public string kod_pr { get; set; }
        public string t_articul { get; set; }
        public decimal seb { get; set; }
        public string v_n { get; set; }
        public decimal kol { get; set; }
        public decimal kol_onr { get; set; }
        public decimal kol_pog { get; set; }
        public decimal sum_seb { get; set; }
        public decimal sum_rash { get; set; }
        public decimal sum_vet_m { get; set; }
    }
}
