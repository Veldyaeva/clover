using DevExpress.Data.Filtering;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTab;
using SewingProduction.form;
using SewingProduction.Helpers;
using SewingProduction.Interfaces;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SewingProduction.Forms
{
    public partial class TeamWork : CustomForm
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly ArtNormService _artNormService;
        private int selectedRowHandle = -1;
        private readonly ILogger _logger = new FileLogger();
        private readonly GridHelper _gridHelper = new GridHelper();
        private readonly SplitContainerHelper _splitContainerHelper = new SplitContainerHelper();
        private int bufferId = 0;
        private BindingList<ArtNormN> _bindingList = new BindingList<ArtNormN>();
        private BindingSource _bindingSource = new BindingSource();
        private bool _hasUnsavedChanges = false;


        public TeamWork()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _artNormService = new ArtNormService(_dbHelper);
            ThemeManager.UpdateTheme(this);

            // Инициализация привязок данных
            _bindingSource.DataSource = _bindingList;
            if (ANNgridControl != null)
            {
                ANNgridControl.DataSource = _bindingSource;
            }

            // Настройка гридов
            if (customGridControl1 != null && customGridControl1.MainView is GridView view7)
            {
                view7.OptionsSelection.MultiSelect = false;
                view7.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            }

            if (customGridControl2 != null && customGridControl2.MainView is GridView view8)
            {
                view8.OptionsSelection.MultiSelect = false;
                view8.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            }
        }


        private async void TeamWorkForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Проверяем инициализацию компонентов
                if (customGridControl1 == null || customGridControl2 == null || ANNgridControl == null)
                {
                    throw new InvalidOperationException("Критические компоненты формы не инициализированы");
                }

                // Загружаем настройки гридов
                LoadGridSettings();

                // Инициализируем привязки данных
                _bindingSource.DataSource = _bindingList;
                ANNgridControl.DataSource = _bindingSource;

                // Загружаем данные
                await LoadWorkDivisions();

                // Обновляем UI
                ANNgridControl.RefreshDataSource();
                customGridControl1.RefreshDataSource();
                customGridControl2.RefreshDataSource();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке формы");
                MessageBox.Show($"Ошибка при инициализации формы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void LoadGridSettings()
        //{
        //    try
        //    {
        //        if (customGridControl1 != null && customGridControl1.MainView != null)
        //        {
        //            _gridHelper.LoadGridViewSettings(customGridControl1.MainView, "customGridControl1Layout.xml");
        //        }
        //        if (customGridControl2 != null && customGridControl2.MainView != null)
        //        {
        //            _gridHelper.LoadGridViewSettings(customGridControl2.MainView, "customGridControl2Layout.xml");
        //        }
        //        if (ANNgridControl != null && ANNgridControl.MainView != null)
        //        {
        //            _gridHelper.LoadGridViewSettings(ANNgridControl.MainView, "ANNgridControlLayout.xml");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogErrorAsync(ex, "Ошибка при загрузке настроек гридов");
        //    }
        //}

        /// <summary>
        /// Обработчик смены активной вкладки
        /// </summary>
        private async void XtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (xtraTabControl1.SelectedTabPage.Name)
            {
                case "xtraTabPageWorkDivisions":
                    await LoadWorkDivisions();
                    break;

                case "xtraTabPageArticles":
                    await CurrentWorks_Load();
                    break;
            }
        }



        /// <summary>
        /// Обработка закрытия формы
        /// </summary>
        private async void TeamWork_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_hasUnsavedChanges)
            {
                var result = MessageBox.Show(
                    "Есть несохраненные изменения. Вы уверены, что хотите выйти?",
                    "Подтверждение закрытия",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            SaveGridSettings();
            await _logger.LogEventAsync("Форма TeamWork закрыта", "FormClosing");
        }
        //public TeamWork()
        //{
        //    InitializeComponent();
        //    _dbHelper = new DatabaseHelper("ace");
        //    _artNormService = new ArtNormService(_dbHelper);

        //    ThemeManager.UpdateTheme(this);

        //    // Set up single row selection for both grid controls
        //    var view7 = customGridControl1.MainView as GridView;
        //    var view8 = customGridControl2.MainView as GridView;

        //    if (view7 != null)
        //    {
        //        view7.OptionsSelection.MultiSelect = false;
        //        view7.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
        //    }

        //    if (view8 != null)
        //    {
        //        view8.OptionsSelection.MultiSelect = false;
        //        view8.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
        //    }

        //    this.gridView7.CellValueChanged += (s, e) => GridView_CellValueChanged<MyDataART>(customGridControl1, e);
        //    this.gridView8.CellValueChanged += (s, e) => GridView_CellValueChanged<MyDataANN>(customGridControl2, e);

        //    // _bindingList = new BindingList<ArtNormN>();
        //    _bindingSource.DataSource = _bindingList; //= new BindingSource { DataSource = _bindingList };
        //    ANNgridControl.DataSource = _bindingSource;

        //}


        //#region Загрузка данных





        ///// <summary>
        ///// Загружает список разделений труда (РТ) для указанного артикула или кода.
        ///// </summary>
        ///// <param name="kod">Код артикула</param>
        ///// <param name="articul">Название артикула</param>
        ///// <returns>Список разделений труда (BindingList&lt;MyDataANN&gt;)</returns>
        ///// 
        //private async Task<BindingList<MyDataANN>> LoadWorksbyArt(int kod, string articul)
        //{
        //    try
        //    {
        //        List<ArtNormN> relatedData;
        //        bool loadAll = loadAllCheckBox.Checked;
        //        // Если включен чекбокс "Загрузить все"
        //        //relatedData = //ConvertDataTableToList<ArtNormN>(await _artNormService.GetArtNormDataCurrent(kod, loadAll));
        //        relatedData = await _artNormService.GetArtNormDataCurrent(kod, loadAll);
        //        if (!loadAll)
        //        { // Если артикул содержит "-", фильтруем по его первой части
        //            int dashIndex = articul.IndexOf("-");
        //            if (dashIndex > 0)
        //            {
        //                List<ArtNormN> partialData = await _artNormService.GetArtNormDataCurrent(articul.Substring(0, dashIndex));
        //                if (partialData != null)
        //                    relatedData.AddRange(partialData);
        //            }
        //        }

        //        BindingList<MyDataANN> myDataList = ConvertToMyDataAnn(relatedData);

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
        //BindingList<MyDataANN> ConvertToMyDataAnn(List<ArtNormN> _relatedData)
        //{
        //    // Преобразуем List<ArtNormN> в BindingList<MyDataANN>
        //    BindingList<MyDataANN> myDataList = new BindingList<MyDataANN>();

        //    foreach (var item in _relatedData)
        //    {
        //        myDataList.Add(new MyDataANN
        //        {
        //            AnnId = item.AnnID,
        //            Kod = item.Kod,
        //            Articul = item.Articul,
        //            Status = item.Status,
        //            Group = item.Group,
        //            Model = item.Mod,
        //            IsChecked = false
        //        });
        //    }

        //    return myDataList;
        //}

        ////private async Task<BindingList<MyDataANN>> LoadWorksbyArt(int kod, string articul)
        ////{
        ////    try
        ////    {
        ////        List<ArtNormN> relatedData;

        ////        // Если включен чекбокс "Загрузить все"
        ////        if (loadAllCheckBox.Checked)
        ////        {
        ////            relatedData = await _artNormService.GetArtNormDataCurrent(kod, true);
        ////        }
        ////        else
        ////        {
        ////            relatedData = await _artNormService.GetArtNormDataCurrent(kod, false);

        ////            // Если артикул содержит "-", фильтруем по его первой части
        ////            int dashIndex = articul.IndexOf("-");
        ////            if (dashIndex > 0)
        ////            {
        ////                List<ArtNormN> partialData = await _artNormService.GetArtNormDataCurrent(articul.Substring(0, dashIndex));
        ////                foreach (var item in partialData)
        ////                { relatedData.Add(item); }
        ////            }
        ////        }

        ////        // Преобразуем DataTable в BindingList<MyDataANN>
        ////        BindingList<MyDataANN> myDataList = new BindingList<MyDataANN>();

        ////        foreach (var row in myDataList)
        ////        {
        ////            myDataList.Add(new MyDataANN
        ////            {
        ////                AnnId = row.AnnId,
        ////                Kod = row.Kod,
        ////                Articul = row.Articul,
        ////                Status = row.Status,
        ////                Stat  = row.Stat,
        ////                Group = row.Group,
        ////                Model = row.Model,
        ////                IsChecked = false
        ////            });
        ////        }
        ////        await _logger.LogEventAsync($"Успешная загрузка РТ для кода {kod} и артикула {articul}", "LoadWorksbyArt");
        ////        return myDataList;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        await _logger.LogErrorAsync(ex, $"Ошибка загрузки РТ для кода {kod} и артикула {articul}");
        ////        MessageBox.Show("Ошибка загрузки данных. Подробности в логе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        ////        return new BindingList<MyDataANN>(); // Возвращаем пустой список в случае ошибки
        ////    }
        ////}
        //private List<T> ConvertDataTableToList<T>(DataTable table) where T : new()
        //{
        //    List<T> list = new List<T>();

        //    foreach (DataRow row in table.Rows)
        //    {
        //        T obj = new T();
        //        foreach (DataColumn column in table.Columns)
        //        {
        //            var property = typeof(T).GetProperty(column.ColumnName);
        //            if (property != null && row[column] != DBNull.Value)
        //            {
        //                property.SetValue(obj, Convert.ChangeType(row[column], property.PropertyType));
        //            }
        //        }
        //        list.Add(obj);
        //    }

        //    return list;
        //}
        //#endregion
        //#region Обработка смены строки


        //#endregion

        //#region Загрузка данных LoadGridControlData

        ///// <summary>
        ///// Загружает изображение в PictureBox по идентификатору разделения труда.
        ///// </summary>
        ///// <param name="pictureBox">Целевой PictureBox</param>
        ///// <param name="kod">Идентификатор разделения труда</param>
        //private async void LoadGridControlData(PictureBox pictureBox, int kod)
        //{
        //    try
        //    {
        //        DataTable dt = await _artNormService.GetImage(kod);
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            pictureBox.ImageLocation = dt.Rows[0]["pathpict"].ToString();
        //        }
        //        else
        //        {
        //            pictureBox.Image = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка загрузки изображения для kod = {kod}");
        //        pictureBox.Image = null;
        //    }
        //}

        ///// <summary>
        ///// Применяет фильтр к GridView на основе annId.
        ///// </summary>
        ///// <param name="grid">GridControl, в котором нужно применить фильтр</param>
        ///// <param name="source">источник данных</param>
        ///// <param name="_annId">Идентификатор разделения труда</param>
        //private async void LoadGridControlData(GridControl grid, BindingSource source, int _annId)
        //{
        //    try
        //    {
        //        string filter = "annId = " + _annId;
        //        GridView view = (GridView)grid.Views[0];

        //        view.BeginUpdate();
        //        view.ActiveFilterString = filter;
        //        view.EndUpdate();
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка фильтрации данных для annId = {_annId}");
        //    }
        //}
        //#endregion

        //#region Работа с данными

        //private async Task LoadRelatedData(int annId)
        //{
        //    await GridHelper.LoadGridControlDataAsync(gridControl1, normraszBindingSource, await _artNormService.GetRelatedNormRasz(annId));
        //    //await GridHelper.LoadGridControlDataAsync(gridControl1, normraszBindingSource, _artNormService.GetRelatedNormRasz1(annId));
        //    await GridHelper.LoadGridControlDataAsync(gridControl3, normraskBindingSource, await _artNormService.GetRelatedNormRask(annId));
        //    await GridHelper.LoadGridControlDataAsync(gridControl4, normkontBindingSource, await _artNormService.GetRelatedNormKont(annId));
        //    await GridHelper.LoadGridControlDataAsync(gridControl5, normdopobrBindingSource, await _artNormService.GetRelatedNormDopObr(annId));
        //    await GridHelper.LoadGridControlDataAsync(customGridControl5, sparticulBindingSource, await _artNormService.GetRelatedSpArt(annId));
        //    //  await GridHelper.LoadGridControlDataAsync(gridControlPreArch, sparticulBindingSource1, await _artNormService.GetRelatedSpArt(annId));
        //    UpdateNZPStatus();
        //}

        //private async void UpdateNZPStatus()
        //{
        //    try
        //    {
        //        var view = customGridControl5.MainView as GridView;
        //        if (view == null || view.FocusedRowHandle < 0) return;

        //        int nzp = CommonFunctions.GetRowCellValueOrDefault<int>(view, view.FocusedRowHandle, "kolNZP", 0);
        //        ButtonUnboundWd.Enabled = nzp <= 0;

        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, "Ошибка обновления статуса НЗП");
        //        MessageBox.Show($"Ошибка при обновлении NZP: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private async void gridView3_InitNewRow(object sender, InitNewRowEventArgs e)
        //{
        //    GridView view = sender as GridView;
        //    if (view == null) return;

        //    try
        //    {
        //        //view.SetRowCellValue(e.RowHandle, "annId", 0);
        //        //view.SetRowCellValue(e.RowHandle, "kod", 0);
        //        //view.SetRowCellValue(e.RowHandle, "articul", string.Empty);
        //        //view.SetRowCellValue(e.RowHandle, "status", 1);
        //        //view.SetRowCellValue(e.RowHandle, "group", string.Empty);
        //        //view.SetRowCellValue(e.RowHandle, "model", string.Empty);
        //        //view.SetRowCellValue(e.RowHandle, "stat", "Новый");
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, "Ошибка при добавлении новой строки");
        //        MessageBox.Show($"Ошибка при добавлении новой строки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void gridView3_ColumnFilterChanged(object sender, EventArgs e)
        //{
        //    // searchControl1.ClearFilter();
        //}



        ///// <summary>
        ///// Обновляет данные в связанных таблицах при смене разделения труда (РТ).
        ///// </summary>
        ///// <param name="annId">Идентификатор разделения труда</param>
        //private async void UpdateRelatedData(int annId)
        //{
        //    try
        //    {
        //        await GridHelper.LoadGridControlDataAsync(gridControl1, normraszBindingSource, await _artNormService.GetRelatedNormRasz(annId));
        //        await GridHelper.LoadGridControlDataAsync(gridControl3, normraskBindingSource, await _artNormService.GetRelatedNormRask(annId));
        //        await GridHelper.LoadGridControlDataAsync(gridControl4, normkontBindingSource, await _artNormService.GetRelatedNormKont(annId));
        //        await GridHelper.LoadGridControlDataAsync(gridControl5, normdopobrBindingSource, await _artNormService.GetRelatedNormDopObr(annId));
        //        await GridHelper.LoadGridControlDataAsync(customGridControl5, sparticulBindingSource, await _artNormService.GetRelatedSpArt(annId));

        //        UpdateNZPStatus(); // Обновляем статус незавершенного производства
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка обновления данных для annId = {annId}");
        //        MessageBox.Show($"Ошибка обновления данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


        //#endregion

        //#region Поиск и фильтрация

        //private async void SearchButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string searchText = searchControl1.Text.TrimEnd(' ');
        //        string columnName = await GridHelper.GetSelectedColumnNameAsync(kode.Checked, articul.Checked, model.Checked, group.Checked);

        //        if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(searchText))
        //        {
        //            // Создаем фильтр поиска
        //            var searchFilter = new FunctionOperator(
        //            FunctionOperatorType.Contains,
        //            new OperandProperty(columnName),
        //            new OperandValue(searchText));

        //            // Создаем фильтры на основе состояния чекбоксов
        //            CriteriaOperator statusCriteria = null;

        //            // Создаем фильтр по статусу
        //            if (preliminaryCheckBox.Checked || actualCheckBox.Checked || archiveCheckBox.Checked)
        //            {
        //                var statusFilters = new List<CriteriaOperator>();

        //                if (preliminaryCheckBox.Checked)
        //                    statusFilters.Add(new BinaryOperator("status", (int)Status.Preliminary));

        //                if (actualCheckBox.Checked)
        //                {
        //                    statusFilters.Add(new BinaryOperator("status", (int)Status.Actual));
        //                    statusFilters.Add(new BinaryOperator("status", (int)Status.PreliminaryArchive));
        //                }

        //                if (archiveCheckBox.Checked)
        //                    statusFilters.Add(new BinaryOperator("status", (int)Status.Archive));

        //                if (statusFilters.Count > 1)
        //                {
        //                    statusCriteria = new GroupOperator(GroupOperatorType.Or, statusFilters.ToArray());
        //                }
        //                else if (statusFilters.Count == 1)
        //                {
        //                    statusCriteria = statusFilters[0];
        //                }
        //            }

        //            // Добавляем фильтр по "Не описанные" если выбран
        //            if (SortBox.Checked)
        //            {
        //                var notDescribedFilter = new GroupOperator(
        //                    GroupOperatorType.And,
        //                    new BinaryOperator("sek_shv", 0),
        //                    new BinaryOperator("status", 0, DevExpress.Data.Filtering.BinaryOperatorType.Greater)
        //                );

        //                if (statusCriteria != null)
        //                {
        //                    statusCriteria = new GroupOperator(
        //                        GroupOperatorType.And,
        //                        statusCriteria,
        //                        notDescribedFilter
        //                    );
        //                }
        //                else
        //                {
        //                    statusCriteria = notDescribedFilter;
        //                }
        //            }

        //            // Если есть фильтр статуса, объединяем его с фильтром поиска
        //            if (statusCriteria != null)
        //            {
        //                ANNgridView.ActiveFilterCriteria = new GroupOperator(
        //                    GroupOperatorType.And,
        //                    searchFilter,
        //                    statusCriteria
        //                );
        //            }
        //            else
        //            {
        //                ANNgridView.ActiveFilterCriteria = searchFilter;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, "Ошибка при поиске в SearchButton_Click");
        //        MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void SearchButton_Click(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    // Вызываем основной метод поиска
        //    SearchButton_Click(sender, new EventArgs());
        //}

        ///// <summary>
        ///// Фильтрация данных в gridView3 по введенному значению в filterTextBox1.
        ///// </summary>
        //private async void customButton12_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string filterString = filterTextBox1.Text.Trim(); // Получаем текст из поля ввода
        //        string columnName = await GridHelper.GetSelectedColumnNameAsync(kode.Checked, articul.Checked, model.Checked, group.Checked); // Определяем, по какой колонке искать

        //        if (!string.IsNullOrEmpty(filterString) && !string.IsNullOrEmpty(columnName))
        //        {
        //            // Применяем фильтр к gridView3
        //            ANNgridView.ActiveFilterCriteria = new DevExpress.Data.Filtering.FunctionOperator(
        //                DevExpress.Data.Filtering.FunctionOperatorType.Contains,
        //                new DevExpress.Data.Filtering.OperandProperty(columnName),
        //                new DevExpress.Data.Filtering.OperandValue(filterString)
        //            );
        //        }
        //        else
        //        {
        //            MessageBox.Show("Введите значение для поиска и выберите колонку!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, "Ошибка при поиске по customButton12_Click");
        //        MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


        //private async void searchControl1_QueryIsSearchColumn(object sender, DevExpress.XtraEditors.QueryIsSearchColumnEventArgs args)
        //{
        //    string colName = await GridHelper.GetSelectedColumnNameAsync(kode.Checked, articul.Checked, model.Checked, group.Checked);
        //    args.IsSearchColumn = args.FieldName == colName;
        //}

        //private async void customCheckBox4_CheckedChanged(object sender, EventArgs e)
        //{
        //    int kod = CommonFunctions.GetRowCellValueOrDefault<int>(gridView7, gridView7.FocusedRowHandle, "kod", 0);
        //    string articul = CommonFunctions.GetRowCellValueOrDefault<string>(gridView7, gridView7.FocusedRowHandle, "articul", "");

        //    BindingList<MyDataANN> list = loadAllCheckBox.Checked ?
        //        await LoadWorksbyArt(0, "") :
        //        await LoadWorksbyArt(kod, articul);
        //    customGridControl2.DataSource = list;//loadAllCheckBox.Checked ? LoadWorksbyArt(0, "") : LoadWorksbyArt(kod, articul);
        //}

        //private void simpleButton2_Click(object sender, EventArgs e)
        //{
        //    //   new TeamWork_AdvanceTW(bufferWorkDivision, (int)Mode.NewWorkDivision).ShowDialog();
        //}

        ///// <summary>
        ///// Применяет фильтры к данным в gridView3
        ///// </summary>
        //private void filterTable()
        //{
        //    try
        //    {
        //        // Сохраняем текущий фильтр поиска, если он есть
        //        CriteriaOperator searchFilter = null;
        //        if (ANNgridView.ActiveFilterCriteria is GroupOperator groupFilter)
        //        {
        //            // Проверяем, есть ли фильтр поиска в группе операторов
        //            foreach (var criteria in groupFilter.Operands)
        //            {
        //                if (criteria is FunctionOperator functionOp &&
        //                    functionOp.OperatorType == FunctionOperatorType.Contains)
        //                {
        //                    searchFilter = criteria;
        //                    break;
        //                }
        //            }
        //        }
        //        else if (ANNgridView.ActiveFilterCriteria is FunctionOperator functionFilter &&
        //                 functionFilter.OperatorType == FunctionOperatorType.Contains)
        //        {
        //            searchFilter = functionFilter;
        //        }

        //        // Создаем фильтры на основе состояния чекбоксов
        //        CriteriaOperator statusCriteria = null;
        //        GroupOperator statusGroup = null;

        //        // Создаем фильтр по статусу
        //        if (preliminaryCheckBox.Checked || actualCheckBox.Checked || archiveCheckBox.Checked)
        //        {
        //            var statusFilters = new List<CriteriaOperator>();

        //            if (preliminaryCheckBox.Checked)
        //                statusFilters.Add(new BinaryOperator("status", (int)Status.Preliminary));

        //            if (actualCheckBox.Checked)
        //            {
        //                statusFilters.Add(new BinaryOperator("status", (int)Status.PreliminaryArchive));
        //                statusFilters.Add(new BinaryOperator("status", (int)Status.Actual));
        //            }

        //            if (archiveCheckBox.Checked)
        //                statusFilters.Add(new BinaryOperator("status", (int)Status.Archive));

        //            if (statusFilters.Count > 1)
        //            {
        //                statusGroup = new GroupOperator(GroupOperatorType.Or, statusFilters.ToArray());
        //                statusCriteria = statusGroup;
        //            }
        //            else if (statusFilters.Count == 1)
        //            {
        //                statusCriteria = statusFilters[0];
        //            }
        //        }

        //        // Добавляем фильтр по "Не описанные" если выбран
        //        if (SortBox.Checked)
        //        {
        //            var notDescribedFilter = new GroupOperator(
        //                GroupOperatorType.And,
        //                new BinaryOperator("sek_shv", 0),
        //                new BinaryOperator("status", 0, DevExpress.Data.Filtering.BinaryOperatorType.Greater)
        //            );

        //            if (statusCriteria != null)
        //            {
        //                statusCriteria = new GroupOperator(
        //                    GroupOperatorType.And,
        //                    statusCriteria,
        //                    notDescribedFilter
        //                );
        //            }
        //            else
        //            {
        //                statusCriteria = notDescribedFilter;
        //            }
        //        }

        //        // Если есть и фильтр поиска, и фильтр статуса
        //        if (searchFilter != null && statusCriteria != null)
        //        {
        //            ANNgridView.ActiveFilterCriteria = new GroupOperator(
        //                GroupOperatorType.And,
        //                searchFilter,
        //                statusCriteria
        //            );
        //        }
        //        else if (searchFilter != null)
        //        {
        //            // Только фильтр поиска
        //            ANNgridView.ActiveFilterCriteria = searchFilter;
        //        }
        //        else if (statusCriteria != null)
        //        {
        //            // Только фильтр статуса
        //            ANNgridView.ActiveFilterCriteria = statusCriteria;
        //        }
        //        else
        //        {
        //            // Нет фильтров
        //            ANNgridView.ActiveFilterString = string.Empty;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogErrorAsync(ex, "Ошибка при применении фильтра");
        //    }
        //}

        ///// <summary>
        ///// Переключение фильтров при изменении чекбоксов
        ///// </summary>
        //private void Filter_CheckedChanged(object sender, EventArgs e) => filterTable();

        ///// <summary>
        ///// Обработчик смены выбранного поля поиска при изменении параметров поиска.
        ///// </summary>
        //private async void search_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Получаем текст текущего поиска
        //        string searchText = searchControl1.Text.TrimEnd(' ');

        //        // Очищаем текущий фильтр поиска
        //        searchControl1.ClearFilter();

        //        // Если есть текст поиска, применяем его к новому выбранному полю
        //        if (!string.IsNullOrEmpty(searchText))
        //        {
        //            // Создаем фильтр поиска для нового выбранного поля
        //            string columnName = await GridHelper.GetSelectedColumnNameAsync(kode.Checked, articul.Checked, model.Checked, group.Checked);
        //            if (!string.IsNullOrEmpty(columnName))
        //            {
        //                var searchFilter = new FunctionOperator(
        //                    FunctionOperatorType.Contains,
        //                    new OperandProperty(columnName),
        //                    new OperandValue(searchText));

        //                // Получаем текущий фильтр статусов
        //                CriteriaOperator statusFilter = GetStatusFilter();

        //                // Если есть фильтр статусов, объединяем его с новым фильтром поиска
        //                if (statusFilter != null)
        //                {
        //                    ANNgridView.ActiveFilterCriteria = new GroupOperator(
        //                        GroupOperatorType.And,
        //                        searchFilter,
        //                        statusFilter
        //                    );
        //                }
        //                else
        //                {
        //                    ANNgridView.ActiveFilterCriteria = searchFilter;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            // Если нет текста поиска, применяем только фильтр статусов
        //            CriteriaOperator statusFilter = GetStatusFilter();
        //            if (statusFilter != null)
        //            {
        //                ANNgridView.ActiveFilterCriteria = statusFilter;
        //            }
        //            else
        //            {
        //                ANNgridView.ActiveFilterString = string.Empty;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, "Ошибка при обновлении параметров поиска");
        //    }
        //}

        //private CriteriaOperator GetStatusFilter()
        //{
        //    // Создаем фильтры на основе состояния чекбоксов
        //    if (preliminaryCheckBox.Checked || actualCheckBox.Checked || archiveCheckBox.Checked)
        //    {
        //        var statusFilters = new List<CriteriaOperator>();

        //        if (preliminaryCheckBox.Checked)
        //            statusFilters.Add(new BinaryOperator("status", (int)Status.Preliminary));

        //        if (actualCheckBox.Checked)
        //        {
        //            statusFilters.Add(new BinaryOperator("status", (int)Status.Actual));
        //            statusFilters.Add(new BinaryOperator("status", (int)Status.PreliminaryArchive));
        //        }
        //        if (archiveCheckBox.Checked)
        //            statusFilters.Add(new BinaryOperator("status", (int)Status.Archive));

        //        if (statusFilters.Count > 1)
        //        {
        //            return new GroupOperator(GroupOperatorType.Or, statusFilters.ToArray());
        //        }
        //        else if (statusFilters.Count == 1)
        //        {
        //            return statusFilters[0];
        //        }
        //    }

        //    // Добавляем фильтр по "Не описанные" если выбран
        //    if (SortBox.Checked)
        //    {
        //        return new GroupOperator(
        //            GroupOperatorType.And,
        //            new BinaryOperator("sek_shv", 0),
        //            new BinaryOperator("status", 0, DevExpress.Data.Filtering.BinaryOperatorType.Greater)
        //        );
        //    }

        //    return null;
        //}

        //#endregion

        //#region текущие работы - требуют увязки
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    if (!Enabled)
        //    {
        //        using (var brush = new SolidBrush(Color.Gray))
        //        {
        //            e.Graphics.FillRectangle(brush, ClientRectangle);
        //        }
        //    }
        //}
        //#endregion
    }
}

