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
        private readonly BaseNodePreviewPanel _previewPanel;
        private readonly string _defaultName;
        private bool _isNameManuallyEdited;
        private bool _isUpdatingGeneratedName;
        private IReadOnlyList<BaseNodeMetadataItem> _nodeTypes = Array.Empty<BaseNodeMetadataItem>();
        private IReadOnlyList<BaseNodeMetadataItem> _nodeGroups = Array.Empty<BaseNodeMetadataItem>();
        private IReadOnlyList<BaseNodeMetadataItem> _nodeSubgroups = Array.Empty<BaseNodeMetadataItem>();
        private IReadOnlyList<BaseNodeMetadataItem> _productCategories = Array.Empty<BaseNodeMetadataItem>();

        public BaseNodeDefinition ResultNode { get; private set; }

        public BaseNodeSaveForm( UserClass User,
            IReadOnlyList<NormRasz> operations,
            string defaultName = null,
            BaseNodeSaveDefaults defaults = null,
            BaseNodeLibraryService libraryService = null): base(User)
        {
            _libraryService = libraryService;
            _defaultName = defaultName;
            _operations = (operations ?? Array.Empty<NormRasz>())
                .Select(operation => operation?.Clone())
                .OfType<NormRasz>()
                .ToList();
            _defaults = defaults;

            InitializeComponent();
            _previewPanel = BaseNodePreviewHelper.Create(previewPanel, previewSourceLabel, previewImageStatusLabel, previewPictureBox);
            InitializeSelectors();
            Shown += BaseNodeSaveForm_Shown;

            BaseNodePreviewHelper.Update(_previewPanel, _defaults);
            UpdateRtCodeCopyState(_defaults?.SourceRtCode);
            RefreshOperationsPreview();
        }

        private void PreviewSourceLabel_Click(object sender, EventArgs e)
        {
            CopyRtCodeToClipboard();
        }

        private void CopyRtCodeToClipboard()
        {
            string sourceCode = StringNormalizer.TrimOrEmpty(_defaults?.SourceRtCode);
            if (string.IsNullOrWhiteSpace(sourceCode))
                return;

            try
            {
                Clipboard.SetText(sourceCode);
                previewToolTip.Show("RT-код скопирован", this, PointToClient(Cursor.Position), 1500);
            }
            catch
            {
            }
        }

        private void UpdateRtCodeCopyState(string sourceRtCode)
        {
            string sourceArticul = StringNormalizer.TrimOrEmpty(_defaults?.SourceArticul);
            string tooltip = string.IsNullOrWhiteSpace(sourceRtCode)
                ? (string.IsNullOrWhiteSpace(sourceArticul)
                    ? "Источник не задан"
                    : $"Артикул: {sourceArticul}")
                : string.IsNullOrWhiteSpace(sourceArticul)
                    ? $"RT-код: {sourceRtCode}. Кликните, чтобы скопировать"
                    : $"Артикул: {sourceArticul}. RT-код: {sourceRtCode}. Кликните, чтобы скопировать RT-код";
            previewToolTip.SetToolTip(previewSourceLabel, tooltip);
        }

        private string BuildInitialName(string defaultName)
        {
            string trimmedName = StringNormalizer.TrimOrEmpty(defaultName);
            string sourceRtCode = StringNormalizer.TrimOrEmpty(_defaults?.SourceRtCode);
            if (string.IsNullOrWhiteSpace(sourceRtCode))
            {
                return trimmedName;
            }

            if (trimmedName.Contains(sourceRtCode, StringComparison.CurrentCultureIgnoreCase))
            {
                return trimmedName;
            }

            return string.IsNullOrWhiteSpace(trimmedName)
                ? $"РТ {sourceRtCode}"
                : $"РТ {sourceRtCode} - {trimmedName}";
        }

        private void InitializeSelectors()
        {
            nodeGroupComboBox.Items.Add(string.Empty);
            nodeSubgroupComboBox.Items.Add(string.Empty);
            productKindComboBox.Items.AddRange(BaseNodeMetadataOptions.NodeTypes.Cast<object>().ToArray());

            productKindComboBox.SelectedIndexChanged += MetadataComboBox_SelectedIndexChanged;
            productCategoryComboBox.SelectedIndexChanged += MetadataComboBox_SelectedIndexChanged;
            nodeGroupComboBox.SelectedIndexChanged += NodeGroupComboBox_SelectedIndexChanged;
            nodeSubgroupComboBox.SelectedIndexChanged += MetadataComboBox_SelectedIndexChanged;
            nameTextBox.TextChanged += NameTextBox_TextChanged;
        }

        private async void BaseNodeSaveForm_Shown(object sender, EventArgs e)
        {
            Shown -= BaseNodeSaveForm_Shown;
            await LoadMetadataAsync();
        }

        private async Task LoadMetadataAsync()
        {
            if (_libraryService == null)
            {
                _nodeTypes = BaseNodeMetadataOptions.NodeTypes;
                _nodeGroups = BaseNodeMetadataOptions.NodeGroups;
                _productCategories = BuildFallbackProductCategories();
                ApplyNodeTypes(_nodeTypes, _defaults?.NodeTypeId, _defaults?.NodeType);
                ApplyNodeGroups(_nodeGroups, _defaults?.NodeGroupId, _defaults?.NodeGroup);
                ApplyProductCategories(_productCategories, _defaults?.ProductCategory);
                await LoadNodeSubgroupsAsync(_defaults?.NodeSubgroupId, _defaults?.NodeGroupDetail);
                TryApplyGeneratedName(force: true);
                return;
            }

            try
            {
                _nodeTypes = await _libraryService.GetNodeTypesAsync();
                _nodeGroups = await _libraryService.GetNodeGroupsAsync();
                _productCategories = await _libraryService.GetProductCategoriesAsync();
                ApplyNodeTypes(_nodeTypes, _defaults?.NodeTypeId, _defaults?.NodeType);
                ApplyNodeGroups(_nodeGroups, _defaults?.NodeGroupId, _defaults?.NodeGroup);
                ApplyProductCategories(_productCategories, _defaults?.ProductCategory);
                await LoadNodeSubgroupsAsync(_defaults?.NodeSubgroupId, _defaults?.NodeGroupDetail);
                TryApplyGeneratedName(force: true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Не удалось загрузить метаданные узлов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ApplyNodeTypes(IEnumerable<BaseNodeMetadataItem> nodeTypes, int? selectedId, string selectedValue)
        {
            productKindComboBox.BeginUpdate();
            try
            {
                productKindComboBox.Items.Clear();
                foreach (var nodeType in nodeTypes ?? BaseNodeMetadataOptions.NodeTypes)
                {
                    productKindComboBox.Items.Add(nodeType);
                }
            }
            finally
            {
                productKindComboBox.EndUpdate();
            }

            SelectMetadataItem(productKindComboBox, selectedId, selectedValue);
        }

        private void ApplyNodeGroups(IEnumerable<BaseNodeMetadataItem> nodeGroups, int? selectedId, string selectedValue)
        {
            nodeGroupComboBox.BeginUpdate();
            try
            {
                nodeGroupComboBox.Items.Clear();
                nodeGroupComboBox.Items.Add(string.Empty);

                foreach (var nodeGroup in nodeGroups ?? BaseNodeMetadataOptions.NodeGroups)
                {
                    nodeGroupComboBox.Items.Add(nodeGroup);
                }
            }
            finally
            {
                nodeGroupComboBox.EndUpdate();
            }

            SelectMetadataItem(nodeGroupComboBox, selectedId, selectedValue);
        }

        private void ApplyProductCategories(IEnumerable<BaseNodeMetadataItem> productCategories, string selectedValue)
        {
            productCategoryComboBox.BeginUpdate();
            try
            {
                productCategoryComboBox.Items.Clear();
                foreach (var productCategory in productCategories ?? BuildFallbackProductCategories())
                {
                    productCategoryComboBox.Items.Add(productCategory);
                }
            }
            finally
            {
                productCategoryComboBox.EndUpdate();
            }

            SelectMetadataItem(productCategoryComboBox, null, selectedValue);
        }

        private async Task LoadNodeSubgroupsAsync(int? selectedId, string selectedValue)
        {
            var selectedGroup = nodeGroupComboBox.SelectedItem as BaseNodeMetadataItem;
            _nodeSubgroups = selectedGroup == null
                ? Array.Empty<BaseNodeMetadataItem>()
                : _libraryService == null
                    ? BaseNodeMetadataOptions.GetNodeSubgroups(selectedGroup.Id)
                    : await _libraryService.GetNodeSubgroupsAsync(selectedGroup.Id);

            nodeSubgroupComboBox.BeginUpdate();
            try
            {
                nodeSubgroupComboBox.Items.Clear();
                nodeSubgroupComboBox.Items.Add(string.Empty);
                foreach (var subgroup in _nodeSubgroups)
                {
                    nodeSubgroupComboBox.Items.Add(subgroup);
                }
            }
            finally
            {
                nodeSubgroupComboBox.EndUpdate();
            }

            SelectMetadataItem(nodeSubgroupComboBox, selectedId, selectedValue);
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

        private static IReadOnlyList<BaseNodeMetadataItem> BuildFallbackProductCategories()
        {
            return BaseNodeMetadataOptions.ProductCategories
                .Select((name, index) => new BaseNodeMetadataItem
                {
                    Id = index + 1,
                    Code = $"PC_{index + 1}",
                    Name = name,
                    SortOrder = (index + 1) * 10
                })
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.Name)
                .ToList();
        }

        private static void SelectMetadataItem(ComboBox comboBox, int? selectedId, string selectedValue)
        {
            BaseNodeMetadataItem selectedItem = null;
            if (selectedId.HasValue)
            {
                selectedItem = comboBox.Items
                    .OfType<BaseNodeMetadataItem>()
                    .FirstOrDefault(item => item.Id == selectedId.Value);
            }

            if (selectedItem == null && !string.IsNullOrWhiteSpace(selectedValue))
            {
                selectedItem = comboBox.Items
                    .OfType<BaseNodeMetadataItem>()
                    .FirstOrDefault(item => string.Equals(item.Name, selectedValue, StringComparison.CurrentCultureIgnoreCase));
            }

            comboBox.SelectedItem = selectedItem ?? comboBox.Items.Cast<object>().FirstOrDefault();
        }

        private async void NodeGroupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadNodeSubgroupsAsync(null, string.Empty);
            TryApplyGeneratedName(force: false);
        }

        private void MetadataComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TryApplyGeneratedName(force: false);
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_isUpdatingGeneratedName)
            {
                return;
            }

            _isNameManuallyEdited = !string.IsNullOrWhiteSpace(nameTextBox.Text);
        }

        private void TryApplyGeneratedName(bool force)
        {
            string generatedName = BuildGeneratedName();
            if (string.IsNullOrWhiteSpace(generatedName))
            {
                generatedName = BuildInitialName(_defaultName);
            }

            if (!force && _isNameManuallyEdited && !string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                return;
            }

            _isUpdatingGeneratedName = true;
            try
            {
                nameTextBox.Text = generatedName;
                _isNameManuallyEdited = false;
            }
            finally
            {
                _isUpdatingGeneratedName = false;
            }
        }

        private string BuildGeneratedName()
        {
            return BaseNodeNameBuilder.Build(
                (nodeGroupComboBox.SelectedItem as BaseNodeMetadataItem)?.Name ?? nodeGroupComboBox.SelectedItem?.ToString(),
                (nodeSubgroupComboBox.SelectedItem as BaseNodeMetadataItem)?.Name ?? nodeSubgroupComboBox.SelectedItem?.ToString(),
                (productCategoryComboBox.SelectedItem as BaseNodeMetadataItem)?.Name ?? productCategoryComboBox.SelectedItem?.ToString());
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
            ResultNode.NodeCode = StringNormalizer.TrimOrEmpty(_defaults?.SourceRtCode);
            ResultNode.SourceAnnId = _defaults?.SourceAnnId;
            ResultNode.SourceArticul = StringNormalizer.TrimOrEmpty(_defaults?.SourceArticul);
            ResultNode.SourceRtCode = StringNormalizer.TrimOrEmpty(_defaults?.SourceRtCode);
            ResultNode.SourceImagePath = StringNormalizer.TrimOrEmpty(_defaults?.SourceImagePath);
            ResultNode.NodeGroup = (nodeGroupComboBox.SelectedItem as BaseNodeMetadataItem)?.Name ?? string.Empty;
            ResultNode.NodeGroupId = (nodeGroupComboBox.SelectedItem as BaseNodeMetadataItem)?.Id;
            ResultNode.NodeGroupDetail = (nodeSubgroupComboBox.SelectedItem as BaseNodeMetadataItem)?.Name ?? string.Empty;
            ResultNode.NodeSubgroupId = (nodeSubgroupComboBox.SelectedItem as BaseNodeMetadataItem)?.Id;
            ResultNode.NodeType = (productKindComboBox.SelectedItem as BaseNodeMetadataItem)?.Name ?? string.Empty;
            ResultNode.NodeTypeId = (productKindComboBox.SelectedItem as BaseNodeMetadataItem)?.Id;
            ResultNode.ProductKind = string.Empty;
            ResultNode.ProductCategory = (productCategoryComboBox.SelectedItem as BaseNodeMetadataItem)?.Name
                ?? productCategoryComboBox.SelectedItem?.ToString()
                ?? string.Empty;

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

