using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SewingProduction.Features.UserDistribution.Helpers;

namespace SewingProduction.Core.Class
{
    public class CustomSpinEdit : SpinEdit, IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }

        private bool _visiblePermission = true;
        private bool _visibleLogic = true;

        // ===== Новые свойства ошибки =====
        private bool _showErrorIcon;
        private string _errorMessage;
        private Color _errorColor = Color.Red;
        private ErrorIconShape _errorShape = ErrorIconShape.CircleExclamation;

        /// <summary>Показывать ли значок ошибки справа внутри поля.</summary>
        [Browsable(true), DefaultValue(false), Category("Behavior")]
        public bool ShowErrorIcon
        {
            get => _showErrorIcon;
            set { _showErrorIcon = value; UpdateRightMargin(); Invalidate(); }
        }

        /// <summary>Текст подсказки при наведении на значок ошибки.</summary>
        [Browsable(true), DefaultValue(""), Category("Behavior")]
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; }
        }

        /// <summary>Цвет значка ошибки.</summary>
        [Browsable(true), Category("Appearance")]
        public Color ErrorColor
        {
            get => _errorColor;
            set { _errorColor = value; Invalidate(); }
        }

        /// <summary>Форма значка: кружок или треугольник с "!".</summary>
        [Browsable(true), DefaultValue(ErrorIconShape.CircleExclamation), Category("Appearance")]
        public ErrorIconShape ErrorShape
        {
            get => _errorShape;
            set { _errorShape = value; Invalidate(); }
        }

        // Подсказка при наведении
        private readonly ToolTip _toolTip = new ToolTip { ShowAlways = true };
        private bool _tooltipShown;

        public CustomSpinEdit()
        {
            ApplyTheme();

            // Режим: однострочный чаще всего — там EM_SETMARGINS работает.
            // Если вам нужно multiline — значок нарисуется, но правое поле для текста не изменится (ограничение Win32).
            //this.BorderStyle = BorderStyle.FixedSingle;

            // Убираем мерцание при частых перерисовках значка
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            // Следим за размерами
            this.SizeChanged += (_, __) => UpdateRightMargin();
            this.FontChanged += (_, __) => UpdateRightMargin();
        }

        // ===== Тема/права — как у вас =====
        public void ApplyTheme()
        {
            // Цвета не форсируем: берем из Designer/скина
            Font = ThemeManager.SharedSettings.DefaultFont;
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        public void ApplyPermission(UserClass user) => PermissionHelper.ApplyTo(this, ObjectName, user);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisiblePermission
        {
            get => _visiblePermission;
            set { _visiblePermission = value; VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisibleLogic
        {
            get => _visibleLogic;
            set { _visibleLogic = value; VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic); }
        }

        public new bool Visible
        {
            get => base.Visible;
            set { _visibleLogic = value; VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic); }
        }

        // ===== Нативка для правого поля =====
        private const int WM_PAINT = 0x000F;
        private const int WM_SETFONT = 0x0030;
        private const int WM_SIZE = 0x0005;

        private const int EM_SETMARGINS = 0x00D3;
        private const int EC_LEFTMARGIN = 0x0001;
        private const int EC_RIGHTMARGIN = 0x0002;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            UpdateRightMargin();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m); // Сначала рисует сам Edit-контрол

            if (m.Msg == WM_SIZE || m.Msg == WM_SETFONT)
                UpdateRightMargin();

            if (m.Msg == WM_PAINT && _showErrorIcon)
            {
                using (var g = Graphics.FromHwnd(this.Handle))
                    DrawErrorIcon(g);
            }
        }

        // Рассчёт прямоугольника значка справа
        private Rectangle GetErrorIconRect()
        {
            int h = ClientRectangle.Height;
            int size = Math.Min(Math.Max(14, h - 6), 18); // 14..18, чтобы адекватно смотреться при разной высоте
            int x = ClientRectangle.Right - size - 2;
            int y = (h - size) / 2;
            return new Rectangle(x, y, size, size);
        }

        private void DrawErrorIcon(Graphics g)
        {
            var r = GetErrorIconRect();
            using var b = new SolidBrush(_errorColor);
            using var pen = new Pen(Color.White, Math.Max(2f, r.Height / 9f));

            if (_errorShape == ErrorIconShape.CircleExclamation)
            {
                // Круг
                using var border = new Pen(ControlPaint.Dark(_errorColor), 1f);
                g.FillEllipse(b, r);
                g.DrawEllipse(border, r);
                // "!"
                int cx = r.Left + r.Width / 2;
                int top = r.Top + (int)(r.Height * 0.25);
                int bot = r.Bottom - (int)(r.Height * 0.25);
                g.DrawLine(pen, cx, top, cx, bot - (int)(r.Height * 0.20));
                g.FillEllipse(Brushes.White,
                    cx - Math.Max(1, r.Width / 12),
                    bot - (int)(r.Height * 0.12),
                    Math.Max(3, r.Width / 6),
                    Math.Max(3, r.Width / 6));
            }
            else // Triangle
            {
                // Равносторонний треугольник
                var p1 = new Point(r.Left + r.Width / 2, r.Top);
                var p2 = new Point(r.Left, r.Bottom);
                var p3 = new Point(r.Right, r.Bottom);
                using var border = new Pen(ControlPaint.Dark(_errorColor), 1f);
                g.FillPolygon(b, new[] { p1, p2, p3 });
                g.DrawPolygon(border, new[] { p1, p2, p3 });

                // "!"
                int cx = r.Left + r.Width / 2;
                int top = r.Top + (int)(r.Height * 0.28);
                int bot = r.Bottom - (int)(r.Height * 0.22);
                g.DrawLine(pen, cx, top, cx, bot - (int)(r.Height * 0.18));
                g.FillEllipse(Brushes.White,
                    cx - Math.Max(1, r.Width / 12),
                    bot - (int)(r.Height * 0.12),
                    Math.Max(3, r.Width / 6),
                    Math.Max(3, r.Width / 6));
            }
        }

        // Создаем правый «защитный» отступ, чтобы текст не налезал на значок
        private void UpdateRightMargin()
        {
            if (!IsHandleCreated) return;

            int margin = _showErrorIcon ? Math.Max(18, ClientSize.Height) : 0; // пикселей справа
            // wParam: какие поля меняем, lParam: LOWORD=левое, HIWORD=правое (в пикселях)
            int left = 0;
            int right = margin;
            int lParam = (right << 16) | (left & 0xFFFF);
            SendMessage(this.Handle, EM_SETMARGINS, (IntPtr)(EC_RIGHTMARGIN | EC_LEFTMARGIN), (IntPtr)lParam);
            Invalidate(); // перерисовать для надежности
        }

        // Подсказка по наведению на значок
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_showErrorIcon && !string.IsNullOrEmpty(_errorMessage))
            {
                if (GetErrorIconRect().Contains(e.Location))
                {
                    if (!_tooltipShown)
                    {
                        _toolTip.Show(_errorMessage, this, Width - 10, Height, 3000);
                        _tooltipShown = true;
                    }
                }
                else if (_tooltipShown)
                {
                    _toolTip.Hide(this);
                    _tooltipShown = false;
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (_tooltipShown) { _toolTip.Hide(this); _tooltipShown = false; }
        }

        public enum ErrorIconShape
        {
            CircleExclamation,
            Triangle
        }
    }
}
