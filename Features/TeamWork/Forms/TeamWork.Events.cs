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
using System.Data;
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
            //try
            //{
            //    string filterString = "";

            //    if (actualCheckBox1.Checked) filterString += $"status = {(int)Status.Actual}";
            //    if (preliminaryCheckBox1.Checked)
            //    {
            //        if (!string.IsNullOrEmpty(filterString)) filterString += " OR ";
            //        filterString += $"status = {(int)Status.Preliminary}";
            //    }

            //    // Применяем фильтр к gridView8
            //    gridView_wdToBind.BeginUpdate();
            //    gridView_wdToBind.ActiveFilterString = filterString;
            //    gridView_wdToBind.EndUpdate();
            //}
            //catch (Exception ex)
            //{
            //    await _logger.LogErrorAsync(ex, "Ошибка при фильтрации gridView8");
            //    MessageBox.Show($"Ошибка при применении фильтра: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
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
        /// Обрабатывает смену выбранной  строки в ANNGridView - разделениях труда
        /// Загружает связанные данные в другие таблицы и обновляет UI.
        /// </summary>
        private async void ANNgridView_FocusedRowChanged_Internal(object sender, FocusedRowChangedEventArgs e)
        {
            var view = sender as GridView;
            if (view == null || e.FocusedRowHandle < 0)
            {
                ButtonArchAndCopyWd.Enabled = false;
                return; 
            }

            var selectedItem = view.GetRow(e.FocusedRowHandle) as ArtNormN;
            if (selectedItem != null)
            {
                bool disableButton = selectedItem.Status == (int)Status.Archive 
                                  || selectedItem.Status == (int)Status.PreliminaryArchive;
                ButtonArchAndCopyWd.Enabled = !disableButton;
            }
            else
            {
                ButtonArchAndCopyWd.Enabled = false;
            }
            
            try
            {
                int annId = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "AnnID", 0);
                await LoadRelatedData(annId);

                string kodString = view.GetRowCellValue(e.FocusedRowHandle, "Kod")?.ToString();

                if (!string.IsNullOrEmpty(kodString))
                {
                    if (int.TryParse(kodString, out int kodValue) && kodValue > 0)
                    {
                        LoadGridControlData(pictureBox1, kodValue); 
                    }
                    else
                    {
                        await _logger.LogWarningAsync($"Не удалось преобразовать Kod '{kodString}' в корректное число > 0 для строки {e.FocusedRowHandle}.", "ANNgridView_FocusedRowChanged_Internal");
                    }
                }
                else
                {
                    await _logger.LogWarningAsync($"Значение Kod пустое или null для строки {e.FocusedRowHandle}.", "ANNgridView_FocusedRowChanged_Internal");
                }
            }
            catch (Exception ex)
            {
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
                var selectedItem = ANNgridView.GetRow(_rowNumber) as ArtNormN;
                if (selectedItem == null) return;

                int selectedAnnId = selectedItem.AnnID;
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
                await _logger.LogErrorAsync(ex, "Ошибка при редактировании записи");
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                selectedRowHandle = -1;
            }
        }

 

        #region Поиск и фильтрация

        private async void SearchButton_Click_Internal(object sender, EventArgs e)
        {
            try
            {
                string searchText = searchControl1.Text.TrimEnd(' ');
                string columnName = await TWGridHelper.GetSelectedColumnNameAsync(kode.Checked, articul.Checked, model.Checked, group.Checked);

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
        /// Фильтрация данных в ANNgridView по введенному значению в filterTextBox1.
        /// </summary>
        private async void customButton12_Click_Internal(object sender, EventArgs e)
        {
            try
            {
                string filterString = filterTextBox1.Text.Trim(); // Получаем текст из поля ввода
                string columnName = await TWGridHelper.GetSelectedColumnNameAsync(kode.Checked, articul.Checked, model.Checked, group.Checked); // Определяем, по какой колонке искать

                if (!string.IsNullOrEmpty(filterString) && !string.IsNullOrEmpty(columnName))
                {
                    // Применяем фильтр к ANNgridView
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
            string colName = await TWGridHelper.GetSelectedColumnNameAsync(kode.Checked, articul.Checked, model.Checked, group.Checked);
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

        private async void simpleButton2_Click_Internal(object sender, EventArgs e)
        {
            ArtNormN newItemShell = null; // Объявляем здесь, чтобы был доступен в catch и finally (если нужно)
            int newAnnId = 0;

            try
            {
                int focusedRowHandle = gridView_unboundArts.FocusedRowHandle;
                if (focusedRowHandle < 0)
                {
                    MessageBox.Show("Пожалуйста, выберите запись из таблицы артикулов.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedArtData = gridView_unboundArts.GetRow(focusedRowHandle) as MyDataART;

                if (selectedArtData == null)
                {
                    MessageBox.Show("Не удалось получить данные выбранного артикула.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await _logger.LogWarningAsync($"Не удалось получить/преобразовать MyDataART из gridView_unboundArts, строка: {focusedRowHandle}", "simpleButton2_Click_Internal");
                    return;
                }

                // 1. Создаем "оболочку" ArtNormN
                newItemShell = new ArtNormN
                {
                    // Основные поля будут заполнены в TeamWork_AdvanceTW из InitialArtData
                    // Здесь устанавливаем только необходимые для вставки и начального отображения значения
                    Kod = "0000000", // Или другой плейсхолдер, если нужно
                    Status = (int)Status.Preliminary, // Новая запись всегда предварительная
                    StatusText = StatusHelper.GetStatusText((int)Status.Preliminary),
                    dateCreate = DateTime.Now,
                    Diz = 0, // Значения по умолчанию или будут установлены в TeamWork_AdvanceTW
                    Constr = 0,
                    Arh = false,
                    AnnID = 0 // БД назначит ID
                };

                // 2. Вставляем "оболочку" в БД для получения AnnID
                newAnnId = await _dbService.InsertEntityAsync(TableNames.Ann, TableNames.AnnId, newItemShell);

                if (newAnnId <= 0)
                {
                    MessageBox.Show("Не удалось создать новую запись в базе данных.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await _logger.LogErrorAsync(null, "Ошибка при вставке новой ArtNormN (AnnID <= 0) simpleButton2_Click_Internal");
                    return;
                }
                newItemShell.AnnID = newAnnId; // Присваиваем полученный ID

                // 3. Добавляем "оболочку" в основной список и грид (если _bindingList используется для ANNgridView)
                if (_bindingList != null) // Убедитесь, что _bindingList - это источник для ANNgridView
                {
                    _bindingList.Add(newItemShell);
                    _bindingSource?.ResetBindings(false); // Обновляем DataGridView
                    ANNgridControl?.RefreshDataSource(); // Обновляем контрол, если ResetBindings недостаточно

                    // Позиционируемся на новой строке
                    int rowHandle = ANNgridView.LocateByValue("AnnID", newAnnId);
                    if (rowHandle != GridControl.InvalidRowHandle)
                    {
                        ANNgridView.FocusedRowHandle = rowHandle;
                    }
                }
                else
                {
                    await _logger.LogWarningAsync("_bindingList is null, cannot add newItemShell to UI.", "simpleButton2_Click_Internal");
                }

                // 4. Открываем форму TeamWork_AdvanceTW
                using (TeamWork_AdvanceTW teamWorkAdvanceTW = new TeamWork_AdvanceTW(
                    0,
                    (int)Mode.NewWorkDivision,
                    newId: newAnnId) 
                   )
                {
                    teamWorkAdvanceTW.InitialArtData = selectedArtData; // Передаем данные из MyDataART
                    DialogResult result = teamWorkAdvanceTW.ShowDialog();

                    // 5. Обрабатываем результат диалога
                    if (result == DialogResult.OK)
                    {
                        var createdOrUpdatedAnn = teamWorkAdvanceTW.CreatedAnn;
                        if (createdOrUpdatedAnn != null)
                        {
                            // Находим и обновляем элемент в _bindingList
                            var itemInList = _bindingList?.FirstOrDefault(ann => ann.AnnID == newAnnId);
                            if (itemInList != null)
                            {
                                // Копируем свойства из возвращенного объекта в объект в списке
                                // Нужен метод CopyPropertiesFrom в ArtNormN или ручное копирование
                                itemInList.CopyPropertiesFrom(createdOrUpdatedAnn); // Предполагается, что такой метод есть
                                itemInList.StatusText = StatusHelper.GetStatusText(itemInList.Status); // Обновляем текстовый статус
                            }

                            _myDataAnnBindingSource.ResetBindings(false);
                            int finalRowHandle = gridView_wdToBind.LocateByValue("AnnID", newAnnId);
                            if (finalRowHandle != GridControl.InvalidRowHandle) gridView_wdToBind.RefreshRow(finalRowHandle);
                            gridView_wdToBind.RefreshData();

                            await _logger.LogEventAsync($"Запись ANN (ID: {newAnnId}) успешно создана/обновлена из артикула.", "simpleButton2_Click_Internal");
                            MessageBox.Show("Новая предварительная запись успешно создана/обновлена.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else // DialogResult.Cancel или другое
                    {
                        await _logger.LogEventAsync($"Создание записи ANN (ID: {newAnnId}) отменено пользователем в TeamWork_AdvanceTW.", "simpleButton2_Click_Internal");
                        // Удаляем "оболочку" из списка
                        if (newItemShell != null && _bindingList != null && _bindingList.Contains(newItemShell))
                        {
                            _bindingList.Remove(newItemShell);
                        }
                        _bindingSource?.ResetBindings(false); // Обновить DataGridView
                        ANNgridControl?.RefreshDataSource();

                        // Удаляем запись из БД и связанные данные
                        await _artNormService.DeleteByAnnId(TableNames.Ann, newAnnId);
                        if (teamWorkAdvanceTW.IsRaszInserted) // Проверяем, были ли вставлены связанные данные
                            await _artNormService.DeleteByAnnId(TableNames.Rasz, newAnnId);
                        if (teamWorkAdvanceTW.IsRaskInserted)
                            await _artNormService.DeleteByAnnId(TableNames.Rask, newAnnId);
                        if (teamWorkAdvanceTW.IsKontInserted)
                            await _artNormService.DeleteByAnnId(TableNames.Kont, newAnnId);
                        if (teamWorkAdvanceTW.IsDopObrInserted) // Если есть логика для доп. обработки
                            await _artNormService.DeleteByAnnId(TableNames.Obr, newAnnId);

                        MessageBox.Show("Создание новой записи отменено.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                // Обновляем основную таблицу после всех операций
                filterTable(); // Вызываем метод обновления/фильтрации главной таблицы ANN
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Критическая ошибка в simpleButton2_Click_Internal");
                MessageBox.Show($"Произошла критическая ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Попытка откатить создание AnnID если он был создан до ошибки
                if (newAnnId > 0)
                {
                    await _logger.LogEventAsync($"Попытка отката AnnID: {newAnnId} из-за ошибки.", "simpleButton2_Click_Internal_Catch");
                    if (newItemShell != null && _bindingList != null && _bindingList.Contains(newItemShell))
                    {
                        _bindingList.Remove(newItemShell);
                        _bindingSource?.ResetBindings(false);
                    }
                    await _artNormService.DeleteByAnnId(TableNames.Ann, newAnnId);
                    // Здесь не можем проверить IsRaszInserted и т.д. из формы, если ошибка была до ее закрытия
                }
            }
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
                string searchText = searchControl1.Text.TrimEnd(' ');

              //  searchControl1.ClearFilter();

                if (!string.IsNullOrEmpty(searchText))
                {
                    string columnName = await TWGridHelper.GetSelectedColumnNameAsync(kode.Checked, articul.Checked, model.Checked, group.Checked);
                    if (!string.IsNullOrEmpty(columnName))
                    {
                        var searchFilter = new FunctionOperator(
                            FunctionOperatorType.Contains,
                            new OperandProperty(columnName),
                            new OperandValue(searchText));

                        CriteriaOperator statusFilter = GetStatusFilter();

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
