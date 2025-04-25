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
            //загрузка артикулов для увязки (актуальные и предварительные)
            await MyDataArtLoad();
            //загрузка  таблицы РТ для увязки (текущие работы)
            await MyDataAnnLoad();
            //norm_rasz
            await NormRaszLoad();
            await PreArchLoad();
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

                _myDataArtList.Clear(); // Очищаем BindingList

                if (loadedData != null)
                {
                    foreach (var item in loadedData)
                    {
                        _myDataArtList.Add(item); // Добавляем элементы в BindingList
                    }
                }

                _myDataArtBindingSource.ResetBindings(false); // Уведомляем BindingSource (и грид) об изменениях

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

                int kod = GetSelectedKodFromGrid(customGridControl1); // Убедитесь, что этот метод корректно работает с BindingSource
                List<MyDataANN> loadedData = await _artNormService.GetArtNormDataCurrent(kod, loadAllCheckBox.Checked);

                _myDataAnnList.Clear(); // Очищаем BindingList

                if (loadedData != null && loadedData.Count > 0)
                {
                    foreach (var item in loadedData)
                    {
                        _myDataAnnList.Add(item); // Добавляем элементы в BindingList
                    }
                    await _logger.LogEventAsync($"Загружено {_myDataAnnList.Count} записей MyDataANN (kod: {kod}, loadAll: {loadAllCheckBox.Checked}).", "MyDataAnnLoad");
                }
                else
                {
                    // Логгируем, если данных нет, вместо MessageBox
                    await _logger.LogEventAsync($"Нет данных MyDataANN для загрузки (kod: {kod}, loadAll: {loadAllCheckBox.Checked})", "MyDataAnnLoad");
                }

                _myDataAnnBindingSource.ResetBindings(false); // Уведомляем BindingSource (и грид) об изменениях
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

        // Убедитесь, что GetSelectedKodFromGrid работает правильно с BindingSource
        // Возможно, он уже работает, т.к. GetRowCellValue часто универсален.
        // Если нет, его нужно будет адаптировать. Примерно так:
        private int GetSelectedKodFromGrid(GridControl grid)
        {
            var view = grid.MainView as GridView;
            if (view != null && view.FocusedRowHandle >= 0)
            {
                // Получаем объект данных для строки (MyDataART)
                var rowData = view.GetRow(view.FocusedRowHandle) as MyDataART;
                if (rowData != null)
                {
                    // Пытаемся получить Kod из объекта
                    if (int.TryParse(rowData.Kod, out int kodValue)) // Предполагая, что Kod это string
                    {
                        return kodValue;
                    }
                    // Если Kod другого типа (например, int), то просто return rowData.Kod;
                    // return rowData.Kod; // Если Kod - это int
                }
                // Можно оставить старый вариант как запасной, если GetRow не сработает
                // return Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "Kod"));
            }
            return 0;
        }




        //private async Task MyDataArtLoad()
        //{
        //    try
        //    {
        //        string query = "SELECT DISTINCT SUBSTRING(kod,1,7) as kod, grup, articul, mod, annId FROM sp_articul WHERE annID IS NULL";
        //        List<MyDataART> relatedData = await _dbService.GetListAsync<MyDataART>(query, null);
        //        customGridControl1.DataSource = relatedData;
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных в текущие работы: {ex.Message}");
        //        MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        //private async Task MyDataAnnLoad()
        //{
        //    try
        //    {
        //        int kod = GetSelectedKodFromGrid(customGridControl1);
        //        List<MyDataANN> artNormNs = await _artNormService.GetArtNormDataCurrent(kod, loadAllCheckBox.Checked);
               
        //        if (artNormNs != null)
        //        {
        //            //   var relatedMyDataAnn = ConvertToMyDataAnn(artNormNs);
        //            customGridControl3.DataSource = artNormNs;// relatedMyDataAnn;
        //        }
        //        else
        //        {
        //            MessageBox.Show("Нет данных для загрузки.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных в текущие работы: {ex.Message}");
        //    }
        //}
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
                List<MyDataANN> relatedData;
                bool loadAll = loadAllCheckBox.Checked;
                // Если включен чекбокс "Загрузить все"
                relatedData = await _artNormService.GetArtNormDataCurrent(kod, loadAll);
                if (!loadAll)
                { // Если артикул содержит "-", фильтруем по его первой части
                    int dashIndex = articul.IndexOf("-");
                    if (dashIndex > 0)
                    {
                        string artPrefix = articul.Substring(0, dashIndex);
                        List<MyDataANN> partialData = await _artNormService.GetArtNormDataByArticulPrefix(artPrefix);
                        if (partialData != null)
                            relatedData.AddRange(partialData);
                    }
                }

                await _logger.LogEventAsync($"Успешная загрузка РТ для кода {kod} и артикула {articul}", "LoadWorksbyArt");
                return relatedData;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки РТ для кода {kod} и артикула {articul}");
                MessageBox.Show("Ошибка загрузки данных. Подробности в логе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<MyDataANN>(); // Возвращаем пустой список в случае ошибки
            }

        }

        //private int GetSelectedKodFromGrid(GridControl grid)
        //{
        //    var view = grid.MainView as GridView;
        //    if (view != null && view.FocusedRowHandle >= 0)
        //    {
        //        return Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "Kod"));
        //    }
        //    return 0;
        //}
        async Task NormRaszLoad()
        {
            int annId = 0;
            var view = customGridControl2.MainView as GridView;
            if (view != null)
            {
                annId = Convert.ToInt32(view.GetRowCellValue(0, "AnnId"));
            }
            var relatedRasz = await _artNormService.GetRelatedNormRasz(annId);
            normraszBindingSource1.DataSource = relatedRasz;
            customGridControl3.DataSource = normraszBindingSource1;
        }

        ///// <summary>
        ///// загрузка norm_rasz
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private async void gridView8_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            int annId = CommonFunctions.GetRowCellValueOrDefault<int>(gridView8, e.FocusedRowHandle, "AnnId", 0);
            await GridHelper.LoadGridControlDataAsync(customGridControl3, normraszBindingSource, await _artNormService.GetRelatedNormRasz(annId));

        }
        private async Task PreArchLoad()
        {
            try
            {
                string query = "SELECT * FROM artNormNView WHERE status = 4"; // Ваш запрос
                List<MyDataANN> preArchData = await _dbService.GetListAsync<MyDataANN>(query, null);

                // Убедимся, что список и BindingSource существуют (они должны быть инициализированы в TeamWork.cs)
                if (_preArchList == null || _preArchBindingSource == null)
                {
                    await _logger.LogErrorAsync(new NullReferenceException("_preArchList or _preArchBindingSource is null"), "PreArchLoad failed initialization check.");
                    MessageBox.Show("Ошибка инициализации списка предварительного архива.", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _preArchList.Clear(); 

                if (preArchData != null)
                {
                    foreach (var item in preArchData)
                    {
                        _preArchList.Add(item);
                    }
                }
                _preArchBindingSource.ResetBindings(false);

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
                GridView artView = customGridControl1.MainView as GridView;
                GridView annView = customGridControl2.MainView as GridView;

                if (artView == null || annView == null)
                {
                    MessageBox.Show("Данные не загружены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                string selectedArt = "";
                int selectedAnn = -1;
                MyDataART selectedArtRow = null;
                MyDataANN selectedAnnRow = null;
                // Получаем выбранные артикулы
                List<MyDataART> artDataSource = artView.DataSource as List<MyDataART>;
                if (artDataSource != null)
                    foreach (var row in artDataSource)
                    {
                        if (row.IsChecked)
                        {
                            selectedArt = row.Kod;
                            selectedArtRow = row;
                            break;
                        }
                    }

                // Получаем выбранное разделение труда
                List<MyDataANN> annDataSource = annView.DataSource as List<MyDataANN>;
                if (annDataSource != null)
                    foreach (var row in annDataSource)
                    {
                        if (row.IsChecked)
                        {
                            selectedAnn = row.AnnId;
                            selectedAnnRow = row;
                            break;
                        }
                    }

                // Проверяем, выбраны ли оба элемента
                if (selectedArt == "" || selectedAnn == -1)
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
                _artNormService.UpdateAnnIdinArticul(selectedArt, selectedAnn);

                // Обновляем UI
                if (selectedArtRow != null && selectedAnnRow != null)
                {
                    selectedArtRow.BindedArt = selectedAnnRow.Articul;

                    // Перемещаем строку в начало списка
                    List<MyDataART> artDataSourceList = artDataSource;
                    if (artDataSourceList != null)
                    {
                        int index = artDataSourceList.IndexOf(selectedArtRow);
                        if (index > 0) 
                        {
                            artDataSourceList.RemoveAt(index);
                            artDataSourceList.Insert(0, selectedArtRow);
                            customGridControl1.RefreshDataSource();
                           
                             // фокус на перемещенной строке
                             if (artView.RowCount > 0) artView.FocusedRowHandle = 0; 
                        }
                        else
                        { 
                            artView.RefreshRow(artView.FocusedRowHandle); 
                        }
                    }
                    else
                    {
                        artView.RefreshData(); 
                    }

                    annView.RefreshData();
                }

                await _logger.LogEventAsync("Привязка завершена", $"Артикул {selectedArt} привязан к РТ {selectedAnn}");
                MessageBox.Show("Привязка успешно выполнена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при привязке артикула к РТ");
                MessageBox.Show($"Ошибка при привязке артикула: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
