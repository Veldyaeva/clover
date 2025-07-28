using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Sprav
{
    public class TarifRabotModel : INotifyPropertyChanged
    {
        private int _id_kod_o;
        [Column("id_kod_o")]
        public int id_kod_o
        {
            get => _id_kod_o;
            set { if (_id_kod_o != value) { _id_kod_o = value; OnPropertyChanged(nameof(id_kod_o)); } }
        }
        private string _Text;
        [Column("Text")]
        public string Text
        {
            get => _Text;
            set { if (_Text != value) { _Text = value; OnPropertyChanged(nameof(Text)); } }
        }

        private int _prizn_podr;
        [Column("prizn_podr")]
        public int prizn_podr
        {
            get => _prizn_podr;
            set { if (_prizn_podr != value) { _prizn_podr = value; OnPropertyChanged(nameof(prizn_podr)); } }
        }

        private decimal _tarif;
        [Column("tarif")]
        public decimal tarif
        {
            get => _tarif;
            set { if (_tarif != value) { _tarif = value; OnPropertyChanged(nameof(tarif)); } }
        }

        private string _ed_izm;
        [Column("ed_izm")]
        public string ed_izm
        {
            get => _ed_izm;
            set { if (_ed_izm != value) { _ed_izm = value; OnPropertyChanged(nameof(ed_izm)); } }
        }

        private decimal _koef_chas;
        [Column("koef_chas")]
        public decimal koef_chas
        {
            get => _koef_chas;
            set { if (_koef_chas != value) { _koef_chas = value; OnPropertyChanged(nameof(koef_chas)); } }
        }

        private int _razr;
        [Column("razr")]
        public int razr
        {
            get => _razr;
            set { if (_razr != value) { _razr = value; OnPropertyChanged(nameof(razr)); } }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
