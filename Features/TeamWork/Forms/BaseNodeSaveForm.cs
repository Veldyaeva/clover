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
        private readonly IReadOnlyList<NormRasz> _operations;
        private readonly BaseNodeSaveDefaults _defaults;

        public BaseNodeDefinition ResultNode { get; private set; }

        public BaseNodeSaveForm(IReadOnlyList<NormRasz> operations, string defaultName = null, BaseNodeSaveDefaults defaults = null)
        {
            _operations = operations ?? Array.Empty<NormRasz>();
            _defaults = defaults;

            InitializeComponent();
            InitializeSelectors();

            summaryLabel.Text = BuildSummary();
            nameTextBox.Text = defaultName ?? string.Empty;
            previewGrid.DataSource = BaseNodeMapper.CreatePreviewRows(_operations.ToList());
        }

        private void InitializeSelectors()
        {
            nodeGroupComboBox.Items.AddRange(BaseNodeMetadataOptions.NodeGroups);
            productKindComboBox.Items.AddRange(BaseNodeMetadataOptions.ProductKinds);
            productCategoryComboBox.Items.AddRange(BaseNodeMetadataOptions.ProductCategories);

            // Автоподстановка только подсказывает значения, пользователь может их изменить
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
            return $"Будут сохранены операций: {chapters}. Подопераций: {_operations.Count}.";
        }
    }
}
