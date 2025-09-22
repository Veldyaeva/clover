using System;

namespace SewingProduction.Models
{
    public class ReestrFurnDeliveryBagView
    {

        public int rfdbID { get; set; }
        public DateTime? rfdbDate { get; set; }
        public int rfdbKolM { get; set; }
        public int rfdbKolBag { get; set; }
        public int rfdbDDID { get; set; }
        public int rfdbDTID { get; set; }
        public int rfdbDdcID { get; set; }
        public string ddName { get; set; }
        public DateTime dtDateTime { get; set; }
    }
}
