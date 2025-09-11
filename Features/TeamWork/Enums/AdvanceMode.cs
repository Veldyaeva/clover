using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction
{
    /// <summary>
    /// 
    /// </summary>
    public enum Mode
    {
        /// <summary>
        /// Комплект
        /// </summary>
        Kit = 5,
        /// <summary>
        /// Дубль
        /// </summary>
        Clone = 4,
        /// <summary>
        ///  Редактирование
        /// </summary>
        Edit = 3,
        /// <summary>
        /// Архив+копия
        /// </summary>
        ArchAndCopy = 2,
        /// <summary>
        /// Предварительное
        /// </summary>
        NewWorkDivision = 1
    }
}
