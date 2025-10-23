using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraGrid.Views.Grid;
using Dapper;
using SewingProduction.Features.TeamWork.Services;
using SewingProduction.Models;
using SewingProduction.Services;
using SewingProduction.Interfaces;
using SewingProduction.Helpers;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Operations
{
    public sealed class BufferImportService
    {
        public sealed class BufferImportReport
        {
            public int PartsCount { get; set; }
            public int InsertedCount { get; set; }
        }

        private readonly ArtNormRepository _artNormService;
        private readonly DatabaseHelper _dbHelper;
        private readonly ILogger _logger;

        private readonly BindingList<NormRasz> _normRaszList;
        private readonly BindingSource _bindingSource;
        private readonly GridView _gridView;
        private readonly Action _sortGridView;
        private readonly Action<NormRasz, bool> _applyPostStructureUi;

        public BufferImportService(
            ArtNormRepository artNormService,
            DatabaseHelper dbHelper,
            ILogger logger,
            BindingList<NormRasz> normRaszList,
            BindingSource bindingSource,
            GridView gridView,
            Action sortGridView,
            Action<NormRasz, bool> applyPostStructureUi)
        {
            _artNormService = artNormService;
            _dbHelper = dbHelper;
            _logger = logger;
            _normRaszList = normRaszList;
            _bindingSource = bindingSource;
            _gridView = gridView;
            _sortGridView = sortGridView;
            _applyPostStructureUi = applyPostStructureUi;
        }

        public async Task<BufferImportReport> ImportAsync(
            IReadOnlyList<int> sourceAnnIds,
            int destinationAnnId,
            bool clearExistingBefore,
            bool markExistingAsDeleted)
        {
            var report = new BufferImportReport { PartsCount = sourceAnnIds?.Count ?? 0, InsertedCount = 0 };
            if (sourceAnnIds == null || sourceAnnIds.Count == 0) return report;

            try
            {
                if (clearExistingBefore)
                {
                    if (markExistingAsDeleted && _normRaszList?.Count > 0)
                    {
                        var existingIds = _normRaszList
                            .Where(x => !x.IsNew && x.nrID > 0)
                            .Select(x => x.nrID)
                            .ToList();
                        if (existingIds.Count > 0)
                        {
                            using (var connection = _dbHelper.GetConnection())
                            {
                                string deleteSql = "UPDATE norm_rasz SET nrDateDel = GETDATE(), nrCompDel = HOST_NAME() WHERE nrId IN @ids";
                                await connection.ExecuteAsync(deleteSql, new { ids = existingIds });
                                await _logger.LogEventAsync($"Помечено на удаление операций NormRasz: {existingIds.Count}", nameof(BufferImportService));
                            }
                        }
                    }

                    _normRaszList?.Clear();
                }

                _gridView.BeginDataUpdate();
                try
                {
                    int currentMaxN = (_normRaszList != null && _normRaszList.Count > 0)
                        ? _normRaszList.Select(x => x.N).DefaultIfEmpty(0).Max()
                        : 0;

                    foreach (var id in sourceAnnIds)
                    {
                        List<NormRasz> raszList = await _artNormService.GetRelatedNormRasz(id);
                        if (raszList == null || raszList.Count == 0) continue;
                        int partMaxN = raszList.Select(x => x.N).DefaultIfEmpty(0).Max();
                        int offset = currentMaxN;

                        foreach (var item in raszList)
                        {
                            item.nrID = 0;
                            item.annId = destinationAnnId;
                            item.nrDateAdd = null;
                            item.nrCompAdd = null;
                            item.IsNew = true;
                            item.N = item.N + offset;
                            _normRaszList.Add(item);
                            report.InsertedCount++;
                        }

                        currentMaxN += partMaxN;
                    }

                    _bindingSource.ResetBindings(false);
                    _sortGridView();
                    _applyPostStructureUi(_normRaszList.FirstOrDefault(), true);
                }
                finally
                {
                    try { _gridView.EndDataUpdate(); } catch { }
                }

                return report;
            }
            catch (SqlException sqlEx)
            {
                await _logger.LogErrorAsync(sqlEx, "Ошибка при вставке данных из буфера");
                throw;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при вставке данных из буфера");
                throw;
            }
        }
    }
}



