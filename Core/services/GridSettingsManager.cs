using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json;
using SewingProduction.Core.Class.Settings;
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
            if (!SettingsManager.GetSaveGridSettings())
                return;

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

                var formName = GetFormName(gridView);
                var dir = Path.Combine(UserFilePaths.GridSettings, formName);
                Directory.CreateDirectory(dir);

                var columnSettings = new List<GridColumnSetting>();

                // Сохраняем все столбцы (включая скрытые)
                // Используем порядок по VisibleIndex для более предсказуемого сохранения
                var columnsToSave = gridView.Columns
                    .Cast<DevExpress.XtraGrid.Columns.GridColumn>()
                    .Where(col => !string.IsNullOrEmpty(col.FieldName))
                    .OrderBy(col => col.VisibleIndex >= 0 ? col.VisibleIndex : int.MaxValue)
                    .ThenBy(col => col.FieldName)
                    .ToList();

                foreach (var col in columnsToSave)
                {
                    columnSettings.Add(new GridColumnSetting
                    {
                        FieldName = col.FieldName,
                        Width = col.Width,
                        VisibleIndex = col.VisibleIndex,
                        Visible = col.Visible,
                        SortOrder = col.SortOrder.ToString(),
                        SortIndex = col.SortIndex
                    });
                }

                // Сохраняем в отдельный JSON файл
                string fileName = $"{settingsKey}.json";
                string filePath = Path.Combine(dir, fileName);

                var json = JsonConvert.SerializeObject(columnSettings, Formatting.Indented);
                File.WriteAllText(filePath, json);

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
            if (!SettingsManager.GetSaveGridSettings())
                return;

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

                var formName = GetFormName(gridView);
                var dir = Path.Combine(UserFilePaths.GridSettings, formName);
                Directory.CreateDirectory(dir);

                // Загружаем из отдельного JSON файла
                string fileName = $"{settingsKey}.json";
                string filePath = Path.Combine(dir, fileName);

                if (!File.Exists(filePath))
                    return;

                var json = File.ReadAllText(filePath);
                var columnSettings = JsonConvert.DeserializeObject<List<GridColumnSetting>>(json);
                
                if (columnSettings == null || columnSettings.Count == 0)
                    return;

                // Сначала устанавливаем видимость и другие свойства (кроме VisibleIndex)
                foreach (var setting in columnSettings)
                {
                    if (string.IsNullOrEmpty(setting.FieldName))
                        continue;

                    var column = gridView.Columns[setting.FieldName];
                    if (column != null)
                    {
                        column.Width = setting.Width;
                        column.Visible = setting.Visible;

                        if (Enum.TryParse<ColumnSortOrder>(setting.SortOrder, out var sortOrder))
                            column.SortOrder = sortOrder;

                        column.SortIndex = setting.SortIndex;
                    }
                }

                // Затем устанавливаем VisibleIndex в правильном порядке (от меньшего к большему)
                // Это предотвращает конфликты при установке позиций столбцов
                // Обрабатываем только видимые столбцы (VisibleIndex >= 0)
                var sortedSettings = columnSettings
                    .Where(s => !string.IsNullOrEmpty(s.FieldName) && s.Visible && s.VisibleIndex >= 0)
                    .OrderBy(s => s.VisibleIndex)
                    .ToList();

                foreach (var setting in sortedSettings)
                {
                    var column = gridView.Columns[setting.FieldName];
                    if (column != null)
                    {
                        column.VisibleIndex = setting.VisibleIndex;
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
        /// Получает имя формы для грида (согласованно используется везде)
        /// </summary>
        private string GetFormName(GridView gridView)
        {
            var form = gridView.GridControl?.FindForm() ?? gridView.GridControl?.TopLevelControl as Form;
            return form?.GetType().Name ?? "UnknownForm";
        }

        /// <summary>
        /// Генерирует уникальный ключ настроек для грида
        /// Использует ту же логику определения имени формы, что и GetFormName
        /// </summary>
        private string GenerateSettingsKey(GridView gridView)
        {
            var formName = GetFormName(gridView);
            var gridName = gridView.Name ?? gridView.GridControl?.Name ?? "UnknownGrid";

            // Всегда используем имя формы в ключе для согласованности
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