using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.Features.UserDistribution.Class;
using SewingProduction.Features.UserDistribution.DataService;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution.Models;
using static DevExpress.Utils.Drawing.Helpers.NativeMethods;

namespace SewingProduction.Features.UserDistribution.Forms
{
    public partial class RolePodr : CustomForm
    {
        private readonly RolePodrDataService _rolePodrDataService;
        private readonly RoleDataService _roleService;
        private readonly AllTableNameDataService _tableService;
        private List<RolePodrModel> _allRolePodr;
        private AllTableNameModel _selectedPodr;
        private RoleModel _selectedRole;
        public RolePodr(UserClass user) : base(user)
        {
            InitializeComponent();
            _rolePodrDataService = new RolePodrDataService();
            _roleService = new RoleDataService();
            _tableService = new AllTableNameDataService();
        }
        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            await InitializeFormAsync();
        }
        private async Task InitializeFormAsync()
        {
            _allRolePodr = await _rolePodrDataService.LoadAllRolePodrAsync();

            customGridControlRole.DataSource = await _roleService.GetListRolesAsync(_user);
            customGridControlRole.RefreshDataSource();

            customGridControlTable.DataSource = await _tableService.GetListTableAsync();
            customGridControlTable.RefreshDataSource();

            customCheckBoxPodr.Checked = true;
        }
        private async void gridViewRole_CellValueChanged(
        object sender,
        DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != nameof(RoleModel.IsSelected))
                return;

            if (_selectedPodr == null)
            {
                gridViewRole.SetRowCellValue(e.RowHandle, e.Column, false);
                return;
            }

            var role = gridViewRole.GetRow(e.RowHandle) as RoleModel;
            if (role == null)
                return;

            bool isChecked = Convert.ToBoolean(e.Value);

            if (isChecked)
            {
                int newId = await _rolePodrDataService.SaveAsync(new RolePodrModel
                {
                    RoleID = role.RoleID,
                    PodrTableID = _selectedPodr.id_atn
                });

                _allRolePodr.Add(new RolePodrModel
                {
                    RoleID = role.RoleID,
                    PodrTableID = _selectedPodr.id_atn
                });
            }
            else
            {
                await _rolePodrDataService.DeleteAsync(
                    role.RoleID,
                    _selectedPodr.id_atn);

                _allRolePodr.RemoveAll(x =>
                    x.RoleID == role.RoleID &&
                    x.PodrTableID == _selectedPodr.id_atn);
            }
        }

        private void gridViewTable_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            var view = gridViewTable;

            var table = view.GetRow(e.RowHandle) as AllTableNameModel;
            if (table == null)
                return;

            // визуально выделяем строку (если есть IsSelected)
            view.BeginUpdate();
            try
            {
                for (int i = 0; i < view.RowCount; i++)
                {
                    int rh = view.GetVisibleRowHandle(i);
                    if (rh < 0) continue;

                    view.SetRowCellValue(rh, IsSelected, rh == e.RowHandle);
                }
            }
            finally
            {
                view.EndUpdate();
            }

            view.FocusedRowHandle = e.RowHandle;
            _selectedPodr = table;

            ApplyRoleChecksForTable(_selectedPodr.id_atn);
        }
        private void ApplyRoleChecksForTable(int podrTableId)
        {
            var roleIds = _allRolePodr
                .Where(x => x.PodrTableID == podrTableId)
                .Select(x => x.RoleID)
                .ToHashSet();

            gridViewRole.BeginUpdate();
            try
            {
                for (int i = 0; i < gridViewRole.RowCount; i++)
                {
                    var role = gridViewRole.GetRow(i) as RoleModel;
                    if (role == null) continue;

                    role.IsSelected = roleIds.Contains(role.RoleID);
                }
            }
            finally
            {
                gridViewRole.EndUpdate();
            }
        }

        private void customCheckBoxPodr_CheckedChanged(object sender, EventArgs e)
        {
            var allowedIds = _allRolePodr
                .Select(x => x.PodrTableID)
                .Distinct()
                .ToList();

            if (customCheckBoxPodr.Checked)
            {
                if (allowedIds.Count == 0)
                {
                    gridViewTable.ActiveFilterString = "1 = 0"; 
                }
                else
                {
                    string filter = string.Join(" OR ",
                        allowedIds.Select(id => $"[{nameof(AllTableNameModel.id_atn)}] = {id}"));

                    gridViewTable.ActiveFilterString = filter;
                }
            }
            else
            {
                gridViewTable.ActiveFilterString = string.Empty;
            }
        }

    }
}
