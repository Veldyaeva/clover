using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        private readonly TWGridHelper _gridHelper = new TWGridHelper();
        private readonly SplitContainerHelper _splitContainerHelper = new SplitContainerHelper();
        private int bufferId = 0;
        private BindingList<ArtNormN> _bindingList;
        private BindingSource _bindingSource;
        private bool _hasUnsavedChanges = false;
        private BindingSource _nzpByKoddRtSource;
        private BindingList<NZPByKoddRt> _nzpList;
        private BindingList<NormRasz> _normRaszListTW;
        private BindingSource _normRaszBindingSourceTW;
        private BindingList<NormRask> _normRaskListTW;
        private BindingSource _normRaskBindingSourceTW;
        private BindingList<NormKont> _normKontListTW;
        private BindingSource _normKontBindingSourceTW;
        private static List<FioModel> _cachedFioData;
        private List<FioModel> fioList;
        private BindingList<MyDataANN> _preArchList;
        private BindingSource _preArchBindingSource;
        private BindingList<MyDataART> _myDataArtList;
        private BindingSource _myDataArtBindingSource;
        private BindingList<MyDataANN> _myDataAnnList; 
        private BindingSource _myDataAnnBindingSource;

        private BindingList<MyDataART> _boundArtList;
        private BindingSource _boundArtBindingSource;

        // BindingList and BindingSource for NormRasz on Articles tab
        private BindingList<NormRasz> _normRaszListArticles;
        private BindingSource _normRaszBindingSourceArticles;

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

            // Инициализация основных BindingList и BindingSource
            _bindingList = new BindingList<ArtNormN>();
            _bindingSource = new BindingSource { DataSource = _bindingList };
            if (ANNgridControl != null) ANNgridControl.DataSource = _bindingSource;

            _preArchList = new BindingList<MyDataANN>();
            _preArchBindingSource = new BindingSource { DataSource = _preArchList };
            if (gridControlPreArch != null) gridControlPreArch.DataSource = _preArchBindingSource;
            
            _nzpList = new BindingList<NZPByKoddRt>();
            _nzpByKoddRtSource = new BindingSource { DataSource = _nzpList };
            if (gridControlNZP != null) gridControlNZP.DataSource = _nzpByKoddRtSource;

            // Инициализация для вкладки "Работа с артикулами"
            _myDataArtList = new BindingList<MyDataART>();
            _myDataArtBindingSource = new BindingSource { DataSource = _myDataArtList };
            if (gridControl_unboundArts != null) gridControl_unboundArts.DataSource = _myDataArtBindingSource;

            _myDataAnnList = new BindingList<MyDataANN>();
            _myDataAnnBindingSource = new BindingSource { DataSource = _myDataAnnList };
            if (gridControl_wdToBind != null) gridControl_wdToBind.DataSource = _myDataAnnBindingSource;

            _boundArtList = new BindingList<MyDataART>();
            _boundArtBindingSource = new BindingSource { DataSource = _boundArtList };
            if (gridControl_binded != null) gridControl_binded.DataSource = _boundArtBindingSource;
            
            // Initialize BindingList and BindingSource for NormRasz on Articles tab
            _normRaszListArticles = new BindingList<NormRasz>();
            _normRaszBindingSourceArticles = new BindingSource { DataSource = _normRaszListArticles };
            // Assuming customGridControl3 is the correct name for the NormRasz grid on the Articles tab.
            // The user will need to confirm/correct 'customGridControl3' if it's different.
            if (customGridControl3 != null) customGridControl3.DataSource = _normRaszBindingSourceArticles;

            // Настройка гридов (общие настройки, не связанные с данными DataSource)
            if (gridControl_unboundArts != null && gridControl_unboundArts.MainView is GridView unboundArtsView)
            {
                unboundArtsView.OptionsSelection.MultiSelect = false;
                unboundArtsView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
                unboundArtsView.CellValueChanged += (s, e) => GridView_CellValueChanged<MyDataART>(gridControl_unboundArts, e);
                unboundArtsView.CellValueChanging += (s, e) => GridView_CellValueChanged<MyDataART>(gridControl_unboundArts, e); 
            }

            if (gridControl_wdToBind != null && gridControl_wdToBind.MainView is GridView wdToBindView)
            {
                wdToBindView.OptionsSelection.MultiSelect = false;
                wdToBindView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
                wdToBindView.CellValueChanging += (s, e) => GridView_CellValueChanged<MyDataANN>(gridControl_wdToBind, e);
            }
        }

        private async void TeamWorkForm_Load(object sender, EventArgs e)
        {
            if (ANNgridView != null) 
            {
                ANNgridView.FocusedRowChanged -= gridView3_FocusedRowChanged;
            }

            try
            {
                if (gridControl_unboundArts == null || gridControl_wdToBind == null || ANNgridControl == null)
                {
                    throw new InvalidOperationException("Критические компоненты формы не инициализированы.");
                }

                LoadGridSettings();

                // Загрузка данных. Методы LoadWorkDivisions и CurrentWorks_Load (через смену вкладок)
                // должны внутренне обновлять соответствующие BindingList и вызывать ResetBindings(false) 
                // на их BindingSource. Это приведет к обновлению гридов.
                await LoadWorkDivisions(); 
                
                TWGridHelper.sortGridView(ANNgridView);
                // TWGridHelper.sortGridView(gridView4); // gridView4 не используется в текущем контексте напрямую с _bindingSource
                
                kodProizvList = await _dbService.GetListAsync<KodProizvModel>("select kod_proizv, text_proizv from kod_proizv", null); 
                podrVyazList = await _dbService.GetListAsync<PodrVyazModel>("select kod_vyaz, text_vyaz from podr_vyaz", null);
                oborudShvList = await _dbService.GetListAsync<OborudShvModel>("select kod_ob, text_ob from oborud_shv", null);

                // Прямые вызовы RefreshDataSource() здесь обычно не нужны,
                // если методы загрузки данных (LoadWorkDivisions, MyDataArtLoad, MyDataAnnLoad)
                // корректно используют ResetBindings(false) на своих BindingSource.
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке формы TeamWork");
                MessageBox.Show($"Ошибка при инициализации формы TeamWork: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (ANNgridView != null)
                {
                    ANNgridView.FocusedRowChanged += gridView3_FocusedRowChanged;
                    if (ANNgridView.IsFocusedView && ANNgridView.RowCount > 0 && ANNgridView.FocusedRowHandle >=0) // Проверка перед вызовом
                    {
                         gridView3_FocusedRowChanged_Internal(ANNgridView, new FocusedRowChangedEventArgs(-1, ANNgridView.FocusedRowHandle));
                    }
                }
            }
        }

        private async void XtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == null) return;

            switch (e.Page.Name) // Используем e.Page.Name, так как xtraTabControl1.SelectedTabPage может быть еще старым значением
            {
                case "TabPage1": // Убедитесь, что имя вкладки xtraTabPageWorkDivisions действительно "TabPage1"
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

        private async void ButtonArchAndCopyWd_Click(object sender, EventArgs e)
        {
            await ArchAndCopy();
        }

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

        private async void TeamWork_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (_hasUnsavedChanges)
            //{
            //    var result = MessageBox.Show(
            //        "Есть несохраненные изменения. Вы уверены, что хотите выйти?",
            //        "Подтверждение закрытия",
            //        MessageBoxButtons.YesNo,
            //        MessageBoxIcon.Warning);

            //    if (result == DialogResult.No)
            //    {
            //        e.Cancel = true;
            //        return;
            //    }
            //}
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

        private void gridControl_binded_Click(object sender, EventArgs e)
        {

        }

        private void LogMemoryUsage()
        {
            long memory = GC.GetTotalMemory(false);
            Debug.WriteLine($"🔍 Total memory: {memory / 1024} KB");
        }

        private void customSimpleButton1_Click(object sender, EventArgs e)
        {
            //int RzuNom = Convert.ToInt32(this.tbRzuNom.Text);
            //int IsChip = Convert.ToInt32(this.cbIsChip.Checked);
            //PrintMlRtReport report1 = new PrintMlRtReport();
            //report1.RequestParameters = false;
            //report1.Parameters["_rzuNom"].Value = RzuNom;
            //report1.Parameters["_isChip"].Value = IsChip;
            //report1.Parameters["_isUpak"].Value = 0;
            //ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            //reportPrintTool1.ShowPreviewDialog();
        }
    }
}

