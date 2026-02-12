using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SewingProduction.Features.UserDistribution.Helpers;

namespace SewingProduction.Core.Class
{
    public class CustomPictureBox : PictureBox//, IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }

        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        private float? _fontSizePermission; // размер шрифта из прав

        public CustomPictureBox()
        {
            //ApplyTheme();
            //ThemeManager.ThemeChanged += OnThemeChanged;
        }

        public void ApplyTheme()
        {
            ForeColor = ThemeManager.ActiveTheme.LabelTextColor;
            BackColor = ThemeManager.ActiveTheme.TextBoxBackground;
            Font = ThemeManager.SharedSettings.DefaultFont;
            ApplyFontSizePermission(); // перекрыть размер, если задан
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
        private string _imagePath;

        /// <summary>
        /// Путь к изображению. При установке автоматически подгружает Image.
        /// </summary>
        [Browsable(true)]
        [Category("Data")]
        [Description("Путь к изображению, отображаемому в PictureBox")]
        public string ImagePath
        {
            get => _imagePath;
            set
            {
                if (_imagePath == value)
                    return;

                _imagePath = value;
                LoadImageFromPath(_imagePath);
            }
        }

        /// <summary>
        /// Загрузка изображения из файла без блокировки.
        /// </summary>
        private void LoadImageFromPath(string path)
        {
            try
            {
                // освобождаем старую картинку
                if (Image != null)
                {
                    var old = Image;
                    Image = null;
                    old.Dispose();
                }

                if (string.IsNullOrWhiteSpace(path) || !System.IO.File.Exists(path))
                {
                    _imagePath = null;
                    return;
                }

                using (var fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite))
                using (var tmp = System.Drawing.Image.FromStream(fs))
                {
                    Image = (System.Drawing.Image)tmp.Clone();
                }
            }
            catch
            {
                // если не удалось загрузить, сбрасываем
                Image = null;
            }
        }
    }
}