using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Extensions
{
    public static class ControlExtensions
    {
        /// <summary>
        /// Асинхронно выполняет действие в UI-потоке для элемента управления.
        /// </summary>
        public static Task InvokeAsync(this Control control, Action action)
        {
            var tcs = new TaskCompletionSource<object>();

            if (control.InvokeRequired)
            {
                control.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        action();
                        tcs.SetResult(null);
                    }
                    catch (Exception ex)
                    {
                        tcs.SetException(ex);
                    }
                }));
            }
            else
            {
                try
                {
                    action();
                    tcs.SetResult(null);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }

            return tcs.Task;
        }
    }
}
