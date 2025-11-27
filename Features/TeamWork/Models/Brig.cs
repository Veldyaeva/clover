using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.TeamWork.Models
{
    public class Brig
    {
        [Column("dost_zeh")]
        public string dost_zeh { get; set; }

        [Column("id_brig")]
        public int id_brig { get; set; }
    }

}

