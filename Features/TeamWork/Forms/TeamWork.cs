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
        private BindingList<ArtNormN> _archList;
        private BindingSource _archBindingSource;
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

            _archList = new BindingList<ArtNormN>();
            _archBindingSource = new BindingSource { DataSource = _archList };
            if (gridControlArch != null) gridControlArch.DataSource = _archBindingSource;

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

            //// Инициализация для NormRask на вкладке Articles
            //_normRaskListArticles = new BindingList<NormRask>();
            //_normRaskBindingSourceArticles = new BindingSource { DataSource = _normRaskListArticles };
            //if (customGridControl2 != null) 
            //{
            //    customGridControl2.DataSource = _normRaskBindingSourceArticles;
            //    _logger?.LogEventAsync($"Constructor: customGridControl2.DataSource set to _normRaskBindingSourceArticles", "TeamWork.Constructor");

            //    // Verify the grid view configuration
            //    if (customGridControl2.MainView is GridView gridView)
            //    {
            //        _logger?.LogEventAsync($"Constructor: customGridControl2.MainView is GridView with {gridView.Columns.Count} columns", "TeamWork.Constructor");
            //        foreach (var col in gridView.Columns)
            //        {
            //            _logger?.LogEventAsync($"Constructor: Column '{col.Name}' - FieldName: '{col.FieldName}', Visible: {col.Visible}, Width: {col.Width}", "TeamWork.Constructor");
            //        }
            //    }
            //}
            //else
            //{
            //    _logger?.LogWarningAsync("Constructor: customGridControl2 is null, cannot set DataSource", "TeamWork.Constructor");
            //}

            //// Инициализация для NormKont на вкладке Articles
            //_normKontListArticles = new BindingList<NormKont>();
            //_normKontBindingSourceArticles = new BindingSource { DataSource = _normKontListArticles };
            //if (customGridControl1 != null) customGridControl1.DataSource = _normKontBindingSourceArticles;

            InitializeGridSettings();
            SetupDateUpdateColumn();
            VerifyGridConfigurations();
        }

        /// <summary>
        /// Принудительно обновляет данные в normRaskArt гриде
        /// </summary>
        public async Task ForceRefreshNormRaskArt()
        {
            try
            {
                if (gridView_wdToBind?.RowCount > 0 && gridView_wdToBind.FocusedRowHandle >= 0)
                {
                    int annId = CommonFunctions.GetRowCellValueOrDefault<int>(gridView_wdToBind, gridView_wdToBind.FocusedRowHandle, "AnnID", 0);
                    if (annId > 0)
                    {
                        await _logger.LogEventAsync($"ForceRefreshNormRaskArt: Refreshing data for annId={annId}", "ForceRefreshNormRaskArt");

                        // Call the method from TeamWork.Articles.cs
                        var articlesForm = this as dynamic;
                        if (articlesForm != null)
                        {
                            await articlesForm.RefreshNormRaskForArticlesTab(annId, _loadCts.Token);
                        }
                    }
                }
                else
                {
                    await _logger.LogWarningAsync("ForceRefreshNormRaskArt: No focused row in gridView_wdToBind", "ForceRefreshNormRaskArt");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Error in ForceRefreshNormRaskArt");
            }
        }

        /// <summary>
        /// Проверяет текущее состояние normRaskArt грида
        /// </summary>
        public async Task CheckNormRaskArtState()
        {
            try
            {
                await _logger.LogEventAsync($"CheckNormRaskArtState: Starting grid state check", "CheckNormRaskArtState");

                if (customGridControl2 == null)
                {
                    await _logger.LogWarningAsync("CheckNormRaskArtState: customGridControl2 is null", "CheckNormRaskArtState");
                    return;
                }

                await _logger.LogEventAsync($"CheckNormRaskArtState: customGridControl2.Visible={customGridControl2.Visible}, Enabled={customGridControl2.Enabled}", "CheckNormRaskArtState");

                if (customGridControl2.MainView is GridView gridView)
                {
                    await _logger.LogEventAsync($"CheckNormRaskArtState: GridView.RowCount={gridView.RowCount}, DataRowCount={gridView.DataRowCount}", "CheckNormRaskArtState");
                    await _logger.LogEventAsync($"CheckNormRaskArtState: GridView.DataSource={gridView.GridControl?.DataSource}", "CheckNormRaskArtState");

                    if (gridView.DataSource is BindingSource bindingSource)
                    {
                        await _logger.LogEventAsync($"CheckNormRaskArtState: BindingSource.DataSource={bindingSource.DataSource}, Count={bindingSource.Count}", "CheckNormRaskArtState");

                        if (bindingSource.DataSource is BindingList<NormRask> bindingList)
                        {
                            await _logger.LogEventAsync($"CheckNormRaskArtState: BindingList.Count={bindingList.Count}", "CheckNormRaskArtState");
                            if (bindingList.Count > 0)
                            {
                                var firstItem = bindingList[0];
                                await _logger.LogEventAsync($"CheckNormRaskArtState: First item - kod_o='{firstItem.Kod_o}', text='{firstItem.TextRask}', razryd={firstItem.razryd}, sek={firstItem.Sek}, spec='{firstItem.Spec}', obor='{firstItem.Obor}'", "CheckNormRaskArtState");
                            }
                        }
                    }
                }
                else
                {
                    await _logger.LogWarningAsync("CheckNormRaskArtState: customGridControl2.MainView is not GridView", "CheckNormRaskArtState");
                }

                await _logger.LogEventAsync($"CheckNormRaskArtState: Grid state check completed", "CheckNormRaskArtState");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Error in CheckNormRaskArtState");
            }
        }

        /// <summary>
        /// Принудительно обновляет и проверяет состояние normRaskArt грида
        /// </summary>
        public async Task ForceRefreshAndCheckNormRaskArt()
        {
            try
            {
                await _logger.LogEventAsync($"ForceRefreshAndCheckNormRaskArt: Starting forced refresh and check", "ForceRefreshAndCheckNormRaskArt");

                // First check current state
                await CheckNormRaskArtState();

                // Then force refresh
                await ForceRefreshNormRaskArt();

                // Wait a bit for the refresh to complete
                await Task.Delay(100);

                // Check state again after refresh
                await CheckNormRaskArtState();

                await _logger.LogEventAsync($"ForceRefreshAndCheckNormRaskArt: Completed forced refresh and check", "ForceRefreshAndCheckNormRaskArt");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Error in ForceRefreshAndCheckNormRaskArt");
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
                ANNgridView.ColumnFilterChanged -= ANNgridView_ActiveFilterChanged;
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
                    ANNgridView.ColumnFilterChanged += ANNgridView_ActiveFilterChanged;
                    // Фокус и связанные данные при фильтрации/поиске
                    ANNgridView.ColumnFilterChanged += (s, e2) => FocusFirstResultAndLoadRelated();

                    // Используем уже существующий обработчик для поддержки комплектных артикулов

                    // Подписываемся на событие изменения текста поиска
                    if (ANNgridView.IsFocusedView && ANNgridView.RowCount > 0 && ANNgridView.FocusedRowHandle >= 0) // Проверка перед вызовом
                    {
                        ANNgridView_FocusedRowChanged_Internal(ANNgridView, new FocusedRowChangedEventArgs(-1, ANNgridView.FocusedRowHandle));
                    }

                    // Включаем подсветку строк
                    ApplyAnnGridRowStyling();
                }

                // Устанавливаем начальный режим (обычный)
                SetNormalMode();
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

        /// <summary>
        /// Обрабатывает смену вложенной вкладки в "Текущие работы"
        /// </summary>
        private async void XtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == null) return;

            try
            {
                switch (e.Page.Name)
                {
                    case "xtraTabPageWorkDivisions":
                        // Когда переходим на вкладку "Требуют увязки", обновляем данные для normRaskArt
                        if (gridView_wdToBind?.RowCount > 0 && gridView_wdToBind.FocusedRowHandle >= 0)
                        {
                            int annId = CommonFunctions.GetRowCellValueOrDefault<int>(gridView_wdToBind, gridView_wdToBind.FocusedRowHandle, "AnnID", 0);
                            if (annId > 0)
                            {
                                await _logger.LogEventAsync($"XtraTabControl2_SelectedPageChanged: Refreshing NormRask data for annId={annId} on xtraTabPageWorkDivisions", "XtraTabControl2_SelectedPageChanged");
                                await RefreshNormRaskForArticlesTab(annId, _loadCts.Token);

                                // Check the grid state after refresh
                                await CheckNormRaskArtState();
                            }
                        }
                        else
                        {
                            await _logger.LogWarningAsync("XtraTabControl2_SelectedPageChanged: No focused row in gridView_wdToBind", "XtraTabControl2_SelectedPageChanged");
                        }
                        break;

                    case "xtraTabPage3":
                        // Можно добавить логику для другой вложенной вкладки если необходимо
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Error in XtraTabControl2_SelectedPageChanged");
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
        /// Обрабатывает изменение фильтра поиска в ANNgridView с поддержкой комплектных артикулов
        /// Автоматически переходит на первую строку результатов поиска
        /// В режиме комплекта разбирает артикул типа "1Т1773С1990" на компоненты "1Т1773" и "С1990"
        /// </summary>
        private void ANNgridView_ActiveFilterChanged(object sender, EventArgs e)
        {
            try
            {
                var gridView = sender as GridView;
                if (gridView == null) return;

                // В режиме комплекта проверяем FindFilterText для комплектного поиска
                if (toggleSwitchKit.IsOn && !string.IsNullOrEmpty(gridView.FindFilterText))
                {
                    string searchText = gridView.FindFilterText.Trim();
                    var kitComponents = ParseKitArticle(searchText);

                    if (kitComponents.HasValidComponents)
                    {
                        // Применяем комплектный поиск
                        string filterExpression = $"[Articul] Like '%{kitComponents.Component1}%' OR [Articul] Like '%{kitComponents.Component2}%'";

                        // Временно отключаем FindFilter и применяем кастомный фильтр
                        gridView.GridControl.BeginInvoke(new Action(() =>
                        {
                            try
                            {
                                gridView.FindFilterText = "";
                                gridView.ActiveFilterString = filterExpression;

                                _logger?.LogEventAsync($"Комплектный поиск: '{searchText}' разобран на '{kitComponents.Component1}' и '{kitComponents.Component2}'", "ANNgridView_ActiveFilterChanged");
                            }
                            catch (Exception ex)
                            {
                                _logger?.LogErrorAsync(ex, "Ошибка при применении комплектного поиска");
                            }
                        }));
                        return; // Выходим, чтобы не выполнять стандартную логику
                    }
                }

                // Стандартная логика для обычного поиска
                if (gridView.ActiveFilterCriteria != null)
                {
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
                }
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, "Ошибка в обработчике изменения фильтра поиска");
            }
        }


        /// <summary>
        /// Разбирает комплектный артикул на компоненты
        /// Поддерживает форматы: 1Т1773С1990, 1dТ1773С1990, 1Ф7738Ш1395
        /// </summary>
        private (bool HasValidComponents, string Component1, string Component2) ParseKitArticle(string articleText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(articleText))
                    return (false, "", "");

                string cleanText = articleText.Trim().ToUpperInvariant();

                // Шаблоны для разбора комплектных артикулов
                var patterns = new[]
                {
                    // Паттерн 1: 1Т1773С1990 = 1Т1773 + С1990 (буква в середине)
                    @"^(\d+[А-ЯЁ]+\d+)([А-ЯЁ]+\d+)$",
                    // Паттерн 2: 1dТ1773С1990 = 1dТ1773 + С1990 (латинская буква + кириллица)
                    @"^(\d+[A-Za-z]*[А-ЯЁ]+\d+)([А-ЯЁ]+\d+)$",
                    // Паттерн 3: более гибкий паттерн для различных комбинаций
                    @"^(\d+[A-Za-zА-ЯЁ]+\d+)([А-ЯЁ]+\d+)$"
                };

                foreach (var pattern in patterns)
                {
                    var match = System.Text.RegularExpressions.Regex.Match(cleanText, pattern);
                    if (match.Success && match.Groups.Count == 3)
                    {
                        string component1 = match.Groups[1].Value;
                        string component2 = match.Groups[2].Value;

                        // Проверяем, что обе части имеют разумную длину
                        if (component1.Length >= 3 && component2.Length >= 2)
                        {
                            return (true, component1, component2);
                        }
                    }
                }

                // Если автоматическое разбиение не сработало, пробуем найти разделитель по последней заглавной букве
                // Например: 1Т1773С1990 -> ищем последнюю заглавную букву как начало второй части
                for (int i = cleanText.Length - 1; i > 2; i--)
                {
                    char c = cleanText[i];
                    if (char.IsLetter(c) && char.IsUpper(c))
                    {
                        // Проверяем, что после буквы есть цифры
                        bool hasDigitsAfter = false;
                        for (int j = i + 1; j < cleanText.Length; j++)
                        {
                            if (char.IsDigit(cleanText[j]))
                            {
                                hasDigitsAfter = true;
                                break;
                            }
                        }

                        if (hasDigitsAfter)
                        {
                            string component1 = cleanText.Substring(0, i);
                            string component2 = cleanText.Substring(i);

                            if (component1.Length >= 3 && component2.Length >= 2)
                            {
                                return (true, component1, component2);
                            }
                        }
                    }
                }

                return (false, "", "");
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, $"Ошибка при разборе комплектного артикула: {articleText}");
                return (false, "", "");
            }
        }

        private async void loadAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            loadAllCheckBox_CheckedChanged_Internal(sender, e, loadAllCheckBox.Checked);
        }

        private async void TeamWork_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Отписываемся от событий
            if (ANNgridView != null)
            {
                ANNgridView.FocusedRowChanged -= ANNgridView_FocusedRowChanged;
                ANNgridView.ColumnFilterChanged -= ANNgridView_ActiveFilterChanged;
                // ANNgridView.DataSourceChanged -= ANNgridView_DataSourceChanged;
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
                await _logger.LogWarningAsync("Попытка дублирования без выбора строки", "DuplicateWorkDivision_Click_Internal");
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
                await _logger.LogWarningAsync("Не удалось получить данные выбранного РТ", "DuplicateWorkDivision_Click_Internal");
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
                await _logger.LogWarningAsync("Ошибка при создании новой записи РТ в базе данных", "DuplicateWorkDivision_Click_Internal");
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

        private async void layoutControlGroup14_CustomButtonClick(object sender, BaseButtonEventArgs e)
        {
            int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);

            switch (buttonIndex)
            {
                case 0:
                    await BindButton_Click_Internal(sender, e);// увязать
                    break;
                case 2:
                    await UnboundWD(sender, e);
                    break;
                case 4:
                    await SetUpdateDate_Internal(sender, e);
                    break;
                //case 6:
                //    loadAllCheckBox_CheckedChanged_Internal(sender, e, );
                //    break;
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопок в группе архива (layoutControlGroup19)
        /// </summary>
        private async void layoutControlGroup19_CustomButtonClick(object sender, BaseButtonEventArgs e)
        {
            int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);

            switch (buttonIndex)
            {
                case 0:
                    await RestoreFromArchive_Internal(sender, e); // Вернуть в актуальные
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

        private async void customSimpleButtonArch_Click(object sender, EventArgs e)
        {
            await SetArchiveStatus_Internal(sender, e);
        }
        private async void customSimpleButtonArchArt_Click(object sender, EventArgs e)
        {
            await UpdateSpArticulArch_Internal(sender, e);
        }
        private async void customSimpleButtonUpd_Click(object sender, EventArgs e)
        {
            await SetUpdateDate_Internal(sender, e);
        }

        private async void customSimpleButton5_Click(object sender, EventArgs e)
        {
            await UnbindArticulesFromWorkDivision_Internal(sender, e);
        }

        /// <summary>
        /// Отвязывает артикулы от выбранного разделения труда, устанавливая annid = 0 в таблице sp_articul
        /// </summary>
        /// <summary>
        /// Универсальная процедура отвязки артикулов от РТ для разных гридов
        /// </summary>
        /// <param name="nzpGridView">GridView с данными НЗП</param>
        /// <param name="nzpDataSource">Источник данных НЗП (BindingList)</param>
        /// <param name="workDivisionGridView">GridView с разделениями труда</param>
        /// <param name="useCheckedRows">Использовать отмеченные строки (true) или текущую выбранную (false)</param>
        private async Task UnbindArticulesFromWorkDivision_Internal(
            GridView nzpGridView,
            IEnumerable<NZPByKoddRt> nzpDataSource,
            GridView workDivisionGridView,
            bool useCheckedRows = false)
        {
            try
            {
                await _logger.LogEventAsync($"UnbindArticulesFromWorkDivision_Internal: Начало отвязки. UseCheckedRows: {useCheckedRows}", "UnbindArticles");

                // Проверяем входные параметры
                if (nzpGridView == null || workDivisionGridView == null)
                {
                    MessageBox.Show("Один из гридов не инициализирован.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Получаем данные НЗП для отвязки
                List<NZPByKoddRt> itemsToUnbind;

                if (useCheckedRows)
                {
                    // Используем отмеченные строки
                    itemsToUnbind = nzpDataSource?.Where(row => row.IsChecked).ToList();
                    if (itemsToUnbind == null || !itemsToUnbind.Any())
                    {
                        MessageBox.Show("Выберите артикулы для отвязки!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    // Используем текущую выбранную строку
                    if (nzpGridView.FocusedRowHandle < 0)
                    {
                        MessageBox.Show("Выберите запись в гриде НЗП.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var selectedNzp = nzpGridView.GetRow(nzpGridView.FocusedRowHandle) as NZPByKoddRt;
                    if (selectedNzp == null)
                    {
                        MessageBox.Show("Не удалось получить данные выбранной записи в гриде НЗП.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    itemsToUnbind = new List<NZPByKoddRt> { selectedNzp };
                }

                // Получаем данные РТ
                ArtNormN selectedAnn = null;
                if (workDivisionGridView.FocusedRowHandle >= 0)
                {
                    selectedAnn = workDivisionGridView.GetRow(workDivisionGridView.FocusedRowHandle) as ArtNormN;
                    if (selectedAnn == null && workDivisionGridView.Name == "gridView_wdToBind")
                    {
                        // Для gridView_wdToBind получаем MyDataANN и конвертируем
                        var myDataAnn = workDivisionGridView.GetRow(workDivisionGridView.FocusedRowHandle) as MyDataANN;
                        if (myDataAnn != null)
                        {
                            selectedAnn = await _artNormService.GetArtNormDataById(myDataAnn.AnnID);
                        }
                    }
                }

                if (selectedAnn == null)
                {
                    MessageBox.Show("Не удалось получить данные выбранного разделения труда.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Подтверждение операции
                string displayInfo = $"Группа: {selectedAnn.grup?.TrimEnd(' ')}, " +
                                   $"Модель: {selectedAnn.Mod?.TrimEnd(' ')}, " +
                                   $"Артикул: {selectedAnn.Articul?.TrimEnd(' ')}";

                string message = $"Отвязать {itemsToUnbind.Count} артикул(ов) от разделения труда:\n\n" +
                                $"AnnID: {selectedAnn.AnnID}\n" +
                                $"{displayInfo}\n\n" +
                                $"Продолжить?";

                var result = MessageBox.Show(message, "Подтверждение отвязывания артикулов",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (result != DialogResult.Yes)
                    return;

                int totalAffectedRows = 0;

                // Обрабатываем каждый элемент для отвязки
                foreach (var nzpItem in itemsToUnbind)
                {
                    int kod = nzpItem.kodd; // код артикула из строки НЗП
                    int annIdNzpRow = nzpItem.annId; // AnnID РТ из строки НЗП
                    string articul = nzpItem.articul?.TrimEnd(' ') ?? "";

                    await _logger.LogEventAsync($"Отвязка артикула KOD: {kod}, articul: {articul}, AnnID: {annIdNzpRow}", "UnbindArticles");
                    var parameters = new Dictionary<string, object>
                {
                    { "@annID", annIdNzpRow },
                    { "@kod", kod },
                    { "@art", articul}
                };
                    // Вызов метода для отвязки артикула в sp_articul
                    await _dbService.UpdateFieldAsync(TableNames.Art, "annId", "", "left(kod, 7) = @kod AND articul = @art AND annID = @annId", parameters);//_artNormService.ResetAnnIdinArticul(kod);
                    await _dbService.UpdateFieldAsync(TableNames.Ann, "size_label", null, TableNames.AnnId, annIdNzpRow);

                    // Обновление статуса РТ
                    await _dbService.UpdateFieldAsync(TableNames.Ann, "status", (int)Status.Actual, "parentId", annIdNzpRow);

                    totalAffectedRows++;
                }

                // Логируем операцию
                await _logger.LogEventAsync($"Отвязано {totalAffectedRows} артикулов от РТ. AnnID: {selectedAnn.AnnID}, {displayInfo}", "UnbindArticles");

                // Показываем результат
                if (totalAffectedRows > 0)
                {
                    MessageBox.Show($"Успешно отвязано {totalAffectedRows} артикул(ов) от разделения труда.",
                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Обновляем связанные данные в интерфейсе
                    await LoadRelatedData(selectedAnn.AnnID);

                    // Обновляем NZP данные
                    await RefreshNzpData(workDivisionGridView);
                }
                else
                {
                    MessageBox.Show("Не найдено артикулов для отвязывания.", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при отвязывании артикулов от разделения труда");
                MessageBox.Show($"Ошибка при отвязывании артикулов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обновляет данные НЗП после отвязки
        /// </summary>
        private async Task RefreshNzpData(GridView workDivisionGridView)
        {
            try
            {
                if (workDivisionGridView?.FocusedRowHandle >= 0)
                {
                    int annId = 0;
                    if (workDivisionGridView.Name == "ANNgridView")
                    {
                        var ann = workDivisionGridView.GetRow(workDivisionGridView.FocusedRowHandle) as ArtNormN;
                        annId = ann?.AnnID ?? 0;
                    }
                    else if (workDivisionGridView.Name == "gridView_wdToBind")
                    {
                        var myDataAnn = workDivisionGridView.GetRow(workDivisionGridView.FocusedRowHandle) as MyDataANN;
                        annId = myDataAnn?.AnnID ?? 0;
                    }

                    if (annId > 0)
                    {
                        var nzpData = await _artNormService.GetNzpWithPztCounts(annId);
                        if (workDivisionGridView.Name == "ANNgridView")
                        {
                            _nzpListWd?.BulkLoad(nzpData ?? new List<NZPByKoddRt>());
                            _nzpByKoddRtSourceWd?.ResetBindings(false);
                        }
                        else if (workDivisionGridView.Name == "gridView_wdToBind")
                        {
                            _nzpListArt?.BulkLoad(nzpData ?? new List<NZPByKoddRt>());
                            _nzpByKoddRtSourceArt?.ResetBindings(false);
                        }
                        gridControlNZP?.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при обновлении данных НЗП");
            }
        }

        // Оригинальный метод для обратной совместимости (первая вкладка)
        private async Task UnbindArticulesFromWorkDivision_Internal(object sender, EventArgs e)
        {
            await _logger.LogEventAsync("UnbindArticulesFromWorkDivision_Internal: Отвязка на первой вкладке");

            // Используем универсальную процедуру для первой вкладки
            await UnbindArticulesFromWorkDivision_Internal(
                gridView5,           // GridView НЗП (customGridControl4)
                _nzpListWd,          // Источник данных НЗП для первой вкладки
                ANNgridView,         // GridView с РТ (ArtNormN)
                useCheckedRows: false // Используем текущую выбранную строку
            );
        }

        private async void customSimpleButtonDel_Click(object sender, EventArgs e)
        {
            await MarkWorkDivisionForDeletion_Internal(sender, e);
        }

        private void ModeRadio_CheckedChanged(object sender, EventArgs e)
        {
            ModeRadio_CheckedChanged_internal(sender, e);
        }

        private void layoutControlGroup14_CustomButtonChecked(object sender, BaseButtonEventArgs e)
        {
            loadAllCheckBox_CheckedChanged_Internal(sender, e, ((DevExpress.XtraEditors.ButtonPanel.BaseButton)e.Button).Checked);
        }

        private void layoutControlGroup14_CustomButtonUnchecked(object sender, BaseButtonEventArgs e)
        {
            loadAllCheckBox_CheckedChanged_Internal(sender, e, ((DevExpress.XtraEditors.ButtonPanel.BaseButton)e.Button).Checked);
        }
    }
}
   