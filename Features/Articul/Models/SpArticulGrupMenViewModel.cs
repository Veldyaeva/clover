using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SewingProduction.Features.Articul.Models
{
    public class SpArticulGrupMenViewModel : ArticulModel, INotifyPropertyChanged
    {

        public GrupMenModel GrupMen { get; set; } = new GrupMenModel();

        [NotMapped]
        public string po = " ";

        private bool _pr_po;
        public bool pr_po
        {
            get => _pr_po;
            set
            {
                if (_pr_po != value)
                {
                    _pr_po = value;
                    OnPropertyChanged(nameof(pr_po));
                }
            }
        }
        public int TabIndex { get; set; } = -1;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
