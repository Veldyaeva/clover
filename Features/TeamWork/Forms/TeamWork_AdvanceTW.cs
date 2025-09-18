using Dapper;
using DevExpress.XtraBars.Customization;
using DevExpress.XtraDiagram.Bars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using NLog.Filters;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
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

                _normRaszList.ListChanged -= OnDataChanged;
                _normRaskList.ListChanged -= OnDataChanged;
                _normKontList.ListChanged -= OnDataChanged;
                _normRaszList.ListChanged += OnDataChanged;
                _normRaskList.ListChanged += OnDataChanged;
                _normKontList.ListChanged += OnDataChanged;
                //_normRaszList.ListChanged -= (_, __) => _sekDebouncer.Debounce(10, async () => { if (_newAnnId > 0) RecalculateSek(); });
                //_normRaszList.ListChanged += (_, __) => _sekDebouncer.Debounce(10, async () => { if (_newAnnId > 0) RecalculateSek(); });
                //    _normRaskList.ListChanged += (_, __) => _sekDebouncer.Debounce(500, async () => { if (_newAnnId > 0) RecalculateSek(); }); это другие какие-то секунды
                //    _normKontList.ListChanged += (_, __) => _sekDebouncer.Debounce(500, async () => { if (_newAnnId > 0) RecalculateSek(); });
                bool allowDelete = _currentAnnData?.dateUpdate == null || _currentAnnData.dateUpdate == DateTime.MinValue;
                if (allowDelete)//(_mode == (int)Mode.ArchAndCopy || _mode == (int)Mode.NewWorkDivision || _mode ==(int)Mode.Clone)
                {
                    AttachDeleteContextMenuForRasz(gridViewRasz, _normRaszList, r => r.nrID, _deletedNormRaszIds);
                   // AttachDeleteContextMenu(gridViewKont, _normKontList, k => k.nkId, _deletedNormKontIds);
                    AttachDeleteContextMenu(gridViewKont, _normKontList, k => k.nkId, _deletedNormKontIds);
                }
                else
                {
                    gridViewRasz.PopupMenuShowing -= ShowPopUpForRasz(gridViewRasz, _normRaszList, r => r.nrID, _deletedNormRaszIds);
                    gridViewKont.PopupMenuShowing -= ShowPopUp(gridViewKont, _normKontList, k => k.nkId, _deletedNormKontIds);
                }

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
        private  PopupMenuShowingEventHandler ShowPopUpForRasz(
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

                    // Удаляем строку
                    bindingList.Remove(rowObj);

                    // Перенумеровываем операции с номерами больше удаленной
                    RenumberOperationsAfterDeletion(bindingList, deletedOperationN, deletedOperationN1);

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

                        // Обновляем отображение после перенумерации
                        view.RefreshData();
                    }));
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
                        
                        // Выполняем перенумерацию в зависимости от типа операции
                        if (isSuboperation)
                        {
                            if (choice.ConvertMainToSuboperation)
                            {
                                // Преобразуем основную операцию в подоперацию и добавляем новую подоперацию
                                ConvertMainOperationToSuboperation(insertOperationN);
                            }
                            else
                            {
                                // Для обычной подоперации - перенумеровываем только подоперации в рамках той же основной операции
                                RenumberSuboperationsForInsertion(insertOperationN, insertOperationN1);
                            }
                        }
                        else
                        {
                            // Для основной операции - перенумеровываем все операции начиная с указанной позиции
                            int maxN = _normRaszList?.Select(x => x.N).DefaultIfEmpty(0).Max() ?? 0;
                            if (insertOperationN <= maxN)
                            {
                                RenumberOperationsForInsertion(insertOperationN);
                            }
                        }
                        
                        // Присваиваем новой операции нужные номера
                        selectedData.N = insertOperationN;
                        selectedData.N1 = insertOperationN1;
                        
                        _normRaszList.Add(selectedData);
                        _normRaszBindingSource.ResetBindings(false);
                        gridControlRasz.RefreshDataSource();
                        
                        // Обновляем сортировку после добавления
                        TWGridHelper.sortGridView(gridViewRasz);
                        
                        // Автоматически открываем форму редактирования для новой операции
                        int newRowDataSourceIndex = _normRaszList.IndexOf(selectedData);
                        if (newRowDataSourceIndex >= 0)
                        {
                            int newRowHandle = gridViewRasz.GetRowHandle(newRowDataSourceIndex);
                            if (gridViewRasz.IsValidRowHandle(newRowHandle))
                            {
                                // Устанавливаем фокус и открываем форму редактирования
                                gridViewRasz.FocusedRowHandle = newRowHandle;
                                gridViewRasz.MakeRowVisible(newRowHandle);
                                
                                // Открываем форму редактирования через небольшую задержку
                                gridViewRasz.GridControl.BeginInvoke(new Action(() =>
                                {
                                    _isCustomEditFormOpen = true;
                                    gridViewRasz.ShowEditForm();
                                }));
                            }
                        }
                        
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



        /// <summary>
        /// Преобразует основную операцию в подоперацию при добавлении первой подоперации
        /// </summary>
        /// <param name="operationN">Номер основной операции для преобразования</param>
        private void ConvertMainOperationToSuboperation(int operationN)
        {
            // Находим основную операцию (N.0) и преобразуем её в подоперацию (N.1)
            var mainOperation = _normRaszList?.FirstOrDefault(r => r.N == operationN && r.N1 == 0);
            
            if (mainOperation != null)
            {
                int oldN1 = mainOperation.N1;
                mainOperation.N1 = 1; // Основная операция становится первой подоперацией
                
                // Помечаем как измененную, если это не новая запись
                if (!mainOperation.IsNew)
                {
                    mainOperation.IsModified = true;
                }
                
                _logger.LogEventAsync($"Основная операция преобразована: {operationN}.{oldN1} -> {operationN}.{mainOperation.N1}", "ConvertMainOperationToSuboperation");
            }
        }

        /// <summary>
        /// Перенумеровывает подоперации для освобождения места под новую подоперацию
        /// </summary>
        /// <param name="operationN">Номер основной операции</param>
        /// <param name="insertSuboperationN1">Номер подоперации, начиная с которого нужно сдвинуть нумерацию</param>
        private void RenumberSuboperationsForInsertion(int operationN, int insertSuboperationN1)
        {
            // Находим все подоперации с тем же N и N1 >= insertSuboperationN1 и увеличиваем их N1 на 1
            var suboperationsToRenumber = _normRaszList
                .Where(r => r.N == operationN && r.N1 >= insertSuboperationN1)
                .OrderByDescending(r => r.N1) // Обрабатываем в обратном порядке, чтобы избежать конфликтов
                .ToList();

            if (suboperationsToRenumber.Any())
            {
                _logger.LogEventAsync($"Перенумерация подопераций: сдвиг {suboperationsToRenumber.Count} подопераций операции №{operationN} начиная с {insertSuboperationN1}", "RenumberSuboperationsForInsertion");
                
                foreach (var operation in suboperationsToRenumber)
                {
                    int oldN1 = operation.N1;
                    operation.N1 += 1;
                    
                    // Помечаем как измененную, если это не новая запись
                    if (!operation.IsNew)
                    {
                        operation.IsModified = true;
                    }
                    
                    _logger.LogEventAsync($"Подоперация перенумерована: {operation.N}.{oldN1} -> {operation.N}.{operation.N1}", "RenumberSuboperationsForInsertion");
                }
            }
        }

        /// <summary>
        /// Перенумеровывает операции для освобождения места под новую операцию
        /// </summary>
        /// <param name="insertOperationN">Номер операции, начиная с которого нужно сдвинуть нумерацию</param>
        private void RenumberOperationsForInsertion(int insertOperationN)
        {
            // Находим все операции с номерами >= insertOperationN и увеличиваем их номера на 1
            var operationsToRenumber = _normRaszList
                .Where(r => r.N >= insertOperationN)
                .OrderByDescending(r => r.N) // Обрабатываем в обратном порядке, чтобы избежать конфликтов
                .ToList();

            if (operationsToRenumber.Any())
            {
                _logger.LogEventAsync($"Перенумерация операций: сдвиг {operationsToRenumber.Count} операций начиная с номера {insertOperationN}", "RenumberOperationsForInsertion");
                
                foreach (var operation in operationsToRenumber)
                {
                    int oldN = operation.N;
                    operation.N += 1;
                    
                    // Помечаем как измененную, если это не новая запись
                    if (!operation.IsNew)
                    {
                        operation.IsModified = true;
                    }
                    
                    _logger.LogEventAsync($"Операция перенумерована: {oldN}.{operation.N1} -> {operation.N}.{operation.N1}", "RenumberOperationsForInsertion");
                }
            }
        }

        /// <summary>
        /// Перенумеровывает операции после удаления строки
        /// </summary>
        /// <param name="normRaszList">Список операций</param>
        /// <param name="deletedOperationN">Номер удаленной операции</param>
        /// <param name="deletedOperationN1">Номер удаленной подоперации</param>
        private static void RenumberOperationsAfterDeletion(BindingList<NormRasz> normRaszList, int deletedOperationN, int deletedOperationN1)
        {
            if (deletedOperationN1 == 0)
            {
                // Удаляется основная операция (например, 3.0)
                // Нужно перенумеровать все операции с N > deletedOperationN
                var operationsToRenumber = normRaszList
                    .Where(r => r.N > deletedOperationN)
                    .ToList();

                foreach (var operation in operationsToRenumber)
                {
                    operation.N -= 1;
                    // Помечаем как измененную, если это не новая запись
                    if (!operation.IsNew)
                    {
                        operation.IsModified = true;
                    }
                }
            }
            else
            {
                // Удаляется подоперация (например, 2.3)
                // Нужно перенумеровать только подоперации с тем же N и N1 > deletedOperationN1
                var suboperationsToRenumber = normRaszList
                    .Where(r => r.N == deletedOperationN && r.N1 > deletedOperationN1)
                    .ToList();

                foreach (var operation in suboperationsToRenumber)
                {
                    operation.N1 -= 1;
                    // Помечаем как измененную, если это не новая запись
                    if (!operation.IsNew)
                    {
                        operation.IsModified = true;
                    }
                }

                // Проверяем, осталась ли только одна подоперация с данным номером
                var remainingSuboperationsWithSameN = normRaszList
                    .Where(r => r.N == deletedOperationN && r.N1 > 0)
                    .ToList();

                if (remainingSuboperationsWithSameN.Count == 1)
                {
                    // Если осталась только одна подоперация, делаем её основной (N1 = 0)
                    var lastSuboperation = remainingSuboperationsWithSameN.First();
                    lastSuboperation.N1 = 0;
                    // Помечаем как измененную, если это не новая запись
                    if (!lastSuboperation.IsNew)
                    {
                        lastSuboperation.IsModified = true;
                    }
                }
                else if (remainingSuboperationsWithSameN.Count == 0)
                {
                    // Если не осталось ни одной подоперации, проверяем есть ли основная операция
                    var mainOperation = normRaszList.FirstOrDefault(r => r.N == deletedOperationN && r.N1 == 0);
                    // Основная операция остается без изменений (это нормально)
                }
            }
        }

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
                }
                await LoadAnnDataAsync();
                _hasUnsavedChanges = false;

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

                // Подписываем таблицы на обработчик RowStyle
                gridViewRasz.RowStyle += GridView_RowStyle;
                gridViewRaskr.RowStyle += GridView_RowStyle;
                gridViewKont.RowStyle += GridView_RowStyle;
                
                // Подписываемся на изменение фокуса для сохранения последней выбранной операции
                gridViewRasz.FocusedRowChanged += GridViewRasz_FocusedRowChanged;

                if (_currentAnnData != null && _sourceAnnIdToCopyDetailsFrom.HasValue && _duplicateAnnData != null && _mode == (int)Mode.NewWorkDivision)
                {
                    // Копирование данных шапки из _duplicateAnnData в _currentAnn (уже загруженный для newAnnId)
                    _currentAnnData = _duplicateAnnData.CloneProperties();
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

                    _normRaszBindingSource.ResetBindings(false);
                    _normRaskBindingSource.ResetBindings(false);
                    _normKontBindingSource.ResetBindings(false);


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

                // Отписываемся от событий при выходе из формы
                this.FormClosed += (s, args) => TeamWorkBuffer.BufferChanged -= OnBufferChanged;
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
                var rasz = _normRaszList?.Where(r => r.N1 < 100 && (r.KodProizv == 1 || r.KodProizv == 3)).ToList() ?? new List<NormRasz>();

                int Sum(Func<NormRasz, bool> condition) => rasz.Where(condition).Sum(r => r.Sek);
                //      int Sum1(Func<NormRask, bool> condition)=>rask
                _currentAnnData.SekVyazo = Sum(r => r.KodOb == 28);
                _currentAnnData.SekVyaz5 = Sum(r => r.KodOb == 25);
                _currentAnnData.SekVyaz12 = Sum(r => r.KodOb == 35);
                _currentAnnData.SekVyaz7 = Sum(r => r.KodOb == 26);
                _currentAnnData.SekVyaz10 = Sum(r => r.KodOb == 37);
                _currentAnnData.SekVyaz6 = Sum(r => r.KodOb == 38);
                _currentAnnData.SekVyaz3 = Sum(r => r.KodOb == 29);
                _currentAnnData.SekShv = Sum(r => r.KodPodr != 1 && r.KodPodr != 6);
                _currentAnnData.SekKr = _normRaszList.Where(r => r.KodPodr == 7).Sum(r => r.Sek);
                _currentAnnData.Sek = _normRaszList.Where(r => r.N1 < 100).Sum(r => r.Sek);
                _currentAnnData.Seb = (int)_normRaszList.Where(r => r.N1 < 100).Sum(r => r.Seb); // sb
                _currentAnnData.SekVyaz14 = Sum(r => r.KodOb == 62);
                _currentAnnData.SekVyaz70 = Sum(r => r.KodOb == 55);
                _currentAnnData.SekVyaz71 = Sum(r => r.KodOb == 59);
                _currentAnnData.SekVyaz72 = Sum(r => r.KodOb == 73);
                _currentAnnData.SekVyaz62 = Sum(r => r.KodOb == 60);
                _currentAnnData.SekVyaz57 = Sum(r => r.KodOb == 114);
                _currentAnnData.SekVyaz18 = Sum(r => r.KodOb == 115);
                _currentAnnData.SekVyaz = Sum(r => r.KodPodr == 1 || r.KodPodr == 6);

                _currentAnnData.SekShv = _currentAnnData.SekVyaz != 0 ? Sum(r => r.KodPodr != 1 && r.KodPodr != 6) : _currentAnnData.Sek;

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
                gridViewRasz.UpdateCurrentRow(); // Обновляем строку, чтобы RowStyle сработал
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
                    if (row != null && row.nrID > 0 && !row.IsNew)
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
                        await _logger.LogEventAsync($"EditFormHidden for existing row (nrId: {row.nrID}) with Result: {e.Result}. Original delete logic is currently commented.", "gridViewRasz_EditFormHidden");
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
                await _dbHelper.ExecuteInTransactionAsync(async () =>
                {
                    await SaveAnnDataAsync();
                    await SaveAllDataAsync();
                });

                // Эти действия выполняются ПОСЛЕ успешной транзакции
                IsRaszInserted = true; // Предполагаем, что если сохранение дошло сюда, то все списки были обработаны
                IsRaskInserted = true;
                IsKontInserted = true;
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
            int bufferIdToUse = TeamWorkBuffer.HasData ? TeamWorkBuffer.BufferId : _bufferWorkDivision;

            if (bufferIdToUse > 0)
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
                        if (_normRaszList?.Any() == true)
                        {
                            string updateSql = @"
                                UPDATE norm_rasz 
                                SET nrDateDel = @DateDel, nrCompDel = @CompDel 
                                WHERE annId = @AnnId AND nrDateDel IS NULL";

                            await _dbHelper.ExecuteNonQueryAsync(updateSql, new Dictionary<string, object>
                            {
                                { "@DateDel", DateTime.Now },
                                { "@CompDel", Environment.MachineName },
                                { "@AnnId", _currentAnnData?.AnnID ?? 0 }
                            });
                        }

                        // Очищаем текущие списки
                        _normRaszList?.Clear();
                        _lastFocusedRaszOperation = null; // Сбрасываем последнюю операцию

                        _normRaszBindingSource?.ResetBindings(false);
                    }

                    int currentMaxN = _normRaszList?.Any() == true ? _normRaszList.Max(x => x.N) : 0;
                    
                    List<NormRasz> raszList = await _artNormService.GetRelatedNormRasz(bufferIdToUse);
                    if (raszList == null) raszList = new List<NormRasz>();

                    foreach (var item in raszList)
                    {
                        // Сбрасываем ID для получения нового от БД
                        item.nrID = 0;
                        // Привязываем к текущему разделению труда
                        item.annId = _currentAnnData?.AnnID ?? 0;
                        // Очищаем поля для автоматической вставки SQL
                        item.nrDateAdd = null;
                        item.nrCompAdd = null;
                        // Помечаем как новую запись
                        item.IsNew = true;
                        
                        currentMaxN++;
                        item.N = currentMaxN;
                        _normRaszList.Add(item);
                    }

                    TWGridHelper.sortGridView(gridViewRasz);
                    //if (gridControlRaskrTW.MainView is GridView raskrView) TWGridHelper.sortGridView(raskrView);
                    //if (gridControlKontTW.MainView is GridView kontView) TWGridHelper.sortGridView(kontView);
                
                    //var rasz = await _artNormService.GetRelatedNormRasz(_bufferWorkDivision);
                    //_normRaszList.Add(rasz);//.BulkLoad(CloneUtils.CloneList(rasz, _newAnnId, "nrId"));

                //_currentAnnData.dateCreate = DateTime.Now;



                // Показываем статус успешной загрузки
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
                    textBoxBuffer.Text = TeamWorkBuffer.BufferText;
                }
                else if (_bufferWorkDivision > 0)
                {
                    // Оставляем существующую логику для обратной совместимости
                    // Текст будет установлен в основном методе загрузки
                }
                else
                {
                    textBoxBuffer.Text = "Буфер пуст";
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
    }
}

