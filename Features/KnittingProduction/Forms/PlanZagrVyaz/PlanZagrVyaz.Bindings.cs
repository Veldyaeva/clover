using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Models;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SewingProduction.Helpers.GridHelper;

namespace SewingProduction.Features.KnittingProduction.Forms
{
     partial class PlanZagrVyaz
    {
        private async Task InitializeBindingsAsync()
        {
            try
            {
                var planTotalHoursByKnitMachineTask = Task.Run(() =>
                {
                    _planTotalHoursByKnitMachineBindingList = new BindingList<PlanTotalHoursByKnitMachine>();
                    _planTotalHoursByKnitMachineBindingSource = new BindingSource { DataSource = _planTotalHoursByKnitMachineBindingList };
                });
                var zadanyListTask = Task.Run(() =>
                {
                    _zadanyListBindingList = new BindingList<PZVZadanyList>();
                    _zadanyListBindingSource = new BindingSource { DataSource = _zadanyListBindingList };
                });
                var zadanyListNewTask = Task.Run(() =>
                {
                    _zadanyListNewBindingList = new BindingList<PZVZadanyList>();
                    _zadanyListNewBindingSource = new BindingSource { DataSource = _zadanyListNewBindingList };
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
                await Task.WhenAll(planTotalHoursByKnitMachineTask, zadanyListTask, zadanyListNewTask
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

                _planTotalQuantityByArticul = new BindingSource { DataSource = new BindingList<PlanTotalQuantityByArticul>() };
                switch (vyazPodrKod)
                {
                    case 1:
                        //layoutControlGroup7.CustomHeaderButtons[0].Properties.Visible = true;
                        //layoutControlGroup7.CustomHeaderButtons[1].Properties.Visible = true;
                        //layoutControlGroup7.CustomHeaderButtons[2].Properties.Visible = true;
                        _lcgHelper.SetButtonsVisible(
                            layoutControlGroup7,
                            false,
                            "lcg3HideAll",
                            "lcg3HideAllSeparator",
                            "lcg3ShowAll"
                        );
                        #region описание gridControlPlanTotalHoursByKnitMachine "общие часы по вяз машинам/зонам"
                        layoutControlItemPlanTotalQuantityByArticul.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        layoutControlItemPlanTotalHoursByKnitMachine.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
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
                        break;
                    case 2:
                    case 3:
                        layoutControlGroup7.CustomHeaderButtons[0].Properties.Visible = false;
                        layoutControlGroup7.CustomHeaderButtons[1].Properties.Visible = false;
                        layoutControlGroup7.CustomHeaderButtons[2].Properties.Visible = false;
                        #region описание gridViewPlanTotalQuantityByArticul "общее количество по артикулам"
                        layoutControlItemPlanTotalHoursByKnitMachine.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        layoutControlItemPlanTotalQuantityByArticul.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        gridControlPlanTotalQuantityByArticul.DataSource = _planTotalQuantityByArticul;
                        gridPlanTotalQuantityByArticulColumnKod.FieldName = "Kod";
                        gridPlanTotalQuantityByArticulColumnArticul.FieldName = "Articul";
                        gridPlanTotalQuantityByArticulColumnKol.FieldName = "Kol";
                        gridPlanTotalQuantityByArticulColumnDataPlan.FieldName = "DataPlan";
                        _gridHelper.AutoRowFilterConfig(gridViewPlanTotalQuantityByArticul as GridView, 0);
                        #endregion
                        break;
                }



                #region описание gridControlZadanyList "задания по номеру машины/артикулу"
                gridControlZadanyList.DataSource = _zadanyListBindingSource;
                gridZadanyListColumnPszNom.FieldName = "pszNom";
                gridZadanyListColumnNom.FieldName = "nom";
                gridZadanyListColumnArticul.FieldName = "articul";
                gridZadanyListColumnZvet.FieldName = "zvet";
                gridZadanyListColumnKol.FieldName = "kol";
                gridZadanyListColumnDatePryazZayav.FieldName = "pryazZayav";
                gridZadanyListColumnData_plan.FieldName = "data_plan";
                gridZadanyListColumnVid_stir.FieldName = "vid_stir";
                gridZadanyListColumnDopr_name.FieldName = "dopr_name";
                gridZadanyListColumnKmlID.FieldName = "kmlID";
                gridZadanyListColumnSyncSelection.FieldName = "SyncSelection";
                gridZadanyListColumnGradacia.FieldName = "Gradacia";
                gridZadanyListColumnYearPlan.FieldName = "yearPlan";
                gridZadanyListColumnKod.FieldName = "kod";
                gridZadanyListColumnMinNPach.FieldName = "minNPach";
                gridZadanyListColumnMaxNPach.FieldName = "maxNPach";
                gridZadanyListColumnBrig.FieldName = "brig";
                switch (vyazPodrKod)
                {
                    case 1:
                        gridZadanyListColumnSyncSelection.Visible = true;
                        gridZadanyListColumnSyncSelection.VisibleIndex = 0;
                        gridZadanyListColumnNom.Visible = true;
                        gridZadanyListColumnNom.VisibleIndex = 1;
                        gridZadanyListColumnArticul.Visible = true;
                        gridZadanyListColumnArticul.VisibleIndex = 2;
                        gridZadanyListColumnZvet.Visible = true;
                        gridZadanyListColumnZvet.VisibleIndex = 3;
                        gridZadanyListColumnKol.Visible = true;
                        gridZadanyListColumnKol.VisibleIndex = 4;
                        gridZadanyListColumnDatePryazZayav.Visible = true;
                        gridZadanyListColumnDatePryazZayav.VisibleIndex = 5;
                        gridZadanyListColumnData_plan.Visible = true;
                        gridZadanyListColumnData_plan.VisibleIndex = 6;
                        gridZadanyListColumnPszNom.Visible = true;
                        gridZadanyListColumnPszNom.VisibleIndex = 7;
                        gridZadanyListColumnVid_stir.Visible = true;
                        gridZadanyListColumnVid_stir.VisibleIndex = 8;
                        gridZadanyListColumnDopr_name.Visible = true;
                        gridZadanyListColumnDopr_name.VisibleIndex = 9;
                        gridZadanyListColumnGradacia.Visible = true;
                        gridZadanyListColumnGradacia.VisibleIndex = 10;

                        gridZadanyListColumnMinNPach.Visible = false;
                        gridZadanyListColumnMaxNPach.Visible = false;
                        gridZadanyListColumnBrig.Visible = false;
                        break;
                    case 2:
                    case 3:
                        gridZadanyListColumnPszNom.Visible = true;
                        gridZadanyListColumnPszNom.VisibleIndex = 0;
                        gridZadanyListColumnMinNPach.Visible = true;
                        gridZadanyListColumnMinNPach.VisibleIndex = 1;
                        gridZadanyListColumnMaxNPach.Visible = true;
                        gridZadanyListColumnMaxNPach.VisibleIndex = 2;
                        gridZadanyListColumnKol.Visible = true;
                        gridZadanyListColumnKol.VisibleIndex = 3;
                        gridZadanyListColumnZvet.Visible = true;
                        gridZadanyListColumnZvet.VisibleIndex = 4;
                        gridZadanyListColumnData_plan.Visible = true;
                        gridZadanyListColumnData_plan.VisibleIndex = 5;
                        gridZadanyListColumnBrig.Visible = true;
                        gridZadanyListColumnBrig.VisibleIndex = 6;
                        gridZadanyListColumnSyncSelection.Visible = true;
                        gridZadanyListColumnSyncSelection.VisibleIndex = 7;

                        gridZadanyListColumnNom.Visible = false;
                        gridZadanyListColumnArticul.Visible = false;
                        gridZadanyListColumnDatePryazZayav.Visible = false;
                        gridZadanyListColumnVid_stir.Visible = false;
                        gridZadanyListColumnDopr_name.Visible = false;
                        gridZadanyListColumnGradacia.Visible = false;
                        break;
                }
                gridZadanyListColumnData_plan.DisplayFormat.FormatType = FormatType.DateTime;
                gridZadanyListColumnData_plan.DisplayFormat.FormatString = "dd.MM.yy";
                _gridHelper.AutoRowFilterConfig(gridViewZadanyList as GridView, 0);
                gridViewZadanyList.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
                //switch (vyazPodrKod)
                //{
                //    case 1:

                //        break;
                //    case 2: case 3:

                //        break;
                //}

                //----------------------------------------
                gridViewZadanyList.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDown;
                gridZadanyListColumnGradacia.OptionsColumn.AllowEdit = false;
                gridZadanyListColumnGradacia.OptionsColumn.ReadOnly = false;

                gridViewZadanyList.OptionsBehavior.Editable = true;
                gridViewZadanyList.OptionsBehavior.ReadOnly = false;

                //gridZadanyListColumnSyncSelection.OptionsColumn.AllowEdit = true;
                //gridZadanyListColumnSyncSelection.OptionsColumn.ReadOnly = false;
                //gridZadanyListColumnSyncSelection.ColumnEdit = repositoryItemCheckEdit1;
                //MessageBox.Show($"gridZadanyListColumnSyncSelection.ColumnEdit = {gridZadanyListColumnSyncSelection.ColumnEdit}");
                //gridZadanyListColumnSyncSelection.OptionsColumn.AllowFocus = true;

                //MessageBox.Show(
                //    $"AllowEdit={gridZadanyListColumnSyncSelection.OptionsColumn.AllowEdit}\n" +
                //    $"ReadOnly={gridZadanyListColumnSyncSelection.OptionsColumn.ReadOnly}\n" +
                //    $"ColumnEdit={(gridZadanyListColumnSyncSelection.ColumnEdit == null ? "null" : gridZadanyListColumnSyncSelection.ColumnEdit.Name)}"
                //);
                //----------------------------------------
                //repositoryItemCheckEdit1.MouseUp += (s, e) =>
                //{
                //    BeginInvoke(new Action(() => SyncSelectionUpdate()));
                //};
                //----------------------------------------
                #endregion

                #region описание gridControlRzvPachListByNom "пачки по расчету вяз"
                gridControlRzvPachListByNom.DataSource = _rzvPachListByNomBindingSource;
                gridRzvPachListByNomColumnNomZad.FieldName = "nomZad";
                gridRzvPachListByNomColumnNom.FieldName = "nom";
                gridRzvPachListByNomColumnNom_n.FieldName = "nom_n";
                gridRzvPachListByNomColumnN_pach.FieldName = "n_pach";
                gridRzvPachListByNomColumnRazm.FieldName = "razm";
                gridRzvPachListByNomColumnKol.FieldName = "kol";
                gridRzvPachListByNomColumnGradacia.FieldName = "gradacia";
                gridRzvPachListByNomColumnSyncSelection.FieldName = "SyncSelection";

                _gridHelper.AutoRowFilterConfig(gridViewRzvPachListByNom as GridView, 0);
                //gridViewRzvPachListByNom.BestFitColumns();

                switch (vyazPodrKod)
                {
                    case 1:
                        gridRzvPachListByNomColumnGradacia.Visible = true;
                        gridRzvPachListByNomColumnGradacia.VisibleIndex = 0;
                        gridRzvPachListByNomColumnN_pach.Visible = true;
                        gridRzvPachListByNomColumnN_pach.VisibleIndex = 1;
                        gridRzvPachListByNomColumnRazm.Visible = true;
                        gridRzvPachListByNomColumnRazm.VisibleIndex = 2;
                        gridRzvPachListByNomColumnKol.Visible = true;
                        gridRzvPachListByNomColumnKol.VisibleIndex = 3;
                        gridRzvPachListByNomColumnSyncSelection.Visible = true;
                        gridRzvPachListByNomColumnSyncSelection.VisibleIndex = 4;

                        //gridRzvPachListByNomColumnNomZad.Visible = false;
                        //gridRzvPachListByNomColumnNom.Visible = false;
                        //gridRzvPachListByNomColumnNom_n.Visible = false;
                        gridRzvPachListByNomColumnSerialNumber.Visible = false;
                        break;
                    case 2:
                    case 3:
                        gridRzvPachListByNomColumnSerialNumber.Visible = true;
                        gridRzvPachListByNomColumnSerialNumber.VisibleIndex = 0;
                        gridRzvPachListByNomColumnSerialNumber.Width = 20;
                        gridRzvPachListByNomColumnN_pach.Visible = true;
                        gridRzvPachListByNomColumnN_pach.VisibleIndex = 1;
                        gridRzvPachListByNomColumnN_pach.Width = 50;
                        gridRzvPachListByNomColumnRazm.Visible = true;
                        gridRzvPachListByNomColumnRazm.VisibleIndex = 2;
                        gridRzvPachListByNomColumnKol.Visible = true;
                        gridRzvPachListByNomColumnKol.VisibleIndex = 3;
                        gridRzvPachListByNomColumnKol.Width = 40;
                        gridRzvPachListByNomColumnSyncSelection.Visible = true;
                        gridRzvPachListByNomColumnSyncSelection.VisibleIndex = 4;
                        gridRzvPachListByNomColumnSyncSelection.Width = 20;

                        gridRzvPachListByNomColumnGradacia.Visible = false;

                        //gridRzvPachListByNomColumnNomZad.Visible = false;
                        //gridRzvPachListByNomColumnNom.Visible = false;
                        //gridRzvPachListByNomColumnNom_n.Visible = false;



                        break;
                }

                #endregion

                _smenZadanyVyazBindingSource = new BindingSource
                {
                    DataSource = new BindingList<SmenZadanyVyaz>()
                };
                _smenZadanyVyazNewBindingSource = new BindingSource
                {
                    DataSource = new BindingList<SmenZadanyVyaz>()
                };
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
                advBandedGridViewSmenZadany.Columns["chasNaznZadGroup"].UnboundType = UnboundColumnType.Decimal;
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
                bandedGridSmenZadanyColumnChasNaznZad.DisplayFormat.FormatType = FormatType.Numeric;
                bandedGridSmenZadanyColumnChasNaznZad.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
                bandedGridSmenZadanyColumnChasNotConfirmedZad.DisplayFormat.FormatType = FormatType.Numeric;
                bandedGridSmenZadanyColumnChasNotConfirmedZad.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
                bandedGridSmenZadanyColumnChasRemainZad.DisplayFormat.FormatType = FormatType.Numeric;
                bandedGridSmenZadanyColumnChasRemainZad.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
                bandedGridSmenZadanyColumnShiftsRemainZad.DisplayFormat.FormatType = FormatType.Numeric;
                bandedGridSmenZadanyColumnShiftsRemainZad.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
                bandedGridSmenZadanyColumnChasNaznSmen.DisplayFormat.FormatType = FormatType.Numeric;
                bandedGridSmenZadanyColumnChasNaznSmen.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
                bandedGridSmenZadanyColumnChasNaznSmenProc.DisplayFormat.FormatType = FormatType.Numeric;
                bandedGridSmenZadanyColumnChasNaznSmenProc.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
                bandedGridSmenZadanyColumnChasInWorkSmen.DisplayFormat.FormatType = FormatType.Numeric;
                bandedGridSmenZadanyColumnChasInWorkSmen.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
                bandedGridSmenZadanyColumnChasDoneSmen.DisplayFormat.FormatType = FormatType.Numeric;
                bandedGridSmenZadanyColumnChasDoneSmen.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
                bandedGridSmenZadanyColumnChasDoneSmenProc.DisplayFormat.FormatType = FormatType.Numeric;
                bandedGridSmenZadanyColumnChasDoneSmenProc.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
                bandedGridSmenZadanyColumnChasRemainSmen.DisplayFormat.FormatType = FormatType.Numeric;
                bandedGridSmenZadanyColumnChasRemainSmen.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";
                bandedGridSmenZadanyColumnChasConfirmedSmen.DisplayFormat.FormatType = FormatType.Numeric;
                bandedGridSmenZadanyColumnChasConfirmedSmen.DisplayFormat.FormatString = "{0:0.00#;-0.00#;#}";

                string _xNumFormat = "{0:0.00;-0.00;}";
                foreach (GridGroupSummaryItem gsi in advBandedGridViewSmenZadany.GroupSummary)
                {
                    gsi.DisplayFormat = _xNumFormat;
                }

                _gridHelper.AutoRowFilterConfig(advBandedGridViewSmenZadany, 0);
                advBandedGridViewSmenZadany.OptionsView.GroupFooterShowMode = GroupFooterShowMode.Hidden;
                _gridHelper.EnableGroupSummariesInGroupRow(advBandedGridViewSmenZadany, GroupSummaryLevelMode.IncludeOnly, new[] { 2 });


                gridSmenZadanyOtpColumnSzFio.FieldName = "szFio";
                gridSmenZadanyOtpColumnSzDolgn.FieldName = "szDolgn";
                gridSmenZadanyOtpColumnSzTab.FieldName = "szTab";
                gridSmenZadanyOtpColumnSzKoefVNV.FieldName = "szKoefVNV";
                gridSmenZadanyOtpColumnSzPlanHours.FieldName = "szPlanHours";
                gridSmenZadanyOtpColumnSzNaznHours.FieldName = "szNaznHours";
                gridSmenZadanyOtpColumnSzPlanNaznPercent.FieldName = "szPlanNaznPercent";
                gridSmenZadanyOtpColumnSzHoursToDo.FieldName = "szHoursToDo";
                gridSmenZadanyOtpColumnSzHoursDone.FieldName = "szHoursDone";
                gridSmenZadanyOtpColumnSzShiftVNV.FieldName = "szShiftVNV";
                gridSmenZadanyOtpColumnSzDTab.FieldName = "szDTab";
                //_gridHelper.AutoRowFilterConfig(gridViewSmenZadanyOtp, 0);

                switch (vyazPodrKod)
                {
                    case 1:
                        gridControlSmenZadany.MainView = advBandedGridViewSmenZadany;
                        break;
                    case 2:
                    case 3:
                        gridControlSmenZadany.MainView = gridViewSmenZadanyOtp;
                        _lcgHelper.SetButtonsVisible(
                            layoutControlGroup2,
                            false,
                            "lcg2HideAll",
                            "lcg2HideAllSeparator",
                            "lcg2ShowAll",
                            "lcg2ShowAllSeparator",
                            "lcg2ShowFIO",
                            "lcg2ShowFIOSeparator",
                            "lcg2ShowMH"
                        );
                        //layoutControlGroup2.CustomHeaderButtons[5].Properties.Visible = false;
                        //layoutControlGroup2.CustomHeaderButtons[6].Properties.Visible = false;
                        //layoutControlGroup2.CustomHeaderButtons[7].Properties.Visible = false;
                        //layoutControlGroup2.CustomHeaderButtons[8].Properties.Visible = false;
                        //layoutControlGroup2.CustomHeaderButtons[9].Properties.Visible = false;
                        //layoutControlGroup2.CustomHeaderButtons[10].Properties.Visible = false;
                        //layoutControlGroup2.CustomHeaderButtons[11].Properties.Visible = false;
                        //layoutControlGroup2.CustomHeaderButtons[12].Properties.Visible = false;
                        break;
                }


                //// перенос текста
                //advBandedGridViewSmenZadany.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                //advBandedGridViewSmenZadany.Appearance.BandPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

                //// авто-высота заголовков (колонки и бэнды)
                //advBandedGridViewSmenZadany.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                //// высота строки заголовков бэндов 
                //advBandedGridViewSmenZadany.BandPanelRowHeight = 37;
                //TODO : если понадобится, можно будет донастроить высоту строк заголовков колонок, но пока не нужно
                #endregion

                _lcgHelper.SetButtonsVisible(
                            layoutControlGroup3,
                            false,
                            "lcg3HideAll",
                            "lcg3HideAllSeparator",
                            "lcg3ShowAll"
                        );
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

                gridViewNaryadZadany.OptionsView.ShowGroupPanel = false;
                if (vyazPodrKod == 2 || vyazPodrKod == 3)
                {
                    gridViewNaryadZadany.ClearGrouping();
                    gridNaryadZadanyColumnKmlNumber.Visible = false;
                    gridNaryadZadanyColumnPzvNom.Visible = false;
                    gridNaryadZadanyColumnPzvTab.Visible = false;
                }
                gridNaryadZadanyColumnSekObServ.DisplayFormat.FormatType = FormatType.Numeric;
                gridNaryadZadanyColumnSekObServ.DisplayFormat.FormatString = "#,0.00;-#,0.00;";

                gridNaryadZadanyColumnHoursTotalPlan.DisplayFormat.FormatType = FormatType.Numeric;
                gridNaryadZadanyColumnHoursTotalPlan.DisplayFormat.FormatString = "#,0.00;-#,0.00;";

                gridNaryadZadanyColumnHoursTotalFact.DisplayFormat.FormatType = FormatType.Numeric;
                gridNaryadZadanyColumnHoursTotalFact.DisplayFormat.FormatString = "#,0.00;-#,0.00;";

                gridNaryadZadanyColumnStatusDate.DisplayFormat.FormatType = FormatType.DateTime;
                gridNaryadZadanyColumnStatusDate.DisplayFormat.FormatString = "dd.MM.yy";

                _gridHelper.AutoRowFilterConfig(gridViewNaryadZadany, 0);

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
                #endregion

                #region описание gridControlPZVOperList
                gridControlPZVOperList.DataSource = _pZVOperListByPachListBindingSource;
                gridPZVOperListColumnOlPzvID.FieldName = "olPzvID";
                gridPZVOperListColumnOlPzvIDParent.FieldName = "olPzvIDParent";
                gridPZVOperListColumnOlPzvDivision.FieldName = "olPzvDivision";
                gridPZVOperListColumnOlPzvIDMlOp.FieldName = "olPzvIDMlOp";
                gridPZVOperListColumnOlNom.FieldName = "olNom";
                gridPZVOperListColumnOlNomN.FieldName = "olNomN";
                gridPZVOperListColumnOlNomZad.FieldName = "olNomZad";
                gridPZVOperListColumnOlPzvAnnID.FieldName = "olPzvAnnID";
                gridPZVOperListColumnOlPzvNrID.FieldName = "olPzvNrID";
                gridPZVOperListColumnOlPzvIdBrig.FieldName = "olPzvIdBrig";
                gridPZVOperListColumnOlPzvKmlID.FieldName = "olPzvKmlID";
                gridPZVOperListColumnOlPzvArticul.FieldName = "olPzvArticul";
                gridPZVOperListColumnOlPzvMod.FieldName = "olPzvMod";
                gridPZVOperListColumnOlNPach.FieldName = "olNPach";
                //pach_kod
                //kod
                gridPZVOperListColumnOlNo.FieldName = "olNo";
                gridPZVOperListColumnOlNpo.FieldName = "olNpo";
                gridPZVOperListColumnOlNomOper.FieldName = "olNomOper";
                gridPZVOperListColumnOlOperName.FieldName = "olOperName";
                gridPZVOperListColumnOlKodOb.FieldName = "olKodOb";
                gridPZVOperListColumnOlOborudClass.FieldName = "olOborudClass";
                gridPZVOperListColumnOlRazryd.FieldName = "olRazryd";
                gridPZVOperListColumnOlSekEd.FieldName = "olSekEd";
                gridPZVOperListColumnOlKol.FieldName = "olKol";
                gridPZVOperListColumnOlPzvNChasi.FieldName = "olPzvNChasi";
                gridPZVOperListColumnOlKmlNumber.FieldName = "olKmlNumber";
                gridPZVOperListColumnOlPvDateNaznKm.FieldName = "olPzvDateNaznKm";
                gridPZVOperListColumnOlPzvTab.FieldName = "olPzvTab";
                gridPZVOperListColumnOlPzvDateNaznTab.FieldName = "olPzvDateNaznTab";
                gridPZVOperListColumnOlPzvDateStart.FieldName = "olPzvDateStart";
                gridPZVOperListColumnOlPzvDateEnd.FieldName = "olPzvDateEnd";
                gridPZVOperListColumnOlPzvDateML.FieldName = "olPzvDateML";
                gridPZVOperListColumnOlPzvDateMast.FieldName = "olPzvDateMast";
                gridPZVOperListColumnSyncSelection.FieldName = "SyncSelection";
                gridPZVOperListColumnOlPzvGradacia.FieldName = "olPzvGradacia";
                gridPZVOperListColumnOlDefect.FieldName = "olDefect";
                gridPZVOperListColumnOlTextObS.FieldName = "olTextObS";
                gridPZVOperListColumnOlDolgn.FieldName = "olDolgn";

                switch (vyazPodrKod)
                {
                    case 1:
                        gridPZVOperListColumnOlDefect.Visible = false;
                        gridPZVOperListColumnOlTextObS.Visible = false;
                        gridPZVOperListColumnOlDolgn.Visible = false;
                        break;
                    case 2:
                    case 3:
                        gridPZVOperListColumnOlOborudClass.Visible = false;
                        gridPZVOperListColumnOlPzvGradacia.Visible = false;
                        gridPZVOperListColumnOlGsName.Visible = false;
                        gridPZVOperListColumnOlKmlNumber.Visible = false;
                        gridPZVOperListColumnOlPvDateNaznKm.Visible = false;
                        break;
                }

                gridPZVOperListColumnOlPzvNChasi.DisplayFormat.FormatType = FormatType.Numeric;
                //gridColumnPZVOperListOlNChasi.DisplayFormat.FormatString = "#,0.00;-#,0.00;''";
                gridPZVOperListColumnOlPzvNChasi.DisplayFormat.FormatString = "#,0.00;-#,0.00;";
                gridPZVOperListColumnOlPvDateNaznKm.DisplayFormat.FormatType = FormatType.DateTime;
                gridPZVOperListColumnOlPvDateNaznKm.DisplayFormat.FormatString = "dd.MM.yy";
                gridPZVOperListColumnOlPzvDateNaznTab.DisplayFormat.FormatType = FormatType.DateTime;
                gridPZVOperListColumnOlPzvDateNaznTab.DisplayFormat.FormatString = "dd.MM.yy";
                gridPZVOperListColumnOlPzvDateStart.DisplayFormat.FormatType = FormatType.DateTime;
                gridPZVOperListColumnOlPzvDateStart.DisplayFormat.FormatString = "dd.MM.yy";
                gridPZVOperListColumnOlPzvDateEnd.DisplayFormat.FormatType = FormatType.DateTime;
                gridPZVOperListColumnOlPzvDateEnd.DisplayFormat.FormatString = "dd.MM.yy";
                gridPZVOperListColumnOlPzvDateML.DisplayFormat.FormatType = FormatType.DateTime;
                gridPZVOperListColumnOlPzvDateML.DisplayFormat.FormatString = "dd.MM.yy";
                gridPZVOperListColumnOlPzvDateMast.DisplayFormat.FormatType = FormatType.DateTime;
                gridPZVOperListColumnOlPzvDateMast.DisplayFormat.FormatString = "dd.MM.yy";

                gridPZVOperListColumnOlKmlNumber.OptionsColumn.AllowEdit = false;
                gridPZVOperListColumnOlPvDateNaznKm.OptionsColumn.AllowEdit = false;
                gridPZVOperListColumnOlPzvTab.OptionsColumn.AllowEdit = false;
                gridPZVOperListColumnOlPzvDateNaznTab.OptionsColumn.AllowEdit = false;
                gridPZVOperListColumnOlPzvDateStart.OptionsColumn.AllowEdit = false;
                gridPZVOperListColumnOlPzvDateEnd.OptionsColumn.AllowEdit = false;
                gridPZVOperListColumnOlPzvDateMast.OptionsColumn.AllowEdit = false;
                _gridHelper.AutoRowFilterConfig(gridViewPZVOperList as GridView, 0);
                gridViewPZVOperList.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
                gridPZVOperListColumnOlNPach.OptionsColumn.AllowSort = DefaultBoolean.False;

                gridViewPZVOperList.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDown;
                gridPZVOperListColumnSyncSelection.OptionsColumn.AllowEdit = true;
                gridPZVOperListColumnSyncSelection.OptionsColumn.ReadOnly = false;

                //gridViewPZVOperList.DoubleClick += gridViewPZVOperList_DoubleClick;

                #endregion

                #region описание блока Информация по операции
                textBoxOlPzvNom.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olNom), true, DataSourceUpdateMode.Never);
                textBoxOlPzvID.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvID), true, DataSourceUpdateMode.Never);
                textBoxOlPzvIDMlOp.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvIDMlOp), true, DataSourceUpdateMode.Never);
                textBoxOlPzvAnnID.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvAnnID), true, DataSourceUpdateMode.Never);
                textBoxOlPzvUpdDate.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvUpdDate), true, DataSourceUpdateMode.Never);
                textBoxOlNomOper.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olNomOper), true, DataSourceUpdateMode.Never);
                textBoxOlPzvNrID.DataBindings.Add("Text", _pZVOperListByPachListBindingSource, nameof(PZVOperList.olPzvNrID), true, DataSourceUpdateMode.Never);
                #endregion

                Debug.WriteLine($"[PlanZagrVyaz] InitializeBindingsAsync completed");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }

            await Task.CompletedTask;
        }
        #region грид GridNaryadZadany popupMenu init
        private void SetupGridNaryadZadanyEvents()
        {
            // Обработка клика правой кнопкой мыши через MouseDown
            gridControlNaryadZadany.MouseDown += GridControlNaryadZadany_MouseDown;
            Debug.WriteLine($"[PlanZagrVyaz] SetupGridNaryadZadanyEvents completed");
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
                if (hit.InRowCell && hit.Column.FieldName == "pzvNomZad" /*|| hit.Column.FieldName == "pzvNom" || hit.Column.FieldName == "nPach"*/)
                {
                    // Получаем значение ячейки
                    object cellValue = gridViewNaryadZadany.GetRowCellValue(hit.RowHandle, hit.Column);

                    // Создаем и показываем контекстное меню
                    ShowContextMenuWithValue(cellValue, e.Location, hit);

                    // Предотвращаем дальнейшую обработку
                    // (опционально, если нужно отменить стандартное меню)
                }
            }
            Debug.WriteLine($"[PlanZagrVyaz] GridControlNaryadZadany_MouseDown completed");
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
                            textBoxPzvYearPachSearch.Text = Convert.ToString(gridViewNaryadZadany.GetRowCellValue(_hit.RowHandle, gridNaryadZadanyColumnYearPach));
                            textBoxPzvNomSearch.Text = searchItem.Tag.ToString();
                            textBoxPzvNomSearch.Focus();
                            textBoxPzvNomSearch.SelectAll();
                            break;
                        case "nPach":
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

            Debug.WriteLine($"[PlanZagrVyaz] ShowContextMenuWithValue completed");
        }
        #endregion
    }
}