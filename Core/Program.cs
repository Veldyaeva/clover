using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraReports.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using SewingProduction.form;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z.Dapper.Plus;

namespace SewingProduction.Core
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
                SetIEFeatureMode();
            DapperMappings.Configure();
            GridLocalizer.Active = new CustomLocalizer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DapperMappings.Configure();
            using (SplashScreen splashScreen = new SplashScreen())
            {
                SplashScreen splash = new SplashScreen();
                splash.Show();
                splash.Update(); // Чтобы экран сразу отобразился
                Application.DoEvents(); // Важно для обновления UI заставки                     
                SpMainForm mainForm = new SpMainForm();// Загружаем основную форму
                Thread.Sleep(2000); // Пример задержки - 2 секунды
                ThemeManager.LoadTheme();//Загружаем тему
                splash.Close(); // Закрываем сплэш
                Application.Run(new SpMainForm());
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
