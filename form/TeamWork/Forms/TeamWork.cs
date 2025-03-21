using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using SewingProduction.Interfaces;
using SewingProduction.form;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;

namespace SewingProduction.Forms
{
    public partial class TeamWork : CustomForm, IArtNormController, IAnnManager, INormRaszManager, INormRaskManager
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly ArtNormService _artNormService;
        private readonly ILogger _logger = new FileLogger();
        private readonly GridHelper _gridHelper = new GridHelper();
        private AnnManager _annManager;
        private NormRaszManager _normRaszManager;
        private NormRaskManager _normRaskManager;
        private int bufferWorkDivision;

        private BindingList<ArtNormN> _annBindingList;
        private readonly BindingSource _annBindingSource;
        private BindingList<NormRasz> _raszBindingList;
        private readonly BindingSource _raszBindingSource;
        private BindingList<NormRask> _raskBindingList;
        private readonly BindingSource _raskBindingSource;
        private int selectedRowHandle = -1;

        public TeamWork()
        {
            InitializeComponent();

            _dbHelper = new DatabaseHelper("ace");
            _artNormService = new ArtNormService(_dbHelper);
            _annBindingList = new BindingList<ArtNormN>();
            _annBindingSource = new BindingSource { DataSource = _annBindingList };
            _raszBindingList = new BindingList<NormRasz>();
            _raszBindingSource = new BindingSource { DataSource = _raszBindingList };
            _raskBindingList = new BindingList<NormRask>();
            _raskBindingSource = new BindingSource { DataSource = _raskBindingList };

            ANNgridControl.DataSource = _annBindingSource;
            FormClosing += TeamWork_FormClosing;
            _annManager = new AnnManager(_artNormService, _logger, _annBindingSource, ANNgridControl, ANNgridView);
            _normRaszManager = new NormRaszManager(_artNormService, _raszBindingSource);
            _normRaskManager = new NormRaskManager(_artNormService, _raskBindingSource);
        }

        private async void TeamWorkForm_Load(object sender, EventArgs e)
        {
            await LoadSettingsAsync();
            await LoadCurrentDataAsync();
        }

        private async Task LoadCurrentDataAsync()
        {
            try
            {
                _annBindingList.Clear();
                var data = await _artNormService.GetArtNormData();

                if (data != null && data.Count > 0)
                {
                    //foreach (var item in data)
                    //{
                    //    _annBindingList.Add(item);
                    //}
                    _annBindingList = data;
                }
                else
                {
                    MessageBox.Show("Нет данных для загрузки.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                RefreshData();
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
                _annBindingSource.ResetBindings(false);
                ANNgridControl.RefreshDataSource();
                ANNgridView.RefreshData();
                ANNgridView.PopulateColumns();
                filterTable();
            });
        }
        private async void TeamWork_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _gridHelper.SaveGridViewSettings(ANNgridView, "ANNgridViewLayout.xml");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при сохранении настроек");
            }
        }

        #region IArtNormController реализация
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
                await LoadCurrentDataAsync(annId);

                string kod = CommonFunctions.GetRowCellValueOrDefault<string>(view, e.FocusedRowHandle, "kod", "");
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
        public async Task LoadCurrentDataAsync(int annId)
        {
            try
            {
                var data = await _artNormService.GetArtNormData();
                _annBindingList = new BindingList<ArtNormN>(data ?? new List<ArtNormN>());
                _annBindingSource.DataSource = _annBindingList;
                
                // Создаем словарь с GridControl и их BindingSource для централизованной загрузки данных
                var controls = new Dictionary<GridControl, BindingSource>
                {
                    { gridControl1, normraszBindingSource },
                    { gridControl3, normraskBindingSource },
                    { gridControl4, normkontBindingSource },
                    { gridControl5, normdopobrBindingSource },
                    { customGridControl5, sparticulBindingSource }
                };
                
                // Используем метод GridHelper для загрузки связанных данных
                await GridHelper.LoadCurrentDataAsync(
                    annId,
                    _artNormService,
                    controls,
                    pictureBox1,
                    _annBindingSource,
                    _annBindingList,
                    _logger as HybridLogger
                );

                UpdateNZPStatus();
                RefreshGrid();
                await _logger.LogEventAsync("Данные успешно загружены", "LoadCurrentData");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка загрузки данных");
                MessageBox.Show("Ошибка загрузки данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        public async Task<BindingList<MyDataANN>> LoadWorksByArtAsync(int kod, string articul)
        {
            try
            {
                var data = await _artNormService.GetArtNormDataCurrent(kod, loadAllCheckBox.Checked);
                if (!loadAllCheckBox.Checked && articul.Contains("-"))
                {
                    var prefix = articul.Split('-')[0];
                    var extra = await _artNormService.GetArtNormDataCurrent(prefix);
                    data.AddRange(extra);
                }

                var result = new BindingList<MyDataANN>();
                foreach (var item in data)
                {
                    result.Add(new MyDataANN
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

                return result;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка в LoadWorksByArtAsync");
                return new BindingList<MyDataANN>();
            }
        }

        #endregion


        private async void customButton12_Click(object sender, EventArgs e)
        {
            ApplySearchFilter();
        }
        
        private void filterTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ApplySearchFilter();
        }

        private async void ApplySearchFilter()
        {
            try
            {
                string filterString = searchControl1.Text.Trim();
                string columnName = await GridHelper.GetSelectedColumnNameAsync(kode.Checked, articul.Checked, model.Checked, group.Checked);

                if (!string.IsNullOrEmpty(filterString) && !string.IsNullOrEmpty(columnName))
                {
                    ANNgridView.ActiveFilterCriteria = new DevExpress.Data.Filtering.FunctionOperator(
                        DevExpress.Data.Filtering.FunctionOperatorType.Contains,
                        new DevExpress.Data.Filtering.OperandProperty(columnName),
                        new DevExpress.Data.Filtering.OperandValue(filterString)
                    );
                    await _logger.LogEventAsync($"Применен фильтр по колонке {columnName}: {filterString}", "Filter");
                }
                else
                {
                    MessageBox.Show("Введите значение для поиска и выберите колонку!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при применении фильтра");
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
            int kod = CommonFunctions.GetRowCellValueOrDefault<int>(gridView7, gridView7.FocusedRowHandle, "Kod", 0);
            string articul = CommonFunctions.GetRowCellValueOrDefault<string>(gridView7, gridView7.FocusedRowHandle, "Articul", "");

            BindingList<MyDataANN> list = loadAllCheckBox.Checked ?
                await LoadWorksByArtAsync(0, "") :
                await LoadWorksByArtAsync(kod, articul);
            customGridControl2.DataSource = list;//loadAllCheckBox.Checked ? LoadWorksbyArt(0, "") : LoadWorksbyArt(kod, articul);
        }

        /// <summary>
        /// Применяет фильтры к данным в gridView3
        /// </summary>
        private void filterTable()
        {
            string filterString = "";

            if (preliminaryCheckBox.Checked) filterString += $"[status] = {(int)Status.Preliminary}";
            if (actualCheckBox.Checked) filterString += (filterString.Length > 0 ? " OR " : "") + $"[Status] = {(int)Status.Actual}";
            if (archiveCheckBox.Checked) filterString += (filterString.Length > 0 ? " OR " : "") + $"[Status] = {(int)Status.Archive}";

            if (filterString.Length > 0) filterString = "(" + filterString + ")";
            if (SortBox.Checked) filterString += (filterString.Length > 0 ? " AND " : "") + "[Sek_shv] = 0 AND [Status] > 0";

            ANNgridView.ActiveFilterString = filterString;
        }

        /// <summary>
        /// Переключение фильтров при изменении чекбоксов
        /// </summary>
        private void Filter_CheckedChanged(object sender, EventArgs e) => filterTable();

        /// <summary>
        /// Сбрасывает текущий фильтр и текст поиска при изменении параметров поиска.
        /// </summary>
        private async void search_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // Сбрасываем текст поиска
                searchControl1.Text = string.Empty;
                
                // Сбрасываем любые установленные фильтры
                ANNgridView.ActiveFilterCriteria = null;
                searchControl1.ClearFilter();
                
                // Очищаем поисковый фильтр
                _gridHelper.ClearSearchFilter(ANNgridView);
                
                await _logger.LogEventAsync("Сброшен фильтр поиска из-за изменения колонки", "FilterReset");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при сбросе фильтра поиска");
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

                await _logger.LogEventAsync("Привязка завершена", $"Артикул {selectedArt} привязан к РТ {selectedAnn}");
                MessageBox.Show("Привязка успешно выполнена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при привязке артикула к РТ");
                MessageBox.Show($"Ошибка при привязке артикула: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                 //   await CopyRow();
                    using (TeamWork_AdvanceTW teamWork_AdvanceTW = new TeamWork_AdvanceTW(newId, (int)ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "AnnID"), (int)Mode.ArchAndCopy))
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
                  //  await CopyRow();
                    using (TeamWork_AdvanceTW teamWork_AdvanceTW = new TeamWork_AdvanceTW(newId, (int)ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "AnnID"), (int)Mode.ArchAndCopy))
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


            using (TeamWork_AdvanceTW teamWork_AdvanceTW = new TeamWork_AdvanceTW(0, (int)ANNgridView.GetRowCellValue(ANNgridView.FocusedRowHandle, "AnnID"), (int)Mode.Edit))
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

        #region Старые методы (для совместимости)

        private async Task LoadWorkDivisions()
        {
            try
            {
                var data = await _artNormService.GetArtNormData();
                if (data != null && data.Count > 0)
                {
                    _annBindingList = new BindingList<ArtNormN>(data);
                    _annBindingSource.DataSource = _annBindingList;
                }

                RefreshGrid();
                await _logger.LogEventAsync("Загрузка RT завершена", "LoadWorkDivisions");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке RT");
            }
        }

        private void RefreshGrid()
        {
            SafeInvoke(ANNgridControl, () =>
            {
                _annBindingSource.ResetBindings(false);
                ANNgridControl.RefreshDataSource();
                ANNgridView.RefreshData();
                filterTable();
            });
        }

        private void SafeInvoke(Control control, Action action)
        {
            if (control.InvokeRequired)
                control.Invoke(action);
            else
                action();
        }

        #endregion

        private async Task LoadSettingsAsync()
        {
            try
            {
                _gridHelper.LoadGridViewSettings(ANNgridView, "ANNgridViewLayout.xml");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке настроек грида");
            }
        }

        public Task LoadAnnByArticulAsync(int kod, string articul)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertNormRaszAsync(NormRasz normRasz)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertNormRaskAsync(NormRask normRask)
        {
            throw new NotImplementedException();
        }

        public Task<BindingList<MyDataANN>> LoadAnnByArtAsync(int kod, string articul)
        {
            throw new NotImplementedException();
        }

        public void RefreshAnnGrid()
        {
            throw new NotImplementedException();
        }

        public Task SaveAnnToDatabaseAsync(MyDataANN ann)
        {
            throw new NotImplementedException();
        }

        public Task<List<NormRasz>> LoadNormRaszAsync(int annId)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveNormRaszAsync(NormRasz norm)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> LoadRaskroyNormByGroupAsync(int groupId)
        {
            throw new NotImplementedException();
        }


        public Task LoadNormRask(int annId)
        {
            throw new NotImplementedException();
        }

        private void copyButton_Click(object sender, EventArgs e)
        {

        }

        // Обработчики событий для радиокнопок поиска
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // Вызываем очистку поиска при смене радиокнопки
            _gridHelper.OnSearchRadioButtonChanged(ANNgridView);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // Вызываем очистку поиска при смене радиокнопки
            _gridHelper.OnSearchRadioButtonChanged(ANNgridView);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            // Вызываем очистку поиска при смене радиокнопки
            _gridHelper.OnSearchRadioButtonChanged(ANNgridView);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            // Вызываем очистку поиска при смене радиокнопки
            _gridHelper.OnSearchRadioButtonChanged(ANNgridView);
        }

        public Task<BindingList<NormRasz>> LoadByAnnIdAsync(int annId)
        {
            return ((INormRaszManager)_normRaszManager).LoadByAnnIdAsync(annId);
        }

        Task<BindingList<NormRask>> INormRaskManager.LoadByAnnIdAsync(int annId)
        {
            return ((INormRaskManager)_normRaskManager).LoadByAnnIdAsync(annId);
        }

        public Task<BindingList<MyDataANN>> LoadByArtAsync(int kod, string articul, bool loadAll)
        {
            return ((IAnnManager)_annManager).LoadByArtAsync(kod, articul, loadAll);
        }
    }
}
