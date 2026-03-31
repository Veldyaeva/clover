using SewingProduction.Core.helpers;
using SewingProduction.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class PlanZagrVyaz
    {
        private void MutePlanBrokerNotifications()
        {
            _sbController.Mute(PlanBrokerSelfMute);
        }

        private void InitObjectRestartMap()
        {
            _objectRestartMap = new Dictionary<string, Func<Task>>(StringComparer.OrdinalIgnoreCase)
            {
                ["dbo.planZagrVyaz"] = LoadPlanZagrVyazByZadanySelection,
                ["dbo.smenZadanyVyaz"] = LoadSmenZadanyVyazDataAsync,
                ["dbo.planTotalHoursByKnitMachine"] = LoadPlanTotalHoursByKnitMachineDataAsync,
            };
        }
        public IReadOnlyList<string> ServiceBrokerObjects =>
            _objectRestartMap?.Keys.ToArray() ?? Array.Empty<string>();

        public IReadOnlyDictionary<string, int> RefreshPriorities =>
            new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                { "GetPlanZagrVyazByPachList", 10 },
                { "GetSmenZadanyVyaz", 5 }
            };

        public string ServiceBrokerFormName => GetType().Name;
        public bool UseSchemaInListenName => true;
        public IReadOnlyCollection<string> IgnoredTables => _ignoredServiceBrokerTables;
        public async Task InitServiceBrokerAsync(CancellationToken ct)
        {
            await _sbController.InitializeAsync(_sbHub, _sbHubOwnerId, ct);
        }
        public async Task<List<ServiceBrokerModel.TableListenInfo>> LoadListenInfoByObjectNameAsync(
            string objectName, CancellationToken ct)
        {
            var list = await _sbService.GetObjectListForServiceBroker(objectName, ct);
            return ServiceBrokerListenInfoNormalizer.Normalize(list);
        }
        private Task InvokeOnUiAsync(Func<Task> fn)
        {
            if (InvokeRequired)
            {
                var tcs = new TaskCompletionSource<object>();
                BeginInvoke(new Action(async () =>
                {
                    try { await fn(); tcs.TrySetResult(null); }
                    catch (Exception ex) { tcs.TrySetException(ex); }
                }));
                return tcs.Task;
            }
            Debug.WriteLine($"[PlanZagrVyaz] InvokeOnUiAsync: already on UI thread");
            return fn();
        }
        public async Task RestartDataByObjectNameAsync(string objectName, CancellationToken ct)
        {
            if (_objectRestartMap.TryGetValue(objectName, out var restartAction))
                await restartAction();
        }

        public async Task UpdateDataInFormAsync(string table, string? changedFieldsCsv = null)
        {
            if (_ignoredServiceBrokerTables.Contains(table))
                return;

            if (_objectRestartMap.TryGetValue(table, out var restartAction))
                await restartAction();
        }

        //protected override void OnFormClosed(FormClosedEventArgs e)
        //{
        //    base.OnFormClosed(e);
        //    _closing = true;
        //    _lifetimeCts?.Cancel();
        //}
    }
}