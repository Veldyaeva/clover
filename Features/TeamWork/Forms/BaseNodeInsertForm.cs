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
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    internal sealed partial class BaseNodeInsertForm : CustomForm
    {
        private readonly BaseNodeLibraryService _libraryService;
        private readonly List<BaseNodeDefinition> _nodes;
        private readonly BaseNodePreviewPanel _previewPanel;
        private bool _updatingFilters;

        public BaseNodeDefinition SelectedNode => nodeCardsListView.SelectedItems.Count > 0
            ? nodeCardsListView.SelectedItems[0].Tag as BaseNodeDefinition
            : null;
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
            searchTextBox.PlaceholderText = "Поиск";
            searchTextBoxitem.Text = "Поиск по названию";
            nodeTypeFilterComboBoxitem.Text = "Класс изделия";
            productCategoryFilterComboBoxitem.Text = "Категория изделия";
            nodeGroupFilterComboBoxitem.Text = "Группа узла";

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
                previewTitleLabel.Text = "Визуальная библиотека узлов";
                BaseNodePreviewHelper.Update(_previewPanel, (BaseNodeDefinition)null);
                return;
            }

            previewGrid.DataSource = BaseNodeMapper.CreatePreviewRows(node);
            BaseNodePreviewHelper.Update(_previewPanel, node);
            UpdateRtCodeCopyState(node.SourceRtCode);
            previewTitleLabel.Text = string.IsNullOrWhiteSpace(node.Name) ? node.DisplayName : node.Name;

            int chapters = node.Operations.Select(x => x.SourceN).Distinct().Count();
            string description = string.IsNullOrWhiteSpace(node.Description) ? "Без описания" : StringNormalizer.TrimOrEmpty(node.Description);
            string sourceCode = string.IsNullOrWhiteSpace(node.SourceRtCode) ? "-" : node.SourceRtCode;
            string sourceArticul = string.IsNullOrWhiteSpace(node.SourceArticul) ? "-" : node.SourceArticul;
            string productKind = string.IsNullOrWhiteSpace(GetProductKindValue(node)) ? "-" : GetProductKindValue(node);
            string productCategory = string.IsNullOrWhiteSpace(node.ProductCategory) ? "-" : node.ProductCategory;
            string nodeGroup = string.IsNullOrWhiteSpace(node.NodeGroup) ? "-" : node.NodeGroup;
            string nodeSubgroup = string.IsNullOrWhiteSpace(node.NodeGroupDetail) ? "-" : node.NodeGroupDetail;
            detailsLabel.Text =
                $"РТ: {sourceCode}{Environment.NewLine}" +
                $"Артикул: {sourceArticul}{Environment.NewLine}" +
                $"Тип узла: {productKind}{Environment.NewLine}" +
                $"Категория: {productCategory}{Environment.NewLine}" +
                $"Группа: {nodeGroup}{Environment.NewLine}" +
                $"Уточнение: {nodeSubgroup}{Environment.NewLine}" +
                $"Операций: {chapters}. Подопераций: {node.Operations.Count}.{Environment.NewLine}" +
                description;
        }

        private void NodeCardsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPreview();
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
            string sourceArticul = StringNormalizer.TrimOrEmpty(SelectedNode?.SourceArticul);
            string tooltip = string.IsNullOrWhiteSpace(sourceRtCode)
                ? (string.IsNullOrWhiteSpace(sourceArticul)
                    ? "Источник не задан"
                    : $"Артикул: {sourceArticul}")
                : string.IsNullOrWhiteSpace(sourceArticul)
                    ? $"RT-код: {sourceRtCode}. Кликните, чтобы скопировать"
                    : $"Артикул: {sourceArticul}. RT-код: {sourceRtCode}. Кликните, чтобы скопировать RT-код";
            previewToolTip.SetToolTip(previewSourceLabel, tooltip);
        }

        private void ApplyNodeFilter(int? preferredNodeId)
        {
            string filter = StringNormalizer.TrimOrEmpty(searchTextBox?.Text);
            PopulateFilterValues();

            string productKind = StringNormalizer.TrimOrEmpty(nodeTypeFilterComboBox.SelectedItem?.ToString());
            string productCategory = StringNormalizer.TrimOrEmpty(productCategoryFilterComboBox.SelectedItem?.ToString());
            string nodeGroup = StringNormalizer.TrimOrEmpty(nodeGroupFilterComboBox.SelectedItem?.ToString());

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
                RefreshPreview();
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
        }

        private void PopulateFilterValues()
        {
            _updatingFilters = true;
            try
            {
                PopulateFilterComboBox(nodeTypeFilterComboBox, _nodes.Where(node => node != null).Select(GetProductKindValue));
                PopulateFilterComboBox(productCategoryFilterComboBox, _nodes.Where(node => node != null).Select(node => node.ProductCategory));
                PopulateFilterComboBox(nodeGroupFilterComboBox, _nodes.Where(node => node != null).Select(node => node.NodeGroup));
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
                node.NodeGroupDetail,
                node.NodeType,
                node.ProductCategory,
                node.Description
            });

            return StringNormalizer.TrimOrEmpty(haystack)
                .Contains(filter, StringComparison.CurrentCultureIgnoreCase);
        }

        private static string GetProductKindValue(BaseNodeDefinition node)
        {
            return StringNormalizer.TrimOrEmpty(node?.ProductKind) switch
            {
                { Length: > 0 } value => value,
                _ => StringNormalizer.TrimOrEmpty(node?.NodeType)
            };
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
