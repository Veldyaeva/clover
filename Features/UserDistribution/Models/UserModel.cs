using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using SewingProduction.Helpers;
using SewingProduction.Interfaces;
using SewingProduction.Services;
using SewingProduction.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SewingProduction.Features.UserDistribution.Models
{
    public class UserModel : INotifyPropertyChanged//, INewable
    {
        private int _userId;
        private string _userName;
        private int _brigID;
        public string _brigName;
        private int _creatorId;
        private string _creatorName;
        private int _fioId;
        private string _fio;
        private string _passwordHash;

        //public bool IsNew { get; set; } = true;

        [Column("UserID")]
        public int UserID
        {
            get => _userId;
            set { if (_userId != value) { _userId = value; OnPropertyChanged(nameof(UserID)); } }
        }

        [Column("UserName")]
        public string UserName
        {
            get => _userName;
            set { if (_userName != value) { _userName = value; OnPropertyChanged(nameof(UserName)); } }
        }

        [Column("CreatorID")]
        public int CreatorID
        {
            get => _creatorId;
            set { if (_creatorId != value) { _creatorId = value; OnPropertyChanged(nameof(CreatorID)); } }
        }

        [Column("FioID")]
        public int FioID
        {
            get => _fioId;
            set { if (_fioId != value) { _fioId = value; OnPropertyChanged(nameof(FioID)); } }
        }

        [Column("BrigID")]
        public int BrigID
        {
            get => _brigID;
            set { if (_brigID != value) { _brigID = value; OnPropertyChanged(nameof(BrigID)); } }
        }

        [NotMapped]
        public string Fio
        {
            get => _fio;
            set { if (_fio != value) { _fio = value; OnPropertyChanged(nameof(Fio)); } }
        }

        [NotMapped]
        public string BrigName
        {
            get => _brigName;
            set { if (_brigName != value) { _brigName = value; OnPropertyChanged(nameof(BrigName)); } }
        }

        [NotMapped]
        public string CreatorName
        {
            get => _creatorName;
            set { if (_creatorName != value) { _creatorName = value; OnPropertyChanged(nameof(CreatorName)); } }
        }
        [NotMapped]
        public string Password { get; set; }
        [Column("PasswordHash")]
        public string PasswordHash
        {
            get => _passwordHash;
            set { if (_passwordHash != value) { _passwordHash = value; OnPropertyChanged(nameof(PasswordHash)); } }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public class BrigDto
    {
        public int BrigID { get; set; }
        public string Brig { get; set; }
    }
    public class FioDto
    {
        public int FioID { get; set; }
        public string Fio { get; set; }
    }
    public class UserModelDataService
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelper _dbHelper;

        public UserModelDataService(DbService dbService, DatabaseHelper dbHelper)
        {
            _dbService = dbService;
            _dbHelper = dbHelper;
        }

        public async Task<List<UserModel>> GetUsersHierarchyAsync(int creatorId)
        {
            string query = @"
                SELECT u.UserID, u.UserName, u.CreatorID, u2.UserName AS CreatorName, u.FioID, u.BrigID, u.PasswordHash
                FROM Users u
                LEFT JOIN Users u2 ON u.CreatorId = u2.UserId
                WHERE u.UserId IN (
                    SELECT UserId FROM GetDescendants(@UserId)
                    UNION SELECT @UserId
                )";

            return await _dbService.GetListAsync<UserModel>(query, new { UserId = creatorId });
        }


        public async Task<int> SaveAsync(UserModel user)
        {
            var result = await _dbService.SaveEntityAsync("Users", "UserID", user);
            Console.WriteLine($"Создан пользователь {user.UserName}, фио ид: {user.FioID}, бриг ид: {user.BrigID},");
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
            string query = "SELECT f_id AS FioID, Fio FROM fio";
            return await _dbService.GetListAsync<FioDto>(query, new Dictionary<string, object>());
        }
        public async void SetPravaForAddUser(int newId)
        {
            string query = $@"SELECT RoleID FROM Roles WHERE RoleName = 'Базовая'";
            DataTable dt = await _dbHelper.ExecuteQueryAsync(query);
            int roleId = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["RoleID"]) : -1;
            UserRoleDataService userRoleDataService = new UserRoleDataService(new DbService(_dbHelper), _dbHelper);
            await userRoleDataService.AssignRoleAsync(newId, roleId);
            Console.WriteLine($"Назначены базовые ({roleId}) права, профиль:" + newId.ToString());
        }
    }
}
