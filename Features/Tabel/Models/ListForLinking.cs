using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Tabel.Models
{
    public class ListForLinking
    {
        public string inn { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public DateTime? bDay { get; set; }
        public string? vid_gr { get; set; }
        public string s_uin { get; set; }
        public DateTime? date_p { get; set; }
        public DateTime? date_u { get; set; }
        public string? orgName { get; set; }
        public string? naimen { get; set; } 
        public string? scheduleName { get; set; }
        public string? podrName { get; set; }
        public int? idSchedule { get; set; }
        public int? gr { get; set; }


    }
}
