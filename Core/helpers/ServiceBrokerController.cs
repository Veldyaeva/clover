using SewingProduction.Core.Class.Settings;
using SewingProduction.Core.interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Core.helpers
{
    /// <summary>
    /// Композиционный контроллер ServiceBroker без базовой формы.
    /// Использование: форма (CustomForm) реализует IServiceBrokerHost и вызывает InitAsync.
    /// </summary>
    public sealed class ServiceBrokerController : IAsyncDisposable
    {
        private readonly IServiceBrokerHost _host;
        private readonly object _initLock = new();
        private bool _initialized;

        public ServiceBrokerHelper Helper { get; private set; }
        public EnhancedRefreshCoordinator Coordinator { get; private set; }

        public ServiceBrokerController(IServiceBrokerHost host)
        {
            _host = host ?? throw new ArgumentNullException(nameof(host));
        }

        public async Task InitAsync(CancellationToken ct)
        {
            lock (_initLock)
            {
                if (_initialized)
                    return;
                _initialized = true;
            }

            if (!ServiceBrokerSettings.Enabled)
                return;
            try
            {
                var sbSettings = SettingsManager.GetServiceBrokerSettings();
                Coordinator = new EnhancedRefreshCoordinator(
                    reloadByObjectNameAsync: async (obj) => await _host.RestartDataByObjectNameAsync(obj, ct).ConfigureAwait(false),
                    debounce: TimeSpan.FromMilliseconds(sbSettings.DebounceMs),
                    throttle: sbSettings.ThrottleMs > 0 ? TimeSpan.FromMilliseconds(sbSettings.ThrottleMs) : null,
                    maxWait: TimeSpan.FromMilliseconds(sbSettings.MaxWaitMs),
                    maxBatchSize: sbSettings.MaxBatchSize,
                    maxParallelReloads: sbSettings.MaxParallelReloads,
                    maxCascadeDepth: sbSettings.MaxCascadeDepth);

                foreach (var kv in _host.RefreshPriorities ?? new Dictionary<string, int>())
                    Coordinator.SetPriority(kv.Key, kv.Value);

                Helper = new ServiceBrokerHelper(
                    owner: _host,
                    loadByObjectAsync: (obj, token) => _host.LoadListenInfoByObjectNameAsync(obj, token))
                {
                    UseSchemaInListenName = _host.UseSchemaInListenName
                };

                if (_host.IgnoredTables != null)
                {
                    foreach (var t in _host.IgnoredTables.Where(x => !string.IsNullOrWhiteSpace(x)))
                    {
                        var tt = t.Trim();
                        Helper.IgnoredTables.Add(tt);
                        if (!tt.Contains('.'))
                            Helper.IgnoredTables.Add("dbo." + tt);
                    }
                }

                var objects = _host.ServiceBrokerObjects?
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList() ?? new List<string>();

                if (objects.Count == 0)
                {
                    Debug.WriteLine($"[{_host.ServiceBrokerFormName}] InitAsync called, but ServiceBrokerObjects is empty");
                    return;
                }

                await Helper.InitAndStartAsync(objects, ct).ConfigureAwait(false);
            }
            catch
            {
                lock (_initLock)
                {
                    _initialized = false;
                }
                throw;
            }
        }

        /// <summary>
        /// Базовая обработка обновления (table->objectName->координатор).
        /// Вызывайте из IDataUpdatableFormAsyncV2.UpdateDataInFormAsync.
        /// </summary>
        public async Task HandleUpdateAsync(string tableName, string fieldsChangedCsv)
        {
            if (Helper == null || Coordinator == null)
                return;

            await Helper.HandleBrokerUpdateAsync(tableName, fieldsChangedCsv);
            var affected = Helper.GetAffectedObjectsByTable(tableName);
            if (affected == null || affected.Count == 0)
                return;

            Coordinator.RequestBatch(affected);
            return;
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                if (Helper != null)
                    await Helper.DisposeAsync();
            }
            catch { /* ignore */ }

            try
            {
                Coordinator?.Dispose();
            }
            catch { /* ignore */ }
        }
    }
}
