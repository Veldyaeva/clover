using SewingProduction.form;
using SewingProduction.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SewingProduction.Features.CardByNom.Models
{
    public class NaklViewByPachKod
    {
        public int Nom { get; set; }
        public string Iz { get; set; }
        public int IzNakl { get; set; }
        public DateTime? IzDate { get; set; }
        public DateTime? DateIzm { get; set; }
        public DateTime? DostDate { get; set; }
        public int DostN { get; set; }
        public DateTime? DatePrint { get; set; }
        public string GlNomer { get; set; }
        public string SklNaimen { get; set; }
        public string Prich { get; set; }
        public int CountBefore { get; set; }
        public int CountAfter { get; set; }
        public string Articul { get; set; }
        public string Mod { get; set; }
        public string ChipInUT { get; set; }
        public string ChipInUTForeColor { get; set; }
        public string ChipPech { get; set; }
        public string ChipPechForeColor { get; set; }
        public string ChipScan { get; set; }
        public string ChipScanForeColor { get; set; }
        public string ChipOtgr { get; set; }
        public string ChipOtgrForeColor { get; set; }

    }
}
