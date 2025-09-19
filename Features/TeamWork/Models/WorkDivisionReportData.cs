using SewingProduction.Models;
using System;
using System.Collections.Generic;

namespace SewingProduction.Features.TeamWork.Models
{
    /// <summary>
    /// Модель данных для отчета технологической схемы разделения труда
    /// </summary>
    public class WorkDivisionReportData
    {
        // Основная информация
        public string Articul { get; set; }
        public string Grup { get; set; }
        public string Mod { get; set; }
        public string Komment { get; set; }
        public DateTime? DateCreate { get; set; }
        public string Designer { get; set; }
        public string Constructor { get; set; }
        public string Reco { get; set; }

        // Затраты времени (секунды)
        public int Sek { get; set; }
        public decimal StrSum => Sek / 60.0m; // Затраты времени в минутах + " сек / " + "+ allTrim(str(Sek)) + " сек"
        public string AllTrimStr => $"str({Sek}) + \"+allTrim({Sek})\"";

        // Количество рабочих
        public int KolRab { get; set; }

        // Такт потока
        public decimal TaktPotoka { get; set; }

        // Расчетный выпуск в смену
        public int RaschetVypusk { get; set; }

        // Итоговые секунды по видам оборудования (будут рассчитываться из NormRasz)
        public int SekShv { get; set; }      // Швейные операции
        public int SekVyaz { get; set; }     // Вязальные операции всего
        public int SekVyaz5 { get; set; }    // 5-кл
        public int SekVyaz7 { get; set; }    // 7-кл
        public int SekVyaz10 { get; set; }   // 10-кл
        public int SekVyaz12 { get; set; }   // 12-кл
        public int SekVyaz6 { get; set; }    // 6-кл
        public int SekVyazo { get; set; }    // Оверлок
        public int SekVyaz14 { get; set; }   // 14-кл
        public int SekVyaz70 { get; set; }   // 70-кл
        public int SekVyaz71 { get; set; }   // 71-кл
        public int SekVyaz72 { get; set; }   // 72-кл
        public int SekVyaz62 { get; set; }   // 62-кл
        public int SekVyaz57 { get; set; }   // 57-кл
        public int SekVyaz18 { get; set; }   // 18-кл
        public int SekKr { get; set; }       // Краеобметочные операции

        // Списки операций для отображения в отчете
        public List<NormRasz> Operations { get; set; } = new List<NormRasz>();
        public List<NormRask> RaskroyOperations { get; set; } = new List<NormRask>();
        public List<NormKont> KontrolOperations { get; set; } = new List<NormKont>();
    }

    /// <summary>
    /// Дополнительный класс для группировки секунд по оборудованию в отчете
    /// </summary>
    public class EquipmentSecondsGroup
    {
        public string EquipmentName { get; set; }
        public int Seconds { get; set; }
        public string DisplayText => $"sd({Seconds})";
    }
}