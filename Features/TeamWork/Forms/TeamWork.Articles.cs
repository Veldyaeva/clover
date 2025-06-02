//// TeamWork.Articles.cs
//using System.Threading.Tasks;
//using System;
//using SewingProduction.Models;
//using System.ComponentModel;
//using System.Data;
//using System.Windows.Forms;
//using DevExpress.XtraGrid.Columns;
//using DevExpress.XtraGrid.Views.Grid.ViewInfo;
//using DevExpress.XtraGrid.Views.Grid;
//using DevExpress.XtraGrid;
//using SewingProduction.Helpers;
//using System.Collections.Generic;
//using System.Drawing;
//using DevExpress.XtraGrid.Views.Base;
//using System.Collections;
//using SewingProduction.Services;
//using DevExpress.Xpo.DB.Helpers;
//using System.Linq;

//namespace SewingProduction.Forms
//{
//    public partial class TeamWork
//    {
//        // Вторая вкладка — "Работа с артикулами"

//        private bool _isUnchecking = false;

//        /// <summary>
//        /// Загрузка вкладки "текущие работы" (MyDataAnn)
//        /// </summary>
//        private async Task CurrentWorks_Load()
//        {
//            // Отписываемся от событий ПЕРЕД загрузкой
//            if (this.gridView_unboundArts != null)
//            {
//                this.gridView_unboundArts.FocusedRowChanged -= gridView_unboundArts_FocusedRowChanged;
//            }
//            if (this.gridView_wdToBind != null)
//            {
//                 this.gridView_wdToBind.FocusedRowChanged -= gridView8_FocusedRowChanged;
//            }

//            try 
//            {
//                Task preArchTask = PreArchLoad();

//                // Загружаем основные данные
//                await MyDataArtLoad(); 
//                await MyDataAnnLoad(); 

//                await preArchTask;

//                if (this.gridView_wdToBind != null && gridView_wdToBind.RowCount > 0 && gridView_wdToBind.FocusedRowHandle >= 0)
//                {
//                    int initialAnnId = CommonFunctions.GetRowCellValueOrDefault<int>(gridView_wdToBind, gridView_wdToBind.FocusedRowHandle, "AnnId", 0);
//                    if (initialAnnId > 0)
//                    {
//                        await TWGridHelper.LoadGridControlDataAsync(customGridControl3, normraszBindingSource, await _artNormService.GetRelatedNormRasz(initialAnnId));
//                    }
//                    else
//                    {
//                         await TWGridHelper.LoadGridControlDataAsync(customGridControl3, normraszBindingSource, new List<NormRasz>());
//                    }
//                }
//                else
//                {
//                     await TWGridHelper.LoadGridControlDataAsync(customGridControl3, normraszBindingSource, new List<NormRasz>());
//                }
//            }
//            finally
//            {
//                // Подписываемся обратно ПОСЛЕ всей загрузки
//                if (this.gridView_unboundArts != null)
//                {
//                    this.gridView_unboundArts.FocusedRowChanged += gridView_unboundArts_FocusedRowChanged;
//                }
//                if (this.gridView_wdToBind != null)
//                {
//                    this.gridView_wdToBind.FocusedRowChanged += gridView8_FocusedRowChanged;
//                }
//            }
//        }


//        private async Task MyDataArtLoad()
//        {
//            try
//            {
//                // Проверяем, инициализирован ли список/источник (должны быть в TeamWork.cs)
//                if (_myDataArtList == null || _myDataArtBindingSource == null)
//                {
//                    await _logger.LogErrorAsync(new NullReferenceException("_myDataArtList or _myDataArtBindingSource is null"), "MyDataArtLoad initialization check failed.");
//                    MessageBox.Show("Ошибка инициализации списка артикулов.", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    return;
//                }

//                string query = "SELECT DISTINCT SUBSTRING(kod,1,7) as kod, grup, articul, mod, annId FROM sp_articul WHERE annID IS NULL";
//                List<MyDataART> loadedData = await _dbService.GetListAsync<MyDataART>(query, null);

//                _myDataArtList.Clear(); // Очищаем BindingList

//                if (loadedData != null)
//                {
//                    foreach (var item in loadedData)
//                    {
//                        _myDataArtList.Add(item); // Добавляем элементы в BindingList
//                    }
//                }

//                _myDataArtBindingSource.ResetBindings(false); // Уведомляем BindingSource (и грид) об изменениях

//                await _logger.LogEventAsync($"Загружено {_myDataArtList.Count} записей MyDataART.", "MyDataArtLoad");
//            }
//            catch (Exception ex)
//            {
//                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных MyDataART: {ex.Message}");
//                // Отображаем сообщение только если инициализация прошла успешно
//                if (_myDataArtList != null && _myDataArtBindingSource != null)
//                {
//                    MessageBox.Show($"Ошибка загрузки данных артикулов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private async Task MyDataAnnLoad()
//        {
//            try
//            {
//                // Проверяем, инициализирован ли список/источник (должны быть в TeamWork.cs)
//                if (_myDataAnnList == null || _myDataAnnBindingSource == null)
//                {
//                    await _logger.LogErrorAsync(new NullReferenceException("_myDataAnnList or _myDataAnnBindingSource is null"), "MyDataAnnLoad initialization check failed.");
//                    MessageBox.Show("Ошибка инициализации списка РТ.", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    return;
//                }

//                int kod = GetCurrentKodFromDataSource();
//                List<MyDataANN> loadedData = await _artNormService.GetArtNormDataCurrent(kod, loadAllCheckBox.Checked);

//                _myDataAnnList.Clear(); // Очищаем BindingList

//                if (loadedData != null && loadedData.Count > 0)
//                {
//                    foreach (var item in loadedData)
//                    {
//                        _myDataAnnList.Add(item); // Добавляем элементы в BindingList
//                    }
//                    await _logger.LogEventAsync($"Загружено {_myDataAnnList.Count} записей MyDataANN (kod: {kod}, loadAll: {loadAllCheckBox.Checked}).", "MyDataAnnLoad");
//                }
//                else
//                {
//                    // Логгируем, если данных нет, вместо MessageBox
//                    await _logger.LogEventAsync($"Нет данных MyDataANN для загрузки (kod: {kod}, loadAll: {loadAllCheckBox.Checked})", "MyDataAnnLoad");
//                }

//                _myDataAnnBindingSource.ResetBindings(false); // Уведомляем BindingSource (и грид) об изменениях
//            }
//            catch (Exception ex)
//            {
//                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных MyDataANN: {ex.Message}");
//                // Отображаем сообщение только если инициализация прошла успешно
//                if (_myDataAnnList != null && _myDataAnnBindingSource != null)
//                {
//                    MessageBox.Show($"Ошибка загрузки данных РТ: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        /// <summary>
//        /// Получает значение Kod из текущего элемента источника данных _myDataArtBindingSource.
//        /// </summary>
//        /// <returns>Значение Kod или 0, если текущий элемент не найден или Kod не может быть преобразован в число.</returns>
//        private int GetCurrentKodFromDataSource()
//        {
//            if (_myDataArtBindingSource != null && _myDataArtBindingSource.Current != null)
//            {
//                var currentItem = _myDataArtBindingSource.Current as MyDataART;
//                if (currentItem != null)
//                {
//                    if (int.TryParse(currentItem.Kod, out int kodValue))
//                    {
//                        return kodValue;
//                    }
//                    else
//                    {
//                        _logger.LogEventAsync($"Не удалось преобразовать Kod '{currentItem.Kod}' в число.", "GetCurrentKodFromDataSource").ConfigureAwait(false);
//                    }
//                }
//            }
//            return 0;
//        }

//        /// <summary>
//        /// Загружает список разделений труда (РТ) для указанного артикула или кода.
//        /// </summary>
//        /// <param name="kod">Код артикула</param>
//        /// <param name="articul">Название артикула</param>
//        /// <returns>Список разделений труда (BindingList&lt;MyDataANN&gt;)</returns>
//        private async Task<List<MyDataANN>> LoadWorksbyArt(int kod, string articul)
//        {
//            try
//            {
//                List<MyDataANN> relatedData;
//                bool loadAll = loadAllCheckBox.Checked;
//                // Если включен чекбокс "Загрузить все"
//                relatedData = await _artNormService.GetArtNormDataCurrent(kod, loadAll);
//                if (!loadAll)
//                { // Если артикул содержит "-", фильтруем по его первой части
//                    int dashIndex = articul.IndexOf("-");
//                    if (dashIndex > 0)
//                    {
//                        string artPrefix = articul.Substring(0, dashIndex);
//                        List<MyDataANN> partialData = await _artNormService.GetArtNormDataByArticulPrefix(artPrefix);
//                        if (partialData != null)
//                            relatedData.AddRange(partialData);
//                    }
//                }

//                await _logger.LogEventAsync($"Успешная загрузка РТ для кода {kod} и артикула {articul}", "LoadWorksbyArt");
//                return relatedData;
//            }
//            catch (Exception ex)
//            {
//                await _logger.LogErrorAsync(ex, $"Ошибка загрузки РТ для кода {kod} и артикула {articul}");
//                MessageBox.Show("Ошибка загрузки данных. Подробности в логе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return new List<MyDataANN>(); // Возвращаем пустой список в случае ошибки
//            }

//        }

//        //async Task NormRaszLoad()
//        //{
//        //    int annId = 0;
//        //    var view = gridControl_wdToBind.MainView as GridView;
//        //    if (view != null)
//        //    {
//        //        annId = Convert.ToInt32(view.GetRowCellValue(0, "AnnId"));
//        //    }
//        //    var relatedRasz = await _artNormService.GetRelatedNormRasz(annId);
//        //    normraszBindingSource1.DataSource = relatedRasz;
//        //    customGridControl3.DataSource = normraszBindingSource1;
//        //}

//        /// <summary>
//        /// загрузка norm_rasz
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        private async void gridView8_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
//        {
//            int annId = CommonFunctions.GetRowCellValueOrDefault<int>(gridView_wdToBind, e.FocusedRowHandle, "AnnId", 0);
//            //     await TWGridHelper.LoadGridControlDataAsync(customGridControl3, normraszBindingSource, await _artNormService.GetRelatedNormRasz(annId));
//            var raszList = await _artNormService.GetRelatedNormRasz(annId);

//            // Кладём в BindingList и обновляем ваш BindingSource
//            _normRaszListTW = new BindingList<NormRasz>(raszList);
//            _normRaszBindingSourceTW.DataSource = _normRaszListTW;

//            // И ставим его как DataSource грида
//            customGridControl3.DataSource = _normRaszBindingSourceTW;

//        }
//        private async Task PreArchLoad()
//        {
//            try
//            {
//                string query = "SELECT * FROM artNormNView WHERE status = 4"; 
//                List<MyDataANN> preArchData = await _dbService.GetListAsync<MyDataANN>(query, null);

//                if (_preArchList == null || _preArchBindingSource == null)
//                {
//                    await _logger.LogErrorAsync(new NullReferenceException("_preArchList or _preArchBindingSource is null"), "PreArchLoad failed initialization check.");
//                    MessageBox.Show("Ошибка инициализации списка предварительного архива.", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    return;
//                }
//                _preArchList.Clear(); 

//                if (preArchData != null)
//                {
//                    foreach (var item in preArchData)
//                    {
//                        _preArchList.Add(item);
//                    }
//                }
//                _preArchBindingSource.ResetBindings(false);

//                await _logger.LogEventAsync($"Загружено {_preArchList.Count} записей в предварительный архив.", "PreArchLoad");

//            }
//            catch (Exception ex)
//            {
//                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных в предварительный архив: {ex.Message}");
//                if (_preArchList != null && _preArchBindingSource != null)
//                {
//                    MessageBox.Show($"Ошибка загрузки данных предварительного архива: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        /// <summary>
//        /// Привязывает выбранные артикулы к выбранному разделению труда (РТ).
//        /// </summary>
//        private async Task BindButton_Click_Internal(object sender, EventArgs e)
//        {
//            try
//            {
//                GridView artView = gridView_unboundArts;
//                GridView annView = gridView_wdToBind;

//                if (artView == null || annView == null)
//                {
//                    MessageBox.Show("Данные не загружены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                    return;
//                }

//                MyDataART selectedArtRow = null;
//                MyDataANN selectedAnnRow = null;
//                // Получаем выбранные артикулы
//                var artBindingSource = artView.DataSource as BindingSource;
//                var artDataSource = artBindingSource?.DataSource as BindingList<MyDataART>;

//                //// Получаем выбранное разделение труда
//                var annBindingSource = _myDataAnnBindingSource;
//                var annDataSource = annBindingSource?.DataSource as BindingList<MyDataANN>;
//                selectedArtRow = artDataSource?.FirstOrDefault(r => r.IsChecked);
//                selectedAnnRow = annDataSource?.FirstOrDefault(r => r.IsChecked);

//                // Проверяем, выбраны ли оба элемента
//                if (selectedArtRow is null || selectedAnnRow is null)
//                {
//                    MessageBox.Show("Выберите артикул и разделение труда для привязки!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                    return;
//                }

//                DialogResult result = MessageBox.Show(
//                    $"Вы действительно хотите привязать артикул {selectedArtRow.Articul.TrimEnd()} к разделению труда {selectedAnnRow.Articul.TrimEnd()}?",
//                    "Подтверждение привязки",
//                    MessageBoxButtons.YesNo,
//                    MessageBoxIcon.Question
//                );

//                if (result == DialogResult.No) return;


//                // Обновляем annId в базе данных
//                _artNormService.UpdateAnnIdinArticul(selectedArtRow.Kod, selectedAnnRow.AnnId); 

//                // Обновляем UI:
//                if (artDataSource != null && selectedArtRow != null)
//                {
//                    // 0. Присваиваем привязанному артиклю артикля разделений
//                    selectedArtRow.BindedArt = selectedAnnRow.Articul;
//                    // 1. Добавляем привязанный артикул в список для gridView1
//                    _boundArtList?.Add(selectedArtRow); 

//                    // 2. Удаляем артикул из списка доступных для gridView7
//                    artDataSource.Remove(selectedArtRow);
//                }
//                else
//                {
//                    artView.RefreshData();
//                    annView.RefreshData();
//                    // Обновим и gridView12 на всякий случай
//                    gridControl_binded?.RefreshDataSource(); 
//                    gridControl_wdToBind?.RefreshDataSource();
//                }

//                await _logger.LogEventAsync("Привязка завершена", $"Артикул {selectedArtRow.Kod} привязан к РТ {selectedAnnRow.AnnId}");
//                MessageBox.Show("Привязка успешно выполнена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//            catch (Exception ex)
//            {
//                await _logger.LogErrorAsync(ex, "Ошибка при привязке артикула к РТ");
//                MessageBox.Show($"Ошибка при привязке артикула: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private async void gridView_unboundArts_FocusedRowChanged_Internal(object sender, FocusedRowChangedEventArgs e)
//        {
//            var gv_unbound_Arts = sender as GridView; 
//            if (gv_unbound_Arts == null || e.FocusedRowHandle < 0) return;

//            string kod = CommonFunctions.GetRowCellValueOrDefault<string>(gv_unbound_Arts, e.FocusedRowHandle, "Kod", "");
//            string articul = CommonFunctions.GetRowCellValueOrDefault<string>(gv_unbound_Arts, e.FocusedRowHandle, "Articul", "");

//            int kodInt = 0;
//            int.TryParse(kod, out kodInt);

//            List<MyDataANN> list = await LoadWorksbyArt(kodInt, articul);

//            if (_myDataAnnBindingSource != null)
//            {
//                _myDataAnnList.Clear(); 
//                if (list != null)
//                {
//                    foreach(var item in list) _myDataAnnList.Add(item);
//                }
//                _myDataAnnBindingSource.ResetBindings(false); 
//            }
//            else 
//            {
//                gridControl_wdToBind.DataSource = list;
//                gridView_wdToBind.RefreshData(); 
//            }


//            // Явно обновляем/очищаем customGridControl3 после обновления gridView8
//            try
//            {
//                if (list != null && list.Count > 0)
//                {
//                    int rowHandleToUse = gridView_wdToBind.FocusedRowHandle >= 0 ? gridView_wdToBind.FocusedRowHandle : 0;
//                    if (rowHandleToUse < gridView_wdToBind.RowCount)
//                    {
//                        int annIdToLoad = CommonFunctions.GetRowCellValueOrDefault<int>(gridView_wdToBind, rowHandleToUse, "AnnId", 0);
//                        if (annIdToLoad > 0)
//                        {
//                            await TWGridHelper.LoadGridControlDataAsync(customGridControl3, normraszBindingSource, await _artNormService.GetRelatedNormRasz(annIdToLoad));
//                        }
//                        else
//                        {
//                            await TWGridHelper.LoadGridControlDataAsync(customGridControl3, normraszBindingSource, new List<NormRasz>());
//                        }
//                    }
//                    else
//                    {
//                        await TWGridHelper.LoadGridControlDataAsync(customGridControl3, normraszBindingSource, new List<NormRasz>());
//                    }
//                }
//                else
//                {
//                    // Если загруженный список ПУСТ, очищаем customGridControl3
//                    // await GridHelper.LoadGridControlDataAsync(customGridControl3, normraszBindingSource, new List<NormRasz>());
//                    customGridControl3.DataSource = null;
//                    customGridControl3.RefreshDataSource();
//                }
//            }
//            catch (Exception ex)
//            {
//                await _logger.LogErrorAsync(ex, "Ошибка обновления customGridControl3 в gridView7_FocusedRowChanged_Internal");
//                await TWGridHelper.LoadGridControlDataAsync(customGridControl3, normraszBindingSource, new List<NormRasz>()); // Очищаем при ошибке
//            }
//        }
//    }
//}
// TeamWork.Articles.cs
using System.Threading.Tasks;
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

namespace SewingProduction.Forms
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
                    _normRaszListArticles.Clear();
                    _normRaszBindingSourceArticles.ResetBindings(false);
                }
            }
            finally
            {
                // Подписываемся обратно ПОСЛЕ всей загрузки
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

                string query = "SELECT DISTINCT SUBSTRING(kod,1,7) as kod, grup, articul, mod, annId FROM sp_articul WHERE annID IS NULL";
                List<MyDataART> loadedData = await _dbService.GetListAsync<MyDataART>(query, null);

                //_myDataArtList.Clear(); // Очищаем BindingList

                //if (loadedData != null)
                //{
                //    foreach (var item in loadedData)
                //    {
                //        _myDataArtList.Add(item); // Добавляем элементы в BindingList
                //    }
                //}

                //_myDataArtBindingSource.ResetBindings(false); // Уведомляем BindingSource (и грид) об изменениях
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
                List<MyDataANN> loadedData = await _artNormService.GetArtNormDataCurrent(kod, loadAllCheckBox.Checked);

                //_myDataAnnList.Clear(); // Очищаем BindingList

                //if (loadedData != null && loadedData.Count > 0)
                //{
                //    foreach (var item in loadedData)
                //    {
                //        _myDataAnnList.Add(item); // Добавляем элементы в BindingList
                //    }
                //    await _logger.LogEventAsync($"Загружено {_myDataAnnList.Count} записей MyDataANN (kod: {kod}, loadAll: {loadAllCheckBox.Checked}).", "MyDataAnnLoad");
                //}
                //else
                //{
                //    // Логгируем, если данных нет, вместо MessageBox
                //    await _logger.LogEventAsync($"Нет данных MyDataANN для загрузки (kod: {kod}, loadAll: {loadAllCheckBox.Checked})", "MyDataAnnLoad");
                //}
                _myDataAnnList.BulkLoad(loadedData);

               // _myDataAnnBindingSource.ResetBindings(false); // Уведомляем BindingSource (и грид) об изменениях
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
                    if (int.TryParse(currentItem.Kod, out int kodValue))
                    {
                        return kodValue;
                    }
                    else
                    {
                        _logger.LogEventAsync($"Не удалось преобразовать Kod '{currentItem.Kod}' в число.", "GetCurrentKodFromDataSource").ConfigureAwait(false);
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
        private async Task<List<MyDataANN>> LoadWorksbyArt(int kod, string articul)
        {
            try
            {
                List<MyDataANN> relatedData = new List<MyDataANN>();
                bool loadAll = loadAllCheckBox.Checked;

                // Если включен чекбокс "Загрузить все"
                if (loadAll)
                {
                    return await _artNormService.GetArtNormDataCurrent(kod, true);
                }

                // Загружаем все данные параллельно
                var tasks = new List<Task<List<MyDataANN>>>();

                // Загружаем данные по коду
                tasks.Add(_artNormService.GetArtNormDataCurrent(kod, false));

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

                await _logger.LogEventAsync($"Успешная загрузка РТ для кода {kod} и артикула {articul}", "LoadWorksbyArt");
                return uniqueData.Values.ToList();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при загрузке РТ для кода {kod} и артикула {articul}");
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
            GridView view = gridView_wdToBind; // Кастуем sender к GridView один раз
            if (view == null) return; // Если view null, выходим

            int annId = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "AnnID", 0);

            // Обновляем NormRasz для customGridControl3
            await RefreshNormRaszForArticlesTab(annId);

            // Загрузка данных НЗП
            if (_nzpList != null)
            {
                _nzpList.Clear();
                if (annId > 0)
                {
                    List<NZPByKoddRt> nzpData = await _artNormService.GetNzpWithPztCounts(annId);
                    if (nzpData != null)
                    {
                        foreach (var item in nzpData)
                        {
                            _nzpList.Add(item);
                        }
                    }
                }
                _nzpByKoddRtSource?.ResetBindings(false);
                gridControlNZP?.RefreshDataSource(); // Обновить грид НЗП
                await UpdateUnboundButtonStatusBasedOnNZP(); // Обновить состояние кнопки
            }

            string kodString = view.GetRowCellValue(e.FocusedRowHandle, "Kod")?.ToString();

            if (!string.IsNullOrEmpty(kodString))
            {
                if (int.TryParse(kodString, out int kodValue) && kodValue > 0)
                {
                    LoadGridControlData(pictureBox2, kodValue);
                }
                else
                {
                    await _logger.LogWarningAsync($"Не удалось преобразовать Kod '{kodString}' в корректное число > 0 для строки {e.FocusedRowHandle}.", "gridViewWdToBind_FocusedRowChanged");
                }
            }
            else
            {
                await _logger.LogWarningAsync($"Значение Kod пустое или null для строки {e.FocusedRowHandle}.", "gridViewWdToBind_FocusedRowChanged");
            }
        }

        /// <summary>
        /// Загружает и обновляет NormRasz данные для вкладки "Артикулы" (customGridControl3).
        /// </summary>
        /// <param name="annId">AnnID для загрузки NormRasz.</param>
        private async Task RefreshNormRaszForArticlesTab(int annId)
        {
            List<NormRasz> raszList = new List<NormRasz>();
            if (annId > 0)
            {
                raszList = await _artNormService.GetRelatedNormRasz(annId);
            }

            _normRaszListArticles.Clear();
            if (raszList != null)
            {
                foreach (var item in raszList)
                {
                    _normRaszListArticles.Add(item);
                }
            }
            _normRaszBindingSourceArticles.ResetBindings(false);
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
                // Получаем выбранные артикулы
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
                    MessageBox.Show("Выберите артикул и разделение труда для привязки!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"Вы действительно хотите привязать артикул {selectedArtRow.Articul.TrimEnd()} к разделению труда {selectedAnnRow.Articul.TrimEnd()}?",
                    "Подтверждение привязки",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.No) return;


                // Обновляем annId в базе данных
                _artNormService.UpdateAnnIdinArticul(selectedArtRow.Kod, selectedAnnRow.AnnID);
                selectedArtRow.BindedArt = selectedAnnRow.Articul;//заполняем в артикуле из РТ
                selectedAnnRow.grup = selectedArtRow.grup;//заполняем в РТ из артикула
                selectedAnnRow.mod = selectedArtRow.mod;//заполняем в РТ из артикула
                                                        // await _dbService.UpdateFieldAsync(TableNames.Art, "annId", selectedAnnRow.AnnID, "kod", selectedArtRow.Kod);//хочу поменять обновление annId в артикуле, но пока не могу
                await _dbService.UpdateEntityAsync(TableNames.Ann, TableNames.AnnId, selectedAnnRow);
                // Обновляем UI:
                if (artDataSource != null && selectedArtRow != null)
                {
                    // 0. Присваиваем привязанному артиклю артикля разделений
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

                await _logger.LogEventAsync("Привязка завершена", $"Артикул {selectedArtRow.Kod} привязан к РТ {selectedAnnRow.AnnID}");
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
            if (gv_unbound_Arts == null || e.FocusedRowHandle < 0) return;

            try
            {
                // Получаем данные из текущей строки
                string kod = CommonFunctions.GetRowCellValueOrDefault<string>(gv_unbound_Arts, e.FocusedRowHandle, "Kod", "");
                string articul = CommonFunctions.GetRowCellValueOrDefault<string>(gv_unbound_Arts, e.FocusedRowHandle, "Articul", "");

                if (!int.TryParse(kod, out int kodInt))
                {
                    await _logger.LogWarningAsync($"Не удалось преобразовать Kod '{kod}' в число", "gridView_unboundArts_FocusedRowChanged_Internal");
                    return;
                }

                // Загружаем данные параллельно
                var loadWorksTask = LoadWorksbyArt(kodInt, articul);
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

                // Загружаем изображение, если есть код
                if (kodInt > 0)
                {
                    await Task.Run(() => LoadGridControlData(pictureBox2, kodInt));
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка в gridView_unboundArts_FocusedRowChanged_Internal");
                // В случае ошибки очищаем данные
                if (_myDataAnnBindingSource != null)
                {
                    _myDataAnnList.Clear();
                    _myDataAnnBindingSource.ResetBindings(false);
                }
                gridView_wdToBind.RefreshData();
                gridControl_wdToBind.RefreshDataSource();
                _normRaszListArticles.Clear(); // Clear in case of error
                _normRaszBindingSourceArticles.ResetBindings(false);
            }
        }
    }
}

