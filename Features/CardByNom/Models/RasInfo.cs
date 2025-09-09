using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SewingProduction.Features.CardByNom.Models
{
    public class RasInfoByPachKod
    {
        public int RzuNom { get; set; }
        public DateTime? RzuDataR { get; set; }
        public int MinPach { get; set; }
        public int MaxPach { get; set; }
        public string RzuPach { get; set; }
        public int RzuKol { get; set; }
        public string PsaPrn { get; set; }
        public string RzuArticul { get; set; }
        public string RzuMod { get; set; }
        public string RzuDostZeh { get; set; }
        public int PsaIDSbit { get; set; }
        public string PsaNameSbit { get; set; }
        public string PsaNN { get; set; }
        public string PsaNomZad { get; set; }
        public string PsaTbID { get; set; }
        public string PsaMenName { get; set; }
        public string PsaYear { get; set; }
        public string PsaSez { get; set; }
        public string PsaSezName { get; set; }
        public string ArtGrup { get; set; }
        public string PsaKodZv1 { get; set; }
        public string PsaKodZv2 { get; set; }
        public DateTime? PsaDataZap { get; set; }
        public DateTime? PsaDataCdPlan { get; set; }
        public DateTime? RzuDataCdUt { get; set; }
        public DateTime? RzuDataZeh { get; set; }
        public DateTime? RzuDataRab { get; set; }
        public DateTime? RzuDataUp { get; set; }
        public DateTime? RzuDataCd { get; set; }
        public decimal PszPrintPlan { get; set; }
        public int RzuPrintFact { get; set; }
        public int PszVishPlan { get; set; }
        public int RzuVishFact { get; set; }
        public int PszStirPlan { get; set; }
        public int RzuStirFact { get; set; }
        public DateTime? RzuDataRasp { get; set; }
        public DateTime? RzuDataPrP { get; set; }
        public DateTime? RzuDataPrR { get; set; }
        public DateTime? RzuDataPrPe { get; set; }
        public DateTime? RzuDataPrKm { get; set; }
        public DateTime? RzuDataPrCd { get; set; }
        public DateTime? RzuDataRasv { get; set; }
        public DateTime? RzuDataVP { get; set; }
        public DateTime? RzuDataVR { get; set; }
        public DateTime? RzuDataVChi { get; set; }
        public DateTime? RzuDataVCd { get; set; }
        public DateTime? RzuDataStP { get; set; }
        public DateTime? RzuDataStR { get; set; }
        public DateTime? RzuDataStCd { get; set; }
        public string RzuVidStir { get; set; }
        [Column("pictPath")]
        public string PictPath { get; set; }
        [Column("sost")]
        public string Sost { get; set; }
        [Column("sostOtdelka")]
        public string SostOtdelka { get; set; }
        [Column("sostPodklad")]
        public string SostPodklad { get; set; }
        [Column("sostPoln")]
        public string SostPoln { get; set; }
        public DateTime? RzuData1C { get; set; }
        public int PsaPsaID { get; set; }
        public string PszRpcNom { get; set; }
        public int PsaPsaIDOsn { get; set; }
        public int PsaKombOsn { get; set; }
        public string PsaKombIzd { get; set; }
        public int PsaTkIdSet { get; set; }
        public string ArtTradeMark { get; set; }
    }
}
