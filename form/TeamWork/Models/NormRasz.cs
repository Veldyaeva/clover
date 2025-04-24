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
    public class NormRasz : INewable, INotifyPropertyChanged, IModifiable
    {
        [NotMapped]
        public bool IsNew { get; set; } = true;
        [NotMapped]
        public bool IsModified { get; set; } = false;
        public int nrId { get; set; }
        public int AnnId { get; set; }
        public int Kod { get; set; }
        public int Kod_o { get; set; }
        public string Text { get; set; }
        [Column("razryd")]
        public int Razryad { get; set; }
        public int N { get; set; }
        public int N1 { get; set; }
        public int Sek { get; set; }
        public string Obor { get; set; }
        public int Kod_podr { get; set; }
        public int Kod_proizv { get; set; }
        public string Spec { get; set; }
        public int Kod_ob { get; set; }
        [NotMapped]
        public string TextProizv { get; set; }
        [NotMapped]
        public string TextOb { get; set; }
        [NotMapped]
        public string TextVyaz { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            // Не вызываем событие для ID, IsNew, IsModified
            bool isInternalProperty = propertyName == nameof(nrId) || // Используем nrId для NormRasz
                                     propertyName == nameof(IsNew) || 
                                     propertyName == nameof(IsModified);

            if (!isInternalProperty)
            {
                 PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

        }
    }
}
