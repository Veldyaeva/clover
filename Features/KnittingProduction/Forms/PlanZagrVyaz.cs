using DevExpress.Data;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils;
using DevExpress.XtraGauges.Core.Styles;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json;
using SewingProduction.Extensions;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Features.KnittingProduction.Services;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class PlanZagrVyaz : CustomForm, IThemeable
    {
        int vyazPodrKod = 0;

        private static DatabaseHelper _dbHelper;
        private static DbService _dbService;
        private static BulkHelper _bulkHelper;
        private static GridHelper _gridHelper;
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

        public PlanZagrVyaz()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _dbService = new DbService(_dbHelper);
            _bulkHelper = new BulkHelper();
            _gridHelper = new GridHelper();
            _vyazService = new VyazService(_dbHelper);
            ThemeManager.UpdateTheme(this);
            //нужно будет определять, мастер вяз цеха или отпарки заходит в форму и
            //сохранять признак подразделения для дальнейшней загрузки операций только того подразделения, чей мастер зашел
            // пока что примем, что заходит только мастер вяз цеха, признак пропишем жестко 1
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
                //await Task.WhenAll(vyazPlanViewTask, artPrFioProgrTask, planSezonZadanyTask, knitMachineListTask
                //        , artPrKnitMachineViewPr1Task, artPrKnitMachineViewPr2Task, artPrKnitMachineViewRecom1Task, artPrKnitMachineViewRecom2Task);

                await Task.WhenAll(planTotalHoursByKnitMachineTask, zadanyListByMachineTask, zadanyListByMachineNewTask
                        , rzvPachListByNomTask, rzvPachListByNomNewTask, pZVOperListByPachListTask, pZVOperListByPachListNewTask);

                //#region описание comboBox "Список машин"
                //comboBoxKnitMachineList.DataSource = _knitMachineListBindingSource;
                //comboBoxKnitMachineList.SelectedIndex = -1;
                //comboBoxKnitMachineList.ValueMember = "kmlID";
                //comboBoxKnitMachineList.DisplayMember = "kmlNumber";
                //#endregion

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

                gridViewRzvPachListByNom.OptionsView.ShowAutoFilterRow = false;
                gridViewRzvPachListByNom.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
                gridViewRzvPachListByNom.OptionsFilter.AllowFilterEditor = false;

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
                gridColumnRzvPachListByNomGrad.FieldName = "grad";
                gridColumnRzvPachListByNomSyncSelection.FieldName = "SyncSelection";
                #endregion

                //#region описание gridControlArtPrFioProgr "проработки"
                //gridControlArtPrFioProgr.DataSource = _artPrFioProgrViewBindingSource;
                //gridColumnArtPrFioProgrArticul.FieldName = "art_pr";
                //gridColumnArtPrFioProgrRazm.FieldName = "razm";
                //#endregion

                //#region описание gridControlPlanSezonZadanyRazmKol "количество по размерам"
                //gridControlPlanSezonZadanyRazmKol.DataSource = _planSezonZadanyViewBindingSource;
                //gridColumnPlanSezonZadanyRazmKolRazm.FieldName = "razm";
                //gridColumnPlanSezonZadanyRazmKolKol.FieldName = "kol";
                //#endregion

                //#region описание gridControlArtPrKnitMachineViewPr1 "в.м ПР основн."
                //gridControlArtPrKnitMachineViewPr1.DataSource = _artPrKnitMachineViewPr1BindingSource;
                //gridColumnArtPrKnitMachineViewPr1KmlNumber.FieldName = "kmlNumber";
                //#endregion
                //#region описание gridControlArtPrKnitMachineViewPr2 "в.м ПР вспомог."
                //gridControlArtPrKnitMachineViewPr2.DataSource = _artPrKnitMachineViewPr2BindingSource;
                //gridColumnArtPrKnitMachineViewPr1KmlNumber.FieldName = "kmlNumber";
                //#endregion
                //#region описание gridControlArtPrKnitMachineViewRecom1 "в.м ПР основн."
                //gridControlArtPrKnitMachineViewRecom1.DataSource = _artPrKnitMachineViewRecom1BindingSource;
                //gridColumnArtPrKnitMachineViewRecom1KmlNumber.FieldName = "kmlNumber";
                //gridColumnArtPrKnitMachineViewRecom1AvailableHoursCurrMonth.FieldName = "availableHoursCurrMonth";
                //gridColumnArtPrKnitMachineViewRecom1AvailableHoursNextMonth.FieldName = "availableHoursNextMonth";
                //#endregion
                //#region описание gridControlArtPrKnitMachineViewRecom2 "в.м ПР вспомог."
                //gridControlArtPrKnitMachineViewRecom2.DataSource = _artPrKnitMachineViewRecom2BindingSource;
                //gridColumnArtPrKnitMachineViewRecom2KmlNumber.FieldName = "kmlNumber";
                //gridColumnArtPrKnitMachineViewRecom2AvailableHoursCurrMonth.FieldName = "availableHoursCurrMonth";
                //gridColumnArtPrKnitMachineViewRecom2AvailableHoursNextMonth.FieldName = "availableHoursNextMonth";
                //#endregion

                //pictureBoxEskiz.DataBindings.Add("ImageLocation", _vyazPlanViewBindingSource, nameof(VyazPlanView.PictPath), true, DataSourceUpdateMode.Never);

                #region описание gridControlPZVOperList
                gridControlPZVOperList.DataSource = _pZVOperListByPachListBindingSource;
                gridColumnPZVOperListOlPzvID.FieldName = "olPzvID";
                gridColumnPZVOperListOlPzvIDParent.FieldName = "olPzvIDParent";
                gridColumnPZVOperListOlPzvIDMlOp.FieldName = "olPzvIDMlOp";
                gridColumnPZVOperListOlNom.FieldName = "olNom";
                gridColumnPZVOperListOlNomN.FieldName = "olNomN";
                gridColumnPZVOperListOlNomZad.FieldName = "olNomZad";
                gridColumnPZVOperListOlPzvAnnID.FieldName = "olPzvAnnID";
                gridColumnPZVOperListOlPzvNrID.FieldName = "olPzvNrID";
                gridColumnPZVOperListOlPzvIdBrig.FieldName = "olPzvIdBrig";
                gridColumnPZVOperListOlPzvKmlID.FieldName = "olPzvKmlID";
                gridColumnPZVOperListOlPzvArticul.FieldName = "olPzvArticul";
                gridColumnPZVOperListOlNPach.FieldName = "olNPach";
                gridColumnPZVOperListOlNo.FieldName = "olNo";
                gridColumnPZVOperListOlNpo.FieldName = "olNpo";
                gridColumnPZVOperListOlOperName.FieldName = "olOperName";
                gridColumnPZVOperListOlKodOb.FieldName = "olKodOb";
                gridColumnPZVOperListOlOborudClass.FieldName = "olOborudClass";
                gridColumnPZVOperListOlRazryd.FieldName = "olRazryd";
                gridColumnPZVOperListOlSekEd.FieldName = "olSekEd";
                gridColumnPZVOperListOlKol.FieldName = "olKol";
                gridColumnPZVOperListOlSekAll.FieldName = "olSekAll";
                gridColumnPZVOperListOlKmlNumber.FieldName = "olKmlNumber";
                gridColumnPZVOperListOlPvDateNaznKm.FieldName = "olPvDateNaznKm";
                gridColumnPZVOperListOlPzvTab.FieldName = "olPzvTab";
                gridColumnPZVOperListOlPzvDateNaznTab.FieldName = "olPzvDateNaznTab";
                gridColumnPZVOperListOlPzvDateStart.FieldName = "olPzvDateStart";
                gridColumnPZVOperListOlPzvDateEnd.FieldName = "olPzvDateEnd";
                gridColumnPZVOperListOlPzvDateML.FieldName = "olPzvDateML";
                gridColumnPZVOperListOlPzvDateMast.FieldName = "olPzvDateMast";
                gridColumnPZVOperListSyncSelection.FieldName = "SyncSelection";
                // Сначала сортируем по артикулу, далее пачка, номер операции/подоперации, ID родительской записи, ID записи
                gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olPzvArticul"], ColumnSortOrder.Ascending));
                gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olNPach"], ColumnSortOrder.Ascending));
                gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olNo"], ColumnSortOrder.Ascending));
                gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olNpo"], ColumnSortOrder.Ascending));
                gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olPzvIDParent"], ColumnSortOrder.Ascending));
                gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olPzvID"], ColumnSortOrder.Ascending));
                _gridHelper.AutoRowFilterConfig(gridViewPZVOperList as GridView);
                //AutoRowFilterConfigForm(gridViewPZVOperList as GridView);
                #endregion

                #region описание блока Информация по операции
                textBoxOlPzvNom.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olNom), true, DataSourceUpdateMode.Never);
                textBoxOlPzvID.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvID), true, DataSourceUpdateMode.Never);
                textBoxOlPzvIDMlOp.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvIDMlOp), true, DataSourceUpdateMode.Never);
                textBoxOlPzvAnnID.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvAnnID), true, DataSourceUpdateMode.Never);
                textBoxOlPzvUpdDate.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvUpdDate), true, DataSourceUpdateMode.Never);
                textBoxOlNo.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olNo), true, DataSourceUpdateMode.Never);
                textBoxOlNpo.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olNpo), true, DataSourceUpdateMode.Never);
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

            }
        }

        private async void LoadPlanZagrVyazByZadanySelection()
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
                //// Сначала сортируем по артикулу, далее пачка, номер операции/подоперации, ID родительской записи, ID записи
                //gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olPzvArticul"], ColumnSortOrder.Ascending));
                //gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olNPach"], ColumnSortOrder.Ascending));
                //gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olNo"], ColumnSortOrder.Ascending));
                //gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olNpo"], ColumnSortOrder.Ascending));
                //gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olPzvIDParent"], ColumnSortOrder.Ascending));
                //gridViewPZVOperList.SortInfo.Add(new GridColumnSortInfo(gridViewPZVOperList.Columns["olPzvID"], ColumnSortOrder.Ascending));

                gridViewPZVOperList.EndSort();

                // Безопасное получение списков
                var oldList = _pZVOperListByPachListBindingSource.List?.Cast<dynamic>().Where(x => x != null).ToList() ?? new List<dynamic>();
                var newList = _pZVOperListByPachListNewBindingSource.List?.Cast<dynamic>().Where(x => x != null).ToList() ?? new List<dynamic>();

                // Добавляем новые записи
                var recordsToAdd = newList
                    .Where(newRec => !oldList.Any(oldRec =>
                        oldRec.olPzvID == newRec.olPzvID))
                    .ToList();

                foreach (var record in recordsToAdd)
                {
                    _pZVOperListByPachListBindingSource.Add(record);
                }

                // Удаляем отсутствующие записи
                var recordsToRemove = _pZVOperListByPachListBindingSource.List.Cast<dynamic>()
                    .Where(oldRec => oldRec != null &&
                           !newList.Any(newRec => newRec != null &&
                                newRec.olPzvID == oldRec.olPzvID))
                    .ToList();

                // Удаляем через временный список
                var tempList = recordsToRemove.ToList();
                foreach (var record in tempList)
                {
                    _pZVOperListByPachListBindingSource.Remove(record);
                }

                //// Применяем фильтр
                //if (selectedRow != null)
                //{
                //    gridViewRzvPachListByNom.ActiveFilterString = $"nomZad == '{selectedRow.pszNom}' and nom == {selectedRow.nom}";
                //}

                _pZVOperListByPachListBindingSource.ResetBindings(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}");
            }
            //----------------------------------------------------
        }

        private async void PlanZagrVyaz_Load(object sender, EventArgs e)
        {
            Task bindingsTask = InitializeBindingsAsync();
            await Task.WhenAll(bindingsTask);
            await LoadPlanTotalHoursByKnitMachineDataAsync();
            gridViewPlanTotalHoursByKnitMachine.ExpandAllGroups();
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
            gridViewZadanyListByMachine.OptionsView.ShowAutoFilterRow = false;
            gridViewZadanyListByMachine.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gridViewZadanyListByMachine.OptionsFilter.AllowFilterEditor = false;
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
            gridViewZadanyListByMachine.FocusedColumn = gridViewZadanyListByMachine.Columns["data_paln"];
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

        //public void AutoRowFilterConfigForm(GridView _gridView)
        //{
        //    if (_gridView == null) return;

        //    try
        //    {
        //        // Основные настройки GridView
        //        _gridView.OptionsView.ShowAutoFilterRow = true;
        //        _gridView.OptionsCustomization.AllowFilter = true;
        //        //gridView.OptionsFilter.AllowColumnFilter = true;
        //        _gridView.OptionsFilter.AllowFilterEditor = true;

        //        //// Улучшенные настройки фильтрации
        //        //gridView.OptionsFilter.ImmediateUpdateAutoFilter = false; // Отложенное обновление
        //        //gridView.OptionsFilter.AllowFilterEditorMenu = true; // Меню в редакторе фильтров

        //        // Настройка каждого столбца
        //        foreach (GridColumn _column in _gridView.Columns)
        //        {
        //            if (!_column.Visible) continue; // Пропускаем скрытые колонки

        //            //column.OptionsFilter.AllowAutoFilter = true;
        //            //column.OptionsFilter.AllowFilter = true;
        //            _column.OptionsFilter.AllowAutoFilter = false;
        //            _column.OptionsFilter.AllowFilter = false;
        //            _column.OptionsColumn.AllowSort = DefaultBoolean.False;
        //            // Умная настройка условий фильтрации
        //            SetColumnFilterConditionForm(_column);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка настройки GridView: {ex.Message}");
        //    }
        //}
        //private static void SetColumnFilterConditionForm(GridColumn _column)
        //{
        //    if (_column.ColumnType == typeof(string))
        //    {
        //        _column.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
        //    }
        //    else if (_column.ColumnType == typeof(DateTime))
        //    {
        //        _column.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Equals;
        //        _column.OptionsFilter.FilterPopupMode = FilterPopupMode.Date;
        //    }
        //    else if (_column.ColumnType == typeof(bool))
        //    {
        //        _column.OptionsFilter.FilterPopupMode = FilterPopupMode.CheckedList;
        //    }
        //    else if (IsNumericTypeForm(_column.ColumnType))
        //    {
        //        _column.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Equals;
        //    }
        //}
        ///// <summary>
        ///// Вспомогательный метод для проверки числовых типов
        ///// </summary>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //private static bool IsNumericTypeForm(Type type)
        //{
        //    return type == typeof(int) || type == typeof(double) || type == typeof(decimal)
        //           || type == typeof(float) || type == typeof(long) || type == typeof(short);
        //}
    }
}
