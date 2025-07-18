using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars.ViewInfo;
using System.Windows.Forms;
using SewingProduction.Features.UserDistribution.Helpers;
using static DevExpress.LookAndFeel.DXSkinColors;
using DevExpress.XtraEditors;
using System.Drawing;

namespace SewingProduction.Core.Class
{
    public class CustomButton : Button, IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
            // Гарантируем, что ObjectName есть
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

            Debug.WriteLine($"[Доступ Button] {ObjectName}: Просмотр={hasRead}, Редактор={hasWrite}, Visible={this.Visible}, Enabled={this.Enabled}");
        }
    }
    public class CustomSimpleButton : SimpleButton, IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }

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

}
