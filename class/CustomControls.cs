using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static SewingProduction.ThemeManager;

namespace SewingProduction
{
    // Базовый класс для общих свойств компонентов
    public abstract class BaseComponent : Control
    {
        public virtual void ApplyBaseProperties(Control control)
        {
           // control.Font = ThemeManager.ActiveTheme.DefaultFont;
        }
    }

    // Кастомная кнопка
    public class CustomButton : Button
    {
        public CustomButton()
        {
            ApplyTheme();
            ThemeChanged += OnThemeChanged; // Подписка на изменение темы
        }
        public Button Btn { get; set; }
        public Color ComponentBackColor { get; set; }
        public Color ComponentFontColor { get; set; }
        public Size ComponentSize { get; set; }


        public void ApplyTheme()
        {
            BackColor = ThemeManager.ActiveTheme.ButtonBackground;
            ForeColor = ThemeManager.ActiveTheme.ButtonTextColor;
            Font = ThemeManager.SharedSettings.DefaultFont;
            FlatStyle = FlatStyle.Standard;
            FlatAppearance.BorderSize = 1;
            Height = ThemeManager.SharedSettings.ButtonHeight;
        }

        private void OnThemeChanged()
        {
            ApplyTheme();
         //   Invalidate(); // Перерисовка кнопки
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            var path = CreateRoundedRectanglePath(ClientRectangle, SharedSettings.ButtonRoundRadius);
            Region = new Region(path);
        }

        private GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }
    }

    public class CustomOkButton : CustomButton
    {
        public CustomOkButton()
        {
            this.Text = Text;
            //this.Text = Theme.OkText;//"Ок"; // Текст кнопки
            this.Click += OnOkButtonClick; // Обработчик события Click
            this.BackColor = ThemeManager.ActiveTheme.OkButtonBackground; // Цвет кнопки
            this.ForeColor = ActiveTheme.OkButtonTextColor; // Цвет текста
            this.Text = ActiveTheme.OkButtonText;
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
            Text = "Отмена"; // Текст кнопки
            Click += OnCancelButtonClick; // Обработчик события Click
            BackColor = ActiveTheme.CancelButtonBackground; // Цвет кнопки
            ForeColor = ActiveTheme.CancelButtonTextColor; // Цвет текста
            Text = ActiveTheme.CancelButtonText;
        }

        // Событие при нажатии на кнопку "Отмена"
        private void OnCancelButtonClick(object sender, EventArgs e)
        {
             //  this.FindForm()?.Close(); // Закрыть текущую форму
        }
    }



    //Класс-наследник для CheckBox
    public class CustomCheckBox : CheckBox
    {
        public CustomCheckBox()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged; // Подписка на изменение темы
        }

        public void ApplyTheme()
        {

            this.ForeColor = ActiveTheme.TextBoxText;
            this.Font = SharedSettings.DefaultFont;
        }

        private void OnThemeChanged()
        {
            ApplyTheme();
            Invalidate(); // Перерисовка текстового поля
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }

    }

    // Кастомное текстовое поле
    public class CustomTextBox : TextBox
    {
        public CustomTextBox()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged; // Подписка на изменение темы
        }

        public void ApplyTheme()
        {
            this.BackColor = ThemeManager.ActiveTheme.TextBoxBackground;
            this.ForeColor = ThemeManager.ActiveTheme.TextBoxText;
            this.Font = ThemeManager.SharedSettings.DefaultFont;
        }

        private void OnThemeChanged()
        {
            ApplyTheme();
            Invalidate(); // Перерисовка текстового поля
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }
    }

    //Класс-наследник для ComboBox
    public class CustomComboBox : ComboBox
    {
        public CustomComboBox()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged; // Подписка на изменение темы
        }
        public void ApplyTheme()
        {
            this.BackColor = ThemeManager.ActiveTheme.TextBoxBackground;
            this.ForeColor = ThemeManager.ActiveTheme.TextBoxText;
            this.Font = ThemeManager.SharedSettings.DefaultFont;
        }
        private void OnThemeChanged()
        {
            ApplyTheme();
            Invalidate(); // Перерисовка текстового поля
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }

    }

    //    //Класс-наследник для Label
    public class CustomLabel : System.Windows.Forms.Label
    {
        public CustomLabel()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged; // Подписка на изменение темы

        }
        public void ApplyTheme()
        {
            this.BackColor = Color.Transparent;
            this.ForeColor = ThemeManager.ActiveTheme.LabelText;
            this.Font = ThemeManager.SharedSettings.DefaultFont;
        }
        private void OnThemeChanged()
        {
            ApplyTheme();
            Invalidate(); // Перерисовка текстового поля
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }

    }

    //Класс-наследник для MaskedTextBox
    public class CustomMaskedTextBox : MaskedTextBox
    {
        public CustomMaskedTextBox()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged; // Подписка на изменение темы

        }
        public void ApplyTheme()
        {
            this.BackColor = ActiveTheme.TextBoxBackground;
            this.ForeColor = ActiveTheme.TextBoxText;
            this.Font = SharedSettings.DefaultFont;
        }
        private void OnThemeChanged()
        {
            ApplyTheme();
            Invalidate(); // Перерисовка текстового поля
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }


    }

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

        public void ApplyTheme()
        {
            // Применяем тему к GridControl (например, цвет фона)
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            //this.BackColor = ActiveTheme.GridBackground;

            //// Применяем тему к связанному GridView
            //CustomView.ApplyTheme();
        }

        private void OnThemeChanged()
        {
            ApplyTheme();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //  Theme.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }
    }

    public class CustomGridView : GridView
    {
        public CustomGridView()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged; // Подписка на изменение темы

        }
        private void ApplyTheme()
        {
        }
        private void OnThemeChanged()
        {
            ApplyTheme();
            Invalidate(); // Перерисовка текстового поля
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }


    }
    public class CustomGroupBox : GroupBox
    {
        private Color _borderColor = Color.Black; // Цвет обводки по умолчанию
        private int _borderThickness = 1;       // Толщина обводки по умолчанию

        // Свойство для установки цвета обводки
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        // Свойство для установки толщины обводки
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
                var rect = new Rectangle(ClientRectangle.X , ClientRectangle.Y, ClientRectangle.Width - _borderThickness, ClientRectangle.Height - _borderThickness);
                rect.X += _borderThickness / 2 ;
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

    }
    // Класс для формы с использованием базовых компонентов
    public class CustomForm : Form
    {
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
        public void UpdateTheme(Control control)
        {
            foreach (Control child in control.Controls)
            {
                if (child is CustomButton button)
                {
                    button.ApplyTheme();
                }
                else if (child is CustomTextBox textBox)
                {
                    textBox.ApplyTheme();
                }
                else if (child is CustomLabel label)
                {
                    label.ApplyTheme();
                }
                else if (child is CustomGridControl gridControl)
                {
                    gridControl.ApplyTheme();
                }
                else if (child is CustomCheckBox check)
                {
                    check.ApplyTheme();
                }
                else if (child is CustomComboBox combo)
                {
                    combo.ApplyTheme();
                }
                else if (child is CustomMaskedTextBox maskedTextBox)
                {
                    maskedTextBox.ApplyTheme();
                }
            
                else if (child.HasChildren)
                {
                    UpdateTheme(child); // Рекурсивно обновляем тему для вложенных элементов
                }
            }
        }



        #region
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
        // Заполнение комбобоксов:
        protected void comboOneTableItems(System.Windows.Forms.ComboBox comboBox, string query, string nameColumn, SqlConnection connection, bool allOrOne)
        {
            /* 
             * comboBox: сам комбобокс
             * query: текст запроса
             * connection: подключение
             * allOrOne: 
             * true - для заполнения комбобокса
             * false - для отображения значения
            */
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
            //Создаем в памяти таблицу:
            System.Data.DataTable tableList = new System.Data.DataTable();
            //Добавляем ответ сервера в таблицу:
            dataAdapter.Fill(tableList);
            if (allOrOne)
            {
                comboBox.Items.Clear();
                //Загрузка в комбобокс:
                foreach (DataRow row in tableList.Rows)
                {
                    comboBox.Items.Add(row[0].ToString());
                }
            }
            else
            {
                comboBox.Text = tableList.Rows.Count > 0 ? tableList.Rows[0][nameColumn].ToString() : "";
            }
        }
        //protected void ShowRelatedComboBox(CustomComboBox comboBox, string _serv, string query, string displayMember, string valueMember)
        //{
        //    try
        //    {
        //        DataTable dataTable = ShowRelatedData(_serv, query);
        //        if (dataTable != null)
        //        {
        //            comboBox.DataSource = dataTable;
        //            comboBox.DisplayMember = displayMember;
        //            comboBox.ValueMember = valueMember;
        //        }
        //        else
        //        {
        //            comboBox.DataSource = null;
        //            comboBox.Items.Clear();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Ошибка при создании ComboBox: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //Универсальный метод для выполнения INSERT, UPDATE, DELETE запросов
        protected void ExecuteNonQuery(string _serv, string query, params SqlParameter[] parameters)
        {
            string connectionString = "";
            switch (_serv.ToLower())
            {
                case "ace": connectionString = Properties.Settings.Default.ACEConnectionString; break;
                case "ace_test": connectionString = Properties.Settings.Default.ACEtestConnectionString; break;
                case "oms": connectionString = Properties.Settings.Default.OMSConnectionString; break;
                case "global": connectionString = Properties.Settings.Default.GlobalConnectionString; break;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null) command.Parameters.AddRange(parameters);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    if (rowsAffected < 0)
                    {
                        MessageBox.Show($"Ошибка при выполнении запроса: {command.CommandText}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw new Exception($"Error executing query: {command.CommandText}");
                    }
                }
            }
        }
    }
    #endregion
}