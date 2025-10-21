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
                    };

                    overlayHandle = SplashScreenManager.ShowOverlayForm(targetGrid, options);

                    //overlayHandle = SplashScreenManager.ShowOverlayForm(targetGrid);
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
    }
}


