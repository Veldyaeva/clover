using Org.BouncyCastle.Asn1.X509;
using SewingProduction.Features.Articul;
using SewingProduction.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace SewingProduction.Features.KnittingProduction.Models
{
    public class VyazPlanView : INewable, IModifiable, IDeletable
    {
        [NotMapped]
        public string Nn { get; set; }
        [NotMapped]
        public string NomZad { get; set; }
        [NotMapped]
        public DateTime? DateZap { get; set; }
        [NotMapped]
        public int IDSbit { get; set; }
        [NotMapped]
        public string NameSbit { get; set; }
        [NotMapped]
        public DateTime? DateCdPlan { get; set; }
        [NotMapped]
        public string kod { get; set; }
        //[NotMapped]
        //public string KoddRt { get; set; }
        [NotMapped]
        public int annID { get; set; }
        [NotMapped]
        public string Articul { get; set; }
        [NotMapped]
        public string Grup { get; set; }
        [NotMapped]
        public int IDVyazClass { get; set; }
        [NotMapped]
        public int NameVyazClass { get; set; }
        [NotMapped]
        public int SekVyaz { get; set; }
        [NotMapped]
        public string ZvetTkan { get; set; }
        [NotMapped]
        public int Kol { get; set; }
        [NotMapped]
        public decimal SekVyazAll { get; set; }
        public int pszkmID { get; set; }
        public int KmlID { get; set; }
        [NotMapped]
        public string KmlNumber { get; set; }
        [NotMapped]
        public DateTime? DateZapPlanFrom { get; set; }
        [NotMapped]
        public DateTime? DateZapPlanTo { get; set; }
        [NotMapped]
        public string PictPath { get; set; }
        [NotMapped]
        public int SyncSelection { get; set; } = 0;
        [NotMapped]
        public bool IsModified { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
        [NotMapped]
        public bool IsDeleted { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [Table("plan_sezon_zad_knitMachine")]
    public class PlanSezonZadKnitMachineList : INewable, IModifiable, IDeletable
    {
        public int pszkmID { set; get; }
        public string pszkmPszNom { set; get; }
        public int pszkmKnitClass { set; get; }
        public int pszkmKmlID { get; set; }
        public int pszkmlSeconds { set; get; }
        public DateTime? pszkmPlanDateFrom { get; set; }
        public DateTime? pszkmPlanDateTo { get; set; }
        public DateTime? pszkmDateAdd { get; set; }
        [NotMapped]
        public bool IsModified { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
        [NotMapped]
        public bool IsDeleted { get; set; } = false;


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ArtPrKnitMachineView
    {
        public int kmlid { get; set; }
        public string kmlNumber { get; set; }
        public int availableHoursCurrMonth { get; set; }
        public int availableHoursNextMonth { get; set; }
        public int typevyazkm { get; set; }
        public string vidVyazKM { get; set; }
    }

    public class KnitMachineLoadAllInfo
    {
        [NotMapped]
        public string yearMonth { get; set; }
        [NotMapped]
        public string kmlNumber { get; set; }
        [NotMapped]
        public int kmlID { get; set; }
        [NotMapped]
        public string combinedPszNom { get; set; }
        [NotMapped]
        public int monthNumber { get; set; }
        [NotMapped]
        public int yearNumber { get; set; }
    }

    public class PlanSezonZadKnitMachine
    {
        [NotMapped]
        public int pszkmID { get; set; }
        [NotMapped]
        public string pszkmPszNom { get; set; }
        [NotMapped]
        public int pszkmKnitClass { get; set; }
        [NotMapped]
        public string kmlNumber { get; set; }
        [NotMapped]
        public string pszkmKmlID { get; set; }
        [NotMapped]
        public decimal pszkmlSeconds { get; set; }
        [NotMapped]
        public DateTime? pszkmPlanDateFrom { get; set; }
        [NotMapped]
        public DateTime? pszkmPlanDateTo { get; set; }
        [NotMapped]
        public int SecondsWorked { get; set; }
        [NotMapped]
        public decimal hoursTotal { get; set; }
        [NotMapped]
        public DateTime? pszkmDateAdd { get; set; }
        [NotMapped]
        public DateTime? DateZap { get; set; }
        [NotMapped]
        public string articul { get; set; }
        [NotMapped]
        public string yearMonthDateZap { get; set; }
        [NotMapped]
        public int monthNumberDateZap { get; set; }
        [NotMapped]
        public int yearNumberDateZap { get; set; }
        [NotMapped]
        public string yearMonthPlanDate { get; set; }
        [NotMapped]
        public int monthNumberPlanDate { get; set; }
        [NotMapped]
        public int yearNumberPlanDate { get; set; }
        [NotMapped]
        public int pszkmYearMonthInt { get; set; }
        [NotMapped]
        public int kmlKmaID { get; set; }
        [NotMapped]
        public string kmaNumber { get; set; }
    }

    public class PlanSezonZadKnitMachineLoadingSummary
    {
        [NotMapped]
        public string period { get; set; }
        [NotMapped]
        public int yearNumberDateZap { get; set; }
        [NotMapped]
        public int monthNumberDateZap { get; set; }
        [NotMapped]
        public decimal hoursTotal { get; set; }
        [NotMapped]
        public decimal sortOrder { get; set; }
    }

    public class PlanTotalHoursByKnitMachine
    {
        [NotMapped]
        public int kmaID { get; set; }
        [NotMapped]
        public string kmaNumber { get; set; }
        [NotMapped]
        public int kmlID { get; set; }
        [NotMapped]
        public string kmlNumber { get; set; }
        [NotMapped]
        public DateTime? DateZap { get; set; }
        [NotMapped]
        public string mgZap { get; set; }

        [NotMapped]
        public int hoursTotal { get; set; }

        [NotMapped]
        public int idVyazClass { get; set; }
        [NotMapped]
        public int knitClass { get; set; }
    }

    public class ZadanyListByMachine
    { 
        [NotMapped]
        public string pszNom { get; set; }
        [NotMapped]
        public int nom { get; set; }
        [NotMapped]
        public string articul { get; set; }
        [NotMapped]
        public string zvet { get; set; }
        [NotMapped]
        public int kol { get; set; }
        [NotMapped]
        public int pryazZayav { get; set; }
        [NotMapped]
        public DateTime? data_plan { get; set; }
        [NotMapped]
        public string vid_stir { get; set; }
        [NotMapped]
        public string dopr_name { get; set; }
        [NotMapped]
        public int kmlID { get; set; }
        [NotMapped]
        public int SyncSelection { get; set; } = 0;
    }
    public class RzvPachListByNom
    {
        [NotMapped]
        public string nomZad {  get; set; }
        [NotMapped]
        public int nom { get; set; }
        [NotMapped]
        public int nom_n { get; set; }
        [NotMapped]
        public int annID { get; set; }
        [NotMapped]
        public int n_pach { get; set; }
        [NotMapped]
        public string pach_kod { get; set; }
        [NotMapped]
        public string kod { get; set; }
        [NotMapped]
        public string razm { get; set; }
        [NotMapped]
        public int kol { get; set; }
        [NotMapped]
        public int grad { get; set; }
        [NotMapped]
        public int SyncSelection { get; set; } = 0;
    }

    public class PZVOperList : INewable, IModifiable, IDeletable
    {
        [NotMapped]
        public int olPzvID {  get; set; }
        [NotMapped]
        public int olPzvIDParent { get; set; }
        [NotMapped]
        public int olPzvIDMlOp { get; set; }
        [NotMapped]
        public int olNom { get; set; }
        [NotMapped]
        public int olNomN { get; set; }
        [NotMapped]
        public string olNomZad { get; set; }
        [NotMapped]
        public int olPzvAnnID { get; set; }
        [NotMapped]
        public int olPzvNrID { get; set; }
        [NotMapped]
        public int olPzvIdBrig { get; set; }
        [NotMapped]
        public int olPzvKmlID { get; set; }
        [NotMapped]
        public string olPzvArticul { get; set; }
        [NotMapped]
        public int olNPach { get; set; }
        [NotMapped]
        public string olNPachKod { get; set; }
        [NotMapped]
        public string olKod { get; set; }
        [NotMapped]
        public int olNo { get; set; }
        [NotMapped]
        public int olNpo { get; set; }
        [NotMapped]
        public string olOperName { get; set; }
        [NotMapped]
        public int olKodOb { get; set; }
        [NotMapped]
        public string olOborudClass { get; set; }
        [NotMapped]
        public int olRazryd { get; set; }
        [NotMapped]
        public int olSekEd { get; set; }
        [NotMapped]
        public int olKol { get; set; }
        [NotMapped]
        public int olSekAll { get; set; }
        [NotMapped]
        public string olKmlNumber { get; set; }
        [NotMapped]
        public DateTime? olPzvDateNaznKm { get; set; }
        [NotMapped]
        public int olPzvTab { get; set; }
        [NotMapped]
        public DateTime? olPzvDateNaznTab { get; set; }
        [NotMapped]
        public DateTime? olPzvDateStart { get; set; }
        [NotMapped]
        public DateTime? olPzvDateEnd { get; set; }
        [NotMapped]
        public DateTime? olPzvDateML { get; set; }
        [NotMapped]
        public DateTime? olPzvDateMast { get; set; }
        [NotMapped]
        public DateTime? olPzvUpdDate { get; set; }
        [NotMapped]
        public int SyncSelection { get; set; } = 0;
        [NotMapped]
        public bool IsModified { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
        [NotMapped]
        public bool IsDeleted { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class KnitPlanReportParametersList
    {
        [NotMapped]
        public int kmlIdVyazClass { get; set; }
        [NotMapped]
        public string name_class { get; set; }
        [NotMapped]
        public int pszkmKmlID { get; set; }
        [NotMapped]
        public string kmlInvNumber { get; set; }
        [NotMapped]
        public int yearMonthZapInt { get; set; }
        [NotMapped]
        public string yearMonthDateZap { get; set; }
        [NotMapped]
        public string articulKod { get; set; }
        [NotMapped]
        public string articul { get; set; }
    }
}
