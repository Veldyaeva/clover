//using System.Collections.Generic;
//using System.Data.SqlClient;
//using DataTable = System.Data.DataTable;
//using System.Threading.Tasks;
//using DevExpress.XtraGrid.Views.Grid;
//using System.Windows.Forms;
//using System;
//using System.Linq;
//using DevExpress.XtraGrid;
//using System.Data;
//using DevExpress.XtraGrid.Views.Base;
//using System.Drawing;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using DevExpress.DataProcessing.InMemoryDataProcessor;
//using DevExpress.XtraBars.Customization;
//using DevExpress.XtraWaitForm;
//using DevExpress.ChartRangeControlClient.Core;
//using BindingSource = System.Windows.Forms.BindingSource;
//using SewingProduction;
//using DevExpress.Xpo.DB.Helpers;
//using DevExpress.Utils.Gesture;
//using DevExpress.DataAccess.Native.Data;
//using DevExpress.DataAccess.Sql;
//using DevExpress.XtraEditors;
//using DevExpress.XtraVerticalGrid;
//using DevExpress.PivotGrid.QueryMode;


////namespace SewingProduction.form
////{
////    public partial class TeamWork : CustomForm
////    {
////        // public IDatabaseManager _database;
////        string connectionString = Properties.Settings.Default.ACEConnectionString;

////        public TeamWork()
////        {
////            InitializeComponent();
////            // Load += TeamWork_Load; // Подключаем обработчик события Load
////        }
////        private void InitializeComponents()
////        {
////        }
////        private void TeamWork_Load(object sender, EventArgs e)
////        {
////            // TODO: данная строка кода позволяет загрузить данные в таблицу "aCEDataSet.norm_rasz". При необходимости она может быть перемещена или удалена.
////            // this.norm_raszTableAdapter.Fill(this.aCEDataSet.norm_rasz);
////            // TODO: данная строка кода позволяет загрузить данные в таблицу "aCEDataSet.art_norm_n". При необходимости она может быть перемещена или удалена.
////            // this.art_norm_nTableAdapter.Fill(this.aCEDataSet.art_norm_n);
////            LoadData();

////            //  gridView3_FocusedRowChanged(sender, e.);

////        }

////        private void LoadData(string searchName = "")
////        {
////            try
////            {
////                using (SqlConnection connection = new SqlConnection(connectionString))
////                {
////                    connection.Open();
////                    using (SqlTransaction transaction = connection.BeginTransaction()) // Используем транзакцию
////                    {
////                        try
////                        {
////                            SqlDataAdapter adapter = new SqlDataAdapter();

////                            DataTable artNormN = new DataTable();
////                            string queryArt1 = "SELECT * FROM [ACE].[dbo].[Art_norm_n]";
////                            using (SqlCommand commandArt1 = new SqlCommand(queryArt1, connection, transaction))
////                            {
////                                adapter.SelectCommand = commandArt1;
////                                adapter.Fill(artNormN);
////                                // gridControl2.DataSource = artNormN; // Привязываем напрямую
////                            }
////                            artnormnBindingSource.DataSource = artNormN;
////                            gridControl2.DataSource = artnormnBindingSource;// Привязываем через BindingSource
////                            transaction.Commit(); // Подтверждаем транзакцию
////                        }
////                        catch (Exception ex)
////                        {
////                            transaction.Rollback(); // Отменяем транзакцию при ошибке
////                            throw new Exception("Ошибка в транзакции: " + ex.Message, ex);
////                        }
////                    }
////                }
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
////            }
////            //this.sp_articulTableAdapter.Fill(this.aCEDataSet.sp_articul);

////        }

////        //более правильно через BindingSource
////        private void ShowRelatedData(string query, GridControl grid, BindingSource source)
////        {
////            try
////            {
////                using (SqlConnection connection = new SqlConnection(connectionString))
////                {
////                    SqlDataAdapter adapter = new SqlDataAdapter();
////                    connection.Open();

////                    using (SqlTransaction transaction = connection.BeginTransaction()) // Используем транзакцию
////                    {
////                        DataTable normRasz = new DataTable();
////                        using (SqlCommand command = new SqlCommand(query, connection, transaction))
////                        {
////                            adapter.SelectCommand = command;
////                            adapter.Fill(normRasz);
////                        }

////                        // Привязываем данные к BindingSource
////                        source.DataSource = normRasz;

////                        // Привязываем BindingSource к GridControl
////                        grid.DataSource = source;

////                        transaction.Commit(); // Подтверждаем транзакцию
////                    }
////                }
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
////            }
////        }


////        private void LoadData()
////        {
////            LoadData("");  // Вызов основного метода с пустой строкой для отображения всех данных
////        }


////        private void gridView3_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
////        {
////            try
////            {
////                if (e.FocusedRowHandle >= 0)
////                {
////                    GridView view = gridControl2.MainView as GridView;
////                    int Id = Convert.ToInt32(view.GetRowCellValue(e.FocusedRowHandle, "annID"));
////                    NormRaszUpd(Id);
////                    Norm_raskUpd(Id);
////                    Norm_kontUpd(Id);
////                    Norm_dop_obrUpd(Id);
////                    Sp_artUpd(Id);
////                    KommentUpd(e);
////                }
////            }
////            catch (Exception ex)
////            {
////            }
////        }


////        private void KommentUpd(FocusedRowChangedEventArgs e)
////        {
////            GridView gridView = gridControl2.MainView as GridView;

////            if (gridView != null)
////            {
////                object komment = gridView.GetRowCellValue(e.FocusedRowHandle, "colkomment");
////                commentRichTextBox.Text = komment?.ToString() ?? ""; // Обработка null
////                string diz = gridView.GetRowCellValue(e.FocusedRowHandle, "coldiz")?.ToString();
////                //  string queryDiz = $"SELECT [ACE].[dbo].[fio].fio FROM [ACE].[dbo].fio WHERE [ACE].[dbo].[fio].tab = '{diz}'";
////                //         ShowRelatedData(diz, designerComboBox);
////                //  designerComboBox.Text = diz?.ToString() ?? "";
////                string konstr = gridView.GetRowCellValue(e.FocusedRowHandle, "colconstr")?.ToString();
////                //           ShowRelatedData(konstr, constructorComboBox);
////                //constructorComboBox.Text = konstr?.ToString() ?? string.Empty;
////            }
////        }

////        private void TextBoxUpd(string tab, int diz)
////        {
////            string queryDiz = $"SELECT [ACE].[dbo].[fio].fio FROM [ACE].[dbo].fio WHERE [ACE].[dbo].[fio].tab = '{tab}'";
////            //            ShowRelatedData(queryDiz);
////        }
////        private void NormRaszUpd(int Id)
////        {
////            string query = $"SELECT * FROM norm_rasz WHERE [norm_rasz].annId = '{Id}'";
////            ShowRelatedData(query, gridControl1, normraszBindingSource);
////        }
////        private void Norm_raskUpd(int Id)
////        {

////            string query = $"SELECT * FROM norm_rask WHERE [norm_rask].annId = '{Id}'";
////            ShowRelatedData(query, gridControl3, normraskBindingSource);
////        }
////        private void Norm_kontUpd(int Id)
////        {
////            string query = $"SELECT * FROM norm_kont WHERE [norm_kont].annId = '{Id}'";
////            ShowRelatedData(query, gridControl4, normkontBindingSource);
////        }

////        private void Norm_dop_obrUpd(int Id)
////        {
////            string query = $"SELECT * FROM norm_dop_obr WHERE [norm_dop_obr].annId = '{Id}'";
////            ShowRelatedData(query, gridControl5, normdopobrBindingSource);
////        }
////        private void Sp_artUpd(int Id)
////        {
////            string query = $"SELECT SUBSTRING(kod,1,7), grup, articul, mod, kod FROM sp_articul WHERE annID = '{Id}'";
////            ShowRelatedData(query, customGridControl5, sparticulBindingSource);
////        }

////        private void doubleBtn_Click(object sender, EventArgs e)
////        {

////        }



////        private void customButton2_Click(object sender, EventArgs e)
////        {

////        }

////        private void customGridControl1_Load(object sender, EventArgs e)
////        {
////            //   ShowArtData("SELECT SUBSTRING(kod, 1, 7) AS kod, grup, articul, mod FROM sp_articul where annId is NULL", customGridControl1);
////        }

////        //Загрузка таблиц "Артикулы для увязки" и "РТ для увязки"
////        private void ShowArtData(string query, GridControl grid)
////        {
////            try
////            {
////                using (SqlConnection connection = new SqlConnection(connectionString))
////                {
////                    SqlDataAdapter adapter = new SqlDataAdapter();

////                    connection.Open();
////                    using (SqlTransaction transaction = connection.BeginTransaction()) // Используем транзакцию
////                    {
////                        DataTable dt = new DataTable();

////                        using (SqlCommand command = new SqlCommand(query, connection, transaction))
////                        {
////                            adapter.SelectCommand = command;
////                            adapter.Fill(dt);
////                            grid.DataSource = dt; // Привязываем напрямую
////                        }

////                        transaction.Commit(); // Подтверждаем транзакцию

////                    }
////                }
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
////            }
////        }


////        //Загрузка таблицы "Артикулы для увязки"
////        private void customGridControl2_Load(object sender, EventArgs e)
////        {
////            ShowArtData("SELECT kod, grup, articul, mod, st FROM art_norm_n", customGridControl2);
////        }


////        //Предварительный архив
////        private void customGridControl4_Load(object sender, EventArgs e)
////        {
////            ShowArtData("SELECT kod, articul FROM art_norm_n", customGridControl4);

////        }

////        // не описанные - sec_shv=0 & arh = 0
////        private void customButton3_Click(object sender, EventArgs e)
////        {
////            if (SortBox.Checked)
////            {
////                gridView3.ActiveFilterString = string.Format("[colsec_shv] = '{0}' AND [colarh] = '{1}'", 0, 0);// string.Format("kod LIKE '{0}'", textBox1.Text); // Поиск по полю FieldName, содержащему SearchText
////            }

////        }

////        //Отвязка артикула от РТ
////        private void customButton7_Click(object sender, EventArgs e)
////        {
////            // Получение индекса выбранной строки в customGridControl5
////            int[] selectedRows = gridView10.GetSelectedRows();

////            if (selectedRows.Length > 0)
////            {
////                foreach (var rowHandle in selectedRows)
////                {
////                    // Получение ID строки из norm_rasz
////                    int sp_articul = Convert.ToInt32(gridView10.GetRowCellValue(rowHandle, "kod"));

////                    try
////                    {
////                        // Обнуление annId
////                        ResetAnnId(sp_articul);
////                        MessageBox.Show("РТ успешно отвязано", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

////                        // Обновление данных после изменения
////                        //
////                        customGridControl5.RefreshDataSource();//не обновляется

////                        gridView10.RefreshData();
////                    }
////                    catch (Exception ex)
////                    {
////                        MessageBox.Show("Ошибка отвязки РТ: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                    }
////                }
////            }
////            else
////            {
////                MessageBox.Show("Выберите запись для редактирования.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
////            }

////        }

////        //отвязка РТ - обнуление annID в таблице sp_articul
////        private void ResetAnnId(int sp_articul)
////        {
////            string query = "UPDATE sp_articul SET annId = NULL WHERE kod = @Id";
////            using (SqlConnection connection = new SqlConnection(connectionString))
////            {
////                connection.Open();
////                using (SqlCommand command = new SqlCommand(query, connection))
////                {
////                    command.Parameters.AddWithValue("@Id", sp_articul);
////                    command.ExecuteNonQuery();
////                }
////            }
////        }

////        //Загрузка вкладки "текущие работы"
////        private void xtraTabControl2_Enter(object sender, EventArgs e)
////        {
////            loadConnectionTab("SELECT annID, kod, articul, status FROM art_norm_n", artnormnBindingSource1, customGridControl2);
////            loadConnectionTab("Select * from sp_articul where annId is null", sparticulBindingSource1, customGridControl1);

////        }
////        //Загрузка таблиц "Артикулы для увязки" и "РТ для увязки"
////        private void loadConnectionTab(string query, BindingSource bindingSource, CustomGridControl gridControl)
////        {
////            string _query = query;
////            BindingSource _bindingSource = bindingSource;
////            CustomGridControl _gridControl = gridControl;

////            try
////            {
////                using (SqlConnection connection = new SqlConnection(connectionString))
////                {
////                    connection.Open();
////                    using (SqlTransaction transaction = connection.BeginTransaction()) // Используем транзакцию
////                    {
////                        try
////                        {
////                            SqlDataAdapter adapter = new SqlDataAdapter();

////                            DataTable dt = new DataTable();
////                            // string query = "SELECT * FROM [ACE].[dbo].[Art_norm_n]";
////                            using (SqlCommand command = new SqlCommand(_query, connection, transaction))
////                            {
////                                adapter.SelectCommand = command;
////                                adapter.Fill(dt);
////                                // gridControl2.DataSource = artNormN; // Привязываем напрямую
////                            }
////                            _bindingSource.DataSource = dt;
////                            _gridControl.DataSource = _bindingSource;// Привязываем через BindingSource
////                            transaction.Commit(); // Подтверждаем транзакцию
////                        }
////                        catch (Exception ex)
////                        {
////                            transaction.Rollback(); // Отменяем транзакцию при ошибке
////                            throw new Exception("Ошибка в транзакции: " + ex.Message, ex);
////                        }
////                    }
////                }
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
////            }
////        }

////        //Реализация зависимости таблиц "Артикулы для увязки" и  "norm_rasz"
////        private void gridView8_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
////        {
////            if (e.FocusedRowHandle >= 0)
////            {
////                GridView view = customGridControl2.MainView as GridView;
////                int Id = Convert.ToInt32(view.GetRowCellValue(e.FocusedRowHandle, "annID"));
////                string query = $"SELECT * FROM norm_rasz WHERE [norm_rasz].annId = '{Id}'";
////                ShowRelatedData(query, customGridControl3, normraszBindingSource1);
////            }
////        }

////        //Увязка выбранных артикулов
////        private void customButton4_Click(object sender, EventArgs e)
////        {
////            // Получение индекса выбранной строки в customGridControl
////            int[] selectedArticuls = gridView7.GetSelectedRows();

////            if (selectedArticuls.Length > 0)
////            {
////                foreach (var rowHandle in selectedArticuls)
////                {
////                    // Получение ID строки из norm_rasz
////                    int sp_articul = Convert.ToInt32(gridView10.GetRowCellValue(rowHandle, "kod"));
////                    int ann = Convert.ToInt32(gridView10.GetRowCellValue(rowHandle, "kod"));

////                    try
////                    {
////                        // Запись annId
////                        //   UpdateAnnId(sp_articul, ann);
////                        MessageBox.Show("annId успешно привязан .", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

////                        // Обновление данных после изменения
////                        //
////                        customGridControl5.RefreshDataSource();

////                        gridView10.RefreshData();
////                    }
////                    catch (Exception ex)
////                    {
////                        MessageBox.Show("Ошибка при привязке annId: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                    }
////                }
////            }
////            else
////            {
////                MessageBox.Show("Выберите запись для редактирования.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
////            }

////        }

////        private void UpdateAnnId(int sp_articul, int ann)
////        {
////            string query = "UPDATE sp_articul SET annId = @annId WHERE kod = @Id";
////            using (SqlConnection connection = new SqlConnection(connectionString))
////            {
////                connection.Open();
////                using (SqlCommand command = new SqlCommand(query, connection))
////                {
////                    command.Parameters.AddWithValue("@Id", sp_articul);
////                    command.ExecuteNonQuery();
////                }
////            }
////        }

////        private void customButton5_Click(object sender, EventArgs e)
////        {
////            // Получение индекса строки и столбца, которые нужно редактировать
////            int rowHandle = gridView3.FocusedRowHandle;
////            int columnIndex = gridView3.FocusedColumn.VisibleIndex; // Или gridView1.Columns["ColumnName"].VisibleIndex;

////            // Проверка, выбрана ли строка и доступен ли редактор
////            if (rowHandle >= 0 && gridView3.IsDataRow(rowHandle) && columnIndex >= 0)
////            {
////                // Вызов редактора
////                gridView3.FocusedRowHandle = rowHandle;
////                gridView3.FocusedColumn = gridView3.Columns[columnIndex];
////                gridView3.ShowEditor();
////            }
////        }

////        private void customButton11_Click(object sender, EventArgs e)
////        {
////            string searchText = customComboBox1.Text;

////            string columnName = "";
////            if (kode.Checked)
////            {
////                columnName = "kod";
////            }
////            else if (articul.Checked)
////            {
////                columnName = "articul";
////            }
////            else if (model.Checked)
////            { columnName = "mod"; }
////            else if (group.Checked)
////            { columnName = "grup"; }
////            gridView3.ActiveFilterCriteria = new DevExpress.Data.Filtering.FunctionOperator(
////                DevExpress.Data.Filtering.FunctionOperatorType.Contains,
////                new DevExpress.Data.Filtering.OperandProperty(columnName),
////                new DevExpress.Data.Filtering.OperandValue(searchText)
////            );
////        }

////        private void kode_CheckedChanged(object sender, EventArgs e)
////        {
////            if (kode.Checked)
////            {
////                ItemsLoad("kod");
////            }
////        }

////        private void articul_CheckedChanged(object sender, EventArgs e)
////        {
////            if (articul.Checked)
////                ItemsLoad("articul");
////        }
////        private void model_CheckedChanged(object sender, EventArgs e)
////        {
////            if (model.Checked)
////                ItemsLoad("mod");
////        }
////        private void group_CheckedChanged(object sender, EventArgs e)
////        {
////            if (group.Checked)
////                ItemsLoad("grup");
////        }

////        async Task ItemsLoad(string filter)
////        {
////            // Загрузка DataTable асинхронно
////            DataTable dataTable = await Task.Run(() => GetDataTableFromDatabase());

////            // Обновление ComboBox на главном потоке
////            this.BeginInvoke((Action)(() => {
////                customComboBox1.BeginUpdate();
////                customComboBox1.DataSource = dataTable;
////                customComboBox1.DisplayMember = filter;
////                customComboBox1.EndUpdate();
////            }));
////        }

////        private DataTable GetDataTableFromDatabase()
////        {
////            DataTable dt = ((BindingSource)gridView3.DataSource).DataSource as DataTable;
////            return dt;
////        }

////        private void gridControl2_Click(object sender, EventArgs e)
////        {

////        }

////        //    private async void ItemsLoad(string filter)
////        //    {
////        //        DataTable dt = await Task.Run(() => ((BindingSource)gridView3.DataSource).DataSource as DataTable);
////        //        var uniqueValues = dt.AsEnumerable()
////        //.Select(row => row.Field<string>(filter)).Distinct().ToList();
////        //        await this.BeginInvoke((Action)(() =>
////        //        {
////        //            customComboBox1.BeginUpdate();
////        //            customComboBox1.DataSource = dt;
////        //            customComboBox1.DisplayMember = filter;
////        //            customComboBox1.EndUpdate();
////        //        }));

////        //    }

////    }

////}


//namespace SewingProduction.form
//{
//    public partial class TeamWork : CustomForm
//    {
//        private readonly ArtNormService _artNormService;
//        public TeamWork()
//        {
//            InitializeComponent();
//            var dbHelper = new DatabaseHelper(Properties.Settings.Default.ACEConnectionString);
//            _artNormService = new ArtNormService(dbHelper);
//        }

//        private void TeamWorkForm_Load(object sender, EventArgs e)
//        {
//            try
//            {
//                var data = _artNormService.GetArtNormData();
//                artnormnBindingSource.DataSource = data;
//                gridControl2.DataSource = artnormnBindingSource;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void gridView3_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
//        {
//            if (e.FocusedRowHandle >= 0)
//            {
//                try
//                {
//                    var view = gridControl2.MainView as GridView;
//                    if (view != null)
//                    {
//                        int annId = Convert.ToInt32(view.GetRowCellValue(e.FocusedRowHandle, "annID"));
//                        //normRasz
//                        var relatedData = _artNormService.GetRelatedNormRasz(annId);
//                        normraszBindingSource.DataSource = relatedData;
//                        gridControl1.DataSource = normraszBindingSource;
//                        //normRask
//                        relatedData = _artNormService.GetRelatedNormRask(annId);
//                        normraskBindingSource.DataSource = relatedData;
//                        gridControl3.DataSource = normraskBindingSource;
//                        //normKont
//                        relatedData = _artNormService.GetRelatedNormKont(annId);
//                        normkontBindingSource.DataSource = relatedData;
//                        gridControl4.DataSource = normkontBindingSource;
//                        //normDopObr
//                        relatedData = _artNormService.GetRelatedNormDopObr(annId);
//                        normdopobrBindingSource.DataSource = relatedData;
//                        gridControl5.DataSource = normdopobrBindingSource;
//                        //spArt
//                        relatedData = _artNormService.GetRelatedspArt(annId);
//                        sparticulBindingSource.DataSource = relatedData;
//                        customGridControl5.DataSource = sparticulBindingSource;
//                        //komment
//                    //    commentRichTextBox.Text = gridControl2//gridView3.FocusedValue.ToString();

//                    }
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }
//        private void ItemsLoad(string columnName)
//        {
//            try
//            {
//                var uniqueValues = _artNormService.GetUniqueValues("Art_norm_n", columnName);

//                customComboBox1.BeginUpdate();
//                customComboBox1.DataSource = uniqueValues;
//                customComboBox1.DisplayMember = columnName;
//                customComboBox1.ValueMember = columnName;
//                customComboBox1.EndUpdate();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        //Отвязка артикула от РТ
//        private void ResetButton_Click(object sender, EventArgs e)
//        {
//            var view = gridControl2.MainView as GridView;
//            if (view != null)
//            {
//                int[] selectedRows = view.GetSelectedRows();
//                foreach (var rowHandle in selectedRows)
//                {
//                    int kod = Convert.ToInt32(view.GetRowCellValue(rowHandle, "kod"));
//                    _artNormService.ResetAnnId(kod);
//                }
//                //ReloadGridControlData();
//                customGridControl5.RefreshDataSource();
//                MessageBox.Show("Записи обновлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//        }
//        // Метод для обновления данных в GridControl
//        private void ReloadGridControlData()
//        {
//            try
//            {
//               //// var data = _artNormService.GetRelatedspArt(); // Получение обновленных данных
//               // artnormnBindingSource.DataSource = data; // Обновление источника данных
//               // customGridControl5.DataSource = sparticulBindingSource; // Привязка данных к GridControl
//               // customGridControl5.RefreshDataSource(); // Обновление отображения данных
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        //Увязка выбранных артикулов
//        private void BindButton_Click(object sender, EventArgs e)
//        {
//            var view = gridControl1.MainView as GridView;
//            if (view != null)
//            {
//                int[] selectedRows = view.GetSelectedRows();
//                foreach (var rowHandle in selectedRows)
//                {
//                    int kod = Convert.ToInt32(view.GetRowCellValue(rowHandle, "kod"));
//                    int annId = Convert.ToInt32(view.GetRowCellValue(rowHandle, "annID"));
//                    _artNormService.UpdateAnnId(kod, annId);
//                }
//                MessageBox.Show("Привязка завершена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//        }

//        //поиск
//        private void SearchButton_Click(object sender, EventArgs e)
//        {
//            string searchText = customComboBox1.Text;

//            string columnName = "";
//            if (kode.Checked)
//            {
//                columnName = "kod";
//            }
//            else if (articul.Checked)
//            {
//                columnName = "articul";
//            }
//            else if (model.Checked)
//            { columnName = "mod"; }
//            else if (group.Checked)
//            { columnName = "grup"; }
//            gridView3.ActiveFilterCriteria = new DevExpress.Data.Filtering.FunctionOperator(
//                DevExpress.Data.Filtering.FunctionOperatorType.Contains,
//                new DevExpress.Data.Filtering.OperandProperty(columnName),
//                new DevExpress.Data.Filtering.OperandValue(searchText)
//            );
//        }

//        //обработка изменения параметров фильтрации
//        private void kode_CheckedChanged(object sender, EventArgs e)
//        {
//            if (kode.Checked) ItemsLoad("kod");
//        }

//        private void articul_CheckedChanged(object sender, EventArgs e)
//        {
//            if (articul.Checked) ItemsLoad("articul");
//        }

//        private void model_CheckedChanged(object sender, EventArgs e)
//        {
//            if (model.Checked) ItemsLoad("mod");
//        }

//        private void group_CheckedChanged(object sender, EventArgs e)
//        {
//            if (group.Checked) ItemsLoad("grup");
//        }

//        // не описанные - sec_shv=0 & arh = 0
//        private void customButton3_Click(object sender, EventArgs e)
//        {
//            if (SortBox.Checked)
//            {
//                gridView3.ActiveFilterString = string.Format("[colsec_shv] = '{0}' AND [colarh] = '{1}'", 0, 0);// string.Format("kod LIKE '{0}'", textBox1.Text); // Поиск по полю FieldName, содержащему SearchText
//            }

//        }
//        //Загрузка таблицы "Артикулы для увязки"
//        private void customGridControl2_Load(object sender, EventArgs e)
//        {
//          //  ShowArtData("SELECT kod, grup, articul, mod, st FROM art_norm_n", customGridControl2);
//        }


//        //Предварительный архив
//        private void customGridControl4_Load(object sender, EventArgs e)
//        {
//          //  ShowArtData("SELECT kod, articul FROM art_norm_n", customGridControl4);

//        }
//        //Загрузка вкладки "текущие работы"
//        private void xtraTabControl2_Enter(object sender, EventArgs e)
//        {
//            //артикулы для увязки
//            var relatedData = _artNormService.GetRelatedspArt(0);
//            sparticulBindingSource.DataSource = relatedData;
//            customGridControl1.DataSource = sparticulBindingSource;
//            //РТ для увязки
//            //relatedData = _artNormService.GetArtNormData();
//            //sparticulBindingSource.DataSource = relatedData;
//            customGridControl2.DataSource = artnormnBindingSource;

//        }
//        private void customGridControl1_Load(object sender, EventArgs e)
//        {
//           //    ShowArtData("SELECT SUBSTRING(kod, 1, 7) AS kod, grup, articul, mod FROM sp_articul where annId is NULL", customGridControl1);
//        }
//        //Реализация зависимости таблиц "Артикулы для увязки" и  "norm_rasz"
//        private void gridView8_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
//        {
//            if (e.FocusedRowHandle >= 0)
//            {
//                try
//                {
//                    var view = gridControl2.MainView as GridView;
//                    if (view != null)
//                    {
//                        int annId = Convert.ToInt32(view.GetRowCellValue(e.FocusedRowHandle, "annID"));
//                        var relatedData = _artNormService.GetRelatedNormRasz(annId);
//                        normraszBindingSource.DataSource = relatedData;
//                        customGridControl3.DataSource = normraszBindingSource;
//                    }
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }

//        }

//    }


//    public class DatabaseHelper
//    {
//        private readonly string _connectionString;

//        public DatabaseHelper(string connectionString)
//        {
//            _connectionString = connectionString;
//        }

//        // Выполнение SELECT-запросов
//        public DataTable ExecuteQuery(string query, Dictionary<string, object> parameters = null)
//        {
//            var dt = new DataTable();
//            using (var connection = new SqlConnection(_connectionString))
//            {
//                connection.Open();
//                using (var command = new SqlCommand(query, connection))
//                {
//                    if (parameters != null)
//                    {
//                        foreach (var param in parameters)
//                        {
//                            command.Parameters.AddWithValue(param.Key, param.Value);
//                        }
//                    }
//                    using (var adapter = new SqlDataAdapter(command))
//                    {
//                        adapter.Fill(dt);
//                    }
//                }
//            }
//            return dt;
//        }

//        // Выполнение команд INSERT, UPDATE, DELETE
//        public void ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
//        {
//            using (var connection = new SqlConnection(_connectionString))
//            {
//                connection.Open();
//                using (var command = new SqlCommand(query, connection))
//                {
//                    if (parameters != null)
//                    {
//                        foreach (var param in parameters)
//                        {
//                            command.Parameters.AddWithValue(param.Key, param.Value);
//                        }
//                    }
//                    command.ExecuteNonQuery();
//                }
//            }
//        }
//        public DataTable GetUniqueColumnValues(string tableName, string columnName)
//        {
//            string query = $"SELECT DISTINCT {columnName} FROM {tableName} ORDER BY {columnName}";
//            return ExecuteQuery(query);

//        }
//    }

//    public class ArtNormService
//    {
//        private readonly DatabaseHelper _dbHelper;

//        public ArtNormService(DatabaseHelper dbHelper)
//        {
//            _dbHelper = dbHelper;
//        }

//        // Получение данных из таблицы Art_norm_n
//        public DataTable GetArtNormData()
//        {
//            string query = "SELECT * FROM Art_norm_n";
//            return _dbHelper.ExecuteQuery(query);
//        }

//        // Получение связанных данных из norm_rasz
//        public DataTable GetRelatedNormRasz(int annId)
//        {
//            string query = "SELECT * FROM norm_rasz WHERE annId = @annId";
//            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } });
//        }

//        // Получение связанных данных из norm_rask
//        public DataTable GetRelatedNormRask(int annId)
//        {
//            string query = "SELECT * FROM norm_rask WHERE annId = @annId";
//            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } });
//        }

//        // Получение связанных данных из norm_kont
//        public DataTable GetRelatedNormKont(int annId)
//        {
//            string query = "SELECT * FROM norm_kont WHERE annId = @annId";
//            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } });
//        }
//        // Получение связанных данных из norm_dop_obr
//        public DataTable GetRelatedNormDopObr(int annId)
//        {
//            string query = "SELECT * FROM norm_dop_obr WHERE annId = @annId";
//            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } });
//        }
//        // Получение связанных данных из sp_articul
//        public DataTable GetRelatedspArt(int annId)
//        {
//            //string query = "";
//            //if (annId == 0)
//            //{ query = "SELECT SUBSTRING(kod,1,7) as kod, grup, articul, mod, FROM sp_articul WHERE annID IS NULL"; }
//            //else if (annId>0)
//            //{ query = $"SELECT SUBSTRING(kod,1,7) as kod, grup, articul, mod FROM sp_articul WHERE annID = @annId"; }
//            string query = annId == 0 
//                ? "SELECT SUBSTRING(kod,1,7) as kod, grup, articul, mod FROM sp_articul WHERE annID IS NULL" 
//                : $"SELECT SUBSTRING(kod,1,7) as kod, grup, articul, mod FROM sp_articul WHERE annID = @annId";
//            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } });
//        }

//        //отвязка РТ - обнуление annID в таблице sp_articul
//        public void ResetAnnId(int spArticul)
//        {
//            string query = "UPDATE sp_articul SET annId = NULL WHERE kod LIKE @kod";
//            string param = spArticul.ToString()+"%";
//            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@kod", param } });
//        }

//        // Привязка annId к sp_articul
//        public void UpdateAnnId(int spArticul, int annId)
//        {
//            string query = "UPDATE sp_articul SET annId = @annId WHERE kod = @kod";
//            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object>
//        {
//            { "@kod", spArticul },
//            { "@annId", annId }
//        });
//        }
//        public DataTable GetUniqueValues(string tableName, string columnName)
//        {
//            return _dbHelper.GetUniqueColumnValues(tableName, columnName);
//        }
//    }

//}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data.Filtering;
using DevExpress.XtraGrid.Views.Base;
using BindingSource = System.Windows.Forms.BindingSource;
using DataTable = System.Data.DataTable;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.DataAccess.Sql;
using DevExpress.Utils;
using DevExpress.Mvvm.Native;

namespace SewingProduction.form
{
    public partial class TeamWork : CustomForm
    {
        private readonly ArtNormService _artNormService;

        public TeamWork()
        {
            InitializeComponent();
            var dbHelper = new DatabaseHelper(Properties.Settings.Default.ACEConnectionString);
            _artNormService = new ArtNormService(dbHelper);
        }

        private void TeamWorkForm_Load(object sender, EventArgs e)
        {
            try
            {
                int annId = 0;
                var data = _artNormService.GetArtNormData();
                artnormnBindingSource.DataSource = data;
                gridControl2.DataSource = artnormnBindingSource;

                var view = gridControl2.MainView as GridView;
                if (view != null)
                {
                    annId = Convert.ToInt32(view.GetRowCellValue(0, "annId"));
                }

                commentRichTextBox.Text = FieldsUpdate(view, 0, "komment");
                ModelTextBox.Text = FieldsUpdate(view, 0, "mod");
                NameTextBox.Text = FieldsUpdate(view, 0, "articul");
                dateCreate.Text = FieldsUpdate(view, 0, "data_sozd");
                SecTimeTextBox.Text = FieldsUpdate(view, 0, "sek");
                constructorComboBox.Text = FieldsUpdate(view, 0, "constr");
                designerComboBox.Text = FieldsUpdate(view, 0, "diz");

                LoadGridControlData(gridControl1, normraszBindingSource, _artNormService.GetRelatedNormRasz(annId));
                LoadGridControlData(gridControl3, normraskBindingSource, _artNormService.GetRelatedNormRask(annId));
                LoadGridControlData(gridControl4, normkontBindingSource, _artNormService.GetRelatedNormKont(annId));
                LoadGridControlData(gridControl5, normdopobrBindingSource, _artNormService.GetRelatedNormDopObr(annId));
                LoadGridControlData(customGridControl5, sparticulBindingSource, _artNormService.GetRelatedspArt(annId));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gridControl2_Load(object sender, EventArgs e)
        {

        }
        private void gridView3_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                try
                {
                    var view = gridControl2.MainView as GridView;
                    if (view != null)
                    {
                            commentRichTextBox.Text = FieldsUpdate(view, e.FocusedRowHandle, "komment");
                            ModelTextBox.Text = FieldsUpdate(view, e.FocusedRowHandle, "mod");
                            NameTextBox.Text = FieldsUpdate(view, e.FocusedRowHandle, "articul");
                            dateCreate.Text = FieldsUpdate(view, e.FocusedRowHandle, "data_sozd");
                            SecTimeTextBox.Text = FieldsUpdate(view, e.FocusedRowHandle, "sek");
                            constructorComboBox.Text = FieldsUpdate(view, e.FocusedRowHandle, "constr");
                            designerComboBox.Text = FieldsUpdate(view, e.FocusedRowHandle, "diz");
                        int annId = Convert.ToInt32(view.GetRowCellValue(e.FocusedRowHandle, "annId"));
                        UpdateRelatedData(annId);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

            private string FieldsUpdate(GridView view, int row_number, string field)
            {
               string text = (view.GetRowCellValue(row_number, field) == null) ? "" : view.GetRowCellValue(row_number, field).ToString();
            return text;   
            }
        

        private void UpdateRelatedData(int annId)
        {
            
            LoadGridControlData(gridControl1, normraszBindingSource, _artNormService.GetRelatedNormRasz(annId));
            LoadGridControlData(gridControl3, normraskBindingSource, _artNormService.GetRelatedNormRask(annId));
            LoadGridControlData(gridControl4, normkontBindingSource, _artNormService.GetRelatedNormKont(annId));
            LoadGridControlData(gridControl5, normdopobrBindingSource, _artNormService.GetRelatedNormDopObr(annId));
            LoadGridControlData(customGridControl5, sparticulBindingSource, _artNormService.GetRelatedspArt(annId));
        }

        //Загрузка данных в связанные таблицы с помощью фильтров
        private void LoadGridControlData(GridControl grid, BindingSource source, int _annId)//DataTable data)
        {
            //// Привязываем данные к BindingSource
            //source.DataSource = data;

            //// Привязываем BindingSource к GridControl
            //grid.DataSource = source;

            //// Обновляем визуализацию данных
            //grid.RefreshDataSource();
            string filter = "annId = " + _annId;
            GridView view = (GridView)grid.Views[0];
            view.BeginUpdate();
            view.ActiveFilterString = filter;
            view.EndUpdate();

        }

        //загрузка данных в связанные таблицы с помощью запросов
        private void LoadGridControlData(GridControl grid, BindingSource source, DataTable data)
        {
            // Привязываем данные к BindingSource
            source.DataSource = data;

            // Привязываем BindingSource к GridControl
            grid.DataSource = source;

            // Обновляем визуализацию данных
            grid.RefreshDataSource();
        }
        private void ResetButton_Click(object sender, EventArgs e)
        {
            var view = gridControl2.MainView as GridView;
            if (view != null)
            {
                int[] selectedRows = view.GetSelectedRows();
                foreach (var rowHandle in selectedRows)
                {
                    int kod = Convert.ToInt32(view.GetRowCellValue(rowHandle, "kod"));
                    _artNormService.ResetAnnId(kod);
                }
                customGridControl5.RefreshDataSource();
                MessageBox.Show("Записи обновлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BindButton_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view != null)
            {
                int[] selectedRows = view.GetSelectedRows();
                foreach (var rowHandle in selectedRows)
                {
                    int kod = Convert.ToInt32(view.GetRowCellValue(rowHandle, "kod"));
                    int annId = Convert.ToInt32(view.GetRowCellValue(rowHandle, "annID"));
                    _artNormService.UpdateAnnId(kod, annId);
                }
                MessageBox.Show("Привязка завершена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string searchText = searchComboBox1.Text;
            string columnName = GetSelectedColumnName();
            if (!string.IsNullOrEmpty(columnName))
            {
                gridView3.ActiveFilterCriteria = new FunctionOperator(
                    FunctionOperatorType.Contains,
                    new OperandProperty(columnName),
                    new OperandValue(searchText));
            }
        }

        private string GetSelectedColumnName()
        {
            if (kode.Checked) return "kod";
            if (articul.Checked) return "articul";
            if (model.Checked) return "mod";
            if (group.Checked) return "grup";
            return string.Empty;
        }

        private void customButton5_Click(object sender, EventArgs e)
        {

        }

        // не описанные - sec_shv=0 & arh = 0
        private void customButton3_Click(object sender, EventArgs e)
        {

        }

        private void customCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            string filterString = "";
            if (customCheckBox1.Checked) filterString += "status = 1";
            if (customCheckBox2.Checked) filterString += (filterString.Length > 0 ? " OR " : "") + "status = 2";
            if (customCheckBox3.Checked) filterString += (filterString.Length > 0 ? " OR " : "") + "status = 3";

            gridView3.BeginUpdate();
            gridView3.ActiveFilterString = filterString;
            gridView3.EndUpdate();
        }

        private void customButton12_Click(object sender, EventArgs e)
        {
            string filterString = filterTextBox1.Text.TrimEnd(' ');
            string columnName = GetSelectedColumnName();
            if (filterString.Length > 0)
            {
                if (!string.IsNullOrEmpty(columnName))
                {
                    gridView3.ActiveFilterCriteria = new FunctionOperator(
                        FunctionOperatorType.Contains,
                        new OperandProperty(columnName),
                        new OperandValue(filterString));
                }
            }

        }


    }

    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

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

        public void ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
        {
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
                    command.ExecuteNonQuery();
                }
            }
        }
    }

    public class ArtNormService
    {
        private readonly DatabaseHelper _dbHelper;

        public ArtNormService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public DataTable GetArtNormData()
        {
            string query = "SELECT annId, kod, grup, articul, mod, sek, sek_vyaz, data_obn, sek_shv, status, sek_vyazo, sek_vyaz5, sek_vyaz7, sek_vyaz12, sek_vyaz10, sek_vyaz6, sek_kr, slogn, komment, data_sozd, diz, constr FROM Art_norm_n";
            return _dbHelper.ExecuteQuery(query);
        }

        public void ResetAnnId(int spArticul)
        {
            string query = "UPDATE sp_articul SET annId = NULL WHERE kod = @kod";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@kod", spArticul } });
        }

        public void UpdateAnnId(int spArticul, int annId)
        {
            string query = "UPDATE sp_articul SET annId = @annId WHERE kod = @kod";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@kod", spArticul }, { "@annId", annId } });
        }

        // Получение связанных данных

        public DataTable GetRelatedNormRasz(int annId)
        {
            string query = "SELECT annId, n, n1, razryd, text, sek, kod, kod_o, kod_ob FROM norm_rasz WHERE annId = @annId";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } });
        }
        public DataTable GetRelatedNormRask(int annId)
        {
            string query = "SELECT annId, kod_o, razryd, text, sek  FROM norm_rask WHERE annId = @annId";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } });
        }

        public DataTable GetRelatedNormKont(int annId)
        {
            string query = "SELECT annId, kod_o, razryd, text, sek FROM norm_kont WHERE annId = @annId";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } });
        }

        public DataTable GetRelatedNormDopObr(int annId)
        {
            string query = "SELECT annId, sek_p, sek_p_tamp, sek_v, sek_stra FROM norm_dop_obr WHERE annId = @annId";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } });
        }

        // Получение связанных данных из sp_articul
        public DataTable GetRelatedspArt(int annId)
        {
            //string query = "";
            //if (annId == 0)
            //{ query = "SELECT SUBSTRING(kod,1,7) as kod, grup, articul, mod, FROM sp_articul WHERE annID IS NULL"; }
            //else if (annId>0)
            //{ query = $"SELECT SUBSTRING(kod,1,7) as kod, grup, articul, mod FROM sp_articul WHERE annID = @annId"; }
            string query = annId == 0 
                ? "SELECT SUBSTRING(kod,1,7) as kod, grup, articul, mod, annId FROM sp_articul WHERE annID IS NULL" 
                : $"SELECT SUBSTRING(kod,1,7) as kod, grup, articul, mod, annId FROM sp_articul WHERE annID = @annId";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } });
        }


    }

    
    }