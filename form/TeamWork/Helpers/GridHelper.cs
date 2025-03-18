using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Interfaces;
using SewingProduction.Models;

namespace SewingProduction.Helpers
{
    public  class GridHelper
    {
        public readonly HybridLogger _logger = new HybridLogger();
        #region async
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

        #region GridColumnSettings
        // Сохранение настроек грида
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
                gridView.SaveLayoutToXml(fullPath);
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при сохранении настроек грида");
            }
        }

        public void GridView_ColumnWidthChanged(object sender, ColumnEventArgs e)
        {
            if (sender is GridView view)
            {
                string fileName = $"{view.Name}Layout.xml";
                SaveGridViewSettings(view, fileName);
            }
        }

        public void LoadGridViewSettings(GridView view, string fileName)
        {
            try
            {
                string appPath = Application.StartupPath;
                string settingsPath = Path.Combine(appPath, "Settings");
                string fullPath = Path.Combine(settingsPath, fileName);

                if (File.Exists(fullPath))
                    view.RestoreLayoutFromXml(fullPath);
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при загрузке настроек грида");
            }
        }
        #endregion

        #region sync
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


