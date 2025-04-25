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
using SewingProduction.form.UserDistribution.Models;
using SewingProduction.Helpers;

namespace SewingProduction.form.UserDistribution
{
    public partial class AllUser : CustomForm
    {
        private readonly AllUserDataService _allUserDataService;
        private readonly UserModel _userModel;
        DatabaseHelper dbHelper = new DatabaseHelper("ace");
        private readonly UserClass _user;
        private int selectedRoleId = -1;
        private int selectedUserId = -1;
        public AllUser(UserClass user) : base(user)
        {
            InitializeComponent();
            _allUserDataService = new AllUserDataService(dbHelper);
            _user = user;
            SetupGrid();
        }
        private void SetupGrid()
        {
            customGridControlUser.LevelTree.Nodes[0].RelationName = "Роли";
            customGridControlUser.LevelTree.Nodes[0].LevelTemplate = gridViewRoles;

            //customGridControlUser.LevelTree.Nodes.Add("Роли", gridViewRoles);
            gridViewUsers.MasterRowGetRelationCount += (s, e) => e.RelationCount = 1;
            gridViewUsers.MasterRowGetRelationName += (s, e) => e.RelationName = "Роли";
            gridViewUsers.MasterRowGetChildList += async (s, e) =>
            {
                var view = s as GridView;
                var row = view.GetDataRow(e.RowHandle);
                int userId = Convert.ToInt32(row["UserID"]);
                e.ChildList = (await _allUserDataService.GetRolesWithFlags(userId)).DefaultView;
            };

            gridViewRoles.CellValueChanged += async (s, e) =>
            {
                if (e.Column.FieldName == "HasRole")
                {
                    GridView roleView = s as GridView;
                    var row = roleView.GetDataRow(e.RowHandle);
                    int roleId = Convert.ToInt32(row["RoleID"]);
                    int userId = Convert.ToInt32(row["UserID"]);
                    bool hasRole = Convert.ToBoolean(e.Value);

                    if (hasRole)
                        await _allUserDataService.AssignRole(userId, roleId);
                    else
                        await _allUserDataService.RemoveRole(userId, roleId);
                }
            };
        }
        private async void customGridControlUsers_Load(object sender, EventArgs e)
        {
            await LoadUsers();
        }
        private async Task LoadUsers()
        {
            bindingSourceUsers.DataSource = await _allUserDataService.GetUsersHierarchy(_user.UserId);
        }

        private void customButtonAddUser_Click(object sender, EventArgs e)
        {
            gridViewUsers.AddNewRow();
        }

        private void gridViewUsers_InitNewRow(object sender, InitNewRowEventArgs e)
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
            Console.WriteLine("RowUpdated start");
            DataRow row = ((DataRowView)e.Row).Row;
            if (row == null)
                return;

            int rowHandle = gridViewUsers.GetRowHandle(gridViewUsers.DataSource is BindingSource ? ((BindingSource)gridViewUsers.DataSource).IndexOf(e.Row) : -1);

            int id = row["UserID"] != DBNull.Value ? Convert.ToInt32(row["UserID"]) : 0;

            string UserName = row["UserName"]?.ToString() ?? "";
            try
            {
                if (id > 0)
                {
                    await _allUserDataService.UpdateUser(UserName, id);
                }
                else
                {
                    int newId = await _allUserDataService.InsertUser(UserName, _user.UserId);
                    row["RoleID"] = newId;
                }
                await LoadUsers();
                Console.WriteLine("RowUpdated");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Message.Contains("UQ_UserName"))
                    MessageBox.Show($"Пользователь с именем \"{UserName}\" уже существует. Имя должно быть уникальным.", "Ошибка добавления", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Ошибка базы данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gridViewUsers.DeleteRow(gridViewUsers.FocusedRowHandle);
            }

        }
        private void gridViewUsers_FocusedRow()
        {
            var Id = gridViewUsers.GetFocusedRowCellValue("UserID");
            selectedUserId = (Id != null && Id != DBNull.Value) ? Convert.ToInt32(Id) : 0;
            Console.WriteLine("selected UserId: " + selectedUserId);
        }
        private void gridViewRoles_FocusedRow()
        {
            GridView activeView = customGridControlUser.FocusedView as GridView;
            if (activeView != null && activeView.Name == "gridViewRoles" && activeView.FocusedRowHandle >= 0)
            {
                var Id = activeView.GetFocusedRowCellValue("RoleID");
                selectedRoleId = Id != null ? Convert.ToInt32(Id) : 0;
                Console.WriteLine("selected RoleId: " + selectedRoleId);
            }
        }

        private void gridViewUsers_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridViewUsers_FocusedRow();
        }

        private void gridViewRoles_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridViewRoles_FocusedRow();
        }
        private async Task<bool> checkEditUser()
        {
            if (selectedRoleId <= 0)
                gridViewRoles_FocusedRow();
            return true;
        }
    }
    public class AllUserDataService
    {
        private readonly DatabaseHelper _dbHelper;
        public AllUserDataService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<DataTable> GetUsersHierarchy(int userId)
        {
            string query = @"SELECT UserID, UserName, FIOID, f.fio FROM Users u 
                            LEFT JOIN fio f ON f.f_id = u.FioID
                            WHERE UserID IN (SELECT UserID FROM GetDescendants(@UserID) UNION SELECT @UserID)";
            return await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@UserID", userId } });
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

        public async Task AssignRole(int userId, int roleId)
        {
            string query = "INSERT INTO UserRoles (UserID, RoleID) VALUES (@UserID, @RoleID)";
            await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object>
            {
                { "@UserID", userId },
                { "@RoleID", roleId }
            });
        }

        public async Task RemoveRole(int userId, int roleId)
        {
            string query = "DELETE FROM UserRoles WHERE UserID = @UserID AND RoleID = @RoleID";
            await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object>
            {
                { "@UserID", userId },
                { "@RoleID", roleId }
            });
        }
        public async Task UpdateUser(object eValue, int userId)
        {
            string query = $@"UPDATE Users SET UserName = @eValue 
                              WHERE UserID = @UserID";
            await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object>
            {
                { "@UserID", userId },
                { "@eValue", eValue }
            });
        }

        public async Task<int> InsertUser(string eUserName, int CreatorID)
        {
            string query = $@"
                INSERT INTO Users (UserName,CreatorID)
                OUTPUT INSERTED.UserID
                VALUES (@UserName,@CreatorID)";
            var result = await _dbHelper.ExecuteScalarAsync(query, new Dictionary<string, object>
            {
                { "@UserName", eUserName },
                { "@CreatorID", CreatorID }
            });
            Console.WriteLine($"InsertUsers => result: {result}");
            return Convert.ToInt32(result);
        }
    }
    //public class UserInAllUser
    //{
    //    private int UserID;
    //    public string UserName;
    //    private string PasswordHash;
    //    private int FioID;
    //    public string FioName;
    //    private int CreatorID;
    //    public string CreatorName;
    //    private int BrigID;
    //    public string BrigName;
    //}
}