using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraRichEdit.Model;
using SewingProduction;

namespace SewingProduction.form
{
    public partial class PrintBarcode : CustomForm
    {
        private BarcodePrinter _barcodePrinter;
        private string kompName = System.Environment.MachineName;
        public PrintBarcode()
        {
            InitializeComponent();
            _barcodePrinter = new BarcodePrinter();
        }
        private void PrintBarcode_Load(object sender, EventArgs e)
        {
        }

        private void customButtonPech1_Click(object sender, EventArgs e)
        {
            Pech1(customComboBoxPechVed.Text, customTextBoxPechSHK1.Text);
        }
        public void Pech1(string PechVed, string PechSHK1)
        {
            if (string.IsNullOrEmpty(PechVed) || string.IsNullOrEmpty(PechSHK1))
            {
                MessageBox.Show("Выберите ведомость и количество ШК", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int vedId = int.Parse(PechVed);
            int kolvo = int.Parse(PechSHK1);
            DataTable data = GetDataForPrintByVed(vedId);

            foreach (DataRow row in data.Rows)
            {
                string kod_sh = row["k_sh"].ToString();
                string ean13 = _barcodePrinter.GenerateEAN13(kod_sh);
                // Печать или сбор в список
            }

            MessageBox.Show($"Сгенерировано {data.Rows.Count} ШК");
        }

        private void customButtonPech2_Click(object sender, EventArgs e)
        {
            Pech2(customTextBoxPechTab.Text.Trim(), customTextBoxPechSHK2.Text.Trim());
        }
        public void Pech2(string tab, string kolvo)
        {
            if (string.IsNullOrEmpty(tab) || string.IsNullOrEmpty(kolvo))
            {
                MessageBox.Show("Введите табельный номер и количество ШК", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string kod_sh = tab.PadLeft(12, '0'); // или другая логика
            string ean13 = _barcodePrinter.GenerateEAN13(kod_sh);
            MessageBox.Show($"Штрихкод: {ean13}");
        }
        // Метод для получения данных для печати по ведомости (command2.click)
        public DataTable GetDataForPrintByVed(int vdId)
        {
            string query = @"
            SELECT 
                fio.tab AS tab,
                SUBSTRING(fio.fio, 1, CHARINDEX(' ', fio.fio) - 1) + ' ' + SUBSTRING(fio.fio, CHARINDEX(' ', fio.fio) + 1, 1) + '.' + SUBSTRING(fio.fio, CHARINDEX(' ', fio.fio, CHARINDEX(' ', fio.fio) + 1) + 1, 1) + '.' AS fio,
                brig.n_brig AS n_brig,
                brig_ved_sk.pref_naim AS pref_naim,
                brig_ved_sk.prefix AS prefix,
               REPLICATE('0', 2 - LEN(CAST(brig.n_brig AS VARCHAR(2)))) + CAST(brig.n_brig AS VARCHAR(2)) + REPLICATE('0', 6 - LEN(CAST(fio.tab AS VARCHAR(6)))) + CAST(fio.tab AS VARCHAR(6)) + ' ' +
               REPLICATE('0', 5 - LEN(ISNULL(brig_ved_sk.prefix,''))) + ISNULL(brig_ved_sk.prefix,'') AS sk,
			   REPLICATE('0', 12 - LEN(CAST(fio.tab AS VARCHAR(12)))) + CAST(fio.tab AS VARCHAR(12)) AS k_sh,
                '' AS k_sum,
                SPACE(13) AS kod_sh,
                SPACE(15) AS kod_sh13
            FROM fio
			LEFT JOIN brig_ved_sk ON fio.ved=brig_ved_sk.vdID
			LEFT JOIN brig ON brig_ved_sk.br = brig.brig
            WHERE fio.ved = @vdId AND fio.datau IS NULL AND ISNULL(fio.dekret, 0) = 0
            AND (LOWER(RTRIM(fio.rab)) = 'швея' OR LOWER(RTRIM(fio.rab)) = 'упаковщица' OR LOWER(RTRIM(fio.rab)) = 'утюжильщица');
        ";
            SqlParameter param = new SqlParameter("@vdId", SqlDbType.Int);
            param.Value = vdId;
            return ShowRelatedData("ace", query, param);
        }
        // Метод для получения данных для печати по табельному номеру (command1.click)
        public DataTable GetDataForPrintByTab(int tabNumber)
        {
            string query = @"
           SELECT 
                fio.tab AS tab,
                SUBSTRING(fio.fio, 1, CHARINDEX(' ', fio.fio) - 1) + ' ' + SUBSTRING(fio.fio, CHARINDEX(' ', fio.fio) + 1, 1) + '.' + SUBSTRING(fio.fio, CHARINDEX(' ', fio.fio, CHARINDEX(' ', fio.fio) + 1) + 1, 1) + '.' AS fio,
                brig.n_brig AS n_brig,
                brig_ved_sk.pref_naim AS pref_naim,
                brig_ved_sk.prefix AS prefix,
               REPLICATE('0', 2 - LEN(CAST(brig.n_brig AS VARCHAR(2)))) + CAST(brig.n_brig AS VARCHAR(2)) + REPLICATE('0', 6 - LEN(CAST(fio.tab AS VARCHAR(6)))) + CAST(fio.tab AS VARCHAR(6)) + ' ' +
               REPLICATE('0', 5 - LEN(ISNULL(brig_ved_sk.prefix,''))) + ISNULL(brig_ved_sk.prefix,'') AS sk,
			    REPLICATE('0', 12 - LEN(CAST(fio.tab AS VARCHAR(12)))) + CAST(fio.tab AS VARCHAR(12)) AS k_sh,
                '' AS k_sum,
                SPACE(13) AS kod_sh,
                SPACE(15) AS kod_sh13
            FROM fio
			LEFT JOIN brig_ved_sk ON fio.ved=brig_ved_sk.vdID
			LEFT JOIN brig ON brig_ved_sk.br = brig.brig
            WHERE fio.tab = @tabNumber AND fio.datau IS NULL;
        ";

            SqlParameter param = new SqlParameter("@tabNumber", SqlDbType.Int);
            param.Value = tabNumber;
            return ShowRelatedData("ace", query, param);
        }
        public bool IsEmployeeDismissed(int tabNumber)
        {
            string query = "SELECT datau FROM fio WHERE tab = @tabNumber";
            SqlParameter param = new SqlParameter("@tabNumber", SqlDbType.Int);
            param.Value = tabNumber;
            DateTime? datau = GetScalarValue<DateTime?>(query, param);
            return datau.HasValue;
        }
        public string GetPrinterSettings(string reportName)
        {
            string query = @"select param from printer_parameters 
                        where komp_name=@kompName and pp_prg_name='proizv_set' and shortname=@reportName";
            SqlParameter[] parameters = new SqlParameter[]
          {
            new SqlParameter("@kompName", SqlDbType.NVarChar, 255) { Value = kompName },
            new SqlParameter("@reportName", SqlDbType.NVarChar, 255) { Value = reportName }
          };
            return GetScalarValue<string>(query, parameters);
        }
        public void UpdatePrinterSettings(string printerSettings, int ppId)
        {
            string query = "update printer_parameters set param=@printerSettings where pp_id=@ppId";
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@printerSettings", SqlDbType.NVarChar, -1) { Value = printerSettings},
             new SqlParameter("@ppId", SqlDbType.Int) { Value = ppId }
            };

            ExecuteNonQuery(query, parameters);
        }
        public DataTable GetPrinterSettingsData(string reportName)
        {
            string query = @"select * from printer_parameters 
                        where komp_name=@kompName and pp_prg_name='proizv_set' and shortname=@reportName";
            SqlParameter[] parameters = new SqlParameter[]
          {
            new SqlParameter("@kompName", SqlDbType.NVarChar, 255) { Value = kompName },
            new SqlParameter("@reportName", SqlDbType.NVarChar, 255) { Value = reportName }
          };
            return ShowRelatedData("ace", query, parameters);
        }
        //Универсальный метод для выполнения INSERT, UPDATE, DELETE запросов
        private void ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            string connectionString = Properties.Settings.Default.ACEConnectionString3;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null) command.Parameters.AddRange(parameters);
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        connection.Close();
                        if (rowsAffected < 0)
                        {
                            throw new Exception($"Error executing query: {command.CommandText}. RowsAffected: {rowsAffected}");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Исключение при выполнении запроса: {command.CommandText} \n Message: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw new Exception($"Exception executing query: {command.CommandText} \n Message: {ex.Message}");
                    }
                }
            }
        }
        //Универсальный метод для получения scalar value
        private T GetScalarValue<T>(string query, params SqlParameter[] parameters)
        {
            string connectionString = Properties.Settings.Default.ACEConnectionString3;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null) command.Parameters.AddRange(parameters);
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        connection.Close();
                        if (result == null || result == DBNull.Value)
                        {
                            return default(T);
                        }
                        return (T)Convert.ChangeType(result, typeof(T));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при чтении данных {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw new Exception($"Error getting scalar value: {ex.Message}");
                    }
                }
            }
        }

        protected System.Data.DataTable ShowRelatedData(string _serv, string query, params SqlParameter[] parameters)
        {
            System.Data.DataTable dT = new System.Data.DataTable();
            try
            {
                string _connStr = "";
                switch (_serv.ToLower())
                {
                    case "ace": _connStr = Properties.Settings.Default.ACEConnectionString3; break;
                    case "ace_test": _connStr = Properties.Settings.Default.ACEtestConnectionString; break;
                    case "oms": _connStr = Properties.Settings.Default.OMSConnectionString; break;
                    case "global": _connStr = Properties.Settings.Default.GlobalConnectionString; break;
                }
                string connectionString = _connStr;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null) command.Parameters.AddRange(parameters);
                        adapter.SelectCommand = command;
                        adapter.Fill(dT);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dT;
        }

    }
}