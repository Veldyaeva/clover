using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Helpers;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork
    {
        /// <summary>
        /// Отвязывает артикулы от выбранного разделения труда, устанавливая annid = 0 в таблице sp_articul
        /// </summary>
        /// <summary>
        /// Универсальная процедура отвязки артикулов от РТ для разных гридов
        /// </summary>
        /// <param name="nzpGridView">GridView с данными НЗП</param>
        /// <param name="nzpDataSource">Источник данных НЗП (BindingList)</param>
        /// <param name="workDivisionGridView">GridView с разделениями труда</param>
        /// <param name="useCheckedRows">Использовать отмеченные строки (true) или текущую выбранную (false)</param>
        private async Task UnbindArticulesFromWorkDivision_Internal(
            GridView nzpGridView,
            IEnumerable<NZPByKoddRt> nzpDataSource,
            GridView workDivisionGridView,
            bool useCheckedRows = false)
        {
            try
            {
                await _logger.LogEventAsync($"UnbindArticulesFromWorkDivision_Internal: Начало отвязки. UseCheckedRows: {useCheckedRows}", "UnbindArticles");

                // Проверяем входные параметры
                if (nzpGridView == null || workDivisionGridView == null)
                {
                    MessageBox.Show("Один из гридов не инициализирован.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Получаем данные НЗП для отвязки
                List<NZPByKoddRt> itemsToUnbind;

                if (useCheckedRows)
                {
                    // Используем отмеченные строки
                    itemsToUnbind = nzpDataSource?.Where(row => row.IsChecked).ToList();
                    if (itemsToUnbind == null || !itemsToUnbind.Any())
                    {
                        MessageBox.Show("Выберите артикулы для отвязки!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    // Используем текущую выбранную строку
                    if (nzpGridView.FocusedRowHandle < 0)
                    {
                        MessageBox.Show("Выберите запись в гриде НЗП.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var selectedNzp = nzpGridView.GetRow(nzpGridView.FocusedRowHandle) as NZPByKoddRt;
                    if (selectedNzp == null)
                    {
                        MessageBox.Show("Не удалось получить данные выбранной записи в гриде НЗП.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    itemsToUnbind = new List<NZPByKoddRt> { selectedNzp };
                }

                // Получаем данные РТ
                ArtNormN selectedAnn = null;
                if (workDivisionGridView.FocusedRowHandle >= 0)
                {
                    selectedAnn = workDivisionGridView.GetRow(workDivisionGridView.FocusedRowHandle) as ArtNormN;
                    if (selectedAnn == null && workDivisionGridView.Name == "gridView_wdToBind")
                    {
                        // Для gridView_wdToBind получаем MyDataANN и конвертируем
                        var myDataAnn = workDivisionGridView.GetRow(workDivisionGridView.FocusedRowHandle) as MyDataANN;
                        if (myDataAnn != null)
                        {
                            selectedAnn = await _artNormService.GetArtNormDataById(myDataAnn.AnnID);
                        }
                    }
                }

                if (selectedAnn == null)
                {
                    MessageBox.Show("Не удалось получить данные выбранного разделения труда.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Подтверждение операции
                string displayInfo = $"Группа: {selectedAnn.grup?.TrimEnd(' ')}, " +
                                   $"Модель: {selectedAnn.Mod?.TrimEnd(' ')}, " +
                                   $"Артикул: {selectedAnn.Articul?.TrimEnd(' ')}";

                string message = $"Отвязать {itemsToUnbind.Count} артикул(ов) от разделения труда:\n\n" +
                                $"AnnID: {selectedAnn.AnnID}\n" +
                                $"{displayInfo}\n\n" +
                                $"Продолжить?";

                var result = MessageBox.Show(message, "Подтверждение отвязывания артикулов",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (result != DialogResult.Yes)
                    return;

                int totalAffectedRows = 0;

                // Обрабатываем каждый элемент для отвязки
                foreach (var nzpItem in itemsToUnbind)
                {
                    string kod = nzpItem.kodd.ToString(); // код артикула из строки НЗП
                    int annIdNzpRow = nzpItem.annId; // AnnID РТ из строки НЗП
                    string articul = nzpItem.articul?.TrimEnd(' ') ?? string.Empty;

                    await _logger.LogEventAsync($"Отвязка артикула KOD: {kod}, articul: {articul}, AnnID: {annIdNzpRow}", "UnbindArticles");
                    var parameters = new Dictionary<string, object>
                {
                    { "@annID", annIdNzpRow },
                    { "@kod", kod },
                    { "@art", articul}
                };
                    // Вызов метода для отвязки артикула в sp_articul
                    await _dbService.UpdateFieldAsync(TableNames.Art, "annId", string.Empty, "left(kod, 7) = @kod AND articul = @art AND annID = @annId", parameters);//_artNormService.ResetAnnIdinArticul(kod);
                    await _dbService.UpdateFieldAsync(TableNames.Ann, "size_label", null, TableNames.AnnId, annIdNzpRow);

                    // Обновление статуса РТ
                    await _dbService.UpdateFieldAsync(TableNames.Ann, "status", (int)Status.Actual, "parentId", annIdNzpRow);

                    totalAffectedRows++;
                }

                // Логируем операцию
                await _logger.LogEventAsync($"Отвязано {totalAffectedRows} артикулов от РТ. AnnID: {selectedAnn.AnnID}, {displayInfo}", "UnbindArticles");

                // Показываем результат
                if (totalAffectedRows > 0)
                {
                    MessageBox.Show($"Успешно отвязано {totalAffectedRows} артикул(ов) от разделения труда.",
                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Обновляем связанные данные в интерфейсе
                    await LoadRelatedData(selectedAnn.AnnID);

                    // Обновляем NZP данные
                    await RefreshNzpData(workDivisionGridView);
                }
                else
                {
                    MessageBox.Show("Не найдено артикулов для отвязывания.", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при отвязывании артикулов от разделения труда");
                MessageBox.Show($"Ошибка при отвязывании артикулов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обновляет данные НЗП после отвязки
        /// </summary>
        private async Task RefreshNzpData(GridView workDivisionGridView)
        {
            try
            {
                if (workDivisionGridView?.FocusedRowHandle >= 0)
                {
                    int annId = 0;
                    if (workDivisionGridView.Name == "ANNgridView")
                    {
                        var ann = workDivisionGridView.GetRow(workDivisionGridView.FocusedRowHandle) as ArtNormN;
                        annId = ann?.AnnID ?? 0;
                    }
                    else if (workDivisionGridView.Name == "gridView_wdToBind")
                    {
                        var myDataAnn = workDivisionGridView.GetRow(workDivisionGridView.FocusedRowHandle) as MyDataANN;
                        annId = myDataAnn?.AnnID ?? 0;
                    }

                    if (annId > 0)
                    {
                        var nzpData = await _artNormService.GetNzpWithPztCounts(annId);
                        if (workDivisionGridView.Name == "ANNgridView")
                        {
                            _nzpListWd?.BulkLoad(nzpData ?? new List<NZPByKoddRt>());
                            _nzpByKoddRtSourceWd?.ResetBindings(false);
                        }
                        else if (workDivisionGridView.Name == "gridView_wdToBind")
                        {
                            _nzpListArt?.BulkLoad(nzpData ?? new List<NZPByKoddRt>());
                            _nzpByKoddRtSourceArt?.ResetBindings(false);
                        }
                        gridControlNZP?.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при обновлении данных НЗП");
            }
        }

        // Оригинальный метод для обратной совместимости (первая вкладка)
        private async Task UnbindArticulesFromWorkDivision_Internal(object sender, EventArgs e)
        {
            await _logger.LogEventAsync("UnbindArticulesFromWorkDivision_Internal: Отвязка на первой вкладке");

            // Используем универсальную процедуру для первой вкладки
            await UnbindArticulesFromWorkDivision_Internal(
                gridViewNZP,           // GridView НЗП (customGridControl4)
                                       //gridViewBindedArts,
                _nzpListWd,          // Источник данных НЗП для первой вкладки
                ANNgridView,         // GridView с РТ (ArtNormN)
                useCheckedRows: false // Используем текущую выбранную строку
            );
        }
    }
}
