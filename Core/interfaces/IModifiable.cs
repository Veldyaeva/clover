using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Interfaces
{
    public interface IModifiable
    {
        /// <summary>
        /// Возвращает или устанавливает флаг, указывающий, была ли запись изменена.
        /// </summary>
        bool IsModified { get; set; }
    }
}
