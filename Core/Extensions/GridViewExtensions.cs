using DevExpress.XtraGrid.Views.Grid;
using System.ComponentModel;

namespace SewingProduction.Extensions
{
    public static class GridViewExtensions
    {
        /// <summary>
        /// Запрещает редактирование данных в GridView. Строка автофильтра остаётся доступной.
        /// </summary>
        public static void ApplyReadOnly(this GridView gridView)
        {
            if (gridView == null)
            {
                return;
            }

            gridView.OptionsBehavior.Editable = false;
            gridView.OptionsBehavior.ReadOnly = true;

            foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridView.Columns)
            {
                column.OptionsColumn.AllowEdit = false;
                column.OptionsColumn.ReadOnly = true;
            }

            gridView.ShowingEditor -= OnReadOnlyShowingEditor;
            gridView.ShowingEditor += OnReadOnlyShowingEditor;
        }

        /// <summary>
        /// Снимает режим «только чтение», заданный через <see cref="ApplyReadOnly"/>.
        /// </summary>
        public static void ClearReadOnly(this GridView gridView)
        {
            if (gridView == null)
            {
                return;
            }

            gridView.ShowingEditor -= OnReadOnlyShowingEditor;
            gridView.OptionsBehavior.Editable = true;
            gridView.OptionsBehavior.ReadOnly = false;
        }

        private static void OnReadOnlyShowingEditor(object sender, CancelEventArgs e)
        {
            if (sender is not GridView view)
            {
                return;
            }

            var rowHandle = view.FocusedRowHandle;
            if (view.IsDataRow(rowHandle) || view.IsNewItemRow(rowHandle))
            {
                e.Cancel = true;
            }
        }
    }
}
