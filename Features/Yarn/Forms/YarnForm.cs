using DevExpress.XtraGrid.Views.Grid;
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
        private readonly BindingList<YarnCardRow> _rows = new BindingList<YarnCardRow>();

        public YarnForm(UserClass user) : base(user)
        {
            InitializeComponent();
            var dbHelper = new DatabaseHelperSQL();
            var dbService = new DbService(dbHelper);
            _dataService = new YarnDataService(dbService, dbHelper);
            bindingSource.DataSource = _rows;
            gridView.ApplyReadOnly();
            gridView.PopupMenuShowing += (s, e) =>
                GridContextMenuHelper.AddCopyCellMenuItem(s, e);
            btnHistory.Enabled = false;
            btnObnovit.Enabled = false;
        }

        private void YarnForm_Load(object sender, EventArgs e)
        {
            txtNakl.Focus();
        }

        private async void txtNakl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;
            var nakl = txtNakl.Text?.Trim();
            if (string.IsNullOrWhiteSpace(nakl))
                return;

            await LoadCardDataAsync(nakl);
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            var kodArt = txtArticul.Text?.Trim();
            if (string.IsNullOrWhiteSpace(kodArt))
            {
                MessageBox.Show("Сначала загрузите данные карты.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var historyForm = new YarnHistoryForm(_dataService, kodArt, kodArt, "");
            historyForm.ShowDialog(this);
        }

        private async void btnCalc_Click(object sender, EventArgs e)
        {
            var nakl = txtNakl.Text?.Trim();
            if (string.IsNullOrWhiteSpace(nakl) || nakl == "0")
                return;

            try
            {
                var newSeb = await _dataService.CalculateCostAsync(nakl);
                if (newSeb.HasValue)
                    txtSebUpr.Text = newSeb.Value.ToString("F2");
                else
                    MessageBox.Show("Карта не найдена!", "Внимание",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "YarnForm.btnCalc_Click");
                MessageBox.Show($"Ошибка расчета: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnObnovit_Click(object sender, EventArgs e)
        {
            var nakl = txtNakl.Text?.Trim();
            if (string.IsNullOrWhiteSpace(nakl) || nakl == "0")
            {
                MessageBox.Show("Введите номер карты пряжи!", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var kodArt = txtArticul.Text?.Trim();
            if (string.IsNullOrWhiteSpace(kodArt))
            {
                MessageBox.Show("Сначала загрузите данные карты (нажмите Enter в поле номера).",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (kodArt.Length > 7)
                kodArt = kodArt.Substring(0, 7);

            if (MessageBox.Show(
                    "Обновить себестоимость в п\\ф и приходных накладных?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            btnObnovit.Enabled = false;
            gridView.ShowLoadingPanel();
            try
            {
                var result = await _dataService.RecalculateCostAsync(nakl, kodArt);

                if (!result.IsOk)
                {
                    MessageBox.Show(result.MessageError, "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (result.SebUpr.HasValue)
                    txtSebUpr.Text = result.SebUpr.Value.ToString("F3");

                await LoadCardDataAsync(nakl);

                MessageBox.Show(
                    "Себестоимость пересчитана в п\\ф и расходных накладных!",
                    "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "YarnForm.btnObnovit_Click");
                MessageBox.Show($"Ошибка пересчета: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnObnovit.Enabled = true;
                gridView.HideLoadingPanel();
            }
        }

        private async Task LoadCardDataAsync(string nakl)
        {
            try
            {
                var data = await _dataService.LoadCardDataAsync(nakl);

                _rows.Clear();
                foreach (var row in data)
                    _rows.Add(row);

                gridControl.RefreshDataSource();

                bool hasData = data.Count > 0;
                btnObnovit.Enabled = hasData;
                btnHistory.Enabled = hasData;

                if (hasData)
                {
                    txtArticul.Text = data.First().t_articul?.Trim() ?? "";
                    txtSebUpr.Text = data.First().seb_t_m?.ToString("F3") ?? "";
                }
                else
                {
                    txtArticul.Text = "";
                    txtSebUpr.Text = "";
                    MessageBox.Show("Карта не найдена!", "Внимание",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "YarnForm.LoadCardDataAsync");
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
