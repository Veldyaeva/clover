using SewingProduction.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace SewingProduction.Features.Articul.Models
{
    public class SpArticulPreviewModel : ArticulModel
    {
        [NotMapped]
        public string SeasonName { get;set;}
        [NotMapped]
        public string TmName { get;set; }
        [NotMapped]
        public string AssortName { get;set; }
        [NotMapped]
        public string CountryName { get; set; }
        [NotMapped]
        public string GrupMenName { get; set; }
        [NotMapped]
        public string GostName { get; set; }
        [NotMapped]
        public string GostOpi { get; set; }
        [NotMapped]
        public string ScNomer { get; set; }
        [NotMapped]
        public decimal Kf_tkan_kach1 { get; set; }
        [NotMapped]
        public decimal Kf_tkan_kach2 { get; set; }
        [NotMapped]
        public decimal Kf_tkan_kach3 { get; set; }
        [NotMapped]
        public decimal Kf_tkan_kach4 { get; set; }
        [NotMapped]
        public decimal Kf_tkan_kach5 { get; set; }
        [NotMapped]
        public decimal Kf_tkan_kach6 { get; set; }
        [NotMapped]
        public decimal Kf_tkan_kach7 { get; set; }




        //контроль изменения поля
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged(string propertyName)
        //    => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
