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
using System.Drawing.Text;
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
                if (view == null || e.FocusedRowHandle < 0)
                {
                    ButtonUnboundWd.Enabled = false; 
                    return;
                }

                // Получаем объект выбранной строки
                var selectedRow = view.GetRow(e.FocusedRowHandle) as NZPByKoddRt;
                if (selectedRow == null)
                {
                    ButtonUnboundWd.Enabled = false; 
                    await _logger.LogWarningAsync($"Не удалось получить объект NZPByKoddRt для строки {e.FocusedRowHandle}", "gridView5_FocusedRowChanged_Internal");
                    return;
                }

                int nzp = selectedRow.kolNZP;
                int pzt = selectedRow.PZTCount; 

                // Кнопка активна, если либо нет НЗП, либо нет PZT операций
                ButtonUnboundWd.Enabled = (nzp <= 0 || pzt <= 0);
            }
            catch (Exception ex)
            {
                ButtonUnboundWd.Enabled = false; 
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
        private async void GridView_CellValueChanged<T>(GridControl gridControl, CellValueChangedEventArgs e) where T : class, ICheckable
        {
            if (e.Column.FieldName != nameof(ICheckable.IsChecked)) return;

            // Используем поле класса _isUnchecking
            if (_isUnchecking) return;

            var view = gridControl.MainView as GridView;
            if (view == null) return;

            var currentItem = view.GetRow(e.RowHandle) as T;
            if (currentItem == null) return;

            bool isChecked = Convert.ToBoolean(e.Value);

            if (isChecked)
            {
                // Устанавливаем флаг перед изменением других строк
                _isUnchecking = true;
                try
                {
                    var dataSource = view.DataSource as IList<T>; 
                    if (dataSource == null)
                    {
                        if (view.DataSource is BindingSource bs && bs.DataSource is IList<T> list)
                        {
                            dataSource = list;
                        }
                    }

                    if (dataSource != null)
                    {
                        foreach (var item in dataSource)
                        {
                            if (item == currentItem) continue;

                            if (item is ICheckable checkableItem && checkableItem.IsChecked)
                            {
                                // Устанавливаем IsChecked в false для других элементов
                                checkableItem.IsChecked = false;
                            }
                        }
                        // Обновляем данные после цикла, чтобы избежать лишних обновлений
                        view.RefreshData();
                    }
                }
                finally
                {
                    // Сбрасываем флаг в любом случае
                     _isUnchecking = false;
                }
            }
            _hasUnsavedChanges = true;
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
                gridView_twToBind.BeginUpdate();
                gridView_twToBind.ActiveFilterString = filterString;
                gridView_twToBind.EndUpdate();
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

        /// <summary>
        /// Обрабатывает смену выбранной  строки в gridView3 - разделениях труда
        /// Загружает связанные данные в другие таблицы и обновляет UI.
        /// </summary>
        private async void gridView3_FocusedRowChanged_Internal(object sender, FocusedRowChangedEventArgs e)
        {
            // Используем имя view из sender
            var view = sender as GridView;

            // Проверяем валидность view и rowHandle
            if (view == null || e.FocusedRowHandle < 0)
            {
                // Если строка не выбрана или view невалиден, отключаем кнопку
                ButtonArchAndCopyWd.Enabled = false;
                return; // Выходим, если строка не выбрана
            }

            // --- Новая логика для кнопки "Архив+Копия" ---
            var selectedItem = view.GetRow(e.FocusedRowHandle) as ArtNormN;
            if (selectedItem != null)
            {
                // Кнопка НЕ доступна для статусов Архив и Предв.Архив
                bool disableButton = selectedItem.Status == (int)Status.Archive 
                                  || selectedItem.Status == (int)Status.PreliminaryArchive;
                ButtonArchAndCopyWd.Enabled = !disableButton;
            }
            else
            {
                // Если не удалось получить объект строки, отключаем кнопку
                ButtonArchAndCopyWd.Enabled = false;
            }
            // --- Конец новой логики ---
            
            // --- Существующая логика для связанных данных и картинки ---
            try
            {
                int annId = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "AnnID", 0);
                await LoadRelatedData(annId); // Загрузка связанных данных по AnnID

                // Получаем Kod как строку, проверяем на null/пустоту
                string kodString = view.GetRowCellValue(e.FocusedRowHandle, "Kod")?.ToString();

                if (!string.IsNullOrEmpty(kodString))
                {
                    // Пытаемся преобразовать в int безопасно
                    if (int.TryParse(kodString, out int kodValue) && kodValue > 0)
                    {
                        // Загружаем данные для картинки только если kodValue > 0
                        // Убедись, что pictureBox1 доступен из этого контекста
                        LoadGridControlData(pictureBox1, kodValue); 
                    }
                    else
                    {
                        // Логируем, если не удалось преобразовать или kodValue <= 0
                        await _logger.LogWarningAsync($"Не удалось преобразовать Kod '{kodString}' в корректное число > 0 для строки {e.FocusedRowHandle}.", "gridView3_FocusedRowChanged_Internal");
                    }
                }
                else
                {
                    // Логируем, если Kod пустой или null
                    await _logger.LogWarningAsync($"Значение Kod пустое или null для строки {e.FocusedRowHandle}.", "gridView3_FocusedRowChanged_Internal");
                }
            }
            catch (Exception ex)
            {
                 // Логируем другие неожиданные ошибки
                await _logger.LogErrorAsync(ex, $"Ошибка при смене выбранной строки в {view.Name} (RowHandle: {e.FocusedRowHandle})");
                // MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); // Опционально
            }
        }



        /// <summary>
        /// Редактировать РТ
        /// </summary>
        private async void ButtonEditWd_Click_Internal(object sender, EventArgs e)
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
                        await LoadRelatedData(selectedAnnId);

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
                await _logger.LogErrorAsync(ex, "Ошибка при редактировании записи");
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //private async void gridView7_FocusedRowChanged_Internal(object sender, FocusedRowChangedEventArgs e)
        //{
        //    string kod = CommonFunctions.GetRowCellValueOrDefault<string>(gridView7, e.FocusedRowHandle, "Kod", "");
        //    string articul = CommonFunctions.GetRowCellValueOrDefault<string>(gridView7, e.FocusedRowHandle, "Articul", "");


        //    List<MyDataANN> list = loadAllCheckBox.Checked ? await LoadWorksbyArt(0, "") : await LoadWorksbyArt(Convert.ToInt32(kod), articul);

        //    customGridControl2.DataSource = list; 
        //}
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

        ///// <summary>
        ///// Переключение фильтров при изменении чекбоксов
        ///// </summary>
        //private void Filter_CheckedChanged_Internal(object sender, EventArgs e) => filterTable();

        ///// <summary>
        ///// Обработчик смены выбранного поля поиска при изменении параметров поиска.
        ///// </summary>
        //private async void search_CheckedChanged_Internal(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Получаем текст текущего поиска
        //        string searchText = searchControl1.Text.TrimEnd(' ');

        //        // Очищаем текущий фильтр поиска
        //        searchControl1.ClearFilter();

        //        // Если есть текст поиска, применяем его к новому выбранному полю
        //        if (!string.IsNullOrEmpty(searchText))
        //        {
        //            // Создаем фильтр поиска для нового выбранного поля
        //            string columnName = await GridHelper.GetSelectedColumnNameAsync(kode.Checked, articul.Checked, model.Checked, group.Checked);
        //            if (!string.IsNullOrEmpty(columnName))
        //            {
        //                var searchFilter = new FunctionOperator(
        //                    FunctionOperatorType.Contains,
        //                    new OperandProperty(columnName),
        //                    new OperandValue(searchText));

        //                // Получаем текущий фильтр статусов
        //                CriteriaOperator statusFilter = GetStatusFilter();

        //                // Если есть фильтр статусов, объединяем его с новым фильтром поиска
        //                if (statusFilter != null)
        //                {
        //                    ANNgridView.ActiveFilterCriteria = new GroupOperator(
        //                        GroupOperatorType.And,
        //                        searchFilter,
        //                        statusFilter
        //                    );
        //                }
        //                else
        //                {
        //                    ANNgridView.ActiveFilterCriteria = searchFilter;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            // Если нет текста поиска, применяем только фильтр статусов
        //            CriteriaOperator statusFilter = GetStatusFilter();
        //            if (statusFilter != null)
        //            {
        //                ANNgridView.ActiveFilterCriteria = statusFilter;
        //            }
        //            else
        //            {
        //                ANNgridView.ActiveFilterString = string.Empty;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, "Ошибка при обновлении параметров поиска");
        //    }
        //}


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
            int kod = CommonFunctions.GetRowCellValueOrDefault<int>(gridView_unboundArts, gridView_unboundArts.FocusedRowHandle, "kod", 0);
            string articul = CommonFunctions.GetRowCellValueOrDefault<string>(gridView_unboundArts, gridView_unboundArts.FocusedRowHandle, "articul", "");

            List<MyDataANN> list = loadAllCheckBox.Checked ?
                await LoadWorksbyArt(0, "") :
                await LoadWorksbyArt(kod, articul);
            gridControl_wdToBind.DataSource = list;//loadAllCheckBox.Checked ? LoadWorksbyArt(0, "") : LoadWorksbyArt(kod, articul);
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
