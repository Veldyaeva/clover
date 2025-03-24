using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction
{
    /// <summary>
    /// Статус РТ
    /// </summary>
    public enum Status
    {
        /// <summary>
        ///  Предварительный архив
        /// </summary>
        PreliminaryArchive = 4, 
        /// <summary>
        /// Архив
        /// </summary>
        Archive = 3, 
        /// <summary>
        /// Актуальное
        /// </summary>
        Actual = 2,       
        /// <summary>
        /// Предварительное
        /// </summary>
        Preliminary = 1          
    }
}

