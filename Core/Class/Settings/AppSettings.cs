using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace SewingProduction.Core.Class.Settings
{
    public class AppSettings
    {
        public string Theme { get; set; } = "Ocean";
        public Dictionary<string, UserSettings> Users { get; set; } = new();
    }

    public class UserSettings
    {
        public List<string> OpenTabs { get; set; } = new();
        public List<string> Logins { get; set; } = new();
    }

    public static class SettingsManager
    {
        private static readonly string SettingsPath = "settings.json";
        private static AppSettings _settings;

        public static AppSettings Current => _settings ??= Load();

        public static void Save()
        {
            var json = JsonConvert.SerializeObject(_settings, Formatting.Indented);
            File.WriteAllText(SettingsPath, json);
        }

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
    }
}
