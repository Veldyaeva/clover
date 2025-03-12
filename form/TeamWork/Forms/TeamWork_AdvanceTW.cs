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

        private void GridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            using (var selectionForm = new NormOperNew())
            {
                if (selectionForm.ShowDialog() == DialogResult.OK)
                {
                    var selectedData = selectionForm.SelectedRowData;
                    if (selectedData != null)
                    {
                        // Заполняем значения в текущей новой строке
                        gridView5.SetRowCellValue(e.RowHandle, "AnnId", _newAnnId);
                        gridView5.SetRowCellValue(e.RowHandle, "KodO", selectedData.KodO);
                        gridView5.SetRowCellValue(e.RowHandle, "Text", selectedData.Text);
                        gridView5.SetRowCellValue(e.RowHandle, "Spec", selectedData.Spec);
                        gridView5.SetRowCellValue(e.RowHandle, "Razryad", selectedData.Razryad);
                        gridView5.SetRowCellValue(e.RowHandle, "Obor", selectedData.Obor);
                        gridView5.SetRowCellValue(e.RowHandle, "KodProizv", selectedData.KodProizv);
                        gridView5.SetRowCellValue(e.RowHandle, "Kod", selectedData.Kod);
                        gridView5.SetRowCellValue(e.RowHandle, "N1", selectedData.N1);
                        gridView5.SetRowCellValue(e.RowHandle, "Sek", selectedData.Sek);
                        gridView5.SetRowCellValue(e.RowHandle, "KodPodr", selectedData.KodPodr);
                        gridView5.SetRowCellValue(e.RowHandle, "KodOb", selectedData.KodOb);
                    }
                    else
                    {
                        // Если данные не выбраны, удаляем строку
                        gridView5.DeleteRow(e.RowHandle);
                    }
                }
                else
                {
                    // Если диалог отменен, удаляем строку
                    gridView5.DeleteRow(e.RowHandle);
                }
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
