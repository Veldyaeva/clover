using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.UserDistribution.Models
{
    public class RoleDataService
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelper _dbHelper;

        public RoleDataService()
        {
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);
        }
        public async Task<List<RoleModel>> GetListRolesAsync(UserClass user)
        {
            string query = @"
            SELECT r.RoleID, r.RoleName, r.Description, u.UserName
            FROM Roles r
            JOIN Users u ON r.CreatorID = u.UserID
            WHERE NOT EXISTS(
                SELECT 1
                FROM RoleObject ro
                WHERE ro.RoleID = r.RoleID
                AND NOT EXISTS(
                    SELECT 1
                    FROM
                      (SELECT ro.ObjectID, MAX(ro.ModeID) AS UserModeID
                      FROM RoleObject ro
                      JOIN UserRoles ur ON ro.RoleID = ur.RoleID
                      WHERE ur.UserID = @UserID
                      GROUP BY ro.ObjectID) uo
                    WHERE uo.ObjectID = ro.ObjectID
                    AND uo.UserModeID >= ro.ModeID
                )
            )";

            return await _dbService.GetListAsync<RoleModel>(query, new { UserID = user.UserId });
        }

        public async Task<int> SaveAsync(RoleModel role)
        {
            return await _dbService.SaveEntityAsync("Roles", "RoleID", role);
        }

        public async Task DeleteAsync(int roleId)
        {
            string query = @"
                DELETE FROM Roles WHERE RoleID = @RoleID;
                DELETE FROM RoleObject WHERE RoleID = @RoleID;";
            await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@RoleID", roleId } });
        }

        public async Task<bool> RoleNameExists(string roleName)
        {
            string query = "SELECT COUNT(*) FROM Roles WHERE RoleName = @RoleName";
            object result = await _dbHelper.ExecuteScalarAsync(query, new Dictionary<string, object> { { "@RoleName", roleName } });
            return Convert.ToInt32(result) > 0;
        }

        public async Task<int> GetCreatorIdByRole(int roleId)
        {
            string query = "SELECT CreatorID FROM Roles WHERE RoleID = @RoleID";
            object result = await _dbHelper.ExecuteScalarAsync(query, new Dictionary<string, object> { { "@RoleID", roleId } });
            return result != DBNull.Value ? Convert.ToInt32(result) : -1;
        }
        public async Task<int> CopyRole(int originalRoleId, int newCreatorId)
        {
            // 1. Создание новой пустой роли
            string insertQuery = @"
                INSERT INTO Roles (RoleName, Description, CreatorID)
                VALUES ('', '', @CreatorID);

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            object result = await _dbHelper.ExecuteScalarAsync(insertQuery, new Dictionary<string, object>
            {
                { "@CreatorID", newCreatorId }
            });

            int newRoleId = Convert.ToInt32(result);

            // 2. Копирование объектов
            string copyObjectsQuery = @"
                INSERT INTO RoleObject (RoleID, ObjectID, ModeID)
                SELECT @NewRoleID, ObjectID, ModeID
                FROM RoleObject
                WHERE RoleID = @OriginalRoleID";

            await _dbHelper.ExecuteQueryAsync(copyObjectsQuery, new Dictionary<string, object>
            {
                { "@NewRoleID", newRoleId },
                { "@OriginalRoleID", originalRoleId }
            });

            return newRoleId;
        }


        public async Task<HashSet<int>> GetEditableCreatorIds(int userId)
        {
            string query = "SELECT UserID FROM GetDescendants(@UserID) UNION SELECT @UserID";
            DataTable table = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@UserID", userId } });

            return table.AsEnumerable()
                .Select(r => Convert.ToInt32(r["UserID"]))
                .ToHashSet();
        }
        public async Task<List<int>> GetRoleIdsByNamesAsync(List<string> roleNames)
        {
            if (roleNames == null || roleNames.Count == 0)
                return new List<int>();

            string query = $@"
            SELECT RoleID FROM Roles
            WHERE RoleName IN ({string.Join(",", roleNames.Select((r, i) => $"@name{i}"))})";

            var parameters = roleNames
                .Select((r, i) => new KeyValuePair<string, object>($"@name{i}", r))
                .ToDictionary(kvp => kvp.Key, kvp => (object)kvp.Value);

            DataTable result = await _dbHelper.ExecuteQueryAsync(query, parameters);

            return result.AsEnumerable()
                .Select(r => Convert.ToInt32(r["RoleID"]))
                .ToList();
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
