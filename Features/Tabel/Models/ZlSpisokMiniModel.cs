using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Tabel.Models
{
    public class ZlSpisokMiniModel
    {
        public int Tabno { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Middlename { get; set; }
        public int? Gr { get; set; }
        public int? Kod { get; set; }

        [NotMapped]
        public DateTime? Date_u { get; set; }

        [NotMapped]
        public string Fio =>
        string.Join(" ",
            new[] { Lastname.Trim(), Firstname.Trim(), Middlename.Trim() }
                .Where(x => !string.IsNullOrWhiteSpace(x)));
    }
}
