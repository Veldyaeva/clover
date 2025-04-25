using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraScheduler.Drawing;
using DevExpress.XtraTab;
using SewingProduction.form;
using SewingProduction.Helpers;
using SewingProduction.Interfaces;
using SewingProduction.Models;
using SewingProduction.Services;
using SewingProduction.Helpers;
using DevExpress.XtraExport.Helpers;
using System.Threading.Tasks;
using DevExpress.Data.Filtering;
using DevExpress.CodeParser;
using SewingProduction.form.TeamWork;
using DevExpress.XtraBars.Customization;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using NLog;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z.Dapper.Plus;
using DevExpress.XtraCharts;
using SewingProduction.Interfaces;
using System.IO;
using SewingProduction.form.UserDistribution;

namespace SewingProduction.Forms
{
    public partial class TeamWork : CustomForm
    {
        private readonly DatabaseHelper _dbHelper; 
        private readonly DbService _dbService;
        private readonly ArtNormService _artNormService;
        private int selectedRowHandle = -1;
        private readonly ILogger _logger = new FileLogger();
        private readonly GridHelper _gridHelper = new GridHelper();

        private int bufferWorkDivision;
        //private readonly BindingList<ArtNormN> _bindingList;
        //private readonly BindingSource _bindingSource;
        //public TeamWork(UserClass user) : base(user);
        private readonly SplitContainerHelper _splitContainerHelper = new SplitContainerHelper();
        private int bufferId = 0;
        private BindingList<ArtNormN> _bindingList = new BindingList<ArtNormN>();
        private BindingSource _bindingSource = new BindingSource();
        private bool _hasUnsavedChanges = false;
        private BindingSource _nzpByKoddRtSource = new BindingSource();
        private BindingList<NZPByKoddRt> _nzpList = new BindingList<NZPByKoddRt>();
        private BindingList<NormRasz> _normRaszListTW;
        private BindingSource _normRaszBindingSourceTW;
        private BindingList<NormRask> _normRaskListTW;
        private BindingSource _normRaskBindingSourceTW;
        private BindingList<NormKont> _normKontListTW;
        private BindingSource _normKontBindingSourceTW;
        private BindingList<NormDopObr> _normDopObrListTW;
        private BindingSource _normDopObrBindingSourceTW;
        private static List<FioModel> _cachedFioData;
        private List<FioModel> fioList;

        public TeamWork()

        {
            InitializeComponent();
            DapperMappings.Configure();
            _dbHelper = new DatabaseHelper("ace");
            _dbService = new DbService(_dbHelper);
            _artNormService = new ArtNormService(_dbHelper);
            ThemeManager.UpdateTheme(this);

            // Инициализация привязок данных
            _bindingSource.DataSource = _bindingList;
          //  _bindingSource.PositionChanged += BindingSource_PositionChanged;
            if (ANNgridControl != null)
            {
                ANNgridControl.DataSource = _bindingSource;
            }
            //_bindingSource.PositionChanged += BindingSource_PositionChanged;
            // Настройка гридов
            if (customGridControl1 != null && customGridControl1.MainView is GridView view7)
            {
                view7.OptionsSelection.MultiSelect = false;
                view7.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            }

            if (customGridControl2 != null && customGridControl2.MainView is GridView view8)
            {
                view8.OptionsSelection.MultiSelect = false;
                view8.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            }
            this.gridView7.CellValueChanging += (s, e) => GridView_CellValueChanged<MyDataART>(customGridControl1, e);
            this.gridView8.CellValueChanging += (s, e) => GridView_CellValueChanged<MyDataANN>(customGridControl2, e);
        }

        private async void TeamWorkForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Проверяем инициализацию компонентов
                if (customGridControl1 == null || customGridControl2 == null || ANNgridControl == null)
                {
                    throw new InvalidOperationException("Критические компоненты формы не инициализированы");
                }

                // Загружаем настройки гридов
                LoadGridSettings();

                // Инициализируем привязки данных
                _bindingSource.DataSource = _bindingList;
                ANNgridControl.DataSource = _bindingSource;
                _nzpByKoddRtSource.DataSource = _nzpList;
                customGridControl5.DataSource = _nzpByKoddRtSource;

                // Загружаем данные
                await LoadWorkDivisions();
                sortGridView(gridView1);
                sortGridView(gridView4);
                sortGridView(gridView3);

                // Обновляем UI
                ANNgridControl.RefreshDataSource();
                customGridControl1.RefreshDataSource();
                customGridControl2.RefreshDataSource();

                //// Привязываем комментарий
                //if (commentRichTextBox != null)
                //{
                //     commentRichTextBox.DataBindings.Clear(); 
                //     commentRichTextBox.DataBindings.Add("Text", _bindingSource, nameof(ArtNormN.Komment), true, DataSourceUpdateMode.OnPropertyChanged);
                //}

                //// Настраиваем и привязываем Дизайнера
                //if (lookUpDesigner != null && desBindingSource != null) 
                //{
                //    // Настройка
                //    lookUpDesigner.Properties.DataSource = desBindingSource;
                //    lookUpDesigner.Properties.ValueMember = "tab"; 
                //    lookUpDesigner.Properties.DisplayMember = "fio";
                //    lookUpDesigner.Properties.Columns.Clear();
                //    lookUpDesigner.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("fio", "ФИО"));

                //    // Привязка EditValue к ID дизайнера в основной модели
                //    lookUpDesigner.DataBindings.Clear();
                //    lookUpDesigner.DataBindings.Add("EditValue", _bindingSource, nameof(ArtNormN.Diz), true, DataSourceUpdateMode.OnPropertyChanged);
                //}
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке формы");
                MessageBox.Show($"Ошибка при инициализации формы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик смены активной вкладки
        /// </summary>
        private async void XtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (xtraTabControl1.SelectedTabPage.Name)
            {
                case "xtraTabPageWorkDivisions":
                    await LoadWorkDivisions();
                    break;

                case "xtraTabPageArticles":
                    await CurrentWorks_Load();
                    break;
            }
        }


        private void ButtonEditWd_Click(object sender, EventArgs e)
        {
            ButtonEditWd_Click_Internal(sender, e);
        }

        /// <summary>
        /// Архив+копия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonArchAndCopyWd_Click(object sender, EventArgs e)
        {
            await ArchAndCopy();
        }

        /// <summary>
        /// Отвязать артикул от РТ
        /// Удаляет связь между выбранным артикулом и разделением труда.
        /// </summary>
        private async void ResetButton_Click(object sender, EventArgs e)
        {
            await UnboundWD(sender, e);
        }
        private async void customCheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            customCheckBox6_CheckedChanged_Internal(sender, e);
        }
        private async void ButtonPreliminaryWd_Click(object sender, EventArgs e)
        {
            ButtonPreliminaryWd_Click_Internal(sender, e);
        }

        private async void ButtonCopyWd_Click(object sender, EventArgs e)
        {
            ButtonCopyWd_Click_Internal(sender, e);
        }
        private async void gridView5_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        { 
            gridView5_FocusedRowChanged_Internal(sender, e); 
        }
        private async void gridView7_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            gridView7_FocusedRowChanged_Internal(sender, e);
        }
        private void Filter_CheckedChanged(object sender, EventArgs e)
        {
            Filter_CheckedChanged_Internal(sender, e);
        }
        private async void search_CheckedChanged(object sender, EventArgs e)
        { search_CheckedChanged_Internal(sender, e); }
        private void gridControl2_Leave(object sender, EventArgs e)
        {
            gridControl2_Leave_Internal(sender, e);
        }
        private async void gridControl2_GotFocus(object sender, EventArgs e)
        { gridControl2_GotFocus_Internal(sender, e); }

        private void searchControl1_QueryIsSearchColumn(object sender, DevExpress.XtraEditors.QueryIsSearchColumnEventArgs args)
        {
            searchControl1_QueryIsSearchColumn_Internal(sender, args);
        }

        private void SearchButton_Click(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SearchButton_Click_Internal(sender, e);
        }

        private void gridView3_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            gridView3_FocusedRowChanged_Internal(sender, e);
        }
        private async void customButton12_Click(object sender, EventArgs e)
        { customButton12_Click_Internal(sender, e); }
        private async void customCheckBox4_CheckedChanged(object sender, EventArgs e)
        { customCheckBox4_CheckedChanged_Internal(sender, e); }

        private async void simpleButton2_Click(object sender, EventArgs e)
        { simpleButton2_Click_Internal(sender, e); }

        /// <summary>
        /// Обработка закрытия формы
        /// </summary>
        private async void TeamWork_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_hasUnsavedChanges)
            {
                var result = MessageBox.Show(
                    "Есть несохраненные изменения. Вы уверены, что хотите выйти?",
                    "Подтверждение закрытия",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            SaveGridSettings();
            await _logger.LogEventAsync("Форма TeamWork закрыта", "FormClosing");
        }

        private async void customButton2_Click(object sender, EventArgs e)
        {
            await Arch(sender, e);
        }

        private async void BindButton_Click(object sender, EventArgs e)
        {
            await BindButton_Click_Internal(sender, e);
        }
    }
}

