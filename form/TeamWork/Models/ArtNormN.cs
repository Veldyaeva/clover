using SewingProduction.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SewingProduction.Models
{
    public class ArtNormN : INotifyPropertyChanged, IDataErrorInfo, INewable, IModifiable
    {
        private string _kod;
        private string _group;
        private string _articul;
        private string _mod;
        private int _sek;
        private int _diz;
        private int _constr;
        private string _komment;
        [NotMapped]
        public bool IsNew { get; set; } = true;
        public string Kod
        {
            get => _kod;
            set { if (_kod != value) { _kod = value; OnPropertyChanged(nameof(Kod)); } }
        }

        [Column("grup")]
        public string Group
        {
            get => _group;
            set { if (_group != value) { _group = value; OnPropertyChanged(nameof(Group)); } }
        }

        public string Articul
        {
            get => _articul;
            set { if (_articul != value) { _articul = value; OnPropertyChanged(nameof(Articul)); } }
        }

        public string Mod
        {
            get => _mod;
            set { if (_mod != value) { _mod = value; OnPropertyChanged(nameof(Mod)); } }
        }

        public int Sek
        {
            get => _sek;
            set { if (_sek != value) { _sek = value; OnPropertyChanged(nameof(Sek)); } }
        }

        [Column("sek_shv")]
        public int SekShv { get; set; }
        [Column("sek_vyaz5")]
        public int SekVyaz5 { get; set; }
        [Column("sek_vyaz6")]
        public int SekVyaz6 { get; set; }
        [Column("sek_vyaz7")]
        public int SekVyaz7 { get; set; }
        [Column("sek_vyaz10")]
        public int SekVyaz10 { get; set; }
        [Column("sek_vyaz12")]
        public int SekVyaz12 { get; set; }
        [Column("sek_vyazo")]
        public int SekVyazo { get; set; }
        [Column("sek_vyaz")]
        public int SekVyaz { get; set; }
        [Column("sek_vyaz14")]
        public int SekVyaz14 {get; set;}
        [Column("sek_vyaz70")]
        public int SekVyaz70 {get; set;}
        [Column("sek_vyaz71")]
        public int SekVyaz71 {get; set;}
        [Column("sek_vyaz72")]
        public int SekVyaz72 {get; set;}
        [Column("sek_vyaz62")]
        public int SekVyaz62 {get; set;}
        //[Column("sek_vyaz57")]
        //public int SekVyaz57 {get; set;}
        //[Column("sek_vyaz18")]
        //public int SekVyaz18 {get; set;}
        [NotMapped]
        public int SekShv1   {get; set;}

        [Column("komment")]
        public string Komment
        {
            get => _komment;
            set { if (_komment != value) { _komment = value; OnPropertyChanged(nameof(Komment)); } }
        }

        [Column("data_sozd")]
        public DateTime? dateCreate { get; set; }

        [Column("diz")]
        public int Diz
        {
            get => _diz;
            set { if (_diz != value) { _diz = value; OnPropertyChanged(nameof(Diz)); } }
        }
        [Column("constr")]
        public int Constr
        {
            get => _constr;
            set { if (_constr != value) { _constr = value; OnPropertyChanged(nameof(Constr)); } }
        }

        [Column("data_obn")]
        public DateTime? dateUpdate { get; set; }

        [Column("sek_kr")]
        public int SekKr { get; set; }

        public int Slogn { get; set; }
        public int AnnID { get; set; }
        public int Status { get; set; }

        [NotMapped]
        public bool preArch { get; set; }
        [NotMapped]
        public string StatusText { get; set; }
   //     [NotMapped]
        public bool Arh { get; set; }
        [Column("parentId")]
        public int ParentId { get; set; }

        // Реализация INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        [NotMapped]
        public static List<FioModel> FioSource { get; set; }

        [NotMapped]
        public FioModel FioDiz => FioSource?.FirstOrDefault(f => f.Tab == Diz);

        [NotMapped]
        public FioModel FioConstr => FioSource?.FirstOrDefault(f => f.Tab == Constr);


        // Реализация IDataErrorInfo для валидации
        [NotMapped]
        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                switch (columnName)
                {
                    case nameof(Articul):
                        if (string.IsNullOrWhiteSpace(Articul))
                            result = "Артикул не может быть пустым.";
                        break;
                    case nameof(Group):
                        if (string.IsNullOrWhiteSpace(Group))
                            result = "Группа не может быть пустой.";
                        break;
                    case nameof(Mod):
                        if (string.IsNullOrWhiteSpace(Mod))
                            result = "Модель не может быть пустой.";
                        break;
                    case nameof(Sek):
                        if (Sek <= 0)
                            result = "Время шитья должно быть больше нуля.";
                        break;
                }
                return result;
            }
        }
        [NotMapped]
        public string Error => null;
        [NotMapped]
        public bool IsModified { get; set; }

        // Добавляем метод Clone для создания копии объекта
        public ArtNormN Clone()
        {
            return (ArtNormN)this.MemberwiseClone();
        }
    }
}
