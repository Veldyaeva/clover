using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SewingProduction.Models
{
    public class MyDataART : Interfaces.ICheckable, INotifyPropertyChanged
    {
        public string Articul { get; set; }
        public string kodd_rt { get; set; }
        public string kodd { get; set; }
        [Column("grup")]
        public string grup { get; set; }
        [Column("mod")]
        public string mod { get; set; }
        [Column("razm")]
        public string razm { get; set; }
        [NotMapped]
        public string BindedArt { get; set; }

        private bool _isChecked;
        [NotMapped]
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged(nameof(IsChecked));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
