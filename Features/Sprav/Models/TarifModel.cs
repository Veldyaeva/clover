using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Sprav
{
    public class TarifModel : INotifyPropertyChanged
    {
        public int pcId { get; set; } = 0;
        public string constant_name { get; set; }
        public string dimens { get; set; }
        public int pcstId { get; set; }
        public string typeConst { get; set; }
        private string _describe;
        [Column("describe")]
        public string describe
        {
            get => _describe;
            set { if (_describe != value) { _describe = value; OnPropertyChanged(nameof(describe)); } }
        }
        private DateTime _begin_dt;
        [Column("begin_dt")]
        public DateTime begin_dt
        {
            get => _begin_dt;
            set { if (_begin_dt != value) { _begin_dt = value; OnPropertyChanged(nameof(begin_dt)); } }
        }

        private decimal? _value_numeric;
        [Column("value_numeric")]
        public decimal? value_numeric
        {
            get => _value_numeric;
            set { if (_value_numeric != value) { _value_numeric = value; OnPropertyChanged(nameof(value_numeric)); } }
        }

        private int? _value_integer;
        [Column("value_integer")]
        public int? value_integer
        {
            get => _value_integer;
            set { if (_value_integer != value) { _value_integer = value; OnPropertyChanged(nameof(value_integer)); } }
        }

        private float? _value_float;
        [Column("value_float")]
        public float? value_float
        {
            get => _value_float;
            set { if (_value_float != value) { _value_float = value; OnPropertyChanged(nameof(value_float)); } }
        }

        private string _value_character;
        [Column("value_character")]
        public string value_character
        {
            get => _value_character;
            set { if (_value_character != value) { _value_character = value; OnPropertyChanged(nameof(value_character)); } }
        }

        private DateTime? _value_datetime;
        [Column("value_datetime")]
        public DateTime? value_datetime
        {
            get => _value_datetime;
            set { if (_value_datetime != value) { _value_datetime = value; OnPropertyChanged(nameof(value_datetime)); } }
        }

        private string _firm;
        [Column("firm")]
        public string firm
        {
            get => _firm;
            set { if (_firm != value) { _firm = value; OnPropertyChanged(nameof(firm)); } }
        }
        [NotMapped]
        public string nameTable { get; set; }
        [NotMapped]
        public string nameField { get; set; }
        //[NotMapped]
        //public decimal valNum { get; set; }
        //[NotMapped]
        //public int valInt { get; set; }
        //[NotMapped]
        //public float valFlo { get; set; }
        //[NotMapped]
        //public string valCh { get; set; }
        //[NotMapped]
        //public DateTime valDat { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
    public class TypeItemModel
    {
        public int pcst_id { get; set; }
        public string field_name { get; set; }
        public int type_n { get; set; }
    }
}
