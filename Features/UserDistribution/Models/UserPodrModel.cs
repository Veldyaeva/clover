using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.UserDistribution.Models
{
    public class UserPodrModel
    {
        public int UserPodrID { get; set; }
        public int UserID { get; set; }
        public int PodrID { get; set; }         // ID подразделения
        public int PodrTableID { get; set; }    // ID таблицы (all_table_name.id_atn)
        public int CreatorID { get; set; }

        [NotMapped]
        public string Name { get; set; }
        [NotMapped]
        public string PodrTableName { get; set; }
        [NotMapped]
        public bool IsSelected { get; set; }
    }
    public class RolePodrModel
    {
        public int RolePodrID { get; set; }
        public int RoleID { get; set; }
        public int PodrTableID { get; set; }
        [NotMapped]
        public string PodrTableName { get; set; }
    }
}
