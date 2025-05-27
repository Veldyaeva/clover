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

        }
        public interface IDataUpdatableForm
        {
            void UpdateDataInForm();
        }
        public void UpdateDataInForm()
        {
            SettingsForm_Load(null, EventArgs.Empty);
        }
        private void SaveThemeToJson(string theme)
        {
            string path = "settings.json";
            Dictionary<string, object> config;

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                config = JsonConvert.DeserializeObject<Dictionary<string, object>>(json)
                         ?? new Dictionary<string, object>();
            }
            else
            {
                config = new Dictionary<string, object>();
            }

            config["theme"] = theme;
            File.WriteAllText(path, JsonConvert.SerializeObject(config, Formatting.Indented));
        }
    }
}
