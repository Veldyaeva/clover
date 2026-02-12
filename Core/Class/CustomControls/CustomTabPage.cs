using System.ComponentModel;
using DevExpress.XtraTab;
using SewingProduction.Features.UserDistribution.Helpers;

namespace SewingProduction.Core.Class
{
    public class CustomTabPage : XtraTabPage//, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;

        public CustomTabPage()
        {
            //ApplyTheme();
            //ThemeManager.ThemeChanged += OnThemeChanged;
        }

        public void ApplyTheme()
        {
            BackColor = ThemeManager.ActiveTheme.TextBoxBackground;
            ForeColor = ThemeManager.ActiveTheme.LabelTextColor;
        }

        //private void OnThemeChanged() => ApplyTheme();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }

        public void ApplyPermission(UserClass user)
        {
            if (string.IsNullOrEmpty(ObjectName) && !string.IsNullOrEmpty(Name))
                ObjectName = Name;

            if (string.IsNullOrEmpty(ObjectName))
            {
                this.PageVisible = false;
                return;
            }

            bool hasWrite = user.HasPermission(ObjectName, "Редактор");
            bool hasRead = user.HasPermission(ObjectName, "Просмотр");

            // 🔥 Важное отличие от обычных контролов
            this.PageVisible = hasRead || hasWrite;

            // можно логировать при отладке:
            System.Diagnostics.Debug.WriteLine(
                $"[Доступ TabPage] {ObjectName}: Просмотр={hasRead}, Редактор={hasWrite}, PageVisible={this.PageVisible}"
            );
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
