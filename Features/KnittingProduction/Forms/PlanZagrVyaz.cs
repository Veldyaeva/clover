using DevExpress.Data;
using DevExpress.Mvvm.Native;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Newtonsoft.Json;
using SewingProduction.Core.helpers;
using SewingProduction.Extensions;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Features.KnittingProduction.Services;
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
using static SewingProduction.Core.helpers.BindingSourceHelper;
using static SewingProduction.Helpers.GridHelper;


namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class PlanZagrVyaz : CustomForm, IThemeable
    {
        int vyazPodrKod = 0;
        
        private bool _suppressZadanyFocusedChanged;
        private int _rzvLoadVersion;

        private readonly DebouncedLoader _loader = new(delayMs: 300);

        private CancellationTokenSource? _loadCts;

        private static DatabaseHelper _dbHelper;
        private static DbService _dbService;
        private static MlService _mlService;
        private static ArtNormRepository _anService;
        private static BulkHelper _bulkHelper;
        private static GridHelper _gridHelper;
        //        private static BindingSourceHelper _bSHelper;
        private readonly ILogger _logger = new FileLogger();
        private readonly VyazService _vyazService;

        string _xColumn = string.Empty;
        int _xPzvID = 0;
        private bool _isCountTabKM = false;

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

        //private List<SmenZadanyVyazMachine> _currentSmenZadanyVyazMachineData = new List<SmenZadanyVyazMachine>();
        //private List<SmenZadanyVyazMachine> smenZadanyVyazMachineData = new List<SmenZadanyVyazMachine>();
        //private BindingList<SmenZadanyVyazMachine> _smenZadanyVyazMachineBindingList;
        //private BindingSource _smenZadanyVyazMachineBindingSource;

        //private List<SmenZadanyVyazEmp> _currentSmenZadanyVyazEmpData = new List<SmenZadanyVyazEmp>();
        //private List<SmenZadanyVyazEmp> smenZadanyVyazEmpData = new List<SmenZadanyVyazEmp>();
        //private BindingList<SmenZadanyVyazEmp> _smenZadanyVyazEmpBindingList;
        //private BindingSource _smenZadanyVyazEmpBindingSource;

        //private List<SmenZadanyVyaz> smenZadanyVyazData = new List<SmenZadanyVyaz>();
        //private BindingList<SmenZadanyVyaz> _smenZadanyVyazBindingList;
        private BindingSource _smenZadanyVyazBindingSource;

        //private List<SmenZadanyVyaz> smenZadanyVyazNewData = new List<SmenZadanyVyaz>();
        //private BindingList<SmenZadanyVyaz> _smenZadanyVyazNewBindingList;
        private BindingSource _smenZadanyVyazNewBindingSource;

        private List<KnitWorkingShiftSmen> KnitWorkingShiftSmenData = new List<KnitWorkingShiftSmen>();
        private BindingList<KnitWorkingShiftSmen> _knitWorkingShiftSmenBindingList;
        private BindingSource _knitWorkingShiftSmenBindingSource;

        //private List<NaryadZadanyVyaz> naryadZadanyVyazData = new List<NaryadZadanyVyaz>();
        //private BindingList<NaryadZadanyVyaz> _naryadZadanyVyazBindingList;
        private BindingSource _naryadZadanyVyazBindingSource;

        public PlanZagrVyaz()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _dbService = new DbService(_dbHelper);
            _anService = new ArtNormRepository(_dbHelper);
            _bulkHelper = new BulkHelper();
            _gridHelper = new GridHelper();
            _vyazService = new VyazService(_dbHelper);
            _mlService = new MlService(_dbHelper);
            ThemeManager.UpdateTheme(this);
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
                //var smenZadanyVyazEmpTask = Task.Run(() =>
                //{
                //    _smenZadanyVyazEmpBindingList = new BindingList<SmenZadanyVyazEmp>();
                //    _smenZadanyVyazEmpBindingSource = new BindingSource { DataSource = _smenZadanyVyazEmpBindingList };
                //});
                //var smenZadanyVyazMachineTask = Task.Run(() =>
                //{
                //    _smenZadanyVyazMachineBindingList = new BindingList<SmenZadanyVyazMachine>();
                //    _smenZadanyVyazMachineBindingSource = new BindingSource { DataSource = _smenZadanyVyazMachineBindingList };
                //});
                //var smenZadanyVyazTask = Task.Run(() =>
                //{
                //    _smenZadanyVyazBindingList = new BindingList<SmenZadanyVyaz>();
                //    _smenZadanyVyazBindingSource = new BindingSource { DataSource = _smenZadanyVyazBindingList };
                //});
                //var smenZadanyVyazNewTask = Task.Run(() =>
                //{
                //    _smenZadanyVyazNewBindingList = new BindingList<SmenZadanyVyaz>();
                //    _smenZadanyVyazNewBindingSource = new BindingSource { DataSource = _smenZadanyVyazNewBindingList };
                //});
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
                        //, smenZadanyVyazEmpTask, smenZadanyVyazMachineTask
                        //, smenZadanyVyazTask
                        //, smenZadanyVyazNewTask
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
                #region multiselect
                // 1. Настраиваем стандартный MultiSelect
                //gridViewZadanyListByMachine.OptionsSelection.MultiSelect = true;  // Включаем множественный выбор
                //gridViewZadanyListByMachine.OptionsBehavior.AutoUpdateTotalSummary = true;
                //gridViewZadanyListByMachine.OptionsView.ShowIndicator = false;   // Скрываем стандартный индикатор
                //gridViewVyazPlan.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = false;
                //gridViewVyazPlan.OptionsSelection.ShowCheckBoxSelectorInGroupRow = false;

                //// 2. Создаем кастомный столбец с чекбоксами
                ////var checkColumn = new DevExpress.XtraGrid.Columns.GridColumn();
                ////checkColumn.FieldName = "IsSelected";
                ////checkColumn.Caption = " ";
                ////checkColumn.VisibleIndex = 0;  // Ставим на первое место
                ////checkColumn.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
                //gridViewZadanyListByMachine.IndicatorWidth = 0;
                //gridColumnZadanyListByMachineSyncSelection.UnboundDataType = typeof(bool);

                //// Создаем RepositoryItemCheckEdit для отображения чекбоксов
                ////var checkEdit = new RepositoryItemCheckEdit();
                ////gridControl1.RepositoryItems.Add(checkEdit);
                ////checkColumn.ColumnEdit = checkEdit;

                ////gridView1.Columns.Add(checkColumn);

                //// 3. Обновляем состояние чекбоксов при выделении строк

                //gridViewZadanyListByMachine.SelectionChanged += (s, e) =>
                //{
                //    #region "новый обработчик"
                //    // Устанавливаем фокус на нужные колонки
                //    gridViewZadanyListByMachine.FocusedColumn = gridViewZadanyListByMachine.Columns["dopr_name"];
                //    gridViewZadanyListByMachine.FocusedColumn = gridViewZadanyListByMachine.Columns["SyncSelection"];

                //    //gridViewZadanyListByMachine.BeginUpdate();
                //    //try
                //    //{
                //    //    // Проверка выделения по IDVyazClass (новая логика)
                //    //    if (gridViewVyazPlan.SelectedRowsCount > 0)
                //    //    {
                //    //        var firstSelectedId = gridViewVyazPlan.GetRowCellValue(gridViewVyazPlan.GetSelectedRows()[0], "IDVyazClass");
                //    //        var invalidSelections = new List<int>();

                //    //        foreach (int rowHandle in gridViewVyazPlan.GetSelectedRows())
                //    //        {
                //    //            var currentId = gridViewVyazPlan.GetRowCellValue(rowHandle, "IDVyazClass");
                //    //            if (!object.Equals(currentId, firstSelectedId))
                //    //            {
                //    //                invalidSelections.Add(rowHandle);
                //    //            }
                //    //        }

                //    //        // Если есть недопустимые выделения, снимаем их
                //    //        if (invalidSelections.Count > 0)
                //    //        {
                //    //            foreach (int rowHandle in invalidSelections)
                //    //            {
                //    //                gridViewVyazPlan.UnselectRow(rowHandle);
                //    //            }

                //    //            XtraMessageBox.Show("Можно выделять только строки с одинаковым IDVyazClass",
                //    //                               "Ограничение выделения",
                //    //                               MessageBoxButtons.OK,
                //    //                               MessageBoxIcon.Information);
                //    //            return; // Прерываем обработку, так как выделение изменилось
                //    //        }
                //    //    }

                //    //    // Ваша оригинальная логика обновления SyncSelection
                //    //    foreach (int rowHandle in gridViewVyazPlan.GetSelectedRows())
                //    //    {
                //    //        gridViewVyazPlan.SetRowCellValue(rowHandle, gridColumnVyazPlanSyncSelection, true);
                //    //        gridViewVyazPlan.PostEditor();
                //    //    }

                //    //    // Сбрасываем чекбоксы у невыделенных строк
                //    //    for (int i = 0; i < gridViewVyazPlan.RowCount; i++)
                //    //    {
                //    //        if (!gridViewVyazPlan.IsRowSelected(i))
                //    //        {
                //    //            gridViewVyazPlan.SetRowCellValue(i, gridColumnVyazPlanSyncSelection, false);
                //    //            gridViewVyazPlan.PostEditor();
                //    //        }
                //    //    }
                //    //}
                //    //finally
                //    //{
                //    //    gridViewVyazPlan.EndUpdate();
                //    //    gridViewVyazPlan.FocusedColumn = gridViewVyazPlan.Columns["SyncSelection"];
                //    //}
                //    #endregion
                //};

                //// 4. Обрабатываем клик по чекбоксу для выделения/снятия строки
                //gridViewZadanyListByMachine.RowCellClick += (s, e) =>
                //{
                //    if (e.Column == gridColumnZadanyListByMachineSyncSelection)
                //    {
                //        bool newValue = !(bool)(gridViewZadanyListByMachine.GetRowCellValue(e.RowHandle, gridColumnZadanyListByMachineSyncSelection) ?? false);
                //        //gridView1.SetRowCellValue(e.RowHandle, gridColumnZadanyListByMachineSyncSelection, newValue);

                //        if (newValue)
                //        {
                //            gridViewZadanyListByMachine.SelectRow(e.RowHandle);
                //        }
                //        else
                //        {
                //            gridViewZadanyListByMachine.UnselectRow(e.RowHandle);
                //        }
                //    }
                //};

                ////// Подписываемся на событие изменения данных
                ////// В конструкторе или методе инициализации
                ////gridViewVyazPlan.ActiveFilter.Nodes.CollectionChanged += (s, e) =>
                ////{
                ////    if (gridViewVyazPlan.RowCount > 0)
                ////    {
                ////        gridViewVyazPlan_FocusedRowChanged?.Invoke(
                ////            gridViewVyazPlan,
                ////            new FocusedRowChangedEventArgs(gridViewVyazPlan.FocusedRowHandle, -1)
                ////        );
                ////    }
                ////};

                #endregion

                ////// При программном изменении фильтра
                ////gridViewVyazPlan.ActiveFilterCriteria = newCriteria;
                ////gridViewVyazPlan.RefreshData();
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


                //// 2. Создаём GroupSummary 
                //advBandedGridViewSmenZadany.GroupSummary.Clear();

                //advBandedGridViewSmenZadany.GroupSummary.Add(
                //    new GridGroupSummaryItem
                //    {
                //        SummaryType = DevExpress.Data.SummaryItemType.Average,
                //        FieldName = "chasNaznZadGroup",
                //        ShowInGroupColumnFooter = bandedGridSmenZadanyColumnChasNaznZad,
                //        DisplayFormat = "0.00;-0.00;"
                //    });

                //advBandedGridViewSmenZadany.GroupSummary.Clear();

                //advBandedGridViewSmenZadany.GroupSummary.Add(
                //    new GridGroupSummaryItem
                //    {
                //        SummaryType = DevExpress.Data.SummaryItemType.Average,
                //        FieldName = "chasNotConfirmedZadGroup",
                //        ShowInGroupColumnFooter = bandedGridSmenZadanyColumnChasNotConfirmedZad,
                //        DisplayFormat = "0.00;-0.00;"
                //    });

                string _xNumFormat = "{0:0.00;-0.00;}";
                //string _xNumFormat = "{0:n2}";
                foreach (GridGroupSummaryItem gsi in advBandedGridViewSmenZadany.GroupSummary)
                {
                    gsi.DisplayFormat = _xNumFormat;
                }

                //advBandedGridViewSmenZadany.RefreshData();
                //advBandedGridViewSmenZadany.UpdateGroupSummary();

                //advBandedGridViewSmenZadany.GroupSummary[0].DisplayFormat = _xNumFormat;
                //advBandedGridViewSmenZadany.GroupSummary[0].FieldName = "chasNaznZad";
                //advBandedGridViewSmenZadany.GroupSummary[0].SummaryType = DevExpress.Data.SummaryItemType.Sum;
                //advBandedGridViewSmenZadany.GroupSummary[0].

                ////var item = new DevExpress.XtraGrid.GridGroupSummaryItem();
                //var item = advBandedGridViewSmenZadany.GroupSummary[0];
                //item.SummaryType = DevExpress.Data.SummaryItemType.Sum;

                //// 1) по чему считаем
                //item.FieldName = "chasNaznZadGroup";

                //// 2) где показываем
                //item.ShowInGroupColumnFooter = AdvBandedGridView;

                //// (опционально) формат вывода
                //item.DisplayFormat = "{0:n2}";


                //advBandedGridViewSmenZadany.CustomDrawBandHeader += AdvBandedGridView1_CustomDrawBandHeader;
                //advBandedGridViewSmenZadany.CustomDrawColumnHeader += AdvBandedGridView1_CustomDrawColumnHeader;
                //advBandedGridViewSmenZadany.ColumnPanelRowHeight = 40; // можно больше/меньше
                _gridHelper.AutoRowFilterConfig(advBandedGridViewSmenZadany, 0);
                //advBandedGridViewSmenZadany.Appearance.GroupRow.Assign(
                //    advBandedGridViewSmenZadany.Appearance.HeaderPanel);
                advBandedGridViewSmenZadany.OptionsView.GroupFooterShowMode = GroupFooterShowMode.Hidden;
                _gridHelper.EnableGroupSummariesInGroupRow(advBandedGridViewSmenZadany, GroupSummaryLevelMode.IncludeOnly, new[] { 2 });
                //advBandedGridViewSmenZadany.RowCountChanged += (_, __) => SetGroupExpandState();
                //advBandedGridViewSmenZadany.CustomDrawGroupRow += (s, e) =>
                //{
                //    GridView view = s as GridView;
                //    int rowHandle = e.RowHandle;

                //    int level = view.GetRowLevel(rowHandle);

                //    GridGroupRowInfo groupInfo = e.Info as GridGroupRowInfo;
                //    //groupInfo.GroupExpanded = true;

                //    if (level == 0) // зона
                //    {
                //        groupInfo.GroupText = $"Зона: {view.GetGroupRowValue(e.RowHandle, view.Columns["kmaNumber"])} ";
                //    }
                //    if (level == 1) // ФИО
                //    {
                //        groupInfo.GroupText = $"{view.GetGroupRowValue(e.RowHandle, view.Columns["fio"])} ({view.GetGroupRowValue(e.RowHandle, view.Columns["kwsTabStart"])}) ";
                //    }
                //    if (level == 2) // тип данных
                //    {
                //        groupInfo.GroupText = $"{view.GetGroupRowValue(e.RowHandle, view.Columns["typeName"])}";
                //    }
                //};
                #endregion

                _naryadZadanyVyazBindingSource = new BindingSource { DataSource = new BindingList<NaryadZadanyVyaz>() };
                #region gridControlNaryadZadany "сменное задание"
                gridControlNaryadZadany.DataSource = _naryadZadanyVyazBindingSource;
                gridNaryadZadanyColumnKmlNumber.FieldName = "kmlNumber";
                gridNaryadZadanyColumnPzvArticul.FieldName = "pzvArticul";
                gridNaryadZadanyColumnPzvNomZad.FieldName = "pzvNomZad";
                gridNaryadZadanyColumnPzvNom.FieldName = "pzvNom";
                gridNaryadZadanyColumnNPach.FieldName = "n_pach";
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
                gridViewNaryadZadany.OptionsView.ShowGroupPanel = false;
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
                gridColumnPZVOperListOlNChasi.FieldName = "olPzvNChasi";
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

                gridColumnPZVOperListOlNChasi.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //gridColumnPZVOperListOlNChasi.DisplayFormat.FormatString = "#,0.00;-#,0.00;''";
                gridColumnPZVOperListOlNChasi.DisplayFormat.FormatString = "#,0.00;-#,0.00;";
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

                gridViewPZVOperList.FocusedRowChanged += (s, e) =>
                {
                    //Debug.WriteLine($"FocusedRowChanged: {e.FocusedRowHandle}");
                    Debug.WriteLine($"FocusedRowChanged -> {e.FocusedRowHandle}, stack:\n{Environment.StackTrace}");
                };

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
        //private void View_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        //{
        //    var view = (BandedGridView)sender;

        //    // стандартная отрисовка заголовка группы
        //    e.DefaultDraw();

        //    // Получаем прямоугольник всей строки группы
        //    Rectangle rowRect = e.Bounds;

        //    // Высота "строки итогов" внутри GroupRow
        //    int summaryHeight = rowRect.Height - 4;

        //    // Смещение текста от верхней части (чтобы не налезало на знак раскрытия)
        //    int offsetY = 0;

        //    // Для красоты — рисуем светлый фон строки итогов
        //    using (var brush = new SolidBrush(Color.FromArgb(245, 245, 245)))
        //        e.Graphics.FillRectangle(brush, rowRect);

        //    // Перебираем ВСЕ ВИДИМЫЕ КОЛОНКИ
        //    foreach (GridColumn col in view.VisibleColumns)
        //    {
        //        // Координаты колонки
        //        Rectangle colRect = view.GetColumnBounds(col);

        //        Rectangle summaryRect = new Rectangle(
        //            colRect.X,
        //            rowRect.Y + offsetY,
        //            colRect.Width,
        //            summaryHeight
        //        );

        //        // Получаем групповой итог для этой колонки
        //        var summaryItem = view.GroupSummary
        //            .OfType<GridSummaryItem>()
        //            .FirstOrDefault(s => s.FieldName == col.FieldName);

        //        if (summaryItem != null)
        //        {
        //            object val = view.GetGroupSummaryValue(e.RowHandle, summaryItem);
        //            string text = "";

        //            if (val != null && val != DBNull.Value)
        //                text = Convert.ToDecimal(val).ToString("n2");

        //            // Центрируем текст
        //            var sf = new StringFormat()
        //            {
        //                Alignment = StringAlignment.Center,
        //                LineAlignment = StringAlignment.Center,
        //                Trimming = StringTrimming.EllipsisCharacter
        //            };

        //            // Фон
        //            e.Graphics.FillRectangle(Brushes.White, summaryRect);

        //            // Текст
        //            e.Graphics.DrawString(
        //                text,
        //                e.Appearance.Font,
        //                Brushes.Black,
        //                summaryRect,
        //                sf
        //            );

        //            // Рамка
        //            e.Graphics.DrawRectangle(Pens.Gray, summaryRect);
        //        }
        //    }

        //    // Сообщаем DevExpress, что мы всё сделали (не перерисовывать)
        //    e.Handled = true;
        //}
        //private void SetGroupExpandState()
        //{
        //    var view = advBandedGridViewSmenZadany;

        //    // Обходим ВСЕ группы (а не строки данных)
        //    for (int rowHandle = view.RowCount - 1; rowHandle >= 0; rowHandle--)
        //    {
        //        if (!view.IsGroupRow(rowHandle))
        //            continue;

        //        int level = view.GetRowLevel(rowHandle);

        //        // Нас интересует только 3-й уровень (level = 2, если нумерация с 0)
        //        if (level != 2)
        //            continue;

        //        // Получаем значение 3-го уровня группировки — "Тип данных"
        //        var groupValue = view.GetGroupRowValue(rowHandle);

        //        // Здесь groupValue = код типа: 1 или 2
        //        int type = Convert.ToInt32(groupValue);

        //        if (type == 1)  // м/ч
        //        {
        //            view.SetRowExpanded(rowHandle, true);
        //        }
        //        else if (type == 2) // ч/ч
        //        {
        //            view.SetRowExpanded(rowHandle, false);
        //        }
        //    }
        //}
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

        //private void SetGroupExpandStateSafe()
        //{

        //    try
        //    {
        //        var view = advBandedGridViewSmenZadany;

        //        view.GridControl.BeginInvoke(new Action(() =>
        //        {
        //            int rootCount = view.GetChildRowCount(
        //                DevExpress.XtraGrid.GridControl.InvalidRowHandle
        //            );

        //            if (rootCount == 0)
        //            {
        //                // группы ещё не построены — пробуем позже
        //                SetGroupExpandStateSafe();
        //                return;
        //            }

        //            SetGroupExpandState();
        //        }));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка SetGroupExpandStateSafe: {ex.Message}");
        //    }
        //}
        //private void SetGroupExpandStateSafe()
        //{
        //    var view = advBandedGridViewSmenZadany;

        //    view.GridControl.BeginInvoke(new Action(() =>
        //    {
        //        int rootCount = view.GetChildRowCount(
        //            DevExpress.XtraGrid.GridControl.InvalidRowHandle
        //        );

        //        if (rootCount == 0)
        //        {
        //            // группы ещё не построены — пробуем позже
        //            SetGroupExpandStateSafe();
        //            return;
        //        }

        //        SetGroupExpandState_NoRecursion();
        //    }));
        //}
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

                    //if (level == 2)
                    //{
                    //    var value = view.GetGroupRowValue(childHandle);
                    //    Debug.WriteLine($"    LEVEL 2 VALUE = {value} ({value?.GetType()})");

                    //    int type;
                    //    if (value != null && int.TryParse(value.ToString(), out type))
                    //    {
                    //        view.SetRowExpanded(childHandle, type == 1);
                    //    }
                    //}
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
                        //_currentPlanTotalHoursByKnitMachineData = planTotalHoursByKnitMachineData;              // Обновляем текущую модель
                        //_planTotalHoursByKnitMachineBindingSource.DataSource = planTotalHoursByKnitMachineData; // Привязываем данные к форме
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
                        //_currentZadanyListByMachineNewData = zadanyListByMachineNewData;                // Обновляем текущую модель
                        //_zadanyListByMachineNewBindingSource.DataSource = _currentZadanyListByMachineNewData; // Привязываем данные к форме
                        _zadanyListByMachineNewBindingSource.DataSource = zadanyListByMachineNewData;
                    });

                    await _logger.LogEventAsync($"Данные ZadanyListByMachine успешно загружены", "LoadZadanyListByMachineNewDataAsync");
                    //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
                    _zadanyListByMachineNewBindingList.Add(zadanyListByMachineNewData[0]);
                    _zadanyListByMachineNewBindingSource.ResetBindings(false);
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
                        //_currentRzvPachListByNomNewData = rzvPachListByNomNewData;                // Обновляем текущую модель
                        //_rzvPachListByNomNewBindingSource.DataSource = _currentRzvPachListByNomNewData; // Привязываем данные к форме
                        _rzvPachListByNomNewBindingSource.DataSource = rzvPachListByNomNewData;
                    });

                    await _logger.LogEventAsync($"Данные RzvPachListByNom успешно загружены", "LoadRzvPachListByNomNewDataAsync");
                    //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
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
                        //_currentPZVOperListByPachListNewData = pZVOperListByPachListNewData;                // Обновляем текущую модель
                        //_pZVOperListByPachListNewBindingSource.DataSource = _currentPZVOperListByPachListNewData; // Привязываем данные к форме
                        _pZVOperListByPachListNewBindingSource.DataSource = pZVOperListByPachListNewData;
                    });

                    await _logger.LogEventAsync($"Данные PZVOperListByPachList успешно загружены", "LoadPZVOperListByPachListNewDataAsync");
                    //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
                    _pZVOperListByPachListNewBindingList.Add(pZVOperListByPachListNewData[0]);
                    //_pZVOperListByPachListNewBindingSource.Sort = "olPzvArticul, olNPach, olNo, olNpo, olPzvIDParent, olPzvID";
                    _pZVOperListByPachListNewBindingSource.ResetBindings(false);
                    //gridViewPZVOperList.ExpandAllGroups();
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные PZVOperListByPachList", "LoadPZVOperListByPachListNewDataAsync");
                }
            }
            //try
            //{
            //    _pZVOperListByPachListNewBindingSource.Clear();
            //    _pZVOperListByPachListNewBindingSource.ResetBindings(false);

            //    if (pachList.Length > 0 && pachList != "[]")
            //    {
            //        pZVOperListByPachListNewData = await _vyazService.GetPZVOperListByPachList(pachList, vyazPodrKod);
            //    }
            //    else
            //    {
            //        pZVOperListByPachListNewData = null;
            //        _pZVOperListByPachListNewBindingSource.DataSource = new PZVOperList();
            //        _pZVOperListByPachListNewBindingSource.ResetBindings(false);
            //    }

            //    if (pZVOperListByPachListNewData != null)
            //    {
            //        await _logger.LogEventAsync($"Получены данные PZVOperListByPachListNew", "LoadPZVOperListByPachListNewDataAsync");

            //        await this.InvokeAsync(() =>
            //        {
            //            _currentPZVOperListByPachListNewData = pZVOperListByPachListNewData;                // Обновляем текущую модель
            //            _pZVOperListByPachListNewBindingSource.DataSource = _currentPZVOperListByPachListNewData; // Привязываем данные к форме
            //        });

            //        await _logger.LogEventAsync($"Данные PZVOperListByPachList успешно загружены", "LoadPZVOperListByPachListNewDataAsync");
            //        //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
            //        _pZVOperListByPachListNewBindingList.Add(pZVOperListByPachListNewData[0]);
            //        //_pZVOperListByPachListNewBindingSource.Sort = "olPzvArticul, olNPach, olNo, olNpo, olPzvIDParent, olPzvID";
            //        _pZVOperListByPachListNewBindingSource.ResetBindings(false);
            //        //gridViewPZVOperList.ExpandAllGroups();
            //    }
            //    else
            //    {
            //        await _logger.LogEventAsync($"Не удалось найти данные PZVOperListByPachList", "LoadPZVOperListByPachListNewDataAsync");
            //    }
            //}
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных PZVOperListByPachListNew");
            }
        }
        //private async Task LoadSmenZadanyVyazMachineDataAsync()
        //{
        //    try
        //    {
        //        _smenZadanyVyazMachineBindingSource.Clear();
        //        _smenZadanyVyazMachineBindingSource.ResetBindings(false);

        //        smenZadanyVyazMachineData = await _vyazService.GetSmenZadanyVyazMachine();


        //        if (smenZadanyVyazMachineData != null)
        //        {
        //            await _logger.LogEventAsync($"Получены данные SmenZadanyVyazMachine", "LoadSmenZadanyVyazMachineDataAsync");

        //            await this.InvokeAsync(() =>
        //            {
        //                //_currentSmenZadanyVyazMachineData = smenZadanyVyazMachineData;                // Обновляем текущую модель
        //                //_smenZadanyVyazMachineBindingSource.DataSource = _currentSmenZadanyVyazMachineData; // Привязываем данные к форме
        //                _smenZadanyVyazMachineBindingSource.DataSource = smenZadanyVyazMachineData;
        //            });

        //            await _logger.LogEventAsync($"Данные SmenZadanyVyazMachine успешно загружены", "LoadSmenZadanyVyazMachineDataAsync");
        //            //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
        //            _smenZadanyVyazMachineBindingList.Add(smenZadanyVyazMachineData[0]);
        //            //_pZVOperListByPachListNewBindingSource.Sort = "olPzvArticul, olNPach, olNo, olNpo, olPzvIDParent, olPzvID";
        //            _pZVOperListByPachListNewBindingSource.ResetBindings(false);
        //            //gridViewSmenZadanyVyazMachine.ExpandAllGroups();
        //        }
        //        else
        //        {
        //            await _logger.LogEventAsync($"Не удалось найти данные PZVOperListByPachList", "LoadPZVOperListByPachListNewDataAsync");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных PZVOperListByPachListNew");
        //    }
        //}
        //private async Task LoadSmenZadanyVyazEmpDataAsync()
        //{
        //    try
        //    {
        //        _smenZadanyVyazEmpBindingSource.Clear();
        //        _smenZadanyVyazEmpBindingSource.ResetBindings(false);

        //        smenZadanyVyazEmpData = await _vyazService.GetSmenZadanyVyazEmp();

        //        if (smenZadanyVyazEmpData != null)
        //        {
        //            await _logger.LogEventAsync($"Получены данные SmenZadanyVyazEmp", "LoadSmenZadanyVyazEmpDataAsync");

        //            await this.InvokeAsync(() =>
        //            {
        //                //_currentSmenZadanyVyazEmpData = smenZadanyVyazEmpData;                // Обновляем текущую модель
        //                //_smenZadanyVyazEmpBindingSource.DataSource = _currentSmenZadanyVyazEmpData; // Привязываем данные к форме
        //                _smenZadanyVyazEmpBindingSource.DataSource = smenZadanyVyazEmpData;
        //            });

        //            await _logger.LogEventAsync($"Данные SmenZadanyVyazEmp успешно загружены", "LoadSmenZadanyVyazEmpDataAsync");
        //            //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
        //            _smenZadanyVyazEmpBindingList.Add(smenZadanyVyazEmpData[0]);
        //            _smenZadanyVyazEmpBindingSource.ResetBindings(false);
        //            //gridViewSmenZadanyVyazEmp.ExpandAllGroups();
        //        }
        //        else
        //        {
        //            await _logger.LogEventAsync($"Не удалось найти данные SmenZadanyVyazEmp", "LoadSmenZadanyVyazEmpDataAsync");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных SmenZadanyVyazEmp");
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

        private async Task LoadSmenZadanyVyazDataAsync()
        {
            //try
            //{
            //    _smenZadanyVyazBindingSource.Clear();
            //    _smenZadanyVyazBindingSource.ResetBindings(false);

            //    int _xIDNazn = 6; // признак принаджелности зоны к Вязальному производству
            //    int _xKodProizv = 1; // код производства 1 - вязальное производство
            //    int _xKodPodr = 1; // код подразделения 1 - вязальное подразделение
            //    smenZadanyVyazData = await _vyazService.GetSmenZadanyVyaz(_xIDNazn, _xKodProizv, _xKodPodr);

            //    if (smenZadanyVyazData != null)
            //    {
            //        await _logger.LogEventAsync($"Получены данные SmenZadanyVyaz", "LoadSmenZadanyVyazDataAsync");
            //        await this.InvokeAsync(() =>
            //        {
            //            _smenZadanyVyazBindingSource.DataSource = smenZadanyVyazData;
            //        });
            //        Application.Idle -= ExpandGroupsOnIdle;
            //        Application.Idle += ExpandGroupsOnIdle;
            //        await _logger.LogEventAsync($"Данные SmenZadanyVyaz успешно загружены", "LoadSmenZadanyVyazDataAsync");
            //        //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
            //        _smenZadanyVyazBindingList.Add(smenZadanyVyazData[0]);
            //        _smenZadanyVyazBindingSource.ResetBindings(false);
            //    }
            //    else
            //    {
            //        await _logger.LogEventAsync($"Не удалось найти данные SmenZadanyVyaz", "LoadSmenZadanyVyazDataAsync");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных SmenZadanyVyaz");
            //}

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
                            //----------------------
                            var changes = BindingSourceHelper.GetChanges<SmenZadanyVyaz>(
                                _smenZadanyVyazBindingSource,
                                _smenZadanyVyazNewBindingSource,
                                HashMode.ExcludeOnly,
                                keyProperties: new[] { "kwsKmaID", "kwsmlKmlID" },
                                hashProperties: new[] { "IsNew", "IsModified", "IsDeleted" }
                            );

                            BindingSourceHelper.ApplyChanges<SmenZadanyVyaz>(
                                _smenZadanyVyazBindingSource,
                                changes,
                                UpdateFieldsMode.ExcludeOnly,
                                keyProperties: new[] { "kwsKmaID", "kwsmlKmlID" },
                                gridViewPZVOperList,
                                fields: new[] { "IsNew", "IsModified", "IsDeleted" }
                            );

                            BindingSourceHelper.RemoveMissingSmart<SmenZadanyVyaz>(
                                _smenZadanyVyazBindingSource,
                                changes.Removed,
                                gridControlSmenZadany
                            );
                            //----------------------
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
        //private void OnSmenZadanyGroupingReady(object sender, EventArgs e)
        //{
        //    advBandedGridViewSmenZadany.EndSorting -= OnSmenZadanyGroupingReady;
        //    //SetGroupExpandState();
        //    SetGroupExpandStateSafe();
        //}

        //private void LoadSmenZadanyVyazNewDataSync()
        //{
        //    try
        //    {
        //        LoadSmenZadanyVyazNewDataAsync().GetAwaiter().GetResult();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogErrorAsync(ex, $"Ошибка загрузки данных LoadSmenZadanyVyazNewDataSync");
        //    }
        //}
        //private async Task LoadSmenZadanyVyazNewDataAsync(int _xKodProizv)
        //{
        //    try
        //    {
        //        _smenZadanyVyazNewBindingSource.Clear();
        //        _smenZadanyVyazNewBindingSource.ResetBindings(false);

        //        int _xIDNazn = 6; // признак принаджелности зоны к Вязальному производству
        //        //_xKodProizv = 1; // код производства 1 - вязальное производство
        //        int _xKodPodr = 1; // код подразделения 1 - вязальное подразделение
        //        smenZadanyVyazNewData = await _vyazService.GetSmenZadanyVyaz(_xIDNazn, _xKodProizv, _xKodPodr);

        //        if (smenZadanyVyazNewData != null)
        //        {
        //            await _logger.LogEventAsync($"Получены данные SmenZadanyVyazNew", "LoadSmenZadanyVyazNewDataAsync");

        //            await this.InvokeAsync(() =>
        //            {
        //                _smenZadanyVyazNewBindingSource.DataSource = smenZadanyVyazNewData;
        //            });

        //            await _logger.LogEventAsync($"Данные SmenZadanyVyazNew успешно загружены", "LoadSmenZadanyVyazNewDataAsync");
        //            //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
        //            _smenZadanyVyazNewBindingList.Add(smenZadanyVyazNewData[0]);
        //            _smenZadanyVyazNewBindingSource.ResetBindings(false);
        //            //advBandedGridViewSmenZadany.ExpandAllGroups();
        //        }
        //        else
        //        {
        //            await _logger.LogEventAsync($"Не удалось найти данные SmenZadanyVyazNew", "LoadSmenZadanyVyazNewDataAsync");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных SmenZadanyVyaz");
        //    }
        //}
        private async Task LoadArtNormNDataAsync(int _annID)
        {
            try
            {
                _artNormNBindingSource.Clear();
                _artNormNBindingSource.ResetBindings(false);

                //artNormNData = await _vyazService.GetZadanyListByMachine(kmlID);
                //artNormNData = await _dbService.GetListAsync<ArtNormN>($"Select annID, grup, articul, mod, data_obn, annDateDel, annCompDel, status from ArtNormNView where annId = {_annID}", new {  });
                var newArtNormNData = await _anService.GetArtNormDataById(_annID);
                artNormNData.Clear();
                artNormNData.Add(newArtNormNData);
                if (artNormNData != null)
                {
                    await _logger.LogEventAsync($"Получены данные ArtNormN", "LoadArtNormNDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        //_currentArtNormNData = artNormNData;                // Обновляем текущую модель
                        //_artNormNBindingSource.DataSource = _currentArtNormNData; // Привязываем данные к форме
                        _artNormNBindingSource.DataSource = artNormNData;
                    });

                    await _logger.LogEventAsync($"Данные ArtNormN успешно загружены", "LoadArtNormNDataAsync");
                    //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
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

                //artNormNData = await _vyazService.GetZadanyListByMachine(kmlID);
                //normRaszData = await _dbService.GetListAsync<NormRasz>($"Select * from NormRaszView where nrId = {_nrID}", new {  });
                var newNormRaszData = await _anService.GetRelatedNormRaszByID(_nrID);
                normRaszData.Clear();
                normRaszData.Add(newNormRaszData);
                if (normRaszData != null)
                {
                    await _logger.LogEventAsync($"Получены данные NormRasz", "LoadNormRaszDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        //_currentNormRaszData = normRaszData;                // Обновляем текущую модель
                        //_normRaszBindingSource.DataSource = _currentNormRaszData; // Привязываем данные к форме
                        _normRaszBindingSource.DataSource = newNormRaszData;
                    });

                    await _logger.LogEventAsync($"Данные NormRasz успешно загружены", "LoadNormRaszDataAsync");
                    //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
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
        //private void LoadKnitWorkingShiftSmenToMoveDataSync(int kmaID)
        //{
        //    try
        //    {
        //        LoadKnitWorkingShiftSmenToMoveDataSyncInternal(kmaID).GetAwaiter().GetResult();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogErrorAsync(ex, $"Ошибка загрузки данных LoadKnitWorkingShiftSmenToMoveDataSync");
        //    }
        //}
        //private async Task LoadKnitWorkingShiftSmenToMoveDataSyncInternal(int kmaID)
        //{
        //    try
        //    { 
        //        await LoadKnitWorkingShiftSmenToMoveDataAsync(kmaID);

        //        // кэшируем зоны (bindingSource уже наполнен)
        //        _zonesCache = _knitWorkingShiftSmenBindingSource.Cast<KnitWorkingShiftSmen>().ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogErrorAsync(ex, $"Ошибка загрузки данных LoadKnitWorkingShiftSmenToMoveDataSyncInternal");
        //    }
        //}
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
            //try
            //{
            //    _knitWorkingShiftSmenBindingSource.Clear();
            //    _knitWorkingShiftSmenBindingSource.ResetBindings(false);

            //    //artNormNData = await _vyazService.GetZadanyListByMachine(kmlID);
            //    KnitWorkingShiftSmenData = await _dbService.GetListAsync<KnitWorkingShiftSmen>($"SELECT * FROM knitWorkingShiftNewCurrentSmen_view " +
            //        $" WHERE kmaID <> {_kmaID} " +
            //        $"  AND dateShiftStart IS NOT NULL " +
            //        $"  AND dateShiftEnd IS NULL ", null);
            //    //KnitWorkingShiftSmenData = await _dbService.GetListAsync<KnitWorkingShiftSmen>("SELECT * FROM knitWorkingShiftNewCurrentSmen_view " +
            //    //    " WHERE kmaID <> @kmaID " +
            //    //    "  AND dateShiftStart IS NOT NULL " +
            //    //    "  AND dateShiftEnd IS NULL ", new {_kmaID });
            //    if (KnitWorkingShiftSmenData != null)
            //    {
            //        await _logger.LogEventAsync($"Получены данные KnitWorkingShiftSmen", "LoadKnitWorkingShiftSmenDataAsync");

            //        await this.InvokeAsync(() =>
            //        {
            //            //_currentMlOpData = mlOpData;                // Обновляем текущую модель
            //            //_mlOpBindingSource.DataSource = _currentMlOpData; // Привязываем данные к форме
            //            _knitWorkingShiftSmenBindingSource.DataSource = KnitWorkingShiftSmenData;
            //        });

            //        await _logger.LogEventAsync($"Данные KnitWorkingShiftSmen успешно загружены", "LoadKnitWorkingShiftSmenDataAsync");
            //        //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
            //        _knitWorkingShiftSmenBindingList.Add(KnitWorkingShiftSmenData[0]);
            //        _knitWorkingShiftSmenBindingSource.ResetBindings(false);
            //    }
            //    else
            //    {
            //        await _logger.LogEventAsync($"Не удалось найти данные KnitWorkingShiftSmen", "LoadKnitWorkingShiftSmenDataAsync");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных KnitWorkingShiftSmen");
            //}
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
                        //if (ButtonPreliminaryWd.Enabled && ButtonPreliminaryWd.Visible)
                        //    ButtonPreliminaryWd_Click_Internal(sender, e);
                        MessageBox.Show("Просмотр работы к подтверждению");
                        break;
                    case 2:
                        //Debug.WriteLine(ButtonEditWd.Enabled + " " + ButtonEditWd.Visible);
                        //if (ButtonEditWd.Enabled && ButtonEditWd.Visible)
                        //    if (ButtonEditOnlyAdv.Enabled && ButtonEditOnlyAdv.Visible)
                        //        await EditWd_Internal2(ANNgridView, _bindingList, _bindingSource, Editing: true);
                        //    else
                        //        await EditWd_Internal2(ANNgridView, _bindingList, _bindingSource, Editing: false);
                        MessageBox.Show("История по операции");
                        break;
                    case 4:
                        //Debug.WriteLine(customSimpleButton1.Enabled + " " + customSimpleButton1.Visible);
                        //if (ButtonDouble.Enabled && ButtonDouble.Visible)
                        //    await DuplicateWorkDivision_Click_Internal(ANNgridView, _bindingList, _bindingSource);
                        MessageBox.Show("Выгрузить операции в XLS");
                        break;
                    case 6:
                        //Debug.WriteLine(ButtonArchAndCopyWd.Enabled + " " + ButtonArchAndCopyWd.Visible);
                        //if (ButtonArchAndCopyWd.Enabled && ButtonArchAndCopyWd.Visible)
                        //    await SetArchiveStatus_Internal(sender, e);//МЕНЯЮ НА АРХИВ для Чирковой
                        //ArchAndCopy(ANNgridView, _bindingList, _bindingSource, false);
                        //MessageBox.Show("Загрузить операции");
                        gridViewRzvPachListByNom.FocusedColumn = gridViewRzvPachListByNom.Columns["data_paln"];
                        gridViewRzvPachListByNom.FocusedColumn = gridViewRzvPachListByNom.Columns["SyncSelection"];
                        //LoadPlanZagrVyazByZadanySelection();
                        await GridOverlayLoader.RunTaskWithOverlayAsync(
                            gridControlPZVOperList,
                            LoadPlanZagrVyazByZadanySelection
                            , CancellationToken.None
                            );
                        //gridViewPZVOperList.ExpandAllGroups();
                        break;
                    case 8:
                        //ClearSelectedPachList();
                        await GridOverlayLoader.RunTaskWithOverlayAsync(
                            gridControlPZVOperList,
                            ClearSelectedPachList
                            , CancellationToken.None
                            );
                        break;
                    //case 9:
                    //    //Debug.WriteLine(PrintButton.Enabled + " " + PrintButton.Visible);
                    //    //if (PrintButton.Enabled && PrintButton.Visible)
                    //    //    // Отчет технологической схемы разделения труда
                    //    //    PrintWorkDivisionScheme_Click(null, null);
                    //    break;
                    //case 11:
                    //    if (printButtonPlus.Enabled && printButtonPlus.Visible)
                    //        // Отчет технологической схемы разделения труда
                    //        //printButtonPlus_Click(null, null);
                    //    break;
                    case 10:
                        _pZVOperListByPachListBindingSource.Clear();
                        gridViewRzvPachListByNom.FocusedColumn = gridViewRzvPachListByNom.Columns["data_paln"];
                        gridViewRzvPachListByNom.FocusedColumn = gridViewRzvPachListByNom.Columns["SyncSelection"];
                        //LoadPlanZagrVyazByZadanySelection();
                        //await GridOverlayLoader.RunTaskWithOverlayAsync(
                        //    gridControlPZVOperList,
                        //    () => LoadPlanZagrVyazByZadanySelection(),
                        //    default);
                        await GridOverlayLoader.RunTaskWithOverlayAsync(
                            gridControlPZVOperList,
                            LoadPlanZagrVyazByZadanySelection
                            , CancellationToken.None
                            );
                        //gridViewPZVOperList.ExpandAllGroups();
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
                //if (jsonString != "[]")
                //{
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

                //_pZVOperListByPachListBindingSource.ResetBindings(false);
                gridViewPZVOperList.ExpandAllGroups();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}");
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
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}");
            }
        }
        private async void PlanZagrVyaz_Load(object sender, EventArgs e)
        {
            try
            {
                Task bindingsTask = InitializeBindingsAsync();
                await Task.WhenAll(bindingsTask);
                await LoadPlanTotalHoursByKnitMachineDataAsync();
                await LoadSmenZadanyVyazDataAsync();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке формы PlanZagrVyaz");
                MessageBox.Show($"Ошибка при загрузке формы PlanZagrVyaz: {ex.Message}");
            }
        }

        private async void gridViewPlanTotalHoursByKnitMachine_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                var selectedRow = _planTotalHoursByKnitMachineBindingSource.Current as PlanTotalHoursByKnitMachine;
                await LoadZadanyListByMachineNewDataAsync(selectedRow.kmlID);

                var changes = BindingSourceHelper.GetChanges<ZadanyListByMachine>(
                    _zadanyListByMachineBindingSource,
                    _zadanyListByMachineNewBindingSource,
                    HashMode.ExcludeOnly,
                    keyProperties: new[] { "kmlID", "pszNom" },
                    hashProperties: new[] { "SyncSelection" }
                );
                // обновить и добавить
                BindingSourceHelper.ApplyChanges<ZadanyListByMachine>(
                    _zadanyListByMachineBindingSource,
                    changes,
                    UpdateFieldsMode.ExcludeOnly,
                    keyProperties: new[] { "kmlID", "pszNom" },
                    fields: new[] { "SyncSelection" }
                );

                gridViewZadanyListByMachine.ActiveFilterString = $" kmlID == {selectedRow.kmlID}";
                gridControlZadanyListByMachine.ForceInitialize();
                gridViewZadanyListByMachine.RefreshData();

                //gridControlZadanyListByMachine.BeginInvoke(new Action(() =>
                //{
                //    if (gridViewZadanyListByMachine.RowCount <= 0) return;

                //    gridViewZadanyListByMachine.CloseEditor();
                //    gridViewZadanyListByMachine.UpdateCurrentRow();

                //    int first = gridViewZadanyListByMachine.GetVisibleRowHandle(0);

                //    // если первая уже была в фокусе — уйдём на другую и вернёмся,
                //    // чтобы FocusedRowChanged точно сработал
                //    if (gridViewZadanyListByMachine.FocusedRowHandle == first &&
                //        gridViewZadanyListByMachine.RowCount > 1)
                //    {
                //        int second = gridViewZadanyListByMachine.GetVisibleRowHandle(1);
                //        gridViewZadanyListByMachine.FocusedRowHandle = second;
                //    }

                //    gridViewZadanyListByMachine.FocusedRowHandle = first;
                //    gridViewZadanyListByMachine.ClearSelection();
                //    gridViewZadanyListByMachine.SelectRow(first);
                //}));
                gridControlZadanyListByMachine.BeginInvoke(new Action(() =>
                {
                    if (gridViewZadanyListByMachine.RowCount <= 0) return;

                    // 1) Отслеживаем, сработало ли событие реально
                    bool fired = false;
                    DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler probe = (s, ee) => fired = true;
                    gridViewZadanyListByMachine.FocusedRowChanged += probe;

                    // 2) Пробуем "честно" сменить фокус так, чтобы событие точно могло сработать
                    gridViewZadanyListByMachine.CloseEditor();
                    gridViewZadanyListByMachine.UpdateCurrentRow();

                    int oldHandle = gridViewZadanyListByMachine.FocusedRowHandle;
                    int first = gridViewZadanyListByMachine.GetVisibleRowHandle(0);

                    // если уже на первой и есть 2+ строк — уходим на вторую и возвращаемся
                    if (oldHandle == first && gridViewZadanyListByMachine.RowCount > 1)
                    {
                        int second = gridViewZadanyListByMachine.GetVisibleRowHandle(1);
                        gridViewZadanyListByMachine.FocusedRowHandle = second;
                    }

                    gridViewZadanyListByMachine.FocusedRowHandle = first;
                    gridViewZadanyListByMachine.ClearSelection();
                    gridViewZadanyListByMachine.SelectRow(first);

                    // 3) Снимаем "пробник"
                    gridViewZadanyListByMachine.FocusedRowChanged -= probe;

                    // 4) Если DevExpress НЕ вызвал событие (например, осталась 1 строка) —
                    //    честно гарантируем выполнение той же логики обновления 3-го грида
                    if (!fired)
                    {
                        gridViewZadanyListByMachine_FocusedRowChanged(
                            gridViewZadanyListByMachine,
                            new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(oldHandle, first)
                        );
                    }
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в gridViewPlanTotalHoursByKnitMachine_FocusedRowChanged: {ex.Message}");
            }
        }
        private void FocusFirstRow(GridView view)
        {
            view.CloseEditor();
            view.UpdateCurrentRow();

            if (view.RowCount <= 0) return;

            int first = view.GetVisibleRowHandle(0);
            view.FocusedRowHandle = first;

            view.ClearSelection();
            view.SelectRow(first);
        }
        private async Task RefreshRzvFromZadanyCurrentAsync()
        {
            var z = _zadanyListByMachineBindingSource.Current as ZadanyListByMachine;
            if (z == null) return;

            await LoadRzvPachListByNomNewDataAsync(z.nom, z.pszNom /* + остальные ключи, если нужны */);
        }
        private async void gridViewRzvPachListByNom_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            //var selectedRow = _zadanyListByMachineBindingSource.Current as ZadanyListByMachine;
            //if (selectedRow != null)
            //{
            //    if (selectedRow.Gradacia != 1)
            //    {
            //        repositoryItemCheckEdit5.ReadOnly = true;
            //    }
            //    else
            //    {
            //        repositoryItemCheckEdit5.ReadOnly = false;
            //    }
            //}
            //else
            //{
            //    await LoadPZVOperListByPachListNewDataAsync("", vyazPodrKod);
            //}
        }

        private async void gridViewZadanyListByMachine_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                if (_suppressZadanyFocusedChanged) return;

                var selectedRow = _zadanyListByMachineBindingSource.Current as ZadanyListByMachine;
                if (selectedRow != null)
                {
                    //MessageBox.Show(selectedRow.SyncSelection.ToString());
                    //MessageBox.Show(gridViewZadanyListByMachine.IsCellSelect.ToString());
                    await LoadRzvPachListByNomNewDataAsync(selectedRow.nom, selectedRow.pszNom);
                    var changes = BindingSourceHelper.GetChanges<RzvPachListByNom>(
                        _rzvPachListByNomBindingSource,
                        _rzvPachListByNomNewBindingSource,
                        HashMode.ExcludeOnly,
                        keyProperties: new[] { "nomZad", "nom", "nom_n" },
                        hashProperties: new[] { "SyncSelection" }
                    );
                    // обновить и добавить
                    //BindingSourceHelper.ApplyChanges(_smenZadanyVyazBindingSource, changes, x => (x.kwsmlKmlID, x.typeID));
                    BindingSourceHelper.ApplyChanges<RzvPachListByNom>(
                        _rzvPachListByNomBindingSource,
                        changes,
                        UpdateFieldsMode.ExcludeOnly,
                        keyProperties: new[] { "nomZad", "nom", "nom_n" },
                        fields: new[] { "SyncSelection" }
                    );
                    //// удалить отсутствующие
                    //BindingSourceHelper.RemoveMissing(_rzvPachListByNomBindingSource, changes.Removed);

                    if (selectedRow.Gradacia != 1)
                    {
                        gridColumnRzvPachListByNomGradacia.OptionsColumn.ReadOnly = true;
                    }
                    else
                    {
                        gridColumnRzvPachListByNomGradacia.OptionsColumn.ReadOnly = false;
                    }
                }
                else
                {
                    int myVersion = ++_rzvLoadVersion;
                    if (myVersion != _rzvLoadVersion) return;
                    await LoadRzvPachListByNomNewDataAsync(0, "");
                    //await RefreshRzvFromZadanyCurrentAsync();
                }
                //var missingRecords = _rzvPachListByNomNewBindingSource.List.Cast<dynamic>()
                //    .Where(newRec => newRec != null &&
                //           !_rzvPachListByNomBindingSource.List.Cast<dynamic>()
                //            .Where(oldRec => oldRec != null)
                //            .Any(oldRec => oldRec.nomZad == newRec.nomZad && oldRec.nom == newRec.nom))
                //    .ToList();
                //foreach (var record in missingRecords)
                //{
                //    //// Добавляем строку данных
                //    _rzvPachListByNomBindingSource.Add(record);
                //}


                gridViewRzvPachListByNom.ActiveFilterString = $" nomZad == '{selectedRow.pszNom}' and nom == {selectedRow.nom}";
                gridControlRzvPachListByNom.ForceInitialize();
                gridViewRzvPachListByNom.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в gridViewZadanyListByMachine_FocusedRowChanged: {ex.Message}");
            }

        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    gridViewZadanyListByMachine.FocusedColumn = gridViewZadanyListByMachine.Columns["data_plan"];
            //    gridViewZadanyListByMachine.FocusedColumn = gridViewZadanyListByMachine.Columns["SyncSelection"];
            //    //MessageBox.Show(gridViewZadanyListByMachine.GetRowCellValue(gridViewZadanyListByMachine.FocusedRowHandle, "SyncSelection").ToString());
            //    var selectedRow = _zadanyListByMachineBindingSource.Current as ZadanyListByMachine;
            //    var recordsToUpdate = _rzvPachListByNomBindingSource.List
            //        .Cast<RzvPachListByNom>()
            //        .Where(record => record != null &&
            //               record.nomZad == selectedRow.pszNom &&
            //               record.nom == selectedRow.nom)
            //        .ToList();
            //    int isSelected = Convert.ToInt32(gridViewZadanyListByMachine.GetRowCellValue(gridViewZadanyListByMachine.FocusedRowHandle, "SyncSelection"));
            //    foreach (var record in recordsToUpdate)
            //    {
            //        record.SyncSelection = isSelected;
            //    }

            //    _zadanyListByMachineBindingSource.ResetBindings(false);
            //    gridViewZadanyListByMachine.RefreshData();
            //    _rzvPachListByNomBindingSource.ResetBindings(false);
            //    gridViewRzvPachListByNom.RefreshData();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Ошибка обновления: {ex.Message}");
            //}
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
                //int isSelected = Convert.ToInt32(gridViewZadanyListByMachine.GetRowCellValue(gridViewZadanyListByMachine.FocusedRowHandle, "SyncSelection"));
                foreach (var record in recordsToUpdate)
                {
                    //MessageBox.Show(gridViewZadanyListByMachine.GetRowCellValue(gridViewZadanyListByMachine.FocusedRowHandle, "SyncSelection").ToString());
                    //record.SyncSelection = (int)gridViewZadanyListByMachine.GetRowCellValue(gridViewZadanyListByMachine.FocusedRowHandle, "SyncSelection");
                    record.SyncSelection = 0;
                }
                gridViewRzvPachListByNom.PostEditor();
                gridViewRzvPachListByNom.UpdateCurrentRow();
                _rzvPachListByNomBindingSource.ResetBindings(false);
                gridViewRzvPachListByNom.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления списка пачек: {ex.Message}");
            }

            try
            {
                var recordsToUpdate = _zadanyListByMachineBindingSource.List
                    .Cast<ZadanyListByMachine>()
                    .Where(record => record != null &&
                           record.SyncSelection == 1)
                    .ToList();
                //int isSelected = Convert.ToInt32(gridViewZadanyListByMachine.GetRowCellValue(gridViewZadanyListByMachine.FocusedRowHandle, "SyncSelection"));
                foreach (var record in recordsToUpdate)
                {
                    //MessageBox.Show(gridViewZadanyListByMachine.GetRowCellValue(gridViewZadanyListByMachine.FocusedRowHandle, "SyncSelection").ToString());
                    //record.SyncSelection = (int)gridViewZadanyListByMachine.GetRowCellValue(gridViewZadanyListByMachine.FocusedRowHandle, "SyncSelection");
                    record.SyncSelection = 0;
                }
                gridViewZadanyListByMachine.PostEditor();
                gridViewZadanyListByMachine.UpdateCurrentRow();
                _zadanyListByMachineBindingSource.ResetBindings(false);
                gridViewZadanyListByMachine.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления списка заданий: {ex.Message}");
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


                //foreach (GridColumn column in gridViewPZVOperList.Columns)
                //{
                //    if (!column.Visible) continue; // Пропускаем скрытые колонки

                //    //column.OptionsFilter.AllowAutoFilter = true;
                //    //column.OptionsFilter.AllowFilter = true;
                //    MessageBox.Show($"{column.Name}.OptionsFilter.AllowAutoFilter = {column.OptionsFilter.AllowAutoFilter}");
                //    MessageBox.Show($"{column.Name}.OptionsFilter.AllowFilter = {column.OptionsFilter.AllowFilter}");
                //    MessageBox.Show($"{column.Name}.OptionsColumn.AllowSort = {column.OptionsColumn.AllowSort}");
                //    MessageBox.Show($"{column.Name}.OptionsFilter.AutoFilterCondition = {column.OptionsFilter.AutoFilterCondition}");
                //    MessageBox.Show($"{column.Name}.OptionsFilter.FilterPopupMode = {column.OptionsFilter.FilterPopupMode}");
                //}

                //AutoRowFilterConfigForm(gridViewPZVOperList);

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
            //try
            //{
            //    int buttonIndex = ((DevExpress.XtraTab.XtraTabControl)sender).CustomHeaderButtons.IndexOf(e.Button);
            //    switch (buttonIndex)
            //    {
            //        case 0:
            //            //Debug.WriteLine(ButtonPreliminaryWd.Enabled + " " + ButtonPreliminaryWd.Visible);
            //            //if (ButtonPreliminaryWd.Enabled && ButtonPreliminaryWd.Visible)
            //            //    ButtonPreliminaryWd_Click_Internal(sender, e);
            //            switch (customTabControl1.SelectedTabPageIndex)
            //            {
            //                case 0:
            //                    //MessageBox.Show("Обновление грида на вкладке xtraTabPage1");
            //                    await LoadSmenZadanyVyazMachineDataAsync();
            //                    //gridViewSmenZadanyVyazMachine.ExpandAllGroups();
            //                    break;
            //                case 1:
            //                    //MessageBox.Show("Обновление грида на вкладке xtraTabPage3");
            //                    await LoadSmenZadanyVyazEmpDataAsync();
            //                    //gridViewSmenZadanyVyazEmp.ExpandAllGroups();
            //                    break;
            //                default:
            //                    MessageBox.Show("Обновление грида на вкладке ???");
            //                    break;
            //            }
            //            break;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Ошибка в customTabControl1_CustomHeaderButtonClick: {ex.Message}");
            //}
        }

        //private async void customTabControl1_CustomHeaderButtonClick(object sender, DevExpress.XtraTab.ViewInfo.CustomHeaderButtonEventArgs e)
        //{
        //    try
        //    {
        //        int buttonIndex = ((DevExpress.XtraTab.XtraTabControl)sender).CustomHeaderButtons.IndexOf(e.Button);
        //        switch (buttonIndex)
        //        {
        //            case 0:
        //                //Debug.WriteLine(ButtonPreliminaryWd.Enabled + " " + ButtonPreliminaryWd.Visible);
        //                //if (ButtonPreliminaryWd.Enabled && ButtonPreliminaryWd.Visible)
        //                //    ButtonPreliminaryWd_Click_Internal(sender, e);
        //                switch (customTabControl1.SelectedTabPageIndex)
        //                {
        //                    case 0:
        //                        //MessageBox.Show("Обновление грида на вкладке xtraTabPage1");
        //                        await LoadSmenZadanyVyazMachineDataAsync();
        //                        //gridViewSmenZadanyVyazMachine.ExpandAllGroups();
        //                        break;
        //                    case 1:
        //                        //MessageBox.Show("Обновление грида на вкладке xtraTabPage3");
        //                        await LoadSmenZadanyVyazEmpDataAsync();
        //                        //gridViewSmenZadanyVyazEmp.ExpandAllGroups();
        //                        break;
        //                    default:
        //                        MessageBox.Show("Обновление грида на вкладке ???");
        //                        break;
        //                }
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка в customTabControl1_CustomHeaderButtonClick: {ex.Message}");
        //    }
        //}

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
                        //GoToPzvID(_xPzvID, _xColumn);
                        //_gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
                        LoadSmenZadanyVyazDataAsync();
                    }
                    else
                    {
                        KnittingMachineCancelWorkAssignment();
                        //GoToPzvID(_xPzvID, _xColumn);
                        //_gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
                        LoadSmenZadanyVyazDataAsync();
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
                        //GoToPzvID(_xPzvID, _xColumn);
                        //_gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
                        LoadSmenZadanyVyazDataAsync();
                    }
                    else
                    {
                        TabCancelWorkAssignment();
                        //GoToPzvID(_xPzvID, _xColumn);
                        //_gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
                        LoadSmenZadanyVyazDataAsync();
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
                        //GoToPzvID(_xPzvID, _xColumn);
                        //_gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
                        LoadSmenZadanyVyazDataAsync();
                    }
                    else
                    {
                        CancelWorkStartExecution();
                        //GoToPzvID(_xPzvID, _xColumn);
                        //_gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
                        LoadSmenZadanyVyazDataAsync();
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
                        //GoToPzvID(_xPzvID, _xColumn);
                        //_gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
                        LoadSmenZadanyVyazDataAsync();
                    }
                    else
                    {
                        CancelWorkStopExecution();
                        //GoToPzvID(_xPzvID, _xColumn);
                        //_gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
                        LoadSmenZadanyVyazDataAsync();
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
                        //GoToPzvID(_xPzvID, _xColumn);
                        //_gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
                        LoadSmenZadanyVyazDataAsync();
                    }
                    else
                    {
                        MasterCancelConfirmation();
                        //GoToPzvID(_xPzvID, _xColumn);
                        //_gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
                        LoadSmenZadanyVyazDataAsync();
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
                MessageBox.Show($"Ошибка заполнения даты начала вязания: {ex.Message}");
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
                MessageBox.Show($"Ошибка заполнения даты окончания вязания: {ex.Message}");
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
                MessageBox.Show($"Ошибка заполнения даты подтверждения мастером: {ex.Message}");
                throw;
            }
        }

        private void SetKnitMachineToPzvID(List<int> _pzvId, int _kmlID)
        {
            //MessageBox.Show($"Двойной клик по операции. В/М {curr.kmlNumber} ({curr.kmlInvNum}) - Зона {curr.kmaNumber}");
            try
            {
                //if (customTabControl1.SelectedTabPageIndex != 0)
                //{
                //    MessageBox.Show("Перейдите на вкладку 'маш/час' блока Сменное задание и выберите В/М!");
                //    return;
                //}
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
                MessageBox.Show($"Ошибка присвоения машины: {ex.Message}");
                throw;
            }

        }

        private void gridViewPZVOperList_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            ////if (e.Clicks == 2 && e.Button == MouseButtons.Left &&
            ////e.RowHandle >= 0 && e.Column == gridColumnPZVOperListOlKmlNumber)
            ////{
            ////    var view = (GridView)sender;

            ////    var kmlNumber = view.GetRowCellValue(e.RowHandle, gridColumnPZVOperListOlKmlNumber)?.ToString();

            ////    // Берём pzvID из нужной колонки в текущей строке
            ////    var pzvIdObj = view.GetRowCellValue(e.RowHandle, gridColumnPZVOperListOlPzvID);
            ////    int pzvId = pzvIdObj == null || pzvIdObj == DBNull.Value ? 0 : Convert.ToInt32(pzvIdObj);

            ////    HandleOlKmlDoubleClick(pzvId, kmlNumber);
            ////}

            //if (e.Clicks == 2 && e.Button == MouseButtons.Left &&
            //    e.RowHandle >= 0 && e.Column == gridColumnPZVOperListOlKmlNumber)
            //{
            //    var view = (GridView)sender;

            //    string kmlNumber = view.GetRowCellValue(e.RowHandle, gridColumnPZVOperListOlKmlNumber)?.ToString();

            //    var pzvIdObj = view.GetRowCellValue(e.RowHandle, gridColumnPZVOperListOlPzvID);
            //    int pzvId = pzvIdObj == null || pzvIdObj == DBNull.Value ? 0 : Convert.ToInt32(pzvIdObj);

            //    HandleOlKmlDoubleClick(pzvId, kmlNumber);
            //}
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
                SmenZadanyVyaz curr = _smenZadanyVyazBindingSource.Current as SmenZadanyVyaz;
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
                        int _kmlID = _dbHelper.ExecuteScalar($"SELECT pszkmKmlID " +
                            $"FROM plan_sezon_zad_knitMachine pszm " +
                            $"WHERE pszm.pszkmPszNom = '{record.olNomZad}' and pszkmKnitClass = {record.olIdVyazClass}");
                        if (_kmlID != curr.kwsmlKmlID && record.olPzvKmlID == 0)
                        {
                            var _result = MessageBox.Show(
                                "Внимание! Назначаемая машина не совпадает с плановой. Продолжить?", 
                                "", 
                                MessageBoxButtons.YesNo, 
                                MessageBoxIcon.Warning);
                            if (_result == DialogResult.No)
                            {
                                return;
                            }
                        }
                        //if (record.olPzvTab != 0)
                        //{
                        //    MessageBox.Show("Операция уже назначена работнику, нельзя изменить В/М!");
                        //    return;
                        //}
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
                //SmenZadanyVyazMachine curr = _smenZadanyVyazMachineBindingSource.Current as SmenZadanyVyazMachine;
                //SmenZadanyVyaz curr = _smenZadanyVyazBindingSource.Current as SmenZadanyVyaz;
                //if (curr == null || curr.kwsmlKmlID == null || curr.kwsmlKmlID == 0)
                //{
                //    MessageBox.Show("Не выбрана машина для назначения");
                //    return;
                //}
                SetKnitMachineToPzvID(filteredList, curr.kwsmlKmlID);
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

                LoadSmenZadanyVyazNewDataAsync(1);
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
                //GoToPzvID(_xPzvID, _xColumn);
                //_gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, _pZVOperListByPachListBindingSource, x => x.olPzvID, _xPzvID, _xColumn);
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
                        //if (record.olPzvTab != 0)
                        //{
                        //    MessageBox.Show("Операция уже назначена работнику, нельзя изменить В/М!");
                        //    return;
                        //}
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
                //GoToPzvID(_xPzvID, _xColumn);
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
            var checkList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .ToList();
            if (checkList.Count != 0)
            {
                foreach (var record in checkList)
                {
                    //if (record.olPzvTab != 0)
                    //{
                    //    MessageBox.Show("Операция уже назначена работнику, нельзя изменить В/М!");
                    //    return;
                    //}
                    //if (!CheckPZVDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя проставить таб№ 999 (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                    //{
                    //    record.ErrorSelection = 1;
                    //    record.SyncSelection = 0;
                    //}
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

            PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

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
        private async Task SetTabToPzvID(List<int> _pzvId, int _tab, int _kwsID)
        {
            //MessageBox.Show($"Двойной клик по операции. В/М {curr.kmlNumber} ({curr.kmlInvNum}) - Зона {curr.kmaNumber}");
            try
            {
                //if (customTabControl1.SelectedTabPageIndex != 1 && _tab != 999 && _tab != 0)
                //{
                //    MessageBox.Show("Перейдите на вкладку 'чел/час' блока Сменное задание и выберите работника!");
                //    return;
                //}

                // получаем список строк (pzvID) для присовения машины (kmlID)
                var idSet = _pzvId != null ? new HashSet<int>(_pzvId) : new HashSet<int>();

                var recordsToUpdate = _pZVOperListByPachListBindingSource.List
                    .Cast<PZVOperList>()
                    .Where(r => r != null && idSet.Contains(r.olPzvID))
                    .ToList();
                foreach (var record in recordsToUpdate)
                {
                    record.olPzvTab = _tab;
                    record.olPzvKwsID = _kwsID;
                    record.olPzvDateNaznTab = _tab == 0 ? null : DateTime.Now;
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
                        // получить все изменённые элементы
                        //var toRemove = _pZVOperListByPachListBindingSource.List
                        //    .OfType<PZVOperList>()
                        //    .Where(x => x.IsModified)
                        //    .ToList();

                        // удалить из BindingSource
                        //_pZVOperListByPachListBindingSource.RemoveModified<PZVOperList>();
                        // MessageBox.Show("0");
                        //_pZVOperListByPachListBindingSource.RemoveWhere<PZVOperList>(x => x.IsModified == true);
                        //if (this.InvokeRequired)
                        //    this.Invoke(new Action(() => _pZVOperListByPachListBindingSource.RemoveWhere<PZVOperList>(x => x.IsModified)));
                        //else
                        //    _pZVOperListByPachListBindingSource.RemoveWhere<PZVOperList>(x => x.IsModified);
                        try
                        {
                            _pZVOperListByPachListBindingSource.EndEdit();
                            PZVOperList[] toRemove = _pZVOperListByPachListBindingSource.List
                                .OfType<PZVOperList>()
                                .Where(x => x.IsModified)
                                .ToArray();
                            Debug.WriteLine(toRemove.Length);
                            Debug.WriteLine(toRemove.Where(x => x.IsModified));
                            //_pZVOperListByPachListBindingSource.RemoveWhere<PZVOperList>(x => x.IsModified);
                            //_pZVOperListByPachListBindingSource.RemoveModified<PZVOperList>();
                            foreach (var record in toRemove)
                            {
                                record.IsModified = false;
                                record.SyncSelection = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка при удалении");
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
                MessageBox.Show($"Ошибка присвоения табельного номера: {ex.Message}");
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
            string query = $"SELECT * " +
                            $"  FROM knitWorkingShiftNewCurrentSmen_view wsncsv " +
                            $"      LEFT JOIN knitWorkingShiftMachineListNew wsmln ON wsncsv.kwsID = wsmln.kwsmlKwsID" +
                            $"   WHERE wsncsv.shiftStatusID in (0,2) " +
                            $"      AND wsncsv.tabShiftStart = {_tab} " +
                            $"      AND wsmln.kwsmlKmlID = {_kmlID} ";
            _isCountTabKM = _dbHelper.Exists(query, new Dictionary<string, object> { });
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
                //SmenZadanyVyazEmp curr = _smenZadanyVyazEmpBindingSource.Current as SmenZadanyVyazEmp;
                //if (curr == null || curr.empTab == null || curr.empTab == 0)
                //{
                //    MessageBox.Show("Не выбранн работниик для назначения");
                //    return;
                //}
                //SetTabToPzvID(filteredList, curr.empTab);
                //await SetTabToPzvID(filteredList, curr.empTab);
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
            try
            {
                if (e.FocusedRowHandle < 0)
                    return;
                // --- Проверяем BindingSource перед обращением к Current ---
                if (_pZVOperListByPachListBindingSource == null)
                    return; // или MessageBox.Show("BindingSource не инициализирован");

                if (_pZVOperListByPachListBindingSource.Count == 0)
                    return; // список пуст

                if (_pZVOperListByPachListBindingSource.Position < 0)
                    return; // ничего не выбрано
                PZVOperList currPZV = _pZVOperListByPachListBindingSource.Current as PZVOperList;
                if (currPZV != null)
                {
                    Task artNormNTask = LoadArtNormNDataAsync(currPZV.olPzvAnnID);
                    Task normRaszTask = LoadNormRaszDataAsync(currPZV.olPzvNrID);
                    await Task.WhenAll(artNormNTask, normRaszTask);
                    gridViewArtNormN.RefreshData();
                    gridViewNormRasz.RefreshData();
                }
            }
            catch (Exception ex)
            { //MessageBox.Show(ex.ToString());
            }
        }

        private void layoutControlGroup1_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            //int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);
            //MessageBox.Show($"Нажата кнопка с индексом {buttonIndex} в layoutControlGroup1");

            //layoutControlGroup16.CustomHeaderButtons[0].Properties.Caption = "Скрыть информацию по делению накладной";

            //switch (buttonIndex)
            //{
            //    case 0:
            //        ShowNaklPart();
            //        break;
            //    case 2:
            //        PrintNakl();
            //        break;
            //}
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
                        // деление через сторно, когда операция уже выполнена и нужно изменить количество
                        // 1) пометить исходную строку
                        //currentItem.IsModified = true;
                        //int xKolNew = Convert.ToInt32(e.Value);
                        //int xKoldelta = currentItem.olKolCopy - Convert.ToInt32(e.Value);
                        //currentItem.olKol = currentItem.olKolCopy;  // обновлённое значение
                        //currentItem.olPzvDivision = 1;

                        //// 2) создать копию с исключениями и проставить нужные поля
                        //// строка для отрицательного значения
                        //var newItemNeg = ObjectCloneHelper.CloneWithExclusions(currentItem, clone =>
                        //{
                        //    clone.olPzvIDParent = currentItem.olPzvID;
                        //    clone.olPzvDivision = 1;
                        //    clone.IsNew = true;
                        //    clone.IsModified = false;
                        //    // 👇 сбрасываем "копию" перед присвоением нового olKol
                        //    clone.ResetOlKolCopy();

                        //    // присваиваем новое значение
                        //    //clone.olKol = Convert.ToInt32(e.Value);
                        //    clone.olKol = -1 * xKoldelta;  // новое значение в копии
                        //                                   //clone.olSekAll = Math.Round((clone.olKol * clone.olSekEd) / 3600m, 2);
                        //    clone.olPzvNChasi = (int)Math.Round((clone.olKol * clone.olSekEd) / 3600m);
                        //    // 👇 фиксируем новое значение как "оригинал" для этой строки
                        //    clone.RebaselineOlKolCopy();

                        //}, "olPzvID", "olKol", "olKolCopy", "olNChasi", "IsModified", "IsNew", "olPzvDivision", "olPzvIDParent");
                        //// 3) добавить биндинги
                        //_pZVOperListByPachListBindingSource.Add(newItemNeg);

                        //// строка для отрицательного значения
                        //var newItemPos = ObjectCloneHelper.CloneWithExclusions(currentItem, clone =>
                        //{
                        //    clone.olPzvIDParent = currentItem.olPzvID;
                        //    clone.olPzvDivision = 1;
                        //    clone.IsNew = true;
                        //    clone.IsModified = false;
                        //    // 👇 сбрасываем "копию" перед присвоением нового olKol
                        //    clone.ResetOlKolCopy();

                        //    // присваиваем новое значение
                        //    //clone.olKol = Convert.ToInt32(e.Value);
                        //    clone.olKol = xKoldelta;  // новое значение в копии
                        //                              //clone.olSekAll = Math.Round((clone.olKol * clone.olSekEd) / 3600m, 2);
                        //    clone.olPzvNChasi = (int)Math.Round((clone.olKol * clone.olSekEd) / 3600m);
                        //    // 👇 фиксируем новое значение как "оригинал" для этой строки
                        //    clone.RebaselineOlKolCopy();
                        //}, "olPzvID", "olKol", "olKolCopy", "olNChasi", "IsModified", "IsNew", "olPzvDivision", "olPzvIDParent"
                        //    , "olPzvTab", "olPzvDateNaznTab", "olPzvDateStart", "olPzvDateEnd", "olPzvDateML"
                        //    , "olSekNazn", "olKolNazn", "olChasNazn");
                        //// 3) добавить биндинги
                        //_pZVOperListByPachListBindingSource.Add(newItemPos);
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
                        // деление, если операция еще не назначена
                        //// 1) пометить исходную строку
                        //currentItem.IsModified = true;
                        ////currentItem.olSekAll = Math.Round((currentItem.olKol * currentItem.olSekEd) / 3600m, 2);
                        //currentItem.olPzvNChasi = (int)Math.Round((currentItem.olKol * currentItem.olSekEd) / 3600m);
                        //currentItem.olPzvDivision = 1;

                        //// 2) создать копию с исключениями и проставить нужные поля
                        //var newItem = ObjectCloneHelper.CloneWithExclusions(currentItem, clone =>
                        //{
                        //    clone.olPzvIDParent = currentItem.olPzvID;
                        //    clone.olPzvDivision = 1;
                        //    clone.IsNew = true;
                        //    clone.IsModified = false;
                        //    // 👇 сбрасываем "копию" перед присвоением нового olKol
                        //    clone.ResetOlKolCopy();

                        //    // присваиваем новое значение
                        //    //clone.olKol = Convert.ToInt32(e.Value);
                        //    clone.olKol = currentItem.olKolCopy - Convert.ToInt32(e.Value);  // новое значение в копии
                        //    clone.olPzvNChasi = (int)Math.Round((clone.olKol * currentItem.olSekEd) / 3600m);
                        //    // 👇 фиксируем новое значение как "оригинал" для этой строки
                        //    clone.RebaselineOlKolCopy();

                        //    //clone.olPzvIDParent = currentItem.olPzvID;
                        //    //clone.IsNew = true;
                        //    //clone.IsModified = false;
                        //    ////clone.olKol = Convert.ToInt32(e.Value);  // новое значение в копии
                        //    //clone.olKol = currentItem.olKolCopy - Convert.ToInt32(e.Value);  // новое значение в копии
                        //    ////clone.olKolCopy = clone.olKol;
                        //}, "olPzvID", "olKol", "olKolCopy", "olNChasi", "IsModified", "IsNew", "olPzvDivision", "olPzvIDParent"
                        //    , "olPzvTab", "olPzvDateNaznTab", "olPzvDateStart", "olPzvDateEnd", "olPzvDateML"
                        //    , "olSekNazn", "olKolNazn", "olChasNazn");
                        //// 3) добавить биндинги
                        //_pZVOperListByPachListBindingSource.Add(newItem);
                        string query = $"exec dbo.PZV_Split @pzvId = {currentItem.olPzvID}, @mode = 3, @qtyFact = {Convert.ToInt32(e.Value)} ";
                        Task updateRZV = _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { });
                        await Task.WhenAll(updateRZV);

                        await GridOverlayLoader.RunTaskWithOverlayAsync(
                            gridControlPZVOperList,
                            LoadPlanZagrVyazByZadanySelection
                            , CancellationToken.None
                            );
                    }

                    ////  x => x.IsNew || x.IsModified || x.IsDeleted
                    //// Получаем список строк с флагом IsModified = true
                    //List<PZV> filteredList = _pZVOperListByPachListBindingSource.List
                    //    .OfType<PZVOperList>()
                    //    .Where(x => x?.IsModified == true || x?.IsNew == true)
                    //    .Select(x => x.ToPZV())   // новый маппер
                    //    .ToList();
                    //// Преобразуем в BindingList
                    //if (filteredList.Count > 0)
                    //{
                    //    using (SqlConnection connection = _dbHelper.GetConnection())
                    //    {
                    //        _bulkHelper.BulkAllDataUpdate<PZV>(connection, filteredList, "planZagrVyaz", new[] { "pzvID" });
                    //        // удалить из BindingSource
                    //        _pZVOperListByPachListBindingSource.RemoveModified<PZVOperList>();
                    //        _pZVOperListByPachListBindingSource.RemoveNew<PZVOperList>();
                    //        //LoadPlanZagrVyazByZadanySelection();
                    //        Task loadPlanZagrVyazTask = LoadPlanZagrVyazByZadanySelection();
                    //        await Task.WhenAll(loadPlanZagrVyazTask);
                    //    }
                    //}

                    //// 3) обновить биндинги
                    ////_pZVOperListByPachListBindingSource.Add(newItem);
                    //_pZVOperListByPachListBindingSource.ResetBindings(false);
                    //gridViewPZVOperList.RefreshData();
                    ////// 4) сфокусироваться на новой строке
                    ////int newIndex = _pZVOperListByPachListBindingSource.Count - 1;
                    ////int newHandle = view.GetRowHandle(newIndex);
                    ////if (newHandle >= 0)
                    ////{
                    ////    view.FocusedRowHandle = newHandle;
                    ////    view.MakeRowVisible(newHandle);
                    ////    view.SelectRow(newHandle);
                    ////}

                    //GoToPzvID(xPzvID, _xColumn);
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
                //string query = $"update raskr_zeh_vyaz set gradacia = {selectedRow.gradacia} where pach_kod = '{selectedRow.pach_kod}'";
                //Task updateRZV = _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { });
                //await Task.WhenAll(updateRZV);

                // градация
                if (selectedRow != null)
                {
                    //int _xTypeAction = 0;
                    //_xTypeAction = selectedRow.gradacia;
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
                MessageBox.Show($"Ошибка обновления: {ex.Message}");
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
                        if (record.olKodPodr != 1 && !CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                        }
                        if (!CheckPZVDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                        }
                        if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                        }
                        if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
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
                //_pZVOperListByPachListBindingSource.ResetBindings(false);
                var checkList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .ToList();
                if (checkList.Count != 0)
                {
                    foreach (var record in checkList)
                    {
                        if (record.olKodPodr != 1 && !CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя начать выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя начать выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя начать выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                        }
                        if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя начать выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                        }
                        if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя начать выполнение (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
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
                        if (record.olKodPodr != 1 && !CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                        }
                        if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                        }
                        if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
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
                        if (record.olKodPodr != 1 && !CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateNaznTab", Convert.ToDateTime(record.olPzvDateNaznTab), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                        }
                        if (!CheckPZVEmptyDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
                        }
                        if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                        {
                            record.ErrorSelection = 1;
                            record.SyncSelection = 0;
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

        private async void buttonCancelWorkStartExecution_Click(object sender, EventArgs e)
        {
            try
            {
                await GridOverlayLoader.RunTaskWithOverlayAsync(
                    gridControlPZVOperList,
                    CancelWorkStartExecution,              // обычный void-метод
                    CancellationToken.None);
                //GoToPzvID(_xPzvID, _xColumn);
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
                //WorkStopExecution();
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
            //var view = (GridView)sender;
            //// если колонка gridColumnRzvPachListByNomGradacia редактировать запрещена:
            //if (view.FocusedColumn == gridColumnRzvPachListByNomGradacia &&
            //    gridColumnRzvPachListByNomGradacia.OptionsColumn.ReadOnly)
            //{
            //    //a.Cancel = true; // запретить редактирование
            //    MessageBox.Show("Внимание! Технологом не была определена необходимость градации - нельзя проставить признак градации на пачку!");
            //}
        }

        private void repositoryItemCheckEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    gridViewZadanyListByMachine.FocusedColumn = gridViewZadanyListByMachine.Columns["data_plan"];
            //    gridViewZadanyListByMachine.FocusedColumn = gridViewZadanyListByMachine.Columns["SyncSelection"];
            //    //MessageBox.Show(gridViewZadanyListByMachine.GetRowCellValue(gridViewZadanyListByMachine.FocusedRowHandle, "SyncSelection").ToString());
            //    var selectedRow = _zadanyListByMachineBindingSource.Current as ZadanyListByMachine;
            //    var recordsToUpdate = _rzvPachListByNomBindingSource.List
            //        .Cast<RzvPachListByNom>()
            //        .Where(record => record != null &&
            //               record.nomZad == selectedRow.pszNom &&
            //               record.nom == selectedRow.nom)
            //        .ToList();
            //    int isSelected = Convert.ToInt32(gridViewZadanyListByMachine.GetRowCellValue(gridViewZadanyListByMachine.FocusedRowHandle, "SyncSelection"));
            //    foreach (var record in recordsToUpdate)
            //    {
            //        record.SyncSelection = isSelected;
            //    }

            //    _zadanyListByMachineBindingSource.ResetBindings(false);
            //    gridViewZadanyListByMachine.RefreshData();
            //    _rzvPachListByNomBindingSource.ResetBindings(false);
            //    gridViewRzvPachListByNom.RefreshData();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Ошибка обновления: {ex.Message}");
            //}
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
                        await LoadSmenZadanyVyazDataAsync();
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
                        //switch (customTabControl1.SelectedTabPageIndex)
                        //{
                        //    case 0:
                        //        //MessageBox.Show("Обновление грида на вкладке xtraTabPage1");
                        //        await LoadSmenZadanyVyazMachineDataAsync();
                        //        //gridViewSmenZadanyVyazMachine.ExpandAllGroups();
                        //        break;
                        //    case 1:
                        //        //MessageBox.Show("Обновление грида на вкладке xtraTabPage3");
                        //        await LoadSmenZadanyVyazEmpDataAsync();
                        //        //gridViewSmenZadanyVyazEmp.ExpandAllGroups();
                        //        break;
                        //    default:
                        //        MessageBox.Show("Обновление грида на вкладке ???");
                        //        break;
                        //}
                        //break;
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
        private async void MoveRowToZone(AdvBandedGridView _view, int _rowHandle, int _kmaID, int _kwsID)
        {
            //MessageBox.Show($"Перенос в зону kmaID = ({_kmaID}) kwsID = {_kwsID}");

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

            //(row as SmenZadanyVyaz).kwsID = _kwsID;
            //(row as SmenZadanyVyaz).IsModified = true;
            //_smenZadanyVyazBindingSource.RemoveModified<SmenZadanyVyaz>();

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

            ////_gridHelper.GoToRowById<SmenZadanyVyaz, int>(gridViewSmenZadany, _smenZadanyVyazBindingSource, x => x.kwsmlKmlID, xKmlID);
            //gridViewSmenZadany.RefreshData();
            //if (_rowHandle >= 0 && gridViewSmenZadany.DataRowCount > 0)
            //{
            //    gridViewSmenZadany.FocusedRowHandle = _rowHandle;
            //}

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
            }));


            //else if (_rowHandle == 0 & gridViewSmenZadany.RowCount > 0)
            //{
            //    gridViewSmenZadany.FocusedRowHandle = _rowHandle + 1;
            //}

            ////_smenZadanyVyazBindingSource.ResetBindings(false);
            //var col = gridViewSmenZadany.Columns["kwsmlKmlID"];
            //int handle = gridViewSmenZadany.LocateByValue("kwsmlKmlID", xKmlID);
            //if (handle != DevExpress.XtraGrid.GridControl.InvalidRowHandle && handle >= 0 && handle < gridViewSmenZadany.RowCount)
            //{
            //    gridViewSmenZadany.FocusedRowHandle = handle;          // фокус на строку
            //    gridViewSmenZadany.MakeRowVisible(handle);            // прокрутить, чтобы строку было видно
            //    //gridViewSmenZadany.FocusedColumn = gridViewSmenZadany.VisibleColumns[0]; // опционально — фокус в первую колонку
            //    gridViewSmenZadany.FocusedColumn = bandedGridSmenZadanyColumnKmlNumber;
            //}
        }

        private async void advBandedGridViewSmenZadany_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            //var col = advBandedGridViewSmenZadany.Columns["chasNaznZadGroup"];
            //MessageBox.Show($"{col.ColumnType}");
            var currentSmenZadanyVyaz = _smenZadanyVyazBindingSource.Current as SmenZadanyVyaz;
            if (currentSmenZadanyVyaz == null)
                return;
            await LoadKnitWorkingShiftSmenToMoveDataAsync(currentSmenZadanyVyaz.kwsKmaID);
            int _xTab = currentSmenZadanyVyaz.kwsTabStart;
            int _xKmlID = currentSmenZadanyVyaz.kwsmlKmlID;
            int _groupLevel = gridViewNaryadZadany.GetRowLevel(e.FocusedRowHandle);

            //if (!new[] { 0, 1 }.Contains(_groupLevel))
            //{
            //    // level НЕ 0 и НЕ 1
            //}
            if (currentSmenZadanyVyaz.typeID == 1 && !gridViewNaryadZadany.IsGroupRow(e.FocusedRowHandle))
            {
                // наряд-задание по В/М
                _xTab = 0;
                //await LoadNaryadZadanyVyazDataAsync(currentSmenZadanyVyaz.kwsTabStart, 0);
            }
            else if (currentSmenZadanyVyaz.typeID == 2 || (gridViewNaryadZadany.IsGroupRow(e.FocusedRowHandle) && new[] { 0, 1 }.Contains(_groupLevel)))
            {
                // наряд-задание по таб№
                _xKmlID = 0;
                //await LoadNaryadZadanyVyazDataAsync(0, currentSmenZadanyVyaz.kwsmlKmlID);
            }
            await LoadNaryadZadanyVyazDataAsync(_xTab, _xKmlID);
            gridViewNaryadZadany.RefreshData();
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

        //private void PlanZagrVyaz_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    Application.Idle -= ExpandGroupsOnIdle;
        //    base.OnFormClosed(e);
        //}
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Application.Idle -= ExpandGroupsOnIdle;
            _loadCts?.Cancel();
            base.OnFormClosed(e);
        }

        private void layoutControlGroup7_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);
            //MessageBox.Show($"Нажата кнопка с индексом {buttonIndex} в layoutControlGroup1");

            //layoutControlGroup7.CustomHeaderButtons[0].Properties.Caption = "Скрыть информацию по делению накладной";

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

        private void layoutControlGroup9_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
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

        private void layoutControlGroup10_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
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

        private void layoutControlGroup15_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
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

        private void layoutControlGroup3_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
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
    }
}
