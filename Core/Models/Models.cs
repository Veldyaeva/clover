using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Core.Models
{
    public class AssortModel
    {
        public int kod_v { get; set; }
        public string txt_v { get; set; }
    }
    public class TvnModel
    {
        public int TK_ID { get; set; }
        public string TK_NAME { get; set; }
        public string Men { get; set; }
    }
    public class GostModel
    {
        public int Id_gost { get; set; }
        public string Name_gost { get; set; }
        public string Opi_gost { get; set; }
    }
    public class GostGrupIzdViewModel
    {
        public int Id_gost { get; set; }
        public int Ag_id { get; set; }
        public string Ag_tnved { get; set; } //gost_sv_pict.ag_tnved
        public string N_i { get; set; } //articul_grup.ag_naimen
        public string N_g { get; set; } //gost_vid_cheloveka.name_vid
    }
}
