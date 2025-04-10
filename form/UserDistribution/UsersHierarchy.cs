using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.AspNetCore.Identity;
using SewingProduction.Helpers;

namespace SewingProduction.form.UserDistribution
{
    public partial class UsersHierarchy : CustomForm
    {
    private readonly AllProfileDataService _allProfileDataService;
    DatabaseHelper dbHelper = new DatabaseHelper("ace");
    private readonly UserClass _user;
    private List<UserClass> _userHierarchy;
        public UsersHierarchy(UserClass user) : base(user)
        {
            InitializeComponent();
            _allProfileDataService = new AllProfileDataService(dbHelper);
            _user = user;
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
                    Fio = row.Field<string>("FIO"), 
                    Children = new List<UserClass>()
                })
                .ToList();

            _userHierarchy = BuildUserHierarchy(allUsers, _user.UserId);

            bindingSourceUsers.DataSource = _userHierarchy;

            //ConfigureGridView(gridViewUsers);

        }

        private void ConfigureGridView(GridView view)
        {
            if (view.Columns["UserId"] != null) view.Columns["UserId"].Visible = false;
            if (view.Columns["CreatorID"] != null) view.Columns["CreatorID"].Visible = false;
            if (view.Columns["FioID"] != null) view.Columns["FioID"].Visible = false;
            if (view.Columns["BrigID"] != null) view.Columns["BrigID"].Visible = false;

            if (view.Columns["UserName"] != null) view.Columns["UserName"].Caption = "Имя пользователя";

            if (view.Columns["Fio"] != null) view.Columns["Fio"].Caption = "ФИО";

            if (view.Columns["Brig"] != null) view.Columns["Brig"].Caption = "Бригада";

            if (view.Columns["Fio"] == null)
            {
                DevExpress.XtraGrid.Columns.GridColumn fioColumn = view.Columns.AddField("Fio");
                fioColumn.VisibleIndex = 1;
                fioColumn.Caption = "ФИО";  
                fioColumn.FieldName = "Fio"; 
            }
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
            var user = e.Row as UserClass;

            if (user == null) return;

            if (user.UserId > 0)
            {
                // обновление, если нужно
                return;
            }

            var hasher = new PasswordHasher<UserClass>();
            string passwordHash = hasher.HashPassword(null, "0");

            int newUserId = await _allProfileDataService.AddUser(
                user.UserName,
                passwordHash,
                user.Fio,
                _user.UserId,
                user.BrigID
            );

            user.UserId = newUserId;

            customGridControlUsers_Load(sender, e);
        }

        private void customGridControlAllProfile_Click(object sender, EventArgs e)
        {

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
                WHERE CreatorID = @CreatorID 

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
                f.fio AS FIO,
                sb.Brig
            FROM
                UserHierarchy uh
            LEFT JOIN fio f ON f.f_id = uh.FioID
            LEFT JOIN spBrig sb ON sb.id_brig = uh.BrigID
            ORDER BY
                UserPath;";
            return await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@CreatorID", CreatorID } });
        }
        public async Task<System.Data.DataTable> GetUserRole(int CreatorID)
        {
            string query = $@"
                SELECT * FROM UserRole ";
            return await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@CreatorID", CreatorID } });
        }
        public async Task<int> AddUser(string userName, string passwordHash, string fio, int creatorId, int brigId)
        {
            string query = @"
            INSERT INTO Users (UserName, PasswordHash, CreatorID, BrigID)
            OUTPUT INSERTED.UserId
            VALUES (@UserName, @PasswordHash, @CreatorID, @BrigID)";

            var result = await _dbHelper.ExecuteScalarAsync(query, new Dictionary<string, object>
            {
                { "@UserName", userName },
                { "@PasswordHash", passwordHash },
                { "@CreatorID", creatorId },
                { "@BrigID", brigId }
            });

            return Convert.ToInt32(result);
        }
    }
}
