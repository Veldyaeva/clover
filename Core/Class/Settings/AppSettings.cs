using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using SewingProduction;

namespace SewingProduction.Core.Class.Settings
{
    public class AppSettings
    {
        public string Theme { get; set; } = "Gray";
        public int FontSize { get; set; } = 10;
        public bool SaveOpenTabs { get; set; } = true;
        public bool ShortTabNames { get; set; } = true;
        public bool AllowDuplicateTabs { get; set; } = false;
        public bool SaveGridSettings { get; set; } = true;
        public Dictionary<string, UserSettings> Users { get; set; } = new();
        public string SelectedDatabase { get; set; } = "ace";
        public Dictionary<string, ThemeManager.Theme> Themes { get; set; } = new();
        public ServiceBrokerSettings ServiceBroker { get; set; } = new ServiceBrokerSettings();
    }

    public class ServiceBrokerSettings
    {
        /// <summary>
        /// Задержка перед выполнением после последнего события (мс).
        /// </summary>
        public int DebounceMs { get; set; } = 500;

        /// <summary>
        /// Минимальный интервал между обновлениями одного объекта (мс). 0 = отключен.
        /// </summary>
        public int ThrottleMs { get; set; } = 10;//1000;

        /// <summary>
        /// Максимальное время ожидания накопления изменений (мс).
        /// </summary>
        public int MaxWaitMs { get; set; } = 3000;

        /// <summary>
        /// Максимальное количество объектов в одном батче.
        /// </summary>
        public int MaxBatchSize { get; set; } = 50;

        /// <summary>
        /// Максимальное количество параллельных обновлений.
        /// </summary>
        public int MaxParallelReloads { get; set; } = 2;

        /// <summary>
        /// Максимальная глубина каскадных обновлений для защиты от циклов.
        /// </summary>
        public int MaxCascadeDepth { get; set; } = 3;
    }

    public class UserSettings
    {
        public List<string> OpenTabs { get; set; } = new();
        public List<string> Logins { get; set; } = new();
        public string SavedPassword { get; set; } = "";
    }

    public class GridColumnSetting
    {
        public string FieldName { get; set; }
        public int Width { get; set; }
        public int VisibleIndex { get; set; }
        public bool Visible { get; set; }
        public string SortOrder { get; set; }
        public int SortIndex { get; set; }
    }

    public static class SettingsManager
    {
        private static readonly string SettingsPath = UserFilePaths.Settings;
        private static AppSettings _settings;

        public static AppSettings Current => _settings ??= Load();

        public static void Save()
        {
            var json = JsonConvert.SerializeObject(_settings, Formatting.Indented);
            File.WriteAllText(SettingsPath, json);
        }
        #region Тема
        public static void LoadTheme()
        {
            if (!string.IsNullOrEmpty(Current.Theme))
                ThemeManager.SetTheme(Current.Theme);
        }

        public static void SetTheme(string theme)
        {
            Current.Theme = theme;
            Save();
        }

        #endregion
        #region Логин
        public static List<string> GetLoginHistory()
        {
            return Current.Users.TryGetValue("logins", out var user) ? user.Logins : new List<string>();
        }

        public static void AddLogin(string login)
        {
            if (!Current.Users.ContainsKey("logins"))
                Current.Users["logins"] = new UserSettings();

            var logins = Current.Users["logins"].Logins;
            logins.RemoveAll(l => l.Equals(login, StringComparison.OrdinalIgnoreCase));
            logins.Add(login);

            Save();
        }
        #endregion
        #region Пароль
        public static void SavePassword(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login)) return;

            if (!Current.Users.ContainsKey(login))
                Current.Users[login] = new UserSettings();

            Current.Users[login].SavedPassword = password;
            Save();
        }
        public static string GetSavedPassword(string login)
        {
            if (string.IsNullOrWhiteSpace(login)) return "";

            return Current.Users.TryGetValue(login, out var settings)
                ? settings.SavedPassword ?? ""
                : "";
        }
        public static void ClearSavedPassword(string login)
        {
            if (string.IsNullOrWhiteSpace(login)) return;

            if (Current.Users.ContainsKey(login))
            {
                Current.Users[login].SavedPassword = "";
                Save();
            }
        }
        #endregion
        #region Вкладки
        public static List<string> GetOpenTabs(string username)
        {
            return Current.Users.TryGetValue(username, out var user) ? user.OpenTabs : new List<string>();
        }

        public static void SetOpenTabs(string username, List<string> tabs)
        {
            if (string.IsNullOrWhiteSpace(username))
                return;

            if (!Current.Users.ContainsKey(username))
                Current.Users[username] = new UserSettings();

            Current.Users[username].OpenTabs = tabs;
            Save();
        }
        public static bool GetSaveOpenTabs()
        {
            return Current.SaveOpenTabs;
        }

        public static void SetSaveOpenTabs(bool value)
        {
            Current.SaveOpenTabs = value;
            Save();
        }
        #endregion

        private static AppSettings Load()
        {
            if (File.Exists(SettingsPath))
            {
                try
                {
                    var json = File.ReadAllText(SettingsPath);
                    return JsonConvert.DeserializeObject<AppSettings>(json) ?? new AppSettings();
                }
                catch
                {
                    return new AppSettings();
                }
            }
            return new AppSettings();
        }
        public static void SetFontSize(int size)
        {
            Current.FontSize = size;
            ThemeManager.UpdateDefaultFont(new Font("Arial", size));
            Save();
        }
        public static void ClearLoginAndPasswordHistory()
        {
            if (Current.Users.ContainsKey("logins"))
            {
                Current.Users.Remove("logins");
            }

            foreach (var user in Current.Users.Values)
            {
                user.SavedPassword = "";
            }

            Save();
        }
        public static bool GetShortTabNames()
        {
            return Current.ShortTabNames;
        }

        public static void SetShortTabNames(bool value)
        {
            Current.ShortTabNames = value;
            Save();
        }
        public static bool GetAllowDuplicateTabs()
        {
            return Current.AllowDuplicateTabs;
        }

        public static void SetAllowDuplicateTabs(bool value)
        {
            Current.AllowDuplicateTabs = value;
            Save();
        }
        public static bool GetSaveGridSettings()
        {
            return Current.SaveGridSettings;
        }

        public static void SetSaveGridSettings(bool value)
        {
            Current.SaveGridSettings = value;
            Save();
        }
        #region Темы
        public static Dictionary<string, ThemeManager.Theme> GetThemes()
        {
            if (Current.Themes == null || Current.Themes.Count == 0)
            {
                Current.Themes = ThemeManager.GetDefaultThemes();
                Save();
            }
            return Current.Themes;
        }

        public static void SetThemes(Dictionary<string, ThemeManager.Theme> themes)
        {
            Current.Themes = themes ?? new Dictionary<string, ThemeManager.Theme>();
            Save();
        }
        #endregion
        #region база данных
        public static void SaveSelectedDatabase(string login, string connectionName)
        {
            if (!Current.Users.ContainsKey(login))
                Current.Users[login] = new UserSettings();

            Current.SelectedDatabase = connectionName;
            Save();
        }

        public static string GetSelectedDatabase()
        {
            return Current.SelectedDatabase ?? "ace";
        }
        public static string GetCurrentConnectionString()
        {
            var dbKey = GetSelectedDatabase();

            switch (dbKey.ToLower())
            {
                case "ace":
                case "aceconnectionstring":
                    return Properties.Settings.Default.ACEConnectionString;

                case "ace_test":
                case "acetestconnectionstring":
                    return Properties.Settings.Default.ACEtestConnectionString;

                case "ace_backup":
                case "acebackupconnectionstring":
                    return Properties.Settings.Default.ACEbackupConnectionString;

                case "ace_backup_new":
                case "acebackupnewconnectionstring":
                    return Properties.Settings.Default.ACEbackupnewConnectionString;

                case "oms":
                case "omsconnectionstring":
                    return Properties.Settings.Default.OMSConnectionString;

                case "global":
                case "globalconnectionstring":
                    return Properties.Settings.Default.GlobalConnectionString;

                default:
                    throw new Exception($"Неизвестное имя строки подключения: '{dbKey}'");
            }
        }
        #endregion
        #region ServiceBroker
        public static ServiceBrokerSettings GetServiceBrokerSettings()
        {
            return Current.ServiceBroker ?? new ServiceBrokerSettings();
        }

        public static void SetServiceBrokerSettings(ServiceBrokerSettings settings)
        {
            Current.ServiceBroker = settings ?? new ServiceBrokerSettings();
            Save();
        }

        public static int GetServiceBrokerDebounceMs()
        {
            return GetServiceBrokerSettings().DebounceMs;
        }

        public static int GetServiceBrokerThrottleMs()
        {
            return GetServiceBrokerSettings().ThrottleMs;
        }

        public static int GetServiceBrokerMaxWaitMs()
        {
            return GetServiceBrokerSettings().MaxWaitMs;
        }

        public static int GetServiceBrokerMaxBatchSize()
        {
            return GetServiceBrokerSettings().MaxBatchSize;
        }

        public static int GetServiceBrokerMaxParallelReloads()
        {
            return GetServiceBrokerSettings().MaxParallelReloads;
        }

        public static int GetServiceBrokerMaxCascadeDepth()
        {
            return GetServiceBrokerSettings().MaxCascadeDepth;
        }
        #endregion
    }
    public static class UserFilePaths
    {
        public static string BaseFolder => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "SewingProduction");
        public static string GridSettings => Path.Combine(BaseFolder, "GridSettings");
        public static string Settings => Path.Combine(BaseFolder, "settings.json");
        public static string LogFile => Path.Combine(BaseFolder, "log.txt");

        public static void EnsureFolderExists()
        {
            if (!Directory.Exists(BaseFolder))
                Directory.CreateDirectory(BaseFolder);
            if (!Directory.Exists(GridSettings))
                Directory.CreateDirectory(GridSettings);
        }
    }
}
