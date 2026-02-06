using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SewingProduction.Core.Models;

namespace SewingProduction.Core.interfaces
{
    /// <summary>
    /// Контракт для подключения ServiceBroker через композицию (без базовой формы).
    /// </summary>
    public interface IServiceBrokerHost : IDataUpdatableFormAsyncV2
    {
        /// <summary>Короткое имя формы для логирования/диагностики.</summary>
        string ServiceBrokerFormName { get; }

        /// <summary>Список логических объектов (процедуры/вьюхи), которые нужно слушать.</summary>
        IReadOnlyList<string> ServiceBrokerObjects { get; }

        /// <summary>Загрузка списка таблиц/полей для objectName.</summary>
        Task<List<ServiceBrokerModel.TableListenInfo>> LoadListenInfoByObjectNameAsync(
            string objectName,
            CancellationToken ct);

        /// <summary>Перезапуск данных по objectName.</summary>
        Task RestartDataByObjectNameAsync(string objectName, CancellationToken ct);

        /// <summary>Приоритеты обновления объектов (чем больше, тем выше).</summary>
        IReadOnlyDictionary<string, int> RefreshPriorities { get; }

        /// <summary>Если true — слушаем "schema.table", иначе только "table".</summary>
        bool UseSchemaInListenName { get; }

        /// <summary>Таблицы, которые нужно игнорировать (schema.table или table).</summary>
        IReadOnlyCollection<string> IgnoredTables { get; }
    }
}
