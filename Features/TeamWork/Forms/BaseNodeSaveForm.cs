using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    internal sealed class BaseNodeSaveForm : Form
    {
        private readonly IReadOnlyList<NormRasz> _operations;
        private readonly TextBox _nameTextBox;
        private readonly TextBox _descriptionTextBox;
        private readonly DataGridView _previewGrid;
        private readonly Label _summaryLabel;

        public BaseNodeDefinition ResultNode { get; private set; }

        public BaseNodeSaveForm(IReadOnlyList<NormRasz> operations, string defaultName = null)
        {
            _operations = operations ?? Array.Empty<NormRasz>();

            Text = "Сохранить как базовый узел";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            MinimumSize = new Size(760, 520);
            Size = new Size(900, 620);

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                Padding = new Padding(12)
            };
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            Controls.Add(layout);

            _summaryLabel = new Label
            {
                AutoSize = true,
                Dock = DockStyle.Top,
                Text = BuildSummary()
            };
            layout.Controls.Add(_summaryLabel, 0, 0);

            var namePanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 1,
                RowCount = 2,
                Margin = new Padding(0, 12, 0, 0)
            };
            namePanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            namePanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            namePanel.Controls.Add(new Label { AutoSize = true, Text = "Название узла" }, 0, 0);
            _nameTextBox = new TextBox
            {
                Dock = DockStyle.Top,
                PlaceholderText = "Например: Обработка манжеты",
                Text = defaultName ?? string.Empty
            };
            namePanel.Controls.Add(_nameTextBox, 0, 1);
            layout.Controls.Add(namePanel, 0, 1);

            var descriptionPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 1,
                RowCount = 2,
                Margin = new Padding(0, 10, 0, 0)
            };
            descriptionPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            descriptionPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            descriptionPanel.Controls.Add(new Label { AutoSize = true, Text = "Описание" }, 0, 0);
            _descriptionTextBox = new TextBox
            {
                Dock = DockStyle.Top,
                Multiline = true,
                Height = 72,
                ScrollBars = ScrollBars.Vertical
            };
            descriptionPanel.Controls.Add(_descriptionTextBox, 0, 1);
            layout.Controls.Add(descriptionPanel, 0, 2);

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
            _previewGrid.DataSource = BaseNodeMapper.CreatePreviewRows(_operations.ToList());
            layout.Controls.Add(_previewGrid, 0, 3);

            var buttonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                AutoSize = true,
                Margin = new Padding(0, 12, 0, 0)
            };

            var okButton = new Button
            {
                Text = "Сохранить",
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
            layout.Controls.Add(buttonsPanel, 0, 4);

            AcceptButton = okButton;
            CancelButton = cancelButton;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_nameTextBox.Text))
            {
                MessageBox.Show(this, "Укажите название базового узла.", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _nameTextBox.Focus();
                return;
            }

            if (_operations.Count == 0)
            {
                MessageBox.Show(this, "Нет операций для сохранения.", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ResultNode = BaseNodeMapper.CreateDefinition(_nameTextBox.Text, _descriptionTextBox.Text, _operations);
            DialogResult = DialogResult.OK;
            Close();
        }

        private string BuildSummary()
        {
            int chapters = _operations.Select(x => x.N).Distinct().Count();
            return $"Будут сохранены операций: {_operations.Count}. Глав: {chapters}.";
        }
    }
}
