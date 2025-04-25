using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Models
{
    /// <summary>
    ///Список ФИО
    /// </summary>
    public class FioModel
    {
        /// <summary>
        /// Табельный номер
        /// </summary>
        //public int tab { get; set; }

        ///// <summary>
        /////ФИО
        ///// </summary>
        //public string fio { get; set; }
        [Column("tab")]
        public int Tab { get; set; }

        [Column("fio")]
        public string Fio { get; set; }
    }
} 