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
                await LoadWorkDivisions(ct, loadRelatedData: _lastFocusedAnnId <= 0);
                // После загрузки восстановим фокус, если есть сохраненный AnnID
                if (_lastFocusedAnnId > 0)
                {
                    await RestoreFocusAsync(_lastFocusedAnnId);
                }
                InitializeThreadNormsButton();
                InitHeaderButtonTags();
                InitializeMainBaseNodeActions();

                // Инициализируем переменную состояния кнопки "показать все"
                var showAllButton = FindButtonByTag(layoutControlGroup14, "bind:show-all");
                if (showAllButton != null)
                {
                    showAllWD = showAllButton.Checked;
                }

                // Инициализируем объект управления кнопкой "bind:unlink"
                ButtonUnbindWd = new ButtonUnbindWd(layoutControlGroup14, "bind:unlink");
                RefreshCurrentWorksUxState();
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

            // При переключении вкладок не перезагружаем источники повторно:
            // просто останавливаем незавершенные операции.
            CancelCurrentLoad();
            var token = StartNewLoadToken();

            try
            {
                await RefreshMainTabAsync(e.Page.Name, token);
            }
            catch (OperationCanceledException) when (token.IsCancellationRequested)
            {
                // Более новая смена вкладки отменила текущее обновление.
            }
        }

        private async void XtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == null || xtraTabControl1?.SelectedTabPage?.Name != "xtraTabPageArticles") return;

            CancelCurrentLoad();
            var token = StartNewLoadToken();

            try
            {
                await RefreshArticlesSubTabAsync(e.Page.Name, token);
            }
            catch (OperationCanceledException) when (token.IsCancellationRequested)
            {
                // Более новая смена вложенной вкладки отменила текущее обновление.
            }
        }
    }
}
