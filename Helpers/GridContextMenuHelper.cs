using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Forms;

namespace SewingProduction.Helpers
{
    public static class GridContextMenuHelper
    {
        public static void AddCopyCellMenuItem(object sender, PopupMenuShowingEventArgs e, string caption = "Копировать")
        {
            if (e.MenuType != GridMenuType.Row || e.HitInfo?.Column == null)
            {
                return;
            }

            if (sender is not GridView view)
            {
                return;
            }

            var copyCellItem = new DXMenuItem(caption, (_, __) =>
            {
                int rowHandle = e.HitInfo.RowHandle;
                var col = e.HitInfo.Column;
                if (rowHandle < 0 || col == null)
                {
                    return;
                }

                var cellText = view.GetRowCellDisplayText(rowHandle, col);
                if (!string.IsNullOrEmpty(cellText))
                {
                    Clipboard.SetText(cellText);
                }
            });

            e.Menu.Items.Add(copyCellItem);
        }
    }
}
