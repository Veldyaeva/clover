using DevExpress.Spreadsheet.Export;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Utilities;
using SewingProduction.Features.Articul;
using SewingProduction.form;
using SewingProduction.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace SewingProduction.Features.KnittingProduction.Models
{
    public class VyazPlanView : INewable, IModifiable, IDeletable
    {
        [NotMapped] public string Nn { get; set; }
        [NotMapped] public string NomZad { get; set; }
        [NotMapped] public DateTime? DateZap { get; set; }
        [NotMapped] public int IDSbit { get; set; }
        [NotMapped] public string NameSbit { get; set; }
        [NotMapped] public DateTime? DateCdPlan { get; set; }
        [NotMapped] public string kod { get; set; }
        //[NotMapped]
        //public string KoddRt { get; set; }
        [NotMapped] public int annID { get; set; }
        [NotMapped] public string Articul { get; set; }
        [NotMapped] public string Grup { get; set; }
        [NotMapped] public int IDVyazClass { get; set; }
        [NotMapped] public int NameVyazClass { get; set; }
        [NotMapped] public int SekVyaz { get; set; }
        [NotMapped] public string ZvetTkan { get; set; }
        [NotMapped] public int Kol { get; set; }
        [NotMapped] public decimal SekVyazAll { get; set; }
        public int pszkmID { get; set; }
        [NotMapped] public string KmlNumber { get; set; }
        [NotMapped] public DateTime? DateZapPlanFrom { get; set; }
        [NotMapped] public DateTime? DateZapPlanTo { get; set; }
        [NotMapped] public string PictPath { get; set; }

        public int KmlID { get; set; }
        [NotMapped] public int Gradacia { get; set; }
        [NotMapped] public OriginalValues Original { get; private set; } = new OriginalValues();
        [NotMapped] public int KmlIDCopy => Original.KmlID;
        [NotMapped] public int GradaciaCopy => Original.Gradacia;
        [NotMapped] public bool IsKmlIDChanged => KmlID != Original.KmlID;
        [NotMapped] public bool IsGradaciaChanged => Gradacia != Original.Gradacia;

        [NotMapped] public string ProgrFio { get; set; }
        [NotMapped] public bool SyncSelection { get; set; } = false;
        [NotMapped] public bool IsModified { get; set; } = false;
        [NotMapped] public bool IsNew { get; set; } = false;
        [NotMapped] public bool IsDeleted { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void FixOriginalValues()
        {
            Original.Gradacia = this.Gradacia;
            Original.KmlID = this.KmlID;
        }
    }
    public class OriginalValues
    {
        public int Gradacia { get; set; }
        public int KmlID { get; set; }
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
        //public int pszkmGradacia { get; set; }
        [NotMapped] public bool IsModified { get; set; } = false;
        [NotMapped] public bool IsNew { get; set; } = false;
        [NotMapped] public bool IsDeleted { get; set; } = false;


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
        [NotMapped] public string yearMonth { get; set; }
        [NotMapped] public string kmlNumber { get; set; }
        [NotMapped] public int kmlID { get; set; }
        [NotMapped] public string combinedPszNom { get; set; }
        [NotMapped] public int monthNumber { get; set; }
        [NotMapped] public int yearNumber { get; set; }
    }

    public class PlanSezonZadKnitMachine
    {
        [NotMapped] public int pszkmID { get; set; }
        [NotMapped] public string pszkmPszNom { get; set; }
        [NotMapped] public int pszkmKnitClass { get; set; }
        [NotMapped] public string kmlNumber { get; set; }
        [NotMapped] public string pszkmKmlID { get; set; }
        [NotMapped] public decimal pszkmlSeconds { get; set; }
        [NotMapped] public DateTime? pszkmPlanDateFrom { get; set; }
        [NotMapped] public DateTime? pszkmPlanDateTo { get; set; }
        [NotMapped] public int SecondsWorked { get; set; }
        [NotMapped] public decimal hoursTotal { get; set; }
        [NotMapped] public DateTime? pszkmDateAdd { get; set; }
        [NotMapped] public DateTime? DateZap { get; set; }
        [NotMapped] public string articul { get; set; }
        [NotMapped] public string yearMonthDateZap { get; set; }
        [NotMapped] public int monthNumberDateZap { get; set; }
        [NotMapped] public int yearNumberDateZap { get; set; }
        [NotMapped] public string yearMonthPlanDate { get; set; }
        [NotMapped] public int monthNumberPlanDate { get; set; }
        [NotMapped] public int yearNumberPlanDate { get; set; }
        [NotMapped] public int pszkmYearMonthInt { get; set; }
        [NotMapped] public int kmlKmaID { get; set; }
        [NotMapped] public string kmaNumber { get; set; }
    }

    public class PlanSezonZadKnitMachineLoadingSummary
    {
        [NotMapped] public string period { get; set; }
        [NotMapped] public int yearNumberDateZap { get; set; }
        [NotMapped] public int monthNumberDateZap { get; set; }
        [NotMapped] public decimal hoursTotal { get; set; }
        [NotMapped] public decimal sortOrder { get; set; }
    }

    public class PlanTotalHoursByKnitMachine
    {
        [NotMapped] public int kmaID { get; set; }
        [NotMapped] public string kmaNumber { get; set; }
        [NotMapped] public int kmlID { get; set; }
        [NotMapped] public string kmlNumber { get; set; }
        [NotMapped] public DateTime? DateZap { get; set; }
        [NotMapped] public string mgZap { get; set; }
        [NotMapped] public int hoursTotal { get; set; }
        [NotMapped] public int idVyazClass { get; set; }
        [NotMapped] public int knitClass { get; set; }
    }

    public class ZadanyListByMachine
    {
        [NotMapped] public string pszNom { get; set; }
        [NotMapped] public int nom { get; set; }
        [NotMapped] public string articul { get; set; }
        [NotMapped] public string zvet { get; set; }
        [NotMapped] public int kol { get; set; }
        [NotMapped] public int pryazZayav { get; set; }
        [NotMapped] public DateTime? data_plan { get; set; }
        [NotMapped] public string vid_stir { get; set; }
        [NotMapped] public string dopr_name { get; set; }
        [NotMapped] public int kmlID { get; set; }
        [NotMapped] public int SyncSelection { get; set; } = 0;
        [NotMapped] public int Gradacia { get; set; }
    }
    public class RzvPachListByNom
    {
        [NotMapped] public string nomZad { get; set; }
        [NotMapped] public int nom { get; set; }
        [NotMapped] public int nom_n { get; set; }
        [NotMapped] public int annID { get; set; }
        [NotMapped] public int n_pach { get; set; }
        [NotMapped] public string pach_kod { get; set; }
        [NotMapped] public string kod { get; set; }
        [NotMapped] public string razm { get; set; }
        [NotMapped] public int kol { get; set; }
        public int gradacia { get; set; }
        [NotMapped] public int SyncSelection { get; set; } = 0;
    }
    public class PZV : INewable, IModifiable, IDeletable
    {
        public int pzvID { get; set; }
        public int pzvIDParent { get; set; }
        public int pzvDivision { get; set; }
        public int pzvIDMlOp { get; set; }
        public int pzvAnnID { get; set; }
        public int pzvNrID { get; set; }
        public string pzvNomZad { get; set; }
        public int pzvNom { get; set; }
        public int pzvNomN { get; set; }
        public string pzvArticul { get; set; }
        public string pzvMod { get; set; }
        public int pzvIdBrig { get; set; }
        public int pzvSek { get; set; }
        public int pzvKol { get; set; }
        public decimal pzvNChasi { get; set; }
        public int pzvKmlID { get; set; }
        public DateTime? pzvDateNaznKm { get; set; }
        public int pzvTab { get; set; }
        public DateTime? pzvDateNaznTab { get; set; }
        public DateTime? pzvDateStart { get; set; }
        public DateTime? pzvDateEnd { get; set; }
        public DateTime? pzvDateML { get; set; }
        public DateTime? pzvDateMLUt { get; set; }
        public DateTime? pzvDateMast { get; set; }
        public int pzvRKol { get; set; }
        public int pzvSekNazn { get; set; }
        public decimal pzvChasNazn { get; set; }
        public int pzvKolNazn { get; set; }
        public string pzvVidPr { get; set; }
        [NotMapped] public string pzvCompAdd { get; set; }
        [NotMapped] public DateTime? pzvDateAdd { get; set; }
        [NotMapped] public DateTime? pzvUpdDate { get; set; }
        public int pzvGradacia { get; set; }
        public int pzvGsID { get; set; }
        [NotMapped] public bool IsModified { get; set; } = false;
        [NotMapped] public bool IsNew { get; set; } = false;
        [NotMapped] public bool IsDeleted { get; set; } = false;
    }
    public class PZVOperList : INewable, IModifiable, IDeletable
    {
        [NotMapped] public int olPzvID { get; set; }
        [NotMapped] public int olPzvIDParent { get; set; }
        [NotMapped] public int olPzvDivision { get; set; }
        [NotMapped] public int olPzvIDMlOp { get; set; }
        [NotMapped] public int olNom { get; set; }
        [NotMapped] public int olNomN { get; set; }
        [NotMapped] public string olNomZad { get; set; }
        [NotMapped] public int olPzvAnnID { get; set; }
        [NotMapped] public int olPzvNrID { get; set; }
        [NotMapped] public int olPzvIdBrig { get; set; }
        [NotMapped] public int olPzvKmlID { get; set; }
        [NotMapped] public string olPzvArticul { get; set; }
        [NotMapped] public string olPzvMod { get; set; }
        [NotMapped] public int olNPach { get; set; }
        [NotMapped] public string olPachKod { get; set; }
        [NotMapped] public string olKod { get; set; }
        [NotMapped] public int olNo { get; set; }
        [NotMapped] public int olNpo { get; set; }
        [NotMapped] public string olNomOper => $"{olNo}/{olNpo}";
        [NotMapped] public string olOperName { get; set; }
        [NotMapped] public int olKodOb { get; set; }
        [NotMapped] public string olOborudClass { get; set; }
        [NotMapped] public int olRazryd { get; set; }
        [NotMapped] public int olSekEd { get; set; }
        [NotMapped] private int _olKol;
        [NotMapped] private bool _olKolCopyInitialized;
        [NotMapped] public int olKol
        {
                get => _olKol;
                set
                {
                    if (!_olKolCopyInitialized)
                    {
                        olKolCopy = value;          // зафиксировали «исходное» значение
                        _olKolCopyInitialized = true;
                    }
                    _olKol = value;
                }
            }
        [NotMapped] public int olKolCopy { get; set; }
        [NotMapped] public int olPzvRKol { get; set; }
        [NotMapped] public decimal olPzvNChasi { get; set; }
        [NotMapped] public string olKmlNumber { get; set; }
        [NotMapped] public DateTime? olPzvDateNaznKm { get; set; }
        [NotMapped] public int olPzvTab { get; set; }
        [NotMapped] public DateTime? olPzvDateNaznTab { get; set; }
        [NotMapped] public DateTime? olPzvDateStart { get; set; }
        [NotMapped] public DateTime? olPzvDateEnd { get; set; }
        [NotMapped] public DateTime? olPzvDateML { get; set; }
        [NotMapped] public DateTime? olPzvDateMLUt { get; set; }
        [NotMapped] public DateTime? olPzvDateMast { get; set; }
        [NotMapped] public DateTime? olPzvUpdDate { get; set; }
        [NotMapped] public int olPzvSekNazn { get; set; }
        [NotMapped] public int olPzvKolNazn { get; set; }
        [NotMapped] public int olPzvChasNazn { get; set; }
        [NotMapped] public string olPzvVidPr { get; set; }
        [NotMapped] public int olPzvGradacia { get; set; }
        [NotMapped] public int olPzvGsID { get; set; }
        [NotMapped] public int olGsName { get; set; }
        [NotMapped] public int SyncSelection { get; set; } = 0;
        [NotMapped] public int ErrorSelection { get; set; } = 0;
        [NotMapped] public bool IsModified { get; set; } = false;
        [NotMapped] public bool IsNew { get; set; } = false;
        [NotMapped] public bool IsDeleted { get; set; } = false;
        [NotMapped] public int olKodPodr { get; set; }
        [NotMapped] public int olKodProizv { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// При необходимости можно «перебазировать» копию вручную.
        /// </summary>
        public void RebaselineOlKolCopy()
        {
            olKolCopy = _olKol;
            _olKolCopyInitialized = true;
        }

        /// <summary>
        /// Сброс копии (если хочешь, чтобы следующее присвоение olKol снова зафиксировало копию).
        /// </summary>
        public void ResetOlKolCopy()
        {
            _olKolCopyInitialized = false;
        }
    }
    public static class PZVOperListMappingExtensions
    {
        public static PZV ToPZV(this PZVOperList x) => new PZV
        {
            pzvID = x.olPzvID, 
            pzvIDParent = x.olPzvIDParent, 
            pzvDivision = x.olPzvDivision, 
            pzvIDMlOp = x.olPzvIDMlOp, 
            pzvAnnID = x.olPzvAnnID, 
            pzvNrID = x.olPzvNrID, 
            pzvNomZad = x.olNomZad, 
            pzvNom = x.olNom, 
            pzvNomN = x.olNomN, 
            pzvArticul = x.olPzvArticul, 
            pzvMod = x.olPzvMod, 
            pzvIdBrig = x.olPzvIdBrig, 
            pzvSek = x.olSekEd, 
            pzvKol = x.olKol, 
            pzvNChasi = x.olPzvNChasi, 
            pzvKmlID = x.olPzvKmlID, 
            pzvDateNaznKm = x.olPzvDateNaznKm, 
            pzvTab = x.olPzvTab, 
            pzvDateNaznTab = x.olPzvDateNaznTab, 
            pzvDateStart = x.olPzvDateStart, 
            pzvDateEnd = x.olPzvDateEnd, 
            pzvDateML = x.olPzvDateML, 
            pzvDateMLUt = x.olPzvDateMLUt, 
            pzvDateMast = x.olPzvDateMast, 
            pzvRKol = x.olPzvRKol, 
            pzvSekNazn = x.olPzvSekNazn, 
            pzvChasNazn = x.olPzvChasNazn, 
            pzvKolNazn = x.olPzvKolNazn, 
            pzvVidPr = x.olPzvVidPr, 
            pzvUpdDate = x.olPzvUpdDate,
            pzvGradacia = x.olPzvGradacia,
            pzvGsID = x.olPzvGsID,
            IsModified = x.IsModified,
            IsNew = x.IsNew,
            IsDeleted = x.IsDeleted
        };
    }

    public class KnitPlanReportParametersList
    {
        [NotMapped] public int kmlIdVyazClass { get; set; }
        [NotMapped] public string name_class { get; set; }
        [NotMapped] public int pszkmKmlID { get; set; }
        [NotMapped] public string kmlInvNumber { get; set; }
        [NotMapped] public int yearMonthZapInt { get; set; }
        [NotMapped] public string yearMonthDateZap { get; set; }
        [NotMapped] public string articulKod { get; set; }
        [NotMapped] public string articul { get; set; }
    }
    public class SmenZadanyVyazMachine
    {
        [NotMapped] public int kmlKmaID { get; set; }
        [NotMapped] public string kmaNumber { get; set; }
        [NotMapped] public int kmlID { get; set; }
        [NotMapped] public string kmlNumber { get; set; }
        [NotMapped] public string kmlInvNum { get; set; }
        [NotMapped] public int kmlIdVyazClass { get; set; }
        [NotMapped] public int name_class { get; set; }
        [NotMapped] public decimal taskToDo { get; set; }
        [NotMapped] public decimal taskAtWork { get; set; }
        [NotMapped] public decimal taskDone { get; set; }
        [NotMapped] public decimal taskNotConfirmed { get; set; }
        [NotMapped] public decimal taskConfirmed { get; set; }
        [NotMapped] public decimal taskNotPlanned { get; set; }
    }
    public class SmenZadanyVyazEmp
    {
        [NotMapped] public int kmaID { get; set; }
        [NotMapped] public string kmaNumber { get; set; }
        [NotMapped] public int empTab { get; set; }
        [NotMapped] public string empFioSokr { get; set; }
        [NotMapped] public decimal taskToDo { get; set; }
        [NotMapped] public decimal taskAtWork { get; set; }
        [NotMapped] public decimal taskDone { get; set; }
        [NotMapped] public decimal taskNotConfirmed { get; set; }
        [NotMapped] public decimal taskConfirmed { get; set; }
        [NotMapped] public int kmaIDNazn { get; set; }
    }
    
    public class SmenZadanyVyaz
    {
        [NotMapped] public string machNazn { get; set; }
        [NotMapped] public int kwsTabStart { get; set; }
        [NotMapped] public string fio { get; set; }
        [NotMapped] public string kwsID { get; set; }
        [NotMapped] public int kwsKmaID { get; set; }
        [NotMapped] public string kmaNumber { get; set; }
        [NotMapped] public int kwsmlKmlID { get; set; }
        [NotMapped] public string kmlNumber { get; set; }
        [NotMapped] public int idVyazClass { get; set; }
        [NotMapped] public int nameVyazClass { get; set; }
        [NotMapped] public int kmaNumberInt { get; set; }
        [NotMapped] public int kmlNumberInt { get; set; }
        [NotMapped] public int typeID { get; set; }
        [NotMapped] public string typeName { get; set; }
        [NotMapped] public decimal chasNaznZad { get; set; }
        [NotMapped] public decimal chasNotConfirmedZad { get; set; }
        [NotMapped] public decimal chasRemainZad { get; set; }
        [NotMapped] public decimal shiftsRemainZad { get; set; }
        [NotMapped] public decimal chasNaznSmen { get; set; }
        [NotMapped] public decimal chasNaznSmenProc { get; set; }
        [NotMapped] public decimal chasInWorkSmen { get; set; }
        [NotMapped] public decimal chasDoneSmen { get; set; }
        [NotMapped] public decimal chasDoneSmenProc { get; set; }
        [NotMapped] public decimal chasRemainSmen { get; set; }
        [NotMapped] public decimal chasConfirmedSmen { get; set; }
        [NotMapped] public decimal koefObServ { get; set; }
        [NotMapped] public int smenLength { get; set; }
    }
}
