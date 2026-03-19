# ServiceBroker: шаблон для новой формы

Ниже шаблон подключения ServiceBroker для новой WinForms-формы через `ServiceBrokerController` и `IServiceBrokerHost`.

## 1) Каркас формы

```csharp
using SewingProduction.Core.Class;
using SewingProduction.Core.helpers;
using SewingProduction.Core.interfaces;
using SewingProduction.Core.Models;
using SewingProduction.Features.UserDistribution.Class;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.Example.Forms
{
    public partial class ExampleForm : CustomForm, IServiceBrokerHost
    {
        private readonly ServiceBrokerController _sbController;
        private ServiceBrokerService _sbService;

        private CancellationTokenSource _lifetimeCts;
        private int _serviceBrokerShutdownStarted;

        private readonly HashSet<string> _ignoredServiceBrokerTables =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                // "sysdiagrams"
            };

        private readonly Dictionary<string, Func<Task>> _objectRestartMap =
            new Dictionary<string, Func<Task>>(StringComparer.OrdinalIgnoreCase);

        public ExampleForm(UserClass user) : base(user)
        {
            InitializeComponent();
            _sbController = new ServiceBrokerController(this);
            this.Load += ExampleForm_Load;
            this.FormClosing += ExampleForm_FormClosing;
        }

        // IServiceBrokerHost
        public string ServiceBrokerFormName => GetType().Name;
        public bool UseSchemaInListenName => true;
        public IReadOnlyCollection<string> IgnoredTables => _ignoredServiceBrokerTables;
        public IReadOnlyDictionary<string, int> RefreshPriorities =>
            new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                { "GetExampleMain", 10 },
                { "GetExampleDetails", 5 }
            };
        public IReadOnlyList<string> ServiceBrokerObjects =>
            _objectRestartMap.Keys.ToArray();

        private async void ExampleForm_Load(object sender, EventArgs e)
        {
            try
            {
                _lifetimeCts = new CancellationTokenSource();
                InitObjectRestartMap();

                await InitServiceBrokerAsync(_lifetimeCts.Token);
                await ReloadInitialDataAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ExampleForm] Load error: {ex}");
                MessageBox.Show($"Ошибка загрузки формы: {ex.Message}");
            }
        }

        private void InitObjectRestartMap()
        {
            _objectRestartMap["GetExampleMain"] = async () =>
            {
                await ReloadMainAsync();
            };

            _objectRestartMap["GetExampleDetails"] = async () =>
            {
                await ReloadDetailsAsync();
            };
        }

        public async Task InitServiceBrokerAsync(CancellationToken ct)
        {
            Debug.WriteLine($"[ExampleForm] InitServiceBrokerAsync start: objects={string.Join(", ", ServiceBrokerObjects)}");
            await _sbController.InitAsync(ct);

            var tables = _sbController.Helper?.GetListeningTables() ?? Array.Empty<string>();
            Debug.WriteLine($"[ExampleForm] Listening tables: {string.Join(", ", tables)}");
        }

        public async Task<List<ServiceBrokerModel.TableListenInfo>> LoadListenInfoByObjectNameAsync(
            string objectName,
            CancellationToken ct)
        {
            if (_sbService == null)
            {
                // Важно: используйте ту же БД, что и в рабочих формах SB.
                var dbHelper = new DatabaseHelper("ace");
                _sbService = new ServiceBrokerService(dbHelper);
            }

            var list = await _sbService.GetObjectListForServiceBroker(objectName, ct).ConfigureAwait(false);
            Debug.WriteLine($"[ExampleForm] LoadListenInfoByObjectNameAsync: object={objectName}, rows={list?.Count ?? 0}");

            return list;
        }

        public async Task RestartDataByObjectNameAsync(string objectName, CancellationToken ct)
        {
            await InvokeOnUiAsync(async () =>
            {
                if (string.IsNullOrWhiteSpace(objectName))
                    return;

                if (_objectRestartMap.TryGetValue(objectName, out var action))
                    await action().ConfigureAwait(true);
            }).ConfigureAwait(false);
        }

        public async Task UpdateDataInFormAsync(string table, string? changedFields)
        {
            await _sbController.HandleUpdateAsync(table, changedFields ?? string.Empty).ConfigureAwait(false);
        }

        public Task UpdateDataInFormAsync(string table) =>
            UpdateDataInFormAsync(table, changedFields: null);

        private async void ExampleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await ShutdownServiceBrokerAsync();
        }

        private async Task ShutdownServiceBrokerAsync()
        {
            if (System.Threading.Interlocked.Exchange(ref _serviceBrokerShutdownStarted, 1) != 0)
                return;

            try
            {
                if (_lifetimeCts != null && !_lifetimeCts.IsCancellationRequested)
                    _lifetimeCts.Cancel();
            }
            catch { /* ignore */ }

            try
            {
                await _sbController.DisposeAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ExampleForm] SB controller dispose error: {ex}");
            }
            finally
            {
                if (_sbService != null)
                {
                    try { await _sbService.DisposeAsync().ConfigureAwait(false); } catch { }
                    _sbService = null;
                }

                try { _lifetimeCts?.Dispose(); } catch { }
                _lifetimeCts = null;
            }
        }

        private Task InvokeOnUiAsync(Func<Task> fn)
        {
            if (InvokeRequired)
            {
                var tcs = new TaskCompletionSource<object?>();
                BeginInvoke(new Action(async () =>
                {
                    try { await fn(); tcs.TrySetResult(null); }
                    catch (Exception ex) { tcs.TrySetException(ex); }
                }));
                return tcs.Task;
            }

            return fn();
        }

        // Заглушки под вашу бизнес-логику.
        private Task ReloadInitialDataAsync() => Task.CompletedTask;
        private Task ReloadMainAsync() => Task.CompletedTask;
        private Task ReloadDetailsAsync() => Task.CompletedTask;
    }
}
```

## 2) Чек-лист подключения

- Реализовать `IServiceBrokerHost` в форме.
- Инициализировать `_sbController` в конструкторе.
- В `Load` вызывать `InitObjectRestartMap()` и затем `InitServiceBrokerAsync(...)`.
- В `FormClosing` обязательно вызывать `ShutdownServiceBrokerAsync()`.
- В `LoadListenInfoByObjectNameAsync` использовать корректный `DatabaseHelper` (для текущего проекта это `"ace"`).
- Для каждого объекта из `ServiceBrokerObjects` добавить обработчик в `_objectRestartMap`.

