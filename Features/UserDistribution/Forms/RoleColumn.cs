using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution.Models;
using SewingProduction.form;
using SewingProduction.form.UserDistribution;
using SewingProduction.form.UserDistribution.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.UserDistribution.Forms
{
    public partial class RoleColumn : CustomForm
    {
        private readonly FormDataService _formService;
        private readonly ObjectDataService _objectService;
        private readonly RoleDataService _roleService;
        private readonly AllTableNameDataService _tableService;
        private readonly AllColumnNameDataService _columnService;

        private List<FormModel> _forms;
        private List<ObjectModel> _objects;
        private List<RoleModel> _roles;
        private List<AllTableNameModel> _tables;
        private List<AllColumnNameModel> _columns;
        public RoleColumn(UserClass user) : base(user)
        {
            InitializeComponent();
            gridViewTable.FocusedRowChanged += gridViewTable_FocusedRowChanged;
            gridViewForm.FocusedRowChanged += gridViewForm_FocusedRowChanged;
            var dbHelper = new DatabaseHelper("ace");
            var dbService = new DbService(dbHelper);
            _formService = new FormDataService(dbService, dbHelper);
            _objectService = new ObjectDataService(dbService, dbHelper);
            _roleService = new RoleDataService(dbService, dbHelper);
            _tableService = new AllTableNameDataService(dbService, dbHelper);
            _columnService = new AllColumnNameDataService(dbService, dbHelper);

            customGridControlForm.DataSource = new BindingSource();
            customGridControlObject.DataSource = new BindingSource();
            customGridControlRole.DataSource = new BindingSource();
            customGridControlTable.DataSource = bindingSourceTable;
            customGridControlColumn.DataSource = bindingSourceColumn;
        }

        private async void RoleColumn_Load(object sender, EventArgs e)
        {
            await LoadTablesAsync();
            await LoadFormsAsync();
            await LoadRolesAsync();
        }

        private async Task LoadTablesAsync()
        {
            _tables = await _tableService.GetListTable();
            bindingSourceTable.DataSource = _tables;
        }
        private async Task LoadFormsAsync()
        {
            _forms = await _formService.GetListAsync();
            customGridControlForm.DataSource = _forms;
            customGridControlForm.RefreshDataSource();
        }

        private async Task LoadObjectsAsync(FormModel selectedForm)
        {
            _objects = await _objectService.GetListAsync(selectedForm.ProjectFormsID);
            customGridControlObject.DataSource = _objects;
            customGridControlObject.RefreshDataSource();
        }

        private async Task LoadRolesAsync()
        {
            _roles = await _roleService.GetRoles(_user.UserId);
            customGridControlRole.DataSource = _roles;
            customGridControlRole.RefreshDataSource();
        }


        private async Task LoadColumnsFromSelectedTableAsync()
        {
            if (bindingSourceTable.Current is AllTableNameModel selectedTable)
            {
                _columns = await _columnService.GetListColumnFromTable(selectedTable.id_atn);
                bindingSourceColumn.DataSource = _columns;
            }
        }
        private async Task LoadObjectsFromSelectedFormAsync()
        {
            if (gridViewForm.GetFocusedRow() is FormModel selectedForm)
                await LoadObjectsAsync(selectedForm);
        }
        private async void gridViewTable_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            await LoadColumnsFromSelectedTableAsync();
        }
        private async void gridViewForm_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            await LoadObjectsFromSelectedFormAsync();
        }

    }

}
