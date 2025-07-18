using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars.Docking2010.DragEngine;
using DevExpress.XtraBars.ViewInfo;
using DevExpress.XtraGrid;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using static DevExpress.LookAndFeel.DXSkinColors;

namespace SewingProduction.Core.Class
{
    public class CustomGridControl : GridControl, SewingProduction.IThemeable, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color? AlternateRowColor { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color? FocusedRowColor { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool FocusedRowBold { get; set; } = true;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }
        private UserClass _user;
        private List<int> _tableIds;
        private int _formId;
        public CustomGridControl()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
            ViewRegistered += OnViewRegistered;
        }
        public void ApplyTheme()
        {
            BackColor = ThemeManager.ActiveTheme.GridBackground;
            ForeColor = ThemeManager.ActiveTheme.TextBoxText;
            Font = ThemeManager.SharedSettings.DefaultFont;
            AlternateRowColor = ThemeManager.ActiveTheme.BandHighlightColor;
            FocusedRowColor = ThemeManager.ActiveTheme.ButtonBackground;


            foreach (var view in ViewCollection)
            {
                if (view is DevExpress.XtraGrid.Views.Grid.GridView gridView)
                {
                    ApplyRowColors(gridView);
                    ApplyFocusedRowStyle(gridView);
                    gridView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                    gridView.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                }
            }
        }
        private void OnViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            if (e.View is DevExpress.XtraGrid.Views.Grid.GridView gridView)
            {
                ApplyRowColors(gridView);
                ApplyFocusedRowStyle(gridView);
            }
        }
        private void ApplyRowColors(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            if (AlternateRowColor.HasValue)
            {
                gridView.Appearance.EvenRow.BackColor = AlternateRowColor.Value;
                gridView.OptionsView.EnableAppearanceEvenRow = true;
            }
        }

        private void ApplyFocusedRowStyle(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            if (FocusedRowColor.HasValue)
            {
                gridView.Appearance.FocusedRow.BackColor = FocusedRowColor.Value;
                gridView.Appearance.FocusedRow.Options.UseBackColor = true;
            }

            if (FocusedRowBold)
            {
                gridView.Appearance.FocusedRow.Font = new Font(gridView.Appearance.FocusedRow.Font ?? Font, FontStyle.Bold);
                gridView.Appearance.FocusedRow.Options.UseFont = true;
            }
        }
        private void OnThemeChanged() => ApplyTheme();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
            }
            base.Dispose(disposing);
        }
        public void ApplyPermission(UserClass user)
        {
            //PermissionHelper.ApplyTo(this, ObjectName, user);

            if (string.IsNullOrEmpty(ObjectName)) return;

            bool hasWrite = user.HasPermission(ObjectName, "Редактор");
            bool hasRead = user.HasPermission(ObjectName, "Просмотр");

            this.Visible = hasRead || hasWrite;
            //this.Enabled = hasWrite;
            foreach (var view in ViewCollection)
            {
                if (view is DevExpress.XtraGrid.Views.Grid.GridView gridView)
                {
                    gridView.OptionsBehavior.ReadOnly = !hasWrite;
                    gridView.OptionsBehavior.Editable = hasWrite;
                }
            }
        }

    }

}
