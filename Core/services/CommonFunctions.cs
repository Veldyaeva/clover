using System;
using DevExpress.XtraGrid.Views.Grid;

namespace SewingProduction.Helpers
{
    public static class CommonFunctions
    {
        private static readonly ILogger _logger = new FileLogger();

        public static T GetRowCellValueOrDefault<T>(GridView view, int rowHandle, string fieldName, T defaultValue)
        {
            try
            {
                object value = view.GetRowCellValue(rowHandle, fieldName);
                return value == null || value == DBNull.Value ? defaultValue : (T)value;
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, $"Ошибка получения значения из GridView: {fieldName}, строка {rowHandle}");
                return defaultValue;
            }
        }
    }
}
