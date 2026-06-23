using System.ComponentModel;
using DevExpress.XtraEditors;
using SewingProduction.Features.UserDistribution.Helpers;

namespace SewingProduction.Core.Class
{
    public class CustomGridLookUpEdit : GridLookUpEdit, IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }

        private bool _visiblePermission = true;
        private bool _visibleLogic = true;

        public CustomGridLookUpEdit()
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
            if (string.IsNullOrEmpty(ObjectName))
                ObjectName = this.Name;

            bool hasWrite = user.HasPermission(ObjectName, "Редактор");
            bool hasRead = user.HasPermission(ObjectName, "Просмотр");

            VisiblePermission = hasRead || hasWrite;
            this.Enabled = hasWrite;

            System.Diagnostics.Debug.WriteLine(
                $"[Доступ GridLookUpEdit] {ObjectName}: Просмотр={hasRead}, Редактор={hasWrite}");
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
