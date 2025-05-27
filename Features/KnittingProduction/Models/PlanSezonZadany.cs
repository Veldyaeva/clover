using SewingProduction.form;
using SewingProduction.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SewingProduction.Features.KnittingProduction.Models
{
    public class PlanSezonZadanyView
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string nn { get; set; }
        public DateTime? data_za { get; set; }
        public string kod { get; set; }
        public string grup { get; set; }
        public string articul { get; set; }
        public string mod { get; set; }
        public string razm { get; set; }
        public decimal norm_t { get; set; }
        public int sek { get; set; }
        public string kle { get; set; }
        public int grupp { get; set; }
        public string tkb { get; set; }
        public string tkb1 { get; set; }
        public decimal norm_t1 { get; set; }
        public string tkb2 { get; set; }
        public decimal norm_t2 { get; set; }
        public string tkb3 { get; set; }
        public decimal norm_t3 { get; set; }
        public string tkb4 { get; set; }
        public decimal norm_t4 { get; set; }
        public string tkb5 { get; set; }
        public decimal norm_t5 { get; set; }
        public string tkb6 { get; set; }
        public decimal norm_t6 { get; set; }
        public int baza { get; set; }
        public int kod_v { get; set; }
        public string kodd { get; set; }
        public int kol { get; set; }
        public int kol_sapr { get; set; }
        public int kol_s { get; set; }

    }
}
