using DevExpress.CodeParser;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ButtonsPanelControl;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraSpreadsheet.Model;
using Newtonsoft.Json.Serialization;
using SewingProduction;
using SewingProduction.Core.Class.Settings;
using SewingProduction.Core.helpers;
using SewingProduction.Core.interfaces;
using SewingProduction.Core.Models;
using SewingProduction.Core.services;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

#nullable enable
namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class KnitterWorkSpace : CustomForm, IServiceBrokerHost
    {
        private readonly ServiceBrokerController _sbController;
        private readonly HashSet<string> _ignoredServiceBrokerTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// Оркестратор доменной логики: загрузка данных, сохранение дат и прочие операции.
        /// </summary>
        private readonly IKnitterOrchestrator _orchestrator;

        /// <summary>
        /// Сервис для работы с ServiceBroker.
        /// </summary>
        private ServiceBrokerService? _sbService;

        /// <summary>
        /// Список SQL-объектов для отслеживания через ServiceBroker.
        /// </summary>
        private static readonly IReadOnlyList<string> _sbObjects =
            new[] { "GetPlanZagrVyazNorm_ByTab3" };
        public IReadOnlyList<string> ServiceBrokerObjects => _sbObjects;
        /// <summary>
        /// Приоритеты обновления объектов (чем выше число, тем выше приоритет).
        /// </summary>
        private static readonly IReadOnlyDictionary<string, int> _sbPriorities =
    new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
    {
        { "GetPlanZagrVyazNorm_ByTab3", 10 }
    };
        public IReadOnlyDictionary<string, int> RefreshPriorities => _sbPriorities;
        public string ServiceBrokerFormName => GetType().Name;

        public bool UseSchemaInListenName => true;

        public IReadOnlyCollection<string> IgnoredTables => _ignoredServiceBrokerTables;
        /// <summary>
        /// Источник данных, к которому привязан GridControl.
        /// </summary>
        private readonly BindingSource _planBindingSource = new BindingSource();
        /// <summary>
        /// Презентер, который собирает иерархию мастер-деталь и настраивает события.
        /// </summary>
        private readonly KnitterPlanPresenter _planPresenter = new KnitterPlanPresenter();
        /// <summary>
        /// Список ID незавершённых операций (pzvID) для подсветки красным цветом
        /// </summary>
        private HashSet<int> _unfinishedOperationIds = new HashSet<int>();
        private CheckBox _adminToggle;
        private CheckBox _expandNrToggle;
        private RepositoryItemProgressBar _statusProgressBar;
        private DevExpress.XtraGrid.GridGroupSummaryItem _pzvChasNaznGroupSumItem;

        // Вью для третьего уровня (деталь детальной таблицы)
        private RepositoryItemButtonEdit _pzvDateStartButtonEdit;
        private RepositoryItemTextEdit _pzvDateStartTextEdit;
        private RepositoryItemButtonEdit _pzvDateEndButtonEdit;
        private RepositoryItemTextEdit _pzvDateEndTextEdit;

        /// <summary>
        /// Таймер для отслеживания бездействия пользователя (1 минута).
        /// </summary>
        private readonly System.Windows.Forms.Timer _idleTimer = new System.Windows.Forms.Timer();
        /// <summary>
        /// Таймер смены (идёт с момента нажатия 'Начать смену' до 'Закончить смену').
        /// </summary>
        private readonly System.Windows.Forms.Timer _shiftTimer = new System.Windows.Forms.Timer();
        private readonly System.Windows.Forms.Timer _blinkCheckTimer = new System.Windows.Forms.Timer();
        private readonly System.Windows.Forms.Timer _blinkTimer = new System.Windows.Forms.Timer();
        private bool _isBlinking;
        private bool _isGroupRowCellHandlerAttached;
        private string _lastBlinkWindowKey;
        private GridGroupSummaryItem _planChasGroupSummaryItem;
        private GridGroupSummaryItem _factChasGroupSummaryItem;
        private DateTime _blinkEndTime;
        private Color _buttonDefaultBackColor;
        private Color _planFooterColor;
        private Color _factFooterColor;
        private TimeSpan _blinkTimeMorning = new TimeSpan(8, 0, 0);
        private TimeSpan _blinkTimeEvening = new TimeSpan(20, 0, 0);
        private int _blinkDurationMinutes = 1;
        private decimal _maxHoursClosedShift = 14m;
        private bool _showAllAssignedWhenClosed = false;
        private Button _adminSettingsButton;
        private CancellationTokenSource? _sbCts;
        private int _serviceBrokerShutdownStarted;
        /// <summary>
        /// Флаг активной смены.
        /// </summary>
        private bool _isShiftRunning = false;
        /// <summary>
        /// Текущая запись смены (kwsID) для закрытия.
        /// </summary>
        private int? _currentShiftId = null;
        /// <summary>
        /// Время старта смены.
        /// </summary>
        private DateTime? _shiftStartTime = null;
        /// <summary>
        /// Текущая зона сотрудника (id и номер).
        /// </summary>
        private int? _currentKmaId = null;
        private string _currentKmaNum = null;
        /// <summary>
        /// Последний загруженный табельный номер, чтобы не перезагружать план без смены таба.
        /// </summary>
        private int? _currentLoadedTab = null;

        /// <summary>
        /// Список ФИО для повторного показа сплеша при бездействии.
        /// </summary>
        private List<FioModel> _cachedFioList;

        /// <summary>
        /// Флаг, указывающий, что сплеш выбора сотрудника уже открыт.
        /// </summary>
        private bool _isSplashShowing = false;

        /// <summary>
        /// Инициализирует форму рабочего места вязальщика.
        /// Настраивает источники данных, колонки гридов, оркестратор и подписки.
        /// </summary>
        public KnitterWorkSpace(UserClass user) : base(user)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("[KnitterWorkSpace] ctor(UserClass) start");
                InitializeComponent();
                _planFooterColor = Color.LightCoral;
                _factFooterColor = Color.LightSkyBlue;
                _sbController = new ServiceBrokerController(this);
                dataLayoutControl1.DataSource = _planBindingSource;

                ConfigureAdvBandedGridColumns();

                var dbHelper = new DatabaseHelper();
                IKnitterRepository repo = new KnitterRepository(dbHelper);
                _orchestrator = new KnitterOrchestrator(repo, new FileLogger());

                PlanZagrVyazGridControl.DataSource = _planBindingSource;

                // Детализация на втором уровне настраивается в Designer: advBandedGridView1 является шаблоном уровня "ArtNom"
                this.Load += async (s, e) =>
                {
                    System.Diagnostics.Debug.WriteLine("[KnitterWorkSpace] Load event start");
                    await InitializeAsync();
                    _sbCts = new CancellationTokenSource();
                    await InitServiceBrokerAsync(_sbCts.Token);
                    InitHeaderButtonTags();
                };
                this.FormClosing += KnitterWorkSpace_FormClosing;
                SetupPzvDateStartColumn();
                SetupIdleTimer();
                SetupShiftTimer();
                InitAdminToggle();
                InitExpandNrToggle();
                SetupStatusColumn();
                SetupRowStyling();
                SetupGridFonts();
                bandedGridView3.MasterRowExpanded += BandedGridView3_MasterRowExpanded;
                bandedGridView3.ShowingEditor += GridView_PreventForeignEdit;
                advBandedGridView1.ShowingEditor += GridView_PreventForeignEdit;
                bandedGridView3.CustomColumnDisplayText += BandedGridView3_CustomColumnDisplayText;
                bandedGridView3.CustomDrawFooterCell += BandedGridView3_CustomDrawFooterCell;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка инициализации формы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Вариант конструктора с внедрением зависимостей (DI).
        /// </summary>
        /// <param name="orchestrator">Оркестратор доменной логики.</param>
        public KnitterWorkSpace(IKnitterOrchestrator orchestrator)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("[KnitterWorkSpace] ctor(IKnitterOrchestrator) start");
                InitializeComponent();
                _planFooterColor = Color.LightCoral;
                _factFooterColor = Color.LightSkyBlue;
                _sbController = new ServiceBrokerController(this);
                _orchestrator = orchestrator ?? throw new ArgumentNullException(nameof(orchestrator));
                dataLayoutControl1.DataSource = _planBindingSource;
                ConfigureAdvBandedGridColumns();
                PlanZagrVyazGridControl.DataSource = _planBindingSource;
                // Детализация на втором уровне настраивается в Designer: advBandedGridView1 является шаблоном уровня "ArtNom"
                this.Load += async (s, e) =>
                {
                    System.Diagnostics.Debug.WriteLine("[KnitterWorkSpace] Load event start");
                    await InitializeAsync();
                    _sbCts = new CancellationTokenSource();
                    await InitServiceBrokerAsync(_sbCts.Token);
                    InitHeaderButtonTags();
                };
                this.FormClosing += KnitterWorkSpace_FormClosing;
                SetupPzvDateStartColumn();
                SetupIdleTimer();
                SetupShiftTimer();
                InitAdminToggle();
                InitExpandNrToggle();
                //InitAdminSettingsButton();
                SetupStatusColumn();
                SetupBlinkTimers();
                SetupRowStyling();
                SetupGridFonts();
                bandedGridView3.MasterRowExpanded += BandedGridView3_MasterRowExpanded;
                bandedGridView3.ShowingEditor += GridView_PreventForeignEdit;
                advBandedGridView1.ShowingEditor += GridView_PreventForeignEdit;
                bandedGridView3.CustomColumnDisplayText += BandedGridView3_CustomColumnDisplayText;
                bandedGridView3.CustomDrawFooterCell += BandedGridView3_CustomDrawFooterCell;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка инициализации формы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Первичная инициализация: загрузка справочника ФИО, установка дефолтного табеля (1438) и автозагрузка плана.
        /// </summary>
        private async Task InitializeAsync()
        {
            try
            {
                // Заполняем список ФИО
                List<FioModel> fioList = await _orchestrator.GetFioListAsync();
                fioList = await FilterFioByOpenShiftAsync(fioList);
                fioList ??= new List<FioModel>();

                // Жестко выбираем табельный при загрузке формы
                const int defaultTab = 1438;
                bool hasDefault = fioList.Any(f => f.Tab == defaultTab);
                if (!hasDefault)
                {
                    string defaultFio = await _orchestrator.GetFioByTabAsync(defaultTab);
                    if (!string.IsNullOrWhiteSpace(defaultFio))
                    {
                        fioList.Insert(0, new FioModel { Tab = defaultTab, Fio = defaultFio });
                    }
                }

                // Отображаем ФИО, зона — отдельным столбцом в всплывающем списке
                FioGridLookUpEdit.Properties.DisplayMember = nameof(FioModel.Fio);
                FioGridLookUpEdit.Properties.ValueMember = nameof(FioModel.Tab);
                FioGridLookUpEdit.Properties.DataSource = fioList;
                TabGridLookUpEdit.Properties.DisplayMember = nameof(FioModel.Tab);
                TabGridLookUpEdit.Properties.ValueMember = nameof(FioModel.Tab);
                TabGridLookUpEdit.Properties.DataSource = fioList;
                dateEdit1.EditValue = DateTime.Now;

                // Сохраняем список для повторного показа сплеша при бездействии
                _cachedFioList = fioList;

                PresentFioSelectionSplash(fioList, defaultTab);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка загрузки списка сотрудников: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PresentFioSelectionSplash(IReadOnlyCollection<FioModel> fioList, int defaultTab)
        {
            if (fioList == null || fioList.Count == 0)
            {
                XtraMessageBox.Show(this, "Список сотрудников пуст. Обратитесь к администратору.", "Нет данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Останавливаем таймер бездействия, пока показывается сплеш
            _idleTimer.Stop();

            int? currentTab = int.TryParse(FioGridLookUpEdit.EditValue?.ToString(), out int parsedTab)
                ? parsedTab
                : (int?)null;

            int? initialTab = currentTab;
            if (initialTab is null && fioList.Any(f => f.Tab == defaultTab))
            {
                initialTab = defaultTab;
            }

            _isSplashShowing = true;
            double oldOpacity = this.Opacity;
            Form overlay = null;
            try
            {
                // Перекрываем только текущую вкладку/форму KnitterWorkSpace, не блокируя остальные вкладки/кнопки
                overlay = new Form();
                overlay.FormBorderStyle = FormBorderStyle.None;
                overlay.StartPosition = FormStartPosition.Manual;
                overlay.ShowInTaskbar = false;
                overlay.BackColor = System.Drawing.Color.AliceBlue;
                overlay.TopMost = false; // достаточно быть над текущей формой
                overlay.Owner = this;

                // Берём границы основного layout текущей вкладки; если что-то пойдёт не так — используем всю клиентскую область формы
                var bounds = dataLayoutControl1?.RectangleToScreen(dataLayoutControl1.ClientRectangle)
                    ?? this.RectangleToScreen(this.ClientRectangle);
                overlay.Bounds = bounds;
                overlay.Show();

                using (var splash = new FioSelectionSplash(fioList, initialTab))
                {
                    splash.StartPosition = FormStartPosition.CenterScreen;
                    var result = splash.ShowDialog(overlay);
                    if (result == DialogResult.OK && splash.SelectedTab.HasValue)
                    {
                        FioGridLookUpEdit.EditValue = splash.SelectedTab.Value;
                        TabGridLookUpEdit.EditValue = splash.SelectedTab.Value;
                        // Перезапускаем таймер после успешного выбора
                        ResetIdleTimer();
                    }
                    else
                    {
                        BeginInvoke(new Action(Close));
                    }
                }
            }
            finally
            {
                // Убираем оверлей и возвращаем видимость формы
                if (overlay != null)
                {
                    try { overlay.Close(); } catch { }
                    overlay.Dispose();
                }
                this.Opacity = oldOpacity;
                _isSplashShowing = false;
            }
        }

        /// <summary>
        /// Дополнительная настройка второго уровня (advBandedGridView1):
        /// - создаёт скрытую unbound-колонку с готовой строкой заголовка группы
        /// - группирует по этой колонке и авторазворачивает единственную группу
        /// В результате под номером В/М сразу отображается шапка "Пачка | Задание | Размер | Кол-во" и таблица операций.
        /// </summary>
        private void ConfigureAdvBandedGridColumns()
        {
            if (advBandedGridView1 == null)
                return;

            advBandedGridView1.BeginUpdate();
            try
            {
                var headerCol = advBandedGridView1.Columns.ColumnByFieldName("__Header");
                if (headerCol == null)
                {
                    headerCol = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                    {
                        FieldName = "__Header",
                        Caption = "Header",
                        UnboundType = DevExpress.Data.UnboundColumnType.String,
                        UnboundExpression =
                         "Concat('№пачки: ', [n_pach], ' | Размер: ', [razm], ' | Кол-во: ', [pzvRKol])",
                        Visible = false,
                        OptionsColumn = { ShowInCustomizationForm = false }
                    };
                    advBandedGridView1.Columns.Add(headerCol);
                }

                advBandedGridView1.ClearGrouping();
                headerCol.GroupIndex = 0;

                advBandedGridView1.GroupFormat = "{1}";
                advBandedGridView1.OptionsView.ShowGroupedColumns = false;
                advBandedGridView1.OptionsView.ShowGroupPanel = false;
                advBandedGridView1.OptionsBehavior.AlignGroupSummaryInGroupRow = DefaultBoolean.True;
                advBandedGridView1.ExpandAllGroups();
            }
            finally
            {
                advBandedGridView1.EndUpdate();
            }
        }

        private void BandedGridView3_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            if (sender is not BandedGridView masterView)
                return;

            var detailView = masterView.GetDetailView(e.RowHandle, e.RelationIndex) as AdvBandedGridView;
            ApplyOperationNameWidth(detailView);
        }

        private static void ApplyOperationNameWidth(AdvBandedGridView? detailView)
        {
            if (detailView == null)
                return;

            const int operationNameWidth = 465;

            detailView.BeginUpdate();
            try
            {
                // detailView.OptionsView.ColumnAutoWidth = false;

                var operationCol = detailView.Columns.ColumnByFieldName("nrText");
                if (operationCol != null)
                {
                    operationCol.OptionsColumn.FixedWidth = true;
                    operationCol.MinWidth = operationNameWidth;
                    operationCol.MaxWidth = operationNameWidth;
                    operationCol.Width = operationNameWidth;
                }

                var operationBand = detailView.Bands
                    .Cast<GridBand>()
                    .FirstOrDefault(b => b.Columns.Contains(operationCol));
                if (operationBand != null)
                {
                    operationBand.OptionsBand.FixedWidth = true;
                    operationBand.MinWidth = operationNameWidth;
                    operationBand.Width = operationNameWidth;
                }
            }
            finally
            {
                detailView.EndUpdate();
                detailView.LayoutChanged();
            }
        }


        /// <summary>
        /// Обработчик выбора сотрудника: получает план по табелю, собирает иерархию и привязывает к гриду.
        /// </summary>
        private async void FioGridLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                // Сбрасываем таймер бездействия при активности пользователя
                ResetIdleTimer();

                if (FioGridLookUpEdit.EditValue == null || !int.TryParse(FioGridLookUpEdit.EditValue.ToString(), out int tab))
                {
                    _planBindingSource.DataSource = null;
                    PlanZagrVyazGridControl.RefreshDataSource();
                    TabGridLookUpEdit.EditValue = null;
                    _currentLoadedTab = null;
                    return;
                }

                await LoadPlanForTabAsync(tab);
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(this, $"Ошибка доступа к базе данных при загрузке плана: {ex.Message}", "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка загрузки плана: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Кнопка "Начать смену": массово назначает табель выбранным строкам, присваивает kolNazn, ChasiNazn, sekNazn и обновляет отображение
        /// </summary>
        private async void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                // Если смена уже запущена — завершаем смену: запись в БД, остановка таймера и смена текста
                if (_isShiftRunning)
                {
                    // Перед завершением смены: обработать все операции; если есть незавершённые — не закрываем.
                    var canClose = await ProcessOperationsOnShiftEndAsync();
                    if (!canClose)
                        return;

                    // Очищаем список незавершённых операций при успешном закрытии смены
                    _unfinishedOperationIds.Clear();
                    // Обновляем отображение, чтобы убрать подсветку
                    this.BeginInvoke(new Action(() =>
                    {
                        bandedGridView3?.RefreshData();
                        advBandedGridView1?.RefreshData();
                    }));
                    //// снимаем назначение у всех НЕ начатых в текущей смене
                    var stat = (await _orchestrator.AdjustNotStartedBeforeShiftEndAsync(_currentShiftId, 12m)).ToList();

                    if (!int.TryParse(FioGridLookUpEdit.EditValue?.ToString(), out int tabEnd) || tabEnd <= 0)
                    {
                        XtraMessageBox.Show(this, "Не удалось определить табель при завершении смены.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (_currentShiftId.HasValue && _currentShiftId.Value > 0)
                    {
                        await _orchestrator.EndWorkingShiftAsync(_currentShiftId.Value, tabEnd);
                        // Перезагрузим план, чтобы обновить статусы/проценты
                        await LoadPlanForTabAsync(tabEnd, forceReload: true);
                    }

                    await RefreshFioListAsync();
                    ApplyShiftUi(false, null, null);
                    return;
                }

                if (!int.TryParse(FioGridLookUpEdit.EditValue?.ToString(), out int selectedTab) || selectedTab <= 0)
                {
                    XtraMessageBox.Show(this, "Выберите сотрудника для назначения табельного номера.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Проверка: нельзя открыть вторую смену в зоне
                if (_currentKmaId.HasValue)
                {
                    var openByZone = await _orchestrator.GetOpenShiftByZoneAsync(_currentKmaId.Value);
                    if (openByZone.shiftId.HasValue)
                    {
                        XtraMessageBox.Show(this, $"В зоне {_currentKmaNum} уже открыта смена (таб. {openByZone.tabStart}), сначала завершите её.", "Смена уже открыта", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Назначаем таб ВСЕМ загруженным строкам 
                var rowsForUpdate = _planPresenter.AllRows?.ToList() ?? new List<KnitterPZVModel>();
                if (!rowsForUpdate.Any())
                {
                    XtraMessageBox.Show(this, "Нет строк для назначения табельного номера.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var pzvIds = rowsForUpdate
                    .Select(r => r.pzvID)
                    .Where(id => id > 0)
                    .Distinct()
                    .ToList();

                if (pzvIds.Count == 0)
                {
                    XtraMessageBox.Show(this, "Не удалось определить записи для обновления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // Успешный старт смены: фиксируем в БД, проставляем pzvKwsID для всех! операций, меняем текст кнопки и запускаем таймер
                try
                {
                    await _orchestrator.SetPzvTabAsync(pzvIds, selectedTab);
                    _currentShiftId = await _orchestrator.StartWorkingShiftAsync(selectedTab, _currentKmaId, _currentKmaNum);
                    if (_currentShiftId.HasValue && _currentShiftId.Value > 0)
                    {
                        await _orchestrator.UpdatePzvKwsIdAsync(pzvIds, _currentShiftId.Value);
                    }

                    // Обновим план после проставления pzvKwsID
                    await LoadPlanForTabAsync(selectedTab, forceReload: true);
                    await RefreshFioListAsync();

                    ApplyShiftUi(true, _currentShiftId, DateTime.Now);
                }
                catch (Exception exStart)
                {
                    XtraMessageBox.Show(this, $"Не удалось записать начало смены: {exStart.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка при назначении табельного номера: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshStatusColumns()
        {
            // Форсируем перерасчёт unbound-колонок (процент/статус)
            bandedGridView3?.RefreshData();
            advBandedGridView1?.RefreshData();
            RefreshFooterSummaries();
        }

        /// <summary>
        /// Обновляет футеры после изменений данных.
        /// </summary>
        private void RefreshFooterSummaries()
        {
            bandedGridView3?.UpdateSummary();
            advBandedGridView1?.UpdateSummary();
        }

        /// <summary>
        /// Перезагружает список ФИО с учётом фильтра по открытым сменам, сохраняет текущий выбор, если он есть.
        /// </summary>
        private async Task RefreshFioListAsync()
        {
            var currentSelection = FioGridLookUpEdit.EditValue?.ToString();

            List<FioModel> fioList = await _orchestrator.GetFioListAsync();
            fioList = await FilterFioByOpenShiftAsync(fioList);
            fioList ??= new List<FioModel>();

            FioGridLookUpEdit.Properties.DataSource = fioList;
            TabGridLookUpEdit.Properties.DataSource = fioList;
            _cachedFioList = fioList;

            if (int.TryParse(currentSelection, out int tab) && fioList.Any(f => f.Tab == tab))
            {
                FioGridLookUpEdit.EditValue = tab;
                TabGridLookUpEdit.EditValue = tab;
            }
            else
            {
                FioGridLookUpEdit.EditValue = null;
                TabGridLookUpEdit.EditValue = null;
            }
        }

        private void ShowAdminSettingsDialog()
        {
            using var form = new Form
            {
                Text = "Настройки админки",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false,
                ClientSize = new Size(320, 220)
            };

            var lblBlink1 = new Label { Text = "Время мигания 1:", Location = new Point(10, 20), AutoSize = true };
            var timeBlink1 = new DateTimePicker
            {
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true,
                Location = new Point(150, 16),
                Width = 120,
                Value = DateTime.Today.Add(_blinkTimeMorning)
            };

            var lblBlink2 = new Label { Text = "Время мигания 2:", Location = new Point(10, 55), AutoSize = true };
            var timeBlink2 = new DateTimePicker
            {
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true,
                Location = new Point(150, 51),
                Width = 120,
                Value = DateTime.Today.Add(_blinkTimeEvening)
            };

            var lblMaxHours = new Label { Text = "MaxHours (закрытая):", Location = new Point(10, 90), AutoSize = true };
            var numMaxHours = new NumericUpDown
            {
                Location = new Point(150, 86),
                Width = 120,
                DecimalPlaces = 1,
                Minimum = 0,
                Maximum = 500,
                Value = _maxHoursClosedShift
            };

            var chkShowAllAssigned = new CheckBox
            {
                Text = "Показывать все назначенные (закрытая)",
                Location = new Point(10, 125),
                AutoSize = true,
                Checked = _showAllAssignedWhenClosed
            };

            var btnOk = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(70, 170), Width = 80 };
            var btnCancel = new Button { Text = "Отмена", DialogResult = DialogResult.Cancel, Location = new Point(170, 170), Width = 80 };

            form.Controls.AddRange(new Control[] { lblBlink1, timeBlink1, lblBlink2, timeBlink2, lblMaxHours, numMaxHours, chkShowAllAssigned, btnOk, btnCancel });
            form.AcceptButton = btnOk;
            form.CancelButton = btnCancel;

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _blinkTimeMorning = timeBlink1.Value.TimeOfDay;
                _blinkTimeEvening = timeBlink2.Value.TimeOfDay;
                _maxHoursClosedShift = numMaxHours.Value;
                _showAllAssignedWhenClosed = chkShowAllAssigned.Checked;

                // Сбросим ключ окна, чтобы мигание могло сработать с новыми настройками
                _lastBlinkWindowKey = null;
            }
        }

        private void SetupBlinkTimers()
        {
            _buttonDefaultBackColor = simpleButton2.BackColor;
            _blinkCheckTimer.Interval = 15_000; // раз в 15 секунд проверяем окно 8:00/20:00
            _blinkCheckTimer.Tick += BlinkCheckTimer_Tick;
            _blinkCheckTimer.Start();

            _blinkTimer.Interval = 500; // мигаем раз в полсекунды
            _blinkTimer.Tick += BlinkTimer_Tick;
        }

        private void BlinkCheckTimer_Tick(object sender, EventArgs e)
        {
            if (_isBlinking)
                return;

            var now = DateTime.Now;
            var windowKey = GetBlinkWindowKey(now);
            if (windowKey == null)
                return;
            if (windowKey == _lastBlinkWindowKey)
                return; // уже мигали в этом окне

            var windowStart = GetWindowStart(now);
            if (now >= windowStart && now <= windowStart.AddMinutes(1))
            {
                StartBlink(windowKey, windowStart.AddMinutes(1));
            }
        }

        private void BlinkTimer_Tick(object sender, EventArgs e)
        {
            if (!_isBlinking)
                return;

            if (DateTime.Now >= _blinkEndTime)
            {
                StopBlink();
                return;
            }

            // Тоггл цвета между фиолетовым и дефолтным
            simpleButton2.BackColor = simpleButton2.BackColor == Color.MediumPurple
                ? _buttonDefaultBackColor
                : Color.MediumPurple;
        }

        private void StartBlink(string windowKey, DateTime endTime)
        {
            _isBlinking = true;
            _blinkEndTime = endTime;
            _lastBlinkWindowKey = windowKey;
            _blinkTimer.Start();
        }

        private void StopBlink()
        {
            _blinkTimer.Stop();
            _isBlinking = false;
            simpleButton2.BackColor = _buttonDefaultBackColor;
        }

        private static string GetBlinkWindowKey(DateTime now)
        {
            if (IsInBlinkWindow(now))
            {
                return $"{now:yyyyMMdd}_{now.Hour}";
            }
            return null;
        }

        private static DateTime GetWindowStart(DateTime now)
        {
            if (now.Hour >= 15 && now.Hour < 17)
                return new DateTime(now.Year, now.Month, now.Day, 16, 13, 0);
            if (now.Hour >= 17)
                return new DateTime(now.Year, now.Month, now.Day, 20, 0, 0);
            // до 8 утра: окно предыдущего дня в 20:00 уже прошло, следующее — 8:00 сегодняшнего
            return new DateTime(now.Year, now.Month, now.Day, 8, 0, 0);
        }

        private static bool IsInBlinkWindow(DateTime now)
        {
            var start8 = new DateTime(now.Year, now.Month, now.Day, 16, 8, 0);
            var start20 = new DateTime(now.Year, now.Month, now.Day, 16, 7, 0);

            return (now >= start8 && now <= start8.AddMinutes(1)) ||
                   (now >= start20 && now <= start20.AddMinutes(1));
        }

        /// <summary>
        /// При завершении смены: для неначатых — split mode=2 с отриц. количеством; для начатых без конца — спросить факт и закрыть.
        /// </summary>
        private async Task<bool> ProcessOperationsOnShiftEndAsync()
        {
            var rows = _planPresenter.AllRows?.Where(r => r != null && r.pzvID > 0).ToList() ?? new List<KnitterPZVModel>();
            if (!rows.Any())
                return true;


            // Начатые, но не завершённые → спросить факт, закрыть, при необходимости split по факту
            var inProgress = rows.Where(r => r.pzvDateStart != null && r.pzvDateEnd == null).ToList();
            if (inProgress.Any())
            {
                // Сохраняем ID незавершённых операций для подсветки
                _unfinishedOperationIds = new HashSet<int>(inProgress.Where(r => r.pzvID > 0).Select(r => r.pzvID));

                // Сразу обновляем отображение для подсветки незавершённых операций
                bandedGridView3?.RefreshData();
                advBandedGridView1?.RefreshData();

                // Позиционируемся на первую незавершённую операцию и разворачиваем её группы
                var firstUnfinished = inProgress.FirstOrDefault(r => r.pzvID > 0);
                if (firstUnfinished != null)
                {
                    var snap = new PlanFocusSnap
                    {
                        MasterTop = bandedGridView3?.TopRowIndex ?? 0,
                        TaskNum = KnitterPlanUtils.NormalizeTaskNum(firstUnfinished.pzvNomZad),
                        Machine = KnitterPlanUtils.NormalizeMachineKey(firstUnfinished.kmlNumber),
                        DetailPzvId = firstUnfinished.pzvID,
                        WasInDetail = true
                    };

                    RestorePlanFocus(snap, preferDetailId: firstUnfinished.pzvID);
                }

                // Показываем сообщение после обновления отображения
                MessageBox.Show("В смене есть начатые, но не завершённые операции. Завершите операции, прежде чем закончить смену.", "Завершение операций", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            // Неначатые (нет даты старта и окончания) → split mode=2
            var notStarted = rows.Where(r => r.pzvDateStart == null && r.pzvDateEnd == null).ToList();
            foreach (var row in notStarted)
            {
                try
                { //если завершается в конце смены с фактом 0 - это случай 2 с отрицательной строкой
                    await _orchestrator.SplitPzvAsync(row.pzvID, 2, 0);
                }
                catch
                {
                    // Игнорируем сбой split одной операции, продолжаем остальные
                }
            }

            return true;
        }

        /// <summary>
        /// Настройка редакторов ячеек для колонок "Начато" и "Закончено":
        /// - в "Начато" показывает кнопку, если дата пустая, и текст — если дата заполнена
        /// - в "Закончено" показывает кнопку "Завершить" только когда дата начала уже заполнена и дата окончания пуста
        /// - регистрирует репозитории редакторов в GridControl
        /// </summary>
        private void SetupPzvDateStartColumn()
        {
            bandedGridColumn18.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            bandedGridColumn18.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            bandedGridColumn18.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";

            _pzvDateStartButtonEdit = new RepositoryItemButtonEdit { TextEditStyle = TextEditStyles.HideTextEditor };
            _pzvDateStartButtonEdit.Buttons.Clear();
            _pzvDateStartButtonEdit.Buttons.Add(new EditorButton(ButtonPredefines.Glyph, "Начать", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleLeft, null));
            _pzvDateStartButtonEdit.DoubleClick += PzvDateStartButtonEdit_DoubleClick;

            _pzvDateStartTextEdit = new RepositoryItemTextEdit { ReadOnly = true };

            PlanZagrVyazGridControl.RepositoryItems.Add(_pzvDateStartButtonEdit);
            PlanZagrVyazGridControl.RepositoryItems.Add(_pzvDateStartTextEdit);
            BandedGridColumn dateStart = bandedGridColumn18;
            advBandedGridView1.CustomRowCellEdit += (s, e) =>
            {
                if (e.Column != null && e.Column.FieldName == dateStart.FieldName)
                {
                    var cellValue = e.CellValue;
                    bool isEmpty = cellValue == null ||
                                   cellValue == DBNull.Value ||
                                   (cellValue is DateTime dt && dt == DateTime.MinValue);
                    e.RepositoryItem = isEmpty ? _pzvDateStartButtonEdit : _pzvDateStartTextEdit;
                }
                // Закончено
                if (e.Column != null && e.Column.FieldName == bandedGridColumn19.FieldName)
                {
                    // Кнопка "Завершить" показывается ТОЛЬКО если дата начала заполнена и дата окончания пуста
                    GridView view = (GridView)PlanZagrVyazGridControl.FocusedView;
                    var startValue = view.GetRowCellValue(e.RowHandle, bandedGridColumn18);//advBandedGridView1.GetRowCellValue(e.RowHandle, bandedGridColumn18);
                    bool hasStart = !(startValue == null ||
                                      startValue == DBNull.Value ||
                                      (startValue is DateTime sdt && sdt == DateTime.MinValue));

                    var endValue = e.CellValue;
                    bool isEndEmpty = endValue == null ||
                                      endValue == DBNull.Value ||
                                      (endValue is DateTime edt && edt == DateTime.MinValue);

                    e.RepositoryItem = (hasStart && isEndEmpty) ? _pzvDateEndButtonEdit : _pzvDateEndTextEdit;
                }
            };
            advBandedGridView1.CustomColumnDisplayText -= AdvBandedGridView1_CustomColumnDisplayText;
            advBandedGridView1.CustomColumnDisplayText += AdvBandedGridView1_CustomColumnDisplayText;

            // Настройка для "Закончено" (pzvDateEnd)
            bandedGridColumn19.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            bandedGridColumn19.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            bandedGridColumn19.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";

            _pzvDateEndButtonEdit = new RepositoryItemButtonEdit { TextEditStyle = TextEditStyles.HideTextEditor };
            _pzvDateEndButtonEdit.Buttons.Clear();
            _pzvDateEndButtonEdit.Buttons.Add(new EditorButton(ButtonPredefines.Glyph, "Завершить", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleLeft, null));
            _pzvDateEndButtonEdit.DoubleClick += PzvDateEndButtonEdit_DoubleClick;

            _pzvDateEndTextEdit = new RepositoryItemTextEdit { ReadOnly = true };
            PlanZagrVyazGridControl.RepositoryItems.Add(_pzvDateEndButtonEdit);
            PlanZagrVyazGridControl.RepositoryItems.Add(_pzvDateEndTextEdit);
            // Колонка фактического количества — unbound для отображения введённого значения
            bandedGridColumn22.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
        }

        /// <summary>
        /// Подменяет редактор ячейки "Начато" (кнопка/текст) в зависимости от значения
        /// </summary>
        private void AdvBandedGridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column != bandedGridColumn18)
                return;

            bool isEmpty = e.CellValue == null ||
                          e.CellValue == DBNull.Value ||
                          (e.CellValue is DateTime dt && dt == DateTime.MinValue);

            e.RepositoryItem = isEmpty ? _pzvDateStartButtonEdit : _pzvDateStartTextEdit;
        }

        /// <summary>
        /// Клик по кнопке в "Начато" — установить дату начала для текущей строки
        /// </summary>
        private async void PzvDateStartButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            GridView view = PlanZagrVyazGridControl.FocusedView as GridView;
            await ApplyPzvDateStartAsync(view);
        }

        /// <summary>
        /// Двойной клик по ячейке "Начато" — установить дату начала для текущей строки.
        /// </summary>
        private async void PzvDateStartButtonEdit_DoubleClick(object sender, EventArgs e)
        {
            GridView view = PlanZagrVyazGridControl.FocusedView as GridView;
            await ApplyPzvDateStartAsync(view);
        }

        /// <summary>
        /// Устанавливает дату начала: сохраняет на сервере (с использованием серверного времени) и моментально отражает в ячейке
        /// </summary>
        private async Task ApplyPzvDateStartAsync(GridView view)//int rowHandle)
        {
            await ApplyPzvDateAsync(
                view,
                bandedGridColumn18,
                _orchestrator.UpdatePzvDateStartAsync,
                m => m.pzvDateStart,
                (m, v) => m.pzvDateStart = v,
                "начала");
        }

        /// <summary>
        /// Клик по кнопке "Завершить" — запросить количество и завершить операцию (установить дату окончания)
        /// </summary>
        private async void PzvDateEndButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            GridView view = PlanZagrVyazGridControl.FocusedView as GridView;
            await ApplyPzvDateEndAsync(view);
        }

        /// <summary>
        /// Двойной клик по ячейке "Завершить" — запросить количество и завершить операцию
        /// </summary>
        private async void PzvDateEndButtonEdit_DoubleClick(object sender, EventArgs e)
        {
            GridView view = PlanZagrVyazGridControl.FocusedView as GridView;
            await ApplyPzvDateEndAsync(view);
        }

        /// <summary>
        /// Показываем сумму по группе в ячейке "назначено в м/ч" (pzvChasNazn) в строке группы
        /// </summary>
        private void AdvBandedGridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == null || e.Column.FieldName != "pzvChasNazn")
                return;

            if (_pzvChasNaznGroupSumItem == null)
                return;

            if (sender is DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView view)
            {
                int rowHandle = view.GetRowHandle(e.ListSourceRowIndex);
                if (!view.IsGroupRow(rowHandle))
                    return;

                var val = view.GetGroupSummaryValue(rowHandle, _pzvChasNaznGroupSumItem);
                if (val != null && val != DBNull.Value)
                {
                    e.DisplayText = string.Format("{0:0.00}", val);
                }
            }
        }

        /// <summary>
        /// Считает общие суммы часов по всем строкам детального уровня.
        /// </summary>
        private (decimal planTotal, decimal factTotal) GetGlobalHourTotals()
        {
            if (_planPresenter?.AllRows == null)
                return (0m, 0m);

            decimal plan = _planPresenter.AllRows
                .Where(r => r != null)
                .Sum(r => r.PlanChas_UI ?? 0m);

            decimal fact = _planPresenter.AllRows
                .Where(r => r != null)
                .Sum(r => r.FactChas_UI ?? 0m);

            return (Math.Round(plan, 2), Math.Round(fact, 2));
        }

        /// <summary>
        /// В футере bandedGridView3 показываем локальную сумму и общую сумму по всем строкам.
        /// </summary>
        private void BandedGridView3_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            if (e.Column == null)
                return;

            bool isPlan = string.Equals(e.Column.FieldName, "pzvChasNazn", StringComparison.OrdinalIgnoreCase);
            bool isFact = string.Equals(e.Column.FieldName, "pzvNChasi", StringComparison.OrdinalIgnoreCase);
            if (!isPlan && !isFact)
                return;

            ApplyFooterBackColor(e, isPlan);

            decimal localSum = 0m;
            if (e.Info?.Value != null && e.Info.Value != DBNull.Value && decimal.TryParse(e.Info.Value.ToString(), out var parsedLocal))
            {
                localSum = parsedLocal;
            }

            var totals = GetGlobalHourTotals();
            decimal globalSum = isPlan ? totals.planTotal : totals.factTotal;

            e.Info.DisplayText = $"все: {globalSum:0.##}";
            e.Appearance.BackColor = isPlan ? _planFooterColor : _factFooterColor;
            e.Appearance.Options.UseBackColor = true;
        }

        private void ApplyFooterBackColor(DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e, bool isPlan)
        {
            e.Appearance.BackColor = isPlan ? _planFooterColor : _factFooterColor;
            e.Appearance.Options.UseBackColor = true;
        }

        /// <summary>
        /// Для верхнего уровня (bandedGridView3): в колонке pzvChasNazn отображаем сумму назначенных часов по всем операциям этой машины/задания.
        /// </summary>
        private void BandedGridView3_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == null || e.Column.FieldName != "pzvChasNazn")
                return;

            if (sender is BandedGridView view)
            {
                int rowHandle = view.GetRowHandle(e.ListSourceRowIndex);
                var row = view.GetRow(rowHandle) as KnitterPZVModel;
                if (row == null)
                    return;

                var taskNum = KnitterPlanUtils.NormalizeTaskNum(row.pzvNomZad);
                var machineKey = KnitterPlanUtils.NormalizeMachineKey(row.kmlNumber);

                var sum = _planPresenter.AllRows
                    .Where(r =>
                        KnitterPlanUtils.NormalizeTaskNum(r.pzvNomZad) == taskNum &&
                        KnitterPlanUtils.NormalizeMachineKey(r.kmlNumber) == machineKey)
                    .Sum(r => r.pzvChasNazn);

                e.DisplayText = string.Format("{0:0.00}", sum);
            }
        }

        /// <summary>
        /// Устанавливает часы факт (pzvNChasi), если они пусты, по формуле pzvSek * факт.кол-во / 3600.
        /// </summary>
        private void EnsureFactHours(KnitterPZVModel row)
        {
            if (row == null)
                return;

            // Считаем факт-часы только для завершённых операций (есть дата окончания)
            if ((row.pzvNChasi == null || row.pzvNChasi == 0m) && row.pzvDateEnd != null)
            {
                var factQty = row.pzvKol ?? 0;
                if (row.pzvSek > 0 && factQty > 0)
                {
                    row.pzvNChasi = Math.Round((row.pzvSek * factQty) / 3600m, 2);
                }
            }
        }

        private int? PromptFactQuantity(int defaultQty)
        {
            using (var form = new DevExpress.XtraEditors.XtraForm())
            using (var inputFont = new Font(Font.FontFamily, Font.Size + 8f, FontStyle.Bold))
            using (var labelFont = new Font(Font.FontFamily, Font.Size + 2f, FontStyle.Regular))
            {
                form.Text = "Завершение операции";
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;
                form.ShowInTaskbar = false;
                form.ClientSize = new Size(520, 220);

                var label = new DevExpress.XtraEditors.LabelControl
                {
                    Text = "Количество отвязанных изделий",
                    Font = labelFont,
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    Bounds = new Rectangle(20, 20, 480, 40)
                };

                var input = new DevExpress.XtraEditors.TextEdit
                {
                    Font = inputFont,
                    Bounds = new Rectangle(20, 70, 480, 60),
                    Text = defaultQty.ToString()
                };
                input.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
                input.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
                input.Properties.MaskSettings.Set("mask", "d");
                input.Properties.UseMaskAsDisplayFormat = true;

                var okButton = new DevExpress.XtraEditors.SimpleButton
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Bounds = new Rectangle(260, 150, 110, 40)
                };

                var cancelButton = new DevExpress.XtraEditors.SimpleButton
                {
                    Text = "Отмена",
                    DialogResult = DialogResult.Cancel,
                    Bounds = new Rectangle(390, 150, 110, 40)
                };

                form.Controls.Add(label);
                form.Controls.Add(input);
                form.Controls.Add(okButton);
                form.Controls.Add(cancelButton);
                form.AcceptButton = okButton;
                form.CancelButton = cancelButton;

                var result = form.ShowDialog(this);
                if (result != DialogResult.OK)
                    return null;

                if (int.TryParse(input.Text, out int qty))
                    return qty;

                return null;
            }
        }

        /// <summary>
        /// Запрашивает у пользователя фактическое количество, отражает его в колонке "Кол-во факт (шт)" и устанавливает дату окончания.
        /// </summary>
        private async Task ApplyPzvDateEndAsync(GridView view)
        {
            GridView _view = view;
            int rowHandle = _view?.FocusedRowHandle ?? -1;
            if (_view == null || rowHandle < 0)
                return;

            // Получим текущую строку для плейсхолдера (кол-во к выполнению)
            var currentRow = _view.GetRow(rowHandle) as KnitterPZVModel;

            // Снимаем снапшот фокуса до любых RefreshData: после ApplyPzvDateAsync/RefreshData фокус сбрасывается,
            // и при факт < назн (split) CapturePlanFocus() тогда ловил бы уже другую машину/строку.
            var focusSnap = CapturePlanFocusFromRow(currentRow, _view);

            // Если плановое количество уже перенесено в назначенное (pzvKol обнулён), используем pzvKolNazn как "к выполнению"
            int defaultQty = currentRow?.pzvKolNazn ?? 0;
            if (defaultQty == 0 && currentRow != null && currentRow.pzvKolNazn > 0)
                defaultQty = currentRow.pzvKolNazn;

            // Диалог ввода количества отвязанных изделий
            var qtyObj = PromptFactQuantity(defaultQty);
            if (qtyObj == null)
                return; // отмена
            if (!int.TryParse(qtyObj.ToString(), out int qty) || qty < 0 || qty > defaultQty)
            {
                XtraMessageBox.Show(this, "Значение должно быть меньше запланированного.", "Неверное значение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Обновляем факт в текущей строке и в мастер-коллекции, чтобы статус пересчитался без полной перезагрузки
            // Мгновенно пересчитываем часы факт для прогресса (секунды на изделие * факт / 3600)
            decimal factHours = 0m;
            if (currentRow != null)
            {
                currentRow.pzvKol = qty;
                currentRow.FactKol_UI = qty; // вроде так
                if (currentRow.pzvSek > 0)
                {
                    factHours = Math.Round((currentRow.pzvSek * qty) / 3600m, 2);
                    currentRow.pzvNChasi = factHours;
                }
                var masterRow = _planPresenter.AllRows?.FirstOrDefault(r => r != null && r.pzvID == currentRow.pzvID);
                if (masterRow != null)
                {
                    masterRow.pzvKol = qty;
                    masterRow.FactKol_UI = qty;
                    masterRow.FactChas_UI = factHours;
                    if (factHours > 0)
                        masterRow.pzvNChasi = factHours;
                }
            }
            // Отобразим введённое значение в столбце факта (unbound)
            _view.SetRowCellValue(rowHandle, bandedGridColumn22, qty);
            _view.SetRowCellValue(rowHandle, bandedGridColumn27, factHours);
            _view.PostEditor();
            _view.CloseEditor();
            _view.UpdateCurrentRow();
            _view.RefreshRowCell(rowHandle, bandedGridColumn22);

            // Затем — разделение записи в зависимости от введённого количества
            IReadOnlyList<PzvSplitResult> newIds = Array.Empty<PzvSplitResult>();
            if (currentRow?.pzvID > 0)
            {
                if (defaultQty > 0)
                {
                    if (qty <= defaultQty)
                    {
                        // Факт меньше запланированного — mode = 1 c qtyFact
                        newIds = await _orchestrator.SplitPzvByFactAsync(currentRow.pzvID, qty);
                        Debug.WriteLine(string.Join(", ", newIds.Select(x => $"{x.Kind}:{x.NewPzvId}")));
                    }
                }
            }

            // Обновим план, чтобы показать новую запись остатка (если была создана)
            if (int.TryParse(FioGridLookUpEdit.EditValue?.ToString(), out int tab))
            {
                int? preferDetailId = null;

                if (newIds != null && newIds.Count > 0)
                {
                    // 1️ Пытаемся найти остаток
                    var remainder = newIds.FirstOrDefault(x =>
                        x != null &&
                        string.Equals(x.Kind, "Remainder", StringComparison.OrdinalIgnoreCase));

                    if (remainder != null && remainder.NewPzvId > 0)
                    {
                        // Есть остаток → фокус на нём
                        preferDetailId = remainder.NewPzvId;
                    }
                    else
                    {
                        // 2️⃣ Остатка нет (remaining = 0) → остаёмся на исходной строке
                        preferDetailId = currentRow?.pzvID;

                        // 3️⃣ Fallback (на всякий случай)
                        if (preferDetailId == null || preferDetailId <= 0)
                            preferDetailId = newIds[0].NewPzvId;
                    }
                }
                else
                {
                    // Если split ничего не вернул — остаёмся на текущей строке
                    preferDetailId = currentRow?.pzvID;
                }

                var refreshedPlan = await _orchestrator.GetPlanByTabAsync(tab, _currentShiftId, _currentKmaId, false, false, 14);
                var ids = new HashSet<int>(refreshedPlan.Select(r => r.pzvID));
                Debug.WriteLine($"Has remainder? {ids.Contains(preferDetailId ?? -1)}");
                _planPresenter.BindGroupDetails(bandedGridView3, advBandedGridView1, _planBindingSource, refreshedPlan ?? new List<KnitterPZVModel>(), clearTabs: false);

                // Принудительно обновляем detail для текущей master-строки:
                // DevExpress кеширует child-list в master-detail, и после ребинда
                // detail может не пересобраться пока не сделать Collapse/Expand.
                int masterHandle = FindMasterHandleBySnap(focusSnap);
                if (masterHandle >= 0)
                {
                    // важно: RefreshData не пересоздаёт detail, но помогает применить новые данные к master
                    bandedGridView3.RefreshData();

                    // Если мастер раскрыт — заставляем пересобрать detail view
                    if (bandedGridView3.GetMasterRowExpanded(masterHandle))
                    {
                        bandedGridView3.RefreshRow(masterHandle);

                        // Иногда detail view уже создан — обновим и его
                        var detail = bandedGridView3.GetDetailView(masterHandle, 0) as GridView;
                        detail?.RefreshData();
                    }
                }

                var rem = refreshedPlan.FirstOrDefault(r => r.pzvID == preferDetailId);
                Debug.WriteLine($"Remainder nrModels = {rem?.nrModels?.Count ?? -1}, rzvModels = {rem?.rzvModels?.Count ?? -1}");
                var fin = refreshedPlan.FirstOrDefault(r => r.pzvID == currentRow.pzvID);
                Debug.WriteLine($"Finished pzvDateEnd = {fin?.pzvDateEnd:dd.MM HH:mm:ss}");

                RestorePlanFocus(focusSnap, preferDetailId);
            }
            else
            {
                // Нет табеля — просто обновим расчётные колонки
                RefreshStatusColumns();
            }

            // Обновляем статус/процент после завершения операции
            RefreshStatusColumns();
        }
        private int FindMasterHandleBySnap(PlanFocusSnap snap)
        {
            if (snap == null) return DevExpress.XtraGrid.GridControl.InvalidRowHandle;

            for (int rh = 0; rh < bandedGridView3.RowCount; rh++)
            {
                if (!bandedGridView3.IsDataRow(rh)) continue;
                if (bandedGridView3.GetRow(rh) is not KnitterPZVModel row) continue;

                if (KnitterPlanUtils.NormalizeTaskNum(row.pzvNomZad) == snap.TaskNum &&
                    KnitterPlanUtils.NormalizeMachineKey(row.kmlNumber) == snap.Machine)
                    return rh;
            }

            return DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }
        /// <summary>
        /// Унифицированный метод для установки даты в колонках "Начато"/"Закончено":
        /// - сохраняет дату на сервере (серверное время)
        /// - применяет дельту к модели и немедленно отражает значение в ячейке без смены фокуса
        /// </summary>
        private async Task ApplyPzvDateAsync(
            GridView view,
            DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn column,
            Func<int, Task<KnitterPZVModel>> updateFunc,
            Func<KnitterPZVModel, DateTime?> getDate,
            Action<KnitterPZVModel, DateTime?> setDate,
            string errorContext)
        {
            GridView _view = view;
            int rowHandle = _view.FocusedRowHandle;
            KnitterPZVModel row = _view.GetRow(rowHandle) as KnitterPZVModel;
            if (rowHandle < 0 || row?.pzvID <= 0)
                return;

            try
            {
                var updated = await updateFunc(row.pzvID);
                var newValue = getDate(updated) ?? getDate(row);
                var oldValue = getDate(row);
                setDate(row, newValue);
                // Также обновляем мастер-коллекцию, чтобы проверки при закрытии смены видели актуальные даты
                var masterRow = _planPresenter?.AllRows?.FirstOrDefault(r => r != null && r.pzvID == row.pzvID);
                if (masterRow != null)
                {
                    setDate(masterRow, newValue);
                }

                // При старте операции фактические значения должны быть пустыми
                if (column.FieldName == "pzvDateStart" && newValue != null && oldValue == null)
                {
                    row.FactKol_UI = 0;
                    row.FactChas_UI = 0m;
                    if (masterRow != null)
                    {
                        masterRow.FactKol_UI = 0;
                        masterRow.FactChas_UI = 0m;
                    }

                    var factKolColumn = _view.Columns.ColumnByFieldName("FactKol_UI");
                    if (factKolColumn != null)
                    {
                        _view.SetRowCellValue(rowHandle, factKolColumn, 0);
                        _view.RefreshRowCell(rowHandle, factKolColumn);
                    }

                    var factChasColumn = _view.Columns.ColumnByFieldName("FactChas_UI");
                    if (factChasColumn != null)
                    {
                        _view.SetRowCellValue(rowHandle, factChasColumn, 0m);
                        _view.RefreshRowCell(rowHandle, factChasColumn);
                    }
                }

                // Если операция завершена (установлена дата окончания), убираем её из списка незавершённых
                if (column.FieldName == "pzvDateEnd" && newValue != null && oldValue == null)
                {
                    _unfinishedOperationIds.Remove(row.pzvID);
                }

                _view.PostEditor();
                _view.SetRowCellValue(rowHandle, column, newValue);
                _view.PostEditor();
                _view.CloseEditor();
                _view.UpdateCurrentRow();
                _view.RefreshRowCell(rowHandle, column);
                _view.RefreshData();

                // Обновляем подсветку строк после изменения даты
                if (column.FieldName == "pzvDateEnd")
                {
                    _view.RefreshRow(rowHandle);
                    // Также обновляем в детализации, если она видна
                    if (advBandedGridView1 != null)
                    {
                        advBandedGridView1.RefreshData();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка при обновлении даты {errorContext}: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private sealed class ExpansionState
        {
            public static ExpansionState Empty { get; } = new ExpansionState(new HashSet<string>(), new HashSet<(string MachineKey, string ArtKey, int? Nom)>());

            public ExpansionState(HashSet<string> machineKeys, HashSet<(string MachineKey, string ArtKey, int? Nom)> artNomKeys)
            {
                MachineKeys = machineKeys ?? new HashSet<string>();
                ArtNomKeys = artNomKeys ?? new HashSet<(string MachineKey, string ArtKey, int? Nom)>();
            }

            public HashSet<string> MachineKeys { get; }
            public HashSet<(string MachineKey, string ArtKey, int? Nom)> ArtNomKeys { get; }
        }
        private static string NormalizeMachineKey(string kmlNumber)
        {
            return string.IsNullOrWhiteSpace(kmlNumber) ? string.Empty : kmlNumber.Trim();
        }

        /// <summary>
        /// Настраивает таймер бездействия и подписывается на события активности пользователя.
        /// </summary>
        private void SetupIdleTimer()
        {
            _idleTimer.Interval = 180000000; // 1 минута = 60000 миллисекунд
            _idleTimer.Tick += IdleTimer_Tick;

            // Подписываемся на события активности для сброса таймера
            this.MouseMove += (s, e) => ResetIdleTimer();
            this.KeyDown += (s, e) => ResetIdleTimer();
            this.MouseClick += (s, e) => ResetIdleTimer();
            this.MouseDown += (s, e) => ResetIdleTimer();
            this.KeyPress += (s, e) => ResetIdleTimer();

            // Подписываемся на события активности после полной загрузки формы
            this.Shown += (s, e) =>
            {
                // Также отслеживаем активность в дочерних контролах
                AttachActivityHandlers(this);
            };

            // Останавливаем таймер при закрытии формы
            this.FormClosing += (s, e) =>
            {
                _idleTimer.Stop();
                _idleTimer.Dispose();
                _shiftTimer.Stop();
                _shiftTimer.Dispose();
            };
        }

        /// <summary>
        /// Рекурсивно подписывается на события активности для всех дочерних контролов.
        /// </summary>
        private void AttachActivityHandlers(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                control.MouseMove += (s, e) => ResetIdleTimer();
                control.MouseClick += (s, e) => ResetIdleTimer();
                control.MouseDown += (s, e) => ResetIdleTimer();
                control.KeyDown += (s, e) => ResetIdleTimer();
                control.KeyPress += (s, e) => ResetIdleTimer();

                // Рекурсивно обрабатываем вложенные контролы
                if (control.HasChildren)
                {
                    AttachActivityHandlers(control);
                }
            }
        }

        /// <summary>
        /// Обработчик таймера бездействия: показывает сплеш выбора сотрудника.
        /// </summary>
        private void IdleTimer_Tick(object sender, EventArgs e)
        {
            // Не показываем сплеш, если он уже открыт
            if (_isSplashShowing)
                return;

            // Останавливаем таймер перед показом сплеша
            _idleTimer.Stop();

            // Показываем сплеш с сохраненным списком ФИО
            if (_cachedFioList != null && _cachedFioList.Count > 0)
            {
                const int defaultTab = 1438;
                PresentFioSelectionSplash(_cachedFioList, defaultTab);
            }
        }

        /// <summary>
        /// Сбрасывает таймер бездействия, перезапуская отсчет с начала.
        /// </summary>
        private void ResetIdleTimer()
        {
            // Не сбрасываем таймер, если сплеш уже открыт
            if (_isSplashShowing)
                return;

            _idleTimer.Stop();
            _idleTimer.Start();
        }

        /// <summary>
        /// Настройка таймера смены.
        /// </summary>
        private void SetupShiftTimer()
        {
            _shiftTimer.Interval = 1000; // 1 секунда
            _shiftTimer.Tick += (s, e) =>
            {
                if (_isShiftRunning && _shiftStartTime.HasValue)
                {
                    var elapsed = DateTime.Now - _shiftStartTime.Value;
                    if (elapsed < TimeSpan.Zero) elapsed = TimeSpan.Zero;
                    simpleLabelItem1.Text = $"Смена: {elapsed:hh\\:mm\\:ss}";
                }
            };
        }
        private async void KnitterWorkSpace_FormClosing(object sender, FormClosingEventArgs e)
        {
            await ShutdownServiceBrokerAsync();
        }

        private async Task ShutdownServiceBrokerAsync()
        {
            if (Interlocked.Exchange(ref _serviceBrokerShutdownStarted, 1) != 0)
                return;

            // 1) Сначала отменяем слушание/лупы
            try { _sbCts?.Cancel(); } catch { }

            // 2) И гарантированно дожидаемся корректной отписки/END CONVERSATION
            try
            {
                if (_sbController != null)
                    await _sbController.DisposeAsync();
            }
            catch
            {
                // лог/игнор — но НЕ даём крашить закрытие формы
            }
            finally
            {
                try { _sbCts?.Dispose(); } catch { }
                _sbCts = null;
            }
        }

        #region adminToggle
        private void InitAdminToggle()
        {
            _adminToggle = new CheckBox
            {
                Text = "Админ режим",
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(this.ClientSize.Width - 130, 5)
            };
            Controls.Add(_adminToggle);
            _adminToggle.BringToFront();
            _adminToggle.CheckedChanged += async (s, e) => await ReloadCurrentTabAsync();
        }

        private void InitExpandNrToggle()
        {
            _expandNrToggle = new CheckBox
            {
                Text = "Все операции",
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(this.ClientSize.Width - 130, 20)
            };
            Controls.Add(_expandNrToggle);
            _expandNrToggle.BringToFront();
            _expandNrToggle.CheckedChanged += async (s, e) => await ReloadCurrentTabAsync();
        }

        private void InitAdminSettingsButton()
        {
            _adminSettingsButton = new Button
            {
                Text = "Админка",
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(this.ClientSize.Width - 220, 5)
            };
            Controls.Add(_adminSettingsButton);
            _adminSettingsButton.BringToFront();
            _adminSettingsButton.Click += (s, e) => ShowAdminSettingsDialog();
        }
        #endregion

        private void SetupStatusColumn()
        {
            // Индикатор в колонке статуса: часы факт / часы назначено
            _statusProgressBar = new RepositoryItemProgressBar
            {
                Minimum = 0,
                Maximum = 100,
                ShowTitle = true,
                PercentView = true
            };

            gridColumn8.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            gridColumn8.UnboundExpression = string.Empty;
            gridColumn8.ColumnEdit = _statusProgressBar;

            bandedGridView3.CustomUnboundColumnData -= BandedGridView3_CustomUnboundColumnData;
            bandedGridView3.CustomUnboundColumnData += BandedGridView3_CustomUnboundColumnData;
        }

        /// <summary>
        /// Настраивает подсветку текущей строки для bandedGridView3 и advBandedGridView1
        /// </summary>
        private void SetupRowStyling()
        {
            try
            {
                if (bandedGridView3 != null)
                {
                    bandedGridView3.RowStyle -= BandedGridView3_RowStyle;
                    bandedGridView3.RowStyle += BandedGridView3_RowStyle;
                }

                if (advBandedGridView1 != null)
                {
                    advBandedGridView1.RowStyle -= AdvBandedGridView1_RowStyle;
                    advBandedGridView1.RowStyle += AdvBandedGridView1_RowStyle;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка при настройке подсветки строк: {ex.Message}");
            }
        }

        /// <summary>
        /// Настраивает увеличенные шрифты для таблиц bandedGridView3 и advBandedGridView1
        /// </summary>
        private void SetupGridFonts()
        {
            try
            {
                // Увеличиваем шрифт на 3 pt (можно изменить на 2-4 pt по необходимости)
                float fontSizeIncrease = 5f;

                if (bandedGridView3 != null)
                {
                    // Отключаем раскраску нечётных/чётных строк
                    bandedGridView3.OptionsView.EnableAppearanceOddRow = false;
                    bandedGridView3.OptionsView.EnableAppearanceEvenRow = false;

                    // Получаем текущий шрифт или используем стандартный
                    Font currentFont = bandedGridView3.Appearance.Row.Font ?? SystemFonts.DefaultFont;
                    Font newFont = new Font(currentFont.FontFamily, currentFont.Size + fontSizeIncrease, currentFont.Style);

                    // Применяем увеличенный шрифт ко всем элементам отображения
                    bandedGridView3.Appearance.Row.Font = newFont;
                    bandedGridView3.Appearance.HeaderPanel.Font = newFont;
                    bandedGridView3.Appearance.FooterPanel.Font = newFont;
                    bandedGridView3.Appearance.GroupPanel.Font = newFont;
                    bandedGridView3.Appearance.GroupRow.Font = newFont;

                    // Применяем к каждой колонке
                    foreach (BandedGridColumn column in bandedGridView3.Columns)
                    {
                        if (column.AppearanceCell != null)
                            column.AppearanceCell.Font = newFont;
                        if (column.AppearanceHeader != null)
                            column.AppearanceHeader.Font = newFont;
                    }
                }

                if (advBandedGridView1 != null)
                {
                    // Отключаем раскраску нечётных/чётных строк
                    advBandedGridView1.OptionsView.EnableAppearanceOddRow = false;
                    advBandedGridView1.OptionsView.EnableAppearanceEvenRow = false;

                    // Получаем текущий шрифт или используем стандартный
                    Font currentFont = advBandedGridView1.Appearance.Row.Font ?? SystemFonts.DefaultFont;
                    Font newFont = new Font(currentFont.FontFamily, currentFont.Size + fontSizeIncrease, currentFont.Style);

                    // Применяем увеличенный шрифт ко всем элементам отображения
                    advBandedGridView1.Appearance.Row.Font = newFont;
                    advBandedGridView1.Appearance.HeaderPanel.Font = newFont;
                    advBandedGridView1.Appearance.FooterPanel.Font = newFont;
                    advBandedGridView1.Appearance.GroupPanel.Font = newFont;
                    advBandedGridView1.Appearance.GroupRow.Font = newFont;

                    // Применяем к каждой колонке
                    foreach (BandedGridColumn column in advBandedGridView1.Columns)
                    {
                        if (column.AppearanceCell != null)
                            column.AppearanceCell.Font = newFont;
                        if (column.AppearanceHeader != null)
                            column.AppearanceHeader.Font = newFont;
                    }

                    int headerGroupRowHeight = GetHeaderGroupRowHeight(currentFont);
                    if (advBandedGridView1.GroupRowHeight < headerGroupRowHeight)
                        advBandedGridView1.GroupRowHeight = headerGroupRowHeight;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка при настройке шрифтов таблиц: {ex.Message}");
            }
        }

        private int GetHeaderGroupRowHeight(Font baseFont)
        {
            float largeSize = baseFont.Size + 2f;
            FontStyle largeStyle = baseFont.Style | FontStyle.Bold;
            using (var largeFont = new Font(baseFont.FontFamily, largeSize, largeStyle))
            {
                return (int)Math.Ceiling(largeFont.GetHeight() + 6f);
            }
        }

        private static bool IsHeaderEmphasisSegment(string segment)
        {
            if (string.IsNullOrWhiteSpace(segment))
                return false;

            string trimmed = segment.TrimStart();
            return trimmed.StartsWith("№пачки:", StringComparison.OrdinalIgnoreCase)
                || trimmed.StartsWith("Размер:", StringComparison.OrdinalIgnoreCase);
        }

        private int GetGroupSummaryLeftEdge(AdvBandedGridView view, DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo info)
        {
            if (view == null || info == null)
                return info?.Bounds.Right ?? 0;

            var viewInfo = view.GetViewInfo() as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo;
            if (viewInfo == null)
                return info.Bounds.Right;

            int left = info.Bounds.Right;
            var planCol = view.Columns.ColumnByFieldName("PlanChas_UI");
            var factCol = view.Columns.ColumnByFieldName("FactChas_UI");
            var columns = new[] { planCol, factCol };
            foreach (var col in columns)
            {
                if (col == null || !col.Visible)
                    continue;

                var colInfo = viewInfo.ColumnsInfo[col];
                if (colInfo != null)
                    left = Math.Min(left, colInfo.Bounds.Left);
            }

            return left;
        }

        private void DrawGroupSummaryValue(
            AdvBandedGridView view,
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo viewInfo,
            int rowHandle,
            GridGroupSummaryItem summaryItem,
            string fieldName,
            DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            if (summaryItem == null || viewInfo == null)
                return;

            var column = view.Columns.ColumnByFieldName(fieldName);
            if (column == null || !column.Visible)
                return;

            var colInfo = viewInfo.ColumnsInfo[column];
            if (colInfo == null)
                return;

            var value = view.GetGroupSummaryValue(rowHandle, summaryItem);
            if (value == null || value == DBNull.Value)
                return;

            string displayFormat = summaryItem.DisplayFormat;
            string text = string.IsNullOrWhiteSpace(displayFormat)
                ? value.ToString()
                : string.Format(System.Globalization.CultureInfo.CurrentCulture, displayFormat, value);

            using (var format = new StringFormat(StringFormatFlags.NoWrap))
            {
                format.Alignment = StringAlignment.Far;
                format.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(text, view.Appearance.GroupRow.Font, e.Appearance.GetForeBrush(e.Cache), colInfo.Bounds, format);
            }
        }

        private void AdvBandedGridView1_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            var view = sender as AdvBandedGridView;
            var info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;
            if (view == null || info == null)
                return;

            string groupText = info.GroupText;
            if (string.IsNullOrWhiteSpace(groupText))
                return;

            string originalText = info.GroupText;
            info.GroupText = string.Empty;
            e.Painter.DrawObject(e.Info);
            info.GroupText = originalText;

            Font baseFont = view.Appearance.GroupRow.Font ?? SystemFonts.DefaultFont;
            float largeSize = baseFont.Size;// + 1f;
            float smallSize = Math.Max(6f, baseFont.Size - 2f);

            FontStyle largeStyle = baseFont.Style | FontStyle.Bold;
            FontStyle smallStyle = baseFont.Style & ~FontStyle.Bold;

            Rectangle textBounds = info.Bounds;
            int left = info.ButtonBounds.Right;// + 3;
            if (left > textBounds.Left)
                textBounds = new Rectangle(left, textBounds.Top, Math.Max(0, textBounds.Right - left), textBounds.Height);

            int summaryLeft = GetGroupSummaryLeftEdge(view, info);
            if (summaryLeft > textBounds.Left)
            {
                int width = Math.Max(0, summaryLeft - textBounds.Left - 4);
                textBounds = new Rectangle(textBounds.Left, textBounds.Top, width, textBounds.Height);
            }

            using (var largeFont = new Font(baseFont.FontFamily, largeSize, largeStyle))
            using (var smallFont = new Font(baseFont.FontFamily, smallSize, smallStyle))
            using (var format = new StringFormat(StringFormatFlags.NoWrap))
            {
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = StringAlignment.Center;

                using (var shadeBrush = new SolidBrush(Color.FromArgb(24, Color.Red)))
                {
                    e.Graphics.FillRectangle(shadeBrush, textBounds);
                }

                float x = textBounds.Left;
                string[] parts = groupText.Split(new[] { " | " }, StringSplitOptions.None);
                var tokens = new List<(string Text, Font Font, float Width)>(parts.Length * 2);
                for (int i = 0; i < parts.Length; i++)
                {
                    string part = parts[i];
                    Font font = IsHeaderEmphasisSegment(part) ? largeFont : smallFont;
                    float width = e.Cache.CalcTextSize(part, font).Width;
                    tokens.Add((part, font, width));

                    if (i < parts.Length - 1)
                    {
                        const string separator = " | ";
                        float sepWidth = e.Cache.CalcTextSize(separator, smallFont).Width;
                        tokens.Add((separator, smallFont, sepWidth));
                    }
                }

                foreach (var token in tokens)
                {
                    var tokenBounds = new RectangleF(x, textBounds.Top, token.Width, textBounds.Height);
                    e.Graphics.DrawString(token.Text, token.Font, e.Appearance.GetForeBrush(e.Cache), tokenBounds, format);
                    x += token.Width;
                }
            }

            var viewInfo = view.GetViewInfo() as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo;
            if (viewInfo != null)
            {
                DrawGroupSummaryValue(view, viewInfo, e.RowHandle, _planChasGroupSummaryItem, "PlanChas_UI", e);
                DrawGroupSummaryValue(view, viewInfo, e.RowHandle, _factChasGroupSummaryItem, "FactChas_UI", e);
            }

            e.Handled = true;
        }

        /// <summary>
        /// Обновляет список незавершённых операций на основе текущих данных
        /// </summary>
        private void UpdateUnfinishedOperationsList()
        {
            try
            {
                var rows = _planPresenter.AllRows?.Where(r => r != null && r.pzvID > 0).ToList() ?? new List<KnitterPZVModel>();
                // Находим незавершённые операции: начатые, но не завершённые
                var unfinished = rows.Where(r => r.pzvDateStart != null && r.pzvDateEnd == null).ToList();
                _unfinishedOperationIds = new HashSet<int>(unfinished.Where(r => r.pzvID > 0).Select(r => r.pzvID));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка при обновлении списка незавершённых операций: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик стиля строк для bandedGridView3 - подсветка фокусной строки и незавершённых операций
        /// </summary>
        private void BandedGridView3_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                var view = sender as BandedGridView;
                if (view == null) return;
                if (e.RowHandle < 0) return;

                var row = view.GetRow(e.RowHandle) as KnitterPZVModel;

                // Подсветка незавершённых операций красным цветом (приоритет над фокусной строкой)
                if (row != null && _unfinishedOperationIds.Contains(row.pzvID))
                {
                    e.Appearance.BackColor = Color.LightCoral;
                    e.Appearance.BackColor2 = Color.LightCoral;
                    e.HighPriority = true;
                    return;
                }

                // Подсветка фокусной строки (только если не незавершённая)
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    //                  e.Appearance.BackColor = Color.Coral;
                    //                  e.Appearance.BackColor2 = Color.Coral;
                    e.HighPriority = true;
                    return;
                }
            }
            catch { }
        }

        /// <summary>
        /// Обработчик стиля строк для advBandedGridView1 - подсветка фокусной строки и незавершённых операций
        /// </summary>
        private void AdvBandedGridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                var view = sender as AdvBandedGridView;
                if (view == null) return;
                if (e.RowHandle < 0) return;

                // Не применяем стили к группам
                if (view.IsGroupRow(e.RowHandle))
                    return;

                var row = view.GetRow(e.RowHandle) as KnitterPZVModel;

                // Подсветка незавершённых операций красным цветом (приоритет над фокусной строкой)
                if (row != null && _unfinishedOperationIds.Contains(row.pzvID))
                {
                    e.Appearance.BackColor = Color.LightCoral;
                    e.Appearance.BackColor2 = Color.LightCoral;
                    e.HighPriority = true;
                    return;
                }

                // Подсветка фокусной строки (только если не незавершённая)
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.Bisque;
                    e.Appearance.BackColor2 = Color.BlanchedAlmond;
                    e.HighPriority = true;
                    return;
                }
            }
            catch { }
        }

        private void BandedGridView3_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column != gridColumn8 || !e.IsGetData)
                return;

            // По умолчанию показываем 0%
            e.Value = 0m;

            if (e.Row is KnitterPZVModel row)
            {
                // Считаем статус по суммам часов из БД:
                // bandedGridColumn26 (pzvChasNazn) и bandedGridColumn27 (pzvNChasi)
                var machineKey = NormalizeMachineKey(row.kmlNumber);
                var taskKey = KnitterPlanUtils.NormalizeTaskNum(row.pzvNomZad);
                var rows = _planPresenter.AllRows?
                    .Where(r =>
                        r != null &&
                        string.Equals(NormalizeMachineKey(r.kmlNumber), machineKey, StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(KnitterPlanUtils.NormalizeTaskNum(r.pzvNomZad), taskKey, StringComparison.OrdinalIgnoreCase))
                    .ToList() ?? new List<KnitterPZVModel>();

                decimal assignedHours = rows.Sum(r => r.PlanChas_UI ?? 0m);
                decimal doneHours = rows.Sum(r => r.FactChas_UI ?? 0m);

                decimal percent =
                    assignedHours > 0m
                        ? Math.Round(doneHours * 100m / assignedHours, 1)
                        : 0m;
                e.Value = percent;
            }
        }

        private async Task ReloadCurrentTabAsync()
        {
            if (int.TryParse(FioGridLookUpEdit.EditValue?.ToString(), out int tab) && tab > 0)
            {
                await LoadPlanForTabAsync(tab, forceReload: true);
            }
        }

        /// <summary>
        /// Запрещает редактирование строк, назначенных на другой табельный номер.
        /// </summary>
        private void GridView_PreventForeignEdit(object sender, CancelEventArgs e)
        {
            if (sender is not ColumnView view)
                return;

            if (view is DevExpress.XtraGrid.Views.Base.ColumnView columnView)
            {
                // Групповые строки не редактируются
                if (view is GridView grid && grid.IsGroupRow(grid.FocusedRowHandle))
                {
                    e.Cancel = true;
                    return;
                }

                if (columnView.GetFocusedRow() is not KnitterPZVModel row)
                {
                    e.Cancel = true;
                    return;
                }

                if (_currentLoadedTab.HasValue && row.pzvTab.HasValue && row.pzvTab.Value != _currentLoadedTab.Value)
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Фильтрует список ФИО: если в зоне есть открытая смена, показывает только сотрудника(ов) с этой сменой; иначе — всех в зоне.
        /// </summary>
        private async Task<List<FioModel>> FilterFioByOpenShiftAsync(List<FioModel> fioList)
        {
            if (fioList == null || fioList.Count == 0)
                return fioList ?? new List<FioModel>();

            var result = new List<FioModel>();

            // Группируем по зоне; пустая зона считается отдельной группой
            foreach (var group in fioList.GroupBy(f => f.Zone ?? string.Empty))
            {
                var openTabs = new List<FioModel>();
                foreach (var fio in group)
                {
                    var open = await _orchestrator.GetOpenShiftByTabAsync(fio.Tab);
                    if (open.shiftId.HasValue)
                    {
                        openTabs.Add(fio);
                    }
                }

                if (openTabs.Any())
                {
                    // Если найдены открытые смены, показываем только их в данной зоне
                    result.AddRange(openTabs);
                }
                else
                {
                    // Иначе показываем всех сотрудников зоны
                    result.AddRange(group);
                }
            }

            return result;
        }

        /// <summary>
        /// Централизованно применяет состояние смены к UI и поведению гридов.
        /// </summary>
        private void ApplyShiftUi(bool isRunning, int? shiftId, DateTime? shiftStart)
        {
            _isShiftRunning = isRunning;
            _currentShiftId = shiftId;
            _shiftStartTime = shiftStart;

            if (isRunning && shiftStart.HasValue)
            {
                simpleButton2.Text = "Закончить смену";
                var elapsed = DateTime.Now - shiftStart.Value;
                if (elapsed < TimeSpan.Zero) elapsed = TimeSpan.Zero;
                simpleLabelItem1.Text = $"Смена: {elapsed:hh\\:mm\\:ss}";
                _shiftTimer.Start();
            }
            else
            {
                _shiftTimer.Stop();
                simpleButton2.Text = "Начать смену";
                simpleLabelItem1.Text = " ";
            }

            ApplyShiftEditMode(isRunning);
        }

        /// <summary>
        /// Переключает режим редактирования гридов: только просмотр, если смена не начата.
        /// </summary>
        private void ApplyShiftEditMode(bool isRunning)
        {
            bool editable = isRunning;

            void SetViewState(ColumnView view)
            {
                if (view == null)
                    return;

                view.OptionsBehavior.Editable = editable;
                view.OptionsBehavior.ReadOnly = !editable;

                foreach (GridColumn col in view.Columns)
                {
                    col.OptionsColumn.AllowEdit = editable;
                    col.OptionsColumn.AllowFocus = editable;
                }
            }

            SetViewState(bandedGridView3);
            SetViewState(advBandedGridView1);
        }
        private sealed class PlanFocusSnap
        {
            public string TaskNum;
            public string Machine;
            public int? DetailPzvId;
            public int MasterTop;
            public int? DetailTop;
            public bool WasInDetail;
        }

        private PlanFocusSnap CapturePlanFocus()
        {
            var snap = new PlanFocusSnap
            {
                MasterTop = bandedGridView3.TopRowIndex
            };

            var fv = PlanZagrVyazGridControl.FocusedView as DevExpress.XtraGrid.Views.Grid.GridView;
            snap.WasInDetail = fv != null && fv != bandedGridView3;

            if (bandedGridView3.GetFocusedRow() is KnitterPZVModel m)
            {
                snap.TaskNum = KnitterPlanUtils.NormalizeTaskNum(m.pzvNomZad);
                snap.Machine = KnitterPlanUtils.NormalizeMachineKey(m.kmlNumber);
            }

            if (snap.WasInDetail && fv?.GetFocusedRow() is KnitterPZVModel d && d.pzvID > 0)
            {
                snap.DetailPzvId = d.pzvID;
                snap.DetailTop = fv.TopRowIndex;
            }

            return snap;
        }

        /// <summary>
        /// Строит снапшот фокуса по строке и текущему виду (без чтения FocusedRow после RefreshData).
        /// Используется при завершении операции до вызова ApplyPzvDateAsync/RefreshData, чтобы не терять машину при факт &lt; назн.
        /// </summary>
        private PlanFocusSnap CapturePlanFocusFromRow(KnitterPZVModel? currentRow, GridView detailView)
        {
            var snap = new PlanFocusSnap
            {
                MasterTop = bandedGridView3?.TopRowIndex ?? 0,
                WasInDetail = detailView != null && detailView != bandedGridView3
            };

            if (currentRow != null)
            {
                snap.TaskNum = KnitterPlanUtils.NormalizeTaskNum(currentRow.pzvNomZad);
                snap.Machine = KnitterPlanUtils.NormalizeMachineKey(currentRow.kmlNumber);
                if (currentRow.pzvID > 0)
                {
                    snap.DetailPzvId = currentRow.pzvID;
                    if (detailView != null)
                        snap.DetailTop = detailView.TopRowIndex;
                }
            }

            return snap;
        }

        private void RestorePlanFocus(PlanFocusSnap snap, int? preferDetailId = null)
        {
            if (snap == null) return;

            var masterView = bandedGridView3;
            if (masterView == null) return;

            // 1) Найти мастер-строку по ключу (по всем data rows, не только видимым)
            int masterHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle;

            for (int rh = 0; rh < masterView.RowCount; rh++)
            {
                if (!masterView.IsDataRow(rh)) continue;

                if (masterView.GetRow(rh) is not KnitterPZVModel row) continue;

                if (KnitterPlanUtils.NormalizeTaskNum(row.pzvNomZad) == snap.TaskNum &&
                    KnitterPlanUtils.NormalizeMachineKey(row.kmlNumber) == snap.Machine)
                {
                    masterHandle = rh;
                    break;
                }
            }

            if (masterHandle < 0) return;

            // 2) Фокус и видимость мастера
            masterView.FocusedRowHandle = masterHandle;
            masterView.MakeRowVisible(masterHandle, true);

            // 3) Целевая detail-строка
            int? targetDetailId = preferDetailId ?? snap.DetailPzvId;
            if (!targetDetailId.HasValue || targetDetailId.Value <= 0) return;

            // 4) Раскрыть master-detail
            if (!masterView.GetMasterRowExpanded(masterHandle))
                masterView.ExpandMasterRow(masterHandle);

            // 5) Detail view создаётся лениво → берём на следующем UI-такте
            BeginInvoke(new Action(() =>
            {
                var detail = masterView.GetDetailView(masterHandle, 0) as DevExpress.XtraGrid.Views.Grid.GridView;
                if (detail == null) return;

                int drh = detail.LocateByValue("pzvID", targetDetailId.Value);
                if (drh < 0) return;

                detail.FocusedRowHandle = drh;
                detail.MakeRowVisible(drh, true);
            }));
        }

        private async Task LoadPlanForTabAsync(int tab, bool forceReload = false)
        {
            if (!forceReload && _currentLoadedTab.HasValue && _currentLoadedTab.Value == tab)
                return;

            TabGridLookUpEdit.EditValue = tab;

            await UpdateZoneAsync(tab);
            await UpdateShiftStateAsync(tab);

            bool isAdmin = _adminToggle?.Checked == true;
            bool isShiftOpen = _isShiftRunning && _currentShiftId.HasValue;

            // Режимы выборки:
            // - закрытая смена: только неназначенные, ограничение 14ч
            // - открытая смена: назначенные на текущую смену, без лимита по часам
            // - админ: includeFinished=true (видит завершённые)
            int? kwsId = isShiftOpen ? _currentShiftId : 0;
            bool onlyUnassigned = !isShiftOpen && !_showAllAssignedWhenClosed;
            decimal maxHours = isShiftOpen ? 240m : _maxHoursClosedShift;
            bool expandByNr = _expandNrToggle?.Checked == true;

            // Сохраняем фокус до перезагрузки (обновление по Service Broker иначе сбрасывает фокус).
            // Снимок делаем до await — после await продолжение может выполниться не на UI-потоке.
            var focusSnap = CapturePlanFocus();

            var plan = await _orchestrator.GetPlanByTabAsync(tab, kwsId, _currentKmaId, onlyUnassigned, expandByNr, maxHours, includeFinished: isAdmin);

            // Привязка и восстановление фокуса — только в UI-потоке (после await контекст мог смениться)
            void ApplyPlanAndRestoreFocus()
            {
                if (IsDisposed || bandedGridView3 == null) return;
                if (plan != null)
                {
                    foreach (var row in plan)
                    {
                        EnsureFactHours(row);
                    }
                }
                _planPresenter.BindGroupDetails(bandedGridView3, advBandedGridView1, _planBindingSource, plan ?? new List<KnitterPZVModel>(), clearTabs: false);
                RefreshFooterSummaries();
                _currentLoadedTab = tab;
                RestorePlanFocus(focusSnap);
            }

            if (InvokeRequired)
                Invoke(new Action(ApplyPlanAndRestoreFocus));
            else
                ApplyPlanAndRestoreFocus();
        }

        private async Task UpdateZoneAsync(int tab)
        {
            try
            {
                var zone = await _orchestrator.GetZoneByTabAsync(tab);
                _currentKmaId = zone.kmaId;
                _currentKmaNum = zone.kmaNum;
                textEdit1.Text = _currentKmaNum?.ToString() ?? string.Empty;
            }
            catch (Exception)
            {
                textEdit1.Text = string.Empty;
            }
        }

        private async Task UpdateShiftStateAsync(int tab)
        {
            try
            {
                var open = await _orchestrator.GetOpenShiftByTabAsync(tab);
                if (open.shiftId.HasValue && open.dateStart.HasValue)
                {
                    ApplyShiftUi(true, open.shiftId.Value, open.dateStart);
                }
                else
                {
                    ApplyShiftUi(false, null, null);
                }
            }
            catch (Exception)
            {
                ApplyShiftUi(false, null, null);
            }
        }


        /// <summary>
        /// Перезапускает загрузку данных по имени объекта (вызывается координатором).
        /// </summary>
        public async Task RestartDataByObjectNameAsync(string objectName, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(objectName))
                return;

            try
            {
                await InvokeOnUiAsync(async () =>
                {
                    System.Diagnostics.Debug.WriteLine($"[KnitterWorkSpace] RestartDataByObjectNameAsync: {objectName}");

                    // Если это наша хранимая процедура плана - перезагружаем план
                    if (string.Equals(objectName, "GetPlanZagrVyazNorm_ByTab3", StringComparison.OrdinalIgnoreCase))
                    {
                        if (_currentLoadedTab.HasValue)
                        {
                            System.Diagnostics.Debug.WriteLine($"[KnitterWorkSpace] Reloading plan for tab {_currentLoadedTab.Value}");
                            await LoadPlanForTabAsync(_currentLoadedTab.Value, forceReload: true);
                            System.Diagnostics.Debug.WriteLine($"[KnitterWorkSpace] Plan reloaded successfully");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"[KnitterWorkSpace] No current tab loaded, skipping reload");
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[KnitterWorkSpace] Error in RestartDataByObjectNameAsync for {objectName}: {ex}");
                throw;
            }
        }

        private Task InvokeOnUiAsync(Func<Task> fn)
        {
            if (this.InvokeRequired)
            {
                var tcs = new TaskCompletionSource<object?>();
                this.BeginInvoke(new Action(async () =>
                {
                    try { await fn(); tcs.TrySetResult(null); }
                    catch (Exception ex) { tcs.TrySetException(ex); }
                }));
                return tcs.Task;
            }

            return fn();
        }

        /// <summary>
        /// Загружает информацию о таблицах и полях для указанного объекта из БД.
        /// </summary>
        public async Task<List<ServiceBrokerModel.TableListenInfo>> LoadListenInfoByObjectNameAsync(
            string objectName, CancellationToken ct)
        {
            if (_sbService == null)
            {
                var dbHelper = new DatabaseHelper();
                _sbService = new ServiceBrokerService(dbHelper);
            }

            var list = await _sbService.GetObjectListForServiceBroker(objectName, ct);
            System.Diagnostics.Debug.WriteLine($"[KnitterWorkSpace] LoadListenInfoByObjectNameAsync: object={objectName}, rows={list?.Count ?? 0}");
            return list;
        }

        /// <summary>
        /// Переопределяем инициализацию для установки UseSchemaInListenName = true.
        /// </summary>
        public async Task InitServiceBrokerAsync(CancellationToken ct)
        {
            System.Diagnostics.Debug.WriteLine($"[KnitterWorkSpace] InitServiceBrokerAsync start: objects={string.Join(", ", ServiceBrokerObjects)}");
            await _sbController.InitAsync(ct);

            var tables = _sbController.Helper?.GetListeningTables() ?? Array.Empty<string>();
            System.Diagnostics.Debug.WriteLine($"[KnitterWorkSpace] Listening tables: {string.Join(", ", tables)}");
        }

        /// <summary>
        /// Обновление данных формы по уведомлению брокера (как в PlanZagrVyaz)
        /// </summary>
        public async Task UpdateDataInFormAsync(string tableName, string? fieldsChangedCsv)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"[KnitterWorkSpace] UpdateDataInFormAsync: table={tableName}, fields={fieldsChangedCsv}");

                await _sbController.HandleUpdateAsync(tableName, fieldsChangedCsv ?? string.Empty);

                if (_sbController.Coordinator != null)
                {
                    var stats = _sbController.Coordinator.GetStatistics();
                    System.Diagnostics.Debug.WriteLine($"[KnitterWorkSpace] RefreshCoordinator stats: Pending={stats.PendingCount}, InFlight={stats.InFlightCount}, TotalRequests={stats.TotalRequests}, TotalExecutions={stats.TotalExecutions}, CascadePreventions={stats.CascadePreventions}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[KnitterWorkSpace] Error in UpdateDataInFormAsync: {ex.Message}");
            }
        }

        public Task UpdateDataInFormAsync(string tableName)
            => UpdateDataInFormAsync(tableName, fieldsChangedCsv: string.Empty);

        private async void layoutControlGroup1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (sender is LayoutControlGroup group && e.Button is GroupBoxButton button)
            {
                string tag = button.Tag?.ToString() ?? string.Empty;

                switch (tag)
                {
                    case "refresh":
                        await ReloadCurrentTabAsync();
                        break;

                    case "collapse-to-vm":
                        // Свернуть всё: скрыть все детальные уровни (master-detail)
                        CollapseAllMasterDetail();
                        break;

                    case "collapse-to-pachka":
                        // Свернуть до пачки: машины раскрыты, внутри детального грида свёрнуты группы по __Header
                        CollapseToPachkaLevel();
                        break;

                    case "expand-all":
                    case "toggle-expand":
                        // Показать всё: раскрыть все машины и все группы в детальном гриде
                        ExpandAllMasterDetail();
                        break;

                    default:
                        // Если тег не установлен, пытаемся определить по Caption
                        string caption = button.Caption ?? string.Empty;
                        if (caption.Contains("Обновить", StringComparison.OrdinalIgnoreCase))
                        {
                            await ReloadCurrentTabAsync();
                        }
                        if (caption.Contains("Свернуть всё", StringComparison.OrdinalIgnoreCase))
                        {
                            CollapseAllMasterDetail();
                        }
                        break;
                }
            }
        }

        #region Header Buttons

        /// <summary>
        /// Инициализирует теги для кнопок в заголовке групп
        /// </summary>
        private void InitHeaderButtonTags()
        {
            // layoutControlGroup1 — основная группа с гридом
            TagByCaption(layoutControlGroup1, new (string caption, string tag)[] {
                ("Обновить", "refresh"),
                ("Свернуть до ВМ", "collapse-to-vm"),
                ("Свернуть до пачки", "collapse-to-pachka"),
                ("Показать всё совсем", "expand-all"),
                ("Показать всё", "toggle-expand"),
            });

        }

        /// <summary>
        /// Проставляет теги кнопкам по их подписям
        /// </summary>
        private void TagByCaption(LayoutControlGroup group, IEnumerable<(string caption, string tag)> map)
        {
            if (group == null || group.CustomHeaderButtons == null) return;

            foreach (var (caption, tag) in map)
            {
                var btn = group.CustomHeaderButtons
                               .OfType<GroupBoxButton>()
                               .FirstOrDefault(b => string.Equals(b.Caption, caption, StringComparison.OrdinalIgnoreCase));
                if (btn != null && (btn.Tag == null || string.IsNullOrWhiteSpace(btn.Tag.ToString())))
                {
                    btn.Tag = tag;
                }
            }
        }

        /// <summary>
        /// Свернуть всё: скрыть все детальные уровни master-detail (остаётся только верхний уровень машин).
        /// </summary>
        private void CollapseAllMasterDetail()
        {
            if (bandedGridView3 == null)
                return;

            bandedGridView3.BeginUpdate();
            try
            {
                for (int rh = 0; rh < bandedGridView3.RowCount; rh++)
                {
                    if (bandedGridView3.IsMasterRow(rh))
                    {
                        bandedGridView3.SetMasterRowExpanded(rh, false);
                    }
                }
            }
            finally
            {
                bandedGridView3.EndUpdate();
            }
        }

        /// <summary>
        /// Свернуть до пачки: все машины раскрыты, внутри детального грида свернуты группы по __Header.
        /// </summary>
        private void CollapseToPachkaLevel()
        {
            if (bandedGridView3 == null)
                return;

            bandedGridView3.BeginUpdate();
            try
            {
                for (int rh = 0; rh < bandedGridView3.RowCount; rh++)
                {
                    if (!bandedGridView3.IsDataRow(rh))
                        continue;

                    if (bandedGridView3.IsMasterRow(rh))
                    {
                        bandedGridView3.SetMasterRowExpanded(rh, true);
                    }

                    var detail = bandedGridView3.GetDetailView(rh, 0) as DevExpress.XtraGrid.Views.Grid.GridView;
                    if (detail == null)
                        continue;

                    detail.BeginUpdate();
                    try
                    {
                        // Группы по __Header находятся на уровне 0 — CollapseAllGroups сворачивает их,
                        // оставляя видимыми строки-группы (пачки) с итогами.
                        detail.CollapseAllGroups();
                    }
                    finally
                    {
                        detail.EndUpdate();
                    }
                }
            }
            finally
            {
                bandedGridView3.EndUpdate();
            }
        }

        /// <summary>
        /// Показать всё: раскрыть все master-строки и все группы в детальном гриде.
        /// </summary>
        private void ExpandAllMasterDetail()
        {
            if (bandedGridView3 == null)
                return;

            bandedGridView3.BeginUpdate();
            try
            {
                for (int rh = 0; rh < bandedGridView3.RowCount; rh++)
                {
                    if (!bandedGridView3.IsDataRow(rh))
                        continue;

                    if (bandedGridView3.IsMasterRow(rh))
                    {
                        bandedGridView3.ExpandAllGroups();//.SetMasterRowExpanded(rh, true);
                    }

                    var detail = bandedGridView3.GetDetailView(rh, 0) as DevExpress.XtraGrid.Views.Grid.GridView;
                    if (detail == null)
                        continue;

                    detail.BeginUpdate();
                    try
                    {
                        detail.ExpandAllGroups();
                    }
                    finally
                    {
                        detail.EndUpdate();
                    }
                }
            }
            finally
            {
                bandedGridView3.EndUpdate();
            }
        }


        #endregion
    }
}
    internal class PlanFocusSnap
    {
        public string TaskNum;
        public string Machine;
        public int? DetailPzvId;
        public int MasterTop;
        public int? DetailTop;
        public bool WasInDetail;
}


    


