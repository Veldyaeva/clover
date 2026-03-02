using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraPrinting.Localization;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraReports.Design;
using DevExpress.XtraReports.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using SewingProduction.Core;
using SewingProduction.Core.Class.Settings;
using SewingProduction.Features.KnittingProduction.Forms;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service;
using SewingProduction.Helpers;
using SewingProduction.Models;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SewingProduction.Core
{
    internal static class Program
    {
        // WinAPI — функции для управления окнами
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private const int SW_RESTORE = 9;

        private static ILogger _logger = new HybridLogger();

        private const string DefaultSkin = "Office 2019 Colorful";

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //PrintDialogRunner.Instance = new DefaultPrintDialogRunner();
            //Debug.WriteLine(PrintDialogRunner.Instance.GetType().FullName);
            //PrintDialogRunner.Instance = new DefaultPrintDialogRunner();
            //Debug.WriteLine("After set: " + PrintDialogRunner.Instance.GetType().FullName);

            RegisterGlobalExceptionHandlers();

            //CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("ru-RU");
            //CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru-RU");

            // Уникальное имя Mutex

            bool createdNew;
            bool isRestarting = args.Contains("--restart");
            using (var mutex = new Mutex(true, "SewingProductionAppMutex", out createdNew))
            {
                if (!createdNew && !isRestarting)
                {
                    // Ищем главное окно по заголовку (он должен быть уникальным!)
                    IntPtr hWnd = FindWindow(null, "Швейное производство"); // название главной формы
                    if (hWnd != IntPtr.Zero)
                    {
                        ShowWindow(hWnd, SW_RESTORE); // восстанавливаем, если свернуто
                        SetForegroundWindow(hWnd);    // переводим в активное
                    }
                    return;
                }

                SetIEFeatureMode();
                GridLocalizer.Active = new CustomLocalizer();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                DapperMappings.Configure();

                // DI контейнер
                var services = new ServiceCollection();
                ConfigureServices(services);
                var provider = services.BuildServiceProvider();
                AppServices.Configure(provider);
                //SqlDependency.Start(SettingsManager.GetCurrentConnectionString());
                //Application.ApplicationExit += (_, __) =>
                //    SqlDependency.Stop(SettingsManager.GetCurrentConnectionString());
                var qnConn = SettingsManager.GetCurrentConnectionString();
                SqlDependency.Start(qnConn);
                
                void StopQN()
                {
                    try { SqlDependency.Stop(qnConn); } catch { }
                }
                
                Application.ApplicationExit += (_, __) => StopQN();
                AppDomain.CurrentDomain.ProcessExit += (_, __) => StopQN();
                AppDomain.CurrentDomain.DomainUnload += (_, __) => StopQN();

                EnsureSeasonImagesInRoaming();

                using (SplashScreen splashScreen = new SplashScreen())
                {
                    splashScreen.Show();
                    splashScreen.Update();
                    Application.DoEvents();

                    if (!ValidateSystemDate(out var dateError))
                    {
                        //MessageBox.Show(dateError,
                        //    "SewingProduction — Ошибка",
                        //    MessageBoxButtons.OK,
                        //    MessageBoxIcon.Error);
                        try { splashScreen.Close(); } catch { }
                        return;
                    }

                    SpMainForm mainForm = new SpMainForm();
                    Thread.Sleep(2000);

                    //ThemeManager.LoadTheme();

                    BonusSkins.Register();
                    SkinManager.EnableFormSkins();

                    // 1) Миграция user-настроек после обновления (один раз)
                    if (!Properties.Settings.Default.SettingsUpgraded)
                    {
                        Properties.Settings.Default.Upgrade();
                        Properties.Settings.Default.SettingsUpgraded = true;
                        Properties.Settings.Default.Save();
                    }

                    // 2) Применяем тему безопасно (если темы нет — ставим дефолт)
                    var saved = Properties.Settings.Default.AppSkin;
                    var skinToApply = PickExistingSkinOrDefault(saved, DefaultSkin);

                    UserLookAndFeel.Default.SkinName = skinToApply;

                    // если сохранённая была битая/несуществующая — поправим и сохраним
                    if (skinToApply != saved)
                    {
                        Properties.Settings.Default.AppSkin = skinToApply;
                        Properties.Settings.Default.Save();
                    }

                    ThemeManager.LoadTheme();

                    //XtraReportsLocalizer.Active = new DxReportsLocalizerRu(traceUnknown);
                    //PrintingSystemLocalizer.Active = new DxPrintingLocalizerRu(traceUnknown);
                    PreviewLocalizer.Active = new DxPreviewLocalizerRu();
                    
                    splashScreen.Close();

                    Application.Run(mainForm);
                }

            }
        }
        private static string PickExistingSkinOrDefault(string? skinName, string defaultSkin)
        {
            if (!string.IsNullOrWhiteSpace(skinName) && SkinExists(skinName))
                return skinName;

            // дефолт тоже проверим (на всякий случай)
            if (SkinExists(defaultSkin))
                return defaultSkin;

            // крайний случай: берём первую доступную
            return SkinManager.Default.Skins.Count > 0
                ? SkinManager.Default.Skins[0].SkinName
                : defaultSkin;
        }

        private static bool SkinExists(string skinName)
            => SkinManager.Default.Skins.Cast<SkinContainer>()
                .Any(s => string.Equals(s.SkinName, skinName, StringComparison.OrdinalIgnoreCase));
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<DatabaseHelper>();
            services.AddTransient<ILogger, HybridLogger>();
            services.AddTransient<IKnitterRepository, KnitterRepository>();
            services.AddTransient<IKnitterOrchestrator, KnitterOrchestrator>();
            services.AddTransient<KnitterWorkSpace>();
        }
        public class CustomLocalizer : GridLocalizer
        {
            public override string GetLocalizedString(GridStringId id)
            {
                switch (id)
                {
                    case GridStringId.EditFormUpdateButton:
                        return "Сохранить";
                    case GridStringId.EditFormCancelButton:
                        return "Отмена";
                    case GridStringId.FindControlFindButton:
                        return "Найти";
                    case GridStringId.CustomFilterDialogCancelButton:
                        return "Отмена";
                    case GridStringId.CustomFilterDialogCaption:
                        return "Настройка фильтра";
                    case GridStringId.FilterPanelCustomizeButton: return "Настроить";
                    default:
                        return base.GetLocalizedString(id);
                }
            }
        }
        private static void SetIEFeatureMode()
        {
            try
            {
                string appName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION");
                if (key.GetValue(appName) == null)
                {
                    // 11001 = IE11, 10001 = IE10, 9999 = IE9, 8000 = IE8, 7000 = IE7
                    key.SetValue(appName, 11001, RegistryValueKind.DWord);
                }
            }
            catch { /* ignore */ }
        }

        private static void RegisterGlobalExceptionHandlers()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.ThreadException += (s, e) =>
            {
                try { _ = _logger.LogErrorAsync(e.Exception, "UI ThreadException"); } catch { }
                MessageBox.Show($"Ошибка UI:\r\n{e.Exception.Message}",
                    "SewingProduction — Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var ex = e.ExceptionObject as Exception ?? new Exception("Unknown AppDomain exception");
                try { _ = _logger.LogErrorAsync(ex, "AppDomain UnhandledException"); } catch { }
                MessageBox.Show($"Критическая ошибка:\r\n{ex.Message}",
                    "SewingProduction — Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                try { _ = _logger.LogErrorAsync(e.Exception, "TaskScheduler UnobservedTaskException"); } catch { }
                e.SetObserved();
            };
        }

        private static void EnsureSeasonImagesInRoaming()
        {
            try
            {
                string sourceRoot = Path.Combine(AppContext.BaseDirectory, "SplashImages");
                if (!Directory.Exists(sourceRoot))
                {
                    return;
                }

                string SplashImages = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "SewingProduction",
                    "SplashImages");
                Directory.CreateDirectory(SplashImages);

                foreach (string sourceFile in Directory.GetFiles(sourceRoot, "*", SearchOption.AllDirectories))
                {
                    string relativePath = Path.GetRelativePath(sourceRoot, sourceFile);
                    string destinationFile = Path.Combine(SplashImages, relativePath);
                    string destinationDir = Path.GetDirectoryName(destinationFile);
                    if (!string.IsNullOrEmpty(destinationDir))
                    {
                        Directory.CreateDirectory(destinationDir);
                    }

                    if (NeedToCopyFile(sourceFile, destinationFile))
                    {
                        File.Copy(sourceFile, destinationFile, true);
                    }
                }
            }
            catch (Exception ex)
            {
                try { _ = _logger.LogErrorAsync(ex, "Failed to seed splash images to Roaming settings"); } catch { }
            }
        }

        private static bool NeedToCopyFile(string sourceFile, string destinationFile)
        {
            if (!File.Exists(destinationFile))
            {
                return true;
            }

            var sourceInfo = new FileInfo(sourceFile);
            var destinationInfo = new FileInfo(destinationFile);

            if (sourceInfo.Length != destinationInfo.Length)
            {
                return true;
            }

            return sourceInfo.LastWriteTimeUtc > destinationInfo.LastWriteTimeUtc;
        }

        private static bool ValidateSystemDate(out string errorMessage)
        {
            errorMessage = null;
            try
            {
                DateTime systemDateTime = DateTime.Now;
                DateTime dbDateTime;
                using (var connection = new SqlConnection(SettingsManager.GetCurrentConnectionString()))
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT GETDATE()", connection))
                    {
                        var result = command.ExecuteScalar();
                        if (result == null || result == DBNull.Value)
                        {
                            throw new InvalidOperationException("Сервер БД вернул пустую дату.");
                        }
                        dbDateTime = Convert.ToDateTime(result);
                    }
                }

                var difference = (systemDateTime - dbDateTime).Duration();
                if (difference > TimeSpan.FromMinutes(10))//даём 10 минут на расхождение, т.к. может быть небольшая разница из-за синхронизации времени
                {
                    errorMessage =
                        $"Дата/время компьютера ({systemDateTime:dd.MM.yyyy HH:mm:ss}) не совпадает с сервером " +
                        $"({dbDateTime:dd.MM.yyyy HH:mm:ss}). " +
                        "Разница более 10 минут. Обратитесь к администратору и перезапустите программу.";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                try { _ = _logger.LogErrorAsync(ex, "Date check failed"); } catch { }
                errorMessage = $"Не удалось проверить дату на сервере БД: {ex.Message}";
                return false;
            }
        }
    }
}
