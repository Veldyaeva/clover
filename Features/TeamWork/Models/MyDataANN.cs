using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SewingProduction.Models
{
    public class MyDataANN : Interfaces.ICheckable, INotifyPropertyChanged
    {
        public int AnnID { get; set; }
        public string Kod { get; set; }
        public string Articul { get; set; }
        public int Status { get; set; }
        public string grup { get; set; }
        public string mod { get; set; }
        public string size_label { get; set; }
        [Column("data_obn")]
        public DateTime? dateUpdate { get; set; }
        [NotMapped]
        public string Stat { get; set; }
        [NotMapped]
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
