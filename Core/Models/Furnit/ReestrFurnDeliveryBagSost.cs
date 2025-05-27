using DevExpress.Data.Linq.Helpers;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraScheduler.Native;
using SewingProduction.form;
using SewingProduction.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace SewingProduction.Models
{
    public class ReestrFurnDeliveryBagSost
    {

        public int rfdbsID { get; set; }
        public int rfdbsrfdbID { get; set; }
        public int rfdbsNumber { get; set; }
        public string rfdbsShtr { get; set; }
        public DateTime? rfdbsScan { get; set; }
        public DateTime rfdbsScanPriem { get; set; }
    }
}
