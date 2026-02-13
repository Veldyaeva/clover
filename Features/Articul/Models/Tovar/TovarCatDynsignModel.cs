using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Articul.Models
{
    public class TovarCatDynsignModel
    {
        public int TCDS_ID { get; set; }
        public int TCDS_TCAT_ID { get; set; }
        public string TCDS_Name { get; set; }

        public int TCDS_id_spr { get; set; }
        public string TCDS_ShortName { get; set; }
        public string TCDS_SostName { get; set; }


    }
}
