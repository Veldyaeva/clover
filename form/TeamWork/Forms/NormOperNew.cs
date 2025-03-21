using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using SewingProduction.Helpers;
using SewingProduction.Services;
using SewingProduction.Models;
using System.IO;
using DevExpress.XtraGrid.Views.Base;

namespace SewingProduction.form
{
    public partial class NormOperNew : CustomForm
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly ArtNormService _artNormService;
        private readonly ILogger _logger = new FileLogger();
        private readonly GridHelper _gridHelper = new GridHelper();

        public NormRasz SelectedRowData { get; private set; }

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
                // Загружаем настройки грида перед заполнением данными
                _gridHelper.LoadGridViewSettings(gridView1, "NormOperGrid.xml");

                // Заполнение данных из БД
               // this.norm_operTableAdapter.Fill(this.aCE_backupDataSet.norm_oper);
                LoadData();
            }
            catch (ConstraintException ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Логируем подробности ошибки
            }
        }
        private void NormOperNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Сохраняем настройки грида при закрытии формы
                _gridHelper.SaveGridViewSettings(gridView1, "NormOperGrid.xml");
            }

            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при сохранении настроек грида");
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
                    //normoperBindingSource.DataSource = data;
                    customGridControl1.DataSource = data;//normoperBindingSource;
                    
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
        private void gridView1_ColumnWidthChanged(object sender, ColumnEventArgs e)
        {
            // Можно сохранять при каждом изменении размера колонки
            // (Внимание: частые сохранения могут повлиять на производительность)
            _gridHelper.SaveGridViewSettings(gridView1, "NormOperGrid.xml");
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

                SelectedRowData = new NormRasz
                {
                    KodO = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "kod_o")),
                    Text = Convert.ToString(view.GetRowCellValue(view.FocusedRowHandle, "text")),
                    Spec = Convert.ToString(view.GetRowCellValue(view.FocusedRowHandle, "spec")),
                    Razryad = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "razryd")),
                    Obor = Convert.ToString(view.GetRowCellValue(view.FocusedRowHandle, "obor")),
                    KodProizv = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "kod_proizv")),

                    Kod = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "kod")),
                    N1 = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "n1")),
                    Sek = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "sek")),
                    KodPodr = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "kod_podr")),
                    KodOb = Convert.ToString(view.GetRowCellValue(view.FocusedRowHandle, "kod_ob")),

                    TextProizv = Convert.ToString(view.GetRowCellValue(view.FocusedRowHandle, "text_proizv")),
                    TextVyaz = Convert.ToString(view.GetRowCellValue(view.FocusedRowHandle, "text_vyaz")),
                    TextOb = Convert.ToString(view.GetRowCellValue(view.FocusedRowHandle, "text_ob"))
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

        // Обработчики событий для радиокнопок поиска
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // Вызываем очистку поиска при смене радиокнопки
            GridHelper.OnSearchRadioButtonChanged(gridView1);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // Вызываем очистку поиска при смене радиокнопки
            GridHelper.OnSearchRadioButtonChanged(gridView1);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            // Вызываем очистку поиска при смене радиокнопки
            GridHelper.OnSearchRadioButtonChanged(gridView1);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            // Вызываем очистку поиска при смене радиокнопки
            GridHelper.OnSearchRadioButtonChanged(gridView1);
        }
    }

}

