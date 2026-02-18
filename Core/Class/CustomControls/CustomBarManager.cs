using System;
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraBars;
using SewingProduction.Features.UserDistribution.Helpers;

namespace SewingProduction.Core.Class.CustomControls
{
    public class CustomBarManager : BarManager
    {
        public UserClass User { get; private set; }

        /// <summary>
        /// Если права на объект не найдены — скрываем (безопаснее).
        /// </summary>
        public bool HideIfNoRight { get; set; } = true;

        /// <summary>
        /// Если true — DevExpress Skin/Theme items не трогаем правами (всегда видимы).
        /// Это нужно, потому что DevExpress добавляет служебные items.
        /// </summary>
        public bool SkipDevExpressSkinItems { get; set; } = true;

        // важно: чтобы Designer мог делать new CustomBarManager(components)
        public CustomBarManager() : base()
        {
        }

        public CustomBarManager(IContainer container) : base(container)
        {
        }

        /// <summary>
        /// Применить права к текущим BarItem'ам.
        /// Важно: user должен быть уже с загруженным myObjectForm (LoadObjectForm(formName)).
        /// </summary>
        public void ApplyPermissions(UserClass user)
        {
            User = user;
            if (User == null) return;

            // 1) применяем права ко всем items
            foreach (BarItem item in Items)
                ApplyToItem(item);

            // 2) чистим пустые подменю (после применения)
            foreach (BarItem item in Items)
                if (item is BarSubItem sub)
                    CleanupEmptySubmenus(sub);
        }

        private void ApplyToItem(BarItem item)
        {
            if (item == null) return;

            // Скины/темы не трогаем правами
            if (SkipDevExpressSkinItems && IsDevExpressSkinItem(item))
            {
                item.Visibility = BarItemVisibility.Always;
                item.Enabled = true;
                return;
            }

            // КЛЮЧ = Name (как в БД и как сканит MenuScanner)
            string objectName = item.Name;

            if (string.IsNullOrWhiteSpace(objectName))
            {
                // без имени — лучше скрыть, чтобы не открыть лишнего
                if (HideIfNoRight)
                {
                    item.Visibility = BarItemVisibility.Never;
                    item.Enabled = false;
                }
                else
                {
                    item.Visibility = BarItemVisibility.Always;
                    item.Enabled = true;
                }
                return;
            }

            bool hasWrite = User.HasPermission(objectName, "Редактор");
            bool hasRead = User.HasPermission(objectName, "Просмотр");

            if (hasRead || hasWrite)
            {
                item.Visibility = BarItemVisibility.Always;
                item.Enabled = hasWrite; // если только просмотр — disabled, но видно
            }
            else
            {
                if (HideIfNoRight)
                {
                    item.Visibility = BarItemVisibility.Never;
                    item.Enabled = false;
                }
                else
                {
                    item.Visibility = BarItemVisibility.Always;
                    item.Enabled = true;
                }
            }
        }

        private void CleanupEmptySubmenus(BarSubItem sub)
        {
            if (sub == null) return;

            // сначала дочерние
            foreach (BarItemLink link in sub.ItemLinks)
                if (link?.Item is BarSubItem childSub)
                    CleanupEmptySubmenus(childSub);

            bool anyVisibleChild = sub.ItemLinks.Any(l =>
                l?.Item != null && l.Item.Visibility == BarItemVisibility.Always);

            if (!anyVisibleChild)
            {
                sub.Visibility = BarItemVisibility.Never;
                sub.Enabled = false;
            }
        }

        private static bool IsDevExpressSkinItem(BarItem item)
        {
            // надёжнее по типу + по имени
            var t = item.GetType().FullName ?? "";

            if (t.Contains("DevExpress.XtraBars.SkinBarSubItem") ||
                t.Contains("DevExpress.XtraBars.SkinDropDownButtonItem") ||
                t.Contains("DevExpress.XtraBars.SkinPaletteDropDownButtonItem"))
                return true;

            var n = item.Name ?? "";
            if (n.StartsWith("skin", StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
    }
}
