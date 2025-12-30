using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.XtraGrid;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Core.Class
{
    public class CustomGridControlColumn : GridControl, SewingProduction.IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color? AlternateRowColor { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color? FocusedRowColor { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool FocusedRowBold { get; set; } = true;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        private UserClass _user;
        private List<int> _tableIds;
        private int _formId;
        public CustomGridControlColumn()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
            ViewRegistered += OnViewRegistered;
        }
        public void ApplyTheme()
        {
            BackColor = ThemeManager.ActiveTheme.GridBackground;
            ForeColor = ThemeManager.ActiveTheme.TextBoxText;
            Font = ThemeManager.SharedSettings.DefaultFont;
            AlternateRowColor = ThemeManager.ActiveTheme.BandHighlightColor;
            FocusedRowColor = ThemeManager.ActiveTheme.ButtonBackground;


            foreach (var view in ViewCollection)
            {
                if (view is DevExpress.XtraGrid.Views.Grid.GridView gridView)
                {
                    ApplyRowColors(gridView);
                    ApplyFocusedRowStyle(gridView);
                    gridView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                    gridView.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                }
            }
        }
        private void OnViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            if (e.View is DevExpress.XtraGrid.Views.Grid.GridView gridView)
            {
                ApplyRowColors(gridView);
                ApplyFocusedRowStyle(gridView);
            }
        }
        private void ApplyRowColors(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            if (AlternateRowColor.HasValue)
            {
                gridView.Appearance.EvenRow.BackColor = AlternateRowColor.Value;
                gridView.OptionsView.EnableAppearanceEvenRow = true;
            }
        }

        private void ApplyFocusedRowStyle(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            if (FocusedRowColor.HasValue)
            {
                gridView.Appearance.FocusedRow.BackColor = FocusedRowColor.Value;
                gridView.Appearance.FocusedRow.Options.UseBackColor = true;
            }

            if (FocusedRowBold)
            {
                gridView.Appearance.FocusedRow.Font = new Font(gridView.Appearance.FocusedRow.Font ?? Font, FontStyle.Bold);
                gridView.Appearance.FocusedRow.Options.UseFont = true;
            }
        }
        private void OnThemeChanged() => ApplyTheme();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }
        public void ApplyPermission(UserClass user)
        {
            //PermissionHelper.ApplyTo(this, ObjectName, user);

            if (string.IsNullOrEmpty(ObjectName)) return;

            bool hasWrite = user.HasPermission(ObjectName, "Редактор");
            bool hasRead = user.HasPermission(ObjectName, "Просмотр");

            this.Visible = hasRead || hasWrite;
            //this.Enabled = hasWrite;
            foreach (var view in ViewCollection)
            {
                if (view is DevExpress.XtraGrid.Views.Grid.GridView gridView)
                {
                    gridView.OptionsBehavior.ReadOnly = !hasWrite;
                    gridView.OptionsBehavior.Editable = hasWrite;
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisiblePermission
        {
            get => _visiblePermission;
            set
            {
                _visiblePermission = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisibleLogic
        {
            get => _visibleLogic;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        public new bool Visible
        {
            get => base.Visible;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }
        public async void InitializeAccess(UserClass user, string formName, List<string> tableNames = null)
        {
            var dbHelper = new DatabaseHelper();
            var dbService = new DbService(dbHelper);
            var roleService = new RoleDataService();
            var columnService = new AllColumnNameDataService(dbService, dbHelper);
            var tableService = new AllTableNameDataService();
            var formService = new FormDataService();

            _user = user;

            // Получаем ID формы
            _formId = (await formService.GetFormIdByNameAsync(formName)) ?? 0;

            Debug.WriteLine($" _user.name: {_user.UserName}");
            Debug.WriteLine($" ObjectName: {ObjectName}");
            Debug.WriteLine($" formName: {formName}");
            Debug.WriteLine($" _formId: {_formId}");
            if (_user == null || string.IsNullOrEmpty(ObjectName) || _formId <= 0)
            {
                Debug.WriteLine("[InitializeAccess] Ошибка: не заданы обязательные параметры.");
                return;
            }

            // Получаем ID ролей пользователя
            List<int> roleIds = await roleService.GetRoleIdsByNamesAsync(_user.Roles);

            // Готовим словарь с правами на колонки
            Dictionary<string, int> columnAccess = new();

            await ApplyPermissionPerColumn(roleIds, columnService, tableService, columnAccess, tableNames);
            await ApplyPermissionPerMode(columnAccess);
        }
        private async Task ApplyPermissionPerColumn(
            List<int> roleIds,
            AllColumnNameDataService columnService,
            AllTableNameDataService tableService,
            Dictionary<string, int> columnAccess,
            List<string> tableNames)
        {
            if (tableNames != null && tableNames.Any())
            {
                // Есть указание конкретных таблиц — применяем по каждой
                foreach (var tableName in tableNames)
                {
                    var tableId = await tableService.GetTableIdByNameAsync(tableName);
                    if (tableId.HasValue && tableId.Value > 0)
                    {
                        Debug.WriteLine($"[InitializeAccess] Таблица '{tableName}' → ID: {tableId.Value}");
                        var columns = await columnService.GetColumnsWithAccessAsync(roleIds, ObjectName, _formId, tableId.Value);

                        foreach (var col in columns)
                        {
                            if (columnAccess.TryGetValue(col.name, out int current))
                                columnAccess[col.name] = Math.Max(current, col.ModeID);
                            else
                                columnAccess[col.name] = col.ModeID;
                        }
                    }
                    else
                    {
                        Debug.WriteLine($"[InitializeAccess] Таблица '{tableName}' не найдена.");
                    }
                }
            }
            else
            {
                // Нет конкретных таблиц — берём все доступные по форме
                var columns = await columnService.GetColumnsWithAccessAsync(roleIds, ObjectName, _formId);

                foreach (var col in columns)
                {
                    columnAccess[col.name] = col.ModeID;
                }
            }
        }
        private async Task ApplyPermissionPerMode(Dictionary<string, int> columnAccess)
        {
            foreach (var view in ViewCollection)
            {
                if (view is DevExpress.XtraGrid.Views.Grid.GridView gridView)
                {
                    foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridView.Columns)
                    {
                        if (columnAccess.TryGetValue(column.FieldName, out int mode))
                        {
                            column.Visible = mode > 0;
                            column.OptionsColumn.ReadOnly = mode < 2;
                            column.OptionsColumn.AllowEdit = mode == 2;
                            Debug.WriteLine($"[ApplyPermissionPerColumn] Колонка {column.FieldName} → ModeID={mode}");
                        }
                        else
                        {
                            column.Visible = false;
                            Debug.WriteLine($"[ApplyPermissionPerColumn] Колонка {column.FieldName} скрыта (нет прав)");
                        }
                    }
                }
            }
            Debug.WriteLine("[ApplyPermissionPerColumn] Завершено");
        }
    }
}
