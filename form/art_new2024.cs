using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraEditors;

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
            visibleSP(false);
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
                    break;
                // Новый артикул СП (шнуры,резинка)
                case 1:
                    visibleSP(true);
                    break;
                // Копия артикула
                case 2:
                    visibleSP(false);
                    copyArt(kodSQL);
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
            lookUpEditPrizn.Visible = boolShow;
        }

        // Загрузка комбобоксов
        private void comboAllTableItems()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                lookUpEditOneTableItems(lookUpEditGost,  "SELECT id_gost AS 'ИД' ,name_gost AS 'Имя' ,opi_gost AS 'Описание' FROM gost WHERE ust=1", "Имя", connection);
                lookUpEditOneTableItems(lookUpEditGroup, "SELECT ag_naimen AS 'Наименование' FROM gost_sv_pict,articul_grup  where articul_grup.ag_id=gost_sv_pict.id_art ", "Наименование", connection);
                lookUpEditOneTableItems(lookUpEditTm1,  "SELECT kodsp AS kle, m_naimen AS 'Наименование' FROM dbo.view_tovar_marka where tmOwn = 1 ", "Наименование", connection);
                lookUpEditOneTableItems(lookUpEditTm2, "SELECT name AS 'Наименование' FROM view_grup_men where men_id >0 order by men_id ", "Наименование", connection);
                lookUpEditOneTableItems(lookUpEditRazm, "SELECT razm AS 'Размер' FROM gost_sv_razmer, gost_razmer where gost_sv_razmer.id_razmer=gost_razmer.id_rost ", "Размер", connection);
                //lookUpEditOneTableItems(lookUpEditPrizn, "SELECT tcds_name FROM TOVAR_CAT_DYNSIGN where tcds_tcat_id in (886,895) ORDER BY TCDS_NAME ", "tcds_name", connection);
            }
            lookUpEditPrizn.Properties.DataSource = ShowRelatedData("global", "SELECT tcds_name AS 'Признак' FROM TOVAR_CAT_DYNSIGN where tcds_tcat_id in (886,895) ORDER BY TCDS_NAME ");
            lookUpEditPrizn.Properties.DisplayMember = "Признак";
        }
        private void lookUpEditOneTableItems(LookUpEdit lookUpEdit1, string query, string displayMember, SqlConnection connection)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
            DataTable tableList = new DataTable();
            dataAdapter.Fill(tableList);
            // Загрузка в LookUpEdit
            lookUpEdit1.Properties.DataSource = tableList;
            lookUpEdit1.Properties.DisplayMember = displayMember;
        }
        private void copyArt(string kodArtSQL)
        {
            string query = "select kod, articul, razm from sp_articul where kod = " + customTextBoxKod1.Text;
            var tableList = ShowRelatedData("ace", query);
            // Загружаем данные:
            customTextBoxKod1.Text = tableList.Rows[0]["kod"].ToString();
            customTextBoxArt.Text = tableList.Rows[0]["articul"].ToString();
            customTextBoxRazm.Text = tableList.Rows[0]["razm"].ToString();


            //// комбобоксы:
            //customComboBoxDolj.Text = tableList.Rows[0]["rab"].ToString().Trim();
            //if (!string.IsNullOrWhiteSpace(tableList.Rows[0]["mast"].ToString()))
            //    comboOneTableItems(customComboBoxOrg,
            //            $"SELECT TRIM(name) AS nameColumn FROM sp_firms WHERE sp_firms.kod = {tableList.Rows[0]["mast"].ToString()}",
            //             "nameColumn", connection, false);
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
