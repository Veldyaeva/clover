using System.Collections.Generic;

namespace SewingProduction.Core.Services.DataCleanup
{
    public interface IDataCleanupService
    {
        /// <summary>
        /// Обрезает пробелы у всех строковых свойств объекта
        /// </summary>
        T CleanupStringFields<T>(T entity) where T : class;

        /// <summary>
        /// Очищает коллекцию сущностей
        /// </summary>
        IEnumerable<T> CleanupCollection<T>(IEnumerable<T> entities) where T : class;
    }
}
