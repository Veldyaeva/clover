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
using System.Threading.Tasks;

namespace SewingProduction.form
{
    public partial class NormOperNew : CustomForm
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly DbService _dbService;
        private readonly ArtNormService _artNormService;
        private readonly ILogger _logger = new FileLogger();
        private readonly GridHelper _gridHelper = new GridHelper();
        private readonly int _annId;
        public NormRasz SelectedRowData { get; private set; }

        public NormOperNew(int annId)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _dbService = new DbService(_dbHelper);
            _artNormService = new ArtNormService(_dbHelper);

            _annId = annId;
            ThemeManager.UpdateTheme(this);
        }

        private async void NormOperNew_Load(object sender, EventArgs e)
        {
            try
            {
                _gridHelper.LoadGridViewSettings(gridView1, "NormOperGrid.xml");
                LoadData();
            }
            catch (ConstraintException ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await _logger.LogErrorAsync(ex, "Ошибка загрузки данных}");
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
                    customGridControl1.DataSource = data;
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

                SelectedRowData = new NormRasz
                {
                    AnnId = _annId,
                    Kod_o = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "kod_o")),
                    Text = Convert.ToString(view.GetRowCellValue(view.FocusedRowHandle, "text")),
                    Spec = Convert.ToString(view.GetRowCellValue(view.FocusedRowHandle, "spec")),
                    Razryad = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "razryd")),
                    Obor = Convert.ToString(view.GetRowCellValue(view.FocusedRowHandle, "obor")),
                    Kod = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "kod")),
                    N1 = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "n1")),
                    Sek = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "sek")),
                    Kod_ob = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "kod_ob")),
                    Kod_podr = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "kod_podr")),
                    Kod_proizv = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "kod_proizv")),
                    TextProizv = Convert.ToString(view.GetRowCellValue(view.FocusedRowHandle, "text_proizv")),
                    TextVyaz = Convert.ToString(view.GetRowCellValue(view.FocusedRowHandle, "text_vyaz")),
                    TextOb = Convert.ToString(view.GetRowCellValue(view.FocusedRowHandle, "text_ob")),
                    IsNew = true
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка вставки", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при выборе строки в NormOperNew");
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

