using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SewingProduction.Interfaces;

namespace SewingProduction.Models
{
    public class NormKont : INewable, INotifyPropertyChanged, IModifiable, ICloneable
    {
        [NotMapped]
        public bool IsNew { get; set; }
        [NotMapped]
        public bool IsModified { get; set; } = false;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Указываем, что значение генерируется БД
        public int nkId { get; set; }
        public int AnnId { get; set; }
        [Column("kod")]
        public int kod { get; set; }

        private string _kodO;
        public string kod_o
        {
            get => _kodO;
            set => _kodO = StringNormalizer.TrimOrNull(value);
        }

        private string _text;
        [Column("text")]
        public string text
        {
            get => _text;
            set => _text = StringNormalizer.TrimOrNull(value);
        }

        private string _spec;
        public string spec
        {
            get => _spec;
            set => _spec = StringNormalizer.TrimOrNull(value);
        }
        public decimal razryd { get; set; }

        private string _obor;
        public string obor
        {
            get => _obor;
            set => _obor = StringNormalizer.TrimOrNull(value);
        }
        public int sek { get; set; }
        public decimal seb { get; set; }
        public int n { get; set; }
        public int n_ch { get; set; }
        public int n1 { get; set; }
        [Column("seb_s")]
        public decimal sebS { get; set; }

        // Добавляем реализацию INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            // Не вызываем событие для ID, IsNew, IsModified
            bool isInternalProperty = propertyName == nameof(nkId) ||
                                     propertyName == nameof(IsNew) ||
                                     propertyName == nameof(IsModified);

            if (!isInternalProperty)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                if (propertyName != nameof(IsModified) && propertyName != nameof(IsNew) && propertyName != nameof(nkId) && !IsNew)
                {
                    IsModified = true;
                }
            }
            // Убираем автоматическую установку IsModified отсюда
            // if (propertyName != nameof(IsModified) && propertyName != nameof(IsNew))
            // {
            //      IsModified = !IsNew;
            // }
        }
        public NormKont Clone()
        {
            return (NormKont)this.MemberwiseClone();
        }

        object ICloneable.Clone() => Clone();
    }
}
