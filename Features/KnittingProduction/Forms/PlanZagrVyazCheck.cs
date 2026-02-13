using DevExpress.CodeParser;
using DevExpress.Data;
using DevExpress.Mvvm.Native;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Newtonsoft.Json;
using SewingProduction.Core.helpers;
using SewingProduction.Extensions;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Features.KnittingProduction.Services;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SewingProduction.Core.helpers.BindingSourceHelper;
using static SewingProduction.Helpers.GridHelper;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class PlanZagrVyazCheck : CustomForm//, IThemeable
    {
        private static DatabaseHelper _dbHelper;
        private static DbService _dbService;
        private static BulkHelper _bulkHelper;
        private static GridHelper _gridHelper;
        private readonly ILogger _logger = new FileLogger();
        private readonly SprMonthService _sprMonthService;
        private readonly VyazService _vyazService;

        //private CancellationTokenSource? _loadCts;
        private readonly DebouncedLoader _loader = new(delayMs: 300);

        private BindingSource _sprMonthBindingSource;
        private BindingSource _podrVyazBindingSource;
        private BindingSource _pzvСheckBindingSource;

        //private const string FooterLabelRow1 = "По таб.";
        //private const string FooterLabelRow2 = "По МЛ";
        private const string FooterLabelColumnField = "tabFioSokr";
        private DevExpress.XtraGrid.Columns.GridColumn? _footerLabelColumn;
        public PlanZagrVyazCheck()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _dbService = new DbService(_dbHelper);
            _bulkHelper = new BulkHelper();
            _gridHelper = new GridHelper();

            _sprMonthService = new SprMonthService(_dbHelper);
            _vyazService = new VyazService(_dbHelper);

           // ThemeManager.UpdateTheme(this);
        }
        private async Task InitializeBindingsAsync()
        {
            try
            {
                #region comboBoxMonthList
                _sprMonthBindingSource = new BindingSource
                {
                    DataSource = new BindingList<SprMonth>()
                };

                comboBoxMonthList.DataSource = _sprMonthBindingSource;
                comboBoxMonthList.SelectedIndex = -1;
                comboBoxMonthList.ValueMember = "kod";
                comboBoxMonthList.DisplayMember = "name_cl";
                #endregion

                #region comboBoxPodrVyazList
                _podrVyazBindingSource = new BindingSource
                {
                    DataSource = new BindingList<PodrVyaz>()
                };

                comboBoxPodrVyazList.DataSource = _podrVyazBindingSource;
                comboBoxPodrVyazList.SelectedIndex = -1;
                comboBoxPodrVyazList.ValueMember = "kod_vyaz";
                comboBoxPodrVyazList.DisplayMember = "text_vyaz";
                #endregion

                #region описание gridControlVyazPlan "оперативное планирование"
                _pzvСheckBindingSource = new BindingSource
                {
                    DataSource = new BindingList<PzvCheck>()
                };
                //gridControlPzvCheck.DataSource = _pzvСheckBindingSource;
                //gridPzvCheckColumnTabTab.FieldName = "tabTab";
                //gridPzvCheckColumnTabFio.FieldName = "tabFioSokr";
                //gridPzvCheckColumnTabFio.Width = 90;
                //gridViewPzvCheck.OptionsView.EnableAppearanceEvenRow = false;
                //gridViewPzvCheck.OptionsView.EnableAppearanceOddRow = false;

                //gridViewPzvCheck.Appearance.Row.Options.UseTextOptions = true;
                //gridViewPzvCheck.Appearance.Row.TextOptions.WordWrap = WordWrap.Wrap;
                //gridViewPzvCheck.Appearance.Row.TextOptions.Trimming = Trimming.None;
                //gridViewPzvCheck.OptionsView.RowAutoHeight = true;

                //gridViewPzvCheck.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
                //gridViewPzvCheck.OptionsView.ShowFooter = true;

                //_gridHelper.AutoRowFilterConfig(gridViewPzvCheck as GridView, 1);

                gridControlPzvCheck.DataSource = _pzvСheckBindingSource;
                gridControlPzvCheck.MainView = bandedGridViewPzvCheck;

                bandedGridPzvCheckColumnTabTab.FieldName = "tabTab";
                bandedGridPzvCheckColumnTabFioSokr.FieldName = "tabFioSokr";
                bandedGridPzvCheckColumnTabFioSokr.Width = 90;
                bandedGridPzvCheckColumnTabFioSokr.Fixed = FixedStyle.None;

                bandedGridPzvCheckColumnTabFioSokr.Summary.Clear();
                bandedGridPzvCheckColumnTabFioSokr.Summary.Add(
                    new GridColumnSummaryItem(SummaryItemType.Custom, "tabFioSokr", "") { Tag = "R1" });
                bandedGridPzvCheckColumnTabFioSokr.Summary.Add(
                    new GridColumnSummaryItem(SummaryItemType.Custom, "tabFioSokr", "") { Tag = "R2" });

                bandedGridViewPzvCheck.OptionsView.EnableAppearanceEvenRow = false;
                bandedGridViewPzvCheck.OptionsView.EnableAppearanceOddRow = false;

                bandedGridViewPzvCheck.Appearance.Row.Options.UseTextOptions = true;
                bandedGridViewPzvCheck.Appearance.Row.TextOptions.WordWrap = WordWrap.Wrap;
                bandedGridViewPzvCheck.Appearance.Row.TextOptions.Trimming = Trimming.None;
                bandedGridViewPzvCheck.OptionsView.RowAutoHeight = true;

                bandedGridViewPzvCheck.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
                bandedGridViewPzvCheck.OptionsView.ShowFooter = true;
                
                bandedGridViewPzvCheck.RowCellStyle -= gridViewPzvCheck_RowCellStyle;
                bandedGridViewPzvCheck.RowCellStyle += gridViewPzvCheck_RowCellStyle;

                EnableTwoRowFooter(bandedGridViewPzvCheck);
                EnableFooterLabels(bandedGridViewPzvCheck);
                bandedGridViewPzvCheck.CustomSummaryCalculate -= bandedGridViewPzvCheck_CustomSummaryCalculate;
                bandedGridViewPzvCheck.CustomSummaryCalculate += bandedGridViewPzvCheck_CustomSummaryCalculate;

                _gridHelper.AutoRowFilterConfig(bandedGridViewPzvCheck as GridView, 1);

                //bandedGridViewPzvCheck.GroupFormat = "{1}: [#image]{2}"; // Показывает: <Caption>: <value>

                //bandedGridViewPzvCheck.CustomDrawGroupRow += (s, e) =>
                //{
                //    GridView view = s as GridView;
                //    int rowHandle = e.RowHandle;

                //    int level = view.GetRowLevel(rowHandle);

                //    GridGroupRowInfo groupInfo = e.Info as GridGroupRowInfo;
                //    if (level == 1)
                //    {
                //        string _olKmlNumber = Convert.ToString(view.GetGroupRowValue(e.RowHandle, view.Columns["olKmlNumber"]));
                //        groupInfo.GroupText = $"{view.GetGroupRowValue(e.RowHandle, view.Columns["olNom"])}, в/м - {view.GetGroupRowValue(e.RowHandle, view.Columns["olKmlNumber"])}";
                //    }
                //};

                #endregion
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
        }
        private void EnableTwoRowFooter(DevExpress.XtraGrid.Views.BandedGrid.BandedGridView view)
        {
            //view.OptionsView.ShowFooter = true;
            //view.FooterPanelHeight = 55;

            //view.Appearance.FooterPanel.Options.UseTextOptions = true;
            //view.Appearance.FooterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //view.Appearance.FooterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

            //view.CustomDrawFooterCell -= View_CustomDrawFooterCell_TwoRows;
            //view.CustomDrawFooterCell += View_CustomDrawFooterCell_TwoRows;

            view.OptionsView.ShowFooter = true;
            view.FooterPanelHeight = 55;

            view.Appearance.FooterPanel.Options.UseTextOptions = true;
            view.Appearance.FooterPanel.TextOptions.WordWrap = WordWrap.Wrap;
            view.Appearance.FooterPanel.TextOptions.VAlignment = VertAlignment.Center;

            view.CustomDrawFooterCell -= View_CustomDrawFooterCell_TwoRows;
            view.CustomDrawFooterCell += View_CustomDrawFooterCell_TwoRows;

            view.CustomDrawFooter -= View_CustomDrawFooterLabels;
            view.CustomDrawFooter += View_CustomDrawFooterLabels;
        }
        private void View_CustomDrawFooterLabels(object sender, RowObjectCustomDrawEventArgs e)
        {
            // Рисуем стандартный футер
            e.Painter.DrawObject(e.Info);

            // Поверх — подписи слева
            var view = (BandedGridView)sender;

            // e.Bounds — прямоугольник всего футера (работает во всех версиях)
            Rectangle r = e.Bounds;
            r.Inflate(-4, -2);
            r.X += bandedGridPzvCheckColumnTabTab.Width;
            r.Width = bandedGridPzvCheckColumnTabFioSokr.Width;

            //r.Width = 120;

            //TextRenderer.DrawText(
            //    e.Graphics,
            //    "По таб.\r\n\r\nПо МЛ",
            //    view.Appearance.FooterPanel.Font,
            //    r,
            //    view.Appearance.FooterPanel.ForeColor,
            //    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak
            //);

            e.Handled = true;
        }

        private void EnableFooterLabels(BandedGridView view)
        {
            view.CustomDrawFooter -= View_CustomDrawFooter;
            view.CustomDrawFooter += View_CustomDrawFooter;
        }

        private void View_CustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
        {
            var view = (BandedGridView)sender;

            // Рисуем стандартный футер
            e.Painter.DrawObject(e.Info);

            //// Поверх — подписи слева (под твоей фиксированной областью)
            //var info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridFooterInfo;
            //if (info == null) return;

            //// Левый край футера (немного отступа)
            //Rectangle r = info.Bounds;
            Rectangle r = e.Bounds;
            r.Inflate(-4, -2);

            // Можно чуть ограничить ширину, чтобы не наезжало на данные
            //r.Width = 120;
            r.Width = bandedGridPzvCheckColumnTabFioSokr.Width;

            //TextRenderer.DrawText(
            //    e.Graphics,
            //    "По таб.\r\nПо МЛ",
            //    e.Appearance.Font,
            //    r,
            //    e.Appearance.GetForeColor(),
            //    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak
            //);
            //TextRenderer.DrawText(
            //    e.Graphics,
            //    "По таб.\r\n\r\nПо МЛ",
            //    view.Appearance.FooterPanel.Font,
            //    r,
            //    view.Appearance.FooterPanel.ForeColor,
            //    TextFormatFlags.Left |
            //    TextFormatFlags.VerticalCenter |
            //    TextFormatFlags.WordBreak
            //);

            e.Handled = true;
        }

        // рисуем 2 строки в футере, используя 2 summary у колонки
        private void View_CustomDrawFooterCell_TwoRows(object sender, FooterCellCustomDrawEventArgs e)
        {
            #region OLD
            ////Debug.WriteLine($"FOOTER DRAW -> {e.Column?.FieldName}");
            //var view = (BandedGridView)sender;
            //var col = e.Column;
            //if (col == null) return;

            //// 1) Подписи слева в футере колонки tabTab
            ////if (col.FieldName == FooterLabelColumnField)
            ////{
            ////    e.Info.DisplayText = $"{FooterLabelRow1}\r\n{FooterLabelRow2}";
            ////if (_footerLabelColumn != null && ReferenceEquals(col, _footerLabelColumn))
            //if (col.FieldName == "tabFioSokr")
            //    {
            //    e.Info.DisplayText = "По таб.\r\n\r\nПо МЛ";
            //    e.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
            //    e.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            //    e.Appearance.TextOptions.HAlignment = HorzAlignment.Near;

            //    e.Painter.DrawObject(e.Info);
            //    e.Handled = true;
            //    return;
            //}

            //string line1 = string.Empty;
            //string line2 = string.Empty;

            //// 2) procentOf — вычисляемый итог: Sum(tabChasiOf)/Sum(pztChasiOf)*100
            //if (col.FieldName == "procentOf")
            //{
            //    //line1 = ""; // первая строка пустая
            //    //line2 = CalcProcentFooter(view);
            //    //if (string.IsNullOrEmpty(line2)) line2 = " ";  // чтобы ячейка футера была видима
            //    //DrawTwoLines(e, line1, line2, HorzAlignment.Far);
            //    //return;

            //    line1 = "";
            //    if (col.Summary.Count > 0 && col.Summary[0] is GridColumnSummaryItem it)
            //        line1 = FormatFooterSummary(it);

            //    DrawTwoLines(e, "", string.IsNullOrEmpty(line1) ? "0.00" : line1, HorzAlignment.Far);
            //    return;
            //}

            //// 3) Остальные колонки: берём 2 SummaryItem и форматируем их SummaryValue
            //FillFromTwoSummaries(col, out line1, out line2);

            //// ✅ ДНИ: всегда 2 строки (не сворачиваем!)
            //if (col.FieldName != null && col.FieldName.StartsWith("pzvTab"))
            //{
            //    DrawTwoRowsStrict(e, line1, line2, HorzAlignment.Far);
            //    return;
            //}

            //// Правила пустых строк:
            //// tabChasiOf: пусто в 1-й строке
            //if (col.FieldName == "tabChasiOf")
            //    line1 = "";

            //// pztChasiOf: пусто во 2-й строке
            //if (col.FieldName == "pztChasiOf")
            //    line2 = "";

            //DrawTwoLines(e, line1, line2, HorzAlignment.Far);
            #endregion

            var view = (BandedGridView)sender;
            var col = e.Column;
            if (col == null) return;

            // Текущий summary item (DevExpress вызывает обработчик отдельно для каждого SummaryItem)
            var item = e.Info?.SummaryItem as GridColumnSummaryItem;
            if (item == null)
                return;

            string tag = item.Tag as string ?? "";

            // ---- 1) Подписи слева под колонкой tabFioSokr ----
            if (col.FieldName == "tabFioSokr")
            {
                e.Info.DisplayText = (tag == "R2") ? "По МЛ" : "По таб.";

                e.Appearance.TextOptions.WordWrap = WordWrap.NoWrap;
                e.Appearance.TextOptions.VAlignment = VertAlignment.Center;
                e.Appearance.TextOptions.HAlignment = HorzAlignment.Near;

                // Рисуем только свой текст (каждый вызов рисует свою строку)
                e.Painter.DrawObject(e.Info);
                e.Handled = true;
                return;
            }

            // ---- 2) procentOf: показываем только в 1-й строке (R1) ----
            if (col.FieldName == "procentOf")
            {
                //if (tag == "R1")
                //{
                //    string s = CalcProcentFooter(view);     // ожидаем строку типа "12.34 %"
                //    e.Info.DisplayText = s;
                //}
                //else
                //{
                //    e.Info.DisplayText = ""; // верхняя строка пустая
                //}

                //e.Appearance.TextOptions.WordWrap = WordWrap.NoWrap;
                //e.Appearance.TextOptions.VAlignment = VertAlignment.Center;
                //e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;

                //e.Painter.DrawObject(e.Info);
                //e.Handled = true;
                //return;
                // показываем процент только в первой строке (R1), во второй пусто
                if ((item.Tag as string) == "R1")
                    e.Info.DisplayText = FormatSummaryItemValue(item); // применит "{0:0.00} %"
                else
                    e.Info.DisplayText = "";

                e.Appearance.TextOptions.WordWrap = WordWrap.NoWrap;
                e.Appearance.TextOptions.VAlignment = VertAlignment.Center;
                e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;

                e.Painter.DrawObject(e.Info);
                e.Handled = true;
                return;
            }

            // ---- 2.1) tabChasiOf: показываем только в 1-й строке (R1) ----
            if (col.FieldName == "tabChasiOf")
            {
                if (tag == "R1")
                {
                    string s = FormatSummaryItemValue(item);
                    e.Info.DisplayText = s;
                }
                else
                {
                    e.Info.DisplayText = ""; // верхняя строка пустая
                }

                e.Appearance.TextOptions.WordWrap = WordWrap.NoWrap;
                e.Appearance.TextOptions.VAlignment = VertAlignment.Center;
                e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;

                e.Painter.DrawObject(e.Info);
                e.Handled = true;
                return;
            }

            // ---- 2.2) pztChasiOf: показываем только во 2-й строке (R2) ----
            if (col.FieldName == "pztChasiOf")
            {
                if (tag == "R2")
                {
                    string s = FormatSummaryItemValue(item);
                    e.Info.DisplayText = s;
                }
                else
                {
                    e.Info.DisplayText = ""; // первая строка пустая
                }

                e.Appearance.TextOptions.WordWrap = WordWrap.NoWrap;
                e.Appearance.TextOptions.VAlignment = VertAlignment.Center;
                e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;

                e.Painter.DrawObject(e.Info);
                e.Handled = true;
                return;
            }

            // ---- 3) Обычные колонки (в т.ч. дни): берём значение именно текущего summary item ----
            string text = FormatSummaryItemValue(item); // с учётом "0 -> пусто"

            e.Info.DisplayText = text;

            e.Appearance.TextOptions.WordWrap = WordWrap.NoWrap;
            e.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;

            e.Painter.DrawObject(e.Info);
            e.Handled = true;
        }
        private string FormatSummaryItemValue(GridColumnSummaryItem item)
        {
            if (item == null) return "";

            object v = item.SummaryValue;
            if (v == null || v == DBNull.Value) return "";

            // если 0 -> ничего не показываем
            try
            {
                if (Convert.ToDecimal(v) == 0m)
                    return "";
            }
            catch { }

            // DisplayFormat вида "{0:0.00}"
            try
            {
                if (!string.IsNullOrWhiteSpace(item.DisplayFormat) && item.DisplayFormat.Contains("{0"))
                    return string.Format(item.DisplayFormat, v);
            }
            catch { }

            return Convert.ToString(v) ?? "";
        }
        private static void DrawTwoLines(FooterCellCustomDrawEventArgs e, string line1, string line2, HorzAlignment align)
        {
            //e.Info.DisplayText = string.IsNullOrEmpty(line2) ? line1 : $"{line1}\r\n{line2}";

            //e.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
            //e.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            //e.Appearance.TextOptions.HAlignment = align;

            //e.Painter.DrawObject(e.Info);
            //e.Handled = true;
            
            bool has1 = !string.IsNullOrWhiteSpace(line1);
            bool has2 = !string.IsNullOrWhiteSpace(line2);

            // если обе пустые — рисуем пусто
            if (!has1 && !has2)
            {
                e.Info.DisplayText = "";
                e.Handled = false; // пусть рисует стандартно (или true — оба ок)
                return;
            }

            // если одна строка — показываем её одну, по центру
            if (has1 && !has2)
            {
                e.Info.DisplayText = line1;
                e.Appearance.TextOptions.WordWrap = WordWrap.NoWrap;
                e.Appearance.TextOptions.VAlignment = VertAlignment.Center;
                e.Appearance.TextOptions.HAlignment = align;

                e.Painter.DrawObject(e.Info);
                e.Handled = true;
                return;
            }

            if (!has1 && has2)
            {
                e.Info.DisplayText = line2;
                e.Appearance.TextOptions.WordWrap = WordWrap.NoWrap;
                e.Appearance.TextOptions.VAlignment = VertAlignment.Center;
                e.Appearance.TextOptions.HAlignment = align;

                e.Painter.DrawObject(e.Info);
                e.Handled = true;
                return;
            }

            // если обе строки заполнены — рисуем две строки
            e.Info.DisplayText = $"{line1}\r\n{line2}";
            e.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
            e.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            e.Appearance.TextOptions.HAlignment = align;

            e.Painter.DrawObject(e.Info);
        }
        private void FillFromTwoSummaries(GridColumn col, out string line1, out string line2)
        {
            line1 = string.Empty;
            line2 = string.Empty;

            if (col.Summary == null || col.Summary.Count < 2)
                return;

            var s1 = col.Summary[0] as GridColumnSummaryItem;
            var s2 = col.Summary[1] as GridColumnSummaryItem;
            if (s1 == null || s2 == null)
                return;

            line1 = FormatFooterSummary(s1);
            line2 = FormatFooterSummary(s2);
        }
        #region OLD FillFromSummaries
        //private void FillFromSummaries(
        //    AdvBandedGridView view,
        //    GridColumn col,
        //    ref string line1,
        //    ref string line2)
        //{
        //    if (col.Summary == null || col.Summary.Count < 2)
        //        return;

        //    var s1 = col.Summary[0] as GridColumnSummaryItem;
        //    var s2 = col.Summary[1] as GridColumnSummaryItem;
        //    if (s1 == null || s2 == null)
        //        return;

        //    line1 = GetSummaryText(view, col, s1);
        //    line2 = GetSummaryText(view, col, s2);
        //}
        #endregion
        private void FillFromSummaries(
            DevExpress.XtraGrid.Columns.GridColumn col,
            ref string line1,
            ref string line2)
        {
            if (col.Summary == null || col.Summary.Count < 2)
                return;

            var s1 = col.Summary[0] as DevExpress.XtraGrid.GridColumnSummaryItem;
            var s2 = col.Summary[1] as DevExpress.XtraGrid.GridColumnSummaryItem;

            line1 = GetSummaryText(s1);
            line2 = GetSummaryText(s2);
        }

        #region OLD GetFooterSum
        //private decimal GetFooterSum(AdvBandedGridView view, string fieldName)
        //{
        //    var col = view.Columns[fieldName];
        //    if (col == null || col.Summary == null || col.Summary.Count == 0)
        //        return 0m;

        //    var item = col.Summary[0] as GridColumnSummaryItem; // берём любой (у тебя они одинаковые)
        //    if (item == null) return 0m;

        //    object v = view.GetFooterSummaryValue(col, item);
        //    if (v == null || v == DBNull.Value) return 0m;

        //    try { return Convert.ToDecimal(v); }
        //    catch { return 0m; }
        //}
        #endregion
        private decimal GetFooterSum(string fieldName)
        {
            var col = bandedGridViewPzvCheck.Columns[fieldName];
            if (col == null || col.Summary == null || col.Summary.Count == 0)
                return 0m;

            var item = col.Summary[0] as DevExpress.XtraGrid.GridColumnSummaryItem;
            if (item == null) return 0m;

            object v = item.SummaryValue;
            if (v == null || v == DBNull.Value) return 0m;

            try { return Convert.ToDecimal(v); }
            catch { return 0m; }
        }
        #region OLD FormatFooterSummary
        //private string FormatFooterSummary(AdvBandedGridView view, GridColumn col, GridColumnSummaryItem item)
        //{
        //    object v = view.GetFooterSummaryValue(col, item);
        //    if (v == null || v == DBNull.Value) return "";

        //    // displayFormat в DevExpress обычно "{0:...}"
        //    try
        //    {
        //        if (!string.IsNullOrWhiteSpace(item.DisplayFormat) && item.DisplayFormat.Contains("{0"))
        //            return string.Format(item.DisplayFormat, v);
        //    }
        //    catch { /* ignore */ }

        //    return Convert.ToString(v) ?? "";
        //}
        #endregion
        private string FormatFooterSummary(GridColumnSummaryItem item)
        {
            if (item == null)
                return string.Empty;

            object v = item.SummaryValue;
            if (v == null || v == DBNull.Value)
                return string.Empty;

            try
            {
                decimal d = Convert.ToDecimal(v);
                if (d == 0m)
                    return string.Empty;   // ← НИЧЕГО НЕ РИСУЕМ
            }
            catch
            {
                // если не decimal — просто идём дальше
            }

            try
            {
                if (!string.IsNullOrWhiteSpace(item.DisplayFormat) &&
                    item.DisplayFormat.Contains("{0"))
                    return string.Format(item.DisplayFormat, v);
            }
            catch { }

            return Convert.ToString(v) ?? string.Empty;
        }
        #region OLD SetTwoFooterSummaries
        //private void SetTwoFooterSummaries(GridColumn col, string fieldRow1, string fieldRow2, string fmt = "{0:0.00;-0.00;;}")
        //{
        //    col.Summary.Clear();

        //    col.Summary.Add(new GridColumnSummaryItem(SummaryItemType.Sum, fieldRow1, fmt) { Tag = "R1" });
        //    col.Summary.Add(new GridColumnSummaryItem(SummaryItemType.Sum, fieldRow2, fmt) { Tag = "R2" });
        //}
        #endregion
        private void SetTwoFooterSummaries(GridColumn col, string fieldRow1, string fieldRow2, string fmt = "{0:0.00;-0.00;;}")
        {
            //col.Summary.Clear();
            //col.Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, fieldRow1, fmt));
            //col.Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, fieldRow2, fmt));
            //-----------------------
            col.Summary.Clear();

            //var s1 = new GridColumnSummaryItem(SummaryItemType.Sum, fieldRow1, fmt) { Tag = "R1" };
            //var s2 = new GridColumnSummaryItem(SummaryItemType.Sum, fieldRow2, fmt) { Tag = "R2" };

            //col.Summary.Add(s1);
            //col.Summary.Add(s2);

            col.Summary.Add(new GridColumnSummaryItem
            {
                SummaryType = SummaryItemType.Custom,
                FieldName = fieldRow1,
                DisplayFormat = fmt,
                Tag = "R1"
            });

            col.Summary.Add(new GridColumnSummaryItem
            {
                SummaryType = SummaryItemType.Custom,
                FieldName = fieldRow2,
                DisplayFormat = fmt,
                Tag = "R2"
            });
        }
        private static decimal ExtractDecimal(object raw)
        {
            if (raw == null)
                return 0m;

            // Если уже число — просто приводим
            if (raw is decimal d) return d;
            if (raw is int i) return i;
            if (raw is double db) return (decimal)db;

            string s = raw.ToString();
            if (string.IsNullOrWhiteSpace(s))
                return 0m;

            // Берём только цифры, точку и запятую
            var filtered = new string(
                s.Where(c => char.IsDigit(c) || c == '.' || c == ',')
                 .ToArray()
            );

            filtered = filtered.Replace(',', '.');

            if (decimal.TryParse(
                filtered,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out decimal result))
            {
                return result;
            }

            return 0m;
        }

        #region OLD CalcProcentFooter
        //private string CalcProcentFooter(AdvBandedGridView view)
        //{
        //    decimal tabSum = GetFooterSum(view, "tabChasiOf");
        //    decimal pztSum = GetFooterSum(view, "pztChasiOf");

        //    if (pztSum == 0m)
        //        return string.Empty;

        //    decimal percent = tabSum / pztSum * 100m;
        //    return percent.ToString("0.00"); // или "0.00%"
        //}
        #endregion
        private string CalcProcentFooter(BandedGridView view)
        {
            decimal tabSum = GetSummaryDecimal(view, "tabChasiOf");
            decimal pztSum = GetSummaryDecimal(view, "pztChasiOf");

            //Debug.WriteLine($"PERCENT: tabSum={tabSum} pztSum={pztSum}");

            if (pztSum == 0m)
                //return string.Empty;
                return "0.00";

            decimal percent = pztSum / tabSum * 100m;
            //return percent.ToString("0.00"); // если хочешь "0.00%" -> + "%"
            //return percent.ToString("{0:0.00} %"); // если хочешь "0.00%" -> + "%"
            return string.Format("{0:0.00} %", percent);
        }
        private decimal GetSummaryDecimal(BandedGridView view, string fieldName)
        {
            var col = view.Columns[fieldName];
            if (col == null || col.Summary == null || col.Summary.Count == 0)
                return 0m;

            // Берём первое SummaryItem (у tabChasiOf/pztChasiOf ты ставишь Sum дважды — любое подойдёт)
            var item = col.Summary[0] as GridColumnSummaryItem;
            if (item == null) return 0m;

            object v = item.SummaryValue;
            if (v == null || v == DBNull.Value) return 0m;

            try { return Convert.ToDecimal(v); }
            catch { return 0m; }
        }

        #region OLD GetSummaryText
        //private string GetSummaryText(
        //    AdvBandedGridView view,
        //    GridColumn column,
        //    GridColumnSummaryItem item)
        //{
        //    object value = view.GetFooterSummaryValue(column, item);
        //    if (value == null || value == DBNull.Value)
        //        return string.Empty;

        //    try
        //    {
        //        if (!string.IsNullOrWhiteSpace(item.DisplayFormat))
        //            return string.Format(item.DisplayFormat, value);
        //    }
        //    catch { }

        //    return Convert.ToString(value) ?? string.Empty;
        //}
        #endregion
        private string GetSummaryText(DevExpress.XtraGrid.GridColumnSummaryItem item)
        {
            if (item == null) return string.Empty;

            object value = item.SummaryValue;
            if (value == null || value == DBNull.Value)
                return string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(item.DisplayFormat))
                    return string.Format(item.DisplayFormat, value);
            }
            catch { }

            return Convert.ToString(value) ?? string.Empty;
        }
        private GridBand FindBandByName(GridBandCollection bands, string bandName)
        {
            foreach (GridBand band in bands)
            {
                var found = FindBandByNameRecursive(band, bandName);
                if (found != null)
                    return found;
            }
            return null;
        }
        private GridBand FindBandByNameRecursive(GridBand band, string bandName)
        {
            if (band.Name == bandName)
                return band;

            foreach (GridBand child in band.Children)
            {
                var found = FindBandByNameRecursive(child, bandName);
                if (found != null)
                    return found;
            }
            return null;
        }
        #region OLD CreateDayColumns
        //private void CreateDayColumns(GridView _gridView)
        //{
        //    try
        //    {
        //        _gridView.BeginUpdate();
        //        _gridView.GroupSummary.Clear();

        //        var memo = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
        //        gridControlPzvCheck.RepositoryItems.Add(memo);

        //        string _xGridName = _gridView.Name;
        //        int _xMonth = Convert.ToInt32(comboBoxMonthList.SelectedValue);
        //        int _xYear = Convert.ToInt32(spinEditYear.Value);
        //        if (_xMonth == 0 || _xYear == 0)
        //        {
        //            _xMonth = DateTime.Now.Month;
        //            _xYear = DateTime.Now.Year;
        //        }
        //        DateTime currentDate = DateTime.Now;
        //        int daysInMonth = DateTime.DaysInMonth(_xYear, _xMonth);
        //        for (int day = 1; day <= daysInMonth; day++)
        //        {
        //            GridColumn grdDayColumn = new GridColumn();
        //            string fName = $"grd{day.ToString("00")}";
        //            grdDayColumn.Name = $"{_xGridName}ColumnGrd{day.ToString("00")}";
        //            grdDayColumn.FieldName = fName;
        //            grdDayColumn.Caption = $"grd{day.ToString()}";
        //            grdDayColumn.Visible = false;
        //            grdDayColumn.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
        //            grdDayColumn.OptionsColumn.AllowEdit = false;
        //            _gridView.Columns.Add(grdDayColumn);

        //            GridColumn dayColumn = new GridColumn();
        //            string fieldName = $"pzvTab{day.ToString("00")}";
        //            dayColumn.Name = $"{_xGridName}ColumnPzvTab{day.ToString("00")}";
        //            dayColumn.FieldName = fieldName;
        //            dayColumn.Caption = day.ToString();
        //            dayColumn.Visible = true;
        //            dayColumn.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
        //            dayColumn.OptionsColumn.AllowEdit = false;
        //            dayColumn.ColumnEdit = memo;
        //            dayColumn.AppearanceCell.Options.UseTextOptions = true;
        //            //dayColumn.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
        //            _gridView.Columns.Add(dayColumn);

        //            _gridView.GroupSummary.Add(
        //                new GridGroupSummaryItem
        //                {
        //                    SummaryType = DevExpress.Data.SummaryItemType.Sum,
        //                    FieldName = $"pzv{day.ToString("00")}",
        //                    ShowInGroupColumnFooter = dayColumn,
        //                    DisplayFormat = "0.00;-0.00;"
        //                });
        //            _gridView.GroupSummary.Add(
        //                new GridGroupSummaryItem
        //                {
        //                    SummaryType = DevExpress.Data.SummaryItemType.Sum,
        //                    FieldName = $"tab{day.ToString("00")}",
        //                    ShowInGroupColumnFooter = dayColumn,
        //                    DisplayFormat = "0.00;-0.00;"
        //                });
        //            //string _xNumFormat = "{0:0.00;-0.00;}";
        //            ////string _xNumFormat = "{0:n2}";
        //            //foreach (GridGroupSummaryItem gsi in _gridView.GroupSummary)
        //            //{
        //            //    gsi.DisplayFormat = _xNumFormat;
        //            //}
        //        }
        //        GridColumn itogColumnTabChasiOf = new GridColumn();
        //        itogColumnTabChasiOf.FieldName = "tabChasiOf";
        //        itogColumnTabChasiOf.Caption = "Итого час по таб.";
        //        itogColumnTabChasiOf.Visible = true;
        //        itogColumnTabChasiOf.DisplayFormat.FormatString = "{0:0.00#;0:#;#}";
        //        itogColumnTabChasiOf.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
        //        itogColumnTabChasiOf.OptionsColumn.AllowEdit = false;
        //        _gridView.Columns.Add(itogColumnTabChasiOf);
        //        _gridView.GroupSummary.Add(
        //            new GridGroupSummaryItem
        //            {
        //                SummaryType = DevExpress.Data.SummaryItemType.None,
        //                FieldName = $"",
        //                ShowInGroupColumnFooter = itogColumnTabChasiOf,
        //                DisplayFormat = "0.00;-0.00;"
        //            });
        //        _gridView.GroupSummary.Add(
        //            new GridGroupSummaryItem
        //            {
        //                SummaryType = DevExpress.Data.SummaryItemType.Sum,
        //                FieldName = $"tabChasiOf",
        //                ShowInGroupColumnFooter = itogColumnTabChasiOf,
        //                DisplayFormat = "0.00;-0.00;"
        //            });

        //        GridColumn itogColumnPztChasiOf = new GridColumn();
        //        itogColumnPztChasiOf.FieldName = "pztChasiOf";
        //        itogColumnPztChasiOf.Caption = "Итого час по МЛ";
        //        itogColumnPztChasiOf.Visible = true;
        //        itogColumnPztChasiOf.DisplayFormat.FormatString = "{0:0.00#;0:#;#}";
        //        itogColumnPztChasiOf.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
        //        itogColumnPztChasiOf.OptionsColumn.AllowEdit = false;
        //        _gridView.Columns.Add(itogColumnPztChasiOf);
        //        _gridView.GroupSummary.Add(
        //            new GridGroupSummaryItem
        //            {
        //                SummaryType = DevExpress.Data.SummaryItemType.Sum,
        //                FieldName = $"pztChasiOf",
        //                ShowInGroupColumnFooter = itogColumnPztChasiOf,
        //                DisplayFormat = "0.00;-0.00;"
        //            });
        //        _gridView.GroupSummary.Add(
        //            new GridGroupSummaryItem
        //            {
        //                SummaryType = DevExpress.Data.SummaryItemType.None,
        //                FieldName = $"",
        //                ShowInGroupColumnFooter = itogColumnPztChasiOf,
        //                DisplayFormat = "0.00;-0.00;"
        //            });

        //        GridColumn itogColumnProcentOf = new GridColumn();
        //        itogColumnProcentOf.FieldName = "procentOf";
        //        itogColumnProcentOf.Caption = "% выраб.";
        //        itogColumnProcentOf.Visible = true;
        //        itogColumnProcentOf.DisplayFormat.FormatString = "{0:0.00#;0:#;#}";
        //        itogColumnProcentOf.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
        //        itogColumnProcentOf.OptionsColumn.AllowEdit = false;
        //        _gridView.Columns.Add(itogColumnProcentOf);
        //        _gridView.GroupSummary.Add(
        //            new GridGroupSummaryItem
        //            {
        //                SummaryType = DevExpress.Data.SummaryItemType.None,
        //                FieldName = $"",
        //                ShowInGroupColumnFooter = itogColumnProcentOf,
        //                DisplayFormat = "0.00;-0.00;"
        //            });
        //        _gridView.GroupSummary.Add(
        //            new GridGroupSummaryItem
        //            {
        //                SummaryType = DevExpress.Data.SummaryItemType.None,
        //                FieldName = $"",
        //                ShowInGroupColumnFooter = itogColumnProcentOf,
        //                DisplayFormat = "0.00;-0.00;"
        //            });

        //        _gridView.BestFitColumns();
        //        _gridView.EndUpdate();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка CreateDayColumns: {ex.Message}");
        //    }
        //}
        #endregion
        private void CreateDayColumns(BandedGridView view)
        {
            try
            {
                if (view == null) return;

                view.BeginUpdate();
                try
                {
                    // 0) Сначала удаляем только динамику
                    RemoveDayColumns(view);

                    // 1) Включаем футер и 2 строки
                    EnableTwoRowFooter(view);

                    var memo = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
                    gridControlPzvCheck.RepositoryItems.Add(memo);

                    int month = Convert.ToInt32(comboBoxMonthList.SelectedValue);
                    int year = Convert.ToInt32(spinEditYear.Value);
                    if (month == 0 || year == 0) { month = DateTime.Now.Month; year = DateTime.Now.Year; }

                    int daysInMonth = DateTime.DaysInMonth(year, month);

                    // 2) Берём/создаём bands для динамики
                    var bandDays = GetOrCreateTopBand(view, "bandDays", "Дни");
                    var bandTotals = GetOrCreateTopBand(view, "bandTotals", "Итоги");

                    // 3) Дневные колонки
                    for (int day = 1; day <= daysInMonth; day++)
                    {
                        // скрытая grdXX
                        var grdCol = new BandedGridColumn
                        {
                            FieldName = $"grd{day:00}",
                            Caption = $"grd{day}",
                            Visible = false,
                            OptionsColumn = { AllowEdit = false }
                        };
                        view.Columns.Add(grdCol);
                        bandDays.Columns.Add(grdCol);

                        // видимая pzvTabXX
                        var dayCol = new BandedGridColumn
                        {
                            FieldName = $"pzvTab{day:00}",
                            Caption = day.ToString(),
                            Visible = true,
                            OptionsColumn = { AllowEdit = false },
                            ColumnEdit = memo
                        };
                        dayCol.AppearanceCell.Options.UseTextOptions = true;
                        dayCol.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

                        view.Columns.Add(dayCol);
                        bandDays.Columns.Add(dayCol);

                        // 2 итога в футере (1 строка / 2 строка)
                        //SetTwoFooterSummaries(dayCol, $"tab{day:00}", $"pzvTab{day:00}");
                        SetTwoFooterSummaries(dayCol, $"tab{day:00}", $"pzv{day:00}");
                    }

                    // 4) Итоговые колонки
                    var tabChasi = new BandedGridColumn
                    {
                        FieldName = "tabChasiOf",
                        Caption = "Итого час по таб.",
                        Visible = true,
                        OptionsColumn = { AllowEdit = false }
                    };
                    view.Columns.Add(tabChasi);
                    bandTotals.Columns.Add(tabChasi);
                    SetTwoFooterSummaries(tabChasi, "tabChasiOf", "tabChasiOf"); // пустую строку сделаем в CustomDraw

                    var pztChasi = new BandedGridColumn
                    {
                        FieldName = "pztChasiOf",
                        Caption = "Итого час по МЛ",
                        Visible = true,
                        OptionsColumn = { AllowEdit = false }
                    };
                    view.Columns.Add(pztChasi);
                    bandTotals.Columns.Add(pztChasi);
                    SetTwoFooterSummaries(pztChasi, "pztChasiOf", "pztChasiOf"); // пустую строку сделаем в CustomDraw

                    var proc = new BandedGridColumn
                    {
                        FieldName = "procentOf",
                        Caption = "% выраб.",
                        Visible = true,
                        OptionsColumn = { AllowEdit = false },
                        DisplayFormat =
                        {
                            FormatType = DevExpress.Utils.FormatType.Numeric,
                            FormatString = "0.00' %'"
                        }
                    };
                    view.Columns.Add(proc);
                    bandTotals.Columns.Add(proc);
                    proc.Summary.Clear();
                    //proc.Summary.Add(new GridColumnSummaryItem(SummaryItemType.Custom, "procentOf", "{0:0.00}"));
                    proc.Summary.Add(new GridColumnSummaryItem(SummaryItemType.Custom, "procentOf", "{0:0.00} %") { Tag = "R1" });
                    //proc.Summary.Clear(); // мы рисуем вручную (деление)
                    CalcProcentFooter(bandedGridViewPzvCheck);
                }
                finally
                {
                    gridBand1.Caption = "";                 // пустое название
                    gridBand1.AppearanceHeader.Options.UseTextOptions = true;
                    gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                    gridBand1.Fixed = FixedStyle.None;

                    //view.OptionsView.ShowFooter = true;
                    //view.FooterPanelHeight = 55;

                    //view.CustomDrawFooterCell -= View_CustomDrawFooterCell_TwoRows;
                    //view.CustomDrawFooterCell += View_CustomDrawFooterCell_TwoRows;
                    view.UpdateSummary();
                    view.InvalidateFooter();
                    //view.RefreshData();

                    view.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка CreateDayColumns: {ex.Message}");
            }
        }
        private void bandedGridViewPzvCheck_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            if (e.Item is not GridColumnSummaryItem item)
                return;

            // --- 1) procentOf считаем ОТДЕЛЬНО (не суммируем по строкам) ---
            if (item.FieldName == "procentOf" && item.SummaryType == SummaryItemType.Custom)
            {
                if (e.SummaryProcess != CustomSummaryProcess.Finalize)
                    return;

                decimal tabSum = GetSummaryDecimal((BandedGridView)sender, "tabChasiOf");
                decimal pztSum = GetSummaryDecimal((BandedGridView)sender, "pztChasiOf");

                e.TotalValue = (tabSum == 0m) ? 0m : (pztSum / tabSum * 100m);
                return;
            }

            // --- 2) Все остальные Custom summary: суммируем "только числа, откинув буквы" ---
            if (item.SummaryType != SummaryItemType.Custom)
                return;

            switch (e.SummaryProcess)
            {
                case CustomSummaryProcess.Start:
                    e.TotalValue = 0m;
                    break;

                case CustomSummaryProcess.Calculate:
                    if (e.FieldValue == null) return;

                    decimal value = ExtractDecimal(e.FieldValue);
                    e.TotalValue = (decimal)e.TotalValue + value;
                    break;

                case CustomSummaryProcess.Finalize:
                    // ничего
                    break;
            }
        }
        private void RemoveDayColumns(BandedGridView view)
        {
            try
            {
                if (view == null) return;

                view.BeginUpdate();
                try
                {
                    bool IsDynamic(string? fn) =>
                        !string.IsNullOrEmpty(fn) &&
                        (fn.StartsWith("pzvTab") ||
                         fn.StartsWith("grd") ||
                         fn == "tabChasiOf" ||
                         fn == "pztChasiOf" ||
                         fn == "procentOf");

                    // 1) Удаляем динамические колонки из view.Columns
                    for (int i = view.Columns.Count - 1; i >= 0; i--)
                    {
                        GridColumn col = view.Columns[i];
                        if (!IsDynamic(col.FieldName)) continue;

                        col.Summary?.Clear();
                        view.Columns.RemoveAt(i);
                    }

                    // 2) Чистим динамические колонки из band’ов (не трогаем статические)
                    foreach (GridBand band in view.Bands)
                        CleanupBandDynamicColumnsRecursive(band, IsDynamic);

                    // 3) (опционально) если ты используешь специальные bands под динамику — чистим их полностью по имени
                    var bandDays = GetOrCreateTopBand(view, "bandDays", "Дни");
                    var bandTotals = GetOrCreateTopBand(view, "bandTotals", "Итоги");
                    RemoveBandColumns(bandDays);
                    RemoveBandColumns(bandTotals);
                }
                finally
                {
                    view.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка RemoveDayColumns: {ex.Message}");
            }
        }

        private void CleanupBandDynamicColumnsRecursive(GridBand band, Func<string?, bool> isDynamicField)
        {
            for (int i = band.Columns.Count - 1; i >= 0; i--)
            {
                var c = band.Columns[i];
                if (c != null && isDynamicField(c.FieldName))
                    band.Columns.RemoveAt(i);
            }

            foreach (GridBand child in band.Children)
                CleanupBandDynamicColumnsRecursive(child, isDynamicField);
        }
        private GridBand GetOrCreateTopBand(BandedGridView view, string bandName, string caption)
        {
            // поиск по верхнему уровню (обычно достаточно)
            foreach (GridBand b in view.Bands)
                if (b.Name == bandName) return b;

            var band = new GridBand { Name = bandName, Caption = caption };
            view.Bands.Add(band);
            return band;
        }

        private void RemoveBandColumns(GridBand band)
        {
            // удаляем колонки из band.Columns, но сам band оставляем
            for (int i = band.Columns.Count - 1; i >= 0; i--)
                band.Columns.RemoveAt(i);

            // если band вложенный — чистим и детей
            for (int i = band.Children.Count - 1; i >= 0; i--)
                RemoveBandColumns(band.Children[i]);
        }
        private void RemoveBandIfExists(AdvBandedGridView view, string bandName)
        {
            if (view == null) return;

            var band = FindBandByName(view.Bands, bandName);
            if (band == null) return;

            // Бэнд может быть вложенным — удаляем из правильной коллекции
            if (band.ParentBand != null)
                band.ParentBand.Children.Remove(band);
            else
                view.Bands.Remove(band);
        }
        private Task SetStatusAsync(string text)
            => this.UI(() => labelStatus.Text = text);

        private Task SetLoadingAsync(bool isLoading)
            => this.UI(() =>
            {
                labelStatus.Text = isLoading ? "Загрузка…" : "Готово";
                Cursor = isLoading ? Cursors.WaitCursor : Cursors.Default;
            });

        private async Task LoadSprMonthDataAsync()
        {
            await _loader.RunAsync(
                async token =>
                {
                    try
                    {
                        await SetLoadingAsync(true);

                        // ❌ НЕ очищаем ComboBox здесь

                        var bs = await _sprMonthService.GetSprMonth(token);

                        await this.UI(() =>
                        {
                            comboBoxMonthList.BeginUpdate();
                            _sprMonthBindingSource.DataSource = bs.DataSource;
                            comboBoxMonthList.EndUpdate();
                        });

                        await SetStatusAsync(bs.Count == 0 ? "Нет данных" : "Готово");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка LoadSprMonthDataAsync: {ex.Message}");
                    }
                },
                onError: async ex =>
                {
                    await _logger.LogErrorAsync(ex, "Ошибка загрузки spr_month");
                    await SetStatusAsync("Ошибка загрузки");
                    await SetLoadingAsync(false);
                },
                onCanceled: async byLifetime =>
                {
                    if (!byLifetime)
                        await SetStatusAsync("Отменено");

                    await SetLoadingAsync(false);
                }
            );

            await SetLoadingAsync(false);
        }

        private async Task LoadPodrVyazDataAsync()
        {
            await _loader.RunAsync(
                async token =>
                {
                    try
                    {
                        await SetLoadingAsync(true);

                        await this.UI(() =>
                        {
                            comboBoxPodrVyazList.BeginUpdate();
                            _podrVyazBindingSource.DataSource = new BindingList<PodrVyaz>();
                            comboBoxPodrVyazList.EndUpdate();
                            comboBoxPodrVyazList.SelectedIndex = -1;
                        });

                        var bs = await _vyazService.GetPodrVyaz(token);

                        await this.UI(() =>
                        {
                            comboBoxPodrVyazList.BeginUpdate();
                            _podrVyazBindingSource.DataSource = bs.DataSource;
                            comboBoxPodrVyazList.EndUpdate();
                            comboBoxPodrVyazList.SelectedIndex = -1;
                        });

                        await SetStatusAsync(bs.Count == 0 ? "Нет данных" : "Готово");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка LoadPodrVyazDataAsync: {ex.Message}");
                    }
                },
                onError: async ex =>
                {
                    await _logger.LogErrorAsync(ex, "Ошибка загрузки podr_vyaz");
                    await SetStatusAsync("Ошибка загрузки");
                    await SetLoadingAsync(false);
                },
                onCanceled: async byLifetime =>
                {
                    if (!byLifetime)
                        await SetStatusAsync("Отменено");

                    await SetLoadingAsync(false);
                }
            );

            await SetLoadingAsync(false);
        }

        private async Task LoadPzvCheckDataAsync()
        {
            await _loader.RunAsync(
                async token =>
                {
                    try
                    {
                        //gridViewPzvCheck.ShowLoadingPanel();
                        bandedGridViewPzvCheck.ShowLoadingPanel();
                        await SetLoadingAsync(true);

                        //await this.UI(() =>
                        //{
                        //    gridControlPzvCheck.BeginUpdate();
                        //    _podrVyazBindingSource.DataSource = new BindingList<PodrVyaz>();
                        //    gridControlPzvCheck.EndUpdate();
                        //});
                        //MessageBox.Show($" {comboBoxPodrVyazList.SelectedIndex} ");
                        int _xPodrID = Convert.ToInt32(comboBoxPodrVyazList.SelectedValue);
                        int _xMonth = Convert.ToInt32(comboBoxMonthList.SelectedValue);
                        int _xYear = Convert.ToInt32(spinEditYear.Value);
                        var bs = await _vyazService.GetPzvCheck(_xPodrID, _xMonth, _xYear, token);

                        await this.UI(() =>
                        {
                            gridControlPzvCheck.BeginUpdate();
                            _pzvСheckBindingSource.DataSource = bs.DataSource;

                            bandedGridViewPzvCheck.UpdateSummary();
                            bandedGridViewPzvCheck.InvalidateFooter();   // ВАЖНО
                            bandedGridViewPzvCheck.RefreshData();

                            gridControlPzvCheck.EndUpdate();
                        });

                        await SetStatusAsync(bs.Count == 0 ? "Нет данных" : "Готово");
                        //gridViewPzvCheck.HideLoadingPanel();
                        bandedGridViewPzvCheck.HideLoadingPanel();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка LoadPzvCheckDataAsync: {ex.Message}");
                    }
                },
                onError: async ex =>
                {
                    await _logger.LogErrorAsync(ex, "Ошибка загрузки pzvCheck");
                    await SetStatusAsync("Ошибка загрузки");
                    await SetLoadingAsync(false);
                },
                onCanceled: async byLifetime =>
                {
                    if (!byLifetime)
                        await SetStatusAsync("Отменено");

                    await SetLoadingAsync(false);
                }
            );

            await SetLoadingAsync(false);
        }
        private async void PlanZagrVyazCheck_Load(object sender, EventArgs e)
        {
            //InitializeBindings();
            try
            {
                //RemoveDayColumns(gridViewPzvCheck);
                //CreateDayColumns(gridViewPzvCheck);
                //RemoveDayColumns(bandedGridViewPzvCheck);
                //CreateDayColumns(bandedGridViewPzvCheck);
                //            MessageBox.Show(
                //$"Footer={bandedGridViewPzvCheck.OptionsView.ShowFooter}\n" +
                //$"tabTab col exists={bandedGridViewPzvCheck.Columns["tabTab"] != null}\n" +
                //$"tabFioSokr col exists={bandedGridViewPzvCheck.Columns["tabFioSokr"] != null}\n" +
                //$"Bands top-level={bandedGridViewPzvCheck.Bands.Count}"
                //);
                Task bindingsTask = InitializeBindingsAsync();
                await Task.WhenAll(bindingsTask);

                RemoveDayColumns(bandedGridViewPzvCheck);
                CreateDayColumns(bandedGridViewPzvCheck);

                //_footerLabelColumn = bandedGridViewPzvCheck.Columns.ColumnByName("bandedGridPzvCheckColumnTabFioSokr");
                _footerLabelColumn = bandedGridPzvCheckColumnTabFioSokr;

                await LoadSprMonthDataAsync();

                comboBoxMonthList.SelectedValue = DateTime.Now.Month;
                spinEditYear.Value = DateTime.Now.Year;

                await LoadPodrVyazDataAsync();
                comboBoxPodrVyazList.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка PlanZagrVyazCheck_Load: {ex.Message}");
            }
        }

        private async void comboBoxMonthList_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                _loader.CancelUser();          // ⛔ отменяем предыдущие загрузки
                //RemoveDayColumns(gridViewPzvCheck);
                RemoveDayColumns(bandedGridViewPzvCheck);
                if (comboBoxPodrVyazList.SelectedIndex != -1 && spinEditYear.Value != 0)
                {
                    LoadPzvCheckDataAsync();
                }
                //CreateDayColumns(gridViewPzvCheck);
                CreateDayColumns(bandedGridViewPzvCheck);
                //await LoadPodrVyazDataAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка comboBoxMonthList_SelectedValueChanged: {ex.Message}");
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try
            {
                _loader.CancelLifetime();
                _loader.Dispose();
                base.OnFormClosed(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка OnFormClosed: {ex.Message}");
            }
        }

        private void simpleButtonPrevMonth_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime _prevMonth = Convert.ToDateTime($"01.{comboBoxMonthList.SelectedValue}.{spinEditYear.Value}");
                _prevMonth = _prevMonth.AddMonths(-1);

                comboBoxMonthList.SelectedValue = _prevMonth.Month;
                spinEditYear.Value = _prevMonth.Year;

                //RemoveDayColumns(gridViewPzvCheck);
                RemoveDayColumns(bandedGridViewPzvCheck);
                if (comboBoxPodrVyazList.SelectedIndex != -1 && spinEditYear.Value != 0)
                {
                    LoadPzvCheckDataAsync();
                }
                //CreateDayColumns(gridViewPzvCheck);
                CreateDayColumns(bandedGridViewPzvCheck);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка simpleButtonPrevMonth_Click: {ex.Message}");
            }
        }

        private void simpleButtonNextMonth_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime _nextMonth = Convert.ToDateTime($"01.{comboBoxMonthList.SelectedValue}.{spinEditYear.Value}");
                _nextMonth = _nextMonth.AddMonths(1);

                comboBoxMonthList.SelectedValue = _nextMonth.Month;
                spinEditYear.Value = _nextMonth.Year;

                //RemoveDayColumns(gridViewPzvCheck);
                RemoveDayColumns(bandedGridViewPzvCheck);
                if (comboBoxPodrVyazList.SelectedIndex != -1 && spinEditYear.Value != 0)
                {
                    LoadPzvCheckDataAsync();
                }
                //CreateDayColumns(gridViewPzvCheck);
                CreateDayColumns(bandedGridViewPzvCheck);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка simpleButtonNextMonth_Click: {ex.Message}");
            }
        }

        private void buttonGetPzvCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxPodrVyazList.SelectedIndex != -1 && spinEditYear.Value != 0)
                {
                    LoadPzvCheckDataAsync();
                }
                else
                {
                    MessageBox.Show("Выберите подразеделение и повторите попытку");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка buttonGetPzvCheck_Click: {ex.Message}");
            }
        }

        private void comboBoxMonthList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboBoxPodrVyazList.SelectedIndex != -1)
            //{
            //    LoadPzvCheckDataAsync();
            //}
        }

        private void spinEditYear_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                //RemoveDayColumns(gridViewPzvCheck);
                RemoveDayColumns(bandedGridViewPzvCheck);
                if (comboBoxPodrVyazList.SelectedIndex != -1 && spinEditYear.Value != 0)
                {
                    LoadPzvCheckDataAsync();
                }
                //CreateDayColumns(gridViewPzvCheck);
                CreateDayColumns(bandedGridViewPzvCheck);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка spinEditYear_ValueChanged: {ex.Message}");
            }
        }

        private void gridViewPzvCheck_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column != null && e.Column.FieldName.StartsWith("pzvTab"))
            {
                var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                // BandedGridView наследуется от GridView, так что это ок
                if (view == null) return;

                //string dayNumber = e.Column.FieldName.Substring(6);
                string dayNumber = e.Column.FieldName.Replace("pzvTab", "");
                if (int.TryParse(dayNumber, out int day) && day >= 1 && day <= 31)
                {
                    //string markColumnName = $"grd{day.ToString("00")}";
                    string markColumnName = $"grd{day.ToString("00")}";
                    //object markValue = gridViewPzvCheck.GetRowCellValue(e.RowHandle, markColumnName);
                    object markValue = bandedGridViewPzvCheck.GetRowCellValue(e.RowHandle, markColumnName);

                    if (markValue != null)
                    {
                        string[] workingValue = { "1", "11", "8" };
                        bool IsWorkingDay = workingValue.Any(v => markValue.ToString().Contains(v));
                        bool IsHoliday = markValue.ToString().Contains("/3");
                        bool IsOrderBasedLeave = markValue.ToString().Contains("/4");
                        if (IsHoliday) { e.Appearance.BackColor = Color.FromArgb(128, 128, 255); }
                        else
                        {
                            if (IsOrderBasedLeave) { e.Appearance.BackColor = Color.FromArgb(255, 128, 64); }
                            else
                            {
                                if (IsWorkingDay) { e.Appearance.BackColor = Color.FromArgb(255, 255, 255); }
                                else
                                { e.Appearance.BackColor = Color.FromArgb(255, 128, 128); }
                            }
                        }
                    }
                }
            }
        }
        private static void DrawTwoRowsStrict(FooterCellCustomDrawEventArgs e, string line1, string line2, HorzAlignment align)
        {
            // чтобы "окошко" не исчезало, подставляем пробел
            if (string.IsNullOrWhiteSpace(line1)) line1 = " ";
            if (string.IsNullOrWhiteSpace(line2)) line2 = " ";

            e.Info.DisplayText = $"{line1}\r\n{line2}";
            e.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
            e.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            e.Appearance.TextOptions.HAlignment = align;

            e.Painter.DrawObject(e.Info);
            e.Handled = true;
        }
        private void customSimpleButton1_Click(object sender, EventArgs e)
        {
//            var cols = string.Join("\n",
//    bandedGridViewPzvCheck.Columns
//        .Cast<GridColumn>()
//        .Select(c => $"{c.Name} | FieldName={c.FieldName} | Visible={c.Visible}")
//);

//            MessageBox.Show(cols);
        }
    }
}