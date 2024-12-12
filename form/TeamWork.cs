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
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.XtraBars.Customization;
using DevExpress.XtraWaitForm;
using DevExpress.ChartRangeControlClient.Core;
using BindingSource = System.Windows.Forms.BindingSource;
using SewingProduction;

namespace SewingProduction.form
{
    public partial class TeamWork : CustomForm
    {
        // public IDatabaseManager _database;
        string connectionString = Properties.Settings.Default.ACEConnectionString;
        // Источник данных для привязки данных к DataGridView
         private BindingSource bindingSource1;

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
                               // gridControl2.DataSource = artNormN; // Привязываем напрямую
                            }
                            artnormnBindingSource.DataSource = artNormN;
                            gridControl2.DataSource = artnormnBindingSource;// Привязываем через BindingSource
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
                            grid.DataSource = normRasz; // Привязываем напрямую
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

 
        private void LoadData()
        {
            LoadData("");  // Вызов основного метода с пустой строкой для отображения всех данных
        }

        //////    // Инициализация источника данных
        //////    bindingSource1 = new BindingSource();

        private void gridView3_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    GridView view = gridControl2.MainView as GridView;
                    int Id = Convert.ToInt32(view.GetRowCellValue(e.FocusedRowHandle, "annID"));
                    NormRaszUpd(Id);
                    Norm_raskUpd(Id);
                    Norm_kontUpd(Id);
                    Norm_dop_obrUpd(Id);
                    Sp_artUpd(Id);
                    KommentUpd(e);
                }
            }
            catch(Exception ex)
            {
            }
        }

        //////private void xtraTabPage1_Paint(object sender, PaintEventArgs e)
        //////{

        private void KommentUpd(FocusedRowChangedEventArgs e)
        {
            GridView gridView = gridControl2.MainView as GridView;

            if (gridView != null)
            {
                object komment = gridView.GetRowCellValue(e.FocusedRowHandle, "colkomment");
                commentRichTextBox.Text = komment?.ToString() ?? ""; // Обработка null
                string diz = gridView.GetRowCellValue(e.FocusedRowHandle, "coldiz")?.ToString();
                //  string queryDiz = $"SELECT [ACE].[dbo].[fio].fio FROM [ACE].[dbo].fio WHERE [ACE].[dbo].[fio].tab = '{diz}'";
                //         ShowRelatedData(diz, designerComboBox);
                //  designerComboBox.Text = diz?.ToString() ?? "";
                string konstr = gridView.GetRowCellValue(e.FocusedRowHandle, "colconstr")?.ToString();
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
            string query = $"SELECT * FROM norm_kont WHERE [norm_kont].annId = '{Id}'";
            ShowRelatedData(Id, query, gridControl4, normkontBindingSource);
        }

        private void Norm_dop_obrUpd(int Id)
        {
            string query = $"SELECT * FROM norm_dop_obr WHERE [norm_dop_obr].annId = '{Id}'";
            ShowRelatedData(Id, query, gridControl5, normdopobrBindingSource);
        }
        private void Sp_artUpd(int Id)
        {
            string query = $"SELECT SUBSTRING(kod,1,7), grup, articul, mod, kod FROM sp_articul WHERE annID = '{Id}'";
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
            //if (row != null)
            //{
            //    // Строка найдена, получаем индекс строки
            //    int rowIndex = dt.Rows.IndexOf(row);

        }

        private void customGridControl1_Load(object sender, EventArgs e)
        {
         //   ShowArtData("SELECT SUBSTRING(kod, 1, 7) AS kod, grup, articul, mod FROM sp_articul where annId is NULL", customGridControl1);
        }

        //Загрузка таблиц "Артикулы для увязки" и "РТ для увязки"
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


        //Загрузка таблицы "Артикулы для увязки"
        private void customGridControl2_Load(object sender, EventArgs e)
        {
            ShowArtData("SELECT kod, grup, articul, mod, st FROM art_norm_n", customGridControl2);
        }


        //Предварительный архив
        private void customGridControl4_Load(object sender, EventArgs e)
        {
            ShowArtData("SELECT kod, articul FROM art_norm_n", customGridControl4);

            string query = $"SELECT * FROM [ACE].[dbo].norm_rasz WHERE [ACE].[dbo].[norm_rasz].kod = '{Id}'";
            ShowRelatedData(Id, query, gridControl1);
        }

        // не описанные - sec_shv=0 & arh = 0
        private void customButton3_Click(object sender, EventArgs e)
        {
            gridView3.ActiveFilterString = string.Format("[colsec_shv] = '{0}' AND [colarh] = '{1}'", 0, 0);// string.Format("kod LIKE '{0}'", textBox1.Text); // Поиск по полю FieldName, содержащему SearchText

            string query = $"SELECT * FROM [ACE].[dbo].norm_rask WHERE [ACE].[dbo].[norm_rask].kod = '{Id}'";
            ShowRelatedData(Id, query, gridControl3);
        }

        //Отвязка артикула от РТ
        private void customButton7_Click(object sender, EventArgs e)
        {
            // Получение индекса выбранной строки в customGridControl5
            int[] selectedRows = gridView10.GetSelectedRows();

            if (selectedRows.Length > 0)
            {
                foreach (var rowHandle in selectedRows)
                {
                    // Получение ID строки из norm_rasz
                    int sp_articul = Convert.ToInt32(gridView10.GetRowCellValue(rowHandle, "kod"));

                    try
        {
                        // Обнуление annId
                        ResetAnnId(sp_articul);
                        MessageBox.Show("РТ успешно отвязано", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Обновление данных после изменения
                        //
                        customGridControl5.RefreshDataSource();

                        gridView10.RefreshData();
        }
                    catch (Exception ex)
        {
                        MessageBox.Show("Ошибка отвязки РТ: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
        }
            else
        {
                MessageBox.Show("Выберите запись для редактирования.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        }

        //отвязка РТ - обнуление annID в таблице sp_articul
        private void ResetAnnId(int sp_articul)
        {
            string query = "UPDATE sp_articul SET annId = NULL WHERE kod = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", sp_articul);
                    command.ExecuteNonQuery();
                }
            }
    }

        //Загрузка вкладки "текущие работы"
        private void xtraTabControl2_Enter(object sender, EventArgs e)
        {
            loadConnectionTab("SELECT annID, kod, articul, status FROM art_norm_n", artnormnBindingSource1, customGridControl2 );
            loadConnectionTab("Select * from sp_articul where annId is null", sparticulBindingSource1, customGridControl1);

}
        //Загрузка таблиц "Артикулы для увязки" и "РТ для увязки"
        private void loadConnectionTab(string query, BindingSource bindingSource, CustomGridControl gridControl)
        {
            string _query = query;
            BindingSource _bindingSource = bindingSource;
            CustomGridControl _gridControl = gridControl;

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

                            DataTable dt = new DataTable();
                           // string query = "SELECT * FROM [ACE].[dbo].[Art_norm_n]";
                            using (SqlCommand command = new SqlCommand(_query, connection, transaction))
                            {
                                adapter.SelectCommand = command;
                                adapter.Fill(dt);
                                // gridControl2.DataSource = artNormN; // Привязываем напрямую
                            }
                            _bindingSource.DataSource = dt;
                            _gridControl.DataSource = _bindingSource;// Привязываем через BindingSource
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

        //Реализация зависимости таблиц "Артикулы для увязки" и  "norm_rasz"
        private void gridView8_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
    {
            if (e.FocusedRowHandle >= 0)
        {
                GridView view = customGridControl2.MainView as GridView;
                int Id = Convert.ToInt32(view.GetRowCellValue(e.FocusedRowHandle, "annID"));
                string query = $"SELECT * FROM norm_rasz WHERE [norm_rasz].annId = '{Id}'";
                ShowRelatedData(Id, query, customGridControl3, normraszBindingSource1);
            }
        }

        //Увязка выбранных артикулов
        private void customButton4_Click(object sender, EventArgs e)
        {
            // Получение индекса выбранной строки в customGridControl
            int[] selectedArticuls = gridView7.GetSelectedRows();

            if (selectedArticuls.Length > 0)
            {
                foreach (var rowHandle in selectedArticuls)
        {
                    // Получение ID строки из norm_rasz
                    int sp_articul = Convert.ToInt32(gridView10.GetRowCellValue(rowHandle, "kod"));
                    int ann = Convert.ToInt32(gridView10.GetRowCellValue(rowHandle, "kod"));

            try
            {
                        // Запись annId
                     //   UpdateAnnId(sp_articul, ann);
                        MessageBox.Show("annId успешно обнулен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Обновление данных после изменения
                        //
                        customGridControl5.RefreshDataSource();

                        gridView10.RefreshData();
            }
            catch (Exception ex)
            {
                        MessageBox.Show("Ошибка при обнулении annId: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для редактирования.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void UpdateAnnId(int sp_articul, int ann)
        {
            string query = "UPDATE sp_articul SET annId = @annId WHERE kod = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", sp_articul);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void customButton5_Click(object sender, EventArgs e)
        {

        }
    }

}


