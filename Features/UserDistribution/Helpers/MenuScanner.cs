using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.XtraBars;
using SewingProduction.Features.UserDistribution.Forms;

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

        #region === DevExpress BarManager Scan ===

        public async Task ScanAndInsertBarAsync(BarManager manager, int formId)
        {
            if (_adminFormDataService == null) return;
            if (manager == null) return;

            // Защита от циклов / повторов
            var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (Bar bar in manager.Bars)
            {
                if (bar == null) continue;

                foreach (BarItemLink link in bar.ItemLinks)
                {
                    var item = link?.Item;
                    if (item == null) continue;

                    await ScanBarItemAsync(item, formId, visited);
                }
            }
        }

        private async Task ScanBarItemAsync(BarItem item, int formId, HashSet<string> visited)
        {
            if (item == null) return;
            if (visited == null) return;

            // скины/темы не пишем в БД
            if (IsDevExpressSkinItem(item))
                return;

            // КЛЮЧ — ТОЛЬКО Name (как ты и сказал)
            string name = (item.Name ?? "").Trim();
            if (string.IsNullOrWhiteSpace(name))
                return;

            // если уже проходили — выходим (иначе легко зациклиться на ссылках)
            if (!visited.Add(name))
                return;

            string caption = GetBarCaption(item);
            string typeName = item.GetType().Name;

            bool exists = await _adminFormDataService.ObjectExists(formId, name);
            if (!exists)
            {
                await _adminFormDataService.InsertObjectForm(
                    name,               // object_name (ключ)
                    caption,            // caption
                    typeName,           // type
                    _user?.UserId ?? 0, // кто добавил
                    formId);

                Debug.WriteLine($"[MenuScanner] ✅ Добавлен BarItem: {name} / {caption} ({typeName})");
            }

            // рекурсия для подменю
            if (item is BarSubItem sub)
            {
                foreach (BarItemLink childLink in sub.ItemLinks)
                {
                    var child = childLink?.Item;
                    if (child == null) continue;

                    await ScanBarItemAsync(child, formId, visited);
                }
            }
        }

        private static string GetBarCaption(BarItem item)
        {
            string cap = item?.Caption ?? "";
            return cap.Replace("&", "").Trim();
        }

        private static bool IsDevExpressSkinItem(BarItem item)
        {
            if (item == null) return false;

            string fullName = item.GetType().FullName ?? "";
            if (fullName.Contains("DevExpress.XtraBars.SkinBarSubItem", StringComparison.OrdinalIgnoreCase) ||
                fullName.Contains("DevExpress.XtraBars.SkinDropDownButtonItem", StringComparison.OrdinalIgnoreCase) ||
                fullName.Contains("DevExpress.XtraBars.SkinPaletteDropDownButtonItem", StringComparison.OrdinalIgnoreCase))
                return true;

            string name = item.Name ?? "";
            if (!string.IsNullOrWhiteSpace(name) &&
                name.StartsWith("skin", StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }

        #endregion
    }
}
