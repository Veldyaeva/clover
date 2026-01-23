using DevExpress.XtraBars.ViewInfo;
using SewingProduction.Features.UserDistribution.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DevExpress.LookAndFeel.DXSkinColors;

namespace SewingProduction.Core.Class
{
    internal class CastomLayoutControl : DevExpress.XtraLayout.LayoutControl, IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        public CastomLayoutControl()
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
}
