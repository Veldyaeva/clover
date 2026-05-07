using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Features.TeamWork.Services;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    internal sealed partial class BaseNodeLibraryEditorForm : CustomForm
    {
        private readonly BaseNodeLibraryService _libraryService;
        private readonly List<BaseNodeDefinition> _nodes = new List<BaseNodeDefinition>();
        private readonly int? _preferredNodeId;
        private readonly Action<int> _openSourceArticle;
        private readonly BaseNodePreviewPanel _previewPanel;
        private BaseNodeDefinition _workingNode;
        private bool _updatingFilters;
        private bool _isUpdatingGeneratedName;
        private bool _isNameManuallyEdited;
        private IReadOnlyList<BaseNodeMetadataItem> _nodeTypes = Array.Empty<BaseNodeMetadataItem>();
        private IReadOnlyList<BaseNodeMetadataItem> _nodeGroups = Array.Empty<BaseNodeMetadataItem>();
        private IReadOnlyList<BaseNodeMetadataItem> _nodeSubgroups = Array.Empty<BaseNodeMetadataItem>();
        private IReadOnlyList<BaseNodeMetadataItem> _productCategories = Array.Empty<BaseNodeMetadataItem>();

        public BaseNodeDefinition SelectedNode => nodeCardsListView.SelectedItems.Count > 0
            ? nodeCardsListView.SelectedItems[0].Tag as BaseNodeDefinition
            : null;
        public int? SelectedBaseNodeId => SelectedNode?.BaseNodeId;

        public BaseNodeLibraryEditorForm(
            UserClass User,
            BaseNodeLibraryService libraryService,
            int? preferredNodeId = null,
            Action<int> openSourceArticle = null):base(User)
        {
            _libraryService = libraryService ?? throw new ArgumentNullException(nameof(libraryService));
            _preferredNodeId = preferredNodeId;
            _openSourceArticle = openSourceArticle;

            InitializeComponent();
            _previewPanel = BaseNodePreviewHelper.Create(previewPanel, previewSourceLabel, previewImageStatusLabel, previewPictureBox);
            searchTextBox.PlaceholderText = "Поиск";
            searchTextBoxitem.Text = "Поиск по названию";
            libraryProductKindFilterComboBoxitem.Text = "Тип узла";
            libraryProductCategoryFilterComboBoxitem.Text = "Категория изделия";
            libraryNodeGroupFilterComboBoxitem.Text = "Группа узла";
            InitializeSelectors();
            Shown += BaseNodeLibraryEditorForm_Shown;
        }

        private async void BaseNodeLibraryEditorForm_Shown(object sender, EventArgs e)
        {
            Shown -= BaseNodeLibraryEditorForm_Shown;
            await LoadNodeGroupsAsync();
            await ReloadNodesAsync(_preferredNodeId);
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

        private async Task LoadNodeGroupsAsync()
        {
            try
            {
                //                _nodeTypes = await _libraryService.GetNodeTypesAsync();
                //                _nodeGroups = await _libraryService.GetNodeGroupsAsync();
                //                _productCategories = await _libraryService.GetProductCategoriesAsync();
                var nodeTypes = await _libraryService.GetNodeTypesAsync();
                var nodeGroups = await _libraryService.GetNodeGroupsAsync();
                var productCategories = await _libraryService.GetProductCategoriesAsync();

                _nodeTypes = nodeTypes != null && nodeTypes.Count > 0
                    ? nodeTypes
                    : BaseNodeMetadataOptions.NodeTypes;

                _nodeGroups = nodeGroups != null && nodeGroups.Count > 0
                    ? nodeGroups
                    : BaseNodeMetadataOptions.NodeGroups;

                _productCategories = productCategories != null && productCategories.Count > 0
                    ? productCategories
                    : BuildFallbackProductCategories();
                ApplyNodeTypes(_workingNode?.NodeTypeId, _workingNode?.NodeType);
                ApplyNodeGroups(_workingNode?.NodeGroupId, _workingNode?.NodeGroup);
                ApplyProductCategories(_workingNode?.ProductCategory);
                await LoadNodeSubgroupsAsync(_workingNode?.NodeSubgroupId, _workingNode?.NodeGroupDetail);
                PopulateFilterValuesFromMetadata();
            }
            catch (Exception ex)
            {
                _nodeTypes = BaseNodeMetadataOptions.NodeTypes;
                _nodeGroups = BaseNodeMetadataOptions.NodeGroups;
                _productCategories = BuildFallbackProductCategories();
                ApplyNodeTypes(_workingNode?.NodeTypeId, _workingNode?.NodeType);
                ApplyNodeGroups(_workingNode?.NodeGroupId, _workingNode?.NodeGroup);
                ApplyProductCategories(_workingNode?.ProductCategory);
                await LoadNodeSubgroupsAsync(_workingNode?.NodeSubgroupId, _workingNode?.NodeGroupDetail);
                PopulateFilterValuesFromMetadata();
                MessageBox.Show(this, $"Не удалось загрузить группы узлов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ApplyNodeTypes(int? selectedId, string selectedValue)
        {
            productKindComboBox.BeginUpdate();
            try
            {
                productKindComboBox.Items.Clear();
                foreach (var nodeType in _nodeTypes)
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

        private void ApplyNodeGroups(int? selectedId, string selectedValue)
        {
            nodeGroupComboBox.BeginUpdate();
            try
            {
                nodeGroupComboBox.Items.Clear();
                nodeGroupComboBox.Items.Add(string.Empty);

                foreach (var nodeGroup in _nodeGroups)
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

        private void ApplyProductCategories(string selectedValue)
        {
            productCategoryComboBox.BeginUpdate();
            try
            {
                productCategoryComboBox.Items.Clear();
                foreach (var productCategory in _productCategories.Any() ? _productCategories : BuildFallbackProductCategories())
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
            //_nodeSubgroups = selectedGroup == null
            //    ? Array.Empty<BaseNodeMetadataItem>()
            // //   : await _libraryService.GetNodeSubgroupsAsync(selectedGroup.Id);
            // : await TryLoadNodeSubgroupsWithFallback(selectedGroup.Id);
                        if (selectedGroup == null)
                            {
                _nodeSubgroups = Array.Empty<BaseNodeMetadataItem>();
                            }
                        else if (_libraryService == null)
                            {
                _nodeSubgroups = BaseNodeMetadataOptions.GetNodeSubgroups(selectedGroup.Id);
                            }
                        else
                            {
                                try
                {
                    var fromDb = await _libraryService.GetNodeSubgroupsAsync(selectedGroup.Id);
                    _nodeSubgroups = fromDb != null && fromDb.Count > 0
                                            ? fromDb
                                            : BaseNodeMetadataOptions.GetNodeSubgroups(selectedGroup.Id);
                                    }
                                catch
                {
                    _nodeSubgroups = BaseNodeMetadataOptions.GetNodeSubgroups(selectedGroup.Id);
                                    }
                            }
                nodeSubgroupComboBox.BeginUpdate();
            try
            {
                nodeSubgroupComboBox.Items.Clear();
                nodeSubgroupComboBox.Items.Add(string.Empty);

                foreach (var nodeSubgroup in _nodeSubgroups)
                {
                    nodeSubgroupComboBox.Items.Add(nodeSubgroup);
                }
            }
            finally
            {
                nodeSubgroupComboBox.EndUpdate();
            }

            SelectMetadataItem(nodeSubgroupComboBox, selectedId, selectedValue);
        }
        private async Task<IReadOnlyList<BaseNodeMetadataItem>> TryLoadNodeSubgroupsWithFallback(int nodeGroupId)
        {
            if (_libraryService == null)
                return BaseNodeMetadataOptions.GetNodeSubgroups(nodeGroupId);

            try
            {
                var fromDb = await _libraryService.GetNodeSubgroupsAsync(nodeGroupId);
                if (fromDb != null && fromDb.Count > 0)
                    return fromDb;
            }
            catch
            {
            }

            return BaseNodeMetadataOptions.GetNodeSubgroups(nodeGroupId);
        }
        private async Task ReloadNodesAsync(int? preferredNodeId = null)
        {
            ToggleBusyState(true);
            try
            {
                var nodes = await _libraryService.GetAllAsync();
                _nodes.Clear();
                _nodes.AddRange(nodes.OrderBy(x => x.DisplayName, StringComparer.CurrentCultureIgnoreCase));
                PopulateFilterValuesFromMetadata();
                ApplyNodeFilter(preferredNodeId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Не удалось загрузить библиотеку узлов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ToggleBusyState(false);
            }
        }

        private void BindSelectedNode()
        {
            var node = SelectedNode;
            bool hasNode = node != null;

     //       editorPanel.Enabled = hasNode;
            saveButton.Enabled = hasNode;
            deleteButton.Enabled = hasNode;

            if (!hasNode)
            {
                _workingNode = null;
                nodeCodeValueLabel.Text = "-";
                nodeCodeValueLabel.Visible = true;
                nameTextBox.Text = string.Empty;
                descriptionTextBox.Text = string.Empty;
                nodeGroupComboBox.SelectedIndex = nodeGroupComboBox.Items.Count > 0 ? 0 : -1;
                productKindComboBox.SelectedItem = "Производственный";
                SelectMetadataItem(productCategoryComboBox, null, "Универсально");
                detailsLabel.Text = "Выберите базовый узел для редактирования.";
                previewTitleLabel.Text = "Визуальная библиотека узлов";
                previewGrid.DataSource = null;
                BaseNodePreviewHelper.Update(_previewPanel, (BaseNodeDefinition)null);
                UpdateOperationButtonsState();
                return;
            }

            _workingNode = CloneNode(node);

            nodeCodeValueLabel.Text = string.IsNullOrWhiteSpace(_workingNode.NodeCode) ? "-" : _workingNode.NodeCode;
      //     nodeCodeLabel.Visible = true;
            nodeCodeValueLabel.Visible = true;
            nameTextBox.Text = _workingNode.Name ?? string.Empty;
            descriptionTextBox.Text = _workingNode.Description ?? string.Empty;
            ApplyNodeTypes(_workingNode.NodeTypeId, _workingNode.NodeType);
            ApplyNodeGroups(_workingNode.NodeGroupId, _workingNode.NodeGroup);
            _ = LoadNodeSubgroupsAsync(_workingNode.NodeSubgroupId, _workingNode.NodeGroupDetail);
            SelectMetadataItem(productCategoryComboBox, null, _workingNode.ProductCategory);
            BaseNodePreviewHelper.Update(_previewPanel, _workingNode);
            UpdateRtCodeCopyState(_workingNode.SourceRtCode, _workingNode.NodeCode);
            previewTitleLabel.Text = string.IsNullOrWhiteSpace(_workingNode.Name) ? _workingNode.DisplayName : _workingNode.Name;
            _isNameManuallyEdited = !string.IsNullOrWhiteSpace(_workingNode.Name);

            RefreshOperationsPreview();
        }

        private void NodeCardsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSelectedNode();
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            ApplyNodeFilter(SelectedNode?.BaseNodeId);
        }

        private void FilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_updatingFilters)
                return;

            ApplyNodeFilter(SelectedNode?.BaseNodeId);
        }

        private void PreviewSourceLabel_Click(object sender, EventArgs e)
        {
            if (TryOpenSourceArticle())
            {
                Close();
                return;
            }

            CopyRtCodeToClipboard();
        }

        private void NodeCodeValueLabel_Click(object sender, EventArgs e)
        {
            CopyRtCodeToClipboard();
        }

        private void CopyRtCodeToClipboard()
        {
            string sourceCode = StringNormalizer.TrimOrEmpty(_workingNode?.SourceRtCode);
            if (string.IsNullOrWhiteSpace(sourceCode))
                return;

            try
            {
                Clipboard.SetText(sourceCode);
                previewToolTip.Show("RT-код скопирован", this, PointToClient(Cursor.Position), 1500);
            }
            catch
            {
                // suppress clipboard failures in UI affordance
            }
        }

        private bool TryOpenSourceArticle()
        {
            if (_openSourceArticle == null || _workingNode?.SourceAnnId == null)
                return false;

            var sourceAnnId = _workingNode.SourceAnnId.Value;
            if (sourceAnnId <= 0)
                return false;

            _openSourceArticle(sourceAnnId);
            return true;
        }

        private void UpdateRtCodeCopyState(string sourceRtCode, string nodeCode)
        {
            string sourceArticul = StringNormalizer.TrimOrEmpty(_workingNode?.SourceArticul);
            string codeToCopy = StringNormalizer.TrimOrEmpty(sourceRtCode);
            if (string.IsNullOrWhiteSpace(codeToCopy))
                codeToCopy = StringNormalizer.TrimOrEmpty(nodeCode);

            string tooltip;
            if (_openSourceArticle != null && _workingNode?.SourceAnnId > 0)
            {
                tooltip = string.IsNullOrWhiteSpace(sourceArticul)
                    ? $"RT-код: {codeToCopy}. Кликните, чтобы открыть исходный РТ"
                    : $"Артикул: {sourceArticul}. RT-код: {codeToCopy}. Кликните, чтобы открыть исходный РТ";
            }
            else
            {
                tooltip = string.IsNullOrWhiteSpace(codeToCopy)
                    ? (string.IsNullOrWhiteSpace(sourceArticul)
                        ? "Источник не задан"
                        : $"Артикул: {sourceArticul}")
                    : string.IsNullOrWhiteSpace(sourceArticul)
                        ? $"RT-код: {codeToCopy}. Кликните, чтобы скопировать"
                        : $"Артикул: {sourceArticul}. RT-код: {codeToCopy}. Кликните, чтобы скопировать RT-код";
            }

            previewToolTip.SetToolTip(previewSourceLabel, tooltip);
            previewToolTip.SetToolTip(nodeCodeValueLabel, tooltip);
        }

        private void ApplyNodeFilter(int? preferredNodeId)
        {
            string filter = StringNormalizer.TrimOrEmpty(searchTextBox?.Text);
            string productKind = StringNormalizer.TrimOrEmpty(libraryProductKindFilterComboBox.SelectedItem?.ToString());
            string productCategory = StringNormalizer.TrimOrEmpty(libraryProductCategoryFilterComboBox.SelectedItem?.ToString());
            string nodeGroup = StringNormalizer.TrimOrEmpty(libraryNodeGroupFilterComboBox.SelectedItem?.ToString());
            var filtered = _nodes
                .Where(node => MatchesNodeFilter(node, filter, productKind, productCategory, nodeGroup))
                .OrderBy(node => node.DisplayName, StringComparer.CurrentCultureIgnoreCase)
                .ToList();

            nodeCardsListView.BeginUpdate();
            try
            {
                nodeCardsListView.Items.Clear();
                nodeCardsImageList.Images.Clear();

                foreach (var node in filtered)
                {
                    string imageKey = $"node-{node.BaseNodeId}-{nodeCardsImageList.Images.Count}";
                    nodeCardsImageList.Images.Add(imageKey, CreateNodeCardImage(node.SourceImagePath));

                    var item = new ListViewItem(string.IsNullOrWhiteSpace(node.Name) ? node.DisplayName : node.Name)
                    {
                        Tag = node,
                        ImageKey = imageKey
                    };
                    item.SubItems.Add(BuildNodeSourceSubtitle(node));
                    item.SubItems.Add(BuildNodeCardSubtitle(node));
                    nodeCardsListView.Items.Add(item);
                }
            }
            finally
            {
                nodeCardsListView.EndUpdate();
            }

            if (nodeCardsListView.Items.Count == 0)
            {
                nodeCardsListView.SelectedIndices.Clear();
                BindSelectedNode();
                return;
            }

            int selectedIndex = 0;
            if (preferredNodeId.HasValue)
            {
                for (int i = 0; i < nodeCardsListView.Items.Count; i++)
                {
                    if (nodeCardsListView.Items[i].Tag is BaseNodeDefinition node && node.BaseNodeId == preferredNodeId.Value)
                    {
                        selectedIndex = i;
                        break;
                    }
                }
            }

            if (nodeCardsListView.Items.Count > selectedIndex)
            {
                nodeCardsListView.Items[selectedIndex].Selected = true;
                nodeCardsListView.Items[selectedIndex].Focused = true;
                nodeCardsListView.EnsureVisible(selectedIndex);
            }
            BindSelectedNode();
        }

        private void PopulateFilterValuesFromMetadata()
        {
            _updatingFilters = true;
            try
            {
                //                PopulateFilterComboBox(libraryProductKindFilterComboBox, _nodes.Where(node => node != null).Select(GetProductKindValue));
                //                PopulateFilterComboBox(libraryProductCategoryFilterComboBox, _nodes.Where(node => node != null).Select(node => node.ProductCategory));
                //                PopulateFilterComboBox(libraryNodeGroupFilterComboBox, _nodes.Where(node => node != null).Select(node => node.NodeGroup));
                PopulateFilterComboBox(libraryProductKindFilterComboBox, _nodeTypes.Select(x => x.Name));
                PopulateFilterComboBox(libraryProductCategoryFilterComboBox, _productCategories.Select(x => x.Name));
                PopulateFilterComboBox(libraryNodeGroupFilterComboBox, _nodeGroups.Select(x => x.Name));
            }
            finally
            {
                _updatingFilters = false;
            }
        }

        private static void PopulateFilterComboBox(ComboBox comboBox, IEnumerable<string> values)
        {
            string selectedValue = comboBox.SelectedItem?.ToString() ?? string.Empty;
            var items = values
                .Select(StringNormalizer.TrimOrEmpty)
                .Where(value => !string.IsNullOrWhiteSpace(value))
                .Distinct(StringComparer.CurrentCultureIgnoreCase)
                .OrderBy(value => value, StringComparer.CurrentCultureIgnoreCase)
                .ToList();

            comboBox.BeginUpdate();
            try
            {
                comboBox.Items.Clear();
                comboBox.Items.Add(string.Empty);
                foreach (var item in items)
                    comboBox.Items.Add(item);
            }
            finally
            {
                comboBox.EndUpdate();
            }

            comboBox.SelectedItem = comboBox.Items.Contains(selectedValue)
                ? selectedValue
                : string.Empty;
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

        private static bool MatchesNodeFilter(BaseNodeDefinition node, string filter, string productKind, string productCategory, string nodeGroup)
        {
            if (node == null)
                return false;

            if (!string.IsNullOrWhiteSpace(productKind) &&
                !string.Equals(GetProductKindValue(node), productKind, StringComparison.CurrentCultureIgnoreCase))
                return false;

            if (!string.IsNullOrWhiteSpace(productCategory) &&
                !string.Equals(StringNormalizer.TrimOrEmpty(node.ProductCategory), productCategory, StringComparison.CurrentCultureIgnoreCase))
                return false;

            if (!string.IsNullOrWhiteSpace(nodeGroup) &&
                !string.Equals(StringNormalizer.TrimOrEmpty(node.NodeGroup), nodeGroup, StringComparison.CurrentCultureIgnoreCase))
                return false;

            if (string.IsNullOrWhiteSpace(filter))
                return true;

            string haystack = string.Join(" ", new[]
            {
                node.DisplayName,
                node.Name,
                node.NodeCode,
                node.SourceArticul,
                node.SourceRtCode,
                node.NodeGroup,
                node.NodeType,
                node.ProductKind,
                node.NodeGroupDetail,
                node.ProductCategory,
                node.Description
            }).ToLowerInvariant();

            var tokens = filter.ToLowerInvariant()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return tokens.All(token => haystack.Contains(token));
        }

        private static string GetProductKindValue(BaseNodeDefinition node)
        {
            //return StringNormalizer.TrimOrEmpty(node?.ProductKind) switch
            //{
            //    { Length: > 0 } value => value,
            //    _ => StringNormalizer.TrimOrEmpty(node?.NodeType)
            //};
            return StringNormalizer.TrimOrEmpty(node?.NodeType);
        }

        private static string BuildNodeCardSubtitle(BaseNodeDefinition node)
        {
            string productCategory = string.IsNullOrWhiteSpace(node?.ProductCategory) ? "Без категории" : node.ProductCategory;
            string nodeGroup = string.IsNullOrWhiteSpace(node?.NodeGroup) ? "Без группы" : node.NodeGroup;
            string detail = string.IsNullOrWhiteSpace(node?.NodeGroupDetail) ? string.Empty : $" • {node.NodeGroupDetail}";
            return $"{productCategory} • {nodeGroup}{detail}";
        }

        private static string BuildNodeSourceSubtitle(BaseNodeDefinition node)
        {
            if (!string.IsNullOrWhiteSpace(node?.SourceArticul) && !string.IsNullOrWhiteSpace(node?.SourceRtCode))
            {
                return $"Арт.: {node.SourceArticul} • РТ: {node.SourceRtCode}";
            }

            if (!string.IsNullOrWhiteSpace(node?.SourceArticul))
            {
                return $"Арт.: {node.SourceArticul}";
            }

            if (!string.IsNullOrWhiteSpace(node?.SourceRtCode))
            {
                return $"РТ: {node.SourceRtCode}";
            }

            return "Источник: -";
        }

        private static Image CreateNodeCardImage(string imagePath)
        {
            const int imageSize = 96;

            if (!string.IsNullOrWhiteSpace(imagePath) && File.Exists(imagePath))
            {
                try
                {
                    using var sourceImage = Image.FromFile(imagePath);
                    return new Bitmap(sourceImage, new Size(imageSize, imageSize));
                }
                catch
                {
                }
            }

            var bitmap = new Bitmap(imageSize, imageSize);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.WhiteSmoke);
            using var borderPen = new Pen(Color.Gainsboro);
            graphics.DrawRectangle(borderPen, 0, 0, imageSize - 1, imageSize - 1);
            using var font = new Font("Segoe UI", 9F, FontStyle.Bold);
            using var textBrush = new SolidBrush(Color.DimGray);
            var rectangle = new RectangleF(8, 8, imageSize - 16, imageSize - 16);
            graphics.DrawString("Узел", font, textBrush, rectangle, new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            });
            return bitmap;
        }

        private static void SelectComboValue(ComboBox comboBox, string value, string fallback)
        {
            string targetValue = string.IsNullOrWhiteSpace(value) ? fallback : StringNormalizer.TrimOrEmpty(value);
            if (comboBox.Items.Contains(targetValue))
            {
                comboBox.SelectedItem = targetValue;
                return;
            }

            if (!string.IsNullOrWhiteSpace(targetValue))
            {
                comboBox.Items.Add(targetValue);
                comboBox.SelectedItem = targetValue;
                return;
            }

            comboBox.SelectedIndex = comboBox.Items.Count > 0 ? 0 : -1;
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
            if (_workingNode == null)
            {
                return;
            }

            string generatedName = BaseNodeNameBuilder.Build(
                (nodeGroupComboBox.SelectedItem as BaseNodeMetadataItem)?.Name ?? nodeGroupComboBox.SelectedItem?.ToString(),
                (nodeSubgroupComboBox.SelectedItem as BaseNodeMetadataItem)?.Name ?? nodeSubgroupComboBox.SelectedItem?.ToString(),
                (productCategoryComboBox.SelectedItem as BaseNodeMetadataItem)?.Name ?? productCategoryComboBox.SelectedItem?.ToString());

            if (string.IsNullOrWhiteSpace(generatedName))
            {
                return;
            }

            if (!force && _isNameManuallyEdited && !string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                return;
            }

            _isUpdatingGeneratedName = true;
            try
            {
                nameTextBox.Text = generatedName;
            }
            finally
            {
                _isUpdatingGeneratedName = false;
            }
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            var node = SelectedNode;
            if (node == null || _workingNode == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                MessageBox.Show(this, "Укажите название базового узла.", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextBox.Focus();
                return;
            }

            if (_workingNode.Operations.Count == 0)
            {
                MessageBox.Show(this, "В узле должна остаться хотя бы одна операция.", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var updatedNode = CloneNode(_workingNode);
            updatedNode.Name = StringNormalizer.TrimOrEmpty(nameTextBox.Text);
            updatedNode.Description = StringNormalizer.TrimOrEmpty(descriptionTextBox.Text);
            updatedNode.NodeGroup = (nodeGroupComboBox.SelectedItem as BaseNodeMetadataItem)?.Name ?? string.Empty;
            updatedNode.NodeGroupId = (nodeGroupComboBox.SelectedItem as BaseNodeMetadataItem)?.Id;
            updatedNode.NodeGroupDetail = (nodeSubgroupComboBox.SelectedItem as BaseNodeMetadataItem)?.Name ?? string.Empty;
            updatedNode.NodeSubgroupId = (nodeSubgroupComboBox.SelectedItem as BaseNodeMetadataItem)?.Id;
            updatedNode.NodeType = (productKindComboBox.SelectedItem as BaseNodeMetadataItem)?.Name ?? string.Empty;
            updatedNode.NodeTypeId = (productKindComboBox.SelectedItem as BaseNodeMetadataItem)?.Id;
            updatedNode.ProductKind = string.Empty;
            updatedNode.ProductCategory = StringNormalizer.TrimOrEmpty(
                (productCategoryComboBox.SelectedItem as BaseNodeMetadataItem)?.Name
                ?? productCategoryComboBox.SelectedItem?.ToString());

            ToggleBusyState(true);
            try
            {
                var savedNode = await _libraryService.SaveAsync(updatedNode);
                await ReloadNodesAsync(savedNode.BaseNodeId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Не удалось сохранить базовый узел: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ToggleBusyState(false);
            }
        }

        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            var node = SelectedNode;
            if (node == null)
            {
                return;
            }

            var result = MessageBox.Show(
                this,
                $"Деактивировать базовый узел \"{node.Name}\"?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes)
            {
                return;
            }

            ToggleBusyState(true);
            try
            {
                await _libraryService.DeleteAsync(node.BaseNodeId);

                int? nextPreferredId = _nodes
                    .Where(x => x.BaseNodeId != node.BaseNodeId)
                    .Select(x => (int?)x.BaseNodeId)
                    .FirstOrDefault();

                await ReloadNodesAsync(nextPreferredId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Не удалось удалить базовый узел: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ToggleBusyState(false);
            }
        }

        private void ToggleBusyState(bool isBusy)
        {
            UseWaitCursor = isBusy;
            nodeCardsListView.Enabled = !isBusy;
            //editorPanel.Enabled = !isBusy && SelectedNode != null;
            saveButton.Enabled = !isBusy && SelectedNode != null;
            deleteButton.Enabled = !isBusy && SelectedNode != null;
            closeButton.Enabled = !isBusy;
            UpdateOperationButtonsState(isBusy);
        }

        private void UpdateOperationButtonsState(bool isBusy = false)
        {
            bool hasOperations = _workingNode?.Operations?.Count > 0;
            deleteOperationButton.Enabled = !isBusy && hasOperations;
            moveUpButton.Enabled = !isBusy && (_workingNode?.Operations?.Count ?? 0) > 1;
            moveDownButton.Enabled = !isBusy && (_workingNode?.Operations?.Count ?? 0) > 1;
        }

        private void RefreshOperationsPreview(int? selectedIndex = null)
        {
            int chapters = _workingNode?.Operations?.Select(x => x.SourceN).Distinct().Count() ?? 0;
            detailsLabel.Text = _workingNode == null
                ? "Выберите базовый узел для редактирования."
                : $"РТ: {(string.IsNullOrWhiteSpace(_workingNode.SourceRtCode) ? "-" : _workingNode.SourceRtCode)}{Environment.NewLine}" +
                  $"Тип узла: {(string.IsNullOrWhiteSpace(GetProductKindValue(_workingNode)) ? "-" : GetProductKindValue(_workingNode))}{Environment.NewLine}" +
                  $"Категория: {(string.IsNullOrWhiteSpace(_workingNode.ProductCategory) ? "-" : _workingNode.ProductCategory)}{Environment.NewLine}" +
                  $"Группа: {(string.IsNullOrWhiteSpace(_workingNode.NodeGroup) ? "-" : _workingNode.NodeGroup)}{Environment.NewLine}" +
                  $"Уточнение: {(string.IsNullOrWhiteSpace(_workingNode.NodeGroupDetail) ? "-" : _workingNode.NodeGroupDetail)}{Environment.NewLine}" +
                  $"Операций: {chapters}. Подопераций: {_workingNode.Operations.Count}.";

            previewGrid.DataSource = null;
            previewGrid.DataSource = _workingNode == null
                ? null
                : BaseNodeMapper.CreatePreviewRows(_workingNode);

            SelectPreviewRow(selectedIndex);
            UpdateOperationButtonsState();
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

        private void PreviewGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            EditOperationAt(e.RowIndex);
        }

        private void EditOperationAt(int selectedIndex)
        {
            if (_workingNode == null || selectedIndex < 0 || selectedIndex >= _workingNode.Operations.Count)
            {
                return;
            }

            using var form = new BaseNodeOperationEditForm(User, _workingNode.Operations[selectedIndex]);
            if (form.ShowDialog(this) != DialogResult.OK || form.ResultOperation == null)
            {
                return;
            }

            _workingNode.Operations[selectedIndex] = form.ResultOperation;
            RefreshOperationsPreview(selectedIndex);
        }

        private bool EnsureCanMoveSelectedOperation(int delta, out int selectedIndex, out int targetIndex)
        {
            selectedIndex = GetSelectedOperationIndex();
            targetIndex = selectedIndex + delta;

            if (_workingNode == null || selectedIndex < 0)
            {
                MessageBox.Show(this, "Выберите операцию в списке.", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (!BaseNodeOperationEditingHelper.CanMove(_workingNode.Operations, selectedIndex, targetIndex))
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

            BaseNodeOperationEditingHelper.Move(_workingNode.Operations, selectedIndex, targetIndex);
            RefreshOperationsPreview(targetIndex);
        }

        private void MoveDownButton_Click(object sender, EventArgs e)
        {
            if (!EnsureCanMoveSelectedOperation(1, out int selectedIndex, out int targetIndex))
            {
                return;
            }

            BaseNodeOperationEditingHelper.Move(_workingNode.Operations, selectedIndex, targetIndex);
            RefreshOperationsPreview(targetIndex);
        }

        private void DeleteOperationButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = GetSelectedOperationIndex();
            if (_workingNode == null || selectedIndex < 0)
            {
                MessageBox.Show(this, "Выберите операцию в списке.", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            BaseNodeOperationEditingHelper.RemoveAt(_workingNode.Operations, selectedIndex);
            RefreshOperationsPreview(Math.Min(selectedIndex, _workingNode.Operations.Count - 1));
        }

        private static BaseNodeDefinition CloneNode(BaseNodeDefinition source)
        {
            return new BaseNodeDefinition
            {
                BaseNodeId = source.BaseNodeId,
                Id = source.Id,
                NodeCode = source.NodeCode,
                Name = source.Name,
                NodeGroup = source.NodeGroup,
                NodeGroupDetail = source.NodeGroupDetail,
                NodeGroupId = source.NodeGroupId,
                NodeType = source.NodeType,
                NodeTypeId = source.NodeTypeId,
                NodeSubgroupId = source.NodeSubgroupId,
                ProductKind = source.ProductKind,
                ProductCategory = source.ProductCategory,
                SourceAnnId = source.SourceAnnId,
                SourceArticul = source.SourceArticul,
                SourceRtCode = source.SourceRtCode,
                SourceImagePath = source.SourceImagePath,
                Description = source.Description,
                CreatedAtUtc = source.CreatedAtUtc,
                UpdatedAtUtc = source.UpdatedAtUtc,
                Operations = source.Operations.Select(CloneOperation).ToList()
            };
        }

        private static BaseNodeOperationDefinition CloneOperation(BaseNodeOperationDefinition source)
        {
            return new BaseNodeOperationDefinition
            {
                BaseNodeOperationId = source.BaseNodeOperationId,
                OperationRefId = source.OperationRefId,
                SortOrder = source.SortOrder,
                SourceN = source.SourceN,
                SourceN1 = source.SourceN1,
                Kod = source.Kod,
                KodO = source.KodO,
                Text = source.Text,
                Razryd = source.Razryd,
                Sek = source.Sek,
                Seb = source.Seb,
                Obor = source.Obor,
                Spec = source.Spec,
                KodProizv = source.KodProizv,
                KodPodr = source.KodPodr,
                KodOb = source.KodOb,
                TextOb = source.TextOb,
                TextVyaz = source.TextVyaz,
                TextProizv = source.TextProizv
            };
        }
    }
}

