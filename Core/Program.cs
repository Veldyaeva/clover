using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Localization;
using Microsoft.Extensions.DependencyInjection;
using DevExpress.XtraReports.Design;
using SewingProduction.form;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;

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
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Уникальное имя Mutex
            bool createdNew;
            using (var mutex = new Mutex(true, "SewingProductionAppMutex", out createdNew))
            {
                if (!createdNew)
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

                using (SplashScreen splashScreen = new SplashScreen())
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
            }
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
    }
}
