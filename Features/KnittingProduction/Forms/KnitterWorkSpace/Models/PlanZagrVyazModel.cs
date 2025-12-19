using DevExpress.CodeParser;
using DevExpress.XtraCharts;
using Microsoft.Identity.Client;
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
        public string pzvNomZad { get; set; }
        public int? pzvAnnID { get; set; }
        public int? pzvNom { get; set; }
        public int? pzvKol { get; set; }
        public int pzvSek { get; set; }
        public DateTime? pzvDateStart { get; set; }
        public DateTime? pzvDateEnd { get; set; }
        public int pzvKolNazn { get; set; }
        public decimal pzvChasNazn { get; set; }
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
        [NotMapped]
        public int kol_Effective { get; set; }
        [NotMapped]
        public int sekEd_Effective { get; set; }
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
//        kml.*, 
//            kma.kmaNumber
//		, cast(ltrim(rtrim(odp.odpDevName)) + ' // ' + ltrim(rtrim(odp.odpCategoryName)) + ' // ' + ltrim(rtrim(odp.odpName)) as nvarchar(250)) as paramName
//--		, cast(REPLACE(odp.odpCategoryName, 'Вход ', '') as int) inputIndex
//		, os.text_ob_s
//		, odp.odpCode
//    , kma.kmaIDNazn
//    , kma.nazn AS machNazn
//    , kma.kmaZdID
//    , kma.object AS machObject
//    , 'зона ' + trim(cast(ISNULL(kmaNumber, 0) as nvarchar)) + ' - ' + 'авт.№ ' + trim(cast(ISNULL(kmlNumber, 0) as nvarchar)) + ' - ' + trim(cast(ISNULL(kmlInvNum, '') as nvarchar)) + ' - ' + trim(cast(ISNULL(text_ob_s, '') as nvarchar)) as oborFullNaimen
//    , os.id_class AS kmlIdVyazClass
//    ,mc.name_class
//    , CAST(dbo.getNumbersOnly(kml.kmlNumber) AS INT) AS kmlNumberInt
//    , CAST(dbo.getNumbersOnly(kma.kmaNumber) AS INT) AS kmaNumberInt
//    , mc.koefObServ
    }
    /*
    public class PlanZagrVyazOper
    {
        public int? olPzvID { get; set; }
        public string pzvArticul { get; set; }
        public string pzvNomZad { get; set; }
        public int? pzvNom { get; set; }
        public int? olNomN { get; set; }
        public string olPachKod { get; set; }
        public string olKod { get; set; }
        public string olOperName { get; set; }
        public string olOborudClass { get; set; }
        public decimal? olSekAll { get; set; }
        public DateTime? olPzvDateStart { get; set; }
        public DateTime? olPzvDateEnd { get; set; }
        public string pzvMod { get; set; }
        public int pzvIdBrig { get; set; }
        public int pzvSek  {get; set;}
        public int pzvKol {get; set;}
        public decimal pzvNChasi {get; set;}
        public int pzvKmlID {get; set;}
        public DateTime? pzvDateNaznKm {get; set;}
        public int pzvTab {get; set;}
        public DateTime? pzvDateNaznTab {get; set;}
        public DateTime? pzvDateStart {get; set;}
        public DateTime? pzvDateEnd {get; set;}
        public DateTime? pzvDateML {get; set;}
        public DateTime? pzvDateMLUt {get; set;}
        public DateTime? pzvDateMast {get; set;}
        public int pzvRKol {get; set;}
        public int pzvSekNazn {get; set;}
        public decimal pzvChasNazn {get; set;}
        public int pzvKolNazn {get; set;}
        public string pzvVidPr {get; set;}
        public string pzvCompAdd {get; set;}
        public DateTime? pzvDateAdd {get; set;}
        public DateTime? pzvUpdDate { get; set; }

        public int nrID {get; set;}
        public int kod {get; set;}
        public int kod_o {get; set;}
        public int kod_podr {get; set;}
        public int kod_proizv {get; set;}
        public string nrText {get; set;}
        public int sek  {get; set;}
        public decimal seb  {get; set;}
        public int n {get; set;}
        public int n_ch  {get; set;}
        public int n1 {get; set;}
        public decimal seb_s  {get; set;}
        public int razryd {get; set;}
        public string spec {get; set;}
        public string obor {get; set;}
        public int sek12  {get; set;}
        public int sek7 {get; set;}
        public int sek5 {get; set;}
        public int kod_ob  {get; set;}
        public int sql_pr_add {get; set;}
        public DateTime? date_add {get; set;}
        public int komp_name {get; set;}
        public DateTime? nrDateDel {get; set;}
        public int nrCompDel {get; set;}
        public DateTime? nrDateAdd {get; set;}
        public int nrCompAdd { get; set; }
        public int annId { get; set; }

    }
    */

}