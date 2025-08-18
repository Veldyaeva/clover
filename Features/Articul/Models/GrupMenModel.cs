using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.Models;

namespace SewingProduction.Features.Articul.Models
{
    public class GrupMenModel : INotifyPropertyChanged
    {
        private string _frm_v;
        private string _frm_s;
        private string _men;

        public string frm_v
        {
            get => _frm_v;
            set { if (_frm_v != value) { _frm_v = value; OnPropertyChanged(nameof(frm_v)); } }
        }

        public string frm_s
        {
            get => _frm_s;
            set { if (_frm_s != value) { _frm_s = value; OnPropertyChanged(nameof(frm_s)); } }
        }

        public string men
        {
            get => _men;
            set { if (_men != value) { _men = value; OnPropertyChanged(nameof(men)); } }
        }

        public List<ArticulModel> Articuls { get; set; } = new List<ArticulModel>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
