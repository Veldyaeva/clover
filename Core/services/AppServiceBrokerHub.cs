using SewingProduction.Core.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Core.services
{
    public sealed class AppServiceBrokerHub : IAppServiceBrokerHub
    {
        private readonly SemaphoreSlim _gate = new(1, 1);
        private readonly Dictionary<string, OwnerSubscription> _owners = new(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, TableListenerState> _listeners = new(StringComparer.OrdinalIgnoreCase);

        public async Task SubscribeAsync(
            string ownerId,
            string ownerName,
            IReadOnlyDictionary<string, IReadOnlyCollection<string>> tableFields,
            Func<string, string?, Task> onTableChangedAsync,
            CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(ownerId))
                throw new ArgumentException("ownerId is required", nameof(ownerId));
            if (onTableChangedAsync == null)
                throw new ArgumentNullException(nameof(onTableChangedAsync));

            var normalized = NormalizeTableFields(tableFields);

            await _gate.WaitAsync(ct).ConfigureAwait(false);
            try
            {
                UnsubscribeCore(ownerId);

                var reg = ct.Register(() =>
                {
                    try { _ = UnsubscribeAsync(ownerId); } catch { }
                });

                _owners[ownerId] = new OwnerSubscription(
                    ownerId: ownerId,
                    ownerName: string.IsNullOrWhiteSpace(ownerName) ? ownerId : ownerName,
                    tableFields: normalized,
                    onTableChangedAsync: onTableChangedAsync,
                    cancelRegistration: reg);

                var touchedTables = normalized.Keys.ToList();
                foreach (var tableKey in touchedTables)
                    ReconcileTableListenerCore(tableKey);

                System.Diagnostics.Debug.WriteLine(
                    $"[AppServiceBrokerHub] Subscribe: owner={ownerId}, tables={touchedTables.Count}");
            }
            finally
            {
                _gate.Release();
            }
        }

        public async Task UnsubscribeAsync(string ownerId)
        {
            if (string.IsNullOrWhiteSpace(ownerId))
                return;

            await _gate.WaitAsync().ConfigureAwait(false);
            try
            {
                UnsubscribeCore(ownerId);
            }
            finally
            {
                _gate.Release();
            }
        }

        public string GetDiagnosticsSnapshot()
        {
            try
            {
                _gate.Wait();
                var owners = _owners.Count;
                var listeners = _listeners.Count;
                var tables = string.Join(", ", _listeners.Keys.OrderBy(x => x));
                return $"owners={owners}, listeners={listeners}, tables=[{tables}]";
            }
            finally
            {
                _gate.Release();
            }
        }

        private void UnsubscribeCore(string ownerId)
        {
            if (!_owners.TryGetValue(ownerId, out var sub))
                return;

            _owners.Remove(ownerId);
            try { sub.CancelRegistration.Dispose(); } catch { }

            foreach (var tableKey in sub.TableFields.Keys)
                ReconcileTableListenerCore(tableKey);

            System.Diagnostics.Debug.WriteLine(
                $"[AppServiceBrokerHub] Unsubscribe: owner={ownerId}");
        }

        private void ReconcileTableListenerCore(string tableKey)
        {
            var subscribers = _owners.Values
                .Where(x => x.TableFields.ContainsKey(tableKey))
                .ToList();

            if (subscribers.Count == 0)
            {
                if (_listeners.TryGetValue(tableKey, out var stale))
                {
                    try { stale.Broker.Changed -= stale.Handler; } catch { }
                    try { stale.Broker.StopBroker(); } catch { }
                    _listeners.Remove(tableKey);
                    System.Diagnostics.Debug.WriteLine($"[AppServiceBrokerHub] Listener stopped: table={tableKey}");
                }
                return;
            }

            var mergedFields = subscribers
                .SelectMany(s => s.TableFields[tableKey])
                .Where(f => !string.IsNullOrWhiteSpace(f))
                .Select(f => f.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(f => f, StringComparer.OrdinalIgnoreCase)
                .ToArray();

            var fieldsCsv = mergedFields.Length > 0 ? string.Join(",", mergedFields) : "*";
            if (_listeners.TryGetValue(tableKey, out var existing) &&
                string.Equals(existing.FieldsCsv, fieldsCsv, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (existing != null)
            {
                try { existing.Broker.Changed -= existing.Handler; } catch { }
                try { existing.Broker.StopBroker(); } catch { }
            }

            var broker = new ServiceBroker(this);
            Func<string, string?, Task> handler = (tableFromBroker, fieldsCsvFromBroker) =>
                OnTableChangedAsync(tableKey, tableFromBroker, fieldsCsvFromBroker);

            broker.Changed += handler;
            broker.StartListening(fieldsCsv, tableKey);

            _listeners[tableKey] = new TableListenerState(broker, handler, fieldsCsv);
            System.Diagnostics.Debug.WriteLine(
                $"[AppServiceBrokerHub] Listener started: table={tableKey}, fields={fieldsCsv}, subscribers={subscribers.Count}");
        }

        private async Task OnTableChangedAsync(string tableKey, string tableFromBroker, string? fieldsChangedCsv)
        {
            List<OwnerSubscription> targets;
            await _gate.WaitAsync().ConfigureAwait(false);
            try
            {
                targets = _owners.Values
                    .Where(s => s.TableFields.ContainsKey(tableKey))
                    .ToList();
            }
            finally
            {
                _gate.Release();
            }

            System.Diagnostics.Debug.WriteLine(
                $"[AppServiceBrokerHub] Dispatch: table={tableKey}, subscribers={targets.Count}");

            foreach (var target in targets)
            {
                try
                {
                    await target.OnTableChangedAsync(tableFromBroker, fieldsChangedCsv).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"[AppServiceBrokerHub] Dispatch error: owner={target.OwnerId}, table={tableKey}, error={ex.Message}");
                }
            }
        }

        private static Dictionary<string, IReadOnlyCollection<string>> NormalizeTableFields(
            IReadOnlyDictionary<string, IReadOnlyCollection<string>> tableFields)
        {
            var result = new Dictionary<string, IReadOnlyCollection<string>>(StringComparer.OrdinalIgnoreCase);
            if (tableFields == null)
                return result;

            foreach (var kv in tableFields)
            {
                var tableKey = NormalizeTableKey(kv.Key);
                if (string.IsNullOrWhiteSpace(tableKey))
                    continue;

                var fields = (kv.Value ?? Array.Empty<string>())
                    .Where(f => !string.IsNullOrWhiteSpace(f))
                    .Select(f => f.Trim())
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToArray();

                result[tableKey] = fields;
            }

            return result;
        }

        private static string NormalizeTableKey(string incoming)
        {
            if (string.IsNullOrWhiteSpace(incoming))
                return string.Empty;

            var s = incoming.Trim().Trim('[', ']');
            if (s.Contains("."))
            {
                var parts = s.Split('.');
                if (parts.Length >= 2)
                    return $"{parts[0].Trim().Trim('[', ']')}.{parts[1].Trim().Trim('[', ']')}";
            }

            return $"dbo.{s}";
        }

        private sealed class OwnerSubscription
        {
            public OwnerSubscription(
                string ownerId,
                string ownerName,
                IReadOnlyDictionary<string, IReadOnlyCollection<string>> tableFields,
                Func<string, string?, Task> onTableChangedAsync,
                CancellationTokenRegistration cancelRegistration)
            {
                OwnerId = ownerId;
                OwnerName = ownerName;
                TableFields = tableFields;
                OnTableChangedAsync = onTableChangedAsync;
                CancelRegistration = cancelRegistration;
            }

            public string OwnerId { get; }
            public string OwnerName { get; }
            public IReadOnlyDictionary<string, IReadOnlyCollection<string>> TableFields { get; }
            public Func<string, string?, Task> OnTableChangedAsync { get; }
            public CancellationTokenRegistration CancelRegistration { get; }
        }

        private sealed class TableListenerState
        {
            public TableListenerState(ServiceBroker broker, Func<string, string?, Task> handler, string fieldsCsv)
            {
                Broker = broker;
                Handler = handler;
                FieldsCsv = fieldsCsv;
            }

            public ServiceBroker Broker { get; }
            public Func<string, string?, Task> Handler { get; }
            public string FieldsCsv { get; }
        }
    }
}
