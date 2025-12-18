using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.CodeParser;
using DevExpress.XtraGrid;
using DevExpress.XtraSplashScreen;
using SewingProduction.Core.Class;
using WinFormsBindingSource = System.Windows.Forms.BindingSource;

namespace SewingProduction.Core.helpers
{
    public static class GridOverlayLoader
    {
        /// <summary>
        /// Загружает данные в BindingList с отображением оверлея на GridControl.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="targetGrid"></param>
        /// <param name="targetList"></param>
        /// <param name="bindingSource"></param>
        /// <param name="loadFunc"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task LoadListAsync<T>(
            GridControl targetGrid,
            BindingList<T> targetList,
            WinFormsBindingSource bindingSource,
            Func<CancellationToken, Task<IEnumerable<T>>> loadFunc,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            IOverlaySplashScreenHandle overlayHandle = null;
            try
            {
                if (targetGrid != null && targetGrid.Visible)
                {
                    int diameter = (int)(Math.Min(targetGrid.ClientSize.Width, targetGrid.ClientSize.Height) * 0.15);

                    var options = new OverlayWindowOptions
                    {
                        // Размер спиннера/индикатора
                        ImageSize = new Size(diameter, diameter),
                        // BackColor = Color.FromArgb(…),
                        // Opacity = 0.8,
                        FadeIn = true,
                        FadeOut = true,
                    };

                    overlayHandle = SplashScreenManager.ShowOverlayForm(targetGrid, options);

                }

                var data = await loadFunc(cancellationToken) ?? Enumerable.Empty<T>();
                cancellationToken.ThrowIfCancellationRequested();

                if (targetList != null)
                {
                    targetList.RaiseListChangedEvents = false;
                    targetList.Clear();
                    foreach (var item in data)
                        targetList.Add(item);
                    targetList.RaiseListChangedEvents = true;
                }

                bindingSource?.ResetBindings(false);
            }
            catch { SplashScreenManager.CloseOverlayForm(overlayHandle); }
            finally
            {
                try
                {
                    if (overlayHandle != null)
                    {
                        if (targetGrid != null && targetGrid.IsHandleCreated)
                        {
                            targetGrid.BeginInvoke(new Action(() =>
                            {
                                try { SplashScreenManager.CloseOverlayForm(overlayHandle); } catch { }
                            }));
                        }
                        else
                        {
                            SplashScreenManager.CloseOverlayForm(overlayHandle);
                        }
                    }
                }
                catch { SplashScreenManager.CloseOverlayForm(overlayHandle); }
            }
        }
        public static Task LoadListAsync<T>(
            GridControl targetGrid,
            BindingList<T> targetList,
            WinFormsBindingSource bindingSource,
            Task<IEnumerable<T>> loadTask,
            CancellationToken ct)
        {
            return LoadListAsync(targetGrid, targetList, bindingSource, _ => loadTask, ct);
        }
        /// <summary>
        /// Executes a specified asynchronous task while displaying an overlay on a given grid control.
        /// </summary>
        /// <remarks>The overlay is displayed on the grid control to indicate that a background operation
        /// is in progress. The overlay is automatically removed once the task completes or if an exception
        /// occurs.</remarks>
        /// <param name="grid">The <see cref="GridControl"/> on which to display the overlay. If the grid is null or not visible, no
        /// overlay is shown.</param>
        /// <param name="action">The asynchronous task to execute. This task is awaited and runs concurrently with the overlay display.</param>
        /// <param name="ct">A <see cref="CancellationToken"/> to observe while waiting for the task to complete. The task is canceled if
        /// the token is triggered.</param>
        /// <returns></returns>
        public static async Task RunTaskWithOverlayAsync(
            GridControl grid,
            Func<Task> action,
            CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            IOverlaySplashScreenHandle overlay = null;
            try
            {
                if (grid != null && grid.Visible)
                {
                    int d = (int)(Math.Min(grid.ClientSize.Width, grid.ClientSize.Height) * 0.15);
                    var opts = new OverlayWindowOptions { ImageSize = new Size(d, d), FadeIn = true, FadeOut = true };
                    overlay = SplashScreenManager.ShowOverlayForm(grid, opts);
                }
                await action().ConfigureAwait(true);
            }
            finally
            {
                try
                {
                    if (overlay != null)
                    {
                        if (grid != null && grid.IsHandleCreated)
                            grid.BeginInvoke(new Action(() => { try { SplashScreenManager.CloseOverlayForm(overlay); } catch { } }));
                        else
                            SplashScreenManager.CloseOverlayForm(overlay);
                    }
                }
                catch 
                {
                    SplashScreenManager.CloseOverlayForm(overlay);
                }
            }
        }
        public static Task RunTaskWithOverlayAsync(
            GridControl grid,
            Action action,
            CancellationToken ct)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            return RunTaskWithOverlayAsync(
                grid,
                () =>
                {
                    action();                   // вызываем синхронный метод
                    return Task.CompletedTask;  // возвращаем уже завершённый Task
                },
                ct);
        }
        //internal static async Task RunTaskWithOverlayAsync(CustomGridControl gridControl, object v, CancellationToken cancellationToken)
        //{
        //    RunTaskWithOverlayAsync(gridControl, () => Task.CompletedTask, cancellationToken);
        //}
    }
}


