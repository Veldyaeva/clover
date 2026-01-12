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
        public int TCDS_GlovalCode { get; set; }
        public string TCDS_1cCode { get; set; }
        public string TCDS_TCAT_ID { get; set; }
        public string TCDS_Name { get; set; }
        public int DeletionMark { get; set; }


    }
}
