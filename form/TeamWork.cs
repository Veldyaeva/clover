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
namespace SewingProduction.form
{
    public partial class TeamWork : SP_form
    {
        public IDatabaseManager _database;
        string connectionString = Properties.Settings.Default.ACEConnectionString;
        // Источник данных для привязки данных к DataGridView
         private BindingSource bindingSource1;

        public TeamWork()
        {
            InitializeComponent();
            Load += TeamWork_Load; // Подключаем обработчик события Load
        }

        private void TeamWork_Load(object sender, EventArgs e)
        {
            LoadData();
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

                            //DataTable normRasz = new DataTable();
                            //string queryArt2 = "SELECT * FROM [ACE].[dbo].[norm_rasz]";
                            //using (SqlCommand commandArt2 = new SqlCommand(queryArt2, connection, transaction))
                            //{
                            //    adapter.SelectCommand = commandArt2;
                            //    adapter.Fill(normRasz);
                            //    gridControl1.DataSource = normRasz; // Привязываем напрямую
                            //}

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

 
        //////public TeamWork()
        //////{
        //////    InitializeComponent();
        //////    //Load += TeamWork_Load;

        //////    // Инициализация источника данных
        //////    bindingSource1 = new BindingSource();

        //////    sqlDataSource1.FillAsync();
        //////}

        //////private void xtraTabPage1_Paint(object sender, PaintEventArgs e)
        //////{

        //////}

        //////private void TeamWork_Load(object sender, EventArgs e)
        //////{
        //////    LoadData();

        //////}

        //////private void LoadData(string searchName = "")
        //////{
        //////    try
        //////    {


        //////        using (SqlConnection connection = new SqlConnection(connectionString))
        //////        {
        //////            connection.Open();
        //////            SqlDataAdapter adapterArtNormN = new SqlDataAdapter();
        //////            DataTable artNormN = new DataTable();
        //////            string queryArt = $"SELECT * FROM [ACE].[dbo].[Art_norm_n]";
        //////            SqlCommand commandArt = new SqlCommand(queryArt, connection);
        //////            adapterArtNormN.SelectCommand = commandArt;
        //////            adapterArtNormN.Fill(artNormN);
        //////            gridControl2.DataSource = artNormN;
        //////        }
        //////        using (SqlConnection connection = new SqlConnection(connectionString))
        //////        {
        //////            connection.Open();
        //////            SqlDataAdapter adapterArtNormN = new SqlDataAdapter();
        //////            DataTable artNormN = new DataTable();
        //////            string queryArt = $"SELECT * FROM [ACE].[dbo].[norm_rasz]";
        //////            SqlCommand commandArt = new SqlCommand(queryArt, connection);
        //////            adapterArtNormN.SelectCommand = commandArt;
        //////            adapterArtNormN.Fill(artNormN);
        //////            gridControl1.DataSource = artNormN;
        //////        }

        //////    }
        //////    catch (Exception ex)
        //////    {
        //////        MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //////    }

        //////}
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
            string query = $"SELECT kod, grup, articul, mod FROM [ACE].[dbo].sp_articul WHERE [ACE].[dbo].[sp_articul].kod = '{Id}'";
            ShowRelatedData(Id, query, gridControl6);
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
