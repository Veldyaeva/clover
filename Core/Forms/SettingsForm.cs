using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using SewingProduction.Core.Class.Settings;
using SewingProduction.Features.UserDistribution.Helpers;
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
            var current = SettingsManager.Current.Theme;
            customComboBoxTheme.Items.Clear();
            customComboBoxTheme.Items.AddRange(ThemeManager.GetAvailableThemes().ToArray());
            customComboBoxTheme.SelectedItem = current;
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
    }
}
