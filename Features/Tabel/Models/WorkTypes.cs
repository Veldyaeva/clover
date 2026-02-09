using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Tabel.Models
{
    public class WorkTypes
    {
        public int id { get; set; }
        public string nameWorkTypes { get; set; }
        public string description { get; set; }
        public int podrId { get; set; }
    }
}
