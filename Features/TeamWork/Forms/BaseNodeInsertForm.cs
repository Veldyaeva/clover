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
            var filtered = _nodes
                .Where(node => MatchesNodeFilter(node, filter))
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

        private static bool MatchesNodeFilter(BaseNodeDefinition node, string filter)
        {
            if (node == null)
                return false;
            if (string.IsNullOrWhiteSpace(filter))
                return true;

            string haystack = string.Join(" ", new[]
            {
                node.DisplayName,
                node.Name,
                node.NodeCode,
                node.SourceRtCode,
                node.NodeGroup,
                node.NodeType,
                node.ProductKind,
                node.ProductCategory,
                node.Description
            }).ToLowerInvariant();

            var tokens = filter.ToLowerInvariant()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return tokens.All(token => haystack.Contains(token));
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
