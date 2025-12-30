using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using SewingProduction.Helpers;
using SewingProduction.Report;
using SewingProduction.Services;

namespace SewingProduction.form
{
    public partial class PrintBarcode : CustomForm
    {
        private BarcodePrinter _barcodePrinter;
        private string kompName = System.Environment.MachineName;
        private readonly DbService _dbService;
        public PrintBarcode()
        {
            InitializeComponent();
            _barcodePrinter = new BarcodePrinter();
            _dbService = new DbService(new DatabaseHelper());
        }
        private void PrintBarcode_Load(object sender, EventArgs e)
        {
        }

        private void customButtonPech1_Click(object sender, EventArgs e)
        {
            Pech1(customComboBoxPechVed.Text, customTextBoxPechSHK1.Text);
        }

        private void customButtonPech2_Click(object sender, EventArgs e)
        {
            Pech2(customTextBoxPechTab.Text.Trim(), customTextBoxPechSHK2.Text.Trim());
        }

        public async void Pech1(string pechVed, string pechSHK1)
        {
            if (string.IsNullOrWhiteSpace(pechVed) || string.IsNullOrWhiteSpace(pechSHK1))
            {
                MessageBox.Show("Выберите ведомость и количество ШК", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(pechVed, out int vedId) || !int.TryParse(pechSHK1, out int kolvo) || kolvo <= 0)
            {
                MessageBox.Show("Неверный формат данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = @"
                SELECT 
                    fio.tab, 
                    fio.fio, 
                    brig.n_brig, 
                    brig_ved.pref_naim, 
                    brig_ved.prefix,
                    RIGHT('00' + CAST(brig.n_brig AS VARCHAR), 2) + RIGHT('000000' + CAST(fio.tab AS VARCHAR), 6) + ' ' +
                    LTRIM(RTRIM(brig_ved.prefix)) AS sk
                FROM fio
                LEFT JOIN brig_ved ON fio.ved = brig_ved.vdID
                LEFT JOIN brig ON brig_ved.br = brig.brig
                WHERE fio.ved = @vedId
                  AND fio.datau IS NULL
                  AND ISNULL(fio.dekret, 0) = 0
                  AND (LOWER(LTRIM(RTRIM(fio.rab))) IN ('швея', 'упаковщица', 'утюжильщица', 'отпарщик', 'раскройщик', 'термоотделочник')
                  OR LOWER(LTRIM(RTRIM(fio.rab))) like 'раскройщик%' OR LOWER(LTRIM(RTRIM(fio.rab))) like 'термоотделочник%')";

            var data = await _dbService.GetListAsync<dynamic>(sql, new { vedId });

            if (data == null || data.Count == 0)
            {
                MessageBox.Show("Нет подходящих сотрудников", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataTable table = CreatePrintTable();

            foreach (var item in data)
            {
                string tabPadded = item.tab.ToString().PadLeft(12, '0');
                string kod_sh13 = _barcodePrinter.CalculateCheckSum(tabPadded);

                for (int i = 0; i < kolvo; i++)
                {
                    table.Rows.Add(
                        item.tab,
                        item.fio,
                        item.n_brig,
                        item.pref_naim,
                        item.prefix,
                        item.sk,
                        tabPadded,
                        "", // k_sum
                        tabPadded,
                        kod_sh13
                    );
                }
            }

            PrintReport(table);
        }

        public async void Pech2(string tab, string kolvoStr)
        {
            if (string.IsNullOrWhiteSpace(tab) || string.IsNullOrWhiteSpace(kolvoStr))
            {
                MessageBox.Show("Введите табельный номер и количество ШК", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(tab, out int tabNumber) || !int.TryParse(kolvoStr, out int kolvo) || kolvo <= 0)
            {
                MessageBox.Show("Неверный формат данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = @"
                SELECT 
                    fio.tab, 
                    fio.fio, 
                    brig.n_brig, 
                    brig_ved.pref_naim, 
                    brig_ved.prefix,
                    RIGHT('00' + CAST(brig.n_brig AS VARCHAR), 2) + RIGHT('000000' + CAST(fio.tab AS VARCHAR), 6) + ' ' +
                    LTRIM(RTRIM(brig_ved.prefix)) AS sk,
                    fio.datau
                FROM fio
                LEFT JOIN brig_ved ON fio.ved = brig_ved.vdID
                LEFT JOIN brig ON brig_ved.br = brig.brig
                WHERE fio.tab = @tabNumber";

            var data = await _dbService.GetListAsync<dynamic>(sql, new { tabNumber });

            if (data == null || data.Count == 0)
            {
                MessageBox.Show("Сотрудник не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var item = data[0];

            if (item.datau != null)
            {
                MessageBox.Show("Сотрудник уволен — печать запрещена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tabPadded = item.tab.ToString().PadLeft(12, '0');
            string kod_sh13 = _barcodePrinter.CalculateCheckSum(tabPadded);

            DataTable table = CreatePrintTable();

            for (int i = 0; i < kolvo; i++)
            {
                table.Rows.Add(
                    item.tab,
                    item.fio,
                    item.n_brig,
                    item.pref_naim,
                    item.prefix,
                    item.sk,
                    tabPadded,
                    "", // k_sum
                    tabPadded,
                    kod_sh13
                );
            }

            PrintReport(table);
        }

        private DataTable CreatePrintTable()
        {
            var table = new DataTable();
            table.Columns.Add("tab");
            table.Columns.Add("fio");
            table.Columns.Add("n_brig");
            table.Columns.Add("pref_naim");
            table.Columns.Add("prefix");
            table.Columns.Add("sk");
            table.Columns.Add("k_sh");
            table.Columns.Add("k_sum");
            table.Columns.Add("kod_sh");
            table.Columns.Add("kod_sh13");
            return table;
        }

        private void PrintReport(DataTable table)
        {
            var report = new PrintShkReport();
            report.DataSource = table;
            report.ShowPreviewDialog();
        }
    }
}