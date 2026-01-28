using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Articul.Models
{
    public class TovarCategoryModel
    {
        public int TCAT_ID { get; set; }
        public int TCAT_GlovalCode { get; set; }
        public string TCAT_1cCode { get; set; }
        public string TCAT_TG_ID { get; set; }
        public string TCAT_CategoryName { get; set; }
        public int TCAT_NotToAccountInCompleteness { get; set; }
        public int TCAT_OldTK { get; set; }
        public int DeletionMark { get; set; }


    }
}
