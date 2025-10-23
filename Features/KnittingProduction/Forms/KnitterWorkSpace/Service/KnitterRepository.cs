using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    /// <summary>
    /// Репозиторий данных для KnitterWorkSpace (минимальный слой доступа к БД).
    /// </summary>
    public class KnitterRepository
    {
        private readonly DbService _dbService;

        public KnitterRepository(DatabaseHelper dbHelper)
        {
            _dbService = new DbService(dbHelper);
        }

        public async Task<List<KnitterPZVModel>> GetPlanByTabAsync(int tab)
        {
            const string query = "SELECT * FROM dbo.planZagrVyaz WHERE pzvTab = @tab";
            return await _dbService.GetListAsync<KnitterPZVModel>(query, new { tab });
        }

        public async Task<string> GetFioByTabAsync(int tab)
        {
            const string query = "SELECT fio FROM dbo.fio WHERE tab = @tab";
            var fio = await _dbService.GetFirstOrDefaultAsync<string>(query, new { tab });
            return fio ?? string.Empty;
        }

        public async Task<List<FioModel>> GetFioListAsync()
        {
            const string query = @"SELECT tab AS Tab, fio AS Fio FROM dbo.fio ORDER BY fio";
            return await _dbService.GetListAsync<FioModel>(query, new { });
        }
    }
}


