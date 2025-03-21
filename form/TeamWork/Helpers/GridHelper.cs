using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Interfaces;
using SewingProduction.Models;
using SewingProduction.Services;

namespace SewingProduction.Helpers
{
    public class GridHelper
    {
        private readonly HybridLogger _logger = new HybridLogger();
        
        #region Асинхронные методы
        /// <summary>
        /// Загружает данные в `GridControl` через `BindingSource` асинхронно.
        /// </summary>
        /// <param name="grid">GridControl, в который загружаются данные</param>
        /// <param name="source">BindingSource для привязки данных</param>
        /// <param name="data">DataTable с данными</param>
        public static async Task LoadGridControlDataAsync(GridControl grid, BindingSource source, DataTable data)
        {
            await Task.Run(() =>
            {
                // Подготовка данных в фоновом потоке
                var newData = data.Copy();

                grid.Invoke((MethodInvoker)(() =>
                {
                    source.DataSource = newData;
                    grid.DataSource = source;
                    grid.RefreshDataSource();
                }));
            });
        }

        /// <summary>
        /// Загружает данные из таблицы ANN асинхронно и обновляет интерфейс.
        /// </summary>
        /// <param name="bindingList">BindingList для хранения данных</param>
        /// <param name="bindingSource">BindingSource для привязки данных к элементам управления</param>
        /// <param name="annData">Список объектов ArtNormN</param>
        /// <param name="logger">Логгер для записи событий</param>
        public static async Task LoadCurrentDataAsync(BindingList<ArtNormN> bindingList, BindingSource bindingSource, List<ArtNormN> annData, HybridLogger logger)
        {
            try
            {
                // Очищаем текущий список
                if (bindingList != null)
                {
                    bindingList.Clear();
                }
                
                // Проверяем данные
                if (annData != null && annData.Count > 0)
                {
                    // Создаем новый BindingList, если текущий пуст или null
                    if (bindingList == null)
                    {
                        bindingList = new BindingList<ArtNormN>(annData);
                    }
                    else
                    {
                        // Добавляем данные в существующий BindingList
                        foreach (var item in annData)
                        {
                            bindingList.Add(item);
                        }
                    }
                }
                else
                {
                    await logger.LogEventAsync("Нет данных для загрузки из таблицы ANN", "LoadCurrentDataAsync");
                }
                
                // Обновляем BindingSource
                bindingSource.DataSource = bindingList;
                
                // Обновляем логгер
                await logger.LogEventAsync("Данные из таблицы ANN загружены успешно", "LoadCurrentDataAsync");
            }
            catch (Exception ex)
            {
                await logger.LogErrorAsync(ex, "Ошибка при загрузке данных из таблицы ANN");
                throw;
            }
        }
        
        /// <summary>
        /// Загружает данные из таблицы ANN и связанные данные по указанному идентификатору разделения труда.
        /// </summary>
        /// <param name="annId">Идентификатор разделения труда</param>
        /// <param name="artNormService">Сервис для работы с данными</param>
        /// <param name="controls">Словарь с GridControl'ами и их BindingSource'ами</param>
        /// <param name="pictureBox">PictureBox для загрузки изображения</param>
        /// <param name="annBindingSource">BindingSource для данных ANN</param>
        /// <param name="annBindingList">BindingList для данных ANN</param>
        /// <param name="logger">Логгер для записи событий</param>
        public static async Task LoadCurrentDataAsync(
            int annId, 
            IArtNormService artNormService, 
            Dictionary<GridControl, BindingSource> controls,
            PictureBox pictureBox,
            BindingSource annBindingSource,
            BindingList<ArtNormN> annBindingList,
            HybridLogger logger)
        {
            try
            {
                // Загружаем данные из таблицы ANN
                var data = await artNormService.GetArtNormData();
                annBindingList = new BindingList<ArtNormN>(data ?? new List<ArtNormN>());
                annBindingSource.DataSource = annBindingList;
                
                // Загружаем связанные данные для указанного annId
                foreach (var control in controls)
                {
                    GridControl grid = control.Key;
                    BindingSource source = control.Value;
                    
                    // Определяем тип данных для загрузки на основе имени GridControl
                    DataTable relatedData = null;
                    
                    if (grid.Name.Contains("normrasz", StringComparison.OrdinalIgnoreCase))
                    {
                        relatedData = await artNormService.GetRelatedNormRasz(annId);
                    }
                    else if (grid.Name.Contains("normrask", StringComparison.OrdinalIgnoreCase))
                    {
                        relatedData = await artNormService.GetRelatedNormRask(annId);
                    }
                    else if (grid.Name.Contains("normkont", StringComparison.OrdinalIgnoreCase))
                    {
                        relatedData = await artNormService.GetRelatedNormKont(annId);
                    }
                    else if (grid.Name.Contains("normdopobr", StringComparison.OrdinalIgnoreCase))
                    {
                        relatedData = await artNormService.GetRelatedNormDopObr(annId);
                    }
                    else if (grid.Name.Contains("sparticul", StringComparison.OrdinalIgnoreCase))
                    {
                        relatedData = await artNormService.GetRelatedSpArt(annId);
                    }
                    
                    // Загружаем данные в GridControl
                    if (relatedData != null)
                    {
                        await LoadGridControlDataAsync(grid, source, relatedData);
                    }
                }
                
                // Загружаем изображение
                await LoadImageAsync(pictureBox, await artNormService.GetImage(annId));
                
                // Логируем успешную загрузку
                await logger.LogEventAsync("Данные успешно загружены", "LoadCurrentDataAsync");
            }
            catch (Exception ex)
            {
                await logger.LogErrorAsync(ex, "Ошибка загрузки данных");
                throw;
            }
        }

        /// <summary>
        /// Загружает данные в список асинхронно.
        /// </summary>
        /// <param name="list">Список для загрузки данных</param>
        /// <param name="source">BindingSource для привязки данных</param>
        /// <param name="data">Список с данными</param>
        public static async Task LoadListDataAsync(List<MyDataART> list, BindingSource source, List<MyDataART> data)
        {
            await Task.Run(() =>
            {
                source.DataSource = data;
            });

            if (source.CurrencyManager?.Current is Control control && control.InvokeRequired)
            {
                control.Invoke((MethodInvoker)(() =>
                {
                    source.ResetBindings(false);
                }));
            }
            else
            {
                source.ResetBindings(false);
            }
        }

        /// <summary>
        /// Применяет фильтр к `GridControl` асинхронно.
        /// </summary>
        /// <param name="grid">GridControl, который нужно отфильтровать</param>
        /// <param name="annId">Идентификатор разделения труда</param>
        public static async Task ApplyFilterAsync(GridControl grid, int annId)
        {
            await Task.Run(() =>
            {
                string filter = "annId = " + annId;
                GridView view = (GridView)grid.Views[0];

                view.BeginUpdate();
                view.ActiveFilterString = filter;
                view.EndUpdate();
            });
        }

        /// <summary>
        /// Загружает изображение в `PictureBox` по `annId` асинхронно.
        /// </summary>
        /// <param name="pictureBox">PictureBox для загрузки изображения</param>
        /// <param name="data">DataTable с путем к изображению</param>
        public static async Task LoadImageAsync(PictureBox pictureBox, DataTable data)
        {
            await Task.Run(() =>
            {
                if (data != null && data.Rows.Count > 0)
                {
                    pictureBox.Invoke((MethodInvoker)(() =>
                    {
                        pictureBox.ImageLocation = data.Rows[0]["pathpict"].ToString();
                    }));
                }
            });
        }

        /// <summary>
        /// Возвращает имя выбранного столбца для поиска.
        /// </summary>
        /// <param name="kode">Радиокнопка "Код"</param>
        /// <param name="articul">Радиокнопка "Артикул"</param>
        /// <param name="model">Радиокнопка "Модель"</param>
        /// <param name="group">Радиокнопка "Группа"</param>
        /// <returns>Имя столбца для поиска</returns>
        public static Task<string> GetSelectedColumnNameAsync(bool kode, bool articul, bool model, bool group)
        {
            return Task.FromResult(
                kode ? "kod" :
                articul ? "articul" :
                model ? "mod" :
                group ? "grup" :
                string.Empty);
        }

        /// <summary>
        /// Проверяет, загружены ли данные в GridView.
        /// </summary>
        /// <param name="view">GridView для проверки</param>
        /// <returns>True, если данные загружены</returns>
        public static Task<bool> IsDataTableLoadedAsync(GridView view)
        {
            return Task.FromResult(view.DataSource is BindingSource bindingSource &&
                                   bindingSource.DataSource is DataTable dataTable &&
                                   dataTable.Rows.Count > 0);
        }

        /// <summary>
        /// Асинхронно снимает выделение всех строк, кроме текущей.
        /// </summary>
        /// <typeparam name="T">Тип данных объекта</typeparam>
        /// <param name="grid">GridControl</param>
        /// <param name="rowHandle">Индекс выбранной строки</param>
        public static async Task UpdateExclusiveCheckAsync<T>(GridControl grid, int rowHandle) where T : class
        {
            GridView gridView = grid.MainView as GridView;
            if (grid.InvokeRequired)
            {
                grid.Invoke(new MethodInvoker(async () => await UpdateExclusiveCheckAsync<T>(grid, rowHandle)));
                return;
            }

            await Task.Run(() =>
            {
                var selectedData = gridView.GetRow(rowHandle) as T;
                if (selectedData is ICheckable checkableSelectedData)
                {
                    checkableSelectedData.IsChecked = true;

                    for (int i = 0; i < gridView.RowCount; i++)
                    {
                        if (i != rowHandle)
                        {
                            var otherData = gridView.GetRow(i) as T;
                            if (otherData is ICheckable checkableOtherData && checkableOtherData.IsChecked)
                            {
                                checkableOtherData.IsChecked = false;
                            }
                        }
                    }
                }
                gridView.RefreshData();
            });
        }
        #endregion

        #region Настройки GridView
        /// <summary>
        /// Сохраняет настройки GridView в XML-файл.
        /// </summary>
        /// <param name="gridView">GridView для сохранения настроек</param>
        /// <param name="fileName">Имя файла для сохранения</param>
        public void SaveGridViewSettings(GridView gridView, string fileName)
        {
            try
            {
                string appPath = Application.StartupPath;
                string settingsPath = Path.Combine(appPath, "Settings");

                // Создаем директорию, если она не существует
                if (!Directory.Exists(settingsPath))
                    Directory.CreateDirectory(settingsPath);

                string fullPath = Path.Combine(settingsPath, fileName);
                
                // Сохраняем текущие настройки
                var storeAppearance = gridView.OptionsLayout.StoreAppearance;
                var storeFilter = gridView.OptionsLayout.StoreDataSettings;
                
                // Отключаем сохранение фильтров и поиска
                gridView.OptionsLayout.StoreAppearance = false;
                gridView.OptionsLayout.StoreDataSettings = false;
                
                // Сохраняем временный текущий фильтр поиска, чтобы восстановить его после
                var findFilterText = gridView.FindFilterText;
                // Очищаем текст поиска перед сохранением
                gridView.FindFilterText = string.Empty;
                
                // Сохраняем макет
                gridView.SaveLayoutToXml(fullPath);
                
                // Восстанавливаем предыдущие настройки
                gridView.OptionsLayout.StoreAppearance = storeAppearance;
                gridView.OptionsLayout.StoreDataSettings = storeFilter;
                
                // Восстанавливаем текст поиска
                gridView.FindFilterText = findFilterText;
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при сохранении настроек грида");
            }
        }

        /// <summary>
        /// Обработчик события изменения ширины столбца.
        /// </summary>
        /// <param name="sender">GridView</param>
        /// <param name="e">Параметры события</param>
        public void GridView_ColumnWidthChanged(object sender, ColumnEventArgs e)
        {
            if (sender is GridView view)
            {
                string fileName = $"{view.Name}Layout.xml";
                SaveGridViewSettings(view, fileName);
            }
        }

        /// <summary>
        /// Загружает настройки GridView из XML-файла.
        /// </summary>
        /// <param name="view">GridView для загрузки настроек</param>
        /// <param name="fileName">Имя файла с настройками</param>
        public void LoadGridViewSettings(GridView view, string fileName)
        {
            try
            {
                string appPath = Application.StartupPath;
                string settingsPath = Path.Combine(appPath, "Settings");
                string fullPath = Path.Combine(settingsPath, fileName);

                if (File.Exists(fullPath))
                {
                    // Сохраняем текущие настройки
                    var storeFilter = view.OptionsLayout.StoreDataSettings;
                    
                    // Отключаем загрузку фильтров
                    view.OptionsLayout.StoreDataSettings = false;
                    
                    // Загружаем макет
                    view.RestoreLayoutFromXml(fullPath);
                    
                    // Восстанавливаем предыдущие настройки
                    view.OptionsLayout.StoreDataSettings = storeFilter;
                    
                    // Очищаем любые возможные оставшиеся фильтры
                    view.ActiveFilterString = string.Empty;
                    view.ActiveFilterCriteria = null;
                    view.FindFilterText = string.Empty;
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при загрузке настроек грида");
            }
        }
        
        /// <summary>
        /// Очищает поисковый фильтр в GridView
        /// </summary>
        /// <param name="gridView">GridView для очистки</param>
        public void ClearSearchFilter(GridView gridView)
        {
            if (gridView == null) return;
            
            try
            {
                // Сохраняем актуальное состояние для восстановления фокуса
                int focusedRowHandle = gridView.FocusedRowHandle;
                
                // Очищаем поисковый фильтр
                gridView.FindFilterText = string.Empty;
                gridView.ClearFindFilter();
                
                // Восстанавливаем фокус, если был
                if (focusedRowHandle >= 0 && focusedRowHandle < gridView.RowCount)
                {
                    gridView.FocusedRowHandle = focusedRowHandle;
                }
            }
            catch (Exception ex)
            {
                // Используем экземпляр _logger
                _logger.LogErrorAsync(ex, "Ошибка при очистке поискового фильтра");
            }
        }
        
        /// <summary>
        /// Обработчик события смены RadioButton для поиска
        /// </summary>
        /// <param name="gridView">GridView, в котором выполняется поиск</param>
        public void OnSearchRadioButtonChanged(GridView gridView)
        {
            // Очищаем поисковый фильтр при смене радиокнопки
            ClearSearchFilter(gridView);
        }
        #endregion

        #region Синхронные методы
        /// <summary>
        /// Загружает данные в `GridControl` через `BindingSource`
        /// </summary>
        /// <param name="grid">GridControl, в который загружаются данные</param>
        /// <param name="source">BindingSource для привязки данных</param>
        /// <param name="data">DataTable с данными</param>
        public static void LoadGridControlData(GridControl grid, BindingSource source, DataTable data)
        {
            source.DataSource = data;
            grid.DataSource = source;
            grid.RefreshDataSource();
        }

        /// <summary>
        /// Загружает данные из таблицы ANN и обновляет интерфейс
        /// </summary>
        /// <param name="bindingList">BindingList для хранения данных</param>
        /// <param name="bindingSource">BindingSource для привязки данных к элементам управления</param>
        /// <param name="annData">Список объектов ArtNormN</param>
        public static void LoadCurrentData(BindingList<ArtNormN> bindingList, BindingSource bindingSource, List<ArtNormN> annData)
        {
            try
            {
                // Очищаем текущий список
                if (bindingList != null)
                {
                    bindingList.Clear();
                }
                
                // Проверяем данные
                if (annData != null && annData.Count > 0)
                {
                    // Создаем новый BindingList, если текущий пуст или null
                    if (bindingList == null)
                    {
                        bindingList = new BindingList<ArtNormN>(annData);
                    }
                    else
                    {
                        // Добавляем данные в существующий BindingList
                        foreach (var item in annData)
                        {
                            bindingList.Add(item);
                        }
                    }
                }
                
                // Обновляем BindingSource
                bindingSource.DataSource = bindingList;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при загрузке данных из таблицы ANN", ex);
            }
        }

        /// <summary>
        /// Применяет фильтр к `GridControl`
        /// </summary>
        /// <param name="grid">GridControl, который нужно отфильтровать</param>
        /// <param name="annId">Идентификатор разделения труда</param>
        public static void ApplyFilter(GridControl grid, int annId)
        {
            string filter = "annId = " + annId;
            GridView view = (GridView)grid.Views[0];
            view.BeginUpdate();
            view.ActiveFilterString = filter;
            view.EndUpdate();
        }

        /// <summary>
        /// Загружает изображение в `PictureBox` по `annId`
        /// </summary>
        /// <param name="pictureBox">PictureBox для загрузки изображения</param>
        /// <param name="data">DataTable с путем к изображению</param>
        public static void LoadImage(PictureBox pictureBox, DataTable data)
        {
            if (data != null && data.Rows.Count > 0)
            {
                pictureBox.ImageLocation = data.Rows[0]["pathpict"].ToString();
            }
        }

        /// <summary>
        /// Возвращает имя выбранного столбца для поиска
        /// </summary>
        /// <param name="kode">Радиокнопка "Код"</param>
        /// <param name="articul">Радиокнопка "Артикул"</param>
        /// <param name="model">Радиокнопка "Модель"</param>
        /// <param name="group">Радиокнопка "Группа"</param>
        /// <returns>Имя столбца для поиска</returns>
        public static string GetSelectedColumnName(bool kode, bool articul, bool model, bool group)
        {
            if (kode) return "kod";
            if (articul) return "articul";
            if (model) return "mod";
            if (group) return "grup";
            return string.Empty;
        }

        /// <summary>
        /// Проверяет, загружены ли данные в GridView
        /// </summary>
        /// <param name="view">GridView для проверки</param>
        /// <returns>True, если данные загружены</returns>
        public static bool IsDataTableLoaded(GridView view)
        {
            return view.DataSource is BindingSource bindingSource &&
                   bindingSource.DataSource is DataTable dataTable &&
                   dataTable.Rows.Count > 0;
        }

        /// <summary>
        /// Снимает выделение всех строк, кроме текущей
        /// </summary>
        /// <typeparam name="T">Тип данных (например, MyDataANN)</typeparam>
        /// <param name="gridView">GridView, в котором выполняется выделение</param>
        /// <param name="rowHandle">Индекс выбранной строки</param>
        public static void UpdateExclusiveCheck<T>(GridView gridView, int rowHandle) where T : class
        {
            var selectedData = gridView.GetRow(rowHandle) as T;
            if (selectedData is ICheckable checkableSelectedData)
            {
                checkableSelectedData.IsChecked = true;

                // Снимаем выделение с остальных строк
                for (int i = 0; i < gridView.RowCount; i++)
                {
                    if (i != rowHandle)
                    {
                        var otherData = gridView.GetRow(i) as T;
                        if (otherData is ICheckable checkableOtherData && checkableOtherData.IsChecked)
                        {
                            checkableOtherData.IsChecked = false;
                        }
                    }
                }
            }
            gridView.RefreshData();
        }
        #endregion
    }
}


