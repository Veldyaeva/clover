using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction
{
    /// <summary>
    /// Вспомогательный класс для работы со сплеш-скрином при загрузке данных
    /// </summary>
    public static class SplashScreenHelper
    {
        private static SplashScreen _splashScreen;
        private static Thread _splashThread;

        /// <summary>
        /// Показывает сплеш-скрин в отдельном потоке
        /// </summary>
        public static void ShowSplash()
        {
            // Создаем и запускаем сплеш-скрин в отдельном потоке
            _splashThread = new Thread(() =>
            {
                _splashScreen = new SplashScreen();
                _splashScreen.FormClosed += (s, e) => Application.ExitThread();
                _splashScreen.StartPosition = FormStartPosition.CenterScreen;
                _splashScreen.TopMost = true;
                Application.Run(_splashScreen);
            });

            _splashThread.SetApartmentState(ApartmentState.STA);
            _splashThread.Start();
        }

        /// <summary>
        /// Скрывает сплеш-скрин
        /// </summary>
        public static void CloseSplash()
        {
            if (_splashScreen != null && !_splashScreen.IsDisposed)
            {
                if (_splashScreen.InvokeRequired)
                {
                    _splashScreen.Invoke(new Action(() => _splashScreen.Close()));
                }
                else
                {
                    _splashScreen.Close();
                }

                if (_splashThread != null && _splashThread.IsAlive)
                {
                    _splashThread.Join(1000);
                }
            }
        }

        /// <summary>
        /// Запускает операцию с отображением сплеш-скрина
        /// </summary>
        /// <param name="action">Действие, которое нужно выполнить</param>
        public static async Task RunWithSplashAsync(Func<Task> action)
        {
            ShowSplash();
            try
            {
                await action();
            }
            finally
            {
                CloseSplash();
            }
        }
    }
}