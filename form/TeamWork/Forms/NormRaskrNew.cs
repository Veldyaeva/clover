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

namespace SewingProduction.form.TeamWork.Forms
{
    public partial class norm_raskrNew : CustomForm
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly ArtNormService _artNormService;
        private readonly ILogger _logger = new FileLogger();
        private BindingSource _raskroyNormBindingSource;
        public List<NormRask> SelectedData { get; private set; } = new List<NormRask>();
        private List<GridBand> _selectedBands = new List<GridBand>(); // Список для хранения выбранных бэндов
        private List<BandData> _bandDataList = new List<BandData>(); // Список для хранения данных выбранных бэндов

        public struct BandData
        {
            public int gr {  get; set; }
         //   public string Kod { get; set; }
            public string Naimen { get; set; }
            public string Dras { get; set; }
            public string Drez { get; set; }
            public string Dpro { get; set; }
            public string Lras { get; set; }
            public string Lrez { get; set; }
            public string Lpro { get; set; }
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

                    // Отладочный вывод для проверки столбцов
                    Debug.WriteLine("Доступные столбцы в DataTable:");
                    foreach (DataColumn column in data.Columns)
                    {
                        Debug.WriteLine($"Столбец: {column.ColumnName}");
                    }
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
                MessageBox.Show("Выберите хотя бы одну сложность!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ProcessSelectedComplexity();
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
                    var row = gridView1.GetRow(rowHandle) as DataRowView;
                    if (row != null)
                    {
                        BandData bandData = new BandData
                        {
                            gr = row["gr"] != DBNull.Value ? Convert.ToInt32(row["gr"]) : 0,
                            Naimen = row["naimen"]?.ToString() ?? "Не найдено",
                            Dras = row[$"dras{complexityIndex}"] != DBNull.Value ? row[$"dras{complexityIndex}"].ToString().TrimEnd(' '): "0",
                            Drez = row[$"drez{complexityIndex}"] != DBNull.Value ? row[$"drez{complexityIndex}"].ToString().TrimEnd(' '): "0",
                            Dpro = row[$"dpro{complexityIndex}"] != DBNull.Value ? row[$"dpro{complexityIndex}"].ToString().TrimEnd(' ') : "0",
                            Lras = row[$"lras{complexityIndex}"] != DBNull.Value ? row[$"lras{complexityIndex}"].ToString().TrimEnd(' ') : "0",
                            Lrez = row[$"lrez{complexityIndex}"] != DBNull.Value ? row[$"lrez{complexityIndex}"].ToString().TrimEnd(' '): "0",
                            Lpro = row[$"lpro{complexityIndex}"] != DBNull.Value ? row[$"lpro{complexityIndex}"].ToString().TrimEnd(' ') : "0"
                        };

                        _bandDataList.Add(bandData); // Добавляем данные в список
                        Debug.WriteLine($"Операция: {bandData.Naimen}, Время: {bandData.Dras}");

                        // Создаем экземпляр NormRask и добавляем в SelectedData
                        //SelectedData.Add(new NormRask
                        //{
                        //    AnnId = -1,
                        //    KodO = "301",
                        //    Text = bandData.Naimen,
                        //    Sek = bandData.Dras,
                        //    Razryad = 5,
                        //    n_ch = bandData.gr,
                        //    Obor = bandData.Naimen,
                        //});
                        SelectedData = GenerateNormRaskList(bandData, complexityIndex);
                    }
                }
            }
        }

        private List<NormRask> GenerateNormRaskList(BandData raskroyList, int _slogn)
        {
            return new List<NormRask>
            {
                new NormRask{annId = -1, kodO = "301", text = "Рассекание на куски диском", sek = Convert.ToInt32(raskroyList.Dras), razryad =  5, n_ch = raskroyList.gr, obor = raskroyList.Naimen, seb = 0, n = 0, n1 = 0,seb_s = 0, spec = ""},
                new NormRask{annId = -1, kodO = "302", text = "Резка диском", sek = Convert.ToInt32(raskroyList.Drez), razryad = 5, n_ch = raskroyList.gr, obor = raskroyList.Naimen, seb = 0, n = 0, n1 = 0, seb_s = 0, spec = ""},
                new NormRask{annId = -1, kodO = "303", text = "До проймы диск", sek = Convert.ToInt32(raskroyList.Dpro), razryad =  5, n_ch = raskroyList.gr, obor =  raskroyList.Naimen, seb = 0, n = 0, n1 = 0, seb_s = 0, spec = ""},
                new NormRask{annId = -1, kodO = "311", text = "Рассекание на куски (лента)",sek = Convert.ToInt32(raskroyList.Lras), razryad = 5,  n_ch = raskroyList.gr, obor = raskroyList.Naimen, seb = 0, n = 0, n1 = 0, seb_s = 0, spec = ""},
                new NormRask{annId = -1, kodO = "313", text = "До проймы (лента)", sek = Convert.ToInt32(raskroyList.Lpro), razryad =5,  n_ch = raskroyList.gr, obor = raskroyList.Naimen, seb = 0, n = 0, n1 = 0, seb_s = 0, spec = ""},
                new NormRask{annId = -1, kodO = "320", text = "Перекладывание деталей", sek = 455, razryad =5,  n_ch = raskroyList.gr, obor = raskroyList.Naimen, seb = 0, n = 0, n1 = 0, seb_s = 0, spec = ""},
                new NormRask{annId = -1, kodO = "330", text = "Перекладывание деталей/полоска", sek = _slogn == 1 ? 2275 : _slogn == 2 ? 3000 : 3600, razryad =5,  n_ch = raskroyList.gr, obor = raskroyList.Naimen, seb = 0, n = 0, n1 = 0, seb_s = 0, spec = ""},
                new NormRask{annId = -1, kodO = "400", text = "Укладывание шаблона", sek = 150, razryad =5,  n_ch = raskroyList.gr, obor = raskroyList.Naimen, seb = 0, n = 0, n1 = 0, seb_s = 0, spec = ""},
                new NormRask{annId = -1, kodO = "500", text = "Вырезание шаблона", sek = 320, razryad =5,  n_ch = raskroyList.gr, obor = raskroyList.Naimen , seb = 0, n = 0, n1 = 0, seb_s = 0, spec = ""},
                new NormRask{annId = -1, kodO = "340", text = "Разрезание вруч.парных дет/пол", sek = 300, razryad =5,  n_ch = raskroyList.gr, obor = raskroyList.Naimen, seb = 0, n = 0, n1 = 0, seb_s = 0, spec = "" } 
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
    }
} 