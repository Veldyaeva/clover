using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Helpers
{
    public static class ControlUiExtensions
    {
        public static Task UI(this Control control, Action action)
        {
            if (control.IsDisposed)
                return Task.CompletedTask;

            if (control.InvokeRequired)
            {
                var tcs = new TaskCompletionSource<object?>();
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
                return tcs.Task;
            }

            action();
            return Task.CompletedTask;
        }
    }
}
