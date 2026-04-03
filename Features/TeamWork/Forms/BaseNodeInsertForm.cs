using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Features.TeamWork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    internal sealed partial class BaseNodeInsertForm : Form
    {
        private readonly BaseNodeLibraryService _libraryService;
        private readonly List<BaseNodeDefinition> _nodes;

        public BaseNodeDefinition SelectedNode => nodesListBox.SelectedItem as BaseNodeDefinition;
        public BaseNodeInsertionPoint SelectedInsertionPoint => positionComboBox.SelectedItem as BaseNodeInsertionPoint;

        public BaseNodeInsertForm(
            IReadOnlyList<BaseNodeDefinition> nodes,
            IReadOnlyList<BaseNodeInsertionPoint> insertionPoints,
            int? defaultAfterN = null,
            BaseNodeLibraryService libraryService = null)
        {
            _libraryService = libraryService;
            _nodes = (nodes ?? Array.Empty<BaseNodeDefinition>()).ToList();

            InitializeComponent();

            nodesListBox.DisplayMember = nameof(BaseNodeDefinition.Name);
            nodesListBox.SelectedIndexChanged += (_, __) => RefreshPreview();
            editNodesButton.Enabled = _libraryService != null;

            foreach (var point in insertionPoints ?? Array.Empty<BaseNodeInsertionPoint>())
            {
                positionComboBox.Items.Add(point);
            }

            BindData(defaultAfterN);
        }

        private void BindData(int? defaultAfterN, int? preferredNodeId = null)
        {
            nodesListBox.Items.Clear();
            foreach (var node in _nodes.OrderBy(x => x.Name, StringComparer.CurrentCultureIgnoreCase))
            {
                nodesListBox.Items.Add(node);
            }

            if (nodesListBox.Items.Count > 0)
            {
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
                return;
            }

            var previewRows = BaseNodeMapper.CreatePreviewRows(node);
            previewGrid.DataSource = previewRows;

            int chapters = node.Operations.Select(x => x.SourceN).Distinct().Count();
            string description = string.IsNullOrWhiteSpace(node.Description) ? "Без описания" : node.Description.Trim();
            detailsLabel.Text = $"Операций: {node.Operations.Count}. Глав: {chapters}. {description}";
        }

        private async void EditNodesButton_Click(object sender, EventArgs e)
        {
            if (_libraryService == null)
            {
                return;
            }

            int? selectedNodeId = SelectedNode?.BaseNodeId;

            using var form = new BaseNodeLibraryEditorForm(_libraryService, selectedNodeId);
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
