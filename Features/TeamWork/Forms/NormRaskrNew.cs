using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.BandedGrid;
using SewingProduction.Features.TeamWork.Services;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;

namespace SewingProduction.form.TeamWork.Forms
{
    public partial class norm_raskrNew : CustomForm
    {
        private readonly DatabaseHelperSQL _dbHelper;
        private readonly DbService _dbService;
        private readonly ArtNormRepository _artNormService;
        private readonly ILogger _logger = new FileLogger();
        private BindingSource _raskroyNormBindingSource;
        private readonly TWGridHelper _gridHelper = new TWGridHelper();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<NormRask> SelectedData { get; private set; } = new List<NormRask>();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int? SelectedComplexity { get; private set; }
        private List<GridBand> _selectedBands = new List<GridBand>(); // Список для хранения выбранных бэндов
        private List<BandData> _bandDataList = new List<BandData>(); // Список для хранения данных выбранных бэндов
        private int _annId;
        private bool _loadingGroups;
        public struct BandData
        {
            public int gr { get; set; }
            public string Naimen { get; set; }
            public int Dras { get; set; }
            public int Drez { get; set; }
            public int Dpro { get; set; }
            public int Lras { get; set; }
            public int Lrez { get; set; }
            public int Lpro { get; set; }
        }
        public norm_raskrNew()
        { InitializeComponent(); }
        public norm_raskrNew(int annId)
        {
            InitializeComponent();
            _annId = annId;

			var databaseServices = TeamWorkDependencyFactory.CreateDatabaseServices();
            _dbHelper = databaseServices.DbHelper;
            _dbService = new DbService(new DatabaseHelperSQL());
            _artNormService = new ArtNormRepository(new DatabaseHelperSQL());
            //ThemeManager.UpdateTheme(this);
            // Загружаем настройки грида перед загрузкой данных
            ConfigureGrid();
            _gridHelper.LoadGridViewSettings(gridView1, "NormRaskrGrid.xml");
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
                _loadingGroups = true;
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
            finally
            {
                _loadingGroups = false;
            }
        }

        private async void CustomComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loadingGroups)
            {
                return;
            }

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
            // Используем системный цвет подсветки вместо кастомной темы
            var color = SystemColors.Highlight;
            band.AppearanceHeader.BackColor = color;
            foreach (GridBand child in band.Children)
                child.AppearanceHeader.BackColor = color;
        }
        private void ProcessSelectedComplexity()
        {
            _bandDataList.Clear();
            SelectedComplexity = null;
            SelectedData = new List<NormRask>();

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

                SelectedComplexity = complexityIndex;

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
                        SelectedData.AddRange(GenerateNormRaskList(bandData, complexityIndex));
                    }
                }
            }
        }

        private void norm_raskrNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Сохраняем настройки грида при закрытии
                _gridHelper.SaveGridViewSettings(gridView1, "NormRaskrGrid.xml");
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при сохранении настроек грида NormRaskr");
            }
        }
        private int GetIntValue(DataRow row, string column) =>
    row[column] is DBNull ? 0 : int.TryParse(row[column]?.ToString(), out var val) ? val : 0;

        private List<NormRask> GenerateNormRaskList(BandData raskroyList, int _slogn)
        {
            int kol = raskroyList.gr;
            string obor = StringNormalizer.TrimEndOrNull(raskroyList.Naimen);
            return new List<NormRask>
            {
                new NormRask{IsNew = true, AnnId = -1, Kod_o = "301", TextRask = "Рассекание на куски диском", Sek = raskroyList.Dras, razryd =  5, N_ch = kol, Obor = obor, Seb = 0, N = 0, N1 = 0,Seb_s = 0, Spec = ""},
                new NormRask{IsNew = true, AnnId = -1, Kod_o = "302", TextRask = "Резка диском", Sek = raskroyList.Drez, razryd = 5, N_ch = kol, Obor = obor, Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{IsNew = true, AnnId = -1, Kod_o = "303", TextRask = "До проймы диск", Sek = raskroyList.Dpro, razryd =  5, N_ch = kol, Obor =  obor, Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{IsNew = true, AnnId = -1, Kod_o = "311", TextRask = "Рассекание на куски (лента)",Sek = raskroyList.Lras, razryd = 5,  N_ch = kol, Obor = obor, Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{IsNew = true, AnnId = -1, Kod_o = "313", TextRask = "До проймы (лента)", Sek = raskroyList.Lpro, razryd =5,  N_ch = kol, Obor = obor, Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{IsNew = true, AnnId = -1, Kod_o = "320", TextRask = "Перекладывание деталей", Sek = 455, razryd =5,  N_ch = kol, Obor = obor, Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{IsNew = true, AnnId = -1, Kod_o = "330", TextRask = "Перекладывание деталей/полоска", Sek = _slogn == 1 ? 2275 : _slogn == 2 ? 3000 : 3600, razryd =5,  N_ch = kol, Obor = obor, Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{IsNew = true, AnnId = -1, Kod_o = "400", TextRask = "Укладывание шаблона", Sek = 150, razryd =5,  N_ch = kol, Obor = obor, Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{IsNew = true, AnnId = -1, Kod_o = "500", TextRask = "Вырезание шаблона", Sek = 320, razryd =5,  N_ch = kol, Obor = obor , Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{IsNew = true, AnnId = -1, Kod_o = "340", TextRask = "Разрезание вруч.парных дет/пол", Sek = 300, razryd =5,  N_ch = kol, Obor = obor, Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = "" }
            };
        }

    }
}
