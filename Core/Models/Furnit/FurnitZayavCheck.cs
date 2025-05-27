using DevExpress.DataAccess.Sql;
using SewingProduction.form;
using SewingProduction.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace SewingProduction.Models
{
    public class FurnitZayavCheckByPachKod
    {

        public string pach_kod { get; set; }
        public string nn { get; set; }
        public string zad_pl { get; set; }
        public DateTime? planCd { get; set; }
        public string dost_zeh { get; set; }
        public string type_sozd { get; set; }
        public DateTime? data_zap { get; set; }
        public string prn { get; set; }
        public string furnKKStatus { get; set; }
        public int is_furnit { get; set; }
        public string furnKKStat { get; set; }
        public string furnStatus { get; set; }
        public string fZSozdStat { get; set; }
        public DateTime? data_f_o { get; set; }
        public string fZSobrStat { get; set; }
        public DateTime? data_f_z { get; set; }
        public int furnZayav { get; set; }
        public int is_upak { get; set; }
        public string upakKKStatus { get; set; }
        public string upakKKStat { get; set; }
        public string upakStatus { get; set; }
        public string uZSozdStat { get; set; }
        public DateTime? data_f_o_u { get; set; }
        public string uZSobrStat { get; set; }
        public DateTime? data_f_z_u { get; set; }
        public int upakZayav { get; set; }
        public string brigStatus { get; set; }
        public string brigStat { get; set; }
        public DateTime? datZayav { get; set; }
        public string otgrStatus { get; set; }
        public string otgrStat { get; set; }
        public DateTime? data_cd { get; set; }
        public string is_got { get; set; }
        public DateTime? data_zeh { get; set; }
        public DateTime? data_zap1 { get; set; }
        public string prn1 { get; set; }
        public string kod_zv { get; set; }
        public string kod1 { get; set; }
        public string kod2 { get; set; }
        public string FKodFD { get; set; }
        public string UKodFD { get; set; }
        public int FSpecRez { get; set; }
        public int USpecRez { get; set; }

    }
}
