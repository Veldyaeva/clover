using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        [Display(Name ="Id вязальной машины")]
        public int pzvKmlID { get; set; }
        [Display(Name ="Номер вязальной машины")]
        public string kmlNumber { get; set; }

        [Display(Name = "Номер задания")]//, Order = 12, GroupName = "Номенклатура")]
        [StringLength(10)]
        public string pzvNomZad { get; set;}
        
        [Display(Name = "annID", Order = 11, GroupName = "Номенклатура")]
        public int? pzvAnnID { get; set; }
        
        [Display(Name = "Номер рассчёта")]//, Order = 13, GroupName = "Номенклатура")]
        public int? pzvNom { get; set; }

        public int? pzvKol { get; set; }
        public int pzvSek { get; set; }
        public DateTime? pzvDateStart { get; set; }
        public DateTime? pzvDateEnd { get; set; }
        public int pzvKolNazn { get; set; }

        // Поля для детального грида (из vwPlanZagrVyazNorm_ByTab)
        public int nrN { get; set; }
        public int nrN1 { get; set; }
        public string nrText { get; set; }
        public int nrRazryd { get; set; }
        public string nrObor { get; set; }
        [Display(Name ="Номер пачки")]
        public int n_pach { get; set; }

    }

    //public class PlanZagrVyaz
    //    {
    //        // === Основное ===
    //        [Key]
    //        [Display(Name = "ID", Order = 0, GroupName = "Основное")]
    //        [ReadOnly(true)]
    //        public int pzvID { get; set; }

    //        [Display(Name = "ID родителя", Order = 1, GroupName = "Основное")]
    //        public int? pzvIDParent { get; set; }

    //        [Display(Name = "Подразделение", Order = 2, GroupName = "Основное")]
    //        [Range(1, int.MaxValue, ErrorMessage = "Укажите код подразделения")]
    //        public int? pzvDivision { get; set; }

    //        [Display(Name = "ID м/оп", Order = 3, GroupName = "Основное")]
    //        public int? pzvIDMlOp { get; set; }

    //        // === Номенклатура ===
    //        [Display(Name = "annID", Order = 10, GroupName = "Номенклатура")]
    //        public int? pzvAnnID { get; set; }

    //        [Display(Name = "nrID (норма)", Order = 11, GroupName = "Номенклатура")]
    //        public int? pzvNrID { get; set; }

    //        [Display(Name = "Номер задания", Order = 12, GroupName = "Номенклатура")]
    //        [StringLength(10)]
    //        public string? pzvNomZad { get; set; }

    //        [Display(Name = "Номенклатура (Nom)", Order = 13, GroupName = "Номенклатура")]
    //        public int? pzvNom { get; set; }

    //        [Display(Name = "Номенклатура (NomN)", Order = 14, GroupName = "Номенклатура")]
    //        public int? pzvNomN { get; set; }

    //        [Display(Name = "Артикул", Order = 15, GroupName = "Номенклатура")]
    //        [StringLength(25)]
    //        public string? pzvArticul { get; set; }

    //        [Display(Name = "Модель", Order = 16, GroupName = "Номенклатура")]
    //        [StringLength(25)]
    //        public string? pzvMod { get; set; }

    //        [Display(Name = "Бригада", Order = 17, GroupName = "Номенклатура")]
    //        public int? pzvIdBrig { get; set; }

    //        // === Нормы и объём ===
    //        [Display(Name = "Секунд на изделие", Order = 20, GroupName = "Нормы и объём")]
    //        public int? pzvSek { get; set; }

    //        [Display(Name = "Количество", Order = 21, GroupName = "Нормы и объём")]
    //        public int? pzvKol { get; set; }

    //        [Display(Name = "Часы (итого)", Order = 22, GroupName = "Нормы и объём")]
    //        [DataType(DataType.Currency)]
    //        public decimal? pzvNChasi { get; set; }

    //        // === Машина и назначения ===
    //        [Display(Name = "Машина (KmlID)", Order = 30, GroupName = "Назначения")]
    //        public int? pzvKmlID { get; set; }

    //        [Display(Name = "Назначено (МЛ)", Order = 31, GroupName = "Назначения")]
    //        [DataType(DataType.DateTime)]
    //        public DateTime? pzvDateNaznKm { get; set; }

    //        [Display(Name = "Таб. №", Order = 32, GroupName = "Назначения")]
    //        public int? pzvTab { get; set; }

    //        [Display(Name = "Назначено (табель)", Order = 33, GroupName = "Назначения")]
    //        [DataType(DataType.DateTime)]
    //        public DateTime? pzvDateNaznTab { get; set; }

    //        [Display(Name = "Дата начала", Order = 34, GroupName = "Назначения")]
    //        [DataType(DataType.DateTime)]
    //        public DateTime? pzvDateStart { get; set; }

    //        [Display(Name = "Дата окончания", Order = 35, GroupName = "Назначения")]
    //        [DataType(DataType.DateTime)]
    //        public DateTime? pzvDateEnd { get; set; }

    //        [Display(Name = "Дата МЛ", Order = 36, GroupName = "Назначения")]
    //        [DataType(DataType.DateTime)]
    //        public DateTime? pzvDateML { get; set; }

    //        [Display(Name = "Дата МЛ (Уточн.)", Order = 37, GroupName = "Назначения")]
    //        [DataType(DataType.DateTime)]
    //        public DateTime? pzvDateMLUt { get; set; }

    //        [Display(Name = "Дата мастера", Order = 38, GroupName = "Назначения")]
    //        [DataType(DataType.DateTime)]
    //        public DateTime? pzvDateMast { get; set; }

    //        // === План и факт ===
    //        [Display(Name = "Количество (факт)", Order = 40, GroupName = "План и факт")]
    //        public int? pzvRKol { get; set; }

    //        [Display(Name = "Секунд назначено", Order = 41, GroupName = "План и факт")]
    //        public int? pzvSekNazn { get; set; }

    //        [Display(Name = "Часов назначено", Order = 42, GroupName = "План и факт")]
    //        [DataType(DataType.Currency)]
    //        public decimal? pzvChasNazn { get; set; }

    //        [Display(Name = "Кол-во назначено", Order = 43, GroupName = "План и факт")]
    //        public int? pzvKolNazn { get; set; }

    //        [Display(Name = "Вид производства", Order = 44, GroupName = "План и факт")]
    //        [StringLength(2)]
    //        public string? pzvVidPr { get; set; }

    //        // === Служебное ===
    //        [Display(Name = "Компьютер добавления", Order = 50, GroupName = "Служебное")]
    //        [ReadOnly(true)]
    //        [StringLength(50)]
    //        public string? pzvCompAdd { get; set; }

    //        [Display(Name = "Дата добавления", Order = 51, GroupName = "Служебное")]
    //        [DataType(DataType.DateTime)]
    //        [ReadOnly(true)]
    //        public DateTime? pzvDateAdd { get; set; }

    //        [Display(Name = "Дата обновления", Order = 52, GroupName = "Служебное")]
    //        [DataType(DataType.DateTime)]
    //        public DateTime? pzvUpdDate { get; set; }
    //    }

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



    //public class PzvOperRow
    //{
    //    // === Идентификаторы / связи ===
    //    [Display(Name = "ID операции", Order = 0, GroupName = "Операция")]
    //    [ReadOnly(true)]
    //    public int? olPzvID { get; set; }

    //    [Display(Name = "ID родителя", Order = 1, GroupName = "Операция")]
    //    [ReadOnly(true)]
    //    public int? olPzvIDParent { get; set; }

    //    [Display(Name = "ID м/оп", Order = 2, GroupName = "Операция")]
    //    [ReadOnly(true)]
    //    public int? olPzvIDMlOp { get; set; }

    //    [Display(Name = "annID (операции)", Order = 3, GroupName = "Операция")]
    //    [ReadOnly(true)]
    //    public int? olPzvAnnID { get; set; }

    //    [Display(Name = "nrID (норма)", Order = 4, GroupName = "Операция")]
    //    [ReadOnly(true)]
    //    public int? olPzvNrID { get; set; }

    //    [Display(Name = "Бригада", Order = 5, GroupName = "Операция")]
    //    public int? olPzvIdBrig { get; set; }

    //    [Display(Name = "KML ID", Order = 6, GroupName = "Машина")]
    //    public int? olPzvKmlID { get; set; }

    //    [Display(Name = "Артикул", Order = 7, GroupName = "Операция")]
    //    [StringLength(64)]
    //    public string olPzvArticul { get; set; }

    //    // === Номенклатура и партия ===
    //    [Display(Name = "Номенклатура (nom)", Order = 10, GroupName = "Номенклатура и партия")]
    //    [Required] public int olNom { get; set; }

    //    [Display(Name = "Номенклатура (nom_n)", Order = 11, GroupName = "Номенклатура и партия")]
    //    [Required] public int olNomN { get; set; }

    //    [Display(Name = "annID (ном.)", Order = 12, GroupName = "Номенклатура и партия")]
    //    public int? olAnnID { get; set; }

    //    [Display(Name = "Номер задания", Order = 13, GroupName = "Номенклатура и партия")]
    //    [StringLength(100)]
    //    public string olNomZad { get; set; }

    //    [Display(Name = "№ партии (n_pach)", Order = 14, GroupName = "Номенклатура и партия")]
    //    public int? olNPach { get; set; }

    //    [Display(Name = "Код партии", Order = 15, GroupName = "Номенклатура и партия")]
    //    [StringLength(19)]
    //    public string olPachKod { get; set; }

    //    [Display(Name = "Код изделия", Order = 16, GroupName = "Номенклатура и партия")]
    //    [StringLength(8)]
    //    public string olKod { get; set; }

    //    // === Нормы / операция ===
    //    [Display(Name = "№ опер. (n)", Order = 20, GroupName = "Нормы и время")]
    //    public int? olNo { get; set; }

    //    [Display(Name = "№ п/оп (n1)", Order = 21, GroupName = "Нормы и время")]
    //    public int? olNpo { get; set; }

    //    [Display(Name = "Наименование операции", Order = 22, GroupName = "Нормы и время")]
    //    [StringLength(256)]
    //    public string olOperName { get; set; }

    //    [Display(Name = "Код оборудования", Order = 23, GroupName = "Нормы и время")]
    //    public int? olKodOb { get; set; }

    //    [Display(Name = "Класс оборудования", Order = 24, GroupName = "Нормы и время")]
    //    [StringLength(128)]
    //    public string olOborudClass { get; set; }

    //    [Display(Name = "Разряд", Order = 25, GroupName = "Нормы и время")]
    //    public int? olRazryd { get; set; }

    //    [Display(Name = "Сек/ед", Order = 26, GroupName = "Нормы и время")]
    //    public decimal? olSekEd { get; set; }

    //    [Display(Name = "Кол-во (шт)", Order = 27, GroupName = "Нормы и время")]
    //    [Range(0, int.MaxValue)]
    //    public int? olKol { get; set; }

    //    [Display(Name = "Трудоёмк., ч", Order = 28, GroupName = "Нормы и время")]
    //    [DataType(DataType.Currency)]
    //    public decimal? olSekAll { get; set; } // фактически decimal(…,2) — часы

    //    // === Машина / назначения ===
    //    [Display(Name = "Машина (№)", Order = 30, GroupName = "Машина и бригада")]
    //    [StringLength(64)]
    //    public string olKmlNumber { get; set; }

    //    [Display(Name = "Назначено КМЛ", Order = 31, GroupName = "Назначения и даты")]
    //    [DataType(DataType.DateTime)]
    //    public DateTime? olPzvDateNaznKm { get; set; }

    //    [Display(Name = "Таб. №", Order = 32, GroupName = "Назначения и даты")]
    //    public int? olPzvTab { get; set; }

    //    [Display(Name = "Назначено табелем", Order = 33, GroupName = "Назначения и даты")]
    //    [DataType(DataType.DateTime)]
    //    public DateTime? olPzvDateNaznTab { get; set; }

    //    [Display(Name = "Старт", Order = 34, GroupName = "Назначения и даты")]
    //    [DataType(DataType.DateTime)]
    //    public DateTime? olPzvDateStart { get; set; }

    //    [Display(Name = "Окончание", Order = 35, GroupName = "Назначения и даты")]
    //    [DataType(DataType.DateTime)]
    //    public DateTime? olPzvDateEnd { get; set; }

    //    [Display(Name = "Мастер-лог", Order = 36, GroupName = "Назначения и даты")]
    //    [DataType(DataType.DateTime)]
    //    public DateTime? olPzvDateML { get; set; }

    //    [Display(Name = "Проверка мастера", Order = 37, GroupName = "Назначения и даты")]
    //    [DataType(DataType.DateTime)]
    //    public DateTime? olPzvDateMast { get; set; }

    //    [Display(Name = "Обновлено", Order = 38, GroupName = "Назначения и даты")]
    //    [DataType(DataType.DateTime)]
    //    public DateTime? olPzvUpdDate { get; set; }
    //}

}