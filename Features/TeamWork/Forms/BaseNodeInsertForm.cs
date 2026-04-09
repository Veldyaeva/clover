using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Features.TeamWork.Services;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    internal sealed partial class BaseNodeInsertForm : CustomForm
    {
        private readonly BaseNodeLibraryService _libraryService;
        private readonly List<BaseNodeDefinition> _nodes;
        private readonly BaseNodePreviewPanel _previewPanel;
        private readonly ComboBox _productKindFilterComboBox = new ComboBox();
        private readonly ComboBox _productCategoryFilterComboBox = new ComboBox();
        private readonly ComboBox _nodeGroupFilterComboBox = new ComboBox();

        public BaseNodeDefinition SelectedNode => nodesListBox.SelectedItem as BaseNodeDefinition;
        public BaseNodeInsertionPoint SelectedInsertionPoint => positionComboBox.SelectedItem as BaseNodeInsertionPoint;

        public BaseNodeInsertForm( UserClass User,
            IReadOnlyList<BaseNodeDefinition> nodes,
            IReadOnlyList<BaseNodeInsertionPoint> insertionPoints,
            int? defaultAfterN = null,
            BaseNodeLibraryService libraryService = null): base(User)
        {
            _libraryService = libraryService;
            _nodes = (nodes ?? Array.Empty<BaseNodeDefinition>()).ToList();

            InitializeComponent();
            _previewPanel = BaseNodePreviewHelper.Create(previewPanel, previewSourceLabel, previewImageStatusLabel, previewPictureBox);
            editNodesButton.Enabled = _libraryService != null;
            searchLabel.Text = "Поиск по названию";
            searchTextBox.PlaceholderText = "Поиск по названию";
            InitializeFiltersUi();

            foreach (var point in insertionPoints ?? Array.Empty<BaseNodeInsertionPoint>())
            {
                positionComboBox.Items.Add(point);
            }

            BindData(defaultAfterN);
        }

        private void BindData(int? defaultAfterN, int? preferredNodeId = null)
        {
            ApplyNodeFilter(preferredNodeId);

            if (positionComboBox.Items.Count > 0 && positionComboBox.SelectedIndex < 0)
            {
                int preferredIndex = 0;
                if (defaultAfterN.HasValue)
                {
                    for (int i = 0; i < positionComboBox.Items.Count; i++)
                    {
                        if (positionComboBox.Items[i] is BaseNodeInsertionPoint point && point.AfterN == defaultAfterN)
                        {
                            preferredIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    var endIndex = positionComboBox.Items
                        .Cast<BaseNodeInsertionPoint>()
                        .Select((point, index) => new { point, index })
                        .FirstOrDefault(x => x.point.AppendToEnd);

                    if (endIndex != null)
                    {
                        preferredIndex = endIndex.index;
                    }
                }

                positionComboBox.SelectedIndex = preferredIndex;
            }

            RefreshPreview();
        }

        private void RefreshPreview()
        {
            var node = SelectedNode;
            if (node == null)
            {
                previewGrid.DataSource = null;
                detailsLabel.Text = "Выберите базовый узел.";
                BaseNodePreviewHelper.Update(_previewPanel, (BaseNodeDefinition)null);
                return;
            }

            previewGrid.DataSource = BaseNodeMapper.CreatePreviewRows(node);
            BaseNodePreviewHelper.Update(_previewPanel, node);
            UpdateRtCodeCopyState(node.SourceRtCode);

            int chapters = node.Operations.Select(x => x.SourceN).Distinct().Count();
            string description = string.IsNullOrWhiteSpace(node.Description) ? "Без описания" : StringNormalizer.TrimOrEmpty(node.Description);
            string sourceCode = string.IsNullOrWhiteSpace(node.SourceRtCode) ? "-" : node.SourceRtCode;
            detailsLabel.Text = $"РТ: {sourceCode}. Операций: {chapters}. Подопераций: {node.Operations.Count}. {description}";
        }

        private void NodesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            ApplyNodeFilter(SelectedNode?.BaseNodeId);
        }

        private void FilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyNodeFilter(SelectedNode?.BaseNodeId);
        }

        private void PreviewSourceLabel_Click(object sender, EventArgs e)
        {
            CopyRtCodeToClipboard();
        }

        private void CopyRtCodeToClipboard()
        {
            string sourceCode = StringNormalizer.TrimOrEmpty(SelectedNode?.SourceRtCode);
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
            string tooltip = string.IsNullOrWhiteSpace(sourceRtCode)
                ? "RT-код не задан"
                : $"RT-код: {sourceRtCode}. Кликните, чтобы скопировать";
            previewToolTip.SetToolTip(previewSourceLabel, tooltip);
        }

        private void ApplyNodeFilter(int? preferredNodeId)
        {
            string filter = StringNormalizer.TrimOrEmpty(searchTextBox?.Text);
            PopulateFilterValues();

            string productKind = StringNormalizer.TrimOrEmpty(_productKindFilterComboBox.SelectedItem?.ToString());
            string productCategory = StringNormalizer.TrimOrEmpty(_productCategoryFilterComboBox.SelectedItem?.ToString());
            string nodeGroup = StringNormalizer.TrimOrEmpty(_nodeGroupFilterComboBox.SelectedItem?.ToString());

            var filtered = _nodes
                .Where(node => MatchesNodeFilter(node, filter, productKind, productCategory, nodeGroup))
                .OrderBy(node => node.DisplayName, StringComparer.CurrentCultureIgnoreCase)
                .ToList();

            nodesListBox.BeginUpdate();
            try
            {
                nodesListBox.Items.Clear();
                foreach (var node in filtered)
                    nodesListBox.Items.Add(node);
            }
            finally
            {
                nodesListBox.EndUpdate();
            }

            if (nodesListBox.Items.Count == 0)
            {
                nodesListBox.SelectedIndex = -1;
                RefreshPreview();
                return;
            }

            int selectedIndex = 0;
            if (preferredNodeId.HasValue)
            {
                for (int i = 0; i < nodesListBox.Items.Count; i++)
                {
                    if (nodesListBox.Items[i] is BaseNodeDefinition node && node.BaseNodeId == preferredNodeId.Value)
                    {
                        selectedIndex = i;
                        break;
                    }
                }
            }

            nodesListBox.SelectedIndex = selectedIndex;
        }

        private void InitializeFiltersUi()
        {
            ConfigureFilterComboBox(_productKindFilterComboBox);
            ConfigureFilterComboBox(_productCategoryFilterComboBox);
            ConfigureFilterComboBox(_nodeGroupFilterComboBox);

            _productKindFilterComboBox.SelectedIndexChanged += FilterComboBox_SelectedIndexChanged;
            _productCategoryFilterComboBox.SelectedIndexChanged += FilterComboBox_SelectedIndexChanged;
            _nodeGroupFilterComboBox.SelectedIndexChanged += FilterComboBox_SelectedIndexChanged;

            var filtersPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                ColumnCount = 1,
                Margin = new Padding(0, 6, 10, 0)
            };
            filtersPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            filtersPanel.RowStyles.Add(new RowStyle());
            filtersPanel.RowStyles.Add(new RowStyle());
            filtersPanel.RowStyles.Add(new RowStyle());
            filtersPanel.RowStyles.Add(new RowStyle());
            filtersPanel.RowStyles.Add(new RowStyle());
            filtersPanel.RowStyles.Add(new RowStyle());

            filtersPanel.Controls.Add(CreateFilterLabel("Класс изделия (ProductKind)"), 0, 0);
            filtersPanel.Controls.Add(_productKindFilterComboBox, 0, 1);
            filtersPanel.Controls.Add(CreateFilterLabel("Категория изделия (ProductCategory)"), 0, 2);
            filtersPanel.Controls.Add(_productCategoryFilterComboBox, 0, 3);
            filtersPanel.Controls.Add(CreateFilterLabel("Группа узла (NodeGroup)"), 0, 4);
            filtersPanel.Controls.Add(_nodeGroupFilterComboBox, 0, 5);

            leftLayoutPanel.RowCount += 1;
            leftLayoutPanel.RowStyles.Insert(3, new RowStyle());
            leftLayoutPanel.Controls.Add(filtersPanel, 0, 3);
            leftLayoutPanel.SetRow(nodesListBox, 4);
        }

        private void PopulateFilterValues()
        {
            PopulateFilterComboBox(_productKindFilterComboBox, _nodes.Select(node => node.ProductKind));
            PopulateFilterComboBox(_productCategoryFilterComboBox, _nodes.Select(node => node.ProductCategory));
            PopulateFilterComboBox(_nodeGroupFilterComboBox, _nodes.Select(node => node.NodeGroup));
        }

        private static void ConfigureFilterComboBox(ComboBox comboBox)
        {
            comboBox.Dock = DockStyle.Top;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private static Label CreateFilterLabel(string text)
        {
            return new Label
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                Text = text
            };
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

        private static bool MatchesNodeFilter(
            BaseNodeDefinition node,
            string filter,
            string productKind,
            string productCategory,
            string nodeGroup)
        {
            if (node == null)
                return false;

            if (!string.IsNullOrWhiteSpace(productKind) &&
                !string.Equals(StringNormalizer.TrimOrEmpty(node.ProductKind), productKind, StringComparison.CurrentCultureIgnoreCase))
                return false;

            if (!string.IsNullOrWhiteSpace(productCategory) &&
                !string.Equals(StringNormalizer.TrimOrEmpty(node.ProductCategory), productCategory, StringComparison.CurrentCultureIgnoreCase))
                return false;

            if (!string.IsNullOrWhiteSpace(nodeGroup) &&
                !string.Equals(StringNormalizer.TrimOrEmpty(node.NodeGroup), nodeGroup, StringComparison.CurrentCultureIgnoreCase))
                return false;

            if (string.IsNullOrWhiteSpace(filter))
                return true;

            return StringNormalizer.TrimOrEmpty(node.Name)
                .Contains(filter, StringComparison.CurrentCultureIgnoreCase);
        }

        private async void EditNodesButton_Click(object sender, EventArgs e)
        {
            if (_libraryService == null)
            {
                return;
            }

            int? selectedNodeId = SelectedNode?.BaseNodeId;

            using var form = new BaseNodeLibraryEditorForm(User, _libraryService, selectedNodeId);
            form.ShowDialog(this);
            selectedNodeId = form.SelectedBaseNodeId ?? selectedNodeId;

            try
            {
                var nodes = await _libraryService.GetAllAsync();
                _nodes.Clear();
                _nodes.AddRange(nodes);
                BindData(null, selectedNodeId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Не удалось обновить библиотеку узлов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
