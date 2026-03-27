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
using SewingProduction.Core;
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
using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Label = System.Windows.Forms.Label;

#nullable enable
namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class KnitterWorkSpace : CustomForm, IServiceBrokerHost
    {
        private readonly ServiceBrokerController _sbController;
        private readonly IAppServiceBrokerHub _sbHub;
        private readonly string _sbHubOwnerId = $"KnitterWorkSpace:{Guid.NewGuid():N}";
        private readonly HashSet<string> _ignoredServiceBrokerTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// Оркестратор доменной логики: загрузка данных, сохранение дат и прочие операции.
        /// </summary>
        private readonly IKnitterOrchestrator _orchestrator;
        private readonly IKnitterWorkSpaceService _workSpaceService;
        private readonly ILogger _logger = new FileLogger();
        private const string LoggerContext = "KnitterWorkSpace";
        private static readonly TimeSpan PlanBrokerSelfMute = TimeSpan.FromSeconds(2);

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
        private void InitServiceBrokerIgnoredTables()
        {
            _ignoredServiceBrokerTables.UnionWith(new[]
            {
                // GetPlanZagrVyazNorm_ByTab3 traverses broad reference/view dependencies.
                // Keep broker focused on operational plan/shift data and ignore static/reference sources.
                "dbo.fio",
                "dbo.gr_rab_dn",
                "dbo.knitMachineArea",
                "dbo.knitMachineAreaEmp",
                "dbo.knitMachineList",
                "dbo.matrix_class",
                "dbo.norm_rasz",
                "dbo.owenDeviceParam",
                "dbo.plan_sezon_zad",
                "dbo.podr_vyaz",
                "dbo.proizv_modify_zc_history",
                "dbo.raskr_zeh_vyaz",
                "dbo.spOborudShv",
                "dbo.tab_n",
                "dbo.tabel_sp",
                "dbo.v_nazn_akt"
            });
        }
        /// <summary>
        /// Источник данных, к которому привязан GridControl.
        /// </summary>
        private readonly BindingSource _planBindingSource = new BindingSource();
        /// <summary>
        /// Презентер, который собирает иерархию мастер-деталь и настраивает события.
        /// </summary>
        private readonly KnitterPlanPresenter _planPresenter = new KnitterPlanPresenter();
        private readonly KnitterPlanFocusService _planFocusService;
        private readonly KnitterGridVisualService _gridVisualService;
        /// <summary>
        /// Список ID незавершённых операций (pzvID) для подсветки красным цветом
        /// </summary>
        private HashSet<int> _unfinishedOperationIds = new HashSet<int>();
        private CheckBox _adminToggle;
        private CheckBox _expandNrToggle;
        private RepositoryItemProgressBar? _statusProgressBar;
        private DevExpress.XtraGrid.GridGroupSummaryItem _pzvChasNaznGroupSumItem;

        // Вью для третьего уровня (деталь детальной таблицы)
        private RepositoryItemButtonEdit _pzvDateStartButtonEdit;
        private RepositoryItemTextEdit _pzvDateStartTextEdit;
        private RepositoryItemButtonEdit _pzvDateEndButtonEdit;
        private RepositoryItemTextEdit _pzvDateEndTextEdit;

        /// <summary>
        /// Таймер для отслеживания бездействия пользователя (1 минута).
        /// </summary>
        /// <summary>
        /// Таймер смены (идёт с момента нажатия 'Начать смену' до 'Закончить смену').
        /// </summary>
        private readonly System.Windows.Forms.Timer _shiftTimer = new System.Windows.Forms.Timer();
        private bool _isGroupRowCellHandlerAttached;
        private GridGroupSummaryItem _planChasGroupSummaryItem;
        private GridGroupSummaryItem _factChasGroupSummaryItem;
        private Color _planFooterColor = Color.LightCoral;
        private Color _factFooterColor = Color.LightSkyBlue;
        private readonly KnitterBlinkController _blinkController;
        private readonly KnitterIdleSplashController _idleSplashController;
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
        private void LogSuccess(string message, string scope)
        {
            _ = SafeLogAsync(() => _logger.LogEventAsync(message, $"{LoggerContext}.{scope}"));
        }

        private void LogWarning(string message, string scope)
        {
            _ = SafeLogAsync(() => _logger.LogWarningAsync(message, $"{LoggerContext}.{scope}"));
        }

        private void LogError(Exception ex, string scope)
        {
            _ = SafeLogAsync(() => _logger.LogErrorAsync(ex, $"{LoggerContext}.{scope}"));
        }

        private static async Task SafeLogAsync(Func<Task> writeLog)
        {
            try
            {
                await writeLog().ConfigureAwait(false);
            }
            catch
            {
                // Логгер не должен ломать бизнес-поток формы.
            }
        }

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
                _sbController = new ServiceBrokerController(this);
                _sbHub = AppServices.Services.GetRequiredService<IAppServiceBrokerHub>();
                dataLayoutControl1.DataSource = _planBindingSource;

                var dbHelper = new DatabaseHelper();
                IKnitterRepository repo = new KnitterRepository(dbHelper);
                _orchestrator = new KnitterOrchestrator(repo);
                _workSpaceService = new KnitterWorkSpaceService(repo, _logger);
                InitServiceBrokerIgnoredTables();
                _planFocusService = new KnitterPlanFocusService(this, PlanZagrVyazGridControl, bandedGridView3);
                _gridVisualService = new KnitterGridVisualService(
                    bandedGridView3,
                    advBandedGridView1,
                    gridColumn8,
                    () => _planPresenter.AllRows ?? Array.Empty<KnitterPZVModel>(),
                    LogError);
                _blinkController = new KnitterBlinkController(simpleButton2);
                _idleSplashController = new KnitterIdleSplashController(
                    this,
                    this,
                    dataLayoutControl1,
                    () => int.TryParse(FioGridLookUpEdit.EditValue?.ToString(), out int tab) ? tab : (int?)null,
                    tab =>
                    {
                        FioGridLookUpEdit.EditValue = tab;
                        TabGridLookUpEdit.EditValue = tab;
                    },
                    Close,
                    LogWarning);
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
                _gridVisualService.Initialize();
                SetupRowStyling();
                bandedGridView3.ShowingEditor += GridView_PreventForeignEdit;
                advBandedGridView1.ShowingEditor += GridView_PreventForeignEdit;
                bandedGridView3.CustomColumnDisplayText += BandedGridView3_CustomColumnDisplayText;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка инициализации формы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogError(ex, "Ctor.UserClass");
            }
        }

        /// <summary>
        /// Вариант конструктора с внедрением зависимостей (DI).
        /// </summary>
        /// <param name="orchestrator">Оркестратор доменной логики.</param>
        public KnitterWorkSpace(IKnitterOrchestrator orchestrator, IKnitterShiftGateway shiftWorkflowGateway)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("[KnitterWorkSpace] ctor(IKnitterOrchestrator, IKnitterShiftGateway) start");
                InitializeComponent();
                _sbController = new ServiceBrokerController(this);
                _sbHub = AppServices.Services.GetRequiredService<IAppServiceBrokerHub>();
                _orchestrator = orchestrator ?? throw new ArgumentNullException(nameof(orchestrator));
                _workSpaceService = new KnitterWorkSpaceService(shiftWorkflowGateway ?? throw new ArgumentNullException(nameof(shiftWorkflowGateway)), _logger);
                InitServiceBrokerIgnoredTables();
                _planFocusService = new KnitterPlanFocusService(this, PlanZagrVyazGridControl, bandedGridView3);
                _gridVisualService = new KnitterGridVisualService(
                    bandedGridView3,
                    advBandedGridView1,
                    gridColumn8,
                    () => _planPresenter.AllRows ?? Array.Empty<KnitterPZVModel>(),
                    LogError);
                _blinkController = new KnitterBlinkController(simpleButton2);
                _idleSplashController = new KnitterIdleSplashController(
                    this,
                    this,
                    dataLayoutControl1,
                    () => int.TryParse(FioGridLookUpEdit.EditValue?.ToString(), out int tab) ? tab : (int?)null,
                    tab =>
                    {
                        FioGridLookUpEdit.EditValue = tab;
                        TabGridLookUpEdit.EditValue = tab;
                    },
                    Close,
                    LogWarning);
                dataLayoutControl1.DataSource = _planBindingSource;
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
                _gridVisualService.Initialize();
                SetupBlinkTimers();
                SetupRowStyling();
                bandedGridView3.ShowingEditor += GridView_PreventForeignEdit;
                advBandedGridView1.ShowingEditor += GridView_PreventForeignEdit;
                bandedGridView3.CustomColumnDisplayText += BandedGridView3_CustomColumnDisplayText;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка инициализации формы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogError(ex, "Ctor.OrchestratorAndShiftWorkflowGateway");
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
                _idleSplashController.UpdateFioList(fioList);
                _idleSplashController.ShowSelectionSplash(defaultTab);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка загрузки списка сотрудников: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogError(ex, "InitializeAsync");
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
                _idleSplashController.Reset();

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
                LogError(ex, "FioGridLookUpEdit_EditValueChanged.Sql");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка загрузки плана: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogError(ex, "FioGridLookUpEdit_EditValueChanged");
            }
        }

        /// <summary>
        /// Кнопка "Начать смену": массово назначает табель выбранным строкам, присваивает kolNazn, ChasiNazn, sekNazn и обновляет отображение
        /// </summary>
        private async void simpleButton2_Click(object sender, EventArgs e)
        {

            string logContext = _isShiftRunning ? "CloseShift" : "StartShift";
            simpleButton2.Enabled = false;
            try
            {
                if (_isShiftRunning)
                {
                    if (!ShowShiftEndConfirmationDialog())
                        return;

                    if (!int.TryParse(FioGridLookUpEdit.EditValue?.ToString(), out int tabEnd) || !_currentShiftId.HasValue)
                    {
                        LogWarning("Не удалось определить данные для завершения смены",logContext);
                        return;
                    }

                    var res = await _workSpaceService.CloseShiftAsync(new CloseShiftCommand
                    {
                        ShiftId = _currentShiftId.Value,
                        TabEnd = tabEnd,
                        MinHours = 12m,
                        CurrentRows = _planPresenter.AllRows?.ToList() ?? new List<KnitterPZVModel>()
                    });

                    if (!res.Success)
                    {
                        if (res.HasUnfinishedOperations)
                        {
                            _unfinishedOperationIds = res.UnfinishedPzvIds.ToHashSet();
                            RefreshHighlight();
                            FocusFirstUnfinishedOperation();
                            XtraMessageBox.Show(this, "В смене есть начатые, но не завершённые операции. Завершите операции, прежде чем закончить смену.", "Завершение операций", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LogWarning("Есть начатые и не завершённые операции. Смену закрывать нельзя", logContext);
                            return;
                        }

                        LogWarning(string.IsNullOrWhiteSpace(res.ErrorMessage) ? "Не удалось завершить смену." : res.ErrorMessage, logContext);
                        return;
                    }

                    await LoadPlanForTabAsync(tabEnd, forceReload: true);
                    await RefreshFioListAsync();
                    ApplyShiftUi(false, null, null);
                    return;
                }

                if (!int.TryParse(FioGridLookUpEdit.EditValue?.ToString(), out int selectedTab) || selectedTab <= 0)
                {
                    LogWarning("Выберите сотрудника.", logContext);
                    return;
                }

                var pzvIds = (_planPresenter.AllRows ?? Enumerable.Empty<KnitterPZVModel>())
                    .Select(x => x.pzvID)
                    .Where(x => x > 0)
                    .Distinct()
                    .ToList();

                var result = await _workSpaceService.StartShiftAsync(new StartShiftCommand
                {
                    Tab = selectedTab,
                    KmaId = _currentKmaId,
                    KmaNum = _currentKmaNum ?? "",
                    PzvIds = pzvIds
                });

                if (!result.Success)
                {
                    LogWarning(result.ErrorMessage, logContext);
                    return;
                }

                _currentShiftId = result.ShiftId;
                await LoadPlanForTabAsync(selectedTab, forceReload: true);
                await RefreshFioListAsync();
                ApplyShiftUi(true, result.ShiftId, result.ShiftStartTime);
            }
            catch (Exception ex)
            {
                LogError(ex, logContext);
            }
            finally
            {
                simpleButton2.Enabled = true;
            }
        }        

        private bool ShowShiftEndConfirmationDialog()
        {
            using (var dialog = new Form())
            using (var messageLabel = new Label())
            using (var okButton = new Button())
            using (var cancelButton = new Button())
            using (var messageFont = new Font("Segoe UI", 25f, FontStyle.Regular, GraphicsUnit.Point))
            using (var buttonFont = new Font("Segoe UI", 18f, FontStyle.Bold, GraphicsUnit.Point))
            using (var cancelFont = new Font("Segoe UI", 20f, FontStyle.Bold, GraphicsUnit.Point))
            {
                dialog.Text = "Завершение смены";
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.MinimizeBox = false;
                dialog.MaximizeBox = false;
                dialog.ShowInTaskbar = false;
                dialog.ClientSize = new Size(980, 360);

                messageLabel.AutoSize = false;
                messageLabel.Text = "Завершить текущую смену?";
                messageLabel.Font = messageFont;
                messageLabel.TextAlign = ContentAlignment.MiddleCenter;
                messageLabel.Location = new Point(20, 20);
                messageLabel.Size = new Size(940, 150);

                okButton.Text = "Завершить";
                okButton.Font = buttonFont;
                okButton.DialogResult = DialogResult.OK;
                okButton.Size = new Size(320, 120);
                okButton.Location = new Point(170, 205);

                cancelButton.Text = "ОТМЕНА";
                cancelButton.Font = cancelFont;
                cancelButton.DialogResult = DialogResult.Cancel;
                cancelButton.Size = new Size(320, 120);
                cancelButton.Location = new Point(510, 205);

                dialog.Controls.Add(messageLabel);
                dialog.Controls.Add(okButton);
                dialog.Controls.Add(cancelButton);

                dialog.AcceptButton = cancelButton;
                dialog.CancelButton = cancelButton;
                dialog.Shown += (_, __) => cancelButton.Focus();

                return dialog.ShowDialog(this) == DialogResult.OK;
            }
        }

        private void RefreshStatusColumns()
        {
            // Форсируем перерасчёт unbound-колонок (процент/статус)
            _gridVisualService.RefreshStatusColumns();
        }

        private void RefreshHighlight()
        {
            bandedGridView3?.RefreshData();
            advBandedGridView1?.RefreshData();
        }

        private void FocusFirstUnfinishedOperation()
        {
            var firstUnfinished = (_planPresenter.AllRows ?? Enumerable.Empty<KnitterPZVModel>())
                .FirstOrDefault(r => r != null && _unfinishedOperationIds.Contains(r.pzvID));

            if (firstUnfinished == null)
                return;

            var snap = new PlanFocusSnap
            {
                MasterTop = bandedGridView3?.TopRowIndex ?? 0,
                TaskNum = KnitterPlanUtils.NormalizeTaskNum(firstUnfinished.pzvNomZad),
                Machine = KnitterPlanUtils.NormalizeMachineKey(firstUnfinished.kmlNumber),
                DetailPzvId = firstUnfinished.pzvID,
                WasInDetail = true
            };

            _planFocusService.Restore(snap, preferDetailId: firstUnfinished.pzvID);
        }

        /// <summary>
        /// Обновляет футеры после изменений данных.
        /// </summary>
        private void RefreshFooterSummaries()
        {
            _gridVisualService.RefreshFooterSummaries();
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
                _idleSplashController.UpdateFioList(fioList);

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
                Value = DateTime.Today.Add(_blinkController.MorningTime)
            };

            var lblBlink2 = new Label { Text = "Время мигания 2:", Location = new Point(10, 55), AutoSize = true };
            var timeBlink2 = new DateTimePicker
            {
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true,
                Location = new Point(150, 51),
                Width = 120,
                Value = DateTime.Today.Add(_blinkController.EveningTime)
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
                _blinkController.MorningTime = timeBlink1.Value.TimeOfDay;
                _blinkController.EveningTime = timeBlink2.Value.TimeOfDay;
                _maxHoursClosedShift = numMaxHours.Value;
                _showAllAssignedWhenClosed = chkShowAllAssigned.Checked;
                _blinkController.ResetWindow();
            }
        }

        private void SetupBlinkTimers()
        {
            _blinkController.Start();
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
            var focusSnap = _planFocusService.CaptureFromRow(currentRow, _view);

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
            bool forceRowRefresh = false;
            if (currentRow?.pzvID > 0)
            {
                if (defaultQty > 0)
                {
                    if (qty <= defaultQty)
                    {
                        try
                        {
                            // Факт меньше или равен запланированному — mode = 1 c qtyFact
                            _sbController.MuteTable("dbo.planZagrVyaz", PlanBrokerSelfMute);
                            newIds = await _orchestrator.SplitPzvByFactAsync(currentRow.pzvID, qty);
                            Debug.WriteLine(string.Join(", ", newIds.Select(x => $"{x.Kind}:{x.NewPzvId}")));
                        }
                        catch (SqlException ex)
                        {
                            // При ошибке SQL (PZV_Split) не падаем, а принудительно перезагружаем текущую строку из БД
                            Debug.WriteLine($"[KnitterWorkSpace] SplitPzvByFactAsync SQL error {ex.Number}: {ex.Message}");
                            LogError(ex, "SplitPzvByFactAsync");
                            forceRowRefresh = true;
                        }
                    }
                }
            }

            // Обновим план, чтобы показать новую запись остатка (если была создана)
            if (int.TryParse(FioGridLookUpEdit.EditValue?.ToString(), out int tab))
            {
                int? preferDetailId = null;

                if (forceRowRefresh)
                {
                    // В случае ошибки split просто обновляем текущую строку/задание
                    preferDetailId = currentRow?.pzvID;
                }
                else
                {
                    if (newIds != null && newIds.Count > 0)
                    {
                        // 1️ Пытаемся найти остаток
                        var remainder = newIds.FirstOrDefault(x =>
                            x != null &&
                            string.Equals(x.Kind, "Remainder", StringComparison.OrdinalIgnoreCase));

                        if (remainder != null && remainder.NewPzvId > 0)
                        {
                            // Есть остаток - фокус на нём
                            preferDetailId = remainder.NewPzvId;
                        }
                        else
                        {
                            // 2️ Остатка нет (remaining = 0) - остаёмся на исходной строке
                            preferDetailId = currentRow?.pzvID;

                            // 3️ Fallback (на всякий случай)
                            if (preferDetailId == null || preferDetailId <= 0)
                                preferDetailId = newIds[0].NewPzvId;
                        }
                    }
                    else
                    {
                        // Если split ничего не вернул — остаёмся на текущей строке
                        preferDetailId = currentRow?.pzvID;
                    }
                }

                var refreshedPlan = await _orchestrator.GetPlanByTabAsync(tab, _currentShiftId, _currentKmaId, false, false, 14);
                var ids = new HashSet<int>(refreshedPlan.Select(r => r.pzvID));
                Debug.WriteLine($"Has remainder? {ids.Contains(preferDetailId ?? -1)}");
                _planPresenter.BindGroupDetails(bandedGridView3, advBandedGridView1, _planBindingSource, refreshedPlan ?? new List<KnitterPZVModel>(), clearTabs: false);

                // Принудительно обновляем detail для текущей master-строки:
                // DevExpress кеширует child-list в master-detail, и после ребинда
                // detail может не пересобраться пока не сделать Collapse/Expand.
                int masterHandle = _planFocusService.FindMasterHandleBySnap(focusSnap);
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

                _planFocusService.Restore(focusSnap, preferDetailId);
            }
            else
            {
                // Нет табеля — просто обновим расчётные колонки
                RefreshStatusColumns();
            }

            // Обновляем статус/процент после завершения операции
            RefreshStatusColumns();
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
                _sbController.MuteTable("dbo.planZagrVyaz", PlanBrokerSelfMute);
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
                LogError(ex, "PzvDateEndButtonEdit_DoubleClick");
            }
        }
        /// <summary>
        /// Настраивает таймер бездействия и подписывается на события активности пользователя.
        /// </summary>
        private void SetupIdleTimer()
        {
            _idleSplashController.Start();
            this.FormClosing += (s, e) =>
            {
                _shiftTimer.Stop();
                _shiftTimer.Dispose();
                _gridVisualService.Dispose();
                _blinkController.Dispose();
                _idleSplashController.Dispose();
            };
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
                LogWarning("Ошибка при завершении ServiceBroker в DisposeAsync контроллера.", nameof(ShutdownServiceBrokerAsync));
                // лог/игнор — но НЕ даём крашить закрытие формы
            }
            finally
            {
                try
                {
                    if (_sbService != null)
                        await _sbService.DisposeAsync();
                }
                catch { }
                _sbService = null;
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
                LogError(ex, nameof(SetupRowStyling));
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
                LogError(ex, nameof(SetupGridFonts));
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

        private static bool TrySplitHeaderSegment(string segment, out string label, out string value)
        {
            label = segment;
            value = string.Empty;
            if (string.IsNullOrWhiteSpace(segment))
                return false;

            int separatorIndex = segment.IndexOf(':');
            if (separatorIndex < 0)
                return false;

            label = segment.Substring(0, separatorIndex + 1);
            value = separatorIndex + 1 < segment.Length
                ? segment.Substring(separatorIndex + 1).TrimStart()
                : string.Empty;
            return true;
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
            string fieldName,
            DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            if (viewInfo == null)
                return;

            var column = view.Columns.ColumnByFieldName(fieldName);
            if (column == null || !column.Visible)
                return;

            var colInfo = viewInfo.ColumnsInfo[column];
            if (colInfo == null)
                return;

            decimal value = GetGroupColumnSum(view, rowHandle, fieldName) ?? 0m;
            string text = string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:0.00}", value);

            using (var format = new StringFormat(StringFormatFlags.NoWrap))
            {
                format.Alignment = StringAlignment.Far;
                format.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(text, view.Appearance.GroupRow.Font, e.Appearance.GetForeBrush(e.Cache), colInfo.Bounds, format);
            }
        }

        private decimal? GetGroupColumnSum(AdvBandedGridView view, int groupRowHandle, string fieldName)
        {
            if (view == null || !view.IsGroupRow(groupRowHandle))
                return null;

            int childCount = view.GetChildRowCount(groupRowHandle);
            if (childCount <= 0)
                return 0m;

            decimal sum = 0m;
            for (int i = 0; i < childCount; i++)
            {
                int childHandle = view.GetChildRowHandle(groupRowHandle, i);
                if (view.IsGroupRow(childHandle))
                {
                    var nested = GetGroupColumnSum(view, childHandle, fieldName);
                    if (nested.HasValue)
                        sum += nested.Value;
                    continue;
                }

                object cellValue = view.GetRowCellValue(childHandle, fieldName);
                if (cellValue == null || cellValue == DBNull.Value)
                    continue;

                try
                {
                    sum += Convert.ToDecimal(cellValue, System.Globalization.CultureInfo.CurrentCulture);
                }
                catch
                {
                    // Игнорируем значения, которые не удалось привести к decimal.
                }
            }

            return sum;
        }

        private void AdvBandedGridView1_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            var view = sender as AdvBandedGridView;
            var info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;
            if (view == null || info == null)
                return;

            string groupText = info.GroupText ?? string.Empty;

            string originalText = info.GroupText;
            info.GroupText = string.Empty;
            e.Painter.DrawObject(e.Info);
            info.GroupText = originalText;

            Font baseFont = view.Appearance.GroupRow.Font ?? SystemFonts.DefaultFont;
            float valueSize = baseFont.Size + 1f;
            float labelSize = baseFont.Size - 2f;
            FontStyle valueStyle = baseFont.Style | FontStyle.Bold;
            FontStyle labelStyle = baseFont.Style & ~FontStyle.Bold;

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

            using (var valueFont = new Font(baseFont.FontFamily, valueSize, valueStyle))
            using (var labelFont = new Font(baseFont.FontFamily, labelSize, labelStyle))
            using (var labelBrush = new SolidBrush(Color.DimGray))
            {
                float x = textBounds.Left;
                string[] parts = groupText.Split(new[] { " | " }, StringSplitOptions.None);
                const float blockPadding = 12f;
                const float separatorPadding = 6f;
                var tokens = new List<(string Text, Font Font, float Width, Brush Brush, float LeftInset)>(parts.Length * 3);
                for (int i = 0; i < parts.Length; i++)
                {
                    string part = parts[i];
                    if (TrySplitHeaderSegment(part, out string label, out string value))
                    {
                        float labelWidth = (float)Math.Ceiling(e.Cache.CalcTextSize($"{label} ", labelFont).Width) + blockPadding;
                        tokens.Add(($"{label} ", labelFont, labelWidth, labelBrush, blockPadding / 2f));

                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            float valueWidth = (float)Math.Ceiling(e.Cache.CalcTextSize(value, valueFont).Width) + blockPadding;
                            tokens.Add((value, valueFont, valueWidth, e.Appearance.GetForeBrush(e.Cache), blockPadding / 2f));
                        }
                    }
                    else
                    {
                        float partWidth = (float)Math.Ceiling(e.Cache.CalcTextSize(part, valueFont).Width) + blockPadding;
                        tokens.Add((part, valueFont, partWidth, e.Appearance.GetForeBrush(e.Cache), blockPadding / 2f));
                    }

                    if (i < parts.Length - 1)
                    {
                        const string separator = " | ";
                        float sepWidth = (float)Math.Ceiling(e.Cache.CalcTextSize(separator, labelFont).Width) + separatorPadding;
                        tokens.Add((separator, labelFont, sepWidth, labelBrush, separatorPadding / 2f));
                    }
                }

                foreach (var token in tokens)
                {
                    SizeF tokenSize = e.Cache.CalcTextSize(token.Text, token.Font);
                    float y = textBounds.Top + Math.Max(0f, (textBounds.Height - tokenSize.Height) / 2f);
                    e.Graphics.DrawString(token.Text, token.Font, token.Brush, x + token.LeftInset, y);
                    x += token.Width;
                }
            }

            var viewInfo = view.GetViewInfo() as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo;
            if (viewInfo != null)
            {
                DrawGroupSummaryValue(view, viewInfo, e.RowHandle, "PlanChas_UI", e);
                DrawGroupSummaryValue(view, viewInfo, e.RowHandle, "FactChas_UI", e);
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
                LogError(ex, nameof(UpdateUnfinishedOperationsList));
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
                var machineKey = KnitterPlanUtils.NormalizeMachineKey(row.kmlNumber);
                var taskKey = KnitterPlanUtils.NormalizeTaskNum(row.pzvNomZad);
                var rows = _planPresenter.AllRows?
                    .Where(r =>
                        r != null &&
                        string.Equals(KnitterPlanUtils.NormalizeMachineKey(r.kmlNumber), machineKey, StringComparison.OrdinalIgnoreCase) &&
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
                LogSuccess($"Выполнено ручное обновление текущего табеля {tab}.", nameof(ReloadCurrentTabAsync));
            }
            else
            {
                LogWarning("Попытка ручного обновления без выбранного табельного номера.", nameof(ReloadCurrentTabAsync));
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
            var focusSnap = _planFocusService.CaptureCurrent();

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
                _planFocusService.Restore(focusSnap);
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
                LogWarning($"Не удалось загрузить зону по табельному номеру {tab}.", nameof(UpdateZoneAsync));
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
                LogWarning($"Не удалось определить состояние смены для табельного номера {tab}.", nameof(UpdateShiftStateAsync));
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
                    LogSuccess($"Получен сигнал обновления для объекта {objectName}.", nameof(RestartDataByObjectNameAsync));

                    // Если это наша хранимая процедура плана - перезагружаем план
                    if (string.Equals(objectName, "GetPlanZagrVyazNorm_ByTab3", StringComparison.OrdinalIgnoreCase))
                    {
                        if (_currentLoadedTab.HasValue)
                        {
                            System.Diagnostics.Debug.WriteLine($"[KnitterWorkSpace] Reloading plan for tab {_currentLoadedTab.Value}");
                            await LoadPlanForTabAsync(_currentLoadedTab.Value, forceReload: true);
                            LogSuccess($"План успешно перезагружен по уведомлению брокера для табеля {_currentLoadedTab.Value}.", nameof(RestartDataByObjectNameAsync));
                        }
                        else
                        {
                            LogWarning("Пропущена перезагрузка плана: текущий табель не выбран.", nameof(RestartDataByObjectNameAsync));
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                LogError(ex, $"{nameof(RestartDataByObjectNameAsync)}:{objectName}");
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
                var dbHelper = new DatabaseHelper("ace");
                _sbService = new ServiceBrokerService(dbHelper);
            }

            var list = await _sbService.GetObjectListForServiceBroker(objectName, ct);
            return ServiceBrokerListenInfoNormalizer.Normalize(list);
        }

        /// <summary>
        /// Переопределяем инициализацию для установки UseSchemaInListenName = true.
        /// </summary>
        public async Task InitServiceBrokerAsync(CancellationToken ct)
        {
            LogSuccess($"Инициализация ServiceBroker: objects={string.Join(", ", ServiceBrokerObjects)}.", nameof(InitServiceBrokerAsync));
            await _sbController.InitAsync(ct, _sbHub, _sbHubOwnerId, startBrokers: false);

            var tables = _sbController.Helper?.GetListeningTables() ?? Array.Empty<string>();
            LogSuccess($"ServiceBroker подписан на таблицы: {string.Join(", ", tables)}.", nameof(InitServiceBrokerAsync));
        }

        /// <summary>
        /// Обновление данных формы по уведомлению брокера (как в PlanZagrVyaz)
        /// </summary>
        public async Task UpdateDataInFormAsync(string tableName, string? fieldsChangedCsv)
        {
            try
            {
                await _sbController.HandleUpdateAsync(tableName, fieldsChangedCsv ?? string.Empty);
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(UpdateDataInFormAsync));
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



    


