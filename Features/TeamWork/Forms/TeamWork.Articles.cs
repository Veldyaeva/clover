using System.Threading.Tasks;
using System.Threading;
using System;
using SewingProduction.Models;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using SewingProduction.Helpers;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.XtraGrid.Views.Base;
using System.Collections;
using SewingProduction.Services;
using DevExpress.Xpo.DB.Helpers;
using System.Linq;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork
    {
        // Вторая вкладка — "Работа с артикулами"

        private bool _isUnchecking = false;

        /// <summary>
        /// Загрузка вкладки "текущие работы" (MyDataAnn)
        /// </summary>
        private async Task CurrentWorks_Load()
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
                Task preArchTask = PreArchLoad();

                // Загружаем основные данные
                await MyDataArtLoad();
                await MyDataAnnLoad();

                await preArchTask;

                TWGridHelper.sortGridView(gridView6);
                //TWGridHelper.sortGridView(gridViewRaskr);
                //TWGridHelper.sortGridView(gridViewKont);


                // Load NormRasz data for the Articles tab using the dedicated BindingList and BindingSource
                if (this.gridView_wdToBind != null && gridView_wdToBind.RowCount > 0 && gridView_wdToBind.FocusedRowHandle >= 0)
                {
                    int initialAnnId = CommonFunctions.GetRowCellValueOrDefault<int>(gridView_wdToBind, gridView_wdToBind.FocusedRowHandle, "AnnId", 0);
                    List<NormRasz> raszData = new List<NormRasz>();
                    if (initialAnnId > 0)
                    {
                        raszData = await _artNormService.GetRelatedNormRasz(initialAnnId);
                    }

                    _normRaszListArticles.Clear();
                    if (raszData != null)
                    {
                        foreach (var item in raszData)
                        {
                            _normRaszListArticles.Add(item);
                        }
                    }
                    _normRaszBindingSourceArticles.ResetBindings(false);
                }
                else
                {
                    // Если нет выбранных строк в gridView_wdToBind - очищаем все связанные данные
                    _normRaszListArticles.Clear();
                    _normRaszBindingSourceArticles.ResetBindings(false);
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


        private async Task MyDataArtLoad()
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
                                                                 "SELECT * FROM articulListUnboundRTBySizeLabel";
                List<MyDataART> loadedData = await _dbService.GetListAsync<MyDataART>(query, null);

                _myDataArtList.BulkLoad(loadedData);

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

        private async Task MyDataAnnLoad()
        {
            try
            {
                // Проверяем, инициализирован ли список/источник (должны быть в TeamWork.cs)
                if (_myDataAnnList == null || _myDataAnnBindingSource == null)
                {
                    await _logger.LogErrorAsync(new NullReferenceException("_myDataAnnList or _myDataAnnBindingSource is null"), "MyDataAnnLoad initialization check failed.");
                    MessageBox.Show("Ошибка инициализации списка РТ.", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int kod = GetCurrentKodFromDataSource();
                List<MyDataANN> loadedData = await _artNormService.GetArtNormDataCurrent(loadAllCheckBox.Checked);

                _myDataAnnList.BulkLoad(loadedData);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных MyDataANN: {ex.Message}");
                // Отображаем сообщение только если инициализация прошла успешно
                if (_myDataAnnList != null && _myDataAnnBindingSource != null)
                {
                    MessageBox.Show($"Ошибка загрузки данных РТ: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
        /// Загружает список разделений труда (РТ) для указанного артикула
        /// </summary>
        /// <param name="articul">Название артикула</param>
        /// <returns>Список разделений труда (BindingList&lt;MyDataANN&gt;)</returns>
        private async Task<List<MyDataANN>> LoadWorksbyArt(string articul)
        {
            try
            {
                List<MyDataANN> relatedData = new List<MyDataANN>();
                bool loadAll = loadAllCheckBox.Checked;

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
                
                // Обновляем NormRasz для customGridControl3
                await RefreshNormRaszForArticlesTab(annId, token);
                await RefreshNormRaskForArticlesTab(annId, token);
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
            
            List<NormRasz> raszList = new List<NormRasz>();
            if (annId > 0)
            {
                raszList = await _artNormService.GetRelatedNormRasz(annId);
                cancellationToken.ThrowIfCancellationRequested();
            }

            _normRaszListArticles.RaiseListChangedEvents = false;
            _normRaszListArticles.Clear();
            if (raszList != null)
            {
                foreach (var item in raszList)
                {
                    _normRaszListArticles.Add(item);
                }
            }
            _normRaszListArticles.RaiseListChangedEvents = true;
            _normRaszBindingSourceArticles.ResetBindings(false);
        }

        private async Task RefreshNormRaskForArticlesTab(int annId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            List<NormRask> raskList = new List<NormRask>();
            if (annId > 0)
            {
                raskList = await _artNormService.GetRelatedNormRask(annId);
                cancellationToken.ThrowIfCancellationRequested();
            }

            _normRaskListArticles.RaiseListChangedEvents = false;
            _normRaskListArticles.Clear();
            if (raskList != null)
            {
                foreach (var item in raskList)
                {
                    _normRaskListArticles.Add(item);
                }
            }
            _normRaskListArticles.RaiseListChangedEvents = true;
            _normRaskBindingSourceArticles.ResetBindings(false);
        }

        /// <summary>
        /// Загружает данные НЗП для вкладки "Артикулы".
        /// </summary>
        /// <param name="annId">AnnID для загрузки НЗП.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        private async Task LoadNZPForArticlesTab(int annId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var nzpData = annId > 0 ? await _artNormService.GetNzpWithPztCounts(annId, cancellationToken) : new List<NZPByKoddRt>();
            
            cancellationToken.ThrowIfCancellationRequested();
            
            _nzpListArt.RaiseListChangedEvents = false;
            _nzpListArt.Clear();
            foreach (var item in nzpData)
            {
                _nzpListArt.Add(item);
            }
            _nzpListArt.RaiseListChangedEvents = true;
            
            _nzpByKoddRtSourceArt?.ResetBindings(false);
            gridControlNZP?.RefreshDataSource(); // Обновить грид НЗП
            await UpdateUnboundButtonStatusBasedOnNZP(); // Обновить состояние кнопки
        }

        private async Task PreArchLoad()
        {
            try
            {
                string query = "SELECT * FROM artNormNView WHERE status = 4";
                List<MyDataANN> preArchData = await _dbService.GetListAsync<MyDataANN>(query, null);

                if (_preArchList == null || _preArchBindingSource == null)
                {
                    await _logger.LogErrorAsync(new NullReferenceException("_preArchList or _preArchBindingSource is null"), "PreArchLoad failed initialization check.");
                    MessageBox.Show("Ошибка инициализации списка предварительного архива.", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //_preArchList.Clear();

                //if (preArchData != null)
                //{
                //    foreach (var item in preArchData)
                //    {
                //        _preArchList.Add(item);
                //    }
                //}
                //_preArchBindingSource.ResetBindings(false);
                _preArchList.BulkLoad(preArchData);

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
                if (fillGroupCheckBox.Checked && string.IsNullOrEmpty(selectedAnnRow.grup))
                {
                    selectedAnnRow.grup = selectedArtRow.grup;
                }
                if (fillModelCheckBox.Checked && string.IsNullOrEmpty(selectedAnnRow.mod))
                {
                    selectedAnnRow.mod = selectedArtRow.mod;
                }
                // Обновляем annId в базе данных
                _artNormService.UpdateAnnIdinArticul(selectedAnnRow.AnnID, selectedArtRow.kodd, selectedArtRow.kodd_rt,selectedArtRow.Articul);
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
    }
}

