using DevExpress.ChartRangeControlClient.Core;
using DevExpress.CodeParser.VB;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ButtonPanel;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSpreadsheet.Import.Xls;
using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Features.TeamWork;
using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.Features.TeamWork.Services;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.form;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Report;
using SewingProduction.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BindingSource = System.Windows.Forms.BindingSource;
using PopupMenuShowingEventHandler = DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork : CustomForm
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly DbService _dbService;
        private readonly ArtNormService _artNormService;
        private int selectedRowHandle = -1;
        private readonly ILogger _logger = new FileLogger();
        private readonly TWGridHelper _gridHelper = new TWGridHelper();
        private readonly SplitContainerHelper _splitContainerHelper = new SplitContainerHelper();
        private readonly FormSettingsHelper _formSettingsHelper = new FormSettingsHelper();
        private readonly SecondsUpdateManager _secondsUpdateManager;
        private TeamWorkService _teamWorkService;
        private UIHelper _uiHelper;
        private int bufferId = 0;
        private BindingList<ArtNormN> _bindingList;
        private BindingSource _bindingSource;
        private bool _hasUnsavedChanges = false;
        private BindingSource _nzpByKoddRtSourceArt;
        private BindingSource _nzpByKoddRtSourceWd;
        private BindingList<NZPByKoddRt> _nzpListArt;
        private BindingList<NZPByKoddRt> _nzpListWd;
        private BindingList<NormRasz> _normRaszListTW;
        private BindingSource _normRaszBindingSourceTW;
        private BindingList<NormRask> _normRaskListTW;
        private BindingSource _normRaskBindingSourceTW;
        private BindingList<NormKont> _normKontListTW;
        private BindingSource _normKontBindingSourceTW;
        private static List<FioModel> _cachedFioData;
        private List<FioModel> fioList;
        private BindingList<MyDataANN> _preArchList;
        private BindingSource _preArchBindingSource;
        private BindingList<MyDataART> _myDataArtList;
        private BindingSource _myDataArtBindingSource;
        private BindingList<MyDataANN> _myDataAnnList;
        private BindingSource _myDataAnnBindingSource;

        private BindingList<MyDataART> _boundArtList;
        private BindingSource _boundArtBindingSource;

        // BindingList и BindingSource схемы разденеий на вкладке Articles 
        private BindingList<NormRasz> _normRaszListArticles;
        private BindingSource _normRaszBindingSourceArticles;

        private BindingList<NormRask> _normRaskListArticles;
        private BindingSource _normRaskBindingSourceArticles;

        private BindingList<NormKont> _normKontListArticles;
        private BindingSource _normKontBindingSourceArticles;

        private List<KodProizvModel> kodProizvList;
        private List<PodrVyazModel> podrVyazList;
        private List<OborudShvModel> oborudShvList;

        private CancellationTokenSource _loadCts = new CancellationTokenSource();


        public TeamWork(UserClass user) : base(user)
        {
            InitializeComponent();


            ANNgridView.OptionsView.ShowPreview = true;
            ANNgridView.PreviewLineCount = 1;
            //  ANNgridView.CalcPreviewText += CalcPreviewText;
            DapperMappings.Configure();
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);
            _artNormService = new ArtNormService(_dbHelper);
            _secondsUpdateManager = new SecondsUpdateManager(_artNormService, _logger);
            _teamWorkService = new TeamWorkService(_artNormService, _dbService, _logger);
            _uiHelper = new UIHelper(_logger);

            // Инициализация основных BindingList и BindingSource
            _bindingList = new BindingList<ArtNormN>();
            _bindingSource = new BindingSource { DataSource = _bindingList };
            if (ANNgridControl != null) ANNgridControl.DataSource = _bindingSource;

            _preArchList = new BindingList<MyDataANN>();
            _preArchBindingSource = new BindingSource { DataSource = _preArchList };
            if (gridControlPreArch != null) gridControlPreArch.DataSource = _preArchBindingSource;

            _nzpListArt = new BindingList<NZPByKoddRt>();
            _nzpByKoddRtSourceArt = new BindingSource { DataSource = _nzpListArt };
            if (gridControlNZP != null) gridControlNZP.DataSource = _nzpByKoddRtSourceArt;

            _nzpListWd = new BindingList<NZPByKoddRt>();
            _nzpByKoddRtSourceWd = new BindingSource { DataSource = _nzpListWd };
            if (customGridControl4 != null) customGridControl4.DataSource = _nzpByKoddRtSourceWd;

            // Инициализация для вкладки "Работа с артикулами"
            _myDataArtList = new BindingList<MyDataART>();
            _myDataArtBindingSource = new BindingSource { DataSource = _myDataArtList };
            if (gridControl_unboundArts != null) gridControl_unboundArts.DataSource = _myDataArtBindingSource;

            _myDataAnnList = new BindingList<MyDataANN>();
            _myDataAnnBindingSource = new BindingSource { DataSource = _myDataAnnList };
            if (gridControl_wdToBind != null) gridControl_wdToBind.DataSource = _myDataAnnBindingSource;

            _boundArtList = new BindingList<MyDataART>();
            _boundArtBindingSource = new BindingSource { DataSource = _boundArtList };
            if (gridControl_binded != null) gridControl_binded.DataSource = _boundArtBindingSource;

            _normRaszListArticles = new BindingList<NormRasz>();
            _normRaszBindingSourceArticles = new BindingSource { DataSource = _normRaszListArticles };
            if (customGridControl3 != null) customGridControl3.DataSource = _normRaszBindingSourceArticles;

            InitializeGridSettings();
            SetupDateUpdateColumn();
        }

        /// <summary>
        /// Настраивает базовые параметры гридов
        /// </summary>
        private void InitializeGridSettings()
        {
            // Настройка гридов (общие настройки, не связанные с данными DataSource)
            if (gridControl_unboundArts?.MainView is GridView unboundArtsView)
            {
                ConfigureGridSelection(unboundArtsView, "unboundArts");
                unboundArtsView.CellValueChanged += (s, e) => GridView_CellValueChanged<MyDataART>(gridControl_unboundArts, e);
                unboundArtsView.CellValueChanging += (s, e) => GridView_CellValueChanged<MyDataART>(gridControl_unboundArts, e);
            }

            if (gridControl_wdToBind?.MainView is GridView wdToBindView)
            {
                ConfigureGridSelection(wdToBindView, "wdToBind");
                wdToBindView.CellValueChanging += (s, e) => GridView_CellValueChanged<MyDataANN>(gridControl_wdToBind, e);
            }

            ANNgridView.CalcPreviewText += CalcPreviewText;
        }
        /// <summary>
        /// Настраивает колонку dateUpdate с кнопкой для проставления даты
        /// </summary>
        private void SetupDateUpdateColumn()
        {
            var commandsEditDateNull = new RepositoryItemButtonEdit { TextEditStyle = TextEditStyles.HideTextEditor };
            commandsEditDateNull.Buttons.Clear();
            commandsEditDateNull.Buttons.Add(new EditorButton(ButtonPredefines.Glyph, "Проставить дату", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleLeft, DemoHelper.GetEditImage()));
            commandsEditDateNull.DoubleClick -= CommandsEditDateNull_DoubleClick;
            commandsEditDateNull.DoubleClick += CommandsEditDateNull_DoubleClick;

            // Репозиторий для отображения только текста
            var commandsEditDateText = new RepositoryItemTextEdit();
            commandsEditDateText.ReadOnly = true;

            GridColumn colDateUpdate = ANNgridView.Columns["dateUpdate"];
            if (colDateUpdate != null)
            {
                // Устанавливаем формат отображения даты без времени
                colDateUpdate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                colDateUpdate.DisplayFormat.FormatString = "dd.MM.yyyy";

                ANNgridView.CustomRowCellEdit += (s, e) =>
                {
                    if (e.Column == colDateUpdate)
                    {
                        var dateUpdate = ANNgridView.GetRowCellValue(e.RowHandle, "dateUpdate");
                        if (dateUpdate == null || string.IsNullOrEmpty(dateUpdate.ToString()))
                            e.RepositoryItem = commandsEditDateNull;
                        else e.RepositoryItem = commandsEditDateText;
                    }
                };
            }
        }


        /// <summary>
        /// Очищает элементы управления поиском на форме
        /// </summary>
        private void ClearSearchControls()
        {
            try
            {
                // Очищаем текстовые поля поиска, если они есть на форме
                // Например, если есть searchTextEdit
                var searchControls = this.Controls.Find("searchTextEdit", true);
                foreach (Control control in searchControls)
                {
                    if (control is TextEdit textEdit)
                    {
                        textEdit.Text = string.Empty;
                    }
                }

                // Очищаем другие элементы поиска
                var comboBoxes = this.Controls.OfType<ComboBoxEdit>().Where(cb => cb.Name.Contains("search", StringComparison.OrdinalIgnoreCase));
                foreach (var comboBox in comboBoxes)
                {
                    comboBox.SelectedIndex = -1;
                    comboBox.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, "Ошибка при очистке элементов управления поиском");
            }
        }
        /// <summary>
        /// Применяет базовые настройки к гриду (сброс фильтров, сортировки и восстановление стандартных настроек)
        /// </summary>
        private void ApplyBaseGridSettings(GridView gridView, string gridName = "")
        {
            if (gridView == null) return;

            try
            {
                gridView.BeginUpdate();

                // Сбрасываем фильтры и сортировку
                gridView.ActiveFilter.Clear();
                gridView.ClearSorting();
                gridView.ClearGrouping();
                // Очищаем строку поиска
                gridView.ActiveFilterString = string.Empty;

                // Очищаем быстрый поиск (если есть)
                if (gridView.FindFilterText != null)
                {
                    gridView.FindFilterText = string.Empty;
                }

                // Очищаем автофильтры в колонках
                foreach (GridColumn column in gridView.Columns)
                {
                    column.FilterInfo = new ColumnFilterInfo();
                }

                // Применяем базовые настройки отображения
                gridView.OptionsView.EnableAppearanceEvenRow = true;
                gridView.OptionsView.EnableAppearanceOddRow = true;
                gridView.OptionsView.ShowAutoFilterRow = true;
                gridView.OptionsView.ShowGroupPanel = false;
                gridView.OptionsView.ShowIndicator = true;

                // Для основного грида ANNgridView - специальные настройки
                if (gridView == ANNgridView)
                {
                    //gridView.OptionsView.ShowPreview = true;
                    //gridView.PreviewLineCount = 1;

                    // Восстанавливаем базовую сортировку
                    if (gridView.Columns["AnnID"] != null)
                    {
                        gridView.Columns["AnnID"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                    }

                    // Восстанавливаем настройки колонки dateUpdate
                    RestoreDateUpdateColumnSettings(gridView);
                }

                // Настройки выбора для разных гридов
                ConfigureGridSelection(gridView, gridName);
            }
            finally
            {
                gridView.EndUpdate();
            }
        }

        /// <summary>
        /// Настраивает параметры выбора для грида
        /// </summary>
        private void ConfigureGridSelection(GridView gridView, string gridName)
        {
            gridView.OptionsSelection.MultiSelect = false;
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;

            // Можно добавить специфические настройки для разных гридов
            switch (gridName.ToLower())
            {
                case "unboundarts":
                case "wdtobind":
                    // Дополнительные настройки для этих гридов
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Восстанавливает настройки колонки dateUpdate
        /// </summary>
        private void RestoreDateUpdateColumnSettings(GridView gridView)
        {
            try
            {
                GridColumn colDateUpdate = gridView.Columns["dateUpdate"];
                if (colDateUpdate != null)
                {
                    // Устанавливаем формат отображения даты без времени
                    colDateUpdate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    colDateUpdate.DisplayFormat.FormatString = "dd.MM.yyyy";
                }
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, "Ошибка при восстановлении настроек колонки dateUpdate");
            }
        }


        /// <summary>
        /// Восстанавливает фокус на записи с указанным AnnID
        /// </summary>
        private async Task<bool> RestoreFocusAsync(int annId)
        {
            try
            {
                int rowHandle = ANNgridView.LocateByValue("AnnID", annId);

                if (rowHandle >= 0)
                {
                    ANNgridView.BeginUpdate();
                    try
                    {
                        ANNgridView.FocusedRowHandle = rowHandle;
                        ANNgridView.MakeRowVisible(rowHandle);
                        ANNgridView.RefreshRow(rowHandle);
                    }
                    finally
                    {
                        ANNgridView.EndUpdate();
                    }

                    // Загружаем связанные данные для восстановленной записи
                    await LoadRelatedData(annId);
                    await _logger.LogEventAsync($"Фокус восстановлен на AnnID: {annId}, RowHandle: {rowHandle}", "RestoreFocus");
                    return true;
                }
                else
                {
                    await _logger.LogEventAsync($"Запись с AnnID: {annId} не найдена после перезагрузки", "RestoreFocus");
                    return false;
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при восстановлении фокуса на AnnID: {annId}");
                return false;
            }
        }

        /// <summary>
        /// Применяет базовые фильтры к основному гриду
        /// </summary>
        private void ApplyBaseFilters()
        {
            try
            {
                if (ANNgridView == null) return;

                ANNgridView.BeginUpdate();

                // Применяем существующий метод фильтрации, если он есть
                filterTable();
            }
            finally
            {
                ANNgridView.EndUpdate();
            }
        }

        /// <summary>
        /// Показывает статусное сообщение пользователю
        /// </summary>
        private void ShowStatusMessage(string message)
        {
            try
            {
                // Можно показать в статусной строке или временно в заголовке
                this.Text = $"Нормативные расценки - {message}";

                // Через 3 секунды сбрасываем заголовок
                Task.Run(async () =>
                {
                    await Task.Delay(3000);
                    if (this.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)(() => this.Text = "Нормативные расценки"));
                    }
                    else
                    {
                        this.Text = "Нормативные расценки";
                    }
                });
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, "Ошибка при отображении статусного сообщения");
            }
        }

        private async void CommandsEditDateNull_DoubleClick(object sender, EventArgs e)
        {
            var view = ANNgridView;
            var rowHandle = view.FocusedRowHandle;
            var dateUpdate = view.GetRowCellValue(rowHandle, "dateUpdate");
            var annId = view.GetRowCellValue(rowHandle, "AnnID");
            // Действие только если дата не задана
            if (dateUpdate == null || dateUpdate == DBNull.Value || string.IsNullOrEmpty(dateUpdate.ToString()))
            {
                var result = MessageBox.Show("Обновить данные во всех справочниках?",
     "Пересчёт себ.",
     MessageBoxButtons.YesNo,
     MessageBoxIcon.Question,
     MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    var parameters = new Dictionary<string, object>
                    {
                        { "@xAnnID", annId}
                    };
                    await _dbHelper.ExecuteQueryAsync("EXEC dbo.updateSebZArticulPsz @xAnnID", parameters); //пересчитать себ. - при простановке даты обн

                    view.RefreshRowCell(rowHandle, view.Columns["dateUpdate"]);

                    await _dbService.UpdateFieldAsync(TableNames.Ann, "data_obn", DateTime.Now, TableNames.AnnId, annId);
                    await _dbService.UpdateFieldAsync(TableNames.Ann, "status", (int)Status.Actual, TableNames.AnnId, annId);
                    view.SetRowCellValue(rowHandle, "dateUpdate", DateTime.Now);
                    view.SetRowCellValue(rowHandle, "status", (int)Status.Actual);
                    view.SetRowCellValue(rowHandle, "StatusText", "Актуальное");

                }
            }
        }

        private void CalcPreviewText(object sender,
                                       CalcPreviewTextEventArgs e)
        {
            if (e.RowHandle >= 0 && ANNgridView.GetRow(e.RowHandle) is ArtNormN row)
            {
                e.PreviewText = $"Дизайнер: {row.Diz}, Конструктор: {row.Constr}, Особенности: {row.Komment}, Рекомендации: {row.Reco}";
            }
        }

        private int _lastFocusedAnnId = 0;

        private async void TeamWorkForm_Load(object sender, EventArgs e)
        {
            if (ANNgridView != null)
            {
                ANNgridView.FocusedRowChanged -= ANNgridView_FocusedRowChanged;
            }

            // Загружаем сохраненные настройки интерфейса
            LoadGridSettings();

            try
            {
                if (gridControl_unboundArts == null || gridControl_wdToBind == null || ANNgridControl == null)
                {
                    throw new InvalidOperationException("Критические компоненты формы не инициализированы.");
                }
                await LoadWorkDivisions();
                // После загрузки восстановим фокус, если есть сохранённый AnnID
                if (_lastFocusedAnnId > 0)
                {
                    await RestoreFocusAsync(_lastFocusedAnnId);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке формы TeamWork");
                MessageBox.Show($"Ошибка при инициализации формы TeamWork: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (ANNgridView != null)
                {
                    ANNgridView.FocusedRowChanged += ANNgridView_FocusedRowChanged;

                    // Фокус и связанные данные при фильтрации/поиске
                    ANNgridView.ColumnFilterChanged += (s, e2) => FocusFirstResultAndLoadRelated();

                    // Подписываемся на событие изменения текста поиска
                    if (ANNgridView.IsFocusedView && ANNgridView.RowCount > 0 && ANNgridView.FocusedRowHandle >= 0) // Проверка перед вызовом
                    {
                        ANNgridView_FocusedRowChanged_Internal(ANNgridView, new FocusedRowChangedEventArgs(-1, ANNgridView.FocusedRowHandle));
                    }

                    // Включаем подсветку строк
                    ApplyAnnGridRowStyling();
                }
            }
        }

        private void ApplyAnnGridRowStyling()
        {
            try
            {
                if (ANNgridView == null) return;
                ANNgridView.RowStyle -= ANNgridView_RowStyle;
                ANNgridView.RowStyle += ANNgridView_RowStyle;
            }
            catch { }
        }

        private void ANNgridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                var view = sender as GridView;
                if (view == null) return;
                if (e.RowHandle < 0) return;

                // Подсветка фокусной строки
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.Coral;//.FromArgb(255, 255, 230);
                    e.Appearance.BackColor2 = Color.Coral;//.FromArgb(255, 240, 200);
                    e.HighPriority = true;
                    return;
                }
            }
            catch { }
        }

        private void FocusFirstResultAndLoadRelated()
        {
            try
            {
                var gv = ANNgridView;
                if (gv == null) return;

                if (gv.DataRowCount <= 0)
                {
                    // опционально: очистить связанные таблицы, если нужен пустой показ
                    return;
                }

                int firstHandle = gv.GetVisibleRowHandle(0);
                if (!gv.IsValidRowHandle(firstHandle)) return;

                int prev = gv.FocusedRowHandle;

                gv.BeginUpdate();
                try
                {
                    gv.FocusedRowHandle = firstHandle;
                    gv.MakeRowVisible(firstHandle);
                    gv.RefreshRow(firstHandle);
                }
                finally
                {
                    gv.EndUpdate();
                }

                ANNgridView_FocusedRowChanged_Internal(gv, new FocusedRowChangedEventArgs(prev, firstHandle));
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, "Ошибка в FocusFirstResultAndLoadRelated");
            }
        }

        private async void XtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == null) return;

            switch (e.Page.Name)
            {
                case "TabPage1":
                    await LoadWorkDivisions();
                    break;

                case "xtraTabPageArticles":
                    // Загружаем данные для вкладки артикулов
                    await CurrentWorks_Load();
                    break;

                default:
                    // При переходе на другие вкладки можно добавить дополнительную логику если необходимо
                    break;
            }
        }


        private void ButtonEditWd_Click(object sender, EventArgs e)
        {

            EditWd_Internal2(ANNgridView, _bindingList, _bindingSource, Editing: false);
        }

        private void ButtonEditOnlyAdv_Click(object sender, EventArgs e)
        {
            EditWd_Internal2(ANNgridView, _bindingList, _bindingSource, Editing: true);
        }
        private async void ButtonArchAndCopyWd_Click(object sender, EventArgs e)
        {
            await ArchAndCopy(ANNgridView, _bindingList, _bindingSource, false);

        }

        private async void ButtonPreliminaryWd_Click(object sender, EventArgs e)
        {
            ButtonPreliminaryWd_Click_Internal(sender, e);
        }

        private async void ButtonCopyWd_Click(object sender, EventArgs e)
        {
            ButtonCopyWd_Click_Internal(sender, e);
        }
        private async void gridView5_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            gridView5_FocusedRowChanged_Internal(sender, e);
        }
        /// <summary>
        /// Обрабатывает смену строки в неувязанных артикулах
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void gridView_unboundArts_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            gridView_unboundArts_FocusedRowChanged_Internal(sender, e);
        }
        private void Filter_CheckedChanged(object sender, EventArgs e)
        {
            Filter_CheckedChanged_Internal(sender, e);
        }

        private void ANNgridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0 && ANNgridView.GetRow(e.FocusedRowHandle) is ArtNormN row)
                {
                    _lastFocusedAnnId = row.AnnID;
                }
            }
            catch { }
            ANNgridView_FocusedRowChanged_Internal(sender, e);
        }

        /// <summary>
        /// Обрабатывает изменение фильтра поиска в ANNgridView
        /// Автоматически переходит на первую строку результатов поиска
        /// </summary>
        private void ANNgridView_ActiveFilterChanged(object sender, EventArgs e)
        {
            try
            {
                var gridView = sender as GridView;
                if (gridView == null) return;

                // Проверяем, есть ли активный фильтр поиска
                if (gridView.ActiveFilterCriteria != null)
                {
                    // Небольшая задержка для завершения применения фильтра
                    //gridView.BeginInvoke(new Action(() =>
                    //{
                    try
                    {
                        // Проверяем, есть ли видимые строки после применения фильтра
                        if (gridView.DataRowCount > 0)
                        {
                            // Переходим на первую строку результатов поиска
                            int firstVisibleRow = gridView.GetVisibleRowHandle(0);
                            if (gridView.IsValidRowHandle(firstVisibleRow))
                            {
                                gridView.FocusedRowHandle = firstVisibleRow;
                                gridView.MakeRowVisible(firstVisibleRow);

                                // Логируем действие
                                _logger?.LogEventAsync($"Автоматический переход на первую строку результатов поиска. Всего строк: {gridView.DataRowCount}", "ANNgridView_ActiveFilterChanged");
                                ANNgridView_FocusedRowChanged_Internal(ANNgridView, new FocusedRowChangedEventArgs(-1, ANNgridView.FocusedRowHandle));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger?.LogErrorAsync(ex, "Ошибка при автоматическом переходе на первую строку результатов поиска");
                    }
                    // }));
                }
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, "Ошибка в обработчике изменения фильтра поиска");
            }
        }

        ///// <summary>
        ///// Обрабатывает изменение текста поиска в ANNgridView
        ///// Автоматически переходит на первую строку результатов поиска
        ///// </summary>
        //private void ANNgridView_FindFilterTextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var gridView = sender as GridView;
        //        if (gridView == null) return;

        //        // Проверяем, есть ли текст поиска
        //        if (!string.IsNullOrEmpty(gridView.FindFilterText))
        //        {
        //            // Небольшая задержка для завершения применения фильтра поиска
        //            gridView.BeginInvoke(new Action(() =>
        //            {
        //                try
        //                {
        //                    // Проверяем, есть ли видимые строки после применения поиска
        //                    if (gridView.DataRowCount > 0)
        //                    {
        //                        // Переходим на первую строку результатов поиска
        //                        int firstVisibleRow = gridView.GetVisibleRowHandle(0);
        //                        if (gridView.IsValidRowHandle(firstVisibleRow))
        //                        {
        //                            gridView.FocusedRowHandle = firstVisibleRow;
        //                            gridView.MakeRowVisible(firstVisibleRow);

        //                            // Логируем действие
        //                            _logger?.LogEventAsync($"Автоматический переход на первую строку результатов поиска по тексту '{gridView.FindFilterText}'. Всего строк: {gridView.DataRowCount}", "ANNgridView_FindFilterTextChanged");
        //                        }
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    _logger?.LogErrorAsync(ex, "Ошибка при автоматическом переходе на первую строку результатов поиска по тексту");
        //                }
        //            }));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger?.LogErrorAsync(ex, "Ошибка в обработчике изменения текста поиска");
        //    }
        //}

        private async void loadAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            loadAllCheckBox_CheckedChanged_Internal(sender, e);
        }

        private async void TeamWork_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Отписываемся от событий
            if (ANNgridView != null)
            {
                ANNgridView.FocusedRowChanged -= ANNgridView_FocusedRowChanged;
                ANNgridView.ColumnFilterChanged -= ANNgridView_ActiveFilterChanged;
                //ANNgridView.FindFilterTextChanged -= ANNgridView_FindFilterTextChanged;
            }

            _secondsUpdateManager?.CancelUpdate();
            _secondsUpdateManager?.Dispose();
            SaveGridSettings();
            await _logger.LogEventAsync("Форма TeamWork закрыта", "FormClosing");
        }

        /// <summary>
        /// Отображает статус обновления секунд
        /// </summary>
        private void ShowSecondsUpdateStatus(string message)
        {
            try
            {
                // Ищем statusLabel или используем заголовок формы
                if (this.Controls.Find("statusLabel", true).FirstOrDefault() is Label statusLabel)
                {
                    if (statusLabel.InvokeRequired)
                    {
                        statusLabel.Invoke((MethodInvoker)(() => statusLabel.Text = message));
                    }
                    else
                    {
                        statusLabel.Text = message;
                    }
                }
                else
                {
                    // Используем заголовок формы как индикатор
                    if (this.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)(() => this.Text = $"Нормативные расценки - {message}"));
                    }
                    else
                    {
                        this.Text = $"Нормативные расценки - {message}";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, "Ошибка при отображении статуса обновления секунд");
            }
        }

        /// <summary>
        /// Очищает статус обновления
        /// </summary>
        private void ClearSecondsUpdateStatus()
        {
            try
            {
                if (this.Controls.Find("statusLabel", true).FirstOrDefault() is Label statusLabel)
                {
                    if (statusLabel.InvokeRequired)
                    {
                        statusLabel.Invoke((MethodInvoker)(() => statusLabel.Text = ""));
                    }
                    else
                    {
                        statusLabel.Text = "";
                    }
                }
                else
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)(() => this.Text = "Нормативные расценки"));
                    }
                    else
                    {
                        this.Text = "Нормативные расценки";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, "Ошибка при очистке статуса");
            }
        }

        private async void customButton2_Click(object sender, EventArgs e)
        {
            await Arch(sender, e);
        }

        private async void BindButton_Click(object sender, EventArgs e)
        {
            await BindButton_Click_Internal(sender, e);
        }


        private async void customSimpleButton1_Click(object sender, EventArgs e)
        {
            await DuplicateWorkDivision_Click_Internal(ANNgridView, _bindingList, _bindingSource, false);
        }

        private async Task DuplicateWorkDivision_Click_Internal(GridView gridView, IList list, BindingSource bindingSource, bool forMyDataAnnView = false)
        {
            if (gridView == null || gridView.FocusedRowHandle < 0)
            {
                MessageBox.Show("Выберите Разделение Труда для дублирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedAnnToDuplicate = gridView.GetRow(gridView.FocusedRowHandle) as ArtNormN;
            int rowHandle = gridView.FocusedRowHandle;

            if (forMyDataAnnView)
            { // Вторая вкладка: объект - MyDataANN (нужно получить ArtNormN по AnnID)
                var selectedMyDataAnn = gridView.GetRow(rowHandle) as MyDataANN;
                if (selectedMyDataAnn == null) return;
                int annId = selectedMyDataAnn.AnnID;
                selectedAnnToDuplicate = await _artNormService.GetArtNormDataById(annId);
                if (selectedAnnToDuplicate == null) return;
            }
            if (selectedAnnToDuplicate == null)
            {
                MessageBox.Show("Не удалось получить данные выбранного РТ.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ArtNormN CopyedWorkDivisionShell = selectedAnnToDuplicate.CloneProperties();
            CopyedWorkDivisionShell.Status = (int)Status.Preliminary;
            CopyedWorkDivisionShell.StatusText = StatusHelper.GetStatusText((int)Status.Preliminary);
            CopyedWorkDivisionShell.dateCreate = DateTime.Now;
            CopyedWorkDivisionShell.dateUpdate = null;
            CopyedWorkDivisionShell.Arh = false;
            CopyedWorkDivisionShell.ParentId = selectedAnnToDuplicate.AnnID;
            CopyedWorkDivisionShell.AnnID = 0;
            CopyedWorkDivisionShell.Mod = "";
            CopyedWorkDivisionShell.Articul = "";
            CopyedWorkDivisionShell.grup = "";

            int newAnnId = await _dbService.InsertEntityAsync(TableNames.Ann, TableNames.AnnId, CopyedWorkDivisionShell);
            if (newAnnId <= 0)
            {
                MessageBox.Show("Ошибка при создании новой записи РТ в базе данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

			// Открываем форму редактирования клона (немодально)
			var teamWorkAdvanceTW = OpenAdvanceFormNonModal(bufferId, (int)Mode.Clone, newId: newAnnId, oldId: selectedAnnToDuplicate.AnnID);
			if (teamWorkAdvanceTW == null)
			{
				// Форма уже открыта или произошла ошибка
				return;
			}

			teamWorkAdvanceTW.FormClosed += async (s, args) =>
			{
				if (teamWorkAdvanceTW.DialogResult == DialogResult.OK)
				{
					CopyedWorkDivisionShell = teamWorkAdvanceTW.CreatedAnn;
					if (CopyedWorkDivisionShell == null) return;

					_bindingList.Add(CopyedWorkDivisionShell);
					_bindingSource.ResetBindings(false);
					rowHandle = ANNgridView.LocateByValue("AnnID", CopyedWorkDivisionShell.AnnID);
					if (rowHandle >= 0)
					{
						ANNgridView.BeginUpdate();
						try
						{
							ANNgridView.FocusedRowHandle = rowHandle;
							ANNgridView.MakeRowVisible(rowHandle);
							ANNgridView.RefreshRow(rowHandle);
						}
						finally
						{
							ANNgridView.EndUpdate();
						}
					}
				}
				else
				{
					// Возврат к исходной строке
					rowHandle = ANNgridView.LocateByValue("AnnID", selectedAnnToDuplicate.AnnID);
					if (rowHandle >= 0)
					{
						ANNgridView.BeginUpdate();
						try
						{
							ANNgridView.FocusedRowHandle = rowHandle;
							ANNgridView.MakeRowVisible(rowHandle);
							ANNgridView.RefreshRow(rowHandle);
						}
						finally
						{
							ANNgridView.EndUpdate();
						}
					}

					// Удаляем созданную запись из списка и базы
					_bindingList.Remove(CopyedWorkDivisionShell);
					_bindingSource.ResetBindings(false);
					await _artNormService.DeleteByAnnId(TableNames.Ann, newAnnId);
				}
			};
        }
        private async void layoutControlGroup2_CustomButtonClick(object sender, BaseButtonEventArgs e)
        {
            int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);

            switch (buttonIndex)
            {
                case 2:
                    ButtonPreliminaryWd_Click_Internal(sender, e); // Первая кнопка
                    break;
                case 4:
                    //EditWd_Internal2(gridView_wdToBind, _myDataAnnList, _myDataAnnBindingSource, forMyDataAnnView: true);
                    await DuplicateWorkDivision_Click_Internal(gridView_wdToBind, _myDataAnnList, _myDataAnnBindingSource, forMyDataAnnView: true); // Вторая кнопка
                    break;
                case 6:
                    await ArchAndCopy(gridView_wdToBind, _myDataAnnList, _myDataAnnBindingSource, true); // Третья кнопка
                    break;

            }
        }

        private void ButtonUnboundWd_Click(object sender, EventArgs e)
        {
            UnboundWD(sender, e);
        }

        private async void ANNgridView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Upd")
            {
                GridView view = sender as GridView;
                if (view != null)
                {
                    ArtNormN row = view.GetRow(e.RowHandle) as ArtNormN;
                    if (row != null)
                    {
                        if (e.Value is bool val && val)
                        {
                            row.dateUpdate = DateTime.Now;
                            _hasUnsavedChanges = true;

                            try
                            {
                                decimal updatedSeb = await _artNormService.getArtNormnSeb(row.AnnID);
                                row.Seb = updatedSeb;
                                row.dateUpdate = DateTime.Now;
                            }
                            catch (Exception ex)
                            {
                                await _logger.LogErrorAsync(ex, $"Ошибка при вызове getArtNormnSeb для AnnID: {row.AnnID}");
                                MessageBox.Show("Ошибка при обновлении данных после вызова процедуры: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            view.RefreshRow(e.RowHandle);
                        }
                    }
                }
            }
        }

        private async void layoutControlGroup8_CustomButtonClick(object sender, BaseButtonEventArgs e)
        {
            int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);

            switch (buttonIndex)
            {
                case 0:
                    //Debug.WriteLine(ButtonPreliminaryWd.Enabled + " " + ButtonPreliminaryWd.Visible);
                    if (ButtonPreliminaryWd.Enabled && ButtonPreliminaryWd.Visible)
                        ButtonPreliminaryWd_Click_Internal(sender, e);
                    break;
                case 2:
                    //Debug.WriteLine(ButtonEditWd.Enabled + " " + ButtonEditWd.Visible);
                    if (ButtonEditWd.Enabled && ButtonEditWd.Visible)
                        if (ButtonEditOnlyAdv.Enabled && ButtonEditOnlyAdv.Visible)
                            await EditWd_Internal2(ANNgridView, _bindingList, _bindingSource, Editing: true);
                        else
                            await EditWd_Internal2(ANNgridView, _bindingList, _bindingSource, Editing: false);
                    break;
                case 4:
                    //Debug.WriteLine(customSimpleButton1.Enabled + " " + customSimpleButton1.Visible);
                    if (ButtonDouble.Enabled && ButtonDouble.Visible)
                        await DuplicateWorkDivision_Click_Internal(ANNgridView, _bindingList, _bindingSource);
                    break;
                case 6:
                    //Debug.WriteLine(ButtonArchAndCopyWd.Enabled + " " + ButtonArchAndCopyWd.Visible);
                    if (ButtonArchAndCopyWd.Enabled && ButtonArchAndCopyWd.Visible)
                        await SetArchiveStatus_Internal(sender, e);//МЕНЯЮ НА АРХИВ для Чирковой
                    //ArchAndCopy(ANNgridView, _bindingList, _bindingSource, false);
                    break;
                case 9:
                    //Debug.WriteLine(PrintButton.Enabled + " " + PrintButton.Visible);
                    if (PrintButton.Enabled && PrintButton.Visible)
                        // Отчет технологической схемы разделения труда
                        PrintWorkDivisionScheme_Click(null, null);
                    break;
                case 11:
                    if (printButtonPlus.Enabled && printButtonPlus.Visible)
                        // Отчет технологической схемы разделения труда
                        printButtonPlus_Click(null, null);
                    break;

            }
        }

        private void ANNgridView_CalcPreviewText(object sender, CalcPreviewTextEventArgs e)
        {
            if (e.RowHandle >= 0 && ANNgridView.GetRow(e.RowHandle) is ArtNormN row)
            {
                e.PreviewText = $"Дизайнер: {row.Diz}, Конструктор: {row.Constr}, Особенности: {row.Komment}, Рекомендации: {row.Reco}";
            }
        }

        private void layoutControlGroup6_CustomButtonClick(object sender, BaseButtonEventArgs e)
        {
            simpleButton2_Click_Internal(sender, e); // Четвертая кнопка  создать из артикула
                                                     // break;
        }

        private void layoutControlGroup14_CustomButtonClick(object sender, BaseButtonEventArgs e)
        {
            int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);

            switch (buttonIndex)
            {
                case 0:
                    BindButton_Click_Internal(sender, e);// увязать
                    break;
                case 2:
                    UnboundWD(sender, e);
                    break;
            }
        }

        private void gridView_unboundArts_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            _gridHelper.popUpMenuCopy(sender, e);
        }

        private void ANNgridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            _gridHelper.popUpMenuCopy(sender, e);
        }

        private void gridView_binded_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            _gridHelper.popUpMenuCopy(sender, e);
        }

        private void gridViewRaskrTW_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            _gridHelper.popUpMenuCopy(sender, e);
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            _gridHelper.popUpMenuCopy(sender, e);
        }

        /// <summary>
        /// Печать технологической схемы разделения труда
        /// </summary>
        private async void PrintWorkDivisionScheme_Click(object sender, EventArgs e)
        {
            int rowNumber = ANNgridView.FocusedRowHandle;

            NormRaszTest report1 = new NormRaszTest();
            //report1.RequestParameters = false;
            var selectedAnn = ANNgridView.GetRow(rowNumber) as ArtNormN;

            report1.Parameters["_annId"].Value = selectedAnn.AnnID;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();
        }

        private async void customSimpleButton2_Click(object sender, EventArgs e)
        {
            await SetArchiveStatus_Internal(sender, e);
        }

        /// <summary>
        /// Устанавливает архивный статус для выбранных строк в ANNgridView
        /// </summary>
        private async Task SetArchiveStatus_Internal(object sender, EventArgs e)
        {
            try
            {
                if (ANNgridView == null)
                {
                    MessageBox.Show("Грид не инициализирован.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Получаем выбранные строки
                var selectedRowHandles = ANNgridView.GetSelectedRows();

                // Если нет выбранных строк, берем текущую строку
                if (selectedRowHandles == null || selectedRowHandles.Length == 0)
                {
                    if (ANNgridView.FocusedRowHandle < 0)
                    {
                        MessageBox.Show("Выберите записи для архивирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    selectedRowHandles = new int[] { ANNgridView.FocusedRowHandle };
                }

                var selectedItems = new List<ArtNormN>();

                // Собираем данные выбранных строк
                foreach (int rowHandle in selectedRowHandles)
                {
                    if (rowHandle >= 0)
                    {
                        var item = ANNgridView.GetRow(rowHandle) as ArtNormN;
                        if (item != null)
                        {
                            selectedItems.Add(item);
                        }
                    }
                }

                if (selectedItems.Count == 0)
                {
                    MessageBox.Show("Не найдено записей для архивирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Подтверждение операции
                string message = selectedItems.Count == 1
                    ? $"Установить архивный статус для записи:\n{selectedItems[0].Articul} - {selectedItems[0].Mod}?"
                    : $"Установить архивный статус для {selectedItems.Count} записей?";

                var result = MessageBox.Show(
                    message,
                    "Подтверждение архивирования",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (result != DialogResult.Yes)
                    return;

                // Обновляем статус для каждой выбранной записи
                int successCount = 0;
                var errors = new List<string>();

                ANNgridView.BeginUpdate();
                try
                {
                    foreach (var item in selectedItems)
                    {
                        try
                        {
                            // Проверяем, что запись можно архивировать
                            if (item.Status == (int)Status.Archive)
                            {
                                await _logger.LogEventAsync($"Запись AnnID: {item.AnnID} уже имеет архивный статус", "SetArchiveStatus");
                                continue;
                            }

                            // Обновляем статус в базе данных
                            await _dbService.UpdateFieldAsync(TableNames.Ann, "Status", (int)Status.Archive, TableNames.AnnId, item.AnnID);

                            // Обновляем объект в памяти
                            item.Status = (int)Status.Archive;
                            item.StatusText = StatusHelper.GetStatusText((int)Status.Archive);

                            // Обновляем строку в гриде
                            int rowHandle = ANNgridView.LocateByValue("AnnID", item.AnnID);
                            if (rowHandle >= 0)
                            {
                                ANNgridView.RefreshRow(rowHandle);
                            }

                            successCount++;
                            await _logger.LogEventAsync($"Статус записи AnnID: {item.AnnID} изменен на 'Архивное'", "SetArchiveStatus");
                        }
                        catch (Exception ex)
                        {
                            string errorMsg = $"AnnID: {item.AnnID} - {ex.Message}";
                            errors.Add(errorMsg);
                            await _logger.LogErrorAsync(ex, $"Ошибка при архивировании записи AnnID: {item.AnnID}");
                        }
                    }
                }
                finally
                {
                    ANNgridView.EndUpdate();
                }

                // Обновляем привязку данных
                _bindingSource.ResetBindings(false);

                // Показываем результат операции
                if (errors.Count == 0)
                {
                    string successMessage = successCount == 1
                        ? "Запись успешно архивирована."
                        : $"Успешно архивировано {successCount} записей.";

                    MessageBox.Show(successMessage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string errorMessage = $"Архивировано: {successCount} записей.\nОшибки:\n" + string.Join("\n", errors);
                    MessageBox.Show(errorMessage, "Результат архивирования", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при выполнении архивирования записей");
                MessageBox.Show($"Ошибка при архивировании: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void customSimpleButton3_Click(object sender, EventArgs e)
        {
            await UpdateSpArticulArch_Internal(sender, e);
        }

        /// <summary>
        /// Обновляет поле arch в таблице sp_articul для записей, соответствующих условиям
        /// </summary>
        private async Task UpdateSpArticulArch_Internal(object sender, EventArgs e)
        {
            try
            {
                // Проверяем, что gриды инициализированы
                if (ANNgridView == null || gridView5 == null)
                {
                    MessageBox.Show("Грид не инициализирован.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Получаем AnnID из выбранной строки в ANNgridView
                if (ANNgridView.FocusedRowHandle < 0)
                {
                    MessageBox.Show("Выберите запись в основном гриде (ANNgridView).", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedAnn = ANNgridView.GetRow(ANNgridView.FocusedRowHandle) as ArtNormN;
                if (selectedAnn == null)
                {
                    MessageBox.Show("Не удалось получить данные выбранной записи в основном гриде.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int annId = selectedAnn.AnnID;

                // Получаем артикул из выбранной строки в gridView5
                if (gridView5.FocusedRowHandle < 0)
                {
                    MessageBox.Show("Выберите запись в гриде НЗП (gridView5).", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedNzp = gridView5.GetRow(gridView5.FocusedRowHandle) as NZPByKoddRt;
                if (selectedNzp == null)
                {
                    MessageBox.Show("Не удалось получить данные выбранной записи в гриде НЗП.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string articul = selectedNzp.articul.TrimEnd(' ');
                int kod = selectedNzp.kodd;
                if (string.IsNullOrEmpty(articul))
                {
                    MessageBox.Show("Артикул в выбранной записи НЗП пустой.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Подтверждение операции
                string message = $"Обновить поле 'arch' в таблице sp_articul для:\n" +
                                $"AnnID: {annId}\n" +
                                $"Артикул: {articul}\n\n" +
                                $"Это затронет записи, где:\n" +
                                $"- sa.annid = ann.annID\n" +
                                $"- left(sa.kod, 7) = {kod}\n" +
                                $"- sa.articul = '{articul}'\n" +
                                $"- ann.annID = {annId}";

                var result = MessageBox.Show(
                    message,
                    "Подтверждение обновления arch",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (result != DialogResult.Yes)
                    return;

                // SQL запрос для обновления поля arch
                string sqlQuery = @"
                    UPDATE sa 
                    SET sa.arh = 1
                    WHERE left(sa.kod, 7) = @kod 
                    AND ann.articul = @art  
                    AND ann.annID = @annId";

                var parameters = new Dictionary<string, object>
                {
                    { "@annId", annId },
                    { "@art", articul },
                    { "@kod", kod }
                };

                // Выполняем обновление
                await _dbHelper.ExecuteQueryAsync(sqlQuery, parameters);

                // Логируем операцию
                await _logger.LogEventAsync($"Обновлено поле arch для записей AnnID: {annId}, Артикул: {articul}", "UpdateSpArticulArch");

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при обновлении поля arch в sp_articul");
                MessageBox.Show($"Ошибка при обновлении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void customSimpleButton4_Click(object sender, EventArgs e)
        {
            await SetUpdateDate_Internal(sender, e);
        }

        /// <summary>
        /// Устанавливает дату обновления (data_obn) сегодняшним числом для выбранных строк в ANNgridView
        /// </summary>
        private async Task SetUpdateDate_Internal(object sender, EventArgs e)
        {
            try
            {
                if (ANNgridView == null)
                {
                    MessageBox.Show("Грид не инициализирован.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Получаем выбранные строки
                var selectedRowHandles = ANNgridView.GetSelectedRows();

                // Если нет выбранных строк, берем текущую строку
                if (selectedRowHandles == null || selectedRowHandles.Length == 0)
                {
                    if (ANNgridView.FocusedRowHandle < 0)
                    {
                        MessageBox.Show("Выберите записи для обновления даты.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    selectedRowHandles = new int[] { ANNgridView.FocusedRowHandle };
                }

                var selectedItems = new List<ArtNormN>();

                // Собираем данные выбранных строк
                foreach (int rowHandle in selectedRowHandles)
                {
                    if (rowHandle >= 0)
                    {
                        var item = ANNgridView.GetRow(rowHandle) as ArtNormN;
                        if (item != null)
                        {
                            selectedItems.Add(item);
                        }
                    }
                }

                if (selectedItems.Count == 0)
                {
                    MessageBox.Show("Не найдено записей для обновления даты.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Подтверждение операции
                string message = selectedItems.Count == 1
                    ? $"Установить дату обновления для записи:\n{selectedItems[0].Articul} - {selectedItems[0].Mod}?"
                    : $"Установить дату обновления для {selectedItems.Count} записей?";

                var result = MessageBox.Show(
                    message,
                    "Подтверждение обновления даты",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (result != DialogResult.Yes)
                    return;

                // Текущая дата для установки
                DateTime currentDate = DateTime.Now;

                // Обновляем дату для каждой выбранной записи
                int successCount = 0;
                var errors = new List<string>();

                ANNgridView.BeginUpdate();
                try
                {
                    foreach (var item in selectedItems)
                    {
                        try
                        {
                            // Обновляем дату обновления в базе данных
                            await _dbService.UpdateFieldAsync(TableNames.Ann, "data_obn", currentDate, TableNames.AnnId, item.AnnID);

                            // Обновляем объект в памяти
                            item.dateUpdate = currentDate;

                            // Обновляем строку в гриде
                            int rowHandle = ANNgridView.LocateByValue("AnnID", item.AnnID);
                            if (rowHandle >= 0)
                            {
                                ANNgridView.SetRowCellValue(rowHandle, "dateUpdate", currentDate);
                                ANNgridView.RefreshRow(rowHandle);
                            }

                            successCount++;
                            await _logger.LogEventAsync($"Дата обновления для записи AnnID: {item.AnnID} установлена: {currentDate:dd.MM.yyyy}", "SetUpdateDate");
                        }
                        catch (Exception ex)
                        {
                            string errorMsg = $"AnnID: {item.AnnID} - {ex.Message}";
                            errors.Add(errorMsg);
                            await _logger.LogErrorAsync(ex, $"Ошибка при обновлении даты для записи AnnID: {item.AnnID}");
                        }
                    }
                }
                finally
                {
                    ANNgridView.EndUpdate();
                }

                // Обновляем привязку данных
                _bindingSource.ResetBindings(false);

                // Показываем результат операции
                if (errors.Count == 0)
                {
                    string successMessage = successCount == 1
                        ? $"Дата обновления успешно установлена: {currentDate:dd.MM.yyyy}"
                        : $"Дата обновления установлена для {successCount} записей: {currentDate:dd.MM.yyyy}";

                    MessageBox.Show(successMessage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string errorMessage = $"Обновлено: {successCount} записей.\nОшибки:\n" + string.Join("\n", errors);
                    MessageBox.Show(errorMessage, "Результат обновления даты", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при выполнении обновления даты");
                MessageBox.Show($"Ошибка при обновлении даты: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void customSimpleButton5_Click(object sender, EventArgs e)
        {
            await UnbindArticulesFromWorkDivision_Internal(sender, e);
        }

        /// <summary>
        /// Отвязывает артикулы от выбранного разделения труда, устанавливая annid = 0 в таблице sp_articul
        /// </summary>
        private async Task UnbindArticulesFromWorkDivision_Internal(object sender, EventArgs e)
        {
            try
            {
                // Проверяем, что грид инициализирован
                if (ANNgridView == null)
                {
                    MessageBox.Show("Грид не инициализирован.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Получаем AnnID из выбранной строки в ANNgridView
                if (ANNgridView.FocusedRowHandle < 0)
                {
                    MessageBox.Show("Выберите разделение труда для отвязывания артикулов.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedAnn = ANNgridView.GetRow(ANNgridView.FocusedRowHandle) as ArtNormN;
                if (selectedAnn == null)
                {
                    MessageBox.Show("Не удалось получить данные выбранного разделения труда.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int annId = selectedAnn.AnnID;
                // Получаем артикул из выбранной строки в gridView5
                if (gridView5.FocusedRowHandle < 0)
                {
                    MessageBox.Show("Выберите запись в гриде НЗП (gridView5).", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedNzp = gridView5.GetRow(gridView5.FocusedRowHandle) as NZPByKoddRt;
                if (selectedNzp == null)
                {
                    MessageBox.Show("Не удалось получить данные выбранной записи в гриде НЗП.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string articul = selectedNzp.articul.TrimEnd(' ');
                int kod = selectedNzp.kodd;
                if (string.IsNullOrEmpty(articul))
                {
                    MessageBox.Show("Артикул в выбранной записи НЗП пустой.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                string displayInfo = $"Группа: {selectedAnn.grup?.TrimEnd(' ')}, " +
                                   $"Модель: {selectedAnn.Mod?.TrimEnd(' ')}, " +
                                   $"Артикул: {selectedAnn.Articul?.TrimEnd(' ')}";

                // Подтверждение операции
                string message = $"Отвязать все артикулы от разделения труда:\n\n" +
                                $"AnnID: {annId}\n" +
                                $"{displayInfo}\n\n" +
                                $"Это установит annid = 0 для всех записей в таблице sp_articul,\n" +
                                $"связанных с данным разделением труда.\n\n" +
                                $"Продолжить?";

                var result = MessageBox.Show(
                    message,
                    "Подтверждение отвязывания артикулов",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (result != DialogResult.Yes)
                    return;

                // SQL запрос для отвязывания артикулов
                string sqlQuery =
                    //"UPDATE sp_articul SET annid = 0 WHERE annid = @annID";
                    "UPDATE sp_articul SET annid = 0 WHERE left(kod, 7) = @kod AND articul = @art AND annID = @annId";


                var parameters = new Dictionary<string, object>
                {
                    { "@annID", annId },
                    { "@kod", kod },
                    { "@art", articul}
                };

                // Выполняем обновление
                int affectedRows = await _dbHelper.ExecuteNonQueryWithRowCountAsync(sqlQuery, parameters);

                // Логируем операцию
                await _logger.LogEventAsync($"Отвязано {affectedRows} артикулов от РТ. AnnID: {annId}, {displayInfo}", "UnbindArticules");

                // Показываем результат
                if (affectedRows > 0)
                {
                    MessageBox.Show(
                        $"Успешно отвязано {affectedRows} артикулов от разделения труда.\n" +
                        $"Поле 'annid' установлено в 0 для соответствующих записей в sp_articul.",
                        "Информация",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        "Не найдено артикулов для отвязывания.\n" +
                        "Возможно, к данному разделению труда не привязаны артикулы.",
                        "Информация",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                // Обновляем связанные данные в интерфейсе, если необходимо
                if (affectedRows > 0)
                {
                    // Можно добавить обновление других компонентов интерфейса при необходимости
                    await LoadRelatedData(annId);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при отвязывании артикулов от разделения труда");
                MessageBox.Show($"Ошибка при отвязывании артикулов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void customSimpleButton6_Click(object sender, EventArgs e)
        {
            await MarkWorkDivisionForDeletion_Internal(sender, e);
        }

        /// <summary>
        /// Помечает выбранные разделения труда на удаление, устанавливая annDateDel и annCompDel
        /// </summary>
        private async Task MarkWorkDivisionForDeletion_Internal(object sender, EventArgs e)
        {
            try
            {
                // Проверяем, что грид инициализирован
                if (ANNgridView == null)
                {
                    MessageBox.Show("Грид не инициализирован.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Получаем выбранные строки
                var selectedRowHandles = ANNgridView.GetSelectedRows();

                // Если нет выбранных строк, берем текущую строку
                if (selectedRowHandles == null || selectedRowHandles.Length == 0)
                {
                    if (ANNgridView.FocusedRowHandle < 0)
                    {
                        MessageBox.Show("Выберите записи для пометки на удаление.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    selectedRowHandles = new int[] { ANNgridView.FocusedRowHandle };
                }

                var selectedItems = new List<ArtNormN>();
                var alreadyMarkedItems = new List<ArtNormN>();

                // Собираем данные выбранных строк
                foreach (int rowHandle in selectedRowHandles)
                {
                    if (rowHandle >= 0)
                    {
                        var item = ANNgridView.GetRow(rowHandle) as ArtNormN;
                        if (item != null)
                        {
                            if (item.dateDel.HasValue)
                            {
                                alreadyMarkedItems.Add(item);
                            }
                            else
                            {
                                selectedItems.Add(item);
                            }
                        }
                    }
                }

                // Сообщаем о записях, которые уже помечены на удаление
                if (alreadyMarkedItems.Count > 0)
                {
                    string alreadyMarkedMessage = alreadyMarkedItems.Count == 1
                        ? $"Запись уже помечена на удаление:\n{alreadyMarkedItems[0].Articul} - {alreadyMarkedItems[0].Mod}\nДата: {alreadyMarkedItems[0].dateDel.Value:dd.MM.yyyy HH:mm:ss}"
                        : $"{alreadyMarkedItems.Count} записей уже помечены на удаление.";

                    if (selectedItems.Count == 0)
                    {
                        MessageBox.Show(alreadyMarkedMessage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        alreadyMarkedMessage += "\n\nОни будут пропущены.";
                        MessageBox.Show(alreadyMarkedMessage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                if (selectedItems.Count == 0)
                {
                    MessageBox.Show("Нет записей для пометки на удаление.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Подтверждение операции
                DateTime currentDate = DateTime.Now;
                string computerName = Environment.MachineName;

                string message = selectedItems.Count == 1
                    ? $"Пометить разделение труда на удаление:\n\n" +
                      $"AnnID: {selectedItems[0].AnnID}\n" +
                      $"Группа: {selectedItems[0].grup?.TrimEnd(' ')}, " +
                      $"Модель: {selectedItems[0].Mod?.TrimEnd(' ')}, " +
                      $"Артикул: {selectedItems[0].Articul?.TrimEnd(' ')}\n\n" +
                      $"Будут установлены:\n" +
                      $"• annDateDel = {currentDate:dd.MM.yyyy HH:mm:ss}\n" +
                      $"• annCompDel = {computerName}\n\n" +
                      $"Продолжить?"
                    : $"Пометить {selectedItems.Count} разделений труда на удаление?\n\n" +
                      $"Будут установлены:\n" +
                      $"• annDateDel = {currentDate:dd.MM.yyyy HH:mm:ss}\n" +
                      $"• annCompDel = {computerName}\n\n" +
                      $"Продолжить?";

                var result = MessageBox.Show(
                    message,
                    "Подтверждение пометки на удаление",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (result != DialogResult.Yes)
                    return;

                // Обрабатываем каждую запись
                int successCount = 0;
                var errors = new List<string>();

                ANNgridView.BeginUpdate();
                try
                {
                    foreach (var item in selectedItems)
                    {
                        try
                        {
                            // SQL запрос для пометки на удаление
                            string sqlQuery = "UPDATE art_norm_n SET annDateDel = @currentDate, annCompDel = @computerName WHERE annID = @annID";

                            var parameters = new Dictionary<string, object>
                            {
                                { "@annID", item.AnnID },
                                { "@currentDate", currentDate },
                                { "@computerName", computerName }
                            };

                            // Выполняем обновление
                            int affectedRows = await _dbHelper.ExecuteNonQueryWithRowCountAsync(sqlQuery, parameters);

                            if (affectedRows > 0)
                            {
                                // Обновляем объект в памяти
                                item.dateDel = currentDate;
                                item.compDel = computerName;

                                // Обновляем строку в гриде
                                int rowHandle = ANNgridView.LocateByValue("AnnID", item.AnnID);
                                if (rowHandle >= 0)
                                {
                                    ANNgridView.RefreshRow(rowHandle);
                                }

                                successCount++;
                                await _logger.LogEventAsync($"РТ помечено на удаление. AnnID: {item.AnnID}, Группа: {item.grup?.TrimEnd(' ')}, Модель: {item.Mod?.TrimEnd(' ')}, Артикул: {item.Articul?.TrimEnd(' ')}, Дата: {currentDate:dd.MM.yyyy HH:mm:ss}, Компьютер: {computerName}", "MarkForDeletion");
                            }
                            else
                            {
                                string errorMsg = $"AnnID: {item.AnnID} - не удалось обновить в БД";
                                errors.Add(errorMsg);
                                await _logger.LogErrorAsync(new Exception("affectedRows = 0"), $"Не удалось пометить РТ на удаление. AnnID: {item.AnnID}");
                            }
                        }
                        catch (Exception ex)
                        {
                            string errorMsg = $"AnnID: {item.AnnID} - {ex.Message}";
                            errors.Add(errorMsg);
                            await _logger.LogErrorAsync(ex, $"Ошибка при пометке РТ на удаление. AnnID: {item.AnnID}");
                        }
                    }
                }
                finally
                {
                    ANNgridView.EndUpdate();
                }

                // Обновляем привязку данных
                _bindingSource.ResetBindings(false);

                // Показываем результат операции
                if (errors.Count == 0)
                {
                    string successMessage = successCount == 1
                        ? $"Разделение труда успешно помечено на удаление.\n\nДата удаления: {currentDate:dd.MM.yyyy HH:mm:ss}\nКомпьютер: {computerName}"
                        : $"Успешно помечено на удаление {successCount} разделений труда.\n\nДата удаления: {currentDate:dd.MM.yyyy HH:mm:ss}\nКомпьютер: {computerName}";

                    MessageBox.Show(successMessage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string errorMessage = $"Успешно обработано: {successCount}\nОшибок: {errors.Count}\n\nОшибки:\n" + string.Join("\n", errors.Take(5));
                    if (errors.Count > 5)
                        errorMessage += $"\n... и еще {errors.Count - 5} ошибок";

                    MessageBox.Show(errorMessage, "Результат операции", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при пометке разделений труда на удаление: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await _logger.LogErrorAsync(ex, "Ошибка в MarkWorkDivisionForDeletion_Internal");
            }
        }

        private void printButtonPlus_Click(object sender, EventArgs e)
        {
            int rowNumber = ANNgridView.FocusedRowHandle;

            NormRaszForEconomist report1 = new NormRaszForEconomist();
            //report1.RequestParameters = false;
            var selectedAnn = ANNgridView.GetRow(rowNumber) as ArtNormN;

            report1.Parameters["_annId"].Value = selectedAnn.AnnID;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreviewDialog();

        }

        // Управление немодальной формой TeamWork_AdvanceTW
        private static readonly List<TeamWork_AdvanceTW> _openAdvanceForms = new List<TeamWork_AdvanceTW>();
        private static readonly object _lockObject = new object();

        private static bool HasOpenAdvanceForms()
        {
            lock (_lockObject)
            {
                // Очищаем закрытые формы из списка
                _openAdvanceForms.RemoveAll(form => form == null || form.IsDisposed);
                return _openAdvanceForms.Count > 0;
            }
        }

        /// <summary>
        /// Добавляет экземпляр TeamWork_AdvanceTW в список открытых форм
        /// </summary>
        /// <param name="form">Форма для добавления</param>
        private static void AddOpenAdvanceForm(TeamWork_AdvanceTW form)
        {
            lock (_lockObject)
            {
                if (form != null && !form.IsDisposed && !_openAdvanceForms.Contains(form))
                {
                    _openAdvanceForms.Add(form);
                }
            }
        }

        /// <summary>
        /// Удаляет экземпляр TeamWork_AdvanceTW из списка открытых форм
        /// </summary>
        /// <param name="form">Форма для удаления</param>
        private static void RemoveOpenAdvanceForm(TeamWork_AdvanceTW form)
        {
            lock (_lockObject)
            {
                _openAdvanceForms.Remove(form);
            }
        }

        /// <summary>
        /// Открывает TeamWork_AdvanceTW не в модальном режиме с проверкой на уже открытые экземпляры
        /// </summary>
        /// <param name="bufferWorkDivision">ID из буфера</param>
        /// <param name="mode">Режим работы</param>
        /// <param name="newId">ID новой записи</param>
        /// <param name="oldId">ID исходной записи</param>
        /// <param name="sourceAnnIdToCopyDetailsFrom">ID источника для копирования деталей</param>
        /// <param name="initialArtData">Начальные данные артикула</param>
        /// <param name="duplicateAnnData">Данные для дублирования</param>
        /// <returns>Созданная форма или существующая форма если уже есть открытые экземпляры</returns>
        public TeamWork_AdvanceTW OpenAdvanceFormNonModal(int bufferWorkDivision, int mode, int? newId = null, int? oldId = null, int? sourceAnnIdToCopyDetailsFrom = null, MyDataART initialArtData = null, ArtNormN duplicateAnnData = null)
        {
            // Проверяем, есть ли уже открытые экземпляры
            if (HasOpenAdvanceForms())
            {
                // Получаем первый открытый экземпляр
                TeamWork_AdvanceTW existingForm = null;
                lock (_lockObject)
                {
                    existingForm = _openAdvanceForms.FirstOrDefault(form => form != null && !form.IsDisposed);
                }

                if (existingForm != null)
                {
                    string articul = existingForm.CurrentArticul;
                    string msg = string.IsNullOrWhiteSpace(articul)
                        ? "Открыто разделение труда"
                        : $"Открыто разделение труда для артикула \"{articul}\"";

                    // Показать сплеш на 3 секунды
                    Task.Run(async () =>
                    {
                        SplashScreenHelper.ShowSplash(msg, textOnly: true);
                        await Task.Delay(3000);
                        SplashScreenHelper.CloseSplash();
                    });

                    existingForm.WindowState = FormWindowState.Normal;
                    existingForm.BringToFront();
                    existingForm.Activate();
                }

                return null;
            }

            var advanceForm = new TeamWork_AdvanceTW(bufferWorkDivision, mode, newId, oldId, sourceAnnIdToCopyDetailsFrom, initialArtData, duplicateAnnData);

            // Добавляем в список открытых форм
            AddOpenAdvanceForm(advanceForm);

            // Подписываемся на событие закрытия формы
            advanceForm.FormClosed += (sender, e) =>
            {
                RemoveOpenAdvanceForm(advanceForm);
            };

            // Открываем форму не в модальном режиме
            advanceForm.Show();

            return advanceForm;
        }
    }
    public static class DemoHelper
    {

        public static Image GetDeleteImage()
        {
            return GetImage(Brushes.Red);
        }

        public static Image GetEditImage()
        {
            return GetImage(Brushes.Green);
        }

        public static Image GetImage(Brush b)
        {
            Image img = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(img))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillEllipse(b, new Rectangle(0, 0, img.Width - 1, img.Height - 1));
            }
            return img;
        }
    }
}
   