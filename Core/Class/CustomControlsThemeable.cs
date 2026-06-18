
using SewingProduction.Features.UserDistribution.Class;
using SewingProduction.Core.Extensions;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SewingProduction
{
    public interface IThemeable
    {
        void ApplyTheme();
    }
    public interface IThemeableControl
    {
        string ObjectName { get; set; }
        bool VisiblePermission { get; set; }
        bool VisibleLogic { get; set; }
        void ApplyPermission(UserClass user);
    }

    /// <summary>
    /// Формы реализуют этот интерфейс, если хотят централизованно описывать права для header-button.
    /// </summary>
    public interface IHeaderButtonPermissionHost
    {
        IEnumerable<HeaderButtonPermissionBinding> GetHeaderButtonPermissionBindings();
    }

    public class CustomCheckBox : CheckBox, IThemeable, IThemeableControl
    {
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
        public CustomCheckBox()
        {
            ApplyTheme();
        }

        public void ApplyTheme()
        {
            // Цвета не трогаем, только общий шрифт
            Font = ThemeManager.SharedSettings.DefaultFont;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisiblePermission
        {
            get => _visiblePermission;
            set
            {
                _visiblePermission = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisibleLogic
        {
            get => _visibleLogic;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        public new bool Visible
        {
            get => base.Visible;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }
    }

    public class CustomRadioButton : RadioButton, IThemeable, IThemeableControl
    {
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        public CustomRadioButton()
        {
            ApplyTheme();
        }
        public void ApplyTheme()
        {
            Font = ThemeManager.SharedSettings.DefaultFont;
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisiblePermission
        {
            get => _visiblePermission;
            set
            {
                _visiblePermission = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisibleLogic
        {
            get => _visibleLogic;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        public new bool Visible
        {
            get => base.Visible;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }
    }
    public class CustomDateTimePicker : DateTimePicker, IThemeable, IThemeableControl
    {
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        public CustomDateTimePicker()
        {
            ApplyTheme();
        }
        public void ApplyTheme()
        {
            Font = ThemeManager.SharedSettings.DefaultFont;
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisiblePermission
        {
            get => _visiblePermission;
            set
            {
                _visiblePermission = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisibleLogic
        {
            get => _visibleLogic;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        public new bool Visible
        {
            get => base.Visible;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }
    }
    public class CustomNumericUpDown : NumericUpDown, IThemeable, IThemeableControl
    {
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        public CustomNumericUpDown()
        {
            ApplyTheme();
        }
        public void ApplyTheme()
        {
            Font = ThemeManager.SharedSettings.DefaultFont;
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisiblePermission
        {
            get => _visiblePermission;
            set
            {
                _visiblePermission = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisibleLogic
        {
            get => _visibleLogic;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        public new bool Visible
        {
            get => base.Visible;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }
    }
    
    public class CustomCheckedListBox : CheckedListBox, IThemeable, IThemeableControl
    {
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        public CustomCheckedListBox()
        {
            ApplyTheme();
        }
        public void ApplyTheme()
        {
            // Цвета не форсируем: берем из Designer/скина
            Font = ThemeManager.SharedSettings.DefaultFont;
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisiblePermission
        {
            get => _visiblePermission;
            set
            {
                _visiblePermission = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisibleLogic
        {
            get => _visibleLogic;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        public new bool Visible
        {
            get => base.Visible;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }
    }
    public class CustomTextBoxEx : DevExpress.XtraEditors.TextEdit, IThemeable, IThemeableControl
    {
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        public CustomTextBoxEx()
        {
            ApplyTheme();
        }
        public void ApplyTheme()
        {
            // Цвета не форсируем: берем из Designer/скина
            Font = ThemeManager.SharedSettings.DefaultFont;
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisiblePermission
        {
            get => _visiblePermission;
            set
            {
                _visiblePermission = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisibleLogic
        {
            get => _visibleLogic;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        public new bool Visible
        {
            get => base.Visible;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }
    }

    public class CustomMaskedTextBox : MaskedTextBox, IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        public CustomMaskedTextBox()
        {
            ApplyTheme();
        }

        public void ApplyTheme()
        {
            // Цвета не форсируем: берем из Designer/скина
            Font = ThemeManager.SharedSettings.DefaultFont;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisiblePermission
        {
            get => _visiblePermission;
            set
            {
                _visiblePermission = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisibleLogic
        {
            get => _visibleLogic;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        public new bool Visible
        {
            get => base.Visible;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }
    }


    public class CustomComboBox : ComboBox, IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        public CustomComboBox()
        {
            ApplyTheme();
        }

        public void ApplyTheme()
        {
            // Цвета не форсируем: берем из Designer/скина
            Font = ThemeManager.SharedSettings.DefaultFont;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisiblePermission
        {
            get => _visiblePermission;
            set
            {
                _visiblePermission = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisibleLogic
        {
            get => _visibleLogic;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        public new bool Visible
        {
            get => base.Visible;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }
    }

    public class CustomTabControl : DevExpress.XtraTab.XtraTabControl, IThemeable, IThemeableControl
    {
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        public CustomTabControl()
        {
            ApplyTheme();
        }

        public void ApplyTheme()
        {
            // Цвета не форсируем: берем из Designer/скина
            Font = ThemeManager.SharedSettings.DefaultFont;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisiblePermission
        {
            get => _visiblePermission;
            set
            {
                _visiblePermission = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisibleLogic
        {
            get => _visibleLogic;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        public new bool Visible
        {
            get => base.Visible;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }
    }




    /// <summary>
    /// Кастомный прозрачный группбокс с черной обводкой
    /// </summary>
    public class CustomGroupBox : GroupBox, IThemeableControl//, IThemeable
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        private Color _borderColor = Color.Black; // Цвет обводки по умолчанию
        private int _borderThickness = 1;       // Толщина обводки по умолчанию

        // Свойство для установки цвета обводки
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        // Свойство для установки толщины обводки
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderThickness
        {
            get { return _borderThickness; }
            set { _borderThickness = value; Invalidate(); }
        }

        // Переопределение метода OnPaint для рисования обводки
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e); //рисуем всё что есть в дефолтном GroupBox
            // Рисуем границу
            using (Pen borderPen = new Pen(_borderColor, _borderThickness))
            {
                // Определение прямоугольника для обводки
                var rect = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - _borderThickness, ClientRectangle.Height - _borderThickness);
                rect.X += _borderThickness / 2;
                rect.Y += _borderThickness / 2;

                //e.Graphics.DrawRectangle(borderPen, rect);

                // Замеряем размер текста
                SizeF textSize = e.Graphics.MeasureString(Text, Font);
                int textBottomY = (int)textSize.Height;
                // рисуем границу
                e.Graphics.DrawLine(borderPen, rect.X, rect.Y + textBottomY, rect.X, rect.Y + rect.Height); //Левая полоса
                e.Graphics.DrawLine(borderPen, rect.X + rect.Width, rect.Y + textBottomY, rect.X + rect.Width, rect.Y + rect.Height); // Правая
                e.Graphics.DrawLine(borderPen, rect.X, rect.Y + textBottomY, rect.X + rect.Width, rect.Y + textBottomY); // Верхняя
                e.Graphics.DrawLine(borderPen, rect.X, rect.Y + rect.Height, rect.X + rect.Width, rect.Y + rect.Height); // Нижняя
                //e.Graphics.DrawLine(borderPen, rect.X + (int)rect.Width / 2 + (int)textSize.Width / 2 + 5, rect.Y + textBottomY, rect.X + rect.Width, rect.Y + textBottomY);

            }
        }
        public CustomGroupBox()
        {
            this.BackColor = Color.Transparent;
            ApplyTheme();
        }

        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }

        public void ApplyTheme()
        {
            // Цвета не форсируем: берем из Designer/скина
            this.Font = ThemeManager.SharedSettings.DefaultFont;
            Invalidate(); // Перерисовать контрол с новыми цветами
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisiblePermission
        {
            get => _visiblePermission;
            set
            {
                _visiblePermission = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisibleLogic
        {
            get => _visibleLogic;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        public new bool Visible
        {
            get => base.Visible;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }
    }
    /// <summary>
    /// Кастомная форма с градиентным фоном
    /// </summary>
    public class CustomForm : Form, SewingProduction.IThemeable
    {
        public int FormID;
        protected UserClass _user;
        public UserClass User => _user;
        public bool IsPreview { get; set; }
        private string _appliedFontSignature;
        private readonly LayoutControlGroupHelper _headerButtonPermissionHelper = new LayoutControlGroupHelper();
        private bool _runtimeInitialized;

        private static string GetFontSignature(Font font)
        {
            if (font == null)
                return string.Empty;

            return $"{font.FontFamily?.Name}|{font.SizeInPoints}|{font.Style}|{font.Unit}|{font.GdiCharSet}|{font.GdiVerticalFont}";
        }

        private void InitializeFontTracking()
        {
            _appliedFontSignature = GetFontSignature(ThemeManager.SharedSettings.DefaultFont);
            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        private void OnThemeChanged()
        {
            var currentSignature = GetFontSignature(ThemeManager.SharedSettings.DefaultFont);
            if (string.Equals(currentSignature, _appliedFontSignature, StringComparison.Ordinal))
                return;

            _appliedFontSignature = currentSignature;

            if (IsDisposed || !IsHandleCreated)
                return;

            if (InvokeRequired)
            {
                Invoke(new Action(() => ApplyThemeToChildren(this)));
            }
            else
            {
                ApplyThemeToChildren(this);
            }
        }

        private void ApplyThemeToChildren(Control parentControl)
        {
            foreach (Control childControl in parentControl.Controls)
            {
                if (childControl is IThemeable themeableChild)
                {
                    themeableChild.ApplyTheme();
                }
                if (childControl.HasChildren)
                {
                    ApplyThemeToChildren(childControl);
                }
            }
        }

        public CustomForm()
        {
            if (IsPreview)
            {
                return;
            }
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
            {
                _user = new UserClass();
                InitializeFontTracking();
                return;
            }

            if (CurrentUser.User != null)
            {
                InitializeRuntime(CurrentUser.User);
            }
            else
            {
                InitializeFontTracking();
            }
        }
        public CustomForm(UserClass user)
        {
            if (IsPreview)
            {
                return;
            }
            InitializeRuntime(user ?? throw new ArgumentNullException(nameof(user)));
        }
        private void InitializeRuntime(UserClass user)
        {
            if (_runtimeInitialized)
                return;

            _runtimeInitialized = true;
            _user = user;
            InitializeFontTracking();

            // Общий runtime-pipeline формы: логирование, grid-settings и права доступа.
            this.Load += async (s, e) =>
            {
                if (IsPreview) return;

                try
                {
                    await ActionLogger.Log(_user.UserId, "Открытие формы", NameForm: this.GetType().Name);
                }
                catch (Exception logEx)
                {
                    System.Diagnostics.Debug.WriteLine($"[CustomForm] ActionLogger.Log failed: {logEx.Message}");
                }

                InitializeAutoGridSettings();
                await CustomForm_LoadAsync(s, e);
            };

            // Сохраняем настройки при закрытии формы.
            this.FormClosing += (s, e) =>
            {
                if (IsPreview) return;
                this.SaveAllGridSettings();
            };

            this.FormClosed += async (s, e) =>
            {
                if (IsPreview) return;
                foreach (Control c in this.Controls)
                {
                    ClearBindings(c);
                }
            };
        }
        /// <summary>
        /// Отписка от всех биндингов в контроле и его дочерних контролах
        /// </summary>
        /// <param name="c"></param>
        private static void ClearBindings(Control c)
        {
            if (c == null) return;

            // 1) Снять биндинги у самого контрола
            c.DataBindings.Clear();

            // 2) Рекурсивно пройтись по дочерним
            foreach (Control child in c.Controls)
                ClearBindings(child);
        }
        public void ApplyTheme()
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Invalidate()));
            }
            else
            {
                Invalidate();
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e); // Call base.OnPaint first
            //Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            //using (LinearGradientBrush brush = new LinearGradientBrush(
            //    rect,
            //    ThemeManager.ActiveTheme.GradientStartColor, 
            //    ThemeManager.ActiveTheme.GradientEndColor,
            //    LinearGradientMode.ForwardDiagonal))
            //{
            //    e.Graphics.FillRectangle(brush, rect);
            //}
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Инициализирует автоматическое сохранение настроек для всех CustomGridControl на форме
        /// </summary>
        protected virtual void InitializeAutoGridSettings()
        {
            try
            {
                this.EnableAutoGridSettings(true);
            }
            catch (Exception ex)
            {
                // Логируем ошибку, но не прерываем работу формы
                System.Diagnostics.Debug.WriteLine($"Ошибка при инициализации автоматических настроек гридов: {ex.Message}");
            }
        }

        private async Task CustomForm_LoadAsync(object sender, EventArgs e)
        {
            if (IsPreview || _user == null)
                return;

            string formName = this.GetType().Name;

            await _user.LoadObjectForm(formName);

            if (!_user.HasPermission(formName, "Просмотр") && !_user.HasPermission(formName, "Редактор"))
            {
                MessageBox.Show(
                    $"У вас нет доступа к форме '{formName}'.",
                    "Доступ запрещён",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                this.BeginInvoke((MethodInvoker)(() => this.Close()));
                return;
            }

            // если только просмотр — отключаем все контролы
            if (_user.HasPermission(formName, "Просмотр") && !_user.HasPermission(formName, "Редактор"))
            {
                // Header-buttons не входят в дерево Control, поэтому применяем их права отдельно.
                ApplyHeaderButtonPermissions(_user);
                DisableAllControls(this);
                return;
            }

            // если редактор — применяем доступ к каждому элементу
            ApplyHeaderButtonPermissions(_user);
            ApplyPermissionsToControls(this, _user);
        }
        private void ApplyHeaderButtonPermissions(UserClass user)
        {
            if (user == null || this is not IHeaderButtonPermissionHost host)
                return;

            var bindings = host.GetHeaderButtonPermissionBindings()?
                .Where(binding =>
                    binding != null &&
                    binding.Group != null &&
                    !string.IsNullOrWhiteSpace(binding.ButtonTag) &&
                    !string.IsNullOrWhiteSpace(binding.PermissionObjectName))
                .ToList();

            if (bindings == null || bindings.Count == 0)
                return;

            // Сначала регистрируем все привязки, затем применяем права по каждой группе.
            foreach (var binding in bindings)
            {
                _headerButtonPermissionHelper.RegisterButtonPermission(
                    binding.Group,
                    binding.ButtonTag,
                    binding.PermissionObjectName);
            }

            foreach (var group in bindings.Select(binding => binding.Group).Distinct())
            {
                _headerButtonPermissionHelper.ApplyButtonPermissions(group, user);
            }
        }
        private void ApplyPermissionsToControls(Control parent, UserClass user)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is IThemeableControl themeable)
                {
                    if (string.IsNullOrEmpty(themeable.ObjectName) && !string.IsNullOrEmpty(ctrl.Name))
                        themeable.ObjectName = ctrl.Name;

                    string name = themeable.ObjectName ?? ctrl.Name;
                    bool hasWrite = user.HasPermission(name, "Редактор");
                    bool hasRead = user.HasPermission(name, "Просмотр");

                    Debug.WriteLine($"[Доступ Control] Объект: {name}, Просмотр: {hasRead}, Редактор: {hasWrite}");

                    if (!ctrl.GetType().Name.StartsWith("Custom"))
                        PermissionHelper.ApplyTo(ctrl, name, user);
                    else
                        themeable.ApplyPermission(user);
                }
                // Обработка табов — обязательно
                if (ctrl is DevExpress.XtraTab.XtraTabControl tabControl)
                {
                    foreach (DevExpress.XtraTab.XtraTabPage tabPage in tabControl.TabPages)
                    {
                        if (tabPage is IThemeableControl themeableTab)
                        {
                            if (string.IsNullOrEmpty(themeableTab.ObjectName) && !string.IsNullOrEmpty(tabPage.Name))
                                themeableTab.ObjectName = tabPage.Name;

                            string tabName = themeableTab.ObjectName ?? tabPage.Name;
                            bool hasWrite = user.HasPermission(tabName, "Редактор");
                            bool hasRead = user.HasPermission(tabName, "Просмотр");

                            Debug.WriteLine($"[Доступ TabPage] Вкладка: {tabName}, Просмотр: {hasRead}, Редактор: {hasWrite}");

                            themeableTab.ApplyPermission(user);
                        }

                        // 🔁 Обработка всех контролов внутри вкладки
                        ApplyPermissionsToControls(tabPage, user);
                    }
                }

                // 🔁 Рекурсивно обрабатываем все вложенные контролы
                if (ctrl.HasChildren)
                    ApplyPermissionsToControls(ctrl, user);
            }
        }

        private void DisableAllControls(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (!(ctrl is Label || ctrl is PictureBox))
                    ctrl.Enabled = false;

                if (ctrl.HasChildren)
                    DisableAllControls(ctrl);
            }
        }

    }
    public static class PermissionHelper
    {
        public static void ApplyTo(Control ctrl, string objectName, UserClass user)
        {
            if (string.IsNullOrEmpty(objectName))
                objectName = ctrl.Name;

            if (string.IsNullOrEmpty(objectName))
            {
                Debug.WriteLine("[PermissionHelper] objectName не найден");
                return;
            }

            bool hasWrite = user.HasPermission(objectName, "Редактор");
            bool hasRead = user.HasPermission(objectName, "Просмотр");

            Debug.WriteLine($"[PermissionHelper] {objectName}: Просмотр={hasRead}, Редактор={hasWrite}");

            ctrl.Enabled = hasWrite;
            ctrl.Visible = hasRead || hasWrite;
        }
    }
    public static class VisibilityHelper
    {
        public static void UpdateVisibility(Control ctrl, bool visiblePermission, bool visibleLogic)
        {
            ctrl.Visible = visiblePermission && visibleLogic;
        }
    }
}
