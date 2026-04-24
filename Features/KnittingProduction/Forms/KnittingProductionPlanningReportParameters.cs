using DevExpress.Office.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraReports.UI;
using SewingProduction.Core.Class;
using SewingProduction.Core.interfaces;
using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Features.KnittingProduction.Services;
using SewingProduction.Helpers;
using SewingProduction.Report;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class KnittingProductionPlanningReportParameters : CustomForm
    {
        private static DatabaseHelperSQL _dbHelper;
        private static DbService _dbService;
        private static BulkHelper _bulkHelper;
        private readonly ILogger _logger = new FileLogger();
        private readonly VyazService _vyazService;
        //private LoadingScreen _loadingScreen;

        private List<KnitPlanReportParametersList> _currentKnitClassListData = new List<KnitPlanReportParametersList>();
        private BindingList<KnitPlanReportParametersList> _knitClassListBindingList;
        private BindingSource _knitClassListBindingSource;

        private List<KnitPlanReportParametersList> _currentKmlInvNumberListData = new List<KnitPlanReportParametersList>();
        private BindingList<KnitPlanReportParametersList> _kmlInvNumberListBindingList;
        private BindingSource _kmlInvNumberListBindingSource;

        private List<KnitPlanReportParametersList> _currentMonthZapListData = new List<KnitPlanReportParametersList>();
        private BindingList<KnitPlanReportParametersList> _monthZapListBindingList;
        private BindingSource _monthZapListBindingSource;

        private List<KnitPlanReportParametersList> _currentArticulListData = new List<KnitPlanReportParametersList>();
        private BindingList<KnitPlanReportParametersList> _articulListBindingList;
        private BindingSource _articulListBindingSource;


        public KnittingProductionPlanningReportParameters()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelperSQL("ace");
            _dbService = new DbService(_dbHelper);
            _vyazService = new VyazService(_dbHelper);
            _bulkHelper = new BulkHelper();
            //_loadingScreen = new LoadingScreen();
           // ThemeManager.UpdateTheme(this);
        }

        private async Task InitializeBindingsAsync()
        {
            try
            {
                var knitClassListTask = Task.Run(() =>
            {
                _knitClassListBindingList = new BindingList<KnitPlanReportParametersList>();
                _knitClassListBindingSource = new BindingSource { DataSource = _knitClassListBindingList };
            });
                var kmlInvNumberListTask = Task.Run(() =>
                {
                    _kmlInvNumberListBindingList = new BindingList<KnitPlanReportParametersList>();
                    _kmlInvNumberListBindingSource = new BindingSource { DataSource = _kmlInvNumberListBindingList };
                });
                var monthZapListTask = Task.Run(() =>
                {
                    _monthZapListBindingList = new BindingList<KnitPlanReportParametersList>();
                    _monthZapListBindingSource = new BindingSource { DataSource = _monthZapListBindingList };
                });
                var articulListTask = Task.Run(() =>
                {
                    _articulListBindingList = new BindingList<KnitPlanReportParametersList>();
                    _articulListBindingSource = new BindingSource { DataSource = _articulListBindingList };
                });
                await Task.WhenAll(knitClassListTask, kmlInvNumberListTask, monthZapListTask, articulListTask);

                #region описание comboBox "Классы вязания"
                comboBoxKnitClass.DataSource = _knitClassListBindingSource;
                comboBoxKnitClass.SelectedIndex = -1;
                comboBoxKnitClass.ValueMember = "kmlIdVyazClass";
                comboBoxKnitClass.DisplayMember = "name_class";
                #endregion

                #region описание comboBox "Инвентарные номера В/М"
                comboBoxKmlInvNumber.DataSource = _kmlInvNumberListBindingSource;
                comboBoxKmlInvNumber.SelectedIndex = -1;
                comboBoxKmlInvNumber.ValueMember = "pszkmKmlID";
                comboBoxKmlInvNumber.DisplayMember = "kmlInvNumber";
                #endregion

                #region описание comboBox "Месяц запуска"
                comboBoxExecMonth.DataSource = _monthZapListBindingSource;
                comboBoxExecMonth.SelectedIndex = -1;
                comboBoxExecMonth.ValueMember = "yearMonthZapInt";
                comboBoxExecMonth.DisplayMember = "yearMonthDateZap";
                #endregion

                #region описание comboBox "Артикулы"
                comboBoxArticul.DataSource = _articulListBindingSource;
                comboBoxArticul.ValueMember = "articulKod";
                comboBoxArticul.DisplayMember = "articul";
                comboBoxArticul.SelectedIndex = -1;
                #endregion
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
        }

        private async void KnittingProductionPlanningReport_Load(object sender, EventArgs e)
        {
            dateEditPeriodFrom.EditValue = DateTime.Now;

            try
            {
                Task bindingsTask = InitializeBindingsAsync();
                await Task.WhenAll(bindingsTask);

                //await KnitPlanReportParametersList(1);
                Task ParametersList1Task = LoadKnitPlanReportParametersListDataAsync(1, _knitClassListBindingSource, _currentKnitClassListData);
                Task ParametersList2Task = LoadKnitPlanReportParametersListDataAsync(2, _kmlInvNumberListBindingSource, _currentKmlInvNumberListData);
                Task ParametersList3Task = LoadKnitPlanReportParametersListDataAsync(3, _monthZapListBindingSource, _currentMonthZapListData);
                Task ParametersList4Task = LoadKnitPlanReportParametersListDataAsync(4, _articulListBindingSource, _currentArticulListData);
                //_loadingScreen.CreateOverlaySpinner(this);
                //_loadingScreen.ShowOverlay();
                //try
                //{
                await Task.WhenAll(ParametersList1Task, ParametersList2Task, ParametersList3Task, ParametersList4Task);
                //}
                //finally { _loadingScreen.HideOverlay(); }

                comboBoxKnitClass.SelectedIndex = -1;
                comboBoxKmlInvNumber.SelectedIndex = -1;
                comboBoxExecMonth.SelectedIndex = -1;
                //comboBoxArticul.Sorted = true;
                comboBoxArticul.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке формы KnittingProductionPlanning");
            }
        }
        private async Task LoadKnitPlanReportParametersListDataAsync(int parameterType, BindingSource bindSource, List<KnitPlanReportParametersList> currentData)
        {
            try
            {
                BindingSource _bindSource = bindSource;
                List<KnitPlanReportParametersList> _currentData = currentData;
                _bindSource.Clear();
                _bindSource.ResetBindings(false);
                //var _data = await _vyazService.GetKnitPlanReportParametersList(parameterType);
                List<KnitPlanReportParametersList> _data = await _vyazService.GetKnitPlanReportParametersList(parameterType);
                if (_data != null)  // && _data.Count != 0
                {
                    await _logger.LogEventAsync($"Получены данные GetKnitPlanReportParametersList {parameterType}", "LoadKnitPlanReportParametersListDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentData = _data;                // Обновляем текущую модель
                        _bindSource.DataSource = _currentData; // Привязываем данные к форме
                        return Task.CompletedTask;   // <-- возвращаем Task
                    });

                    await _logger.LogEventAsync($"Данные GetKnitPlanReportParametersList успешно загружены {parameterType}", "LoadKnitPlanReportParametersListDataAsync");
                    _bindSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные GetKnitPlanReportParametersList {parameterType}", "LoadKnitPlanReportParametersListDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных GetKnitPlanReportParametersList");
            }
        }

        private void comboBoxKnitClass_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxKnitClass.SelectedIndex > -1)
            {
                checkBoxKnitClass.Checked = true;
            }
            else
            {
                checkBoxKnitClass.Checked = false;
            }
        }

        private void buttonKnitClassClear_Click(object sender, EventArgs e)
        {
            comboBoxKnitClass.SelectedIndex = -1;
        }

        private void comboBoxKmlInvNumber_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxKmlInvNumber.SelectedIndex > -1)
            {
                checkBoxKmlInvNumber.Checked = true;
            }
            else
            {
                checkBoxKmlInvNumber.Checked = false;
            }
        }

        private void buttonKmlInvNumberClear_Click(object sender, EventArgs e)
        {
            comboBoxKmlInvNumber.SelectedIndex = -1;
        }

        private void comboBoxExecMonth_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxExecMonth.SelectedIndex > -1)
            {
                checkBoxExecMonth.Checked = true;
            }
            else
            {
                checkBoxExecMonth.Checked = false;
            }
        }

        private void buttonExecMonthClear_Click(object sender, EventArgs e)
        {
            comboBoxExecMonth.SelectedIndex = -1;
            textBoxExecMonth.Text = "";
        }

        private void comboBoxArticul_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxArticul.SelectedIndex > -1)
            {
                checkBoxArticul.Checked = true;
            }
            else
            {
                checkBoxArticul.Checked = false;
            }
        }

        private void buttonArticulClear_Click(object sender, EventArgs e)
        {
            comboBoxArticul.SelectedIndex = -1;
        }

        //private string BuildConditionForReport()
        //{
        //    string xConditionForReport = string.Empty;
        //    if (сomboBoxKnitClass.SelectedIndex != -1)  // класс вязания
        //    {
        //        xConditionForReport = $"kmlIdVyazClass = {сomboBoxKnitClass.SelectedValue}";
        //    }
        //    if (comboBoxKmlInvNumber.SelectedIndex != -1)   // инвентарный № В/М
        //    {
        //        xConditionForReport = xConditionForReport + (xConditionForReport != string.Empty? " and " : "") +  $"pszkmKmlID = {comboBoxKmlInvNumber.SelectedValue}";
        //    }
        //    if (comboBoxExecMonth.SelectedIndex != -1)   // месяц запуска
        //    {
        //        xConditionForReport = xConditionForReport + (xConditionForReport != string.Empty ? " and " : "") + $"yearMonthZapInt = {comboBoxExecMonth.SelectedValue}";
        //    }
        //    if (comboBoxArticul.SelectedIndex != -1)   // артикул
        //    {
        //        xConditionForReport = xConditionForReport + (xConditionForReport != string.Empty ? " and " : "") + $"articulKod = '{comboBoxArticul.SelectedValue}'";
        //    }
        //    return xConditionForReport;
        //}

        private void buttonPrintReport_Click(object sender, EventArgs e)
        {
            //string xCondition = BuildConditionForReport();
            KnittingProductionPlanningReport report1 = new KnittingProductionPlanningReport();
            report1.RequestParameters = false;
            //report1.Parameters["_naklIz"].Value = _currentNaklViewData[0].Iz;
            //var selectedRow = _naklViewByPachKodBindingSource.Current as NaklView;
            report1.Parameters["_IdVyazClass"].Value = checkBoxKnitClass.Checked == true ? comboBoxKnitClass.SelectedValue : 0;
            report1.Parameters["_kmlID"].Value = checkBoxKmlInvNumber.Checked == true ? comboBoxKmlInvNumber.SelectedValue : 0;
            //report1.Parameters["_yearMonthZapInt"].Value = checkBoxExecMonth.Checked == true ? comboBoxExecMonth.SelectedValue : 0;
            report1.Parameters["_yearMonthZapInt"].Value = checkBoxExecMonth.Checked == true ? Convert.ToInt32(textBoxExecMonth.Text) : 0;
            report1.Parameters["_articulKod"].Value = checkBoxArticul.Checked == true ? comboBoxArticul.SelectedValue : " ";

            dateEditPeriodFrom.EditValue = (dateEditPeriodFrom.EditValue is DateTime f && f != DateTime.MinValue) ? f : null;
            dateEditPeriodTo.EditValue = (dateEditPeriodTo.EditValue is DateTime t && t != DateTime.MinValue) ? t : null;

            report1.Parameters["_dateFrom"].Value = checkBoxKnitPeriod.Checked == true ? dateEditPeriodFrom.EditValue : null;
            report1.Parameters["_dateTo"].Value = checkBoxKnitPeriod.Checked == true ? dateEditPeriodTo.EditValue : null;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        private void customSimpleButton1_Click(object sender, EventArgs e)
        {
            dateEditPeriodFrom.EditValue = null;
        }

        private void customSimpleButton2_Click(object sender, EventArgs e)
        {
            dateEditPeriodTo.EditValue = null;
        }

        private void textBoxExecMonth_EditValueChanged(object sender, EventArgs e)
        {
            if (textBoxExecMonth.Text.Length > 0)
            {
                checkBoxExecMonth.Checked = true;
            }
            else
            {
                checkBoxExecMonth.Checked = false;
            }
        }
    }
}
