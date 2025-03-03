using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Models
{
    public class ArtNormN
    {
        public string Kod { get; set; }
        public string Group { get; set; }
        public string Articul { get; set; }
        public string Mod { get; set; }
        public int SekShv { get; set; }
        public int SekVyaz5 { get; set; }
        public int SekVyaz6 { get; set; }
        public int SekVyaz7 { get; set; }
        public int SekVyaz10 { get; set; }
        public int SekVyaz12 { get; set; }
        public int SekVyazo { get; set; }
        public int SekVyaz { get; set; }
        public int Sek { get; set; }
        public string Komment { get; set; }
        public DateTime? DataSozd { get; set; }
        public int Diz { get; set; }
        public int Constr { get; set; }
        public DateTime? DataObn { get; set; } // Поле может быть NULL
        public int SekKr { get; set; }
        public int Slogn { get; set; }
        public int AnnID { get; set; } // Первичный ключ
        public int Status { get; set; }
        public string StatusText { get; set; }
        public bool Arh { get; set; } // Архив (BIT в БД)
    }

}
