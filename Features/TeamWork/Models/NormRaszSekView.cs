using System.ComponentModel.DataAnnotations.Schema;

namespace SewingProduction.Models
{
    // Эта модель не является таблицей, она только для чтения данных из View
    [Table("NormRaszSek_view")]
    public class NormRaszSekView
    {
        public int annId { get; set; }
        public int sek_O { get; set; }
        public int sek_5 { get; set; }
        public int sek_12 { get; set; }
        public int sek_7 { get; set; }
        public int sek_10 { get; set; }
        public int sek_6 { get; set; }
        public int sek_3 { get; set; }
        public int sek_70 { get; set; }
        public int sek_71 { get; set; }
        public int sek_72 { get; set; }
        public int sek_62 { get; set; }
        public int sek_14 { get; set; }
        public int sek_57 { get; set; }
        public int sek_18 { get; set; }
        public int sek_shv { get; set; }
        public int sek_sh1 { get; set; }
        public int sek_kr { get; set; }
        public decimal sb { get; set; } 
        public int sk { get; set; }
    }
}
