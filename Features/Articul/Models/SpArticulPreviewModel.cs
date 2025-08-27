using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace SewingProduction.Features.Articul.Models
{
    public class SpArticulPreviewModel
    {
        [NotMapped]
        public string Va_kod{get;set;}
        
        public string Va_po{get;set;}
        public string Va_articul{get;set;}
        public string Va_mod{get;set;}
        public string Va_seasonname { get;set;}



        //контроль изменения поля
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged(string propertyName)
        //    => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
