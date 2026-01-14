using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.UserDistribution.Models
{
    public class DistributionModel
    {
        public int DistributionID { get; set; }
        public int ParentID { get; set; }
        public int ChildID { get; set; }
        [NotMapped]
        public bool IsSelected { get; set; }
    }
}
