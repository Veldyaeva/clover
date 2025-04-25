using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using SewingProduction.Helpers;
using SewingProduction.Interfaces;
using SewingProduction.Services;
using SewingProduction.Models;

namespace SewingProduction.form.UserDistribution.Models
{
    public class UserModel : INotifyPropertyChanged, INewable
    {
        private int _userId;
        private string _userName;
        private int _brigId;
        public string _brigName;
        private int _creatorId;
        private string _creatorName;
        private int _fioId;
        private string _fio;
        private string _passwordHash;

        public bool IsNew { get; set; } = true;

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

        [Column("BrigId")]
        public int BrigId
        {
            get => _brigId;
            set { if (_brigId != value) { _brigId = value; OnPropertyChanged(nameof(BrigId)); } }
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
    public class UserModelDataService
    {
        private readonly DbService _dbService;

        public UserModelDataService(DbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<List<UserModel>> GetUsersHierarchyAsync(int creatorId)
        {
            string query = @"
                SELECT u.UserId, u.UserName, u.CreatorId, u2.UserName AS CreatorName, u.FioId, f.Fio, u.BrigId, b.Brig, u.PasswordHash
                FROM Users u
                LEFT JOIN Users u2 ON u.CreatorId = u2.UserId
                LEFT JOIN fio f ON u.FioId = f.f_id
                LEFT JOIN spBrig b ON u.BrigId = b.id_brig
                WHERE u.UserId IN (
                    SELECT UserId FROM GetDescendants(@UserId)
                    UNION SELECT @UserId
                )";

            return await _dbService.GetListAsync<UserModel>(query, new { UserId = creatorId });
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            string query = @"SELECT u.UserID, u.UserName, u.CreatorID, u.FioID, f.fio AS FIO, u.PasswordHash
                             FROM Users u
                             LEFT JOIN fio f ON u.FioID = f.f_id
                             WHERE u.UserID = @id";
            return await _dbService.GetEntityAsync<UserModel>(query, new { id });
        }

        public async Task<int> SaveAsync(UserModel user)
        {
            return await _dbService.SaveEntityAsync("Users", "UserID", user);
        }

        public async Task DeleteAsync(UserModel user)
        {
            await _dbService.DeleteEntityAsync("Users", "UserID", user);
        }
    }
}
