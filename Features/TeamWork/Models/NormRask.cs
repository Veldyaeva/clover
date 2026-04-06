using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using SewingProduction.Interfaces;

namespace SewingProduction.Models
{
    public class NormRask : INewable, INotifyPropertyChanged, IModifiable, ICloneable
    {
        [NotMapped]
        public bool IsNew { get; set; }
        [NotMapped]
        public bool IsModified { get; set; } = false;
        [NotMapped]
        public string DisplayNumber => N1 > 0 ? $"{N}.{N1}" : $"{N}";
        private int _id;
        public int id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(id));
            }
        }
        [Column("annId")]
        public int AnnId { get; set; }

        private string _kodO;
        [Column("kod_o")]
        public string Kod_o
        {
            get => _kodO;
            set => _kodO = StringNormalizer.TrimOrNull(value);
        }

        private string _textRask;
        [Column("text")]
        public string TextRask
        {
            get => _textRask;
            set => _textRask = StringNormalizer.TrimOrNull(value);
        }

        private string _spec;
        [Column("spec")]
        public string Spec
        {
            get => _spec;
            set => _spec = StringNormalizer.TrimOrNull(value);
        }
        public int razryd { get; set; }

        private string _obor;
        public string Obor
        {
            get => _obor;
            set => _obor = StringNormalizer.TrimOrNull(value);
        }
        public int Kod { get; set; }
        public int N { get; set; }
        public int N1 { get; set; }
        public int N_ch { get; set; }
        public int? Sek { get; set; }
        public decimal? Seb { get; set; }
        public int Seb_s { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            // Не вызываем событие для ID, IsNew, IsModified
            bool isInternalProperty = propertyName == nameof(id) || // Используем 'id' для NormRask
                                     propertyName == nameof(IsNew) ||
                                     propertyName == nameof(IsModified);

            if (!isInternalProperty)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                if (propertyName != nameof(IsModified) && propertyName != nameof(IsNew) && propertyName != nameof(id) && !IsNew)
                {
                    IsModified = true;
                }
            }

        }
        public NormRask Clone()
        {
            return (NormRask)this.MemberwiseClone();
        }

        object ICloneable.Clone() => Clone();
    }
}
