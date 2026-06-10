using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace SewingProduction.Features.UserDistribution.DataService
{
    public class UserModelDataService
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelperSQL _dbHelper;

        public UserModelDataService()
        {
            _dbHelper = new DatabaseHelperSQL();
            _dbService = new DbService(_dbHelper);
        }

        public async Task<List<UserModel>> GetUsersHierarchyAsync(UserClass user)
        {
            string query;

            bool isAdmin = user.Roles.Contains("Администратор");

            if (isAdmin)
            {
                // Администратор — получает всех пользователей
                query = @"
                    SELECT u.UserID, u.UserName, u.CreatorID, u2.UserName AS CreatorName, 
                           u.FioID, f.Fio, u.BrigID, u.PasswordHash
                    FROM Users u
                    LEFT JOIN Users u2 ON u.CreatorId = u2.UserId
                    LEFT JOIN fio f ON f.f_id = u.FioID";

                return await _dbService.GetListAsync<UserModel>(query, new { });
            }
            else
            {
                // Обычный пользователь — только себя и потомков
                query = @"
                    SELECT u.UserID, u.UserName, u.CreatorID, u2.UserName AS CreatorName, 
                           u.FioID, f.Fio, u.BrigID, u.PasswordHash
                    FROM Users u
                    LEFT JOIN Users u2 ON u.CreatorId = u2.UserId
                    LEFT JOIN fio f ON f.f_id = u.FioID
                    WHERE u.UserId IN (
                        SELECT UserId FROM GetDescendants(@UserId)
                        UNION SELECT @UserId
                    )";

                return await _dbService.GetListAsync<UserModel>(query, new { UserId = user.UserId });
            }
        }


        public async Task<int> SaveAsync(UserModel user)
        {
            var result = await _dbService.SaveEntityAsync("Users", "UserID", user);
            //Console.WriteLine($"Создан пользователь {user.UserName}, фио ид: {user.FioID}, бриг ид: {user.BrigID},");
            return result;
        }

        public async Task DeleteAsync(UserModel user)
        {
            await _dbService.DeleteEntityAsync("Users", "UserID", user);
        }
        public async Task<List<BrigDto>> LoadBrigList()
        {
            string query = "SELECT id_brig AS BrigID, Brig FROM Brig";
            return await _dbService.GetListAsync<BrigDto>(query, new Dictionary<string, object>());
        }
        public async Task<List<FioDto>> LoadFioList()
        {
            string query = "SELECT f_id AS FioID, Fio , Rab FROM fio WHERE datau IS NULL AND (tab_sovm = 0 OR tab_sovm = tab OR tab_sovm IS NULL)";
            return await _dbService.GetListAsync<FioDto>(query, new Dictionary<string, object>());
        }
        public async void SetPravaForAddUser(int newId)
        {
            string query = $@"SELECT RoleID FROM Roles WHERE RoleName = 'Базовая'";
            DataTable dt = await _dbHelper.ExecuteQueryAsync(query);
            int roleId = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["RoleID"]) : -1;
            UserRoleDataService userRoleDataService = new UserRoleDataService();
            await userRoleDataService.AssignRoleAsync(newId, roleId);
            Console.WriteLine($"Назначены базовые ({roleId}) права, профиль:" + newId.ToString());
        }
    }
}
