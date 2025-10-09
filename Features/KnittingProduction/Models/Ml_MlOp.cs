using SewingProduction.Features.Articul;
using SewingProduction.form;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SewingProduction.Features.KnittingProduction.Models
{
    public class MlOp
    {
        [NotMapped] public int id { get; set; }
        [NotMapped] public string mg { get; set; }
        [NotMapped] public int pach { get; set; }
        [NotMapped] public string kod { get; set; }
        [NotMapped] public string kod_o { get; set; }
        [NotMapped] public string kod_op { get; set; }
        [NotMapped] public string text { get; set; }
        [NotMapped] public int sek { get; set; }
        [NotMapped] public decimal seb { get; set; }
        [NotMapped] public int n { get; set; }
        [NotMapped] public int n1 { get; set; }
        public string OperationNumber => n1 > 0 ? $"{n}.{n1}" : $"{n}";
        [NotMapped] public int n_ch { get; set; }
        [NotMapped] public int kol { get; set; }
        [NotMapped] public int tab { get; set; }
        [NotMapped] public string fio { get; set; }
        [NotMapped] public int razryd { get; set; }
        [NotMapped] public string master { get; set; }
        [NotMapped] public int pach_max { get; set; }
        [NotMapped] public int pach_god { get; set; }
        [NotMapped] public DateTime? data_r { get; set; }
        [NotMapped] public string kodd { get; set; }
        [NotMapped] public string grup { get; set; }
        [NotMapped] public string articul { get; set; }
        [NotMapped] public string mod { get; set; }
        [NotMapped] public string brig { get; set; }
        [NotMapped] public string kod_ob { get; set; }
        [NotMapped] public string mg_nez { get; set; }
        [NotMapped] public string mg_pach { get; set; }
        [NotMapped] public string shtr2 { get; set; }
        [NotMapped] public int mashina { get; set; }
        [NotMapped] public int spec_ob { get; set; }
        [NotMapped] public string komp_polzv { get; set; }
        [NotMapped] public int mp_id { get; set; }
        [NotMapped] public int nomr { get; set; }
        [NotMapped] public string vidpr { get; set; }
        [NotMapped] public string shtr { get; set; }
        [NotMapped] public DateTime? date_add { get; set; }
        [NotMapped] public int fromPZT { get; set; }
        [NotMapped] public int annID { get; set; }
        [NotMapped] public int nrID { get; set; }
    }

    public class Ml
    {
        [NotMapped] public int id { get; set; }
        [NotMapped] public string mg { get; set; }
        [NotMapped] public string master { get; set; }
        [NotMapped] public int pach { get; set; }
        [NotMapped] public string kod { get; set; }
        [NotMapped] public string grup { get; set; }
        [NotMapped] public string articul { get; set; }
        [NotMapped] public string mod { get; set; }
        [NotMapped] public string razm { get; set; }
        [NotMapped] public string kod_o { get; set; }
        [NotMapped] public string po { get; set; }
        [NotMapped] public int kol { get; set; }
        [NotMapped] public string mg_pach { get; set; }
        [NotMapped] public string po_bu { get; set; }
        [NotMapped] public int pach_max { get; set; }
        [NotMapped] public int pach_god { get; set; }
        [NotMapped] public string brig { get; set; }
        [NotMapped] public string mg_nez { get; set; }
        [NotMapped] public string po_nez { get; set; }
        [NotMapped] public DateTime? d_kontr_ml { get; set; }
        [NotMapped] public int pr_got_nez { get; set; }
        [NotMapped] public int nomr { get; set; }
        [NotMapped] public string vidpr { get; set; }
        [NotMapped] public string userName { get; set; }
        [NotMapped] public string annID { get; set; }
    }
}
