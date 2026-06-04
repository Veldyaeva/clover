using Dapper;
using SewingProduction.Core.Models;
using SewingProduction.Features.TeamWork.Forms;
using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace SewingProduction.Features.TeamWork.Services
{

    internal sealed class ThreadNormsDataService
    {
        private readonly DbService _dbService;
        private bool? _threadNormsSaveHasKompNameParameter;

        public ThreadNormsDataService(DbService dbService, ILogger logger)
        {
            _dbService = dbService;
        }

        public Task<List<ThreadAssortModel>> LoadAssortsAsync()
        {
            return _dbService.GetListAsync<ThreadAssortModel>(
                @"SELECT TAT_ID, TAT_Name
                  FROM dbo.sewing_thread_assorts_view
                  ORDER BY TAT_ID",
                new { });
        }

        public Task<List<ThreadMaterialOption>> LoadMaterialsAsync()
        {
            return _dbService.GetListAsync<ThreadMaterialOption>(
                @"SELECT
                      RTRIM(kod_dr) AS kod_dr,
                      RTRIM(ISNULL(kod3, '')) AS kod3,
                      RTRIM(ISNULL(kod_art, '')) AS kod_art,
                      RTRIM(ISNULL(articul, '')) AS displayText
                  FROM dbo.thread_norms_default
                  WHERE NULLIF(RTRIM(ISNULL(kod_dr, '')), '') IS NOT NULL
                  ORDER BY RTRIM(kod_dr)",
                new { });//RTRIM(kod_dr) + ' | ' + RTRIM(ISNULL(gr, '')) + ' | ' + 
        }

        public Task<List<ThreadNormRow>> LoadRowsAsync(bool zeroNormOnly)
        {

            return _dbService.GetListFromProcedureAsync<ThreadNormRow>(
    "dbo.ThreadNorms_LoadRows",
    new { ZeroNormOnly = zeroNormOnly });
        }
        public async Task<int> SaveAsync(ThreadNormDbRow row)
        {
            try
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

                if (await ThreadNormsSaveSupportsComputerNameAsync())
                {
                    p.Add("@komp_change", row.date_change.HasValue ? Environment.MachineName : null);
                }

                return await _dbService.ExecuteScalarProcedureAsync<int>(
                    "cfn.ThreadNorms_Save", p
                   );
            }
            catch (Exception ex) {Debug.WriteLine(ex.Message+"123");
                return 0;
            }
            
        }
        public Task DeleteAsync(ThreadNormRow row)
        {
            var p = new DynamicParameters();
            p.Add("@id", row.id);
            return _dbService.ExecuteScalarProcedureAsync<int>(
                "cfn.ThreadNorms_Delete",
                p);
        }

        private async Task<bool> ThreadNormsSaveSupportsComputerNameAsync()
        {
            if (_threadNormsSaveHasKompNameParameter.HasValue)
            {
                return _threadNormsSaveHasKompNameParameter.Value;
            }

            const string query = @"
SELECT TOP (1) 1
FROM sys.parameters p
INNER JOIN sys.objects o
    ON o.object_id = p.object_id
INNER JOIN sys.schemas s
    ON s.schema_id = o.schema_id
WHERE s.name = @SchemaName
  AND o.name = @ProcedureName
  AND p.name = @ParameterName;";

            var result = await _dbService.GetFirstOrDefaultAsync<int?>(
                query,
                new
                {
                    SchemaName = "cfn",
                    ProcedureName = "ThreadNorms_Save",
                    ParameterName = "@komp_change"
                });

            _threadNormsSaveHasKompNameParameter = result.HasValue;
            return _threadNormsSaveHasKompNameParameter.Value;
        }
    }
}
