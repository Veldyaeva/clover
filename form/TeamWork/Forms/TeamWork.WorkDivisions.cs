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

                        // Заменяем _bindingList на новый BindingList с данными
                        //                        _bindingSource.DataSource = new BindingList<ArtNormN>(data);
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


        private void SearchWorkDivisionsButton_Click(object sender, EventArgs e)
        {
            // TODO: Поиск разделений труда
        }
        private async Task LoadRelatedData(int annId)
        {
            await GridHelper.LoadGridControlDataAsync(gridControl1, normraszBindingSource, await _artNormService.GetRelatedNormRasz(annId));
            //await GridHelper.LoadGridControlDataAsync(gridControl1, normraszBindingSource, _artNormService.GetRelatedNormRasz1(annId));
            await GridHelper.LoadGridControlDataAsync(gridControl3, normraskBindingSource, await _artNormService.GetRelatedNormRask(annId));
            await GridHelper.LoadGridControlDataAsync(gridControl4, normkontBindingSource, await _artNormService.GetRelatedNormKont(annId));
            await GridHelper.LoadGridControlDataAsync(gridControl5, normdopobrBindingSource, await _artNormService.GetRelatedNormDopObr(annId));
            await GridHelper.LoadGridControlDataAsync(customGridControl5, sparticulBindingSource, await _artNormService.GetRelatedSpArt(annId));
            //  await GridHelper.LoadGridControlDataAsync(gridControlPreArch, sparticulBindingSource1, await _artNormService.GetRelatedSpArt(annId));
            UpdateNZPStatus();
        }
        private async void UpdateNZPStatus()
        {
            try
            {
                var view = customGridControl5.MainView as GridView;
                if (view == null || view.FocusedRowHandle < 0) return;

                int nzp = CommonFunctions.GetRowCellValueOrDefault<int>(view, view.FocusedRowHandle, "kolNZP", 0);
                ButtonUnboundWd.Enabled = nzp <= 0;

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
        private async void ButtonPreliminaryWd_Click(object sender, EventArgs e)
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
            GridView annView = ANNgridView;
            if (annView == null || annView.FocusedRowHandle < 0)
            {
                MessageBox.Show("Выберите запись для архивирования", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ArtNormN selectedItem = ANNgridView.GetRow(annView.FocusedRowHandle) as ArtNormN;
            ArtNormN newRow = null;
            int? oldStatus = selectedItem?.Status;

            try
            {
                bool hasNZP = await checkNzp(selectedItem.AnnID);
                newRow = await CopyRow(hasNZP);

                using (var teamWorkAdvanceTW = new TeamWork_AdvanceTW(bufferId, (int)Mode.ArchAndCopy, newRow.AnnID, selectedItem.AnnID))
                {
                    await HandleAnnEditResult(teamWorkAdvanceTW, newRow);
                }

                selectedItem.Status = hasNZP ? (int)Status.PreliminaryArchive : (int)Status.Archive;
                selectedItem.StatusText = StatusHelper.GetStatusText(selectedItem.Status);
                await _artNormService.UpdateAnnId("art_norm_n", selectedItem.AnnID, "Status", selectedItem.Status);

                await _logger.LogEventAsync($"Запись ID={selectedItem.AnnID} архивирована. Создана новая запись ID={newRow.AnnID}", "CopyRow");

                await BindArticulToNewRowAsync(selectedItem.AnnID, newRow.AnnID);

                _bindingSource.ResetBindings(false);
                ANNgridView.RefreshData();
            }
            catch (Exception ex)
            {
                if (newRow != null && newRow.AnnID > 0)
                {
                    _bindingList.Remove(newRow);
                    _bindingSource.Remove(newRow);
                    await _artNormService.deleteRow("art_norm_n", newRow.AnnID);
                }

                if (oldStatus.HasValue && selectedItem != null)
                {
                    selectedItem.Status = oldStatus.Value;
                    selectedItem.StatusText = StatusHelper.GetStatusText(oldStatus.Value);
                    await _artNormService.UpdateAnnId("art_norm_n", selectedItem.AnnID, "Status", oldStatus.Value);
                }

                await _logger.LogErrorAsync(ex, "Ошибка при архивировании и копировании записи");
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                await _artNormService.deleteRow("art_norm_n", newItem.AnnID);
                if (teamWorkForm.IsRaszInserted)
                    await _artNormService.deleteRow("norm_rasz", newItem.AnnID);
                if (teamWorkForm.IsRaskInserted)
                    await _artNormService.deleteRow("norm_rask", newItem.AnnID);
                if (teamWorkForm.IsKontInserted)
                    await _artNormService.deleteRow("norm_kont", newItem.AnnID);
                if (teamWorkForm.IsDopObrInserted)
                    await _artNormService.deleteRow("norm_dop_obr", newItem.AnnID);

                _bindingSource.ResetBindings(false);
                ANNgridControl.RefreshDataSource();
                ANNgridView.RefreshData();
            }
        }


        private async Task BindArticulToNewRowAsync(int oldAnnId, int newAnnId)
        {
            var tableNames = new List<string> { "norm_rasz", "norm_rask", "norm_kont", "norm_dop_obr" };
            foreach (var table in tableNames)
            {
                await _artNormService.UpdateAnnId(table, oldAnnId, "AnnId", newAnnId);
            }
        }


        private async Task updateNewRow(int selectedAnnId, int newId)
        {
            await LoadWorkDivisions();
         //   await CurrentWorks_Load();
            await LoadRelatedData(newId);
            await _logger.LogEventAsync($"Запись ID={selectedAnnId} успешно архивирована и скопирована как ID={newId}", "ArchAndCopy");

            // Показываем сообщение об успешном завершении операции
            MessageBox.Show(
                $"Запись успешно архивирована",
                "Информация",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }


        private async Task<bool> checkNzp(int selectedAnnId)
        {
            bool hasNZP = false;
            if (gridView10.RowCount > 0)
                for (int i = 0; i < gridView10.RowCount; i++)
                {
                    object rowObject = gridView10.GetRow(i);
                    int nzp = Convert.ToInt32(gridView10.GetRowCellValue(0, "kolNZP"));
                    hasNZP = nzp > 0;
                    await _logger.LogEventAsync($"Запись ID={selectedAnnId} имеет НЗП: {hasNZP}", "ArchAndCopy");

                }

            return hasNZP;
        }

        /// <summary>
        /// Создает копию выбранной записи разделения труда в базе данных
        /// </summary>
        /// <returns>новая запись или null в случае ошибки</returns>
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

                // Получаем выбранную запись
                ArtNormN sourceRecord = ANNgridView.GetRow(selectedRowHandle) as ArtNormN;
                if (sourceRecord == null)
                {
                    MessageBox.Show("Не удалось получить данные выбранной записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                // Создаем копию записи с новыми значениями
                ArtNormN newRecord = new ArtNormN
                {
                    Kod = sourceRecord.Kod,
                    Group = sourceRecord.Group,
                    Articul = sourceRecord.Articul,
                    Mod = sourceRecord.Mod,
                    SekShv = sourceRecord.SekShv,
                    SekVyaz5 = sourceRecord.SekVyaz5,
                    SekVyaz6 = sourceRecord.SekVyaz6,
                    SekVyaz7 = sourceRecord.SekVyaz7,
                    SekVyaz10 = sourceRecord.SekVyaz10,
                    SekVyaz12 = sourceRecord.SekVyaz12,
                    SekVyazo = sourceRecord.SekVyazo,
                    SekVyaz = sourceRecord.SekVyaz,
                    Sek = sourceRecord.Sek,
                    Komment = sourceRecord.Komment == null ? "" : sourceRecord.Komment,
                    dateCreate = DateTime.Now,
                    Diz = sourceRecord.Diz,
                    Constr = sourceRecord.Constr,
                    dataUpdate = null,
                    SekKr = sourceRecord.SekKr,
                    Slogn = sourceRecord.Slogn,
                    Arh = false,
                    Status = nzp ? (int)Status.Preliminary : (int)Status.Actual,
                    StatusText = StatusHelper.GetStatusText(nzp ? (int)Status.Preliminary : (int)Status.Actual),
                    preArch = ((int)sourceRecord.Status == (int)Status.PreliminaryArchive) ? true : false,

                };
                _bindingList.Add(newRecord);
                // Сохраняем копию в базу данных
                newRecord.AnnID = await Task.Run(() => _artNormService.SaveCopyToDatabase(newRecord));
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

    }
}
