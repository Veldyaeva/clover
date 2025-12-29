using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.DocumentServices.ServiceModel.DataContracts;
using SewingProduction.Features.UserDistribution.Class;
using SewingProduction.Features.UserDistribution.DataService;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution.Models;

namespace SewingProduction.Features.UserDistribution.Forms
{
    public partial class ShareUser : CustomForm
    {
        private readonly UserModelDataService _userModelDataService;
        private readonly ShareUserDataService _shareUserDataService;
        private UserModel _selectedUser;
        private List<DistributionModel> _currentUserDistribution;
        private List<DistributionModel> _allDistribution;
        private List<UserModel> _childUsers;
        public ShareUser(UserClass user) : base(user)
        {
            InitializeComponent();
            _userModelDataService = new UserModelDataService();
            _shareUserDataService = new ShareUserDataService();
        }

        #region Initialization
        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            await InitializeFormAsync();
            HideTechnicalColumns();
        }
        private async Task InitializeFormAsync()
        {
            var admUser = new UserClass { Roles = { "Администратор" } };

            customGridControlUser.DataSource =
                await _userModelDataService.GetUsersHierarchyAsync(admUser);

            var childUsers = await _userModelDataService
            .GetUsersHierarchyAsync(CurrentUser.User);

            _childUsers = childUsers
                .Select(u => new UserModel
                {
                    UserID = u.UserID,
                    UserName = u.UserName,
                    Fio = u.Fio,
                    FioID = u.FioID
                })
                .ToList();

            customGridControlChild.DataSource = _childUsers;

            _allDistribution = await _shareUserDataService.GetDistribution();

        }
        #endregion
        private void gridViewUser_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != IsSelected) return;
            if (!(bool)e.Value) return;

            SelectUserRow(e.RowHandle);
        }
        private void gridViewUser_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0) return;
            SelectUserRow(e.RowHandle);
        }

        private void SelectUserRow(int rowHandle)
        {
            var user = gridViewUser.GetRow(rowHandle) as UserModel;
            if (user == null) return;

            gridViewUser.BeginUpdate();
            try
            {
                for (int i = 0; i < gridViewUser.RowCount; i++)
                {
                    int rh = gridViewUser.GetVisibleRowHandle(i);
                    if (rh < 0) continue;

                    gridViewUser.SetRowCellValue(rh, IsSelected, rh == rowHandle);
                }
            }
            finally
            {
                gridViewUser.EndUpdate();
            }

            _selectedUser = user;
            LoadDistributionForSelectedUser();
        }

        private void LoadDistributionForSelectedUser()
        {
            if (_selectedUser == null) return;

            _currentUserDistribution = _allDistribution
                .Where(x => x.ParentID == _selectedUser.UserID)
                .ToList();

            foreach (var child in _childUsers)
            {
                if (child.UserID == _selectedUser.UserID)
                {
                    child.IsSelected = false;
                    continue;
                }
                child.IsSelected = _currentUserDistribution
                    .Any(d => d.ChildID == child.UserID);
            }

            gridViewChild.RefreshData();
        }

        private void gridViewChild_CellValueChanging(object sender,
        DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != IsSelected1) return;

            if (_selectedUser == null)
            {
                gridViewChild.SetRowCellValue(e.RowHandle, IsSelected1, false);
                return;
            }

            var row = gridViewChild.GetRow(e.RowHandle) as UserModel;
            if (row == null) return;

            if (row.UserID == _selectedUser.UserID)
            {
                gridViewChild.SetRowCellValue(e.RowHandle, IsSelected1, false);
                return;
            }

            row.IsSelected = Convert.ToBoolean(e.Value);
        }

        private async void customButtonOk_Click(object sender, EventArgs e)
        {
            if (_selectedUser == null) return;

            var selectedIds = _childUsers
                .Where(x => x.IsSelected)
                .Select(x => x.UserID)
                .ToList();

            var existedIds = _currentUserDistribution
                .Select(x => x.ChildID)
                .ToList();

            foreach (var id in selectedIds.Except(existedIds))
            {
                var model = new DistributionModel
                {
                    ParentID = _selectedUser.UserID,
                    ChildID = id
                };

                int newId = await _shareUserDataService.SaveAsync(model);
                model.DistributionID = newId;
                _allDistribution.Add(model);
            }

            foreach (var dist in _currentUserDistribution
                .Where(x => !selectedIds.Contains(x.ChildID)))
            {
                await _shareUserDataService.DeleteAsync(dist);
                _allDistribution.Remove(dist);
            }

            Close();
        }


        #region Technical
        private void HideTechnicalColumns()
        {
            gridViewUser.BeginUpdate();
            try
            {
                gridViewUser.Columns["UserID"].Visible = false;
                gridViewUser.Columns["FioID"].Visible = false;
                gridViewUser.Columns["IsSelected"].Visible = true;
            }
            finally
            {
                gridViewUser.EndUpdate();
            }
        }
        #endregion

    }
}
