using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.Features.UserDistribution.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.UserDistribution.DataService
{
    public class RolePodrDataService
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelper _dbHelper;
        public RolePodrDataService()
        {
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);
        }

        public async Task<List<RolePodrModel>> LoadAllRolePodrAsync()
        {
            const string sql = @"
            SELECT
                rp.RolePodrID,
                rp.RoleID,
                rp.PodrTableID
            FROM RolePodr rp";

            return await _dbService.GetListAsync<RolePodrModel>(sql, new { });
        }

        public async Task<int> SaveAsync(RolePodrModel RolePodr)
        {
            var result = await _dbService.SaveEntityAsync("RolePodr", "RolePodrID", RolePodr);
            return result;
        }

        public async Task DeleteAsync(int RoleID, int PodrTableID)
        {
            const string sql = @"
            DELETE FROM RolePodr WHERE RoleID = @RoleID AND PodrTableID = @PodrTableID";

            await _dbService.GetListAsync<RolePodrModel>(sql, new { RoleID, PodrTableID });
        }
    }
}
