using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.UserDistribution.Models
{
    public class AllColumnNameModel : INotifyPropertyChanged
    {
        private int _idAcn;
        private int _idAtn;
        private int _ordinalPosition;
        private string _name;
        private string _nameRus;
        private string _dataType;
        private int _readonly;
        private int _modeID;
        private string _modeName;
        private string _defaultValue;

        [Column("id_acn")]
        public int id_acn
        {
            get => _idAcn;
            set { if (_idAcn != value) { _idAcn = value; OnPropertyChanged(nameof(id_acn)); } }
        }

        [Column("id_atn")]
        public int id_atn
        {
            get => _idAtn;
            set { if (_idAtn != value) { _idAtn = value; OnPropertyChanged(nameof(id_atn)); } }
        }

        [Column("ordinal_position")]
        public int ordinal_position
        {
            get => _ordinalPosition;
            set { if (_ordinalPosition != value) { _ordinalPosition = value; OnPropertyChanged(nameof(ordinal_position)); } }
        }

        [Column("name")]
        public string name
        {
            get => _name;
            set { if (_name != value) { _name = value; OnPropertyChanged(nameof(name)); } }
        }

        [Column("name_rus")]
        public string name_rus
        {
            get => _nameRus;
            set { if (_nameRus != value) { _nameRus = value; OnPropertyChanged(nameof(name_rus)); } }
        }

        [Column("data_type")]
        public string data_type
        {
            get => _dataType;
            set { if (_dataType != value) { _dataType = value; OnPropertyChanged(nameof(data_type)); } }
        }

        [Column("readonly")]
        public int Readonly
        {
            get => _readonly;
            set { if (_readonly != value) { _readonly = value; OnPropertyChanged(nameof(Readonly)); } }
        }

        [Column("default_value")]
        public string default_value
        {
            get => _defaultValue;
            set { if (_defaultValue != value) { _defaultValue = value; OnPropertyChanged(nameof(default_value)); } }
        }

        //[Column("ModeID")]
        [NotMapped]
        public int ModeID
        {
            get => _modeID;
            set { if (_modeID != value) { _modeID = value; OnPropertyChanged(nameof(ModeID)); } }
        }

        //[Column("ModeName")]
        [NotMapped]
        public string ModeName
        {
            get => _modeName;
            set { if (_modeName != value) { _modeName = value; OnPropertyChanged(nameof(ModeName)); } }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }

}

