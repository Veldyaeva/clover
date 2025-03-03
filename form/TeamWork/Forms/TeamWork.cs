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


namespace SewingProduction.Forms
{
    public partial class TeamWork : CustomForm
    {
        private readonly DatabaseHelper _dbHelper; 
        private readonly ArtNormService _artNormService;
        private int selectedRowHandle = -1;
        private readonly ILogger _logger =new FileLogger();
        private int bufferWorkDivision;
        private readonly BindingList<ArtNormN> _bindingList;
        private readonly BindingSource _bindingSource;
        public TeamWork()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _artNormService = new ArtNormService(_dbHelper);

            ThemeManager.UpdateTheme(this);
            this.gridView7.CellValueChanged += (s, e) => GridView_CellValueChanged<MyDataART>(customGridControl1, e);
            this.gridView8.CellValueChanged += (s, e) => GridView_CellValueChanged<MyDataANN>(customGridControl2, e);

            _bindingList = new BindingList<ArtNormN>();
            _bindingSource = new BindingSource { DataSource = _bindingList };
            ANNgridControl.DataSource = _bindingSource;
        }

        #region Загрузка данных

        private async void TeamWorkForm_Load(object sender, EventArgs e)
        {
            await LoadWorkDivisions();
            await CurrentWorks_Load();
        }

        /// <summary>
        /// Загрузка вкладки "Список РТ"
        /// </summary>
        /// <returns></returns>
        private async Task LoadWorkDivisions()
        {

            //    //var designer = _artNormService.GetRelDesigner();
            //    //designerComboBox.DataSource = designer;
            //    //var constructor = _artNormService.GetRelDesigner();
            //    //constructorComboBox.DataSource = constructor;

            try
            {
                _bindingList.Clear();
                List<ArtNormN> data = await _artNormService.GetArtNormData();

                foreach (var item in data)
                {
                    _bindingList.Add(item);
                }

                // Принудительное обновление данных в UI
                _bindingSource.ResetBindings(false);
                ANNgridControl.RefreshDataSource();
                ANNgridView.RefreshData();
                ANNgridView.PopulateColumns(); // Заполняем колонки

                filterTable();//применяем фильтры


                await _logger.LogEventAsync("Данные загружены успешно", "LoadData");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка загрузки данных");
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        


        /// <summary>
        /// Загружает список разделений труда (РТ) для указанного артикула или кода.
        /// </summary>
        /// <param name="kod">Код артикула</param>
        /// <param name="articul">Название артикула</param>
        /// <returns>Список разделений труда (BindingList&lt;MyDataANN&gt;)</returns>
        private async Task<BindingList<MyDataANN>> LoadWorksbyArt(int kod, string articul)
        {
            try
            {
                List<ArtNormN> relatedData;

                // Если включен чекбокс "Загрузить все"
                if (loadAllCheckBox.Checked)
                {
                    relatedData = await _artNormService.GetArtNormDataCurrent(kod, true);
                }
                else
                {
                    relatedData = await _artNormService.GetArtNormDataCurrent(kod, false);

                    // Если артикул содержит "-", фильтруем по его первой части
                    int dashIndex = articul.IndexOf("-");
                    if (dashIndex > 0)
                    {
                        List<ArtNormN> partialData = await _artNormService.GetArtNormDataCurrent(articul.Substring(0, dashIndex));
                        foreach (var item in partialData)
                        { relatedData.Add(item); }
                    }
                }

                // Преобразуем DataTable в BindingList<MyDataANN>
                BindingList<MyDataANN> myDataList = new BindingList<MyDataANN>();

                foreach (var row in myDataList)
                {
                    myDataList.Add(new MyDataANN
                    {
                        AnnId = row.AnnId,
                        Kod = row.Kod,
                        Articul = row.Articul,
                        Status = row.Status,
                        //Stat "].ToString(),
                        Group = row.Group,
                        Model = row.Model,
                        IsChecked = false
                    });
                }
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

                commentRichTextBox.Text = CommonFunctions.GetRowCellValueOrDefault<string>(view, e.FocusedRowHandle, "komment", "");
                constructorComboBox.Text = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "constr", 0).ToString();
                designerComboBox.Text = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "diz", 0).ToString();

                int annId = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "AnnID", 0);
                //UpdateRelatedData(annId);
                await LoadRelatedData(annId);

                string kod = CommonFunctions.GetRowCellValueOrDefault<string>(view, e.FocusedRowHandle, "kod", "");
                int o = 0;
                try { o=Convert.ToInt32(kod); }
                catch (Exception ex) { await _logger.LogErrorAsync(ex, "опять КОД это строка"); return; }
                finally { if(o>0) LoadGridControlData(pictureBox1, o); }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при смене выбранной строки в gridView3");
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Загрузка данных LoadGridControlData

        ///// <summary>
        ///// Загружает данные в указанный GridControl через BindingSource.
        ///// </summary>
        ///// <param name="grid">Целевой GridControl</param>
        ///// <param name="source">BindingSource, привязанный к данным</param>
        ///// <param name="data">DataTable с новыми данными</param>
        //private async void LoadGridControlData(GridControl grid, BindingSource source, DataTable data)
        //{
        //    try
        //    {
        //        if (data == null)
        //        {
        //            source.DataSource = null;
        //        }
        //        else
        //        {
        //            source.DataSource = data;
        //        }

        //        grid.DataSource = source;
        //        grid.RefreshDataSource();
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, "Ошибка при загрузке данных в GridControl");
        //        MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private async void LoadGridControlData(List<MyDataART> grid, BindingSource source, DataTable data)
        //{
        //    try
        //    {
        //        if (data == null)
        //        {
        //            source.DataSource = null;
        //        }
        //        else
        //        {
        //            source.DataSource = data;
        //        }

        //        grid.DataSource = source;
        //        grid.RefreshDataSource();
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, "Ошибка при загрузке данных в GridControl");
        //        MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

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
            await GridHelper.LoadGridControlDataAsync(gridControl3, normraskBindingSource, await _artNormService.GetRelatedNormRask(annId));
            await GridHelper.LoadGridControlDataAsync(gridControl4, normkontBindingSource, await  _artNormService.GetRelatedNormKont(annId));
            await GridHelper.LoadGridControlDataAsync(gridControl5, normdopobrBindingSource, await _artNormService.GetRelatedNormDopObr(annId));
            await GridHelper.LoadGridControlDataAsync(customGridControl5, sparticulBindingSource, await _artNormService.GetRelatedSpArt(annId));
            await GridHelper.LoadImageAsync(pictureBox1, await _artNormService.GetImage(annId)); //не надо annId

            UpdateNZPStatus();
        }

        private async void UpdateNZPStatus()
        {
            try
            {
                int nzp = 0;
                var viewNzp = customGridControl5.MainView as GridView;

                if (viewNzp != null)
                {
                    for (int i = 0; i < viewNzp.RowCount; i++)
                    {
                        int parsedValue = CommonFunctions.GetRowCellValueOrDefault<int>(viewNzp, i, "kolNZP", 0);
                        nzp = parsedValue;
                    }
                }

                customButton7.Enabled = nzp <= 0;
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
            string searchText = searchControl1.Text.TrimEnd(' ');//filterTextBox1.Text.Trim();
            string columnName = await GridHelper.GetSelectedColumnNameAsync(kode.Checked, articul.Checked, model.Checked, group.Checked);

            if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(searchText))
            {
                ANNgridView.ActiveFilterCriteria = new FunctionOperator(
                    FunctionOperatorType.Contains,
                    new OperandProperty(columnName),
                    new OperandValue(searchText));
            }
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

        private void customCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            int kod = CommonFunctions.GetRowCellValueOrDefault<int>(gridView7, gridView7.FocusedRowHandle, "kod", 0);
            string articul = CommonFunctions.GetRowCellValueOrDefault<string>(gridView7, gridView7.FocusedRowHandle, "articul", "");

            customGridControl2.DataSource = loadAllCheckBox.Checked ? LoadWorksbyArt(0, "") : LoadWorksbyArt(kod, articul);
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
            string filterString = "";

            if (preliminaryCheckBox.Checked) filterString += $"[status] = {(int)Status.Preliminary}";
            if (actualCheckBox.Checked) filterString += (filterString.Length > 0 ? " OR " : "") + $"[status] = {(int)Status.Actual}";
            if (archiveCheckBox.Checked) filterString += (filterString.Length > 0 ? " OR " : "") + $"[status] = {(int)Status.Archive}";

            if (filterString.Length > 0) filterString = "(" + filterString + ")";
            if (SortBox.Checked) filterString += (filterString.Length > 0 ? " AND " : "") + "[sek_shv] = 0 AND [status] > 0";

            ANNgridView.ActiveFilterString = filterString;
        }

        /// <summary>
        /// Переключение фильтров при изменении чекбоксов
        /// </summary>
        private void Filter_CheckedChanged(object sender, EventArgs e) => filterTable();

        /// <summary>
        /// Сбрасывает текущий фильтр в searchControl1 при изменении параметров поиска.
        /// </summary>
        private async void search_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                searchControl1.ClearFilter(); // Сброс фильтра
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при очистке фильтра в searchControl1");
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
                            await _artNormService.ResetAnnId(kod);

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
                    $"Вы действительно хотите привязать артикул {selectedArt} к разделению труда {selectedAnn}?",
                    "Подтверждение привязки",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.No) return;

                // Выполняем привязку через сервис
                _artNormService.UpdateAnnId(selectedArt, selectedAnn);

                // Обновляем UI
                if (selectedArtRow != null && selectedAnnRow != null)
                {
                    selectedArtRow.BindedArt = selectedAnnRow.Articul;
                    artView.RefreshData();
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
            try
            {
                if (e.Column.FieldName == "IsChecked")
                {
                    await GridHelper.UpdateExclusiveCheckAsync<T>(gridControl, e.RowHandle);
                }
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
        private void copyButton_Click(object sender, EventArgs e)
        {
            try
            {
                bufferWorkDivision = (int)ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "annId");
                buffer.Text = $"группа: {ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "grup")}, модель {ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "mod")}, артикул: {ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "articul")}";
            }
            catch
            {
                bufferWorkDivision = 0;
                MessageBox.Show("Копирование не реализовано", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Добавить предварительное
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void customButton6_Click(object sender, EventArgs e)
        {
            await HandleButtonClickAsync();
        }
        private async Task HandleButtonClickAsync() { 

            if (ANNgridView == null) return;

            // Создаём новую запись модели `ArtNorm`
            ArtNormN newItem = new ArtNormN
            {
                Kod = "0000000",
                Group = "Группа",
                Articul = "Артикул",
                Mod = "Модель",
                SekShv = 0,
                SekVyaz5 = 0,
                SekVyaz6 = 0,
                SekVyaz7 = 0,
                SekVyaz10 = 0,
                SekVyaz12 = 0,
                SekVyazo = 0,
                SekVyaz = 0,
                Sek = 0,
                Komment = "Комментарий",
                DataSozd = DateTime.Now,
                Diz = 0,
                Constr = 0,
                DataObn = null,
                SekKr = 0,
                Slogn = 0,
                Arh = false,
                Status = 1
            };

            // Сохраняем в БД и получаем новый `annID`
            int newId = await _artNormService.InsertANN(newItem);
            if (newId <= 0)
            {
                MessageBox.Show("Ошибка сохранения в БД!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Обновляем ID в объекте и загружаем данные заново
            newItem.AnnID = newId;
            //LoadData(); // Загружаем актуальные данные
            _bindingList.Add(newItem);
            ANNgridControl.RefreshDataSource();
            // Ищем строку по `annID` в `GridView`
            int realRowHandle = ANNgridView.LocateByValue("annID", newId);
            if (realRowHandle >= 0 && ANNgridView.IsDataRow(realRowHandle))
            {
                ANNgridView.FocusedRowHandle = realRowHandle;

                // Открываем `EditForm`
                using (TeamWork_AdvanceTW teamWork_AdvanceTW = new TeamWork_AdvanceTW(newId, bufferWorkDivision, (int)Mode.NewWorkDivision))
                {
                    if (teamWork_AdvanceTW.ShowDialog() == DialogResult.OK)
                    {
                        // Можно обновить данные после закрытия формы, если нужно
                        await LoadWorkDivisions();
                    }
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Новая строка не найдена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        

        /// <summary>
        /// Архив+копия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void customButton10_Click(object sender, EventArgs e)
        {
            await ArchAndCopy();
        }

        private async Task ArchAndCopy()
        {
            GridView AnnView = ANNgridView;
            if (AnnView == null) return;
            if (gridView10.RowCount > 0)
            {
                int nzp = (int)gridView10.GetRowCellValue(0, "kolNZP");
                int newId = 0;//найти новый айди и присвоить
                if (nzp > 0)
                {
                    await CopyRow();
                    using (TeamWork_AdvanceTW teamWork_AdvanceTW = new TeamWork_AdvanceTW(newId, (int)ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "annId"), (int)Mode.ArchAndCopy))
                    {
                        if (teamWork_AdvanceTW.ShowDialog() == DialogResult.OK)
                        {
                            //сохраняем
                            AnnView.AddNewRow(); // Добавляем новую строку
                        }
                        else
                        {
                            //отменяем
                        }
                    }
                }

                else
                {
                    await CopyRow();
                    using (TeamWork_AdvanceTW teamWork_AdvanceTW = new TeamWork_AdvanceTW(newId, (int)ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "annId"), (int)Mode.ArchAndCopy))
                    {
                        if (teamWork_AdvanceTW.ShowDialog() == DialogResult.OK)
                        {
                            //сохраняем
                            AnnView.AddNewRow(); // Добавляем новую строку
                        }
                        else
                        {
                            //отменяем
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Редактировать РТ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customButton5_Click(object sender, EventArgs e)
        {
            if (ANNgridView.FocusedRowHandle >= 0)
                ANNgridView.ShowPopupEditForm();
            GridView view = ANNgridView;
            if (view == null) return;


            using (TeamWork_AdvanceTW teamWork_AdvanceTW = new TeamWork_AdvanceTW(0, (int)ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "annId"), (int)Mode.Edit))
            {
                if (teamWork_AdvanceTW.ShowDialog() == DialogResult.OK)
                {
                    //сохраняем
                    view.AddNewRow(); // Добавляем новую строку
                }
                else
                {
                    //отменяем
                }
            }

        }
        private async Task CopyRow()
        {
            int selectedRowHandle = gridView1.FocusedRowHandle;
            if (selectedRowHandle < 0)
            {
                MessageBox.Show("Выберите РТ для копирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ArtNormN oldRow = gridView1.GetRow(selectedRowHandle) as ArtNormN;
            if (oldRow == null) return;

            //  Создаём копию объекта
            ArtNormN newItem = new ArtNormN
            {
                Kod = oldRow.Kod,
                Group = oldRow.Group,
                Articul = oldRow.Articul,
                Mod = oldRow.Mod,
                SekShv = oldRow.SekShv,
                SekVyaz5 = oldRow.SekVyaz5,
                SekVyaz6 = oldRow.SekVyaz6,
                SekVyaz7 = oldRow.SekVyaz7,
                SekVyaz10 = oldRow.SekVyaz10,
                SekVyaz12 = oldRow.SekVyaz12,
                SekVyazo = oldRow.SekVyazo,
                SekVyaz = oldRow.SekVyaz,
                Sek = oldRow.Sek,
                Komment = oldRow.Komment,
                DataSozd = DateTime.Now, // Новая дата создания
                Diz = oldRow.Diz,
                Constr = oldRow.Constr,
                DataObn = null,
                SekKr = oldRow.SekKr,
                Slogn = oldRow.Slogn,
                Arh = false,
                Status = oldRow.Status
            };

            //  Сохраняем копию в БД
            int newID = _artNormService.SaveCopyToDatabase(newItem);
            if (newID <= 0)
            {
                MessageBox.Show("Ошибка копирования в БД!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //  Загружаем данные заново
            await LoadWorkDivisions();

            //  Ищем новую строку в `GridView`
            int newRowHandle = gridView1.LocateByValue("annID", newID);
            if (newRowHandle >= 0)
            {
                gridView1.FocusedRowHandle = newRowHandle;
                gridView1.ShowPopupEditForm();
            }
            else
            {
                MessageBox.Show("Ошибка: Копированная строка не найдена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                //артикулы для увязки
                List<MyDataART> relatedData = await _artNormService.GetRelatedSpArt(0);
                BindingList<MyDataART> artDataList = new BindingList<MyDataART>();
                // Заполняем myDataList данными из DataTable 
                foreach (var row in relatedData)
                {
                    try
                    {
                        artDataList.Add(new MyDataART
                        {
                            Kod = row.Kod,
                            Articul = row.Articul,
                            Group = row.Group,
                            Model = row.Model,
                            // binded_art = row["binded_art"].ToString(),
                            IsChecked = false
                        });
                    }
                    catch (Exception ex)
                    {
                        await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных в текущие работы: {ex.Message}");
                        MessageBox.Show("Произошла ошибка. Подробности в логе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    ;
                }

                customGridControl1.DataSource = artDataList;

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных в текущие работы: {ex.Message}");
                MessageBox.Show($"Ошибка загрузки данных в текущие работы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task MyDataAnnLoad()
        {
            int annId = 0;
            var view = customGridControl1.MainView as GridView;
            if (view != null)
            {
                annId = Convert.ToInt32(view.GetRowCellValue(0, "AnnId"));
            }
            List<MyDataANN> relatedMyDataAnn = new List<MyDataANN>();
            //РТ для увязки
            relatedMyDataAnn = await _artNormService
            normraszBindingSource1.DataSource = relatedMyDataAnn;
            customGridControl3.DataSource = normraszBindingSource1;



            BindingList<MyDataANN> myDataList = new BindingList<MyDataANN>();
            // Заполняем myDataList данными из DataTable 
            foreach (MyDataANN row in relatedData)
            {
                try
                {
                    myDataList.Add(new MyDataANN
                    {
                        AnnId = row.AnnId,
                        Kod = row.Kod,
                        Articul = row.Articul,
                        Status = row.Status,
                        Stat = row.Stat,
                        Group = row.Group,
                        Model = row.Model,
                        IsChecked = false
                    });
                }
                catch (Exception ex)
                {
                    await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных в текущие работы: {ex.Message}");
                    MessageBox.Show("Произошла ошибка. Подробности в логе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ;
            }
            customGridControl2.DataSource = myDataList;
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

        private void gridView7_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            int kod = CommonFunctions.GetRowCellValueOrDefault<int>(gridView7, e.FocusedRowHandle, "kod", 0);
            string articul = CommonFunctions.GetRowCellValueOrDefault<string>(gridView7, e.FocusedRowHandle, "articul", "");

            customGridControl2.DataSource = LoadWorksbyArt(kod, articul);
        }

        /// <summary>
        /// обработка клика на заголовке, 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView8_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column == gridView8.Columns["IsChecked"])
            {
                int rowHandle = e.RowHandle;
                MyDataANN data = gridView8.GetRow(rowHandle) as MyDataANN;

                if (data != null)
                {
                    data.IsChecked = (bool)e.Value;
                    if (data.IsChecked)
                    { // Обходим все строки и устанавливаем IsChecked в false для остальных
                        for (int i = 0; i < gridView8.RowCount; i++)
                        {
                            if (i != rowHandle)
                            {
                                MyDataANN otherData = gridView8.GetRow(i) as MyDataANN;
                                if (otherData != null)
                                {
                                    if (otherData.IsChecked)
                                        otherData.IsChecked = false;
                                }
                            }
                        }
                    }
                }
                gridView8.RefreshData();
            }

        }

        //обработка клика на заголовке
        private void gridView7_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column == gridView7.Columns["IsChecked"])
            {
                int rowHandle = e.RowHandle;
                MyDataART data = gridView7.GetRow(rowHandle) as MyDataART;

                if (data != null)
                {
                    data.IsChecked = (bool)e.Value;
                    if (data.IsChecked)
                    { // Обходим все строки и устанавливаем IsChecked в false для остальных
                        for (int i = 0; i < gridView7.RowCount; i++)
                        {
                            if (i != rowHandle)
                            {
                                MyDataART otherData = gridView7.GetRow(i) as MyDataART;
                                if (otherData != null)
                                {
                                    if (otherData.IsChecked)
                                        otherData.IsChecked = false;
                                }
                            }
                        }
                    }
                }
                gridView7.RefreshData();
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

    }
}
