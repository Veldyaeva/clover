using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Features.TeamWork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    internal sealed partial class BaseNodeLibraryEditorForm : Form
    {
        private readonly BaseNodeLibraryService _libraryService;
        private readonly List<BaseNodeDefinition> _nodes = new List<BaseNodeDefinition>();
        private readonly int? _preferredNodeId;

        public BaseNodeDefinition SelectedNode => nodesListBox.SelectedItem as BaseNodeDefinition;
        public int? SelectedBaseNodeId => SelectedNode?.BaseNodeId;

        public BaseNodeLibraryEditorForm(BaseNodeLibraryService libraryService, int? preferredNodeId = null)
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
            await ReloadNodesAsync(_preferredNodeId);
        }

        private void InitializeSelectors()
        {
            nodeGroupComboBox.Items.AddRange(BaseNodeMetadataOptions.NodeGroups);
            productKindComboBox.Items.AddRange(BaseNodeMetadataOptions.ProductKinds);
            productCategoryComboBox.Items.AddRange(BaseNodeMetadataOptions.ProductCategories);
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
                nodeCodeValueLabel.Text = "-";
                nameTextBox.Text = string.Empty;
                descriptionTextBox.Text = string.Empty;
                nodeGroupComboBox.SelectedIndex = 0;
                productKindComboBox.SelectedItem = "Универсальный";
                productCategoryComboBox.SelectedItem = "Универсально";
                detailsLabel.Text = "Выберите базовый узел для редактирования.";
                previewGrid.DataSource = null;
                return;
            }

            nodeCodeValueLabel.Text = string.IsNullOrWhiteSpace(node.NodeCode) ? "-" : node.NodeCode;
            nameTextBox.Text = node.Name ?? string.Empty;
            descriptionTextBox.Text = node.Description ?? string.Empty;
            SelectComboValue(nodeGroupComboBox, node.NodeGroup, string.Empty);
            SelectComboValue(productKindComboBox, node.ProductKind, "Универсальный");
            SelectComboValue(productCategoryComboBox, node.ProductCategory, "Универсально");

            int chapters = node.Operations.Select(x => x.SourceN).Distinct().Count();
            detailsLabel.Text = $"Операций: {node.Operations.Count}. Глав: {chapters}.";
            previewGrid.DataSource = BaseNodeMapper.CreatePreviewRows(node);
        }

        private static void SelectComboValue(ComboBox comboBox, string value, string fallback)
        {
            string targetValue = string.IsNullOrWhiteSpace(value) ? fallback : value.Trim();
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
            if (node == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                MessageBox.Show(this, "Укажите название базового узла.", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextBox.Focus();
                return;
            }

            var updatedNode = CloneNode(node);
            updatedNode.Name = nameTextBox.Text.Trim();
            updatedNode.Description = descriptionTextBox.Text.Trim();
            updatedNode.NodeGroup = nodeGroupComboBox.SelectedItem?.ToString() ?? string.Empty;
            updatedNode.ProductKind = productKindComboBox.SelectedItem?.ToString() ?? string.Empty;
            updatedNode.ProductCategory = productCategoryComboBox.SelectedItem?.ToString() ?? string.Empty;

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
