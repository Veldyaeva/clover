using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace ExchangeApp.Models
{
    public sealed class ExchangeDocumentItem
    {
        public bool IsSelected { get; set; }
        public long DocumentId { get; set; }
        public string SourceDocType { get; set; }
        public string SourceDocId { get; set; }
        public DateTime? SourceDocDate { get; set; }
        public int? SourceCompanyId { get; set; }
        public string LastLoadStatus { get; set; }
        public bool NeedsExport { get; set; }
        public bool NeedsReexport { get; set; }
    }
}