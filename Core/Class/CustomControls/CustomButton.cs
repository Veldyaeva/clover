using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using SewingProduction;
using SewingProduction.Features.UserDistribution.Helpers;

namespace SewingProduction.Core.Class
{
    public class CustomButton : Button, IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
        private UserClass _lastUser;
        /// <summary>
        ///Контролирует видимость элемента на основе прав пользователя в системе
        /// </summary>
        private bool _visiblePermission = true;
        /// <summary>
        ///Контролирует видимость элемента на основе бизнес-логики приложения
        /// </summary>
        private bool _visibleLogic = true;

        public CustomButton()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Здесь уже точно Name, Parent, Visible и всё остальное готово
            if (!DesignMode && _lastUser != null)
            {
                // Отложенный вызов ApplyPermission, чтобы точно ничего не перезаписалось
                BeginInvoke(new MethodInvoker(() => ApplyPermission(_lastUser)));
            }
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
            _lastUser = user;

            if (!this.IsHandleCreated)
            {
                this.HandleCreated += (_, _) => ApplyPermission(user);
                return;
            }

            if (string.IsNullOrEmpty(ObjectName))
                ObjectName = this.Name;

            if (string.IsNullOrEmpty(ObjectName))
            {
                this.VisiblePermission = false;
                return;
            }

            bool hasWrite = user.HasPermission(ObjectName, "Редактор");
            bool hasRead = user.HasPermission(ObjectName, "Просмотр");

            this.VisiblePermission = hasRead || hasWrite;
            this.Enabled = hasWrite;

            Debug.WriteLine($"[Доступ Button] {ObjectName}: Просмотр={hasRead}, Редактор={hasWrite}, VisiblePermission={this.VisiblePermission}, Enabled={this.Enabled}");
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DisplayName("VisiblePermission")]
        [Description("Определяет видимость элемента на основе прав пользователя")]
        public bool VisiblePermission
        {
            get => _visiblePermission;
            set
            {
                _visiblePermission = value;
                UpdateVisibility();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("Видимость")]
        [DisplayName("VisibleLogic")]
        [Description("Контролирует видимость элемента на основе бизнес-логики приложения")]
        public bool VisibleLogic
        {
            get => _visibleLogic;
            set
            {
                _visibleLogic = value;
                UpdateVisibility();
            }
        }

        private void UpdateVisibility()
        {
            //Применяем и к контролу, и к лейауту. DevExpress LayoutControl
            this.ApplyVisibility(_visiblePermission, _visibleLogic);
        }

        public new bool Visible
        {
            get => base.Visible;
            set
            {
                _visibleLogic = value;
                UpdateVisibility();
            }
        }
    }
    public class CustomSimpleButton : SimpleButton, IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;

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

            if (!this.IsHandleCreated)
            {
                this.HandleCreated += (_, _) => ApplyPermission(user);
                return;
            }
            // Гарантия, что ObjectName задан
            if (string.IsNullOrEmpty(ObjectName))
                ObjectName = this.Name;

            if (string.IsNullOrEmpty(ObjectName))
            {
                this.Visible = false;
                return;
            }

            bool hasWrite = user.HasPermission(ObjectName, "Редактор");
            bool hasRead = user.HasPermission(ObjectName, "Просмотр");

            this.Visible = hasRead || hasWrite;
            this.Enabled = hasWrite;

            Debug.WriteLine($"[Доступ SimpleButton] {ObjectName}: Просмотр={hasRead}, Редактор={hasWrite}, Visible={this.Visible}, Enabled={this.Enabled}");
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DisplayName("VisiblePermission")]
        [Description("Определяет видимость элемента на основе прав пользователя")]
        public bool VisiblePermission
        {
            get => _visiblePermission;
            set
            {
                _visiblePermission = value;
                UpdateVisibility();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DisplayName("VisibleLogic")]
        [Description("Контролирует видимость кнопки на основе бизнес-логики приложения")]
        public bool VisibleLogic
        {
            get => _visibleLogic;
            set
            {
                _visibleLogic = value;
                UpdateVisibility();
            }
        }

        private void UpdateVisibility()
        {
            base.Visible = _visiblePermission && _visibleLogic;
        }

        public new bool Visible
        {
            get => base.Visible;
            set
            {
                _visibleLogic = value;
                UpdateVisibility();
            }
        }
    }
    /// <summary>
    /// Кнопка с записью в бд
    /// </summary>
    public class CustomActionButton : CustomButton
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
    public class CustomOkButton : CustomButton
    {
        public CustomOkButton()
        {
            Text = "OK";
            DialogResult = DialogResult.OK;
        }
    }

    public class CustomCancelButton : CustomButton
    {
        public CustomCancelButton()
        {
            Text = "Cancel";
            DialogResult = DialogResult.Cancel;
        }
    }
    public static class ControlExtensions
    {
        public static void ApplyVisibility(this Control control, bool visiblePermission, bool visibleLogic)
        {
            bool finalVisible = visiblePermission && visibleLogic;

            control.Visible = finalVisible;

            if (control.Parent is LayoutControl layout)
            {
                var item = layout.GetItemByControl(control);
                if (item != null)
                    item.Visibility = finalVisible
                        ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }
    }
}
