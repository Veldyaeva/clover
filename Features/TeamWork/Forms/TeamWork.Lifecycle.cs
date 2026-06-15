using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.Models;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork
    {
        private void CancelAllLoads() => _presenter.CancelAllLoads();

        private void ModeRadio_CheckedChanged(object sender, EventArgs e)
        {
            ModeRadio_CheckedChanged_internal(sender, e);
        }

        private void layoutControlGroup14_CustomButtonChecked(object sender, BaseButtonEventArgs e)
        {
            var button = e.Button as DevExpress.XtraEditors.ButtonPanel.BaseButton;
            if (button != null)
            {
                showAllWD = button.Checked;
                RefreshCurrentWorksUxState();
                loadAllCheckBox_CheckedChanged_Internal(sender, e, button.Checked);
            }
        }

        private void layoutControlGroup14_CustomButtonUnchecked(object sender, BaseButtonEventArgs e)
        {
            var button = e.Button as DevExpress.XtraEditors.ButtonPanel.BaseButton;
            if (button != null)
            {
                showAllWD = button.Checked;
                RefreshCurrentWorksUxState();
                loadAllCheckBox_CheckedChanged_Internal(sender, e, button.Checked);
            }
        }

        private async Task ReloadActiveTabAsync()
        {
            var tabControl = xtraTabControl1;
            if (tabControl == null)
            {
                return;
            }

            var activePage = tabControl.SelectedTabPage;
            if (activePage == null)
            {
                return;
            }

            CancelAllLoads();

            // Начинаем НОВУЮ загрузку -> старый _loadCts отменится и dispose-ится,
            // создастся новый, а мы заберем из него токен
            var ct = StartNewLoadToken();
            try
            {
                await RefreshMainTabAsync(activePage.Name, ct);
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested)
            {
                // Перезагрузка вкладки была отменена более новым действием.
            }
        }

        private async Task RefreshMainTabAsync(string tabName, CancellationToken ct)
        {
            switch (tabName)
            {
                case "TabPage1":
                    await RefreshWorkDivisionsTabPreservingStateAsync(ct);
                    break;
                case "xtraTabPageArticles":
                    await RefreshArticlesTabPreservingStateAsync(ct);
                    break;
            }
        }

        private async Task RefreshArticlesSubTabAsync(string tabName, CancellationToken ct)
        {
            switch (tabName)
            {
                case "xtraTabPageWorkDivisions":
                    await RefreshArticlesTabPreservingStateAsync(ct);
                    break;
                case "xtraTabPage3":
                    await RefreshArchiveTabPreservingStateAsync(ct);
                    break;
            }
        }

        private async Task RefreshWorkDivisionsTabPreservingStateAsync(CancellationToken ct)
        {
            var annGridState = GridViewRefreshStateHelper.Capture(ANNgridView, "AnnID");

            await LoadWorkDivisions(ct, loadRelatedData: false);
            RestoreGridViewRefreshState(ANNgridView, annGridState);

            int restoredAnnId = GridViewRefreshStateHelper.GetFocusedIntValue(ANNgridView, "AnnID");
            if (restoredAnnId > 0)
            {
                await LoadRelatedData(restoredAnnId, ct);
            }
        }

        private async Task RefreshArticlesTabPreservingStateAsync(CancellationToken ct)
        {
            var unboundArtsState = GridViewRefreshStateHelper.Capture(gridView_unboundArts, "kodd_rt");
            var workDivisionsState = GridViewRefreshStateHelper.Capture(gridView_wdToBind, "AnnID");
            var preArchiveState = GridViewRefreshStateHelper.Capture(gridViewPreArch, "AnnID");
            var archiveState = GridViewRefreshStateHelper.Capture(gridViewArch, "AnnID");

            if (_articlesTabInitialized)
            {
                await RefreshCurrentWorksTabAsync(ct);
            }
            else
            {
                await InitializeCurrentWorksTabAsync(ct);
            }

            RestoreGridViewRefreshState(gridView_unboundArts, unboundArtsState);
            RestoreGridViewRefreshState(gridView_wdToBind, workDivisionsState);
            RestoreGridViewRefreshState(gridViewPreArch, preArchiveState);
            RestoreGridViewRefreshState(gridViewArch, archiveState);

            await SeedArticlesDetailsForCurrentSelectionAsync(ct);
        }

        private async Task RefreshArchiveTabPreservingStateAsync(CancellationToken ct)
        {
            var preArchiveState = GridViewRefreshStateHelper.Capture(gridViewPreArch, "AnnID");
            var archiveState = GridViewRefreshStateHelper.Capture(gridViewArch, "AnnID");

            Task preArchTask = PreArchLoad(ct);
            Task archTask = ArchLoad(ct);
            await Task.WhenAll(preArchTask, archTask);

            RestoreGridViewRefreshState(gridViewPreArch, preArchiveState);
            RestoreGridViewRefreshState(gridViewArch, archiveState);
        }

        private void RestoreGridViewRefreshState(GridView gridView, GridViewRefreshState state)
        {
            if (gridView == null || state == null)
            {
                return;
            }

            _isRestoringGridState = true;
            try
            {
                GridViewRefreshStateHelper.Restore(gridView, state);
            }
            finally
            {
                _isRestoringGridState = false;
            }

            if (ReferenceEquals(gridView, ANNgridView))
            {
                ApplyAnnGridKitSearchFilter(gridView);
            }
        }

        private async void xtraTabControl1_CustomHeaderButtonClick(object sender, DevExpress.XtraTab.ViewInfo.CustomHeaderButtonEventArgs e)
        {
            try
            {
                await ReloadActiveTabAsync();
            }
            catch (OperationCanceledException)
            {
                // проглатываем отмену как ожидаемый сценарий
            }
            catch (Exception ex)
            {
                if (_logger != null)
                {
                    await _logger.LogErrorAsync(ex, "Ошибка при обновлении данных по кнопке вкладки");
                }
                MessageBox.Show($"Не удалось обновить данные: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void customSimpleButtonAnnLog_Click(object sender, EventArgs e) =>
            OpenLogForm(LogSourceType.Ann);

        private void customSimpleButtonRaszLog_Click(object sender, EventArgs e) =>
            OpenLogForm(LogSourceType.Rasz);

        private void OpenLogForm(LogSourceType sourceType)
        {
            try
            {
                int annId = 0;
                if (ANNgridView != null && ANNgridView.FocusedRowHandle >= 0)
                {
                    var row = ANNgridView.GetRow(ANNgridView.FocusedRowHandle) as ArtNormN;
                    annId = row?.AnnID ?? 0;
                }

                var logForm = annId > 0 ? new Log(annId, sourceType) : new Log();
                logForm.StartPosition = FormStartPosition.CenterParent;
                logForm.Show(this);
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, $"Ошибка при открытии журнала {sourceType}");
                MessageBox.Show($"Не удалось открыть журнал: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private CancellationToken StartNewLoadToken() => _presenter.StartNewLoad();

        private void CancelCurrentLoad() => _presenter.CancelCurrentLoad();
    }
}
