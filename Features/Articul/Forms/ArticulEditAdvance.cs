//using DevExpress.ChartRangeControlClient.Core;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using NLog.Layouts;
using SewingProduction.Core.Class;
using SewingProduction.Core.helpers;
using SewingProduction.Core.Models;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.Articul.Service;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
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
//using WinBindingSource = System.Windows.Forms.BindingSource;

namespace SewingProduction.Features.Articul.Forms
{
    public partial class ArticulEditAdvance : CustomForm
    {
        private DatabaseHelper _dbHelper;
        private DbService _dbService;
        private static BulkHelper _bulkHelper;

        //private ArticulDataService _articulDataService;
        private ArticulEditAdvanceService _articulEdAdvDataService;

        private readonly ILogger _logger = new FileLogger();

        private string _kodd;

        private BindingSource _bindingSourceArtKod;
        private BindingSource _bindingSourceArtCommon;
        private BindingSource _bindingSourceArtCommonSave;
        

        public ArticulEditAdvance(UserClass user) : this(user, "", "")
        { }

        public ArticulEditAdvance(UserClass user, string kodd, string articul) : base(user)
        {
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);
            _bulkHelper = new BulkHelper();

            _articulEdAdvDataService = new ArticulEditAdvanceService();

            InitializeComponent();
            _user = user;
            _kodd = kodd;
            this.Text = articul + " " + kodd;
            ThemeManager.UpdateTheme(this);

            _bindingSourceArtKod = new BindingSource { };
            if (gridEditAdRazm != null) gridEditAdRazm.DataSource = _bindingSourceArtKod;

            _bindingSourceArtCommon = new BindingSource { };
            _bindingSourceArtCommonSave = new BindingSource { };
        }

        private async void ArticulEditAdvance_Load(object sender, EventArgs e)
        {
            //загрузка списка размеров 

            _bindingSourceArtKod.DataSource = await _articulEdAdvDataService.GetArtByKoddAsync(this._kodd);
            // загрузка кешированных данных из справочников 
            await CommonSpravArticulEditAdvance.EnsureLoadedAsync(_dbService);


            InitializeBindingsAsync();

        }
        private async Task InitializeBindingsAsync()
        {
            try
            {
                //загрузка перечня кодов из справочника общая информация
                _bindingSourceArtCommon.DataSource = await _articulEdAdvDataService.GetCommonArtByKoddAsync(this._kodd);


                #region заполнение блока основных данных артикула

                txbArticul.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(ArticulModel.Articul), true);
                //txbArticul.DataBindings.Add("Text", _bindingSourceArtCommonSokr, nameof(SpArticulSaveAdvance.Articul), true);

                txbMod.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(ArticulModel.Mod), true);

                //госты
                txbGostId.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(ArticulModel.Id_gost), true);
                lookUpGost.Properties.DataSource = CommonSpravArticulEditAdvance.Gosts;
                lookUpGost.Properties.DisplayMember = nameof(GostModel.Name_gost);
                lookUpGost.Properties.ValueMember = nameof(GostModel.Id_gost);
                lookUpGost.Properties.NullText = "Не выбрано";
                lookUpGost.DataBindings.Add("EditValue", _bindingSourceArtCommon, nameof(ArticulModel.Id_gost), true);
                //инициализация описания госта
                UpdateOpi();
                
                //Страна
                cbTM.DataSource = CommonSpravArticulEditAdvance.Tms;
                cbTM.DisplayMember = nameof(TmModel.Kle_naimen);
                cbTM.ValueMember = nameof(TmModel.M_id_gl);
                cbTM.DataBindings.Add("SelectedValue", _bindingSourceArtCommon, nameof(ArticulModel.Id_country), true);
                //сезон
                cbSeason.DataSource = CommonSpravArticulEditAdvance.Seasons;
                cbSeason.DisplayMember = nameof(Szon_newModel.Txt);
                cbSeason.ValueMember = nameof(Szon_newModel.N);
                cbSeason.DataBindings.Add("SelectedValue", _bindingSourceArtCommon, nameof(ArticulModel.Baza), true);
                //группа менеджеров
                cbGrupMen.DataSource = CommonSpravArticulEditAdvance.GrupMen;
                cbGrupMen.DisplayMember = nameof(GrupMenModel.Name);
                cbGrupMen.ValueMember = nameof(GrupMenModel.Men_int);
                cbGrupMen.DataBindings.Add("SelectedValue", _bindingSourceArtCommon, nameof(ArticulModel.Grupp), true);
                //страна
                cbCountry.DataSource = CommonSpravArticulEditAdvance.Countries;
                cbCountry.DisplayMember = nameof(CountryModel.frm_country);
                cbCountry.ValueMember = nameof(CountryModel.frm_cu_id);
                cbCountry.DataBindings.Add("SelectedValue", _bindingSourceArtCommon, nameof(ArticulModel.Id_country), true);
                //ассортимент
                cbAssort.DataSource = CommonSpravArticulEditAdvance.Assorts;
                cbAssort.DisplayMember = nameof(AssortModel.txt_v);
                cbAssort.ValueMember = nameof(AssortModel.kod_v);
                cbAssort.DataBindings.Add("SelectedValue", _bindingSourceArtCommon, nameof(ArticulModel.Kod_v), true);
                txbAssort.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(ArticulModel.Kod_v), true);
                //ткань
                cbTkan.DataSource = CommonSpravArticulEditAdvance.Tkans;
                cbTkan.DisplayMember = nameof(SpArticulTkanSokr.Tkan);
                cbTkan.ValueMember = nameof(SpArticulTkanSokr.Tkb);
                cbTkan.DataBindings.Add("SelectedValue", _bindingSourceArtCommon, nameof(ArticulModel.Tkb), true);

                chbArh.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.Arh), true);
                chbKombDet.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.Komb_det), true);
                chbKombIzd.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.Komb_izd), true);

                //составы
                txbSost.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(ArticulModel.Sost), true);
                txbSost2.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(ArticulModel.Sost2), true);
                txbSost3.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(ArticulModel.Sost3), true);

                #endregion

                #region Отделка 
                chbIsUpak.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.Is_upak), true);
                chbIsFurnit.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.Is_furnit), true);

                chkP.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.P), true);
                chkV.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.V), true);
                chkBus.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.Bus), true);
                chkStra.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.Stra), true);
                chkPres.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.P_pres), true);
                #endregion

                #region Нормы

                BindFieldByName(this.layoutControlGroup4, _bindingSourceArtCommon);
                BindFieldByName(this.layoutControlGroup7, _bindingSourceArtCommon);

                BindFieldByNameLookUp(this.layoutControlGroup8, _bindingSourceArtCommon, CommonSpravArticulEditAdvance.Tkans);

                /*
                cbuKod_t1.Properties.DataSource = CommonSpravArticulEditAdvance.Tkans;
                cbuKod_t1.Properties.DisplayMember = nameof(SpArticulTkanSokr.Tkb);
                cbuKod_t1.Properties.ValueMember = nameof(SpArticulTkanSokr.Kod_t);
                cbuKod_t1.Properties.NullText = "Не выбрано";
                cbuKod_t1.DataBindings.Add("EditValue", _bindingSourceArtCommon, nameof(ArticulModel.Kod_t1), true);
                */

                #endregion

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
        }
        //, String NameField

        public async void BindFieldByNameLookUp(LayoutControlGroup group, BindingSource bs, IReadOnlyList<SpArticulTkanSokr> sprav) {
            try
            {
                foreach (BaseLayoutItem item in group.Items)
                {
                    if (item is not LayoutControlItem lci)
                        continue;

                    if (lci.Control == null)
                        continue;

                    var props = bs.CurrencyManager?.GetItemProperties();
                    if (props == null)
                        throw new InvalidOperationException("BindingSource не инициализирован");

                    if (lci.Control is SearchLookUpEdit sle)
                    {
                        string propName = sle.Name[3..]; // sluKod_t1 → Kod_t1

                        // проверка модели
                        if (props.Find(propName, true) == null)
                            throw new ArgumentException($"В модели нет свойства '{propName}'");

                        sle.Properties.DataSource = sprav; // общий справочник
                        sle.Properties.DisplayMember = nameof(SpArticulTkanSokr.Tkb);
                        sle.Properties.ValueMember = nameof(SpArticulTkanSokr.Kod_t);
                        sle.Properties.NullText = "Не выбрано";

                        sle.DataBindings.Clear();
                        sle.DataBindings.Add(
                            "EditValue",
                            bs,
                            propName,
                            true,
                            DataSourceUpdateMode.OnPropertyChanged
                        );

                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка привязки поля " + ex.Message);
                throw;
            }
        }
        

        public async void BindFieldByName(LayoutControlGroup group, BindingSource bs)
        {
            try
            {
                foreach (BaseLayoutItem item in group.Items)
                {
                    if (item is not LayoutControlItem lci)
                        continue;
                    if (lci.Control is DevExpress.XtraEditors.BaseEdit edit)
                    {
                        var props = bs.CurrencyManager?.GetItemProperties();
                        if (props == null)
                            throw new InvalidOperationException("BindingSource не инициализирован");
                        
                        string propName = edit.Name.Length > 3 ? edit.Name[3..] : edit.Name;
                        // удаление префикса txt или txb controlName.Substring(3);
                        var pd = props.Find(propName, true);
                        if (pd == null)
                            throw new ArgumentException($"В модели нет свойства '{propName}'");

                        edit.DataBindings.Clear();
                        edit.DataBindings.Add(
                            "EditValue",
                            bs,
                            propName,
                            true,
                            DataSourceUpdateMode.OnPropertyChanged
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка привязки поля " + ex.Message);
                throw;
            }
        }

       

        private void UpdateOpi()
        {
            if (lookUpGost.EditValue is int id)
                txbOpiGost.Text = CommonSpravArticulEditAdvance.Gosts.FirstOrDefault(x => x.Id_gost == id)?.Opi_gost ?? "";
            else
                txbOpiGost.Text = "";
        }
        /// <summary>
        /////Сохранение изменений общих данных артикула для всех кодов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveChanges(object sender, EventArgs e)
        {
            try
            {
                _bindingSourceArtCommon.EndEdit();
                //копирование всех кодов с измененными общими данными в список для сохранения
                var currentItem = (ArticulModel)_bindingSourceArtCommon.Current;
                if (currentItem.IsModified == false)
                {
                    return;
                }

                for (int i = 0; i < _bindingSourceArtKod.Count; i++)
                {
                    var item = (ArticulModel)_bindingSourceArtKod[i];
                    //    
                    var newItem = ObjectCloneHelper.CloneWithExclusions(currentItem, clone =>
                    {
                        clone.Kod = item.Kod;
                        clone.IsModified = true;

                    }, "Kod", "Razm");
                    _bindingSourceArtCommonSave.Add(newItem);
                }
                //формирование списка для сохранения с уникальными кодами
                List<ArticulModel> filteredList = _bindingSourceArtCommonSave.List
                    .OfType<ArticulModel>()
                    .Where(x => x?.IsModified == true)
                    .ToList();

                if (filteredList.Count > 0)
                {
                    using (SqlConnection connection = _dbHelper.GetConnection())
                    {
                        _bulkHelper.BulkAllDataUpdate<ArticulModel>(connection, filteredList, "sp_articul", new[] { "kod" });
                    }
                }
                _bindingSourceArtCommonSave.Clear();
                XtraMessageBox.Show("Изменения успешно сохранены.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при сохранении изменений артикула");
                XtraMessageBox.Show("Ошибка при сохранении изменений артикула: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            }


        private void lookUpGost_EditValueChanged(object sender, EventArgs e)
        {
            UpdateOpi();
        }

        private void ArticulEditAdvance_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
