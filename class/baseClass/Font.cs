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

        // Метод для применения общих свойств к компоненту
        public void ApplyBaseProperties(Control control)
        {
            control.Font = ComponentFont;
        }



    }


    //  
    // Класс-наследник для кнопки
    public class CustomButton : Button
    {
        public CustomButton() :  this(Color.FromArgb(181, 230, 196), Color.FromArgb(20, 36, 24)) { }

        public CustomButton(Color backColor, Color fontColor)
        {
            Btn = new Button();
            //ApplyBaseProperties(Btn);
            BackColor = backColor;
            ForeColor = fontColor;
          //  Font = ComponentFont;
          //Paint()
        }
        public Button Btn { get; set; }
        public Color ComponentBackColor { get;  set; }
        public Color ComponentFontColor { get;  set; }



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
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new System.Drawing.Region(grPath);
            base.OnPaint(e);
        }
        // }
    }
    // Класс-наследник для текстового поля
    public class CustomTextBox : BaseComponent
    {
        public TextBox TextBox { get; private set; }
        public CustomTextBox()
        {
            TextBox = new TextBox();
            ApplyBaseProperties(TextBox);
        }
        public void SetPlaceholderText(string placeholder)
        {
            //TextBox.PlaceholderText = placeholder;
        }
    }

    // Класс-наследник для кастомного GridControl
    public class CustomGridView : BaseComponent
    {
        public GridView gridView { get; private set; }

        public CustomGridView()
        {
            gridView = new GridView();


            // ApplyBaseProperties(gridView);
            CustomizeSelectionStyles();
        }

        private void CustomizeSelectionStyles()
        {
            // Установка стиля выделенной строки
            gridView.Appearance.FocusedRow.BackColor = Color.LightBlue;
            gridView.Appearance.FocusedRow.ForeColor = Color.Black;

            // Установка стиля выделенной ячейки
            gridView.CustomDrawCell += (sender, e) =>
            {
                if (e.RowHandle >= 0 && gridView.IsRowSelected(e.RowHandle)) // Проверка на выделенную строку
                {
                    if (e.Column != null) // Проверка на существование столбца
                    {
                        e.Appearance.Font = new Font(gridView.Appearance.Row.Font.FontFamily, gridView.Appearance.Row.Font.Size, FontStyle.Bold);
                    }
                }
            };
        }

    }



    // Класс для формы с использованием базовых компонентов
    public class CustomForm : Form
    {
        public Color GradientStartColor { get; set; } = Color.FromArgb(255, 248, 240);
        public Color GradientEndColor { get; set; } = Color.FromArgb(120, 167, 137);


        public CustomForm()
        {
            // Настройка формы
            this.Text = "Custom Form";
            this.Size = new Size(400, 300);



            //// Создание и добавление кнопки
            CustomButton customButton = new CustomButton();
            // customButton.SetButtonText("Click Me");
            customButton.Btn.Location = new Point(50, 50);
            this.Controls.Add(customButton.Btn);

            CustomButton btn = new CustomButton();
            btn.Btn.Location = new Point(50, 150);
            btn.Text = "3333333";
            btn.Btn.BackColor = this.GradientStartColor;
            this.Controls.Add(btn.Btn);

            // Создание и добавление текстового поля
            CustomTextBox customTextBox = new CustomTextBox();
            customTextBox.SetPlaceholderText("Enter text here");
            customTextBox.TextBox.Location = new Point(50, 100);
            this.Controls.Add(customTextBox.TextBox);

        }
        public void Form_Paint(object sender, PaintEventArgs e)
        {
            DrawLinearGradient(e.Graphics);
        }


        public void DrawLinearGradient(Graphics graphics)
        {
            // Определение области для градиента
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            // Создание градиента
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect, GradientStartColor, // Начальный цвет
                GradientEndColor, // Конечный цвет
                LinearGradientMode.ForwardDiagonal))
            {
                // Заливка области градиентом
                graphics.FillRectangle(brush, rect);
            }
        }


    }
}