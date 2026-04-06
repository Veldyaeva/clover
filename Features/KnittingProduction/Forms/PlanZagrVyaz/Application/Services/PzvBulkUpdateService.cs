using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Services
{
    public sealed class PzvBulkUpdateService : IPzvBulkUpdateService
    {
        private readonly BindingSource _bindingSource;
        private readonly DatabaseHelper _dbHelper;
        private readonly BulkHelper _bulkHelper;
        private readonly Action _muteBrokerNotifications;
        private readonly Func<Task> _reloadCallback;

        public PzvBulkUpdateService(
            BindingSource bindingSource,
            DatabaseHelper dbHelper,
            BulkHelper bulkHelper,
            Action muteBrokerNotifications,
            Func<Task> reloadCallback)
        {
            _bindingSource = bindingSource;
            _dbHelper = dbHelper;
            _bulkHelper = bulkHelper;
            _muteBrokerNotifications = muteBrokerNotifications;
            _reloadCallback = reloadCallback;
        }

        public async Task UpdateAsync(
            IReadOnlyCollection<int> pzvIds,
            Action<PZVOperList> mutate,
            CancellationToken ct = default)
        {
            if (pzvIds == null || pzvIds.Count == 0)
                return;

            var idSet = pzvIds.ToHashSet();

            var allRows = _bindingSource.List.Cast<PZVOperList>().ToList();
            var rowsToUpdate = allRows.Where(x => idSet.Contains(x.olPzvID)).ToList();

            foreach (var row in rowsToUpdate)
            {
                mutate(row);
                row.IsModified = true;
            }

            var changedRows = allRows
                .Where(x => x.IsModified)
                .Select(x => x.ToPZV())
                .ToList();

            if (changedRows.Count == 0)
                return;

            _muteBrokerNotifications();

            using var connection = _dbHelper.GetConnection();
            _bulkHelper.BulkAllDataUpdate<PZV>(connection, changedRows, "planZagrVyaz", new[] { "pzvID" });

            foreach (var row in rowsToUpdate)
            {
                row.IsModified = false;
                row.SyncSelection = 0;
                row.ErrorSelection = 0;
            }

            await _reloadCallback();
        }
    }
}