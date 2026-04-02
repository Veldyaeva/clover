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
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.UserDistribution.Class;
using SewingProduction.Features.UserDistribution.DataService;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution.Models;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SewingProduction.Helpers;
using DevExpress.XtraGrid.Views.Base;

namespace SewingProduction.Features.UserDistribution.Forms
{
    public partial class UserPodr : CustomForm
    {
        private readonly UserModelDataService _userModelDataService;
        private readonly UserPodrDataService _userPodrDataService;
        private UserModel _selectedUser;
        private List<RolePodrModel> _selectedRolePodr;
        private List<UserPodrModel> _selectedUserPodr;
        private List<RolePodrModel> _currentUserRolePodr;
        public UserPodr(UserClass user) : base(user)
        {
            InitializeComponent();
            _userPodrDataService = new UserPodrDataService();
            _userModelDataService = new UserModelDataService();
        }
        #region Initialization
        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            await InitializeFormAsync();
            HideTechnicalColumns();
            ApplyUserSorting();
        }
        private async Task InitializeFormAsync()
        {
            customGridControlUser.DataSource =
                await _userModelDataService.GetUsersHierarchyAsync(_user);

            _currentUserRolePodr =
                await _userPodrDataService.GetAvailablePodrTablesByRoleAsync(
                    CurrentUser.User.UserId) ?? new List<RolePodrModel>();

            _selectedUserPodr =
                await _userPodrDataService.LoadAllUserPodrAsync() ?? new List<UserPodrModel>();

            _selectedRolePodr = new List<RolePodrModel>();
            customGridControlPodr.DataSource = new List<UserPodrModel>();
        }
        public async Task<List<UserPodrModel>> LoadPodrAsync(int? userId = null)
        {
            var result = new List<UserPodrModel>();

            var tables = _selectedRolePodr ?? new List<RolePodrModel>();

            foreach (var table in tables)
            {
                var part = await _userPodrDataService.LoadPodrFromTableAsync(
                    table.PodrTableID,
                    table.PodrTableName);

                if (part != null && part.Count > 0)
                    result.AddRange(part);
            }

            return result;
        }
        private async Task ReloadPodrForSelectedUserAsync()
        {
            if (_selectedUser == null)
            {
                _selectedRolePodr = new List<RolePodrModel>();
                customGridControlPodr.DataSource = new List<UserPodrModel>();
                return;
            }

            var selectedUserRolePodr =
                await _userPodrDataService.GetAvailablePodrTablesByRoleAsync(_selectedUser.UserID)
                ?? new List<RolePodrModel>();

            var currentUserRolePodr = _currentUserRolePodr ?? new List<RolePodrModel>();

            var allowedTableIds = currentUserRolePodr
                .Select(x => x.PodrTableID)
                .Intersect(selectedUserRolePodr.Select(x => x.PodrTableID))
                .ToHashSet();

            _selectedRolePodr = selectedUserRolePodr
                .Where(x => allowedTableIds.Contains(x.PodrTableID))
                .GroupBy(x => x.PodrTableID)
                .Select(g => g.First())
                .ToList();

            customGridControlPodr.DataSource = await LoadPodrAsync();

            ApplyPodrChecksFromCache(_selectedUser.UserID);
            ApplyPodrGroupingAndSorting();
        }
        #endregion 

        #region GridViewClick
        private async void gridViewUser_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != IsSelected) return;
            if (!(bool)e.Value) return;

            await SelectUserRow(e.RowHandle);
        }
        private async Task SelectUserRow(int rowHandle)
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

            await ReloadPodrForSelectedUserAsync();
        }
        private void ApplyPodrChecksFromCache(int userId)
        {
            var userLinks = _selectedUserPodr
                .Where(x => x.UserID == userId)
                .ToDictionary(
                    x => (x.PodrTableID, x.PodrID),
                    x => x.UserPodrID);

            gridViewPodr.BeginUpdate();
            try
            {
                for (int i = 0; i < gridViewPodr.RowCount; i++)
                {
                    var row = gridViewPodr.GetRow(i) as UserPodrModel;
                    if (row == null)
                        continue;

                    if (userLinks.TryGetValue(
                            (row.PodrTableID, row.PodrID),
                            out int userPodrId))
                    {
                        row.IsSelected = true;
                        row.UserPodrID = userPodrId;
                        row.UserID = userId;
                    }
                    else
                    {
                        row.IsSelected = false;
                        row.UserPodrID = 0;
                        row.UserID = userId;
                    }
                }
            }
            finally
            {
                gridViewPodr.EndUpdate();
            }
        }

        private async void gridViewPodr_CellValueChanged(
        object sender,
        DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != IsSelectedPodr)
                return;

            if (_selectedUser == null)
            {
                gridViewPodr.SetRowCellValue(e.RowHandle, IsSelectedPodr, false);
                return;
            }

            int oldFocusedHandle = gridViewPodr.FocusedRowHandle;
            int oldTopRowIndex = gridViewPodr.TopRowIndex;

            var row = gridViewPodr.GetRow(e.RowHandle) as UserPodrModel;

            if (row == null)
                return;

            bool isChecked = Convert.ToBoolean(e.Value);

            row.IsSelected = isChecked;

            if (isChecked)
            {
                int newId = await _userPodrDataService.SaveAsync(new UserPodrModel
                {
                    UserID = _selectedUser.UserID,
                    PodrID = row.PodrID,
                    PodrTableID = row.PodrTableID,
                    CreatorID = CurrentUser.User.UserId
                });

                row.UserPodrID = newId;

                _selectedUserPodr.Add(new UserPodrModel
                {
                    UserPodrID = newId,
                    UserID = _selectedUser.UserID,
                    PodrID = row.PodrID,
                    PodrTableID = row.PodrTableID
                });
            }
            else
            {
                await _userPodrDataService.DeleteAsync(row);

                _selectedUserPodr.RemoveAll(x => x.UserPodrID == row.UserPodrID);

                row.UserPodrID = 0;
            }

            gridViewPodr.TopRowIndex = oldTopRowIndex;
            gridViewPodr.FocusedRowHandle = oldFocusedHandle;
        }
        private void repositoryItemCheckEditPodr_CheckedChanged(object sender, EventArgs e)
        {
            gridViewPodr.PostEditor();
            gridViewPodr.UpdateCurrentRow();
        }

        private void repositoryItemCheckEditUser_CheckedChanged(object sender, EventArgs e)
        {
            gridViewUser.PostEditor();
            gridViewUser.UpdateCurrentRow();
        }

        #endregion

        #region RowClick
        private async void gridViewUser_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle < 0) return;
            await SelectUserRow(e.RowHandle);
        }

        private void gridViewPodr_RowClick(object sender, RowClickEventArgs e)
        {

        }

        #endregion

        #region KeyBoardClick
        private async void gridViewUser_KeyDown(object sender, KeyEventArgs e)
        {
            var view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;

            if (e.KeyCode == Keys.Right)
            {
                customGridControlPodr.Focus();
                gridViewPodr.Focus();
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
                    await SelectUserRow(newHandle);

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
                    view.GetRowCellValue(handle, IsSelectedPodr));

                view.SetRowCellValue(handle, IsSelectedPodr, !current);

                view.PostEditor();
                view.UpdateCurrentRow();

                e.Handled = true;
            }
        }

        #endregion 

        #region Technical
        private void HideTechnicalColumns()
        {
            gridViewUser.BeginUpdate();
            gridViewPodr.BeginUpdate();
            try
            {
                gridViewUser.Columns["UserID"].Visible = false;
                gridViewUser.Columns["FioID"].Visible = false;

                gridViewPodr.Columns["PodrID"].Visible = false;
                gridViewPodr.Columns["PodrTableID"].Visible = false;
                gridViewPodr.Columns["UserPodrID"].Visible = false;

                gridViewPodr.Columns["IsSelected"].VisibleIndex = 1;
                gridViewPodr.Columns["Name"].VisibleIndex = 2;
                gridViewPodr.Columns["PodrTableName"].VisibleIndex = 3;
            }
            finally
            {
                gridViewUser.EndUpdate();
                gridViewPodr.EndUpdate();
            }
        }
        private void ApplyUserSorting()
        {
            gridViewUser.BeginSort();
            try
            {
                gridViewUser.ClearSorting();
                gridViewUser.Columns[nameof(UserModel.Fio)]
                    .SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            }
            finally
            {
                gridViewUser.EndSort();
            }
        }

        private void ApplyPodrGroupingAndSorting()
        {
            var colSelected = gridViewPodr.Columns[nameof(UserPodrModel.IsSelected)];
            var colOrg = gridViewPodr.Columns[nameof(UserPodrModel.PodrTableName)];
            var colName = gridViewPodr.Columns[nameof(UserPodrModel.Name)];

            gridViewPodr.BeginUpdate();
            try
            {
                gridViewPodr.ClearGrouping();
                gridViewPodr.ClearSorting();

                colSelected.GroupIndex = 0;

                colSelected.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;

                colName.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                colName.SortIndex = 1;

                colOrg.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                colOrg.SortIndex = 2;

                gridViewPodr.OptionsBehavior.AutoExpandAllGroups = true;
                gridViewPodr.OptionsView.ShowGroupPanel = false;

                gridViewPodr.Columns["IsSelected"].Visible = true;
                gridViewPodr.OptionsView.ShowGroupedColumns = true;
            }
            finally
            {
                gridViewPodr.EndUpdate();
            }
        }

        private void gridViewPodr_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            var view = (GridView)sender;

            var info = e.Info as GridGroupRowInfo;
            if (info == null)
                return;

            // группируем только по IsSelected
            if (info.Column == null || info.Column.FieldName != nameof(UserPodrModel.IsSelected))
                return;

            // RowHandle группы
            int groupHandle = e.RowHandle;

            // значение группы берём у View
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
