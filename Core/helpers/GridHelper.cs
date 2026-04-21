using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraDialogs.Adapters;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Skins;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Extensions;
using SewingProduction.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static SewingProduction.Helpers.GridHelper;

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




        #endregion
        #region добавление строки Автофильтр для GridView
        public void AutoRowFilterConfig(GridView gridView, int showColFilter)
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
                    column.OptionsFilter.AllowFilter = showColFilter == 1 ? true : false;
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
        #region прорисовка подытогов в строках группировки BandedGridView
        public enum GroupSummaryLevelMode
        {
            IncludeOnly,   // показывать ТОЛЬКО на указанных уровнях
            ExcludeOnly,   // показывать ВЕЗДЕ, КРОМЕ указанных уровней
            All,           // показывать на всех уровнях
            //None           // не обновлять никаких полей
        }
        private static bool ShouldDrawGroupSummary(
            int groupLevel,
            GroupSummaryLevelMode mode,
            IReadOnlyCollection<int> levels)
        {
            // если список уровней пуст
            if (levels == null || levels.Count == 0)
            {
                return mode switch
                {
                    GroupSummaryLevelMode.All => true,
                    GroupSummaryLevelMode.IncludeOnly => false,
                    GroupSummaryLevelMode.ExcludeOnly => true,
                    _ => true
                };
            }

            bool contains = levels.Contains(groupLevel);

            return mode switch
            {
                GroupSummaryLevelMode.All => true,
                GroupSummaryLevelMode.IncludeOnly => contains,
                GroupSummaryLevelMode.ExcludeOnly => !contains,
                _ => true
            };
        }
        //public void EnableGroupSummariesInGroupRow(BandedGridView view)
        //{
        //    view.CustomDrawGroupRow -= View_CustomDrawGroupRow;
        //    view.CustomDrawGroupRow += View_CustomDrawGroupRow;
        //}
        public sealed class GroupSummaryDrawOptions
        {
            public GroupSummaryLevelMode LevelMode { get; set; }
            public HashSet<int> Levels { get; set; } = new HashSet<int>();
        }
        public void EnableGroupSummariesInGroupRow(
            BandedGridView view,
            GroupSummaryLevelMode levelMode,
            IEnumerable<int> levels)
        {
            var options = new GroupSummaryDrawOptions
            {
                LevelMode = levelMode,
                Levels = levels != null
            ? new HashSet<int>(levels)
            : new HashSet<int>()
            };

            view.Tag = options;

            view.CustomDrawGroupRow -= View_CustomDrawGroupRow;
            view.CustomDrawGroupRow += View_CustomDrawGroupRow;

            view.Invalidate();
        }
        private void View_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            var view = (BandedGridView)sender;
            var viewInfo = view.GetViewInfo() as BandedGridViewInfo;
            if (viewInfo == null)
                return;

            var options = view.Tag as GroupSummaryDrawOptions
            ?? new GroupSummaryDrawOptions
            {
                LevelMode = GroupSummaryLevelMode.All
            };

            int groupLevel = view.GetRowLevel(e.RowHandle);

            // ─────────────────────────────────────────────
            // ▶▶ Проверяем: нужно ли рисовать итоги на этом уровне
            // ─────────────────────────────────────────────
            bool drawSummary = ShouldDrawGroupSummary(
                groupLevel,
                options.LevelMode,
                options.Levels
            );

            Rectangle rowRect = e.Bounds;

            // ───────────────────────────────────────────────────
            // ▶▶ 1. РИСУЕМ ФОН СТРОКИ ГРУППЫ КАК ЗАГОЛОВОК
            // ───────────────────────────────────────────────────

            // Берем skin и элемент Column Header
            Skin skin = GridSkins.GetSkin(view.GridControl.LookAndFeel);
            // Реальный элемент заголовка колонки
            SkinElement headerElement = skin[GridSkins.SkinHeader];

            if (headerElement == null)
                headerElement = skin[GridSkins.SkinHeader];

            if (headerElement != null)
            {
                var headerInfo = new SkinElementInfo(headerElement, rowRect);
                // Используем ObjectPainter + SkinElementPainter.Default
                using (var cache = new GraphicsCache(e.Graphics))
                {
                    ObjectPainter.DrawObject(cache, SkinElementPainter.Default, headerInfo);
                }
            }

            // ───────────────────────────────────────────────────
            // ▶▶ 2. Далее рисуем СТАНДАРТНЫЙ ТЕКСТ ГРУППЫ поверх фона
            // ───────────────────────────────────────────────────

            e.Appearance.BackColor = Color.Transparent;
            e.Appearance.Options.UseBackColor = false;
            e.Painter.DrawObject(e.Info);

            // ─────────────────────────────────────────────
            // ▶▶ 3. Если на этом уровне итоги не нужны — выходим
            // ─────────────────────────────────────────────
            if (!drawSummary)
            {
                e.Handled = true;
                return;
            }

            // ─────────────────────────────────────────────
            // ▶▶ 4. Рисуем итоги под колонками
            // ─────────────────────────────────────────────
            int top = rowRect.Top;           // верх строки группы
            int bottom = rowRect.Bottom - 1; // -1, чтобы не вылезти за границу

            foreach (BandedGridColumn col in view.VisibleColumns)
            {
                //var summaryItem = view.GroupSummary
                //    .OfType<GridGroupSummaryItem>()
                //    .FirstOrDefault(s => s.FieldName == col.FieldName);
                var summaryItem = view.GroupSummary
                    .OfType<GridGroupSummaryItem>()
                    .FirstOrDefault(s =>
                        s.ShowInGroupColumnFooter == col
                    );

                if (summaryItem == null)
                    continue;

                var colInfo = viewInfo.ColumnsInfo[col];
                if (colInfo == null)
                    continue;

                Rectangle colRect = colInfo.Bounds;

                //Rectangle cellRect = new Rectangle(
                //    colRect.X - 1,
                //    offsetY,
                //    colRect.Width,
                //    summaryHeight
                //);
                Rectangle cellRect = new Rectangle(
                    colRect.X - 1,
                    top - 1,
                    colRect.Width,
                    bottom - top + 1
                );

                object val = view.GetGroupSummaryValue(e.RowHandle, summaryItem);
                if (val == null || val == DBNull.Value)
                    continue;

                string text = !string.IsNullOrEmpty(summaryItem.DisplayFormat)
                    ? string.Format(summaryItem.DisplayFormat, val)
                    : Convert.ToDecimal(val).ToString("n2");

                // 1. Рисуем текст подытога
                using (var sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter
                })
                {
                    e.Graphics.DrawString(
                        text,
                        e.Appearance.Font,
                        new SolidBrush(e.Appearance.ForeColor),
                        cellRect,
                        sf
                    );
                }

                // 2. Тонкая светлая граница в стиле DevExpress
                //Color borderColor = ControlPaint.LightLight(view.Appearance.HeaderPanel.BackColor);
                Color borderColor = Color.FromArgb(80, ControlPaint.LightLight(view.Appearance.HeaderPanel.BackColor));

                using (var pen = new Pen(borderColor))
                {
                    pen.Width = 1;
                    e.Graphics.DrawRectangle(pen, cellRect);
                }
            }

            e.Handled = true;
        }
        //показывать итоги Только на 2-м уровне
        //EnableGroupSummariesInGroupRow(
        //    advBandedGridViewSmenZadany,
        //    GroupSummaryLevelMode.OnlySpecified,
        //    new[] { 1 }
        //);

        //Везде, кроме первого
        //EnableGroupSummariesInGroupRow(
        //    advBandedGridViewSmenZadany,
        //    GroupSummaryLevelMode.ExceptSpecified,
        //    new[] { 0 }
        //);

        //На всех уровнях
        //EnableGroupSummariesInGroupRow(
        //    advBandedGridViewSmenZadany,
        //    GroupSummaryLevelMode.All,
        //    null
        //);
#endregion
/// <summary>
/// переход к строке gridview по ID в bindingsource
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TKey"></typeparam>
/// <param name="view"></param>
/// <param name="bindingSource"></param>
/// <param name="idSelector"></param>
/// <param name="idValue"></param>
/// <param name="columnFieldName"></param>
    public void GoToRowById<T, TKey>(
        GridView view,
        BindingSource bindingSource,
        Func<T, TKey> idSelector,       // поле идентификатора для позиционирования
        TKey idValue,                   // значение идентификатора
        string? columnFieldName = null  // колонка, на которую нужно перенести фокус
        )
        {
            if (view == null || bindingSource == null)
                return;

            // 1) Индекс элемента в BindingSource
            int dataIndex = -1;
            //for (int i = 0; i < bindingSource.Count; i++)
            //{
            //    if (bindingSource[i] is T item &&
            //        EqualityComparer<TKey>.Default.Equals(idSelector(item), idValue))
            //    {
            //        dataIndex = i;
            //        break;
            //    }
            //}
            for (int i = 0; i < bindingSource.Count; i++)
            {
                var obj = bindingSource[i];

                Debug.WriteLine($"[{i}] type = {obj?.GetType().FullName}");

                if (obj is T item)
                {
                    var val = idSelector(item);
                    Debug.WriteLine($"    id = {val}");

                    if (EqualityComparer<TKey>.Default.Equals(val, idValue))
                    {
                        dataIndex = i;
                        break;
                    }
                }
            }

            if (dataIndex < 0)
                return;

            // 2) RowHandle с учетом сортировки/фильтра/группировки
            int rowHandle = view.GetRowHandle(dataIndex);
            if (rowHandle < 0)
                return;

            // 3) Раскрываем родительские группы
            RestoreExpandParentGroups(view, rowHandle);

            // 4) Фокусируем строку (+ колонку, если задана)
            view.BeginUpdate();
            try
            {
                view.ClearSelection();
                view.MakeRowVisible(rowHandle);
                view.FocusedRowHandle = rowHandle;

                if (!string.IsNullOrWhiteSpace(columnFieldName))
                {
                    var col = view.Columns.ColumnByFieldName(columnFieldName);
                    if (col != null)
                        view.FocusedColumn = col;
                }
            }
            finally
            {
                view.EndUpdate();
            }
        }

        private void ExpandParentGroups(GridView view, int rowHandle)
        {
            int parent = view.GetParentRowHandle(rowHandle);
            while (parent != GridControl.InvalidRowHandle)
            {
                view.SetRowExpanded(parent, true);
                parent = view.GetParentRowHandle(parent);
            }
        }
        //// только строка
        //GoToRowById<PZVOperList, int>(gridViewPZVOperList, bsPzv, x => x.olPzvID, _pzvID);

        //// строка + колонка
        //GoToRowById<PZVOperList, int>(gridViewPZVOperList, bsPzv, x => x.olPzvID, _pzvID, _column);

        /// <summary>
        /// Присвоение значения в поле грида по всем строкам. Если на грид наложен фильтр, то только для строк, попадающих в фильтр
        /// </summary>
        /// <param name="_view"></param>
        /// <param name="_grIDColumn"></param>
        /// <param name="_newValue"></param>
        public void SetValueForFilteredRecordsInGrid<T>(GridView _view, GridColumn _grIDColumn, T _newValue)
        {
            Debug.WriteLine($"SetIntValueForFilteredRecordsInGrid started or GridView={_view}, GridColumn={_grIDColumn}, newValue = {_newValue}");
            _view.BeginUpdate();
            _view.GridControl.BeginUpdate();
            try
            {
                Enumerable.Range(0, _view.RowCount)
                .Where(_view.IsDataRow) // отсекаем group rows и прочие
                .ToList()
                .ForEach(rh =>
                {
                    _view.SetRowCellValue(rh, _grIDColumn, _newValue);
                    //view.PostEditor();
                });
                _view.PostEditor();
                _view.UpdateCurrentRow();
            }
            finally
            {
                _view.GridControl.EndUpdate();
                _view.EndUpdate();
            }
            Debug.WriteLine($"SetIntValueForFilteredRecordsInGrid completed or GridView={_view}, GridColumn={_grIDColumn}, newValue = {_newValue}");
        }
        /// <summary>
        /// Фокусирует строку и аккуратно так, чтобы она была примерно посередине экрана
        /// </summary>
        /// <param name="view"></param>
        /// <param name="rowHandle"></param>
        public void FocusAndScrollToRow(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            view.FocusedRowHandle = rowHandle;

            // 1) гарантируем, что строка станет видимой
            view.MakeRowVisible(rowHandle);

            // 2) аккуратно прокручиваем вверх так, чтобы строка была не в самом низу
            int visibleIndex = view.GetVisibleIndex(rowHandle);
            if (visibleIndex < 0) return;

            int rowsOnScreen = Math.Max(1, view.GridControl.Height / view.RowHeight);
            int targetTop = Math.Max(0, visibleIndex - rowsOnScreen / 2);

            view.TopRowIndex = targetTop;
        }
        public sealed class GridViewState
        {
            public string? FocusedColumnFieldName { get; set; }
            public int TopRowIndex { get; set; } = -1;
            public string? FocusedRowKey { get; set; }
            public List<string> ExpandedGroupKeys { get; set; } = new();
        }

        public GridViewState CaptureState<T>(
            GridView view,
            BindingSource bindingSource,
            Func<T, string> rowKeySelector)
        {
            if (view == null) throw new ArgumentNullException(nameof(view));
            if (bindingSource == null) throw new ArgumentNullException(nameof(bindingSource));
            if (rowKeySelector == null) throw new ArgumentNullException(nameof(rowKeySelector));

            var state = new GridViewState
            {
                FocusedColumnFieldName = view.FocusedColumn?.FieldName,
                TopRowIndex = view.TopRowIndex,
                ExpandedGroupKeys = CaptureExpandedGroups(view),
                FocusedRowKey = view.GetFocusedRow() is T focusedRow
                    ? rowKeySelector(focusedRow)
                    : null
            };

            //if (bindingSource.Current is T currentRow)
            //    state.FocusedRowKey = rowKeySelector(currentRow);

            return state;
        }

        public void RestoreState<T>(
            GridView view,
            BindingSource bindingSource,
            GridViewState state,
            Func<T, string> rowKeySelector)
        {
            if (view == null) throw new ArgumentNullException(nameof(view));
            if (bindingSource == null) throw new ArgumentNullException(nameof(bindingSource));
            if (state == null) throw new ArgumentNullException(nameof(state));
            if (rowKeySelector == null) throw new ArgumentNullException(nameof(rowKeySelector));

            view.BeginUpdate();
            try
            {
                RestoreExpandedGroups(view, state.ExpandedGroupKeys);

                if (!string.IsNullOrWhiteSpace(state.FocusedRowKey))
                {
                    int index = -1;

                    for (int i = 0; i < bindingSource.Count; i++)
                    {
                        if (bindingSource[i] is T item && rowKeySelector(item) == state.FocusedRowKey)
                        {
                            index = i;
                            break;
                        }
                    }

                    if (index >= 0)
                    {
                        bindingSource.Position = index;

                        int rowHandle = view.GetRowHandle(index);
                        if (rowHandle >= 0)
                        {
                            RestoreExpandParentGroups(view, rowHandle);
                            view.FocusedRowHandle = rowHandle;
                            view.MakeRowVisible(rowHandle);
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(state.FocusedColumnFieldName))
                {
                    var col = view.Columns.ColumnByFieldName(state.FocusedColumnFieldName);
                    if (col != null)
                        view.FocusedColumn = col;
                }

                if (state.TopRowIndex >= 0)
                    view.TopRowIndex = state.TopRowIndex;
            }
            finally
            {
                view.EndUpdate();
            }
        }

        //public List<string> CaptureExpandedGroups(GridView view)
        //{
        //    var result = new List<string>();

        //    for (int rowHandle = 0; rowHandle < view.RowCount; rowHandle++)
        //    {
        //        if (!view.IsGroupRow(rowHandle))
        //            continue;

        //        if (view.GetRowExpanded(rowHandle))
        //            result.Add(BuildGroupPath(view, rowHandle));
        //    }

        //    return result;
        //}
        public List<string> CaptureExpandedGroups(GridView view)
        {
            if (view == null) throw new ArgumentNullException(nameof(view));

            var result = new List<string>();

            for (int visibleIndex = 0; visibleIndex < view.RowCount; visibleIndex++)
            {
                int rowHandle = view.GetVisibleRowHandle(visibleIndex);

                if (!view.IsGroupRow(rowHandle))
                    continue;

                if (view.GetRowExpanded(rowHandle))
                    result.Add(BuildGroupPath(view, rowHandle));
            }

            return result;
        }
        //public void RestoreExpandedGroups(GridView view, List<string> expandedGroupKeys)
        //{
        //    if (view == null) throw new ArgumentNullException(nameof(view));
        //    //view.CollapseAllGroups();
        //    if (expandedGroupKeys == null || expandedGroupKeys.Count == 0)
        //        return;

        //    var expandedSet = expandedGroupKeys.ToHashSet();

        //    for (int rowHandle = 0; rowHandle < view.RowCount; rowHandle++)
        //    {
        //        if (!view.IsGroupRow(rowHandle))
        //            continue;

        //        string path = BuildGroupPath(view, rowHandle);
        //        //bool shouldExpand = expandedGroupKeys.Contains(path);

        //        //view.SetRowExpanded(rowHandle, shouldExpand);
        //        if (expandedSet.Contains(path))
        //            view.SetRowExpanded(rowHandle, true);
        //    }
        //}
        public void RestoreExpandedGroups(GridView view, List<string> expandedGroupKeys)
        {
            if (view == null) throw new ArgumentNullException(nameof(view));

            view.CollapseAllGroups();

            if (expandedGroupKeys == null || expandedGroupKeys.Count == 0)
                return;

            var expandedSet = expandedGroupKeys.ToHashSet();

            for (int visibleIndex = 0; visibleIndex < view.RowCount; visibleIndex++)
            {
                int rowHandle = view.GetVisibleRowHandle(visibleIndex);

                if (!view.IsGroupRow(rowHandle))
                    continue;

                string path = BuildGroupPath(view, rowHandle);
                if (expandedSet.Contains(path))
                    view.SetRowExpanded(rowHandle, true);
            }
        }
        public void RestoreExpandParentGroups(GridView view, int rowHandle)
        {
            int parent = view.GetParentRowHandle(rowHandle);

            while (parent != GridControl.InvalidRowHandle)
            {
                view.SetRowExpanded(parent, true);
                parent = view.GetParentRowHandle(parent);
            }
        }

        public string BuildGroupPath(GridView view, int groupRowHandle)
        {
            var parts = new Stack<string>();
            int current = groupRowHandle;

            //while (current != GridControl.InvalidRowHandle && view.IsGroupRow(current))
            //{
            //    int level = view.GetRowLevel(current);
            //    string groupText = Convert.ToString(view.GetGroupRowValue(current)) ?? string.Empty;
            //    parts.Push($"{level}:{groupText}");

            //    current = view.GetParentRowHandle(current);
            //}
            while (current != GridControl.InvalidRowHandle && view.IsGroupRow(current))
            {
                int level = view.GetRowLevel(current);

                string fieldName = view.GroupedColumns.Count > level
                    ? view.GroupedColumns[level].FieldName
                    : $"Level{level}";

                object groupValue = view.GetGroupRowValue(current);
                string valueText = Convert.ToString(groupValue) ?? string.Empty;

                parts.Push($"{level}:{fieldName}={valueText}");

                current = view.GetParentRowHandle(current);
            }
            return string.Join("|", parts);
        }
    }
}


