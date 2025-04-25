using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using SewingProduction.Models;
using SewingProduction.Services;
using SewingProduction.Helpers;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.CodeParser;

namespace SewingProduction.form.TeamWork.Forms
{
    public partial class norm_raskrNew : CustomForm
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly DbService _dbService;
        private readonly ArtNormService _artNormService;
        private readonly ILogger _logger = new FileLogger();
        private BindingSource _raskroyNormBindingSource;
        private readonly GridHelper _gridHelper = new GridHelper();
        public List<NormRask> SelectedData { get; private set; } = new List<NormRask>();
        private List<GridBand> _selectedBands = new List<GridBand>(); // Список для хранения выбранных бэндов
        private List<BandData> _bandDataList = new List<BandData>(); // Список для хранения данных выбранных бэндов
        private int _annId;
        public struct BandData
        {
            public int gr {  get; set; }
            public string Naimen { get; set; }
            public int Dras { get; set; }
            public int Drez { get; set; }
            public int Dpro { get; set; }
            public int Lras { get; set; }
            public int Lrez { get; set; }
            public int Lpro { get; set; }
        }

        public norm_raskrNew(int annId)
        {
            InitializeComponent();
            _annId = annId;

            _dbService = new DbService(new DatabaseHelper("ace"));
            _artNormService = new ArtNormService(new DatabaseHelper("ace"));
            ThemeManager.UpdateTheme(this);

            ConfigureGrid();
            this.Load += async (s, e) => await LoadInitialDataAsync();
        }
        private async Task LoadInitialDataAsync()
        {
            await LoadGroups(); // Загружаем группы
            if (customComboBox1.SelectedValue != null)
            {
                await LoadData((int)customComboBox1.SelectedValue); // Загружаем данные по первой выбранной группе
            }
        }

        private void ConfigureGrid()
        {
            _raskroyNormBindingSource = new BindingSource();
            gridControl1.DataSource = _raskroyNormBindingSource;
        }

        private async Task LoadGroups()
        {
            try
            {
                DataTable data = await _artNormService.GetRaskroyNormGroups();
                if (data != null && data.Rows.Count > 0)
                {
                    customComboBox1.DataSource = data;
                    customComboBox1.DisplayMember = "naimen";
                    customComboBox1.ValueMember = "gr";
                }
                else
                {
                    MessageBox.Show("Нет данных для отображения групп.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка загрузки групп в ComboBox");
                MessageBox.Show($"Ошибка при загрузке групп: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CustomComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (customComboBox1.SelectedValue != null)
                {
                    await LoadData((int)customComboBox1.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при изменении выбранной группы");
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async Task LoadData(int groupId)
        {
            try
            {
                DataTable data = await _artNormService.GetRaskroyNormByGroup(groupId);
                if (data != null && data.Rows.Count > 0)
                {
                    _raskroyNormBindingSource.DataSource = data;
                }
                else
                {
                    _raskroyNormBindingSource.DataSource = null;
                    MessageBox.Show("Нет данных для отображения.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка загрузки данных в GridControl");
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            if (_selectedBands.Count == 0)
            {
                MessageBox.Show("Выберите сложность!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }
            else ProcessSelectedComplexity();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void bandedGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            BandedGridView view = sender as BandedGridView;
            var hit = view.CalcHitInfo(e.Location);
            if (!hit.InBandPanel || hit.Band == null) return;

            ResetBandColors(view);

            if (hit.Band.Caption.Contains("Сложность"))
            {
                if (_selectedBands.Contains(hit.Band))
                    _selectedBands.Remove(hit.Band);
                else
                    _selectedBands.Add(hit.Band);

                HighlightBand(hit.Band);
            }

            view.LayoutChanged();
        }
        private void ResetBandColors(BandedGridView view)
        {
            foreach (GridBand band in view.Bands)
            {
                band.AppearanceHeader.BackColor = Color.Empty;
                foreach (GridBand child in band.Children)
                    child.AppearanceHeader.BackColor = Color.Empty;
            }
        }
        private void HighlightBand(GridBand band)
        {
            var color = ThemeManager.ActiveTheme.BandHighlightColor;
            band.AppearanceHeader.BackColor = color;
            foreach (GridBand child in band.Children)
                child.AppearanceHeader.BackColor = color;
        }
        private void ProcessSelectedComplexity()
        {
            _bandDataList.Clear(); 

            foreach (var band in _selectedBands)
            {
                // Извлекаем индекс сложности из названия бэнда
                string complexityIndexStr = band.Caption.Last().ToString(); // Получаем последний символ
                int complexityIndex;
                if (!int.TryParse(band.Caption.Last().ToString(), out int complexity)) continue;

                if (!int.TryParse(complexityIndexStr, out complexityIndex))
                {
                    continue; // Пропускаем, если индекс не удалось извлечь
                }

                var selectedRows = gridView1.GetSelectedRows();
                foreach (int rowHandle in selectedRows)
                {
                    var drow = gridView1.GetRow(rowHandle) as DataRowView;
                    DataRow row = drow?.Row;
                    if (row != null)
                    {
                        BandData bandData = new BandData
                        {
                            gr = row["gr"] != DBNull.Value ? Convert.ToInt32(row["gr"]) : 0,
                            Naimen = row["naimen"]?.ToString() ?? "Не найдено",
                            Dras = GetIntValue(row, $"dras{complexityIndex}"),
                            Drez = GetIntValue(row, $"drez{complexityIndex}"),
                            Dpro = GetIntValue(row, $"dpro{complexityIndex}"),
                            Lras = GetIntValue(row, $"lras{complexityIndex}"),
                            Lrez = GetIntValue(row, $"lrez{complexityIndex}"),
                            Lpro = GetIntValue(row, $"lpro{complexityIndex}")
                        };

                        _bandDataList.Add(bandData); // Добавляем данные в список
                        SelectedData = GenerateNormRaskList(bandData, complexityIndex);
                    }
                }
            }
        }
        private int GetIntValue(DataRow row, string column) =>
    row[column] is DBNull ? 0 : int.TryParse(row[column]?.ToString(), out var val) ? val : 0;

        private List<NormRask> GenerateNormRaskList(BandData raskroyList, int _slogn)
        {
            int kol = raskroyList.gr;
            string obor = raskroyList.Naimen?.TrimEnd();
            return new List<NormRask>
            {
                new NormRask{IsNew = true, AnnId = -1, KodO = "301", Text = "Рассекание на куски диском", Sek = raskroyList.Dras, Razryad =  5, N_ch = kol, Obor = obor, Seb = 0, N = 0, N1 = 0,Seb_s = 0, Spec = ""},
                new NormRask{IsNew = true, AnnId = -1, KodO = "302", Text = "Резка диском", Sek = raskroyList.Drez, Razryad = 5, N_ch = kol, Obor = obor, Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{IsNew = true, AnnId = -1, KodO = "303", Text = "До проймы диск", Sek = raskroyList.Dpro, Razryad =  5, N_ch = kol, Obor =  obor, Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{IsNew = true, AnnId = -1, KodO = "311", Text = "Рассекание на куски (лента)",Sek = raskroyList.Lras, Razryad = 5,  N_ch = kol, Obor = obor, Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{IsNew = true, AnnId = -1, KodO = "313", Text = "До проймы (лента)", Sek = raskroyList.Lpro, Razryad =5,  N_ch = kol, Obor = obor, Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{IsNew = true, AnnId = -1, KodO = "320", Text = "Перекладывание деталей", Sek = 455, Razryad =5,  N_ch = kol, Obor = obor, Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{IsNew = true, AnnId = -1, KodO = "330", Text = "Перекладывание деталей/полоска", Sek = _slogn == 1 ? 2275 : _slogn == 2 ? 3000 : 3600, Razryad =5,  N_ch = kol, Obor = obor, Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{IsNew = true, AnnId = -1, KodO = "400", Text = "Укладывание шаблона", Sek = 150, Razryad =5,  N_ch = kol, Obor = obor, Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{IsNew = true, AnnId = -1, KodO = "500", Text = "Вырезание шаблона", Sek = 320, Razryad =5,  N_ch = kol, Obor = obor , Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{IsNew = true, AnnId = -1, KodO = "340", Text = "Разрезание вруч.парных дет/пол", Sek = 300, Razryad =5,  N_ch = kol, Obor = obor, Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = "" } 
            };
        }

        private void norm_raskrNew_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
} 