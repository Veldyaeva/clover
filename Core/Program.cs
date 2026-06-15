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
using SewingProduction.Core.interfaces;
using SewingProduction.Core.services;
using SewingProduction.Features.KnittingProduction.Forms;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service;
using SewingProduction.Core.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Models;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SewingProduction.ServiceBroker;


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
        private const string SqlFirstChanceVerboseEnv = "SP_SQL_TRACE_ALL";

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            RegisterSqlFirstChanceTrace();
            //PrintDialogRunner.Instance = new DefaultPrintDialogRunner();
            //Debug.WriteLine(PrintDialogRunner.Instance.GetType().FullName);
            //PrintDialogRunner.Instance = new DefaultPrintDialogRunner();
            //Debug.WriteLine("After set: " + PrintDialogRunner.Instance.GetType().FullName);

            RegisterGlobalExceptionHandlers();

            //CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("ru-RU");
            //CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru-RU");
            var culture = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

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
                GridLocalizer.Active = new global::RussianGridLocalizer();
                DevExpress.XtraEditors.Controls.Localizer.Active = new SewingProduction.CustomControls.RuEditorsLocalizer();

                Application.EnableVisualStyles();
                SplashImageStorage.EnsureCachedBeforeSplash(_logger);
                SplashImageStorage.StartBackgroundSync(_logger);
                Application.SetCompatibleTextRenderingDefault(false);
                DapperMappings.Configure();

                // DI контейнер
                var services = new ServiceCollection();
                ConfigureServices(services);
                var provider = services.BuildServiceProvider();
                AppServices.Configure(provider);
                ServiceBrokerSettings.Enabled = true;
                var sqlDependencyConnection = SettingsManager.GetCurrentConnectionString();
                var sqlDependencyStarted = false;
                if (ServiceBrokerSettings.Enabled && !string.IsNullOrWhiteSpace(sqlDependencyConnection))
                {
                    try
                    {
                        SqlDependency.Start(sqlDependencyConnection);
                        sqlDependencyStarted = true;
                        Debug.WriteLine("[Program] SqlDependency.Start initialized globally.");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"[Program] SqlDependency.Start failed: {ex}");
                    }
                }
                using (SplashScreen splashScreen = new SplashScreen())
                {
                    try
                    {
                        splashScreen.Show();
                        splashScreen.Update();
                        Application.DoEvents();

                        var splashTimer = Stopwatch.StartNew();
                        const int MinSplashMs = 2000;

                    if (!ValidateSystemDate(out var dateError))
                    {
                        MessageBox.Show(dateError,
                            "SewingProduction — Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        try { splashScreen.Close(); } catch { }
                        return;
                    }

                    SpMainForm mainForm = new SpMainForm();

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

                    // Держим сплэш минимум MinSplashMs. Если загрузка заняла дольше — не ждём.
                    // DoEvents позволяет сплэшу перерисовываться во время ожидания.
                    while (splashTimer.ElapsedMilliseconds < MinSplashMs)
                    {
                        Application.DoEvents();
                        Thread.Sleep(30);
                    }

                        splashScreen.Close();

                        Application.Run(mainForm);
                    }
                    finally
                    {
                        if (sqlDependencyStarted && !string.IsNullOrWhiteSpace(sqlDependencyConnection))
                        {
                            try
                            {
                                SqlDependency.Stop(sqlDependencyConnection);
                                Debug.WriteLine("[Program] SqlDependency.Stop completed.");
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"[Program] SqlDependency.Stop failed: {ex}");
                            }
                        }
                    }
                }

            }
        }
        [Conditional("DEBUG")]
        private static void RegisterSqlFirstChanceTrace()
        {
            var (traceTransient, rawEnvValue, envSource) = ResolveSqlFirstChanceVerbose();
            Debug.WriteLine(
                $"[SQL-FIRST-CHANCE] Trace enabled. IncludeTransient={traceTransient}, " +
                $"{SqlFirstChanceVerboseEnv}='{rawEnvValue ?? "<null>"}', Source={envSource}");

            AppDomain.CurrentDomain.FirstChanceException += (_, e) =>
            {
                if (e?.Exception is not SqlException sqlEx)
                    return;

                // Для SqlDependency Query Notifications SqlClient может бросать и сам
                // перехватывать транзиентные first-chance (-2 timeout при регистрации,
                // 2714 duplicate internal QN procedure). Не засоряем лог ими.
                if (!traceTransient && (sqlEx.Number == -2 || sqlEx.Number == 2714))
                    return;

                var topStack = sqlEx.StackTrace;
                if (!string.IsNullOrWhiteSpace(topStack))
                {
                    var nl = topStack.IndexOf('\n');
                    if (nl > 0)
                        topStack = topStack[..nl].Trim();
                }

                Debug.WriteLine(
                    $"[SQL-FIRST-CHANCE] Number={sqlEx.Number}, State={sqlEx.State}, Class={sqlEx.Class}, " +
                    $"Procedure={sqlEx.Procedure}, Line={sqlEx.LineNumber}, " +
                    $"ClientConnectionId={sqlEx.ClientConnectionId}, ThreadId={Environment.CurrentManagedThreadId}, " +
                    $"Message={sqlEx.Message}");
                if (ServiceBroker.TryGetConnectionContext(sqlEx.ClientConnectionId, out var sbContext))
                {
                    Debug.WriteLine($"[SQL-FIRST-CHANCE] ServiceBrokerContext={sbContext}");
                }
                else if (traceTransient)
                {
                    var snapshot = ServiceBroker.GetActiveConnectionContextsSnapshot();
                    Debug.WriteLine(
                        $"[SQL-FIRST-CHANCE] ServiceBrokerContext=<not found>, ActiveContexts={snapshot}");
                }
                if (!string.IsNullOrWhiteSpace(topStack))
                    Debug.WriteLine($"[SQL-FIRST-CHANCE] TopFrame={topStack}");

                try
                {
                    var allErrors = sqlEx.Errors
                        .Cast<SqlError>()
                        .Select(err => $"#{err.Number}/S{err.State}/C{err.Class}/P:{err.Procedure}/L:{err.LineNumber} -> {err.Message}")
                        .ToArray();

                    if (allErrors.Length > 0)
                        Debug.WriteLine("[SQL-FIRST-CHANCE] Errors=[" + string.Join(" | ", allErrors) + "]");
                }
                catch { }

                try
                {
                    var st = new StackTrace(fNeedFileInfo: false);
                    var appFrame = st.GetFrames()?
                        .Select(f => f.GetMethod())
                        .FirstOrDefault(m =>
                            m?.DeclaringType?.FullName?.StartsWith("SewingProduction.", StringComparison.Ordinal) == true &&
                            !string.Equals(m.DeclaringType?.FullName, typeof(Program).FullName, StringComparison.Ordinal) &&
                            !string.Equals(m.Name, "RegisterSqlFirstChanceTrace", StringComparison.Ordinal));

                    if (appFrame != null)
                    {
                        Debug.WriteLine(
                            $"[SQL-FIRST-CHANCE] AppFrame={appFrame.DeclaringType!.FullName}.{appFrame.Name}");
                    }
                }
                catch { }
            };
        }
        private static (bool Enabled, string? RawValue, string Source) ResolveSqlFirstChanceVerbose()
        {
            string? value = Environment.GetEnvironmentVariable(SqlFirstChanceVerboseEnv);
            string source = "process";

            if (string.IsNullOrWhiteSpace(value))
            {
                value = Environment.GetEnvironmentVariable(SqlFirstChanceVerboseEnv, EnvironmentVariableTarget.User);
                source = "user";
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                value = Environment.GetEnvironmentVariable(SqlFirstChanceVerboseEnv, EnvironmentVariableTarget.Machine);
                source = "machine";
            }

            if (string.IsNullOrWhiteSpace(value))
                return (false, value, source);

            bool enabled =
                string.Equals(value.Trim(), "1", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(value.Trim(), "true", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(value.Trim(), "yes", StringComparison.OrdinalIgnoreCase);

            return (enabled, value, source);
        }
        private static string PickExistingSkinOrDefault(string skinName, string defaultSkin)
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
            services.AddSingleton<DatabaseHelperSQL>();
            services.AddSingleton<IAppServiceBrokerHub, AppServiceBrokerHub>();
            services.AddTransient<ILogger, HybridLogger>();
            services.AddTransient<IKnitterRepository, KnitterRepository>();
            services.AddTransient<IKnitterWorkSpaceUiGateway, KnitterRepository>();
            services.AddTransient<IKnitterShiftGateway, KnitterRepository>();
            services.AddTransient<IKnitterOrchestrator, KnitterOrchestrator>();
            services.AddTransient<KnitterWorkSpace>();
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
