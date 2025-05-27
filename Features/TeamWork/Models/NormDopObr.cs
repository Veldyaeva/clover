using SewingProduction.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Models
{
   public class NormDopObr : INewable, INotifyPropertyChanged, IModifiable
    {
        public int AnnId { get; set; }
        public string Kod { get; set; }
        public int SekP { get; set; }
        public int SekV { get; set; }
        public int SekStra { get; set; }
        public int SekTamp { get; set; }
        [NotMapped]
        public bool IsNew { get; set; } = true;
        [NotMapped]
        public bool IsModified { get; set; } = false;

        private int _doId;

        public int doId
        {
            get => _doId;
            set
            {
                _doId = value;
                OnPropertyChanged(nameof(doId));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            // Не вызываем событие для ID, IsNew, IsModified, чтобы избежать проблем с потоками при сохранении
            // и лишних срабатываний RowStyle
            bool isInternalProperty = propertyName == nameof(doId) || 
                                     propertyName == nameof(IsNew) || 
                                     propertyName == nameof(IsModified);

            if (!isInternalProperty)
            {
                 PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

        }
    }
}
