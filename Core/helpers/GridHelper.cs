using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Extensions;
using SewingProduction.Interfaces;

namespace SewingProduction.Helpers
{
    public class GridHelper
    {
        public readonly FileLogger _logger = new FileLogger();
        #region async
        /// <summary>
        /// Загружает данные в `GridControl` через `BindingSource` асинхронно.
        /// </summary>
        /// <param name="grid">GridControl, в который загружаются данные</param>
        /// <param name="source">BindingSource для привязки данных</param>
        /// <param name="data">DataTable с данными</param>
        public static async Task LoadGridControlDataAsync<T>(GridControl grid, BindingSource source, List<T> data)
        {
            await Task.Run(() =>
            {
                grid.Invoke((MethodInvoker)(() =>
                {
                    source.DataSource = data;
                    grid.DataSource = source;
                    grid.RefreshDataSource();
                }));
            });
        }

        //public static async Task LoadListDataAsync(List<MyDataART> list, BindingSource source, List<MyDataART> data)
        //{
        //    await Task.Run(() =>
        //    {
        //        source.DataSource = data;
        //    });

        //    if (source.CurrencyManager?.Current is Control control && control.InvokeRequired)
        //    {
        //        control.Invoke((MethodInvoker)(() =>
        //        {
        //            source.ResetBindings(false);
        //        }));
        //    }
        //    else
        //    {
        //        source.ResetBindings(false);
        //    }
        //}

        public static async Task LoadListDataAsync<T>(GridControl grid, BindingSource source, List<T> dataList)
        {
            if (grid.InvokeRequired)
            {
                // Мы не в UI-потоке → оборачиваем всё в Invoke
                await grid.InvokeAsync(() =>
                {
                    source.DataSource = dataList;
                    grid.DataSource = source;
                    grid.RefreshDataSource();
                });
            }
            else
            {
                // Уже в UI-потоке
                source.DataSource = dataList;
                grid.DataSource = source;
                grid.RefreshDataSource();
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
                kode ? "Kod" :
                articul ? "Articul" :
                model ? "Mod" :
                group ? "Group" :
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
        //public void SaveGridViewSettings(GridView gridView, string fileName)
        //{
        //    try
        //    {
        //        string appPath = System.Windows.Forms.Application.StartupPath;
        //        string settingsPath = Path.Combine(appPath, "Settings");

        //        // Create directory if it doesn't exist
        //        if (!Directory.Exists(settingsPath))
        //            Directory.CreateDirectory(settingsPath);

        //        string fullPath = Path.Combine(settingsPath, fileName);

        //        // Create XML document with column settings
        //        //using (XmlWriter writer = XmlWriter.Create(fullPath))
        //        //{
        //        //    writer.WriteStartDocument();
        //        //    writer.WriteStartElement("GridViewLayout");

        //        //    writer.WriteStartElement("Columns");
        //        //    foreach (GridColumn column in gridView.Columns)
        //        //    {
        //        //        writer.WriteStartElement("Column");
        //        //        writer.WriteAttributeString("FieldName", column.FieldName);
        //        //        writer.WriteAttributeString("Width", column.Width.ToString());
        //        //        writer.WriteAttributeString("VisibleIndex", column.VisibleIndex.ToString());
        //        //        writer.WriteAttributeString("Visible", column.Visible.ToString());
        //        //        writer.WriteEndElement(); // Column
        //        //    }
        //        //    writer.WriteEndElement(); // Columns

        //        //    writer.WriteEndElement(); // GridViewLayout
        //        //    writer.WriteEndDocument();
        //        //}
        //        // Сохраняем текущие настройки: запоминаем опции для сохранения внешнего вида и настроек данных
        //        var storeAppearance = gridView.OptionsLayout.StoreAppearance;
        //        var storeDataSettings = gridView.OptionsLayout.StoreDataSettings;

        //        // Отключаем сохранение фильтров и поиска, чтобы не сохранить ненужные данные
        //        gridView.OptionsLayout.StoreAppearance = false;
        //        gridView.OptionsLayout.StoreDataSettings = false;

        //        // Сохраняем текущий текст поиска, чтобы потом его восстановить
        //        var findFilterText = gridView.FindFilterText;
        //        gridView.FindFilterText = string.Empty;

        //        // Сохраняем макет грида в XML
        //        gridView.SaveLayoutToXml(fullPath);

        //        // Восстанавливаем прежние настройки
        //        gridView.OptionsLayout.StoreAppearance = storeAppearance;
        //        gridView.OptionsLayout.StoreDataSettings = storeDataSettings;
        //        gridView.FindFilterText = findFilterText;
        //        // Сохраняем текущие значения для восстановления
        //        var storeVisualOptions = gridView.OptionsLayout.StoreVisualOptions;
        //         storeAppearance = gridView.OptionsLayout.StoreAppearance;
        //         storeDataSettings = gridView.OptionsLayout.StoreDataSettings;

        //        // Оставляем только визуальные настройки столбцов (размеры, порядок, видимость)
        //        gridView.OptionsLayout.StoreVisualOptions = true;
        //        gridView.OptionsLayout.StoreAppearance = false;
        //        gridView.OptionsLayout.StoreDataSettings = false;

        //        gridView.SaveLayoutToXml(fullPath);

        //        // Восстанавливаем прежние настройки
        //        gridView.OptionsLayout.StoreVisualOptions = storeVisualOptions;
        //        gridView.OptionsLayout.StoreAppearance = storeAppearance;
        //        gridView.OptionsLayout.StoreDataSettings = storeDataSettings;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogErrorAsync(ex, "Error saving grid view settings");
        //    }
        //}

        public void GridView_ColumnWidthChanged(object sender, ColumnEventArgs e)
        {
            if (sender is GridView view)
            {
                string fileName = $"{view.Name}Layout.xml";
                SaveGridViewSettings(view, fileName);
            }
        }

        //public void LoadGridViewSettings(GridView gridView, string fileName)
        //{
        //    try
        //    {
        //        string appPath = System.Windows.Forms.Application.StartupPath;
        //        string settingsPath = Path.Combine(appPath, "Settings");
        //        string fullPath = Path.Combine(settingsPath, fileName);

        //        if (!File.Exists(fullPath))
        //            return;

        //        using (XmlReader reader = XmlReader.Create(fullPath))
        //        {
        //            while (reader.Read())
        //            {
        //                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Column")
        //                {
        //                    string fieldName = reader.GetAttribute("FieldName");
        //                    string widthStr = reader.GetAttribute("Width");
        //                    string visibleIndexStr = reader.GetAttribute("VisibleIndex");
        //                    string visibleStr = reader.GetAttribute("Visible");

        //                    if (int.TryParse(widthStr, out int width) && 
        //                        int.TryParse(visibleIndexStr, out int visibleIndex) &&
        //                        bool.TryParse(visibleStr, out bool visible))
        //                    {
        //                        GridColumn column = gridView.Columns[fieldName];
        //                        if (column != null)
        //                        {
        //                            column.Width = width;
        //                            column.VisibleIndex = visibleIndex;
        //                            column.Visible = visible;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogErrorAsync(ex, "Error loading grid view settings");
        //    }
        //}

        public void SaveGridViewSettings(GridView gridView, string fileName)
        {
            try
            {
                string appPath = Application.StartupPath;
                string settingsPath = Path.Combine(appPath, "Settings");
                if (!Directory.Exists(settingsPath))
                    Directory.CreateDirectory(settingsPath);
                string fullPath = Path.Combine(settingsPath, fileName);
                List<XElement> columnElements = new List<XElement>();

                // Проходим по всем колонкам с помощью цикла
                for (int i = 0; i < gridView.Columns.Count; i++)
                {
                    GridColumn col = gridView.Columns[i];
                    if (!string.IsNullOrEmpty(col.FieldName))
                    {
                        columnElements.Add(
                            new XElement("Column",
                                new XAttribute("FieldName", col.FieldName),
                                new XAttribute("Width", col.Width),
                                new XAttribute("VisibleIndex", col.VisibleIndex),
                                new XAttribute("Visible", col.Visible),
                                new XAttribute("SortOrder", col.SortOrder.ToString()),
                                new XAttribute("SortIndex", col.SortIndex)
                                )
                        );
                    }
                }

                XElement columnsElement = new XElement("Columns", columnElements);
                // Формируем документ и сохраняем его в файл
                XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), columnsElement);
                doc.Save(fullPath);
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при сохранении настроек грида");
            }
        }

        public void LoadGridViewSettings(GridView gridView, string fileName)
        {
            try
            {
                string appPath = Application.StartupPath;
                string settingsPath = Path.Combine(appPath, "Settings");
                string fullPath = Path.Combine(settingsPath, fileName);

                if (!File.Exists(fullPath))
                    return;

                XDocument doc = XDocument.Load(fullPath);
                var columnElements = doc.Descendants("Column");

                foreach (var element in columnElements)
                {
                    string fieldName = element.Attribute("FieldName")?.Value;
                    if (string.IsNullOrEmpty(fieldName))
                        continue;

                    GridColumn column = gridView.Columns[fieldName];
                    if (column == null)
                        continue;

                    if (int.TryParse(element.Attribute("Width")?.Value, out int width))
                        column.Width = width;

                    if (int.TryParse(element.Attribute("VisibleIndex")?.Value, out int visibleIndex))
                        column.VisibleIndex = visibleIndex;

                    if (bool.TryParse(element.Attribute("Visible")?.Value, out bool visible))
                        column.Visible = visible;
                    string sortOrderStr = element.Attribute("SortOrder")?.Value;
                    if (!string.IsNullOrEmpty(sortOrderStr))
                    {
                        if (Enum.TryParse<DevExpress.Data.ColumnSortOrder>(sortOrderStr, out var sortOrder))
                        {
                            column.SortOrder = sortOrder;
                        }
                    }
                    if (int.TryParse(element.Attribute("SortIndex")?.Value, out int sortIndex))
                        column.SortIndex = sortIndex;
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Error loading grid view settings");
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
        public void AutoRowFilterConfig(GridView gridView)
        {
            if (gridView == null) return;

            try
            {
                // Основные настройки GridView
                gridView.OptionsView.ShowAutoFilterRow = true;
                gridView.OptionsCustomization.AllowFilter = true;
                //gridView.OptionsFilter.AllowColumnFilter = true;
                gridView.OptionsFilter.AllowFilterEditor = true;

                //// Улучшенные настройки фильтрации
                //gridView.OptionsFilter.ImmediateUpdateAutoFilter = false; // Отложенное обновление
                //gridView.OptionsFilter.AllowFilterEditorMenu = true; // Меню в редакторе фильтров

                // Настройка каждого столбца
                foreach (GridColumn column in gridView.Columns)
                {
                    if (!column.Visible) continue; // Пропускаем скрытые колонки

                    //column.OptionsFilter.AllowAutoFilter = true;
                    //column.OptionsFilter.AllowFilter = true;
                    column.OptionsFilter.AllowAutoFilter = true;
                    column.OptionsFilter.AllowFilter = false;
                    column.OptionsColumn.AllowSort = DefaultBoolean.False;
                    // Умная настройка условий фильтрации
                    SetColumnFilterCondition(column);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка настройки GridView: {ex.Message}");
            }
        }
        private static void SetColumnFilterCondition(GridColumn column)
        {
            if (column.ColumnType == typeof(string))
            {
                column.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
            }
            else if (column.ColumnType == typeof(DateTime))
            {
                column.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Equals;
                column.OptionsFilter.FilterPopupMode = FilterPopupMode.Date;
            }
            else if (column.ColumnType == typeof(bool))
            {
                column.OptionsFilter.FilterPopupMode = FilterPopupMode.CheckedList;
            }
            else if (IsNumericType(column.ColumnType))
            {
                column.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Equals;
            }
        }
        /// <summary>
        /// Вспомогательный метод для проверки числовых типов
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsNumericType(Type type)
        {
            return type == typeof(int) || type == typeof(double) || type == typeof(decimal)
                   || type == typeof(float) || type == typeof(long) || type == typeof(short);
        }
        #endregion
    }
}


