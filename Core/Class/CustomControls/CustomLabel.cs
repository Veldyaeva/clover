using DevExpress.Utils;
using DevExpress.XtraEditors;
using SewingProduction.Features.UserDistribution.Helpers;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace SewingProduction.Core.Class
{
    public class CustomLabel : DevExpress.XtraEditors.LabelControl//, IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }

        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        private float? _fontSizePermission; // размер шрифта из прав


        public CustomLabel()
        {
            //ApplyTheme();
            //ThemeManager.ThemeChanged += OnThemeChanged;
        }

        public void ApplyTheme()
        {
            //ForeColor = ThemeManager.ActiveTheme.LabelTextColor;
            //Font = ThemeManager.SharedSettings.DefaultFont;
            //ApplyFontSizePermission(); // перекрыть размер, если задан

            // Если вы включили DevExpress skin — лучше отпустить цвета
            // и не красить вручную
            //if (ThemeManager.UseDevExpressSkin) // сделайте флаг в ThemeManager
            //{
                ResetToSkin();
            //    return;
            //}

            //// ваш текущий кастомный режим
            //Appearance.ForeColor = ThemeManager.ActiveTheme.LabelTextColor;
            //Appearance.Options.UseForeColor = true;

            //Appearance.Font = ThemeManager.SharedSettings.DefaultFont;
            //Appearance.Options.UseFont = true;

            //ApplyFontSizePermission();
        }
        private void ResetToSkin()
        {
            // Отпускаем управление скину
            Appearance.BackColor = Color.Empty;
            Appearance.ForeColor = Color.Empty;

            Appearance.Options.UseBackColor = false;
            Appearance.Options.UseForeColor = false;

            // шрифт можно оставить вашим (если хотите единый),
            // но если он тоже "ломает" — отпустите:
            // Appearance.Font = null;
            // Appearance.Options.UseFont = false;

            LookAndFeel.UseDefaultLookAndFeel = true;
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
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
        public ContentAlignment TextAlign
        {
            get
            {
                // можно вернуть фиксированное значение или не реализовывать
                return ContentAlignment.MiddleLeft;
            }
            set
            {
                Appearance.Options.UseTextOptions = true;

                switch (value)
                {
                    case ContentAlignment.MiddleCenter:
                        Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                        Appearance.TextOptions.VAlignment = VertAlignment.Center;
                        break;

                    case ContentAlignment.MiddleRight:
                        Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                        Appearance.TextOptions.VAlignment = VertAlignment.Center;
                        break;

                    case ContentAlignment.MiddleLeft:
                        Appearance.TextOptions.HAlignment = HorzAlignment.Near;
                        Appearance.TextOptions.VAlignment = VertAlignment.Center;
                        break;
                    case ContentAlignment.TopCenter:
                        Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                        Appearance.TextOptions.VAlignment = VertAlignment.Top;
                        break;

                    case ContentAlignment.TopRight:
                        Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                        Appearance.TextOptions.VAlignment = VertAlignment.Top;
                        break;

                    case ContentAlignment.TopLeft:
                        Appearance.TextOptions.HAlignment = HorzAlignment.Near;
                        Appearance.TextOptions.VAlignment = VertAlignment.Top;
                        break;
                    case ContentAlignment.BottomCenter:
                        Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                        Appearance.TextOptions.VAlignment = VertAlignment.Bottom;
                        break;

                    case ContentAlignment.BottomRight:
                        Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                        Appearance.TextOptions.VAlignment = VertAlignment.Bottom;
                        break;

                    case ContentAlignment.BottomLeft:
                        Appearance.TextOptions.HAlignment = HorzAlignment.Near;
                        Appearance.TextOptions.VAlignment = VertAlignment.Bottom;
                        break;
                        // при необходимости добавьте остальные варианты
                }
            }
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
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
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

        /// <summary>
        /// Размер шрифта, заданный правами (главнее обычного Font.Size)
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float? FontSizePermission
        {
            get => _fontSizePermission;
            set
            {
                _fontSizePermission = value;
                ApplyFontSizePermission();
            }
        }

        /// <summary>
        /// Применяет FontSizePermission, если он задан
        /// </summary>
        private void ApplyFontSizePermission()
        {
            if (_fontSizePermission.HasValue)
            {
                base.Font = new Font(Font.FontFamily, _fontSizePermission.Value, Font.Style);
            }
        }

        /// <summary>
        /// Переопределяем установку шрифта, чтобы при смене темы
        /// сохранялся приоритет FontSizePermission
        /// </summary>
        public override Font Font
        {
            get => base.Font;
            set
            {
                if (_fontSizePermission.HasValue && value.Size != _fontSizePermission.Value)
                {
                    base.Font = new Font(value.FontFamily, _fontSizePermission.Value, value.Style);
                }
                else
                {
                    base.Font = value;
                }
            }
        }
    }
    public class CustomHeaderLabel : CustomLabel
    {
        private const int HeaderFontOffset = 7;

        public CustomHeaderLabel()
        {
            ApplyHeaderStyle();
            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }

        private void OnThemeChanged()
        {
            ApplyHeaderStyle();
        }

        private void ApplyHeaderStyle()
        {
            float baseSize = FontSizePermission ?? ThemeManager.SharedSettings.DefaultFont.Size;
            var baseFamily = ThemeManager.SharedSettings.DefaultFont.FontFamily;
            base.Font = new Font(baseFamily, baseSize + HeaderFontOffset, FontStyle.Bold);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Font Font
        {
            get => base.Font;
            set
            {
                float size = FontSizePermission ?? value.Size;
                var baseFamily = value.FontFamily;
                base.Font = new Font(baseFamily, size + HeaderFontOffset, FontStyle.Bold);
            }
        }

        public new float? FontSizePermission
        {
            get => base.FontSizePermission;
            set
            {
                base.FontSizePermission = value;
                ApplyHeaderStyle();
            }
        }
    }

}