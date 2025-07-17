using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class NormOperNew : CustomForm
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly DbService _dbService;
        private readonly ArtNormService _artNormService;
        private readonly ILogger _logger = new FileLogger();
        private readonly TWGridHelper _gridHelper = new TWGridHelper();
        private readonly int _annId;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] 
        public NormRasz SelectedRowData { get; private set; }
        public NormOperNew()
        {
            InitializeComponent();
        }
        public NormOperNew(int annId)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
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
                DataTable data = await _artNormService.GetNormOper();

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

        private async void customOkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                GridView view = gridView1;
                if (view == null || view.FocusedRowHandle < 0) return;

                SelectedRowData = NormalizeDataFromView(view, view.FocusedRowHandle);
                SelectedRowData.annId = _annId; // AnnId устанавливается здесь
                SelectedRowData.IsNew = true;   // IsNew также устанавливается здесь

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка вставки", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                await _logger.LogErrorAsync(ex, "InvalidOperationException при выборе строки в NormOperNew");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при выборе строки в NormOperNew");
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int GetIntFromView(GridView view, int rowHandle, string fieldName, int defaultValue = 0)
        {
            object value = view.GetRowCellValue(rowHandle, fieldName);
            return value == DBNull.Value ? defaultValue : Convert.ToInt32(value);
        }

        private int? GetNullableIntFromView(GridView view, int rowHandle, string fieldName)
        {
            object value = view.GetRowCellValue(rowHandle, fieldName);
            return value == DBNull.Value ? (int?)null : Convert.ToInt32(value);
        }
        private NormRasz NormalizeDataFromView(GridView view, int rowHandle)
        {
            var normRasz = new NormRasz();

            normRasz.kod_o = view.GetRowCellValue(rowHandle, "kod_o")?.ToString();
            normRasz.Text = Convert.ToString(view.GetRowCellValue(rowHandle, "text"))?.TrimEnd(' ');
            normRasz.Spec = Convert.ToString(view.GetRowCellValue(rowHandle, "spec"))?.TrimEnd(' ');
            normRasz.Obor = Convert.ToString(view.GetRowCellValue(rowHandle, "obor"))?.TrimEnd(' ');
            normRasz.razryd = GetIntFromView(view, rowHandle, "razryd"); 
            normRasz.N1 = GetIntFromView(view, rowHandle, "n1"); 
            normRasz.Sek = GetIntFromView(view, rowHandle, "sek"); 
            normRasz.KodOb = GetIntFromView(view, rowHandle, "kod_ob"); 
            normRasz.KodPodr = GetIntFromView(view, rowHandle, "kod_podr"); 
            normRasz.KodProizv = GetIntFromView(view, rowHandle, "kod_proizv"); 
            normRasz.TextProizv = Convert.ToString(view.GetRowCellValue(rowHandle, "text_proizv"))?.TrimEnd(' ');
            normRasz.TextVyaz = Convert.ToString(view.GetRowCellValue(rowHandle, "text_vyaz"))?.TrimEnd(' ');
            normRasz.TextOb = Convert.ToString(view.GetRowCellValue(rowHandle, "text_ob"))?.TrimEnd(' ');

            return normRasz;
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

