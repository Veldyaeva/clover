using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraLayout;
using SewingProduction.Features.UserDistribution.Helpers;

namespace SewingProduction.Core.Class
{
    public class CustomLayoutControl : LayoutControl, IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }

        private bool _visiblePermission = true;
        private bool _visibleLogic = true;

        public CustomLayoutControl()
        {
            if (IsDesignMode())
                return;

            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        public void ApplyTheme()
        {
            var theme = ThemeManager.ActiveTheme;
            if (theme == null)
                return;

            BackColor = theme.TextBoxBackground;
            ForeColor = theme.LabelTextColor;
            Font = ThemeManager.SharedSettings.DefaultFont;
        }

        private void OnThemeChanged() => ApplyTheme();

        protected override void Dispose(bool disposing)
        {
            if (disposing && !IsDesignMode())
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }

        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
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
        [Description("Контролирует видимость на основе бизнес-логики приложения")]
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

        private static bool IsDesignMode()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime;
        }
    }
}
