using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BindingSource = System.Windows.Forms.BindingSource;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork
    {
        private async Task DuplicateWorkDivision_Click_Internal(GridView gridView, IList list, BindingSource bindingSource, bool forMyDataAnnView = false)
        {
            if (gridView == null || gridView.FocusedRowHandle < 0)
            {
                MessageBox.Show("Выберите Разделение Труда для дублирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                await _logger.LogWarningAsync("Попытка дублирования без выбора строки", "DuplicateWorkDivision_Click_Internal");
                return;
            }

            var selectedAnnToDuplicate = gridView.GetRow(gridView.FocusedRowHandle) as ArtNormN;
            int rowHandle = gridView.FocusedRowHandle;

            if (forMyDataAnnView)
            { // Вторая вкладка: объект - MyDataANN (нужно получить ArtNormN по AnnID)
                var selectedMyDataAnn = gridView.GetRow(rowHandle) as MyDataANN;
                if (selectedMyDataAnn == null) return;
                int annId = selectedMyDataAnn.AnnID;
                selectedAnnToDuplicate = await _artNormService.GetArtNormDataById(annId);
                if (selectedAnnToDuplicate == null) return;
            }
            if (selectedAnnToDuplicate == null)
            {
                MessageBox.Show("Не удалось получить данные выбранного РТ.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await _logger.LogWarningAsync("Не удалось получить данные выбранного РТ", "DuplicateWorkDivision_Click_Internal");
                return;
            }


            ArtNormN CopyedWorkDivisionShell = selectedAnnToDuplicate.CloneOperationalData();
            // Инициализируем корректный начальный статус для дубля
            CopyedWorkDivisionShell.Status = (int)Status.Preliminary;
            CopyedWorkDivisionShell.StatusText = StatusHelper.GetStatusText(CopyedWorkDivisionShell.Status);
            CopyedWorkDivisionShell.AnnID = 0;
            CopyedWorkDivisionShell.Arh = false;
            CopyedWorkDivisionShell.dateCreate = DateTime.Now;
            CopyedWorkDivisionShell.dateUpdate = null;

            int newAnnId = await _dbService.InsertEntityAsync(TableNames.Ann, TableNames.AnnId, CopyedWorkDivisionShell);
            if (newAnnId <= 0)
            {
                MessageBox.Show("Ошибка при создании новой записи РТ в базе данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await _logger.LogWarningAsync("Ошибка при создании новой записи РТ в базе данных", "DuplicateWorkDivision_Click_Internal");
                return;
            }

            // Открываем форму редактирования клона (немодально)
            var teamWorkAdvanceTW = OpenAdvanceFormNonModal(bufferId, (int)Mode.Clone, newId: newAnnId, oldId: selectedAnnToDuplicate.AnnID);
            if (teamWorkAdvanceTW == null)
            {
                // Форма уже открыта или произошла ошибка
                return;
            }

            teamWorkAdvanceTW.FormClosed += async (s, args) =>
            {
                if (teamWorkAdvanceTW.DialogResult == DialogResult.OK)
                {
                    CopyedWorkDivisionShell = teamWorkAdvanceTW.CreatedAnn;
                    if (CopyedWorkDivisionShell == null) return;

                    _bindingList.Add(CopyedWorkDivisionShell);
                    _bindingSource.ResetBindings(false);
                    rowHandle = ANNgridView.LocateByValue("AnnID", CopyedWorkDivisionShell.AnnID);
                    if (rowHandle >= 0)
                    {
                        ANNgridView.BeginUpdate();
                        try
                        {
                            ANNgridView.FocusedRowHandle = rowHandle;
                            ANNgridView.MakeRowVisible(rowHandle);
                            ANNgridView.RefreshRow(rowHandle);
                        }
                        finally
                        {
                            ANNgridView.EndUpdate();
                        }
                    }
                }
                else
                {
                    // Возврат к исходной строке
                    rowHandle = ANNgridView.LocateByValue("AnnID", selectedAnnToDuplicate.AnnID);
                    if (rowHandle >= 0)
                    {
                        ANNgridView.BeginUpdate();
                        try
                        {
                            ANNgridView.FocusedRowHandle = rowHandle;
                            ANNgridView.MakeRowVisible(rowHandle);
                            ANNgridView.RefreshRow(rowHandle);
                        }
                        finally
                        {
                            ANNgridView.EndUpdate();
                        }
                    }

                    // Удаляем созданную запись из списка и базы
                    _bindingList.Remove(CopyedWorkDivisionShell);
                    _bindingSource.ResetBindings(false);
                    await _artNormService.DeleteRelatedNormTables(newAnnId);
                    await _artNormService.DeleteByAnnId(TableNames.Ann, newAnnId);
                }
            };
        }

        private async Task SendMsgToBrig(int annId, string msg)
        {
            try
            {
                List<Brig> brigades = await _artNormService.GetWorkingBrigs(annId);
                var brigIds = brigades?
    .Select(b => b.id_brig)
    .Where(id => id > 0)
    .Distinct()
    .ToArray();

                if (brigIds is { Length: > 0 })
                {
                    await _jabberSender.SendToBrigsAsync(brigIds, msg);
                    await _logger.LogEventAsync(
                        $"Отправлено '{msg}' в {brigIds.Length} бригад(ы) для annId={annId}",
                        "EditWd_Internal2");
                }
                else
                {
                    await _logger.LogEventAsync(
                        $"Бригад для рассылки не найдено (annId={annId})",
                        "EditWd_Internal2");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, "Ошибка при отправке сообщения в бригады");
            }
        }
    }
}
