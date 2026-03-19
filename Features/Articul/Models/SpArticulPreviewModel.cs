using DevExpress.Diagram.Core.Shapes;
using SewingProduction.Core.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SewingProduction.Features.Articul.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SpArticulPreviewModel : ArticulModel
    {
        [NotMapped]
        public string SeasonName { get; set; }
        [NotMapped]
        public string TmName { get; set; }
        [NotMapped]
        public string AssortName { get; set; }
        [NotMapped]
        public string CountryName { get; set; }
        [NotMapped]
        public string GrupMenName { get; set; }
        [NotMapped]
        public string GostName { get; set; }
        [NotMapped]
        public string GostOpi { get; set; }
        [NotMapped]
        public string ScNomer { get; set; }
        [NotMapped]
        public decimal Kf_tkan_kach1 { get; set; }
        [NotMapped]
        public decimal Kf_tkan_kach2 { get; set; }
        [NotMapped]
        public decimal Kf_tkan_kach3 { get; set; }
        [NotMapped]
        public decimal Kf_tkan_kach4 { get; set; }
        [NotMapped]
        public decimal Kf_tkan_kach5 { get; set; }
        [NotMapped]
        public decimal Kf_tkan_kach6 { get; set; }
        [NotMapped]
        public decimal Kf_tkan_kach7 { get; set; }
        [NotMapped]
        public decimal? Brak_percent1 { get; set; }
        [NotMapped]
        public decimal? Brak_percent2 { get; set; }
        [NotMapped]
        public decimal? Brak_percent3 { get; set; }
        [NotMapped]
        public decimal? Brak_percent4 { get; set; }
        [NotMapped]
        public decimal? Brak_percent5 { get; set; }
        [NotMapped]
        public decimal? Brak_percent6 { get; set; }
        [NotMapped]
        public decimal? Brak_percent7 { get; set; }



        //контроль изменения поля
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged(string propertyName)
        //    => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    /// <summary>
    /// 
    /// </summary>
    public partial class SpArticulPreviewModel : IDataErrorInfo
    {
        public string Error => string.Empty;

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Kod) && string.IsNullOrWhiteSpace(Kod))
                    return "Код обязателен.";
                if (columnName == nameof(Articul) && string.IsNullOrWhiteSpace(Articul))
                    return "Артикул обязателен.";
                return string.Empty;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public partial class SpArticulPreviewModel
    {
        // В БД Komb_det / Komb_izd — string, а чекбокс ждёт bool.
        [NotMapped]
        public bool KombDetFlag
        {
            get => Komb_det == "1" || string.Equals(Komb_det, "true", StringComparison.OrdinalIgnoreCase);
            set => Komb_det = value ? "1" : "0";
        }

        [NotMapped]
        public bool KombIzdFlag
        {
            get => Komb_izd == "1" || string.Equals(Komb_izd, "true", StringComparison.OrdinalIgnoreCase);
            set => Komb_izd = value ? "1" : "0";
        }
        [NotMapped]
        public bool KrujFlag
        {
            get => Kruj == 1 || string.Equals(Komb_izd, "true", StringComparison.OrdinalIgnoreCase);
            set => Kruj = (short)(value ? 1 : 0);
        }

        // Arh — int, чекбокс — bool.
        [NotMapped]
        public bool ArhFlag
        {
            get => Arh != 0;
            set => Arh = value ? 1 : 0;
        }
    }
}
