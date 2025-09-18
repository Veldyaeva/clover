using SewingProduction.Core.Class.Settings;
using SewingProduction.Features.UserDistribution.Helpers;
using System;
using System.Windows.Forms;
using static SewingProduction.form.SettingsForm;

namespace SewingProduction.form
{
    public partial class SettingsForm : CustomForm, IDataUpdatableForm
    {
        public SettingsForm(UserClass user) : base(user)
        {
            InitializeComponent();
        }
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            InitDatabaseSelector();
            // Тема
            var currentTheme = SettingsManager.Current.Theme;
            customComboBoxTheme.Items.Clear();
            customComboBoxTheme.Items.AddRange(ThemeManager.GetAvailableThemes().ToArray());
            customComboBoxTheme.SelectedItem = currentTheme;

            // Размер текста
            customComboBoxSizeText.Items.Clear();
            customComboBoxSizeText.Items.AddRange(new object[] { 8, 9, 10, 11, 12, 14, 16 });
            customComboBoxSizeText.SelectedItem = SettingsManager.Current.FontSize;

            // Сохранение вкладок
            customCheckBoxSaveOpenTabs.Checked = SettingsManager.GetSaveOpenTabs();
            // Сокращенное Название Вкладок
            customCheckBoxSokrNameTabs.Checked = SettingsManager.GetShortTabNames();
            customCheckBoxPovtOpenTabs.Checked = SettingsManager.GetAllowDuplicateTabs();
        }

        public interface IDataUpdatableForm
        {
            void UpdateDataInForm();
        }
        public void UpdateDataInForm()
        {
            SettingsForm_Load(null, EventArgs.Empty);
        }

        private void customComboBoxTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTheme = customComboBoxTheme.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(selectedTheme))
                return;

            ThemeManager.SetTheme(selectedTheme);
            SettingsManager.SetTheme(selectedTheme);
        }

        private void customComboBoxSizeText_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(customComboBoxSizeText.SelectedItem?.ToString(), out int fontSize))
            {
                SettingsManager.SetFontSize(fontSize);
            }
        }

        private void customCheckBoxSaveOpenTabs_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.SetSaveOpenTabs(customCheckBoxSaveOpenTabs.Checked);
        }
        private void customCheckBoxSokrNameVklad_CheckedChanged(object sender, EventArgs e)
        {
            bool checkedValue = customCheckBoxSokrNameTabs.Checked;
            SettingsManager.SetShortTabNames(checkedValue);
        }

        private void customCheckBoxPovtOpenTabs_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.SetAllowDuplicateTabs(customCheckBoxPovtOpenTabs.Checked);
        }
        private void customButtonClearProfile_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Вы действительно хотите очистить историю профилей?",
                                           "Подтверждение",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                SettingsManager.ClearLoginAndPasswordHistory();
                MessageBox.Show("История профилей очищена", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void InitDatabaseSelector()
        {
            customComboBoxRejimRab.Items.AddRange(new string[]
            {
                "ace",
                "ace_test",
                "ace_backup",
                "ace_backup_new",
                "global",
                "oms"
            });

            var savedDb = SettingsManager.GetSelectedDatabase();
            if (customComboBoxRejimRab.Items.Contains(savedDb))
                customComboBoxRejimRab.SelectedItem = savedDb;
            else
                customComboBoxRejimRab.SelectedIndex = 0;
        }

        private void customButtonSaveExit_Click(object sender, EventArgs e)
        {
            var selectedDb = customComboBoxRejimRab.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedDb))
            {
                SettingsManager.SaveSelectedDatabase(_user.UserName, selectedDb);
                MessageBox.Show("Настройки сохранены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }
    }
}
