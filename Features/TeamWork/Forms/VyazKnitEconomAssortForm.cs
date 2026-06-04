using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors.ButtonsPanelControl;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Features.TeamWork.Services;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    internal partial class VyazKnitEconomAssortForm : CustomForm
    {
        private const string HeaderButtonSock = "vyaz-econom:sock";
        private const string HeaderButtonKnit = "vyaz-econom:knit";
        private const string HeaderButtonCord = "vyaz-econom:cord";
        private const string HeaderButtonRefresh = "vyaz-econom:refresh";

        private readonly ILogger _logger = new FileLogger();
        private readonly TWGridHelper _gridHelper = new TWGridHelper();
        private readonly VyazKnitEconomAssortDataService _dataService;
        private readonly BindingList<VyazKnitEconomAssortRow> _rows = new BindingList<VyazKnitEconomAssortRow>();
        private VyazEconomAssortPodrMode _podrMode = VyazEconomAssortPodrMode.Knit;
        private bool _isSyncingFilterButtons;
        private bool _isLoading;

        public VyazKnitEconomAssortForm(UserClass user) : base(user)
        {
            InitializeComponent();
            var dbHelper = new DatabaseHelperSQL();
            var dbService = new DbService(dbHelper);
            _dataService = new VyazKnitEconomAssortDataService(dbService);
            bindingSource.DataSource = _rows;
            InitializeHeaderButtons();
        }

        private async void VyazKnitEconomAssortForm_Load(object sender, EventArgs e)
        {
            _gridHelper.LoadGridViewSettings(gridView, "VyazKnitEconomAssortGrid.xml");
            await LoadDataAsync();
        }

        private void VyazKnitEconomAssortForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _gridHelper.SaveGridViewSettings(gridView, "VyazKnitEconomAssortGrid.xml");
            }
            catch (Exception ex)
            {
                _ = _logger.LogErrorAsync(ex, "VyazKnitEconomAssortForm_FormClosing");
            }
        }

        private void InitializeHeaderButtons()
        {
            SetHeaderButtonTag("Носочный", HeaderButtonSock);
            SetHeaderButtonTag("Вязальный", HeaderButtonKnit);
            SetHeaderButtonTag("Шнуры", HeaderButtonCord);
            SetHeaderButtonTag("Обновить", HeaderButtonRefresh);
            SetFilterControls(_podrMode);
        }

        private void SetHeaderButtonTag(string caption, string tag)
        {
            var button = layoutControlGroup2.CustomHeaderButtons
                .OfType<GroupBoxButton>()
                .FirstOrDefault(x => string.Equals(x.Caption?.Trim(), caption, StringComparison.OrdinalIgnoreCase));

            if (button != null)
            {
                button.Tag = tag;
            }
        }

        private async Task LoadDataAsync()
        {
            if (_isLoading)
            {
                return;
            }

            _isLoading = true;
            gridView.ShowLoadingPanel();
            try
            {
                gridView.CloseEditor();
                var loaded = await _dataService.LoadRowsAsync(_podrMode) ?? new System.Collections.Generic.List<VyazKnitEconomAssortRow>();

                _rows.RaiseListChangedEvents = false;
                _rows.Clear();
                foreach (var row in loaded)
                {
                    _rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "VyazKnitEconomAssortForm.LoadDataAsync");
                MessageBox.Show(
                    $"Ошибка загрузки калькуляции вязального ассортимента: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                _rows.RaiseListChangedEvents = true;
                bindingSource.ResetBindings(false);
                gridView.BestFitColumns();
                gridView.HideLoadingPanel();
                _isLoading = false;
            }
        }

        private async Task SelectFilterAsync(VyazEconomAssortPodrMode mode)
        {
            if (_podrMode == mode)
            {
                SetFilterControls(_podrMode);
                return;
            }

            _podrMode = mode;
            SetFilterControls(_podrMode);
            await LoadDataAsync();
        }

        private void SetFilterControls(VyazEconomAssortPodrMode mode)
        {
            _isSyncingFilterButtons = true;
            try
            {
                SetHeaderButtonChecked(HeaderButtonSock, mode == VyazEconomAssortPodrMode.Sock);
                SetHeaderButtonChecked(HeaderButtonKnit, mode == VyazEconomAssortPodrMode.Knit);
                SetHeaderButtonChecked(HeaderButtonCord, mode == VyazEconomAssortPodrMode.Cord);
            }
            finally
            {
                _isSyncingFilterButtons = false;
            }
        }

        private void SetHeaderButtonChecked(string tag, bool isChecked)
        {
            var button = layoutControlGroup2.CustomHeaderButtons
                .OfType<GroupBoxButton>()
                .FirstOrDefault(x => string.Equals(x.Tag as string, tag, StringComparison.OrdinalIgnoreCase));

            if (button != null && button.Checked != isChecked)
            {
                button.Checked = isChecked;
            }
        }

        private bool IsHeaderButtonChecked(string tag)
        {
            var button = layoutControlGroup2.CustomHeaderButtons
                .OfType<GroupBoxButton>()
                .FirstOrDefault(x => string.Equals(x.Tag as string, tag, StringComparison.OrdinalIgnoreCase));

            return button?.Checked == true;
        }

        private async void LayoutControlGroup2_CustomButtonClick(object sender, BaseButtonEventArgs e)
        {
            var tag = (e.Button as GroupBoxButton)?.Tag as string;
            if (tag == HeaderButtonRefresh)
            {
                await LoadDataAsync();
            }
        }

        private async void layoutControlGroup2_CustomButtonChecked(object sender, BaseButtonEventArgs e)
        {
            if (_isSyncingFilterButtons)
            {
                return;
            }

            var tag = (e.Button as GroupBoxButton)?.Tag as string;
            switch (tag)
            {
                case HeaderButtonSock:
                    await SelectFilterAsync(VyazEconomAssortPodrMode.Sock);
                    break;
                case HeaderButtonKnit:
                    await SelectFilterAsync(VyazEconomAssortPodrMode.Knit);
                    break;
                case HeaderButtonCord:
                    await SelectFilterAsync(VyazEconomAssortPodrMode.Cord);
                    break;
            }
        }

        private void layoutControlGroup2_CustomButtonUnchecked(object sender, BaseButtonEventArgs e)
        {
            if (_isSyncingFilterButtons)
            {
                return;
            }

            var tag = (e.Button as GroupBoxButton)?.Tag as string;
            if (tag is HeaderButtonSock or HeaderButtonKnit or HeaderButtonCord)
            {
                BeginInvoke(new MethodInvoker(RestoreFilterSelectionIfNeeded));
            }
        }

        private void RestoreFilterSelectionIfNeeded()
        {
            if (_isSyncingFilterButtons)
            {
                return;
            }

            if (!IsHeaderButtonChecked(HeaderButtonSock)
                && !IsHeaderButtonChecked(HeaderButtonKnit)
                && !IsHeaderButtonChecked(HeaderButtonCord))
            {
                SetFilterControls(_podrMode);
            }
        }
    }
}
