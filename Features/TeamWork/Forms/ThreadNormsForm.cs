using DevExpress.XtraGrid.Columns;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors.ButtonsPanelControl;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.Models;
using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Features.TeamWork.Services;
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
        private readonly ILogger _logger = new FileLogger();
        private readonly TWGridHelper _gridHelper = new TWGridHelper();
        private readonly ThreadNormsDataService _dataService;
        private readonly BindingList<ThreadNormRow> _rows = new BindingList<ThreadNormRow>();
        private List<ThreadAssortModel> _assorts = new List<ThreadAssortModel>();
        private List<ThreadMaterialOption> _materials = new List<ThreadMaterialOption>();

        private bool _isAutoSavingRowChange;
        private bool _isLoading;
        private bool _isSyncingFilterButtons;
        private bool _zeroNormOnly = true;
        private bool _allowCloseWithoutPrompt;
        private const string HeaderButtonZeroNorm = "thread-norms:zero-norm";
        private const string HeaderButtonAll = "thread-norms:all";
        private const string HeaderButtonRefresh = "thread-norms:refresh";

        public ThreadNormsForm(UserClass user) : base(user)
        {
            InitializeComponent();

            var dbService = new DbService(new DatabaseHelperSQL());
            _dataService = new ThreadNormsDataService(dbService, _logger);
            bindingSource.DataSource = _rows;
            InitializeHeaderButtons();
        }

        private async void ThreadNormsForm_Load(object sender, EventArgs e)
        {
            _gridHelper.LoadGridViewSettings(gridView, "ThreadNormsGrid.xml");
            await LoadDataAsync();
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

        private async Task LoadDataAsync()
        {
            gridView.ShowLoadingPanel();
            try
            {
                await LoadReferenceDataAsync();
                await LoadRowsAsync();
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

        private async Task LoadReferenceDataAsync()
        {
            _assorts = await _dataService.LoadAssortsAsync() ?? new List<ThreadAssortModel>();
            assortLookup.DataSource = _assorts;

            try
            {
                _materials = await _dataService.LoadMaterialsAsync() ?? new List<ThreadMaterialOption>();
            }
            catch (Exception ex)
            {
                _materials = new List<ThreadMaterialOption>();
                await _logger.LogErrorAsync(ex, "Не удалось загрузить справочник материалов ниток для копирования");
            }
        }

        private async Task LoadRowsAsync()
        {
            _isLoading = true;
            try
            {
                gridView.CloseEditor();
                gridView.UpdateCurrentRow();

                var loaded = await _dataService.LoadRowsAsync(_zeroNormOnly);
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

            var targetKodDrList = new[] { 2501, 2502 };
            ThreadNormRow lastAdded = null;

            foreach (int kodDr in targetKodDrList)
            {
                if (HasRowsForAllAssorts(current.tg_id_n, kodDr))
                    continue;

                var material = ResolveThreadMaterial(kodDr);

                var copy = current.CloneForCopy(
                    kodDr,
                    material.kod3,
                    material.kodArt,
                    material.displayText);

                _rows.Add(copy);
                lastAdded = copy;
            }
            bindingSource.ResetBindings(false);
            if (lastAdded != null)
            {
                FocusRow(lastAdded);
            }
            else
            {
                MessageBox.Show(
                    "Для выбранной категории строки 2501 и 2502 уже созданы для всех доступных ассортиментов.",
                    "Копирование норм ниток",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private bool HasRowsForAllAssorts(int categoryId, int kodDr)
        {
            int assortCount = _assorts?.Count ?? 0;
            if (categoryId <= 0 || kodDr <= 0 || assortCount <= 0)
            {
                return false;
            }

            int existingRowsCount = _rows.Count(x =>
                !x.IsDeleted &&
                x.tg_id_n == categoryId &&
                x.kod_dr == kodDr);

            return existingRowsCount >= assortCount;
        }

        private (string kod3, string kodArt, string displayText) ResolveThreadMaterial(int kodDr)
        {
            var material = FindThreadMaterial(kodDr);

            if (material != null)
            {
                return (
                    material.kod3 ?? string.Empty,
                    material.kod_art ?? string.Empty,
                    material.displayText ?? string.Empty
                );
            }

            // Страховка, если справочник не загрузился
            return kodDr switch
            {
                2501 => ("2501632", "0242", "2501"),
                2502 => ("2502443", "0144", "2502"),
                _ => (string.Empty, string.Empty, kodDr.ToString())
            };
        }
        private async Task DeleteCurrentRowAsync()
        {
            if (bindingSource.Current is not ThreadNormRow current)
            {
                return;
            }

            if (current.approved)
            {
                MessageBox.Show(
                    "Утвержденную строку нельзя изменять или удалять.",
                    "Справочник норм ниток",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Удалить выбранную строку?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            if (current.id > 0)
            {
                await _dataService.DeleteAsync(current);
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

            if (!ValidateDuplicates())
            {
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
                var dbRow = ThreadNormDbRow.ToDbRow(row);
                row.id = await _dataService.SaveAsync(dbRow);
                row.IsNew = false;
                row.IsModified = false;
            }

            await LoadRowsAsync();
            if (showSuccessMessage)
            {
                MessageBox.Show("Изменения сохранены.", "Справочник норм ниток", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return true;
        }
        private bool ValidateDuplicates()
        {
            var duplicates = _rows
                .Where(x => !x.IsDeleted)
                .GroupBy(x => new
                {
                    x.tg_id_n,
                    x.ta_id,
                    kod_dr = x.kod_dr,
                    //kod3 = x.kod3?.Trim(),
                    //kod_art = x.kod_art?.Trim()
                })
                .Where(g => g.Count() > 1)
                .ToList();

            if (duplicates.Count != 0)
            {
                MessageBox.Show(
                    "В справочнике есть дублирующиеся нормы ниток. Сохранение невозможно.",
                    "Проверка норм ниток",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        private bool HasDuplicateThreadNorm(ThreadNormRow row, int assortId)
        {
            if (row == null || assortId <= 0)
            {
                return false;
            }

           // var kodDr = NormalizeCode(row.kod_dr);
            return _rows.Any(x =>
                !ReferenceEquals(x, row) &&
                !x.IsDeleted &&
                x.tg_id_n == row.tg_id_n &&
                x.ta_id == assortId &&
                //string.Equals(NormalizeCode(x.kod_dr), kodDr, StringComparison.OrdinalIgnoreCase));
                x.kod_dr == row.kod_dr);
        }

        private static string NormalizeCode(string value)
        {
            return value?.Trim() ?? string.Empty;
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
                gridView.FocusedColumn = gridView.Columns["norm"];
                gridView.ShowEditor();
            }
        }

        private static (ThreadNormRow row, string error) ValidateRow(ThreadNormRow row)
        {
            if (row == null)
            {
                return (row, "Пустая строка справочника.");
            }

            if (row.tg_id_n <= 0)
            {
                return (row, "Не заполнена категория.");
            }

            if (row.ta_id <= 0)
            {
                return (row, "Не заполнен ассортимент.");
            }

            if (row.norm > 0 && row.kod_dr<=0)
            {
                return (row, "Для нормы больше нуля нужно выбрать код ниток.");
            }

            return (row, null);
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


        private void GridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName is "norm" or "date_change")
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
                //case "tg_id_n":
                case "ta_id":
                    //case "kod_dr":
                    //ApplyDisplayFields(row);
                    //bindingSource.ResetBindings(false);
                    //break;
                    var assort = _assorts.FirstOrDefault(x => x.TAT_ID == row.ta_id);
                    row.TAT_Name = assort?.TAT_Name ?? string.Empty;
                    bindingSource.ResetBindings(false);
                    break;
                case "approved":
                    row.approved = Convert.ToBoolean(e.Value);
                    bindingSource.ResetBindings(false);
                    break;
                case "date_change":
                    bindingSource.ResetBindings(false);
                    break;
            }
        }

        private void GridView_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (gridView.FocusedColumn?.FieldName != nameof(ThreadNormRow.ta_id))
            {
                return;
            }

            if (gridView.GetFocusedRow() is not ThreadNormRow row)
            {
                return;
            }

            var assortId = ToInt(e.Value);
            if (!HasDuplicateThreadNorm(row, assortId))
            {
                return;
            }

            e.Valid = false;
            e.ErrorText = "Такая норма ниток уже есть. Выберите другой ассортимент.";
        }

        private static int ToInt(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return 0;
            }

            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }

        private async void GridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (_isLoading || _isAutoSavingRowChange || e.PrevFocusedRowHandle < 0)
            {
                return;
            }

            if (gridView.GetRow(e.PrevFocusedRowHandle) is not ThreadNormRow previousRow)
            {
                return;
            }

            if (!previousRow.IsNew && !previousRow.IsModified)
            {
                return;
            }

            _isAutoSavingRowChange = true;
            try
            {
                await SaveInternalAsync(showSuccessMessage: false);
            }
            finally
            {
                _isAutoSavingRowChange = false;
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

        private void GridView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (gridView.FocusedRowHandle < 0)
            {
                return;
            }

            if (gridView.GetRow(gridView.FocusedRowHandle) is not ThreadNormRow row)
            {
                return;
            }

            if (!row.approved)
            {
                return;
            }
            // Утверждённую строку нельзя менять, кроме самой галки "Утверждено"
            if (gridView.FocusedColumn?.FieldName == nameof(ThreadNormRow.approved))
                return;
            e.Cancel = true;
        }

        private async void customSimpleButton1_Click(object sender, EventArgs e)
        {
            await ReloadRowsAsync("обновлением данных");
        }

        private void InitializeHeaderButtons()
        {
            SetHeaderButtonTag("В работе", HeaderButtonZeroNorm);
            SetHeaderButtonTag("Все", HeaderButtonAll);
            SetHeaderButtonTag("Обновить", HeaderButtonRefresh);

            //    layoutControlGroup2.CustomButtonClick += LayoutControlGroup2_CustomButtonClick;
            _zeroNormOnly = true;
            SetFilterControls(_zeroNormOnly);
        }

        private void SetHeaderButtonTag(string caption, string tag)
        {
            var button = layoutControlGroup2.CustomHeaderButtons
                .OfType<GroupBoxButton>()
                .FirstOrDefault(x => string.Equals(x.Caption?.Trim(), caption, StringComparison.OrdinalIgnoreCase));

            if (button != null)
            {
                button.Tag = tag;
            }
        }

        private async void LayoutControlGroup2_CustomButtonClick(object sender, BaseButtonEventArgs e)
        {
            var tag = (e.Button as GroupBoxButton)?.Tag as string;

            switch (tag)
            {
                case HeaderButtonRefresh:
                    await ReloadRowsAsync("обновлением данных");
                    break;
            }
        }

        private async Task SelectFilterAsync(bool zeroNormOnly)
        {
            if (_zeroNormOnly == zeroNormOnly)
            {
                SetFilterControls(_zeroNormOnly);
                return;
            }

            var previousZeroNormOnly = _zeroNormOnly;
            if (!await ConfirmPendingChangesAsync("сменой фильтра"))
            {
                SetFilterControls(previousZeroNormOnly);
                return;
            }

            _zeroNormOnly = zeroNormOnly;
            SetFilterControls(_zeroNormOnly);
            await ReloadRowsAsync("сменой фильтра", promptPendingChanges: false);
        }

        private void SetFilterControls(bool zeroNormOnly)
        {
            _isSyncingFilterButtons = true;
            try
            {
                SetHeaderButtonChecked(HeaderButtonZeroNorm, zeroNormOnly);
                SetHeaderButtonChecked(HeaderButtonAll, !zeroNormOnly);
            }
            finally
            {
                _isSyncingFilterButtons = false;
            }
        }

        private void SetHeaderButtonChecked(string tag, bool isChecked)
        {
            var button = layoutControlGroup2.CustomHeaderButtons
                .OfType<GroupBoxButton>()
                .FirstOrDefault(x => string.Equals(x.Tag as string, tag, StringComparison.OrdinalIgnoreCase));

            if (button != null && button.Checked != isChecked)
            {
                button.Checked = isChecked;
            }
        }

        private bool IsHeaderButtonChecked(string tag)
        {
            var button = layoutControlGroup2.CustomHeaderButtons
                .OfType<GroupBoxButton>()
                .FirstOrDefault(x => string.Equals(x.Tag as string, tag, StringComparison.OrdinalIgnoreCase));

            return button?.Checked == true;
        }

        private async Task<bool> ReloadRowsAsync(string pendingChangesAction, bool promptPendingChanges = true)
        {
            if (promptPendingChanges && !await ConfirmPendingChangesAsync(pendingChangesAction))
            {
                return false;
            }

            gridView.ShowLoadingPanel();
            try
            {
                await LoadRowsAsync();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка обновления данных справочника ниток из базы: {ex.Message}",
                    "Ошибка UI",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                gridView.HideLoadingPanel();
            }
        }

        private async Task<bool> ConfirmPendingChangesAsync(string action)
        {
            if (!HasPendingChanges())
            {
                return true;
            }

            var result = MessageBox.Show(
                $"Есть несохранённые изменения. Сохранить перед {action}?",
                "Нормы ниток",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Cancel)
            {
                return false;
            }

            if (result == DialogResult.Yes)
            {
                return await SaveInternalAsync(showSuccessMessage: false);
            }

            return true;
        }

        private async void layoutControlGroup2_CustomButtonChecked(object sender, BaseButtonEventArgs e)
        {
            if (_isSyncingFilterButtons)
            {
                return;
            }

            var tag = (e.Button as GroupBoxButton)?.Tag as string;

            switch (tag)
            {
                case HeaderButtonZeroNorm:
                    await SelectFilterAsync(zeroNormOnly: true);
                    break;
                case HeaderButtonAll:
                    await SelectFilterAsync(zeroNormOnly: false);
                    break;
            }
        }

        private void layoutControlGroup2_CustomButtonUnchecked(object sender, BaseButtonEventArgs e)
        {
            if (_isSyncingFilterButtons)
            {
                return;
            }

            var tag = (e.Button as GroupBoxButton)?.Tag as string;
            if (tag is HeaderButtonZeroNorm or HeaderButtonAll)
            {
                BeginInvoke(new MethodInvoker(RestoreFilterSelectionIfNeeded));
            }
        }

        private void RestoreFilterSelectionIfNeeded()
        {
            if (_isSyncingFilterButtons)
            {
                return;
            }

            if (!IsHeaderButtonChecked(HeaderButtonZeroNorm) && !IsHeaderButtonChecked(HeaderButtonAll))
            {
                SetFilterControls(_zeroNormOnly);
            }
        }
        private ThreadMaterialOption FindThreadMaterial(int kodDr)
        {
            return _materials.FirstOrDefault(x =>
                int.TryParse(x.kod_dr, out var parsed) && parsed == kodDr);
        }
    }
    public sealed class ThreadMaterialOption
    {
        public string kod_dr { get; set; } = string.Empty;
        public string kod3 { get; set; } = string.Empty;
        public string kod_art { get; set; } = string.Empty;
        public string displayText { get; set; } = string.Empty;
    }
}
