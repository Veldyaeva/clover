using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.Data;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BindingSource = System.Windows.Forms.BindingSource;
using SewingProduction.form.TeamWork.Forms;
using DevExpress.XtraGrid.Views.Base;
using System.IO;

namespace SewingProduction.form
{
    public partial class TeamWork_AdvanceTW : CustomForm
    {
        private readonly ArtNormService _artNormService;
        private int _bufferWorkDivision;
        private readonly DatabaseHelper _dbHelper;
        private readonly GridHelper _gridHelper = new GridHelper();
        private int _newAnnId = -1;
        private readonly ILogger _logger = new FileLogger();
        private int _mode;

        private BindingList<NormRasz> _normRaszList;
        private BindingSource _normRaszBindingSource;
        private BindingList<NormRask> _normRaskList;
        private BindingSource _normRaskBindingSource;

        public TeamWork_AdvanceTW(int id, int bufferWorkDivision, int mode)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _artNormService = new ArtNormService(_dbHelper);
            ThemeManager.UpdateTheme(this);

            _bufferWorkDivision = bufferWorkDivision;
            _mode = mode;
            _newAnnId = id;

            InitializeBindings();
        }

        private void InitializeBindings()
        {
            // Инициализация для NormRasz
            _normRaszList = new BindingList<NormRasz>();
            _normRaszBindingSource = new BindingSource { DataSource = _normRaszList };
            gridControl5.DataSource = _normRaszBindingSource;

            // Инициализация для NormRask
            _normRaskList = new BindingList<NormRask>();
            _normRaskBindingSource = new BindingSource { DataSource = _normRaskList };
            gridControl2.DataSource = _normRaskBindingSource;

            // Настраиваем обработчики для NormRasz
            gridView5.InitNewRow += GridView5_InitNewRow;
            gridView5.RowUpdated += GridView5_RowUpdated;
            gridView5.ValidateRow += GridView5_ValidateRow;
            gridView5.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;

            // Настраиваем обработчики для NormRask
            gridView2.InitNewRow += GridView2_InitNewRow;
            gridView2.RowUpdated += GridView2_RowUpdated;
            gridView2.ValidateRow += GridView2_ValidateRow;
            gridView2.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;

            // Подписываемся на события изменения комбобоксов
            designerComboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            constructorComboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
        }



        private void TeamWork_AdvanceTW_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Проверяем, не установлены ли редакторы в режим Inplace
                if (gridView5 != null && gridView5.OptionsBehavior.EditingMode == GridEditingMode.Inplace)
                {
                    gridView5.OptionsBehavior.EditingMode = GridEditingMode.EditForm;
                }
                
                if (gridView2 != null && gridView2.OptionsBehavior.EditingMode == GridEditingMode.Inplace)
                {
                    gridView2.OptionsBehavior.EditingMode = GridEditingMode.EditForm;
                }

                // Сохраняем настройки для всех гридов при закрытии формы
                _gridHelper.SaveGridViewSettings(gridView2, "AdvanceTW_gridView2Layout.xml");
                _gridHelper.SaveGridViewSettings(gridView3, "AdvanceTW_gridView3Layout.xml");
                _gridHelper.SaveGridViewSettings(gridView4, "AdvanceTW_gridView4Layout.xml");
                _gridHelper.SaveGridViewSettings(gridView5, "AdvanceTW_gridView5Layout.xml");
            }
            catch (Exception ex)
            {
                // Логируем ошибку, но не мешаем закрытию формы
                if (_logger != null)
                {
                    _logger.LogErrorAsync(ex, "Ошибка при закрытии формы TeamWork_AdvanceTW");
                }
            }
        }

        private void GridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            var gridView = sender as GridView;
            if (gridView == null)
                return;

            // Сохраняем настройки редактирования
            var allowEditing = gridView.OptionsBehavior.Editable;
            
            // Временно отключаем редактирование, чтобы предотвратить появление PopupEditForm
            gridView.OptionsBehavior.Editable = false;

            try
            {
            using (var selectionForm = new NormOperNew())
            {
                    DialogResult result = selectionForm.ShowDialog();
                    
                    if (result == DialogResult.OK)
                {
                    var selectedData = selectionForm.SelectedRowData;
                    if (selectedData != null)
                    {
                        // Заполняем значения в текущей новой строке
                            gridView.SetRowCellValue(e.RowHandle, "AnnId", _newAnnId);
                            gridView.SetRowCellValue(e.RowHandle, "KodO", selectedData.KodO);
                            gridView.SetRowCellValue(e.RowHandle, "Text", selectedData.Text);
                            gridView.SetRowCellValue(e.RowHandle, "Spec", selectedData.Spec);
                            gridView.SetRowCellValue(e.RowHandle, "Razryad", selectedData.Razryad);
                            gridView.SetRowCellValue(e.RowHandle, "Obor", selectedData.Obor);
                            gridView.SetRowCellValue(e.RowHandle, "KodProizv", selectedData.KodProizv);
                            gridView.SetRowCellValue(e.RowHandle, "Kod", selectedData.Kod);
                            gridView.SetRowCellValue(e.RowHandle, "N1", selectedData.N1);
                            gridView.SetRowCellValue(e.RowHandle, "Sek", selectedData.Sek);
                            gridView.SetRowCellValue(e.RowHandle, "KodPodr", selectedData.KodPodr);
                            gridView.SetRowCellValue(e.RowHandle, "KodOb", selectedData.KodOb);
                    }
                    else
                    {
                        // Если данные не выбраны, удаляем строку
                            gridView.DeleteRow(e.RowHandle);
                    }
                }
                else
                {
                        // Если диалог закрыт не через OK, удаляем строку
                        gridView.DeleteRow(e.RowHandle);
                    }
                }
                }
            finally
            {
                // Восстанавливаем настройки редактирования
                gridView.OptionsBehavior.Editable = allowEditing;
            }
        }

        private void GridView5_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (e.Row is NormRasz normRasz)
            {
                normRasz.AnnId = _newAnnId;
                gridView5.UpdateCurrentRow();
            }
        }

        private async void GridView5_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (e.Row is NormRasz normRasz)
            {
                try
                {
                    // Убеждаемся что AnnId установлен
                    normRasz.AnnId = _newAnnId;

                    // Если это новая запись (nrId <= 0), сохраняем в БД
                    if (normRasz.nrId <= 0)
                    {
                        normRasz.nrId = await _artNormService.InsertNormRaszAsync(normRasz);
                        if (normRasz.nrId <= 0)
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

        private async void GridView2_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var gridView = sender as GridView;
            if (gridView == null)
                return;

            // Сохраняем настройки редактирования
            var allowEditing = gridView.OptionsBehavior.Editable;

            // Временно отключаем редактирование, чтобы предотвратить появление PopupEditForm
            gridView.OptionsBehavior.Editable = false;

            try
            {
                using (var selectionForm = new norm_raskrNew())
                {
                    DialogResult result = selectionForm.ShowDialog();

                    if (result == DialogResult.OK && selectionForm.SelectedData != null && selectionForm.SelectedData.Count > 0)
                    {
                        var selectedDataList = selectionForm.SelectedData;

                        // Логируем количество выбранных элементов
                        await _logger.LogEventAsync($"Выбрано элементов: {selectedDataList.Count}", "GridView2_InitNewRow");

                        // Вставляем данные в gridView2
                        foreach (var normRask in selectedDataList)
                        {
                            normRask.AnnId = _newAnnId;
                            _normRaskList.Add(normRask); // Добавляем в список

                            // Сохраняем данные в базу данных сразу, с ожиданием результата
                            try
                            {
                                int newId = await _artNormService.InsertNormRaskAsync(normRask);
                                normRask.id = newId; // Обновляем ID после сохранения
                            }
                            catch (Exception ex)
                            {
                                await _logger.LogErrorAsync(ex, "Ошибка при сохранении NormRask в БД");
            }
        }

                        // Обновляем привязку данных и интерфейс
                        _normRaskBindingSource.ResetBindings(false);
                        gridControl2.RefreshDataSource();
                        gridView.RefreshData();

                        // Обновляем текущую строку
                        gridView.UpdateCurrentRow();
                    }
                    else
                    {
                        // Если пользователь отменил выбор или не выбрал данные, удаляем строку
                        gridView.DeleteRow(e.RowHandle);
                    }
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка в методе GridView2_InitNewRow");
            }
            finally
            {
                // Восстанавливаем настройки редактирования
                gridView.OptionsBehavior.Editable = allowEditing;
            }
        }

        private void GridView2_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (e.Row is NormRask normRask)
            {
                normRask.AnnId = _newAnnId;
                gridView2.UpdateCurrentRow();
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

        private async void TeamWork_AdvanceTW_Load(object sender, EventArgs e)
        {
            try
            {
                // Загружаем настройки для всех гридов
                _gridHelper.LoadGridViewSettings(gridView2, "AdvanceTW_gridView2Layout.xml");
                _gridHelper.LoadGridViewSettings(gridView3, "AdvanceTW_gridView3Layout.xml");
                _gridHelper.LoadGridViewSettings(gridView4, "AdvanceTW_gridView4Layout.xml");
                _gridHelper.LoadGridViewSettings(gridView5, "AdvanceTW_gridView5Layout.xml");

                // Загружаем списки дизайнеров и конструкторов
                await LoadFioLists();

                switch (_mode)
                {
                    case (int)Mode.NewWorkDivision:
                        this.Text = "Добавить предварительное";
                        break;
                    case (int)Mode.ArchAndCopy:
                        this.Text = "Архив+копия";
                        await bufferLoad();
                        break;
                    case (int)Mode.Archive:
                        this.Text = "В архив";
                        await bufferLoad();
                        break;
                    case (int)Mode.Edit:
                        this.Text = "Редактировать";
                        await bufferLoad();
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при загрузке формы TeamWork_AdvanceTW");
            }
        }

        /// <summary>
        /// Загружает списки дизайнеров и конструкторов в комбобоксы
        /// </summary>
        private async Task LoadFioLists()
        {
            try
            {
                // Получаем список сотрудников
                var fioData = await _artNormService.GetRelDesigner();

                if (fioData != null && fioData.Rows.Count > 0)
                {
                    // Создаем источники данных для комбобоксов
                    BindingSource designerBindingSource = new BindingSource();
                    BindingSource constructorBindingSource = new BindingSource();
                    
                    // Устанавливаем данные
                    designerBindingSource.DataSource = fioData.Copy();
                    constructorBindingSource.DataSource = fioData.Copy();
                    
                    // Настраиваем комбобоксы
                    designerComboBox.DataSource = designerBindingSource;
                    designerComboBox.DisplayMember = "fio";
                    designerComboBox.ValueMember = "tab";
                    
                    constructorComboBox.DataSource = constructorBindingSource;
                    constructorComboBox.DisplayMember = "fio";
                    constructorComboBox.ValueMember = "tab";
                    
                    // Логируем успешную загрузку
                    await _logger.LogEventAsync($"Списки дизайнеров и конструкторов успешно загружены", "LoadFioLists");
                }
                else
                {
                    await _logger.LogEventAsync("Не удалось загрузить списки дизайнеров и конструкторов", "LoadFioLists");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке списков дизайнеров и конструкторов");
            }
        }

        private async Task bufferLoad()
        {
            try
            {
                // Загрузка данных NormRasz
                var normRaszData = await _artNormService.GetRelatedNormRasz(_bufferWorkDivision);
                _normRaszList.Clear();
                
                // Преобразуем DataTable в список объектов NormRasz
                foreach (DataRow row in normRaszData.Rows)
                {
                    var normRasz = new NormRasz();
                    foreach (DataColumn col in normRaszData.Columns)
                    {
                        var prop = typeof(NormRasz).GetProperty(col.ColumnName);
                        if (prop != null && row[col] != DBNull.Value)
                        {
                            prop.SetValue(normRasz, Convert.ChangeType(row[col], prop.PropertyType));
                        }
                    }
                    _normRaszList.Add(normRasz);
                }
                
                _normRaszBindingSource.ResetBindings(false);
                
                // Загрузка данных NormRask
                var normRaskData = await _artNormService.GetRelatedNormRask(_bufferWorkDivision);
                _normRaskList.Clear();
                
                // Преобразуем DataTable в список объектов NormRask
                foreach (DataRow row in normRaskData.Rows)
                {
                    var normRask = new NormRask();
                    foreach (DataColumn col in normRaskData.Columns)
                    {
                        var prop = typeof(NormRask).GetProperty(col.ColumnName);
                        if (prop != null && row[col] != DBNull.Value)
                        {
                            prop.SetValue(normRask, Convert.ChangeType(row[col], prop.PropertyType));
                        }
                    }
                    _normRaskList.Add(normRask);
                }
                
                _normRaskBindingSource.ResetBindings(false);
                
                // Загрузка данных из ANN
                await LoadAnnData();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка загрузки данных в буфер");
                MessageBox.Show("Ошибка загрузки данных. Подробности в логе.", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Загружает данные из таблицы ArtNormN в соответствующие контролы формы
        /// </summary>
        private async Task LoadAnnData()
        {
            try
            {
                // Получаем данные ANN по ID
                var annData = await _artNormService.GetArtNormDataById(_bufferWorkDivision);
                
                if (annData != null)
                {
                    // Заполняем текстовые поля
                    nameTextBox.Text = annData.Articul;
                    groupTextBox.Text = annData.Group;
                    modelTextBox.Text = annData.Mod;
                    secTimeTextBox.Text = annData.Sek.ToString();
                    
                    // Устанавливаем выбранные значения для дизайнера и конструктора
                    if (annData.Diz > 0)
                    {
                        try
                        {
                            designerComboBox.SelectedValue = annData.Diz;
                        }
                        catch
                        {
                            // Если не удалось найти значение в списке, просто продолжаем
                            await _logger.LogEventAsync($"Не удалось найти дизайнера с ID {annData.Diz} в списке", "LoadAnnData");
                        }
                    }
                    
                    if (annData.Constr > 0)
                    {
                        try
                        {
                            constructorComboBox.SelectedValue = annData.Constr;
                        }
                        catch
                        {
                            // Если не удалось найти значение в списке, просто продолжаем
                            await _logger.LogEventAsync($"Не удалось найти конструктора с ID {annData.Constr} в списке", "LoadAnnData");
                        }
                    }
                    
                    // Логируем успешную загрузку
                    await _logger.LogEventAsync($"Данные ANN успешно загружены для ID {_bufferWorkDivision}", "LoadAnnData");
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные ANN для ID {_bufferWorkDivision}", "LoadAnnData");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных ANN для ID {_bufferWorkDivision}");
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
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
                    // Получаем выбранный ID сотрудника
                    int selectedId = Convert.ToInt32(comboBox.SelectedValue);
                    string fieldName = string.Empty;
                    
                    // Определяем, какой комбобокс был изменен
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
                    
                    // Если известно поле для обновления, сохраняем изменения
                    if (!string.IsNullOrEmpty(fieldName) && _bufferWorkDivision > 0)
                    {
                        // Проверяем, в каком режиме находимся
                        if (_mode == (int)Mode.Edit)
                        {
                            // Сохраняем изменения в базу данных
                            await _artNormService.UpdateEmployeeField(_bufferWorkDivision, fieldName, selectedId);
                            await _logger.LogEventAsync($"Обновлено поле {fieldName} для ID {_bufferWorkDivision} значением {selectedId}", "ComboBox_SelectedIndexChanged");
                        }
                        else
                        {
                            // В других режимах сохраняем значение для использования при сохранении
                            await _logger.LogEventAsync($"Выбрано значение {fieldName}={selectedId} для нового разделения труда", "ComboBox_SelectedIndexChanged");
                        }
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
            try
            {
                // Загружаем данные из буфера
                await bufferLoad();
                
                // Здесь можно добавить дополнительную логику для обработки данных после загрузки из буфера
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
    }
}
