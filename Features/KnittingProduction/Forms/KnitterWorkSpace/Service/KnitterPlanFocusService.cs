using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.helpers;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using System;
using System.Windows.Forms;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    public sealed class KnitterPlanFocusService
    {
        private static readonly Func<KnitterPZVModel, (string TaskNum, string MachineKey)> MasterKeySelector =
            row => (
                KnitterPlanUtils.NormalizeTaskNum(row.pzvNomZad),
                KnitterPlanUtils.NormalizeMachineKey(row.kmlNumber));

        private readonly Control _uiHost;
        private readonly GridControl _gridControl;
        private readonly BandedGridView _masterView;

        public KnitterPlanFocusService(Control uiHost, GridControl gridControl, BandedGridView masterView)
        {
            _uiHost = uiHost ?? throw new ArgumentNullException(nameof(uiHost));
            _gridControl = gridControl ?? throw new ArgumentNullException(nameof(gridControl));
            _masterView = masterView ?? throw new ArgumentNullException(nameof(masterView));
        }

        public GridStateHelper.MasterDetailFocusState<(string TaskNum, string MachineKey), int> CaptureCurrent() =>
            GridStateHelper.CaptureMasterDetailFocus<KnitterPZVModel, KnitterPZVModel, (string TaskNum, string MachineKey), int>(
                _gridControl,
                _masterView,
                MasterKeySelector,
                MasterKeySelector,
                row => row.pzvID);

        public GridStateHelper.MasterDetailFocusState<(string TaskNum, string MachineKey), int> CaptureFromRow(KnitterPZVModel? currentRow, GridView detailView) =>
            currentRow == null
                ? new GridStateHelper.MasterDetailFocusState<(string TaskNum, string MachineKey), int> { MasterTop = _masterView?.TopRowIndex ?? 0 }
                : GridStateHelper.CaptureMasterDetailFocusFromRow<KnitterPZVModel, (string TaskNum, string MachineKey), int>(
                    _masterView,
                    detailView,
                    currentRow,
                    MasterKeySelector,
                    row => row.pzvID);

        public int FindMasterHandleBySnap(GridStateHelper.MasterDetailFocusState<(string TaskNum, string MachineKey), int> snap)
        {
            if (snap == null || !snap.HasMasterKey)
                return GridControl.InvalidRowHandle;

            return GridStateHelper.FindDataRowHandleByKey(
                _masterView,
                MasterKeySelector,
                snap.MasterKey);
        }

        public void Restore(GridStateHelper.MasterDetailFocusState<(string TaskNum, string MachineKey), int> snap, int? preferDetailId = null)
        {
            GridStateHelper.RestoreMasterDetailFocus<KnitterPZVModel, KnitterPZVModel, (string TaskNum, string MachineKey), int>(
                _uiHost,
                _masterView,
                snap,
                MasterKeySelector,
                row => row.pzvID,
                preferDetailId ?? 0,
                preferDetailId.HasValue && preferDetailId.Value > 0);
        }
    }
}
