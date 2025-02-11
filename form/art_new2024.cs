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
using DevExpress.Pdf.Native.BouncyCastle.Crypto;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using static DevExpress.XtraEditors.Filtering.DataItemsExtension;

namespace SewingProduction.form
{
    public partial class art_new2024 : CustomForm
    {
        // Оснавная БД:
        string connectionString = Properties.Settings.Default.ACEConnectionString;
        // Для тестов:
        //string connectionString = Properties.Settings.Default.ACEtestConnectionString;
        string kodSQL;
        public art_new2024(string kodArtSQL)
        {
            InitializeComponent();
            radioGroup1.SelectedIndex = 0;
            comboAllTableItems();
            kodSQL = kodArtSQL;
        }

        private void art_new2024_Load(object sender, EventArgs e)
        {

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
            string query;
            /*using (SqlConnection connection = new SqlConnection(connectionString))
            {
                query = "SELECT id_gost AS 'ИД' ,name_gost AS 'Имя' ,TRIM(opi_gost) AS 'Описание' FROM gost WHERE ust=1";
                lookUpEditOneTableItems(searchLookUpEditGost, query, "Описание", connection);

                query = $"SELECT TRIM(ag_naimen) AS 'Наименование' FROM gost_sv_pict,articul_grup  WHERE articul_grup.ag_id=gost_sv_pict.id_art ";
                lookUpEditOneTableItems(searchLookUpEditGroup, query, "Наименование", connection);

                query = "SELECT TRIM(kodsp) AS kle, TRIM(m_naimen) AS 'Наименование' FROM dbo.view_tovar_marka WHERE tmOwn = 1 "; 
                lookUpEditOneTableItems(searchLookUpEditTm1, query, "Наименование", connection);

                query = "SELECT men_id AS 'Группа', TRIM(name) AS 'Наименование' FROM view_grup_men WHERE men_id >0 order by men_id ";
                lookUpEditOneTableItems(searchLookUpEditTm2, query, "Наименование", connection);

                query = "SELECT DISTINCT TRIM(razm) AS 'Размер' FROM gost_sv_razmer, gost_razmer WHERE gost_sv_razmer.id_razmer=gost_razmer.id_rost ";
                lookUpEditOneTableItems(searchLookUpEditRazm, query, "Размер", connection);
            }*/
            query = "SELECT id_gost AS 'ИД' ,name_gost AS 'Имя' ,TRIM(opi_gost) AS 'Описание' FROM gost WHERE ust=1";
            searchLookUpEditGost.Properties.DataSource = ShowRelatedData("ace", query);
            searchLookUpEditGost.Properties.DisplayMember = "Описание";

            query = $"SELECT TRIM(ag_naimen) AS 'Наименование' FROM gost_sv_pict,articul_grup  WHERE articul_grup.ag_id=gost_sv_pict.id_art ";
            searchLookUpEditGroup.Properties.DataSource = ShowRelatedData("ace", query);
            searchLookUpEditGroup.Properties.DisplayMember = "Наименование";

            query = "SELECT TRIM(kodsp) AS kle, TRIM(m_naimen) AS 'Наименование' FROM dbo.view_tovar_marka WHERE tmOwn = 1 ";
            searchLookUpEditTm1.Properties.DataSource = ShowRelatedData("ace", query);
            searchLookUpEditTm1.Properties.DisplayMember = "Наименование";

            query = "SELECT men_id, TRIM(name) AS 'Наименование' FROM view_grup_men WHERE men_id >0 order by men_id ";
            searchLookUpEditTm2.Properties.DataSource = ShowRelatedData("ace", query);
            searchLookUpEditTm2.Properties.DisplayMember = "Наименование";

            query = "SELECT DISTINCT TRIM(razm) AS 'Размер' FROM gost_sv_razmer, gost_razmer WHERE gost_sv_razmer.id_razmer=gost_razmer.id_rost ";
            searchLookUpEditRazm.Properties.DataSource = ShowRelatedData("ace", query);
            searchLookUpEditRazm.Properties.DisplayMember = "Размер";

            query = "SELECT tcds_name AS 'Признак' FROM TOVAR_CAT_DYNSIGN WHERE tcds_tcat_id in (886,895) ORDER BY TCDS_NAME ";
            searchLookUpEditPrizn.Properties.DataSource = ShowRelatedData("global", query);
            searchLookUpEditPrizn.Properties.DisplayMember = "Признак";
        }
        /*
        private void lookUpEditOneTableItems(SearchLookUpEdit searchLookUpEdit1, string query, string displayMember, SqlConnection connection)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
            DataTable tableList = new DataTable();
            dataAdapter.Fill(tableList);
            // Загрузка в LookUpEdit
            searchLookUpEdit1.Properties.DataSource = tableList;
            searchLookUpEdit1.Properties.DisplayMember = displayMember;
        }
        */
        private void lookUpEditGost_EditValueChanged(object sender, EventArgs e)
        {
            // услови выборки
            string query = string.IsNullOrWhiteSpace(searchLookUpEditGost.Text) ? "" : $" AND id_gost = (SELECT id_gost FROM gost WHERE ust=1 AND opi_gost = '{searchLookUpEditGost.Text}')";
            // сами запросы для searchLookUpEditGroup-ов (комбобокса с гридом)
            // группы
            string queryGroup = $"SELECT TRIM(ag_naimen) AS 'Наименование' FROM gost_sv_pict,articul_grup  WHERE articul_grup.ag_id=gost_sv_pict.id_art ";
            queryGroup += query;
            searchLookUpEditGroup.Properties.DataSource = ShowRelatedData("ace", queryGroup);
            searchLookUpEditGroup.Properties.DisplayMember = "Наименование";
            // размеры
            string queryRazm = $"SELECT DISTINCT TRIM(razm) AS 'Размер' FROM gost_sv_razmer, gost_razmer WHERE gost_sv_razmer.id_razmer=gost_razmer.id_rost ";
            queryRazm += query;
            searchLookUpEditRazm.Properties.DataSource = ShowRelatedData("ace", queryRazm);
            searchLookUpEditRazm.Properties.DisplayMember = "Размер";
        }
        private void copyArt()
        {
            string query = $"select kod, articul, razm AS 'Размер', kle, mod, grup, ag_id, kod_tnved, CAST(grupp AS INT) AS men_id from sp_articul where kod = '{kodSQL}'";
            var tableList = ShowRelatedData("ace", query);
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
