using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Articul.Models
{
    public class ArtKomplektModel
    {
        public int Npp { get; set; }
        public int Ak_id { get; set; }
        public string Parent_nn { get; set; }
        public string Sostav { get; set; }
        public int Tk_id { get; set; }
        public int Ta_id { get; set; }
        public int Id_gost { get; set; }
        public int Ag_id_grupgost { get; set; }
        public int Tg_id_n { get; set; }
        public int Tgm_id_n { get; set; }
        [NotMapped]
        public string TCAT_CategoryName { get; set; }
        [NotMapped]
        public string TCDS_Name { get; set; }
        [NotMapped]
        public int Tb_id { get; set; }
        [NotMapped]
        public string Tk_name { get; set; }
    }
}
