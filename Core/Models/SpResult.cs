using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Core.Models
{
    public class SpResult
    {
        public int Error { get; set; } 
        public string MessageError { get; set; } 
        public bool IsOk => Error == 0;
    }
}
