using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.Features.UserDistribution.Helpers;
using System.Windows.Forms;

namespace SewingProduction.Core.Class.Settings
{
    public class FormManager
    {
        private readonly Form _mainForm;
        private readonly MenuStrip _menuStrip;
        private readonly UserClass _user;
        private readonly Dictionary<string, Form> _openedForms;

        public FormManager(Form mainForm, MenuStrip menuStrip, UserClass user)
        {
            _mainForm = mainForm;
            _menuStrip = menuStrip;
            _user = user;
            _openedForms = new Dictionary<string, Form>();
        }

        public void OpenForm(Form form, object sender = null)
        {
            form.MdiParent = _mainForm;
            string menuItemName = sender switch
            {
                ToolStripMenuItem menuItem => menuItem.Name,
                string name => name,
                _ => null
            };

            if (!string.IsNullOrEmpty(menuItemName))
            {
                if (_openedForms.TryGetValue(menuItemName, out Form existingForm))
                {
                    if (existingForm != null && !existingForm.IsDisposed)
                    {
                        existingForm.Activate();
                        return;
                    }
                    else
                        _openedForms.Remove(menuItemName);
                }
                _openedForms.Add(menuItemName, form);
            }

            form.FormClosed += (s, e) =>
            {
                var closedForm = s as Form;
                var item = _openedForms.FirstOrDefault(x => x.Value == closedForm);
                if (!string.IsNullOrEmpty(item.Key))
                {
                    _openedForms.Remove(item.Key);
                }
            };

            form.Show();
        }

        public async Task RestoreOpenTabs()
        {
            var openTabs = SettingsManager.GetOpenTabs(_user.UserName);
            foreach (var menuItemName in openTabs)
            {
                ToolStripMenuItem menuItem = FindMenuItemByName(_menuStrip.Items, menuItemName);
                if (menuItem != null)
                {
                    await Task.Delay(100);
                    menuItem.PerformClick();
                }
            }
        }

        public void SaveOpenTabs()
        {
            SettingsManager.SetOpenTabs(_user.UserName, _openedForms.Keys.ToList());
        }
        public ToolStripMenuItem GetMenuItemByName(string name)
        {
            return FindMenuItemByName(_menuStrip.Items, name);
        }
        private ToolStripMenuItem FindMenuItemByName(ToolStripItemCollection items, string name)
        {
            foreach (ToolStripItem item in items)
            {
                if (item.Name == name && item is ToolStripMenuItem menuItem)
                    return menuItem;

                if (item is ToolStripMenuItem parentMenu && parentMenu.DropDownItems.Count > 0)
                {
                    var found = FindMenuItemByName(parentMenu.DropDownItems, name);
                    if (found != null)
                        return found;
                }
            }
            return null;
        }


    }
}
