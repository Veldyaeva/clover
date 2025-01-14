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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (SplashScreen splash = new SplashScreen())
            {
                splash.Show();
                Application.DoEvents(); // Важно для обновления UI заставки

                // Здесь выполняется долгая инициализация
                Thread.Sleep(3000); // Пример задержки - 3 секунды

                // После инициализации создаем и показываем главную форму
             
            }
            ThemeManager.LoadTheme();
            Application.Run(new SpMainForm());

        }
    }
}
