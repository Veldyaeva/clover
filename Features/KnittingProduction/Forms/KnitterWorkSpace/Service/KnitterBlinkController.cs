using System;
using System.Drawing;
using System.Windows.Forms;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    public sealed class KnitterBlinkController : IDisposable
    {
        private readonly Timer _blinkCheckTimer = new Timer();
        private readonly Timer _blinkTimer = new Timer();
        private readonly Control _targetControl;
        private readonly Color _defaultBackColor;
        private bool _isBlinking;
        private string _lastBlinkWindowKey;
        private DateTime _blinkEndTime;

        public KnitterBlinkController(Control targetControl)
        {
            _targetControl = targetControl ?? throw new ArgumentNullException(nameof(targetControl));
            _defaultBackColor = targetControl.BackColor;
            MorningTime = new TimeSpan(15, 0, 0);
            EveningTime = new TimeSpan(16, 0, 0);
        }

        public TimeSpan MorningTime { get; set; }
        public TimeSpan EveningTime { get; set; }

        public void Start()
        {
            _blinkCheckTimer.Interval = 15_000;
            _blinkCheckTimer.Tick -= BlinkCheckTimer_Tick;
            _blinkCheckTimer.Tick += BlinkCheckTimer_Tick;
            _blinkCheckTimer.Start();

            _blinkTimer.Interval = 500;
            _blinkTimer.Tick -= BlinkTimer_Tick;
            _blinkTimer.Tick += BlinkTimer_Tick;
        }

        public void ResetWindow()
        {
            _lastBlinkWindowKey = null;
        }

        public void Dispose()
        {
            StopBlink();
            _blinkCheckTimer.Stop();
            _blinkCheckTimer.Dispose();
            _blinkTimer.Dispose();
        }

        private void BlinkCheckTimer_Tick(object sender, EventArgs e)
        {
            if (_isBlinking)
                return;

            var now = DateTime.Now;
            var windowKey = GetBlinkWindowKey(now);
            if (windowKey == null || windowKey == _lastBlinkWindowKey)
                return;

            var windowStart = GetWindowStart(now);
            if (now >= windowStart && now <= windowStart.AddMinutes(1))
            {
                StartBlink(windowKey, windowStart.AddMinutes(1));
            }
        }

        private void BlinkTimer_Tick(object sender, EventArgs e)
        {
            if (!_isBlinking)
                return;

            if (DateTime.Now >= _blinkEndTime)
            {
                StopBlink();
                return;
            }

            _targetControl.BackColor = _targetControl.BackColor == Color.MediumPurple
                ? _defaultBackColor
                : Color.MediumPurple;
        }

        private void StartBlink(string windowKey, DateTime endTime)
        {
            _isBlinking = true;
            _blinkEndTime = endTime;
            _lastBlinkWindowKey = windowKey;
            _blinkTimer.Start();
        }

        private void StopBlink()
        {
            _blinkTimer.Stop();
            _isBlinking = false;
            _targetControl.BackColor = _defaultBackColor;
        }

        private string GetBlinkWindowKey(DateTime now)
        {
            if (IsInBlinkWindow(now))
                return $"{now:yyyyMMdd}_{now.Hour}";

            return null;
        }

        private DateTime GetWindowStart(DateTime now)
        {
            if (now.TimeOfDay >= MorningTime && now.TimeOfDay < MorningTime.Add(TimeSpan.FromHours(2)))
                return new DateTime(now.Year, now.Month, now.Day, MorningTime.Hours, MorningTime.Minutes, MorningTime.Seconds);

            if (now.TimeOfDay >= EveningTime)
                return new DateTime(now.Year, now.Month, now.Day, EveningTime.Hours, EveningTime.Minutes, EveningTime.Seconds);

            return new DateTime(now.Year, now.Month, now.Day, MorningTime.Hours, MorningTime.Minutes, MorningTime.Seconds);
        }

        private bool IsInBlinkWindow(DateTime now)
        {
            var startMorning = new DateTime(now.Year, now.Month, now.Day, MorningTime.Hours, MorningTime.Minutes, MorningTime.Seconds);
            var startEvening = new DateTime(now.Year, now.Month, now.Day, EveningTime.Hours, EveningTime.Minutes, EveningTime.Seconds);

            return (now >= startMorning && now <= startMorning.AddMinutes(1)) ||
                   (now >= startEvening && now <= startEvening.AddMinutes(1));
        }
    }
}
