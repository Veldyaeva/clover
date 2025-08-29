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
using System.Security.Cryptography;

namespace SewingProduction.Features.KnittingProduction.Models
{
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
        public int kzEnded {  get; set; }
        [NotMapped]
        public string kmlNumber { get; set; }
        [NotMapped]
        public int kmlKodOb {  get; set; }
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
        public string grup {  get; set; }
        [NotMapped]
        public string articul { get; set; }
        [NotMapped]
        public decimal kol {  get; set; }
        [NotMapped]
        public int kolFakt { get; set; }
        [NotMapped]
        public int divider { get; set; }
        [NotMapped]
        public int kolDefect { get; set; }
        [NotMapped]
        public decimal kgDefect { get; set; }
        [NotMapped]
        public string fioSt {  get; set; }
        [NotMapped]
        public string fioEn { get; set; }
        [NotMapped]
        public int kwsKmsID { get; set; }
        [NotMapped]
        public decimal chasVyaz { get; set; }
        [NotMapped]
        public int DaysDiff { get; set; }
        [NotMapped]
        public Time? TimeDiff { get; set; }
    }
}
