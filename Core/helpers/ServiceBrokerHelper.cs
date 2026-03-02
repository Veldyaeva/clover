// ServiceBrokerHelper.cs
// Универсальный хелпер для формы:
// - Ты передаёшь список ObjectName (хранимки/SQL-объекты)
// - Хелпер сам по каждому ObjectName вызывает твой GetObjectListForServiceBroker(objectName, ct)
// - Строит общий план прослушивания: TableKey (schema.table) -> UNION полей
// - Строит индекс зависимостей: TableKey -> ObjectName -> поля, которые использует этот ObjectName
// - При событии от брокера (table + changedFieldsCSV) фильтрует зависимости по пересечению полей
// - Складывает результаты в очередь, форма забирает DrainPending() и решает, что обновлять
//
// ВАЖНО:
// 1) Этот файл НЕ содержит объявление TableListenInfo (модель в отдельном файле).
//    Ожидаемые поля: ObjectName, TableSchema, TableName, TableFieldList.
// 2) Если твой ServiceBroker.StartListening(columns, table) НЕ понимает "schema.table",
//    включи UseSchemaInListenName = false (по умолчанию true).
#nullable enable
using Microsoft.IdentityModel.Tokens;
using SewingProduction.Core.interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SewingProduction.Core.Models.ServiceBrokerModel;

namespace SewingProduction.Core.helpers
{
    /// <summary>
    /// Результат: какой ObjectName затронут, по какой таблице, и какие поля совпали.
    /// </summary>
    public sealed class ObjectTableFieldMatch
    {
        public string ObjectName { get; init; } = "";   // хранимка/объект
        public string TableKey   { get; init; } = "";   // "dbo.table"

        // Поля, которые этот ObjectName использует (из TableFieldList)
        public IReadOnlyList<string> UsedFields { get; init; } = Array.Empty<string>();

        // Поля, которые реально изменились (пришли от брокера)
        public IReadOnlyList<string> ChangedFields { get; init; } = Array.Empty<string>();

        // Пересечение UsedFields и ChangedFields (если пусто - match не добавляем)
        public IReadOnlyList<string> MatchedFields { get; init; } = Array.Empty<string>();

        public DateTime DetectedAtUtc { get; init; }
    }

    public sealed class ServiceBrokerHelper : IAsyncDisposable
    {
        private readonly object _owner;
        private readonly Func<string, CancellationToken, Task<List<TableListenInfo>>> _loadByObjectAsync;
        private readonly StringComparer _cmp;

        // objectName -> list rows
        private readonly Dictionary<string, List<TableListenInfo>> _sourcesByObject;

        // tableKey -> list(objectName + used fields)
        private readonly Dictionary<string, List<DependencyItem>> _depsByTable;

        // tableKey -> union(fields)
        private readonly Dictionary<string, HashSet<string>> _unionFieldsByTable;

        // tableKey -> broker
        private readonly Dictionary<string, ServiceBroker> _brokers;

        // pending matches
        private readonly ConcurrentQueue<ObjectTableFieldMatch> _pending;
        private CancellationTokenRegistration _stopReg;
        private int _disposeState = 0; // 0=not disposed, 1=disposing/disposed
        private int _disposed; // 0 = не disposed, 1 = disposed

        private sealed class DependencyItem
        {
            public string ObjectName { get; init; } = "";
            public HashSet<string> Fields { get; init; } = new(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Если true: StartListening получает "schema.table".
        /// Если false: StartListening получает только "table".
        /// </summary>
        public bool UseSchemaInListenName { get; set; } = true;

        /// <summary>
        /// Доступ к активным брокерам по таблицам (schema.table).
        /// </summary>
        public IReadOnlyDictionary<string, ServiceBroker> Brokers => _brokers;

        /// <summary>
        /// Таблицы, которые игнорируем (полное имя schema.table).
        /// Полезно для LEFT JOIN-справочников, изменения которых не должны триггерить обновления.
        /// </summary>
        public HashSet<string> IgnoredTables { get; } = new(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Если список не пустой — слушаем ТОЛЬКО эти таблицы (schema.table).
        /// Удобно для жёсткого allow-list.
        /// </summary>
        public HashSet<string> AllowedTables { get; } = new(StringComparer.OrdinalIgnoreCase);

        public ServiceBrokerHelper(
            object owner,
            Func<string, CancellationToken, Task<List<TableListenInfo>>> loadByObjectAsync,
            StringComparer? comparer = null)
        {
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));
            _loadByObjectAsync = loadByObjectAsync ?? throw new ArgumentNullException(nameof(loadByObjectAsync));
            _cmp = comparer ?? StringComparer.OrdinalIgnoreCase;

            _sourcesByObject = new Dictionary<string, List<TableListenInfo>>(_cmp);
            _depsByTable = new Dictionary<string, List<DependencyItem>>(_cmp);
            _unionFieldsByTable = new Dictionary<string, HashSet<string>>(_cmp);
            _brokers = new Dictionary<string, ServiceBroker>(_cmp);
            _pending = new ConcurrentQueue<ObjectTableFieldMatch>();
        }

        public static string MakeTableKey(string schema, string table) => $"{schema}.{table}";

        /// <summary>
        /// Инициализация и запуск:
        /// - загрузит зависимости по каждому ObjectName
        /// - построит индекс
        /// - запустит брокеры на общий (объединенный) список таблиц/полей
        /// </summary>
        public async Task InitAndStartAsync(IEnumerable<string> objectNames, CancellationToken ct)
        {
            if (objectNames == null) throw new ArgumentNullException(nameof(objectNames));

            _sourcesByObject.Clear();

            var objs = objectNames
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .Distinct(_cmp)
                .ToList();

            foreach (var obj in objs)
            {
                ct.ThrowIfCancellationRequested();

                List<TableListenInfo> list;
                try
                {
                    var m = _loadByObjectAsync.Method;
                    var target = _loadByObjectAsync.Target;
                    Debug.WriteLine($"LOAD: method={m.DeclaringType?.FullName}.{m.Name}, target={target?.GetType().FullName ?? "<static>"}");

                    list = await _loadByObjectAsync(obj, ct).ConfigureAwait(false) ?? new List<TableListenInfo>();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(
                        $"Ошибка при получении списка таблиц/полей для ObjectName='{obj}'. " +
                        $"Проверь GetObjectListForServiceBroker и SQL-объект.",
                        ex);
                }

                // На всякий случай: если ObjectName не заполнен в строках — заполним.
                foreach (var row in list)
                {
                    if (string.IsNullOrWhiteSpace(row.ObjectName))
                        row.ObjectName = obj;
                }

                _sourcesByObject[obj] = list;
            }

            RebuildIndex();
            StartAllBrokers();

            // Регистрируем остановку всех брокеров при отмене токена
            // ct.Register(() => StopAllBrokers());
            _stopReg = ct.Register(() => StopAllBrokers());
        }
        private Task Broker_Changed(string table, string? fieldsCsv)
        {
            // если _owner — форма/контрол:
            if (_owner is Control c && c.InvokeRequired)
            {
                var tcs = new TaskCompletionSource<object?>();
                c.BeginInvoke(new Action(async () =>
                {
                    try
                    {
                        await DispatchToFormAsync(table, fieldsCsv);
                        tcs.TrySetResult(null);
                    }
                    catch (Exception ex) { tcs.TrySetException(ex); }
                }));
                return tcs.Task;
            }

            return DispatchToFormAsync(table, fieldsCsv);
        }

        private Task DispatchToFormAsync(string table, string? fieldsCsv)
        {
            if (_owner is IDataUpdatableFormAsyncV2 v2)
                return v2.UpdateDataInFormAsync(table, fieldsCsv);
            if (_owner is IDataUpdatableFormAsync v1)
                return v1.UpdateDataInFormAsync(table);
            if (_owner is IDataUpdatableForm sync)
            {
                sync.UpdateDataInForm(table);
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
        
        /// <summary>
        /// Вызов при событии от брокера:
        /// tableFromBroker: "dbo.table" или "table"
        /// changedFieldsCsv: "a,b,c"
        /// </summary>
        public Task HandleBrokerUpdateAsync(string tableFromBroker, string? changedFieldsCsv)
        {
            // подавление “самих себя”
            if (IsMutedTable(tableFromBroker))
                return Task.CompletedTask;

            var changed = ParseFields(changedFieldsCsv ?? "");
            return HandleBrokerUpdateAsync(tableFromBroker, changed);
        }

        /// <summary>
        /// Вызов при событии от брокера:
        /// tableFromBroker: "dbo.table" или "table"
        /// changedFields: перечисление изменённых полей
        /// </summary>
        public Task HandleBrokerUpdateAsync(string tableFromBroker, IEnumerable<string> changedFields)
        {
            if (string.IsNullOrWhiteSpace(tableFromBroker))
                return Task.CompletedTask;

            var incoming = tableFromBroker.Trim();
            var tableKey = incoming;

            // Если пришло без схемы (например "dop_ras") — найдём полное "dbo.dop_ras"
            if (!incoming.Contains('.'))
            {
                var match = _depsByTable.Keys.FirstOrDefault(k =>
                    k.EndsWith("." + incoming, StringComparison.OrdinalIgnoreCase));

                if (match != null)
                    tableKey = match;
            }

            // если таблица в ignore — выходим максимально рано
            if (IgnoredTables.Contains(tableKey))
                return Task.CompletedTask;

            // если allow-list задан — работаем только по нему
            if (AllowedTables.Count > 0 && !AllowedTables.Contains(tableKey))
                return Task.CompletedTask;

            if (!_depsByTable.TryGetValue(tableKey, out var deps))
                return Task.CompletedTask;

            var changedSet = (changedFields ?? Array.Empty<string>())
                .Select(x => (x ?? "").Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var hasChangedFields = changedSet.Count > 0;
            var now = DateTime.UtcNow;

            foreach (var d in deps)
            {
                // Если брокер не прислал поля — считаем, что влияет на всё
                List<string> matched;
                if (!hasChangedFields)
                {
                    matched = d.Fields.OrderBy(x => x).ToList();
                }
                else
                {
                    matched = d.Fields.Where(changedSet.Contains).OrderBy(x => x).ToList();
                    if (matched.Count == 0)
                        continue;
                }

                _pending.Enqueue(new ObjectTableFieldMatch
                {
                    ObjectName = d.ObjectName,
                    TableKey = tableKey,
                    UsedFields = d.Fields.OrderBy(x => x).ToList(),
                    ChangedFields = hasChangedFields ? changedSet.OrderBy(x => x).ToList() : Array.Empty<string>(),
                    MatchedFields = matched,
                    DetectedAtUtc = now
                });
            }

            return Task.CompletedTask;
        }

        /// <summary>Забрать список ObjectName, которые нужно перезапустить.</summary>
        public List<string> DrainPending()
        {
            try
            {
                var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                while (_pending.TryDequeue(out var item))
                {
                    if (!string.IsNullOrWhiteSpace(item.ObjectName))
                        result.Add(item.ObjectName);
                }

                return result.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка ServiceBrokerHelper.InitAndStartAsync: {ex.Message}");
                return new List<string>();
            }
        }

        /// <summary>Список union-полей, которые слушаем по таблице (для дебага).</summary>
        public List<string> GetUnionListeningFieldsForTable(string tableFromBroker)
        {
            if (string.IsNullOrWhiteSpace(tableFromBroker))
                return new List<string>();

            var incoming = tableFromBroker.Trim();
            var tableKey = incoming;

            // если брокер прислал только имя таблицы (без схемы)
            if (!incoming.Contains('.'))
            {
                var match = _unionFieldsByTable.Keys.FirstOrDefault(k =>
                    k.EndsWith("." + incoming, StringComparison.OrdinalIgnoreCase));

                if (match != null)
                    tableKey = match;
            }

            if (_unionFieldsByTable.TryGetValue(tableKey, out var fields))
                return fields.OrderBy(x => x).ToList();

            return new List<string>();
        }
        /// <summary>Для отладки: какие таблицы реально слушаем.</summary>
        public IReadOnlyList<string> GetListeningTables()
            => _unionFieldsByTable.Keys.OrderBy(k => k).ToList();

        /// <summary>Для отладки: какие union-поля слушаем по таблице.</summary>
        public IReadOnlyList<string> GetListeningFields(string tableKey)
        {
            if (_unionFieldsByTable.TryGetValue(tableKey, out var set))
                return set.OrderBy(x => x).ToList();

            return Array.Empty<string>();
        }

        /// <summary>
        /// Получает список затронутых объектов (ObjectName) по имени таблицы.
        /// </summary>
        public IReadOnlyList<string> GetAffectedObjectsByTable(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                return Array.Empty<string>();

            var incoming = tableName.Trim();
            var tableKey = incoming;

            // Если пришло без схемы — найдём полное "schema.table"
            if (!incoming.Contains('.'))
            {
                var match = _depsByTable.Keys.FirstOrDefault(k =>
                    k.EndsWith("." + incoming, StringComparison.OrdinalIgnoreCase));

                if (match != null)
                    tableKey = match;
            }

            if (!_depsByTable.TryGetValue(tableKey, out var deps))
                return Array.Empty<string>();

            return deps.Select(d => d.ObjectName)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        public ValueTask DisposeAsync()
        {
            if (System.Threading.Interlocked.Exchange(ref _disposeState, 1) != 0)
                return ValueTask.CompletedTask;
            if (_brokers == null || _brokers.Count == 0)
                return ValueTask.CompletedTask;

            try { _stopReg.Dispose(); } catch { }

            foreach (var broker in _brokers.Values)
            {
                try
                {
                    broker.Changed -= Broker_Changed;
                    broker.StopBroker(); // локально: StopListening + запрет переподписки
                }
                catch { }
            }

            _brokers.Clear();
            _sourcesByObject.Clear();
            _depsByTable.Clear();
            _unionFieldsByTable.Clear();
            while (_pending.TryDequeue(out _)) { }

            return ValueTask.CompletedTask;

        }

        // ----------------- internal -----------------

        private void RebuildIndex()
        {
            try
            {
            _depsByTable.Clear();
            _unionFieldsByTable.Clear();

            foreach (var kvp in _sourcesByObject)
            {
                var defaultObjectName = kvp.Key;
                var list = kvp.Value ?? new List<TableListenInfo>();

                foreach (var x in list)
                {
                    var objectName = !string.IsNullOrWhiteSpace(x.ObjectName) ? x.ObjectName.Trim() : defaultObjectName;

                    var schema = (x.TableSchema ?? "").Trim();
                    var table = (x.TableName ?? "").Trim();
                    var fieldCsv = (x.TableFieldList ?? "").Trim();

                    if (string.IsNullOrWhiteSpace(objectName) ||
                        string.IsNullOrWhiteSpace(schema) ||
                        string.IsNullOrWhiteSpace(table) ||
                        string.IsNullOrWhiteSpace(fieldCsv))
                        continue;

                    var tableKey = MakeTableKey(schema, table);

                    // ФИЛЬТРЫ
                    if (IgnoredTables.Contains(tableKey))
                        continue;

                    if (AllowedTables.Count > 0 && !AllowedTables.Contains(tableKey))
                        continue;

                    var fields = ParseFields(fieldCsv);

                    if (!_depsByTable.TryGetValue(tableKey, out var depList))
                    {
                        depList = new List<DependencyItem>();
                        _depsByTable[tableKey] = depList;
                    }

                    var dep = depList.FirstOrDefault(d => _cmp.Equals(d.ObjectName, objectName));
                    if (dep == null)
                    {
                        dep = new DependencyItem
                        {
                            ObjectName = objectName,
                            Fields = new HashSet<string>(fields, StringComparer.OrdinalIgnoreCase)
                        };
                        depList.Add(dep);
                    }
                    else
                    {
                        foreach (var f in fields) dep.Fields.Add(f);
                    }

                    if (!_unionFieldsByTable.TryGetValue(tableKey, out var union))
                    {
                        union = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                        _unionFieldsByTable[tableKey] = union;
                    }
                    foreach (var f in fields) union.Add(f);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка ServiceBrokerHelper.RebuildIndex: {ex.Message}");
            }
        }

        private void StartAllBrokers()
        {
            foreach (var kvp in _unionFieldsByTable)
            {
                var tableKey = kvp.Key;
                var unionFields = kvp.Value;

                if (_brokers.ContainsKey(tableKey))
                    continue;

                var broker = new ServiceBroker(_owner);

                // ВАЖНО: подписка на событие
                broker.Changed += Broker_Changed;

                var columns = string.Join(",", unionFields);
                var listenName = UseSchemaInListenName ? tableKey : ExtractTableName(tableKey);

                //Debug.WriteLine($"[ServiceBrokerHelper] Start broker: tableKey={tableKey}, listenName={listenName}, columns={columns}");
                broker.StartListening(columns, listenName);
                _brokers[tableKey] = broker;
            }
        }

        /// <summary>
        /// Останавливает все активные брокеры. Вызывается автоматически при отмене CancellationToken.
        /// </summary>
        private void StopAllBrokers()
        {
            if (_brokers == null || _brokers.Count == 0)
                return;

            Debug.WriteLine($"[ServiceBrokerHelper] Stopping {_brokers.Count} brokers due to cancellation");

            foreach (var broker in _brokers.Values)
            {
                try
                {
                    Debug.WriteLine("[ServiceBrokerHelper] Stop broker");
                    broker.Changed -= Broker_Changed;
                    broker.StopBroker();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[ServiceBrokerHelper] Error stopping broker: {ex}");
                }
            }

            _brokers.Clear();
        }

        private static string ExtractTableName(string tableKey)
        {
            var idx = tableKey.LastIndexOf('.');
            return idx >= 0 ? tableKey[(idx + 1)..] : tableKey;
        }

        private static IEnumerable<string> ParseFields(string csv)
        {
            return (csv ?? "")
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrWhiteSpace(s));
        }
        private readonly ConcurrentDictionary<string, DateTime> _muteUntilUtc = new(StringComparer.OrdinalIgnoreCase);

        public void MuteTable(string tableKey, TimeSpan duration)
        {
            if (string.IsNullOrWhiteSpace(tableKey)) return;
            _muteUntilUtc[NormalizeKey(tableKey)] = DateTime.UtcNow.Add(duration);
        }

        public bool IsMutedTable(string tableKey)
        {
            if (string.IsNullOrWhiteSpace(tableKey)) return false;

            var key = NormalizeKey(tableKey);
            if (!_muteUntilUtc.TryGetValue(key, out var until))
                return false;

            if (DateTime.UtcNow <= until)
                return true;

            _muteUntilUtc.TryRemove(key, out _);
            return false;
        }

        private static string NormalizeKey(string s)
        {
            s = s.Trim();
            // поддержим dbo.table и table
            return s.Contains('.') ? s : "dbo." + s;
        }

    }
}
