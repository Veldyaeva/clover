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
        private BindingList<NormKont> _normKontList;
        private BindingSource _normKontBindingSource;
        private BindingList<NormDopObr> _normDopObrList;
        private BindingSource _normDopObrBindingSource;
        // Кэш для данных дизайнеров/конструкторов, чтобы не загружать их повторно
        private static DataTable _cachedFioData;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Id новой записи (либо старый Id, если архив+копия)</param>
        /// <param name="bufferWorkDivision">Id из буфера (либо новый, если арх+копия)</param>
        /// <param name="mode">режим</param>
        public TeamWork_AdvanceTW(int id, int bufferWorkDivision, int mode)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _artNormService = new ArtNormService(_dbHelper);
            ThemeManager.UpdateTheme(this);

            _newAnnId = id;
            _bufferWorkDivision = bufferWorkDivision;
            _mode = mode;

            // Включаем inplace редактирование для gridView3 и gridView4
            gridViewKont.OptionsBehavior.EditingMode = GridEditingMode.Inplace;
            gridViewDopObr.OptionsBehavior.EditingMode = GridEditingMode.Inplace;
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

                await WorkDivisionLoadAsync(_newAnnId);

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
                    case (int)Mode.NewWorkDivision: this.Text = "Добавить предварительное"; break;
                    case (int)Mode.ArchAndCopy: this.Text = "Архив+копия"; break;
                    case (int)Mode.Archive: this.Text = "В архив"; break;
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
            #region old
            //try
            //{
            //    Task loadNormRaszTask = Task.Run(async () =>
            //    {
            //        var normRaszData = await _artNormService.GetRelatedNormRasz(_bufferWorkDivision);

            //        await this.InvokeAsync(() =>
            //        {
            //            _normRaszList.Clear();
            //            foreach (DataRow row in normRaszData.Rows)
            //            {
            //                var normRasz = new NormRasz();
            //                foreach (DataColumn col in normRaszData.Columns)
            //                {
            //                    var prop = typeof(NormRasz).GetProperty(col.ColumnName);
            //                    if (prop != null && row[col] != DBNull.Value)
            //                        prop.SetValue(normRasz, Convert.ChangeType(row[col], prop.PropertyType));
            //                }
            //                _normRaszList.Add(normRasz);
            //            }
            //            _normRaszBindingSource.ResetBindings(false);
            //        });
            //    });

            //    Task loadNormRaskTask = Task.Run(async () =>
            //    {
            //        var normRaskData = await _artNormService.GetRelatedNormRask(_bufferWorkDivision);
            //        await this.InvokeAsync(() =>
            //        {
            //            _normRaskList.Clear();
            //            foreach (DataRow row in normRaskData.Rows)
            //            {
            //                var normRask = new NormRask();
            //                foreach (DataColumn col in normRaskData.Columns)
            //                {
            //                    var prop = typeof(NormRask).GetProperty(col.ColumnName);
            //                    if (prop != null && row[col] != DBNull.Value)
            //                        prop.SetValue(normRask, Convert.ChangeType(row[col], prop.PropertyType));
            //                }
            //                _normRaskList.Add(normRask);
            //            }
            //            _normRaskBindingSource.ResetBindings(false);
            //        });
            //    });

            //    Task loadNormKontTask = Task.Run(async () =>
            //    {
            //        var normKontData = await _artNormService.GetRelatedNormKont(_bufferWorkDivision);
            //        await this.InvokeAsync(() =>
            //        {
            //            _normKontList.Clear();
            //            foreach (DataRow row in normKontData.Rows)
            //            {
            //                var normKont = new NormKont();
            //                foreach (DataColumn col in normKontData.Columns)
            //                {
            //                    var prop = typeof(NormKont).GetProperty(col.ColumnName);
            //                    if (prop != null && row[col] != DBNull.Value)
            //                        prop.SetValue(normKont, Convert.ChangeType(row[col], prop.PropertyType));
            //                }
            //                _normKontList.Add(normKont);
            //            }
            //            _normKontBindingSource.ResetBindings(false);
            //        });
            //    });

            //    Task loadNormDopObrTask = Task.Run(async () =>
            //    {
            //        var normDopObrData = await _artNormService.GetRelatedNormDopObr(_bufferWorkDivision);
            //        await this.InvokeAsync(() =>
            //        {
            //            _normDopObrList.Clear();
            //            foreach (DataRow row in normDopObrData.Rows)
            //            {
            //                var normDopObr = new NormDopObr();
            //                foreach (DataColumn col in normDopObrData.Columns)
            //                {
            //                    var prop = typeof(NormDopObr).GetProperty(col.ColumnName);
            //                    if (prop != null && row[col] != DBNull.Value)
            //                        prop.SetValue(normDopObr, Convert.ChangeType(row[col], prop.PropertyType));
            //                }
            //                _normDopObrList.Add(normDopObr);
            //            }
            //            _normDopObrBindingSource.ResetBindings(false);
            //        });
            //    });

            //    Task loadAnnTask = LoadAnnDataAsync();

            //    await Task.WhenAll(loadNormRaszTask, loadNormRaskTask, loadNormKontTask, loadNormDopObrTask, loadAnnTask);
            //}
            //catch (Exception ex)
            //{
            //    await _logger.LogErrorAsync(ex, "Ошибка загрузки данных в буфер");
            //    await this.InvokeAsync(() =>
            //    {
            //        MessageBox.Show("Ошибка загрузки данных. Подробности в логе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    });
            //}
            #endregion
            try
            {
                // Загружаем все данные параллельно
                var raszTask = _artNormService.GetRelatedNormRasz(id);
                var raskTask = _artNormService.GetRelatedNormRask(id);
                var kontTask = _artNormService.GetRelatedNormKont(id);
                var dopObrTask = _artNormService.GetRelatedNormDopObr(id);
                var annTask = LoadAnnDataAsync();

                await Task.WhenAll(raszTask, raskTask, kontTask, dopObrTask, annTask);

                // Преобразуем в списки
                var raszList = ConvertDataTable<NormRasz>(raszTask.Result);
                var raskList = ConvertDataTable<NormRask>(raskTask.Result);
                var kontList = ConvertDataTable<NormKont>(kontTask.Result);
                var dopObrList = ConvertDataTable<NormDopObr>(dopObrTask.Result);

                // Обновляем UI
                await this.InvokeAsync(() =>
                {
                    _normRaszList.Clear();
                    foreach (var item in raszList) _normRaszList.Add(item);
                    _normRaszBindingSource.ResetBindings(false);

                    _normRaskList.Clear();
                    foreach (var item in raskList) _normRaskList.Add(item);
                    _normRaskBindingSource.ResetBindings(false);

                    _normKontList.Clear();
                    foreach (var item in kontList) _normKontList.Add(item);
                    _normKontBindingSource.ResetBindings(false);

                    _normDopObrList.Clear();
                    foreach (var item in dopObrList) _normDopObrList.Add(item);
                    _normDopObrBindingSource.ResetBindings(false);
                });
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка загрузки данных в буфер");
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
                var annData = await _artNormService.GetArtNormDataById(_bufferWorkDivision);
                if (annData != null)
                {
                    await this.InvokeAsync(() =>
                    {
                        nameTextBox.Text = annData.Articul;
                        groupTextBox.Text = annData.Group;
                        modelTextBox.Text = annData.Mod;
                        secTimeTextBox.Text = annData.Sek.ToString();
                        if (annData.Diz > 0)
                        {
                            try { designerComboBox.SelectedValue = annData.Diz; }
                            catch { /* логирование */ }
                        }
                        if (annData.Constr > 0)
                        {
                            try { constructorComboBox.SelectedValue = annData.Constr; }
                            catch { /* логирование */ }
                        }
                    });
                    await _logger.LogEventAsync($"Данные ANN успешно загружены для ID {_bufferWorkDivision}", "LoadAnnDataAsync");
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные ANN для ID {_bufferWorkDivision}", "LoadAnnDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных ANN для ID {_bufferWorkDivision}");
            }
        }

        private async void TeamWork_AdvanceTW_FormClosing(object sender, FormClosingEventArgs e)
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


        private void gridViewRasz_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            var gridView = sender as GridView;
            if (gridView == null)
                return;
            try
            {
            using (var selectionForm = new NormOperNew(_newAnnId))
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
                            gridView.CancelUpdateCurrentRow();
                            gridView.HideEditForm();
                            // Если данные не выбраны, удаляем строку
                            gridView.DeleteRow(e.RowHandle);
                        }
                    }
                    else
                    {
                        gridView.CancelUpdateCurrentRow();
                        gridView.HideEditForm();
                        // Если диалог закрыт не через OK, удаляем строку
                        gridView.DeleteRow(e.RowHandle);

                    }
                }
            }
            finally
            {
                // Восстанавливаем настройки редактирования
                //    gridView.OptionsBehavior.Editable = allowEditing;
            }
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
                normRasz.AnnId = _newAnnId;
                gridViewRasz.UpdateCurrentRow();
            }
        }

        private async void gridViewRasz_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
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

        private async void GridView2_InitNewRow(object sender, InitNewRowEventArgs e)
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
                using (var selectionForm = new norm_raskrNew(_bufferWorkDivision))
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
                        gridControlRaskr.RefreshDataSource();
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
                            await _artNormService.UpdateAnnId("art_norm_n", _bufferWorkDivision, fieldName, selectedId);
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
