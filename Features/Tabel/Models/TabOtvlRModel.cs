using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Tabel.Models
{
    public class TabOtvlRModel
    {
        public int? Id { get; set; }
        public string? Mg { get; set; }
        public int? Gr { get; set; }
        public int? Tab { get; set; }
        public int? N_r { get; set; }
        public DateTime? Dat { get; set; }

        #region Приемка
        public decimal Priem_t { get; set; } = 0m;
        [NotMapped]
        public DateTime? Priem_t_Time
        {
            get => DecimalToTime(Priem_t);
            set => Priem_t = TimeToDecimal(value);
        }
        public DateTime? Priem_t_s { get; set; }
        public DateTime? Priem_t_po { get; set; }
        #endregion

        #region Выклдака
        public decimal Sort_t { get; set; } = 0m;
        [NotMapped]
        public DateTime? Sort_t_Time
        {
            get => DecimalToTime(Sort_t);
            set => Sort_t = TimeToDecimal(value);
        }
        public DateTime? Sort_t_s { get; set; }
        public DateTime? Sort_t_po { get; set; }
        #endregion

        #region Другой склад
        public decimal Drug_s { get; set; } = 0m;
        [NotMapped]
        public DateTime? Drug_s_Time
        {
            get => DecimalToTime(Drug_s);
            set => Drug_s = TimeToDecimal(value);
        }
        public DateTime? Drug_s_s { get; set; }
        public DateTime? Drug_s_po { get; set; }
        #endregion

        #region Отвлеченка
        public decimal Otvl_r { get; set; } = 0m;
        [NotMapped]
        public DateTime? Otvl_r_Time
        {
            get => DecimalToTime(Otvl_r);
            set => Otvl_r = TimeToDecimal(value);
        }
        public DateTime? Otvl_r_s { get; set; }
        public DateTime? Otvl_r_po { get; set; }
        #endregion

        #region Convert
        private static DateTime? DecimalToTime(decimal value)
        {
            if (value <= 0m) return null;
            var minutes = (int)Math.Round(value * 60m, MidpointRounding.AwayFromZero);
            return DateTime.Today.AddMinutes(minutes);
        }

        private static decimal TimeToDecimal(DateTime? value)
        {
            if (value == null) return 0m;
            var hours = (decimal)value.Value.TimeOfDay.TotalMinutes / 60m;
            return Math.Round(hours, 2, MidpointRounding.AwayFromZero);
        }
        #endregion
        public int? OrNew { get; set; }
    }
}
