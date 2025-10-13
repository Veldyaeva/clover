using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.Models;
using SewingProduction.Interfaces;

namespace SewingProduction.Features.TeamWork.Operations
{
    public sealed class RaszOperationsController
    {
        private readonly BindingList<NormRasz> _normRaszList;
        private readonly GridControl _gridControl;
        private readonly GridView _gridView;
        private readonly BindingSource _bindingSource;
        private readonly ILogger _logger;
        private readonly Action _sortGridView;
        private readonly Action<NormRasz, bool> _applyPostStructureUi;

        // DnD state
        private Point _dragStartPoint;
        private int _dragSourceHandle = -1;
        private bool _dragging = false;

        public RaszOperationsController(
            BindingList<NormRasz> normRaszList,
            GridControl gridControl,
            GridView gridView,
            BindingSource bindingSource,
            ILogger logger,
            Action sortGridView,
            Action<NormRasz, bool> applyPostStructureUi)
        {
            _normRaszList = normRaszList;
            _gridControl = gridControl;
            _gridView = gridView;
            _bindingSource = bindingSource;
            _logger = logger;
            _sortGridView = sortGridView;
            _applyPostStructureUi = applyPostStructureUi;
        }

        public void Attach()
        {
            if (_gridControl == null || _gridView == null) return;
            _gridControl.AllowDrop = true;
            _gridView.MouseDown += GridView_MouseDown;
            _gridView.MouseMove += GridView_MouseMove;
            _gridControl.DragOver += GridControl_DragOver;
            _gridControl.DragDrop += GridControl_DragDrop;
            _gridControl.DragLeave += GridControl_DragLeave;
            _gridControl.QueryContinueDrag += GridControl_QueryContinueDrag;
        }

        public void Detach()
        {
            if (_gridControl == null || _gridView == null) return;
            try { _gridView.MouseDown -= GridView_MouseDown; } catch { }
            try { _gridView.MouseMove -= GridView_MouseMove; } catch { }
            try { _gridControl.DragOver -= GridControl_DragOver; } catch { }
            try { _gridControl.DragDrop -= GridControl_DragDrop; } catch { }
            try { _gridControl.DragLeave -= GridControl_DragLeave; } catch { }
            try { _gridControl.QueryContinueDrag -= GridControl_QueryContinueDrag; } catch { }
            _dragging = false;
            _dragSourceHandle = -1;
        }

        private void GridView_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                _dragStartPoint = e.Location;
                var hit = _gridView.CalcHitInfo(e.Location);
                _dragSourceHandle = hit.RowHandle;
                _dragging = false;
            }
            catch { }
        }

        private void GridView_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
                if (_dragSourceHandle < 0) return;

                Size dragSize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(
                    new Point(_dragStartPoint.X - dragSize.Width / 2, _dragStartPoint.Y - dragSize.Height / 2),
                    dragSize);

                if (!dragRect.Contains(e.Location))
                {
                    var draggedList = GetSelectedRaszRowsOrCurrent();
                    if (draggedList == null || draggedList.Count == 0) return;

                    _dragging = true;
                    var data = new DataObject();
                    data.SetData(typeof(List<NormRasz>), draggedList);
                    data.SetData(typeof(NormRasz), draggedList[0]);
                    _gridControl.DoDragDrop(data, DragDropEffects.Move);
                }
            }
            catch { }
        }

        private void GridControl_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                if (!e.Data.GetDataPresent(typeof(List<NormRasz>)) && !e.Data.GetDataPresent(typeof(NormRasz)))
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }

                Point clientPoint = _gridControl.PointToClient(new Point(e.X, e.Y));
                var hit = _gridView.CalcHitInfo(clientPoint);

                List<NormRasz> draggedList = e.Data.GetData(typeof(List<NormRasz>)) as List<NormRasz>;
                if (draggedList == null)
                {
                    var single = e.Data.GetData(typeof(NormRasz)) as NormRasz;
                    if (single != null) draggedList = new List<NormRasz> { single };
                }

                if (hit.InRow && hit.RowHandle >= 0)
                {
                    if (_gridView.IsNewItemRow(hit.RowHandle))
                    {
                        e.Effect = DragDropEffects.None;
                    }
                    else
                    {
                        var target = _gridView.GetRow(hit.RowHandle) as NormRasz;
                        if (target == null || (draggedList != null && draggedList.Contains(target)))
                            e.Effect = DragDropEffects.None;
                        else
                            e.Effect = DragDropEffects.Move;
                    }
                }
                else if (_gridView.IsGroupRow(hit.RowHandle))
                {
                    var colN = _gridView.Columns.ColumnByFieldName("N");
                    if (colN == null)
                        e.Effect = DragDropEffects.None;
                    else
                    {
                        var groupValue = _gridView.GetGroupRowValue(hit.RowHandle, colN);
                        if (groupValue == null || !int.TryParse(groupValue.ToString(), out _))
                            e.Effect = DragDropEffects.None;
                        else
                            e.Effect = DragDropEffects.Move;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.Move;
                }

                const int margin = 24;
                var bounds = _gridControl.ClientRectangle;
                if (clientPoint.Y <= bounds.Top + margin)
                {
                    _gridView.TopRowIndex = Math.Max(0, _gridView.TopRowIndex - 1);
                }
                else if (clientPoint.Y >= bounds.Bottom - margin)
                {
                    _gridView.TopRowIndex = _gridView.TopRowIndex + 1;
                }
            }
            catch { e.Effect = DragDropEffects.None; }
        }

        private void GridControl_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                _gridView.BeginDataUpdate();
                if (!_dragging) { return; }
                _dragging = false;

                var draggedList = e.Data.GetData(typeof(List<NormRasz>)) as List<NormRasz>;
                if (draggedList == null)
                {
                    var single = e.Data.GetData(typeof(NormRasz)) as NormRasz;
                    if (single != null) draggedList = new List<NormRasz> { single };
                }
                if (draggedList == null || draggedList.Count == 0) { return; }

                draggedList = draggedList
                    .Where(r => r != null)
                    .Distinct()
                    .OrderBy(r => r.N)
                    .ThenBy(r => r.N1)
                    .ToList();

                Point clientPoint = _gridControl.PointToClient(new Point(e.X, e.Y));
                var hit = _gridView.CalcHitInfo(clientPoint);

                bool ctrl = (Control.ModifierKeys & Keys.Control) == Keys.Control;

                bool changed = false;

                if (_gridView.IsGroupRow(hit.RowHandle))
                {
                    var colN = _gridView.Columns.ColumnByFieldName("N");
                    if (colN != null)
                    {
                        var groupValue = _gridView.GetGroupRowValue(hit.RowHandle, colN);
                        if (groupValue != null && int.TryParse(groupValue.ToString(), out int groupN))
                        {
                            if (!AreDraggedAtGroupEnd(draggedList, groupN))
                            {
                                int afterN1 = GetMaxN1(groupN);
                                foreach (var r in draggedList)
                                    MoveRaszIntoGroup(r, groupN, afterN1++);
                                changed = true;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else if (!(hit.InRow && hit.RowHandle >= 0))
                {
                    if (!AreDraggedAtEndAsTopLevel(draggedList))
                    {
                        int newN = _normRaszList.Any() ? _normRaszList.Max(r => r.N) + 1 : 1;
                        foreach (var r in draggedList)
                        {
                            r.N = newN++;
                            r.N1 = 0;
                            if (!r.IsNew) r.IsModified = true;
                        }
                        changed = true;
                    }
                }
                else
                {
                    if (_gridView.IsNewItemRow(hit.RowHandle)) { return; }

                    var target = _gridView.GetRow(hit.RowHandle) as NormRasz;
                    if (target == null) { return; }
                    if (draggedList.Contains(target)) { return; }

                    bool alt = (Control.ModifierKeys & Keys.Alt) == Keys.Alt;

                    if (alt)
                    {
                        if (!AreDraggedAtStartAsTopLevel(draggedList))
                        {
                            MoveRowsToTopLevelAtStart(draggedList);
                            changed = true;
                        }
                        if (changed)
                        {
                            _bindingSource.ResetBindings(false);
                            _sortGridView();
                            _applyPostStructureUi(draggedList.FirstOrDefault(), draggedList.Count > 1);
                        }
                        return;
                    }

                    if (ctrl && target.N1 == 0)
                    {
                        if (MoveRowsToTopLevelInsertAfter(draggedList, target.N))
                        {
                            changed = true;
                        }
                        if (changed)
                        {
                            _bindingSource.ResetBindings(false);
                            _sortGridView();
                            _applyPostStructureUi(draggedList.FirstOrDefault(), draggedList.Count > 1);
                        }
                        return;
                    }

                    if (draggedList.Count == 1 && ctrl && target.N != draggedList[0].N && target.N1 == 0 && draggedList[0].N1 == 0)
                    {
                        var dragged = draggedList[0];
                        int tmpN = dragged.N;
                        dragged.N = target.N;
                        target.N = tmpN;
                        if (!dragged.IsNew) dragged.IsModified = true;
                        if (!target.IsNew) target.IsModified = true;
                        changed = true;
                    }
                    else
                    {
                        if (draggedList.Any(r => r.N != target.N))
                        {
                            if (!AreDraggedConsecutiveAfterTargetInGroup(draggedList, target))
                            {
                                int afterN1 = target.N1;
                                foreach (var r in draggedList)
                                    MoveRaszIntoGroup(r, target.N, afterN1++);
                                changed = true;
                            }
                        }
                        else
                        {
                            if (!AreDraggedConsecutiveAfterTargetInGroup(draggedList, target))
                            {
                                MoveBlockWithinSameGroup(draggedList, target);
                                changed = true;
                            }
                        }
                    }
                }

                if (changed)
                {
                    _bindingSource.ResetBindings(false);
                    _sortGridView();
                    _applyPostStructureUi(draggedList.FirstOrDefault(), draggedList.Count > 1);
                }
            }
            catch { }
            finally { try { _gridView.EndDataUpdate(); } catch { } }
        }

        private void GridControl_DragLeave(object sender, EventArgs e) { }
        private void GridControl_QueryContinueDrag(object sender, QueryContinueDragEventArgs e) { }

        private List<NormRasz> GetSelectedRaszRowsOrCurrent()
        {
            var result = new List<NormRasz>();
            var selectedHandles = _gridView.GetSelectedRows()
                .Where(h => h >= 0 && !_gridView.IsGroupRow(h) && !_gridView.IsNewItemRow(h))
                .ToArray();

            if (selectedHandles.Length > 0)
            {
                foreach (var h in selectedHandles)
                {
                    if (_gridView.GetRow(h) is NormRasz r && r != null)
                        result.Add(r);
                }
            }
            else if (_dragSourceHandle >= 0)
            {
                var r = _gridView.GetRow(_dragSourceHandle) as NormRasz;
                if (r != null) result.Add(r);
            }

            return result
                .Distinct()
                .OrderBy(r => r.N)
                .ThenBy(r => r.N1)
                .ToList();
        }

        private int GetMaxN1(int groupN)
        {
            var items = _normRaszList.Where(x => x.N == groupN).ToList();
            return items.Count == 0 ? 0 : items.Max(x => x.N1);
        }

        private bool AreDraggedAtGroupEnd(List<NormRasz> dragged, int groupN)
        {
            if (dragged == null || dragged.Count == 0) return true;
            if (dragged.Any(r => r.N != groupN)) return false;
            var othersN1 = _normRaszList.Where(r => r.N == groupN && !dragged.Contains(r)).Select(r => r.N1).ToList();
            int maxOthers = othersN1.Count == 0 ? 0 : othersN1.Max();
            var byN1 = dragged.OrderBy(r => r.N1).ToList();
            for (int i = 0; i < byN1.Count; i++)
            {
                if (byN1[i].N1 != maxOthers + 1 + i) return false;
            }
            return true;
        }

        private bool AreDraggedAtEndAsTopLevel(List<NormRasz> dragged)
        {
            if (dragged == null || dragged.Count == 0) return true;
            if (dragged.Any(r => r.N1 != 0)) return false;
            int maxN = _normRaszList.Any() ? _normRaszList.Max(r => r.N) : 0;
            var draggedByN = dragged.OrderBy(r => r.N).ToList();
            int expectedStart = maxN - draggedByN.Count + 1;
            for (int i = 0; i < draggedByN.Count; i++)
            {
                if (draggedByN[i].N != expectedStart + i) return false;
            }
            return true;
        }

        private bool AreDraggedAtStartAsTopLevel(List<NormRasz> dragged)
        {
            if (dragged == null || dragged.Count == 0) return true;
            if (dragged.Any(r => r.N1 != 0)) return false;
            var draggedByN = dragged.OrderBy(r => r.N).ToList();
            for (int i = 0; i < draggedByN.Count; i++)
            {
                if (draggedByN[i].N != 1 + i) return false;
            }
            return true;
        }

        private void MoveRowsToTopLevelAtStart(List<NormRasz> dragged)
        {
            if (dragged == null || dragged.Count == 0) return;
            int sourceN = dragged[0].N;
            var restOfSource = _normRaszList
                .Where(r => r.N == sourceN && !dragged.Contains(r))
                .OrderBy(r => r.N1)
                .ToList();

            foreach (var op in _normRaszList.Where(r => r.N != sourceN && !dragged.Contains(r)))
            {
                int oldN = op.N;
                op.N = oldN + 1;
                if (!op.IsNew && op.N != oldN) op.IsModified = true;
            }

            var block = dragged.OrderBy(r => r.N1).ToList();
            for (int i = 0; i < block.Count; i++)
            {
                int oldN = block[i].N;
                int oldN1 = block[i].N1;
                block[i].N = 1;
                block[i].N1 = (block.Count == 1) ? 0 : i + 1;
                if (!block[i].IsNew && (block[i].N != oldN || block[i].N1 != oldN1)) block[i].IsModified = true;
            }

            if (restOfSource.Count > 0)
            {
                for (int i = 0; i < restOfSource.Count; i++)
                {
                    var it = restOfSource[i];
                    int oldN = it.N;
                    int oldN1 = it.N1;
                    it.N = 2;
                    it.N1 = i + 1;
                    if (!it.IsNew && (it.N != oldN || it.N1 != oldN1)) it.IsModified = true;
                }
            }
        }

        private bool AreDraggedConsecutiveAfterTargetInGroup(List<NormRasz> dragged, NormRasz target)
        {
            if (target == null || dragged == null || dragged.Count == 0) return false;
            int groupN = target.N;
            var byN1 = dragged.OrderBy(r => r.N1).ToList();
            if (byN1.Any(r => r.N != groupN)) return false;
            for (int i = 0; i < byN1.Count; i++)
            {
                if (byN1[i].N1 != target.N1 + 1 + i) return false;
            }
            return true;
        }

        private bool MoveRowsToTopLevelInsertAfter(List<NormRasz> dragged, int afterN)
        {
            if (dragged == null || dragged.Count == 0) return false;
            foreach (var op in _normRaszList.Where(r => r.N > afterN && !dragged.Contains(r)))
            {
                int oldN = op.N;
                op.N = oldN + 1;
                if (!op.IsNew && op.N != oldN) op.IsModified = true;
            }
            var ordered = dragged.OrderBy(r => r.N).ThenBy(r => r.N1).ToList();
            for (int i = 0; i < ordered.Count; i++)
            {
                var it = ordered[i];
                int oldN = it.N, oldN1 = it.N1;
                it.N = afterN + 1;
                it.N1 = (ordered.Count == 1) ? 0 : i + 1;
                if (!it.IsNew && (it.N != oldN || it.N1 != oldN1)) it.IsModified = true;
            }
            return true;
        }

        private void MoveBlockWithinSameGroup(List<NormRasz> block, NormRasz target)
        {
            OperationNumberingService.MoveBlockWithinSameGroup(_normRaszList, block, target);
        }

        private void MoveRaszIntoGroup(NormRasz dragged, int targetGroupN, int? desiredAfterN1)
        {
            OperationNumberingService.MoveRaszIntoGroup(dragged, targetGroupN, desiredAfterN1, _normRaszList);
        }
    }
}


