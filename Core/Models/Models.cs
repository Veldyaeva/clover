using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public short? Symbol { get; set; }
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
        [NotMapped] public string Unic_IdGost_idAg => $"{Ag_id}|{Id_gost}"; // уникальное поле для поиска гост + группа по госту

    }
    public class GostRazmerNabViewModel
    {
        public int Id_razmer { get; set; }
        public string Razm { get; set; }
        public int Id_gost { get; set; }
        public int Id_gost_parent { get; set; }
    }

    public class GostSvPictModel
    {
        public int Id_pict { get; set; }
        public int Id_art { get; set; }
        public int Id_vidchel { get; set; }
        public int Id_gost { get; set; }
        public int Id_svyz { get; set; }
        public int Arh { get; set; }
        public string Ag_tnved { get; set; }
        public int Ag_tk_id { get; set; }
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

    public class PrintSewnRazmKolRow
    {
        public string Kod { get; set; } = string.Empty;
        public string Articul { get; set; } = string.Empty;
        public string Mod { get; set; } = string.Empty;
        public string Razm { get; set; } = string.Empty;
        public int? Kol { get; set; }
    }
    public class ViewPlanSezonAll
    {
        public int Tk_id { get; set; }
        public int T_typeUp { get; set; }
    }
    public class KomplNormalized
    {
        public string sost { get; set; }
        public string sost1 { get; set; }
        public string sost2 { get; set; }
        public string sost3 { get; set; }
    }
    public class PrintSewnBlVshRow
    {
        public string Kod { get; set; } = string.Empty;
        public string Grup { get; set; } = string.Empty;
        public string Articul { get; set; } = string.Empty;
        public string Mod { get; set; } = string.Empty;
        public string Kle { get; set; } = string.Empty;
        public string Razm { get; set; } = string.Empty;
        public string Razm1 { get; set; } = string.Empty;
        public string Razm2 { get; set; } = string.Empty;
        public string Sost { get; set; } = string.Empty;
        public string Sost2 { get; set; } = string.Empty;
        public string Sost3 { get; set; } = string.Empty;
        public int? IdGost { get; set; }
        public string Gost { get; set; } = string.Empty;
        public int? IdSvyaz { get; set; }
        public string Kruj { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
	}
	
}
