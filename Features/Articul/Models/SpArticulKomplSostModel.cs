using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.Core.Models;

namespace SewingProduction.Features.Articul.Models
{
    public class SpArticulKomplSostModel : ArticulModel
    {
        [NotMapped]
        public string Kod_k { get; set; }
        [NotMapped]
        public string Kod_n { get; set; }
        [NotMapped]
        public string Grup_k { get; set; }
        [NotMapped]
        public string Sost_k { get; set; }
        public bool HasDifference
        {
            get
            {
                // Сравниваем с учётом null'ов названия составов комплекта и 
                return !string.Equals(Sost, Sost_k, StringComparison.OrdinalIgnoreCase);
            }
        }

    }
}
