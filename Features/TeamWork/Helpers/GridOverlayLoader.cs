using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.XtraGrid;
using DevExpress.XtraSplashScreen;
using WinFormsBindingSource = System.Windows.Forms.BindingSource;

namespace SewingProduction.Features.TeamWork.Helpers
{
    public static class GridOverlayLoader
    {
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
                    overlayHandle = SplashScreenManager.ShowOverlayForm(targetGrid);
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


