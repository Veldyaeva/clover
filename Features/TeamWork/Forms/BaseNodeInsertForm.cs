using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.Features.TeamWork.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    internal sealed class BaseNodeInsertForm : Form
    {
        private readonly IReadOnlyList<BaseNodeDefinition> _nodes;
        private readonly ListBox _nodesListBox;
        private readonly ComboBox _positionComboBox;
        private readonly DataGridView _previewGrid;
        private readonly Label _detailsLabel;

        public BaseNodeDefinition SelectedNode => _nodesListBox.SelectedItem as BaseNodeDefinition;
        public BaseNodeInsertionPoint SelectedInsertionPoint => _positionComboBox.SelectedItem as BaseNodeInsertionPoint;

        public BaseNodeInsertForm(
            IReadOnlyList<BaseNodeDefinition> nodes,
            IReadOnlyList<BaseNodeInsertionPoint> insertionPoints,
            int? defaultAfterN = null)
        {
            _nodes = nodes ?? Array.Empty<BaseNodeDefinition>();

            Text = "Добавить базовый узел";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            MinimumSize = new Size(920, 560);
            Size = new Size(1120, 680);

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3,
                Padding = new Padding(12)
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            Controls.Add(layout);

            var leftPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            leftPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            leftPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            leftPanel.Controls.Add(new Label { AutoSize = true, Text = "Базовые узлы" }, 0, 0);

            _nodesListBox = new ListBox
            {
                Dock = DockStyle.Fill,
                DisplayMember = nameof(BaseNodeDefinition.Name),
                Margin = new Padding(0, 8, 12, 0)
            };
            _nodesListBox.SelectedIndexChanged += (_, __) => RefreshPreview();
            leftPanel.Controls.Add(_nodesListBox, 0, 1);
            layout.Controls.Add(leftPanel, 0, 0);
            layout.SetRowSpan(leftPanel, 2);

            var topPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 1,
                RowCount = 3
            };
            topPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            topPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            topPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            topPanel.Controls.Add(new Label { AutoSize = true, Text = "Куда вставить узел" }, 0, 0);

            _positionComboBox = new ComboBox
            {
                Dock = DockStyle.Top,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Margin = new Padding(0, 8, 0, 0)
            };
            foreach (var point in insertionPoints ?? Array.Empty<BaseNodeInsertionPoint>())
            {
                _positionComboBox.Items.Add(point);
            }
            topPanel.Controls.Add(_positionComboBox, 0, 1);

            _detailsLabel = new Label
            {
                AutoSize = true,
                Dock = DockStyle.Top,
                Margin = new Padding(0, 10, 0, 0)
            };
            topPanel.Controls.Add(_detailsLabel, 0, 2);
            layout.Controls.Add(topPanel, 1, 0);

            _previewGrid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoGenerateColumns = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                Margin = new Padding(0, 12, 0, 0)
            };
            layout.Controls.Add(_previewGrid, 1, 1);

            var buttonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                AutoSize = true,
                Margin = new Padding(0, 12, 0, 0)
            };

            var okButton = new Button
            {
                Text = "Добавить",
                AutoSize = true
            };
            okButton.Click += OkButton_Click;

            var cancelButton = new Button
            {
                Text = "Отмена",
                AutoSize = true,
                DialogResult = DialogResult.Cancel
            };

            buttonsPanel.Controls.Add(okButton);
            buttonsPanel.Controls.Add(cancelButton);
            layout.Controls.Add(buttonsPanel, 1, 2);

            AcceptButton = okButton;
            CancelButton = cancelButton;

            BindData(defaultAfterN);
        }

        private void BindData(int? defaultAfterN)
        {
            _nodesListBox.Items.Clear();
            foreach (var node in _nodes.OrderBy(x => x.Name, StringComparer.CurrentCultureIgnoreCase))
            {
                _nodesListBox.Items.Add(node);
            }

            if (_nodesListBox.Items.Count > 0)
            {
                _nodesListBox.SelectedIndex = 0;
            }

            if (_positionComboBox.Items.Count > 0)
            {
                int preferredIndex = 0;
                if (defaultAfterN.HasValue)
                {
                    for (int i = 0; i < _positionComboBox.Items.Count; i++)
                    {
                        if (_positionComboBox.Items[i] is BaseNodeInsertionPoint point && point.AfterN == defaultAfterN)
                        {
                            preferredIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    var endIndex = _positionComboBox.Items
                        .Cast<BaseNodeInsertionPoint>()
                        .Select((point, index) => new { point, index })
                        .FirstOrDefault(x => x.point.AppendToEnd);

                    if (endIndex != null)
                    {
                        preferredIndex = endIndex.index;
                    }
                }

                _positionComboBox.SelectedIndex = preferredIndex;
            }

            RefreshPreview();
        }

        private void RefreshPreview()
        {
            var node = SelectedNode;
            if (node == null)
            {
                _previewGrid.DataSource = null;
                _detailsLabel.Text = "Выберите базовый узел.";
                return;
            }

            _previewGrid.DataSource = BaseNodeMapper.CreatePreviewRows(node);

            int chapters = node.Operations.Select(x => x.SourceN).Distinct().Count();
            string description = string.IsNullOrWhiteSpace(node.Description) ? "Без описания" : node.Description.Trim();
            _detailsLabel.Text = $"Операций: {node.Operations.Count}. Глав: {chapters}. {description}";
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (SelectedNode == null)
            {
                MessageBox.Show(this, "Выберите базовый узел.", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (SelectedInsertionPoint == null)
            {
                MessageBox.Show(this, "Выберите место вставки.", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
