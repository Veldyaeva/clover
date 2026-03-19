using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit.Layout;
using SewingProduction.Features.Tabel.Models;
using SewingProduction.Features.Tabel.Services;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.Tabel.Forms
{
    public partial class CalculatorDay : CustomForm
    {
        public string CalculatorResult { get; private set; }
        public string ddResult { get ; private set; }
        public string dopResult { get ; private set; }
        private readonly int _grId;
        private readonly string _dd;
        private readonly int _dl_d;
        private readonly string _dLetters;
        private readonly string _dNumber;
        private static DatabaseHelper _dbHelper;
        private static DbService _dbService;
        private readonly ILogger _logger = new FileLogger();
        private static TabelDataService _tabelDataService;
        private List<WorkTypes> _WorkTypesList = new List<WorkTypes>();
        public CalculatorDay(int idGr, string dd, string d,string dop, int dl_d)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Deactivate += (s, e) => this.Close();
            this.BackColor = SystemColors.Menu;
            this.Padding = new Padding(1);
            this.Size = new Size(220, 150);
            _grId = idGr;
            _dd = dd;
            _dl_d = dl_d;
            _dLetters = ExtractLettersRegex(d);
            _dNumber = ExtractNumber(d);
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);
            _tabelDataService = new TabelDataService(_dbHelper);
            _WorkTypesList = new List<WorkTypes>();
        }
        private async Task GetWorkTypes(int idGr)
        {
            _WorkTypesList.Clear();
            _WorkTypesList = await _tabelDataService.GetWorkTypesAsync(idGr);
            _WorkTypesList.RemoveAll(p => p.nameWorkTypes.Trim() == "1");

        }
        private void FillCheckedList(List<WorkTypes> workTypes)
        {
            workTypesCheckedListBox.BeginUpdate();
            try
            {
                workTypesCheckedListBox.Items.Clear();
                foreach (var item in workTypes)
                {
                    var displayText = $"{item.nameWorkTypes} -  {item.description} ";
                    var listItem = new DevExpress.XtraEditors.Controls.CheckedListBoxItem(
                            item,
                            displayText,
                            CheckState.Unchecked);
                    workTypesCheckedListBox.Items.Add(listItem);
                }
            }
            finally
            {
                workTypesCheckedListBox.EndUpdate();
            }

        }

        private async void CalculatorDay_Load(object sender, EventArgs e)
        {
            await Task.WhenAll(GetWorkTypes(_grId));
            if (_grId == 20)
            {
                layoutControlItem1.ContentVisible = false;
                layoutControlItem2.ContentVisible = false;
                layoutControlItem22.ContentVisible = false;
            }
            FillCheckedList(_WorkTypesList);
            customTextBoxDd.Text = _dd;
            SetCheckedSaveItemsText(_dLetters);
            SetNumberSave(_dNumber);
            SetMarkDay(_dd);
        }

        private void customSimpleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }
        private void HandleDigitInputForInteger(char digit)
        {

            string currentText = customTextBoxValueDigit?.Text.ToString() ?? "";

            // Очищаем, если текущее значение не число
            //if (!decimal.TryParse(currentText, out _))
            //{
            //    currentText = "";
            //}

            // Формируем новое значение
            string newText;
            if (currentText.Length == 0)
            {
                newText = digit.ToString();
            }
            else if (currentText.Length <= 3)
            {
                // Проверяем, что число не больше 23
                string testValue = currentText + digit;
                if (decimal.TryParse(testValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal testInt) && testInt <= 23)
                {
                    newText = testValue;
                }
                else
                {
                    // Если больше 23, заменяем первую цифру
                    newText = digit.ToString();
                }
            }
            else
            {
                // Если уже 2 цифры, заменяем значение
                newText = digit.ToString();
            }

            // Проверяем, что число <= 23
            if (decimal.TryParse(newText, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal value) && value <= 23)
            {
                customTextBoxValueDigit.Text = newText;
            }
        }
        public string ExtractCode(string description)
        {
            if (string.IsNullOrEmpty(description))
                return string.Empty;

            // Разделяем строку по дефису
            string[] parts = description.Split('-');

            if (parts.Length > 0)
            {
                // Берем первую часть и удаляем пробелы
                return parts[0].Trim();
            }

            return string.Empty;
        }
        public string GetCheckedItemsText()
        {
            StringBuilder ItemsText = new StringBuilder();
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in workTypesCheckedListBox.Items)
            {
                if (item.CheckState == CheckState.Checked)
                {

                    ItemsText.Append(ExtractCode(item.Description));
                }
            }
            return ItemsText.ToString();
        }
        public void SetCheckedSaveItemsText(string d)
        {
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in workTypesCheckedListBox.Items)
            {
                bool contains = d.Equals(ExtractCode(item.Description));
                if (contains)
                {
                    item.CheckState = CheckState.Checked;
                }
            }
        }
        public void SetNumberSave(string d)
        {
            customTextBoxValueDigit.Text = d;
        }


        private void workTypesCheckedListBox_ItemChecking(object sender, DevExpress.XtraEditors.Controls.ItemCheckingEventArgs e)
        {

        }

        private void workTypesCheckedListBox_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            customTextBoxString.Text = GetCheckedItemsText();

            customTextBoxString.Refresh();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            HandleDigitInputForInteger('1');
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            HandleDigitInputForInteger('2');
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            HandleDigitInputForInteger('3');
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            HandleDigitInputForInteger('4');
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            HandleDigitInputForInteger('5');
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            HandleDigitInputForInteger('6');
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            HandleDigitInputForInteger('7');
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            HandleDigitInputForInteger('8');
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            HandleDigitInputForInteger('9');
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            HandleDigitInputForInteger('0');
        }

        private void customTextBoxValueDigit_TextChanged(object sender, EventArgs e)
        {
            customTextBoxValue.Text = customTextBoxValueDigit.Text + customTextBoxString.Text;
        }

        private void customTextBoxString_TextChanged(object sender, EventArgs e)
        {
            customTextBoxValue.Text = customTextBoxValueDigit.Text + customTextBoxString.Text;
        }
        public static string ExtractLettersRegex(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            Match match = Regex.Match(input, @"([А-ЯЁA-Z/]+)");

            return match.Success ? match.Value : string.Empty;
        }
        public static string ExtractNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            Match match = Regex.Match(input, @"^([\d]+([\.,]\d+)?)");

            if (match.Success)
            {
                string numberStr = match.Value.Replace(',', '.');
                return numberStr;

            }

            return null;
        }

        private void btnPoint_Click(object sender, EventArgs e)
        {
            customTextBoxValueDigit.Text = customTextBoxValueDigit.Text + '.';
        }
        private void SetMarkDay(string MarkDay)
        {
            string[] workingValue = { "1", "11", "8" };
            bool IsWorkingDay = workingValue.Any(v => MarkDay.ToString().Contains(v));
            bool IsHoliday = MarkDay.ToString().Contains("/3");
            bool IsOrderBasedLeave = MarkDay.ToString().Contains("/4");
            if (IsWorkingDay)
            {
                customRadioGroup1.SelectedIndex = 0;
              //  customRadioGroup1.Properties.Items[1].Value = true;
            }
            else
            {
                customRadioGroup1.SelectedIndex = 1;
              //  customRadioGroup1.Properties.Items[0].Value = true;
            }
            if (IsOrderBasedLeave)
            {
                customRadioGroup2.SelectedIndex = 1;
                //customRadioGroup2.Properties.Items[0].Value = true;
            }
            if (IsHoliday)
            {
                customRadioGroup2.SelectedIndex = 0;
               // customRadioGroup2.Properties.Items[1].Value = true;
            }
            if (!IsOrderBasedLeave && !IsHoliday)
            {
                customRadioGroup2.SelectedIndex = 2;
               // customRadioGroup2.Properties.Items[0].Value = false;
               // customRadioGroup2.Properties.Items[1].Value = false;
            }
            
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            customTextBoxValueDigit.Text = "";

        }

        private void customSimpleButton2_Click(object sender, EventArgs e)
        {
            this.CalculatorResult = customTextBoxValue.Text;
            this.ddResult = customTextBoxDd.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void customTextBoxDd_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetValueDd();
        }
        public void GetValueDd()
        {
            StringBuilder valueDd = new StringBuilder();
            if (customRadioGroup1.SelectedIndex == 0)
            {
                if (_dl_d == 8)
                {
                    valueDd.Append("1");
                }
                else
                {
                    valueDd.Append("11");
                }
                    
            }
            else if(customRadioGroup1.SelectedIndex == 1)
            {
                valueDd.Append("2");
            }
            if (customRadioGroup2.SelectedIndex == 0)
            {
                valueDd.Append("/3");
            }
            else if (customRadioGroup2.SelectedIndex == 1)
            {
                valueDd.Append("/4");
            }
            customTextBoxDd.Text = valueDd.ToString().Trim();
            customTextBoxDd.Refresh();
        }
    }
}
