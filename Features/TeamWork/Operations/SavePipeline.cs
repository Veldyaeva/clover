using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SewingProduction.Helpers;
using SewingProduction.Interfaces;
using SewingProduction.Models;
using SewingProduction.Services;

namespace SewingProduction.Features.TeamWork.Operations
{
    public sealed class SavePipeline
    {
        private readonly DatabaseHelperSQL _dbHelper;
        private readonly ILogger _logger;
        private readonly Action _finalizeRecalculateNumbers;
        private readonly BulkHelper _bulkHelper = new BulkHelper();

        public SavePipeline(DatabaseHelperSQL dbHelper, ILogger logger, Action finalizeRecalculateNumbers)
        {
            _dbHelper = dbHelper;
            _logger = logger;
            _finalizeRecalculateNumbers = finalizeRecalculateNumbers;
        }

        public async Task SaveAllAsync(
            BindingList<NormRasz> raszList,
            BindingList<NormRask> raskList,
            BindingList<NormKont> kontList,
            int newAnnId,
            bool shouldInsertAllAsNew,
            List<int> deletedRaszIds,
            List<int> deletedRaskIds,
            List<int> deletedKontIds)
        {
            await _logger.LogEventAsync($"[SavePipeline] Start. shouldInsertAllAsNew={shouldInsertAllAsNew}, AnnId={newAnnId}", nameof(SavePipeline));

            // Финальный пересчет нумерации перед сохранением
            _finalizeRecalculateNumbers?.Invoke();

            // Удаления
            await DeleteByIdsAsync("norm_rasz", "nrId", deletedRaszIds);
            await DeleteByIdsAsync("norm_rask", "id", deletedRaskIds);
            await DeleteByIdsAsync("norm_kont", "nkId", deletedKontIds);

            // Сохранение списков
            await SaveListAsync(raszList, "norm_rasz", "nrId", newAnnId, shouldInsertAllAsNew);
            await SaveListAsync(raskList, "norm_rask", "id", newAnnId, shouldInsertAllAsNew);
            await SaveListAsync(kontList, "norm_kont", "nkId", newAnnId, shouldInsertAllAsNew);

            await _logger.LogEventAsync("[SavePipeline] Finished.", nameof(SavePipeline));
        }

        private async Task DeleteByIdsAsync(string tableName, string keyFieldName, List<int> ids)
        {
            if (ids == null || ids.Count == 0) return;
            using (var connection = _dbHelper.GetConnection())
            {
                string deleteSql = $"Update {tableName} SET nrDateDel = GETDATE(), nrCompDel = HOST_NAME() where {keyFieldName} in @ids";
                await connection.ExecuteAsync(deleteSql, new { ids });
                await _logger.LogEventAsync($"[{tableName}] Удалено записей: {ids.Count}", nameof(SavePipeline));
            }
        }

        private async Task SaveListAsync<T>(BindingList<T> list, string tableName, string keyFieldName, int newAnnId, bool shouldInsertAllAsNew)
            where T : class, INewable, IModifiable, new()
        {
            var stopwatch = Stopwatch.StartNew();
            var bulkStopwatch = new Stopwatch();

            List<T> itemsToInsert;
            List<T> itemsToUpdate;

            if (shouldInsertAllAsNew)
            {
                itemsToInsert = list.ToList();
                itemsToUpdate = new List<T>();
            }
            else
            {
                itemsToInsert = list.Where(x => x.IsNew).ToList();
                itemsToUpdate = list.Where(x => x.IsModified && !x.IsNew).ToList();
            }

            string itemTypeName = typeof(T).Name;
            await _logger.LogEventAsync($"[{itemTypeName}] Start saving. Insert: {itemsToInsert.Count}, Update: {itemsToUpdate.Count}", nameof(SavePipeline));

            if (itemsToInsert.Any())
            {
                foreach (var item in itemsToInsert)
                {
                    var annIdProp = typeof(T).GetProperty("annId");
                    if (annIdProp != null)
                    {
                        annIdProp.SetValue(item, newAnnId);
                    }
                }

                using (var connection = _dbHelper.GetConnection())
                {
                    try
                    {
                        await _logger.LogEventAsync($"[{itemTypeName}] BulkInsert: {itemsToInsert.Count}", nameof(SavePipeline));
                        bulkStopwatch.Restart();
                        _bulkHelper.BulkInsert(connection, itemsToInsert, tableName, new[] { keyFieldName });
                        bulkStopwatch.Stop();
                    }
                    catch (Exception ex)
                    {
                        await _logger.LogErrorAsync(ex, $"Ошибка при BulkInsert [{itemTypeName}].");
                        throw;
                    }
                }

                foreach (var item in itemsToInsert)
                {
                    item.IsNew = false;
                    item.IsModified = false;
                }
            }

            if (itemsToUpdate.Any())
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    try
                    {
                        await _logger.LogEventAsync($"[{itemTypeName}] BulkUpdate: {itemsToUpdate.Count}", nameof(SavePipeline));
                        bulkStopwatch.Restart();
                        _bulkHelper.BulkUpdate(connection, itemsToUpdate, tableName, new[] { keyFieldName });
                        bulkStopwatch.Stop();
                    }
                    catch (Exception ex)
                    {
                        await _logger.LogErrorAsync(ex, $"Ошибка при BulkUpdate [{itemTypeName}].");
                        throw;
                    }

                    foreach (var item in itemsToUpdate)
                    {
                        item.IsModified = false;
                    }
                }
            }

            stopwatch.Stop();
            await _logger.LogEventAsync($"[{itemTypeName}] Finished saving. Total: {stopwatch.ElapsedMilliseconds} ms", nameof(SavePipeline));
        }
    }
}



