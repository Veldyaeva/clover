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
        private int _kod_v;
        private string _kle;

        public int Kod
        {
            get => _kod;
            set { if (_kod != value) { _kod = value; OnPropertyChanged(nameof(Kod)); } }
        }

        public string Grup
        {
            get => _grup;
            set { if (_grup != value) { _grup = value; OnPropertyChanged(nameof(Grup)); } }
        }

        public string Articul
        {
            get => _articul;
            set { if (_articul != value) { _articul = value; OnPropertyChanged(nameof(Articul)); } }
        }

        public string Mod
        {
            get => _mod;
            set { if (_mod != value) { _mod = value; OnPropertyChanged(nameof(Mod)); } }
        }

        public string Razm
        {
            get => _razm;
            set { if (_razm != value) { _razm = value; OnPropertyChanged(nameof(Razm)); } }
        }

        public string Sost
        {
            get => _sost;
            set { if (_sost != value) { _sost = value; OnPropertyChanged(nameof(Sost)); } }
        }

        public int Kod_v
        {
            get => _kod_v;
            set { if (_kod_v != value) { _kod_v = value; OnPropertyChanged(nameof(Kod_v)); } }
        }

        public string Kle
        {
            get => _kle;
            set { if (_kle != value) { _kle = value; OnPropertyChanged(nameof(Kle)); } }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
