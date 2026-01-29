using DevExpress.Charts.Model;
using DevExpress.DataAccess.Sql;
using DevExpress.Utils;
using DevExpress.Utils.DPI;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraReports.UI;
using SewingProduction.Core.Class;
using SewingProduction.Core.helpers;
using SewingProduction.Core.interfaces;
using SewingProduction.Extensions;
using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Features.CuttingProduction.Forms;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Features.Tabel.Models;
using SewingProduction.Features.Tabel.Services;
using SewingProduction.Features.UserDistribution.Class;
using SewingProduction.Helpers;
using SewingProduction.Report;
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
using static SewingProduction.Core.helpers.BindingSourceHelper;

namespace SewingProduction.Features.Tabel.Forms
{
    public partial class TabelMain : CustomForm
    {
        private string currentMG = "1225";
        private static DatabaseHelper _dbHelper;
        private static DbService _dbService;
        private readonly ILogger _logger = new FileLogger();
        private static TabelDataService _tabelDataService;
        public BindingSource _zlPodr;
        public BindingSource _spPodr;
        private BindingSource _timeSheetBindingSource;
        private BindingSource _timeSheetFreshBindingSource;
        private BindingList<TimeSheet> _TimeSheetBindingList;
        private BindingList<TimeSheet> _timeSheetFreshBindingList;
        private List<TimeSheet> _currentTimeSheetData = new List<TimeSheet>();
        private List<TimeSheet> _TimeSheetDataFresh = new List<TimeSheet>();
        private List<SpPodr> _spPodrList = new List<SpPodr>();
        int idUser = CurrentUser.User.UserId;
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
        Dictionary<int, string> workTypes = new Dictionary<int, string>
        {
            { 19, "ШП" },
            { 20, "ЗЛ" }
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
        private void GetInfoGroup(int id)
        {


        }
        private void CreateDayColumns(string mg)
        {
            int month = int.Parse(mg.Substring(0, 2));
            int year = 2000 + int.Parse(mg.Substring(2, 2));
            int daysInMonth = DateTime.DaysInMonth(year, month);
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
            itogColumnD.FieldName = "tItogD";
            itogColumnD.Caption = "Ит(Д)";
            itogColumnD.Visible = true;
            itogColumnD.DisplayFormat.FormatString = "{0:0.00#;0:#;#}";
            itogColumnD.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            itogColumnD.OptionsColumn.AllowEdit = false;
            gridView1.Columns.Add(itogColumnD);
            GridColumn itogColumnChas = new GridColumn();
            itogColumnChas.FieldName = "tItogViewChas";
            itogColumnChas.Caption = "Ит(Ч)";
            itogColumnChas.Visible = true;
            itogColumnChas.DisplayFormat.FormatString = "{0:0.00#;0:#;#}";
            itogColumnChas.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            itogColumnChas.OptionsColumn.AllowEdit = false;
            gridView1.Columns.Add(itogColumnChas);
            gridView1.BestFitColumns();
        }

        private async void TabelMain_Load(object sender, EventArgs e)
        {
            CreateDayColumns(currentMG);
            Task bindingsTask = InitializeBindingsAsync();
            await Task.WhenAll(bindingsTask);
            CheckUserAccess(idUser);
        }
        private void RemoveDayColumns()
        {
            gridView1.BeginUpdate();

            var toRemove = gridView1.Columns
                .Cast<GridColumn>()
                .Where(c => (c.FieldName.StartsWith("d") && c.FieldName.Length == 3) || c.FieldName.StartsWith("tItog"))
                .ToList();

            foreach (var column in toRemove)
            {
                gridView1.Columns.Remove(column);
            }

            gridView1.EndUpdate();
        }

        private async Task InitializeBindingsAsync()
        {
            var timeSheetFresh = Task.Run(() =>
            {
                _timeSheetFreshBindingList = new BindingList<TimeSheet>();
                _timeSheetFreshBindingSource = new BindingSource { DataSource = _timeSheetFreshBindingList };
            });
            var spisokViewTask = Task.Run(() =>
            {
                _TimeSheetBindingList = new BindingList<TimeSheet>();
                _timeSheetBindingSource = new BindingSource { DataSource = _TimeSheetBindingList };
                _spPodr = new BindingSource { DataSource = _spPodrList };
            });
            await Task.WhenAll(spisokViewTask);
            //var zlPodr = await _tabelDataService.GetzlPodrAsync();
            //_zlPodr = new BindingSource { DataSource = zlPodr.ToList() };
            //lookUpEditGr.Properties.DataSource = zlPodr;
            //lookUpEditGr.Properties.DisplayMember = "naimen";
            //lookUpEditGr.Properties.ValueMember = "gr";
            UpdateMonthYearLabels(currentMG);
            lookUpEditGr.Properties.DataSource = _spPodr;
            lookUpEditGr.Properties.DisplayMember = "naimen";
            lookUpEditGr.Properties.ValueMember = "tnid";
            lookUpEditGroup.Properties.DataSource = new BindingSource(workTypes, null);
            lookUpEditGroup.Properties.DisplayMember = "Value";
            lookUpEditGroup.Properties.ValueMember = "Key";
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
            gridColumnCheckIncludePlan.FieldName = "ts_plan";


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
                await _logger.LogEventAsync($"Получены данные TimeSheet", "GetTabelGrAsync");

                await this.InvokeAsync(() =>
                {
                    _currentTimeSheetData = TimeSheetData;                // Обновляем текущую модель
                    _timeSheetBindingSource.DataSource = _currentTimeSheetData; // Привязываем данные к форме
                });

                await _logger.LogEventAsync($"Данные TimeSheet успешно загружены", "GetTabelGrAsync");
                //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
                //_spisokBindingList.Add(spisokData[0]);
                _timeSheetBindingSource.ResetBindings(false);

            }
            else
            {
                await _logger.LogEventAsync($"Не удалось найти данные TimeSheet", "GetTabelGrAsync");
            }
        }
        private async Task GetTimeSheetFresh(string mg, int gr)
        {
            _timeSheetFreshBindingSource.Clear();
            _timeSheetFreshBindingSource.ResetBindings(false);
            _TimeSheetDataFresh = await _tabelDataService.GetTabelGrAsync(mg, gr);
            if (_TimeSheetDataFresh != null)
            {
                await _logger.LogEventAsync($"Получены данные _timeSheetFreshBindingSource", "GetTimeSheetFresh");

                await this.InvokeAsync(() =>
                {
                    _timeSheetFreshBindingSource.DataSource = _TimeSheetDataFresh; // Привязываем данные к форме
                });

                await _logger.LogEventAsync($"Данные _timeSheetFreshBindingSource успешно загружены", "GetTimeSheetFresh");

            }
            else
            {
                await _logger.LogEventAsync($"Не удалось найти данные _timeSheetFreshBindingSource", "GetListForLinkingsAsync");
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
            RemoveDayColumns();
            CreateDayColumns(currentMG);
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
            RemoveDayColumns();
            CreateDayColumns(currentMG);
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
                object includePlan = gridView1.GetRowCellValue(rowHandle, "ts_plan");
                object koefValue = gridView1.GetRowCellValue(rowHandle, "tsplPartOf");
                int kol_chas;
                if (int.Parse(includePlan.ToString()) == 0)
                {
                    kol_chas = (int)(int.Parse(markValue) * 1);
                }
                else
                {
                    kol_chas = (int)(int.Parse(markValue) * decimal.Parse(koefValue.ToString()));
                }

                string newValue = kol_chas.ToString().Trim();
                allStates[1] = newValue;
                // Получаем текущее значение ячейки
                string currentValue = gridView1.GetRowCellValue(rowHandle, column).ToString().Trim();
                if (currentValue.Length == 0)
                {
                    currentValue = " ";
                }
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

        }

        private void gridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            GridView view = (GridView)sender;

            if (view.FocusedColumn != null &&
                view.FocusedColumn.FieldName.StartsWith("d"))
            {
                // Если нажата цифра
                if (char.IsDigit(e.KeyChar))
                {
                    HandleDigitInputForInteger(view, e.KeyChar);
                    e.Handled = true; // Обработали сами
                }
                // Если Backspace
                else if (e.KeyChar == (char)8)
                {
                    HandleBackspaceForInteger(view);
                    e.Handled = true;
                }
                // Запрещаем все остальные символы (кроме управляющих)
                else if (!char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void HandleDigitInputForInteger(GridView view, char digit)
        {
            int rowHandle = view.FocusedRowHandle;
            GridColumn column = view.FocusedColumn;

            // Получаем текущее значение
            object currentValue = view.GetRowCellValue(rowHandle, column);
            string currentText = currentValue?.ToString() ?? "";

            // Очищаем, если текущее значение не число
            if (!int.TryParse(currentText, out _))
            {
                currentText = "";
            }

            // Формируем новое значение
            string newText;
            if (currentText.Length == 0)
            {
                newText = digit.ToString();
            }
            else if (currentText.Length == 1)
            {
                // Проверяем, что число не больше 23
                string testValue = currentText + digit;
                if (int.TryParse(testValue, out int testInt) && testInt <= 23)
                {
                    newText = testValue;
                }
                else
                {
                    // Если больше 23, заменяем первую цифру
                    newText = digit.ToString();
                }
            }
            else
            {
                // Если уже 2 цифры, заменяем значение
                newText = digit.ToString();
            }

            // Проверяем, что число <= 23
            if (int.TryParse(newText, out int value) && value <= 23)
            {
                view.SetRowCellValue(rowHandle, column, newText);

                // Автоматически переходим к следующей ячейке при вводе двух цифр
                if (newText.Length == 2)
                {
                    MoveToNextCell(view);
                }
            }
        }
        private void HandleBackspaceForInteger(GridView view)
        {
            int rowHandle = view.FocusedRowHandle;
            GridColumn column = view.FocusedColumn;

            object currentValue = view.GetRowCellValue(rowHandle, column);
            string currentText = currentValue?.ToString() ?? "";

            if (currentText.Length > 0)
            {
                // Удаляем последнюю цифру
                string newText = currentText.Substring(0, currentText.Length - 1);
                view.SetRowCellValue(rowHandle, column, newText);
            }
            else
            {
                // Если строка пустая, очищаем ячейку
                view.SetRowCellValue(rowHandle, column, "");
            }
        }
        private void MoveToNextCell(GridView view)
        {
            // Переходим к следующей ячейке справа
            int currentColIndex = view.Columns.IndexOf(view.FocusedColumn);
            if (currentColIndex < view.Columns.Count - 1)
            {
                GridColumn nextColumn = view.Columns[currentColIndex + 1];
                if (nextColumn.Visible && nextColumn.OptionsColumn.AllowFocus)
                {
                    view.FocusedColumn = nextColumn;
                    view.ShowEditor();
                }
            }
        }



        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (IsDayColumn(e.Column))
            {
                int rowHandle = e.RowHandle;
                int recordId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "id"));
                string columnName = e.Column.FieldName;
                string value = gridView1.GetRowCellValue(rowHandle, columnName).ToString();
                UpdateDayTimeSheetInDataBase(columnName, value, recordId);

            }
            if (e.Column != null && e.Column.ColumnEdit is RepositoryItemCheckEdit)
            {
                int rowHandle = e.RowHandle;
                int valueChecked = Convert.ToInt32(e.Value);
                string fieldName = e.Column.FieldName;
                int recordId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "id"));

                UpdateCheckBoxInDataBase(recordId, fieldName, valueChecked);
            }
        }
        public void UpdateCheckBoxInDataBase(int id, string fieldName, int value)
        {
            //if (value == 1)
            //{
            //    string query = $"update tabel_sp set {fieldName} = {value},ts_plan_pr = '',ts_tsp_id = 0, tsPlPart = 1 where id = {id}";
            //    _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { });
            //}
            //else
            //{
            //    string query = $"update tabel_sp set {fieldName} = {value},tsPlPart = 0 where id = {id}";
            //    _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { });

            //}
        }
        public async void UpdateDayTimeSheetInDataBase(string fieldName, string value, int id)
        {
            try
            {
                string query = $"update tabel_sp set {fieldName} = '{value}' where id = {id} ";
                int grId = Convert.ToInt32(lookUpEditGr.EditValue);
                await _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { });
                await GetTimeSheetFresh(currentMG, grId);
                var changes = BindingSourceHelper.GetChanges<TimeSheet>(
                  _timeSheetBindingSource,
                  _timeSheetFreshBindingSource,
                  HashMode.All,
                  keyProperties: new[] { "id" });
                BindingSourceHelper.ApplyChanges<TimeSheet>(
                   _timeSheetBindingSource,
                   changes,
                   UpdateFieldsMode.All,
                   keyProperties: new[] { "id" },
                   gridView1);
            }
            catch (Exception ex)
            {
                await _logger.LogEventAsync($"Не удалось обновить tabel_sp", "UpdateDayTimeSheetInDataBase");
            }
        }
        private bool IsDayColumn(GridColumn column)
        {
            if (column == null) return false;

            // Проверяем формат d01, d02, ..., d31
            if (column.FieldName.StartsWith("d") && column.FieldName.Length == 3)
            {
                string dayPart = column.FieldName.Substring(1);
                return int.TryParse(dayPart, out int day) && day >= 1 && day <= 31;
            }
            return false;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridHitInfo hitInfo = gridView1.CalcHitInfo(gridView1.GridControl.PointToClient(Control.MousePosition));

            if (hitInfo.InRowCell && hitInfo.Column.FieldName.StartsWith("d"))
            {
                CycleCellState(hitInfo.RowHandle, hitInfo.Column);
            }

        }

        private void customButton1_Click_1(object sender, EventArgs e)
        {
            int grId = Convert.ToInt32(lookUpEditGr.EditValue);
            TimeSheetParsecOrionPrint report1 = new TimeSheetParsecOrionPrint();
            report1.RequestParameters = false;
            report1.Parameters["grid"].Value = grId;
            report1.Parameters["grid"].Visible = false;
            report1.Parameters["Mg"].Value = currentMG;
            report1.Parameters["Mg"].Visible = false;
            report1.Parameters["orionAdd"].Value = 0;
            report1.Parameters["orionAdd"].Visible = false;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();

        }

        private void customButton2_Click(object sender, EventArgs e)
        {
            int grId = Convert.ToInt32(lookUpEditGr.EditValue);
            TimeSheetParsecOrionPrint report1 = new TimeSheetParsecOrionPrint();
            report1.RequestParameters = false;
            report1.Parameters["grid"].Value = grId;
            report1.Parameters["grid"].Visible = false;
            report1.Parameters["Mg"].Value = currentMG;
            report1.Parameters["Mg"].Visible = false;
            report1.Parameters["orionAdd"].Value = 1;
            report1.Parameters["orionAdd"].Visible = false;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        private void customButton3_Click(object sender, EventArgs e)
        {
            int grId = Convert.ToInt32(lookUpEditGr.EditValue);
            TimeSheetReport report1 = new TimeSheetReport();
            report1.RequestParameters = false;
            report1.Parameters["ttabn"].Value = grId;
            report1.Parameters["ttabn"].Visible = false;
            report1.Parameters["MG"].Value = currentMG;
            report1.Parameters["MG"].Visible = false;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }
        public void CheckUserAccess(int id)
        {
            int resultCountGroup = _tabelDataService.CheckRecordsExistsGroupGr(id);
            if (resultCountGroup == 0)
            {
                MessageBox.Show("Нет прав!");
                return;
            }
            if (resultCountGroup == 2)
            {
                lookUpEditGroup.Enabled = true;
            }
            if (resultCountGroup == 1)
            {
                lookUpEditGroup.Visible = false;
                customLabel3.Visible = false;
                int resultGroupId = _tabelDataService.GetTabelGroupForUser(id);
                if (resultGroupId == 0)
                {
                    MessageBox.Show("Не удалось определить принадлежность");
                    return;
                }
                if (resultGroupId == 19)
                {
                    GetGrAcess(19, idUser);
                }
                if (resultGroupId == 20)
                {
                    GetGrAcess(20, idUser);
                }
            }
        }

        private void lookUpEditGroup_EditValueChanged(object sender, EventArgs e)
        {
            int grId = Convert.ToInt32(lookUpEditGroup.EditValue);
            GetGrAcess(grId, idUser);
        }
        private async Task GetGrAcess(int idGr, int idUser)
        {
            var SpPodr = await _tabelDataService.GetSpPodrAsync(idGr, idUser);
            _spPodr.Clear();
            _spPodr.ResetBindings(false);
            _spPodr.DataSource = SpPodr.ToList();
            lookUpEditGroup.Refresh();

        }

        private void customButtonOtvlRab_Click(object sender, EventArgs e)
        {
            int tnid = Convert.ToInt32(lookUpEditGr.EditValue);
            if (tnid == 0) return;
            if (this.MdiParent is SpMainForm mainForm)
            {
                mainForm.OpenForm(new OtvlRab(CurrentUser.User, tnid)); // пока для теста 3, поменять на tnid
            }
        }
    }
}
