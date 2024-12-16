using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Configuration;
using DevExpress.ChartRangeControlClient.Core;
using DevExpress.Xpo;
using SewingProduction;
using BindingSource = System.Windows.Forms.BindingSource;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid;
using System.Data;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils;
using SewingProduction;
using System.Drawing;
using Button = SewingProduction.form.TeamWork.Button;
namespace SewingProduction.form
{
    public partial class TeamWork : SP_form
    {
        public IDatabaseManager _database;
        string connectionString = Properties.Settings.Default.ACEConnectionString;

        public TeamWork()
        {
            InitializeComponent();
         //   InitializeComponents();
            Load += TeamWork_Load; // Подключаем обработчик события Load
        }
        private void InitializeComponents()
        {
            // Создание TextBox
            TextBox textbox = new TextBox
            {
                Text = "Введите текст...",
                ForeColor = System.Drawing.Color.Blue,
                Font = new System.Drawing.Font("Arial", 14),
                Location = new System.Drawing.Point(50, 50), // позиция на форме
                Width = 200
            };

            // Создание Button
            //Button Mybutton = new Button
            //{
            //    Text = "Нажмите",
            //    ForeColor = System.Drawing.Color.Red,
            //    Font = new System.Drawing.Font("Arial", 14),
            //    Location = new System.Drawing.Point(50, 100), // позиция на форме
            //    Width = 100
            //};

            //// Добавление обработчика события для Button
            //Mybutton.Click += (sender, args) => MessageBox.Show("Кнопка нажата!");

            //// Добавление компонентов на форму
            //this.Controls.Add(textbox);
            //this.Controls.Add(Mybutton);
        }
        private void TeamWork_Load(object sender, EventArgs e)
        {

            // Пример использования
            Textbox textbox = new Textbox(textColor: "blue", textSize: 14, placeholder: "Введите имя");
            textbox.DisplayTextboxInfo();
            

            Button button = new Button(textColor: "red", textSize: 16, label: "Отправить");
            button.DisplayButtonInfo();
//            LoadData();
        }
        public class Component
        {
            public string TextColor { get; set; }
            public int TextSize { get; set; }

            // Конструктор по умолчанию
            public Component(string textColor = "black", int textSize = 12)
            {
                TextColor = textColor;
                TextSize = textSize;
            }

            public void DisplayInfo()
            {
                Console.WriteLine($"Text Color: {TextColor}, Text Size: {TextSize}");
            }
        }

        // Класс Textbox, наследующий Component
        public class Textbox : Component
        {
            public string Placeholder { get; set; }

            public Textbox(string textColor = "black", int textSize = 12, string placeholder = "Enter text")
                : base(textColor, textSize)
            {
                Placeholder = placeholder;
            }

            public void DisplayTextboxInfo()
            {
                Console.WriteLine($"Textbox - Placeholder: {Placeholder}, Text Color: {TextColor}, Text Size: {TextSize}");
            }
        }
        public class Button : Component
        {
            internal string Text;
            internal Color ForeColor;
            internal Font Font;
            internal Point Location;

            public string Label { get; set; }
            public int Width { get; internal set; }
            public Func<object, object, DialogResult> Click { get; internal set; }

            public Button(string textColor = "black", int textSize = 12, string label = "Click Me")
                : base(textColor, textSize)
            {
                Label = label;
            }

            public void DisplayButtonInfo()
            {
                Console.WriteLine($"Button - Label: {Label}, Text Color: {TextColor}, Text Size: {TextSize}");
            }
        }

        private void LoadData(string searchName = "")
        {



            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction()) // Используем транзакцию
                    {
                        try
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter();

                            DataTable artNormN = new DataTable();
                            string queryArt1 = "SELECT * FROM [ACE].[dbo].[Art_norm_n]";
                            using (SqlCommand commandArt1 = new SqlCommand(queryArt1, connection, transaction))
                            {
                                adapter.SelectCommand = commandArt1;
                                adapter.Fill(artNormN);
                                gridControl2.DataSource = artNormN; // Привязываем напрямую
                            }

                            transaction.Commit(); // Подтверждаем транзакцию
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback(); // Отменяем транзакцию при ошибке
                            throw new Exception("Ошибка в транзакции: " + ex.Message, ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void ShowRelatedData(int artNormNId, string query, GridControl grid)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();

                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction()) // Используем транзакцию
                    {
                        DataTable normRasz = new DataTable();
                      
                        using (SqlCommand command = new SqlCommand(query, connection, transaction))
                        {
                            adapter.SelectCommand = command;
                            adapter.Fill(normRasz);
                            grid.DataSource = normRasz; // Привязываем напрямую
                        }

                        transaction.Commit(); // Подтверждаем транзакцию

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadData()
        {
            LoadData("");  // Вызов основного метода с пустой строкой для отображения всех данных
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (row != null)
            //{
            //    // Строка найдена, получаем индекс строки
            //    int rowIndex = dt.Rows.IndexOf(row);

            //    // Получаем индекс столбца (замените "ColumnName" на имя вашего столбца)
            //    int columnIndex = dt.Columns.IndexOf("ColumnName");

            //    // Выделяем ячейку (после привязки данных к GridView)
            //    GridView.Rows[rowIndex].Cells[columnIndex].BackColor = Color.Yellow;
            //}

        }



        private void gridView3_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                GridView view = gridControl2.MainView as GridView;
                int Id = Convert.ToInt32(view.GetRowCellValue(e.FocusedRowHandle, "kod"));
                normRaszUpd(Id);
                norm_raskUpd(Id);
                norm_kontUpd(Id);
                norm_dop_obrUpd(Id);
                sp_artUpd(Id);
                kommentUpd(e);
            }
        }


        private void kommentUpd(FocusedRowChangedEventArgs e)
        {
            GridView gridView = gridControl2.MainView as GridView;

            //GridView view = sender as GridView;
            if (gridView != null)
            {
                // Замените "YourColumnName" на имя столбца и textBox1 на имя вашего поля
                object komment = gridView.GetRowCellValue(e.FocusedRowHandle, "komment");
                commentRichTextBox.Text = komment?.ToString() ?? ""; // Обработка null
                object diz = gridView.GetRowCellValue(e.FocusedRowHandle, "diz");
                designerComboBox.Text = diz?.ToString() ?? "";
                object konstr = gridView.GetRowCellValue(e.FocusedRowHandle, "constr");
                constructorComboBox.Text = konstr?.ToString() ?? string.Empty;
            }
        }
        private void normRaszUpd(int Id)
        {
            string query = $"SELECT * FROM [ACE].[dbo].norm_rasz WHERE [ACE].[dbo].[norm_rasz].kod = '{Id}'";
            ShowRelatedData(Id, query, gridControl1);
        }
        private void norm_raskUpd(int Id)
        {

            string query = $"SELECT * FROM [ACE].[dbo].norm_rask WHERE [ACE].[dbo].[norm_rask].kod = '{Id}'";
            ShowRelatedData(Id, query, gridControl3);
        }
        private void norm_kontUpd(int Id)
        {
            string query = $"SELECT * FROM [ACE].[dbo].norm_kont WHERE [ACE].[dbo].[norm_kont].kod = '{Id}'";
            ShowRelatedData(Id, query, gridControl4);
        }

        private void norm_dop_obrUpd(int Id)
        {
            string query = $"SELECT * FROM [ACE].[dbo].norm_dop_obr WHERE [ACE].[dbo].[norm_dop_obr].kod = '{Id}'";
            ShowRelatedData(Id, query, gridControl5);
        }
        private void sp_artUpd(int Id)
        {
            string query = $"SELECT SUBSTRING(kod,1,7), grup, articul, mod FROM [ACE].[dbo].sp_articul WHERE SUBSTRING(kod ,1,7)= '{Id}'";
            ShowRelatedData(Id, query, gridControl6);
        }

        private void doubleBtn_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            //form.MdiParent = this;
            form.Show();
        }
    }

}


#region new
namespace SewingProduction.form
{
    public interface IDatabaseManager : IDisposable
    {
        List<ArtNormN> GetAllData();
        //List<ArtNormN> SearchData(string searchName);
        //void SaveData(List<ArtNormN> updatedData);
    }

    public class DatabaseManager : IDatabaseManager
    {
        private readonly DbContext _context; // Используем DbContext напрямую

        public DatabaseManager(DbContext context) // Инъекция зависимостей
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public DbSet<ArtNormN> ArtNormNs => _context.Set<ArtNormN>(); // Более чистый доступ к DbSet

        public List<ArtNormN> GetAllData()
        {
            try
            {
                return ArtNormNs.ToList(); // Используем свойство ArtNormNs
            }
            catch (Exception ex)
            {
                // Запись в лог или другое обработка исключения
                Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
                return new List<ArtNormN>(); // Возвращаем пустой список в случае ошибки
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
#endregion
