using System;

namespace SewingProduction.Models
{
    public class ReestrFurnShtr
    {

        public int rfshID { get; set; }
        public int rfshrfID { get; set; }
        public int rfshSNum { get; set; }
        public string rfshShtr { get; set; }
        public DateTime? rfshScan { get; set; }
        public string rfshType { get; set; }
        public DateTime? rfshScanPriem { get; set; }
        public string rfshShtrEAN { get; set; }
    }
}
