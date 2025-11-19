using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraReports.Design;
using Microsoft.Win32;
using SewingProduction.Models;
using Microsoft.Extensions.DependencyInjection;
using SewingProduction.Core;
using SewingProduction.Features.KnittingProduction.Forms;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service;
using SewingProduction.Helpers;


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
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            RegisterGlobalExceptionHandlers();
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

                using (SplashScreen splashScreen = new SplashScreen())
                {
                    try
                    {
                        splashScreen.Show();
                        splashScreen.Update();
                        Application.DoEvents();

                        SpMainForm mainForm = new SpMainForm();
                        Thread.Sleep(2000);
                        ThemeManager.LoadTheme();
                        splashScreen.Close();

                        Application.Run(mainForm);
                    }
                    catch (Exception ex)
                    {
                        try { _ = _logger.LogErrorAsync(ex, "Fatal in Application.Run"); } catch { }
                    }
                }
            }
        }
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
    }
}
