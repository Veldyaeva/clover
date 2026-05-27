using SewingProduction.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace SewingProduction.Features.TeamWork.Models
{
    public sealed class ThreadNormRow : INewable, IModifiable, IDeletable, INotifyPropertyChanged
    {
        private int _id;
        private string _men = string.Empty;
        private int _tg_id_n;
        private int _ta_id;
        private decimal _norm;
        private int _kod_dr;
        private string _kod3 = string.Empty;
        private string _kod_art = string.Empty;
        private DateTime? _date_change;

        public int id
        {
            get => _id;
            set => SetField(ref _id, value);
        }

        public string men
        {
            get => _men;
            set => SetField(ref _men, value ?? string.Empty);
        }

        public int tg_id_n
        {
            get => _tg_id_n;
            set => SetField(ref _tg_id_n, value);
        }

        public int ta_id
        {
            get => _ta_id;
            set => SetField(ref _ta_id, value);
        }

        public decimal norm
        {
            get => _norm;
            set => SetField(ref _norm, value);
        }

        public int kod_dr
        {
            get => _kod_dr;
            set => SetField(ref _kod_dr, value);
        }

        public string kod3
        {
            get => _kod3;
            set => SetField(ref _kod3, value ?? string.Empty);
        }

        public string kod_art
        {
            get => _kod_art;
            set => SetField(ref _kod_art, value ?? string.Empty);
        }

        public DateTime? date_change
        {
            get => _date_change;
            set => SetField(ref _date_change, value);
        }


        [NotMapped] public string men_name { get; set; } = string.Empty;
        [NotMapped] public int TC_ID { get; set; }
        [NotMapped] public string TC_ClassName { get; set; } = string.Empty;
        [NotMapped] public int TG_ID { get; set; }
        [NotMapped] public string TG_GroupName { get; set; } = string.Empty;
        [NotMapped] public string TCAT_CategoryName { get; set; } = string.Empty;
        [NotMapped] public string TAT_Name { get; set; } = string.Empty;
        [NotMapped] public string ThreadArticul { get; set; } = string.Empty;
        [NotMapped] public string ThreadDisplay { get; set; } = string.Empty;
        [NotMapped] public bool approved
        {
            get => date_change.HasValue;
            set
            {
                if (value)
                {
                    date_change = date_change ?? DateTime.Now;
                }
                else
                {
                    date_change = null;
                }
            }
        }

        [NotMapped] public bool IsNew { get; set; }
        [NotMapped] public bool IsModified { get; set; }
        [NotMapped] public bool IsDeleted { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ThreadNormRow CloneForCopy(int kodDr, string kod3, string kodArt, string threadDisplay)
        {
            return new ThreadNormRow
            {
                id = 0,
                men = men,
                tg_id_n = tg_id_n,
                ta_id = ta_id,
                norm = norm,
                kod_dr = kodDr,
                kod3 = kod3 ?? string.Empty,
                kod_art = kodArt ?? string.Empty,
                date_change = null,
                men_name = men_name,
                TC_ID = TC_ID,
                TC_ClassName = TC_ClassName,
                TG_ID = TG_ID,
                TG_GroupName = TG_GroupName,
                TCAT_CategoryName = TCAT_CategoryName,
                TAT_Name = TAT_Name,
                ThreadDisplay = threadDisplay ?? string.Empty,
                ThreadArticul = kodArt ?? string.Empty,
                IsNew = true,
                IsModified = true
            };
        }

        private void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value))
            {
                return;
            }

            field = value;
            if (!IsNew)
            {
                IsModified = true;
            }

            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (propertyName == nameof(date_change))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(approved)));
            }
        }
    }
    public sealed class ThreadNormDbRow
    {
        public int id { get; set; }
        public string men { get; set; }
        public int tg_id_n { get; set; }
        public int ta_id { get; set; }
        public decimal norm { get; set; }
        public int kod_dr { get; set; }
        public string kod3 { get; set; }
        public string kod_art { get; set; }
        public DateTime? date_change { get; set; }
        

        public static ThreadNormDbRow ToDbRow(ThreadNormRow row)
        {
            return new ThreadNormDbRow
            {
                id = row.id,
                men = row.men,
                tg_id_n = row.tg_id_n,
                ta_id = row.ta_id,
                norm = row.norm,
                kod_dr = row.kod_dr,
                kod3 = row.kod3,
                kod_art = row.kod_art,
                date_change = row.date_change
            };
        }
    }

    public sealed class ThreadAssortModel
    {
        public int TAT_ID { get; set; }
        public string TAT_Name { get; set; } = string.Empty;
    }
}
