using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Core.Models
{    
    public class KomplModel : INotifyPropertyChanged
    {
        public int? Kod_k { get; set; }
        public string Grup_k { get; set; }
        public string Articul_k { get; set; }
        public string Mod_k { get; set; }
        public string Razm_k { get; set; }
        public string Sost_k { get; set; }

        public int? Kod1 { get; set; }
        public int? Kod2 { get; set; }
        public int? Kod3 { get; set; }
        public int? Kod4 { get; set; }
        public int? Kod5 { get; set; }
        public int? Kod6 { get; set; }
        public int? Kod7 { get; set; }
        public int? Kod8 { get; set; }
        public int? Kod9 { get; set; }
        public int? Kod10 { get; set; }

        public string CompName { get; set; } = Environment.MachineName;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
