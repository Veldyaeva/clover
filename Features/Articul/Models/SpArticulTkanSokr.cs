using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Articul.Models
{
    public class SpArticulTkanSokr
    {
        public int Kod_t { get; set; }
        public string Tkan { get; set; }
        public string Tkb { get; set; }
        public int IsDifficult { get; set; }
        public decimal Difficult_koef { get; set; }
    }
}
