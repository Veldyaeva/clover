using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System.Data;

namespace SewingProduction.Features.UserDistribution.Models
{
    public class RoleModel : INotifyPropertyChanged
    {
        private int _roleID;
        private string _roleName;
        private string _description;
        private string _creatorName; // из JOIN с таблицей Users
        private int _creatorID; // из Insert

        [Column("RoleID")]
        public int RoleID
        {
            get => _roleID;
            set { if (_roleID != value) { _roleID = value; OnPropertyChanged(nameof(RoleID)); } }
        }

        [Column("RoleName")]
        public string RoleName
        {
            get => _roleName;
            set { if (_roleName != value) { _roleName = value; OnPropertyChanged(nameof(RoleName)); } }
        }

        [Column("Description")]
        public string Description
        {
            get => _description;
            set { if (_description != value) { _description = value; OnPropertyChanged(nameof(Description)); } }
        }

        [NotMapped]
        public string CreatorName
        {
            get => _creatorName;
            set { if (_creatorName != value) { _creatorName = value; OnPropertyChanged(nameof(CreatorName)); } }
        }

        [Column("CreatorID")]
        public int CreatorID
        {
            get => _creatorID;
            set { if (_creatorID != value) { _creatorID = value; OnPropertyChanged(nameof(CreatorID)); } }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
    public class RoleDataService
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelper _dbHelper;

        public RoleDataService(DbService dbService, DatabaseHelper dbHelper)
        {
            _dbService = dbService;
            _dbHelper = dbHelper;
        }

        public async Task<List<RoleModel>> GetListRolesAsync(int userId)
        {
            string query = @"
                SELECT r.RoleID, r.RoleName, r.Description, r.CreatorID, u.UserName AS CreatorName
                FROM Roles r
                LEFT JOIN Users u ON r.CreatorID = u.UserID
                WHERE 
                    r.CreatorID = @UserID
                    OR r.RoleID IN (SELECT ur.RoleID FROM UserRoles ur WHERE ur.UserID = @UserID)
                    OR r.CreatorID IN (SELECT UserID FROM GetDescendants(@UserID))";

            return await _dbService.GetListAsync<RoleModel>(query, new { UserID = userId });
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
                OUTPUT INSERTED.RoleID
                VALUES ('', '', @CreatorID)";
            object result = await _dbHelper.ExecuteScalarAsync(insertQuery, new Dictionary<string, object> { { "@CreatorID", newCreatorId } });
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
            UserRoleDataService userRoleDataService = new UserRoleDataService(_dbHelper);
            await userRoleDataService.AssignRoleAsync(newId, roleId);
            Console.WriteLine($"Назначены базовые ({roleId}) права, профиль:" + newId.ToString());
        }
    }
}
