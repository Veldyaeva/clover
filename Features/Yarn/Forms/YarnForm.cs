using SewingProduction.Extensions;
using SewingProduction.Features.Yarn.Models;
using SewingProduction.Features.Yarn.Services;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.Yarn.Forms
{
    internal partial class YarnForm : CustomForm
    {
        private readonly ILogger _logger = new FileLogger();
        private readonly YarnDataService _dataService;
        private readonly BindingList<YarnCardRow> _yarnRows = new();
        private readonly BindingList<YarnCardRow> _colorRows = new();
        private readonly BindingList<FabricSpreadingRow> _spreadingRows = new();

        public YarnForm(UserClass user) : base(user)
        {
            InitializeComponent();
            var dbHelper = new DatabaseHelperSQL();
            var dbService = new DbService(dbHelper);
            _dataService = new YarnDataService(dbService, dbHelper);

            bsYarnData.DataSource = _yarnRows;
            bsColorResult.DataSource = _colorRows;
            bsSpreading.DataSource = _spreadingRows;

            gvYarnData.PopupMenuShowing += (s, e) => GridContextMenuHelper.AddCopyCellMenuItem(s, e);
            gvColorResult.PopupMenuShowing += (s, e) => GridContextMenuHelper.AddCopyCellMenuItem(s, e);
            gvSpreading.PopupMenuShowing += (s, e) => GridContextMenuHelper.AddCopyCellMenuItem(s, e);

            btnObnovit.Enabled = false;
            btnHistory.Enabled = false;
        }

        private void YarnForm_Load(object sender, EventArgs e)
        {
            splitMain.SplitterDistance = splitMain.Width / 2;
            txtFabricNakl.Focus();
        }

        // ==================== Полотно ====================

        private void txtFabricNakl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            // TODO: загрузка данных карты полотна
        }

        private void btnFabricReplace_Click(object sender, EventArgs e)
        {
            // TODO: замена себестоимости полотна
        }

        // ==================== Пряжа ====================

        private async void txtNakl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            var nakl = txtNakl.Text?.Trim();
            if (!string.IsNullOrWhiteSpace(nakl))
                await LoadYarnCardAsync(nakl);
        }

        private async void btnCalc_Click(object sender, EventArgs e)
        {
            var nakl = txtNakl.Text?.Trim();
            if (string.IsNullOrWhiteSpace(nakl) || nakl == "0") return;
            try
            {
                var seb = await _dataService.CalculateCostAsync(nakl);
                txtSebUpr.Text = seb.HasValue ? seb.Value.ToString("F2") : "";
                if (!seb.HasValue)
                    MessageBox.Show("Карта не найдена!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "YarnForm.btnCalc_Click");
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnObnovit_Click(object sender, EventArgs e)
        {
            var nakl = txtNakl.Text?.Trim();
            if (string.IsNullOrWhiteSpace(nakl) || nakl == "0")
            { MessageBox.Show("Введите номер карты пряжи!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var kodArt = txtArticul.Text?.Trim();
            if (string.IsNullOrWhiteSpace(kodArt))
            { MessageBox.Show("Сначала загрузите данные карты.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (kodArt.Length > 7) kodArt = kodArt.Substring(0, 7);

            if (MessageBox.Show("Обновить себестоимость в п\\ф и приходных накладных?", "Подтверждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            btnObnovit.Enabled = false;
            try
            {
                var result = await _dataService.RecalculateCostAsync(nakl, kodArt);
                if (!result.IsOk)
                { MessageBox.Show(result.MessageError, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                if (result.SebUpr.HasValue) txtSebUpr.Text = result.SebUpr.Value.ToString("F3");
                await LoadYarnCardAsync(nakl);

                MessageBox.Show("Себестоимость пересчитана в п\\ф и расходных накладных!", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "YarnForm.btnObnovit_Click");
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { btnObnovit.Enabled = true; }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            var kodArt = txtArticul.Text?.Trim();
            if (string.IsNullOrWhiteSpace(kodArt))
            { MessageBox.Show("Сначала загрузите данные карты.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            new YarnHistoryForm(_dataService, kodArt, kodArt, "").ShowDialog(this);
        }

        // ==================== Поиск по цвету ====================

        private async void txtColorSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            var color = txtColorSearch.Text?.Trim();
            if (string.IsNullOrWhiteSpace(color)) return;
            try
            {
                var data = await _dataService.SearchByColorAsync(color);
                _colorRows.Clear();
                foreach (var r in data) _colorRows.Add(r);
                gridColorResult.RefreshDataSource();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "YarnForm.txtColorSearch_KeyDown");
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvColorResult_DoubleClick(object sender, EventArgs e)
        {
            if (gvColorResult.FocusedRowHandle < 0) return;
            var row = gvColorResult.GetRow(gvColorResult.FocusedRowHandle) as YarnCardRow;
            if (row == null || string.IsNullOrWhiteSpace(row.nakl)) return;
            txtNakl.Text = row.nakl.Trim();
            _ = LoadYarnCardAsync(row.nakl.Trim());
        }

        // ==================== Data ====================

        private async Task LoadYarnCardAsync(string nakl)
        {
            try
            {
                var data = await _dataService.LoadCardDataAsync(nakl);
                _yarnRows.Clear();
                foreach (var r in data) _yarnRows.Add(r);
                gridYarnData.RefreshDataSource();

                bool ok = data.Count > 0;
                btnObnovit.Enabled = ok;
                btnHistory.Enabled = ok;

                if (ok)
                {
                    txtArticul.Text = data.First().t_articul?.Trim() ?? "";
                    txtSebUpr.Text = data.First().seb_t_m?.ToString("F3") ?? "";
                }
                else
                {
                    txtArticul.Text = "";
                    txtSebUpr.Text = "";
                    MessageBox.Show("Карта не найдена!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "YarnForm.LoadYarnCardAsync");
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
