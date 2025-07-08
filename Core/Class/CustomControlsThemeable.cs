using DevExpress.Utils.Menu;
using DevExpress.XtraGrid;
using DevExpress.XtraReports.Native;
using SewingProduction.form.TeamWork.Interfaces;
using SewingProduction.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.LookAndFeel.DXSkinColors;
using SewingProduction;
using DevExpress.XtraLayout;
using System.ComponentModel;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution.Models;
using SewingProduction.Services;
using System.Diagnostics;
using System.Linq;

namespace SewingProduction
{
    public interface IThemeable
    {
        void ApplyTheme();
    }
    public interface IThemeableControl
    {
        string ObjectName { get; set; }
        void ApplyPermission(UserClass user);
    }
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
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
    }
    public class CustomOkButton : CustomButton, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
        public CustomOkButton()
        {
            Text = "OK";
            DialogResult = DialogResult.OK;
        }
        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
    }

    public class CustomCancelButton : CustomButton, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
        public CustomCancelButton()
        {
            Text = "Cancel";
            DialogResult = DialogResult.Cancel;
        }
        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
    }

    public class CustomTextBox : TextBox, IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
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
        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
    }

    public class CustomCheckBox : CheckBox, IThemeable, IThemeableControl
    {
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
    }


    public class CustomSimpleButton : DevExpress.XtraEditors.SimpleButton, IThemeable
    {
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
            //BackColor = ThemeManager.ActiveTheme.ButtonBackground;
            //ForeColor = ThemeManager.ActiveTheme.ButtonTextColor;
            //Font = ThemeManager.SharedSettings.DefaultFont;

            //FlatStyle = FlatStyle.Standard;
            //FlatAppearance.BorderSize = 1;
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
    public class CustomRadioButton : RadioButton, IThemeable
    {
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
    }
    public class CustomDateTimePicker : DateTimePicker, IThemeable
    {
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
    }
    public class CustomNumericUpDown : NumericUpDown, IThemeable
    {
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
    }
    public class CustomListBox : ListBox, IThemeable
    {
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
    }
    public class CustomCheckedListBox : CheckedListBox, IThemeable
    {
        public CustomCheckedListBox()
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
    public class CustomTextBoxEx : DevExpress.XtraEditors.TextEdit, IThemeable
    {
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
    }

    public class CustomMaskedTextBox : MaskedTextBox, IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
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
    }


    public class CustomComboBox : ComboBox, IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
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
    }

    public class CustomTabControl : DevExpress.XtraTab.XtraTabControl, IThemeable
    {
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
    }

    public class CustomLabel : Label, IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
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
        public void ApplyPermission(UserClass user)
        {
            PermissionHelper.ApplyTo(this, ObjectName, user);
        }
    }


    public class CustomGridControl : GridControl, SewingProduction.IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color? AlternateRowColor { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
        private UserClass _user;
        private List<int> _tableIds;
        private int _formId;
        public CustomGridControl()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
            ViewRegistered += OnViewRegistered;
        }
        public void ApplyTheme()
        {
            BackColor = ThemeManager.ActiveTheme.GridBackground;
            ForeColor = ThemeManager.ActiveTheme.TextBoxText;
            Font = ThemeManager.SharedSettings.DefaultFont;
            AlternateRowColor = ThemeManager.ActiveTheme.BandHighlightColor;


            foreach (var view in ViewCollection)
            {
                if (view is DevExpress.XtraGrid.Views.Grid.GridView gridView)
                {
                    ApplyRowColors(gridView);
                    gridView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                    gridView.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                }
            }
        }
        private void OnViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            if (e.View is DevExpress.XtraGrid.Views.Grid.GridView gridView)
            {
                ApplyRowColors(gridView);
            }
        }
        private void ApplyRowColors(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            if (AlternateRowColor.HasValue)
            {
                gridView.Appearance.EvenRow.BackColor = AlternateRowColor.Value;
                gridView.OptionsView.EnableAppearanceEvenRow = true;
            }
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
            //PermissionHelper.ApplyTo(this, ObjectName, user);

            if (string.IsNullOrEmpty(ObjectName)) return;

            bool hasWrite = user.HasPermission(ObjectName, "Редактор");
            bool hasRead = user.HasPermission(ObjectName, "Просмотр");

            this.Visible = hasRead || hasWrite;
            //this.Enabled = hasWrite;
            foreach (var view in ViewCollection)
            {
                if (view is DevExpress.XtraGrid.Views.Grid.GridView gridView)
                {
                    gridView.OptionsBehavior.ReadOnly = !hasWrite;
                    gridView.OptionsBehavior.Editable = hasWrite;
                }
            }
        }

        public async void InitializeAccess(UserClass user, string formName, List<string> tableNames = null)
        {
            var dbHelper = new DatabaseHelper("ace");
            var dbService = new DbService(dbHelper);
            var roleService = new RoleDataService(dbService, dbHelper);
            var columnService = new AllColumnNameDataService(dbService, dbHelper);
            var tableService = new AllTableNameDataService(dbService, dbHelper);
            var formService = new FormDataService(dbService, dbHelper);

            _user = user;

            // Получаем ID формы
            _formId = (await formService.GetFormIdByNameAsync(formName)) ?? 0;

            Debug.WriteLine($" _user.name: {_user.UserName}");
            Debug.WriteLine($" ObjectName: {ObjectName}");
            Debug.WriteLine($" formName: {formName}");
            Debug.WriteLine($" _formId: {_formId}");
            if (_user == null || string.IsNullOrEmpty(ObjectName) || _formId <= 0)
            {
                Debug.WriteLine("[InitializeAccess] Ошибка: не заданы обязательные параметры.");
                return;
            }

            // Получаем ID ролей пользователя
            List<int> roleIds = await roleService.GetRoleIdsByNamesAsync(_user.Roles);

            // Готовим словарь с правами на колонки
            Dictionary<string, int> columnAccess = new();

            await ApplyPermissionPerColumn(roleIds, columnService, tableService, columnAccess, tableNames);
            await ApplyPermissionPerMode(columnAccess);
        }
        private async Task ApplyPermissionPerColumn(
            List<int> roleIds, 
            AllColumnNameDataService columnService, 
            AllTableNameDataService tableService, 
            Dictionary<string, int> columnAccess, 
            List<string> tableNames)
        {
            if (tableNames != null && tableNames.Any())
            {
                // Есть указание конкретных таблиц — применяем по каждой
                foreach (var tableName in tableNames)
                {
                    var tableId = await tableService.GetTableIdByNameAsync(tableName);
                    if (tableId.HasValue)
                    {
                        Debug.WriteLine($"[InitializeAccess] Таблица '{tableName}' → ID: {tableId.Value}");
                        var columns = await columnService.GetColumnsWithAccessAsync(roleIds, ObjectName, _formId, tableId.Value);

                        foreach (var col in columns)
                        {
                            if (columnAccess.TryGetValue(col.name, out int current))
                                columnAccess[col.name] = Math.Max(current, col.ModeID);
                            else
                                columnAccess[col.name] = col.ModeID;
                        }
                    }
                    else
                    {
                        Debug.WriteLine($"[InitializeAccess] Таблица '{tableName}' не найдена.");
                    }
                }
            }
            else
            {
                // Нет конкретных таблиц — берём все доступные по форме
                var columns = await columnService.GetColumnsWithAccessAsync(roleIds, ObjectName, _formId);

                foreach (var col in columns)
                {
                    columnAccess[col.name] = col.ModeID;
                }
            }
        }
        private async Task ApplyPermissionPerMode(Dictionary<string, int> columnAccess)
        {
            foreach (var view in ViewCollection)
            {
                if (view is DevExpress.XtraGrid.Views.Grid.GridView gridView)
                {
                    foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridView.Columns)
                    {
                        if (columnAccess.TryGetValue(column.FieldName, out int mode))
                        {
                            column.Visible = mode > 0;
                            column.OptionsColumn.ReadOnly = mode < 2;
                            column.OptionsColumn.AllowEdit = mode == 2;
                            Debug.WriteLine($"[ApplyPermissionPerColumn] Колонка {column.FieldName} → ModeID={mode}");
                        }
                        else
                        {
                            column.Visible = false;
                            Debug.WriteLine($"[ApplyPermissionPerColumn] Колонка {column.FieldName} скрыта (нет прав)");
                        }
                    }
                }
            }
            Debug.WriteLine("[ApplyPermissionPerColumn] Завершено");
        }
    }
    /// <summary>
    /// Кастомный прозрачный группбокс с черной обводкой
    /// </summary>
    public class CustomGroupBox : GroupBox, IThemeableControl, IThemeable
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
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
    }
    /// <summary>
    /// Кнопка с записью в бд
    /// </summary>
    public class CustomActionButton : CustomButton
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EventDescription { get; set; } = "Нажатие кнопки";

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            _ = LogActionToDatabase(); // Fire-and-forget
        }

        private async Task LogActionToDatabase()
        {
            if (this.FindForm() is CustomForm form && form.User is UserClass user)
            {
                await ActionLogger.Log(user.UserId, "Нажатие на кнопку", NameForm: this.FindForm()?.Name, NameObject: this.Name);
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
            ApplyTheme(); // Применяем тему к самой форме (фон)
            ThemeManager.ThemeChanged += OnThemeChanged;
            // Применяем тему к дочерним контролам после инициализации самой формы
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
                CustomForm_Load(s, e);
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
            base.OnPaint(e); // Call base.OnPaint first
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
                    // Если ObjectName не задан вручную — ставим по имени контрола
                    if (string.IsNullOrEmpty(themeable.ObjectName) && !string.IsNullOrEmpty(ctrl.Name))
                    {
                        themeable.ObjectName = ctrl.Name;
                    }

                    themeable.ApplyPermission(user);
                }

                if (ctrl.HasChildren)
                {
                    ApplyPermissionsToControls(ctrl, user);
                }
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
            if (string.IsNullOrEmpty(objectName)) return;

            bool hasWrite = user.HasPermission(objectName, "Редактор");
            bool hasRead = user.HasPermission(objectName, "Просмотр");

            ctrl.Visible = hasRead || hasWrite;
            ctrl.Enabled = hasWrite;
        }
    }
}
