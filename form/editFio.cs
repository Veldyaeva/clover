using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.CodeParser;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit.Import.Html;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace SewingProduction.form
{
    public partial class editFio : CustomForm
    {
        // Оснавная БД:
        //string connectionString = Properties.Settings.Default.ACEConnectionString;
        // Для тестов:
        string connectionString = Properties.Settings.Default.ACEtestConnectionString;
        public editFio(string idFIO, string openType)
        {
            InitializeComponent();
            //Имя формы:
            this.Text = openType;
            customTextBoxTab.Text = idFIO;
        }

        private void editFio_Load(object sender, EventArgs e)
        {
            comboAllTableItems(sender, e);
            if (this.Text == "Редактирование сотрудника")
                oldUser();
            else
                newUser();
        }
        private void comboAllTableItems(object sender, EventArgs e)
        {
            using (SqlConnection connectionCombo = new SqlConnection(connectionString))
            {
                comboOneTableItems(customComboBoxOrg,    "SELECT TRIM(name)       FROM sp_firms       ORDER BY kod", connectionCombo, true);
                comboOneTableItems(customComboBoxDolj,   "SELECT TRIM(rab)        FROM rab            ORDER BY rab ", connectionCombo, true);
                comboOneTableItems(customComboBoxOb,     "SELECT TRIM(fvr_name)   FROM fio_vid_rabot  ORDER BY fvr_kod", connectionCombo, true);
                comboOneTableItems(customComboBoxPodr,   "SELECT TRIM(name)       FROM brig_object    ORDER BY gr", connectionCombo, true);
                comboOneTableItems(customComboBoxNTab,   "SELECT TRIM(naimen)     FROM tab_n          ORDER BY naimen", connectionCombo, true);
                comboOneTableItems(customComboBoxPodr1c, "SELECT DISTINCT TRIM(podrname1c) FROM spbrig", connectionCombo,true);
            }
        }
        private void comboOneTableItems(System.Windows.Forms.ComboBox comboBox,string query, SqlConnection connection, bool allOrOne)
        {
            /* comboBox: сам комбобокс
             * query: текст запроса
             * connection: подключение
             * allOrOne: 
             * true - для заполнения комбобокса
             * false - для отображения значения
             */
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
            //Создаем в памяти таблицу:
            System.Data.DataTable tableList = new System.Data.DataTable();
            //Добавляем ответ сервера в таблицу:
            dataAdapter.Fill(tableList);
            if (allOrOne)
            {
                comboBox.Items.Clear();
                //Загрузка в комбобокс:
                foreach (DataRow row in tableList.Rows)
                {
                    comboBox.Items.Add(row[0].ToString());
                }
            }
            else
            {
                comboBox.Text = tableList.Rows.Count > 0 ? tableList.Rows[0]["nameColumn"].ToString() : "";
            }
        }

        /* ЗАГРУЗКА ДАННЫХ СОТРУДНИКА */
        private void oldUser()
        {
            string query = "SELECT * FROM fio " +
                " LEFT JOIN sp_firms ON sp_firms.kod = fio.mast" +
                " LEFT JOIN spisok1c ON TRY_CAST(REPLACE(spisok1c.tab1c, ' ', '') AS INT) = CAST(fio.tab1c AS INT) AND spisok1c.orgcode = sp_firms.frm_1c_inn" +
                " WHERE tab = " + customTextBoxTab.Text;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                //Создаем в памяти таблицу:
                System.Data.DataTable tableList = new System.Data.DataTable();
                //Добавляем ответ сервера в таблицу:
                dataAdapter.Fill(tableList);
                // Данные о редакторе:
                DateTime? editDate = tableList.Rows[0]["edit_date"] == DBNull.Value ? null : (DateTime?)tableList.Rows[0]["edit_date"];
                string editKomp = tableList.Rows[0]["edit_komp"] == DBNull.Value ? "" : tableList.Rows[0]["edit_komp"].ToString().Trim();
                int tab = (int)tableList.Rows[0]["tab"];
                // если никто не редактирует или редактируем мы((дата пустая ИЛИ комп тот же) И табельный не 0)
                if ((!editDate.HasValue || string.IsNullOrEmpty(editKomp) || editKomp == Environment.MachineName) && tab != 0)
                {
                    // Записываем редактора:
                    UpdateEditor(tab, connection);
                    // Загружаем данные:
                    // ФИО:
                    customTextBoxFIO.Text = tableList.Rows[0]["fio"].ToString();
                    // Телефон и Даты:
                    customMaskedTextBoxTelSot.Text = tableList.Rows[0]["tel_s"].ToString();
                    dateRozd.Text = tableList.Rows[0]["bday"].ToString();
                    customMaskedTextBoxDatePriem.Text = tableList.Rows[0]["data_p"].ToString();
                    customMaskedTextBoxDateYvoln.Text = tableList.Rows[0]["datau"].ToString();
                    // Чекбоксы:
                    customCheckBoxSovm.Checked = tableList.Rows[0]["sovm"].ToString() == "1" ? true : false;
                    customCheckBoxSdel.Checked = tableList.Rows[0]["sdel"].ToString() == "1" ? true : false;
                    customCheckBoxITR.Checked = tableList.Rows[0]["itr"].ToString() == "1" ? true : false;
                    customCheckBoxDekret.Checked = tableList.Rows[0]["dekret"].ToString() == "1" ? true : false;
                    // Остальное:
                    customTextBoxOsnTab.Text = tableList.Rows[0]["tab_sovm"].ToString();
                    customTextBoxTab1с.Text = tableList.Rows[0]["tab1c"].ToString();
                    customTextBoxNved.Text = tableList.Rows[0]["ved"].ToString();
                    customTextBoxNTab.Text = tableList.Rows[0]["ftabn"].ToString();
                    customTextBoxTabN.Text = tableList.Rows[0]["fgrd"].ToString();
                    customTextBoxSorted.Text = tableList.Rows[0]["ftabnsort"].ToString();
                    customTextBoxMast.Text = tableList.Rows[0]["mast"].ToString();
                    customTextBoxPodr.Text = tableList.Rows[0]["okl"].ToString();
                    customTextBoxNTabVed.Text = tableList.Rows[0]["tab_new"].ToString();
                    customTextBoxPom.Text = tableList.Rows[0]["po"].ToString();
                    customTextBoxTelRab.Text = tableList.Rows[0]["tel_r"].ToString();
                    customTextBoxTelDom.Text = tableList.Rows[0]["tel_d"].ToString();
                    // inn
                    customTextBoxINN.Text = tableList.Rows[0]["inn"].ToString();

                    // комбобоксы:
                    customComboBoxDolj.Text = tableList.Rows[0]["rab"].ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(tableList.Rows[0]["mast"].ToString())) 
                        comboOneTableItems(customComboBoxOrg, 
                        $"SELECT TRIM(name) AS nameColumn FROM sp_firms WHERE sp_firms.kod = {tableList.Rows[0]["mast"].ToString()}",
                        connection, false);
                    if (!string.IsNullOrWhiteSpace(tableList.Rows[0]["f_fvr_kod"].ToString()))
                        comboOneTableItems(customComboBoxOb,   
                        $"SELECT TRIM(fvr_name) AS nameColumn FROM fio_vid_rabot WHERE fio_vid_rabot.fvr_kod =  {tableList.Rows[0]["f_fvr_kod"].ToString()}",
                        connection, false);
                    if (!string.IsNullOrWhiteSpace(tableList.Rows[0]["gr"].ToString()))
                        comboOneTableItems(customComboBoxPodr, 
                        $"SELECT TRIM(name) AS nameColumn FROM brig_object WHERE brig_object.gr =  '{tableList.Rows[0]["gr"].ToString()}'",
                        connection, false);
                    if (!string.IsNullOrWhiteSpace(tableList.Rows[0]["ftabn"].ToString()))
                        comboOneTableItems(customComboBoxNTab, 
                        $"SELECT TRIM(naimen) AS nameColumn FROM tab_n WHERE tab_n.tnid =  {tableList.Rows[0]["ftabn"].ToString()}",
                        connection, false);
                    if (!string.IsNullOrWhiteSpace(tableList.Rows[0]["podr_1c_id"].ToString()))
                        comboOneTableItems(customComboBoxPodr1c,
                        $"SELECT DISTINCT TRIM(podrname1c) AS nameColumn FROM spbrig WHERE spbrig.podrid1c = '{tableList.Rows[0]["podr_1c_id"].ToString()}'",
                        connection, false);

                }
                // Если кто то уже радактирует:
                else
                {
                    string eMessageTitle = "Занято другим компьютером";
                    string eMessageText = $"Пользователь {editKomp} уже редактирует этот профиль с {editDate}!";
                    DialogResult nAnswer = MessageBox.Show(eMessageText, eMessageTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
        }
        // Занесение данных редактора в БД
        private bool UpdateEditor(int xTab, SqlConnection connection)
        {
             connection.Open();
             string updateQuery = $"UPDATE FIO SET edit_komp = '{Environment.MachineName}', edit_date = GETDATE() WHERE tab = {xTab}";
             using (SqlCommand command = new SqlCommand(updateQuery, connection))
             {
                    command.ExecuteNonQuery();
                    try
                    {
                        if (command.ExecuteNonQuery() < 0)
                        {
                            string errorMessage = "Ошибка обновления записи";
                            MessageBox.Show(errorMessage);
                            this.DialogResult = DialogResult.Cancel;
                            this.Close();
                            return false;
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка выполнения запроса: {ex.Message}");
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                        return false;
                    }
             }
        }
        private void newUser()
        {
        }
        //Проверка на заполенность данных
        string proverkaZap(TextBox FIO, MaskedTextBox DatePriem, ComboBox Org, ComboBox Dolj)
        {
            if (FIO.Text == "")
                return "Заполните поле 'ФИО'!";
            if (DatePriem.Text == "")
                return "Заполните поле 'Дата приема'!";
            if (Org.Text == "")
                return "Выберите Организацию!";
            if (Dolj.Text == "")
                return "Выберите Должность!";
            return "OK";
        }
        /* КНОПКА СОХРАНЕНИЯ */
        private void customOkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string proverka = proverkaZap(customTextBoxFIO, customMaskedTextBoxDatePriem, customComboBoxOrg, customComboBoxDolj);
                if (proverka == "OK")
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string queryFio;
                        if (this.Text == "Редактирование сотрудника")
                        {
                            // Запрос на обновление данных существующего сотрудника
                            queryFio = "UPDATE fio SET " +
                                "fio = @fio, " +
                                "tel_s = REPLACE(@tel_s,' ',''), " +
                                "bday = @bday, " +
                                "data_p = @data_p, " +
                                "datau = @datau, " +
                                "rab = @rab, " +
                                "mast = CASE WHEN @OrgName IS NULL THEN NULL ELSE (SELECT sf.kod FROM sp_firms sf WHERE sf.name = @OrgName) END, " +
                                "f_fvr_kod = CASE WHEN @ObName IS NULL THEN NULL ELSE (SELECT fvr.fvr_kod FROM fio_vid_rabot fvr WHERE fvr.fvr_name = @ObName) END, " +
                                 "gr = CASE WHEN @PodrName IS NULL THEN NULL ELSE (SELECT bo.gr FROM brig_object bo WHERE bo.name = @PodrName) END, " +
                                "ftabn = CASE WHEN @NTabName IS NULL THEN NULL ELSE (SELECT tn.tnid FROM tab_n tn WHERE tn.naimen = @NTabName) END, " +
                                "podr_1c_id = CASE WHEN @Podr1cName IS NULL THEN NULL ELSE (SELECT sb.podrid1c FROM spbrig sb WHERE sb.podrname1c = @Podr1cName) END, " +
                                 "tab_sovm = @tab_sovm, " +
                                 "tab1c = @tab1c, " +
                                "ved = @ved, " +
                                "fgrd = @fgrd, " +
                                "ftabnsort = @ftabnsort, " +
                                "okl = @okl, " +
                                "tab_new = @tab_new, " +
                                "po = @po, " +
                                 "tel_r = @tel_r, " +
                                 "tel_d = @tel_d, " +
                                "sovm = @sovm, " +
                                "sdel = @sdel, " +
                                 "itr = @itr, " +
                                "dekret = @dekret " +
                                 " WHERE tab = @tab";
                        }
                        else
                        {
                            // Запрос на добавление нового сотрудника:
                            queryFio = "INSERT INTO fio (fio, tel_s, bday, data_p, datau, rab, mast, f_fvr_kod, gr, ftabn, podr_1c_id, tab_sovm, tab1c," +
                                        " ved, fgrd, ftabnsort, okl, tab_new, po, tel_r, tel_d, sovm, sdel, itr, dekret,tab) " +
                                        "VALUES (@fio, @tel_s, @bday, @data_p, @datau, @rab, " +
                                        "CASE WHEN @OrgName IS NULL THEN NULL ELSE (SELECT sf.kod FROM sp_firms sf WHERE sf.name = @OrgName) END, " +
                                        "CASE WHEN @ObName IS NULL THEN NULL ELSE (SELECT fvr.fvr_kod FROM fio_vid_rabot fvr WHERE fvr.fvr_name = @ObName) END, " +
                                        "CASE WHEN @PodrName IS NULL THEN NULL ELSE (SELECT bo.gr FROM brig_object bo WHERE bo.name = @PodrName) END, " +
                                        "CASE WHEN @NTabName IS NULL THEN NULL ELSE (SELECT tn.tnid FROM tab_n tn WHERE tn.naimen = @NTabName) END, " +
                                        "CASE WHEN @Podr1cName IS NULL THEN NULL ELSE (SELECT sb.podrid1c FROM spbrig sb WHERE sb.podrname1c = @Podr1cName) END, " +
                                        "@tab_sovm, @tab1c, @ved, @fgrd, @ftabnsort, @okl, @tab_new, @po, @tel_r, @tel_d, @sovm, @sdel, @itr, @dekret, " +
                                        "(SELECT MAX(tab)+1 FROM fio))";
                        }
                        //определение данных в запрос и его выполенине
                        using (SqlCommand command = new SqlCommand(queryFio, connection))
                        {
                                command.Parameters.AddWithValue("@fio", customTextBoxFIO.Text); 
                                command.Parameters.AddWithValue("@tel_s", customMaskedTextBoxTelSot.Text.Replace(" ", ""));
                                command.Parameters.AddWithValue("@bday", dateRozd.Value);
                                command.Parameters.AddWithValue("@data_p", string.IsNullOrWhiteSpace(customMaskedTextBoxDatePriem.Text) ? (object)DBNull.Value : DateTime.ParseExact(customMaskedTextBoxDatePriem.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture));
                                command.Parameters.AddWithValue("@datau", string.IsNullOrWhiteSpace(customMaskedTextBoxDateYvoln.Text) ? (object)DBNull.Value : (customMaskedTextBoxDateYvoln.Text == "  .  ." ? (object)DBNull.Value : DateTime.ParseExact(customMaskedTextBoxDateYvoln.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture)));
                                command.Parameters.AddWithValue("@rab", string.IsNullOrWhiteSpace(customComboBoxDolj.Text) ? (object)DBNull.Value : customComboBoxDolj.Text.Trim());
                                command.Parameters.AddWithValue("@OrgName", string.IsNullOrWhiteSpace(customComboBoxOrg.Text) ? (object)DBNull.Value : customComboBoxOrg.Text.Trim());
                                command.Parameters.AddWithValue("@ObName", string.IsNullOrWhiteSpace(customComboBoxOb.Text) ? (object)DBNull.Value : customComboBoxOb.Text.Trim());
                                command.Parameters.AddWithValue("@PodrName", string.IsNullOrWhiteSpace(customComboBoxPodr.Text) ? (object)DBNull.Value : customComboBoxPodr.Text.Trim());
                                command.Parameters.AddWithValue("@NTabName", string.IsNullOrWhiteSpace(customComboBoxNTab.Text) ? (object)DBNull.Value : customComboBoxNTab.Text.Trim());
                                command.Parameters.AddWithValue("@Podr1cName", string.IsNullOrWhiteSpace(customComboBoxPodr1c.Text) ? (object)DBNull.Value : customComboBoxPodr1c.Text.Trim());
                                command.Parameters.AddWithValue("@tab_sovm", string.IsNullOrWhiteSpace(customTextBoxOsnTab.Text) ? (object)DBNull.Value : customTextBoxOsnTab.Text);
                                command.Parameters.AddWithValue("@tab1c", string.IsNullOrWhiteSpace(customTextBoxTab1с.Text) ? (object)DBNull.Value : customTextBoxTab1с.Text);
                                command.Parameters.AddWithValue("@ved", string.IsNullOrWhiteSpace(customTextBoxNved.Text) ? (object)DBNull.Value : customTextBoxNved.Text);
                                command.Parameters.AddWithValue("@fgrd", string.IsNullOrWhiteSpace(customTextBoxTabN.Text) ? (object)DBNull.Value : customTextBoxTabN.Text);
                                command.Parameters.AddWithValue("@ftabnsort", string.IsNullOrWhiteSpace(customTextBoxSorted.Text) ? (object)DBNull.Value : customTextBoxSorted.Text);
                                command.Parameters.AddWithValue("@okl", string.IsNullOrWhiteSpace(customTextBoxPodr.Text) ? (object)DBNull.Value : customTextBoxPodr.Text);
                                command.Parameters.AddWithValue("@tab_new", string.IsNullOrWhiteSpace(customTextBoxNTabVed.Text) ? (object)DBNull.Value : customTextBoxNTabVed.Text);
                                command.Parameters.AddWithValue("@po", string.IsNullOrWhiteSpace(customTextBoxPom.Text) ? (object)DBNull.Value : customTextBoxPom.Text);
                                command.Parameters.AddWithValue("@tel_r", string.IsNullOrWhiteSpace(customTextBoxTelRab.Text) ? (object)DBNull.Value : customTextBoxTelRab.Text);
                                command.Parameters.AddWithValue("@tel_d", string.IsNullOrWhiteSpace(customTextBoxTelDom.Text) ? (object)DBNull.Value : customTextBoxTelDom.Text);
                                command.Parameters.AddWithValue("@sovm", customCheckBoxSovm.Checked);
                                command.Parameters.AddWithValue("@sdel", customCheckBoxSdel.Checked);
                                command.Parameters.AddWithValue("@itr", customCheckBoxITR.Checked);
                                command.Parameters.AddWithValue("@dekret", customCheckBoxDekret.Checked);
                                command.Parameters.AddWithValue("@tab", customTextBoxTab.Text == "АВТО" ? (object)DBNull.Value : customTextBoxTab.Text);

                                connection.Open();
                                command.ExecuteNonQuery();
                                connection.Close();
                        }
                        queryFio = $"UPDATE fio SET edit_komp = NULL,edit_date = NULL WHERE tab = {customTextBoxTab.Text}";
                        if (customTextBoxTab.Text != "АВТО")
                            using (SqlCommand commandUPDATE = new SqlCommand(queryFio, connection))
                            {
                                connection.Open();
                                commandUPDATE.ExecuteNonQuery();
                                connection.Close();
                            }
                    }
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else MessageBox.Show(proverka, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException sqlEx)
            {
                Debug.WriteLine($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error starting listener: {ex.Message}");
            }
        }

        /* ЧИСТКА КОМБОБОКСОВ */
        private void customButtonXOrg_Click(object sender, EventArgs e)
        {
            clearComboBox(customComboBoxOrg);
        }

        private void customButtonXDolj_Click(object sender, EventArgs e)
        {
            clearComboBox(customComboBoxDolj);
        }

        private void customButtonXOb_Click(object sender, EventArgs e)
        {
            clearComboBox(customComboBoxOb);
        }

        private void customButtonXPodr_Click(object sender, EventArgs e)
        {
            clearComboBox(customComboBoxPodr);
        }

        private void customButtonXNtab_Click(object sender, EventArgs e)
        {
            clearComboBox(customComboBoxNTab);
        }

        private void customButtonXPodr1c_Click(object sender, EventArgs e)
        {
            clearComboBox(customComboBoxPodr1c);
        }
        private void clearComboBox(System.Windows.Forms.ComboBox comboBox)
        {
            comboBox.SelectedItem = null;
        }

        private void customButtonNowPriem_Click(object sender, EventArgs e)
        {
            customMaskedTextBoxDatePriem.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        private void customButtonNowYvol_Click(object sender, EventArgs e)
        {
            customMaskedTextBoxDateYvoln.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        /* АВТОЗАПОЛНЕНИЕ ТЕКСТБОКСОВ ПОСЛЕ ВЫБОРКИ В КОМБОБОКСАХ */
        private void customComboBoxNTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection connectionCombo = new SqlConnection(connectionString))
            {
                string query = $"SELECT tnid AS nameColumn FROM tab_n WHERE tab_n.naimen =  '{customComboBoxNTab.Text}'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connectionCombo);
                System.Data.DataTable tableList = new System.Data.DataTable();
                dataAdapter.Fill(tableList);
                customTextBoxNTab.Text = tableList.Rows[0]["nameColumn"].ToString();
            }
        }

        private void customComboBoxOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection connectionOrg = new SqlConnection(connectionString))
            {
                string query = $"SELECT sp_firms.kod AS nameColumn FROM sp_firms WHERE sp_firms.name =  '{customComboBoxOrg.Text}'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connectionOrg);
                System.Data.DataTable tableList = new System.Data.DataTable();
                dataAdapter.Fill(tableList);
                customTextBoxMast.Text = tableList.Rows[0]["nameColumn"].ToString();
            }
        }

        /* ПРОВЕРКА ДАТ НА КОРРЕКТНОСТЬ */
        private void customMaskedTextBoxDatePriem_TextChanged(object sender, EventArgs e)
        {
            // Преобразуем текст в DateTime, если это возможно.
            if (DateTime.TryParseExact(customMaskedTextBoxDatePriem.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDatePriem))
            {
                DateTime currentDate = DateTime.Now.Date; // берем только дату без времени
                if (parsedDatePriem > currentDate)
                {
                    MessageBox.Show("Эта дата еще не наступила!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    TimeSpan age = parsedDatePriem - dateRozd.Value;
                    if (age.TotalDays <= 14 * 365) // проверка на 14 лет
                    {
                        MessageBox.Show("На дату приема работнику нет 14 лет!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (age.TotalDays >= 80 * 365) // проверка на 80 лет
                    {
                        MessageBox.Show("На дату приема работнику больше 80 лет!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else if (!string.IsNullOrWhiteSpace(customMaskedTextBoxDatePriem.Text))
            {
                //MessageBox.Show("Некорректный формат даты!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void customMaskedTextBoxDateYvoln_TextChanged(object sender, EventArgs e)
        {
            // Преобразуем текст в DateTime, если это возможно.
            if (DateTime.TryParseExact(customMaskedTextBoxDateYvoln.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDateYvoln))
            {
                DateTime currentDate = DateTime.Now.Date; // берем только дату без времени
                if (parsedDateYvoln > currentDate)
                {
                    MessageBox.Show("Эта дата еще не наступила!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (DateTime.TryParseExact(customMaskedTextBoxDatePriem.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDatePriem))
                {
                    if (parsedDateYvoln < parsedDatePriem)
                        MessageBox.Show("Дата увольнения меньше даты приема!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (!string.IsNullOrWhiteSpace(customMaskedTextBoxDateYvoln.Text))
            {
                //MessageBox.Show("Некорректный формат даты!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* ЗАКРЫТИЕ И ОЧИСТКА ДАННЫХ РЕДАКТОРА */
        private void customCancelButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void editFio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)  // проверяем, был ли диалог закрыт по нажатию ОК
            {
                DialogResult result = MessageBox.Show("Вы уверены, что хотите отменить?",
                                                    "Подтверждение",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (customTextBoxTab.Text != "АВТО")
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            string queryFio = $"UPDATE fio SET edit_komp = NULL,edit_date = NULL WHERE tab = {customTextBoxTab.Text}";
                            using (SqlCommand command = new SqlCommand(queryFio, connection))
                            {
                                connection.Open();
                                command.ExecuteNonQuery();

                            }
                        }
                    }
                    this.DialogResult = DialogResult.Cancel;
                }
                else
                {
                    e.Cancel = true; // отменяем закрытие
                }
            }
        }
    }
}
