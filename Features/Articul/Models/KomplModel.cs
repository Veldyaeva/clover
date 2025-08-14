using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Articul.Models
{    
    public class KomplModel : INotifyPropertyChanged
    {
        public int? kod_k { get; set; }
        public string grup_k { get; set; }
        public string articul_k { get; set; }
        public string mod_k { get; set; }
        public string razm_k { get; set; }
        public string sost_k { get; set; }

        public int? kod1 { get; set; }
        public int? kod2 { get; set; }
        public int? kod3 { get; set; }
        public int? kod4 { get; set; }
        public int? kod5 { get; set; }
        public int? kod6 { get; set; }
        public int? kod7 { get; set; }
        public int? kod8 { get; set; }
        public int? kod9 { get; set; }
        public int? kod10 { get; set; }

        public string compName { get; set; } = Environment.MachineName;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
