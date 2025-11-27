using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.XtraGrid;
using DevExpress.XtraSplashScreen;
using WinFormsBindingSource = System.Windows.Forms.BindingSource;

namespace SewingProduction.Core.helpers
{
    public static class GridOverlayLoader
    {
        private static Func<CancellationToken, ValueTask<IEnumerable<T>>> ToLoader<T>(
            Func<CancellationToken, Task<IEnumerable<T>>> f)
            => async ct => await f(ct).ConfigureAwait(false);

        private static Func<CancellationToken, ValueTask<IEnumerable<T>>> ToLoader<T>(
            Func<Task<IEnumerable<T>>> f)
            => async _ => await f().ConfigureAwait(false);

        private static Func<CancellationToken, ValueTask<IEnumerable<T>>> ToLoader<T>(
            Func<IEnumerable<T>> f)
            => _ => new ValueTask<IEnumerable<T>>(f());

        private static Func<CancellationToken, ValueTask<IEnumerable<T>>> ToLoader<T>(
            Task<IEnumerable<T>> t)
            => async _ => await t.ConfigureAwait(false);

        private static Func<CancellationToken, ValueTask<IEnumerable<T>>> ToLoader<T>(
            IEnumerable<T> data)
            => _ => new ValueTask<IEnumerable<T>>(data ?? Enumerable.Empty<T>());

        private static Func<CancellationToken, ValueTask<IEnumerable<T>>> ToLoader<T>(
            IAsyncEnumerable<T> stream)
            => async ct =>
            {
                var list = new List<T>();
                await foreach (var item in stream.WithCancellation(ct).ConfigureAwait(false))
                    list.Add(item);
                return list;
            };

        private static Func<CancellationToken, ValueTask<IEnumerable<T>>> ToLoader<T>(
            Func<CancellationToken, IAsyncEnumerable<T>> fstream)
            => async ct =>
            {
                var list = new List<T>();
                await foreach (var item in fstream(ct).WithCancellation(ct).ConfigureAwait(false))
                    list.Add(item);
                return list;
            };

        // Обобщённые адаптеры для Task<TData>/Func<Task<TData>>, где TData : IEnumerable<T>
        private static Func<CancellationToken, ValueTask<IEnumerable<T>>> ToLoader<T, TData>(
            Func<CancellationToken, Task<TData>> f)
            where TData : IEnumerable<T>
            => async ct => (IEnumerable<T>)await f(ct).ConfigureAwait(false);

        private static Func<CancellationToken, ValueTask<IEnumerable<T>>> ToLoader<T, TData>(
            Func<Task<TData>> f)
            where TData : IEnumerable<T>
            => async _ => (IEnumerable<T>)await f().ConfigureAwait(false);

        private static Func<CancellationToken, ValueTask<IEnumerable<T>>> ToLoader<T, TData>(
            Task<TData> t)
            where TData : IEnumerable<T>
            => async _ => (IEnumerable<T>)await t.ConfigureAwait(false);

        private static Func<CancellationToken, ValueTask<IEnumerable<T>>> ToLoader<T, TData>(
            TData data)
            where TData : IEnumerable<T>
            => _ => new ValueTask<IEnumerable<T>>(data ?? (IEnumerable<T>)Array.Empty<T>());
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
        public static async Task LoadListAsyncCore<T>(
            GridControl targetGrid,
            BindingList<T> targetList,
            WinFormsBindingSource bindingSource,
            Func<CancellationToken, ValueTask<IEnumerable<T>>> loader,
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
                         FadeIn = true, FadeOut = true,
                    };

                    overlayHandle = SplashScreenManager.ShowOverlayForm(targetGrid, options);
                    
                }

                var data = await loader(cancellationToken).ConfigureAwait(true) ?? Enumerable.Empty<T>();
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
        // Исходная сигнатура
        public static Task LoadListAsync<T>(
            GridControl targetGrid, BindingList<T> targetList, WinFormsBindingSource bindingSource,
            Func<CancellationToken, Task<IEnumerable<T>>> loadFunc, CancellationToken ct)
            => LoadListAsyncCore(targetGrid, targetList, bindingSource, ToLoader(loadFunc), ct);

        // Без CancellationToken
        public static Task LoadListAsync<T>(
            GridControl targetGrid, BindingList<T> targetList, WinFormsBindingSource bindingSource,
            Func<Task<IEnumerable<T>>> loadFunc, CancellationToken ct)
            => LoadListAsyncCore(targetGrid, targetList, bindingSource, ToLoader(loadFunc), ct);

        // Синхронная функция
        public static Task LoadListAsync<T>(
            GridControl targetGrid, BindingList<T> targetList, WinFormsBindingSource bindingSource,
            Func<IEnumerable<T>> loadFunc, CancellationToken ct)
            => LoadListAsyncCore(targetGrid, targetList, bindingSource, ToLoader(loadFunc), ct);

        // Готовый Task<IEnumerable<T>>
        public static Task LoadListAsync<T>(
            GridControl targetGrid, BindingList<T> targetList, WinFormsBindingSource bindingSource,
            Task<IEnumerable<T>> loadTask, CancellationToken ct)
            => LoadListAsyncCore(targetGrid, targetList, bindingSource, ToLoader(loadTask), ct);

        // Готовая коллекция
        public static Task LoadListAsync<T>(
            GridControl targetGrid, BindingList<T> targetList, WinFormsBindingSource bindingSource,
            IEnumerable<T> data, CancellationToken ct)
            => LoadListAsyncCore(targetGrid, targetList, bindingSource, ToLoader(data), ct);

        // Async-стрим
        public static Task LoadListAsync<T>(
            GridControl targetGrid, BindingList<T> targetList, WinFormsBindingSource bindingSource,
            IAsyncEnumerable<T> stream, CancellationToken ct)
            => LoadListAsyncCore(targetGrid, targetList, bindingSource, ToLoader(stream), ct);

        public static Task LoadListAsync<T>(
            GridControl targetGrid, BindingList<T> targetList, WinFormsBindingSource bindingSource,
            Func<CancellationToken, IAsyncEnumerable<T>> streamFunc, CancellationToken ct)
            => LoadListAsyncCore(targetGrid, targetList, bindingSource, ToLoader(streamFunc), ct);

        // Обобщённые перегрузки под Task<List<T>> / Task<T[]> / и т.п.
        public static Task LoadListAsync<T, TData>(
            GridControl targetGrid, BindingList<T> targetList, WinFormsBindingSource bindingSource,
            Func<CancellationToken, Task<TData>> loadFunc, CancellationToken ct)
            where TData : IEnumerable<T>
            => LoadListAsyncCore(targetGrid, targetList, bindingSource, ToLoader<T, TData>(loadFunc), ct);

        public static Task LoadListAsync<T, TData>(
            GridControl targetGrid, BindingList<T> targetList, WinFormsBindingSource bindingSource,
            Func<Task<TData>> loadFunc, CancellationToken ct)
            where TData : IEnumerable<T>
            => LoadListAsyncCore(targetGrid, targetList, bindingSource, ToLoader<T, TData>(loadFunc), ct);

        public static Task LoadListAsync<T, TData>(
            GridControl targetGrid, BindingList<T> targetList, WinFormsBindingSource bindingSource,
            Task<TData> loadTask, CancellationToken ct)
            where TData : IEnumerable<T>
            => LoadListAsyncCore(targetGrid, targetList, bindingSource, ToLoader<T, TData>(loadTask), ct);

        public static Task LoadListAsync<T, TData>(
            GridControl targetGrid, BindingList<T> targetList, WinFormsBindingSource bindingSource,
            TData data, CancellationToken ct)
            where TData : IEnumerable<T>
            => LoadListAsyncCore(targetGrid, targetList, bindingSource, ToLoader<T, TData>(data), ct);
    
    }
}



