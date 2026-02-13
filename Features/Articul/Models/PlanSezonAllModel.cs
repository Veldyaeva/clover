using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Articul.Models
{
    public class PlanSezonAllModel
    {
        public int Psa_id { get; set; }
        public decimal n { get; set; }
        public string nn { get; set; }
        public string year { get { return nn.Substring(0, 4); } }
        public string seazon { get { return nn.Substring(4, 1); } }
        public string kodd { get; set; }
        public string grup { get; set; }
        public string articul { get; set; }
        public string mod { get; set; }
        public string men { get; set; }
        public string text_mo { get; set; }
        public string tb_id { get; set; }
        public string tgm_id { get; set; }
        public int tg_id_n { get; set; }
        public int tgm_id_n { get; set; }
    }
}
