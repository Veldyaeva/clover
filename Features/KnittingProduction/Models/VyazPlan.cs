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
        public bool SyncSelection { get; set; } = false;
        [NotMapped]
        public bool IsModified { get; set; } = false ;
        [NotMapped]
        public bool IsNew { get; set; } = false ;
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
        public int pszkmID {  set; get; }
        public string pszkmPszNom {  set; get; }
        public int pszkmKnitClass {  set; get; }
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

}
