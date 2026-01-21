using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using SewingProduction.Core.Class.Settings;
using SewingProduction.Features.UserDistribution.Helpers;
using static SewingProduction.form.SettingsForm;

namespace SewingProduction.form
{
    public partial class SettingsForm : CustomForm, IDataUpdatableForm
    {
        private bool _isInitializingColors;
        private string _currentThemeName;

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
            GridSettingsCheckBox.Checked = SettingsManager.GetSaveGridSettings();
            LoadColorPickersFromSettings();
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

            // Сохраняем изменения в текущей теме перед переключением
            if (!string.IsNullOrEmpty(_currentThemeName))
            {
                UpdateThemeColorFromPickers();
            }

            ThemeManager.SetTheme(selectedTheme);
            SettingsManager.SetTheme(selectedTheme);
            LoadColorPickersFromSettings();
        }

        private void UpdateThemeColorFromPickers()
        {
            if (string.IsNullOrEmpty(_currentThemeName))
                return;

            var themes = SettingsManager.GetThemes();
            if (!themes.TryGetValue(_currentThemeName, out var theme))
                return;

            if (colorPickEditLabel.EditValue is Color labelColor && labelColor != Color.Empty)
                theme.LabelTextColor = labelColor;

            if (colorPickEditTextBox.EditValue is Color textBoxColor && textBoxColor != Color.Empty)
                theme.TextBoxBackground = textBoxColor;

            if (colorPickEditButtonBackground.EditValue is Color buttonBgColor && buttonBgColor != Color.Empty)
                theme.ButtonBackground = buttonBgColor;

            if (colorPickEditButtonTextColor.EditValue is Color buttonTextColor && buttonTextColor != Color.Empty)
                theme.ButtonTextColor = buttonTextColor;

            if (colorPickEditLabelGridColor.EditValue is Color gridTextColor && gridTextColor != Color.Empty)
                theme.GridTextColor = gridTextColor;

            if (colorPickEdit3.EditValue is Color textBoxTextColor && textBoxTextColor != Color.Empty)
                theme.TextBoxText = textBoxTextColor;

            if (colorPickEditGridBackground.EditValue is Color gridBgColor && gridBgColor != Color.Empty)
                theme.GridBackground = gridBgColor;

            if (colorPickEditGridRowBackground.EditValue is Color gridRowBgColor && gridRowBgColor != Color.Empty)
                theme.GridRowBackground = gridRowBgColor;

            themes[_currentThemeName] = theme;
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
        private void GridSettingsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.SetSaveGridSettings(GridSettingsCheckBox.Checked);
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
            }

            // Сохраняем изменения в текущей теме перед сохранением
            if (!string.IsNullOrEmpty(_currentThemeName))
            {
                UpdateThemeColorFromPickers();
            }

            // Сохраняем обновлённые темы
            var themes = SettingsManager.GetThemes();
            SettingsManager.SetThemes(themes);

            MessageBox.Show("Настройки сохранены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void LoadColorPickersFromSettings()
        {
            _isInitializingColors = true;

            _currentThemeName = SettingsManager.Current.Theme;
            var themes = SettingsManager.GetThemes();

            if (themes.TryGetValue(_currentThemeName, out var currentTheme))
            {
                colorPickEditLabel.EditValue = currentTheme.LabelTextColor;
                colorPickEditTextBox.EditValue = currentTheme.TextBoxBackground;
                colorPickEditButtonBackground.EditValue = currentTheme.ButtonBackground;
                colorPickEditButtonTextColor.EditValue = currentTheme.ButtonTextColor;
                colorPickEditLabelGridColor.EditValue = currentTheme.GridTextColor;
                colorPickEdit3.EditValue = currentTheme.TextBoxText;
                colorPickEditGridBackground.EditValue = currentTheme.GridBackground;
                colorPickEditGridRowBackground.EditValue = currentTheme.GridRowBackground;
            }
            else
            {
                var activeTheme = ThemeManager.ActiveTheme;
                colorPickEditLabel.EditValue = activeTheme?.LabelTextColor ?? Color.Black;
                colorPickEditTextBox.EditValue = activeTheme?.TextBoxBackground ?? Color.White;
                colorPickEditButtonBackground.EditValue = activeTheme?.ButtonBackground ?? Color.LightGray;
                colorPickEditButtonTextColor.EditValue = activeTheme?.ButtonTextColor ?? Color.Black;
                colorPickEditLabelGridColor.EditValue = activeTheme?.GridTextColor ?? Color.Black;
                colorPickEdit3.EditValue = activeTheme?.TextBoxText ?? Color.Black;
                colorPickEditGridBackground.EditValue = activeTheme?.GridBackground ?? Color.White;
                colorPickEditGridRowBackground.EditValue = activeTheme?.GridRowBackground ?? Color.White;
            }

            _isInitializingColors = false;
        }

        private void colorPickEditLabel_EditValueChanged(object sender, EventArgs e)
        {
            if (_isInitializingColors)
                return;

            if (colorPickEditLabel.EditValue is Color color && color != Color.Empty)
            {
                UpdateThemeColor(color, null);
            }
        }

        private void colorPickEditTextBox_EditValueChanged(object sender, EventArgs e)
        {
            if (_isInitializingColors)
                return;

            if (colorPickEditTextBox.EditValue is Color color && color != Color.Empty)
            {
                UpdateThemeColor(null, color);
            }
        }

        private void UpdateThemeColor(Color? labelColor, Color? textBoxColor)
        {
            if (string.IsNullOrEmpty(_currentThemeName))
                return;

            var themes = SettingsManager.GetThemes();
            if (!themes.TryGetValue(_currentThemeName, out var theme))
                return;

            if (labelColor.HasValue)
                theme.LabelTextColor = labelColor.Value;

            if (textBoxColor.HasValue)
                theme.TextBoxBackground = textBoxColor.Value;

            themes[_currentThemeName] = theme;

            // Применяем изменения к активной теме для предпросмотра
            if (ThemeManager.ActiveTheme != null)
            {
                if (labelColor.HasValue)
                    ThemeManager.ActiveTheme.LabelTextColor = labelColor.Value;
                if (textBoxColor.HasValue)
                    ThemeManager.ActiveTheme.TextBoxBackground = textBoxColor.Value;
                ThemeManager.ApplyUserColorOverrides(raiseEvent: true);
            }
        }

        private void colorPickEditButtonBackground_EditValueChanged(object sender, EventArgs e)
        {
            if (_isInitializingColors)
                return;

            if (colorPickEditButtonBackground.EditValue is Color color && color != Color.Empty)
            {
                UpdateThemeColorProperty("ButtonBackground", color);
            }
        }

        private void colorPickEditButtonTextColor_EditValueChanged(object sender, EventArgs e)
        {
            if (_isInitializingColors)
                return;

            if (colorPickEditButtonTextColor.EditValue is Color color && color != Color.Empty)
            {
                UpdateThemeColorProperty("ButtonTextColor", color);
            }
        }

        private void colorPickEditLabelTextColor_EditValueChanged(object sender, EventArgs e)
        {
            if (_isInitializingColors)
                return;

            if (colorPickEditLabelGridColor.EditValue is Color color && color != Color.Empty)
            {
                UpdateThemeColorProperty("LabelText", color);
            }
        }

        private void colorPickEdit3_EditValueChanged(object sender, EventArgs e)
        {
            if (_isInitializingColors)
                return;

            if (colorPickEdit3.EditValue is Color color && color != Color.Empty)
            {
                UpdateThemeColorProperty("TextBoxText", color);
            }
        }

        private void colorPickEditGridBackground_EditValueChanged(object sender, EventArgs e)
        {
            if (_isInitializingColors)
                return;

            if (colorPickEditGridBackground.EditValue is Color color && color != Color.Empty)
            {
                UpdateThemeColorProperty("GridBackground", color);
            }
        }

        private void colorPickEditGridRowBackground_EditValueChanged(object sender, EventArgs e)
        {
            if (_isInitializingColors)
                return;

            if (colorPickEditGridRowBackground.EditValue is Color color && color != Color.Empty)
            {
                UpdateThemeColorProperty("GridRowBackground", color);
            }
        }

        private void UpdateThemeColorProperty(string propertyName, Color color)
        {
            if (string.IsNullOrEmpty(_currentThemeName))
                return;

            var themes = SettingsManager.GetThemes();
            if (!themes.TryGetValue(_currentThemeName, out var theme))
                return;

            // Обновляем свойство через рефлексию для универсальности
            var property = typeof(ThemeManager.Theme).GetProperty(propertyName);
            if (property != null && property.CanWrite)
            {
                property.SetValue(theme, color);
                themes[_currentThemeName] = theme;

                // Применяем изменения к активной теме для предпросмотра
                if (ThemeManager.ActiveTheme != null)
                {
                    property.SetValue(ThemeManager.ActiveTheme, color);
                    ThemeManager.ApplyUserColorOverrides(raiseEvent: true);
                }
            }
        }

        private void menuItemReset_Click(object sender, EventArgs e)
        {
            if (contextMenuReset.SourceControl is DevExpress.XtraEditors.ColorPickEdit picker)
            {
                ResetPickerToDefault(picker);
            }
        }

        private void ResetPickerToDefault(DevExpress.XtraEditors.ColorPickEdit picker)
        {
            if (ThemeManager.ActiveTheme == null)
                return;

            var themes = SettingsManager.GetThemes();
            if (!themes.TryGetValue(_currentThemeName, out var themeFromSettings))
                return;

            // Определяем, какой это пикер, и берём дефолт из текущей темы (ActiveTheme)
            if (picker == colorPickEditLabel)
            {
                picker.EditValue = ThemeManager.ActiveTheme.LabelTextColor;
                UpdateThemeColor(ThemeManager.ActiveTheme.LabelTextColor, null);
            }
            else if (picker == colorPickEditTextBox)
            {
                picker.EditValue = ThemeManager.ActiveTheme.TextBoxBackground;
                UpdateThemeColor(null, ThemeManager.ActiveTheme.TextBoxBackground);
            }
            else if (picker == colorPickEditButtonBackground)
            {
                picker.EditValue = ThemeManager.ActiveTheme.ButtonBackground;
                UpdateThemeColorProperty("ButtonBackground", ThemeManager.ActiveTheme.ButtonBackground);
            }
            else if (picker == colorPickEditButtonTextColor)
            {
                picker.EditValue = ThemeManager.ActiveTheme.ButtonTextColor;
                UpdateThemeColorProperty("ButtonTextColor", ThemeManager.ActiveTheme.ButtonTextColor);
            }
            else if (picker == colorPickEditLabelGridColor)
            {
                picker.EditValue = ThemeManager.ActiveTheme.GridTextColor;
                UpdateThemeColorProperty("GridTextColor", ThemeManager.ActiveTheme.GridTextColor);
            }
            else if (picker == colorPickEdit3)
            {
                picker.EditValue = ThemeManager.ActiveTheme.TextBoxText;
                UpdateThemeColorProperty("TextBoxText", ThemeManager.ActiveTheme.TextBoxText);
            }
            else if (picker == colorPickEditGridBackground)
            {
                picker.EditValue = ThemeManager.ActiveTheme.GridBackground;
                UpdateThemeColorProperty("GridBackground", ThemeManager.ActiveTheme.GridBackground);
            }
            else if (picker == colorPickEditGridRowBackground)
            {
                picker.EditValue = ThemeManager.ActiveTheme.GridRowBackground;
                UpdateThemeColorProperty("GridRowBackground", ThemeManager.ActiveTheme.GridRowBackground);
            }
        }

        private void customButton1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Сбросить настройки темы к значениям по умолчанию?",
                "Сброс темы",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            // Сбрасываем темы в settings.json на кодовые дефолты
            var defaultThemes = ThemeManager.GetDefaultThemes();
            SettingsManager.SetThemes(defaultThemes);

            //// Возвращаем выбор темы к дефолтной
            //SettingsManager.SetTheme("Gray");
            //ThemeManager.SetTheme("Gray");

            // Обновляем UI-пикеры
            LoadColorPickersFromSettings();

            MessageBox.Show("Настройки темы сброшены.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void customButton2_Click(object sender, EventArgs e)
        {
            var dir = UserFilePaths.GridSettings;
            if (!Directory.Exists(dir))
            {
                MessageBox.Show("Сохраненных настроек таблиц не найдено.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var files = Directory.GetFiles(dir, "*.json", SearchOption.TopDirectoryOnly);
            var confirm = MessageBox.Show(
                $"Удалить сохраненные настройки таблиц? (файлов: {files.Length}, папок: {Directory.GetDirectories(dir).Length})",
                "Сброс настроек таблиц",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            int deleted = 0;
            foreach (var file in files)
            {
                try
                {
                    File.Delete(file);
                    deleted++;
                }
                catch
                {
                    // пропускаем неудачные удаления
                }
            }

            int deletedDirs = 0;
            foreach (var subdir in Directory.GetDirectories(dir))
            {
                try
                {
                    Directory.Delete(subdir, true);
                    deletedDirs++;
                }
                catch
                {
                    // пропускаем неудачные удаления
                }
            }

            MessageBox.Show($"Сброс настроек таблиц завершен. Удалено файлов: {deleted}, папок: {deletedDirs}.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
