//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using DevExpress.XtraGrid;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using DevExpress.XtraVerticalGrid;
//using SewingProduction.Helpers;
//using SewingProduction.Services;
//using DevExpress.XtraGrid.Views.Grid;

//namespace SewingProduction.form
//{
//    public partial class NormOperNew : CustomForm
//    {
//        private readonly DatabaseHelper _dbHelper;
//        private readonly ArtNormService _artNormService;
//        public MyData SelectedRowData { get; private set; }

//        public NormOperNew()
//        {
//            InitializeComponent();
//            _dbHelper = new DatabaseHelper("ace");
//            _artNormService = new ArtNormService(_dbHelper);
//            UpdateTheme(this);
//        }

//        private void NormOperNew_Load(object sender, EventArgs e)
//        {
//            // TODO: данная строка кода позволяет загрузить данные в таблицу "aCE_backupDataSet1.norm_oper". При необходимости она может быть перемещена или удалена.
//            this.norm_operTableAdapter.Fill(this.aCE_backupDataSet.norm_oper);
//            LoadData();

//        }

//        private void LoadData()
//        {
//            // Загружаем данные из базы
//            DataTable data = _artNormService.GetNormOper(0);

//            if (data != null && data.Rows.Count > 0)
//            {
//                // Заполняем таблицу данными из БД
//                normoperBindingSource.DataSource = data;
//                customGridControl1.DataSource = normoperBindingSource;
//            }
//            else
//            {
//                MessageBox.Show("Данные отсутствуют", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//        }

//        private void customOkButton1_Click(object sender, EventArgs e)
//        {
//            //GridView view = customGridControl1.MainView as GridView;
//            GridView view = gridView1;
//            if (view == null || view.FocusedRowHandle < 0) return;

//            SelectedRowData = new MyData
//            {
//                Kod_o = view.GetRowCellValue(view.FocusedRowHandle, "kod_o").ToString(),
//                Text = view.GetRowCellValue(view.FocusedRowHandle, "text").ToString(),
//                Spec = view.GetRowCellValue(view.FocusedRowHandle, "spec").ToString(),
//                Razryad = view.GetRowCellValue(view.FocusedRowHandle, "razryd").ToString(),
//                Obor = view.GetRowCellValue(view.FocusedRowHandle, "obor").ToString(),
//                Kod_proizv = view.GetRowCellValue(view.FocusedRowHandle, "kod_proizv").ToString()
//            };

//            this.DialogResult = DialogResult.OK;
//            this.Close();

//        }

//        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            if (!(sender is DevExpress.XtraEditors.RadioGroup radioGroup)) return;

//            string filterString = ""; // Строка фильтра

//            switch (radioGroup.SelectedIndex)
//            {
//                case 1: // вязальное
//                    filterString = "[kod_proizv] = 1";
//                    break;
//                case 2: // швейное
//                    filterString = "[kod_proizv] = 0";
//                    break;
//                case 3: // носки
//                    filterString = "[kod_proizv] = 3";
//                    break;
//                default: // всё
//                    filterString = "";
//                    break;
//            }

//            // Применяем фильтр к GridView
//            gridView1.ActiveFilterString = filterString;
//        }

//    }
//    public class MyData
//    {
//        public string Kod_o { get; set; }
//        public string Text { get; set; }
//        public string Spec { get; set; }
//        public string Razryad { get; set; }
//        public string Obor { get; set; }
//        public string Kod_proizv { get; set; }
//    }

//}
using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.form
{
    public partial class NormOperNew : CustomForm
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly ArtNormService _artNormService;
        private readonly ILogger _logger = new FileLogger();

        public MyData SelectedRowData { get; private set; }

        public NormOperNew()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _artNormService = new ArtNormService(_dbHelper);
            ThemeManager.UpdateTheme(this);
        }

        private void NormOperNew_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "aCE_backupDataSet.norm_oper". При необходимости она может быть перемещена или удалена.
           // this.norm_operTableAdapter.Fill(this.aCE_backupDataSet.norm_oper);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "aCE_backupDataSet.norm_oper". При необходимости она может быть перемещена или удалена.
            // this.norm_operTableAdapter.Fill(this.aCE_backupDataSet.norm_oper);
            try
            {
                // Заполнение данных из БД
               // this.norm_operTableAdapter.Fill(this.aCE_backupDataSet.norm_oper);
                LoadData();
            }
            catch (ConstraintException ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Логируем подробности ошибки
                foreach (DataTable table in aCE_backupDataSet.Tables)
                {
                    foreach (DataRow row in table.GetErrors())
                    {
                        foreach (DataColumn col in table.Columns)
                        {
                            Console.WriteLine($"Столбец {col.ColumnName}: {row[col]}");
                        }
                        Console.WriteLine($"Ошибка: {row.RowError}");
                    }
                }
            }
        }

        /// <summary>
        /// Загружает данные в `GridControl`
        /// </summary>
        private async void LoadData()
        {
            try
            {
                DataTable data = await _artNormService.GetNormOper(0);

                if (data != null && data.Rows.Count > 0)
                {
                    normoperBindingSource.DataSource = data;
                    customGridControl1.DataSource = normoperBindingSource;
                }
                else
                {
                    MessageBox.Show("Нет данных для отображения.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка загрузки данных в GridControl");
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Выбор строки и передача данных в `TeamWork_AdvanceTW`
        /// </summary>
        private async void customOkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                GridView view = gridView1;
                if (view == null || view.FocusedRowHandle < 0) return;

                SelectedRowData = new MyData
                {
                    Kod_o = Convert.ToString(view.GetRowCellValue(view.FocusedRowHandle, "kod_o")),
                    Text = Convert.ToString(view.GetRowCellValue(view.FocusedRowHandle, "text")),
                    Spec = Convert.ToString(view.GetRowCellValue(view.FocusedRowHandle, "spec")),
                    Razryad = Convert.ToString(view.GetRowCellValue(view.FocusedRowHandle, "razryd")),
                    Obor = Convert.ToString(view.GetRowCellValue(view.FocusedRowHandle, "obor")),
                    Kod_proizv = Convert.ToString(view.GetRowCellValue(view.FocusedRowHandle, "kod_proizv")),
                    Text_proizv = Convert.ToString(view.GetRowCellValue(view.FocusedRowHandle, "text_proizv")),
                    Text_vyaz = Convert.ToString(view.GetRowCellValue(view.FocusedRowHandle, "text_vyaz")),
                    Text_ob = Convert.ToString(view.GetRowCellValue(view.FocusedRowHandle, "text_ob"))
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при выборе строки в NormOperNew");
                MessageBox.Show($"Ошибка при выборе строки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Фильтрация данных в `GridView` по `radioGroup`
        /// </summary>
        private async void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is RadioGroup radioGroup)) return;

                string filterString = ""; // Строка фильтра

                switch (radioGroup.SelectedIndex)
                {
                    case 1: // Вязальное производство
                        filterString = "[kod_proizv] = 1";
                        break;
                    case 2: // Швейное производство
                        filterString = "[kod_proizv] = 0";
                        break;
                    case 3: // Носки
                        filterString = "[kod_proizv] = 3";
                        break;
                    default: // Показать все
                        filterString = "";
                        break;
                }

                // Применяем фильтр к GridView
                gridView1.ActiveFilterString = filterString;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при фильтрации данных");
                MessageBox.Show($"Ошибка при фильтрации данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
