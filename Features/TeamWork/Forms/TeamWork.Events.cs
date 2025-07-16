using DevExpress.Data.Filtering;
using DevExpress.XtraBars.Customization;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraScheduler.Commands;
using DevExpress.XtraScheduler.Reporting;
using DevExpress.XtraVerticalGrid;
using SewingProduction.form;
using SewingProduction.Helpers;
using SewingProduction.Interfaces;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
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

                if (actualCheckBox.Checked) filterString += $"status = {(int)Status.Actual}";
                if (preliminaryCheckBox.Checked)
                {
                    if (!string.IsNullOrEmpty(filterString)) filterString += " OR ";
                    filterString += $"status = {(int)Status.Preliminary}";
                }

                // Применяем фильтр к gridView8
                gridView_wdToBind.BeginUpdate();
                gridView_wdToBind.ActiveFilterString = filterString;
                gridView_wdToBind.EndUpdate();
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
                var annId = (int)ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "AnnID");
                var selectedItem = ANNgridView.GetRow(ANNgridView.FocusedRowHandle) as ArtNormN;
                
                var displayText = $"группа: {ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "grup").ToString().TrimEnd(' ')},\n\r" +
                    $"модель: {ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "Mod").ToString().TrimEnd(' ')},\n\r" +
                    $"артикул: {ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "Articul").ToString().TrimEnd(' ')}";

                // Копируем в глобальный буфер
                TeamWorkBuffer.CopyToBuffer(annId, displayText, selectedItem);
                
                // Обновляем локальный буфер для обратной совместимости
                bufferId = annId;
                buffer.Text = displayText;

                // Показываем статус в statusLabel (если он существует)
                if (this.Controls.Find("statusLabel", true).FirstOrDefault() is Label statusLabel)
                {
                    statusLabel.Text = "Данные скопированы в буфер";
                    // Автоматически очищаем через 3 секунды
                    _ = Task.Delay(3000).ContinueWith(t => 
                    {
                        if (!this.IsDisposed && statusLabel != null)
                        {
                            this.Invoke((MethodInvoker)(() => statusLabel.Text = ""));
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                bufferId = 0;
                TeamWorkBuffer.ClearBuffer();
                
                // Показываем ошибку в statusLabel (если он существует)
                if (this.Controls.Find("statusLabel", true).FirstOrDefault() is Label statusLabel)
                {
                    statusLabel.Text = $"Ошибка копирования: {ex.Message}";
                    statusLabel.ForeColor = Color.Red;
                    // Автоматически очищаем через 5 секунд
                    _ = Task.Delay(5000).ContinueWith(t => 
                    {
                        if (!this.IsDisposed && statusLabel != null)
                        {
                            this.Invoke((MethodInvoker)(() => 
                            {
                                statusLabel.Text = "";
                                statusLabel.ForeColor = Color.Black;
                            }));
                        }
                    });
                }
                else
                {
                    MessageBox.Show($"Ошибка копирования: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Обрабатывает смену выбранной  строки в ANNGridView - разделениях труда
        /// Загружает связанные данные в другие таблицы и обновляет UI.
        /// </summary>
        private async void ANNgridView_FocusedRowChanged_Internal(object sender, FocusedRowChangedEventArgs e)
        {
            var view = sender as GridView;
            if (!await PrepareUiAsync(view, e.FocusedRowHandle)) return;

            var oldCts = Interlocked.Exchange(ref _loadCts, new CancellationTokenSource());
            oldCts?.Cancel();
            oldCts?.Dispose();
            var token = _loadCts.Token;

            //bool flowControl = await ButtonsEnabled(e, view);
            //if (!flowControl)
            //{
            //    return;
            //}

            try
            {
                int annId = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "AnnID", 0);
                
                // Добавляем небольшую задержку для предотвращения частых вызовов при быстром поиске
                await Task.Delay(200, token);
                
                var tRelated = LoadRelatedData(annId, token);
                var tNzp = LoadNZP(annId, token);
                await Task.WhenAll(tRelated, tNzp);
            }
            catch (OperationCanceledException)
            {
                // тихо игнорируем
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при смене выбранной строки в {view.Name} (RowHandle: {e.FocusedRowHandle})");
            }
        }

        public async Task<bool> PrepareUiAsync(GridView view, int FocusedRowHandle)
        {
            try
            {
                if (view == null || FocusedRowHandle < 0)
                {
                    ButtonArchAndCopyWd.Enabled = false;
                    return false;
                }

                var selectedItem = view.GetRow(FocusedRowHandle) as ArtNormN;
                LoadGridImage(pictureBox1, annId: selectedItem.AnnID); if (selectedItem != null)
                {
                    bool disableButton = selectedItem.Status == (int)Status.Archive
                                      || selectedItem.Status == (int)Status.PreliminaryArchive;
                    ButtonArchAndCopyWd.Enabled = !disableButton;
                    textEditMod.Text = selectedItem.Mod?.TrimEnd(' ') ?? string.Empty;
                    textEditArt.Text = selectedItem.Articul?.TrimEnd(' ') ?? string.Empty;
                    textEditSec.Text = selectedItem.Sek.ToString();
                    textEditCreate.Text = selectedItem.dateCreate.HasValue ? selectedItem.dateCreate.Value.ToString("dd.MM.yyyy") : string.Empty;
                }
                else
                {
                    ButtonArchAndCopyWd.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex); return false;
            }

            return true;
        }

        private async Task LoadNZP(int annId, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var list = annId > 0 ? await _artNormService.GetNzpWithPztCounts(annId, ct) : new List<NZPByKoddRt>();
            ct.ThrowIfCancellationRequested();
            _nzpListWd.RaiseListChangedEvents = false;
            _nzpListWd.Clear();
            foreach (var item in list)
                _nzpListWd.Add(item);
            _nzpListWd.RaiseListChangedEvents = true;

            _nzpByKoddRtSourceWd.ResetBindings(false);
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
                                        ANNgridView.MakeRowVisible(rowHandle); // Прокручиваем до строки
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


        private async Task EditWd_Internal2(GridView gridView, IList list, BindingSource bindingSource, bool forMyDataAnnView = false)
        {
            try
            {
                int rowNumber = gridView.FocusedRowHandle;
                if (rowNumber < 0)
                {
                    MessageBox.Show("Выберите запись для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int annId;
                ArtNormN selectedArtNormN = null;

                if (!forMyDataAnnView)
                {
                    // Первая вкладка: объект - ArtNormN
                    var selectedAnn = gridView.GetRow(rowNumber) as ArtNormN;
                    if (selectedAnn == null) return;
                    annId = selectedAnn.AnnID;
         //          selectedArtNormN.CopyPropertiesFrom(selectedAnn);// = selectedAnn;
                    selectedArtNormN = selectedAnn.CloneProperties();
                }
                else
                {
                    // Вторая вкладка: объект - MyDataANN (нужно получить ArtNormN по AnnID)
                    var selectedMyDataAnn = gridView.GetRow(rowNumber) as MyDataANN;
                    if (selectedMyDataAnn == null) return;
                    annId = selectedMyDataAnn.AnnID;
                    selectedArtNormN = await _artNormService.GetArtNormDataById(annId);
                    if (selectedArtNormN == null) return;
                }
                var updatedArtNormN = new ArtNormN();
                using (var teamWorkAdvanceTW = new TeamWork_AdvanceTW(bufferId, (int)Mode.Edit, oldId: annId))
                {
                    DialogResult result = teamWorkAdvanceTW.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        updatedArtNormN = teamWorkAdvanceTW.CreatedAnn;
                        if (updatedArtNormN == null) return;

                        object updatedDataAnn = forMyDataAnnView
                            ? ToMyDataANN(updatedArtNormN)
                            : updatedArtNormN;

                        int annIdToFind = forMyDataAnnView
                            ? ((MyDataANN)updatedDataAnn).AnnID
                            : ((ArtNormN)updatedDataAnn).AnnID;

                        int index = list.Cast<object>()
                            .Select((item, i) => new { item, i })
                            .FirstOrDefault(x => forMyDataAnnView
                                ? x.item is MyDataANN mda && mda.AnnID == annIdToFind
                                : x.item is ArtNormN an && an.AnnID == annIdToFind)
                            ?.i ?? -1;

                        if (index >= 0)
                            list[index] = updatedDataAnn;

                        bindingSource.ResetBindings(false);
                        int rowHandle = gridView.LocateByValue("AnnID", updatedArtNormN.AnnID);
                        if (rowHandle >= 0)
                        {
                            gridView.BeginUpdate();
                            try
                            {
                                gridView.FocusedRowHandle = rowHandle;
                                gridView.MakeRowVisible(rowHandle); // Прокручиваем до строки
                                gridView.RefreshRow(rowHandle);
                            }
                            finally
                            {
                                gridView.EndUpdate();
                            }
                        }

                        // Если редактирование было для второй вкладки (MyDataANN view), обновим NormRasz для customGridControl3
                        if (forMyDataAnnView && updatedArtNormN != null && updatedArtNormN.AnnID > 0)
                        {
                            await RefreshNormRaszForArticlesTab(updatedArtNormN.AnnID, CancellationToken.None);
                        }
                        else if (!forMyDataAnnView && updatedArtNormN != null && updatedArtNormN.AnnID > 0) // Иначе, если для первой вкладки
                        {
                            await LoadRelatedData(updatedArtNormN.AnnID); // Загружаем связанные данные для первой вкладки 
                        }
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
                    AnnID = 0, // БД назначит ID
                    Mod = selectedArtData.mod,
                    grup = selectedArtData.grup,
                    Articul = selectedArtData.Articul

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
                        ANNgridView.MakeRowVisible(rowHandle); // Прокручиваем до строки
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
                                itemInList.CopyPropertiesFrom(createdOrUpdatedAnn);
                                itemInList.StatusText = StatusHelper.GetStatusText(itemInList.Status); // Обновляем текстовый статус
                            }

                            _myDataAnnBindingSource.ResetBindings(false);
                            int finalRowHandle = gridView_wdToBind.LocateByValue("AnnID", newAnnId);
                            if (finalRowHandle != GridControl.InvalidRowHandle) 
                            {
                                gridView_wdToBind.RefreshRow(finalRowHandle);
                                // Фокусируемся на созданной записи в gridView_wdToBind
                                gridView_wdToBind.FocusedRowHandle = finalRowHandle;
                                gridView_wdToBind.MakeRowVisible(finalRowHandle); // Прокручиваем до строки
                            }
                            gridView_wdToBind.RefreshData();
                            
                            // Также фокусируемся на записи в основном ANNgridView
                            _bindingSource.ResetBindings(false);
                            int annRowHandle = ANNgridView.LocateByValue("AnnID", newAnnId);
                            if (annRowHandle >= 0)
                            {
                                ANNgridView.BeginUpdate();
                                try
                                {
                                    ANNgridView.FocusedRowHandle = annRowHandle;
                                    ANNgridView.MakeRowVisible(annRowHandle); // Прокручиваем до строки
                                    ANNgridView.RefreshRow(annRowHandle);
                                }
                                finally
                                {
                                    ANNgridView.EndUpdate();
                                }
                            }

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

                        //MessageBox.Show("Создание новой записи отменено.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await _logger.LogEventAsync($"Создание новой записи ANN (ID: {newAnnId}) отменено пользователем.", "simpleButton2_Click_Internal_Cancel");
                      //  await ShowStatusMessage("Сохранение данных...");


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



        #region Поиск и фильтрация
        private async void loadAllCheckBox_CheckedChanged_Internal(object sender, EventArgs e)
        {
            List<MyDataANN> list = null;
            if (loadAllCheckBox.Checked)
                list = await LoadWorksbyArt(0, "");
            else if (!loadAllCheckBox.Checked)
            {
                string kod = gridView_unboundArts.GetRowCellValue(gridView_unboundArts.FocusedRowHandle, "Kod").ToString();
                if (!int.TryParse(kod, out int kodInt))
                {
                    await _logger.LogWarningAsync($"Не удалось преобразовать Kod '{kod}' в число", "gridView_unboundArts_FocusedRowChanged_Internal");
                    kodInt = 0;
                }

                string articul = gridView_unboundArts.GetRowCellValue(gridView_unboundArts.FocusedRowHandle, "Articul").ToString();
                list = await LoadWorksbyArt(kodInt, articul);
            }
            // gridControl_wdToBind.DataSource = list;//loadAllCheckBox.Checked ? LoadWorksbyArt(0, "") : LoadWorksbyArt(kod, articul);
            //var bindingList = new BindingList<MyDataANN>(list);
            //_myDataAnnBindingSource = new BindingSource(bindingList, null);
            //gridControl_wdToBind.DataSource = _myDataAnnBindingSource;
            _myDataAnnList.Clear();
                        if (list != null)
                            {
                _myDataAnnList.RaiseListChangedEvents = false;
                                foreach (var item in list)
                                    {
                    _myDataAnnList.Add(item);
                                    }
                _myDataAnnList.RaiseListChangedEvents = true;
                            }
            _myDataAnnBindingSource.ResetBindings(false);
            gridView_wdToBind.RefreshData();
        }


        /// <summary>
        /// Переключение фильтров при изменении чекбоксов
        /// </summary>
        private void Filter_CheckedChanged_Internal(object sender, EventArgs e) => filterTable();

        #endregion


    }
}
