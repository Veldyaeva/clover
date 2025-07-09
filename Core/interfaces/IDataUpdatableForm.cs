using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Core.interfaces
{
    public interface IDataUpdatableForm
    {
        void UpdateDataInForm(string table);
    }
    public interface IDataUpdatableFormAsync
    {
        Task UpdateDataInFormAsync(string table);
    }
    public static class ControlExtensions
    {
        public static Task InvokeAsync(this Control control, Func<Task> asyncAction)
        {
            var tcs = new TaskCompletionSource<bool>();
            control.BeginInvoke(new MethodInvoker(async () =>
            {
                try
                {
                    await asyncAction();
                    tcs.SetResult(true);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }));
            return tcs.Task;
        }
    }
}
