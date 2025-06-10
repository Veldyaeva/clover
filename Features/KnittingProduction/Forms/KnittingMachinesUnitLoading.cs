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
        public KnittingMachinesUnitLoading(int xKmlID, string xKmlNumber)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _dbService = new DbService(_dbHelper);
            _vyazService = new VyazService(_dbHelper);
            _bulkHelper = new BulkHelper();
            _xKmlID = xKmlID;
            _xKmlNumber = xKmlNumber;
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

                await Task.WhenAll(planSezonZadKnitMachineTask);

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

                gridViewPlanSezonZadKnitMachine.CustomUnboundColumnData += (s, e) =>
                {
                    if (e.Column.FieldName == "yearMonthText")
                    {
                        int year = Convert.ToInt32(gridViewPlanSezonZadKnitMachine.GetListSourceRowCellValue(e.ListSourceRowIndex, "yearNumberPlanDate"));
                        int month = Convert.ToInt32(gridViewPlanSezonZadKnitMachine.GetListSourceRowCellValue(e.ListSourceRowIndex, "monthNumberPlanDate"));
                        e.Value = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {year}";
                    }
                };
                gridViewPlanSezonZadKnitMachine.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;

                gridViewPlanSezonZadKnitMachine.Columns["kmlNumber"].SortIndex = 0;
                gridViewPlanSezonZadKnitMachine.Columns["yearNumberPlanDate"].SortIndex = 1;
                gridViewPlanSezonZadKnitMachine.Columns["monthNumberPlanDate"].SortIndex = 2;


                gridViewPlanSezonZadKnitMachine.Columns["kmlNumber"].GroupIndex = 0;
                if (gridViewPlanSezonZadKnitMachine.Columns["yearMonthText"] != null)
                {
                    gridViewPlanSezonZadKnitMachine.Columns["yearMonthText"].GroupIndex = 1;
                    gridViewPlanSezonZadKnitMachine.RefreshData();
                }

                
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
                //#region описание comboBox "Список зон обслуживания"
                ////сomboBoxKnitMachineAreaList.DataSource = _knitMachineAreaListViewBindingSource;
                ////сomboBoxKnitMachineAreaList.SelectedIndex = -1;
                ////сomboBoxKnitMachineAreaList.ValueMember = "kmaID";
                ////сomboBoxKnitMachineAreaList.DisplayMember = "kmaNumber";
                ////int xSelectedIndex = сomboBoxKnitMachineClassList.SelectedIndex;
                //сomboBoxKnitMachineClassList.DataSource = _knitMachineClassListBindingSource;
                ////сomboBoxKnitMachineClassList.SelectedIndex = xSelectedIndex;
                //сomboBoxKnitMachineClassList.ValueMember = "id_class";
                //сomboBoxKnitMachineClassList.DisplayMember = "caption";
                //сomboBoxKnitMachineClassList.SelectedValue = xClassID;
                //#endregion

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
                    _planSezonZadKnitMachineBindingSource.Sort = "yearNumberPlanDate, monthNumberPlanDate, pszkmPlanDateFrom";
                    gridControlPlanSezonZadKnitMachine.DataSource = _planSezonZadKnitMachineBindingSource;
                    
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

        private async void KnittingMachinesUnitLoading_Load(object sender, EventArgs e)
        {
            try
            {
                Task bindingsTask = InitializeBindingsAsync();
                await Task.WhenAll(bindingsTask);

                //await LoadKnitMachineAreaListDataAsync();
                await LoadKnitMachineUnitLoadingDataAsync(_xKmlID);

                labelKmlNumber.Text = _xKmlNumber;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке формы KnittingMachinesUnitLoading");
            }
        }
        
    }
}
