using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution.Models;
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
            gridViewRole.FocusedRowChanged += gridViewRole_FocusedRowChanged;
            repositoryItemComboBoxColumnMode.EditValueChanged += RepositoryItemComboBoxColumnMode_EditValueChanged;


            var dbHelper = new DatabaseHelper();
            var dbService = new DbService(dbHelper);
            _formService = new FormDataService(dbService, dbHelper);
            _objectService = new ObjectDataService(dbService, dbHelper);
            _roleService = new RoleDataService(dbService, dbHelper);
            _tableService = new AllTableNameDataService(dbService, dbHelper);
            _columnService = new AllColumnNameDataService(dbService, dbHelper);

            customGridControlForm.DataSource = bindingSourceForm;
            customGridControlObject.DataSource = bindingSourceObject;
            customGridControlRole.DataSource = bindingSourceRole;
            customGridControlTable.DataSource = bindingSourceTable;
            customGridControlColumn.DataSource = bindingSourceColumn;
            //customGridControlButton.DataSource = bindingSourceButton;
        }

        private async void RoleColumn_Load(object sender, EventArgs e)
        {
            await LoadRolesAsync();
            await LoadFormsAsync();
            await LoadTablesAsync();
        }

        private async Task LoadRolesAsync()
        {
            _roles = await _roleService.GetListRolesAsync(_user.UserId);
            customGridControlRole.DataSource = _roles;
            customGridControlRole.RefreshDataSource();
        }
        private async Task LoadFormsAsync(RoleModel selectedRole = null)
        {
            _forms = await _formService.GetListFormsAsync();
            customGridControlForm.DataSource = _forms;
            customGridControlForm.RefreshDataSource();
        }
        private async Task LoadObjectsAsync(FormModel selectedForm)
        {
            _objects = await _objectService.GetListObjectGridAsync(selectedForm.ProjectFormsID);
            customGridControlObject.DataSource = _objects;
            customGridControlObject.RefreshDataSource();
        }
        private async Task LoadTablesAsync()
        {
            _tables = await _tableService.GetListTableAsync();
            bindingSourceTable.DataSource = _tables;
        }
        private async Task LoadColumnsAsync()
        {
            if (gridViewRole.GetFocusedRow() is not RoleModel role ||
                gridViewObject.GetFocusedRow() is not ObjectModel obj ||
                gridViewTable.GetFocusedRow() is not AllTableNameModel table)
                return;

            _columns = await _columnService.GetListColumnWithModeFromTable(role.RoleID, obj.ObjectID, table.id_atn);
            bindingSourceColumn.DataSource = _columns;
        }
        //private async Task LoadButtonsAsync()
        //{
        //    if (gridViewRole.GetFocusedRow() is not RoleModel role ||
        //        gridViewObject.GetFocusedRow() is not ObjectModel obj)
        //        return;

        //    var allControls = await _columnService.GetListColumnWithModeFromTable(role.RoleID, obj.ObjectID, 0);
        //    var buttonControls = allControls
        //        .Where(c => c.data_type == "button")
        //        .ToList();

        //    bindingSourceButton.DataSource = buttonControls;
        //}

        private async Task LoadColumnsFromSelectedTableAsync()
        {
            if (bindingSourceTable.Current is AllTableNameModel selectedTable)
            {
                await LoadColumnsAsync();
                //await LoadButtonsAsync(); 
            }
        }
        private async Task LoadObjectsFromSelectedFormAsync()
        {
            if (gridViewForm.GetFocusedRow() is FormModel selectedForm)
                await LoadObjectsAsync(selectedForm);
        }
        private async Task LoadFormsFromSelectedRoleAsync()
        {
            if (gridViewRole.GetFocusedRow() is RoleModel selectedRole)
                await LoadFormsAsync(selectedRole);
        }
        private async void gridViewTable_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            await LoadColumnsFromSelectedTableAsync();
        }
        private async void gridViewForm_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            await LoadObjectsFromSelectedFormAsync();
            await LoadColumnsFromSelectedTableAsync();
        }
        private async void gridViewRole_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            await LoadFormsFromSelectedRoleAsync();
            await LoadColumnsFromSelectedTableAsync();
        }

        private async void RepositoryItemComboBoxColumnMode_EditValueChanged(object sender, EventArgs e)
        {
            var editor = sender as DevExpress.XtraEditors.ComboBoxEdit;
            if (editor == null) return;

            if (gridViewColumn.FocusedRowHandle < 0) return;

            var column = gridViewColumn.GetRow(gridViewColumn.FocusedRowHandle) as AllColumnNameModel;
            if (column == null) return;

            string selectedText = editor.Text;
            int modeId = selectedText switch
            {
                "Просмотр" => 1,
                "Редактор" => 2,
                _ => 0
            };

            if (column.Readonly == 1 && modeId > 1)
            {
                MessageBox.Show($"Столбец \"{column.name}\" доступен только для чтения. Установлен режим 'Просмотр'.", "Ограничение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                modeId = 1;
                editor.SelectedIndex = 1;
            }

            if (gridViewRole.GetFocusedRow() is not RoleModel role ||
                gridViewObject.GetFocusedRow() is not ObjectModel obj)
                return;

            await _columnService.SaveRoleColumnAccessAsync(role.RoleID, obj.ObjectID, column.id_acn, modeId);
        }

        private async void customComboBoxRightForTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gridViewRole.GetFocusedRow() is not RoleModel role ||
                gridViewObject.GetFocusedRow() is not ObjectModel obj ||
                gridViewTable.GetFocusedRow() is not AllTableNameModel table)
                return;

            var modeText = customComboBoxRightForTable.Text;
            int modeId = modeText switch
            {
                "Просмотр" => 1,
                "Редактор" => 2,
                _ => 0 // "Нет доступа"
            };

            if (_columns == null || _columns.Count == 0)
                return;

            foreach (var column in _columns)
            {
                // если колонка только для чтения, не даём право выше "Просмотр"
                int finalModeId = column.Readonly == 1 && modeId > 1 ? 1 : modeId;

                await _columnService.SaveRoleColumnAccessAsync(role.RoleID, obj.ObjectID, column.id_acn, finalModeId);
                column.ModeID = finalModeId;
                column.ModeName = finalModeId switch
                {
                    1 => "Просмотр",
                    2 => "Редактор",
                    _ => "Нет доступа"
                };
            }

            customGridControlColumn.RefreshDataSource();
        }
        //private async void RepositoryItemCheckEditButton_EditValueChanged(object sender, EventArgs e)
        //{
        //    if (gridViewButton.FocusedRowHandle < 0) return;

        //    var row = gridViewButton.GetRow(gridViewButton.FocusedRowHandle) as AllColumnNameModel;
        //    if (row == null) return;

        //    if (gridViewRole.GetFocusedRow() is not RoleModel role ||
        //        gridViewObject.GetFocusedRow() is not ObjectModel obj)
        //        return;

        //    int modeId = Convert.ToInt32(gridViewButton.GetRowCellValue(gridViewButton.FocusedRowHandle, "ModeID"));

        //    await _columnService.SaveRoleColumnAccessAsync(role.RoleID, obj.ObjectID, row.id_acn, modeId);
        //}
    }

}
