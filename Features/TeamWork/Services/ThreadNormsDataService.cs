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
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SewingProduction.Features.TeamWork.Services
{

    internal sealed class ThreadNormsDataService
    {
        private const string ThreadNormsTableName = "cfn.confection_norm_nitki";
        private const string ThreadNormsKeyColumn = "id";

        private readonly DbService _dbService;
        private readonly DatabaseHelperSQL _dbHelper;
        private readonly BulkHelper _bulkHelper = new BulkHelper();
        private HashSet<string> _threadNormsTableColumns;

        public ThreadNormsDataService(DbService dbService, DatabaseHelperSQL dbHelper, ILogger logger)
        {
            _dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
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
            if (row == null) throw new ArgumentNullException(nameof(row));

            await SaveAsync(new[] { row });
            return row.id;
        }

        public async Task SaveAsync(IReadOnlyCollection<ThreadNormDbRow> rows)
        {
            if (rows == null) throw new ArgumentNullException(nameof(rows));
            if (rows.Count == 0) return;

            foreach (var row in rows)
            {
                row.komp_change = row.date_change.HasValue ? Environment.MachineName : null;
            }

            var rowsToInsert = rows.Where(x => x.id <= 0).ToList();
            var rowsToUpdate = rows.Where(x => x.id > 0).ToList();
            var excludeColumns = await GetThreadNormBulkExcludeColumnsAsync();

            using (var connection = _dbHelper.GetConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    if (rowsToInsert.Count > 0)
                    {
                        _bulkHelper.BulkInsert(
                            connection,
                            rowsToInsert,
                            ThreadNormsTableName,
                            new[] { ThreadNormsKeyColumn },
                            transaction,
                            excludeColumns: excludeColumns);
                    }

                    if (rowsToUpdate.Count > 0)
                    {
                        _bulkHelper.BulkUpdate(
                            connection,
                            rowsToUpdate,
                            ThreadNormsTableName,
                            new[] { ThreadNormsKeyColumn },
                            transaction,
                            excludeColumns: excludeColumns);
                    }

                   transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                try
                {
                    var p = new DynamicParameters();
                    p.AddDynamicParams(new
                    {
                        rowsToUpdate[0].id,
                        rowsToUpdate[0].men,
                        rowsToUpdate[0].tg_id_n,
                        rowsToUpdate[0].ta_id,
                        rowsToUpdate[0].norm,
                        rowsToUpdate[0].kod_dr,
                        rowsToUpdate[0].kod3,
                        rowsToUpdate[0].kod_art,
                        rowsToUpdate[0].date_change
                    });


                    await _dbService.ExecuteScalarProcedureAsync<int>(
                   "cfn.ThreadNorms_Save", p);
                }
                catch (Exception ex) {
                    Debug.WriteLine(ex);
                }
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

        private async Task<HashSet<string>> GetThreadNormBulkExcludeColumnsAsync()
        {
            var tableColumns = await GetThreadNormsTableColumnsAsync();
            var modelColumns = typeof(ThreadNormDbRow)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(x => x.Name);

            return modelColumns
                .Where(x => !tableColumns.Contains(x))
                .ToHashSet(StringComparer.OrdinalIgnoreCase);
        }

        private async Task<HashSet<string>> GetThreadNormsTableColumnsAsync()
        {
            if (_threadNormsTableColumns != null)
            {
                return _threadNormsTableColumns;
            }

            const string query = @"
SELECT c.name
FROM sys.columns c
INNER JOIN sys.objects o
    ON o.object_id = c.object_id
INNER JOIN sys.schemas s
    ON s.schema_id = o.schema_id
WHERE s.name = @SchemaName
  AND o.name = @TableName;";

            var columns = await _dbService.GetListAsync<string>(
                query,
                new
                {
                    SchemaName = "cfn",
                    TableName = "confection_norm_nitki"
                });

            _threadNormsTableColumns = (columns ?? new List<string>())
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            if (!_threadNormsTableColumns.Contains(ThreadNormsKeyColumn))
            {
                throw new InvalidOperationException(
                    $"В таблице {ThreadNormsTableName} не найден ключевой столбец {ThreadNormsKeyColumn}.");
            }

            return _threadNormsTableColumns;
        }
    }
}
