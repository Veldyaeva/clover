using Microsoft.AspNet.Identity;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.UserDistribution.Forms
{
    public partial class UserHierarchy : CustomForm
    {
        DatabaseHelper dbHelper = new DatabaseHelper();
        DbService dbService;
        private readonly AllProfileDataService _allProfileDataService;
        private readonly UserModelDataService _userModelDataService;
        private readonly UserClass _user;
        private List<UserClass> _userHierarchy;
        public UserHierarchy(UserClass user) : base(user)
        {
            InitializeComponent();
            dbService = new DbService(dbHelper);
            _userModelDataService = new UserModelDataService(dbService, dbHelper);
            _allProfileDataService = new AllProfileDataService(dbHelper);
            _user = user;
        }
        private async void UsersHierarchy_Load(object sender, EventArgs e)
        {
            repositoryItemLookUpEditBrig.DataSource = await _userModelDataService.LoadBrigList();
            repositoryItemLookUpEditFio.DataSource = await _userModelDataService.LoadFioList();
        }
        private async void customGridControlUsers_Load(object sender, EventArgs e)
        {
            DataTable dataTable = await _allProfileDataService.GetUsers(_user.UserId);

            List<UserClass> allUsers = dataTable.AsEnumerable()
                .Select(row => new UserClass
                {
                    UserId = row.Field<int>("UserId"),
                    UserName = row.Field<string>("UserName"),
                    CreatorID = row.Field<int>("CreatorID"),
                    Fio = row.Field<string>("Fio"),
                    Brig = row.Field<string>("Brig"),
                    FioID = row.Table.Columns.Contains("FioID") && row["FioID"] != DBNull.Value ? row.Field<int>("FioID") : 0,
                    BrigID = row.Table.Columns.Contains("BrigID") && row["BrigID"] != DBNull.Value ? row.Field<int>("BrigID") : 0,
                    Children = new List<UserClass>()
                })
                .ToList();

            _userHierarchy = BuildUserHierarchy(allUsers, 0);

            bindingSourceUsers.DataSource = _userHierarchy;
        }

        private List<UserClass> BuildUserHierarchy(List<UserClass> allUsers, int rootUserId)
        {
            // Находим корневые элементы иерархии - пользователей, у которых CreatorID совпадает с rootUserId.
            List<UserClass> rootItems = allUsers.Where(u => u.CreatorID == rootUserId).ToList();

            // Для каждого корневого элемента вызываем метод BuildChildHierarchy, чтобы построить поддерево.
            foreach (UserClass rootItem in rootItems)
            {
                BuildChildHierarchy(rootItem, allUsers);
            }

            // Возвращаем список корневых элементов - начало иерархии.
            return rootItems;
        }

        // Рекурсивный метод для построения поддерева для каждого пользователя.
        private void BuildChildHierarchy(UserClass parent, List<UserClass> allUsers)
        {
            // Находим дочерние элементы для данного пользователя - пользователей, у которых CreatorID совпадает с UserId текущего пользователя.
            List<UserClass> childItems = allUsers.Where(u => u.CreatorID == parent.UserId).ToList();

            // Присваиваем список дочерних элементов свойству Children текущего пользователя.
            parent.Children = childItems;

            foreach (UserClass childItem in childItems)
            {
                BuildChildHierarchy(childItem, allUsers);
            }
        }

        private async void customButtonAddProfile_Click(object sender, EventArgs e)
        {
            gridViewUsers.AddNewRow();
        }
        private void gridViewUsers_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridViewUsers.GridControl.BeginInvoke(new Action(() =>
            {
                if (gridViewUsers.IsValidRowHandle(e.RowHandle))
                {
                    gridViewUsers.FocusedRowHandle = e.RowHandle;
                    gridViewUsers.ShowPopupEditForm();
                }
            }));
        }
        private async void gridViewUsers_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                var user = e.Row as UserModel;
                if (user == null) return;

                var hasher = new PasswordHasher();
                string password = string.IsNullOrWhiteSpace(user.Password) ? "0" : user.Password;
                user.PasswordHash = hasher.HashPassword(password);
                user.CreatorID = _user.UserId;

                int newUserId = await _userModelDataService.SaveAsync(user);
                user.UserID = newUserId;

                customGridControlUsers_Load(sender, e);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Message.Contains("UQ_UserName")) // если уникальность нарушена
                {
                    MessageBox.Show($"Пользователь с таким именем уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Ошибка сохранения: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                gridViewUsers.DeleteRow(gridViewUsers.FocusedRowHandle); // откат строки
            }
        }


    }
    public class AllProfileDataService
    {
        private readonly DatabaseHelper _dbHelper;
        public AllProfileDataService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<System.Data.DataTable> GetUsers(int CreatorID)
        {
            // SQL-запрос для получения данных пользователей с поддержкой иерархии.
            string query = $@"
            WITH UserHierarchy AS (
                SELECT
                    UserId,
                    UserName,
                    CreatorID,
                    FioID,
                    BrigID,
                    CAST(UserID AS VARCHAR(MAX)) AS UserPath,
                    0 AS Generation
                FROM Users
                WHERE CreatorID = 0

                UNION ALL

                SELECT
                    u.UserId,
                    u.UserName,
                    u.CreatorID,
                    u.FioID,
                    u.BrigID, 
                    uh.UserPath + ' > ' + CAST(u.UserId AS VARCHAR(MAX)),
                    uh.Generation + 1
                FROM
                    Users u
                INNER JOIN
                    UserHierarchy uh ON u.CreatorID = uh.UserId
            )
            SELECT
                uh.UserId,
                uh.UserName,
                uh.CreatorID,
                uh.UserPath,
                uh.Generation,
                uh.FioID,
                uh.BrigID,
                f.fio AS Fio,
                sb.Brig
            FROM
                UserHierarchy uh
            LEFT JOIN fio f ON f.f_id = uh.FioID
            LEFT JOIN spBrig sb ON sb.id_brig = uh.BrigID
            ORDER BY
                UserPath;";
            return await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@CreatorID", CreatorID } });
        }
    }
}
