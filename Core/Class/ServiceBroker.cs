using SewingProduction.Core.Class.Settings;
using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SewingProduction.Core.helpers;

namespace SewingProduction
{
    /// <summary>
    /// Тонкий адаптер SqlDependency (Query Notifications).
    /// Глобальный SqlDependency.Start/Stop управляется в Program/Main.
    /// Делает только:
    /// - StartListening / StopListening
    /// - уведомление Changed при изменениях
    /// </summary>
    public sealed class ServiceBroker
    {
        public event Func<string, string?, Task>? Changed;

        private Task RaiseChangedAsync(string table, string? changedFieldsCsv)
            => Changed?.Invoke(table, changedFieldsCsv) ?? Task.CompletedTask;

        private readonly string _connectionString = BuildListenerConnectionString();
        private readonly string _ownerName;

        private SqlConnection? _connection;
        private SqlCommand? _command;
        private SqlDependency? _dependency;

        private bool _flagStartListening;
        private bool _brokerStopped;

        private string _fields = "*";
        private string _table = "";
        private DateTime _lastStartAttemptUtc = DateTime.MinValue;
        private int _startRetryScheduled;
        private int _restartScheduled;

        private int _onChangeGate = 0;

        private readonly SemaphoreSlim _listenSemaphore = new(1, 1);
        private volatile bool _isListening;
        private readonly Guid _brokerId = Guid.NewGuid();
        private static readonly ConcurrentDictionary<Guid, string> _connectionContext =
            new ConcurrentDictionary<Guid, string>();
        private string OwnerName => string.IsNullOrWhiteSpace(_ownerName) ? "<unknown>" : _ownerName;


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
            //if (string.IsNullOrWhiteSpace(table))
            //    return;

            //var requestedFields = string.IsNullOrWhiteSpace(fields) ? "*" : fields.Trim();
            //var requestedTable = table.Trim();

            //// Защита от лишних повторных вызовов StartListening извне
            //// (если уже подписаны на те же table/fields и соединение живое).
            //if (_flagStartListening &&
            //    !_brokerStopped &&
            //    _dependency != null &&
            //    _connection != null &&
            //    _connection.State == ConnectionState.Open &&
            //    string.Equals(_fields, requestedFields, StringComparison.OrdinalIgnoreCase) &&
            //    string.Equals(_table, requestedTable, StringComparison.OrdinalIgnoreCase))
            //{
            //    return;
            //}

            //// Минимальный интервал между попытками старта при нестабильной БД,
            //// чтобы не разгонять STARTED_OUTBOUND в цикле.
            //var nowUtc = DateTime.UtcNow;
            //if ((nowUtc - _lastStartAttemptUtc) < TimeSpan.FromSeconds(10))
            //{
            //    Debug.WriteLine(
            //        $"[ServiceBroker] StartListening skipped by backoff: owner={_ownerName}, table={requestedTable}, fields={requestedFields}");
            //    return;
            //}
            //_lastStartAttemptUtc = nowUtc;

            //_fields = requestedFields;
            //_table = requestedTable;
            //_flagStartListening = true;
            //_brokerStopped = false;

            //try
            //{
            //    EnsureSqlDependencyStarted(_connectionString);
            //    StopListening();

            //    var fullTable = BuildQuotedTableName(_table);
            //    var query = $"SELECT {_fields} FROM {fullTable}";
            //    Debug.WriteLine(
            //        $"[ServiceBroker] StartListening: owner={_ownerName}, table={_table}, fields={_fields}, query={query}");

            //    _connection = new SqlConnection(_connectionString);
            //    _command = new SqlCommand(query, _connection)
            //    {
            //        Notification = null,
            //        CommandTimeout = 120
            //    };

            //    _dependency = new SqlDependency(_command);
            //    _dependency.OnChange += OnDependencyChange;

            //    _connection.Open();

            //    // обязательно выполнить команду — иначе QN не зарегистрируется
            //    using (var reader = _command.ExecuteReader(CommandBehavior.SingleResult))
            //    {
            //        // no-op
            //    }

            //    Debug.WriteLine(
            //        $"[ServiceBroker] Listening started: owner={_ownerName}, table={_table}, fields={_fields}, state={_connection.State}");
            //    Interlocked.Exchange(ref _startRetryScheduled, 0);
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine($"[ServiceBroker] StartListening error: owner={_ownerName}, table={_table}, fields={_fields}, error={ex}");
            //    StopListening();
            //    ScheduleStartRetry("start-error");
            //}

            StartListeningAsync(fields, table).GetAwaiter().GetResult();
        }


        public async Task StartListeningAsync(string fields, string table, CancellationToken ct = default)
        {
            await _listenSemaphore.WaitAsync(ct).ConfigureAwait(false);
            try
            {
                if (_brokerStopped)
                    return;

                if (_isListening)
                {
                    Debug.WriteLine(
                        $"[ServiceBroker:{_brokerId}] StartListening skipped: already listening, owner={OwnerName}, table={table}");
                    return;
                }

                _fields = fields;
                _table = table;
                _flagStartListening = true;

                var query = $"SELECT {fields} FROM [{table.Replace(".", "].[")}]";

                Debug.WriteLine(
                    $"[ServiceBroker:{_brokerId}] StartListening: owner={OwnerName}, table={table}, fields={fields}, query={query}");

                _connection = new SqlConnection(_connectionString);
                await _connection.OpenAsync(ct).ConfigureAwait(false);

                Debug.WriteLine(
                    $"[ServiceBroker:{_brokerId}] Connection opened: owner={OwnerName}, table={table}, clientConnectionId={_connection.ClientConnectionId}");
                _connectionContext[_connection.ClientConnectionId] =
                    $"owner={OwnerName}, table={table}, brokerId={_brokerId}";

                _command = _connection.CreateCommand();
                _command.CommandText = query;
                _command.CommandType = CommandType.Text;
                _command.CommandTimeout = 120; // больше времени на регистрацию QN при нагрузке

                _dependency = new SqlDependency(_command);
                _dependency.OnChange -= OnDependencyChange;
                _dependency.OnChange += OnDependencyChange;

                using (var reader = await _command.ExecuteReaderAsync(ct).ConfigureAwait(false))
                {
                    while (await reader.ReadAsync(ct).ConfigureAwait(false))
                    {
                        // ничего не делаем — нам важно зарегистрировать подписку
                    }
                }

                _isListening = true;

                Debug.WriteLine(
                    $"[ServiceBroker:{_brokerId}] Listening started: owner={OwnerName}, table={table}, clientConnectionId={_connection.ClientConnectionId}, state={_connection.State}");
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(
                    $"[ServiceBroker:{_brokerId}][SQL-ERROR] owner={OwnerName}, table={table}, fields={fields}, " +
                    $"clientConnectionId={ex.ClientConnectionId}, number={ex.Number}, state={ex.State}, class={ex.Class}, " +
                    $"procedure={ex.Procedure}, message={ex.Message}");

                SafeStopListeningInternal();
                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"[ServiceBroker:{_brokerId}][ERROR] owner={OwnerName}, table={table}, fields={fields}, message={ex}");

                SafeStopListeningInternal();
                throw;
            }
            finally
            {
                _listenSemaphore.Release();
            }
        }

        public void StopListening()
        {
            try
            {
                _listenSemaphore.Wait();
                SafeStopListeningInternal();
            }
            finally
            {
                _listenSemaphore.Release();
            }
        }

        private void SafeStopListeningInternal()
        {
            var cid = _connection?.ClientConnectionId;
            try
            {
                if (_dependency != null)
                    _dependency.OnChange -= OnDependencyChange;
            }
            catch { }

            try
            {
                if (_command != null)
                    _command.Notification = null;
            }
            catch { }

            try
            {
                _command?.Dispose();
            }
            catch { }

            try
            {
                if (_connection != null && _connection.State != ConnectionState.Closed)
                    _connection.Close();
            }
            catch { }

            try
            {
                _connection?.Dispose();
            }
            catch { }

            Debug.WriteLine(
                $"[ServiceBroker:{_brokerId}] StopListening: owner={OwnerName}, table={_table}, clientConnectionId={cid}");
            if (cid.HasValue)
                _connectionContext.TryRemove(cid.Value, out _);

            _dependency = null;
            _command = null;
            _connection = null;
            _isListening = false;
        }

        public static bool TryGetConnectionContext(Guid clientConnectionId, out string context)
        {
            return _connectionContext.TryGetValue(clientConnectionId, out context);
        }

        public static string GetActiveConnectionContextsSnapshot(int maxItems = 20)
        {
            if (_connectionContext.IsEmpty)
                return "<empty>";

            var items = _connectionContext
                .Take(Math.Max(1, maxItems))
                .Select(kv => $"{kv.Key} => {kv.Value}")
                .ToArray();

            return string.Join(" || ", items);
        }
        //public bool StopListening()
        //{
        //    try
        //    {
        //        //Debug.WriteLine($"[ServiceBroker] StopListening: table={_table}, fields={_fields}");
        //        // 1) Отписываемся от события ПЕРЕД обнулением dependency
        //        if (_dependency != null)
        //        {
        //            _dependency.OnChange -= OnDependencyChange;
        //            _dependency = null;
        //        }

        //        // 2) КРИТИЧНО: обнуляем Notification ПЕРЕД Dispose команды
        //        if (_command != null)
        //        {
        //            _command.Notification = null; // ← ВАЖНО для предотвращения утечек
        //            _command.Dispose();
        //            _command = null;
        //        }

        //        // 3) Закрываем и освобождаем соединение
        //        if (_connection != null)
        //        {
        //            try 
        //            { 
        //                if (_connection.State != ConnectionState.Closed)
        //                    _connection.Close(); 
        //            } 
        //            catch (Exception closeEx)
        //            {
        //                Debug.WriteLine($"[ServiceBroker] Error closing connection: {closeEx}");
        //            }
                    
        //            _connection.Dispose();
        //            _connection = null;
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"[ServiceBroker] StopListening error: {ex}");
        //        // Гарантируем освобождение даже при ошибке
        //        try
        //        {
        //            _command?.Dispose();
        //            _command = null;
        //            _connection?.Dispose();
        //            _connection = null;
        //            _dependency = null;
        //        }
        //        catch { }
        //        return false;
        //    }
        //}

        private async void OnDependencyChange(object sender, SqlNotificationEventArgs e)
        {
            //Debug.WriteLine(
            //    $"[ServiceBroker] Notification: table={_table}, fields={_fields}, type={e.Type}, info={e.Info}, source={e.Source}");
            Debug.WriteLine(
        $"[ServiceBroker:{_brokerId}] Notification: owner={OwnerName}, table={_table}, fields={_fields}, " +
        $"type={e.Type}, info={e.Info}, source={e.Source}");

            // защита от параллельных вызовов
            if (Interlocked.Exchange(ref _onChangeGate, 1) == 1)
                return;

            try
            {
                // ВСЕГДА снимаем старую подписку (она одноразовая)
                StopListening();

                if (_brokerStopped)
                    return;

                if (IsInvalidSubscription(e))
                {
                    _flagStartListening = false;
                    _brokerStopped = true;
                    Debug.WriteLine(
                        $"[ServiceBroker:{_brokerId}] Subscription invalid. owner={OwnerName}, table={_table}, " +
                        $"type={e.Type}, info={e.Info}, source={e.Source}");
                    return;
                }

                ScheduleRestart(
                    reason: $"notification:{e.Type}/{e.Info}/{e.Source}",
                    delay: GetResubscribeDelay(e));

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
                 Debug.WriteLine(//$"[ServiceBroker] OnDependencyChange error: {ex}");
                $"[ServiceBroker:{_brokerId}] OnDependencyChange error: owner={OwnerName}, table={_table}, message={ex}");

                // на всякий случай попробуем переподписаться
                //try
                //{
                //    StopListening();
                //    if (!_brokerStopped && _flagStartListening)
                //        StartListening(_fields, _table);
                //}
                //catch { }
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
                    await Task.Delay(GetRetryDelay(reason)).ConfigureAwait(false);
                    if (_brokerStopped || !_flagStartListening || string.IsNullOrWhiteSpace(_table))
                        return;

                    Debug.WriteLine(
                        $"[ServiceBroker:{_brokerId}] Retry StartListening: owner={OwnerName}, table={_table}, reason={reason}");
                    StartListening(_fields, _table);
                }
                catch { }
                finally
                {
                    Interlocked.Exchange(ref _startRetryScheduled, 0);
                }
            });
        }

        private void ScheduleRestart(string reason, TimeSpan delay)
        {
            if (_brokerStopped || !_flagStartListening || string.IsNullOrWhiteSpace(_table))
                return;

            if (Interlocked.Exchange(ref _restartScheduled, 1) == 1)
                return;

            Task.Run(async () =>
            {
                try
                {
                    if (delay > TimeSpan.Zero)
                        await Task.Delay(delay).ConfigureAwait(false);

                    if (_brokerStopped || !_flagStartListening || string.IsNullOrWhiteSpace(_table))
                        return;

                    try
                    {
                        await StartListeningAsync(_fields, _table).ConfigureAwait(false);
                    }
                    catch (SqlException ex) when (ex.Number == -2 || ex.Number == 2714 || ex.Number == 0)
                    {
                        Debug.WriteLine(
                            $"[ServiceBroker:{_brokerId}] Restart deferred after SQL error: owner={OwnerName}, table={_table}, " +
                            $"number={ex.Number}, reason={reason}");

                        Interlocked.Exchange(ref _restartScheduled, 0);
                        ScheduleStartRetry($"restart-sql:{ex.Number}");
                        return;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(
                            $"[ServiceBroker:{_brokerId}] Restart failed: owner={OwnerName}, table={_table}, reason={reason}, error={ex.Message}");

                        Interlocked.Exchange(ref _restartScheduled, 0);
                        ScheduleStartRetry("restart-error");
                        return;
                    }
                }
                finally
                {
                    Interlocked.Exchange(ref _restartScheduled, 0);
                }
            });
        }

        private static TimeSpan GetResubscribeDelay(SqlNotificationEventArgs e)
        {
            if (e.Type != SqlNotificationType.Change)
                return TimeSpan.FromSeconds(4);

            if (e.Info == SqlNotificationInfo.Insert ||
                e.Info == SqlNotificationInfo.Update ||
                e.Info == SqlNotificationInfo.Delete ||
                e.Info == SqlNotificationInfo.Merge)
            {
                // SqlDependency notifications are one-shot, but SQL may still be finalizing
                // the previous registration immediately after a data change.
                return TimeSpan.FromSeconds(2);
            }

            return TimeSpan.FromSeconds(4);
        }

        private static TimeSpan GetRetryDelay(string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
                return TimeSpan.FromSeconds(8);

            if (reason.Contains("restart-sql:2714", StringComparison.OrdinalIgnoreCase))
                return TimeSpan.FromSeconds(20);

            if (reason.Contains("restart-sql:-2", StringComparison.OrdinalIgnoreCase))
                return TimeSpan.FromSeconds(15);

            if (reason.Contains("restart-sql:0", StringComparison.OrdinalIgnoreCase))
                return TimeSpan.FromSeconds(10);

            if (reason.Contains("restart-error", StringComparison.OrdinalIgnoreCase))
                return TimeSpan.FromSeconds(10);

            return TimeSpan.FromSeconds(8);
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

        private static string BuildListenerConnectionString()
        {
            var builder = new SqlConnectionStringBuilder(SettingsManager.GetCurrentConnectionString())
            {
                Pooling = false
            };

            if (string.IsNullOrWhiteSpace(builder.ApplicationName))
                builder.ApplicationName = "SewingProduction.ServiceBroker";
            else
                builder.ApplicationName = builder.ApplicationName + ".ServiceBroker";

            return builder.ConnectionString;
        }
    }
}
