using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Tabel.Models
{
    public class TabelSpLockModel
    {
        public int? TslID { get; set; }
        public int? Tsl_Mes { get; set; }
        public int? Tsl_God { get; set; }
        public string Tsl_Mg { get; set; }
        public DateTime? Tsl_DateTo { get; set; }
        public int? Tsl_TsltID { get; set; }
        [NotMapped]
        public DateTime? Tsl_Mg_Date
        {
            get
            {
                if (string.IsNullOrEmpty(Tsl_Mg) || Tsl_Mg.Length != 4)
                    return null;

                if (!int.TryParse(Tsl_Mg.Substring(0, 2), out int month))
                    return null;

                if (!int.TryParse(Tsl_Mg.Substring(2, 2), out int year))
                    return null;

                try
                {
                    return new DateTime(2000 + year, month, 1);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
    public class TabelSpLockTypeModel
    {
        public int? TsltID { get; set; }
        public string Tslt_Name { get; set; }
    }
}
