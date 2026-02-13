using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SewingProduction.Features.UserDistribution.Helpers;


namespace SewingProduction.Core.Class
{
    public class CustomLabel : Label, IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }

        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        private float? _fontSizePermission; // размер шрифта из прав


        public CustomLabel()
        {
            ApplyTheme();
        }

        public void ApplyTheme()
        {
            // Цвета не форсируем: берем из Designer/скина
            Font = ThemeManager.SharedSettings.DefaultFont;
            ApplyFontSizePermission(); // перекрыть размер, если задан
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
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
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