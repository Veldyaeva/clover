// ServiceBrokerHelper.cs
// Итоговый универсальный хелпер для формы:
// - Ты передаёшь список ObjectName (хранимки/SQL-объекты)
// - Хелпер сам по каждому ObjectName вызывает твой GetObjectListForServiceBroker(objectName, ct)
// - Строит общий план прослушивания: TableKey (schema.table) -> UNION полей
// - Строит индекс зависимостей: TableKey -> ObjectName -> поля, которые использует этот ObjectName
// - При событии от брокера (table + changedFieldsCSV) фильтрует зависимости по пересечению полей
// - Складывает результаты в очередь, форма забирает DrainPending() и решает, что обновлять
//
// ВАЖНО:
// 1) Этот файл НЕ содержит объявление TableListenInfo (ты сказала, модель в отдельном файле).
//    Хелпер предполагает, что в проекте есть класс TableListenInfo с полями:
//    ObjectName, TableSchema, TableName, TableFieldList.
// 2) Если твой ServiceBroker.StartListening(columns, table) НЕ понимает "schema.table",
//    включи UseSchemaInListenName = false (по умолчанию true).
#nullable enable
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        public string TableKey { get; init; } = "";     // "dbo.table"

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
            try
            {
                if (objectNames == null) throw new ArgumentNullException(nameof(objectNames));

                _sourcesByObject.Clear();

                foreach (var obj in objectNames
                             .Where(x => !string.IsNullOrWhiteSpace(x))
                             .Select(x => x.Trim())
                             .Distinct(_cmp))
                {
                    ct.ThrowIfCancellationRequested();

                    List<TableListenInfo> list;
                    try
                    {
                        if (_loadByObjectAsync is null)
                            throw new InvalidOperationException("_loadByObjectAsync == null. Делегат не передан в конструктор ServiceBrokerHelper.");

                        if (obj is null)
                            throw new InvalidOperationException("obj == null (не должен быть null после фильтрации).");

                        // чтобы увидеть, ЧТО за делегат реально лежит внутри:
                        var m = _loadByObjectAsync.Method;
                        var target = _loadByObjectAsync.Target;
                        System.Diagnostics.Debug.WriteLine($"LOAD: method={m.DeclaringType?.FullName}.{m.Name}, target={target?.GetType().FullName ?? "<static>"}");

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
                    RebuildIndex();
                    StartAllBrokers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка ServiceBrokerHelper.InitAndStartAsync: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Вызов при событии от брокера:
        /// tableFromBroker: "dbo.table" или "table"
        /// changedFieldsCsv: "a,b,c" (может содержать несколько полей)
        /// </summary>
        public Task HandleBrokerUpdateAsync(string tableFromBroker, string? changedFieldsCsv)
        {
            var changed = ParseFields(changedFieldsCsv ?? "");
            return HandleBrokerUpdateAsync(tableFromBroker, changed);
        }

        /// <summary>
        /// Вызов при событии от брокера:
        /// tableFromBroker: "dbo.table" или "table"
        /// changedFields: перечисление изменённых полей (может быть несколько)
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
                // Если брокер не прислал поля (редко, но бывает) — считаем, что влияет на всё
                List<string> matched;
                if (!hasChangedFields)
                {
                    matched = d.Fields.OrderBy(x => x).ToList();
                }
                else
                {
                    matched = d.Fields.Where(changedSet.Contains).OrderBy(x => x).ToList();
                    if (matched.Count == 0)
                        continue; // изменились поля, которые этот ObjectName не использует
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

        /// <summary>
        /// Забрать все накопленные совпадения и очистить очередь.
        /// </summary>
        //public List<ObjectTableFieldMatch> DrainPending()
        //{
        //    var list = new List<ObjectTableFieldMatch>();
        //    while (_pending.TryDequeue(out var item))
        //        list.Add(item);
        //    return list;
        //}
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

        public ValueTask DisposeAsync()
        {
            // Если у ServiceBroker есть Stop/Dispose — вызови здесь.
            //if (_brokers != null)
            //    _brokers.Clear();
            //_sourcesByObject.Clear();
            //_depsByTable.Clear();
            //_unionFieldsByTable.Clear();
            //while (_pending.TryDequeue(out _)) { }
            //return ValueTask.CompletedTask;
            // ВАЖНО: не трогаем _pending, не надо его вычищать.

            if (System.Threading.Interlocked.Exchange(ref _disposed, 1) == 1)
                return ValueTask.CompletedTask;

            if (_brokers != null)
                _brokers?.Clear();
            _sourcesByObject.Clear();
            _depsByTable.Clear();
            _unionFieldsByTable.Clear();

            // безопасная очистка: ограничим итерации
            for (int i = 0; i < 100000; i++)
            {
                if (!_pending.TryDequeue(out _))
                    break;
            }

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
                        var fields = ParseFields(fieldCsv);

                        // depsByTable: tableKey -> objectName -> used fields
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

                        // unionFieldsByTable: tableKey -> union(fields)
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
            try
            {
                foreach (var kvp in _unionFieldsByTable)
                {
                    var tableKey = kvp.Key;
                    var unionFields = kvp.Value;

                    if (_brokers.ContainsKey(tableKey))
                        continue;

                    var broker = new ServiceBroker(_owner);
                    broker.StartBroker();

                    var columns = string.Join(",", unionFields);

                    var listenName = UseSchemaInListenName ? tableKey : ExtractTableName(tableKey);

                    broker.StartListening(columns, listenName);

                    _brokers[tableKey] = broker;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка ServiceBrokerHelper.StartAllBrokers: {ex.Message}");
            }
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
    }
}
