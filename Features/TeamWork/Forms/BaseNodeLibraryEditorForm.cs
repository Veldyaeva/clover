using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
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
    internal sealed partial class BaseNodeLibraryEditorForm : CustomForm
    {
        private readonly BaseNodeLibraryService _libraryService;
        private readonly List<BaseNodeDefinition> _nodes = new List<BaseNodeDefinition>();
        private readonly int? _preferredNodeId;
        private BaseNodeDefinition _workingNode;

        public BaseNodeDefinition SelectedNode => nodesListBox.SelectedItem as BaseNodeDefinition;
        public int? SelectedBaseNodeId => SelectedNode?.BaseNodeId;

        public BaseNodeLibraryEditorForm(UserClass User, BaseNodeLibraryService libraryService, int? preferredNodeId = null):base(User)
        {
            _libraryService = libraryService ?? throw new ArgumentNullException(nameof(libraryService));
            _preferredNodeId = preferredNodeId;

            InitializeComponent();
            InitializeSelectors();

            nodesListBox.DisplayMember = nameof(BaseNodeDefinition.Name);
            nodesListBox.SelectedIndexChanged += (_, __) => BindSelectedNode();
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
            productKindComboBox.Items.AddRange(BaseNodeMetadataOptions.ProductKinds);
            productCategoryComboBox.Items.AddRange(BaseNodeMetadataOptions.ProductCategories);
        }

        private async Task LoadNodeGroupsAsync()
        {
            try
            {
                string selectedValue = _workingNode?.NodeGroup ?? string.Empty;
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

        private async Task ReloadNodesAsync(int? preferredNodeId = null)
        {
            ToggleBusyState(true);
            try
            {
                var nodes = await _libraryService.GetAllAsync();
                _nodes.Clear();
                _nodes.AddRange(nodes.OrderBy(x => x.Name, StringComparer.CurrentCultureIgnoreCase));

                nodesListBox.BeginUpdate();
                try
                {
                    nodesListBox.Items.Clear();
                    foreach (var node in _nodes)
                    {
                        nodesListBox.Items.Add(node);
                    }
                }
                finally
                {
                    nodesListBox.EndUpdate();
                }

                if (nodesListBox.Items.Count == 0)
                {
                    nodesListBox.SelectedIndex = -1;
                    BindSelectedNode();
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
                BindSelectedNode();
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

            editorPanel.Enabled = hasNode;
            saveButton.Enabled = hasNode;
            deleteButton.Enabled = hasNode;

            if (!hasNode)
            {
                _workingNode = null;
                nodeCodeValueLabel.Text = "-";
                nameTextBox.Text = string.Empty;
                descriptionTextBox.Text = string.Empty;
                nodeGroupComboBox.SelectedIndex = nodeGroupComboBox.Items.Count > 0 ? 0 : -1;
                productKindComboBox.SelectedItem = "Универсальный";
                productCategoryComboBox.SelectedItem = "Универсально";
                detailsLabel.Text = "Выберите базовый узел для редактирования.";
                previewGrid.DataSource = null;
                UpdateOperationButtonsState();
                return;
            }

            _workingNode = CloneNode(node);

            nodeCodeValueLabel.Text = string.IsNullOrWhiteSpace(_workingNode.NodeCode) ? "-" : _workingNode.NodeCode;
            nameTextBox.Text = _workingNode.Name ?? string.Empty;
            descriptionTextBox.Text = _workingNode.Description ?? string.Empty;
            SelectComboValue(nodeGroupComboBox, _workingNode.NodeGroup, string.Empty);
            SelectComboValue(productKindComboBox, _workingNode.ProductKind, "Универсальный");
            SelectComboValue(productCategoryComboBox, _workingNode.ProductCategory, "Универсально");

            RefreshOperationsPreview();
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
            updatedNode.NodeGroup = StringNormalizer.TrimOrEmpty(nodeGroupComboBox.SelectedItem?.ToString());
            updatedNode.ProductKind = StringNormalizer.TrimOrEmpty(productKindComboBox.SelectedItem?.ToString());
            updatedNode.ProductCategory = StringNormalizer.TrimOrEmpty(productCategoryComboBox.SelectedItem?.ToString());

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
            nodesListBox.Enabled = !isBusy;
            editorPanel.Enabled = !isBusy && SelectedNode != null;
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
                : $"Операций: {_workingNode.Operations.Count}. Глав: {chapters}.";

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
                NodeType = source.NodeType,
                ProductKind = source.ProductKind,
                ProductCategory = source.ProductCategory,
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
