using System;
using System.ComponentModel;

namespace SewingProduction.Core.Models
{
    public class KomplModel : INotifyPropertyChanged
    {
        public string Kod_k { get; set; }
        public string Grup_k { get; set; }
        public string Articul_k { get; set; }
        public string Mod_k { get; set; }
        public string Razm_k { get; set; }
        public string Sost_k { get; set; }

        public string Kod1 { get; set; }
        public string Kod2 { get; set; }
        public string Kod3 { get; set; }
        public string Kod4 { get; set; }
        public string Kod5 { get; set; }
        public string Kod6 { get; set; }
        public string Kod7 { get; set; }
        public string Kod8 { get; set; }
        public string Kod9 { get; set; }
        public string Kod10 { get; set; }

        public string CompName { get; set; } = Environment.MachineName;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
