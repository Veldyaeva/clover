using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.form;
using SewingProduction.Models;
using SewingProduction.Services;
using SewingProduction.Helpers;
using DevExpress.XtraExport.Helpers;
using System.Threading.Tasks;
using DevExpress.Data.Filtering;
using DevExpress.CodeParser;
using SewingProduction.form.TeamWork;
using DevExpress.XtraBars.Customization;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using NLog;
using System.Collections;
using System.Drawing;
using DevExpress.XtraCharts;
using SewingProduction.Interfaces;
using System.IO;
using DevExpress.ClipboardSource.SpreadsheetML;
using System.Linq;


namespace SewingProduction.Forms
{
    public partial class TeamWork : CustomForm
    {
        private readonly DatabaseHelper _dbHelper; 
        private readonly ArtNormService _artNormService;
        private int selectedRowHandle = -1;
        private readonly ILogger _logger =new FileLogger();
        private readonly GridHelper _gridHelper = new GridHelper();
        private readonly SplitContainerHelper _splitContainerHelper = new SplitContainerHelper();
        private int bufferId =0;
        private  BindingList<ArtNormN> _bindingList = new BindingList<ArtNormN>();
        private BindingSource _bindingSource =new BindingSource();

        public TeamWork()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _artNormService = new ArtNormService(_dbHelper);

            ThemeManager.UpdateTheme(this);
            
            // Set up single row selection for both grid controls
            var view7 = customGridControl1.MainView as GridView;
            var view8 = customGridControl2.MainView as GridView;
            
            if (view7 != null)
            {
                view7.OptionsSelection.MultiSelect = false;
                view7.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            }
            
            if (view8 != null)
            {
                view8.OptionsSelection.MultiSelect = false;
                view8.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            }

            this.gridView7.CellValueChanged += (s, e) => GridView_CellValueChanged<MyDataART>(customGridControl1, e);
            this.gridView8.CellValueChanged += (s, e) => GridView_CellValueChanged<MyDataANN>(customGridControl2, e);

            // _bindingList = new BindingList<ArtNormN>();
            _bindingSource.DataSource = _bindingList; //= new BindingSource { DataSource = _bindingList };
            ANNgridControl.DataSource = _bindingSource;

        }

        private async void TeamWork_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Save grid settings
                _gridHelper.SaveGridViewSettings(ANNgridView, "ANNgridViewLayout.xml");
                _gridHelper.SaveGridViewSettings(gridView1, "gridView1Layout.xml");
                _gridHelper.SaveGridViewSettings(gridView2, "gridView2Layout.xml");
                _gridHelper.SaveGridViewSettings(gridView4, "gridView4Layout.xml");
                _gridHelper.SaveGridViewSettings(gridView5, "gridView5Layout.xml");
                _gridHelper.SaveGridViewSettings(gridView6, "gridView6Layout.xml");
                _gridHelper.SaveGridViewSettings(gridView7, "gridView7Layout.xml");
                _gridHelper.SaveGridViewSettings(gridView8, "gridView8Layout.xml");
                _gridHelper.SaveGridViewSettings(gridView9, "gridView9Layout.xml");
                _gridHelper.SaveGridViewSettings(gridView10, "gridView10Layout.xml");
                _gridHelper.SaveGridViewSettings(gridView11, "gridView11Layout.xml");
                _gridHelper.SaveGridViewSettings(gridView12, "gridView12Layout.xml");
                _gridHelper.SaveGridViewSettings(customGridControl1.MainView as GridView, "customGridControl1Layout.xml");
                _gridHelper.SaveGridViewSettings(customGridControl2.MainView as GridView, "customGridControl2Layout.xml");
                _gridHelper.SaveGridViewSettings(customGridControl3.MainView as GridView, "customGridControl3Layout.xml");
                _gridHelper.SaveGridViewSettings(customGridControl4.MainView as GridView, "customGridControl4Layout.xml");
                _gridHelper.SaveGridViewSettings(customGridControl5.MainView as GridView, "customGridControl5Layout.xml");
                _gridHelper.SaveGridViewSettings(customGridControl6.MainView as GridView, "customGridControl6Layout.xml");

                // Save split container settings
                _splitContainerHelper.SaveSplitContainerSettings(splitContainerControl1, "splitContainer1Layout.xml");
                _splitContainerHelper.SaveSplitContainerSettings(splitContainerControl2, "splitContainer2Layout.xml");
                _splitContainerHelper.SaveSplitContainerSettings(splitContainerControl3, "splitContainer3Layout.xml");
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Error in TeamWork_FormClosing");
            }
        }

        #region Загрузка данных

        private async void TeamWorkForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Load split container settings
                _splitContainerHelper.LoadSplitContainerSettings(splitContainerControl1, "splitContainer1Layout.xml");
                _splitContainerHelper.LoadSplitContainerSettings(splitContainerControl2, "splitContainer2Layout.xml");
                _splitContainerHelper.LoadSplitContainerSettings(splitContainerControl3, "splitContainer3Layout.xml");

                // Загружаем настройки для всех гридов
                _gridHelper.LoadGridViewSettings(ANNgridView, "ANNgridViewLayout.xml");
                _gridHelper.LoadGridViewSettings(gridView1, "gridView1Layout.xml");
                _gridHelper.LoadGridViewSettings(gridView2, "gridView2Layout.xml");
                _gridHelper.LoadGridViewSettings(gridView4, "gridView4Layout.xml");
                _gridHelper.LoadGridViewSettings(gridView5, "gridView5Layout.xml");
                _gridHelper.LoadGridViewSettings(gridView6, "gridView6Layout.xml");
                _gridHelper.LoadGridViewSettings(gridView7, "gridView7Layout.xml");
                _gridHelper.LoadGridViewSettings(gridView8, "gridView8Layout.xml");
                _gridHelper.LoadGridViewSettings(gridView9, "gridView9Layout.xml");
                _gridHelper.LoadGridViewSettings(gridView10, "gridView10Layout.xml");
                _gridHelper.LoadGridViewSettings(gridView11, "gridView11Layout.xml");
                _gridHelper.LoadGridViewSettings(gridView12, "gridView12Layout.xml");
                _gridHelper.LoadGridViewSettings(customGridControl1.MainView as GridView, "customGridControl1Layout.xml");
                _gridHelper.LoadGridViewSettings(customGridControl2.MainView as GridView, "customGridControl2Layout.xml");
                _gridHelper.LoadGridViewSettings(customGridControl3.MainView as GridView, "customGridControl3Layout.xml");
                _gridHelper.SaveGridViewSettings(customGridControl4.MainView as GridView, "customGridControl4Layout.xml");
                _gridHelper.LoadGridViewSettings(customGridControl5.MainView as GridView, "customGridControl5Layout.xml");
                _gridHelper.SaveGridViewSettings(customGridControl6.MainView as GridView, "customGridControl6Layout.xml");

                // Подписываемся на событие смены строки в customGridControl5
                var view5 = customGridControl5.MainView as GridView;
                if (view5 != null)
                {
                    view5.FocusedRowChanged += GridView5_FocusedRowChanged;
                }

            await LoadWorkDivisions();
            await CurrentWorks_Load();
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Error in TeamWorkForm_Load");
            }
        }

        /// <summary>
        /// Обработчик смены выбранной строки в customGridControl5
        /// </summary>
        private async void GridView5_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                var view = sender as GridView;
                if (view == null || e.FocusedRowHandle < 0) return;

                // Получаем значение nzp из выбранной строки
                int nzp = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "kolNZP", 0);
                
                // Обновляем видимость кнопки в зависимости от значения nzp
                //customButton3.Enabled = nzp <= 0;
                ButtonUnboundWd.Enabled = nzp <= 0;

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при обработке смены строки в GridView5");
            }
        }



        /// <summary>
        /// Загрузка вкладки "Список РТ"
        /// </summary>
        /// <returns></returns>

        private async Task LoadWorkDivisions()
        {
            try
            {
                // Получаем данные
                var data = await _artNormService.GetArtNormData();

                if (data != null && data.Count > 0)
                {
                    // Отключаем обновление UI во время загрузки данных
                    ANNgridControl.BeginUpdate();
                    try
                    {
                        // Настраиваем отображение GridView
                        ANNgridView.OptionsView.EnableAppearanceEvenRow = true;
                        ANNgridView.OptionsView.EnableAppearanceOddRow = true;
                        ANNgridView.OptionsView.ShowAutoFilterRow = true;
                        ANNgridView.OptionsView.ShowGroupPanel = false;
                        ANNgridView.OptionsView.ShowIndicator = false;
                        ANNgridView.OptionsView.ShowPreview = false;
                        
                        // Заменяем _bindingList на новый BindingList с данными
//                        _bindingSource.DataSource = new BindingList<ArtNormN>(data);
                        _bindingList = new BindingList<ArtNormN>(data);
                        _bindingSource.DataSource = _bindingList;

                        // Обновляем источник данных
                        _bindingSource.ResetBindings(false);
                        
                        // Применяем фильтры
                        filterTable();
                    }
                    finally
                    {
                        // Включаем обновление UI
                        ANNgridControl.EndUpdate();
                    }
                }
                else
                {
                    MessageBox.Show("Нет данных для загрузки.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                await _logger.LogEventAsync("Данные загружены успешно", "LoadData");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных в текущие работы: {ex.Message}");
            }
        }
        private void RefreshData()
        {
            SafeInvoke(ANNgridControl, () =>
            {
                ANNgridControl.BeginUpdate();
                try
                {
                _bindingSource.ResetBindings(false);
                ANNgridControl.RefreshDataSource();
                ANNgridView.RefreshData();
                    filterTable();
                }
                finally
                {
                    ANNgridControl.EndUpdate();
                }
            });
        }

        private void SafeInvoke(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        


        /// <summary>
        /// Загружает список разделений труда (РТ) для указанного артикула или кода.
        /// </summary>
        /// <param name="kod">Код артикула</param>
        /// <param name="articul">Название артикула</param>
        /// <returns>Список разделений труда (BindingList&lt;MyDataANN&gt;)</returns>
        /// 
        private async Task<BindingList<MyDataANN>> LoadWorksbyArt(int kod, string articul)
        {
            try
            {
                List<ArtNormN> relatedData;
                bool loadAll = loadAllCheckBox.Checked;
                // Если включен чекбокс "Загрузить все"
                //relatedData = //ConvertDataTableToList<ArtNormN>(await _artNormService.GetArtNormDataCurrent(kod, loadAll));
                relatedData = await _artNormService.GetArtNormDataCurrent(kod, loadAll);
                if (!loadAll)
                { // Если артикул содержит "-", фильтруем по его первой части
                    int dashIndex = articul.IndexOf("-");
                    if (dashIndex > 0)
                    {
                        List<ArtNormN> partialData = await _artNormService.GetArtNormDataCurrent(articul.Substring(0, dashIndex));
                        if (partialData != null)
                            relatedData.AddRange(partialData);
                    }
                }

                BindingList<MyDataANN> myDataList = ConvertToMyDataAnn(relatedData);

                await _logger.LogEventAsync($"Успешная загрузка РТ для кода {kod} и артикула {articul}", "LoadWorksbyArt");
                return myDataList;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки РТ для кода {kod} и артикула {articul}");
                MessageBox.Show("Ошибка загрузки данных. Подробности в логе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new BindingList<MyDataANN>(); // Возвращаем пустой список в случае ошибки
            }

        }
        BindingList<MyDataANN> ConvertToMyDataAnn(List<ArtNormN> _relatedData)
        {
            // Преобразуем List<ArtNormN> в BindingList<MyDataANN>
                BindingList<MyDataANN> myDataList = new BindingList<MyDataANN>();

            foreach (var item in _relatedData)
                {
                    myDataList.Add(new MyDataANN
                    {
                    AnnId = item.AnnID,
                    Kod = item.Kod,
                    Articul = item.Articul,
                    Status = item.Status,
                    Group = item.Group,
                    Model = item.Mod,
                        IsChecked = false
                    });
                }

                return myDataList;
            }

        //private async Task<BindingList<MyDataANN>> LoadWorksbyArt(int kod, string articul)
        //{
        //    try
        //    {
        //        List<ArtNormN> relatedData;

        //        // Если включен чекбокс "Загрузить все"
        //        if (loadAllCheckBox.Checked)
        //        {
        //            relatedData = await _artNormService.GetArtNormDataCurrent(kod, true);
        //        }
        //        else
        //        {
        //            relatedData = await _artNormService.GetArtNormDataCurrent(kod, false);

        //            // Если артикул содержит "-", фильтруем по его первой части
        //            int dashIndex = articul.IndexOf("-");
        //            if (dashIndex > 0)
        //            {
        //                List<ArtNormN> partialData = await _artNormService.GetArtNormDataCurrent(articul.Substring(0, dashIndex));
        //                foreach (var item in partialData)
        //                { relatedData.Add(item); }
        //            }
        //        }

        //        // Преобразуем DataTable в BindingList<MyDataANN>
        //        BindingList<MyDataANN> myDataList = new BindingList<MyDataANN>();

        //        foreach (var row in myDataList)
        //        {
        //            myDataList.Add(new MyDataANN
        //            {
        //                AnnId = row.AnnId,
        //                Kod = row.Kod,
        //                Articul = row.Articul,
        //                Status = row.Status,
        //                Stat  = row.Stat,
        //                Group = row.Group,
        //                Model = row.Model,
        //                IsChecked = false
        //            });
        //        }
        //        await _logger.LogEventAsync($"Успешная загрузка РТ для кода {kod} и артикула {articul}", "LoadWorksbyArt");
        //        return myDataList;
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка загрузки РТ для кода {kod} и артикула {articul}");
        //        MessageBox.Show("Ошибка загрузки данных. Подробности в логе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return new BindingList<MyDataANN>(); // Возвращаем пустой список в случае ошибки
        //    }
        //}
        private List<T> ConvertDataTableToList<T>(DataTable table) where T : new()
        {
            List<T> list = new List<T>();

            foreach (DataRow row in table.Rows)
            {
                T obj = new T();
                foreach (DataColumn column in table.Columns)
                {
                    var property = typeof(T).GetProperty(column.ColumnName);
                    if (property != null && row[column] != DBNull.Value)
            {
                        property.SetValue(obj, Convert.ChangeType(row[column], property.PropertyType));
                    }
                }
                list.Add(obj);
            }

            return list;
        }

        #region Обработка смены строки

        /// <summary>
        /// Обрабатывает смену выбранной  строки в gridView3 - разделениях труда
        /// Загружает связанные данные в другие таблицы и обновляет UI.
        /// </summary>
        private async void gridView3_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            try
            {
                var view = ANNgridView;

                // Получаем значения из выбранной строки
                string comment = CommonFunctions.GetRowCellValueOrDefault<string>(view, e.FocusedRowHandle, "Komment", "");
                int constructorId = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "Constr", 0);
                int designerId = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "Diz", 0);

                // Log the retrieved IDs
                await _logger.LogEventAsync($"Constructor ID: {constructorId}, Designer ID: {designerId}", "RowChange");

                // Устанавливаем значения в соответствующие элементы управления
                commentRichTextBox.Text = comment;

                // Fetch and set the constructor's full name
                string constructorName = await _artNormService.GetEmployeeFullName(constructorId);
                constructorTextBox.Text = constructorName;

                // Fetch and set the designer's full name
                string designerName = await _artNormService.GetEmployeeFullName(designerId);
                designerTextBox.Text = designerName;

                int annId = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "AnnID", 0);
                //UpdateRelatedData(annId);
                await LoadRelatedData(annId);

                string kod = CommonFunctions.GetRowCellValueOrDefault<string>(view, e.FocusedRowHandle, "Kod", "");
                int o = 0;
                try { o = Convert.ToInt32(kod); }
                catch (Exception ex) { await _logger.LogErrorAsync(ex, "опять КОД это строка"); return; }
                finally { if (o > 0) LoadGridControlData(pictureBox1, o); }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при смене выбранной строки в gridView3");
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Загрузка данных LoadGridControlData

        /// <summary>
        /// Загружает изображение в PictureBox по идентификатору разделения труда.
        /// </summary>
        /// <param name="pictureBox">Целевой PictureBox</param>
        /// <param name="kod">Идентификатор разделения труда</param>
        private async void LoadGridControlData(PictureBox pictureBox, int kod)
        {
            try
            {
                DataTable dt = await _artNormService.GetImage(kod);
                if (dt != null && dt.Rows.Count > 0)
                {
                    pictureBox.ImageLocation = dt.Rows[0]["pathpict"].ToString();
                }
                else
                {
                    pictureBox.Image = null;
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки изображения для kod = {kod}");
                pictureBox.Image = null;
            }
        }

        /// <summary>
        /// Применяет фильтр к GridView на основе annId.
        /// </summary>
        /// <param name="grid">GridControl, в котором нужно применить фильтр</param>
        /// <param name="source">источник данных</param>
        /// <param name="_annId">Идентификатор разделения труда</param>
        private async void LoadGridControlData(GridControl grid, BindingSource source, int _annId)
        {
            try
            {
                string filter = "annId = " + _annId;
                GridView view = (GridView)grid.Views[0];

                view.BeginUpdate();
                view.ActiveFilterString = filter;
                view.EndUpdate();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка фильтрации данных для annId = {_annId}");
            }
        }
        #endregion


        #endregion

        #region Вспомогательные методы

        private void gridControl2_Leave(object sender, EventArgs e)
        {
            selectedRowHandle = ANNgridView.FocusedRowHandle;
        }

        private void gridControl2_GotFocus(object sender, EventArgs e)
        {
            if (selectedRowHandle >= 0)
            {
                gridView1.FocusedRowHandle = selectedRowHandle;
                gridView1.SelectRow(selectedRowHandle);
                selectedRowHandle = -1; //Сбрасываем после восстановления выделения
            }
        }

        #endregion

        #region Работа с данными

        private async Task LoadRelatedData(int annId)
        {
            await GridHelper.LoadGridControlDataAsync(gridControl1, normraszBindingSource, await _artNormService.GetRelatedNormRasz(annId));
            //await GridHelper.LoadGridControlDataAsync(gridControl1, normraszBindingSource, _artNormService.GetRelatedNormRasz1(annId));
            await GridHelper.LoadGridControlDataAsync(gridControl3, normraskBindingSource, await _artNormService.GetRelatedNormRask(annId));
            await GridHelper.LoadGridControlDataAsync(gridControl4, normkontBindingSource, await _artNormService.GetRelatedNormKont(annId));
            await GridHelper.LoadGridControlDataAsync(gridControl5, normdopobrBindingSource, await _artNormService.GetRelatedNormDopObr(annId));
            await GridHelper.LoadGridControlDataAsync(customGridControl5, sparticulBindingSource, await _artNormService.GetRelatedSpArt(annId));

            UpdateNZPStatus();
        }

        private async void UpdateNZPStatus()
        {
            try
            {
                var view = customGridControl5.MainView as GridView;
                if (view == null || view.FocusedRowHandle < 0) return;

                int nzp = CommonFunctions.GetRowCellValueOrDefault<int>(view, view.FocusedRowHandle, "kolNZP", 0);
                //customButton3.Enabled = nzp <= 0;
                ButtonUnboundWd.Enabled = nzp <= 0;

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка обновления статуса НЗП");
                MessageBox.Show($"Ошибка при обновлении NZP: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void gridView3_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;

            try
            {
                //view.SetRowCellValue(e.RowHandle, "annId", 0);
                //view.SetRowCellValue(e.RowHandle, "kod", 0);
                //view.SetRowCellValue(e.RowHandle, "articul", string.Empty);
                //view.SetRowCellValue(e.RowHandle, "status", 1);
                //view.SetRowCellValue(e.RowHandle, "group", string.Empty);
                //view.SetRowCellValue(e.RowHandle, "model", string.Empty);
                //view.SetRowCellValue(e.RowHandle, "stat", "Новый");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при добавлении новой строки");
                MessageBox.Show($"Ошибка при добавлении новой строки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridView3_ColumnFilterChanged(object sender, EventArgs e)
        {
           // searchControl1.ClearFilter();
        }



        /// <summary>
        /// Обновляет данные в связанных таблицах при смене разделения труда (РТ).
        /// </summary>
        /// <param name="annId">Идентификатор разделения труда</param>
        private async void UpdateRelatedData(int annId)
        {
            try
            {
                await GridHelper.LoadGridControlDataAsync(gridControl1, normraszBindingSource, await _artNormService.GetRelatedNormRasz(annId));
                await GridHelper.LoadGridControlDataAsync(gridControl3, normraskBindingSource, await _artNormService.GetRelatedNormRask(annId));
                await GridHelper.LoadGridControlDataAsync(gridControl4, normkontBindingSource, await _artNormService.GetRelatedNormKont(annId));
                await GridHelper.LoadGridControlDataAsync(gridControl5, normdopobrBindingSource, await _artNormService.GetRelatedNormDopObr(annId));
                await GridHelper.LoadGridControlDataAsync(customGridControl5, sparticulBindingSource, await _artNormService.GetRelatedSpArt(annId));

                UpdateNZPStatus(); // Обновляем статус незавершенного производства
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка обновления данных для annId = {annId}");
                MessageBox.Show($"Ошибка обновления данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region Поиск и фильтрация

        private async void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                string searchText = searchControl1.Text.TrimEnd(' ');
            string columnName = await GridHelper.GetSelectedColumnNameAsync(kode.Checked, articul.Checked, model.Checked, group.Checked);

            if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(searchText))
            {
                    // Создаем фильтр поиска
                    var searchFilter = new FunctionOperator(
                    FunctionOperatorType.Contains,
                    new OperandProperty(columnName),
                    new OperandValue(searchText));
                    
                    // Создаем фильтры на основе состояния чекбоксов
                    CriteriaOperator statusCriteria = null;
                    
                    // Создаем фильтр по статусу
                    if (preliminaryCheckBox.Checked || actualCheckBox.Checked || archiveCheckBox.Checked)
                    {
                        var statusFilters = new List<CriteriaOperator>();
                        
                        if (preliminaryCheckBox.Checked)
                            statusFilters.Add(new BinaryOperator("status", (int)Status.Preliminary));

                        if (actualCheckBox.Checked)
                        {
                            statusFilters.Add(new BinaryOperator("status", (int)Status.Actual));
                            statusFilters.Add(new BinaryOperator("status", (int)Status.PreliminaryArchive));
                        }
                        
                        if (archiveCheckBox.Checked)
                            statusFilters.Add(new BinaryOperator("status", (int)Status.Archive));
                        
                        if (statusFilters.Count > 1)
                        {
                            statusCriteria = new GroupOperator(GroupOperatorType.Or, statusFilters.ToArray());
                        }
                        else if (statusFilters.Count == 1)
                        {
                            statusCriteria = statusFilters[0];
                        }
                    }
                    
                    // Добавляем фильтр по "Не описанные" если выбран
                    if (SortBox.Checked)
                    {
                        var notDescribedFilter = new GroupOperator(
                            GroupOperatorType.And,
                            new BinaryOperator("sek_shv", 0),
                            new BinaryOperator("status", 0, DevExpress.Data.Filtering.BinaryOperatorType.Greater)
                        );
                        
                        if (statusCriteria != null)
                        {
                            statusCriteria = new GroupOperator(
                                GroupOperatorType.And,
                                statusCriteria,
                                notDescribedFilter
                            );
                        }
                        else
                        {
                            statusCriteria = notDescribedFilter;
                        }
                    }
                    
                    // Если есть фильтр статуса, объединяем его с фильтром поиска
                    if (statusCriteria != null)
                    {
                        ANNgridView.ActiveFilterCriteria = new GroupOperator(
                            GroupOperatorType.And,
                            searchFilter,
                            statusCriteria
                        );
                    }
                    else
                    {
                        ANNgridView.ActiveFilterCriteria = searchFilter;
                    }
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при поиске в SearchButton_Click");
                MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }

        private void SearchButton_Click(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            // Вызываем основной метод поиска
            SearchButton_Click(sender, new EventArgs());
        }

        /// <summary>
        /// Фильтрация данных в gridView3 по введенному значению в filterTextBox1.
        /// </summary>
        private async void customButton12_Click(object sender, EventArgs e)
        {
            try
            {
                string filterString = filterTextBox1.Text.Trim(); // Получаем текст из поля ввода
                string columnName = await GridHelper.GetSelectedColumnNameAsync(kode.Checked, articul.Checked, model.Checked, group.Checked); // Определяем, по какой колонке искать

                if (!string.IsNullOrEmpty(filterString) && !string.IsNullOrEmpty(columnName))
                {
                    // Применяем фильтр к gridView3
                    ANNgridView.ActiveFilterCriteria = new DevExpress.Data.Filtering.FunctionOperator(
                        DevExpress.Data.Filtering.FunctionOperatorType.Contains,
                        new DevExpress.Data.Filtering.OperandProperty(columnName),
                        new DevExpress.Data.Filtering.OperandValue(filterString)
                    );
                }
                else
                {
                    MessageBox.Show("Введите значение для поиска и выберите колонку!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при поиске по customButton12_Click");
                MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async Task searchControl1_QueryIsSearchColumn(object sender, DevExpress.XtraEditors.QueryIsSearchColumnEventArgs args)
        {
            string colName = await GridHelper.GetSelectedColumnNameAsync(kode.Checked, articul.Checked, model.Checked, group.Checked);
            args.IsSearchColumn = args.FieldName == colName;
        }

        private async void customCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            int kod = CommonFunctions.GetRowCellValueOrDefault<int>(gridView7, gridView7.FocusedRowHandle, "kod", 0);
            string articul = CommonFunctions.GetRowCellValueOrDefault<string>(gridView7, gridView7.FocusedRowHandle, "articul", "");

            BindingList<MyDataANN> list = loadAllCheckBox.Checked ? 
                await LoadWorksbyArt(0, "") : 
                await LoadWorksbyArt(kod, articul);
            customGridControl2.DataSource = list;//loadAllCheckBox.Checked ? LoadWorksbyArt(0, "") : LoadWorksbyArt(kod, articul);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
         //   new TeamWork_AdvanceTW(bufferWorkDivision, (int)Mode.NewWorkDivision).ShowDialog();
        }

        /// <summary>
        /// Применяет фильтры к данным в gridView3
        /// </summary>
        private void filterTable()
        {
            try
            {
                // Сохраняем текущий фильтр поиска, если он есть
                CriteriaOperator searchFilter = null;
                if (ANNgridView.ActiveFilterCriteria is GroupOperator groupFilter)
                {
                    // Проверяем, есть ли фильтр поиска в группе операторов
                    foreach (var criteria in groupFilter.Operands)
                    {
                        if (criteria is FunctionOperator functionOp && 
                            functionOp.OperatorType == FunctionOperatorType.Contains)
                        {
                            searchFilter = criteria;
                            break;
                        }
                    }
                }
                else if (ANNgridView.ActiveFilterCriteria is FunctionOperator functionFilter && 
                         functionFilter.OperatorType == FunctionOperatorType.Contains)
                {
                    searchFilter = functionFilter;
                }

                // Создаем фильтры на основе состояния чекбоксов
                CriteriaOperator statusCriteria = null;
                GroupOperator statusGroup = null;

                // Создаем фильтр по статусу
                if (preliminaryCheckBox.Checked || actualCheckBox.Checked || archiveCheckBox.Checked)
                {
                    var statusFilters = new List<CriteriaOperator>();
                    
                    if (preliminaryCheckBox.Checked)
                        statusFilters.Add(new BinaryOperator("status", (int)Status.Preliminary));

                    if (actualCheckBox.Checked)
                    {
                        statusFilters.Add(new BinaryOperator("status", (int)Status.PreliminaryArchive));
                        statusFilters.Add(new BinaryOperator("status", (int)Status.Actual));
                    }
                    
                    if (archiveCheckBox.Checked)
                        statusFilters.Add(new BinaryOperator("status", (int)Status.Archive));
                    
                    if (statusFilters.Count > 1)
                    {
                        statusGroup = new GroupOperator(GroupOperatorType.Or, statusFilters.ToArray());
                        statusCriteria = statusGroup;
                    }
                    else if (statusFilters.Count == 1)
                    {
                        statusCriteria = statusFilters[0];
                    }
                }
                
                // Добавляем фильтр по "Не описанные" если выбран
                if (SortBox.Checked)
                {
                    var notDescribedFilter = new GroupOperator(
                        GroupOperatorType.And,
                        new BinaryOperator("sek_shv", 0),
                        new BinaryOperator("status", 0, DevExpress.Data.Filtering.BinaryOperatorType.Greater)
                    );
                    
                    if (statusCriteria != null)
                    {
                        statusCriteria = new GroupOperator(
                            GroupOperatorType.And,
                            statusCriteria,
                            notDescribedFilter
                        );
                    }
                    else
                    {
                        statusCriteria = notDescribedFilter;
                    }
                }

                // Если есть и фильтр поиска, и фильтр статуса
                if (searchFilter != null && statusCriteria != null)
                {
                    ANNgridView.ActiveFilterCriteria = new GroupOperator(
                        GroupOperatorType.And,
                        searchFilter,
                        statusCriteria
                    );
                }
                else if (searchFilter != null)
                {
                    // Только фильтр поиска
                    ANNgridView.ActiveFilterCriteria = searchFilter;
                }
                else if (statusCriteria != null)
                {
                    // Только фильтр статуса
                    ANNgridView.ActiveFilterCriteria = statusCriteria;
                }
                else
                {
                    // Нет фильтров
                    ANNgridView.ActiveFilterString = string.Empty;
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при применении фильтра");
            }
        }

        /// <summary>
        /// Переключение фильтров при изменении чекбоксов
        /// </summary>
        private void Filter_CheckedChanged(object sender, EventArgs e) => filterTable();

        /// <summary>
        /// Обработчик смены выбранного поля поиска при изменении параметров поиска.
        /// </summary>
        private async void search_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // Получаем текст текущего поиска
                string searchText = searchControl1.Text.TrimEnd(' ');
                
                // Очищаем текущий фильтр поиска
                searchControl1.ClearFilter();
                
                // Если есть текст поиска, применяем его к новому выбранному полю
                if (!string.IsNullOrEmpty(searchText))
                {
                    // Создаем фильтр поиска для нового выбранного поля
                    string columnName = await GridHelper.GetSelectedColumnNameAsync(kode.Checked, articul.Checked, model.Checked, group.Checked);
                    if (!string.IsNullOrEmpty(columnName))
                    {
                        var searchFilter = new FunctionOperator(
                            FunctionOperatorType.Contains,
                            new OperandProperty(columnName),
                            new OperandValue(searchText));

                        // Получаем текущий фильтр статусов
                        CriteriaOperator statusFilter = GetStatusFilter();
                        
                        // Если есть фильтр статусов, объединяем его с новым фильтром поиска
                        if (statusFilter != null)
                        {
                            ANNgridView.ActiveFilterCriteria = new GroupOperator(
                                GroupOperatorType.And,
                                searchFilter,
                                statusFilter
                            );
                        }
                        else
                        {
                            ANNgridView.ActiveFilterCriteria = searchFilter;
                        }
                    }
                }
                else
                {
                    // Если нет текста поиска, применяем только фильтр статусов
                    CriteriaOperator statusFilter = GetStatusFilter();
                    if (statusFilter != null)
                    {
                        ANNgridView.ActiveFilterCriteria = statusFilter;
                    }
                    else
                    {
                        ANNgridView.ActiveFilterString = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при обновлении параметров поиска");
            }
        }

        private CriteriaOperator GetStatusFilter()
        {
            // Создаем фильтры на основе состояния чекбоксов
            if (preliminaryCheckBox.Checked || actualCheckBox.Checked || archiveCheckBox.Checked)
            {
                var statusFilters = new List<CriteriaOperator>();
                
                if (preliminaryCheckBox.Checked)
                    statusFilters.Add(new BinaryOperator("status", (int)Status.Preliminary));

                if (actualCheckBox.Checked)
                {
                    statusFilters.Add(new BinaryOperator("status", (int)Status.Actual));
                    statusFilters.Add(new BinaryOperator("status", (int)Status.PreliminaryArchive));
                }
                    if (archiveCheckBox.Checked)
                        statusFilters.Add(new BinaryOperator("status", (int)Status.Archive));
                    
                    if (statusFilters.Count > 1)
                    {
                        statusCriteria = new GroupOperator(GroupOperatorType.Or, statusFilters.ToArray());
                    }
                    else if (statusFilters.Count == 1)
                    {
                        statusCriteria = statusFilters[0];
                    }
                }
                
                // Добавляем фильтр по "Не описанные" если выбран
                if (SortBox.Checked)
                {
                    var notDescribedFilter = new GroupOperator(
                        GroupOperatorType.And,
                        new BinaryOperator("sek_shv", 0),
                        new BinaryOperator("status", 0, DevExpress.Data.Filtering.BinaryOperatorType.Greater)
                    );
                    
                    if (statusCriteria != null)
                    {
                        statusCriteria = new GroupOperator(
                            GroupOperatorType.And,
                            statusCriteria,
                            notDescribedFilter
                        );
                    }
                    else
                    {
                        statusCriteria = notDescribedFilter;
                    }
                }
                
                // Если есть фильтр статуса, объединяем его с фильтром поиска
                if (statusCriteria != null)
                {
                    ANNgridView.ActiveFilterCriteria = new GroupOperator(
                        GroupOperatorType.And,
                        searchFilter,
                        statusCriteria
                    );
                }
                else
                {
                    ANNgridView.ActiveFilterCriteria = searchFilter;
                }
            }
        }

        #endregion

        #region Работа с артикулами и разделением труда

        /// <summary>
        /// Обработчик кнопки "Отвязать артикул от РТ".
        /// Удаляет связь между выбранным артикулом и разделением труда.
        /// </summary>
        private async void ResetButton_Click(object sender, EventArgs e)
        {
            try
            {
                var view = customGridControl5.MainView as GridView;
                if (view != null)
                {
                    int[] selectedRows = view.GetSelectedRows();
                    if (selectedRows.Length > 0)
                    {
                        int kod = Convert.ToInt32(view.GetRowCellValue(selectedRows[0], "kodd_rt"));

                        try
                        {
                            // Вызов метода для отвязки артикула
                            await _artNormService.ResetAnnIdinArticul(kod);

                            // Обновление данных в таблице после отвязки
                             view.DeleteRow(selectedRows[0]);
                        }
                        catch (Exception ex)
                        { MessageBox.Show("Ошибка отвязки от РТ", ex.Message);
                            await _logger.LogErrorAsync(ex, "Ошибка отвязки от РТ "+ ex.Message);
                        }
                        finally
                        {
                            MessageBox.Show("Артикул успешно отвязан от РТ.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await _logger.LogEventAsync($"Артикул отвязан от РТ", "ResetBtnClick");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите артикул для отвязки!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при отвязке артикула от РТ");
                MessageBox.Show($"Ошибка при отвязке артикула: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Привязывает выбранные артикулы к выбранному разделению труда (РТ).
        /// </summary>
        private async void BindButton_Click(object sender, EventArgs e)
        {
            try
            {
                GridView artView = customGridControl1.MainView as GridView;
                GridView annView = customGridControl2.MainView as GridView;

                if (artView == null || annView == null)
                {
                    MessageBox.Show("Данные не загружены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                int selectedArt = -1;
                int selectedAnn = -1;
                MyDataART selectedArtRow = null;
                MyDataANN selectedAnnRow = null;
                // Получаем выбранные артикулы
                BindingList<MyDataART> artDataSource = artView.DataSource as BindingList<MyDataART>;
                foreach (var row in artDataSource)
                {
                    if (row.IsChecked)
                    {
                        selectedArt = row.Kod;
                        selectedArtRow = row;
                        break;
                    }
                }

                // Получаем выбранное разделение труда
                BindingList<MyDataANN> annDataSource = annView.DataSource as BindingList<MyDataANN>;
                foreach (var row in annDataSource)
                {
                    if (row.IsChecked)
                    {
                        selectedAnn = row.AnnId;
                        selectedAnnRow = row;
                        break;
                    }
                }

                // Проверяем, выбраны ли оба элемента
                if (selectedArt == -1 || selectedAnn == -1)
                {
                    MessageBox.Show("Выберите артикул и разделение труда для привязки!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // Подтверждение привязки
                DialogResult result = MessageBox.Show(
                    $"Вы действительно хотите привязать артикул {selectedArtRow.Articul.TrimEnd()} к разделению труда {selectedAnnRow.Articul.TrimEnd()}?",
                    "Подтверждение привязки",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.No) return;

                // Выполняем привязку через сервис
                _artNormService.UpdateAnnIdinArticul(selectedArt, selectedAnn);

                // Обновляем UI
                if (selectedArtRow != null && selectedAnnRow != null)
                {
                    selectedArtRow.BindedArt = selectedAnnRow.Articul;
                    artView.RefreshData();
                    annView.RefreshData();
                    customGridControl1.MainView.RefreshData();
                   // customGridControl1.MainView;

                    customGridControl2.MainView.RefreshData();
                }

                await  _logger.LogEventAsync("Привязка завершена", $"Артикул {selectedArt} привязан к РТ {selectedAnn}");
                MessageBox.Show("Привязка успешно выполнена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при привязке артикула к РТ");
                MessageBox.Show($"Ошибка при привязке артикула: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Гарантирует, что `IsChecked` может быть установлен только у одной строки.
        /// Работает с `gridView7` и `gridView8`, а также с любым другим `GridView`, где используется `IsChecked`.
        /// </summary>
        /// <typeparam name="T">Тип данных, реализующий `ICheckable`</typeparam>
        /// <param name="gridControl">GridControl, где произошло изменение</param>
        /// <param name="e">Аргумент события `CellValueChangedEventArgs`</param>
        private async void GridView_CellValueChanged<T>(GridControl gridControl, CellValueChangedEventArgs e) where T : class
        {
            if (e.Column.FieldName != "IsChecked") return;

            try
            {
                SafeInvoke(gridControl, () =>
                {
                    if (!(gridControl.MainView is GridView view)) return;

                    var newCheckedRow = view.GetRow(e.RowHandle) as ICheckable;
                    if (newCheckedRow == null || !(e.Value is bool isChecked)) return;

                    if (!isChecked)
                    {
                        view.RefreshRow(e.RowHandle); // просто обновим UI для снятия флажка
                        return;
                    }

                    // Снимаем флажки со всех строк, кроме текущей
                    for (int i = 0; i < view.RowCount; i++)
                    {
                        if (i == e.RowHandle) continue; // текущую не трогаем

                        var row = view.GetRow(i) as ICheckable;
                        if (row != null && row.IsChecked)
                        {
                            row.IsChecked = false;

                        }
                    }
                            view.RefreshData();

                    // Устанавливаем флаг только текущей строке
                    newCheckedRow.IsChecked = true;

                    view.RefreshData();
                });
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при изменении значения в GridView");
                MessageBox.Show($"Ошибка при изменении значения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region Поиск и фильтрация данных

        /// <summary>
        /// Обработчик изменения состояния customCheckBox6.  
        /// Фильтрует gridView8 по статусу.
        /// </summary>
        private async void customCheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string filterString = "";

                if (actualCheckBox1.Checked) filterString += $"status = {(int)Status.Actual}";
                if (preliminaryCheckBox1.Checked)
                {
                    if (!string.IsNullOrEmpty(filterString)) filterString += " OR ";
                    filterString += $"status = {(int)Status.Preliminary}";
                }

                // Применяем фильтр к gridView8
                gridView8.BeginUpdate();
                gridView8.ActiveFilterString = filterString;
                gridView8.EndUpdate();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при фильтрации gridView8");
                MessageBox.Show($"Ошибка при применении фильтра: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #endregion

        #region Обработчики кнопок

        /// <summary>
        /// Скопировать РТ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCopyWd_Click(object sender, EventArgs e)
        {
            try
            {
                bufferId = (int)ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "AnnID");
                buffer.Text = $"группа: {ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "Group").ToString().TrimEnd(' ')},\r" +
                    $"модель: {ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "Mod").ToString().TrimEnd(' ')},\r" +
                    $"артикул: {ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "Articul").ToString().TrimEnd(' ')}";
            }
            catch
            {
                bufferId = 0;
                MessageBox.Show("Копирование не реализовано", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Добавить предварительное
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonPreliminaryWd_Click(object sender, EventArgs e)
        {
            if (ANNgridView == null) return;

            // Создаём новую запись модели `ArtNorm`
            ArtNormN newItem = new ArtNormN
            {
                Kod = "0000000",
                Group = "",
                Articul = "",
                Mod = "",
                SekShv = 0,
                SekVyaz5 = 0,
                SekVyaz6 = 0,
                SekVyaz7 = 0,
                SekVyaz10 = 0,
                SekVyaz12 = 0,
                SekVyazo = 0,
                SekVyaz = 0,
                Sek = 0,
                Komment = "",
                DataSozd = DateTime.Now,
                Diz = 0,
                Constr = 0,
                DataObn = DateTime.MinValue,
                SekKr = 0,
                Slogn = 0,
                Status = 1,
                StatusText = "предварительный",
                Arh = false,
                AnnID = 0
            };

            // Сохраняем в БД и получаем новый `annID`
            int newId = await _artNormService.InsertANN(newItem);
            if (newId <= 0)
            {
                MessageBox.Show("Ошибка сохранения в БД!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Обновляем ID в объекте
            newItem.AnnID = newId;

            // Добавляем новую строку в источник данных
            _bindingSource.Add(newItem);
            // Обновляем отображение грида
            ANNgridControl.RefreshDataSource();

            // Даем время на обновление UI
            //await Task.Delay(100);

            // Открываем форму редактирования
            using (TeamWork_AdvanceTW teamWork_AdvanceTW = new TeamWork_AdvanceTW(bufferId, (int)Mode.NewWorkDivision, newId: newId))
            {
                //if (teamWork_AdvanceTW.ShowDialog() == DialogResult.OK)
                //{
                //    var createdItem = teamWork_AdvanceTW.CreatedAnn;
                //    if (createdItem != null) {
                //        newItem.Articul = createdItem.Articul;
                //        newItem.Mod = createdItem.Mod;
                //        newItem.Group = createdItem.Group;
                //        newItem.Komment = createdItem.Komment;
                //        newItem.Diz = createdItem.Diz;
                //        newItem.Constr = createdItem.Constr;
                //        newItem.Sek = createdItem.Sek;
                //       // newItem.DataSozd = createdItem.DataSozd;

                //    }
                //        _bindingSource.ResetBindings(false);

                //    int newRowHandle = ANNgridView.LocateByValue("AnnID", newItem.AnnID);
                //    if (newRowHandle >= 0)
                //    {
                //        ANNgridView.FocusedRowHandle = newRowHandle;
                //        ANNgridView.RefreshRow(newRowHandle);
                //    }
                //    //await LoadWorkDivisions();
                //    //await CurrentWorks_Load();
                //    //await LoadRelatedData(newId);
                //}
                //else
                //{
                //    // Удаляем строку при отмене
                //    _bindingList.Remove(newItem);
                //    _bindingSource.Remove(newItem);
                //    await _artNormService.deleteRow("art_norm_n", newId);
                //    if (teamWork_AdvanceTW.IsRaszInserted)
                //        await _artNormService.deleteRow("norm_rasz", newId);
                //    if (teamWork_AdvanceTW.IsRaskInserted)
                //        await _artNormService.deleteRow("norm_rask", newId);
                //    if (teamWork_AdvanceTW.IsKontInserted)
                //        await _artNormService.deleteRow("norm_kont", newId);
                //    if (teamWork_AdvanceTW.IsDopObrInserted)
                //        await _artNormService.deleteRow("norm_dop_obr", newId);
                //    _bindingSource.ResetBindings(false);
                //    ANNgridControl.RefreshDataSource();
                //    ANNgridView.RefreshData();
                //}
                await HandleAnnEditResult(teamWork_AdvanceTW, newItem);
            }
        }
        

        /// <summary>
        /// Архив+копия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonArchAndCopyWd_Click(object sender, EventArgs e)
        {
            await ArchAndCopy();
        }

        private async Task ArchAndCopy()
        {
            GridView annView = ANNgridView;
            if (annView == null || annView.FocusedRowHandle < 0)
            {
                MessageBox.Show("Выберите запись для архивирования", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Получаем ID выбранной записи
                int selectedAnnId = (int)annView.GetRowCellValue(annView.FocusedRowHandle, "AnnID");

                // Проверяем наличие незавершенного производства (НЗП)
                bool hasNZP = await checkNzp(selectedAnnId);

                // Копируем запись в новую и получаем ID новой записи
                ArtNormN newRow = await CopyRow(hasNZP);
                if (newRow.AnnID <= 0)
                {
                    return;
                }

                //                //Открываем форму расширенного редактирования
                //                using (TeamWork_AdvanceTW teamWorkAdvanceTW = new TeamWork_AdvanceTW(bufferId, (int)Mode.ArchAndCopy, newId, selectedAnnId))
                //                {
                //                    DialogResult result = teamWorkAdvanceTW.ShowDialog();

                //                    if (result == DialogResult.OK)
                //                    {
                //                        // Обновляем все данные после сохранения
                //                        await updateNewRow(selectedAnnId, newId);
                //                    }
                //                    else
                //                    {
                //                        // Обрабатываем отмену операции
                //                        await _logger.LogEventAsync($"Редактирование копии записи ID={newId} отменено пользователем", "ArchAndCopy");
                ////TODO: удалить вновь обавленную строку?
                //                    }
                //                }


                using (var teamWorkAdvanceTW = new TeamWork_AdvanceTW(bufferId, (int)Mode.ArchAndCopy, newRow.AnnID, selectedAnnId))
                {
                    await HandleAnnEditResult(teamWorkAdvanceTW, newRow);
                }


                // Обновляем архивный статус исходной записи
                await _artNormService.UpdateAnnId("art_norm_n", selectedAnnId, "Status", hasNZP ? (int)Status.PreliminaryArchive : (int)Status.Archive);
                await _logger.LogEventAsync($"Запись ID={selectedAnnId} архивирована. Создана новая запись ID={newRow.AnnID}", "CopyRow");

                // Обновляем данные в гриде
                ANNgridView.RefreshData();

                //// Выделяем новую запись в гриде
                //int newRowHandle = ANNgridView.LocateByValue("AnnID", newId);
                //if (newRowHandle >= 0)
                //{
                //    ANNgridView.FocusedRowHandle = newRowHandle;
                //}
                //перепривязываем артикулы базового РТ
                await bindArticulToNewRow(selectedAnnId, newRow.AnnID);

            }
            catch (Exception ex)
            {
//TODO: удалить новую строку и вернуть статус архивируемой?
                // Обрабатываем возможные ошибки
                await _logger.LogErrorAsync(ex, "Ошибка при архивировании и копировании записи");
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task HandleAnnEditResult(TeamWork_AdvanceTW teamWorkForm, ArtNormN newItem)
        {
            if (teamWorkForm.ShowDialog() == DialogResult.OK)
            {
                var createdItem = teamWorkForm.CreatedAnn;

                if (createdItem != null)
                {
                    // Обновляем существующий объект
                    newItem.Articul = createdItem.Articul;
                    newItem.Mod = createdItem.Mod;
                    newItem.Group = createdItem.Group;
                    newItem.Komment = createdItem.Komment;
                    newItem.Diz = createdItem.Diz;
                    newItem.Constr = createdItem.Constr;
                    newItem.Sek = createdItem.Sek;
                }

                _bindingSource.ResetBindings(false);
                int newRowHandle = ANNgridView.LocateByValue("AnnID", newItem.AnnID);
                if (newRowHandle >= 0)
                {
                    ANNgridView.FocusedRowHandle = newRowHandle;
                    ANNgridView.RefreshRow(newRowHandle);
                }
            }
            else
            {
                // Удаляем несохранённую строку
                _bindingList.Remove(newItem);
                _bindingSource.Remove(newItem);

                await _artNormService.deleteRow("art_norm_n", newItem.AnnID);
                if (teamWorkForm.IsRaszInserted)
                    await _artNormService.deleteRow("norm_rasz", newItem.AnnID);
                if (teamWorkForm.IsRaskInserted)
                    await _artNormService.deleteRow("norm_rask", newItem.AnnID);
                if (teamWorkForm.IsKontInserted)
                    await _artNormService.deleteRow("norm_kont", newItem.AnnID);
                if (teamWorkForm.IsDopObrInserted)
                    await _artNormService.deleteRow("norm_dop_obr", newItem.AnnID);

                _bindingSource.ResetBindings(false);
                ANNgridControl.RefreshDataSource();
                ANNgridView.RefreshData();
            }
        }

        private async Task bindArticulToNewRow(int selectedAnnId, int newId)
        {
            await _artNormService.UpdateAnnId("norm_rasz", selectedAnnId, "annId", newId);
            await _artNormService.UpdateAnnId("norm_Rask", selectedAnnId, "annId", newId);
            await _artNormService.UpdateAnnId("norm_Kont", selectedAnnId, "annId", newId);
            await _artNormService.UpdateAnnId("norm_dop_obr", selectedAnnId, "annId", newId);
        }

        private async Task updateNewRow(int selectedAnnId, int newId)
        {
            await LoadWorkDivisions();
            await CurrentWorks_Load();
            await LoadRelatedData(newId);
            await _logger.LogEventAsync($"Запись ID={selectedAnnId} успешно архивирована и скопирована как ID={newId}", "ArchAndCopy");

            // Показываем сообщение об успешном завершении операции
            MessageBox.Show(
                $"Запись успешно архивирована",
                "Информация",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private async Task<bool> checkNzp(int selectedAnnId)
        {
            bool hasNZP = false;
            if (gridView10.RowCount > 0)
                for (int i = 0; i < gridView10.RowCount; i++)
                {
                    object rowObject = gridView10.GetRow(i);
                    int nzp = Convert.ToInt32(gridView10.GetRowCellValue(0, "kolNZP"));
                    hasNZP = nzp > 0;
                    await _logger.LogEventAsync($"Запись ID={selectedAnnId} имеет НЗП: {hasNZP}", "ArchAndCopy");

                }

            return hasNZP;
        }

        /// <summary>
        /// Создает копию выбранной записи разделения труда в базе данных
        /// </summary>
        /// <returns>новая запись или null в случае ошибки</returns>
        private async Task<ArtNormN> CopyRow(bool nzp)
        {
            try
            {
                int selectedRowHandle = ANNgridView.FocusedRowHandle;
                if (selectedRowHandle < 0)
                {
                    MessageBox.Show("Выберите разделение труда для копирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                // Получаем выбранную запись
                ArtNormN sourceRecord = ANNgridView.GetRow(selectedRowHandle) as ArtNormN;
                if (sourceRecord == null)
                {
                    MessageBox.Show("Не удалось получить данные выбранной записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                // Создаем копию записи с новыми значениями
                ArtNormN newRecord = new ArtNormN
                {
                    Kod = sourceRecord.Kod,
                    Group = sourceRecord.Group,
                    Articul = sourceRecord.Articul,
                    Mod = sourceRecord.Mod,
                    SekShv = sourceRecord.SekShv,
                    SekVyaz5 = sourceRecord.SekVyaz5,
                    SekVyaz6 = sourceRecord.SekVyaz6,
                    SekVyaz7 = sourceRecord.SekVyaz7,
                    SekVyaz10 = sourceRecord.SekVyaz10,
                    SekVyaz12 = sourceRecord.SekVyaz12,
                    SekVyazo = sourceRecord.SekVyazo,
                    SekVyaz = sourceRecord.SekVyaz,
                    Sek = sourceRecord.Sek,
                    Komment = sourceRecord.Komment==null?"":sourceRecord.Komment,
                    DataSozd = DateTime.Now,
                    Diz = sourceRecord.Diz,
                    Constr = sourceRecord.Constr,
                    DataObn = null,
                    SekKr = sourceRecord.SekKr,
                    Slogn = sourceRecord.Slogn,
                    Arh = false,
                    Status = nzp ? (int)Status.Preliminary : (int)Status.Actual
                };

                // Сохраняем копию в базу данных
                newRecord.AnnID = await Task.Run(() => _artNormService.SaveCopyToDatabase(newRecord));
                if (newRecord.AnnID <= 0)
                {
                    MessageBox.Show("Не удалось сохранить копию записи в базе данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                return newRecord;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при копировании записи");
                MessageBox.Show($"Произошла ошибка при копировании: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Редактировать РТ
        /// </summary>
        private void ButtonEditWd_Click(object sender, EventArgs e)
        {
            try
            {
                int _rowNumber = ANNgridView.FocusedRowHandle;
                if (_rowNumber < 0)
                {
                    MessageBox.Show("Выберите запись для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
                // Создаём новую запись модели `ArtNorm` из выбранной строки
                /*       ArtNormN newItem = new ArtNormN
                       {
                               Kod = ANNgridView.GetRowCellValue(_rowNumber, "Kod").ToString(),
                               Group = ANNgridView.GetRowCellValue(_rowNumber, "Group").ToString(),
                               Articul = ANNgridView.GetRowCellValue(_rowNumber, "Articul").ToString(),
                               Mod = ANNgridView.GetRowCellValue(_rowNumber, "Mod").ToString(),
                               SekShv = Convert.ToInt32(ANNgridView.GetRowCellValue(_rowNumber, "SekShv")),
                               SekVyaz5 = Convert.ToInt32(ANNgridView.GetRowCellValue(_rowNumber, "SekVyaz5")),
                               SekVyaz6 = Convert.ToInt32(ANNgridView.GetRowCellValue(_rowNumber, "SekVyaz6")),
                               SekVyaz7 = Convert.ToInt32(ANNgridView.GetRowCellValue(_rowNumber, "SekVyaz7")),
                               SekVyaz10 = Convert.ToInt32(ANNgridView.GetRowCellValue(_rowNumber, "SekVyaz10")),
                               SekVyaz12 = Convert.ToInt32(ANNgridView.GetRowCellValue(_rowNumber, "SekVyaz12")),
                               SekVyazo = Convert.ToInt32(ANNgridView.GetRowCellValue(_rowNumber, "SekVyazo")),
                               SekVyaz = Convert.ToInt32(ANNgridView.GetRowCellValue(_rowNumber, "SekVyaz")),
                               Sek = Convert.ToInt32(ANNgridView.GetRowCellValue(_rowNumber, "Sek")),
                               Komment = ANNgridView.GetRowCellValue(_rowNumber, "Komment")==DBNull.Value?"": ANNgridView.GetRowCellValue(_rowNumber, "Komment").ToString(),
                               DataSozd = Convert.ToDateTime(ANNgridView.GetRowCellValue(_rowNumber, "DataSozd")),
                               DataObn = Convert.ToDateTime(ANNgridView.GetRowCellValue(_rowNumber, "DataObn")),
                               Diz = Convert.ToInt32(ANNgridView.GetRowCellValue(_rowNumber, "Diz")),
                               Constr = Convert.ToInt32(ANNgridView.GetRowCellValue(_rowNumber, "Constr")),
                               SekKr = Convert.ToInt32(ANNgridView.GetRowCellValue(_rowNumber, "SekKr")),
                               Slogn = Convert.ToInt32(ANNgridView.GetRowCellValue(_rowNumber, "Slogn")),
                               Status = Convert.ToInt32(ANNgridView.GetRowCellValue(_rowNumber, "Status")),
                               StatusText = ANNgridView.GetRowCellValue(_rowNumber, "StatusText").ToString(),
                               Arh = Convert.ToBoolean(ANNgridView.GetRowCellValue(_rowNumber, "Arch")),
                               AnnID = Convert.ToInt32(ANNgridView.GetRowCellValue(_rowNumber, "AnnID"))
                           };

                           */
                ArtNormN newItem = ANNgridView.GetRow(_rowNumber) as ArtNormN;

                // Получаем ID выбранной записи
                int selectedAnnId = (int)ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "AnnID");

                // Открываем форму редактирования
                using (TeamWork_AdvanceTW teamWorkAdvanceTW = new TeamWork_AdvanceTW(
                   bufferId,
                    (int)Mode.Edit,oldId: selectedAnnId ))                    // Новый ID не нужен, так как мы редактируем существующую запись
                {
                    DialogResult result = teamWorkAdvanceTW.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        // Обновляем все данные после сохранения
                        //       LoadWorkDivisions();
                        //       CurrentWorks_Load();
                        //       LoadRelatedData(selectedAnnId);
                        ANNgridControl.RefreshDataSource();

                        // Отображаем сообщение об успешном редактировании
                        MessageBox.Show(
                            "Запись успешно отредактирована.",
                            "Информация",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // Обрабатываем возможные ошибки
                _logger.LogErrorAsync(ex, "Ошибка при редактировании записи");
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region текущие работы - требуют увязки
        /// <summary>
        /// Загрузка вкладки "текущие работы" (MyDataAnn)
        /// </summary>
        private async Task CurrentWorks_Load()
        {
            //загрузка артикулов для увязки (актуальные и предварительные)
            await MyDataArtLoad();
            //загрузка  таблицы РТ для увязки (текущие работы)
            await MyDataAnnLoad();
            //norm_rasz
            await NormRaszLoad();
        }

        private async Task MyDataArtLoad()
        {
            try
            {
                // Fetch unbound articles
                DataTable relatedData = await _artNormService.GetRelatedSpArt(0);
                await _logger.LogEventAsync($"Related Data Count: {relatedData.Rows.Count}", "MyDataArtLoad");

                if (relatedData != null && relatedData.Rows.Count > 0)
                {
                    BindingList<MyDataART> artDataList = new BindingList<MyDataART>();

                foreach (DataRow row in relatedData.Rows)
                {
                    try
                    {
                            artDataList.Add(MapDataRowToMyDataART(row));
                    }
                    catch (Exception ex)
                    {
                        await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных в текущие работы: {ex.Message}");
                        MessageBox.Show("Произошла ошибка. Подробности в логе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    ;
                }

                    try
                    {
                        customGridControl1.DataSource = artDataList; // This should work if types match
            }
                    catch(Exception ex) { MessageBox.Show("Ошибка приведения artDataList.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                else
                {
                    MessageBox.Show("Нет данных для загрузки.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных в текущие работы: {ex.Message}");
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private MyDataART MapDataRowToMyDataART(DataRow row)
        {
            if (row == null || row.Table == null)
            {
                Console.WriteLine("Ошибка: передан пустой DataRow или у него отсутствует таблица!");
                return null;
            }

            // Проверяем, содержит ли DataRow нужные колонки
            bool hasKod = row.Table.Columns.Contains("Kod");
            bool hasArticul = row.Table.Columns.Contains("Articul");
            bool hasGrup = row.Table.Columns.Contains("Grup");
            bool hasMod = row.Table.Columns.Contains("Mod");

            // Логируем список доступных колонок (для отладки)
            Console.WriteLine("Доступные колонки в DataRow:");
            foreach (DataColumn col in row.Table.Columns)
            {
                Console.WriteLine($"🔹 {col.ColumnName}");
            }

            return new MyDataART
            {
                Kod = hasKod && row["Kod"] != DBNull.Value ? Convert.ToInt32(row["Kod"]) : 0,
                Articul = hasArticul && row["Articul"] != DBNull.Value ? row["Articul"].ToString() : string.Empty,
                Group = hasGrup && row["Grup"] != DBNull.Value ? row["Grup"].ToString() : string.Empty,
                Model = hasMod && row["Mod"] != DBNull.Value ? row["Mod"].ToString() : string.Empty,
                IsChecked = false // Default value
            };
        }
        private async Task MyDataAnnLoad()
        {
            try
            {
                int kod = GetSelectedKodFromGrid(customGridControl1);
                List<ArtNormN> artNormNs = await _artNormService.GetArtNormDataCurrent(kod, loadAllCheckBox.Checked);

                if (artNormNs != null)
                {
                    var relatedMyDataAnn = ConvertToMyDataAnn(artNormNs);
                    customGridControl3.DataSource = relatedMyDataAnn;
                }
                else
                {
                    MessageBox.Show("Нет данных для загрузки.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                 await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных в текущие работы: {ex.Message}");
            }

            //BindingList<MyDataANN> myDataList = new BindingList<MyDataANN>();
            //// Заполняем myDataList данными из DataTable 
            //foreach (MyDataANN row in relatedData)
            //{
            //    try
            //    {
            //        myDataList.Add(new MyDataANN
            //        {
            //            AnnId = row.AnnId,
            //            Kod = row.Kod,
            //            Articul = row.Articul,
            //            Status = row.Status,
            //            Stat = row.Stat,
            //            Group = row.Group,
            //            Model = row.Model,
            //            IsChecked = false
            //        });
            //    }
            //    catch (Exception ex)
            //    {
            //        await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных в текущие работы: {ex.Message}");
            //        MessageBox.Show("Произошла ошибка. Подробности в логе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    ;
            //}
            //customGridControl2.DataSource = myDataList;
        }
        private int GetSelectedKodFromGrid(GridControl grid)
        {
            var view = grid.MainView as GridView;
            if (view != null && view.FocusedRowHandle >= 0)
            {
                // Retrieve the "Kod" value from the focused row
                return Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "Kod"));
            }
            return 0; // Return 0 or an appropriate default value if no valid row is selected
        }
        async Task NormRaszLoad()
        {
            int annId = 0;
            var view = customGridControl2.MainView as GridView;
            if (view != null)
            {
                annId = Convert.ToInt32(view.GetRowCellValue(0, "AnnId"));
            }
            DataTable relatedRasz = new DataTable();
            relatedRasz = await _artNormService.GetRelatedNormRasz(annId);
            normraszBindingSource1.DataSource = relatedRasz;
            customGridControl3.DataSource = normraszBindingSource1;
        }

        ///// <summary>
        ///// загрузка norm_rasz
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private async void gridView8_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            int annId = CommonFunctions.GetRowCellValueOrDefault<int>(gridView8, e.FocusedRowHandle, "annId", 0);
            await LoadRelatedData(annId);

            //int annId = 0;
            //var view = gridView8;//customGridControl2.MainView as GridView;
            //if (view != null)
            //{
            //    annId = Convert.ToInt32(view.GetRowCellValue(e.FocusedRowHandle, "AnnId"));
            //}

            //var relatedData = _artNormService.GetRelatedNormRasz(annId);
            //normraszBindingSource1.DataSource = relatedData;
            //customGridControl3.DataSource = normraszBindingSource1;

        }

        private async void gridView7_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            int kod = CommonFunctions.GetRowCellValueOrDefault<int>(gridView7, e.FocusedRowHandle, "Kod", 0);
            string articul = CommonFunctions.GetRowCellValueOrDefault<string>(gridView7, e.FocusedRowHandle, "Articul", "");


            BindingList<MyDataANN> list = loadAllCheckBox.Checked ?
                await LoadWorksbyArt(0, "") :
    await LoadWorksbyArt(kod, articul);

            customGridControl2.DataSource = list; //LoadWorksbyArt(kod, articul);
        }

        /// <summary>
        /// обработка клика на заголовке, 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView8_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "IsChecked")
            {
                SafeInvoke(gridView8.GridControl, () =>
                {
                    var view = sender as GridView;
                    if (view == null) return;

                    var currentRow = view.GetRow(e.RowHandle) as MyDataANN;
                    if (currentRow == null) return;

                    bool isChecked = (bool)e.Value;
                    
                    // Если текущая строка отмечается
                    if (isChecked)
                    {
                        // Сначала снимаем все отметки
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            var row = view.GetRow(i) as MyDataANN;
                            if (row != null)
                            {
                                row.IsChecked = false;
                            }
                        }
                        // Затем отмечаем только текущую строку
                        currentRow.IsChecked = true;
                    }

                    view.RefreshData();
                });
            }
        }

        //обработка клика на заголовке
        private void gridView7_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "IsChecked")
            {
                SafeInvoke(gridView7.GridControl, () =>
            {
                    var view = sender as GridView;
                    if (view == null) return;

                    var currentRow = view.GetRow(e.RowHandle) as MyDataART;
                    if (currentRow == null) return;

                    bool isChecked = (bool)e.Value;

                    // Если текущая строка отмечается
                    if (isChecked)
                        {
                        // Сначала снимаем все отметки
                        for (int i = 0; i < view.RowCount; i++)
                            {
                            var row = view.GetRow(i) as MyDataART;
                            if (row != null)
                                {
                                row.IsChecked = false;
                    }
                }
                        // Затем отмечаем только текущую строку
                        currentRow.IsChecked = true;
            }

                    view.RefreshData();
                });
            }
        }

        #region headerCheckBox
        private bool isHeaderChecked = false; // Состояние CheckBox в заголовке
        private Dictionary<GridView, bool> gridViewStates = new Dictionary<GridView, bool>();//словарь состояний CheckBox
        //отрисовка чекБокса в заголовке столбца
        private void gridView8_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            // DrawCheckBox(e);
        }

        private void gridView7_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            // DrawCheckBox(e);
        }
        private void gridView9_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            //DrawCheckBox(e);
        }
        private void DrawCheckBox(ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column != null && e.Column.FieldName == "IsChecked") // Убедимся, что это нужный столбец
            {
                // Очищаем стандартную отрисовку заголовка
                e.Handled = true;

                // Отрисовка заголовка
                e.Painter.DrawObject(e.Info);

                // Отрисовка CheckBox
                // Получаем размеры области заголовка
                Rectangle rect = e.Bounds;
                CheckBoxRenderer.DrawCheckBox(
                    e.Graphics,
                    new Point(rect.Left + (rect.Width - 16) / 2, rect.Y + (rect.Height / 2) - 8),   // Рассчитываем позицию чекбокса по центру
                    isHeaderChecked ? System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal : System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal
                );
            }
        }
        private List<MyDataART> GetCheckedRows<MyDataART>(GridView view)
        {
            BindingList<MyDataART> dataSource = view.DataSource as BindingList<MyDataART>;
            if (dataSource == null) return new List<MyDataART>(); // Обработка null
            List<MyDataART> list = new List<MyDataART>();
            foreach (MyDataART row in dataSource)
            {
                //if (row.IsChecked = true)
                {
                    list.Add(row);

                }
            }
            return new List<MyDataART>();//
                                         //dataSource.Where(item => item.IsChecked).ToList();
        }
        private void AddCheckBoxToHeader(GridView gridView)
        {
            DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit headerCheckEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            GridColumn column = gridView.Columns["IsChecked"];
            column.OptionsColumn.AllowEdit = true;

            column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            column.ColumnEdit = headerCheckEdit;


        }
        //Обработка нажатия на заголовке чекБоксов
        private void CheckBoxMouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;
            GridHitInfo hitInfo = view.CalcHitInfo(e.Location);

            if (!hitInfo.InColumn || hitInfo.Column.FieldName != "IsChecked") return;
            isHeaderChecked = !isHeaderChecked;
            //UpdateAllRows(view, isHeaderChecked);
            //view.RefreshData();
            // Получаем или устанавливаем состояние для текущего GridView
            if (!gridViewStates.TryGetValue(view, out bool isChecked))
            {
                isChecked = false; // Значение по умолчанию, если GridView еще не был обработан
            }

            isChecked = !isChecked;
            gridViewStates[view] = isChecked; // Сохраняем новое состояние

            UpdateAllRows(view, isChecked);
            view.RefreshData();
        }

        //обновление статуса CheckBox у всех строк таблицы
        private void UpdateAllRows(GridView view, bool isChecked)
        {
            SafeInvoke(view.GridControl, () =>
        {
            IList dataSource = view.DataSource as IList;
            if (dataSource == null) return;

            foreach (var item in dataSource)
            {
                if (item != null)
                {
                    var property = item.GetType().GetProperty("IsChecked");
                    if (property != null && property.CanWrite)
                    {
                        property.SetValue(item, isChecked);
                    }
                }
            }
            });
        }
        //переопределяем метод сортировки кликом на заголовке столбца. Хотелось бы оставить, конечно
        private void gridView_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {
            if (e.Column.FieldName == "IsChecked")
            {
                e.Handled = true; // Отменяем сортировку по колонке "IsChecked"
            }
        }

        #endregion

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!Enabled)
            {
                using (var brush = new SolidBrush(Color.Gray))
                {
                    e.Graphics.FillRectangle(brush, ClientRectangle);
                }
            }
        }
    }
}
