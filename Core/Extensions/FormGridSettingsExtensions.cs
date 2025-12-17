using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.Class;
using SewingProduction.Core.Services;

namespace SewingProduction.Core.Extensions
{
    /// <summary>
    /// Методы расширения для автоматического управления настройками гридов на формах
    /// </summary>
    public static class FormGridSettingsExtensions
    {
        /// <summary>
        /// Включает автоматическое сохранение настроек для всех CustomGridControl на форме
        /// </summary>
        /// <param name="form">Форма для обработки</param>
        /// <param name="enableAutoSettings">Включить автоматическое сохранение (по умолчанию true)</param>
        public static void EnableAutoGridSettings(this Form form, bool enableAutoSettings = true)
        {
            var customGridControls = GetAllCustomGridControls(form);

            foreach (var gridControl in customGridControls)
            {
                gridControl.EnableAutoSettings = enableAutoSettings;

                if (enableAutoSettings)
                {
                    gridControl.LoadGridSettings();
                }
            }
        }

        /// <summary>
        /// Сохраняет настройки всех CustomGridControl на форме
        /// </summary>
        /// <param name="form">Форма для сохранения настроек</param>
        public static void SaveAllGridSettings(this Form form)
        {
            var customGridControls = GetAllCustomGridControls(form);

            foreach (var gridControl in customGridControls)
            {
                if (gridControl.EnableAutoSettings)
                {
                    gridControl.SaveGridSettings();
                }
            }
        }
        
        /// <summary>
        /// Загружает настройки всех CustomGridControl на форме
        /// </summary>
        /// <param name="form">Форма для загрузки настроек</param>
        public static void LoadAllGridSettings(this Form form)
        {
            var customGridControls = GetAllCustomGridControls(form);

            foreach (var gridControl in customGridControls)
            {
                if (gridControl.EnableAutoSettings)
                {
                    gridControl.LoadGridSettings();
                }
            }
        }

        /// <summary>
        /// Включает автоматическое сохранение для обычных GridControl (не CustomGridControl)
        /// </summary>
        /// <param name="form">Форма для обработки</param>
        /// <param name="enableAutoSettings">Включить автоматическое сохранение</param>
        public static void EnableAutoSettingsForAllGrids(this Form form, bool enableAutoSettings = true)
        {
            var allGridControls = GetAllGridControls(form);

            foreach (var gridControl in allGridControls)
            {
                foreach (var view in gridControl.ViewCollection)
                {
                    if (view is GridView gridView)
                    {
                        if (enableAutoSettings)
                        {
                            GridSettingsManager.Instance.EnableAutoSettings(gridView);
                        }
                        else
                        {
                            GridSettingsManager.Instance.DisableAutoSettings(gridView);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Устанавливает пользовательский ключ настроек для конкретного CustomGridControl
        /// </summary>
        /// <param name="form">Форма</param>
        /// <param name="gridControlName">Имя GridControl</param>
        /// <param name="settingsKey">Пользовательский ключ настроек</param>
        public static void SetGridSettingsKey(this Form form, string gridControlName, string settingsKey)
        {
            var customGridControls = GetAllCustomGridControls(form);
            var targetGrid = customGridControls.FirstOrDefault(g => g.Name == gridControlName);

            if (targetGrid != null)
            {
                targetGrid.SettingsKey = settingsKey;
            }
        }

        /// <summary>
        /// Получает все CustomGridControl на форме (включая вложенные контролы)
        /// </summary>
        /// <param name="form">Форма для поиска</param>
        /// <returns>Список CustomGridControl</returns>
        private static List<CustomGridControl> GetAllCustomGridControls(Form form)
        {
            var result = new List<CustomGridControl>();
            FindCustomGridControlsRecursive(form, result);
            return result;
        }

        /// <summary>
        /// Получает все GridControl на форме (включая вложенные контролы)
        /// </summary>
        /// <param name="form">Форма для поиска</param>
        /// <returns>Список GridControl</returns>
        private static List<GridControl> GetAllGridControls(Form form)
        {
            var result = new List<GridControl>();
            FindGridControlsRecursive(form, result);
            return result;
        }

        /// <summary>
        /// Рекурсивно ищет CustomGridControl в контроле и его дочерних контролах
        /// </summary>
        /// <param name="control">Контрол для поиска</param>
        /// <param name="result">Список найденных CustomGridControl</param>
        private static void FindCustomGridControlsRecursive(Control control, List<CustomGridControl> result)
        {
            if (control is CustomGridControl customGrid)
            {
                result.Add(customGrid);
            }

            foreach (Control child in control.Controls)
            {
                FindCustomGridControlsRecursive(child, result);
            }
        }

        /// <summary>
        /// Рекурсивно ищет GridControl в контроле и его дочерних контролах
        /// </summary>
        /// <param name="control">Контрол для поиска</param>
        /// <param name="result">Список найденных GridControl</param>
        private static void FindGridControlsRecursive(Control control, List<GridControl> result)
        {
            if (control is GridControl gridControl)
            {
                result.Add(gridControl);
            }

            foreach (Control child in control.Controls)
            {
                FindGridControlsRecursive(child, result);
            }
        }
    }
}