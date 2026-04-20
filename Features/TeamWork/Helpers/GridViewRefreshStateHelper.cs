using DevExpress.XtraGrid.Views.Grid;
using System;

namespace SewingProduction.Features.TeamWork.Helpers
{
    internal sealed class GridViewRefreshState
    {
        public string ActiveFilterString { get; set; }
        public string FindFilterText { get; set; }
        public int TopRowIndex { get; set; }
        public string FocusFieldName { get; set; }
        public object FocusFieldValue { get; set; }
    }

    internal static class GridViewRefreshStateHelper
    {
        public static GridViewRefreshState Capture(GridView gridView, string focusFieldName = null)
        {
            if (gridView == null)
            {
                return null;
            }

            object focusFieldValue = null;
            if (!string.IsNullOrWhiteSpace(focusFieldName) && gridView.FocusedRowHandle >= 0)
            {
                try
                {
                    focusFieldValue = gridView.GetRowCellValue(gridView.FocusedRowHandle, focusFieldName);
                }
                catch
                {
                    focusFieldValue = null;
                }
            }

            return new GridViewRefreshState
            {
                ActiveFilterString = gridView.ActiveFilterString,
                FindFilterText = gridView.FindFilterText,
                TopRowIndex = gridView.TopRowIndex,
                FocusFieldName = focusFieldName,
                FocusFieldValue = focusFieldValue
            };
        }

        public static void Restore(GridView gridView, GridViewRefreshState state)
        {
            if (gridView == null || state == null)
            {
                return;
            }

            gridView.BeginUpdate();
            try
            {
                gridView.ActiveFilterString = state.ActiveFilterString ?? string.Empty;
                gridView.FindFilterText = state.FindFilterText ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(state.FocusFieldName) && state.FocusFieldValue != null)
                {
                    int rowHandle = gridView.LocateByValue(state.FocusFieldName, state.FocusFieldValue);
                    if (gridView.IsValidRowHandle(rowHandle))
                    {
                        gridView.FocusedRowHandle = rowHandle;
                        gridView.MakeRowVisible(rowHandle);
                    }
                }

                if (state.TopRowIndex >= 0)
                {
                    gridView.TopRowIndex = state.TopRowIndex;
                }
            }
            finally
            {
                gridView.EndUpdate();
            }
        }

        public static int GetFocusedIntValue(GridView gridView, string fieldName)
        {
            if (gridView == null || gridView.FocusedRowHandle < 0 || string.IsNullOrWhiteSpace(fieldName))
            {
                return 0;
            }

            try
            {
                object value = gridView.GetRowCellValue(gridView.FocusedRowHandle, fieldName);
                return value == null ? 0 : Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }
    }
}
