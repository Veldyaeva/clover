using SewingProduction.Core.helpers;
using SewingProduction.Core.Models;
using SewingProduction.Core.Class.Settings;
using SewingProduction.Features.UserDistribution.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace SewingProduction
{
    /// <summary>
    /// Базовый класс (опциональный) для форм, которые хотят подключаться к Service Broker одинаково.
    ///
    /// Идея:
    ///  - в форме вы описываете:
    ///     1) какие SQL-объекты наблюдать (ServiceBrokerObjects)
    ///     2) как перезагружать данные по имени объекта (RestartDataByObjectNameAsync)
    ///     3) как получить таблицы/поля для объекта (LoadListenInfoByObjectNameAsync)
    ///  - базовый класс берёт на себя:
    ///     - wiring SqlDependency
    ///     - маппинг table -> objectNames
    ///     - антидребезг через EnhancedRefreshCoordinator
    ///     - корректный Dispose/Cancel
    /// </summary>
    public abstract class ServiceBrokerFormBase : CustomForm, IServiceBrokerForm
    {
        private static readonly IReadOnlyDictionary<string, ServiceBroker> EmptyBrokers =
            new Dictionary<string, ServiceBroker>(StringComparer.OrdinalIgnoreCase);

        private readonly object _sbInitLock = new();
        private bool _sbInitialized;

        protected CancellationTokenSource _sbCts;

        protected ServiceBrokerFormBase()
        {
            // важно: CTS создаём здесь, чтобы даже при раннем закрытии форма корректно гасила фоновые таски
            _sbCts = new CancellationTokenSource();
        }

        protected ServiceBrokerFormBase(UserClass user) : base(user)
        {
            // важно: CTS создаём здесь, чтобы даже при раннем закрытии форма корректно гасила фоновые таски
            _sbCts = new CancellationTokenSource();
        }

        public virtual string ServiceBrokerFormName => GetType().Name;

        public ServiceBrokerHelper ServiceBrokerHelper { get; protected set; }

        public IReadOnlyDictionary<string, ServiceBroker> ServiceBrokers =>
            ServiceBrokerHelper?.Brokers ?? EmptyBrokers;

        public EnhancedRefreshCoordinator RefreshCoordinator { get; protected set; }

        public CancellationToken ServiceBrokerToken => _sbCts?.Token ?? CancellationToken.None;

        /// <summary>
        /// Список логических объектов, которые форма хочет обновлять.
        /// Обычно сюда кладут имена процедур/вьюх, которые наполняют гриды.
        /// </summary>
        protected abstract IReadOnlyList<string> ServiceBrokerObjects { get; }

        /// <summary>
        /// Достаёт TableListenInfo для указанного объекта (обычно из БД: getSQLobj.sql).
        /// </summary>
        protected abstract Task<List<ServiceBrokerModel.TableListenInfo>> LoadListenInfoByObjectNameAsync(string objectName, CancellationToken ct);

        /// <summary>
        /// Перезагрузка конкретного объекта данных.
        /// ВАЖНО: вы можете вызывать UI-обновления внутри этого метода,
        /// но желательно делать "переключение" на UI-поток (Invoke/BeginInvoke) если нужно.
        /// </summary>
        public abstract Task RestartDataByObjectNameAsync(string objectName, CancellationToken ct);

        /// <summary>
        /// Переопределяйте, если для конкретной формы нужна иная приоритизация.
        /// </summary>
        protected virtual IReadOnlyDictionary<string, int> RefreshPriorities => new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Если true — логировать каждый входящий брокер-ивент (шумно).
        /// </summary>
        protected virtual bool VerboseBrokerLogging => true;

        /// <summary>
        /// Основная точка инициализации. Обычно вызывается из OnShown/Load.
        /// </summary>
        public virtual async Task InitServiceBrokerAsync(CancellationToken ct)
        {
            lock (_sbInitLock)
            {
                if (_sbInitialized) return;
                _sbInitialized = true;
            }

            // 1) coordinator (антидребезг)
            var sbSettings = SettingsManager.GetServiceBrokerSettings();
            RefreshCoordinator = new EnhancedRefreshCoordinator(
                reloadByObjectNameAsync: async (obj) => await RestartDataByObjectNameAsync(obj, ct).ConfigureAwait(false),
                debounce: TimeSpan.FromMilliseconds(sbSettings.DebounceMs),
                throttle: sbSettings.ThrottleMs > 0 ? TimeSpan.FromMilliseconds(sbSettings.ThrottleMs) : null,
                maxWait: TimeSpan.FromMilliseconds(sbSettings.MaxWaitMs),
                maxBatchSize: sbSettings.MaxBatchSize,
                maxParallelReloads: sbSettings.MaxParallelReloads,
                maxCascadeDepth: sbSettings.MaxCascadeDepth);

            foreach (var kv in RefreshPriorities)
                RefreshCoordinator.SetPriority(kv.Key, kv.Value);

            // 2) helper (SqlDependency)
            ServiceBrokerHelper = new ServiceBrokerHelper(
                owner: this,
                loadByObjectAsync: (obj, token) => LoadListenInfoByObjectNameAsync(obj, token));

            // 3) подписки
            var objects = ServiceBrokerObjects?.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct(StringComparer.OrdinalIgnoreCase).ToList()
                          ?? new List<string>();

            if (objects.Count == 0)
            {
                Debug.WriteLine($"[{ServiceBrokerFormName}] InitServiceBrokerAsync called, but ServiceBrokerObjects is empty");
                return;
            }

            await ServiceBrokerHelper.InitAndStartAsync(objects, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Базовая обработка "table changed":
        ///  - просим ServiceBrokerHelper вернуть список affected objectNames
        ///  - отдаём их в RefreshCoordinator, чтобы он схлопнул дребезг
        /// </summary>
        public virtual Task UpdateDataInFormAsync(string tableName, string fieldsChangedCsv)
        {
            return UpdateDataInFormAsyncInternal(tableName, fieldsChangedCsv);
        }

        public virtual Task UpdateDataInFormAsync(string tableName)
        {
            return UpdateDataInFormAsyncInternal(tableName, fieldsChangedCsv: "");
        }

        private Task UpdateDataInFormAsyncInternal(string tableName, string fieldsChangedCsv)
        {
            if (ServiceBrokerHelper == null || RefreshCoordinator == null)
                return Task.CompletedTask;

            if (VerboseBrokerLogging)
            {
                Debug.WriteLine($"[{ServiceBrokerFormName}] UpdateDataInFormAsync: table={tableName}, fields={fieldsChangedCsv}");
            }

            var affected = ServiceBrokerHelper.GetAffectedObjectsByTable(tableName);
            if (affected == null || affected.Count == 0)
                return Task.CompletedTask;

            // Используем RequestBatch для эффективной обработки нескольких объектов
            RefreshCoordinator.RequestBatch(affected);
            return Task.CompletedTask;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    _sbCts?.Cancel();
                }
                catch { /* ignore */ }

                try
                {
                    _sbCts?.Dispose();
                }
                catch { /* ignore */ }

                try
                {
                    ServiceBrokerHelper?.DisposeAsync().AsTask().Wait();
                }
                catch { /* ignore */ }

                try
                {
                    RefreshCoordinator?.Dispose();
                }
                catch { /* ignore */ }
            }

            base.Dispose(disposing);
        }
    }
}
