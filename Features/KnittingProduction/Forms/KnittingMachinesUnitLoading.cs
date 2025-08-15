using DevExpress.CodeParser;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using SewingProduction.Extensions;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Features.KnittingProduction.Services;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Charts.Native;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class KnittingMachinesUnitLoading : CustomForm
    {
        private static DatabaseHelper _dbHelper;
        private static DbService _dbService;
        private static BulkHelper _bulkHelper;
        private readonly ILogger _logger = new FileLogger();
        private readonly VyazService _vyazService;
        private int _xKmlID;
        private string _xKmlNumber;
        private List<PlanSezonZadKnitMachine> _currentPlanSezonZadKnitMachineData = new List<PlanSezonZadKnitMachine>();
        private BindingList<PlanSezonZadKnitMachine> _planSezonZadKnitMachineBindingList;
        private BindingSource _planSezonZadKnitMachineBindingSource;
        private List<PlanSezonZadKnitMachine> _planSezonZadKnitMachineData = new List<PlanSezonZadKnitMachine>();
        
        private List<PlanSezonZadKnitMachineLoadingSummary> _currentPlanSezonZadKnitMachineLoadingSummaryData = new List<PlanSezonZadKnitMachineLoadingSummary>();
        private BindingList<PlanSezonZadKnitMachineLoadingSummary> _planSezonZadKnitMachineLoadingSummaryBindingList;
        private BindingSource _planSezonZadKnitMachineLoadingSummaryBindingSource;
        private List<PlanSezonZadKnitMachineLoadingSummary> _planSezonZadKnitMachineLoadingSummaryData = new List<PlanSezonZadKnitMachineLoadingSummary>();

        public KnittingMachinesUnitLoading(int xKmlID, string xKmlNumber)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _dbService = new DbService(_dbHelper);
            _vyazService = new VyazService(_dbHelper);
            _bulkHelper = new BulkHelper();
            _xKmlID = xKmlID;
            _xKmlNumber = xKmlNumber;
            gridViewPlanSezonZadKnitMachineLoadingSummary.OptionsView.ShowColumnHeaders = false;

        }
        private async Task InitializeBindingsAsync()
        {
            try
            {
                var planSezonZadKnitMachineTask = Task.Run(() =>
                {
                    _planSezonZadKnitMachineBindingList = new BindingList<PlanSezonZadKnitMachine>();
                    _planSezonZadKnitMachineBindingSource = new BindingSource { DataSource = _planSezonZadKnitMachineBindingList };
                });
                var planSezonZadKnitMachineLoadingSummaryTask = Task.Run(() =>
                {
                    _planSezonZadKnitMachineLoadingSummaryBindingList = new BindingList<PlanSezonZadKnitMachineLoadingSummary>();
                    _planSezonZadKnitMachineLoadingSummaryBindingSource = new BindingSource { DataSource = _planSezonZadKnitMachineLoadingSummaryBindingList };
                });
                await Task.WhenAll(planSezonZadKnitMachineTask, planSezonZadKnitMachineLoadingSummaryTask);

                #region описание gridControlPlanSezonZadKnitMachine "текущий загруз В/М"
                gridControlPlanSezonZadKnitMachine.DataSource = _planSezonZadKnitMachineBindingSource;
                gridPlanSezonZadKnitMachineColumnArticul.FieldName = "articul";
                gridPlanSezonZadKnitMachineColumnPszkmPszNom.FieldName = "pszkmPszNom";
                gridPlanSezonZadKnitMachineColumnHoursTotal.FieldName = "hoursTotal";
                gridPlanSezonZadKnitMachineColumnDateZap.FieldName = "yearMonthDateZap";
                gridPlanSezonZadKnitMachineColumnPszkmPlanDateFrom.FieldName = "pszkmPlanDateFrom";
                gridPlanSezonZadKnitMachineColumnPszkmPlanDateTo.FieldName = "pszkmPlanDateTo";
                gridPlanSezonZadKnitMachineColumnYearMonthPlanDate.FieldName = "yearMonthPlanDate";
                gridPlanSezonZadKnitMachineColumnKmlNumber.FieldName = "kmlNumber";
                gridPlanSezonZadKnitMachineColumnKmlNumber.Visible = false;
                gridPlanSezonZadKnitMachineColumnYearNumberPlanDate.FieldName = "yearNumberPlanDate";
                gridPlanSezonZadKnitMachineColumnYearNumberPlanDate.Visible = false;
                gridPlanSezonZadKnitMachineColumnMonthNumberPlanDate.FieldName = "monthNumberPlanDate";
                gridPlanSezonZadKnitMachineColumnMonthNumberPlanDate.Visible = false;
                gridPlanSezonZadKnitMachineColumnYearMonthText.FieldName = "yearMonthText";
                gridPlanSezonZadKnitMachineColumnPszkmYearMonthInt.FieldName = "pszkmYearMonthInt";

                gridViewPlanSezonZadKnitMachine.RowStyle += (s, e) =>
                {
                    var rowData = gridViewPlanSezonZadKnitMachine.GetRow(e.RowHandle) as PlanSezonZadKnitMachine;
                    if (rowData == null) return;

                    int targetYear = DateTime.Now.Year;
                    int targetMonth = DateTime.Now.Month;

                    bool shouldHighlight =
                        (rowData.pszkmPlanDateFrom.Value.Year != rowData.yearNumberDateZap
                            || rowData.pszkmPlanDateFrom.Value.Month != rowData.monthNumberDateZap) ||
                        (rowData.pszkmPlanDateTo.Value.Year != rowData.yearNumberDateZap
                            || rowData.pszkmPlanDateTo.Value.Month != rowData.monthNumberDateZap);

                    if (shouldHighlight)
                    {
                        // Красный цвет текста
                        e.Appearance.ForeColor = Color.Red;
                        //// Жирный шрифт (сохраняем текущий шрифт, добавляя Bold)
                        //e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        gridViewPlanSezonZadKnitMachine.Appearance.SelectedRow.ForeColor = Color.Red;
                        gridViewPlanSezonZadKnitMachine.Appearance.SelectedRow.Options.UseForeColor = true;
                        gridViewPlanSezonZadKnitMachine.Appearance.FocusedRow.ForeColor = Color.Red;
                        gridViewPlanSezonZadKnitMachine.Appearance.FocusedRow.Options.UseForeColor = true;
                        // Чтобы стиль не переопределялся другими правилами
                        //e.HighPriority = true;

                        // Или можно изменить фон:
                        // e.Appearance.BackColor = Color.LightPink;
                    }
                };

                //gridViewPlanSezonZadKnitMachine.CustomDrawGroupFooterCell += (sender, e) =>
                //{
                //    if (e.Column.FieldName == "Total") // колонка, для которой делаем подитог
                //    {
                //        object groupValue = gridView1.GetGroupRowValue(e.RowHandle, "Category"); // "Category" — поле группировки
                //        decimal sum = Convert.ToDecimal(e.Info.Value);
                //        e.Info.DisplayText = $"Сумма по {groupValue}: {sum:C}";
                //    }
                //};

                //gridViewPlanSezonZadKnitMachine.CustomUnboundColumnData += (s, e) =>
                //{
                //    if (e.Column.FieldName == "yearMonthText")
                //    {
                //        int year = Convert.ToInt32(gridViewPlanSezonZadKnitMachine.GetListSourceRowCellValue(e.ListSourceRowIndex, "yearNumberPlanDate"));
                //        int month = Convert.ToInt32(gridViewPlanSezonZadKnitMachine.GetListSourceRowCellValue(e.ListSourceRowIndex, "monthNumberPlanDate"));
                //        //e.Value = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {year}";
                //        //var date = new DateTime(year, month, 1);
                //        //e.Value = date.ToString("MMMM-yy"); // "Июль-25"
                //        string mg = $"{year}{month}";
                //    }
                //};

                gridViewPlanSezonZadKnitMachine.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;

                //gridViewPlanSezonZadKnitMachine.Columns["kmlNumber"].SortIndex = 0;
                //gridViewPlanSezonZadKnitMachine.Columns["yearNumberPlanDate"].SortIndex = 1;
                //gridViewPlanSezonZadKnitMachine.Columns["monthNumberPlanDate"].SortIndex = 2;


                //gridViewPlanSezonZadKnitMachine.Columns["kmlNumber"].GroupIndex = 0;
                //if (gridViewPlanSezonZadKnitMachine.Columns["yearMonthText"] != null)
                //{
                //    gridViewPlanSezonZadKnitMachine.Columns["yearMonthText"].GroupIndex = 1;
                //    gridViewPlanSezonZadKnitMachine.RefreshData();
                //}

                //gridViewPlanSezonZadKnitMachine.BeginUpdate();
                gridViewPlanSezonZadKnitMachine.BeginSort();
                gridViewPlanSezonZadKnitMachine.ClearGrouping();
                gridViewPlanSezonZadKnitMachine.ClearSorting();

                // Сначала группируем по номеру машины
                gridViewPlanSezonZadKnitMachine.Columns["kmlNumber"].GroupIndex = 0;
                gridViewPlanSezonZadKnitMachine.SortInfo.Add(new GridColumnSortInfo(gridViewPlanSezonZadKnitMachine.Columns["kmlNumber"], ColumnSortOrder.Ascending));

                // Потом по году-месяцу
                gridViewPlanSezonZadKnitMachine.Columns["pszkmYearMonthInt"].GroupIndex = 1;
                gridViewPlanSezonZadKnitMachine.SortInfo.Add(new GridColumnSortInfo(gridViewPlanSezonZadKnitMachine.Columns["pszkmYearMonthInt"], ColumnSortOrder.Ascending));

                //// Потом по году
                //gridViewPlanSezonZadKnitMachine.Columns["yearNumberPlanDate"].GroupIndex = 1;
                //gridViewPlanSezonZadKnitMachine.SortInfo.Add(new GridColumnSortInfo(gridViewPlanSezonZadKnitMachine.Columns["yearNumberPlanDate"], ColumnSortOrder.Ascending));

                //// Потом по месяцу (цифровому!)
                //gridViewPlanSezonZadKnitMachine.Columns["monthNumberPlanDate"].GroupIndex = 2;
                //gridViewPlanSezonZadKnitMachine.SortInfo.Add(new GridColumnSortInfo(gridViewPlanSezonZadKnitMachine.Columns["monthNumberPlanDate"], ColumnSortOrder.Ascending));

                gridViewPlanSezonZadKnitMachine.EndSort();

                gridViewPlanSezonZadKnitMachine.GroupFormat = "{1}: [#image]{2}"; // Показывает: <Caption>: <value>

                gridViewPlanSezonZadKnitMachine.CustomDrawGroupRow += (s, e) =>
                {
                    GridView view = s as GridView;
                    int rowHandle = e.RowHandle;

                    int level = view.GetRowLevel(rowHandle);

                    GridGroupRowInfo groupInfo = e.Info as GridGroupRowInfo;
                    //groupInfo.GroupExpanded = true;
                    if (level == 1) // mg
                    {
                        //string mg = view.GetGroupRowValue(e.RowHandle, view.Columns["yearMonthText"]).ToString();
                        //groupInfo.GroupText = $"{mg}";
                        int year = Convert.ToInt32(view.GetGroupRowValue(e.RowHandle, view.Columns["yearNumberPlanDate"]));
                        int month = Convert.ToInt32(view.GetGroupRowValue(e.RowHandle, view.Columns["monthNumberPlanDate"]));
                        DateTime dt = new DateTime(year, month, 1);
                        string formatted = dt.ToString("MMMM-yy", new System.Globalization.CultureInfo("ru-RU"));

                        //groupInfo.GroupText = $"Месяц: {formatted}";
                        //groupInfo.GroupText = $"Месяц: {dt.ToString("MMMM-yy", new System.Globalization.CultureInfo("ru-RU"))}";
                        groupInfo.GroupText = $"{dt.ToString("MMMM-yy", new System.Globalization.CultureInfo("ru-RU"))}";
                        
                    }

                    //if (level == 1) // yearNumber
                    //{
                    //    // Пропустить отображение группы по году
                    //    groupInfo.GroupText = ""; // Пусто
                    //}
                    //if (level == 2) // уровень месяца
                    //{
                    //    int year = Convert.ToInt32(view.GetGroupRowValue(e.RowHandle, view.Columns["yearNumberPlanDate"]));
                    //    int month = Convert.ToInt32(view.GetGroupRowValue(e.RowHandle, view.Columns["monthNumberPlanDate"]));
                    //    DateTime dt = new DateTime(year, month, 1);
                    //    string formatted = dt.ToString("MMMM-yy", new System.Globalization.CultureInfo("ru-RU"));

                    //    //groupInfo.GroupText = $"Месяц: {formatted}";
                    //    groupInfo.GroupText = $"Месяц: {dt.ToString("MMMM-yy", new System.Globalization.CultureInfo("ru-RU"))}";
                    //}
                };


                //gridViewPlanSezonZadKnitMachine.CustomDrawGroupRow += (s, e) =>
                //{
                //    GridView view = s as GridView;
                //    GridGroupRowInfo info = e.Info as GridGroupRowInfo;

                //    if (view.GetRowLevel(e.RowHandle) == 1) // нужный уровень группировки
                //    {
                //        // Получить сумму или другой агрегат
                //        GridSummaryItem gridSummaryItem = gridViewPlanSezonZadKnitMachine.GroupSummary[0];
                //        decimal sum = Convert.ToDecimal(view.GetGroupSummaryValue(e.RowHandle, (GridGroupSummaryItem)gridSummaryItem));

                //        string caption = "Итого по в/м, ч/ч: " + sum.ToString("N2");

                //        // Нарисовать заголовок вручную
                //        info.GroupText = caption; // ← здесь можно задать длинный текст, он не обрезается
                //    }
                //};

                //gridViewPlanSezonZadKnitMachine.GroupSummary.Clear();

                //gridViewPlanSezonZadKnitMachine.GroupSummary.Add(new GridGroupSummaryItem()
                //{
                //    FieldName = "hoursTotal",
                //    SummaryType = DevExpress.Data.SummaryItemType.Sum,
                //    ShowInGroupColumnFooterName = "hoursTotal"
                //});

                //gridViewPlanSezonZadKnitMachine.DataSourceChanged += (s, e) =>
                //{
                //    gridViewPlanSezonZadKnitMachine.BeginUpdate();
                //    gridViewPlanSezonZadKnitMachine.ExpandAllGroups();
                //    gridViewPlanSezonZadKnitMachine.EndUpdate();
                //};


                //gridViewPlanSezonZadKnitMachine.ExpandAllGroups();

                //gridViewPlanSezonZadKnitMachine.Columns["yearNumberPlanDate"].GroupIndex = 1;
                //gridViewPlanSezonZadKnitMachine.Columns["monthNumberPlanDate"].GroupIndex = 2;

                //gridViewPlanSezonZadKnitMachine.Columns["yearNumberPlanDate"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                //gridViewPlanSezonZadKnitMachine.Columns["monthNumberPlanDate"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

                ////gridViewPlanSezonZadKnitMachine.Columns["yearMonthText"].Group();

                //gridViewPlanSezonZadKnitMachine.Columns["yearNumberPlanDate"].Group();
                //gridViewPlanSezonZadKnitMachine.CustomGroupDisplayText += (sender, e) =>
                //{
                //    if (e.Column.FieldName == "DateColumn" && e.Value != null)
                //    {
                //        DateTime date = Convert.ToDateTime(e.Value);
                //        e.DisplayText = date.ToString("MMMM yyyy", CultureInfo.GetCultureInfo("ru-RU"));
                //    }
                //};



                //var summaryItem = new GridGroupSummaryItem()
                //{
                //    FieldName = "hoursTotal",
                //    SummaryType = SummaryItemType.Sum,
                //    ShowInGroupColumnFooter = gridViewPlanSezonZadKnitMachine.Columns["hoursTotal"],
                //};
                //gridViewPlanSezonZadKnitMachine.GroupSummary.Add(summaryItem);

                //gridViewPlanSezonZadKnitMachine.CustomDrawFooterCell += (s, e) =>
                //{
                //    if (e.Column.FieldName == "hoursTotal")
                //    {
                //        var view = s as GridView;
                //        var groupRowHandle = e.RowHandle;
                //        object kmlValue = view.GetGroupRowValue(groupRowHandle); // номер в/м
                //        decimal sum = Convert.ToDecimal(e.Info.Value);
                //        e.Info.DisplayText = $"Итого по в/м {kmlValue}, ч/ч: {sum:0.##}";
                //    }
                //};

                #endregion

                #region описание gridControlPlanSezonZadKnitMachineLoadingSummary "итоги по текущему загрузу В/М"
                gridControlPlanSezonZadKnitMachineLoadingSummary.DataSource = _planSezonZadKnitMachineLoadingSummaryBindingSource;
                gridPlanSezonZadKnitMachineLoadingSummaryColumnPeriod.FieldName = "period";
                gridPlanSezonZadKnitMachineLoadingSummaryColumnHoursTotal.FieldName = "hoursTotal";
                #endregion

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
        }

        private async Task LoadKnitMachineUnitLoadingDataAsync(int xKmlID)
        {
            try
            {
                _planSezonZadKnitMachineBindingSource.Clear();
                _planSezonZadKnitMachineBindingSource.ResetBindings(false);
                _planSezonZadKnitMachineData = await _vyazService.GetPlanSezonZadKnitMachine(xKmlID);
                if (_planSezonZadKnitMachineData != null)
                {
                    await _logger.LogEventAsync($"Получены данные PlanSezonZadKnitMachine", "LoadKnitMachineUnitLoadingDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentPlanSezonZadKnitMachineData = _planSezonZadKnitMachineData;                // Обновляем текущую модель
                        _planSezonZadKnitMachineBindingSource.DataSource = _currentPlanSezonZadKnitMachineData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные PlanSezonZadKnitMachine успешно загружены", "LoadKnitMachineUnitLoadingDataAsync");
                    _planSezonZadKnitMachineBindingSource.ResetBindings(false);
                    _planSezonZadKnitMachineBindingSource.Sort = "pszkmYearMonthInt, pszkmPlanDateFrom";
                    gridControlPlanSezonZadKnitMachine.DataSource = _planSezonZadKnitMachineBindingSource;
                    //gridViewPlanSezonZadKnitMachine.ExpandAllGroups = true;
                    
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные PlanSezonZadKnitMachine", "LoadKnitMachineUnitLoadingDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных PlanSezonZadKnitMachine");
            }
        }

        private async Task LoadKnitMachineUnitLoadingSummaryDataAsync(int xKmlID)
        {
            try
            {
                _planSezonZadKnitMachineLoadingSummaryBindingSource.Clear();
                _planSezonZadKnitMachineLoadingSummaryBindingSource.ResetBindings(false);
                _planSezonZadKnitMachineLoadingSummaryData = await _vyazService.GetPlanSezonZadKnitMachineLoadingSummary(xKmlID);
                if (_planSezonZadKnitMachineLoadingSummaryData != null)
                {
                    await _logger.LogEventAsync($"Получены данные PlanSezonZadKnitMachineLoadingSummary", "LoadKnitMachineUnitLoadingSummaryDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentPlanSezonZadKnitMachineLoadingSummaryData = _planSezonZadKnitMachineLoadingSummaryData;                // Обновляем текущую модель
                        _planSezonZadKnitMachineLoadingSummaryBindingSource.DataSource = _currentPlanSezonZadKnitMachineLoadingSummaryData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные PlanSezonZadKnitMachineLoadingSummary успешно загружены", "LoadKnitMachineUnitLoadingSummaryDataAsync");
                    _planSezonZadKnitMachineLoadingSummaryBindingSource.ResetBindings(false);
                    _planSezonZadKnitMachineLoadingSummaryBindingSource.Sort = "sortOrder, yearNumberDateZap, monthNumberDateZap";
                    gridControlPlanSezonZadKnitMachineLoadingSummary.DataSource = _planSezonZadKnitMachineLoadingSummaryBindingSource;
                    //gridViewPlanSezonZadKnitMachine.ExpandAllGroups = true;

                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные PlanSezonZadKnitMachineLoadingSummary", "LoadKnitMachineUnitLoadingSummaryDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных PlanSezonZadKnitMachineLoadingSummary");
            }
        }

        private async void KnittingMachinesUnitLoading_Load(object sender, EventArgs e)
        {
            try
            {
                Task bindingsTask = InitializeBindingsAsync();
                await Task.WhenAll(bindingsTask);

                //await LoadKnitMachineAreaListDataAsync();
                await LoadKnitMachineUnitLoadingDataAsync(_xKmlID);
                await LoadKnitMachineUnitLoadingSummaryDataAsync(_xKmlID);
                labelKmlNumber.Text = _xKmlNumber;

                gridViewPlanSezonZadKnitMachine.Columns["hoursTotal"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "hoursTotal", "(ч/ч: {0:0.##})");    //"(ч/ч: {0:c})" - с - отображение валюты руб
                //gridViewPlanSezonZadKnitMachine.Columns["hoursTotal"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "hoursTotal", "(дней: {0:0})");
                GridColumnSummaryItem _daysTotal;
                _daysTotal = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "hoursTotal", "(дней: {0:n2})");
                gridViewPlanSezonZadKnitMachine.Columns["hoursTotal"].Summary.Add(_daysTotal);
                decimal _acc = 0m;


                gridViewPlanSezonZadKnitMachine.CustomSummaryCalculate += (sender, e) =>
                {
                    // Сработали по «чужому» итогу? Выходим — его не трогаем.
                    if (!ReferenceEquals(e.Item, _daysTotal)) return; //    && !ReferenceEquals(e.Item, _sumWithNds)

                    if (e.SummaryProcess == CustomSummaryProcess.Start)
                    {
                        _acc = 0m; // обнуляем аккумулятор для ЭТОГО итога
                    }
                    else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                    {
                        // На каждой строке берём значение поля "Amount" и копим
                        var val = e.FieldValue;
                        if (val != null && val != DBNull.Value)
                            _acc += Convert.ToDecimal(val);

                        // Если формула сложнее, можно читать любые другие поля строки:
                        // var v = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "OtherField"));
                    }
                    else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                    {
                        // Выдаём результат — зависит от того, КАКОЙ именно итог мы сейчас закрываем
                        if (ReferenceEquals(e.Item, _daysTotal))
                            e.TotalValue = _acc / 23;    // количество дней
                        //else if (ReferenceEquals(e.Item, _sumWithNds))
                        //    e.TotalValue = _acc * 1.20m;    // сумма + НДС
                    }
                };
                //---------------------------------------------
                gridViewPlanSezonZadKnitMachine.BeginSort();
                gridViewPlanSezonZadKnitMachine.ClearSorting();

                gridViewPlanSezonZadKnitMachine.SortInfo.AddRange(new[] {
                        new DevExpress.XtraGrid.Columns.GridColumnSortInfo(gridViewPlanSezonZadKnitMachineLoadingSummary.Columns["pszkmYearMonthInt"], DevExpress.Data.ColumnSortOrder.Ascending),
                        new DevExpress.XtraGrid.Columns.GridColumnSortInfo(gridViewPlanSezonZadKnitMachineLoadingSummary.Columns["DateZap"], DevExpress.Data.ColumnSortOrder.Ascending)
                    });
                gridViewPlanSezonZadKnitMachine.EndSort();
                //---------------------------------------------

                gridViewPlanSezonZadKnitMachine.ExpandAllGroups();
                gridViewPlanSezonZadKnitMachine.OptionsView.ShowGroupPanel = false;

                //---------------------------------------------
                gridViewPlanSezonZadKnitMachineLoadingSummary.BeginSort();
                gridViewPlanSezonZadKnitMachineLoadingSummary.ClearSorting();

                gridViewPlanSezonZadKnitMachineLoadingSummary.SortInfo.AddRange(new[] {
                        new DevExpress.XtraGrid.Columns.GridColumnSortInfo(gridViewPlanSezonZadKnitMachine.Columns["sortOrder"], DevExpress.Data.ColumnSortOrder.Ascending),
                        new DevExpress.XtraGrid.Columns.GridColumnSortInfo(gridViewPlanSezonZadKnitMachine.Columns["yearNumberDateZap"], DevExpress.Data.ColumnSortOrder.Ascending),
                        new DevExpress.XtraGrid.Columns.GridColumnSortInfo(gridViewPlanSezonZadKnitMachine.Columns["monthNumberDateZap"], DevExpress.Data.ColumnSortOrder.Ascending)
                    });
                gridViewPlanSezonZadKnitMachineLoadingSummary.EndSort();
                //---------------------------------------------
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке формы KnittingMachinesUnitLoading");
            }
        }
        
    }
}
