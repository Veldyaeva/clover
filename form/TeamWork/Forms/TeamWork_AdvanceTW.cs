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

namespace SewingProduction.form
{
    public partial class TeamWork_AdvanceTW : CustomForm
    {
        private readonly DbService _dbService;
        private readonly ArtNormService _artNormService;
        private int _bufferWorkDivision;
        private readonly DatabaseHelper _dbHelper;
        private readonly GridHelper _gridHelper = new GridHelper();
        private int _newAnnId = -1;
        private int _selectedAnnId = -1;
        private readonly ILogger _logger = new FileLogger();
        private int _mode;
        private ArtNormN _currentAnnData;
        private ArtNormN _originalAnnData; 

        private BindingList<NormRasz> _normRaszList;
        private BindingSource _normRaszBindingSource;
        private BindingList<NormRask> _normRaskList;
        private BindingSource _normRaskBindingSource;
        private BindingList<NormKont> _normKontList;
        private BindingSource _normKontBindingSource;
        private BindingList<NormDopObr> _normDopObrList;
        private BindingSource _normDopObrBindingSource;
        private static List<FioModel> _cachedFioData;
        private bool _isCustomEditFormOpen = false;
        private bool _okPressed = false;
        public bool IsRaszInserted { get; private set; }
        public bool IsRaskInserted { get; private set; }
        public bool IsKontInserted { get; private set; }
        public bool IsDopObrInserted { get; private set; }

        public ArtNormN CreatedAnn { get; private set; }

        private bool _isSelectionFormOpen = false;
        private bool _hasUnsavedChanges = false;

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
            if (control == groupTextBox) return nameof(ArtNormN.Group);
            if (control == modelTextBox) return nameof(ArtNormN.Mod);
            if (control == secTimeTextBox) return nameof(ArtNormN.Sek);
            if (control == textBoxKomment) return nameof(ArtNormN.Komment);
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
                var normRaszTask = Task.Run(() =>
                {
                    _normRaszList = new BindingList<NormRasz>();
                    _normRaszBindingSource = new BindingSource { DataSource = _normRaszList };
                });
                var normRaskTask = Task.Run(() =>
                {
                    _normRaskList = new BindingList<NormRask>();
                    _normRaskBindingSource = new BindingSource { DataSource = _normRaskList };
                });
                var normKontTask = Task.Run(() =>
                {
                    _normKontList = new BindingList<NormKont>();
                    _normKontBindingSource = new BindingSource { DataSource = _normKontList };
                });
                var normDopObrTask = Task.Run(() =>
                {
                    _normDopObrList = new BindingList<NormDopObr>();
                    _normDopObrBindingSource = new BindingSource { DataSource = _normDopObrList };
                });

                await Task.WhenAll(normRaszTask, normRaskTask, normKontTask, normDopObrTask);

                gridControlRasz.DataSource = _normRaszBindingSource;
                gridControlRaskr.DataSource = _normRaskBindingSource;
                gridControlKont.DataSource = _normKontBindingSource;
                gridControlDopObr.DataSource = _normDopObrBindingSource;
                _currentAnnData = new ArtNormN();
                bindingSource1.DataSource = _currentAnnData;

                nameTextBox.DataBindings.Add("Text", bindingSource1, nameof(ArtNormN.Articul), true, DataSourceUpdateMode.OnPropertyChanged);
                groupTextBox.DataBindings.Add("Text", bindingSource1, nameof(ArtNormN.Group), true, DataSourceUpdateMode.OnPropertyChanged);
                modelTextBox.DataBindings.Add("Text", bindingSource1, nameof(ArtNormN.Mod), true, DataSourceUpdateMode.OnPropertyChanged);
                secTimeTextBox.DataBindings.Add("Text", bindingSource1, nameof(ArtNormN.Sek), true, DataSourceUpdateMode.OnPropertyChanged);
                textBoxKomment.DataBindings.Add("Text", bindingSource1, nameof(ArtNormN.Komment), true, DataSourceUpdateMode.OnPropertyChanged);
                dateCreate.DataBindings.Add("Value", bindingSource1, nameof(ArtNormN.dateCreate), true, DataSourceUpdateMode.OnPropertyChanged);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
            finally
            {
                _normRaszList.ListChanged += OnDataChanged;
                _normRaskList.ListChanged += OnDataChanged;
                _normKontList.ListChanged += OnDataChanged;
                _normDopObrList.ListChanged += OnDataChanged;
            }
        }

        private async void TeamWork_AdvanceTW_Load(object sender, EventArgs e)
        {
            try
            {
                Task gridTask = Task.Run(() =>
                {
                    _gridHelper.LoadGridViewSettings(gridViewRaskr, "AdvanceTW_gridViewRaskrLayout.xml");
                    _gridHelper.LoadGridViewSettings(gridViewKont, "AdvanceTW_gridViewKontLayout.xml");
                    _gridHelper.LoadGridViewSettings(gridViewDopObr, "AdvanceTW_gridViewDopObrLayout.xml");
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
                            textBoxBuffer.Text = $"группа: {annData.Group.TrimEnd(' ')}, \r" +
                                                 $"модель: {annData.Mod.TrimEnd(' ')}, \r" +
                                                 $"артикул: {annData.Articul.TrimEnd(' ')}";
                        }));
                    }
                }

                switch (_mode)
                {
                    case (int)Mode.NewWorkDivision: this.Text = "Добавить предварительное"; nameTextBox.Enabled = true; break;
                    case (int)Mode.ArchAndCopy: this.Text = "Архив+копия"; break;
                    case (int)Mode.Edit: this.Text = "Редактировать"; break;
                }
                _hasUnsavedChanges = false;

                AttachChangeHandlers();

                designerComboBox.DataBindings.Clear(); 
                constructorComboBox.DataBindings.Clear();

                designerComboBox.DataBindings.Add("EditValue", bindingSource1, nameof(ArtNormN.Diz), true, DataSourceUpdateMode.OnPropertyChanged);
                constructorComboBox.DataBindings.Add("EditValue", bindingSource1, nameof(ArtNormN.Constr), true, DataSourceUpdateMode.OnPropertyChanged);

                // Подписываем таблицы на обработчик RowStyle
                gridViewRasz.RowStyle += GridView_RowStyle;
                gridViewRaskr.RowStyle += GridView_RowStyle;
                gridViewKont.RowStyle += GridView_RowStyle;
                gridViewDopObr.RowStyle += GridView_RowStyle;
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
                var dopObrTask = _artNormService.GetRelatedNormDopObr(id);
                var annDataTask = _artNormService.GetArtNormDataById(id);

                await Task.WhenAll(raszTask, raskTask, kontTask, dopObrTask, annDataTask);

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

                    // Обновляем списки
                    _normRaszList.Clear();
                    _normRaskList.Clear();
                    _normKontList.Clear();
                    _normDopObrList.Clear();

                    bool isCopyOrBuffer = _mode == (int)Mode.ArchAndCopy || caller == "buffer";

                    LoadList(raszTask.Result, _normRaszList, nameof(NormRasz.nrId), isCopyOrBuffer);
                    LoadList(raskTask.Result, _normRaskList, nameof(NormRask.id), isCopyOrBuffer);
                    LoadList(kontTask.Result, _normKontList, nameof(NormKont.nkId), isCopyOrBuffer);
                    LoadList(dopObrTask.Result, _normDopObrList, nameof(NormDopObr.doId), isCopyOrBuffer);

                    // Обновляем привязки
                    _normRaszBindingSource.ResetBindings(false);
                    _normRaskBindingSource.ResetBindings(false);
                    _normKontBindingSource.ResetBindings(false);
                    _normDopObrBindingSource.ResetBindings(false);

                    // Обновляем данные ANN через биндинг
                    var annData = annDataTask.Result;
                    if (annData != null)
                    {
                        _currentAnnData = annData;
                        bindingSource1.DataSource = _currentAnnData;
                        bindingSource1.ResetBindings(false);
                    }
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
                        bindingSource1.DataSource = _currentAnnData; // Привязываем данные к форме
                        bindingSource1.ResetBindings(false);
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
                        _gridHelper.SaveGridViewSettings(gridViewDopObr, "AdvanceTW_gridViewDopObrLayout.xml");
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
            if (gridView == null || !gridView.IsNewItemRow(e.RowHandle))
                return;

            if (_isCustomEditFormOpen)
            {
                // Если флаг установлен, значит мы специально открыли форму
                _isCustomEditFormOpen = false;
                return;
            }
            if (gridView.GetRowCellValue(e.RowHandle, "Kod") != null)//решаем, показывать ли editForm  
            {
                e.Allow = true;
                return;
            }
            e.Allow = false;

            using (var selectionForm = new NormOperNew(_selectedAnnId))
            {
                var result = selectionForm.ShowDialog();

                if (result == DialogResult.OK && selectionForm.SelectedRowData != null)
                {
                    var selected = selectionForm.SelectedRowData;

                    selected.IsNew = true;
                    _normRaszList.Add(selected);

                    // Обновляем привязку данных и интерфейс
                    _normRaszBindingSource.ResetBindings(false);
                    gridControlRasz.RefreshDataSource();
                    gridViewRasz.RefreshData();
                    gridView.PostEditor();
                    gridView.UpdateCurrentRow();

                    int indexInList = _normRaszList.IndexOf(selected);

                    if (indexInList >= 0)
                    {
                        int rowHandle = gridView.GetRowHandle(indexInList);

                        if (rowHandle >= 0)
                        {
                            gridView.FocusedRowHandle = rowHandle;
                        }
                        FinalizeRow(rowHandle, gridView);
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    // Если пользователь отменил выбор, удаляем строку
                    gridView.CancelUpdateCurrentRow();
                    gridView.HideEditForm();
                    gridView.CloseEditForm();
                }
                else
                {
                    e.Allow = false;
                    gridView.CancelUpdateCurrentRow();
                    gridView.DeleteRow(e.RowHandle);
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
            if (view != null)
            {
                view.HideEditForm();
            }
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

            if (e.Result != EditFormResult.Update)
            {
                try
                {
                    // Получаем текущую строку
                    var row = gridView.GetRow(e.RowHandle) as NormRasz;
                    if (row != null && row.nrId > 0)
                    {
                        // Удаляем из базы данных
                        await _dbService.DeleteEntityAsync(TableNames.Rasz, TableNames.RaszId, row);//.DeleteNormRaszAsync(row.nrId);
                        
                        // Удаляем из списка
                        _normRaszList.Remove(row);
                        _normRaszBindingSource.ResetBindings(false);
                        
                        // Удаляем из грида
                        gridView.DeleteRow(e.RowHandle);
                    }
                }
                catch (Exception ex)
                {
                    await _logger.LogErrorAsync(ex, "Ошибка при удалении строки после закрытия формы редактирования");
                    MessageBox.Show($"Ошибка при удалении строки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

        #region DopObr
        private void gridViewDopObr_ShowingEditor(object sender, CancelEventArgs e)
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

                    if (_normDopObrList == null || _normDopObrList.Count == 0)
                    {
                        var newDopObr = new NormDopObr
                        {
                            AnnId = _newAnnId,
                            IsNew = true 
                        };
                        _normDopObrList.Add(newDopObr);
                        _normDopObrBindingSource.ResetBindings(false);
                        MessageBox.Show("Добавлена строка дополнительной обработки.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("В таблице дополнительной обработки может быть только одна строка.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                finally
                {
                    _isSelectionFormOpen = false;
                }
            }
            else
            {
                e.Cancel = false;
            }
        }

        private async void gridViewDopObr_KeyDown(object sender, KeyEventArgs e)
        {
             GridView view = sender as GridView;
             if (view == null) return;

             if (e.KeyCode == Keys.Delete && view.FocusedRowHandle >= 0)
             {
                 if (MessageBox.Show("Удалить строку дополнительной обработки?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                 {
                     var rowToDelete = view.GetRow(view.FocusedRowHandle) as NormDopObr;
                     if (rowToDelete != null)
                     {
                         try
                         {
                             if (!rowToDelete.IsNew && rowToDelete.doId > 0)
                             {
                                 await _dbService.DeleteEntityAsync(TableNames.Obr, TableNames.ObrId, rowToDelete);
                             }
                             _normDopObrList.Remove(rowToDelete);
                             _normDopObrBindingSource.ResetBindings(false);
                             view.RefreshData(); 
                             _hasUnsavedChanges = true;
                         }
                         catch(Exception ex)
                         {
                              await _logger.LogErrorAsync(ex, "Ошибка при удалении строки NormDopObr");
                              MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         }
                     }
                 }
                 e.Handled = true; 
             }
        }

        private async void gridViewDopObr_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (e.Row is NormDopObr normDopObr)
            {
                try
                {
                    normDopObr.AnnId = _newAnnId;
                    e.Valid = true;
                }
                catch (Exception ex)
                {
                    e.Valid = false;
                    e.ErrorText = $"Ошибка: {ex.Message}";
                    await _logger.LogErrorAsync(ex, "Ошибка при валидации строки NormDopObr");
                }
            }
        }

        private void gridViewDopObr_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (e.Row is NormDopObr normDopObr)
            {
                normDopObr.AnnId = _newAnnId;
                if (!normDopObr.IsNew)
                {
                    normDopObr.IsModified = true;
                }
                gridViewDopObr.UpdateCurrentRow(); 
            }
        }

        #endregion

        private async void btnOK_Click(object sender, EventArgs e)
        {
            _okPressed = true;
            await ShowStatusMessage("Сохранение данных...");
            var stepStopwatch = Stopwatch.StartNew(); // Таймер для шагов

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

            //    await _logger.LogEventAsync("Starting ExecutePztOperUpdateAsync...", "btnOK_Click");
            //    stepStopwatch.Restart(); // Замеряем ExecutePztOperUpdateAsync

            ////    await _artNormService.ExecutePztOperUpdateAsync();
            //    stepStopwatch.Stop();
            //    await _logger.LogEventAsync($"Finished ExecutePztOperUpdateAsync. Elapsed: {stepStopwatch.ElapsedMilliseconds} ms", "btnOK_Click");
            //    await ShowStatusMessage("Плановые загрузки обновлены!");
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при сохранении данных");
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
                await SaveListAsync(_normRaszList, TableNames.Rasz, TableNames.RaszId, _newAnnId);
                await SaveListAsync(_normRaskList, TableNames.Rask, TableNames.RaskId, _newAnnId);
                await SaveListAsync(_normKontList, TableNames.Kont, TableNames.KontId, _newAnnId);
                await SaveListAsync(_normDopObrList, TableNames.Obr, TableNames.ObrId, _newAnnId);

                // Обновляем UI после сохранения
                await this.InvokeAsync(() =>
                {
                    gridControlRasz.RefreshDataSource();
                    gridControlRaskr.RefreshDataSource();
                    gridControlKont.RefreshDataSource();
                    gridControlDopObr.RefreshDataSource();
                 });
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при сохранении данных");
                throw;
            }
        }

        private async Task SaveListAsync<T>(BindingList<T> list, string tableName, string keyFieldName, int newAnnId)
            where T : class, INewable, new()
        {
            var stopwatch = Stopwatch.StartNew(); // Запускаем таймер для всего метода
            var bulkStopwatch = new Stopwatch(); // Таймер для конкретных Bulk операций

            var newItems = list.Where(x => x.IsNew).ToList();
            var existingItems = list.Where(x => !x.IsNew).ToList();

            string itemTypeName = typeof(T).Name;
            await _logger.LogEventAsync($"[{itemTypeName}] Start saving. New: {newItems.Count}, Existing: {existingItems.Count}\", \"SaveListAsync");

            if (newItems.Any())
            {
                // Обновляем AnnId для новых записей
                foreach (var item in newItems)
                {
                    var annIdProp = typeof(T).GetProperty("AnnId");
                    if (annIdProp != null)
                    {
                        annIdProp.SetValue(item, newAnnId);
                    }
                }

                // Пакетная вставка через Dapper Plus
                using (var connection = _dbHelper.GetConnection()) 
                {
                    await _logger.LogEventAsync($"[{itemTypeName}] Starting BulkInsert for {newItems.Count} items...\", \"SaveListAsync");
                    bulkStopwatch.Restart();
                    await connection.BulkInsertAsync(newItems);
                    bulkStopwatch.Stop();
                    await _logger.LogEventAsync($"[{itemTypeName}] Finished BulkInsert. Elapsed: {bulkStopwatch.ElapsedMilliseconds} ms\", \"SaveListAsync");
                }
                // После вставки сбрасываем флаги
                foreach (var item in newItems)
                {
                    item.IsNew = false;
                }
            }

            // Обновляем существующие записи через BulkUpdate
            if (existingItems.Any())
            {
                using (var connection = _dbHelper.GetConnection()) 
                {
                     await _logger.LogEventAsync($"[{itemTypeName}] Starting BulkUpdate for {existingItems.Count} items...\", \"SaveListAsync");
                    bulkStopwatch.Restart();
                    await connection.BulkUpdateAsync(existingItems);
                     bulkStopwatch.Stop();
                    await _logger.LogEventAsync($"[{itemTypeName}] Finished BulkUpdate. Elapsed: {bulkStopwatch.ElapsedMilliseconds} ms\", \"SaveListAsync");
                }
            }

            stopwatch.Stop();
            await _logger.LogEventAsync($"[{itemTypeName}] Finished saving. Total Elapsed: {stopwatch.ElapsedMilliseconds} ms\", \"SaveListAsync");
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
                        await _logger.LogEventAsync($"Выбран конструктор с ID {selectedId}", "ComboBox_SelectedIndexChanged");
                    }
                    else if (lookUpEdit == designerComboBox)
                    {
                        fieldName = "diz";
                        await _logger.LogEventAsync($"Выбран дизайнер с ID {selectedId}", "ComboBox_SelectedIndexChanged");
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
                        if (_normRaszList == null || _normRaskList == null || _normKontList == null || _normDopObrList == null)
                        {
                            await InitializeBindingsAsync();
                        }

                        // Очищаем текущие списки
                        _normRaszList?.Clear();
                        _normRaskList?.Clear();
                        _normKontList?.Clear();
                        _normDopObrList?.Clear();

                        _normRaszBindingSource?.ResetBindings(false);
                        _normRaskBindingSource?.ResetBindings(false);
                        _normKontBindingSource?.ResetBindings(false);
                        _normDopObrBindingSource?.ResetBindings(false);
                    }

                    // Загружаем данные из буфера
                    await WorkDivisionLoadAsync(caller: "buffer", _bufferWorkDivision);
                    await LoadAnnDataAsync();
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

            // Проверка поля Модель или Группа
            if (string.IsNullOrWhiteSpace(groupTextBox.Text) && string.IsNullOrWhiteSpace(modelTextBox.Text))
            {
                errorProvider1.SetError(groupTextBox, "Заполните либо 'Модель', либо 'Группу'.");
                errorProvider1.SetError(modelTextBox, "Заполните либо 'Модель', либо 'Группу'.");
                if (isValid)
                    statusLabel.Text = "Ошибка: Заполните либо 'Модель', либо 'Группу'.";
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
            if (e.ListChangedType != ListChangedType.Reset || _normRaszList.Count > 0 || _normRaskList.Count > 0 || _normKontList.Count > 0 || _normDopObrList.Count > 0)
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
            this.Close();
        }

        private void customGroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void customLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
