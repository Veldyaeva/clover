using SewingProduction.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SewingProduction.Models
{
   public class NormKont :INewable, INotifyPropertyChanged, IModifiable, ICloneable
    {
        [NotMapped]
        public bool IsNew { get; set; }
        [NotMapped]
        public bool IsModified { get; set; } = false;
        [NotMapped]
        public int nkId { get; set; }
        public int AnnId { get; set; }
        [Column("kod")]
        public int Kod { get; set; }
        public string kod_o { get; set; }
        [Column("text")]
        public string Text { get; set; }
        public string Spec { get; set; }
        public int razryd { get; set; }
        public string Obor { get; set; }
        public int Sek { get; set; }
        public int Seb { get; set; }
        public int N { get; set; }
        public int n_ch { get; set; }
        public int N1 { get; set; }
        public int SebS { get; set; }

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