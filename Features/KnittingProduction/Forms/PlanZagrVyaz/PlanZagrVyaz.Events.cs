using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.helpers;
using SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Routing;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                var context = _contextBuilder.Build();

                //var result = await GridOverlayLoader.RunTaskWithOverlayAsync(
                //    gridControlPZVOperList,
                //    () => _actionRouter.ExecuteAsync(action, context, CancellationToken.None),
                //    CancellationToken.None);

                var result = await _actionRouter.ExecuteAsync(action, context, CancellationToken.None);

                if (!result.Success && !string.IsNullOrWhiteSpace(result.ErrorMessage))
                    MessageBox.Show(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                    Debug.WriteLine($"Finished processing CellValueChanged for olPzvID={currentItem.olPzvID}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в gridViewPZVOperList_CellValueChanged: {ex.Message}");
            }
        }
        private async void gridViewPZVOperList_DoubleClick(object sender, EventArgs e)
        {
            var view = (GridView)sender;
            var pt = view.GridControl.PointToClient(Control.MousePosition);
            var hit = view.CalcHitInfo(pt);

            if (!hit.InRowCell || hit.RowHandle < 0)
                return;

            var current = _pZVOperListByPachListBindingSource.Current as PZVOperList;
            if (current == null)
                return;

            current.SyncSelection = 1;
            _xPzvID = current.olPzvID;
            _xColumn = hit.Column?.FieldName ?? string.Empty;

            PzvActionType? action = ResolveDoubleClickAction(hit.Column?.FieldName, current);
            if (action == null)
                return;

            await ExecutePzvActionAsync(action.Value);
        }

        private PzvActionType? ResolveDoubleClickAction(string? fieldName, PZVOperList row)
        {
            return fieldName switch
            {
                "olKmlNumber" or "olPvDateNaznKm" =>
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
            var current = _pZVOperListByPachListBindingSource.Current as PZVOperList;
            _xPzvID = current?.olPzvID ?? 0;
        }

        private void repositoryItemCheckEdit1_EditValueChanged(object sender, EventArgs e)
        {
            BeginInvoke(new Action(SyncSelectionUpdate));
        }

        private void repositoryItemCheckEdit5_EditValueChanged(object sender, EventArgs e)
        {
            BeginInvoke(new Action(SyncSelectionUpdate));
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
                Debug.WriteLine($"layoutControlGroup6_CustomButtonClick completed for button index {buttonIndex}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обработки layoutControlGroup6_CustomButtonClick: {ex.Message}");
            }
        }
    }
}