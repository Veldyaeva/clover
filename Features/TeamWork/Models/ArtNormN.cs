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
        public ArtNormN() { }
        private string _kod;
        private string _group;
        private string _articul;
        private string _mod;
        private string _size_label;
        private int _sek;
        private int _diz;
        private int _constr;
        private string _komment;
        private string _reco;

        [NotMapped]
        public bool IsNew { get; set; } = true;
        public string Kod
        {
            get => _kod;
            set { if (_kod != value) { _kod = value; OnPropertyChanged(nameof(Kod)); } }
        }

        [Column("grup")]
        public string grup
        {
            get => _group;
            set { if (_group != value) { _group = value; OnPropertyChanged(nameof(grup)); } }
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

        public string Size_label
        {
            get => _size_label;
            set { if (_size_label != value) { _size_label = value; OnPropertyChanged(nameof(Size_label)); } }
        }
        public int Sek
        {
            get => _sek;
            set { if (_sek != value) { _sek = value; OnPropertyChanged(nameof(Sek)); } }
        }

        [Column("sek_shv")]
        public int SekShv { get; set; }
        [Column("sek_vyaz3")]
        public int SekVyaz3 { get; set; }
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
        [Column("sek_vyaz57")]
        public int SekVyaz57 { get; set; }
        [Column("sek_vyaz18")]
        public int SekVyaz18 { get; set; }
        [NotMapped]
        public int SekShv1   {get; set;}
        [Column("st")]
        public int st { get; set; } // Стоимость?

        [Column("komment")]
        public string Komment
        {
            get => _komment;
            set { if (_komment != value) { _komment = value; OnPropertyChanged(nameof(Komment)); } }
        }
        [Column("annRecommendation")]
        public string Reco
        {
            get => _reco;
            set { if (_reco != value) { _reco = value; OnPropertyChanged(nameof(Reco)); } }
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
        
        private bool _upd;
        [NotMapped]
        public bool Upd 
        { 
            get => _upd;
            set
            {
                if (_upd != value)
                {
                    _upd = value;
                    OnPropertyChanged(nameof(Upd));
                }
            }
        }
        [Column("annDateDel")]
        public DateTime? dateDel { get; set; }
        [Column("annCompDel")]
        public string compDel { get; set; }
        [Column("annDateAdd")]
        [NotMapped]
        public DateTime? dateAdd { get; set; }
        [Column("annCompAdd")]
        [NotMapped]
        public string compAdd { get; set; }

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
                            result = "Артикул не может быть пустым";
                        break;
                    case nameof(grup):
                        if (string.IsNullOrWhiteSpace(grup))
                            result = "Группа не может быть пустой";
                        break;
                    case nameof(Mod):
                        if (string.IsNullOrWhiteSpace(Mod))
                            result = "Модель не может быть пустой";
                        break;
                    case nameof(Sek):
                        if (Sek <= 0)
                            result = "Секунды должны быть больше нуля";
                        break;
                }
                return result;
            }
        }
        [NotMapped]
        public string Error => null;
        [NotMapped]
        public bool IsModified { get; set; }
        public decimal Seb { get; set; }

        // Добавляем метод Clone для создания копии объекта

        public void CopyPropertiesFrom(ArtNormN source)
        {
            if (source == null)
            {
                return;
            }

            this.Kod = source.Kod;
            this.grup = source.grup; 
            this.Articul = source.Articul;
            this.Mod = source.Mod;
            this.Size_label = source.Size_label;
            this.SekShv = source.SekShv;
            this.SekVyaz3 = source.SekVyaz3;
            this.SekVyaz5 = source.SekVyaz5;
            this.SekVyaz6 = source.SekVyaz6;
            this.SekVyaz7 = source.SekVyaz7;
            this.SekVyaz10 = source.SekVyaz10;
            this.SekVyaz12 = source.SekVyaz12;
            this.SekVyazo = source.SekVyazo;
            this.SekVyaz = source.SekVyaz;
            this.Sek = source.Sek;
            this.Komment = source.Komment;
            this.Reco = source.Reco; 
            //this.dateCreate = source.dateCreate;
            this.Diz = source.Diz;
            this.Constr = source.Constr;
            //this.dateUpdate = source.dateUpdate;
            this.SekKr = source.SekKr;
            this.Slogn = source.Slogn;
            //this.Status = source.Status;
            //this.StatusText = source.StatusText; 
            //this.Arh = source.Arh;
            this.Seb = source.Seb;
            //this.ParentId = source.ParentId;
            this.SekVyaz14 = source.SekVyaz14;
            this.SekVyaz70 = source.SekVyaz70;
            this.SekVyaz71 = source.SekVyaz71;
            this.SekVyaz72 = source.SekVyaz72;
            this.SekVyaz62 = source.SekVyaz62;
            this.SekVyaz57 = source.SekVyaz57;
            this.SekVyaz18 = source.SekVyaz18;
            this.SekShv1 = source.SekShv1;
            //this.dateDel = source.dateDel;
            //this.compDel = source.compDel;
            //this.dateAdd = source.dateAdd;
            //this.compAdd = source.compAdd;
        }

        public ArtNormN Clone()
        {
            // MemberwiseClone создает "поверхностную" копию.
            // Для простых типов и строк это нормально.
            // Если есть ссылочные типы (кроме string), которые должны быть независимо скопированы (глубокое клонирование),
            // то их нужно клонировать отдельно внутри этого метода.
            ArtNormN cloned = (ArtNormN)this.MemberwiseClone();

            // Пример глубокого клонирования для ссылочного типа, если FioDiz это объект:
            //if (this.FioDiz != null)
            //{
            //    cloned.FioDiz = this.FioDiz.Clone(); // Предполагается, что FioModel имеет Clone()
            //}
            //if (this.FioConstr != null)
            //{
            //    cloned.FioConstr = this.FioConstr.Clone();
            //}
            return cloned;
        }

        /// <summary>
        /// Создает полную копию объекта ArtNormN (алиас для Clone для обратной совместимости)
        /// </summary>
        /// <returns>Копия объекта</returns>
        public ArtNormN CloneProperties()
        {
            return this.Clone();
        }
    }
}
