using DevExpress.Utils.Menu;
using DevExpress.XtraGrid;
using DevExpress.XtraReports.Native;
using SewingProduction.form.UserDistribution;
using SewingProduction.form.TeamWork.Interfaces;
using SewingProduction.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.LookAndFeel.DXSkinColors;
using SewingProduction;

namespace SewingProduction
{
    public interface IThemeable
    {
        void ApplyTheme();
    }
    public interface IThemeableControl
    {
        string ObjectName { get; set; }
        void ApplyPermission(UserClass user);
    }
    public class CustomButton : Button, IThemeable, IThemeableControl
    {
        public string ObjectName { get; set; }
        public CustomButton()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        public void ApplyTheme()
        {
            BackColor = ThemeManager.ActiveTheme.ButtonBackground;
            ForeColor = ThemeManager.ActiveTheme.ButtonTextColor;
            Font = ThemeManager.SharedSettings.DefaultFont;
            FlatStyle = FlatStyle.Standard;
            FlatAppearance.BorderSize = 1;
            Height = ThemeManager.SharedSettings.ButtonHeight;
        }

        private void OnThemeChanged() => ApplyTheme();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }
        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
    }
    public class CustomOkButton : CustomButton, IThemeableControl
    {
        public string ObjectName { get; set; }
        public CustomOkButton()
        {
            Text = "OK";
            DialogResult = DialogResult.OK;
        }
        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
    }

    public class CustomCancelButton : CustomButton, IThemeableControl
    {
        public string ObjectName { get; set; }
        public CustomCancelButton()
        {
            Text = "Cancel";
            DialogResult = DialogResult.Cancel;
        }
        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
    }

    public class CustomTextBox : TextBox, IThemeable, IThemeableControl
    {
        public string ObjectName { get; set; }
        public CustomTextBox()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        public void ApplyTheme()
        {
            BackColor = ThemeManager.ActiveTheme.TextBoxBackground;
            ForeColor = ThemeManager.ActiveTheme.TextBoxText;
            Font = ThemeManager.SharedSettings.DefaultFont;
        }

        private void OnThemeChanged() => ApplyTheme();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }
        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
    }

    public class CustomCheckBox : CheckBox, IThemeable, IThemeableControl
    {
        public string ObjectName { get; set; }
        public CustomCheckBox()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        public void ApplyTheme()
        {
            ForeColor = ThemeManager.ActiveTheme.TextBoxText;
            Font = ThemeManager.SharedSettings.DefaultFont;
        }

        private void OnThemeChanged() => ApplyTheme();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }
        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
    }


    public class CustomSimpleButton : DevExpress.XtraEditors.SimpleButton, IThemeable
    {
        public CustomSimpleButton()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        public void ApplyTheme()
        {

            Appearance.BackColor = ThemeManager.ActiveTheme.ButtonBackground;
            Appearance.ForeColor = ThemeManager.ActiveTheme.ButtonTextColor;
            Appearance.Font = ThemeManager.SharedSettings.DefaultFont;

            AppearanceDisabled.BackColor = Color.Green;
            AppearanceDisabled.ForeColor = Color.GreenYellow;
            AppearanceDisabled.Options.UseBackColor = true;
            AppearanceDisabled.Options.UseForeColor = true;
            //BackColor = ThemeManager.ActiveTheme.ButtonBackground;
            //ForeColor = ThemeManager.ActiveTheme.ButtonTextColor;
            //Font = ThemeManager.SharedSettings.DefaultFont;

            //FlatStyle = FlatStyle.Standard;
            //FlatAppearance.BorderSize = 1;
            Height = ThemeManager.SharedSettings.ButtonHeight;
        }

        private void OnThemeChanged() => ApplyTheme();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }
    }
    public class CustomRadioButton : RadioButton, IThemeable
    {
        public CustomRadioButton()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
        }
        public void ApplyTheme()
        {
            ForeColor = ThemeManager.ActiveTheme.TextBoxText;
            Font = ThemeManager.SharedSettings.DefaultFont;
        }
        private void OnThemeChanged() => ApplyTheme();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }
    }
    public class CustomDateTimePicker : DateTimePicker, IThemeable
    {
        public CustomDateTimePicker()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
        }
        public void ApplyTheme()
        {
            BackColor = ThemeManager.ActiveTheme.TextBoxBackground;
            ForeColor = ThemeManager.ActiveTheme.TextBoxText;
            Font = ThemeManager.SharedSettings.DefaultFont;
        }
        private void OnThemeChanged() => ApplyTheme();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }
    }
    public class CustomNumericUpDown : NumericUpDown, IThemeable
    {
        public CustomNumericUpDown()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
        }
        public void ApplyTheme()
        {
            BackColor = ThemeManager.ActiveTheme.TextBoxBackground;
            ForeColor = ThemeManager.ActiveTheme.TextBoxText;
            Font = ThemeManager.SharedSettings.DefaultFont;
        }
        private void OnThemeChanged() => ApplyTheme();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }
    }
    public class CustomListBox : ListBox, IThemeable
    {
        public CustomListBox()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
        }
        public void ApplyTheme()
        {
            BackColor = ThemeManager.ActiveTheme.TextBoxBackground;
            ForeColor = ThemeManager.ActiveTheme.TextBoxText;
            Font = ThemeManager.SharedSettings.DefaultFont;
        }
        private void OnThemeChanged() => ApplyTheme();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }
    }
    public class CustomCheckedListBox : CheckedListBox, IThemeable
    {
        public CustomCheckedListBox()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
        }
        public void ApplyTheme()
        {
            BackColor = ThemeManager.ActiveTheme.TextBoxBackground;
            ForeColor = ThemeManager.ActiveTheme.TextBoxText;
            Font = ThemeManager.SharedSettings.DefaultFont;
        }
        private void OnThemeChanged() => ApplyTheme();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }
    }
    public class CustomTextBoxEx : DevExpress.XtraEditors.TextEdit, IThemeable
    {
        public CustomTextBoxEx()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
        }
        public void ApplyTheme()
        {
            BackColor = ThemeManager.ActiveTheme.TextBoxBackground;
            ForeColor = ThemeManager.ActiveTheme.TextBoxText;
            Font = ThemeManager.SharedSettings.DefaultFont;
        }
        private void OnThemeChanged() => ApplyTheme();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }
    }

    public class CustomMaskedTextBox : MaskedTextBox, IThemeable, IThemeableControl
    {
        public string ObjectName { get; set; }
        public CustomMaskedTextBox()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        public void ApplyTheme()
        {
            BackColor = ThemeManager.ActiveTheme.TextBoxBackground;
            ForeColor = ThemeManager.ActiveTheme.TextBoxText;
            Font = ThemeManager.SharedSettings.DefaultFont;
        }

        private void OnThemeChanged() => ApplyTheme();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }
        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
    }


    public class CustomComboBox : ComboBox, IThemeable, IThemeableControl
    {
        public string ObjectName { get; set; }
        public CustomComboBox()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        public void ApplyTheme()
        {
            BackColor = ThemeManager.ActiveTheme.TextBoxBackground;
            ForeColor = ThemeManager.ActiveTheme.TextBoxText;
            Font = ThemeManager.SharedSettings.DefaultFont;
        }

        private void OnThemeChanged() => ApplyTheme();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }
        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
    }

    public class CustomLabel : Label, IThemeable, IThemeableControl
    {
        public string ObjectName { get; set; }
        public CustomLabel()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        public void ApplyTheme()
        {
            ForeColor = ThemeManager.ActiveTheme.LabelTextColor;
            Font = ThemeManager.SharedSettings.DefaultFont;
        }

        private void OnThemeChanged() => ApplyTheme();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }
        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
    }


    public class CustomGridControl : GridControl, SewingProduction.IThemeable, IThemeableControl
    {
        public Color? AlternateRowColor { get; set; }
        public string ObjectName { get; set; }
        public CustomGridControl()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
            ViewRegistered += OnViewRegistered;
        }

        public void ApplyTheme()
        {
            BackColor = ThemeManager.ActiveTheme.GridBackground;
            ForeColor = ThemeManager.ActiveTheme.TextBoxText;
            Font = ThemeManager.SharedSettings.DefaultFont;
            AlternateRowColor = ThemeManager.ActiveTheme.BandHighlightColor;

            foreach (var view in ViewCollection)
            {
                if (view is DevExpress.XtraGrid.Views.Grid.GridView gridView)
                    ApplyRowColors(gridView);
            }
        }
        private void OnViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            if (e.View is DevExpress.XtraGrid.Views.Grid.GridView gridView)
            {
                ApplyRowColors(gridView);
            }
        }
        private void ApplyRowColors(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            if (AlternateRowColor.HasValue)
            {
                gridView.Appearance.EvenRow.BackColor = AlternateRowColor.Value;
                gridView.OptionsView.EnableAppearanceEvenRow = true;
            }
        }
        private void OnThemeChanged() => ApplyTheme();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }
        public void ApplyPermission(UserClass user)
        {
            //PermissionHelper.ApplyTo(this, ObjectName, user);

            if (string.IsNullOrEmpty(ObjectName)) return;

            bool hasWrite = user.HasPermission(ObjectName, "Редактор");
            bool hasRead = user.HasPermission(ObjectName, "Просмотр");

            this.Visible = hasRead || hasWrite;
            //this.Enabled = hasWrite;
            foreach (var view in ViewCollection)
            {
                if (view is DevExpress.XtraGrid.Views.Grid.GridView gridView)
                {
                    gridView.OptionsBehavior.ReadOnly = !hasWrite;
                    gridView.OptionsBehavior.Editable = hasWrite;
                }
            }
        }
    }
    /// <summary>
    /// Кастомный прозрачный группбокс с черной обводкой
    /// </summary>
    public class CustomGroupBox : GroupBox, IThemeableControl, IThemeable
    {
        public string ObjectName { get; set; }
        private Color _borderColor = Color.Black; // Цвет обводки по умолчанию
        private int _borderThickness = 1;       // Толщина обводки по умолчанию

        // Свойство для установки цвета обводки
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        // Свойство для установки толщины обводки
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
            //this.ForeColor = Theme.TextBoxText;
            //this.Font = Theme.DefaultFont;
        }

        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }

        public void ApplyTheme()
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Кнопка с записью в бд
    /// </summary>
    public class CustomActionButton : CustomButton
    {
        public string EventDescription { get; set; } = "Нажатие кнопки";

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            _ = LogActionToDatabase(); // Fire-and-forget
        }

        private async Task LogActionToDatabase()
        {
            if (this.FindForm() is CustomForm form && form.User is UserClass user)
            {
                await ActionLogger.Log(user.UserId, "Нажатие на кнопку", NameForm: this.FindForm()?.Name, NameObject: this.Name);
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
        public CustomForm()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
        }
        public CustomForm(UserClass user)
        {
            // сохраняем пользователя
            _user = user ?? throw new ArgumentNullException(nameof(user));

            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;

            // подписка на загрузку формы
            this.Load += async (s, e) =>
            {
                await ActionLogger.Log(_user.UserId, "Открытие формы", NameForm: this.GetType().Name);
                CustomForm_Load(s, e);
            };
        }
        public void ApplyTheme()
        {
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
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

        private void OnThemeChanged() => ApplyTheme();
        private async void CustomForm_Load(object sender, EventArgs e)
        {
            string formName = this.GetType().Name;

            await _user.LoadObjectForm(formName);

            // нет вообще доступа — закрываем
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
                DisableAllControls(this);
                return;
            }

            // если редактор — применяем доступ к каждому элементу
            ApplyPermissionsToControls(this, _user);
        }
        private void ApplyPermissionsToControls(Control parent, UserClass user)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is IThemeableControl themeable)
                {
                    // Если ObjectName не задан вручную — ставим по имени контрола
                    if (string.IsNullOrEmpty(themeable.ObjectName) && !string.IsNullOrEmpty(ctrl.Name))
                    {
                        themeable.ObjectName = ctrl.Name;
                    }

                    themeable.ApplyPermission(user);
                }

                if (ctrl.HasChildren)
                {
                    ApplyPermissionsToControls(ctrl, user);
                }
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
            if (string.IsNullOrEmpty(objectName)) return;

            bool hasWrite = user.HasPermission(objectName, "Редактор");
            bool hasRead = user.HasPermission(objectName, "Просмотр");

            ctrl.Visible = hasRead || hasWrite;
            ctrl.Enabled = hasWrite;
        }
    }
}
