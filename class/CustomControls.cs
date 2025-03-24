using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting.Native.WebClientUIControl;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static SewingProduction.ThemeManager;
using System.Data;
using DevExpress.XtraRichEdit.Model;
using DevExpress.XtraEditors.Controls;
using System.Diagnostics;

namespace SewingProduction
{
    // Базовый класс для общих свойств компонентов
    public abstract class BaseComponent : Control
    {
        // Метод для применения общих свойств к компоненту
        public virtual void ApplyBaseProperties(Control control)
        {
            // control.Font = ThemeManager.ActiveTheme.DefaultFont;
        }
    }

    /// <summary>
    /// Кастомная кнопка
    /// </summary>
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

        private GraphicsPath _graphicsPath;

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
            _graphicsPath = CreateRoundedRectanglePath(ClientRectangle, Theme.ButtonRoundRadius); // Радиус скругления
            Region = new Region(_graphicsPath);
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

    /// <summary>
    /// Класс-наследник для кнопки "Отмена"
    /// </summary>
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



    /// <summary>
    /// Класс-наследник для CheckBox
    /// </summary>
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

    /// <summary>
    /// Кастомное текстовое поле
    /// </summary>
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

    /// <summary>
    /// Класс-наследник для ComboBox
    /// </summary>
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

    /// <summary>
    ///    //Класс-наследник для Label
    /// </summary>
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
            this.ForeColor = ThemeManager.ActiveTheme.LabelTextColor;
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

    /// <summary>
    /// Класс-наследник для MaskedTextBox
    /// </summary>
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
            //// Подписываемся на изменения темы
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged; // Подписка на изменение темы

            //// Подписываемся на изменения темы
            //Theme.ThemeChanged += OnThemeChanged;
        }

        public void ApplyTheme()
        {
            // Применяем тему к GridControl (например, цвет фона)
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BackColor = ActiveTheme.GridBackground;

            //// Применяем тему к связанному GridView
            //CustomView.ApplyTheme();
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
    /// <summary>
    /// Кастомная форма с градиентным фоном
    /// </summary>
    public class CustomForm : Form
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            // Создание градиента
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect,
                ThemeManager.ActiveTheme.GradientStartColor,
                ThemeManager.ActiveTheme.GradientEndColor,
                LinearGradientMode.ForwardDiagonal))
            {
                // Заливка области градиентом
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



    }

    #region CommonFunctions
    /// <summary>
    /// Класс общих функций
    /// </summary>
    public static class CommonFunctions
    {


        /// <summary>
        /// получает значение в ячейке, либо, при его отсутствии присваивает значение по умолчанию
        /// </summary>
        /// <typeparam name="T">Тип возвращаемого значения</typeparam>
        /// <param name="view">таблица</param>
        /// <param name="rowHandle">идентификатор выбранной строки</param>
        /// <param name="fieldName">название поля</param>
        /// <param name="defaultValue">задаваемое значение по умолчанию</param>
        /// <returns></returns>
        public static T GetRowCellValueOrDefault<T>(GridView view, int rowHandle, string fieldName, T defaultValue = default)
        {
            System.Data.DataTable dT = new System.Data.DataTable();
            try
            {
                object value = view.GetRowCellValue(rowHandle, fieldName);
                if (value == DBNull.Value || value == null)
                {
                    return defaultValue;
                }
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception ex)
            {
                // Логирование ошибки или другое действие
                //  MessageBox.Show($"Ошибка при получении значения поля '{fieldName}': {ex.Message}");
                return defaultValue;
            }
        }
    }


}
#endregion



#region DbHelper
/// <summary>
/// класс для работы с БД
/// </summary>
public class DatabaseHelper
{
    private readonly string _connectionString;

    public DatabaseHelper(string _serv)//(string connectionString)
    {
        //_connectionString = connectionString;
        switch (_serv.ToLower())
        {
            case "ace": _connectionString = SewingProduction.Properties.Settings.Default.ACEConnectionString; break;
            case "oms": _connectionString = SewingProduction.Properties.Settings.Default.OMSConnectionString; break;
            case "global": _connectionString = SewingProduction.Properties.Settings.Default.GlobalConnectionString; break;
        }

    }

    //public static System.Data.DataTable ShowRelatedData(string _serv, string query)
    //{
    //    //System.Data.DataTable dT = new System.Data.DataTable();
    //    try
    //    {
    //        //    string _connStr = "";
    //        //    switch (_serv.ToLower())
    //        //    {
    //        //        case "ace": _connStr = Properties.Settings.Default.ACEConnectionString; break;
    //        //        case "oms": _connStr = Properties.Settings.Default.OMSConnectionString; break;
    //        //        case "global": _connStr = Properties.Settings.Default.GlobalConnectionString; break;
    //        //    }
    //        //    string connectionString = _connStr;
    //        //    using (SqlConnection connection = new SqlConnection(connectionString))
    //        //    {
    //        //        SqlDataAdapter adapter = new SqlDataAdapter();

    //        //        connection.Open();
    //        //        using (SqlCommand command = new SqlCommand(query, connection))
    //        //        {
    //        //            adapter.SelectCommand = command;
    //        //            adapter.Fill(dT);
    //        //        }
    //        //    }
    //        }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //    }
    //        return ExecuteQuery(query);
        
    //    //return dT;
    //}
    /// <summary>
    /// Выполнение SQL запроса, возвращает dataTable
    /// </summary>
    /// <param name="query">запрос</param>
    /// <param name="parameters">параметры</param>
    /// <returns></returns>
    public DataTable ExecuteQuery(string query, Dictionary<string, object> parameters = null)
    {
            var dt = new DataTable();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
    }

    /// <summary>
    /// Выполнение SQL запроса
    /// </summary>
    /// <param name="query">запрос</param>
    /// <param name="parameters">параметры</param>
    public void ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
                    SqlDataAdapter adapter = new SqlDataAdapter();
            connection.Open();
            using (var command = new SqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }
                command.ExecuteNonQuery();
            }
        }
    }
}
#endregion

#region Logger
public static class Logger
{
    private static readonly string logFilePath = "error_log.json";
    private static readonly object _lock = new object();

    public static void LogError(Exception ex, string context = "")
    {
        var logEntry = new LogEntry
        {
            Timestamp = DateTime.UtcNow.ToString("o"),
            Message = ex.Message,
            StackTrace = ex.StackTrace,
            Context = context
        };

        WriteLog(logEntry);
    }

    public static void LogEvent(string ev, string context = "")
    {
        var logEntry = new LogEntry
        {
            Timestamp = DateTime.UtcNow.ToString("o"),
            Message = ev,
            StackTrace = "",
            Context = context
        };
        WriteLog(logEntry);
    }
    private static void WriteLog(LogEntry logEntry)
    {
        lock (_lock)
        {
            List<LogEntry> logs = new List<LogEntry>();

            // Если файл существует, загружаем предыдущие логи
            if (File.Exists(logFilePath))
            {
                try
                {
                    string existingLogs = File.ReadAllText(logFilePath);
                    logs = JsonConvert.DeserializeObject<List<LogEntry>>(existingLogs) ?? new List<LogEntry>();
                }
                catch (Exception readEx)
                {
                    Console.WriteLine($"Ошибка при чтении логов: {readEx.Message}");
                }
            }

            // Добавляем новый лог
            logs.Add(logEntry);

            try
            {
                // Записываем обновленный список логов в JSON-файл
                File.WriteAllText(logFilePath, JsonConvert.SerializeObject(logs, Formatting.Indented));
            }
            catch (Exception writeEx)
            {
                Console.WriteLine($"Ошибка при записи логов: {writeEx.Message}");
            }
        }
            return dT;
    }
========
>>>>>>>> main:class/Font.cs
}

// Класс для хранения информации об ошибке
public class LogEntry
{
    public string Timestamp { get; set; }
    public string Message { get; set; }
    public string StackTrace { get; set; }
    public string Context { get; set; }
}
#endregion