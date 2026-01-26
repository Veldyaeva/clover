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
    public class UserPodrDataService
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelper _dbHelper;

        public UserPodrDataService()
        {
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);
        }
        public async Task<List<RolePodrModel>> GetAvailablePodrTablesByRoleAsync(int userID)
        {
            string sql = @"
            SELECT DISTINCT 
                rp.PodrTableID,
                atn.name AS PodrTableName
            FROM RolePodr rp
            INNER JOIN all_table_name atn ON atn.id_atn = rp.PodrTableID
            INNER JOIN UserRoles ur ON ur.RoleID = rp.RoleID
            WHERE ur.UserID = @UserID";

            return await _dbService.GetListAsync<RolePodrModel>(
                sql, new { UserID = userID });
        }
        public async Task<List<UserPodrModel>> LoadAllUserPodrAsync()
        {
            const string sql = @"
            SELECT
                up.UserPodrID,
                up.UserID,
                up.PodrID,
                up.PodrTableID,
                up.CreatorID
            FROM UserPodr up";

            return await _dbService.GetListAsync<UserPodrModel>(sql, new { });
        }
        public async Task<List<UserPodrModel>> LoadPodrFromTableAsync(
        int podrTableId,
        string tableName)
        {
            string sql = $@"
            SELECT DISTINCT
                t.PodrID,
                @PodrTableID AS PodrTableID,
                t.Name AS Name,
                atn.name_rus AS PodrTableName
            FROM {tableName}_FormingView t
            LEFT JOIN UserPodr up
                ON up.PodrID = t.PodrID
               AND up.PodrTableID = @PodrTableID
            LEFT JOIN all_table_name atn ON atn.id_atn = @PodrTableID
            WHERE t.Name IS NOT NULL
            AND LTRIM(RTRIM(t.Name)) <> ''
            ORDER BY t.Name";

            return await _dbService.GetListAsync<UserPodrModel>(
                sql,
                new
                {
                    PodrTableID = podrTableId
                });
        }

        public async Task<int> SaveAsync(UserPodrModel UserPodr)
        {
            var result = await _dbService.SaveEntityAsync("UserPodr", "UserPodrID", UserPodr);
            return result;
        }

        public async Task DeleteAsync(UserPodrModel UserPodr)
        {
            await _dbService.DeleteEntityAsync("UserPodr", "UserPodrID", UserPodr);
        }
    }
}
