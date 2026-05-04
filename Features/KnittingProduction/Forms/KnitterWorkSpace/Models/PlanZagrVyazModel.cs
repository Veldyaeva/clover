using DevExpress.CodeParser;
using DevExpress.XtraCharts;
//using Microsoft.Identity.Client;
using SewingProduction.Features.KnittingProduction.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models
{
    public class KnitterPZVModel
    {
        public int pzvID { get; set; }
        public int? pzvDivision { get; set; }
        public string pzvMod { get; set; }
        public string pzvArticul { get; set; }
        public int pzvKmlID { get; set; }
        public string kmlNumber { get; set; }
        public decimal? koefObServ { get; set; }
        public string name_class { get; set; }
        public string pzvNomZad { get; set; }
        public int? pzvAnnID { get; set; }
        public int? pzvNom { get; set; }
        public int? pzvKol { get; set; }
        public int pzvSek { get; set; }
        public int pzvSekNazn { get; set; }
        public DateTime? pzvDateNaznKm { get; set; }
        public DateTime? pzvDateNaznTab { get; set; }
        public DateTime? pzvDateStart { get; set; }
        public DateTime? pzvDateEnd { get; set; }
        public DateTime? pzvDateMast { get; set; }
        public int pzvKolNazn { get; set; }
        public decimal pzvChasNazn { get; set; }
        public decimal? pzvNChasi { get; set; } // факт. часы из БД
        public int? pzvTab { get; set; }
        public int? n_pach { get; set; }
        //количество в пачке (или в рассчёте, хз)
        public int? pzvRKol { get; set; }
        public string razm { get; set; }
        public int? nrN { get; set; }
        public int? nrN1 { get; set; }
        [NotMapped]
        public string DisplayNumber => nrN1 > 0 ? $"{nrN}.{nrN1}" : $"{nrN}";
        public string nrText { get; set; }
        public int? nrRazryd { get; set; }
        public string nrObor { get; set; }
        public int? nr_kod_ob { get; set; }
        public int? nr_kod_proizv { get; set; }
        //id открытой смены
        public int? pzvKwsID { get; set; }
        public BindingList<nrModel> nrModels { get; set; } = new();
        public BindingList<rzvModel> rzvModels { get; set; } = new();
        public DateTime? DataCd { get; set; } // data_cd -> DataCd (MatchNamesWithUnderscores = true)
        public int? PriorityGroup { get; set; }

        // Новые поля для плана/факта под UI-колонки
        public int? PlanKol_UI { get; set; }
        public decimal? PlanChas_UI { get; set; }
        public int? FactKol_UI { get; set; }
        public decimal? FactChas_UI { get; set; }
    }

    public class nrModel
    {
        // dapper сопоставит благодаря MatchNamesWithUnderscores = true
        public int nr_kod_proizv { get; set; }  // колонка: nrKodProizv
        public int nrN { get; set; }            // nrN
        public int nrN1 { get; set; }           // nrN1
        public int nrRazryd { get; set; }       // nrRazryd
        public string nrText { get; set; }      // nrText
        public string nrObor { get; set; }      // nrObor
        public int nr_kod_ob { get; set; }      // nrKodOb
        public string kmlNumber { get; set; }
    }

    public class rzvModel
    {
        public int n_pach { get; set; }     // n_pach
        public string pach_kod { get; set; }// pach_kod
        public string razm { get; set; }    // razm
        public int rzv_kod { get; set; }    // rzv_kod  (алиас в SQL)
        public int rzv_kol { get; set; }    // rzv_kol  (алиас в SQL)
    }
    public sealed class PzvSplitResult
    {
        public string Kind { get; set; }    // "Remainder", "Negative", "FinishedFact" и т.п.
        public int NewPzvId { get; set; }   // Id новой (или исходной) строки
    }

    public class knitMachineList
    {
        public int kmlId { get; set; }
        public int kmlKmAId { get; set; }
        public string kmlNumber { get; set; }
        public int kmlOdpId { get; set; }
        public int kmlOdpIdx { get; set; }
        public int kmlKodOb { get; set; }
        public int kmlLongRep { get; set; }
        public int kmlInvNom { get; set; }
        public int kmlPkuId { get; set; }
        public string kmaNumber { get; set; }
        [Column("kmlIdVyazClass")]
        public int idKnitClass { get; set; }
        public string name_class { get; set; }
        public decimal? koefObServ { get; set; }


    }
}
