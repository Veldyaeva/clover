// ServiceBrokerDependencyHelper.cs
#nullable enable
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static SewingProduction.Core.Models.ServiceBrokerModel;

namespace SewingProduction.Core.helpers
{
    /// <summary>
    /// Результат: какой ObjectName (хранимка/объект) затронут, по какой таблице, и какие поля этот ObjectName использует.
    /// </summary>
    public sealed class ObjectTableFieldMatch
    {
        public string ObjectName { get; init; } = "";   // имя хранимки/объекта
        public string TableKey { get; init; } = "";     // "dbo.table"
        public IReadOnlyList<string> Fields { get; init; } = Array.Empty<string>();
        public DateTime DetectedAtUtc { get; init; }
    }

    /// <summary>
    /// Хелпер:
    /// - получает список ObjectName
    /// - по каждому ObjectName грузит List&lt;TableListenInfo&gt;
    /// - объединяет их в общий план (таблица -> union полей) и индекс (таблица -> objectName + fields)
    /// - запускает брокеры
    /// - при обновлении таблицы возвращает, какие objectName затронуты
    /// </summary>
    public sealed class ServiceBrokerDependencyHelper : IAsyncDisposable
    {
        private readonly object _owner;
        private readonly Func<string, CancellationToken, Task<List<TableListenInfo>>> _loadByObjectAsync;
        private readonly StringComparer _cmp;

        // objectName -> list(schema, table, fields)
        private readonly Dictionary<string, List<TableListenInfo>> _sourcesByObject;

        // tableKey -> list(objectName + fields)
        private readonly Dictionary<string, List<DependencyItem>> _depsByTable;

        // tableKey -> union(fields)
        private readonly Dictionary<string, HashSet<string>> _unionFieldsByTable;

        // tableKey -> broker
        private readonly Dictionary<string, ServiceBroker> _brokers;

        // pending matches for the form
        private readonly ConcurrentQueue<ObjectTableFieldMatch> _pending;

        private sealed class DependencyItem
        {
            public string ObjectName { get; init; } = "";
            public HashSet<string> Fields { get; init; } = new(StringComparer.OrdinalIgnoreCase);
        }

        public ServiceBrokerDependencyHelper(
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
        /// Главный метод: передаёшь список ObjectName, хелпер сам всё загрузит, объединит и запустит брокеры.
        /// </summary>
        public async Task InitAndStartAsync(IEnumerable<string> objectNames, CancellationToken ct)
        {
            if (objectNames == null) throw new ArgumentNullException(nameof(objectNames));

            // 1) загрузка зависимостей по каждому ObjectName
            _sourcesByObject.Clear();

            foreach (var obj in objectNames.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct(_cmp))
            {
                ct.ThrowIfCancellationRequested();

                var list = await _loadByObjectAsync(obj, ct).ConfigureAwait(false) ?? new List<TableListenInfo>();

                // на всякий случай: если ObjectName не заполнен в строках, проставим
                foreach (var row in list)
                {
                    if (string.IsNullOrWhiteSpace(row.ObjectName))
                        row.ObjectName = obj;
                }

                _sourcesByObject[obj] = list;
            }

            // 2) индекс + общий план
            RebuildIndex();

            // 3) запуск брокеров
            StartAllBrokers();
        }

        /// <summary>
        /// Перестроить индекс "таблица -> какие ObjectName/поля используют" и общий план (union полей по таблице).
        /// </summary>
        private void RebuildIndex()
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

                    // depsByTable
                    if (!_depsByTable.TryGetValue(tableKey, out var depList))
                    {
                        depList = new List<DependencyItem>();
                        _depsByTable[tableKey] = depList;
                    }

                    // если один objectName несколько раз встретился по одной таблице — объединяем поля
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

                    // unionFieldsByTable
                    if (!_unionFieldsByTable.TryGetValue(tableKey, out var union))
                    {
                        union = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                        _unionFieldsByTable[tableKey] = union;
                    }
                    foreach (var f in fields) union.Add(f);
                }
            }
        }

        /// <summary>
        /// Запускает ServiceBroker для всех таблиц из общего плана: tableKey -> union fields.
        /// </summary>
        private void StartAllBrokers()
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

                // ВАЖНО:
                // Если StartListening НЕ понимает "schema.table", поменяй на ExtractTableName(tableKey)
                var listenName = tableKey;

                broker.StartListening(columns, listenName);

                _brokers[tableKey] = broker;
            }
        }

        private static string ExtractTableName(string tableKey)
        {
            var idx = tableKey.LastIndexOf('.');
            return idx >= 0 ? tableKey[(idx + 1)..] : tableKey;
        }

        /// <summary>
        /// Вызывай из формы, когда ServiceBroker поймал изменение.
        /// tableFromBroker может быть "dbo.table" или "table".
        /// Хелпер положит в очередь совпадения по всем ObjectName, которые зависят от этой таблицы.
        /// </summary>
        public Task HandleBrokerUpdateAsync(string tableFromBroker)
        {
            if (string.IsNullOrWhiteSpace(tableFromBroker))
                return Task.CompletedTask;

            var incoming = tableFromBroker.Trim();
            var tableKey = incoming;

            // пришло без схемы (например "dop_ras") — найдём "dbo.dop_ras"
            if (!incoming.Contains('.'))
            {
                var match = _depsByTable.Keys.FirstOrDefault(k =>
                    k.EndsWith("." + incoming, StringComparison.OrdinalIgnoreCase));

                if (match != null)
                    tableKey = match;
            }

            if (_depsByTable.TryGetValue(tableKey, out var deps))
            {
                var now = DateTime.UtcNow;

                foreach (var d in deps)
                {
                    _pending.Enqueue(new ObjectTableFieldMatch
                    {
                        ObjectName = d.ObjectName,
                        TableKey = tableKey,
                        Fields = d.Fields.OrderBy(x => x).ToList(),
                        DetectedAtUtc = now
                    });
                }
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Забрать все накопленные совпадения и очистить очередь.
        /// Ты в форме просматриваешь этот список и решаешь, что обновлять.
        /// </summary>
        public List<ObjectTableFieldMatch> DrainPending()
        {
            var list = new List<ObjectTableFieldMatch>();
            while (_pending.TryDequeue(out var item))
                list.Add(item);
            return list;
        }

        /// <summary>
        /// Для отладки: какие таблицы реально слушаем.
        /// </summary>
        public IReadOnlyList<string> GetListeningTables()
            => _unionFieldsByTable.Keys.OrderBy(k => k).ToList();

        /// <summary>
        /// Для отладки: какие поля (union) слушаем по конкретной таблице.
        /// </summary>
        public IReadOnlyList<string> GetListeningFields(string tableKey)
        {
            if (_unionFieldsByTable.TryGetValue(tableKey, out var set))
                return set.OrderBy(x => x).ToList();
            return Array.Empty<string>();
        }

        public ValueTask DisposeAsync()
        {
            // Если у ServiceBroker есть Stop/Dispose — вызови здесь.
            _brokers.Clear();
            _sourcesByObject.Clear();
            _depsByTable.Clear();
            _unionFieldsByTable.Clear();
            while (_pending.TryDequeue(out _)) { }
            return ValueTask.CompletedTask;
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

//-----------------------------------------------------------
//    // ServiceBrokerHelper.cs
//    // Универсальный хелпер/менеджер для ServiceBroker, пригодный для использования из любой формы.
//    // - Загружает список таблиц (schema, name, fields) через делегат (подходит под Dapper/EF/ADO.NET)
//    // - Хранит брокеры по ключу "schema.table"
//    // - Регистрирует обработчики обновлений (вместо switch)
//    // - Умеет безопасно выполнять UI-обновления через переданный UI-dispatcher (WinForms/WPF/что угодно)
//    //
//    // ВАЖНО:
//    // 1) Если ваш ServiceBroker.StartListening(columns, table) НЕ понимает формат "schema.table",
//    //    то в StartAsync замените listenName = key на listenName = x.tableName.
//    // 2) Если у ServiceBroker есть Stop/Dispose — добавьте вызов в DisposeAsync.

//#nullable enable
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using static SewingProduction.Core.Models.ServiceBrokerModel;

//namespace SewingProduction.Core.helpers
//{
//    /// <summary>
//    /// Универсальный менеджер ServiceBroker'ов.
//    /// </summary>
//    public class ServiceBrokerManager : IAsyncDisposable
//    {
//        private readonly object _owner;
//        private readonly Func<CancellationToken, Task<List<TableListenInfo>>> _loadListAsync;
//        private readonly Func<Func<Task>, Task> _runOnUiAsync;

//        private readonly Dictionary<string, ServiceBroker> _brokers;
//        private readonly Dictionary<string, Func<Task>> _handlers;

//        /// <summary>Последний загруженный список из источника (SQL/хранимки).</summary>
//        public IReadOnlyList<TableListenInfo> LastLoaded => _lastLoaded;
//        private List<TableListenInfo> _lastLoaded = new();

//        public ServiceBrokerManager(
//            object owner,
//            Func<CancellationToken, Task<List<TableListenInfo>>> loadListAsync,
//            Func<Func<Task>, Task>? runOnUiAsync = null,
//            StringComparer? comparer = null)
//        {
//            _owner = owner ?? throw new ArgumentNullException(nameof(owner));
//            _loadListAsync = loadListAsync ?? throw new ArgumentNullException(nameof(loadListAsync));
//            _runOnUiAsync = runOnUiAsync ?? (f => f());

//            comparer ??= StringComparer.OrdinalIgnoreCase;
//            _brokers = new Dictionary<string, ServiceBroker>(comparer);
//            _handlers = new Dictionary<string, Func<Task>>(comparer);
//        }

//        /// <summary>Ключ для словарей/обработчиков: "schema.table".</summary>
//        public static string MakeKey(string schema, string table) => $"{schema}.{table}";
//        public static string MakeKey(TableListenInfo x) => MakeKey(x.TableSchema, x.TableName);

//        /// <summary>
//        /// Регистрирует обработчик обновлений для таблицы (ключ "schema.table").
//        /// </summary>
//        public void RegisterHandler(string tableKey, Func<Task> handler)
//        {
//            if (string.IsNullOrWhiteSpace(tableKey))
//                throw new ArgumentException("Table key is empty.", nameof(tableKey));

//            _handlers[tableKey] = handler ?? throw new ArgumentNullException(nameof(handler));
//        }

//        /// <summary>
//        /// Регистрирует обработчик, который должен выполняться в UI-потоке (например, изменение TextBox).
//        /// </summary>
//        public void RegisterUiHandler(string tableKey, Action uiAction)
//        {
//            if (uiAction is null) throw new ArgumentNullException(nameof(uiAction));

//            RegisterHandler(tableKey, () => _runOnUiAsync(() =>
//            {
//                uiAction();
//                return Task.CompletedTask;
//            }));
//        }

//        /// <summary>
//        /// Возвращает true, если брокер уже создан и запущен для указанного ключа "schema.table".
//        /// </summary>
//        public bool IsStarted(string tableKey) => _brokers.ContainsKey(tableKey);

//        /// <summary>
//        /// Загружает список таблиц (через loadListAsync) и запускает брокеры для новых таблиц.
//        /// </summary>
//        public async Task StartAsync(CancellationToken ct)
//        {
//            var list = await _loadListAsync(ct).ConfigureAwait(false) ?? new List<TableListenInfo>();

//            // фильтрация мусорных строк
//            list = list.Where(x =>
//                    !string.IsNullOrWhiteSpace(x.TableSchema) &&
//                    !string.IsNullOrWhiteSpace(x.TableName) &&
//                    !string.IsNullOrWhiteSpace(x.TableFieldList))
//                .ToList();

//            _lastLoaded = list;

//            foreach (var x in list)
//            {
//                var key = MakeKey(x);

//                if (_brokers.ContainsKey(key))
//                    continue;

//                var broker = new ServiceBroker(_owner);

//                // Если StartBroker нужно вызывать один раз на приложение — вынесите это наружу.
//                broker.StartBroker();

//                // ВАЖНО: если StartListening не понимает "schema.table", замените на x.tableName
//                var listenName = key;
//                broker.StartListening(x.TableFieldList, listenName);

//                _brokers[key] = broker;
//            }
//        }

//        /// <summary>
//        /// Обработка уведомления "таблица изменилась".
//        /// Поддерживает вход "schema.table" и "table" (без схемы).
//        /// </summary>
//        public async Task HandleUpdateAsync(string tableFromBroker)
//        {
//            if (string.IsNullOrWhiteSpace(tableFromBroker))
//                return;

//            // 1) пришло "schema.table"
//            if (_handlers.TryGetValue(tableFromBroker, out var handler))
//            {
//                await handler().ConfigureAwait(false);
//                return;
//            }

//            // 2) пришло только "table" — ищем обработчик по совпадению окончания
//            var matchKey = _handlers.Keys.FirstOrDefault(k =>
//                k.EndsWith("." + tableFromBroker, StringComparison.OrdinalIgnoreCase));

//            if (matchKey != null && _handlers.TryGetValue(matchKey, out handler))
//                await handler().ConfigureAwait(false);
//        }

//        /// <summary>
//        /// Дополнительно: можно назначить дефолтный обработчик для таблиц без явной регистрации.
//        /// </summary>
//        public Func<string, Task>? DefaultHandler { get; set; }

//        /// <summary>
//        /// Вариант HandleUpdateAsync с DefaultHandler (если не найден зарегистрированный обработчик).
//        /// </summary>
//        public async Task HandleUpdateWithDefaultAsync(string tableFromBroker)
//        {
//            if (string.IsNullOrWhiteSpace(tableFromBroker))
//                return;

//            if (_handlers.TryGetValue(tableFromBroker, out var handler))
//            {
//                await handler().ConfigureAwait(false);
//                return;
//            }

//            var matchKey = _handlers.Keys.FirstOrDefault(k =>
//                k.EndsWith("." + tableFromBroker, StringComparison.OrdinalIgnoreCase));

//            if (matchKey != null && _handlers.TryGetValue(matchKey, out handler))
//            {
//                await handler().ConfigureAwait(false);
//                return;
//            }

//            if (DefaultHandler != null)
//                await DefaultHandler(tableFromBroker).ConfigureAwait(false);
//        }

//        public ValueTask DisposeAsync()
//        {
//            // Если у ServiceBroker есть методы остановки/Dispose — вызовите здесь.
//            // Например:
//            // foreach (var b in _brokers.Values) b.Stop();
//            _brokers.Clear();
//            _handlers.Clear();
//            _lastLoaded.Clear();
//            return ValueTask.CompletedTask;
//        }
//    }

//    // -----------------------------
//    // UI dispatchers (опционально)
//    // -----------------------------
//    // WinForms:
//    //   var mgr = new ServiceBrokerManager(this, LoadAsync, ServiceBrokerUiDispatchers.WinForms(this));
//    //
//    // WPF:
//    //   var mgr = new ServiceBrokerManager(this, LoadAsync, ServiceBrokerUiDispatchers.Wpf(Application.Current.Dispatcher));
//    //
//    public static class ServiceBrokerUiDispatchers
//    {
//#if WINDOWS
//        // Если проект WinForms, подключите:
//        // using System.Windows.Forms;
//        // и раскомментируйте метод WinForms.

//        /*
//        public static Func<Func<Task>, Task> WinForms(Control control)
//        {
//            return f =>
//            {
//                if (control.IsDisposed) return Task.CompletedTask;

//                if (control.InvokeRequired)
//                {
//                    var tcs = new TaskCompletionSource();
//                    control.BeginInvoke(new Action(async () =>
//                    {
//                        try { await f().ConfigureAwait(false); tcs.SetResult(); }
//                        catch (Exception ex) { tcs.SetException(ex); }
//                    }));
//                    return tcs.Task;
//                }

//                return f();
//            };
//        }
//        */
//#endif

//        // WPF вариант (если нужен) — раскомментируйте и добавьте ссылку на PresentationCore/WindowsBase:
//        /*
//        public static Func<Func<Task>, Task> Wpf(System.Windows.Threading.Dispatcher dispatcher)
//        {
//            return async f =>
//            {
//                if (dispatcher.CheckAccess())
//                {
//                    await f().ConfigureAwait(false);
//                }
//                else
//                {
//                    await dispatcher.InvokeAsync(async () => await f().ConfigureAwait(false));
//                }
//            };
//        }
//        */
//    }
//}
