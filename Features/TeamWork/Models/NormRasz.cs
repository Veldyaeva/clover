
using SewingProduction.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using DevExpress.Entity.Model.Metadata;

namespace SewingProduction.Models
{
    public class NormRasz : INewable, INotifyPropertyChanged, IModifiable, ICloneable
    {
        [NotMapped]
        public bool IsNew { get; set; }
        [NotMapped]
        public bool IsModified { get; set; } = false;
        [NotMapped]
        public string DisplayNumber => N1 > 0 ? $"{N}.{N1}" : $"{N}";
        public int nrId { get; set; }
        public int AnnId { get; set; }
        public string Kod { get; set; }
        public string kod_o { get; set; }
        public string Text { get; set; }
        public int razryd { get; set; }
        public int N { get; set; }
        public int N1 { get; set; }
        public int Sek { get; set; }
        public decimal Seb { get; set; }
        public string Obor { get; set; }
        public string Spec { get; set; }
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


        public void CopyPropertiesFrom(NormRasz source)
        {
            if (source == null) return; // или throw new ArgumentNullException(nameof(source));

            this.nrId = source.nrId;
            this.AnnId = source.AnnId;
            this.N = source.N;
            this.N1 = source.N1;
            this.razryd = source.razryd; // Предполагается, что тип int
            this.Text = source.Text;
            this.Sek = source.Sek;     // Предполагается, что тип int
            this.Spec = source.Spec;
            this.Obor = source.Obor;
            this.Kod = source.Kod;
            this.kod_o = source.kod_o; // Обратите внимание на регистр, если отличается от свойства
            this.KodProizv = source.KodProizv; // Предполагается, что тип int? или int
            this.KodPodr = source.KodPodr;   // Предполагается, что тип int? или int
            this.KodOb = source.KodOb;       // Предполагается, что тип int? или int
            this.Seb = source.Seb;           // Предполагается, что тип decimal? или decimal


            this.IsNew = source.IsNew;
            this.IsModified = source.IsModified;

        }
        public NormRasz Clone()
        {
            return (NormRasz)this.MemberwiseClone();
        }

        object ICloneable.Clone() => Clone();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (propertyName != nameof(IsModified) &&
                propertyName != nameof(IsNew) &&
                propertyName != nameof(nrId))
            {
                IsModified = true;
            }

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
