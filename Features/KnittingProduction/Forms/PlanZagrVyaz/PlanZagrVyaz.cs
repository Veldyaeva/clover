using DevExpress.Data;
using DevExpress.Diagram.Core.Shapes;
using DevExpress.Mvvm.Native;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ButtonsPanelControl;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraReports.UI;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SewingProduction.Core;
using SewingProduction.Core.helpers;
using SewingProduction.Core.interfaces;
using SewingProduction.Core.Models;
using SewingProduction.Core.services;
using SewingProduction.Extensions;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Adapters;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Contexts;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Requests;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Results;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Routing;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Services;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.UseCases;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.UseCases.PzvActions;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Validation;
//using SewingProduction.Features.KnittingProduction.Forms.PlanZagrVyaz.Adapters;
//using SewingProduction.Features.KnittingProduction.Forms.PlanZagrVyaz.Application.Contexts;

using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Features.KnittingProduction.Services;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Report;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SewingProduction.Core.helpers.BindingSourceHelper;
using static SewingProduction.Helpers.GridHelper;
using Formatting = Newtonsoft.Json.Formatting;
using Volatile = System.Threading.Volatile;
//using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Adapters;
//using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Routing;


namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class PlanZagrVyaz : CustomForm, IServiceBrokerHost
    {
        private readonly ServiceBrokerController _sbController;
        private readonly IAppServiceBrokerHub _sbHub;
        private readonly string _sbHubOwnerId = $"PlanZagrVyaz:{Guid.NewGuid():N}";
        private readonly HashSet<string> _ignoredServiceBrokerTables = new(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, Func<Task>> _objectRestartMap = null!;
        private CancellationTokenSource? _loadCts;
        private CancellationTokenSource? _lifetimeCts;
        private CancellationTokenSource? _focusLoadCts;

        private readonly DebouncedLoader _loader = new(delayMs: 300);
        private readonly ILogger _logger = new FileLogger();
        private readonly VyazService _vyazService;
        private readonly ServiceBrokerService _sbService;

        private static readonly TimeSpan PlanBrokerSelfMute = TimeSpan.FromSeconds(2);

        private static DatabaseHelper _dbHelper;
        private static DbService _dbService;
        private static MlService _mlService;
        private static ArtNormRepository _anService;
        private static BulkHelper _bulkHelper;
        private static GridHelper _gridHelper;
        private static LayoutControlGroupHelper _lcgHelper;

        private readonly int vyazPodrKod;
        private int _xPzvID;
        private string _xColumn = string.Empty;
        private bool _closing;
        private bool _isCountTabKM;
        private bool _suppressZadanyFocusHandler;
        private bool _suppressPachFocusHandler;
        private bool _suppressZadanyFocusedChanged;
        private int _serviceBrokerShutdownStarted;
        private int _rzvLoadVersion;
        private int _loadVersion;
        private int _focusSeq;
        private int _isUiRefreshing;

        private int TopRowIndexPZV;
        private int TopRowIndexSmenZadany;

        private string? _pendingPszNom;
        private bool _autoSelectPending;

        private readonly PlanZagrVyazContextBuilder _contextBuilder;
        private readonly IPzvBulkUpdateService _pzvBulkUpdateService;
        private readonly PzvActionValidator _pZVActionValidator;
        private readonly IPzvActionRouter _actionRouter;
        private readonly LoadPzvOperationsBySelectionUseCase _loadPzvOperationsUseCase;
        private readonly PzvOperationsGridUpdater _pzvOperationsGridUpdater;

        private List<PlanTotalHoursByKnitMachine> _currentPlanTotalHoursByKnitMachineData = new();
        private List<PlanTotalHoursByKnitMachine> planTotalHoursByKnitMachineData = new();
        private BindingList<PlanTotalHoursByKnitMachine> _planTotalHoursByKnitMachineBindingList;
        private BindingSource _planTotalHoursByKnitMachineBindingSource;

        private List<PZVZadanyList> _currentZadanyListData = new();
        private List<PZVZadanyList> zadanyListData = new();
        private BindingList<PZVZadanyList> _zadanyListBindingList;
        private BindingSource _zadanyListBindingSource;

        private List<RzvPachListByNom> _currentRzvPachListByNomData = new();
        private List<RzvPachListByNom> rzvPachListByNomData = new();
        private BindingList<RzvPachListByNom> _rzvPachListByNomBindingList;
        private BindingSource _rzvPachListByNomBindingSource;

        private List<PZVOperList> _currentPZVOperListByPachListData = new();
        private List<PZVOperList> pZVOperListByPachListData = new();
        private BindingList<PZVOperList> _pZVOperListByPachListBindingList;
        private BindingSource _pZVOperListByPachListBindingSource;

        private BindingSource _smenZadanyVyazBindingSource;
        private BindingSource _smenZadanyVyazNewBindingSource;
        private BindingSource _naryadZadanyVyazBindingSource;
        private BindingSource _planTotalQuantityByArticulBindingSource;
        //------------------------------------------
        //private int vyazPodrKod = 0;


        //private readonly ServiceBrokerController _sbController;
        //private readonly IAppServiceBrokerHub _sbHub;
        //private readonly string _sbHubOwnerId = $"PlanZagrVyaz:{Guid.NewGuid():N}";

        //// Таблицы, изменения в которых НЕ должны инициировать обновление UI
        //// (типичные LEFT JOIN справочники и прочий "шум").
        //private readonly HashSet<string> _ignoredServiceBrokerTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //private Dictionary<string, Func<Task>> _objectRestartMap = null!;
        //private CancellationTokenSource _loadCts;
        //private CancellationTokenSource _lifetimeCts;

        //private readonly DebouncedLoader _loader = new(delayMs: 300);

        //private static DatabaseHelper _dbHelper;
        //private static DbService _dbService;
        //private static MlService _mlService;
        //private static ArtNormRepository _anService;
        //private readonly ServiceBrokerService _sbService;
        //private static BulkHelper _bulkHelper;
        //private static GridHelper _gridHelper;
        //private static LayoutControlGroupHelper _lcgHelper;
        ////        private static BindingSourceHelper _bSHelper;
        //private readonly ILogger _logger = new FileLogger();
        //private readonly VyazService _vyazService;
        //private static readonly TimeSpan PlanBrokerSelfMute = TimeSpan.FromSeconds(2);

        //string _xColumn = string.Empty;
        //int _xPzvID = 0;
        //private bool _isCountTabKM = false;
        //private bool _closing;
        //private string _pendingPszNom;
        //private bool _autoSelectPending;
        //private bool _suppressZadanyFocusHandler;
        //private bool _suppressPachFocusHandler;
        //private bool _suppressZadanyFocusedChanged;
        //private int _serviceBrokerShutdownStarted;
        //private int _rzvLoadVersion = 0;
        //private int _loadVersion = 0;

        //private int TopRowIndexPZV = 0;
        //private int TopRowIndexSmenZadany = 0;

        private List<KnitWorkingShiftSmen> _zonesCache = new List<KnitWorkingShiftSmen>();

        //private List<PlanTotalHoursByKnitMachine> _currentPlanTotalHoursByKnitMachineData = new List<PlanTotalHoursByKnitMachine>();
        //private List<PlanTotalHoursByKnitMachine> planTotalHoursByKnitMachineData = new List<PlanTotalHoursByKnitMachine>();
        //private BindingList<PlanTotalHoursByKnitMachine> _planTotalHoursByKnitMachineBindingList;
        //private BindingSource _planTotalHoursByKnitMachineBindingSource;

        //private List<PZVZadanyList> _currentZadanyListData = new List<PZVZadanyList>();
        //private List<PZVZadanyList> zadanyListData = new List<PZVZadanyList>();
        //private BindingList<PZVZadanyList> _zadanyListBindingList;
        //private BindingSource _zadanyListBindingSource;

        private List<PZVZadanyList> _currentZadanyListNewData = new List<PZVZadanyList>();
        private List<PZVZadanyList> zadanyListNewData = new List<PZVZadanyList>();
        private BindingList<PZVZadanyList> _zadanyListNewBindingList;
        private BindingSource _zadanyListNewBindingSource;

        //private List<RzvPachListByNom> _currentRzvPachListByNomData = new List<RzvPachListByNom>();
        //private List<RzvPachListByNom> rzvPachListByNomData = new List<RzvPachListByNom>();
        //private BindingList<RzvPachListByNom> _rzvPachListByNomBindingList;
        //private BindingSource _rzvPachListByNomBindingSource;

        //private List<RzvPachListByNom> _currentRzvPachListByNomNewData = new List<RzvPachListByNom>();
        private List<RzvPachListByNom> rzvPachListByNomNewData = new List<RzvPachListByNom>();
        private BindingList<RzvPachListByNom> _rzvPachListByNomNewBindingList;
        private BindingSource _rzvPachListByNomNewBindingSource;

        //private List<PZVOperList> _currentPZVOperListByPachListData = new List<PZVOperList>();
        //private List<PZVOperList> pZVOperListByPachListData = new List<PZVOperList>();
        //private BindingList<PZVOperList> _pZVOperListByPachListBindingList;
        //private BindingSource _pZVOperListByPachListBindingSource;

        //private List<PZVOperList> _currentPZVOperListByPachListNewData = new List<PZVOperList>();
        private List<PZVOperList> pZVOperListByPachListNewData = new List<PZVOperList>();
        private BindingList<PZVOperList> _pZVOperListByPachListNewBindingList;
        private BindingSource _pZVOperListByPachListNewBindingSource;

        //private List<ArtNormN> _currentArtNormNData = new List<ArtNormN>();
        //private List<ArtNormN> artNormNData = new List<ArtNormN>();
        private BindingList<ArtNormN> _artNormNBindingList;
        private BindingSource _artNormNBindingSource;

        //private List<NormRasz> _currentNormRaszData = new List<NormRasz>();
        //private List<NormRasz> normRaszData = new List<NormRasz>();
        private BindingList<NormRasz> _normRaszBindingList;
        private BindingSource _normRaszBindingSource;

        //private List<MlOp> _currentMlOpData = new List<MlOp>();
        //private List<MlOp> mlOpData = new List<MlOp>();
        private BindingList<MlOp> _mlOpBindingList;
        private BindingSource _mlOpBindingSource;

        //private BindingSource _smenZadanyVyazBindingSource;

        //private BindingSource _smenZadanyVyazNewBindingSource;

        private List<KnitWorkingShiftSmen> KnitWorkingShiftSmenData = new List<KnitWorkingShiftSmen>();
        private BindingList<KnitWorkingShiftSmen> _knitWorkingShiftSmenBindingList;
        private BindingSource _knitWorkingShiftSmenBindingSource;

        //private BindingSource _naryadZadanyVyazBindingSource;
        //private BindingSource _planTotalQuantityByArticul;

        //private CancellationTokenSource _focusLoadCts;
        //private int _focusSeq;
        //private int _isUiRefreshing;

        public PlanZagrVyaz(UserClass User, int _vyazPodrKod) : base(User)
        {
            InitializeComponent();

            this.vyazPodrKod = _vyazPodrKod;
            switch (vyazPodrKod)
            {
                case 1:
                    //this.Text = "РС Мастера Вяз.Цеха";
                    Text = "РС Мастера ВЦ";
                    break;
                case 2:
                    //this.Text = "РС Мастера Цеха Отпарки";
                    Text = "РС Мастера Отп";
                    break;
                case 3:
                    //this.Text = "РС Мастера Раскр.Цеха";
                    Text = "РС Мастера РЦ";
                    break;
            }
            _dbHelper = new DatabaseHelper("ace");
            _dbService = new DbService(_dbHelper);
            _anService = new ArtNormRepository(_dbHelper);
            _sbService = new ServiceBrokerService(_dbHelper);
            _sbController = new ServiceBrokerController(this);
            _sbHub = AppServices.Services.GetRequiredService<IAppServiceBrokerHub>();
            _bulkHelper = new BulkHelper();
            _gridHelper = new GridHelper();
            _lcgHelper = new LayoutControlGroupHelper();
            _vyazService = new VyazService(_dbHelper);
            _mlService = new MlService(_dbHelper);

            _smenZadanyVyazBindingSource = new BindingSource();
            _smenZadanyVyazNewBindingSource = new BindingSource();
            _naryadZadanyVyazBindingSource = new BindingSource();
            _planTotalHoursByKnitMachineBindingSource = new BindingSource();
            _planTotalQuantityByArticulBindingSource = new BindingSource();
            _zadanyListBindingSource = new BindingSource();
            _zadanyListNewBindingSource = new BindingSource();
            _rzvPachListByNomBindingSource = new BindingSource();
            _rzvPachListByNomNewBindingSource = new BindingSource();
            _pZVOperListByPachListBindingSource = new BindingSource();
            _pZVOperListByPachListNewBindingSource = new BindingSource();
            _artNormNBindingSource = new BindingSource();
            _normRaszBindingSource = new BindingSource();
            _mlOpBindingSource = new BindingSource();
            _knitWorkingShiftSmenBindingSource = new BindingSource();

            _contextBuilder = new PlanZagrVyazContextBuilder(
                this.vyazPodrKod,
                _pZVOperListByPachListBindingSource,
                _rzvPachListByNomBindingSource,
                _smenZadanyVyazBindingSource,
                gridViewPZVOperList,
                gridViewRzvPachListByNom,
                advBandedGridViewSmenZadany);

            _pzvOperationsGridUpdater = new PzvOperationsGridUpdater(
                gridViewPZVOperList,
                _pZVOperListByPachListBindingSource,
                () => TopRowIndexPZV,
                value => TopRowIndexPZV = value);

            _pzvBulkUpdateService = new PzvBulkUpdateService(
                _pZVOperListByPachListBindingSource,
                _dbHelper,
                _bulkHelper,
                MutePlanBrokerNotifications,
                async () => await LoadPlanZagrVyazByZadanySelection());

            _pZVActionValidator = new PzvActionValidator();

            var assignKnittingMachine = new AssignKnittingMachineUseCase(
    _pZVActionValidator,
    _pzvBulkUpdateService,
    _dbHelper);
            var cancelKnittingMachine = new CancelKnittingMachineUseCase(_pZVActionValidator, _pzvBulkUpdateService);
            var assignTab = new AssignTabUseCase(_pZVActionValidator, _pzvBulkUpdateService);
            var cancelTab = new CancelTabUseCase(_pZVActionValidator, _pzvBulkUpdateService);
            var startWork = new StartWorkUseCase(_pZVActionValidator, _pzvBulkUpdateService);
            var cancelStartWork = new CancelWorkStartUseCase(_pZVActionValidator, _pzvBulkUpdateService);
            var stopWork = new StopWorkUseCase(_pZVActionValidator, _pzvBulkUpdateService);
            var cancelStopWork = new CancelWorkStopUseCase(_pZVActionValidator, _pzvBulkUpdateService);
            var confirmMaster = new ConfirmMasterUseCase(_pZVActionValidator, _pzvBulkUpdateService);
            var cancelMaster = new CancelMasterConfirmationUseCase(_pZVActionValidator, _pzvBulkUpdateService);
            var assignTab999 = new AssignTab999UseCase(_pZVActionValidator, _pzvBulkUpdateService);
            var cancelTab999 = new CancelTab999UseCase(_pZVActionValidator, _pzvBulkUpdateService);

            _actionRouter = new PzvActionRouter(
                assignKnittingMachine,
                cancelKnittingMachine,
                assignTab,
                cancelTab,
                startWork,
                cancelStartWork,
                stopWork,
                cancelStopWork,
                confirmMaster,
                cancelMaster,
                assignTab999,
                cancelTab999);

            _loadPzvOperationsUseCase = new LoadPzvOperationsBySelectionUseCase(_vyazService);

            SetupGridNaryadZadanyEvents();
            InitializeIgnoredTables();

            //DxSkinFix.ResetLabelsToSkin(this);
            //SetupGridNaryadZadanyEvents();
            //_dbHelper = new DatabaseHelper("ace");
            //_dbService = new DbService(_dbHelper);
            //_anService = new ArtNormRepository(_dbHelper);
            //_sbService = new ServiceBrokerService(_dbHelper);
            //_sbController = new ServiceBrokerController(this);
            //_sbHub = AppServices.Services.GetRequiredService<IAppServiceBrokerHub>();
            //_bulkHelper = new BulkHelper();
            //_gridHelper = new GridHelper();
            //_lcgHelper = new LayoutControlGroupHelper();

            //_vyazService = new VyazService(_dbHelper);
            //_mlService = new MlService(_dbHelper);
            //_ignoredServiceBrokerTables.UnionWith(new[]
            //{
            //    // GetPlanZagrVyazByPachList / GetSmenZadanyVyaz pull these via views and reference joins,
            //    // but they do not need to force live-refresh for this form.
            //    "dbo.matrix_class",
            //    "dbo.plan_sezon_zad",
            //    "dbo.tab_n",
            //    "dbo.norm_rasz",
            //    "dbo.knitMachineArea",
            //    "dbo.gr_rab_dn",
            //    "dbo.tabel_sp",
            //    "dbo.spOborudShv",
            //    "dbo.podr_vyaz",
            //    "dbo.v_nazn_akt",
            //    "dbo.GradaciaStatus",
            //    "dbo.proizv_modify_zc_history",
            //    "dbo.knitMachineList",
            //    "dbo.owenDeviceParam",
            //    "dbo.knitMachineAreaEmp",
            //    "dbo.fio"
            //});

            //_smenZadanyVyazBindingSource = new BindingSource
            //{
            //    DataSource = new BindingList<SmenZadanyVyaz>()
            //};
            //_smenZadanyVyazNewBindingSource = new BindingSource
            //{
            //    DataSource = new BindingList<SmenZadanyVyaz>()
            //};

            ////нужно будет определять, мастер вяз цеха или отпарки заходит в форму и
            ////сохранять признак подразделения для дальнейшней загрузки операций только того подразделения, чей мастер зашел
            //// пока что примем, что заходит только мастер вяз цеха, признак пропишем жестко 1
            //gridControlZadanyList.ForceInitialize();
            //gridViewZadanyList.RefreshData();
            //gridControlRzvPachListByNom.ForceInitialize();
            //gridViewRzvPachListByNom.RefreshData();
            //gridViewPZVOperList.ShownEditor += gridViewPZVOperList_ShownEditor;
            //gridViewPZVOperList.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDownFocused;
            //vyazPodrKod = _vyazPodrKod;
            //switch (vyazPodrKod)
            //{
            //    case 1:
            //        //this.Text = "РС Мастера Вяз.Цеха";
            //        Text = "РС Мастера ВЦ";
            //        break;
            //    case 2:
            //        //this.Text = "РС Мастера Цеха Отпарки";
            //        Text = "РС Мастера Отп";
            //        break;
            //    case 3:
            //        //this.Text = "РС Мастера Раскр.Цеха";
            //        Text = "РС Мастера РЦ";
            //        break;
            //}
        }
        public PlanZagrVyaz(UserClass User) : base(User)
        {
            InitializeComponent();
        }
        private void InitializeIgnoredTables()
        {
            _ignoredServiceBrokerTables.UnionWith(new[]
            {
                "dbo.matrix_class",
                "dbo.plan_sezon_zad",
                "dbo.tab_n",
                "dbo.norm_rasz",
                "dbo.knitMachineArea",
                "dbo.gr_rab_dn",
                "dbo.tabel_sp",
                "dbo.spOborudShv",
                "dbo.podr_vyaz",
                "dbo.v_nazn_akt",
                "dbo.GradaciaStatus",
                "dbo.proizv_modify_zc_history",
                "dbo.knitMachineList",
                "dbo.owenDeviceParam",
                "dbo.knitMachineAreaEmp",
                "dbo.fio"
            });
        }

        private void MutePlanBrokerNotifications()
        {
            _sbController.MuteTable("dbo.planZagrVyaz", PlanBrokerSelfMute);
        }
        //public static class DxSkinFix
        //{
        //    public static void ResetLabelsToSkin(Control root)
        //    {
        //        foreach (Control c in root.Controls)
        //        {
        //            if (c is LabelControl lc)
        //            {
        //                lc.Appearance.BackColor = Color.Empty;
        //                lc.Appearance.ForeColor = Color.Empty;
        //                lc.Appearance.Options.UseBackColor = false;
        //                lc.Appearance.Options.UseForeColor = false;
        //                lc.LookAndFeel.UseDefaultLookAndFeel = true;
        //            }

        //            if (c.HasChildren)
        //                ResetLabelsToSkin(c);
        //        }
        //    }
        //}
        #region ServiceBroker
        private void InitObjectRestartMap()
        {
            try
            {
                _objectRestartMap = new Dictionary<string, Func<Task>>(StringComparer.OrdinalIgnoreCase)
                {
                    ["GetSmenZadanyVyaz"] = async () =>
                    {
                        int xTopRowIndex = 0;
                        int xFocusedRowHandle = 0;
                        switch (vyazPodrKod)
                        {
                            case 1:
                                xTopRowIndex = advBandedGridViewSmenZadany.TopRowIndex;
                                xFocusedRowHandle = advBandedGridViewSmenZadany.FocusedRowHandle;
                                break;
                            case 2:
                            case 3:
                                xTopRowIndex = gridViewSmenZadanyOtp.TopRowIndex;
                                xFocusedRowHandle = gridViewSmenZadanyOtp.FocusedRowHandle;
                                break;
                        }
                        //await LoadSmenZadanyVyazNewDataAsync(vyazPodrKod); // сменное задание
                        await LoadSmenZadanyVyazNewDataAsync(1); // сменное задание
                        //await InvokeOnUiAsync(async () =>
                        //{
                        //    gridViewSmenZadany.TopRowIndex = xTopRowIndex;
                        //    gridViewSmenZadany.TopRowIndex = xTopRowIndex;
                        //});
                        switch (vyazPodrKod)
                        {
                            case 1:
                                advBandedGridViewSmenZadany.TopRowIndex = xTopRowIndex;
                                advBandedGridViewSmenZadany.FocusedRowHandle = xFocusedRowHandle;
                                break;
                            case 2:
                            case 3:
                                gridViewSmenZadanyOtp.TopRowIndex = xTopRowIndex;
                                gridViewSmenZadanyOtp.FocusedRowHandle = xFocusedRowHandle;
                                break;
                        }
                    },

                    ["knitWorkingShiftNewCurrentSmen_view"] = async () =>
                    {
                        SmenZadanyFocusedRowChanged(advBandedGridViewSmenZadany, advBandedGridViewSmenZadany.FocusedRowHandle); // наряд-задание ВЗП
                        SmenZadanyFocusedRowChanged(gridViewSmenZadanyOtp, gridViewSmenZadanyOtp.FocusedRowHandle); // наряд-задание Отп, РЦ
                    },

                    ["GetPlanZagrVyazByPachList"] = async () =>
                    {
                        int xTopRowIndex = gridViewPZVOperList.TopRowIndex;
                        int xFocusedRowHandle = gridViewPZVOperList.FocusedRowHandle;
                        //MessageBox.Show($"before xTopRowIndex = {xTopRowIndex}" +
                        //    $"\n before gridViewPZVOperList.TopRowIndex = {gridViewPZVOperList.TopRowIndex} " +
                        //    $"\n before xFocusedRowHandle = {xFocusedRowHandle}" +
                        //    $"\n before gridViewPZVOperList.FocusedRowHandle = {gridViewPZVOperList.FocusedRowHandle}");
                        await LoadPlanZagrVyazByZadanySelection(); // операции по расчетам/пачкам
                        //Debug.WriteLine($"after1 xTopRowIndex = {xTopRowIndex}" +
                        //    $"\n after1 gridViewPZVOperList.TopRowIndex = {gridViewPZVOperList.TopRowIndex}" +
                        //    $"\n after1 xFocusedRowHandle = {xFocusedRowHandle}" +
                        //    $"\n after1 gridViewPZVOperList.FocusedRowHandle = {gridViewPZVOperList.FocusedRowHandle}");
                        //MessageBox.Show($"after1 xTopRowIndex = {xTopRowIndex}" +
                        //    $"\n after1 gridViewPZVOperList.TopRowIndex = {gridViewPZVOperList.TopRowIndex}" +
                        //    $"\n after1 xFocusedRowHandle = {xFocusedRowHandle}" +
                        //    $"\n after1 gridViewPZVOperList.FocusedRowHandle = {gridViewPZVOperList.FocusedRowHandle}");
                        //Thread.Sleep(10000);
                        //await Task.Yield();
                        await InvokeOnUiAsync(async () =>
                        {
                            gridViewPZVOperList.FocusedRowHandle = xFocusedRowHandle;
                            gridViewPZVOperList.TopRowIndex = xTopRowIndex;
                        });
                        //MessageBox.Show($"after2 xTopRowIndex = {xTopRowIndex}" +
                        //    $"\n after2 gridViewPZVOperList.TopRowIndex = {gridViewPZVOperList.TopRowIndex}" +
                        //    $"\n after2 xFocusedRowHandle = {xFocusedRowHandle}" +
                        //    $"\n after2 gridViewPZVOperList.FocusedRowHandle = {gridViewPZVOperList.FocusedRowHandle}");
                    }
                };
                Debug.WriteLine($"[PlanZagrVyaz] InitObjectRestartMap initialized with keys: {string.Join(", ", _objectRestartMap.Keys)}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка InitObjectRestartMap: {ex.Message}");
            }

        }

        public IReadOnlyList<string> ServiceBrokerObjects =>
            _objectRestartMap?.Keys.ToArray() ?? Array.Empty<string>();

        public IReadOnlyDictionary<string, int> RefreshPriorities =>
            new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                { "GetPlanZagrVyazByPachList", 10 },
                { "GetSmenZadanyVyaz", 5 }
            };

        public string ServiceBrokerFormName => GetType().Name;

        public bool UseSchemaInListenName => true;

        public IReadOnlyCollection<string> IgnoredTables => _ignoredServiceBrokerTables;

        public async Task InitServiceBrokerAsync(CancellationToken ct)
        {
            await _sbController.InitAsync(ct, _sbHub, _sbHubOwnerId, startBrokers: false);
            //await _sbController.InitAsync(ct, _sbHub, _sbHubOwnerId, startBrokers: true);

            var tables = _sbController.Helper?.GetListeningTables() ?? Array.Empty<string>();
            Debug.WriteLine($"[PlanZagrVyaz] Listening tables: {string.Join(", ", tables)}");
        }

        public async Task<List<ServiceBrokerModel.TableListenInfo>> LoadListenInfoByObjectNameAsync(
            string objectName, CancellationToken ct)
        {
            var list = await _sbService.GetObjectListForServiceBroker(objectName, ct);
            return ServiceBrokerListenInfoNormalizer.Normalize(list);
        }
        private Task InvokeOnUiAsync(Func<Task> fn)
        {
            if (InvokeRequired)
            {
                var tcs = new TaskCompletionSource<object>();
                BeginInvoke(new Action(async () =>
                {
                    try { await fn(); tcs.TrySetResult(null); }
                    catch (Exception ex) { tcs.TrySetException(ex); }
                }));
                return tcs.Task;
            }
            Debug.WriteLine($"[PlanZagrVyaz] InvokeOnUiAsync: already on UI thread");
            return fn();
        }

        private async Task RestartDataByObjectNameAsync(string objectName)
        {
            await InvokeOnUiAsync(async () =>
            {
                Debug.WriteLine($"[PlanZagrVyaz] RestartDataByObjectNameAsync(UI): {objectName}");

                if (string.Equals(objectName, "GetPlanZagrVyazByPachList", StringComparison.OrdinalIgnoreCase))
                {
                    await LoadPlanTotalHoursByKnitMachineDataAsync();
                }
                else if (string.Equals(objectName, "getSmenZadanyVyaz", StringComparison.OrdinalIgnoreCase))
                {
                    await LoadSmenZadanyVyazDataAsync();
                }
            });
        }

        public async Task RestartDataByObjectNameAsync(string objectName, CancellationToken ct)
        {
            try
            {
                await InvokeOnUiAsync(async () =>
                {
                    Debug.WriteLine($"[PlanZagrVyaz] RestartDataByObjectNameAsync(UI): {objectName}");

                    if (string.IsNullOrWhiteSpace(objectName))
                        return;

                    if (_objectRestartMap.TryGetValue(objectName, out var action))
                    {
                        await action();
                    }
                    else
                    {
                        // на всякий случай: если прилетело неизвестное имя
                        // можно залогировать
                    }
                });
                Debug.WriteLine($"[PlanZagrVyaz] RestartDataByObjectNameAsync completed: {objectName}");
            }
            catch (SqlException ex)
            {
                var first = ex.Errors.Cast<SqlError>().FirstOrDefault();
                _logger.LogErrorAsync(ex,
                    $"SQL error: Number={ex.Number}, State={ex.State}, Class={ex.Class}, " +
                    $"Procedure={first?.Procedure}, Line={first?.LineNumber}, Message={first?.Message}");
                throw;
            }
            catch (ArgumentException ex)
            {
                // именно дубликаты ключей
                MessageBox.Show(
                    $"RestartDataByObjectNameAsync Ошибка перезапуска {objectName}: {ex.Message}",
                    "Duplicate key",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"RestartDataByObjectNameAsync Ошибка перезапуска {objectName}: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }


        }

        // Важно: ServiceBroker дергает owner.UpdateDataInFormAsync(...)
        // Если сигнатуры нет/закомментирована — ты НЕ увидишь обновлений.
        public async Task UpdateDataInFormAsync(string table, string changedFieldsCsv = null)
        {
            try
            {
                await _sbController.HandleUpdateAsync(table, changedFieldsCsv ?? string.Empty);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[PlanZagrVyaz] Error in UpdateDataInFormAsync: {ex.Message}");
                await _logger.LogErrorAsync(ex, $"UpdateDataInFormAsync failed for table {table}");
            }
        }

        // На всякий случай: если где-то вызывают упрощённую сигнатуру.
        public Task UpdateDataInFormAsync(string table)
            => UpdateDataInFormAsync(table, changedFieldsCsv: null);
        #endregion

        //private async Task InitializeBindingsAsync()
        //{
        //    try
        //    {
        //        var planTotalHoursByKnitMachineTask = Task.Run(() =>
        //        {
        //            _planTotalHoursByKnitMachineBindingList = new BindingList<PlanTotalHoursByKnitMachine>();
        //            _planTotalHoursByKnitMachineBindingSource = new BindingSource { DataSource = _planTotalHoursByKnitMachineBindingList };
        //        });
        //        var zadanyListTask = Task.Run(() =>
        //        {
        //            _zadanyListBindingList = new BindingList<PZVZadanyList>();
        //            _zadanyListBindingSource = new BindingSource { DataSource = _zadanyListBindingList };
        //        });
        //        var zadanyListNewTask = Task.Run(() =>
        //        {
        //            _zadanyListNewBindingList = new BindingList<PZVZadanyList>();
        //            _zadanyListNewBindingSource = new BindingSource { DataSource = _zadanyListNewBindingList };
        //        });
        //        var rzvPachListByNomTask = Task.Run(() =>
        //        {
        //            _rzvPachListByNomBindingList = new BindingList<RzvPachListByNom>();
        //            _rzvPachListByNomBindingSource = new BindingSource { DataSource = _rzvPachListByNomBindingList };
        //        });
        //        var rzvPachListByNomNewTask = Task.Run(() =>
        //        {
        //            _rzvPachListByNomNewBindingList = new BindingList<RzvPachListByNom>();
        //            _rzvPachListByNomNewBindingSource = new BindingSource { DataSource = _rzvPachListByNomNewBindingList };
        //        });
        //        var pZVOperListByPachListTask = Task.Run(() =>
        //        {
        //            _pZVOperListByPachListBindingList = new BindingList<PZVOperList>();
        //            _pZVOperListByPachListBindingSource = new BindingSource { DataSource = _pZVOperListByPachListBindingList };
        //        });
        //        var pZVOperListByPachListNewTask = Task.Run(() =>
        //        {
        //            _pZVOperListByPachListNewBindingList = new BindingList<PZVOperList>();
        //            _pZVOperListByPachListNewBindingSource = new BindingSource { DataSource = _pZVOperListByPachListNewBindingList };
        //        });
        //        var artNormNTask = Task.Run(() =>
        //        {
        //            _artNormNBindingList = new BindingList<ArtNormN>();
        //            _artNormNBindingSource = new BindingSource { DataSource = _artNormNBindingList };
        //        });
        //        var normRaszTask = Task.Run(() =>
        //        {
        //            _normRaszBindingList = new BindingList<NormRasz>();
        //            _normRaszBindingSource = new BindingSource { DataSource = _normRaszBindingList };
        //        });
        //        var mlOpTask = Task.Run(() =>
        //        {
        //            _mlOpBindingList = new BindingList<MlOp>();
        //            _mlOpBindingSource = new BindingSource { DataSource = _mlOpBindingList };
        //        });
        //        var knitWorkingShiftTask = Task.Run(() =>
        //        {
        //            _knitWorkingShiftSmenBindingList = new BindingList<KnitWorkingShiftSmen>();
        //            _knitWorkingShiftSmenBindingSource = new BindingSource { DataSource = _knitWorkingShiftSmenBindingList };
        //        });
        //        await Task.WhenAll(planTotalHoursByKnitMachineTask, zadanyListTask, zadanyListNewTask
        //                , rzvPachListByNomTask, rzvPachListByNomNewTask
        //                , pZVOperListByPachListTask, pZVOperListByPachListNewTask
        //                , artNormNTask, normRaszTask
        //                , mlOpTask
        //                , knitWorkingShiftTask);

        //        _smenZadanyVyazBindingSource = new BindingSource
        //        {
        //            DataSource = new BindingList<SmenZadanyVyaz>()
        //        };
        //        _smenZadanyVyazNewBindingSource = new BindingSource
        //        {
        //            DataSource = new BindingList<SmenZadanyVyaz>()
        //        };

        //        _planTotalQuantityByArticul = new BindingSource { DataSource = new BindingList<PlanTotalQuantityByArticul>() };
        //        switch (vyazPodrKod)
        //        {
        //            case 1:
        //                //layoutControlGroup7.CustomHeaderButtons[0].Properties.Visible = true;
        //                //layoutControlGroup7.CustomHeaderButtons[1].Properties.Visible = true;
        //                //layoutControlGroup7.CustomHeaderButtons[2].Properties.Visible = true;
        //                _lcgHelper.SetButtonsVisible(
        //                    layoutControlGroup7,
        //                    false,
        //                    "lcg3HideAll",
        //                    "lcg3HideAllSeparator",
        //                    "lcg3ShowAll"
        //                );
        //                #region описание gridControlPlanTotalHoursByKnitMachine "общие часы по вяз машинам/зонам"
        //                layoutControlItemPlanTotalQuantityByArticul.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                layoutControlItemPlanTotalHoursByKnitMachine.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                gridControlPlanTotalHoursByKnitMachine.DataSource = _planTotalHoursByKnitMachineBindingSource;
        //                gridColumnPlanTotalHoursByKnitMachineKmaNumber.FieldName = "kmaNumber";
        //                gridColumnPlanTotalHoursByKnitMachineKmlNumber.FieldName = "kmlNumber";
        //                gridColumnPlanTotalHoursByKnitMachineDateZap.FieldName = "DateZap";
        //                gridColumnPlanTotalHoursByKnitMachineMgZap.FieldName = "mgZap";
        //                gridColumnPlanTotalHoursByKnitMachineHoursTotal.FieldName = "hoursTotal";
        //                gridColumnPlanTotalHoursByKnitMachineIdVyazClass.FieldName = "idVyazClass";
        //                gridColumnPlanTotalHoursByKnitMachineKnitClass.FieldName = "knitClass";
        //                gridColumnPlanTotalHoursByKnitMachineKmlID.FieldName = "kmlID";
        //                #endregion
        //                break;
        //            case 2:
        //            case 3:
        //                layoutControlGroup7.CustomHeaderButtons[0].Properties.Visible = false;
        //                layoutControlGroup7.CustomHeaderButtons[1].Properties.Visible = false;
        //                layoutControlGroup7.CustomHeaderButtons[2].Properties.Visible = false;
        //                #region описание gridViewPlanTotalQuantityByArticul "общее количество по артикулам"
        //                layoutControlItemPlanTotalHoursByKnitMachine.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //                layoutControlItemPlanTotalQuantityByArticul.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //                gridControlPlanTotalQuantityByArticul.DataSource = _planTotalQuantityByArticul;
        //                gridPlanTotalQuantityByArticulColumnKod.FieldName = "Kod";
        //                gridPlanTotalQuantityByArticulColumnArticul.FieldName = "Articul";
        //                gridPlanTotalQuantityByArticulColumnKol.FieldName = "Kol";
        //                gridPlanTotalQuantityByArticulColumnDataPlan.FieldName = "DataPlan";
        //                _gridHelper.AutoRowFilterConfig(gridViewPlanTotalQuantityByArticul as GridView, 0);
        //                #endregion
        //                break;
        //        }



        //        #region описание gridControlZadanyList "задания по номеру машины/артикулу"
        //        gridControlZadanyList.DataSource = _zadanyListBindingSource;
        //        gridZadanyListColumnPszNom.FieldName = "pszNom";
        //        gridZadanyListColumnNom.FieldName = "nom";
        //        gridZadanyListColumnArticul.FieldName = "articul";
        //        gridZadanyListColumnZvet.FieldName = "zvet";
        //        gridZadanyListColumnKol.FieldName = "kol";
        //        gridZadanyListColumnDatePryazZayav.FieldName = "pryazZayav";
        //        gridZadanyListColumnData_plan.FieldName = "data_plan";
        //        gridZadanyListColumnVid_stir.FieldName = "vid_stir";
        //        gridZadanyListColumnDopr_name.FieldName = "dopr_name";
        //        gridZadanyListColumnKmlID.FieldName = "kmlID";
        //        gridZadanyListColumnSyncSelection.FieldName = "SyncSelection";
        //        gridZadanyListColumnGradacia.FieldName = "Gradacia";
        //        gridZadanyListColumnYearPlan.FieldName = "yearPlan";
        //        gridZadanyListColumnKod.FieldName = "kod";
        //        gridZadanyListColumnMinNPach.FieldName = "minNPach";
        //        gridZadanyListColumnMaxNPach.FieldName = "maxNPach";
        //        gridZadanyListColumnBrig.FieldName = "brig";
        //        switch (vyazPodrKod)
        //        {
        //            case 1:
        //                gridZadanyListColumnSyncSelection.Visible = true;
        //                gridZadanyListColumnSyncSelection.VisibleIndex = 0;
        //                gridZadanyListColumnNom.Visible = true;
        //                gridZadanyListColumnNom.VisibleIndex = 1;
        //                gridZadanyListColumnArticul.Visible = true;
        //                gridZadanyListColumnArticul.VisibleIndex = 2;
        //                gridZadanyListColumnZvet.Visible = true;
        //                gridZadanyListColumnZvet.VisibleIndex = 3;
        //                gridZadanyListColumnKol.Visible = true;
        //                gridZadanyListColumnKol.VisibleIndex = 4;
        //                gridZadanyListColumnDatePryazZayav.Visible = true;
        //                gridZadanyListColumnDatePryazZayav.VisibleIndex = 5;
        //                gridZadanyListColumnData_plan.Visible = true;
        //                gridZadanyListColumnData_plan.VisibleIndex = 6;
        //                gridZadanyListColumnPszNom.Visible = true;
        //                gridZadanyListColumnPszNom.VisibleIndex = 7;
        //                gridZadanyListColumnVid_stir.Visible = true;
        //                gridZadanyListColumnVid_stir.VisibleIndex = 8;
        //                gridZadanyListColumnDopr_name.Visible = true;
        //                gridZadanyListColumnDopr_name.VisibleIndex = 9;
        //                gridZadanyListColumnGradacia.Visible = true;
        //                gridZadanyListColumnGradacia.VisibleIndex = 10;

        //                gridZadanyListColumnMinNPach.Visible = false;
        //                gridZadanyListColumnMaxNPach.Visible = false;
        //                gridZadanyListColumnBrig.Visible = false;
        //                break;
        //            case 2:
        //            case 3:
        //                gridZadanyListColumnPszNom.Visible = true;
        //                gridZadanyListColumnPszNom.VisibleIndex = 0;
        //                gridZadanyListColumnMinNPach.Visible = true;
        //                gridZadanyListColumnMinNPach.VisibleIndex = 1;
        //                gridZadanyListColumnMaxNPach.Visible = true;
        //                gridZadanyListColumnMaxNPach.VisibleIndex = 2;
        //                gridZadanyListColumnKol.Visible = true;
        //                gridZadanyListColumnKol.VisibleIndex = 3;
        //                gridZadanyListColumnZvet.Visible = true;
        //                gridZadanyListColumnZvet.VisibleIndex = 4;
        //                gridZadanyListColumnData_plan.Visible = true;
        //                gridZadanyListColumnData_plan.VisibleIndex = 5;
        //                gridZadanyListColumnBrig.Visible = true;
        //                gridZadanyListColumnBrig.VisibleIndex = 6;
        //                gridZadanyListColumnSyncSelection.Visible = true;
        //                gridZadanyListColumnSyncSelection.VisibleIndex = 7;

        //                gridZadanyListColumnNom.Visible = false;
        //                gridZadanyListColumnArticul.Visible = false;
        //                gridZadanyListColumnDatePryazZayav.Visible = false;
        //                gridZadanyListColumnVid_stir.Visible = false;
        //                gridZadanyListColumnDopr_name.Visible = false;
        //                gridZadanyListColumnGradacia.Visible = false;
        //                break;
        //        }
        //        gridZadanyListColumnData_plan.DisplayFormat.FormatType = FormatType.DateTime;
        //        gridZadanyListColumnData_plan.DisplayFormat.FormatString = "dd.MM.yy";
        //        _gridHelper.AutoRowFilterConfig(gridViewZadanyList as GridView, 0);
        //        gridViewZadanyList.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
        //        //switch (vyazPodrKod)
        //        //{
        //        //    case 1:

        //        //        break;
        //        //    case 2: case 3:

        //        //        break;
        //        //}

        //        //----------------------------------------
        //        gridViewZadanyList.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDown;
        //        gridZadanyListColumnGradacia.OptionsColumn.AllowEdit = false;
        //        gridZadanyListColumnGradacia.OptionsColumn.ReadOnly = false;

        //        gridViewZadanyList.OptionsBehavior.Editable = true;
        //        gridViewZadanyList.OptionsBehavior.ReadOnly = false;

        //        //gridZadanyListColumnSyncSelection.OptionsColumn.AllowEdit = true;
        //        //gridZadanyListColumnSyncSelection.OptionsColumn.ReadOnly = false;
        //        //gridZadanyListColumnSyncSelection.ColumnEdit = repositoryItemCheckEdit1;
        //        //MessageBox.Show($"gridZadanyListColumnSyncSelection.ColumnEdit = {gridZadanyListColumnSyncSelection.ColumnEdit}");
        //        //gridZadanyListColumnSyncSelection.OptionsColumn.AllowFocus = true;

        //        //MessageBox.Show(
        //        //    $"AllowEdit={gridZadanyListColumnSyncSelection.OptionsColumn.AllowEdit}\n" +
        //        //    $"ReadOnly={gridZadanyListColumnSyncSelection.OptionsColumn.ReadOnly}\n" +
        //        //    $"ColumnEdit={(gridZadanyListColumnSyncSelection.ColumnEdit == null ? "null" : gridZadanyListColumnSyncSelection.ColumnEdit.Name)}"
        //        //);
        //        //----------------------------------------
        //        //repositoryItemCheckEdit1.MouseUp += (s, e) =>
        //        //{
        //        //    BeginInvoke(new Action(() => SyncSelectionUpdate()));
        //        //};
        //        //----------------------------------------
        //        #endregion

        //        #region описание gridControlRzvPachListByNom "пачки по расчету вяз"
        //        gridControlRzvPachListByNom.DataSource = _rzvPachListByNomBindingSource;
        //        gridRzvPachListByNomColumnNomZad.FieldName = "nomZad";
        //        gridRzvPachListByNomColumnNom.FieldName = "nom";
        //        gridRzvPachListByNomColumnNom_n.FieldName = "nom_n";
        //        gridRzvPachListByNomColumnN_pach.FieldName = "n_pach";
        //        gridRzvPachListByNomColumnRazm.FieldName = "razm";
        //        gridRzvPachListByNomColumnKol.FieldName = "kol";
        //        gridRzvPachListByNomColumnGradacia.FieldName = "gradacia";
        //        gridRzvPachListByNomColumnSyncSelection.FieldName = "SyncSelection";

        //        _gridHelper.AutoRowFilterConfig(gridViewRzvPachListByNom as GridView, 0);
        //        //gridViewRzvPachListByNom.BestFitColumns();

        //        switch (vyazPodrKod)
        //        {
        //            case 1:
        //                gridRzvPachListByNomColumnGradacia.Visible = true;
        //                gridRzvPachListByNomColumnGradacia.VisibleIndex = 0;
        //                gridRzvPachListByNomColumnN_pach.Visible = true;
        //                gridRzvPachListByNomColumnN_pach.VisibleIndex = 1;
        //                gridRzvPachListByNomColumnRazm.Visible = true;
        //                gridRzvPachListByNomColumnRazm.VisibleIndex = 2;
        //                gridRzvPachListByNomColumnKol.Visible = true;
        //                gridRzvPachListByNomColumnKol.VisibleIndex = 3;
        //                gridRzvPachListByNomColumnSyncSelection.Visible = true;
        //                gridRzvPachListByNomColumnSyncSelection.VisibleIndex = 4;

        //                //gridRzvPachListByNomColumnNomZad.Visible = false;
        //                //gridRzvPachListByNomColumnNom.Visible = false;
        //                //gridRzvPachListByNomColumnNom_n.Visible = false;
        //                gridRzvPachListByNomColumnSerialNumber.Visible = false;
        //                break;
        //            case 2:
        //            case 3:
        //                gridRzvPachListByNomColumnSerialNumber.Visible = true;
        //                gridRzvPachListByNomColumnSerialNumber.VisibleIndex = 0;
        //                gridRzvPachListByNomColumnSerialNumber.Width = 20;
        //                gridRzvPachListByNomColumnN_pach.Visible = true;
        //                gridRzvPachListByNomColumnN_pach.VisibleIndex = 1;
        //                gridRzvPachListByNomColumnN_pach.Width = 50;
        //                gridRzvPachListByNomColumnRazm.Visible = true;
        //                gridRzvPachListByNomColumnRazm.VisibleIndex = 2;
        //                gridRzvPachListByNomColumnKol.Visible = true;
        //                gridRzvPachListByNomColumnKol.VisibleIndex = 3;
        //                gridRzvPachListByNomColumnKol.Width = 40;
        //                gridRzvPachListByNomColumnSyncSelection.Visible = true;
        //                gridRzvPachListByNomColumnSyncSelection.VisibleIndex = 4;
        //                gridRzvPachListByNomColumnSyncSelection.Width = 20;

        //                gridRzvPachListByNomColumnGradacia.Visible = false;

        //                //gridRzvPachListByNomColumnNomZad.Visible = false;
        //                //gridRzvPachListByNomColumnNom.Visible = false;
        //                //gridRzvPachListByNomColumnNom_n.Visible = false;



        //                break;
        //        }

        //        #endregion

        //        _smenZadanyVyazBindingSource = new BindingSource
        //        {
        //            DataSource = new BindingList<SmenZadanyVyaz>()
        //        };
        //        _smenZadanyVyazNewBindingSource = new BindingSource
        //        {
        //            DataSource = new BindingList<SmenZadanyVyaz>()
        //        };
        //        #region gridControlSmenZadany "сменное задание"
        //        gridControlSmenZadany.DataSource = _smenZadanyVyazBindingSource;
        //        bandedGridSmenZadanyColumnKmaID.FieldName = "kmaID";
        //        bandedGridSmenZadanyColumnKmaNumber.FieldName = "kmaNumber";
        //        //bandedGridSmenZadanyColumnKmlID.FieldName = "kmlID";
        //        bandedGridSmenZadanyColumnKmlID.FieldName = "kwsmlKmlID";
        //        bandedGridSmenZadanyColumnKmlNumber.FieldName = "kmlNumber";
        //        bandedGridSmenZadanyColumnIDVyazClass.FieldName = "idVyazClass";
        //        bandedGridSmenZadanyColumnNameVyazClass.FieldName = "nameVyazClass";
        //        bandedGridSmenZadanyColumnSmenLength.FieldName = "smenLength";
        //        //bandedGridSmenZadanyColumnChasNaznZad.FieldName = "chasNaznZad";
        //        //bandedGridSmenZadanyColumnChasNaznZadGroup.FieldName = "chasNaznZadGroup";
        //        advBandedGridViewSmenZadany.Columns["chasNaznZadGroup"].UnboundType = UnboundColumnType.Decimal;
        //        bandedGridSmenZadanyColumnChasNotConfirmedZad.FieldName = "chasNotConfirmedZad";
        //        bandedGridSmenZadanyColumnChasNotConfirmedZadGroup.FieldName = "chasNotConfirmedZadGroup";
        //        bandedGridSmenZadanyColumnChasRemainZad.FieldName = "chasRemainZad";
        //        bandedGridSmenZadanyColumnChasRemainZadGroup.FieldName = "chasRemainZadGroup";
        //        bandedGridSmenZadanyColumnShiftsRemainZad.FieldName = "shiftsRemainZad";
        //        bandedGridSmenZadanyColumnShiftsRemainZadGroup.FieldName = "shiftsRemainZadGroup";
        //        bandedGridSmenZadanyColumnChasNaznSmen.FieldName = "chasNaznSmen";
        //        bandedGridSmenZadanyColumnChasNaznSmenGroup.FieldName = "chasNaznSmenGroup";
        //        bandedGridSmenZadanyColumnChasNaznSmenProc.FieldName = "chasNaznSmenProc";
        //        bandedGridSmenZadanyColumnChasNaznSmenProcGroup.FieldName = "chasNaznSmenProcGroup";
        //        bandedGridSmenZadanyColumnChasInWorkSmen.FieldName = "chasInWorkSmen";
        //        bandedGridSmenZadanyColumnChasInWorkSmenGroup.FieldName = "chasInWorkSmenGroup";
        //        bandedGridSmenZadanyColumnChasDoneSmen.FieldName = "chasDoneSmen";
        //        bandedGridSmenZadanyColumnChasDoneSmenGroup.FieldName = "chasDoneSmenGroup";
        //        bandedGridSmenZadanyColumnChasDoneSmenProc.FieldName = "chasDoneSmenProc";
        //        bandedGridSmenZadanyColumnChasDoneSmenProcGroup.FieldName = "chasDoneSmenProcGroup";
        //        bandedGridSmenZadanyColumnChasRemainSmen.FieldName = "chasRemainSmen";
        //        bandedGridSmenZadanyColumnChasRemainSmenGroup.FieldName = "chasRemainSmenGroup";
        //        bandedGridSmenZadanyColumnChasConfirmedSmen.FieldName = "chasConfirmedSmen";
        //        bandedGridSmenZadanyColumnChasConfirmedSmenGroup.FieldName = "chasConfirmedSmenGroup";
        //        bandedGridSmenZadanyColumnTypeID.FieldName = "typeID";
        //        bandedGridSmenZadanyColumnTypeName.FieldName = "typeName";
        //        bandedGridSmenZadanyColumnFio.FieldName = "fio";
        //        bandedGridSmenZadanyColumnKwsID.FieldName = "kwsID";

        //        bandedGridSmenZadanyColumnKmaID.OptionsColumn.AllowEdit = false;
        //        bandedGridSmenZadanyColumnKmaNumber.OptionsColumn.AllowEdit = false;
        //        bandedGridSmenZadanyColumnKmlID.OptionsColumn.AllowEdit = false;
        //        bandedGridSmenZadanyColumnKmlNumber.OptionsColumn.AllowEdit = false;
        //        bandedGridSmenZadanyColumnIDVyazClass.OptionsColumn.AllowEdit = false;
        //        bandedGridSmenZadanyColumnNameVyazClass.OptionsColumn.AllowEdit = false;
        //        bandedGridSmenZadanyColumnSmenLength.OptionsColumn.AllowEdit = false;
        //        bandedGridSmenZadanyColumnChasNaznZad.OptionsColumn.AllowEdit = false;
        //        bandedGridSmenZadanyColumnChasNotConfirmedZad.OptionsColumn.AllowEdit = false;
        //        bandedGridSmenZadanyColumnChasRemainZad.OptionsColumn.AllowEdit = false;
        //        bandedGridSmenZadanyColumnShiftsRemainZad.OptionsColumn.AllowEdit = false;
        //        bandedGridSmenZadanyColumnChasNaznSmen.OptionsColumn.AllowEdit = false;
        //        bandedGridSmenZadanyColumnChasNaznSmenProc.OptionsColumn.AllowEdit = false;
        //        bandedGridSmenZadanyColumnChasInWorkSmen.OptionsColumn.AllowEdit = false;
        //        bandedGridSmenZadanyColumnChasDoneSmen.OptionsColumn.AllowEdit = false;
        //        bandedGridSmenZadanyColumnChasDoneSmenProc.OptionsColumn.AllowEdit = false;
        //        bandedGridSmenZadanyColumnChasRemainSmen.OptionsColumn.AllowEdit = false;
        //        bandedGridSmenZadanyColumnChasConfirmedSmen.OptionsColumn.AllowEdit = false;
        //        bandedGridSmenZadanyColumnTypeID.OptionsColumn.AllowEdit = false;
        //        bandedGridSmenZadanyColumnTypeName.OptionsColumn.AllowEdit = false;
        //        bandedGridSmenZadanyColumnFio.OptionsColumn.AllowEdit = false;
        //        bandedGridSmenZadanyColumnKwsID.OptionsColumn.AllowEdit = false;

        //        advBandedGridViewSmenZadany.OptionsView.ShowColumnHeaders = false;
        //        advBandedGridViewSmenZadany.OptionsView.ShowGroupPanel = false;
        //        bandedGridSmenZadanyColumnChasNaznZad.DisplayFormat.FormatType = FormatType.Numeric;
        //        bandedGridSmenZadanyColumnChasNaznZad.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
        //        bandedGridSmenZadanyColumnChasNotConfirmedZad.DisplayFormat.FormatType = FormatType.Numeric;
        //        bandedGridSmenZadanyColumnChasNotConfirmedZad.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
        //        bandedGridSmenZadanyColumnChasRemainZad.DisplayFormat.FormatType = FormatType.Numeric;
        //        bandedGridSmenZadanyColumnChasRemainZad.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
        //        bandedGridSmenZadanyColumnShiftsRemainZad.DisplayFormat.FormatType = FormatType.Numeric;
        //        bandedGridSmenZadanyColumnShiftsRemainZad.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
        //        bandedGridSmenZadanyColumnChasNaznSmen.DisplayFormat.FormatType = FormatType.Numeric;
        //        bandedGridSmenZadanyColumnChasNaznSmen.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
        //        bandedGridSmenZadanyColumnChasNaznSmenProc.DisplayFormat.FormatType = FormatType.Numeric;
        //        bandedGridSmenZadanyColumnChasNaznSmenProc.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
        //        bandedGridSmenZadanyColumnChasInWorkSmen.DisplayFormat.FormatType = FormatType.Numeric;
        //        bandedGridSmenZadanyColumnChasInWorkSmen.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
        //        bandedGridSmenZadanyColumnChasDoneSmen.DisplayFormat.FormatType = FormatType.Numeric;
        //        bandedGridSmenZadanyColumnChasDoneSmen.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
        //        bandedGridSmenZadanyColumnChasDoneSmenProc.DisplayFormat.FormatType = FormatType.Numeric;
        //        bandedGridSmenZadanyColumnChasDoneSmenProc.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
        //        bandedGridSmenZadanyColumnChasRemainSmen.DisplayFormat.FormatType = FormatType.Numeric;
        //        bandedGridSmenZadanyColumnChasRemainSmen.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
        //        bandedGridSmenZadanyColumnChasConfirmedSmen.DisplayFormat.FormatType = FormatType.Numeric;
        //        bandedGridSmenZadanyColumnChasConfirmedSmen.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";

        //        string _xNumFormat = "{0:0.00;-0.00;}";
        //        foreach (GridGroupSummaryItem gsi in advBandedGridViewSmenZadany.GroupSummary)
        //        {
        //            gsi.DisplayFormat = _xNumFormat;
        //        }

        //        _gridHelper.AutoRowFilterConfig(advBandedGridViewSmenZadany, 0);
        //        advBandedGridViewSmenZadany.OptionsView.GroupFooterShowMode = GroupFooterShowMode.Hidden;
        //        _gridHelper.EnableGroupSummariesInGroupRow(advBandedGridViewSmenZadany, GroupSummaryLevelMode.IncludeOnly, new[] { 2 });


        //        gridSmenZadanyOtpColumnSzFio.FieldName = "szFio";
        //        gridSmenZadanyOtpColumnSzDolgn.FieldName = "szDolgn";
        //        gridSmenZadanyOtpColumnSzTab.FieldName = "szTab";
        //        gridSmenZadanyOtpColumnSzKoefVNV.FieldName = "szKoefVNV";
        //        gridSmenZadanyOtpColumnSzPlanHours.FieldName = "szPlanHours";
        //        gridSmenZadanyOtpColumnSzNaznHours.FieldName = "szNaznHours";
        //        gridSmenZadanyOtpColumnSzPlanNaznPercent.FieldName = "szPlanNaznPercent";
        //        gridSmenZadanyOtpColumnSzHoursToDo.FieldName = "szHoursToDo";
        //        gridSmenZadanyOtpColumnSzHoursDone.FieldName = "szHoursDone";
        //        gridSmenZadanyOtpColumnSzShiftVNV.FieldName = "szShiftVNV";
        //        gridSmenZadanyOtpColumnSzDTab.FieldName = "szDTab";
        //        //_gridHelper.AutoRowFilterConfig(gridViewSmenZadanyOtp, 0);

        //        switch (vyazPodrKod)
        //        {
        //            case 1:
        //                gridControlSmenZadany.MainView = advBandedGridViewSmenZadany;
        //                break;
        //            case 2:
        //            case 3:
        //                gridControlSmenZadany.MainView = gridViewSmenZadanyOtp;
        //                _lcgHelper.SetButtonsVisible(
        //                    layoutControlGroup2,
        //                    false,
        //                    "lcg2HideAll",
        //                    "lcg2HideAllSeparator",
        //                    "lcg2ShowAll",
        //                    "lcg2ShowAllSeparator",
        //                    "lcg2ShowFIO",
        //                    "lcg2ShowFIOSeparator",
        //                    "lcg2ShowMH"
        //                );
        //                //layoutControlGroup2.CustomHeaderButtons[5].Properties.Visible = false;
        //                //layoutControlGroup2.CustomHeaderButtons[6].Properties.Visible = false;
        //                //layoutControlGroup2.CustomHeaderButtons[7].Properties.Visible = false;
        //                //layoutControlGroup2.CustomHeaderButtons[8].Properties.Visible = false;
        //                //layoutControlGroup2.CustomHeaderButtons[9].Properties.Visible = false;
        //                //layoutControlGroup2.CustomHeaderButtons[10].Properties.Visible = false;
        //                //layoutControlGroup2.CustomHeaderButtons[11].Properties.Visible = false;
        //                //layoutControlGroup2.CustomHeaderButtons[12].Properties.Visible = false;
        //                break;
        //        }


        //        //// перенос текста
        //        //advBandedGridViewSmenZadany.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
        //        //advBandedGridViewSmenZadany.Appearance.BandPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

        //        //// авто-высота заголовков (колонки и бэнды)
        //        //advBandedGridViewSmenZadany.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
        //        //// высота строки заголовков бэндов 
        //        //advBandedGridViewSmenZadany.BandPanelRowHeight = 37;
        //        //TODO : если понадобится, можно будет донастроить высоту строк заголовков колонок, но пока не нужно
        //        #endregion

        //        _lcgHelper.SetButtonsVisible(
        //                    layoutControlGroup3,
        //                    false,
        //                    "lcg3HideAll",
        //                    "lcg3HideAllSeparator",
        //                    "lcg3ShowAll"
        //                );
        //        _naryadZadanyVyazBindingSource = new BindingSource { DataSource = new BindingList<NaryadZadanyVyaz>() };
        //        #region gridControlNaryadZadany "наряд-задание"
        //        gridControlNaryadZadany.DataSource = _naryadZadanyVyazBindingSource;
        //        gridNaryadZadanyColumnKmlNumber.FieldName = "kmlNumber";
        //        gridNaryadZadanyColumnPzvArticul.FieldName = "pzvArticul";
        //        gridNaryadZadanyColumnPzvNomZad.FieldName = "pzvNomZad";
        //        gridNaryadZadanyColumnPzvNom.FieldName = "pzvNom";
        //        gridNaryadZadanyColumnNPach.FieldName = "nPach";
        //        gridNaryadZadanyColumnNomOper.FieldName = "nomOper";
        //        gridNaryadZadanyColumnText.FieldName = "text";
        //        gridNaryadZadanyColumnRazryd.FieldName = "razryd";
        //        gridNaryadZadanyColumnSekObServ.FieldName = "sekObServ";
        //        gridNaryadZadanyColumnHoursTotalPlan.FieldName = "hoursTotalPlan";
        //        gridNaryadZadanyColumnHoursTotalFact.FieldName = "hoursTotalFact";
        //        gridNaryadZadanyColumnPzvKol.FieldName = "pzvKol";
        //        gridNaryadZadanyColumnStatusName.FieldName = "statusName";
        //        gridNaryadZadanyColumnStatusDate.FieldName = "statusDate";
        //        gridNaryadZadanyColumnPzvTab.FieldName = "pzvTab";
        //        gridNaryadZadanyColumnYearPach.FieldName = "yearPach";
        //        gridNaryadZadanyColumnPachKod.FieldName = "pachKod";


        //        gridNaryadZadanyColumnKmlNumber.OptionsColumn.AllowEdit = false;
        //        gridNaryadZadanyColumnPzvArticul.OptionsColumn.AllowEdit = false;
        //        gridNaryadZadanyColumnPzvNomZad.OptionsColumn.AllowEdit = false;
        //        gridNaryadZadanyColumnPzvNom.OptionsColumn.AllowEdit = false;
        //        gridNaryadZadanyColumnNPach.OptionsColumn.AllowEdit = false;
        //        gridNaryadZadanyColumnNomOper.OptionsColumn.AllowEdit = false;
        //        gridNaryadZadanyColumnText.OptionsColumn.AllowEdit = false;
        //        gridNaryadZadanyColumnRazryd.OptionsColumn.AllowEdit = false;
        //        gridNaryadZadanyColumnSekObServ.OptionsColumn.AllowEdit = false;
        //        gridNaryadZadanyColumnHoursTotalPlan.OptionsColumn.AllowEdit = false;
        //        gridNaryadZadanyColumnHoursTotalFact.OptionsColumn.AllowEdit = false;
        //        gridNaryadZadanyColumnPzvKol.OptionsColumn.AllowEdit = false;
        //        gridNaryadZadanyColumnStatusName.OptionsColumn.AllowEdit = false;
        //        gridNaryadZadanyColumnStatusDate.OptionsColumn.AllowEdit = false;
        //        gridNaryadZadanyColumnPzvTab.OptionsColumn.AllowEdit = false;

        //        gridViewNaryadZadany.OptionsView.ShowGroupPanel = false;
        //        if (vyazPodrKod == 2 || vyazPodrKod == 3)
        //        {
        //            gridViewNaryadZadany.ClearGrouping();
        //            gridNaryadZadanyColumnKmlNumber.Visible = false;
        //            gridNaryadZadanyColumnPzvNom.Visible = false;
        //            gridNaryadZadanyColumnPzvTab.Visible = false;
        //        }
        //        gridNaryadZadanyColumnSekObServ.DisplayFormat.FormatType = FormatType.Numeric;
        //        gridNaryadZadanyColumnSekObServ.DisplayFormat.FormatString = "#,0.00;-#,0.00;";

        //        gridNaryadZadanyColumnHoursTotalPlan.DisplayFormat.FormatType = FormatType.Numeric;
        //        gridNaryadZadanyColumnHoursTotalPlan.DisplayFormat.FormatString = "#,0.00;-#,0.00;";

        //        gridNaryadZadanyColumnHoursTotalFact.DisplayFormat.FormatType = FormatType.Numeric;
        //        gridNaryadZadanyColumnHoursTotalFact.DisplayFormat.FormatString = "#,0.00;-#,0.00;";

        //        gridNaryadZadanyColumnStatusDate.DisplayFormat.FormatType = FormatType.DateTime;
        //        gridNaryadZadanyColumnStatusDate.DisplayFormat.FormatString = "dd.MM.yy";

        //        _gridHelper.AutoRowFilterConfig(gridViewNaryadZadany, 0);

        //        #endregion

        //        #region описание gridControlArtNormN "заголовок РТ"
        //            gridControlArtNormN.DataSource = _artNormNBindingSource;
        //        gridArtNormNColumnAnnID.FieldName = "annID";
        //        gridArtNormNColumnGrup.FieldName = "grup";
        //        gridArtNormNColumnArticul.FieldName = "Articul";
        //        gridArtNormNColumnMod.FieldName = "Mod";
        //        gridArtNormNColumnDataObn.FieldName = "dateUpdate";
        //        gridArtNormNColumnAnnDateDel.FieldName = "dateDel";
        //        gridArtNormNColumnAnnCompDel.FieldName = "compDel";
        //        gridArtNormNColumnStatus.FieldName = "StatusText";
        //        #endregion

        //        #region описание gridControlNormRasz "строка по операции в РТ"
        //        gridControlNormRasz.DataSource = _normRaszBindingSource;
        //        gridNormRaszColumnNrID.FieldName = "nrID";
        //        gridNormRaszColumnDisplayNumber.FieldName = "DisplayNumber";
        //        gridNormRaszColumnText.FieldName = "Text";
        //        gridNormRaszColumnRazryd.FieldName = "razryd";
        //        gridNormRaszColumnSek.FieldName = "Sek";
        //        gridNormRaszColumnKodOb.FieldName = "KodOb";
        //        gridNormRaszColumnObor.FieldName = "TextOb";
        //        gridNormRaszColumnKodPodr.FieldName = "TextVyaz";
        //        gridNormRaszColumnKodProizv.FieldName = "TextProizv";
        //        gridNormRaszColumnNrDateDel.FieldName = "nrDateDel";
        //        gridNormRaszColumnNrCompDel.FieldName = "nrCompDel";
        //        #endregion

        //        #region описание gridControlMlOp "строка по операции в МЛ"
        //        gridControlMlOp.DataSource = _mlOpBindingSource;
        //        gridMlOpColumnID.FieldName = "id";
        //        gridMlOpColumnFromPzt.FieldName = "fromPzt";
        //        gridMlOpColumnDateAdd.FieldName = "date_add";
        //        gridMlOpColumnVidPr.FieldName = "vidPr";
        //        gridMlOpColumnNomR.FieldName = "nomR";
        //        gridMlOpColumnMgPach.FieldName = "mg_pach";
        //        gridMlOpColumnMgNez.FieldName = "mg_nez";
        //        gridMlOpColumnMaster.FieldName = "master";
        //        gridMlOpColumnOperationNumber.FieldName = "OperationNumber";
        //        gridMlOpColumnText.FieldName = "text";
        //        gridMlOpColumnKol.FieldName = "kol";
        //        gridMlOpColumnTab.FieldName = "tab";
        //        gridMlOpColumnFio.FieldName = "fio";
        //        gridMlOpColumnDataR.FieldName = "data_r";
        //        #endregion

        //        #region описание gridControlPZVOperList
        //        gridControlPZVOperList.DataSource = _pZVOperListByPachListBindingSource;
        //        gridPZVOperListColumnOlPzvID.FieldName = "olPzvID";
        //        gridPZVOperListColumnOlPzvIDParent.FieldName = "olPzvIDParent";
        //        gridPZVOperListColumnOlPzvDivision.FieldName = "olPzvDivision";
        //        gridPZVOperListColumnOlPzvIDMlOp.FieldName = "olPzvIDMlOp";
        //        gridPZVOperListColumnOlNom.FieldName = "olNom";
        //        gridPZVOperListColumnOlNomN.FieldName = "olNomN";
        //        gridPZVOperListColumnOlNomZad.FieldName = "olNomZad";
        //        gridPZVOperListColumnOlPzvAnnID.FieldName = "olPzvAnnID";
        //        gridPZVOperListColumnOlPzvNrID.FieldName = "olPzvNrID";
        //        gridPZVOperListColumnOlPzvIdBrig.FieldName = "olPzvIdBrig";
        //        gridPZVOperListColumnOlPzvKmlID.FieldName = "olPzvKmlID";
        //        gridPZVOperListColumnOlPzvArticul.FieldName = "olPzvArticul";
        //        gridPZVOperListColumnOlPzvMod.FieldName = "olPzvMod";
        //        gridPZVOperListColumnOlNPach.FieldName = "olNPach";
        //        //pach_kod
        //        //kod
        //        gridPZVOperListColumnOlNo.FieldName = "olNo";
        //        gridPZVOperListColumnOlNpo.FieldName = "olNpo";
        //        gridPZVOperListColumnOlNomOper.FieldName = "olNomOper";
        //        gridPZVOperListColumnOlOperName.FieldName = "olOperName";
        //        gridPZVOperListColumnOlKodOb.FieldName = "olKodOb";
        //        gridPZVOperListColumnOlOborudClass.FieldName = "olOborudClass";
        //        gridPZVOperListColumnOlRazryd.FieldName = "olRazryd";
        //        gridPZVOperListColumnOlSekEd.FieldName = "olSekEd";
        //        gridPZVOperListColumnOlKol.FieldName = "olKol";
        //        gridPZVOperListColumnOlPzvNChasi.FieldName = "olPzvNChasi";
        //        gridPZVOperListColumnOlKmlNumber.FieldName = "olKmlNumber";
        //        gridPZVOperListColumnOlPvDateNaznKm.FieldName = "olPzvDateNaznKm";
        //        gridPZVOperListColumnOlPzvTab.FieldName = "olPzvTab";
        //        gridPZVOperListColumnOlPzvDateNaznTab.FieldName = "olPzvDateNaznTab";
        //        gridPZVOperListColumnOlPzvDateStart.FieldName = "olPzvDateStart";
        //        gridPZVOperListColumnOlPzvDateEnd.FieldName = "olPzvDateEnd";
        //        gridPZVOperListColumnOlPzvDateML.FieldName = "olPzvDateML";
        //        gridPZVOperListColumnOlPzvDateMast.FieldName = "olPzvDateMast";
        //        gridPZVOperListColumnSyncSelection.FieldName = "SyncSelection";
        //        gridPZVOperListColumnOlPzvGradacia.FieldName = "olPzvGradacia";
        //        gridPZVOperListColumnOlDefect.FieldName = "olDefect";
        //        gridPZVOperListColumnOlTextObS.FieldName = "olTextObS";
        //        gridPZVOperListColumnOlDolgn.FieldName = "olDolgn";

        //        switch (vyazPodrKod)
        //        {
        //            case 1:
        //                gridPZVOperListColumnOlDefect.Visible = false;
        //                gridPZVOperListColumnOlTextObS.Visible = false;
        //                gridPZVOperListColumnOlDolgn.Visible = false;
        //                break;
        //            case 2:
        //            case 3:
        //                gridPZVOperListColumnOlOborudClass.Visible = false;
        //                gridPZVOperListColumnOlPzvGradacia.Visible = false;
        //                gridPZVOperListColumnOlGsName.Visible = false;
        //                gridPZVOperListColumnOlKmlNumber.Visible = false;
        //                gridPZVOperListColumnOlPvDateNaznKm.Visible = false;
        //                break;
        //        }

        //        gridPZVOperListColumnOlPzvNChasi.DisplayFormat.FormatType = FormatType.Numeric;
        //        //gridColumnPZVOperListOlNChasi.DisplayFormat.FormatString = "#,0.00;-#,0.00;''";
        //        gridPZVOperListColumnOlPzvNChasi.DisplayFormat.FormatString = "#,0.00;-#,0.00;";
        //        gridPZVOperListColumnOlPvDateNaznKm.DisplayFormat.FormatType = FormatType.DateTime;
        //        gridPZVOperListColumnOlPvDateNaznKm.DisplayFormat.FormatString = "dd.MM.yy";
        //        gridPZVOperListColumnOlPzvDateNaznTab.DisplayFormat.FormatType = FormatType.DateTime;
        //        gridPZVOperListColumnOlPzvDateNaznTab.DisplayFormat.FormatString = "dd.MM.yy";
        //        gridPZVOperListColumnOlPzvDateStart.DisplayFormat.FormatType = FormatType.DateTime;
        //        gridPZVOperListColumnOlPzvDateStart.DisplayFormat.FormatString = "dd.MM.yy";
        //        gridPZVOperListColumnOlPzvDateEnd.DisplayFormat.FormatType = FormatType.DateTime;
        //        gridPZVOperListColumnOlPzvDateEnd.DisplayFormat.FormatString = "dd.MM.yy";
        //        gridPZVOperListColumnOlPzvDateML.DisplayFormat.FormatType = FormatType.DateTime;
        //        gridPZVOperListColumnOlPzvDateML.DisplayFormat.FormatString = "dd.MM.yy";
        //        gridPZVOperListColumnOlPzvDateMast.DisplayFormat.FormatType = FormatType.DateTime;
        //        gridPZVOperListColumnOlPzvDateMast.DisplayFormat.FormatString = "dd.MM.yy";

        //        gridPZVOperListColumnOlKmlNumber.OptionsColumn.AllowEdit = false;
        //        gridPZVOperListColumnOlPvDateNaznKm.OptionsColumn.AllowEdit = false;
        //        gridPZVOperListColumnOlPzvTab.OptionsColumn.AllowEdit = false;
        //        gridPZVOperListColumnOlPzvDateNaznTab.OptionsColumn.AllowEdit = false;
        //        gridPZVOperListColumnOlPzvDateStart.OptionsColumn.AllowEdit = false;
        //        gridPZVOperListColumnOlPzvDateEnd.OptionsColumn.AllowEdit = false;
        //        gridPZVOperListColumnOlPzvDateMast.OptionsColumn.AllowEdit = false;
        //        _gridHelper.AutoRowFilterConfig(gridViewPZVOperList as GridView, 0);
        //        gridViewPZVOperList.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
        //        gridPZVOperListColumnOlNPach.OptionsColumn.AllowSort = DefaultBoolean.False;

        //        gridViewPZVOperList.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDown;
        //        gridPZVOperListColumnSyncSelection.OptionsColumn.AllowEdit = true;
        //        gridPZVOperListColumnSyncSelection.OptionsColumn.ReadOnly = false;

        //        //gridViewPZVOperList.DoubleClick += gridViewPZVOperList_DoubleClick;

        //        #endregion

        //        #region описание блока Информация по операции
        //        textBoxOlPzvNom.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olNom), true, DataSourceUpdateMode.Never);
        //        textBoxOlPzvID.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvID), true, DataSourceUpdateMode.Never);
        //        textBoxOlPzvIDMlOp.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvIDMlOp), true, DataSourceUpdateMode.Never);
        //        textBoxOlPzvAnnID.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvAnnID), true, DataSourceUpdateMode.Never);
        //        textBoxOlPzvUpdDate.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvUpdDate), true, DataSourceUpdateMode.Never);
        //        textBoxOlNomOper.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olNomOper), true, DataSourceUpdateMode.Never);
        //        textBoxOlPzvNrID.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvNrID), true, DataSourceUpdateMode.Never);
        //        #endregion

        //        Debug.WriteLine($"[PlanZagrVyaz] InitializeBindingsAsync completed");
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
        //        throw;
        //    }
        //}

        //#region грид GridNaryadZadany popupMenu init
        //private void SetupGridNaryadZadanyEvents()
        //{
        //    // Обработка клика правой кнопкой мыши через MouseDown
        //    gridControlNaryadZadany.MouseDown += GridControlNaryadZadany_MouseDown;
        //    Debug.WriteLine($"[PlanZagrVyaz] SetupGridNaryadZadanyEvents completed");
        //}
        //private void GridControlNaryadZadany_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Right)
        //    {
        //        // Получаем информацию о месте клика
        //        GridHitInfo hit = gridViewNaryadZadany.CalcHitInfo(e.X, e.Y);
        //        string _columnName = hit.Column.FieldName;
        //        Debug.WriteLine($"FocusedColumn = '{hit.Column.Name}'");
        //        // Проверяем, что клик был в ячейке колонки NomZad
        //        if (hit.InRowCell && hit.Column.FieldName == "pzvNomZad" /*|| hit.Column.FieldName == "pzvNom" || hit.Column.FieldName == "nPach"*/)
        //        {
        //            // Получаем значение ячейки
        //            object cellValue = gridViewNaryadZadany.GetRowCellValue(hit.RowHandle, hit.Column);

        //            // Создаем и показываем контекстное меню
        //            ShowContextMenuWithValue(cellValue, e.Location, hit);

        //            // Предотвращаем дальнейшую обработку
        //            // (опционально, если нужно отменить стандартное меню)
        //        }
        //    }
        //    Debug.WriteLine($"[PlanZagrVyaz] GridControlNaryadZadany_MouseDown completed");
        //}

        //private void ShowContextMenuWithValue(object _cellValue, Point _location, GridHitInfo _hit)
        //{
        //    ContextMenuStrip menu = new ContextMenuStrip();

        //    // Формируем текст пункта меню
        //    string displayText = _cellValue?.ToString() ?? "(пусто)";

        //    // Пункт меню с отображением значения
        //    ToolStripMenuItem searchItem = new ToolStripMenuItem($"Поиск в НЗП по {_hit.Column.Caption}: {displayText}");
        //    searchItem.Tag = _cellValue;
        //    searchItem.Click += (s, args) =>
        //    {
        //        if (searchItem.Tag != null)
        //        {
        //            switch (gridViewNaryadZadany.FocusedColumn.FieldName)
        //            {
        //                case "pzvNomZad":
        //                    textBoxPzvNomZadSearch.Text = string.Empty;
        //                    textBoxPzvNomZadSearch.Text = searchItem.Tag.ToString();
        //                    textBoxPzvNomSearch.Text = string.Empty;
        //                    textBoxPzvNomZadSearch.Focus();
        //                    textBoxPzvNomZadSearch.SelectAll();
        //                    break;
        //                case "pzvNom":
        //                    textBoxPzvYearPachSearch.Text = Convert.ToString(gridViewNaryadZadany.GetRowCellValue(_hit.RowHandle, gridNaryadZadanyColumnYearPach));
        //                    textBoxPzvNomSearch.Text = searchItem.Tag.ToString();
        //                    textBoxPzvNomSearch.Focus();
        //                    textBoxPzvNomSearch.SelectAll();
        //                    break;
        //                case "nPach":
        //                    textBoxPzvYearPachSearch.Text = Convert.ToString(gridViewNaryadZadany.GetRowCellValue(_hit.RowHandle, gridNaryadZadanyColumnYearPach));
        //                    textBoxPzvNPachSearch.Text = searchItem.Tag.ToString();
        //                    textBoxPzvNPachSearch.Focus();
        //                    textBoxPzvNPachSearch.SelectAll();
        //                    break;
        //            }

        //        }
        //    };

        //    // Пункт "Копировать"
        //    ToolStripMenuItem copyItem = new ToolStripMenuItem("Копировать");
        //    copyItem.Tag = _cellValue;
        //    copyItem.Click += (s, args) =>
        //    {
        //        if (copyItem.Tag != null)
        //        {
        //            Clipboard.SetText(copyItem.Tag.ToString());
        //        }
        //    };

        //    menu.Items.Add(searchItem);
        //    menu.Items.Add(new ToolStripSeparator());
        //    menu.Items.Add(copyItem);

        //    // Показываем меню
        //    menu.Show(gridControlNaryadZadany, _location);

        //    Debug.WriteLine($"[PlanZagrVyaz] ShowContextMenuWithValue completed");
        //}
        //#endregion

        private void SetGroupExpandState_NoRecursion()
        {
            var view = advBandedGridViewSmenZadany;

            view.BeginUpdate();
            try
            {
                view.ExpandGroupLevel(0);
                view.ExpandGroupLevel(1);

                for (int i = 0; i < view.DataRowCount; i++)
                {
                    int dataRowHandle = view.GetRowHandle(i);
                    int groupHandle = view.GetParentRowHandle(dataRowHandle);

                    if (groupHandle == GridControl.InvalidRowHandle)
                        continue;

                    if (!view.IsGroupRow(groupHandle))
                        continue;

                    if (view.GetRowLevel(groupHandle) != 2)
                        continue;

                    string groupText = view.GetGroupRowValue(groupHandle)?.ToString();

                    view.SetRowExpanded(groupHandle, groupText == "м/ч");
                }
                Debug.WriteLine($"SetGroupExpandState_NoRecursion completed");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка SetGroupExpandState_NoRecursion: {ex.Message}");
            }
            finally
            {
                view.EndUpdate();
            }
        }

        private void ProcessGroups(GridView view, int parentHandle)
        {
            try
            {
                int childCount = view.GetChildRowCount(parentHandle);
                Debug.WriteLine($"Parent {parentHandle}, children = {childCount}");

                for (int i = 0; i < childCount; i++)
                {
                    int childHandle = view.GetChildRowHandle(parentHandle, i);

                    Debug.WriteLine(
                        $"  Handle={childHandle}, " +
                        $"IsGroup={view.IsGroupRow(childHandle)}, " +
                        $"Level={view.GetRowLevel(childHandle)}"
                    );

                    if (!view.IsGroupRow(childHandle))
                        continue;

                    int level = view.GetRowLevel(childHandle);

                    if (level == 2)
                    {
                        string groupText = view.GetGroupRowValue(childHandle)?.ToString();
                        view.SetRowExpanded(childHandle, groupText == "м/ч");
                    }

                    else if (level < 2)
                    {
                        view.ExpandGroupRow(childHandle);
                        ProcessGroups(view, childHandle);
                    }
                }
                Debug.WriteLine($"ProcessGroups completed for parent {parentHandle}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка ProcessGroups: {ex.Message}");
            }
        }
        //private void SyncSelectionUpdate()
        //{
        //    try
        //    {
        //        // обязательно после BeginInvoke: BindingSource уже обновлён
        //        gridViewZadanyList.PostEditor();
        //        gridViewZadanyList.UpdateCurrentRow();

        //        var selectedRow = _zadanyListBindingSource.Current as PZVZadanyList;
        //        if (selectedRow == null)
        //            return;

        //        int isSelected = Convert.ToInt32(
        //            gridViewZadanyList.GetFocusedRowCellValue("SyncSelection"));

        //        var recordsToUpdate = _rzvPachListByNomBindingSource.List
        //            .Cast<RzvPachListByNom>()
        //            .Where(record =>
        //                record != null &&
        //                record.nomZad == selectedRow.pszNom &&
        //                record.nom == selectedRow.nom)
        //            .ToList();

        //        foreach (var record in recordsToUpdate)
        //            record.SyncSelection = isSelected;

        //        _zadanyListBindingSource.ResetBindings(false);
        //        gridViewZadanyList.RefreshData();

        //        _rzvPachListByNomBindingSource.ResetBindings(false);
        //        gridViewRzvPachListByNom.RefreshData();

        //        Debug.WriteLine($"SyncSelectionUpdate completed for NomZad='{selectedRow.pszNom}', Nom='{selectedRow.nom}', IsSelected={isSelected}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка обновления: {ex.Message}");
        //    }
        //}

        //private async Task LoadPlanTotalHoursByKnitMachineDataAsync()
        //{
        //    try
        //    {
        //        _planTotalHoursByKnitMachineBindingSource.Clear();
        //        _planTotalHoursByKnitMachineBindingSource.ResetBindings(false);

        //        planTotalHoursByKnitMachineData = await _vyazService.GetPlanTotalHoursByKnitMachine();

        //        if (planTotalHoursByKnitMachineData != null)
        //        {
        //            await _logger.LogEventAsync($"Получены данные PlanTotalHoursByKnitMachine", "LoadPlanTotalHoursByKnitMachineDataAsync");

        //            await this.InvokeAsync(() =>
        //            {
        //                _planTotalHoursByKnitMachineBindingSource.DataSource = planTotalHoursByKnitMachineData;
        //            });

        //            await _logger.LogEventAsync($"Данные PlanTotalHoursByKnitMachine успешно загружены", "LoadPlanTotalHoursByKnitMachineDataAsync");
        //            _planTotalHoursByKnitMachineBindingList.Add(planTotalHoursByKnitMachineData[0]);
        //            _planTotalHoursByKnitMachineBindingSource.ResetBindings(false);
        //            gridViewPlanTotalHoursByKnitMachine.ExpandAllGroups();
        //        }
        //        else
        //        {
        //            await _logger.LogEventAsync($"Не удалось найти данные PlanTotalHoursByKnitMachine", "LoadPlanTotalHoursByKnitMachineDataAsync");
        //        }
        //        Debug.WriteLine($"LoadPlanTotalHoursByKnitMachineDataAsync completed");
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных PlanTotalHoursByKnitMachine");
        //    }
        //}
        //private async Task LoadZadanyListByMachineNewDataAsync(int kmlID)
        //{
        //    try
        //    {
        //        _zadanyListNewBindingSource.Clear();
        //        _zadanyListNewBindingSource.ResetBindings(false);

        //        zadanyListNewData = await _vyazService.GetZadanyListByMachine(kmlID);

        //        if (zadanyListNewData != null)
        //        {
        //            await _logger.LogEventAsync($"Получены данные ZadanyListByMachine", "LoadZadanyListByMachineNewDataAsync");

        //            await this.InvokeAsync(() =>
        //            {
        //                gridControlZadanyList.BeginUpdate();
        //                _zadanyListNewBindingSource.DataSource = zadanyListNewData;
        //                gridControlZadanyList.EndUpdate();
        //            });

        //            await _logger.LogEventAsync($"Данные ZadanyListByMachine успешно загружены", "LoadZadanyListByMachineNewDataAsync");
        //            _zadanyListNewBindingList.Add(zadanyListNewData[0]);
        //            _zadanyListNewBindingSource.ResetBindings(false);

        //            gridViewZadanyList.BeginSort();
        //            gridViewZadanyList.ClearSorting();
        //            gridViewZadanyList.SortInfo.Add(new GridColumnSortInfo(gridZadanyListColumnYearPlan, ColumnSortOrder.Ascending));
        //            gridViewZadanyList.SortInfo.Add(new GridColumnSortInfo(gridZadanyListColumnNom, ColumnSortOrder.Ascending));
        //            gridViewZadanyList.EndSort();
        //        }
        //        else
        //        {
        //            await _logger.LogEventAsync($"Не удалось найти данные ZadanyListByMachine", "LoadZadanyListByMachineNewDataAsync");
        //        }
        //        Debug.WriteLine($"LoadZadanyListByMachineNewDataAsync completed");
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных ZadanyListByMachine");
        //    }
        //}
        //private async Task LoadRzvPachListByNomNewDataAsync(int nom, string nomZad)
        //{
        //    try
        //    {
        //        _rzvPachListByNomNewBindingSource.Clear();
        //        _rzvPachListByNomNewBindingSource.ResetBindings(false);

        //        if (nom > 0)
        //        {
        //            rzvPachListByNomNewData = await _vyazService.GetRzvPachListByNom(nom, nomZad);
        //        }
        //        else
        //        {
        //            rzvPachListByNomNewData = null;
        //        }

        //        if (rzvPachListByNomNewData != null)
        //        {
        //            await _logger.LogEventAsync($"Получены данные RzvPachListByNom", "LoadRzvPachListByNomNewDataAsync");

        //            await this.InvokeAsync(() =>
        //            {
        //                _rzvPachListByNomNewBindingSource.DataSource = rzvPachListByNomNewData;
        //            });

        //            await _logger.LogEventAsync($"Данные RzvPachListByNom успешно загружены", "LoadRzvPachListByNomNewDataAsync");
        //            _rzvPachListByNomNewBindingList.Add(rzvPachListByNomNewData[0]);
        //            _rzvPachListByNomNewBindingSource.ResetBindings(false);
        //        }
        //        else
        //        {
        //            await _logger.LogEventAsync($"Не удалось найти данные RzvPachListByNom", "LoadRzvPachListByNomNewDataAsync");
        //        }
        //        Debug.WriteLine($"LoadRzvPachListByNomNewDataAsync completed");
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных RzvPachListByNom");
        //    }
        //}
        //private async Task LoadPZVOperListByPachListNewDataAsync(string pachList, int vyazPodrKod)
        //{
        //    try
        //    {
        //        _pZVOperListByPachListNewBindingSource.Clear();
        //        _pZVOperListByPachListNewBindingSource.ResetBindings(false);

        //        if (pachList.Length > 0 && pachList != "[]" && pachList is not null)
        //        {
        //            pZVOperListByPachListNewData = await _vyazService.GetPZVOperListByPachList(pachList, vyazPodrKod);
        //        }
        //        else
        //        {
        //            pZVOperListByPachListNewData = null;
        //        }

        //        if (pZVOperListByPachListNewData != null)
        //        {
        //            await _logger.LogEventAsync($"Получены данные PZVOperListByPachListNew", "LoadPZVOperListByPachListNewDataAsync");

        //            await this.InvokeAsync(() =>
        //            {
        //                _pZVOperListByPachListNewBindingSource.DataSource = pZVOperListByPachListNewData;
        //            });

        //            await _logger.LogEventAsync($"Данные PZVOperListByPachList успешно загружены", "LoadPZVOperListByPachListNewDataAsync");
        //            _pZVOperListByPachListNewBindingList.Add(pZVOperListByPachListNewData[0]);
        //            _pZVOperListByPachListNewBindingSource.ResetBindings(false);
        //        }
        //        else
        //        {
        //            await _logger.LogEventAsync($"Не удалось найти данные PZVOperListByPachList", "LoadPZVOperListByPachListNewDataAsync");
        //        }
        //        Debug.WriteLine($"LoadPZVOperListByPachListNewDataAsync completed");
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных PZVOperListByPachListNew");
        //    }
        //}
        private Task SetStatusAsync(string text)
           => this.UI(() => labelStatus.Text = text);

        private Task SetLoadingAsync(bool isLoading)
            => this.UI(() =>
            {
                labelStatus.Text = isLoading ? "Загрузка…" : "Готово";
                Cursor = isLoading ? Cursors.WaitCursor : Cursors.Default;
            });

        //private async Task LoadSmenZadanyVyazDataAsync()
        //{
        //    await _loader.RunAsync(
        //        async token =>
        //        {
        //            try
        //            {
        //                switch (vyazPodrKod)
        //                {
        //                    case 1:
        //                        advBandedGridViewSmenZadany.ShowLoadingPanel();
        //                        break;
        //                    case 2:
        //                    case 3:
        //                        gridViewSmenZadanyOtp.ShowLoadingPanel();
        //                        break;
        //                }

        //                await SetLoadingAsync(true);
        //                int _xIDNazn = 6; // признак принаджелности зоны к Вязальному производству
        //                int _xKodProizv = 1; // код производства 1 - вязальное производство
        //                //int _xKodPodr = 1; // код подразделения 1 - вязальное подразделение
        //                var bs = await _vyazService.GetSmenZadanyVyaz(_xIDNazn, _xKodProizv, vyazPodrKod, token);

        //                await this.UI(() =>
        //                {
        //                    gridControlSmenZadany.BeginUpdate();
        //                    _smenZadanyVyazBindingSource.DataSource = bs.DataSource;
        //                    if (vyazPodrKod == 1)
        //                    {
        //                        Application.Idle -= ExpandGroupsOnIdle;
        //                        Application.Idle += ExpandGroupsOnIdle;
        //                    }
        //                    gridControlSmenZadany.EndUpdate();
        //                    switch (vyazPodrKod)
        //                    {
        //                        case 1:
        //                            advBandedGridViewSmenZadany.BeginSort();
        //                            advBandedGridViewSmenZadany.ClearSorting();
        //                            advBandedGridViewSmenZadany.SortInfo.Add(new GridColumnSortInfo(bandedGridSmenZadanyColumnKmaNumber, ColumnSortOrder.Ascending));
        //                            advBandedGridViewSmenZadany.SortInfo.Add(new GridColumnSortInfo(bandedGridSmenZadanyColumnKmlNumber, ColumnSortOrder.Ascending));
        //                            advBandedGridViewSmenZadany.EndSort();
        //                            break;
        //                        case 2:
        //                        case 3:
        //                            gridViewSmenZadanyOtp.BeginSort();
        //                            gridViewSmenZadanyOtp.ClearSorting();
        //                            gridViewSmenZadanyOtp.SortInfo.Add(new GridColumnSortInfo(gridSmenZadanyOtpColumnSzFio, ColumnSortOrder.Ascending));
        //                            //gridViewSmenZadany.SortInfo.Add(new GridColumnSortInfo(gridZadanyListColumnNom, ColumnSortOrder.Ascending));
        //                            gridViewSmenZadanyOtp.EndSort();
        //                            break;
        //                    }

        //                });

        //                await SetStatusAsync(bs.Count == 0 ? "Нет данных" : "Готово");
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show($"Ошибка LoadSmenZadanyVyazDataAsync: {ex.Message}");
        //            }
        //        },
        //        onError: async ex =>
        //        {
        //            await _logger.LogErrorAsync(ex, "Ошибка загрузки LoadSmenZadanyVyazDataAsync");
        //            await SetStatusAsync("Ошибка загрузки");
        //            await SetLoadingAsync(false);
        //        },
        //        onCanceled: async byLifetime =>
        //        {
        //            if (!byLifetime)
        //                await SetStatusAsync("Отменено");

        //            await SetLoadingAsync(false);
        //        }
        //    );
        //    await SetLoadingAsync(false);

        //    switch (vyazPodrKod)
        //    {
        //        case 1:
        //            advBandedGridViewSmenZadany.HideLoadingPanel();
        //            break;
        //        case 2:
        //        case 3:
        //            gridViewSmenZadanyOtp.HideLoadingPanel();
        //            break;
        //    }

        //    Debug.WriteLine($"LoadSmenZadanyVyazDataAsync completed");
        //}
        //private async Task LoadSmenZadanyVyazNewDataAsync(int _xKodProizv)
        //private async Task LoadSmenZadanyVyazNewDataAsync(int _xKodProizv)
        //{
        //    try
        //    {
        //        await _loader.RunAsync(
        //            async token =>
        //            {
        //                try
        //                {
        //                    switch (vyazPodrKod)
        //                    {
        //                        case 1:
        //                            advBandedGridViewSmenZadany.ShowLoadingPanel();
        //                            break;
        //                        case 2:
        //                        case 3:
        //                            gridViewSmenZadanyOtp.ShowLoadingPanel();
        //                            break;
        //                    }

        //                    await SetLoadingAsync(true);
        //                    int _xIDNazn = 6; // признак принаджелности зоны к Вязальному производству
        //                    //int _xKodProizv = 1; // код производства 1 - вязальное производство
        //                    //int _xKodPodr = 1; // код подразделения 1 - вязальное подразделение
        //                    var bs = await _vyazService.GetSmenZadanyVyaz(_xIDNazn, _xKodProizv, vyazPodrKod, token);

        //                    await this.UI(() =>
        //                    {
        //                        gridControlSmenZadany.BeginUpdate();
        //                        _smenZadanyVyazNewBindingSource.DataSource = bs.DataSource;
        //                        Application.Idle -= ExpandGroupsOnIdle;
        //                        Application.Idle += ExpandGroupsOnIdle;
        //                        //----------------------
        //                        var changes = GetChanges<SmenZadanyVyaz>(
        //                            _smenZadanyVyazBindingSource,
        //                            _smenZadanyVyazNewBindingSource,
        //                            HashMode.ExcludeOnly,
        //                            keyProperties: new[] { "kwsKmaID", "kwsmlKmlID", "typeID", "szTab" },
        //                            hashProperties: new[] { "IsNew", "IsModified", "IsDeleted" }
        //                        );

        //                        ApplyChanges(
        //                            _smenZadanyVyazBindingSource,
        //                            changes,
        //                            UpdateFieldsMode.ExcludeOnly,
        //                            keyProperties: new[] { "kwsKmaID", "kwsmlKmlID", "typeID", "szTab" },
        //                            (GridView)gridControlSmenZadany.MainView,
        //                            fields: new[] { "IsNew", "IsModified", "IsDeleted" }
        //                        );

        //                        RemoveMissingSmart(
        //                            _smenZadanyVyazBindingSource,
        //                            changes.Removed,
        //                            gridControlSmenZadany
        //                        );
        //                        if (changes != null)
        //                        {
        //                            changes.Added?.Clear();
        //                            changes.Modified?.Clear();
        //                            changes.Removed?.Clear();
        //                            changes = null;
        //                        }
        //                        //------------------------------
        //                        if (vyazPodrKod == 1)
        //                        {
        //                            Application.Idle -= ExpandGroupsOnIdle;
        //                            Application.Idle += ExpandGroupsOnIdle;
        //                        }
        //                        gridControlSmenZadany.EndUpdate();

        //                        //----------------------
        //                        switch (vyazPodrKod)
        //                        {
        //                            case 1:
        //                                advBandedGridViewSmenZadany.BeginSort();
        //                                advBandedGridViewSmenZadany.ClearSorting();
        //                                advBandedGridViewSmenZadany.SortInfo.Add(new GridColumnSortInfo(bandedGridSmenZadanyColumnKmaNumber, ColumnSortOrder.Ascending));
        //                                advBandedGridViewSmenZadany.SortInfo.Add(new GridColumnSortInfo(bandedGridSmenZadanyColumnKmlNumber, ColumnSortOrder.Ascending));
        //                                advBandedGridViewSmenZadany.EndSort();
        //                                break;
        //                            case 2:
        //                            case 3:
        //                                gridViewSmenZadanyOtp.BeginSort();
        //                                gridViewSmenZadanyOtp.ClearSorting();
        //                                gridViewSmenZadanyOtp.SortInfo.Add(new GridColumnSortInfo(gridSmenZadanyOtpColumnSzFio, ColumnSortOrder.Ascending));
        //                                //gridViewSmenZadany.SortInfo.Add(new GridColumnSortInfo(gridZadanyListColumnNom, ColumnSortOrder.Ascending));
        //                                gridViewSmenZadanyOtp.EndSort();
        //                                break;
        //                        }
        //                        //----------------------

        //                        advBandedGridViewSmenZadany.TopRowIndex = TopRowIndexSmenZadany;
        //                    });

        //                    await SetStatusAsync(bs.Count == 0 ? "Нет данных" : "Готово");
        //                }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show($"Ошибка LoadSmenZadanyVyazNewDataAsync (GetSmenZadanyVyaz + _smenZadanyVyazBindingSource update): {ex.Message}");
        //                }
        //            },
        //            onError: async ex =>
        //            {
        //                await _logger.LogErrorAsync(ex, "Ошибка загрузки LoadSmenZadanyVyazNewDataAsync");
        //                await SetStatusAsync("Ошибка загрузки");
        //                await SetLoadingAsync(false);
        //            },
        //            onCanceled: async byLifetime =>
        //            {
        //                if (!byLifetime)
        //                    await SetStatusAsync("Отменено");

        //                await SetLoadingAsync(false);
        //            }
        //        );
        //        await SetLoadingAsync(false);

        //        switch (vyazPodrKod)
        //        {
        //            case 1:
        //                advBandedGridViewSmenZadany.HideLoadingPanel();
        //                break;
        //            case 2:
        //            case 3:
        //                gridViewSmenZadanyOtp.HideLoadingPanel();
        //                break;
        //        }

        //        Debug.WriteLine($"LoadSmenZadanyVyazNewDataAsync completed");
        //    }
        //    catch (Exception ex) { Debug.WriteLine($"LoadSmenZadanyVyazNewDataAsync - {ex.ToString()}"); }
        //}
        //private async Task LoadNaryadZadanyVyazDataAsync(int tab, int kmlID)
        //{
        //    // 1️ отменяем предыдущий запрос
        //    _loadCts?.Cancel();
        //    _loadCts?.Dispose();
        //    _loadCts = new CancellationTokenSource();

        //    var token = _loadCts.Token;

        //    try
        //    {
        //        // 2️ сразу очищаем грид + показываем загрузку
        //        await this.InvokeAsync(() =>
        //        {
        //            gridControlNaryadZadany.BeginUpdate();
        //            gridViewNaryadZadany.ShowLoadingPanel();

        //            _naryadZadanyVyazBindingSource.DataSource =
        //                new BindingList<NaryadZadanyVyaz>();
        //        });

        //        // 3️ долгий запрос
        //        var bs = await _vyazService.GetNaryadZadanyVyaz(tab, kmlID, vyazPodrKod, token);

        //        // если отменили — просто выходим
        //        if (token.IsCancellationRequested)
        //            return;

        //        // 4️ привязываем результат
        //        await this.InvokeAsync(() =>
        //        {
        //            _naryadZadanyVyazBindingSource.DataSource = bs.DataSource;
        //        });

        //        if (bs.Count == 0)
        //        {
        //            await _logger.LogEventAsync(
        //                "Данные NaryadZadanyVyaz не найдены",
        //                nameof(LoadNaryadZadanyVyazDataAsync)
        //            );
        //        }
        //        else
        //        {
        //            await _logger.LogEventAsync(
        //                "Данные NaryadZadanyVyaz успешно загружены",
        //                nameof(LoadNaryadZadanyVyazDataAsync)
        //            );
        //        }
        //        Debug.WriteLine($"LoadNaryadZadanyVyazDataAsync completed");
        //    }
        //    catch (OperationCanceledException)
        //    {
        //        // молча — это нормальный сценарий
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, "Ошибка загрузки данных NaryadZadanyVyaz");
        //    }
        //    finally
        //    {
        //        await this.InvokeAsync(() =>
        //        {
        //            gridViewNaryadZadany.HideLoadingPanel();
        //            gridControlNaryadZadany.EndUpdate();
        //            gridViewNaryadZadany.ExpandAllGroups();
        //        });
        //    }
        //}

        //private async Task LoadPlanTotalQuantityByArticulDataAsync()
        //{
        //    // 1️ отменяем предыдущий запрос
        //    _loadCts?.Cancel();
        //    _loadCts?.Dispose();
        //    _loadCts = new CancellationTokenSource();

        //    var token = _loadCts.Token;

        //    try
        //    {
        //        // 2️ сразу очищаем грид + показываем загрузку
        //        await this.InvokeAsync(() =>
        //        {
        //            gridControlPlanTotalQuantityByArticul.BeginUpdate();
        //            gridViewPlanTotalQuantityByArticul.ShowLoadingPanel();

        //            _planTotalQuantityByArticul.DataSource =
        //                new BindingList<PlanTotalQuantityByArticul>();
        //        });

        //        // 3️ долгий запрос
        //        var bs = await _vyazService.GetPlanTotalQuantityByArticul(token);

        //        // если отменили — просто выходим
        //        if (token.IsCancellationRequested)
        //            return;

        //        // 4️ привязываем результат
        //        await this.InvokeAsync(() =>
        //        {
        //            _planTotalQuantityByArticul.DataSource = bs.DataSource;
        //        });

        //        if (bs.Count == 0)
        //        {
        //            await _logger.LogEventAsync(
        //                "Данные GetPlanTotalQuantityByArticul не найдены",
        //                nameof(LoadPlanTotalQuantityByArticulDataAsync)
        //            );
        //        }
        //        else
        //        {
        //            await _logger.LogEventAsync(
        //                "Данные GetPlanTotalQuantityByArticul успешно загружены",
        //                nameof(LoadPlanTotalQuantityByArticulDataAsync)
        //            );
        //        }
        //        Debug.WriteLine($"LoadPlanTotalQuantityByArticulDataAsync completed");
        //    }
        //    catch (OperationCanceledException)
        //    {
        //        // молча — это нормальный сценарий
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, "Ошибка загрузки данных GetPlanTotalQuantityByArticul");
        //    }
        //    finally
        //    {
        //        await this.InvokeAsync(() =>
        //        {
        //            gridViewPlanTotalQuantityByArticul.HideLoadingPanel();
        //            gridControlPlanTotalQuantityByArticul.EndUpdate();
        //            gridViewPlanTotalQuantityByArticul.ExpandAllGroups();
        //        });
        //    }
        //}

        //private async Task LoadZadanyListByArticulKodDataAsync(string _kod)
        //{
        //    await _loader.RunAsync(
        //        async token =>
        //        {
        //            try
        //            {
        //                gridViewZadanyList.ShowLoadingPanel();

        //                await SetLoadingAsync(true);
        //                var bs = await _vyazService.GetZadanyListByArticulKod(_kod, token);

        //                await this.UI(() =>
        //                {
        //                    gridControlZadanyList.BeginUpdate();
        //                    _zadanyListBindingSource.DataSource = bs.DataSource;
        //                    gridControlZadanyList.EndUpdate();
        //                });

        //                await SetStatusAsync(bs.Count == 0 ? "Нет данных" : "Готово");
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show($"Ошибка LoadZadanyListByArticulKodDataAsync: {ex.Message}");
        //            }
        //        },
        //        onError: async ex =>
        //        {
        //            await _logger.LogErrorAsync(ex, "Ошибка загрузки LoadZadanyListByArticulKodDataAsync");
        //            await SetStatusAsync("Ошибка загрузки");
        //            await SetLoadingAsync(false);
        //        },
        //        onCanceled: async byLifetime =>
        //        {
        //            if (!byLifetime)
        //                await SetStatusAsync("Отменено");

        //            await SetLoadingAsync(false);
        //        }
        //    );
        //    await SetLoadingAsync(false);
        //    Debug.WriteLine($"LoadZadanyListByArticulKodDataAsync completed");
        //}

        //private async Task LoadZadanyListByArticulKodNewDataAsync(int _xKodProizv)
        //{
        //    try
        //    {
        //        await _loader.RunAsync(
        //            async token =>
        //            {
        //                try
        //                {
        //                    gridViewSmenZadany.ShowLoadingPanel();

        //                    await SetLoadingAsync(true);
        //                    int _xIDNazn = 6; // признак принаджелности зоны к Вязальному производству
        //                                      //int _xKodProizv = 1; // код производства 1 - вязальное производство
        //                    //int _xKodPodr = 1; // код подразделения 1 - вязальное подразделение
        //                    var bs = await _vyazService.GetSmenZadanyVyaz(_xIDNazn, _xKodProizv, vyazPodrKod, token);

        //                    await this.UI(() =>
        //                    {
        //                        gridControlSmenZadany.BeginUpdate();
        //                        _smenZadanyVyazNewBindingSource.DataSource = bs.DataSource;
        //                        Application.Idle -= ExpandGroupsOnIdle;
        //                        Application.Idle += ExpandGroupsOnIdle;
        //                        //----------------------
        //                        var changes = BindingSourceHelper.GetChanges<SmenZadanyVyaz>(
        //                            _smenZadanyVyazBindingSource,
        //                            _smenZadanyVyazNewBindingSource,
        //                            HashMode.ExcludeOnly,
        //                            keyProperties: new[] { "kwsKmaID", "kwsmlKmlID", "typeID" },
        //                            hashProperties: new[] { "IsNew", "IsModified", "IsDeleted" }
        //                        );

        //                        BindingSourceHelper.ApplyChanges<SmenZadanyVyaz>(
        //                            _smenZadanyVyazBindingSource,
        //                            changes,
        //                            UpdateFieldsMode.ExcludeOnly,
        //                            keyProperties: new[] { "kwsKmaID", "kwsmlKmlID", "typeID" },
        //                            gridViewPZVOperList,
        //                            fields: new[] { "IsNew", "IsModified", "IsDeleted" }
        //                        );

        //                        BindingSourceHelper.RemoveMissingSmart<SmenZadanyVyaz>(
        //                            _smenZadanyVyazBindingSource,
        //                            changes.Removed,
        //                            gridControlSmenZadany
        //                        );
        //                        if (changes != null)
        //                        {
        //                            changes.Added?.Clear();
        //                            changes.Modified?.Clear();
        //                            changes.Removed?.Clear();
        //                            changes = null;
        //                        }
        //                        //----------------------
        //                        Application.Idle -= ExpandGroupsOnIdle;
        //                        Application.Idle += ExpandGroupsOnIdle;
        //                        gridControlSmenZadany.EndUpdate();

        //                        gridViewSmenZadany.TopRowIndex = TopRowIndexSmenZadany;
        //                    });

        //                    await SetStatusAsync(bs.Count == 0 ? "Нет данных" : "Готово");
        //                }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show($"Ошибка LoadSmenZadanyVyazNewDataAsync (GetSmenZadanyVyaz + _smenZadanyVyazBindingSource update): {ex.Message}");
        //                }
        //            },
        //            onError: async ex =>
        //            {
        //                await _logger.LogErrorAsync(ex, "Ошибка загрузки LoadSmenZadanyVyazNewDataAsync");
        //                await SetStatusAsync("Ошибка загрузки");
        //                await SetLoadingAsync(false);
        //            },
        //            onCanceled: async byLifetime =>
        //            {
        //                if (!byLifetime)
        //                    await SetStatusAsync("Отменено");

        //                await SetLoadingAsync(false);
        //            }
        //        );
        //        await SetLoadingAsync(false);

        //        Debug.WriteLine($"LoadSmenZadanyVyazNewDataAsync completed");
        //    }
        //    catch (Exception ex) { Debug.WriteLine($"LoadSmenZadanyVyazNewDataAsync - {ex.ToString()}"); }
        //}
        private void ExpandGroupsOnIdle(object sender, EventArgs e)
        {
            Application.Idle -= ExpandGroupsOnIdle;

            // здесь группы уже ТОЧНО есть
            SetGroupExpandState_NoRecursion();
            Debug.WriteLine($"ExpandGroupsOnIdle executed");
        }
        //private async Task LoadArtNormNDataAsync(int annId, CancellationToken ct)
        //{
        //    var data = await _anService.GetArtNormDataById(annId, ct); // добавь ct внутрь сервиса
        //    ct.ThrowIfCancellationRequested();

        //    await this.InvokeAsync(() =>
        //    {
        //        _artNormNBindingSource.DataSource = data == null
        //            ? new List<ArtNormN>()
        //            : new List<ArtNormN> { data };

        //        _artNormNBindingSource.ResetBindings(false);
        //    });
        //    Debug.WriteLine($"LoadArtNormNDataAsync completed for annId={annId}");
        //}

        //private async Task LoadNormRaszDataAsync(int nrId, CancellationToken ct)
        //{
        //    var data = await _anService.GetRelatedNormRaszByID(nrId, ct);
        //    ct.ThrowIfCancellationRequested();

        //    await this.InvokeAsync(() =>
        //    {
        //        _normRaszBindingSource.DataSource = data == null
        //            ? new List<NormRasz>()
        //            : new List<NormRasz> { data };

        //        _normRaszBindingSource.ResetBindings(false);
        //    });
        //    Debug.WriteLine($"LoadNormRaszDataAsync completed for nrId={nrId}");
        //}

        //private async Task LoadKnitWorkingShiftSmenToMoveDataAsync(int _kmaID)
        //{
        //    try
        //    {
        //        var sql = @$"
        //                    SELECT * 
        //                    FROM knitWorkingShiftNewCurrentSmen_view
        //                    WHERE kmaID <> {_kmaID}
        //                      AND dateShiftStart IS NOT NULL
        //                      AND dateShiftEnd IS NULL";

        //        var data = await _dbService.GetListAsync<KnitWorkingShiftSmen>(sql, null);

        //        _zonesCache = data ?? new List<KnitWorkingShiftSmen>();
        //        await _logger.LogEventAsync($"Данные KnitWorkingShiftSmenToMove успешно загружены", "LoadKnitWorkingShiftSmenToMoveDataAsync");
        //        Debug.WriteLine($"LoadKnitWorkingShiftSmenToMoveDataAsync completed with {_zonesCache.Count} records");
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, "Ошибка загрузки зон");
        //        await _logger.LogEventAsync($"Не удалось найти данные KnitWorkingShiftSmenToMove", "LoadKnitWorkingShiftSmenToMoveDataAsync");
        //        _zonesCache = new();
        //    }
        //}
        //private async void layoutControlGroup6_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        //{
        //    try
        //    {
        //        int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);

        //        switch (buttonIndex)
        //        {
        //            case 0:
        //                //Debug.WriteLine(ButtonPreliminaryWd.Enabled + " " + ButtonPreliminaryWd.Visible);
        //                MessageBox.Show("Просмотр работы к подтверждению");
        //                break;
        //            case 2:
        //                //Debug.WriteLine(ButtonEditWd.Enabled + " " + ButtonEditWd.Visible);
        //                MessageBox.Show("История по операции");
        //                break;
        //            case 4:
        //                //Debug.WriteLine(customSimpleButton1.Enabled + " " + customSimpleButton1.Visible);
        //                MessageBox.Show("Выгрузить операции в XLS");
        //                break;
        //            case 6:
        //                //Debug.WriteLine(ButtonArchAndCopyWd.Enabled + " " + ButtonArchAndCopyWd.Visible);
        //                //MessageBox.Show("Загрузить операции");
        //                gridViewRzvPachListByNom.FocusedColumn = gridViewRzvPachListByNom.Columns["data_paln"];
        //                gridViewRzvPachListByNom.FocusedColumn = gridViewRzvPachListByNom.Columns["SyncSelection"];
        //                await GridOverlayLoader.RunTaskWithOverlayAsync(
        //                    gridControlPZVOperList,
        //                    LoadPlanZagrVyazByZadanySelection
        //                    , CancellationToken.None
        //                    );
        //                break;
        //            case 8:
        //                await GridOverlayLoader.RunTaskWithOverlayAsync(
        //                    gridControlPZVOperList,
        //                    ClearSelectedPachList
        //                    , CancellationToken.None
        //                    );
        //                break;
        //            case 10:
        //                _pZVOperListByPachListBindingSource.Clear();
        //                gridViewRzvPachListByNom.FocusedColumn = gridViewRzvPachListByNom.Columns["data_paln"];
        //                gridViewRzvPachListByNom.FocusedColumn = gridViewRzvPachListByNom.Columns["SyncSelection"];
        //                await GridOverlayLoader.RunTaskWithOverlayAsync(
        //                    gridControlPZVOperList,
        //                    LoadPlanZagrVyazByZadanySelection
        //                    , CancellationToken.None
        //                    );
        //                break;
        //        }
        //        Debug.WriteLine($"layoutControlGroup6_CustomButtonClick completed for button index {buttonIndex}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка обработки layoutControlGroup6_CustomButtonClick: {ex.Message}");
        //    }
        //}
        private void CommitRzvSelectionEditor()
        {
            gridViewRzvPachListByNom.PostEditor();
            gridViewRzvPachListByNom.UpdateCurrentRow();
            _rzvPachListByNomBindingSource.EndEdit();
            _rzvPachListByNomBindingSource.CurrencyManager?.EndCurrentEdit();
        }
        public async Task LoadPlanZagrVyazByZadanySelection()
        {
            GridHelper.GridViewState? state = null;

            try
            {
                state = _gridHelper.CaptureState<PZVOperList>(
                    gridViewPZVOperList,
                    _pZVOperListByPachListBindingSource,
                    x => x.olPzvID.ToString());

                CommitRzvSelectionEditor();
                var _xTopRowIndex = gridViewPZVOperList.TopRowIndex;
                //var context = _contextBuilder.Build();
                var context = _contextBuilder.BuildForPachSelection();
                var request = new LoadPzvOperationsRequest
                {
                    VyazPodrKod = context.VyazPodrKod,
                    SelectedPachList = context.SelectedPachList
                };

                var result = await _loadPzvOperationsUseCase.ExecuteAsync(request, CancellationToken.None);

                textBoxJson.Text = result.JsonPayload ?? string.Empty;
                _pzvOperationsGridUpdater.Apply(result.Rows);

                bool needInitialExpand = string.IsNullOrWhiteSpace(state.FocusedRowKey);

                if (!needInitialExpand)
                {
                    _gridHelper.RestoreState<PZVOperList>(
                        gridViewPZVOperList,
                        _pZVOperListByPachListBindingSource,
                        state,
                        x => x.olPzvID.ToString());
                }
                gridViewPZVOperList.TopRowIndex = _xTopRowIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных LoadPlanZagrVyazByZadanySelection: {ex.Message}");
            }
            //try
            //{
            //    Debug.WriteLine($"LoadPlanZagrVyazByZadanySelection started TopRowIndex={gridViewPZVOperList.TopRowIndex}, TopRowIndexPZV={TopRowIndexPZV}");
            //    try { gridViewRzvPachListByNom.PostEditor(); }
            //    catch (SqlException ex)
            //    {
            //        Debug.WriteLine(
            //             $"SQL ERROR 1 {ex.Number}: {ex.Message}\n" +
            //             $"Procedure: {ex.Procedure}\n" +
            //             $"Line: {ex.LineNumber}"
            //         );
            //        throw;
            //    }//catch { }
            //    try { gridViewRzvPachListByNom.UpdateCurrentRow(); }
            //    catch (SqlException ex)
            //    {
            //        Debug.WriteLine(
            //             $"SQL ERROR 2 {ex.Number}: {ex.Message}\n" +
            //             $"Procedure: {ex.Procedure}\n" +
            //             $"Line: {ex.LineNumber}"
            //         );
            //        throw;
            //    }//catch { }

            //    try { _rzvPachListByNomBindingSource.EndEdit(); }
            //    catch (SqlException ex)
            //    {
            //        Debug.WriteLine(
            //             $"SQL ERROR 3 {ex.Number}: {ex.Message}\n" +
            //             $"Procedure: {ex.Procedure}\n" +
            //             $"Line: {ex.LineNumber}"
            //         );
            //        throw;
            //    }//catch { }
            //     //catch { }
            //    try { _rzvPachListByNomBindingSource.CurrencyManager?.EndCurrentEdit(); }
            //    catch (SqlException ex)
            //    {
            //        Debug.WriteLine(
            //             $"SQL ERROR 4 {ex.Number}: {ex.Message}\n" +
            //             $"Procedure: {ex.Procedure}\n" +
            //             $"Line: {ex.LineNumber}"
            //         );
            //        throw;
            //    }//catch { }
            //    //catch { }

            //    // Фильтруем записи где syncSelection = 1
            //    var filteredRecords = _rzvPachListByNomBindingSource.Cast<object>()
            //        .Where(item =>
            //        {
            //            var property = item.GetType().GetProperty("SyncSelection");
            //            return property != null && Convert.ToInt32(property.GetValue(item)) == 1;
            //        })
            //        .ToList();

            //    // Преобразуем в JSON
            //    string jsonString = JsonConvert.SerializeObject(filteredRecords, Formatting.Indented);
            //    textBoxJson.Text = jsonString;
            //    await LoadPZVOperListByPachListNewDataAsync(jsonString, vyazPodrKod);
            //    Debug.WriteLine($"LoadPlanZagrVyazByZadanySelection after await LoadPZVOperListByPachListNewDataAsync TopRowIndex={gridViewPZVOperList.TopRowIndex}, TopRowIndexPZV={TopRowIndexPZV}");
            //    gridViewPZVOperList.BeginSort();
            //    gridViewPZVOperList.ClearSorting();
            //    // Сначала сортируем по артикулу, далее пачка, номер операции/подоперации, ID родительской записи, ID записи
            //    gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olPzvArticul"], ColumnSortOrder.Ascending));
            //    gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olNPach"], ColumnSortOrder.Ascending));
            //    gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olNo"], ColumnSortOrder.Ascending));
            //    gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olNpo"], ColumnSortOrder.Ascending));
            //    gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olPzvIDParent"], ColumnSortOrder.Ascending));
            //    gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olPzvID"], ColumnSortOrder.Ascending));

            //    gridViewPZVOperList.EndSort();
            //    gridViewPZVOperList.TopRowIndex = TopRowIndexPZV;

            //    Debug.WriteLine($"LoadPlanZagrVyazByZadanySelection after await gridViewPZVOperList.EndSort TopRowIndex={gridViewPZVOperList.TopRowIndex}, TopRowIndexPZV={TopRowIndexPZV}");
            //    var changes = GetChanges<PZVOperList>(
            //        _pZVOperListByPachListBindingSource,
            //        _pZVOperListByPachListNewBindingSource,
            //        HashMode.ExcludeOnly,
            //        keyProperties: new[] { "olPzvID" },
            //        hashProperties: new[] { "IsNew", "IsModified", "IsDeleted", "SyncSelection", "ErrorSelection" }
            //    );
            //    Debug.WriteLine($"LoadPlanZagrVyazByZadanySelection after GetChanges.EndSort TopRowIndex={gridViewPZVOperList.TopRowIndex}, TopRowIndexPZV={TopRowIndexPZV}");

            //    int xTopRowIndex = TopRowIndexPZV;
            //    ApplyChanges(
            //        _pZVOperListByPachListBindingSource,
            //        changes,
            //        UpdateFieldsMode.ExcludeOnly,
            //        keyProperties: new[] { "olPzvID" },
            //        gridViewPZVOperList,
            //        fields: new[] { "IsNew", "IsModified", "IsDeleted", "SyncSelection", "ErrorSelection" }
            //    );
            //    gridViewPZVOperList.TopRowIndex = xTopRowIndex;
            //    TopRowIndexPZV = xTopRowIndex;
            //    Debug.WriteLine($"LoadPlanZagrVyazByZadanySelection after ApplyChanges.EndSort TopRowIndex={gridViewPZVOperList.TopRowIndex}, TopRowIndexPZV={TopRowIndexPZV}");


            //    RemoveMissingSmart(
            //        _pZVOperListByPachListBindingSource,
            //        changes.Removed,
            //        gridControlPZVOperList
            //    );
            //    Debug.WriteLine($"LoadPlanZagrVyazByZadanySelection after RemoveMissingSmart.EndSort TopRowIndex={gridViewPZVOperList.TopRowIndex}, TopRowIndexPZV={TopRowIndexPZV}");

            //    if (changes != null)
            //    {
            //        changes.Added?.Clear();
            //        changes.Modified?.Clear();
            //        changes.Removed?.Clear();
            //        changes = null;
            //    }
            //    //------------------------------------------------------
            //    gridViewPZVOperList.ExpandAllGroups();

            //    int _focusedRowHandle = gridViewPZVOperList.FocusedRowHandle;
            //    gridViewPZVOperList.TopRowIndex = TopRowIndexPZV;
            //    Debug.WriteLine($"LoadPlanZagrVyazByZadanySelection completed with {filteredRecords.Count} selected records, focused row handle: {_focusedRowHandle}, TopRowIndex={gridViewPZVOperList.TopRowIndex}, TopRowIndexPZV={TopRowIndexPZV}");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Ошибка при обновлении данных LoadPlanZagrVyazByZadanySelection: {ex.Message}");
            //}
            ////----------------------------------------------------
        }
        private async void PlanZagrVyaz_Load(object sender, EventArgs e)
        {
            try
            {
                customLabel12.VisibleLogic = false;
                customLabel12.VisiblePermission = false;
                textBoxPzvNomSearch.VisibleLogic = false;
                textBoxPzvNomSearch.VisiblePermission = false;
                customLabel13.VisibleLogic = false;
                customLabel13.VisiblePermission = false;
                textBoxPzvNPachSearch.VisibleLogic = false;
                textBoxPzvNPachSearch.VisiblePermission = false;
                customLabel14.VisibleLogic = false;
                customLabel14.VisiblePermission = false;
                textBoxPzvYearPachSearch.VisibleLogic = false;
                textBoxPzvYearPachSearch.VisiblePermission = false;

                layoutControlItem47.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem48.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem49.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem50.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem51.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem52.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                switch (vyazPodrKod)
                {
                    case 1:
                        //layoutControlItemKnittingMachineWorkAssignment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        //layoutControlItemKnittingMachineCancelWorkAssignment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        break;
                    case 2:
                    case 3:
                        layoutControlItemKnittingMachineWorkAssignment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        layoutControlItemKnittingMachineCancelWorkAssignment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        break;
                }

                _lifetimeCts = new CancellationTokenSource();
                _loadCts = new CancellationTokenSource();
                InitObjectRestartMap();

                Task bindingsTask = InitializeBindingsAsync();
                await Task.WhenAll(bindingsTask);

                await InitServiceBrokerAsync(_lifetimeCts.Token);
                switch (vyazPodrKod)
                {
                    case 1:
                        await LoadPlanTotalHoursByKnitMachineDataAsync();
                        break;
                    case 2:
                    case 3:
                        await LoadPlanTotalQuantityByArticulDataAsync();
                        break;
                }

                await LoadSmenZadanyVyazDataAsync();
                //ExpandGroupsOnIdle();
                Debug.WriteLine($"PlanZagrVyaz_Load completed");
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine($"OperationCanceledException: {ex}");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке формы PlanZagrVyaz");
                MessageBox.Show($"Ошибка при загрузке формы PlanZagrVyaz PlanZagrVyaz_Load: {ex.Message}");
            }
        }

        private async void gridViewPlanTotalHoursByKnitMachine_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                var viewTop = (GridView)sender;

                if (!viewTop.IsDataRow(e.FocusedRowHandle))
                    return;

                var row = viewTop.GetRow(e.FocusedRowHandle) as PlanTotalHoursByKnitMachine;
                if (row == null) return;

                await ReloadZadanyForKmlAsync(row.kmlID);
                Debug.WriteLine($"gridViewPlanTotalHoursByKnitMachine_FocusedRowChanged completed for kmlID={row.kmlID}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в gridViewPlanTotalHoursByKnitMachine_FocusedRowChanged: {ex.Message}");
            }
        }
        private async Task ReloadZadanyForKmlAsync(int kmlId)
        {
            var view = gridViewZadanyList;

            view.BeginUpdate();
            try
            {
                // 1) грузим новые данные
                int version = ++_loadVersion;
                await LoadZadanyListByMachineNewDataAsync(kmlId);
                if (version != _loadVersion) return; // был новый запрос — этот выкидываем

                // 2) применяем изменения
                var changes = GetChanges<PZVZadanyList>(
                    _zadanyListBindingSource,
                    _zadanyListNewBindingSource,
                    HashMode.ExcludeOnly,
                    keyProperties: new[] { "kmlID", "pszNom" },
                    hashProperties: new[] { "SyncSelection" }
                );

                ApplyChanges(
                    _zadanyListBindingSource,
                    changes,
                    UpdateFieldsMode.ExcludeOnly,
                    keyProperties: new[] { "kmlID", "pszNom" },
                    fields: new[] { "SyncSelection" }
                );
                if (changes != null)
                {
                    changes.Added?.Clear();
                    changes.Modified?.Clear();
                    changes.Removed?.Clear();
                    changes = null;
                }
                // 3) фильтр
                view.ActiveFilterString = $"kmlID == {kmlId}";

                // 4) обновление данных грида
                //view.RefreshData();
            }
            finally
            {
                view.EndUpdate();
            }

            gridControlZadanyList.BeginInvoke(new Action(() =>
            {
                if (_autoSelectPending)
                {
                    TryFocusPendingPszNom();
                    _autoSelectPending = false;
                }
                else
                {
                    EnsureFirstRowSelectedIfNothingSelected(gridViewZadanyList, _suppressZadanyFocusHandler);
                    gridViewZadanyList_FocusedRowChanged(
                                gridViewZadanyList,
                                new FocusedRowChangedEventArgs(-1, gridViewZadanyList.FocusedRowHandle)
                            );
                }
            }));
            Debug.WriteLine($"ReloadZadanyForKmlAsync completed for kmlId={kmlId}");
        }
        private async Task ReloadZadanyForArticulKodAsync(string kod)
        {
            var view = gridViewZadanyList;

            view.BeginUpdate();
            try
            {
                // 1) грузим новые данные
                int version = ++_loadVersion;
                await LoadZadanyListByArticulNewDataAsync(kod);
                if (version != _loadVersion) return; // был новый запрос — этот выкидываем

                // 2) применяем изменения
                var changes = GetChanges<PZVZadanyList>(
                    _zadanyListBindingSource,
                    _zadanyListNewBindingSource,
                    HashMode.ExcludeOnly,
                    keyProperties: new[] { "kmlID", "pszNom", "kod" },
                    hashProperties: new[] { "SyncSelection" }
                );

                ApplyChanges(
                    _zadanyListBindingSource,
                    changes,
                    UpdateFieldsMode.ExcludeOnly,
                    keyProperties: new[] { "kmlID", "pszNom", "kod" },
                    fields: new[] { "SyncSelection" }
                );
                if (changes != null)
                {
                    changes.Added?.Clear();
                    changes.Modified?.Clear();
                    changes.Removed?.Clear();
                    changes = null;
                }
                // 3) фильтр
                view.ActiveFilterString = $"Kod == {kod}";

                // 4) обновление данных грида
                //view.RefreshData();
            }
            finally
            {
                view.EndUpdate();
                //gridControlZadanyList.BeginInvoke(new Action(() =>
                //{
                //    if (_autoSelectPending)
                //    {
                //        TryFocusPendingPszNom();
                //        _autoSelectPending = false;
                //    }
                //    else
                //    {
                //        EnsureFirstRowSelectedIfNothingSelected(gridViewZadanyList, _suppressZadanyFocusHandler);
                //        gridViewZadanyList_FocusedRowChanged(
                //                    gridViewZadanyList,
                //                    new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gridViewZadanyList.FocusedRowHandle)
                //                );
                //    }
                //}));
            }

            gridControlZadanyList.BeginInvoke(new Action(() =>
            {
                if (_autoSelectPending)
                {
                    TryFocusPendingPszNom();
                    _autoSelectPending = false;
                }
                else
                {
                    EnsureFirstRowSelectedIfNothingSelected(gridViewZadanyList, _suppressZadanyFocusHandler);
                    gridViewZadanyList_FocusedRowChanged(
                                gridViewZadanyList,
                                new FocusedRowChangedEventArgs(-1, gridViewZadanyList.FocusedRowHandle)
                            );
                }
            }));
            Debug.WriteLine($"ReloadZadanyForArticulKodAsync completed for kod={kod}");
        }
        private void TryFocusPendingPszNom()
        {
            if (string.IsNullOrWhiteSpace(_pendingPszNom)) return;

            var view = gridViewZadanyList;
            var col = view.Columns.FirstOrDefault(c => c.FieldName == "pszNom");
            if (col == null) return;

            string searchVal = NormalizeNom(_pendingPszNom);

            int rh = view.LocateByValue(0, col, searchVal);
            if (!view.IsValidRowHandle(rh) || !view.IsDataRow(rh)) return;

            FocusRowWithScrollAndFire(view, rh, ref _suppressZadanyFocusHandler, forceFire: true);

            // после снятия suppress вызываем обработчик вручную
            gridViewZadanyList_FocusedRowChanged(
                view,
                new FocusedRowChangedEventArgs(-1, view.FocusedRowHandle)
            );

            _pendingPszNom = null;
            _autoSelectPending = false;

            Debug.WriteLine($"TryFocusPendingPszNom executed with search='{searchVal}', found row handle={rh}");
        }
        private static string NormalizeNom(string s)
        {
            return (s ?? "")
                .Replace('\u00A0', ' ')
                .Trim();
        }

        private async void gridViewZadanyList_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (_suppressZadanyFocusHandler)
                return;

            try
            {
                var viewTop = (GridView)sender;

                if (!viewTop.IsDataRow(e.FocusedRowHandle))
                    return;

                var row = viewTop.GetRow(e.FocusedRowHandle) as PZVZadanyList;
                if (row == null) return;

                await ReloadPachListForZadanyAsync(row.nom, row.pszNom, row.Gradacia);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в gridViewZadanyList_FocusedRowChanged: {ex.Message}");
            }

            //try
            //{
            //    var viewTop = (DevExpress.XtraGrid.Views.Grid.GridView)sender;

            //    //if (viewTop?.FocusedColumn == gridZadanyListColumnSyncSelection)
            //    //    return;

            //    //MessageBox.Show(viewTop.FocusedColumn?.FieldName ?? "null");
            //    //if (viewTop.FocusedColumn == gridZadanyListColumnSyncSelection)
            //    //    return;

            //    if (!viewTop.IsDataRow(e.FocusedRowHandle))
            //        return;

            //    var row = viewTop.GetRow(e.FocusedRowHandle) as PZVZadanyList;
            //    if (row == null) return;

            //    await ReloadPachListForZadanyAsync(row.nom, row.pszNom, row.Gradacia);
            //    Debug.WriteLine($"gridViewZadanyList_FocusedRowChanged completed for nom={row.nom}, pszNom={row.pszNom}, Gradacia={row.Gradacia}");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Ошибка в gridViewZadanyList_FocusedRowChanged: {ex.Message}");
            //}
        }
        private async Task ReloadPachListForZadanyAsync(int _nom, string _nomZad, int _gradacia)
        {
            var view = gridViewRzvPachListByNom;

            view.BeginUpdate();
            try
            {
                // 1) грузим новые данные
                int version = ++_rzvLoadVersion;
                await LoadRzvPachListByNomNewDataAsync(_nom, _nomZad);
                if (version != _rzvLoadVersion) return; // был новый запрос — этот выкидываем

                // 2) применяем изменения (как у тебя)
                var changes = GetChanges<RzvPachListByNom>(
                    _rzvPachListByNomBindingSource,
                    _rzvPachListByNomNewBindingSource,
                    HashMode.ExcludeOnly,
                    keyProperties: new[] { "nomZad", "nom", "nom_n" },
                    hashProperties: new[] { "SyncSelection" }
                );

                ApplyChanges(
                    _rzvPachListByNomBindingSource,
                    changes,
                    UpdateFieldsMode.ExcludeOnly,
                    keyProperties: new[] { "nomZad", "nom", "nom_n" },
                    fields: new[] { "SyncSelection" }
                );
                if (changes != null)
                {
                    changes.Added?.Clear();
                    changes.Modified?.Clear();
                    changes.Removed?.Clear();
                    changes = null;
                }
                // 3) фильтр + запрет редактирования поля Градация
                gridRzvPachListByNomColumnGradacia.OptionsColumn.ReadOnly = _gradacia != 1;

                // Фильтр 3-го грида — используем сохранённые nomZad/nom
                view.ActiveFilterString = $"nomZad == '{_nomZad}' and nom == {_nom}";

                // 4) обновление данных грида
                //view.RefreshData();

                //view.TopRowIndex = TopRowIndexPZV;
            }
            finally
            {
                view.EndUpdate();
            }

            // 5) после того, как UI применил изменения — делаем фокус/поиск
            //gridControlZadanyListByMachine.BeginInvoke(new Action(() =>
            //{
            //    TryFocusPendingPszNom();
            //}));

            gridControlRzvPachListByNom.BeginInvoke(new Action(() =>
            {
                //if (_autoSelectPending)
                //{
                //    TryFocusPendingPszNom();
                //}
                //else
                EnsureFirstRowSelectedIfNothingSelected(gridViewRzvPachListByNom, _suppressPachFocusHandler);
            }));
            Debug.WriteLine($"ReloadPachListForZadanyAsync applied changes for nom={_nom}, nomZad={_nomZad}, gradacia={_gradacia}");
        }
        private void EnsureFirstRowSelectedIfNothingSelected(GridView _view, bool _handler)
        {
            if (_view.RowCount <= 0) return;

            if (_view.IsValidRowHandle(_view.FocusedRowHandle) && _view.IsDataRow(_view.FocusedRowHandle))
                return; // <-- уже есть выбор, не ломаем пользовательский клик

            int first = _view.GetVisibleRowHandle(0);
            if (!_view.IsValidRowHandle(first) || !_view.IsDataRow(first)) return;

            _handler = true;
            try
            {
                _gridHelper.FocusAndScrollToRow(_view, first);
                _view.ClearSelection();
                _view.SelectRow(first);
            }
            finally
            {
                _handler = false;
            }
            Debug.WriteLine($"EnsureFirstRowSelectedIfNothingSelected executed for view={_view.Name}, first row handle={first}");
        }

        //private static void FocusAndScrollToRow(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        //{
        //    view.FocusedRowHandle = rowHandle;

        //    // 1) гарантируем, что строка станет видимой
        //    view.MakeRowVisible(rowHandle);

        //    // 2) аккуратно прокручиваем вверх так, чтобы строка была не в самом низу
        //    int visibleIndex = view.GetVisibleIndex(rowHandle);
        //    if (visibleIndex < 0) return;

        //    int rowsOnScreen = Math.Max(1, view.GridControl.Height / view.RowHeight);
        //    int targetTop = Math.Max(0, visibleIndex - rowsOnScreen / 2);

        //    view.TopRowIndex = targetTop;
        //}
        private void FocusRowWithScrollAndFire(GridView view, int rowHandle, ref bool suppressFlag, bool forceFire = true)
        {
            if (!view.IsValidRowHandle(rowHandle) || !view.IsDataRow(rowHandle)) return;

            suppressFlag = true;
            view.BeginUpdate();
            try
            {
                // Чтобы FocusedRowChanged точно сработал даже если "уже на этой строке"
                if (forceFire && view.FocusedRowHandle == rowHandle)
                    view.FocusedRowHandle = GridControl.InvalidRowHandle;

                _gridHelper.FocusAndScrollToRow(view, rowHandle);
                view.ClearSelection();
                view.SelectRow(rowHandle);
            }
            finally
            {
                view.EndUpdate();
                suppressFlag = false;
            }
            Debug.WriteLine($"FocusRowWithScrollAndFire executed for view={view.Name}, row handle={rowHandle}, forceFire={forceFire}");
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() => SyncSelectionUpdate()));
        }

        private void ClearSelectedPachList()
        {
            try
            {
                var recordsToUpdate = _rzvPachListByNomBindingSource.List
                    .Cast<RzvPachListByNom>()
                    .Where(record => record != null &&
                           record.SyncSelection == 1)
                    .ToList();
                foreach (var record in recordsToUpdate)
                {
                    record.SyncSelection = 0;
                }
                gridViewRzvPachListByNom.PostEditor();
                gridViewRzvPachListByNom.UpdateCurrentRow();
                _rzvPachListByNomBindingSource.ResetBindings(false);
                gridViewRzvPachListByNom.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления списка пачек ClearSelectedPachList: {ex.Message}");
            }

            try
            {
                var recordsToUpdate = _zadanyListBindingSource.List
                    .Cast<PZVZadanyList>()
                    .Where(record => record != null &&
                           record.SyncSelection == 1)
                    .ToList();
                foreach (var record in recordsToUpdate)
                {
                    record.SyncSelection = 0;
                }
                gridViewZadanyList.PostEditor();
                gridViewZadanyList.UpdateCurrentRow();
                _zadanyListBindingSource.ResetBindings(false);
                gridViewZadanyList.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления списка заданий ClearSelectedPachList: {ex.Message}");
            }

            _pZVOperListByPachListNewBindingSource.Clear();
            var changes = GetChanges<PZVOperList>(
                    _pZVOperListByPachListBindingSource,
                    _pZVOperListByPachListNewBindingSource,
                    HashMode.ExcludeOnly,
                    keyProperties: new[] { "olPzvID" },
                    hashProperties: new[] { "IsNew", "IsModified", "IsDeleted", "SyncSelection", "ErrorSelection" }
                );
            var metrics = RemoveMissingSmart(
                _pZVOperListByPachListBindingSource,
                changes.Removed,
                gridControlPZVOperList
            );
            if (changes != null)
            {
                changes.Added?.Clear();
                changes.Modified?.Clear();
                changes.Removed?.Clear();
                changes = null;
            }
            _logger.LogEventAsync(metrics.ToString());
            Debug.WriteLine($"ClearSelectedPachList completed records from PZVOperList");
        }

        private void customSimpleButton8_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show($"gridViewPZVOperList.OptionsView.ShowAutoFilterRow = {gridViewPZVOperList.OptionsView.ShowAutoFilterRow}");
                MessageBox.Show($"gridViewPZVOperList.OptionsCustomization.AllowFilter = {gridViewPZVOperList.OptionsCustomization.AllowFilter}");

                MessageBox.Show($"gridViewPZVOperList.OptionsFilter.AllowFilterEditor = {gridViewPZVOperList.OptionsFilter.AllowFilterEditor}");

                MessageBox.Show($"gridViewPZVOperList.OptionsView.ShowAutoFilterRow = {gridViewPZVOperList.OptionsView.ShowAutoFilterRow}");
                MessageBox.Show($"gridViewPZVOperList.OptionsCustomization.AllowFilter = {gridViewPZVOperList.OptionsCustomization.AllowFilter}");

                MessageBox.Show($"gridViewPZVOperList.OptionsFilter.AllowFilterEditor = {gridViewPZVOperList.OptionsFilter.AllowFilterEditor}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в customSimpleButton8_Click: {ex.Message}");
            }
        }
        /// <summary>
        /// Проверка заполненности дат
        /// </summary>
        /// <param name="_dateName"></param>
        /// <param name="_dateValue"></param>
        /// <param name="_xMessage"></param>
        /// <returns></returns>
        //private bool CheckPZVDate(string _dateName, DateTime _dateValue, string _xMessage)
        //{
        //    try
        //    {
        //        string _detailMessage = string.Empty;
        //        switch (_dateName)
        //        {
        //            case "OlPvDateNaznKm":  // дата назначения машины
        //                _detailMessage = "Операция уже назначена на машину";
        //                break;
        //            case "OlPvDateNaznTab": // дата назначения работника
        //                _detailMessage = "Операция уже назначена работнику";
        //                break;
        //            case "OlPvDateStart":   // дата начала работы
        //                _detailMessage = "Работник уже начал выполнять операцию";
        //                break;
        //            case "OlPvDateEnd":     // дата окончания работы
        //                _detailMessage = "Работник уже выполнил операцию";
        //                break;
        //            case "OlPvDateMast":    // дата подтверждения мастером
        //                _detailMessage = "Операция уже подтверждена мастером";
        //                break;
        //            default:
        //                MessageBox.Show("Неизвестная дата");
        //                return false;
        //        }
        //        if (_dateValue != null && _dateValue != DateTime.MinValue)
        //        {
        //            MessageBox.Show($"{_detailMessage}, {_xMessage}!");
        //            return false;
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в CheckPZVDate: {ex.Message}");
        //        return false;

        //    }
        //}
        /// <summary>
        /// Проверка пустых дат
        /// </summary>
        /// <param name="_dateName"></param>
        /// <param name="_dateValue"></param>
        /// <param name="_xMessage"></param>
        /// <returns></returns>
        //private bool CheckPZVEmptyDate(string _dateName, DateTime _dateValue, string _xMessage)
        //{
        //    try
        //    {
        //        string _detailMessage = string.Empty;
        //        switch (_dateName)
        //        {
        //            case "OlPvDateNaznKm":  // дата назначения машины
        //                _detailMessage = "Операция еще не назначена на машину";
        //                break;
        //            case "OlPvDateNaznTab": // дата назначения работника
        //                _detailMessage = "Операция еще не назначена работнику";
        //                break;
        //            case "OlPvDateStart":   // дата начала работы
        //                _detailMessage = "Работник еще не начал выполнять операцию";
        //                break;
        //            case "OlPvDateEnd":     // дата окончания работы
        //                _detailMessage = "Работник еще выполнил операцию";
        //                break;
        //            case "OlPvDateMast":    // дата подтверждения мастером
        //                _detailMessage = "Операция уже подтверждена мастером";
        //                break;
        //            default:
        //                MessageBox.Show("Неизвестная дата");
        //                return false;
        //        }
        //        if (_dateValue == null || _dateValue == DateTime.MinValue)
        //        {
        //            MessageBox.Show($"{_detailMessage}, {_xMessage}!");
        //            return false;
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в CheckPZVEmptyDate: {ex.Message}");
        //        return false;

        //    }
        //}
        private PzvActionType? ResolveDoubleClickAction(GridColumn column, PZVOperList row)
        {
            if (column == gridPZVOperListColumnOlKmlNumber || column == gridPZVOperListColumnOlPvDateNaznKm)
                return row.olPzvDateNaznKm == null
                    ? PzvActionType.AssignKnittingMachine
                    : PzvActionType.CancelKnittingMachine;

            if (column == gridPZVOperListColumnOlPzvTab || column == gridPZVOperListColumnOlPzvDateNaznTab)
                return row.olPzvDateNaznTab == null
                    ? PzvActionType.AssignTab
                    : PzvActionType.CancelTab;

            if (column == gridPZVOperListColumnOlPzvDateStart)
                return row.olPzvDateStart == null
                    ? PzvActionType.StartWork
                    : PzvActionType.CancelStartWork;

            if (column == gridPZVOperListColumnOlPzvDateEnd)
                return row.olPzvDateEnd == null
                    ? PzvActionType.StopWork
                    : PzvActionType.CancelStopWork;

            if (column == gridPZVOperListColumnOlPzvDateMast)
                return row.olPzvDateMast == null
                    ? PzvActionType.ConfirmMaster
                    : PzvActionType.CancelMasterConfirmation;

            return null;
        }
        //private void gridViewPZVOperList_DoubleClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var view = (GridView)sender;
        //        var pt = view.GridControl.PointToClient(Control.MousePosition);
        //        var hit = view.CalcHitInfo(pt);

        //        if (!hit.InRowCell && !hit.InColumnPanel)
        //            return;

        //        if (hit.InColumnPanel && hit.Column == gridPZVOperListColumnSyncSelection)
        //        {
        //            int xSelected = Convert.ToInt32(gridViewPZVOperList.GetRowCellValue(0, gridPZVOperListColumnSyncSelection));
        //            int newValue = xSelected == 0 ? 1 : 0;
        //            _gridHelper.SetValueForFilteredRecordsInGrid(gridViewPZVOperList, gridPZVOperListColumnSyncSelection, newValue);
        //            return;
        //        }

        //        if (!hit.InRowCell || hit.RowHandle < 0)
        //            return;

        //        var current = _pZVOperListByPachListBindingSource.Current as PZVOperList;
        //        if (current == null)
        //            return;

        //        current.SyncSelection = 1;
        //        _xPzvID = current.olPzvID;
        //        _xColumn = hit.Column?.FieldName ?? string.Empty;

        //        var action = ResolveDoubleClickAction(hit.Column, current);
        //        if (action == null)
        //            return;

        //        var context = _contextBuilder.Build();
        //        var result = await _actionRouter.ExecuteAsync(action.Value, context, CancellationToken.None);

        //        if (!result.Success && !string.IsNullOrWhiteSpace(result.ErrorMessage))
        //            MessageBox.Show(result.ErrorMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в gridViewPZVOperList_DoubleClick: {ex.Message}");
        //    }
        //    //try
        //    //{
        //    //    var view = sender as GridView;
        //    //    if (view == null) return;

        //    //    Point pt = view.GridControl.PointToClient(MousePosition);
        //    //    GridHitInfo hit = view.CalcHitInfo(pt);
        //    //    int tab = Convert.ToInt32(view.GetRowCellValue(hit.RowHandle, gridPZVOperListColumnOlPzvTab));
        //    //    _xColumn = view.FocusedColumn.FieldName;
        //    //    _xPzvID = Convert.ToInt32(view.GetRowCellValue(hit.RowHandle, gridPZVOperListColumnOlPzvID));

        //    //    DateTime _OlPvDateNaznKm = Convert.ToDateTime(view.GetRowCellValue(hit.RowHandle, gridPZVOperListColumnOlPvDateNaznKm));
        //    //    DateTime _OlPvDateNaznTab = Convert.ToDateTime(view.GetRowCellValue(hit.RowHandle, gridPZVOperListColumnOlPzvDateNaznTab));
        //    //    DateTime _OlPvDateStart = Convert.ToDateTime(view.GetRowCellValue(hit.RowHandle, gridPZVOperListColumnOlPzvDateStart));
        //    //    DateTime _OlPvDateEnd = Convert.ToDateTime(view.GetRowCellValue(hit.RowHandle, gridPZVOperListColumnOlPzvDateEnd));
        //    //    DateTime _OlPvDateMast = Convert.ToDateTime(view.GetRowCellValue(hit.RowHandle, gridPZVOperListColumnOlPzvDateMast));
        //    //    int _olNpach = Convert.ToInt32(view.GetRowCellValue(hit.RowHandle, gridPZVOperListColumnOlNPach));
        //    //    string _olNomOper = Convert.ToString(view.GetRowCellValue(hit.RowHandle, gridPZVOperListColumnOlNomOper));
        //    //    string _olOperName = Convert.ToString(view.GetRowCellValue(hit.RowHandle, gridPZVOperListColumnOlOperName));

        //    //    Debug.WriteLine($"DoubleClick started at row {hit.RowHandle}, column {hit.Column.FieldName}, pzvID={_xPzvID}, tab={tab}, OlPvDateNaznKm={_OlPvDateNaznKm}, OlPvDateNaznTab={_OlPvDateNaznTab}, OlPvDateStart={_OlPvDateStart}, OlPvDateEnd={_OlPvDateEnd}, OlPvDateMast={_OlPvDateMast}, olNpach={_olNpach}, olNomOper={_olNomOper}, olOperName={_olOperName}, TopRowIndex={view.TopRowIndex}, TopRowIndexPZV={TopRowIndexPZV}");
        //    //    //var view = (GridView)sender;
        //    //    //// где именно кликнули
        //    //    //var pt = view.GridControl.PointToClient(Control.MousePosition);
        //    //    //var hit = view.CalcHitInfo(pt);



        //    //    if (hit.InRowCell && (hit.Column == gridPZVOperListColumnOlKmlNumber || hit.Column == gridPZVOperListColumnOlPvDateNaznKm) && hit.RowHandle >= 0)
        //    //    {
        //    //        var pzvOperList = _pZVOperListByPachListBindingSource.Current as PZVOperList;
        //    //        if (pzvOperList == null) return;
        //    //        pzvOperList.SyncSelection = 1;
        //    //        if (pzvOperList.olPzvDateNaznKm == null)
        //    //        {
        //    //            KnittingMachineWorkAssignment();
        //    //        }
        //    //        else
        //    //        {
        //    //            KnittingMachineCancelWorkAssignment();
        //    //        }
        //    //    }

        //    //    if (hit.InRowCell && (hit.Column == gridPZVOperListColumnOlPzvTab || hit.Column == gridPZVOperListColumnOlPzvDateNaznTab) && hit.RowHandle >= 0)
        //    //    {
        //    //        var pzvOperList = _pZVOperListByPachListBindingSource.Current as PZVOperList;
        //    //        if (pzvOperList == null) return;
        //    //        pzvOperList.SyncSelection = 1;
        //    //        if (pzvOperList.olPzvDateNaznTab == null)
        //    //        {
        //    //            TabWorkAssignment();
        //    //        }
        //    //        else
        //    //        {
        //    //            TabCancelWorkAssignment();
        //    //        }
        //    //    }

        //    //    if (hit.InRowCell && hit.Column == gridPZVOperListColumnOlPzvDateStart && hit.RowHandle >= 0)
        //    //    {
        //    //        var pzvOperList = _pZVOperListByPachListBindingSource.Current as PZVOperList;
        //    //        if (pzvOperList == null) return;
        //    //        pzvOperList.SyncSelection = 1;
        //    //        if (pzvOperList.olPzvDateStart == null)
        //    //        {
        //    //            WorkStartExecution();
        //    //        }
        //    //        else
        //    //        {
        //    //            CancelWorkStartExecution();
        //    //        }
        //    //    }

        //    //    if (hit.InRowCell && hit.Column == gridPZVOperListColumnOlPzvDateEnd && hit.RowHandle >= 0)
        //    //    {
        //    //        var pzvOperList = _pZVOperListByPachListBindingSource.Current as PZVOperList;
        //    //        if (pzvOperList == null) return;
        //    //        pzvOperList.SyncSelection = 1;
        //    //        if (pzvOperList.olPzvDateEnd == null)
        //    //        {
        //    //            WorkStopExecution();
        //    //        }
        //    //        else
        //    //        {
        //    //            CancelWorkStopExecution();
        //    //        }
        //    //    }

        //    //    if (hit.InRowCell && hit.Column == gridPZVOperListColumnOlPzvDateMast && hit.RowHandle >= 0)
        //    //    {
        //    //        var pzvOperList = _pZVOperListByPachListBindingSource.Current as PZVOperList;
        //    //        if (pzvOperList == null) return;
        //    //        pzvOperList.SyncSelection = 1;
        //    //        if (pzvOperList.olPzvDateMast == null)
        //    //        {
        //    //            MasterConfirmation();
        //    //        }
        //    //        else
        //    //        {
        //    //            MasterCancelConfirmation();
        //    //        }
        //    //    }

        //    //    if (hit.InColumnPanel && hit.Column == gridPZVOperListColumnSyncSelection)
        //    //    {
        //    //        int xSelected = Convert.ToInt32(gridViewPZVOperList.GetRowCellValue(0, gridPZVOperListColumnSyncSelection));
        //    //        int newValue = xSelected == 0 ? 1 : 0;

        //    //        _gridHelper.SetValueForFilteredRecordsInGrid(gridViewPZVOperList, gridPZVOperListColumnSyncSelection, newValue);

        //    //        //view.BeginUpdate();
        //    //        //view.GridControl.BeginUpdate();
        //    //        //try
        //    //        //{
        //    //        //    Enumerable.Range(0, view.RowCount)
        //    //        //    .Where(view.IsDataRow) // отсекаем group rows и прочие
        //    //        //    .ToList()
        //    //        //    .ForEach(rh =>
        //    //        //    {
        //    //        //        view.SetRowCellValue(rh, gridColumnPZVOperListSyncSelection, newValue);
        //    //        //        //view.PostEditor();
        //    //        //    });
        //    //        //    view.PostEditor();
        //    //        //    view.UpdateCurrentRow();
        //    //        //}
        //    //        //finally
        //    //        //{
        //    //        //    view.GridControl.EndUpdate();
        //    //        //    view.EndUpdate();
        //    //        //}
        //    //    }

        //    //    // нужен именно заголовок колонки
        //    //    //if (!hit.InColumnPanel || hit.Column == null)
        //    //    //    return;

        //    //    // только для нужной колонки
        //    //    //if (hit.Column != gridColumnPZVOperListSyncSelection)
        //    //    //    return;

        //    //    // значение берём у текущей строки
        //    //    Debug.WriteLine($"DoubleClick finished at row {hit.RowHandle}, column {hit.Column.FieldName}, pzvID={_xPzvID}, tab={tab}, OlPvDateNaznKm={_OlPvDateNaznKm}, OlPvDateNaznTab={_OlPvDateNaznTab}, OlPvDateStart={_OlPvDateStart}, OlPvDateEnd={_OlPvDateEnd}, OlPvDateMast={_OlPvDateMast}, olNpach={_olNpach}, olNomOper={_olNomOper}, olOperName={_olOperName}, TopRowIndex={view.TopRowIndex}, TopRowIndexPZV={TopRowIndexPZV}");
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    MessageBox.Show($"Ошибка в gridViewPZVOperList_DoubleClick: {ex.Message}");
        //    //}
        //}
        //public void SetDateStartToPzvID(List<int> _pzvId, DateTime? _dateStart)
        //{
        //    //MessageBox.Show($"Двойной клик по операции. В/М {curr.kmlNumber} ({curr.kmlInvNum}) - Зона {curr.kmaNumber}");
        //    try
        //    {
        //        // получаем список строк (pzvID) для присовения машины (kmlID)
        //        var idSet = _pzvId != null ? new HashSet<int>(_pzvId) : new HashSet<int>();

        //        var recordsToUpdate = _pZVOperListByPachListBindingSource.List
        //            .Cast<PZVOperList>()
        //            .Where(r => r != null && idSet.Contains(r.olPzvID))
        //            .ToList();
        //        foreach (var record in recordsToUpdate)
        //        {
        //            record.olPzvDateStart = _dateStart;
        //            record.IsModified = true;
        //        }
        //        // Получаем список строк с флагом IsModified = true
        //        List<PZV> filteredList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x?.IsModified == true)
        //            .Select(x => x.ToPZV())   // новый маппер
        //            .ToList();
        //        // Преобразуем в BindingList
        //        if (filteredList.Count > 0)
        //        {
        //            MutePlanBrokerNotifications();
        //            using (SqlConnection connection = _dbHelper.GetConnection())
        //            {
        //                _bulkHelper.BulkAllDataUpdate(connection, filteredList, "planZagrVyaz", new[] { "pzvID" });
        //                // удалить из BindingSource
        //                //_pZVOperListByPachListBindingSource.RemoveModified<PZVOperList>();
        //                foreach (var record in recordsToUpdate)
        //                {
        //                    record.IsModified = false;
        //                    record.SyncSelection = 0;
        //                }
        //                LoadPlanZagrVyazByZadanySelection();
        //            }
        //        }
        //        //_pZVOperListByPachListBindingSource.ResetBindings(false);
        //        //gridViewPZVOperList.RefreshData();
        //        //gridViewPZVOperList.TopRowIndex = TopRowIndexPZV;

        //        Debug.WriteLine($"SetDateStartToPzvID completed for pzvIDs={string.Join(",", _pzvId)}, dateStart={_dateStart}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка заполнения даты начала вязания SetDateStartToPzvID: {ex.Message}");
        //        throw;
        //    }

        //}
        //private void SetDateEndToPzvID(List<int> _pzvId, DateTime? _date)
        //{
        //    //MessageBox.Show($"Двойной клик по операции. В/М {curr.kmlNumber} ({curr.kmlInvNum}) - Зона {curr.kmaNumber}");
        //    try
        //    {
        //        // получаем список строк (pzvID) для присовения машины (kmlID)
        //        var idSet = _pzvId != null ? new HashSet<int>(_pzvId) : new HashSet<int>();

        //        var recordsToUpdate = _pZVOperListByPachListBindingSource.List
        //            .Cast<PZVOperList>()
        //            .Where(r => r != null && idSet.Contains(r.olPzvID))
        //            .ToList();
        //        foreach (var record in recordsToUpdate)
        //        {
        //            record.olPzvDateEnd = _date;
        //            record.IsModified = true;
        //        }
        //        // Получаем список строк с флагом IsModified = true
        //        List<PZV> filteredList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x?.IsModified == true)
        //            .Select(x => x.ToPZV())   // новый маппер
        //            .ToList();
        //        // Преобразуем в BindingList
        //        if (filteredList.Count > 0)
        //        {
        //            MutePlanBrokerNotifications();
        //            using (SqlConnection connection = _dbHelper.GetConnection())
        //            {
        //                _bulkHelper.BulkAllDataUpdate(connection, filteredList, "planZagrVyaz", new[] { "pzvID" });
        //                // удалить из BindingSource
        //                foreach (var record in recordsToUpdate)
        //                {
        //                    record.IsModified = false;
        //                    record.SyncSelection = 0;
        //                }
        //                LoadPlanZagrVyazByZadanySelection();
        //            }
        //        }
        //        //_pZVOperListByPachListBindingSource.ResetBindings(false);
        //        //gridViewPZVOperList.RefreshData();
        //        //gridViewPZVOperList.TopRowIndex = TopRowIndexPZV;
        //        Debug.WriteLine($"SetDateEndToPzvID completed for pzvIDs={string.Join(",", _pzvId)}, dateEnd={_date}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка заполнения даты окончания вязания SetDateEndToPzvID: {ex.Message}");
        //        throw;
        //    }

        //}

        //private void SetDateMastToPzvID(List<int> _pzvId, DateTime? _date)
        //{
        //    //MessageBox.Show($"Двойной клик по операции. В/М {curr.kmlNumber} ({curr.kmlInvNum}) - Зона {curr.kmaNumber}");
        //    try
        //    {
        //        // получаем список строк (pzvID) для присовения машины (kmlID)
        //        var idSet = _pzvId != null ? new HashSet<int>(_pzvId) : new HashSet<int>();

        //        var recordsToUpdate = _pZVOperListByPachListBindingSource.List
        //            .Cast<PZVOperList>()
        //            .Where(r => r != null && idSet.Contains(r.olPzvID))
        //            .ToList();
        //        foreach (var record in recordsToUpdate)
        //        {
        //            record.olPzvDateMast = _date;
        //            record.IsModified = true;
        //        }
        //        // Получаем список строк с флагом IsModified = true
        //        List<PZV> filteredList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x?.IsModified == true)
        //            .Select(x => x.ToPZV())   // новый маппер
        //            .ToList();
        //        // Преобразуем в BindingList
        //        if (filteredList.Count > 0)
        //        {
        //            MutePlanBrokerNotifications();
        //            using (SqlConnection connection = _dbHelper.GetConnection())
        //            {
        //                _bulkHelper.BulkAllDataUpdate(connection, filteredList, "planZagrVyaz", new[] { "pzvID" });
        //                // удалить из BindingSource
        //                foreach (var record in recordsToUpdate)
        //                {
        //                    record.IsModified = false;
        //                    record.SyncSelection = 0;
        //                }
        //                LoadPlanZagrVyazByZadanySelection();
        //            }
        //        }
        //        //_pZVOperListByPachListBindingSource.ResetBindings(false);
        //        //gridViewPZVOperList.RefreshData();
        //        //gridViewPZVOperList.TopRowIndex = TopRowIndexPZV;
        //        Debug.WriteLine($"SetDateMastToPzvID completed for pzvIDs={string.Join(",", _pzvId)}, dateMast={_date}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка заполнения даты подтверждения мастером SetDateMastToPzvID: {ex.Message}");
        //        throw;
        //    }
        //}

        //private void SetKnitMachineToPzvID(List<int> _pzvId, int _kmlID)
        //{
        //    //MessageBox.Show($"Двойной клик по операции. В/М {curr.kmlNumber} ({curr.kmlInvNum}) - Зона {curr.kmaNumber}");
        //    try
        //    {
        //        // получаем список строк (pzvID) для присовения машины (kmlID)
        //        var idSet = _pzvId != null ? new HashSet<int>(_pzvId) : new HashSet<int>();

        //        var recordsToUpdate = _pZVOperListByPachListBindingSource.List
        //            .Cast<PZVOperList>()
        //            .Where(r => r != null && idSet.Contains(r.olPzvID))
        //            .ToList();
        //        foreach (var record in recordsToUpdate)
        //        {
        //            record.olPzvKmlID = _kmlID;
        //            record.olPzvDateNaznKm = _kmlID == 0 ? null : DateTime.Now;
        //            record.IsModified = true;
        //        }
        //        // Получаем список строк с флагом IsModified = true
        //        List<PZV> filteredList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x?.IsModified == true)
        //            .Select(x => x.ToPZV())   // новый маппер
        //            .ToList();
        //        // Преобразуем в BindingList
        //        if (filteredList.Count > 0)
        //        {
        //            MutePlanBrokerNotifications();
        //            using (SqlConnection connection = _dbHelper.GetConnection())
        //            {
        //                _bulkHelper.BulkAllDataUpdate(connection, filteredList, "planZagrVyaz", new[] { "pzvID" });

        //                // удалить из BindingSource
        //                //_pZVOperListByPachListBindingSource.RemoveModified<PZVOperList>();
        //                foreach (var record in recordsToUpdate)
        //                {
        //                    record.IsModified = false;
        //                    record.SyncSelection = 0;
        //                }
        //                LoadPlanZagrVyazByZadanySelection();
        //            }
        //        }
        //        //_pZVOperListByPachListBindingSource.ResetBindings(false);
        //        //gridViewPZVOperList.RefreshData();
        //        Debug.WriteLine($"SetKnitMachineToPzvID completed for pzvIDs={string.Join(",", _pzvId)}, kmlID={_kmlID}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка присвоения машины SetKnitMachineToPzvID: {ex.Message}");
        //        throw;
        //    }

        //}

        /// <summary>
        /// Назначить на В/М по V для выбранных операций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private async void KnittingMachineWorkAssignment()
        //{
        //    try
        //    {
        //        Debug.WriteLine($"KnittingMachineWorkAssignment started for pzvID={_xPzvID}, column={_xColumn}");
        //        //SmenZadanyVyaz curr = _smenZadanyVyazBindingSource.Current as SmenZadanyVyaz;
        //        SmenZadanyVyaz curr = advBandedGridViewSmenZadany.GetRow(advBandedGridViewSmenZadany.FocusedRowHandle) as SmenZadanyVyaz;
        //        if (curr == null || curr.kwsmlKmlID == null || curr.kwsmlKmlID == 0)
        //        {
        //            MessageBox.Show("Не выбрана машина для назначения");
        //            return;
        //        }
        //        var checkList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.SyncSelection == 1)
        //            .ToList();
        //        if (checkList.Count != 0)
        //        {
        //            foreach (var record in checkList)
        //            {
        //                if (record.olKodPodr != 1)
        //                {
        //                    MessageBox.Show("Операция не относится к вязальному подразделению, нельзя изменять В/М!");
        //                    return;
        //                }
        //                string query = @"SELECT pszm.pszkmKmlID, mlv.kmlNumber, mlv.name_class " +
        //                    @"FROM plan_sezon_zad_knitMachine pszm " +
        //                    @"  LEFT JOIN knitMachineList_view mlv ON pszm.pszkmKmlID = mlv.kmlID " +
        //                    @"WHERE pszm.pszkmPszNom = @PszNom and pszkmKnitClass = @KnitClass";
        //                DataTable machineInfo = _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@PszNom", record.olNomZad }, { "@KnitClass", record.olIdVyazClass } });

        //                int _kmlID = 0;
        //                string _kmlNumber = string.Empty;
        //                int _vyazClass = 0;
        //                if (machineInfo.Rows.Count == 0)
        //                {
        //                    _kmlID = 0;
        //                    _kmlNumber = string.Empty;
        //                    _vyazClass = 0;
        //                }
        //                else
        //                {
        //                    _kmlID = Convert.ToInt32(machineInfo.Rows[0]["pszkmKmlID"]);
        //                    _kmlNumber = Convert.ToString(machineInfo.Rows[0]["kmlNumber"]);
        //                    _vyazClass = Convert.ToInt32(machineInfo.Rows[0]["name_class"]);
        //                }

        //                if (_kmlID != curr.kwsmlKmlID && record.olPzvKmlID == 0)
        //                {
        //                    string button1Text = $"Продолжить: назначить В/М \n оп. {record.olNomOper} {record.olOperName}";
        //                    string button2Text = $"Пропустить: НЕ назначать В/М \n оп. {record.olNomOper} {record.olOperName}";
        //                    string button3Text = checkList.Count == 1 ? "" : "Прервать назначение В/М на выбранные операции";
        //                    var ButtonsList = new Dictionary<string, DialogResult>();
        //                    if (checkList.Count == 1)
        //                    {
        //                        ButtonsList = new Dictionary<string, DialogResult>
        //                        {
        //                            { button1Text, DialogResult.Yes },
        //                            { button2Text, DialogResult.No }
        //                        };
        //                    }
        //                    else if (checkList.Count > 1)
        //                    {
        //                        ButtonsList = new Dictionary<string, DialogResult>
        //                        {
        //                            { button1Text, DialogResult.Yes },
        //                            { button2Text, DialogResult.No },
        //                            { button3Text, DialogResult.Abort }
        //                        };
        //                    }

        //                    // Создаем опции с полным контролем
        //                    var options = new MessageBoxOptions
        //                    {
        //                        Text = $"Внимание!\nОперация: 1/5\nОписание: Вязание воротник\n\n" +
        //                               $"Назначаемая машина не совпадает с плановой:" +
        //                               $"\n плановая В/М№ {_kmlNumber} - класс {_vyazClass}" +
        //                               $"\n назначаемая В/М№ {curr.kmlNumber} - класс {curr.nameVyazClass}",
        //                        Caption = "Назначение В/М на операцию",
        //                        Buttons = ButtonsList,
        //                        Icon = MessageBoxIcon.Warning,
        //                        ButtonLayout = ButtonLayout.Vertical, // Вертикальное расположение
        //                        StretchVerticalButtons = true, // Растянуть кнопки по ширине
        //                        VerticalButtonSpacing = 15, // Больше расстояние между кнопками
        //                        ButtonHeight = 45, // Высота кнопок для многострочного текста
        //                        ButtonPadding = 20 // Отступы внутри кнопок
        //                    };
        //                    var _result = AdvancedMessageBox.Show(options);

        //                    //// Вызов с явным указанием вертикального расположения
        //                    //var _result = AdvancedMessageBox.Show(
        //                    //    message: $"Внимание!\nОперация: 1/5\nОписание: Вязание воротник\n\n" +
        //                    //             $"Назначаемая машина (в2193) не совпадает с плановой (в1615)",
        //                    //    caption: "Назначение В/М на операцию",
        //                    //    buttons: ButtonsList,
        //                    //    icon: MessageBoxIcon.Warning,
        //                    //    layout: ButtonLayout.Vertical // Явно указываем вертикальное расположение
        //                    //);



        //                    //var _result = AdvancedMessageBox.Show(
        //                    //    $"Внимание!\n" +
        //                    //    $"Операция: 1/5\n" +
        //                    //    $"Описание: Вязание воротник\n\n" +
        //                    //    $"Назначаемая машина (в2193) не совпадает с плановой (в1615)",
        //                    //    "Назначение В/М на операцию",
        //                    //    new Dictionary<string, DialogResult>
        //                    //    {
        //                    //        { "Продолжить: назначить В/М для оп. 1/5 Вязание воротник", DialogResult.Yes },
        //                    //        { "Пропустить: НЕ назначать В/М для оп. 1/5 Вязание воротник", DialogResult.No },
        //                    //        { "Отмена", DialogResult.Cancel }
        //                    //    }
        //                    //);

        //                    //// Или используйте специализированный метод
        //                    //var _result2 = AdvancedMessageBox.ShowMachineAssignment(
        //                    //    "1/5",
        //                    //    "Вязание воротник",
        //                    //    "в2193",
        //                    //    "в1615"
        //                    //);

        //                    //// Или даже проще
        //                    //var _result3 = AdvancedMessageBox.ShowConfirmation("Вы уверены?");
        //                    //var _result4 = AdvancedMessageBox.ShowWarning("Что-то пошло не так!");

        //                    //var _result = AdvancedMessageBox.ShowMachineAssignmentWarning(
        //                    //    $"Внимание!\n" +
        //                    //    $"Операция: 1/5\n" +
        //                    //    $"Описание: Вязание воротник\n\n" +
        //                    //    $"Назначаемая машина (в2193) не совпадает с плановой (в1615)",
        //                    //    "Назначение В/М на операцию",
        //                    //    ButtonsList
        //                    //);

        //                    //var _result = AdvancedMessageBox.ShowWithMultilineButtons(new MessageBoxOptions
        //                    //{
        //                    //    Text = $"Внимание! " +
        //                    //        $"\n Операция: {record.olNomOper} " +
        //                    //        $"\n Описание: {record.olOperName} " +
        //                    //        $"\n\n Назначаемая машина ({curr.kmlNumber}) не совпадает с плановой ({_kmlNumber}) ",
        //                    //    Caption = "Назначение В/М на операцию",
        //                    //    Buttons = ButtonsList,
        //                    //    Icon = MessageBoxIcon.Warning,
        //                    //    Width = 600,
        //                    //    //TextFont = new Font("Segoe UI", 10f),
        //                    //    TextFont = ThemeManager.SharedSettings.DefaultFont,
        //                    //    BackColor = this.BackColor
        //                    //});

        //                    //var _result = AdvancedMessageBox.ShowWithOptimalButtons(new MessageBoxOptions
        //                    //{
        //                    //    Text = $"Внимание! " +
        //                    //        $"\n Операция: {record.olNomOper} " +
        //                    //        $"\n Описание: {record.olOperName} " +
        //                    //        $"\n\n Назначаемая машина ({curr.kmlNumber}) не совпадает с плановой ({_kmlNumber}) ",
        //                    //    Caption = "Назначение В/М на операцию",
        //                    //    Buttons = ButtonsList
        //                    //});
        //                    ////var _result = AdvancedMessageBox.ShowWithOptimalButtons(
        //                    ////    text: $"Внимание! " +
        //                    ////          $"\nОперация: {record.olNomOper} " +
        //                    ////          $"\nОписание: {record.olOperName} " +
        //                    ////          $"\n\nНазначаемая машина ({curr.kmlNumber}) не совпадает с плановой ({_kmlNumber})",
        //                    ////    caption: "Назначение В/М на операцию",
        //                    ////    buttons: buttons
        //                    ////);
        //                    switch (_result)
        //                    {
        //                        case DialogResult.No:
        //                            record.ErrorSelection = 1;
        //                            record.SyncSelection = 0;
        //                            break;
        //                        case DialogResult.Abort:
        //                            return;
        //                        //break;
        //                        case DialogResult.Cancel:
        //                            return;
        //                            //break;
        //                    }
        //                    //if (_result == DialogResult.No)
        //                    //{
        //                    //    return;
        //                    //}
        //                }
        //                if (!CheckPZVDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя назначить В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя назначить В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя назначить В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя назначить В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя назначить В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //            }
        //        }

        //        PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

        //        List<int> filteredList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.SyncSelection == 1)
        //            .Select(x => x.olPzvID)   // новый маппер
        //            .ToList();
        //        //SetKnitMachineToPzvID(filteredList, curr.kwsmlKmlID);
        //        await ExecutePzvActionAsync(PzvActionType.AssignKnittingMachine);

        //        var errList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.ErrorSelection == 1)
        //            .ToList();
        //        if (errList.Count != 0)
        //        {
        //            foreach (var record in errList)
        //            {
        //                if (record.ErrorSelection == 1 && record.SyncSelection == 0)
        //                {
        //                    record.ErrorSelection = 0;
        //                    record.SyncSelection = 1;
        //                }
        //            }
        //        }
        //        Debug.WriteLine($"KnittingMachineWorkAssignment completed for pzvIDs={string.Join(",", filteredList)}, kmlID={curr.kwsmlKmlID}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в KnittingMachineWorkAssignment: {ex.Message}");
        //    }
        //}

        //private async void buttonKnittingMachineWorkAssignment_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        await GridOverlayLoader.RunTaskWithOverlayAsync(
        //            gridControlPZVOperList,
        //            KnittingMachineWorkAssignment,              // обычный void-метод
        //            CancellationToken.None);
        //        Debug.WriteLine($"Finished KnittingMachineWorkAssignment, now going to row with pzvID={_xPzvID}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в buttonKnittingMachineWorkAssignment_Click: {ex.Message}");
        //    }
        //}

        /// <summary>
        /// Отменить назначение по В/М по V для выбранных операций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private async void KnittingMachineCancelWorkAssignment()
        //{
        //    try
        //    {
        //        Debug.WriteLine($"KnittingMachineCancelWorkAssignment started for pzvID={_xPzvID}, column={_xColumn}");
        //        var checkList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.SyncSelection == 1)
        //            .ToList();
        //        if (checkList.Count != 0)
        //        {
        //            foreach (var record in checkList)
        //            {
        //                if (record.olKodPodr != 1)
        //                {
        //                    MessageBox.Show("Операция не относится к вязальному подразделению, нельзя изменять В/М!");
        //                    return;
        //                }
        //                if (!CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя отменить назначение В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя отменить назначение В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя отменить назначение В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя отменить назначение В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя отменить назначение В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }

        //            }
        //        }

        //        List<int> filteredList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.SyncSelection == 1)
        //            .Select(x => x.olPzvID)   // новый маппер
        //            .ToList();

        //        //SetKnitMachineToPzvID(filteredList, 0);
        //        await ExecutePzvActionAsync(PzvActionType.CancelKnittingMachine);

        //        Debug.WriteLine($"KnittingMachineCancelWorkAssignment completed for pzvIDs={string.Join(",", filteredList)}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в KnittingMachineCancelWorkAssignment: {ex.Message}");
        //    }
        //}

        //private async void buttonKnittingMachineCancelWorkAssignment_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        await GridOverlayLoader.RunTaskWithOverlayAsync(
        //            gridControlPZVOperList,
        //            KnittingMachineCancelWorkAssignment,              // обычный void-метод
        //            CancellationToken.None);
        //        _gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
        //        Debug.WriteLine($"Finished KnittingMachineCancelWorkAssignment, now going to row with pzvID={_xPzvID}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в buttonKnittingMachineCancelWorkAssignment_Click: {ex.Message}");
        //    }
        //}

        /// <summary>
        /// Проставить табельный номер 999 по V для выбранных операций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private async void buttonTab999WorkAssignment_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Debug.WriteLine($"Starting Tab999WorkAssignment for pzvID={_xPzvID}, column={_xColumn}");
        //        await GridOverlayLoader.RunTaskWithOverlayAsync(
        //            gridControlPZVOperList,
        //            Tab999WorkAssignment,              // обычный void-метод
        //            CancellationToken.None);
        //        //GoToPzvID(_xPzvID, _xColumn);
        //        _gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
        //        Debug.WriteLine($"Finished Tab999WorkAssignment, now going to row with pzvID={_xPzvID}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в buttonTab999WorkAssignment_Click: {ex.Message}");
        //    }
        //}
        //private async void Tab999WorkAssignment()
        //{
        //    try
        //    {
        //        Debug.WriteLine($"Starting Tab999WorkAssignment for pzvID={_xPzvID}, column={_xColumn}");
        //        var checkList = _pZVOperListByPachListBindingSource.List
        //                .OfType<PZVOperList>()
        //                .Where(x => x.SyncSelection == 1)
        //                .ToList();
        //        //if (checkList.Count != 0)
        //        //{
        //        //    foreach (var record in checkList)
        //        //    {
        //        //        if (!CheckPZVDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя проставить таб№ 999 (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //        //        {
        //        //            record.ErrorSelection = 1;
        //        //            record.SyncSelection = 0;
        //        //            continue;
        //        //        }
        //        //        if (!CheckPZVDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя проставить таб№ 999 (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //        //        {
        //        //            record.ErrorSelection = 1;
        //        //            record.SyncSelection = 0;
        //        //            continue;
        //        //        }
        //        //        if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя проставить таб№ 999 (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //        //        {
        //        //            record.ErrorSelection = 1;
        //        //            record.SyncSelection = 0;
        //        //            continue;
        //        //        }
        //        //        if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя проставить таб№ 999 (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //        //        {
        //        //            record.ErrorSelection = 1;
        //        //            record.SyncSelection = 0;
        //        //            continue;
        //        //        }

        //        //    }
        //        //}

        //        PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

        //        //List<int> filteredList = _pZVOperListByPachListBindingSource.List
        //        //    .OfType<PZVOperList>()
        //        //    .Where(x => x.SyncSelection == 1)
        //        //    .Select(x => x.olPzvID)   // новый маппер
        //        //    .ToList();
        //        if (checkList.Count != 0)
        //        {
        //            foreach (var record in checkList)
        //            {
        //                if (record.olKodPodr != 1 || record.olKodPodr == 1 && !CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя проставить таб№ 999 (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя проставить таб№ 999 (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя проставить таб№ 999 (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя проставить таб№ 999 (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }

        //            }
        //        }

        //        //PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

        //        List<int> filteredList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.SyncSelection == 1)
        //            .Select(x => x.olPzvID)   // новый маппер
        //            .ToList();
        //        //await 
        //        //SetTabToPzvID(filteredList, 999, 0);
        //        await ExecutePzvActionAsync(PzvActionType.AssignTab999);
        //        //gridViewPZVOperList.RefreshData();
        //        //GoToPzvID(pzvCurrent.olPzvID, _xColumn);
        //        Debug.WriteLine($"Finished Tab999WorkAssignment, now going to row with pzvID={_xPzvID}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка Tab999WorkAssignment: {ex.Message}");
        //    }
        //}
        //private async Task SetTabToPzvID(List<int> _pzvId, int _tab, int _kwsID)
        //{
        //    //MessageBox.Show($"Двойной клик по операции. В/М {curr.kmlNumber} ({curr.kmlInvNum}) - Зона {curr.kmaNumber}");
        //    try
        //    {
        //        Debug.WriteLine($"Starting SetTabToPzvID for pzvIDs={string.Join(",", _pzvId)}, tab={_tab}, kwsID={_kwsID}");
        //        // получаем список строк (pzvID) для присовения машины (kmlID)
        //        var idSet = _pzvId != null ? new HashSet<int>(_pzvId) : new HashSet<int>();

        //        var recordsToUpdate = _pZVOperListByPachListBindingSource.List
        //            .Cast<PZVOperList>()
        //            .Where(r => r != null && idSet.Contains(r.olPzvID))
        //            .ToList();
        //        foreach (var record in recordsToUpdate)
        //        {
        //            record.olPzvTab = _tab;
        //            record.olPzvKwsID = _tab == 0 ? 0 : _kwsID;
        //            record.olPzvDateNaznTab = _tab == 0 ? null : DateTime.Now;
        //            record.olPzvSekNazn = _tab == 0 ? 0 : record.olSekEd;
        //            record.olPzvKolNazn = _tab == 0 ? 0 : record.olKol;
        //            record.olPzvChasNazn = _tab == 0 ? 0 : record.olPzvNChasi;
        //            if (_tab == 999)
        //            {
        //                record.olPzvDateStart = DateTime.Now;
        //                record.olPzvDateEnd = DateTime.Now;
        //                record.olPzvDateML = DateTime.Now;
        //                record.olPzvDateMast = DateTime.Now;
        //            }
        //            record.IsModified = true;
        //        }
        //        // Получаем список строк с флагом IsModified = true
        //        List<PZV> filteredList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x?.IsModified == true)
        //            .Select(x => x.ToPZV())   // новый маппер
        //            .ToList();
        //        // Преобразуем в BindingList
        //        if (filteredList.Count > 0)
        //        {
        //            MutePlanBrokerNotifications();
        //            using (SqlConnection connection = _dbHelper.GetConnection())
        //            {
        //                _bulkHelper.BulkAllDataUpdate(connection, filteredList, "planZagrVyaz", new[] { "pzvID" });
        //                try
        //                {
        //                    _pZVOperListByPachListBindingSource.EndEdit();
        //                    PZVOperList[] toRemove = _pZVOperListByPachListBindingSource.List
        //                        .OfType<PZVOperList>()
        //                        .Where(x => x.IsModified)
        //                        .ToArray();
        //                    Debug.WriteLine(toRemove.Length);
        //                    Debug.WriteLine(toRemove.Where(x => x.IsModified));
        //                    foreach (var record in toRemove)
        //                    {
        //                        record.IsModified = false;
        //                        record.SyncSelection = 0;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show(ex.Message, "Ошибка при удалении SetTabToPzvID");
        //                }

        //                LoadPlanZagrVyazByZadanySelection();
        //            }
        //        }
        //        Debug.WriteLine($"SetTabToPzvID completed for pzvIDs={string.Join(",", _pzvId)}, tab={_tab}, kwsID={_kwsID}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка присвоения табельного номера SetTabToPzvID: {ex.Message}");
        //        throw;
        //    }

        //}

        /// <summary>
        /// Подтвердить мастером по V для выбранных операций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private async void MasterConfirmation()
        //{
        //    try
        //    {
        //        Debug.WriteLine($"Starting MasterConfirmation for pzvID={_xPzvID}, column={_xColumn}");
        //        _pZVOperListByPachListBindingSource.ResetBindings(false);
        //        var checkList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.SyncSelection == 1)
        //            .ToList();
        //        if (checkList.Count != 0)
        //        {
        //            foreach (var record in checkList)
        //            {
        //                if (record.olKodPodr == 1 && !CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVEmptyDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVEmptyDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVEmptyDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }

        //            }
        //        }

        //        PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

        //        List<int> filteredList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.SyncSelection == 1)
        //            .Select(x => x.olPzvID)   // новый маппер
        //            .ToList();
        //        //SetDateMastToPzvID(filteredList, DateTime.Now);
        //        await ExecutePzvActionAsync(PzvActionType.ConfirmMaster);

        //        var errList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.ErrorSelection == 1)
        //            .ToList();
        //        if (errList.Count != 0)
        //        {
        //            foreach (var record in errList)
        //            {
        //                if (record.ErrorSelection == 1 && record.SyncSelection == 0)
        //                {
        //                    record.ErrorSelection = 0;
        //                    record.SyncSelection = 1;
        //                }
        //            }
        //        }
        //        Debug.WriteLine($"MasterConfirmation completed for pzvIDs={string.Join(",", filteredList)}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в MasterConfirmation: {ex.Message}");
        //    }
        //}
        //private async void buttonMasterConfirmation_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Debug.WriteLine($"Starting MasterConfirmation for pzvID={_xPzvID}, column={_xColumn}");
        //        await GridOverlayLoader.RunTaskWithOverlayAsync(
        //            gridControlPZVOperList,
        //            MasterConfirmation,              // обычный void-метод
        //            CancellationToken.None);
        //        _gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
        //        Debug.WriteLine($"Finished MasterConfirmation, now going to row with pzvID={_xPzvID}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в buttonMasterConfirmation_Click: {ex.Message}");
        //    }
        //}

        /// <summary>
        /// Отменить подтверждение мастером по V для выбранных операций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private async void MasterCancelConfirmation()
        //{
        //    try
        //    {
        //        Debug.WriteLine($"Starting MasterCancelConfirmation for pzvID={_xPzvID}, column={_xColumn}");
        //        _pZVOperListByPachListBindingSource.ResetBindings(false);
        //        var checkList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.SyncSelection == 1)
        //            .ToList();
        //        if (checkList.Count != 0)
        //        {
        //            foreach (var record in checkList)
        //            {
        //                if (record.olKodPodr == 1 && !CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                }
        //                if (!CheckPZVEmptyDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVEmptyDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVEmptyDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVEmptyDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }

        //            }
        //        }

        //        PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

        //        List<int> filteredList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.SyncSelection == 1)
        //            .Select(x => x.olPzvID)   // новый маппер
        //            .ToList();
        //        //SetDateMastToPzvID(filteredList, null);
        //        await ExecutePzvActionAsync(PzvActionType.CancelMasterConfirmation);

        //        var errList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.ErrorSelection == 1)
        //            .ToList();
        //        if (errList.Count != 0)
        //        {
        //            foreach (var record in errList)
        //            {
        //                if (record.ErrorSelection == 1 && record.SyncSelection == 0)
        //                {
        //                    record.ErrorSelection = 0;
        //                    record.SyncSelection = 1;
        //                }
        //            }
        //        }
        //        Debug.WriteLine($"MasterCancelConfirmation completed for pzvIDs={string.Join(",", filteredList)}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в MasterCancelConfirmation: {ex.Message}");
        //    }
        //}
        //private async void buttonMasterCancelConfirmation_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Debug.WriteLine($"Starting MasterCancelConfirmation for pzvID={_xPzvID}, column={_xColumn}");
        //        await GridOverlayLoader.RunTaskWithOverlayAsync(
        //            gridControlPZVOperList,
        //            MasterCancelConfirmation,              // обычный void-метод
        //            CancellationToken.None);
        //        _gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
        //        Debug.WriteLine($"Finished MasterCancelConfirmation, now going to row with pzvID={_xPzvID}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в buttonMasterCancelConfirmation_Click: {ex.Message}");
        //    }
        //}
        private void customSimpleButton5_Click(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine($"Starting DeletePzvByV for pzvID={_xPzvID}, column={_xColumn}");
                var recordsListToDelete = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .ToList();
                foreach (var record in recordsListToDelete)
                {
                    record.IsDeleted = true;
                    record.IsNew = false;
                    record.IsModified = false;
                }
                // Получаем список строк с флагом IsDeleted = true
                List<PZV> filteredList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x?.IsDeleted == true)
                    .Select(x => x.ToPZV())   // новый маппер
                    .ToList();
                // Преобразуем в BindingList
                if (filteredList.Count > 0)
                {
                    MutePlanBrokerNotifications();
                    using (SqlConnection connection = _dbHelper.GetConnection())
                    {
                        _bulkHelper.BulkAllDataUpdate(connection, filteredList, "planZagrVyaz", new[] { "pzvID" });
                        // удалить из BindingSource
                        foreach (var record in recordsListToDelete)
                        {
                            record.IsDeleted = false;
                            record.SyncSelection = 0;
                        }
                        LoadPlanZagrVyazByZadanySelection();
                    }
                }
                //_pZVOperListByPachListBindingSource.ResetBindings(false);
                //gridViewPZVOperList.RefreshData();
                Debug.WriteLine($"Finished DeletePzvByV, now going to row with pzvID={_xPzvID}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка customSimpleButton5_Click: {ex.Message}");
            }
        }
        /// <summary>
        /// Назначить на Таб№ по V для выбранных операций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private async void TabWorkAssignment()
        //{
        //    try
        //    {
        //        Debug.WriteLine($"Starting TabWorkAssignment for pzvID={_xPzvID}, column={_xColumn}");
        //        //SmenZadanyVyaz curr = _smenZadanyVyazBindingSource.Current as SmenZadanyVyaz;
        //        GridView _gridView = (GridView)gridControlSmenZadany.MainView;
        //        var currentSmenZadanyVyaz = _gridView.GetRow(_gridView.FocusedRowHandle) as SmenZadanyVyaz;
        //        if (currentSmenZadanyVyaz == null)
        //            return;

        //        int xKwsTabStart = vyazPodrKod == 1 ? currentSmenZadanyVyaz.kwsTabStart : currentSmenZadanyVyaz.szTab;

        //        _pZVOperListByPachListBindingSource.ResetBindings(false);
        //        var checkList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.SyncSelection == 1)
        //            .ToList();
        //        if (checkList.Count != 0)
        //        {
        //            if (currentSmenZadanyVyaz == null || xKwsTabStart == null || xKwsTabStart == 0)
        //            {
        //                MessageBox.Show("Не выбранн работниик для назначения");
        //                return;
        //            }
        //            foreach (var record in checkList)
        //            {
        //                if (record.olKodPodr == 1 && !CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (record.olKodPodr == 1 && !CompareTabAndMachineArea(record.olPzvKmlID, xKwsTabStart))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //            }
        //        }

        //        PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;
        //        List<int> filteredList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.SyncSelection == 1)
        //            .Select(x => x.olPzvID)   // новый маппер
        //            .ToList();
        //        ///await SetTabToPzvID(filteredList, xKwsTabStart, currentSmenZadanyVyaz.kwsID);
        //        await ExecutePzvActionAsync(PzvActionType.AssignTab);

        //        var errList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.ErrorSelection == 1)
        //            .ToList();
        //        if (errList.Count != 0)
        //        {
        //            foreach (var record in errList)
        //            {
        //                if (record.ErrorSelection == 1 && record.SyncSelection == 0)
        //                {
        //                    record.ErrorSelection = 0;
        //                    record.SyncSelection = 1;
        //                }
        //            }
        //        }
        //        Debug.WriteLine($"Finished TabWorkAssignment, now going to row with pzvID={_xPzvID}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в TabWorkAssignment: {ex.Message}");
        //    }
        //}
        //private bool CompareTabAndMachineArea(int _kmlID, int _tab)
        //{
        //    // проверка наличия сочетания табельного № и В/М к одной зоне
        //    try
        //    {
        //        Debug.WriteLine($"Starting CompareTabAndMachineArea for kmlID={_kmlID}, tab={_tab}");
        //        string query = $"SELECT * " +
        //                        $"  FROM knitWorkingShiftNewCurrentSmen_view wsncsv " +
        //                        $"      LEFT JOIN knitWorkingShiftMachineListNew wsmln ON wsncsv.kwsID = wsmln.kwsmlKwsID" +
        //                        $"   WHERE wsncsv.shiftStatusID in (0,2) " +
        //                        $"      AND wsncsv.tabShiftStart = {_tab} " +
        //                        $"      AND wsmln.kwsmlKmlID = {_kmlID} ";
        //        _isCountTabKM = _dbHelper.Exists(query, new Dictionary<string, object> { });
        //        Debug.WriteLine($"CompareTabAndMachineArea result for kmlID={_kmlID}, tab={_tab} is {_isCountTabKM}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка CompareTabAndMachineArea: {ex.Message}");
        //    }
        //    if (!_isCountTabKM)
        //    {
        //        MessageBox.Show($"Внимание! Табельный номер и В/М находятся в разных зонах!");
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
        //private async void buttonTabWorkAssignment_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Debug.WriteLine($"Starting TabWorkAssignment for pzvID={_xPzvID}, column={_xColumn}");
        //        await GridOverlayLoader.RunTaskWithOverlayAsync(
        //            gridControlPZVOperList,
        //            TabWorkAssignment,              // обычный void-метод
        //            CancellationToken.None);
        //        _gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
        //        Debug.WriteLine($"Finished TabWorkAssignment, now going to row with pzvID={_xPzvID}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в buttonTabWorkAssignment_Click: {ex.Message}");
        //    }
        //}

        /// <summary>
        /// Отменить назначение на Таб№ по V для выбранных операций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private async void TabCancelWorkAssignment()
        //{
        //    try
        //    {
        //        Debug.WriteLine($"Starting TabCancelWorkAssignment for pzvID={_xPzvID}, column={_xColumn}");
        //        _pZVOperListByPachListBindingSource.ResetBindings(false);
        //        var checkList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.SyncSelection == 1)
        //            .ToList();
        //        if (checkList.Count != 0)
        //        {
        //            foreach (var record in checkList)
        //            {
        //                if (record.olKodPodr == 1 && !CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя отменить назначение работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVEmptyDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя отменить назначение работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя отменить назначение работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя отменить назначение работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя отменить назначение работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //            }
        //        }

        //        PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

        //        List<int> filteredList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.SyncSelection == 1)
        //            .Select(x => x.olPzvID)   // новый маппер
        //            .ToList();
        //        //SetTabToPzvID(filteredList, 0, 0);
        //        await ExecutePzvActionAsync(PzvActionType.CancelTab);

        //        var errList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.ErrorSelection == 1)
        //            .ToList();
        //        if (errList.Count != 0)
        //        {
        //            foreach (var record in errList)
        //            {
        //                if (record.ErrorSelection == 1 && record.SyncSelection == 0)
        //                {
        //                    record.ErrorSelection = 0;
        //                    record.SyncSelection = 1;
        //                }
        //            }
        //        }
        //        Debug.WriteLine($"Finished TabCancelWorkAssignment, now going to row with pzvID={_xPzvID}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в TabCancelWorkAssignment: {ex.Message}");
        //    }
        //}
        //private async void buttonTabCancelWorkAssignment_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Debug.WriteLine($"Starting TabCancelWorkAssignment for pzvID={_xPzvID}, column={_xColumn}");
        //        await GridOverlayLoader.RunTaskWithOverlayAsync(
        //            gridControlPZVOperList,
        //            TabCancelWorkAssignment,              // обычный void-метод
        //            CancellationToken.None);
        //        //GoToPzvID(_xPzvID, _xColumn);
        //        _gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
        //        Debug.WriteLine($"Finished TabCancelWorkAssignment, now going to row with pzvID={_xPzvID}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в buttonTabCancelWorkAssignment_Click: {ex.Message}");
        //    }
        //}
        //private async void gridViewPZVOperList_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        //{
        //    Debug.WriteLine($"gridViewPZVOperList_FocusedRowChanged triggered with FocusedRowHandle={e.FocusedRowHandle}, tTopRowIndex={gridViewPZVOperList.TopRowIndex}, TopRowIndexPZV={TopRowIndexPZV}");
        //    // 0) Игнор при программном обновлении
        //    if (Volatile.Read(ref _isUiRefreshing) == 1)
        //        return;

        //    if (e.FocusedRowHandle < 0) return;
        //    if (_pZVOperListByPachListBindingSource == null) return;
        //    if (_pZVOperListByPachListBindingSource.Count == 0) return;
        //    if (_pZVOperListByPachListBindingSource.Position < 0) return;

        //    if (!(_pZVOperListByPachListBindingSource.Current is PZVOperList currPZV))
        //        return;

        //    // 1) Отменяем предыдущую загрузку деталей
        //    _focusLoadCts?.Cancel();
        //    _focusLoadCts?.Dispose();
        //    _focusLoadCts = new CancellationTokenSource();
        //    var ct = _focusLoadCts.Token;

        //    // 2) Номер “версии” (чтобы не применить устаревший результат)
        //    var seq = Interlocked.Increment(ref _focusSeq);

        //    TopRowIndexPZV = gridViewPZVOperList.TopRowIndex;
        //    Debug.WriteLine($"FocusedRowChanged: olPzvID={currPZV.olPzvID}, tTopRowIndex={gridViewPZVOperList.TopRowIndex}, TopRowIndexPZV={TopRowIndexPZV}");

        //    try
        //    {
        //        // (опционально) маленький debounce на мышь/клавиатуру, чтобы не дергать БД при быстром скролле
        //        await Task.Delay(120, ct);

        //        // 3) Грузим детали
        //        var annId = currPZV.olPzvAnnID;
        //        var nrId = currPZV.olPzvNrID;

        //        await Task.WhenAll(
        //            LoadArtNormNDataAsync(annId, ct),
        //            LoadNormRaszDataAsync(nrId, ct)
        //        );

        //        // 4) “последний победил”: если пока грузили фокус сменился — не трогаем UI
        //        if (ct.IsCancellationRequested) return;
        //        if (seq != Volatile.Read(ref _focusSeq)) return;

        //        // 5) Обновление гридов строго в UI-потоке
        //        gridViewArtNormN.RefreshData();
        //        gridViewNormRasz.RefreshData();
        //    }
        //    catch (OperationCanceledException)
        //    {
        //        // нормально: пользователь/refresh сменил фокус
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    Debug.WriteLine($"Finished gridViewPZVOperList_FocusedRowChanged for olPzvID={currPZV.olPzvID}, tTopRowIndex={gridViewPZVOperList.TopRowIndex}, TopRowIndexPZV={TopRowIndexPZV}");
        //}

        //private async void gridViewPZVOperList_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        //{
        //    try
        //    {
        //        // 0) Игнор при программном обновлении
        //        if (Volatile.Read(ref _isUiRefreshing) == 1)
        //            return;

        //        if (e.FocusedRowHandle < 0)
        //            return;
        //        // --- Проверяем BindingSource перед обращением к Current ---
        //        if (_pZVOperListByPachListBindingSource == null)
        //            return; // или MessageBox.Show("BindingSource не инициализирован");

        //        if (_pZVOperListByPachListBindingSource.Count == 0)
        //            return; // список пуст

        //        if (_pZVOperListByPachListBindingSource.Position < 0)
        //            return; // ничего не выбрано
        //        PZVOperList currPZV = _pZVOperListByPachListBindingSource.Current as PZVOperList;
        //        if (currPZV != null)
        //        {
        //            Task artNormNTask = LoadArtNormNDataAsync(currPZV.olPzvAnnID);
        //            Task normRaszTask = LoadNormRaszDataAsync(currPZV.olPzvNrID);
        //            await Task.WhenAll(artNormNTask, normRaszTask);
        //            gridViewArtNormN.RefreshData();
        //            gridViewNormRasz.RefreshData();
        //        }
        //    }
        //    catch (Exception ex)
        //    { //MessageBox.Show(ex.ToString());
        //    }
        //}

        //private async void gridViewPZVOperList_CellValueChanged(object sender, CellValueChangedEventArgs e)
        //{
        //    try
        //    {
        //        Debug.WriteLine($"gridViewPZVOperList_CellValueChanged triggered for Column={e.Column.FieldName}, RowHandle={e.RowHandle}");
        //        if (e.Column.FieldName != gridPZVOperListColumnOlKol.FieldName)
        //            return;

        //        var view = (GridView)sender;
        //        var currentItem = view.GetRow(e.RowHandle) as PZVOperList;
        //        if (currentItem == null) return;

        //        if (currentItem.olPzvDateNaznKm != null && currentItem.olPzvDateEnd == null)
        //        {
        //            MessageBox.Show("Нельзя изменять количество по операции, назначенной на В/М и не завершённой!");
        //            currentItem.olKol = currentItem.olKolCopy;
        //            return;
        //        }

        //        if (currentItem.olPzvDateNaznTab != null && currentItem.olPzvDateEnd == null)
        //        {
        //            MessageBox.Show("Нельзя изменять количество по операции, назначенной работнику и не завершённой!");
        //            currentItem.olKol = currentItem.olKolCopy;
        //            return;
        //        }

        //        if (Convert.ToInt32(e.Value) >= currentItem.olKolCopy)
        //        {
        //            MessageBox.Show("Новое количество не может быть больше или равно исходному!");
        //            currentItem.olKol = currentItem.olKolCopy;
        //            return;
        //        }

        //        if (Convert.ToInt32(e.Value) < 0)
        //        {
        //            MessageBox.Show("Новое количество не должно быть меньше нуля!");
        //            currentItem.olKol = currentItem.olKolCopy;
        //            return;
        //        }

        //        if (vyazPodrKod == 1)
        //        {
        //            int xPzvID = currentItem.olPzvID;
        //            string _xColumn = gridViewPZVOperList.FocusedColumn.ToString();
        //            if (currentItem.olPzvDateEnd != null && currentItem.olPzvDateMast == null)
        //            {
        //                string query = $"exec dbo.PZV_Split @pzvId = {currentItem.olPzvID}, @mode = 2, @qtyFact = {Convert.ToInt32(e.Value)} ";
        //                Task updateRZV = _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { });
        //                await Task.WhenAll(updateRZV);

        //                await GridOverlayLoader.RunTaskWithOverlayAsync(
        //                    gridControlPZVOperList,
        //                    LoadPlanZagrVyazByZadanySelection
        //                    , CancellationToken.None
        //                    );
        //            }
        //            else if (currentItem.olPzvDateNaznKm == null
        //                    && currentItem.olPzvDateNaznTab == null
        //                    && currentItem.olPzvDateStart == null
        //                    && currentItem.olPzvDateEnd == null
        //                    && currentItem.olPzvDateMast == null)
        //            {
        //                string query = $"exec dbo.PZV_Split @pzvId = {currentItem.olPzvID}, @mode = 3, @qtyFact = {Convert.ToInt32(e.Value)} ";
        //                Task updateRZV = _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { });
        //                await Task.WhenAll(updateRZV);

        //                await GridOverlayLoader.RunTaskWithOverlayAsync(
        //                    gridControlPZVOperList,
        //                    LoadPlanZagrVyazByZadanySelection
        //                    , CancellationToken.None
        //                    );
        //            }
        //            _gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, xPzvID, _xColumn);
        //            Debug.WriteLine($"Finished processing CellValueChanged for olPzvID={currentItem.olPzvID}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в gridViewPZVOperList_CellValueChanged: {ex.Message}");
        //    }
        //}
        //private void gridViewPZVOperList_ShownEditor(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Debug.WriteLine($"gridViewPZVOperList_ShownEditor triggered");
        //        GridView view = sender as GridView;
        //        var editor = view?.ActiveEditor as TextEdit;
        //        editor?.SelectAll();
        //        Debug.WriteLine($"Finished gridViewPZVOperList_ShownEditor");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в gridViewPZVOperList_ShownEditor: {ex.Message}");
        //    }
        //}

        //private async void repositoryItemCheckEdit5_EditValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Debug.WriteLine($"repositoryItemCheckEdit5_EditValueChanged triggered");
        //        gridViewRzvPachListByNom.PostEditor();        // применить новое значение из редактора
        //        gridViewRzvPachListByNom.UpdateCurrentRow();  // сохранить в источник данных
        //        var selectedRow = _rzvPachListByNomBindingSource.Current as RzvPachListByNom;

        //        // градация
        //        if (selectedRow != null)
        //        {
        //            // Преобразуем в JSON
        //            string jsonString = JsonConvert.SerializeObject(selectedRow, Formatting.Indented);
        //            // 1 - вяз подразделение. по другим подразделениям на градацию не отделяем
        //            string query = $"dbo.setGraduationRate @xNomListJson = '{jsonString}', @xGradationValue = {selectedRow.gradacia}, @xPodrKod = 1 ";
        //            Task updateRZV = _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { });
        //            await Task.WhenAll(updateRZV);

        //            await GridOverlayLoader.RunTaskWithOverlayAsync(
        //                gridControlPZVOperList,
        //                LoadPlanZagrVyazByZadanySelection
        //                , CancellationToken.None
        //                );
        //        }
        //        Debug.WriteLine($"Finished repositoryItemCheckEdit5_EditValueChanged");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка repositoryItemCheckEdit5_EditValueChanged: {ex.Message}");

        //    }

        //    // деление операций на градацию - жду Катю Бабинцеву
        //    // отпарку и раскрой на градацию не отделяем

        //}
        /// <summary>
        /// Начать выполнение по V для выбранных операций
        /// </summary>
        //private async void WorkStartExecution()
        //{
        //    try
        //    {
        //        Debug.WriteLine($"Starting WorkStartExecution for pzvID={_xPzvID}, column={_xColumn}");
        //        var checkList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.SyncSelection == 1)
        //            .ToList();
        //        if (checkList.Count != 0)
        //        {
        //            foreach (var record in checkList)
        //            {
        //                if (record.olKodPodr == 1 && record.olPzvKwsID == 0)
        //                {
        //                    MessageBox.Show($"Операция не назначена на смену, нельзя начать выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})");
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (record.olKodPodr == 1 && !(CheckPZVShiftOpened(record.olPzvKwsID, $"нельзя начать выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})")
        //                    && !CheckPZVShiftClosed(record.olPzvKwsID, $"нельзя начать выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})")))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (record.olKodPodr == 1 && !CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVEmptyDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }

        //            }
        //        }

        //        PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

        //        List<int> filteredList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.SyncSelection == 1)
        //            .Select(x => x.olPzvID)   // новый маппер
        //            .ToList();
        //        //SetDateStartToPzvID(filteredList, DateTime.Now);
        //        await ExecutePzvActionAsync(PzvActionType.StartWork);

        //        var errList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.ErrorSelection == 1)
        //            .ToList();
        //        if (errList.Count != 0)
        //        {
        //            foreach (var record in errList)
        //            {
        //                if (record.ErrorSelection == 1 && record.SyncSelection == 0)
        //                {
        //                    record.ErrorSelection = 0;
        //                    record.SyncSelection = 1;
        //                }
        //            }
        //        }
        //        Debug.WriteLine($"Finished WorkStartExecution, now going to row with pzvID={_xPzvID}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в WorkStartExecution: {ex.Message}");
        //    }
        //}

        //private async void buttonWorkStartExecution_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Debug.WriteLine($"Starting buttonWorkStartExecution_Click for pzvID={_xPzvID}, column={_xColumn}");
        //        await GridOverlayLoader.RunTaskWithOverlayAsync(
        //            gridControlPZVOperList,
        //            WorkStartExecution,              // обычный void-метод
        //            CancellationToken.None);
        //        _gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
        //        Debug.WriteLine($"Finished buttonWorkStartExecution_Click, now going to row with pzvID={_xPzvID}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в buttonWorkStartExecution_Click: {ex.Message}");
        //    }
        //}
        /// <summary>
        /// Отменить начало выполнения по V для выбранных операций
        /// </summary>
        //private async void CancelWorkStartExecution()
        //{
        //    try
        //    {
        //        Debug.WriteLine($"Starting CancelWorkStartExecution for pzvID={_xPzvID}, column={_xColumn}");
        //        var checkList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.SyncSelection == 1)
        //            .ToList();
        //        if (checkList.Count != 0)
        //        {
        //            foreach (var record in checkList)
        //            {
        //                if (record.olKodPodr == 1 && record.olPzvKwsID == 0)
        //                {
        //                    MessageBox.Show($"Операция не назначена на смену, нельзя отменить начало выполнения (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})");
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (record.olKodPodr == 1 && !(CheckPZVShiftOpened(record.olPzvKwsID, $"нельзя отменить начало выполнения (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})")
        //                    && !CheckPZVShiftClosed(record.olPzvKwsID, $"нельзя отменить начало выполнения (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})")))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (record.olKodPodr == 1 && !CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя начать выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVEmptyDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя начать выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVEmptyDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя начать выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя начать выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя начать выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //            }
        //        }

        //        PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

        //        List<int> filteredList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.SyncSelection == 1)
        //            .Select(x => x.olPzvID)   // новый маппер
        //            .ToList();
        //        DateTime? date = null;
        //        //SetDateStartToPzvID(filteredList, date);
        //        await ExecutePzvActionAsync(PzvActionType.StartWork);

        //        var errList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.ErrorSelection == 1)
        //            .ToList();
        //        if (errList.Count != 0)
        //        {
        //            foreach (var record in errList)
        //            {
        //                if (record.ErrorSelection == 1 && record.SyncSelection == 0)
        //                {
        //                    record.ErrorSelection = 0;
        //                    record.SyncSelection = 1;
        //                }
        //            }
        //        }
        //        Debug.WriteLine($"Finished CancelWorkStartExecution, now going to row with pzvID={_xPzvID}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в CancelWorkStartExecution: {ex.Message}");
        //    }
        //}

        /// <summary>
        /// Окончить выполнение по V для выбранных операций
        /// </summary>
        //private async void WorkStopExecution()
        //{
        //    try
        //    {
        //        Debug.WriteLine($"Starting WorkStopExecution for pzvID={_xPzvID}, column={_xColumn}");
        //        _pZVOperListByPachListBindingSource.ResetBindings(false);
        //        var checkList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.SyncSelection == 1)
        //            .ToList();
        //        if (checkList.Count != 0)
        //        {
        //            foreach (var record in checkList)
        //            {
        //                if (record.olKodPodr == 1 && record.olPzvKwsID == 0)
        //                {
        //                    MessageBox.Show($"Операция не назначена на смену, нельзя завершить выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})");
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (record.olKodPodr == 1 && !(CheckPZVShiftOpened(record.olPzvKwsID, $"нельзя завершить выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})")
        //                    && !CheckPZVShiftClosed(record.olPzvKwsID, $"нельзя завершить выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})")))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (record.olKodPodr == 1 && !CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVEmptyDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVEmptyDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }

        //            }
        //        }

        //        PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

        //        List<int> filteredList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.SyncSelection == 1)
        //            .Select(x => x.olPzvID)   // новый маппер
        //            .ToList();
        //        //SetDateEndToPzvID(filteredList, DateTime.Now);
        //        await ExecutePzvActionAsync(PzvActionType.StopWork);

        //        var errList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.ErrorSelection == 1)
        //            .ToList();
        //        if (errList.Count != 0)
        //        {
        //            foreach (var record in errList)
        //            {
        //                if (record.ErrorSelection == 1 && record.SyncSelection == 0)
        //                {
        //                    record.ErrorSelection = 0;
        //                    record.SyncSelection = 1;
        //                }
        //            }
        //        }
        //        Debug.WriteLine($"Finished WorkStopExecution, now going to row with pzvID={_xPzvID}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в WorkStopExecution: {ex.Message}");
        //    }
        //}
        /// <summary>
        ///  Отменить окончание выполнения по V для выбранных операций
        /// </summary>
        //private async void CancelWorkStopExecution()
        //{
        //    try
        //    {
        //        Debug.WriteLine($"Starting CancelWorkStopExecution for pzvID={_xPzvID}, column={_xColumn}");
        //        _pZVOperListByPachListBindingSource.ResetBindings(false);
        //        var checkList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.SyncSelection == 1)
        //            .ToList();
        //        if (checkList.Count != 0)
        //        {
        //            foreach (var record in checkList)
        //            {
        //                if (record.olKodPodr == 1 && record.olPzvKwsID == 0)
        //                {
        //                    MessageBox.Show($"Операция не назначена на смену, нельзя отменить окончание выполнения (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})");
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (record.olKodPodr == 1 && !(CheckPZVShiftOpened(record.olPzvKwsID, $"нельзя отменить окончание выполнения (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})")
        //                    && !CheckPZVShiftClosed(record.olPzvKwsID, $"нельзя отменить окончание выполнения (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})")))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (record.olKodPodr == 1 && !CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVEmptyDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVEmptyDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVEmptyDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //                if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
        //                {
        //                    record.ErrorSelection = 1;
        //                    record.SyncSelection = 0;
        //                    continue;
        //                }
        //            }
        //        }

        //        PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

        //        List<int> filteredList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.SyncSelection == 1)
        //            .Select(x => x.olPzvID)   // новый маппер
        //            .ToList();
        //        DateTime? date = null;
        //        //SetDateEndToPzvID(filteredList, date);
        //        await ExecutePzvActionAsync(PzvActionType.StopWork);

        //        var errList = _pZVOperListByPachListBindingSource.List
        //            .OfType<PZVOperList>()
        //            .Where(x => x.ErrorSelection == 1)
        //            .ToList();
        //        if (errList.Count != 0)
        //        {
        //            foreach (var record in errList)
        //            {
        //                if (record.ErrorSelection == 1 && record.SyncSelection == 0)
        //                {
        //                    record.ErrorSelection = 0;
        //                    record.SyncSelection = 1;
        //                }
        //            }
        //        }
        //        Debug.WriteLine($"Finished CancelWorkStopExecution, now going to row with pzvID={_xPzvID}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в CancelWorkStopExecution: {ex.Message}");
        //    }
        //}

        private bool CheckPZVShiftClosed(int _olPzvKwsID, string _xMessage)
        {
            try
            {
                Debug.WriteLine($"Checking if shift is closed for kwsID={_olPzvKwsID}");
                string query = "SELECT COUNT(*) AS kwsClosedCount FROM knitWorkingShiftNew_view wsnv WHERE wsnv.kwsID = @kwsID AND kwsDateEnd IS NOT NULL";
                int result = _dbHelper.ExecuteScalar(query, new Dictionary<string, object> { { "@kwsID", _olPzvKwsID } });

                string _detailMessage = string.Empty;
                if (result == 1)
                {
                    _detailMessage = "Смена завершена";
                    MessageBox.Show($"{_detailMessage}, {_xMessage}!");
                    Debug.WriteLine($"Return true - Shift with kwsID={_olPzvKwsID} is closed");
                    return true;
                }
                else if (result > 1)
                {
                    _detailMessage = "Не удалось получить информацию по смене";
                    MessageBox.Show($"{_detailMessage}, {_xMessage}!");
                    Debug.WriteLine($"Return false - Multiple records found for kwsID={_olPzvKwsID} when checking if shift is closed");
                    return false;
                }
                else
                {
                    Debug.WriteLine($"Return false - Shift with kwsID={_olPzvKwsID} is not closed");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в CheckPZVShiftClosed: {ex.Message}");
                return false;
            }
        }

        private bool CheckPZVShiftOpened(int _olPzvKwsID, string _xMessage)
        {
            try
            {
                Debug.WriteLine($"Checking if shift is opened for kwsID={_olPzvKwsID}");
                string query = "SELECT COUNT(*) AS kwsOpenedCount FROM knitWorkingShiftNew_view wsnv WHERE wsnv.kwsID = @kwsID AND kwsDateStart IS NOT NULL";
                int result = _dbHelper.ExecuteScalar(query, new Dictionary<string, object> { { "@kwsID", _olPzvKwsID } });

                string _detailMessage = string.Empty;
                if (result == 0)
                {
                    _detailMessage = "Смена не начата";
                    MessageBox.Show($"{_detailMessage}, {_xMessage}!");
                    Debug.WriteLine($"Return false - Shift with kwsID={_olPzvKwsID} is not opened");
                    return false;
                }
                else if (result > 1)
                {
                    _detailMessage = "Не удалось получить информацию по смене";
                    MessageBox.Show($"{_detailMessage}, {_xMessage}!");
                    Debug.WriteLine($"Return false - Multiple records found for kwsID={_olPzvKwsID} when checking if shift is opened");
                    return false;
                }
                else
                {
                    Debug.WriteLine($"Return true - Shift with kwsID={_olPzvKwsID} is opened");
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в CheckPZVShiftOpened: {ex.Message}");
                return false;
            }
        }

        //private async void buttonCancelWorkStartExecution_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Debug.WriteLine($"Starting buttonCancelWorkStartExecution_Click for pzvID={_xPzvID}, column={_xColumn}");
        //        await GridOverlayLoader.RunTaskWithOverlayAsync(
        //            gridControlPZVOperList,
        //            CancelWorkStartExecution,              // обычный void-метод
        //            CancellationToken.None);
        //        _gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
        //        Debug.WriteLine($"Finished buttonCancelWorkStartExecution_Click, now going to row with pzvID={_xPzvID}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в buttonCancelWorkStartExecution_Click: {ex.Message}");
        //    }
        //}
        //private async void buttonWorkStopExecution_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Debug.WriteLine($"Starting buttonWorkStopExecution_Click for pzvID={_xPzvID}, column={_xColumn}");
        //        await GridOverlayLoader.RunTaskWithOverlayAsync(
        //            gridControlPZVOperList,
        //            WorkStopExecution,              // обычный void-метод
        //            CancellationToken.None);
        //        //GoToPzvID(_xPzvID, _xColumn);
        //        _gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
        //        Debug.WriteLine($"Finished buttonWorkStopExecution_Click, now going to row with pzvID={_xPzvID}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в buttonWorkStopExecution_Click: {ex.Message}");
        //    }
        //}

        //private async void buttonCancelWorkStopExecution_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Debug.WriteLine($"Starting buttonCancelWorkStopExecution_Click for pzvID={_xPzvID}, column={_xColumn}");
        //        await GridOverlayLoader.RunTaskWithOverlayAsync(
        //            gridControlPZVOperList,
        //            CancelWorkStopExecution,              // обычный void-метод
        //            CancellationToken.None);
        //        _gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
        //        Debug.WriteLine($"Finished buttonCancelWorkStopExecution_Click, now going to row with pzvID={_xPzvID}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в buttonCancelWorkStopExecution_Click: {ex.Message}");
        //    }
        //}

        private void gridViewRzvPachListByNom_ShowingEditor(object sender, CancelEventArgs e)
        {
            // если колонка gridColumnRzvPachListByNomGradacia редактировать запрещена:
            try
            {
                Debug.WriteLine($"gridViewRzvPachListByNom_ShowingEditor triggered for column {gridViewRzvPachListByNom.FocusedColumn?.FieldName}");
                if (gridViewRzvPachListByNom.FocusedColumn == gridRzvPachListByNomColumnGradacia &&
                    gridRzvPachListByNomColumnGradacia.OptionsColumn.ReadOnly)
                {
                    e.Cancel = true; // запретить редактирование
                    MessageBox.Show("Внимание! Технологом не проставлен признак градации - нельзя проставить градацию на пачку!");
                }
                Debug.WriteLine($"Finished gridViewRzvPachListByNom_ShowingEditor for column {gridViewRzvPachListByNom.FocusedColumn?.FieldName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в gridViewRzvPachListByNom_ShowingEditor: {ex.Message}");
            }
        }
        private async void layoutControlGroup2_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            try
            {
                int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);
                switch (buttonIndex)
                {
                    case 4:
                        //MessageBox.Show("Обновление сменного задания");
                        //await LoadSmenZadanyVyazNewDataAsync(vyazPodrKod);
                        await LoadSmenZadanyVyazNewDataAsync(1);
                        break;
                    case 6:
                        advBandedGridViewSmenZadany.CollapseAllGroups();
                        break;
                    case 8:
                        advBandedGridViewSmenZadany.ExpandAllGroups();
                        break;
                    case 10:
                        advBandedGridViewSmenZadany.CollapseAllGroups();
                        advBandedGridViewSmenZadany.ExpandGroupLevel(0);
                        break;
                    case 12:
                        await this.UI(() =>
                        {
                            gridControlSmenZadany.BeginUpdate();
                            Application.Idle -= ExpandGroupsOnIdle;
                            Application.Idle += ExpandGroupsOnIdle;
                            gridControlSmenZadany.EndUpdate();
                        });
                        break;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в customTabControl1_CustomHeaderButtonClick: {ex.Message}");
            }
        }
        private void advBandedGridViewSmenZadany_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                Debug.WriteLine($"advBandedGridViewSmenZadany_PopupMenuShowing triggered for menu type {e.MenuType}");
                if (e.MenuType != GridMenuType.Row)
                    return;

                var view = (AdvBandedGridView)sender;

                int rowHandle = e.HitInfo.RowHandle;
                if (rowHandle < 0)
                    return;

                view.FocusedRowHandle = rowHandle;

                var currentSmenZadanyVyaz = view.GetRow(rowHandle) as SmenZadanyVyaz;
                if (currentSmenZadanyVyaz == null)
                    return;

                // Загружаем зоны СИНХРОННО (внутри async уже не выполняется)
                // список зон, по которым открыты смены и в которые можно перенести В/М, загружается при перемещении по строкам грида

                // Очищаем стандартное меню
                e.Menu.Items.Clear();

                if (e.Menu == null)
                    e.Menu = new GridViewMenu(view);

                // Создаём подменю
                DXSubMenuItem moveToZone = new DXSubMenuItem("Переместить в зону");

                foreach (var zone in _zonesCache)
                {
                    var item = new DXMenuItem(zone.kmaNumber, (o, args) =>
                    {
                        MoveRowToZone(view, rowHandle, zone.kmaID, zone.kwsID);
                    });

                    moveToZone.Items.Add(item);
                }

                // Добавляем подменю в меню DevExpress
                e.Menu.Items.Add(moveToZone);
                Debug.WriteLine($"Finished advBandedGridViewSmenZadany_PopupMenuShowing for menu type {e.MenuType}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка advBandedGridViewSmenZadany_PopupMenuShowing: {ex.Message}");
            }
        }
        private async void MoveRowToZone(AdvBandedGridView _view, int _rowHandle, int _kmaID, int _kwsID)
        {
            //MessageBox.Show($"Перенос в зону kmaID = ({_kmaID}) kwsID = {_kwsID}");
            try
            {
                Debug.WriteLine($"MoveRowToZone triggered for rowHandle={_rowHandle}, kmaID={_kmaID}, kwsID={_kwsID}");
                var row = _view.GetRow(_rowHandle);
                if (row == null)
                    return;

                // Например, получить ID записи
                int xKmlID = (row as SmenZadanyVyaz).kwsmlKmlID;
                int xKodOb = (row as SmenZadanyVyaz).kodOb;
                int xLongRep = (row as SmenZadanyVyaz).longRep;

                //MessageBox.Show($"Перенос kmlID {xKmlID} kodOb | {xKodOb} | в зону kmaID {_kmaID} | kwsID {_kwsID}");

                string query = $"INSERT INTO knitWorkingShiftMachineListNew (kwsmlDateAdd, kwsmlCompAdd, kwsmlKwsID, kwsmlKmlID, kwsmlKodOb, kiwsmlLongRep) " +
                    $"VALUES (DEFAULT, DEFAULT, {_kwsID}, {xKmlID}, {xKodOb}, {xLongRep})";
                _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { });


                //await LoadSmenZadanyVyazNewDataAsync(vyazPodrKod); ;
                await LoadSmenZadanyVyazNewDataAsync(1);

                var changes = GetChanges<SmenZadanyVyaz>(
                        _smenZadanyVyazBindingSource,
                        _smenZadanyVyazNewBindingSource,
                        HashMode.ExcludeOnly,
                        keyProperties: new[] { "kwsmlKmlID", "typeID" },
                        hashProperties: new[] { "IsNew", "IsModified", "IsDeleted" }
                    );
                // обновить и добавить
                //BindingSourceHelper.ApplyChanges(_smenZadanyVyazBindingSource, changes, x => (x.kwsmlKmlID, x.typeID));
                ApplyChanges(
                        _smenZadanyVyazBindingSource,
                        changes,
                        UpdateFieldsMode.ExcludeOnly,
                        keyProperties: new[] { "kwsmlKmlID", "typeID" },
                        gridViewSmenZadany,
                        fields: new[] { "IsNew", "IsModified", "IsDeleted" }
                    );
                // удалить отсутствующие
                var metrics = RemoveMissingSmart(
                    _smenZadanyVyazBindingSource,
                    changes.Removed,
                    gridControlSmenZadany
                );
                if (changes != null)
                {
                    changes.Added?.Clear();
                    changes.Modified?.Clear();
                    changes.Removed?.Clear();
                    changes = null;
                }
                //  Application.Idle += ExpandGroupsOnIdle;


                //gridViewSmenZadany.RefreshData();

                gridViewSmenZadany.GridControl.BeginInvoke(new Action(() =>
                  {
                      if (_rowHandle == 0 && gridViewSmenZadany.DataRowCount == 1)
                      {
                          gridViewSmenZadany.FocusedRowHandle = _rowHandle;
                      }
                      else if (_rowHandle > 0 && gridViewSmenZadany.DataRowCount > 0)
                      {
                          gridViewSmenZadany.FocusedRowHandle = _rowHandle - 1;
                      }
                      else if (_rowHandle == 0 && gridViewSmenZadany.DataRowCount > 0)
                      {
                          gridViewSmenZadany.FocusedRowHandle = _rowHandle + 1;
                      }
                  }));
                Debug.WriteLine($"Finished MoveRowToZone for rowHandle={_rowHandle}, kmaID={_kmaID}, kwsID={_kwsID}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка MoveRowToZone: {ex.Message}");
            }

        }
        private async void advBandedGridViewSmenZadany_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                Debug.WriteLine($"advBandedGridViewSmenZadany_FocusedRowChanged triggered for new focused row handle {e.FocusedRowHandle}");
                var _view = sender as AdvBandedGridView;
                TopRowIndexSmenZadany = _view.FocusedRowHandle;
                SmenZadanyFocusedRowChanged(_view, e.FocusedRowHandle);
                Debug.WriteLine($"Finished advBandedGridViewSmenZadany_FocusedRowChanged for new focused row handle {e.FocusedRowHandle}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка advBandedGridViewSmenZadany_FocusedRowChanged: {ex.Message}");
            }
        }
        private async void SmenZadanyFocusedRowChanged(GridView _gridView, int _focusedRowHandle)
        {
            try
            {
                Debug.WriteLine($"SmenZadanyFocusedRowChanged triggered for focused row handle {_focusedRowHandle}");
                //var currentSmenZadanyVyaz = _smenZadanyVyazBindingSource.Current as SmenZadanyVyaz;
                var currentSmenZadanyVyaz = _gridView.GetRow(_gridView.FocusedRowHandle) as SmenZadanyVyaz;

                if (currentSmenZadanyVyaz == null)
                    return;

                if (vyazPodrKod == 1)
                {
                    await LoadKnitWorkingShiftSmenToMoveDataAsync(currentSmenZadanyVyaz.kwsKmaID);
                }

                int _xTab = vyazPodrKod == 1 ? currentSmenZadanyVyaz.kwsTabStart : currentSmenZadanyVyaz.szTab;
                int _xKmlID = currentSmenZadanyVyaz.kwsmlKmlID;
                int _groupLevel = gridViewNaryadZadany.GetRowLevel(_focusedRowHandle);

                if (currentSmenZadanyVyaz.typeID == 1 && !gridViewNaryadZadany.IsGroupRow(_focusedRowHandle))
                {
                    // наряд-задание по В/М
                    _xTab = 0;
                }
                else if (currentSmenZadanyVyaz.typeID == 2 || gridViewNaryadZadany.IsGroupRow(_focusedRowHandle) && new[] { 0, 1 }.Contains(_groupLevel))
                {
                    // наряд-задание по таб№
                    _xKmlID = 0;
                }
                await LoadNaryadZadanyVyazDataAsync(_xTab, _xKmlID);
                gridViewNaryadZadany.RefreshData();
                Debug.WriteLine($"Finished SmenZadanyFocusedRowChanged for focused row handle {_focusedRowHandle}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка smenZadanyFocusedRowChanged: {ex.Message}");
            }
        }
        private void customSimpleButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{advBandedGridViewSmenZadany.FocusedRowHandle}");
            MessageBox.Show($"{advBandedGridViewSmenZadany.IsGroupRow(advBandedGridViewSmenZadany.FocusedRowHandle)}");
        }
        private void customSimpleButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{advBandedGridViewSmenZadany.RowCount}");
            MessageBox.Show($"{advBandedGridViewSmenZadany.GetRowLevel(advBandedGridViewSmenZadany.FocusedRowHandle)}");
            MessageBox.Show($"{advBandedGridViewSmenZadany.GetGroupRowValue(advBandedGridViewSmenZadany.FocusedRowHandle)}");
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try
            {
                Application.Idle -= ExpandGroupsOnIdle;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Form] OnFormClosed error: {ex}");
            }
            finally
            {
                // Fallback: если FormClosing прервался исключением, гарантируем shutdown здесь.
                EnsureServiceBrokerShutdownOnClosed();
                base.OnFormClosed(e);
            }
        }
        protected async void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_closing) return;
                _closing = true;

                gridViewPlanTotalHoursByKnitMachine.FocusedRowChanged -= gridViewPlanTotalHoursByKnitMachine_FocusedRowChanged;
                gridViewPlanTotalQuantityByArticul.FocusedRowChanged -= gridViewPlanTotalQuantityByArticul_FocusedRowChanged;
                gridViewZadanyList.FocusedRowChanged -= gridViewZadanyList_FocusedRowChanged;
                //gridViewRzvPachListByNom.FocusedRowChanged -= gridViewRzvPachListByNom_FocusedRowChanged;
                gridViewPZVOperList.FocusedRowChanged -= gridViewPZVOperList_FocusedRowChanged;
                advBandedGridViewSmenZadany.FocusedRowChanged -= advBandedGridViewSmenZadany_FocusedRowChanged;
                gridViewSmenZadanyOtp.FocusedRowChanged -= gridViewSmenZadanyOtp_FocusedRowChanged;
                //gridViewNaryadZadany.PopupMenuShowing -= gridViewNaryadZadany_PopupMenuShowing;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FormClosingError");
                Debug.WriteLine(ex);
            }
            finally
            {
                await ShutdownServiceBrokerAsync();
            }
        }
        private async Task ShutdownServiceBrokerAsync()
        {
            Debug.WriteLine($"[PlanZagrVyaz] ShutdownServiceBrokerAsync started. ServiceBrokerShutdownStarted={_serviceBrokerShutdownStarted}");
            if (Interlocked.Exchange(ref _serviceBrokerShutdownStarted, 1) != 0)
                return;

            try
            {
                try { _loadCts?.Cancel(); } catch { }
                try { _lifetimeCts?.Cancel(); } catch { }
                try { await _sbHub.UnsubscribeAsync(_sbHubOwnerId); } catch { }

                // Важно: дожидаемся корректного снятия SqlDependency/ServiceBroker диалогов.
                await _sbController.DisposeAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[PlanZagrVyaz] ShutdownServiceBrokerAsync error: {ex}");
            }
            finally
            {
                try { await _sbService.DisposeAsync(); } catch { }
                try { _lifetimeCts?.Dispose(); } catch { }
                _lifetimeCts = null;

                try { _loadCts?.Dispose(); } catch { }
                _loadCts = null;
            }
            Debug.WriteLine($"[PlanZagrVyaz] ShutdownServiceBrokerAsync completed.");
        }
        private void EnsureServiceBrokerShutdownOnClosed()
        {
            Debug.WriteLine($"[PlanZagrVyaz] EnsureServiceBrokerShutdownOnClosed called. ServiceBrokerShutdownStarted={_serviceBrokerShutdownStarted}");
            if (Interlocked.CompareExchange(ref _serviceBrokerShutdownStarted, 1, 1) == 1)
                return;

            try
            {
                Task.Run(() => ShutdownServiceBrokerAsync()).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[PlanZagrVyaz] EnsureServiceBrokerShutdownOnClosed error: {ex}");
            }
            Debug.WriteLine($"[PlanZagrVyaz] EnsureServiceBrokerShutdownOnClosed completed.");
        }
        private void layoutControlGroup7_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            try
            {
                int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);
                switch (buttonIndex)
                {
                    case 0:
                        gridViewPlanTotalHoursByKnitMachine.CollapseAllGroups();
                        break;
                    case 2:
                        gridViewPlanTotalHoursByKnitMachine.ExpandAllGroups();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка layoutControlGroup7_CustomButtonClick: {ex.Message}");
            }
        }
        private void layoutControlGroup9_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            try
            {
                int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);
                switch (buttonIndex)
                {
                    case 0:
                        gridViewZadanyList.CollapseAllGroups();
                        break;
                    case 2:
                        gridViewZadanyList.ExpandAllGroups();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка layoutControlGroup9_CustomButtonClick: {ex.Message}");
            }
        }
        private void layoutControlGroup10_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            try
            {
                int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);
                switch (buttonIndex)
                {
                    case 0:
                        gridViewRzvPachListByNom.CollapseAllGroups();
                        break;
                    case 2:
                        gridViewRzvPachListByNom.ExpandAllGroups();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка layoutControlGroup10_CustomButtonClick: {ex.Message}");
            }
        }
        private void layoutControlGroup15_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            try
            {
                int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);
                switch (buttonIndex)
                {
                    case 0:
                        gridViewPZVOperList.CollapseAllGroups();
                        gridViewPZVOperList.ExpandGroupLevel(0);
                        break;
                    case 2:
                        gridViewPZVOperList.CollapseAllGroups();
                        break;
                    case 4:
                        gridViewPZVOperList.ExpandAllGroups();
                        break;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка layoutControlGroup15_CustomButtonClick: {ex.Message}");
            }
        }
        private void layoutControlGroup3_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            try
            {
                int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);
                switch (buttonIndex)
                {
                    case 0:
                        gridViewNaryadZadany.CollapseAllGroups();
                        break;
                    case 2:
                        gridViewNaryadZadany.ExpandAllGroups();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка layoutControlGroup3_CustomButtonClick: {ex.Message}");
            }
        }
        private void textBoxPzvNomZadSearch_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine($"textBoxPzvNomZadSearch_KeyDown triggered for key {e.KeyCode}");
            if (e.KeyCode == Keys.Enter)
            {
                FindPzvNomZadInNzp();
            }
            Debug.WriteLine($"Finished textBoxPzvNomZadSearch_KeyDown for key {e.KeyCode}");
        }
        private async void FindPzvNomZadInNzp()
        //{
        //    Debug.WriteLine($"FindPzvNomZadInNzp started with search text: {textBoxPzvNomZadSearch.Text}");
        //    _autoSelectPending = true;
        //    string pszNom = NormalizeNom(textBoxPzvNomZadSearch.Text);
        //    if (pszNom.Length == 0) return;

        //    switch (vyazPodrKod)
        //    {
        //        case 1:
        //            FindPzvNomZadInMachineListNzp(pszNom);
        //            break;
        //        case 2:
        //        case 3:
        //            FindPzvNomZadInArticulListNzp(pszNom);
        //            break;
        //    }

        //    _autoSelectPending = false;
        //    Debug.WriteLine($"FindPzvNomZadInNzp completed for search text: {textBoxPzvNomZadSearch.Text}");
        //}
        {
            Debug.WriteLine($"FindPzvNomZadInNzp started with search text: {textBoxPzvNomZadSearch.Text}");
            _autoSelectPending = true;
            string pszNom = NormalizeNom(textBoxPzvNomZadSearch.Text);
            if (pszNom.Length == 0) return;
            switch (vyazPodrKod)
            {
                case 1:
                    FindPzvNomZadInMachineListNzp(pszNom);
                    //  // Параметр вместо интерполяции
                    //  int kmlId = _dbHelper.ExecuteScalar(
                    //      @"SELECT TOP (1) pszm.pszkmKmlID
                    //FROM plan_sezon_zad_knitMachine pszm
                    //WHERE pszm.pszkmPszNom = @pszNom",
                    //      new Dictionary<string, object>
                    //      {
                    //          ["@pszNom"] = pszNom
                    //      });

                    //  if (kmlId <= 0) return;

                    //  _pendingPszNom = pszNom;

                    //  //------------------------------------
                    //  var colKnitMachine = gridViewPlanTotalHoursByKnitMachine.Columns.FirstOrDefault(c => c.FieldName == "kmlID");
                    //  if (colKnitMachine == null) return;

                    //  int rhKnitMachine = gridViewPlanTotalHoursByKnitMachine.LocateByValue(0, colKnitMachine, kmlId);
                    //  if (!gridViewPlanTotalHoursByKnitMachine.IsValidRowHandle(rhKnitMachine) || !gridViewPlanTotalHoursByKnitMachine.IsDataRow(rhKnitMachine)) return;

                    //  gridViewPlanTotalHoursByKnitMachine.BeginUpdate();
                    //  try
                    //  {
                    //      gridViewPlanTotalHoursByKnitMachine.FocusedRowHandle = rhKnitMachine;
                    //      gridViewPlanTotalHoursByKnitMachine.MakeRowVisible(rhKnitMachine);
                    //      gridViewPlanTotalHoursByKnitMachine.ClearSelection();
                    //      gridViewPlanTotalHoursByKnitMachine.SelectRow(rhKnitMachine);
                    //  }
                    //  finally
                    //  {
                    //      gridViewPlanTotalHoursByKnitMachine.EndUpdate();
                    //  }
                    //-----------------------------------
                    break;
                case 2:
                case 3:
                    FindPzvNomZadInArticulListNzp(pszNom);
                    //  // Параметр вместо интерполяции
                    //  //int count = ExecuteScalar<int>("SELECT COUNT(*) FROM Users");
                    //  string _xKod = _dbHelper.ExecuteScalar<string>(
                    //      @"SELECT TOP (1) left(rzv.kod, 7) as kod
                    //FROM raskr_zeh_vyaz rzv
                    //WHERE zad_pl = @pszNom",
                    //      new Dictionary<string, object>
                    //      {
                    //          ["@pszNom"] = pszNom
                    //      });

                    //  if (Convert.ToInt32(_xKod) <= 0) return;

                    //  _pendingPszNom = pszNom;

                    //  //------------------------------------
                    //  var colArticulKod = gridViewPlanTotalQuantityByArticul.Columns.FirstOrDefault(c => c.FieldName == "Kod");
                    //  if (colArticulKod == null) return;

                    //  int rhArticulKod = gridViewPlanTotalQuantityByArticul.LocateByValue(0, colArticulKod, _xKod);
                    //  if (!gridViewPlanTotalQuantityByArticul.IsValidRowHandle(rhArticulKod) || !gridViewPlanTotalQuantityByArticul.IsDataRow(rhArticulKod)) return;

                    //  gridViewPlanTotalQuantityByArticul.BeginUpdate();
                    //  try
                    //  {
                    //      gridViewPlanTotalQuantityByArticul.FocusedRowHandle = rhArticulKod;
                    //      gridViewPlanTotalQuantityByArticul.MakeRowVisible(rhArticulKod);
                    //      gridViewPlanTotalQuantityByArticul.ClearSelection();
                    //      gridViewPlanTotalQuantityByArticul.SelectRow(rhArticulKod);
                    //  }
                    //  finally
                    //  {
                    //      gridViewPlanTotalQuantityByArticul.EndUpdate();
                    //  }
                    //-----------------------------------
                    break;
            }

            //var colNomZadany = gridViewZadanyList.Columns.FirstOrDefault(c => c.FieldName == "kmlID");
            var colNomZadany = gridViewZadanyList.Columns.FirstOrDefault(c => c.FieldName == "pszNom");
            if (colNomZadany == null) return;

            int rhNomZadany = gridViewZadanyList.LocateByValue(0, colNomZadany, _pendingPszNom);
            if (!gridViewZadanyList.IsValidRowHandle(rhNomZadany) || !gridViewZadanyList.IsDataRow(rhNomZadany)) return;

            gridViewZadanyList.BeginUpdate();
            try
            {
                if (gridViewZadanyList.FocusedRowHandle == rhNomZadany)
                    gridViewZadanyList.FocusedRowHandle = GridControl.InvalidRowHandle;

                gridViewZadanyList.FocusedRowHandle = rhNomZadany;
                gridViewZadanyList.MakeRowVisible(rhNomZadany);
                gridViewZadanyList.ClearSelection();
                gridViewZadanyList.SelectRow(rhNomZadany);
            }
            finally
            {
                gridViewZadanyList.EndUpdate();
            }

            _autoSelectPending = false;
            Debug.WriteLine($"FindPzvNomZadInNzp completed for search text: {textBoxPzvNomZadSearch.Text}");
        }
        private void FindPzvNomZadInMachineListNzp(string _pszNom)
        {
            // Параметр вместо интерполяции
            int kmlId = _dbHelper.ExecuteScalar(
                @"SELECT TOP (1) pszm.pszkmKmlID
                  FROM plan_sezon_zad_knitMachine pszm
                  WHERE pszm.pszkmPszNom = @pszNom",
                new Dictionary<string, object>
                {
                    ["@pszNom"] = _pszNom
                });

            if (kmlId <= 0) return;

            _pendingPszNom = _pszNom;

            //------------------------------------
            var colKnitMachine = gridViewPlanTotalHoursByKnitMachine.Columns.FirstOrDefault(c => c.FieldName == "kmlID");
            if (colKnitMachine == null) return;

            int rhKnitMachine = gridViewPlanTotalHoursByKnitMachine.LocateByValue(0, colKnitMachine, kmlId);
            if (!gridViewPlanTotalHoursByKnitMachine.IsValidRowHandle(rhKnitMachine)
                || !gridViewPlanTotalHoursByKnitMachine.IsDataRow(rhKnitMachine)) return;

            gridViewPlanTotalHoursByKnitMachine.BeginUpdate();
            try
            {
                if (gridViewPlanTotalHoursByKnitMachine.FocusedRowHandle == rhKnitMachine)
                    gridViewPlanTotalHoursByKnitMachine.FocusedRowHandle = GridControl.InvalidRowHandle;

                gridViewPlanTotalHoursByKnitMachine.FocusedRowHandle = rhKnitMachine;
                gridViewPlanTotalHoursByKnitMachine.MakeRowVisible(rhKnitMachine);
                gridViewPlanTotalHoursByKnitMachine.ClearSelection();
                gridViewPlanTotalHoursByKnitMachine.SelectRow(rhKnitMachine);
            }
            finally
            {
                gridViewPlanTotalHoursByKnitMachine.EndUpdate();
            }

            //-----------------------------------
        }
        private void FindPzvNomZadInArticulListNzp(string _pszNom)
        {
            // Параметр вместо интерполяции
            //int count = ExecuteScalar<int>("SELECT COUNT(*) FROM Users");
            string _xKod = _dbHelper.ExecuteScalar<string>(
                @"SELECT TOP (1) left(rzv.kod, 7) as kod
                  FROM raskr_zeh_vyaz rzv
                  WHERE zad_pl = @pszNom",
                new Dictionary<string, object>
                {
                    ["@pszNom"] = _pszNom
                });

            if (Convert.ToInt32(_xKod) <= 0) return;

            _pendingPszNom = _pszNom;

            //------------------------------------
            var colArticulKod = gridViewPlanTotalQuantityByArticul.Columns.FirstOrDefault(c => c.FieldName == "Kod");
            if (colArticulKod == null) return;

            int rhArticulKod = gridViewPlanTotalQuantityByArticul.LocateByValue(0, colArticulKod, _xKod);
            if (!gridViewPlanTotalQuantityByArticul.IsValidRowHandle(rhArticulKod)
                || !gridViewPlanTotalQuantityByArticul.IsDataRow(rhArticulKod)) return;

            gridViewPlanTotalQuantityByArticul.BeginUpdate();
            try
            {
                if (gridViewPlanTotalQuantityByArticul.FocusedRowHandle == rhArticulKod)
                    gridViewPlanTotalQuantityByArticul.FocusedRowHandle = GridControl.InvalidRowHandle;

                gridViewPlanTotalQuantityByArticul.FocusedRowHandle = rhArticulKod;
                gridViewPlanTotalQuantityByArticul.MakeRowVisible(rhArticulKod);
                gridViewPlanTotalQuantityByArticul.ClearSelection();
                gridViewPlanTotalQuantityByArticul.SelectRow(rhArticulKod);
            }
            finally
            {
                gridViewPlanTotalQuantityByArticul.EndUpdate();
            }
            //-----------------------------------
        }
        private void textBoxPzvNomZadSearch_TextChanged(object sender, EventArgs e)
        {
            Debug.WriteLine($"textBoxPzvNomZadSearch_TextChanged triggered for text: {textBoxPzvNomZadSearch.Text}");
            if (textBoxPzvNomZadSearch.Text.Length > 0)
            {
                FindPzvNomZadInNzp();
            }
            Debug.WriteLine($"Finished textBoxPzvNomZadSearch_TextChanged for text: {textBoxPzvNomZadSearch.Text}");
        }

        private async void layoutControlGroup1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            //int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);
            //switch (buttonIndex)
            //{
            //    case 0:
            //        MessageBox.Show("1");
            //        break;
            //    case 10:
            //        try
            //        {
            //            PZVCurrentMachineAssignmentReport report1 = new PZVCurrentMachineAssignmentReport();
            //            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            //            reportPrintTool1.ShowPreviewDialog();
            //        }
            //        catch (Exception ex)
            //        {
            //            await _logger.LogErrorAsync(ex, $"Ошибка печати накладной");
            //        }
            //        break;
            //}

            string buttonTag = (e.Button as GroupBoxButton)?.Tag.ToString();
            switch (buttonTag)
            {
                //case 0:
                //    MessageBox.Show("1");
                //    break;
                case "lcg1CurrKMAssign":
                    try
                    {
                        PZVCurrentMachineAssignmentReport report1 = new PZVCurrentMachineAssignmentReport();
                        ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
                        reportPrintTool1.ShowPreviewDialog();
                    }
                    catch (Exception ex)
                    {
                        await _logger.LogErrorAsync(ex, $"Ошибка печати накладной");
                    }
                    break;
                case "lcg1LoadDataFromTSD":
                    string query = "exec dbo.loadDataFromTSD";
                    _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { });
                    break;
            }
        }

        private void gridViewPZVOperList_BeforeLeaveRow(object sender, RowAllowEventArgs e)
        {
            Debug.WriteLine($"gridViewPZVOperList_BeforeLeaveRow triggered for row handle {e.RowHandle}, TopRowIndex={gridViewPZVOperList.TopRowIndex}, TopRowIndexPZV={TopRowIndexPZV}");
        }
        private async void gridViewPlanTotalQuantityByArticul_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                var viewTop = (GridView)sender;

                if (!viewTop.IsDataRow(e.FocusedRowHandle))
                    return;

                var row = viewTop.GetRow(e.FocusedRowHandle) as PlanTotalQuantityByArticul;
                if (row == null) return;

                //await ReloadZadanyListForArticulAsync(row.Kod);
                await ReloadZadanyForArticulKodAsync(row.Kod);
                Debug.WriteLine($"gridViewPlanTotalQuantityByArticul_FocusedRowChanged completed for kmlID={row.Kod}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в gridViewPlanTotalQuantityByArticul_FocusedRowChanged: {ex.Message}");
            }
        }

        private async Task LoadZadanyListByArticulNewDataAsync(string _kod)
        {
            // 1️ отменяем предыдущий запрос
            _loadCts?.Cancel();
            _loadCts?.Dispose();
            _loadCts = new CancellationTokenSource();

            var token = _loadCts.Token;

            try
            {
                // 2️ сразу очищаем грид + показываем загрузку
                await this.InvokeAsync(() =>
                {
                    gridControlZadanyList.BeginUpdate();
                    gridViewZadanyList.ShowLoadingPanel();

                    _zadanyListNewBindingSource.DataSource =
                        new BindingList<PZVZadanyList>();
                });

                // 3️ долгий запрос
                var bs = await _vyazService.GetZadanyListByArticulKod(_kod, token);

                // если отменили — просто выходим
                if (token.IsCancellationRequested)
                    return;

                // 4️ привязываем результат
                await this.InvokeAsync(() =>
                {
                    _zadanyListNewBindingSource.DataSource = bs.DataSource;
                });

                if (bs.Count == 0)
                {
                    await _logger.LogEventAsync(
                        "Данные ZadanyListByArticul не найдены",
                        nameof(LoadZadanyListByArticulNewDataAsync)
                    );
                }
                else
                {
                    await _logger.LogEventAsync(
                        "Данные ZadanyListByArticul успешно загружены",
                        nameof(LoadZadanyListByArticulNewDataAsync)
                    );
                }
                Debug.WriteLine($"LoadZadanyListByArticulNewDataAsync completed");
            }
            catch (OperationCanceledException)
            {
                // молча — это нормальный сценарий
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка загрузки данных ZadanyListByArticul");
            }
            finally
            {
                await this.InvokeAsync(() =>
                {
                    gridViewZadanyList.HideLoadingPanel();
                    gridControlZadanyList.EndUpdate();
                    gridViewZadanyList.ExpandAllGroups();
                });
            }
        }
        private void gridViewRzvPachListByNom_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
        }
        private void gridViewSmenZadanyOtp_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                Debug.WriteLine($"gridViewSmenZadanyOtp_FocusedRowChanged triggered for new focused row handle {e.FocusedRowHandle}");
                var _view = sender as GridView;
                TopRowIndexSmenZadany = _view.FocusedRowHandle;
                SmenZadanyFocusedRowChanged(_view, e.FocusedRowHandle);
                Debug.WriteLine($"Finished gridViewSmenZadanyOtp_FocusedRowChanged for new focused row handle {e.FocusedRowHandle}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка gridViewSmenZadanyOtp_FocusedRowChanged: {ex.Message}");
            }
        }
    }
}


namespace SewingProduction.Features.KnittingProduction.Forms //.PlanZagrVyaz
{
    // (оставляем namespace как в файле, чтобы класс был доступен)
    /// <summary>
    /// Координатор перезагрузок по "объектам" (хранимка/вьюха/логическая группа данных).
    /// Делает накопление событий, анти-дребезг (debounce), max-wait и защиту от параллельных перезагрузок одного и того же объекта.
    /// </summary>
    internal sealed class ObjectRefreshCoordinator : IDisposable
    {
        private readonly Func<string, Task> _reloadByObjectNameAsync;
        private readonly TimeSpan _debounce;
        private readonly TimeSpan _maxWait;

        private readonly object _lock = new();
        private readonly HashSet<string> _pending = new(StringComparer.OrdinalIgnoreCase);

        private DateTime _batchStartedUtc = DateTime.MinValue;

        private CancellationTokenSource _debounceCts;
        private CancellationTokenSource _maxWaitCts;

        // single-flight + cancel per object
        private readonly Dictionary<string, CancellationTokenSource> _inflightCts = new(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, SemaphoreSlim> _objectLocks = new(StringComparer.OrdinalIgnoreCase);

        // ограничение параллелизма по форме
        private readonly SemaphoreSlim _globalGate;

        public ObjectRefreshCoordinator(
            Func<string, Task> reloadByObjectNameAsync,
            TimeSpan? debounce = null,
            TimeSpan? maxWait = null,
            int maxParallelReloads = 2)
        {
            _reloadByObjectNameAsync = reloadByObjectNameAsync ?? throw new ArgumentNullException(nameof(reloadByObjectNameAsync));
            _debounce = debounce ?? TimeSpan.FromMilliseconds(500);
            _maxWait = maxWait ?? TimeSpan.FromSeconds(3);
            _globalGate = new SemaphoreSlim(Math.Max(1, maxParallelReloads));
        }

        public void Request(string objectName)
        {
            if (string.IsNullOrWhiteSpace(objectName)) return;

            lock (_lock)
            {
                if (_batchStartedUtc == DateTime.MinValue)
                    _batchStartedUtc = DateTime.UtcNow;

                _pending.Add(objectName);

                // debounce от последнего события
                _debounceCts?.Cancel();
                _debounceCts = new CancellationTokenSource();
                _ = FireAfterAsync(_debounce, _debounceCts.Token, isMaxWait: false);

                // max-wait от первого события пачки
                if (_maxWaitCts == null)
                {
                    _maxWaitCts = new CancellationTokenSource();
                    _ = FireAfterAsync(_maxWait, _maxWaitCts.Token, isMaxWait: true);
                }
            }
        }

        private async Task FireAfterAsync(TimeSpan delay, CancellationToken token, bool isMaxWait)
        {
            try
            {
                await Task.Delay(delay, token).ConfigureAwait(false);

                string[] toRun;
                lock (_lock)
                {
                    if (_pending.Count == 0) return;

                    if (isMaxWait && _batchStartedUtc == DateTime.MinValue)
                        return;

                    toRun = _pending.ToArray();
                    _pending.Clear();

                    _batchStartedUtc = DateTime.MinValue;

                    _debounceCts?.Cancel();
                    _debounceCts = null;

                    _maxWaitCts?.Cancel();
                    _maxWaitCts = null;
                }

                foreach (var objName in toRun)
                    _ = RunSingleAsync(objName);
            }
            catch (OperationCanceledException) { }
        }

        private SemaphoreSlim GetObjectLock(string objectName)
        {
            lock (_lock)
            {
                if (!_objectLocks.TryGetValue(objectName, out var sem))
                {
                    sem = new SemaphoreSlim(1, 1);
                    _objectLocks[objectName] = sem;
                }
                return sem;
            }
        }

        private async Task RunSingleAsync(string objectName)
        {
            var objLock = GetObjectLock(objectName);

            await objLock.WaitAsync().ConfigureAwait(false);
            CancellationTokenSource cts;
            try
            {
                if (_inflightCts.TryGetValue(objectName, out var old))
                {
                    old.Cancel();
                    old.Dispose();
                }

                cts = new CancellationTokenSource();
                _inflightCts[objectName] = cts;
            }
            finally
            {
                objLock.Release();
            }

            try
            {
                await _globalGate.WaitAsync(cts.Token).ConfigureAwait(false);
                try
                {
                    await _reloadByObjectNameAsync(objectName).ConfigureAwait(false);
                }
                finally
                {
                    _globalGate.Release();
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ObjectRefreshCoordinator] Reload failed: {objectName}. {ex}");
            }
        }

        public void Dispose()
        {
            lock (_lock)
            {
                _debounceCts?.Cancel();
                _maxWaitCts?.Cancel();
                _debounceCts?.Dispose();
                _maxWaitCts?.Dispose();
                _debounceCts = null;
                _maxWaitCts = null;
            }

            foreach (var cts in _inflightCts.Values)
            {
                cts.Cancel();
                cts.Dispose();
            }

            foreach (var sem in _objectLocks.Values)
                sem.Dispose();

            _globalGate.Dispose();
        }
    }
}
