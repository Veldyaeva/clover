using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using SewingProduction.Features.UserDistribution.Helpers;

namespace SewingProduction.Core.Class.Settings
{
    public class FormManager
    {
        private readonly Form _mainForm;
        private readonly BarManager _barManager; // вместо MenuStrip
        private readonly UserClass _user;
        private readonly Dictionary<string, Form> _openedForms;

        public FormManager(Form mainForm, BarManager barManager, UserClass user)
        {
            _mainForm = mainForm ?? throw new ArgumentNullException(nameof(mainForm));
            _barManager = barManager; 
            _user = user;
            _openedForms = new Dictionary<string, Form>();
        }

        public void OpenForm(Form form, object sender = null)
        {
            if (form == null) return;

            form.MdiParent = _mainForm;

            string key = ExtractBarItemName(sender);

            bool allowDuplicates = SettingsManager.GetAllowDuplicateTabs();

            if (!allowDuplicates && !string.IsNullOrWhiteSpace(key))
            {
                if (_openedForms.TryGetValue(key, out var existing) && existing != null && !existing.IsDisposed)
                {
                    existing.Activate();
                    return;
                }
                _openedForms.Remove(key);
            }

            if (!string.IsNullOrWhiteSpace(key) && !_openedForms.ContainsKey(key))
                _openedForms[key] = form;

            form.FormClosed += (_, __) =>
            {
                if (string.IsNullOrWhiteSpace(key)) return;
                _openedForms.Remove(key);
            };

            form.Show();
        }

        public async Task RestoreOpenTabs()
        {
            if (_barManager == null || _user == null) return;

            var openTabs = SettingsManager.GetOpenTabs(_user.UserName);
            foreach (var barItemName in openTabs)
            {
                if (string.IsNullOrWhiteSpace(barItemName)) continue;

                var item = GetBarItemByName(barItemName);

                if (item is BarButtonItem button)
                {
                    await Task.Delay(50);

                    if (_mainForm.InvokeRequired)
                        _mainForm.BeginInvoke(new Action(button.PerformClick));
                    else
                        button.PerformClick();
                }
            }
        }
        private static string ExtractBarItemName(object sender)
        {
            return sender switch
            {
                BarItem bi => bi.Name,
                ItemClickEventArgs icea when icea.Item != null => icea.Item.Name,
                _ => null
            };
        }

        public void SaveOpenTabs()
        {
            SettingsManager.SetOpenTabs(_user.UserName, _openedForms.Keys.ToList());
        }

        public BarItem GetBarItemByName(string name)
        {
            if (_barManager == null || string.IsNullOrWhiteSpace(name))
                return null;

            return _barManager.Items.FirstOrDefault(i => i != null && i.Name == name);
        }

        public Form GetActiveForm()
        {
            if (_mainForm == null)
                return null;

            foreach (var form in _mainForm.MdiChildren)
            {
                if (form.ContainsFocus || form == _mainForm.ActiveMdiChild)
                    return form;
            }

            return null;
        }
        public string GetHelpFilePath(Form form = null)
        {
            string projectRoot = AppContext.BaseDirectory;

            Form targetForm = form ?? GetActiveForm();

            if (targetForm == null)
                return Path.Combine(projectRoot, "Help", "Help.html");

            var type = targetForm.GetType();
            var ns = type.Namespace ?? string.Empty;
            var className = type.Name;

            string relativeNamespace = ns.StartsWith("SewingProduction.")
                ? ns.Substring("SewingProduction.".Length)
                : ns;

            string relativePath = Path.Combine(
                relativeNamespace.Replace('.', Path.DirectorySeparatorChar),
                className + ".html"
            );

            return Path.Combine(projectRoot, "Help", relativePath);
        }
    }
}
