using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace SewingProduction.Core.helpers
{
    public static class GridStateHelper
    {
        public sealed class FlatFocusState<TKey>
        {
            public bool HasKey { get; set; }
            public TKey Key { get; set; }
            public int Position { get; set; }
            public int TopRowIndex { get; set; }
            public string FocusedColumnFieldName { get; set; } = string.Empty;
        }

        public sealed class MasterDetailFocusState<TMasterKey, TDetailKey>
        {
            public bool HasMasterKey { get; set; }
            public TMasterKey MasterKey { get; set; }
            public bool HasDetailKey { get; set; }
            public TDetailKey DetailKey { get; set; }
            public int MasterTop { get; set; }
            public int? DetailTop { get; set; }
            public bool WasInDetail { get; set; }
        }

        public sealed class MasterDetailExpansionState<TMasterKey, TGroupKey>
        {
            public MasterDetailExpansionState(
                HashSet<TMasterKey> expandedMasterKeys,
                HashSet<(TMasterKey MasterKey, TGroupKey GroupKey)> expandedGroupKeys)
            {
                ExpandedMasterKeys = expandedMasterKeys ?? new HashSet<TMasterKey>();
                ExpandedGroupKeys = expandedGroupKeys ?? new HashSet<(TMasterKey MasterKey, TGroupKey GroupKey)>();
            }

            public HashSet<TMasterKey> ExpandedMasterKeys { get; }
            public HashSet<(TMasterKey MasterKey, TGroupKey GroupKey)> ExpandedGroupKeys { get; }
        }

        public static FlatFocusState<TKey>? CaptureFlatFocus<TItem, TKey>(
            GridView view,
            BindingSource bindingSource,
            Func<TItem, TKey> keySelector)
        {
            if (view == null || bindingSource == null || bindingSource.Count == 0 || keySelector == null)
                return null;

            var current = bindingSource.Current is TItem item ? item : default;
            return new FlatFocusState<TKey>
            {
                HasKey = current != null,
                Key = current != null ? keySelector(current) : default,
                Position = bindingSource.Position,
                TopRowIndex = view.TopRowIndex,
                FocusedColumnFieldName = view.FocusedColumn?.FieldName ?? string.Empty
            };
        }

        public static bool RestoreFlatFocus<TItem, TKey>(
            GridView view,
            BindingSource bindingSource,
            FlatFocusState<TKey>? state,
            Func<TItem, TKey> keySelector)
        {
            if (view == null || bindingSource == null || state == null || bindingSource.Count == 0 || keySelector == null)
                return false;

            int dataIndex = -1;
            if (state.HasKey)
                dataIndex = FindDataIndex(bindingSource, keySelector, state.Key);

            if (dataIndex < 0)
                dataIndex = Math.Min(Math.Max(state.Position, 0), bindingSource.Count - 1);

            if (dataIndex < 0)
                return false;

            int rowHandle = view.GetRowHandle(dataIndex);
            if (!view.IsValidRowHandle(rowHandle))
                return false;

            ExpandParentGroups(view, rowHandle);

            view.BeginUpdate();
            try
            {
                view.ClearSelection();
                view.MakeRowVisible(rowHandle);
                view.FocusedRowHandle = rowHandle;

                if (!string.IsNullOrWhiteSpace(state.FocusedColumnFieldName))
                {
                    var col = view.Columns.ColumnByFieldName(state.FocusedColumnFieldName);
                    if (col != null)
                        view.FocusedColumn = col;
                }

                if (view.RowCount > 0)
                {
                    int topRow = Math.Min(Math.Max(state.TopRowIndex, 0), view.RowCount - 1);
                    view.TopRowIndex = topRow;
                }
            }
            finally
            {
                view.EndUpdate();
            }

            return true;
        }

        public static MasterDetailFocusState<TMasterKey, TDetailKey> CaptureMasterDetailFocus<TMaster, TDetail, TMasterKey, TDetailKey>(
            GridControl gridControl,
            BandedGridView masterView,
            Func<TMaster, TMasterKey> masterKeySelector,
            Func<TDetail, TMasterKey> detailMasterKeySelector,
            Func<TDetail, TDetailKey> detailKeySelector)
        {
            var state = new MasterDetailFocusState<TMasterKey, TDetailKey>
            {
                MasterTop = masterView?.TopRowIndex ?? 0
            };

            if (gridControl == null || masterView == null || masterKeySelector == null || detailMasterKeySelector == null || detailKeySelector == null)
                return state;

            var focusedView = gridControl.FocusedView as GridView;
            state.WasInDetail = focusedView != null && focusedView != masterView;

            if (state.WasInDetail && focusedView?.GetFocusedRow() is TDetail detailRow)
            {
                state.HasMasterKey = true;
                state.MasterKey = detailMasterKeySelector(detailRow);
                state.HasDetailKey = true;
                state.DetailKey = detailKeySelector(detailRow);
                state.DetailTop = focusedView.TopRowIndex;
                return state;
            }

            if (masterView.GetFocusedRow() is TMaster masterRow)
            {
                state.HasMasterKey = true;
                state.MasterKey = masterKeySelector(masterRow);
            }

            return state;
        }

        public static MasterDetailFocusState<TMasterKey, TDetailKey> CaptureMasterDetailFocusFromRow<TDetail, TMasterKey, TDetailKey>(
            BandedGridView masterView,
            GridView detailView,
            TDetail currentRow,
            Func<TDetail, TMasterKey> masterKeySelector,
            Func<TDetail, TDetailKey> detailKeySelector)
        {
            var state = new MasterDetailFocusState<TMasterKey, TDetailKey>
            {
                MasterTop = masterView?.TopRowIndex ?? 0,
                WasInDetail = detailView != null && detailView != masterView
            };

            if (currentRow == null || masterKeySelector == null || detailKeySelector == null)
                return state;

            state.HasMasterKey = true;
            state.MasterKey = masterKeySelector(currentRow);
            state.HasDetailKey = true;
            state.DetailKey = detailKeySelector(currentRow);
            if (detailView != null)
                state.DetailTop = detailView.TopRowIndex;

            return state;
        }

        public static int FindDataRowHandleByKey<TItem, TKey>(
            GridView view,
            Func<TItem, TKey> keySelector,
            TKey key)
        {
            if (view == null || keySelector == null)
                return GridControl.InvalidRowHandle;

            for (int rowHandle = 0; rowHandle < view.RowCount; rowHandle++)
            {
                if (!view.IsDataRow(rowHandle))
                    continue;

                if (view.GetRow(rowHandle) is not TItem row)
                    continue;

                if (EqualityComparer<TKey>.Default.Equals(keySelector(row), key))
                    return rowHandle;
            }

            return GridControl.InvalidRowHandle;
        }

        public static void RestoreMasterDetailFocus<TMaster, TDetail, TMasterKey, TDetailKey>(
            Control uiHost,
            BandedGridView masterView,
            MasterDetailFocusState<TMasterKey, TDetailKey> state,
            Func<TMaster, TMasterKey> masterKeySelector,
            Func<TDetail, TDetailKey> detailKeySelector,
            TDetailKey preferredDetailKey,
            bool usePreferredDetailKey)
        {
            if (uiHost == null || masterView == null || state == null || !state.HasMasterKey || masterKeySelector == null || detailKeySelector == null)
                return;

            int masterHandle = FindDataRowHandleByKey(masterView, masterKeySelector, state.MasterKey);
            if (masterHandle < 0)
                return;

            masterView.BeginUpdate();
            try
            {
                masterView.FocusedRowHandle = masterHandle;
                masterView.MakeRowVisible(masterHandle, true);

                if (masterView.RowCount > 0)
                {
                    int masterTop = Math.Min(Math.Max(state.MasterTop, 0), masterView.RowCount - 1);
                    masterView.TopRowIndex = masterTop;
                }
            }
            finally
            {
                masterView.EndUpdate();
            }

            bool shouldRestoreDetail = state.WasInDetail || usePreferredDetailKey;
            if (!shouldRestoreDetail)
                return;

            TDetailKey detailKey = usePreferredDetailKey ? preferredDetailKey : state.DetailKey;
            bool hasDetailKey = usePreferredDetailKey || state.HasDetailKey;
            if (!hasDetailKey)
                return;

            if (!masterView.GetMasterRowExpanded(masterHandle))
                masterView.ExpandMasterRow(masterHandle);

            uiHost.BeginInvoke(new Action(() =>
            {
                var detailView = masterView.GetDetailView(masterHandle, 0) as GridView;
                if (detailView == null)
                    return;

                int detailRowHandle = FindDataRowHandleByKey(detailView, detailKeySelector, detailKey);
                if (detailRowHandle < 0)
                    return;

                detailView.FocusedRowHandle = detailRowHandle;
                detailView.MakeRowVisible(detailRowHandle, true);

                if (state.DetailTop.HasValue && detailView.RowCount > 0)
                {
                    int detailTop = Math.Min(Math.Max(state.DetailTop.Value, 0), detailView.RowCount - 1);
                    detailView.TopRowIndex = detailTop;
                }
            }));
        }

        public static MasterDetailExpansionState<TMasterKey, TGroupKey> CaptureMasterDetailExpansion<TMaster, TMasterKey, TGroupKey>(
            BandedGridView masterView,
            Func<TMaster, TMasterKey> masterKeySelector,
            Func<GridView, int, TGroupKey> groupKeySelector)
        {
            if (masterView == null || masterView.RowCount == 0 || masterKeySelector == null || groupKeySelector == null)
                return new MasterDetailExpansionState<TMasterKey, TGroupKey>(
                    new HashSet<TMasterKey>(),
                    new HashSet<(TMasterKey MasterKey, TGroupKey GroupKey)>());

            var masterKeys = new HashSet<TMasterKey>();
            var groupKeys = new HashSet<(TMasterKey MasterKey, TGroupKey GroupKey)>();

            for (int rowHandle = 0; rowHandle < masterView.RowCount; rowHandle++)
            {
                if (!masterView.IsDataRow(rowHandle))
                    continue;

                if (!masterView.IsMasterRow(rowHandle) || !masterView.GetMasterRowExpanded(rowHandle))
                    continue;

                if (masterView.GetRow(rowHandle) is not TMaster masterRow)
                    continue;

                var masterKey = masterKeySelector(masterRow);
                masterKeys.Add(masterKey);

                var detailView = masterView.GetDetailView(rowHandle, 0) as GridView;
                if (detailView == null)
                    continue;

                for (int groupRowHandle = 0; groupRowHandle < detailView.RowCount; groupRowHandle++)
                {
                    if (!detailView.IsGroupRow(groupRowHandle) || detailView.GetRowLevel(groupRowHandle) != 0)
                        continue;

                    if (!detailView.GetRowExpanded(groupRowHandle))
                        continue;

                    groupKeys.Add((masterKey, groupKeySelector(detailView, groupRowHandle)));
                }
            }

            return new MasterDetailExpansionState<TMasterKey, TGroupKey>(masterKeys, groupKeys);
        }

        public static void RestoreMasterDetailExpansion<TMaster, TMasterKey, TGroupKey>(
            BandedGridView masterView,
            MasterDetailExpansionState<TMasterKey, TGroupKey> state,
            Func<TMaster, TMasterKey> masterKeySelector,
            Func<GridView, int, TGroupKey> groupKeySelector)
        {
            if (masterView == null || state == null || masterKeySelector == null || groupKeySelector == null)
                return;

            masterView.BeginUpdate();
            try
            {
                for (int rowHandle = 0; rowHandle < masterView.RowCount; rowHandle++)
                {
                    if (!masterView.IsDataRow(rowHandle))
                        continue;

                    if (masterView.GetRow(rowHandle) is not TMaster masterRow)
                        continue;

                    var masterKey = masterKeySelector(masterRow);
                    bool shouldExpandMaster = state.ExpandedMasterKeys.Contains(masterKey);

                    if (masterView.IsMasterRow(rowHandle))
                        masterView.SetMasterRowExpanded(rowHandle, shouldExpandMaster);

                    if (!shouldExpandMaster)
                        continue;

                    var detailView = masterView.GetDetailView(rowHandle, 0) as GridView;
                    if (detailView == null)
                        continue;

                    detailView.BeginUpdate();
                    try
                    {
                        for (int groupRowHandle = 0; groupRowHandle < detailView.RowCount; groupRowHandle++)
                        {
                            if (!detailView.IsGroupRow(groupRowHandle) || detailView.GetRowLevel(groupRowHandle) != 0)
                                continue;

                            var groupKey = groupKeySelector(detailView, groupRowHandle);
                            bool shouldExpandGroup = state.ExpandedGroupKeys.Contains((masterKey, groupKey));
                            detailView.SetRowExpanded(groupRowHandle, shouldExpandGroup);
                        }
                    }
                    finally
                    {
                        detailView.EndUpdate();
                    }
                }
            }
            finally
            {
                masterView.EndUpdate();
            }
        }

        private static int FindDataIndex<TItem, TKey>(
            BindingSource bindingSource,
            Func<TItem, TKey> keySelector,
            TKey key)
        {
            for (int i = 0; i < bindingSource.Count; i++)
            {
                if (bindingSource[i] is not TItem item)
                    continue;

                if (EqualityComparer<TKey>.Default.Equals(keySelector(item), key))
                    return i;
            }

            return -1;
        }

        private static void ExpandParentGroups(GridView view, int rowHandle)
        {
            int parent = view.GetParentRowHandle(rowHandle);
            while (parent != GridControl.InvalidRowHandle)
            {
                view.SetRowExpanded(parent, true);
                parent = view.GetParentRowHandle(parent);
            }
        }
    }
}
