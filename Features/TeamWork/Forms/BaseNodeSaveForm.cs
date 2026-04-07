using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Features.TeamWork.Services;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    internal sealed partial class BaseNodeSaveForm : CustomForm
    {
        private readonly BaseNodeLibraryService _libraryService;
        private readonly List<NormRasz> _operations;
        private readonly BaseNodeSaveDefaults _defaults;

        public BaseNodeDefinition ResultNode { get; private set; }

        public BaseNodeSaveForm( UserClass User,
            IReadOnlyList<NormRasz> operations,
            string defaultName = null,
            BaseNodeSaveDefaults defaults = null,
            BaseNodeLibraryService libraryService = null): base(User)
        {
            _libraryService = libraryService;
            _operations = (operations ?? Array.Empty<NormRasz>())
                .Select(operation => operation?.Clone())
                .OfType<NormRasz>()
                .ToList();
            _defaults = defaults;

            InitializeComponent();
            InitializeSelectors();
            Shown += BaseNodeSaveForm_Shown;

            nameTextBox.Text = defaultName ?? string.Empty;
            RefreshOperationsPreview();
        }

        private void InitializeSelectors()
        {
            nodeGroupComboBox.Items.Add(string.Empty);
            productKindComboBox.Items.AddRange(BaseNodeMetadataOptions.NodeTypes);
            productCategoryComboBox.Items.AddRange(BaseNodeMetadataOptions.ProductCategories);

            // Автоподстановка только предлагает значения, но пользователь свободно может их поменять.
            SelectComboValue(nodeGroupComboBox, _defaults?.NodeGroup, string.Empty);
            SelectComboValue(productKindComboBox, _defaults?.NodeType, "Производственный");
            SelectComboValue(productCategoryComboBox, _defaults?.ProductCategory, "Универсально");
        }

        private async void BaseNodeSaveForm_Shown(object sender, EventArgs e)
        {
            Shown -= BaseNodeSaveForm_Shown;
            await LoadNodeGroupsAsync();
        }

        private async Task LoadNodeGroupsAsync()
        {
            if (_libraryService == null)
            {
                return;
            }

            string selectedValue = nodeGroupComboBox.SelectedItem?.ToString() ?? _defaults?.NodeGroup ?? string.Empty;

            try
            {
                var nodeGroups = await _libraryService.GetNodeGroupsAsync();
                ApplyNodeGroups(nodeGroups, selectedValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Не удалось загрузить группы узлов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ApplyNodeGroups(IEnumerable<string> nodeGroups, string selectedValue)
        {
            nodeGroupComboBox.BeginUpdate();
            try
            {
                nodeGroupComboBox.Items.Clear();

                foreach (var nodeGroup in nodeGroups ?? new[] { string.Empty })
                {
                    nodeGroupComboBox.Items.Add(nodeGroup);
                }
            }
            finally
            {
                nodeGroupComboBox.EndUpdate();
            }

            SelectComboValue(nodeGroupComboBox, selectedValue, string.Empty);
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
            ResultNode.NodeCode = StringNormalizer.TrimOrEmpty(_defaults?.SourceArticul);
            ResultNode.NodeGroup = nodeGroupComboBox.SelectedItem?.ToString() ?? string.Empty;
            ResultNode.NodeType = productKindComboBox.SelectedItem?.ToString() ?? string.Empty;
            ResultNode.ProductKind = string.Empty;
            ResultNode.ProductCategory = productCategoryComboBox.SelectedItem?.ToString() ?? string.Empty;

            DialogResult = DialogResult.OK;
            Close();
        }

        private string BuildSummary()
        {
            int chapters = _operations.Select(x => x.N).Distinct().Count();
            return $"Будут сохранены операций: {chapters}. Подопераций: {_operations.Count}.";
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
                MessageBox.Show(this, "Перемещение доступно только внутри текущей операции узла.", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
