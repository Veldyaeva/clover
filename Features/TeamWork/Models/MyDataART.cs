using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SewingProduction.Models
{
    public class MyDataART : Interfaces.ICheckable, INotifyPropertyChanged
    {
        private string _articul;
        private string _articulForRt;
        private string _tbId;
        private string _minSizeAll;
        private string _maxSizeAll;
        private string _koddRt;
        private string _kodd;
        private string _grup;
        private string _mod;
        private string _sizeLabel;
        private string _bindedArt;

        public string Articul
        {
            get => _articul;
            set => _articul = StringNormalizer.TrimOrNull(value);
        }
        public string ArticulForRT
        {
            get => _articulForRt;
            set => _articulForRt = StringNormalizer.TrimOrNull(value);
        }
        /// <summary>
        /// Блок, в котором запланирована модель
        /// </summary>
        public string tb_id
        {
            get => _tbId;
            set => _tbId = StringNormalizer.TrimOrNull(value);
        }
        public string minSizeAll
        {
            get => _minSizeAll;
            set => _minSizeAll = StringNormalizer.TrimOrNull(value);
        }
        public string maxSizeAll
        {
            get => _maxSizeAll;
            set => _maxSizeAll = StringNormalizer.TrimOrNull(value);
        }
        public string kodd_rt
        {
            get => _koddRt;
            set => _koddRt = StringNormalizer.TrimOrNull(value);
        }
        public string kodd
        {
            get => _kodd;
            set => _kodd = StringNormalizer.TrimOrNull(value);
        }
        [Column("grup")]
        public string grup
        {
            get => _grup;
            set => _grup = StringNormalizer.TrimOrNull(value);
        }
        [Column("mod")]
        public string mod
        {
            get => _mod;
            set => _mod = StringNormalizer.TrimOrNull(value);
        }
        [Column("size_label")]
        public string size_label
        {
            get => _sizeLabel;
            set => _sizeLabel = StringNormalizer.TrimOrNull(value);
        }
        [NotMapped]
        public string BindedArt
        {
            get => _bindedArt;
            set => _bindedArt = StringNormalizer.TrimOrNull(value);
        }

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
