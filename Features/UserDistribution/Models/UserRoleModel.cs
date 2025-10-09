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
    public class UserRoleModel : INotifyPropertyChanged//, INewable
    {
        private int _userRolesID;
        private int _userID;
        private int _roleID;
        private string _roleName;
        private string _description;
        private bool _hasRole;
        //public bool IsNew { get; set; } = true;

        [Column("UserRolesID")]
        public int UserRolesID
        {
            get => _userRolesID;
            set { if (_userRolesID != value) { _userRolesID = value; OnPropertyChanged(nameof(UserRolesID)); } }
        }

        [Column("UserID")]
        public int UserID
        {
            get => _userID;
            set { if (_userID != value) { _userID = value; OnPropertyChanged(nameof(UserID)); } }
        }

        [Column("RoleID")]
        public int RoleID
        {
            get => _roleID;
            set { if (_roleID != value) { _roleID = value; OnPropertyChanged(nameof(RoleID)); } }
        }

        [Column("RoleName")]
        [NotMapped]
        public string RoleName
        {
            get => _roleName;
            set { if (_roleName != value) { _roleName = value; OnPropertyChanged(nameof(RoleName)); } }
        }
        [NotMapped]
        public string Description
        {
            get => _description;
            set { if (_description != value) { _description = value; OnPropertyChanged(nameof(Description)); } }
        }

        [NotMapped]
        public bool HasRole
        {
            get => _hasRole;
            set { if (_hasRole != value) { _hasRole = value; OnPropertyChanged(nameof(HasRole)); } }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public class UserRoleDataService
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelper _dbHelper;

        public UserRoleDataService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
            _dbService = new DbService(dbHelper);
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
        public async Task<DataTable> GetRolesWithFlags(int userId)
        {
            string query = @"
            SELECT r.RoleID, r.RoleName, r.Description,
                   CASE WHEN ur.UserID IS NOT NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS HasRole,
                   @UserID AS UserID
            FROM Roles r
            LEFT JOIN UserRoles ur ON r.RoleID = ur.RoleID AND ur.UserID = @UserID";
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
