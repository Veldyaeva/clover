using System;
using System.Globalization;
using System.Windows.Forms;
using SewingProduction.Features.Sprav;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;

namespace SewingProduction.form
{
    public partial class EditFio : CustomForm
    {
        private readonly EditFioDataService _editFioDataService;
        DatabaseHelperSQL dbHelper = new DatabaseHelperSQL();
        private Person _person;
        private ToolTip toolTip = new ToolTip();
        bool isBusy = false;

        public EditFio(UserClass user, string idFIO, string openType) : base(user)
        {
            InitializeComponent();
            _editFioDataService = new EditFioDataService(dbHelper);
            _person = new Person();
            // ThemeManager.UpdateTheme(this);
            customOkButton1.DialogResult = DialogResult.None;
            //Имя формы:
            this.Text = openType;
            customTextBoxTab.Text = idFIO;
            toolTipButton();
        }
        public EditFio(UserClass user) : base(user)
        {
            InitializeComponent();
            _editFioDataService = new EditFioDataService(dbHelper);
            _person = new Person();
        }
        public EditFio()
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
            _person.UpdateEditor();

            _isLoadingUi = true;
            try
            {
                customTextBoxTab.Text = _person.tab.ToString();

                dateRozd.Text = _person.bday;
                customTextBoxFIO.Text = _person.fio;
                customMaskedTextBoxTelSot.Text = _person.tel_s;

                customMaskedTextBoxDatePriem.Text = _person.data_p;
                customMaskedTextBoxDateYvoln.Text = _person.datau;

                // Комбо — ставим текст как у тебя было
                customComboBoxDolj.Text = _person.rab;
                customComboBoxOrg.Text = _person.mast_sp_firms;
                customComboBoxOb.Text = _person.f_fvr_kod_fio_vid_rabot;
                customComboBoxPodr.Text = _person.gr_brig_object;
                customComboBoxNved.Text = _person.ved_brig_ved;
                customComboBoxNTab.Text = _person.ftabn_tab_n;
                customComboBoxPodr1c.Text = _person.podr_1c_id_spbrig;

                customComboBox1Cpodr.Text = _person.podr_name_podr1C;
                customComboBox1Cdolg.Text = _person.dolg_name_dolg1C;

                // Текстбоксы
                customTextBoxOsnTab.Text = _person.tab_sovm.ToString();
                customTextBoxTab1с.Text = _person.tab1c;

                customTextBoxNved.Text = _person.ved.ToString();
                customTextBoxNTab.Text = _person.ftabn.ToString();
                customTextBoxTabN.Text = _person.fgrd.ToString();
                customTextBoxSorted.Text = _person.ftabnsort.ToString();

                customTextBoxMast.Text = _person.mast?.ToString();
                customTextBoxPodr.Text = _person.okl.ToString();
                customTextBoxNTabVed.Text = _person.tab_new.ToString();

                customTextBoxTelRab.Text = _person.tel_r;
                customTextBoxTelDom.Text = _person.tel_d;

                customCheckBoxSovm.Checked = _person.sovm == 1;
                customCheckBoxSdel.Checked = _person.sdel == 1;
                customCheckBoxITR.Checked = _person.itr == 1;
                customCheckBoxDekret.Checked = _person.dekret == 1;

                customTextBoxINN.Text = _person.inn;

                // 1C поля
                customTextBox1CpodrID.Text = _person.podr_id_podr1C;
                customTextBox1CpodrINN.Text = _person.podr_inn_podr1C;

                customTextBox1CdolgID.Text = _person.dolg_d_id.ToString();
                customTextBox1CdolgINN.Text = _person.dolg_inn_dolg1C;
            }
            finally
            {
                _isLoadingUi = false;
            }

            // После загрузки — обновим автополя из комбо, но НЕ модель
            RefreshDerivedUiFromSelections();
        }
        private void RefreshDerivedUiFromSelections()
        {
            UpdateOrgDerivedUi();
            UpdateObDerivedUi();
            UpdatePodrDerivedUi();
            UpdateNvedDerivedUi();
            UpdateNTabDerivedUi();
            UpdatePodr1cDerivedUi();
            Update1CPodrDerivedUi();
            Update1CDolgDerivedUi();
        }

        private void UpdateOrgDerivedUi()
        {
            var orgName = customComboBoxOrg.Text?.Trim();
            if (string.IsNullOrWhiteSpace(orgName))
            {
                customTextBoxMast.Text = "";
                return;
            }

            var kod = _editFioDataService.GetKodFromSp_firms(orgName);
            customTextBoxMast.Text = kod ?? "";
        }

        private void UpdateObDerivedUi()
        {
            var obName = customComboBoxOb.Text?.Trim();
            if (string.IsNullOrWhiteSpace(obName))
                return;

            // тут UI полей нет, но оставлю вычисление на SetDataPerson()
        }

        private void UpdatePodrDerivedUi()
        {
            var podrName = customComboBoxPodr.Text?.Trim();
            if (string.IsNullOrWhiteSpace(podrName))
                return;

            // UI полей нет — тоже в SetDataPerson()
        }

        private void UpdateNvedDerivedUi()
        {
            var name = customComboBoxNved.Text?.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                customTextBoxNved.Text = "";
                return;
            }

            customTextBoxNved.Text = _editFioDataService.GetVdidFromBrig_ved(name) ?? "";
        }

        private void UpdateNTabDerivedUi()
        {
            var name = customComboBoxNTab.Text?.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                customTextBoxNTab.Text = "";
                return;
            }

            customTextBoxNTab.Text = _editFioDataService.GetTnidFromTab_n(name) ?? "";
        }

        private void UpdatePodr1cDerivedUi()
        {
            var name = customComboBoxPodr1c.Text?.Trim();
            if (string.IsNullOrWhiteSpace(name))
                return;

            // UI полей нет — в SetDataPerson()
        }

        private void Update1CPodrDerivedUi()
        {
            var name = customComboBox1Cpodr.Text?.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                customTextBox1CpodrID.Text = "";
                customTextBox1CpodrINN.Text = "";
                return;
            }

            var pdUid = _editFioDataService.GetPd_uidFromPodr1C(name);
            var id = _editFioDataService.GetIdFromPodr1C(pdUid);
            var inn = _editFioDataService.GetInnFromPodr1C(pdUid);

            customTextBox1CpodrID.Text = id ?? "";
            customTextBox1CpodrINN.Text = inn ?? "";
        }

        private void Update1CDolgDerivedUi()
        {
            var name = customComboBox1Cdolg.Text?.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                customTextBox1CdolgID.Text = "";
                customTextBox1CdolgINN.Text = "";
                return;
            }

            var dId = _editFioDataService.GetD_idFromDolg1C(name);
            var inn = _editFioDataService.GetInnFromDolg1C(dId);

            customTextBox1CdolgID.Text = dId ?? "";
            customTextBox1CdolgINN.Text = inn ?? "";
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
        private bool _isLoadingUi;

        private int ParseIntOrZero(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return 0;
            return int.TryParse(text.Trim(), out var v) ? v : 0;
        }

        private int? ParseIntOrNull(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            return int.TryParse(text.Trim(), out var v) ? v : (int?)null;
        }
        /// <summary>
        /// Процедура добавления данных в обьект
        /// </summary>
        public void SetDataPerson()
        {
            // простые поля
            _person.fio = customTextBoxFIO.Text;
            _person.tel_s = customMaskedTextBoxTelSot.Text.Replace(" ", "");

            _person.bday = dateRozd.Value.ToString();
            _person.data_p = customMaskedTextBoxDatePriem.Text;
            _person.datau = customMaskedTextBoxDateYvoln.Text == "  .  ." ? "01.01.1900" : customMaskedTextBoxDateYvoln.Text;

            _person.tel_r = customTextBoxTelRab.Text;
            _person.tel_d = customTextBoxTelDom.Text;

            _person.sovm = customCheckBoxSovm.Checked ? 1 : 0;
            _person.sdel = customCheckBoxSdel.Checked ? 1 : 0;
            _person.itr = customCheckBoxITR.Checked ? 1 : 0;
            _person.dekret = customCheckBoxDekret.Checked ? 1 : 0;

            _person.edit_komp = Environment.MachineName;
            _person.tab = customTextBoxTab.Text == "АВТО" ? 0 : ParseIntOrZero(customTextBoxTab.Text);

            // --- Организация ---
            _person.mast_sp_firms = customComboBoxOrg.Text?.Trim() ?? "";
            _person.mast = string.IsNullOrWhiteSpace(_person.mast_sp_firms)
                ? ""
                : (_editFioDataService.GetKodFromSp_firms(_person.mast_sp_firms) ?? "");

            // --- Должность ---
            _person.rab = customComboBoxDolj.Text?.Trim() ?? "";

            // --- Вид работ ---
            _person.f_fvr_kod_fio_vid_rabot = customComboBoxOb.Text?.Trim() ?? "";
            var fvrKodStr = string.IsNullOrWhiteSpace(_person.f_fvr_kod_fio_vid_rabot)
                ? ""
                : (_editFioDataService.GetKodFromFio_vid_rabot(_person.f_fvr_kod_fio_vid_rabot) ?? "");
            _person.f_fvr_kod = ParseIntOrZero(fvrKodStr);

            // --- Подразделение (brig_object) ---
            _person.gr_brig_object = customComboBoxPodr.Text?.Trim() ?? "";
            var grStr = string.IsNullOrWhiteSpace(_person.gr_brig_object)
                ? ""
                : (_editFioDataService.GetGrFromBrig_object(_person.gr_brig_object) ?? "");
            _person.gr = ParseIntOrZero(grStr);

            // --- Табельность/признак (Tab_n) ---
            _person.ftabn_tab_n = customComboBoxNTab.Text?.Trim() ?? "";
            // ты автозаполняешь customTextBoxNTab — используем его (и не падаем)
            _person.ftabn = ParseIntOrZero(customTextBoxNTab.Text);

            // --- Ведущая (Brig_ved) ---
            _person.ved_brig_ved = customComboBoxNved.Text?.Trim() ?? "";
            // customTextBoxNved автозаполняется из комбо
            _person.ved = ParseIntOrZero(customTextBoxNved.Text);

            // --- 1C Подр из spbrig ---
            _person.podr_1c_id_spbrig = customComboBoxPodr1c.Text?.Trim() ?? "";
            _person.podr_1c_id = string.IsNullOrWhiteSpace(_person.podr_1c_id_spbrig)
                ? ""
                : (_editFioDataService.GetPodr_1c_idFromSpbrig(_person.podr_1c_id_spbrig) ?? "");

            // --- 1C Подразделение (Podr1C) ---
            _person.podr_name_podr1C = customComboBox1Cpodr.Text?.Trim() ?? "";
            _person.podr_pd_uid = string.IsNullOrWhiteSpace(_person.podr_name_podr1C)
                ? ""
                : (_editFioDataService.GetPd_uidFromPodr1C(_person.podr_name_podr1C) ?? "");
            _person.podr_id_podr1C = string.IsNullOrWhiteSpace(_person.podr_pd_uid)
                ? ""
                : (_editFioDataService.GetIdFromPodr1C(_person.podr_pd_uid) ?? "");
            _person.podr_inn_podr1C = string.IsNullOrWhiteSpace(_person.podr_pd_uid)
                ? ""
                : (_editFioDataService.GetInnFromPodr1C(_person.podr_pd_uid) ?? "");

            // --- 1C Должность (Dolg1C) ---
            _person.dolg_name_dolg1C = customComboBox1Cdolg.Text?.Trim() ?? "";
            _person.dolg_d_id = string.IsNullOrWhiteSpace(_person.dolg_name_dolg1C)
                ? ""
                : (_editFioDataService.GetD_idFromDolg1C(_person.dolg_name_dolg1C) ?? "");
            _person.dolg_inn_dolg1C = string.IsNullOrWhiteSpace(_person.dolg_d_id)
                ? ""
                : (_editFioDataService.GetInnFromDolg1C(_person.dolg_d_id) ?? "");

            // --- Остальные int из текстбоксов (БЕЗ Parse) ---
            _person.tab_sovm = ParseIntOrZero(customTextBoxOsnTab.Text);
            _person.tab1c = customTextBoxTab1с.Text;

            _person.fgrd = ParseIntOrZero(customTextBoxTabN.Text);
            _person.ftabnsort = ParseIntOrZero(customTextBoxSorted.Text);

            _person.okl = ParseIntOrZero(customTextBoxPodr.Text);
            _person.tab_new = ParseIntOrZero(customTextBoxNTabVed.Text);

            _person.inn = customTextBoxINN.Text?.Trim();
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
            if (_isLoadingUi) return;
            UpdateOrgDerivedUi();
        }

        private void customComboBoxDolj_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingUi) return;
            // UI-логики нет — можно вообще удалить обработчик, если не нужен
        }

        private void customComboBoxOb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingUi) return;
            // UI-логики нет — можно удалить
        }

        private void customComboBoxPodr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingUi) return;
            // UI-логики нет — можно удалить
        }

        private void customComboBoxNved_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingUi) return;
            UpdateNvedDerivedUi();
        }

        private void customComboBoxNTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingUi) return;
            UpdateNTabDerivedUi();
        }

        private void customComboBoxPodr1c_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingUi) return;
            // UI-логики нет — можно удалить
        }

        private void customComboBox1Cpodr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingUi) return;
            Update1CPodrDerivedUi();
        }

        private void customComboBox1Cdolg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingUi) return;
            Update1CDolgDerivedUi();
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


}
