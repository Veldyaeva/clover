using DevExpress.CodeParser;
using DevExpress.CodeParser;
using DevExpress.Data;
using DevExpress.Mvvm.Native;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraReports.UI;
using Newtonsoft.Json;
using SewingProduction.Core.Class.Settings;
using SewingProduction.Core.Class.Settings;
using SewingProduction.Core.helpers;
using SewingProduction.Core.interfaces;
using SewingProduction.Core.interfaces;
using SewingProduction.Core.Models;
using SewingProduction.Core.Models;
using SewingProduction.Core.services;
using SewingProduction.Core.services;
using SewingProduction.Extensions;
using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Features.KnittingProduction.Services;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Models;
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
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;
using static DevExpress.XtraEditors.XtraInputBox;
using static SewingProduction.Core.helpers.BindingSourceHelper;
using static SewingProduction.Core.helpers.ServiceBrokerHelper;
using static SewingProduction.Helpers.GridHelper;
using Formatting = Newtonsoft.Json.Formatting;
using Volatile = System.Threading.Volatile;





namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class PlanZagrVyaz : CustomForm//, IThemeable
                                                  , IServiceBrokerHost
    //, IDataUpdatableForm, IDataUpdatableFormAsync
    {
        int vyazPodrKod = 0;


        private readonly ServiceBrokerController _sbController;

        // Таблицы, изменения в которых НЕ должны инициировать обновление UI
        // (типичные LEFT JOIN справочники и прочий "шум").
        private readonly HashSet<string> _ignoredServiceBrokerTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, Func<Task>> _objectRestartMap = null!;
        private CancellationTokenSource? _loadCts;
        private CancellationTokenSource? _lifetimeCts;

        private readonly DebouncedLoader _loader = new(delayMs: 300);

        private static DatabaseHelper _dbHelper;
        private static DbService _dbService;
        private static MlService _mlService;
        private static ArtNormRepository _anService;
        private static ServiceBrokerService _sbService;
        private static BulkHelper _bulkHelper;
        private static GridHelper _gridHelper;
        //        private static BindingSourceHelper _bSHelper;
        private readonly ILogger _logger = new FileLogger();
        private readonly VyazService _vyazService;

        string _xColumn = string.Empty;
        int _xPzvID = 0;
        private bool _isCountTabKM = false;
        private bool _closing;
        private string? _pendingPszNom;
        private bool _autoSelectPending;
        private bool _suppressZadanyFocusHandler;
        private bool _suppressPachFocusHandler;
        private bool _suppressZadanyFocusedChanged;
        private int _rzvLoadVersion = 0;
        private int _loadVersion = 0;

        private List<KnitWorkingShiftSmen> _zonesCache = new List<KnitWorkingShiftSmen>();

        private List<PlanTotalHoursByKnitMachine> _currentPlanTotalHoursByKnitMachineData = new List<PlanTotalHoursByKnitMachine>();
        private List<PlanTotalHoursByKnitMachine> planTotalHoursByKnitMachineData = new List<PlanTotalHoursByKnitMachine>();
        private BindingList<PlanTotalHoursByKnitMachine> _planTotalHoursByKnitMachineBindingList;
        private BindingSource _planTotalHoursByKnitMachineBindingSource;

        private List<ZadanyListByMachine> _currentZadanyListByMachineData = new List<ZadanyListByMachine>();
        private List<ZadanyListByMachine> zadanyListByMachineData = new List<ZadanyListByMachine>();
        private BindingList<ZadanyListByMachine> _zadanyListByMachineBindingList;
        private BindingSource _zadanyListByMachineBindingSource;

        private List<ZadanyListByMachine> _currentZadanyListByMachineNewData = new List<ZadanyListByMachine>();
        private List<ZadanyListByMachine> zadanyListByMachineNewData = new List<ZadanyListByMachine>();
        private BindingList<ZadanyListByMachine> _zadanyListByMachineNewBindingList;
        private BindingSource _zadanyListByMachineNewBindingSource;

        private List<RzvPachListByNom> _currentRzvPachListByNomData = new List<RzvPachListByNom>();
        private List<RzvPachListByNom> rzvPachListByNomData = new List<RzvPachListByNom>();
        private BindingList<RzvPachListByNom> _rzvPachListByNomBindingList;
        private BindingSource _rzvPachListByNomBindingSource;

        private List<RzvPachListByNom> _currentRzvPachListByNomNewData = new List<RzvPachListByNom>();
        private List<RzvPachListByNom> rzvPachListByNomNewData = new List<RzvPachListByNom>();
        private BindingList<RzvPachListByNom> _rzvPachListByNomNewBindingList;
        private BindingSource _rzvPachListByNomNewBindingSource;

        private List<PZVOperList> _currentPZVOperListByPachListData = new List<PZVOperList>();
        private List<PZVOperList> pZVOperListByPachListData = new List<PZVOperList>();
        private BindingList<PZVOperList> _pZVOperListByPachListBindingList;
        private BindingSource _pZVOperListByPachListBindingSource;

        private List<PZVOperList> _currentPZVOperListByPachListNewData = new List<PZVOperList>();
        private List<PZVOperList> pZVOperListByPachListNewData = new List<PZVOperList>();
        private BindingList<PZVOperList> _pZVOperListByPachListNewBindingList;
        private BindingSource _pZVOperListByPachListNewBindingSource;

        private List<ArtNormN> _currentArtNormNData = new List<ArtNormN>();
        private List<ArtNormN> artNormNData = new List<ArtNormN>();
        private BindingList<ArtNormN> _artNormNBindingList;
        private BindingSource _artNormNBindingSource;

        private List<NormRasz> _currentNormRaszData = new List<NormRasz>();
        private List<NormRasz> normRaszData = new List<NormRasz>();
        private BindingList<NormRasz> _normRaszBindingList;
        private BindingSource _normRaszBindingSource;

        private List<MlOp> _currentMlOpData = new List<MlOp>();
        private List<MlOp> mlOpData = new List<MlOp>();
        private BindingList<MlOp> _mlOpBindingList;
        private BindingSource _mlOpBindingSource;

        private BindingSource _smenZadanyVyazBindingSource;

        private BindingSource _smenZadanyVyazNewBindingSource;

        private List<KnitWorkingShiftSmen> KnitWorkingShiftSmenData = new List<KnitWorkingShiftSmen>();
        private BindingList<KnitWorkingShiftSmen> _knitWorkingShiftSmenBindingList;
        private BindingSource _knitWorkingShiftSmenBindingSource;

        private BindingSource _naryadZadanyVyazBindingSource;


        private CancellationTokenSource? _focusLoadCts;
        private int _focusSeq;
        private int _isUiRefreshing;

        public PlanZagrVyaz(UserClass User) : base(User)
        {
            InitializeComponent();
            DxSkinFix.ResetLabelsToSkin(this);
            SetupGridNaryadZadanyEvents();
            _dbHelper = new DatabaseHelper("ace");
            _dbService = new DbService(_dbHelper);
            _anService = new ArtNormRepository(_dbHelper);
            _sbService = new ServiceBrokerService(_dbHelper);
            _sbController = new ServiceBrokerController(this);
            //_sbService.Changed += UpdateDataInFormAsync;
            _bulkHelper = new BulkHelper();
            _gridHelper = new GridHelper();
            //_sbHelper = new ServiceBrokerHelper(this, List<string>{ "GetPlanZagrVyazByPachList" });

            _vyazService = new VyazService(_dbHelper);
            _mlService = new MlService(_dbHelper);
        //    ThemeManager.UpdateTheme(this);

            _smenZadanyVyazBindingSource = new BindingSource
            {
                DataSource = new BindingList<SmenZadanyVyaz>()
            };
            _smenZadanyVyazNewBindingSource = new BindingSource
            {
                DataSource = new BindingList<SmenZadanyVyaz>()
            };

            //_broker.Changed += UpdateDataInFormAsync;
            //_sbHelper.UseSchemaInListenName = false;

            //_broker.StartBroker();
            //_broker.StartListening(columns, listenName);

            //нужно будет определять, мастер вяз цеха или отпарки заходит в форму и
            //сохранять признак подразделения для дальнейшней загрузки операций только того подразделения, чей мастер зашел
            // пока что примем, что заходит только мастер вяз цеха, признак пропишем жестко 1
            gridControlZadanyListByMachine.ForceInitialize();
            gridViewZadanyListByMachine.RefreshData();
            gridControlRzvPachListByNom.ForceInitialize();
            gridViewRzvPachListByNom.RefreshData();
            gridViewPZVOperList.ShownEditor += gridViewPZVOperList_ShownEditor;
            gridViewPZVOperList.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDownFocused;
            vyazPodrKod = 1;
        }
        public static class DxSkinFix
        {
            public static void ResetLabelsToSkin(Control root)
            {
                foreach (Control c in root.Controls)
                {
                    if (c is LabelControl lc)
                    {
                        lc.Appearance.BackColor = Color.Empty;
                        lc.Appearance.ForeColor = Color.Empty;
                        lc.Appearance.Options.UseBackColor = false;
                        lc.Appearance.Options.UseForeColor = false;
                        lc.LookAndFeel.UseDefaultLookAndFeel = true;
                    }

                    if (c.HasChildren)
                        ResetLabelsToSkin(c);
                }
            }
        }
        #region ServiceBroker
        private void InitObjectRestartMap()
        {
            try
            {
                _objectRestartMap = new Dictionary<string, Func<Task>>(StringComparer.OrdinalIgnoreCase)
                {
                    ["GetSmenZadanyVyaz"] = async () =>
                    {
                        await LoadSmenZadanyVyazNewDataAsync(vyazPodrKod); // сменное задание
                                                                           //SetGroupExpandState(); // sync-метод — просто вызываем
                    },

                    ["knitWorkingShiftNewCurrentSmen_view"] = async () =>
                    {
                        SmenZadanyFocusedRowChanged(advBandedGridViewSmenZadany.FocusedRowHandle); // наряд-задание
                    },

                    ["GetPlanZagrVyazByPachList"] = async () =>
                    {
                        await LoadPlanZagrVyazByZadanySelection(); // операции по расчетам/пачкам
                    }
                };
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
            await _sbController.InitAsync(ct);

            var tables = _sbController.Helper?.GetListeningTables() ?? Array.Empty<string>();
            Debug.WriteLine($"[PlanZagrVyaz] Listening tables: {string.Join(", ", tables)}");

            //------------------------
        }

        public async Task<List<ServiceBrokerModel.TableListenInfo>> LoadListenInfoByObjectNameAsync(
            string objectName, CancellationToken ct)
        {
            return await _sbService.GetObjectListForServiceBroker(objectName, ct);
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

        //private async Task RestartDataByObjectNameAsync(string objectName)
        //{
        //    await InvokeOnUiAsync(async () =>
        //    {
        //        Debug.WriteLine($"[PlanZagrVyaz] RestartDataByObjectNameAsync(UI): {objectName}");

        //        if (string.Equals(objectName, "GetPlanZagrVyazByPachList", StringComparison.OrdinalIgnoreCase))
        //        {
        //            await LoadPlanTotalHoursByKnitMachineDataAsync();
        //        }
        //        else if (string.Equals(objectName, "getSmenZadanyVyaz", StringComparison.OrdinalIgnoreCase))
        //        {
        //            await LoadSmenZadanyVyazDataAsync();
        //        }
        //    });
        //}

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

        //public async Task UpdateDataInFormAsync(string table, string changedFieldsCsv)
        //{
        //    if (_sbHelper == null) return;

        //    await _sbHelper.HandleBrokerUpdateAsync(table, changedFieldsCsv);

        //    // Забираешь список того, что реально затронуто (с учетом пересечения полей)
        //    var affected = _sbHelper.DrainPending();

        //    // Дальше твоя логика: сгруппировать по ObjectName и обновить нужные данные
        //    // Пример:
        //    foreach (var group in affected.GroupBy(x => x.ObjectName))
        //    {
        //        var objectName = group.Key;
        //        // group: содержит TableKey, MatchedFields и т.д.
        //        // Ты решаешь, что и как перезагружать.
        //        MessageBox.Show($"{objectName}");
        //    }
        //}
        //public async Task UpdateDataInFormAsync(string table, List<string> changedFields)
        //{
        //    if (_sbHelper == null) return;

        //    await _sbHelper.HandleBrokerUpdateAsync(table, changedFields);
        //    var affected = _sbHelper.DrainPending();
        //    // твоя логика
        //    //MessageBox.Show($"{ affected }");
        //    LoadSmenZadanyVyazDataAsync();
        //    MessageBox.Show($"{table} updated");

        //}
        ////public async Task UpdateDataInFormAsync(string table)
        ////{
        ////    if (_sbHelper == null) return;

        ////    // Глобальный фильтр "шумовых" таблиц (LEFT JOIN справочники и т.п.)
        ////    if (_ignoredServiceBrokerTables != null && _ignoredServiceBrokerTables.Contains(table))
        ////        return;

        ////    // ServiceBroker.cs в текущей версии присылает только table (без списка колонок),
        ////    // поэтому берём "интересующие поля" из подписок.
        ////    var changedFields = _sbHelper.GetUnionListeningFieldsForTable(table);

        ////    await _sbHelper.HandleBrokerUpdateAsync(table, changedFields);

        ////    // Сюда попадают только "объекты" (вьюхи/хранимки), которые реально затронуты,
        ////    // с учётом пересечения полей/таблиц внутри ServiceBrokerHelper.
        ////    var objectsToRestart = _sbHelper.DrainPending(); // List<string>

        ////    if (objectsToRestart == null || objectsToRestart.Count == 0)
        ////        return;

        ////    // Ставим на перезагрузку через анти-дребезг/накопление
        ////    foreach (var objName in objectsToRestart)
        ////        _refreshCoordinator?.Request(objName);
        ////}
        // Важно: ServiceBroker дергает owner.UpdateDataInFormAsync(...)
        // Если сигнатуры нет/закомментирована — ты НЕ увидишь обновлений.
        public async Task UpdateDataInFormAsync(string table, string? changedFieldsCsv = null)
        {
            try
            {
                Debug.WriteLine($"[PlanZagrVyaz] UpdateDataInFormAsync: table={table}, fields={changedFieldsCsv}");
                await _sbController.HandleUpdateAsync(table, changedFieldsCsv ?? string.Empty);

                if (_sbController.Coordinator != null)
                {
                    var stats = _sbController.Coordinator.GetStatistics();
                    Debug.WriteLine($"[PlanZagrVyaz] RefreshCoordinator stats: Pending={stats.PendingCount}, InFlight={stats.InFlightCount}, TotalRequests={stats.TotalRequests}, TotalExecutions={stats.TotalExecutions}, CascadePreventions={stats.CascadePreventions}");
                }
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

        private async Task InitializeBindingsAsync()
        {
            try
            {
                var planTotalHoursByKnitMachineTask = Task.Run(() =>
                {
                    _planTotalHoursByKnitMachineBindingList = new BindingList<PlanTotalHoursByKnitMachine>();
                    _planTotalHoursByKnitMachineBindingSource = new BindingSource { DataSource = _planTotalHoursByKnitMachineBindingList };
                });
                var zadanyListByMachineTask = Task.Run(() =>
                {
                    _zadanyListByMachineBindingList = new BindingList<ZadanyListByMachine>();
                    _zadanyListByMachineBindingSource = new BindingSource { DataSource = _zadanyListByMachineBindingList };
                });
                var zadanyListByMachineNewTask = Task.Run(() =>
                {
                    _zadanyListByMachineNewBindingList = new BindingList<ZadanyListByMachine>();
                    _zadanyListByMachineNewBindingSource = new BindingSource { DataSource = _zadanyListByMachineNewBindingList };
                });
                var rzvPachListByNomTask = Task.Run(() =>
                {
                    _rzvPachListByNomBindingList = new BindingList<RzvPachListByNom>();
                    _rzvPachListByNomBindingSource = new BindingSource { DataSource = _rzvPachListByNomBindingList };
                });
                var rzvPachListByNomNewTask = Task.Run(() =>
                {
                    _rzvPachListByNomNewBindingList = new BindingList<RzvPachListByNom>();
                    _rzvPachListByNomNewBindingSource = new BindingSource { DataSource = _rzvPachListByNomNewBindingList };
                });
                var pZVOperListByPachListTask = Task.Run(() =>
                {
                    _pZVOperListByPachListBindingList = new BindingList<PZVOperList>();
                    _pZVOperListByPachListBindingSource = new BindingSource { DataSource = _pZVOperListByPachListBindingList };
                });
                var pZVOperListByPachListNewTask = Task.Run(() =>
                {
                    _pZVOperListByPachListNewBindingList = new BindingList<PZVOperList>();
                    _pZVOperListByPachListNewBindingSource = new BindingSource { DataSource = _pZVOperListByPachListNewBindingList };
                });
                var artNormNTask = Task.Run(() =>
                {
                    _artNormNBindingList = new BindingList<ArtNormN>();
                    _artNormNBindingSource = new BindingSource { DataSource = _artNormNBindingList };
                });
                var normRaszTask = Task.Run(() =>
                {
                    _normRaszBindingList = new BindingList<NormRasz>();
                    _normRaszBindingSource = new BindingSource { DataSource = _normRaszBindingList };
                });
                var mlOpTask = Task.Run(() =>
                {
                    _mlOpBindingList = new BindingList<MlOp>();
                    _mlOpBindingSource = new BindingSource { DataSource = _mlOpBindingList };
                });
                var knitWorkingShiftTask = Task.Run(() =>
                {
                    _knitWorkingShiftSmenBindingList = new BindingList<KnitWorkingShiftSmen>();
                    _knitWorkingShiftSmenBindingSource = new BindingSource { DataSource = _knitWorkingShiftSmenBindingList };
                });
                await Task.WhenAll(planTotalHoursByKnitMachineTask, zadanyListByMachineTask, zadanyListByMachineNewTask
                        , rzvPachListByNomTask, rzvPachListByNomNewTask
                        , pZVOperListByPachListTask, pZVOperListByPachListNewTask
                        , artNormNTask, normRaszTask
                        , mlOpTask
                        , knitWorkingShiftTask);

                _smenZadanyVyazBindingSource = new BindingSource
                {
                    DataSource = new BindingList<SmenZadanyVyaz>()
                };
                _smenZadanyVyazNewBindingSource = new BindingSource
                {
                    DataSource = new BindingList<SmenZadanyVyaz>()
                };

                #region описание gridControlPlanTotalHoursByKnitMachine "общие часы по вяз машинам/зонам"
                gridControlPlanTotalHoursByKnitMachine.DataSource = _planTotalHoursByKnitMachineBindingSource;
                gridColumnPlanTotalHoursByKnitMachineKmaNumber.FieldName = "kmaNumber";
                gridColumnPlanTotalHoursByKnitMachineKmlNumber.FieldName = "kmlNumber";
                gridColumnPlanTotalHoursByKnitMachineDateZap.FieldName = "DateZap";
                gridColumnPlanTotalHoursByKnitMachineMgZap.FieldName = "mgZap";
                gridColumnPlanTotalHoursByKnitMachineHoursTotal.FieldName = "hoursTotal";
                gridColumnPlanTotalHoursByKnitMachineIdVyazClass.FieldName = "idVyazClass";
                gridColumnPlanTotalHoursByKnitMachineKnitClass.FieldName = "knitClass";
                gridColumnPlanTotalHoursByKnitMachineKmlID.FieldName = "kmlID";
                #endregion

                #region описание gridControlZadanyListByMachine "задания по номеру машины"
                gridControlZadanyListByMachine.DataSource = _zadanyListByMachineBindingSource;
                gridColumnZadanyListByMachinePszNom.FieldName = "pszNom";
                gridColumnZadanyListByMachineNom.FieldName = "nom";
                gridColumnZadanyListByMachineArticul.FieldName = "articul";
                gridColumnZadanyListByMachineZvet.FieldName = "zvet";
                gridColumnZadanyListByMachineKol.FieldName = "kol";
                gridColumnZadanyListByMachineDatePryazZayav.FieldName = "pryazZayav";
                gridColumnZadanyListByMachineData_plan.FieldName = "data_plan";
                gridColumnZadanyListByMachineVid_stir.FieldName = "vid_stir";
                gridColumnZadanyListByMachineDopr_name.FieldName = "dopr_name";
                gridColumnZadanyListByMachineKmlID.FieldName = "kmlID";
                gridColumnZadanyListByMachineSyncSelection.FieldName = "SyncSelection";
                gridColumnZadanyListByMachineGradacia.FieldName = "Gradacia";
                gridColumnZadanyListByMachineYearPlan.FieldName = "yearPlan";

                gridColumnZadanyListByMachineData_plan.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridColumnZadanyListByMachineData_plan.DisplayFormat.FormatString = "dd.MM.yy";
                _gridHelper.AutoRowFilterConfig(gridViewZadanyListByMachine as GridView, 0);
                gridViewZadanyListByMachine.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;

                //gridViewRzvPachListByNom.OptionsView.ShowAutoFilterRow = false;
                //gridViewRzvPachListByNom.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
                //gridViewRzvPachListByNom.OptionsFilter.AllowFilterEditor = false;
                //----------------------------------------
                gridViewZadanyListByMachine.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
                gridColumnZadanyListByMachineGradacia.OptionsColumn.AllowEdit = false;
                gridColumnZadanyListByMachineGradacia.OptionsColumn.ReadOnly = false;
                //----------------------------------------
                repositoryItemCheckEdit1.MouseUp += (s, e) =>
                {
                    BeginInvoke(new Action(() => SyncSelectionUpdate()));
                };
                //----------------------------------------
                #endregion

                #region описание gridControlRzvPachListByNom "пачки по расчету вяз"
                gridControlRzvPachListByNom.DataSource = _rzvPachListByNomBindingSource;
                gridColumnRzvPachListByNomNomZad.FieldName = "nomZad";
                gridColumnRzvPachListByNomNom.FieldName = "nom";
                gridColumnRzvPachListByNomNom_n.FieldName = "nom_n";
                gridColumnRzvPachListByNomN_pach.FieldName = "n_pach";
                gridColumnRzvPachListByNomRazm.FieldName = "razm";
                gridColumnRzvPachListByNomKol.FieldName = "kol";
                gridColumnRzvPachListByNomGradacia.FieldName = "gradacia";
                gridColumnRzvPachListByNomSyncSelection.FieldName = "SyncSelection";

                //gridViewRzvPachListByNom.ShowingEditor += (s, e) =>
                //{
                //    var view = (GridView)s;
                //    // если колонка gridColumnRzvPachListByNomGradacia редактировать запрещена:
                //    if (view.FocusedColumn == gridColumnRzvPachListByNomGradacia &&
                //        gridColumnRzvPachListByNomGradacia.OptionsColumn.ReadOnly)
                //    {
                //        e.Cancel = true; // запретить редактирование
                //        MessageBox.Show("Внимание! Технологом не была определена необходимость градации - нельзя проставить признак градации на пачку!");
                //    }
                //};
                #endregion

                #region gridControlSmenZadany "сменное задание"
                gridControlSmenZadany.DataSource = _smenZadanyVyazBindingSource;
                bandedGridSmenZadanyColumnKmaID.FieldName = "kmaID";
                bandedGridSmenZadanyColumnKmaNumber.FieldName = "kmaNumber";
                //bandedGridSmenZadanyColumnKmlID.FieldName = "kmlID";
                bandedGridSmenZadanyColumnKmlID.FieldName = "kwsmlKmlID";
                bandedGridSmenZadanyColumnKmlNumber.FieldName = "kmlNumber";
                bandedGridSmenZadanyColumnIDVyazClass.FieldName = "idVyazClass";
                bandedGridSmenZadanyColumnNameVyazClass.FieldName = "nameVyazClass";
                bandedGridSmenZadanyColumnSmenLength.FieldName = "smenLength";
                //bandedGridSmenZadanyColumnChasNaznZad.FieldName = "chasNaznZad";
                //bandedGridSmenZadanyColumnChasNaznZadGroup.FieldName = "chasNaznZadGroup";
                advBandedGridViewSmenZadany.Columns["chasNaznZadGroup"].UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
                bandedGridSmenZadanyColumnChasNotConfirmedZad.FieldName = "chasNotConfirmedZad";
                bandedGridSmenZadanyColumnChasNotConfirmedZadGroup.FieldName = "chasNotConfirmedZadGroup";
                bandedGridSmenZadanyColumnChasRemainZad.FieldName = "chasRemainZad";
                bandedGridSmenZadanyColumnChasRemainZadGroup.FieldName = "chasRemainZadGroup";
                bandedGridSmenZadanyColumnShiftsRemainZad.FieldName = "shiftsRemainZad";
                bandedGridSmenZadanyColumnShiftsRemainZadGroup.FieldName = "shiftsRemainZadGroup";
                bandedGridSmenZadanyColumnChasNaznSmen.FieldName = "chasNaznSmen";
                bandedGridSmenZadanyColumnChasNaznSmenGroup.FieldName = "chasNaznSmenGroup";
                bandedGridSmenZadanyColumnChasNaznSmenProc.FieldName = "chasNaznSmenProc";
                bandedGridSmenZadanyColumnChasNaznSmenProcGroup.FieldName = "chasNaznSmenProcGroup";
                bandedGridSmenZadanyColumnChasInWorkSmen.FieldName = "chasInWorkSmen";
                bandedGridSmenZadanyColumnChasInWorkSmenGroup.FieldName = "chasInWorkSmenGroup";
                bandedGridSmenZadanyColumnChasDoneSmen.FieldName = "chasDoneSmen";
                bandedGridSmenZadanyColumnChasDoneSmenGroup.FieldName = "chasDoneSmenGroup";
                bandedGridSmenZadanyColumnChasDoneSmenProc.FieldName = "chasDoneSmenProc";
                bandedGridSmenZadanyColumnChasDoneSmenProcGroup.FieldName = "chasDoneSmenProcGroup";
                bandedGridSmenZadanyColumnChasRemainSmen.FieldName = "chasRemainSmen";
                bandedGridSmenZadanyColumnChasRemainSmenGroup.FieldName = "chasRemainSmenGroup";
                bandedGridSmenZadanyColumnChasConfirmedSmen.FieldName = "chasConfirmedSmen";
                bandedGridSmenZadanyColumnChasConfirmedSmenGroup.FieldName = "chasConfirmedSmenGroup";
                bandedGridSmenZadanyColumnTypeID.FieldName = "typeID";
                bandedGridSmenZadanyColumnTypeName.FieldName = "typeName";
                bandedGridSmenZadanyColumnFio.FieldName = "fio";
                bandedGridSmenZadanyColumnKwsID.FieldName = "kwsID";

                bandedGridSmenZadanyColumnKmaID.OptionsColumn.AllowEdit = false;
                bandedGridSmenZadanyColumnKmaNumber.OptionsColumn.AllowEdit = false;
                bandedGridSmenZadanyColumnKmlID.OptionsColumn.AllowEdit = false;
                bandedGridSmenZadanyColumnKmlNumber.OptionsColumn.AllowEdit = false;
                bandedGridSmenZadanyColumnIDVyazClass.OptionsColumn.AllowEdit = false;
                bandedGridSmenZadanyColumnNameVyazClass.OptionsColumn.AllowEdit = false;
                bandedGridSmenZadanyColumnSmenLength.OptionsColumn.AllowEdit = false;
                bandedGridSmenZadanyColumnChasNaznZad.OptionsColumn.AllowEdit = false;
                bandedGridSmenZadanyColumnChasNotConfirmedZad.OptionsColumn.AllowEdit = false;
                bandedGridSmenZadanyColumnChasRemainZad.OptionsColumn.AllowEdit = false;
                bandedGridSmenZadanyColumnShiftsRemainZad.OptionsColumn.AllowEdit = false;
                bandedGridSmenZadanyColumnChasNaznSmen.OptionsColumn.AllowEdit = false;
                bandedGridSmenZadanyColumnChasNaznSmenProc.OptionsColumn.AllowEdit = false;
                bandedGridSmenZadanyColumnChasInWorkSmen.OptionsColumn.AllowEdit = false;
                bandedGridSmenZadanyColumnChasDoneSmen.OptionsColumn.AllowEdit = false;
                bandedGridSmenZadanyColumnChasDoneSmenProc.OptionsColumn.AllowEdit = false;
                bandedGridSmenZadanyColumnChasRemainSmen.OptionsColumn.AllowEdit = false;
                bandedGridSmenZadanyColumnChasConfirmedSmen.OptionsColumn.AllowEdit = false;
                bandedGridSmenZadanyColumnTypeID.OptionsColumn.AllowEdit = false;
                bandedGridSmenZadanyColumnTypeName.OptionsColumn.AllowEdit = false;
                bandedGridSmenZadanyColumnFio.OptionsColumn.AllowEdit = false;
                bandedGridSmenZadanyColumnKwsID.OptionsColumn.AllowEdit = false;

                advBandedGridViewSmenZadany.OptionsView.ShowColumnHeaders = false;
                advBandedGridViewSmenZadany.OptionsView.ShowGroupPanel = false;
                bandedGridSmenZadanyColumnChasNaznZad.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //bandedGridSmenZadanyColumnChasNaznZad.DisplayFormat.FormatString = "#,0.00;-#,0.00;";
                bandedGridSmenZadanyColumnChasNaznZad.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
                bandedGridSmenZadanyColumnChasNotConfirmedZad.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                bandedGridSmenZadanyColumnChasNotConfirmedZad.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
                bandedGridSmenZadanyColumnChasRemainZad.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                bandedGridSmenZadanyColumnChasRemainZad.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
                bandedGridSmenZadanyColumnShiftsRemainZad.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                bandedGridSmenZadanyColumnShiftsRemainZad.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
                bandedGridSmenZadanyColumnChasNaznSmen.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                bandedGridSmenZadanyColumnChasNaznSmen.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
                bandedGridSmenZadanyColumnChasNaznSmenProc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                bandedGridSmenZadanyColumnChasNaznSmenProc.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
                bandedGridSmenZadanyColumnChasInWorkSmen.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                bandedGridSmenZadanyColumnChasInWorkSmen.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
                bandedGridSmenZadanyColumnChasDoneSmen.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                bandedGridSmenZadanyColumnChasDoneSmen.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
                bandedGridSmenZadanyColumnChasDoneSmenProc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                bandedGridSmenZadanyColumnChasDoneSmenProc.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
                bandedGridSmenZadanyColumnChasRemainSmen.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                bandedGridSmenZadanyColumnChasRemainSmen.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
                bandedGridSmenZadanyColumnChasConfirmedSmen.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                bandedGridSmenZadanyColumnChasConfirmedSmen.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";

                string _xNumFormat = "{0:0.00;-0.00;}";
                //string _xNumFormat = "{0:n2}";
                foreach (GridGroupSummaryItem gsi in advBandedGridViewSmenZadany.GroupSummary)
                {
                    gsi.DisplayFormat = _xNumFormat;
                }

                _gridHelper.AutoRowFilterConfig(advBandedGridViewSmenZadany, 0);
                advBandedGridViewSmenZadany.OptionsView.GroupFooterShowMode = GroupFooterShowMode.Hidden;
                _gridHelper.EnableGroupSummariesInGroupRow(advBandedGridViewSmenZadany, GroupSummaryLevelMode.IncludeOnly, new[] { 2 });
                #endregion

                _naryadZadanyVyazBindingSource = new BindingSource { DataSource = new BindingList<NaryadZadanyVyaz>() };
                #region gridControlNaryadZadany "наряд-задание"
                gridControlNaryadZadany.DataSource = _naryadZadanyVyazBindingSource;
                gridNaryadZadanyColumnKmlNumber.FieldName = "kmlNumber";
                gridNaryadZadanyColumnPzvArticul.FieldName = "pzvArticul";
                gridNaryadZadanyColumnPzvNomZad.FieldName = "pzvNomZad";
                gridNaryadZadanyColumnPzvNom.FieldName = "pzvNom";
                gridNaryadZadanyColumnNPach.FieldName = "nPach";
                gridNaryadZadanyColumnNomOper.FieldName = "nomOper";
                gridNaryadZadanyColumnText.FieldName = "text";
                gridNaryadZadanyColumnRazryd.FieldName = "razryd";
                gridNaryadZadanyColumnSekObServ.FieldName = "sekObServ";
                gridNaryadZadanyColumnHoursTotalPlan.FieldName = "hoursTotalPlan";
                gridNaryadZadanyColumnHoursTotalFact.FieldName = "hoursTotalFact";
                gridNaryadZadanyColumnPzvKol.FieldName = "pzvKol";
                gridNaryadZadanyColumnStatusName.FieldName = "statusName";
                gridNaryadZadanyColumnStatusDate.FieldName = "statusDate";
                gridNaryadZadanyColumnPzvTab.FieldName = "pzvTab";
                gridNaryadZadanyColumnYearPach.FieldName = "yearPach";
                gridNaryadZadanyColumnPachKod.FieldName = "pachKod";


                gridNaryadZadanyColumnKmlNumber.OptionsColumn.AllowEdit = false;
                gridNaryadZadanyColumnPzvArticul.OptionsColumn.AllowEdit = false;
                gridNaryadZadanyColumnPzvNomZad.OptionsColumn.AllowEdit = false;
                gridNaryadZadanyColumnPzvNom.OptionsColumn.AllowEdit = false;
                gridNaryadZadanyColumnNPach.OptionsColumn.AllowEdit = false;
                gridNaryadZadanyColumnNomOper.OptionsColumn.AllowEdit = false;
                gridNaryadZadanyColumnText.OptionsColumn.AllowEdit = false;
                gridNaryadZadanyColumnRazryd.OptionsColumn.AllowEdit = false;
                gridNaryadZadanyColumnSekObServ.OptionsColumn.AllowEdit = false;
                gridNaryadZadanyColumnHoursTotalPlan.OptionsColumn.AllowEdit = false;
                gridNaryadZadanyColumnHoursTotalFact.OptionsColumn.AllowEdit = false;
                gridNaryadZadanyColumnPzvKol.OptionsColumn.AllowEdit = false;
                gridNaryadZadanyColumnStatusName.OptionsColumn.AllowEdit = false;
                gridNaryadZadanyColumnStatusDate.OptionsColumn.AllowEdit = false;
                gridNaryadZadanyColumnPzvTab.OptionsColumn.AllowEdit = false;

                //advBandedGridViewSmenZadany.OptionsView.ShowColumnHeaders = false;
                gridViewNaryadZadany.OptionsView.ShowGroupPanel = false;
                gridNaryadZadanyColumnSekObServ.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //gridNaryadZadanyColumnSekObServ.DisplayFormat.FormatString = "#,0.00;-#,0.00;";
                gridNaryadZadanyColumnSekObServ.DisplayFormat.FormatString = "#,0.00;-#,0.00;";

                gridNaryadZadanyColumnHoursTotalPlan.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //gridNaryadZadanyColumnHoursTotalPlan.DisplayFormat.FormatString = "{0:0.00#;0:#;#}";
                //gridNaryadZadanyColumnHoursTotalPlan.DisplayFormat.FormatString = "#,0.00;-#,0.00;''";
                gridNaryadZadanyColumnHoursTotalPlan.DisplayFormat.FormatString = "#,0.00;-#,0.00;";

                gridNaryadZadanyColumnHoursTotalFact.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //gridNaryadZadanyColumnHoursTotalFact.DisplayFormat.FormatString = "{0:0.00#;0:#;#}";
                gridNaryadZadanyColumnHoursTotalFact.DisplayFormat.FormatString = "#,0.00;-#,0.00;";

                gridNaryadZadanyColumnStatusDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridNaryadZadanyColumnStatusDate.DisplayFormat.FormatString = "dd.MM.yy";

                _gridHelper.AutoRowFilterConfig(gridViewNaryadZadany, 0);

                //gridViewNaryadZadany.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
                #endregion

                #region описание gridControlArtNormN "заголовок РТ"
                gridControlArtNormN.DataSource = _artNormNBindingSource;
                gridArtNormNColumnAnnID.FieldName = "annID";
                gridArtNormNColumnGrup.FieldName = "grup";
                gridArtNormNColumnArticul.FieldName = "Articul";
                gridArtNormNColumnMod.FieldName = "Mod";
                gridArtNormNColumnDataObn.FieldName = "dateUpdate";
                gridArtNormNColumnAnnDateDel.FieldName = "dateDel";
                gridArtNormNColumnAnnCompDel.FieldName = "compDel";
                gridArtNormNColumnStatus.FieldName = "StatusText";
                //_gridHelper.AutoRowFilterConfig(gridViewArtNormN);
                #endregion

                #region описание gridControlNormRasz "строка по операции в РТ"
                gridControlNormRasz.DataSource = _normRaszBindingSource;
                gridNormRaszColumnNrID.FieldName = "nrID";
                gridNormRaszColumnDisplayNumber.FieldName = "DisplayNumber";
                gridNormRaszColumnText.FieldName = "Text";
                gridNormRaszColumnRazryd.FieldName = "razryd";
                gridNormRaszColumnSek.FieldName = "Sek";
                gridNormRaszColumnKodOb.FieldName = "KodOb";
                gridNormRaszColumnObor.FieldName = "TextOb";
                gridNormRaszColumnKodPodr.FieldName = "TextVyaz";
                gridNormRaszColumnKodProizv.FieldName = "TextProizv";
                gridNormRaszColumnNrDateDel.FieldName = "nrDateDel";
                gridNormRaszColumnNrCompDel.FieldName = "nrCompDel";
                //_gridHelper.AutoRowFilterConfig(gridViewNormRasz);
                #endregion

                #region описание gridControlMlOp "строка по операции в МЛ"
                gridControlMlOp.DataSource = _mlOpBindingSource;
                gridMlOpColumnID.FieldName = "id";
                gridMlOpColumnFromPzt.FieldName = "fromPzt";
                gridMlOpColumnDateAdd.FieldName = "date_add";
                gridMlOpColumnVidPr.FieldName = "vidPr";
                gridMlOpColumnNomR.FieldName = "nomR";
                gridMlOpColumnMgPach.FieldName = "mg_pach";
                gridMlOpColumnMgNez.FieldName = "mg_nez";
                gridMlOpColumnMaster.FieldName = "master";
                gridMlOpColumnOperationNumber.FieldName = "OperationNumber";
                gridMlOpColumnText.FieldName = "text";
                gridMlOpColumnKol.FieldName = "kol";
                gridMlOpColumnTab.FieldName = "tab";
                gridMlOpColumnFio.FieldName = "fio";
                gridMlOpColumnDataR.FieldName = "data_r";
                //_gridHelper.AutoRowFilterConfig(gridViewNormRasz);
                #endregion

                #region описание gridControlPZVOperList
                gridControlPZVOperList.DataSource = _pZVOperListByPachListBindingSource;
                gridColumnPZVOperListOlPzvID.FieldName = "olPzvID";
                gridColumnPZVOperListOlPzvIDParent.FieldName = "olPzvIDParent";
                gridColumnPZVOperListOlPzvDivision.FieldName = "olPzvDivision";
                gridColumnPZVOperListOlPzvIDMlOp.FieldName = "olPzvIDMlOp";
                gridColumnPZVOperListOlNom.FieldName = "olNom";
                gridColumnPZVOperListOlNomN.FieldName = "olNomN";
                gridColumnPZVOperListOlNomZad.FieldName = "olNomZad";
                gridColumnPZVOperListOlPzvAnnID.FieldName = "olPzvAnnID";
                gridColumnPZVOperListOlPzvNrID.FieldName = "olPzvNrID";
                gridColumnPZVOperListOlPzvIdBrig.FieldName = "olPzvIdBrig";
                gridColumnPZVOperListOlPzvKmlID.FieldName = "olPzvKmlID";
                gridColumnPZVOperListOlPzvArticul.FieldName = "olPzvArticul";
                gridColumnPZVOperListOlPzvMod.FieldName = "olPzvMod";
                gridColumnPZVOperListOlNPach.FieldName = "olNPach";
                //pach_kod
                //kod
                gridColumnPZVOperListOlNo.FieldName = "olNo";
                gridColumnPZVOperListOlNpo.FieldName = "olNpo";
                gridColumnPZVOperListOlNomOper.FieldName = "olNomOper";
                gridColumnPZVOperListOlOperName.FieldName = "olOperName";
                gridColumnPZVOperListOlKodOb.FieldName = "olKodOb";
                gridColumnPZVOperListOlOborudClass.FieldName = "olOborudClass";
                gridColumnPZVOperListOlRazryd.FieldName = "olRazryd";
                gridColumnPZVOperListOlSekEd.FieldName = "olSekEd";
                gridColumnPZVOperListOlKol.FieldName = "olKol";
                gridColumnPZVOperListOlPzvNChasi.FieldName = "olPzvNChasi";
                gridColumnPZVOperListOlKmlNumber.FieldName = "olKmlNumber";
                gridColumnPZVOperListOlPvDateNaznKm.FieldName = "olPzvDateNaznKm";
                gridColumnPZVOperListOlPzvTab.FieldName = "olPzvTab";
                gridColumnPZVOperListOlPzvDateNaznTab.FieldName = "olPzvDateNaznTab";
                gridColumnPZVOperListOlPzvDateStart.FieldName = "olPzvDateStart";
                gridColumnPZVOperListOlPzvDateEnd.FieldName = "olPzvDateEnd";
                gridColumnPZVOperListOlPzvDateML.FieldName = "olPzvDateML";
                gridColumnPZVOperListOlPzvDateMast.FieldName = "olPzvDateMast";
                gridColumnPZVOperListSyncSelection.FieldName = "SyncSelection";
                gridColumnPZVOperListOlPzvGradacia.FieldName = "olPzvGradacia";

                gridColumnPZVOperListOlPzvNChasi.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //gridColumnPZVOperListOlNChasi.DisplayFormat.FormatString = "#,0.00;-#,0.00;''";
                gridColumnPZVOperListOlPzvNChasi.DisplayFormat.FormatString = "#,0.00;-#,0.00;";
                gridColumnPZVOperListOlPvDateNaznKm.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridColumnPZVOperListOlPvDateNaznKm.DisplayFormat.FormatString = "dd.MM.yy";
                gridColumnPZVOperListOlPzvDateNaznTab.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridColumnPZVOperListOlPzvDateNaznTab.DisplayFormat.FormatString = "dd.MM.yy";
                gridColumnPZVOperListOlPzvDateStart.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridColumnPZVOperListOlPzvDateStart.DisplayFormat.FormatString = "dd.MM.yy";
                gridColumnPZVOperListOlPzvDateEnd.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridColumnPZVOperListOlPzvDateEnd.DisplayFormat.FormatString = "dd.MM.yy";
                gridColumnPZVOperListOlPzvDateML.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridColumnPZVOperListOlPzvDateML.DisplayFormat.FormatString = "dd.MM.yy";
                gridColumnPZVOperListOlPzvDateMast.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridColumnPZVOperListOlPzvDateMast.DisplayFormat.FormatString = "dd.MM.yy";

                //// Сначала сортируем по артикулу, далее пачка, номер операции/подоперации, ID родительской записи, ID записи
                //gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olPzvArticul"], ColumnSortOrder.Ascending));
                //gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olNPach"], ColumnSortOrder.Ascending));
                //gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olNo"], ColumnSortOrder.Ascending));
                //gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olNpo"], ColumnSortOrder.Ascending));
                //gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olPzvIDParent"], ColumnSortOrder.Ascending));
                //gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olPzvID"], ColumnSortOrder.Ascending));
                //gridViewPZVOperList.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;

                //_pZVOperListByPachListBindingSource.CurrentItemChanged += (_, __) => RecalcFromModel();
                //_pZVOperListByPachListBindingSource.PositionChanged += (_, __) => RecalcFromModel();
                //RecalcFromModel();

                gridColumnPZVOperListOlKmlNumber.OptionsColumn.AllowEdit = false;
                gridColumnPZVOperListOlPvDateNaznKm.OptionsColumn.AllowEdit = false;
                gridColumnPZVOperListOlPzvTab.OptionsColumn.AllowEdit = false;
                gridColumnPZVOperListOlPzvDateNaznTab.OptionsColumn.AllowEdit = false;
                gridColumnPZVOperListOlPzvDateStart.OptionsColumn.AllowEdit = false;
                gridColumnPZVOperListOlPzvDateEnd.OptionsColumn.AllowEdit = false;
                gridColumnPZVOperListOlPzvDateMast.OptionsColumn.AllowEdit = false;
                _gridHelper.AutoRowFilterConfig(gridViewPZVOperList as GridView, 0);
                gridViewPZVOperList.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
                gridColumnPZVOperListOlNPach.OptionsColumn.AllowSort = DefaultBoolean.False;

                gridViewPZVOperList.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
                gridColumnPZVOperListSyncSelection.OptionsColumn.AllowEdit = true;
                gridColumnPZVOperListSyncSelection.OptionsColumn.ReadOnly = false;

                //gridViewPZVOperList.FocusedRowChanged += (s, e) =>
                //{
                //    //Debug.WriteLine($"FocusedRowChanged: {e.FocusedRowHandle}");
                //    Debug.WriteLine($"FocusedRowChanged -> {e.FocusedRowHandle}, stack:\n{Environment.StackTrace}");
                //};

                #endregion

                #region описание блока Информация по операции
                textBoxOlPzvNom.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olNom), true, DataSourceUpdateMode.Never);
                textBoxOlPzvID.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvID), true, DataSourceUpdateMode.Never);
                textBoxOlPzvIDMlOp.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvIDMlOp), true, DataSourceUpdateMode.Never);
                textBoxOlPzvAnnID.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvAnnID), true, DataSourceUpdateMode.Never);
                textBoxOlPzvUpdDate.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvUpdDate), true, DataSourceUpdateMode.Never);
                //textBoxOlNo.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olNo), true, DataSourceUpdateMode.Never);
                //textBoxOlNpo.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olNpo), true, DataSourceUpdateMode.Never);
                textBoxOlNomOper.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olNomOper), true, DataSourceUpdateMode.Never);
                textBoxOlPzvNrID.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvNrID), true, DataSourceUpdateMode.Never);
                #endregion
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
        }

        #region грид GridNaryadZadany popupMenu init
        private void SetupGridNaryadZadanyEvents()
        {
            // Обработка клика правой кнопкой мыши через MouseDown
            gridControlNaryadZadany.MouseDown += GridControlNaryadZadany_MouseDown;
        }
        private void GridControlNaryadZadany_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Получаем информацию о месте клика
                GridHitInfo hit = gridViewNaryadZadany.CalcHitInfo(e.X, e.Y);
                string _columnName = hit.Column.FieldName;
                Debug.WriteLine($"FocusedColumn = '{hit.Column.Name}'");
                // Проверяем, что клик был в ячейке колонки NomZad
                if (hit.InRowCell && (hit.Column.FieldName == "pzvNomZad" /*|| hit.Column.FieldName == "pzvNom" || hit.Column.FieldName == "nPach"*/))
                {
                    // Получаем значение ячейки
                    object cellValue = gridViewNaryadZadany.GetRowCellValue(hit.RowHandle, hit.Column);

                    // Создаем и показываем контекстное меню
                    ShowContextMenuWithValue(cellValue, e.Location, hit);

                    // Предотвращаем дальнейшую обработку
                    // (опционально, если нужно отменить стандартное меню)
                }
            }
        }

        private void ShowContextMenuWithValue(object _cellValue, Point _location, GridHitInfo _hit)
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            // Формируем текст пункта меню
            string displayText = _cellValue?.ToString() ?? "(пусто)";

            // Пункт меню с отображением значения
            ToolStripMenuItem searchItem = new ToolStripMenuItem($"Поиск в НЗП по {_hit.Column.Caption}: {displayText}");
            searchItem.Tag = _cellValue;
            searchItem.Click += (s, args) =>
            {
                if (searchItem.Tag != null)
                {
                    switch (gridViewNaryadZadany.FocusedColumn.FieldName)
                    {
                        case "pzvNomZad":
                            textBoxPzvNomZadSearch.Text = string.Empty;
                            textBoxPzvNomZadSearch.Text = searchItem.Tag.ToString();
                            textBoxPzvNomSearch.Text = string.Empty;
                            textBoxPzvNomZadSearch.Focus();
                            textBoxPzvNomZadSearch.SelectAll();
                            break;
                        case "pzvNom":
                            //textBoxPzvNomZadSearch.Text = Convert.ToString(gridViewNaryadZadany.GetRowCellValue(_hit.RowHandle, gridNaryadZadanyColumnPzvNomZad));
                            textBoxPzvYearPachSearch.Text = Convert.ToString(gridViewNaryadZadany.GetRowCellValue(_hit.RowHandle, gridNaryadZadanyColumnYearPach));
                            textBoxPzvNomSearch.Text = searchItem.Tag.ToString();
                            //textBoxPzvNPachSearch.Text = string.Empty;
                            textBoxPzvNomSearch.Focus();
                            textBoxPzvNomSearch.SelectAll();
                            break;
                        case "nPach":
                            //textBoxPzvNomZadSearch.Text = Convert.ToString(gridViewNaryadZadany.GetRowCellValue(_hit.RowHandle, gridNaryadZadanyColumnPzvNomZad));
                            //textBoxPzvNomSearch.Text = Convert.ToString(gridViewNaryadZadany.GetRowCellValue(_hit.RowHandle, gridNaryadZadanyColumnPzvNom));
                            textBoxPzvYearPachSearch.Text = Convert.ToString(gridViewNaryadZadany.GetRowCellValue(_hit.RowHandle, gridNaryadZadanyColumnYearPach));
                            textBoxPzvNPachSearch.Text = searchItem.Tag.ToString();
                            textBoxPzvNPachSearch.Focus();
                            textBoxPzvNPachSearch.SelectAll();
                            break;
                    }

                }
            };

            // Пункт "Копировать"
            ToolStripMenuItem copyItem = new ToolStripMenuItem("Копировать");
            copyItem.Tag = _cellValue;
            copyItem.Click += (s, args) =>
            {
                if (copyItem.Tag != null)
                {
                    Clipboard.SetText(copyItem.Tag.ToString());
                }
            };

            menu.Items.Add(searchItem);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(copyItem);

            // Показываем меню
            menu.Show(gridControlNaryadZadany, _location);
        }
        #endregion
        private void SetGroupExpandState()
        {
            var view = advBandedGridViewSmenZadany;

            view.BeginUpdate();
            try
            {
                ProcessGroups(
                    view,
                    DevExpress.XtraGrid.GridControl.InvalidRowHandle
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка SetGroupExpandState: {ex.Message}");
            }
            finally
            {
                view.EndUpdate();
            }
        }

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

                    if (groupHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                        continue;

                    if (!view.IsGroupRow(groupHandle))
                        continue;

                    if (view.GetRowLevel(groupHandle) != 2)
                        continue;

                    string groupText = view.GetGroupRowValue(groupHandle)?.ToString();

                    view.SetRowExpanded(groupHandle, groupText == "м/ч");
                }
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка ProcessGroups: {ex.Message}");
            }
        }
        private void SyncSelectionUpdate()
        {
            try
            {
                // обязательно после BeginInvoke: BindingSource уже обновлён
                gridViewZadanyListByMachine.PostEditor();
                gridViewZadanyListByMachine.UpdateCurrentRow();

                var selectedRow = _zadanyListByMachineBindingSource.Current as ZadanyListByMachine;
                if (selectedRow == null)
                    return;

                int isSelected = Convert.ToInt32(
                    gridViewZadanyListByMachine.GetFocusedRowCellValue("SyncSelection"));

                var recordsToUpdate = _rzvPachListByNomBindingSource.List
                    .Cast<RzvPachListByNom>()
                    .Where(record =>
                        record != null &&
                        record.nomZad == selectedRow.pszNom &&
                        record.nom == selectedRow.nom)
                    .ToList();

                foreach (var record in recordsToUpdate)
                    record.SyncSelection = isSelected;

                _zadanyListByMachineBindingSource.ResetBindings(false);
                gridViewZadanyListByMachine.RefreshData();

                _rzvPachListByNomBindingSource.ResetBindings(false);
                gridViewRzvPachListByNom.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления: {ex.Message}");
            }
        }

        private async Task LoadPlanTotalHoursByKnitMachineDataAsync()
        {
            try
            {
                _planTotalHoursByKnitMachineBindingSource.Clear();
                _planTotalHoursByKnitMachineBindingSource.ResetBindings(false);

                planTotalHoursByKnitMachineData = await _vyazService.GetPlanTotalHoursByKnitMachine();

                if (planTotalHoursByKnitMachineData != null)
                {
                    await _logger.LogEventAsync($"Получены данные PlanTotalHoursByKnitMachine", "LoadPlanTotalHoursByKnitMachineDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _planTotalHoursByKnitMachineBindingSource.DataSource = planTotalHoursByKnitMachineData;
                    });

                    await _logger.LogEventAsync($"Данные PlanTotalHoursByKnitMachine успешно загружены", "LoadPlanTotalHoursByKnitMachineDataAsync");
                    //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
                    _planTotalHoursByKnitMachineBindingList.Add(planTotalHoursByKnitMachineData[0]);
                    _planTotalHoursByKnitMachineBindingSource.ResetBindings(false);
                    gridViewPlanTotalHoursByKnitMachine.ExpandAllGroups();
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные PlanTotalHoursByKnitMachine", "LoadPlanTotalHoursByKnitMachineDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных PlanTotalHoursByKnitMachine");
            }
        }
        private async Task LoadZadanyListByMachineNewDataAsync(int kmlID)
        {
            try
            {
                _zadanyListByMachineNewBindingSource.Clear();
                _zadanyListByMachineNewBindingSource.ResetBindings(false);

                zadanyListByMachineNewData = await _vyazService.GetZadanyListByMachine(kmlID);

                if (zadanyListByMachineNewData != null)
                {
                    await _logger.LogEventAsync($"Получены данные ZadanyListByMachine", "LoadZadanyListByMachineNewDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        gridControlZadanyListByMachine.BeginUpdate();
                        _zadanyListByMachineNewBindingSource.DataSource = zadanyListByMachineNewData;
                        gridControlZadanyListByMachine.EndUpdate();
                    });

                    await _logger.LogEventAsync($"Данные ZadanyListByMachine успешно загружены", "LoadZadanyListByMachineNewDataAsync");
                    _zadanyListByMachineNewBindingList.Add(zadanyListByMachineNewData[0]);
                    _zadanyListByMachineNewBindingSource.ResetBindings(false);
                    
                    gridViewZadanyListByMachine.BeginSort();
                    gridViewZadanyListByMachine.ClearSorting();
                    //gridViewZadanyListByMachine.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["nom"], ColumnSortOrder.Ascending));
                    gridViewZadanyListByMachine.SortInfo.Add(new GridColumnSortInfo(gridColumnZadanyListByMachineYearPlan, ColumnSortOrder.Ascending));
                    gridViewZadanyListByMachine.SortInfo.Add(new GridColumnSortInfo(gridColumnZadanyListByMachineNom, ColumnSortOrder.Ascending));
                    gridViewZadanyListByMachine.EndSort();
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные ZadanyListByMachine", "LoadZadanyListByMachineNewDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных ZadanyListByMachine");
            }
        }
        private async Task LoadRzvPachListByNomNewDataAsync(int nom, string nomZad)
        {
            try
            {
                _rzvPachListByNomNewBindingSource.Clear();
                _rzvPachListByNomNewBindingSource.ResetBindings(false);

                if (nom > 0)
                {
                    rzvPachListByNomNewData = await _vyazService.GetRzvPachListByNom(nom, nomZad);
                }
                else
                {
                    rzvPachListByNomNewData = null;
                }

                if (rzvPachListByNomNewData != null)
                {
                    await _logger.LogEventAsync($"Получены данные RzvPachListByNom", "LoadRzvPachListByNomNewDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _rzvPachListByNomNewBindingSource.DataSource = rzvPachListByNomNewData;
                    });

                    await _logger.LogEventAsync($"Данные RzvPachListByNom успешно загружены", "LoadRzvPachListByNomNewDataAsync");
                    _rzvPachListByNomNewBindingList.Add(rzvPachListByNomNewData[0]);
                    _rzvPachListByNomNewBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные RzvPachListByNom", "LoadRzvPachListByNomNewDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных RzvPachListByNom");
            }
        }
        private async Task LoadPZVOperListByPachListNewDataAsync(string pachList, int vyazPodrKod)
        {
            try
            {
                _pZVOperListByPachListNewBindingSource.Clear();
                _pZVOperListByPachListNewBindingSource.ResetBindings(false);

                if (pachList.Length > 0 && pachList != "[]" && pachList is not null)
                {
                    pZVOperListByPachListNewData = await _vyazService.GetPZVOperListByPachList(pachList, vyazPodrKod);
                }
                else
                {
                    pZVOperListByPachListNewData = null;
                }

                if (pZVOperListByPachListNewData != null)
                {
                    await _logger.LogEventAsync($"Получены данные PZVOperListByPachListNew", "LoadPZVOperListByPachListNewDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _pZVOperListByPachListNewBindingSource.DataSource = pZVOperListByPachListNewData;
                    });

                    await _logger.LogEventAsync($"Данные PZVOperListByPachList успешно загружены", "LoadPZVOperListByPachListNewDataAsync");
                    _pZVOperListByPachListNewBindingList.Add(pZVOperListByPachListNewData[0]);
                    _pZVOperListByPachListNewBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные PZVOperListByPachList", "LoadPZVOperListByPachListNewDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных PZVOperListByPachListNew");
            }
        }
        private Task SetStatusAsync(string text)
           => this.UI(() => labelStatus.Text = text);

        private Task SetLoadingAsync(bool isLoading)
            => this.UI(() =>
            {
                labelStatus.Text = isLoading ? "Загрузка…" : "Готово";
                Cursor = isLoading ? Cursors.WaitCursor : Cursors.Default;
            });

        private async Task LoadSmenZadanyVyazDataAsync()
        {
            await _loader.RunAsync(
                async token =>
                {
                    try
                    {
                        gridViewSmenZadany.ShowLoadingPanel();

                        await SetLoadingAsync(true);
                        int _xIDNazn = 6; // признак принаджелности зоны к Вязальному производству
                        int _xKodProizv = 1; // код производства 1 - вязальное производство
                        int _xKodPodr = 1; // код подразделения 1 - вязальное подразделение
                        var bs = await _vyazService.GetSmenZadanyVyaz(_xIDNazn, _xKodProizv, _xKodPodr, token);

                        await this.UI(() =>
                        {
                            gridControlSmenZadany.BeginUpdate();
                            _smenZadanyVyazBindingSource.DataSource = bs.DataSource;
                            Application.Idle -= ExpandGroupsOnIdle;
                            Application.Idle += ExpandGroupsOnIdle;
                            gridControlSmenZadany.EndUpdate();
                        });

                        await SetStatusAsync(bs.Count == 0 ? "Нет данных" : "Готово");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка LoadSmenZadanyVyazDataAsync: {ex.Message}");
                    }
                },
                onError: async ex =>
                {
                    await _logger.LogErrorAsync(ex, "Ошибка загрузки LoadSmenZadanyVyazDataAsync");
                    await SetStatusAsync("Ошибка загрузки");
                    await SetLoadingAsync(false);
                },
                onCanceled: async byLifetime =>
                {
                    if (!byLifetime)
                        await SetStatusAsync("Отменено");

                    await SetLoadingAsync(false);
                }
            );
            await SetLoadingAsync(false);
        }
        private async Task LoadSmenZadanyVyazNewDataAsync(int _xKodProizv)
        {
            try
            {
                await _loader.RunAsync(
                    async token =>
                    {
                        try
                        {
                            gridViewSmenZadany.ShowLoadingPanel();

                            await SetLoadingAsync(true);
                            int _xIDNazn = 6; // признак принаджелности зоны к Вязальному производству
                                              //int _xKodProizv = 1; // код производства 1 - вязальное производство
                            int _xKodPodr = 1; // код подразделения 1 - вязальное подразделение
                            var bs = await _vyazService.GetSmenZadanyVyaz(_xIDNazn, _xKodProizv, _xKodPodr, token);

                            await this.UI(() =>
                            {
                                gridControlSmenZadany.BeginUpdate();
                                _smenZadanyVyazNewBindingSource.DataSource = bs.DataSource;
                                Application.Idle -= ExpandGroupsOnIdle;
                                Application.Idle += ExpandGroupsOnIdle;
                                //----------------------
                                var changes = BindingSourceHelper.GetChanges<SmenZadanyVyaz>(
                                    _smenZadanyVyazBindingSource,
                                    _smenZadanyVyazNewBindingSource,
                                    HashMode.ExcludeOnly,
                                    keyProperties: new[] { "kwsKmaID", "kwsmlKmlID", "typeID" },
                                    hashProperties: new[] { "IsNew", "IsModified", "IsDeleted" }
                                );

                                BindingSourceHelper.ApplyChanges<SmenZadanyVyaz>(
                                    _smenZadanyVyazBindingSource,
                                    changes,
                                    UpdateFieldsMode.ExcludeOnly,
                                    keyProperties: new[] { "kwsKmaID", "kwsmlKmlID", "typeID" },
                                    gridViewPZVOperList,
                                    fields: new[] { "IsNew", "IsModified", "IsDeleted" }
                                );

                                BindingSourceHelper.RemoveMissingSmart<SmenZadanyVyaz>(
                                    _smenZadanyVyazBindingSource,
                                    changes.Removed,
                                    gridControlSmenZadany
                                );
                                //----------------------
                                Application.Idle -= ExpandGroupsOnIdle;
                                Application.Idle += ExpandGroupsOnIdle;
                                gridControlSmenZadany.EndUpdate();
                            });

                            await SetStatusAsync(bs.Count == 0 ? "Нет данных" : "Готово");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка LoadSmenZadanyVyazNewDataAsync (GetSmenZadanyVyaz + _smenZadanyVyazBindingSource update): {ex.Message}");
                        }
                    },
                    onError: async ex =>
                    {
                        await _logger.LogErrorAsync(ex, "Ошибка загрузки LoadSmenZadanyVyazNewDataAsync");
                        await SetStatusAsync("Ошибка загрузки");
                        await SetLoadingAsync(false);
                    },
                    onCanceled: async byLifetime =>
                    {
                        if (!byLifetime)
                            await SetStatusAsync("Отменено");

                        await SetLoadingAsync(false);
                    }
                );
                await SetLoadingAsync(false);
            }
            catch (Exception ex) { Debug.WriteLine($"LoadSmenZadanyVyazNewDataAsync - {ex.ToString()}"); }
        }
        private async Task LoadNaryadZadanyVyazDataAsync(int tab, int kmlID)
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
                    gridControlNaryadZadany.BeginUpdate();
                    gridViewNaryadZadany.ShowLoadingPanel();

                    _naryadZadanyVyazBindingSource.DataSource =
                        new BindingList<NaryadZadanyVyaz>();
                });

                // 3️ долгий запрос
                var bs = await _vyazService.GetNaryadZadanyVyaz(tab, kmlID, token);

                // если отменили — просто выходим
                if (token.IsCancellationRequested)
                    return;

                // 4️ привязываем результат
                await this.InvokeAsync(() =>
                {
                    _naryadZadanyVyazBindingSource.DataSource = bs.DataSource;
                });

                if (bs.Count == 0)
                {
                    await _logger.LogEventAsync(
                        "Данные NaryadZadanyVyaz не найдены",
                        nameof(LoadNaryadZadanyVyazDataAsync)
                    );
                }
                else
                {
                    await _logger.LogEventAsync(
                        "Данные NaryadZadanyVyaz успешно загружены",
                        nameof(LoadNaryadZadanyVyazDataAsync)
                    );
                }
            }
            catch (OperationCanceledException)
            {
                // молча — это нормальный сценарий
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка загрузки данных NaryadZadanyVyaz");
            }
            finally
            {
                await this.InvokeAsync(() =>
                {
                    gridViewNaryadZadany.HideLoadingPanel();
                    gridControlNaryadZadany.EndUpdate();
                    gridViewNaryadZadany.ExpandAllGroups();
                });
            }
        }


        private void ExpandGroupsOnIdle(object sender, EventArgs e)
        {
            Application.Idle -= ExpandGroupsOnIdle;

            // здесь группы уже ТОЧНО есть
            SetGroupExpandState_NoRecursion();
        }
        private async Task LoadArtNormNDataAsync(int _annID)
        {
            try
            {
                _artNormNBindingSource.Clear();
                _artNormNBindingSource.ResetBindings(false);

                var newArtNormNData = await _anService.GetArtNormDataById(_annID);
                artNormNData.Clear();
                artNormNData.Add(newArtNormNData);
                if (artNormNData != null)
                {
                    await _logger.LogEventAsync($"Получены данные ArtNormN", "LoadArtNormNDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _artNormNBindingSource.DataSource = artNormNData;
                    });

                    await _logger.LogEventAsync($"Данные ArtNormN успешно загружены", "LoadArtNormNDataAsync");
                    _artNormNBindingList.Add(artNormNData[0]);
                    _artNormNBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные ArtNormN", "LoadArtNormNDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных ArtNormN");
            }
        }
        private async Task LoadNormRaszDataAsync(int _nrID)
        {
            try
            {
                _normRaszBindingSource.Clear();
                _normRaszBindingSource.ResetBindings(false);

                var newNormRaszData = await _anService.GetRelatedNormRaszByID(_nrID);
                normRaszData.Clear();
                normRaszData.Add(newNormRaszData);
                if (normRaszData != null)
                {
                    await _logger.LogEventAsync($"Получены данные NormRasz", "LoadNormRaszDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _normRaszBindingSource.DataSource = newNormRaszData;
                    });

                    await _logger.LogEventAsync($"Данные NormRasz успешно загружены", "LoadNormRaszDataAsync");
                    _normRaszBindingList.Add(normRaszData[0]);
                    _normRaszBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные NormRasz", "LoadnormRaszDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных NormRasz");
            }
        }
        private async Task LoadArtNormNDataAsync(int annId, CancellationToken ct)
        {
            var data = await _anService.GetArtNormDataById(annId, ct); // добавь ct внутрь сервиса
            ct.ThrowIfCancellationRequested();

            await this.InvokeAsync(() =>
            {
                _artNormNBindingSource.DataSource = data == null
                    ? new List<ArtNormN>()
                    : new List<ArtNormN> { data };

                _artNormNBindingSource.ResetBindings(false);
            });
        }

        private async Task LoadNormRaszDataAsync(int nrId, CancellationToken ct)
        {
            var data = await _anService.GetRelatedNormRaszByID(nrId, ct);
            ct.ThrowIfCancellationRequested();

            await this.InvokeAsync(() =>
            {
                _normRaszBindingSource.DataSource = data == null
                    ? new List<NormRasz>()
                    : new List<NormRasz> { data };

                _normRaszBindingSource.ResetBindings(false);
            });
        }

        private async Task LoadMlOpDataAsync(int _nom, int _nrID)
        {
            try
            {
                _mlOpBindingSource.Clear();
                _mlOpBindingSource.ResetBindings(false);

                //artNormNData = await _vyazService.GetZadanyListByMachine(kmlID);
                mlOpData = await _dbService.GetListAsync<MlOp>($"select * from ml_op where nomr = {_nom} and nrID = {_nrID} ", new { });

                if (mlOpData != null)
                {
                    await _logger.LogEventAsync($"Получены данные MlOp", "LoadMlOpDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        //_currentMlOpData = mlOpData;                // Обновляем текущую модель
                        //_mlOpBindingSource.DataSource = _currentMlOpData; // Привязываем данные к форме
                        _mlOpBindingSource.DataSource = mlOpData;
                    });

                    await _logger.LogEventAsync($"Данные MlOp успешно загружены", "LoadMlOpDataAsync");
                    //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
                    _mlOpBindingList.Add(mlOpData[0]);
                    _mlOpBindingSource.ResetBindings(false);
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные MlOp", "LoadMlOpDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных MlOp");
            }
        }
        private async Task LoadKnitWorkingShiftSmenToMoveDataAsync(int _kmaID)
        {
            try
            {
                var sql = @$"
                            SELECT * 
                            FROM knitWorkingShiftNewCurrentSmen_view
                            WHERE kmaID <> {_kmaID}
                              AND dateShiftStart IS NOT NULL
                              AND dateShiftEnd IS NULL";

                var data = await _dbService.GetListAsync<KnitWorkingShiftSmen>(sql, null);

                _zonesCache = data ?? new List<KnitWorkingShiftSmen>();
                await _logger.LogEventAsync($"Данные KnitWorkingShiftSmenToMove успешно загружены", "LoadKnitWorkingShiftSmenToMoveDataAsync");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка загрузки зон");
                await _logger.LogEventAsync($"Не удалось найти данные KnitWorkingShiftSmenToMove", "LoadKnitWorkingShiftSmenToMoveDataAsync");
                _zonesCache = new();
            }
        }
        private async void layoutControlGroup6_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            try
            {
                int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);

                switch (buttonIndex)
                {
                    case 0:
                        //Debug.WriteLine(ButtonPreliminaryWd.Enabled + " " + ButtonPreliminaryWd.Visible);
                        MessageBox.Show("Просмотр работы к подтверждению");
                        break;
                    case 2:
                        //Debug.WriteLine(ButtonEditWd.Enabled + " " + ButtonEditWd.Visible);
                        MessageBox.Show("История по операции");
                        break;
                    case 4:
                        //Debug.WriteLine(customSimpleButton1.Enabled + " " + customSimpleButton1.Visible);
                        MessageBox.Show("Выгрузить операции в XLS");
                        break;
                    case 6:
                        //Debug.WriteLine(ButtonArchAndCopyWd.Enabled + " " + ButtonArchAndCopyWd.Visible);
                        //MessageBox.Show("Загрузить операции");
                        gridViewRzvPachListByNom.FocusedColumn = gridViewRzvPachListByNom.Columns["data_paln"];
                        gridViewRzvPachListByNom.FocusedColumn = gridViewRzvPachListByNom.Columns["SyncSelection"];
                        await GridOverlayLoader.RunTaskWithOverlayAsync(
                            gridControlPZVOperList,
                            LoadPlanZagrVyazByZadanySelection
                            , CancellationToken.None
                            );
                        break;
                    case 8:
                        await GridOverlayLoader.RunTaskWithOverlayAsync(
                            gridControlPZVOperList,
                            ClearSelectedPachList
                            , CancellationToken.None
                            );
                        break;
                    case 10:
                        _pZVOperListByPachListBindingSource.Clear();
                        gridViewRzvPachListByNom.FocusedColumn = gridViewRzvPachListByNom.Columns["data_paln"];
                        gridViewRzvPachListByNom.FocusedColumn = gridViewRzvPachListByNom.Columns["SyncSelection"];
                        await GridOverlayLoader.RunTaskWithOverlayAsync(
                            gridControlPZVOperList,
                            LoadPlanZagrVyazByZadanySelection
                            , CancellationToken.None
                            );
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обработки layoutControlGroup6_CustomButtonClick: {ex.Message}");
            }
        }

        public async Task LoadPlanZagrVyazByZadanySelection()
        {
            try
            {
                try { gridViewRzvPachListByNom.PostEditor(); }
                catch (SqlException ex)
                {
                   Debug.WriteLine(
                        $"SQL ERROR 1 {ex.Number}: {ex.Message}\n" +
                        $"Procedure: {ex.Procedure}\n" +
                        $"Line: {ex.LineNumber}"
                    );
                    throw;
                }//catch { }
                try { gridViewRzvPachListByNom.UpdateCurrentRow(); }
                catch (SqlException ex)
                {
                    Debug.WriteLine(
                         $"SQL ERROR 2 {ex.Number}: {ex.Message}\n" +
                         $"Procedure: {ex.Procedure}\n" +
                         $"Line: {ex.LineNumber}"
                     );
                    throw;
                }//catch { }
                
                try { _rzvPachListByNomBindingSource.EndEdit(); }
                catch (SqlException ex)
                {
                    Debug.WriteLine(
                         $"SQL ERROR 3 {ex.Number}: {ex.Message}\n" +
                         $"Procedure: {ex.Procedure}\n" +
                         $"Line: {ex.LineNumber}"
                     );
                    throw;
                }//catch { }
                 //catch { }
                try { _rzvPachListByNomBindingSource.CurrencyManager?.EndCurrentEdit(); }
                catch (SqlException ex)
                {
                    Debug.WriteLine(
                         $"SQL ERROR 4 {ex.Number}: {ex.Message}\n" +
                         $"Procedure: {ex.Procedure}\n" +
                         $"Line: {ex.LineNumber}"
                     );
                    throw;
                }//catch { }
                //catch { }

                // Фильтруем записи где syncSelection = 1
                var filteredRecords = _rzvPachListByNomBindingSource.Cast<object>()
                    .Where(item =>
                    {
                        var property = item.GetType().GetProperty("SyncSelection");
                        return property != null && Convert.ToInt32(property.GetValue(item)) == 1;
                    })
                    .ToList();

                // Преобразуем в JSON
                string jsonString = JsonConvert.SerializeObject(filteredRecords, Formatting.Indented);
                await LoadPZVOperListByPachListNewDataAsync(jsonString, vyazPodrKod);
                gridViewPZVOperList.BeginSort();
                gridViewPZVOperList.ClearSorting();
                // Сначала сортируем по артикулу, далее пачка, номер операции/подоперации, ID родительской записи, ID записи
                gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olPzvArticul"], ColumnSortOrder.Ascending));
                gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olNPach"], ColumnSortOrder.Ascending));
                gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olNo"], ColumnSortOrder.Ascending));
                gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olNpo"], ColumnSortOrder.Ascending));
                gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olPzvIDParent"], ColumnSortOrder.Ascending));
                gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olPzvID"], ColumnSortOrder.Ascending));

                gridViewPZVOperList.EndSort();

                var changes = BindingSourceHelper.GetChanges<PZVOperList>(
                    _pZVOperListByPachListBindingSource,
                    _pZVOperListByPachListNewBindingSource,
                    HashMode.ExcludeOnly,
                    keyProperties: new[] { "olPzvID" },
                    hashProperties: new[] { "IsNew", "IsModified", "IsDeleted", "SyncSelection", "ErrorSelection" }
                );

                BindingSourceHelper.ApplyChanges<PZVOperList>(
                    _pZVOperListByPachListBindingSource,
                    changes,
                    UpdateFieldsMode.ExcludeOnly,
                    keyProperties: new[] { "olPzvID" },
                    gridViewPZVOperList,
                    fields: new[] { "IsNew", "IsModified", "IsDeleted", "SyncSelection", "ErrorSelection" }
                );

                BindingSourceHelper.RemoveMissingSmart<PZVOperList>(
                    _pZVOperListByPachListBindingSource,
                    changes.Removed,
                    gridControlPZVOperList
                );
                //------------------------------------------------------

                gridViewPZVOperList.ExpandAllGroups();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных LoadPlanZagrVyazByZadanySelection: {ex.Message}");
            }
            //----------------------------------------------------
        }
        private async Task LoadPlanZagrVyazByZadanySelectionAsync()
        {
            try
            {
                // Фильтруем записи где syncSelection = 1
                var filteredRecords = _rzvPachListByNomBindingSource.Cast<object>()
                    .Where(item =>
                    {
                        var property = item.GetType().GetProperty("SyncSelection");
                        return property != null && Convert.ToInt32(property.GetValue(item)) == 1;
                    })
                    .ToList();

                // Преобразуем в JSON
                string jsonString = JsonConvert.SerializeObject(filteredRecords, Formatting.Indented);
                //MessageBox.Show(jsonString);

                await LoadPZVOperListByPachListNewDataAsync(jsonString, vyazPodrKod);
                // Task rzvLoad = LoadPZVOperListByPachListNewDataAsync(jsonString, vyazPodrKod);
                //  Task.WhenAll(rzvLoad);
                gridViewPZVOperList.BeginSort();
                gridViewPZVOperList.ClearSorting();
                // Сначала сортируем по артикулу, далее пачка, номер операции/подоперации, ID родительской записи, ID записи
                gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olPzvArticul"], ColumnSortOrder.Ascending));
                gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olNPach"], ColumnSortOrder.Ascending));
                gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olNo"], ColumnSortOrder.Ascending));
                gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olNpo"], ColumnSortOrder.Ascending));
                gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olPzvIDParent"], ColumnSortOrder.Ascending));
                gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olPzvID"], ColumnSortOrder.Ascending));

                gridViewPZVOperList.EndSort();

                // Приводим к правильному типу, без dynamic
                var oldList = _pZVOperListByPachListBindingSource.List?
                    .OfType<PZVOperList>()
                    .Where(x => x != null)
                    .ToList() ?? new List<PZVOperList>();

                var newList = _pZVOperListByPachListNewBindingSource.List?
                    .OfType<PZVOperList>()
                    .Where(x => x != null)
                    .ToList() ?? new List<PZVOperList>();

                // Создаем быстрый набор ID старого списка
                var oldIds = new HashSet<int>(oldList.Select(x => x.olPzvID));

                // Фильтруем новые записи БЕЗ цикла Any()
                var recordsToAdd = newList
                    .Where(newRec => !oldIds.Contains(newRec.olPzvID))
                    .ToList();

                _pZVOperListByPachListBindingSource.RaiseListChangedEvents = false;
                try
                {
                    foreach (var record in recordsToAdd)
                        _pZVOperListByPachListBindingSource.Add(record);
                }
                finally
                {
                    _pZVOperListByPachListBindingSource.RaiseListChangedEvents = true;
                    _pZVOperListByPachListBindingSource.ResetBindings(false);
                }

                //// Удаляем отсутствующие записи
                var recordsToRemove = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()              // вместо Cast<dynamic>()
                    .Where(oldRec => oldRec != null &&
                           !newList.Any(newRec => newRec != null &&
                                newRec.olPzvID == oldRec.olPzvID))
                    .Select(oldRec =>
                    {
                        oldRec.IsDeleted = true;   // или isDeleted, как у тебя называется
                        return oldRec;
                    })
                    .ToList();                          // <- здесь уже List<PzvOperList>

                //// Удаляем через временный список
                if (recordsToRemove.Count != 0)
                {
                    _pZVOperListByPachListBindingSource.RemoveDeleted<PZVOperList>();
                }

                //// Применяем фильтр
                //if (selectedRow != null)
                //{
                //    gridViewRzvPachListByNom.ActiveFilterString = $"nomZad == '{selectedRow.pszNom}' and nom == {selectedRow.nom}";
                //}

                _pZVOperListByPachListBindingSource.ResetBindings(false);
                gridViewPZVOperList.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных LoadPlanZagrVyazByZadanySelectionAsync: {ex.Message}");
            }
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

                _lifetimeCts = new CancellationTokenSource();
                _loadCts = new CancellationTokenSource();
                InitObjectRestartMap();

                Task bindingsTask = InitializeBindingsAsync();
                await Task.WhenAll(bindingsTask);

                await InitServiceBrokerAsync(_lifetimeCts.Token);
                await LoadPlanTotalHoursByKnitMachineDataAsync();
                await LoadSmenZadanyVyazDataAsync();
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
                var viewTop = (DevExpress.XtraGrid.Views.Grid.GridView)sender;

                if (!viewTop.IsDataRow(e.FocusedRowHandle))
                    return;

                var row = viewTop.GetRow(e.FocusedRowHandle) as PlanTotalHoursByKnitMachine;
                if (row == null) return;

                await ReloadZadanyForKmlAsync(row.kmlID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в gridViewPlanTotalHoursByKnitMachine_FocusedRowChanged: {ex.Message}");
            }
            //try
            //{
            //    var selectedRow = _planTotalHoursByKnitMachineBindingSource.Current as PlanTotalHoursByKnitMachine;
            //    await LoadZadanyListByMachineNewDataAsync(selectedRow.kmlID);

            //    var changes = BindingSourceHelper.GetChanges<ZadanyListByMachine>(
            //        _zadanyListByMachineBindingSource,
            //        _zadanyListByMachineNewBindingSource,
            //        HashMode.ExcludeOnly,
            //        keyProperties: new[] { "kmlID", "pszNom" },
            //        hashProperties: new[] { "SyncSelection" }
            //    );
            //    // обновить и добавить
            //    BindingSourceHelper.ApplyChanges<ZadanyListByMachine>(
            //        _zadanyListByMachineBindingSource,
            //        changes,
            //        UpdateFieldsMode.ExcludeOnly,
            //        keyProperties: new[] { "kmlID", "pszNom" },
            //        fields: new[] { "SyncSelection" }
            //    );

            //    gridViewZadanyListByMachine.ActiveFilterString = $" kmlID == {selectedRow.kmlID}";
            //    gridControlZadanyListByMachine.ForceInitialize();
            //    gridViewZadanyListByMachine.RefreshData();
            //    gridControlZadanyListByMachine.BeginInvoke(new Action(() =>
            //    {
            //        if (gridViewZadanyListByMachine.RowCount <= 0) return;

            //        // 1) Отслеживаем, сработало ли событие реально
            //        bool fired = false;
            //        DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler probe = (s, ee) => fired = true;
            //        gridViewZadanyListByMachine.FocusedRowChanged += probe;

            //        // 2) Пробуем "честно" сменить фокус так, чтобы событие точно могло сработать
            //        gridViewZadanyListByMachine.CloseEditor();
            //        gridViewZadanyListByMachine.UpdateCurrentRow();

            //        int oldHandle = gridViewZadanyListByMachine.FocusedRowHandle;
            //        int first = gridViewZadanyListByMachine.GetVisibleRowHandle(0);

            //        // если уже на первой и есть 2+ строк — уходим на вторую и возвращаемся
            //        if (oldHandle == first && gridViewZadanyListByMachine.RowCount > 1)
            //        {
            //            int second = gridViewZadanyListByMachine.GetVisibleRowHandle(1);
            //            gridViewZadanyListByMachine.FocusedRowHandle = second;
            //        }

            //        gridViewZadanyListByMachine.FocusedRowHandle = first;
            //        gridViewZadanyListByMachine.ClearSelection();
            //        gridViewZadanyListByMachine.SelectRow(first);

            //        // 3) Снимаем "пробник"
            //        gridViewZadanyListByMachine.FocusedRowChanged -= probe;

            //        // 4) Если DevExpress НЕ вызвал событие (например, осталась 1 строка) —
            //        //    честно гарантируем выполнение той же логики обновления 3-го грида
            //        if (!fired)
            //        {
            //            gridViewZadanyListByMachine_FocusedRowChanged(
            //                gridViewZadanyListByMachine,
            //                new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(oldHandle, first)
            //            );
            //        }
            //    }));
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Ошибка в gridViewPlanTotalHoursByKnitMachine_FocusedRowChanged: {ex.Message}");
            //}
        }
        private async Task ReloadZadanyForKmlAsync(int kmlId)
        {
            var view = gridViewZadanyListByMachine;

            view.BeginUpdate();
            try
            {
                // 1) грузим новые данные
                int version = ++_loadVersion;
                await LoadZadanyListByMachineNewDataAsync(kmlId);
                if (version != _loadVersion) return; // был новый запрос — этот выкидываем

                // 2) применяем изменения (как у тебя)
                var changes = BindingSourceHelper.GetChanges<ZadanyListByMachine>(
                    _zadanyListByMachineBindingSource,
                    _zadanyListByMachineNewBindingSource,
                    HashMode.ExcludeOnly,
                    keyProperties: new[] { "kmlID", "pszNom" },
                    hashProperties: new[] { "SyncSelection" }
                );

                BindingSourceHelper.ApplyChanges<ZadanyListByMachine>(
                    _zadanyListByMachineBindingSource,
                    changes,
                    UpdateFieldsMode.ExcludeOnly,
                    keyProperties: new[] { "kmlID", "pszNom" },
                    fields: new[] { "SyncSelection" }
                );

                // 3) фильтр
                view.ActiveFilterString = $"kmlID == {kmlId}";

                // 4) обновление данных грида
                view.RefreshData();
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
            gridControlZadanyListByMachine.BeginInvoke(new Action(() =>
            {
                if (_autoSelectPending)
                {
                    TryFocusPendingPszNom();
                    _autoSelectPending = false;
                }
                else
                {
                    EnsureFirstRowSelectedIfNothingSelected(gridViewZadanyListByMachine, _suppressZadanyFocusHandler);
                    //EnsureFirstRowSelectedIfNothingSelected(gridViewRzvPachListByNom, _suppressPachFocusHandler);
                    gridViewZadanyListByMachine_FocusedRowChanged(
                                gridViewZadanyListByMachine,
                                new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gridViewZadanyListByMachine.FocusedRowHandle)
                            );
                }
            }));
        }
        private void TryFocusPendingPszNom()
        {
            if (string.IsNullOrWhiteSpace(_pendingPszNom)) return;

            var view = gridViewZadanyListByMachine;
            var col = view.Columns.FirstOrDefault(c => c.FieldName == "pszNom");
            if (col == null) return;

            string search = NormalizeNom(_pendingPszNom);

            int rh = view.LocateByValue(0, col, search);
            if (!view.IsValidRowHandle(rh) || !view.IsDataRow(rh)) return;

            FocusRowWithScrollAndFire(view, rh, ref _suppressZadanyFocusHandler, forceFire: true);

            _pendingPszNom = null;
            _autoSelectPending = false;
            //if (!_autoSelectPending) return;
            //if (string.IsNullOrWhiteSpace(_pendingPszNom))
            //    return;

            //var view = gridViewZadanyListByMachine;
            //var col = view.Columns.FirstOrDefault(c => c.FieldName == "pszNom");
            //if (col == null) return;

            //string search = NormalizeNom(_pendingPszNom);

            //int rh = view.LocateByValue(0, col, search);

            //if (!view.IsValidRowHandle(rh) || !view.IsDataRow(rh))
            //    return;

            //_suppressZadanyFocusHandler = true;
            //view.BeginUpdate();
            //try
            //{
            //    //view.FocusedRowHandle = rh;
            //    //view.MakeRowVisible(rh);
            //    //view.ClearSelection();
            //    //view.SelectRow(rh);
            //    FocusAndScrollToRow(view, rh); // см. ниже
            //    view.ClearSelection();
            //    view.SelectRow(rh);
            //}
            //finally
            //{
            //    //view.EndUpdate();
            //    view.EndUpdate();
            //    _suppressZadanyFocusHandler = false;
            //}

            //_pendingPszNom = null;
        }
        private static string NormalizeNom(string? s)
        {
            return (s ?? "")
                .Replace('\u00A0', ' ')
                .Trim();
        }
        private void FocusFirstRow(GridView view)
        {
            try
            {
                view.CloseEditor();
                view.UpdateCurrentRow();

                if (view.RowCount <= 0) return;

                int first = view.GetVisibleRowHandle(0);
                view.FocusedRowHandle = first;

                view.ClearSelection();
                view.SelectRow(first);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка FocusFirstRow: {ex.Message}");
            }
        }
        private async Task RefreshRzvFromZadanyCurrentAsync()
        {
            try
            {
                var z = _zadanyListByMachineBindingSource.Current as ZadanyListByMachine;
                if (z == null) return;

                await LoadRzvPachListByNomNewDataAsync(z.nom, z.pszNom /* + остальные ключи, если нужны */);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка RefreshRzvFromZadanyCurrentAsync: {ex.Message}");
            }
        }
        private async void gridViewRzvPachListByNom_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
        }

        private async void gridViewZadanyListByMachine_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                var viewTop = (DevExpress.XtraGrid.Views.Grid.GridView)sender;

                if (!viewTop.IsDataRow(e.FocusedRowHandle))
                    return;

                var row = viewTop.GetRow(e.FocusedRowHandle) as ZadanyListByMachine;
                if (row == null) return;

                await ReloadPachListForZadanyAsync(row.nom, row.pszNom, row.Gradacia);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в gridViewZadanyListByMachine_FocusedRowChanged: {ex.Message}");
            }

            //try
            //{
            //    if (_suppressZadanyFocusHandler) return;
            //    if (_suppressZadanyFocusedChanged) return;

            //    var view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;

            //    // если ушли на group row / пустоту — чистим 3-й грид
            //    if (!view.IsDataRow(e.FocusedRowHandle))
            //    {
            //        int ver = ++_rzvLoadVersion;
            //        await LoadRzvPachListByNomNewDataAsync(0, "");
            //        if (ver != _rzvLoadVersion) return;

            //        gridViewRzvPachListByNom.ActiveFilterString = "1=0"; // ничего не показываем
            //        gridViewRzvPachListByNom.RefreshData();
            //        return;
            //    }

            //    // Берём строку именно из view (а не BindingSource.Current)
            //    var selectedRow = view.GetRow(e.FocusedRowHandle) as ZadanyListByMachine;
            //    if (selectedRow == null) return;

            //    // Снимаем значения сразу, чтобы после await не зависеть от selectedRow
            //    int nom = selectedRow.nom;
            //    //string nomZad = selectedRow.pszNom ?? "";
            //    string nomZad = (selectedRow.pszNom ?? "").Replace("'", "''");
            //    int grad = selectedRow.Gradacia;

            //    int myVersion = ++_rzvLoadVersion;

            //    await LoadRzvPachListByNomNewDataAsync(nom, nomZad);

            //    // если за время await фокус сменился и стартовала новая загрузка — выходим
            //    if (myVersion != _rzvLoadVersion) return;

            //    var changes = BindingSourceHelper.GetChanges<RzvPachListByNom>(
            //        _rzvPachListByNomBindingSource,
            //        _rzvPachListByNomNewBindingSource,
            //        HashMode.ExcludeOnly,
            //        keyProperties: new[] { "nomZad", "nom", "nom_n" },
            //        hashProperties: new[] { "SyncSelection" }
            //    );

            //    BindingSourceHelper.ApplyChanges<RzvPachListByNom>(
            //        _rzvPachListByNomBindingSource,
            //        changes,
            //        UpdateFieldsMode.ExcludeOnly,
            //        keyProperties: new[] { "nomZad", "nom", "nom_n" },
            //        fields: new[] { "SyncSelection" }
            //    );

            //    // read-only по градации
            //    gridColumnRzvPachListByNomGradacia.OptionsColumn.ReadOnly = (grad != 1);

            //    // Фильтр 3-го грида — используем сохранённые nomZad/nom
            //    gridViewRzvPachListByNom.ActiveFilterString = $"nomZad == '{nomZad}' and nom == {nom}";
            //    gridViewRzvPachListByNom.RefreshData();

            //    // если надо — чтобы прокрутка/фокус в 3-м гриде тоже “устаканились” после refresh
            //    // gridControlRzvPachListByNom.BeginInvoke(new Action(() => { ... }));
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Ошибка в gridViewZadanyListByMachine_FocusedRowChanged: {ex.Message}");
            //}

            ////try
            ////{
            ////    if (_suppressZadanyFocusHandler) return;
            ////    if (_suppressZadanyFocusedChanged) return;

            ////    var selectedRow = _zadanyListByMachineBindingSource.Current as ZadanyListByMachine;
            ////    if (selectedRow != null)
            ////    {
            ////        //MessageBox.Show(selectedRow.SyncSelection.ToString());
            ////        //MessageBox.Show(gridViewZadanyListByMachine.IsCellSelect.ToString());
            ////        await LoadRzvPachListByNomNewDataAsync(selectedRow.nom, selectedRow.pszNom);
            ////        var changes = BindingSourceHelper.GetChanges<RzvPachListByNom>(
            ////            _rzvPachListByNomBindingSource,
            ////            _rzvPachListByNomNewBindingSource,
            ////            HashMode.ExcludeOnly,
            ////            keyProperties: new[] { "nomZad", "nom", "nom_n" },
            ////            hashProperties: new[] { "SyncSelection" }
            ////        );
            ////        // обновить и добавить
            ////        BindingSourceHelper.ApplyChanges<RzvPachListByNom>(
            ////            _rzvPachListByNomBindingSource,
            ////            changes,
            ////            UpdateFieldsMode.ExcludeOnly,
            ////            keyProperties: new[] { "nomZad", "nom", "nom_n" },
            ////            fields: new[] { "SyncSelection" }
            ////        );
            ////        //// удалить отсутствующие

            ////        if (selectedRow.Gradacia != 1)
            ////        {
            ////            gridColumnRzvPachListByNomGradacia.OptionsColumn.ReadOnly = true;
            ////        }
            ////        else
            ////        {
            ////            gridColumnRzvPachListByNomGradacia.OptionsColumn.ReadOnly = false;
            ////        }
            ////    }
            ////    else
            ////    {
            ////        int myVersion = ++_rzvLoadVersion;
            ////        if (myVersion != _rzvLoadVersion) return;
            ////        await LoadRzvPachListByNomNewDataAsync(0, "");
            ////    }
            ////    gridViewRzvPachListByNom.ActiveFilterString = $" nomZad == '{selectedRow.pszNom}' and nom == {selectedRow.nom}";
            ////    gridControlRzvPachListByNom.ForceInitialize();
            ////    gridViewRzvPachListByNom.RefreshData();
            ////}
            ////catch (Exception ex)
            ////{
            ////    MessageBox.Show($"Ошибка в gridViewZadanyListByMachine_FocusedRowChanged: {ex.Message}");
            ////}

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
                var changes = BindingSourceHelper.GetChanges<RzvPachListByNom>(
                    _rzvPachListByNomBindingSource,
                    _rzvPachListByNomNewBindingSource,
                    HashMode.ExcludeOnly,
                    keyProperties: new[] { "nomZad", "nom", "nom_n" },
                    hashProperties: new[] { "SyncSelection" }
                );

                BindingSourceHelper.ApplyChanges<RzvPachListByNom>(
                    _rzvPachListByNomBindingSource,
                    changes,
                    UpdateFieldsMode.ExcludeOnly,
                    keyProperties: new[] { "nomZad", "nom", "nom_n" },
                    fields: new[] { "SyncSelection" }
                );

                // 3) фильтр + запрет редактирования поля Градация
                gridColumnRzvPachListByNomGradacia.OptionsColumn.ReadOnly = (_gradacia != 1);

                // Фильтр 3-го грида — используем сохранённые nomZad/nom
                view.ActiveFilterString = $"nomZad == '{_nomZad}' and nom == {_nom}";

                // 4) обновление данных грида
                view.RefreshData();
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
                FocusAndScrollToRow(_view, first);
                _view.ClearSelection();
                _view.SelectRow(first);
            }
            finally
            {
                _handler = false;
            }
        }

        private static void FocusAndScrollToRow(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            view.FocusedRowHandle = rowHandle;

            // 1) гарантируем, что строка станет видимой
            view.MakeRowVisible(rowHandle);

            // 2) аккуратно прокручиваем вверх так, чтобы строка была не в самом низу
            int visibleIndex = view.GetVisibleIndex(rowHandle);
            if (visibleIndex < 0) return;

            int rowsOnScreen = Math.Max(1, view.GridControl.Height / view.RowHeight);
            int targetTop = Math.Max(0, visibleIndex - rowsOnScreen / 2);

            view.TopRowIndex = targetTop;
        }
        private static int GetFirstDataRowHandle(GridView view)
        {
            for (int i = 0; i < view.RowCount; i++)
            {
                int rh = view.GetVisibleRowHandle(i);
                if (view.IsDataRow(rh)) return rh;
            }
            return GridControl.InvalidRowHandle;
        }

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

                FocusAndScrollToRow(view, rowHandle);
                view.ClearSelection();
                view.SelectRow(rowHandle);
            }
            finally
            {
                view.EndUpdate();
                suppressFlag = false;
            }
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
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
                var recordsToUpdate = _zadanyListByMachineBindingSource.List
                    .Cast<ZadanyListByMachine>()
                    .Where(record => record != null &&
                           record.SyncSelection == 1)
                    .ToList();
                foreach (var record in recordsToUpdate)
                {
                    record.SyncSelection = 0;
                }
                gridViewZadanyListByMachine.PostEditor();
                gridViewZadanyListByMachine.UpdateCurrentRow();
                _zadanyListByMachineBindingSource.ResetBindings(false);
                gridViewZadanyListByMachine.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления списка заданий ClearSelectedPachList: {ex.Message}");
            }

            //LoadPlanZagrVyazByZadanySelection();
            _pZVOperListByPachListNewBindingSource.Clear();
            var changes = BindingSourceHelper.GetChanges<PZVOperList>(
                    _pZVOperListByPachListBindingSource,
                    _pZVOperListByPachListNewBindingSource,
                    HashMode.ExcludeOnly,
                    keyProperties: new[] { "olPzvID" },
                    hashProperties: new[] { "IsNew", "IsModified", "IsDeleted", "SyncSelection", "ErrorSelection" }
                );
            // удалить отсутствующие
            //BindingSourceHelper.RemoveMissing(
            //    _pZVOperListByPachListBindingSource,
            //    changes.Removed
            //);
            var metrics = BindingSourceHelper.RemoveMissingSmart<PZVOperList>(
                _pZVOperListByPachListBindingSource,
                changes.Removed,
                gridControlPZVOperList
            );

            _logger.LogEventAsync(metrics.ToString());

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

        private async void customTabControl1_CustomHeaderButtonClick(object sender, DevExpress.XtraTab.ViewInfo.CustomHeaderButtonEventArgs e)
        {
        }

        /// <summary>
        /// Проверка заполненности дат
        /// </summary>
        /// <param name="_dateName"></param>
        /// <param name="_dateValue"></param>
        /// <param name="_xMessage"></param>
        /// <returns></returns>
        private bool CheckPZVDate(string _dateName, DateTime _dateValue, string _xMessage)
        {
            try
            {
                string _detailMessage = string.Empty;
                switch (_dateName)
                {
                    case "OlPvDateNaznKm":  // дата назначения машины
                        _detailMessage = "Операция уже назначена на машину";
                        break;
                    case "OlPvDateNaznTab": // дата назначения работника
                        _detailMessage = "Операция уже назначена работнику";
                        break;
                    case "OlPvDateStart":   // дата начала работы
                        _detailMessage = "Работник уже начал выполнять операцию";
                        break;
                    case "OlPvDateEnd":     // дата окончания работы
                        _detailMessage = "Работник уже выполнил операцию";
                        break;
                    case "OlPvDateMast":    // дата подтверждения мастером
                        _detailMessage = "Операция уже подтверждена мастером";
                        break;
                    default:
                        MessageBox.Show("Неизвестная дата");
                        return false;
                }
                if (_dateValue != null && _dateValue != DateTime.MinValue)
                {
                    MessageBox.Show($"{_detailMessage}, {_xMessage}!");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в CheckPZVDate: {ex.Message}");
                return false;

            }
        }
        /// <summary>
        /// Проверка пустых дат
        /// </summary>
        /// <param name="_dateName"></param>
        /// <param name="_dateValue"></param>
        /// <param name="_xMessage"></param>
        /// <returns></returns>
        private bool CheckPZVEmptyDate(string _dateName, DateTime _dateValue, string _xMessage)
        {
            try
            {
                string _detailMessage = string.Empty;
                switch (_dateName)
                {
                    case "OlPvDateNaznKm":  // дата назначения машины
                        _detailMessage = "Операция еще не назначена на машину";
                        break;
                    case "OlPvDateNaznTab": // дата назначения работника
                        _detailMessage = "Операция еще не назначена работнику";
                        break;
                    case "OlPvDateStart":   // дата начала работы
                        _detailMessage = "Работник еще не начал выполнять операцию";
                        break;
                    case "OlPvDateEnd":     // дата окончания работы
                        _detailMessage = "Работник еще выполнил операцию";
                        break;
                    case "OlPvDateMast":    // дата подтверждения мастером
                        _detailMessage = "Операция уже подтверждена мастером";
                        break;
                    default:
                        MessageBox.Show("Неизвестная дата");
                        return false;
                }
                if (_dateValue == null || _dateValue == DateTime.MinValue)
                {
                    MessageBox.Show($"{_detailMessage}, {_xMessage}!");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в CheckPZVEmptyDate: {ex.Message}");
                return false;

            }
        }
        private void gridViewPZVOperList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var view = sender as GridView;
                if (view == null) return;

                Point pt = view.GridControl.PointToClient(Control.MousePosition);
                GridHitInfo hit = view.CalcHitInfo(pt);
                //string _xColumn = view.FocusedColumn.ToString();
                _xColumn = view.FocusedColumn.FieldName;
                _xPzvID = Convert.ToInt32(view.GetRowCellValue(hit.RowHandle, gridColumnPZVOperListOlPzvID));
                int tab = Convert.ToInt32(view.GetRowCellValue(hit.RowHandle, gridColumnPZVOperListOlPzvTab));
                DateTime _OlPvDateNaznKm = Convert.ToDateTime(view.GetRowCellValue(hit.RowHandle, gridColumnPZVOperListOlPvDateNaznKm));
                DateTime _OlPvDateNaznTab = Convert.ToDateTime(view.GetRowCellValue(hit.RowHandle, gridColumnPZVOperListOlPzvDateNaznTab));
                DateTime _OlPvDateStart = Convert.ToDateTime(view.GetRowCellValue(hit.RowHandle, gridColumnPZVOperListOlPzvDateStart));
                DateTime _OlPvDateEnd = Convert.ToDateTime(view.GetRowCellValue(hit.RowHandle, gridColumnPZVOperListOlPzvDateEnd));
                DateTime _OlPvDateMast = Convert.ToDateTime(view.GetRowCellValue(hit.RowHandle, gridColumnPZVOperListOlPzvDateMast));
                int _olNpach = Convert.ToInt32(view.GetRowCellValue(hit.RowHandle, gridColumnPZVOperListOlNPach));
                string _olNomOper = Convert.ToString(view.GetRowCellValue(hit.RowHandle, gridColumnPZVOperListOlNomOper));
                string _olOperName = Convert.ToString(view.GetRowCellValue(hit.RowHandle, gridColumnPZVOperListOlOperName));
                if (hit.InRowCell && (hit.Column == gridColumnPZVOperListOlKmlNumber || hit.Column == gridColumnPZVOperListOlPvDateNaznKm) && hit.RowHandle >= 0)
                {
                    var pzvOperList = _pZVOperListByPachListBindingSource.Current as PZVOperList;
                    if (pzvOperList == null) return;
                    pzvOperList.SyncSelection = 1;
                    if (pzvOperList.olPzvDateNaznKm == null)
                    {
                        KnittingMachineWorkAssignment();
                    }
                    else
                    {
                        KnittingMachineCancelWorkAssignment();
                    }
                }

                if (hit.InRowCell && (hit.Column == gridColumnPZVOperListOlPzvTab || hit.Column == gridColumnPZVOperListOlPzvDateNaznTab) && hit.RowHandle >= 0)
                {
                    var pzvOperList = _pZVOperListByPachListBindingSource.Current as PZVOperList;
                    if (pzvOperList == null) return;
                    pzvOperList.SyncSelection = 1;
                    if (pzvOperList.olPzvDateNaznTab == null)
                    {
                        TabWorkAssignment();
                    }
                    else
                    {
                        TabCancelWorkAssignment();
                    }
                }

                if (hit.InRowCell && hit.Column == gridColumnPZVOperListOlPzvDateStart && hit.RowHandle >= 0)
                {
                    var pzvOperList = _pZVOperListByPachListBindingSource.Current as PZVOperList;
                    if (pzvOperList == null) return;
                    pzvOperList.SyncSelection = 1;
                    if (pzvOperList.olPzvDateStart == null)
                    {
                        WorkStartExecution();
                    }
                    else
                    {
                        CancelWorkStartExecution();
                    }
                }

                if (hit.InRowCell && hit.Column == gridColumnPZVOperListOlPzvDateEnd && hit.RowHandle >= 0)
                {
                    var pzvOperList = _pZVOperListByPachListBindingSource.Current as PZVOperList;
                    if (pzvOperList == null) return;
                    pzvOperList.SyncSelection = 1;
                    if (pzvOperList.olPzvDateEnd == null)
                    {
                        WorkStopExecution();
                    }
                    else
                    {
                        CancelWorkStopExecution();
                    }
                }

                if (hit.InRowCell && hit.Column == gridColumnPZVOperListOlPzvDateMast && hit.RowHandle >= 0)
                {
                    var pzvOperList = _pZVOperListByPachListBindingSource.Current as PZVOperList;
                    if (pzvOperList == null) return;
                    pzvOperList.SyncSelection = 1;
                    if (pzvOperList.olPzvDateMast == null)
                    {
                        MasterConfirmation();
                    }
                    else
                    {
                        MasterCancelConfirmation();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в gridViewPZVOperList_DoubleClick: {ex.Message}");
            }
        }
        private void GoToPzvID(int _pzvID, string _column)
        {
            try
            {
                //int rowHandle = gridViewPZVOperList.LocateByValue("olPzvID", _pzvID);
                //GridColumn col = gridViewPZVOperList.Columns.ColumnByFieldName("olPzvID");
                //MessageBox.Show($"{col}");
                int rowHandle = gridViewPZVOperList.LocateByValue("olPzvID", _pzvID);
                //int rowHandle = gridViewPZVOperList.FocusedRowHandle;
                //int rowHandle = gridViewPZVOperList.LocateByValue("gridColumnPZVOperListOlPzvID", _pzvID);

                if (rowHandle >= 0)
                {
                    gridViewPZVOperList.MakeRowVisible(rowHandle);
                    gridViewPZVOperList.FocusedRowHandle = rowHandle;
                    gridViewPZVOperList.FocusedColumn = gridViewPZVOperList.Columns[$"{_column}"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в GoToPzvID: {ex.Message}");
            }
        }
        public void SetDateStartToPzvID(List<int> _pzvId, DateTime? _dateStart)
        {
            //MessageBox.Show($"Двойной клик по операции. В/М {curr.kmlNumber} ({curr.kmlInvNum}) - Зона {curr.kmaNumber}");
            try
            {
                // получаем список строк (pzvID) для присовения машины (kmlID)
                var idSet = _pzvId != null ? new HashSet<int>(_pzvId) : new HashSet<int>();

                var recordsToUpdate = _pZVOperListByPachListBindingSource.List
                    .Cast<PZVOperList>()
                    .Where(r => r != null && idSet.Contains(r.olPzvID))
                    .ToList();
                foreach (var record in recordsToUpdate)
                {
                    record.olPzvDateStart = _dateStart;
                    record.IsModified = true;
                }
                // Получаем список строк с флагом IsModified = true
                List<PZV> filteredList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x?.IsModified == true)
                    .Select(x => x.ToPZV())   // новый маппер
                    .ToList();
                // Преобразуем в BindingList
                if (filteredList.Count > 0)
                {
                    using (SqlConnection connection = _dbHelper.GetConnection())
                    {
                        _bulkHelper.BulkAllDataUpdate<PZV>(connection, filteredList, "planZagrVyaz", new[] { "pzvID" });
                        // удалить из BindingSource
                        //_pZVOperListByPachListBindingSource.RemoveModified<PZVOperList>();
                        foreach (var record in recordsToUpdate)
                        {
                            record.IsModified = false;
                            record.SyncSelection = 0;
                        }
                        LoadPlanZagrVyazByZadanySelection();
                    }
                }
                _pZVOperListByPachListBindingSource.ResetBindings(false);
                gridViewPZVOperList.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка заполнения даты начала вязания SetDateStartToPzvID: {ex.Message}");
                throw;
            }

        }
        private void SetDateEndToPzvID(List<int> _pzvId, DateTime? _date)
        {
            //MessageBox.Show($"Двойной клик по операции. В/М {curr.kmlNumber} ({curr.kmlInvNum}) - Зона {curr.kmaNumber}");
            try
            {
                // получаем список строк (pzvID) для присовения машины (kmlID)
                var idSet = _pzvId != null ? new HashSet<int>(_pzvId) : new HashSet<int>();

                var recordsToUpdate = _pZVOperListByPachListBindingSource.List
                    .Cast<PZVOperList>()
                    .Where(r => r != null && idSet.Contains(r.olPzvID))
                    .ToList();
                foreach (var record in recordsToUpdate)
                {
                    record.olPzvDateEnd = _date;
                    record.IsModified = true;
                }
                // Получаем список строк с флагом IsModified = true
                List<PZV> filteredList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x?.IsModified == true)
                    .Select(x => x.ToPZV())   // новый маппер
                    .ToList();
                // Преобразуем в BindingList
                if (filteredList.Count > 0)
                {
                    using (SqlConnection connection = _dbHelper.GetConnection())
                    {
                        _bulkHelper.BulkAllDataUpdate<PZV>(connection, filteredList, "planZagrVyaz", new[] { "pzvID" });
                        // удалить из BindingSource
                        foreach (var record in recordsToUpdate)
                        {
                            record.IsModified = false;
                            record.SyncSelection = 0;
                        }
                        LoadPlanZagrVyazByZadanySelection();
                    }
                }
                _pZVOperListByPachListBindingSource.ResetBindings(false);
                gridViewPZVOperList.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка заполнения даты окончания вязания SetDateEndToPzvID: {ex.Message}");
                throw;
            }

        }

        private void SetDateMastToPzvID(List<int> _pzvId, DateTime? _date)
        {
            //MessageBox.Show($"Двойной клик по операции. В/М {curr.kmlNumber} ({curr.kmlInvNum}) - Зона {curr.kmaNumber}");
            try
            {
                // получаем список строк (pzvID) для присовения машины (kmlID)
                var idSet = _pzvId != null ? new HashSet<int>(_pzvId) : new HashSet<int>();

                var recordsToUpdate = _pZVOperListByPachListBindingSource.List
                    .Cast<PZVOperList>()
                    .Where(r => r != null && idSet.Contains(r.olPzvID))
                    .ToList();
                foreach (var record in recordsToUpdate)
                {
                    record.olPzvDateMast = _date;
                    record.IsModified = true;
                }
                // Получаем список строк с флагом IsModified = true
                List<PZV> filteredList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x?.IsModified == true)
                    .Select(x => x.ToPZV())   // новый маппер
                    .ToList();
                // Преобразуем в BindingList
                if (filteredList.Count > 0)
                {
                    using (SqlConnection connection = _dbHelper.GetConnection())
                    {
                        _bulkHelper.BulkAllDataUpdate<PZV>(connection, filteredList, "planZagrVyaz", new[] { "pzvID" });
                        // удалить из BindingSource
                        foreach (var record in recordsToUpdate)
                        {
                            record.IsModified = false;
                            record.SyncSelection = 0;
                        }
                        LoadPlanZagrVyazByZadanySelection();
                    }
                }
                _pZVOperListByPachListBindingSource.ResetBindings(false);
                gridViewPZVOperList.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка заполнения даты подтверждения мастером SetDateMastToPzvID: {ex.Message}");
                throw;
            }
        }

        private void SetKnitMachineToPzvID(List<int> _pzvId, int _kmlID)
        {
            //MessageBox.Show($"Двойной клик по операции. В/М {curr.kmlNumber} ({curr.kmlInvNum}) - Зона {curr.kmaNumber}");
            try
            {
                // получаем список строк (pzvID) для присовения машины (kmlID)
                var idSet = _pzvId != null ? new HashSet<int>(_pzvId) : new HashSet<int>();

                var recordsToUpdate = _pZVOperListByPachListBindingSource.List
                    .Cast<PZVOperList>()
                    .Where(r => r != null && idSet.Contains(r.olPzvID))
                    .ToList();
                foreach (var record in recordsToUpdate)
                {
                    record.olPzvKmlID = _kmlID;
                    record.olPzvDateNaznKm = _kmlID == 0 ? null : DateTime.Now;
                    record.IsModified = true;
                }
                // Получаем список строк с флагом IsModified = true
                List<PZV> filteredList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x?.IsModified == true)
                    .Select(x => x.ToPZV())   // новый маппер
                    .ToList();
                // Преобразуем в BindingList
                if (filteredList.Count > 0)
                {
                    using (SqlConnection connection = _dbHelper.GetConnection())
                    {
                        _bulkHelper.BulkAllDataUpdate<PZV>(connection, filteredList, "planZagrVyaz", new[] { "pzvID" });

                        // удалить из BindingSource
                        //_pZVOperListByPachListBindingSource.RemoveModified<PZVOperList>();
                        foreach (var record in recordsToUpdate)
                        {
                            record.IsModified = false;
                            record.SyncSelection = 0;
                        }
                        LoadPlanZagrVyazByZadanySelection();
                    }
                }
                _pZVOperListByPachListBindingSource.ResetBindings(false);
                gridViewPZVOperList.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка присвоения машины SetKnitMachineToPzvID: {ex.Message}");
                throw;
            }

        }

        private void gridViewPZVOperList_RowCellClick(object sender, RowCellClickEventArgs e)
        {
        }

        /// <summary>
        /// Назначить на В/М по V для выбранных операций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KnittingMachineWorkAssignment()
        {
            try
            {
                //SmenZadanyVyaz curr = _smenZadanyVyazBindingSource.Current as SmenZadanyVyaz;
                SmenZadanyVyaz curr = advBandedGridViewSmenZadany.GetRow(advBandedGridViewSmenZadany.FocusedRowHandle) as SmenZadanyVyaz  ;
                if (curr == null || curr.kwsmlKmlID == null || curr.kwsmlKmlID == 0)
                {
                    MessageBox.Show("Не выбрана машина для назначения");
                    return;
                }
                var checkList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .ToList();
                if (checkList.Count != 0)
                {
                    foreach (var record in checkList)
                    {
                        if (record.olKodPodr != 1)
                        {
                            MessageBox.Show("Операция не относится к вязальному подразделению, нельзя изменять В/М!");
                            return;
                        }
                        //int _kmlID = _dbHelper.ExecuteScalar($"SELECT pszkmKmlID " +
                        //    $"FROM plan_sezon_zad_knitMachine pszm " +
                        //    $"WHERE pszm.pszkmPszNom = '{record.olNomZad}' and pszkmKnitClass = {record.olIdVyazClass}");
                        string query = @"SELECT pszm.pszkmKmlID, mlv.kmlNumber, mlv.name_class " +
                            @"FROM plan_sezon_zad_knitMachine pszm " +
                            @"  LEFT JOIN knitMachineList_view mlv ON pszm.pszkmKmlID = mlv.kmlID " +
                            @"WHERE pszm.pszkmPszNom = @PszNom and pszkmKnitClass = @KnitClass";
                        DataTable machineInfo = _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@PszNom", record.olNomZad }, { "@KnitClass", record.olIdVyazClass } });

                        int _kmlID = 0;
                        string _kmlNumber = string.Empty;
                        int _vyazClass = 0;
                        if (machineInfo.Rows.Count == 0)
                        {
                            _kmlID = 0;
                            _kmlNumber = string.Empty;
                            _vyazClass = 0;
                        }
                        else
                        {
                            _kmlID = Convert.ToInt32(machineInfo.Rows[0]["pszkmKmlID"]);
                            _kmlNumber = Convert.ToString(machineInfo.Rows[0]["kmlNumber"]);
                            _vyazClass = Convert.ToInt32(machineInfo.Rows[0]["name_class"]);
                        }

                        //int _kmlID = Convert.ToInt32(machineInfo.Rows[0]["pszkmKmlID"]);
                        //string _kmlNumber = Convert.ToString(machineInfo.Rows[0]["kmlNumber"]);
                        //int _vyazClass = Convert.ToInt32(machineInfo.Rows[0]["name_class"]);

                        if (_kmlID != curr.kwsmlKmlID && record.olPzvKmlID == 0)
                        {
                            //var _result = MessageBox.Show(
                            //    $"Внимание! Операция {record.olNomOper} {record.olOperName} " +
                            //    $"\n Назначаемая машина ({curr.kmlNumber}) не совпадает с плановой ({_kmlNumber}). Продолжить?",
                            //    "",
                            //    MessageBoxButtons.YesNo,
                            //    MessageBoxIcon.Warning);
                            //if (_result == DialogResult.No)
                            //{
                            //    return;
                            //}

                            //AdvancedMessageBox.Show("Это новое сообщение");

                            string button1Text = $"Продолжить: назначить В/М \n оп. {record.olNomOper} {record.olOperName}";
                            string button2Text = $"Пропустить: НЕ назначать В/М \n оп. {record.olNomOper} {record.olOperName}";
                            string button3Text = checkList.Count == 1 ? "" : "Прервать назначение В/М на выбранные операции";
                            var ButtonsList = new Dictionary<string, DialogResult>();
                            if (checkList.Count == 1)
                            {
                                ButtonsList = new Dictionary<string, DialogResult>
                                {
                                    { button1Text, DialogResult.Yes },
                                    { button2Text, DialogResult.No }
                                };
                            }
                            else if (checkList.Count > 1)
                            {
                                ButtonsList = new Dictionary<string, DialogResult>
                                {
                                    { button1Text, DialogResult.Yes },
                                    { button2Text, DialogResult.No },
                                    { button3Text, DialogResult.Abort }
                                };
                            }

                            //var _result = AdvancedMessageBox.Show(
                            //    $"Внимание! " +
                            //        $"\n Операция: {record.olNomOper} " +
                            //        $"\n Описание: {record.olOperName} " +
                            //        $"\n\n Назначаемая машина ({curr.kmlNumber}) не совпадает с плановой ({_kmlNumber}) ",
                            //    "Назначение В/М на операцию",
                            //    ButtonsList
                            //);

                            // Создаем опции с полным контролем
                            var options = new MessageBoxOptions
                            {
                                Text = $"Внимание!\nОперация: 1/5\nОписание: Вязание воротник\n\n" +
                                       $"Назначаемая машина не совпадает с плановой:" +
                                       $"\n плановая В/М№ {_kmlNumber} - класс {_vyazClass}" +
                                       $"\n назначаемая В/М№ {curr.kmlNumber} - класс {curr.nameVyazClass}",
                                Caption = "Назначение В/М на операцию",
                                Buttons = ButtonsList,
                                Icon = MessageBoxIcon.Warning,
                                ButtonLayout = ButtonLayout.Vertical, // Вертикальное расположение
                                StretchVerticalButtons = true, // Растянуть кнопки по ширине
                                VerticalButtonSpacing = 15, // Больше расстояние между кнопками
                                ButtonHeight = 45, // Высота кнопок для многострочного текста
                                ButtonPadding = 20 // Отступы внутри кнопок
                            };
                            var _result = AdvancedMessageBox.Show(options);

                            //// Вызов с явным указанием вертикального расположения
                            //var _result = AdvancedMessageBox.Show(
                            //    message: $"Внимание!\nОперация: 1/5\nОписание: Вязание воротник\n\n" +
                            //             $"Назначаемая машина (в2193) не совпадает с плановой (в1615)",
                            //    caption: "Назначение В/М на операцию",
                            //    buttons: ButtonsList,
                            //    icon: MessageBoxIcon.Warning,
                            //    layout: ButtonLayout.Vertical // Явно указываем вертикальное расположение
                            //);



                            //var _result = AdvancedMessageBox.Show(
                            //    $"Внимание!\n" +
                            //    $"Операция: 1/5\n" +
                            //    $"Описание: Вязание воротник\n\n" +
                            //    $"Назначаемая машина (в2193) не совпадает с плановой (в1615)",
                            //    "Назначение В/М на операцию",
                            //    new Dictionary<string, DialogResult>
                            //    {
                            //        { "Продолжить: назначить В/М для оп. 1/5 Вязание воротник", DialogResult.Yes },
                            //        { "Пропустить: НЕ назначать В/М для оп. 1/5 Вязание воротник", DialogResult.No },
                            //        { "Отмена", DialogResult.Cancel }
                            //    }
                            //);

                            //// Или используйте специализированный метод
                            //var _result2 = AdvancedMessageBox.ShowMachineAssignment(
                            //    "1/5",
                            //    "Вязание воротник",
                            //    "в2193",
                            //    "в1615"
                            //);

                            //// Или даже проще
                            //var _result3 = AdvancedMessageBox.ShowConfirmation("Вы уверены?");
                            //var _result4 = AdvancedMessageBox.ShowWarning("Что-то пошло не так!");

                            //var _result = AdvancedMessageBox.ShowMachineAssignmentWarning(
                            //    $"Внимание!\n" +
                            //    $"Операция: 1/5\n" +
                            //    $"Описание: Вязание воротник\n\n" +
                            //    $"Назначаемая машина (в2193) не совпадает с плановой (в1615)",
                            //    "Назначение В/М на операцию",
                            //    ButtonsList
                            //);

                            //var _result = AdvancedMessageBox.ShowWithMultilineButtons(new MessageBoxOptions
                            //{
                            //    Text = $"Внимание! " +
                            //        $"\n Операция: {record.olNomOper} " +
                            //        $"\n Описание: {record.olOperName} " +
                            //        $"\n\n Назначаемая машина ({curr.kmlNumber}) не совпадает с плановой ({_kmlNumber}) ",
                            //    Caption = "Назначение В/М на операцию",
                            //    Buttons = ButtonsList,
                            //    Icon = MessageBoxIcon.Warning,
                            //    Width = 600,
                            //    //TextFont = new Font("Segoe UI", 10f),
                            //    TextFont = ThemeManager.SharedSettings.DefaultFont,
                            //    BackColor = this.BackColor
                            //});

                            //var _result = AdvancedMessageBox.ShowWithOptimalButtons(new MessageBoxOptions
                            //{
                            //    Text = $"Внимание! " +
                            //        $"\n Операция: {record.olNomOper} " +
                            //        $"\n Описание: {record.olOperName} " +
                            //        $"\n\n Назначаемая машина ({curr.kmlNumber}) не совпадает с плановой ({_kmlNumber}) ",
                            //    Caption = "Назначение В/М на операцию",
                            //    Buttons = ButtonsList
                            //});
                            ////var _result = AdvancedMessageBox.ShowWithOptimalButtons(
                            ////    text: $"Внимание! " +
                            ////          $"\nОперация: {record.olNomOper} " +
                            ////          $"\nОписание: {record.olOperName} " +
                            ////          $"\n\nНазначаемая машина ({curr.kmlNumber}) не совпадает с плановой ({_kmlNumber})",
                            ////    caption: "Назначение В/М на операцию",
                            ////    buttons: buttons
                            ////);
                            switch (_result)
                            {
                                case DialogResult.No:
                                    record.ErrorSelection = 1;
                                    record.SyncSelection = 0;
                                    break;
                                case DialogResult.Abort:
                                    return;
                                    //break;
                                case DialogResult.Cancel:
                                    return;
                                    //break;
                            }
                            //if (_result == DialogResult.No)
                            //{
                            //    return;
                            //}
                        }
                        if (!CheckPZVDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя назначить В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя назначить В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя назначить В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя назначить В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя назначить В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                    }
                }

                PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

                List<int> filteredList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .Select(x => x.olPzvID)   // новый маппер
                    .ToList();
                SetKnitMachineToPzvID(filteredList, curr.kwsmlKmlID);

                var errList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.ErrorSelection == 1)
                    .ToList();
                if (errList.Count != 0)
                {
                    foreach (var record in errList)
                    {
                        if (record.ErrorSelection == 1 && record.SyncSelection == 0)
                        {
                            record.ErrorSelection = 0;
                            record.SyncSelection = 1;
                        }
                    }
                }

                //LoadSmenZadanyVyazNewDataAsync(vyazPodrKod);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в KnittingMachineWorkAssignment: {ex.Message}");
            }
        }

        private async void buttonKnittingMachineWorkAssignment_Click(object sender, EventArgs e)
        {
            try
            {
                await GridOverlayLoader.RunTaskWithOverlayAsync(
                    gridControlPZVOperList,
                    KnittingMachineWorkAssignment,              // обычный void-метод
                    CancellationToken.None);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в buttonKnittingMachineWorkAssignment_Click: {ex.Message}");
            }
        }

        /// <summary>
        /// Отменить назначение по В/М по V для выбранных операций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KnittingMachineCancelWorkAssignment()
        {
            try
            {
                var checkList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .ToList();
                if (checkList.Count != 0)
                {
                    foreach (var record in checkList)
                    {
                        if (record.olKodPodr != 1)
                        {
                            MessageBox.Show("Операция не относится к вязальному подразделению, нельзя изменять В/М!");
                            return;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя отменить назначение В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя отменить назначение В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя отменить назначение В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя отменить назначение В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя отменить назначение В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }

                    }
                }

                List<int> filteredList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .Select(x => x.olPzvID)   // новый маппер
                    .ToList();

                SetKnitMachineToPzvID(filteredList, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в KnittingMachineCancelWorkAssignment: {ex.Message}");
            }
        }

        private async void buttonKnittingMachineCancelWorkAssignment_Click(object sender, EventArgs e)
        {
            try
            {
                await GridOverlayLoader.RunTaskWithOverlayAsync(
                    gridControlPZVOperList,
                    KnittingMachineCancelWorkAssignment,              // обычный void-метод
                    CancellationToken.None);
                _gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в buttonKnittingMachineCancelWorkAssignment_Click: {ex.Message}");
            }
        }

        /// <summary>
        /// Проставить табельный номер 999 по V для выбранных операций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonTab999WorkAssignment_Click(object sender, EventArgs e)
        {
            try
            {
                await GridOverlayLoader.RunTaskWithOverlayAsync(
                    gridControlPZVOperList,
                    Tab999WorkAssignment,              // обычный void-метод
                    CancellationToken.None);
                //GoToPzvID(_xPzvID, _xColumn);
                _gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в buttonTab999WorkAssignment_Click: {ex.Message}");
            }
        }
        private async void Tab999WorkAssignment()
        {
            try
            {
                var checkList = _pZVOperListByPachListBindingSource.List
                        .OfType<PZVOperList>()
                        .Where(x => x.SyncSelection == 1)
                        .ToList();
                //if (checkList.Count != 0)
                //{
                //    foreach (var record in checkList)
                //    {
                //        if (!CheckPZVDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя проставить таб№ 999 (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                //        {
                //            record.ErrorSelection = 1;
                //            record.SyncSelection = 0;
                //            continue;
                //        }
                //        if (!CheckPZVDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя проставить таб№ 999 (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                //        {
                //            record.ErrorSelection = 1;
                //            record.SyncSelection = 0;
                //            continue;
                //        }
                //        if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя проставить таб№ 999 (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                //        {
                //            record.ErrorSelection = 1;
                //            record.SyncSelection = 0;
                //            continue;
                //        }
                //        if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя проставить таб№ 999 (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                //        {
                //            record.ErrorSelection = 1;
                //            record.SyncSelection = 0;
                //            continue;
                //        }

                //    }
                //}

                PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

                //List<int> filteredList = _pZVOperListByPachListBindingSource.List
                //    .OfType<PZVOperList>()
                //    .Where(x => x.SyncSelection == 1)
                //    .Select(x => x.olPzvID)   // новый маппер
                //    .ToList();
                if (checkList.Count != 0)
                {
                    foreach (var record in checkList)
                    {
                        if (record.olKodPodr != 1 || (record.olKodPodr == 1 && !CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})")))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя проставить таб№ 999 (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя проставить таб№ 999 (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя проставить таб№ 999 (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя проставить таб№ 999 (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }

                    }
                }

                //PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

                List<int> filteredList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .Select(x => x.olPzvID)   // новый маппер
                    .ToList();
                //await 
                SetTabToPzvID(filteredList, 999, 0);
                gridViewPZVOperList.RefreshData();
                //GoToPzvID(pzvCurrent.olPzvID, _xColumn);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка Tab999WorkAssignment: {ex.Message}");
            }
        }
        private async Task SetTabToPzvID(List<int> _pzvId, int _tab, int _kwsID)
        {
            //MessageBox.Show($"Двойной клик по операции. В/М {curr.kmlNumber} ({curr.kmlInvNum}) - Зона {curr.kmaNumber}");
            try
            {
                // получаем список строк (pzvID) для присовения машины (kmlID)
                var idSet = _pzvId != null ? new HashSet<int>(_pzvId) : new HashSet<int>();

                var recordsToUpdate = _pZVOperListByPachListBindingSource.List
                    .Cast<PZVOperList>()
                    .Where(r => r != null && idSet.Contains(r.olPzvID))
                    .ToList();
                foreach (var record in recordsToUpdate)
                {
                    record.olPzvTab = _tab;
                    record.olPzvKwsID = _tab == 0 ? 0 : _kwsID;
                    record.olPzvDateNaznTab = _tab == 0 ? null : DateTime.Now;
                    record.olPzvSekNazn = _tab == 0 ? 0 : record.olSekEd;
                    record.olPzvKolNazn = _tab == 0 ? 0 : record.olKol;
                    record.olPzvChasNazn = _tab == 0 ? 0 : record.olPzvNChasi;
                    if (_tab == 999)
                    {
                        record.olPzvDateStart = DateTime.Now;
                        record.olPzvDateEnd = DateTime.Now;
                        record.olPzvDateML = DateTime.Now;
                        record.olPzvDateMast = DateTime.Now;
                    }
                    record.IsModified = true;
                }
                // Получаем список строк с флагом IsModified = true
                List<PZV> filteredList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x?.IsModified == true)
                    .Select(x => x.ToPZV())   // новый маппер
                    .ToList();
                // Преобразуем в BindingList
                if (filteredList.Count > 0)
                {
                    using (SqlConnection connection = _dbHelper.GetConnection())
                    {
                        _bulkHelper.BulkAllDataUpdate<PZV>(connection, filteredList, "planZagrVyaz", new[] { "pzvID" });
                        try
                        {
                            _pZVOperListByPachListBindingSource.EndEdit();
                            PZVOperList[] toRemove = _pZVOperListByPachListBindingSource.List
                                .OfType<PZVOperList>()
                                .Where(x => x.IsModified)
                                .ToArray();
                            Debug.WriteLine(toRemove.Length);
                            Debug.WriteLine(toRemove.Where(x => x.IsModified));
                            foreach (var record in toRemove)
                            {
                                record.IsModified = false;
                                record.SyncSelection = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка при удалении SetTabToPzvID");
                        }

                        //LoadPlanZagrVyazByZadanySelection();
                        // Создаем и ожидаем завершения асинхронной задачи
                        /*await*/

                        //   MessageBox.Show("1");
                        //LoadPlanZagrVyazByZadanySelectionAsync();
                        LoadPlanZagrVyazByZadanySelection();
                        //MessageBox.Show("2");
                    }
                }
                ////MessageBox.Show("3");
                //_pZVOperListByPachListBindingSource.ResetBindings(false);
                ////MessageBox.Show("4");
                //gridViewPZVOperList.RefreshData();
                ////MessageBox.Show("5");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка присвоения табельного номера SetTabToPzvID: {ex.Message}");
                throw;
            }

        }
        //private async Task SaveAsync() { await LoadPlanZagrVyazByZadanySelection(); }

        /// <summary>
        /// Подтвердить мастером по V для выбранных операций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MasterConfirmation()
        {
            try
            {
                _pZVOperListByPachListBindingSource.ResetBindings(false);
                var checkList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .ToList();
                if (checkList.Count != 0)
                {
                    foreach (var record in checkList)
                    {
                        if (record.olKodPodr != 1 && !CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }

                    }
                }

                PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

                List<int> filteredList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .Select(x => x.olPzvID)   // новый маппер
                    .ToList();
                SetDateMastToPzvID(filteredList, DateTime.Now);

                var errList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.ErrorSelection == 1)
                    .ToList();
                if (errList.Count != 0)
                {
                    foreach (var record in errList)
                    {
                        if (record.ErrorSelection == 1 && record.SyncSelection == 0)
                        {
                            record.ErrorSelection = 0;
                            record.SyncSelection = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в MasterConfirmation: {ex.Message}");
            }
        }
        private async void buttonMasterConfirmation_Click(object sender, EventArgs e)
        {
            try
            {
                await GridOverlayLoader.RunTaskWithOverlayAsync(
                    gridControlPZVOperList,
                    MasterConfirmation,              // обычный void-метод
                    CancellationToken.None);
                //GoToPzvID(_xPzvID, _xColumn);
                _gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в buttonMasterConfirmation_Click: {ex.Message}");
            }
        }

        /// <summary>
        /// Отменить подтверждение мастером по V для выбранных операций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MasterCancelConfirmation()
        {
            try
            {
                _pZVOperListByPachListBindingSource.ResetBindings(false);
                var checkList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .ToList();
                if (checkList.Count != 0)
                {
                    foreach (var record in checkList)
                    {
                        if (record.olKodPodr != 1 && !CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }

                    }
                }

                PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

                List<int> filteredList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .Select(x => x.olPzvID)   // новый маппер
                    .ToList();
                SetDateMastToPzvID(filteredList, null);

                var errList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.ErrorSelection == 1)
                    .ToList();
                if (errList.Count != 0)
                {
                    foreach (var record in errList)
                    {
                        if (record.ErrorSelection == 1 && record.SyncSelection == 0)
                        {
                            record.ErrorSelection = 0;
                            record.SyncSelection = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в MasterCancelConfirmation: {ex.Message}");
            }
        }
        private async void buttonMasterCancelConfirmation_Click(object sender, EventArgs e)
        {
            try
            {
                await GridOverlayLoader.RunTaskWithOverlayAsync(
                    gridControlPZVOperList,
                    MasterCancelConfirmation,              // обычный void-метод
                    CancellationToken.None);
                //GoToPzvID(_xPzvID, _xColumn);
                _gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в buttonMasterCancelConfirmation_Click: {ex.Message}");
            }
        }

        private void customSimpleButton7_Click(object sender, EventArgs e)
        {

        }

        private void customSimpleButton5_Click(object sender, EventArgs e)
        {
            try
            {
                var recordsListToDelete = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    //.Select(x => x.olPzvID)   // новый маппер
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
                    using (SqlConnection connection = _dbHelper.GetConnection())
                    {
                        _bulkHelper.BulkAllDataUpdate<PZV>(connection, filteredList, "planZagrVyaz", new[] { "pzvID" });
                        // удалить из BindingSource
                        //_pZVOperListByPachListBindingSource.RemoveDeleted<PZVOperList>();
                        foreach (var record in recordsListToDelete)
                        {
                            record.IsDeleted = false;
                            record.SyncSelection = 0;
                        }
                        LoadPlanZagrVyazByZadanySelection();
                    }
                }
                _pZVOperListByPachListBindingSource.ResetBindings(false);
                gridViewPZVOperList.RefreshData();
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
        private async void TabWorkAssignment()
        {
            try
            {
                SmenZadanyVyaz curr = _smenZadanyVyazBindingSource.Current as SmenZadanyVyaz;
                _pZVOperListByPachListBindingSource.ResetBindings(false);
                var checkList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .ToList();
                if (checkList.Count != 0)
                {
                    if (curr == null || curr.kwsTabStart == null || curr.kwsTabStart == 0)
                    {
                        MessageBox.Show("Не выбранн работниик для назначения");
                        return;
                    }
                    foreach (var record in checkList)
                    {
                        if (record.olKodPodr != 1 && !CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CompareTabAndMachineArea(record.olPzvKmlID, curr.kwsTabStart))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                    }
                }

                //string _xColumn = gridViewPZVOperList.FocusedColumn.ToString();
                PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;
                List<int> filteredList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .Select(x => x.olPzvID)   // новый маппер
                    .ToList();
                //SmenZadanyVyazEmp curr = _smenZadanyVyazEmpBindingSource.Current as SmenZadanyVyazEmp;

                //SetTabToPzvID(filteredList, curr.empTab);
                await SetTabToPzvID(filteredList, curr.kwsTabStart, curr.kwsID);
                //GoToPzvID(pzvCurrent.olPzvID, _xColumn);

                var errList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.ErrorSelection == 1)
                    //.Select(x => x.olPzvID)   // новый маппер
                    .ToList();
                if (errList.Count != 0)
                {
                    foreach (var record in errList)
                    {
                        if (record.ErrorSelection == 1 && record.SyncSelection == 0)
                        {
                            record.ErrorSelection = 0;
                            record.SyncSelection = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в TabWorkAssignment: {ex.Message}");
            }
        }
        private bool CompareTabAndMachineArea(int _kmlID, int _tab)
        {
            // проверка наличия сочетания табельного № и В/М к одной зоне
            try
            {
                string query = $"SELECT * " +
                                $"  FROM knitWorkingShiftNewCurrentSmen_view wsncsv " +
                                $"      LEFT JOIN knitWorkingShiftMachineListNew wsmln ON wsncsv.kwsID = wsmln.kwsmlKwsID" +
                                $"   WHERE wsncsv.shiftStatusID in (0,2) " +
                                $"      AND wsncsv.tabShiftStart = {_tab} " +
                                $"      AND wsmln.kwsmlKmlID = {_kmlID} ";
                _isCountTabKM = _dbHelper.Exists(query, new Dictionary<string, object> { });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка CompareTabAndMachineArea: {ex.Message}");
            }
            if (!_isCountTabKM)
            {
                MessageBox.Show($"Внимание! Табельный номер и В/М находятся в разных зонах!");
                return false;
            }
            else
            {
                return true;
            }
            //gridViewSmenZadany.LocateByValue("kmlID", _kmlID);
            //    "kwsTabStart
        }
        private async void buttonTabWorkAssignment_Click(object sender, EventArgs e)
        {
            try
            {
                await GridOverlayLoader.RunTaskWithOverlayAsync(
                    gridControlPZVOperList,
                    TabWorkAssignment,              // обычный void-метод
                    CancellationToken.None);
                //GoToPzvID(_xPzvID, _xColumn);
                _gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в buttonTabWorkAssignment_Click: {ex.Message}");
            }
        }

        /// <summary>
        /// Отменить назначение на Таб№ по V для выбранных операций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TabCancelWorkAssignment()
        {
            try
            {
                _pZVOperListByPachListBindingSource.ResetBindings(false);
                var checkList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .ToList();
                if (checkList.Count != 0)
                {
                    foreach (var record in checkList)
                    {
                        if (record.olKodPodr != 1 && !CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя отменить назначение работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя отменить назначение работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя отменить назначение работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя отменить назначение работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя отменить назначение работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                    }
                }

                PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

                List<int> filteredList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .Select(x => x.olPzvID)   // новый маппер
                    .ToList();
                SetTabToPzvID(filteredList, 0, 0);
                //GoToPzvID(pzvCurrent.olPzvID, _xColumn);

                var errList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.ErrorSelection == 1)
                    .ToList();
                if (errList.Count != 0)
                {
                    foreach (var record in errList)
                    {
                        if (record.ErrorSelection == 1 && record.SyncSelection == 0)
                        {
                            record.ErrorSelection = 0;
                            record.SyncSelection = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в TabCancelWorkAssignment: {ex.Message}");
            }
        }
        private async void buttonTabCancelWorkAssignment_Click(object sender, EventArgs e)
        {
            try
            {
                await GridOverlayLoader.RunTaskWithOverlayAsync(
                    gridControlPZVOperList,
                    TabCancelWorkAssignment,              // обычный void-метод
                    CancellationToken.None);
                //GoToPzvID(_xPzvID, _xColumn);
                _gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в buttonTabCancelWorkAssignment_Click: {ex.Message}");
            }
        }
        private async void gridViewPZVOperList_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            // 0) Игнор при программном обновлении
            if (Volatile.Read(ref _isUiRefreshing) == 1)
                return;

            if (e.FocusedRowHandle < 0) return;
            if (_pZVOperListByPachListBindingSource == null) return;
            if (_pZVOperListByPachListBindingSource.Count == 0) return;
            if (_pZVOperListByPachListBindingSource.Position < 0) return;

            if (!(_pZVOperListByPachListBindingSource.Current is PZVOperList currPZV))
                return;

            // 1) Отменяем предыдущую загрузку деталей
            _focusLoadCts?.Cancel();
            _focusLoadCts?.Dispose();
            _focusLoadCts = new CancellationTokenSource();
            var ct = _focusLoadCts.Token;

            // 2) Номер “версии” (чтобы не применить устаревший результат)
            var seq = Interlocked.Increment(ref _focusSeq);

            try
            {
                // (опционально) маленький debounce на мышь/клавиатуру, чтобы не дергать БД при быстром скролле
                await Task.Delay(120, ct);

                // 3) Грузим детали
                var annId = currPZV.olPzvAnnID;
                var nrId = currPZV.olPzvNrID;

                await Task.WhenAll(
                    LoadArtNormNDataAsync(annId, ct),
                    LoadNormRaszDataAsync(nrId, ct)
                );

                // 4) “последний победил”: если пока грузили фокус сменился — не трогаем UI
                if (ct.IsCancellationRequested) return;
                if (seq != Volatile.Read(ref _focusSeq)) return;

                // 5) Обновление гридов строго в UI-потоке
                gridViewArtNormN.RefreshData();
                gridViewNormRasz.RefreshData();
            }
            catch (OperationCanceledException)
            {
                // нормально: пользователь/refresh сменил фокус
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

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

        private async void layoutControlGroup1_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);
            switch (buttonIndex)
            {
                case 10:
                    try
                    {
                        NaklReport report1 = new NaklReport();
                        ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
                        reportPrintTool1.ShowPreviewDialog();
                    }
                    catch (Exception ex)
                    {
                        await _logger.LogErrorAsync(ex, $"Ошибка печати накладной");
                    }
                    break;
            }
        }

        private async void gridViewPZVOperList_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.FieldName != gridColumnPZVOperListOlKol.FieldName)
                    return;

                var view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                var currentItem = view.GetRow(e.RowHandle) as PZVOperList;
                if (currentItem == null) return;

                if (currentItem.olPzvDateNaznKm != null && currentItem.olPzvDateEnd == null)
                {
                    MessageBox.Show("Нельзя изменять количество по операции, назначенной на В/М и не завершённой!");
                    currentItem.olKol = currentItem.olKolCopy;
                    return;
                }

                if (currentItem.olPzvDateNaznTab != null && currentItem.olPzvDateEnd == null)
                {
                    MessageBox.Show("Нельзя изменять количество по операции, назначенной работнику и не завершённой!");
                    currentItem.olKol = currentItem.olKolCopy;
                    return;
                }

                if (Convert.ToInt32(e.Value) >= currentItem.olKolCopy)
                {
                    MessageBox.Show("Новое количество не может быть больше или равно исходному!");
                    currentItem.olKol = currentItem.olKolCopy;
                    return;
                }

                if (Convert.ToInt32(e.Value) < 0)
                {
                    MessageBox.Show("Новое количество не должно быть меньше нуля!");
                    currentItem.olKol = currentItem.olKolCopy;
                    return;
                }

                if (vyazPodrKod == 1)
                {
                    int xPzvID = currentItem.olPzvID;
                    string _xColumn = gridViewPZVOperList.FocusedColumn.ToString();
                    if (currentItem.olPzvDateEnd != null && currentItem.olPzvDateMast == null)
                    {
                        string query = $"exec dbo.PZV_Split @pzvId = {currentItem.olPzvID}, @mode = 2, @qtyFact = {Convert.ToInt32(e.Value)} ";
                        Task updateRZV = _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { });
                        await Task.WhenAll(updateRZV);

                        await GridOverlayLoader.RunTaskWithOverlayAsync(
                            gridControlPZVOperList,
                            LoadPlanZagrVyazByZadanySelection
                            , CancellationToken.None
                            );
                    }
                    else if (currentItem.olPzvDateNaznKm == null
                            && currentItem.olPzvDateNaznTab == null
                            && currentItem.olPzvDateStart == null
                            && currentItem.olPzvDateEnd == null
                            && currentItem.olPzvDateMast == null)
                    {
                        string query = $"exec dbo.PZV_Split @pzvId = {currentItem.olPzvID}, @mode = 3, @qtyFact = {Convert.ToInt32(e.Value)} ";
                        Task updateRZV = _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { });
                        await Task.WhenAll(updateRZV);

                        await GridOverlayLoader.RunTaskWithOverlayAsync(
                            gridControlPZVOperList,
                            LoadPlanZagrVyazByZadanySelection
                            , CancellationToken.None
                            );
                    }
                    _gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, xPzvID, _xColumn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в gridViewPZVOperList_CellValueChanged: {ex.Message}");
            }
        }
        private void gridViewPZVOperList_ShownEditor(object sender, EventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                var editor = view?.ActiveEditor as TextEdit;
                editor?.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в gridViewPZVOperList_ShownEditor: {ex.Message}");
            }
        }

        private async void repositoryItemCheckEdit5_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                gridViewRzvPachListByNom.PostEditor();        // применить новое значение из редактора
                gridViewRzvPachListByNom.UpdateCurrentRow();  // сохранить в источник данных
                var selectedRow = _rzvPachListByNomBindingSource.Current as RzvPachListByNom;

                // градация
                if (selectedRow != null)
                {
                    // Преобразуем в JSON
                    string jsonString = JsonConvert.SerializeObject(selectedRow, Formatting.Indented);
                    // 1 - вяз подразделение. по другим подразделениям на градацию не отделяем
                    string query = $"dbo.setGraduationRate @xNomListJson = '{jsonString}', @xGradationValue = {selectedRow.gradacia}, @xPodrKod = 1 ";
                    Task updateRZV = _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { });
                    await Task.WhenAll(updateRZV);

                    await GridOverlayLoader.RunTaskWithOverlayAsync(
                        gridControlPZVOperList,
                        LoadPlanZagrVyazByZadanySelection
                        , CancellationToken.None
                        );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка repositoryItemCheckEdit5_EditValueChanged: {ex.Message}");

            }

            // деление операций на градацию - жду Катю Бабинцеву
            // отпарку и раскрой на градацию не отделяем

        }
        /// <summary>
        /// Начать выполнение по V для выбранных операций
        /// </summary>
        private async void WorkStartExecution()
        {
            try
            {
                //_pZVOperListByPachListBindingSource.ResetBindings(false);
                var checkList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .ToList();
                if (checkList.Count != 0)
                {
                    foreach (var record in checkList)
                    {
                        if (record.olPzvKwsID == 0)
                        {
                            MessageBox.Show($"Операция не назначена на смену, нельзя начать выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})");
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!(CheckPZVShiftOpened(record.olPzvKwsID, $"нельзя начать выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})")
                            && !CheckPZVShiftClosed(record.olPzvKwsID, $"нельзя начать выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})")))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (record.olKodPodr != 1 && !CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }

                    }
                }

                PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

                List<int> filteredList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .Select(x => x.olPzvID)   // новый маппер
                    .ToList();
                SetDateStartToPzvID(filteredList, DateTime.Now);

                var errList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.ErrorSelection == 1)
                    .ToList();
                if (errList.Count != 0)
                {
                    foreach (var record in errList)
                    {
                        if (record.ErrorSelection == 1 && record.SyncSelection == 0)
                        {
                            record.ErrorSelection = 0;
                            record.SyncSelection = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в WorkStartExecution: {ex.Message}");
            }
        }
        private async void buttonWorkStartExecution_Click(object sender, EventArgs e)
        {
            try
            {
                await GridOverlayLoader.RunTaskWithOverlayAsync(
                    gridControlPZVOperList,
                    WorkStartExecution,              // обычный void-метод
                    CancellationToken.None);
                //GoToPzvID(_xPzvID, _xColumn);
                _gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в buttonWorkStartExecution_Click: {ex.Message}");
            }
        }
        /// <summary>
        /// Отменить начало выполнения по V для выбранных операций
        /// </summary>
        private async void CancelWorkStartExecution()
        {
            try
            {
                var checkList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .ToList();
                if (checkList.Count != 0)
                {
                    foreach (var record in checkList)
                    {
                        if (record.olPzvKwsID == 0)
                        {
                            MessageBox.Show($"Операция не назначена на смену, нельзя отменить начало выполнения (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})");
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!(CheckPZVShiftOpened(record.olPzvKwsID, $"нельзя отменить начало выполнения (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})")
                            && !CheckPZVShiftClosed(record.olPzvKwsID, $"нельзя отменить начало выполнения (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})")))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (record.olKodPodr != 1 && !CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя начать выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя начать выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя начать выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя начать выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя начать выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                    }
                }

                PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

                List<int> filteredList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .Select(x => x.olPzvID)   // новый маппер
                    .ToList();
                DateTime? date = (DateTime?)null;
                SetDateStartToPzvID(filteredList, date);

                var errList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.ErrorSelection == 1)
                    .ToList();
                if (errList.Count != 0)
                {
                    foreach (var record in errList)
                    {
                        if (record.ErrorSelection == 1 && record.SyncSelection == 0)
                        {
                            record.ErrorSelection = 0;
                            record.SyncSelection = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в CancelWorkStartExecution: {ex.Message}");
            }
        }

        /// <summary>
        /// Окончить выполнение по V для выбранных операций
        /// </summary>
        private async void WorkStopExecution()
        {
            try
            {
                _pZVOperListByPachListBindingSource.ResetBindings(false);
                var checkList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .ToList();
                if (checkList.Count != 0)
                {
                    foreach (var record in checkList)
                    {
                        if (record.olPzvKwsID == 0)
                        {
                            MessageBox.Show($"Операция не назначена на смену, нельзя завершить выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})");
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!(CheckPZVShiftOpened(record.olPzvKwsID, $"нельзя завершить выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})")
                            && !CheckPZVShiftClosed(record.olPzvKwsID, $"нельзя завершить выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})")))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (record.olKodPodr != 1 && !CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }

                    }
                }

                PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

                List<int> filteredList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .Select(x => x.olPzvID)   // новый маппер
                    .ToList();
                SetDateEndToPzvID(filteredList, DateTime.Now);

                var errList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.ErrorSelection == 1)
                    .ToList();
                if (errList.Count != 0)
                {
                    foreach (var record in errList)
                    {
                        if (record.ErrorSelection == 1 && record.SyncSelection == 0)
                        {
                            record.ErrorSelection = 0;
                            record.SyncSelection = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в WorkStopExecution: {ex.Message}");
            }
        }
        /// <summary>
        ///  Отменить окончание выполнения по V для выбранных операций
        /// </summary>
        private async void CancelWorkStopExecution()
        {
            try
            {
                _pZVOperListByPachListBindingSource.ResetBindings(false);
                var checkList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .ToList();
                if (checkList.Count != 0)
                {
                    foreach (var record in checkList)
                    {
                        if (record.olPzvKwsID == 0)
                        {
                            MessageBox.Show($"Операция не назначена на смену, нельзя отменить окончание выполнения (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})");
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!(CheckPZVShiftOpened(record.olPzvKwsID, $"нельзя отменить окончание выполнения (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})") 
                            && !CheckPZVShiftClosed(record.olPzvKwsID, $"нельзя отменить окончание выполнения (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})")))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (record.olKodPodr != 1 && !CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                        if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                            continue;
                        }
                    }
                }

                PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

                List<int> filteredList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .Select(x => x.olPzvID)   // новый маппер
                    .ToList();
                DateTime? date = (DateTime?)null;
                SetDateEndToPzvID(filteredList, date);

                var errList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.ErrorSelection == 1)
                    .ToList();
                if (errList.Count != 0)
                {
                    foreach (var record in errList)
                    {
                        if (record.ErrorSelection == 1 && record.SyncSelection == 0)
                        {
                            record.ErrorSelection = 0;
                            record.SyncSelection = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в CancelWorkStopExecution: {ex.Message}");
            }
        }

        private bool CheckPZVShiftClosed(int _olPzvKwsID, string _xMessage)
        {
            try
            {
                string query = "SELECT COUNT(*) AS kwsClosedCount FROM knitWorkingShiftNew_view wsnv WHERE wsnv.kwsID = @kwsID AND kwsDateEnd IS NOT NULL";
                int result = _dbHelper.ExecuteScalar(query, new Dictionary<string, object> { { "@kwsID", _olPzvKwsID } });

                string _detailMessage = string.Empty;
                if (result == 1)
                {
                    _detailMessage = "Смена завершена";
                    MessageBox.Show($"{_detailMessage}, {_xMessage}!");
                    return true;
                }
                else if (result > 1)
                {
                    _detailMessage = "Не удалось получить информацию по смене";
                    MessageBox.Show($"{_detailMessage}, {_xMessage}!");
                    return false;
                }
                else
                {
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
                string query = "SELECT COUNT(*) AS kwsOpenedCount FROM knitWorkingShiftNew_view wsnv WHERE wsnv.kwsID = @kwsID AND kwsDateStart IS NOT NULL";
                int result = _dbHelper.ExecuteScalar(query, new Dictionary<string, object> { { "@kwsID", _olPzvKwsID } });

                string _detailMessage = string.Empty;
                if (result == 0)
                {
                    _detailMessage = "Смена не начата";
                    MessageBox.Show($"{_detailMessage}, {_xMessage}!");
                    return false;
                }
                else if (result > 1)
                {
                    _detailMessage = "Не удалось получить информацию по смене";
                    MessageBox.Show($"{_detailMessage}, {_xMessage}!");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в CheckPZVShiftOpened: {ex.Message}");
                return false;
            }
        }

        private async void buttonCancelWorkStartExecution_Click(object sender, EventArgs e)
        {
            try
            {
                await GridOverlayLoader.RunTaskWithOverlayAsync(
                    gridControlPZVOperList,
                    CancelWorkStartExecution,              // обычный void-метод
                    CancellationToken.None);
                _gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в buttonCancelWorkStartExecution_Click: {ex.Message}");
            }
        }
        private async void buttonWorkStopExecution_Click(object sender, EventArgs e)
        {
            try
            {
                await GridOverlayLoader.RunTaskWithOverlayAsync(
                    gridControlPZVOperList,
                    WorkStopExecution,              // обычный void-метод
                    CancellationToken.None);
                //GoToPzvID(_xPzvID, _xColumn);
                _gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в buttonWorkStopExecution_Click: {ex.Message}");
            }
        }

        private async void buttonCancelWorkStopExecution_Click(object sender, EventArgs e)
        {
            try
            {
                await GridOverlayLoader.RunTaskWithOverlayAsync(
                    gridControlPZVOperList,
                    CancelWorkStopExecution,              // обычный void-метод
                    CancellationToken.None);
                //GoToPzvID(_xPzvID, _xColumn);
                _gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в buttonCancelWorkStopExecution_Click: {ex.Message}");
            }
        }

        private void gridViewRzvPachListByNom_ShowingEditor(object sender, CancelEventArgs e)
        {
            //GridView view = (GridView)sender;
            // если колонка gridColumnRzvPachListByNomGradacia редактировать запрещена:
            try
            {
                if (gridViewRzvPachListByNom.FocusedColumn == gridColumnRzvPachListByNomGradacia &&
                    gridColumnRzvPachListByNomGradacia.OptionsColumn.ReadOnly)
                {
                    e.Cancel = true; // запретить редактирование
                    MessageBox.Show("Внимание! Технологом не проставлен признак градации - нельзя проставить градацию на пачку!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в gridViewRzvPachListByNom_ShowingEditor: {ex.Message}");
            }
        }

        private void gridViewRzvPachListByNom_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
        }

        private void repositoryItemCheckEdit1_EditValueChanged(object sender, EventArgs e)
        {
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
                        await LoadSmenZadanyVyazNewDataAsync(vyazPodrKod);
                        //SetGroupExpandState();
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

        private void перенестиВДругуюЗонуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("111");
        }

        private void advBandedGridViewSmenZadany_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
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
                //LoadKnitWorkingShiftSmenToMoveDataSync(currentSmenZadanyVyaz.kwsKmaID);
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
                var row = _view.GetRow(_rowHandle);
                if (row == null)
                    return;

                // Например, получить ID записи
                //int xKwsID = (row as SmenZadanyVyaz).kwsID;
                int xKmlID = (row as SmenZadanyVyaz).kwsmlKmlID;
                int xKodOb = (row as SmenZadanyVyaz).kodOb;
                int xLongRep = (row as SmenZadanyVyaz).longRep;

                //MessageBox.Show($"Перенос kmlID {xKmlID} kodOb | {xKodOb} | в зону kmaID {_kmaID} | kwsID {_kwsID}");

                string query = $"INSERT INTO knitWorkingShiftMachineListNew (kwsmlDateAdd, kwsmlCompAdd, kwsmlKwsID, kwsmlKmlID, kwsmlKodOb, kiwsmlLongRep) " +
                    $"VALUES (DEFAULT, DEFAULT, {_kwsID}, {xKmlID}, {xKodOb}, {xLongRep})";
                _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { });


                await LoadSmenZadanyVyazNewDataAsync(vyazPodrKod); ;

                var changes = BindingSourceHelper.GetChanges<SmenZadanyVyaz>(
                        _smenZadanyVyazBindingSource,
                        _smenZadanyVyazNewBindingSource,
                        HashMode.ExcludeOnly,
                        keyProperties: new[] { "kwsmlKmlID", "typeID" },
                        hashProperties: new[] { "IsNew", "IsModified", "IsDeleted" }
                    );
                // обновить и добавить
                //BindingSourceHelper.ApplyChanges(_smenZadanyVyazBindingSource, changes, x => (x.kwsmlKmlID, x.typeID));
                BindingSourceHelper.ApplyChanges<SmenZadanyVyaz>(
                        _smenZadanyVyazBindingSource,
                        changes,
                        UpdateFieldsMode.ExcludeOnly,
                        keyProperties: new[] { "kwsmlKmlID", "typeID" },
                        gridViewSmenZadany,
                        fields: new[] { "IsNew", "IsModified", "IsDeleted" }
                    );
                // удалить отсутствующие
                //BindingSourceHelper.RemoveMissing(_smenZadanyVyazBindingSource, changes.Removed);
                var metrics = BindingSourceHelper.RemoveMissingSmart<SmenZadanyVyaz>(
                    _smenZadanyVyazBindingSource,
                    changes.Removed,
                    gridControlSmenZadany
                );

                Application.Idle += ExpandGroupsOnIdle;


                gridViewSmenZadany.RefreshData();

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
                  }          ));  
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
                SmenZadanyFocusedRowChanged(e.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка advBandedGridViewSmenZadany_FocusedRowChanged: {ex.Message}");
            }
        }
        private async void SmenZadanyFocusedRowChanged(int _focusedRowHandle)
        {
            try
            {
                //var col = advBandedGridViewSmenZadany.Columns["chasNaznZadGroup"];
                //MessageBox.Show($"{col.ColumnType}");
                var currentSmenZadanyVyaz = _smenZadanyVyazBindingSource.Current as SmenZadanyVyaz;
                if (currentSmenZadanyVyaz == null)
                    return;
                await LoadKnitWorkingShiftSmenToMoveDataAsync(currentSmenZadanyVyaz.kwsKmaID);
                int _xTab = currentSmenZadanyVyaz.kwsTabStart;
                int _xKmlID = currentSmenZadanyVyaz.kwsmlKmlID;
                int _groupLevel = gridViewNaryadZadany.GetRowLevel(_focusedRowHandle);

                //if (!new[] { 0, 1 }.Contains(_groupLevel))
                //{
                //    // level НЕ 0 и НЕ 1
                //}
                if (currentSmenZadanyVyaz.typeID == 1 && !gridViewNaryadZadany.IsGroupRow(_focusedRowHandle))
                {
                    // наряд-задание по В/М
                    _xTab = 0;
                    //await LoadNaryadZadanyVyazDataAsync(currentSmenZadanyVyaz.kwsTabStart, 0);
                }
                else if (currentSmenZadanyVyaz.typeID == 2 || (gridViewNaryadZadany.IsGroupRow(_focusedRowHandle) && new[] { 0, 1 }.Contains(_groupLevel)))
                {
                    // наряд-задание по таб№
                    _xKmlID = 0;
                    //await LoadNaryadZadanyVyazDataAsync(0, currentSmenZadanyVyaz.kwsmlKmlID);
                }
                await LoadNaryadZadanyVyazDataAsync(_xTab, _xKmlID);
                gridViewNaryadZadany.RefreshData();
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

        //private void OnFormClosed(object sender, FormClosedEventArgs e)
        //{
        //    Application.Idle -= ExpandGroupsOnIdle;
        // //   _loadCts?.Cancel();
        //   // base.OnFormClosed(e);
        //}
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try
            {
                _lifetimeCts?.Cancel(); // ← ВСЁ, остальное автоматически
                _loadCts?.Cancel();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Form] OnFormClosed error: {ex}");
            }
            finally
            {
                _lifetimeCts?.Dispose();
                _loadCts?.Dispose();
                base.OnFormClosed(e);
            }
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Application.Idle -= ExpandGroupsOnIdle;
                // _loadCts?.Cancel();
                //base.OnFormClosed(e);
                _loadCts?.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка OnFormClosed: {ex.Message}");
            }
        }
        //protected override async void OnFormClosing(FormClosingEventArgs e)
        //{
        //    _loadCts?.Cancel();

        //    if (_sbHelper != null)
        //        await _sbHelper.DisposeAsync();

        //    _loadCts?.Dispose();
        //    base.OnFormClosing(e);
        //}
        protected void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //_loadCts?.Cancel();

                //if (_sbHelper != null)
                //    _sbHelper.DisposeAsync();

                ////_loadCts?.Dispose();
                //base.OnFormClosing(e);


                if (_closing) return;
                _closing = true;

                gridViewPlanTotalHoursByKnitMachine.FocusedRowChanged -= gridViewPlanTotalHoursByKnitMachine_FocusedRowChanged;
                gridViewZadanyListByMachine.FocusedRowChanged -= gridViewZadanyListByMachine_FocusedRowChanged;
                gridViewRzvPachListByNom.FocusedRowChanged -= gridViewRzvPachListByNom_FocusedRowChanged;
                gridViewPZVOperList.FocusedRowChanged -= gridViewPZVOperList_FocusedRowChanged;
                advBandedGridViewSmenZadany.FocusedRowChanged -= advBandedGridViewSmenZadany_FocusedRowChanged;
                gridViewNaryadZadany.PopupMenuShowing -= gridViewNaryadZadany_PopupMenuShowing;
                _loadCts?.Cancel();

                // НЕ ждём, чтобы не блокировать закрытие формы
                _ = _sbController.DisposeAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FormClosingError");
                Debug.WriteLine(ex);
            }
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
                        gridViewZadanyListByMachine.CollapseAllGroups();
                        break;
                    case 2:
                        gridViewZadanyListByMachine.ExpandAllGroups();
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

        private void layoutControlGroup2_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {

        }

        private void gridViewNaryadZadany_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            //if (e.HitInfo.InRowCell && e.HitInfo.Column.FieldName == "NomZad")
            //{
            //    // Получаем значение
            //    //var cellValue = gridViewNaryadZadany.GetCellValue(e.HitInfo.RowHandle, e.HitInfo.Column);
            //    var cellValue = gridViewNaryadZadany.GetFocusedValue();
            //    string _selectedCellValue = cellValue?.ToString();

            //    // Добавляем кастомный пункт в существующее меню DevExpress
            //    var customMenuItem = new DevExpress.Xpf.Bars.BarButtonItem()
            //    {
            //        Content = "Поиск в TextBox",
            //        Glyph = new System.Windows.Media.GeometryGeometry() // Иконка если нужно
            //    };

            //    customMenuItem.ItemClick += (s, args) =>
            //    {
            //        if (!string.IsNullOrEmpty(_selectedCellValue))
            //        {
            //            textBox1.Text = _selectedCellValue;
            //        }
            //    };

            //    // Вставляем в начало меню
            //    e.Customizations.Insert(0, new DevExpress.Xpf.Bars.AddBarItemAction()
            //    {
            //        Item = customMenuItem
            //    });
            //}

            // Получаем позицию курсора относительно GridView
            //var position = e.GetPosition(gridViewNaryadZadany);
            //var hit = gridViewNaryadZadany.CalcHitInfo(position);

            //----------------------------

            //// Проверяем, что клик был в нужной колонке
            //if (e.HitInfo.InRowCell && e.HitInfo.Column == gridViewNaryadZadany.Columns["NomZad"])
            //{
            //    // Получаем значение ячейки
            //    object cellValue = gridViewNaryadZadany.GetRowCellValue(e.HitInfo.RowHandle, e.HitInfo.Column);

            //    // Создаем контекстное меню
            //    DXPopupMenu menu = new DXPopupMenu();

            //    // Добавляем пункт "Поиск"
            //    DXMenuItem searchItem = new DXMenuItem("Поиск");
            //    searchItem.Tag = cellValue; // Сохраняем значение
            //    searchItem.Click += (s, args) =>
            //    {
            //        if (searchItem.Tag != null)
            //        {
            //            textBoxZadanyNumberSearch.Text = searchItem.Tag.ToString();
            //        }
            //    };

            //    // Добавляем пункт "Копировать"
            //    DXMenuItem copyItem = new DXMenuItem("Копировать");
            //    copyItem.Tag = cellValue;
            //    copyItem.Click += (s, args) =>
            //    {
            //        if (copyItem.Tag != null)
            //        {
            //            Clipboard.SetText(copyItem.Tag.ToString());
            //            MessageBox.Show("Скопировано в буфер обмена");
            //        }
            //    };

            //    menu.Items.Add(searchItem);
            //    menu.Items.Add(copyItem);

            //    // Заменяем стандартное меню на наше
            //    e.Menu = (GridMenu)menu;
            //}
        }

        private void textBoxPzvNomZadSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindPzvNomZadInNzp();
            }
        }

        private async void FindPzvNomZadInNzp()
        {
            _autoSelectPending = true;
            string pszNom = NormalizeNom(textBoxPzvNomZadSearch.Text);
            if (pszNom.Length == 0) return;

            // Параметр вместо интерполяции
            int kmlId = _dbHelper.ExecuteScalar(
                @"SELECT TOP (1) pszm.pszkmKmlID
                  FROM plan_sezon_zad_knitMachine pszm
                  WHERE pszm.pszkmPszNom = @pszNom",
                new Dictionary<string, object>
                {
                    ["@pszNom"] = pszNom
                });

            if (kmlId <= 0) return;

            _pendingPszNom = pszNom;

            //------------------------------------
            //var viewTop = gridViewPlanTotalHoursByKnitMachine;
            var colKnitMachine = gridViewPlanTotalHoursByKnitMachine.Columns.FirstOrDefault(c => c.FieldName == "kmlID");
            if (colKnitMachine == null) return;

            int rhKnitMachine = gridViewPlanTotalHoursByKnitMachine.LocateByValue(0, colKnitMachine, kmlId);
            if (!gridViewPlanTotalHoursByKnitMachine.IsValidRowHandle(rhKnitMachine) || !gridViewPlanTotalHoursByKnitMachine.IsDataRow(rhKnitMachine)) return;

            gridViewPlanTotalHoursByKnitMachine.BeginUpdate();
            try
            {
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
            var colNomZadany = gridViewZadanyListByMachine.Columns.FirstOrDefault(c => c.FieldName == "kmlID");
            if (colNomZadany == null) return;

            int rhNomZadany = gridViewZadanyListByMachine.LocateByValue(0, colNomZadany, _pendingPszNom);
            if (!gridViewZadanyListByMachine.IsValidRowHandle(rhNomZadany) || !gridViewZadanyListByMachine.IsDataRow(rhNomZadany)) return;

            gridViewZadanyListByMachine.BeginUpdate();
            try
            {
                gridViewZadanyListByMachine.FocusedRowHandle = rhNomZadany;
                gridViewZadanyListByMachine.MakeRowVisible(rhNomZadany);
                gridViewZadanyListByMachine.ClearSelection();
                gridViewZadanyListByMachine.SelectRow(rhNomZadany);
            }
            finally
            {
                gridViewZadanyListByMachine.EndUpdate();
            }

            _autoSelectPending = false;
            // ВАЖНО: НЕ вызываем gridViewPlanTotalHoursByKnitMachine_FocusedRowChanged руками.
            ////MessageBox.Show($"Поиск по № задания {textBoxPzvNomZadSearch.Text}");
            //int _xKmlID = _dbHelper.ExecuteScalar($"SELECT pszm.pszkmKmlID, mlv.kmlNumber, pszm.pszkmPszNom " +
            //    $" FROM plan_sezon_zad_knitMachine pszm " +
            //    $"    LEFT JOIN knitMachineList_view mlv ON pszm.pszkmKmlID = mlv.kmlID " +
            //    $" WHERE pszm.pszkmPszNom = '{textBoxPzvNomZadSearch.Text}'", new Dictionary<string, object> {  });

            ////var col = gridViewPlanTotalHoursByKnitMachine.Columns["kmlID"];
            ////var col = gridViewPlanTotalHoursByKnitMachine.Columns["gridColumnPlanTotalHoursByKnitMachineKmlID"];

            ////----позиционирование на строке с номером машины
            //var col = gridViewPlanTotalHoursByKnitMachine.Columns
            //    .FirstOrDefault(c => c.FieldName == "kmlID");
            //if (col == null) return;

            //int _rowHandle = gridViewPlanTotalHoursByKnitMachine.LocateByValue(0, col, _xKmlID);
            //if (_rowHandle < 0 || !gridViewPlanTotalHoursByKnitMachine.IsDataRow(_rowHandle)) return;

            //gridViewPlanTotalHoursByKnitMachine.FocusedRowHandle = _rowHandle;
            //gridViewPlanTotalHoursByKnitMachine.MakeRowVisible(_rowHandle);
            //gridViewPlanTotalHoursByKnitMachine_FocusedRowChanged(
            //            gridViewPlanTotalHoursByKnitMachine,
            //            new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, _rowHandle)
            //        );

            //gridViewZadanyListByMachine.RefreshData();
            //gridControlZadanyListByMachine.ForceInitialize();


            ////----позиционирование на строке с номером задания
            //gridControlZadanyListByMachine.BeginInvoke(new Action(() =>
            //{
            //    var view = gridViewZadanyListByMachine;

            //    var col = view.Columns.FirstOrDefault(c => c.FieldName == "pszNom");
            //    if (col == null) return;

            //    var search = (textBoxPzvNomZadSearch.Text ?? "").Trim();
            //    if (search.Length == 0) return;

            //    int rh = view.LocateByValue(0, col, search);

            //    if (!view.IsValidRowHandle(rh) || !view.IsDataRow(rh)) return;

            //    view.FocusedRowHandle = rh;
            //    view.MakeRowVisible(rh);
            //}));
        }

        private void textBoxPzvNomZadSearch_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPzvNomZadSearch.Text.Length > 0)
            {
                FindPzvNomZadInNzp();
            }
        }
    }
}


namespace SewingProduction.Features.KnittingProduction.Forms
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

        private CancellationTokenSource? _debounceCts;
        private CancellationTokenSource? _maxWaitCts;

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
                System.Diagnostics.Debug.WriteLine($"[ObjectRefreshCoordinator] Reload failed: {objectName}. {ex}");
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
