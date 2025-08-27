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
        
        




        //контроль изменения поля
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged(string propertyName)
        //    => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
