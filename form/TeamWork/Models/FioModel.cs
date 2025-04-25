using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Models
{
    /// <summary>
    /// Represents data for an employee (FIO).
    /// </summary>
    public class FioModel
    {
        /// <summary>
        /// Employee ID (Tabelnyy Nomer).
        /// </summary>
        public int Tab { get; set; }

        /// <summary>
        /// Employee Full Name.
        /// </summary>
        public string Fio { get; set; }
    }
} 