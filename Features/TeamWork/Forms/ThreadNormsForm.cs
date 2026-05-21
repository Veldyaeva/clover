using DevExpress.XtraGrid.Columns;
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

        private bool _isLoading;
        private bool _allowCloseWithoutPrompt;

        public ThreadNormsForm(UserClass user) : base(user)
        {
            InitializeComponent();

            var dbService = new DbService(new DatabaseHelperSQL());
            _dataService = new ThreadNormsDataService(dbService, _logger);
            bindingSource.DataSource = _rows;
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

        private async Task LoadRowsAsync()
        {
            _isLoading = true;
            try
            {
                gridView.CloseEditor();
                gridView.UpdateCurrentRow();

                var loaded = await _dataService.LoadRowsAsync(filterGroup.SelectedIndex == 0);
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
                    men = x.men?.Trim(),
                    x.tg_id_n,
                    x.ta_id,
                    kod_dr = x.kod_dr?.Trim(),
                    //kod3 = x.kod3?.Trim(),
                    //kod_art = x.kod_art?.Trim()
                })
                .Where(g => g.Count() > 1)
                .ToList();

            if (duplicates.Any())
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

            if (row.norm > 0 && string.IsNullOrWhiteSpace(row.kod_dr))
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

        private async void FilterGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HasPendingChanges())
            {
                var result = MessageBox.Show(
                    "Есть несохранённые изменения. Сохранить перед сменой фильтра?",
                    "Нормы ниток",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                    return;

                if (result == DialogResult.Yes)
                {
                    if (!await SaveInternalAsync(showSuccessMessage: false))
                        return;
                }
            }
            gridView.ShowLoadingPanel();
            try
            {
                await LoadRowsAsync();
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
                //case "ta_id":
                //case "kod_dr":
                //    ApplyDisplayFields(row);
                //    bindingSource.ResetBindings(false);
                //    break;
                case "approved":
                    row.approved = Convert.ToBoolean(e.Value);
                    bindingSource.ResetBindings(false);
                    break;
                case "date_change":
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
    }
}
