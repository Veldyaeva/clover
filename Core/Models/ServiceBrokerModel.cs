using DevExpress.Xpo.DB.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Core.Models
{
    public class ServiceBrokerModel
    {
        public class TableListenInfo
        {
            [NotMapped] public string ObjectName { get; set; } = "";
            [NotMapped] public string TableSchema { get; set; } = "";
            [NotMapped] public string TableName { get; set; } = "";
            [NotMapped] public string TableFieldList { get; set; } = "";
        }
    }
}
