using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SewingProduction.Features.CardByNom.Models
{
    public class RasNomList
    {
        public string nom_zad { get; set; }
        public int nom_pach { get; set; }
        public int minPach { get; set; }
        public int maxPach { get; set; }
        public string mg_zakr { get; set; }
        public string kod7 { get; set; }
        public string grup_pach { get; set; }
        public string articul_pach { get; set; }
        public string mod_pach { get; set; }
        public int yearPach { get; set; }
        public int source { get; set; }
        public int proizvType { get; set; }
        public string dost_zeh { get; set; }
        public int id_brig { get; set; }
        public DateTime? data_r { get; set; }
    }
}
