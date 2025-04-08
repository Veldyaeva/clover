using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Models
{
    public class ArtNormN
    {
        public string Kod { get; set; }
        [Column("grup")]
        public string Group { get; set; }
        public string Articul { get; set; }
        public string Mod { get; set; }
        [Column("sek_shv")]
        public int SekShv { get; set; }
        [Column("sek_vyaz5")]
        public int SekVyaz5 { get; set; }
        [Column("sek_vyaz6")]
        public int SekVyaz6 { get; set; }
        [Column("sek_vyaz7")]
        public int SekVyaz7 { get; set; }
        [Column("sek_vyaz10")]
        public int SekVyaz10 { get; set; }
        [Column("sek_vyaz12")]
        public int SekVyaz12 { get; set; }
        [Column("sek_vyazo")]
        public int SekVyazo { get; set; }
        [Column("sek_vyaz")]
        public int SekVyaz { get; set; }
        public int Sek { get; set; }
        public string Komment { get; set; }
        [Column("data_sozd")]
        public DateTime? dateCreate { get; set; }
        public int Diz { get; set; }
        public int Constr { get; set; }
        [Column("data_obn")]
        public DateTime? dataUpdate { get; set; } // Поле может быть NULL
        [Column("sek_kr")]
        public int SekKr { get; set; }
        public int Slogn { get; set; }
        public int AnnID { get; set; } // Первичный ключ
        public int Status { get; set; }
        [NotMapped]
        public bool preArch { get; set; }//предварительный архив
        [NotMapped]
        public string StatusText { get; set; }
        [NotMapped]
        public bool Arh { get; set; } // 
    }

}
