//using DevExpress.ChartRangeControlClient.Core;
using DevExpress.CodeParser;
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
        private decimal _nRub_k;

        private BindingSource _bindingSourceArtKod;
        private BindingSource _bindingSourceArtCommon;
        private BindingSource _bindingSourceArtCommonSave;
        private BindingSource _bindingSourceGostGrup;

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
            _bindingSourceGostGrup = new BindingSource { };

        }

        private async void ArticulEditAdvance_Load(object sender, EventArgs e)
        {
            //загрузка списка размеров 

            var edAdvTask = _articulEdAdvDataService.GetArtByKoddAsync(this._kodd);
            var nRub_kTask = _dbHelper.ExecuteScalarAsync("SELECT dbo.getConstN('', LEFT(CONVERT(NVARCHAR, GETDATE(), 12), 4))");

            // загрузка кешированных данных из справочников 
            var sprTask = CommonSpravArticulEditAdvance.EnsureLoadedAsync(_dbService);

            await Task.WhenAll(edAdvTask, nRub_kTask, sprTask);
            
            _nRub_k = await nRub_kTask;
            _bindingSourceArtKod.DataSource = await edAdvTask;

            InitializeBindingsAsync();
            UpdateNorms();

        }
        private async Task InitializeBindingsAsync()
        {
            try
            {
                //загрузка перечня кодов из справочника общая информация
                _bindingSourceArtCommon.DataSource = await _articulEdAdvDataService.GetCommonArtByKoddAsync(this._kodd);

                #region заполнение блока основных данных артикула

                txbArticul.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(ArticulModel.Articul), true);

                txbMod.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(ArticulModel.Mod), true);

                //госты
                txbGostId.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(ArticulModel.Id_gost), true);
                lookUpGost.Properties.DataSource = CommonSpravArticulEditAdvance.Gosts;
                lookUpGost.Properties.DisplayMember = nameof(GostModel.Name_gost);
                lookUpGost.Properties.ValueMember = nameof(GostModel.Id_gost);
                lookUpGost.Properties.NullText = "Не выбрано";
                lookUpGost.DataBindings.Clear();
                lookUpGost.DataBindings.Add("EditValue", _bindingSourceArtCommon, nameof(ArticulModel.Id_gost), true);
                //инициализация описания госта
                UpdateOpi();

                #region описание lookUpGostGrup группы госта 
                //группа по гостам
                lookUpGostGrup.Properties.DataSource = _bindingSourceGostGrup;
                lookUpGostGrup.Properties.DisplayMember = nameof(GostGrupIzdViewModel.Ag_name_sokr);
                lookUpGostGrup.Properties.ValueMember = nameof(GostGrupIzdViewModel.Ag_id);
                lookUpGostGrup.Properties.NullText = "Не выбрано";
                lookUpGostGrup.DataBindings.Clear();
                lookUpGostGrup.DataBindings.Add("EditValue", _bindingSourceArtCommon, nameof(ArticulModel.Ag_id), true);

                var view = lookUpGostGrup.Properties.PopupView as DevExpress.XtraGrid.Views.Grid.GridView;
                if (view == null)
                    throw new InvalidOperationException("PopupView не GridView");
                view.OptionsView.ShowColumnHeaders = true;
                view.OptionsView.ShowIndicator = false;
                //view.OptionsView.ShowAutoFilterRow = true; // ⭐ фильтр по колонкам

                view.OptionsBehavior.Editable = false;
                view.OptionsSelection.EnableAppearanceFocusedCell = false;
                view.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;

                view.Columns.Clear();

                view.Columns.AddVisible(nameof(GostGrupIzdViewModel.Id_gost), "ГостId");
                view.Columns.AddVisible(nameof(GostGrupIzdViewModel.Ag_name_sokr), "Сокращенное название");
                view.Columns.AddVisible(nameof(GostGrupIzdViewModel.N_i), "Наименование");

                // Скрытые поля (но доступны как ValueMember)
                //view.Columns[nameof(GostGrupIzdViewModel.Ag_id)].Visible = false;
                //view.Columns[nameof(GostGrupIzdViewModel.Id_gost)].Visible = false;

                view.BestFitColumns();

                #endregion

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
                chbKruj.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.Kruj), true);

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
                //привязка полей норм
                BindFieldByName(this.layoutControlGroup4, _bindingSourceArtCommon);
                //привязка полей себестоимость 
                BindFieldByName(this.layoutControlGroup7, _bindingSourceArtCommon);
                //привязка полей Ткань
                BindFieldByNameLookUp(this.layoutControlGroup8, _bindingSourceArtCommon, CommonSpravArticulEditAdvance.Tkans);
                //привязка полей брак
                BindFieldByName(this.layoutControlGroup6, _bindingSourceArtCommon);
                //описание opis_t 
                BindFieldByName(this.layoutControlGroup10, _bindingSourceArtCommon);

                txtNorm_t.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(ArticulModel.Norm_t), true);

                #endregion
                _bindingSourceArtCommon.CurrentItemChanged += BindingSource_CurrentChanged;


            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
        }

        /// <summary>
        /// настройка внешнего вида PopupGrid для выбора ткани
        /// </summary>
        /// <param name="sle"></param>
        /// <exception cref="InvalidOperationException"></exception>
        private void ConfigurePopupGridTkan(SearchLookUpEdit sle)
        {
            var view = sle.Properties.PopupView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null)
                throw new InvalidOperationException("PopupView не GridView");
            view.OptionsView.ShowColumnHeaders = true;
            view.OptionsView.ShowIndicator = false;
            //view.OptionsView.ShowAutoFilterRow = true; // ⭐ фильтр по колонкам
            view.OptionsBehavior.Editable = false;
            view.OptionsSelection.EnableAppearanceFocusedCell = false;
            view.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            view.Columns.Clear();
            //view.Columns.AddVisible(nameof(SpArticulTkanSokr.Kod_t), "Код ткани");
            view.Columns.AddVisible(nameof(SpArticulTkanSokr.Tkan), "Ткань");
            //view.Columns.AddVisible(nameof(SpArticulTkanSokr.Tkb), "Краткое наименование");
            
            // Скрытые поля (но доступны как ValueMember)
            //view.Columns[nameof(SpArticulTkanSokr.IsDifficult)].Visible = false;
            //view.Columns[nameof(SpArticulTkanSokr.Difficult_koef)].Visible = false;
            view.BestFitColumns();
        }

        /// <summary>
        /// функция привязки полей LookUpEdit по имени контрола
        /// </summary>
        /// <param name="group"></param>
        /// <param name="bs"></param>
        /// <param name="sprav"></param>
        private async void BindFieldByNameLookUp(LayoutControlGroup group, BindingSource bs, IReadOnlyList<SpArticulTkanSokr> sprav)
        {
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
                        string propName = sle.Name[3..]; // txtKod_t1 → Kod_t1

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
                        ConfigurePopupGridTkan(sle);

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

        /// <summary>
        /// функция  привязки полей по имени контрола
        /// </summary>
        /// <param name="group"></param>
        /// <param name="bs"></param>
        private async void BindFieldByName(LayoutControlGroup group, BindingSource bs)
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

                        //настройка маски для числовых полей
                        if (pd.PropertyType == typeof(decimal) || pd.PropertyType == typeof(double) || pd.PropertyType == typeof(float))
                        {
                            ConfigureNumericMask((DevExpress.XtraEditors.TextEdit)edit);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка привязки поля " + ex.Message);
                throw;
            }
        }

        private void ConfigureNumericMask(DevExpress.XtraEditors.TextEdit edit)
        {
            edit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //edit.Properties.Mask.EditMask = "n2"; // 2 знака после запятой
            edit.Properties.Mask.UseMaskAsDisplayFormat = true;

        }
        
        private void UpdateOpi()
        {
            if (lookUpGost.EditValue is int id)
                txbOpiGost.Text = CommonSpravArticulEditAdvance.Gosts.FirstOrDefault(x => x.Id_gost == id)?.Opi_gost ?? "";
            else
                txbOpiGost.Text = "";
        }

        private void UpdateNorms()
        {
            try
            {
                var currentItem = (ArticulModel)_bindingSourceArtCommon.Current;
                if (currentItem == null)
                   return;

                var props = TypeDescriptor.GetProperties(currentItem);

                decimal sum = 0m;
                decimal norm_all = 0m;

                // Ищем все norm_tX
                var normProps = props
                    .Cast<PropertyDescriptor>()
                    .Where(p =>
                        p.PropertyType == typeof(decimal)
                        && p.Name.StartsWith("Norm_t", StringComparison.OrdinalIgnoreCase));

                foreach (var normProp in normProps)
                {
                    // получаем индекс: Norm_t1 -> 1
                    string index = normProp.Name.Substring("Norm_t".Length);

                    var sebProp = props.Find($"Seb_t{index}", true);
                    var brakProp = props.Find($"Brak_t{index}", true);
                    var kgmProp = props.Find($"K_kg_m{index}", true);

                    if (sebProp == null || brakProp == null || kgmProp == null)
                        continue;

                    decimal norm = (decimal?)normProp.GetValue(currentItem) ?? 0m;
                    decimal seb = (decimal?)sebProp.GetValue(currentItem) ?? 0m;
                    // !!!проверка на 0 брак вынесена в поле модели ArticulModel
                    decimal brak = (decimal?)brakProp.GetValue(currentItem) ?? 0m;

                    decimal kgm = (decimal?)kgmProp.GetValue(currentItem) ?? 0m;

                    sum += (norm * seb) - (brak * _nRub_k * kgm);
                    norm_all += norm;
                }

                currentItem.Norm_t = norm_all;
                txtSeb_all.EditValue = sum;

            }
            catch (Exception ex)
            { 
                _logger.LogErrorAsync(ex, "Ошибка при обновлении норм UpdateNorms");
                throw;
            }
        }


        private void BindingSource_CurrentChanged(object? sender, EventArgs e)
        {
            var currentModel = (ArticulModel)_bindingSourceArtCommon.Current;
            // отписываемся от старого объекта
            if (currentModel != null)
                currentModel.PropertyChanged -= Model_PropertyChanged;

            currentModel = _bindingSourceArtCommon.Current as ArticulModel;

            // подписываемся на новый
            if (currentModel != null)
                currentModel.PropertyChanged += Model_PropertyChanged;

            // при смене строки — пересчёт целиком
            UpdateNorms();
        }
        private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.PropertyName))
                return;

            // фильтр по нужным полям
            if (e.PropertyName.StartsWith("Norm_t", StringComparison.OrdinalIgnoreCase) ||
                e.PropertyName.StartsWith("Seb_t", StringComparison.OrdinalIgnoreCase) ||
                e.PropertyName.StartsWith("Brak_t", StringComparison.OrdinalIgnoreCase) ||
                e.PropertyName.StartsWith("K_kg_m", StringComparison.OrdinalIgnoreCase))
            {
                UpdateNorms();
            }
        }
        /// <summary>
        /// Сохранение изменений общих данных артикула для всех кодов 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveChanges(object sender, EventArgs e)
        {
            try
            {
                _bindingSourceArtCommon.EndEdit();
                _bindingSourceArtCommonSave.Clear();

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
                //принятие изменений в текущей модели
                currentItem.AcceptChanges();

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
            try
            {
                UpdateOpi();

                //oбновление фильтра группы по госту
                var currentItem = (ArticulModel)_bindingSourceArtCommon.Current;
                if (lookUpGost.EditValue is not int idGost)
                    return;
                _bindingSourceGostGrup.DataSource = CommonSpravArticulEditAdvance.GostGroupNames
                    .Where(x => x.Id_gost == idGost)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при изменении госта (lookUpGost_EditValueChanged)");
                throw;
            }
        }

        private void ArticulEditAdvance_FormClosing(object sender, FormClosingEventArgs e)
        {
            var currentItem = (ArticulModel)_bindingSourceArtCommon.Current;
            if (currentItem.IsModified)
            {
                var result = XtraMessageBox.Show("Есть несохраненные изменения. Сохранить перед закрытием?", "Подтверждение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    SaveChanges(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true; // Отменяем закрытие формы
                }
            }
        }

        private void lookUpGostGrup_EditValueChanged(object sender, EventArgs e)
        {
            //UpdateGostGrup();
            var currentItem = (ArticulModel)_bindingSourceArtCommon.Current;
            if (lookUpGostGrup.EditValue is int id)
                currentItem.Grup = CommonSpravArticulEditAdvance.GostGroupNames.FirstOrDefault(x => x.Ag_id == id)?.Ag_name_sokr ?? "";
            else
                currentItem.Grup = "";
        }
    }
}
