using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

namespace SewingProduction.Features.UserDistribution.Forms.User
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

         }
        #endregion 

        #region GridViewClick
        private void gridViewUser_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != IsSelected) return;
            if (!(bool)e.Value) return;

            SelectUserRow(e.RowHandle);
        }
        private void SelectUserRow(int rowHandle)
        {
            var view = gridViewUser;

            var user = view.GetRow(rowHandle) as UserModel;
            if (user == null) return;

            view.BeginUpdate();
            try
            {
                for (int i = 0; i < view.RowCount; i++)
                {
                    int rh = view.GetVisibleRowHandle(i);
                    if (rh < 0) continue;

                    view.SetRowCellValue(rh, IsSelected, rh == rowHandle);
                }
            }
            finally
            {
                view.EndUpdate();
            }

            _selectedUser = user;

            ApplyRoleChecksFromCache(_selectedUser.UserID);
        }
        private void ApplyRoleChecksFromCache(int userId)
        {
            //var userLinks = _selectedUserRole
            //    .Where(x => x.UserID == userId)
            //    .ToDictionary(
            //        x => (x.PodrTableID, x.PodrID),
            //        x => x.UserPodrID);

            //gridViewRole.BeginUpdate();
            //try
            //{
            //    for (int i = 0; i < gridViewRole.RowCount; i++)
            //    {
            //        var row = gridViewRole.GetRow(i) as UserPodrModel;
            //        if (row == null)
            //            continue;

            //        if (userLinks.TryGetValue(
            //                (row.PodrTableID, row.PodrID),
            //                out int userPodrId))
            //        {
            //            row.IsSelected = true;
            //            row.UserPodrID = userPodrId;
            //            row.UserID = userId;
            //        }
            //        else
            //        {
            //            row.IsSelected = false;
            //            row.UserPodrID = 0;
            //            row.UserID = userId;
            //        }
            //    }
            //}
            //finally
            //{
            //    gridViewRole.EndUpdate();
            //}
        }

        private async void gridViewPodr_CellValueChanged(
        object sender,
        DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != IsSelectedRole)
                return;

            if (_selectedUser == null)
            {
                gridViewRole.SetRowCellValue(e.RowHandle, IsSelectedRole, false);
                return;
            }

            int oldFocusedHandle = gridViewRole.FocusedRowHandle;
            int oldTopRowIndex = gridViewRole.TopRowIndex;

            var row = gridViewRole.GetRow(e.RowHandle) as UserPodrModel;

            if (row == null)
                return;

            bool isChecked = Convert.ToBoolean(e.Value);

            row.IsSelected = isChecked;

            //if (isChecked)
            //{
            //    int newId = await _userRoleDataService.SaveAsync(new UserRoleModel
            //    {
            //        UserID = _selectedUser.UserID,
            //        PodrID = row.PodrID,
            //        PodrTableID = row.PodrTableID,
            //        CreatorID = CurrentUser.User.UserId
            //    });

            //    row.UserPodrID = newId;

            //    _selectedUserRole.Add(new UserRoleModel
            //    {
            //        UserPodrID = newId,
            //        UserID = _selectedUser.UserID,
            //        PodrID = row.PodrID,
            //        PodrTableID = row.PodrTableID
            //    });
            //}
            //else
            //{
            //    await _userRoleDataService.DeleteAsync(row);

            //    _selectedUserRole.RemoveAll(x => x.UserRoleID == row.UserRoleID);

            //    row.UserRoleID = 0;
            //}

            gridViewRole.TopRowIndex = oldTopRowIndex;
            gridViewRole.FocusedRowHandle = oldFocusedHandle;
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
        private void gridViewUser_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle < 0) return;
            SelectUserRow(e.RowHandle);
        }

        private void gridViewPodr_RowClick(object sender, RowClickEventArgs e)
        {

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
                    view.GetRowCellValue(handle, IsSelectedRole));

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

            if (info.Column == null || info.Column.FieldName != nameof(UserPodrModel.IsSelected))
                return;

            int groupHandle = e.RowHandle;

            object groupValueObj = view.GetGroupRowValue(groupHandle);
            bool isSelectedGroup = groupValueObj != null && Convert.ToBoolean(groupValueObj);

            int childCount = view.GetChildRowCount(groupHandle);
            info.GroupText = isSelectedGroup
                ? $"Выбранные ({childCount})"
                : $"Доступные для выбора ({childCount})";
        }

        #endregion

        private void customGridControlUser_Click(object sender, EventArgs e)
        {

        }
    }
}
