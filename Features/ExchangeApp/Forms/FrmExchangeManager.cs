using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using ExchangeApp.Data;
using ExchangeApp.Models;
using ExchangeApp.Services;
using SewingProduction;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExchangeApp.Forms
{
    public partial class FrmExchangeManager : CustomForm
    {
        private readonly IExchangeManagerService _service;
        private List<ExchangeDocumentItem> _documents = new List<ExchangeDocumentItem>();
        private List<ExportBatchItem> _batches = new List<ExportBatchItem>();

        private readonly DataBaseHelperPostgreSQL _dbHelper;
        //public FrmExchangeManager(UserClass User, IExchangeManagerService service) : base(User)
        //{
        //    _service = service ?? throw new ArgumentNullException(nameof(service));
        //    InitializeComponent();
        //    _dbHelper = new DatabaseHelper("cleverPG");
        //    ConfigureDocumentGrid();
        //    ConfigureBatchGrid();
        //}
        public FrmExchangeManager(UserClass User) : base(User)
        {
            _service = BuildExchangeManagerService();
            InitializeComponent();
            InitModeItems();
            _dbHelper = new DataBaseHelperPostgreSQL("cleverPG");
            ConfigureDocumentGrid();
            ConfigureBatchGrid();
        }
        private async void FrmExchangeManager_Load(object sender, EventArgs e)
        {
            if (_service == null)
                throw new Exception("_service == null");
            await InitializeFormAsync();
            //MessageBox.Show(rgMode.Properties.Items.Count.ToString());
        }
        private void InitModeItems()
        {
            rgMode.Properties.Items.Clear();
            rgMode.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(ExportRunMode.Primary, "Первичная"));
            rgMode.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(ExportRunMode.Delta, "Догрузка"));
            rgMode.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(ExportRunMode.Reexport, "Перевыгрузка"));
            rgMode.EditValue = ExportRunMode.Primary;
        }
        private static IExchangeManagerService BuildExchangeManagerService()
        {
            //string connectionString = SewingProduction.Properties.Settings.Default.CleverPGConnectionString;

            //var repository = new ExchangeRepository(connectionString);
            //return new ExchangeManagerService(repository);
            var repository = new ExchangeRepository();
            return new ExchangeManagerService(repository);
        }
        private async Task InitializeFormAsync()
        {
            try
            {
                ToggleUi(false);

                await LoadCompaniesAsync();
                await LoadExportTypesAsync();
                await RefreshBatchesAsync();

                deFrom.EditValue = DateTime.Today.AddDays(-7);
                deTo.EditValue = DateTime.Today;
                rgMode.EditValue = ExportRunMode.Primary;

                ApplyModeUi();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Ошибка инициализации");
            }
            finally
            {
                ToggleUi(true);
            }
        }

        private async Task LoadCompaniesAsync()
        {
            var companies = await _service.GetCompaniesAsync();

            lueCompany.Properties.DataSource = companies;
            lueCompany.Properties.DisplayMember = nameof(CompanyItem.CompanyName);
            lueCompany.Properties.ValueMember = nameof(CompanyItem.CompanyId);
            lueCompany.Properties.NullText = "";

            if (companies.Count > 0)
                lueCompany.EditValue = companies[0].CompanyId;
        }

        //private async Task LoadExportTypesAsync()
        //{
        //    var exportTypes = await _service.GetExportTypesAsync();

        //    ccbeExportTypes.Properties.Items.Clear();
        //    foreach (var item in exportTypes)
        //    {
        //        //cbeExportTypes.Properties.Items.Add(item.Code, item.Name, item.IsSelected);
        //        ccbeExportTypes.Properties.Items.Add(
        //            item.Code,
        //            item.Name,
        //            item.IsSelected ? CheckState.Checked : CheckState.Unchecked,
        //            true
        //        );
        //    }

        //    if (ccbeExportTypes.Properties.Items.Count > 0)
        //    {
        //        ccbeExportTypes.Properties.Items[0].CheckState = System.Windows.Forms.CheckState.Checked;
        //    }
        //}
        private async Task LoadExportTypesAsync()
        {
            var exportTypes = await _service.GetExportTypesAsync();

            ccbeExportTypes.Properties.Items.Clear();
            foreach (var item in exportTypes)
            {
                ccbeExportTypes.Properties.Items.Add(
                    item.Code,
                    item.Name,
                    item.IsSelected ? CheckState.Checked : CheckState.Unchecked,
                    true
                );
            }

            if (ccbeExportTypes.Properties.Items.Count > 0)
            {
                ccbeExportTypes.Properties.Items[0].CheckState = System.Windows.Forms.CheckState.Checked;
            }
        }

        private async Task LoadDocumentsAsync()
        {
            var selectedTypes = GetSelectedExportTypes();
            if (selectedTypes.Count == 0)
            {
                XtraMessageBox.Show(this, "Выберите хотя бы один вид выгрузки.");
                return;
            }

            if (!TryGetSelectedCompanyId(out int companyId))
                return;

            if (deFrom.EditValue == null || deTo.EditValue == null)
            {
                XtraMessageBox.Show(this, "Укажите период.");
                return;
            }

            var dateFrom = Convert.ToDateTime(deFrom.EditValue).Date;
            var dateTo = Convert.ToDateTime(deTo.EditValue).Date;

            ToggleUi(false);
            try
            {
                // Для минимального интерфейса показываем документы по первому выбранному виду выгрузки.
                _documents = await _service.GetDocumentsAsync(companyId, dateFrom, dateTo, selectedTypes[0]);
                gcDocuments.DataSource = _documents;
                gvDocuments.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Ошибка загрузки документов");
            }
            finally
            {
                ToggleUi(true);
            }
        }

        private async Task RefreshBatchesAsync()
        {
            if (!TryGetSelectedCompanyId(out int companyId))
                return;

            ToggleUi(false);
            try
            {
                _batches = await _service.GetBatchesAsync(companyId);
                gcBatches.DataSource = _batches;
                gvBatches.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Ошибка загрузки пакетов");
            }
            finally
            {
                ToggleUi(true);
            }
        }

        private async Task RunAsync()
        {
            var selectedTypes = GetSelectedExportTypes();
            if (selectedTypes.Count == 0)
            {
                XtraMessageBox.Show(this, "Выберите хотя бы один вид выгрузки.");
                return;
            }

            if (!TryGetSelectedCompanyId(out int companyId))
                return;

            if (deFrom.EditValue == null || deTo.EditValue == null)
            {
                XtraMessageBox.Show(this, "Укажите период.");
                return;
            }

            var mode = (ExportRunMode)rgMode.EditValue;
            var userName = Environment.UserName;
            var dateFrom = Convert.ToDateTime(deFrom.EditValue).Date;
            var dateTo = Convert.ToDateTime(deTo.EditValue).Date;

            ToggleUi(false);
            try
            {
                if (mode == ExportRunMode.Primary)
                {
                    foreach (var exportType in selectedTypes)
                    {
                        await _service.RunPrimaryExportAsync(exportType, companyId, dateFrom, dateTo, userName);
                    }
                }
                else
                {
                    var selectedDocIds = _documents
                        .Where(x => x.IsSelected)
                        .Select(x => x.DocumentId)
                        .Distinct()
                        .ToList();

                    if (selectedDocIds.Count == 0)
                    {
                        XtraMessageBox.Show(this, "Отметьте хотя бы один документ.");
                        return;
                    }

                    foreach (var exportType in selectedTypes)
                    {
                        await _service.CreateRequestAndRunAsync(exportType, mode, companyId, selectedDocIds, userName);
                    }
                }

                await RefreshBatchesAsync();
                XtraMessageBox.Show(this, "Операция выполнена.");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Ошибка выполнения");
            }
            finally
            {
                ToggleUi(true);
            }
        }

        private List<string> GetSelectedExportTypes()
        {
            return ccbeExportTypes.Properties.Items
                .GetCheckedValues()
                .Cast<object>()
                .Select(x => x?.ToString())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();
        }

        private bool TryGetSelectedCompanyId(out int companyId)
        {
            companyId = 0;

            if (lueCompany.EditValue == null)
            {
                XtraMessageBox.Show(this, "Выберите организацию.");
                return false;
            }

            if (!int.TryParse(lueCompany.EditValue.ToString(), out companyId))
            {
                XtraMessageBox.Show(this, "Некорректная организация.");
                return false;
            }

            return true;
        }

        private void ToggleUi(bool enabled)
        {
            UseWaitCursor = !enabled;
            panelTop.Enabled = enabled;
            panelBottom.Enabled = enabled;
            splitMain.Enabled = enabled;
        }

        private void ApplyModeUi()
        {
            var mode = (ExportRunMode)rgMode.EditValue;
            bool needDocuments = mode != ExportRunMode.Primary;

            gcDocuments.Enabled = true;
            btnLoadDocuments.Enabled = true;

            lblHint.Text = mode switch
            {
                ExportRunMode.Primary => "Первичная выгрузка: документы можно не отмечать, пакет формируется за период.",
                ExportRunMode.Delta => "Догрузка: отметьте документы, которые нужно догрузить.",
                ExportRunMode.Reexport => "Перевыгрузка: отметьте документы, которые нужно перевыгрузить.",
                _ => string.Empty
            };

            if (!needDocuments)
            {
                // Документы можно загружать и при первичной, но это не обязательно.
                gvDocuments.ClearSelection();
            }
        }

        private async void btnLoadDocuments_Click(object sender, EventArgs e)
        {
            await LoadDocumentsAsync();
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            await RunAsync();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await RefreshBatchesAsync();
        }

        private void rgMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyModeUi();
        }

        private void btnSelectAllDocuments_Click(object sender, EventArgs e)
        {
            foreach (var doc in _documents)
                doc.IsSelected = true;

            gcDocuments.RefreshDataSource();
        }

        private void btnUnselectAllDocuments_Click(object sender, EventArgs e)
        {
            foreach (var doc in _documents)
                doc.IsSelected = false;

            gcDocuments.RefreshDataSource();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gvDocuments_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            var row = gvDocuments.GetRow(e.RowHandle) as ExchangeDocumentItem;
            if (row == null)
                return;

            if (row.NeedsReexport)
            {
                e.Appearance.BackColor = Color.MistyRose;
            }
            else if (row.NeedsExport)
            {
                e.Appearance.BackColor = Color.LemonChiffon;
            }

            if (string.Equals(row.LastLoadStatus, "error", StringComparison.OrdinalIgnoreCase))
            {
                e.Appearance.ForeColor = Color.DarkRed;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            else if (string.Equals(row.LastLoadStatus, "loaded", StringComparison.OrdinalIgnoreCase))
            {
                e.Appearance.ForeColor = Color.DarkGreen;
            }
        }

        private void gvBatches_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            var row = gvBatches.GetRow(e.RowHandle) as ExportBatchItem;
            if (row == null)
                return;

            if (string.Equals(row.Status, "error", StringComparison.OrdinalIgnoreCase))
            {
                e.Appearance.BackColor = Color.MistyRose;
                e.Appearance.ForeColor = Color.DarkRed;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            else if (string.Equals(row.Status, "partially_loaded", StringComparison.OrdinalIgnoreCase))
            {
                e.Appearance.BackColor = Color.LemonChiffon;
                e.Appearance.ForeColor = Color.DarkOrange;
            }
            else if (string.Equals(row.Status, "loaded", StringComparison.OrdinalIgnoreCase))
            {
                e.Appearance.BackColor = Color.Honeydew;
                e.Appearance.ForeColor = Color.DarkGreen;
            }
            else if (string.Equals(row.Status, "ready", StringComparison.OrdinalIgnoreCase))
            {
                e.Appearance.ForeColor = Color.DarkBlue;
            }
        }

        private void gvBatches_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == null || e.Value == null)
                return;

            if (e.Column.FieldName == nameof(ExportBatchItem.Reason))
            {
                e.DisplayText = e.Value.ToString() switch
                {
                    "primary" => "Первичная",
                    "delta" => "Догрузка",
                    "reexport" => "Перевыгрузка",
                    "manual" => "Ручная",
                    _ => e.Value.ToString()
                };
            }

            if (e.Column.FieldName == nameof(ExportBatchItem.Status))
            {
                e.DisplayText = e.Value.ToString() switch
                {
                    "new" => "Новый",
                    "building" => "Формируется",
                    "ready" => "Готов",
                    "sent" => "Отправлен",
                    "loaded" => "Загружен",
                    "partially_loaded" => "Частично загружен",
                    "error" => "Ошибка",
                    "cancelled" => "Отменен",
                    _ => e.Value.ToString()
                };
            }
        }

        private void gvDocuments_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == null || e.Value == null)
                return;

            if (e.Column.FieldName == nameof(ExchangeDocumentItem.LastLoadStatus))
            {
                e.DisplayText = e.Value.ToString() switch
                {
                    "loaded" => "Загружен",
                    "error" => "Ошибка",
                    "skipped" => "Пропущен",
                    "pending" => "Ожидает",
                    _ => e.Value.ToString()
                };
            }
        }
    }
}