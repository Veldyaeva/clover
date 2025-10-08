using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using SewingProduction.Interfaces;

namespace SewingProduction.Core.Models
{
    public class ArtDrModel : INewable, IModifiable, IDeletable, INotifyPropertyChanged
    {
        public string Kod { get; set; }
        public string Kod_dr { get; set; }
        public string Articul { get; set; }
        public string Articul_poln { get; set; }
        public string T_ed { get; set; }
        public decimal T_seb { get; set; }
        public decimal Kol { get; set; }
        public decimal Sum { get; set; }
        public int Kod_dr_s { get; set; }
        public string Kod_furn { get; set; }
        public string Kod_furn_ar { get; set; }
        public string Art_fur { get; set; }
        public DateTime Data_nitki { get; set; }
        public int Adid { get; set; }
        public DateTime DateAdd { get; set; }
        public string CompAdd { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        [NotMapped]
        public bool IsModified { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
        [NotMapped]
        public bool IsDeleted { get; set; } = false;
    }
}
