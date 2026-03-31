using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    internal sealed class KnitterGridVisualService : IDisposable
    {
        private readonly BandedGridView _masterView;
        private readonly AdvBandedGridView _detailView;
        private readonly GridColumn _statusColumn;
        private readonly Func<IReadOnlyList<KnitterPZVModel>> _getAllRows;
        private readonly Action<Exception, string>? _logError;
        private readonly Color _planFooterColor = Color.LightCoral;
        private readonly Color _factFooterColor = Color.LightSkyBlue;
        private RepositoryItemProgressBar? _statusProgressBar;

        public KnitterGridVisualService(
            BandedGridView masterView,
            AdvBandedGridView detailView,
            GridColumn statusColumn,
            Func<IReadOnlyList<KnitterPZVModel>> getAllRows,
            Action<Exception, string>? logError = null)
        {
            _masterView = masterView ?? throw new ArgumentNullException(nameof(masterView));
            _detailView = detailView ?? throw new ArgumentNullException(nameof(detailView));
            _statusColumn = statusColumn ?? throw new ArgumentNullException(nameof(statusColumn));
            _getAllRows = getAllRows ?? throw new ArgumentNullException(nameof(getAllRows));
            _logError = logError;
        }

        public void Initialize()
        {
            ConfigureDetailGrouping();
            SetupStatusColumn();
            SetupGridFonts();
            AttachEvents();
        }

        public void RefreshStatusColumns()
        {
            _masterView.RefreshData();
            _detailView.RefreshData();
            RefreshFooterSummaries();
        }

        public void RefreshFooterSummaries()
        {
            _masterView.UpdateSummary();
            _detailView.UpdateSummary();
        }

        public void Dispose()
        {
            _masterView.MasterRowExpanded -= MasterView_MasterRowExpanded;
            _masterView.CustomUnboundColumnData -= MasterView_CustomUnboundColumnData;
            _masterView.CustomDrawFooterCell -= MasterView_CustomDrawFooterCell;
            _detailView.CustomDrawGroupRow -= DetailView_CustomDrawGroupRow;
            _statusProgressBar?.Dispose();
        }

        private void AttachEvents()
        {
            _masterView.MasterRowExpanded -= MasterView_MasterRowExpanded;
            _masterView.MasterRowExpanded += MasterView_MasterRowExpanded;
            _masterView.CustomUnboundColumnData -= MasterView_CustomUnboundColumnData;
            _masterView.CustomUnboundColumnData += MasterView_CustomUnboundColumnData;
            _masterView.CustomDrawFooterCell -= MasterView_CustomDrawFooterCell;
            _masterView.CustomDrawFooterCell += MasterView_CustomDrawFooterCell;
            _detailView.CustomDrawGroupRow -= DetailView_CustomDrawGroupRow;
            _detailView.CustomDrawGroupRow += DetailView_CustomDrawGroupRow;
        }

        private void ConfigureDetailGrouping()
        {
            _detailView.BeginUpdate();
            try
            {
                var headerCol = _detailView.Columns.ColumnByFieldName("__Header");
                if (headerCol == null)
                {
                    headerCol = new BandedGridColumn
                    {
                        FieldName = "__Header",
                        Caption = "Header",
                        UnboundType = DevExpress.Data.UnboundColumnType.String,
                        UnboundExpression = "Concat('№пачки: ', [n_pach], ' | Размер: ', [razm], ' | Кол-во: ', [pzvRKol])",
                        Visible = false,
                        OptionsColumn = { ShowInCustomizationForm = false }
                    };
                    _detailView.Columns.Add(headerCol);
                }

                _detailView.ClearGrouping();
                headerCol.GroupIndex = 0;
                _detailView.GroupFormat = "{1}";
                _detailView.OptionsView.ShowGroupedColumns = false;
                _detailView.OptionsView.ShowGroupPanel = false;
                _detailView.OptionsBehavior.AlignGroupSummaryInGroupRow = DefaultBoolean.True;
                _detailView.ExpandAllGroups();
            }
            finally
            {
                _detailView.EndUpdate();
            }
        }

        private void SetupStatusColumn()
        {
            _statusProgressBar = new RepositoryItemProgressBar
            {
                Minimum = 0,
                Maximum = 100,
                ShowTitle = true,
                PercentView = true
            };

            _statusColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            _statusColumn.UnboundExpression = string.Empty;
            _statusColumn.ColumnEdit = _statusProgressBar;
        }

        private void SetupGridFonts()
        {
            try
            {
                const float fontSizeIncrease = 5f;
                ApplyViewFonts(_masterView, fontSizeIncrease);
                ApplyViewFonts(_detailView, fontSizeIncrease);

                Font currentDetailFont = _detailView.Appearance.Row.Font ?? SystemFonts.DefaultFont;
                int headerGroupRowHeight = GetHeaderGroupRowHeight(currentDetailFont);
                if (_detailView.GroupRowHeight < headerGroupRowHeight)
                    _detailView.GroupRowHeight = headerGroupRowHeight;
            }
            catch (Exception ex)
            {
                _logError?.Invoke(ex, nameof(SetupGridFonts));
            }
        }

        private static void ApplyViewFonts(GridView view, float fontSizeIncrease)
        {
            view.OptionsView.EnableAppearanceOddRow = false;
            view.OptionsView.EnableAppearanceEvenRow = false;

            Font currentFont = view.Appearance.Row.Font ?? SystemFonts.DefaultFont;
            Font newFont = new Font(currentFont.FontFamily, currentFont.Size + fontSizeIncrease, currentFont.Style);

            view.Appearance.Row.Font = newFont;
            view.Appearance.HeaderPanel.Font = newFont;
            view.Appearance.FooterPanel.Font = newFont;
            view.Appearance.GroupPanel.Font = newFont;
            view.Appearance.GroupRow.Font = newFont;

            foreach (GridColumn column in view.Columns)
            {
                column.AppearanceCell.Font = newFont;
                column.AppearanceHeader.Font = newFont;
            }
        }

        private static int GetHeaderGroupRowHeight(Font baseFont)
        {
            float largeSize = baseFont.Size + 2f;
            FontStyle largeStyle = baseFont.Style | FontStyle.Bold;
            using var largeFont = new Font(baseFont.FontFamily, largeSize, largeStyle);
            return (int)Math.Ceiling(largeFont.GetHeight() + 6f);
        }

        private void MasterView_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            if (sender is not BandedGridView masterView)
                return;

            var detailView = masterView.GetDetailView(e.RowHandle, e.RelationIndex) as AdvBandedGridView;
            ApplyOperationNameWidth(detailView);
        }

        private static void ApplyOperationNameWidth(AdvBandedGridView? detailView)
        {
            if (detailView == null)
                return;

            const int operationNameWidth = 465;

            detailView.BeginUpdate();
            try
            {
                var operationCol = detailView.Columns.ColumnByFieldName("nrText");
                if (operationCol != null)
                {
                    operationCol.OptionsColumn.FixedWidth = true;
                    operationCol.MinWidth = operationNameWidth;
                    operationCol.MaxWidth = operationNameWidth;
                    operationCol.Width = operationNameWidth;
                }

                var operationBand = detailView.Bands
                    .Cast<GridBand>()
                    .FirstOrDefault(b => b.Columns.Contains(operationCol));
                if (operationBand != null)
                {
                    operationBand.OptionsBand.FixedWidth = true;
                    operationBand.MinWidth = operationNameWidth;
                    operationBand.Width = operationNameWidth;
                }
            }
            finally
            {
                detailView.EndUpdate();
                detailView.LayoutChanged();
            }
        }

        private void DetailView_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            var view = sender as AdvBandedGridView;
            var info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;
            if (view == null || info == null)
                return;

            string groupText = info.GroupText ?? string.Empty;
            string originalText = info.GroupText;
            info.GroupText = string.Empty;
            e.Painter.DrawObject(e.Info);
            info.GroupText = originalText;

            Font baseFont = view.Appearance.GroupRow.Font ?? SystemFonts.DefaultFont;
            float valueSize = baseFont.Size + 1f;
            float labelSize = baseFont.Size - 2f;
            FontStyle valueStyle = baseFont.Style | FontStyle.Bold;
            FontStyle labelStyle = baseFont.Style & ~FontStyle.Bold;

            Rectangle textBounds = info.Bounds;
            int left = info.ButtonBounds.Right;
            if (left > textBounds.Left)
                textBounds = new Rectangle(left, textBounds.Top, Math.Max(0, textBounds.Right - left), textBounds.Height);

            int summaryLeft = GetGroupSummaryLeftEdge(view, info);
            if (summaryLeft > textBounds.Left)
            {
                int width = Math.Max(0, summaryLeft - textBounds.Left - 4);
                textBounds = new Rectangle(textBounds.Left, textBounds.Top, width, textBounds.Height);
            }

            using var valueFont = new Font(baseFont.FontFamily, valueSize, valueStyle);
            using var labelFont = new Font(baseFont.FontFamily, labelSize, labelStyle);
            using var labelBrush = new SolidBrush(Color.DimGray);

            float x = textBounds.Left;
            string[] parts = groupText.Split(new[] { " | " }, StringSplitOptions.None);
            const float blockPadding = 12f;
            const float separatorPadding = 6f;
            var tokens = new List<(string Text, Font Font, float Width, Brush Brush, float LeftInset)>(parts.Length * 3);

            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i];
                if (TrySplitHeaderSegment(part, out string label, out string value))
                {
                    float labelWidth = (float)Math.Ceiling(e.Cache.CalcTextSize($"{label} ", labelFont).Width) + blockPadding;
                    tokens.Add(($"{label} ", labelFont, labelWidth, labelBrush, blockPadding / 2f));

                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        float valueWidth = (float)Math.Ceiling(e.Cache.CalcTextSize(value, valueFont).Width) + blockPadding;
                        tokens.Add((value, valueFont, valueWidth, e.Appearance.GetForeBrush(e.Cache), blockPadding / 2f));
                    }
                }
                else
                {
                    float partWidth = (float)Math.Ceiling(e.Cache.CalcTextSize(part, valueFont).Width) + blockPadding;
                    tokens.Add((part, valueFont, partWidth, e.Appearance.GetForeBrush(e.Cache), blockPadding / 2f));
                }

                if (i < parts.Length - 1)
                {
                    const string separator = " | ";
                    float sepWidth = (float)Math.Ceiling(e.Cache.CalcTextSize(separator, labelFont).Width) + separatorPadding;
                    tokens.Add((separator, labelFont, sepWidth, labelBrush, separatorPadding / 2f));
                }
            }

            foreach (var token in tokens)
            {
                SizeF tokenSize = e.Cache.CalcTextSize(token.Text, token.Font);
                float y = textBounds.Top + Math.Max(0f, (textBounds.Height - tokenSize.Height) / 2f);
                e.Graphics.DrawString(token.Text, token.Font, token.Brush, x + token.LeftInset, y);
                x += token.Width;
            }

            if (view.GetViewInfo() is DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo viewInfo)
            {
                DrawGroupSummaryValue(view, viewInfo, e.RowHandle, "PlanChas_UI", e);
                DrawGroupSummaryValue(view, viewInfo, e.RowHandle, "FactChas_UI", e);
            }

            e.Handled = true;
        }

        private void MasterView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column != _statusColumn || !e.IsGetData)
                return;

            e.Value = 0m;
            if (e.Row is not KnitterPZVModel row)
                return;

            var machineKey = KnitterPlanUtils.NormalizeMachineKey(row.kmlNumber);
            var taskKey = KnitterPlanUtils.NormalizeTaskNum(row.pzvNomZad);
            var rows = _getAllRows()
                .Where(r =>
                    r != null &&
                    string.Equals(KnitterPlanUtils.NormalizeMachineKey(r.kmlNumber), machineKey, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(KnitterPlanUtils.NormalizeTaskNum(r.pzvNomZad), taskKey, StringComparison.OrdinalIgnoreCase))
                .ToList();

            decimal assignedHours = rows.Sum(r => r.PlanChas_UI ?? 0m);
            decimal doneHours = rows.Sum(r => r.FactChas_UI ?? 0m);
            e.Value = assignedHours > 0m
                ? Math.Round(doneHours * 100m / assignedHours, 1)
                : 0m;
        }

        private void MasterView_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            if (e.Column == null)
                return;

            bool isPlan = string.Equals(e.Column.FieldName, "pzvChasNazn", StringComparison.OrdinalIgnoreCase);
            bool isFact = string.Equals(e.Column.FieldName, "pzvNChasi", StringComparison.OrdinalIgnoreCase);
            if (!isPlan && !isFact)
                return;

            ApplyFooterBackColor(e, isPlan);

            var totals = GetGlobalHourTotals();
            decimal globalSum = isPlan ? totals.planTotal : totals.factTotal;

            e.Info.DisplayText = $"РІСЃРµ: {globalSum:0.##}";
            e.Appearance.BackColor = isPlan ? _planFooterColor : _factFooterColor;
            e.Appearance.Options.UseBackColor = true;
        }

        private (decimal planTotal, decimal factTotal) GetGlobalHourTotals()
        {
            var rows = _getAllRows();
            decimal plan = rows.Where(r => r != null).Sum(r => r.PlanChas_UI ?? 0m);
            decimal fact = rows.Where(r => r != null).Sum(r => r.FactChas_UI ?? 0m);
            return (Math.Round(plan, 2), Math.Round(fact, 2));
        }

        private void ApplyFooterBackColor(FooterCellCustomDrawEventArgs e, bool isPlan)
        {
            e.Appearance.BackColor = isPlan ? _planFooterColor : _factFooterColor;
            e.Appearance.Options.UseBackColor = true;
        }

        private static bool TrySplitHeaderSegment(string segment, out string label, out string value)
        {
            label = segment;
            value = string.Empty;
            if (string.IsNullOrWhiteSpace(segment))
                return false;

            int separatorIndex = segment.IndexOf(':');
            if (separatorIndex < 0)
                return false;

            label = segment.Substring(0, separatorIndex + 1);
            value = separatorIndex + 1 < segment.Length
                ? segment.Substring(separatorIndex + 1).TrimStart()
                : string.Empty;
            return true;
        }

        private static int GetGroupSummaryLeftEdge(AdvBandedGridView view, DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo info)
        {
            if (view == null || info == null)
                return info?.Bounds.Right ?? 0;

            if (view.GetViewInfo() is not DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo viewInfo)
                return info.Bounds.Right;

            int left = info.Bounds.Right;
            foreach (var col in new[]
            {
                view.Columns.ColumnByFieldName("PlanChas_UI"),
                view.Columns.ColumnByFieldName("FactChas_UI")
            })
            {
                if (col == null || !col.Visible)
                    continue;

                var colInfo = viewInfo.ColumnsInfo[col];
                if (colInfo != null)
                    left = Math.Min(left, colInfo.Bounds.Left);
            }

            return left;
        }

        private static void DrawGroupSummaryValue(
            AdvBandedGridView view,
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo viewInfo,
            int rowHandle,
            string fieldName,
            RowObjectCustomDrawEventArgs e)
        {
            var column = view.Columns.ColumnByFieldName(fieldName);
            if (column == null || !column.Visible)
                return;

            var colInfo = viewInfo.ColumnsInfo[column];
            if (colInfo == null)
                return;

            decimal value = GetGroupColumnSum(view, rowHandle, fieldName) ?? 0m;
            string text = string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:0.00}", value);

            using var format = new StringFormat(StringFormatFlags.NoWrap)
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Center
            };

            e.Graphics.DrawString(text, view.Appearance.GroupRow.Font, e.Appearance.GetForeBrush(e.Cache), colInfo.Bounds, format);
        }

        private static decimal? GetGroupColumnSum(AdvBandedGridView view, int groupRowHandle, string fieldName)
        {
            if (view == null || !view.IsGroupRow(groupRowHandle))
                return null;

            int childCount = view.GetChildRowCount(groupRowHandle);
            if (childCount <= 0)
                return 0m;

            decimal sum = 0m;
            for (int i = 0; i < childCount; i++)
            {
                int childHandle = view.GetChildRowHandle(groupRowHandle, i);
                if (view.IsGroupRow(childHandle))
                {
                    var nested = GetGroupColumnSum(view, childHandle, fieldName);
                    if (nested.HasValue)
                        sum += nested.Value;
                    continue;
                }

                object cellValue = view.GetRowCellValue(childHandle, fieldName);
                if (cellValue == null || cellValue == DBNull.Value)
                    continue;

                try
                {
                    sum += Convert.ToDecimal(cellValue, System.Globalization.CultureInfo.CurrentCulture);
                }
                catch
                {
                }
            }

            return sum;
        }
    }
}
