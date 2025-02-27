using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Interfaces;

namespace SewingProduction.Helpers
{
    public static class GridHelper
    {
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

    }
}


