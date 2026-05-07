using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.UserDistribution.Models
{
    public class UserRoleDataService
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelperSQL _dbHelper;

        public UserRoleDataService()
        {
            _dbHelper = new DatabaseHelperSQL();
            _dbService = new DbService(_dbHelper);
        }
        public async Task<int> AssignRoleAsync(int userId, int roleId)
        {
            var model = new UserRoleModel
            {
                UserID = userId,
                RoleID = roleId
            };
            return await _dbService.SaveEntityAsync("UserRoles", "UserRolesID", model);
        }
        public async Task<DataTable> GetRolesForUser(int userId)
        {
            string query = @"
            SELECT r.RoleID, r.RoleName, r.Description,
                   CASE WHEN ur.UserID IS NOT NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsSelected,
                   @UserID AS UserID
            FROM Roles r
            LEFT JOIN UserRoles ur ON r.RoleID = ur.RoleID AND ur.UserID = @UserID
            WHERE ur.UserID IS NOT NULL";
            return await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@UserID", userId } });
        }
        public async Task<List<int>> GetRoleIdsByUser(int userId)
        {
            string query = "SELECT RoleID FROM UserRoles WHERE UserID = @UserID";
            var table = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@UserID", userId } });

            return table.AsEnumerable()
                .Select(row => Convert.ToInt32(row["RoleID"]))
                .ToList();
        }
        public async Task RemoveRoleAsync(int userId, int roleId)
        {
            string query = "DELETE FROM UserRoles WHERE UserID = @UserID AND RoleID = @RoleID";
            await _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object>
            {
                { "@UserID", userId },
                { "@RoleID", roleId }
            });
        }
        public async Task setStartRight(int newId)
        {
            string query = $@"SELECT RoleID FROM Roles WHERE RoleName = 'Базовая'";
            int roleId = await _dbHelper.ExecuteScalarAsync(query);
            await AssignRoleAsync(newId, roleId);
            Console.WriteLine($"Назначены базовые ({roleId}) права, профиль:" + newId.ToString());
        }
    }
}
