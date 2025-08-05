using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.Articul.Models
{
    public class SpArticulGrupMenViewModel : ArticulModel, INotifyPropertyChanged
    {

        public GrupMenModel GrupMen { get; set; } = new GrupMenModel();

        private string _po = " ";
        private int _prPo = 0;

        [NotMapped]
        public string po
        {
            get => _po;
            set
            {
                if (_po != value)
                {
                    _po = value;
                    OnPropertyChanged(nameof(po));
                }
            }
        }

        [NotMapped]
        public int prPo
        {
            get => _prPo;
            set
            {
                if (_prPo != value)
                {
                    _prPo = value;
                    OnPropertyChanged(nameof(prPo));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
