using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SewingProduction.Models
{
    public class MyDataART : Interfaces.ICheckable, INotifyPropertyChanged
    {
        public string Articul { get; set; }
        public string ArticulForRT { get; set; }
        /// <summary>
        /// Блок, в котором запланирована модель
        /// </summary>
        public string tb_id { get; set; }
        public string minSizeAll { get; set; }
        public string maxSizeAll { get; set; }
        public string kodd_rt { get; set; }
        public string kodd { get; set; }
        [Column("grup")]
        public string grup { get; set; }
        [Column("mod")]
        public string mod { get; set; }
        [Column("size_label")]
        public string size_label { get; set; }
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
