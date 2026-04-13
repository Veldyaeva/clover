using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SewingProduction.Models
{
    public class MyDataANN : Interfaces.ICheckable, INotifyPropertyChanged
    {
        private string _kod;
        private string _articul;
        private string _grup;
        private string _mod;
        private string _sizeLabel;
        private string _stat;

        public int AnnID { get; set; }
        public string Kod
        {
            get => _kod;
            set => _kod = StringNormalizer.TrimOrNull(value);
        }
        public string Articul
        {
            get => _articul;
            set => _articul = StringNormalizer.TrimOrNull(value);
        }
        public int Status { get; set; }
        public string grup
        {
            get => _grup;
            set => _grup = StringNormalizer.TrimOrNull(value);
        }
        public string mod
        {
            get => _mod;
            set => _mod = StringNormalizer.TrimOrNull(value);
        }
        public string size_label
        {
            get => _sizeLabel;
            set => _sizeLabel = StringNormalizer.TrimOrNull(value);
        }
        [Column("data_obn")]
        public DateTime? dateUpdate { get; set; }
        [NotMapped]
        public string Stat
        {
            get => _stat;
            set => _stat = StringNormalizer.TrimOrNull(value);
        }
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
