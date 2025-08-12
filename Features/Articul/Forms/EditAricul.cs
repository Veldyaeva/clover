using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Pdf.Native;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SewingProduction.Helpers;
using static DevExpress.XtraEditors.Filtering.DataItemsExtension;

namespace SewingProduction.Features.Articul
{
    /// <summary>
    /// Добавление артикула
    /// </summary>
    public partial class EditAricul : CustomForm // FoxPro: art_new2024
    {
        private readonly ArtNewDataService _artNewDataService;
        string kodSQL;
        public EditAricul(string kodArtSQL = null)
        {
            InitializeComponent();
            DatabaseHelper dbHelper = new DatabaseHelper();
            _artNewDataService = new ArtNewDataService(dbHelper);
            ThemeManager.UpdateTheme(this);
            kodSQL = kodArtSQL;
        }
        public EditAricul()
        {
            InitializeComponent();
        }

        private void art_new2024_Load(object sender, EventArgs e)
        {
            comboAllTableItems();
            radioGroup1.SelectedIndex = 0;
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (radioGroup1.SelectedIndex)
            {
                // Новый артикул
                default:
                    visibleSP(false);
                    newArt();
                    break;
                // Новый артикул СП (шнуры,резинка)
                case 1:
                    visibleSP(true);
                    newArt();
                    break;
                // Копия артикула
                case 2:
                    visibleSP(false);
                    copyArt();
                    break;
            }
        }

        private void newArt()
        {
            customTextBoxKod1.Text = "";
            customTextBoxKod2.Text = "";
            customTextBoxArt.Text = "";
            customTextBoxModel.Text = "";
            customTextBoxKodFurn.Text = "";
            customTextBox1.Text = "";
            customTextBoxDlin.Text = "";
            customTextBoxTimePlet.Text = "";
            customTextBoxNormP.Text = "";
            searchLookUpEditGost.EditValue = "";
            searchLookUpEditGroup.EditValue = "";
            searchLookUpEditTm1.EditValue = "";
            searchLookUpEditTm2.EditValue = "";
            searchLookUpEditRazm.EditValue = "";
            searchLookUpEditPrizn.EditValue = "";
            searchLookUpEditGost.Enabled = true;
            searchLookUpEditGroup.Enabled = true;
        }
        // Видимость элиментов для "Новый артикул СП (шнуры,резинка)"
        private void visibleSP(bool boolShow)
        {
            customLabelKodFurn.Visible = boolShow;
            customLabel1.Visible = boolShow;
            customLabelDlin.Visible = boolShow;
            customLabelTimePlet.Visible = boolShow;
            customLabelNormP.Visible = boolShow;
            customLabelPrizn.Visible = boolShow;
            customLabelM.Visible = boolShow;
            customLabelM1.Visible = boolShow;
            customLabelM2.Visible = boolShow;
            customTextBoxKodFurn.Visible = boolShow;
            customTextBox1.Visible = boolShow;
            customTextBoxDlin.Visible = boolShow;
            customTextBoxTimePlet.Visible = boolShow;
            customTextBoxNormP.Visible = boolShow;
            searchLookUpEditPrizn.Visible = boolShow;
        }

        // Загрузка комбобоксов
        private void comboAllTableItems()
        {
            searchLookUpEditGost.Properties.DataSource = _artNewDataService.GetGostUst();
            searchLookUpEditGost.Properties.DisplayMember = "Описание";

            searchLookUpEditGroup.Properties.DataSource = _artNewDataService.GetGostSvPictAndArticulGrup();
            searchLookUpEditGroup.Properties.DisplayMember = "Наименование";

            searchLookUpEditTm1.Properties.DataSource = _artNewDataService.GetViewTovarMarka();
            searchLookUpEditTm1.Properties.DisplayMember = "Наименование";

            searchLookUpEditTm2.Properties.DataSource = _artNewDataService.GetViewGrupMen();
            searchLookUpEditTm2.Properties.DisplayMember = "Наименование";

            searchLookUpEditRazm.Properties.DataSource = _artNewDataService.GetGostSvRazmerAndGostRazmer();
            searchLookUpEditRazm.Properties.DisplayMember = "Размер";

            searchLookUpEditPrizn.Properties.DataSource = _artNewDataService.GetTovarCatDynsign();
            searchLookUpEditPrizn.Properties.DisplayMember = "Признак";
        }
        private void lookUpEditGost_EditValueChanged(object sender, EventArgs e)
        {
            // группы
            searchLookUpEditGroup.Properties.DataSource = _artNewDataService.GetGostSvPictAndArticulGrupWhere(searchLookUpEditGost.Text);
            searchLookUpEditGroup.Properties.DisplayMember = "Наименование";
            // размеры
            searchLookUpEditRazm.Properties.DataSource = _artNewDataService.GetGostSvRazmerAndGostRazmerWhere(searchLookUpEditGost.Text);
            searchLookUpEditRazm.Properties.DisplayMember = "Размер";
        }
        // Копирование артикула
        private void copyArt()
        {
            var tableList = _artNewDataService.GetSpArticulKod(kodSQL);
            // Загружаем данные:
            if (tableList.Rows.Count > 0)
            {
                customTextBoxKod1.Text = tableList.Rows[0]["kod"].ToString();
                customTextBoxArt.Text = tableList.Rows[0]["articul"].ToString();

                searchLookUpEditRazm.Properties.ValueMember = "Размер";
                searchLookUpEditRazm.EditValue = tableList.Rows[0]["Размер"].ToString().Trim();

                searchLookUpEditTm1.Properties.ValueMember = "kle";
                searchLookUpEditTm1.Text = tableList.Rows[0]["kle"].ToString().Trim();

                searchLookUpEditTm2.Properties.ValueMember = "men_id";
                searchLookUpEditTm2.Text = tableList.Rows[0]["men_id"].ToString().Trim();

            }
            searchLookUpEditGost.Enabled = false;
            searchLookUpEditGroup.Enabled = false;
        }

        // Кнопка сохранить
        private void customOkButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        // Кнопка отмена
        private void customCancelButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        // Закрытие формы
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
