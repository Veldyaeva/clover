/*using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SewingProduction.BdContext
{
    public class ArtNormN
    {
        public char kod { get; set; }
        public char grup { get; set; }
        public char articul { get; set; }
        public char mod { get; set; }
        public char po { get; set; }
        public int sek_shv { get; set; }
        public int sek_vyaz3 { get; set; }
        public int sek_vyaz5 { get; set; }
        public int sek_vyaz6 { get; set; }
        public int sek_vyaz7 { get; set; }
        public int sek_vyaz10 { get; set; }
        public int sek_vyaz12 { get; set; }
        public int sek_vyaz62 { get; set; }
        public int sek_vyaz71 { get; set; }
        public int sek_vyaz72 { get; set; }
        public int sek_vyazo { get; set; }
        public int sek_vyaz { get; set; }
        public int sek { get; set; }
        public int seb { get; set; }
        public int st { get; set; }
        public char po1 { get; set; }
        public char komment { get; set; }
        public DateTime data_sozd { get; set; }
        public int diz { get; set; }
        public int constr { get; set; }
        public DateTime data_obn { get; set; }
        public int sek_vyaz70 { get; set; }
        public int sek_kr { get; set; }
        public int slogn { get; set; }
        public int sek_vyaz14 { get; set; }
        public int arh { get; set; }
        public int sql_pr_add { get; set; }
        public DateTime date_add { get; set; }
        public char komp_name { get; set; }
        public DateTime annDateDel { get; set; }
        public char annCompDel { get; set; }
        [Key]
        public int annID { get; set; }
        public DateTime annDateAdd { get; set; }
        public char annCompAdd { get; set; }

    }
}
*/
using System;
using System.ComponentModel.DataAnnotations;
namespace SewingProduction
{
  //  public class ArtNormN { public char kod { get; set; } public char grup { get; set; } public char articul { get; set; } public char mod { get; set; } public char po { get; set; } public int sek_shv { get; set; } public int sek_vyaz3 { get; set; } public int sek_vyaz5 { get; set; } public int sek_vyaz6 { get; set; } public int sek_vyaz7 { get; set; } public int sek_vyaz10 { get; set; } public int sek_vyaz12 { get; set; } public int sek_vyaz62 { get; set; } public int sek_vyaz71 { get; set; } public int sek_vyaz72 { get; set; } public int sek_vyazo { get; set; } public int sek_vyaz { get; set; } public int sek { get; set; } public int seb { get; set; } public int st { get; set; } public char po1 { get; set; } public char komment { get; set; } public DateTime data_sozd { get; set; } public int diz { get; set; } public int constr { get; set; } public DateTime data_obn { get; set; } public int sek_vyaz70 { get; set; } public int sek_kr { get; set; } public int slogn { get; set; } public int sek_vyaz14 { get; set; } public int arh { get; set; } public int sql_pr_add { get; set; } public DateTime date_add { get; set; } public char komp_name { get; set; } public DateTime annDateDel { get; set; } public char annCompDel { get; set; } [Key] public int annID { get; set; } public DateTime annDateAdd { get; set; } public char annCompAdd { get; set; } }
}