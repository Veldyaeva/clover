using System.Collections.Generic;
using System.ComponentModel;

namespace SewingProduction.Core.Models
{
    public class GrupMenModel : INotifyPropertyChanged
    {
        private string _frm_v;
        private string _frm_s;
        private string _men;
        private string _name;
        private string _gr;
        private int _men_int;
        public string Frm_v
        {
            get => _frm_v;
            set { if (_frm_v != value) { _frm_v = value; OnPropertyChanged(nameof(Frm_v)); } }
        }

        public string Frm_s
        {
            get => _frm_s;
            set { if (_frm_s != value) { _frm_s = value; OnPropertyChanged(nameof(Frm_s)); } }
        }

        public string Men
        {
            get => _men;
            set { if (_men != value) { _men = value; OnPropertyChanged(nameof(Men)); } }
        }

        public string Name
        {
            get => _name;
            set { if (_name != value) { _name = value; OnPropertyChanged(nameof(Name)); } }
        }   
        public string Gr
        {
            get => _gr;
            set { if (_gr != value) { _gr = value; OnPropertyChanged(nameof(Gr)); } }
        }
        public int Men_int
        {
            get => _men_int;
            set
            { if (_men_int != value) { _men_int = value; OnPropertyChanged(nameof(Men_int)); } }
        }

        public List<ArticulModel> Articuls { get; set; } = new List<ArticulModel>();


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
