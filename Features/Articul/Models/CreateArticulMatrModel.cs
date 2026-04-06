using SewingProduction.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Articul.Models
{
    public class CreateArticulMatrModel :  INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public string Nn { get; set; }
        public string Article { get; set; }
        public string RepeatArticle { get; set; }
        //товарная группа
        public int Men_int { get; set; }
        public string Grupmen_name { get; set; }
        //название сезона
        public string Tsn_name { get; set; } 
        //идентификатор сезона
        public int Baza { get; set; }
        public string Tb_id { get; set; }
        public string Mod { get; set; }
        public string ModMatrix { get; set; }
        public string Articul { get; set; }
        public string MatrixGrupName { get; set; }
        public DateTime? DatePublic { get; set; }
        public string Tm_name { get; set; }
        public string Kle {  get; set; }
        public string Text_mo { get; set; }
        public int Kod_v { get; set; }
        public string AssortName { get; set; }
        public int Printer { get; set; }
        public int Bus { get; set; }
        public int Stra { get; set; }
        public int P_pres { get; set; }
        public int P { get; set; }
        public string Mod_v { get; set; }
        public int V { get; set; }
        public short Kruj { get; set; }
        public string Tkan { get; set; }
        public string Sost { get; set; }
        public string RazmNames { get; set; }
        public string Komment { get; set; }
        public string Sost1 { get; set; }
        public string Sost2 { get; set; }
        public string Sost3 { get; set; }
        public int Ag_id { get; set; }
        public string Grup { get; set; }
        public int Id_gost { get; set; }
        public string GostName { get; set; }
        public string Tkb {  get; set; }
        public DateTime? DateCertificationApproval { get; set; }

        
    }


}
