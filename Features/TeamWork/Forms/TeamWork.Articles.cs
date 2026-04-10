using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.helpers;
using SewingProduction.Helpers;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork
    {
        // Вторая вкладка — "Работа с артикулами"

        private bool _isUnchecking = false;

        private async Task WithArticlesFocusedRowHandlersSuspendedAsync(Func<Task> action)
        {
            if (action == null)
            {
                return;
            }

            if (gridView_unboundArts != null)
            {
                gridView_unboundArts.FocusedRowChanged -= gridView_unboundArts_FocusedRowChanged;
            }

            if (gridView_wdToBind != null)
            {
                gridView_wdToBind.FocusedRowChanged -= gridViewWdToBind_FocusedRowChanged;
            }

            try
            {
                await action();
            }
            finally
            {
                if (gridView_unboundArts != null)
                {
                    gridView_unboundArts.FocusedRowChanged += gridView_unboundArts_FocusedRowChanged;
                }

                if (gridView_wdToBind != null)
                {
                    gridView_wdToBind.FocusedRowChanged += gridViewWdToBind_FocusedRowChanged;
                }
            }
        }

        private Task CurrentWorks_Load(CancellationToken cancellationToken = default)
        {
            return InitializeCurrentWorksTabAsync(cancellationToken);
        }

        private async Task InitializeCurrentWorksTabAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            showAllWD = false;
            var showAllButton = FindButtonByTag(layoutControlGroup14, "bind:show-all");
            if (showAllButton != null && showAllButton.Checked)
            {
                showAllButton.Checked = false;
            }

            await WithArticlesFocusedRowHandlersSuspendedAsync(async () =>
            {
                await LoadCurrentWorksArchiveDataAsync(cancellationToken);
                await MyDataArtLoad(cancellationToken);

                EnsureFocusedUnboundArtRowSelected();
                string focusedArticul = GetFocusedUnboundArticul();
                int focusedKod = GetFocusedUnboundArtKod();
                if (!string.IsNullOrWhiteSpace(focusedArticul) || focusedKod > 0)
                {
                    await RefreshArticlesWorkDivisionsByArticulAsync(focusedArticul, clearWdFilters: false, cancellationToken);
                }
                else
                {
                    await ClearUnboundArtsRelatedData();
                }

                TWGridHelper.sortGridView(normRaszTab);
                await SeedArticlesDetailsForCurrentSelectionAsync(cancellationToken);

                RefreshCurrentWorksUxState();
                _articlesTabInitialized = true;
            });
        }

        private async Task RefreshCurrentWorksTabAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string focusedArticul = GetFocusedUnboundArticul();
            int focusedKod = GetFocusedUnboundArtKod();

            await WithArticlesFocusedRowHandlersSuspendedAsync(async () =>
            {
                await LoadCurrentWorksArchiveDataAsync(cancellationToken);
                await MyDataArtLoad(cancellationToken);

                if (!string.IsNullOrWhiteSpace(focusedArticul) || focusedKod > 0)
                {
                    await RefreshArticlesWorkDivisionsByArticulAsync(focusedArticul, clearWdFilters: false, cancellationToken);
                }
                else
                {
                    await MyDataAnnLoad(cancellationToken);
                }

                TWGridHelper.sortGridView(normRaszTab);
                RefreshCurrentWorksUxState();
                _articlesTabInitialized = true;
            });
        }

        private async Task LoadCurrentWorksArchiveDataAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Task preArchTask = PreArchLoad(cancellationToken);
            Task archTask = ArchLoad(cancellationToken);
            await Task.WhenAll(preArchTask, archTask);
        }

        private string GetFocusedUnboundArticul()
        {
            if (gridView_unboundArts?.FocusedRowHandle < 0)
            {
                return string.Empty;
            }

            return StringNormalizer.TrimEndOrEmpty(
                CommonFunctions.GetRowCellValueOrDefault<string>(gridView_unboundArts, gridView_unboundArts.FocusedRowHandle, "Articul", string.Empty),
                ' ');
        }

        private int GetFocusedUnboundArtKod()
        {
            if (gridView_unboundArts?.FocusedRowHandle < 0)
            {
                return 0;
            }

            string kod = CommonFunctions.GetRowCellValueOrDefault<string>(gridView_unboundArts, gridView_unboundArts.FocusedRowHandle, "kodd_rt", string.Empty);
            return int.TryParse(kod, out int result) ? result : 0;
        }

        private void EnsureFocusedUnboundArtRowSelected()
        {
            if (gridView_unboundArts == null || gridView_unboundArts.DataRowCount <= 0 || gridView_unboundArts.FocusedRowHandle >= 0)
            {
                return;
            }

            int firstVisibleRow = gridView_unboundArts.GetVisibleRowHandle(0);
            if (gridView_unboundArts.IsValidRowHandle(firstVisibleRow))
            {
                gridView_unboundArts.FocusedRowHandle = firstVisibleRow;
            }
        }

        private int GetFocusedWdToBindAnnId()
        {
            if (gridView_wdToBind?.FocusedRowHandle < 0)
            {
                return 0;
            }

            return CommonFunctions.GetRowCellValueOrDefault<int>(gridView_wdToBind, gridView_wdToBind.FocusedRowHandle, "AnnID", 0);
        }

        private async Task RefreshArticlesWorkDivisionsByArticulAsync(string articul, bool clearWdFilters, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (gridView_wdToBind != null && clearWdFilters && !showAllWD)
            {
                gridView_wdToBind.ActiveFilter.Clear();
                gridView_wdToBind.ActiveFilterString = string.Empty;
            }

            string normalizedArticul = StringNormalizer.TrimEndOrEmpty(articul, ' ');
            var recommendedWorksTask = LoadWorksbyArt(normalizedArticul);
            var loadWorksTask = showAllWD ? LoadWorksbyArt(string.Empty) : recommendedWorksTask;

            var list = await loadWorksTask;
            SetRecommendedAnnIds(await recommendedWorksTask);

            ApplyArticlesWorkDivisionList(list);
            FocusFirstRecommendedWorkDivision();
        }

        private void ApplyArticlesWorkDivisionList(IReadOnlyCollection<MyDataANN> list)
        {
            if (_myDataAnnBindingSource != null && _myDataAnnList != null)
            {
                _myDataAnnList.Clear();
                if (list != null && list.Count > 0)
                {
                    _myDataAnnList.RaiseListChangedEvents = false;
                    try
                    {
                        foreach (var item in list)
                        {
                            _myDataAnnList.Add(item);
                        }
                    }
                    finally
                    {
                        _myDataAnnList.RaiseListChangedEvents = true;
                    }
                }

                _myDataAnnBindingSource.ResetBindings(false);
            }
            else
            {
                gridControl_wdToBind.DataSource = list?.ToList();
            }

            gridView_wdToBind?.RefreshData();
            gridControl_wdToBind?.RefreshDataSource();
        }

        private async Task SeedArticlesDetailsForCurrentSelectionAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await RefreshUnboundArtImageAsync(GetFocusedUnboundArtKod());

            int annId = GetFocusedWdToBindAnnId();
            if (annId <= 0)
            {
                await ClearWdToBindRelatedData();
                RefreshCurrentWorksUxState();
                return;
            }

            if (gridView_wdToBind?.GetRow(gridView_wdToBind.FocusedRowHandle) is MyDataANN selectedItem)
            {
                textEdit1.Text = StringNormalizer.TrimEndOrEmpty(selectedItem.mod, ' ');
                textEdit2.Text = StringNormalizer.TrimEndOrEmpty(selectedItem.Articul, ' ');
                textEdit3.Text = StringNormalizer.TrimEndOrEmpty(selectedItem.grup, ' ');
            }

            await LoadGridImage(pictureBox2, annId: annId);
            cancellationToken.ThrowIfCancellationRequested();
            await RefreshNormRaszForArticlesTab(annId, cancellationToken);
            await RefreshNormRaskForArticlesTab(annId, cancellationToken);
            await RefreshNormKontForArticlesTab(annId, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            await LoadNZPForArticlesTab(annId, cancellationToken);
            RefreshCurrentWorksUxState();
        }

        private async Task RefreshUnboundArtImageAsync(int kodInt)
        {
            if (kodInt > 0)
            {
                await LoadGridImage(pictureBox3, kod: kodInt);
                return;
            }

            if (pictureBox3 == null)
            {
                return;
            }

            if (pictureBox3.InvokeRequired)
            {
                pictureBox3.Invoke((MethodInvoker)(() => pictureBox3.Image = null));
            }
            else
            {
                pictureBox3.Image = null;
            }
        }


        private async Task MyDataArtLoad(CancellationToken cancellationToken = default)
        {
            try
            {
                // Проверяем, инициализирован ли список/источник (должны быть в TeamWork.cs)
                if (_myDataArtList == null || _myDataArtBindingSource == null)
                {
                    await _logger.LogErrorAsync(new NullReferenceException("_myDataArtList or _myDataArtBindingSource is null"), "MyDataArtLoad initialization check failed.");
                    MessageBox.Show("Ошибка инициализации списка артикулов.", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                await GridOverlayLoader.LoadListAsync(
                    gridControl_unboundArts,
                    _myDataArtList,
                    _myDataArtBindingSource,
                    async _ => await _articlesQueryService.LoadUnboundArticlesAsync(),
                    cancellationToken);

                await _logger.LogEventAsync($"Загружено {_myDataArtList.Count} записей MyDataART.", "MyDataArtLoad");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных MyDataART: {ex.Message}");
                // Отображаем сообщение только если инициализация прошла успешно
                if (_myDataArtList != null && _myDataArtBindingSource != null)
                {
                    MessageBox.Show($"Ошибка загрузки данных артикулов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task MyDataAnnLoad(CancellationToken cancellationToken = default)
        {
            try
            {
                // Проверяем, инициализирован ли список/источник (должны быть в TeamWork.cs)
                if (_myDataAnnList == null || _myDataAnnBindingSource == null)
                {
                    await _logger.LogErrorAsync(new NullReferenceException("_myDataAnnList or _myDataAnnBindingSource is null"), "MyDataAnnLoad initialization check failed.");
                  //  MessageBox.Show("Ошибка инициализации списка РТ.", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool loadAll = FindButtonByTag(layoutControlGroup14, "bind:show-all").Checked;
                //loadAll = layoutControlGroup14.CustomHeaderButtons[6].Properties.Checked;
                await GridOverlayLoader.LoadListAsync(
                    gridControl_wdToBind,
                    _myDataAnnList,
                    _myDataAnnBindingSource,
                    async _ =>
                    {
                        return await _articlesQueryService.LoadCurrentWorkDivisionsAsync(loadAll);
                    },
                    cancellationToken);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных MyDataANN: {ex.Message}");
                Debug.WriteLine("1." + ex.Message);
            }
        }


        /// <summary>
        /// Загружает список разделений труда (РТ) для указанного артикула или кода.
        /// </summary>
        /// <param name="kod">Код артикула</param>
        /// <param name="articul">Название артикула</param>
        /// <returns>Список разделений труда (BindingList&lt;MyDataANN&gt;)</returns>
        private async Task<List<MyDataANN>> LoadWorksbyArt(string articul)
        {
            try
            {
                return await _articlesQueryService.LoadWorkDivisionsByArticulAsync(articul);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при загрузке РТ для артикула {articul}");
                return new List<MyDataANN>();
            }
        }


        /// <summary>
        /// Обрабатывает событие смены строки в РТ для увязки(вкладка артикулы)
        /// загрузка norm_rasz и связанных данных (НЗП, картинка).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void gridViewWdToBind_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (_isRestoringGridState)
            {
                return;
            }

            var view = sender as GridView;// gridView_wdToBind; 
            if (view == null)
            {
                await ClearWdToBindRelatedData();
                RefreshCurrentWorksUxState();
                return;
            }

            RefreshCurrentWorksUxState();

            //// Отменяем предыдущие операции загрузки для Articles tab
            //var oldCts = Interlocked.Exchange(ref _loadCts, new CancellationTokenSource());
            //oldCts?.Cancel();
            //oldCts?.Dispose();
            //var token = _loadCts.Token;
            var token = StartNewLoadToken();

            try
            {
                int annId = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "AnnID", 0);
                if (e.FocusedRowHandle < 0 || annId <= 0)
                {
                    await ClearWdToBindRelatedData();
                    RefreshCurrentWorksUxState();
                    return;
                }

                await SeedArticlesDetailsForCurrentSelectionAsync(token);
            }
            catch (OperationCanceledException)
            {
                // Тихо игнорируем отмену операции
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при смене выбранной строки в gridViewWdToBind (RowHandle: {e.FocusedRowHandle})");
                // В случае ошибки очищаем данные
                await ClearWdToBindRelatedData();
                RefreshCurrentWorksUxState();
            }
        }

        /// <summary>
        /// Загружает и обновляет NormRasz данные для вкладки "Артикулы" (customGridControl3).
        /// </summary>
        /// <param name="annId">AnnID для загрузки NormRasz.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        private async Task RefreshNormRaszForArticlesTab(int annId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // Проверяем инициализацию
            if (_normRaszListArticles == null || _normRaszBindingSourceArticles == null)
            {
                await _logger.LogErrorAsync(new NullReferenceException("_normRaszListArticles or _normRaszBindingSourceArticles is null"), "RefreshNormRaszForArticlesTab failed initialization check.");
                return;
            }
            await GridOverlayLoader.LoadListAsync(
                customGridControl3,
                _normRaszListArticles,
                _normRaszBindingSourceArticles,
                async token => await _articlesQueryService.LoadNormRaszAsync(annId, token),
                cancellationToken);
        }

        private async Task RefreshNormRaskForArticlesTab(int annId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // Проверяем инициализацию
            if (_normRaskListArticles == null || _normRaskBindingSourceArticles == null)
            {
                await _logger.LogErrorAsync(new NullReferenceException("_normRaskListArticles or _normRaskBindingSourceArticles is null"), "RefreshNormRaskForArticlesTab failed initialization check.");
                return;
            }
            await GridOverlayLoader.LoadListAsync(
                customGridControl2,
                _normRaskListArticles,
                _normRaskBindingSourceArticles,
                async token => await _articlesQueryService.LoadNormRaskAsync(annId, token),
                cancellationToken);
        }

    private async Task RefreshNormKontForArticlesTab(int annId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        // Проверяем инициализацию
        if (_normKontListArticles == null || _normKontBindingSourceArticles == null)
        {
            await _logger.LogErrorAsync(new NullReferenceException("_normKontListArticles or _normKontBindingSourceArticles is null"), "RefreshNormKontForArticlesTab failed initialization check.");
            return;
        }
        await GridOverlayLoader.LoadListAsync(
            customGridControl1,
            _normKontListArticles,
            _normKontBindingSourceArticles,
            async token => await _articlesQueryService.LoadNormKontAsync(annId, token),
            cancellationToken);
    }

        /// <summary>
        /// Загружает данные НЗП для вкладки "Артикулы".
        /// </summary>
        /// <param name="annId">AnnID для загрузки НЗП.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        private async Task LoadNZPForArticlesTab(int annId, CancellationToken cancellationToken = default)
        {
            var sw = Stopwatch.StartNew();
            cancellationToken.ThrowIfCancellationRequested();

            // Проверяем инициализацию
            if (_nzpListArt == null || _nzpByKoddRtSourceArt == null)
            {
                await _logger.LogErrorAsync(new NullReferenceException("_nzpListArt or _nzpByKoddRtSourceArt is null"), "LoadNZPForArticlesTab failed initialization check.");
                return;
            }
            var swDb = Stopwatch.StartNew();
            await GridOverlayLoader.LoadListAsync(
                gridControlNZP,
                _nzpListArt,
                _nzpByKoddRtSourceArt,
                async token => await _articlesQueryService.LoadNzpAsync(annId, token),
                cancellationToken); 
            Debug.WriteLine($"LoadNZPForArticlesTab DB/load: {swDb.ElapsedMilliseconds} ms");

            gridControlNZP?.RefreshDataSource(); 
            Debug.WriteLine($"LoadNZPForArticlesTab total: {sw.ElapsedMilliseconds} ms");

            await UpdateUnboundButtonStatusBasedOnNZP(); // Обновить состояние кнопки
        }

        private async Task PreArchLoad(CancellationToken cancellationToken = default)
        {
            try
            {
                string query = "SELECT * FROM artNormNView WHERE status = 4";
                if (_preArchList == null || _preArchBindingSource == null)
                {
                    await _logger.LogErrorAsync(new NullReferenceException("_preArchList or _preArchBindingSource is null"), "PreArchLoad failed initialization check.");
                    MessageBox.Show("Ошибка инициализации списка предварительного архива.", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                await GridOverlayLoader.LoadListAsync(
                    gridControlPreArch,
                    _preArchList,
                    _preArchBindingSource,
                    async _ => await _articlesQueryService.LoadPreArchiveAsync(),
                    cancellationToken);

                await _logger.LogEventAsync($"Загружено {_preArchList.Count} записей в предварительный архив.", "PreArchLoad");

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных в предварительный архив: {ex.Message}");
                if (_preArchList != null && _preArchBindingSource != null)
                {
                    MessageBox.Show($"Ошибка загрузки данных предварительного архива: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Загружает данные в архив (gridViewArch) - записи со статусом 3 из artNormNView
        /// </summary>
        private async Task ArchLoad(CancellationToken cancellationToken = default)
        {
            try
            {
                string query = "SELECT * FROM artNormNView WHERE status = 3";
                if (_archList == null || _archBindingSource == null)
                {
                    await _logger.LogErrorAsync(new NullReferenceException("_archList or _archBindingSource is null"), "ArchLoad failed initialization check.");
                    MessageBox.Show("Ошибка инициализации списка архива.", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                await GridOverlayLoader.LoadListAsync(
                    gridControlArch,
                    _archList,
                    _archBindingSource,
                    async _ =>
                    {
                        return await _articlesQueryService.LoadArchiveAsync();
                    },
                    cancellationToken);

                await _logger.LogEventAsync($"Загружено {_archList.Count} записей в архив.", "ArchLoad");

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных в архив: {ex.Message}");
                if (_archList != null && _archBindingSource != null)
                {
                    MessageBox.Show($"Ошибка загрузки данных архива: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Обновляет данные архива после изменения статусов РТ
        /// </summary>
        private async Task RefreshArchData()
        {
            try
            {
                await ArchLoad();
                gridControlArch?.RefreshDataSource();
                await _logger.LogEventAsync("RefreshArchData: Данные архива обновлены", "RefreshData");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при обновлении данных архива");
            }
        }

        /// <summary>
        /// Привязывает выбранные артикулы к выбранному разделению труда (РТ).
        /// </summary>
        private async Task BindButton_Click_Internal(object sender, EventArgs e)
        {
            gridView_unboundArts.CloseEditor();
            gridView_unboundArts.UpdateCurrentRow();
            gridView_wdToBind.CloseEditor();
            gridView_wdToBind.UpdateCurrentRow();
            try
            {
                GridView artView = gridView_unboundArts;
                GridView annView = gridView_wdToBind;

                if (artView == null || annView == null)
                {
                    MessageBox.Show("Данные не загружены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                MyDataART selectedArtRow = null;
                MyDataANN selectedAnnRow = null;
                // Получаем датасорсы
                var artBindingSource = artView.DataSource as BindingSource;
                var artDataSource = artBindingSource?.DataSource as BindingList<MyDataART>;

                //// Получаем выбранное разделение труда
                var annBindingSource = _myDataAnnBindingSource;
                var annDataSource = annBindingSource?.DataSource as BindingList<MyDataANN>;
                selectedArtRow = artDataSource?.FirstOrDefault(r => r.IsChecked);
                selectedAnnRow = annDataSource?.FirstOrDefault(r => r.IsChecked);

                // Проверяем, выбраны ли оба элемента
                if (selectedArtRow is null || selectedAnnRow is null)
                {
                    MessageBox.Show("Выберите артикул и разделение труда для увязки!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // Создаем диалог подтверждения с опциями заполнения
                var confirmDialog = new Form()
                {
                    Text = "Подтверждение увязки",
                    Size = new Size(450, 250),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                var label = new Label()
                {
                    Text = $"Вы действительно хотите увязать артикул {StringNormalizer.TrimEndOrEmpty(selectedArtRow.Articul)} с разделением труда {StringNormalizer.TrimEndOrEmpty(selectedAnnRow.Articul)}?",
                    Location = new Point(20, 20),
                    Size = new Size(400, 40),
                    TextAlign = ContentAlignment.TopLeft
                };

                var fillGroupCheckBox = new CheckBox()
                {
                    Text = "Заполнить группу из справочника артикулов",
                    Location = new Point(20, 70),
                    Size = new Size(350, 20),
                    Checked = string.IsNullOrEmpty(selectedAnnRow.grup) // Автоматически отмечаем, если группа пустая
                };

                var fillModelCheckBox = new CheckBox()
                {
                    Text = "Заполнить модель из справочника артикулов",
                    Location = new Point(20, 100),
                    Size = new Size(350, 20),
                    Checked = string.IsNullOrEmpty(selectedAnnRow.mod) // Автоматически отмечаем, если модель пустая
                };

                var okButton = new System.Windows.Forms.Button()
                {
                    Text = "Да",
                    Location = new Point(270, 150),
                    Size = new Size(75, 25),
                    DialogResult = DialogResult.OK
                };

                var cancelButton = new System.Windows.Forms.Button()
                {
                    Text = "Отмена",
                    Location = new Point(355, 150),
                    Size = new Size(75, 25),
                    DialogResult = DialogResult.Cancel
                };

                confirmDialog.Controls.AddRange(new Control[] { label, fillGroupCheckBox, fillModelCheckBox, okButton, cancelButton });
                confirmDialog.AcceptButton = okButton;
                confirmDialog.CancelButton = cancelButton;

                if (confirmDialog.ShowDialog() != DialogResult.OK) return;

                // Заполняем группу и модель в зависимости от выбора пользователя
                if (fillGroupCheckBox.Checked)// && string.IsNullOrEmpty(selectedAnnRow.grup))
                {
                    selectedAnnRow.grup = selectedArtRow.grup;
                }
                if (fillModelCheckBox.Checked)// && string.IsNullOrEmpty(selectedAnnRow.mod))
                {
                    selectedAnnRow.mod = selectedArtRow.mod;
                }
                selectedAnnRow.size_label = selectedArtRow.size_label;
                // Выполняем команду привязки через application-layer orchestrator
                var bindResult = await _teamWorkService.BindArticleAsync(selectedAnnRow, selectedArtRow);
                if (!bindResult.Success)
                {
                    MessageBox.Show(bindResult.Error ?? "Ошибка при привязке артикула.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                selectedArtRow.BindedArt = selectedAnnRow.Articul;//заполняем в артикуле из РТ
                // Обновляем UI:
                if (artDataSource != null && selectedArtRow != null)
                {
                    // 0. Присваиваем привязанному артикулу артикул разделений
                    selectedArtRow.BindedArt = selectedAnnRow.Articul;
                    // 1. Добавляем привязанный артикул в список для gridView1
                    _boundArtList?.Add(selectedArtRow);

                    // 2. Удаляем артикул из списка доступных для gridView7
                    artDataSource.Remove(selectedArtRow);
                    RefreshCurrentWorksUxState();
                }
                else
                {
                    artView.RefreshData();
                    annView.RefreshData();
                    // Обновим и gridView12 на всякий случай
                    gridControl_binded?.RefreshDataSource();
                    gridControl_wdToBind?.RefreshDataSource();
                }
                MessageBox.Show("Привязка успешно выполнена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при привязке артикула к РТ");
                MessageBox.Show($"Ошибка при привязке артикула: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обрабатывает смену строки в таблице неувязанных артикулов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void gridView_unboundArts_FocusedRowChanged_Internal(object sender, FocusedRowChangedEventArgs e)
        {
            var gv_unbound_Arts = sender as GridView;
            if (gv_unbound_Arts == null)
            {
                await ClearUnboundArtsRelatedData();
                RefreshCurrentWorksUxState();
                return;
            }

            // Если строка не выбрана - очищаем все связанные данные
            RefreshCurrentWorksUxState();

            if (e.FocusedRowHandle < 0)
            {
                await ClearUnboundArtsRelatedData();
                RefreshCurrentWorksUxState();
                return;
            }

            try
            {
                string kod = CommonFunctions.GetRowCellValueOrDefault<string>(gv_unbound_Arts, e.FocusedRowHandle, "kodd_rt", "");
                string articul = StringNormalizer.TrimEndOrEmpty(CommonFunctions.GetRowCellValueOrDefault<string>(gv_unbound_Arts, e.FocusedRowHandle, "Articul", ""), ' ');

                if (!int.TryParse(kod, out int kodInt))
                {
                    await _logger.LogWarningAsync($"Не удалось преобразовать Kod '{kod}' в число", "gridView_unboundArts_FocusedRowChanged_Internal");
                    kodInt = 0;
                }

                if (string.IsNullOrEmpty(articul) && kodInt <= 0)
                {
                    await ClearUnboundArtsRelatedData();
                    return;
                }

                await WithArticlesFocusedRowHandlersSuspendedAsync(() =>
                    RefreshArticlesWorkDivisionsByArticulAsync(articul, clearWdFilters: true));
                await SeedArticlesDetailsForCurrentSelectionAsync();
                RefreshCurrentWorksUxState();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка в gridView_unboundArts_FocusedRowChanged_Internal");
                // В случае ошибки очищаем данные
                await ClearUnboundArtsRelatedData();
                RefreshCurrentWorksUxState();
            }
        }

        /// <summary>
        /// Безопасно получает данные из строки gridView_unboundArts
        /// </summary>
        /// <param name="gridView">GridView для получения данных</param>
        /// <param name="rowHandle">Индекс строки</param>
        /// <returns>Объект MyDataART или null если данные недоступны</returns>
        private MyDataART GetSafeUnboundArtData(GridView gridView, int rowHandle)
        {
            try
            {
                if (gridView == null)
                {
                    _logger?.LogWarningAsync("GridView is null в GetSafeUnboundArtData", "GetSafeUnboundArtData");
                    return null;
                }

                if (rowHandle < 0 || rowHandle >= gridView.DataRowCount)
                {
                    _logger?.LogWarningAsync($"Invalid rowHandle {rowHandle} в GetSafeUnboundArtData. DataRowCount: {gridView.DataRowCount}", "GetSafeUnboundArtData");
                    return null;
                }

                var data = gridView.GetRow(rowHandle) as MyDataART;
                if (data == null)
                {
                    _logger?.LogWarningAsync($"Не удалось получить MyDataART для строки {rowHandle}", "GetSafeUnboundArtData");
                    return null;
                }

                return data;
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, $"Ошибка при получении данных для строки {rowHandle} в GetSafeUnboundArtData");
                return null;
            }
        }

        /// <summary>
        /// Проверяет валидность данных gridView_unboundArts
        /// </summary>
        /// <returns>True если gridView содержит валидные данные</returns>
        private bool IsUnboundArtsGridValid()
        {
            return gridView_unboundArts != null &&
                   gridView_unboundArts.DataRowCount > 0 &&
                   gridView_unboundArts.FocusedRowHandle >= 0 &&
                   gridView_unboundArts.FocusedRowHandle < gridView_unboundArts.DataRowCount;
        }

        /// <summary>
        /// Очищает все связанные данные для неувязанных артикулов
        /// </summary>
        private async Task ClearUnboundArtsRelatedData()
        {
            try
            {
                // Очищаем список РТ
                if (_myDataAnnBindingSource != null && _myDataAnnList != null)
                {
                    _myDataAnnList.Clear();
                    _myDataAnnBindingSource.ResetBindings(false);
                }

                // Обновляем UI
                gridView_wdToBind?.RefreshData();
                gridControl_wdToBind?.RefreshDataSource();
                SetRecommendedAnnIds(Array.Empty<MyDataANN>());

                // Очищаем норм расценки
                if (_normRaszListArticles != null && _normRaszBindingSourceArticles != null)
                {
                    _normRaszListArticles.Clear();
                    _normRaszBindingSourceArticles.ResetBindings(false);
                }

                // Очищаем норм раскроя
                if (_normRaskListArticles != null && _normRaskBindingSourceArticles != null)
                {
                    _normRaskListArticles.Clear();
                    _normRaskBindingSourceArticles.ResetBindings(false);
                }

                if (_normKontListArticles != null && _normKontBindingSourceArticles != null)
                {
                    _normKontListArticles.Clear();
                    _normKontBindingSourceArticles.ResetBindings(false);
                }

                // Очищаем картинку
                if (pictureBox3 != null)
                {
                    if (pictureBox3.InvokeRequired)
                    {
                        pictureBox3.Invoke((MethodInvoker)(() => pictureBox3.Image = null));
                    }
                    else
                    {
                        pictureBox3.Image = null;
                    }
                }

                RefreshCurrentWorksUxState();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при очистке связанных данных для неувязанных артикулов");
            }
        }

        /// <summary>
        /// Очищает все связанные данные для РТ увязки
        /// </summary>
        private async Task ClearWdToBindRelatedData()
        {
            try
            {
                await _logger.LogEventAsync("ClearWdToBindRelatedData: Starting to clear data", "ClearWdToBindRelatedData");

                // Очищаем картинку
                if (pictureBox2 != null)
                {
                    if (pictureBox2.InvokeRequired)
                    {
                        pictureBox2.Invoke((MethodInvoker)(() => pictureBox2.Image = null));
                    }
                    else
                    {
                        pictureBox2.Image = null;
                    }
                }

                // Очищаем норм расценки
                if (_normRaszListArticles != null && _normRaszBindingSourceArticles != null)
                {
                    _normRaszListArticles.Clear();
                    _normRaszBindingSourceArticles.ResetBindings(false);
                    await _logger.LogEventAsync("ClearWdToBindRelatedData: Cleared _normRaszListArticles", "ClearWdToBindRelatedData");
                }

                // Очищаем норм раскроя
                if (_normRaskListArticles != null && _normRaskBindingSourceArticles != null)
                {
                    _normRaskListArticles.Clear();
                    _normRaskBindingSourceArticles.ResetBindings(false);
                    await _logger.LogEventAsync("ClearWdToBindRelatedData: Cleared _normRaskListArticles", "ClearWdToBindRelatedData");
                }

                if (_normKontListArticles != null && _normKontBindingSourceArticles != null)
                {
                    _normKontListArticles.Clear();
                    _normKontBindingSourceArticles.ResetBindings(false);
                    await _logger.LogEventAsync("ClearWdToBindRelatedData: Cleared _normKontListArticles", "ClearWdToBindRelatedData");
                }

                // Очищаем НЗП
                if (_nzpListArt != null && _nzpByKoddRtSourceArt != null)
                {
                    _nzpListArt.Clear();
                    _nzpByKoddRtSourceArt.ResetBindings(false);
                }

                // Обновляем UI
                gridControlNZP?.RefreshDataSource();
                RefreshCurrentWorksUxState();
                await UpdateUnboundButtonStatusBasedOnNZP(); // Обновить состояние кнопки
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при очистке связанных данных для РТ увязки");
            }
        }

        /// <summary>
        /// Возвращает выбранные записи из архива в актуальные (изменяет статус с 3 на 2)
        /// </summary>
        private async Task RestoreFromArchive_Internal(object sender, EventArgs e)
        {
            try
            {
                await _logger.LogEventAsync("Начало процесса восстановления записей из архива", "RestoreFromArchive_Internal");
                // Находим gridViewArch
                GridView archiveGridView = null;
                if (gridControlArch?.MainView is GridView archView)
                {
                    archiveGridView = archView;
                }

                if (archiveGridView == null)
                {
                    MessageBox.Show("Грид архива не инициализирован.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await _logger.LogErrorAsync(new Exception("Грид архива не инициализирован"), "RestoreFromArchive_Internal");
                    return;
                }

                // Получаем выбранные строки
                var selectedRowHandles = archiveGridView.GetSelectedRows();

                // Если нет выбранных строк, берем текущую строку
                if (selectedRowHandles == null || selectedRowHandles.Length == 0)
                {
                    if (archiveGridView.FocusedRowHandle < 0)
                    {
                        MessageBox.Show("Выберите записи для восстановления из архива.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        await _logger.LogWarningAsync("Попытка восстановления из архива без выбора строк", "RestoreFromArchive_Internal");
                        return;
                    }
                    selectedRowHandles = new int[] { archiveGridView.FocusedRowHandle };
                }

                var selectedItems = new List<ArtNormN>();

                // Собираем данные выбранных строк
                foreach (int rowHandle in selectedRowHandles)
                {
                    if (rowHandle >= 0)
                    {
                        var item = archiveGridView.GetRow(rowHandle) as ArtNormN;
                        if (item != null)
                        {
                            selectedItems.Add(item);
                        }
                    }
                }

                if (selectedItems.Count == 0)
                {
                    MessageBox.Show("Не найдено записей для восстановления из архива.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    await _logger.LogWarningAsync("Не найдено записей для восстановления из архива после сбора выбранных строк", "RestoreFromArchive_Internal");
                    return;
                }

                // Подтверждение операции
                string message = selectedItems.Count == 1
                    ? $"Восстановить запись из архива:\n{selectedItems[0].Articul} - {selectedItems[0].Mod}?"
                    : $"Восстановить {selectedItems.Count} записей из архива?";

                var result = MessageBox.Show(
                    message,
                    "Подтверждение восстановления из архива",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (result != DialogResult.Yes)
                    return;

                int successCount = 0;
                var errors = new List<string>();

                archiveGridView.BeginUpdate();
                try
                {
                    var candidates = selectedItems.Where(i => i.Status == (int)Status.Archive).ToList();
                    foreach (var skipped in selectedItems.Where(i => i.Status != (int)Status.Archive))
                    {
                        await _logger.LogEventAsync($"Запись AnnID: {skipped.AnnID} не находится в архиве (статус: {skipped.Status})", "RestoreFromArchive");
                    }

                    var batch = await _teamWorkService.RestoreWorkDivisionsFromArchiveAsync(candidates.Select(x => x.AnnID));
                    errors.AddRange(batch.Errors);
                    successCount = batch.UpdatedAnnIds.Count;

                    foreach (var item in selectedItems)
                    {
                        if (batch.UpdatedAnnIds.Contains(item.AnnID))
                        {
                            item.Status = (int)Status.Preliminary;
                            item.StatusText = StatusHelper.GetStatusText((int)Status.Preliminary);

                            // Обновляем строку в гриде
                            TryRefreshRowByAnnId(archiveGridView, item.AnnID);

                            await _logger.LogEventAsync($"Статус записи AnnID: {item.AnnID} изменен с 'Архивное' на 'Актуальное'", "RestoreFromArchive");
                        }
                    }
                }
                finally
                {
                    archiveGridView.EndUpdate();
                }

                // Обновляем привязку данных и удаляем восстановленные записи из архива
                if (_archBindingSource != null && _archList != null)
                {
                    // Удаляем восстановленные записи из списка архива
                    foreach (var item in selectedItems.Where(i => i.Status == (int)Status.Preliminary))
                    {
                        _archList.Remove(item);
                    }
                    _archBindingSource.ResetBindings(false);
                }

                // Обновляем основной грид с разделениями труда
                if (_bindingSource != null && _bindingList != null)
                {
                    // Добавляем восстановленные записи в основной список
                    foreach (var item in selectedItems.Where(i => i.Status == (int)Status.Preliminary))
                    {
                        if (!_bindingList.Any(b => b.AnnID == item.AnnID))
                        {
                            _bindingList.Add(item);
                        }
                    }
                    _bindingSource.ResetBindings(false);
                    ANNgridControl?.RefreshDataSource();
                }

                // Показываем результат операции
                if (errors.Count == 0)
                {
                    string successMessage = successCount == 1
                        ? "Запись успешно восстановлена из архива."
                        : $"Успешно восстановлено {successCount} записей из архива.";

                    MessageBox.Show(successMessage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await _logger.LogEventAsync(successMessage, "RestoreFromArchive_Internal");
                }
                else
                {
                    string errorMessage = $"Восстановлено: {successCount} записей.\nОшибки:\n" + string.Join("\n", errors);
                    MessageBox.Show(errorMessage, "Результат восстановления", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    await _logger.LogWarningAsync(errorMessage, "RestoreFromArchive_Internal");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при выполнении восстановления записей из архива");
                MessageBox.Show($"Ошибка при восстановлении из архива: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}



