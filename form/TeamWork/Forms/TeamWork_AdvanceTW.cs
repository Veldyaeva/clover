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

namespace SewingProduction.form
{
    public partial class TeamWork_AdvanceTW : CustomForm
    {
        private readonly ArtNormService _artNormService;
        private int _bufferWorkDivision;
        private readonly DatabaseHelper _dbHelper;
        private readonly GridHelper _gridHelper = new GridHelper();
        private int _newAnnId = -1;
        private int _selectedAnnId = -1;
        private readonly ILogger _logger = new FileLogger();
        private int _mode;

        private BindingList<NormRasz> _normRaszList;
        private BindingSource _normRaszBindingSource;
        private BindingList<NormRask> _normRaskList;
        private BindingSource _normRaskBindingSource;
        private BindingList<NormKont> _normKontList;
        private BindingSource _normKontBindingSource;
        private BindingList<NormDopObr> _normDopObrList;
        private BindingSource _normDopObrBindingSource;
        // Кэш для данных дизайнеров/конструкторов, чтобы не загружать их повторно
        private static DataTable _cachedFioData;
        private bool _isCustomEditFormOpen = false;
        private bool _okPressed = false;
        public bool IsRaszInserted { get; private set; }
        public bool IsRaskInserted { get; private set; }
        public bool IsKontInserted { get; private set; }
        public bool IsDopObrInserted { get; private set; }

        public ArtNormN CreatedAnn { get; private set; }

        private bool _isSelectionFormOpen = false;

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

        /// <summary>
        /// Инициализирует привязки для связанных таблиц
        /// Выполняется в отдельном потоке.
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


            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
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

                await WorkDivisionLoadAsync(_selectedAnnId);
                await LoadAnnDataAsync();
                if (_bufferWorkDivision > 0)
                {
                    var annData = await _artNormService.GetArtNormDataById(_bufferWorkDivision);
                    if (annData != null)
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            richTextBox1.Text = $"группа: {annData.Group.TrimEnd(' ')}, \r" +
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
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке формы TeamWork_AdvanceTW");
            }
        }

        private async Task LoadAndBindFioListsAsync()
        {
            //try
            //{
            //    if (_cachedFioData == null)
            //    {
            //        var fioData = await _artNormService.GetRelDesigner();
            //        if (fioData != null && fioData.Rows.Count > 0)
            //        {
            //            _cachedFioData = fioData.Copy();
            //            await _logger.LogEventAsync("FIO загружено и закешировано", "LoadAndBindFioListsAsync");
        //        }
        //        else
        //        {
            //            await _logger.LogEventAsync("Пустой список FIO", "LoadAndBindFioListsAsync");
            //            return;
        //        }
        //    }

            //    // Обновляем данные в существующих источниках привязки
            //    await this.InvokeAsync(() =>
            //    {
            //        designerBindingSource.DataSource = _cachedFioData.Copy();
            //        constructorBindingSource.DataSource = _cachedFioData.Copy();
            //    });
            //}
            //catch (Exception ex)
            //{
            //    await _logger.LogErrorAsync(ex, "Ошибка при обновлении ComboBox из кеша");
            //}
        }

        /// <summary>
        /// Загрузка данных из буфера (NormRasz, NormRask и ANN) с параллельной обработкой.
        /// </summary>
        private async Task WorkDivisionLoadAsync(int id)
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

                DataTable kod_proizv = await _artNormService.GetKod_proizv();
                repositoryItemLookUpEdit1.DataSource = kod_proizv;
                DataTable podr_vyaz = await _artNormService.GetPodr_vyaz();
                repositoryItemLookUpEdit2.DataSource = podr_vyaz;
                DataTable oborud_shv = await _artNormService.GetOborud_shv();
                repositoryItemLookUpEdit3.DataSource = oborud_shv;

                // Преобразуем в списки
                var raszList = ConvertDataTable<NormRasz>(raszTask.Result);
                var raskList = ConvertDataTable<NormRask>(raskTask.Result);
                var kontList = ConvertDataTable<NormKont>(kontTask.Result);
                var dopObrList = ConvertDataTable<NormDopObr>(dopObrTask.Result);

                // Обновляем UI
                await  this.InvokeAsync(async() =>
                {
                    // Очищаем списки перед добавлением новых данных
                    _normRaszList.Clear();
                    _normRaskList.Clear();
                    _normKontList.Clear();
                    _normDopObrList.Clear();

                    // Добавляем новые данные
                    foreach (var item in raszList) _normRaszList.Add(item);
                    foreach (var item in raskList) _normRaskList.Add(item);
                    foreach (var item in kontList) _normKontList.Add(item);
                    foreach (var item in dopObrList) _normDopObrList.Add(item);

                    // Обновляем привязки
                    _normRaszBindingSource.ResetBindings(false);
                    _normRaskBindingSource.ResetBindings(false);
                    _normKontBindingSource.ResetBindings(false);
                    _normDopObrBindingSource.ResetBindings(false);

                    // var annData = await annDataTask;
                    var annData = annDataTask.Result;
                    // Обновляем основные поля формы
                    if (annData != null)
                    {
                        nameTextBox.Text = annData.Articul;
                        groupTextBox.Text = annData.Group;
                        modelTextBox.Text = annData.Mod;
                        secTimeTextBox.Text = annData.Sek.ToString();
                        if (annData.Diz > 0)
                        {
                            try { designerComboBox.SelectedValue = annData.Diz; }
                            catch { }
                        }
                        if (annData.Constr > 0)
                        {
                            try { constructorComboBox.SelectedValue = annData.Constr; }
                            catch { }
                        }

                        // Обновляем отображение данных
                        nameTextBox.Refresh();
                        groupTextBox.Refresh();
                        modelTextBox.Refresh();
                        secTimeTextBox.Refresh();
                        designerComboBox.Refresh();
                        constructorComboBox.Refresh();
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

        private static List<T> ConvertDataTable<T>(DataTable table) where T : new()
        {
            var list = new List<T>();

            foreach (DataRow row in table.Rows)
            {
                var item = new T();
                foreach (DataColumn col in table.Columns)
                {
                    var prop = typeof(T).GetProperty(col.ColumnName);
                    if (prop != null && row[col] != DBNull.Value)
                        prop.SetValue(item, Convert.ChangeType(row[col], prop.PropertyType));
                }
                list.Add(item);
            }

            return list;
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
                        nameTextBox.Text = annData.Articul;
                        groupTextBox.Text = annData.Group;
                        modelTextBox.Text = annData.Mod;
                        secTimeTextBox.Text = annData.Sek.ToString();
                        if (annData.Diz > 0)
                        {
                            try { designerComboBox.SelectedValue = annData.Diz; }
                            catch (Exception ex) 
                            { 
                                _logger.LogErrorAsync(ex, "Ошибка при установке значения дизайнера").Wait();
                            }
                        }
                        if (annData.Constr > 0)
                        {
                            try { constructorComboBox.SelectedValue = annData.Constr; }
                            catch (Exception ex) 
                            { 
                                _logger.LogErrorAsync(ex, "Ошибка при установке значения конструктора").Wait();
                            }
                        }
                        
                        // Обновляем отображение данных
                        nameTextBox.Refresh();
                        groupTextBox.Refresh();
                        modelTextBox.Refresh();
                        secTimeTextBox.Refresh();
                        designerComboBox.Refresh();
                        constructorComboBox.Refresh();
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
                try
                {
                    _normRaszList.Clear();
                    _normRaskList.Clear();
                    _normKontList.Clear();
                    _normDopObrList.Clear();
                    
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
                catch (Exception ex)
                {
                    await _logger.LogErrorAsync(ex, "Ошибка при закрытии формы без сохранения данных");
                }
            }
        }


        #region Rasz
        private void gridViewRasz_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            var gridView = sender as GridView;
            if (gridView == null)
                return;
            try
            {
                using (var selectionForm = new NormOperNew(_selectedAnnId))
                {
                    DialogResult result = selectionForm.ShowDialog();

                    if (result == DialogResult.OK)
                {
                    var selectedData = selectionForm.SelectedRowData;
                    if (selectedData != null)
                    {
                            // Заполняем значения в текущей новой строке
                            gridView.SetRowCellValue(e.RowHandle, "AnnId", _newAnnId);
                            gridView.SetRowCellValue(e.RowHandle, "KodO", selectedData.Kod_o);
                            gridView.SetRowCellValue(e.RowHandle, "Text", selectedData.Text);
                            gridView.SetRowCellValue(e.RowHandle, "Spec", selectedData.Spec);
                            gridView.SetRowCellValue(e.RowHandle, "Razryad", selectedData.Razryad);
                            gridView.SetRowCellValue(e.RowHandle, "Obor", selectedData.Obor);
                            gridView.SetRowCellValue(e.RowHandle, "Kod_proizv", selectedData.Kod_proizv);
                            gridView.SetRowCellValue(e.RowHandle, "Kod", selectedData.Kod);
                            gridView.SetRowCellValue(e.RowHandle, "N1", selectedData.N1);
                            gridView.SetRowCellValue(e.RowHandle, "Sek", selectedData.Sek);
                            gridView.SetRowCellValue(e.RowHandle, "Kod_podr", selectedData.Kod_podr);
                            gridView.SetRowCellValue(e.RowHandle, "Kod_ob", selectedData.Kod_ob);
                            gridView.SetRowCellValue(e.RowHandle, "TextVyaz", selectedData.TextVyaz);
                            gridView.SetRowCellValue(e.RowHandle, "TextOb", selectedData.TextOb);
                            gridView.SetRowCellValue(e.RowHandle, "TextProizv", selectedData.TextProizv);
                            gridView.SetRowCellValue(e.RowHandle, "nrId", selectedData.nrId);
                            
                            gridView.PostEditor();
                            gridView.UpdateCurrentRow();

                            // Фокусируемся на новой строке и открываем форму редактирования
                            gridView.FocusedRowHandle = e.RowHandle;
                            gridView.ShowEditForm();
                        }
                        else
                        {
                            gridView.CancelUpdateCurrentRow();
                            gridView.HideEditForm();
                            gridView.DeleteRow(e.RowHandle);
                        }
                    }
                    else
                    {
                        gridView.CancelUpdateCurrentRow();
                        gridView.HideEditor();
                        gridView.CloseEditForm();
                        gridView.DeleteRow(e.RowHandle);
                    }
                }
            }
            catch (Exception ex)
            {
                 _logger.LogErrorAsync(ex, "Ошибка при добавлении новой строки в gridViewRasz");
                MessageBox.Show($"Ошибка при добавлении новой строки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
            if (gridView.GetRowCellValue(e.RowHandle, "Kod") != null)//решаем, показывать ли editForm  (но почему код???)
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

                    _normRaszList.Add(selected);

                    // Обновляем привязку данных и интерфейс
                    _normRaszBindingSource.ResetBindings(false);
                    gridControlRasz.RefreshDataSource();
                    gridViewRasz.RefreshData();
                    gridView.PostEditor();
                    gridView.UpdateCurrentRow();
                    int newRowHandle = gridView.LocateByValue(TableNames.RaszId, selected.nrId);//_newAnnId);
                    
                    if (newRowHandle >= 0)
                    {

                        gridView.FocusedRowHandle = newRowHandle;
                        gridView.ClearSelection();
                    }

                    FinalizeRow(newRowHandle, gridView);

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
                gridViewRasz.UpdateCurrentRow();
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
                        await _artNormService.DeleteEntityAsync(TableNames.Rasz, TableNames.RaszId, row);//.DeleteNormRaszAsync(row.nrId);
                        
                        // Удаляем из списка
                        _normRaszList.Remove(row);
                        _normRaszBindingSource.ResetBindings(false);
                        
                        // Удаляем из грида
                        gridView.DeleteRow(e.RowHandle);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogErrorAsync(ex, "Ошибка при удалении строки после закрытия формы редактирования");
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
                                await _artNormService.DeleteEntityAsync(TableNames.Rask, TableNames.RaskId, oldRow);
                            }
                        }

                        // Очищаем список
                        _normRaskList.Clear();

                        // Вставляем новые данные
                        foreach (var normRask in selectionForm.SelectedData)
                        {
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
                gridViewRaskr.UpdateCurrentRow();
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
                        normRask.id = await _artNormService.InsertNormRaskAsync(normRask);
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
        private async void GridView3_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            var gridView = sender as GridView;
            if (gridView == null)
                return;

            try
            {
                // Проверяем существующие строки
                bool hasNumberingRow = false;
                bool hasPackingRow = false;

                for (int i = 0; i < gridView.DataRowCount; i++)
                {
                    var rowText = gridView.GetRowCellValue(i, "Text")?.ToString();
                    if (rowText == "Пронумеровать деталь")
                        hasNumberingRow = true;
                    else if (rowText == "Номер пачки")
                        hasPackingRow = true;
                }

                // Добавляем первую строку, если её нет
                if (!hasNumberingRow)
                {
                    await this.InvokeAsync(() =>
                    {
                        gridView.SetRowCellValue(e.RowHandle, "AnnId", _newAnnId);
                        gridView.SetRowCellValue(e.RowHandle, "Text", "Пронумеровать деталь");
                        gridView.UpdateCurrentRow();
                    });
                }

                // Добавляем вторую строку, если её нет
                if (!hasPackingRow)
                {
                    await this.InvokeAsync(() =>
                    {
                        // Добавляем новую строку напрямую в список данных
                        var normKont = new NormKont
                        {
                            AnnId = _newAnnId,
                            Text = "Номер пачки"
                        };
                        _normKontList.Add(normKont);
                        _normKontBindingSource.ResetBindings(false);
                    });
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при добавлении новой строки в GridView3");
                await this.InvokeAsync(() =>
                {
                    MessageBox.Show($"Ошибка при добавлении новой строки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
        }

        private void GridView3_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (e.Row is NormKont normKont)
            {
                normKont.AnnId = _newAnnId;
                gridViewKont.UpdateCurrentRow();
            }
        }

        private async void GridView3_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {

        }

        #endregion

        #region Dop
        private void GridView4_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            var gridView = sender as GridView;
            if (gridView == null)
                return;

            // Проверяем, есть ли уже строки в таблице
            if (gridView.DataRowCount > 0)
            {
                // Если есть хотя бы одна строка, удаляем новую строку
                gridView.DeleteRow(e.RowHandle);
                return;
            }

            try
            {
                // Заполняем значения в текущей новой строке
                gridView.SetRowCellValue(e.RowHandle, "AnnId", _newAnnId);
                gridView.SetRowCellValue(e.RowHandle, "Text", "Дополнительная обработка");
                gridView.UpdateCurrentRow();
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при добавлении новой строки в GridView4");
                MessageBox.Show($"Ошибка при добавлении новой строки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridView4_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (e.Row is NormDopObr normDopObr)
            {
                normDopObr.AnnId = _newAnnId;
                gridViewDopObr.UpdateCurrentRow();
            }
        }

        private async void GridView4_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (e.Row is NormDopObr normDopObr)
            {
                try
                {
                    // Убеждаемся что AnnId установлен
                    normDopObr.AnnId = _newAnnId;

                    // Если это новая запись (Kod пустой), сохраняем в БД
                    if (string.IsNullOrEmpty(normDopObr.Kod))
                    {
                      //  normDopObr.Kod = await _artNormService.InsertNormDopObrAsync(normDopObr);
                        if (string.IsNullOrEmpty(normDopObr.Kod))
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
        private async void btnOK_Click(object sender, EventArgs e)
        {
            _okPressed = true;
            if ((groupTextBox.Text.TrimEnd() == "") && (modelTextBox.Text.TrimEnd() == ""))
            {
                MessageBox.Show("Заполните поле Модель либо Группа", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }
            else
            {
                try
                {
                    await SaveAllDataAsync();
                    try
                    {
                        ArtNormN annData = GetAnnDataFromUI();

                        // Устанавливаем статус в зависимости от режима
                        switch (_mode)
                        {
                            case (int)Mode.NewWorkDivision:
                                annData.Status = (int)Status.Preliminary;
                                annData.StatusText = StatusHelper.GetStatusText((int)Status.Preliminary);
                                break;
                            case (int)Mode.Edit:
                                var originalRecord = await _artNormService.GetArtNormDataById(_selectedAnnId);
                                annData.Status = originalRecord?.Status ?? 0;
                                annData.StatusText = originalRecord?.StatusText ?? "";
                                break;
                            case (int)Mode.ArchAndCopy:
                                originalRecord = await _artNormService.GetArtNormDataById(_selectedAnnId);
                                annData.Status = originalRecord.Status;
                                annData.StatusText = originalRecord.StatusText;//TODO найти, где уже установлен статус, 100%это уже сделано
                                break;
                        }

                        // Сохраняем данные в таблицу ann
                        await _artNormService.UpdateEntityAsync(TableNames.Ann, TableNames.AnnId, annData);
                        CreatedAnn = annData;

                        MessageBox.Show("Данные успешно сохранены", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex) 
                    {
                        await _logger.LogErrorAsync(ex, "Ошибка при сохранении данных в БД");
                        MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DialogResult = DialogResult.None;
                        return;
                    }
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
        }
        private async Task SaveAllDataAsync()
        {
            try
            {
                bool isEditMode = _mode == (int)Mode.Edit;

                // NormRasz
                foreach (var rasz in _normRaszList)
                {
                    try
                    {
                        if (isEditMode)
                        {
                            // В режиме редактирования обновляем существующие записи
                            if (rasz.nrId > 0)
                            {
                                await _artNormService.UpdateEntityAsync(TableNames.Rasz, TableNames.RaszId, rasz);
                            }
                            else
                            {
                                rasz.AnnId = _newAnnId;
                                rasz.nrId = await _artNormService.InsertEntityAsync(TableNames.Rasz, TableNames.RaszId, rasz);
                            }
                        }
                        else
                        {
                            // При добавлении предварительного или копировании создаем новые записи
                            var newRasz = new NormRasz
                            {
                                AnnId = _newAnnId,
                                Kod_o = rasz.Kod_o,
                                Text = rasz.Text,
                                Spec = rasz.Spec,
                                Razryad = rasz.Razryad,
                                Obor = rasz.Obor,
                                Kod = rasz.Kod,
                                N1 = rasz.N1,
                                Sek = rasz.Sek
                            };
                            newRasz.nrId = await _artNormService.InsertEntityAsync(TableNames.Rasz, TableNames.RaszId, newRasz);
                        }
                    }
                    catch (Exception ex)
                    { 
                        await _logger.LogErrorAsync(ex, "Ошибка при сохранении записи NormRasz");
                    }
                    IsRaszInserted = true;
                }

                // NormRask
                if (isEditMode)
                {
                    // В режиме редактирования сначала удаляем все существующие записи
                    var existingRecords = await _artNormService.GetRelatedNormRask(_newAnnId);
                    foreach (DataRow row in existingRecords.Rows)
                    {
                        if (row["id"] != DBNull.Value)
                        {
                            await _artNormService.DeleteEntityAsync(TableNames.Rask, TableNames.RaskId, 
                                new NormRask { id = Convert.ToInt32(row["id"]) });
                        }
                    }
                }

                foreach (var rask in _normRaskList)
                {
                    try
                    {
                        if (isEditMode)
                        {
                            // В режиме редактирования все записи добавляются как новые
                            var newRask = new NormRask
                            {
                                AnnId = _newAnnId,
                                KodO = rask.KodO,
                                Text = rask.Text,
                                Spec = rask.Spec,
                                Razryad = rask.Razryad,
                                Obor = rask.Obor,
                                Kod = rask.Kod,
                                N1 = rask.N1,
                                Sek = rask.Sek
                            };
                            newRask.id = await _artNormService.InsertEntityAsync(TableNames.Rask, TableNames.RaskId, newRask);
                        }
                        else
                        {
                            // При добавлении предварительного или копировании создаем новые записи
                            var newRask = new NormRask
                            {
                                AnnId = _newAnnId,
                                KodO = rask.KodO,
                                Text = rask.Text,
                                Spec = rask.Spec,
                                Razryad = rask.Razryad,
                                Obor = rask.Obor,
                                Kod = rask.Kod,
                                N1 = rask.N1,
                                Sek = rask.Sek
                            };
                            newRask.id = await _artNormService.InsertEntityAsync(TableNames.Rask, TableNames.RaskId, newRask);
                        }
                    }
                    catch (Exception ex)
                    { 
                        await _logger.LogErrorAsync(ex, "Ошибка при сохранении записи NormRask");
                    }
                    IsRaskInserted = true;
                }

                // NormKont
                foreach (var kont in _normKontList)
                {
                    try
                    {
                        if (isEditMode)
                        {
                            // В режиме редактирования обновляем существующую запись
                            await _artNormService.UpdateEntityAsync(TableNames.Kont, TableNames.Kont, kont);
                        }
                        else
                        {
                            // При добавлении предварительного или копировании создаем новую запись
                            var newKont = new NormKont
                            {
                                AnnId = _newAnnId,
                                Text = kont.Text,
                                Kod = kont.Kod,
                                KodO = kont.KodO,
                                SebS = kont.SebS,
                                Sek = kont.Sek,
                                Seb = kont.Seb,
                                Spec = kont.Spec,
                                N = kont.N,
                                N1 = kont.N1,
                                NCh = kont.NCh,
                                Razryad = kont.Razryad,
                                Obor = kont.Obor
                            };
                            await _artNormService.InsertNormKontAsync(newKont);
                        }
                    }
                    catch (Exception ex)
                    {
                        await _logger.LogErrorAsync(ex, "Ошибка при сохранении записи NormKont");
                    }
                    IsKontInserted = true;
                }

                // NormDopObr
                foreach (var dop in _normDopObrList)
                {
                    try
                    {
                        if (isEditMode)
                        {
                            // В режиме редактирования обновляем существующую запись
                            await _artNormService.UpdateEntityAsync(TableNames.Obr, TableNames.ObrId, dop);
                        }
                        else
                        {
                            // При добавлении предварительного или копировании создаем новую запись
                            var newDop = new NormDopObr
                            {
                                AnnId = _newAnnId,
                                Kod = dop.Kod,
                                SekP = dop.SekP,
                                SekStra = dop.SekStra,
                                SekTamp = dop.SekTamp,
                                SekV = dop.SekV
                            };
                            await _artNormService.InsertDopObrAsync(newDop);
                        }
                    }
                    catch (Exception ex)
                    {
                        await _logger.LogErrorAsync(ex, "Ошибка при сохранении записи NormDopObr");
                    }
                    IsDopObrInserted = true;
                }

                // Обновляем UI после сохранения
                await this.InvokeAsync(() =>
                {
                    gridControlRasz.RefreshDataSource();
                    gridControlRaskr.RefreshDataSource();
                    gridControlKont.RefreshDataSource();
                    gridControlDopObr.RefreshDataSource();
                    
                    // Обновляем основные поля формы
                    nameTextBox.Refresh();
                    groupTextBox.Refresh();
                    modelTextBox.Refresh();
                    secTimeTextBox.Refresh();
                    designerComboBox.Refresh();
                    constructorComboBox.Refresh();
                });
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при сохранении данных");
                throw;
            }
        }

        private ArtNormN GetAnnDataFromUI()
        {
            // Обновляем данные в таблице ann
            return new ArtNormN
            {
                AnnID = _newAnnId,
                Articul = nameTextBox.Text,
                Group = groupTextBox.Text,
                Mod = modelTextBox.Text,
                Sek = !string.IsNullOrWhiteSpace(secTimeTextBox.Text)? Convert.ToInt32(secTimeTextBox.Text) : 0,
                Diz = designerComboBox.SelectedValue != null ? Convert.ToInt32(designerComboBox.SelectedValue) : 0,
                Constr = constructorComboBox.SelectedValue != null ? Convert.ToInt32(constructorComboBox.SelectedValue) : 0
            };
        }

        /// <summary>
        /// Обработчик события изменения выбранного элемента в выпадающих списках конструктора и дизайнера
        /// </summary>
        private async void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.ComboBox comboBox && comboBox.SelectedValue != null)
            {
                try
                {
                    int selectedId = Convert.ToInt32(comboBox.SelectedValue);
                    string fieldName = string.Empty;
                    
                    if (comboBox == constructorComboBox)
                    {
                        fieldName = "constr";
                        await _logger.LogEventAsync($"Выбран конструктор с ID {selectedId}", "ComboBox_SelectedIndexChanged");
                    }
                    else if (comboBox == designerComboBox)
                    {
                        fieldName = "diz";
                        await _logger.LogEventAsync($"Выбран дизайнер с ID {selectedId}", "ComboBox_SelectedIndexChanged");
                    }
                    
                    //if (!string.IsNullOrEmpty(fieldName) && _bufferWorkDivision > 0)
                    //{
                    //    if (_mode == (int)Mode.Edit)
                    //    {
                    //        await _artNormService.UpdateAnnId(TableNames.Ann, _bufferWorkDivision, fieldName, selectedId);
                    //        await _logger.LogEventAsync($"Обновлено поле {fieldName} для ID {_bufferWorkDivision} значением {selectedId}", "ComboBox_SelectedIndexChanged");
                            
                    //        // Обновляем UI после изменения
                    //        comboBox.Refresh();
                    //    }
                    //    else
                    //    {
                    //        await _logger.LogEventAsync($"Выбрано значение {fieldName}={selectedId} для нового разделения труда", "ComboBox_SelectedIndexChanged");
        //    }
        //}
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
                    // Загружаем данные из буфера
                    await WorkDivisionLoadAsync(_bufferWorkDivision);

                    MessageBox.Show("Данные из буфера успешно загружены", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    }
}
