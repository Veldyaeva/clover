using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SewingProduction.Features.UserDistribution.Class;
using SewingProduction.Features.UserDistribution.DataService;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution.Models;

namespace SewingProduction.Features.UserDistribution.Forms
{
    public partial class UserRole : CustomForm
    {
        private readonly UserModelDataService _userModelDataService = new UserModelDataService();
        private readonly RoleDataService _roleService = new RoleDataService();
        private readonly UserRoleDataService _userRoleDataService = new UserRoleDataService();
        private UserModel _selectedUser;
        private List<RoleModel> _selectedRole;
        private List<UserRoleModel> _selectedUserRole;
        public UserRole(UserClass user) : base(user)
        {
            InitializeComponent();
        }
        public UserRole(UserClass user, UserModel selectedUser) : base(user)
        {
            InitializeComponent();
            _selectedUser = selectedUser;
        }
        #region Initialization
        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            await InitializeFormAsync();
        }
        private async Task InitializeFormAsync()
        {
            customGridControlUser.DataSource =
                await _userModelDataService.GetUsersHierarchyAsync(_user);

            customGridControlRole.DataSource 
                = await _roleService.GetListRolesAsync(_user);

            if (_selectedUser != null)
                SelectUserById(_selectedUser.UserID);
        }

        private void SelectUserById(int userId)
        {
            for (int i = 0; i < gridViewUser.RowCount; i++)
            {
                var row = gridViewUser.GetRow(i) as UserModel;
                if (row == null)
                    continue;

                if (row.UserID == userId)
                {
                    SelectUserRow(i);
                    gridViewUser.FocusedRowHandle = i;
                    return;
                }
            }
        }
        #endregion 

        #region GridViewClick
        private void gridViewUser_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != IsSelected)
                return;

            if (!(bool)e.Value)
                return;

            SelectUserRow(e.RowHandle);
        }

        private async void SelectUserRow(int rowHandle)
        {
            var view = gridViewUser;

            var user = view.GetRow(rowHandle) as UserModel;
            if (user == null)
                return;

            view.BeginUpdate();
            try
            {
                for (int i = 0; i < view.RowCount; i++)
                {
                    int rh = view.GetVisibleRowHandle(i);
                    if (rh < 0)
                        continue;

                    view.SetRowCellValue(rh, IsSelected, rh == rowHandle);
                }
            }
            finally
            {
                view.EndUpdate();
            }

            _selectedUser = user;

            await LoadRolesForSelectedUserAsync(_selectedUser.UserID);
        }

        private async Task LoadRolesForSelectedUserAsync(int userId)
        {
            if (_selectedUser == null)
            {
                customGridControlRole.DataSource = null;
                return;
            }

            DataTable dtRoles = await _userRoleDataService.GetRolesForUser(userId);

            HashSet<int> assignedRoleIds = new HashSet<int>();

            foreach (DataRow dr in dtRoles.Rows)
            {
                assignedRoleIds.Add(Convert.ToInt32(dr["RoleID"]));
            }

            gridViewRole.BeginUpdate();
            try
            {
                for (int i = 0; i < gridViewRole.RowCount; i++)
                {
                    var row = gridViewRole.GetRow(i) as RoleModel;
                    if (row == null)
                        continue;

                    row.IsSelected = assignedRoleIds.Contains(row.RoleID);
                }
            }
            finally
            {
                gridViewRole.EndUpdate();
            }

            gridViewRole.RefreshData();

            if (gridViewRole.Columns["IsSelected"] != null)
                gridViewRole.Columns["IsSelected"].GroupIndex = 0;

            gridViewRole.ExpandAllGroups();
        }

        private async void gridViewPodr_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != IsSelectedRole)
                return;

            if (_selectedUser == null)
            {
                MessageBox.Show("Сначала выберите пользователя.");
                return;
            }

            var row = gridViewRole.GetRow(e.RowHandle) as RoleModel;
            if (row == null)
                return;

            int roleId = row.RoleID;
            bool isSelected = row.IsSelected;

            try
            {
                if (isSelected)
                    await _userRoleDataService.AssignRoleAsync(_selectedUser.UserID, roleId);
                else
                    await _userRoleDataService.RemoveRoleAsync(_selectedUser.UserID, roleId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при назначении роли: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await LoadRolesForSelectedUserAsync(_selectedUser.UserID);
                return;
            }
        }
        private void repositoryItemCheckEditPodr_CheckedChanged(object sender, EventArgs e)
        {
            gridViewRole.PostEditor();
            gridViewRole.UpdateCurrentRow();
        }

        private void repositoryItemCheckEditUser_CheckedChanged(object sender, EventArgs e)
        {
            gridViewUser.PostEditor();
            gridViewUser.UpdateCurrentRow();
        }

        #endregion

        #region RowClick

        private void gridViewUser_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            SelectUserRow(e.RowHandle);
        }

        private void gridViewPodr_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            if (e.Clicks == 2 && e.RowHandle >= 0)
            {
                bool currentValue = Convert.ToBoolean(gridViewRole.GetRowCellValue(e.RowHandle, IsSelectedRole));
                gridViewRole.SetRowCellValue(e.RowHandle, IsSelectedRole, !currentValue);
            }
        }

        #endregion

        #region KeyBoardClick
        private void gridViewUser_KeyDown(object sender, KeyEventArgs e)
        {
            var view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;

            if (e.KeyCode == Keys.Right)
            {
                customGridControlRole.Focus();
                gridViewRole.Focus();
                e.Handled = true;
                return;
            }

            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                if (e.KeyCode == Keys.Up)
                    view.MovePrev();
                else
                    view.MoveNext();

                int newHandle = view.FocusedRowHandle;
                if (newHandle >= 0)
                    SelectUserRow(newHandle);

                e.Handled = true;
                return;
            }
        }

        private void gridViewPodr_KeyDown(object sender, KeyEventArgs e)
        {
            var view = (GridView)sender;

            if (e.KeyCode == Keys.Left)
            {
                customGridControlUser.Focus();
                gridViewUser.Focus();
                e.Handled = true;
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                int handle = view.FocusedRowHandle;
                if (handle < 0)
                    return;

                bool current = Convert.ToBoolean(
                    view.GetRowCellValue(handle, IsSelected));

                view.SetRowCellValue(handle, IsSelectedRole, !current);

                view.PostEditor();
                view.UpdateCurrentRow();

                e.Handled = true;
            }
        }

        #endregion

        #region Technical
        private void gridViewPodr_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            var view = (GridView)sender;
            var info = e.Info as GridGroupRowInfo;

            if (info == null)
                return;

            if (info.Column == null || info.Column.FieldName != "IsSelected")
                return;

            int childCount = view.GetChildRowCount(e.RowHandle);
            bool isSelectedGroup = Convert.ToBoolean(view.GetGroupRowValue(e.RowHandle));

            info.GroupText = isSelectedGroup
                ? $"Назначенные роли ({childCount})"
                : $"Доступные роли ({childCount})";
        }

        #endregion

        private void customGridControlUser_Click(object sender, EventArgs e)
        {

        }
    }
}
