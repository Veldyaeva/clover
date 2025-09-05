using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Data.Linq.Helpers;
using DevExpress.Xpo.Logger.Transport;
using DevExpress.XtraBars.Docking2010.Dragging;
using DevExpress.XtraReports.Templates;
using DevExpress.XtraRichEdit.Import.EPub;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.X509;
using SewingProduction.Features.Articul;
using SewingProduction.form;
using SewingProduction.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Security.Cryptography;

namespace SewingProduction.Features.CardByNom.Models
{
    public class SockZadanyInfo
    {
        
        [NotMapped]
        public int kzKmaID { get; set; }
        [NotMapped]
        public string kmaNumber { get; set; }
        [NotMapped]
        public int kzKmlID { get; set; }
        [NotMapped]
        public string kmlNumber { get; set; }
        [NotMapped]
        public string grup {  get; set; }
        [NotMapped]
        public string articul {  get; set; }
        [NotMapped]
        public int kol {  get; set; }
        [NotMapped]
        public int kolFakt { get; set; }
        [NotMapped]
        public int kolFactZadany { get; set; }
        [NotMapped]
        public int kolFactDelta { get; set; }
        [NotMapped]
        public int divider { get; set; }
        [NotMapped]
        public int KolIzdKompl { get; set; }
        [NotMapped]
        public string dtList { get; set; }
        [NotMapped]
        public string rList { get; set; }
        [NotMapped]
        public DateTime? dateStart { get; set; }
        [NotMapped]
        public DateTime? dateEnd { get; set; }
        [NotMapped]
        public string text_ob_s { get; set; }
        [NotMapped]
        public string kzPszNom { get; set; }
        [NotMapped]
        public string nom { get; set; }
        [NotMapped]
        public string zv_tkan { get; set; }
        [NotMapped]
        public string pach { get; set; }
        [NotMapped]
        public decimal kgDefects { get; set; }
        [NotMapped]
        public int kolDefects { get; set; }

    }

    public class SockZadanySmenList
    {
        [NotMapped]
        public int kzID { get; set; }
        [NotMapped]
        public DateTime? kzDateAdd { get; set; }
        [NotMapped]
        public string kzCompAdd { get; set; }
        [NotMapped]
        public int kzKwsID { get; set; }
        [NotMapped]
        public string kzPszNom { get; set; }
        [NotMapped]
        public int kzKmlID { get; set; }
        [NotMapped]
        public string kzKmaID { get; set; }
        [NotMapped]
        public DateTime? kzDateEnd { get; set; }
        [NotMapped]
        public int kzEnded { get; set; }
        [NotMapped]
        public string kmlNumber { get; set; }
        [NotMapped]
        public int kmlKodOb { get; set; }
        [NotMapped]
        public int kmlOdpID { get; set; }
        [NotMapped]
        public string kmaNumber { get; set; }
        [NotMapped]
        public int kwsTabStart { get; set; }
        [NotMapped]
        public int kwsTabEnd { get; set; }
        [NotMapped]
        public string fioStart { get; set; }
        [NotMapped]
        public string fioEnd { get; set; }

        [NotMapped]
        public string grup { get; set; }
        [NotMapped]
        public string articul { get; set; }
        [NotMapped]
        public decimal kol { get; set; }
        [NotMapped]
        public int kolFakt { get; set; }
        [NotMapped]
        public int divider { get; set; }
        [NotMapped]
        public int kolDefect { get; set; }
        [NotMapped]
        public decimal kgDefect { get; set; }
        [NotMapped]
        public string fioSt { get; set; }
        [NotMapped]
        public string fioEn { get; set; }
        [NotMapped]
        public int kwsKmsID { get; set; }
        [NotMapped]
        public decimal chasVyaz { get; set; }
        [NotMapped]
        public int DaysDiff { get; set; }
        [NotMapped]
        public TimeSpan TimeDiff { get; set; }
        public string DiffPeriod
        {
            get
            {
                if (DaysDiff > 0)
                {
                    return $"{DaysDiff} дн. {TimeDiff}";
                }
                else
                {
                    return $"{TimeDiff}"; // или return $"0 дн. {TimeDiff}"; если нужно всегда показывать 0
                }
            }
        }
    }

    public class SockServiceList
    {
        [NotMapped]
        public string kzPszNom { get; set; }

        [NotMapped]
        public string kmaNumber { get; set; }
        [NotMapped]
        public string kmlInvNum { get; set; }
        [NotMapped]
        public string kmlNumber { get; set; }
        [NotMapped]
        public string text_ob_s { get; set; }
        [NotMapped]
        public string directorName { get; set; }
        [NotMapped]
        public DateTime? date { get; set; }
        [NotMapped]
        public string resultName { get; set; }

        [NotMapped]
        public string ResultText { get; set; }
        [NotMapped]
        public DateTime? dateEnd { get; set; }
        [NotMapped]
        public string mechanic { get; set; }
        [NotMapped]
        public TimeSpan tmeDiffDHM { get; set; }

    }

    public class SockKnitMachiheService
    {
        [NotMapped]
        public string kzPszNom { get; set; }
        [NotMapped]
        public string kmaNumber { get; set; }
        [NotMapped]
        public string kmlInvNum { get; set; }
        [NotMapped]
        public  string kmlNumber { get; set; }
        [NotMapped]
        public string text_ob_s { get; set; }
        [NotMapped]
        public string directorName { get; set; }
        [NotMapped]
        public DateTime? date { get; set; }
        [NotMapped]
        public DateTime? resultName { get; set; }
        [NotMapped]
        public string ResultText { get; set; }
        [NotMapped]
        public DateTime? dateEnd { get; set; }
        [NotMapped]
        public string mechanic { get; set; }
        [NotMapped]
        public TimeSpan tmeDiffDHM { get; set; }
    }
}
