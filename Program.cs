using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraReports.Design;
using SewingProduction.form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            GridLocalizer.Active = new CustomLocalizer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
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
                    default:
                        return base.GetLocalizedString(id);
                }
            }
        }
    }
}
