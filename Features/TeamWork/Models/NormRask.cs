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
   public class NormRask : INewable, INotifyPropertyChanged, IModifiable
    {
        [NotMapped]
        public bool IsNew { get; set; } = true;
        [NotMapped]
        public bool IsModified { get; set; } = false;
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
        public int AnnId { get; set; }
        public string Kod_o { get; set; }
        public string Text { get; set; }
        public string Spec { get; set; }
        public int razryd { get; set; }
        public string Obor { get; set; }
        public  int Kod {  get; set; }
        public int N {  get; set; }
        public int N1 { get; set; }
        public int N_ch { get; set; }
        public int Sek { get; set; }
        public int Seb  { get; set; }
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
            }

        }
    }
}
