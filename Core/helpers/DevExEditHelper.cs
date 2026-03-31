using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;

namespace SewingProduction.Core.helpers
{
    public static class DevExEditHelper
    {        /// <summary>
             /// Показывает пустую строку вместо 0 для int-полей.
             /// Значение в модели при этом остаётся 0.
             /// </summary>
        public static void ShowEmptyWhenZero(this BaseEdit edit)
        {
            if (edit == null) return;

            edit.CustomDisplayText -= Edit_CustomDisplayText_ZeroAsEmpty;
            edit.CustomDisplayText += Edit_CustomDisplayText_ZeroAsEmpty;
        }

        /// <summary>
        /// Убирает обработчик показа пустой строки вместо 0.
        /// </summary>
        public static void DisableEmptyWhenZero(this BaseEdit edit)
        {
            if (edit == null) return;

            edit.CustomDisplayText -= Edit_CustomDisplayText_ZeroAsEmpty;
        }

        private static void Edit_CustomDisplayText_ZeroAsEmpty(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            if (e.Value == null || e.Value == DBNull.Value)
                return;

            switch (e.Value)
            {
                case int i when i == 0:
                    e.DisplayText = string.Empty;
                    break;

                case long l when l == 0:
                    e.DisplayText = string.Empty;
                    break;

                case short s when s == 0:
                    e.DisplayText = string.Empty;
                    break;

                case byte b when b == 0:
                    e.DisplayText = string.Empty;
                    break;

                case string str when int.TryParse(str, out var parsed) && parsed == 0:
                    e.DisplayText = string.Empty;
                    break;
                case decimal d when d == 0:
                    e.DisplayText = string.Empty;
                    break;
            }
        }
    }
}
