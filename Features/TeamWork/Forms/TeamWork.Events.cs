using System;
using System.Collections;
using System.Collections.Generic;
using DevExpress.Data.Filtering;
using DevExpress.XtraBars.Customization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using DevExpress.XtraScheduler.Commands;
using DevExpress.XtraScheduler.Reporting;
using DevExpress.XtraVerticalGrid;
using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.form;
using SewingProduction.Helpers;
using SewingProduction.Interfaces;
using SewingProduction.Models;
using SewingProduction.Report;
using SewingProduction.Services;
using SewingProduction.Features.TeamWork.Models;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork
    {

        /// <summary>
        /// Обработчик смены выбранной строки в customGridControl5
        /// </summary>
        private async void gridViewNZP_FocusedRowChanged_Internal(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {

                var view = sender as GridView;
                if (view == null || e.FocusedRowHandle < 0)
                {
                    ButtonUnbindWd.Enabled = false;
                    return;
                }

                // Получаем объект выбранной строки
                var selectedRow = view.GetRow(e.FocusedRowHandle) as NZPByKoddRt;
                if (selectedRow == null)
                {
                    ButtonUnbindWd.Enabled = false;
                    await _logger.LogWarningAsync($"Не удалось получить объект NZPByKoddRt для строки {e.FocusedRowHandle}", "gridView5_FocusedRowChanged_Internal");
                    return;
                }

                int nzp = selectedRow.kolNZP;
                int pzt = selectedRow.PZTCount;

                // Кнопка активна, если либо нет НЗП, либо нет PZT операций
                ButtonUnbindWd.Enabled = (nzp <= 0 || pzt <= 0);
            }
            catch (Exception ex)
            {
                ButtonUnbindWd.Enabled = false;
                await _logger.LogErrorAsync(ex, "Ошибка при обработке смены строки в GridView5");
            }
        }

        /// <summary>
        /// Гарантирует, что `IsChecked` может быть установлен только у одной строки.
        /// Работает с `gridView7` и `gridView_wdToBind`, а также с любым другим `GridView`, где используется `IsChecked`.
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
        /// Фильтрует gridView_wdToBind по статусу.
        /// </summary>
        private async void customCheckBox6_CheckedChanged_Internal(object sender, EventArgs e)
        {
            try
            {
                string filterString = "";

                if (actualCheckBox.Checked) filterString += $"Status = {(int)Status.Actual}";
                if (preliminaryCheckBox.Checked)
                {
                    if (!string.IsNullOrEmpty(filterString)) filterString += " OR ";
                    filterString += $"Status = {(int)Status.Preliminary}";
                }

                // Применяем фильтр к gridView_wdToBind
                gridView_wdToBind.BeginUpdate();
                gridView_wdToBind.ActiveFilterString = filterString;
                gridView_wdToBind.EndUpdate();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при фильтрации gridView_wdToBind");
                MessageBox.Show($"Ошибка при применении фильтра: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Скопировать РТ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async Task ButtonCopyWd_Click_Internal(object sender, EventArgs e)
        {
            try
            {
                await _logger.LogEventAsync("Начало копирования записей в буфер", "ButtonCopyWd_Click_Internal");
                // Проверяем текущий режим работы
                bool isKitMode = toggleSwitchKit.IsOn;

                int[] selectedRows = ANNgridView.GetSelectedRows();
                if (selectedRows == null || selectedRows.Length == 0)
                {
                    string message = isKitMode ? "Выберите две записи для создания комплекта." : "Выберите запись для копирования.";
                    MessageBox.Show(message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    await _logger.LogWarningAsync("Попытка копирования без выбора записей", "ButtonCopyWd_Click_Internal");
                    return;
                }

                if (isKitMode)
                {
                    // В режиме комплекта должно быть выбрано точно 2 записи
                    if (selectedRows.Length != 2)
                    {
                        MessageBox.Show("Для создания комплекта выберите точно две записи.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        await _logger.LogWarningAsync("Попытка создания комплекта с неверным количеством выбранных записей", "ButtonCopyWd_Click_Internal");
                        return;
                    }
                }
                else
                {
                    // В обычном режиме должна быть выбрана только 1 запись
                    if (selectedRows.Length != 1)
                    {
                        MessageBox.Show("В обычном режиме можно выбрать только одну запись.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        await _logger.LogWarningAsync("Попытка копирования с выбором нескольких записей в обычном режиме", "ButtonCopyWd_Click_Internal");
                        return;
                    }
                }

                var annIds = new List<int>();
                var displayBuilder = new System.Text.StringBuilder();
                ArtNormN firstSelectedItem = null;

                foreach (int rowHandle in selectedRows)
                {
                    // Получаем AnnID
                    var annIdVal = ANNgridView.GetRowCellValue(rowHandle, "AnnID");
                    if (annIdVal == null || annIdVal == DBNull.Value || !int.TryParse(annIdVal.ToString(), out int annIdTmp))
                    {
                        continue;
                    }
                    annIds.Add(annIdTmp);

                    // Получаем строку как ArtNormN для передачи в буфер (для первой выбранной строки)
                    if (firstSelectedItem == null)
                    {
                        firstSelectedItem = ANNgridView.GetRow(rowHandle) as ArtNormN;
                    }

                    // Функция для безопасного получения значений полей
                    string GetSafeValue(string fieldName)
                    {
                        var value = ANNgridView.GetRowCellValue(rowHandle, fieldName);
                        if (value == null || value == DBNull.Value)
                            return " ";
                        string stringValue = value.ToString();
                        return string.IsNullOrEmpty(stringValue) ? " " : stringValue.TrimEnd(' ');
                    }

                    var grupVal = GetSafeValue("grup");
                    var modVal = GetSafeValue("Mod");
                    var articulVal = GetSafeValue("Articul");
                    // Добавляем в текст буфера информацию о каждой записи на новой строке
                    if (displayBuilder.Length > 0) displayBuilder.AppendLine().AppendLine("----------------------------");
                    displayBuilder.AppendLine($"группа: {grupVal},");
                    displayBuilder.AppendLine($"модель: {modVal},");
                    displayBuilder.Append($"артикул: {articulVal}");
                }
                if (annIds.Count == 0)
                {
                    MessageBox.Show("Не удалось получить идентификаторы выбранных записей.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await _logger.LogWarningAsync("Не удалось получить AnnID выбранных записей", "ButtonCopyWd_Click_Internal");
                    return;
                }

                var combinedDisplayText = displayBuilder.ToString();

                // Копируем в глобальный буфер (обновлённый метод принимает коллекцию идентификаторов)
                TeamWorkBuffer.CopyToBuffer(annIds, combinedDisplayText, firstSelectedItem);

                // Обновляем локальный буфер для обратной совместимости: сохраняем первый идентификатор и текст
                bufferId = annIds.First();
                buffer.Text = combinedDisplayText;

                // Показываем статус в statusLabel (если он существует)
                if (this.Controls.Find("statusLabel", true).FirstOrDefault() is Label statusLabel)
                {
                    statusLabel.ForeColor = System.Drawing.Color.Black;
                    statusLabel.Text = annIds.Count > 1 ? "Данные двух записей скопированы в буфер" : "Данные скопированы в буфер";
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
                    await _logger.LogWarningAsync($"Ошибка копирования: {ex.Message}", "ButtonCopyWd_Click_Internal");
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

                var tRelated = LoadRelatedDataFromView(annId, token);
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


        /// <summary>
        /// Выполняет поиск артикулов по тексту из searchControl1 в таблицах WDtoBind и unboundArts
        /// </summary>
        private async Task SearchArticulesByText()
        {
            try
            {
                string searchText = searchControl1.Text?.Trim();
                if (string.IsNullOrEmpty(searchText))
                {
                    await _logger.LogWarningAsync("Пустой текст для поиска артикулов", "SearchArticulesByText");
                    return;
                }

                await _logger.LogEventAsync($"Начало поиска артикулов по тексту: '{searchText}' в таблицах WDtoBind и unboundArts", "SearchArticulesByText");

                // Запрос для gridView_wdToBind (модель MyDataANN)
                string queryWdToBind = @"SELECT ann.annId as AnnID, ann.kod as Kod, ann.articul as Articul, 
                                       ann.status as Status, ann.grup, ann.mod, ann.size_label, ann.data_obn as dateUpdate
                                       FROM art_norm_n ann 
                                       WHERE ann.annId IN (
                                           SELECT sa.annId 
                                           FROM sp_articul sa 
                                           WHERE sa.articul LIKE @searchPattern)
                                       ORDER BY ann.annId DESC";

                // Запрос для gridView_unboundArts (модель MyDataART)
                string queryUnboundArts = @"SELECT * FROM articulListGroupBySizeLabel 
                                          WHERE  (annId is null or annId = 0) and
                                          articul LIKE @searchPattern 
                                          ORDER BY articul, row_num";

                var parameters = new Dictionary<string, object>
                {
                    { "@searchPattern", $"%{searchText}%" }
                };

                // Выполняем оба запроса параллельно
                var wdToBindTask = _dbService.GetListAsync<MyDataANN>(queryWdToBind, parameters);
                var unboundArtsTask = _dbService.GetListAsync<MyDataART>(queryUnboundArts, parameters);

                await Task.WhenAll(wdToBindTask, unboundArtsTask);

                var wdToBindResults = wdToBindTask.Result;
                var unboundArtsResults = unboundArtsTask.Result;
                // Обновляем данные в gridView_unboundArts
                if (unboundArtsResults != null && unboundArtsResults.Any())
                {
                    _myDataArtList.Clear();
                    _myDataArtList.BulkLoad(unboundArtsResults);

                    // Устанавливаем фокус на первую строку в gridView_unboundArts
                    if (gridView_unboundArts.DataRowCount > 0)
                    {
                        gridView_unboundArts.FocusedRowHandle = 0;
                    }
                }
                else
                {
                    _myDataArtList.Clear();
                }


                // Обновляем данные в gridView_wdToBind
                if (wdToBindResults != null && wdToBindResults.Any())
                {
                    // Заполняем текстовый статус для каждой записи
                    foreach (var item in wdToBindResults)
                    {
                        item.Stat = StatusHelper.GetStatusText(item.Status);
                    }

                    _myDataAnnList.Clear();
                    _myDataAnnList.BulkLoad(wdToBindResults);

                    // Устанавливаем фокус на первую строку в gridView_wdToBind
                    if (gridView_wdToBind.DataRowCount > 0)
                    {
                        gridView_wdToBind.FocusedRowHandle = 0;
                    }
                }
                else
                {
                    _myDataAnnList.Clear();
                }

                // Логируем результаты
                int totalResults = (wdToBindResults?.Count ?? 0) + (unboundArtsResults?.Count ?? 0);
                if (totalResults > 0)
                {
                    await _logger.LogEventAsync($"Найдено артикулов по запросу '{searchText}': WDtoBind={wdToBindResults?.Count ?? 0}, unboundArts={unboundArtsResults?.Count ?? 0}", "SearchArticulesByText");
                }
                else
                {
                    await _logger.LogEventAsync($"По запросу '{searchText}' артикулы не найдены", "SearchArticulesByText");
                    MessageBox.Show($"По запросу '{searchText}' артикулы не найдены", "Поиск", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при поиске артикулов по тексту: '{searchControl1.Text}'");
                MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            await SewingProduction.Features.TeamWork.Helpers.GridOverlayLoader.LoadListAsync(
                GridControlBindedArts,
                _nzpListWd,
                (System.Windows.Forms.BindingSource)_nzpByKoddRtSourceWd,
                async token => annId > 0 ? await _artNormService.GetNzpWithPztCounts(annId, token) : new List<NZPByKoddRt>(),
                ct);
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
                    await _logger.LogWarningAsync("Попытка редактирования без выбора записи", "ButtonEditWd_Click_Internal");
                    return;
                }
                var selectedItem = ANNgridView.GetRow(_rowNumber) as ArtNormN;
                if (selectedItem == null) return;

                int selectedAnnId = selectedItem.AnnID;

                // Используем новый метод для открытия формы не в модальном режиме
                var teamWorkAdvanceTW = OpenAdvanceFormNonModal(bufferId, (int)Mode.Edit, oldId: selectedAnnId);

                if (teamWorkAdvanceTW == null)
                {
                    // Форма уже открыта или произошла ошибка
                    return;
                }

                // Подписываемся на событие закрытия формы для обработки результата
                teamWorkAdvanceTW.FormClosed += async (s, args) =>
                {
                    if (teamWorkAdvanceTW.DialogResult == DialogResult.OK)
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

                        // Запускаем асинхронное обновление секунд для отредактированной записи
                        _ = Task.Run(async () =>
                        {
                            await _secondsUpdateManager.StartSecondsUpdateAsync(selectedAnnId, ANNgridView, _bindingList, ShowSecondsUpdateStatus);
                            // Очищаем статус через 3 секунды после завершения
                            await Task.Delay(3000);
                            ClearSecondsUpdateStatus();
                        });

                        // Отображаем сообщение об успешном редактировании
                        //MessageBox.Show("Запись успешно отредактирована.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await _logger.LogEventAsync("Запись успешно отредактирована.", "ButtonEditWD");
                    }
                };
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при редактировании записи");
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async Task EditWd_Internal2(GridView gridView, IList list, BindingSource bindingSource, bool forMyDataAnnView = false, bool Editing = false)
        {
            try
            {
                int rowNumber = gridView.FocusedRowHandle;
                if (rowNumber < 0)
                {
                    MessageBox.Show("Выберите запись для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    await _logger.LogWarningAsync("Попытка редактирования без выбора записи", "EditWd_Internal2");
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
                    selectedArtNormN = selectedAnn.CloneProperties();//.CloneOperationalData();//
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

                //Проверяем статус "актуальный" и наличие даты обновления
                if (selectedArtNormN.Status == (int)Status.Actual && selectedArtNormN.dateUpdate.HasValue)
                    if (!Editing) //если нельзя редактировать без проверки
                    {
                        MessageBox.Show(
                            "Редактирование недоступно.\nЗапись имеет статус 'Актуальный' и уже была обновлена.",
                            "Ограничение редактирования",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        await _logger.LogWarningAsync("Попытка редактирования записи со статусом 'Актуальный' и датой обновления", "EditWd_Internal2");
                        return;
                    }
                else if (Editing)
                    {
                        List<Brig> brigades = await _artNormService.GetWorkingBrigs(annId);
                        var brigIds = brigades?
       .Select(b => b.id_brig )
       .Where(id => id > 0)
       .Distinct()
       .ToArray();

                        if (brigIds is { Length: > 0 })
                        {
                            await _jabberSender.SendToBrigsAsync(brigIds, "проверьте РТ"); 
                            await _logger.LogEventAsync(
                                $"Отправлено '{"проверьте РТ"}' в {brigIds.Length} бригад(ы) для annId={annId}",
                                "EditWd_Internal2");
                        }
                        else
                        {
                            await _logger.LogEventAsync(
                                $"Бригад для рассылки не найдено (annId={annId})",
                                "EditWd_Internal2");
                        }
                    }
                var updatedArtNormN = new ArtNormN();

                // Используем новый метод для открытия формы не в модальном режиме
                var teamWorkAdvanceTW = OpenAdvanceFormNonModal(bufferId, (int)Mode.Edit, oldId: annId);

                if (teamWorkAdvanceTW == null)
                {
                    // Форма уже открыта или произошла ошибка
                    return;
                }

                // Подписываемся на событие закрытия формы для обработки результата
                teamWorkAdvanceTW.FormClosed += async (s, args) =>
                {
                    if (teamWorkAdvanceTW.DialogResult == DialogResult.OK)
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
                        // 4. НАХОДИМ И ЗАМЕНЯЕМ СТАРЫЙ ОБЪЕКТ В ИСТОЧНИКЕ ДАННЫХ
                        // Находим индекс старой записи в списке _bindingList
                        int i = _bindingList.IndexOf(_bindingList.FirstOrDefault(x => x.AnnID == updatedArtNormN.AnnID));
                        if (i >= 0)
                        {
                            // Заменяем старый объект на новый. Это гарантирует, что все поля
                            // будут обновлены в источнике данных.
                            _bindingList[i] = updatedArtNormN;
                        }
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
                            await RefreshNormRaskForArticlesTab(updatedArtNormN.AnnID, CancellationToken.None);

                        }
                        else if (!forMyDataAnnView && updatedArtNormN != null && updatedArtNormN.AnnID > 0) // Иначе, если для первой вкладки
                        {
                            await LoadRelatedData(updatedArtNormN.AnnID); // Загружаем связанные данные для первой вкладки 
                        }

                        // Запускаем асинхронное обновление секунд для отредактированной записи
                        if (updatedArtNormN != null && updatedArtNormN.AnnID > 0)
                        {
                            _ = Task.Run(async () =>
                            {
                                await _secondsUpdateManager.StartSecondsUpdateAsync(updatedArtNormN.AnnID, gridView,
                                    forMyDataAnnView ? null : _bindingList, ShowSecondsUpdateStatus);
                                // Очищаем статус через 3 секунды после завершения
                                await Task.Delay(3000);
                                ClearSecondsUpdateStatus();
                            });
                        }
                    }
                };
            }

            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при редактировании записи");
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private async void simpleButton2_Click_Internal(object sender, EventArgs e)
        {
            ArtNormN newItemShell = null; // Объявляем здесь, чтобы был доступен в catch и finally (если нужно)
            int newAnnId = 0;

            try
            {
                // Проверяем валидность gridView_unboundArts
                if (!IsUnboundArtsGridValid())
                {
                    MessageBox.Show("Пожалуйста, выберите запись из таблицы артикулов.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    await _logger.LogWarningAsync("GridView_unboundArts не содержит валидных данных", "simpleButton2_Click_Internal");
                    return;
                }

                int focusedRowHandle = gridView_unboundArts.FocusedRowHandle;
                var selectedArtData = GetSafeUnboundArtData(gridView_unboundArts, focusedRowHandle);

                if (selectedArtData == null)
                {
                    MessageBox.Show("Не удалось получить данные выбранного артикула.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await _logger.LogWarningAsync($"GetSafeUnboundArtData вернул null для строки: {focusedRowHandle}", "simpleButton2_Click_Internal");
                    return;
                }

                // 1. Создаем "оболочку" ArtNormN
                newItemShell = new ArtNormN
                {
                    // Основные поля будут заполнены в TeamWork_AdvanceTW из InitialArtData
                    // Здесь устанавливаем только необходимые для вставки и начального отображения значения
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
                    await _logger.LogErrorAsync("", "Ошибка при вставке новой ArtNormN (AnnID <= 0) Кнопка Создать из артикула");
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

                // 4. Открываем форму TeamWork_AdvanceTW (немодально)
                var teamWorkAdvanceTW = OpenAdvanceFormNonModal(
                    0,
                    (int)Mode.NewWorkDivision,
                    newId: newAnnId);
                if (teamWorkAdvanceTW == null)
                {
                    return;
                }
                teamWorkAdvanceTW.InitialArtData = selectedArtData; // Передаем данные из MyDataART

                // Подписываемся на событие закрытия формы для обработки результата
                teamWorkAdvanceTW.FormClosed += async (s, args) =>
                {
                    // 5. Обрабатываем результат диалога
                    if (teamWorkAdvanceTW.DialogResult == DialogResult.OK)
                    {
                        var createdOrUpdatedAnn = teamWorkAdvanceTW.CreatedAnn;
                        if (createdOrUpdatedAnn != null)
                        {
                            // Находим и обновляем элемент в _bindingList
                            var itemInList = _bindingList?.FirstOrDefault(ann => ann.AnnID == newAnnId);
                            if (itemInList != null)
                            {
                                // Копируем свойства из возвращенного объекта в объект в списке
                                itemInList.CopyPropertiesFrom(createdOrUpdatedAnn);
                                itemInList.StatusText = StatusHelper.GetStatusText(itemInList.Status);
                            }

                            _myDataAnnBindingSource.ResetBindings(false);
                            int finalRowHandle = gridView_wdToBind.LocateByValue("AnnID", newAnnId);
                            if (finalRowHandle != GridControl.InvalidRowHandle)
                            {
                                gridView_wdToBind.RefreshRow(finalRowHandle);
                                gridView_wdToBind.FocusedRowHandle = finalRowHandle;
                                gridView_wdToBind.MakeRowVisible(finalRowHandle);
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
                                    ANNgridView.MakeRowVisible(annRowHandle);
                                    ANNgridView.RefreshRow(annRowHandle);
                                }
                                finally
                                {
                                    ANNgridView.EndUpdate();
                                }
                            }

                            await _logger.LogEventAsync($"Запись ANN (ID: {newAnnId}) успешно создана/обновлена из артикула.", "simpleButton2_Click_Internal");

                            _ = Task.Run(async () =>
                            {
                                await _secondsUpdateManager.StartSecondsUpdateAsync(newAnnId, ANNgridView, _bindingList, ShowSecondsUpdateStatus);
                                await Task.Delay(3000);
                                ClearSecondsUpdateStatus();
                            });

                            //MessageBox.Show("Новая предварительная запись успешно создана/обновлена.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await _logger.LogEventAsync("Новая предварительная запись успешно создана/обновлена.", "simpleButton2_Click_Internal");
                        }
                    }
                    else
                    {
                        await _logger.LogEventAsync($"Создание записи ANN (ID: {newAnnId}) отменено пользователем в TeamWork_AdvanceTW.", "simpleButton2_Click_Internal");
                        if (newItemShell != null && _bindingList != null && _bindingList.Contains(newItemShell))
                        {
                            _bindingList.Remove(newItemShell);
                        }
                        _bindingSource?.ResetBindings(false);
                        ANNgridControl?.RefreshDataSource();

                        await _artNormService.DeleteByAnnId(TableNames.Ann, newAnnId);
                        if (teamWorkAdvanceTW.IsRaszInserted)
                            await _artNormService.DeleteByAnnId(TableNames.Rasz, newAnnId);
                        if (teamWorkAdvanceTW.IsRaskInserted)
                            await _artNormService.DeleteByAnnId(TableNames.Rask, newAnnId);
                        if (teamWorkAdvanceTW.IsKontInserted)
                            await _artNormService.DeleteByAnnId(TableNames.Kont, newAnnId);
                    }
                };
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

        #region Administration
        /// <summary>
        /// Помечает выбранные разделения труда на удаление, устанавливая annDateDel и annCompDel
        /// </summary>
        private async Task MarkWorkDivisionForDeletion_Internal(object sender, EventArgs e)
        {
            try
            {
                // Проверяем, что грид инициализирован
                if (ANNgridView == null)
                {
                    MessageBox.Show("Грид не инициализирован.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Получаем выбранные строки
                var selectedRowHandles = ANNgridView.GetSelectedRows();

                // Если нет выбранных строк, берем текущую строку
                if (selectedRowHandles == null || selectedRowHandles.Length == 0)
                {
                    if (ANNgridView.FocusedRowHandle < 0)
                    {
                        MessageBox.Show("Выберите записи для пометки на удаление.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    selectedRowHandles = new int[] { ANNgridView.FocusedRowHandle };
                }

                var selectedItems = new List<ArtNormN>();
                var alreadyMarkedItems = new List<ArtNormN>();

                // Собираем данные выбранных строк
                foreach (int rowHandle in selectedRowHandles)
                {
                    if (rowHandle >= 0)
                    {
                        var item = ANNgridView.GetRow(rowHandle) as ArtNormN;
                        if (item != null)
                        {
                            if (item.dateDel.HasValue)
                            {
                                alreadyMarkedItems.Add(item);
                            }
                            else
                            {
                                selectedItems.Add(item);
                            }
                        }
                    }
                }

                // Сообщаем о записях, которые уже помечены на удаление
                if (alreadyMarkedItems.Count > 0)
                {
                    string alreadyMarkedMessage = alreadyMarkedItems.Count == 1
                        ? $"Запись уже помечена на удаление:\n{alreadyMarkedItems[0].Articul} - {alreadyMarkedItems[0].Mod}\nДата: {alreadyMarkedItems[0].dateDel.Value:dd.MM.yyyy HH:mm:ss}"
                        : $"{alreadyMarkedItems.Count} записей уже помечены на удаление.";

                    if (selectedItems.Count == 0)
                    {
                        MessageBox.Show(alreadyMarkedMessage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        alreadyMarkedMessage += "\n\nОни будут пропущены.";
                        MessageBox.Show(alreadyMarkedMessage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                if (selectedItems.Count == 0)
                {
                    MessageBox.Show("Нет записей для пометки на удаление.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Подтверждение операции
                DateTime currentDate = DateTime.Now;
                string computerName = Environment.MachineName;

                string message = selectedItems.Count == 1
                    ? $"Пометить разделение труда на удаление:\n\n" +
                      $"AnnID: {selectedItems[0].AnnID}\n" +
                      $"Группа: {selectedItems[0].grup?.TrimEnd(' ')}, " +
                      $"Модель: {selectedItems[0].Mod?.TrimEnd(' ')}, " +
                      $"Артикул: {selectedItems[0].Articul?.TrimEnd(' ')}\n\n" +
                      $"Будут установлены:\n" +
                      $"• annDateDel = {currentDate:dd.MM.yyyy HH:mm:ss}\n" +
                      $"• annCompDel = {computerName}\n\n" +
                      $"Продолжить?"
                    : $"Пометить {selectedItems.Count} разделений труда на удаление?\n\n" +
                      $"Будут установлены:\n" +
                      $"• annDateDel = {currentDate:dd.MM.yyyy HH:mm:ss}\n" +
                      $"• annCompDel = {computerName}\n\n" +
                      $"Продолжить?";

                var result = MessageBox.Show(
                    message,
                    "Подтверждение пометки на удаление",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (result != DialogResult.Yes)
                    return;

                // Обрабатываем каждую запись
                int successCount = 0;
                var errors = new List<string>();

                ANNgridView.BeginUpdate();
                try
                {
                    foreach (var item in selectedItems)
                    {
                        try
                        {
                            // SQL запрос для пометки на удаление
                            string sqlQuery = "UPDATE art_norm_n SET annDateDel = @currentDate, annCompDel = @computerName WHERE annID = @annID";

                            var parameters = new Dictionary<string, object>
                            {
                                { "@annID", item.AnnID },
                                { "@currentDate", currentDate },
                                { "@computerName", computerName }
                            };

                            // Выполняем обновление
                            int affectedRows = await _dbHelper.ExecuteNonQueryWithRowCountAsync(sqlQuery, parameters);

                            if (affectedRows > 0)
                            {
                                // Обновляем объект в памяти
                                item.dateDel = currentDate;
                                item.compDel = computerName;

                                // Обновляем строку в гриде
                                int rowHandle = ANNgridView.LocateByValue("AnnID", item.AnnID);
                                if (rowHandle >= 0)
                                {
                                    ANNgridView.RefreshRow(rowHandle);
                                }

                                successCount++;
                                await _logger.LogEventAsync($"РТ помечено на удаление. AnnID: {item.AnnID}, Группа: {item.grup?.TrimEnd(' ')}, Модель: {item.Mod?.TrimEnd(' ')}, Артикул: {item.Articul?.TrimEnd(' ')}, Дата: {currentDate:dd.MM.yyyy HH:mm:ss}, Компьютер: {computerName}", "MarkForDeletion");
                            }
                            else
                            {
                                string errorMsg = $"AnnID: {item.AnnID} - не удалось обновить в БД";
                                errors.Add(errorMsg);
                                await _logger.LogErrorAsync(new Exception("affectedRows = 0"), $"Не удалось пометить РТ на удаление. AnnID: {item.AnnID}");
                            }
                        }
                        catch (Exception ex)
                        {
                            string errorMsg = $"AnnID: {item.AnnID} - {ex.Message}";
                            errors.Add(errorMsg);
                            await _logger.LogErrorAsync(ex, $"Ошибка при пометке РТ на удаление. AnnID: {item.AnnID}");
                        }
                    }
                }
                finally
                {
                    ANNgridView.EndUpdate();
                }

                // Обновляем привязку данных
                _bindingSource.ResetBindings(false);

                // Показываем результат операции
                if (errors.Count == 0)
                {
                    string successMessage = successCount == 1
                        ? $"Разделение труда успешно помечено на удаление.\n\nДата удаления: {currentDate:dd.MM.yyyy HH:mm:ss}\nКомпьютер: {computerName}"
                        : $"Успешно помечено на удаление {successCount} разделений труда.\n\nДата удаления: {currentDate:dd.MM.yyyy HH:mm:ss}\nКомпьютер: {computerName}";

                    MessageBox.Show(successMessage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string errorMessage = $"Успешно обработано: {successCount}\nОшибок: {errors.Count}\n\nОшибки:\n" + string.Join("\n", errors.Take(5));
                    if (errors.Count > 5)
                        errorMessage += $"\n... и еще {errors.Count - 5} ошибок";

                    MessageBox.Show(errorMessage, "Результат операции", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при пометке разделений труда на удаление: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await _logger.LogErrorAsync(ex, "Ошибка в MarkWorkDivisionForDeletion_Internal");
            }
        }

        /// <summary>
        /// Устанавливает дату обновления (data_obn) сегодняшним числом для выбранных строк
        /// Работает с двумя вкладками: "Разделения труда" (ANNgridView) и "Текущие работы" (gridView_wdToBind)
        /// Использует процедуру updateSebZArticulPsz для обновления данных во всех справочниках
        /// </summary>
        private async Task SetUpdateDate_Internal(object sender, EventArgs e)
        {
            try
            {
                // Определяем, какая вкладка активна и какой грид использовать
                GridView activeGridView = null;
                string gridType = "";
                BindingSource activeBindingSource = null;

                // Проверяем, какая вкладка активна
                if (xtraTabControl1.SelectedTabPage?.Name == "xtraTabPageArticles")
                {
                    // Вкладка "Текущие работы" - используем gridView_wdToBind
                    if (gridView_wdToBind == null)
                    {
                        MessageBox.Show("Грид текущих работ не инициализирован.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        await _logger.LogWarningAsync("Грид текущих работ не инициализирован", "SetUpdateDate_Internal");
                        return;
                    }
                    activeGridView = gridView_wdToBind;
                    gridType = "текущих работ";
                    activeBindingSource = _myDataAnnBindingSource;
                }
                else
                {
                    // Вкладка "Разделения труда" - используем ANNgridView
                    if (ANNgridView == null)
                    {
                        MessageBox.Show("Грид разделений труда не инициализирован.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        await _logger.LogWarningAsync("Грид разделений труда не инициализирован", "SetUpdateDate_Internal");
                        return;
                    }
                    activeGridView = ANNgridView;
                    gridType = "разделений труда";
                    activeBindingSource = _bindingSource;
                }

                // Получаем выбранные строки
                var selectedRowHandles = activeGridView.GetSelectedRows();

                // Если нет выбранных строк, берем текущую строку
                if (selectedRowHandles == null || selectedRowHandles.Length == 0)
                {
                    if (activeGridView.FocusedRowHandle < 0)
                    {
                        MessageBox.Show($"Выберите записи для обновления даты на вкладке \"{gridType}\".", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        await _logger.LogWarningAsync($"Попытка обновления даты без выбора строк на вкладке \"{gridType}\"", "SetUpdateDate_Internal");
                        return;
                    }
                    selectedRowHandles = new int[] { activeGridView.FocusedRowHandle };
                }

                var selectedItems = new List<object>();
                var selectedAnnIds = new List<int>();

                // Собираем данные выбранных строк в зависимости от типа грида
                foreach (int rowHandle in selectedRowHandles)
                {
                    if (rowHandle >= 0)
                    {
                        var item = activeGridView.GetRow(rowHandle);
                        if (item != null)
                        {
                            selectedItems.Add(item);

                            // Получаем AnnID в зависимости от типа объекта
                            if (item is ArtNormN artNorm)
                            {
                                selectedAnnIds.Add(artNorm.AnnID);
                            }
                            else if (item is MyDataANN myDataAnn)
                            {
                                selectedAnnIds.Add(myDataAnn.AnnID);
                            }
                        }
                    }
                }

                if (selectedItems.Count == 0)
                {
                    MessageBox.Show($"Не найдено записей для обновления даты на вкладке \"{gridType}\".", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    await _logger.LogWarningAsync($"Не найдено записей для обновления даты после сбора выбранных строк на вкладке \"{gridType}\"", "SetUpdateDate_Internal");
                    return;
                }

                // Подтверждение операции
                string message = selectedItems.Count == 1
                    ? $"Обновить данные во всех справочниках для записи на вкладке \"{gridType}\"?"
                    : $"Обновить данные во всех справочниках для {selectedItems.Count} записей на вкладке \"{gridType}\"?";

                var result = MessageBox.Show(
                    message,
                    "Пересчёт себ.",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (result != DialogResult.Yes)
                    return;

                // Текущая дата для установки
                DateTime currentDate = DateTime.Now;

                // Обновляем дату для каждой выбранной записи
                int successCount = 0;
                var errors = new List<string>();

                activeGridView.BeginUpdate();
                try
                {
                    foreach (int annId in selectedAnnIds)
                    {
                        try
                        {
                            // Используем общий метод для обновления даты и статуса
                            int rowHandle = activeGridView.LocateByValue("AnnID", annId);
                            bool success = await UpdateDateAndStatusAsync(annId, activeGridView, rowHandle);

                            if (success)
                            {
                                // Обновляем объект в памяти в зависимости от типа
                                foreach (var item in selectedItems)
                                {
                                    if (item is ArtNormN artNorm && artNorm.AnnID == annId)
                                    {
                                        artNorm.dateUpdate = currentDate;
                                        artNorm.Status = (int)Status.Actual;
                                        artNorm.StatusText = "Актуальное";
                                        break;
                                    }
                                    else if (item is MyDataANN myDataAnn && myDataAnn.AnnID == annId)
                                    {
                                        // MyDataANN может не иметь поля dateUpdate, но мы обновляем в БД
                                        break;
                                    }
                                }

                                successCount++;
                            }
                            else
                            {
                                string errorMsg = $"AnnID: {annId} - не удалось обновить данные";
                                errors.Add(errorMsg);
                            }
                        }
                        catch (Exception ex)
                        {
                            string errorMsg = $"AnnID: {annId} - {ex.Message}";
                            errors.Add(errorMsg);
                            await _logger.LogErrorAsync(ex, $"Ошибка при обновлении данных для записи AnnID: {annId}");
                        }
                    }
                }
                finally
                {
                    activeGridView.EndUpdate();
                }

                // Обновляем привязку данных
                if (activeBindingSource != null)
                {
                    activeBindingSource.ResetBindings(false);
                }

                // Показываем результат операции
                if (errors.Count == 0)
                {
                    string successMessage = successCount == 1
                        ? $"Данные успешно обновлены для записи. Дата: {currentDate:dd.MM.yyyy}, статус: Актуальное"
                        : $"Данные обновлены для {successCount} записей. Дата: {currentDate:dd.MM.yyyy}, статус: Актуальное";

                    //  MessageBox.Show(successMessage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await _logger.LogEventAsync(successMessage, "SetUpdateDate_Internal");
                }
                else
                {
                    string errorMessage = $"Обновлено: {successCount} записей.\nОшибок: {errors.Count}\n\nОшибки:\n" + string.Join("\n", errors.Take(5));
                    if (errors.Count > 5)
                        errorMessage += $"\n... и еще {errors.Count - 5} ошибок";

                    MessageBox.Show(errorMessage, "Результат обновления", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    await _logger.LogWarningAsync(errorMessage, "SetUpdateDate_Internal");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при выполнении обновления данных");
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Обновляет поле arch в таблице sp_articul для записей, соответствующих условиям
        /// </summary>
        private async Task UpdateSpArticulArch_Internal(object sender, EventArgs e)
        {
            try
            {
                // Проверяем, что gриды инициализированы
                if (ANNgridView == null || gridViewBindedArts == null)
                {
                    MessageBox.Show("Грид не инициализирован.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await _logger.LogErrorAsync(new Exception("Грид не инициализирован"), "UpdateSpArticulArch_Internal");
                    return;
                }

                // Получаем AnnID из выбранной строки в ANNgridView
                if (ANNgridView.FocusedRowHandle < 0)
                {
                    MessageBox.Show("Выберите запись в основном гриде (ANNgridView).", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    await _logger.LogWarningAsync("Попытка обновления arch без выбора строки в ANNgridView", "UpdateSpArticulArch_Internal");
                    return;
                }

                var selectedAnn = ANNgridView.GetRow(ANNgridView.FocusedRowHandle) as ArtNormN;
                if (selectedAnn == null)
                {
                    MessageBox.Show("Не удалось получить данные выбранной записи в основном гриде.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await _logger.LogErrorAsync(new Exception("Не удалось получить данные выбранной записи в ANNgridView"), "UpdateSpArticulArch_Internal");
                    return;
                }

                int annId = selectedAnn.AnnID;

                // Получаем артикул из выбранной строки в gridView5
                if (gridViewBindedArts.FocusedRowHandle < 0)
                {
                    MessageBox.Show("Выберите запись в гриде НЗП.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    await _logger.LogWarningAsync("Попытка обновления arch без выбора строки в gridView5", "UpdateSpArticulArch_Internal");
                    return;
                }

                var selectedNzp = gridViewBindedArts.GetRow(gridViewBindedArts.FocusedRowHandle) as NZPByKoddRt;
                if (selectedNzp == null)
                {
                    MessageBox.Show("Не удалось получить данные выбранной записи в гриде НЗП.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await _logger.LogErrorAsync(new Exception("Не удалось получить данные выбранной записи в гриде НЗП"), "UpdateSpArticulArch_Internal");
                    return;
                }

                string articul = selectedNzp.articul.TrimEnd(' ');
                string kod = selectedNzp.kodd.ToString();
                if (string.IsNullOrEmpty(articul))
                {
                    MessageBox.Show("Артикул в выбранной записи НЗП пустой.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await _logger.LogErrorAsync(new Exception("Артикул в выбранной записи НЗП пустой"), "UpdateSpArticulArch_Internal");
                    return;
                }

                // Подтверждение операции
                string message = $"Обновить поле 'arch' в таблице sp_articul для:\n" +
                                $"AnnID: {annId}\n" +
                                $"Артикул: {articul}\n\n" +
                                $"Это затронет записи, где:\n" +
                                $"- sa.annid = ann.annID\n" +
                                $"- left(sa.kod, 7) = {kod}\n" +
                                $"- sa.articul = '{articul}'\n" +
                                $"- ann.annID = {annId}";

                var result = MessageBox.Show(
                    message,
                    "Подтверждение обновления arch",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (result != DialogResult.Yes)
                    return;
                await _logger.LogEventAsync($"Пользователь подтвердил обновление arch для AnnID: {annId}, Артикул: {articul}", "UpdateSpArticulArch_Internal");
                // SQL запрос для обновления поля arch
                //string sqlQuery = @"
                //    UPDATE sp_articul sa 
                //    SET sa.arh = 1
                //    WHERE left(sa.kod, 7) = @kod 
                //    AND ann.articul = @art  
                //    AND ann.annID = @annId";
                string sqlQuery = @"UPDATE sa
                    SET sa.arh = 1
                    FROM dbo.sp_articul AS sa
                    JOIN dbo.art_norm_n AS ann ON ann.AnnID = sa.AnnID
                    WHERE left(sa.kod, 7) = @kod 
                    AND sa.articul = @art  
                    AND ann.annID = @annId";
                var parameters = new Dictionary<string, object>
                {
                    { "@annId", annId },
                    { "@art", articul },
                    { "@kod", kod }
                };

                // Выполняем обновление
                await _dbHelper.ExecuteQueryAsync(sqlQuery, parameters);

                // Логируем операцию
                await _logger.LogEventAsync($"Обновлено поле arch для записей AnnID: {annId}, Артикул: {articul}", "UpdateSpArticulArch");

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при обновлении поля arch в sp_articul");
                MessageBox.Show($"Ошибка при обновлении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Устанавливает архивный статус для выбранных строк в ANNgridView
        /// </summary>
        private async Task SetArchiveStatus_Internal(object sender, EventArgs e)
        {
            try
            {
                if (ANNgridView == null)
                {
                    MessageBox.Show("Грид не инициализирован.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await _logger.LogErrorAsync(new Exception("Грид не инициализирован"), "SetArchiveStatus_Internal");
                    return;
                }

                // Получаем выбранные строки
                var selectedRowHandles = ANNgridView.GetSelectedRows();

                // Если нет выбранных строк, берем текущую строку
                if (selectedRowHandles == null || selectedRowHandles.Length == 0)
                {
                    if (ANNgridView.FocusedRowHandle < 0)
                    {
                        MessageBox.Show("Выберите записи для архивирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        await _logger.LogWarningAsync("Попытка архивирования без выбора строк", "SetArchiveStatus_Internal");
                        return;
                    }
                    selectedRowHandles = new int[] { ANNgridView.FocusedRowHandle };
                }

                var selectedItems = new List<ArtNormN>();

                // Собираем данные выбранных строк
                foreach (int rowHandle in selectedRowHandles)
                {
                    if (rowHandle >= 0)
                    {
                        var item = ANNgridView.GetRow(rowHandle) as ArtNormN;
                        if (item != null)
                        {
                            selectedItems.Add(item);
                        }
                    }
                }

                if (selectedItems.Count == 0)
                {
                    MessageBox.Show("Не найдено записей для архивирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    await _logger.LogWarningAsync("Не найдено записей для архивирования после сбора выбранных строк", "SetArchiveStatus_Internal");
                    return;
                }

                // Подтверждение операции
                string message = selectedItems.Count == 1
                    ? $"Установить архивный статус для записи:\n{selectedItems[0].Articul} - {selectedItems[0].Mod}?"
                    : $"Установить архивный статус для {selectedItems.Count} записей?";

                var result = MessageBox.Show(
                    message,
                    "Подтверждение архивирования",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);
                if (result != DialogResult.Yes)
                    return;
                await _logger.LogEventAsync($"Пользователь подтвердил архивирование {selectedItems.Count} записей", "SetArchiveStatus_Internal");

                // Обновляем статус для каждой выбранной записи
                int successCount = 0;
                var errors = new List<string>();

                ANNgridView.BeginUpdate();
                try
                {
                    foreach (var item in selectedItems)
                    {
                        try
                        {
                            // Проверяем, что запись можно архивировать
                            if (item.Status == (int)Status.Archive)
                            {
                                await _logger.LogEventAsync($"Запись AnnID: {item.AnnID} уже имеет архивный статус", "SetArchiveStatus");
                                continue;
                            }

                            // Обновляем статус в базе данных
                            await _dbService.UpdateFieldAsync(TableNames.Ann, "Status", (int)Status.Archive, TableNames.AnnId, item.AnnID);

                            // Обновляем объект в памяти
                            item.Status = (int)Status.Archive;
                            item.StatusText = StatusHelper.GetStatusText((int)Status.Archive);

                            // Обновляем строку в гриде
                            int rowHandle = ANNgridView.LocateByValue("AnnID", item.AnnID);
                            if (rowHandle >= 0)
                            {
                                ANNgridView.RefreshRow(rowHandle);
                            }

                            successCount++;
                            await _logger.LogEventAsync($"Статус записи AnnID: {item.AnnID} изменен на 'Архивное'", "SetArchiveStatus");
                        }
                        catch (Exception ex)
                        {
                            string errorMsg = $"AnnID: {item.AnnID} - {ex.Message}";
                            errors.Add(errorMsg);
                            await _logger.LogErrorAsync(ex, $"Ошибка при архивировании записи AnnID: {item.AnnID}");
                        }
                    }
                }
                finally
                {
                    ANNgridView.EndUpdate();
                }

                // Обновляем привязку данных
                _bindingSource.ResetBindings(false);

                // Показываем результат операции
                if (errors.Count == 0)
                {
                    string successMessage = successCount == 1
                        ? "Запись успешно архивирована."
                        : $"Успешно архивировано {successCount} записей.";

                    //MessageBox.Show(successMessage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await _logger.LogEventAsync(successMessage, "SetArchiveStatus_Internal");
                }
                else
                {
                    string errorMessage = $"Архивировано: {successCount} записей.\nОшибки:\n" + string.Join("\n", errors);
                    //MessageBox.Show(errorMessage, "Результат архивирования", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    await _logger.LogWarningAsync(errorMessage, "SetArchiveStatus_Internal");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex.Message, "Ошибка при выполнении архивирования записей");
                MessageBox.Show($"Ошибка при архивировании: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region Поиск и фильтрация
        private async void loadAllCheckBox_CheckedChanged_Internal(object sender, EventArgs e, bool all)
        {
            try
            {
                bool _loadAll = all;
                List<MyDataANN> list = null;
                if (_loadAll)
                {
                    list = await LoadWorksbyArt("");
                    await _logger.LogEventAsync("showAllWD: Загружены все РТ", "loadAllCheckBox_CheckedChanged");
                }
                else if (!_loadAll)
                {
                    // Безопасно получаем артикул из выбранной строки
                    string articul = "";
                    if (IsUnboundArtsGridValid())
                    {
                        var selectedData = GetSafeUnboundArtData(gridView_unboundArts, gridView_unboundArts.FocusedRowHandle);
                        if (selectedData != null && !string.IsNullOrEmpty(selectedData.Articul))
                        {
                            articul = selectedData.Articul.TrimEnd(' ');
                        }
                        else
                        {
                            await _logger.LogWarningAsync("Не удалось получить артикул из выбранной строки, используем пустую строку", "loadAllCheckBox_CheckedChanged");
                        }
                    }
                    else
                    {
                        await _logger.LogWarningAsync("GridView_unboundArts не содержит валидных данных, используем пустую строку", "loadAllCheckBox_CheckedChanged");
                    }

                    list = await LoadWorksbyArt(articul);
                    await _logger.LogEventAsync($"showAllWD: Загружены РТ для артикула '{articul}'", "loadAllCheckBox_CheckedChanged");
                }
                // gridControl_wdToBind.DataSource = list;//loadAllCheckBox.Checked ? LoadWorksbyArt(0, "") : LoadWorksbyArt(kod, articul);
                //var bindingList = new BindingList<MyDataANN>(list);
                //_myDataAnnBindingSource = new BindingSource(bindingList, null);
                //gridControl_wdToBind.DataSource = _myDataAnnBindingSource;
                // Обновляем данные в UI
                _myDataAnnList.Clear();
                if (list != null)
                {
                    // Заполняем текстовый статус для каждой записи
                    foreach (var item in list)
                    {
                        item.Stat = StatusHelper.GetStatusText(item.Status);
                    }

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
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка в loadAllCheckBox_CheckedChanged_Internal");
                // В случае ошибки очищаем данные
                _myDataAnnList.Clear();
                _myDataAnnBindingSource.ResetBindings(false);
                gridView_wdToBind.RefreshData();
            }
        }


        /// <summary>
        /// Переключение фильтров при изменении чекбоксов
        /// </summary>
        private void Filter_CheckedChanged_Internal(object sender, EventArgs e) => filterTable();

        #endregion

        #region Arch
        /// <summary>
        /// Обработка нажатия HeaderButtons в группе Предварительный Архив
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutControlGroupPreArch_CustomButtonClick(object sender, BaseButtonEventArgs e)
        {
            int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);
            switch (buttonIndex)
            {
                case 0:
                    {
                        Arch(sender, e);
                        break;
                    }
            }
        }
        #endregion

        #region Print
        private void printButtonPlus_Click(object sender, EventArgs e)
        {
            int rowNumber = ANNgridView.FocusedRowHandle;

            NormRaszForEconomist report1 = new NormRaszForEconomist();
            //report1.RequestParameters = false;
            var selectedAnn = ANNgridView.GetRow(rowNumber) as ArtNormN;

            report1.Parameters["_annId"].Value = selectedAnn.AnnID;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();

        }

        /// <summary>
        /// Печать технологической схемы разделения труда
        /// </summary>
        private void PrintWorkDivisionScheme_Click(object sender, EventArgs e)
        {
            int rowNumber = ANNgridView.FocusedRowHandle;

            NormRaszTest report1 = new NormRaszTest();
            //report1.RequestParameters = false;
            var selectedAnn = ANNgridView.GetRow(rowNumber) as ArtNormN;

            report1.Parameters["_annId"].Value = selectedAnn.AnnID;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        #endregion
    }
}
