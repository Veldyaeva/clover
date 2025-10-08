using DevExpress.XtraGrid.Views.Grid;

namespace SewingProduction.Core.Interfaces
{
    /// <summary>
    /// Интерфейс для автоматического управления настройками гридов
    /// </summary>
    public interface IGridSettingsManager
    {
        /// <summary>
        /// Включает автоматическое сохранение настроек для грида
        /// </summary>
        /// <param name="gridView">GridView для управления настройками</param>
        /// <param name="settingsKey">Уникальный ключ для настроек (если не указан, используется Name грида)</param>
        void EnableAutoSettings(GridView gridView, string settingsKey = null);

        /// <summary>
        /// Отключает автоматическое сохранение настроек для грида
        /// </summary>
        /// <param name="gridView">GridView для отключения управления настройками</param>
        void DisableAutoSettings(GridView gridView);

        /// <summary>
        /// Принудительно сохраняет настройки грида
        /// </summary>
        /// <param name="gridView">GridView для сохранения настроек</param>
        /// <param name="settingsKey">Ключ настроек</param>
        void SaveSettings(GridView gridView, string settingsKey = null);

        /// <summary>
        /// Принудительно загружает настройки грида
        /// </summary>
        /// <param name="gridView">GridView для загрузки настроек</param>
        /// <param name="settingsKey">Ключ настроек</param>
        void LoadSettings(GridView gridView, string settingsKey = null);

        /// <summary>
        /// Сохраняет все зарегистрированные настройки гридов
        /// </summary>
        void SaveAllSettings();

        /// <summary>
        /// Загружает все зарегистрированные настройки гридов
        /// </summary>
        void LoadAllSettings();
    }
}