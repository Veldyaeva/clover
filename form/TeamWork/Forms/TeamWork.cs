using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

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
        private BindingList<MyDataANN> _preArchList = new BindingList<MyDataANN>();
        private BindingSource _preArchBindingSource;
        private BindingList<MyDataART> _myDataArtList;
        private BindingSource _myDataArtBindingSource;
        private BindingList<MyDataANN> _myDataAnnList; 
        private BindingSource _myDataAnnBindingSource;

        private BindingList<MyDataART> _boundArtList;
        private BindingSource _boundArtBindingSource;
        private List<KodProizvModel> kodProizvList;
        private List<PodrVyazModel> podrVyazList;
        private List<OborudShvModel> oborudShvList;

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
            if (ANNgridControl != null)
            {
                ANNgridControl.DataSource = _bindingSource;
            }
            _preArchList = new BindingList<MyDataANN>();
            _preArchBindingSource = new BindingSource { DataSource = _preArchList };
            gridControlPreArch.DataSource = _preArchBindingSource;
            // Настройка гридов
            if (gridControl_unboundArts != null)
            {
                gridView_unboundArts.OptionsSelection.MultiSelect = false;
                gridView_unboundArts.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            }

            if (gridControl_wdToBind != null && gridControl_wdToBind.MainView is GridView view8)
            {
                view8.OptionsSelection.MultiSelect = false;
                view8.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            }
            this.gridView_unboundArts.CellValueChanging += (s, e) => GridView_CellValueChanged<MyDataART>(gridControl_unboundArts, e);
            this.gridView_wdToBind.CellValueChanging += (s, e) => GridView_CellValueChanged<MyDataANN>(gridControl_wdToBind, e);
            _myDataArtList = new BindingList<MyDataART>();
            _myDataArtBindingSource = new BindingSource { DataSource = _myDataArtList };
            if (gridControl_unboundArts != null)
            {
                gridControl_unboundArts.DataSource = _myDataArtBindingSource;
            }

            _myDataAnnList = new BindingList<MyDataANN>();
            _myDataAnnBindingSource = new BindingSource { DataSource = _myDataAnnList };
            if (gridControl_wdToBind != null)
            {
                gridControl_wdToBind.DataSource = _myDataAnnBindingSource;
            }

            _boundArtList = new BindingList<MyDataART>();
            _boundArtBindingSource = new BindingSource { DataSource = _boundArtList };
            if (gridControl_binded != null) 
            {
                gridControl_binded.DataSource = _boundArtBindingSource;
            }
        }

        private async void TeamWorkForm_Load(object sender, EventArgs e)
        {
            // Отписываемся от события перед загрузкой данных
            if (ANNgridView != null) 
            {
                ANNgridView.FocusedRowChanged -= gridView3_FocusedRowChanged;
            }

            try
            {
                // Проверяем инициализацию компонентов
                if (gridControl_unboundArts == null || gridControl_wdToBind == null || ANNgridControl == null)
                {
                    throw new InvalidOperationException("Критические компоненты формы не инициализированы");
                }

                // Загружаем настройки гридов
                LoadGridSettings();

                // Инициализируем привязки данных
                _bindingSource.DataSource = _bindingList;
                ANNgridControl.DataSource = _bindingSource;
                _nzpByKoddRtSource.DataSource = _nzpList;
                gridControlNZP.DataSource = _nzpByKoddRtSource;

                // Загружаем данные
                await LoadWorkDivisions();
                sortGridView(ANNgridView); 
                sortGridView(gridView4);
                // sortGridView(gridView3); 
                kodProizvList = await _dbService.GetListAsync<KodProizvModel>("select kod_proizv, text_proizv from kod_proizv", null); 
                podrVyazList = await _dbService.GetListAsync<PodrVyazModel>("select kod_vyaz, text_vyaz from podr_vyaz", null);
                oborudShvList = await _dbService.GetListAsync<OborudShvModel>("select kod_ob, text_ob from oborud_shv", null);

                // Обновляем UI
                ANNgridControl.RefreshDataSource();
                gridControl_unboundArts.RefreshDataSource();
                gridControl_wdToBind.RefreshDataSource();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке формы");
                MessageBox.Show($"Ошибка при инициализации формы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Подписываемся обратно на событие ПОСЛЕ загрузки данных
                if (ANNgridView != null)
                {
                    ANNgridView.FocusedRowChanged += gridView3_FocusedRowChanged;
                    // Инициируем первую обработку
                    if (ANNgridView.FocusedRowHandle >= 0)
                    {
                         gridView3_FocusedRowChanged_Internal(ANNgridView, new FocusedRowChangedEventArgs(-1, ANNgridView.FocusedRowHandle));
                    }
                }
            }
        }

        /// <summary>
        /// Обработчик смены активной вкладки
        /// </summary>
        private async void XtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (xtraTabControl1.SelectedTabPage.Name)
            {
                case "TabPage1":// "xtraTabPageWorkDivisions":
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
        private async void gridView_unboundArts_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            gridView_unboundArts_FocusedRowChanged_Internal(sender, e);
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

