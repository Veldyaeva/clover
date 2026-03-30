using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.UserDistribution.Class;
using SewingProduction.Features.UserDistribution.DataService;
using SewingProduction.Features.UserDistribution.Helpers;

namespace SewingProduction.Features.UserDistribution.Forms
{
    public partial class RoleFormObject : CustomForm
    {
        private readonly AllRoleDataService _allRoleDataService;
        private readonly UserClass _user;

        private int _selectedRoleId = 0;
        private int _selectedFormId = 0;

        private Dictionary<int, int> _userPermissions = new Dictionary<int, int>();
        private HashSet<int> _editableRoleCreatorIds = new HashSet<int>();

        private bool _isLoadingRoles = false;
        private bool _isLoadingForms = false;
        private bool _isLoadingObjects = false;

        public RoleFormObject(UserClass user) : base(user)
        {
            _user = user;
            _allRoleDataService = new AllRoleDataService();

            InitializeComponent();
        }

        public RoleFormObject(UserClass user, int selectedRoleId) : base(user)
        {
            _user = user;
            _allRoleDataService = new AllRoleDataService();
            _selectedRoleId = selectedRoleId;

            InitializeComponent();
        }

        #region Initialization

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            await InitializeFormAsync();
        }

        private async Task InitializeFormAsync()
        {
            _isLoadingRoles = true;
            try
            {
                DataTable dtRoles = await _allRoleDataService.GetRoles(_user.UserId);

                if (!dtRoles.Columns.Contains("IsSelected"))
                    dtRoles.Columns.Add("IsSelected", typeof(bool));

                foreach (DataRow row in dtRoles.Rows)
                    row["IsSelected"] = false;

                customGridControlRole.DataSource = dtRoles;

                _userPermissions = await _allRoleDataService.GetUserObjectPermissions(_user.UserId);

                DataTable descendants = await _allRoleDataService.LoadEditableCreatorIds(_user.UserId);
                _editableRoleCreatorIds = descendants.AsEnumerable()
                    .Select(r => Convert.ToInt32(r["UserID"]))
                    .ToHashSet();

                customGridControlForm.DataSource = null;
                customGridControlObject.DataSource = null;
            }
            finally
            {
                _isLoadingRoles = false;
            }

            //ConfigureViews();

            if (_selectedRoleId > 0)
                SelectRoleById(_selectedRoleId);
        }

        private void SelectRoleById(int roleId)
        {
            for (int i = 0; i < gridViewRole.RowCount; i++)
            {
                DataRow row = gridViewRole.GetDataRow(i);
                if (row == null)
                    continue;

                if (Convert.ToInt32(row["RoleID"]) == roleId)
                {
                    SelectRoleRow(i);
                    gridViewRole.FocusedRowHandle = i;
                    return;
                }
            }
        }

        #endregion

        #region Role Selection

        private void gridViewRole_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != IsSelected)
                return;

            if (!(bool)e.Value)
                return;

            SelectRoleRow(e.RowHandle);
        }
        private async void SelectRoleRow(int rowHandle)
        {
            DataRow roleRow = gridViewRole.GetDataRow(rowHandle);
            if (roleRow == null)
                return;

            int roleId = Convert.ToInt32(roleRow["RoleID"]);

            gridViewRole.BeginUpdate();
            try
            {
                for (int i = 0; i < gridViewRole.RowCount; i++)
                {
                    if (gridViewRole.GetDataRow(i) == null)
                        continue;

                    gridViewRole.SetRowCellValue(i, IsSelected, i == rowHandle);
                }
            }
            finally
            {
                gridViewRole.EndUpdate();
            }

            _selectedRoleId = roleId;
            _selectedFormId = 0;

            gridViewRole.RefreshData();

            await LoadFormsForSelectedRoleAsync();
            customGridControlObject.DataSource = null;
        }


        private async Task LoadFormsForSelectedRoleAsync()
        {
            if (_selectedRoleId <= 0)
            {
                customGridControlForm.DataSource = null;
                customGridControlObject.DataSource = null;
                return;
            }

            _isLoadingForms = true;
            try
            {
                DataTable dtForms = await _allRoleDataService.GetFormsForRoles(_selectedRoleId, _user.UserId);

                customGridControlForm.DataSource = dtForms;
                customGridControlObject.DataSource = null;
                _selectedFormId = 0;
            }
            finally
            {
                _isLoadingForms = false;
            }

            gridViewForm.RefreshData();
        }

        private void gridViewRole_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            SelectRoleRow(e.RowHandle);
        }

        #endregion

        #region Form Selection

        private void gridViewForm_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            SelectFormRow(e.RowHandle);
        }

        private void gridViewForm_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            SelectFormRow(e.FocusedRowHandle);
        }

        private async void SelectFormRow(int rowHandle)
        {
            DataRow formRow = gridViewForm.GetDataRow(rowHandle);
            if (formRow == null)
                return;

            int formId = GetFormIdFromRow(formRow);
            if (formId <= 0)
                return;

            _selectedFormId = formId;
            await LoadObjectsForSelectedFormAsync();
        }

        private async Task LoadObjectsForSelectedFormAsync()
        {
            if (_selectedRoleId <= 0 || _selectedFormId <= 0)
            {
                customGridControlObject.DataSource = null;
                return;
            }

            _isLoadingObjects = true;
            try
            {
                DataTable dtObjects = await _allRoleDataService.GetObjectsForFormRoles(_selectedRoleId, _selectedFormId, _user.UserId);
                
                customGridControlObject.DataSource = dtObjects;
            }
            finally
            {
                _isLoadingObjects = false;
            }

            gridViewObject.RefreshData();
        }

        #endregion

        #region Save Access

        private async void gridViewForm_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (_isLoadingForms)
                return;

            if (e.Column != HasAccess)
                return;

            if (_selectedRoleId <= 0)
                return;

            if (!await CheckEditRoleAsync())
            {
                await ReloadCurrentFormsAsync();
                return;
            }

            DataRow row = gridViewForm.GetDataRow(e.RowHandle);
            if (row == null)
                return;

            int formId = GetFormIdFromRow(row);
            if (formId <= 0)
                return;

            string modeText = Convert.ToString(row["HasAccess"]);
            int requestedMode = GetRequestedMode(modeText);

            try
            {
                DataTable dtObjects = await _allRoleDataService.GetObjectsForFormRoles(_selectedRoleId, formId, _user.UserId);

                foreach (DataRow objectRow in dtObjects.Rows)
                {
                    int objectId = Convert.ToInt32(objectRow["ObjectID"]);
                    int currentUserMode = _userPermissions.TryGetValue(objectId, out int mode) ? mode : 0;
                    int finalMode = Math.Min(requestedMode, currentUserMode);

                    await _allRoleDataService.UpdateRoleObjectMode(_selectedRoleId, objectId, finalMode);
                }

                //await LoadFormsForSelectedRoleAsync();

                if (_selectedFormId == formId)
                    await LoadObjectsForSelectedFormAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при изменении доступа к форме: " + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                await ReloadCurrentFormsAsync();
            }
        }

        private async void gridViewObject_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (_isLoadingObjects)
                return;

            if (e.Column != HasAccessObject)
                return;

            if (_selectedRoleId <= 0 || _selectedFormId <= 0)
                return;

            if (!await CheckEditRoleAsync())
            {
                await ReloadCurrentObjectsAsync();
                return;
            }

            DataRow row = gridViewObject.GetDataRow(e.RowHandle);
            if (row == null)
                return;

            int objectId = Convert.ToInt32(row["ObjectID"]);
            string modeText = Convert.ToString(row["HasAccessObject"]);
            int requestedMode = GetRequestedMode(modeText);

            try
            {
                int currentUserMode = _userPermissions.TryGetValue(objectId, out int mode) ? mode : 0;
                int finalMode = Math.Min(requestedMode, currentUserMode);

                await _allRoleDataService.UpdateRoleObjectMode(_selectedRoleId, objectId, finalMode);

                //await LoadObjectsForSelectedFormAsync();
                //await LoadFormsForSelectedRoleAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при изменении доступа к объекту: " + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                await ReloadCurrentObjectsAsync();
            }
        }

        #endregion

        #region Permissions

        private async Task<bool> CheckEditRoleAsync()
        {
            if (_selectedRoleId <= 0)
                return false;

            if (_user.Roles != null && _user.Roles.Contains("Администратор"))
                return true;

            int creatorId = await _allRoleDataService.GetCreatorIdByRole(_selectedRoleId);

            if (!_editableRoleCreatorIds.Contains(creatorId))
            {
                MessageBox.Show("Вы не можете редактировать эту роль. Она не создана вами или вашими потомками.",
                    "Ограничение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void gridViewObject_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column != HasAccessObject)
                return;

            DataRow row = gridViewObject.GetDataRow(e.RowHandle);
            if (row == null)
                return;

            int objectId = Convert.ToInt32(row["ObjectID"]);
            int maxMode = _userPermissions.TryGetValue(objectId, out int mode) ? mode : 0;

            RepositoryItemComboBox repository = new RepositoryItemComboBox();
            repository.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            repository.Items.Clear();
            repository.Items.Add("Нет доступа");

            if (maxMode >= 1)
                repository.Items.Add("Просмотр");

            if (maxMode >= 2)
                repository.Items.Add("Редактор");

            e.RepositoryItem = repository;
        }

        #endregion

        #region Keyboard

        private void gridViewRole_KeyDown(object sender, KeyEventArgs e)
        {
            var view = (GridView)sender;

            if (e.KeyCode == Keys.Right)
            {
                customGridControlForm.Focus();
                gridViewForm.Focus();
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
                    SelectRoleRow(newHandle);

                e.Handled = true;
            }
        }

        private void gridViewForm_KeyDown(object sender, KeyEventArgs e)
        {
            var view = (GridView)sender;

            if (e.KeyCode == Keys.Left)
            {
                customGridControlRole.Focus();
                gridViewRole.Focus();
                e.Handled = true;
                return;
            }

            if (e.KeyCode == Keys.Right)
            {
                customGridControlObject.Focus();
                gridViewObject.Focus();
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
                    SelectFormRow(newHandle);

                e.Handled = true;
                return;
            }
        }

        private void gridViewObject_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                customGridControlForm.Focus();
                gridViewForm.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #region Helpers

        private async Task ReloadCurrentFormsAsync()
        {
            if (_selectedRoleId <= 0)
                return;

            await LoadFormsForSelectedRoleAsync();
        }

        private async Task ReloadCurrentObjectsAsync()
        {
            if (_selectedRoleId <= 0 || _selectedFormId <= 0)
                return;

            await LoadObjectsForSelectedFormAsync();
        }

        private int GetRequestedMode(string mode)
        {
            switch (mode)
            {
                case "Редактор":
                    return 2;
                case "Просмотр":
                    return 1;
                case "Нет доступа":
                default:
                    return 0;
            }
        }

        private int GetFormIdFromRow(DataRow row)
        {
            if (row.Table.Columns.Contains("ProjectFormsID"))
                return Convert.ToInt32(row["ProjectFormsID"]);

            if (row.Table.Columns.Contains("FormID"))
                return Convert.ToInt32(row["FormID"]);

            return 0;
        }

        #endregion
    }
}
