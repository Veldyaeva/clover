using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using BindingSource = System.Windows.Forms.BindingSource;
using SewingProduction.form.TeamWork.Forms;
using DevExpress.XtraGrid.Views.Base;
using System.Collections.Generic;
using DevExpress.CodeParser;
using static SewingProduction.form.TeamWork.Forms.norm_raskrNew;
using System.Linq;
using System.Reflection;
using SewingProduction.Interfaces;
using System.Data.SqlClient;
using Z.Dapper.Plus;
using System.Drawing;
using System.Diagnostics;
using Dapper;
using DevExpress.Utils;
using DevExpress.XtraExport.Helpers;
using MethodInvoker = System.Windows.Forms.MethodInvoker;

namespace SewingProduction.form
{
    public partial class TeamWork_AdvanceTW : CustomForm
    {
        private readonly DbService _dbService;
        private readonly ArtNormService _artNormService;
        private int _bufferWorkDivision;
        private readonly DatabaseHelper _dbHelper;
        private readonly TWGridHelper _gridHelper = new TWGridHelper();
        private int _newAnnId = -1;
        private int _selectedAnnId = -1;
        private readonly ILogger _logger = new FileLogger();
        private int _mode;
        private ArtNormN _currentAnnData;
        private ArtNormN _originalAnnData;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MyDataART InitialArtData { get; set; }
        private readonly Debouncer _sekDebouncer = new Debouncer();
        private NormRasz _originalNormRaszDataBeforeEdit;


        private BindingList<NormRasz> _normRaszList;
        private BindingSource _normRaszBindingSource;
        private BindingList<NormRask> _normRaskList;
        private BindingSource _normRaskBindingSource;
        private BindingList<NormKont> _normKontList;
        private BindingSource _normKontBindingSource;
        private static List<FioModel> _cachedFioData;
        private bool _isCustomEditFormOpen = false;
        private bool _okPressed = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsRaszInserted { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsRaskInserted { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsKontInserted { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDopObrInserted { get; private set; }
        private readonly List<int> _deletedNormRaszIds = new List<int>();
        private readonly List<int> _deletedNormRaskIds = new List<int>();
        private readonly List<int> _deletedNormKontIds = new List<int>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [NotifyParentProperty(true)]
        public ArtNormN CreatedAnn { get; private set; }

        private bool _isSelectionFormOpen = false;
        private bool _hasUnsavedChanges = false;
        private List<KodProizvModel> kodProizvList;
        private List<PodrVyazModel> podrVyazList;
        private List<OborudShvModel> oborudShvList;

        #region Highlighting
        private readonly Color _highlightColor = Color.LightYellow;
        private Dictionary<Control, Color> _originalColors = new Dictionary<Control, Color>();

        private void HighlightControl(Control control)
        {
            if (!_originalColors.ContainsKey(control))
            {
                 _originalColors[control] = control.BackColor;
            }
            control.BackColor = _highlightColor;
        }

        private void UnhighlightControl(Control control)
        {
            if (_originalColors.TryGetValue(control, out Color originalColor))
            {
                 control.BackColor = originalColor; 
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newId">Id новой записи</param>
        /// <param name="oldId">Id исходной записи</param>
        /// <param name="bufferWorkDivision">Id из буфера</param>
        /// <param name="mode">режим</param>
        public TeamWork_AdvanceTW(int bufferWorkDivision, int mode, int? newId = null ,int? oldId = null)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _dbService = new DbService(_dbHelper);
            _artNormService = new ArtNormService(_dbHelper);
            ThemeManager.UpdateTheme(this);

            if (!oldId.HasValue)
                _selectedAnnId =_newAnnId =newId.Value;
            if (!newId.HasValue)
                _selectedAnnId = _newAnnId = oldId.Value;
            if (oldId.HasValue&&newId.HasValue)
            {
                _newAnnId = newId.Value;
                _selectedAnnId = oldId.Value;
            }
            _bufferWorkDivision = bufferWorkDivision;
            _mode = mode;
        }

        private void AttachChangeHandlers()
        {
            nameTextBox.TextChanged += HandleAnnDataChange;
            groupTextBox.TextChanged += HandleAnnDataChange;
            modelTextBox.TextChanged += HandleAnnDataChange;
            secTimeTextBox.TextChanged += HandleAnnDataChange;
            textBoxKomment.TextChanged += HandleAnnDataChange;
            textBoxReco.TextChanged += HandleAnnDataChange;
            dateCreate.ValueChanged += HandleAnnDataChange;
            designerComboBox.EditValueChanged += HandleAnnDataChange;
            constructorComboBox.EditValueChanged += HandleAnnDataChange;
        }

        // Общий обработчик изменений данных ANN
        private void HandleAnnDataChange(object sender, EventArgs e)
        {
            _hasUnsavedChanges = true;
            var control = sender as Control;
            if (control == null || _originalAnnData == null || _currentAnnData == null)
                return;

            string propertyName = GetPropertyNameFromControl(control);
            if (string.IsNullOrEmpty(propertyName))
                return;

            try
            {
                var currentPropInfo = _currentAnnData.GetType().GetProperty(propertyName);
                var originalPropInfo = _originalAnnData.GetType().GetProperty(propertyName);

                if (currentPropInfo == null || originalPropInfo == null)
                    return;

                object currentValue = currentPropInfo.GetValue(_currentAnnData);
                object originalValue = originalPropInfo.GetValue(_originalAnnData);

                // Сравниваем значения (учитываем null)
                bool areEqual = Equals(currentValue, originalValue);

                if (!areEqual)
                {
                    HighlightControl(control);
                }
                else
                {
                    UnhighlightControl(control);
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, $"Ошибка при сравнении значений для контроля {control.Name}, свойства {propertyName}");
                // Не прерываем работу, просто логгируем
            }
        }

        // Вспомогательный метод для получения имени свойства по элементу управления
        private string GetPropertyNameFromControl(Control control)
        {
            if (control == nameTextBox) return nameof(ArtNormN.Articul);
            if (control == groupTextBox) return nameof(ArtNormN.grup);
            if (control == modelTextBox) return nameof(ArtNormN.Mod);
            if (control == secTimeTextBox) return nameof(ArtNormN.Sek);
            if (control == textBoxKomment) return nameof(ArtNormN.Komment);
            if (control == textBoxReco) return nameof(ArtNormN.Reco);
            if (control == dateCreate) return nameof(ArtNormN.dateCreate); 
            if (control == designerComboBox) return nameof(ArtNormN.Diz);
            if (control == constructorComboBox) return nameof(ArtNormN.Constr);
            return null;
        }

        /// <summary>
        /// Инициализирует привязки для связанных таблиц
        /// </summary>
        private async Task InitializeBindingsAsync()
        {
            try
            {
                    _normRaszList = new BindingList<NormRasz>();
                    _normRaszBindingSource = new BindingSource { DataSource = _normRaszList };
                    _normRaskList = new BindingList<NormRask>();
                    _normRaskBindingSource = new BindingSource { DataSource = _normRaskList };
                    _normKontList = new BindingList<NormKont>();
                    _normKontBindingSource = new BindingSource { DataSource = _normKontList };

                gridControlRasz.DataSource = _normRaszBindingSource;
                gridControlRaskr.DataSource = _normRaskBindingSource;
                gridControlKont.DataSource = _normKontBindingSource;
                _currentAnnData = new ArtNormN();
                bindingSource1.DataSource = _currentAnnData;

                nameTextBox.DataBindings.Add("Text", bindingSource1, nameof(ArtNormN.Articul), true, DataSourceUpdateMode.OnPropertyChanged);
                groupTextBox.DataBindings.Add("Text", bindingSource1, nameof(ArtNormN.grup), true, DataSourceUpdateMode.OnPropertyChanged);
                modelTextBox.DataBindings.Add("Text", bindingSource1, nameof(ArtNormN.Mod), true, DataSourceUpdateMode.OnPropertyChanged);
                secTimeTextBox.DataBindings.Add("Text", bindingSource1, nameof(ArtNormN.Sek), true, DataSourceUpdateMode.OnPropertyChanged);
                textBoxKomment.DataBindings.Add("Text", bindingSource1, nameof(ArtNormN.Komment), true, DataSourceUpdateMode.OnPropertyChanged);
                textBoxReco.DataBindings.Add("Text", bindingSource1, nameof(ArtNormN.Reco), true, DataSourceUpdateMode.OnPropertyChanged);
                dateCreate.DataBindings.Add("Value", bindingSource1, nameof(ArtNormN.dateCreate), true, DataSourceUpdateMode.OnPropertyChanged);
                // Сортируем каждую таблицу отдельно
                TWGridHelper.sortGridView(gridViewRasz);
                TWGridHelper.sortGridView(gridViewRaskr);
                TWGridHelper.sortGridView(gridViewKont);

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
            finally
            {
                _normRaszList.ListChanged -= OnNormRaszListChanged; // защитная отписка
                _normRaszList.ListChanged += OnNormRaszListChanged;

                _normRaszList.ListChanged += OnDataChanged;
                _normRaskList.ListChanged += OnDataChanged;
                _normKontList.ListChanged += OnDataChanged;
                _normRaskList.ListChanged += (_, __) => _sekDebouncer.Debounce(500, async () => { if (_newAnnId > 0) RecalculateSek(); });
                _normKontList.ListChanged += (_, __) => _sekDebouncer.Debounce(500, async () => { if (_newAnnId > 0) RecalculateSek(); });
                if (_mode == (int)Mode.ArchAndCopy || _mode == (int)Mode.NewWorkDivision)
                {
                    AttachDeleteContextMenu(gridViewRasz, _normRaszList, r => r.nrId, _deletedNormRaszIds);
                    AttachDeleteContextMenu(gridViewRaskr, _normRaskList, r => r.id, _deletedNormRaskIds);
                    AttachDeleteContextMenu(gridViewKont, _normKontList, k => k.nkId, _deletedNormKontIds);
                }

            }
        }
        private void AttachDeleteContextMenu<T>(GridView view, BindingList<T> bindingList, Func<T, int> getId = null, List<int> deletedIds = null)
                    where T : class
        {
            view.PopupMenuShowing += (s, e) =>
            {
                if (e.MenuType != GridMenuType.Row)
                    return;

                var menu = e.Menu;
                var deleteItem = new DevExpress.Utils.Menu.DXMenuItem("Удалить строку", (_, __) =>
                {
                    int rowHandle = e.HitInfo.RowHandle;
                    if (view.IsValidRowHandle(rowHandle))
                    {
                        var rowObj = view.GetRow(rowHandle) as T;
                        if (rowObj != null)
                        {
                            if (getId != null && deletedIds != null)
                            {
                                int id = getId(rowObj);
                                if (id > 0) deletedIds.Add(id);
                            }

                            bindingList.Remove(rowObj); // удаляем из источника
                            view.DeleteRow(rowHandle);  // удаляем визуально
                        }
                    }
                });

                menu.Items.Add(deleteItem);
            };
        }


        private void OnNormRaszListChanged(object sender, ListChangedEventArgs e)
        {
            // Запускаем отложенный пересчёт Sek
            _sekDebouncer.Debounce(500, async () =>
            {
                 RecalculateSek();
            });

            OnDataChanged(sender, e); 
        }
        private async void TeamWork_AdvanceTW_Load(object sender, EventArgs e)
        {
            try
            {
                Task gridTask = Task.Run(() =>
                {
                    _gridHelper.LoadGridViewSettings(gridViewRaskr, "AdvanceTW_gridViewRaskrLayout.xml");
                    _gridHelper.LoadGridViewSettings(gridViewKont, "AdvanceTW_gridViewKontLayout.xml");
                    _gridHelper.LoadGridViewSettings(gridViewRasz, "AdvanceTW_gridViewRaszLayout.xml");
                });
                
                Task comboBoxTask = LoadAndBindFioListsAsync();
                Task bindingsTask = InitializeBindingsAsync();

                await Task.WhenAll(gridTask, comboBoxTask, bindingsTask);

                if (gridViewKont != null && gridViewKont.Columns["Text"] != null)
                {
                    gridViewKont.Columns["Text"].OptionsColumn.AllowEdit = false;
                }

                await WorkDivisionLoadAsync(caller: "DataLoad", _selectedAnnId);
                await LoadAnnDataAsync();
                if (_bufferWorkDivision > 0)
                {
                    var annData = await _artNormService.GetArtNormDataById(_bufferWorkDivision);
                    if (annData != null)
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            textBoxBuffer.Text = $"группа: {annData.grup.TrimEnd(' ')}, \r" +
                                                 $"модель: {annData.Mod.TrimEnd(' ')}, \r" +
                                                 $"артикул: {annData.Articul.TrimEnd(' ')}";
                        }));
                    }
                }

                switch (_mode)
                {
                    case (int)Mode.NewWorkDivision: this.Text = "Добавить предварительное"; nameTextBox.Enabled = true;// break;

                        // Заполняем поля, если переданы начальные данные из MyDataART
                        // _currentAnnData должен быть уже инициализирован (например, в InitializeBindingsAsync)
                        if (InitialArtData != null && _currentAnnData != null)
                        {
                            bool dataChangedByInitialValues = false;

                            if (!string.IsNullOrEmpty(InitialArtData.Articul))
                            {
                                _currentAnnData.Articul = InitialArtData.Articul;
                                dataChangedByInitialValues = true;
                            }
                            // Используем свойство 'grup' из MyDataART (с маленькой буквы)
                            // Свойство в ArtNormN (_currentAnnData) также 'grup' (с маленькой буквы)
                            if (!string.IsNullOrEmpty(InitialArtData.grup))
                            {
                                _currentAnnData.grup = InitialArtData.grup;
                                dataChangedByInitialValues = true;
                            }
                            // Используем свойство 'mod' из MyDataART (с маленькой буквы)
                            // Свойство в ArtNormN (_currentAnnData) - 'Mod' (с большой буквы)
                            if (!string.IsNullOrEmpty(InitialArtData.mod))
                            {
                                _currentAnnData.Mod = InitialArtData.mod;
                                dataChangedByInitialValues = true;
                            }

                            if (dataChangedByInitialValues && bindingSource1.DataSource == _currentAnnData)
                            {
                                // Обновляем привязанные контролы
                                bindingSource1.ResetBindings(false);
                            }
                        }
                        break;
                    case (int)Mode.ArchAndCopy: this.Text = "Архив+копия"; break;
                    case (int)Mode.Edit: this.Text = "Редактировать"; break;
                }
                _hasUnsavedChanges = false;

                AttachChangeHandlers();

                kodProizvList = await _dbService.GetListAsync<KodProizvModel>("SELECT kod_proizv, text_proizv FROM kod_proizv", null);
                podrVyazList = await _dbService.GetListAsync<PodrVyazModel>("SELECT kod_vyaz, text_vyaz FROM podr_vyaz", null);
                oborudShvList = await _dbService.GetListAsync<OborudShvModel>("SELECT kod_ob_all as kod_ob, text_ob FROM oborud_shv_ob", null);

                designerComboBox.DataBindings.Clear(); 
                constructorComboBox.DataBindings.Clear();

                designerComboBox.DataBindings.Add("EditValue", bindingSource1, nameof(ArtNormN.Diz), true, DataSourceUpdateMode.OnPropertyChanged);
                constructorComboBox.DataBindings.Add("EditValue", bindingSource1, nameof(ArtNormN.Constr), true, DataSourceUpdateMode.OnPropertyChanged);

                // Подписываем таблицы на обработчик RowStyle
                gridViewRasz.RowStyle += GridView_RowStyle;
                gridViewRaskr.RowStyle += GridView_RowStyle;
                gridViewKont.RowStyle += GridView_RowStyle;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке формы TeamWork_AdvanceTW");
            }
        }

        private async Task LoadAndBindFioListsAsync()
        {
            try
            {
                if (_cachedFioData == null)
                {
                    var fioData = await _artNormService.GetRelDesigner();
                    if (fioData != null && fioData.Count > 0)
                    {
                        _cachedFioData = new List<FioModel>(fioData); 
                        await _logger.LogEventAsync("FIO загружено и закешировано", "LoadAndBindFioListsAsync");
                    }
                    else
                    {
                        await _logger.LogEventAsync("Пустой список FIO", "LoadAndBindFioListsAsync");
                        return;
                    }
                }

                // Обновляем данные в существующих источниках привязки
                await this.InvokeAsync(() =>
                {
                    designerBindingSource.DataSource = new List<FioModel>(_cachedFioData);
                    constructorBindingSource.DataSource = new List<FioModel>(_cachedFioData);

                    _gridHelper.ConfigureComboBox(designerComboBox, designerBindingSource);
                    _gridHelper.ConfigureComboBox(constructorComboBox, constructorBindingSource);
                });
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке или привязке списка ФИО");
                // MessageBox.Show("Не удалось загрузить список сотрудников.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Загрузка данных из буфера (NormRasz, NormRask и ANN) с параллельной обработкой.
        /// </summary>
        private async Task WorkDivisionLoadAsync(string caller, int id)
        {
            if (id <= 0)
                return;
            try
            {
                // Загружаем все данные параллельно
                var raszTask = _artNormService.GetRelatedNormRasz(id);
                var raskTask = _artNormService.GetRelatedNormRask(id);
                var kontTask = _artNormService.GetRelatedNormKont(id);
                var annDataTask = _artNormService.GetArtNormDataById(id);

                await Task.WhenAll(raszTask, raskTask, kontTask, annDataTask);

                var kod_proizv = await _artNormService.GetKod_proizv();
                var podr_vyaz = await _artNormService.GetPodr_vyaz();
                var oborud_shv = await _artNormService.GetOborud_shv();

                await this.InvokeAsync(() =>
                {
                    // Подгружаем справочники в репозитории
                    repositoryItemLookUpEdit_kod_proizv.DataSource = kod_proizv;
                    repositoryItemLookUpEdit_podrVyaz.DataSource = podr_vyaz;
                    repositoryItemLookUpEdit_oborudShv.DataSource = oborud_shv;

                    repositoryItemLookUpEdit_kod_proizv.ValueMember = nameof(KodProizvModel.kod_proizv);
                    repositoryItemLookUpEdit_kod_proizv.DisplayMember = nameof(KodProizvModel.text_proizv);
                    repositoryItemLookUpEdit_podrVyaz.ValueMember = nameof(PodrVyazModel.kod_vyaz);
                    repositoryItemLookUpEdit_podrVyaz.DisplayMember = nameof(PodrVyazModel.text_vyaz);
                    repositoryItemLookUpEdit_oborudShv.ValueMember = nameof(OborudShvModel.kod_ob);
                    repositoryItemLookUpEdit_oborudShv.DisplayMember = nameof(OborudShvModel.text_ob);
                    //foreach (var r in _normRaszList)
                    //{
                    //    r.TextProizv = kodProizvList.FirstOrDefault(x => x.kod_proizv == r.KodProizv)?.text_proizv;
                    //    r.TextVyaz = podrVyazList.FirstOrDefault(x => x.kod_vyaz == r.KodPodr)?.text_vyaz;
                    //    r.TextOb = oborudShvList.FirstOrDefault(x => x.kod_ob == r.KodOb)?.text_ob;
                    //}



                    // Обновляем списки
                    _normRaszList.Clear();
                    _normRaskList.Clear();
                    _normKontList.Clear();

                    bool isCopyOrBuffer = _mode == (int)Mode.ArchAndCopy || caller == "buffer";

                    LoadList(raszTask.Result, _normRaszList, nameof(NormRasz.nrId), isCopyOrBuffer);
                    LoadList(raskTask.Result, _normRaskList, nameof(NormRask.id), isCopyOrBuffer);
                    LoadList(kontTask.Result, _normKontList, nameof(NormKont.nkId), isCopyOrBuffer);

                    // Обновляем привязки
                    _normRaszBindingSource.ResetBindings(false);
                    _normRaskBindingSource.ResetBindings(false);
                    _normKontBindingSource.ResetBindings(false);

                    // Обновляем данные ANN через биндинг
                    var annData = annDataTask.Result;
                    if (annData != null)
                    {
                        _currentAnnData = annData;
                        bindingSource1.DataSource = _currentAnnData;
                        //bindingSource1.ResetBindings(false);
                        // Если загрузка из буфера, обновим и оригинал для сравнения
                        if (isCopyOrBuffer) 
                        {
                             _originalAnnData = _currentAnnData?.Clone();
                             _hasUnsavedChanges = false; // Сброс флага после вставки из буфера
                        }
                    }
                    gridColumn4.ColumnEdit = repositoryItemLookUpEdit_kod_proizv;
                    Kod_podr.ColumnEdit = repositoryItemLookUpEdit_podrVyaz;
                    Kod_ob.ColumnEdit = repositoryItemLookUpEdit_oborudShv;

                });
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке данных ANN в WorkDivisionLoadAsync");

                await this.InvokeAsync(() =>
                {
                    MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
        }
        private void LoadList<T>(List<T> sourceList, BindingList<T> targetList, string idFieldName, bool isCopyOrBuffer) where T : INewable
        {
            foreach (var item in sourceList)
            {
                var idProp = typeof(T).GetProperty(idFieldName);

                if (isCopyOrBuffer)
                {
                    // При Архив+Копия или вставке из буфера: сбрасываем ID, ставим IsNew
                    if (idProp != null && idProp.PropertyType == typeof(int))
                    {
                        idProp.SetValue(item, 0);
                    }
                    item.IsNew = true;
                }
                else
                {
                    // При обычной загрузке для редактирования
                    item.IsNew = false;
                }

                targetList.Add(item);
            }
        }

        /// <summary>
        /// Загрузка данных ANN по ID с групповым обновлением UI.
        /// </summary>
        private async Task LoadAnnDataAsync()
        {
            try
            {
                // При архивировании нам нужны данные из _selectedAnnId
                int idToLoad = _mode == (int)Mode.ArchAndCopy ? _selectedAnnId : _newAnnId;
                
                await _logger.LogEventAsync($"Загрузка данных ANN. Mode: {_mode}, ID: {idToLoad}", "LoadAnnDataAsync");
                
                var annData = await _artNormService.GetArtNormDataById(idToLoad);
                if (annData != null)
                {
                    await _logger.LogEventAsync($"Получены данные ANN: Status={annData.Status}, Articul={annData.Articul}", "LoadAnnDataAsync");
                    
                    await this.InvokeAsync(() =>
                    {
                        _currentAnnData = annData;                // Обновляем текущую модель
                        //bindingSource1.DataSource = _currentAnnData; // Привязываем данные к форме
                        //bindingSource1.ResetBindings(false);
                        bindingSource1.SuspendBinding();
                        bindingSource1.DataSource = _currentAnnData;
                        bindingSource1.ResumeBinding();
                        bindingSource1.ResetBindings(true);
                        // Сохраняем копию для сравнения
                        _originalAnnData = _currentAnnData?.Clone(); 
                         _hasUnsavedChanges = false; // Сбрасываем флаг после загрузки оригинала
                    });

                    await _logger.LogEventAsync($"Данные ANN успешно загружены для ID {idToLoad}", "LoadAnnDataAsync");
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные ANN для ID {idToLoad}", "LoadAnnDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных ANN для ID {_selectedAnnId}");
            }
        }

        private void RecalculateSek()
        {
            try
            {
                var rasz = _normRaszList?.Where(r => r.N1 < 100 && (r.KodProizv == 1 || r.KodProizv == 3)).ToList() ?? new List<NormRasz>();

                int Sum(Func<NormRasz, bool> condition) => rasz.Where(condition).Sum(r => r.Sek);

                _currentAnnData.SekVyazo = Sum(r => r.KodOb == 28);
                _currentAnnData.SekVyaz5 = Sum(r => r.KodOb == 25);
                _currentAnnData.SekVyaz12 = Sum(r => r.KodOb == 35);
                _currentAnnData.SekVyaz7 = Sum(r => r.KodOb == 26);
                _currentAnnData.SekVyaz10 = Sum(r => r.KodOb == 37);
                _currentAnnData.SekVyaz6 = Sum(r => r.KodOb == 38);
                _currentAnnData.SekVyaz = Sum(r => r.KodOb == 29);
                _currentAnnData.SekShv = Sum(r => r.KodPodr != 1 && r.KodPodr != 6);
                _currentAnnData.SekKr = _normRaszList.Where(r => r.KodPodr == 7).Sum(r => r.Sek);
                _currentAnnData.Sek = _normRaszList.Where(r => r.N1 < 100).Sum(r => r.Sek);
                _currentAnnData.Slogn = (int)_normRaszList.Where(r => r.N1 < 100).Sum(r => r.Seb); // sb
                _currentAnnData.SekVyaz14 = Sum(r => r.KodOb == 62);
                _currentAnnData.SekVyaz70 = Sum(r => r.KodOb == 55);
                _currentAnnData.SekVyaz71 = Sum(r => r.KodOb == 59);
                _currentAnnData.SekVyaz72 = Sum(r => r.KodOb == 73);
                _currentAnnData.SekVyaz62 = Sum(r => r.KodOb == 60);
                _currentAnnData.SekVyaz57 = Sum(r => r.KodOb == 114);
                _currentAnnData.SekVyaz18 = Sum(r => r.KodOb == 115);
                _currentAnnData.SekShv1 = Sum(r => r.KodPodr == 1 || r.KodPodr == 6);

                if (!this.IsDisposed && this.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)(() => bindingSource1.ResetBindings(false)));
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при расширенном пересчете секунд");
            }
        }

        private async void TeamWork_AdvanceTW_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_okPressed)
            {
                try
                {
                    await this.InvokeAsync(() =>
                    {
                        if (gridViewRasz != null && gridViewRasz.OptionsBehavior.EditingMode == GridEditingMode.Inplace)
                            gridViewRasz.OptionsBehavior.EditingMode = GridEditingMode.EditForm;
                        if (gridViewRaskr != null && gridViewRaskr.OptionsBehavior.EditingMode == GridEditingMode.Inplace)
                            gridViewRaskr.OptionsBehavior.EditingMode = GridEditingMode.EditForm;

                        _gridHelper.SaveGridViewSettings(gridViewRaskr, "AdvanceTW_gridViewRaskrLayout.xml");
                        _gridHelper.SaveGridViewSettings(gridViewKont, "AdvanceTW_gridViewKontLayout.xml");
                        _gridHelper.SaveGridViewSettings(gridViewRasz, "AdvanceTW_gridViewRaszLayout.xml");
                    });
                }
                catch (Exception ex)
                {
                    await _logger.LogErrorAsync(ex, "Ошибка при закрытии формы TeamWork_AdvanceTW");
                }
            }
            else if (e.CloseReason == CloseReason.UserClosing)
            {
                if (_hasUnsavedChanges)
                {
                    var result = MessageBox.Show(
     "Вы уверены, что хотите отменить все изменения?",
     "Подтверждение отмены",
     MessageBoxButtons.YesNo,
     MessageBoxIcon.Question,
     MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.No)
                    {
                        e.Cancel = true; 
                    }
                    else
                    {
                        PerformCleanupOnCancel();
                    }
                }
                else
                {
                    if (_mode == (int)Mode.NewWorkDivision)
                    {
                        PerformCleanupOnCancel();
                    }
                }
            }
        }

        private async void PerformCleanupOnCancel()
        {
             try
             {
                 if (_mode == (int)Mode.NewWorkDivision && _newAnnId > 0)
                 {
                      await _artNormService.DeleteRelatedNormTables(_newAnnId);
                      await _artNormService.DeleteByAnnId(TableNames.Ann, _newAnnId);
                      await _logger.LogEventAsync($"Отмена создания. Удалена запись AnnID: {_newAnnId}", "PerformCleanupOnCancel");
                 }
             }
             catch (Exception ex)
             {
                 await _logger.LogErrorAsync(ex, "Ошибка при очистке данных при отмене");
                 // MessageBox.Show($"Ошибка при удалении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
        }

        #region Rasz

        private void gridViewRasz_EditFormShowing(object sender, EditFormShowingEventArgs e)
        {
            var gridView = sender as GridView;
            if (gridView == null) return;

            if (!gridView.IsNewItemRow(e.RowHandle))
            {
                if (_isCustomEditFormOpen) 
                {
                    _isCustomEditFormOpen = false;
                }
                else 
                {
                    var currentNormRasz = gridView.GetRow(e.RowHandle) as NormRasz;
                    if (currentNormRasz != null && !currentNormRasz.IsNew) 
                    {
                        _originalNormRaszDataBeforeEdit = currentNormRasz.Clone();
                    }
                    else
                    {
                        _originalNormRaszDataBeforeEdit = null;
                    }
                }
                e.Allow = true;
                return;
            }

            e.Allow = false;

            using (var selectionForm = new NormOperNew(_selectedAnnId))
            {
                var result = selectionForm.ShowDialog();

                if (result == DialogResult.OK && selectionForm.SelectedRowData != null)
                {
                    var selectedData = selectionForm.SelectedRowData;
                    selectedData.IsNew = true;
                    _normRaszList.Add(selectedData);
                    _normRaszBindingSource.ResetBindings(false);

                    int newRowDataSourceIndex = _normRaszList.IndexOf(selectedData);
                    if (newRowDataSourceIndex >= 0)
                    {
                        int newRowHandle = gridView.GetRowHandle(newRowDataSourceIndex);
                        if (gridView.IsValidRowHandle(newRowHandle))
                        {
                            FinalizeRow(newRowHandle, gridView);
                        }
                        else
                        {
                            _logger.LogEventAsync($"Could not find new row handle for added NormRasz. DataSource Index: {newRowDataSourceIndex}", "gridViewRasz_EditFormShowing_NewRowFail");
                        }
                    }
                }
            }
        }
        //private void gridViewRasz_EditFormShowing(object sender, EditFormShowingEventArgs e)
        //{
        //    var gridView = sender as GridView;
        //    if (gridView == null) return;

        //    // If we are editing an existing row (not the NewItemRow placeholder)
        //    // or if this event is for the row just added by our custom logic (FinalizeRow).
        //    if (!gridView.IsNewItemRow(e.RowHandle))
        //    {
        //        if (_isCustomEditFormOpen)
        //        {
        //            // This EditForm is being shown programmatically by FinalizeRow
        //            // for the newly added row. Allow it and reset the flag.
        //            _isCustomEditFormOpen = false;
        //        }
        //        // For any non-NewItemRow, allow the standard editor.
        //        e.Allow = true;
        //        return;
        //    }

        //    // If we reach here, gridView.IsNewItemRow(e.RowHandle) is TRUE.
        //    // This means the user clicked the "Add New Row" placeholder.
        //    // Always execute our custom logic.
        //    e.Allow = false; // Prevent the standard EditForm from appearing for the NewItemRow.

        //    using (var selectionForm = new NormOperNew(_selectedAnnId))
        //    {
        //        var result = selectionForm.ShowDialog();

        //        if (result == DialogResult.OK && selectionForm.SelectedRowData != null)
        //        {
        //            var selectedData = selectionForm.SelectedRowData;
        //            selectedData.IsNew = true; // Mark as new for styling/saving logic
        //            _normRaszList.Add(selectedData);

        //            _normRaszBindingSource.ResetBindings(false);

        //            int newRowDataSourceIndex = _normRaszList.IndexOf(selectedData);
        //            if (newRowDataSourceIndex >= 0)
        //            {
        //                int newRowHandle = gridView.GetRowHandle(newRowDataSourceIndex);
        //                if (gridView.IsValidRowHandle(newRowHandle))
        //                {
        //                    FinalizeRow(newRowHandle, gridView);
        //                }
        //                else
        //                {
        //                    // Optional: Log if the new row handle couldn't be found immediately.
        //                    // This might indicate a timing issue or filtering that prevents the row
        //                    // from being visible right after adding to the source.
        //                    _logger.LogEventAsync($"Could not find new row handle for added NormRasz. DataSource Index: {newRowDataSourceIndex}", "gridViewRasz_EditFormShowing_NewRowFail");
        //                }
        //            }
        //        }
        //        // If DialogResult was not OK (e.g., Cancel) or no data selected,
        //        // the NewItemRow placeholder should typically revert or disappear
        //        // because e.Allow is false and no data was committed.
        //        // If the placeholder undesirably persists after cancelling NormOperNew,
        //        // you might need to add explicit cancellation for the new item row:
        //        // else if (e.RowHandle == DevExpress.XtraGrid.GridControl.NewItemRowHandle)
        //        // {
        //        //     gridView.CancelUpdateCurrentRow(); // This should revert the NewItemRow changes
        //        // }
        //    }
        //}
        ////private void gridViewRasz_EditFormShowing(object sender, EditFormShowingEventArgs e)
        //{
        //    var gridView = sender as GridView;
        //    if (gridView == null || !gridView.IsNewItemRow(e.RowHandle))
        //        return;

        //    if (_isCustomEditFormOpen)
        //    {
        //        // Если флаг установлен, значит мы специально открыли форму
        //        _isCustomEditFormOpen = false;
        //        return;
        //    }
        //    if (gridView.GetRowCellValue(e.RowHandle, "Kod") != null)//решаем, показывать ли editForm  
        //    {
        //        e.Allow = true;
        //        return;
        //    }
        //    e.Allow = false;

        //    using (var selectionForm = new NormOperNew(_selectedAnnId))
        //    {
        //        var result = selectionForm.ShowDialog();

        //        if (result == DialogResult.OK && selectionForm.SelectedRowData != null)
        //        {
        //            var selected = selectionForm.SelectedRowData;

        //            selected.IsNew = true;
        //            _normRaszList.Add(selected);

        //            // Обновляем привязку данных и интерфейс
        //            _normRaszBindingSource.ResetBindings(false);
        //            gridControlRasz.RefreshDataSource();
        //            gridViewRasz.RefreshData();
        //            gridView.PostEditor();
        //            gridView.UpdateCurrentRow();

        //            int indexInList = _normRaszList.IndexOf(selected);

        //            if (indexInList >= 0)
        //            {
        //                int rowHandle = gridView.GetRowHandle(indexInList);

        //                if (rowHandle >= 0)
        //                {
        //                    gridView.FocusedRowHandle = rowHandle;
        //                }
        //                FinalizeRow(rowHandle, gridView);
        //            }
        //        }
        //        else if (result == DialogResult.Cancel)
        //        {
        //            // Если пользователь отменил выбор, удаляем строку
        //            gridView.CancelUpdateCurrentRow();
        //            gridView.HideEditForm();
        //            gridView.CloseEditForm();
        //            gridView.DeleteRow(e.RowHandle);
        //        }
        //        else
        //        {
        //            e.Allow = false;
        //            gridView.CancelUpdateCurrentRow();
        //            gridView.DeleteRow(e.RowHandle);
        //        }
        //    }
        //}

        private void FinalizeRow(int rowHandle, GridView gridView)
        {
            // разрешаем показать EditForm
            gridView.PostEditor();
            gridView.UpdateCurrentRow();
            gridView.RefreshRow(rowHandle);

            _isCustomEditFormOpen = true;

            gridView.GridControl.BeginInvoke(new Action(() =>
            {
                gridView.FocusedRowHandle = rowHandle; 
                gridView.ShowEditForm();               
            }));
        }

        //private void gridViewRasz_RowEditCanceled(object sender, RowObjectEventArgs e)
        //{
        //    GridView view = sender as GridView;
        //    if (view != null)
        //    {
        //        view.HideEditForm();
        //    }
        //}
        private void gridViewRasz_RowEditCanceled(object sender, RowObjectEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;

            var canceledNormRasz = e.Row as NormRasz;
            if (canceledNormRasz == null) return;

            if (canceledNormRasz.IsNew) // Если это была новая строка (после NormOperNew и FinalizeRow)
            {
                if (_normRaszList.Contains(canceledNormRasz))
                {
                    _normRaszList.Remove(canceledNormRasz);
                    _logger.LogEventAsync($"New NormRasz row removed due to edit cancellation.", "gridViewRasz_RowEditCanceled");
                    _normRaszBindingSource.ResetBindings(false); // Обновить грид
                }
            }
            else if (_originalNormRaszDataBeforeEdit != null) // Если редактирование существующей строки было отменено
            {
                int index = _normRaszList.IndexOf(canceledNormRasz);
                if (index != -1)
                {
                    // Восстанавливаем оригинальные данные. Убедитесь, что NormRasz.Clone() был эффективен.
                    _normRaszList[index].CopyPropertiesFrom(_originalNormRaszDataBeforeEdit); // Нужен метод CopyPropertiesFrom или ручное копирование
                    _logger.LogEventAsync($"NormRasz row (nrId: {_originalNormRaszDataBeforeEdit.nrId}) edit canceled, reverted to original state.", "gridViewRasz_RowEditCanceled");
                    _normRaszBindingSource.ResetBindings(false); // Важно для обновления грида
                }
            }

            _originalNormRaszDataBeforeEdit = null; // Очищаем сохраненное состояние
                                                    // view.HideEditForm(); // Обычно не требуется, грид сам закроет форму при отмене
            _hasUnsavedChanges = true; // Список данных изменился или редактирование отменено
        }
        private void gridViewRasz_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (e.Row is NormRasz normRasz)
            {
                // Если строка не новая, помечаем ее как измененную
                if (!normRasz.IsNew)
                {
                    normRasz.IsModified = true;
                }
                gridViewRasz.UpdateCurrentRow(); // Обновляем строку, чтобы RowStyle сработал
            }
        }

        private async void gridViewRasz_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            if (e.Row is NormRasz normRasz)
            {
                try
                {
                    normRasz.AnnId = _newAnnId;
                    e.Valid = true;
                }
                catch (Exception ex)
                {
                    e.Valid = false;
                    e.ErrorText = $"Ошибка: {ex.Message}";
                    await _logger.LogErrorAsync(ex, "Ошибка при валидации данных");
                }
            }
        }
        private async void gridViewRasz_EditFormHidden(object sender, EditFormHiddenEventArgs e)
        {
            var gridView = sender as GridView;
            if (gridView == null) return;

            // Если редактирование было отменено (кнопкой Cancel или Esc), RowEditCanceled должен был это обработать.
            if (e.Result == EditFormResult.Cancel)
            {
                _originalNormRaszDataBeforeEdit = null; // Убедимся, что очищено
                return; // Явно ничего не делаем для отмены/прерывания.
            }

            // Обработка других случаев, когда форма скрыта без обновления
            if (e.Result != EditFormResult.Update)
            {
                try
                {
                    var row = gridView.GetRow(e.RowHandle) as NormRasz;
                    // Если это существующая, сохраненная строка, и EditForm была закрыта без Update (и не Cancel/Abort)
                    if (row != null && row.nrId > 0 && !row.IsNew)
                    {
                        // Эта логика подразумевает, что закрытие EditForm для существующей строки
                        // способами, отличными от "Update", "Cancel", или "Abort", может привести к удалению.
                        // Пока эта логика удаления закомментирована, так как она агрессивна.
                        /*
                        await _logger.LogEventAsync($"Performing delete in EditFormHidden for existing row. Result: {e.Result}, RowID: {row.nrId}", "gridViewRasz_EditFormHidden_AttemptDelete");
                        await _dbService.DeleteEntityAsync(TableNames.Rasz, TableNames.RaszId, row);
                        _deletedNormRaszIds.Add(row.nrId); // Отслеживаем для SaveAllDataAsync
                        if (_normRaszList.Contains(row))
                        {
                            _normRaszList.Remove(row);
                        }
                        */
                        await _logger.LogEventAsync($"EditFormHidden for existing row (nrId: {row.nrId}) with Result: {e.Result}. Original delete logic is currently commented.", "gridViewRasz_EditFormHidden");
                    }
                    // Запасной вариант для новой строки, которая могла не быть обработана RowEditCanceled (должно быть редко)
                    else if (row != null && row.IsNew)
                    {
                        if (_normRaszList.Contains(row)) // Если она все еще в списке
                        {
                            _normRaszList.Remove(row);
                            await _logger.LogEventAsync($"Fallback removal of new row in EditFormHidden. Result: {e.Result}", "gridViewRasz_EditFormHidden");
                            _normRaszBindingSource.ResetBindings(false); // Обновить грид
                        }
                    }
                }
                catch (Exception ex)
                {
                    await _logger.LogErrorAsync(ex, "Ошибка при обработке EditFormHidden для не-Update результата");
                    // MessageBox.Show($"Ошибка при обработке закрытия формы редактирования: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            _originalNormRaszDataBeforeEdit = null; // Убедимся, что очищено после любого закрытия формы редактирования
        }
        //private async void gridViewRasz_EditFormHidden(object sender, EditFormHiddenEventArgs e)
        //{
        //    var gridView = sender as GridView;
        //    if (gridView == null) return;

        //    if (e.Result != EditFormResult.Update)
        //    {
        //        try
        //        {
        //            // Получаем текущую строку
        //            var row = gridView.GetRow(e.RowHandle) as NormRasz;
        //            if (row != null && row.nrId > 0)
        //            {
        //                // Удаляем из базы данных
        //                await _dbService.DeleteEntityAsync(TableNames.Rasz, TableNames.RaszId, row);//.DeleteNormRaszAsync(row.nrId);

        //                // Удаляем из списка
        //                _normRaszList.Remove(row);
        //                _normRaszBindingSource.ResetBindings(false);

        //                // Удаляем из грида
        //                gridView.DeleteRow(e.RowHandle);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            await _logger.LogErrorAsync(ex, "Ошибка при удалении строки после закрытия формы редактирования");
        //            MessageBox.Show($"Ошибка при удалении строки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}
        #endregion

        #region Rask
        // private async void GridView2_InitNewRow(object sender, InitNewRowEventArgs e)
        private async void OpenSelectionForm() 
        {
            if (_isSelectionFormOpen)
                return;

            try
            {
                _isSelectionFormOpen = true;
                using (var selectionForm = new norm_raskrNew(_newAnnId))
                {
                    DialogResult result = selectionForm.ShowDialog();

                    if (result == DialogResult.OK && selectionForm.SelectedData != null && selectionForm.SelectedData.Count > 0)
                    {
                        // Удаляем старые записи из базы данных
                        foreach (var oldRow in _normRaskList.ToList())
                        {
                            if (oldRow.id > 0)
                            {
                                await _dbService.DeleteEntityAsync(TableNames.Rask, TableNames.RaskId, oldRow);
                            }
                        }

                        // Очищаем список
                        _normRaskList.Clear();

                        // Вставляем новые данные
                        foreach (var normRask in selectionForm.SelectedData)
                        {
                            normRask.IsNew = true;
                            normRask.AnnId = _newAnnId;
                            _normRaskList.Add(normRask);
                        }

                        // Обновляем привязку данных и интерфейс
                        _normRaskBindingSource.ResetBindings(false);
                        gridControlRaskr.RefreshDataSource();
                        gridViewRaskr.RefreshData();
                    }
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка в методе GridView2_InitNewRow");
                await this.InvokeAsync(() =>
                {
                    MessageBox.Show($"Ошибка при добавлении новой строки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
            finally
            {
                _isSelectionFormOpen = false;
            }
        }


        private void gridViewRaskr_ShowingEditor(object sender, CancelEventArgs e)
        {
            var view = sender as GridView;
            if (view == null)
                return;

            if (view.FocusedRowHandle == DevExpress.XtraGrid.GridControl.NewItemRowHandle)
            {
                e.Cancel = true;
                if (!_isSelectionFormOpen)
                {
                    OpenSelectionForm();
                }
            }
        }

        private void GridView2_RowUpdated(object sender, RowObjectEventArgs e)
        {
            if (e.Row is NormRask normRask)
            {
                normRask.AnnId = _newAnnId;
                // Если строка не новая, помечаем ее как измененную
                if (!normRask.IsNew)
                {
                    normRask.IsModified = true;
                }
                gridViewRaskr.UpdateCurrentRow(); // Обновляем строку, чтобы RowStyle сработал
            }
        }

        private async void GridView2_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (e.Row is NormRask normRask)
            {
                try
                {
                    // Убеждаемся что AnnId установлен
                    normRask.AnnId = _newAnnId;

                    // Если это новая запись (Id <= 0), сохраняем в БД
                    if (normRask.id <= 0)
                    {
                        normRask.id = await _dbService.InsertEntityAsync(TableNames.Rask, TableNames.RaskId, normRask);//InsertNormRaskAsync(normRask);
                        if (normRask.id <= 0)
                        {
                            e.Valid = false;
                            e.ErrorText = "Ошибка при сохранении записи в базу данных";
                        }
                    }
                }
                catch (Exception ex)
                {
                    e.Valid = false;
                    e.ErrorText = $"Ошибка: {ex.Message}";
                    await _logger.LogErrorAsync(ex, "Ошибка при сохранении данных");
                }
            }
        }
        #endregion

        #region Kont
        private void gridViewKont_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (e.Row is NormKont normKont)
            {
                normKont.AnnId = _newAnnId;
                // Если строка не новая, помечаем ее как измененную
                if (!normKont.IsNew)
                {
                    normKont.IsModified = true;
                }
                gridViewKont.UpdateCurrentRow(); // Обновляем строку, чтобы RowStyle сработал
            }
        }

        private async void gridViewKont_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
             e.Valid = true; 
        }

        private void gridViewKont_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;

            if (view.IsNewItemRow(view.FocusedRowHandle))
            {
                e.Cancel = true; 

                if (_isSelectionFormOpen) return;

                try
                {
                    _isSelectionFormOpen = true;

                    string choice1 = "Пронумеровать деталь";
                    string choice2 = "Номер пачки";
                    // Проверяем, какие строки уже есть
                    bool hasChoice1 = _normKontList.Any(nk => nk.Text == choice1);
                    bool hasChoice2 = _normKontList.Any(nk => nk.Text == choice2);

                    List<string> options = new List<string>();
                    if (!hasChoice1) options.Add(choice1);
                    if (!hasChoice2) options.Add(choice2);

                    string selectedText = null;

                    if (options.Count == 2)
                    {
                        DialogResult choiceResult = MessageBox.Show($"Добавить '{options[0]}'?", "Выбор операции", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (choiceResult == DialogResult.Yes)
                        {
                            selectedText = options[0];
                        }
                        else if (choiceResult == DialogResult.No)
                        {
                             DialogResult choiceResult2 = MessageBox.Show($"Добавить '{options[1]}'?", "Выбор операции", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                             if (choiceResult2 == DialogResult.Yes)
                             {
                                 selectedText = options[1];
                             }
                        }
                    }
                    else if (options.Count == 1)
                    {
                        selectedText = options[0]; 
                        MessageBox.Show($"Добавлена строка: {selectedText}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Обе возможные строки уже добавлены.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (selectedText != null)
                    {
                        var newKont = new NormKont
                        {
                            AnnId = _newAnnId,
                            Text = selectedText,
                            IsNew = true 
                        };
                        _normKontList.Add(newKont);
                        _normKontBindingSource.ResetBindings(false); // Обновляем грид
                    }
                }
                finally
                {
                    _isSelectionFormOpen = false;
                }
            }
            else if (view.FocusedColumn.FieldName == "Text")
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        #endregion

        #region DopObr
        //private void gridViewDopObr_ShowingEditor(object sender, CancelEventArgs e)
        //{
        //    GridView view = sender as GridView;
        //    if (view == null) return;

        //    if (view.IsNewItemRow(view.FocusedRowHandle))
        //    {
        //        e.Cancel = true;

        //        if (_isSelectionFormOpen) return;

        //        try
        //        {
        //            _isSelectionFormOpen = true;

        //            if (_normDopObrList == null || _normDopObrList.Count == 0)
        //            {
        //                var newDopObr = new NormDopObr
        //                {
        //                    AnnId = _newAnnId,
        //                    IsNew = true 
        //                };
        //                _normDopObrList.Add(newDopObr);
        //                _normDopObrBindingSource.ResetBindings(false);
        //                MessageBox.Show("Добавлена строка дополнительной обработки.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //            else
        //            {
        //                MessageBox.Show("В таблице дополнительной обработки может быть только одна строка.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }
        //        }
        //        finally
        //        {
        //            _isSelectionFormOpen = false;
        //        }
        //    }
        //    else
        //    {
        //        e.Cancel = false;
        //    }
        //}

        //private async void gridViewDopObr_KeyDown(object sender, KeyEventArgs e)
        //{
        //     GridView view = sender as GridView;
        //     if (view == null) return;

        //     if (e.KeyCode == Keys.Delete && view.FocusedRowHandle >= 0)
        //     {
        //         if (MessageBox.Show("Удалить строку дополнительной обработки?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        //         {
        //             var rowToDelete = view.GetRow(view.FocusedRowHandle) as NormDopObr;
        //             if (rowToDelete != null)
        //             {
        //                 try
        //                 {
        //                     if (!rowToDelete.IsNew && rowToDelete.doId > 0)
        //                     {
        //                         await _dbService.DeleteEntityAsync(TableNames.Obr, TableNames.ObrId, rowToDelete);
        //                     }
        //                     _normDopObrList.Remove(rowToDelete);
        //                     _normDopObrBindingSource.ResetBindings(false);
        //                     view.RefreshData(); 
        //                     _hasUnsavedChanges = true;
        //                 }
        //                 catch(Exception ex)
        //                 {
        //                      await _logger.LogErrorAsync(ex, "Ошибка при удалении строки NormDopObr");
        //                      MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                 }
        //             }
        //         }
        //         e.Handled = true; 
        //     }
        //}

        //private async void gridViewDopObr_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        //{
        //    if (e.Row is NormDopObr normDopObr)
        //    {
        //        try
        //        {
        //            normDopObr.AnnId = _newAnnId;
        //            e.Valid = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            e.Valid = false;
        //            e.ErrorText = $"Ошибка: {ex.Message}";
        //            await _logger.LogErrorAsync(ex, "Ошибка при валидации строки NormDopObr");
        //        }
        //    }
        //}

        //private void gridViewDopObr_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        //{
        //    if (e.Row is NormDopObr normDopObr)
        //    {
        //        normDopObr.AnnId = _newAnnId;
        //        if (!normDopObr.IsNew)
        //        {
        //            normDopObr.IsModified = true;
        //        }
        //        gridViewDopObr.UpdateCurrentRow(); 
        //    }
        //}

        #endregion

        private async void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave_Click(sender, e);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }

        }

        private async Task SaveAnnDataAsync()
        {
            try
            {
                // Сохраняем данные в таблицу ann
                _currentAnnData.AnnID = _newAnnId;
                if (_newAnnId > 0)
                     RecalculateSek();
                await _dbService.UpdateEntityAsync(TableNames.Ann, TableNames.AnnId, _currentAnnData);
                CreatedAnn = _currentAnnData;

               // MessageBox.Show("Данные успешно сохранены", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при сохранении данных в БД");
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
        }        
        private async Task SaveAllDataAsync()
        {
            try
            {
                await SaveListAsync(_normRaszList, TableNames.Rasz, TableNames.RaszId, _newAnnId, _deletedNormRaszIds);
                await SaveListAsync(_normRaskList, TableNames.Rask, TableNames.RaskId, _newAnnId, _deletedNormRaskIds);
                await SaveListAsync(_normKontList, TableNames.Kont, TableNames.KontId, _newAnnId, _deletedNormKontIds);

                // Обновляем UI после сохранения
                await this.InvokeAsync(() =>
                {
                    gridControlRasz.RefreshDataSource();
                    gridControlRaskr.RefreshDataSource();
                    gridControlKont.RefreshDataSource();
                 });
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при сохранении данных");
                throw;
            }
        }

        private async Task SaveListAsync<T>(BindingList<T> list, string tableName, string keyFieldName, int newAnnId, List<int> deletedIds)
    where T : class, INewable, new()
        {
            var stopwatch = Stopwatch.StartNew();
            var bulkStopwatch = new Stopwatch();

            var newItems = list.Where(x => x.IsNew).ToList();
            var existingItems = list.Where(x => !x.IsNew).ToList();

            string itemTypeName = typeof(T).Name;
            await _logger.LogEventAsync($"[{itemTypeName}] Start saving. New: {newItems.Count}, Existing: {existingItems.Count}", "SaveListAsync");

            // 1. Удаление
            if (deletedIds?.Any() == true)
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string deleteSql = $"DELETE FROM {tableName} WHERE {keyFieldName} IN @ids";
                    await connection.ExecuteAsync(deleteSql, new { ids = deletedIds });
                    await _logger.LogEventAsync($"[{itemTypeName}] Удалено записей: {deletedIds.Count}", "SaveListAsync");
                }
            }

            // 2. Обработка новых
            if (newItems.Any())
            {
                foreach (var item in newItems)
                {
                    var annIdProp = typeof(T).GetProperty("AnnId");
                    if (annIdProp != null)
                    {
                        annIdProp.SetValue(item, newAnnId);
                    }
                }

                using (var connection = _dbHelper.GetConnection())
                {
                    await _logger.LogEventAsync($"[{itemTypeName}] BulkInsert: {newItems.Count}", "SaveListAsync");
                    bulkStopwatch.Restart();
                    await connection.BulkInsertAsync(newItems);
                    bulkStopwatch.Stop();
                }

                foreach (var item in newItems)
                    item.IsNew = false;
            }

            // 3. Обновление
            if (existingItems.Any())
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    await _logger.LogEventAsync($"[{itemTypeName}] BulkUpdate: {existingItems.Count}", "SaveListAsync");
                    bulkStopwatch.Restart();
                    await connection.BulkUpdateAsync(existingItems);
                    bulkStopwatch.Stop();
                }
            }

            stopwatch.Stop();
            await _logger.LogEventAsync($"[{itemTypeName}] Finished saving. Total: {stopwatch.ElapsedMilliseconds} ms", "SaveListAsync");
        }


        /// <summary>
        /// Обработчик события изменения выбранного элемента в LookUpEdit конструктора и дизайнера
        /// </summary>
        private async void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is DevExpress.XtraEditors.LookUpEdit lookUpEdit && lookUpEdit.EditValue != null && lookUpEdit.EditValue != DBNull.Value)
            {
                try
                {
                    int selectedId = Convert.ToInt32(lookUpEdit.EditValue);
                    string fieldName = string.Empty;
                    
                    if (lookUpEdit == constructorComboBox)
                    {
                        fieldName = "constr";
                        //await _logger.LogEventAsync($"Выбран конструктор с ID {selectedId}", "ComboBox_SelectedIndexChanged");
                    }
                    else if (lookUpEdit == designerComboBox)
                    {
                        fieldName = "diz";
                        //await _logger.LogEventAsync($"Выбран дизайнер с ID {selectedId}", "ComboBox_SelectedIndexChanged");
                    }
                }
                catch (Exception ex)
                {
                    await _logger.LogErrorAsync(ex, "Ошибка при обработке выбора сотрудника");
                }
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Вставить из буфера"
        /// </summary>
        private async void buffer_Click(object sender, EventArgs e)
        {
            if (_bufferWorkDivision > 0)
            {
                try
                {
                    var result = MessageBox.Show(
     "Очистить текущие данные перед вставкой из буфера?",
     "Вставка из буфера",
     MessageBoxButtons.YesNo,
     MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (_normRaszList == null || _normRaskList == null || _normKontList == null)
                        {
                            await InitializeBindingsAsync();
                        }

                        // Очищаем текущие списки
                        _normRaszList?.Clear();
                        _normRaskList?.Clear();
                        _normKontList?.Clear();

                        _normRaszBindingSource?.ResetBindings(false);
                        _normRaskBindingSource?.ResetBindings(false);
                        _normKontBindingSource?.ResetBindings(false);
                    }

                    // Загружаем данные из буфера
                    await WorkDivisionLoadAsync(caller: "buffer", _bufferWorkDivision);
                    MessageBox.Show("Данные из буфера успешно загружены", "Информация",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException sqlEx)
                {
                    await _logger.LogErrorAsync(sqlEx, "Ошибка при вставке данных из буфера");
                    MessageBox.Show($"Ошибка при вставке данных из буфера: {sqlEx.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    await _logger.LogErrorAsync(ex, "Ошибка при вставке данных из буфера");
                    MessageBox.Show($"Ошибка при вставке данных из буфера: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else { MessageBox.Show("В буфере пусто", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private async Task InvokeAsync(Action action)
        {
            if (this.InvokeRequired)
            {
                await Task.Run(() => this.Invoke(action));
            }
            else
            {
                action();
            }
        }


        private bool ValidateForm()
        {
            bool isValid = true;
            statusLabel.Text = ""; // Очищаем статус
            errorProvider1.Clear(); // Очищаем все старые ошибки

            // Проверка поля Артикул
            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                errorProvider1.SetError(nameTextBox, "Введите артикул.");
                if (isValid)
                    statusLabel.Text = "Ошибка: Поле 'Артикул' обязательно для заполнения.";
                isValid = false;
            }

            //// Проверка поля Модель или Группа
            //if (string.IsNullOrWhiteSpace(groupTextBox.Text) && string.IsNullOrWhiteSpace(modelTextBox.Text))
            //{
            //    errorProvider1.SetError(groupTextBox, "Заполните либо 'Модель', либо 'Группу'.");
            //    errorProvider1.SetError(modelTextBox, "Заполните либо 'Модель', либо 'Группу'.");
            //    if (isValid)
            //        statusLabel.Text = "Ошибка: Заполните либо 'Модель', либо 'Группу'.";
            //    isValid = false;
            //}

            return isValid;
        }
        private async Task ShowStatusMessage(string message, int delayMs = 3000)
        {
            statusLabel.Text = message;
            await Task.Delay(delayMs);
            statusLabel.Text = "";
        }

        private void OnDataChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType != ListChangedType.Reset || _normRaszList.Count > 0 || _normRaskList.Count > 0 || _normKontList.Count > 0)
            {
                 _hasUnsavedChanges = true;
            }
        }

        private void GridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null || e.RowHandle < 0)
                return;

            object row = view.GetRow(e.RowHandle);

            if (row is INewable newableRow && newableRow.IsNew)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.HighPriority = true; 
                return; 
            }

            try
            {
                if (row is IModifiable modifiable && modifiable.IsModified)
                {
                    e.Appearance.BackColor = Color.LightYellow;
                    e.HighPriority = true;
                }
            }
            catch (Exception ex) 
            {
                 _logger.LogErrorAsync(ex, $"Ошибка в GridView_RowStyle при проверке IsModified для строки {e.RowHandle}").ConfigureAwait(false); 
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            _okPressed = true;
            await ShowStatusMessage("Сохранение данных...");

            if (!ValidateForm())
            {
                await ShowStatusMessage("Ошибки заполнения формы");
                return;
            }
            try
            {
                await _dbHelper.ExecuteInTransactionAsync(async () =>
                {
                    await SaveAnnDataAsync();
                    await SaveAllDataAsync();
                    if (_mode == (int)Mode.ArchAndCopy)
                    {

                    }
                });
                await ShowStatusMessage("Данные успешно сохранены!");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при сохранении данных");
            }

        }


    }
}
