using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using System;
using System.Windows.Forms;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    public sealed class KnitterPlanFocusService
    {
        private readonly Control _uiHost;
        private readonly GridControl _gridControl;
        private readonly BandedGridView _masterView;

        public KnitterPlanFocusService(Control uiHost, GridControl gridControl, BandedGridView masterView)
        {
            _uiHost = uiHost ?? throw new ArgumentNullException(nameof(uiHost));
            _gridControl = gridControl ?? throw new ArgumentNullException(nameof(gridControl));
            _masterView = masterView ?? throw new ArgumentNullException(nameof(masterView));
        }

        public PlanFocusSnap CaptureCurrent()
        {
            var snap = new PlanFocusSnap
            {
                MasterTop = _masterView.TopRowIndex
            };

            var focusedView = _gridControl.FocusedView as GridView;
            snap.WasInDetail = focusedView != null && focusedView != _masterView;

            if (_masterView.GetFocusedRow() is KnitterPZVModel masterRow)
            {
                snap.TaskNum = KnitterPlanUtils.NormalizeTaskNum(masterRow.pzvNomZad);
                snap.Machine = KnitterPlanUtils.NormalizeMachineKey(masterRow.kmlNumber);
            }

            if (snap.WasInDetail && focusedView?.GetFocusedRow() is KnitterPZVModel detailRow && detailRow.pzvID > 0)
            {
                snap.DetailPzvId = detailRow.pzvID;
                snap.DetailTop = focusedView.TopRowIndex;
            }

            return snap;
        }

        public PlanFocusSnap CaptureFromRow(KnitterPZVModel? currentRow, GridView detailView)
        {
            var snap = new PlanFocusSnap
            {
                MasterTop = _masterView?.TopRowIndex ?? 0,
                WasInDetail = detailView != null && detailView != _masterView
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

        public int FindMasterHandleBySnap(PlanFocusSnap snap)
        {
            if (snap == null)
                return GridControl.InvalidRowHandle;

            for (int rowHandle = 0; rowHandle < _masterView.RowCount; rowHandle++)
            {
                if (!_masterView.IsDataRow(rowHandle))
                    continue;

                if (_masterView.GetRow(rowHandle) is not KnitterPZVModel row)
                    continue;

                if (KnitterPlanUtils.NormalizeTaskNum(row.pzvNomZad) == snap.TaskNum &&
                    KnitterPlanUtils.NormalizeMachineKey(row.kmlNumber) == snap.Machine)
                {
                    return rowHandle;
                }
            }

            return GridControl.InvalidRowHandle;
        }

        public void Restore(PlanFocusSnap snap, int? preferDetailId = null)
        {
            if (snap == null)
                return;

            int masterHandle = FindMasterHandleBySnap(snap);
            if (masterHandle < 0)
                return;

            _masterView.FocusedRowHandle = masterHandle;
            _masterView.MakeRowVisible(masterHandle, true);

            int? targetDetailId = preferDetailId ?? snap.DetailPzvId;
            if (!targetDetailId.HasValue || targetDetailId.Value <= 0)
                return;

            if (!_masterView.GetMasterRowExpanded(masterHandle))
                _masterView.ExpandMasterRow(masterHandle);

            _uiHost.BeginInvoke(new Action(() =>
            {
                var detailView = _masterView.GetDetailView(masterHandle, 0) as GridView;
                if (detailView == null)
                    return;

                int detailRowHandle = detailView.LocateByValue("pzvID", targetDetailId.Value);
                if (detailRowHandle < 0)
                    return;

                detailView.FocusedRowHandle = detailRowHandle;
                detailView.MakeRowVisible(detailRowHandle, true);
            }));
        }
    }
}
