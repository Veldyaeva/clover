using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.TeamWork.Helpers;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z.Dapper.Plus;
using BindingSource = System.Windows.Forms.BindingSource;
using MethodInvoker = System.Windows.Forms.MethodInvoker;

namespace SewingProduction.Features.TeamWork.Forms
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

        public static class CloneUtils
        {
                    public static List<T> CloneList<T>(IEnumerable<T> source, int newAnnId, string idFieldName, bool markAsNew = true)
                where T : ICloneable
        {
            var list = new List<T>();
            foreach (var item in source)
            {
                var clone = (T)item.Clone();
                
                // Принудительно сбрасываем ID в 0
                var idProperty = typeof(T).GetProperty(idFieldName);
                if (idProperty != null)
                {
                    idProperty.SetValue(clone, 0);
                }
                else
                {
                    // Если свойство не найдено через рефлексию, пробуем альтернативные имена
                    var alternativeNames = new[] { "nrID", "id", "nkId", "NrID", "Id", "NkId" };
                    foreach (var altName in alternativeNames)
                    {
                        var altProperty = typeof(T).GetProperty(altName);
                        if (altProperty != null)
                        {
                            altProperty.SetValue(clone, 0);
                            break;
                        }
                    }
                }
                
                // Устанавливаем новый AnnId
                var annIdProperty = typeof(T).GetProperty("AnnId") ?? typeof(T).GetProperty("annId");
                if (annIdProperty != null)
                {
                    annIdProperty.SetValue(clone, newAnnId);
                }
                
                // Устанавливаем флаги
                var isNewProperty = typeof(T).GetProperty("IsNew");
                if (isNewProperty != null)
                {
                    isNewProperty.SetValue(clone, markAsNew);
                }
                
                var isModifiedProperty = typeof(T).GetProperty("IsModified");
                if (isModifiedProperty != null)
                {
                    isModifiedProperty.SetValue(clone, markAsNew);
                }
                
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
                    if (item is T clonedItem)
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

            // Перетаскивание строк в gridViewRasz
            if (gridControlRasz != null)
            {
                gridControlRasz.AllowDrop = true;
                gridViewRasz.MouseDown += gridViewRasz_MouseDown;
                gridViewRasz.MouseMove += gridViewRasz_MouseMove;
                gridControlRasz.DragOver += gridControlRasz_DragOver;
                gridControlRasz.DragDrop += gridControlRasz_DragDrop;
                gridControlRasz.DragLeave += gridControlRasz_DragLeave;
                gridControlRasz.QueryContinueDrag += gridControlRasz_QueryContinueDrag;

                // Поддержка перетаскивания остаётся через DoDragDrop (настройки DevExpress зависят от версии)
            }

            // Группировка по основному номеру операции (N)
            ConfigureRaszGrouping();

            _dbHelper = new DatabaseHelper();
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
            if (_isInitialLoading) return; // Игнорируем изменения во время начальной загрузки

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
                //_normRaszList.ListChanged -= (_, __) => _sekDebouncer.Debounce(10, async () => { if (_newAnnId > 0) RecalculateSek(); });
                //_normRaszList.ListChanged += (_, __) => _sekDebouncer.Debounce(10, async () => { if (_newAnnId > 0) RecalculateSek(); });
                //    _normRaskList.ListChanged += (_, __) => _sekDebouncer.Debounce(500, async () => { if (_newAnnId > 0) RecalculateSek(); }); это другие какие-то секунды
                //    _normKontList.ListChanged += (_, __) => _sekDebouncer.Debounce(500, async () => { if (_newAnnId > 0) RecalculateSek(); });
                bool allowDelete = _currentAnnData?.dateUpdate == null || _currentAnnData.dateUpdate == DateTime.MinValue;
                if (allowDelete)//(_mode == (int)Mode.ArchAndCopy || _mode == (int)Mode.NewWorkDivision || _mode ==(int)Mode.Clone)
                {
                    if (_raszPopupHandler == null)
                    {
                        _raszPopupHandler = ShowPopUpForRasz(gridViewRasz, _normRaszList, r => r.nrID, _deletedNormRaszIds);
                        gridViewRasz.PopupMenuShowing += _raszPopupHandler;
                    }
                    if (_kontPopupHandler == null)
                    {
                        _kontPopupHandler = ShowPopUp(gridViewKont, _normKontList, k => k.nkId, _deletedNormKontIds);
                        gridViewKont.PopupMenuShowing += _kontPopupHandler;
                    }
                }
                else
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
                }
                _bindingsInitialized = true;
            }
        }
        private void AttachDeleteContextMenu<T>(GridView view, BindingList<T> bindingList, Func<T, int> getId = null, List<int> deletedIds = null)
                    where T : class
        {
            try
            {
                view.PopupMenuShowing += ShowPopUp(view, bindingList, getId, deletedIds);
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при удалении строки TeamWork_AdvanceTW");
            }
        }

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
                view.PopupMenuShowing += ShowPopUpForRasz(view, bindingList, getId, deletedIds);
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
            return (s, e) =>
            {
                if (e.MenuType != GridMenuType.Row)
                    return;

                var menu = e.Menu;
                
                // Получаем количество выбранных строк
                int[] selectedRows = view.GetSelectedRows();
                bool hasSelectedRows = selectedRows != null && selectedRows.Length > 0;
                int totalRows = view.DataRowCount;
                
                // Пункты управления выделением
                var selectAllItem = new DevExpress.Utils.Menu.DXMenuItem("Выделить все", (_, __) =>
                {
                    view.SelectAll();
                });
                
                var clearSelectionItem = new DevExpress.Utils.Menu.DXMenuItem("Снять выделение", (_, __) =>
                {
                    view.ClearSelection();
                });
                
                // Пункт массового удаления (показываем только если есть выбранные строки)
                if (hasSelectedRows)
                {
                    var deleteSelectedItem = new DevExpress.Utils.Menu.DXMenuItem($"🗑 Удалить выбранные строки ({selectedRows.Length})", (_, __) =>
                    {
                        DeleteSelectedOperations(view, bindingList, getId, deletedIds);
                    });
                    menu.Items.Add(deleteSelectedItem);
                  //  menu.Items.Add(new DevExpress.Utils.Menu.DXMenuSeparator()); // Разделитель
                }
                
                // Добавляем пункты управления выделением
                menu.Items.Add(selectAllItem);
                if (hasSelectedRows)
                {
                    menu.Items.Add(clearSelectionItem);
                }
                
                if (totalRows > 0)
                {
               //     menu.Items.Add(new DevExpress.Utils.Menu.DXMenuSeparator()); // Разделитель
                }

                var deleteItem = new DevExpress.Utils.Menu.DXMenuItem("Удалить строку", (_, __) =>
                {
                    int rowHandle = e.HitInfo.RowHandle;
                    if (!view.IsValidRowHandle(rowHandle)) return;

                    var rowObj = view.GetRow(rowHandle) as NormRasz;
                    if (rowObj == null) return;

                    // Сохраняем номер удаляемой операции для перенумерации
                    int deletedOperationN = rowObj.N;
                    int deletedOperationN1 = rowObj.N1;

                    // Если удаляем последнюю сфокусированную операцию, сбрасываем ссылку
                    if (_lastFocusedRaszOperation != null &&
                        deletedOperationN == _lastFocusedRaszOperation.N &&
                        deletedOperationN1 == _lastFocusedRaszOperation.N1)
                    {
                        _lastFocusedRaszOperation = null;
                    }

                    // Добавляем в список удалённых
                    if (getId != null && deletedIds != null)
                    {
                        int id = getId(rowObj);
                        if (id > 0)
                            deletedIds.Add(id);
                    }

                    // Батч‑обновление UI
                    gridViewRasz.BeginDataUpdate();
                    try
                    {
                        // Удаляем строку
                        bindingList.Remove(rowObj);

                        // Перенумеровываем через единый сервис
                        OperationNumberingService.RenumberAfterDeletion(bindingList, deletedOperationN, deletedOperationN1);

                        // Делаем один пересчёт и сортировку
                        OperationNumberingService.RecalculateAllOperationNumbers(bindingList);
                        _normRaszBindingSource.ResetBindings(false);
                        TWGridHelper.sortGridView(gridViewRasz);

                        // Централизованная пост‑обработка UI
                        NormRasz focusOp = null;
                        if (view.DataRowCount > 0)
                        {
                            int newRowHandle = Math.Min(rowHandle, view.RowCount - 1);
                            newRowHandle = view.GetVisibleRowHandle(newRowHandle);
                            focusOp = view.IsValidRowHandle(newRowHandle) ? view.GetRow(newRowHandle) as NormRasz : null;
                        }
                        ApplyPostStructureUi(focusOp, false);
                    }
                    finally
                    {
                        gridViewRasz.EndDataUpdate();
                    }
                });

                // Добавляем пункт "Добавить строку"
                var addItem = new DevExpress.Utils.Menu.DXMenuItem("Добавить строку", (_, __) =>
                {
                    int rowHandle = e.HitInfo.RowHandle;

                    // Определяем тип строки и соответствующую логику
                    if (view.IsNewItemRow(rowHandle))
                    {
                        // Клик по newRow - добавляем в конец списка без диалога
                        AddNewRaszOperation(null, true);
                    }
                    else if (view.IsValidRowHandle(rowHandle))
                    {
                        // Клик по обычной строке - предлагаем варианты
                        var rowObj = view.GetRow(rowHandle) as NormRasz;
                        if (rowObj != null)
                        {
                            AddNewRaszOperation(rowObj, false);
                        }
                    }
                    else
                    {
                        // Клик в пустой области - добавляем в конец
                        AddNewRaszOperation(null, true);
                    }
                });

                menu.Items.Add(addItem);
                menu.Items.Add(deleteItem);
            };
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

                // Подтверждение удаления
                var result = MessageBox.Show(
                    $"Удалить {selectedRows.Length} выбранных операций?\n\nЭто действие нельзя отменить.",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (result != DialogResult.Yes)
                    return;

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

                // Батч‑обновление: удаление, один пересчёт, один ResetBindings/Sort, затем UI
                gridViewRasz.BeginDataUpdate();
                try
                {
                    foreach (var operation in operationsToDelete)
                    {
                        bindingList.Remove(operation);
                    }
                    OperationNumberingService.RecalculateAllOperationNumbers(bindingList);
                    _normRaszBindingSource.ResetBindings(false);
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
                MessageBox.Show($"Успешно удалено {operationsToDelete.Count} операций.\nНумерация операций пересчитана.", 
                               "Удаление завершено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при массовом удалении операций");
                MessageBox.Show($"Ошибка при удалении операций: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
        private async void AddNewRaszOperation(NormRasz insertAfterOperation = null, bool forceAppendToEnd = false)
        {
            try
            {
                int insertOperationN;
                int insertOperationN1 = 0;
                bool isSuboperation = false;
                OperationInsertChoice choice = new OperationInsertChoice(); // Объявляем в более широкой области видимости

                if (forceAppendToEnd || insertAfterOperation == null)
                {
                    // Добавляем в конец списка как основную операцию
                    int maxN = _normRaszList?.Select(x => x.N).DefaultIfEmpty(0).Max() ?? 0;
                    insertOperationN = maxN + 1;
                    insertOperationN1 = 0;
                }
                else
                {
                    // Предлагаем пользователю выбрать тип операции и позицию
                    choice = ShowOperationInsertDialog(insertAfterOperation);

                    if (choice.Cancel)
                    {
                        return; // Отмена
                    }

                    insertOperationN = choice.OperationN;
                    insertOperationN1 = choice.OperationN1;
                    isSuboperation = choice.IsSuboperation;
                }

                using (var selectionForm = new NormOperNew(_selectedAnnId))
                {
                    var result = selectionForm.ShowDialog();

                    if (result == DialogResult.OK && selectionForm.SelectedRowData != null)
                    {
                        var selectedData = selectionForm.SelectedRowData;
                        selectedData.IsNew = true;
                        selectedData.IsBeingAdded = true; // помечаем как добавляемую в текущей сессии
                        
                        // Вставка основной операции после текущей главы: всегда создаём новую главу (N+1.0) и сдвигаем последующие
                        if (!isSuboperation && insertOperationN1 == 0)
                        {
                            await _logger.LogEventAsync($"Вставка основной операции после главы {insertOperationN - 1}: создаётся {insertOperationN}.0", "AddNewRaszOperation");
                            OperationNumberingService.InsertMainAfter(_normRaszList, insertOperationN - 1, selectedData);
                        }
                        else if (isSuboperation)
                        {
                            // Подоперация: используем сервис с возможным преобразованием основной в подоперацию
                            OperationNumberingService.InsertSuboperation(_normRaszList, insertOperationN, insertOperationN1, choice.ConvertMainToSuboperation, selectedData);
                        }
                        else
                        {
                            // Прямая установка (редкий случай)
                            selectedData.N = insertOperationN;
                            selectedData.N1 = insertOperationN1;
                        }

                        // Массовое обновление UI во избежание мерцаний
                        gridViewRasz.BeginDataUpdate();
                        try
                        {
                            _normRaszList.Add(selectedData);
                            OperationNumberingService.RecalculateAllOperationNumbers(_normRaszList);
                        }
                        finally
                        {
                            gridViewRasz.EndDataUpdate();
                        }

                        _normRaszBindingSource.ResetBindings(false);
                        gridControlRasz.RefreshDataSource();

                        // Обновляем сортировку и применяем единую пост‑обработку (фокус на добавленной)
                        TWGridHelper.sortGridView(gridViewRasz);
                        ApplyPostStructureUi(selectedData, false);

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

        /// <summary>
        /// Проверяет наличие дублирующихся номеров операций
        /// </summary>
        /// <param name="n">Номер операции</param>
        /// <param name="n1">Номер подоперации</param>
        /// <returns>True если такой номер уже существует</returns>
        private bool HasDuplicateNumbers(int n, int n1)
        {
            return _normRaszList?.Any(r => r.N == n && r.N1 == n1) ?? false;
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



        // Удалены локальные Renumber*/Convert — используется OperationNumberingService

        private void OnNormRaszListChanged(object sender, ListChangedEventArgs e)
        {
            // Запускаем отложенный пересчёт Sek только если не идёт начальная загрузка
            if (!_isInitialLoading)
            {
                _sekDebouncer.Debounce(5, async () =>
                {
                    RecalculateSek();
                });
            }

            OnDataChanged(sender, e);
        }
        private async void TeamWork_AdvanceTW_Load(object sender, EventArgs e)
        {
            try
            {
                _isInitialLoading = true; // Устанавливаем флаг начальной загрузки

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
                        _lastFocusedRaszOperation = null; // Сбрасываем последнюю операцию
                        // Автоматически добавляем две стандартные строки в norm_kont
                        AddStandardKontRows();
                        break;
                    case (int)Mode.ArchAndCopy:
                        this.Text = $"Архив+копия";//. Артикул: {CreatedAnn.ArticulModel}" ;
                        var raszArch = await _artNormService.GetRelatedNormRasz(_selectedAnnId);
                        _normRaszList.BulkLoad(CloneUtils.CloneList(raszArch, _newAnnId, "nrId", false)); // markAsNew = false
                        LoadGridImage(pictureBox1, annId: _selectedAnnId);
                        _normRaskList.Clear();
                        _normKontList.Clear();
                        _lastFocusedRaszOperation = null; // Сбрасываем последнюю операцию
                        // Автоматически добавляем две стандартные строки в norm_kont
                        AddStandardKontRows();
                        _currentAnnData.dateCreate = DateTime.Now;
                        break;
                    case (int)Mode.Edit:
                        this.Text = $"Редактировать";//. Артикул: {_selectedAnnId.ArticulModel}";
                        await LoadForEdit(_selectedAnnId);
                        LoadGridImage(pictureBox1, annId: _selectedAnnId);
                        break;
                    case (int)Mode.Clone:
                        this.Text = $"Дубль";//. Артикул: {CreatedAnn.ArticulModel}";
                        var raszClone = await _artNormService.GetRelatedNormRasz(_selectedAnnId);
                        _normRaszList.BulkLoad(CloneUtils.CloneList(raszClone, _newAnnId, "nrId", false)); // markAsNew = false
                        _normRaskList.Clear();
                        _normKontList.Clear();
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

                        // 2) Проставляем текст оборудования в Obor
                        var obItem = oborudShvList?.FirstOrDefault(x => x.kod_ob == newKodOb);
                        if (obItem != null)
                        {
                            gridViewRasz.SetFocusedRowCellValue("Obor", obItem.text_ob);
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
                    
                    gridViewRasz.BeginDataUpdate();
                    gridViewRaskr.BeginDataUpdate();
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

                        OperationNumberingService.RecalculateAllOperationNumbers(_normRaszList);
                        _normRaszBindingSource.ResetBindings(false);
                        _normRaskBindingSource.ResetBindings(false);
                        _normKontBindingSource.ResetBindings(false);
                        TWGridHelper.sortGridView(gridViewRasz);
                        ApplyPostStructureUi(_normRaszList.FirstOrDefault(), true);
                    }
                    finally
                    {
                        try { gridViewRasz.EndDataUpdate(); } catch { }
                        try { gridViewRaskr.EndDataUpdate(); } catch { }
                        try { gridViewKont.EndDataUpdate(); } catch { }
                    }


                    //  _hasUnsavedChanges = true;
                    //UpdateFormTitle();
                    //DisplayCurrentAnnData(); // Обновить поля на форме данными из _currentAnn
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
                };
            }
        }
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
        private async Task LoadAndCloneAll(int sourceAnnId, int newAnnId)
        {
            var rasz = await _artNormService.GetRelatedNormRasz(sourceAnnId);
            var rask = await _artNormService.GetRelatedNormRask(sourceAnnId);
            var kont = await _artNormService.GetRelatedNormKont(sourceAnnId);

            var clonedRasz = CloneUtils.CloneList(rasz, newAnnId, "nrId", false); // markAsNew = false для начальной загрузки
            var clonedRask = CloneUtils.CloneList(rask, newAnnId, "id", false);
            var clonedKont = CloneUtils.CloneList(kont, newAnnId, "nkId", false);

            _normRaszList.BulkLoad(clonedRasz);
            _normRaskList.BulkLoad(clonedRask);
            _normKontList.BulkLoad(clonedKont);
        }

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



        private async Task LoadForEdit(int annId)
        {
            var rasz = await _artNormService.GetRelatedNormRasz(annId);
            var rask = await _artNormService.GetRelatedNormRask(annId);
            var kont = await _artNormService.GetRelatedNormKont(annId);

            // Убеждаемся, что все элементы помечены как неизмененные
            foreach (var item in rasz)
            {
                item.IsNew = false;
                item.IsModified = false;
            }
            foreach (var item in rask)
            {
                item.IsNew = false;
                item.IsModified = false;
            }
            foreach (var item in kont)
            {
                item.IsNew = false;
                item.IsModified = false;
            }

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

            // Используем новый метод для добавления операции
            // Определяем, нужно ли предлагать позицию или добавить в конец
            bool shouldOfferPosition = _lastFocusedRaszOperation != null && _normRaszList?.Count > 0;

            if (shouldOfferPosition)
            {
                // Предлагаем позицию на основе последней сфокусированной операции
                AddNewRaszOperation(_lastFocusedRaszOperation, false);
            }
            else
            {
                // Добавляем в конец списка
                AddNewRaszOperation(null, true);
            }
        }

        private void gridViewRasz_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (e.Row is NormRasz row)
            {
                // Проверяем уникальность сочетания N/N1
                bool duplicate = _normRaszList.Any(x =>
                    x != row && // исключаем саму себя при редактировании
                    x.N == row.N && x.N1 == row.N1);

                if (duplicate)
                {
                    e.Valid = false;
                    e.ErrorText = $"Операция с номером {row.N} и подоперацией {row.N1} уже существует!";
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
                    // Восстанавливаем оригинальные данные.
                    _normRaszList[index].CopyPropertiesFrom(_originalNormRaszDataBeforeEdit);
                    _logger.LogEventAsync($"NormRasz row (nrId: {_originalNormRaszDataBeforeEdit.nrID}) edit canceled, reverted to original state.", "gridViewRasz_RowEditCanceled");
                    _normRaszBindingSource.ResetBindings(false);
                }
            }

            _originalNormRaszDataBeforeEdit = null; // Очищаем сохраненное состояние
                                                    // view.HideEditForm(); // Обычно не требуется, грид сам закроет форму при отмене
            _hasUnsavedChanges = true; // Список данных изменился или редактирование отменено
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
                gridViewRasz.UpdateCurrentRow(); // Обновляем строку, чтобы RowStyle сработал
            }
        }


        // Drag & Drop для gridViewRasz
        private Point _raszDragStartPoint;
        private int _raszDragSourceHandle = -1;
        private bool _raszDragging = false;

        private void gridViewRasz_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                _raszDragStartPoint = e.Location;
                var hit = ((GridView)sender).CalcHitInfo(e.Location);
                _raszDragSourceHandle = hit.RowHandle;
                _raszDragging = false;
            }
            catch { }
        }

        private void gridViewRasz_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
                if (_raszDragSourceHandle < 0) return;

                Size dragSize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(
                    new Point(_raszDragStartPoint.X - dragSize.Width / 2, _raszDragStartPoint.Y - dragSize.Height / 2),
                    dragSize);

                //if (!dragRect.Contains(e.Location))
                //{
                //    var view = (GridView)sender;
                //    var row = view.GetRow(_raszDragSourceHandle) as NormRasz;
                //    if (row == null) return;
                //    _raszDragging = true;
                //    // Показ превью перетаскиваемой строки
                //    ShowRaszAdorner(row, Control.MousePosition);
                //    view.GridControl.DoDragDrop(row, DragDropEffects.Move);
                //}
                if (!dragRect.Contains(e.Location))
                {
                    var view = (GridView)sender;

                    var draggedList = GetSelectedRaszRowsOrCurrent(view);
                    if (draggedList == null || draggedList.Count == 0) return;

                    _raszDragging = true;

                    // Готовим DataObject: и список, и одиночную — для обратной совместимости
                    var data = new DataObject();
                    data.SetData(typeof(List<NormRasz>), draggedList);
                    data.SetData(typeof(NormRasz), draggedList[0]);

                    view.GridControl.DoDragDrop(data, DragDropEffects.Move);
                }

            }
            catch { }
        }

        private void gridControlRasz_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                if (!e.Data.GetDataPresent(typeof(List<NormRasz>)) && !e.Data.GetDataPresent(typeof(NormRasz)))
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }

                Point clientPoint = ((Control)sender).PointToClient(new Point(e.X, e.Y));
                var hit = gridViewRasz.CalcHitInfo(clientPoint);
                if (hit.InRow && hit.RowHandle >= 0 && !gridViewRasz.IsNewItemRow(hit.RowHandle))
                {
                    e.Effect = DragDropEffects.Move;
                }
                else if (gridViewRasz.IsGroupRow(hit.RowHandle))
                {
                    // Разрешаем дроп на заголовок группы (перенос в группу)
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    // Разрешаем дроп в пустую область грида — вынести в отдельную операцию
                    e.Effect = DragDropEffects.Move;
                }

                // Явная автопрокрутка у верхней/нижней кромки
                const int margin = 24;
                var bounds = gridControlRasz.ClientRectangle;
                if (clientPoint.Y <= bounds.Top + margin)
                {
                    gridViewRasz.TopRowIndex = Math.Max(0, gridViewRasz.TopRowIndex - 1);
                }
                else if (clientPoint.Y >= bounds.Bottom - margin)
                {
                    gridViewRasz.TopRowIndex = gridViewRasz.TopRowIndex + 1;
                }
            }
            catch { e.Effect = DragDropEffects.None; }
        }

        private void gridControlRasz_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                gridViewRasz.BeginDataUpdate();
                if (!_raszDragging) { gridViewRasz.EndDataUpdate(); return; }
                _raszDragging = false;

                // Достаём список или одиночную
                var draggedList = e.Data.GetData(typeof(List<NormRasz>)) as List<NormRasz>;
                if (draggedList == null)
                {
                    var single = e.Data.GetData(typeof(NormRasz)) as NormRasz;
                    if (single != null) draggedList = new List<NormRasz> { single };
                }
                if (draggedList == null || draggedList.Count == 0) { gridViewRasz.EndDataUpdate(); return; }

                // Защитимся: список без null и повторов, в «экранном» порядке
                draggedList = draggedList
                    .Where(r => r != null)
                    .Distinct()
                    .OrderBy(r => r.N)
                    .ThenBy(r => r.N1)
                    .ToList();

                Point clientPoint = ((Control)sender).PointToClient(new Point(e.X, e.Y));
                var hit = gridViewRasz.CalcHitInfo(clientPoint);

                bool ctrl = (Control.ModifierKeys & Keys.Control) == Keys.Control;

                bool changed = false;

                // 1) Бросили на заголовок группы → положим блок в конец этой группы
                if (gridViewRasz.IsGroupRow(hit.RowHandle))
                {
                    var colN = gridViewRasz.Columns.ColumnByFieldName("N");
                    if (colN != null)
                    {
                        var groupValue = gridViewRasz.GetGroupRowValue(hit.RowHandle, colN);
                        if (groupValue != null && int.TryParse(groupValue.ToString(), out int groupN))
                        {
                            // No-op: блок уже в конце этой группы в том же порядке
                            if (!AreDraggedAtGroupEnd(draggedList, groupN))
                            {
                                int afterN1 = GetMaxN1(groupN);
                                foreach (var r in draggedList)
                                    MoveRaszIntoGroup(r, groupN, afterN1++);
                                changed = true;
                            }
                        }
                    }
                }
                // 2) Пустая область → превратить блок в отдельные главы в конце (N1=0)
                else if (!(hit.InRow && hit.RowHandle >= 0))
                {
                    // No-op: весь блок уже является последними главами (N1=0) и идёт подряд в конце
                    if (!AreDraggedAtEndAsTopLevel(draggedList))
                    {
                        int newN = _normRaszList.Any() ? _normRaszList.Max(r => r.N) + 1 : 1;
                        foreach (var r in draggedList)
                        {
                            r.N = newN++;
                            r.N1 = 0;
                            if (!r.IsNew) r.IsModified = true;
                        }
                        changed = true;
                    }
                }
                // 3) Бросили на строку
                else
                {
                    if (gridViewRasz.IsNewItemRow(hit.RowHandle)) { gridViewRasz.EndDataUpdate(); return; }

                    var target = gridViewRasz.GetRow(hit.RowHandle) as NormRasz;
                    if (target == null) { gridViewRasz.EndDataUpdate(); return; }
                    if (draggedList.Contains(target)) { gridViewRasz.EndDataUpdate(); return; } // не вкладываем блок сам в себя

                    // Модификаторы: Alt → вынести блок в начало (глава 1), Ctrl + Drop на главу → вынести блок отдельной главой после этой
                    bool alt = (Control.ModifierKeys & Keys.Alt) == Keys.Alt;

                    if (alt)
                    {
                        if (!AreDraggedAtStartAsTopLevel(draggedList))
                        {
                            MoveRowsToTopLevelAtStart(draggedList);
                            changed = true;
                        }
                        if (changed)
                        {
                            OperationNumberingService.RecalculateAllOperationNumbers(_normRaszList);
                            _normRaszBindingSource.ResetBindings(false);
                            TWGridHelper.sortGridView(gridViewRasz);
                            ApplyPostStructureUi(draggedList.FirstOrDefault(), draggedList.Count > 1);
                        }
                        return;
                    }

                    if (ctrl && target.N1 == 0)
                    {
                        if (MoveRowsToTopLevelInsertAfter(draggedList, target.N))
                        {
                            changed = true;
                        }
                        if (changed)
                        {
                            OperationNumberingService.RecalculateAllOperationNumbers(_normRaszList);
                            _normRaszBindingSource.ResetBindings(false);
                            TWGridHelper.sortGridView(gridViewRasz);
                            ApplyPostStructureUi(draggedList.FirstOrDefault(), draggedList.Count > 1);
                        }
                        return;
                    }

                    // Ctrl+Drop «обмен одиночных» — работает только для одиночного DnD, как и раньше
                    if (draggedList.Count == 1 && ctrl && target.N != draggedList[0].N && target.N1 == 0 && draggedList[0].N1 == 0)
                    {
                        var dragged = draggedList[0];
                        int tmpN = dragged.N;
                        dragged.N = target.N;
                        target.N = tmpN;
                        if (!dragged.IsNew) dragged.IsModified = true;
                        if (!target.IsNew) target.IsModified = true;
                        changed = true;
                    }
                    else
                    {
                        // Если переносим между разными главами → кладём блок после target в его главе
                        if (draggedList.Any(r => r.N != target.N))
                        {
                            // No-op: блок уже идёт сразу после target в этой главе
                            if (!AreDraggedConsecutiveAfterTargetInGroup(draggedList, target))
                            {
                                int afterN1 = target.N1;
                                foreach (var r in draggedList)
                                    MoveRaszIntoGroup(r, target.N, afterN1++);
                                changed = true;
                            }
                        }
                        // Внутри одной главы → вставляем блок после target, с сохранением относительного порядка
                        else
                        {
                            // No-op: блок уже расположен сразу после target в этой же главе
                            if (!AreDraggedConsecutiveAfterTargetInGroup(draggedList, target))
                            {
                                MoveBlockWithinSameGroup(draggedList, target);
                                changed = true;
                            }
                        }
                    }
                }

                // Финал батча — только если реально что‑то изменили
                if (changed)
                {
                    OperationNumberingService.RecalculateAllOperationNumbers(_normRaszList);
                    _normRaszBindingSource.ResetBindings(false);
                    TWGridHelper.sortGridView(gridViewRasz);
                    ApplyPostStructureUi(draggedList.FirstOrDefault(), draggedList.Count > 1);
                }
            }
            catch { }
            finally { try { gridViewRasz.EndDataUpdate(); } catch { } }
        }

        private int GetMaxN1(int groupN)
        {
            var items = _normRaszList.Where(x => x.N == groupN).ToList();
            return items.Count == 0 ? 0 : items.Max(x => x.N1);
        }

        // Проверка: все перетаскиваемые операции уже находятся в конце указанной главы groupN, подряд и в том же порядке
        private bool AreDraggedAtGroupEnd(List<NormRasz> dragged, int groupN)
        {
            if (dragged == null || dragged.Count == 0) return true;
            // Все относятся к этой главе
            if (dragged.Any(r => r.N != groupN)) return false;

            // Максимальный N1 в группе без учёта перетаскиваемых
            var othersN1 = _normRaszList.Where(r => r.N == groupN && !dragged.Contains(r)).Select(r => r.N1).ToList();
            int maxOthers = othersN1.Count == 0 ? 0 : othersN1.Max();

            var byN1 = dragged.OrderBy(r => r.N1).ToList();
            // Последовательность должна начинаться сразу после maxOthers: maxOthers+1, +2, ...
            for (int i = 0; i < byN1.Count; i++)
            {
                if (byN1[i].N1 != maxOthers + 1 + i) return false;
            }
            return true;
        }

        // Проверка: блок уже является последними главами (N1 == 0) и их N идут подряд от (maxN - k + 1) до maxN
        private bool AreDraggedAtEndAsTopLevel(List<NormRasz> dragged)
        {
            if (dragged == null || dragged.Count == 0) return true;
            if (dragged.Any(r => r.N1 != 0)) return false;

            int maxN = _normRaszList.Any() ? _normRaszList.Max(r => r.N) : 0;
            var draggedByN = dragged.OrderBy(r => r.N).ToList();
            // Проверим, что они занимают хвост и идут подряд
            int expectedStart = maxN - draggedByN.Count + 1;
            for (int i = 0; i < draggedByN.Count; i++)
            {
                if (draggedByN[i].N != expectedStart + i) return false;
            }
            return true;
        }

        // Проверка: блок уже верхнеуровневый (N1==0) и находится в самом начале (N идут подряд от 1)
        private bool AreDraggedAtStartAsTopLevel(List<NormRasz> dragged)
        {
            if (dragged == null || dragged.Count == 0) return true;
            if (dragged.Any(r => r.N1 != 0)) return false;

            var draggedByN = dragged.OrderBy(r => r.N).ToList();
            for (int i = 0; i < draggedByN.Count; i++)
            {
                if (draggedByN[i].N != 1 + i) return false;
            }
            return true;
        }

        // Перевод строк в верхний уровень (N1=0 если одиночная, иначе 1..n) и помещение в самое начало (остаток исходной главы становится главой 2)
        private void MoveRowsToTopLevelAtStart(List<NormRasz> dragged)
        {
            if (dragged == null || dragged.Count == 0) return;

            int sourceN = dragged[0].N;
            var restOfSource = _normRaszList
                .Where(r => r.N == sourceN && !dragged.Contains(r))
                .OrderBy(r => r.N1)
                .ToList();

            // Сдвигаем все главы, кроме исходной, на +1 (освобождаем место для будущей главы 2)
            foreach (var op in _normRaszList.Where(r => r.N != sourceN && !dragged.Contains(r)))
            {
                int oldN = op.N;
                op.N = oldN + 1;
                if (!op.IsNew && op.N != oldN) op.IsModified = true;
            }

            // Переносимый блок становится главой 1, N1 = 0 если одна строка, иначе 1..n
            var block = dragged.OrderBy(r => r.N1).ToList();
            for (int i = 0; i < block.Count; i++)
            {
                int oldN = block[i].N;
                int oldN1 = block[i].N1;
                block[i].N = 1;
                block[i].N1 = (block.Count == 1) ? 0 : i + 1;
                if (!block[i].IsNew && (block[i].N != oldN || block[i].N1 != oldN1)) block[i].IsModified = true;
            }

            // Остаток исходной главы становится главой 2: N1 = 1..n
            if (restOfSource.Count > 0)
            {
                for (int i = 0; i < restOfSource.Count; i++)
                {
                    var it = restOfSource[i];
                    int oldN = it.N;
                    int oldN1 = it.N1;
                    it.N = 2;
                    it.N1 = i + 1;
                    if (!it.IsNew && (it.N != oldN || it.N1 != oldN1)) it.IsModified = true;
                }
            }
        }

        // Проверка: блок уже расположен сразу после target в группе target.N (в порядке N1 = target.N1+1, +2, ...)
        private bool AreDraggedConsecutiveAfterTargetInGroup(List<NormRasz> dragged, NormRasz target)
        {
            if (target == null || dragged == null || dragged.Count == 0) return false;
            int groupN = target.N;
            var byN1 = dragged.OrderBy(r => r.N1).ToList();

            // Все в одной главе
            if (byN1.Any(r => r.N != groupN)) return false;

            // Должны начинаться именно после target.N1
            for (int i = 0; i < byN1.Count; i++)
            {
                if (byN1[i].N1 != target.N1 + 1 + i) return false;
            }
            return true;
        }

        // Вынести блок в отдельную главу непосредственно после главы afterN (afterN >= 0)
        private bool MoveRowsToTopLevelInsertAfter(List<NormRasz> dragged, int afterN)
        {
            if (dragged == null || dragged.Count == 0) return false;

            // 1) Сдвигаем все главы с N > afterN (кроме переносимых) на +1
            foreach (var op in _normRaszList.Where(r => r.N > afterN && !dragged.Contains(r)))
            {
                int oldN = op.N;
                op.N = oldN + 1;
                if (!op.IsNew && op.N != oldN) op.IsModified = true;
            }

            // 2) Переносимый блок становится новой главой afterN+1. Если в блоке одна строка — N1=0, иначе N1 = 1..
            var ordered = dragged.OrderBy(r => r.N).ThenBy(r => r.N1).ToList();
            for (int i = 0; i < ordered.Count; i++)
            {
                var it = ordered[i];
                int oldN = it.N, oldN1 = it.N1;
                it.N = afterN + 1;
                it.N1 = (ordered.Count == 1) ? 0 : i + 1;
                if (!it.IsNew && (it.N != oldN || it.N1 != oldN1)) it.IsModified = true;
            }

            return true;
        }

        // Вставка блока после target внутри одной и той же главы (N совпадают)
        private void MoveBlockWithinSameGroup(List<NormRasz> block, NormRasz target)
        {
            OperationNumberingService.MoveBlockWithinSameGroup(_normRaszList, block, target);
        }

        private void gridControlRasz_DragLeave(object sender, EventArgs e) { }

        private void gridControlRasz_QueryContinueDrag(object sender, QueryContinueDragEventArgs e) { }

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
            }
            catch { }
        }

        // Перенос операции в указанную группу с размещением в конце подгруппы (или после указанного N1)
        private void MoveRaszIntoGroup(NormRasz dragged, int targetGroupN, int? desiredAfterN1)
        {
            OperationNumberingService.MoveRaszIntoGroup(dragged, targetGroupN, desiredAfterN1, _normRaszList);
        }

        // Визуализация перетаскивания — используем встроенную DevExpress (адорнер удалён)

        // Собираем выделенные строки (или текущую), упорядочиваем как на экране
        private List<NormRasz> GetSelectedRaszRowsOrCurrent(GridView view)
        {
            var result = new List<NormRasz>();

            var selectedHandles = view.GetSelectedRows()
                .Where(h => h >= 0 && !view.IsGroupRow(h) && !view.IsNewItemRow(h))
                .ToArray();

            if (selectedHandles.Length > 0)
            {
                foreach (var h in selectedHandles)
                {
                    if (view.GetRow(h) is NormRasz r && r != null)
                        result.Add(r);
                }
            }
            else if (_raszDragSourceHandle >= 0)
            {
                var r = view.GetRow(_raszDragSourceHandle) as NormRasz;
                if (r != null) result.Add(r);
            }

            return result
                .Distinct()
                .OrderBy(r => r.N)
                .ThenBy(r => r.N1)
                .ToList();
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
                    MessageBox.Show("На одно РТ можно добавить максимум 2 строки контроля.", "Ограничение", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                // Гарантируем заполнение parentId для сценария дублирования
                if (_mode == (int)Mode.Clone && _selectedAnnId > 0)
                {
                    _currentAnnData.ParentId = _selectedAnnId;
                }

                await _dbHelper.ExecuteInTransactionAsync(async () =>
                {
                    await SaveAnnDataAsync();
                    await SaveAllDataAsync();
                });

                // Эти действия выполняются ПОСЛЕ успешной транзакции
                IsRaszInserted = true; // Предполагаем, что если сохранение дошло сюда, то все списки были обработаны
                IsRaskInserted = true;
                IsKontInserted = true;
                // Сбрасываем флаги и обновляем снапшоты для корректной подсветки
                ResetFlagsAndSnapshots();
                _hasUnsavedChanges = false;

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

        private void ResetFlagsAndSnapshots()
        {
            try
            {
                gridViewRasz.BeginDataUpdate();
                gridViewRaskr.BeginDataUpdate();
                gridViewKont.BeginDataUpdate();
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
                finally
                {
                    try { gridViewRasz.EndDataUpdate(); } catch { }
                    try { gridViewRaskr.EndDataUpdate(); } catch { }
                    try { gridViewKont.EndDataUpdate(); } catch { }
                }
            }
            catch { }
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
                {
                    RecalculateSek();
                    //  var calculatedData = await _artNormService.GetCalculatedSekFromViewAsync(_newAnnId);
                    ////  Thread.Sleep(5000);

                    //  // 3. ОБНОВЛЯЕМ нашу основную модель _currentAnnData этими данными
                    //  if (calculatedData != null)
                    //  {
                    //      _currentAnnData.SekVyaz = calculatedData.sek_sh1;
                    //      _currentAnnData.SekVyazo = calculatedData.sek_O;
                    //      _currentAnnData.SekVyaz3 = calculatedData.sek_3;
                    //      _currentAnnData.SekVyaz5 = calculatedData.sek_5;
                    //      _currentAnnData.SekVyaz12 = calculatedData.sek_12;
                    //      _currentAnnData.SekVyaz7 = calculatedData.sek_7;
                    //      _currentAnnData.SekVyaz10 = calculatedData.sek_10;
                    //      _currentAnnData.SekVyaz6 = calculatedData.sek_6;
                    //      _currentAnnData.SekVyaz3 = calculatedData.sek_3; 
                    //      _currentAnnData.SekVyaz70 = calculatedData.sek_70;
                    //      _currentAnnData.SekVyaz71 = calculatedData.sek_71;
                    //      _currentAnnData.SekVyaz72 = calculatedData.sek_72;
                    //      _currentAnnData.SekVyaz62 = calculatedData.sek_62;
                    //      _currentAnnData.SekVyaz14 = calculatedData.sek_14;
                    //      _currentAnnData.SekVyaz57 = calculatedData.sek_57;
                    //      _currentAnnData.SekVyaz18 = calculatedData.sek_18;
                    //      _currentAnnData.SekShv = calculatedData.sek_shv;
                    //      //_currentAnnData.SekShv1 = calculatedData.sek_sh1;
                    //      _currentAnnData.SekKr = calculatedData.sek_kr;
                    //      _currentAnnData.Sek = calculatedData.sk;       // 'sk' из view - это общая сумма секунд
                    //      _currentAnnData.Seb = (int)calculatedData.sb; // 'sb' из view - это себестоимость 
                    //  }
                    //  else
                    //  {
                    //      // Обработка случая, если для annId нет данных в представлении (например, если нет операций)
                    //       //_logger.LogWarningAsync()$"Не найдены расчетные данные в NormRaszSek_view для annId: {_newAnnId}");
                    //  }

                }
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
                foreach (var item in itemsToInsert)
                {
                    var annIdProp = typeof(T).GetProperty("annId");
                    if (annIdProp != null)
                    {
                        annIdProp.SetValue(item, newAnnId);
                    }
                }
                var logStringBuilder = new StringBuilder();
                using (var connection = _dbHelper.GetConnection())
                {
                    try
                    {
                        await _logger.LogEventAsync($"[{itemTypeName}] BulkInsert: {itemsToInsert.Count}", "SaveListAsync");
                        bulkStopwatch.Restart();
                        //await connection.BulkInsertAsync(itemsToInsert);
                        await connection.UseBulkOptions(options =>
                        {
                            // Включаем логирование и указываем, куда записывать лог
                            options.Log = (log) => logStringBuilder.AppendLine(log);
                        })
                   .BulkInsertAsync(itemsToInsert);
                        bulkStopwatch.Stop();
                    }
                    catch (Exception ex)
                    {
                        // Если произошла ошибка, сначала записываем перехваченный SQL-запрос
                        await _logger.LogErrorAsync(ex, $"Ошибка при BulkInsert [{itemTypeName}]. Перехваченный SQL:\n{logStringBuilder.ToString()}");
                        throw; // Пробрасываем исключение дальше
                    }
                }

                foreach (var item in itemsToInsert)
                    item.IsNew = false;
            }

            // 3. Обновление (только если не shouldInsertAllAsNew)
            if (itemsToUpdate.Any())
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    await _logger.LogEventAsync($"[{itemTypeName}] BulkUpdate: {itemsToUpdate.Count}", "SaveListAsync");
                    bulkStopwatch.Restart();
                    await connection.BulkUpdateAsync(itemsToUpdate);
                    bulkStopwatch.Stop();

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
            // Используем глобальный буфер если доступен, иначе локальный
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

                    if (result == DialogResult.Yes)
                    {
                        if (_normRaszList == null || _normRaskList == null || _normKontList == null)
                        {
                            await InitializeBindingsAsync();
                        }

                        // Помечаем существующие записи на удаление как при обычном удалении
                        if (_normRaszList?.Count > 0)
                        {
                            var existingIds = _normRaszList.Where(x => !x.IsNew && x.nrID > 0).Select(x => x.nrID).ToList();
                            if (existingIds.Count > 0)
                            {
                                using (var connection = _dbHelper.GetConnection())
                                {
                                    string deleteSql = "UPDATE norm_rasz SET nrDateDel = GETDATE(), nrCompDel = HOST_NAME() WHERE nrId IN @ids";
                                    await connection.ExecuteAsync(deleteSql, new { ids = existingIds });
                                    await _logger.LogEventAsync($"Помечено на удаление операций NormRasz: {existingIds.Count}", "buffer_Click");
                                }
                            }
                        }

                        // Очищаем текущие списки
                        _normRaszList?.Clear();
                        _lastFocusedRaszOperation = null; // Сбрасываем последнюю операцию

                        _normRaszBindingSource?.ResetBindings(false);
                    }

                    // Батч вставки из буфера с единым пересчётом/сортировкой/ResetBindings
                    gridViewRasz.BeginDataUpdate();
                    try
                    {
                        // Логика: первая группа — исходные N/N1; вторая и последующие — смещение по N на текущий максимум, N1 сохраняем
                        int currentMaxN = (_normRaszList != null && _normRaszList.Count > 0)
                            ? _normRaszList.Select(x => x.N).DefaultIfEmpty(0).Max()
                            : 0;

                        foreach (var id in bufferIdsToUse)
                        {
                            List<NormRasz> raszList = await _artNormService.GetRelatedNormRasz(id);
                            if (raszList == null) continue;
                            int partMaxN = raszList.Select(x => x.N).DefaultIfEmpty(0).Max();
                            int offset = currentMaxN; // для первой группы будет 0, далее — сдвиг
                            foreach (var item in raszList)
                            {
                                item.nrID = 0;
                                item.annId = _currentAnnData?.AnnID ?? 0;
                                item.nrDateAdd = null;
                                item.nrCompAdd = null;
                                item.IsNew = true;
                                // сохраняем подоперации, сдвигаем только N на offset
                                item.N = item.N + offset;
                                _normRaszList.Add(item);
                            }
                            currentMaxN += partMaxN; // обновляем максимум для следующей группы
                        }

                        // Сохраняем N/N1 с учетом группового смещения, общего пересчёта не делаем
                        _normRaszBindingSource.ResetBindings(false);
                        TWGridHelper.sortGridView(gridViewRasz);
                        ApplyPostStructureUi(_normRaszList.FirstOrDefault(), true);
                    }
                    finally
                    {
                        gridViewRasz.EndDataUpdate();
                    }

                    await ShowStatusMessage("Данные из буфера успешно загружены");
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
        private async Task ShowStatusMessage(string message, int delayMs = 3000, Color? color = null)
        {
            statusLabel.Text = message;

            if (color.HasValue)
            {
                var originalColor = statusLabel.ForeColor;
                statusLabel.ForeColor = color.Value;

                await Task.Delay(delayMs);
                statusLabel.Text = "";
                statusLabel.ForeColor = originalColor;
            }
            else
            {
                await Task.Delay(delayMs);
                statusLabel.Text = "";
            }
        }

        private void OnDataChanged(object sender, ListChangedEventArgs e)
        {
            // Помечаем изменения только если не идёт начальная загрузка
            if (!_isInitialLoading && (e.ListChangedType != ListChangedType.Reset || _normRaszList.Count > 0 || _normRaskList.Count > 0 || _normKontList.Count > 0))
            {
                _hasUnsavedChanges = true;
            }
        }

        /// <summary>
        /// Обновляет отображение буфера на основе глобального или локального состояния
        /// </summary>
        private void UpdateBufferDisplay()
        {
            try
            {
                if (TeamWorkBuffer.HasData)
                {
                    // Проверяем, является ли это комплектом (Kit режим)
                    if (TeamWorkBuffer.BufferIds?.Count > 1)
                    {
                        // Форматируем отображение для комплекта
                        var bufferText = TeamWorkBuffer.BufferText;
                        if (!string.IsNullOrEmpty(bufferText))
                        {
                            // Разделяем части комплекта по разделителю
                            var parts = bufferText.Split(new string[] { "----------------------------" }, StringSplitOptions.RemoveEmptyEntries);
                            
                            if (parts.Length >= 2)
                            {
                                var formattedText = new System.Text.StringBuilder();
                                formattedText.AppendLine("📦 КОМПЛЕКТ (2 части):");
                                formattedText.AppendLine();
                                
                                formattedText.AppendLine("🔸 Часть 1:");
                                formattedText.AppendLine(parts[0].Trim());
                                formattedText.AppendLine();
                                
                                formattedText.AppendLine("🔸 Часть 2:");
                                formattedText.Append(parts[1].Trim());
                                
                                textBoxBuffer.Text = formattedText.ToString();
                            }
                            else
                            {
                                // Если разделитель не найден, показываем как есть
                                textBoxBuffer.Text = "📦 КОМПЛЕКТ:\n\n" + bufferText;
                            }
                        }
                        else
                        {
                            textBoxBuffer.Text = "📦 КОМПЛЕКТ (2 части)";
                        }
                        
                        // Обновляем текст кнопки для комплекта
                        buffer.Text = "Вставить комплект из буфера:";
                    }
                    else
                    {
                        // Обычный режим (одна запись)
                        textBoxBuffer.Text = TeamWorkBuffer.BufferText;
                        buffer.Text = "Вставить из буфера:";
                    }
                }
                else if (_bufferWorkDivision > 0)
                {
                    // Оставляем существующую логику для обратной совместимости
                    // Текст будет установлен в основном методе загрузки
                    buffer.Text = "Вставить из буфера:";
                }
                else
                {
                    textBoxBuffer.Text = "Буфер пуст";
                    buffer.Text = "Вставить из буфера:";
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при обновлении отображения буфера");
            }
        }

        /// <summary>
        /// Обработчик события изменения глобального буфера
        /// </summary>
        private void OnBufferChanged(object sender, BufferChangedEventArgs e)
        {
            try
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    UpdateBufferDisplay();
                }));
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при обработке изменения буфера");
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

        private void gridViewRasz_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gridViewRasz_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            //if (_mode == (int)Mode.Edit &&
            //    _currentAnnData?.dateUpdate != null &&
            //    _currentAnnData.dateUpdate != DateTime.MinValue)
            //{
            //    if (e.Value is int newSec && gridViewRasz.GetFocusedRow() is NormRasz row)
            //    {
            //        var original = _originalNormRaszList.FirstOrDefault(x => x.nrId == row.nrId);
            //        if (original != null && newSec > original.Sek)
            //        {
            //            e.Valid = false;
            //            e.ErrorText = $"Значение нельзя увеличивать. Было: {original.Sek}, стало: {newSec}";
            //        }
            //    }
            //}
        }

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

        /// <summary>
        /// Автоматически добавляет две стандартные строки в norm_kont для новых РТ
        /// </summary>
        private void AddStandardKontRows()
        {
            try
            {
                string choice1 = "Пронумеровать деталь";
                string choice2 = "Комплектация пачки";

                // Проверяем, что строки еще не добавлены
                bool hasChoice1 = _normKontList.Any(nk => nk.text == choice1);
                bool hasChoice2 = _normKontList.Any(nk => nk.text == choice2);

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
    

        /// <summary>
        /// Вставляет операции из глобального буфера в текущий список NormRasz.
        /// Используется для режима комплекта, чтобы автоматически
        /// объединить операции из нескольких разделений труда, выбранных
        /// пользователем. В отличие от <see cref="buffer_Click"/>, этот метод
        /// не запрашивает подтверждение пользователя и не очищает списки
        /// операций — предполагается, что список уже подготовлен и пуст.
        /// </summary>
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
            foreach (var id in bufferIdsToUse)
            {
                var raszList = await _artNormService.GetRelatedNormRasz(id);
                if (raszList == null) continue;
                int partMaxN = raszList.Select(x => x.N).DefaultIfEmpty(0).Max();
                int offset = currentMaxN; // 0 для первой группы, далее — накопленный максимум
                foreach (var item in raszList)
                {
                    // Сбрасываем ID операции, чтобы база присвоила новый ID
                    item.nrID = 0;
                    
                    // Привязываем операцию к текущему разделению труда (не копируем annId родительской записи)
                    item.annId = _currentAnnData?.AnnID ?? 0;
                    
                    // Сбрасываем автоматически заполняемые поля, чтобы SQL сам их вставил
                    item.nrDateAdd = null;
                    item.nrCompAdd = null;
                    
                    item.IsNew = true;
                    // Сдвигаем только N на offset, N1 сохраняется
                    item.N = item.N + offset;
                    _normRaszList.Add(item);
                }
                currentMaxN += partMaxN; // обновляем максимум для следующей группы
            }
            // После добавления сортируем таблицу, чтобы новые элементы встали
            // в порядке возрастания N
            TWGridHelper.sortGridView(gridViewRasz);
            // Обновляем биндинги, чтобы изменения отобразились в UI
            _normRaszBindingSource?.ResetBindings(false);
            await ShowStatusMessage("Операции из буфера добавлены для комплекта");
        }
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

                // Меняем местами текущую операцию с предыдущей
                var previousOperation = allOperations[currentIndex - 1];
                SwapOperationPositions(selectedOperation, previousOperation);

                // Пересчитываем нумерацию
                RecalculateAllOperationNumbers();

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

                // Меняем местами текущую операцию со следующей
                var nextOperation = allOperations[currentIndex + 1];
                SwapOperationPositions(selectedOperation, nextOperation);

                // Пересчитываем нумерацию
                RecalculateAllOperationNumbers();

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
                int rowIndex = _normRaszList.IndexOf(operation);
                if (rowIndex >= 0)
                {
                    int rowHandle = gridViewRasz.GetRowHandle(rowIndex);
                    if (gridViewRasz.IsValidRowHandle(rowHandle))
                    {
                        gridViewRasz.FocusedRowHandle = rowHandle;
                        gridViewRasz.MakeRowVisible(rowHandle);
                    }
                }
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
                if (focusOperation != null)
                {
                    SetFocusToOperation(focusOperation);
                }
                gridViewRasz.RefreshData();
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
                    
                    var result = MessageBox.Show(
                        $"Обнаружены дублирующиеся номера операций: {duplicatesList}\n\n" +
                        "Выполнить автоматический пересчет нумерации?",
                        "Проблема с нумерацией",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        RecalculateAllOperationNumbers();
                        MessageBox.Show("Нумерация операций исправлена.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
            var result = MessageBox.Show(
                "Выполнить полный пересчет нумерации всех операций?\n\n" +
                "Это действие изменит номера всех операций в порядке их текущего расположения.",
                "Подтверждение пересчета",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                RecalculateAllOperationNumbers();
                MessageBox.Show("Нумерация операций обновлена.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnValidateNumbers_Click(object sender, EventArgs e)
        {
            ValidateAndFixOperationNumbers();
        }

        #endregion

    } }

