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
using SewingProduction.Features.Sprav;

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
   
    
}
