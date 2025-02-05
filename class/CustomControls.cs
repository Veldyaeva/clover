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
            //// Подписываемся на изменения темы
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged; // Подписка на изменение темы

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

    // Кастомная форма с градиентным фоном
    public class CustomForm : Form
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
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
                rect,
        }

    }
    public static class CommonFunctions
    #region
    {
        public static System.Data.DataTable ShowRelatedData(string _serv, string query)
            }
        #region
        protected System.Data.DataTable ShowRelatedData(string _serv, string query)
    #region
    {
        public static System.Data.DataTable ShowRelatedData(string _serv, string query)

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
                    //}
                }
            }
                    Console.WriteLine($"Ошибка при чтении логов: {readEx.Message}");
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
    }
}

// Класс для хранения информации об ошибке
public class LogEntry
{
    public string Timestamp { get; set; }
    public string Message { get; set; }
    public string StackTrace { get; set; }
    public string Context { get; set; }
}    }
    #endregion
}    public string StackTrace { get; set; }
    public string Context { get; set; }
}    #endregion
}