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
        private readonly ArtNormService _artNormService;
        private readonly ILogger _logger = new FileLogger();
        private BindingSource _raskroyNormBindingSource;
        private readonly GridHelper _gridHelper = new GridHelper();
        public List<NormRask> SelectedData { get; private set; } = new List<NormRask>();
        private List<GridBand> _selectedBands = new List<GridBand>(); // Список для хранения выбранных бэндов
        private List<BandData> _bandDataList = new List<BandData>(); // Список для хранения данных выбранных бэндов

        public struct BandData
        {
            public int gr {  get; set; }
         //   public string Kod { get; set; }
            public string Naimen { get; set; }
            public int Dras { get; set; }
            public int Drez { get; set; }
            public int Dpro { get; set; }
            public int Lras { get; set; }
            public int Lrez { get; set; }
            public int Lpro { get; set; }
        }

        public norm_raskrNew()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _artNormService = new ArtNormService(_dbHelper);
            ThemeManager.UpdateTheme(this);
            InitializeBindings();
            SetupGridColumns();
            LoadData(1);
            LoadGroups();
            //LoadGridSettings();
            // Загружаем настройки грида перед заполнением данными
            _gridHelper.LoadGridViewSettings(gridView1, "NormRaskrGrid.xml");

        }

        private void InitializeBindings()
        {
            _raskroyNormBindingSource = new BindingSource();
            gridControl1.DataSource = _raskroyNormBindingSource;

            // Добавляем обработчик клика по band
            var bandedView = gridControl1.MainView as BandedGridView;
            if (bandedView != null)
            {
                bandedView.MouseDown += bandedGridView1_MouseDown;
            }
        }

        private void SetupGridColumns()
        {
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowIndicator = false;
            gridView1.OptionsView.ShowAutoFilterRow = true;
        }

        private async void LoadGroups()
        {
            try
            {
                DataTable data = await _artNormService.GetRaskroyNormGroups();
                if (data != null && data.Rows.Count > 0)
                {
                    customComboBox1.DataSource = data;
                    customComboBox1.DisplayMember = "naimen";
                    customComboBox1.ValueMember = "gr";
                    customComboBox1.SelectedIndexChanged += CustomComboBox1_SelectedIndexChanged;
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
        }

        private void bandedGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                BandedGridView view = sender as BandedGridView;
                BandedGridHitInfo hitInfo = view.CalcHitInfo(e.Location);

                if (hitInfo.InBandPanel && hitInfo.Band != null)
                {
                    // Сначала сбрасываем цвет для всех колонок и бэндов
                    ResetAllColors(view);

                    // Проверяем, является ли кликнутый бэнд бэндом сложности
                    if (hitInfo.Band.Caption.Contains("Сложность"))
                    {
                        // Если кликнули по тому же бэнду, который уже выбран - снимаем выделение
                        if (_selectedBands.Contains(hitInfo.Band))
                        {
                            _selectedBands.Remove(hitInfo.Band); // Удаляем бэнд из списка
                        }
                        else
                        {
                            _selectedBands.Add(hitInfo.Band); // Добавляем бэнд в список
                        }

                        Color highlightColor = ThemeManager.ActiveTheme.BandHighlightColor;

                        // Подсвечиваем выбранный бэнд сложности
                        if (_selectedBands.Contains(hitInfo.Band))
                        {
                            hitInfo.Band.AppearanceHeader.BackColor = highlightColor;

                            // Подсвечиваем вложенные бэнды
                            foreach (GridBand childBand in hitInfo.Band.Children)
                            {
                                childBand.AppearanceHeader.BackColor = highlightColor;
                            }
                        }
                    }

                    view.LayoutChanged(); // Обновляем отображение
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при обработке клика по band").Wait();
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProcessSelectedComplexity()
        {
            _bandDataList.Clear(); // Очищаем список перед добавлением новых данных

            foreach (var band in _selectedBands)
            {
                Debug.WriteLine($"Обрабатываем данные для бэнда: {band.Caption}");

                // Извлекаем индекс сложности из названия бэнда
                string complexityIndexStr = band.Caption.Last().ToString(); // Получаем последний символ
                int complexityIndex;
                if (!int.TryParse(complexityIndexStr, out complexityIndex))
                {
                    Debug.WriteLine($"Не удалось извлечь индекс сложности из бэнда: {band.Caption}");
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
                        Debug.WriteLine($"Операция: {bandData.Naimen}, Время: {bandData.Dras}");

                        SelectedData = GenerateNormRaskList(bandData, complexityIndex);
                    }
                }
            }
        }

        private int GetIntValue(DataRow row, string columnName)
        {
            try
            {
                object rawValue = row[columnName];
                if (rawValue is DBNull)
                    return 0;

                string value = rawValue.ToString().Trim();
                if (string.IsNullOrEmpty(value))
                    return 0;

                return int.TryParse(value, out int result) ? result : 0;
            }
            catch { return 0; }
        }
        private List<NormRask> GenerateNormRaskList(BandData raskroyList, int _slogn)
        {
            
            return new List<NormRask>
            {
                new NormRask{AnnId = -1, KodO = "301", Text = "Рассекание на куски диском", Sek = raskroyList.Dras, Razryad =  5, N_ch = raskroyList.gr, Obor = raskroyList.Naimen.TrimEnd(), Seb = 0, N = 0, N1 = 0,Seb_s = 0, Spec = ""},
                new NormRask{AnnId = -1, KodO = "302", Text = "Резка диском", Sek = raskroyList.Drez, Razryad = 5, N_ch = raskroyList.gr, Obor = raskroyList.Naimen.TrimEnd(), Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{AnnId = -1, KodO = "303", Text = "До проймы диск", Sek = raskroyList.Dpro, Razryad =  5, N_ch = raskroyList.gr, Obor =  raskroyList.Naimen.TrimEnd(), Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{AnnId = -1, KodO = "311", Text = "Рассекание на куски (лента)",Sek = raskroyList.Lras, Razryad = 5,  N_ch = raskroyList.gr, Obor = raskroyList.Naimen.TrimEnd(), Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{AnnId = -1, KodO = "313", Text = "До проймы (лента)", Sek = raskroyList.Lpro, Razryad =5,  N_ch = raskroyList.gr, Obor = raskroyList.Naimen.TrimEnd(), Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{AnnId = -1, KodO = "320", Text = "Перекладывание деталей", Sek = 455, Razryad =5,  N_ch = raskroyList.gr, Obor = raskroyList.Naimen.TrimEnd(), Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{AnnId = -1, KodO = "330", Text = "Перекладывание деталей/полоска", Sek = _slogn == 1 ? 2275 : _slogn == 2 ? 3000 : 3600, Razryad =5,  N_ch = raskroyList.gr, Obor = raskroyList.Naimen.TrimEnd(), Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{AnnId = -1, KodO = "400", Text = "Укладывание шаблона", Sek = 150, Razryad =5,  N_ch = raskroyList.gr, Obor = raskroyList.Naimen.TrimEnd(), Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{AnnId = -1, KodO = "500", Text = "Вырезание шаблона", Sek = 320, Razryad =5,  N_ch = raskroyList.gr, Obor = raskroyList.Naimen.TrimEnd() , Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = ""},
                new NormRask{AnnId = -1, KodO = "340", Text = "Разрезание вруч.парных дет/пол", Sek = 300, Razryad =5,  N_ch = raskroyList.gr, Obor = raskroyList.Naimen.TrimEnd(), Seb = 0, N = 0, N1 = 0, Seb_s = 0, Spec = "" } 
            };
        }
        private void ResetAllColors(BandedGridView view)
        {
            // Сбрасываем цвет для всех колонок
            foreach (BandedGridColumn col in view.Columns)
            {
                col.AppearanceHeader.BackColor = Color.Empty;
            }

            // Сбрасываем цвет для всех бэндов
            foreach (GridBand band in view.Bands)
            {
                band.AppearanceHeader.BackColor = Color.Empty;
                foreach (GridBand childBand in band.Children)
                {
                    childBand.AppearanceHeader.BackColor = Color.Empty;
                }
            }
        }

        private void norm_raskrNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Сохраняем настройки грида при закрытии формы
                _gridHelper.SaveGridViewSettings(gridView1, "NormRaskrGrid.xml");
            }

            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при сохранении настроек грида");
            }

        }
    }
} 