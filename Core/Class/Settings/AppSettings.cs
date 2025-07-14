using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;

namespace SewingProduction.Core.Class.Settings
{
    public class AppSettings
    {
        public string Theme { get; set; } = "Gray";
        public int FontSize { get; set; } = 10;
        public bool SaveOpenTabs { get; set; } = true;
        public bool ShortTabNames { get; set; } = true;
        public bool AllowDuplicateTabs { get; set; } = false;
        public Dictionary<string, UserSettings> Users { get; set; } = new();
    }

    public class UserSettings
    {
        public List<string> OpenTabs { get; set; } = new();
        public List<string> Logins { get; set; } = new();
        public string SavedPassword { get; set; } = "";
        public string SelectedDatabase { get; set; } = "ACEConnectionString";
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
        #region база данных
        public static void SaveSelectedDatabase(string login, string connectionName)
        {
            if (!Current.Users.ContainsKey(login))
                Current.Users[login] = new UserSettings();

            Current.Users[login].SelectedDatabase = connectionName;
            Save();
        }

        public static string GetSelectedDatabase(string login)
        {
            if (Current.Users.TryGetValue(login, out var settings))
                return string.IsNullOrEmpty(settings.SelectedDatabase) ? "ACEConnectionString" : settings.SelectedDatabase;

            return "ACEConnectionString";
        }
        public static string GetCurrentDatabase()
        {
            var login = GetLoginHistory().LastOrDefault();

            if (!string.IsNullOrEmpty(login))
            {
                var db = GetSelectedDatabase(login);
                if (!string.IsNullOrWhiteSpace(db))
                    return db;
            }

            return "ace"; // дефолт, если ничего не выбрано
        }
        #endregion
    }
    public static class UserFilePaths
    {
        public static string BaseFolder => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "SewingProduction");

        public static string Settings => Path.Combine(BaseFolder, "settings.json");
        public static string LogFile => Path.Combine(BaseFolder, "log.txt");

        public static void EnsureFolderExists()
        {
            if (!Directory.Exists(BaseFolder))
                Directory.CreateDirectory(BaseFolder);
        }
    }
}
