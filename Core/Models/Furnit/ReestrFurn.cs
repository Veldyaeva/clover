using DevExpress.Data.Linq.Helpers;
using DevExpress.DataAccess.Sql;
using SewingProduction.form;
using SewingProduction.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace SewingProduction.Models
{
    public class ReestrFurn
    {

        public int rfID { get; set; }
        public DateTime? rfDateTime { get; set; }
        public string rfKomp { get; set; }
        public int rfDdID { get; set; }
        public int rfDtID { get; set; }
        public string rfVidOtgr { get; set; }
        public string rfInfo { get; set; }
        public int rfKolM { get; set; }
        public int rfKolMAll { get; set; }
        public string rfKodF { get; set; }
        public int rfTipZ { get; set; }
        public string rfBrig { get; set; }
        public string rfGrup { get; set; }
        public string rfArticul { get; set; }
        public string rfMod { get; set; }
        public string rfPachList { get; set; }
        public int rfNOtgrPp { get; set; }
        public int rfRfdbID { get; set; }
        public int rfDdcID { get; set; }
        public int rfGrupSP { get; set; }
        public int rfVozvrat { get; set; }
        public int rfVidf { get; set; }
        public string rfNN_brig { get; set; }
        public string rfNN_from { get; set; }

    }
}
