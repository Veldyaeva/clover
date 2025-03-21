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
using DevExpress.XtraVerticalGrid;
using ComboBox = System.Windows.Forms.ComboBox;
using System.Collections.Generic;

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

            // Настраиваем отображение колонок
            SetupGridColumns();
            SetupGridColumnsRask();
        }

        private void SetupGridColumns()
        {
            //gridView5.Columns.Clear();
            
            //gridView5.Columns.AddVisible("KodO", "Код операции");
            //gridView5.Columns.AddVisible("Text", "Текст");
            //gridView5.Columns.AddVisible("Spec", "Специальность");
            //gridView5.Columns.AddVisible("Razryad", "Разряд");
            //gridView5.Columns.AddVisible("Obor", "Оборудование");
            //gridView5.Columns.AddVisible("KodProizv", "Код производства");
            //gridView5.Columns.AddVisible("Kod", "Код");
            //gridView5.Columns.AddVisible("N1", "Норма");
            //gridView5.Columns.AddVisible("Sek", "Секунды");
            //gridView5.Columns.AddVisible("KodPodr", "Код подразделения");
            //gridView5.Columns.AddVisible("KodOb", "Код оборудования");

            //// Скрываем служебные поля
            //if (gridView5.Columns["nrId"] != null)
            //    gridView5.Columns["nrId"].Visible = false;
            //if (gridView5.Columns["AnnId"] != null)
            //    gridView5.Columns["AnnId"].Visible = false;

            // Настраиваем опции редактирования
            gridView5.OptionsBehavior.EditingMode = GridEditingMode.EditForm;
            gridView5.OptionsEditForm.EditFormColumnCount = 1;
            gridView5.OptionsEditForm.PopupEditFormWidth = 400;
            gridView5.OptionsView.ShowGroupPanel = false;
        }


        private void SetupGridColumnsRask()
        {

            // Скрываем служебные поля
            if (gridView2.Columns["id"] != null)
                gridView2.Columns["id"].Visible = false;
            if (gridView2.Columns["annId"] != null)
                gridView2.Columns["annId"].Visible = false;

            // Настраиваем опции редактирования
            //gridView2.OptionsBehavior.EditingMode = GridEditingMode.EditForm;
            //gridView2.OptionsEditForm.EditFormColumnCount = 1;
           // gridView2.OptionsEditForm.PopupEditFormWidth = 400;
            gridView2.OptionsView.ShowGroupPanel = false;
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
                // Загружаем настройки для всех гридов при открытии формы
                LoadGridSettings();
                // Получаем данные из ANN для _bufferWorkDivision и отображаем информацию в richTextBox1
                if (_bufferWorkDivision > 0)
                {
                    var annData = await _artNormService.GetArtNormById(_bufferWorkDivision);
                    if (annData != null)
                    {
                        richTextBox1.Text = $"группа: {annData.Group.ToString().TrimEnd()}\r\n," +
                            $" модель: {annData.Mod.ToString().TrimEnd()}\r\n," +
                            $" артикул: {annData.Articul.ToString().TrimEnd()}";
                    }
                }

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
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке формы TeamWork_AdvanceTW");
                MessageBox.Show($"Ошибка при инициализации формы: {ex.Message}", 
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Можно добавить логику для мгновенной валидации или других действий при изменении выбора в комбобоксе
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.SelectedItem != null)
            {
                var selectedItem = (KeyValuePair<int, string>)comboBox.SelectedItem;
                string role = comboBox == constructorComboBox ? "дизайнера" : "конструктора";
                
                // Логируем выбор пользователя
                _logger.LogEventAsync($"Выбран новый {role}: {selectedItem.Value} (ID: {selectedItem.Key})", "ComboBox_SelectedIndexChanged").Wait();
            }
        }

        private void LoadGridSettings()
        {
            try
            {
                // Загружаем настройки для всех гридов
                _gridHelper.LoadGridViewSettings(gridView2, "AdvanceTW_gridView2Layout.xml");
                _gridHelper.LoadGridViewSettings(gridView3, "AdvanceTW_gridView3Layout.xml");
                _gridHelper.LoadGridViewSettings(gridView4, "AdvanceTW_gridView4Layout.xml");
                _gridHelper.LoadGridViewSettings(gridView5, "AdvanceTW_gridView5Layout.xml");
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при загрузке настроек для гридов").Wait();
            }
        }

        /// <summary>
        /// Загружает данные из буфера для режимов Архив+копия, В архив и Редактировать
        /// </summary>
        private async Task bufferLoad()
        {
            try
            {
                if (_bufferWorkDivision <= 0)
                {
                    await _logger.LogErrorAsync(new Exception("Некорректный ID разделения работ"), "Ошибка загрузки данных из буфера");
                    return;
                }

                await GridHelper.LoadGridControlDataAsync(gridControl5, normraszBindingSource, await _artNormService.GetRelatedNormRasz(_bufferWorkDivision));
                await GridHelper.LoadGridControlDataAsync(gridControl2, normraskBindingSource, await _artNormService.GetRelatedNormRask(_bufferWorkDivision));
                await GridHelper.LoadGridControlDataAsync(gridControl3, normkontBindingSource, await _artNormService.GetRelatedNormKont(_bufferWorkDivision));
                await GridHelper.LoadGridControlDataAsync(gridControl4, normdopobrBindingSource, await _artNormService.GetRelatedNormDopObr(_bufferWorkDivision));

                // Получаем данные из ANN для _bufferWorkDivision
                var annData = await _artNormService.GetArtNormById(_bufferWorkDivision);
                if (annData != null)
                {
                    // Заполняем текстовые поля данными из ANN
                    groupTextBox.Text = annData.Group;
                    ModelTextBox.Text = annData.Mod;
                    NameTextBox.Text = annData.Articul;
                    SecTimeTextBox.Text = annData.Sek.ToString();
                    if (annData.DataSozd.HasValue)
                    {
                        dateCreate.Value = annData.DataSozd.Value;
                    }

                    // Если есть RichTextBox для комментария, заполняем его
                    if (richTextBox1 != null)
                    {
                        richTextBox1.Text = annData.Komment;
                    }
                    
                    // Заполняем комбобоксы данными дизайнера и конструктора
                    await LoadDesignerAndConstructorData(annData);
                }
                else
                {
                    await _logger.LogErrorAsync(new Exception($"Не удалось получить данные ANN для ID {_bufferWorkDivision}"), 
                        "Ошибка загрузки данных ANN в буфер");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке данных из буфера");
                MessageBox.Show($"Ошибка при загрузке данных из буфера: {ex.Message}", 
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Загружает данные о дизайнере и конструкторе в комбобоксы
        /// </summary>
        private async Task LoadDesignerAndConstructorData(ArtNormN annData)
        {
            try
            {
                // Получение данных дизайнеров и конструкторов
                var designersData = await _artNormService.GetRelDesigner();
                
                //// Очистка комбобоксов перед загрузкой новых данных
                //constructorComboBox.Items.Clear();
                //designerComboBox.Items.Clear();
                
                // Заполнение комбобоксов данными
                foreach (DataRow row in designersData.Rows)
                {
                    string fioValue = row["fio"].ToString();
                    int tabNumber = Convert.ToInt32(row["tab"]);
                    
                    // Добавляем элементы в комбобоксы
                    constructorComboBox.Items.Add(new KeyValuePair<int, string>(tabNumber, fioValue));
                    designerComboBox.Items.Add(new KeyValuePair<int, string>(tabNumber, fioValue));
                }
                
                // Настраиваем отображение элементов в комбобоксах
                constructorComboBox.DisplayMember = "Value";
                constructorComboBox.ValueMember = "Key";
                designerComboBox.DisplayMember = "Value";
                designerComboBox.ValueMember = "Key";
                
                // Выбор дизайнера и конструктора если они указаны в данных
                if (annData.Diz > 0)
                {
                    string designerName = await _artNormService.GetEmployeeFullName(annData.Diz);
                    for (int i = 0; i < constructorComboBox.Items.Count; i++)
                    {
                        var item = (KeyValuePair<int, string>)constructorComboBox.Items[i];
                        if (item.Key == annData.Diz)
                        {
                            constructorComboBox.SelectedIndex = i;
                            break;
                        }
                    }
                }
                
                if (annData.Constr > 0)
                {
                    string constructorName = await _artNormService.GetEmployeeFullName(annData.Constr);
                    for (int i = 0; i < designerComboBox.Items.Count; i++)
                    {
                        var item = (KeyValuePair<int, string>)designerComboBox.Items[i];
                        if (item.Key == annData.Constr)
                        {
                            designerComboBox.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке данных о дизайнере и конструкторе");
                MessageBox.Show($"Ошибка при загрузке данных о дизайнере и конструкторе: {ex.Message}", 
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                    // Сохраняем все данные в связанные таблицы
                    await SaveAllData();
                    
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при сохранении данных");
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Сохраняет все данные в связанные таблицы: art_norm_n, norm_rasz, norm_raskr, norm_kont, norm_dop_obr
        /// </summary>
        private async Task SaveAllData()
        {
            if (_newAnnId <= 0)
            {
                throw new Exception("Некорректный ID разделения работ");
            }

            // 1. Сохраняем данные в таблицу art_norm_n
            await SaveArtNormNData();
            
            // 2. Сохраняем данные в таблицу norm_rasz
            await SaveNormRaszData();
            
            // 3. Сохраняем данные в таблицу norm_raskr
            //await SaveNormRaskrData();
            
            // 4. Сохраняем данные в таблицу norm_kont
            await SaveNormKontData();
            
            // 5. Сохраняем данные в таблицу norm_dop_obr
            await SaveNormDopObrData();
            
            // 6. Сохраняем выбранные значения дизайнера и конструктора
            await SaveDesignerAndConstructorSelections();
            
            // Логируем успешное сохранение
            await _logger.LogEventAsync($"Все данные успешно сохранены для annId={_newAnnId}", "SaveAllData");
        }

        /// <summary>
        /// Сохраняет данные в таблицу art_norm_n
        /// </summary>
        private async Task SaveArtNormNData()
        {
            try
            {
                // Собираем данные из формы
                string group = groupTextBox.Text.Trim();
                string model = ModelTextBox.Text.Trim();
                string articul = NameTextBox.Text.Trim();
                int sek = 0;
                int.TryParse(SecTimeTextBox.Text, out sek);
                DateTime dataSozd = dateCreate.Value;
                string komment = richTextBox1.Text;
                
                // Создаем SQL запрос для обновления
                string updateQuery = @"
                    UPDATE art_norm_n 
                    SET grup = @grup, 
                        mod = @mod, 
                        articul = @articul, 
                        sek = @sek, 
                        data_sozd = @data_sozd, 
                        komment = @komment
                    WHERE annId = @annId";
                    
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@grup", group },
                    { "@mod", model },
                    { "@articul", articul },
                    { "@sek", sek },
                    { "@data_sozd", dataSozd },
                    { "@komment", komment },
                    { "@annId", _newAnnId }
                };
                
                await _dbHelper.ExecuteNonQueryAsync(updateQuery, parameters);
                
                // Логируем успешное обновление
                await _logger.LogEventAsync($"Обновлены данные art_norm_n для annId={_newAnnId}", "SaveArtNormNData");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при сохранении данных в таблицу art_norm_n");
                throw new Exception($"Ошибка при сохранении данных в таблицу art_norm_n: {ex.Message}");
            }
        }

        /// <summary>
        /// Сохраняет данные в таблицу norm_rasz
        /// </summary>
        private async Task SaveNormRaszData()
        {
            try
            {
                // Перебираем все строки в гриде norm_rasz
                GridView view = gridView5;
                for (int i = 0; i < view.RowCount; i++)
                {
                    if (view.IsValidRowHandle(i))
                    {
                        var row = view.GetRow(i) as NormRasz;
                        if (row != null)
                        {
                            // Устанавливаем annId
                            row.AnnId = _newAnnId;
                            
                            // Если запись уже существует, обновляем ее
                            if (row.nrId > 0)
                            {
                                string updateQuery = @"
                                    UPDATE norm_rasz 
                                    SET kod_o = @kod_o, 
                                        text = @text, 
                                        spec = @spec, 
                                        razryad = @razryad, 
                                        obor = @obor, 
                                        kod_proizv = @kod_proizv, 
                                        kod = @kod, 
                                        n1 = @n1, 
                                        sek = @sek, 
                                        kod_podr = @kod_podr, 
                                        kod_ob = @kod_ob
                                    WHERE id = @id";
                                    
                                    Dictionary<string, object> parameters = new Dictionary<string, object>
                                    {
                                        { "@kod_o", row.KodO },
                                        { "@text", row.Text },
                                        { "@spec", row.Spec },
                                        { "@razryad", row.Razryad },
                                        { "@obor", row.Obor },
                                        { "@kod_proizv", row.KodProizv },
                                        { "@kod", row.Kod },
                                        { "@n1", row.N1 },
                                        { "@sek", row.Sek },
                                        { "@kod_podr", row.KodPodr },
                                        { "@kod_ob", row.KodOb },
                                        { "@id", row.nrId }
                                    };
                                    
                                    await _dbHelper.ExecuteNonQueryAsync(updateQuery, parameters);
                            }
                            // Если это новая запись, добавляем ее
                            else
                            {
                                row.nrId = await _artNormService.InsertNormRaszAsync(row);
                            }
                        }
                    }
                }
                
                // Логируем успешное обновление
                await _logger.LogEventAsync($"Обновлены данные norm_rasz для annId={_newAnnId}", "SaveNormRaszData");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при сохранении данных в таблицу norm_rasz");
                throw new Exception($"Ошибка при сохранении данных в таблицу norm_rasz: {ex.Message}");
            }
        }

        /// <summary>
        /// Сохраняет данные в таблицу norm_raskr
        /// </summary>
        //private async Task SaveNormRaskrData()
        //{
        //    try
        //    {
        //        // Перебираем все строки в гриде norm_raskr
        //        GridView view = gridView2;
        //        for (int i = 0; i < view.RowCount; i++)
        //        {
        //            if (view.IsValidRowHandle(i))
        //            {
        //                var row = view.GetRow(i) as NormRask;
        //                if (row != null)
        //                {
        //                    // Устанавливаем annId
        //                    row.AnnId = _newAnnId;
                            
        //                    // Если запись уже существует, обновляем ее
        //                    if (row.id > 0)
        //                    {
        //                        string updateQuery = @"
        //                            UPDATE norm_raskr 
        //                            SET annid = @annid, 
        //                                kod_detail = @kod_detail, 
        //                                name_detail = @name_detail, 
        //                                kol = @kol, 
        //                                kod_tkan = @kod_tkan, 
        //                                name_tkan = @name_tkan, 
        //                                square = @square,
        //                                ed_izm = @ed_izm
        //                            WHERE id = @id";
                                    
        //                            Dictionary<string, object> parameters = new Dictionary<string, object>
        //                            {
        //                                { "@annid", row.AnnId },
        //                                { "@kod_detail", row.KodDetail },
        //                                { "@name_detail", row.NameDetail },
        //                                { "@kol", row.Kol },
        //                                { "@kod_tkan", row.KodTkan },
        //                                { "@name_tkan", row.NameTkan },
        //                                { "@square", row.Square },
        //                                { "@ed_izm", row.EdIzm },
        //                                { "@id", row.id }
        //                            };
                                    
        //                            await _dbHelper.ExecuteNonQueryAsync(updateQuery, parameters);
        //                    }
        //                    // Если это новая запись, добавляем ее
        //                    else
        //                    {
        //                        row.id = await _artNormService.InsertNormRaskAsync(row);
        //                    }
        //                }
        //            }
        //        }
                
        //        // Логируем успешное обновление
        //        await _logger.LogEventAsync($"Обновлены данные norm_raskr для annId={_newAnnId}", "SaveNormRaskrData");
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, "Ошибка при сохранении данных в таблицу norm_raskr");
        //        throw new Exception($"Ошибка при сохранении данных в таблицу norm_raskr: {ex.Message}");
        //    }
        //}

        /// <summary>
        /// Сохраняет данные в таблицу norm_kont
        /// </summary>
        private async Task SaveNormKontData()
        {
            try
            {
                // Перебираем все строки в гриде norm_kont
                GridView view = gridView3;
                for (int i = 0; i < view.RowCount; i++)
                {
                    if (view.IsValidRowHandle(i))
                    {
                        DataRowView row = view.GetRow(i) as DataRowView;
                        if (row != null)
                        {
                            int id = 0;
                            int.TryParse(row["id"]?.ToString(), out id);
                            
                            // Если запись уже существует, обновляем ее
                            if (id > 0)
                            {
                                string updateQuery = @"
                                    UPDATE norm_kont 
                                    SET annid = @annid, 
                                        kod_op = @kod_op, 
                                        text = @text, 
                                        razryad = @razryad, 
                                        kol = @kol, 
                                        obor = @obor, 
                                        norma = @norma
                                    WHERE id = @id";
                                    
                                    Dictionary<string, object> parameters = new Dictionary<string, object>
                                    {
                                        { "@annid", _newAnnId },
                                        { "@kod_op", row["kod_op"] },
                                        { "@text", row["text"] },
                                        { "@razryad", row["razryad"] },
                                        { "@kol", row["kol"] },
                                        { "@obor", row["obor"] },
                                        { "@norma", row["norma"] },
                                        { "@id", id }
                                    };
                                    
                                    await _dbHelper.ExecuteNonQueryAsync(updateQuery, parameters);
                            }
                            // Если это новая запись, добавляем ее
                            else
                            {
                                string insertQuery = @"
                                    INSERT INTO norm_kont (annid, kod_op, text, razryad, kol, obor, norma)
                                    VALUES (@annid, @kod_op, @text, @razryad, @kol, @obor, @norma);
                                    SELECT SCOPE_IDENTITY()";
                                    
                                    Dictionary<string, object> parameters = new Dictionary<string, object>
                                    {
                                        { "@annid", _newAnnId },
                                        { "@kod_op", row["kod_op"] },
                                        { "@text", row["text"] },
                                        { "@razryad", row["razryad"] },
                                        { "@kol", row["kol"] },
                                        { "@obor", row["obor"] },
                                        { "@norma", row["norma"] }
                                    };
                                    
                                    object result = await _dbHelper.ExecuteScalarAsync(insertQuery, parameters);
                                    if (result != null && result != DBNull.Value)
                                    {
                                        row["id"] = Convert.ToInt32(result);
                                    }
                            }
                        }
                    }
                }
                
                // Логируем успешное обновление
                await _logger.LogEventAsync($"Обновлены данные norm_kont для annId={_newAnnId}", "SaveNormKontData");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при сохранении данных в таблицу norm_kont");
                throw new Exception($"Ошибка при сохранении данных в таблицу norm_kont: {ex.Message}");
            }
        }

        /// <summary>
        /// Сохраняет данные в таблицу norm_dop_obr
        /// </summary>
        private async Task SaveNormDopObrData()
        {
            try
            {
                // Перебираем все строки в гриде norm_dop_obr
                GridView view = gridView4;
                for (int i = 0; i < view.RowCount; i++)
                {
                    if (view.IsValidRowHandle(i))
                    {
                        DataRowView row = view.GetRow(i) as DataRowView;
                        if (row != null)
                        {
                            int id = 0;
                            int.TryParse(row["id"]?.ToString(), out id);
                            
                            // Если запись уже существует, обновляем ее
                            if (id > 0)
                            {
                                string updateQuery = @"
                                    UPDATE norm_dop_obr 
                                    SET annid = @annid, 
                                        kod_op = @kod_op, 
                                        text = @text, 
                                        razryad = @razryad, 
                                        kol = @kol, 
                                        obor = @obor, 
                                        norma = @norma,
                                        kod_podr = @kod_podr,
                                        kod_ob = @kod_ob
                                    WHERE id = @id";
                                    
                                    Dictionary<string, object> parameters = new Dictionary<string, object>
                                    {
                                        { "@annid", _newAnnId },
                                        { "@kod_op", row["kod_op"] },
                                        { "@text", row["text"] },
                                        { "@razryad", row["razryad"] },
                                        { "@kol", row["kol"] },
                                        { "@obor", row["obor"] },
                                        { "@norma", row["norma"] },
                                        { "@kod_podr", row["kod_podr"] },
                                        { "@kod_ob", row["kod_ob"] },
                                        { "@id", id }
                                    };
                                    
                                    await _dbHelper.ExecuteNonQueryAsync(updateQuery, parameters);
                            }
                            // Если это новая запись, добавляем ее
                            else
                            {
                                string insertQuery = @"
                                    INSERT INTO norm_dop_obr (annid, kod_op, text, razryad, kol, obor, norma, kod_podr, kod_ob)
                                    VALUES (@annid, @kod_op, @text, @razryad, @kol, @obor, @norma, @kod_podr, @kod_ob);
                                    SELECT SCOPE_IDENTITY()";
                                    
                                    Dictionary<string, object> parameters = new Dictionary<string, object>
                                    {
                                        { "@annid", _newAnnId },
                                        { "@kod_op", row["kod_op"] },
                                        { "@text", row["text"] },
                                        { "@razryad", row["razryad"] },
                                        { "@kol", row["kol"] },
                                        { "@obor", row["obor"] },
                                        { "@norma", row["norma"] },
                                        { "@kod_podr", row["kod_podr"] },
                                        { "@kod_ob", row["kod_ob"] }
                                    };
                                    
                                    object result = await _dbHelper.ExecuteScalarAsync(insertQuery, parameters);
                                    if (result != null && result != DBNull.Value)
                                    {
                                        row["id"] = Convert.ToInt32(result);
                                    }
                            }
                        }
                    }
                }
                
                // Логируем успешное обновление
                await _logger.LogEventAsync($"Обновлены данные norm_dop_obr для annId={_newAnnId}", "SaveNormDopObrData");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при сохранении данных в таблицу norm_dop_obr");
                throw new Exception($"Ошибка при сохранении данных в таблицу norm_dop_obr: {ex.Message}");
            }
        }

        /// <summary>
        /// Сохраняет выбранные значения дизайнера и конструктора в базу данных
        /// </summary>
        private async Task SaveDesignerAndConstructorSelections()
        {
            try
            {
                int selectedDesignerId = 0;
                int selectedConstructorId = 0;
                
                // Получаем выбранный ID дизайнера
                if (constructorComboBox.SelectedItem != null)
                {
                    var selectedDesigner = (KeyValuePair<int, string>)constructorComboBox.SelectedItem;
                    selectedDesignerId = selectedDesigner.Key;
                }
                
                // Получаем выбранный ID конструктора
                if (designerComboBox.SelectedItem != null)
                {
                    var selectedConstructor = (KeyValuePair<int, string>)designerComboBox.SelectedItem;
                    selectedConstructorId = selectedConstructor.Key;
                }
                
                // Обновляем данные в базе данных
                if (_newAnnId > 0)
                {
                    string updateQuery = @"
                        UPDATE art_norm_n 
                        SET diz = @diz, constr = @constr 
                        WHERE annId = @annId";
                        
                    Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@diz", selectedDesignerId },
                        { "@constr", selectedConstructorId },
                        { "@annId", _newAnnId }
                    };
                    
                    await _dbHelper.ExecuteNonQueryAsync(updateQuery, parameters);
                    
                    // Логируем успешное обновление
                    await _logger.LogEventAsync($"Обновлены данные дизайнера ({selectedDesignerId}) и конструктора ({selectedConstructorId}) для annId={_newAnnId}", "SaveDesignerAndConstructorSelections");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при сохранении данных о дизайнере и конструкторе");
                MessageBox.Show($"Ошибка при сохранении данных о дизайнере и конструкторе: {ex.Message}", 
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buffer_Click(object sender, EventArgs e)
        {
            await GridHelper.LoadGridControlDataAsync(gridControl5, normraszBindingSource, await _artNormService.GetRelatedNormRasz(_bufferWorkDivision));
            await GridHelper.LoadGridControlDataAsync(gridControl2, normraskBindingSource, await _artNormService.GetRelatedNormRask(_bufferWorkDivision));
            await GridHelper.LoadGridControlDataAsync(gridControl3, normkontBindingSource, await _artNormService.GetRelatedNormKont(_bufferWorkDivision));
            await GridHelper.LoadGridControlDataAsync(gridControl4, normdopobrBindingSource, await _artNormService.GetRelatedNormDopObr(_bufferWorkDivision));

            // Получаем данные из ANN для _bufferWorkDivision
            var annData = await _artNormService.GetArtNormById(_bufferWorkDivision);
            if (annData != null)
            {
                // Заполняем текстовые поля данными из ANN
                groupTextBox.Text = annData.Group;
                ModelTextBox.Text = annData.Mod;
                NameTextBox.Text = annData.Articul;
                SecTimeTextBox.Text = annData.Sek.ToString();
                if (annData.DataSozd.HasValue)
                {
                    dateCreate.Value = annData.DataSozd.Value;
                }

                // Если есть RichTextBox для комментария, заполняем его
                if (richTextBox1 != null)
                {
                    richTextBox1.Text = annData.Komment;
                }
                
                // Заполняем комбобоксы данными дизайнера и конструктора
                await LoadDesignerAndConstructorData(annData);
            }
            else
            {
                await _logger.LogErrorAsync(new Exception($"Не удалось получить данные ANN для ID {_bufferWorkDivision}"), "Ошибка загрузки данных ANN в буфер");
            }
        }

        // Обработчики событий для радиокнопок поиска
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // Вызываем очистку поиска при смене радиокнопки
            GridHelper.OnSearchRadioButtonChanged(gridView2);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // Вызываем очистку поиска при смене радиокнопки
            GridHelper.OnSearchRadioButtonChanged(gridView2);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            // Вызываем очистку поиска при смене радиокнопки
            GridHelper.OnSearchRadioButtonChanged(gridView2);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            // Вызываем очистку поиска при смене радиокнопки
            GridHelper.OnSearchRadioButtonChanged(gridView2);
        }
    }
}
