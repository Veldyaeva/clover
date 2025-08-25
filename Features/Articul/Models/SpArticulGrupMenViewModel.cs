using SewingProduction.Core.Models;
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
    public class SpArticulGrupMenViewModel : Core.Models.ArticulModel
        //, INotifyPropertyChanged
    {

        public GrupMenModel GrupMen { get; set; } = new GrupMenModel();
        public int countStr { get; set; } = 1;


        //[NotMapped]
        //public string po = " ";

        private bool _pr_po;
        public bool Pr_po
        {
            get => _pr_po;
            set
            {
                if (_pr_po != value)
                {
                    _pr_po = value;
                    OnPropertyChanged(nameof(Pr_po));
                }
            }
        }
        public int TabIndex { get; set; } = -1;
        public string Razm_all { get; set; }

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged(string propertyName)
        //    => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
