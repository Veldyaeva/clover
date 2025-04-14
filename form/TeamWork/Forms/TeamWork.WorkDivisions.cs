// TeamWork.WorkDivisions.cs
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.form;
using SewingProduction.Helpers;
using SewingProduction.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.ComponentModel;
using System.Linq;
using static DevExpress.Xpo.Helpers.CannotLoadObjectsHelper;
using Dapper;

namespace SewingProduction.Forms
{
    public partial class TeamWork
    {
        // Первая вкладка — "Разделение труда"

        /// <summary>
        /// Загрузка вкладки "Список РТ"
        /// </summary>
        /// <returns></returns>
        private async Task LoadWorkDivisions()
        {
            try
            {
                // Получаем данные
                var data = await _artNormService.GetArtNormData();

                if (data != null && data.Count > 0)
                {
                    // Отключаем обновление UI во время загрузки данных
                    ANNgridControl.BeginUpdate();
                    try
                    {
                        // Настраиваем отображение GridView
                        ANNgridView.OptionsView.EnableAppearanceEvenRow = true;
                        ANNgridView.OptionsView.EnableAppearanceOddRow = true;
                        ANNgridView.OptionsView.ShowAutoFilterRow = true;
                        ANNgridView.OptionsView.ShowGroupPanel = false;
                        ANNgridView.OptionsView.ShowIndicator = false;
                        ANNgridView.OptionsView.ShowPreview = false;

                        _bindingList = new BindingList<ArtNormN>(data);
                        _bindingSource.DataSource = _bindingList;

                        // Обновляем источник данных
                        _bindingSource.ResetBindings(false);

                        // Применяем фильтры
                        filterTable();
                    }
                    finally
                    {
                        // Включаем обновление UI
                        ANNgridControl.EndUpdate();
                    }
                }
                else
                {
                    MessageBox.Show("Нет данных для загрузки.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                await _logger.LogEventAsync("Данные загружены успешно", "LoadData");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных в текущие работы: {ex.Message}");
            }
        }


        private async Task LoadRelatedData(int annId)
        {
            await GridHelper.LoadGridControlDataAsync(statusLabel, normraszBindingSource, await _artNormService.GetRelatedNormRasz(annId));
            await GridHelper.LoadGridControlDataAsync(gridControl3, normraskBindingSource, await _artNormService.GetRelatedNormRask(annId));
            await GridHelper.LoadGridControlDataAsync(gridControl4, normkontBindingSource, await _artNormService.GetRelatedNormKont(annId));
            await GridHelper.LoadGridControlDataAsync(gridControl5, normdopobrBindingSource, await _artNormService.GetRelatedNormDopObr(annId));
            // await GridHelper.LoadGridControlDataAsync(customGridControl5, sparticulBindingSource, await _artNormService.GetNZPByKoddRtAsync(annId);//.GetRelatedSpArt(annId));
            List<NZPByKoddRt> nzpData = await _artNormService.GetNZPByKoddRtAsync(annId);
            await GridHelper.LoadListDataAsync(customGridControl5, sparticulBindingSource, nzpData);//await _artNormService.GetNZPByKoddRtAsync(annId));


            GetNZPStatus(nzpData);

            // Сортируем каждую таблицу отдельно
            sortGridView(gridView1);
            sortGridView(gridView4);
            sortGridView(gridView3);
        }
        private async void GetNZPStatus(List<NZPByKoddRt> data)
        {
            try
            {
                var view = customGridControl5.MainView as GridView;
                if (view == null || view.FocusedRowHandle < 0) return;
                var annIdList = data.Select(x => x.annId).Distinct().ToList();

                int count = await _artNormService.GetPztRecordCountByAnnIdsAsync(annIdList);

                int nzp = CommonFunctions.GetRowCellValueOrDefault<int>(view, view.FocusedRowHandle, "kolNZP", count);
                
                ButtonUnboundWd.Enabled = (nzp <= 0||count<=0);

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка обновления статуса НЗП");
                MessageBox.Show($"Ошибка при обновлении NZP: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Добавить предварительное
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonPreliminaryWd_Click_Internal(object sender, EventArgs e)
        {
            if (ANNgridView == null) return;

            // Создаём новую запись модели `ArtNorm`
            ArtNormN newItem = new ArtNormN
            {
                Kod = "0000000",
                Group = "",
                Articul = "",
                Mod = "",
                SekShv = 0,
                SekVyaz5 = 0,
                SekVyaz6 = 0,
                SekVyaz7 = 0,
                SekVyaz10 = 0,
                SekVyaz12 = 0,
                SekVyazo = 0,
                SekVyaz = 0,
                Sek = 0,
                Komment = "",
                dateCreate = DateTime.Now,
                Diz = 0,
                Constr = 0,
                dataUpdate = DateTime.MinValue,
                SekKr = 0,
                Slogn = 0,
                Status = 1,
                StatusText = StatusHelper.GetStatusText(1),//"предварительный",
                Arh = false,
                AnnID = 0
            };

            int newId = await _artNormService.InsertEntityAsync(TableNames.Ann, TableNames.AnnId, newItem);//InsertANN(newItem);
            if (newId <= 0)
            {
                MessageBox.Show("Ошибка сохранения в БД!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Обновляем ID в объекте
            newItem.AnnID = newId;
            // Добавляем новую строку в источник данных
            _bindingList.Add(newItem);
            //// Обновляем отображение грида
            _bindingSource.ResetBindings(false);
            ANNgridControl.RefreshDataSource();


            // Открываем форму редактирования
            using (TeamWork_AdvanceTW teamWork_AdvanceTW = new TeamWork_AdvanceTW(bufferId, (int)Mode.NewWorkDivision, newId: newId))
            {
                await HandleAnnEditResult(teamWork_AdvanceTW, newItem);
            }
        }


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

                using (var editForm = new TeamWork_AdvanceTW(bufferId, (int)Mode.ArchAndCopy, newRow.AnnID, selectedItem.AnnID))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        await HandleSuccessfulEdit(selectedItem, editForm.CreatedAnn, hasNZP);
                    }
                    else
                    {
                        await HandleCancelledEdit(selectedItem, newRow, oldStatus);
                    }
                }
            }
            catch (Exception ex)
            {
                await HandleArchAndCopyError(selectedItem, newRow, oldStatus, ex);
            }
        }

        private async Task HandleSuccessfulEdit(ArtNormN selectedItem, ArtNormN newRow, bool hasNZP)
        {
            if (newRow == null) return;

            int newStatus = hasNZP ? (int)Status.PreliminaryArchive : (int)Status.Archive;

            await _logger.LogEventAsync($"Установка нового статуса: {newStatus}", "ArchAndCopy");

            selectedItem.Status = newStatus;
            selectedItem.StatusText = StatusHelper.GetStatusText(newStatus);

            await _artNormService.UpdateFieldAsync(TableNames.Ann, "Status", newStatus, TableNames.AnnId, selectedItem.AnnID);

            UpdateNewRowInBindingList(newRow);

            await _logger.LogEventAsync($"Запись ID={selectedItem.AnnID} архивирована. Создана новая запись ID={newRow.AnnID}", "ArchAndCopy");
        }

        private async Task HandleCancelledEdit(ArtNormN selectedItem, ArtNormN newRow, int? oldStatus)
        {
            if (oldStatus.HasValue)
            {
                await _logger.LogEventAsync($"Восстановление исходного статуса: {oldStatus.Value}", "ArchAndCopy");

                selectedItem.Status = oldStatus.Value;
                selectedItem.StatusText = StatusHelper.GetStatusText(oldStatus.Value);

                await _artNormService.UpdateFieldAsync(TableNames.Ann, "Status", oldStatus.Value, TableNames.AnnId, selectedItem.AnnID);
            }

            if (newRow != null && newRow.AnnID > 0)
            {
                _bindingList.Remove(newRow);
                _bindingSource.Remove(newRow);
                await _artNormService.DeleteByAnnId(TableNames.Ann, newRow.AnnID);
            }

            _bindingSource.ResetBindings(false);
            ANNgridView.RefreshData();
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

                await _artNormService.UpdateFieldAsync(TableNames.Ann, "Status", oldStatus.Value, TableNames.AnnId, selectedItem.AnnID);
            }

            await _logger.LogErrorAsync(ex, "Ошибка при архивировании и копировании записи");
            MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    newItem.Group = createdItem.Group;
                    newItem.Komment = createdItem.Komment;
                    newItem.Diz = createdItem.Diz;
                    newItem.Constr = createdItem.Constr;
                    newItem.Sek = createdItem.Sek;
                }

                _bindingSource.ResetBindings(false);
                int newRowHandle = ANNgridView.LocateByValue("AnnID", newItem.AnnID);
                if (newRowHandle >= 0)
                {
                    ANNgridView.FocusedRowHandle = newRowHandle;
                    ANNgridView.RefreshRow(newRowHandle);
                }
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
                if (teamWorkForm.IsDopObrInserted)
                    await _artNormService.DeleteByAnnId(TableNames.Obr, newItem.AnnID);

                _bindingSource.ResetBindings(false);
                ANNgridControl.RefreshDataSource();
                ANNgridView.RefreshData();
            }
        }

        private void UpdateNewRowInBindingList(ArtNormN newRow)
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
            if (gridView10 != null && gridView10.FocusedRowHandle >= 0)
            {
                object nzpValue = gridView10.GetRowCellValue(gridView10.FocusedRowHandle, "kolNZP");
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
                newRecord.dataUpdate = null;
                newRecord.Status = nzp ? (int)Status.Preliminary : (int)Status.Actual;
                newRecord.StatusText = StatusHelper.GetStatusText(newRecord.Status);
                newRecord.Arh = false;
                newRecord.AnnID = 0; // чтобы при вставке база сама назначила ID

                _bindingList.Add(newRecord);

                newRecord.AnnID = await _artNormService.InsertEntityAsync(TableNames.Ann, TableNames.AnnId, newRecord); //SaveCopyToDatabase(newRecord);
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


        private void sortGridView(GridView gridView)
        {
            if (gridView == null) return;

            gridView.BeginSort();
            try
            {
                gridView.ClearSorting(); // Очистить старую сортировку

                int sortIndex = 0; // Порядковый индекс сортировки

                // Если столбец "N" существует — сортируем
                var columnN = gridView.Columns.ColumnByFieldName("N");
                if (columnN != null)
                {
                    columnN.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    columnN.SortIndex = sortIndex++;
                }

                // Если столбец "N1" существует — сортируем
                var columnN1 = gridView.Columns.ColumnByFieldName("N1");
                if (columnN1 != null)
                {
                    columnN1.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    columnN1.SortIndex = sortIndex++;
                }

                // Если столбец "Kod_o" существует — сортируем
                var columnKodO = gridView.Columns.ColumnByFieldName("Kod_o");
                if (columnKodO != null)
                {
                    columnKodO.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    columnKodO.SortIndex = sortIndex++;
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при сортировке таблицы");
            }
            finally
            {
                gridView.EndSort();
            }
        }

    }
}
