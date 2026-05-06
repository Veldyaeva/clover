using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeApp.Models
{
    public sealed class CompanyItem
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        public override string ToString() => CompanyName;
    }
}