using DevExpress.Data.Filtering;
using DevExpress.XtraBars.Customization;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.form;
using SewingProduction.Helpers;
using SewingProduction.Interfaces;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Forms
{
    public partial class TeamWork 
    {

        /// <summary>
        /// Обработчик смены выбранной строки в customGridControl5
        /// </summary>
        private async void gridView5_FocusedRowChanged_Internal(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                var view = sender as GridView;
                if (view == null || e.FocusedRowHandle < 0) return;

                // Получаем значение nzp из выбранной строки
                int nzp = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "kolNZP", 0);

                // Обновляем видимость кнопки в зависимости от значения nzp
                //customButton3.Enabled = nzp <= 0;
                ButtonUnboundWd.Enabled = nzp <= 0;

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при обработке смены строки в GridView5");
            }
        }

        /// <summary>
        /// Гарантирует, что `IsChecked` может быть установлен только у одной строки.
        /// Работает с `gridView7` и `gridView8`, а также с любым другим `GridView`, где используется `IsChecked`.
        /// </summary>
        /// <typeparam name="T">Тип данных, реализующий `ICheckable`</typeparam>
        /// <param name="gridControl">GridControl, где произошло изменение</param>
        /// <param name="e">Аргумент события `CellValueChangedEventArgs`</param>
        private async void GridView_CellValueChanged<T>(GridControl gridControl, CellValueChangedEventArgs e) where T : class
        {
            if (e.Column.FieldName != "IsChecked") return;

            try
            {
                SafeInvoke(gridControl, () =>
                {
                    if (!(gridControl.MainView is GridView view)) return;

                    var newCheckedRow = view.GetRow(e.RowHandle) as ICheckable;
                    if (newCheckedRow == null || !(e.Value is bool isChecked)) return;

                    if (!isChecked)
                    {
                        view.RefreshRow(e.RowHandle); // просто обновим UI для снятия флажка
                        return;
                    }

                    // Снимаем флажки со всех строк, кроме текущей
                    for (int i = 0; i < view.RowCount; i++)
                    {
                        if (i == e.RowHandle) continue; // текущую не трогаем

                        var row = view.GetRow(i) as ICheckable;
                        if (row != null && row.IsChecked)
                        {
                            row.IsChecked = false;

                        }
                    }
                    view.RefreshData();

                    // Устанавливаем флаг только текущей строке
                    newCheckedRow.IsChecked = true;

                    view.RefreshData();
                });
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при изменении значения в GridView");
                MessageBox.Show($"Ошибка при изменении значения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Обработчик изменения состояния customCheckBox6.  
        /// Фильтрует gridView8 по статусу.
        /// </summary>
        private async void customCheckBox6_CheckedChanged_Internal(object sender, EventArgs e)
        {
            try
            {
                string filterString = "";

                if (actualCheckBox1.Checked) filterString += $"status = {(int)Status.Actual}";
                if (preliminaryCheckBox1.Checked)
                {
                    if (!string.IsNullOrEmpty(filterString)) filterString += " OR ";
                    filterString += $"status = {(int)Status.Preliminary}";
                }

                // Применяем фильтр к gridView8
                gridView8.BeginUpdate();
                gridView8.ActiveFilterString = filterString;
                gridView8.EndUpdate();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при фильтрации gridView8");
                MessageBox.Show($"Ошибка при применении фильтра: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Скопировать РТ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCopyWd_Click_Internal(object sender, EventArgs e)
        {
            try
            {
                bufferId = (int)ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "AnnID");
                buffer.Text = $"группа: {ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "Group").ToString().TrimEnd(' ')},\r" +
                    $"модель: {ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "Mod").ToString().TrimEnd(' ')},\r" +
                    $"артикул: {ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "Articul").ToString().TrimEnd(' ')}";
            }
            catch
            {
                bufferId = 0;
                MessageBox.Show("Копирование не реализовано", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void RefreshData()
        {
            SafeInvoke(ANNgridControl, () =>
            {
                ANNgridControl.BeginUpdate();
                try
                {
                    _bindingSource.ResetBindings(false);
                    ANNgridControl.RefreshDataSource();
                    ANNgridView.RefreshData();
                    filterTable();
                }
                finally
                {
                    ANNgridControl.EndUpdate();
                }
            });
        }

        private void SafeInvoke(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        /// <summary>
        /// Обрабатывает смену выбранной  строки в gridView3 - разделениях труда
        /// Загружает связанные данные в другие таблицы и обновляет UI.
        /// </summary>
        private async void gridView3_FocusedRowChanged_Internal(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            try
            {
                var view = ANNgridView;

                // Получаем значения из выбранной строки
                string comment = CommonFunctions.GetRowCellValueOrDefault<string>(view, e.FocusedRowHandle, "Komment", "");
                int constructorId = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "Constr", 0);
                int designerId = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "Diz", 0);

                // Log the retrieved IDs
                await _logger.LogEventAsync($"Constructor ID: {constructorId}, Designer ID: {designerId}", "RowChange");

                // Устанавливаем значения в соответствующие элементы управления
                commentRichTextBox.Text = comment;

                // Fetch and set the constructor's full name
                string constructorName = await _artNormService.GetEmployeeFullName(constructorId);
                constructorTextBox.Text = constructorName;

                // Fetch and set the designer's full name
                string designerName = await _artNormService.GetEmployeeFullName(designerId);
                designerTextBox.Text = designerName;

                int annId = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "AnnID", 0);
                //UpdateRelatedData(annId);
                await LoadRelatedData(annId);

                string kod = CommonFunctions.GetRowCellValueOrDefault<string>(view, e.FocusedRowHandle, "Kod", "");
                int o = 0;
                try { o = Convert.ToInt32(kod); }
                catch (Exception ex) { await _logger.LogErrorAsync(ex, "опять КОД это строка"); return; }
                finally { if (o > 0) LoadGridControlData(pictureBox1, o); }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при смене выбранной строки в gridView3");
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        /// <summary>
        /// Редактировать РТ
        /// </summary>
        private void ButtonEditWd_Click_Internal(object sender, EventArgs e)
        {
            try
            {
                int _rowNumber = ANNgridView.FocusedRowHandle;
                if (_rowNumber < 0)
                {
                    MessageBox.Show("Выберите запись для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // // Создаём новую запись модели `ArtNorm` из выбранной строки
                //// ArtNormN newItem = ANNgridView.GetRow(_rowNumber) as ArtNormN;

                // // Получаем ID выбранной записи
                // int selectedAnnId = (int)ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "AnnID");
                var selectedItem = ANNgridView.GetRow(_rowNumber) as ArtNormN;
                if (selectedItem == null) return;

                int selectedAnnId = selectedItem.AnnID;
                // Открываем форму редактирования
                using (TeamWork_AdvanceTW teamWorkAdvanceTW = new TeamWork_AdvanceTW(
                   bufferId,
                    (int)Mode.Edit, oldId: selectedAnnId))
                {
                    DialogResult result = teamWorkAdvanceTW.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        var updatedItem = teamWorkAdvanceTW.CreatedAnn;

                        if (updatedItem != null)
                        {
                            // Находим индекс и заменяем запись в списке
                            int index = _bindingList.IndexOf(_bindingList.FirstOrDefault(x => x.AnnID == updatedItem.AnnID));
                            if (index >= 0)
                            {
                                _bindingList[index] = updatedItem;
                                _bindingSource.ResetBindings(false);

                                // Обновляем выделение и перерисовываем строку
                                int rowHandle = ANNgridView.LocateByValue("AnnID", updatedItem.AnnID);
                                if (rowHandle >= 0)
                                {
                                    ANNgridView.BeginUpdate();
                                    try
                                    {
                                        ANNgridView.FocusedRowHandle = rowHandle;
                                        ANNgridView.RefreshRow(rowHandle);
                                    }
                                    finally
                                    {
                                        ANNgridView.EndUpdate();
                                    }
                                }
                            }
                        }
                        LoadRelatedData(selectedAnnId);

                        // Отображаем сообщение об успешном редактировании
                        MessageBox.Show(
                            "Запись успешно отредактирована.",
                            "Информация",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // Обрабатываем возможные ошибки
                _logger.LogErrorAsync(ex, "Ошибка при редактировании записи");
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void gridView7_FocusedRowChanged_Internal(object sender, FocusedRowChangedEventArgs e)
        {
            int kod = CommonFunctions.GetRowCellValueOrDefault<int>(gridView7, e.FocusedRowHandle, "Kod", 0);
            string articul = CommonFunctions.GetRowCellValueOrDefault<string>(gridView7, e.FocusedRowHandle, "Articul", "");


            BindingList<MyDataANN> list = loadAllCheckBox.Checked ?
                await LoadWorksbyArt(0, "") :
    await LoadWorksbyArt(kod, articul);

            customGridControl2.DataSource = list; //LoadWorksbyArt(kod, articul);
        }
        private void gridControl2_Leave_Internal(object sender, EventArgs e)
        {
            selectedRowHandle = ANNgridView.FocusedRowHandle;
        }

        private void gridControl2_GotFocus_Internal(object sender, EventArgs e)
        {
            if (selectedRowHandle >= 0)
            {
                gridView1.FocusedRowHandle = selectedRowHandle;
                gridView1.SelectRow(selectedRowHandle);
                selectedRowHandle = -1; //Сбрасываем после восстановления выделения
            }
        }

        /// <summary>
        /// обработка клика на заголовке, 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView8_CellValueChanged_Internal(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "IsChecked")
            {
                SafeInvoke(gridView8.GridControl, () =>
                {
                    var view = sender as GridView;
                    if (view == null) return;

                    var currentRow = view.GetRow(e.RowHandle) as MyDataANN;
                    if (currentRow == null) return;

                    bool isChecked = (bool)e.Value;

                    // Если текущая строка отмечается
                    if (isChecked)
                    {
                        // Сначала снимаем все отметки
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            var row = view.GetRow(i) as MyDataANN;
                            if (row != null)
                            {
                                row.IsChecked = false;
                            }
                        }
                        // Затем отмечаем только текущую строку
                        currentRow.IsChecked = true;
                    }

                    view.RefreshData();
                });
            }
        }

        //обработка клика на заголовке
        private void gridView7_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "IsChecked")
            {
                SafeInvoke(gridView7.GridControl, () =>
                {
                    var view = sender as GridView;
                    if (view == null) return;

                    var currentRow = view.GetRow(e.RowHandle) as MyDataART;
                    if (currentRow == null) return;

                    bool isChecked = (bool)e.Value;

                    // Если текущая строка отмечается
                    if (isChecked)
                    {
                        // Сначала снимаем все отметки
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            var row = view.GetRow(i) as MyDataART;
                            if (row != null)
                            {
                                row.IsChecked = false;
                            }
                        }
                        // Затем отмечаем только текущую строку
                        currentRow.IsChecked = true;
                    }

                    view.RefreshData();
                });
            }
        }
        #region Поиск и фильтрация

        private async void SearchButton_Click_Internal(object sender, EventArgs e)
        {
            try
            {
                string searchText = searchControl1.Text.TrimEnd(' ');
                string columnName = await GridHelper.GetSelectedColumnNameAsync(kode.Checked, articul.Checked, model.Checked, group.Checked);

                if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(searchText))
                {
                    // Создаем фильтр поиска
                    var searchFilter = new FunctionOperator(
                    FunctionOperatorType.Contains,
                    new OperandProperty(columnName),
                    new OperandValue(searchText));

                    // Создаем фильтры на основе состояния чекбоксов
                    CriteriaOperator statusCriteria = null;

                    // Создаем фильтр по статусу
                    if (preliminaryCheckBox.Checked || actualCheckBox.Checked || archiveCheckBox.Checked)
                    {
                        var statusFilters = new List<CriteriaOperator>();

                        if (preliminaryCheckBox.Checked)
                            statusFilters.Add(new BinaryOperator("status", (int)Status.Preliminary));

                        if (actualCheckBox.Checked)
                        {
                            statusFilters.Add(new BinaryOperator("status", (int)Status.Actual));
                            statusFilters.Add(new BinaryOperator("status", (int)Status.PreliminaryArchive));
                        }

                        if (archiveCheckBox.Checked)
                            statusFilters.Add(new BinaryOperator("status", (int)Status.Archive));

                        if (statusFilters.Count > 1)
                        {
                            statusCriteria = new GroupOperator(GroupOperatorType.Or, statusFilters.ToArray());
                        }
                        else if (statusFilters.Count == 1)
                        {
                            statusCriteria = statusFilters[0];
                        }
                    }

                    // Добавляем фильтр по "Не описанные" если выбран
                    if (SortBox.Checked)
                    {
                        var notDescribedFilter = new GroupOperator(
                            GroupOperatorType.And,
                            new BinaryOperator("sek_shv", 0),
                            new BinaryOperator("status", 0, DevExpress.Data.Filtering.BinaryOperatorType.Greater)
                        );

                        if (statusCriteria != null)
                        {
                            statusCriteria = new GroupOperator(
                                GroupOperatorType.And,
                                statusCriteria,
                                notDescribedFilter
                            );
                        }
                        else
                        {
                            statusCriteria = notDescribedFilter;
                        }
                    }

                    // Если есть фильтр статуса, объединяем его с фильтром поиска
                    if (statusCriteria != null)
                    {
                        ANNgridView.ActiveFilterCriteria = new GroupOperator(
                            GroupOperatorType.And,
                            searchFilter,
                            statusCriteria
                        );
                    }
                    else
                    {
                        ANNgridView.ActiveFilterCriteria = searchFilter;
                    }
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при поиске в SearchButton_Click");
                MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Фильтрация данных в gridView3 по введенному значению в filterTextBox1.
        /// </summary>
        private async void customButton12_Click_Internal(object sender, EventArgs e)
        {
            try
            {
                string filterString = filterTextBox1.Text.Trim(); // Получаем текст из поля ввода
                string columnName = await GridHelper.GetSelectedColumnNameAsync(kode.Checked, articul.Checked, model.Checked, group.Checked); // Определяем, по какой колонке искать

                if (!string.IsNullOrEmpty(filterString) && !string.IsNullOrEmpty(columnName))
                {
                    // Применяем фильтр к gridView3
                    ANNgridView.ActiveFilterCriteria = new DevExpress.Data.Filtering.FunctionOperator(
                        DevExpress.Data.Filtering.FunctionOperatorType.Contains,
                        new DevExpress.Data.Filtering.OperandProperty(columnName),
                        new DevExpress.Data.Filtering.OperandValue(filterString)
                    );
                }
                else
                {
                    MessageBox.Show("Введите значение для поиска и выберите колонку!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при поиске по customButton12_Click");
                MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void searchControl1_QueryIsSearchColumn_Internal(object sender, DevExpress.XtraEditors.QueryIsSearchColumnEventArgs args)
        {
            string colName = await GridHelper.GetSelectedColumnNameAsync(kode.Checked, articul.Checked, model.Checked, group.Checked);
            args.IsSearchColumn = args.FieldName == colName;
        }

        private async void customCheckBox4_CheckedChanged_Internal(object sender, EventArgs e)
        {
            int kod = CommonFunctions.GetRowCellValueOrDefault<int>(gridView7, gridView7.FocusedRowHandle, "kod", 0);
            string articul = CommonFunctions.GetRowCellValueOrDefault<string>(gridView7, gridView7.FocusedRowHandle, "articul", "");

            BindingList<MyDataANN> list = loadAllCheckBox.Checked ?
                await LoadWorksbyArt(0, "") :
                await LoadWorksbyArt(kod, articul);
            customGridControl2.DataSource = list;//loadAllCheckBox.Checked ? LoadWorksbyArt(0, "") : LoadWorksbyArt(kod, articul);
        }

        private void simpleButton2_Click_Internal(object sender, EventArgs e)
        {
            //   new TeamWork_AdvanceTW(bufferWorkDivision, (int)Mode.NewWorkDivision).ShowDialog();
        }

        /// <summary>
        /// Переключение фильтров при изменении чекбоксов
        /// </summary>
        private void Filter_CheckedChanged_Internal(object sender, EventArgs e) => filterTable();

        /// <summary>
        /// Обработчик смены выбранного поля поиска при изменении параметров поиска.
        /// </summary>
        private async void search_CheckedChanged_Internal(object sender, EventArgs e)
        {
            try
            {
                // Получаем текст текущего поиска
                string searchText = searchControl1.Text.TrimEnd(' ');

                // Очищаем текущий фильтр поиска
                searchControl1.ClearFilter();

                // Если есть текст поиска, применяем его к новому выбранному полю
                if (!string.IsNullOrEmpty(searchText))
                {
                    // Создаем фильтр поиска для нового выбранного поля
                    string columnName = await GridHelper.GetSelectedColumnNameAsync(kode.Checked, articul.Checked, model.Checked, group.Checked);
                    if (!string.IsNullOrEmpty(columnName))
                    {
                        var searchFilter = new FunctionOperator(
                            FunctionOperatorType.Contains,
                            new OperandProperty(columnName),
                            new OperandValue(searchText));

                        // Получаем текущий фильтр статусов
                        CriteriaOperator statusFilter = GetStatusFilter();

                        // Если есть фильтр статусов, объединяем его с новым фильтром поиска
                        if (statusFilter != null)
                        {
                            ANNgridView.ActiveFilterCriteria = new GroupOperator(
                                GroupOperatorType.And,
                                searchFilter,
                                statusFilter
                            );
                        }
                        else
                        {
                            ANNgridView.ActiveFilterCriteria = searchFilter;
                        }
                    }
                }
                else
                {
                    // Если нет текста поиска, применяем только фильтр статусов
                    CriteriaOperator statusFilter = GetStatusFilter();
                    if (statusFilter != null)
                    {
                        ANNgridView.ActiveFilterCriteria = statusFilter;
                    }
                    else
                    {
                        ANNgridView.ActiveFilterString = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при обновлении параметров поиска");
            }
        }


        #endregion


    }
}
