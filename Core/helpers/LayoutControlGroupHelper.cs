using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraDialogs.Adapters;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Skins;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraEditors.ButtonsPanelControl;
using SewingProduction.Extensions;
using SewingProduction.Interfaces;
using SewingProduction.Features.UserDistribution.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static SewingProduction.Helpers.LayoutControlGroupHelper;

namespace SewingProduction.Helpers
{
    /// <summary>
    /// Описывает связь между header-button, ее командным тегом и объектом прав в БД.
    /// </summary>
    public sealed class HeaderButtonPermissionBinding
    {
        public LayoutControlGroup Group { get; init; }
        public string ButtonTag { get; init; }
        public string PermissionObjectName { get; init; }
    }

    public class LayoutControlGroupHelper
    {
        private static readonly ConditionalWeakTable<LayoutControlGroup, Dictionary<string, HeaderButtonPermissionState>> _permissionStates =
            new ConditionalWeakTable<LayoutControlGroup, Dictionary<string, HeaderButtonPermissionState>>();

        public readonly FileLogger _logger = new FileLogger();

        public sealed class HeaderButtonPermissionState
        {
            public string PermissionObjectName { get; set; }
            public bool VisiblePermission { get; set; } = true;
            public bool VisibleLogic { get; set; } = true;
            public bool EnabledPermission { get; set; } = true;
            public bool EnabledLogic { get; set; } = true;
        }

        /// <summary>
        /// Устанавливает видимость кнопок в заголовке LayoutControlGroup по их тегам.
        /// </summary>
        /// <param name="_lcGroup"></param>
        /// <param name="_visible"></param>
        /// <param name="_tags"></param>
        public void SetButtonsVisible(LayoutControlGroup _lcGroup, bool _visible, params string[] _tags)
        {
            foreach (var btn in EnumerateButtons(_lcGroup, _tags))
            {
                var state = GetOrCreateState(_lcGroup, btn.Tag as string);
                if (state != null)
                {
                    state.VisibleLogic = _visible;
                    ApplyButtonState(btn, state);
                }
                else
                {
                    btn.Visible = _visible;
                }
            }
        }
        /// <summary>
        /// Устанавливает доступность кнопок в заголовке LayoutControlGroup по их тегам.
        /// </summary>
        /// <param name="_lcGroup"></param>
        /// <param name="_enabled"></param>
        /// <param name="_tags"></param>
        public void SetButtonsEnabled(LayoutControlGroup _lcGroup, bool _enabled, params string[] _tags)
        {
            foreach (var btn in EnumerateButtons(_lcGroup, _tags))
            {
                var state = GetOrCreateState(_lcGroup, btn.Tag as string);
                if (state != null)
                {
                    state.EnabledLogic = _enabled;
                    ApplyButtonState(btn, state);
                }
                else
                {
                    btn.Enabled = _enabled;
                }
            }
        }

        /// <summary>
        /// Регистрирует permission-ключ для header-button. Tag кнопки остается строковым идентификатором команды.
        /// </summary>
        public void RegisterButtonPermission(LayoutControlGroup group, string buttonTag, string permissionObjectName)
        {
            if (group == null || string.IsNullOrWhiteSpace(buttonTag) || string.IsNullOrWhiteSpace(permissionObjectName))
            {
                return;
            }

            var state = GetOrCreateState(group, buttonTag);
            if (state == null)
            {
                return;
            }

            state.PermissionObjectName = permissionObjectName.Trim();

            var button = FindButton(group, buttonTag);
            if (button != null)
            {
                ApplyButtonState(button, state);
            }
        }

        /// <summary>
        /// Применяет права пользователя ко всем зарегистрированным header-buttons группы.
        /// </summary>
        public void ApplyButtonPermissions(LayoutControlGroup group, UserClass user)
        {
            if (group == null || user == null)
            {
                return;
            }

            if (!_permissionStates.TryGetValue(group, out var states) || states.Count == 0)
            {
                return;
            }

            foreach (var pair in states)
            {
                var button = FindButton(group, pair.Key);
                if (button == null)
                {
                    continue;
                }

                var state = pair.Value;
                if (string.IsNullOrWhiteSpace(state.PermissionObjectName))
                {
                    ApplyButtonState(button, state);
                    continue;
                }

                bool hasWrite = user.HasPermission(state.PermissionObjectName, "Редактор");
                bool hasRead = user.HasPermission(state.PermissionObjectName, "Просмотр");

                state.VisiblePermission = hasRead || hasWrite;
                state.EnabledPermission = hasWrite;

                ApplyButtonState(button, state);
            }
        }

        private static IEnumerable<GroupBoxButton> EnumerateButtons(LayoutControlGroup group, params string[] tags)
        {
            if (group?.CustomHeaderButtons == null || tags == null || tags.Length == 0)
            {
                return Enumerable.Empty<GroupBoxButton>();
            }

            return group.CustomHeaderButtons
                .OfType<GroupBoxButton>()
                .Where(button => tags.Contains(button.Tag as string));
        }

        private static GroupBoxButton FindButton(LayoutControlGroup group, string tag)
        {
            if (group?.CustomHeaderButtons == null || string.IsNullOrWhiteSpace(tag))
            {
                return null;
            }

            return group.CustomHeaderButtons
                .OfType<GroupBoxButton>()
                .FirstOrDefault(button => string.Equals(button.Tag as string, tag, StringComparison.Ordinal));
        }

        private static HeaderButtonPermissionState GetOrCreateState(LayoutControlGroup group, string buttonTag)
        {
            if (group == null || string.IsNullOrWhiteSpace(buttonTag))
            {
                return null;
            }

            var states = _permissionStates.GetOrCreateValue(group);
            if (!states.TryGetValue(buttonTag, out var state))
            {
                state = new HeaderButtonPermissionState();
                states[buttonTag] = state;
            }

            return state;
        }

        private static void ApplyButtonState(GroupBoxButton button, HeaderButtonPermissionState state)
        {
            if (button == null || state == null)
            {
                return;
            }

            button.Visible = state.VisiblePermission && state.VisibleLogic;
            button.Enabled = state.EnabledPermission && state.EnabledLogic;
        }
    }
}
