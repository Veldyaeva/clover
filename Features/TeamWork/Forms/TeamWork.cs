using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Features.TeamWork.Services;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Models;
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

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork : CustomForm
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly DbService _dbService;
        private readonly ArtNormRepository _artNormService; // репозиторий данных
        private readonly JabberSender _jabberSender;
        private int selectedRowHandle = -1;
        private readonly ILogger _logger = new FileLogger();
        private readonly TWGridHelper _gridHelper = new TWGridHelper();
        private readonly SplitContainerHelper _splitContainerHelper = new SplitContainerHelper();
        private readonly FormSettingsHelper _formSettingsHelper = new FormSettingsHelper();
        private readonly SecondsUpdateManager _secondsUpdateManager;
        private TeamWorkOrchestrator _teamWorkService;
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

        // Глобальная переменная для состояния кнопки "показать все"
        private bool showAllWD = false;

        //// Глобальная переменная для управления видимостью кнопки customSimpleButtonUnbind
        //private bool customSimpleButtonUnbindVisible = true;
        // Объект для управления доступностью кнопки "bind:unlink"
        private ButtonUnbindWd ButtonUnbindWd;
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


            ANNgridView.OptionsView.ShowPreview = false;
            ANNgridView.PreviewLineCount = 0;
            DapperMappings.Configure();
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);
            _artNormService = new ArtNormRepository(_dbHelper);
            _secondsUpdateManager = new SecondsUpdateManager(_artNormService, _logger);
            _teamWorkService = new TeamWorkOrchestrator(_artNormService, _dbService, _logger);
            _uiHelper = new UIHelper(_logger);
            _jabberSender = new JabberSender(_dbHelper);

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
            if (GridControlBindedArts != null) GridControlBindedArts.DataSource = _nzpByKoddRtSourceWd;

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
            _normRaskListArticles = new BindingList<NormRask>();
            _normRaskBindingSourceArticles = new BindingSource { DataSource = _normRaskListArticles };
            if (customGridControl2 != null)
            //{
                customGridControl2.DataSource = _normRaskBindingSourceArticles;
            //}
                //_logger?.LogEventAsync($"Constructor: customGridControl2.DataSource set to _normRaskBindingSourceArticles", "TeamWork.Constructor");

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

            // Инициализация для NormKont на вкладке Articles
            _normKontListArticles = new BindingList<NormKont>();
            _normKontBindingSourceArticles = new BindingSource { DataSource = _normKontListArticles };
            if (customGridControl1 != null) customGridControl1.DataSource = _normKontBindingSourceArticles;

            InitializeGridSettings();
            SetupDateUpdateColumn();
            VerifyGridConfigurations();

            // Взаимоисключаем видимость кнопок редактирования по событию изменения видимости
            if (ButtonEditOnlyAdv != null)
                ButtonEditOnlyAdv.VisibleChanged += ButtonEditOnlyAdv_VisibleChanged;
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
                // создаём первый CTS для начальной загрузки,
                // чтобы его можно было отменить при закрытии формы / смене вкладки
                var ct = StartNewLoadToken();
                await LoadWorkDivisions(ct);
                // После загрузки восстановим фокус, если есть сохранённый AnnID
                if (_lastFocusedAnnId > 0)
                {
                    await RestoreFocusAsync(_lastFocusedAnnId);
                }
                InitHeaderButtonTags();

                // Инициализируем переменную состояния кнопки "показать все"
                var showAllButton = FindButtonByTag(layoutControlGroup14, "bind:show-all");
                if (showAllButton != null)
                {
                    showAllWD = showAllButton.Checked;
                }

                // Инициализируем объект управления кнопкой "bind:unlink"
                ButtonUnbindWd = new ButtonUnbindWd(layoutControlGroup14, "bind:unlink");
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

                    // Фокус и связанные данные при фильтрации/поиске — после применения фильтра в колонках/панели поиска
                    ANNgridView.ColumnFilterChanged -= ANNgridView_ActiveFilterChanged;
                    ANNgridView.ColumnFilterChanged += ANNgridView_ActiveFilterChanged;

                    //// Подписываемся на событие изменения текста поиска
                    //if (ANNgridView.IsFocusedView && ANNgridView.RowCount > 0 && ANNgridView.FocusedRowHandle >= 0) // Проверка перед вызовом
                    //{
                    //    ANNgridView_FocusedRowChanged_Internal(ANNgridView, new FocusedRowChangedEventArgs(-1, ANNgridView.FocusedRowHandle));
                    //}

                    // Включаем подсветку строк
                    ApplyAnnGridRowStyling();
                }

                // Устанавливаем начальный режим (обычный)
                SetNormalMode();
            }
        }

        /// <summary>
        /// Если видна расширенная кнопка редактирования — скрываем обычную
        /// Если расширенная скрыта — показываем обычную при наличии прав
        /// </summary>
        private void ButtonEditOnlyAdv_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                if (ButtonEditOnlyAdv == null || ButtonEditWd == null) return;
                if (ButtonEditOnlyAdv.Visible)
                {
                    ButtonEditWd.VisibleLogic = false;
                }
                else if (ButtonEditWd.VisiblePermission)
                {
                    ButtonEditWd.VisibleLogic = true;
                }
            }
            catch { }
        }

        private async void XtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == null) return;

            // Отменяем все активные загрузки на предыдущей вкладке
            CancelAllLoads();
            // для новой вкладки создаём НОВЫЙ токен
            var token = StartNewLoadToken();
            switch (e.Page.Name)
            {
                case "TabPage1":
                    await LoadWorkDivisions(token);
                    break;

                case "xtraTabPageArticles":
                    // Загружаем данные для вкладки артикулов с токеном отмены
                    await CurrentWorks_Load(token);//_loadCts.Token);
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

            // Отменяем все активные загрузки на предыдущей вложенной вкладке
            CancelAllLoads();

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


            ArtNormN CopyedWorkDivisionShell = selectedAnnToDuplicate.CloneOperationalData();
            // Инициализируем корректный начальный статус для дубля
            CopyedWorkDivisionShell.Status = (int)Status.Preliminary;
            CopyedWorkDivisionShell.StatusText = StatusHelper.GetStatusText(CopyedWorkDivisionShell.Status);
            CopyedWorkDivisionShell.AnnID = 0;
            CopyedWorkDivisionShell.Arh = false;
            CopyedWorkDivisionShell.dateCreate = DateTime.Now;
            CopyedWorkDivisionShell.dateUpdate = null;

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
                    await _artNormService.DeleteRelatedNormTables(newAnnId);
                    await _artNormService.DeleteByAnnId(TableNames.Ann, newAnnId);
                }
            };
        }
        private async Task SendMsgToBrig(int annId, string msg)
        {
            try
            {
                List<Brig> brigades = await _artNormService.GetWorkingBrigs(annId);
                var brigIds = brigades?
    .Select(b => b.id_brig)
    .Where(id => id > 0)
    .Distinct()
    .ToArray();

                if (brigIds is { Length: > 0 })
                {
                    await _jabberSender.SendToBrigsAsync(brigIds, msg);
                    await _logger.LogEventAsync(
                        $"Отправлено '{msg}' в {brigIds.Length} бригад(ы) для annId={annId}",
                        "EditWd_Internal2");
                }
                else
                {
                    await _logger.LogEventAsync(
                        $"Бригад для рассылки не найдено (annId={annId})",
                        "EditWd_Internal2");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, "Ошибка при отправке сообщения в бригады");
            }
        }

        ///// <summary>
        ///// Устанавливает видимость кнопки customSimpleButtonUnbind
        ///// </summary>
        ///// <param name="visible">Видимость кнопки</param>
        //private void SetCustomSimpleButtonUnbindVisible(bool visible)
        //{
        //    customSimpleButtonUnbindVisible = visible;
        //    if (customSimpleButtonUnbind != null)
        //    {
        //        customSimpleButtonUnbind.Visible = visible;
        //    }
        //}

        ///// <summary>
        ///// Получает состояние видимости кнопки customSimpleButtonUnbind
        ///// </summary>
        ///// <returns>true если кнопка видима</returns>
        //private bool GetCustomSimpleButtonUnbindVisible()
        //{
        //    return customSimpleButtonUnbindVisible;
        //}

        ///// <summary>
        ///// Показать кнопку отвязки артикулов
        ///// </summary>
        //private void ShowUnbindButton()
        //{
        //    SetCustomSimpleButtonUnbindVisible(true);
        //}

        ///// <summary>
        ///// Скрыть кнопку отвязки артикулов
        ///// </summary>
        //private void HideUnbindButton()
        //{
        //    SetCustomSimpleButtonUnbindVisible(false);
        //}

    }
}
