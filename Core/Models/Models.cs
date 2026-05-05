using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DevExpress.Skins.SolidColorHelper;

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
        public int Pr_nabor { get; set; }
        public int Tk_id { get; set; }
        public int Id_glav_gost { get; set; }
    }
    public class GostGrupIzdViewModel
    {
        public int Id_gost { get; set; }
        public int Ag_id { get; set; }
        public string Ag_name_sokr { get; set; }
        public string Ag_tnved { get; set; } //gost_sv_pict.ag_tnved
        public string N_i { get; set; } //articul_grup.ag_naimen полное название группы 
        public string N_g { get; set; } //gost_vid_cheloveka.name_vid 
        public int Tk_id { get; set; }
        public string Care_instructions { get; set; } // описание по уходу 
        public string CareImagePath { get; set; } = ""; // картинка с символами по уходу 
        
    }
    public class GostRazmerNabViewModel
    {
        public int Id_razmer { get; set; }
        public string Razm { get; set; }
        public int Id_gost { get; set; }
        public int Id_gost_parent { get; set; }
    }

    public class TmModel
    {
        public int M_id_sp { get; set; }
        public int M_id_gl { get; set; }
        public string Kle { get; set; }
        public string Kle_naimen { get; set; }
    }
    public class TovarSeasonModel
    {
        public int Tsn_GlobalCod { get; set; }
        public string Tsn_name { get; set; }
        public string Tsn_shortName { get; set; }
    }
    public class CountryModel
    {
        public int frm_id_country { get; set; }
        public string frm_country { get; set; }
        public int frm_cu_id { get; set; }
    }
    public class  Szon_newModel
    {
        public int N { get; set; }
        public string Txt { get; set; }
    }

}
