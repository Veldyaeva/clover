using Dapper;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.form.TeamWork.Forms;
using SewingProduction.Helpers;
using SewingProduction.Interfaces;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z.Dapper.Plus;
using BindingSource = System.Windows.Forms.BindingSource;
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

        private int _originalBufferId;
        private int? _sourceAnnIdToCopyDetailsFrom = null; // Для копирования операций
        private ArtNormN _duplicateAnnData = null; // Для копирования шапки РТ

        public int CurrentMode => _mode;
        public int? SourceAnnIdToCopyDetailsFrom => _sourceAnnIdToCopyDetailsFrom;

        public static class CloneUtils
        {
            public static List<T> CloneList<T>(IEnumerable<T> source, int newAnnId, string idFieldName)
                where T : ICloneable
            {
                var list = new List<T>();
                foreach (var item in source)
                {
                    var clone = (T)item.Clone();
                    typeof(T).GetProperty(idFieldName)?.SetValue(clone, 0);
                    typeof(T).GetProperty("AnnId")?.SetValue(clone, newAnnId);
                    // For items cloned into a new RT, they are inherently "new" and "modified" in the context of that RT
                    if (typeof(T).GetProperty("IsNew") != null) typeof(T).GetProperty("IsNew").SetValue(clone, true);
                    if (typeof(T).GetProperty("IsModified") != null) typeof(T).GetProperty("IsModified").SetValue(clone, true);
                    list.Add(clone);
                }
                return list;
            }

            public static BindingList<T> DeepCloneBindingList<T>(IEnumerable<T> sourceList) where T : class, ICloneable
            {
                if (sourceList == null) return new BindingList<T>();
                var newList = new BindingList<T>();
                foreach (var item in sourceList)
                {
                    if (item is T clonedItem) // Ensure item is not null and is of type T
                    {
                        newList.Add(clonedItem.Clone() as T);
                    }
                }
                return newList;
            }
        }

        private BindingList<NormRasz> _originalNormRaszList;
        private BindingList<NormRask> _originalNormRaskList;
        private BindingList<NormKont> _originalNormKontList;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newId">Id новой записи</param>
        /// <param name="oldId">Id исходной записи</param>
        /// <param name="bufferWorkDivision">Id из буфера</param>
        /// <param name="mode">режим</param>
        public TeamWork_AdvanceTW(int bufferWorkDivision, int mode, int? newId = null, int? oldId = null, int? sourceAnnIdToCopyDetailsFrom = null, MyDataART initialArtData = null, ArtNormN duplicateAnnData = null)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _dbService = new DbService(_dbHelper);
            _artNormService = new ArtNormService(_dbHelper);
            ThemeManager.UpdateTheme(this);

            if (!oldId.HasValue)
                _selectedAnnId = _newAnnId = newId.Value;
            if (!newId.HasValue)
                _selectedAnnId = _newAnnId = oldId.Value;
            if (oldId.HasValue && newId.HasValue)
            {
                _newAnnId = newId.Value;
                _selectedAnnId = oldId.Value;
            }
            _bufferWorkDivision = bufferWorkDivision;
            _mode = mode;
            _sourceAnnIdToCopyDetailsFrom = sourceAnnIdToCopyDetailsFrom;
            _duplicateAnnData = duplicateAnnData;
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
           // if (control == dateUpdate) return nameof(ArtNormN.dateUpdate);
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
                _normRaszList.ListChanged += (_, __) => _sekDebouncer.Debounce(500, async () => { if (_newAnnId > 0) RecalculateSek(); });
                _normRaskList.ListChanged += (_, __) => _sekDebouncer.Debounce(500, async () => { if (_newAnnId > 0) RecalculateSek(); });
                _normKontList.ListChanged += (_, __) => _sekDebouncer.Debounce(500, async () => { if (_newAnnId > 0) RecalculateSek(); });
                if (_mode == (int)Mode.ArchAndCopy || _mode == (int)Mode.NewWorkDivision || _mode ==(int)Mode.Clone)
                {
                    AttachDeleteContextMenu(gridViewRasz, _normRaszList, r => r.nrId, _deletedNormRaszIds);
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

                //await WorkDivisionLoadAsync(caller: "DataLoad", _selectedAnnId);
                //await LoadAnnDataAsync();
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
                    case (int)Mode.NewWorkDivision:
                        this.Text = "Добавить предварительное";
                        nameTextBox.Enabled = true;
                        // ArtNormN и AnnId уже созданы заранее, коллекции пустые
                        _normRaszList.Clear();
                        _normRaskList.Clear();
                        _normKontList.Clear();
                        break;
                    case (int)Mode.ArchAndCopy:
                        this.Text = "Архив+копия";
                        var raszArch = await _artNormService.GetRelatedNormRasz(_selectedAnnId);
                        _normRaszList.BulkLoad(CloneUtils.CloneList(raszArch, _newAnnId, "nrId"));
                        _normRaskList.Clear();
                        _normKontList.Clear();
                        _currentAnnData.dateCreate = DateTime.Now;
                        break;
                    case (int)Mode.Edit:
                        this.Text = "Редактировать";
                        await LoadForEdit(_selectedAnnId);
                        break;
                    case (int)Mode.Clone:
                        this.Text = "Дубль";
                        var raszClone = await _artNormService.GetRelatedNormRasz(_selectedAnnId);
                        _normRaszList.BulkLoad(CloneUtils.CloneList(raszClone, _newAnnId, "nrId"));
                        _normRaskList.Clear();
                        _normKontList.Clear(); _currentAnnData.dateCreate = DateTime.Now;
                        break;
                }
                await LoadAnnDataAsync();
                _hasUnsavedChanges = false;

                AttachChangeHandlers();

                kodProizvList = await _dbService.GetListAsync<KodProizvModel>("SELECT kod_proizv, text_proizv FROM kod_proizv", null);
                podrVyazList = await _dbService.GetListAsync<PodrVyazModel>("SELECT kod_vyaz, text_vyaz FROM podr_vyaz", null);
                oborudShvList = await _dbService.GetListAsync<OborudShvModel>("SELECT ko_ob_all as kod_ob, text_ob FROM oborud_shv_ob", null);

                repositoryItemLookUpEdit_kod_proizv.DataSource = kodProizvList;
                repositoryItemLookUpEdit_kod_proizv.DisplayMember = "text_proizv";
                repositoryItemLookUpEdit_kod_proizv.ValueMember = "kod_proizv";
                repositoryItemLookUpEdit_kod_proizv.NullText = "[Выберите значение]";
                repositoryItemLookUpEdit_podrVyaz.DataSource = podrVyazList;
                repositoryItemLookUpEdit_podrVyaz.DisplayMember = "text_vyaz";
                repositoryItemLookUpEdit_podrVyaz.ValueMember = "kod_vyaz";
                repositoryItemLookUpEdit_podrVyaz.NullText = "[Выберите значение]";
                repositoryItemLookUpEdit_oborudShv.DataSource = oborudShvList;
                repositoryItemLookUpEdit_oborudShv.DisplayMember = "text_ob";
                repositoryItemLookUpEdit_oborudShv.ValueMember = "kod_ob";
                repositoryItemLookUpEdit_oborudShv.NullText = "[Выберите значение]";


                designerComboBox.DataBindings.Clear();
                constructorComboBox.DataBindings.Clear();

                designerComboBox.DataBindings.Add("EditValue", bindingSource1, nameof(ArtNormN.Diz), true, DataSourceUpdateMode.OnPropertyChanged);
                constructorComboBox.DataBindings.Add("EditValue", bindingSource1, nameof(ArtNormN.Constr), true, DataSourceUpdateMode.OnPropertyChanged);

                // Подписываем таблицы на обработчик RowStyle
                gridViewRasz.RowStyle += GridView_RowStyle;
                gridViewRaskr.RowStyle += GridView_RowStyle;
                gridViewKont.RowStyle += GridView_RowStyle;

                if (_currentAnnData != null && _sourceAnnIdToCopyDetailsFrom.HasValue && _duplicateAnnData != null && _mode == (int)Mode.NewWorkDivision)
                {
                    // Копирование данных шапки из _duplicateAnnData в _currentAnn (уже загруженный для newAnnId)
                    _currentAnnData = _duplicateAnnData.CloneProperties();
                    _currentAnnData.dateUpdate = null;
                    _currentAnnData.dateCreate = DateTime.Now;

                    _currentAnnData.ParentId = _sourceAnnIdToCopyDetailsFrom.Value;

                    // Загрузка операций из sourceAnnIdToCopyDetailsFrom
                    int sourceAnnId = _sourceAnnIdToCopyDetailsFrom.Value;

                    List<NormRasz> raszToCopy = await _artNormService.GetRelatedNormRasz(sourceAnnId);
                    _normRaszList.Clear();
                    foreach (var item in raszToCopy)
                    {
                        item.AnnId = _currentAnnData.AnnID;
                        item.IsNew = true;
                        item.IsModified = true; // Так как это новые записи для нового РТ
                        item.nrId = 0; // Сброс ID для новой записи
                        _normRaszList.Add(item);
                    }
                    _normRaszBindingSource.ResetBindings(false);

                    List<NormRask> raskToCopy = await _artNormService.GetRelatedNormRask(sourceAnnId);
                    _normRaskList.Clear();
                    foreach (var item in raskToCopy)
                    {
                        item.AnnId = _currentAnnData.AnnID;
                        item.IsNew = true;
                        item.IsModified = true;
                        item.id = 0;
                        _normRaskList.Add(item);
                    }
                    _normRaskBindingSource.ResetBindings(false);

                    List<NormKont> kontToCopy = await _artNormService.GetRelatedNormKont(sourceAnnId);
                    _normKontList.Clear();
                    foreach (var item in kontToCopy)
                    {
                        item.AnnId = _currentAnnData.AnnID;
                        item.IsNew = true;
                        item.IsModified = true;
                        item.nkId = 0;
                        _normKontList.Add(item);
                    }
                    _normKontBindingSource.ResetBindings(false);


                    _hasUnsavedChanges = true;
                    //UpdateFormTitle();
                    //DisplayCurrentAnnData(); // Обновить поля на форме данными из _currentAnn
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке формы TeamWork_AdvanceTW");
            }
        }
        private async Task LoadAndCloneAll(int sourceAnnId, int newAnnId)
        {
            var rasz = await _artNormService.GetRelatedNormRasz(sourceAnnId);
            var rask = await _artNormService.GetRelatedNormRask(sourceAnnId);
            var kont = await _artNormService.GetRelatedNormKont(sourceAnnId);
            _normRaszList.BulkLoad(CloneUtils.CloneList(rasz, newAnnId, "nrId"));
            _normRaskList.BulkLoad(CloneUtils.CloneList(rask, newAnnId, "id"));
            _normKontList.BulkLoad(CloneUtils.CloneList(kont, newAnnId, "nkId"));
        }

        private async Task LoadForEdit(int annId)
        {
            var rasz = await _artNormService.GetRelatedNormRasz(annId);
            var rask = await _artNormService.GetRelatedNormRask(annId);
            var kont = await _artNormService.GetRelatedNormKont(annId);
            _normRaszList.BulkLoad(rasz);
            _normRaskList.BulkLoad(rask);
            _normKontList.BulkLoad(kont);
            // Сохраняем оригинальные списки для отката
            _originalNormRaszList = CloneUtils.DeepCloneBindingList(_normRaszList);
            _originalNormRaskList = CloneUtils.DeepCloneBindingList(_normRaskList);
            _originalNormKontList = CloneUtils.DeepCloneBindingList(_normKontList);
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
                        if (_mode == (int)Mode.Clone || _mode == (int)Mode.ArchAndCopy)
                        {
                            annData.dateCreate = DateTime.Now;
                            annData.dateUpdate = null;
                        }
                        _currentAnnData = annData;                // Обновляем текущую модель
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
                        _originalNormRaszDataBeforeEdit = (NormRasz)currentNormRasz.Clone();
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
                return;
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


        // Общий метод для обработки сохранения
        private async Task<bool> ProcessSaveData(bool closeAfterSave)
        {
            _okPressed = false; // Сбрасываем флаг перед попыткой сохранения
            await ShowStatusMessage("Сохранение данных...");

            if (!ValidateForm())
            {
                await ShowStatusMessage("Ошибки заполнения формы");
                return false;
            }

            try
            {
                await _dbHelper.ExecuteInTransactionAsync(async () =>
                {
                    await SaveAnnDataAsync();
                    await SaveAllDataAsync();
                });

                // Эти действия выполняются ПОСЛЕ успешной транзакции
                IsRaszInserted = true; // Предполагаем, что если сохранение дошло сюда, то все списки были обработаны
                IsRaskInserted = true;
                IsKontInserted = true;
                IsDopObrInserted = true; // Если есть логика для DopObr
                _hasUnsavedChanges = false;
                // UpdateFormTitle(); // Если есть такой метод, раскомментируйте

                if (closeAfterSave)
                {
                    await ShowStatusMessage("Данные успешно сохранены! Закрытие формы...", 1500);
                    this.DialogResult = DialogResult.OK;
                    _okPressed = true;
                    this.Close();
                }
                else
                {
                    await ShowStatusMessage("Данные успешно сохранены!");
                }

                // Для режима дублирования, убедимся, что ParentId сохраняется
                if (_mode == (int)Mode.NewWorkDivision && _sourceAnnIdToCopyDetailsFrom.HasValue)
                {
                    _currentAnnData.ParentId = _sourceAnnIdToCopyDetailsFrom.Value;
                }

                return true;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при сохранении данных в ProcessSaveData");
                await ShowStatusMessage($"Ошибка при сохранении: {ex.Message}");
                // this.DialogResult = DialogResult.None; // Не закрываем при ошибке
                return false;
            }
        }

        // Сохранение данных без закрытия формы
        private async void btnSave_Click(object sender, EventArgs e)
        {
            await ProcessSaveData(false);
        }

        // Сохранение данных и закрытие формы
        private async void btnOK_Click(object sender, EventArgs e)
        {
            await ProcessSaveData(true);
        }

        private async Task SaveAnnDataAsync()
        {
            try
            {
                // Сохраняем данные в таблицу ann
                _currentAnnData.AnnID = _newAnnId;
                _currentAnnData.dateUpdate = null;
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

                    // Сброс флага IsModified после успешного обновления
                    foreach (var item in existingItems)
                    {
                        if (item is IModifiable modifiableItem)
                        {
                            modifiableItem.IsModified = false;
                        }
                    }
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
                    //await WorkDivisionLoadAsync(caller: "buffer", _bufferWorkDivision);
                    //MessageBox.Show("Данные из буфера успешно загружены", "Информация",
                    //            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // await LoadAndCloneAll(_bufferWorkDivision, _newAnnId);
                    var rasz = await _artNormService.GetRelatedNormRasz(_bufferWorkDivision);
                    _normRaszList.BulkLoad(CloneUtils.CloneList(rasz, _newAnnId, "nrId"));

                    _currentAnnData.dateCreate = DateTime.Now;

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
            if (_originalAnnData != null)
            {
                _currentAnnData.CopyPropertiesFrom(_originalAnnData);
                bindingSource1.ResetBindings(false);
            }

            if (_originalNormRaszList != null)
            {
                _normRaszList.BulkLoad(_originalNormRaszList);
            }
            if (_originalNormRaskList != null)
            {
                _normRaskList.BulkLoad(_originalNormRaskList);
            }
            if (_originalNormKontList != null)
            {
                _normKontList.BulkLoad(_originalNormKontList);
            }

            _hasUnsavedChanges = false;

            MessageBox.Show("Изменения успешно отменены и восстановлены до исходного состояния.", "Отмена изменений", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void gridViewRasz_EditFormPrepared(object sender, DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventArgs e)
        {
            var fieldNames = new List<string>
    {
        "№ оп.:", "N1", "razryd", "Text", "сек.:", "Spec", "KodProizv", "KodPodr", "Kod_ob"
    };

            var controls = fieldNames
                .Select(fn =>
                {
                    var ctrl = e.BindableControls.Find(c => c.AccessibleName == fn); // или AccessibleName
                    return ctrl != null ? new
                    {
                        FieldName = fn,
                        Control = ctrl,
                        Left = ctrl.Left,
                        Top = ctrl.Top
                    } : null;
                })
                .Where(c => c != null)
                .ToList();

            var tabOrder = controls
                .OrderBy(c => c.Left)
                .ThenBy(c => c.Top)
                .ToList();

            for (int i = 0; i < tabOrder.Count; i++)
                tabOrder[i].Control.TabIndex = i;
        }


    }
}
