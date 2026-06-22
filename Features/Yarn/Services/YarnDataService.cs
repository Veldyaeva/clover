using Dapper;
using SewingProduction.Helpers;
using SewingProduction.Features.Yarn.Models;
using SewingProduction.Services;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SewingProduction.Features.Yarn.Services
{
    internal sealed class YarnDataService
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelperSQL _dbHelper;

        public YarnDataService(DbService dbService, DatabaseHelperSQL dbHelper)
        {
            _dbService = dbService;
            _dbHelper = dbHelper;
        }

        public Task<List<YarnCardRow>> LoadCardDataAsync(string nakl)
        {
            const string sql = @"
                SELECT
                    RTRIM(pp.nakl)       AS nakl,
                    RTRIM(pp.t_articul)  AS t_articul,
                    RTRIM(pp.zvet)       AS zvet,
                    pp.seb_t_m
                FROM dbo.prihod_pryz pp WITH (NOLOCK)
                INNER JOIN dbo.prihod_v pv WITH (NOLOCK) ON pp.kod_pr = pv.kod_pr
                WHERE pp.nakl = @Nakl";

            return _dbService.GetListAsync<YarnCardRow>(sql, new { Nakl = nakl });
        }

        public async Task<decimal?> CalculateCostAsync(string nakl)
        {
            const string sql = @"
                SELECT TOP 1 ROUND(pv.np_summa / ISNULL(NULLIF(pv.nps_kol, 0), 1), 2)
                FROM dbo.prihod_pryz pp WITH (NOLOCK)
                INNER JOIN dbo.prihod_v pv WITH (NOLOCK) ON pp.kod_pr = pv.kod_pr
                WHERE pp.nakl = @Nakl";

            return await _dbService.GetEntityAsync<decimal?>(sql, new { Nakl = nakl });
        }

        public async Task<YarnRecalcResult> RecalculateCostAsync(string nakl, string kodArt)
        {
            using var connection = _dbHelper.GetConnection();

            var result = await connection.QuerySingleAsync<YarnRecalcResult>(
                "dbo.Yarn_RecalculateCost",
                new { Nakl = nakl, KodArt = kodArt },
                commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
