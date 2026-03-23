using Dapper;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.Articul;
using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.Features.TeamWork.Interfaces;
using SewingProduction.Features.TeamWork.Operations;
using SewingProduction.Features.TeamWork.Services;
using SewingProduction.form.TeamWork.Forms;
using SewingProduction.Helpers;
using SewingProduction.Interfaces;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using BindingSource = System.Windows.Forms.BindingSource;
using MethodInvoker = System.Windows.Forms.MethodInvoker;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork_AdvanceTW : CustomForm, ITeamWorkView
    {
        #region Поля и зависимости
        private readonly DbService _dbService;
        private readonly ArtNormRepository _artNormService;
        private readonly ITeamWorkDataService _dataService;
        private readonly ITeamWorkUIService _uiService;
        private readonly ITeamWorkValidationService _validationService;
        private int _bufferWorkDivision;
        private readonly DatabaseHelper _dbHelper;
        private readonly TWGridHelper _gridHelper = new TWGridHelper();
        private int _newAnnId = -1;
        private int _selectedAnnId = -1;
        private readonly ILogger _logger = new FileLogger();
        private readonly BulkHelper _bulkHelper = new BulkHelper();
        private int _mode;
        private ArtNormN _currentAnnData;
        private ArtNormN _originalAnnData;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MyDataART InitialArtData { get; set; }
        private readonly Debouncer _sekDebouncer = new Debouncer();
        private NormRasz _originalNormRaszDataBeforeEdit;
        private bool _bindingsInitialized = false;
        private bool _listChangedHandlersAttached = false;
        private bool _rowStyleHandlersAttached = false;
        private bool _focusedRowHandlersAttached = false;


        private BindingList<NormRasz> _normRaszList;
        private BindingSource _normRaszBindingSource;
        private BindingList<NormRask> _normRaskList;
        private BindingSource _normRaskBindingSource;
        private BindingList<NormKont> _normKontList;
        private BindingSource _normKontBindingSource;
        private RaszOperationsController _raszOpsController;
        private SecondsAggregator _secondsAggregator;
        private BufferImportService _bufferImportService;
        private SavePipeline _savePipeline;
        private CloneService _cloneService;

        // Публичный метод для UI‑сервиса
        public void RefreshAllGridsForService()
        {
            try
            {
                gridControlRasz?.RefreshDataSource();
                gridControlRaskr?.RefreshDataSource();
                gridControlKont?.RefreshDataSource();
            }
            catch { }
        }
        public async Task ShowStatusForService(string message, int delayMs)
        {
            await ShowStatusMessage(message, delayMs);
        }
        public void HighlightControlForService(Control control) => HighlightControl(control);
        public void UnhighlightControlForService(Control control) => UnhighlightControl(control);
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
        private DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler _raszPopupHandler;
        private DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler _kontPopupHandler;

        // Поле для хранения последней выбранной операции
        private NormRasz _lastFocusedRaszOperation = null;

        /// <summary>
        /// Структура для возврата результата диалога выбора позиции операции
        /// </summary>
        private struct OperationInsertChoice
        {
            public int OperationN { get; set; }
            public int OperationN1 { get; set; }
            public bool IsSuboperation { get; set; }
            public bool Cancel { get; set; }
            public bool ConvertMainToSuboperation { get; set; } // Нужно ли преобразовать основную операцию в подоперацию
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [NotifyParentProperty(true)]
        public ArtNormN CreatedAnn { get; private set; }

        private bool _isSelectionFormOpen = false;
        private bool _hasUnsavedChanges = false;
        private bool _isInitialLoading = false; // Флаг для отслеживания начальной загрузки
        private List<KodProizvModel> kodProizvList;
        private List<PodrVyazModel> podrVyazList;
        private List<OborudShvModel> oborudShvList;
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<string, System.Reflection.PropertyInfo> _artNormNPropCache = new System.Collections.Concurrent.ConcurrentDictionary<string, System.Reflection.PropertyInfo>();
        private readonly Dictionary<Control, System.Reflection.PropertyInfo> _controlToArtNormProperty = new Dictionary<Control, System.Reflection.PropertyInfo>();
        #endregion

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
        public string CurrentArticul => (_currentAnnData?.Articul ?? CreatedAnn?.Articul) ?? string.Empty;

        #region Конструкторы и базовая настройка
        public TeamWork_AdvanceTW() { InitializeComponent(); }
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
            gridViewRasz.OptionsView.ShowIndicator = true;
            gridViewRasz.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            gridViewRasz.Appearance.Row.ForeColor = Color.Black;
            gridViewRasz.Appearance.FocusedRow.ForeColor = Color.Black;
            gridViewRasz.Appearance.FocusedCell.ForeColor = Color.Black;
            gridViewRasz.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;

            // Настройка мультиселекта с галочками
            gridViewRasz.OptionsSelection.MultiSelect = true;
            gridViewRasz.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridViewRasz.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;

            // DnD подписки для Rasz перенесены в RaszOperationsController

            // Группировка по основному номеру операции (N)
            ConfigureRaszGrouping();

            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);
            _artNormService = new ArtNormRepository(_dbHelper);
            // Инициализируем сервисы декомпозиции (пока без DI контейнера)
            // Адаптеры для интерфейсов до внедрения DI
            _dataService = new TeamWorkDataServiceAdapter(_artNormService, _dbService);
            _uiService = new TeamWorkUIServiceAdapter(this);
            _validationService = new TeamWorkValidationServiceAdapter();
         //   ThemeManager.UpdateTheme(this);

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

            // Проставляем теги для customHeaderButtons (используются в общих обработчиках кликов)
            InitHeaderButtonTags();

            // Подписка на клики по кнопкам заголовков
            try
            {
                if (layoutControlGroup1 != null)
                    layoutControlGroup1.CustomButtonClick += LayoutControlGroup1_CustomButtonClick;
                if (layoutControlGroup10 != null)
                    layoutControlGroup10.CustomButtonClick += LayoutControlGroup10_CustomButtonClick;
            }
            catch { }
        }
        #endregion

        #region Кнопки заголовков (инициализация и обработчики)
        // Устанавливает Tag для кастомных кнопок заголовков групп лейаута
        private void InitHeaderButtonTags()
        {
            try
            {
                // layoutControlGroup1: Undo / Save / SaveAs
                if (layoutControlGroup1 != null && layoutControlGroup1.CustomHeaderButtons != null)
                {
                    if (layoutControlGroup1.CustomHeaderButtons.Count > 0)
                    {
                        var b0 = layoutControlGroup1.CustomHeaderButtons[0] as DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton;
                        if (b0 != null) b0.Tag = "tw:undo";
                    }
                    if (layoutControlGroup1.CustomHeaderButtons.Count > 1)
                    {
                        var b1 = layoutControlGroup1.CustomHeaderButtons[1] as DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton;
                        if (b1 != null) b1.Tag = "tw:save";
                    }
                    if (layoutControlGroup1.CustomHeaderButtons.Count > 2)
                    {
                        var b2 = layoutControlGroup1.CustomHeaderButtons[2] as DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton;
                        if (b2 != null) b2.Tag = "tw:save-as";
                    }
                }

                // layoutOpis_t: управление нумерацией и преобразованием
                if (layoutControlGroup10 != null && layoutControlGroup10.CustomHeaderButtons != null)
                {
                    if (layoutControlGroup10.CustomHeaderButtons.Count > 0)
                    {
                        var b0 = layoutControlGroup10.CustomHeaderButtons[0] as DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton;
                        if (b0 != null) b0.Tag = "op:move-up";
                    }
                    if (layoutControlGroup10.CustomHeaderButtons.Count > 1)
                    {
                        var b1 = layoutControlGroup10.CustomHeaderButtons[1] as DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton;
                        if (b1 != null) b1.Tag = "op:move-down";
                    }
                    if (layoutControlGroup10.CustomHeaderButtons.Count > 2)
                    {
                        var b2 = layoutControlGroup10.CustomHeaderButtons[2] as DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton;
                        if (b2 != null) b2.Tag = "op:convert";
                    }
                }
            }
            catch { }
        }

        // Обработчик кликов по header-buttons: блок общих действий (undo/save/save-as)
        private void LayoutControlGroup1_CustomButtonClick(object sender, BaseButtonEventArgs e)
        {
            var gb = e.Button as DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton;
            var tag = gb?.Tag as string;
            try
            {
                switch (tag)
                {
                    case "tw:undo":
                        btnCancel_Click(sender, EventArgs.Empty);
                        break;
                    case "tw:save":
                        btnSave_Click(sender, EventArgs.Empty);
                        break;
                    case "tw:save-as":
                        btnOK_Click(sender, EventArgs.Empty);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, $"Ошибка обработки нажатия header-button layoutControlGroup1: {tag}");
            }
        }
        #endregion

        // Обработчик кликов по header-buttons: блок управления операциями (вверх/вниз/валидация нумерации)
        private void LayoutControlGroup10_CustomButtonClick(object sender, BaseButtonEventArgs e)
        {
            var gb = e.Button as DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton;
            var tag = gb?.Tag as string;
            try
            {
                switch (tag)
                {
                    case "op:move-up":
                        MoveOperationUp();
                        break;
                    case "op:move-down":
                        MoveOperationDown();
                        break;
                    case "op:convert":
                        //ValidateAndFixOperationNumbers();
                        RecalculateAllOperationNumbers();
                        //RecalculateNumbers();
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, $"Ошибка обработки нажатия header-button layoutControlGroup10: {tag}");
            }
        }

        #region Привязки полей и обработчики изменений ANN
        private void AttachChangeHandlers()
        {
            // Инициализируем маппинг Control -> PropertyInfo один раз
            _controlToArtNormProperty.Clear();
            var artType = typeof(ArtNormN);
            _controlToArtNormProperty[nameTextBox] = artType.GetProperty(nameof(ArtNormN.Articul));
            _controlToArtNormProperty[groupTextBox] = artType.GetProperty(nameof(ArtNormN.grup));
            _controlToArtNormProperty[modelTextBox] = artType.GetProperty(nameof(ArtNormN.Mod));
            _controlToArtNormProperty[secTimeTextBox] = artType.GetProperty(nameof(ArtNormN.Sek));
            _controlToArtNormProperty[textBoxKomment] = artType.GetProperty(nameof(ArtNormN.Komment));
            _controlToArtNormProperty[textBoxReco] = artType.GetProperty(nameof(ArtNormN.Reco));
            _controlToArtNormProperty[dateCreate] = artType.GetProperty(nameof(ArtNormN.dateCreate));
            _controlToArtNormProperty[designerComboBox] = artType.GetProperty(nameof(ArtNormN.Diz));
            _controlToArtNormProperty[constructorComboBox] = artType.GetProperty(nameof(ArtNormN.Constr));

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
            if (_isInitialLoading) return; // Игнорируем изменения во время начальной загрузки

            _hasUnsavedChanges = true;
            var control = sender as Control;
            if (control == null || _originalAnnData == null || _currentAnnData == null)
                return;

            // Пробуем взять PropertyInfo напрямую из словаря Control->PropertyInfo
            if (!_controlToArtNormProperty.TryGetValue(control, out var propInfo) || propInfo == null)
            {
            string propertyName = GetPropertyNameFromControl(control);
            if (string.IsNullOrEmpty(propertyName))
                return;
                var type = typeof(ArtNormN);
                propInfo = _artNormNPropCache.GetOrAdd(propertyName, name => type.GetProperty(name));
                if (propInfo == null) return;
            }

            try
            {
                object currentValue = propInfo.GetValue(_currentAnnData);
                object originalValue = propInfo.GetValue(_originalAnnData);

                // Сравниваем значения (учитываем null)
                bool areEqual = Equals(currentValue, originalValue);

                if (!areEqual)
                {
                    _uiService?.HighlightControl(control, true);
                }
                else
                {
                    _uiService?.HighlightControl(control, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, $"Ошибка при сравнении значений для контроля {control.Name}");
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
                if (_bindingsInitialized) return;
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

                // Убедиться, что группировка по операциям применяется
                ConfigureRaszGrouping();

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
            finally
            {
                if (!_listChangedHandlersAttached)
                {
                    _normRaszList.ListChanged += OnNormRaszListChanged;
                    _normRaszList.ListChanged += OnDataChanged;
                    _normRaskList.ListChanged += OnDataChanged;
                    _normKontList.ListChanged += OnDataChanged;
                    _listChangedHandlersAttached = true;
                }
                bool allowDelete = ComputeCanEdit(); // см. метод ниже
                if (allowDelete)
                {
                    _raszPopupHandler ??= CreateRaszContextMenu(gridViewRasz, _normRaszList, r => r.nrID, _deletedNormRaszIds);
                    _kontPopupHandler ??= ShowPopUp(gridViewKont, _normKontList, k => k.nkId, _deletedNormKontIds);
                }
                else
                {
                    _raszPopupHandler = null;
                    _kontPopupHandler = null;
                }
                if (_presenter != null)
                {
                    await _presenter.InitializeAsync();
                    _presenter.Attach();
                    UpdateContextMenus(); // <-- место подключения меню
                }
                _bindingsInitialized = true;
            }
        }
        private bool ComputeCanEdit()
        {
            // Гибкое правило: редактируемо, если дата не установлена ИЛИ статус предварительный ИЛИ спец. режимы
            var dateOk = _currentAnnData?.dateUpdate == null || _currentAnnData.dateUpdate == DateTime.MinValue;
            var isPrelim = _currentAnnData?.Status == (int)Status.Preliminary;
            var modeOk = _mode == (int)Mode.NewWorkDivision || _mode == (int)Mode.ArchAndCopy || _mode == (int)Mode.Clone;
            return dateOk || isPrelim || modeOk;
        }
        private void UpdateContextMenus()
        {
            // Отвязать, на всякий случай
            _presenter?.DetachPopupMenus();

            if (!ComputeCanEdit() || _presenter == null) return;

            // Хэндлеры уже построены в InitializeBindingsAsync
            if (_raszPopupHandler != null || _kontPopupHandler != null)
                (_presenter as SewingProduction.Features.TeamWork.Services.TeamWorkPresenter)
                    ?.AttachPopupMenus(_raszPopupHandler, _kontPopupHandler, gridViewKont);
        }
        #endregion
        private void AttachDeleteContextMenu<T>(GridView view, BindingList<T> bindingList, Func<T, int> getId = null, List<int> deletedIds = null)
                    where T : class
        {
            try
            {
                view.PopupMenuShowing += UIHelper.CreateContextMenu(view, bindingList, getId, deletedIds);
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при удалении строки TeamWork_AdvanceTW");
            }
        }

        #region Контекстные меню и массовое удаление
        private PopupMenuShowingEventHandler ShowPopUp<T>(
            GridView view,
            BindingList<T> bindingList,
            Func<T, int> getId,
            List<int> deletedIds
        ) where T : class
        {
            return (s, e) =>
            {
                if (e.MenuType != GridMenuType.Row)
                    return;

                var menu = e.Menu;
                var deleteItem = new DevExpress.Utils.Menu.DXMenuItem("Удалить строку", (_, __) =>
                {
                    int rowHandle = e.HitInfo.RowHandle;
                    if (!view.IsValidRowHandle(rowHandle)) return;

                    var rowObj = view.GetRow(rowHandle) as T;
                    if (rowObj == null) return;

                    // Добавляем в список удалённых
                    if (getId != null && deletedIds != null)
                    {
                        int id = getId(rowObj);
                        if (id > 0)
                            deletedIds.Add(id);
                    }

                    // Удаляем строку
                    bindingList.Remove(rowObj);
                    TWGridHelper.sortGridView(gridViewRasz);
                    // Переносим фокус на новую строку
                    view.GridControl.BeginInvoke(new Action(() =>
                    {
                        if (view.DataRowCount == 0) return;

                        // Если удалили не последнюю строку — фокус остаётся на том же индексе
                        // Если удалили последнюю — фокус на новую последнюю строку
                        int newRowHandle = Math.Min(rowHandle, view.RowCount - 1);
                        newRowHandle = view.GetVisibleRowHandle(newRowHandle);

                        if (view.IsValidRowHandle(newRowHandle))
                        {
                            view.FocusedRowHandle = newRowHandle;
                            view.MakeRowVisible(newRowHandle);
                        }
                    }));
                });

                menu.Items.Add(deleteItem);
            };
        }

        /// <summary>
        /// Специальный метод для подключения контекстного меню удаления для norm_rasz с перенумерацией
        /// </summary>
        private void AttachDeleteContextMenuForRasz(GridView view, BindingList<NormRasz> bindingList, Func<NormRasz, int> getId = null, List<int> deletedIds = null)
        {
            try
            {
                view.PopupMenuShowing += CreateRaszContextMenu(view, bindingList, getId, deletedIds);
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при удалении строки norm_rasz с перенумерацией");
            }
        }

        /// <summary>
        /// Специальный обработчик контекстного меню для norm_rasz с перенумерацией операций
        /// </summary>
        private PopupMenuShowingEventHandler ShowPopUpForRasz(
            GridView view,
            BindingList<NormRasz> bindingList,
            Func<NormRasz, int> getId,
            List<int> deletedIds
        )
        {
            // Заменено на CreateRaszContextMenu + UIHelper.CreateContextMenu
            return CreateRaszContextMenu(view, bindingList, getId, deletedIds);
        }

        /// <summary>
        /// Удаляет все выбранные операции с перенумерацией
        /// </summary>
        /// <param name="view">GridView с операциями</param>
        /// <param name="bindingList">Список операций</param>
        /// <param name="getId">Функция получения ID</param>
        /// <param name="deletedIds">Список удаленных ID</param>
        private async void DeleteSelectedOperations(GridView view, BindingList<NormRasz> bindingList, Func<NormRasz, int> getId, List<int> deletedIds)
        {
            try
            {
                int[] selectedRows = view.GetSelectedRows();
                if (selectedRows == null || selectedRows.Length == 0)
                {
                    MessageBox.Show("Нет выбранных строк для удаления.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //// Подтверждение удаления
                //var result = MessageBox.Show(
                //    $"Удалить {selectedRows.Length} выбранных операций?\n\nЭто действие нельзя отменить.",
                //    "Подтверждение удаления",
                //    MessageBoxButtons.YesNo,
                //    MessageBoxIcon.Question,
                //    MessageBoxDefaultButton.Button2);

                //if (result != DialogResult.Yes)
                //    return;

                // Получаем объекты операций для удаления
                var operationsToDelete = new List<NormRasz>();
                foreach (int rowHandle in selectedRows)
                {
                    if (view.IsValidRowHandle(rowHandle))
                    {
                        var operation = view.GetRow(rowHandle) as NormRasz;
                        if (operation != null)
                        {
                            operationsToDelete.Add(operation);
                        }
                    }
                }

                if (operationsToDelete.Count == 0)
                {
                    MessageBox.Show("Не удалось получить данные выбранных операций.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Собираем ID для списка удаленных (для последующего удаления из БД)
                if (getId != null && deletedIds != null)
                {
                    foreach (var operation in operationsToDelete)
                    {
                        int id = getId(operation);
                        if (id > 0)
                            deletedIds.Add(id);
                    }
                }

                // Проверяем, если удаляем последнюю сфокусированную операцию
                if (_lastFocusedRaszOperation != null && operationsToDelete.Contains(_lastFocusedRaszOperation))
                {
                    _lastFocusedRaszOperation = null;
                }

                // Батч‑обновление: удаление, затем ResetBindings/Sort, затем UI (пересчёт отключён)
                gridViewRasz.BeginDataUpdate();
                try
                {
                    foreach (var operation in operationsToDelete)
                    {
                        bindingList.Remove(operation);
                    }
                    // Точечное обновление первой строки после удаления
                    if (view.DataRowCount > 0)
                    {
                        var firstHandle = view.GetVisibleRowHandle(0);
                        if (view.IsValidRowHandle(firstHandle)) view.RefreshRow(firstHandle);
                    }
                    TWGridHelper.sortGridView(gridViewRasz);

                    // Централизованная пост‑обработка UI: очистка выделения и фокус на первой строке
                    NormRasz first = null;
                    if (view.DataRowCount > 0)
                    {
                        int hr = view.GetVisibleRowHandle(0);
                        if (view.IsValidRowHandle(hr)) first = view.GetRow(hr) as NormRasz;
                    }
                    ApplyPostStructureUi(first, true);
                }
                finally
                {
                    gridViewRasz.EndDataUpdate();
                }

                await _logger.LogEventAsync($"Массово удалено операций: {operationsToDelete.Count}", "DeleteSelectedOperations");

                // Показываем результат
                MessageBox.Show($"Успешно удалено {operationsToDelete.Count} операций.",
                               "Удаление завершено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при массовом удалении операций");
                MessageBox.Show($"Ошибка при удалении операций: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Добавление операций (диалог и вставка)
        /// <summary>
        /// Показывает диалог выбора типа и позиции для новой операции
        /// </summary>
        /// <param name="currentOperation">Текущая операция, относительно которой делается вставка</param>
        /// <returns>Выбор пользователя</returns>
        private OperationInsertChoice ShowOperationInsertDialog(NormRasz currentOperation)
        {
            try
            {
                int maxN = _normRaszList?.Select(x => x.N).DefaultIfEmpty(0).Max() ?? 0;

                // Проверяем, есть ли подоперации у текущей операции
                bool hasSuboperations = _normRaszList?.Any(r => r.N == currentOperation.N && r.N1 > 0) ?? false;
                int maxN1InOperation = hasSuboperations ?
                    _normRaszList.Where(r => r.N == currentOperation.N).Max(r => r.N1) : 0;

                // Определяем варианты в зависимости от типа текущей операции
                if (currentOperation.N1 == 0)
                {
                    // Текущая операция основная - предлагаем сначала подоперацию
                    string subopMessage;
                    if (!hasSuboperations)
                    {
                        // Если у операции еще нет подопераций, объясняем что произойдет
                        subopMessage = $"Добавить подоперацию к операции №{currentOperation.N}?\n\n" +
                                      $"Текущая операция №{currentOperation.N} станет №{currentOperation.N}.1\n" +
                                      $"Новая подоперация получит номер {currentOperation.N}.2";
                    }
                    else
                    {
                        // Если уже есть подоперации, просто добавляем следующую
                        subopMessage = $"Добавить подоперацию к операции №{currentOperation.N}?\n" +
                                      $"Новая подоперация получит номер {currentOperation.N}.{maxN1InOperation + 1}";
                    }

                    var subopResult = MessageBox.Show(subopMessage, "Тип новой операции",
                                                     MessageBoxButtons.YesNoCancel,
                                                     MessageBoxIcon.Question,
                                                     MessageBoxDefaultButton.Button1);

                    if (subopResult == DialogResult.Yes)
                    {
                        if (!hasSuboperations)
                        {
                            // Первая подоперация - основная операция станет X.1, новая X.2
                            return new OperationInsertChoice
                            {
                                OperationN = currentOperation.N,
                                OperationN1 = 2, // Новая подоперация будет X.2
                                IsSuboperation = true,
                                ConvertMainToSuboperation = true // Флаг для преобразования основной операции
                            };
                        }
                        else
                        {
                            // Обычная подоперация к уже существующим
                            return new OperationInsertChoice
                            {
                                OperationN = currentOperation.N,
                                OperationN1 = maxN1InOperation + 1,
                                IsSuboperation = true
                            };
                        }
                    }
                    else if (subopResult == DialogResult.Cancel)
                    {
                        return new OperationInsertChoice { Cancel = true };
                    }
                    // Если "Нет" - продолжаем к выбору основной операции
                }
                else
                {
                    // Текущая операция подоперация - сначала предлагаем вставить подоперацию
                    string subopMessage = $"Добавить подоперацию после №{currentOperation.N}.{currentOperation.N1}?\n" +
                                         $"Новая подоперация получит номер {currentOperation.N}.{currentOperation.N1 + 1}";

                    var subopResult = MessageBox.Show(subopMessage, "Тип новой операции",
                                                     MessageBoxButtons.YesNoCancel,
                                                     MessageBoxIcon.Question,
                                                     MessageBoxDefaultButton.Button1);

                    if (subopResult == DialogResult.Yes)
                    {
                        // Подоперация после текущей подоперации
                        return new OperationInsertChoice
                        {
                            OperationN = currentOperation.N,
                            OperationN1 = currentOperation.N1 + 1,
                            IsSuboperation = true
                        };
                    }
                    else if (subopResult == DialogResult.Cancel)
                    {
                        return new OperationInsertChoice { Cancel = true };
                    }
                    // Если "Нет" - продолжаем к выбору основной операции
                }

                // Второй диалог для выбора основной операции
                string mainOpMessage = $"Добавить основную операцию:\n\n" +
                                      $"Да - После операции №{currentOperation.N} (получит номер {currentOperation.N + 1})\n" +
                                      $"Нет - В конец списка (получит номер {maxN + 1})";

                var mainOpResult = MessageBox.Show(mainOpMessage, "Позиция основной операции",
                                                  MessageBoxButtons.YesNoCancel,
                                                  MessageBoxIcon.Question,
                                                  MessageBoxDefaultButton.Button1);

                if (mainOpResult == DialogResult.Yes)
                {
                    // Основная операция после текущей
                    return new OperationInsertChoice
                    {
                        OperationN = currentOperation.N + 1,
                        OperationN1 = 0,
                        IsSuboperation = false
                    };
                }
                else if (mainOpResult == DialogResult.No)
                {
                    // В конец списка
                    return new OperationInsertChoice
                    {
                        OperationN = maxN + 1,
                        OperationN1 = 0,
                        IsSuboperation = false
                    };
                }
                else
                {
                    return new OperationInsertChoice { Cancel = true };
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка в диалоге выбора позиции операции");
                return new OperationInsertChoice { Cancel = true };
            }
        }

        /// <summary>
        /// Добавляет новую операцию с возможностью выбора позиции вставки
        /// </summary>
        /// <param name="insertAfterOperation">Операция, после которой нужно вставить новую (null = в конец)</param>
        /// <param name="forceAppendToEnd">Принудительно добавить в конец без диалога выбора</param>
        private async void AddNewRaszOperation(NormRasz targetOperation = null, bool forceAppendToEnd = false)
        {
            try
            {
                GridView view = gridControlRasz.MainView as GridView;

                using (var selectionForm = new NormOperNew(_selectedAnnId))
                {
                    var result = selectionForm.ShowDialog();

                    if (result == DialogResult.OK && selectionForm.SelectedRowData != null)
                    {
                        var selectedData = selectionForm.SelectedRowData;
                        selectedData.IsNew = true;
                        selectedData.IsBeingAdded = true; // помечаем как добавляемую в текущей сессии

                        // Детерминированные сценарии без вопросов (батч‑обновление UI):
                        // 1) targetOperation == null → новая глава №1 с глобальным сдвигом вниз
                        // 2) иначе → новая глава сразу после выбранной (даже если выбрана подоперация)
                        gridViewRasz.BeginDataUpdate();
                        try
                        {
                            if (targetOperation == null)
                            {
                                OperationNumberingService.InsertMainAtStart(_normRaszList, selectedData);
                        }
                        else
                        {
                                OperationNumberingService.InsertMainAfter(_normRaszList, targetOperation.N, selectedData);
                        }
                            _normRaszList.Add(selectedData);
                            FinalizeRaszBatch(selectedData, false);
                        }
                        finally
                        {
                            try { gridViewRasz.EndDataUpdate(); } catch { }
                        }

                        // Открываем форму редактирования через небольшую задержку
                        gridViewRasz.GridControl.BeginInvoke(new Action(() =>
                        {
                            _isCustomEditFormOpen = true;
                            gridViewRasz.ShowEditForm();
                        }));

                        await _logger.LogEventAsync($"Добавлена новая операция №{selectedData.N}.{selectedData.N1} через контекстное меню", "AddNewRaszOperation");
                    }
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при добавлении операции через контекстное меню");
                MessageBox.Show($"Ошибка при добавлении операции: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        /// <summary>
        /// Проверяет наличие дублирующихся номеров операций
        /// </summary>
        /// <param name="n">Номер операции</param>
        /// <param name="n1">Номер подоперации</param>
        /// <returns>True если такой номер уже существует</returns>
        private bool HasDuplicateNumbers(int n, int n1)
        {
            return _validationService?.HasDuplicateNumbers(n, n1) ?? (_normRaszList?.Any(r => r.N == n && r.N1 == n1) ?? false);
        }

        /// <summary>
        /// Обработчик изменения фокуса для сохранения последней выбранной операции
        /// </summary>
        private void GridViewRasz_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var gridView = sender as GridView;
            if (gridView == null) return;

            // Сохраняем последнюю валидную операцию (не newRow)
            if (e.FocusedRowHandle >= 0 && e.FocusedRowHandle < gridView.DataRowCount && !gridView.IsNewItemRow(e.FocusedRowHandle))
            {
                var currentRow = gridView.GetRow(e.FocusedRowHandle) as NormRasz;
                if (currentRow != null)
                {
                    _lastFocusedRaszOperation = currentRow;
                }
            }
        }
        #region Реакция на изменения списков / загрузка формы
        private void OnNormRaszListChanged(object sender, ListChangedEventArgs e)
        {
            // Запускаем отложенный пересчёт Sek только если не идёт начальная загрузка
            if (!_isInitialLoading)
            {
                _sekDebouncer.Debounce(5, async () =>
                {
                    _secondsAggregator?.RecalculateAndBind();
                });
            }

            OnDataChanged(sender, e);
        }
        private async void TeamWork_AdvanceTW_Load(object sender, EventArgs e)
        {
            try
            {
                _isInitialLoading = true; // Устанавливаем флаг начальной загрузки

                await this.InvokeAsync(() =>
                {
                    _gridHelper.LoadGridViewSettings(gridViewRaskr, "AdvanceTW_gridViewRaskrLayout.xml");
                    _gridHelper.LoadGridViewSettings(gridViewKont, "AdvanceTW_gridViewKontLayout.xml");
                    _gridHelper.LoadGridViewSettings(gridViewRasz, "AdvanceTW_gridViewRaszLayout.xml");
                });

                await InitializeBindingsAsync();
                await LoadAndBindFioListsAsync();

                // Инициализация Presenter после инициализации биндингов
                if (_presenter == null && _normRaszList != null && _normRaskList != null && _normKontList != null)
                {
                    _presenter = new TeamWorkPresenter(
                        view: this,
                        logger: _logger,
                        artNormService: _artNormService,
                        dbHelper: _dbHelper,
                        gridHelper: _gridHelper,
                        raszGrid: gridControlRasz,
                        raszView: gridViewRasz,
                        rasz: _normRaszList,
                        rask: _normRaskList,
                        kont: _normKontList,
                        raszSource: _normRaszBindingSource,
                        annSource: bindingSource1,
                        processSave: (close) => ProcessSaveData(close),
                        importFromBuffer: () => ImportFromBufferPresenterAsync(),
                        addOperation: () => AddOperationPresenterAsync()
                    );
                }
                if (_presenter != null)
                {
                    await _presenter.InitializeAsync();
                    _presenter.Attach();
                    UpdateContextMenus(); // <-- место подключения меню
                }

                // Инициализация контроллера операций Rasz после готовности привязок
                if (_raszOpsController == null && _normRaszList != null && _normRaszBindingSource != null)
                {
                    _raszOpsController = new RaszOperationsController(
                        _normRaszList,
                        gridControlRasz,
                        gridViewRasz,
                        _normRaszBindingSource,
                        _logger,
                        () => TWGridHelper.sortGridView(gridViewRasz),
                        (op, clearSel) => ApplyPostStructureUi(op, clearSel)
                    );
                    _raszOpsController.Attach();
                }

                // Инициализация агрегатора секунд
                if (_secondsAggregator == null && _normRaszList != null && bindingSource1 != null)
                {
                    _secondsAggregator = new SecondsAggregator(
                        _normRaszList,
                        bindingSource1,
                        _logger,
                        (act) => { try { if (!this.IsDisposed && this.IsHandleCreated) this.BeginInvoke((MethodInvoker)(() => act())); } catch { } }
                    );
                }

                // Сервисы сохранения и клонирования
                if (_savePipeline == null)
                {
                    _savePipeline = new SavePipeline(_dbHelper, _logger, () => RecalculateAllOperationNumbers());
                }
                if (_cloneService == null)
                {
                    _cloneService = new CloneService(_artNormService);
                }

                if (_bufferImportService == null && _normRaszList != null && _normRaszBindingSource != null)
                {
                    _bufferImportService = new BufferImportService(
                        _artNormService,
                        this,
                        _dbHelper,
                        _logger,
                        _normRaszList,
                        _normRaszBindingSource,
                        gridViewRasz,
                        () => TWGridHelper.sortGridView(gridViewRasz),
                        (op, clearSel) => ApplyPostStructureUi(op, clearSel)
                    );
                }

                await this.InvokeAsync(() =>
                {
                if (gridViewKont != null && gridViewKont.Columns["Text"] != null)
                {
                    gridViewKont.Columns["Text"].OptionsColumn.AllowEdit = false;
                }
                });

                // Обновляем текстовое поле буфера из глобального состояния или локального
                UpdateBufferDisplay();

                // Подписываемся на изменения глобального буфера
                TeamWorkBuffer.BufferChanged += OnBufferChanged;

                if (_bufferWorkDivision > 0)
                {
                    var annData = await _artNormService.GetArtNormDataById(_bufferWorkDivision);
                    if (annData != null)
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            textBoxBuffer.Text = $"группа: {annData.grup.TrimEnd(' ') ?? ""}, \r" +
                                                 $"модель: {annData.Mod.TrimEnd(' ') ?? ""}, \r" +
                                                 $"артикул: {annData.Articul.TrimEnd(' ') ?? ""}";
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
                        _lastFocusedRaszOperation = null; // Сбрасываем последнюю операцию
                        // Автоматически добавляем две стандартные строки в norm_kont
                        AddStandardKontRows();
                        break;
                    case (int)Mode.ArchAndCopy:
                        this.Text = $"Архив+копия";//. Артикул: {CreatedAnn.ArticulModel}"
                        await _cloneService.CloneFromAsync(_selectedAnnId, _newAnnId, _normRaszList, _normRaskList, _normKontList);
                        LoadGridImage(pictureBox1, annId: _selectedAnnId);
                        _lastFocusedRaszOperation = null; // Сбрасываем последнюю операцию
                        // Автоматически добавляем две стандартные строки в norm_kont
                        AddStandardKontRows();
                        _currentAnnData.dateCreate = DateTime.Now;
                        break;
                    case (int)Mode.Edit:
                        this.Text = $"Редактировать";//. Артикул: {_selectedAnnId.ArticulModel}"
                        await _cloneService.LoadForEditAsync(_selectedAnnId, _normRaszList, _normRaskList, _normKontList);
                        LoadGridImage(pictureBox1, annId: _selectedAnnId);
                        break;
                    case (int)Mode.Clone:
                        this.Text = $"Дубль";//. Артикул: {CreatedAnn.ArticulModel}"
                        await _cloneService.CloneFromAsync(_selectedAnnId, _newAnnId, _normRaszList, _normRaskList, _normKontList);
                        _lastFocusedRaszOperation = null; // Сбрасываем последнюю операцию
                        // Автоматически добавляем две стандартные строки в norm_kont
                        AddStandardKontRows();
                        _currentAnnData.dateCreate = DateTime.Now;
                        break;

                    case (int)Mode.Kit:
                        // Режим создания комплекта: аналогичен предварительному, но
                        // отличается заголовком формы и тем, что операции из
                        // выбранных разделений будут автоматически перенесены
                        // в список _normRaszList в конце загрузки.
                        this.Text = "Создание комплекта";
                        nameTextBox.Enabled = true;
                        // Очищаем списки операций и добавляем стандартные строки в norm_kont
                        _normRaszList.Clear();
                        _normRaskList.Clear();
                        _normKontList.Clear();
                        _lastFocusedRaszOperation = null;
                        AddStandardKontRows();
                        break;
                }
                await LoadAnnDataAsync();
                // TeamWork_AdvanceTW.cs, после того как _currentAnnData загружен и списки заполнены
                var snapSvc = new RtSnapshotService(_dbService, _dbHelper, _logger);
                await snapSvc.EnsurePendingSnapshotAsync(_newAnnId);
                // если открыли уже утверждённую РТ в режиме редактирования — снимаем дату в UI
                if (_mode == (int)Mode.Edit && _currentAnnData?.dateUpdate != null)
                {
                    _currentAnnData.dateUpdate = null;         // только в модели/UI, без немедленного апдейта в БД
                    bindingSource1.ResetBindings(false);       // обновить поля на форме
                }
                _hasUnsavedChanges = false;

                // Если выбран режим комплекта, автоматически подгружаем операции из буфера
                if (_mode == (int)Mode.Kit)
                {
                    try
                    {
                        await InsertOperationsFromBufferAsync();
                    }
                    catch (Exception ex)
                    {
                        await _logger.LogErrorAsync(ex, "Ошибка при автозагрузке операций из буфера в режиме комплекта");
                    }
                }

                AttachChangeHandlers();
                kodProizvList = await _dbService.GetListAsync<KodProizvModel>("SELECT kod_proizv, text_proizv FROM kod_proizv", null);
                podrVyazList = await _dbService.GetListAsync<PodrVyazModel>("SELECT kod_vyaz, text_vyaz, kod_proizv FROM podr_vyaz", null);
                oborudShvList = await _dbService.GetListAsync<OborudShvModel>("SELECT kod_ob, text_ob FROM spOborudShv", null);

                repositoryItemLookUpEdit_kodProizv.DataSource = kodProizvList;
                repositoryItemLookUpEdit_kodProizv.DisplayMember = "text_proizv";
                repositoryItemLookUpEdit_kodProizv.ValueMember = "kod_proizv";
                repositoryItemLookUpEdit_kodProizv.NullText = "[Выберите значение]";
                repositoryItemLookUpEdit_podrVyaz.DataSource = podrVyazList;
                repositoryItemLookUpEdit_podrVyaz.DisplayMember = "text_vyaz";
                repositoryItemLookUpEdit_podrVyaz.ValueMember = "kod_vyaz";
                repositoryItemLookUpEdit_podrVyaz.NullText = "[Выберите значение]";
                repositoryItemLookUpEdit_oborudShv.DataSource = oborudShvList;
                repositoryItemLookUpEdit_oborudShv.DisplayMember = "text_ob";
                repositoryItemLookUpEdit_oborudShv.ValueMember = "kod_ob";
                repositoryItemLookUpEdit_oborudShv.NullText = "[Выберите значение]";

                repositoryItemLookUpEdit_oborudShv.EditValueChanged += async (s, e) =>
                {
                    var editor = s as LookUpEdit;
                    if (editor?.EditValue is int newKodOb)
                    {
                        // 1) Проставляем код оборудования
                        gridViewRasz.SetFocusedRowCellValue("KodOb", newKodOb);

                        // 2) Проставляем текст оборудования в Obor и TextOb
                        var obItem = oborudShvList?.FirstOrDefault(x => x.kod_ob == newKodOb);
                        if (obItem != null)
                        {
                            gridViewRasz.SetFocusedRowCellValue("Obor", obItem.text_ob);
                            gridViewRasz.SetFocusedRowCellValue("TextOb", obItem.text_ob);
                        }

                        // 3) Подтягиваем spec
                        var spec = await _artNormService.GetSpecByOborudKod(newKodOb);
                        if (!string.IsNullOrEmpty(spec))
                        {
                            gridViewRasz.SetFocusedRowCellValue("Spec", spec);
                        }
                    }
                };

                repositoryItemLookUpEdit_kodProizv.EditValueChanged += (s, e) =>
                {
                    if (gridViewRasz.FocusedRowHandle < 0)
                        return;

                    if (s is LookUpEdit editor)
                    {
                        var value = editor.EditValue;
                        if (value != null && int.TryParse(value.ToString(), out int kodProizv))
                        {
                            // теперь безопасно использовать kodProizv
                            UpdateFilteredPodrVyaz(kodProizv);

                            // Проставим текст производства
                            var prodItem = kodProizvList?.FirstOrDefault(x => x.kod_proizv == kodProizv);
                            if (prodItem != null)
                            {
                                gridViewRasz.SetFocusedRowCellValue("TextProizv", prodItem.text_proizv);
                            }
                        }
                    }
                };


                repositoryItemLookUpEdit_podrVyaz.QueryPopUp += (s, e) =>
                {
                    if (gridViewRasz.FocusedRowHandle < 0)
                        return;

                    var kodProizvObj = gridViewRasz.GetRowCellValue(gridViewRasz.FocusedRowHandle, "kod_proizv");
                    if (kodProizvObj != null && int.TryParse(kodProizvObj.ToString(), out int kodProizv))
                    {
                        var filtered = podrVyazList
                            .Where(x => x.kod_proizv == kodProizv || x.kod_proizv == 9)
                            .ToList();

                        if (s is LookUpEdit editor)
                        {
                            // ВАЖНО: нужно перезаписать .Properties, а не просто DataSource
                            editor.Properties.DataSource = filtered;
                            editor.Properties.ValueMember = "kod_vyaz";
                            editor.Properties.DisplayMember = "text_vyaz";
                            editor.Properties.PopulateColumns();

                            Console.WriteLine($"Фильтрация выполнена: kod_proizv={kodProizv}, filtered={filtered.Count}");
                        }
                    }
                };

                // Устанавливаем текст вязального подразделения при выборе
                repositoryItemLookUpEdit_podrVyaz.EditValueChanged += (s, e) =>
                {
                    if (gridViewRasz.FocusedRowHandle < 0)
                        return;

                    if (s is LookUpEdit editor && editor.EditValue != null && int.TryParse(editor.EditValue.ToString(), out int kodVyaz))
                    {
                        var vyazItem = podrVyazList?.FirstOrDefault(x => x.kod_vyaz == kodVyaz);
                        if (vyazItem != null)
                        {
                            gridViewRasz.SetFocusedRowCellValue("TextVyaz", vyazItem.text_vyaz);
                        }
                    }
                };


                designerComboBox.DataBindings.Clear();
                constructorComboBox.DataBindings.Clear();

                designerComboBox.DataBindings.Add("EditValue", bindingSource1, nameof(ArtNormN.Diz), true, DataSourceUpdateMode.OnPropertyChanged);
                constructorComboBox.DataBindings.Add("EditValue", bindingSource1, nameof(ArtNormN.Constr), true, DataSourceUpdateMode.OnPropertyChanged);

                // Подписки на RowStyle/FocusedRowChanged — один раз
                if (!_rowStyleHandlersAttached)
                {
                    gridViewRasz.RowStyle += GridView_RowStyle;
                    gridViewRaskr.RowStyle += GridView_RowStyle;
                    gridViewKont.RowStyle += GridView_RowStyle;
                    _rowStyleHandlersAttached = true;
                }

                if (!_focusedRowHandlersAttached)
                {
                    gridViewRasz.FocusedRowChanged += GridViewRasz_FocusedRowChanged;
                    _focusedRowHandlersAttached = true;
                }

                if (_currentAnnData != null && _sourceAnnIdToCopyDetailsFrom.HasValue && _duplicateAnnData != null && _mode == (int)Mode.NewWorkDivision)
                {
                    // Копирование только технологических данных из _duplicateAnnData в _currentAnn
                    var operationalData = _duplicateAnnData.CloneOperationalData();
                    _currentAnnData.CopyPropertiesFrom(operationalData);
                    _currentAnnData.dateUpdate = null;
                    _currentAnnData.dateCreate = DateTime.Now;

                    _currentAnnData.ParentId = _sourceAnnIdToCopyDetailsFrom.Value;

                    // Загрузка операций из sourceAnnIdToCopyDetailsFrom
                    int sourceAnnId = _sourceAnnIdToCopyDetailsFrom.Value;

                    // Используем CloneUtils.CloneList для автоматического сброса ID
                    var raszToCopy = await _artNormService.GetRelatedNormRasz(sourceAnnId);
                    var clonedRasz = CloneUtils.CloneList(raszToCopy, _currentAnnData.AnnID, "nrId", false);

                    // Дополнительная проверка - убеждаемся, что все ID сброшены
                    foreach (var item in clonedRasz)
                    {
                        if (item.nrID != 0)
                        {
                            item.nrID = 0; // Принудительно сбрасываем ID
                        }
                    }

                    UIHelper.SafeUpdate(gridViewRasz, () => { });
                    UIHelper.SafeUpdate(gridViewRaskr, () => { });
                    gridViewKont.BeginDataUpdate();
                    try
                    {
                        _normRaszList.Clear();
                        _normRaszList.BulkLoad(clonedRasz);

                        var raskToCopy = await _artNormService.GetRelatedNormRask(sourceAnnId);
                        var clonedRask = CloneUtils.CloneList(raskToCopy, _currentAnnData.AnnID, "id", false);

                        // Дополнительная проверка для NormRask
                        foreach (var item in clonedRask)
                        {
                            if (item.id != 0)
                            {
                                item.id = 0; // Принудительно сбрасываем ID
                            }
                        }

                        _normRaskList.Clear();
                        _normRaskList.BulkLoad(clonedRask);

                        var kontToCopy = await _artNormService.GetRelatedNormKont(sourceAnnId);
                        var clonedKont = CloneUtils.CloneList(kontToCopy, _currentAnnData.AnnID, "nkId", false);

                        // Дополнительная проверка для NormKont
                        foreach (var item in clonedKont)
                        {
                            if (item.nkId != 0)
                            {
                                item.nkId = 0; // Принудительно сбрасываем ID
                            }
                        }

                        _normKontList.Clear();
                        _normKontList.BulkLoad(clonedKont);

                        // Пересчёт нумерации отключён — выполняется по кнопке и при сохранении
                        _normRaszBindingSource.ResetBindings(false);
                        _normRaskBindingSource.ResetBindings(false);
                        _normKontBindingSource.ResetBindings(false);
                        TWGridHelper.sortGridView(gridViewRasz);
                        ApplyPostStructureUi(_normRaszList.FirstOrDefault(), true);
                    }
                    finally
                    {
                        // SafeUpdate сам завершил обновление
                        try { gridViewKont.EndDataUpdate(); } catch { }
                    }
                }
                _hasUnsavedChanges = false;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке формы TeamWork_AdvanceTW");
            }
            finally
            {
                _isInitialLoading = false; // Сбрасываем флаг начальной загрузки

                // Отписываемся от событий при выходе из формы и очищаем ресурсы DnD
                this.FormClosed += (s, args) =>
                {
                    try { TeamWorkBuffer.BufferChanged -= OnBufferChanged; } catch { }
                    try
                    {
                        if (_raszPopupHandler != null)
                        {
                            gridViewRasz.PopupMenuShowing -= _raszPopupHandler;
                            _raszPopupHandler = null;
                        }
                        if (_kontPopupHandler != null)
                        {
                            gridViewKont.PopupMenuShowing -= _kontPopupHandler;
                            _kontPopupHandler = null;
                        }
                        if (_rowStyleHandlersAttached)
                        {
                            gridViewRasz.RowStyle -= GridView_RowStyle;
                            gridViewRaskr.RowStyle -= GridView_RowStyle;
                            gridViewKont.RowStyle -= GridView_RowStyle;
                            _rowStyleHandlersAttached = false;
                        }
                        if (_focusedRowHandlersAttached)
                        {
                            gridViewRasz.FocusedRowChanged -= GridViewRasz_FocusedRowChanged;
                            _focusedRowHandlersAttached = false;
                        }
                        if (_listChangedHandlersAttached)
                        {
                            _normRaszList.ListChanged -= OnNormRaszListChanged;
                            _normRaszList.ListChanged -= OnDataChanged;
                            _normRaskList.ListChanged -= OnDataChanged;
                            _normKontList.ListChanged -= OnDataChanged;
                            _listChangedHandlersAttached = false;
                        }
                    }
                    catch { }
                    try { _raszOpsController?.Detach(); } catch { }
                    try { _presenter?.Detach(); }
                    catch { }
                    finally
                    {
                        // Сброс внутренних флагов и состояния DnD/последнего выбора
                        _raszDragging = false;
                        _raszDragSourceHandle = -1;
                        _lastFocusedRaszOperation = null;
                    }
                };
            }
            #endregion
        }
        #region Загрузка изображений
        private async void LoadGridImage(PictureBox pictureBox, int? annId = null, int? kod = null)
        {
            string imagePath = null;
            try
            {
                imagePath = await _artNormService.GetImage(annId, kod);
                if (!string.IsNullOrEmpty(imagePath))
                {
                    pictureBox.ImageLocation = imagePath;
                }
                else
                {
                    pictureBox.Image = null;
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки изображения по пути '{imagePath ?? "NULL"}' для annId = {annId}");
                pictureBox.Image = null;
            }
        }
        #endregion

        private void UpdateFilteredPodrVyaz(int kodProizv)
        {
            if (podrVyazList == null) return;

            var filtered = podrVyazList
                .Where(x => x.kod_proizv == kodProizv || x.kod_proizv == 9)
                .ToList();


            if (gridViewRasz.FocusedRowHandle >= 0)
            {
                object currentKodPodr = null;

                if (gridViewRasz.ActiveEditor is LookUpEdit editor)
                {
                    currentKodPodr = editor.EditValue;
                }
                else
                {
                    currentKodPodr = gridViewRasz.GetFocusedRowCellValue("kod_podr");
                }

                if (currentKodPodr != null && int.TryParse(currentKodPodr.ToString(), out int kodPodr))
                {
                    if (!filtered.Any(x => x.kod_vyaz == kodPodr))
                    {
                        gridViewRasz.SetRowCellValue(gridViewRasz.FocusedRowHandle, "kod_podr", null);
                    }
                }
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

                var annData = await _dataService.LoadAnnDataAsync(idToLoad);
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
                        _currentAnnData = annData.Clone();                // Обновляем текущую модель
                        _currentAnnData.CopyPropertiesFrom(annData);
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

        #region Пересчёт производных секунд
        private void RecalculateSek()
        {
            try
            {
                // Один проход по данным без лишних Where/ToList
                int sekVyazo = 0, sekVyaz5 = 0, sekVyaz12 = 0, sekVyaz7 = 0, sekVyaz10 = 0, sekVyaz6 = 0, sekVyaz3 = 0;
                int sekVyaz14 = 0, sekVyaz70 = 0, sekVyaz71 = 0, sekVyaz72 = 0, sekVyaz62 = 0, sekVyaz57 = 0, sekVyaz18 = 0;
                int sekVyazAll = 0, sekShv = 0, sekKr = 0, sekTotal = 0, sebTotal = 0;

                if (_normRaszList != null)
                {
                    foreach (var r in _normRaszList)
                    {
                        if (r.N1 >= 100) continue;
                        sekTotal += r.Sek;
                        sebTotal += (int)r.Seb;

                        if (r.KodPodr == 7) sekKr += r.Sek;
                        if (r.KodPodr == 1 || r.KodPodr == 6) sekVyazAll += r.Sek; else sekShv += r.Sek;

                        switch (r.KodOb)
                        {
                            case 28: sekVyazo += r.Sek; break;
                            case 25: sekVyaz5 += r.Sek; break;
                            case 35: sekVyaz12 += r.Sek; break;
                            case 26: sekVyaz7 += r.Sek; break;
                            case 37: sekVyaz10 += r.Sek; break;
                            case 38: sekVyaz6 += r.Sek; break;
                            case 29: sekVyaz3 += r.Sek; break;
                            case 62: sekVyaz14 += r.Sek; break;
                            case 55: sekVyaz70 += r.Sek; break;
                            case 59: sekVyaz71 += r.Sek; break;
                            case 73: sekVyaz72 += r.Sek; break;
                            case 60: sekVyaz62 += r.Sek; break;
                            case 114: sekVyaz57 += r.Sek; break;
                            case 115: sekVyaz18 += r.Sek; break;
                        }
                    }
                }

                _currentAnnData.SekVyazo = sekVyazo;
                _currentAnnData.SekVyaz5 = sekVyaz5;
                _currentAnnData.SekVyaz12 = sekVyaz12;
                _currentAnnData.SekVyaz7 = sekVyaz7;
                _currentAnnData.SekVyaz10 = sekVyaz10;
                _currentAnnData.SekVyaz6 = sekVyaz6;
                _currentAnnData.SekVyaz3 = sekVyaz3;
                _currentAnnData.SekKr = sekKr;
                _currentAnnData.Sek = sekTotal;
                _currentAnnData.Seb = sebTotal;
                _currentAnnData.SekVyaz14 = sekVyaz14;
                _currentAnnData.SekVyaz70 = sekVyaz70;
                _currentAnnData.SekVyaz71 = sekVyaz71;
                _currentAnnData.SekVyaz72 = sekVyaz72;
                _currentAnnData.SekVyaz62 = sekVyaz62;
                _currentAnnData.SekVyaz57 = sekVyaz57;
                _currentAnnData.SekVyaz18 = sekVyaz18;
                _currentAnnData.SekVyaz = sekVyazAll;

                // Если секции вязания = 0, то швейку приравниваем к общим сек
                _currentAnnData.SekShv = _currentAnnData.SekVyaz == 0 ? _currentAnnData.Sek : sekShv;

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
        #endregion

        #region Закрытие формы и очистка
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

                        try { _gridHelper.SaveGridViewSettings(gridViewRaskr, "AdvanceTW_gridViewRaskrLayout.xml"); } catch { }
                        try { _gridHelper.SaveGridViewSettings(gridViewKont, "AdvanceTW_gridViewKontLayout.xml"); } catch { }
                        try { _gridHelper.SaveGridViewSettings(gridViewRasz, "AdvanceTW_gridViewRaszLayout.xml"); } catch { }
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
                    if (IsDraftCreationMode())
                    {
                        PerformCleanupOnCancel();
                    }
                }
            }
        }

        private bool IsDraftCreationMode()
        {
            // Единая точка определения «черновых» режимов
            return _mode == (int)Mode.NewWorkDivision;
        }

        private async void PerformCleanupOnCancel()
        {
            try
            {
                if (IsDraftCreationMode() && _newAnnId > 0)
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
        #endregion

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

            // ВСЕГДА: клик по NewItemRow добавляет новую главу
                AddNewRaszOperation(null, true);
        }

        private void gridViewRasz_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (e.Row is NormRasz row)
            {
                // Расширенная валидация нумерации операций
                var errors = TeamWorkValidationServiceEx.GetOperationNumberErrors(_normRaszList);
                if (errors != null && errors.Count > 0)
                {
                    // Если есть глобальные дубликаты, показываем ошибку; для понятности выводим только относящиеся к текущей строке, если есть
                    var rowErr = errors.FirstOrDefault(msg => msg.Contains($" {row.N}.{row.N1} ") || msg.EndsWith($" {row.N}.{row.N1}") || msg.Contains($"{row.N}.{row.N1}"));
                    e.Valid = false;
                    e.ErrorText = rowErr ?? string.Join("; ", errors);
                    _uiService?.ShowStatus(e.ErrorText);

                }
            }
        }

        private void gridViewRasz_RowEditCanceled(object sender, RowObjectEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;

            var canceledNormRasz = e.Row as NormRasz;
            if (canceledNormRasz == null) return;

            if (canceledNormRasz.IsNew && canceledNormRasz.IsBeingAdded) // удаляем только если строка находится в процессе добавления сейчас
            {
                if (_normRaszList.Contains(canceledNormRasz))
                {
                    int removedIndex = _normRaszList.IndexOf(canceledNormRasz);
                    _normRaszList.Remove(canceledNormRasz);
                    _logger.LogEventAsync($"New NormRasz row removed due to edit cancellation.", "gridViewRasz_RowEditCanceled");
                    if (removedIndex >= 0)
                    {
                        _normRaszBindingSource.ResetItem(Math.Max(0, Math.Min(removedIndex, _normRaszList.Count - 1)));
                    }
                    var rh = view.GetRowHandle(Math.Max(0, Math.Min(removedIndex, _normRaszList.Count - 1)));
                    if (view.IsValidRowHandle(rh)) view.RefreshRow(rh);
                }
            }
            else if (_originalNormRaszDataBeforeEdit != null) // Если редактирование существующей строки было отменено
            {
                int index = _normRaszList.IndexOf(canceledNormRasz);
                if (index != -1)
                {
                    // Восстанавливаем оригинальные данные.
                    _normRaszList[index].CopyPropertiesFrom(_originalNormRaszDataBeforeEdit);
                    _logger.LogEventAsync($"NormRasz row (nrId: {_originalNormRaszDataBeforeEdit.nrID}) edit canceled, reverted to original state.", "gridViewRasz_RowEditCanceled");
                    if (index >= 0)
                    {
                        _normRaszBindingSource.ResetItem(index);
                        var rh2 = view.GetRowHandle(index);
                        if (view.IsValidRowHandle(rh2)) view.RefreshRow(rh2);
                    }
                }
            }

            _originalNormRaszDataBeforeEdit = null; // Очищаем сохраненное состояние
                                                    // view.HideEditForm(); // Обычно не требуется, грид сам закроет форму при отмене
            _hasUnsavedChanges = true; // Список данных изменился или редактирование отменено
            _uiService?.HighlightChangedControls();
        }
        private void gridViewRasz_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (e.Row is NormRasz normRasz && !_isInitialLoading)
            {
                // Если строка не новая, помечаем ее как измененную
                if (!normRasz.IsNew)
                {
                    normRasz.IsModified = true;
                }
                // В любом случае после успешного обновления строка больше не считается «в процессе добавления»
                normRasz.IsBeingAdded = false;
                // Точечное обновление без мерцания
                int index = _normRaszList.IndexOf(normRasz);
                if (index >= 0)
                {
                    _normRaszBindingSource?.ResetItem(index);
                    var rh = ((GridView)sender).GetRowHandle(index);
                    if (((GridView)sender).IsValidRowHandle(rh)) ((GridView)sender).RefreshRow(rh);
                }
                // Если нужна немедленная валидация/пересчёт — включить UpdateCurrentRow только при необходимости
                // ((GridView)sender).UpdateCurrentRow();
            }
        }


        // Drag & Drop для gridViewRasz
        private Point _raszDragStartPoint;
        private int _raszDragSourceHandle = -1;
        private bool _raszDragging = false;


        private void ConfigureRaszGrouping()
        {
            try
            {
                if (gridViewRasz == null) return;
                var colN = gridViewRasz.Columns.ColumnByFieldName("N");
                var colN1 = gridViewRasz.Columns.ColumnByFieldName("N1");

                gridViewRasz.BeginUpdate();
                try
                {
                    if (colN != null)
                    {
                        colN.GroupIndex = 0;
                        colN.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    }
                    if (colN1 != null)
                    {
                        colN1.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                        colN1.SortIndex = 1;
                    }
                    gridViewRasz.OptionsBehavior.AutoExpandAllGroups = true;
                }
                finally
                {
                    gridViewRasz.EndUpdate();
                }
                // Гарантируем разворот всех групп после настройки
                ExpandAllRaszGroups();
            }
            catch { }
        }

        private void ExpandAllRaszGroups()
        {
            try
            {
                if (gridViewRasz == null) return;
                gridViewRasz.BeginUpdate();
                try { gridViewRasz.ExpandAllGroups(); }
                finally { gridViewRasz.EndUpdate(); }
            }
            catch { }
        }

        // Автопрокрутка — используем встроенную в DevExpress (удалён самописный таймер)

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
                    // Больше не удаляем существующие строки при скрытии формы — только логируем
                    if (row != null && row.nrID > 0 && !row.IsNew)
                    {
                        await _logger.LogEventAsync($"EditFormHidden for existing row (nrId: {row.nrID}) with Result: {e.Result}. Original delete logic is currently commented.", "gridViewRasz_EditFormHidden");
                    }
                    // Запасной вариант для новой строки, которая могла не быть обработана RowEditCanceled — удаляем только если она реально в процессе добавления
                    else if (row != null && row.IsNew && row.IsBeingAdded)
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
            else if (e.Result == EditFormResult.Update)
            {
                try
                {
                    var row = gridView.GetRow(e.RowHandle) as NormRasz;
                    if (row != null)
                    {
                        row.IsBeingAdded = false; // подтверждено сохранением
                    }
                }
                catch { }
            }
            _originalNormRaszDataBeforeEdit = null; // Убедимся, что очищено после любого закрытия формы редактирования
        }
        #endregion

        #region Rask
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

                        // Фиксируем выбранную сложность на текущем ANN
                        if (selectionForm.SelectedComplexity.HasValue && _currentAnnData != null)
                        {
                            _currentAnnData.Slogn = selectionForm.SelectedComplexity.Value;
                            bindingSource1.ResetBindings(false);
                            _hasUnsavedChanges = true;
                        }

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
                        ApplyPostStructureUi(null, true);
                    }
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка в методе GridViewRaskr_InitNewRow");
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

        private void GridViewRaskr_RowUpdated(object sender, RowObjectEventArgs e)
        {
            if (e.Row is NormRask normRask && !_isInitialLoading)
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

        private async void GridViewRaskr_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
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
            if (e.Row is NormKont normKont && !_isInitialLoading)
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

            // Запрещаем добавление новых строк, если уже есть 2 записи
            if (view.IsNewItemRow(view.FocusedRowHandle))
            {
                if (_normKontList.Count >= 2)
                {
                    e.Cancel = true;
                    MessageBox.Show("На одно РТ можно добавить максимум 2 строки комплектовки", "Ограничение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                e.Cancel = true;

                if (_isSelectionFormOpen) return;

                try
                {
                    _isSelectionFormOpen = true;

                    string choice1 = "Пронумеровать деталь";
                    string choice2 = "Комплектация пачки";

                    // Проверяем, какие строки уже есть
                    bool hasChoice1 = _normKontList.Any(nk => nk.text == choice1);
                    bool hasChoice2 = _normKontList.Any(nk => nk.text == choice2);

                    List<string> options = new List<string>();
                    if (!hasChoice1) options.Add(choice1);
                    if (!hasChoice2) options.Add(choice2);

                    string selectedText = null;
                    string kod_o = null;

                    if (options.Count == 2)
                    {
                        DialogResult choiceResult = MessageBox.Show($"Добавить '{options[0]}'?", "Выбор операции", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (choiceResult == DialogResult.Yes)
                        {
                            selectedText = options[0];
                            kod_o = "001";
                        }
                        else if (choiceResult == DialogResult.No)
                        {
                            DialogResult choiceResult2 = MessageBox.Show($"Добавить '{options[1]}'?", "Выбор операции", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (choiceResult2 == DialogResult.Yes)
                            {
                                selectedText = options[1];
                                kod_o = "100";
                            }
                        }
                    }
                    else if (options.Count == 1)
                    {
                        selectedText = options[0];
                        kod_o = "100";
                      //  MessageBox.Show($"Добавлена строка: {selectedText}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            kod_o = kod_o,
                            text = selectedText,
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
                e.Cancel = true; // Запрещаем редактирование поля Text для существующих строк
            }
            else
            {
                e.Cancel = false; // Разрешаем редактирование других полей
            }
        }

        #endregion


        #region Сохранение данных (ANN и связанные списки)
        // Общий метод для обработки сохранения
        private async Task<bool> ProcessSaveData(bool closeAfterSave)
        {
            _okPressed = false; // Сбрасываем флаг перед попыткой сохранения
            _uiService?.ShowStatus("Сохранение данных...");

            if (!ValidateForm())
            {
                _uiService?.ShowStatus("Ошибки заполнения формы");
                return false;
            }

            try
            {
                // Перед сохранением всегда выполняем пересчет нумерации операций (UI-поток)
                await this.InvokeAsync(() =>
                {
                    RecalculateAllOperationNumbers();
                });

                // Гарантируем заполнение parentId для сценария дублирования
                if (_mode == (int)Mode.Clone && _selectedAnnId > 0)
                {
                    _currentAnnData.ParentId = _selectedAnnId;
                }


                ////// 3) Если дата утверждения только что появилась — считаем diff
                ////bool isApprovedNow =
                ////    prev != null &&
                ////    !prev.dateUpdate.HasValue &&
                ////    _currentAnnData.dateUpdate.HasValue;

                ////if (isApprovedNow)
                ////{
                ////    var snapSvc = new RtSnapshotService(_dbService, _dbHelper, _logger);

                ////    // дополнительная защита: работаем только если есть "висящий" снимок
                ////    if (await snapSvc.HasPendingAsync(_newAnnId))
                ////    {
                ////        string diffText = await snapSvc.CompareWithCurrentAsync(
                ////            _newAnnId, _currentAnnData.dateUpdate.Value);

                ////        if (!string.IsNullOrWhiteSpace(diffText))
                ////        {
                ////            MessageBox.Show(
                ////                diffText,
                ////                "Изменения к моменту утверждения",
                ////                MessageBoxButtons.OK, MessageBoxIcon.Information);
                ////        }
                ////    }
                ////}
                //// 1) Зафиксируем предыдущее состояние даты из БД ещё до транзакции
                //var prevAnn = await _dataService.LoadAnnDataAsync(_newAnnId);
                //var prevApproved = prevAnn?.dateUpdate;
                var snapSvc = new RtSnapshotService(_dbService, _dbHelper, _logger);

                // 0) Берём состояние из БД ДО транзакции
                var prevAnn = (_newAnnId > 0) ? await _dataService.LoadAnnDataAsync(_newAnnId) : null;
                var prevApproved = prevAnn?.dateUpdate;

                bool clearingNow = prevApproved.HasValue && !_currentAnnData.dateUpdate.HasValue;
                bool approvingNow = !prevApproved.HasValue && _currentAnnData.dateUpdate.HasValue;

                // 1) Если сейчас снимаем дату (первое сохранение после входа в редактирование),
                //    то перед записью удостоверимся, что есть снимок "до изменений"
                if (_newAnnId > 0 && clearingNow && !await snapSvc.HasPendingAsync(_newAnnId))
                {
                    await snapSvc.EnsurePendingSnapshotAsync(_newAnnId);//, userName);
                }


                await _dbHelper.ExecuteInTransactionAsync(async () =>
                {
                    await SaveAnnDataAsync();
                    await SaveAllDataAsync();
                });
                //// 2) Уже после коммита — проверяем переход NULL→НЕ-NULL и считаем diff
                //var snapSvc = new RtSnapshotService(_dbService, _dbHelper, _logger);
                //if (!prevApproved.HasValue && _currentAnnData.dateUpdate.HasValue && await snapSvc.HasPendingAsync(_newAnnId))
                //{
                //    // На всякий случай перечитаем актуальную дату из БД (или используйте _currentAnnData)
                //    var curAnn = await _dataService.LoadAnnDataAsync(_newAnnId);
                //    var approvedAt = curAnn?.dateUpdate ?? _currentAnnData.dateUpdate;

                //    string diffText = await snapSvc.CompareWithCurrentAsync(_newAnnId, approvedAt.Value);
                //    if (!string.IsNullOrWhiteSpace(diffText))
                //    {
                //        MessageBox.Show(diffText, "Изменения к моменту утверждения",
                //            MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    }
                //}
                // 3) После коммита — если ставим дату сейчас, считаем diff и "съедаем" снимок
                if (_newAnnId > 0 && approvingNow && await snapSvc.HasPendingAsync(_newAnnId))
                {
                    var curAnn = await _dataService.LoadAnnDataAsync(_newAnnId);
                    var approvedAt = curAnn?.dateUpdate ?? _currentAnnData.dateUpdate;

                    string diffText = await snapSvc.CompareWithCurrentAsync(_newAnnId, approvedAt.Value);
                    if (!string.IsNullOrWhiteSpace(diffText))
                    {
                        MessageBox.Show(diffText, "Изменения к моменту утверждения",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // Эти действия выполняются ПОСЛЕ успешной транзакции
                IsRaszInserted = true; // Предполагаем, что если сохранение дошло сюда, то все списки были обработаны
                IsRaskInserted = true;
                IsKontInserted = true;
                // Сбрасываем флаги и обновляем снапшоты для корректной подсветки
                ResetFlagsAndSnapshots();
                _hasUnsavedChanges = false;

                if (closeAfterSave)
                {
                    _uiService?.ShowStatus("Данные успешно сохранены! Закрытие формы...", 1500);
                    this.DialogResult = DialogResult.OK;
                    _okPressed = true;
                    this.Close();
                }
                else
                {
                    _uiService?.ShowStatus("Данные успешно сохранены!");
                }

                // После первого успешного сохранения в режимах Clone/ArchAndCopy/NewWorkDivision
                // переходим в обычный режим редактирования, чтобы дальнейшие сохранения
                // выполняли insert/update по флагам IsNew/IsModified, а не массовую вставку
                if (_mode == (int)Mode.Clone || _mode == (int)Mode.ArchAndCopy || _mode == (int)Mode.NewWorkDivision)
                {
                    _selectedAnnId = _newAnnId;
                    _mode = (int)Mode.Edit;
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
                _uiService?.ShowStatus($"Ошибка при сохранении: {ex.Message}");
                // this.DialogResult = DialogResult.None; // Не закрываем при ошибке
                return false;
            }
        }

        private void ResetFlagsAndSnapshots()
        {
            try
            {
                UIHelper.SafeUpdate(gridViewRasz, () => { });
                UIHelper.SafeUpdate(gridViewRaskr, () => { });
                UIHelper.SafeUpdate(gridViewKont, () => { });
                try
                {
                    if (_normRaszList != null)
                    {
                        foreach (var it in _normRaszList)
                        {
                            it.IsNew = false;
                            it.IsModified = false;
                        }
                        _originalNormRaszList = CloneUtils.DeepCloneBindingList(_normRaszList);
                        _normRaszBindingSource?.ResetBindings(false);
                    }

                    if (_normRaskList != null)
                    {
                        foreach (var it in _normRaskList)
                        {
                            it.IsNew = false;
                            it.IsModified = false;
                        }
                        _originalNormRaskList = CloneUtils.DeepCloneBindingList(_normRaskList);
                        _normRaskBindingSource?.ResetBindings(false);
                    }

                    if (_normKontList != null)
                    {
                        foreach (var it in _normKontList)
                        {
                            it.IsNew = false;
                            it.IsModified = false;
                        }
                        _originalNormKontList = CloneUtils.DeepCloneBindingList(_normKontList);
                        _normKontBindingSource?.ResetBindings(false);
                    }
                }
                finally { }
            }
            catch { }
        }

        // Сохранение данных без закрытия формы
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_presenter != null) { await _presenter.SaveAsync(false); return; }
        }

        // Сохранение данных и закрытие формы
        private async void btnOK_Click(object sender, EventArgs e)
        {
            if (_presenter != null) { await _presenter.SaveAsync(true); return; }
        }

        private async Task SaveAnnDataAsync()
        {
            try
            {
                // Сохраняем данные в таблицу ann
                _currentAnnData.AnnID = _newAnnId;
               // _currentAnnData.dateUpdate = null;
                var articul = _currentAnnData?.Articul;
                var annId = _newAnnId > 0 ? _newAnnId : (int?)null;

                if (!await IsArticulUniqueAsync(articul, annId))
                {
                    MessageBox.Show("Разделение с таким артикулом уже существует. Проверьте состав разделения.", "Дублирование", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                // 1) Берём предыдущее состояние до сохранения
                var prev = (_newAnnId > 0)
                    ? await _dataService.LoadAnnDataAsync(_newAnnId)
                    : null;
                // дальше обычное сохранение
                if (_newAnnId > 0)
                {
                    RecalculateSek();
                }
                await _dataService.SaveAnnDataAsync(_currentAnnData);
                CreatedAnn = _currentAnnData;

                // MessageBox.Show("Данные успешно сохранены", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при сохранении данных в БД");
                await ShowStatusMessage($"Ошибка при сохранении данных: {ex.Message}",3500 ,Color.Crimson);
                //MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
        }
        private async Task SaveAllDataAsync()
        {
            try
            {
                // Валидация номеров операций перед сохранением
                var validation = TeamWorkValidationServiceEx.ValidateOperationNumbers(_normRaszList?.ToList() ?? new List<NormRasz>());
                if (validation != null && validation != ValidationResult.Success)
                {
                    MessageBox.Show(validation.ErrorMessage, "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                await SaveListAsync(_normRaszList, TableNames.Rasz, TableNames.RaszId, _newAnnId, _deletedNormRaszIds);
                await SaveListAsync(_normRaskList, TableNames.Rask, TableNames.RaskId, _newAnnId, _deletedNormRaskIds);
                await SaveListAsync(_normKontList, TableNames.Kont, TableNames.KontId, _newAnnId, _deletedNormKontIds);

                // Обновляем UI после сохранения
                _uiService?.RefreshAllGrids();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при сохранении данных");
                throw;
            }
        }

        private async Task SaveListAsync<T>(BindingList<T> list, string tableName, string keyFieldName, int newAnnId, List<int> deletedIds)
    where T : class, INewable, IModifiable, new()
        {
            var stopwatch = Stopwatch.StartNew();
            var bulkStopwatch = new Stopwatch();

            // Определяем, нужно ли вставлять все записи как новые
            // Это происходит когда создается новый ANN ID (режимы Clone, ArchAndCopy, NewWorkDivision)
            // или когда загружаем существующие данные для нового ANN ID
            bool shouldInsertAllAsNew = newAnnId != _selectedAnnId || _mode == (int)Mode.Clone || _mode == (int)Mode.ArchAndCopy || _mode == (int)Mode.NewWorkDivision;

            List<T> itemsToInsert;
            List<T> itemsToUpdate;

            if (shouldInsertAllAsNew)
            {
                // Все записи должны быть вставлены как новые
                itemsToInsert = list.ToList();
                itemsToUpdate = new List<T>();
            }
            else
            {
                // Обычная логика: новые записи - вставка, существующие - обновление
                itemsToInsert = list.Where(x => x.IsNew).ToList();
                itemsToUpdate = list.Where(x => x.IsModified && !x.IsNew).ToList();
            }

            string itemTypeName = typeof(T).Name;
            await _logger.LogEventAsync($"[{itemTypeName}] Start saving. Insert: {itemsToInsert.Count}, Update: {itemsToUpdate.Count}, Mode: {_mode}, NewAnnId: {newAnnId}, SelectedAnnId: {_selectedAnnId}", "SaveListAsync");

            // 1. Удаление
            if (deletedIds?.Any() == true)
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string deleteSql = $"Update {tableName} SET nrDateDel = GETDATE(), nrCompDel = HOST_NAME() where {keyFieldName} in @ids";
                    await connection.ExecuteAsync(deleteSql, new { ids = deletedIds });
                    await _logger.LogEventAsync($"[{itemTypeName}] Удалено записей: {deletedIds.Count}", "SaveListAsync");
                }
            }

            // 2. Обработка вставки
            if (itemsToInsert.Any())
            {
                // Установим annId через кэш отражений (ускорение массовых вставок)
                    var annIdProp = typeof(T).GetProperty("annId");
                    if (annIdProp != null)
                {
                    foreach (var item in itemsToInsert)
                    {
                        annIdProp.SetValue(item, newAnnId);
                    }
                }

                using (var connection = _dbHelper.GetConnection())
                {
                    try
                    {
                        await _logger.LogEventAsync($"[{itemTypeName}] BulkInsert: {itemsToInsert.Count}", "SaveListAsync");
                        bulkStopwatch.Restart();
                        _bulkHelper.BulkInsert(connection, itemsToInsert, tableName, new[] { keyFieldName });
                        bulkStopwatch.Stop();
                    }
                    catch (Exception ex)
                    {
                        await _logger.LogErrorAsync(ex, $"Ошибка при BulkInsert [{itemTypeName}].");
                        throw; // Пробрасываем исключение дальше
                    }
                }

                foreach (var item in itemsToInsert)
                {
                    item.IsNew = false;
                    if (item is IModifiable modifiableNew)
                    {
                        modifiableNew.IsModified = false;
                    }
                }
            }

            // 3. Обновление (только если не shouldInsertAllAsNew)
            if (itemsToUpdate.Any())
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    try
                    {
                        await _logger.LogEventAsync($"[{itemTypeName}] BulkUpdate: {itemsToUpdate.Count}", "SaveListAsync");
                        bulkStopwatch.Restart();
                        _bulkHelper.BulkUpdate(connection, itemsToUpdate, tableName, new[] { keyFieldName });
                        bulkStopwatch.Stop();
                    }
                    catch (Exception ex)
                    {
                        await _logger.LogErrorAsync(ex, $"Ошибка при BulkUpdate [{itemTypeName}].");
                        throw;
                    }

                    // Сброс флага IsModified после успешного обновления
                    foreach (var item in itemsToUpdate)
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

        private async Task<bool> IsArticulUniqueAsync(string articul, int? excludeAnnId = null)
        {
            if (string.IsNullOrWhiteSpace(articul)) return false;
            var sql = @"
        IF EXISTS (
            SELECT 1
            FROM artNormNView WITH (NOLOCK)
            WHERE UPPER(LTRIM(RTRIM(Articul))) = UPPER(LTRIM(RTRIM(@Articul)))
             AND (@ExcludeId IS NULL OR AnnId <> @ExcludeId)
        )
            SELECT CAST(0 AS bit);
        ELSE
            SELECT CAST(1 AS bit);";

            using (var conn = _dbHelper.GetConnection())
            {
                return await conn.ExecuteScalarAsync<bool>(sql, new { Articul = articul, ExcludeId = excludeAnnId });
            }
        }

        #endregion


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

        #region Буфер: вставка через кнопку
        /// <summary>
        /// Обработчик нажатия на кнопку "Вставить из буфера"
        /// </summary>
        private async void buffer_Click(object sender, EventArgs e)
        {
            // Делегируем презентеру (тонкий View)
            if (_presenter != null) { await _presenter.ImportFromBufferAsync(); return; }
            // fallback: Используем глобальный буфер если доступен, иначе локальный 
            IReadOnlyList<int> bufferIdsToUse = TeamWorkBuffer.HasData ? TeamWorkBuffer.BufferIds : (_bufferWorkDivision > 0 ? new List<int> { _bufferWorkDivision } : new List<int>());

            if (bufferIdsToUse != null && bufferIdsToUse.Count > 0)
            {
                try
                {
                    var result = MessageBox.Show(
     "Очистить текущие данные перед вставкой из буфера?",
     "Вставка из буфера",
     MessageBoxButtons.YesNo,
     MessageBoxIcon.Question);

                    bool clearExisting = result == DialogResult.Yes;
                        if (_normRaszList == null || _normRaskList == null || _normKontList == null)
                        {
                            await InitializeBindingsAsync();
                        }

                    var report = await _bufferImportService.ImportAsync(
                        bufferIdsToUse,
                        _currentAnnData?.AnnID ?? _newAnnId,
                        clearExistingBefore: clearExisting,
                        markExistingAsDeleted: clearExisting
                    );

                    await ShowStatusMessage($"Загружено частей: {report.PartsCount}, операций: {report.InsertedCount}");
                }
                catch (SqlException sqlEx)
                {
                    await _logger.LogErrorAsync(sqlEx, "Ошибка при вставке данных из буфера");
                    await ShowStatusMessage($"Ошибка при вставке данных из буфера: {sqlEx.Message}", 5000, Color.Red);
                }
                catch (Exception ex)
                {
                    await _logger.LogErrorAsync(ex, "Ошибка при вставке данных из буфера");
                    await ShowStatusMessage($"Ошибка при вставке данных из буфера: {ex.Message}", 5000, Color.Red);
                }
            }
            else
            {
                await ShowStatusMessage("В буфере пусто", 3000, Color.Orange);
            }
        }
        
        // Тонкая обёртка для презентера: логика вставки из буфера без привязки к обработчику клика
        private async Task ImportFromBufferPresenterAsync()
        {
            IReadOnlyList<int> bufferIdsToUse = TeamWorkBuffer.HasData ? TeamWorkBuffer.BufferIds : (_bufferWorkDivision > 0 ? new List<int> { _bufferWorkDivision } : new List<int>());

            if (bufferIdsToUse != null && bufferIdsToUse.Count > 0)
            {
                try
                {
                    var result = MessageBox.Show(
                        "Очистить текущие данные перед вставкой из буфера?",
                        "Вставка из буфера",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    bool clearExisting = result == DialogResult.Yes;
                    if (_normRaszList == null || _normRaskList == null || _normKontList == null)
                    {
                        await InitializeBindingsAsync();
                    }

                    var report = await _bufferImportService.ImportAsync(
                        bufferIdsToUse,
                        _currentAnnData?.AnnID ?? _newAnnId,
                        clearExistingBefore: clearExisting,
                        markExistingAsDeleted: clearExisting
                    );

                    await ShowStatusMessage($"Загружено частей: {report.PartsCount}, операций: {report.InsertedCount}");
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    await _logger.LogErrorAsync(sqlEx, "Ошибка при вставке данных из буфера");
                    await ShowStatusMessage($"Ошибка при вставке данных из буфера: {sqlEx.Message}", 5000, System.Drawing.Color.Red);
                }
                catch (Exception ex)
                {
                    await _logger.LogErrorAsync(ex, "Ошибка при вставке данных из буфера");
                    await ShowStatusMessage($"Ошибка при вставке данных из буфера: {ex.Message}", 5000, System.Drawing.Color.Red);
                }
            }
            else
            {
                await ShowStatusMessage("В буфере пусто", 3000, System.Drawing.Color.Orange);
            }
        }

        // Тонкая обёртка для презентера: добавление операции с учётом последнего фокуса
        private async Task AddOperationPresenterAsync()
        {
            await this.InvokeAsync(() =>
            {
                bool shouldOfferPosition = _lastFocusedRaszOperation != null && _normRaszList?.Count > 0;
                if (shouldOfferPosition)
                {
                    AddNewRaszOperation(_lastFocusedRaszOperation, false);
                }
                else
                {
                    AddNewRaszOperation(null, true);
                }
            });
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_originalAnnData != null)
            {
                _currentAnnData.CopyPropertiesFrom(_originalAnnData);
                // Точечные обновления завязаны на биндинги контролов, здесь достаточно обновить конкретные элементы при необходимости,
                // но безопасно обновим весь bindingSource формы (одноразовая операция по кнопке Cancel)
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

           // MessageBox.Show("Изменения успешно отменены и восстановлены до исходного состояния.", "Отмена изменений", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #region GridView: подготовка форм редактирования и валидация
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

        private void gridViewRasz_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }


        #endregion

        private void gridViewKont_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            _gridHelper.popUpMenuCopy(sender, e);
        }
        private void gridViewRaskr_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            _gridHelper.popUpMenuCopy(sender, e);
        }
        private void gridViewRasz_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            _gridHelper.popUpMenuCopy(sender, e);
        }
        // Поле для хранения делегата, чтобы корректно отписываться
        private EventHandler _kontEditorValueChanged;

        // 1) Редактор показан — подписываемся на изменения значения
        private void gridViewKont_ShownEditor(object sender, EventArgs e)
        {
            var view = (GridView)sender;
            var editor = view.ActiveEditor;
            if (editor == null) return;

            _kontEditorValueChanged = (s, e2) =>
            {
                // Коммитим значение из редактора в GridView немедленно
                view.PostEditor();

                // Если нужно сразу запушить в BindingList и вызвать ValidateRow:
                // view.UpdateCurrentRow();
            };
            editor.EditValueChanged += _kontEditorValueChanged;
        }
        // 2) Редактор скрыт — обязательно отписываемся
        private void GridViewKont_HiddenEditor(object sender, EventArgs e)
        {
            var view = (GridView)sender;
            var editor = view.ActiveEditor;
            if (editor != null && _kontEditorValueChanged != null)
            {
                editor.EditValueChanged -= _kontEditorValueChanged;
                _kontEditorValueChanged = null;
            }
        }

        // 3) Значение в ячейке изменено (уже после PostEditor)
        private void GridViewKont_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (_isInitialLoading) return; // не реагировать при начальной загрузке
            var view = (GridView)sender;

            // Берём модель строки
            var kont = view.GetRow(e.RowHandle) as NormKont;
            if (kont == null) return;

            // Привязываем AnnId на всякий случай к текущему РТ
            kont.AnnId = _newAnnId;

            // Флаг изменения: если строка не новая — помечаем изменённой
            if (!kont.IsNew)
                kont.IsModified = true;   // или Updated = true, если используется отдельный флаг

            // При необходимости — явное присваивание в свойство, если биндинг особый:
            // if (e.Column.FieldName == nameof(NormKont.SomeField)) kont.SomeField = (тип)e.Value;

            // Точечное обновление источника (без полного ResetBindings)
            if (_normKontBindingSource != null)
            {
                int position = _normKontBindingSource.IndexOf(kont);
                if (position >= 0)
                    _normKontBindingSource.ResetItem(position);
            }
            else
            {
                view.RefreshRow(e.RowHandle);
            }

            // Если есть завязки на пересчёты/подсветку — можно обновить строку стиля
            // view.UpdateCurrentRow(); // только если нужен немедленный ValidateRow/DataSource push
        }


        /// <summary>
        /// Автоматически добавляет две стандартные строки в norm_kont для новых РТ
        /// </summary>
        #region Kont: утилиты
        private void AddStandardKontRows()
        {
            try
            {
                if (_normKontList == null) return;

                string choice1 = "Пронумеровать деталь";
                string choice2 = "Комплектация пачки";

                // Проверяем, что строки еще не добавлены (нормализуем текст)
                bool hasChoice1 = _normKontList.Any(nk => string.Equals((nk.text ?? string.Empty).Trim(), choice1, StringComparison.OrdinalIgnoreCase));
                bool hasChoice2 = _normKontList.Any(nk => string.Equals((nk.text ?? string.Empty).Trim(), choice2, StringComparison.OrdinalIgnoreCase));

                if (!hasChoice1)
                {
                    var newKont1 = new NormKont
                    {
                        AnnId = _newAnnId,
                        text = choice1,
                        kod_o = "001",
                        IsNew = true
                    };
                    _normKontList.Add(newKont1);
                }

                if (!hasChoice2)
                {
                    var newKont2 = new NormKont
                    {
                        AnnId = _newAnnId,
                        text = choice2,
                        kod_o = "100",
                        IsNew = true
                    };
                    _normKontList.Add(newKont2);
                }

                // Обновляем грид
                _normKontBindingSource?.ResetBindings(false);

                _logger.LogEventAsync($"Добавлены стандартные строки norm_kont для AnnId: {_newAnnId}", "AddStandardKontRows").ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при добавлении стандартных строк norm_kont").ConfigureAwait(false);
            }
        }
        #endregion


        /// <summary>
        /// Вставляет операции из глобального буфера в текущий список NormRasz.
        /// Используется для режима комплекта, чтобы автоматически
        /// объединить операции из нескольких разделений труда, выбранных
        /// пользователем. В отличие от <see cref="buffer_Click"/>, этот метод
        /// не запрашивает подтверждение пользователя и не очищает списки
        /// операций — предполагается, что список уже подготовлен и пуст.
        /// </summary>
        #region Буфер: вставка операций для комплекта
        private async Task InsertOperationsFromBufferAsync()
        {
            // Используем глобальный буфер
            IReadOnlyList<int> bufferIdsToUse = TeamWorkBuffer.HasData ? TeamWorkBuffer.BufferIds : null;
            if (bufferIdsToUse == null || bufferIdsToUse.Count == 0)
            {
                return;
            }

            // Первая группа вставляется как есть, следующие группы — со сдвигом N на текущий максимум; N1 сохраняется
            int currentMaxN = (_normRaszList != null && _normRaszList.Count > 0)
                ? _normRaszList.Select(x => x.N).DefaultIfEmpty(0).Max()
                : 0;
            gridViewRasz.BeginDataUpdate();
            try
            {
            foreach (var id in bufferIdsToUse)
            {
                var raszList = await _artNormService.GetRelatedNormRasz(id);
                if (raszList == null) continue;
                int partMaxN = raszList.Select(x => x.N).DefaultIfEmpty(0).Max();
                int offset = currentMaxN; // 0 для первой группы, далее — накопленный максимум
                foreach (var item in raszList)
                {
                    item.nrID = 0;
                    item.annId = _currentAnnData?.AnnID ?? 0;
                    item.nrDateAdd = null;
                    item.nrCompAdd = null;
                    item.IsNew = true;
                    item.N = item.N + offset;
                    _normRaszList.Add(item);
                }
                    currentMaxN += partMaxN;
                }
                FinalizeRaszBatch(_normRaszList.FirstOrDefault(), true);
            }
            finally
            {
                try { gridViewRasz.EndDataUpdate(); } catch { }
            }
            await ShowStatusMessage("Операции из буфера добавлены для комплекта");
        }
        #endregion

        #region Управление порядком и нумерацией операций

        /// <summary>
        /// Полный пересчет нумерации всех операций
        /// </summary>
        public void RecalculateAllOperationNumbers()
        {
            try
            {
                if (_normRaszList == null || _normRaszList.Count == 0) return;
                OperationNumberingService.RecalculateAllOperationNumbers(_normRaszList);
                _normRaszBindingSource.ResetBindings(false);
                TWGridHelper.sortGridView(gridViewRasz);
                _logger.LogEventAsync($"Выполнен полный пересчет нумерации для {_normRaszList.Count} операций", "RecalculateAllOperationNumbers");
                ExpandAllRaszGroups();
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при пересчете нумерации операций");
                MessageBox.Show($"Ошибка при пересчете нумерации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Перемещает выбранную операцию вверх
        /// </summary>
        public void MoveOperationUp()
        {
            try
            {
                var selectedOperation = GetSelectedOperation();
                if (selectedOperation == null)
                {
                    MessageBox.Show("Выберите операцию для перемещения.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var allOperations = _normRaszList.OrderBy(r => r.N).ThenBy(r => r.N1).ToList();
                int currentIndex = allOperations.IndexOf(selectedOperation);

                if (currentIndex <= 0)
                {
                    MessageBox.Show("Операция уже находится в начале списка.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var previousOperation = allOperations[currentIndex - 1];

				// Пограничный случай: предыдущая строка из другой главы или спецоперация (N1 >= 100)
				if (previousOperation.N != selectedOperation.N || previousOperation.N1 >= 100)
                {
                    gridViewRasz.BeginDataUpdate();
                    try
                    {
                        // Если выбранная операция уже отдельная глава (одна в своей главе)
                        bool isSingleChapter = _normRaszList.Count(r => r.N == selectedOperation.N) == 1 && selectedOperation.N1 == 0;
                        if (isSingleChapter)
                        {
							// Переместить в предыдущую главу (в конец главы)
							int targetN = previousOperation.N;
							int? afterN1 = _normRaszList.Where(r => r.N == targetN && r.N1 < 100).Select(r => (int?)r.N1).DefaultIfEmpty(null).Max();
							OperationNumberingService.MoveRaszIntoGroup(selectedOperation, targetN, afterN1, _normRaszList);
                        }
                        else
                        {
                            // Выделить в отдельную главу между главами: вставить как новую главу перед текущей
                            OperationNumberingService.InsertMainAfter(_normRaszList, previousOperation.N, selectedOperation);
                        }
                RecalculateAllOperationNumbers();
                    }
                    finally
                    {
                        gridViewRasz.EndDataUpdate();
                    }
                }
                else
                {
                    // Обычное перемещение внутри главы — обмен местами с предыдущей строкой
                    SwapOperationPositions(selectedOperation, previousOperation);
                    RecalculateAllOperationNumbers();
                }

                // Единая пост‑обработка
                ApplyPostStructureUi(selectedOperation, false);

                _logger.LogEventAsync($"Операция {selectedOperation.N}.{selectedOperation.N1} перемещена вверх", "MoveOperationUp");
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при перемещении операции вверх");
                MessageBox.Show($"Ошибка при перемещении операции: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Перемещает выбранную операцию вниз
        /// </summary>
        public void MoveOperationDown()
        {
            try
            {
                var selectedOperation = GetSelectedOperation();
                if (selectedOperation == null)
                {
                    MessageBox.Show("Выберите операцию для перемещения.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var allOperations = _normRaszList.OrderBy(r => r.N).ThenBy(r => r.N1).ToList();
                int currentIndex = allOperations.IndexOf(selectedOperation);

                if (currentIndex >= allOperations.Count - 1)
                {
                    MessageBox.Show("Операция уже находится в конце списка.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var nextOperation = allOperations[currentIndex + 1];

				// Пограничный случай: следующая строка из другой главы или спецоперация (N1 >= 100)
				if (nextOperation.N != selectedOperation.N || nextOperation.N1 >= 100)
                {
                    gridViewRasz.BeginDataUpdate();
                    try
                    {
                        bool isSingleChapter = _normRaszList.Count(r => r.N == selectedOperation.N) == 1 && selectedOperation.N1 == 0;
                        if (isSingleChapter)
                        {
							// Переместить в следующую главу (в начало главы)
							int targetN = nextOperation.N;
							int? afterN1 = -1; // вставляем перед первой обычной подоперацией
							OperationNumberingService.MoveRaszIntoGroup(selectedOperation, targetN, afterN1, _normRaszList);
                        }
                        else
                        {
                            // Выделить в отдельную главу между главами: вставить как новую главу после текущей
                            OperationNumberingService.InsertMainAfter(_normRaszList, selectedOperation.N, selectedOperation);
                        }
                RecalculateAllOperationNumbers();
                    }
                    finally
                    {
                        gridViewRasz.EndDataUpdate();
                    }
                }
                else
                {
                    // Обычное перемещение внутри главы — обмен местами со следующей строкой
                    SwapOperationPositions(selectedOperation, nextOperation);
                    RecalculateAllOperationNumbers();
                }

                // Единая пост‑обработка
                ApplyPostStructureUi(selectedOperation, false);

                _logger.LogEventAsync($"Операция {selectedOperation.N}.{selectedOperation.N1} перемещена вниз", "MoveOperationDown");
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при перемещении операции вниз");
                MessageBox.Show($"Ошибка при перемещении операции: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Получает выбранную операцию из грида
        /// </summary>
        /// <returns>Выбранная операция или null</returns>
        private NormRasz GetSelectedOperation()
        {
            if (gridViewRasz.FocusedRowHandle < 0)
                return null;

            return gridViewRasz.GetRow(gridViewRasz.FocusedRowHandle) as NormRasz;
        }

        /// <summary>
        /// Меняет местами две операции в списке (логически)
        /// </summary>
        /// <param name="operation1">Первая операция</param>
        /// <param name="operation2">Вторая операция</param>
        private void SwapOperationPositions(NormRasz operation1, NormRasz operation2)
        {
            // Меняем местами номера операций для логического изменения порядка
            int tempN = operation1.N;
            int tempN1 = operation1.N1;

            operation1.N = operation2.N;
            operation1.N1 = operation2.N1;

            operation2.N = tempN;
            operation2.N1 = tempN1;

            // Помечаем операции как измененные
            if (!operation1.IsNew) operation1.IsModified = true;
            if (!operation2.IsNew) operation2.IsModified = true;
        }

        /// <summary>
        /// Устанавливает фокус на указанную операцию в гриде
        /// </summary>
        /// <param name="operation">Операция для фокусировки</param>
        private void SetFocusToOperation(NormRasz operation)
        {
            try
            {
                if (operation == null) return;

                // Откладываем установку фокуса до следующего цикла UI, чтобы успел обновиться контроллер данных
                gridViewRasz.GridControl.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        // Обновляем и раскрываем группы, чтобы строка стала видимой
                        try { gridViewRasz.RefreshData(); } catch { }
                        try { ExpandAllRaszGroups(); } catch { }

                        int rowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle;

                        // 1) Пытаемся найти по первичному ключу, если он уже существует
                        try
                        {
                            if (operation.nrID > 0)
                            {
                                rowHandle = gridViewRasz.LocateByValue(nameof(NormRasz.nrID), operation.nrID);
                            }
                        }
                        catch { }

                        // 2) Иначе используем индекс из BindingSource (ListSource для GridView)
                        if (!gridViewRasz.IsValidRowHandle(rowHandle))
                        {
                            int listSourceIndex = -1;
                            try { listSourceIndex = _normRaszBindingSource != null ? _normRaszBindingSource.IndexOf(operation) : -1; } catch { }
                            if (listSourceIndex >= 0)
                            {
                                try { rowHandle = gridViewRasz.GetRowHandle(listSourceIndex); } catch { }
                            }
                        }

                        // 3) В крайнем случае — перебор видимых строк и сравнение по ссылке
                        if (!gridViewRasz.IsValidRowHandle(rowHandle))
                        {
                            try
                            {
                                for (int i = 0; i < gridViewRasz.DataRowCount; i++)
                                {
                                    int visibleHandle = gridViewRasz.GetVisibleRowHandle(i);
                                    var rowObj = gridViewRasz.GetRow(visibleHandle) as NormRasz;
                                    if (ReferenceEquals(rowObj, operation))
                                    {
                                        rowHandle = visibleHandle;
                                        break;
                                    }
                                }
                            }
                            catch { }
                        }

                        if (gridViewRasz.IsValidRowHandle(rowHandle))
                        {
                            _uiService?.RestoreFocusRow(gridViewRasz, rowHandle);
                        }
                    }
                    catch { }
                }));
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при установке фокуса на операцию");
            }
        }

        // Единая точка применения UI после структурных изменений (DnD/вставка/удаление)
        private void ApplyPostStructureUi(NormRasz focusOperation = null, bool clearSelection = false)
        {
            try
            {
                if (clearSelection)
                {
                    gridViewRasz.ClearSelection();
                }
                // Сначала обновляем и раскрываем, затем устанавливаем фокус
                gridViewRasz.RefreshData();
                ExpandAllRaszGroups();
                if (focusOperation != null)
                {
                    SetFocusToOperation(focusOperation);
                }
            }
            catch { }
        }
        private void FinalizeRaszBatch(NormRasz focusOperation = null, bool clearSelection = false)
        {
            try
            {
                _normRaszBindingSource?.ResetBindings(false);
                TWGridHelper.sortGridView(gridViewRasz);
                ApplyPostStructureUi(focusOperation, clearSelection);
            }
            catch { }
        }

        /// <summary>
        /// Проверяет и исправляет дублирующиеся номера операций
        /// </summary>
        public void ValidateAndFixOperationNumbers()
        {
            try
            {
                if (_normRaszList == null || _normRaszList.Count == 0)
                    return;

                var duplicates = _normRaszList
                    .GroupBy(r => new { r.N, r.N1 })
                    .Where(g => g.Count() > 1)
                    .ToList();

                if (duplicates.Any())
                {
                    string duplicatesList = string.Join(", ", duplicates.Select(d => $"{d.Key.N}.{d.Key.N1}"));

                    //var result = MessageBox.Show(
                    //    $"Обнаружены дублирующиеся номера операций: {duplicatesList}\n\n" +
                    //    "Выполнить автоматический пересчет нумерации?",
                    //    "Проблема с нумерацией",
                    //    MessageBoxButtons.YesNo,
                    //    MessageBoxIcon.Warning);

                    //if (result == DialogResult.Yes)
                    //{
                        RecalculateAllOperationNumbers();
                    //    MessageBox.Show("Нумерация операций исправлена.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                }
                else
                {
                    MessageBox.Show("Проблем с нумерацией не обнаружено.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при проверке нумерации операций");
                MessageBox.Show($"Ошибка при проверке нумерации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Обработчики кнопок управления нумерацией операций

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            MoveOperationUp();
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            MoveOperationDown();
        }
        private void btnRecalculateNumbers_Click(object sender, EventArgs e)
        {
            //RecalculateNumbers();
            RecalculateAllOperationNumbers();
        }



        private void btnValidateNumbers_Click(object sender, EventArgs e)
        {
            ValidateAndFixOperationNumbers();
        }

        #endregion

        #region Контекстное меню Rasz: добавление строки
        private PopupMenuShowingEventHandler CreateRaszContextMenu(
            GridView view,
            BindingList<NormRasz> bindingList,
            Func<NormRasz, int> getId,
            List<int> deletedIds)
        {
            return (s, e) =>
            {
                if (e.MenuType != GridMenuType.Row) return;
                var menu = e.Menu;

                // Подмешиваем универсальные пункты
                var generic = UIHelper.CreateContextMenu(view, bindingList, getId, deletedIds);
                generic?.Invoke(s, e);

                // Пункт "Добавить строку"
                var addItem = new DevExpress.Utils.Menu.DXMenuItem("Добавить операцию", (_, __) =>
                {
                    int rowHandle = e.HitInfo.RowHandle;
                    if (view.IsNewItemRow(rowHandle))
                    {
                        AddNewRaszOperation(null, true);
                    }
                    else if (view.IsValidRowHandle(rowHandle))
                    {
                        var rowObj = view.GetRow(rowHandle) as NormRasz;
                        if (rowObj != null)
                        {
                            AddNewRaszOperation(rowObj, false);
                        }
                    }
                    else
                    {
                        AddNewRaszOperation(null, true);
                    }
                });
                menu.Items.Add(addItem);

                // Пункт "Добавить подоперацию" — добавляет подоперацию сразу после выбранной строки в той же главе
                var addSubItem = new DevExpress.Utils.Menu.DXMenuItem("Добавить подоперацию", async (_, __) =>
                {
                    int rowHandle = e.HitInfo.RowHandle;
                    if (!view.IsValidRowHandle(rowHandle)) return;
                    var rowObj = view.GetRow(rowHandle) as NormRasz;
                    if (rowObj == null) return;

                    using (var selectionForm = new NormOperNew(_selectedAnnId))
                    {
                        var result = selectionForm.ShowDialog();
                        if (result == DialogResult.OK && selectionForm.SelectedRowData != null)
                        {
                            var selectedData = selectionForm.SelectedRowData;
                            selectedData.IsNew = true;
                            selectedData.IsBeingAdded = true;

                            int insertN1 = rowObj.N1 == 0 ? 2 : rowObj.N1 + 1;
                            bool convertMainToSub = rowObj.N1 == 0;

                            // Вставка подоперации в текущую главу (батч‑обновление и единая финализация)
                            gridViewRasz.BeginDataUpdate();
                            try
                            {
                                OperationNumberingService.InsertSuboperation(_normRaszList, rowObj.N, insertN1, convertMainToSub, selectedData);
                                FinalizeRaszBatch(selectedData, false);
                            }
                            finally
                            {
                                try { gridViewRasz.EndDataUpdate(); } catch { }
                            }

                            gridViewRasz.GridControl.BeginInvoke(new Action(() =>
                            {
                                _isCustomEditFormOpen = true;
                                gridViewRasz.ShowEditForm();
                            }));

                            await _logger.LogEventAsync($"Добавлена новая подоперация №{selectedData.N}.{selectedData.N1}", "AddSuboperation");
                        }
                    }
                });
                menu.Items.Add(addSubItem);
            };
        }
        #endregion

    }
}

