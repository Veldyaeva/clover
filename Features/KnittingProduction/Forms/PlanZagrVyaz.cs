using Dapper;
using DevExpress.CodeParser;
using DevExpress.Data;
using DevExpress.DataAccess.Sql;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils;
using DevExpress.Xpo.Helpers;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGauges.Core.Styles;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraLayout;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Tls;
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
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using DevExpress.Office.Import.OpenXml;


namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class PlanZagrVyaz : CustomForm, IThemeable
    {
        int vyazPodrKod = 0;

        private static DatabaseHelper _dbHelper;
        private static DbService _dbService;
        private static MlService _mlService;
        private static ArtNormRepository _anService;
        private static BulkHelper _bulkHelper;
        private static GridHelper _gridHelper;
        //        private static BindingSourceHelper _bSHelper;
        private readonly ILogger _logger = new FileLogger();
        private readonly VyazService _vyazService;
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

        private List<SmenZadanyVyazMachine> _currentSmenZadanyVyazMachineData = new List<SmenZadanyVyazMachine>();
        private List<SmenZadanyVyazMachine> smenZadanyVyazMachineData = new List<SmenZadanyVyazMachine>();
        private BindingList<SmenZadanyVyazMachine> _smenZadanyVyazMachineBindingList;
        private BindingSource _smenZadanyVyazMachineBindingSource;

        private List<SmenZadanyVyazEmp> _currentSmenZadanyVyazEmpData = new List<SmenZadanyVyazEmp>();
        private List<SmenZadanyVyazEmp> smenZadanyVyazEmpData = new List<SmenZadanyVyazEmp>();
        private BindingList<SmenZadanyVyazEmp> _smenZadanyVyazEmpBindingList;
        private BindingSource _smenZadanyVyazEmpBindingSource;
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
                var smenZadanyVyazEmpTask = Task.Run(() =>
                {
                    _smenZadanyVyazEmpBindingList = new BindingList<SmenZadanyVyazEmp>();
                    _smenZadanyVyazEmpBindingSource = new BindingSource { DataSource = _smenZadanyVyazEmpBindingList };
                });
                var smenZadanyVyazMachineTask = Task.Run(() =>
                {
                    _smenZadanyVyazMachineBindingList = new BindingList<SmenZadanyVyazMachine>();
                    _smenZadanyVyazMachineBindingSource = new BindingSource { DataSource = _smenZadanyVyazMachineBindingList };
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
                await Task.WhenAll(planTotalHoursByKnitMachineTask, zadanyListByMachineTask, zadanyListByMachineNewTask
                        , rzvPachListByNomTask, rzvPachListByNomNewTask
                        , pZVOperListByPachListTask, pZVOperListByPachListNewTask, smenZadanyVyazEmpTask
                        , smenZadanyVyazMachineTask
                        , artNormNTask, normRaszTask
                        , mlOpTask);

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
                _gridHelper.AutoRowFilterConfig(gridViewZadanyListByMachine as GridView, 0);
                gridViewZadanyListByMachine.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
                //gridViewRzvPachListByNom.OptionsView.ShowAutoFilterRow = false;
                //gridViewRzvPachListByNom.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
                //gridViewRzvPachListByNom.OptionsFilter.AllowFilterEditor = false;

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
                #endregion

                #region описание gridControlSmenZadanyVyazMachine "сменное задание по машинам"
                gridControlSmenZadanyVyazMachine.DataSource = _smenZadanyVyazMachineBindingSource;
                gridSmenZadanyVyazMachineColumnKmlKmaID.FieldName = "kmlKmaID";
                gridSmenZadanyVyazMachineColumnKmaNumber.FieldName = "kmaNumber";
                gridSmenZadanyVyazMachineColumnKmlID.FieldName = "kmlID";
                gridSmenZadanyVyazMachineColumnKmlNumber.FieldName = "kmlNumber";
                gridSmenZadanyVyazMachineColumnKmlInvNum.FieldName = "kmlInvNum";
                gridSmenZadanyVyazMachineColumnKmlIdVyazClass.FieldName = "kmlIdVyazClass";
                gridSmenZadanyVyazMachineColumnNameClass.FieldName = "name_class";
                gridSmenZadanyVyazMachineColumnTaskToDo.FieldName = "taskToDo";
                gridSmenZadanyVyazMachineColumnTaskAtWork.FieldName = "taskAtWork";
                gridSmenZadanyVyazMachineColumnTaskDone.FieldName = "taskDone";
                gridSmenZadanyVyazMachineColumnTaskNotConfirmed.FieldName = "taskNotConfirmed";
                gridSmenZadanyVyazMachineColumnTaskConfirmed.FieldName = "taskConfirmed";
                gridSmenZadanyVyazMachineColumnTaskNotPlanned.FieldName = "taskNotPlanned";
                _gridHelper.AutoRowFilterConfig(gridViewSmenZadanyVyazMachine, 0);
                #endregion

                #region описание gridControlSmenZadanyVyazEmp "сменное задание по работникам"
                gridControlSmenZadanyVyazEmp.DataSource = _smenZadanyVyazEmpBindingSource;
                gridSmenZadanyVyazEmpColumnKmaID.FieldName = "kmaID";
                gridSmenZadanyVyazEmpColumnKmaNumber.FieldName = "kmaNumber";
                gridSmenZadanyVyazEmpColumnEmpTab.FieldName = "empTab";
                gridSmenZadanyVyazEmpColumnEmpFioSokr.FieldName = "empFioSokr";
                gridSmenZadanyVyazEmpColumnTaskToDo.FieldName = "taskToDo";
                gridSmenZadanyVyazEmpColumnTaskAtWork.FieldName = "taskAtWork";
                gridSmenZadanyVyazEmpColumnTaskDone.FieldName = "taskDone";
                gridSmenZadanyVyazEmpColumnTaskNotConfirmed.FieldName = "taskNotConfirmed";
                gridSmenZadanyVyazEmpColumnTaskConfirmed.FieldName = "taskConfirmed";
                gridSmenZadanyVyazEmpColumnKmaIDNazn.FieldName = "kmaIDNazn";
                _gridHelper.AutoRowFilterConfig(gridViewSmenZadanyVyazEmp, 0);
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

                //gridColumnPZVOperListSyncSelection.OptionsColumn.AllowEdit = true;

                gridViewPZVOperList.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
                // или
                // gridViewPZVOperList.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
                gridColumnPZVOperListSyncSelection.OptionsColumn.AllowEdit = true;
                gridColumnPZVOperListSyncSelection.OptionsColumn.ReadOnly = false;


                //AutoRowFilterConfigForm(gridViewPZVOperList as GridView);
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
                        _currentPlanTotalHoursByKnitMachineData = planTotalHoursByKnitMachineData;                // Обновляем текущую модель
                        _planTotalHoursByKnitMachineBindingSource.DataSource = _currentPlanTotalHoursByKnitMachineData; // Привязываем данные к форме
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
                        _currentZadanyListByMachineNewData = zadanyListByMachineNewData;                // Обновляем текущую модель
                        _zadanyListByMachineNewBindingSource.DataSource = _currentZadanyListByMachineNewData; // Привязываем данные к форме
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
                        _currentRzvPachListByNomNewData = rzvPachListByNomNewData;                // Обновляем текущую модель
                        _rzvPachListByNomNewBindingSource.DataSource = _currentRzvPachListByNomNewData; // Привязываем данные к форме
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

                if (pachList.Length > 0)
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
                        _currentPZVOperListByPachListNewData = pZVOperListByPachListNewData;                // Обновляем текущую модель
                        _pZVOperListByPachListNewBindingSource.DataSource = _currentPZVOperListByPachListNewData; // Привязываем данные к форме
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
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных PZVOperListByPachListNew");
            }
        }
        private async Task LoadSmenZadanyVyazMachineDataAsync()
        {
            try
            {
                _smenZadanyVyazMachineBindingSource.Clear();
                _smenZadanyVyazMachineBindingSource.ResetBindings(false);

                smenZadanyVyazMachineData = await _vyazService.GetSmenZadanyVyazMachine();


                if (smenZadanyVyazMachineData != null)
                {
                    await _logger.LogEventAsync($"Получены данные SmenZadanyVyazMachine", "LoadSmenZadanyVyazMachineDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentSmenZadanyVyazMachineData = smenZadanyVyazMachineData;                // Обновляем текущую модель
                        _smenZadanyVyazMachineBindingSource.DataSource = _currentSmenZadanyVyazMachineData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные SmenZadanyVyazMachine успешно загружены", "LoadSmenZadanyVyazMachineDataAsync");
                    //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
                    _smenZadanyVyazMachineBindingList.Add(smenZadanyVyazMachineData[0]);
                    //_pZVOperListByPachListNewBindingSource.Sort = "olPzvArticul, olNPach, olNo, olNpo, olPzvIDParent, olPzvID";
                    _pZVOperListByPachListNewBindingSource.ResetBindings(false);
                    gridViewSmenZadanyVyazMachine.ExpandAllGroups();
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
        private async Task LoadSmenZadanyVyazEmpDataAsync()
        {
            try
            {
                _smenZadanyVyazEmpBindingSource.Clear();
                _smenZadanyVyazEmpBindingSource.ResetBindings(false);

                smenZadanyVyazEmpData = await _vyazService.GetSmenZadanyVyazEmp();

                if (smenZadanyVyazEmpData != null)
                {
                    await _logger.LogEventAsync($"Получены данные SmenZadanyVyazEmp", "LoadSmenZadanyVyazEmpDataAsync");

                    await this.InvokeAsync(() =>
                    {
                        _currentSmenZadanyVyazEmpData = smenZadanyVyazEmpData;                // Обновляем текущую модель
                        _smenZadanyVyazEmpBindingSource.DataSource = _currentSmenZadanyVyazEmpData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные SmenZadanyVyazEmp успешно загружены", "LoadSmenZadanyVyazEmpDataAsync");
                    //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
                    _smenZadanyVyazEmpBindingList.Add(smenZadanyVyazEmpData[0]);
                    _smenZadanyVyazEmpBindingSource.ResetBindings(false);
                    gridViewSmenZadanyVyazEmp.ExpandAllGroups();
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные SmenZadanyVyazEmp", "LoadSmenZadanyVyazEmpDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных SmenZadanyVyazEmp");
            }
        }
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
                        _currentArtNormNData = artNormNData;                // Обновляем текущую модель
                        _artNormNBindingSource.DataSource = _currentArtNormNData; // Привязываем данные к форме
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
                        _currentNormRaszData = normRaszData;                // Обновляем текущую модель
                        _normRaszBindingSource.DataSource = _currentNormRaszData; // Привязываем данные к форме
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
                        _currentMlOpData = mlOpData;                // Обновляем текущую модель
                        _mlOpBindingSource.DataSource = _currentMlOpData; // Привязываем данные к форме
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
        private void layoutControlGroup6_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
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
                    LoadPlanZagrVyazByZadanySelection();
                    //gridViewPZVOperList.ExpandAllGroups();
                    break;
                case 8:
                    ClearSelectedPachList();
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
                    LoadPlanZagrVyazByZadanySelection();
                    //gridViewPZVOperList.ExpandAllGroups();
                    break;
            }
        }

        private async Task LoadPlanZagrVyazByZadanySelection()
        {
            #region
            //// Фильтруем записи где syncSelection = 1
            //var filteredRecords = _rzvPachListByNomBindingSource.Cast<object>()
            //    .Where(item =>
            //    {
            //        var property = item.GetType().GetProperty("SyncSelection");
            //        return property != null && Convert.ToInt32(property.GetValue(item)) == 1;
            //    })
            //    .ToList();

            //// Преобразуем в JSON
            //string jsonString = JsonConvert.SerializeObject(filteredRecords, Formatting.Indented);
            ////MessageBox.Show(jsonString);

            //await LoadPZVOperListByPachListNewDataAsync(jsonString, vyazPodrKod);

            //// для добавления выбираем только те записи, которые еще в текущем сеансе работы не загружались в _pZVOperListByPachListBindingSource
            //var missingRecords = _pZVOperListByPachListNewBindingSource.List.Cast<dynamic>()
            //    .Where(newRec => newRec != null &&
            //           !_pZVOperListByPachListBindingSource.List.Cast<dynamic>()
            //            .Where(oldRec => oldRec != null)
            //            .Any(oldRec => oldRec.olPzvID == newRec.olPzvID))
            //    .ToList();
            //foreach (var record in missingRecords)
            //{
            //    //// Добавляем строку данных
            //    _pZVOperListByPachListBindingSource.Add(record);
            //}
            //_pZVOperListByPachListBindingSource.ResetBindings(false);
            //gridViewPZVOperList.RefreshData();
            //----------------------------------------------------
            #endregion
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

                // Безопасное получение списков
                var oldList = _pZVOperListByPachListBindingSource.List?.Cast<dynamic>().Where(x => x != null).ToList() ?? new List<dynamic>();
                var newList = _pZVOperListByPachListNewBindingSource.List?.Cast<dynamic>().Where(x => x != null).ToList() ?? new List<dynamic>();

                // Добавляем новые записи
                var recordsToAdd = newList
                    .Where(newRec => !oldList.Any(oldRec =>
                        oldRec.olPzvID == newRec.olPzvID))
                    .ToList();
                if (recordsToAdd.Count != 0)
                {
                    foreach (var record in recordsToAdd)
                    {
                        _pZVOperListByPachListBindingSource.Add(record);
                    }
                }
                // Удаляем отсутствующие записи
                var recordsToRemove = _pZVOperListByPachListBindingSource.List.Cast<dynamic>()
                    .Where(oldRec => oldRec != null &&
                           !newList.Any(newRec => newRec != null &&
                                newRec.olPzvID == oldRec.olPzvID))
                    .ToList();

                // Удаляем через временный список
                if (recordsToRemove.Count != 0)
                {
                    foreach (var record in recordsToRemove)
                    {
                        _pZVOperListByPachListBindingSource.Remove(record);
                    }
                }

                //// Применяем фильтр
                //if (selectedRow != null)
                //{
                //    gridViewRzvPachListByNom.ActiveFilterString = $"nomZad == '{selectedRow.pszNom}' and nom == {selectedRow.nom}";
                //}

                _pZVOperListByPachListBindingSource.ResetBindings(false);
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

                // Безопасное получение списков
                var oldList = _pZVOperListByPachListBindingSource.List?.Cast<dynamic>().Where(x => x != null).ToList() ?? new List<dynamic>();
                var newList = _pZVOperListByPachListNewBindingSource.List?.Cast<dynamic>().Where(x => x != null).ToList() ?? new List<dynamic>();

                // Добавляем новые записи
                var recordsToAdd = newList
                    .Where(newRec => !oldList.Any(oldRec =>
                        oldRec.olPzvID == newRec.olPzvID))
                    .ToList();
                if (recordsToAdd.Count != 0)
                {
                    foreach (var record in recordsToAdd)
                    {
                        _pZVOperListByPachListBindingSource.Add(record);
                    }
                }
                // Удаляем отсутствующие записи
                var recordsToRemove = _pZVOperListByPachListBindingSource.List.Cast<dynamic>()
                    .Where(oldRec => oldRec != null &&
                           !newList.Any(newRec => newRec != null &&
                                newRec.olPzvID == oldRec.olPzvID))
                    .ToList();

                // Удаляем через временный список
                if (recordsToRemove.Count != 0)
                {
                    foreach (var record in recordsToRemove)
                    {
                        _pZVOperListByPachListBindingSource.Remove(record);
                    }
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
            Task bindingsTask = InitializeBindingsAsync();
            await Task.WhenAll(bindingsTask);
            await LoadPlanTotalHoursByKnitMachineDataAsync();
            await LoadSmenZadanyVyazMachineDataAsync();
            await LoadSmenZadanyVyazEmpDataAsync();
            //ConfigureTableViewAdvanced(gridViewPZVOperList as TableView);
            //ConfigureGridView(gridViewPZVOperList);
            //ConfigureTextColumnForPartialSearch(gridColumnPZVOperListOlOperName);
        }

        private async void gridViewPlanTotalHoursByKnitMachine_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            //DataRow _CurrRow = drNomListFoundRow[0]; // Take the first matching row (if there are multiple matches).
            //string _nlArticul = _CurrRow["nlArticul"].ToString();
            var selectedRow = _planTotalHoursByKnitMachineBindingSource.Current as PlanTotalHoursByKnitMachine;
            //MessageBox.Show(selectedRow.kmlID.ToString());
            //await LoadZadanyListByMachineDataAsync(selectedRow.kmlID);
            await LoadZadanyListByMachineNewDataAsync(selectedRow.kmlID);

            var missingRecords = _zadanyListByMachineNewBindingSource.List.Cast<dynamic>()
                .Where(newRec => newRec != null &&
                       !_zadanyListByMachineBindingSource.List.Cast<dynamic>()
                        .Where(oldRec => oldRec != null)
                        .Any(oldRec => oldRec.pszNom == newRec.pszNom && oldRec.nom == newRec.nom))
                .ToList();
            foreach (var record in missingRecords)
            {
                //// Добавляем строку данных
                _zadanyListByMachineBindingSource.Add(record);
            }
            gridViewZadanyListByMachine.ActiveFilterString = $" kmlID == {selectedRow.kmlID}";
            //gridViewZadanyListByMachine.OptionsView.ShowAutoFilterRow = false;
            //gridViewZadanyListByMachine.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            //gridViewZadanyListByMachine.OptionsFilter.AllowFilterEditor = false;
        }

        private async void gridViewRzvPachListByNom_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {

        }

        private async void gridViewZadanyListByMachine_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var selectedRow = _zadanyListByMachineBindingSource.Current as ZadanyListByMachine;
            if (selectedRow != null)
            {
                //MessageBox.Show(selectedRow.SyncSelection.ToString());
                //MessageBox.Show(gridViewZadanyListByMachine.IsCellSelect.ToString());
                await LoadRzvPachListByNomNewDataAsync(selectedRow.nom, selectedRow.pszNom);
            }
            else
            {
                await LoadRzvPachListByNomNewDataAsync(0, "");
            }
            var missingRecords = _rzvPachListByNomNewBindingSource.List.Cast<dynamic>()
                .Where(newRec => newRec != null &&
                       !_rzvPachListByNomBindingSource.List.Cast<dynamic>()
                        .Where(oldRec => oldRec != null)
                        .Any(oldRec => oldRec.nomZad == newRec.nomZad && oldRec.nom == newRec.nom))
                .ToList();
            foreach (var record in missingRecords)
            {
                //// Добавляем строку данных
                _rzvPachListByNomBindingSource.Add(record);
            }
            gridViewRzvPachListByNom.ActiveFilterString = $" nomZad == '{selectedRow.pszNom}' and nom == {selectedRow.nom}";


        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            gridViewZadanyListByMachine.FocusedColumn = gridViewZadanyListByMachine.Columns["data_plan"];
            gridViewZadanyListByMachine.FocusedColumn = gridViewZadanyListByMachine.Columns["SyncSelection"];
            //MessageBox.Show(gridViewZadanyListByMachine.GetRowCellValue(gridViewZadanyListByMachine.FocusedRowHandle, "SyncSelection").ToString());
            var selectedRow = _zadanyListByMachineBindingSource.Current as ZadanyListByMachine;
            try
            {
                var recordsToUpdate = _rzvPachListByNomBindingSource.List
                    .Cast<RzvPachListByNom>()
                    .Where(record => record != null &&
                           record.nomZad == selectedRow.pszNom &&
                           record.nom == selectedRow.nom)
                    .ToList();
                int isSelected = Convert.ToInt32(gridViewZadanyListByMachine.GetRowCellValue(gridViewZadanyListByMachine.FocusedRowHandle, "SyncSelection"));
                foreach (var record in recordsToUpdate)
                {
                    record.SyncSelection = isSelected;
                }

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

                _zadanyListByMachineBindingSource.ResetBindings(false);
                gridViewZadanyListByMachine.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления списка заданий: {ex.Message}");
            }

            LoadPlanZagrVyazByZadanySelection();

        }

        private void customSimpleButton8_Click(object sender, EventArgs e)
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

        private async void customTabControl1_CustomHeaderButtonClick(object sender, DevExpress.XtraTab.ViewInfo.CustomHeaderButtonEventArgs e)
        {
            int buttonIndex = ((DevExpress.XtraTab.XtraTabControl)sender).CustomHeaderButtons.IndexOf(e.Button);
            switch (buttonIndex)
            {
                case 0:
                    //Debug.WriteLine(ButtonPreliminaryWd.Enabled + " " + ButtonPreliminaryWd.Visible);
                    //if (ButtonPreliminaryWd.Enabled && ButtonPreliminaryWd.Visible)
                    //    ButtonPreliminaryWd_Click_Internal(sender, e);
                    switch (customTabControl1.SelectedTabPageIndex)
                    {
                        case 0:
                            //MessageBox.Show("Обновление грида на вкладке xtraTabPage1");
                            await LoadSmenZadanyVyazMachineDataAsync();
                            //gridViewSmenZadanyVyazMachine.ExpandAllGroups();
                            break;
                        case 1:
                            //MessageBox.Show("Обновление грида на вкладке xtraTabPage3");
                            await LoadSmenZadanyVyazEmpDataAsync();
                            //gridViewSmenZadanyVyazEmp.ExpandAllGroups();
                            break;
                        default:
                            MessageBox.Show("Обновление грида на вкладке ???");
                            break;
                    }
                    //

                    break;
                    //case 2:
                    //    //Debug.WriteLine(ButtonEditWd.Enabled + " " + ButtonEditWd.Visible);
                    //    //if (ButtonEditWd.Enabled && ButtonEditWd.Visible)
                    //    //    if (ButtonEditOnlyAdv.Enabled && ButtonEditOnlyAdv.Visible)
                    //    //        await EditWd_Internal2(ANNgridView, _bindingList, _bindingSource, Editing: true);
                    //    //    else
                    //    //        await EditWd_Internal2(ANNgridView, _bindingList, _bindingSource, Editing: false);
                    //    MessageBox.Show("История по операции");
                    //    break;
                    //case 4:
                    //    //Debug.WriteLine(customSimpleButton1.Enabled + " " + customSimpleButton1.Visible);
                    //    //if (ButtonDouble.Enabled && ButtonDouble.Visible)
                    //    //    await DuplicateWorkDivision_Click_Internal(ANNgridView, _bindingList, _bindingSource);
                    //    MessageBox.Show("Выгрузить операции в XLS");
                    //    break;
                    //case 6:
                    //    //Debug.WriteLine(ButtonArchAndCopyWd.Enabled + " " + ButtonArchAndCopyWd.Visible);
                    //    //if (ButtonArchAndCopyWd.Enabled && ButtonArchAndCopyWd.Visible)
                    //    //    await SetArchiveStatus_Internal(sender, e);//МЕНЯЮ НА АРХИВ для Чирковой
                    //    //ArchAndCopy(ANNgridView, _bindingList, _bindingSource, false);
                    //    //MessageBox.Show("Загрузить операции");
                    //    gridViewRzvPachListByNom.FocusedColumn = gridViewRzvPachListByNom.Columns["data_paln"];
                    //    gridViewRzvPachListByNom.FocusedColumn = gridViewRzvPachListByNom.Columns["SyncSelection"];
                    //    LoadPlanZagrVyazByZadanySelection();
                    //    break;
                    //case 8:
                    //    ClearSelectedPachList();
                    //    break;
            }
        }
        private bool CheckPZVDate(string _dateName, DateTime _dateValue, string _xMessage)
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
        private bool CheckPZVEmptyDate(string _dateName, DateTime _dateValue, string _xMessage)
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
                //case "OlPvDateMast":    // дата подтверждения мастером
                //    _detailMessage = "Операция уже подтверждена мастером";
                //    break;
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
        private async void gridViewPZVOperList_DoubleClick(object sender, EventArgs e)
        {
            var view = sender as GridView;
            if (view == null) return;

            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo hit = view.CalcHitInfo(pt);
            string _xColumn = view.FocusedColumn.ToString();
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
                if (tab != 0)
                {
                    MessageBox.Show("Операция уже назначена работнику, нельзя иизменить В/М!");
                    return;
                }
                if (!CheckPZVDate("OlPvDateNaznKm", _OlPvDateNaznKm, $"нельзя изменить В/М (пачка {_olNpach} операция {_olNomOper} {_olOperName.Trim()})")) return;
                if (!CheckPZVDate("OlPvDateStart", _OlPvDateStart, $"нельзя изменить В/М (пачка {_olNpach} операция {_olNomOper} {_olOperName.Trim()})")) return;
                if (!CheckPZVDate("OlPvDateEnd", _OlPvDateEnd, $"нельзя изменить В/М (пачка {_olNpach} операция {_olNomOper} {_olOperName.Trim()})")) return;
                if (!CheckPZVDate("OlPvDateMast", _OlPvDateMast, $"нельзя изменить В/М (пачка {_olNpach} операция {_olNomOper} {_olOperName.Trim()})")) return;
                SmenZadanyVyazMachine curr = _smenZadanyVyazMachineBindingSource.Current as SmenZadanyVyazMachine;
                if (curr == null || curr.kmlID == null || curr.kmlID == 0)
                {
                    MessageBox.Show("Не выбрана машина для назначения");
                    return;
                }
                // Значение ID машины из кликнутой ячейки
                int kmlID = Convert.ToInt32(view.GetRowCellValue(hit.RowHandle, gridColumnPZVOperListOlPzvKmlID));
                // Дополнительно: значение pzvID из колонки gridColumnPZVOperListOlPzvID в ЭТОЙ же строке
                var pzvIdObj = view.GetRowCellValue(hit.RowHandle, gridColumnPZVOperListOlPzvID);
                int pzvId = pzvIdObj == null || pzvIdObj == DBNull.Value ? 0 : Convert.ToInt32(pzvIdObj);

                var xPzvIDList = new List<int> { pzvId };
                SetKnitMachineToPzvID(xPzvIDList, kmlID != 0 ? 0 : curr.kmlID);
                GoToPzvID(pzvId, _xColumn);
            }

            if (hit.InRowCell && (hit.Column == gridColumnPZVOperListOlPzvTab || hit.Column == gridColumnPZVOperListOlPzvDateNaznTab) && hit.RowHandle >= 0)
            {
                SmenZadanyVyazEmp curr = _smenZadanyVyazEmpBindingSource.Current as SmenZadanyVyazEmp;
                if (curr == null || curr.empTab == null || curr.empTab == 0)
                {
                    MessageBox.Show("Не выбран работник для назначения");
                    return;
                }
                if (!CheckPZVDate("OlPvDateStart", _OlPvDateStart, "нельзя отменить назначение работнику")) return;
                if (!CheckPZVDate("OlPvDateEnd", _OlPvDateEnd, "нельзя отменить назначение работнику")) return;
                if (!CheckPZVDate("OlPvDateMast", _OlPvDateMast, "нельзя отменить назначение работнику")) return;
                
                if (!CheckPZVEmptyDate("OlPvDateNaznKm", _OlPvDateStart, "нельзя назначить работнику")) return;

                // Значение ID машины из кликнутой ячейки
                int pzvTab = Convert.ToInt32(view.GetRowCellValue(hit.RowHandle, gridColumnPZVOperListOlPzvTab));
                // Дополнительно: значение pzvID из колонки gridColumnPZVOperListOlPzvID в ЭТОЙ же строке
                var pzvIdObj = view.GetRowCellValue(hit.RowHandle, gridColumnPZVOperListOlPzvID);
                int pzvId = pzvIdObj == null || pzvIdObj == DBNull.Value ? 0 : Convert.ToInt32(pzvIdObj);

                var xPzvIDList = new List<int> { pzvId };
                //SetTabToPzvID(xPzvIDList, pzvTab != 0 ? 0 : curr.empTab);
                await SetTabToPzvID(xPzvIDList, pzvTab != 0 ? 0 : curr.empTab);
                GoToPzvID(pzvId, _xColumn);
            }

            if (hit.InRowCell && hit.Column == gridColumnPZVOperListOlPzvDateStart && hit.RowHandle >= 0)
            {
                if (!CheckPZVDate("OlPvDateEnd", _OlPvDateEnd, "нельзя отменить начало операции")) return;
                if (!CheckPZVDate("OlPvDateMast", _OlPvDateMast, "нельзя отменить начало операции")) return;

                if (!CheckPZVEmptyDate("OlPvDateNaznKm", _OlPvDateStart, "нельзя начать выполнение операции")) return;
                if (!CheckPZVEmptyDate("OlPzvDateNaznTab", _OlPvDateStart, "нельзя начать выполнение операции")) return;

                // Значение даты начала вязания из кликнутой ячейки
                var dateValue = view.GetRowCellValue(hit.RowHandle, gridColumnPZVOperListOlPzvDateStart);
                bool hasDate = dateValue != null && dateValue != DBNull.Value;
                DateTime? date = hasDate ? (DateTime?)null : DateTime.Now;
                // Дополнительно: значение pzvID из колонки gridColumnPZVOperListOlPzvID в ЭТОЙ же строке
                var pzvIdObj = view.GetRowCellValue(hit.RowHandle, gridColumnPZVOperListOlPzvID);
                int pzvId = pzvIdObj == null || pzvIdObj == DBNull.Value ? 0 : Convert.ToInt32(pzvIdObj);

                var xPzvIDList = new List<int> { pzvId };
                SetDateStartToPzvID(xPzvIDList, date);
                GoToPzvID(pzvId, _xColumn);
            }

            if (hit.InRowCell && hit.Column == gridColumnPZVOperListOlPzvDateEnd && hit.RowHandle >= 0)
            {
                if (!CheckPZVDate("OlPvDateMast", _OlPvDateMast, "нельзя отменить выполнение операции")) return;

                if (!CheckPZVEmptyDate("OlPvDateNaznKm", _OlPvDateStart, "нельзя завершить выполнение операции")) return;
                if (!CheckPZVEmptyDate("OlPzvDateNaznTab", _OlPvDateStart, "нельзя завершить выполнение операции")) return;
                if (!CheckPZVEmptyDate("OlPzvDateStart", _OlPvDateStart, "нельзя завершить выполнение операции")) return;

                // Значение даты начала вязания из кликнутой ячейки
                var dateValue = view.GetRowCellValue(hit.RowHandle, gridColumnPZVOperListOlPzvDateEnd);
                bool hasDate = dateValue != null && dateValue != DBNull.Value;
                DateTime? date = hasDate ? (DateTime?)null : DateTime.Now;
                // Дополнительно: значение pzvID из колонки gridColumnPZVOperListOlPzvID в ЭТОЙ же строке
                var pzvIdObj = view.GetRowCellValue(hit.RowHandle, gridColumnPZVOperListOlPzvID);
                int pzvId = pzvIdObj == null || pzvIdObj == DBNull.Value ? 0 : Convert.ToInt32(pzvIdObj);

                var xPzvIDList = new List<int> { pzvId };
                SetDateEndToPzvID(xPzvIDList, date);
                GoToPzvID(pzvId, _xColumn);
            }

            if (hit.InRowCell && hit.Column == gridColumnPZVOperListOlPzvDateMast && hit.RowHandle >= 0)
            {
                if (!CheckPZVEmptyDate("OlPvDateNaznKm", _OlPvDateStart, "нельзя подтвердить мастером")) return;
                if (!CheckPZVEmptyDate("OlPzvDateNaznTab", _OlPvDateStart, "нельзя подтвердить мастером")) return;
                if (!CheckPZVEmptyDate("OlPzvDateStart", _OlPvDateStart, "нельзя подтвердить мастером")) return;
                if (!CheckPZVEmptyDate("OlPzvDateTnd", _OlPvDateStart, "нельзя подтвердить мастером")) return;

                // Значение даты начала вязания из кликнутой ячейки
                var dateValue = view.GetRowCellValue(hit.RowHandle, gridColumnPZVOperListOlPzvDateMast);
                bool hasDate = dateValue != null && dateValue != DBNull.Value;
                DateTime? date = hasDate ? (DateTime?)null : DateTime.Now;
                // Дополнительно: значение pzvID из колонки gridColumnPZVOperListOlPzvID в ЭТОЙ же строке
                var pzvIdObj = view.GetRowCellValue(hit.RowHandle, gridColumnPZVOperListOlPzvID);
                int pzvId = pzvIdObj == null || pzvIdObj == DBNull.Value ? 0 : Convert.ToInt32(pzvIdObj);

                var xPzvIDList = new List<int> { pzvId };
                SetDateMastToPzvID(xPzvIDList, date);
                GoToPzvID(pzvId, _xColumn);
            }
        }
        private void GoToPzvID(int _pzvID, string _column)
        {
            int rowHandle = gridViewPZVOperList.LocateByValue("olPzvID", _pzvID);
            //int rowHandle = gridViewPZVOperList.LocateByValue("gridColumnPZVOperListOlPzvID", _pzvID);

            if (rowHandle >= 0)
            {
                gridViewPZVOperList.FocusedRowHandle = rowHandle;
                gridViewPZVOperList.MakeRowVisible(rowHandle);
                gridViewPZVOperList.FocusedColumn = gridViewPZVOperList.Columns[$"{_column}"];
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
                        _pZVOperListByPachListBindingSource.RemoveModified<PZVOperList>();
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
                        _pZVOperListByPachListBindingSource.RemoveModified<PZVOperList>();
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
                        _pZVOperListByPachListBindingSource.RemoveModified<PZVOperList>();
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
                if (customTabControl1.SelectedTabPageIndex != 0)
                {
                    MessageBox.Show("Перейдите на вкладку 'маш/час' блока Сменное задание и выберите В/М!");
                    return;
                }
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
                        //pZVOperListByPachListData.RemoveAll(x => x.IsModified);
                        // получить все изменённые элементы
                        //var toRemove = _pZVOperListByPachListBindingSource.List
                        //    .OfType<PZVOperList>()
                        //    .Where(x => x.IsModified)
                        //    .ToList();

                        // удалить из BindingSource
                        _pZVOperListByPachListBindingSource.RemoveModified<PZVOperList>();
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

        private void customSimpleButton2_Click(object sender, EventArgs e)
        {
            var checkList = _pZVOperListByPachListBindingSource.List
                .OfType<PZVOperList>()
                .Where(x => x.SyncSelection == 1)
                //.Select(x => x.olPzvID)   // новый маппер
                .ToList();
            if (checkList.Count != 0)
            {
                foreach (var record in checkList)
                {
                    if (record.olPzvTab != 0)
                    {
                        MessageBox.Show("Операция уже назначена работнику, нельзя иизменить В/М!");
                        return;
                    }
                    if (!CheckPZVDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя изменить В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                    {
                        record.ErrorSelection = 1;
                        record.SyncSelection = 0;
                    }
                    if (!CheckPZVDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя изменить В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                    {
                        record.ErrorSelection = 1;
                        record.SyncSelection = 0;
                    }
                    if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя изменить В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                    {
                        record.ErrorSelection = 1;
                        record.SyncSelection = 0;
                    }
                    if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя изменить В/М (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                    {
                        record.ErrorSelection = 1;
                        record.SyncSelection = 0;
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
            SmenZadanyVyazMachine curr = _smenZadanyVyazMachineBindingSource.Current as SmenZadanyVyazMachine;
            if (curr == null || curr.kmlID == null || curr.kmlID == 0)
            {
                MessageBox.Show("Не выбрана машина для назначения");
                return;
            }
            SetKnitMachineToPzvID(filteredList, curr.kmlID);
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

        private void customSimpleButton9_Click(object sender, EventArgs e)
        {
            
            
            List<int> filteredList = _pZVOperListByPachListBindingSource.List
                .OfType<PZVOperList>()
                .Where(x => x.SyncSelection == 1)
                .Select(x => x.olPzvID)   // новый маппер
                .ToList();
            //SmenZadanyVyazMachine curr = _smenZadanyVyazMachineBindingSource.Current as SmenZadanyVyazMachine;
            //if (curr == null || curr.kmlID == null || curr.kmlID == 0)
            //{
            //    MessageBox.Show("Не выбрана машина для назначения");
            //    return;
            //}
            SetKnitMachineToPzvID(filteredList, 0);
        }

        private async void customSimpleButton1_Click(object sender, EventArgs e)
        {
            string _xColumn = gridViewPZVOperList.FocusedColumn.ToString();
            PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

            List<int> filteredList = _pZVOperListByPachListBindingSource.List
                .OfType<PZVOperList>()
                .Where(x => x.SyncSelection == 1)
                .Select(x => x.olPzvID)   // новый маппер
                .ToList();
            //SmenZadanyVyazMachine curr = _smenZadanyVyazMachineBindingSource.Current as SmenZadanyVyazMachine;
            //if (curr == null || curr.kmlID == null || curr.kmlID == 0)
            //{
            //    MessageBox.Show("Не выбрана машина для назначения");
            //    return;
            //}
            //SetTabToPzvID(filteredList, 999);
            await SetTabToPzvID(filteredList, 999);
            //Task.Run(() => SetTabToPzvID(filteredList, 999).GetAwaiter().GetResult();
            gridViewPZVOperList.RefreshData();
            GoToPzvID(pzvCurrent.olPzvID, _xColumn);
        }

        private async Task SetTabToPzvID(List<int> _pzvId, int _tab)
        {
            //MessageBox.Show($"Двойной клик по операции. В/М {curr.kmlNumber} ({curr.kmlInvNum}) - Зона {curr.kmaNumber}");
            try
            {
                if (customTabControl1.SelectedTabPageIndex != 1 && _tab != 999 && _tab != 0)
                {
                    MessageBox.Show("Перейдите на вкладку 'чел/час' блока Сменное задание и выберите работника!");
                    return;
                }

                // получаем список строк (pzvID) для присовения машины (kmlID)
                var idSet = _pzvId != null ? new HashSet<int>(_pzvId) : new HashSet<int>();

                var recordsToUpdate = _pZVOperListByPachListBindingSource.List
                    .Cast<PZVOperList>()
                    .Where(r => r != null && idSet.Contains(r.olPzvID))
                    .ToList();
               foreach (var record in recordsToUpdate)
                {
                    record.olPzvTab = _tab;
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
                            _pZVOperListByPachListBindingSource.RemoveModified<PZVOperList>();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка при удалении");
                        }

                        //LoadPlanZagrVyazByZadanySelection();
                        // Создаем и ожидаем завершения асинхронной задачи
                        /*await*/
                        
                     //   MessageBox.Show("1");
                        LoadPlanZagrVyazByZadanySelectionAsync();
                        //MessageBox.Show("2");
                    }
                }
                //MessageBox.Show("3");
                _pZVOperListByPachListBindingSource.ResetBindings(false);
                //MessageBox.Show("4");
                gridViewPZVOperList.RefreshData();
                //MessageBox.Show("5");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка присвоения машины: {ex.Message}");
                throw;
            }

        }
        //private async Task SaveAsync() { await LoadPlanZagrVyazByZadanySelection(); }
        private void customSimpleButton3_Click(object sender, EventArgs e)
        {
            string _xColumn = gridViewPZVOperList.FocusedColumn.ToString();
            PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;

            List<int> filteredList = _pZVOperListByPachListBindingSource.List
                .OfType<PZVOperList>()
                .Where(x => x.SyncSelection == 1)
                .Select(x => x.olPzvID)   // новый маппер
                .ToList();
            SetDateMastToPzvID(filteredList, DateTime.Now);
            GoToPzvID(pzvCurrent.olPzvID, _xColumn);
        }

        private void customSimpleButton11_Click_1(object sender, EventArgs e)
        {
            List<int> filteredList = _pZVOperListByPachListBindingSource.List
                .OfType<PZVOperList>()
                .Where(x => x.SyncSelection == 1)
                .Select(x => x.olPzvID)   // новый маппер
                .ToList();
            SetDateMastToPzvID(filteredList, null);
        }

        private void customSimpleButton7_Click(object sender, EventArgs e)
        {

        }

        private void customSimpleButton5_Click(object sender, EventArgs e)
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
            // Получаем список строк с флагом IsModified = true
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
                    _pZVOperListByPachListBindingSource.RemoveModified<PZVOperList>();
                    LoadPlanZagrVyazByZadanySelection();
                }
            }
            _pZVOperListByPachListBindingSource.ResetBindings(false);
            gridViewPZVOperList.RefreshData();
        }

        private async void customSimpleButton8_Click_1(object sender, EventArgs e)
        {
            _pZVOperListByPachListBindingSource.ResetBindings(false);
            var checkList = _pZVOperListByPachListBindingSource.List
                .OfType<PZVOperList>()
                .Where(x => x.SyncSelection == 1)
                //.Select(x => x.olPzvID)   // новый маппер
                .ToList();
            if (checkList.Count != 0)
            {
                foreach (var record in checkList)
                {
                    if (!CheckPZVDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя изменить назначение работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                    {
                        record.ErrorSelection = 1;
                        record.SyncSelection = 0;
                    }
                    if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя изменить назначение работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                    {
                        record.ErrorSelection = 1;
                        record.SyncSelection = 0;
                    }
                    if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя изменить назначение работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                    {
                        record.ErrorSelection = 1;
                        record.SyncSelection = 0;
                    }
                    if (!CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateNaznKm), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
                    {
                        record.ErrorSelection = 1;
                        record.SyncSelection = 0;
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
            SmenZadanyVyazEmp curr = _smenZadanyVyazEmpBindingSource.Current as SmenZadanyVyazEmp;
            if (curr == null || curr.empTab == null || curr.empTab == 0)
            {
                MessageBox.Show("Не выбранн работниик для назначения");
                return;
            }
            //SetTabToPzvID(filteredList, curr.empTab);
            await SetTabToPzvID(filteredList, curr.empTab);
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

        private void customSimpleButton10_Click(object sender, EventArgs e)
        {
            //_pZVOperListByPachListBindingSource.ResetBindings(false);
            //var checkList = _pZVOperListByPachListBindingSource.List
            //    .OfType<PZVOperList>()
            //    .Where(x => x.SyncSelection == 1)
            //    //.Select(x => x.olPzvID)   // новый маппер
            //    .ToList();
            //if (checkList.Count != 0)
            //{
            //    foreach (var record in checkList)
            //    {
            //        if (!CheckPZVDate("OlPvDateStart", Convert.ToDateTime(record.olPzvDateStart), $"нельзя отменить назначение работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
            //        {
            //            record.ErrorSelection = 1;
            //            record.SyncSelection = 0;
            //        }
            //        if (!CheckPZVDate("OlPvDateEnd", Convert.ToDateTime(record.olPzvDateEnd), $"нельзя отменить назначение работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
            //        {
            //            record.ErrorSelection = 1;
            //            record.SyncSelection = 0;
            //        }
            //        if (!CheckPZVDate("OlPvDateMast", Convert.ToDateTime(record.olPzvDateMast), $"нельзя отменить назначение работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
            //        {
            //            record.ErrorSelection = 1;
            //            record.SyncSelection = 0;
            //        }
            //        if (!CheckPZVEmptyDate("OlPvDateNaznKm", Convert.ToDateTime(record.olPzvDateStart), $"нельзя назначить работнику (пачка {record.olNPach} операция {record.olNomOper} {record.olOperName.Trim()})"))
            //        {
            //            record.ErrorSelection = 1;
            //            record.SyncSelection = 0;
            //        }
            //    }
            //}
            try
            {
                List<int> filteredList = _pZVOperListByPachListBindingSource.List
                    .OfType<PZVOperList>()
                    .Where(x => x.SyncSelection == 1)
                    .Select(x => x.olPzvID)   // новый маппер
                    .ToList();
                //string _xColumn = gridViewPZVOperList.FocusedColumn.ToString();
                //PZVOperList pzvCurrent = _pZVOperListByPachListBindingSource.Current as PZVOperList;
                SetTabToPzvID(filteredList, 0);
            }
            catch
            (Exception ex)
            { Debug.WriteLine(ex.ToString()); }
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

            int xPzvID = currentItem.olPzvID;
            string _xColumn = gridViewPZVOperList.FocusedColumn.ToString();
            if (currentItem.olPzvDateEnd != null && currentItem.olPzvDateMast == null)
            {
                // деление через сторно, когда операция уже выполнена и нужно изменить количество
                // 1) пометить исходную строку
                currentItem.IsModified = true;
                int xKolNew = Convert.ToInt32(e.Value);
                int xKoldelta = currentItem.olKolCopy - Convert.ToInt32(e.Value);
                currentItem.olKol = currentItem.olKolCopy;  // обновлённое значение
                currentItem.olPzvDivision = 1;

                // 2) создать копию с исключениями и проставить нужные поля
                // строка для отрицательного значения
                var newItemNeg = ObjectCloneHelper.CloneWithExclusions(currentItem, clone =>
                {
                    clone.olPzvIDParent = currentItem.olPzvID;
                    clone.olPzvDivision = 1;
                    clone.IsNew = true;
                    clone.IsModified = false;
                    // 👇 сбрасываем "копию" перед присвоением нового olKol
                    clone.ResetOlKolCopy();

                    // присваиваем новое значение
                    //clone.olKol = Convert.ToInt32(e.Value);
                    clone.olKol = -1 * xKoldelta;  // новое значение в копии
                    //clone.olSekAll = Math.Round((clone.olKol * clone.olSekEd) / 3600m, 2);
                    clone.olPzvNChasi = (int)Math.Round((clone.olKol * clone.olSekEd) / 3600m);
                    // 👇 фиксируем новое значение как "оригинал" для этой строки
                    clone.RebaselineOlKolCopy();

                }, "olPzvID", "olKol", "olKolCopy", "olNChasi", "IsModified", "IsNew", "olPzvDivision", "olPzvIDParent");
                // 3) добавить биндинги
                _pZVOperListByPachListBindingSource.Add(newItemNeg);

                // строка для отрицательного значения
                var newItemPos = ObjectCloneHelper.CloneWithExclusions(currentItem, clone =>
                {
                    clone.olPzvIDParent = currentItem.olPzvID;
                    clone.olPzvDivision = 1;
                    clone.IsNew = true;
                    clone.IsModified = false;
                    // 👇 сбрасываем "копию" перед присвоением нового olKol
                    clone.ResetOlKolCopy();

                    // присваиваем новое значение
                    //clone.olKol = Convert.ToInt32(e.Value);
                    clone.olKol = xKoldelta;  // новое значение в копии
                    //clone.olSekAll = Math.Round((clone.olKol * clone.olSekEd) / 3600m, 2);
                    clone.olPzvNChasi = (int)Math.Round((clone.olKol * clone.olSekEd) / 3600m);
                    // 👇 фиксируем новое значение как "оригинал" для этой строки
                    clone.RebaselineOlKolCopy();
                }, "olPzvID", "olKol", "olKolCopy", "olNChasi", "IsModified", "IsNew", "olPzvDivision", "olPzvIDParent"
                    , "olPzvTab", "olPzvDateNaznTab", "olPzvDateStart", "olPzvDateEnd", "olPzvDateML"
                    , "olSekNazn", "olKolNazn", "olChasNazn");
                // 3) добавить биндинги
                _pZVOperListByPachListBindingSource.Add(newItemPos);
            }
            else if (currentItem.olPzvDateNaznKm == null
                    && currentItem.olPzvDateNaznTab == null
                    && currentItem.olPzvDateStart == null
                    && currentItem.olPzvDateEnd == null
                    && currentItem.olPzvDateMast == null)
            {
                // деление, если операция еще не назначена
                // 1) пометить исходную строку
                currentItem.IsModified = true;
                //currentItem.olSekAll = Math.Round((currentItem.olKol * currentItem.olSekEd) / 3600m, 2);
                currentItem.olPzvNChasi = (int)Math.Round((currentItem.olKol * currentItem.olSekEd) / 3600m);
                currentItem.olPzvDivision = 1;

                // 2) создать копию с исключениями и проставить нужные поля
                var newItem = ObjectCloneHelper.CloneWithExclusions(currentItem, clone =>
                {
                    clone.olPzvIDParent = currentItem.olPzvID;
                    clone.olPzvDivision = 1;
                    clone.IsNew = true;
                    clone.IsModified = false;
                    // 👇 сбрасываем "копию" перед присвоением нового olKol
                    clone.ResetOlKolCopy();

                    // присваиваем новое значение
                    //clone.olKol = Convert.ToInt32(e.Value);
                    clone.olKol = currentItem.olKolCopy - Convert.ToInt32(e.Value);  // новое значение в копии
                    clone.olPzvNChasi = (int)Math.Round((clone.olKol * currentItem.olSekEd) / 3600m);
                    // 👇 фиксируем новое значение как "оригинал" для этой строки
                    clone.RebaselineOlKolCopy();

                    //clone.olPzvIDParent = currentItem.olPzvID;
                    //clone.IsNew = true;
                    //clone.IsModified = false;
                    ////clone.olKol = Convert.ToInt32(e.Value);  // новое значение в копии
                    //clone.olKol = currentItem.olKolCopy - Convert.ToInt32(e.Value);  // новое значение в копии
                    ////clone.olKolCopy = clone.olKol;
                }, "olPzvID", "olKol", "olKolCopy", "olNChasi", "IsModified", "IsNew", "olPzvDivision", "olPzvIDParent"
                    , "olPzvTab", "olPzvDateNaznTab", "olPzvDateStart", "olPzvDateEnd", "olPzvDateML"
                    , "olSekNazn", "olKolNazn", "olChasNazn");
                // 3) добавить биндинги
                _pZVOperListByPachListBindingSource.Add(newItem);
            }

            //  x => x.IsNew || x.IsModified || x.IsDeleted
            // Получаем список строк с флагом IsModified = true
            List<PZV> filteredList = _pZVOperListByPachListBindingSource.List
                .OfType<PZVOperList>()
                .Where(x => x?.IsModified == true || x?.IsNew == true)
                .Select(x => x.ToPZV())   // новый маппер
                .ToList();
            // Преобразуем в BindingList
            if (filteredList.Count > 0)
            {
                using (SqlConnection connection = _dbHelper.GetConnection())
                {
                    _bulkHelper.BulkAllDataUpdate<PZV>(connection, filteredList, "planZagrVyaz", new[] { "pzvID" });
                    // удалить из BindingSource
                    _pZVOperListByPachListBindingSource.RemoveModified<PZVOperList>();
                    _pZVOperListByPachListBindingSource.RemoveNew<PZVOperList>();
                    //LoadPlanZagrVyazByZadanySelection();
                    Task loadPlanZagrVyazTask = LoadPlanZagrVyazByZadanySelection();
                    await Task.WhenAll(loadPlanZagrVyazTask);
                }
            }

            // 3) обновить биндинги
            //_pZVOperListByPachListBindingSource.Add(newItem);
            _pZVOperListByPachListBindingSource.ResetBindings(false);
            gridViewPZVOperList.RefreshData();
            //// 4) сфокусироваться на новой строке
            //int newIndex = _pZVOperListByPachListBindingSource.Count - 1;
            //int newHandle = view.GetRowHandle(newIndex);
            //if (newHandle >= 0)
            //{
            //    view.FocusedRowHandle = newHandle;
            //    view.MakeRowVisible(newHandle);
            //    view.SelectRow(newHandle);
            //}

            GoToPzvID(xPzvID, _xColumn);

        }
        private void gridViewPZVOperList_ShownEditor(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            var editor = view?.ActiveEditor as TextEdit;
            editor?.SelectAll();
        }

        private async void repositoryItemCheckEdit5_EditValueChanged(object sender, EventArgs e)
        {
            gridViewRzvPachListByNom.PostEditor();        // применить новое значение из редактора
            gridViewRzvPachListByNom.UpdateCurrentRow();  // сохранить в источник данных
            var selectedRow = _rzvPachListByNomBindingSource.Current as RzvPachListByNom;
            try
            {
                string query = $"update raskr_zeh_vyaz set gradacia = {selectedRow.gradacia} where pach_kod = '{selectedRow.pach_kod}'";
                Task updateRZV = _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { });
                await Task.WhenAll(updateRZV);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления: {ex.Message}");
            }

            // деление операций на градацию - жду Катю Бабинцеву

        }
    }
}
