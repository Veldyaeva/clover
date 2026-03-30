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
using SewingProduction.Extensions;
using SewingProduction.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static SewingProduction.Helpers.LayoutControlGroupHelper;

namespace SewingProduction.Helpers
{
    public class LayoutControlGroupHelper
    {
        public readonly FileLogger _logger = new FileLogger();
        /// <summary>
        /// Устанавливает видимость кнопок в заголовке LayoutControlGroup по их тегам.
        /// </summary>
        /// <param name="_lcGroup"></param>
        /// <param name="_visible"></param>
        /// <param name="_tags"></param>
        public void SetButtonsVisible(LayoutControlGroup _lcGroup, bool _visible, params string[] _tags)
        {
            foreach (var btn in _lcGroup.CustomHeaderButtons
                     .Where(b => _tags.Contains(b.Properties.Tag as string)))
            {
                btn.Properties.Visible = _visible;
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
            foreach (var btn in _lcGroup.CustomHeaderButtons
                     .Where(b => _tags.Contains(b.Properties.Tag as string)))
            {
                btn.Properties.Enabled = _enabled;
            }
        }
    }
}