using DevExpress.XtraGrid;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static DevExpress.LookAndFeel.DXSkinColors;

namespace SewingProduction
{
    public interface IThemeable
    {
        void ApplyTheme();
    }

    public class CustomButton : Button, IThemeable
    {
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

    public class CustomTextBox : TextBox, IThemeable
    {
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
    }

    public class CustomCheckBox : CheckBox, IThemeable
    {
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
    }
    public class CustomMaskedTextBox : MaskedTextBox, IThemeable
    {
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
    }


    public class CustomComboBox : ComboBox, IThemeable
    {
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
    }

    public class CustomLabel : Label, IThemeable
    {
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
    }

    public class CustomGridControl : GridControl, IThemeable
    {
        public CustomGridControl()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        public void ApplyTheme()
        {
            BackColor = ThemeManager.ActiveTheme.GridBackground;
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

    /// <summary>
    /// Кастомная форма с градиентным фоном
    /// </summary>
    public class CustomForm : Form, IThemeable
    {
        public CustomForm()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        public void ApplyTheme()
        {
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect,
                ThemeManager.ActiveTheme.GradientStartColor,
                ThemeManager.ActiveTheme.GradientEndColor,
                LinearGradientMode.ForwardDiagonal))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
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
    }
}

