using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.helpers;
using SewingProduction.Features.KnittingProduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static SewingProduction.Core.helpers.BindingSourceHelper;

namespace SewingProduction.Features.KnittingProduction.Forms.PZVForm.Adapters
{
    public sealed class PzvOperationsGridUpdater
    {
        private readonly GridView _view;
        private readonly BindingSource _bindingSource;
        private readonly Func<int> _getTopRowIndex;
        private readonly Action<int> _setTopRowIndex;

        public PzvOperationsGridUpdater(
            GridView view,
            BindingSource bindingSource,
            Func<int> getTopRowIndex,
            Action<int> setTopRowIndex)
        {
            _view = view;
            _bindingSource = bindingSource;
            _getTopRowIndex = getTopRowIndex;
            _setTopRowIndex = setTopRowIndex;
        }

        //public void Apply(IReadOnlyList<PZVOperList> newRows)
        //{
        //    var currentRows = _bindingSource.List.Cast<PZVOperList>().ToList();
        //    int topRow = _getTopRowIndex();

        //    _view.BeginDataUpdate();
        //    try
        //    {
        //        var changes = GetChanges(
        //            currentRows,
        //            newRows.ToList(),
        //            x => x.olPzvID);

        //        ApplyChanges(_bindingSource, changes);
        //        //RemoveMissingSmart(_bindingSource, newRows.Select(x => x.olPzvID).ToHashSet(), x => x.olPzvID);
        //        RemoveMissingSmart(
        //            _bindingSource,
        //            changes.Removed,
        //            _view.GridControl);

        //        _bindingSource.ResetBindings(false);
        //        _view.RefreshData();

        //        if (topRow >= 0)
        //            _view.TopRowIndex = topRow;

        //        _setTopRowIndex(_view.TopRowIndex);
        //    }
        //    finally
        //    {
        //        _view.EndDataUpdate();
        //    }
        //}
        public void Apply(IReadOnlyList<PZVOperList> newRows)
        {
            var currentRows = _bindingSource.List.Cast<PZVOperList>().ToList();
            int topRow = _getTopRowIndex();

            _view.BeginDataUpdate();
            try
            {
                var changes = BindingSourceHelper.GetChanges(
                    currentRows,
                    newRows,
                    x => x.olPzvID,
                    BindingSourceHelper.HashMode.ExcludeOnly,
                    "IsNew", "IsModified", "IsDeleted", "SyncSelection", "ErrorSelection");

                BindingSourceHelper.ApplyChanges(
                    _bindingSource,
                    changes,
                    x => x.olPzvID,
                    BindingSourceHelper.UpdateFieldsMode.ExcludeOnly,
                    _view,
                    fields: new[] { "IsNew", "IsModified", "IsDeleted", "SyncSelection", "ErrorSelection" });

                BindingSourceHelper.RemoveMissingSmart<PZVOperList, int>(
                    _bindingSource,
                    newRows.Select(x => x.olPzvID),
                    x => x.olPzvID,
                    _view.GridControl);
                if (changes != null)
                {
                    changes.Added?.Clear();
                    changes.Modified?.Clear();
                    changes.Removed?.Clear();
                    changes = null;
                }

                _bindingSource.ResetBindings(false);
                
                _view.RefreshData();

                //_view.ExpandAllGroups();

                //if (topRow >= 0)
                //    _view.TopRowIndex = topRow;

                //_setTopRowIndex(_view.TopRowIndex);
            }
            finally
            {
                _view.EndDataUpdate();
                _view.ExpandAllGroups();
                if (topRow >= 0)
                    _view.TopRowIndex = topRow;

                _view.BeginSort();
                _view.ClearSorting();
                _view.SortInfo.Add(new GridColumnSortInfo(_view.Columns["olPzvArticul"], ColumnSortOrder.Ascending));
                _view.SortInfo.Add(new GridColumnSortInfo(_view.Columns["olNPach"], ColumnSortOrder.Ascending));
                _view.SortInfo.Add(new GridColumnSortInfo(_view.Columns["olNo"], ColumnSortOrder.Ascending));
                _view.SortInfo.Add(new GridColumnSortInfo(_view.Columns["olNpo"], ColumnSortOrder.Ascending));
                _view.SortInfo.Add(new GridColumnSortInfo(_view.Columns["olPzvIDParent"], ColumnSortOrder.Ascending));
                _view.SortInfo.Add(new GridColumnSortInfo(_view.Columns["olPzvID"], ColumnSortOrder.Ascending));
                _view.EndSort();

                _setTopRowIndex(_view.TopRowIndex);
            }
        }
    }
}