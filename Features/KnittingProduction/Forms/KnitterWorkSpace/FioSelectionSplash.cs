using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    internal sealed class FioSelectionSplash : XtraForm
    {
        private readonly GridLookUpEdit _fioLookup;
        private readonly SimpleButton _okButton;
        private readonly SimpleButton _cancelButton;

        public int? SelectedTab =>
            _fioLookup.EditValue != null && int.TryParse(_fioLookup.EditValue.ToString(), out int tab)
                ? tab
                : (int?)null;

        public FioSelectionSplash(IEnumerable<FioModel> fioList, int? initialTab = null)
        {
            var fioItems = fioList?.ToList() ?? new List<FioModel>();

            // Basic form settings
            Text = "Выбор сотрудника";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowIcon = false;
            ShowInTaskbar = false;
            Size = new Size(420, 180);
            Padding = new Padding(12);

            var captionLabel = new LabelControl
            {
                Dock = DockStyle.Top,
                Text = "Выберите фамилию и табельный номер:",
                Appearance = { Font = new Font("Segoe UI", 9F, FontStyle.Regular) },
                AutoSizeMode = LabelAutoSizeMode.Vertical
            };

            _fioLookup = new GridLookUpEdit
            {
                Dock = DockStyle.Top,
                Height = 32,
                Properties =
                {
                    DataSource = fioItems,
                    DisplayMember = nameof(FioModel.Fio),
                    ValueMember = nameof(FioModel.Tab),
                    NullText = "Не выбрано",
                    ImmediatePopup = true,
                    PopupFilterMode = PopupFilterMode.Contains,
                    PopupFormMinSize = new Size(350, 200)
                }
            };

            var lookupView = new GridView
            {
                FocusRectStyle = DrawFocusRectStyle.RowFocus,
                OptionsSelection = { EnableAppearanceFocusedCell = false },
                OptionsView = { ShowGroupPanel = false, ColumnAutoWidth = false }
            };

            lookupView.Columns.AddVisible(nameof(FioModel.Fio), "ФИО").Width = 200;
            lookupView.Columns.AddVisible(nameof(FioModel.Tab), "Таб. №").Width = 80;
            //lookupView.BestFitColumns();

            _fioLookup.Properties.PopupView = lookupView;
            if (initialTab.HasValue && fioItems.Any(f => f.Tab == initialTab.Value))
            {
                _fioLookup.EditValue = initialTab.Value;
            }

            _okButton = new SimpleButton
            {
                Text = "Продолжить",
                DialogResult = DialogResult.OK,
                Enabled = _fioLookup.EditValue != null
            };
            _cancelButton = new SimpleButton
            {
                Text = "Отмена",
                DialogResult = DialogResult.Cancel
            };

            AcceptButton = _okButton;
            CancelButton = _cancelButton;

            _fioLookup.EditValueChanged += (s, e) =>
            {
                _okButton.Enabled = _fioLookup.EditValue != null;
            };

            lookupView.DoubleClick += (s, e) =>
            {
                if (_fioLookup.EditValue != null)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };

            var buttonsPanel = new PanelControl
            {
                Dock = DockStyle.Bottom,
                Height = 48,
                BorderStyle = BorderStyles.NoBorder,
                Padding = new Padding(0, 12, 0, 0)
            };
            buttonsPanel.Controls.Add(_cancelButton);
            buttonsPanel.Controls.Add(_okButton);

            _okButton.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            _cancelButton.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            _okButton.Location = new Point(buttonsPanel.Width - 2 * _okButton.Width - 16, 12);
            _cancelButton.Location = new Point(buttonsPanel.Width - _cancelButton.Width - 8, 12);
            buttonsPanel.SizeChanged += (s, e) =>
            {
                _cancelButton.Location = new Point(buttonsPanel.Width - _cancelButton.Width - 8, 12);
                _okButton.Location = new Point(_cancelButton.Left - _okButton.Width - 8, 12);
            };

            var container = new PanelControl
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyles.NoBorder,
                Padding = new Padding(0, 12, 0, 0)
            };
            container.Controls.Add(_fioLookup);
            container.Controls.Add(captionLabel);

            Controls.Add(container);
            Controls.Add(buttonsPanel);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            _fioLookup.Focus();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK && SelectedTab is null)
            {
                e.Cancel = true;
                return;
            }

            base.OnFormClosing(e);
        }
    }
}

