using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.UserDistribution.Models
{

    public class AllTableNameModel : INotifyPropertyChanged
    {
        private int _idAtn;
        private string _name;
        private string _nameRus;

        [Column("id_atn")]
        public int id_atn
        {
            get => _idAtn;
            set { if (_idAtn != value) { _idAtn = value; OnPropertyChanged(nameof(id_atn)); } }
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }


}

