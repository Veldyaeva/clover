using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Helpers;
using SewingProduction.Models;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork
    {
        /// <summary>
        /// Принудительно обновляет данные в normRaskArt гриде
        /// </summary>
        public async Task ForceRefreshNormRaskArt()
        {
            try
            {
                if (gridView_wdToBind?.RowCount > 0 && gridView_wdToBind.FocusedRowHandle >= 0)
                {
                    int annId = CommonFunctions.GetRowCellValueOrDefault<int>(gridView_wdToBind, gridView_wdToBind.FocusedRowHandle, "AnnID", 0);
                    if (annId > 0)
                    {
                        await _logger.LogEventAsync($"ForceRefreshNormRaskArt: Refreshing data for annId={annId}", "ForceRefreshNormRaskArt");

                        // вызываем метод из TeamWork.Articles.cs
                        var articlesForm = this as dynamic;
                        if (articlesForm != null)
                        {
                            await articlesForm.RefreshNormRaskForArticlesTab(annId, _presenter.CurrentToken);
                        }
                    }
                }
                else
                {
                    await _logger.LogWarningAsync("ForceRefreshNormRaskArt: No focused row in gridView_wdToBind", "ForceRefreshNormRaskArt");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Error in ForceRefreshNormRaskArt");
            }
        }

        /// <summary>
        /// Проверяет текущее состояние normRaskArt грида
        /// </summary>
        public async Task CheckNormRaskArtState()
        {
            try
            {
                await _logger.LogEventAsync($"CheckNormRaskArtState: Starting grid state check", "CheckNormRaskArtState");

                if (customGridControl2 == null)
                {
                    await _logger.LogWarningAsync("CheckNormRaskArtState: customGridControl2 is null", "CheckNormRaskArtState");
                    return;
                }

                await _logger.LogEventAsync($"CheckNormRaskArtState: customGridControl2.Visible={customGridControl2.Visible}, Enabled={customGridControl2.Enabled}", "CheckNormRaskArtState");

                if (customGridControl2.MainView is GridView gridView)
                {
                    await _logger.LogEventAsync($"CheckNormRaskArtState: GridView.RowCount={gridView.RowCount}, DataRowCount={gridView.DataRowCount}", "CheckNormRaskArtState");
                    await _logger.LogEventAsync($"CheckNormRaskArtState: GridView.DataSource={gridView.GridControl?.DataSource}", "CheckNormRaskArtState");

                    if (gridView.DataSource is BindingSource bindingSource)
                    {
                        await _logger.LogEventAsync($"CheckNormRaskArtState: BindingSource.DataSource={bindingSource.DataSource}, Count={bindingSource.Count}", "CheckNormRaskArtState");

                        if (bindingSource.DataSource is BindingList<NormRask> bindingList)
                        {
                            await _logger.LogEventAsync($"CheckNormRaskArtState: BindingList.Count={bindingList.Count}", "CheckNormRaskArtState");
                            if (bindingList.Count > 0)
                            {
                                var firstItem = bindingList[0];
                                await _logger.LogEventAsync($"CheckNormRaskArtState: First item - kod_o='{firstItem.Kod_o}', text='{firstItem.TextRask}', razryd={firstItem.razryd}, sek={firstItem.Sek}, spec='{firstItem.Spec}', obor='{firstItem.Obor}'", "CheckNormRaskArtState");
                            }
                        }
                    }
                }
                else
                {
                    await _logger.LogWarningAsync("CheckNormRaskArtState: customGridControl2.MainView is not GridView", "CheckNormRaskArtState");
                }

                await _logger.LogEventAsync($"CheckNormRaskArtState: Grid state check completed", "CheckNormRaskArtState");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Error in CheckNormRaskArtState");
            }
        }

        /// <summary>
        /// Принудительно обновляет и проверяет состояние normRaskArt грида
        /// </summary>
        public async Task ForceRefreshAndCheckNormRaskArt()
        {
            try
            {
                await _logger.LogEventAsync($"ForceRefreshAndCheckNormRaskArt: Starting forced refresh and check", "ForceRefreshAndCheckNormRaskArt");

                // First check current state
                await CheckNormRaskArtState();

                // Then force refresh
                await ForceRefreshNormRaskArt();

                // Wait a bit for the refresh to complete
                await Task.Delay(100);

                // Check state again after refresh
                await CheckNormRaskArtState();

                await _logger.LogEventAsync($"ForceRefreshAndCheckNormRaskArt: Completed forced refresh and check", "ForceRefreshAndCheckNormRaskArt");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Error in ForceRefreshAndCheckNormRaskArt");
            }
        }
    }
}
