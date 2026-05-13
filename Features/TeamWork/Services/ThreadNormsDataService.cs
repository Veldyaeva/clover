using SewingProduction.Core.Models;
using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Services
{
    internal sealed class ThreadNormsDataService
    {
        private readonly DbService _dbService;
        private readonly ILogger _logger;
        private readonly ThreadNormsSqlProvider _sqlProvider;

        public ThreadNormsDataService(DbService dbService, ILogger logger, ThreadNormsSqlProvider sqlProvider)
        {
            _dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sqlProvider = sqlProvider ?? throw new ArgumentNullException(nameof(sqlProvider));
        }

        public Task<List<GrupMenModel>> LoadManagersAsync()
        {
            return _dbService.GetListAsync<GrupMenModel>(_sqlProvider.Load("LoadManagers.sql"), new { });
        }

        public Task<List<ThreadCategoryOption>> LoadCategoriesAsync()
        {
            return _dbService.GetListAsync<ThreadCategoryOption>(_sqlProvider.Load("LoadCategories.sql"), new { });
        }

        public async Task<List<ThreadAssortModel>> LoadAssortsAsync()
        {
            try
            {
                return await _dbService.GetListAsync<ThreadAssortModel>(_sqlProvider.Load("LoadAssortsByGlobalCode.sql"), new { });
            }
            catch (Exception ex) when (ex.Message.Contains("GlobalCode", StringComparison.OrdinalIgnoreCase))
            {
                await _logger.LogWarningAsync(
                    "TOVAR_ASSTYPE does not contain TAT_GlobalCode, falling back to TAT_ID.",
                    "ThreadNormsDataService.LoadAssortsAsync");
                return await _dbService.GetListAsync<ThreadAssortModel>(_sqlProvider.Load("LoadAssortsById.sql"), new { });
            }
        }

        public Task<List<ThreadMaterialOption>> LoadThreadMaterialsAsync()
        {
            return _dbService.GetListAsync<ThreadMaterialOption>(_sqlProvider.Load("LoadThreadMaterials.sql"), new { });
        }

        public async Task<List<ThreadNormRow>> LoadRowsAsync(bool zeroNormOnly)
        {
            string whereClause = zeroNormOnly ? "WHERE ISNULL(n.norm, 0) = 0" : string.Empty;
            try
            {
                return await _dbService.GetListAsync<ThreadNormRow>(
                    _sqlProvider.Load("LoadRowsByGlobalCode.sql").Replace("{whereClause}", whereClause),
                    new { });
            }
            catch (Exception ex) when (ex.Message.Contains("GlobalCode", StringComparison.OrdinalIgnoreCase))
            {
                await _logger.LogWarningAsync(
                    "TOVAR_ASSTYPE does not contain TAT_GlobalCode, falling back to TAT_ID for rows.",
                    "ThreadNormsDataService.LoadRowsAsync");
                return await _dbService.GetListAsync<ThreadNormRow>(
                    _sqlProvider.Load("LoadRowsById.sql").Replace("{whereClause}", whereClause),
                    new { });
            }
        }

        public async Task<int> SaveAsync(ThreadNormDbRow row)
        {
            NormalizeRowForSave(row);
            return await _dbService.SaveEntityAsync("cfn.confection_norm_nitki", "id", row, skipNullOnUpdate: false);
        }

        public Task DeleteAsync(ThreadNormRow row)
        {
            return _dbService.DeleteEntityAsync("cfn.confection_norm_nitki", "id", row);
        }

        private static void NormalizeRowForSave(ThreadNormDbRow row)
        {
            row.men = (row.men ?? string.Empty).Trim();
            row.kod_dr = (row.kod_dr ?? string.Empty).Trim();
            row.kod3 = NormalizeNullableCode(row.kod3);
            row.kod_art = NormalizeNullableCode(row.kod_art);
        }

        private static string NormalizeNullableCode(string value)
        {
            var trimmed = (value ?? string.Empty).Trim();
            return string.IsNullOrWhiteSpace(trimmed) || string.Equals(trimmed, "null", StringComparison.OrdinalIgnoreCase)
                ? null
                : trimmed;
        }
    }
}
