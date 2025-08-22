//using Microsoft.ReportingServices.DataProcessing;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using BindingSource = System.Windows.Forms.BindingSource;
using SewingProduction.Helpers;
using SewingProduction.Report;
using System.Diagnostics;
using DevExpress.XtraReports.UI;
using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Help.Form;
using System.Collections.Generic;
using System.Threading.Tasks;
using SewingProduction.Features.Articul.Forms;
using SewingProduction.Features.Sprav;
using SewingProduction.Features.UserDistribution.Forms;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.Articul;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.Articul.Service;
using DevExpress.XtraGrid.Views.Grid;

//using DataTable = DevExpress.DataAccess.Native.Data.DataTable;

namespace SewingProduction.Features.Articul
{
    public partial class Articul : CustomForm
    {
        private readonly DatabaseHelper _dbHelperAce;
        private UserClass _user;
        ArticulModel _articulByKod;
        List<ArticulModel> _artPreview;
        ArticulDataService _articulDataService = new ArticulDataService();
        public Articul(UserClass user) : base(user)
        {
            _dbHelperAce = new DatabaseHelper();
            InitializeComponent();
            _user = user;
        }


        private async void Articul_Load(object sender, EventArgs e)
        {
            // данная строка кода позволяет загрузить данные в таблицу "aCE_backupDataSet.art_norm_n". При необходимости она может быть перемещена или удалена.
            //this.art_norm_nTableAdapter.Fill(this.aCE_backupDataSet.art_norm_n);
            try
            {
                _artPreview = await _articulDataService.GetArtPreviewAsync();
                //загрузка перечня кодов из справочника, часть полей
                //string query = $"select * from dbo.view_art";
                //kodd,kod, grup, articul, razm, mod, kle
                //var dt = _dbHelperAce.ExecuteQuery(query);

                //bsArt.DataSource = dt;
                bsArt.DataSource = _artPreview;
                // загрузка одиночного кода из справочника, все поля  
                getArticulFromSQl("0");

                // загрузка комбиков для выбора полотна
                //bindComboBoxTkanName(); // ЛЕНА ТУТ ОШИБКА Я ЗАКОМЕНТИЛ

                /*// тест 
                comboBoxEdit1.Properties.DataSource = dt;
                lookUpEdit1.Properties.DataSource = dt;
                lookUpEdit1.Properties.DisplayMember = "tkb";
                lookUpEdit1.Properties.ValueMember = "kod_t";
                */

                //customComboBox1.SelectedValue = ((DataTable)bsArticul.DataSource).Rows[0]["va_kod_t1"].ToString();

                //// не нужно. оставила для примера, привязка Combox к полю
                //query = $"SELECT kodsp,M_Naimen_Sokr FROM view_tovar_marka where tmOwn = 1 ";
                //dt = ShowRelatedData("ace", query);
                //bsTM.DataSource = dt;
                //cbTM.DisplayMember = "M_Naimen_Sokr";
                //cbTM.ValueMember = "kodsp";
                //cbTM.DataBindings.Add("SelectedValue", bsArticul, "va_kle", true, DataSourceUpdateMode.OnPropertyChanged);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void bindComboBoxTkanName()
        {
            string query = "SELECT tkan.tkb,tkan,kod_t, concat(tkb,kod_t) as concat  FROM tkan order by tkb";
            DataTable dt = _dbHelperAce.ExecuteQuery(query);

            foreach (CustomComboBox el in gbTkanName.Controls)
            {
                if (el.GetType() == typeof(CustomComboBox))
                {
                    char si = el.Name.Last();
                    BindingSource bs = new BindingSource();
                    bs.DataSource = dt;

                    el.DataSource = bs;
                    el.DisplayMember = "tkb";
                    el.ValueMember = "kod_t";
                    el.DataBindings.Add("SelectedValue", bsArticul, $"va_kod_t{si}", true, DataSourceUpdateMode.OnPropertyChanged);
                }
            }
        }
        private void getArt_drFromSQl(string kod)
        {
            try
            {
                string queryArticul = $"select * from dbo.view_art_dr where kod = '{kod}'";
                DataTable dt = _dbHelperAce.ExecuteQuery(queryArticul);
                bsArtDr.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task getArticulFromSQl(string kod)
        {
            try
            {
                //string queryArticul = $"select * from dbo.viewArticul_preview where va_kod = '{kod}'";
                //DataTable dt = _dbHelperAce.ExecuteQuery(queryArticul);
                _articulByKod = await _articulDataService.GetByKodAsync(kod);

                bsArticul.DataSource = dt;
                if (bsArticul.Count > 0)
                {
                    // нормы , с\стоимость
                    txbKod.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_kod"].ToString();
                    //txbArticul.Text = dt.Rows[0]["va_articul"].ToString();
                    txbArticul.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_articul"].ToString();
                    txbMod.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_mod"].ToString();
                    txbSeason.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_seasonName"].ToString();
                    txbTM.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_kleNaimen"].ToString();
                    txbAssort.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_assort"].ToString();
                    txbCountry.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_countryName"].ToString();
                    txbGrupMenName.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_grupMenName"].ToString();
                    txbGrup.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_grup"].ToString();
                    txbIdGost.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_idGost"].ToString();
                    txbNameGost.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_nameGost"].ToString();
                    txbOpiGost.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_opiGost"].ToString();
                    txbSost.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_sost"].ToString();
                    txbSost2.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_sost2"].ToString();
                    txbSost3.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_sost3"].ToString();
                    mtbDateOpis.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_dateOpis"].ToString();
                    txbScNomer.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_scNomer"].ToString();
                    txbKodTnved.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_kodTnved"].ToString();
                    txbNDS.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_kodTnved"].ToString();
                    txbRazm.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_razm"].ToString();
                    txbNormt.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_norm_t"].ToString();
                    txbBrakAll.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_BrakAll"].ToString();

                    //va_seb_z

                    //галки вяз отделки
                    chbKombIzd.Checked = Convert.ToBoolean(((DataTable)bsArticul.DataSource).Rows[0]["va_kombIzd"]);
                    chbKombDet.Checked = Convert.ToBoolean(((DataTable)bsArticul.DataSource).Rows[0]["va_kombdet"]);
                    chbArh.Checked = Convert.ToBoolean(((DataTable)bsArticul.DataSource).Rows[0]["va_arh"]);

                    //отделка
                    chbIsUpak.Checked = Convert.ToBoolean(((DataTable)bsArticul.DataSource).Rows[0]["va_isUpak"]);
                    chbIsFurnit.Checked = Convert.ToBoolean(((DataTable)bsArticul.DataSource).Rows[0]["va_isFurnit"]);

                    chkP.Checked = Convert.ToBoolean(((DataTable)bsArticul.DataSource).Rows[0]["va_p"]);
                    chkV.Checked = Convert.ToBoolean(((DataTable)bsArticul.DataSource).Rows[0]["va_v"]);
                    chkBus.Checked = Convert.ToBoolean(((DataTable)bsArticul.DataSource).Rows[0]["va_bus"]);
                    chkStra.Checked = Convert.ToBoolean(((DataTable)bsArticul.DataSource).Rows[0]["va_stra"]);
                    chkPres.Checked = Convert.ToBoolean(((DataTable)bsArticul.DataSource).Rows[0]["va_pPres"]);

                    //Норма, сек

                    txbSek.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_sek"].ToString();
                    txbSekVyaz.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_SekVyaz"].ToString();
                    txbSekShv.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_SekShv"].ToString();
                    txbSekKr.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_SekKr"].ToString();
                    txbSebz.Text = ((DataTable)bsArticul.DataSource).Rows[0]["va_Sebz"].ToString();

                    // нормы на полотно 
                    foreach (CustomTextBox el in gbTkanNorm.Controls)
                    {
                        char si = el.Name.Last();
                        if (el.GetType() == typeof(CustomTextBox))
                        {
                            //.Format("{0:C}", price)
                            el.Text = ((DataTable)bsArticul.DataSource).Rows[0][$"va_norm_t{si}"].ToString();
                            el.Text = string.Format("{0:F2}", el.Text);
                        }
                    }
                    //себестоимость
                    foreach (CustomTextBox el in gbTkanSeb.Controls)
                    {
                        char si = el.Name.Last();
                        if (el.GetType() == typeof(CustomTextBox))
                        {
                            el.Text = ((DataTable)bsArticul.DataSource).Rows[0][$"va_seb_t{si}"].ToString();
                        }
                    }
                    //брак
                    foreach (CustomTextBox el in gbTkanBrak.Controls)
                    {
                        char si = el.Name.Last();
                        if (el.GetType() == typeof(CustomTextBox))
                        {
                            el.Text = ((DataTable)bsArticul.DataSource).Rows[0][$"va_brak{si}"].ToString();
                            // вывод строки в формате 2 знака после запятой 
                            el.Text = string.Format("{0:F2}", el.Text);
                        }
                    }


                    foreach (CustomTextBox el in gbBrakPercent.Controls)
                    {
                        char si = el.Name.Last();
                        if (el.GetType() == typeof(CustomTextBox))
                        {
                            //el.Text = ((DataTable)bsArticul.DataSource).Rows[0][$"va_brakpercent{si}"].ToString();
                            // вывод строки в формате 2 знака после запятой 
                            el.Text = $"{((DataTable)bsArticul.DataSource).Rows[0][$"va_brakpercent{si}"]:F2}";

                        }
                    }

                    getArt_drFromSQl(kod);

                    //customComboBox1.SelectedValue = ((DataTable)bsArticul.DataSource).Rows[0]["va_kod_t1"].ToString();
                    //customComboBox2.SelectedValue = ((DataTable)bsArticul.DataSource).Rows[0]["va_kod_t2"].ToString();
                    //customComboBox3.SelectedValue = ((DataTable)bsArticul.DataSource).Rows[0]["va_kod_t3"].ToString();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void gridControl1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            /* ЛЕНА ТУТ ОШИБКА Я ЗАКОМЕНТИЛ
            //var kod = Convert.ToInt32(gridControl1.GetDataRow(gridControl1.FocusedRowHandle)["kod"]);
            string kod = "";
            string kodd = "";

            try
            {
                object data = gridControl1.GetRow(gridControl1.FocusedRowHandle);
                if (data != null)
                {
                    kod = ((DataRowView)data).Row["kod"].ToString();
                    kodd = ((DataRowView)data).Row["kodd"].ToString();
                }

                getArticulFromSQl(kod);

                string query = $"select dbo.getFileEskizForKodd('{kodd}') as pathpict ";
                var dt = _dbHelperAce.ExecuteQuery(query);
                if (dt != null)
                {
                    pictureBoxArticul.Image = Image.FromFile(((DataTable)dt).Rows[0]["pathpict"].ToString());

                }

            }
            catch
            {
                kod = "";
            }
            */
        }

        private void customButtonKart_Click(object sender, EventArgs e)
        {
            GetItogVibKartReport report = new GetItogVibKartReport();
            report.RequestParameters = false;

            object data = gridControl1.GetRow(gridControl1.FocusedRowHandle);
            var kod = ((DataRowView)data).Row["kod"].ToString();
            //var kod = "30367001";
            Debug.WriteLine(kod);
            report.Parameters["kod"].Value = kod;

            var ds = report.sqlDataSource1;
            var query = ds.Queries[0] as DevExpress.DataAccess.Sql.StoredProcQuery;
            query.Parameters[0].Value = kod;

            ds.Fill();

            report.DataSource = ds;
            report.DataMember = "GetItogVibKart";

            ReportPrintTool reportPrintTool = new ReportPrintTool(report);
            reportPrintTool.ShowPreviewDialog();

            //сокарщенный :
            GetItogVibKartSokrReport reportSokr = new GetItogVibKartSokrReport();
            reportSokr.RequestParameters = false;
            reportSokr.Parameters["kod"].Value = kod;
            reportSokr.DataSource = ds;
            reportSokr.DataMember = "GetItogVibKart";
            ReportPrintTool reportSokrPrintTool = new ReportPrintTool(reportSokr);
            reportSokrPrintTool.ShowPreviewDialog();

        }

        private void customButtonAdd_Click(object sender, EventArgs e)
        {
            EditAricul f = new EditAricul(_user);
            if (f.ShowDialog() == DialogResult.OK)
            {
                Articul_Load(sender, e);
            }
        }
        private void customButtonCopy_Click(object sender, EventArgs e)
        {
            var kodObj = gridControl1.GetFocusedRowCellValue("kod");
            EditAricul f = new EditAricul(_user, kodObj.ToString());
            if (f.ShowDialog() == DialogResult.OK)
            {
                Articul_Load(sender, e);
            }
        }
        private void customButtonKompl_Click(object sender, EventArgs e)
        {
            var kodObj = gridControl1.GetFocusedRowCellValue("kod");
            if (this.MdiParent is SpMainForm mainForm)
            {
                mainForm.OpenForm(new AddNewKopml(_user, _articuls, kodObj.ToString()));
            }
        }
    }
}
