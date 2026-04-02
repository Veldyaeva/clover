using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.Features.Tabel.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.Tabel.Services
{
    public class TabelLockDataService
    {
        private readonly DbService _dbService;

        public TabelLockDataService()
        {
            _dbService = new DbService(new DatabaseHelper());
        }
        public async Task<List<TabelSpLockModel>> GetTabelSpLock()
        {
            const string query = @"
            SELECT * FROM tabel_sp_lock
            ORDER BY tsl_god";

            return await _dbService.GetListAsync<TabelSpLockModel>(query, new { });
        }
        public async Task<List<TabelSpLockTypeModel>> GetTabelSpLockType()
        {
            const string query = @"SELECT * FROM tabel_sp_lock_type";

            return await _dbService.GetListAsync<TabelSpLockTypeModel>(query, new { });
        }
        public async Task UpdateTabelSpLock(int? tslId, DateTime? newDateTo)
        {
            const string query = @"Update tabel_sp_lock set tsl_DateTo = @newDateTo 
            WHERE tslID = @tslId";
            await _dbService.GetListAsync<TabelSpLockTypeModel>(query, new { tslId, newDateTo });
        }
    }
}
