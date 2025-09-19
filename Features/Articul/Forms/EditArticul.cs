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
using DevExpress.Xpo.Logger.Transport;
using DevExpress.XtraEditors;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Org.BouncyCastle.Crypto;
using SewingProduction.Core.Models;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using static DevExpress.XtraEditors.Filtering.DataItemsExtension;

namespace SewingProduction.Features.Articul
{
    /// <summary>
    /// Добавление артикула
    /// </summary>
    public partial class EditArticul : CustomForm // FoxPro: art_new2024
    {
        private readonly ArtNewDataService _artNewDataService;
        string kodSQL;
        public EditArticul(string kodArtSQL = null)
        {
            InitializeComponent();
            DatabaseHelper dbHelper = new DatabaseHelper();
            _artNewDataService = new ArtNewDataService(dbHelper);
            ThemeManager.UpdateTheme(this);
            kodSQL = kodArtSQL;
            customTextBoxKod.Text = kodSQL;
            visibleSP(false);
            radioGroup1.SelectedIndex = kodArtSQL == null ? 0 : 2; // новый арт / копия
        }

        private void art_new2024_Load(object sender, EventArgs e) => comboAllTableItems();

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
                    if (customTextBoxKod.Text == "")
                    {
                        MessageBox.Show("Введите код артикула!");
                        radioGroup1.SelectedIndex = 0;
                        customTextBoxKod.Focus();
                    }
                    else
                    {
                        visibleSP(false);
                        kodSQL = customTextBoxKod.Text;
                        copyArt();
                    }
                    break;
            }
        }

        private void newArt()
        {
            customTextBoxKod.Text = "";
            customTextBoxPo.Text = "";
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
            searchLookUpEditTm1.Properties.ValueMember = "kle";

            searchLookUpEditTm2.Properties.DataSource = _artNewDataService.GetViewGrupMen();
            searchLookUpEditTm2.Properties.DisplayMember = "Наименование";
            searchLookUpEditTm2.Properties.ValueMember = "men_id";

            searchLookUpEditRazm.Properties.DataSource = _artNewDataService.GetGostSvRazmerAndGostRazmer();
            searchLookUpEditRazm.Properties.DisplayMember = "Размер";

            searchLookUpEditPrizn.Properties.DataSource = _artNewDataService.GetTovarCatDynsign();
            searchLookUpEditPrizn.Properties.DisplayMember = "Признак";
        }
        private void lookUpEditGost_EditValueChanged(object sender, EventArgs e)
        {
            searchLookUpEditGost.Properties.ValueMember = "Ид";
            var idGost = searchLookUpEditGost.EditValue?.ToString();
            // группы
            searchLookUpEditGroup.Properties.DataSource = _artNewDataService.GetGostSvPictAndArticulGrup(idGost);
            searchLookUpEditGroup.Properties.DisplayMember = "Наименование";
            // размеры
            searchLookUpEditRazm.Properties.DataSource = _artNewDataService.GetGostSvRazmerAndGostRazmer(idGost);
            searchLookUpEditRazm.Properties.DisplayMember = "Размер";
        }
        // Копирование артикула
        private void copyArt()
        {
            var tableList = _artNewDataService.GetSpArticulByKod(kodSQL);
            // Загружаем данные:
            if (tableList.Rows.Count > 0)
            {
                var row = tableList.Rows[0];

                customTextBoxKod.Text = row["kod"].ToString();

                searchLookUpEditGost.Properties.ValueMember = "Ид";
                searchLookUpEditGost.EditValue = row["id_gost"].ToString().Trim();

                searchLookUpEditGroup.Properties.ValueMember = "Наименование";
                searchLookUpEditGroup.EditValue = row["grup"].ToString().Trim();

                searchLookUpEditTm1.Properties.ValueMember = "kle";
                searchLookUpEditTm1.EditValue = row["kle"].ToString().Trim();

                searchLookUpEditTm2.Properties.ValueMember = "men_id";
                searchLookUpEditTm2.EditValue = row["men_id"].ToString().Trim();

                customTextBoxArt.Text = row["articul"].ToString();

                searchLookUpEditRazm.Properties.ValueMember = "Размер";
                searchLookUpEditRazm.EditValue = row["Размер"].ToString().Trim();

                customTextBoxModel.Text = row["mod"].ToString();
            }
            //searchLookUpEditGost.Enabled = false;
            //searchLookUpEditGroup.Enabled = false;
        }

        // Кнопка сохранить
        private void customOkButtonSave_Click(object sender, EventArgs e)
        {
            var error = checkRule();
            if (error != null)
            {
                MessageBox.Show(error);
                return;
            }
            ArticulModel articulModel = new ArticulModel
            {
                Kod = customTextBoxKod.Text,
                Po = customTextBoxPo.Text,
                Id_gost = Convert.ToInt32(searchLookUpEditGost.EditValue),
                Gost = searchLookUpEditGost.Text,
                Grup = searchLookUpEditGroup.Text,
                Kle = searchLookUpEditTm1.Text,
                Articul = customTextBoxArt.Text,
                Razm = searchLookUpEditRazm.Text,
                Mod = customTextBoxModel.Text,
                Komp_name = Environment.MachineName,
                Sql_pr_add = 1,
                Date_add = DateTime.Now,
                Ag_id = Convert.ToInt32((searchLookUpEditGroup.Properties.View
                .GetFocusedRowCellValue("Ag_id") ?? 0)),

                Kod_tnved = searchLookUpEditGroup.Properties.View
                    .GetFocusedRowCellValue("Ag_tnved")?.ToString(),

                Grupp = Convert.ToInt32(searchLookUpEditTm2.EditValue)
            };
            if (radioGroup1.SelectedIndex == 1) // Новый артикул СП (шнуры,резинка)
            {
                error = checkRuleRezinka();
                if (error != null)
                {
                    MessageBox.Show(error);
                    return;
                }
                var cordLength = int.TryParse(customTextBoxDlin.Text, out var len) ? len : 0;
                articulModel.Kod_lv3 = customTextBoxKodFurn.Text;
                articulModel.Sek_cord = int.TryParse(customTextBoxTimePlet.Text, out var sc) ? sc : 0;
                articulModel.Norm_cord = int.TryParse(customTextBoxNormP.Text, out var nc) ? nc : 0;
                articulModel.Norm_t = articulModel.Norm_cord * cordLength;
                articulModel.Norm_t1 = articulModel.Norm_cord * cordLength;
                articulModel.Sek_vyaz = articulModel.Sek_cord * cordLength;
                articulModel.Sek = articulModel.Sek_cord * cordLength;
                if (!string.IsNullOrEmpty(articulModel.Kod) && articulModel.Kod.Length >= 8)
                    articulModel.Po = articulModel.Kod.Substring(7, 1);

                articulModel.Tgm_id_n = Convert.ToInt32((searchLookUpEditPrizn.Properties.View
                .GetFocusedRowCellValue("tcds_id") ?? 0));
            }
            var props = typeof(ArticulModel).GetProperties();
            foreach (var p in props)
            {
                if (p.GetValue(articulModel) != null)
                    Debug.WriteLine($"{p.Name} = {p.GetValue(articulModel)}");
            }
            //await _artNewDataService.SaveAsync(articulModel);

            this.DialogResult = DialogResult.OK;
            this.Close();

        }
        string checkRule()
        {
            int kod = _artNewDataService.GetArticulByKod(customTextBoxKod.Text);
            if (kod > 0)
            {
                return ("Такой код артикула уже есть!");
            }
            if (string.IsNullOrWhiteSpace(customTextBoxKod.Text))
            {
                return ("Укажите код!");
            }
            if (string.IsNullOrWhiteSpace(searchLookUpEditGost.Text) || searchLookUpEditGost.EditValue == null)
            {
                return ("Выберите гост!");
            }
            if (string.IsNullOrWhiteSpace(searchLookUpEditGroup.Text))
            {
                return ("Выберите группу!");
            }
            if (string.IsNullOrWhiteSpace(customTextBoxArt.Text))
            {
                return ("Введите артикул!");
            }
            return null;
        }
        string checkRuleRezinka()
        {
            if (string.IsNullOrWhiteSpace(customTextBoxKodFurn.Text))
            {
                return ("Укажите код материала!");
            }
            if (!int.TryParse(customTextBoxTimePlet.Text, out var sekCord) || sekCord == 0)
            {
                return ("Укажите секунды плетения на 1 метр!");
            }
            if (!int.TryParse(customTextBoxNormP.Text, out var normCord) || normCord == 0)
            {
                return ("Укажите норму пряжи на 1 метр!");
            }
            if (searchLookUpEditPrizn.EditValue == null)
            {
                return ("Укажите признак шнура!");
            }
            return null;
        }
        private void customButtonCancel_Click(object sender, EventArgs e)
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
