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
        private readonly TextEdit _tabEdit;

        public int? SelectedTab =>
            int.TryParse(_tabEdit.Text, out var scanTab) ? scanTab :
            _fioLookup.EditValue != null && int.TryParse(_fioLookup.EditValue.ToString(), out int tab) ? tab : (int?)null;

        public FioSelectionSplash(IEnumerable<FioModel> fioList, int? initialTab = null)
        {
                    // Увеличиваем шрифт сплеша и всех контролов до 12pt
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            this.AutoScaleMode = AutoScaleMode.Font;

            //foreach (Control c in this.Controls)
            //{
            //    c.Font = this.Font;
            //    foreach (Control child in c.Controls)
            //    {
            //        child.Font = this.Font;
            //    }
            //}
            var fioItems = fioList?.ToList() ?? new List<FioModel>();

            // Basic form settings
            Text = "Выбор сотрудника";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowIcon = false;
            ShowInTaskbar = false;
            Size = new Size(630, 270);
            Padding = new Padding(12);

            var captionLabel = new LabelControl
            {
                Dock = DockStyle.Top,
                Text = "Выберите фамилию и табельный номер:",
                Appearance = { Font = new Font("Segoe UI", 12F, FontStyle.Regular) },
                AutoSizeMode = LabelAutoSizeMode.Vertical,
                Margin = new Padding(0, 0, 0, 20)
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
                },
                Margin = new Padding(0, 20, 0, 0)
            };

            var lookupView = new GridView
            {
                FocusRectStyle = DrawFocusRectStyle.RowFocus,
                OptionsSelection = { EnableAppearanceFocusedCell = false },
                OptionsView = { ShowGroupPanel = false, ColumnAutoWidth = false }
            };

            lookupView.Columns.AddVisible(nameof(FioModel.Fio), "ФИО").Width = 220;
            lookupView.Columns.AddVisible(nameof(FioModel.Tab), "Таб. №").Width = 80;
            lookupView.Columns.AddVisible(nameof(FioModel.Zone), "Зона").Width = 80;
           // lookupView.BestFitColumns();

            _fioLookup.Properties.PopupView = lookupView;
            if (initialTab.HasValue && fioItems.Any(f => f.Tab == initialTab.Value))
            {
                _fioLookup.EditValue = initialTab.Value;
            }


            _tabEdit = new TextEdit
            {
                Dock = DockStyle.Left,
                Height = 84,
                Width = 134,
                TabIndex = 1,
                Properties =
                {
                  //  NullText = "Введите табельный номер и нажмите Enter"
                }
            };

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
            // делаем здоровенные кнопки (x2 от базовых 134x42 → 268x84 условно)
            _okButton.Size = new Size(_okButton.Width * 2, _okButton.Height * 2);
            _cancelButton.Size = new Size(_cancelButton.Width * 2, _cancelButton.Height * 2);
            _tabEdit.Size = new Size(_tabEdit.Width * 2, _tabEdit.Height * 2);
            AcceptButton = _okButton;
            CancelButton = _cancelButton;

            _fioLookup.EditValueChanged += (s, e) =>
            {
                _okButton.Enabled = _fioLookup.EditValue != null;
            };

            _tabEdit.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (int.TryParse(_tabEdit.Text, out var enterTab))
                    {
                        // Попробуем выбрать в списке, если есть
                        var found = fioItems.FirstOrDefault(f => f.Tab == enterTab);
                        if (found != null)
                            _fioLookup.EditValue = enterTab;
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    e.Handled = true;
                }
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
                Height = 96,
                BorderStyle = BorderStyles.NoBorder,
                Padding = new Padding(0, 12, 0, 0)
            };
            buttonsPanel.Controls.Add(_cancelButton);
            buttonsPanel.Controls.Add(_okButton);
            buttonsPanel.Controls.Add(_tabEdit);
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
//            container.Controls.Add(_tabEdit);
            container.Controls.Add(_fioLookup);
            // пустая строка между captionLabel и _fioLookup
            var spacer = new LabelControl { Dock = DockStyle.Top, Height = 25, AutoSizeMode = LabelAutoSizeMode.None };
            container.Controls.Add(spacer);
            container.Controls.Add(captionLabel);

            Controls.Add(container);
            Controls.Add(buttonsPanel);

            ApplyFontRecursive(this, new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 204));
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            //_fioLookup.Focus();
            _tabEdit.Focus();
        }

        private void InitializeComponent()
        {

        }

        private static void ApplyFontRecursive(Control parent, Font font)
        {
            if (parent == null || font == null)
                return;
            parent.Font = font;
            foreach (Control child in parent.Controls)
            {
                ApplyFontRecursive(child, font);
            }
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

