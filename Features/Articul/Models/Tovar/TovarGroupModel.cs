using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Articul.Models
{
    public class TovarGroupModel
    {
        public int TG_ID { get; set; }
        public int TG_GlovalCode { get; set; }
        public string TG_1cCode { get; set; }
        public string TG_TC_ID { get; set; }
        public string TG_GroupName { get; set; }
        public int DeletionMark { get; set; }


    }
}
