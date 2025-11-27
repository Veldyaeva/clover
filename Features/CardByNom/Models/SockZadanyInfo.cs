using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SewingProduction.Features.CardByNom.Models
{
    public class SockZadanyInfo
    {

        [NotMapped]
        public int kzKmaID { get; set; }
        [NotMapped]
        public string kmaNumber { get; set; }
        [NotMapped]
        public int kzKmlID { get; set; }
        [NotMapped]
        public string kmlNumber { get; set; }
        [NotMapped]
        public string grup { get; set; }
        [NotMapped]
        public string articul { get; set; }
        [NotMapped]
        public int kol { get; set; }
        [NotMapped]
        public int kolFakt { get; set; }
        [NotMapped]
        public int kolFactZadany { get; set; }
        [NotMapped]
        public int kolFactDelta { get; set; }
        [NotMapped]
        public int divider { get; set; }
        [NotMapped]
        public int KolIzdKompl { get; set; }
        [NotMapped]
        public string dtList { get; set; }
        [NotMapped]
        public string rList { get; set; }
        [NotMapped]
        public DateTime? dateStart { get; set; }
        [NotMapped]
        public DateTime? dateEnd { get; set; }
        [NotMapped]
        public string text_ob_s { get; set; }
        [NotMapped]
        public string kzPszNom { get; set; }
        [NotMapped]
        public string nom { get; set; }
        [NotMapped]
        public string zv_tkan { get; set; }
        [NotMapped]
        public string pach { get; set; }
        [NotMapped]
        public decimal kgDefects { get; set; }
        [NotMapped]
        public int kolDefects { get; set; }
        [NotMapped]
        public string ko { get; set; }

    }

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
        public int kzEnded { get; set; }
        [NotMapped]
        public string kmlNumber { get; set; }
        [NotMapped]
        public string kmlInvNumber { get; set; }
        [NotMapped]
        public int kmlKodOb { get; set; }
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
        public string grup { get; set; }
        [NotMapped]
        public string articul { get; set; }
        [NotMapped]
        public decimal kol { get; set; }
        [NotMapped]
        public int kolFakt { get; set; }
        [NotMapped]
        public int divider { get; set; }
        [NotMapped]
        public int kolDefect { get; set; }
        [NotMapped]
        public decimal kgDefect { get; set; }
        [NotMapped]
        public string fioSt { get; set; }
        [NotMapped]
        public string fioEn { get; set; }
        [NotMapped]
        public int kwsKmsID { get; set; }
        [NotMapped]
        public decimal chasVyaz { get; set; }
        [NotMapped]
        public int DaysDiff { get; set; }
        [NotMapped]
        public string DaysDiffNeg { get; set; }
        [NotMapped]
        public TimeSpan TimeDiff { get; set; }
        [NotMapped]
        public string TimeDiffNeg { get; set; }
        public string DiffPeriod
        {
            get
            {
                if (DaysDiff > 0)
                {
                    return $"{DaysDiffNeg}{DaysDiff} дн. {TimeDiff}";
                }
                else
                {
                    return $"{TimeDiffNeg}{TimeDiff}"; // или return $"0 дн. {TimeDiff}"; если нужно всегда показывать 0
                }
            }
        }
    }

    public class SockServiceList
    {
        [NotMapped]
        public string kzPszNom { get; set; }

        [NotMapped]
        public string kmaNumber { get; set; }
        [NotMapped]
        public string kmlInvNum { get; set; }
        [NotMapped]
        public string kmlNumber { get; set; }
        [NotMapped]
        public string text_ob_s { get; set; }
        [NotMapped]
        public string directorName { get; set; }
        [NotMapped]
        public DateTime? date { get; set; }
        [NotMapped]
        public string resultName { get; set; }

        [NotMapped]
        public string ResultText { get; set; }
        [NotMapped]
        public DateTime? dateEnd { get; set; }
        [NotMapped]
        public string mechanic { get; set; }
        //[NotMapped]
        //public TimeSpan tmeDiffDHM { get; set; }
        [NotMapped]
        public int DaysDiff { get; set; }
        [NotMapped]
        public TimeSpan TimeDiff { get; set; }
        public string DiffPeriod
        {
            get
            {
                if (DaysDiff > 0)
                {
                    return $"{DaysDiff} дн. {TimeDiff}";
                }
                else
                {
                    return $"{TimeDiff}"; // или return $"0 дн. {TimeDiff}"; если нужно всегда показывать 0
                }
            }
        }

    }

    //public class SockKnitMachiheService
    //{
    //    [NotMapped]
    //    public string kzPszNom { get; set; }
    //    [NotMapped]
    //    public string kmaNumber { get; set; }
    //    [NotMapped]
    //    public string kmlInvNum { get; set; }
    //    [NotMapped]
    //    public  string kmlNumber { get; set; }
    //    [NotMapped]
    //    public string text_ob_s { get; set; }
    //    [NotMapped]
    //    public string directorName { get; set; }
    //    [NotMapped]
    //    public DateTime? date { get; set; }
    //    [NotMapped]
    //    public DateTime? resultName { get; set; }
    //    [NotMapped]
    //    public string ResultText { get; set; }
    //    [NotMapped]
    //    public DateTime? dateEnd { get; set; }
    //    [NotMapped]
    //    public string mechanic { get; set; }
    //    [NotMapped]
    //    public int DaysDiff { get; set; }
    //    [NotMapped]
    //    public TimeSpan TimeDiff { get; set; }
    //    public string DiffPeriod
    //    {
    //        get
    //        {
    //            if (DaysDiff > 0)
    //            {
    //                return $"{DaysDiff} дн. {TimeDiff}";
    //            }
    //            else
    //            {
    //                return $"{TimeDiff}"; // или return $"0 дн. {TimeDiff}"; если нужно всегда показывать 0
    //            }
    //        }
    //    }
    //}

    public class SockDownTimeList
    {
        [NotMapped]
        public string kzPszNom { get; set; }
        [NotMapped]
        public string kmaNumber { get; set; }
        [NotMapped]
        public string kmlInvNumber { get; set; }
        [NotMapped]
        public string kmlNumber { get; set; }
        [NotMapped]
        public int kmlID { get; set; }
        [NotMapped]
        public string text_ob_s { get; set; }
        [NotMapped]
        public DateTime? kdtlDateStart { get; set; }
        [NotMapped]
        public DateTime? kdtlDateEnd { get; set; }
        [NotMapped]
        public int DaysDiff { get; set; }
        [NotMapped]
        public TimeSpan TimeDiff { get; set; }
        public string DiffPeriod
        {
            get
            {
                if (DaysDiff > 0)
                {
                    return $"{DaysDiff} дн. {TimeDiff}";
                }
                else
                {
                    return $"{TimeDiff}"; // или return $"0 дн. {TimeDiff}"; если нужно всегда показывать 0
                }
            }
        }
    }

    public class SockDefectList
    {
        [NotMapped]
        public int vspdid { get; set; }
        [NotMapped]
        public string nom_zadany { get; set; }
        [NotMapped]
        public int isdefect { get; set; }
        [NotMapped]
        public decimal kg { get; set; }
        [NotMapped]
        public int kolAll { get; set; }
        [NotMapped]
        public int kolDefect { get; set; }
        [NotMapped]
        public int idspj { get; set; }
        [NotMapped]
        public int id_ndsp { get; set; }
        [NotMapped]
        public string namedefect { get; set; }
    }
}
