using DevExpress.Charts.Model;
using DevExpress.Utils.DPI;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SewingProduction.Core.interfaces;
using SewingProduction.Extensions;
using SewingProduction.Features.CuttingProduction.Forms;
using SewingProduction.Features.Tabel.Models;
using SewingProduction.Features.Tabel.Services;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.Tabel.Forms
{
    public partial class TabelMain : Form
    {
        private string currentMG = "1225";
        private static DatabaseHelper _dbHelper;
        private static DbService _dbService;
        private readonly ILogger _logger = new FileLogger();
        private static TabelDataService _tabelDataService;
        public BindingSource _zlPodr;
        public BindingSource _spPodr;
        private BindingSource _timeSheetBindingSource;
        private BindingList<TimeSheet> _TimeSheetBindingList;
        private List<TimeSheet> _currentTimeSheetData = new List<TimeSheet>();
        private readonly string[] allStates = new[]
        {
            " ",   // Пусто
            "1",   // 8 часов
 //           "11",  // 11 часов
            "Б",   // Больничный
            "К",   // Командировка
            "У",   // Учебный отпуск
            "Н",   // Неявка
            "О",   // Основной отпуск
            "П",   // Праздничный
            "Б/С", // Без содержания
            "Г",   // Прогул
            "НБ"   // Отстранение от работы без начисления ЗП
        };
        private Dictionary<string, int> cellStateIndices = new Dictionary<string, int>();
        public TabelMain()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);
            _tabelDataService = new TabelDataService(_dbHelper);
        }

        private void OpenSpisokButton_Click(object sender, EventArgs e)
        {
            SpisokConnect FDI = new SpisokConnect();

            DialogResult result = FDI.ShowDialog();
        }
        private void CreateDayColumns()
        {
            DateTime currentDate = DateTime.Now;
            int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
            for (int day = 1; day <= daysInMonth; day++)
            {
                GridColumn dayColumn = new GridColumn();
                string fieldName = $"d{day.ToString("00")}";
                dayColumn.FieldName = fieldName;
                dayColumn.Caption = day.ToString();
                dayColumn.Visible = true;
                dayColumn.OptionsColumn.AllowEdit = false;
                gridView1.Columns.Add(dayColumn);

            }
            GridColumn itogColumnD = new GridColumn();
            itogColumnD.FieldName = "it_d";
            itogColumnD.Caption = "Ит(Д)";
            itogColumnD.Visible = true;
            gridView1.Columns.Add(itogColumnD);
            GridColumn itogColumnChas = new GridColumn();
            itogColumnChas.FieldName = "it_ch";
            itogColumnChas.Caption = "Ит(Ч)";
            itogColumnChas.Visible = true;
            gridView1.Columns.Add(itogColumnChas);
            gridView1.BestFitColumns();

        }

        private async void TabelMain_Load(object sender, EventArgs e)
        {
            CreateDayColumns();
            Task bindingsTask = InitializeBindingsAsync();
            await Task.WhenAll(bindingsTask);
        }
        private async Task InitializeBindingsAsync()
        {
            var spisokViewTask = Task.Run(() =>
            {
                _TimeSheetBindingList = new BindingList<TimeSheet>();
                _timeSheetBindingSource = new BindingSource { DataSource = _TimeSheetBindingList };
            });
            await Task.WhenAll(spisokViewTask);
            //var zlPodr = await _tabelDataService.GetzlPodrAsync();
            //_zlPodr = new BindingSource { DataSource = zlPodr.ToList() };
            //lookUpEditGr.Properties.DataSource = zlPodr;
            //lookUpEditGr.Properties.DisplayMember = "naimen";
            //lookUpEditGr.Properties.ValueMember = "gr";
            UpdateMonthYearLabels(currentMG);
            var SpPodr = await _tabelDataService.GetSpPodrAsync();
            _spPodr = new BindingSource { DataSource = SpPodr.ToList() };
            lookUpEditGr.Properties.DataSource = _spPodr;
            lookUpEditGr.Properties.DisplayMember = "naimen";
            lookUpEditGr.Properties.ValueMember = "tnid";
            #region Увязка грида
            customGridControlTimeSheet.DataSource = _timeSheetBindingSource;
            gridColumnDd1.FieldName = "dd01";
            gridColumnDd2.FieldName = "dd02";
            gridColumnDd3.FieldName = "dd03";
            gridColumnDd4.FieldName = "dd04";
            gridColumnDd5.FieldName = "dd05";
            gridColumnDd6.FieldName = "dd06";
            gridColumnDd7.FieldName = "dd07";
            gridColumnDd8.FieldName = "dd08";
            gridColumnDd9.FieldName = "dd09";
            gridColumnDd10.FieldName = "dd10";
            gridColumnDd11.FieldName = "dd11";
            gridColumnDd12.FieldName = "dd12";
            gridColumnDd13.FieldName = "dd13";
            gridColumnDd14.FieldName = "dd14";
            gridColumnDd15.FieldName = "dd15";
            gridColumnDd16.FieldName = "dd16";
            gridColumnDd17.FieldName = "dd17";
            gridColumnDd18.FieldName = "dd18";
            gridColumnDd19.FieldName = "dd19";
            gridColumnDd20.FieldName = "dd20";
            gridColumnDd21.FieldName = "dd21";
            gridColumnDd22.FieldName = "dd22";
            gridColumnDd23.FieldName = "dd23";
            gridColumnDd24.FieldName = "dd24";
            gridColumnDd25.FieldName = "dd25";
            gridColumnDd26.FieldName = "dd26";
            gridColumnDd27.FieldName = "dd27";
            gridColumnDd28.FieldName = "dd28";
            gridColumnDd29.FieldName = "dd29";
            gridColumnDd30.FieldName = "dd30";
            gridColumnDd31.FieldName = "dd31";
            gridColumnTabno.FieldName = "tab";
            gridColumnFio.FieldName = "fio";
            gridColumnTsplPart.FieldName = "tsplPart";
            gridColumnTsplPartOf.FieldName = "tsplPartOf";
            gridColumnFio.Width = 90;
            gridView1.OptionsView.EnableAppearanceEvenRow = false;
            gridView1.OptionsView.EnableAppearanceOddRow = false;
            #endregion
        }
        //private void FormatGridBiewBeforeLoad()
        //{
        //    for (int rowHandle = 0; rowHandle < gridView1.DataRowCount; rowHandle++)
        //    {
        //        for (int day = 1; day <= 31; day++)
        //        {
        //            string columnNameD = $"d{day}";
        //            string columnNameDd = $"dd{day}";
        //            GridColumn columnD = gridView1.Columns.ColumnByFieldName(columnNameD);
        //            GridColumn columnDd = gridView1.Columns.ColumnByFieldName(columnNameDd);
        //            if (columnD != null && columnDd != null)
        //            {
        //                var cellValueDd = gridView1.GetRowCellValue(rowHandle, columnDd);
        //                if (cellValueDd != null)
        //                {
        //                    bool isHoliday = cellValueDd.ToString().Contains("/3");


        //                }
        //            }
        //        }

        //    }

        //}

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column != null && e.Column.FieldName.StartsWith("d"))
            {
                string dayNumber = e.Column.FieldName.Substring(1);
                if (int.TryParse(dayNumber, out int day) && day >= 1 && day <= 31)
                {
                    string markColumnName = $"dd{day.ToString("00")}"; ;
                    object markValue = gridView1.GetRowCellValue(e.RowHandle, markColumnName);

                    if (markValue != null)
                    {
                        string[] workingValue = { "1", "11", "8" };
                        bool IsWorkingDay = workingValue.Any(v => markValue.ToString().Contains(v));
                        bool IsHoliday = markValue.ToString().Contains("/3");
                        bool IsOrderBasedLeave = markValue.ToString().Contains("/4");
                        if (IsHoliday) { e.Appearance.BackColor = Color.FromArgb(128, 128, 255); }
                        else
                        {
                            if (IsOrderBasedLeave) { e.Appearance.BackColor = Color.FromArgb(255, 128, 64); }
                            else
                            {
                                if (IsWorkingDay) { e.Appearance.BackColor = Color.FromArgb(255, 255, 255); }
                                else
                                { e.Appearance.BackColor = Color.FromArgb(255, 128, 128); }
                            }
                        }
                    }
                }
            }
        }
        private async Task GetTimeSheet(string mg, int gr)
        {
            _timeSheetBindingSource.Clear();
            _timeSheetBindingSource.ResetBindings(false);
            var TimeSheetData = await _tabelDataService.GetTabelGrAsync(mg, gr);
            if (TimeSheetData != null)
            {
                await _logger.LogEventAsync($"Получены данные ListForLinking", "LoadCuttingForm");

                await this.InvokeAsync(() =>
                {
                    _currentTimeSheetData = TimeSheetData;                // Обновляем текущую модель
                    _timeSheetBindingSource.DataSource = _currentTimeSheetData; // Привязываем данные к форме
                });

                await _logger.LogEventAsync($"Данные ListForLinking успешно загружены", "LoadVyazPlanDataAsync");
                //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
                //_spisokBindingList.Add(spisokData[0]);
                _timeSheetBindingSource.ResetBindings(false);

            }
            else
            {
                await _logger.LogEventAsync($"Не удалось найти данные spisok", "GetListForLinkingsAsync");
            }
        }
        private void lookUpEditGr_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;

            if (lookUp.EditValue != null)
            {
                // Получаем ID (ValueMember)
                int grId = Convert.ToInt32(lookUp.EditValue);
                if (!_tabelDataService.CheckRecordsExistsTabel(currentMG, grId))
                {
                    MessageBox.Show("В данном подразделении нет данных за выбранный месяц!");
                    return;
                }
                try
                {
                    GetTimeSheet(currentMG, grId);
                }
                catch { }

            }
        }

        private void customButton1_Click(object sender, EventArgs e)
        {
            int grId = Convert.ToInt32(lookUpEditGr.EditValue);
            if (!_tabelDataService.CheckRecordsExistsTabel(GetPreviousMonth(currentMG), grId))
            {
                MessageBox.Show("В предыдущем месяце пусто!");
                return;
            }
            currentMG = GetPreviousMonth(currentMG);
            UpdateMonthYearLabels(currentMG);
            GetTimeSheet(currentMG, grId);
        }
        private string GetPreviousMonth(string mg)
        {
            // Пример: "1225" → месяц=12, год=2025
            int month = int.Parse(mg.Substring(0, 2));
            int year = 2000 + int.Parse(mg.Substring(2, 2));

            // Переход на месяц назад
            DateTime date = new DateTime(year, month, 1).AddMonths(-1);

            // Возвращаем в формате MG
            return date.ToString("MM") + date.ToString("yy");
        }

        // Получить следующий месяц в формате MG
        private string GetNextMonth(string mg)
        {
            int month = int.Parse(mg.Substring(0, 2));
            int year = 2000 + int.Parse(mg.Substring(2, 2));

            DateTime date = new DateTime(year, month, 1).AddMonths(1);

            return date.ToString("MM") + date.ToString("yy");
        }

        private void NextMonthButton_Click(object sender, EventArgs e)
        {
            int grId = Convert.ToInt32(lookUpEditGr.EditValue);
      
            if (!_tabelDataService.CheckRecordsExistsTabel(GetNextMonth(currentMG), grId))
            {
                MessageBox.Show("В следующем месяце пусто!");
                return;
            }
            currentMG = GetNextMonth(currentMG);
            UpdateMonthYearLabels(currentMG);
            GetTimeSheet(currentMG, grId);
        }
        private string GetMonthName(int month)
        {

            string[] monthNames = {
            "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
            "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
                 };

            if (month >= 1 && month <= 12)
                return monthNames[month - 1];

            return "Неизвестно";
        }
        private void UpdateMonthYearLabels(string mg)
        {
            // Извлекаем месяц и год из MG
            int month = int.Parse(mg.Substring(0, 2));    // Пример: "12" из "1225"
            int year = 2000 + int.Parse(mg.Substring(2, 2)); // Пример: 2025 из "1225"

            // Обновляем лейбл месяца (название месяца)
            string monthName = GetMonthName(month);
            customLabelMonth.Text = "МЕСЯЦ - " + monthName;

            // Обновляем лейбл года
            customLabelYear.Text = "ГОД - " + year.ToString();
        }
        private void CycleCellState(int rowHandle, GridColumn column)
        {
            try
            {
                int dayNumber = int.Parse(column.FieldName.Substring(1));
                string markColumnName = $"dd{dayNumber.ToString("00")}";
                string markValue = gridView1.GetRowCellValue(rowHandle, markColumnName).ToString();
                
                object koefValue = gridView1.GetRowCellValue(rowHandle, "tsplPartOf");
                int kol_chas = (int)(int.Parse(markValue) * decimal.Parse(koefValue.ToString()));
                string newValue = kol_chas.ToString().Trim();
                allStates[1] = newValue;
                // Получаем текущее значение ячейки
                object currentValue = gridView1.GetRowCellValue(rowHandle, column);
                string currentState = currentValue?.ToString() ?? " ";

                // Создаем уникальный ключ для ячейки
                string cellKey = $"{rowHandle}_{column.FieldName}";

                // Находим текущий индекс состояния
                int currentIndex = Array.IndexOf(allStates, currentState);

                // Если состояние не найдено в списке, начинаем с первого
                if (currentIndex == -1)
                {
                    currentIndex = 0;
                }
                else
                {
                   
                        // Переходим к следующему состоянию
                        currentIndex = (currentIndex + 1) % allStates.Length;
                }

                // Получаем новое состояние

                string newState = allStates[currentIndex].ToString();
                // Устанавливаем новое значение
                gridView1.SetRowCellValue(rowHandle, column, newState);

                // Сохраняем текущий индекс для этой ячейки
                cellStateIndices[cellKey] = currentIndex;

                // Обновляем отображение (для форматирования)
                gridView1.RefreshRowCell(rowHandle, column);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при переборе состояния: {ex.Message}");
            }
        }

        private void customGridControlTimeSheet_DoubleClick(object sender, EventArgs e)
        {
            GridHitInfo hitInfo = gridView1.CalcHitInfo(gridView1.GridControl.PointToClient(Control.MousePosition));

            if (hitInfo.InRowCell && hitInfo.Column.FieldName.StartsWith("d"))
            {
                CycleCellState(hitInfo.RowHandle, hitInfo.Column);
            }
        }
       
    }
}
