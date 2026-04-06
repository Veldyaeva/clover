using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    internal sealed partial class BaseNodeSaveForm : Form
    {
        private readonly List<NormRasz> _operations;
        private readonly BaseNodeSaveDefaults _defaults;

        public BaseNodeDefinition ResultNode { get; private set; }

        public BaseNodeSaveForm(IReadOnlyList<NormRasz> operations, string defaultName = null, BaseNodeSaveDefaults defaults = null)
        {
            _operations = (operations ?? Array.Empty<NormRasz>())
                .Select(operation => operation?.Clone())
                .OfType<NormRasz>()
                .ToList();
            _defaults = defaults;

            InitializeComponent();
            InitializeSelectors();

            nameTextBox.Text = defaultName ?? string.Empty;
            RefreshOperationsPreview();
        }

        private void InitializeSelectors()
        {
            nodeGroupComboBox.Items.AddRange(BaseNodeMetadataOptions.NodeGroups);
            productKindComboBox.Items.AddRange(BaseNodeMetadataOptions.ProductKinds);
            productCategoryComboBox.Items.AddRange(BaseNodeMetadataOptions.ProductCategories);

            // Автоподстановка только предлагает значения, но пользователь свободно может их поменять.
            SelectComboValue(nodeGroupComboBox, _defaults?.NodeGroup, string.Empty);
            SelectComboValue(productKindComboBox, _defaults?.ProductKind, "Универсальный");
            SelectComboValue(productCategoryComboBox, _defaults?.ProductCategory, "Универсально");
        }

        private static void SelectComboValue(ComboBox comboBox, string preferredValue, string fallbackValue)
        {
            string valueToSelect = comboBox.Items.Contains(preferredValue)
                ? preferredValue
                : fallbackValue;

            if (comboBox.Items.Contains(valueToSelect))
            {
                comboBox.SelectedItem = valueToSelect;
            }
            else if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                MessageBox.Show(this, "Укажите название базового узла.", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextBox.Focus();
                return;
            }

            if (_operations.Count == 0)
            {
                MessageBox.Show(this, "Нет операций для сохранения.", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ResultNode = BaseNodeMapper.CreateDefinition(nameTextBox.Text, descriptionTextBox.Text, _operations);
            ResultNode.NodeGroup = nodeGroupComboBox.SelectedItem?.ToString() ?? string.Empty;
            ResultNode.ProductKind = productKindComboBox.SelectedItem?.ToString() ?? string.Empty;
            ResultNode.ProductCategory = productCategoryComboBox.SelectedItem?.ToString() ?? string.Empty;

            DialogResult = DialogResult.OK;
            Close();
        }

        private string BuildSummary()
        {
            int chapters = _operations.Select(x => x.N).Distinct().Count();
            return $"Будут сохранены глав: {chapters}. Операций: {_operations.Count}.";
        }

        private void RefreshOperationsPreview(int? selectedIndex = null)
        {
            summaryLabel.Text = BuildSummary();

            previewGrid.DataSource = null;
            previewGrid.DataSource = BaseNodeMapper.CreatePreviewRows(_operations);

            SelectPreviewRow(selectedIndex);
        }

        private int GetSelectedOperationIndex()
        {
            if (previewGrid.CurrentCell != null)
            {
                return previewGrid.CurrentCell.RowIndex;
            }

            if (previewGrid.SelectedRows.Count > 0)
            {
                return previewGrid.SelectedRows[0].Index;
            }

            return -1;
        }

        private void SelectPreviewRow(int? selectedIndex)
        {
            if (!selectedIndex.HasValue || selectedIndex.Value < 0 || selectedIndex.Value >= previewGrid.Rows.Count)
            {
                return;
            }

            previewGrid.ClearSelection();
            var row = previewGrid.Rows[selectedIndex.Value];
            row.Selected = true;
            if (row.Cells.Count > 0)
            {
                previewGrid.CurrentCell = row.Cells[0];
            }
        }

        private bool EnsureCanMoveSelectedOperation(int delta, out int selectedIndex, out int targetIndex)
        {
            selectedIndex = GetSelectedOperationIndex();
            targetIndex = selectedIndex + delta;

            if (selectedIndex < 0)
            {
                MessageBox.Show(this, "Выберите операцию в списке.", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (!BaseNodeOperationEditingHelper.CanMove(_operations, selectedIndex, targetIndex))
            {
                MessageBox.Show(this, "Перемещение доступно только внутри текущей главы узла.", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void MoveUpButton_Click(object sender, EventArgs e)
        {
            if (!EnsureCanMoveSelectedOperation(-1, out int selectedIndex, out int targetIndex))
            {
                return;
            }

            BaseNodeOperationEditingHelper.Move(_operations, selectedIndex, targetIndex);
            RefreshOperationsPreview(targetIndex);
        }

        private void MoveDownButton_Click(object sender, EventArgs e)
        {
            if (!EnsureCanMoveSelectedOperation(1, out int selectedIndex, out int targetIndex))
            {
                return;
            }

            BaseNodeOperationEditingHelper.Move(_operations, selectedIndex, targetIndex);
            RefreshOperationsPreview(targetIndex);
        }

        private void DeleteOperationButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = GetSelectedOperationIndex();
            if (selectedIndex < 0)
            {
                MessageBox.Show(this, "Выберите операцию в списке.", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            BaseNodeOperationEditingHelper.RemoveAt(_operations, selectedIndex);
            RefreshOperationsPreview(Math.Min(selectedIndex, _operations.Count - 1));
        }
    }
}
