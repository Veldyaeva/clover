using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Models;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using SewingProduction.Services;
using SewingProduction.Helpers;

namespace SewingProduction.Forms
{
    public partial class TeamWork
    {
        #region First Tab Fields
        private BindingList<ArtNormN> _bindingList = new BindingList<ArtNormN>();
        private BindingSource _bindingSource = new BindingSource();
        private int selectedRowHandle = -1;
        private int bufferId = 0;

        #endregion

        #region First Tab Methods
        private async Task LoadWorkDivisions()
        {
            try
            {
                if (ANNgridControl == null || ANNgridView == null)
                {
                    throw new InvalidOperationException("Критические компоненты формы не инициализированы");
                }

                var data = await _artNormService.GetArtNormData();
                if (data == null)
                {
                    throw new InvalidOperationException("Не удалось загрузить данные");
                }

                await this.InvokeAsync(() =>
                {
                    try
                    {
                        // Очищаем текущие данные
                        _bindingList.Clear();
                        
                        // Добавляем новые данные
                        foreach (var item in data)
                        {
                            _bindingList.Add(item);
                        }

                        // Обновляем привязку данных
                        _bindingSource.DataSource = _bindingList;
                        ANNgridControl.DataSource = _bindingSource;

                        // Настраиваем отображение дат
                        if (ANNgridView.Columns["dateCreate"] != null)
                        {
                            ANNgridView.Columns["dateCreate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                            ANNgridView.Columns["dateCreate"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
                        }

                        if (ANNgridView.Columns["dataUpdate"] != null)
                        {
                            ANNgridView.Columns["dataUpdate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                            ANNgridView.Columns["dataUpdate"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
                        }

                        // Обновляем отображение
                        ANNgridControl.RefreshDataSource();
                        ANNgridView.RefreshData();

                        // Сбрасываем фильтры и сортировку
                        ANNgridView.ActiveFilterString = string.Empty;
                        ANNgridView.ClearSorting();

                        // Применяем фильтры по статусу
                        filterTable();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogErrorAsync(ex, "Ошибка при обновлении UI");
                        throw;
                    }
                });
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке разделений труда");
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ButtonEditWd_Click(object sender, EventArgs e)
        {
            if (ANNgridView.GetSelectedRows().Length > 0)
            {
                var selectedRow = ANNgridView.GetSelectedRows()[0];
                var selectedItem = ANNgridView.GetRow(selectedRow) as ArtNormN;

                if (selectedItem != null)
                {
                    using (var editForm = new TeamWork_AdvanceTW(bufferId, (int)Mode.Edit, null, selectedItem.AnnID))
                    {
                        if (editForm.ShowDialog() == DialogResult.OK)
                        {
                            await LoadWorkDivisions();
                        }
                    }
                }
            }
        }

        private async void ButtonPreliminaryWd_Click(object sender, EventArgs e)
        {
            try
            {
                var newItem = new ArtNormN();
                int newId = await _artNormService.InsertANN(newItem);

                if (newId > 0)
                {
                    using (var teamWorkForm = new TeamWork_AdvanceTW(bufferId, (int)Mode.NewWorkDivision, newId))
                    {
                        if (teamWorkForm.ShowDialog() == DialogResult.OK)
                        {
                            await LoadWorkDivisions();
                        }
                        else
                        {
                            // Если диалог отменен, удаляем созданную запись
                            await _artNormService.DeleteANN(newId);
                            _bindingList.Remove(newItem);
                            ANNgridControl.RefreshDataSource();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при создании предварительного разделения труда");
                MessageBox.Show("Ошибка при создании предварительного разделения труда", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void searchControl1_QueryIsSearchColumn(object sender, DevExpress.XtraEditors.QueryIsSearchColumnEventArgs args)
        {
            await Task.Run(() =>
            {
                args.IsSearchColumn = args.Column.FieldName == "Articul" || 
                                    args.Column.FieldName == "Group" || 
                                    args.Column.FieldName == "Mod";
            });
        }

        private async void customCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            await LoadWorkDivisions();
        }

        private void filterTable()
        {
            try
            {
                string filterExpression = string.Empty;

                if (preliminaryCheckBox.Checked)
                    filterExpression = AppendFilter(filterExpression, "[Status] = 0");

                if (actualCheckBox.Checked)
                    filterExpression = AppendFilter(filterExpression, "[Status] = 1");

                if (archiveCheckBox.Checked)
                    filterExpression = AppendFilter(filterExpression, "[Status] = 2");

                ANNgridView.ActiveFilterString = filterExpression;
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при фильтрации таблицы");
            }
        }

        private string AppendFilter(string currentFilter, string newCondition)
        {
            return string.IsNullOrEmpty(currentFilter) 
                ? newCondition 
                : $"{currentFilter} OR {newCondition}";
        }
        #endregion
    }
} 