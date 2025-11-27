using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Helpers;
using SewingProduction.Models;
using System.Diagnostics;
using SewingProduction.Core.helpers;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork
    {
        // Вторая вкладка — "Работа с артикулами"

        private bool _isUnchecking = false;

        /// <summary>
        /// Загрузка вкладки "текущие работы" (MyDataAnn)
        /// </summary>
        private async Task CurrentWorks_Load(CancellationToken cancellationToken = default)
        {

            // Отписываемся от событий ПЕРЕД загрузкой
            if (this.gridView_unboundArts != null)
            {
                this.gridView_unboundArts.FocusedRowChanged -= gridView_unboundArts_FocusedRowChanged;
            }
            if (this.gridView_wdToBind != null)
            {
                this.gridView_wdToBind.FocusedRowChanged -= gridViewWdToBind_FocusedRowChanged;
            }

            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Task preArchTask = PreArchLoad(cancellationToken);
                Task archTask = ArchLoad(cancellationToken);

                // Загружаем основные данные
                await MyDataArtLoad(cancellationToken);
                await MyDataAnnLoad(cancellationToken);

                await Task.WhenAll(preArchTask, archTask);

                TWGridHelper.sortGridView(normRaszTab);
                //TWGridHelper.sortGridView(gridViewRaskr);
                //TWGridHelper.sortGridView(gridViewKont);


                // Load NormRasz and NormRask data for the Articles tab using the dedicated BindingList and BindingSource
                if (this.gridView_wdToBind != null && gridView_wdToBind.RowCount > 0 && gridView_wdToBind.FocusedRowHandle >= 0)
                {
                    int initialAnnId = CommonFunctions.GetRowCellValueOrDefault<int>(gridView_wdToBind, gridView_wdToBind.FocusedRowHandle, "AnnId", 0);
                    List<NormRasz> raszData = new List<NormRasz>();
                    List<NormRask> raskData = new List<NormRask>();
                    if (initialAnnId > 0)
                    {
                        raszData = await _artNormService.GetRelatedNormRasz(initialAnnId);
                        raskData = await _artNormService.GetRelatedNormRask(initialAnnId);
                    }

                    // Load NormRasz data
                    _normRaszListArticles.Clear();
                    if (raszData != null)
                    {
                        foreach (var item in raszData)
                        {
                            _normRaszListArticles.Add(item);
                        }
                    }
                    _normRaszBindingSourceArticles.ResetBindings(false);

                    //// Load NormRask data
                    //_normRaskListArticles.Clear();
                    //if (raskData != null)
                    //{
                    //    foreach (var item in raskData)
                    //    {
                    //        _normRaskListArticles.Add(item);
                    //    }
                    //}
                    //_normRaskBindingSourceArticles.ResetBindings(false);
                }
                else
                {
                    // Если нет выбранных строк в gridView_wdToBind - очищаем все связанные данные
                    if (_normRaszListArticles is not null) _normRaszListArticles.Clear();
                    if (_normRaszBindingSourceArticles is not null) _normRaszBindingSourceArticles.ResetBindings(false);
                    if (_normRaskListArticles is not null) _normRaskListArticles.Clear();
                    if (_normRaskBindingSourceArticles is not null) _normRaskBindingSourceArticles.ResetBindings(false);
                    await ClearWdToBindRelatedData();
                }

                // Проверяем gridView_unboundArts и очищаем данные если нет выбранных строк
                if (this.gridView_unboundArts == null || gridView_unboundArts.RowCount == 0 || gridView_unboundArts.FocusedRowHandle < 0)
                {
                    await ClearUnboundArtsRelatedData();
                }
            }
            finally
            {
                // Подписываемся на события ПОСЛЕ загрузки
                if (this.gridView_unboundArts != null)
                {
                    this.gridView_unboundArts.FocusedRowChanged += gridView_unboundArts_FocusedRowChanged;
                }
                if (this.gridView_wdToBind != null)
                {
                    this.gridView_wdToBind.FocusedRowChanged += gridViewWdToBind_FocusedRowChanged;
                }
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

                string query =// "SELECT DISTINCT SUBSTRING(sa.kod,1,7) as kod, sa.grup, sa.articul, sa.mod, sa.annId " +
                //    "FROM sp_articul sa " +
                //    "   left join kompl k on sa.kod = k.kod_k " +
                //    "WHERE sa.annID IS NULL and k.kod_k is null";//
                                                                 "SELECT * FROM articulListGroupBySizeLabel where annId is null or annId = 0";
                await GridOverlayLoader.LoadListAsync(
                    gridControl_unboundArts,
                    _myDataArtList,
                    _myDataArtBindingSource,
                    async _ => await _dbService.GetListAsync<MyDataART>(query, null),
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

                int kod = GetCurrentKodFromDataSource();
                bool loadAll = FindButtonByTag(layoutControlGroup14, "bind:show-all").Checked;
                //loadAll = layoutControlGroup14.CustomHeaderButtons[6].Properties.Checked;
                await GridOverlayLoader.LoadListAsync(
                    gridControl_wdToBind,
                    _myDataAnnList,
                    _myDataAnnBindingSource,
                    async _ =>
                    {
                        var data = await _artNormService.GetArtNormDataCurrent(loadAll);
                        if (data != null)
                        {
                            foreach (var item in data)
                                item.Stat = StatusHelper.GetStatusText(item.Status);
                        }
                        return data ?? new List<MyDataANN>();
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
        /// Получает значение Kod из текущего элемента источника данных _myDataArtBindingSource.
        /// </summary>
        /// <returns>Значение Kod или 0, если текущий элемент не найден или Kod не может быть преобразован в число.</returns>
        private int GetCurrentKodFromDataSource()
        {
            if (_myDataArtBindingSource != null && _myDataArtBindingSource.Current != null)
            {
                var currentItem = _myDataArtBindingSource.Current as MyDataART;
                if (currentItem != null)
                {
                    if (int.TryParse(currentItem.kodd_rt, out int kodValue))
                    {
                        return kodValue;
                    }
                    else
                    {
                        _logger.LogEventAsync($"Не удалось преобразовать Kod '{currentItem.kodd_rt}' в число.", "GetCurrentKodFromDataSource").ConfigureAwait(false);
                    }
                }
            }
            return 0;
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
                List<MyDataANN> relatedData = new List<MyDataANN>();
                bool loadAll = string.IsNullOrEmpty(articul);//showAllWD;
                ////loadAll = layoutControlGroup14.CustomHeaderButtons[6].Properties.Checked;

                // Если включен чекбокс "Загрузить все"
                if (loadAll)
                {
                    return await _artNormService.GetArtNormDataCurrent(true);
                }

                // Загружаем все данные параллельно
                var tasks = new List<Task<List<MyDataANN>>>();

                // Загружаем данные по коду
                //tasks.Add(_artNormService.GetArtNormDataCurrent(false));

                // Если артикул не пустой, добавляем задачи для поиска по артикулу
                if (!string.IsNullOrEmpty(articul))
                {
                    // Ищем по полному артикулу
                    tasks.Add(_artNormService.GetArtNormDataByArticul(articul));

                    // Ищем по артикулу без дефиса
                    string artWithoutDash = articul.Replace("-", "");
                    if (artWithoutDash != articul)
                    {
                        tasks.Add(_artNormService.GetArtNormDataByArticul(artWithoutDash));
                    }

                    // Если артикул содержит дефис, ищем по его первой части
                    int dashIndex = articul.IndexOf("-");
                    if (dashIndex > 0)
                    {
                        string artPrefix = articul.Substring(0, dashIndex);
                        tasks.Add(_artNormService.GetArtNormDataByArticul(artPrefix));
                    }
                }

                // Ждем завершения всех задач
                var results = await Task.WhenAll(tasks);

                // Объединяем результаты
                foreach (var result in results)
                {
                    if (result != null)
                    {
                        relatedData.AddRange(result);
                    }
                }

                // Удаляем дубликаты по AnnId используя Dictionary для быстрого поиска
                var uniqueData = new Dictionary<int, MyDataANN>();
                foreach (var item in relatedData)
                {
                    if (!uniqueData.ContainsKey(item.AnnID))
                    {
                        uniqueData[item.AnnID] = item;
                    }
                }

                await _logger.LogEventAsync($"Успешная загрузка РТ для артикула {articul}", "LoadWorksbyArt");
                return uniqueData.Values.ToList();
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
            var view = sender as GridView;// gridView_wdToBind; 
            if (view == null)
            {
                await ClearWdToBindRelatedData();
                return;
            }

            // Отменяем предыдущие операции загрузки для Articles tab
            var oldCts = Interlocked.Exchange(ref _loadCts, new CancellationTokenSource());
            oldCts?.Cancel();
            oldCts?.Dispose();
            var token = _loadCts.Token;

            try
            {
                int annId = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "AnnID", 0);

                // Если строка не выбрана или AnnID невалидный - очищаем данные
                if (e.FocusedRowHandle < 0 || annId <= 0)
                {
                    await ClearWdToBindRelatedData();
                    return;
                }

                //1.Сначала загружаем изображение(быстрая операция)
                LoadGridImage(pictureBox2, annId: annId);

                // 2. Затем загружаем основные данные
                token.ThrowIfCancellationRequested();

                await _logger.LogEventAsync($"gridViewWdToBind_FocusedRowChanged: Starting to load data for annId={annId}", "gridViewWdToBind_FocusedRowChanged");

                // Обновляем NormRasz для customGridControl3
                await RefreshNormRaszForArticlesTab(annId, token);
                await RefreshNormRaskForArticlesTab(annId, token);

                await _logger.LogEventAsync($"gridViewWdToBind_FocusedRowChanged: Finished loading NormRasz and NormRask for annId={annId}", "gridViewWdToBind_FocusedRowChanged");

                // 3. Загрузка данных НЗП только для выбранной строки
                token.ThrowIfCancellationRequested();
                await LoadNZPForArticlesTab(annId, token);
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
                async token => annId > 0 ? await _artNormService.GetRelatedNormRasz(annId) : Enumerable.Empty<NormRasz>(),
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
                async token => annId > 0 ? await _artNormService.GetRelatedNormRask(annId, token) : Enumerable.Empty<NormRask>(),
                cancellationToken);
        }

        /// <summary>
        /// Загружает данные НЗП для вкладки "Артикулы".
        /// </summary>
        /// <param name="annId">AnnID для загрузки НЗП.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        private async Task LoadNZPForArticlesTab(int annId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // Проверяем инициализацию
            if (_nzpListArt == null || _nzpByKoddRtSourceArt == null)
            {
                await _logger.LogErrorAsync(new NullReferenceException("_nzpListArt or _nzpByKoddRtSourceArt is null"), "LoadNZPForArticlesTab failed initialization check.");
                return;
            }
            await GridOverlayLoader.LoadListAsync(
                gridControlNZP,
                _nzpListArt,
                _nzpByKoddRtSourceArt,
                async token => annId > 0 ? await _artNormService.GetNzpWithPztCounts(annId, token) : Enumerable.Empty<NZPByKoddRt>(),
                cancellationToken);
            gridControlNZP?.RefreshDataSource();
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
                    async _ => await _dbService.GetListAsync<MyDataANN>(query, null),
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
                        var data = await _dbService.GetListAsync<ArtNormN>(query, null);
                        if (data != null)
                        {
                            foreach (var item in data)
                                item.StatusText = StatusHelper.GetStatusText(item.Status);
                        }
                        return data ?? new List<ArtNormN>();
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
                    Text = $"Вы действительно хотите увязать артикул {selectedArtRow.Articul.TrimEnd()} с разделением труда {selectedAnnRow.Articul.TrimEnd()}?",
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

                var okButton = new Button()
                {
                    Text = "Да",
                    Location = new Point(270, 150),
                    Size = new Size(75, 25),
                    DialogResult = DialogResult.OK
                };

                var cancelButton = new Button()
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
                // Обновляем annId в базе данных
                _artNormService.UpdateAnnIdinArticul(selectedAnnRow.AnnID, selectedArtRow.kodd, selectedArtRow.kodd_rt, selectedArtRow.Articul);
                selectedArtRow.BindedArt = selectedAnnRow.Articul;//заполняем в артикуле из РТ

                await _dbService.UpdateEntityAsync(TableNames.Ann, TableNames.AnnId, selectedAnnRow);
                // Обновляем UI:
                if (artDataSource != null && selectedArtRow != null)
                {
                    // 0. Присваиваем привязанному артикулу артикул разделений
                    selectedArtRow.BindedArt = selectedAnnRow.Articul;
                    // 1. Добавляем привязанный артикул в список для gridView1
                    _boundArtList?.Add(selectedArtRow);

                    // 2. Удаляем артикул из списка доступных для gridView7
                    artDataSource.Remove(selectedArtRow);
                }
                else
                {
                    artView.RefreshData();
                    annView.RefreshData();
                    // Обновим и gridView12 на всякий случай
                    gridControl_binded?.RefreshDataSource();
                    gridControl_wdToBind?.RefreshDataSource();
                }

                await _logger.LogEventAsync("Привязка завершена", $"Артикул {selectedArtRow.kodd_rt} привязан к РТ {selectedAnnRow.AnnID}");
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
                return;
            }

            // Если строка не выбрана - очищаем все связанные данные
            if (e.FocusedRowHandle < 0)
            {
                await ClearUnboundArtsRelatedData();
                return;
            }

            try
            {
                // При смене строки очищаем фильтры gridView_wdToBind и снимаем галку showAllWD
                if (gridView_wdToBind != null)
                {
                    gridView_wdToBind.ActiveFilter.Clear();
                    gridView_wdToBind.ActiveFilterString = string.Empty;
                }

                // Обновляем состояние кнопки и переменной showAllWD
                var showAllButton = FindButtonByTag(layoutControlGroup14, "bind:show-all");
                if (showAllButton != null)
                {
                    showAllButton.Checked = false;
                    showAllWD = false;
                }
                if (layoutControlGroup14 != null)
                {
                    var btn = layoutControlGroup14.CustomHeaderButtons[6];
                    btn.Properties.Checked = false;
                }

                // Получаем данные из текущей строки
                string kod = CommonFunctions.GetRowCellValueOrDefault<string>(gv_unbound_Arts, e.FocusedRowHandle, "kodd_rt", "");
                string articul = CommonFunctions.GetRowCellValueOrDefault<string>(gv_unbound_Arts, e.FocusedRowHandle, "Articul", "").TrimEnd(' ');

                if (!int.TryParse(kod, out int kodInt))
                {
                    await _logger.LogWarningAsync($"Не удалось преобразовать Kod '{kod}' в число", "gridView_unboundArts_FocusedRowChanged_Internal");
                    kodInt = 0; // Устанавливаем 0 для дальнейшей обработки
                }

                // Если нет артикула и кода - очищаем данные
                if (string.IsNullOrEmpty(articul) && kodInt <= 0)
                {
                    await ClearUnboundArtsRelatedData();
                    return;
                }

                // Загружаем данные параллельно
                var loadWorksTask = LoadWorksbyArt(articul);
                var loadRaszTask = Task.Run(async () =>
                {
                    if (gridView_wdToBind.FocusedRowHandle >= 0)
                    {
                        int annId = CommonFunctions.GetRowCellValueOrDefault<int>(gridView_wdToBind, gridView_wdToBind.FocusedRowHandle, "AnnId", 0);
                        return annId > 0 ? await _artNormService.GetRelatedNormRasz(annId) : new List<NormRasz>();
                    }
                    return new List<NormRasz>();
                });

                // Ждем загрузку данных
                var list = await loadWorksTask;

                // Обновляем основной список данных
                if (_myDataAnnBindingSource != null)
                {
                    _myDataAnnList.Clear();
                    if (list != null && list.Count > 0)
                    {
                        // Отключаем уведомления на время добавления элементов
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
                            // Включаем уведомления обратно
                            _myDataAnnList.RaiseListChangedEvents = true;
                        }
                    }
                    _myDataAnnBindingSource.ResetBindings(false);
                }
                else
                {
                    gridControl_wdToBind.DataSource = list;
                }

                // Обновляем UI один раз после всех изменений данных
                gridView_wdToBind.RefreshData();
                gridControl_wdToBind.RefreshDataSource();

                // Обновляем customGridControl3 using _normRaszListArticles and _normRaszBindingSourceArticles
                var raszList = await loadRaszTask;
                _normRaszListArticles.Clear();
                if (raszList != null)
                {
                    foreach (var item in raszList)
                    {
                        _normRaszListArticles.Add(item);
                    }
                }
                _normRaszBindingSourceArticles.ResetBindings(false);

                // Загружаем или очищаем изображение
                if (kodInt > 0)
                {
                    await Task.Run(() => LoadGridImage(pictureBox3, kod: kodInt));
                }
                else
                {
                    // Очищаем картинку если нет кода
                    if (pictureBox3.InvokeRequired)
                    {
                        pictureBox3.Invoke((MethodInvoker)(() => pictureBox3.Image = null));
                    }
                    else
                    {
                        pictureBox3.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка в gridView_unboundArts_FocusedRowChanged_Internal");
                // В случае ошибки очищаем данные
                await ClearUnboundArtsRelatedData();
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

                // Очищаем НЗП
                if (_nzpListArt != null && _nzpByKoddRtSourceArt != null)
                {
                    _nzpListArt.Clear();
                    _nzpByKoddRtSourceArt.ResetBindings(false);
                }

                // Обновляем UI
                gridControlNZP?.RefreshDataSource();
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

                // Обновляем статус для каждой выбранной записи
                int successCount = 0;
                var errors = new List<string>();

                archiveGridView.BeginUpdate();
                try
                {
                    foreach (var item in selectedItems)
                    {
                        try
                        {
                            // Проверяем, что запись можно восстановить
                            if (item.Status != (int)Status.Archive)
                            {
                                await _logger.LogEventAsync($"Запись AnnID: {item.AnnID} не находится в архиве (статус: {item.Status})", "RestoreFromArchive");
                                continue;
                            }

                            // Обновляем статус в базе данных с 3 (архив) на 2 (актуальное)
                            await _dbService.UpdateFieldAsync(TableNames.Ann, "Status", (int)Status.Preliminary, TableNames.AnnId, item.AnnID);

                            // Обновляем объект в памяти
                            item.Status = (int)Status.Preliminary;
                            item.StatusText = StatusHelper.GetStatusText((int)Status.Preliminary);

                            // Обновляем строку в гриде
                            int rowHandle = archiveGridView.LocateByValue("AnnID", item.AnnID);
                            if (rowHandle >= 0)
                            {
                                archiveGridView.RefreshRow(rowHandle);
                            }

                            successCount++;
                            await _logger.LogEventAsync($"Статус записи AnnID: {item.AnnID} изменен с 'Архивное' на 'Актуальное'", "RestoreFromArchive");
                        }
                        catch (Exception ex)
                        {
                            string errorMsg = $"AnnID: {item.AnnID} - {ex.Message}";
                            errors.Add(errorMsg);
                            await _logger.LogErrorAsync(ex, $"Ошибка при восстановлении записи из архива AnnID: {item.AnnID}");
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

