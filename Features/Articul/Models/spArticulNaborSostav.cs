using SewingProduction.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Articul.Models
{
    public class spArticulNaborSostav 
    {
        // kod,tk_name,tat_name,id_gost,name_gost,ag_naimen,sostav,razm
        [NotMapped]
        public string Kod { get; set; }
        [NotMapped]
        public string Tk_name { get; set; }
        [NotMapped]
        public string Tat_name { get; set; }
        [NotMapped]
        public int Id_gost { get; set; }
        [NotMapped]
        public string Name_gost { get; set; }
        [NotMapped]
        public string Ag_naimen { get; set; }
        [NotMapped]
        public string Sostav { get; set; }
        [NotMapped]
        public string Razm { get; set; }


    }
}
