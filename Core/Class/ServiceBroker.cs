using SewingProduction.Core.Class.Settings;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.Core.helpers;

namespace SewingProduction
{
    /// <summary>
    /// Тонкий адаптер SqlDependency (Query Notifications).
    /// Глобальный SqlDependency.Start/Stop выполняется лениво (при первом StartListening)
    /// и один раз на процесс приложения.
    /// Делает только:
    /// - StartListening / StopListening
    /// - уведомление Changed при изменениях
    /// </summary>
    public sealed class ServiceBroker
    {
        public event Func<string, string?, Task>? Changed;

        private Task RaiseChangedAsync(string table, string? changedFieldsCsv)
            => Changed?.Invoke(table, changedFieldsCsv) ?? Task.CompletedTask;

        private readonly string _connectionString = SettingsManager.GetCurrentConnectionString();
        private readonly string _ownerName;
        private static readonly object _sqlDependencySync = new object();
        private static int _sqlDependencyStarted;
        private static string? _sqlDependencyConn;

        private SqlConnection? _connection;
        private SqlCommand? _command;
        private SqlDependency? _dependency;

        private bool _flagStartListening;
        private bool _brokerStopped;

        private string _fields = "*";
        private string _table = "";
        private DateTime _lastStartAttemptUtc = DateTime.MinValue;
        private int _startRetryScheduled;

        private int _onChangeGate = 0;

        public ServiceBroker(object owner)
        {
            // owner сохраняем для диагностики источника подписок.
            _ownerName = owner?.GetType().FullName ?? "<null>";
        }

        [Obsolete("SqlDependency.Start должен вызываться один раз на процесс (в Program/Main). Не вызывай это из форм/брокеров.")]
        public void StartBroker()
        {
            Debug.WriteLine("ServiceBroker.StartBroker() ignored. Use global SqlDependency.Start on app startup.");
        }

        public void StopBroker()
        {
            _brokerStopped = true;
            _flagStartListening = false;
            Interlocked.Exchange(ref _startRetryScheduled, 0);
            StopListening();
        }

        public void StartListening(string fields, string table)
        {
            if (string.IsNullOrWhiteSpace(table))
                return;

            var requestedFields = string.IsNullOrWhiteSpace(fields) ? "*" : fields.Trim();
            var requestedTable = table.Trim();

            // Защита от лишних повторных вызовов StartListening извне
            // (если уже подписаны на те же table/fields и соединение живое).
            if (_flagStartListening &&
                !_brokerStopped &&
                _dependency != null &&
                _connection != null &&
                _connection.State == ConnectionState.Open &&
                string.Equals(_fields, requestedFields, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(_table, requestedTable, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            // Минимальный интервал между попытками старта при нестабильной БД,
            // чтобы не разгонять STARTED_OUTBOUND в цикле.
            var nowUtc = DateTime.UtcNow;
            if ((nowUtc - _lastStartAttemptUtc) < TimeSpan.FromSeconds(10))
            {
                Debug.WriteLine(
                    $"[ServiceBroker] StartListening skipped by backoff: owner={_ownerName}, table={requestedTable}, fields={requestedFields}");
                return;
            }
            _lastStartAttemptUtc = nowUtc;

            _fields = requestedFields;
            _table = requestedTable;
            _flagStartListening = true;
            _brokerStopped = false;

            try
            {
                EnsureSqlDependencyStarted(_connectionString);
                StopListening();

                var fullTable = BuildQuotedTableName(_table);
                var query = $"SELECT {_fields} FROM {fullTable}";
                Debug.WriteLine(
                    $"[ServiceBroker] StartListening: owner={_ownerName}, table={_table}, fields={_fields}, query={query}");

                _connection = new SqlConnection(_connectionString);
                _command = new SqlCommand(query, _connection)
                {
                    Notification = null,
                    CommandTimeout = 120
                };

                _dependency = new SqlDependency(_command);
                _dependency.OnChange += OnDependencyChange;

                _connection.Open();

                // обязательно выполнить команду — иначе QN не зарегистрируется
                using (var reader = _command.ExecuteReader(CommandBehavior.SingleResult))
                {
                    // no-op
                }

                Debug.WriteLine(
                    $"[ServiceBroker] Listening started: owner={_ownerName}, table={_table}, fields={_fields}, state={_connection.State}");
                Interlocked.Exchange(ref _startRetryScheduled, 0);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ServiceBroker] StartListening error: owner={_ownerName}, table={_table}, fields={_fields}, error={ex}");
                StopListening();
                ScheduleStartRetry("start-error");
            }
        }
        private static void EnsureSqlDependencyStarted(string connectionString)
        {
            if (Interlocked.CompareExchange(ref _sqlDependencyStarted, 1, 1) == 1)
                return;

            lock (_sqlDependencySync)
            {
                if (_sqlDependencyStarted == 1)
                    return;

                SqlDependency.Start(connectionString);
                _sqlDependencyConn = connectionString;
                _sqlDependencyStarted = 1;
                Debug.WriteLine("[ServiceBroker] SqlDependency.Start initialized lazily.");

                AppDomain.CurrentDomain.ProcessExit += (_, __) => StopSqlDependencyGlobal();
                AppDomain.CurrentDomain.DomainUnload += (_, __) => StopSqlDependencyGlobal();
                Application.ApplicationExit += (_, __) => StopSqlDependencyGlobal();
            }
        }

        private static void StopSqlDependencyGlobal()
        {
            if (Interlocked.Exchange(ref _sqlDependencyStarted, 0) != 1)
                return;

            try
            {
                if (!string.IsNullOrWhiteSpace(_sqlDependencyConn))
                    SqlDependency.Stop(_sqlDependencyConn);
            }
            catch { }
            finally
            {
                _sqlDependencyConn = null;
            }
        }

        public bool StopListening()
        {
            try
            {
                //Debug.WriteLine($"[ServiceBroker] StopListening: table={_table}, fields={_fields}");
                // 1) Отписываемся от события ПЕРЕД обнулением dependency
                if (_dependency != null)
                {
                    _dependency.OnChange -= OnDependencyChange;
                    _dependency = null;
                }

                // 2) КРИТИЧНО: обнуляем Notification ПЕРЕД Dispose команды
                if (_command != null)
                {
                    _command.Notification = null; // ← ВАЖНО для предотвращения утечек
                    _command.Dispose();
                    _command = null;
                }

                // 3) Закрываем и освобождаем соединение
                if (_connection != null)
                {
                    try 
                    { 
                        if (_connection.State != ConnectionState.Closed)
                            _connection.Close(); 
                    } 
                    catch (Exception closeEx)
                    {
                        Debug.WriteLine($"[ServiceBroker] Error closing connection: {closeEx}");
                    }
                    
                    _connection.Dispose();
                    _connection = null;
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ServiceBroker] StopListening error: {ex}");
                // Гарантируем освобождение даже при ошибке
                try
                {
                    _command?.Dispose();
                    _command = null;
                    _connection?.Dispose();
                    _connection = null;
                    _dependency = null;
                }
                catch { }
                return false;
            }
        }

        private async void OnDependencyChange(object sender, SqlNotificationEventArgs e)
        {
            Debug.WriteLine(
                $"[ServiceBroker] Notification: table={_table}, fields={_fields}, type={e.Type}, info={e.Info}, source={e.Source}");
            // защита от параллельных вызовов
            if (Interlocked.Exchange(ref _onChangeGate, 1) == 1)
                return;

            try
            {
                // ВСЕГДА снимаем старую подписку (она одноразовая)
                StopListening();

                if (_brokerStopped)
                    return;

                // Если подписка невалидна (SQL options/query restrictions),
                // не запускаем бесконечный цикл мгновенных переподписок.
                if (IsInvalidSubscription(e))
                {
                    _flagStartListening = false;
                    _brokerStopped = true;
                    Debug.WriteLine(
                        $"[ServiceBroker] Subscription invalid. Listening stopped for table={_table}. " +
                        $"type={e.Type}, info={e.Info}, source={e.Source}. " +
                        "Fix SELECT/query notification prerequisites before restarting listening.");
                    return;
                }

                // ВСЕГДА переподписываемся, если слушание активно и подписка валидна
                if (_flagStartListening)
                    StartListening(_fields, _table);

                // UI/обновление — только если это реальное изменение данных
                if (e.Type != SqlNotificationType.Change)
                    return;

                var isDataChange =
                    e.Info == SqlNotificationInfo.Insert ||
                    e.Info == SqlNotificationInfo.Update ||
                    e.Info == SqlNotificationInfo.Delete ||
                    e.Info == SqlNotificationInfo.Merge;

                if (!isDataChange)
                    return;

                await RaiseChangedAsync(_table, changedFieldsCsv: null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ServiceBroker] OnDependencyChange error: {ex}");
                // на всякий случай попробуем переподписаться
                try
                {
                    StopListening();
                    if (!_brokerStopped && _flagStartListening)
                        StartListening(_fields, _table);
                }
                catch { }
            }
            finally
            {
                Interlocked.Exchange(ref _onChangeGate, 0);
            }
        }

        private void ScheduleStartRetry(string reason)
        {
            if (_brokerStopped || !_flagStartListening || string.IsNullOrWhiteSpace(_table))
                return;

            if (Interlocked.Exchange(ref _startRetryScheduled, 1) == 1)
                return;

            Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(5)).ConfigureAwait(false);
                    if (_brokerStopped || !_flagStartListening || string.IsNullOrWhiteSpace(_table))
                        return;

                    Debug.WriteLine($"[ServiceBroker] Retry StartListening: owner={_ownerName}, table={_table}, reason={reason}");
                    StartListening(_fields, _table);
                }
                catch { }
                finally
                {
                    Interlocked.Exchange(ref _startRetryScheduled, 0);
                }
            });
        }

        private static bool IsInvalidSubscription(SqlNotificationEventArgs e)
        {
            return e.Info == SqlNotificationInfo.Invalid
                || e.Info == SqlNotificationInfo.Options
                || e.Info == SqlNotificationInfo.Query
                || e.Info == SqlNotificationInfo.Isolation
                || e.Info == SqlNotificationInfo.TemplateLimit;
        }

        private static string BuildQuotedTableName(string tableOrSchemaTable)
        {
            var incoming = (tableOrSchemaTable ?? "").Trim();

            if (incoming.Contains("."))
            {
                var parts = incoming.Split('.');
                var schema = parts[0].Trim().Trim('[', ']');
                var table = parts[1].Trim().Trim('[', ']');
                return $"[{schema}].[{table}]";
            }

            var t = incoming.Trim().Trim('[', ']');
            return $"[dbo].[{t}]";
        }
    }
}
