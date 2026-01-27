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
    /// НЕ вызывает SqlDependency.Start/Stop (это делается один раз на процесс приложения).
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

        private SqlConnection? _connection;
        private SqlCommand? _command;
        private SqlDependency? _dependency;

        private bool _flagStartListening;
        private bool _brokerStopped;

        private string _fields = "*";
        private string _table = "";

        private int _onChangeGate = 0;

        public ServiceBroker(object owner)
        {
            // owner оставляем только чтобы не ломать текущие вызовы new ServiceBroker(this);
            // В тонкой версии брокер не знает о форме и не вызывает её напрямую.
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
            StopListening();
        }

        public void StartListening(string fields, string table)
        {
            if (string.IsNullOrWhiteSpace(table))
                return;

            _fields = string.IsNullOrWhiteSpace(fields) ? "*" : fields.Trim();
            _table = table.Trim();
            _flagStartListening = true;
            _brokerStopped = false;

            try
            {
                StopListening();

                var fullTable = BuildQuotedTableName(_table);
                var query = $"SELECT {_fields} FROM {fullTable}";

                _connection = new SqlConnection(_connectionString);
                _command = new SqlCommand(query, _connection)
                {
                    Notification = null
                };

                _dependency = new SqlDependency(_command);
                _dependency.OnChange += OnDependencyChange;

                _connection.Open();

                // обязательно выполнить команду — иначе QN не зарегистрируется
                using (var reader = _command.ExecuteReader(CommandBehavior.SingleResult))
                {
                    // no-op
                }

                Debug.WriteLine($"[ServiceBroker] Listening started: table={_table}, fields={_fields}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ServiceBroker] StartListening error: {ex}");
                StopListening();
            }
        }

        public bool StopListening()
        {
            try
            {
                if (_dependency != null)
                {
                    _dependency.OnChange -= OnDependencyChange;
                    _dependency = null;
                }

                _command?.Dispose();
                _command = null;

                if (_connection != null)
                {
                    try { _connection.Close(); } catch { }
                    _connection.Dispose();
                    _connection = null;
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ServiceBroker] StopListening error: {ex}");
                return false;
            }
        }

        //private async void OnDependencyChange(object sender, SqlNotificationEventArgs e)
        //{
        //    Debug.WriteLine($"[ServiceBroker] Notification: table={_table}, type={e.Type}, info={e.Info}, source={e.Source}");

        //    // защита от параллельных вызовов
        //    if (Interlocked.Exchange(ref _onChangeGate, 1) == 1)
        //        return;

        //    try
        //    {
        //        // QN одноразовые — снимаем текущую подписку
        //        StopListening();

        //        if (_brokerStopped)
        //            return;

        //        // SqlDependency НЕ отдаёт список колонок — передаём null
        //        await RaiseChangedAsync(_table, changedFieldsCsv: null);

        //        // переподписка
        //        if (_flagStartListening && !_brokerStopped)
        //            StartListening(_fields, _table);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"[ServiceBroker] OnDependencyChange error: {ex}");
        //        try
        //        {
        //            if (_flagStartListening && !_brokerStopped)
        //                StartListening(_fields, _table);
        //        }
        //        catch { }
        //    }
        //    finally
        //    {
        //        Interlocked.Exchange(ref _onChangeGate, 0);
        //    }
        //}


        private async void OnDependencyChange(object sender, SqlNotificationEventArgs e)
        {
            try
            {
                Debug.WriteLine($"[ServiceBroker] Notification: table={_table}, type={e.Type}, info={e.Info}, source={e.Source}");

                // 1) SqlDependency шлёт Subscribe/Query при установке подписки — это НЕ изменение данных
                if (e.Type != SqlNotificationType.Change)
                {
                    // просто переподписываемся и выходим
                   // ResubscribeSafe();
                    return;
                }
                if (e.Type != SqlNotificationType.Change)
                {
                    Debug.WriteLine($"[ServiceBroker] Ignore notification: type={e.Type}, info={e.Info}, source={e.Source}");
                    StartListening(_fields, _table); // если нужно переподписаться
                    return;
                }
                // 2) Для Change — фильтруем мусорные состояния
                // Обычно изменения: Insert/Update/Delete.
                // Invalid/Unknown — лучше переподписаться, но не дёргать UI.
                if (e.Info != SqlNotificationInfo.Insert &&
                    e.Info != SqlNotificationInfo.Update &&
                    e.Info != SqlNotificationInfo.Delete)
                {
                  //  ResubscribeSafe();
                    return;
                }
                // QN одноразовые — переподписка
                StopListening();
                if (!_brokerStopped && _flagStartListening)
                    StartListening(_fields, _table);

                // Вызов наружу (тонко!)
                await RaiseChangedAsync(_table, changedFieldsCsv: null);
               // Debug.WriteLine($"[ServiceBroker:{_id}] Notification: table=...");

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ServiceBroker] OnDependencyChange error: {ex}");
               // ResubscribeSafe();
            }
        }

        //private void ResubscribeSafe()
        //{
        //    try
        //    {
        //        // dependency одноразовый, пересоздаём listening
        //        StartListening(_fieldsCsv, _table);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"[ServiceBroker] ResubscribeSafe error: {ex.Message}");
        //    }
        //}

        //// Универсальный маршалинг в UI
        //private Task InvokeOnOwnerAsync(Func<Task> fn)
        //{
        //    if (_owner is Control c && c.IsHandleCreated)
        //    {
        //        if (c.InvokeRequired)
        //        {
        //            var tcs = new TaskCompletionSource<object?>();
        //            c.BeginInvoke(new Action(async () =>
        //            {
        //                try { await fn(); tcs.TrySetResult(null); }
        //                catch (Exception ex) { tcs.TrySetException(ex); }
        //            }));
        //            return tcs.Task;
        //        }
        //    }

        //    return fn();
        //}

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
