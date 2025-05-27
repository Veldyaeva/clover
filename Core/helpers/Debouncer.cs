using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Helpers
{
    public class Debouncer
    {
        private CancellationTokenSource _cts;

        public void Debounce(int milliseconds, Func<Task> action)
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(milliseconds, _cts.Token);
                    if (!_cts.Token.IsCancellationRequested)
                    {
                        await action();
                    }
                }
                catch (TaskCanceledException) { }
            });
        }
    }

}
