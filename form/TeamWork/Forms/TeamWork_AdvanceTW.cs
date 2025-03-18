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

namespace SewingProduction.form
{
    public partial class TeamWork_AdvanceTW : CustomForm
    {
        private readonly ArtNormService _artNormService;
        private int _bufferWorkDivision;
        private readonly DatabaseHelper _dbHelper;
        private int _newAnnId = -1;
        private readonly ILogger _logger = new FileLogger();
        private int _mode;

        private BindingList<NormRasz> _normRaszList;
        private BindingSource _normRaszBindingSource;

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
            
            // Подписываемся на событие закрытия формы
            this.FormClosing += TeamWork_AdvanceTW_FormClosing;
        }

        private void InitializeBindings()
        {
            _normRaszList = new BindingList<NormRasz>();
            _normRaszBindingSource = new BindingSource { DataSource = _normRaszList };
            gridControl5.DataSource = _normRaszBindingSource;

            // Настраиваем отображение колонок
            SetupGridColumns();

            // Настраиваем обработчики
            gridView5.InitNewRow += GridView5_InitNewRow;
            gridView5.RowUpdated += GridView5_RowUpdated;
            gridView5.ValidateRow += GridView5_ValidateRow;
            gridView5.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
        }

        private void SetupGridColumns()
        {
            gridView5.Columns.Clear();
            
            gridView5.Columns.AddVisible("KodO", "Код операции");
            gridView5.Columns.AddVisible("Text", "Текст");
            gridView5.Columns.AddVisible("Spec", "Специальность");
            gridView5.Columns.AddVisible("Razryad", "Разряд");
            gridView5.Columns.AddVisible("Obor", "Оборудование");
            gridView5.Columns.AddVisible("KodProizv", "Код производства");
            gridView5.Columns.AddVisible("Kod", "Код");
            gridView5.Columns.AddVisible("N1", "Норма");
            gridView5.Columns.AddVisible("Sek", "Секунды");
            gridView5.Columns.AddVisible("KodPodr", "Код подразделения");
            gridView5.Columns.AddVisible("KodOb", "Код оборудования");

            // Скрываем служебные поля
            if (gridView5.Columns["nrId"] != null)
                gridView5.Columns["nrId"].Visible = false;
            if (gridView5.Columns["AnnId"] != null)
                gridView5.Columns["AnnId"].Visible = false;

            // Настраиваем опции редактирования
            gridView5.OptionsBehavior.EditingMode = GridEditingMode.EditForm;
            gridView5.OptionsEditForm.EditFormColumnCount = 1;
            gridView5.OptionsEditForm.PopupEditFormWidth = 400;
            gridView5.OptionsView.ShowGroupPanel = false;
        }

        private void SetupGridColumnsRask()
        {
            //gridView2.Columns.Clear();

            //gridView2.Columns.AddVisible("KodO", "Код операции");
            //gridView2.Columns.AddVisible("Text", "Текст");
            //gridView2.Columns.AddVisible("Razryad", "Разряд");
            //gridView2.Columns.AddVisible("Obor", "Оборудование");
            //gridView2.Columns.AddVisible("Kod", "Код");
            //gridView2.Columns.AddVisible("n1", "Норма");
            //gridView2.Columns.AddVisible("sek", "Секунды");
            //gridView2.Columns.AddVisible("n", "Количество");
            //gridView2.Columns.AddVisible("n_ch", "Количество человек");
            //gridView2.Columns.AddVisible("seb", "Себестоимость");
            //gridView2.Columns.AddVisible("seb_s", "Себестоимость суммарная");

            // Скрываем служебные поля
            if (gridView2.Columns["id"] != null)
                gridView2.Columns["id"].Visible = false;
            if (gridView2.Columns["annId"] != null)
                gridView2.Columns["annId"].Visible = false;

            // Настраиваем опции редактирования
            gridView2.OptionsBehavior.EditingMode = GridEditingMode.EditForm;
            gridView2.OptionsEditForm.EditFormColumnCount = 1;
            gridView2.OptionsEditForm.PopupEditFormWidth = 400;
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

        private void GridView2_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
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
                    
                    if (result == DialogResult.OK && selectionForm.SelectedData != null)
                    {
                        var selectedDataList = selectionForm.SelectedData;

                        // Вставляем данные в gridView2
                        foreach (var normRask in selectedDataList)
                        {
                            normRask.annId = _newAnnId;
                            _normRaskList.Add(normRask); // Добавляем в список
                        }

                        // Обновляем привязку данных
                        _normRaskBindingSource.ResetBindings(false);

                        // Сохраняем данные в базу данных
                        foreach (var normRask in selectedDataList)
                        {
                            _artNormService.InsertNormRaskAsync(normRask); 
                        }
                        gridView.UpdateCurrentRow();
                    }
                    else
                    {
                        // Если пользователь отменил выбор или не выбрал данные, удаляем строку
                        gridView.DeleteRow(gridView.GetRowHandle(e.RowHandle));
                    }
                }
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
                normRask.annId = _newAnnId;
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
                    normRask.annId = _newAnnId;

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

        private async Task bufferLoad()
        {
            try
            {
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
                    }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка загрузки данных в буфер");
                MessageBox.Show("Ошибка загрузки данных. Подробности в логе.", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
