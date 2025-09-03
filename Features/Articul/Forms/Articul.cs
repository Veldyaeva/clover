//using Microsoft.ReportingServices.DataProcessing;
using DevExpress.Data.Internal;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using SewingProduction.Core.interfaces;
using SewingProduction.Core.Models;
using SewingProduction.Extensions;
using SewingProduction.Features.Articul;
using SewingProduction.Features.Articul.Forms;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.Articul.Service;
using SewingProduction.Features.Sprav;
using SewingProduction.Features.UserDistribution.Forms;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Help.Form;
using SewingProduction.Helpers;
using SewingProduction.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BindingSource = System.Windows.Forms.BindingSource;
using DataTable = System.Data.DataTable;

//using DataTable = DevExpress.DataAccess.Native.Data.DataTable;

namespace SewingProduction.Features.Articul
{
    public partial class Articul : CustomForm
    {
        private readonly DatabaseHelper _dbHelperAce;
        private UserClass _user;
        private readonly ILogger _logger = new FileLogger();
        //все поля таблицы Артикул
        private SpArticulPreviewModel _articulByKod;
        //краткий перечень полей таблицы
        private List<ArticulModel> _artPreview;
        //фурнитура на артикул
        private List<ArtDrModel> _artDrForKod;
        //состав комплекта по коду 
        //private List<SpArticulKomplSostModel> _SpArticulKomplSostKod;

        ////все поля таблицы Артикул
        private BindingList<SpArticulPreviewModel> _articulBindingList;


        private List<SpArticulKomplSostModel> _articulKomplSostList;
        private List<spArticulNaborSostav> _articulNaborSostList;

        //private BindingSource _articulBindingSource;
        //private BindingSource _komplSostBindingSource;


        ArticulDataService _articulDataService = new ArticulDataService();
        public Articul(UserClass user) : base(user)
        {
            _dbHelperAce = new DatabaseHelper();
            InitializeComponent();
            _user = user;
            ThemeManager.UpdateTheme(this);

        }

        private async void Articul_Load(object sender, EventArgs e)
        {

            try
            {
                //загрузка перечня кодов из справочника, часть полей
                _artPreview = await _articulDataService.GetArtPreviewAsync();
                bsArt.DataSource = _artPreview;
                // инициализация привязок данных к элементам
                InitializeBindingsAsync();


                // загрузка комбиков для выбора полотна
                //bindComboBoxTkanName(); // ЛЕНА ТУТ ОШИБКА Я ЗАКОМЕНТИЛ


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
                //MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке формы Articul");
            }

        }

        private async Task InitializeBindingsAsync()
        {
            try
            {
                //var artPreviewTask = Task.Run(() =>
                //{
                //загрузка перечня кодов из справочника, часть полей
                _articulBindingList = new BindingList<SpArticulPreviewModel>();
                bsArticul = new BindingSource { DataSource = _articulBindingList };

                //состав комплекта 
                //_articulKomplSostList = new BindingList<SpArticulKomplSostModel>();
                //bsSostKompl = new BindingSource { DataSource = _articulKomplSostList };

                //});

                //await Task.WhenAll(artPreviewTask);

                #region заполнение блока основных данных артикула
                txbKod.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Kod), true, DataSourceUpdateMode.Never);
                txbArticul.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Articul), true, DataSourceUpdateMode.Never);
                txbMod.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Mod), true);
                txbTM.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.TmName), true);
                txbSeason.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.SeasonName), true);
                txbAssort.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.AssortName), true);
                txbCountry.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.CountryName), true);
                txbGrupMenName.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.GrupMenName), true);
                mtbDateOpis.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.DateOpis), true);
                txbGrup.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Grup), true);
                txbIdGost.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Id_gost), true);
                txbNameGost.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.GostName), true);
                txbOpiGost.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.GostOpi), true);
                txbSost.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Sost), true);
                txbSost2.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Sost2), true);
                txbSost3.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Sost3), true);
                txbRazm.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Razm), true);
                txbScNomer.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.ScNomer), true);
                txbKodTnved.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Kod_tnved), true);
                txbNDS.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Nds), true);
                #endregion

                #region галки с отделками

                //галки вяз отделки
                chbKombIzd.DataBindings.Add("Checked", bsArticul, nameof(SpArticulPreviewModel.Komb_izd), true);
                chbKombDet.DataBindings.Add("Checked", bsArticul, nameof(SpArticulPreviewModel.Komb_det), true);
                chbArh.DataBindings.Add("Checked", bsArticul, nameof(SpArticulPreviewModel.Arh), true);

                //отделка
                chbIsUpak.DataBindings.Add("Checked", bsArticul, nameof(SpArticulPreviewModel.Is_upak), true);
                chbIsFurnit.DataBindings.Add("Checked", bsArticul, nameof(SpArticulPreviewModel.Is_furnit), true);

                chkP.DataBindings.Add("Checked", bsArticul, nameof(SpArticulPreviewModel.P), true);
                chkV.DataBindings.Add("Checked", bsArticul, nameof(SpArticulPreviewModel.V), true);
                chkBus.DataBindings.Add("Checked", bsArticul, nameof(SpArticulPreviewModel.Bus), true);
                chkStra.DataBindings.Add("Checked", bsArticul, nameof(SpArticulPreviewModel.Stra), true);
                chkPres.DataBindings.Add("Checked", bsArticul, nameof(SpArticulPreviewModel.P_pres), true);
                #endregion

                #region Затраты на изготовление
                txbNormt.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Norm_t), true, DataSourceUpdateMode.Never);

                // TODO: добавить расчет полной с\ст на изделие по коду 
                //txbSeb.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Seb), true, DataSourceUpdateMode.Never);

                // нормы на полотно 
                foreach (CustomTextBox el in cgbTkanNorm.Controls)
                {
                    char si = el.Name.Last();
                    var name = $"Norm_t{si}";
                    if (el.GetType() == typeof(CustomTextBox))
                    {
                        el.DataBindings.Add("Text",bsArticul,name,true,DataSourceUpdateMode.Never);
                        el.Text = string.Format("{0:F2}", el.Text);
                    }
                }
                //себестоимость
                foreach (CustomTextBox el in cgbTkanSeb.Controls)
                {
                    char si = el.Name.Last();
                    var name = $"Seb_t{si}";
                    if (el.GetType() == typeof(CustomTextBox))
                    {
                        el.DataBindings.Add("Text", bsArticul, name, true, DataSourceUpdateMode.Never);
                    }
                }
                // брак
                foreach (CustomTextBox el in cgbTkanBrak.Controls)
                {
                    char si = el.Name.Last();
                    //var name = $"nameof(SpArticulPreviewModel.Brak_t{si})";
                    var name = $"Brak_t{si}";

                    if (el.GetType() == typeof(CustomTextBox))
                    {
                        el.DataBindings.Add("Text", bsArticul, name, true, DataSourceUpdateMode.Never);
                        //el.Text = string.Format("{0:F2}", el.Text);
                    }
                }
                // % брака 
                foreach (CustomTextBox el in cgbBrakPercent.Controls)
                {
                    char si = el.Name.Last();
                    var name = $"Brak_percent{si}";
                    if (el.GetType() == typeof(CustomTextBox))
                    {
                        // вывод строки в формате 2 знака после запятой 
                        el.DataBindings.Add("Text", bsArticul, name, true, DataSourceUpdateMode.Never,null, "F2");
                        
                    }
                }
                // коэф-т качества полотна
                foreach (CustomTextBox el in cgbKfKach.Controls)
                {
                    char si = el.Name.Last();
                    var name = $"Kf_tkan_kach{si}";
                    if (el.GetType() == typeof(CustomTextBox))
                    {
                        // вывод строки в формате 2 знака после запятой 
                        el.DataBindings.Add("Text", bsArticul, name, true, DataSourceUpdateMode.Never, null, "F2");
                    }
                }
                // назначение полотна 
                foreach (CustomTextBox el in cgbTkanPurpose.Controls)
                {
                    char si = el.Name.Last();
                    var name = $"Opis_t{si}";
                    if (el.GetType() == typeof(CustomTextBox))
                    {
                        el.DataBindings.Add("Text", bsArticul, name, true, DataSourceUpdateMode.Never);
                    }
                }

                #endregion

                #region Норма/сек + зарплата 

                txbSek.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Sek), true, DataSourceUpdateMode.Never);
                txbSekVyaz.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Sek_vyaz), true, DataSourceUpdateMode.Never);
                txbSekShv.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Sek_shv), true, DataSourceUpdateMode.Never);
                txbSekKr.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Sek_kr), true, DataSourceUpdateMode.Never);
                //зарплата
                txbSumZarpl.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Sum_zarpl), true, DataSourceUpdateMode.Never);
                txbSumDopOpl.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Sum_dopopl), true, DataSourceUpdateMode.Never);
                txbSumStrVznos.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Sum_strvznos), true, DataSourceUpdateMode.Never);
                txbSumSebRaskr.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Sum_sebraskr), true, DataSourceUpdateMode.Never);
                txbSumKomplNum.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Sum_komplnum), true, DataSourceUpdateMode.Never);


                #endregion
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
        }
        /// <summary>
        /// Получение фурнитуры по коду справочника
        /// </summary>
        /// <param name="kod"></param>
        /// <returns></returns>
        private async Task getArtDrForKodAsync(string kod)
        {
            try
            {
                bsArtDr.Clear();
                bsArtDr.ResetBindings(false);

                var articulByKodTemp = await _articulDataService.GetArtDrByKod(kod);

                if (articulByKodTemp != null)
                {
                    await this.InvokeAsync(() =>
                    {
                        _artDrForKod = articulByKodTemp;       // Обновляем текущую модель
                        bsArtDr.DataSource = _artDrForKod; // Привязываем данные к форме

                    });
                    bsArtDr.ResetBindings(false);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных getArticulFromSQlAsync для kod {kod}");
            }
        }
        private async Task getArticulFromSQlAsync(string kod)
        {
            try
            {
                bsArticul.Clear();
                bsArticul.ResetBindings(false);

                var articulByKodTemp = await _articulDataService.GetByKodAsync(kod);

                if (articulByKodTemp != null)
                {

                    await this.InvokeAsync(() =>
                    {
                        _articulByKod = articulByKodTemp;                // Обновляем текущую модель
                        bsArticul.DataSource = _articulByKod; // Привязываем данные к форме

                    });
                    bsArticul.ResetBindings(false);
                }

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных getArticulFromSQlAsync для kod {kod}");
            }
        }
        private async Task getSostKomplFromSQlAsync(string kod)
        {
            try
            {
                bsSostKompl.Clear();
                bsSostKompl.ResetBindings(false);

                var articulByKodTemp = await _articulDataService.GetSostavkomplForKod(kod);

                if (articulByKodTemp != null)
                {

                    await this.InvokeAsync(() =>
                    {
                        _articulKomplSostList = articulByKodTemp;                // Обновляем текущую модель
                        bsSostKompl.DataSource = _articulKomplSostList; // Привязываем данные к форме

                    });
                    bsSostKompl.ResetBindings(false);
                }

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных getSostKomplFromSQlAsync для kod {kod}");
            }
        }
        private async Task getSostNaborFromSQlAsync(string kod)
        {
            try
            {
                bsSostNabor.Clear();
                bsSostNabor.ResetBindings(false);

                var articulByKodTemp = await _articulDataService.GetSostavNaborForKod(kod);

                if (articulByKodTemp != null)
                {

                    await this.InvokeAsync(() =>
                    {
                        _articulNaborSostList = articulByKodTemp;                // Обновляем текущую модель
                        bsSostNabor.DataSource = _articulNaborSostList; // Привязываем данные к форме

                    });
                    bsSostNabor.ResetBindings(false);
                }

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных getSostNaborFromSQlAsync для kod {kod}");
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


        /// <summary>
        ////используется в закоменченном методе при добавлении и копировании кода
        /// </summary>
        /// <param name="kod"></param>
        /// <returns></returns>
        private async Task getArticulFromSQl(string kod)
        {
            try
            {
                //string queryArticul = $"select * from dbo.viewArticul_preview where va_kod = '{kod}'";
                //DataTable dt = _dbHelperAce.ExecuteQuery(queryArticul);
                _articulByKod = await _articulDataService.GetByKodAsync(kod);

                bsArticul.DataSource = _articulByKod;
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
                    foreach (CustomTextBox el in cgbTkanNorm.Controls)
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
                    foreach (CustomTextBox el in cgbTkanSeb.Controls)
                    {
                        char si = el.Name.Last();
                        if (el.GetType() == typeof(CustomTextBox))
                        {
                            el.Text = ((DataTable)bsArticul.DataSource).Rows[0][$"va_seb_t{si}"].ToString();
                        }
                    }
                    //брак
                    //foreach (CustomTextBox el in gbTkanBrak_old.Controls)
                    //{
                    //    char si = el.Name.Last();
                    //    if (el.GetType() == typeof(CustomTextBox))
                    //    {
                    //        el.Text = ((DataTable)bsArticul.DataSource).Rows[0][$"va_brak{si}"].ToString();
                    //        // вывод строки в формате 2 знака после запятой 
                    //        el.Text = string.Format("{0:F2}", el.Text);
                    //    }
                    //}


                    foreach (CustomTextBox el in cgbBrakPercent.Controls)
                    {
                        char si = el.Name.Last();
                        if (el.GetType() == typeof(CustomTextBox))
                        {
                            //el.Text = ((DataTable)bsArticul.DataSource).Rows[0][$"va_brakpercent{si}"].ToString();
                            // вывод строки в формате 2 знака после запятой 
                            el.Text = $"{((DataTable)bsArticul.DataSource).Rows[0][$"va_brakpercent{si}"]:F2}";

                        }
                    }

                    //getArt_drFromSQl(kod);
                    /*
                    //customComboBox1.SelectedValue = ((DataTable)bsArticul.DataSource).Rows[0]["va_kod_t1"].ToString();
                    //customComboBox2.SelectedValue = ((DataTable)bsArticul.DataSource).Rows[0]["va_kod_t2"].ToString();
                    //customComboBox3.SelectedValue = ((DataTable)bsArticul.DataSource).Rows[0]["va_kod_t3"].ToString();
                    */
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        /// <summary>
        /// обновлениме данных на форме по коду при перемещении по таблице артикулов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void gridControl1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            string kod = "";
            string kodd = "";

            try
            {
                var currentRow = bsArt.Current as ArticulModel;
                if (currentRow != null)
                {
                    kod = currentRow.Kod;
                    kodd = currentRow.Kodd;

                    
                    Task getData = getArticulFromSQlAsync(kod);
                    Task getArtDrData = getArtDrForKodAsync(kod);
                    Task getKomplSostData = getSostKomplFromSQlAsync(kod);
                    Task getNaborSostData = getSostNaborFromSQlAsync(kod);

                    await Task.WhenAll(getData, getArtDrData, getKomplSostData, getNaborSostData);

                    cTabPage1.PageVisible = false;
                    cTabPage2.PageVisible = false;

                    switch (bsSostKompl.Current, bsSostNabor.Current) {

                        case ( not null, null):

                            cTabPage1.PageVisible = true;
                            break;
                        case ( null, not null):
                            cTabPage2.PageVisible = true;
                            break;
                        default:
                            cTabPage1.PageVisible = false;
                            cTabPage2.PageVisible = false;
                            break;
                    }


                            string query = $"select dbo.getFileEskizForKodd('{kodd}') as pathpict ";
                    var dt = _dbHelperAce.ExecuteQuery(query);
                    if (dt != null)
                    {
                        pictureBoxArticul.Image = Image.FromFile(((DataTable)dt).Rows[0]["pathpict"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {

                await _logger.LogErrorAsync(ex, $"Ошибка получения данных gridControl1_FocusedRowChanged для kod {kod}");

            }
        }
        /// <summary>
        /// вызывает карточку по коду из справочника ШП
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void customButtonKart_Click(object sender, EventArgs e)
        {
            string kod = "";
            try
            {
                GetItogVibKartReport report = new GetItogVibKartReport();
                report.RequestParameters = false;

                var currentRow = bsArt.Current as ArticulModel;
                if (currentRow != null)
                {
                    kod = currentRow.Kod;

                    report.Parameters["kod"].Value = kod;

                    var ds = report.sqlDataSource1;
                    var query = ds.Queries[0] as DevExpress.DataAccess.Sql.StoredProcQuery;
                    query.Parameters[0].Value = kod;

                    ds.Fill();

                    report.DataSource = ds;
                    report.DataMember = "GetItogVibKart";

                    ReportPrintTool reportPrintTool = new ReportPrintTool(report);
                    reportPrintTool.ShowPreviewDialog();

                    //сокращенный :
                    GetItogVibKartSokrReport reportSokr = new GetItogVibKartSokrReport();
                    reportSokr.RequestParameters = false;
                    reportSokr.Parameters["kod"].Value = kod;
                    reportSokr.DataSource = ds;
                    reportSokr.DataMember = "GetItogVibKart";
                    ReportPrintTool reportSokrPrintTool = new ReportPrintTool(reportSokr);
                    reportSokrPrintTool.ShowPreviewDialog();

                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка получения данных customButtonKart_Click для kod {kod} для отображенияк карточки");
                kod = "";
            }

        }
        /// <summary>
        /// добавление нового кода 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customButtonCopy_Click(object sender, EventArgs e)
        {
            //var kodObj = gridControl1.GetFocusedRowCellValue("Kod");
            var kodObj = (bsArt.Current as ArticulModel).Kod;

            EditAricul f = new EditAricul(kodObj.ToString());
            if (f.ShowDialog() == DialogResult.OK)
            {
                Articul_Load(sender, e);
            }
        }
        /// <summary>
        /// создание состава комплекта kompl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customButtonKompl_Click(object sender, EventArgs e)
        {
            /*//var kodObj = gridControl1.GetFocusedRowCellValue("Kod");
            var kodObj = (bsArt.Current as ArticulModel).Kod;
            if (this.MdiParent is SpMainForm mainForm)
            {
                mainForm.OpenForm(new AddNewKopml(CurrentUser.User, _articuls, kodObj.ToString()));
            }
            */
        }

        private void csButtonNew_Click(object sender, EventArgs e)
        {
            /*EditAricul f = new EditAricul(_user);
            if (f.ShowDialog() == DialogResult.OK)
            {
                Articul_Load(sender, e);
            }
            */
        }
    }
}
