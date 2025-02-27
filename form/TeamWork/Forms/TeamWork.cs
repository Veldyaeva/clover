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


namespace SewingProduction.Forms
{
    public partial class TeamWork : CustomForm
    {
        private readonly DatabaseHelper _dbHelper; 
        private readonly ArtNormService _artNormService;
        private int selectedRowHandle = -1;
        private readonly ILogger _logger =new FileLogger();
        private int bufferWorkDivision;
        public TeamWork()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _artNormService = new ArtNormService(_dbHelper);
            ThemeManager.UpdateTheme(this);
            this.gridView7.CellValueChanged += (s, e) => GridView_CellValueChanged<MyDataART>(gridView7, e);
            this.gridView8.CellValueChanged += (s, e) => GridView_CellValueChanged<MyDataANN>(gridView8, e);
        }

        #region Загрузка данных

        private async void TeamWorkForm_Load(object sender, EventArgs e)
        {
            Thread splashThread = new Thread(() =>
            {
                SplashScreen splash = new SplashScreen();
                splash.ShowDialog();
                Thread.Sleep(2000);
            });

            splashThread.Start();

            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка загрузки данных");
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                splashThread.Abort();
            }
        }

        private async void LoadData()
        {
            try
            {
                var data = _artNormService.GetArtNormData();
                artnormnBindingSource.DataSource = data;
                ANNgridControl.DataSource = artnormnBindingSource;
                
                var designer = _artNormService.GetRelDesigner();
                designerComboBox.DataSource = designer;
                var constructor = _artNormService.GetRelDesigner();
                constructorComboBox.DataSource = constructor;

                GridView view = ANNgridControl.MainView as GridView;
                if (view != null)
                {
                    int annId = CommonFunctions.GetRowCellValueOrDefault<int>(view, 0, "annId", 0);
                    //LoadRelatedData(annId);
                }

                filterTable();
                await _logger.LogEventAsync("данные загружены успешно", "LoadData");
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
                DataTable relatedData;

                // Если включен чекбокс "Загрузить все"
                if (loadAllCheckBox.Checked)
                {
                    relatedData = _artNormService.GetArtNormDataCurrent(kod, true);
                }
                else
                {
                    relatedData = _artNormService.GetArtNormDataCurrent(kod, false);

                    // Если артикул содержит "-", фильтруем по его первой части
                    int dashIndex = articul.IndexOf("-");
                    if (dashIndex > 0)
                    {
                        DataTable partialData = _artNormService.GetArtNormDataCurrent(articul.Substring(0, dashIndex));
                        relatedData.Merge(partialData);
                    }
                }

                // Преобразуем DataTable в BindingList<MyDataANN>
                BindingList<MyDataANN> myDataList = new BindingList<MyDataANN>();

                foreach (DataRow row in relatedData.Rows)
                {
                    myDataList.Add(new MyDataANN
                    {
                        AnnId = Convert.ToInt32(row["annId"]),
                        Kod = Convert.ToInt32(row["kod"]),
                        Articul = row["articul"].ToString(),
                        Status = Convert.ToInt32(row["status"]),
                        //Stat = row["stat"].ToString(),
                        Group = row["grup"].ToString(),
                        Model = row["mod"].ToString(),
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
            if (e.FocusedRowHandle < 0 || !GridHelper.IsDataTableLoaded(ANNgridView))
                return;

            try
            {
                var view = ANNgridView;

                commentRichTextBox.Text = CommonFunctions.GetRowCellValueOrDefault<string>(view, e.FocusedRowHandle, "komment", "");
                constructorComboBox.SelectedValue = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "constr", 0);
                designerComboBox.SelectedValue = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "diz", 0);

                int annId = CommonFunctions.GetRowCellValueOrDefault<int>(view, e.FocusedRowHandle, "annId", 0);
                //UpdateRelatedData(annId);
                LoadRelatedData(annId);

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

        /// <summary>
        /// Загружает данные в указанный GridControl через BindingSource.
        /// </summary>
        /// <param name="grid">Целевой GridControl</param>
        /// <param name="source">BindingSource, привязанный к данным</param>
        /// <param name="data">DataTable с новыми данными</param>
        private async void LoadGridControlData(GridControl grid, BindingSource source, DataTable data)
        {
            try
            {
                if (data == null)
                {
                    source.DataSource = null;
                }
                else
                {
                    source.DataSource = data;
                }

                grid.DataSource = source;
                grid.RefreshDataSource();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке данных в GridControl");
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Загружает изображение в PictureBox по идентификатору разделения труда.
        /// </summary>
        /// <param name="pictureBox">Целевой PictureBox</param>
        /// <param name="annId">Идентификатор разделения труда</param>
        private async void LoadGridControlData(PictureBox pictureBox, int kod)
        {
            try
            {
                DataTable dt = _artNormService.GetImage(kod);
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

        private void LoadRelatedData(int annId)
        {
            GridHelper.LoadGridControlData(gridControl1, normraszBindingSource, _artNormService.GetRelatedNormRasz(annId));
            GridHelper.LoadGridControlData(gridControl3, normraskBindingSource, _artNormService.GetRelatedNormRask(annId));
            GridHelper.LoadGridControlData(gridControl4, normkontBindingSource, _artNormService.GetRelatedNormKont(annId));
            GridHelper.LoadGridControlData(gridControl5, normdopobrBindingSource, _artNormService.GetRelatedNormDopObr(annId));
            GridHelper.LoadGridControlData(customGridControl5, sparticulBindingSource, _artNormService.GetRelatedSpArt(annId));
            //GridHelper.LoadImage(pictureBox1, _artNormService.GetImage(annId)); //не надо annId

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

        private void gridView8_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            int annId = CommonFunctions.GetRowCellValueOrDefault<int>(gridView8, e.FocusedRowHandle, "annId", 0);
            LoadRelatedData(annId);
        }

        private void gridView7_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            int kod = CommonFunctions.GetRowCellValueOrDefault<int>(gridView7, e.FocusedRowHandle, "kod", 0);
            string articul = CommonFunctions.GetRowCellValueOrDefault<string>(gridView7, e.FocusedRowHandle, "articul", "");

            customGridControl2.DataSource = LoadWorksbyArt(kod, articul);
        }


        /// <summary>
        /// Обновляет данные в связанных таблицах при смене разделения труда (РТ).
        /// </summary>
        /// <param name="annId">Идентификатор разделения труда</param>
        private async void UpdateRelatedData(int annId)
        {
            try
            {
                LoadGridControlData(gridControl1, normraszBindingSource, _artNormService.GetRelatedNormRasz(annId));
                LoadGridControlData(gridControl3, normraskBindingSource, _artNormService.GetRelatedNormRask(annId));
                LoadGridControlData(gridControl4, normkontBindingSource, _artNormService.GetRelatedNormKont(annId));
                LoadGridControlData(gridControl5, normdopobrBindingSource, _artNormService.GetRelatedNormDopObr(annId));
                LoadGridControlData(customGridControl5, sparticulBindingSource, _artNormService.GetRelatedSpArt(annId));

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

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string searchText = searchControl1.Text.TrimEnd(' ');//filterTextBox1.Text.Trim();
            string columnName = GridHelper.GetSelectedColumnName(kode.Checked, articul.Checked, model.Checked, group.Checked);

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
                string columnName = GridHelper.GetSelectedColumnName(kode.Checked, articul.Checked, model.Checked, group.Checked); // Определяем, по какой колонке искать

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


        private void searchControl1_QueryIsSearchColumn(object sender, DevExpress.XtraEditors.QueryIsSearchColumnEventArgs args)
        {
            string colName = GridHelper.GetSelectedColumnName(kode.Checked, articul.Checked, model.Checked, group.Checked);
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
        private void customCheckBox1_CheckedChanged(object sender, EventArgs e) => filterTable();

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
                            _artNormService.ResetAnnId(kod);

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
        /// <param name="gridView">`GridView`, где произошло изменение</param>
        /// <param name="e">Аргумент события `CellValueChangedEventArgs`</param>
        private async void GridView_CellValueChanged<T>(GridView gridView, CellValueChangedEventArgs e) where T : class
        {
            try
            {
                if (e.Column.FieldName == "IsChecked")
                {
                    GridHelper.UpdateExclusiveCheck<T>(gridView, e.RowHandle);
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
        private void customButton6_Click(object sender, EventArgs e)
        {
            GridView view = ANNgridView;
            if (view == null) return;

            ArtNormData newData = new ArtNormData();

            // Создаём новую строку в `DataSet`
            DataRow newRow = aCEDataSet.art_norm_n.NewRow();
            newRow["kod"] = newData.Kod;
            newRow["grup"] = newData.Grup;
            newRow["articul"] = newData.Articul;
            newRow["mod"] = newData.Mod;
    //        newRow["po"] = newData.Po;
            newRow["sek_shv"] = newData.SekShv;
 //           newRow["sek_vyaz3"] = newData.SekVyaz3;
            newRow["sek_vyaz5"] = newData.SekVyaz5;
            newRow["sek_vyaz6"] = newData.SekVyaz6;
            newRow["sek_vyaz7"] = newData.SekVyaz7;
            newRow["sek_vyaz10"] = newData.SekVyaz10;
            newRow["sek_vyaz12"] = newData.SekVyaz12;
  //          newRow["sek_vyaz62"] = newData.SekVyaz62;
  //          newRow["sek_vyaz71"] = newData.SekVyaz71;
  //          newRow["sek_vyaz72"] = newData.SekVyaz72;
            newRow["sek_vyazo"] = newData.SekVyazo;
            newRow["sek_vyaz"] = newData.SekVyaz;
            newRow["sek"] = newData.Sek;
 //           newRow["seb"] = newData.Seb;
 //           newRow["st"] = newData.St;
    //        newRow["po1"] = newData.Po1;
            newRow["komment"] = newData.Komment;
            newRow["data_sozd"] = newData.DataSozd;
            newRow["diz"] = newData.Diz;
            newRow["constr"] = newData.Constr;
            newRow["data_obn"] = newData.DataObn ?? (object)DBNull.Value;
  //          newRow["sek_vyaz70"] = newData.SekVyaz70;
            newRow["sek_kr"] = newData.SekKr;
            newRow["slogn"] = newData.Slogn;
  //          newRow["sek_vyaz14"] = newData.SekVyaz14;
            newRow["arh"] = newData.Arh;
 //           newRow["sql_pr_add"] = newData.SqlPrAdd;
 //           newRow["date_add"] = newData.DateAdd;
 //           newRow["komp_name"] = newData.KompName;
  //          newRow["annDateDel"] = newData.AnnDateDel ?? (object)DBNull.Value;
  //          newRow["annCompDel"] = newData.AnnCompDel ?? (object)DBNull.Value;
 //           newRow["annDateAdd"] = newData.AnnDateAdd;
 //           newRow["annCompAdd"] = newData.AnnCompAdd;
            newRow["status"] = newData.Status;

            // Сохраняем в БД через SQL-запрос и получаем `ID`
            int newId = SaveToDatabase(newRow)-1;//!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (newId <= 0)
            {
                MessageBox.Show("Ошибка сохранения в БД!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Обновляем `ID` новой строки в `DataSet`
            newRow["annID"] = newId;
            aCEDataSet.art_norm_n.Rows.Add(newRow);

            // Обновляем `GridView`
            ANNgridControl.RefreshDataSource();

            // Ищем строку в `GridView` по `ID`
            int realRowHandle = view.LocateByValue("annId", newId);//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (realRowHandle >= 0 && view.IsDataRow(realRowHandle))
            {
                view.FocusedRowHandle = realRowHandle;

                // Открываем `EditForm`
                using (TeamWork_AdvanceTW teamWork_AdvanceTW = new TeamWork_AdvanceTW(newId, bufferWorkDivision, (int)Mode.NewWorkDivision))
                {
                    if (teamWork_AdvanceTW.ShowDialog() == DialogResult.OK)
                    {
                        // Дополнительные действия после закрытия формы
                    }
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Новая строка не найдена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private int SaveToDatabase(DataRow row)
        {
           int newId = -1;
           newId= _artNormService.InsertANN(row);
           return newId;
        }

        public class ArtNormData
    {
        public string Kod { get; set; } = "0000000";
        public string Grup { get; set; } = "Группа";
        public string Articul { get; set; } = "Артикул";
        public string Mod { get; set; } = "Модель";
        public string Po { get; set; } = "0";
        public int SekShv { get; set; } = 0;
        public int SekVyaz3 { get; set; } = 0;
        public int SekVyaz5 { get; set; } = 0;
        public int SekVyaz6 { get; set; } = 0;
        public int SekVyaz7 { get; set; } = 0;
        public int SekVyaz10 { get; set; } = 0;
        public int SekVyaz12 { get; set; } = 0;
        public int SekVyaz62 { get; set; } = 0;
        public int SekVyaz71 { get; set; } = 0;
        public int SekVyaz72 { get; set; } = 0;
        public int SekVyazo { get; set; } = 0;
        public int SekVyaz { get; set; } = 0;
        public int Sek { get; set; } = 0;
        public decimal Seb { get; set; } = 0.00000m;
        public int St { get; set; } = 0;
        public string Po1 { get; set; } = "0";
        public string Komment { get; set; } = "Комментарий";
        public DateTime DataSozd { get; set; } = DateTime.Now;
        public int Diz { get; set; } = 0;
        public int Constr { get; set; } = 0;
        public DateTime? DataObn { get; set; } = null;
        public int SekVyaz70 { get; set; } = 0;
        public int SekKr { get; set; } = 0;
        public int Slogn { get; set; } = 0;
        public int SekVyaz14 { get; set; } = 0;
        public bool Arh { get; set; } = false;
        public int SqlPrAdd { get; set; } = 0;
        public DateTime DateAdd { get; set; } = DateTime.Now;
        public string KompName { get; set; } = Environment.MachineName;
        public DateTime? AnnDateDel { get; set; } = null;
        public string AnnCompDel { get; set; } = null;
        public DateTime AnnDateAdd { get; set; } = DateTime.Now;
        public string AnnCompAdd { get; set; } = Environment.MachineName;
        public int Status { get; set; } = 1;
}


        /// <summary>
        /// Архив+копия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customButton10_Click(object sender, EventArgs e)
        {
            GridView AnnView = ANNgridView;
            if (AnnView == null) return;
            if (gridView10.RowCount > 0)
            {
                int nzp = (int)gridView10.GetRowCellValue(0, "kolNZP");
                int newId = 0;//найти новый айди и присвоить
                if (nzp > 0)
                {
                    CopyRow();
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
                    CopyRow();
                    using (TeamWork_AdvanceTW teamWork_AdvanceTW = new TeamWork_AdvanceTW(newId,(int)ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "annId"), (int)Mode.ArchAndCopy))
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
        private void CopyRow()
        {
            int selectedRowHandle = gridView1.FocusedRowHandle;
            if (selectedRowHandle < 0)
            {
                MessageBox.Show("Выберите РТ для копирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var oldRow = gridView1.GetDataRow(selectedRowHandle);
            if (oldRow == null) return;

            DataTable table = gridView1.DataSource as DataTable;
            if (table == null) return;

            DataRow newRow = table.NewRow();

            foreach (DataColumn column in table.Columns)
            {
                if (column.ColumnName != "annId")
                {
                    newRow[column.ColumnName] = oldRow[column.ColumnName];
                }
            }

            table.Rows.Add(newRow);
            int newID = SaveCopyToDatabase(newRow);
            newRow["annId"] = newID;

            gridView1.RefreshData();

            int newRowHandle = gridView1.LocateByValue("annId", newID);
            gridView1.FocusedRowHandle = newRowHandle;

            gridView1.ShowPopupEditForm();
        }
        private int SaveCopyToDatabase(DataRow newRow)
        {
            try
            {
                // Сохраняем изменения из DataSet в БД
                art_norm_nTableAdapter.Update(aCEDataSet.art_norm_n);
                art_norm_nTableAdapter.Fill(aCEDataSet.art_norm_n); // Обновляем DataSet после сохранения

                // Получаем последний ID, добавленный в DataSet
                DataRow lastRow = aCEDataSet.art_norm_n.Rows[aCEDataSet.art_norm_n.Rows.Count - 1];
                return Convert.ToInt32(lastRow["id"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        #endregion

    }
}
