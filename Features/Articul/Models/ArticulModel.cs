using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.Articul.Models
{
    public class ArticulModel : INotifyPropertyChanged
    {
        private int _kod;
        private string _grup;
        private string _articul;
        private string _mod;
        private string _razm;
        private string _sost;
        private string _po = " ";
        private int _pr_po = 0;
        private string _kod_v;
        private string _kle;

        public int kod
        {
            get => _kod;
            set { if (_kod != value) { _kod = value; OnPropertyChanged(nameof(kod)); } }
        }

        public string grup
        {
            get => _grup;
            set { if (_grup != value) { _grup = value; OnPropertyChanged(nameof(grup)); } }
        }

        public string articul
        {
            get => _articul;
            set { if (_articul != value) { _articul = value; OnPropertyChanged(nameof(articul)); } }
        }

        public string mod
        {
            get => _mod;
            set { if (_mod != value) { _mod = value; OnPropertyChanged(nameof(mod)); } }
        }

        public string razm
        {
            get => _razm;
            set { if (_razm != value) { _razm = value; OnPropertyChanged(nameof(razm)); } }
        }

        public string sost
        {
            get => _sost;
            set { if (_sost != value) { _sost = value; OnPropertyChanged(nameof(sost)); } }
        }

        public string kodV
        {
            get => _kod_v;
            set { if (_kod_v != value) { _kod_v = value; OnPropertyChanged(nameof(kodV)); } }
        }

        public string kle
        {
            get => _kle;
            set { if (_kle != value) { _kle = value; OnPropertyChanged(nameof(kle)); } }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
