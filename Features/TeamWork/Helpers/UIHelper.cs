using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Extensions;
using SewingProduction.Features.TeamWork.Services;
using SewingProduction.Helpers;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Helpers
{
    public class UIHelper
    {
        private readonly ILogger _logger;

        public UIHelper(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Восстанавливает фокус на записи с указанным AnnID
        /// </summary>
        public async Task<bool> RestoreFocusAsync(GridView gridView, BindingList<ArtNormN> bindingList, int annId)
        {
            try
            {
                // Ищем строку с нужным AnnID
                int rowHandle = gridView.LocateByValue("AnnID", annId);

                if (rowHandle >= 0)
                {
                    gridView.BeginUpdate();
                    try
                    {
                        gridView.FocusedRowHandle = rowHandle;
                        gridView.MakeRowVisible(rowHandle);
                        gridView.RefreshRow(rowHandle);
                    }
                    finally
                    {
                        gridView.EndUpdate();
                    }

                    await _logger.LogEventAsync($"RestoreFocus: Фокус восстановлен на AnnID: {annId}, RowHandle: {rowHandle}", "TeamWorkUIHelper");
                    return true;
                }
                else
                {
                    await _logger.LogEventAsync($"RestoreFocus: Запись с AnnID: {annId} не найдена", "TeamWorkUIHelper");

                    // Если запись не найдена, устанавливаем фокус на первую доступную
                    if (bindingList.Count > 0)
                    {
                        gridView.FocusedRowHandle = 0;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при восстановлении фокуса на AnnID: {annId}");
                return false;
            }
        }

        /// <summary>
        /// Обновляет UI с новыми связанными данными
        /// </summary>
        public async Task UpdateRelatedDataUIAsync(
            RelatedDataResult data,
            BindingList<NormRask> normRaskList,
            BindingList<NormKont> normKontList,
            BindingList<NormRasz> normRaszList,
            GridControl raszControl,
            GridControl raskrControl,
            GridControl kontControl,
            Control parentControl)
        {
            try
            {
                if (data == null || !data.Success)
                {
                    await _logger.LogEventAsync("UpdateRelatedDataUI: Переданы некорректные данные", "UIHelper");
                    return;
                }

                // Обновление данных должно происходить в UI потоке
                if (parentControl.InvokeRequired)
                {
                    await parentControl.InvokeAsync(() => UpdateRelatedDataSync(data, normRaskList, normKontList, normRaszList, raszControl, raskrControl, kontControl));
                }
                else
                {
                    UpdateRelatedDataSync(data, normRaskList, normKontList, normRaszList, raszControl, raskrControl, kontControl);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при обновлении UI связанных данных");
            }
        }

        private void UpdateRelatedDataSync(
            RelatedDataResult data,
            BindingList<NormRask> normRaskList,
            BindingList<NormKont> normKontList,
            BindingList<NormRasz> normRaszList,
            GridControl raszControl,
            GridControl raskrControl,
            GridControl kontControl)
        {
            try
            {
                if (normRaskList != null && data.NormRask != null)
                    normRaskList.BulkLoad(data.NormRask);

                if (normKontList != null && data.NormKont != null)
                    normKontList.BulkLoad(data.NormKont);

                if (normRaszList != null && data.NormRasz != null)
                    normRaszList.BulkLoad(data.NormRasz);

                // Сортировка
                if (raszControl?.MainView is GridView raszView)
                    TWGridHelper.sortGridView(raszView);
                if (raskrControl?.MainView is GridView raskrView)
                    TWGridHelper.sortGridView(raskrView);
                if (kontControl?.MainView is GridView kontView)
                    TWGridHelper.sortGridView(kontView);
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, "Ошибка при синхронном обновлении UI");
            }
        }
        /// <summary>
        /// Применяет базовые настройки к гриду
        /// </summary>
        public async Task ResetGridToBaseStateAsync(GridView gridView, bool applyBaseSorting = true)
        {
            try
            {
                if (gridView == null) return;

                gridView.BeginUpdate();

                // Очищаем фильтры и сортировку
                gridView.ActiveFilter.Clear();
                gridView.ClearSorting();
                gridView.ClearGrouping();

                // Применяем базовые настройки
                gridView.OptionsView.ShowGroupPanel = false;
                gridView.OptionsView.ShowAutoFilterRow = true;
                gridView.OptionsView.EnableAppearanceEvenRow = true;
                gridView.OptionsView.EnableAppearanceOddRow = true;
                gridView.OptionsView.ShowIndicator = true;

                if (applyBaseSorting && gridView.Columns["dateCreate"] != null)
                {
                    gridView.Columns["dateCreate"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                }

                await _logger.LogEventAsync("Грид сброшен к базовому состоянию", "ResetGridToBaseState");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при сбросе грида к базовому состоянию");
            }
            finally
            {
                gridView?.EndUpdate();
            }
        }

        /// <summary>
        /// Показывает сообщение о статусе операции
        /// </summary>
        public void ShowOperationStatus(Control parentControl, string message, int displayTimeMs = 3000)
        {
            try
            {
                if (parentControl is Form form)
                {
                    string originalTitle = form.Text;
                    form.Text = $"{originalTitle} - {message}";

                    // Восстанавливаем заголовок через указанное время
                    var timer = new System.Timers.Timer(displayTimeMs);
                    timer.Elapsed += (s, e) =>
                    {
                        timer.Stop();
                        timer.Dispose();

                        if (form.InvokeRequired)
                        {
                            form.Invoke(new Action(() => form.Text = originalTitle));
                        }
                        else
                        {
                            form.Text = originalTitle;
                        }
                    };
                    timer.Start();
                }
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, "Ошибка при отображении статуса операции");
            }
        }
    }
}

