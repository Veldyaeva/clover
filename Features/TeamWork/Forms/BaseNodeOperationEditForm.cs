using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Models;
using System;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    internal sealed partial class BaseNodeOperationEditForm : CustomForm
    {
        private readonly BaseNodeOperationDefinition _sourceOperation;

        public BaseNodeOperationDefinition ResultOperation { get; private set; }

        public BaseNodeOperationEditForm(UserClass user, BaseNodeOperationDefinition operation) : base(user)
        {
            _sourceOperation = operation ?? throw new ArgumentNullException(nameof(operation));
            InitializeComponent();
            BindOperation();
        }

        private void BindOperation()
        {
            chapterLabel.Text = _sourceOperation.SourceN1 > 0
                ? $"{_sourceOperation.SourceN}.{_sourceOperation.SourceN1}"
                : _sourceOperation.SourceN.ToString();
            operationCodeTextBox.Text = _sourceOperation.KodO ?? string.Empty;
            operationNameTextBox.Text = _sourceOperation.Text ?? string.Empty;
            equipmentTextBox.Text = _sourceOperation.Obor ?? string.Empty;
            specTextBox.Text = _sourceOperation.Spec ?? string.Empty;
            secondsNumericUpDown.Value = NormalizeDecimal(_sourceOperation.Sek, secondsNumericUpDown.Maximum);
            razrydNumericUpDown.Value = NormalizeDecimal(_sourceOperation.Razryd, razrydNumericUpDown.Maximum);
            kodObNumericUpDown.Value = NormalizeDecimal(_sourceOperation.KodOb, kodObNumericUpDown.Maximum);
            kodProizvNumericUpDown.Value = NormalizeDecimal(_sourceOperation.KodProizv, kodProizvNumericUpDown.Maximum);
            kodPodrNumericUpDown.Value = NormalizeDecimal(_sourceOperation.KodPodr, kodPodrNumericUpDown.Maximum);
        }

        private static decimal NormalizeDecimal(int value, decimal max)
        {
            if (value < 0)
            {
                return 0;
            }

            return Math.Min(value, (int)max);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(operationNameTextBox.Text))
            {
                MessageBox.Show(this, "Укажите название операции.", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                operationNameTextBox.Focus();
                return;
            }

            ResultOperation = new BaseNodeOperationDefinition
            {
                BaseNodeOperationId = _sourceOperation.BaseNodeOperationId,
                OperationRefId = _sourceOperation.OperationRefId,
                SortOrder = _sourceOperation.SortOrder,
                SourceN = _sourceOperation.SourceN,
                SourceN1 = _sourceOperation.SourceN1,
                Kod = _sourceOperation.Kod,
                KodO = operationCodeTextBox.Text?.Trim() ?? string.Empty,
                Text = operationNameTextBox.Text?.Trim() ?? string.Empty,
                Razryd = Decimal.ToInt32(razrydNumericUpDown.Value),
                Sek = Decimal.ToInt32(secondsNumericUpDown.Value),
                Seb = _sourceOperation.Seb,
                Obor = equipmentTextBox.Text?.Trim() ?? string.Empty,
                Spec = specTextBox.Text?.Trim() ?? string.Empty,
                KodProizv = Decimal.ToInt32(kodProizvNumericUpDown.Value),
                KodPodr = Decimal.ToInt32(kodPodrNumericUpDown.Value),
                KodOb = Decimal.ToInt32(kodObNumericUpDown.Value),
                TextOb = _sourceOperation.TextOb,
                TextVyaz = _sourceOperation.TextVyaz,
                TextProizv = _sourceOperation.TextProizv
            };

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
