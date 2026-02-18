using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SewingProduction.Features.UserDistribution.Helpers;

namespace SewingProduction.Core.Class.CustomControls
{
    public class CustomToggleSwitch : ToggleSwitch, IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
        private UserClass _lastUser;
        /// <summary>
        ///Контролирует видимость элемента на основе прав пользователя в системе
        /// </summary>
        private bool _visiblePermission = true;
        /// <summary>
        ///Контролирует видимость элемента на основе бизнес-логики приложения
        /// </summary>
        private bool _visibleLogic = true;
        public CustomToggleSwitch()
        {
            ApplyTheme();
        }
        public void ApplyTheme()
        {
            // Цвета оставляем на усмотрение DevExpress/скинов, только базовые параметры
            Font = ThemeManager.SharedSettings.DefaultFont;
            Height = ThemeManager.SharedSettings.ButtonHeight;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public void ApplyPermission(UserClass user)
        {
            _lastUser = user;

            if (!this.IsHandleCreated)
            {
                this.HandleCreated += (_, _) => ApplyPermission(user);
                return;
            }

            if (string.IsNullOrEmpty(ObjectName))
                ObjectName = this.Name;

            if (string.IsNullOrEmpty(ObjectName))
            {
                this.VisiblePermission = false;
                return;
            }

            bool hasWrite = user.HasPermission(ObjectName, "Редактор");
            bool hasRead = user.HasPermission(ObjectName, "Просмотр");

            this.VisiblePermission = hasRead || hasWrite;
            this.Enabled = hasWrite;

            Debug.WriteLine($"[Доступ Button] {ObjectName}: Просмотр={hasRead}, Редактор={hasWrite}, VisiblePermission={this.VisiblePermission}, Enabled={this.Enabled}");
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
        [Category("Видимость")]
        [DisplayName("VisibleLogic")]
        [Description("Контролирует видимость элемента на основе бизнес-логики приложения")]
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
            //Применяем и к контролу, и к лейауту. DevExpress LayoutControl
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
    }
}
