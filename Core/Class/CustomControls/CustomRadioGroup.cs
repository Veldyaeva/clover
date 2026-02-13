using System.ComponentModel;
using DevExpress.XtraEditors;
using SewingProduction.Features.UserDistribution.Helpers;

namespace SewingProduction.Core.Class
{
    public class CustomRadioGroup : RadioGroup, IThemeable, IThemeableControl
    {
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        public CustomRadioGroup()
        {
            ApplyTheme();
        }
        public void ApplyTheme()
        {
            ForeColor = System.Drawing.SystemColors.ControlText;
            BackColor = System.Drawing.SystemColors.Control;
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

}
