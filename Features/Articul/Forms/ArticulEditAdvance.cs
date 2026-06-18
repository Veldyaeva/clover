//using DevExpress.ChartRangeControlClient.Core;
using Dapper;
using DevExpress.CodeParser;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraBars.Customization;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
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
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
//using WinBindingSource = System.Windows.Forms.BindingSource;

namespace SewingProduction.Features.Articul.Forms
{
    public partial class ArticulEditAdvance : CustomForm
    {
        private DatabaseHelperSQL _dbHelper;
        private DbService _dbService;
        private static BulkHelper _bulkHelper;
        

        //private ArticulDataService _articulDataService;
        private ArticulEditAdvanceService _articulEdAdvDataService;

        private readonly ILogger _logger = new FileLogger();

        private const string PermissionEditLinkedGost = "permArticulEditLinkedGost";
        private const string PermissionEditLinkedFull = "permArticulEditLinkedFull";
        private const string PermissionModeEditor = "Редактор";

        private static readonly HashSet<string> GostEditableProperties =
            new(StringComparer.OrdinalIgnoreCase)
            {
                nameof(ArticulModel.Id_gost),
                nameof(ArticulModel.Ag_id),
                nameof(ArticulModel.Grup)
            };

        private static readonly HashSet<string> SostavEditableProperties =
            new(StringComparer.OrdinalIgnoreCase)
            {
                nameof(ArticulModel.Sost),
                nameof(ArticulModel.Sost2),
                nameof(ArticulModel.Sost3),
                nameof(ArticulModel.Sostav)
            };

        private ArticulModel _currentModel;
        private ArticulModel _originalModelSnapshot;
        private ArticulEditAccessPolicy _editPolicy = ArticulEditAccessPolicy.ReadOnly;

        private string _kodd;
        private string _kod;
        private decimal _nRub_k;
        //по умолчанию не разрешено редактирование
        private bool _linkedWithMatrix = true;
        //открытие формы редактирования размера
        private bool _openEditFormFromButton = false;


        private BindingSource _bindingSourceArtKod;
        private BindingSource _bindingSourceArtCommon;
        private BindingSource _bindingSourceArtCommonSave;
        private BindingSource _bindingSourceGostGrup;

        public ArticulEditAdvance(UserClass user) : this(user, "", "")
        { }

        public ArticulEditAdvance(UserClass user, string kodd, string articul) : base(user)
        {
            _dbHelper = new DatabaseHelperSQL();
            _dbService = new DbService(_dbHelper);
            _bulkHelper = new BulkHelper();

            _articulEdAdvDataService = new ArticulEditAdvanceService();

            InitializeComponent();
            _user = user;
            _kodd = kodd;
            this.Text = articul + " " + kodd;
          //  ThemeManager.UpdateTheme(this);

            _bindingSourceArtKod = new BindingSource { };
            if (gridEditAdRazm != null) gridEditAdRazm.DataSource = _bindingSourceArtKod;

            _bindingSourceArtCommon = new BindingSource { };
            _bindingSourceArtCommonSave = new BindingSource { };
            _bindingSourceGostGrup = new BindingSource { };

        }
        public ArticulEditAdvance(UserClass user, string kodd, string articul, string kod) : this(user, kodd, articul)
        {
            _kod = kod;
        }

        private sealed record ArticulEditAccessPolicy(
            bool CanEditBase,
            bool CanEditGost,
            bool CanEditSostav)
        {
            public static ArticulEditAccessPolicy ReadOnly { get; } = new(false, false, false);
            public bool CanEditAny => CanEditBase || CanEditGost || CanEditSostav;
            public bool CanEditAll => CanEditBase && CanEditGost && CanEditSostav;
        }


        private async void ArticulEditAdvance_Load(object sender, EventArgs e)
        {
            //загрузка списка размеров 

            var edAdvTask = _articulEdAdvDataService.GetArtByKoddAsync(this._kodd);
            var nRub_kTask = _dbHelper.ExecuteScalarAsync("SELECT dbo.getConstN('nRub_k', LEFT(CONVERT(NVARCHAR, GETDATE(), 12), 4))");

            // загрузка кешированных данных из справочников 
            var sprTask = CommonSpravArticulEditAdvance.EnsureLoadedAsync(_dbService);

            await Task.WhenAll(edAdvTask, nRub_kTask, sprTask);

            _nRub_k = nRub_kTask.Result;
            _bindingSourceArtKod.DataSource = edAdvTask.Result;

            //доступ на определенную колонку
            //gridEditAdRazm.InitializeAccess(_user, this.Name, new List<string> { "view_sp_articul" });

            await InitializeBindingsAsync();
            BindGostRazm();

            // view_sp_articul_all не включает po — восполняем из _bindingSourceArtKod.
            // После этого _currentModel.Po является единственным источником истины:
            // его читают txbPo (через биндинг) и IsSingleCodeEditMode.
            if (string.IsNullOrWhiteSpace(_currentModel?.Po))
            {
                var po = _bindingSourceArtKod.List.OfType<ArticulModel>()
                    .FirstOrDefault(x => KodMatches(x.Kod, GetSelectedKod()))
                    ?.Po;
                if (!string.IsNullOrWhiteSpace(po))
                    _currentModel!.Po = po;
            }

            // Фильтр грида: при наличии пометки — только коды с этой пометкой,
            // иначе — все не-удалённые коды группы.
            ApplyRazmFilter();

            if (IsSingleCodeEditMode())
                this.Text += $"  [по: {_currentModel!.Po.Trim()}]";

            await CheckStatusAsync();

            await InitArticulCardAsync();

            CaptureOriginalModelSnapshot();

        }

        /// <summary>
        /// Инициализация размещённого на форме ArticulControl. Контрол привязывается к тому же
        /// _bindingSourceArtCommon (SpArticulPreviewModel), что и поля формы, поэтому его правки
        /// сохраняются существующим механизмом SaveChanges без отдельного pipeline.
        /// </summary>
        private async Task InitArticulCardAsync()
        {
            try
            {
                if (_bindingSourceArtCommon.Count == 0)
                    return;

                articulControlCard.BindTo(_bindingSourceArtCommon);
                await articulControlCard.LoadImageAsync(_kodd);

                if (CanEditArticulCard())
                {
                    await articulControlCard.EnableEditModeAsync();
                    ApplyArticulCardEditPolicy();
                }
                else
                {
                    articulControlCard.SetViewMode();
                    articulControlCard.IsReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка инициализации ArticulControl на ArticulEditAdvance");
            }
        }
        private bool CanEditArticulCard()
        {
            return _editPolicy.CanEditAll || CanEditOnlyGost();
        }

        private void ApplyArticulCardEditPolicy()
        {
            if (_editPolicy.CanEditAll)
                return;

            var editableProperties = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (CanEditOnlyGost())
            {
                editableProperties.Add(nameof(ArticulModel.Id_gost));
                editableProperties.Add(nameof(ArticulModel.Ag_id));
            }

            articulControlCard.ApplyEditableFields(editableProperties);
        }

        private bool CanEditOnlyGost()
        {
            return _editPolicy.CanEditGost
                && !_editPolicy.CanEditBase
                && !_editPolicy.CanEditSostav;
        }

        private async Task CheckStatusAsync()
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "koddArt", _kodd }
                };

                //проверка стыковки в матрице с артикулом
                var matrStatustask = _dbHelper.ExecuteScalarAsync("select top 1 psa_id FROM plan_sezon_all where kodd=@koddArt ", parameters);

                await Task.WhenAll(matrStatustask);

                _linkedWithMatrix = (matrStatustask.Result != null && matrStatustask.Result > 0);

                ApplyEditPolicy();
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при проверке статуса артикула ArticulEditAdvanceService CheckStatus");
                throw;
            }

        }

        private ArticulEditAccessPolicy ResolveEditPolicy()
        {
            var hasFormEdit = HasEditPermission(nameof(ArticulEditAdvance));
            if (!hasFormEdit)
                return ArticulEditAccessPolicy.ReadOnly;

            bool canEditBase;
            bool canEditGost;
            bool canEditSostav;

            if (!_linkedWithMatrix)
            {
                canEditBase = true;
                canEditGost = true;
                canEditSostav = true;
            }
            else if (HasEditPermission(PermissionEditLinkedFull))
            {
                canEditBase = true;
                canEditGost = true;
                canEditSostav = true;
            }
            else if (HasEditPermission(PermissionEditLinkedGost))
            {
                canEditBase = false;
                canEditGost = true;
                canEditSostav = false;
            }
            else
            {
                return ArticulEditAccessPolicy.ReadOnly;
            }

            if (_currentModel?.DateOpis != null)
                canEditGost = false;

            return new ArticulEditAccessPolicy(
                CanEditBase: canEditBase,
                CanEditGost: canEditGost,
                CanEditSostav: canEditSostav);
        }

        private bool HasEditPermission(string objectName)
        {
            return _user?.HasPermission(objectName, PermissionModeEditor) == true;
        }

        private void ApplyEditPolicy()
        {
            _editPolicy = ResolveEditPolicy();

            SetGroupReadOnly(layoutCommonArticul, !_editPolicy.CanEditBase);
            SetGroupReadOnly(layoutGostInsert, !_editPolicy.CanEditGost);
            SetGroupReadOnly(layoutSostav, !_editPolicy.CanEditSostav);

            layoutGostInsert.Enabled = true;

            // Есть дата описания модели - редактирование ГОСТ остается запрещенным отдельным бизнес-правилом.
            if (_currentModel.DateOpis != null)
            {
                SetGroupReadOnly(layoutGostInsert, true);
                layoutGostInsert.Enabled = false;
                //btEdit.Visible = false;
                layoutControlItem23.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }
        private void SetGroupReadOnly(LayoutControlGroup group, bool readOnly)
        {
            foreach (BaseLayoutItem item in group.Items)
            {

                if (item is LayoutControlGroup subGroup)
                {
                    SetGroupReadOnly(subGroup, readOnly);
                    continue;
                }
                if (item is not LayoutControlItem lci)
                    continue;

                if (lci.Control is DevExpress.XtraEditors.BaseEdit edit)
                {
                    edit.Properties.ReadOnly = readOnly;
                    continue;
                }
                if (lci.Control is System.Windows.Forms.ComboBox cb)
                {
                    cb.Enabled = !readOnly;
                    continue;
                }
                if (lci.Control is System.Windows.Forms.CheckBox chb)
                {
                    chb.Enabled = !readOnly;
                    continue;
                }
            }
        }

        private async Task InitializeBindingsAsync()
        {
            try
            {
                //загрузка перечня кодов из справочника общая информация
                _bindingSourceArtCommon.DataSource = await _articulEdAdvDataService.GetCommonArtByKoddAsync(this._kodd, _kod);
                //пересчет при смене значений в модели
                WireModelOnce();

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

                var lookUpGostGrupView = lookUpGostGrup.Properties.PopupView as DevExpress.XtraGrid.Views.Grid.GridView;
                if (lookUpGostGrupView == null)
                    throw new InvalidOperationException("PopupView не GridView");
                lookUpGostGrupView.OptionsView.ShowColumnHeaders = true;
                lookUpGostGrupView.OptionsView.ShowIndicator = false;
                //lookUpGostGrupView.OptionsView.ShowAutoFilterRow = true; // ⭐ фильтр по колонкам

                lookUpGostGrupView.OptionsBehavior.Editable = false;
                lookUpGostGrupView.OptionsSelection.EnableAppearanceFocusedCell = false;
                lookUpGostGrupView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;

                lookUpGostGrupView.Columns.Clear();

                lookUpGostGrupView.Columns.AddVisible(nameof(GostGrupIzdViewModel.Id_gost), "ГостId");
                lookUpGostGrupView.Columns.AddVisible(nameof(GostGrupIzdViewModel.Ag_name_sokr), "Сокращенное название");
                lookUpGostGrupView.Columns.AddVisible(nameof(GostGrupIzdViewModel.N_i), "Наименование");

                // Скрытые поля (но доступны как ValueMember)
                //lookUpGostGrupView.Columns[nameof(GostGrupIzdViewModel.Ag_id)].Visible = false;
                //lookUpGostGrupView.Columns[nameof(GostGrupIzdViewModel.Id_gost)].Visible = false;

                lookUpGostGrupView.BestFitColumns();

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

                #region ткань

                cbTkan.Properties.DataSource = CommonSpravArticulEditAdvance.Tkans;
                cbTkan.Properties.DisplayMember = nameof(SpArticulTkanSokr.Tkan);
                cbTkan.Properties.ValueMember = nameof(SpArticulTkanSokr.Tkb);
                cbTkan.Properties.NullText = "Не выбрано";
                cbTkan.DataBindings.Clear();
                cbTkan.DataBindings.Add("EditValue", _bindingSourceArtCommon, nameof(ArticulModel.Tkb), true);

                var cbTkanView = cbTkan.Properties.PopupView as DevExpress.XtraGrid.Views.Grid.GridView;
                if (cbTkanView == null)
                    throw new InvalidOperationException("PopupView не GridView");
                cbTkanView.OptionsView.ShowColumnHeaders = true;
                cbTkanView.OptionsView.ShowIndicator = false;
                //lookUpGostGrupView.OptionsView.ShowAutoFilterRow = true; // ⭐ фильтр по колонкам

                cbTkanView.OptionsBehavior.Editable = false;
                cbTkanView.OptionsSelection.EnableAppearanceFocusedCell = false;
                cbTkanView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;

                cbTkanView.Columns.Clear();

                cbTkanView.Columns.AddVisible(nameof(SpArticulTkanSokr.Tkb), "Сокращенное наим");
                cbTkanView.Columns.AddVisible(nameof(SpArticulTkanSokr.Tkan), "Наименование");

                #endregion

                chbArh.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.Arh), true);
                chbKombDet.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.Komb_det), true);
                chbKombIzd.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.Komb_izd), true);
                chbKruj.DataBindings.Add("Checked", _bindingSourceArtCommon, nameof(ArticulModel.Kruj), true);

                //составы

                BindFieldByName(this.layoutSostav, _bindingSourceArtCommon);

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
                BindFieldByName(this.layoutNorms, _bindingSourceArtCommon);
                //привязка полей себестоимость 
                BindFieldByName(this.layoutSeb, _bindingSourceArtCommon);
                //привязка полей Ткань
                BindFieldByNameLookUp(this.layoutTkans, _bindingSourceArtCommon, CommonSpravArticulEditAdvance.Tkans);
                //привязка полей брак
                BindFieldByName(this.layoutBrak, _bindingSourceArtCommon);
                //описание opis_t 
                BindFieldByName(this.layoutOpis_t, _bindingSourceArtCommon);

                txtNorm_t.DataBindings.Add("Text", _bindingSourceArtCommon, nameof(ArticulModel.Norm_t), true);
                //зарплата
                BindFieldByName(this.layoutSumZP, _bindingSourceArtCommon);


                #endregion


            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
        }
        private async void BindGostRazm()
        {
            try
            {
                var ri = repositoryItemLookUpEdit1;
                ri.DataSource = await _articulEdAdvDataService.GetGostRazmByIDAsync(_currentModel.Id_gost);
                ri.DisplayMember = nameof(GostRazmerNabViewModel.Razm);
                ri.ValueMember = nameof(GostRazmerNabViewModel.Razm);
                // Колонки выпадающего списка (по желанию)
                ri.Columns.Clear();
                ri.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Razm", "Название"));


                ri.NullText = ""; // что показывать, если значение null
                ri.ShowHeader = false;
                ri.ShowFooter = false;
                ri.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor; // запрет ввода, только выбор
                ri.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок размера ГОСТ");
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
            //lookUpGostGrupView.OptionsView.ShowAutoFilterRow = true; // ⭐ фильтр по колонкам
            view.OptionsBehavior.Editable = false;
            view.OptionsSelection.EnableAppearanceFocusedCell = false;
            view.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            view.Columns.Clear();
            //lookUpGostGrupView.Columns.AddVisible(nameof(SpArticulTkanSokr.Kod_t), "Код ткани");
            view.Columns.AddVisible(nameof(SpArticulTkanSokr.Tkan), "Ткань");
            //lookUpGostGrupView.Columns.AddVisible(nameof(SpArticulTkanSokr.Tkb), "Краткое наименование");

            // Скрытые поля (но доступны как ValueMember)
            //lookUpGostGrupView.Columns[nameof(SpArticulTkanSokr.IsDifficult)].Visible = false;
            //lookUpGostGrupView.Columns[nameof(SpArticulTkanSokr.Difficult_koef)].Visible = false;
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
                        
                        var propTag = edit.Tag as String;
                        if (propTag == "NO")
                            continue;
                        
                        // удаление префикса txt или txb controlName.Substring(3);
                        string propName = edit.Name.Length > 3 ? edit.Name[3..] : edit.Name;

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

        private void WireModelOnce()
        {
            // отписка на всякий случай (если метод вызовут повторно)
            if (_currentModel != null)
                _currentModel.PropertyChanged -= Model_PropertyChanged;

            _currentModel = _bindingSourceArtCommon.Current as ArticulModel;

            if (_currentModel != null)
                _currentModel.PropertyChanged += Model_PropertyChanged;

            UpdateNorms(); // первый расчёт
        }
        /// <summary>
        /// метод пересчета для норм при смене текущей строки 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        private bool isChangedData()
        {
            bool modyfiedRazm = _bindingSourceArtKod.List
                        .OfType<ArticulModel>()
                        .Any(x => (x?.IsModified == true || x?.IsNew == true || x?.IsDeleted == true));

            return _currentModel.IsModified || modyfiedRazm;

        }

        private void CaptureOriginalModelSnapshot()
        {
            if (_currentModel == null)
                return;

            _originalModelSnapshot = ObjectCloneHelper.CloneWithExclusions(_currentModel);
            _originalModelSnapshot.AcceptChanges();
            _currentModel.AcceptChanges();
        }

        private bool ValidateSaveAllowedByPolicy()
        {
            if (_editPolicy.CanEditAll)
                return true;

            if (_originalModelSnapshot == null)
            {
                XtraMessageBox.Show(
                    "Не удалось проверить права на сохранение: исходное состояние артикула не зафиксировано.",
                    "Сохранение запрещено",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            var deniedProperties = GetChangedPersistedProperties(_originalModelSnapshot, _currentModel)
                .Where(propertyName => !IsPropertyAllowedByPolicy(propertyName))
                .ToList();

            if (HasRazmChanges() && !_editPolicy.CanEditBase)
                deniedProperties.Insert(0, "размеры/коды");

            if (deniedProperties.Count == 0)
                return true;

            var visibleNames = string.Join(", ", deniedProperties.Distinct().Take(8));
            if (deniedProperties.Count > 8)
                visibleNames += ", ...";

            XtraMessageBox.Show(
                $"Недостаточно прав для сохранения изменений: {visibleNames}.",
                "Сохранение запрещено",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return false;
        }

        /// <summary>
        /// Пометка на конкретном коде: сохраняем только его, не распространяя на группу.
        /// </summary>
        private bool IsSingleCodeEditMode()
            => !string.IsNullOrWhiteSpace(_currentModel?.Po);

        private string GetSelectedKod()
        {
            return !string.IsNullOrWhiteSpace(_kod)
                ? _kod
                : _currentModel?.Kod;
        }

        private bool HasRazmChanges()
        {
            var items = _bindingSourceArtKod.List.OfType<ArticulModel>();
            if (IsSingleCodeEditMode())
                items = items.Where(IsSelectedSingleCode);
            return items.Any(x => x?.IsModified == true || x?.IsNew == true || x?.IsDeleted == true);
        }

        /// <summary>
        /// Устанавливает фильтр грида размеров:
        /// при наличии пометки — только коды этой пометки, иначе — все не-удалённые.
        /// </summary>
        private void ApplyRazmFilter()
        {
            if (IsSingleCodeEditMode())
            {
                var selectedKod = GetSelectedKod();
                gridViewEditAdvRazm.ActiveFilterCriteria =
                    DevExpress.Data.Filtering.CriteriaOperator.And(
                        new DevExpress.Data.Filtering.BinaryOperator("IsDeleted", false),
                        !string.IsNullOrWhiteSpace(selectedKod)
                            ? BuildTrimEqualsCriteria(nameof(ArticulModel.Kod), selectedKod)
                            : BuildTrimEqualsCriteria(nameof(ArticulModel.Po), _currentModel!.Po));
            }
            else
            {
                gridViewEditAdvRazm.ActiveFilterString = "[IsDeleted] = false";
            }
        }

        private static DevExpress.Data.Filtering.CriteriaOperator BuildTrimEqualsCriteria(string propertyName, string value)
        {
            return new DevExpress.Data.Filtering.BinaryOperator(
                new DevExpress.Data.Filtering.FunctionOperator(
                    DevExpress.Data.Filtering.FunctionOperatorType.Trim,
                    new DevExpress.Data.Filtering.OperandProperty(propertyName)),
                new DevExpress.Data.Filtering.OperandValue(value?.Trim() ?? string.Empty),
                DevExpress.Data.Filtering.BinaryOperatorType.Equal);
        }

        private bool IsSelectedSingleCode(ArticulModel item)
        {
            var selectedKod = GetSelectedKod();
            if (!string.IsNullOrWhiteSpace(selectedKod))
                return KodMatches(item.Kod, selectedKod);

            return PoMatches(item.Po, _currentModel?.Po);
        }

        private static bool KodMatches(string? itemKod, string? selectedKod)
            => string.Equals(itemKod?.Trim(), selectedKod?.Trim(), StringComparison.OrdinalIgnoreCase);

        private static bool PoMatches(string? itemPo, string? modelPo)
            => string.Equals(itemPo?.Trim(), modelPo?.Trim(), StringComparison.Ordinal);

        private bool IsPropertyAllowedByPolicy(string propertyName)
        {
            if (_editPolicy.CanEditGost && GostEditableProperties.Contains(propertyName))
                return true;

            if (_editPolicy.CanEditSostav && SostavEditableProperties.Contains(propertyName))
                return true;

            if (_editPolicy.CanEditBase
                && !GostEditableProperties.Contains(propertyName)
                && !SostavEditableProperties.Contains(propertyName))
                return true;

            return false;
        }

        private static IEnumerable<string> GetChangedPersistedProperties(ArticulModel original, ArticulModel current)
        {
            foreach (var property in typeof(ArticulModel).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!property.CanRead || property.GetIndexParameters().Length > 0)
                    continue;

                if (property.GetCustomAttribute<NotMappedAttribute>() != null)
                    continue;

                var originalValue = property.GetValue(original);
                var currentValue = property.GetValue(current);

                if (!Equals(originalValue, currentValue))
                    yield return property.Name;
            }
        }

        private async void SaveChanges(object sender, EventArgs e)
        {
            try
            {
                _bindingSourceArtCommon.EndEdit();
                _bindingSourceArtKod.EndEdit();

                _bindingSourceArtCommonSave.Clear();
                //проверка на изменения
                if (!isChangedData())
                {
                    return;
                }

                if (!ValidateSaveAllowedByPolicy())
                {
                    return;
                }

                bool singleMode = IsSingleCodeEditMode();

                for (int i = 0; i < _bindingSourceArtKod.Count; i++)
                {
                    var item = (ArticulModel)_bindingSourceArtKod[i];

                    // Если на коде стоит пометка — сохраняем только его,
                    // не распространяя изменения на коды с другой пометкой.
                    if (singleMode && !IsSelectedSingleCode(item))
                        continue;

                    var newItem = ObjectCloneHelper.CloneWithExclusions(_currentModel, clone =>
                    {
                        clone.Kod = item.Kod;
                        clone.Razm = item.Razm;
                        clone.Po = item.Po;
                        clone.IsModified = (_currentModel.IsModified || item.IsModified);
                        clone.IsNew = item.IsNew;
                        clone.IsDeleted = item.IsDeleted;

                    }, "Kod");
                    _bindingSourceArtCommonSave.Add(newItem);
                }

                List<ArticulModel> filteredList = _bindingSourceArtCommonSave.List
                .OfType<ArticulModel>()
                .ToList();

                using (SqlConnection connection = _dbHelper.GetConnection())
                {
                    _bulkHelper.BulkAllDataUpdate<ArticulModel>(connection, filteredList, "sp_articul", new[] { "kod" });
                }
                _bindingSourceArtCommonSave.Clear();


                XtraMessageBox.Show("Изменения успешно сохранены.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CaptureOriginalModelSnapshot();

            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при сохранении изменений артикула");
                XtraMessageBox.Show("Ошибка при сохранении изменений артикула: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void lookUpGost_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateOpi();

                //oбновление фильтра группы по госту
                if (lookUpGost.EditValue is not int idGost)
                    return;
                _bindingSourceGostGrup.DataSource = CommonSpravArticulEditAdvance.GostGroupNames
                    .Where(x => x.Id_gost == idGost)
                    .ToList();

                BindGostRazm();
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при изменении госта (lookUpGost_EditValueChanged)");
                throw;
            }
        }

        private void ArticulEditAdvance_FormClosing(object sender, FormClosingEventArgs e)
        {
            //var _currentItem = (ArticulModel)_bindingSourceArtCommon.Current;
            if (isChangedData())
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
            //var currentItem = (ArticulModel)_bindingSourceArtCommon.Current;

            if (lookUpGostGrup.EditValue is int id)
                _currentModel.Grup = CommonSpravArticulEditAdvance.GostGroupNames.FirstOrDefault(x => x.Ag_id == id)?.Ag_name_sokr ?? "";
            else
                _currentModel.Grup = "";
        }

        private void ArticulEditAdvance_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_currentModel != null)
                _currentModel.PropertyChanged -= Model_PropertyChanged;
        }

        private void txbGostId_TextChanged(object sender, EventArgs e)
        {
            lookUpGost_EditValueChanged(sender, e);
        }

        #region permissions for Add \ Edit \ Del  Button Razm
        private void gridViewEditAdvRazm_EditFormShowing(object sender, EditFormShowingEventArgs e)
        {
            if (!_openEditFormFromButton)
            {
                e.Allow = false;   // запретить двойной клик / Enter / F2 / программные вызовы
                return;
            }
        }
        private void gridViewEditAdvRazm_EditFormHidden(object sender, EditFormHiddenEventArgs e)
        {
            _openEditFormFromButton = false;
            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;

            var row = view.GetFocusedRow() as ArticulModel;
            if (row == null) return;

            // Нажали "Отмена"
            if (e.Result == DevExpress.XtraGrid.Views.Grid.EditFormResult.Cancel)
            {
                if (row.IsNew)
                {
                    _bindingSourceArtKod.Remove(row);
                }
                //else
                //{
                //    row.IsModified = false;
                //}
            }

        }

        private void gridViewEditAdvRazm_EditFormPrepared(object sender, DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventArgs e)
        {

            var cur = (ArticulModel)_bindingSourceArtKod.Current;

            // найдём контрол, связанный с колонкой kod, и заблокируем/разблокируем
            foreach (Control c in e.BindableControls)
            {
                if (c.DataBindings.Cast<System.Windows.Forms.Binding>()
                      .Any(b => b.BindingMemberInfo.BindingField == gcEditKod.FieldName))
                {
                    if (c is DevExpress.XtraEditors.BaseEdit be)
                        be.Properties.ReadOnly = !cur.IsNew;
                    else
                        c.Enabled = cur.IsNew;

                    break;
                }
            }
        }

        private void gridView_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        
        private void gridViewEditAdvRazm_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var row = e.Row as ArticulModel;
            if (row == null) return;
            
            var (ok, err) = isKodAllowed(row, row.Kod);
            if (!ok)
            {
                e.Valid = false;
                gridViewEditAdvRazm.SetColumnError(gcEditKod, err);

                if (row.IsNew)
                {
                    BeginInvoke(new Action(() =>
                    {
                        _bindingSourceArtKod.Remove(row);
                    }));
                }
            }
        }
        /// <summary>
        /// проверка введенного нового кода 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="kod"></param>
        /// <returns></returns>
        private ( bool ok, string Error) isKodAllowed(ArticulModel row, string kod)
        {

            if (string.IsNullOrEmpty(kod))
                return (false, "Код не может быть пустым");
            bool exists = _bindingSourceArtKod.List
                .OfType<ArticulModel>()
                .Any(x => !ReferenceEquals(x, row)
                       && !x.IsDeleted
                       && string.Equals(x.Kod?.Trim(), kod, StringComparison.OrdinalIgnoreCase));

            if (exists)
            {
                return (false,$"Kod '{kod}' уже существует.");
            }
            if (kod.Length != 8)
            {
                return (false, $"Код должен быть длиной 8 символов");
            }
            if (kod[..7] != row.Ko)
            {
                return (false, $"Первые 7 символов кода должны соответствовать коду артикула: {row.Ko}");
            }
            return (true,"");
        }


        #endregion
        private void btEdit_Click(object sender, EventArgs e)
        {
            var view = gridEditAdRazm.FocusedView as GridView;
            if (view == null) return;

            _openEditFormFromButton = true;
            view.ShowEditForm();

        }

        private void btAdd_Click(object sender, EventArgs e)
        {

            // открыть форму редактирования для новой строки
            _openEditFormFromButton = true;
            _bindingSourceArtKod.EndEdit();

            // присвоение новых значений происходит в событии gridViewEditAdvRazm_InitNewRow
            gridViewEditAdvRazm.AddNewRow();

            gridViewEditAdvRazm.ShowEditForm();

            _bindingSourceArtKod.ResetCurrentItem();

        }
        private void gridViewEditAdvRazm_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridViewEditAdvRazm.SetRowCellValue(e.RowHandle, gcEditKod, _currentModel.Kod);

            var row = gridViewEditAdvRazm.GetRow(e.RowHandle) as ArticulModel;
            if (row != null)
            {
                row.IsNew = true;
                row.Po = _currentModel?.Po ?? string.Empty;
                row.Ko = _currentModel.Ko;
            }

        }
        private async void btDel_Click(object sender, EventArgs e)
        {
            if (_bindingSourceArtKod.Current is ArticulModel cur)
            {
                var result = XtraMessageBox.Show($"Удалить код {cur.Kod} ?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    var p = new DynamicParameters();
                    p.Add("@kod", cur.Kod);

                    var res = await _dbService.ExecuteSpWithStatusAsync("dbo.kodArticulisUsed", p);

                    if (!res.IsOk)
                    {
                        XtraMessageBox.Show($"Невозможно удалить код {cur.Kod}. {res.MessageError}.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }


                if (cur.IsNew)
                    {
                        _bindingSourceArtKod.RemoveCurrent();
                    }
                    else
                    {
                        cur.IsDeleted = true;
                        cur.IsModified = false;
                        _bindingSourceArtKod.ResetCurrentItem();
                    }
                    _bindingSourceArtKod.EndEdit();
                }
            }
        }

    }
