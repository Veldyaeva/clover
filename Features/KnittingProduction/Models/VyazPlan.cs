using DevExpress.CodeParser;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.Spreadsheet.Export;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Utilities;
using SewingProduction.Features.Articul;
using SewingProduction.form;
using SewingProduction.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
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

    public class PZVZadanyList
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
        [NotMapped] public int yearPlan { get; set; }
        [NotMapped] public string kod { get; set; }
        [NotMapped] public int minNPach { get; set; }
        [NotMapped] public int maxNPach { get; set; }
        [NotMapped] public string brig { get; set; }
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
        public int pzvKwsID { get; set; }
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
        [NotMapped] public decimal olPzvChasNazn { get; set; }
        [NotMapped] public string olPzvVidPr { get; set; }
        [NotMapped] public int olPzvGradacia { get; set; }
        [NotMapped] public int olPzvGsID { get; set; }
        [NotMapped] public string olGsName { get; set; }
        [NotMapped] public int SyncSelection { get; set; } = 0;
        [NotMapped] public int ErrorSelection { get; set; } = 0;
        [NotMapped] public bool IsModified { get; set; } = false;
        [NotMapped] public bool IsNew { get; set; } = false;
        [NotMapped] public bool IsDeleted { get; set; } = false;
        [NotMapped] public int olKodPodr { get; set; }
        [NotMapped] public int olKodProizv { get; set; }
        [NotMapped] public int olPzvKwsID { get; set; }
        [NotMapped] public int olIdVyazClass { get; set; }
        [NotMapped] public int olDefect { get; set; }
        [NotMapped] public string olTextObS { get; set; }
        [NotMapped] public string olDolgn { get; set; }

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
            pzvKwsID = x.olPzvKwsID,
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
    
    public class SmenZadanyVyaz : INewable, IModifiable, IDeletable
    {
        [NotMapped] public string machNazn { get; set; }
        [NotMapped] public int kwsTabStart { get; set; }
        [NotMapped] public string fio { get; set; }
        [NotMapped] public int kwsID { get; set; }
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
        [NotMapped] public decimal chasNaznZadGroup { get; set; }
        [NotMapped] public decimal chasNotConfirmedZad { get; set; }
        [NotMapped] public decimal chasNotConfirmedZadGroup { get; set; }
        [NotMapped] public decimal chasRemainZad { get; set; }
        [NotMapped] public decimal chasRemainZadGroup { get; set; }
        [NotMapped] public decimal shiftsRemainZad { get; set; }
        [NotMapped] public decimal shiftsRemainZadGroup { get; set; }
        [NotMapped] public decimal chasNaznSmen { get; set; }
        [NotMapped] public decimal chasNaznSmenGroup { get; set; }
        [NotMapped] public decimal chasNaznSmenProc { get; set; }
        [NotMapped] public decimal chasNaznSmenProcGroup { get; set; }
        [NotMapped] public decimal chasInWorkSmen { get; set; }
        [NotMapped] public decimal chasInWorkSmenGroup { get; set; }
        [NotMapped] public decimal chasDoneSmen { get; set; }
        [NotMapped] public decimal chasDoneSmenGroup { get; set; }
        [NotMapped] public decimal chasDoneSmenProc { get; set; }
        [NotMapped] public decimal chasDoneSmenProcGroup { get; set; }
        [NotMapped] public decimal chasRemainSmen { get; set; }
        [NotMapped] public decimal chasRemainSmenGroup { get; set; }
        [NotMapped] public decimal chasConfirmedSmen { get; set; }
        [NotMapped] public decimal chasConfirmedSmenGroup { get; set; }
        [NotMapped] public decimal koefObServ { get; set; }
        [NotMapped] public int smenLength { get; set; }
        [NotMapped] public int kodOb { get; set; }
        [NotMapped] public int longRep { get; set; }
        [NotMapped] public bool IsModified { get; set; } = false;
        [NotMapped] public bool IsNew { get; set; } = false;
        [NotMapped] public bool IsDeleted { get; set; } = false;

        [NotMapped] public int szTab { get; set; }
        [NotMapped] public string szFio { get; set; }
        [NotMapped] public string szDolgn { get; set; }
        [NotMapped] public decimal szKoefVNV { get; set; }
        [NotMapped] public decimal szPlanHours { get; set; }
        [NotMapped] public decimal szNaznHours { get; set; }
        [NotMapped] public decimal szPlanNaznPercent { get; set; }
        [NotMapped] public decimal szHoursToDo { get; set; }
        [NotMapped] public decimal szHoursDone { get; set; }
        [NotMapped] public decimal szShiftVNV { get; set; }
        [NotMapped] public string szDTab { get; set; }
    }
    public class KnitWorkingShiftSmen
    {
        [NotMapped] public int kmaID { get; set; }
        [NotMapped] public int kwsID { get; set; }
        [NotMapped] public string kmaNumber { get; set; }
        [NotMapped] public DateTime? dateShiftStart { get; set; }
        [NotMapped] public DateTime? dateShiftEnd { get; set; }
        [NotMapped] public int tabShiftStart { get; set; }
        [NotMapped] public string fioShiftStart { get; set; }
        [NotMapped] public string fioShiftStartFull { get; set; }
        [NotMapped] public int diffHours { get; set; }
        [NotMapped] public int diffSeconds { get; set; }
        [NotMapped] public string shiftStatus { get; set; }
        [NotMapped] public int shiftStatusID { get; set; }
        [NotMapped] public int kmaNumberInt { get; set; }
    }
    public class NaryadZadanyVyaz : INewable, IModifiable, IDeletable
    {
        [NotMapped] public string kmlNumber { get; set; }
        [NotMapped] public string pzvArticul { get; set; }
        [NotMapped] public string pzvNomZad { get; set; }
        [NotMapped] public int pzvNom { get; set; }
        [NotMapped] public int nPach { get; set; }
        [NotMapped] public string pachKod { get; set; }
        [NotMapped] public int yearPach { get; set; }
        [NotMapped] public int n { get; set; }
        [NotMapped] public int n1 { get; set; }
        [NotMapped] public string nomOper => $"{n}/{n1}";
        [NotMapped] public string text { get; set; }
        [NotMapped] public int razryd { get; set; }
        [NotMapped] public decimal sekObServ { get; set; }
        [NotMapped] public decimal hoursTotalPlan { get; set; }
        [NotMapped] public decimal hoursTotalFact { get; set; }
        [NotMapped] public int pzvKol { get; set; }
        [NotMapped] public int statusID { get; set; }
        [NotMapped] public string statusName { get; set; }
        [NotMapped] public DateTime? statusDate { get; set; }
        [NotMapped] public int pzvTab { get; set; }
        [NotMapped] public string fioShiftStart { get; set; }
        [NotMapped] public bool IsModified { get; set; } = false;
        [NotMapped] public bool IsNew { get; set; } = false;
        [NotMapped] public bool IsDeleted { get; set; } = false;
    }

    public class PzvCheck
    {
        [NotMapped] public int tabGod { get; set; }
        [NotMapped] public int tabMes { get; set; }
        [NotMapped] public string tabMesName { get; set; }
        [NotMapped] public string tabBrig { get; set; }
        [NotMapped] public string tabFio { get; set; }
        [NotMapped] public string tabFioSokr { get; set; }
        [NotMapped] public int tabTab { get; set; }
        [NotMapped] public int tabSovm { get; set; }
        [NotMapped] public decimal tabChasi { get; set; }
        [NotMapped] public int grafChasi { get; set; }
        [NotMapped] public int itr { get; set; }
        [NotMapped] public decimal tabChasiAll { get; set; }
        [NotMapped] public decimal tabChasiOf { get; set; }
        [NotMapped] public decimal pztChasi { get; set; }
        [NotMapped] public decimal pztChasiOf { get; set; }
        [NotMapped] public int pztSek { get; set; }
        [NotMapped] public int pztSekOf { get; set; }
        [NotMapped] public decimal procent { get; set; }
        [NotMapped] public decimal procentOf { get; set; }
        [NotMapped] public decimal pzv01 { get; set; }
        [NotMapped] public decimal pzv02 { get; set; }
        [NotMapped] public decimal pzv03 { get; set; }
        [NotMapped] public decimal pzv04 { get; set; }
        [NotMapped] public decimal pzv05 { get; set; }
        [NotMapped] public decimal pzv06 { get; set; }
        [NotMapped] public decimal pzv07 { get; set; }
        [NotMapped] public decimal pzv08 { get; set; }
        [NotMapped] public decimal pzv09 { get; set; }
        [NotMapped] public decimal pzv10 { get; set; }
        [NotMapped] public decimal pzv11 { get; set; }
        [NotMapped] public decimal pzv12 { get; set; }
        [NotMapped] public decimal pzv13 { get; set; }
        [NotMapped] public decimal pzv14 { get; set; }
        [NotMapped] public decimal pzv15 { get; set; }
        [NotMapped] public decimal pzv16 { get; set; }
        [NotMapped] public decimal pzv17 { get; set; }
        [NotMapped] public decimal pzv18 { get; set; }
        [NotMapped] public decimal pzv19 { get; set; }
        [NotMapped] public decimal pzv20 { get; set; }
        [NotMapped] public decimal pzv21 { get; set; }
        [NotMapped] public decimal pzv22 { get; set; }
        [NotMapped] public decimal pzv23 { get; set; }
        [NotMapped] public decimal pzv24 { get; set; }
        [NotMapped] public decimal pzv25 { get; set; }
        [NotMapped] public decimal pzv26 { get; set; }
        [NotMapped] public decimal pzv27 { get; set; }
        [NotMapped] public decimal pzv28 { get; set; }
        [NotMapped] public decimal pzv29 { get; set; }
        [NotMapped] public decimal pzv30 { get; set; }
        [NotMapped] public decimal pzv31 { get; set; }
        [NotMapped] public string tab01 { get; set; }
        [NotMapped] public string tab02 { get; set; }
        [NotMapped] public string tab03 { get; set; }
        [NotMapped] public string tab04 { get; set; }
        [NotMapped] public string tab05 { get; set; }
        [NotMapped] public string tab06 { get; set; }
        [NotMapped] public string tab07 { get; set; }
        [NotMapped] public string tab08 { get; set; }
        [NotMapped] public string tab09 { get; set; }
        [NotMapped] public string tab10 { get; set; }
        [NotMapped] public string tab11 { get; set; }
        [NotMapped] public string tab12 { get; set; }
        [NotMapped] public string tab13 { get; set; }
        [NotMapped] public string tab14 { get; set; }
        [NotMapped] public string tab15 { get; set; }
        [NotMapped] public string tab16 { get; set; }
        [NotMapped] public string tab17 { get; set; }
        [NotMapped] public string tab18 { get; set; }
        [NotMapped] public string tab19 { get; set; }
        [NotMapped] public string tab20 { get; set; }
        [NotMapped] public string tab21 { get; set; }
        [NotMapped] public string tab22 { get; set; }
        [NotMapped] public string tab23 { get; set; }
        [NotMapped] public string tab24 { get; set; }
        [NotMapped] public string tab25 { get; set; }
        [NotMapped] public string tab26 { get; set; }
        [NotMapped] public string tab27 { get; set; }
        [NotMapped] public string tab28 { get; set; }
        [NotMapped] public string tab29 { get; set; }
        [NotMapped] public string tab30 { get; set; }
        [NotMapped] public string tab31 { get; set; }
        [NotMapped] public string grd01 { get; set; }
        [NotMapped] public string grd02 { get; set; }
        [NotMapped] public string grd03 { get; set; }
        [NotMapped] public string grd04 { get; set; }
        [NotMapped] public string grd05 { get; set; }
        [NotMapped] public string grd06 { get; set; }
        [NotMapped] public string grd07 { get; set; }
        [NotMapped] public string grd08 { get; set; }
        [NotMapped] public string grd09 { get; set; }
        [NotMapped] public string grd10 { get; set; }
        [NotMapped] public string grd11 { get; set; }
        [NotMapped] public string grd12 { get; set; }
        [NotMapped] public string grd13 { get; set; }
        [NotMapped] public string grd14 { get; set; }
        [NotMapped] public string grd15 { get; set; }
        [NotMapped] public string grd16 { get; set; }
        [NotMapped] public string grd17 { get; set; }
        [NotMapped] public string grd18 { get; set; }
        [NotMapped] public string grd19 { get; set; }
        [NotMapped] public string grd20 { get; set; }
        [NotMapped] public string grd21 { get; set; }
        [NotMapped] public string grd22 { get; set; }
        [NotMapped] public string grd23 { get; set; }
        [NotMapped] public string grd24 { get; set; }
        [NotMapped] public string grd25 { get; set; }
        [NotMapped] public string grd26 { get; set; }
        [NotMapped] public string grd27 { get; set; }
        [NotMapped] public string grd28 { get; set; }
        [NotMapped] public string grd29 { get; set; }
        [NotMapped] public string grd30 { get; set; }
        [NotMapped] public string grd31 { get; set; }
        [NotMapped] public string pzvTab01 { get; set; }
        [NotMapped] public string pzvTab02 { get; set; }
        [NotMapped] public string pzvTab03 { get; set; }
        [NotMapped] public string pzvTab04 { get; set; }
        [NotMapped] public string pzvTab05 { get; set; }
        [NotMapped] public string pzvTab06 { get; set; }
        [NotMapped] public string pzvTab07 { get; set; }
        [NotMapped] public string pzvTab08 { get; set; }
        [NotMapped] public string pzvTab09 { get; set; }
        [NotMapped] public string pzvTab10 { get; set; }
        [NotMapped] public string pzvTab11 { get; set; }
        [NotMapped] public string pzvTab12 { get; set; }
        [NotMapped] public string pzvTab13 { get; set; }
        [NotMapped] public string pzvTab14 { get; set; }
        [NotMapped] public string pzvTab15 { get; set; }
        [NotMapped] public string pzvTab16 { get; set; }
        [NotMapped] public string pzvTab17 { get; set; }
        [NotMapped] public string pzvTab18 { get; set; }
        [NotMapped] public string pzvTab19 { get; set; }
        [NotMapped] public string pzvTab20 { get; set; }
        [NotMapped] public string pzvTab21 { get; set; }
        [NotMapped] public string pzvTab22 { get; set; }
        [NotMapped] public string pzvTab23 { get; set; }
        [NotMapped] public string pzvTab24 { get; set; }
        [NotMapped] public string pzvTab25 { get; set; }
        [NotMapped] public string pzvTab26 { get; set; }
        [NotMapped] public string pzvTab27 { get; set; }
        [NotMapped] public string pzvTab28 { get; set; }
        [NotMapped] public string pzvTab29 { get; set; }
        [NotMapped] public string pzvTab30 { get; set; }
        [NotMapped] public string pzvTab31 { get; set; }
        [NotMapped] public int problChas { get; set; }
        [NotMapped] public string problChasStr { get; set; }
    }

    public class PlanTotalQuantityByArticul
    {
        [NotMapped] public string Kod { get; set; }
        [NotMapped] public string Articul { get; set; }
        [NotMapped] public int Kol { get; set; }
        [NotMapped] public DateTime? DataPlan { get; set; }
    }
    
}
