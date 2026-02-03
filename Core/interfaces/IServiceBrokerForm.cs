using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SewingProduction.Core.interfaces;
using SewingProduction;

namespace SewingProduction.Core.helpers
{
    /// <summary>
    /// Контракт формы, подключённой к SQL Service Broker (SqlDependency).
    /// </summary>
    public interface IServiceBrokerForm : IDataUpdatableFormAsyncV2
    {
        /// <summary>Короткое имя формы для логирования/диагностики.</summary>
        string ServiceBrokerFormName { get; }

        /// <summary>Экземпляр хелпера, который управляет SqlDependency и маршрутизацией нотификаций.</summary>
        ServiceBrokerHelper ServiceBrokerHelper { get; }

        /// <summary>
        /// Коллекция ServiceBroker по таблицам (schema.table).
        /// </summary>
        IReadOnlyDictionary<string, ServiceBroker> ServiceBrokers { get; }

        /// <summary>Координатор, который коалесцирует (схлопывает) обновления и убирает дребезг.</summary>
        EnhancedRefreshCoordinator RefreshCoordinator { get; }

        /// <summary>Токен жизни формы/подписок. Отменяется при закрытии/Dispose.</summary>
        CancellationToken ServiceBrokerToken { get; }

        /// <summary>Инициализация подписок. Обычно вызывается из OnShown/Load.</summary>
        Task InitServiceBrokerAsync(CancellationToken ct);

        /// <summary>
        /// Адресное обновление данных по имени объекта.
        /// ObjectName — ваш логический ключ (обычно имя процедуры/набора данных).
        /// </summary>
        Task RestartDataByObjectNameAsync(string objectName, CancellationToken ct);
    }
}
