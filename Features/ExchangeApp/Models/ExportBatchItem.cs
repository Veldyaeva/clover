using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace ExchangeApp.Models
{
    public sealed class ExportBatchItem
    {
        public long ExportBatchId { get; set; }
        public string BatchNo { get; set; }
        public string ExportTypeCode { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public int DocumentCount { get; set; }
        public int RowCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}