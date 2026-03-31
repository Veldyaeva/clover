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

        public BaseNodeDefinition ResultNode { get; private set; }

        public BaseNodeSaveForm(IReadOnlyList<NormRasz> operations, string defaultName = null)
        {
            _operations = operations ?? Array.Empty<NormRasz>();

            InitializeComponent();

            summaryLabel.Text = BuildSummary();
            nameTextBox.Text = defaultName ?? string.Empty;
            previewGrid.DataSource = BaseNodeMapper.CreatePreviewRows(_operations.ToList());
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
            DialogResult = DialogResult.OK;
            Close();
        }

        private string BuildSummary()
        {
            int chapters = _operations.Select(x => x.N).Distinct().Count();
            return $"Будут сохранены операций: {_operations.Count}. Глав: {chapters}.";
        }
    }
}
