using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.form.UserDistribution.Models;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;

namespace SewingProduction.form.UserDistribution
{
    public partial class AllUser : CustomForm
    {
        private readonly UserModel _userModel;
        //private BindingList<UserModel> _userModelList;
        DatabaseHelper dbHelper = new DatabaseHelper("ace");
        DbService dbService;
        //private readonly UserModelDataService _userModelDataService = new UserModelDataService(new DbService(new DatabaseHelper("ace")));
        //private readonly UserRoleDataService _userRoleDataService = new UserRoleDataService(new DbService(new DatabaseHelper("ace")), new DatabaseHelper("ace"));
        //private readonly AllRoleDataService _allRoleDataService = new AllRoleDataService(new DatabaseHelper("ace"));
        private readonly UserModelDataService _userModelDataService;
        private readonly UserRoleDataService _userRoleDataService;
        private readonly AllRoleDataService _allRoleDataService;
        private readonly AllProfileDataService _allProfileDataService;
        private readonly UserClass _user;
        private int selectedRoleId = -1;
        private int selectedUserId = -1;
        public AllUser(UserClass user) : base(user)
        {
            InitializeComponent();
            dbService = new DbService(dbHelper);
            _userModelDataService = new UserModelDataService(dbService, dbHelper);
            _userRoleDataService = new UserRoleDataService(dbService, dbHelper);
            _allRoleDataService = new AllRoleDataService(dbHelper);
            _allProfileDataService = new AllProfileDataService(dbHelper);
            _user = user;
            SetupGrid();
        }
        private async void AllUser_Load(object sender, EventArgs e)
        {
            repositoryItemLookUpEditBrig.DataSource = await _userModelDataService.LoadBrigList();
            repositoryItemLookUpEditFio.DataSource = await _userModelDataService.LoadFioList();
            //customGridControlUser.InitializeAccess(_user, this.Name, new List<string> { "Users" });
        }
        private void SetupGrid()
        {
            customGridControlUser.LevelTree.Nodes[0].RelationName = "Роли";
            customGridControlUser.LevelTree.Nodes[0].LevelTemplate = gridViewRoles;
            gridViewUsers.MasterRowGetRelationCount += (s, e) => e.RelationCount = 1;
            gridViewUsers.MasterRowGetRelationName += (s, e) => e.RelationName = "Роли";
            gridViewUsers.MasterRowGetChildList += async (s, e) =>
            {
                e.ChildList = (await _userRoleDataService.GetRolesWithFlags(selectedUserId)).DefaultView;
            };

            gridViewRoles.CellValueChanging += async (s, e) =>
            {
                Console.WriteLine("CellValueChanging");
                if (e.Column.FieldName == "HasRole")
                {
                    GridView roleView = s as GridView;
                    var row = roleView.GetDataRow(e.RowHandle);
                    int roleId = Convert.ToInt32(row["RoleID"]);
                    int userId = Convert.ToInt32(row["UserID"]);
                    bool hasRole = Convert.ToBoolean(e.Value);
                    if (hasRole)
                    {
                        await _userRoleDataService.AssignRoleAsync(userId, roleId);
                    }
                    else
                    {
                        await _userRoleDataService.RemoveRoleAsync(userId, roleId);
                    }
                }
            };
        }
        private void gridViewRoles_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {

        }
        private async void customGridControlUsers_Load(object sender, EventArgs e)
        {
            await LoadUsers();
        }
        private async Task LoadUsers()
        {
            bindingSourceUsers.DataSource = await _userModelDataService.GetUsersHierarchyAsync(_user.UserId);
        }
        #region добавление/редакитрование пользователя
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
        #endregion
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

        private void customButtonShareUser_Click(object sender, EventArgs e)
        {
            gridViewUsers.ExpandMasterRow(0);
        }
        /// <summary>
        /// Копирование пользователя
        /// </summary>
        private async void customButtonCopyUser_Click(object sender, EventArgs e)
        {
            var currentUser = gridViewUsers.GetFocusedRow() as UserModel;
            if (currentUser == null) return;

            int copyUserId = currentUser.UserID;
            var copyUser = new UserModel
            {
                UserName = "Копия_" + currentUser.UserName,
                FioID = currentUser.FioID,
                BrigID = currentUser.BrigID,
                CreatorID = _user.UserId,
                Password = "",
                PasswordHash = currentUser.PasswordHash
            };

            int newUserId = await _userModelDataService.SaveAsync(copyUser);

            var roleIds = await _userRoleDataService.GetRoleIdsByUser(copyUserId);
            foreach (var roleId in roleIds)
            {
                await _userRoleDataService.AssignRoleAsync(newUserId, roleId);
            }

            await LoadUsers();

            int lastRowHandle = gridViewUsers.RowCount - 1;
            if (lastRowHandle >= 0)
            {
                gridViewUsers.FocusedRowHandle = lastRowHandle;
                gridViewUsers.ShowPopupEditForm();
            }
        }
        /// <summary>
        /// Удаление пользователя
        /// </summary>
        private async void customButtonDeleteUser_Click(object sender, EventArgs e)
        {
            var user = gridViewUsers.GetFocusedRow() as UserModel;
            if (user == null) return;

            string message = "Вы уверены что хотите удалить '" + user.UserName + "' ?";
            var result = MessageBox.Show(message, "Удалить?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                await _userModelDataService.DeleteAsync(user);
                Console.WriteLine("удален пользователь, ID " + user.UserID);
                gridViewUsers.DeleteRow(gridViewUsers.FocusedRowHandle);
            }
        }
    }
}