using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Calendar;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace SewingProduction.CustomControls
{
    #region Enums

    public enum CalendarLayoutMode
    {
        MonthGrid = 0,
        HorizontalDays = 1
    }

    public enum WeekendMode
    {
        SatSun = 0,   // стандарт: выходные = суббота/воскресенье
        Cyclic = 1    // график: 5/2, 2/2, 4/2 ...
    }

    #endregion

    #region DevExpress Russian Localizer

    /// <summary>
    /// Русификация стандартных строк DevExpress для календарей/DateEdit.
    /// Делается один раз на приложение через Localizer.Active.
    /// </summary>
    internal sealed class RuEditorsLocalizer : Localizer
    {
        public override string GetLocalizedString(StringId id)
        {
            if (id == StringId.DateEditToday) return "Сегодня";
            if (id == StringId.DateEditClear) return "Очистить";
            return base.GetLocalizedString(id);
        }
    }

    #endregion

    public class CustomCalendarControl : CalendarControl, IThemeable
    {
        #region Static localization bootstrap

        private static bool _ruLocalizerApplied;
        private PropertyChangedEventHandler _settingsChangedHandler;

        private static void EnsureRussianLocalizer()
        {
            if (_ruLocalizerApplied) return;
            Localizer.Active = new RuEditorsLocalizer(); // глобально
            _ruLocalizerApplied = true;
        }

        #endregion

        #region Fields

        // Bottom panel
        private readonly PanelControl _bottomPanel;
        private readonly SplitterControl _splitter;
        private readonly FlowLayoutPanel _bottomLeftFlow;
        private readonly FlowLayoutPanel _bottomRightFlow;

        // Horizontal mode
        private XtraScrollableControl _horizontalHost;
        private FlowLayoutPanel _daysFlow;
        private DateTime _lastBuiltMonth = DateTime.MinValue;

        // Zoom
        private int _zoomLevel = 0;

        // Schedule
        private int _workDaysInCycle = 5;
        private int _offDaysInCycle = 2;
        private DateTime _cycleStartDate = DateTime.Today.Date;

        // Layout mode
        private CalendarLayoutMode _layoutMode = CalendarLayoutMode.MonthGrid;

        #endregion

        #region Ctor / Dispose
        public CustomCalendarControl()
        {
            EnsureRussianLocalizer();

            FirstDayOfWeek = System.DayOfWeek.Monday;
            ShowTodayButton = true;

            CustomDrawDayNumberCell += OnCustomDrawDayNumberCell;
            DisableCalendarDate += OnDisableCalendarDate;

            #region Bottom panel init

            _bottomPanel = new PanelControl
            {
                Dock = DockStyle.Bottom,
                Height = 48,
                BorderStyle = BorderStyles.NoBorder,
                Padding = new Padding(6),
                Visible = true
            };

            _bottomLeftFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = true,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            _bottomRightFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                AutoSize = true,
                AutoScroll = false,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            _bottomPanel.Controls.Add(_bottomLeftFlow);
            _bottomPanel.Controls.Add(_bottomRightFlow);

            _splitter = new SplitterControl
            {
                Dock = DockStyle.Bottom,
                Height = 6,
                Visible = false
            };

            Controls.Add(_bottomPanel);
            Controls.Add(_splitter);

            _bottomPanel.BringToFront();
            _splitter.BringToFront();

            #endregion

            // 1) Подписка на CustomSettings — ОДИН раз, и ДО применения
            _settingsChangedHandler = (_, __) => ApplyCustomSettings();
            CustomSettings.PropertyChanged += _settingsChangedHandler;

            // 2) Тема
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;

            // 3) Дефолтный Zoom
            ZoomLevel = 0;

            // 4) Перестраиваем горизонтальную ленту при смене даты
            EditValueChanged += (_, __) =>
            {
                if (LayoutMode == CalendarLayoutMode.HorizontalDays)
                    BuildHorizontalForMonth(DateTime);
            };

            // 5) Применяем CustomSettings (после подписки)
            ApplyCustomSettings();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;

                if (_settingsChangedHandler != null)
                    CustomSettings.PropertyChanged -= _settingsChangedHandler;
            }
            base.Dispose(disposing);
        }

        #endregion
        #region CustomSettings

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class CalendarCustomSettings : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            private void Set<T>(ref T field, T value, [CallerMemberName] string prop = null)
            {
                if (EqualityComparer<T>.Default.Equals(field, value)) return;
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }

            #region Bottom panel
            private bool _bottomPanelVisible = false;
            [DefaultValue(true)]
            public bool BottomPanelVisible { get => _bottomPanelVisible; set => Set(ref _bottomPanelVisible, value); }

            private int _bottomPanelHeight = 48;
            [DefaultValue(48)]
            public int BottomPanelHeight { get => _bottomPanelHeight; set => Set(ref _bottomPanelHeight, value); }

            private bool _bottomPanelResizable = false;
            [DefaultValue(false)]
            public bool BottomPanelResizable { get => _bottomPanelResizable; set => Set(ref _bottomPanelResizable, value); }
            #endregion

            #region Theme / colors
            private bool _useThemeByDefault = true;
            [DefaultValue(true)]
            public bool UseThemeByDefault { get => _useThemeByDefault; set => Set(ref _useThemeByDefault, value); }

            private Color _normalDayBackColor = Color.Empty;
            public Color NormalDayBackColor { get => _normalDayBackColor; set => Set(ref _normalDayBackColor, value); }

            private Color _offDayBackColor = Color.Empty;
            public Color OffDayBackColor { get => _offDayBackColor; set => Set(ref _offDayBackColor, value); }

            private Color _offDayForeColor = Color.Empty;
            public Color OffDayForeColor { get => _offDayForeColor; set => Set(ref _offDayForeColor, value); }

            private Color _specialBackColor = Color.Empty;
            public Color SpecialBackColor { get => _specialBackColor; set => Set(ref _specialBackColor, value); }

            private Color _specialForeColor = Color.Empty;
            public Color SpecialForeColor { get => _specialForeColor; set => Set(ref _specialForeColor, value); }
            #endregion

            #region Zoom
            private bool _enableZoomByCtrlWheel = true;
            [DefaultValue(true)]
            public bool EnableZoomByCtrlWheel { get => _enableZoomByCtrlWheel; set => Set(ref _enableZoomByCtrlWheel, value); }

            private int _zoomLevel = 0;
            [DefaultValue(0)]
            public int ZoomLevel { get => _zoomLevel; set => Set(ref _zoomLevel, value); }
            #endregion

            #region Schedule
            private WeekendMode _weekendMode = WeekendMode.SatSun;
            [DefaultValue(WeekendMode.SatSun)]
            public WeekendMode WeekendMode { get => _weekendMode; set => Set(ref _weekendMode, value); }

            private int _workDaysInCycle = 5;
            [DefaultValue(5)]
            public int WorkDaysInCycle { get => _workDaysInCycle; set => Set(ref _workDaysInCycle, value); }

            private int _offDaysInCycle = 2;
            [DefaultValue(2)]
            public int OffDaysInCycle { get => _offDaysInCycle; set => Set(ref _offDaysInCycle, value); }

            private DateTime _cycleStartDate = DateTime.Today.Date;
            public DateTime CycleStartDate { get => _cycleStartDate; set => Set(ref _cycleStartDate, value.Date); }
            #endregion

            #region Layout mode
            private CalendarLayoutMode _layoutMode = CalendarLayoutMode.MonthGrid;
            [DefaultValue(CalendarLayoutMode.MonthGrid)]
            public CalendarLayoutMode LayoutMode { get => _layoutMode; set => Set(ref _layoutMode, value); }

            private bool _horizontalHeaderVisible = true;
            [DefaultValue(true)]
            public bool HorizontalHeaderVisible { get => _horizontalHeaderVisible; set => Set(ref _horizontalHeaderVisible, value); }

            private bool _horizontalNavigationVisible = true;
            [DefaultValue(true)]
            public bool HorizontalNavigationVisible { get => _horizontalNavigationVisible; set => Set(ref _horizontalNavigationVisible, value); }

            private bool _horizontalShowMonth = true;
            [DefaultValue(true)]
            public bool HorizontalShowMonth { get => _horizontalShowMonth; set => Set(ref _horizontalShowMonth, value); }

            private bool _horizontalShowYear = true;
            [DefaultValue(true)]
            public bool HorizontalShowYear { get => _horizontalShowYear; set => Set(ref _horizontalShowYear, value); }

            private int _horizontalHeaderHeight = 30;
            [DefaultValue(30)]
            public int HorizontalHeaderHeight { get => _horizontalHeaderHeight; set => Set(ref _horizontalHeaderHeight, value); }

            private int _horizontalRowHeight = 46;
            [DefaultValue(46)]
            public int HorizontalRowHeight { get => _horizontalRowHeight; set => Set(ref _horizontalRowHeight, value); }

            private bool _horizontalCompactHeight = true;
            [DefaultValue(true)]
            public bool HorizontalCompactHeight { get => _horizontalCompactHeight; set => Set(ref _horizontalCompactHeight, value); }

            private bool _autoCompactInLayoutControl = true;
            [DefaultValue(true)]
            public bool AutoCompactInLayoutControl { get => _autoCompactInLayoutControl; set => Set(ref _autoCompactInLayoutControl, value); }
            #endregion

            public override string ToString() => "Настройки календаря";
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("CustomSettings")]
        public CalendarCustomSettings CustomSettings { get; } = new CalendarCustomSettings();

        #endregion

        #region Public API - Common options

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public HashSet<DateTime> SpecialDates { get; } = new HashSet<DateTime>();

        [DefaultValue(false)]
        public bool BlockFutureDates { get; set; } = false;

        [DefaultValue(true)]
        public bool UseThemeByDefault { get; set; } = true;

        #endregion

        #region Public API - Colors (can be overridden)

        public Color NormalDayBackColor { get; set; } = Color.Empty;
        public Color OffDayBackColor { get; set; } = Color.Empty;
        public Color OffDayForeColor { get; set; } = Color.Empty;
        public Color SpecialBackColor { get; set; } = Color.Empty;
        public Color SpecialForeColor { get; set; } = Color.Empty;

        #endregion

        #region Public API - Zoom

        [DefaultValue(true)]
        public bool EnableZoomByCtrlWheel { get; set; } = true;

        [DefaultValue(0)]
        public int ZoomLevel
        {
            get => _zoomLevel;
            set
            {
                _zoomLevel = Math.Max(-5, Math.Min(10, value));
                ApplyZoom();
                Invalidate();
            }
        }

        #endregion

        #region Public API - Bottom panel

        /// <summary>Панель снизу, если нужно доступ к DevExpress PanelControl (например BorderStyle).</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("Layout")]
        public PanelControl BottomPanel => _bottomPanel;

        [Category("Layout")]
        [DefaultValue(false)]
        public bool BottomPanelVisible
        {
            get => _bottomPanel.Visible;
            set
            {
                _bottomPanel.Visible = value;
                _splitter.Visible = value && BottomPanelResizable;
                Invalidate();
                UpdateCompactHeight();
            }
        }

        [Category("Layout")]
        [DefaultValue(48)]
        public int BottomPanelHeight
        {
            get => _bottomPanel.Height;
            set
            {
                _bottomPanel.Height = Math.Max(0, value); 
                UpdateCompactHeight();
            }
        }

        /// <summary>Разрешить менять высоту нижней панели сплиттером.</summary>
        [Category("Layout")]
        [DefaultValue(false)]
        public bool BottomPanelResizable
        {
            get => _splitter.Visible;
            set
            {
            _splitter.Visible = value && BottomPanelVisible;
            UpdateCompactHeight();
            }
}

        /// <summary>Добавить контрол в левую часть нижней панели (удобно из конструктора формы).</summary>
        public T AddBottomLeft<T>(T control) where T : Control
        {
            control.Margin = new Padding(3);
            _bottomLeftFlow.Controls.Add(control);
            return control;
        }

        /// <summary>Добавить контрол в правую часть нижней панели (удобно из конструктора формы).</summary>
        public T AddBottomRight<T>(T control) where T : Control
        {
            control.Margin = new Padding(3);
            _bottomRightFlow.Controls.Add(control);
            return control;
        }

        #endregion

        #region Public API - Schedule

        [DefaultValue(WeekendMode.SatSun)]
        public WeekendMode WeekendMode { get; set; } = WeekendMode.SatSun;

        [DefaultValue(5)]
        public int WorkDaysInCycle
        {
            get => _workDaysInCycle;
            set { _workDaysInCycle = Math.Max(0, value); Invalidate(); }
        }

        [DefaultValue(2)]
        public int OffDaysInCycle
        {
            get => _offDaysInCycle;
            set { _offDaysInCycle = Math.Max(0, value); Invalidate(); }
        }

        public DateTime CycleStartDate
        {
            get => _cycleStartDate;
            set { _cycleStartDate = value.Date; Invalidate(); }
        }

        public void SetSchedule(int workDays, int offDays, DateTime cycleStart)
        {
            WeekendMode = WeekendMode.Cyclic;
            WorkDaysInCycle = workDays;
            OffDaysInCycle = offDays;
            CycleStartDate = cycleStart.Date;
        }

        public bool IsOffDay(DateTime date)
        {
            date = date.Date;

            if (WeekendMode == WeekendMode.SatSun)
                return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;

            int cycleLen = WorkDaysInCycle + OffDaysInCycle;
            if (cycleLen <= 0) return false;

            int diff = (date - CycleStartDate.Date).Days;
            int pos = diff % cycleLen;
            if (pos < 0) pos += cycleLen;

            return pos >= WorkDaysInCycle;
        }

        #endregion

        #region Public API - Layout Mode (horizontal)

        [DefaultValue(CalendarLayoutMode.MonthGrid)]
        public CalendarLayoutMode LayoutMode
        {
            get => _layoutMode;
            set
            {
                _layoutMode = value;
                EnsureHorizontalUi();
                ApplyLayoutMode();
            }
        }

        /// <summary>
        /// Компактная высота в горизонтальном режиме.
        /// Пытается уменьшить Height контрола, чтобы не было "пустого места снизу".
        /// Работает, только если контейнер позволяет менять высоту (не Dock=Fill).
        /// </summary>
        [DefaultValue(true)]
        public bool HorizontalCompactHeight { get; set; } = true;

        /// <summary>Высота строки дней в горизонтальном режиме.</summary>
        [DefaultValue(46)]
        public int HorizontalRowHeight { get; set; } = 46;

        #endregion

        #region Theme

        public void ApplyTheme()
        {
            if (!UseThemeByDefault) return;

            Font = ThemeManager.SharedSettings.DefaultFont;
            BackColor = ThemeManager.ActiveTheme.GridBackground;
            ForeColor = ThemeManager.ActiveTheme.GridTextColor;

            if (NormalDayBackColor.IsEmpty)
                NormalDayBackColor = ThemeManager.ActiveTheme.GridRowBackground;

            if (OffDayBackColor.IsEmpty)
                OffDayBackColor = ThemeManager.ActiveTheme.BandHighlightColor;

            if (OffDayForeColor.IsEmpty)
                OffDayForeColor = ThemeManager.ActiveTheme.GridTextColor;

            if (SpecialBackColor.IsEmpty)
                SpecialBackColor = ThemeManager.ActiveTheme.HighlightBackground;

            if (SpecialForeColor.IsEmpty)
                SpecialForeColor = ThemeManager.ActiveTheme.GridTextColor;

            ApplyZoom();
            Invalidate();

            if (LayoutMode == CalendarLayoutMode.HorizontalDays)
                RebuildHorizontal(force: true);
        }

        private void OnThemeChanged() => ApplyTheme();

        #endregion

        #region Zoom internals

        private void ApplyZoom()
        {
            CalendarAppearance.DayCell.FontSizeDelta = _zoomLevel;
            CalendarAppearance.Header.FontSizeDelta = _zoomLevel;
            CalendarAppearance.WeekNumber.FontSizeDelta = _zoomLevel;
            CalendarAppearance.WeekDay.FontSizeDelta = _zoomLevel;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (EnableZoomByCtrlWheel && (ModifierKeys & Keys.Control) == Keys.Control)
            {
                ZoomLevel += e.Delta > 0 ? 1 : -1;
                return;
            }
            base.OnMouseWheel(e);
        }

        #endregion

        #region Special dates helpers

        public void AddSpecialDate(DateTime date) { SpecialDates.Add(date.Date); RebuildHorizontal(force: true); Invalidate(); }
        public void RemoveSpecialDate(DateTime date) { SpecialDates.Remove(date.Date); RebuildHorizontal(force: true); Invalidate(); }
        public void ClearSpecialDates() { SpecialDates.Clear(); RebuildHorizontal(force: true); Invalidate(); }

        #endregion

        #region Disable date handler

        private void OnDisableCalendarDate(object sender, DisableCalendarDateEventArgs e)
        {
            if (BlockFutureDates && e.Date.Date > DateTime.Today)
                e.IsDisabled = true;
        }

        #endregion

        #region Custom draw month grid

        private void OnCustomDrawDayNumberCell(object sender, CustomDrawDayNumberCellEventArgs e)
        {
            // В горизонтальном режиме сетку месяца мы не трогаем/не рисуем (она под оверлеем),
            // но обработчик может вызываться — оставляем как есть.
            var d = e.DateTime.Date;

            bool isOff = IsOffDay(d);
            bool isSpecial = SpecialDates.Contains(d);
            bool isToday = d == DateTime.Today;

            Color back = e.Style.BackColor.IsEmpty ? BackColor : e.Style.BackColor;
            Color fore = e.Style.ForeColor.IsEmpty ? ForeColor : e.Style.ForeColor;

            if (!NormalDayBackColor.IsEmpty)
                back = NormalDayBackColor;

            if (isOff)
            {
                if (!OffDayBackColor.IsEmpty) back = OffDayBackColor;
                if (!OffDayForeColor.IsEmpty) fore = OffDayForeColor;
            }

            if (isSpecial)
            {
                if (!SpecialBackColor.IsEmpty) back = SpecialBackColor;
                if (!SpecialForeColor.IsEmpty) fore = SpecialForeColor;
            }

            using (var b = new SolidBrush(back))
                e.Cache.FillRectangle(b, e.Bounds);

            var drawFont = e.Style.Font ?? Font;
            using (var br = new SolidBrush(fore))
            {
                var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                e.Cache.Graphics.DrawString(d.Day.ToString(), drawFont, br, e.Bounds, sf);
            }

            if (isToday)
            {
                using var pen = new Pen(Color.DarkGreen, 2);
                var r = e.Bounds; r.Inflate(-2, -2);
                e.Cache.DrawRectangle(pen, r);
            }

            e.Handled = true;
        }

        #endregion

        #region Horizontal mode - Auto compact in LayoutControl

        [DefaultValue(true)]
        public bool AutoCompactInLayoutControl { get; set; } = true;

        #endregion

        #region Horizontal mode internals

        private void EnsureHorizontalUi()
        {
            if (_horizontalHost != null) return;

            _horizontalHost = new XtraScrollableControl
            {
                Dock = DockStyle.Top,
                Visible = false,
                //BorderStyle = BorderStyles.NoBorder,
                Padding = new Padding(0),
                Margin = new Padding(0),
                BackColor = BackColor,
                Height = HorizontalRowHeight
            };

            if (_horizontalHeader == null)
            {
                _horizontalHeader = new PanelControl
                {
                    Dock = DockStyle.Top,
                    Height = HorizontalHeaderHeight,
                    BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder,
                    Padding = new Padding(6, 4, 6, 4),
                    Visible = false
                };

                _btnPrevMonth = new SimpleButton
                {
                    Text = "◀",
                    Width = 32,
                    Dock = DockStyle.Left
                };
                _btnPrevMonth.Click += (_, __) => ChangeMonth(-1);

                _btnNextMonth = new SimpleButton
                {
                    Text = "▶",
                    Width = 32,
                    Dock = DockStyle.Right
                };
                _btnNextMonth.Click += (_, __) => ChangeMonth(+1);

                _lblMonthYear = new LabelControl
                {
                    Dock = DockStyle.Fill,
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    Appearance = { TextOptions = { HAlignment = DevExpress.Utils.HorzAlignment.Center } }
                };

                _horizontalHeader.Controls.Add(_lblMonthYear);
                _horizontalHeader.Controls.Add(_btnNextMonth);
                _horizontalHeader.Controls.Add(_btnPrevMonth);

                // header должен быть поверх и над строкой дней
                Controls.Add(_horizontalHeader);
                _horizontalHeader.BringToFront();

                _bottomPanel.BringToFront();
                _splitter.BringToFront();
            }


            _daysFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = false,
                Height = HorizontalRowHeight,
                WrapContents = false,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(6, 6, 6, 6),
                Margin = new Padding(0),
                BackColor = BackColor
            };

            _horizontalHost.Controls.Add(_daysFlow);

            // Добавляем поверх календаря (оверлей)
            Controls.Add(_horizontalHost);
            _horizontalHost.BringToFront();

            // Но нижнюю панель оставляем сверху, чтобы не перекрывалась
            _bottomPanel.BringToFront();
            _splitter.BringToFront();
        }

        private void ApplyLayoutMode()
        {
            EnsureHorizontalUi();

            bool horiz = LayoutMode == CalendarLayoutMode.HorizontalDays;
            _horizontalHost.Visible = horiz;

            if (horiz)
            {
                RebuildHorizontal(force: true);
                _horizontalHost.Height = HorizontalRowHeight; // фикс
            }

            UpdateCompactHeight();
            Invalidate();
            UpdateHorizontalHeader();
        }

        private void RebuildHorizontal(bool force)
        {
            if (LayoutMode != CalendarLayoutMode.HorizontalDays) return;
            BuildHorizontalForMonth(DateTime, force);
        }

        private void BuildHorizontalForMonth(DateTime anyDayInMonth, bool force = false)
        {
            EnsureHorizontalUi();

            var month = new DateTime(anyDayInMonth.Year, anyDayInMonth.Month, 1);

            if (!force && _lastBuiltMonth == month && _daysFlow.Controls.Count > 0)
            {
                HighlightSelectedDayButton(anyDayInMonth.Date);
                ScrollToSelected();
                return;
            }

            _lastBuiltMonth = month;

            _daysFlow.SuspendLayout();
            _daysFlow.Controls.Clear();

            int daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);

            for (int day = 1; day <= daysInMonth; day++)
            {
                var d = new DateTime(month.Year, month.Month, day);

                var btn = new SimpleButton
                {
                    Text = day.ToString(),
                    Width = 34,
                    Height = 34,
                    Tag = d,
                    Margin = new Padding(2),
                    ToolTip = d.ToString("dd.MM.yyyy"),
                    TabStop = false
                };

                ApplyDayButtonStyle(btn, d);

                btn.Click += (_, __) =>
                {
                    DateTime = d;
                    HighlightSelectedDayButton(d);
                    ScrollToSelected();
                };

                _daysFlow.Controls.Add(btn);
            }

            _daysFlow.ResumeLayout(true);

            HighlightSelectedDayButton(DateTime.Date);
            ScrollToSelected();

            // фикс высоты ряда, если свойство поменяли после конструктора
            _daysFlow.Height = HorizontalRowHeight;
            _horizontalHost.Height = HorizontalRowHeight;

            UpdateCompactHeight();
            ApplyHorizontalCompactHeight(); 
            UpdateHorizontalHeader();
        }

        private void ApplyDayButtonStyle(SimpleButton btn, DateTime date)
        {
            bool isOff = IsOffDay(date);
            bool isSpecial = SpecialDates.Contains(date.Date);
            bool isToday = date.Date == DateTime.Today;

            Color back = !NormalDayBackColor.IsEmpty ? NormalDayBackColor : BackColor;
            Color fore = ForeColor;

            if (isOff)
            {
                if (!OffDayBackColor.IsEmpty) back = OffDayBackColor;
                if (!OffDayForeColor.IsEmpty) fore = OffDayForeColor;
            }

            if (isSpecial)
            {
                if (!SpecialBackColor.IsEmpty) back = SpecialBackColor;
                if (!SpecialForeColor.IsEmpty) fore = SpecialForeColor;
            }

            btn.Appearance.BackColor = back;
            btn.Appearance.ForeColor = fore;
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseForeColor = true;

            // today — рамка
            if (isToday)
            {
                btn.Appearance.BorderColor = Color.DarkGreen;
                btn.Appearance.Options.UseBorderColor = true;
            }
        }

        private void HighlightSelectedDayButton(DateTime selectedDate)
        {
            foreach (Control c in _daysFlow.Controls)
            {
                if (c is not SimpleButton b) continue;

                var d = (DateTime)b.Tag;
                bool selected = d.Date == selectedDate.Date;

                // только визуальный акцент, без Bold (чтобы размер не прыгал)
                b.Appearance.Font = selected
                    ? new Font(Font, FontStyle.Underline)
                    : new Font(Font, FontStyle.Regular);
                b.Appearance.Options.UseFont = true;
            }
        }

        private void ScrollToSelected()
        {
            foreach (Control c in _daysFlow.Controls)
            {
                if (c is not SimpleButton b) continue;
                var d = (DateTime)b.Tag;

                if (d.Date == DateTime.Date)
                {
                    _horizontalHost.ScrollControlIntoView(b);
                    break;
                }
            }
        }

        /// <summary>
        /// Убираем "пустое место снизу" за счёт уменьшения Height контрола.
        /// Если контролу запрещено менять высоту (Dock=Fill, LayoutControl фиксирует) — визуально пустота останется.
        /// </summary>
        private void ApplyHorizontalCompactHeight()
        {
            if (LayoutMode != CalendarLayoutMode.HorizontalDays) return;
            if (!HorizontalCompactHeight) return;

            // Если Dock=Fill — контейнер сам задаёт высоту, мы не сможем "сжать".
            if (Dock == DockStyle.Fill) return;

            int bottom = BottomPanelVisible ? BottomPanelHeight + (BottomPanelResizable ? _splitter.Height : 0) : 0;
            int desired = HorizontalRowHeight + bottom + 4;

            // Не ломаем минимальный размер, просто выставим текущую высоту
            if (Height != desired)
                Height = desired;
        }

        #endregion

        #region LayoutControl height compact

        private LayoutControlItem _cachedLayoutItem;
        private int _cachedCompactHeight = -1;

        private void UpdateCompactHeight()
        {
            if (!HorizontalCompactHeight) return;

            if (LayoutMode == CalendarLayoutMode.HorizontalDays)
            {
                int desired = CalcDesiredHorizontalHeight();
                ApplyCompactHeightToParent(desired);
            }
            else
            {
                // Снимаем ограничения когда вернулись в обычный режим
                RemoveCompactHeightFromParent();
            }
        }

        private int CalcDesiredHorizontalHeight()
        {
            int h = HorizontalRowHeight;

            if (HorizontalHeaderVisible)
                h += HorizontalHeaderHeight;

            // нижняя панель
            if (BottomPanelVisible)
            {
                h += BottomPanelHeight;
                if (BottomPanelResizable)
                    h += _splitter?.Height ?? 0;
            }

            // небольшой запас на рамки/паддинги
            h += 6;

            return h;
        }

        private void ApplyCompactHeightToParent(int desiredHeight)
        {
            if (!AutoCompactInLayoutControl) return;

            // Если нас распирают Dock=Fill НЕ в LayoutControl — бессмысленно.
            // Но в LayoutControl мы всё равно можем зажать item.
            var item = GetParentLayoutItem();
            if (item == null) return;

            // Чтобы не дёргать лишний раз
            if (_cachedCompactHeight == desiredHeight && item.SizeConstraintsType == SizeConstraintsType.Custom)
                return;

            _cachedCompactHeight = desiredHeight;

            item.SizeConstraintsType = SizeConstraintsType.Custom;
            item.MinSize = new Size(0, desiredHeight);
            item.MaxSize = new Size(0, desiredHeight);
            item.TextVisible = false;

            // Принудительная перестройка лэйаута
            ForceParentLayout();
        }

        private void RemoveCompactHeightFromParent()
        {
            var item = GetParentLayoutItem();
            if (item == null) return;

            // Снимаем ограничения только если мы их ставили
            if (item.SizeConstraintsType != SizeConstraintsType.Custom) return;

            item.SizeConstraintsType = SizeConstraintsType.Default;
            item.MinSize = Size.Empty;
            item.MaxSize = Size.Empty;

            _cachedCompactHeight = -1;
            ForceParentLayout();
        }

        private LayoutControlItem GetParentLayoutItem()
        {
            if (_cachedLayoutItem != null && _cachedLayoutItem.Control == this)
                return _cachedLayoutItem;

            // Ищем LayoutControl выше по дереву родителей
            Control p = Parent;
            while (p != null && p is not LayoutControl)
                p = p.Parent;

            if (p is not LayoutControl lc)
                return null;

            var item = lc.GetItemByControl(this) as LayoutControlItem;
            _cachedLayoutItem = item;
            return item;
        }
        private void ForceParentLayout()
        {
            // если мы в LayoutControl — обновим его
            Control p = Parent;
            while (p != null && p is not LayoutControl)
                p = p.Parent;

            if (p is LayoutControl lc)
            {
                lc.PerformLayout();
                lc.Invalidate();
                lc.Update();
                return;
            }

            // запасной вариант
            Parent?.PerformLayout();
            Parent?.Invalidate();
        }

        #endregion

        #region месяц год
        private PanelControl _horizontalHeader;
        private SimpleButton _btnPrevMonth;
        private SimpleButton _btnNextMonth;
        private LabelControl _lblMonthYear;
        [DefaultValue(true)]
        public bool HorizontalHeaderVisible { get; set; } = true;

        [DefaultValue(true)]
        public bool HorizontalNavigationVisible { get; set; } = true;

        [DefaultValue(true)]
        public bool HorizontalShowMonth { get; set; } = true;

        [DefaultValue(true)]
        public bool HorizontalShowYear { get; set; } = true;

        [DefaultValue(30)]
        public int HorizontalHeaderHeight { get; set; } = 30;

        private void ChangeMonth(int deltaMonths)
        {
            var cur = DateTime;
            var newMonth = new DateTime(cur.Year, cur.Month, 1).AddMonths(deltaMonths);

            // если текущий день больше дней в новом месяце — подрезаем
            int day = Math.Min(cur.Day, DateTime.DaysInMonth(newMonth.Year, newMonth.Month));
            DateTime = new DateTime(newMonth.Year, newMonth.Month, day);

            BuildHorizontalForMonth(DateTime, force: true);
        }

        private void UpdateHorizontalHeader()
        {
            if (_horizontalHeader == null) return;

            _horizontalHeader.Height = HorizontalHeaderHeight;

            bool showHeader = LayoutMode == CalendarLayoutMode.HorizontalDays && HorizontalHeaderVisible;
            _horizontalHeader.Visible = showHeader;

            if (!showHeader) return;

            _btnPrevMonth.Visible = HorizontalNavigationVisible;
            _btnNextMonth.Visible = HorizontalNavigationVisible;

            string month = HorizontalShowMonth ? DateTime.ToString("MMMM") : "";
            string year = HorizontalShowYear ? DateTime.ToString("yyyy") : "";

            string text = (month + " " + year).Trim();
            if (string.IsNullOrWhiteSpace(text))
                text = DateTime.ToString("MM.yyyy");

            _lblMonthYear.Text = text;
        }

        #endregion
        private void ApplyCustomSettings()
        {
            // bottom
            BottomPanelVisible = CustomSettings.BottomPanelVisible;
            BottomPanelHeight = CustomSettings.BottomPanelHeight;
            BottomPanelResizable = CustomSettings.BottomPanelResizable;

            // theme/colors
            UseThemeByDefault = CustomSettings.UseThemeByDefault;
            NormalDayBackColor = CustomSettings.NormalDayBackColor;
            OffDayBackColor = CustomSettings.OffDayBackColor;
            OffDayForeColor = CustomSettings.OffDayForeColor;
            SpecialBackColor = CustomSettings.SpecialBackColor;
            SpecialForeColor = CustomSettings.SpecialForeColor;

            // zoom
            EnableZoomByCtrlWheel = CustomSettings.EnableZoomByCtrlWheel;
            ZoomLevel = CustomSettings.ZoomLevel;

            // schedule
            WeekendMode = CustomSettings.WeekendMode;
            WorkDaysInCycle = CustomSettings.WorkDaysInCycle;
            OffDaysInCycle = CustomSettings.OffDaysInCycle;
            CycleStartDate = CustomSettings.CycleStartDate;

            // layout
            AutoCompactInLayoutControl = CustomSettings.AutoCompactInLayoutControl;
            HorizontalCompactHeight = CustomSettings.HorizontalCompactHeight;
            HorizontalHeaderVisible = CustomSettings.HorizontalHeaderVisible;
            HorizontalNavigationVisible = CustomSettings.HorizontalNavigationVisible;
            HorizontalShowMonth = CustomSettings.HorizontalShowMonth;
            HorizontalShowYear = CustomSettings.HorizontalShowYear;
            HorizontalHeaderHeight = CustomSettings.HorizontalHeaderHeight;
            HorizontalRowHeight = CustomSettings.HorizontalRowHeight;

            LayoutMode = CustomSettings.LayoutMode; // ставим в конце (перестроит UI)

            Invalidate();
        }

    }
}
