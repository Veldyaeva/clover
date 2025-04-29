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
   public class NormKont :INewable, INotifyPropertyChanged
    {
        [NotMapped]
        public bool IsNew { get; set; } = true;
        [NotMapped]
        public bool IsModified { get; set; } = false;
        [NotMapped]
        public int nkId { get; set; }
        public int AnnId { get; set; }
        public int Kod { get; set; }
        [Column("kod_o")]
        public string KodO { get; set; }
        public string Text { get; set; }
        public string Spec { get; set; }
        [Column("razryd")]
        public int Razryad { get; set; }
        public string Obor { get; set; }
        public int Sek { get; set; }
        public int Seb { get; set; }
        public int N { get; set; }
        [Column("n_ch")]
        public int NCh { get; set; }
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
            }
            // Убираем автоматическую установку IsModified отсюда
            // if (propertyName != nameof(IsModified) && propertyName != nameof(IsNew))
            // {
            //      IsModified = !IsNew;
            // }
        }
    }
}