/*Основные цвета:
Фон (градиент):
Мягкий мятный: RGB(209, 241, 221)
Кремовый (айвори): RGB(255, 248, 240)
Элемент "C" (буква):
Светлый серебристо-серый: RGB(192, 192, 192)
Внутренний градиент (если заметен): между мятным (RGB(209, 241, 221)) и белым (RGB(255, 255, 255)).
Лист клевера:
Нежно-зелёный: RGB(181, 230, 196)
Темно-зелёные акценты (если видны): RGB(120, 167, 137)*/

using DevExpress.CodeParser;
using DevExpress.XtraBars.Docking2010.Base;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static DevExpress.XtraReports.UserDesigner.Native.XRDesignComponentContainer;

namespace SewingProduction
{
    // Базовый класс для общих свойств компонентов
    public class BaseComponent : Control
    {    // Свойство для настройки цвета формы
        public Color ComponentBackColor { get; set; } = Color.FromArgb(0, 165, 80);//(209, 241, 221);
        //Свойство для настройки цвета шрифта
        public Color ComponentFontColor { get; set; } = Color.FromArgb(64, 115, 79);//(20, 36, 24);- очень тёмно зелёный
        // Свойство для настройки стиля шрифта
        public Font ComponentFont { get; set; } = new Font("Arial", 12, FontStyle.Regular);

        // public Color ButtonBackColor { get; set; } = Color.FromArgb(20, 36, 24);

    {
        // Метод для применения общих свойств к компоненту
        public virtual void ApplyBaseProperties(Control control)
        public void ApplyBaseProperties(Control control)
        {
            control.Font = Theme.DefaultFont; // Общий шрифт
            control.Font = ComponentFont;
        }
    }


    //  
    // Класс-наследник для кнопки
    public class CustomButton : Button
    {

        public CustomButton()
        public CustomButton(Color backColor, Color fontColor)
        {

            this.BackColor = Theme.ButtonBackground;
            this.ForeColor = Theme.ButtonText;
            this.Font = Theme.DefaultFont;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Height = Theme.ButtonHeight;
            Btn = new Button();
            //ApplyBaseProperties(Btn);
            BackColor = backColor;
            ForeColor = fontColor;
          //  Font = ComponentFont;
          //Paint()
        }
        public Button Btn { get; set; }
        public Color ComponentBackColor { get; set; }
        public Color ComponentFontColor { get; set; }
        public Size ComponentSize { get; set; }

        private GraphicsPath _graphicsPath;

        protected override void OnPaint(PaintEventArgs pevent)
        {
            _graphicsPath = CreateRoundedRectanglePath(ClientRectangle, Theme.ButtonRoundRadius); // Радиус скругления
            Region = new Region(_graphicsPath);
            base.OnPaint(pevent);
        }

        private GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)
        // Свойство для настройки размеров компонента
        // public Size ComponentSize { get; set; } = new Size(100, 30);

        //public void SetButtonText(string text)
        //{
        //    Button.Text = text;
        //}
        //     public class RoundButton : CustomButton
        //   {
        public void Paint(PaintEventArgs e)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            RectangleF arcRect = new RectangleF(rect.X, rect.Y, diameter, diameter);
            path.AddArc(arcRect, 180, 90);
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);
            arcRect.X = rect.X;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();
            return path;
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new System.Drawing.Region(grPath);
            base.OnPaint(e);
        }

        // }
    }
    // Класс-наследник для текстового поля
    public class CustomTextBox : TextBox
    public class CustomTextBox : BaseComponent
    {
        public CustomTextBox()
        {
            this.BackColor = Theme.TextBoxBackground;
            this.ForeColor = Theme.TextBoxText;
            this.Font = Theme.DefaultFont;
            TextBox = new TextBox();
            ApplyBaseProperties(TextBox);
        }

        public void SetPlaceholderText(string placeholder)
        {
            //TextBox.PlaceholderText = placeholder;
        }
    }

    // Класс-наследник для кастомного GridControl

    //public class CustomGridControl : GridControl
    //{
    //    public CustomGridControl()
    //    {
        public CustomGridView()
        {
            gridView = new GridView();

    //        //// Предполагается, что gridControl1 - это ваш GridControl
    //        //GridView view = CustomGridControl.MainView as GridView;
    //        //if (view != null)
    //        //{
    //        //    // Теперь вы можете обратиться к свойству FocusedRow
    //        //    view.FocusedRowHandle = 1; // Пример: установить фокус на вторую строку (индекс 1)

    //        //    // Или получить индекс текущей выделенной строки
    //        //    int focusedRowIndex = view.FocusedRowHandle;
    //        //}
    //        //// Настройка выделенных строк
    //        //this.Views[1].Appearance.FocusedRow.BackColor = Theme.HighlightBackground;
    //        //this.Appearance.FocusedRow.ForeColor = Theme.HighlightText;
            // ApplyBaseProperties(gridView);
            CustomizeSelectionStyles();
        }

    //        //// Общий стиль шрифта
    //        //this.Appearance.Row.Font = Theme.DefaultFont;
    //    }
    //}
    public class CustomGridControl : GridControl
        private void CustomizeSelectionStyles()
        {
        public CustomGridView CustomView { get; private set; }
            // Установка стиля выделенной строки
            gridView.Appearance.FocusedRow.BackColor = Color.LightBlue;
            gridView.Appearance.FocusedRow.ForeColor = Color.Black;

        public CustomGridControl()
            // Установка стиля выделенной ячейки
            gridView.CustomDrawCell += (sender, e) =>
            {
                if (e.RowHandle >= 0 && gridView.IsRowSelected(e.RowHandle)) // Проверка на выделенную строку
                {
                    if (e.Column != null) // Проверка на существование столбца
                    {
            // Создаем и подключаем кастомное представление
            CustomView = new CustomGridView();
            this.MainView = CustomView;
            this.ViewCollection.Add(CustomView);

            //// Применяем начальную тему
            //ApplyTheme();
                        e.Appearance.Font = new Font(gridView.Appearance.Row.Font.FontFamily, gridView.Appearance.Row.Font.Size, FontStyle.Bold);
                    }
                }
            };
        }

            //// Подписываемся на изменения темы
            //Theme.ThemeChanged += OnThemeChanged;
    }

        //private void ApplyTheme()
        //{
        //    // Применяем тему к GridControl (например, цвет фона)
        //    this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
        //    this.LookAndFeel.UseDefaultLookAndFeel = false;
        //    this.BackColor = Theme.GridBackground;

        //    // Применяем тему к связанному GridView
        //    CustomView.ApplyTheme();
        //}

        //private void OnThemeChanged()
        //{
        //    ApplyTheme();
        //}
    // Класс для формы с использованием базовых компонентов
    public class CustomForm : Form
    {
        public Color GradientStartColor { get; set; } = Color.FromArgb(255, 248, 240);
        public Color GradientEndColor { get; set; } = Color.FromArgb(120, 167, 137);

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        Theme.ThemeChanged -= OnThemeChanged;
        //    }
        //    base.Dispose(disposing);
        //}
    }

    public class CustomGridView : GridView
    {
        public CustomGridView() : base()
        public CustomForm()
        {
            // Применяем начальную тему
            //ApplyTheme();
            this.Appearance.FocusedRow.BackColor = Theme.HighlightBackground;
            //this.ForeColor = Theme.ButtonText;
            //this.Font = Theme.DefaultFont;
            //this.FlatStyle = FlatStyle.Flat;
            //this.FlatAppearance.BorderSize = 0;
            //this.Height = Theme.ButtonHeight;
        }
        public GridView gridView { get; set; }
        public Color HighlightBackground { get; set; }
        //public Color ComponentFontColor { get; set; }
        //public Size ComponentSize { get; set; }
            // Настройка формы
            this.Text = "Custom Form";
            this.Size = new Size(400, 300);



        public void ApplyTheme()
        {
            // Настройка выделенной строки
            this.Appearance.FocusedRow.BackColor = Theme.HighlightBackground;
            //this.Appearance.FocusedRow.ForeColor = Theme.HighlightText;
            //// Создание и добавление кнопки
            CustomButton customButton = new CustomButton();
            // customButton.SetButtonText("Click Me");
            customButton.Btn.Location = new Point(50, 50);
            this.Controls.Add(customButton.Btn);

            // Общий стиль шрифта
            this.Appearance.Row.Font = Theme.DefaultFont;
            CustomButton btn = new CustomButton();
            btn.Btn.Location = new Point(50, 150);
            btn.Text = "3333333";
            btn.Btn.BackColor = this.GradientStartColor;
            this.Controls.Add(btn.Btn);

            //// Настройка общего фона
            //this.Appearance.Empty.BackColor = Theme.GridBackground;
            //this.Appearance.Row.BackColor = Theme.GridRowBackground;
            //this.Appearance.Row.ForeColor = Theme.GridTextColor;
            // Создание и добавление текстового поля
            CustomTextBox customTextBox = new CustomTextBox();
            customTextBox.SetPlaceholderText("Enter text here");
            customTextBox.TextBox.Location = new Point(50, 100);
            this.Controls.Add(customTextBox.TextBox);

            // Настройка выделения ячеек
            this.Appearance.FocusedCell.BackColor = Theme.HighlightBackground;
           // this.Appearance.SelectedRow.ForeColor = Theme.HighlightText;
        }
        }
    // Класс для формы с использованием базовых компонентов
    public class CustomForm : Form
        public void Form_Paint(object sender, PaintEventArgs e)
        {
        protected override void OnPaint(PaintEventArgs e)
            DrawLinearGradient(e.Graphics);
        }


        public void DrawLinearGradient(Graphics graphics)
        {
            base.OnPaint(e);
            // Определение области для градиента
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            // Создание градиента
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect, Theme.GradientStartColor, // Начальный цвет
                Theme.GradientEndColor, // Конечный цвет
                rect, GradientStartColor, // Начальный цвет
                GradientEndColor, // Конечный цвет
                LinearGradientMode.ForwardDiagonal))
            {
                // Заливка области градиентом
                e.Graphics.FillRectangle(brush, rect);
                graphics.FillRectangle(brush, rect);
            }
        }
    }
}