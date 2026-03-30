using DevExpress.XtraEditors;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    public sealed class KnitterIdleSplashController : IDisposable
    {
        private readonly Timer _idleTimer = new Timer();
        private readonly Form _ownerForm;
        private readonly Control _activityRoot;
        private readonly Control _overlayTarget;
        private readonly Func<int?> _getCurrentTab;
        private readonly Action<int> _applySelectedTab;
        private readonly Action _closeOwner;
        private readonly Action<string, string> _logWarning;
        private List<FioModel> _cachedFioList = new List<FioModel>();
        private bool _isSplashShowing;

        public KnitterIdleSplashController(
            Form ownerForm,
            Control activityRoot,
            Control overlayTarget,
            Func<int?> getCurrentTab,
            Action<int> applySelectedTab,
            Action closeOwner,
            Action<string, string> logWarning)
        {
            _ownerForm = ownerForm ?? throw new ArgumentNullException(nameof(ownerForm));
            _activityRoot = activityRoot ?? throw new ArgumentNullException(nameof(activityRoot));
            _overlayTarget = overlayTarget ?? throw new ArgumentNullException(nameof(overlayTarget));
            _getCurrentTab = getCurrentTab ?? throw new ArgumentNullException(nameof(getCurrentTab));
            _applySelectedTab = applySelectedTab ?? throw new ArgumentNullException(nameof(applySelectedTab));
            _closeOwner = closeOwner ?? throw new ArgumentNullException(nameof(closeOwner));
            _logWarning = logWarning ?? throw new ArgumentNullException(nameof(logWarning));
        }

        public void UpdateFioList(IEnumerable<FioModel> fioList)
        {
            _cachedFioList = fioList?.ToList() ?? new List<FioModel>();
        }

        public void Start()
        {
            _idleTimer.Interval = 180000000;
            _idleTimer.Tick -= IdleTimer_Tick;
            _idleTimer.Tick += IdleTimer_Tick;

            _activityRoot.MouseMove += (_, __) => Reset();
            _activityRoot.KeyDown += (_, __) => Reset();
            _activityRoot.MouseClick += (_, __) => Reset();
            _activityRoot.MouseDown += (_, __) => Reset();
            _activityRoot.KeyPress += (_, __) => Reset();
            _ownerForm.Shown += (_, __) => AttachActivityHandlers(_activityRoot);
        }

        public void ShowSelectionSplash(int defaultTab)
        {
            PresentFioSelectionSplash(_cachedFioList, defaultTab);
        }

        public void Reset()
        {
            if (_isSplashShowing)
                return;

            _idleTimer.Stop();
            _idleTimer.Start();
        }

        public void Dispose()
        {
            _idleTimer.Stop();
            _idleTimer.Dispose();
        }

        private void AttachActivityHandlers(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                control.MouseMove += (_, __) => Reset();
                control.MouseClick += (_, __) => Reset();
                control.MouseDown += (_, __) => Reset();
                control.KeyDown += (_, __) => Reset();
                control.KeyPress += (_, __) => Reset();

                if (control.HasChildren)
                    AttachActivityHandlers(control);
            }
        }

        private void IdleTimer_Tick(object sender, EventArgs e)
        {
            if (_isSplashShowing)
                return;

            _idleTimer.Stop();

            if (_cachedFioList.Count > 0)
            {
                const int defaultTab = 1438;
                PresentFioSelectionSplash(_cachedFioList, defaultTab);
            }
        }

        private void PresentFioSelectionSplash(IReadOnlyCollection<FioModel> fioList, int defaultTab)
        {
            if (fioList == null || fioList.Count == 0)
            {
                XtraMessageBox.Show(_ownerForm, "Список сотрудников пуст. Обратитесь к администратору.", "Нет данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _logWarning("Список сотрудников пуст при показе выбора табельного номера.", nameof(PresentFioSelectionSplash));
                return;
            }

            _idleTimer.Stop();

            int? initialTab = _getCurrentTab();
            if (initialTab is null && fioList.Any(f => f.Tab == defaultTab))
                initialTab = defaultTab;

            _isSplashShowing = true;
            Form overlay = null;
            try
            {
                overlay = new Form
                {
                    FormBorderStyle = FormBorderStyle.None,
                    StartPosition = FormStartPosition.Manual,
                    ShowInTaskbar = false,
                    BackColor = Color.AliceBlue,
                    TopMost = false,
                    Owner = _ownerForm,
                    Bounds = _overlayTarget.RectangleToScreen(_overlayTarget.ClientRectangle)
                };
                overlay.Show();

                using (var splash = new FioSelectionSplash(fioList, initialTab))
                {
                    splash.StartPosition = FormStartPosition.CenterScreen;
                    var result = splash.ShowDialog(overlay);
                    if (result == DialogResult.OK && splash.SelectedTab.HasValue)
                    {
                        _applySelectedTab(splash.SelectedTab.Value);
                        Reset();
                    }
                    else
                    {
                        _ownerForm.BeginInvoke(new Action(_closeOwner));
                    }
                }
            }
            finally
            {
                if (overlay != null)
                {
                    try { overlay.Close(); } catch { }
                    overlay.Dispose();
                }

                _isSplashShowing = false;
            }
        }
    }
}
