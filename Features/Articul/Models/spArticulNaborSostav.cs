using SewingProduction.Interfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SewingProduction.Features.Articul.Models
{
    public class SpArticulNaborSostav : INewable, IModifiable, IDeletable, INotifyPropertyChanged
    {
        public int Ans_id { get; set; }        // identity PK
        public string Kod { get; set; }        // char(8) not null
        public int Ta_id { get; set; }         // not null
        public int Tk_id { get; set; }         // not null
        public int Id_gost { get; set; }       // not null
        public int Ag_id { get; set; }         // not null
        public string Sostav { get; set; }     // nvarchar(50) null
        public int Id_razm_nab { get; set; }   // not null
        public string Razm { get; set; }       // nvarchar(50) not null

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        [NotMapped]
        public string Txt_v { get; set; }       // Ta_id
        [NotMapped]
        public string Tk_name { get; set; }     // Tk_id
        [NotMapped]
        public string N_i { get; set; }         // Ag_id
        [NotMapped]
        public string Ag_name_sokr { get; set; }// Ag_name_sokr
        [NotMapped]
        public string razm_all { get; set; }    // id_razm_nab
        [NotMapped]
        public string Tat_name { get; set; }        // ассортимент

        [NotMapped]
        public string Name_gost { get; set; }      // ГОСТ
        [NotMapped]
        public bool IsNew { get; set; } = false;
        [NotMapped]
        public bool IsModified { get; set; } = false;
        [NotMapped]
        public bool IsDeleted { get; set; } = false;
    }
}
