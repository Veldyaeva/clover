using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Articul.Models
{
    public class PlanSezonAllModel
    {
        public int Psa_id { get; set; }
        public int N { get; set; }
        public string Nn { get; set; }
        [NotMapped]
        public string Year { get { return Nn.Substring(0, 4); } }
        [NotMapped]
        public string Seazon { get { return Nn.Substring(4, 1); } }
        public string Kodd { get; set; }
        public string Grup { get; set; }
        public string Articul { get; set; }
        public string Mod { get; set; }
        public string Men { get; set; }
        public string Text_mo { get; set; }
        public string Tb_id { get; set; }
        public string Tgm_id { get; set; }
        public int Tg_id_n { get; set; }
        public int Tgm_id_n { get; set; }
        [NotMapped]
        public string TCAT_CategoryName { get; set; }
        [NotMapped]
        public string TCDS_Name { get; set; }
    }
}
