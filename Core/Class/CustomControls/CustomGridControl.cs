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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.Services;
using SewingProduction.Core.Services;
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
                
        /// <summary>
        /// Включает автоматическое сохранение и загрузку настроек грида
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool EnableAutoSettings { get; set; } = true;
        
        /// <summary>
        /// Пользовательский ключ для настроек грида (если не указан, генерируется автоматически)
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SettingsKey { get; set; }

        private bool _visiblePermission = true;
        private bool _visibleLogic = true;
        private UserClass _user;
        private List<int> _tableIds;
        private int _formId;
        private bool _settingsInitialized = false;
        public CustomGridControl()
        {
            ApplyTheme();
            ThemeManager.ThemeChanged += OnThemeChanged;
            ViewRegistered += OnViewRegistered;
                        
            // Подписываемся на события для инициализации настроек
            this.Load += OnCustomGridLoad;
            this.HandleCreated += OnHandleCreated;
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
                // Инициализируем настройки для нового GridView
                InitializeGridSettings(gridView);
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
       /// <summary>
        /// Обработчик события Load для инициализации настроек
        /// </summary>
        private void OnCustomGridLoad(object sender, EventArgs e)
        {
            InitializeAllGridSettings();
        }

        /// <summary>
        /// Обработчик создания handle для инициализации настроек
        /// </summary>
        private void OnHandleCreated(object sender, EventArgs e)
        {
            InitializeAllGridSettings();
        }

        /// <summary>
        /// Инициализирует настройки для всех GridView в контроле
        /// </summary>
        private void InitializeAllGridSettings()
        {
            if (_settingsInitialized || !EnableAutoSettings) return;

            foreach (var view in ViewCollection)
            {
                if (view is GridView gridView)
                {
                    InitializeGridSettings(gridView);
                }
            }

            _settingsInitialized = true;
        }

        /// <summary>
        /// Инициализирует настройки для конкретного GridView
        /// </summary>
        private void InitializeGridSettings(GridView gridView)
        {
            if (!EnableAutoSettings || gridView == null) return;

            try
            {
                var settingsKey = SettingsKey ?? GenerateSettingsKey(gridView);
                GridSettingsManager.Instance.EnableAutoSettings(gridView, settingsKey);
            }
            catch (Exception ex)
            {
                // Логируем ошибку, но не прерываем работу
                System.Diagnostics.Debug.WriteLine($"Ошибка при инициализации настроек грида: {ex.Message}");
            }
        }

        /// <summary>
        /// Генерирует ключ настроек для GridView
        /// </summary>
        private string GenerateSettingsKey(GridView gridView)
        {
            var formName = this.FindForm()?.GetType().Name ?? "UnknownForm";
            var gridName = this.Name ?? "UnknownGrid";
            var viewName = gridView.Name ?? "MainView";
            return $"{formName}_{gridName}_{viewName}";
        }

        /// <summary>
        /// Принудительно сохраняет настройки всех GridView
        /// </summary>
        public void SaveGridSettings()
        {
            if (!EnableAutoSettings) return;

            foreach (var view in ViewCollection)
            {
                if (view is GridView gridView)
                {
                    var settingsKey = SettingsKey ?? GenerateSettingsKey(gridView);
                    GridSettingsManager.Instance.SaveSettings(gridView, settingsKey);
                }
            }
        }

        /// <summary>
        /// Принудительно загружает настройки всех GridView
        /// </summary>
        public void LoadGridSettings()
        {
            if (!EnableAutoSettings) return;

            foreach (var view in ViewCollection)
            {
                if (view is GridView gridView)
                {
                    var settingsKey = SettingsKey ?? GenerateSettingsKey(gridView);
                    GridSettingsManager.Instance.LoadSettings(gridView, settingsKey);
                }
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                               // Сохраняем настройки перед закрытием
                if (EnableAutoSettings)
                {
                    SaveGridSettings();
                }

                // Отключаем автоматическое сохранение для всех GridView
                foreach (var view in ViewCollection)
                {
                    if (view is GridView gridView)
                    {
                        GridSettingsManager.Instance.DisableAutoSettings(gridView);
                    }
                }
                ThemeManager.ThemeChanged -= OnThemeChanged;
                                this.Load -= OnCustomGridLoad;
                this.HandleCreated -= OnHandleCreated;
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisiblePermission
        {
            get => _visiblePermission;
            set
            {
                _visiblePermission = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool VisibleLogic
        {
            get => _visibleLogic;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }

        public new bool Visible
        {
            get => base.Visible;
            set
            {
                _visibleLogic = value;
                VisibilityHelper.UpdateVisibility(this, _visiblePermission, _visibleLogic);
            }
        }
    }

}
