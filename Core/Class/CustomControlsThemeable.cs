
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraReports.Native;
using SewingProduction;
using SewingProduction.Core.Class;
using SewingProduction.Core.Extensions;
using SewingProduction.Features.UserDistribution.Class;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution.Models;
using SewingProduction.form.TeamWork.Interfaces;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.LookAndFeel.DXSkinColors;

namespace SewingProduction
{
    public interface IThemeable
    {
        void ApplyTheme();
    }
    //public interface IThemeableControl
    //{
    //    string ObjectName { get; set; }
    //    bool VisiblePermission { get; set; }
    //    bool VisibleLogic { get; set; }
    //    void ApplyPermission(UserClass user);
    //}
    // Разделение ответственности: Права на видимость
    public interface IVisibilityPermission
    {
        bool VisiblePermission { get; set; }
    }

    // Разделение ответственности: Бизнес-логика/сценарная видимость
    public interface IVisibilityLogic
    {
        bool VisibleLogic { get; set; }
    }

    // Агрегирующий интерфейс для контролов в UI
    public interface IThemeableControl : IThemeable, IVisibilityPermission, IVisibilityLogic
    {
        string ObjectName { get; set; }
        void ApplyPermission(UserClass user);
    }

    // Универсальный помощник для итоговой видимости
    public static class VisibilityHelper
    {
        // Итог: права И логика
        public static bool Compute(bool permission, bool logic) => permission && logic;

        // Применяет видимость к Control и к его LayoutControlItem, если он обернут в DevExpress LayoutControl
        public static void UpdateVisibility(Control control, bool permission, bool logic, Func<Control, LayoutControlItem> findLayoutItem = null)
        {
            var finalVisible = Compute(permission, logic);

            // 1) Привычный слой WinForms — для совместимости со сторонним кодом
            control.Visible = finalVisible;

            // 2) Макетный слой — без «дыр» в LayoutControl
            if (findLayoutItem != null)
            {
                var item = findLayoutItem(control);
                if (item != null)
                {
                    item.Visibility = finalVisible ? LayoutVisibility.Always : LayoutVisibility.Never;
                }
            }
        }
    }

    // Базовая реализация для контролов, поддерживающих наши флаги
    public abstract class ThemeableControlBase : Control, IThemeableControl
    {
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;

        // Имя сущности для системы прав
        [Category("Security")]
        public string ObjectName { get; set; }

        // Права (от авторизации)
        [Category("Security")]
        public bool VisiblePermission
        {
            get => _visiblePermission;
            set
            {
                if (_visiblePermission == value) return;
                _visiblePermission = value;
                ApplyEffectiveVisibility();
            }
        }

        // Логика (от сценария/режима)
        [Category("Behavior")]
        public bool VisibleLogic
        {
            get => _visibleLogic;
            set
            {
                if (_visibleLogic == value) return;
                _visibleLogic = value;
                ApplyEffectiveVisibility();
            }
        }

        // Прямое изменение Visible трактуем как изменение логики (как и раньше)
        public new bool Visible
        {
            get => base.Visible;
            set
            {
                // Обновляем только логику; права не трогаем
                if (_visibleLogic == value && base.Visible == value) return;
                _visibleLogic = value;
                ApplyEffectiveVisibility();
            }
        }

        // Применение темы (оставлено как контракт)
        public abstract void ApplyTheme();

        // Применение прав доступа к контролу
        public virtual void ApplyPermission(UserClass user)
        {
            // Пример: получить разрешение по ObjectName в своей подсистеме прав
            // Здесь вызов условной службы, показываем шаблон.
            // var allowed = PermissionService.CanView(user, ObjectName);
            // VisiblePermission = allowed;

            // Пока — оставим без изменения, если нет службы прав
            ApplyEffectiveVisibility();
        }

        // Централизованное применение состояния видимости
        protected virtual void ApplyEffectiveVisibility()
        {
            VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic, FindLayoutControlItemSafe);
        }

        // Поиск LayoutControlItem для контрола — универсально и без хардкода
        protected LayoutControlItem FindLayoutControlItemSafe(Control control)
        {
            if (control == null) return null;

            // 1) Ищем все LayoutControl в дереве формы
            var form = control.FindForm();
            if (form == null) return null;

            foreach (var lc in GetAllLayoutControls(form))
            {
                var item = FindLayoutItemRecursive(lc.Root, control);
                if (item != null) return item;
            }
            return null;
        }

        public static IEnumerable<LayoutControl> GetAllLayoutControls(Control root)
        {
            var stack = new Stack<Control>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var c = stack.Pop();
                if (c is LayoutControl lc)
                    yield return lc;

                foreach (Control child in c.Controls)
                    stack.Push(child);
            }
        }

        public static LayoutControlItem FindLayoutItemRecursive(BaseLayoutItem item, Control target)
        {
            if (item == null) return null;

            if (item is LayoutControlItem lci && lci.Control == target)
                return lci;

            if (item is LayoutControlGroup group)
            {
                foreach (BaseLayoutItem child in group.Items)
                {
                    var found = FindLayoutItemRecursive(child, target);
                    if (found != null) return found;
                }
            }
            return null;
        }
    }

    // Пример адаптера для уже существующих контролов, если нельзя менять наследование:
    public static class VisibilityExtensions
    {
        // Универсальный способ применить права и логику к любому Control,
        // не требуя, чтобы он реализовывал IThemeableControl.
        public static void ApplyVisibility(this Control control, bool permission, bool logic)
        {
            VisibilityHelper.UpdateVisibility(control, permission, logic, FindLayoutControlItemFromControlTree);
        }

        private static LayoutControlItem FindLayoutControlItemFromControlTree(Control control)
        {
            if (control == null) return null;
            var form = control.FindForm();
            if (form == null) return null;

            foreach (var lc in ThemeableControlBase.GetAllLayoutControls(form))
            {
                var item = ThemeableControlBase.FindLayoutItemRecursive(lc.Root, control);
                if (item != null) return item;
            }
            return null;
        }
    }


   

    public class CustomCheckBox : CheckBox, IThemeable, IThemeableControl
    {
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
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

    public class CustomRadioButton : RadioButton, IThemeable, IThemeableControl
    {
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        public CustomRadioButton()
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
    public class CustomDateTimePicker : DateTimePicker, IThemeable, IThemeableControl
    {
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        public CustomDateTimePicker()
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
    public class CustomNumericUpDown : NumericUpDown, IThemeable, IThemeableControl
    {
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        public CustomNumericUpDown()
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
    public class CustomListBox : ListBox, IThemeable, IThemeableControl
    {
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        public CustomListBox()
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
    
    public class CustomTextBoxEx : DevExpress.XtraEditors.TextEdit, IThemeable, IThemeableControl
    {
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        public CustomTextBoxEx()
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

    public class CustomMaskedTextBox : MaskedTextBox, IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
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


    public class CustomComboBox : ComboBox, IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
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

    public class CustomTabControl : DevExpress.XtraTab.XtraTabControl, IThemeable, IThemeableControl
    {
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        public CustomTabControl()
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




    /// <summary>
    /// Кастомный прозрачный группбокс с черной обводкой
    /// </summary>
    public class CustomGroupBox : GroupBox, IThemeableControl, IThemeable
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        private Color _borderColor = Color.Black; // Цвет обводки по умолчанию
        private int _borderThickness = 1;       // Толщина обводки по умолчанию

        // Свойство для установки цвета обводки
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        // Свойство для установки толщины обводки
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderThickness
        {
            get { return _borderThickness; }
            set { _borderThickness = value; Invalidate(); }
        }

        // Переопределение метода OnPaint для рисования обводки
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e); //рисуем всё что есть в дефолтном GroupBox
            // Рисуем границу
            using (Pen borderPen = new Pen(_borderColor, _borderThickness))
            {
                // Определение прямоугольника для обводки
                var rect = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - _borderThickness, ClientRectangle.Height - _borderThickness);
                rect.X += _borderThickness / 2;
                rect.Y += _borderThickness / 2;

                //e.Graphics.DrawRectangle(borderPen, rect);

                // Замеряем размер текста
                SizeF textSize = e.Graphics.MeasureString(Text, Font);
                int textBottomY = (int)textSize.Height;
                // рисуем границу
                e.Graphics.DrawLine(borderPen, rect.X, rect.Y + textBottomY, rect.X, rect.Y + rect.Height); //Левая полоса
                e.Graphics.DrawLine(borderPen, rect.X + rect.Width, rect.Y + textBottomY, rect.X + rect.Width, rect.Y + rect.Height); // Правая
                e.Graphics.DrawLine(borderPen, rect.X, rect.Y + textBottomY, rect.X + rect.Width, rect.Y + textBottomY); // Верхняя
                e.Graphics.DrawLine(borderPen, rect.X, rect.Y + rect.Height, rect.X + rect.Width, rect.Y + rect.Height); // Нижняя
                //e.Graphics.DrawLine(borderPen, rect.X + (int)rect.Width / 2 + (int)textSize.Width / 2 + 5, rect.Y + textBottomY, rect.X + rect.Width, rect.Y + textBottomY);

            }
        }
        public CustomGroupBox()
        {
            this.BackColor = Color.Transparent;
            //this.ForeColor = Theme.TextBoxText;
            //this.Font = Theme.DefaultFont;
        }

        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }

        public void ApplyTheme()
        {
            // Устанавливаем цвет текста и шрифт из текущей темы
            this.ForeColor = ThemeManager.ActiveTheme.LabelTextColor; // Используем цвет для Label
            this.Font = ThemeManager.SharedSettings.DefaultFont;
            this.BorderColor = ThemeManager.ActiveTheme.LabelTextColor; // Можно сделать цвет рамки таким же
            Invalidate(); // Перерисовать контрол с новыми цветами
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
    /// <summary>
    /// Кастомная форма с градиентным фоном
    /// </summary>
    public class CustomForm : Form, SewingProduction.IThemeable
    {
        public int FormID;
        protected UserClass _user;
        public UserClass User => _user;

        private void ApplyThemeToChildren(Control parentControl)
        {
            foreach (Control childControl in parentControl.Controls)
            {
                if (childControl is IThemeable themeableChild)
                {
                    themeableChild.ApplyTheme();
                }
                if (childControl.HasChildren)
                {
                    ApplyThemeToChildren(childControl);
                }
            }
        }

        public CustomForm()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
            {
                _user = new UserClass();
            }

            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
            this.Load += (s, e) => { if (!this.DesignMode) ApplyThemeToChildren(this); };
        }
        public CustomForm(UserClass user)
        {
            // сохраняем пользователя
            _user = user ?? throw new ArgumentNullException(nameof(user));

            ApplyTheme(); // Применяем тему к самой форме (фон)
            ThemeManager.ThemeChanged += OnThemeChanged;
            // Применяем тему к дочерним контролам после инициализации самой формы
            this.Load += (s, e) => { if (!this.DesignMode) ApplyThemeToChildren(this); };

            // подписка на загрузку формы (для логирования и прав доступа - существующий код)
            this.Load += async (s, e) =>
            {
                await ActionLogger.Log(_user.UserId, "Открытие формы", NameForm: this.GetType().Name);

                // Включаем автоматическое сохранение настроек для всех CustomGridControl
                InitializeAutoGridSettings();

                CustomForm_Load(s, e);
            };

            // Сохраняем настройки при закрытии формы
            this.FormClosing += (s, e) =>
            {
                this.SaveAllGridSettings();
                this.Dispose();
            };
        }

        public void ApplyTheme()
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Invalidate()));
            }
            else
            {
                Invalidate();
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e); // Call base.OnPaint first
            //Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            //using (LinearGradientBrush brush = new LinearGradientBrush(
            //    rect,
            //    ThemeManager.ActiveTheme.GradientStartColor, 
            //    ThemeManager.ActiveTheme.GradientEndColor,
            //    LinearGradientMode.ForwardDiagonal))
            //{
            //    e.Graphics.FillRectangle(brush, rect);
            //}
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;

                // рекурсивно освобождаем все контролы
                DisposeControls(this);

                if (this.Container != null)
                {
                    foreach (var comp in this.Container.Components)
                    {
                        if (comp is IDisposable d)
                            d.Dispose();
                    }
                }
            }
            base.Dispose(disposing);
        }
        private void DisposeControls(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is DevExpress.XtraGrid.GridControl grid)
                {
                    grid.DataSource = null;
                    grid.Dispose();
                }
                else if (ctrl is DataGridView dgv)
                {
                    dgv.DataSource = null;
                    dgv.Dispose();
                }

                if (ctrl.HasChildren)
                    DisposeControls(ctrl);

                ctrl.Dispose();
            }
        }
        /// <summary>
        /// Инициализирует автоматическое сохранение настроек для всех CustomGridControl на форме
        /// </summary>
        protected virtual void InitializeAutoGridSettings()
        {
            try
            {
                this.EnableAutoGridSettings(true);
            }
            catch (Exception ex)
            {
                // Логируем ошибку, но не прерываем работу формы
                System.Diagnostics.Debug.WriteLine($"Ошибка при инициализации автоматических настроек гридов: {ex.Message}");
            }
        }

        private void OnThemeChanged() => ApplyTheme();
        private async void CustomForm_Load(object sender, EventArgs e)
        {
            string formName = this.GetType().Name;

            await _user.LoadObjectForm(formName);

            // нет вообще доступа — закрываем
            if (!_user.HasPermission(formName, "Просмотр") && !_user.HasPermission(formName, "Редактор"))
            {
                MessageBox.Show(
                    $"У вас нет доступа к форме '{formName}'.",
                    "Доступ запрещён",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                this.BeginInvoke((MethodInvoker)(() => this.Close()));
                return;
            }

            // если только просмотр — отключаем все контролы
            if (_user.HasPermission(formName, "Просмотр") && !_user.HasPermission(formName, "Редактор"))
            {
                DisableAllControls(this);
                return;
            }

            // если редактор — применяем доступ к каждому элементу
            ApplyPermissionsToControls(this, _user);
        }
        private void ApplyPermissionsToControls(Control parent, UserClass user)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is IThemeableControl themeable)
                {
                    if (string.IsNullOrEmpty(themeable.ObjectName) && !string.IsNullOrEmpty(ctrl.Name))
                        themeable.ObjectName = ctrl.Name;

                    string name = themeable.ObjectName ?? ctrl.Name;
                    bool hasWrite = user.HasPermission(name, "Редактор");
                    bool hasRead = user.HasPermission(name, "Просмотр");

                    Debug.WriteLine($"[Доступ Control] Объект: {name}, Просмотр: {hasRead}, Редактор: {hasWrite}");

                    if (!ctrl.GetType().Name.StartsWith("Custom"))
                        PermissionHelper.ApplyTo(ctrl, name, user);
                    else
                        themeable.ApplyPermission(user);
                }

                // Обработка табов — обязательно
                if (ctrl is DevExpress.XtraTab.XtraTabControl tabControl)
                {
                    foreach (DevExpress.XtraTab.XtraTabPage tabPage in tabControl.TabPages)
                    {
                        if (tabPage is IThemeableControl themeableTab)
                        {
                            if (string.IsNullOrEmpty(themeableTab.ObjectName) && !string.IsNullOrEmpty(tabPage.Name))
                                themeableTab.ObjectName = tabPage.Name;

                            string tabName = themeableTab.ObjectName ?? tabPage.Name;
                            bool hasWrite = user.HasPermission(tabName, "Редактор");
                            bool hasRead = user.HasPermission(tabName, "Просмотр");

                            Debug.WriteLine($"[Доступ TabPage] Вкладка: {tabName}, Просмотр: {hasRead}, Редактор: {hasWrite}");

                            themeableTab.ApplyPermission(user);
                        }

                        // 🔁 Обработка всех контролов внутри вкладки
                        ApplyPermissionsToControls(tabPage, user);
                    }
                }

                // 🔁 Рекурсивно обрабатываем все вложенные контролы
                if (ctrl.HasChildren)
                    ApplyPermissionsToControls(ctrl, user);
            }
        }

        private void DisableAllControls(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (!(ctrl is Label || ctrl is PictureBox))
                    ctrl.Enabled = false;

                if (ctrl.HasChildren)
                    DisableAllControls(ctrl);
            }
        }

    }
    public static class PermissionHelper
    {
        public static void ApplyTo(Control ctrl, string objectName, UserClass user)
        {
            if (string.IsNullOrEmpty(objectName))
                objectName = ctrl.Name;

            if (string.IsNullOrEmpty(objectName))
            {
                Debug.WriteLine("[PermissionHelper] objectName не найден");
                return;
            }

            bool hasWrite = user.HasPermission(objectName, "Редактор");
            bool hasRead = user.HasPermission(objectName, "Просмотр");

            //Debug.WriteLine($"[PermissionHelper] {objectName}: Просмотр={hasRead}, Редактор={hasWrite}");

            ctrl.Enabled = hasWrite;
            ctrl.Visible = hasRead || hasWrite;
        }
    }
    //public static class VisibilityHelper
    //{
    //    public static void UpdateVisibility(Control ctrl, bool visiblePermission, bool visibleLogic)
    //    {
    //        ctrl.Visible = visiblePermission && visibleLogic;
    //    }
    //}
}
//using DevExpress.XtraLayout;
//using DevExpress.XtraLayout.Utils;
//using SewingProduction.Features.UserDistribution.Class;
//using SewingProduction.Features.UserDistribution.Helpers;
//using SewingProduction.Features.UserDistribution.Models;
//using SewingProduction.Helpers;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Windows.Forms;

//namespace SewingProduction
//{
//    // Тема
//    public interface IThemeable
//    {
//        void ApplyTheme();
//    }

//    // Разделение ответственности: Права на видимость
//    public interface IVisibilityPermission
//    {
//        bool VisiblePermission { get; set; }
//    }

//    // Разделение ответственности: Бизнес-логика/сценарная видимость
//    public interface IVisibilityLogic
//    {
//        bool VisibleLogic { get; set; }
//    }

//    // Агрегирующий интерфейс для контролов в UI
//    public interface IThemeableControl : IThemeable, IVisibilityPermission, IVisibilityLogic
//    {
//        string ObjectName { get; set; }
//        void ApplyPermission(UserClass user);
//    }

//    // Универсальный помощник для итоговой видимости
//    public static class VisibilityHelper
//    {
//        // Итог: права И логика
//        public static bool Compute(bool permission, bool logic) => permission && logic;

//        // Применяет видимость к Control и к его LayoutControlItem, если он обернут в DevExpress LayoutControl
//        public static void UpdateVisibility(Control control, bool permission, bool logic, Func<Control, LayoutControlItem> findLayoutItem = null)
//        {
//            var finalVisible = Compute(permission, logic);

//            // 1) Привычный слой WinForms — для совместимости со сторонним кодом
//            control.Visible = finalVisible;

//            // 2) Макетный слой — без «дыр» в LayoutControl
//            if (findLayoutItem != null)
//            {
//                var item = findLayoutItem(control);
//                if (item != null)
//                {
//                    item.Visibility = finalVisible ? LayoutVisibility.Always : LayoutVisibility.Never;
//                }
//            }
//        }
//    }

//    // Базовая реализация для контролов, поддерживающих наши флаги
//    public abstract class ThemeableControlBase : Control, IThemeableControl
//    {
//        private bool _visiblePermission = true;
//        private bool _visibleLogic = true;

//        // Имя сущности для системы прав
//        [Category("Security")]
//        public string ObjectName { get; set; }

//        // Права (от авторизации)
//        [Category("Security")]
//        public bool VisiblePermission
//        {
//            get => _visiblePermission;
//            set
//            {
//                if (_visiblePermission == value) return;
//                _visiblePermission = value;
//                ApplyEffectiveVisibility();
//            }
//        }

//        // Логика (от сценария/режима)
//        [Category("Behavior")]
//        public bool VisibleLogic
//        {
//            get => _visibleLogic;
//            set
//            {
//                if (_visibleLogic == value) return;
//                _visibleLogic = value;
//                ApplyEffectiveVisibility();
//            }
//        }

//        // Прямое изменение Visible трактуем как изменение логики (как и раньше)
//        public new bool Visible
//        {
//            get => base.Visible;
//            set
//            {
//                // Обновляем только логику; права не трогаем
//                if (_visibleLogic == value && base.Visible == value) return;
//                _visibleLogic = value;
//                ApplyEffectiveVisibility();
//            }
//        }

//        // Применение темы (оставлено как контракт)
//        public abstract void ApplyTheme();

//        // Применение прав доступа к контролу
//        public virtual void ApplyPermission(UserClass user)
//        {
//            // Пример: получить разрешение по ObjectName в своей подсистеме прав
//            // Здесь вызов условной службы, показываем шаблон.
//            // var allowed = PermissionService.CanView(user, ObjectName);
//            // VisiblePermission = allowed;

//            // Пока — оставим без изменения, если нет службы прав
//            ApplyEffectiveVisibility();
//        }

//        // Централизованное применение состояния видимости
//        protected virtual void ApplyEffectiveVisibility()
//        {
//            VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic, FindLayoutControlItemSafe);
//        }

//        // Поиск LayoutControlItem для контрола — универсально и без хардкода
//        protected LayoutControlItem FindLayoutControlItemSafe(Control control)
//        {
//            if (control == null) return null;

//            // 1) Ищем все LayoutControl в дереве формы
//            var form = control.FindForm();
//            if (form == null) return null;

//            foreach (var lc in GetAllLayoutControls(form))
//            {
//                var item = FindLayoutItemRecursive(lc.Root, control);
//                if (item != null) return item;
//            }
//            return null;
//        }

//        public static IEnumerable<LayoutControl> GetAllLayoutControls(Control root)
//        {
//            var stack = new Stack<Control>();
//            stack.Push(root);
//            while (stack.Count > 0)
//            {
//                var c = stack.Pop();
//                if (c is LayoutControl lc)
//                    yield return lc;

//                foreach (Control child in c.Controls)
//                    stack.Push(child);
//            }
//        }

//        public static LayoutControlItem FindLayoutItemRecursive(BaseLayoutItem item, Control target)
//        {
//            if (item == null) return null;

//            if (item is LayoutControlItem lci && lci.Control == target)
//                return lci;

//            if (item is LayoutControlGroup group)
//            {
//                foreach (BaseLayoutItem child in group.Items)
//                {
//                    var found = FindLayoutItemRecursive(child, target);
//                    if (found != null) return found;
//                }
//            }
//            return null;
//        }
//    }

//    // Пример адаптера для уже существующих контролов, если нельзя менять наследование:
//    public static class VisibilityExtensions
//    {
//        // Универсальный способ применить права и логику к любому Control,
//        // не требуя, чтобы он реализовывал IThemeableControl.
//        public static void ApplyVisibility(this Control control, bool permission, bool logic)
//        {
//            VisibilityHelper.UpdateVisibility(control, permission, logic, FindLayoutControlItemFromControlTree);
//        }

//        private static LayoutControlItem FindLayoutControlItemFromControlTree(Control control)
//        {
//            if (control == null) return null;
//            var form = control.FindForm();
//            if (form == null) return null;

//            foreach (var lc in ThemeableControlBase.GetAllLayoutControls(form))
//            {
//                var item = ThemeableControlBase.FindLayoutItemRecursive(lc.Root, control);
//                if (item != null) return item;
//            }
//            return null;
//        }
//    }
//}
