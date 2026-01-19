using System.ComponentModel;
using SewingProduction.Core.Models;

namespace SewingProduction.Features.Articul.Models
{
    public class SpArticulGrupMenViewModel : AddNewKopmlModel, INotifyPropertyChanged
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
        public string Po { get; set; }
        public int TabIndex { get; set; } = -1;
        public string Razm_all { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
