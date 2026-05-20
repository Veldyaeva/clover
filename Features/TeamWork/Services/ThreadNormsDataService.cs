using Dapper;
using SewingProduction.Core.Models;
using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace SewingProduction.Features.TeamWork.Services
{
    //internal sealed class ThreadNormsDataService
    //    {
    //        private const string ManagersView = "dbo.view_thread_norms_managers";
    //        private const string CategoriesView = "dbo.view_thread_norms_categories";
    //        private const string AssortsByGlobalCodeView = "dbo.view_thread_norms_assorts_globalcode";
    //        private const string AssortsByIdView = "dbo.view_thread_norms_assorts_id";
    //        private const string ThreadMaterialsView = "dbo.view_thread_norms_materials";
    //        private const string RowsByGlobalCodeView = "dbo.view_thread_norms_rows_globalcode";
    //        private const string RowsByIdView = "dbo.view_thread_norms_rows_id";

    //        private readonly DbService _dbService;
    //        private readonly ILogger _logger;

    //        public ThreadNormsDataService(DbService dbService, ILogger logger)
    //        {
    //            _dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
    //            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    //        }

    //        public Task<List<GrupMenModel>> LoadManagersAsync()
    //        {
    //            return _dbService.GetListAsync<GrupMenModel>($@"
    //SELECT *
    //FROM {ManagersView}
    //ORDER BY Men;", new { });
    //        }

    //        public Task<List<ThreadCategoryOption>> LoadCategoriesAsync()
    //        {
    //            return _dbService.GetListAsync<ThreadCategoryOption>($@"
    //SELECT *
    //FROM {CategoriesView}
    //ORDER BY TC_ClassName, TG_GroupName, TCAT_CategoryName;", new { });
    //        }

    //        public async Task<List<ThreadAssortModel>> LoadAssortsAsync()
    //        {
    //            try
    //            {
    //                return await LoadAssortsInternalAsync(AssortsByGlobalCodeView);
    //            }
    //            catch (Exception)
    //            {
    //                await _logger.LogWarningAsync(
    //                    "Не удалось загрузить view_thread_norms_assorts_globalcode, использую view_thread_norms_assorts_id.",
    //                    "ThreadNormsDataService.LoadAssortsAsync");
    //                return await LoadAssortsInternalAsync(AssortsByIdView);
    //            }
    //        }

    //        public Task<List<ThreadMaterialOption>> LoadThreadMaterialsAsync()
    //        {
    //            return _dbService.GetListAsync<ThreadMaterialOption>($@"
    //SELECT *
    //FROM {ThreadMaterialsView}
    //ORDER BY SortGroup, SortArticul;", new { });
    //        }

    //        public async Task<List<ThreadNormRow>> LoadRowsAsync(bool zeroNormOnly)
    //        {
    //            try
    //            {
    //                return await LoadRowsInternalAsync(RowsByGlobalCodeView, zeroNormOnly);
    //            }
    //            catch (Exception)
    //            {
    //                await _logger.LogWarningAsync(
    //                    "Не удалось загрузить view_thread_norms_rows_globalcode, использую view_thread_norms_rows_id.",
    //                    "ThreadNormsDataService.LoadRowsAsync");
    //                return await LoadRowsInternalAsync(RowsByIdView, zeroNormOnly);
    //            }
    //        }

    //        public async Task<int> SaveAsync(ThreadNormDbRow row)
    //        {
    //            NormalizeRowForSave(row);
    //            return await _dbService.SaveEntityAsync("cfn.confection_norm_nitki", "id", row, skipNullOnUpdate: false);
    //        }

    //        public Task DeleteAsync(ThreadNormRow row)
    //        {
    //            return _dbService.DeleteEntityAsync("cfn.confection_norm_nitki", "id", row);
    //        }

    //        private Task<List<ThreadAssortModel>> LoadAssortsInternalAsync(string viewName)
    //        {
    //            return _dbService.GetListAsync<ThreadAssortModel>($@"
    //SELECT *
    //FROM {viewName}
    //ORDER BY TAT_ID;", new { });
    //        }

    //        private Task<List<ThreadNormRow>> LoadRowsInternalAsync(string viewName, bool zeroNormOnly)
    //        {
    //            string whereClause = zeroNormOnly ? "WHERE ISNULL(norm, 0) = 0" : string.Empty;
    //            return _dbService.GetListAsync<ThreadNormRow>($@"
    //SELECT *
    //FROM {viewName}
    //{whereClause}
    //ORDER BY men, TC_ClassName, TG_GroupName, TCAT_CategoryName, TAT_Name, kod_dr;", new { });
    //        }

    //        private static void NormalizeRowForSave(ThreadNormDbRow row)
    //        {
    //            row.men = (row.men ?? string.Empty).Trim();
    //            row.kod_dr = (row.kod_dr ?? string.Empty).Trim();
    //            row.kod3 = NormalizeNullableCode(row.kod3);
    //            row.kod_art = NormalizeNullableCode(row.kod_art);
    //        }

    //        private static string NormalizeNullableCode(string value)
    //        {
    //            var trimmed = (value ?? string.Empty).Trim();
    //            return string.IsNullOrWhiteSpace(trimmed) || string.Equals(trimmed, "null", StringComparison.OrdinalIgnoreCase)
    //                ? null
    //                : trimmed;
    //        }
    //    }
    internal sealed class ThreadNormsDataService
    {
        private readonly DbService _dbService;

        public ThreadNormsDataService(DbService dbService, ILogger logger)
        {
            _dbService = dbService;
        }

        public Task<List<GrupMenModel>> LoadManagersAsync()
        {
            return _dbService.GetListAsync<GrupMenModel>(
                @"SELECT Men, Name
              FROM dbo.view_grup_men order by gm_index",
                new { });
        }

        public Task<List<ThreadCategoryOption>> LoadCategoriesAsync()
        {
            return _dbService.GetListAsync<ThreadCategoryOption>(
                @"SELECT TCAT_ID, TCAT_CategoryName, TG_GroupName, TC_ClassName
              FROM dbo.sewing_thread_categories_view
              ORDER BY TC_ClassName, TG_GroupName, TCAT_CategoryName",
                new { });
        }

        public Task<List<ThreadAssortModel>> LoadAssortsAsync()
        {
            return _dbService.GetListAsync<ThreadAssortModel>(
                @"SELECT TAT_ID, TAT_Name
              FROM dbo.sewing_thread_assorts_view
              ORDER BY TAT_ID",
                new { });
        }

        public Task<List<ThreadMaterialOption>> LoadThreadMaterialsAsync()
        {
            return _dbService.GetListAsync<ThreadMaterialOption>(
                //                @"SELECT kod_dr, kod_art, displayText
                @"SELECT kod_dr, displayText
              FROM dbo.sewing_thread_standards_view
              ORDER BY displayText",
                new { });
        }

        public  Task<List<ThreadNormRow>> LoadRowsAsync(bool zeroNormOnly)
        {
            //return _dbService.GetListAsync<ThreadNormRow>(
            //    @"SELECT *
            //  FROM dbo.ThreadNorms_LoadRows
            //  WHERE (@ZeroNormOnly = 0 OR ISNULL(norm, 0) = 0)
            //  ORDER BY men, TC_ClassName, TG_GroupName, TCAT_CategoryName, TAT_Name, kod_dr",
            //    new { ZeroNormOnly = zeroNormOnly ? 1 : 0 });
            var p = new DynamicParameters();
            p.Add("@ZeroNormOnly", zeroNormOnly);
            
            return _dbService.GetListAsync<ThreadNormRow>(
                "dbo.ThreadNorms_LoadRows",
                p);

        }
        public async Task<int> SaveAsync(ThreadNormDbRow row)
        {
            var p = new DynamicParameters();
            p.AddDynamicParams(new
            {
                row.id,
                row.men,
                row.tg_id_n,
                row.ta_id,
                row.norm,
                row.kod_dr,
                row.kod3,
                row.kod_art,
                row.date_change
            });
            return await _dbService.ExecuteScalarProcedureAsync<int>(
                "cfn.ThreadNorms_Save",p
               );
        }
        private async Task<List<ThreadNormRow>> LoadThreadNormRowsAsync(bool zeroNormOnly)
        {
            return await _dbService.GetListFromProcedureAsync<ThreadNormRow>(
                "cfn.ThreadNorms_LoadRows",
                new { ZeroNormOnly = zeroNormOnly });
        }

        public Task DeleteAsync(ThreadNormRow row)
        {
            var p = new DynamicParameters();
            p.Add("@id", row.id);
            return _dbService.ExecuteSpWithStatusAsync(
                "cfn.ThreadNorms_Delete",
                p);
        }
    }
}
