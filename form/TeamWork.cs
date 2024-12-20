using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using System.Data;
using DevExpress.XtraGrid.Views.Base;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.XtraBars.Customization;
using DevExpress.XtraWaitForm;
using DevExpress.ChartRangeControlClient.Core;


namespace SewingProduction.form
{
    public partial class TeamWork : CustomForm
    {
       // public IDatabaseManager _database;
        string connectionString = Properties.Settings.Default.ACEConnectionString;

        public TeamWork()
        {
            InitializeComponent();
           // Load += TeamWork_Load; // Подключаем обработчик события Load
        }
        private void InitializeComponents()
        {
        }
        private void TeamWork_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "aCEDataSet.norm_rasz". При необходимости она может быть перемещена или удалена.
           // this.norm_raszTableAdapter.Fill(this.aCEDataSet.norm_rasz);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "aCEDataSet.art_norm_n". При необходимости она может быть перемещена или удалена.
           // this.art_norm_nTableAdapter.Fill(this.aCEDataSet.art_norm_n);
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
            //this.sp_articulTableAdapter.Fill(this.aCEDataSet.sp_articul);

        }
        // напрямую через dataSource
        //private void ShowRelatedData(int artNormNId, string query, GridControl grid)
        //{
        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            SqlDataAdapter adapter = new SqlDataAdapter();

        //            connection.Open();
        //            using (SqlTransaction transaction = connection.BeginTransaction()) // Используем транзакцию
        //            {
        //                DataTable normRasz = new DataTable();

        //                using (SqlCommand command = new SqlCommand(query, connection, transaction))
        //                {
        //                    adapter.SelectCommand = command;
        //                    adapter.Fill(normRasz);
        //                    grid.DataSource = normRasz; // Привязываем напрямую
        //                }

        //                transaction.Commit(); // Подтверждаем транзакцию

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //более правильно через BindingSource
        private void ShowRelatedData(int artNormNId, string query, GridControl grid, System.Windows.Forms.BindingSource source)
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
                        }

                        // Привязываем данные к BindingSource
                        source.DataSource = normRasz;

                        // Привязываем BindingSource к GridControl
                        grid.DataSource = source;

                        transaction.Commit(); // Подтверждаем транзакцию
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ShowRelatedData(string query, System.Windows.Forms.TextBox textBox)
        {
            string queryDiz = $"SELECT [ACE].[dbo].[fio].fio FROM [ACE].[dbo].fio WHERE [ACE].[dbo].[fio].tab = '{query}'";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();

                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction()) // Используем транзакцию
                    {
                        DataTable fio = new DataTable();

                        using (SqlCommand command = new SqlCommand(query, connection, transaction))
                        {
                            adapter.SelectCommand = command;
                            adapter.Fill(fio);
                            textBox.Text = fio.Rows[0][0].ToString();
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


        private void gridView3_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                GridView view = gridControl2.MainView as GridView;
                int kod = Convert.ToInt32(view.GetRowCellValue(e.FocusedRowHandle, "kod"));
                int Id = Convert.ToInt32(view.GetRowCellValue(e.FocusedRowHandle, "annID"));
                NormRaszUpd(Id);
                Norm_raskUpd(Id);
                Norm_kontUpd(kod);
                Norm_dop_obrUpd(kod);
                Sp_artUpd(Id);
                KommentUpd(e);
            }
        }


        private void KommentUpd(FocusedRowChangedEventArgs e)
        {
            GridView gridView = gridControl2.MainView as GridView;

            //GridView view = sender as GridView;
            if (gridView != null)
            {
                object komment = gridView.GetRowCellValue(e.FocusedRowHandle, "komment");
                commentRichTextBox.Text = komment?.ToString() ?? ""; // Обработка null
                string diz = gridView.GetRowCellValue(e.FocusedRowHandle, "diz")?.ToString();
              //  string queryDiz = $"SELECT [ACE].[dbo].[fio].fio FROM [ACE].[dbo].fio WHERE [ACE].[dbo].[fio].tab = '{diz}'";
       //         ShowRelatedData(diz, designerComboBox);
              //  designerComboBox.Text = diz?.ToString() ?? "";
                string konstr = gridView.GetRowCellValue(e.FocusedRowHandle, "constr")?.ToString();
     //           ShowRelatedData(konstr, constructorComboBox);
                //constructorComboBox.Text = konstr?.ToString() ?? string.Empty;
            }
        }

        private void TextBoxUpd(string tab, int diz)
        {
            string queryDiz = $"SELECT [ACE].[dbo].[fio].fio FROM [ACE].[dbo].fio WHERE [ACE].[dbo].[fio].tab = '{tab}'";
//            ShowRelatedData(queryDiz);
        }
        private void NormRaszUpd(int Id)
        {
            string query = $"SELECT * FROM norm_rasz WHERE [norm_rasz].annId = '{Id}'";
            ShowRelatedData(Id, query, gridControl1, normraszBindingSource);
        }
        private void Norm_raskUpd(int Id)
        {

            string query = $"SELECT * FROM norm_rask WHERE [norm_rask].annId = '{Id}'";
            ShowRelatedData(Id, query, gridControl3, normraskBindingSource);
        }
        private void Norm_kontUpd(int Id)
        {
            string query = $"SELECT * FROM norm_kont WHERE [norm_kont].kod = '{Id}'";
            ShowRelatedData(Id, query, gridControl4, normkontBindingSource);
        }

        private void Norm_dop_obrUpd(int Id)
        {
            string query = $"SELECT * FROM norm_dop_obr WHERE [norm_dop_obr].kod = '{Id}'";
            ShowRelatedData(Id, query, gridControl5, normdopobrBindingSource);
        }
        private void Sp_artUpd(int Id)
        {
            string query = $"SELECT SUBSTRING(kod,1,7), grup, articul, mod FROM sp_articul WHERE annID = '{Id}'";
            ShowRelatedData(Id, query, customGridControl5, sparticulBindingSource);
        }
         
        private void doubleBtn_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
        }

        private void customButton2_Click(object sender, EventArgs e)
        {
                                                                             
        }

        private void customGridControl1_Load(object sender, EventArgs e)
        {
            ShowArtData("SELECT SUBSTRING(kod, 1, 7) AS kod, grup, articul, mod FROM sp_articul where annId is NULL", customGridControl1);
        }
        private void ShowArtData(string query, GridControl grid)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();

                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction()) // Используем транзакцию
                    {
                        DataTable dt = new DataTable();

                        using (SqlCommand command = new SqlCommand(query, connection, transaction))
                        {
                            adapter.SelectCommand = command;
                            adapter.Fill(dt);
                            grid.DataSource = dt; // Привязываем напрямую
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

  

        private void customGridControl2_Load(object sender, EventArgs e)
        {
            ShowArtData("SELECT kod, grup, articul, mod, st FROM art_norm_n", customGridControl2);
        }

        

        private void customGridControl4_Load(object sender, EventArgs e)
        {
            ShowArtData("SELECT kod, grup, articul, mod FROM norm_rasz", customGridControl4);

        }

        private void customButton3_Click(object sender, EventArgs e)
        {
            gridView3.ActiveFilterString = string.Format("kod LIKE '{0}'", textBox1.Text); // Поиск по полю FieldName, содержащему SearchText

        }

        private void customButton7_Click(object sender, EventArgs e)
        {
            // gridView10.FocusedRowHandle
            // Получение индекса выбранной строки
            int[] selectedRows = gridView10.GetSelectedRows();

            if (selectedRows.Length > 0)
            {
                // Получение данных выбранной строки
                var rowData = gridView10.GetRow(selectedRows[0]) as DataView;
                if (rowData != null)
                {
                    // Обновление данных после редактирования
                    //                    UpdateDatabase(rowData);
                    //rowData.annId = null;
                    gridControl1.RefreshDataSource();

                    //  }
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для редактирования.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //if (editForm.ShowDialog() == DialogResult.OK)
            //{
           // }
        }
        //private void UpdateDatabase(GridView data)
        //{
        //    using (var context = new YourDbContext())
        //    {
        //        var entry = context.Entry(data);
        //        if (entry.State == EntityState.Detached)
        //        {
        //            context.YourDataSet.Attach(data);
        //        }
        //        entry.State = EntityState.Modified;
        //        context.SaveChanges();
        //    }
        //}
    }

}

