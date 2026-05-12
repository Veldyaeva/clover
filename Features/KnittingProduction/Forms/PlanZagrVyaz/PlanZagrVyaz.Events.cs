using DevExpress.XtraEditors.ButtonsPanelControl;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using Newtonsoft.Json;
using SewingProduction.Core.helpers;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Contexts;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Routing;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Helpers;
using SewingProduction.Report;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Formatting = Newtonsoft.Json.Formatting;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class PlanZagrVyaz
    {
        //private async void PlanZagrVyaz_Load(object sender, EventArgs e)
        //{
        //    _lifetimeCts = new CancellationTokenSource();

        //    InitObjectRestartMap();

        //    await InitializeBindingsAsync();
        //    await InitServiceBrokerAsync(_lifetimeCts.Token);
        //}

        private async void buttonKnittingMachineWorkAssignment_Click(object sender, EventArgs e)
        {
            await ExecutePzvActionAsync(PzvActionType.AssignKnittingMachine);
        }

        private async void buttonKnittingMachineCancelWorkAssignment_Click(object sender, EventArgs e)
        {
            await ExecutePzvActionAsync(PzvActionType.CancelKnittingMachine);
        }

        private async void buttonTabWorkAssignment_Click(object sender, EventArgs e)
        {
            await ExecutePzvActionAsync(PzvActionType.AssignTab);
        }

        private async void buttonTabCancelWorkAssignment_Click(object sender, EventArgs e)
        {
            await ExecutePzvActionAsync(PzvActionType.CancelTab);
        }
        private async void buttonTab999WorkAssignment_Click(object sender, EventArgs e)
        {
            await ExecutePzvActionAsync(PzvActionType.AssignTab999);
        }
        private async void buttonWorkStartExecution_Click(object sender, EventArgs e)
        {
            await ExecutePzvActionAsync(PzvActionType.StartWork);
        }

        private async void buttonCancelWorkStartExecution_Click(object sender, EventArgs e)
        {
            await ExecutePzvActionAsync(PzvActionType.CancelStartWork);
        }

        private async void buttonWorkStopExecution_Click(object sender, EventArgs e)
        {
            await ExecutePzvActionAsync(PzvActionType.StopWork);
        }

        private async void buttonCancelWorkStopExecution_Click(object sender, EventArgs e)
        {
            await ExecutePzvActionAsync(PzvActionType.CancelStopWork);
        }

        private async void buttonMasterConfirmation_Click(object sender, EventArgs e)
        {
            await ExecutePzvActionAsync(PzvActionType.ConfirmMaster);
        }

        private async void buttonMasterCancelConfirmation_Click(object sender, EventArgs e)
        {
            await ExecutePzvActionAsync(PzvActionType.CancelMasterConfirmation);
        }

        private async Task ExecutePzvActionAsync(PzvActionType action)
        {
            try
            {
                // 👉 подтверждение для Tab 999
                if (action == PzvActionType.AssignTab999)
                {
                    var mResult = MessageBox.Show(
                        "Назначить табельный номер 999 на все выбранные операции?\n\nВы уверены?",
                        "Подтверждение",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (mResult != DialogResult.Yes)
                        return;
                }
                PzvSelectionContext context = action switch
                {
                    PzvActionType.AssignKnittingMachine => _contextBuilder.BuildForPzvActionsWithShift(),
                    PzvActionType.AssignTab => _contextBuilder.BuildForPzvActionsWithShift(),
                    PzvActionType.AssignTab999 => _contextBuilder.BuildForPzvActions(),
                    PzvActionType.CancelKnittingMachine => _contextBuilder.BuildForPzvActions(),
                    PzvActionType.CancelTab => _contextBuilder.BuildForPzvActions(),
                    PzvActionType.CancelTab999 => _contextBuilder.BuildForPzvActions(),
                    PzvActionType.StartWork => _contextBuilder.BuildForPzvActions(),
                    PzvActionType.CancelStartWork => _contextBuilder.BuildForPzvActions(),
                    PzvActionType.StopWork => _contextBuilder.BuildForPzvActions(),
                    PzvActionType.CancelStopWork => _contextBuilder.BuildForPzvActions(),
                    PzvActionType.ConfirmMaster => _contextBuilder.BuildForPzvActions(),
                    PzvActionType.CancelMasterConfirmation => _contextBuilder.BuildForPzvActions(),

                    _ => _contextBuilder.BuildForPzvActions()
                };

                Debug.WriteLine($"ACTION = {action}");
                Debug.WriteLine($"context.CurrentShiftAssignment null = {context.CurrentShiftAssignment == null}");
                Debug.WriteLine($"context.CurrentShiftAssignment type = {context.CurrentShiftAssignment?.GetType().FullName}");

                var result = await _actionRouter.ExecuteAsync(action, context, CancellationToken.None);

                if (!result.Success && !string.IsNullOrWhiteSpace(result.ErrorMessage))
                    MessageBox.Show(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //try
            //{
            //    var context = _contextBuilder.Build();

            //    //var result = await GridOverlayLoader.RunTaskWithOverlayAsync(
            //    //    gridControlPZVOperList,
            //    //    () => _actionRouter.ExecuteAsync(action, context, CancellationToken.None),
            //    //    CancellationToken.None);

            //    var result = await _actionRouter.ExecuteAsync(action, context, CancellationToken.None);

            //    if (!result.Success && !string.IsNullOrWhiteSpace(result.ErrorMessage))
            //        MessageBox.Show(result.ErrorMessage);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        private async void gridViewPZVOperList_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                Debug.WriteLine($"gridViewPZVOperList_CellValueChanged triggered for Column={e.Column.FieldName}, RowHandle={e.RowHandle}");
                if (e.Column.FieldName != gridPZVOperListColumnOlKol.FieldName)
                    return;

                var view = (GridView)sender;
                var currentItem = view.GetRow(e.RowHandle) as PZVOperList;
                if (currentItem == null) return;
                var _xTopRowIndex = view.TopRowIndex;
                var state = _gridHelper.CaptureState<PZVOperList>(
                    gridViewPZVOperList,
                    _pZVOperListByPachListBindingSource,
                    x => x.olPzvID.ToString());

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
                    string _xColumn = view.FocusedColumn.ToString();
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
                    //_gridHelper.GoToRowById<PZVOperList, int>(gridViewPZVOperList, 
                    //    _pZVOperListByPachListBindingSource, 
                    //    x => x.olPzvID, 
                    //    xPzvID, 
                    //    _xColumn);

                    _gridHelper.RestoreState<PZVOperList>(
                        gridViewPZVOperList,
                        _pZVOperListByPachListBindingSource,
                        state,
                        x => x.olPzvID.ToString());

                    _gridHelper.GoToRowById<PZVOperList, int>(
                        view,
                        _pZVOperListByPachListBindingSource,
                        x => x.olPzvID,
                        _xPzvID,
                        _xColumn);
                    view.TopRowIndex = _xTopRowIndex;
                    Debug.WriteLine($"Finished processing CellValueChanged for olPzvID={currentItem.olPzvID}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в gridViewPZVOperList_CellValueChanged: {ex.Message}");
            }
        }
        //private async void gridViewPZVOperList_DoubleClick(object sender, EventArgs e)
        //{
        //    var view = (GridView)sender;
        //    var pt = view.GridControl.PointToClient(Control.MousePosition);
        //    var hit = view.CalcHitInfo(pt);

        //    if (!hit.InRowCell || hit.RowHandle < 0)
        //        return;

        //    var current = _pZVOperListByPachListBindingSource.Current as PZVOperList;
        //    if (current == null)
        //        return;

        //    current.SyncSelection = 1;
        //    _xPzvID = current.olPzvID;
        //    _xColumn = hit.Column?.FieldName ?? string.Empty;

        //    PzvActionType? action = ResolveDoubleClickAction(hit.Column?.FieldName, current);
        //    if (action == null)
        //        return;

        //    await ExecutePzvActionAsync(action.Value);
        //}
        private async void gridViewPZVOperList_DoubleClick(object sender, EventArgs e)
        {
            var view = (GridView)sender;
            var pt = view.GridControl.PointToClient(Control.MousePosition);
            var hit = view.CalcHitInfo(pt);

            // 👇 1. Заголовок
            if (hit.InColumn)
            {
                var _xTopRowIndex = view.TopRowIndex;
                int xSelected = Convert.ToInt32(view.GetRowCellValue(0, gridPZVOperListColumnSyncSelection));
                int newValue = xSelected == 0 ? 1 : 0;

                _gridHelper.SetValueForFilteredRecordsInGrid(view, gridPZVOperListColumnSyncSelection, newValue);
                view.TopRowIndex = _xTopRowIndex;
            }

            // 👇 2. Ячейка
            if (hit.InRowCell)
            {
                var row = view.GetRow(hit.RowHandle) as PZVOperList;
                if (row == null) return;
                var _xTopRowIndex = view.TopRowIndex;

                foreach (var _row in _pZVOperListByPachListBindingSource.List.OfType<PZVOperList>())
                {
                    _row.SyncSelection = _row.olPzvID == row.olPzvID ? 1 : 0;
                }

                view.PostEditor();
                _pZVOperListByPachListBindingSource.ResetBindings(false);
                view.RefreshData();

                _xPzvID = row.olPzvID;
                _xColumn = hit.Column?.FieldName ?? string.Empty;

                var action = ResolveDoubleClickAction(hit.Column?.FieldName, row);

                if (action.HasValue)
                    await ExecutePzvActionAsync(action.Value);
                view.TopRowIndex = _xTopRowIndex;
            }
        }
        private PzvActionType? ResolveDoubleClickAction(string? fieldName, PZVOperList row)
        {
            return fieldName switch
            {
                "olKmlNumber" or "olPzvDateNaznKm" =>
                    row.olPzvDateNaznKm == null
                        ? PzvActionType.AssignKnittingMachine
                        : PzvActionType.CancelKnittingMachine,

                "olPzvTab" or "olPzvDateNaznTab" =>
                    row.olPzvDateNaznTab == null
                        ? PzvActionType.AssignTab
                        : PzvActionType.CancelTab,

                "olPzvDateStart" =>
                    row.olPzvDateStart == null
                        ? PzvActionType.StartWork
                        : PzvActionType.CancelStartWork,

                "olPzvDateEnd" =>
                    row.olPzvDateEnd == null
                        ? PzvActionType.StopWork
                        : PzvActionType.CancelStopWork,

                "olPzvDateMast" =>
                    row.olPzvDateMast == null
                        ? PzvActionType.ConfirmMaster
                        : PzvActionType.CancelMasterConfirmation,

                _ => null
            };
        }
        private void gridViewPZVOperList_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            //var current = _pZVOperListByPachListBindingSource.Current as PZVOperList;
            var current = _pZVOperListByPachListBindingSource.Current as PZVOperList;
            _xPzvID = current?.olPzvID ?? 0;
        }

        private void repositoryItemCheckEdit1_EditValueChanged(object sender, EventArgs e)
        {
            BeginInvoke(new Action(SyncSelectionUpdate));
        }

        private async void repositoryItemCheckEdit5_EditValueChanged(object sender, EventArgs e)
        {
            //BeginInvoke(new Action(SyncSelectionUpdate));
            try
            {
                Debug.WriteLine("repositoryItemCheckEdit5_EditValueChanged triggered");

                gridViewRzvPachListByNom.PostEditor();
                gridViewRzvPachListByNom.UpdateCurrentRow();
                _rzvPachListByNomBindingSource.EndEdit();
                _rzvPachListByNomBindingSource.CurrencyManager?.EndCurrentEdit();

                var selectedRow = _rzvPachListByNomBindingSource.Current as RzvPachListByNom;
                if (selectedRow != null)
                {
                    string jsonString = JsonConvert.SerializeObject(selectedRow, Formatting.Indented);

                    string query =
                        $"dbo.setGraduationRate @xNomListJson = '{jsonString}', @xGradationValue = {selectedRow.gradacia}, @xPodrKod = 1 ";

                    Task updateRZV = _dbHelper.ExecuteNonQueryAsync(
                        query,
                        new Dictionary<string, object> { });

                    await Task.WhenAll(updateRZV);

                    await GridOverlayLoader.RunTaskWithOverlayAsync(
                        gridControlPZVOperList,
                        LoadPlanZagrVyazByZadanySelection,
                        CancellationToken.None);
                }

                Debug.WriteLine("Finished repositoryItemCheckEdit5_EditValueChanged");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка repositoryItemCheckEdit5_EditValueChanged: {ex.Message}");
            }
        }
        private async void layoutControlGroup6_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            try
            {
                //int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);

                //switch (buttonIndex)
                //{
                //    case 0:
                //        //Debug.WriteLine(ButtonPreliminaryWd.Enabled + " " + ButtonPreliminaryWd.Visible);
                //        MessageBox.Show("Просмотр работы к подтверждению");
                //        break;
                //    case 2:
                //        //Debug.WriteLine(ButtonEditWd.Enabled + " " + ButtonEditWd.Visible);
                //        MessageBox.Show("История по операции");
                //        break;
                //    case 4:
                //        //Debug.WriteLine(customSimpleButton1.Enabled + " " + customSimpleButton1.Visible);
                //        MessageBox.Show("Выгрузить операции в XLS");
                //        break;
                //    case 6:
                //        //Debug.WriteLine(ButtonArchAndCopyWd.Enabled + " " + ButtonArchAndCopyWd.Visible);
                //        //MessageBox.Show("Загрузить операции");
                //        gridViewRzvPachListByNom.FocusedColumn = gridViewRzvPachListByNom.Columns["data_paln"];
                //        gridViewRzvPachListByNom.FocusedColumn = gridViewRzvPachListByNom.Columns["SyncSelection"];
                //        gridViewPZVOperList.ShowLoadingPanel();
                //        await LoadPlanZagrVyazByZadanySelection();
                //        gridViewPZVOperList.HideLoadingPanel();
                //        //await GridOverlayLoader.RunTaskWithOverlayAsync(
                //        //    gridControlPZVOperList,
                //        //    LoadPlanZagrVyazByZadanySelection
                //        //    , CancellationToken.None
                //        //    );
                //        break;
                //    case 8:
                //        gridViewPZVOperList.ShowLoadingPanel();
                //        ClearSelectedPachList();
                //        gridViewPZVOperList.HideLoadingPanel();
                //        //await GridOverlayLoader.RunTaskWithOverlayAsync(
                //        //    gridControlPZVOperList,
                //        //    ClearSelectedPachList
                //        //    , CancellationToken.None
                //        //    );
                //        break;
                //    case 10:
                //        _pZVOperListByPachListBindingSource.Clear();
                //        gridViewRzvPachListByNom.FocusedColumn = gridViewRzvPachListByNom.Columns["data_paln"];
                //        gridViewRzvPachListByNom.FocusedColumn = gridViewRzvPachListByNom.Columns["SyncSelection"];
                        
                //        gridViewPZVOperList.ShowLoadingPanel();
                //        await LoadPlanZagrVyazByZadanySelection();
                //        gridViewPZVOperList.HideLoadingPanel();
                //        //await GridOverlayLoader.RunTaskWithOverlayAsync(
                //        //    gridControlPZVOperList,
                //        //    LoadPlanZagrVyazByZadanySelection
                //        //    , CancellationToken.None
                //        //    );
                //        break;
                //}

                string buttonTag = (e.Button as GroupBoxButton)?.Tag.ToString();
                switch (buttonTag)
                {
                    case "lcg6ViewOperToConfirm":
                        MessageBox.Show("Просмотр работы к подтверждению");
                        break;
                    case "lcg6OperHistory":
                        MessageBox.Show("История по операции");
                        break;
                    case "lcg6OperListPrint":
                        //Debug.WriteLine(customSimpleButton1.Enabled + " " + customSimpleButton1.Visible);
                        MessageBox.Show("Печать списка операций");
                        break;
                    case "lcg6OperListLoad":
                        gridViewRzvPachListByNom.FocusedColumn = gridViewRzvPachListByNom.Columns["data_paln"];
                        gridViewRzvPachListByNom.FocusedColumn = gridViewRzvPachListByNom.Columns["SyncSelection"];
                        gridViewPZVOperList.ShowLoadingPanel();
                        await LoadPlanZagrVyazByZadanySelection();
                        gridViewPZVOperList.HideLoadingPanel();
                        break;
                    case "lcg6OperListClear":
                        gridViewPZVOperList.ShowLoadingPanel();
                        ClearSelectedPachList();
                        gridViewPZVOperList.ClearColumnsFilter();
                        gridViewPZVOperList.HideLoadingPanel();
                        break;
                    case "lcg6OperListRefresh":
                        _pZVOperListByPachListBindingSource.Clear();
                        gridViewRzvPachListByNom.FocusedColumn = gridViewRzvPachListByNom.Columns["data_paln"];
                        gridViewRzvPachListByNom.FocusedColumn = gridViewRzvPachListByNom.Columns["SyncSelection"];

                        gridViewPZVOperList.ShowLoadingPanel();
                        await LoadPlanZagrVyazByZadanySelection();
                        gridViewPZVOperList.HideLoadingPanel();
                        break;
                }
//                Debug.WriteLine($"layoutControlGroup6_CustomButtonClick completed for button index {buttonIndex}");
                Debug.WriteLine($"layoutControlGroup6_CustomButtonClick completed for button tag {buttonTag}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обработки layoutControlGroup6_CustomButtonClick: {ex.Message}");
            }
        }
    }
}