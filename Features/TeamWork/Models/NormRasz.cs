
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SewingProduction.Interfaces;

namespace SewingProduction.Models
{
    public class NormRasz : INewable, INotifyPropertyChanged, IModifiable, ICloneable
    {
        [NotMapped]
        public bool IsNew { get; set; }
        [NotMapped]
        public bool IsBeingAdded { get; set; } = false;
        [NotMapped]
        public bool IsModified { get; set; } = false;
        [NotMapped]
        public string DisplayNumber => N1 > 0 ? $"{N}.{N1}" : $"{N}";
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Указываем, что значение генерируется БД
        [Column("nrID")]
        public int nrID { get; set; }
        [Column("annId")]
        public int annId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? nrDateAdd { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string nrCompAdd { get; set; }
        private string _kod;
        [Column("kod")]
        public string Kod
        {
            get => _kod;
            set => _kod = value?.Length > 7 ? value.Substring(0, 7) : value;
        }

        private string _kod_o;
        [Column("kod_o")]
        public string kod_o
        {
            get => _kod_o;
            set => _kod_o = value?.Length > 3 ? value.Substring(0, 3) : value;
        }

        private string _text;
        [Column("text")]
        public string Text
        {
            get => _text;
            set => _text = value?.Length > 200 ? value.Substring(0, 200) : value;
        }
        [Column("razryd")]
        public int razryd { get; set; }
        [Column("n")]
        public int N { get; set; }
        [Column("n1")]
        public int N1 { get; set; }
        [Column("sek")]
        public int Sek { get; set; }
        [Column("seb")]
        public decimal Seb { get; set; }

        private string _obor;
        [Column("obor")]
        public string Obor
        {
            get => _obor;
            set
            {
                var newValue = value?.Length > 35 ? value.Substring(0, 35) : value;
                SetProperty(ref _obor, newValue, nameof(Obor));
            }
        }

        private string _spec;
        [Column("spec")]
        public string Spec
        {
            get => _spec;
            set => _spec = value?.Length > 3 ? value.Substring(0, 3) : value;
        }

        [NotMapped]
        public string TextProizv { get; set; }

        [NotMapped]
        [Column("text_ob")]
        public string TextOb { get; set; }

        [NotMapped]
        public string TextVyaz { get; set; }

        private int _kod_proizv;
        [Column("kod_proizv")]
        public int KodProizv
        {
            get => _kod_proizv;
            set => SetProperty(ref _kod_proizv, value, nameof(KodProizv));
        }

        private int _kod_podr;
        [Column("kod_podr")]
        public int KodPodr
        {
            get => _kod_podr;
            set => SetProperty(ref _kod_podr, value, nameof(KodPodr));
        }

        private int _kod_ob;
        [Column("kod_ob")]
        public int KodOb
        {
            get => _kod_ob;
            set => SetProperty(ref _kod_ob, value, nameof(KodOb));
        }
        //шгкунецущкшгнейуцщшл
        public int? n_ch { get; set; }
        public decimal? seb_s { get; set; }
        public int? sek12 { get; set; }
        public int? sek7 { get; set; }
        public int? sek5 { get; set; }
        public int? sql_pr_add { get; set; }
        public DateTime? date_add { get; set; }

        private string _komp_name;
        public string komp_name { get => _komp_name; set => _komp_name = value?.Length > 50 ? value.Substring(0, 50) : value; }

        public DateTime? nrDateDel { get; set; }

        private string _nrCompDel;
        public string nrCompDel { get => _nrCompDel; set => _nrCompDel = value?.Length > 50 ? value.Substring(0, 50) : value; }
        //яычвяыкавяычапв
        public void CopyPropertiesFrom(NormRasz source)
        {
            if (source == null) return;
            nrID = source.nrID;
            annId = source.annId;
            N = source.N;
            N1 = source.N1;
            razryd = source.razryd;
            Text = source.Text;
            Sek = source.Sek;
            Spec = source.Spec;
            Obor = source.Obor;
            Kod = source.Kod;
            kod_o = source.kod_o;
            KodProizv = source.KodProizv;
            KodPodr = source.KodPodr;
            KodOb = source.KodOb;
            Seb = source.Seb;
            // Служебные поля не копируются - они должны быть актуальными для каждой записи:
            // nrDateAdd и nrCompAdd устанавливаются автоматически при INSERT (DEFAULT)
            // nrDateDel и nrCompDel устанавливаются только при пометке на удаление
            IsNew = source.IsNew;
            IsBeingAdded = source.IsBeingAdded;
            IsModified = source.IsModified;
        }

        public NormRasz Clone() => (NormRasz)this.MemberwiseClone();
        object ICloneable.Clone() => Clone();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (propertyName != nameof(IsModified) && propertyName != nameof(IsNew) && propertyName != nameof(nrID))
                IsModified = true;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
