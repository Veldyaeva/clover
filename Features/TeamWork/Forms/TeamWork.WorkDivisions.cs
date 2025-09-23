using Dapper;
using DevExpress.DataAccess.Native.Excel;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.Design;
using DevExpress.XtraTab;
using SewingProduction.Extensions;
using SewingProduction.Features.TeamWork.Services;
using SewingProduction.form;
using SewingProduction.Helpers;
using SewingProduction.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork
    {
        // Первая вкладка — "Разделения труда"

        /// <summary>
        /// Загрузка вкладки "Список РТ"
        /// </summary>
        /// <returns></returns>
        private async Task LoadWorkDivisions()
        {
            try
            {
                ANNgridControl.BeginUpdate();
                // Получаем данные
                var fioList = await _artNormService.GetRelDesigner();
                ArtNormN.FioSource = fioList;

                var data = await _artNormService.GetArtNormData();

                if (data == null || data.Count == 0)
                {
                    MessageBox.Show("Нет данных для загрузки.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Настраиваем отображение GridView
                ANNgridView.OptionsView.EnableAppearanceEvenRow = true;
                ANNgridView.OptionsView.EnableAppearanceOddRow = true;
                ANNgridView.OptionsView.ShowAutoFilterRow = true;
                ANNgridView.OptionsView.ShowGroupPanel = false;
                ANNgridView.OptionsView.ShowIndicator = false;
                ANNgridView.OptionsView.ShowPreview = false;


                // Сохраняем текущую позицию
                int currentPosition = _bindingSource.Position;

                // Назначаем новые данные
                _bindingList.BulkLoad(data);
                _bindingSource.DataSource = _bindingList; // если ещё не привязано

                ANNgridControl.DataSource = _bindingSource;

                // Применяем фильтры после привязки источника данных
                filterTable();

                // Устанавливаем позицию сразу после DataSource
                _bindingSource.Position = currentPosition < _bindingSource.Count ? currentPosition : 0;

                // Устанавливаем привязки после позиции
                BindTextFields();
                Task bindingsTask = InitializeBindingsAsync();

                await _logger.LogEventAsync("Данные загружены успешно", "LoadData");
                // Включаем обновление UI
				ANNgridControl.EndUpdate();
				// Загружаем связанные данные для текущей строки, а не для первой
				int targetAnnId = 0;
				try
				{
					if (ANNgridView != null && ANNgridView.FocusedRowHandle >= 0)
					{
						if (ANNgridView.GetRow(ANNgridView.FocusedRowHandle) is ArtNormN focusedRow)
							targetAnnId = focusedRow.AnnID;
					}

					if (targetAnnId == 0 && _bindingSource != null)
					{
						int pos = _bindingSource.Position;
						if (pos >= 0 && pos < _bindingList.Count)
							targetAnnId = _bindingList[pos].AnnID;
					}

					if (targetAnnId == 0 && _bindingList.Count > 0)
						targetAnnId = _bindingList[0].AnnID;
				}
				catch { }

				if (targetAnnId > 0)
					await LoadRelatedData(targetAnnId);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных в текущие работы: {ex.Message}");
            }
        }

        private void BindTextFields()
        {
            designerTextBox.DataBindings.Clear();
            designerTextBox.DataBindings.Add("Text", _bindingSource, nameof(ArtNormN.FioDiz), true, DataSourceUpdateMode.OnPropertyChanged);
            designerTextBox.DataBindings["Text"].Format += (s, e) =>
            {
                if (e.Value is FioModel fio)
                    e.Value = fio.Fio;
            };

            constructorTextBox.DataBindings.Clear();
            constructorTextBox.DataBindings.Add("Text", _bindingSource, nameof(ArtNormN.FioConstr), true, DataSourceUpdateMode.OnPropertyChanged);
            constructorTextBox.DataBindings["Text"].Format += (s, e) =>
            {
                if (e.Value is FioModel fio)
                    e.Value = fio.Fio;
            };

            commentRichTextBox.DataBindings.Clear();
            commentRichTextBox.DataBindings.Add("Text", _bindingSource, nameof(ArtNormN.Komment), false);

            RecoRichTextBox.DataBindings.Clear();
            RecoRichTextBox.DataBindings.Add("Text", _bindingSource, nameof(ArtNormN.Reco), false);

            textEditMod.DataBindings.Clear();
            textEditMod.DataBindings.Add("Text", _bindingSource, nameof(ArtNormN.Mod), true, DataSourceUpdateMode.OnPropertyChanged);
            textEditArt.DataBindings.Clear();
            textEditArt.DataBindings.Add("Text", _bindingSource, nameof(ArtNormN.Articul), true, DataSourceUpdateMode.OnPropertyChanged);
            textEditSec.DataBindings.Clear();
            textEditSec.DataBindings.Add("Text", _bindingSource, nameof(ArtNormN.Sek), true, DataSourceUpdateMode.OnPropertyChanged);
            textEditCreate.DataBindings.Clear();
            textEditCreate.DataBindings.Add("Text", _bindingSource, nameof(ArtNormN.dateCreate), true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private async Task InitializeBindingsAsync()
        {
            try
            {
                var normRaszTask = Task.Run(() =>
                {
                    _normRaszListTW = new BindingList<NormRasz>();
                    _normRaszBindingSourceTW = new BindingSource { DataSource = _normRaszListTW };
                });
                var normRaskTask = Task.Run(() =>
                {
                    _normRaskListTW = new BindingList<NormRask>();
                    _normRaskBindingSourceTW = new BindingSource { DataSource = _normRaskListTW };
                });
                var normKontTask = Task.Run(() =>
                {
                    _normKontListTW = new BindingList<NormKont>();
                    _normKontBindingSourceTW = new BindingSource { DataSource = _normKontListTW };
                });

                await Task.WhenAll(normRaszTask, normRaskTask, normKontTask);

                gridControlRaszTW.DataSource = _normRaszBindingSourceTW;
                gridControlRaskrTW.DataSource = _normRaskBindingSourceTW;
                gridControlKontTW.DataSource = _normKontBindingSourceTW;
                
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
        }
        /// <summary>
        /// Загружает связанные данные для указанного AnnID с использованием сервисной архитектуры
        /// </summary>
        /// <param name="annId">ID разделения труда</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        private async Task LoadRelatedData(int annId, CancellationToken cancellationToken = default)
        {
            try
            {
                if (annId <= 0)
                {
                    await _logger.LogEventAsync("LoadRelatedData: Некорректный AnnID", "LoadRelatedData");
                    return;
                }

                cancellationToken.ThrowIfCancellationRequested();

                // Используем TeamWorkService для получения данных
                var result = await _teamWorkService.RefreshRelatedDataAsync(annId);

                cancellationToken.ThrowIfCancellationRequested();

                if (result.Success)
                {
                    // Используем UIHelper для обновления UI
                    await _uiHelper.UpdateRelatedDataUIAsync(
                        result,
                        _normRaskListTW,
                        _normKontListTW,
                        _normRaszListTW,
                        gridControlRaszTW,
                        gridControlRaskrTW,
                        gridControlKontTW,
                        this);

                    cancellationToken.ThrowIfCancellationRequested();

                    // Загружаем и привязываем FIO списки
                    await LoadAndBindFioListsAsync();

                    await _logger.LogEventAsync($"Связанные данные для AnnID: {annId} успешно загружены", "LoadRelatedData");
                }
                else
                {
                    await _logger.LogErrorAsync(new Exception(result.Error), $"Ошибка при загрузке связанных данных для AnnID: {annId}");
                }
            }
            catch (OperationCanceledException)
            {
                await _logger.LogEventAsync($"Загрузка связанных данных для AnnID: {annId} отменена", "LoadRelatedData");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при загрузке связанных данных для AnnID: {annId}");
            }
        }

        /// <summary>
        /// Загружает связанные данные из normraszview (без CancellationToken, но с возможностью отмены через внешний токен)
        /// </summary>
        private async Task LoadRelatedDataFromView(int annId, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            // Загружаем данные в фоновом потоке
            var dataLoadTask = Task.Run(async () =>
            {
                ct.ThrowIfCancellationRequested();

                // Параллельная загрузка данных из БД
                Task<List<NormRask>> normRaskTask = _artNormService.GetRelatedNormRask(annId);
                Task<List<NormKont>> normKontTask = _artNormService.GetRelatedNormKont(annId);
                Task<List<NormRasz>> normRaszTask = _artNormService.GetRelatedNormRasz(annId); // Этот метод использует normraszview

                ct.ThrowIfCancellationRequested();

                var normRaskResult = await normRaskTask;
                var normKontResult = await normKontTask;
                var normRaszResult = await normRaszTask;

                ct.ThrowIfCancellationRequested();

                return new { normRaskResult, normKontResult, normRaszResult };
            }, ct);

            var data = await dataLoadTask;
            ct.ThrowIfCancellationRequested();

            // Обновление UI должно происходить в UI потоке
            if (this.InvokeRequired)
            {
                await this.InvokeAsync(() =>
                {
                    _normRaskListTW.BulkLoad(data.normRaskResult);
                    _normKontListTW.BulkLoad(data.normKontResult);
                    _normRaszListTW.BulkLoad(data.normRaszResult); // Данные из normraszview
                });
            }
            else
            {
                _normRaskListTW.BulkLoad(data.normRaskResult);
                _normKontListTW.BulkLoad(data.normKontResult);
                _normRaszListTW.BulkLoad(data.normRaszResult); // Данные из normraszview
            }

            ct.ThrowIfCancellationRequested();

            await LoadAndBindFioListsAsync();

            // Сортировка детализирующих таблиц после загрузки данных - тоже в UI потоке
            if (this.InvokeRequired)
            {
                await this.InvokeAsync(() =>
                {
                    if (gridControlRaszTW.MainView is GridView raszView) TWGridHelper.sortGridView(raszView);
                    if (gridControlRaskrTW.MainView is GridView raskrView) TWGridHelper.sortGridView(raskrView);
                    if (gridControlKontTW.MainView is GridView kontView) TWGridHelper.sortGridView(kontView);
                });
            }
            else
            {
                if (gridControlRaszTW.MainView is GridView raszView) TWGridHelper.sortGridView(raszView);
                if (gridControlRaskrTW.MainView is GridView raskrView) TWGridHelper.sortGridView(raskrView);
                if (gridControlKontTW.MainView is GridView kontView) TWGridHelper.sortGridView(kontView);
            }
        }
        private async Task UpdateUnboundButtonStatusBasedOnNZP()
        {
            try
            {
                var view = gridControlNZP?.MainView as GridView;
                NZPByKoddRt selectedRow = null;

                if (view != null && _nzpByKoddRtSourceArt != null && _nzpByKoddRtSourceArt.Count > 0)
                {
                    if (_nzpByKoddRtSourceArt.Position >= 0 && _nzpByKoddRtSourceArt.Position < _nzpByKoddRtSourceArt.Count)
                    {
                        selectedRow = _nzpByKoddRtSourceArt[_nzpByKoddRtSourceArt.Position] as NZPByKoddRt;
                    }
                    else if (view.FocusedRowHandle >= 0)
                    {
                        selectedRow = view.GetRow(view.FocusedRowHandle) as NZPByKoddRt;
                    }
                    else if (_nzpByKoddRtSourceArt.Count > 0)
                    {
                        selectedRow = _nzpByKoddRtSourceArt[0] as NZPByKoddRt;
                    }
                }

                if (selectedRow == null)
                {
                    ButtonUnboundWd.Enabled = false;
                    return;
                }

                int nzp = selectedRow.kolNZP;
                int pzt = selectedRow.PZTCount;

                ButtonUnboundWd.Enabled = (nzp <= 0 || pzt <= 0);
            }
            catch (Exception ex)
            {
                if (_logger != null)
                {
                    await _logger.LogErrorAsync(ex, "Ошибка при обновлении статуса кнопки отвязки НЗП");
                }
                ButtonUnboundWd.Enabled = false; 
            }
        }

        private async Task LoadAndBindFioListsAsync()
        {
            try
            {
                if (_cachedFioData == null)
                {
                    var fioData = await _artNormService.GetRelDesigner();
                    if (fioData != null && fioData.Count > 0)
                    {
                        _cachedFioData = new List<FioModel>(fioData);
                        await _logger.LogEventAsync("FIO загружено и закешировано", "LoadAndBindFioListsAsync");
                    }
                    else
                    {
                        await _logger.LogEventAsync("Пустой список FIO", "LoadAndBindFioListsAsync");
                        return;
                    }
                }
                // Устанавливаем общий источник для модели ArtNormN
                ArtNormN.FioSource = _cachedFioData;
                _bindingSource.DataSource = _bindingList;
                _bindingSource.ResetBindings(false);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке или привязке списка ФИО");
            }
        }



        #region Добавить предварительное
        /// <summary>
        /// Добавить предварительное
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonPreliminaryWd_Click_Internal(object sender, EventArgs e)
        {
            if (ANNgridView == null) return;

            ArtNormN newItem = new ArtNormN
            {
         //       Kod = "0000000",
                grup = "",
                Articul = "",
                Mod = "",
                SekShv = 0,
                SekVyaz3 = 0,
                SekVyaz5 = 0,
                SekVyaz6 = 0,
                SekVyaz7 = 0,
                SekVyaz10 = 0,
                SekVyaz12 = 0,
                SekVyazo = 0,
                SekVyaz = 0,
                Sek = 0,
                st = 0,
                Seb = '0',
                Komment = "",
                Reco = "",
                dateCreate = DateTime.Now,
                //dateAdd = DateTime.Now,
                Diz = 0,
                Constr = 0,
                dateUpdate = null,//DateTime.MinValue,
                SekKr = 0,
                Slogn = 0,
                Status = 1,
                StatusText = StatusHelper.GetStatusText(1),
                Arh = false,
                AnnID = 0
            };

            int newId = await _dbService.InsertEntityAsync(TableNames.Ann, TableNames.AnnId, newItem);
            if (newId <= 0)
            {
                MessageBox.Show("Ошибка сохранения в БД!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            newItem.AnnID = newId;
            _bindingList.Add(newItem);
            _bindingSource.ResetBindings(false);
            ANNgridControl.RefreshDataSource();

            // Если на вкладке 2 (Текущие работы)
            if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                MyDataAnnLoad();
            }

			// Открываем форму редактирования новой записи (немодально)
			var teamWork_AdvanceTW = OpenAdvanceFormNonModal(bufferId, (int)Mode.NewWorkDivision, newId: newId);
			if (teamWork_AdvanceTW == null)
			{
				return;
			}
			// Обработка результата по закрытию формы
			teamWork_AdvanceTW.FormClosed += async (s, args) =>
			{
				if (teamWork_AdvanceTW.DialogResult == DialogResult.OK)
				{
					var createdItem = teamWork_AdvanceTW.CreatedAnn;
					if (createdItem != null)
					{
						newItem.Articul = createdItem.Articul;
						newItem.Mod = createdItem.Mod;
						newItem.grup = createdItem.grup;
						newItem.Komment = createdItem.Komment;
						newItem.Reco = createdItem.Reco;
						newItem.Diz = createdItem.Diz;
						newItem.Constr = createdItem.Constr;
						newItem.Sek = createdItem.Sek;
					}

					_bindingSource.ResetBindings(false);
					int newRowHandle = ANNgridView.LocateByValue("AnnID", newItem.AnnID);
					if (newRowHandle >= 0)
					{
						ANNgridView.BeginUpdate();
						try
						{
							ANNgridView.FocusedRowHandle = newRowHandle;
							ANNgridView.MakeRowVisible(newRowHandle);
							ANNgridView.RefreshRow(newRowHandle);
						}
						finally
						{
							ANNgridView.EndUpdate();
						}
					}

					_ = Task.Run(async () =>
					{
						await _secondsUpdateManager.StartSecondsUpdateAsync(newItem.AnnID, ANNgridView, _bindingList, ShowSecondsUpdateStatus);
						await Task.Delay(3000);
						ClearSecondsUpdateStatus();
					});
				}
				else
				{
					_bindingList.Remove(newItem);
					_bindingSource.Remove(newItem);
					await _artNormService.DeleteByAnnId(TableNames.Ann, newItem.AnnID);
					if (teamWork_AdvanceTW.IsRaszInserted)
						await _artNormService.DeleteByAnnId(TableNames.Rasz, newItem.AnnID);
					if (teamWork_AdvanceTW.IsRaskInserted)
						await _artNormService.DeleteByAnnId(TableNames.Rask, newItem.AnnID);
					if (teamWork_AdvanceTW.IsKontInserted)
						await _artNormService.DeleteByAnnId(TableNames.Kont, newItem.AnnID);

					_bindingSource.ResetBindings(false);
					ANNgridControl.RefreshDataSource();
					ANNgridView.RefreshData();
				}
			};
        }

        private async Task HandleAnnEditResult(TeamWork_AdvanceTW teamWorkForm, ArtNormN newItem)
        {
            if (teamWorkForm.ShowDialog() == DialogResult.OK)
            {
                var createdItem = teamWorkForm.CreatedAnn;

                if (createdItem != null)
                {
                    // Обновляем существующий объект
                    newItem.Articul = createdItem.Articul;
                    newItem.Mod = createdItem.Mod;
                    newItem.grup = createdItem.grup;
                    newItem.Komment = createdItem.Komment;
                    newItem.Reco = createdItem.Reco;
                    newItem.Diz = createdItem.Diz;
                    newItem.Constr = createdItem.Constr;
                    newItem.Sek = createdItem.Sek;
                }

                _bindingSource.ResetBindings(false);
                int newRowHandle = ANNgridView.LocateByValue("AnnID", newItem.AnnID);
                if (newRowHandle >= 0)
                {
                    ANNgridView.BeginUpdate();
                    try
                    {
                        ANNgridView.FocusedRowHandle = newRowHandle;
                        ANNgridView.MakeRowVisible(newRowHandle); // Прокручиваем до строки
                        ANNgridView.RefreshRow(newRowHandle);
                    }
                    finally
                    {
                        ANNgridView.EndUpdate();
                    }
                }

                // Запускаем асинхронное обновление секунд для созданного/отредактированного РТ
                _ = Task.Run(async () =>
                {
                    await _secondsUpdateManager.StartSecondsUpdateAsync(newItem.AnnID, ANNgridView, _bindingList, ShowSecondsUpdateStatus);
                    // Очищаем статус через 3 секунды после завершения
                    await Task.Delay(3000);
                    ClearSecondsUpdateStatus();
                });
            }
            else
            {
                // Удаляем несохранённую строку
                _bindingList.Remove(newItem);
                _bindingSource.Remove(newItem);

                await _artNormService.DeleteByAnnId(TableNames.Ann, newItem.AnnID);
                if (teamWorkForm.IsRaszInserted)
                    await _artNormService.DeleteByAnnId(TableNames.Rasz, newItem.AnnID);
                if (teamWorkForm.IsRaskInserted)
                    await _artNormService.DeleteByAnnId(TableNames.Rask, newItem.AnnID);
                if (teamWorkForm.IsKontInserted)
                    await _artNormService.DeleteByAnnId(TableNames.Kont, newItem.AnnID);

                _bindingSource.ResetBindings(false);
                ANNgridControl.RefreshDataSource();
                ANNgridView.RefreshData();
            }
        }
        private async Task Arch(object sender, EventArgs e)
        {
            int rowHandle = gridViewPreArch.FocusedRowHandle;
            MyDataANN Row = gridViewPreArch.GetRow(rowHandle) as MyDataANN;
            int newId = Row.AnnID;
            ArtNormN oldRow = await _dbService.GetEntityAsync<ArtNormN>(@"Select * from ArtNormNView where annId = @newId", new { newId });
            int oldId = oldRow.AnnID;
            int newRowId = await _dbService.GetEntityAsync<int>(@"select annId from art_norm_n where parentId = @oldId", new { oldId });
            if (oldRow == null) return;

            await _logger.LogEventAsync($"Установка нового статуса: {Status.Archive}", "Arch");

            oldRow.Status = (int)Status.Archive;
            oldRow.StatusText = StatusHelper.GetStatusText((int)Status.Archive);

            await _dbService.UpdateFieldAsync(TableNames.Ann, "Status", oldRow.Status, TableNames.AnnId, oldRow.AnnID);
            await _dbService.UpdateFieldAsync(TableNames.Ann, "Status", Status.Actual, TableNames.AnnId, newRowId);
            UpdateRowInBindingList(oldRow);

            await _dbService.UpdateFieldAsync("sp_Articul", "annId", oldRow.AnnID, "annId", newRowId);
            await _logger.LogEventAsync($"Запись ID={oldRow.AnnID} архивирована. Артикулы {""} привязаны к новой записи {oldRow.ParentId}", "Arch");

        }

        #endregion

        #region архив+копия
        /// <summary>
        /// Архив+копия
        /// </summary>
        /// <returns></returns>
        private async Task ArchAndCopy()
        {
            if (ANNgridView == null || ANNgridView.FocusedRowHandle < 0)
            {
                MessageBox.Show("Выберите запись для архивирования", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedItem = ANNgridView.GetRow(ANNgridView.FocusedRowHandle) as ArtNormN;
            if (selectedItem == null) return;

            ArtNormN newRow = null;
            int? oldStatus = selectedItem.Status;

            try
            {
                await _logger.LogEventAsync($"Начало архивирования. Исходный статус: {oldStatus}", "ArchAndCopy");

                bool hasNZP = await checkNzp(selectedItem.AnnID);
                await _logger.LogEventAsync($"Проверка НЗП: {hasNZP}", "ArchAndCopy");

                newRow = await CopyRow(hasNZP);
                await _logger.LogEventAsync($"Создана новая запись со статусом: {newRow?.Status}", "ArchAndCopy");

                if (newRow == null)
                {
                    MessageBox.Show("Не удалось создать новую запись.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

				// Открываем форму редактирования новой записи (немодально)
				var editForm = OpenAdvanceFormNonModal(bufferId, (int)Mode.ArchAndCopy, newRow.AnnID, selectedItem.AnnID);
				if (editForm == null)
				{
					return;
				}
				editForm.FormClosed += async (s, args) =>
				{
					if (editForm.DialogResult == DialogResult.OK)
					{
						await HandleSuccessfulEdit(selectedItem, editForm.CreatedAnn, hasNZP);
					}
					else
					{
						await HandleCancelledEdit(selectedItem, newRow, oldStatus);
					}
				};
            }
            catch (Exception ex)
            {
                await HandleArchAndCopyError(selectedItem, newRow, oldStatus, ex);
            }
        }


        private async Task ArchAndCopy(GridView gridView, IList list, BindingSource bindingSource, bool forMyDataAnnView = false)
        {
            if (gridView == null || gridView.FocusedRowHandle < 0)
            {
                MessageBox.Show("Выберите запись для архивирования", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int rowHandle = gridView.FocusedRowHandle;

            // 1. Определяем исходный объект ArtNormN
            ArtNormN selectedItem = null;
            if (!forMyDataAnnView)
            {
                selectedItem = gridView.GetRow(rowHandle) as ArtNormN;
            }
            else
            {
                var myDataAnn = gridView.GetRow(rowHandle) as MyDataANN;
                if (myDataAnn != null)
                    selectedItem = await _artNormService.GetArtNormDataById(myDataAnn.AnnID);
            }
            if (selectedItem == null) return;

            ArtNormN newRow = null;
            int? oldStatus = selectedItem.Status;

            try
            {
                await _logger.LogEventAsync($"Начало архивирования. Исходный статус: {oldStatus}", "ArchAndCopy");

                bool hasNZP = await checkNzp(selectedItem.AnnID);
                await _logger.LogEventAsync($"Проверка НЗП: {hasNZP}", "ArchAndCopy");

                // 2. Копируем строку (метод может быть вынесен отдельно по аналогии с CopyRow)
                newRow = await CopyRowGeneric(selectedItem, hasNZP, list, bindingSource, forMyDataAnnView);
                newRow.dateCreate = DateTime.Now;
                newRow.dateUpdate = null;
                await _logger.LogEventAsync($"Создана новая запись со статусом: {newRow?.Status}", "ArchAndCopy");

                if (newRow == null)
                {
                    MessageBox.Show("Не удалось создать новую запись.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 3. Открываем форму редактирования новой записи (немодально)
                var editForm = OpenAdvanceFormNonModal(bufferId, (int)Mode.ArchAndCopy, newRow.AnnID, selectedItem.AnnID);
                if (editForm == null)
                {
                    return;
                }
                editForm.FormClosed += async (s, args) =>
                {
                    if (editForm.DialogResult == DialogResult.OK)
                    {
                        await HandleSuccessfulEdit(selectedItem, editForm.CreatedAnn, hasNZP);
                    }
                    else
                    {
                        await HandleCancelledEdit(selectedItem, newRow, oldStatus);
                    }
                };
            }
            catch (Exception ex)
            {
                await HandleArchAndCopyError(selectedItem, newRow, oldStatus, ex);
            }
        }

        private async Task<ArtNormN> CopyRowGeneric(ArtNormN sourceRecord, bool nzp, IList list, BindingSource bindingSource, bool forMyDataAnnView)
        {
            try
            {
                ArtNormN newRecord = sourceRecord.CloneProperties();
                newRecord.dateCreate = DateTime.Now;
                newRecord.dateUpdate = null;
                newRecord.Status = nzp ? (int)Status.Preliminary : (int)Status.Actual;
                newRecord.StatusText = StatusHelper.GetStatusText(newRecord.Status);
                newRecord.Arh = false;
                newRecord.ParentId = sourceRecord.AnnID;
                newRecord.AnnID = 0; // база присвоит новый

                int tempIndex = -1;
                object itemToAdd = forMyDataAnnView ? ToMyDataANN(newRecord) : newRecord;

                // Добавляем в нужный список, если это второй грид
                list.Add(itemToAdd);

                // Сохраняем в базе
                newRecord.AnnID = await _dbService.InsertEntityAsync(TableNames.Ann, TableNames.AnnId, newRecord);
                if (newRecord.AnnID <= 0)
                {
                    MessageBox.Show("Не удалось сохранить копию записи в базе данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    list.Remove(itemToAdd); // удаляем из списка если база не сохранила
                    return null;
                }

                // Если это gridView_wdToBind — обновляем MyDataANN с актуальным AnnID
                if (forMyDataAnnView)
                {
                    var updatedMyDataAnn = ToMyDataANN(newRecord);
                    tempIndex = list.IndexOf(itemToAdd);
                    if (tempIndex >= 0)
                    {
                        list[tempIndex] = updatedMyDataAnn;
                    }
                }

                bindingSource.ResetBindings(false);

                return newRecord;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при копировании записи");
                MessageBox.Show($"Произошла ошибка при копировании: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private async Task HandleSuccessfulEdit(ArtNormN selectedItem, ArtNormN newRow, bool hasNZP)
        {
            if (newRow == null) return;

            int newStatus = hasNZP ? (int)Status.PreliminaryArchive : (int)Status.Archive;

            await _logger.LogEventAsync($"Установка нового статуса: {newStatus}", "ArchAndCopy");

            selectedItem.Status = newStatus;
            selectedItem.StatusText = StatusHelper.GetStatusText(newStatus);

            await _dbService.UpdateFieldAsync(TableNames.Ann, "Status", newStatus, TableNames.AnnId, selectedItem.AnnID);

            UpdateRowInBindingList(newRow);
            if (!hasNZP)
                await _dbService.UpdateFieldAsync("sp_Articul", "annId", newRow.AnnID, "annId", selectedItem.AnnID);
            
            // Фокусируемся на новой строке после успешного редактирования
            int rowHandle = ANNgridView.LocateByValue("AnnID", newRow.AnnID);
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
            
            await _logger.LogEventAsync($"Запись ID={selectedItem.AnnID} архивирована. Создана новая запись ID={newRow.AnnID}, нзп {(hasNZP ? "отсутствует" : "присутствует")}", "ArchAndCopy");
            
            // Запускаем асинхронное обновление секунд для новой записи
            _ = Task.Run(async () =>
            {
                await _secondsUpdateManager.StartSecondsUpdateAsync(newRow.AnnID, ANNgridView, _bindingList, ShowSecondsUpdateStatus);
                // Очищаем статус через 3 секунды после завершения
                await Task.Delay(3000);
                ClearSecondsUpdateStatus();
            });
        }
        private async Task HandleCancelledEdit(ArtNormN selectedItem, ArtNormN newRow, int? oldStatus)
        {
            if (oldStatus.HasValue)
            {
                await _logger.LogEventAsync($"Восстановление исходного статуса: {oldStatus.Value}", "ArchAndCopy");

                selectedItem.Status = oldStatus.Value;
                selectedItem.StatusText = StatusHelper.GetStatusText(oldStatus.Value);

                await _dbService.UpdateFieldAsync(TableNames.Ann, "Status", oldStatus.Value, TableNames.AnnId, selectedItem.AnnID);
            }

            if (newRow != null && newRow.AnnID > 0)
            {
                _bindingList.Remove(newRow);
                _bindingSource.Remove(newRow);
                await _artNormService.DeleteByAnnId(TableNames.Ann, newRow.AnnID);
            }

            _bindingSource.ResetBindings(false);
            ANNgridView.RefreshData();
            
            // Фокусируемся на исходной строке после отмены
            if (selectedItem != null)
            {
                int rowHandle = ANNgridView.LocateByValue("AnnID", selectedItem.AnnID);
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
        private async Task HandleArchAndCopyError(ArtNormN selectedItem, ArtNormN newRow, int? oldStatus, Exception ex)
            {
                if (newRow != null && newRow.AnnID > 0)
                {
                    _bindingList.Remove(newRow);
                    _bindingSource.Remove(newRow);
                await _artNormService.DeleteByAnnId(TableNames.Ann, newRow.AnnID);
                }

                if (oldStatus.HasValue && selectedItem != null)
                {
                await _logger.LogEventAsync($"Ошибка. Восстановление исходного статуса: {oldStatus.Value}", "ArchAndCopy");

                    selectedItem.Status = oldStatus.Value;
                    selectedItem.StatusText = StatusHelper.GetStatusText(oldStatus.Value);

                await _dbService.UpdateFieldAsync(TableNames.Ann, "Status", oldStatus.Value, TableNames.AnnId, selectedItem.AnnID);
                }

                // Фокусируемся на исходной строке после ошибки
                if (selectedItem != null)
                {
                    _bindingSource.ResetBindings(false);
                    int rowHandle = ANNgridView.LocateByValue("AnnID", selectedItem.AnnID);
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

                await _logger.LogErrorAsync(ex, "Ошибка при архивировании и копировании записи");
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void UpdateRowInBindingList(ArtNormN newRow)
        {
            int index = _bindingList.IndexOf(_bindingList.FirstOrDefault(x => x.AnnID == newRow.AnnID));
            if (index >= 0)
            {
                _bindingList[index] = newRow;
                _bindingSource.ResetBindings(false);

                int rowHandle = ANNgridView.LocateByValue("AnnID", newRow.AnnID);
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
        private async Task<bool> checkNzp(int selectedAnnId)
        {
            bool hasNZP = false;
            if (gridViewNZP != null && gridViewNZP.FocusedRowHandle >= 0)
            {
                object nzpValue = gridViewNZP.GetRowCellValue(gridViewNZP.FocusedRowHandle, "kolNZP");
                if (nzpValue != null && nzpValue != DBNull.Value)
                {
                    int nzp = Convert.ToInt32(nzpValue);
                    hasNZP = nzp > 0;
                }
                await _logger.LogEventAsync($"Запись ID={selectedAnnId} имеет НЗП: {hasNZP}", "ArchAndCopy");
            }
            return hasNZP;
        }

        /// <summary>
        /// Создает копию выбранной записи разделения труда в базе данных
        /// </summary>
        /// <returns>новая запись или null в случае ошибки</returns>
        /// 
        private async Task<ArtNormN> CopyRow(bool nzp)
        {
            try
            {
                int selectedRowHandle = ANNgridView.FocusedRowHandle;
                if (selectedRowHandle < 0)
                {
                    MessageBox.Show("Выберите разделение труда для копирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                var sourceRecord = ANNgridView.GetRow(selectedRowHandle) as ArtNormN;
                if (sourceRecord == null)
                {
                    MessageBox.Show("Не удалось получить данные выбранной записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                ArtNormN newRecord = sourceRecord.CloneProperties();
                newRecord.dateCreate = DateTime.Now;
                newRecord.dateUpdate = null;
                newRecord.Status = nzp ? (int)Status.Preliminary : (int)Status.Actual;
                newRecord.StatusText = StatusHelper.GetStatusText(newRecord.Status);
                newRecord.Arh = false;
                newRecord.ParentId = sourceRecord.AnnID;
                newRecord.AnnID = 0; // чтобы при вставке база сама назначила ID

                _bindingList.Add(newRecord);

                newRecord.AnnID = await _dbService.InsertEntityAsync(TableNames.Ann, TableNames.AnnId, newRecord);
                if (newRecord.AnnID <= 0)
                {
                    MessageBox.Show("Не удалось сохранить копию записи в базе данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                return newRecord;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при копировании записи");
                MessageBox.Show($"Произошла ошибка при копировании: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region отвязать
        /// <summary>
        /// Обработчик кнопки "Отвязать артикул от РТ" для вкладки артикулов.
        /// Использует универсальную процедуру UnbindArticulesFromWorkDivision_Internal.
        /// </summary>
        private async Task UnboundWD(object sender, EventArgs e)
        {
            await _logger.LogEventAsync("UnboundWD: Отвязка РТ от артикула на вкладке артикулов");
            
            // Используем универсальную процедуру для вкладки артикулов
            await UnbindArticulesFromWorkDivision_Internal(
                gridControlNZP.MainView as GridView,  // GridView НЗП
                _nzpListArt,                          // Источник данных НЗП для вкладки артикулов
                gridView_wdToBind,                    // GridView с РТ (MyDataANN)
                useCheckedRows: true                  // Используем отмеченные строки
            );
        }
        #endregion

    }
}
