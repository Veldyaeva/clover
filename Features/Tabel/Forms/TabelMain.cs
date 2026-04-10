using DevExpress.Charts.Model;
using DevExpress.Data.Utils;
using DevExpress.DataAccess.Sql;
using DevExpress.LookAndFeel.Design;
using DevExpress.Utils;
using DevExpress.Utils.DPI;
using DevExpress.Xpf.Editors;
using DevExpress.XtraDiagram.Bars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit.Model.History;
using Org.BouncyCastle.Asn1;
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
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Report;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Utils.Drawing.Helpers.NativeMethods;
using static SewingProduction.Core.helpers.BindingSourceHelper;

namespace SewingProduction.Features.Tabel.Forms
{
    public partial class TabelMain : CustomForm
    {

        private string currentMG;
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
        private List<WorkTypes> _workTypes = new List<WorkTypes>();
        int idUser = CurrentUser.User.UserId;
        private static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;
        private List<string> allStates = new List<string>();
        //       {
        //           " ",   // Пусто
        //           "1",   // 8 часов
        ////           "11",  // 11 часов
        //           "Б",   // Больничный
        //           "К",   // Командировка
        //           "У",   // Учебный отпуск
        //           "Н",   // Неявка
        //           "О",   // Основной отпуск
        //           "П",   // Праздничный
        //           "Б/С", // Без содержания
        //           "Г",   // Прогул
        //           "НБ"   // Отстранение от работы без начисления ЗП
        //       };
        Dictionary<int, string> workTypes = new Dictionary<int, string>
        {
            { 19, "ШП" },
            { 20, "ЗЛ" }
        };
        private Dictionary<string, int> cellStateIndices = new Dictionary<string, int>();
        public TabelMain(UserClass user) : base(user)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);
            _tabelDataService = new TabelDataService(_dbHelper);
            currentMG = GetCurrentMg();
        }

        private void OpenSpisokButton_Click(object sender, EventArgs e)
        {
            SpisokConnect FDI = new SpisokConnect();

            DialogResult result = FDI.ShowDialog();
        }
        private void GetInfoGroup(int id)
        {


        }
        private string GetCurrentMg()
        {
            DateTime today = DateTime.Today;
            return today.ToString("MMyy");
        }
        private void CreateDayColumns(string mg)
        {
            gridView1.BeginUpdate();
            //gridView1.BeginUpdate();
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
                //dayColumn.VisibleIndex = day + 4;
                dayColumn.OptionsColumn.AllowEdit = false;
                gridView1.Columns.Add(dayColumn);

            }
            for (int day = 1; day <= daysInMonth; day++)
            {
                GridColumn dayColumn = new GridColumn();
                string fieldName = $"dop{day.ToString("00")}";
                dayColumn.FieldName = fieldName;
                dayColumn.Caption = fieldName;
                dayColumn.Visible = false;
                dayColumn.OptionsColumn.AllowEdit = false;
                gridView1.Columns.Add(dayColumn);

            }
            GridColumn itogColumnD = new GridColumn();
            itogColumnD.FieldName = "tItogD";
            itogColumnD.Caption = "Ит(Д)";
            itogColumnD.Visible = true;
            itogColumnD.DisplayFormat.FormatString = "{0:0.00#;0:#;#}";
            itogColumnD.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            itogColumnD.OptionsColumn.AllowEdit = true;
            gridView1.Columns.Add(itogColumnD);
            GridColumn itogColumnChas = new GridColumn();
            itogColumnChas.FieldName = "tItogViewChas";
            itogColumnChas.Caption = "Ит(Ч)";
            itogColumnChas.Visible = true;
            itogColumnChas.DisplayFormat.FormatString = "{0:0.00#;0:#;#}";
            itogColumnChas.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            itogColumnChas.OptionsColumn.AllowEdit = false;
            gridView1.Columns.Add(itogColumnChas);
            GridColumn prichIncludePlan = new GridColumn();
            prichIncludePlan.FieldName = "tsp_naimen";
            prichIncludePlan.Caption = "Причина искл. из плана или неполная ставка";
            prichIncludePlan.Visible = true;
            prichIncludePlan.OptionsColumn.AllowEdit = false;
            gridView1.Columns.Add(prichIncludePlan);
            gridView1.BestFitColumns();
            gridView1.EndUpdate();

        }

        private async void TabelMain_Load(object sender, EventArgs e)
        {
            Task bindingsTask = InitializeBindingsAsync();
            await Task.WhenAll(bindingsTask);
            CreateDayColumns(currentMG);
            CheckUserAccess(idUser);
        }
        private void RemoveDayColumns()
        {
            gridView1.BeginUpdate();

            var toRemove = gridView1.Columns
                .Cast<GridColumn>()
                .Where(c => (c.FieldName.StartsWith("d") && c.FieldName.Length == 3) || c.FieldName.StartsWith("tItog") || c.FieldName.StartsWith("tsp_naim"))
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
            gridColumnUin.FieldName = "uin";
            gridColumnDlD.FieldName = "dl_d";
            gridColumnTsplPart.FieldName = "tsplPart";
            gridColumnTsplPart.DisplayFormat.FormatString = "0.00";
            gridColumnTsplPart.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            repositoryItemTextEdit1.Mask.EditMask = "f2";                     // 2 знака после запятой
            repositoryItemTextEdit1.Mask.UseMaskAsDisplayFormat = true;
            repositoryItemTextEdit1.Mask.Culture = System.Globalization.CultureInfo.CurrentCulture;
            repositoryItemTextEdit1.MaxLength = 4;
            repositoryItemTextEdit2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            repositoryItemTextEdit2.Mask.EditMask = "f2";                     // 2 знака после запятой
            repositoryItemTextEdit2.Mask.UseMaskAsDisplayFormat = true;
            repositoryItemTextEdit2.Mask.Culture = System.Globalization.CultureInfo.CurrentCulture;
            repositoryItemTextEdit2.MaxLength = 4;
            gridColumnTsplPartOf.FieldName = "tsplPartOf";
            gridColumnTsplPartOf.DisplayFormat.FormatString = "0.00";
            gridColumnTsplPartOf.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView1.OptionsView.ShowFooter = true;
            gridColumnCheckIncludePlan.FieldName = "ts_plan";
            gridColumnCheckIncludePlan.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridColumnCheckIncludePlan.SummaryItem.DisplayFormat = "План: {0}";
            gridColumnPodrTableID.FieldName = "podrTableID";
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
        private async Task GetTimeSheet(string mg, int gr, int groupId)
        {
            _timeSheetBindingSource.Clear();
            _timeSheetBindingSource.ResetBindings(false);
            var TimeSheetData = await _tabelDataService.GetTabelGrAsync(mg, gr, groupId);
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
                if (groupId == 19)
                {
                    DateTime dateTimeNow = DateTime.Now.Date;
                    DateTime? datetimeValue = _tabelDataService.GetDateReadOnlyDd(currentMG);
                    if (dateTimeNow <= datetimeValue)
                    {
                        foreach (GridColumn column in gridView1.Columns)
                        {
                            // Проверяем, что FieldName не пустой и начинается на "d" (без учета регистра)
                            if (!string.IsNullOrEmpty(column.FieldName) &&
                                column.FieldName.StartsWith("d", StringComparison.OrdinalIgnoreCase))
                            {
                                // Делаем колонку только для чтения
                                column.OptionsColumn.ReadOnly = false;

                            }
                        }
                    }
                    else
                    {

                        foreach (GridColumn column in gridView1.Columns)
                        {
                            // Проверяем, что FieldName не пустой и начинается на "d" (без учета регистра)
                            if (!string.IsNullOrEmpty(column.FieldName) &&
                                column.FieldName.StartsWith("d", StringComparison.OrdinalIgnoreCase))
                            {
                                // Делаем колонку только для чтения
                                column.OptionsColumn.ReadOnly = true;
                            }
                        }
                    }
                    DateTime? datetimeValueTarif = _tabelDataService.GetDateReadOnlyTarif(currentMG);
                    if (dateTimeNow <= datetimeValueTarif)
                    {
                        gridColumnTsplPart.OptionsColumn.ReadOnly = false;
                        gridColumnTsplPartOf.OptionsColumn.ReadOnly = false;
                        gridColumnCheckIncludePlan.OptionsColumn.ReadOnly = false;

                    }
                    else
                    {

                        gridColumnTsplPart.OptionsColumn.ReadOnly = true;
                        gridColumnTsplPartOf.OptionsColumn.ReadOnly = true;
                        gridColumnCheckIncludePlan.OptionsColumn.ReadOnly = true;
                    }


                }

            }
            else
            {
                await _logger.LogEventAsync($"Не удалось найти данные TimeSheet", "GetTabelGrAsync");
            }
        }
        private async Task GetTimeSheetFresh(string mg, int gr, int groupId)
        {
            _timeSheetFreshBindingSource.Clear();
            _timeSheetFreshBindingSource.ResetBindings(false);
            _TimeSheetDataFresh = await _tabelDataService.GetTabelGrAsync(mg, gr, groupId);
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
                int groupId = Convert.ToInt32(lookUpEditGroup.EditValue);
                int grId = Convert.ToInt32(lookUp.EditValue);
                if (!_tabelDataService.CheckRecordsExistsTabel(currentMG, grId))
                {
                    MessageBox.Show("В данном подразделении нет данных за выбранный месяц!");
                    return;
                }
                try
                {
                    GetTimeSheet(currentMG, grId, groupId);

                }
                catch { }

            }
        }

        private void customButton1_Click(object sender, EventArgs e)
        {
            int groupId = Convert.ToInt32(lookUpEditGroup.EditValue);
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
            GetTimeSheet(currentMG, grId, groupId);


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
            int groupId = Convert.ToInt32(lookUpEditGroup.EditValue);
            if (!_tabelDataService.CheckRecordsExistsTabel(GetNextMonth(currentMG), grId))
            {
                MessageBox.Show("В следующем месяце пусто!");
                return;
            }
            currentMG = GetNextMonth(currentMG);
            RemoveDayColumns();
            CreateDayColumns(currentMG);
            UpdateMonthYearLabels(currentMG);
            GetTimeSheet(currentMG, grId, groupId);
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
            customLabelMonth.Text = monthName;

            // Обновляем лейбл года
            customLabelYear.Text = year.ToString();
        }
        private void CycleCellState(int rowHandle, GridColumn column)
        {
            try
            {
                int dayNumber = int.Parse(column.FieldName.Substring(1));
                string markColumnName = $"dd{dayNumber.ToString("00")}";
                string markValue = gridView1.GetRowCellValue(rowHandle, markColumnName).ToString().Trim();
                int lenghtArray = allStates.Count;
                string mainMarkValue = "1";
                if (markValue == "1")
                {
                    mainMarkValue = "8";
                }
                if (markValue == "11")
                {
                    mainMarkValue = "11";
                }
                object includePlan = gridView1.GetRowCellValue(rowHandle, "ts_plan");
                object koefValue = gridView1.GetRowCellValue(rowHandle, "tsplPartOf");
                int kol_chas;
                if (int.Parse(includePlan.ToString()) == 0)
                {
                    kol_chas = (int)(int.Parse(mainMarkValue) * 1);
                }
                else
                {
                    kol_chas = (int)(int.Parse(markValue) * decimal.Parse(koefValue.ToString()));
                }

                string newValue = kol_chas.ToString().Trim();
                int index = allStates.FindIndex(s =>
                !string.IsNullOrEmpty(s) && s.All(char.IsDigit));
                allStates[index] = newValue;

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
                int currentIndex = allStates.IndexOf(currentState);

                // Если состояние не найдено в списке, начинаем с первого
                if (currentIndex == -1)
                {
                    currentIndex = 0;
                }
                else
                {

                    // Переходим к следующему состоянию
                    currentIndex = currentIndex + 1;
                    if (currentIndex + 1 > lenghtArray)
                    {
                        currentIndex = 0;
                    }

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
            GridColumn column = view.FocusedColumn;
            if (column.OptionsColumn.ReadOnly)
            {
                MessageBox.Show("Редактирование запрещено!");
                return;

            }
            //if (view.FocusedColumn.FieldName.StartsWith("tsplPart") || view.FocusedColumn.FieldName.StartsWith("tsplPartOf"))
            //{
            //    if (char.IsDigit(e.KeyChar))
            //    {
            //        HandleRateDigitSimple(view, e.KeyChar);
            //        e.Handled = true; // Обработали сами
            //    }
            //}
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
                else if (e.KeyChar == (char)Keys.Back)
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
            string currentText = currentValue?.ToString().Trim() ?? "";

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
                view.SetRowCellValue(rowHandle, "", newText);

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
            string currentText = currentValue?.ToString().Trim() ?? "";

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

            if (IsDayColumn(e.Column) || e.Column.FieldName.StartsWith("dd") || e.Column.FieldName.StartsWith("dop"))
            {
                int rowHandle = e.RowHandle;
                int recordId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "id"));
                string columnName = e.Column.FieldName;
                string value = gridView1.GetRowCellValue(rowHandle, columnName).ToString();
                int podrTableId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "podrTableID"));
                UpdateDayTimeSheetInDataBase(columnName, value, recordId, podrTableId);

            }
            if (e.Column.FieldName == "tsplPartOf" || e.Column.FieldName == "tsplPart")
            {
                int rowHandle = e.RowHandle;
                int recordId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "id"));
                string columnName = e.Column.FieldName;
                string value = gridView1.GetRowCellValue(rowHandle, columnName).ToString();
                int podrTableId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "podrTableID"));
                UpdateDayTimeSheetInDataBase(columnName, value, recordId, podrTableId);
            }

        }
        public async void UpdateCheckBoxInDataBase(int id, string fieldName, int value, string fio, int tabno)
        {
            int grId = Convert.ToInt32(lookUpEditGr.EditValue);
            int groupId = Convert.ToInt32(lookUpEditGroup.EditValue);
            if (value == 1)
            {
                string query = $"update tabel_sp set {fieldName} = {value},ts_plan_pr = '',ts_tsp_id = 0, tsPlPart = 1 where id = {id}";
                _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { });
                await _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { });
                await GetTimeSheetFresh(currentMG, grId, groupId);
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
            else
            {

                string query = $"update tabel_sp set {fieldName} = {value},tsPlPart = 0 where id = {id}";
                _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { });
                using (var prichIskl = new ChoosePrich(id, tabno, fio))
                {
                    //Point mousePosition = Control.MousePosition;
                    //calculator.StartPosition = FormStartPosition.Manual;
                    //calculator.Location = mousePosit

                    if (prichIskl.ShowDialog() == DialogResult.OK)
                    {
                        await _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { });
                        await GetTimeSheetFresh(currentMG, grId, groupId);
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
                }


            }
        }
        public async void UpdateDayTimeSheetInDataBase(string fieldName, string value, int id, int podrTableId)
        {
            try
            {
                int grId = Convert.ToInt32(lookUpEditGr.EditValue);
                int groupId = Convert.ToInt32(lookUpEditGroup.EditValue);
                string query = null;
                if (fieldName == "tsplPart" || fieldName == "tsplPartOf")
                {
                    value = value.Replace(',', '.');
                }
                if (podrTableId == 19)
                {
                    query = $"update tabel_sp set {fieldName} = '{value}' where id = {id} ";
                }
                if (podrTableId == 20)
                {
                    query = $"update tabel_zl set {fieldName} = '{value}' where id = {id} ";
                }


                await _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { });
                await GetTimeSheetFresh(currentMG, grId, groupId);
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
            GridColumn column = gridView1.FocusedColumn;
            if (hitInfo.InRowCell && hitInfo.Column.FieldName.StartsWith("d"))
            {

                if (column.OptionsColumn.ReadOnly)
                {
                    MessageBox.Show("Редактирование запрещено!");
                    return;

                }
                CycleCellState(hitInfo.RowHandle, hitInfo.Column);
            }

        }

        private void customButton1_Click_1(object sender, EventArgs e)
        {
            int grId = Convert.ToInt32(lookUpEditGr.EditValue);
            int groupId = Convert.ToInt32(lookUpEditGroup.EditValue);
            TimeSheetParsecOrionPrint report1 = new TimeSheetParsecOrionPrint();
            report1.RequestParameters = false;
            report1.Parameters["grid"].Value = grId;
            report1.Parameters["grid"].Visible = false;
            report1.Parameters["groupId"].Value = groupId;
            report1.Parameters["groupId"].Visible = false;
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
            int groupId = Convert.ToInt32(lookUpEditGroup.EditValue);
            bool orionUserCheck = _tabelDataService.CheckRecordsExistsOrionUser(currentMG, grId, groupId);
            if (!orionUserCheck)
            {
                DialogResult result = MessageBox.Show(
                "В системе «Орион» не найдены сотрудники данного подразделения.\n\n Сформировать отчет по данным системы Parsec?",
                "Подтверждение формирования отчета",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    customButton1_Click_1(sender, e);
                    return;
                }
                if (result == DialogResult.No)
                {
                    return;
                }
            }
            TimeSheetParsecOrionPrint report1 = new TimeSheetParsecOrionPrint();
            report1.RequestParameters = false;
            report1.Parameters["grid"].Value = grId;
            report1.Parameters["grid"].Visible = false;
            report1.Parameters["groupId"].Value = groupId;
            report1.Parameters["groupId"].Visible = false;
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
            int groupId = Convert.ToInt32(lookUpEditGroup.EditValue);
            if (groupId == 19)
            {
                int month = int.Parse(currentMG.Substring(0, 2));
                TimeSheetReport report1 = new TimeSheetReport();
                report1.RequestParameters = false;
                report1.Parameters["ttabn"].Value = grId;
                report1.Parameters["ttabn"].Visible = false;
                report1.Parameters["groupId"].Value = groupId;
                report1.Parameters["groupId"].Visible = false;
                report1.Parameters["monthTxt"].Value = GetMonthName(month);
                report1.Parameters["monthTxt"].Visible = false;
                report1.Parameters["MG"].Value = currentMG;
                report1.Parameters["MG"].Visible = false;
                ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
                reportPrintTool1.ShowPreviewDialog();
            }
            if (groupId == 20)
            {
                int month = int.Parse(currentMG.Substring(0, 2));
                TimeSheetReportZl report1 = new TimeSheetReportZl();
                report1.RequestParameters = false;
                report1.Parameters["ttabn"].Value = grId;
                report1.Parameters["ttabn"].Visible = false;
                report1.Parameters["groupId"].Value = groupId;
                report1.Parameters["groupId"].Visible = false;
                report1.Parameters["monthTxt"].Value = GetMonthName(month);
                report1.Parameters["monthTxt"].Visible = false;
                report1.Parameters["MG"].Value = currentMG;
                report1.Parameters["MG"].Visible = false;
                ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
                reportPrintTool1.ShowPreviewDialog();
            }
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
                layoutControlItem14.ContentVisible = false;
                layoutControlItem13.ContentVisible = false;
                //lookUpEditGroup.Visible = false;
                //customLabel3.VisibleLogic = false;
                //customLabel3.VisiblePermission = false;
                //customLabel3.Visible = false;
                int resultGroupId = _tabelDataService.GetTabelGroupForUser(id);
                if (resultGroupId == 0)
                {
                    MessageBox.Show("Не удалось определить принадлежность");
                    return;
                }
                if (resultGroupId == 19)
                {
                    lookUpEditGroup.EditValue = 19;
                }
                if (resultGroupId == 20)
                {
                    lookUpEditGroup.EditValue = 20;
                }
            }
        }

        private void lookUpEditGroup_EditValueChanged(object sender, EventArgs e)
        {
            _timeSheetBindingSource.Clear();
            _timeSheetBindingSource.ResetBindings(false);
            int grId = Convert.ToInt32(lookUpEditGroup.EditValue);
            GetCodesArray();
            GetGrAcess(grId, idUser);
        }
        private async Task GetGrAcess(int idGr, int idUser)
        {
            if (idGr == 19)
            {
                gridColumnCheckIncludePlan.Visible = true;
                gridColumnTsplPart.Visible = true;
                gridColumnTsplPartOf.Visible = true;
                gridView1.Columns["tsp_naimen"].Visible = true;
                layoutControlItem18.ContentVisible = false;

            }
            if (idGr == 20)
            {
                gridColumnCheckIncludePlan.Visible = false;
                gridColumnTsplPart.Visible = false;
                gridColumnTsplPartOf.Visible = false;
                gridView1.Columns["tsp_naimen"].Visible = false;
                layoutControlItem18.ContentVisible = true;
            }
            var SpPodr = await _tabelDataService.GetSpPodrAsync(idGr, idUser);
            _spPodr.Clear();
            _spPodr.ResetBindings(false);
            _spPodr.DataSource = SpPodr.ToList();
            lookUpEditGroup.Refresh();


        }

        private void customGridControlTimeSheet_Click(object sender, EventArgs e)
        {

        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            GridColumn column = gridView1.FocusedColumn;
            if (e.Button == MouseButtons.Right && e.Clicks == 1)
            {
                if (column.OptionsColumn.ReadOnly)
                {
                    MessageBox.Show("Редактирование запрещено!");
                    return;

                }
                OpenTimeCalculator(e.RowHandle, e.Column);
            }
        }
        private void OpenTimeCalculator(int rowHandle, GridColumn column)
        {
            if (rowHandle < 0 || column == null) return;
            var currentValue = gridView1.GetRowCellValue(rowHandle, column);
            int dayNumber = int.Parse(column.FieldName.Substring(1));
            string markColumnNameDD = $"dd{dayNumber.ToString("00")}";
            string markColumnNameD = $"d{dayNumber.ToString("00")}";
            string markColumnNameDop = $"dop{dayNumber.ToString("00")}";
            string currentDd = gridView1.GetRowCellValue(rowHandle, markColumnNameDD).ToString();
            string currentD = gridView1.GetRowCellValue(rowHandle, markColumnNameD).ToString();
            string currentDop = gridView1.GetRowCellValue(rowHandle, markColumnNameDop).ToString();
            int dl_d = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "dl_d"));
            int grId = Convert.ToInt32(lookUpEditGroup.EditValue);
            using (var calculator = new CalculatorDay(grId, currentDd, currentD, currentDop, dl_d))
            {
                //Point mousePosition = Control.MousePosition;
                //calculator.StartPosition = FormStartPosition.Manual;
                //calculator.Location = mousePosition;
                Point cursorPos = Cursor.Position;
                Point safePosition = CalculateSafePosition(
                    cursorPos,
                    calculator.Size);

                calculator.StartPosition = FormStartPosition.Manual;
                calculator.Location = safePosition;
                if (currentValue != null && currentValue.ToString() != "")
                {

                }

                if (calculator.ShowDialog() == DialogResult.OK)
                {
                    gridView1.SetRowCellValue(rowHandle, column, calculator.CalculatorResult);
                    if (grId == 19)
                    {
                        gridView1.SetRowCellValue(rowHandle, markColumnNameDD, calculator.ddResult);
                        gridView1.SetRowCellValue(rowHandle, markColumnNameDop, calculator.dopResult);
                    }
                }
            }
        }
        public static Point CalculateSafePosition(Point desiredLocation, Size formSize)
        {
            // Получаем экран, на котором находится курсор
            Screen screen = Screen.FromPoint(desiredLocation);
            Rectangle workingArea = screen.WorkingArea;

            int x = desiredLocation.X;
            int y = desiredLocation.Y;

            // Проверяем правую границу
            if (x + formSize.Width > workingArea.Right)
            {
                // Не помещается справа - показываем слева от курсора
                x = desiredLocation.X - formSize.Width - 10;

                // Если и слева не помещается, прижимаем к левому краю
                if (x < workingArea.Left)
                {
                    x = workingArea.Left;
                }
            }

            // Проверяем нижнюю границу
            if (y + formSize.Height > workingArea.Bottom)
            {
                // Не помещается снизу - показываем сверху от курсора
                y = desiredLocation.Y - formSize.Height - 10;

                // Если и сверху не помещается, прижимаем к верхнему краю
                if (y < workingArea.Top)
                {
                    y = workingArea.Top;
                }
            }

            // Дополнительная проверка левой и верхней границ
            x = Math.Max(workingArea.Left, x);
            y = Math.Max(workingArea.Top, y);

            return new Point(x, y);
        }
        private async void GetCodesArray()
        {
            int groupId = Convert.ToInt32(lookUpEditGroup.EditValue);
            _workTypes = await _tabelDataService.GetWorkTypesAsync(groupId);
            allStates.AddRange(_workTypes.Select(wt => wt.nameWorkTypes.Trim()).ToArray());

        }

        private void customLabelYear_Click(object sender, EventArgs e)
        {

        }

        private void customButton4_Click(object sender, EventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            if (rowHandle < 0)
            {
                MessageBox.Show("Ничего не выбрано!");
                return;
            }
            int idCurrent = (int)gridView1.GetRowCellValue(rowHandle, "id");
            string fio = gridView1.GetRowCellValue(rowHandle, "fio").ToString();
            int tab = (int)gridView1.GetRowCellValue(rowHandle, "tab");
            string naimen = lookUpEditGr.Text;
            int idGr = (int)lookUpEditGr.EditValue;
            int idGroup = (int)lookUpEditGroup.EditValue;
            using (var Employee = new EmployeeTransfer(fio, naimen, idCurrent, idGroup, currentMG, tab))
            {
                if (Employee.ShowDialog() == DialogResult.OK)
                {
                    if (Employee.EmployeeResult)
                    {
                        GetTimeSheet(currentMG, idGr, idGroup);
                    }

                }
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {

            GridView view = (GridView)sender;
            GridColumn column = view.FocusedColumn;


            if (e.KeyCode == Keys.Back && (gridView1.FocusedColumn.FieldName == "tsplPart" || gridView1.FocusedColumn.FieldName == "tsplPartOf"))
            {
                if (column.OptionsColumn.ReadOnly)
                {
                    MessageBox.Show("Редактирование запрещено!");
                    return;

                }
                gridView1.SetRowCellValue(
                    gridView1.FocusedRowHandle,
                    column,
                    0m);

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            if (e.KeyValue == (char)Keys.Delete)
            {
                if (column.OptionsColumn.ReadOnly)
                {
                    MessageBox.Show("Редактирование запрещено!");
                    return;

                }
                int rowHandle = view.FocusedRowHandle;

                view.SetRowCellValue(rowHandle, column, "");
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                int rowHandle = view.FocusedRowHandle;
                Clipboard.SetDataObject(view.GetRowCellValue(rowHandle, column));
                e.Handled = true;
            }
            // Обработка Ctrl+V
            else if (e.Control && e.KeyCode == Keys.V)
            {
                int rowHandle = view.FocusedRowHandle;
                IDataObject iData = Clipboard.GetDataObject();
                if (iData.GetDataPresent(DataFormats.Text))
                {
                    view.SetRowCellValue(rowHandle, column, (string)iData.GetData(DataFormats.Text));
                }
                e.Handled = true;
            }
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

        private void customButton4_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show($"{gridView1.FocusedColumn.VisibleIndex}");
            gridColumnFio.VisibleIndex = 3;
        }

        private void customButton4_Click_2(object sender, EventArgs e)
        {
            int grId = Convert.ToInt32(lookUpEditGr.EditValue);
            TimeSheetReportSkladi report1 = new TimeSheetReportSkladi();
            report1.RequestParameters = false;
            report1.Parameters["groupString"].Value = "zl";
            report1.Parameters["groupString"].Visible = false;
            //report1.Parameters["idgr"].Value = grId;
            //report1.Parameters["idgr"].Visible = false;
            report1.Parameters["mg"].Value = currentMG;
            report1.Parameters["mg"].Visible = false;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        private void customSimpleButton1_Click(object sender, EventArgs e)
        {
            int grId = Convert.ToInt32(lookUpEditGr.EditValue);
            TimeSheetReportSkladi report1 = new TimeSheetReportSkladi();
            report1.RequestParameters = false;
            report1.Parameters["groupString"].Value = "zl";
            report1.Parameters["groupString"].Visible = false;
            report1.Parameters["idgr"].Value = grId;
            report1.Parameters["idgr"].Visible = false;
            report1.Parameters["mg"].Value = currentMG;
            report1.Parameters["mg"].Visible = false;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        private void customSimpleButton1_Click_1(object sender, EventArgs e)
        {
            int grId = Convert.ToInt32(lookUpEditGr.EditValue);
            TimeSheetReportSkladi report1 = new TimeSheetReportSkladi();
            report1.RequestParameters = true;
            report1.Parameters["groupString"].Value = "zl";
            report1.Parameters["groupString"].Visible = false;
            //report1.Parameters["idgr"].Value = grId;
            //report1.Parameters["idgr"].Visible = false;
            report1.Parameters["mg"].Value = currentMG;
            report1.Parameters["mg"].Visible = false;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreview();


        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void repositoryItemCheckEditPlan_EditValueChanged(object sender, EventArgs e)
        {
            gridView1.PostEditor();
            gridView1.UpdateCurrentRow();
            int rowHandle = gridView1.FocusedRowHandle;
            int valueChecked = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "ts_plan"));
            int recordId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "id"));
            string fio = gridView1.GetRowCellValue(rowHandle, "fio").ToString();
            int tabno = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "tab"));

            UpdateCheckBoxInDataBase(recordId, "ts_plan", valueChecked, fio, tabno);

        }

        private void gridView1_ShownEditor(object sender, EventArgs e)
        {

        }

        private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (gridView1.FocusedColumn != gridColumnTsplPart && gridView1.FocusedColumn != gridColumnTsplPartOf) return;

            if (decimal.TryParse(e.Value?.ToString(), out decimal val))
            {
                if (val < 0m || val > 2.00m)
                {
                    e.Valid = false;
                    e.ErrorText = "Допустимый диапазон 0.00 – 2.00";
                }
            }
        }

        private void customSimpleButton3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int grId = Convert.ToInt32(lookUpEditGroup.EditValue);
                using (var otchetTabel = new ChoosePodrOtchet(grId, currentMG))
                {

                    Point cursorPos = Cursor.Position;
                    Point safePosition = CalculateSafePosition(
                        cursorPos,
                        otchetTabel.Size);

                    otchetTabel.StartPosition = FormStartPosition.Manual;
                    otchetTabel.Location = safePosition;
                    if (otchetTabel.ShowDialog() == DialogResult.OK)
                    {

                    }


                }
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0 || string.IsNullOrEmpty(e.Column.FieldName))
                return;

            string fieldName = e.Column.FieldName;

            if (fieldName.StartsWith("d") && fieldName.Length == 3)
            {
                string suffix = fieldName.Substring(1);   // 01, 02, ..., 31
                string dopField = "dop" + suffix;         // dop01, dop02, ...

                var view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;

                var dValue = Convert.ToString(view.GetListSourceRowCellValue(e.ListSourceRowIndex, fieldName));
                var dopValue = Convert.ToString(view.GetListSourceRowCellValue(e.ListSourceRowIndex, dopField));

                e.DisplayText = string.IsNullOrWhiteSpace(dopValue)
                    ? dValue
                    : $"{dValue.Trim()}{dopValue.Trim()}";
            }
        }
    }
}
