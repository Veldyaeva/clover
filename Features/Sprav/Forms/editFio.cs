using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.CodeParser;
using DevExpress.DataAccess.Native.Json;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.Internal.WinApi;
using DevExpress.Pdf.Native;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraBars;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Customization;
using DevExpress.XtraRichEdit.Import.Html;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;
using static SewingProduction.form.EditFioDataService;

namespace SewingProduction.form
{
    public partial class editFio : CustomForm
    {
        private readonly EditFioDataService _editFioDataService;
        DatabaseHelper dbHelper = new DatabaseHelper("ace");
        private Person _person;
        private ToolTip toolTip = new ToolTip();
        bool isBusy = false;

        public editFio(UserClass user, string idFIO, string openType) : base(user)
        {
            InitializeComponent();
            _editFioDataService = new EditFioDataService(dbHelper);
            _person = new Person();
            ThemeManager.UpdateTheme(this);
            //Имя формы:
            this.Text = openType;
            customTextBoxTab.Text = idFIO;
            toolTipButton();
        }
        public editFio()
        {
            InitializeComponent();
        }

        private void editFio_Load(object sender, EventArgs e)
        {
            LoadAllComboBox();
            if (this.Text == "Редактирование сотрудника")
                oldUser();
            else
                newUser();
        }
        public void LoadAllComboBox()
        {
            _editFioDataService.GetNameFromSp_firms(customComboBoxOrg);
            _editFioDataService.GetRabFromRab(customComboBoxDolj);
            _editFioDataService.GetFvr_nameFromFio_vid_rabot(customComboBoxOb);
            _editFioDataService.GetNameFromBrig_object(customComboBoxPodr);
            _editFioDataService.GetNaimenFromTab_n(customComboBoxNTab);
            _editFioDataService.GetNameFromBrig_ved(customComboBoxNved);
            _editFioDataService.GetPodrname1cFromSpbrig(customComboBoxPodr1c);
            _editFioDataService.GetNameFromPodr1C(customComboBox1Cpodr);
            _editFioDataService.GetNameFromDolg1C(customComboBox1Cdolg);
        }

        //Редактирование сотрудника
        private void oldUser()
        {
            //если не получается загрузить данные по сотруднику:
            if (!_person.LoadData(customTextBoxTab.Text, dbHelper))
            {
                isBusy = true;
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            //если данные загрузились
            else 
            {
                GetDataPerson();
            }
        }
        /* ЗАГРУЗКА ДАННЫХ СОТРУДНИКА в поля текстбоксов и комбобоксов*/
        public void GetDataPerson()
        {   
            //Становимся редакторами:
            _person.UpdateEditor();

            customTextBoxTab.Text = _person.tab.ToString();

            dateRozd.Text = _person.bday ;
            customTextBoxFIO.Text = _person.fio;
            customMaskedTextBoxTelSot.Text = _person.tel_s;

            // Обработка DateTime?
            customMaskedTextBoxDatePriem.Text = _person.data_p;
            customMaskedTextBoxDateYvoln.Text = _person.datau;

            customComboBoxDolj.Text = _person.rab;
            customComboBoxOrg.Text = _person.mast_sp_firms;
            customComboBoxOb.Text = _person.f_fvr_kod_fio_vid_rabot;
            customComboBoxPodr.Text = _person.gr_brig_object;
            customComboBoxNved.Text = _person.ved_brig_ved;
            customComboBoxNTab.Text = _person.ftabn_tab_n;
            customComboBoxPodr1c.Text = _person.podr_1c_id_spbrig;

            customTextBoxOsnTab.Text = _person.tab_sovm.ToString();
            customTextBoxTab1с.Text = _person.tab1c;
            customTextBoxNved.Text = _person.ved.ToString();
            customTextBoxNTab.Text = _person.ftabn.ToString();
            customTextBoxTabN.Text = _person.fgrd.ToString();
            customTextBoxSorted.Text = _person.ftabnsort.ToString();

            customTextBoxMast.Text = _person.mast.ToString();
            customTextBoxPodr.Text = _person.okl.ToString();
            customTextBoxNTabVed.Text = _person.tab_new.ToString();
            //customTextBoxPom.Text = _person.po;
            customTextBoxTelRab.Text = _person.tel_r;
            customTextBoxTelDom.Text = _person.tel_d;

            customCheckBoxSovm.Checked = _person.sovm == 1;
            customCheckBoxSdel.Checked = _person.sdel == 1;
            customCheckBoxITR.Checked = _person.itr == 1;
            customCheckBoxDekret.Checked = _person.dekret == 1;
            // inn
            customTextBoxINN.Text = _person.inn;
            //1C:
            customComboBox1Cpodr.Text = _person.podr_name_podr1C;
            customTextBox1CpodrID.Text = _person.podr_id_podr1C;
            customTextBox1CpodrINN.Text = _person.podr_inn_podr1C;
            customComboBox1Cdolg.Text = _person.dolg_name_dolg1C;
            customTextBox1CdolgID.Text = _person.dolg_d_id;
            customTextBox1CdolgINN.Text = _person.dolg_inn_dolg1C;
        }
        //Добавление сотрудника
        private void newUser()
        {
            _person.rab = "";
            _person.mast = "";
            _person.podr_1c_id = "";
            _person.podr_pd_uid = "";
            _person.tab1c = "";
        }
        //Проверка на заполенность данных
        string proverkaZap()
        {
            if (customTextBoxFIO.Text == "")
                return "Заполните поле 'ФИО'!";
            if (customMaskedTextBoxDatePriem.Text == "  .  .")
                return "Заполните поле 'Дата приема'!";
            if (customComboBoxOrg.Text == "")
                return "Выберите Организацию!";
            if (customComboBoxDolj.Text == "")
                return "Выберите Должность!";
            return "OK";
        }
        /* КНОПКА СОХРАНЕНИЯ */
        private void customOkButton1_Click(object sender, EventArgs e)
        {
            if (proverkaZap() == "OK")
            {
                SetDataPerson();
                if (this.Text == "Редактирование сотрудника")
                {
                    _person.UpdatePerson(_person); 
                }
                else 
                {
                    _person.InsertPerson(_person, dbHelper);
                }
                _person.ClearEditor();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else MessageBox.Show(proverkaZap(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// Процедура добавления данных в обьект
        /// </summary>
        public void SetDataPerson()
        {
            _person.fio = customTextBoxFIO.Text;
            _person.tel_s = customMaskedTextBoxTelSot.Text.Replace(" ", "");

            _person.bday = dateRozd.Value.ToString();
            _person.data_p = customMaskedTextBoxDatePriem.Text;
            _person.datau = customMaskedTextBoxDateYvoln.Text == "  .  ." ? "01.01.1900" : customMaskedTextBoxDateYvoln.Text;

            _person.tab_sovm = int.Parse(customTextBoxOsnTab.Text);
            _person.tab1c = customTextBoxTab1с.Text;
            _person.ved = int.Parse(customTextBoxNved.Text);
            _person.fgrd = int.Parse(customTextBoxTabN.Text);
            _person.ftabnsort = int.Parse(customTextBoxSorted.Text);
            _person.okl = string.IsNullOrEmpty(customTextBoxPodr.Text) ? 0 : int.Parse(customTextBoxPodr.Text);
            _person.tab_new = int.Parse(customTextBoxNTabVed.Text);
            _person.tel_r = customTextBoxTelRab.Text;
            _person.tel_d = customTextBoxTelDom.Text;

            _person.sovm = customCheckBoxSovm.Checked ? 1 : 0;
            _person.sdel = customCheckBoxSdel.Checked ? 1 : 0;
            _person.itr = customCheckBoxITR.Checked ? 1 : 0;
            _person.dekret = customCheckBoxDekret.Checked ? 1 : 0;

            _person.edit_komp = System.Environment.MachineName;
            _person.tab = customTextBoxTab.Text == "АВТО" ? 0 : int.Parse(customTextBoxTab.Text);

        }

        #region ЧИСТКА КОМБОБОКСОV
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

        private void customButtonXNved_Click(object sender, EventArgs e)
        {
            clearComboBox(customComboBoxNved);
        }
        private void customButton1Cpodr_Click(object sender, EventArgs e)
        {
            clearComboBox(customComboBox1Cpodr);

        }
        private void customButton1Cdolg_Click(object sender, EventArgs e)
        {
            clearComboBox(customComboBox1Cdolg);

        }
        private void clearComboBox(System.Windows.Forms.ComboBox comboBox)
        {
            comboBox.SelectedItem = null;
        }
        #endregion
        private void customButtonNowPriem_Click(object sender, EventArgs e)
        {
            customMaskedTextBoxDatePriem.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        private void customButtonNowYvol_Click(object sender, EventArgs e)
        {
            customMaskedTextBoxDateYvoln.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        #region АВТОЗАПОЛНЕНИЕ ТЕКСТБОКСОВ ПОСЛЕ ВЫБОРКИ В КОМБОБОКСАХ 
        private void customComboBoxOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            _person.mast_sp_firms = string.IsNullOrWhiteSpace(customComboBoxOrg.Text) ? "" : customComboBoxOrg.Text?.Trim();
            _person.mast = _editFioDataService.GetKodFromSp_firms(_person.mast_sp_firms);
            customTextBoxMast.Text = _person.mast;
        }
        private void customComboBoxDolj_SelectedIndexChanged(object sender, EventArgs e)
        {
            _person.rab = string.IsNullOrWhiteSpace(customComboBoxDolj.Text) ? "" : customComboBoxDolj.Text.Trim();
        }
        private void customComboBoxOb_SelectedIndexChanged(object sender, EventArgs e)
        {
            _person.f_fvr_kod_fio_vid_rabot = string.IsNullOrWhiteSpace(customComboBoxOb.Text) ? "" : customComboBoxOb.Text?.Trim();
            _person.f_fvr_kod = int.Parse(_editFioDataService.GetKodFromFio_vid_rabot(_person.f_fvr_kod_fio_vid_rabot));
        }
        private void customComboBoxPodr_SelectedIndexChanged(object sender, EventArgs e)
        {
            _person.gr_brig_object = string.IsNullOrWhiteSpace(customComboBoxPodr.Text) ? "" : customComboBoxPodr.Text?.Trim();
            _person.gr = int.Parse(_editFioDataService.GetGrFromBrig_object(_person.gr_brig_object));
        }
        private void customComboBoxNved_SelectedIndexChanged(object sender, EventArgs e)
        {
            customTextBoxNved.Text = _editFioDataService.GetVdidFromBrig_ved(customComboBoxNved.Text);
            _person.ved = int.Parse(customTextBoxNved.Text);
        }

        private void customComboBoxNTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            _person.ftabn_tab_n = customComboBoxNTab.Text.Trim();
            customTextBoxNTab.Text = _editFioDataService.GetTnidFromTab_n(customComboBoxNTab.Text);
            _person.ftabn = int.Parse(customTextBoxNTab.Text);
        }
        private void customComboBoxPodr1c_SelectedIndexChanged(object sender, EventArgs e)
        {
            _person.podr_1c_id_spbrig = customComboBoxPodr1c.Text;
            _person.podr_1c_id = _editFioDataService.GetPodr_1c_idFromSpbrig(_person.podr_1c_id_spbrig);
        }

        private void customComboBox1Cpodr_SelectedIndexChanged(object sender, EventArgs e)
        {
            _person.podr_name_podr1C = customComboBox1Cpodr.Text;
            _person.podr_pd_uid = _editFioDataService.GetPd_uidFromPodr1C(_person.podr_name_podr1C);
            _person.podr_id_podr1C = _editFioDataService.GetIdFromPodr1C(_person.podr_pd_uid);
            _person.podr_inn_podr1C = _editFioDataService.GetInnFromPodr1C(_person.podr_pd_uid);
            customTextBox1CpodrID.Text = _person.podr_id_podr1C;
            customTextBox1CpodrINN.Text = _person.podr_inn_podr1C;
        }

        private void customComboBox1Cdolg_SelectedIndexChanged(object sender, EventArgs e)
        {
            _person.dolg_name_dolg1C = customComboBox1Cdolg.Text;
            _person.dolg_d_id = _editFioDataService.GetD_idFromDolg1C(_person.dolg_name_dolg1C);
            _person.dolg_inn_dolg1C = _editFioDataService.GetInnFromDolg1C(_person.dolg_d_id);
            customTextBox1CdolgID.Text = _person.dolg_d_id;
            customTextBox1CdolgINN.Text = _person.dolg_inn_dolg1C;
        }
        #endregion
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
            if (this.DialogResult != DialogResult.OK && !isBusy)  // проверяем, был ли диалог закрыт по нажатию ОК
            {
                DialogResult result = MessageBox.Show("Вы уверены, что хотите отменить?",
                                                    "Подтверждение",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (customTextBoxTab.Text != "АВТО")
                    {
                        _person.ClearEditor();
                    }
                    this.DialogResult = DialogResult.Cancel;
                }
                else
                {
                    e.Cancel = true; // отменяем закрытие
                }
            }
        }
        /// <summary>
        /// Подсказки при наведении на кнопки
        /// </summary>
        private void toolTipButton()
        {
            toolTip.AutoPopDelay = 5000;     // Подсказка исчезнет через 5 секунд.
            toolTip.InitialDelay = 500;      // Подсказка появится через 0.5 секунды.
            toolTip.ReshowDelay = 100;       // Подсказка появится повторно при движении мыши через 0.1 секунду.
            //toolTip.IsBalloon = true;      // Показывать подсказку в виде воздушного шара.
            //toolTip.ToolTipIcon = ToolTipIcon.Info; // Показывать иконку информации.
            //toolTip.ToolTipTitle = "Подсказка";  // Заголовок подсказки.

            toolTip.SetToolTip(customButtonNowPriem, "Сегодня");
            toolTip.SetToolTip(customButtonNowYvol, "Сегодня");
            toolTip.SetToolTip(customButtonXOrg, "Очистить");
            toolTip.SetToolTip(customButtonXDolj, "Очистить");
            toolTip.SetToolTip(customButtonXOb, "Очистить");
            toolTip.SetToolTip(customButtonXPodr, "Очистить");
            toolTip.SetToolTip(customButtonXNved, "Очистить");
            toolTip.SetToolTip(customButtonXNtab, "Очистить");
            toolTip.SetToolTip(customButton1Cpodr, "Очистить");
            toolTip.SetToolTip(customButton1Cdolg, "Очистить");
            toolTip.SetToolTip(customOkButton1, "Сохранить и выйти");
            toolTip.SetToolTip(customCancelButton1, "Выйти не сохранив");
        }

    }
    public class EditFioDataService
    {
        private readonly DatabaseHelper _dbHelper;
        public EditFioDataService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        /// <summary>
        /// Функция для отображения значения в комбобоксе
        /// </summary>
        /// <param name="query">текст запроса</param>
        public string SetComboOneTableItems(string query)
        {
            System.Data.DataTable tableList = new System.Data.DataTable();
            tableList = _dbHelper.ExecuteQuery(query);
            return tableList.Rows.Count > 0 ? tableList.Rows[0]["nameColumn"].ToString() : "";
        }
        /// <summary>
        /// Функция для заполнения значений в комбобоксе
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="query"></param>
        public void SetComboAllTableItems(System.Windows.Forms.ComboBox comboBox, string query)
        {
            System.Data.DataTable tableList = new System.Data.DataTable();
            tableList = _dbHelper.ExecuteQuery(query);
            comboBox.Items.Clear();
           //Загрузка в комбобокс:
           foreach (DataRow row in tableList.Rows)
           {
               comboBox.Items.Add(row[0].ToString());
           }
        }
        public string GetNameFromSp_firms(string kod)
        {
            string query = $"SELECT TRIM(name) AS nameColumn FROM sp_firms WHERE sp_firms.kod = {kod}";
            return SetComboOneTableItems(query);
        }
        public string GetFvr_nameFromFio_vid_rabot(int f_fvr_kod)
        {
            string query = $"SELECT TRIM(fvr_name) AS nameColumn FROM fio_vid_rabot WHERE fio_vid_rabot.fvr_kod = {f_fvr_kod}";
            return SetComboOneTableItems(query);
        }
        public string GetNameFromBrig_object(int gr)
        {
            string query = $"SELECT TRIM(name) AS nameColumn FROM brig_object WHERE brig_object.gr = '{gr}'";
            return SetComboOneTableItems(query);
        }
        public string GetNaimenFromTab_n(int ftabn)
        {
            string query = $"SELECT TRIM(naimen) AS nameColumn FROM tab_n WHERE tab_n.tnid =  {ftabn}";
            return SetComboOneTableItems(query);
        }
        public string GetNameFromBrig_ved(int ved)
        {
            string query = $"SELECT TRIM(name) AS nameColumn FROM brig_ved WHERE brig_ved.vdID =  {ved}";
            return SetComboOneTableItems(query);
        }
        public string GetPodrname1cFromSpbrig(string podr_1c_id)
        {
            string query = $"SELECT DISTINCT TRIM(podrname1c) AS nameColumn FROM spbrig WHERE spbrig.podrid1c = '{podr_1c_id}'";
            return SetComboOneTableItems(query);
        }
        public string GetNameFromPodr1C(string podr_pd_uid)
        {
            string query = $"SELECT DISTINCT TRIM(name) AS nameColumn FROM podr1C WHERE podr1C.pd_uid = '{podr_pd_uid}'";
            return SetComboOneTableItems(query);
        }
        public string GetIdFromPodr1C(string podr_pd_uid)
        {
            string query = $"SELECT id AS nameColumn FROM podr1C WHERE podr1C.pd_uid = '{podr_pd_uid}'";
            return SetComboOneTableItems(query);
        }
        public string GetInnFromPodr1C(string podr_pd_uid)
        {
            string query = $"SELECT inn AS nameColumn FROM podr1C WHERE podr1C.pd_uid = '{podr_pd_uid}'";
            return SetComboOneTableItems(query);
        }
        public string GetNameFromDolg1C(string dolg_d_id)
        {
            string query = $"SELECT DISTINCT TRIM(name) AS nameColumn FROM dolg1C WHERE dolg1C.d_id = {dolg_d_id}";
            return SetComboOneTableItems(query);
        }
        public string GetInnFromDolg1C(string dolg_d_id)
        {
            string query = $"SELECT DISTINCT inn AS nameColumn FROM dolg1C WHERE dolg1C.d_id = {dolg_d_id}";
            return SetComboOneTableItems(query);
        }
        public void GetNameFromSp_firms(ComboBox comboBox)
        {
            string query = $"SELECT TRIM(name) FROM sp_firms ORDER BY kod";
            SetComboAllTableItems(comboBox, query);
        }
        public void GetRabFromRab(ComboBox comboBox)
        {
            string query = $"SELECT TRIM(rab) FROM rab ORDER BY rab";
            SetComboAllTableItems(comboBox, query);
        }
        public void GetFvr_nameFromFio_vid_rabot(ComboBox comboBox)
        {
            string query = $"SELECT TRIM(fvr_name) FROM fio_vid_rabot ORDER BY fvr_kod";
            SetComboAllTableItems(comboBox, query);
        }
        public void GetNameFromBrig_object(ComboBox comboBox)
        {
            string query = $"SELECT TRIM(name) FROM brig_object ORDER BY gr";
            SetComboAllTableItems(comboBox, query);
        }
        public void GetNaimenFromTab_n(ComboBox comboBox)
        {
            string query = $"SELECT TRIM(naimen) FROM tab_n  ORDER BY naimen";
            SetComboAllTableItems(comboBox, query);
        }
        public void GetNameFromBrig_ved(ComboBox comboBox)
        {
            string query = $"SELECT TRIM(name) FROM brig_ved ORDER BY name";
            SetComboAllTableItems(comboBox, query);
        }
        public void GetPodrname1cFromSpbrig(ComboBox comboBox)
        {
            string query = $"SELECT DISTINCT TRIM(podrname1c) FROM spbrig";
            SetComboAllTableItems(comboBox, query);
        }
        public void GetNameFromPodr1C(ComboBox comboBox)
        {
            string query = $"SELECT DISTINCT TRIM(name) FROM podr1C";
            SetComboAllTableItems(comboBox, query);
        }
        public void GetNameFromDolg1C(ComboBox comboBox)
        {
            string query = "SELECT DISTINCT TRIM(name) FROM dolg1C";
            SetComboAllTableItems(comboBox, query);
        }
        public string GetTnidFromTab_n(string naimen)
        {
            string query = $"SELECT tnid AS nameColumn FROM tab_n WHERE tab_n.naimen =  '{naimen}'";
            return SetComboOneTableItems(query);
        }
        public string GetKodFromSp_firms(string name)
        {
            string query = $"SELECT sp_firms.kod AS nameColumn FROM sp_firms WHERE sp_firms.name =  '{name}'";
            return SetComboOneTableItems(query);
        }
        public string GetGrFromBrig_object(string name)
        {
            string query = $"SELECT bo.gr AS nameColumn FROM brig_object bo WHERE bo.name = '{name}'";
            return SetComboOneTableItems(query);
        }
        public string GetKodFromFio_vid_rabot(string name)
        {
            string query = $"SELECT fvr.fvr_kod AS nameColumn FROM fio_vid_rabot fvr WHERE fvr.fvr_name = '{name}'";
            return SetComboOneTableItems(query);
        }
        public string GetVdidFromBrig_ved(string name)
        {
            string query = $"SELECT brig_ved.vdID AS nameColumn FROM brig_ved WHERE brig_ved.name =  '{name}'";
            return SetComboOneTableItems(query);
        }
        public string GetPodr_1c_idFromSpbrig(string name)
        {
            string query = $"SELECT sb.podrid1c AS nameColumn FROM spbrig sb WHERE sb.podrname1c = '{name}'";
            return SetComboOneTableItems(query);
        }

        public string GetPd_uidFromPodr1C(string name)
        {
            string query = $"SELECT pd_uid AS nameColumn FROM podr1c WHERE podr1c.name =  '{name}'";
            return SetComboOneTableItems(query);
        }
        public string GetD_idFromDolg1C(string name)
        {
            string query = $"SELECT d_id AS nameColumn FROM dolg1c WHERE dolg1C.name = '{name}'";
            return SetComboOneTableItems(query);
        }
        public DataTable GetFioAndSpFirmsAndSpisok1c(string tab)
        {
            string query = @"SELECT * FROM fio 
                             LEFT JOIN sp_firms ON sp_firms.kod = fio.mast
                             LEFT JOIN spisok1c ON TRY_CAST(REPLACE(spisok1c.tab1c, ' ', '') AS INT) = CAST(fio.tab1c AS INT) AND spisok1c.orgcode = sp_firms.frm_1c_inn
                             WHERE tab = @tab";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@tab", tab } });
        }
        public void UpdateEditorFromFio(int tab)
        {
            string query = $"UPDATE FIO SET edit_komp = '{Environment.MachineName}', edit_date = GETDATE() WHERE tab = @tab";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@tab", tab } });
        }
        public void ClearEditorFromFio(int tab)
        {
            string query = $"UPDATE fio SET edit_komp = NULL,edit_date = NULL WHERE tab = @tab";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@tab", tab } });
        }
        public void UpdateFioPerson(Person _person)
        {
            string query = "UPDATE fio SET " +
                                "fio = @fio, " +
                                "tel_s = REPLACE(@tel_s,' ',''), " +
                                "bday =  @bday," +
                                "data_p = @data_p, " +
                                "datau = CASE WHEN YEAR(@datau) = 1900 THEN NULL ELSE @datau END," +
                                "rab = @rab, " +
                                "mast =@mast, " +
                                "f_fvr_kod = @f_fvr_kod , " +
                                "gr = @gr, " +
                                "ftabn = @ftabn, " +
                                "podr_1c_id = @podr_1c_id, " +
                                "podr_pd_uid = @podr_pd_uid, " +
                                "dolg_d_id = @dolg_d_id, " +
                                "tab_sovm = @tab_sovm, " +
                                "tab1c = @tab1c, " +
                                "ved = @ved, " +
                                "fgrd = @fgrd, " +
                                "ftabnsort = @ftabnsort, " +
                                "okl = @okl, " +
                                "tab_new = @tab_new, " +
                                "tel_r = @tel_r, " +
                                "tel_d = @tel_d, " +
                                "sovm = @sovm, " +
                                "sdel = @sdel, " +
                                "itr = @itr, " +
                                "dekret = @dekret " +
                                " WHERE tab = @tab";

            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> {  { "@fio", _person.fio } ,                       { "@tel_s", _person.tel_s } ,   
                                                                            { "@bday", _person.bday } ,                     { "@data_p", _person.data_p } ,      
                                                                            { "@datau", _person.datau } ,                   { "@rab", _person.rab } ,
                                                                            { "@okl", _person.okl } ,                       { "@tab_new", _person.tab_new } ,  
                                                                            { "@tel_r", _person.tel_r } ,                   { "@tel_d", _person.tel_d } ,       
                                                                            { "@mast", _person.mast } ,                     { "@f_fvr_kod", _person.f_fvr_kod } ,    
                                                                            { "@gr", _person.gr } ,                         { "@ftabn", _person.ftabn } ,
                                                                            { "@podr_1c_id", _person.podr_1c_id } ,         { "@podr_pd_uid", _person.podr_pd_uid } ,    
                                                                            { "@dolg_d_id", _person.dolg_d_id } ,           { "@tab_sovm", _person.tab_sovm } , 
                                                                            { "@tab1c", _person.tab1c } ,                   { "@ved", _person.ved } ,      
                                                                            { "@fgrd", _person.fgrd } ,                     { "@ftabnsort", _person.ftabnsort } ,    
                                                                            { "@sovm", _person.sovm } ,                     { "@sdel", _person.sdel } ,     
                                                                            { "@itr", _person.itr } ,                       { "@dekret", _person.dekret } ,
                                                                            { "@tab", _person.tab } });
        }
        public void InsertFioPerson(Person _person)
        {
            string query = "INSERT INTO fio (fio, tel_s, bday, data_p, datau, rab, mast, f_fvr_kod, gr, ftabn, podr_1c_id, podr_pd_uid, dolg_d_id," +
                                        " tab_sovm, tab1c, ved, fgrd, ftabnsort, okl, tab_new,  tel_r, tel_d, sovm, sdel, itr, dekret, komp_name, tab) " +
                                        "VALUES (@fio, @tel_s, @bday, @data_p, " +
                                        "CASE WHEN YEAR(@datau) = 1900 THEN NULL ELSE @datau END," +
                                        " @rab, @mast,  @f_fvr_kod, @gr, @ftabn, @podr_1c_id, @podr_pd_uid, @dolg_d_id, " +
                                        "@tab_sovm, @tab1c, @ved, @fgrd, @ftabnsort, @okl, @tab_new,  @tel_r, @tel_d, @sovm, @sdel, @itr, @dekret, @komp_name, " +
                                        "(SELECT MAX(tab)+1 FROM fio))";

            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> {  { "@fio", _person.fio } ,                       { "@tel_s", _person.tel_s } ,
                                                                            { "@bday", _person.bday } ,                     { "@data_p", _person.data_p } ,
                                                                            { "@datau", _person.datau } ,                   { "@rab", _person.rab } ,
                                                                            { "@okl", _person.okl } ,                       { "@tab_new", _person.tab_new } ,
                                                                            { "@tel_r", _person.tel_r } ,                   { "@tel_d", _person.tel_d } ,
                                                                            { "@mast", _person.mast } ,                     { "@f_fvr_kod", _person.f_fvr_kod } ,
                                                                            { "@gr", _person.gr } ,                         { "@ftabn", _person.ftabn } ,
                                                                            { "@podr_1c_id", _person.podr_1c_id } ,         { "@podr_pd_uid", _person.podr_pd_uid } ,
                                                                            { "@dolg_d_id", _person.dolg_d_id } ,           { "@tab_sovm", _person.tab_sovm } ,
                                                                            { "@tab1c", _person.tab1c } ,                   { "@ved", _person.ved } ,
                                                                            { "@fgrd", _person.fgrd } ,                     { "@ftabnsort", _person.ftabnsort } ,
                                                                            { "@sovm", _person.sovm } ,                     { "@sdel", _person.sdel } ,
                                                                            { "@itr", _person.itr } ,                       { "@dekret", _person.dekret } ,
                                                                            { "@komp_name", _person.edit_komp },            { "@tab", _person.tab } });
        }
    }

    public class Person
    {
        private EditFioDataService _editFioDataService;
        #region столбцы
            private int f_id { get; set; }
            public int tab { get; set; }
            public string fio { get; set; }
            public string rab { get; set; }
            public string mast { get; set; }
            public string datau { get; set; }
            public int okl { get; set; }
            public int tab_new { get; set; }
            public string tel_r { get; set; }
            public string tel_d { get; set; }
            public string tel_s { get; set; }
            public string bday { get; set; }
            public string data_p { get; set; }
            public int gr { get; set; }
            public int dekret { get; set; }
            public int f_fvr_kod { get; set; }
            public string tab1c { get; set; }
            public int sdel { get; set; }
            public int itr { get; set; }
            public string podr_1c_id { get; set; }
            public int sovm { get; set; }
            public int tab_sovm { get; set; }
            public int ftabn { get; set; }
            public int fgrd { get; set; }
            public int ftabnsort { get; set; }
            public int ved { get; set; }
            public string edit_komp { get; set; }
            public string edit_date { get; set; }
            public string inn { get; set; }
            //Получаемые по ИД:
            public string mast_sp_firms { get; set; }
            public string f_fvr_kod_fio_vid_rabot { get; set; }
            public string gr_brig_object { get; set; }
            public string ftabn_tab_n { get; set; }
            public string ved_brig_ved { get; set; }
            public string podr_1c_id_spbrig { get; set; }
            //Должности 1С:
            public string dolg_d_id { get; set; }
            public string dolg_name_dolg1C { get; set; }
            public string dolg_inn_dolg1C { get; set; }
            //Подразделения 1С:
            public string podr_pd_uid { get; set; }
            public string podr_id_podr1C { get; set; }
            public string podr_name_podr1C { get; set; }
            public string podr_inn_podr1C { get; set; }
        #endregion

        /// <summary>
        /// Загрузка данных сотрудника
        /// </summary>
        /// <param name="xTab">табельный</param>
        /// <param name="dbHelper"></param>
        public bool LoadData(string xTab, DatabaseHelper dbHelper)
        {
             _editFioDataService = new EditFioDataService(dbHelper);
             System.Data.DataTable tableList = _editFioDataService.GetFioAndSpFirmsAndSpisok1c(xTab);
             DataRow row = tableList.Rows[0];
             // Данные о редакторе:
             DateTime? editDate = row["edit_date"] == DBNull.Value ? null : (DateTime?)row["edit_date"];
             edit_date = editDate?.ToString() ?? "";
             edit_komp = row["edit_komp"]?.ToString().Trim() ?? "";
             tab = (int)row["tab"];
             // если никто не редактирует или редактируем мы((дата пустая ИЛИ комп тот же) И табельный не 0)
             if ((!editDate.HasValue || string.IsNullOrEmpty(edit_komp) || edit_komp == Environment.MachineName) && tab != 0)
             {
                _editFioDataService.UpdateEditorFromFio(tab);
                // ФИО:
                fio = row["fio"]?.ToString() ?? "";
                // Телефон и Даты:
                tel_s = row["tel_s"]?.ToString() ?? "";
                bday = row["bday"].ToString();
                data_p = row["data_p"]?.ToString() ?? "";
                datau = row["datau"]?.ToString() ?? "";
                // Чекбоксы:
                sovm = (int)row["sovm"];
                sdel = (int)row["sdel"];
                itr = (int)row["itr"];
                dekret = (int)row["dekret"];
                // Остальное:
                tab_sovm = (int)row["tab_sovm"];
                tab1c = row["tab1c"]?.ToString() ?? "";
                ved = (int)row["ved"];
                ftabn = (int)row["ftabn"];
                fgrd = (int)row["fgrd"];
                ftabnsort = (int)row["ftabnsort"];
                okl = row["okl"] == DBNull.Value ? 0 : Convert.ToInt32(row["okl"]);
                tab_new = (int)row["tab_new"];
                tel_r = row["tel_r"]?.ToString() ?? "";
                tel_d = row["tel_d"]?.ToString() ?? "";
                inn = row["inn"]?.ToString() ?? "";
                // для комбобоксов:
                rab = row["rab"]?.ToString().Trim() ?? "";
                mast = row["mast"].ToString();
                f_fvr_kod = DBNull.Value.Equals(row["f_fvr_kod"]) ? 0 : (int)row["f_fvr_kod"];
                gr = row["gr"] == null ? 0 : (int)row["gr"];
                ftabn = row["ftabn"] == null ? 0 : (int)row["ftabn"];
                ved = row["ved"] == null ? 0 : (int)row["ved"];
                podr_1c_id = row["podr_1c_id"].ToString();
                // комбобоксы:
                mast_sp_firms = _editFioDataService.GetNameFromSp_firms(mast);
                f_fvr_kod_fio_vid_rabot = _editFioDataService.GetFvr_nameFromFio_vid_rabot(f_fvr_kod);
                gr_brig_object = _editFioDataService.GetNameFromBrig_object(gr);
                ftabn_tab_n = _editFioDataService.GetNaimenFromTab_n(ftabn);
                ved_brig_ved = _editFioDataService.GetNameFromBrig_ved(ved);
                podr_1c_id_spbrig = _editFioDataService.GetPodrname1cFromSpbrig(podr_1c_id);
                // 1C
                dolg_d_id = row["dolg_d_id"].ToString();
                dolg_name_dolg1C = _editFioDataService.GetNameFromDolg1C(dolg_d_id);
                dolg_inn_dolg1C = _editFioDataService.GetInnFromDolg1C(dolg_d_id);
                podr_pd_uid = row["podr_pd_uid"].ToString();
                podr_id_podr1C = _editFioDataService.GetIdFromPodr1C(podr_pd_uid);
                podr_name_podr1C = _editFioDataService.GetNameFromPodr1C(podr_pd_uid); ;
                podr_inn_podr1C = _editFioDataService.GetInnFromPodr1C(podr_pd_uid); ;

                return true;
             }
             // Если кто то уже радактирует:
             else
             {
                 string eMessageTitle = "Занято другим компьютером";
                 string eMessageText = $"Пользователь {edit_komp} уже редактирует этот профиль с {editDate}!";
                 DialogResult nAnswer = MessageBox.Show(eMessageText, eMessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return false;
             }
        }
        /// <summary>
        /// Обновление редактора у "персоны" по табельному
        /// </summary>
        public void UpdateEditor()
        {
            _editFioDataService.UpdateEditorFromFio(tab);
        }
        public void ClearEditor()
        {
            _editFioDataService.ClearEditorFromFio(tab);
        }
        public void UpdatePerson(Person _p)
        {
            _editFioDataService.UpdateFioPerson(_p);
        }
        public void InsertPerson(Person _p, DatabaseHelper dbHelper)
        {
            _editFioDataService = new EditFioDataService(dbHelper);
            _editFioDataService.InsertFioPerson(_p);
        }
    }
}
