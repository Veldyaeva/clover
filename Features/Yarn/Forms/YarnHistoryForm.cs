using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Extensions;
using SewingProduction.Features.Yarn.Models;
using SewingProduction.Features.Yarn.Services;
using SewingProduction.Helpers;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SewingProduction.Features.Yarn.Forms
{
    internal partial class YarnHistoryForm : Form
    {
        private readonly ILogger _logger = new FileLogger();
        private readonly YarnDataService _dataService;
        private readonly string _kodd;
        private readonly string _kod;
        private readonly string _grup;
        private readonly BindingList<YarnCostHistoryRow> _costRows = new BindingList<YarnCostHistoryRow>();
        private readonly BindingList<YarnArticulHistoryRow> _articulRows = new BindingList<YarnArticulHistoryRow>();

        public YarnHistoryForm(YarnDataService dataService, string kodd, string kod, string grup)
        {
            InitializeComponent();
            _dataService = dataService;
            _kodd = kodd;
            _kod = kod;
            _grup = grup ?? "";
            bsCostHistory.DataSource = _costRows;
            bsArticulHistory.DataSource = _articulRows;
            gridViewCostHistory.ApplyReadOnly();
            gridViewArticulHistory.ApplyReadOnly();
            gridViewCostHistory.PopupMenuShowing += (s, e) =>
                GridContextMenuHelper.AddCopyCellMenuItem(s, e);
            gridViewArticulHistory.PopupMenuShowing += (s, e) =>
                GridContextMenuHelper.AddCopyCellMenuItem(s, e);
        }

        private async void YarnHistoryForm_Load(object sender, EventArgs e)
        {
            try
            {
                bool filterByKod = _grup.ToLower().Contains("носки детские")
                                || _grup.ToLower().Contains("носки дет.(набор)");

                var costData = await _dataService.LoadCostHistoryAsync(_kodd, _kod, filterByKod);
                _costRows.Clear();
                foreach (var row in costData)
                    _costRows.Add(row);

                var articulData = await _dataService.LoadArticulHistoryAsync(_kod);
                _articulRows.Clear();
                foreach (var row in articulData)
                    _articulRows.Add(row);

                gridCostHistory.RefreshDataSource();
                gridArticulHistory.RefreshDataSource();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "YarnHistoryForm.Load");
                MessageBox.Show($"Ошибка загрузки истории: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
