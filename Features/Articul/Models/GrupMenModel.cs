using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Articul.Models
{
    public class GrupMenModel : INotifyPropertyChanged
    {
        private string _frm_v;
        private string _frm_s;
        private string _men;

        public string FrmV
        {
            get => _frm_v;
            set { if (_frm_v != value) { _frm_v = value; OnPropertyChanged(nameof(FrmV)); } }
        }

        public string FrmS
        {
            get => _frm_s;
            set { if (_frm_s != value) { _frm_s = value; OnPropertyChanged(nameof(FrmS)); } }
        }

        public string Men
        {
            get => _men;
            set { if (_men != value) { _men = value; OnPropertyChanged(nameof(Men)); } }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
