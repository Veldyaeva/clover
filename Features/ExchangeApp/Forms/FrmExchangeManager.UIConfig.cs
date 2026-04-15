using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using ExchangeApp.Models;
using System.Drawing;

namespace ExchangeApp.Forms
{
    public partial class FrmExchangeManager
    {
        private void ConfigureDocumentGrid()
        {
            gvDocuments.CustomColumnDisplayText -= gvDocuments_CustomColumnDisplayText;
            gvDocuments.CustomColumnDisplayText += gvDocuments_CustomColumnDisplayText;
            gvDocuments.OptionsBehavior.Editable = true;
            gvDocuments.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvDocuments.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
            gvDocuments.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvDocuments.OptionsView.ShowGroupPanel = false;
            gvDocuments.OptionsView.ShowIndicator = true;
            gvDocuments.OptionsView.ColumnAutoWidth = false;
            gvDocuments.OptionsView.ShowAutoFilterRow = true;
            gvDocuments.OptionsFind.AlwaysVisible = true;
            gvDocuments.OptionsFind.FindNullPrompt = "Поиск документов...";
            gvDocuments.Appearance.HeaderPanel.Font = new Font(gvDocuments.Appearance.HeaderPanel.Font, FontStyle.Bold);

            var checkEdit = new RepositoryItemCheckEdit
            {
                ValueChecked = true,
                ValueUnchecked = false,
                NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
            };
            gcDocuments.RepositoryItems.Add(checkEdit);

            gvDocuments.Columns.Clear();

            var colSelected = gvDocuments.Columns.AddVisible(nameof(ExchangeDocumentItem.IsSelected), "Выб.");
            colSelected.ColumnEdit = checkEdit;
            colSelected.Width = 55;
            colSelected.Fixed = FixedStyle.Left;
            colSelected.OptionsColumn.AllowSort = DefaultBoolean.False;
            colSelected.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            var colDocumentId = gvDocuments.Columns.AddVisible(nameof(ExchangeDocumentItem.DocumentId), "ID");
            colDocumentId.Width = 70;
            colDocumentId.OptionsColumn.AllowEdit = false;
            colDocumentId.Fixed = FixedStyle.Left;

            var colSourceDocType = gvDocuments.Columns.AddVisible(nameof(ExchangeDocumentItem.SourceDocType), "Тип документа");
            colSourceDocType.Width = 120;
            colSourceDocType.OptionsColumn.AllowEdit = false;

            var colSourceDocId = gvDocuments.Columns.AddVisible(nameof(ExchangeDocumentItem.SourceDocId), "Документ");
            colSourceDocId.Width = 180;
            colSourceDocId.OptionsColumn.AllowEdit = false;

            var colSourceDocDate = gvDocuments.Columns.AddVisible(nameof(ExchangeDocumentItem.SourceDocDate), "Дата документа");
            colSourceDocDate.Width = 120;
            colSourceDocDate.DisplayFormat.FormatType = FormatType.DateTime;
            colSourceDocDate.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
            colSourceDocDate.OptionsColumn.AllowEdit = false;

            var colSourceCompanyId = gvDocuments.Columns.AddVisible(nameof(ExchangeDocumentItem.SourceCompanyId), "Орг.");
            colSourceCompanyId.Width = 70;
            colSourceCompanyId.OptionsColumn.AllowEdit = false;

            var colLastLoadStatus = gvDocuments.Columns.AddVisible(nameof(ExchangeDocumentItem.LastLoadStatus), "Статус загрузки");
            colLastLoadStatus.Width = 120;
            colLastLoadStatus.OptionsColumn.AllowEdit = false;

            var colNeedsExport = gvDocuments.Columns.AddVisible(nameof(ExchangeDocumentItem.NeedsExport), "Нужна выгрузка");
            colNeedsExport.Width = 110;
            colNeedsExport.ColumnEdit = checkEdit;
            colNeedsExport.OptionsColumn.AllowEdit = false;

            var colNeedsReexport = gvDocuments.Columns.AddVisible(nameof(ExchangeDocumentItem.NeedsReexport), "Нужна перевыгрузка");
            colNeedsReexport.Width = 135;
            colNeedsReexport.ColumnEdit = checkEdit;
            colNeedsReexport.OptionsColumn.AllowEdit = false;

            gvDocuments.BestFitColumns();

            gvDocuments.RowCellStyle -= gvDocuments_RowCellStyle;
            gvDocuments.RowCellStyle += gvDocuments_RowCellStyle;
        }

        private void ConfigureBatchGrid()
        {
            gvBatches.CustomColumnDisplayText -= gvBatches_CustomColumnDisplayText;
            gvBatches.CustomColumnDisplayText += gvBatches_CustomColumnDisplayText;

            gvBatches.OptionsBehavior.Editable = false;
            gvBatches.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvBatches.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
            gvBatches.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvBatches.OptionsView.ShowGroupPanel = false;
            gvBatches.OptionsView.ShowIndicator = true;
            gvBatches.OptionsView.ColumnAutoWidth = false;
            gvBatches.OptionsView.ShowAutoFilterRow = true;
            gvBatches.OptionsFind.AlwaysVisible = true;
            gvBatches.OptionsFind.FindNullPrompt = "Поиск пакетов...";
            gvBatches.Appearance.HeaderPanel.Font = new Font(gvBatches.Appearance.HeaderPanel.Font, FontStyle.Bold);

            gvBatches.Columns.Clear();

            var colExportBatchId = gvBatches.Columns.AddVisible(nameof(ExportBatchItem.ExportBatchId), "ID пакета");
            colExportBatchId.Width = 90;
            colExportBatchId.Fixed = FixedStyle.Left;

            var colBatchNo = gvBatches.Columns.AddVisible(nameof(ExportBatchItem.BatchNo), "Номер пакета");
            colBatchNo.Width = 180;
            colBatchNo.Fixed = FixedStyle.Left;

            var colExportTypeCode = gvBatches.Columns.AddVisible(nameof(ExportBatchItem.ExportTypeCode), "Вид выгрузки");
            colExportTypeCode.Width = 110;

            var colReason = gvBatches.Columns.AddVisible(nameof(ExportBatchItem.Reason), "Режим");
            colReason.Width = 100;

            var colStatus = gvBatches.Columns.AddVisible(nameof(ExportBatchItem.Status), "Статус");
            colStatus.Width = 120;

            var colDocumentCount = gvBatches.Columns.AddVisible(nameof(ExportBatchItem.DocumentCount), "Документов");
            colDocumentCount.Width = 95;
            colDocumentCount.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

            var colRowCount = gvBatches.Columns.AddVisible(nameof(ExportBatchItem.RowCount), "Строк");
            colRowCount.Width = 80;
            colRowCount.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

            var colCreatedAt = gvBatches.Columns.AddVisible(nameof(ExportBatchItem.CreatedAt), "Создан");
            colCreatedAt.Width = 140;
            colCreatedAt.DisplayFormat.FormatType = FormatType.DateTime;
            colCreatedAt.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm:ss";

            gvBatches.BestFitColumns();

            gvBatches.RowCellStyle -= gvBatches_RowCellStyle;
            gvBatches.RowCellStyle += gvBatches_RowCellStyle;
        }
    }
}
