using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.Models;
using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    internal partial class ThreadNormsForm : CustomForm
    {
        private readonly DatabaseHelperSQL _dbHelper;
        private readonly DbService _dbService;
        private readonly ILogger _logger = new FileLogger();
        private readonly TWGridHelper _gridHelper = new TWGridHelper();
        private readonly BindingList<ThreadNormRow> _rows = new BindingList<ThreadNormRow>();

        private List<GrupMenModel> _managers = new List<GrupMenModel>();
        private List<ThreadCategoryOption> _categories = new List<ThreadCategoryOption>();
        private List<ThreadAssortModel> _assorts = new List<ThreadAssortModel>();
        private List<ThreadMaterialOption> _materials = new List<ThreadMaterialOption>();
        private bool _isLoading;
        private bool _allowCloseWithoutPrompt;

        public ThreadNormsForm(UserClass user) : base(user)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelperSQL();
            _dbService = new DbService(_dbHelper);
            bindingSource.DataSource = _rows;
        }

        private async void ThreadNormsForm_Load(object sender, EventArgs e)
        {
            _gridHelper.LoadGridViewSettings(gridView, "ThreadNormsGrid.xml");
            await LoadDataFromDatabaseWithUiAsync();
        }

        private void ThreadNormsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _gridHelper.SaveGridViewSettings(gridView, "ThreadNormsGrid.xml");
            }
            catch (Exception ex)
            {
                _ = _logger.LogErrorAsync(ex, "Ошибка при сохранении настроек грида ThreadNormsForm");
            }

            if (DesignMode || _allowCloseWithoutPrompt || !HasPendingChanges())
            {
                return;
            }

            e.Cancel = true;
            _ = HandleClosingAsync();
        }

        private async Task LoadDataFromDatabaseWithUiAsync()
        {
            gridView.ShowLoadingPanel();
            try
            {
                await LoadReferenceDataFromDatabaseAsync();
                await LoadRowsFromDatabaseAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка загрузки данных справочника ниток из базы: {ex.Message}",
                    "Ошибка UI",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                gridView.HideLoadingPanel();
            }
        }

        private async Task LoadReferenceDataFromDatabaseAsync()
        {
            _isLoading = true;
            try
            {
                var managersTask = LoadManagersAsync();
                var categoriesTask = LoadCategoriesAsync();
                var assortsTask = LoadAssortsAsync();
                var materialsTask = LoadThreadMaterialsAsync();

                await Task.WhenAll(managersTask, categoriesTask, assortsTask, materialsTask);

                _managers = managersTask.Result ?? new List<GrupMenModel>();
                _categories = categoriesTask.Result ?? new List<ThreadCategoryOption>();
                _assorts = assortsTask.Result ?? new List<ThreadAssortModel>();
                _materials = materialsTask.Result ?? new List<ThreadMaterialOption>();

                managerLookup.DataSource = _managers;
                categoryLookup.DataSource = _categories;
                assortLookup.DataSource = _assorts;
                threadLookup.DataSource = _materials;
            }
            finally
            {
                _isLoading = false;
            }
        }

        private async Task LoadRowsFromDatabaseAsync()
        {
            _isLoading = true;
            try
            {
                gridView.CloseEditor();
                gridView.UpdateCurrentRow();

                var loaded = await LoadThreadNormRowsAsync(filterGroup.SelectedIndex == 0);
                _rows.RaiseListChangedEvents = false;
                _rows.Clear();

                foreach (var row in loaded ?? Enumerable.Empty<ThreadNormRow>())
                {
                    row.IsNew = false;
                    row.IsModified = false;
                    _rows.Add(row);
                }
            }
            finally
            {
                _rows.RaiseListChangedEvents = true;
                bindingSource.ResetBindings(false);
                gridView.BestFitColumns();
                _isLoading = false;
            }
        }

        private Task<List<GrupMenModel>> LoadManagersAsync()
        {
            const string query = @"
SELECT DISTINCT
    RTRIM(men) AS Men,
    RTRIM(name) AS Name
FROM dbo.view_grup_men
WHERE ISNULL(RTRIM(men), '') <> ''
ORDER BY Men";

            return _dbService.GetListAsync<GrupMenModel>(query, new { });
        }

        private Task<List<ThreadCategoryOption>> LoadCategoriesAsync()
        {
            const string query = @"
SELECT
    cat.TCAT_ID,
    cat.TCAT_CategoryName,
    grp.TG_GroupName,
    cls.TC_ClassName
FROM global.planeta.dbo.TOVAR_CATEGORY cat
LEFT JOIN global.planeta.dbo.TOVAR_GROUP grp
    ON grp.TG_ID = cat.TCAT_TG_ID
LEFT JOIN global.planeta.dbo.TOVAR_CLASS cls
    ON cls.TC_ID = grp.TG_TC_ID
ORDER BY cls.TC_ClassName, grp.TG_GroupName, cat.TCAT_CategoryName";

            return _dbService.GetListAsync<ThreadCategoryOption>(query, new { });
        }

        private async Task<List<ThreadAssortModel>> LoadAssortsAsync()
        {
            const string globalCodeQuery = @"
SELECT
    TAT_GlobalCode AS TAT_ID,
    TAT_Name
FROM global.planeta.dbo.TOVAR_ASSTYPE
WHERE TAT_GlobalCode <= 4
ORDER BY TAT_GlobalCode";

            const string idQuery = @"
SELECT
    TAT_ID,
    TAT_Name
FROM global.planeta.dbo.TOVAR_ASSTYPE
WHERE TAT_ID <= 4
ORDER BY TAT_ID";

            try
            {
                return await _dbService.GetListAsync<ThreadAssortModel>(globalCodeQuery, new { });
            }
            catch (Exception ex) when (ex.Message.Contains("GlobalCode", StringComparison.OrdinalIgnoreCase))
            {
                await _logger.LogWarningAsync(
                    "TOVAR_ASSTYPE не содержит TAT_GlobalCode, использую TAT_ID.",
                    "ThreadNormsForm.LoadAssortsAsync");
                return await _dbService.GetListAsync<ThreadAssortModel>(idQuery, new { });
            }
        }

        private Task<List<ThreadMaterialOption>> LoadThreadMaterialsAsync()
        {
            const string query = @"
SELECT
    RTRIM(dr.kod_dr) AS kod_dr,
    ISNULL((
        SELECT TOP (1) RTRIM(drm.kod)
        FROM dbo.dop_ras_mat drm
        WHERE dr.kod_dr = LEFT(drm.kod, 4)
          AND ISNULL(drm.kod_art, '') <> ''
        ORDER BY drm.kod
    ), '') AS kod3,
    ISNULL((
        SELECT TOP (1) RTRIM(drm.kod_art)
        FROM dbo.dop_ras_mat drm
        WHERE dr.kod_dr = LEFT(drm.kod, 4)
          AND ISNULL(drm.kod_art, '') <> ''
        ORDER BY drm.kod_art
    ), '') AS kod_art,
    RTRIM(dr.kod_dr) + ' | ' + RTRIM(dr.gr) + ' | ' + RTRIM(dr.articul) AS displayText
FROM dbo.dop_ras dr
WHERE dr.kod_gr = '25'
ORDER BY dr.gr, dr.articul";

            return _dbService.GetListAsync<ThreadMaterialOption>(query, new { });
        }

        private async Task<List<ThreadNormRow>> LoadThreadNormRowsAsync(bool zeroNormOnly)
        {
            string where = zeroNormOnly ? "WHERE ISNULL(n.norm, 0) = 0" : string.Empty;
            string queryByGlobalCode = $@"
SELECT
    n.id,
    n.men,
    menView.name AS men_name,
    n.tg_id_n,
    n.ta_id,
    n.norm,
    n.kod_dr,
    n.kod3,
    n.kod_art,
    n.date_change,
    cat.TCAT_CategoryName,
    grp.TG_ID,
    grp.TG_GroupName,
    cls.TC_ID,
    cls.TC_ClassName,
    assort.TAT_Name,
    LTRIM(RTRIM(ISNULL(n.kod3, ''))) +
        CASE
            WHEN NULLIF(LTRIM(RTRIM(ISNULL(n.kod_art, ''))), '') IS NULL THEN ''
            ELSE ' / ' + LTRIM(RTRIM(n.kod_art))
        END AS ThreadDisplay
FROM cfn.confection_norm_nitki n
LEFT JOIN dbo.view_grup_men menView
    ON menView.men = n.men
LEFT JOIN global.planeta.dbo.TOVAR_CATEGORY cat
    ON cat.TCAT_ID = n.tg_id_n
LEFT JOIN global.planeta.dbo.TOVAR_GROUP grp
    ON grp.TG_ID = cat.TCAT_TG_ID
LEFT JOIN global.planeta.dbo.TOVAR_CLASS cls
    ON cls.TC_ID = grp.TG_TC_ID
LEFT JOIN global.planeta.dbo.TOVAR_ASSTYPE assort
    ON assort.TAT_GlobalCode = n.ta_id
{where}
ORDER BY n.men, cls.TC_ClassName, grp.TG_GroupName, cat.TCAT_CategoryName, assort.TAT_Name, n.kod_dr";

            string queryById = $@"
SELECT
    n.id,
    n.men,
    menView.name AS men_name,
    n.tg_id_n,
    n.ta_id,
    n.norm,
    n.kod_dr,
    n.kod3,
    n.kod_art,
    n.date_change,
    cat.TCAT_CategoryName,
    grp.TG_ID,
    grp.TG_GroupName,
    cls.TC_ID,
    cls.TC_ClassName,
    assort.TAT_Name,
    LTRIM(RTRIM(ISNULL(n.kod3, ''))) +
        CASE
            WHEN NULLIF(LTRIM(RTRIM(ISNULL(n.kod_art, ''))), '') IS NULL THEN ''
            ELSE ' / ' + LTRIM(RTRIM(n.kod_art))
        END AS ThreadDisplay
FROM cfn.confection_norm_nitki n
LEFT JOIN dbo.view_grup_men menView
    ON menView.men = n.men
LEFT JOIN global.planeta.dbo.TOVAR_CATEGORY cat
    ON cat.TCAT_ID = n.tg_id_n
LEFT JOIN global.planeta.dbo.TOVAR_GROUP grp
    ON grp.TG_ID = cat.TCAT_TG_ID
LEFT JOIN global.planeta.dbo.TOVAR_CLASS cls
    ON cls.TC_ID = grp.TG_TC_ID
LEFT JOIN global.planeta.dbo.TOVAR_ASSTYPE assort
    ON assort.TAT_ID = n.ta_id
{where}
ORDER BY n.men, cls.TC_ClassName, grp.TG_GroupName, cat.TCAT_CategoryName, assort.TAT_Name, n.kod_dr";

            try
            {
                return await _dbService.GetListAsync<ThreadNormRow>(queryByGlobalCode, new { });
            }
            catch (Exception ex) when (ex.Message.Contains("GlobalCode", StringComparison.OrdinalIgnoreCase))
            {
                await _logger.LogWarningAsync(
                    "TOVAR_ASSTYPE не содержит TAT_GlobalCode, использую TAT_ID для загрузки строк.",
                    "ThreadNormsForm.LoadThreadNormRowsAsync");
                return await _dbService.GetListAsync<ThreadNormRow>(queryById, new { });
            }
        }

        private void AddRow()
        {
            var row = new ThreadNormRow
            {
                IsNew = true,
                IsModified = true,
                norm = 0m
            };

            _rows.Add(row);
            bindingSource.ResetBindings(false);
            FocusRow(row);
        }

        private void AddCopyRow()
        {
            if (bindingSource.Current is not ThreadNormRow current)
            {
                AddRow();
                return;
            }

            var copy = current.CloneForCopy();
            _rows.Add(copy);
            bindingSource.ResetBindings(false);
            FocusRow(copy);
        }

        private async Task DeleteCurrentRowAsync()
        {
            if (bindingSource.Current is not ThreadNormRow current)
            {
                return;
            }

            if (MessageBox.Show("Удалить выбранную строку?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            if (current.id > 0)
            {
                await _dbService.DeleteEntityAsync("cfn.confection_norm_nitki", "id", current);
            }

            _rows.Remove(current);
            bindingSource.ResetBindings(false);
        }

        private async Task<bool> SaveInternalAsync(bool showSuccessMessage)
        {
            gridView.CloseEditor();
            gridView.UpdateCurrentRow();

            var invalid = _rows
                .Select(ValidateRow)
                .Where(result => !string.IsNullOrWhiteSpace(result.error))
                .ToList();

            if (invalid.Count > 0)
            {
                MessageBox.Show(invalid[0].error, "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var changedRows = _rows.Where(x => x.IsNew || x.IsModified).ToList();
            if (changedRows.Count == 0)
            {
                if (showSuccessMessage)
                {
                    MessageBox.Show("Изменений для сохранения нет.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                return true;
            }

            foreach (var row in changedRows)
            {
                ApplyDisplayFields(row);
                NormalizeRowForSave(row);
                row.id = await _dbService.SaveEntityAsync("cfn.confection_norm_nitki", "id", row);
                row.IsNew = false;
                row.IsModified = false;
            }

            await LoadRowsFromDatabaseAsync();
            if (showSuccessMessage)
            {
                MessageBox.Show("Изменения сохранены.", "Справочник норм ниток", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return true;
        }

        private async Task<bool> TryCommitOnCloseAsync()
        {
            if (!HasPendingChanges())
            {
                return true;
            }

            var result = MessageBox.Show(
                "Сохранить изменения перед закрытием?",
                "Справочник норм ниток",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Cancel)
            {
                return false;
            }

            if (result == DialogResult.No)
            {
                return true;
            }

            return await SaveInternalAsync(showSuccessMessage: false);
        }

        private bool HasPendingChanges()
        {
            return _rows.Any(x => x.IsNew || x.IsModified);
        }

        private void FocusRow(ThreadNormRow row)
        {
            var handle = gridView.LocateByValue("id", row.id);
            if (handle < 0)
            {
                handle = gridView.RowCount - 1;
            }

            if (handle >= 0)
            {
                gridView.FocusedRowHandle = handle;
                gridView.FocusedColumn = gridView.Columns["men"];
                gridView.ShowEditor();
            }
        }

        private void ApplyDisplayFields(ThreadNormRow row)
        {
            var manager = _managers.FirstOrDefault(x =>
                string.Equals((x.Men ?? string.Empty).Trim(), (row.men ?? string.Empty).Trim(), StringComparison.OrdinalIgnoreCase));
            row.men_name = manager?.Name ?? string.Empty;

            var category = _categories.FirstOrDefault(x => x.TCAT_ID == row.tg_id_n);
            row.TCAT_CategoryName = category?.TCAT_CategoryName ?? string.Empty;
            row.TG_GroupName = category?.TG_GroupName ?? string.Empty;
            row.TC_ClassName = category?.TC_ClassName ?? string.Empty;

            var assort = _assorts.FirstOrDefault(x => x.TAT_ID == row.ta_id);
            row.TAT_Name = assort?.TAT_Name ?? string.Empty;

            var material = _materials.FirstOrDefault(x =>
                string.Equals(x.kod_dr, row.kod_dr, StringComparison.OrdinalIgnoreCase));
            if (material != null)
            {
                row.kod3 = material.kod3;
                row.kod_art = material.kod_art;
                row.ThreadDisplay = material.displayText;
            }
            else
            {
                row.ThreadDisplay = string.Empty;
            }
        }

        private static (ThreadNormRow row, string error) ValidateRow(ThreadNormRow row)
        {
            if (row == null)
            {
                return (row, "Пустая строка справочника.");
            }

            if (string.IsNullOrWhiteSpace(row.men))
            {
                return (row, "Не заполнен менеджер.");
            }

            if (row.tg_id_n <= 0)
            {
                return (row, "Не заполнена категория.");
            }

            if (row.ta_id <= 0)
            {
                return (row, "Не заполнен ассортимент.");
            }

            if (row.norm > 0 && string.IsNullOrWhiteSpace(row.kod_dr))
            {
                return (row, "Для нормы больше нуля нужно выбрать код ниток.");
            }

            return (row, null);
        }

        private static void NormalizeRowForSave(ThreadNormRow row)
        {
            row.men = (row.men ?? string.Empty).Trim();
            row.kod_dr = (row.kod_dr ?? string.Empty).Trim();
            row.kod3 = NormalizeNullableCode(row.kod3);
            row.kod_art = NormalizeNullableCode(row.kod_art);
        }

        private static string NormalizeNullableCode(string value)
        {
            var trimmed = (value ?? string.Empty).Trim();
            return string.IsNullOrWhiteSpace(trimmed) || string.Equals(trimmed, "null", StringComparison.OrdinalIgnoreCase)
                ? null
                : trimmed;
        }

        private async Task HandleClosingAsync()
        {
            var canClose = await TryCommitOnCloseAsync();
            if (!canClose)
            {
                return;
            }

            _allowCloseWithoutPrompt = true;
            BeginInvoke(new MethodInvoker(Close));
        }

        private async void FilterGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridView.ShowLoadingPanel();
            try
            {
                await LoadRowsFromDatabaseAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка обновления данных справочника ниток из базы: {ex.Message}",
                    "Ошибка UI",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                gridView.HideLoadingPanel();
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddRow();
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            AddCopyRow();
        }

        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            await DeleteCurrentRowAsync();
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            await SaveInternalAsync(showSuccessMessage: true);
        }

        private async void CloseButton_Click(object sender, EventArgs e)
        {
            if (await TryCommitOnCloseAsync())
            {
                _allowCloseWithoutPrompt = true;
                Close();
            }
        }

        private void GridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName is "kod_dr" or "norm")
            {
                e.Appearance.BackColor = Color.FromArgb(238, 250, 214);
            }
        }

        private void GridView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (_isLoading || e.RowHandle < 0)
            {
                return;
            }

            var row = gridView.GetRow(e.RowHandle) as ThreadNormRow;
            if (row == null)
            {
                return;
            }

            switch (e.Column.FieldName)
            {
                case "men":
                case "tg_id_n":
                case "ta_id":
                case "kod_dr":
                    ApplyDisplayFields(row);
                    bindingSource.ResetBindings(false);
                    break;
                case "approved":
                    row.approved = Convert.ToBoolean(e.Value);
                    bindingSource.ResetBindings(false);
                    break;
            }
        }

        private void GridView_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            if (e.Row is not ThreadNormRow row)
            {
                return;
            }

            var validation = ValidateRow(row);
            if (!string.IsNullOrWhiteSpace(validation.error))
            {
                e.Valid = false;
                e.ErrorText = validation.error;
            }
        }
    }
}
