using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.form.UserDistribution;

namespace SewingProduction.Features.UserDistribution.Helpers
{
    public class MenuScanner
    {
        private readonly AdminFormDataService _adminFormDataService;
        private readonly UserClass _user;

        public MenuScanner(AdminFormDataService adminFormDataService, UserClass user)
        {
            _adminFormDataService = adminFormDataService;
            _user = user;
        }

        public async Task ScanAndInsertMenuAsync(MenuStrip menuStrip, int formId)
        {
            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                await ScanMenuItemAsync(item, formId);
            }
        }

        private async Task ScanMenuItemAsync(ToolStripMenuItem item, int formId)
        {
            string name = item.Name;
            string text = item.Text;

            if (!string.IsNullOrEmpty(name))
            {
                bool exists = await _adminFormDataService.ObjectExists(formId, name);

                if (!exists)
                {
                    await _adminFormDataService.InsertObjectForm(name, text, "ToolStripMenuItem", _user.UserId, formId);
                    Debug.WriteLine($"[MenuScanner] ✅ Добавлен: Name = {name}, Text = {text}");
                }
                else
                {
                    Debug.WriteLine($"[MenuScanner] ⚠️ Уже существует: Name = {name}, Text = {text}");
                }
            }

            foreach (ToolStripItem subItem in item.DropDownItems)
            {
                if (subItem is ToolStripMenuItem subMenuItem)
                {
                    await ScanMenuItemAsync(subMenuItem, formId);
                }
            }
        }
    }

}
