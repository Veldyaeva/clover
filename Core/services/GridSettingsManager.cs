using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.Interfaces;

namespace SewingProduction.Core.Services
{
    /// <summary>
    /// Сервис для автоматического управления настройками гридов
    /// </summary>
    public class GridSettingsManager : IGridSettingsManager
    {
        private readonly Dictionary<GridView, string> _registeredGrids = new Dictionary<GridView, string>();
        private readonly ILogger _logger = new FileLogger();
        private readonly object _lockObject = new object();
        private static GridSettingsManager _instance;
        private static readonly object _instanceLock = new object();

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static GridSettingsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new GridSettingsManager();
                    }
                }
                return _instance;
            }
        }

        private GridSettingsManager() { }

        /// <summary>
        /// Включает автоматическое сохранение настроек для грида
        /// </summary>
        public void EnableAutoSettings(GridView gridView, string settingsKey = null)
        {
            if (gridView == null) return;

            lock (_lockObject)
            {
                // Генерируем ключ настроек
                if (string.IsNullOrEmpty(settingsKey))
                {
                    settingsKey = GenerateSettingsKey(gridView);
                }

                // Регистрируем грид
                if (!_registeredGrids.ContainsKey(gridView))
                {
                    _registeredGrids[gridView] = settingsKey;

                    // Подписываемся на события для автоматического сохранения
                    SubscribeToEvents(gridView);

                    // Загружаем существующие настройки
                    LoadSettings(gridView, settingsKey);

                    _logger.LogEventAsync($"GridSettingsManager: Включено автоматическое сохранение для '{settingsKey}'", "EnableAutoSettings");
                }
            }
        }

        /// <summary>
        /// Отключает автоматическое сохранение настроек для грида
        /// </summary>
        public void DisableAutoSettings(GridView gridView)
        {
            if (gridView == null) return;

            lock (_lockObject)
            {
                if (_registeredGrids.ContainsKey(gridView))
                {
                    var settingsKey = _registeredGrids[gridView];

                    // Сохраняем последние настройки перед отключением
                    SaveSettings(gridView, settingsKey);

                    // Отписываемся от событий
                    UnsubscribeFromEvents(gridView);

                    // Удаляем из регистрации
                    _registeredGrids.Remove(gridView);

                    _logger.LogEventAsync($"GridSettingsManager: Отключено автоматическое сохранение для '{settingsKey}'", "DisableAutoSettings");
                }
            }
        }

        /// <summary>
        /// Принудительно сохраняет настройки грида
        /// </summary>
        public void SaveSettings(GridView gridView, string settingsKey = null)
        {
            if (gridView == null) return;

            try
            {
                if (string.IsNullOrEmpty(settingsKey))
                {
                    lock (_lockObject)
                    {
                        if (!_registeredGrids.TryGetValue(gridView, out settingsKey))
                        {
                            settingsKey = GenerateSettingsKey(gridView);
                        }
                    }
                }

                string fileName = $"{settingsKey}.xml";
                string appPath = Application.StartupPath;
                string settingsPath = Path.Combine(appPath, "Settings", "Grids");

                if (!Directory.Exists(settingsPath))
                    Directory.CreateDirectory(settingsPath);

                string fullPath = Path.Combine(settingsPath, fileName);

                var columnElements = new List<XElement>();

                for (int i = 0; i < gridView.Columns.Count; i++)
                {
                    var col = gridView.Columns[i];
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

                var gridSettings = new XElement("GridSettings",
                    new XAttribute("SettingsKey", settingsKey),
                    new XAttribute("SaveDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    new XElement("Columns", columnElements)
                );

                var doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), gridSettings);
                doc.Save(fullPath);

                _logger.LogEventAsync($"GridSettingsManager: Настройки сохранены для '{settingsKey}'", "SaveSettings");
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, $"GridSettingsManager: Ошибка при сохранении настроек грида '{settingsKey}'");
            }
        }

        /// <summary>
        /// Принудительно загружает настройки грида
        /// </summary>
        public void LoadSettings(GridView gridView, string settingsKey = null)
        {
            if (gridView == null) return;

            try
            {
                if (string.IsNullOrEmpty(settingsKey))
                {
                    lock (_lockObject)
                    {
                        if (!_registeredGrids.TryGetValue(gridView, out settingsKey))
                        {
                            settingsKey = GenerateSettingsKey(gridView);
                        }
                    }
                }

                string fileName = $"{settingsKey}.xml";
                string appPath = Application.StartupPath;
                string settingsPath = Path.Combine(appPath, "Settings", "Grids");
                string fullPath = Path.Combine(settingsPath, fileName);

                if (!File.Exists(fullPath))
                    return;

                var doc = XDocument.Load(fullPath);
                var columns = doc.Root?.Element("Columns")?.Elements("Column");

                if (columns != null)
                {
                    foreach (var columnElement in columns)
                    {
                        var fieldName = columnElement.Attribute("FieldName")?.Value;
                        if (string.IsNullOrEmpty(fieldName)) continue;

                        var column = gridView.Columns[fieldName];
                        if (column != null)
                        {
                            if (int.TryParse(columnElement.Attribute("Width")?.Value, out int width))
                                column.Width = width;

                            if (int.TryParse(columnElement.Attribute("VisibleIndex")?.Value, out int visibleIndex))
                                column.VisibleIndex = visibleIndex;

                            if (bool.TryParse(columnElement.Attribute("Visible")?.Value, out bool visible))
                                column.Visible = visible;

                            if (Enum.TryParse<DevExpress.Data.ColumnSortOrder>(columnElement.Attribute("SortOrder")?.Value, out var sortOrder))
                                column.SortOrder = sortOrder;

                            if (int.TryParse(columnElement.Attribute("SortIndex")?.Value, out int sortIndex))
                                column.SortIndex = sortIndex;
                        }
                    }
                }

                _logger.LogEventAsync($"GridSettingsManager: Настройки загружены для '{settingsKey}'", "LoadSettings");
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, $"GridSettingsManager: Ошибка при загрузке настроек грида '{settingsKey}'");
            }
        }

        /// <summary>
        /// Сохраняет все зарегистрированные настройки гридов
        /// </summary>
        public void SaveAllSettings()
        {
            lock (_lockObject)
            {
                foreach (var kvp in _registeredGrids)
                {
                    SaveSettings(kvp.Key, kvp.Value);
                }
                _logger.LogEventAsync($"GridSettingsManager: Сохранены настройки для {_registeredGrids.Count} гридов", "SaveAllSettings");
            }
        }

        /// <summary>
        /// Загружает все зарегистрированные настройки гридов
        /// </summary>
        public void LoadAllSettings()
        {
            lock (_lockObject)
            {
                foreach (var kvp in _registeredGrids)
                {
                    LoadSettings(kvp.Key, kvp.Value);
                }
                _logger.LogEventAsync($"GridSettingsManager: Загружены настройки для {_registeredGrids.Count} гридов", "LoadAllSettings");
            }
        }

        /// <summary>
        /// Генерирует уникальный ключ настроек для грида
        /// </summary>
        private string GenerateSettingsKey(GridView gridView)
        {
            var formName = gridView.GridControl?.FindForm()?.GetType().Name ?? "UnknownForm";
            var gridName = gridView.Name ?? gridView.GridControl?.Name ?? "UnknownGrid";
            return $"{formName}_{gridName}";
        }

        /// <summary>
        /// Подписывается на события грида для автоматического сохранения
        /// </summary>
        private void SubscribeToEvents(GridView gridView)
        {
            // Сохраняем при изменении ширины колонок
            gridView.ColumnWidthChanged += OnGridSettingsChanged;

            // Сохраняем при изменении видимости колонок
            gridView.ColumnPositionChanged += OnGridSettingsChanged;

            // Сохраняем при изменении сортировки
            //gridView.SortInfoChanged += OnGridSettingsChanged;

            // Сохраняем при закрытии формы
            var form = gridView.GridControl?.FindForm();
            if (form != null)
            {
                form.FormClosing += OnFormClosing;
            }
        }

        /// <summary>
        /// Отписывается от событий грида
        /// </summary>
        private void UnsubscribeFromEvents(GridView gridView)
        {
            gridView.ColumnWidthChanged -= OnGridSettingsChanged;
            gridView.ColumnPositionChanged -= OnGridSettingsChanged;
            //gridView.sort -= OnGridSettingsChanged;

            var form = gridView.GridControl?.FindForm();
            if (form != null)
            {
                form.FormClosing -= OnFormClosing;
            }
        }

        /// <summary>
        /// Обработчик изменения настроек грида
        /// </summary>
        private void OnGridSettingsChanged(object sender, EventArgs e)
        {
            if (sender is GridView gridView)
            {
                lock (_lockObject)
                {
                    if (_registeredGrids.TryGetValue(gridView, out string settingsKey))
                    {
                        // Сохраняем с небольшой задержкой, чтобы избежать частых сохранений
                        System.Threading.Timer timer = null;
                        timer = new System.Threading.Timer(_ =>
                        {
                            SaveSettings(gridView, settingsKey);
                            timer?.Dispose();
                        }, null, 500, System.Threading.Timeout.Infinite);
                    }
                }
            }
        }

        /// <summary>
        /// Обработчик закрытия формы
        /// </summary>
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (sender is Form form)
            {
                // Сохраняем настройки всех гридов на форме
                var gridsToSave = new List<KeyValuePair<GridView, string>>();

                lock (_lockObject)
                {
                    foreach (var kvp in _registeredGrids)
                    {
                        if (kvp.Key.GridControl?.FindForm() == form)
                        {
                            gridsToSave.Add(kvp);
                        }
                    }
                }

                foreach (var kvp in gridsToSave)
                {
                    SaveSettings(kvp.Key, kvp.Value);
                }
            }
        }
    }
}