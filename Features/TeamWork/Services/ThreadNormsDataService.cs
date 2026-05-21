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
                @"SELECT
                    TRIM(CAST(kod_dr AS varchar(4))) AS kod_dr,
                    TRIM(ISNULL(CAST(kod3 AS nvarchar(9)), '')) AS kod3,
                    TRIM(ISNULL(CAST(kod_art AS nvarchar(4)), '')) AS kod_art,
                    TRIM(ISNULL(CAST(articul AS nvarchar(255)), '')) AS articul,
                    TRIM(CAST(kod_dr AS varchar(4))) + ' | ' +
                    TRIM(ISNULL(CAST(gr AS nvarchar(255)), '')) + ' | ' +
                    TRIM(ISNULL(CAST(articul AS nvarchar(255)), '')) AS displayText
              FROM dbo.thread_norms_default
              WHERE NULLIF(TRIM(CAST(kod_dr AS varchar(4))), '') IS NOT NULL
              ORDER BY id",
                new { });
        }

        public Task<List<ThreadNormRow>> LoadRowsAsync(bool zeroNormOnly)
        {

            return _dbService.GetListFromProcedureAsync<ThreadNormRow>(
    "dbo.ThreadNorms_LoadRows",
    new { ZeroNormOnly = zeroNormOnly });
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
