using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeApp.Models
{
    public sealed class ExportTypeItem
    {
        public short ExportTypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}