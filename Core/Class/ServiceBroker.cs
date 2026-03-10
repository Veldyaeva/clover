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
                Debug.WriteLine(
                    $"[ServiceBroker] StartListening: table={_table}, fields={_fields}, query={query}");

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
                    $"[ServiceBroker] Listening started: table={_table}, fields={_fields}, state={_connection.State}");
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

        private static bool IsInvalidSubscription(SqlNotificationEventArgs e)
        {
            return e.Info == SqlNotificationInfo.Invalid
                || e.Info == SqlNotificationInfo.Options
                || e.Info == SqlNotificationInfo.Query
                || e.Info == SqlNotificationInfo.Isolation
                || e.Info == SqlNotificationInfo.TemplateLimit
                || e.Info == SqlNotificationInfo.Error;
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
