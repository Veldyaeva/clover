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

namespace SewingProduction.form
{
    /// <summary>
    /// Добавление артикула
    /// </summary>
    public partial class art_new2024 : CustomForm
    {
        private readonly ArtNewDataService _artNewDataService;
        string kodSQL;
        public art_new2024(string kodArtSQL = null)
        {
            InitializeComponent();
            DatabaseHelper dbHelper = new DatabaseHelper();
            _artNewDataService = new ArtNewDataService(dbHelper);
            ThemeManager.UpdateTheme(this);
            kodSQL = kodArtSQL;
        }
        public art_new2024()
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
    public class ArtNewDataService
    {
        private readonly DatabaseHelper _dbHelper;
        public ArtNewDataService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        #region art_new2024
        public DataTable GetGostUst()
        {
            string query = "SELECT id_gost AS 'ИД' ,name_gost AS 'Имя' ,TRIM(opi_gost) AS 'Описание' FROM gost WHERE ust=1";
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetGostSvPictAndArticulGrup()
        {
            string query = $"SELECT TRIM(ag_naimen) AS 'Наименование' FROM gost_sv_pict,articul_grup  WHERE articul_grup.ag_id=gost_sv_pict.id_art ";
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetViewTovarMarka()
        {
            string query = "SELECT TRIM(kodsp) AS kle, TRIM(m_naimen) AS 'Наименование' FROM dbo.view_tovar_marka WHERE tmOwn = 1 ";
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetViewGrupMen()
        {
            string query = "SELECT men_id, TRIM(name) AS 'Наименование' FROM view_grup_men WHERE men_id >0 order by men_id ";
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetGostSvRazmerAndGostRazmer()
        {
            string query = "SELECT DISTINCT TRIM(razm) AS 'Размер' FROM gost_sv_razmer, gost_razmer WHERE gost_sv_razmer.id_razmer=gost_razmer.id_rost ";
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetTovarCatDynsign()
        {
            string query = "SELECT tcds_name AS 'Признак' FROM TOVAR_CAT_DYNSIGN WHERE tcds_tcat_id in (886,895) ORDER BY TCDS_NAME ";
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetGostSvPictAndArticulGrupWhere(string opiGost)
        {
            string condition = string.IsNullOrWhiteSpace(opiGost) ? "" : $" AND id_gost = (SELECT id_gost FROM gost WHERE ust=1 AND opi_gost = '{opiGost}')";
            string query = $"SELECT TRIM(ag_naimen) AS 'Наименование' FROM gost_sv_pict,articul_grup  WHERE articul_grup.ag_id=gost_sv_pict.id_art" + condition;
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetGostSvRazmerAndGostRazmerWhere(string opiGost)
        {
            string condition = string.IsNullOrWhiteSpace(opiGost) ? "" : $" AND id_gost = (SELECT id_gost FROM gost WHERE ust=1 AND opi_gost = '{opiGost}')";
            string query = $"SELECT DISTINCT TRIM(razm) AS 'Размер' FROM gost_sv_razmer, gost_razmer WHERE gost_sv_razmer.id_razmer=gost_razmer.id_rost" + condition;
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetSpArticulKod(string kodSQL)
        {
            string query = $"SELECT kod, articul, razm AS 'Размер', kle, mod, grup, ag_id, kod_tnved, CAST(grupp AS INT) AS men_id FROM sp_articul WHERE kod = '@kodSQL'";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@kodSQL", kodSQL } });
        }
        #endregion
    }
}
