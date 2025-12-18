//using Microsoft.ReportingServices.DataProcessing;
using DevExpress.Data.Internal;
using DevExpress.Office.Utils;
using DevExpress.Xpo;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit.Model;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraWaitForm;
using SewingProduction.Core.Class;
using SewingProduction.Core.interfaces;
using SewingProduction.Core.Models;
using SewingProduction.Extensions;
using SewingProduction.Features.Articul;
using SewingProduction.Features.Articul.Forms;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.Articul.Service;
using SewingProduction.Features.Sprav;
using SewingProduction.Features.TeamWork.Forms;
using SewingProduction.Features.UserDistribution.Class;
using SewingProduction.Features.UserDistribution.Forms;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Help.Form;
using SewingProduction.Helpers;
using SewingProduction.Report;
using SewingProduction.Services;
using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel;
using System.Data;
using System.Data;
using System.Diagnostics;
using System.Diagnostics;
using System.Drawing;
using System.Drawing;
using System.Linq;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using static DevExpress.Office.PInvoke.Win32;
using static DevExpress.Xpo.DB.DataStoreLongrunnersWatch;
using BindingSource = System.Windows.Forms.BindingSource;
using DataTable = System.Data.DataTable;

//using DataTable = DevExpress.DataAccess.Native.Data.DataTable;

namespace SewingProduction.Features.Articul
{
    public partial class Articul : CustomForm
    {
        private readonly DatabaseHelper _dbHelperAce;

        private readonly DbService _dbService;
        private UserClass _user;
        private readonly ILogger _logger = new FileLogger();
        //все поля таблицы Артикул
        private SpArticulPreviewModel _articulByKod;

        private bool _isInitialized;

        
        private BindingList<SpArticulPreviewModel> _articulBindingList;

        ArticulDataService _articulDataService = new ArticulDataService();
        public Articul(UserClass user) : base(user)
        {
            _dbHelperAce = new DatabaseHelper();
            _dbService = new DbService(_dbHelperAce);
            InitializeComponent();
            _user = user;

            //_artPreviewBindingList = new BindingList<SpArtPreviewModel>();

            ThemeManager.UpdateTheme(this);
        }
        private async Task RefreshArtPreviewAsync()
        {

            bsArt?.Clear();
            bsArt.DataSource = await _articulDataService.GetArtPreviewAsyncBindingList();

        }


        private async void Articul_Load(object sender, EventArgs e)
        {

            try
            {
                //загрузка перечня кодов из справочника, часть полей
                await RefreshArtPreviewAsync();
                if (!_isInitialized)
                {
                    await InitializeBindingsAsync();
                    _isInitialized = true;
                }
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
                //архив
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
                        el.DataBindings.Add("Text", bsArticul, name, true, DataSourceUpdateMode.Never);
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
                        el.DataBindings.Add("Text", bsArticul, name, true, DataSourceUpdateMode.Never, null, "F2");

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

                #region коэфициенты

                txbSebDop.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Seb_dop), true, DataSourceUpdateMode.Never);
                txbKoefPr.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Koef_pr), true, DataSourceUpdateMode.Never);
                txbKoefVedDG.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Koef_d), true, DataSourceUpdateMode.Never);
                txbSebProizv.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Seb_proizv), true, DataSourceUpdateMode.Never);
                txbKoef.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Koef), true, DataSourceUpdateMode.Never);
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
                bsArtDr?.Clear();

                var _artDrForKod = await _articulDataService.GetArtDrByKodAsync(kod);

                if (_artDrForKod != null)
                {
                    await this.InvokeAsync(() =>
                    {
                        
                        bsArtDr.DataSource = _artDrForKod; // Привязываем данные к форме
                        
                    });
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных GetArtDrByKod для kod {kod}");
            }
        }
        private async Task getArticulFromSQlAsync(string kod)
        {
            try
            {
                bsArticul?.Clear();

                _articulByKod = await _articulDataService.GetByKodAsync(kod);

                if (_articulByKod != null)
                {

                    await this.InvokeAsync(() =>
                    {

                        bsArticul.DataSource = _articulByKod; // Привязываем данные к форме
                    });
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
                bsSostKompl?.Clear();

                var articulByKodTemp = await _articulDataService.GetSostavkomplForKod(kod);

                if (articulByKodTemp != null)
                {

                    await this.InvokeAsync(() =>
                    {
                        //_articulKomplSostList = articulByKodTemp;                // Обновляем текущую модель
                        bsSostKompl.DataSource = articulByKodTemp; // Привязываем данные к форме

                    });
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
                bsSostNabor?.Clear();

                var articulByKodTemp = await _articulDataService.GetSostavNaborForKod(kod);

                if (articulByKodTemp != null)
                {

                    await this.InvokeAsync(() =>
                    {
                        
                        bsSostNabor.DataSource = articulByKodTemp; // Привязываем данные к форме

                    });
                    bsSostNabor.ResetBindings(false);
                }

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных getSostNaborFromSQlAsync для kod {kod}");
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
                var currentRow = bsArt.Current as SpArtPreviewModel;

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

                    switch (bsSostKompl.Current, bsSostNabor.Current)
                    {

                        case (not null, null):

                            cTabPage1.PageVisible = true;
                            break;
                        case (null, not null):
                            cTabPage2.PageVisible = true;
                            break;
                        default:
                            cTabPage1.PageVisible = false;
                            cTabPage2.PageVisible = false;
                            break;
                    }
                    // получение изображения по пути
                    string imagePath = null;
                    try
                    {
                        imagePath = await _articulDataService.GetFileEskizForKod(kodd);
                        if (!string.IsNullOrEmpty(imagePath))
                        {
                            pictureBoxArticul.ImageLocation = imagePath;
                        }
                        else
                        {
                            pictureBoxArticul.ImageLocation = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        await _logger.LogErrorAsync(ex, $"Ошибка загрузки изображения по пути '{imagePath ?? "NULL"}'");
                        pictureBoxArticul.ImageLocation = null;
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

                var currentRow = bsArt.Current as SpArtPreviewModel;
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
        private async void customButtonCopy_Click(object sender, EventArgs e)
        {
            //var current = bsArt.Current as SpArtPreviewModel;
            var kodObj = (bsArt.Current as SpArtPreviewModel).Kod;
            if (kodObj == null)
            {
                MessageBox.Show("Не выбран артикул для копирования.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (EditArticul f = new EditArticul(_user, kodObj.ToString()))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    await RefreshArtPreviewAsync();
                }
            }
        }
        /// <summary>
        /// создание состава комплекта kompl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customButtonKompl_Click(object sender, EventArgs e)
        {
            //var kodObj = gridControl1.GetFocusedRowCellValue("Kod");
            var kodObj = (bsArt.Current as SpArtPreviewModel).Kod;
            if (this.MdiParent is SpMainForm mainForm)
            {
                //mainForm.OpenForm(new AddNewKopml(User, _artPreview, kodObj.ToString()));
            }
        }

        private async void csButtonNew_Click(object sender, EventArgs e)
        {
            using (EditArticul f = new EditArticul())
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    await RefreshArtPreviewAsync();
                }
            }
        }
        /// <summary>
        /// удаление кода в справочнике
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void sButtodDeleteKod_Click(object sender, EventArgs e)
        {
            try
            {
                var kod = (bsArt.Current as SpArtPreviewModel).Kod;

                string query = "exec dbo.kodArticulisUsed @kod = @kod";
                DataTable result = await _dbHelperAce.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@kod", kod } });

                var cuRow = (SpArtPreviewModel)bsArt.Current;

                if (result.Rows.Count > 0)
                {
                    //return result.Rows[0]["fio"].ToString(); 
                    if (result.Rows[0].Field<int>("error") != 0)
                    {
                        MessageBox.Show("Ошибка удаления" + result.Rows[0].Field<string>("messageerror"));
                        return;
                    }
                    //удаление кода 

                    await _dbService.DeleteEntityAsync("sp_articul", "Kod", cuRow);

                    //_artPreview.Remove(cuRow);
                    //bsArt.ResetBindings(false);
                    bsArt.RemoveCurrent();

                }
                result?.Dispose();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при Удалении");
            }
        }
        /// <summary>
        /// открывает форму редактирования состава набора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customButton3_Click(object sender, EventArgs e)
        {
            var Obj = bsArt.Current as SpArtPreviewModel;
            //нужно добавить проверку на признак НАБОРА, чтобы можно было открыть только набор.
            //Debug.WriteLine(Obj.Gost);

            ArticulNaborSostavDataService _ANSDataService = new ArticulNaborSostavDataService();
            if (_ANSDataService.CheckOpis(Obj.Kod))
            {
                MessageBox.Show("Набор уже описан, изменения применятся на весь размерный ряд!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (this.MdiParent is SpMainForm mainForm)
            {
                //TODO: нужно изменить тип Obj на SpArtPreviewModel!!
                //mainForm.OpenForm(new EditNaborSostav(User, Obj));
            }
        }
        /// <summary>
        /// открывает на редактирование карточку артикула
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="bindingSource"></param>
        /// <returns></returns>
        //private async Task EditArtciul (GridView gridView, IList list, BindingSource bindingSource, bool forMyDataAnnView = false)
        private void EditArtciul(GridView gridView, BindingSource bindingSource)
        {
            if (gridView == null || gridView.FocusedRowHandle < 0)
            {
                MessageBox.Show("Выберите артикул для редактирования!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var kodd = (bsArt.Current as SpArtPreviewModel).Kodd;
            var articul = (bsArt.Current as SpArtPreviewModel).Articul.Trim();

            if (this.MdiParent is SpMainForm mainForm)
            {
                mainForm.OpenForm(new ArticulEditAdvance(CurrentUser.User, kodd, articul));
            }
        }
        //???
        #region Управление немодальной формой ArticulEditAdvance

        private static readonly List<ArticulEditAdvance> _openEditArticulForms = new List<ArticulEditAdvance>();
        private static readonly object _lockObject = new object();
        private static bool HasOpenAdvanceForms()
        {
            lock (_lockObject)
            {
                // Очищаем закрытые формы из списка
                _openEditArticulForms.RemoveAll(form => form == null || form.IsDisposed);
                return _openEditArticulForms.Count > 0;
            }
        }

        /// <summary>
        /// Добавляет экземпляр ArticulEditAdvance в список открытых форм
        /// </summary>
        /// <param name="form">Форма для добавления</param>
        private static void AddOpenAdvanceForm(ArticulEditAdvance form)
        {
            lock (_lockObject)
            {
                if (form != null && !form.IsDisposed && !_openEditArticulForms.Contains(form))
                {
                    _openEditArticulForms.Add(form);
                }

            }
        }
        /// <summary>
        /// Удаляет экземпляр ArticulEditAdvance из списка открытых форм
        /// </summary>
        /// <param name="form">Форма для удаления</param>
        private static void RemoveOpenAdvanceForm(ArticulEditAdvance form)
        {
            lock (_lockObject)
            {
                _openEditArticulForms.Remove(form);
            }
        }
        #endregion

        private async void csButtonEdit_Click(object sender, EventArgs e)
        {
            EditArtciul(gridControl1, bsArt);

        }

        private void Articul_FormClosed(object sender, FormClosedEventArgs e)
        {
            gridControl1.FocusedRowChanged -= gridControl1_FocusedRowChanged;

            // Отвязать BindingSource
            bsArt.DataSource = null;

            // Dispose DevExpress контролов
            gridControl1?.Dispose();
            gridView1?.Dispose();

            // Dispose автогенерируемых объектов
            components?.Dispose();
        }

        
    }
}
