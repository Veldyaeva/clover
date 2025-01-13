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
using DevExpress.XtraGauges.Core.Base;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static DevExpress.XtraReports.UserDesigner.Native.XRDesignComponentContainer;

namespace SewingProduction
{
    // Базовый класс для общих свойств компонентов
    public class BaseComponent : Control
    {
        // Метод для применения общих свойств к компоненту
        public virtual void ApplyBaseProperties(Control control)
        {
            control.Font = Theme.DefaultFont; // Общий шрифт
        }
    }


    //  
    // Класс-наследник для кнопки
    public class CustomButton : Button
    {

        public CustomButton()
        {

            this.BackColor = Theme.ButtonBackground;
            this.ForeColor = Theme.ButtonText;
            this.Font = Theme.DefaultFont;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Height = Theme.ButtonHeight;
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
        }

    }
    // Класс-наследник для кнопки "Ок"
    public class CustomOkButton : CustomButton
    {
        public CustomOkButton()
        {
            this.Text = Text;
            //this.Text = Theme.OkText;//"Ок"; // Текст кнопки
            this.Click += OnOkButtonClick; // Обработчик события Click
            this.BackColor = Theme.OkButtonBackground; // Цвет кнопки
            this.ForeColor = Theme.OkButtonTextColor; // Цвет текста
            this.Text = Theme.OkButtonText;
        }

        public override string Text { get; set; } = "Ok";
        // Событие при нажатии на кнопку "Ок"
        private void OnOkButtonClick(object sender, EventArgs e)
        {
            // Логика для кнопки "Ок"
        //    MessageBox.Show("Нажата кнопка 'Ок'");
        }
    }

    // Класс-наследник для кнопки "Отмена"
    public class CustomCancelButton : CustomButton
    {
        public CustomCancelButton()
        {
            this.Text = "Отмена"; // Текст кнопки
            this.Click += OnCancelButtonClick; // Обработчик события Click
            this.BackColor = Theme.CancelButtonBackground; // Цвет кнопки
            this.ForeColor = Theme.CancelButtonTextColor; // Цвет текста
            this.Text = Theme.CancelButtonText;
        }

        // Событие при нажатии на кнопку "Отмена"
        private void OnCancelButtonClick(object sender, EventArgs e)
        {
            // Логика для кнопки "Отмена"
            DialogResult result = MessageBox.Show("Вы уверены, что хотите отменить?",
                                                  "Подтверждение",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
              //  this.FindForm()?.Close(); // Закрыть текущую форму
            }
        }
    }


    // Класс-наследник для текстового поля
    public class CustomTextBox : TextBox
    {
        public CustomTextBox()
        {
            this.BackColor = Theme.TextBoxBackground;
            this.ForeColor = Theme.TextBoxText;
            this.Font = Theme.DefaultFont;
            //this.TextAlign = HorizontalAlignment.Left;
            //this.Margin = new Padding(0) ;
        }

    }

    //Класс-наследник для ComboBox
    public class CustomComboBox : ComboBox {
        public CustomComboBox()
        {
            this.BackColor = Theme.TextBoxBackground;
            this.ForeColor = Theme.TextBoxText;
            this.Font = Theme.DefaultFont;

        }
    }

    // Класс-наследник для кастомного GridControl

    //public class CustomGridControl : GridControl
    //{
    //    public CustomGridControl()
    //    {

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

    //        //// Общий стиль шрифта
    //        //this.Appearance.Row.Font = Theme.DefaultFont;
    //    }
    //}
    public class CustomGridControl : GridControl
    {
        public CustomGridView CustomView { get; private set; }

        public CustomGridControl()
        {
            // Создаем и подключаем кастомное представление
            CustomView = new CustomGridView();
            this.MainView = CustomView;
            this.ViewCollection.Add(CustomView);
            //// Применяем начальную тему
            //ApplyTheme();

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
        {
            // Применяем начальную тему
            //ApplyTheme();
         //   this.Appearance.FocusedRow.BackColor = Theme.HighlightBackground;
         this.Appearance.SelectedRow.Options.UseBackColor = true;
            this.Appearance.SelectedRow.BackColor = Theme.HighlightBackground;  
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
        

        public void ApplyTheme()
        {
            // Настройка выделенной строки
            this.Appearance.FocusedRow.BackColor = Theme.HighlightBackground;
            //this.Appearance.FocusedRow.ForeColor = Theme.HighlightText;

            // Общий стиль шрифта
            this.Appearance.Row.Font = Theme.DefaultFont;

            //// Настройка общего фона
            //this.Appearance.Empty.BackColor = Theme.GridBackground;
            //this.Appearance.Row.BackColor = Theme.GridRowBackground;
            //this.Appearance.Row.ForeColor = Theme.GridTextColor;

            // Настройка выделения ячеек
            this.Appearance.FocusedCell.BackColor = Theme.HighlightBackground;
           // this.Appearance.SelectedRow.ForeColor = Theme.HighlightText;
        }
    }
    //Класс-наследник для Label
    public class CustomLabel : System.Windows.Forms.Label
    {
        public CustomLabel()
        {
            //this.BackColor = Theme.TextBoxBackground;
            this.BackColor = Color.Transparent;
            this.ForeColor = Theme.LabelText;
            this.Font = Theme.DefaultFont;

        }
    }
    //Класс-наследник для MaskedTextBox
    public class CustomMaskedTextBox : MaskedTextBox
    {
        public CustomMaskedTextBox()
        {
            this.BackColor = Theme.TextBoxBackground;
            this.ForeColor = Theme.TextBoxText;
            this.Font = Theme.DefaultFont;
        }

    }
    //Класс-наследник для CheckBox
    public class CustomCheckBox : CheckBox
    {
        public CustomCheckBox()
        {
            //this.BackColor = Theme.TextBoxBackground;
            this.ForeColor = Theme.TextBoxText;
            this.Font = Theme.DefaultFont;
        }

    }
    // Класс для формы с использованием базовых компонентов
    public class CustomForm : Form
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            // Создание градиента
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect, Theme.GradientStartColor, // Начальный цвет
                Theme.GradientEndColor, // Конечный цвет
                LinearGradientMode.ForwardDiagonal))
            {
                // Заливка области градиентом
                e.Graphics.FillRectangle(brush, rect);
            }
        }
        protected System.Data.DataTable ShowRelatedData(string _serv, string query)
        //System.Windows.Forms.BindingSource bsource
        {
            System.Data.DataTable dT = new System.Data.DataTable();
            try
            {
                string _connStr = "";
                switch (_serv.ToLower())
                {
                    case "ace": _connStr = Properties.Settings.Default.ACEConnectionString; break;
                    case "ace_test": _connStr = Properties.Settings.Default.ACEtestConnectionString; break;
                    case "oms": _connStr = Properties.Settings.Default.OMSConnectionString; break;
                    case "global": _connStr = Properties.Settings.Default.GlobalConnectionString; break;
                }
                string connectionString = _connStr;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();

                    connection.Open();
                    //using (SqlTransaction transaction = connection.BeginTransaction()) // Используем транзакцию
                    //{
                    //using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        //   SqlCommand command = new SqlCommand(query, connection);
                        //CommandType commandType = command.CommandType;

                        adapter.SelectCommand = command;
                        adapter.Fill(dT);
                        //bsource.DataSource = dT;
                    }

                    // transaction.Commit(); // Подтверждаем транзакцию

                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dT;
        }
    }
}