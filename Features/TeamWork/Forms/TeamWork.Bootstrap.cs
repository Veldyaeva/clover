using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Helpers;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork
    {
        private int _lastFocusedAnnId = 0;

        private async void TeamWorkForm_Load(object sender, EventArgs e)
        {
            if (ANNgridView != null)
            {
                ANNgridView.FocusedRowChanged -= ANNgridView_FocusedRowChanged;
                ANNgridView.ColumnFilterChanged -= ANNgridView_ActiveFilterChanged;
            }

            // Загружаем сохраненные настройки интерфейса
            LoadGridSettings();

            try
            {
                if (gridControl_unboundArts == null || gridControl_wdToBind == null || ANNgridControl == null)
                {
                    throw new InvalidOperationException("Критические компоненты формы не инициализированы.");
                }
                // создаем первый CTS для начальной загрузки,
                // чтобы его можно было отменить при закрытии формы / смене вкладки
                var ct = StartNewLoadToken();
                await LoadWorkDivisions(ct);
                // После загрузки восстановим фокус, если есть сохраненный AnnID
                if (_lastFocusedAnnId > 0)
                {
                    await RestoreFocusAsync(_lastFocusedAnnId);
                }
                InitHeaderButtonTags();

                // Инициализируем переменную состояния кнопки "показать все"
                var showAllButton = FindButtonByTag(layoutControlGroup14, "bind:show-all");
                if (showAllButton != null)
                {
                    showAllWD = showAllButton.Checked;
                }

                // Инициализируем объект управления кнопкой "bind:unlink"
                ButtonUnbindWd = new ButtonUnbindWd(layoutControlGroup14, "bind:unlink");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке формы TeamWork");
                MessageBox.Show($"Ошибка при инициализации формы TeamWork: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (ANNgridView != null)
                {
                    ANNgridView.FocusedRowChanged += ANNgridView_FocusedRowChanged;

                    // Фокус и связанные данные при фильтрации/поиске - после применения фильтра в колонках/панели поиска
                    ANNgridView.ColumnFilterChanged -= ANNgridView_ActiveFilterChanged;
                    ANNgridView.ColumnFilterChanged += ANNgridView_ActiveFilterChanged;

                    // Включаем подсветку строк
                    ApplyAnnGridRowStyling();
                }

                // Устанавливаем начальный режим (обычный)
                SetNormalMode();
            }
        }

        /// <summary>
        /// Если видна расширенная кнопка редактирования - скрываем обычную
        /// Если расширенная скрыта - показываем обычную при наличии прав
        /// </summary>
        private void ButtonEditOnlyAdv_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                if (ButtonEditOnlyAdv == null || ButtonEditWd == null) return;
                if (ButtonEditOnlyAdv.Visible)
                {
                    ButtonEditWd.VisibleLogic = false;
                }
                else if (ButtonEditWd.VisiblePermission)
                {
                    ButtonEditWd.VisibleLogic = true;
                }
            }
            catch (Exception ex)
            {
                _ = _logger.LogErrorAsync(ex, "ButtonEditOnlyAdv_VisibleChanged");
            }
        }

        private async void XtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == null) return;

            // Отменяем все активные загрузки на предыдущей вкладке
            CancelAllLoads();
            // для новой вкладки создаем НОВЫЙ токен
            var token = StartNewLoadToken();
            switch (e.Page.Name)
            {
                case "TabPage1":
                    await LoadWorkDivisions(token);
                    break;

                case "xtraTabPageArticles":
                    // Загружаем данные для вкладки артикулов с токеном отмены
                    await CurrentWorks_Load(token);//_loadCts.Token);
                    break;

                default:
                    // При переходе на другие вкладки можно добавить дополнительную логику если необходимо
                    break;
            }
        }

        /// <summary>
        /// Обрабатывает смену вложенной вкладки в "Текущие работы"
        /// </summary>
        private async void XtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == null) return;

            // Отменяем все активные загрузки на предыдущей вложенной вкладке
            CancelAllLoads();

            try
            {
                switch (e.Page.Name)
                {
                    case "xtraTabPageWorkDivisions":
                        // Когда переходим на вкладку "Требуют увязки", обновляем данные для normRaskArt
                        if (gridView_wdToBind?.RowCount > 0 && gridView_wdToBind.FocusedRowHandle >= 0)
                        {
                            int annId = CommonFunctions.GetRowCellValueOrDefault<int>(gridView_wdToBind, gridView_wdToBind.FocusedRowHandle, "AnnID", 0);
                            if (annId > 0)
                            {
                                await _logger.LogEventAsync($"XtraTabControl2_SelectedPageChanged: Refreshing NormRask data for annId={annId} on xtraTabPageWorkDivisions", "XtraTabControl2_SelectedPageChanged");
                                await RefreshNormRaskForArticlesTab(annId, _loadCts.Token);

                                // Check the grid state after refresh
                                await CheckNormRaskArtState();
                            }
                        }
                        else
                        {
                            await _logger.LogWarningAsync("XtraTabControl2_SelectedPageChanged: No focused row in gridView_wdToBind", "XtraTabControl2_SelectedPageChanged");
                        }
                        break;

                    case "xtraTabPage3":
                        // Можно добавить логику для другой вложенной вкладки если необходимо
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Error in XtraTabControl2_SelectedPageChanged");
            }
        }
    }
}
