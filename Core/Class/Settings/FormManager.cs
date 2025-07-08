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

            bool allowDuplicates = SettingsManager.GetAllowDuplicateTabs();

            string key = sender switch
            {
                ToolStripMenuItem menuItem => menuItem.Name,
                string name => name,
                _ => form.Text // если не из меню — используем заголовок формы как ключ
            };

            // 🔐 Проверка по ключу
            if (!allowDuplicates && !string.IsNullOrEmpty(key))
            {
                if (_openedForms.TryGetValue(key, out Form existingForm))
                {
                    if (existingForm != null && !existingForm.IsDisposed)
                    {
                        existingForm.Activate();
                        return;
                    }
                    else
                    {
                        _openedForms.Remove(key);
                    }
                }
            }

            // Регистрируем форму по ключу (если включено отслеживание)
            if (!string.IsNullOrEmpty(key) && !_openedForms.ContainsKey(key))
            {
                _openedForms[key] = form;
            }

            // Удаление формы из словаря при закрытии
            form.FormClosed += (s, e) =>
            {
                var closedForm = s as Form;
                var item = _openedForms.FirstOrDefault(x => x.Value == closedForm);
                if (!string.IsNullOrEmpty(item.Key))
                    _openedForms.Remove(item.Key);
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

        public Form GetActiveForm()
        {
            if (_mainForm == null)
                return null;

            // Ищем форму, у которой сейчас фокус или которая активна
            foreach (var form in _mainForm.MdiChildren)
            {
                if (form.ContainsFocus || form == _mainForm.ActiveMdiChild)
                    return form;
            }

            return null;
        }
    }
}
