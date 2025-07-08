using DevExpress.ChartRangeControlClient.Core;
using DevExpress.CodeParser.VB;
using DevExpress.Data.Filtering;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ButtonPanel;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.form;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BindingSource = System.Windows.Forms.BindingSource;

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
        private BindingSource _nzpByKoddRtSourceArt;
        private BindingSource _nzpByKoddRtSourceWd;
        private BindingList<NZPByKoddRt> _nzpListArt;
        private BindingList<NZPByKoddRt> _nzpListWd;
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

        private CancellationTokenSource _loadCts = new CancellationTokenSource();

        public TeamWork()
        {
            InitializeComponent();
            ANNgridView.OptionsView.ShowPreview = true;
            ANNgridView.PreviewLineCount = 1;
          //  ANNgridView.CalcPreviewText += CalcPreviewText;
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

            _nzpListArt = new BindingList<NZPByKoddRt>();
            _nzpByKoddRtSourceArt = new BindingSource { DataSource = _nzpListArt };
            if (gridControlNZP != null) gridControlNZP.DataSource = _nzpByKoddRtSourceArt;

            _nzpListWd = new BindingList<NZPByKoddRt>();
            _nzpByKoddRtSourceWd = new BindingSource { DataSource = _nzpListWd };
            if (customGridControl4 != null) customGridControl4.DataSource = _nzpByKoddRtSourceWd;

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

            _normRaszListArticles = new BindingList<NormRasz>();
            _normRaszBindingSourceArticles = new BindingSource { DataSource = _normRaszListArticles };
            if (customGridControl3 != null) customGridControl3.DataSource = _normRaszBindingSourceArticles;

            // Настройка гридов (общие настройки, не связанные с данными DataSource)
            if (gridControl_unboundArts != null && gridControl_unboundArts.MainView is GridView unboundArtsView)
            {
                unboundArtsView.OptionsSelection.MultiSelect = false;
                unboundArtsView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
                unboundArtsView.CellValueChanged += (s, e) => GridView_CellValueChanged<MyDataART>(gridControl_unboundArts, e);
                unboundArtsView.CellValueChanging += (s, e) => GridView_CellValueChanged<MyDataART>(gridControl_unboundArts, e);
            }
        //    ANNgridView.CalcPreviewText += CalcPreviewText;

            if (gridControl_wdToBind != null && gridControl_wdToBind.MainView is GridView wdToBindView)
            {
                wdToBindView.OptionsSelection.MultiSelect = false;
                wdToBindView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
                wdToBindView.CellValueChanging += (s, e) => GridView_CellValueChanged<MyDataANN>(gridControl_wdToBind, e);
            }

            var commandsEditDateNull = new RepositoryItemButtonEdit { TextEditStyle = TextEditStyles.HideTextEditor };
            commandsEditDateNull.Buttons.Clear();
            commandsEditDateNull.Buttons.Add(new EditorButton(ButtonPredefines.Glyph, "Проставить дату", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleLeft, DemoHelper.GetEditImage()));
            ////кнопка "Проставить дату обн"
       //     commandsEditDateNull.ButtonClick += CommandsEdit_ButtonClick;
            commandsEditDateNull.DoubleClick -= CommandsEditDateNull_DoubleClick;
            commandsEditDateNull.DoubleClick += CommandsEditDateNull_DoubleClick;
            GridColumn Updated = ANNgridView.Columns["dateUpdate"];
            // Updated.ColumnEdit = commandsEditDateNull;

            // Репозиторий для отображения только текста
            var commandsEditDateText = new RepositoryItemTextEdit();
            commandsEditDateText.ReadOnly = true;
            
            GridColumn colDateUpdate = ANNgridView.Columns["dateUpdate"];
            
            // Устанавливаем формат отображения даты без времени
            colDateUpdate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            colDateUpdate.DisplayFormat.FormatString = "dd.MM.yyyy";
            
            ANNgridView.CustomRowCellEdit += (s, e) =>
            {
                if (e.Column == colDateUpdate)
                {
                    var dateUpdate = ANNgridView.GetRowCellValue(e.RowHandle, "dateUpdate");
                    if (dateUpdate == null || string.IsNullOrEmpty(dateUpdate.ToString()))
                        e.RepositoryItem = commandsEditDateNull;
                    else e.RepositoryItem = commandsEditDateText;
                }
            };
            ANNgridView.CalcPreviewText += CalcPreviewText;
        }

        private async void CommandsEditDateNull_DoubleClick(object sender, EventArgs e)
        {
            var view = ANNgridView;
            var rowHandle = view.FocusedRowHandle;
            var dateUpdate = view.GetRowCellValue(rowHandle, "dateUpdate");
            var annId = view.GetRowCellValue(rowHandle, "AnnID");
            // Действие только если дата не задана
            if (dateUpdate == null || dateUpdate == DBNull.Value || string.IsNullOrEmpty(dateUpdate.ToString()))
            {
                var result = MessageBox.Show("Обновить данные во всех справочниках?",
     "Пересчёт себ.",
     MessageBoxButtons.YesNo,
     MessageBoxIcon.Question,
     MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    var parameters = new Dictionary<string, object>
                    {
                        { "@xAnnID", annId}
                    };
                    await _dbHelper.ExecuteQueryAsync("EXEC dbo.updateSebZArticulPsz @xAnnID", parameters); //пересчитать себ. - при простановке даты обн

                    view.RefreshRowCell(rowHandle, view.Columns["dateUpdate"]);

                    await _dbService.UpdateFieldAsync(TableNames.Ann, "data_obn", DateTime.Now, TableNames.AnnId, annId);
                    await _dbService.UpdateFieldAsync(TableNames.Ann, "status", (int)Status.Actual, TableNames.AnnId, annId);
                    view.SetRowCellValue(rowHandle, "dateUpdate", DateTime.Now);
                    view.SetRowCellValue(rowHandle, "status", (int)Status.Actual);
                    view.SetRowCellValue(rowHandle, "StatusText", "Актуальное");

                    setDateUpdate();
                }
            }
        }

        private async void CommandsEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
        }

        private void setDateUpdate()
        {
        }

        private void CalcPreviewText(object sender,
                                       CalcPreviewTextEventArgs e)
        {
            e.PreviewText = "Тест превью";
            //var row = e.Row as ArtNormN;
            //if (row == null) return;

            //var parts = new List<string>();

            //if (!string.IsNullOrWhiteSpace(row.Komment))
            //    parts.Add(row.Komment);

            //// выводим всегда
            //parts.Add($"Дизайнер: {row.Diz}, конструктор: {row.Constr}");

            //if (!string.IsNullOrWhiteSpace(row.Reco))
            //    parts.Add($"Рекомендация: {row.Reco}");
            //if (!string.IsNullOrWhiteSpace(row.Komment))
            //    parts.Add($"Комментарий: {row.Komment}");

            //e.PreviewText = string.Join(Environment.NewLine, parts);
        }

        private async void TeamWorkForm_Load(object sender, EventArgs e)
        {
            if (ANNgridView != null)
            {
                ANNgridView.FocusedRowChanged -= ANNgridView_FocusedRowChanged;
                //ANNgridView.CellValueChanged -= ANNgridView_CellValueChanged;
               // ANNgridView.CellValueChanging -= ANNgridView_CellValueChanging;
            }

            try
            {
                if (gridControl_unboundArts == null || gridControl_wdToBind == null || ANNgridControl == null)
                {
                    throw new InvalidOperationException("Критические компоненты формы не инициализированы.");
                }

                LoadGridSettings();
                await LoadWorkDivisions();

                TWGridHelper.sortGridView(ANNgridView);

                kodProizvList = await _dbService.GetListAsync<KodProizvModel>("select kod_proizv, text_proizv from kod_proizv", null);
                podrVyazList = await _dbService.GetListAsync<PodrVyazModel>("select kod_vyaz, text_vyaz from podr_vyaz", null);
                oborudShvList = await _dbService.GetListAsync<OborudShvModel>("select ko_ob_all as kod_ob, text_ob from oborud_shv_ob", null);
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
                    ANNgridView.FocusedRowChanged += ANNgridView_FocusedRowChanged;
                  //  ANNgridView.CellValueChanged += ANNgridView_CellValueChanged;
                 //   ANNgridView.CellValueChanging += ANNgridView_CellValueChanging;
                    if (ANNgridView.IsFocusedView && ANNgridView.RowCount > 0 && ANNgridView.FocusedRowHandle >= 0) // Проверка перед вызовом
                    {
                        ANNgridView_FocusedRowChanged_Internal(ANNgridView, new FocusedRowChangedEventArgs(-1, ANNgridView.FocusedRowHandle));
                    }
                }
            }
        }

        private async void XtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == null) return;

            switch (e.Page.Name)
            {
                case "TabPage1":
                    await LoadWorkDivisions();
                    break;

                case "xtraTabPageArticles":
                    await CurrentWorks_Load();
                    break;
            }
        }


        private void ButtonEditWd_Click(object sender, EventArgs e)
        {
            EditWd_Internal2(ANNgridView, _bindingList, _bindingSource);
        }

        private async void ButtonArchAndCopyWd_Click(object sender, EventArgs e)
        {
            await ArchAndCopy(ANNgridView, _bindingList, _bindingSource, false);

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
        /// <summary>
        /// Обрабатывает смену строки в неувязанных артикулах
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void gridView_unboundArts_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            gridView_unboundArts_FocusedRowChanged_Internal(sender, e);
        }
        private void Filter_CheckedChanged(object sender, EventArgs e)
        {
            Filter_CheckedChanged_Internal(sender, e);
        }
        private async void search_CheckedChanged(object sender, EventArgs e)
        { //search_CheckedChanged_Internal(sender, e);
        }
        private void gridControl2_Leave(object sender, EventArgs e)
        {
            gridControl2_Leave_Internal(sender, e);
        }
        private async void gridControl2_GotFocus(object sender, EventArgs e)
        { gridControl2_GotFocus_Internal(sender, e); }

        private void searchControl1_QueryIsSearchColumn(object sender, DevExpress.XtraEditors.QueryIsSearchColumnEventArgs args)
        {
            //searchControl1_QueryIsSearchColumn_Internal(sender, args);
        }

        private void SearchButton_Click(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            // SearchButton_Click_Internal(sender, e);
        }

        private void ANNgridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            ANNgridView_FocusedRowChanged_Internal(sender, e);
        }
        private async void customButton12_Click(object sender, EventArgs e)
        {// customButton12_Click_Internal(sender, e);
        }
        private async void loadAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            loadAllCheckBox_CheckedChanged_Internal(sender, e);
        }

        private void searchControl1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    SearchControl searchControl = sender as SearchControl;
            //    if (searchControl != null)
            //    {
            //        // Simulate a click on the search button.
            //        // We need to find the actual search button in the SearchControl's buttons collection.
            //        EditorButton searchButton = searchControl.Properties.Buttons.OfType<EditorButton>().FirstOrDefault(b => b.Kind == ButtonPredefines.Search);// || b.IsDefault);
            //        if (searchButton != null)
            //        {
            //            SearchButton_Click_Internal(searchControl, new ButtonPressedEventArgs(searchButton));
            //        }
            //        else
            //        {
            //            // Fallback if a specific search button isn't found, try with a general non-clear button.
            //            EditorButton firstNonClearButton = searchControl.Properties.Buttons.OfType<EditorButton>().FirstOrDefault(b => b.Kind != ButtonPredefines.Clear);
            //            if (firstNonClearButton != null)
            //            {
            //                SearchButton_Click_Internal(searchControl, new ButtonPressedEventArgs(firstNonClearButton));
            //            }
            //        }
            //    }
            //    e.Handled = true;
            //    e.SuppressKeyPress = true;
            //}
        }

        private async void simpleButton2_Click(object sender, EventArgs e)
        {
            simpleButton2_Click_Internal(sender, e);
        }

        private async void TeamWork_FormClosing(object sender, FormClosingEventArgs e)
        {
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


        private async void customSimpleButton1_Click(object sender, EventArgs e)
        {
            await DuplicateWorkDivision_Click_Internal(sender, e);
        }

        private async Task DuplicateWorkDivision_Click_Internal(object sender, EventArgs e)
        {
            if (ANNgridView == null || ANNgridView.FocusedRowHandle < 0)
            {
                MessageBox.Show("Выберите Разделение Труда для дублирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedAnnToDuplicate = ANNgridView.GetRow(ANNgridView.FocusedRowHandle) as ArtNormN;
            if (selectedAnnToDuplicate == null)
            {
                MessageBox.Show("Не удалось получить данные выбранного РТ.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ArtNormN CopyedWorkDivisionShell = selectedAnnToDuplicate.CloneProperties();
            CopyedWorkDivisionShell.Status = (int)Status.Preliminary;
            CopyedWorkDivisionShell.StatusText = StatusHelper.GetStatusText((int)Status.Preliminary);
            CopyedWorkDivisionShell.dateCreate = DateTime.Now;
            CopyedWorkDivisionShell.dateUpdate = null;
            CopyedWorkDivisionShell.Arh = false;
            CopyedWorkDivisionShell.ParentId = selectedAnnToDuplicate.AnnID;
            CopyedWorkDivisionShell.AnnID = 0;
            CopyedWorkDivisionShell.Mod = "";
            CopyedWorkDivisionShell.Articul = "";
            CopyedWorkDivisionShell.grup = "";

            int newAnnId = await _dbService.InsertEntityAsync(TableNames.Ann, TableNames.AnnId, CopyedWorkDivisionShell);
            if (newAnnId <= 0)
            {
                MessageBox.Show("Ошибка при создании новой записи РТ в базе данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var teamWorkAdvanceTW = new TeamWork_AdvanceTW(bufferId, (int)Mode.Clone, oldId: selectedAnnToDuplicate.AnnID, newId: newAnnId))
            {
                DialogResult result = teamWorkAdvanceTW.ShowDialog();
                if (result == DialogResult.OK)
                {
                    CopyedWorkDivisionShell = teamWorkAdvanceTW.CreatedAnn;
                    if (CopyedWorkDivisionShell == null) return;

                    // object updatedDataAnn =  updatedArtNormN;

                    //    int annIdToFind = forMyDataAnnView ? ((MyDataANN)updatedDataAnn).AnnID : ((ArtNormN)updatedDataAnn).AnnID;

                    //    int index = list.Cast<object>().Select((item, i) => new { item, i }).FirstOrDefault(x => forMyDataAnnView
                    //            ? x.item is MyDataANN mda && mda.AnnID == annIdToFind
                    //            : x.item is ArtNormN an && an.AnnID == annIdToFind)
                    //        ?.i ?? -1;

                    //    if (index >= 0)
                    //        list[index] = updatedDataAnn;

                    _bindingList.Add(CopyedWorkDivisionShell);
                    _bindingSource.ResetBindings(false);
                    int rowHandle = ANNgridView.LocateByValue("AnnID", CopyedWorkDivisionShell.AnnID);
                    if (rowHandle >= 0)
                    {
                        ANNgridView.BeginUpdate();
                        try
                        {
                            ANNgridView.FocusedRowHandle = rowHandle;
                            ANNgridView.RefreshRow(rowHandle);
                        }
                        finally
                        {
                            ANNgridView.EndUpdate();
                        }
                    }

                    //    // Если редактирование было для второй вкладки (MyDataANN view), обновим NormRasz для customGridControl3
                    //    if (forMyDataAnnView && updatedArtNormN != null && updatedArtNormN.AnnID > 0)
                    //    {
                    //        await RefreshNormRaszForArticlesTab(updatedArtNormN.AnnID);
                    //    }
                    //    else if (!forMyDataAnnView && updatedArtNormN != null && updatedArtNormN.AnnID > 0) // Иначе, если для первой вкладки
                    //    {
                    //        await LoadRelatedData(updatedArtNormN.AnnID); // Загружаем связанные данные для первой вкладки (НЗП, раскрой, контроль)
                    //    }
                    //}
                }

            }
        }
        private void layoutControlGroup2_CustomButtonClick(object sender, BaseButtonEventArgs e)
        {
            int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);

            switch (buttonIndex)
            {
                case 2:
                    ButtonPreliminaryWd_Click_Internal(sender, e); // Первая кнопка
                    break;
                case 4:
                    EditWd_Internal2(gridView_wdToBind, _myDataAnnList, _myDataAnnBindingSource, forMyDataAnnView: true);
                    break;
                case 6:
                    ArchAndCopy(gridView_wdToBind, _myDataAnnList, _myDataAnnBindingSource, true); // Третья кнопка
                    break;

            }
        }

        private void ButtonUnboundWd_Click(object sender, EventArgs e)
        {
            UnboundWD(sender, e);
        }

        private async void ANNgridView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Upd")
            {
                GridView view = sender as GridView;
                if (view != null)
                {
                    ArtNormN row = view.GetRow(e.RowHandle) as ArtNormN;
                    if (row != null)
                    {
                        if (e.Value is bool val && val)
                        {
                            row.dateUpdate = DateTime.Now;
                            _hasUnsavedChanges = true;

                            try
                            {
                                decimal updatedSeb = await _artNormService.getArtNormnSeb(row.AnnID);
                                row.Seb = updatedSeb;
                                row.dateUpdate = DateTime.Now;
                            }
                            catch (Exception ex)
                            {
                                await _logger.LogErrorAsync(ex, $"Ошибка при вызове getArtNormnSeb для AnnID: {row.AnnID}");
                                MessageBox.Show("Ошибка при обновлении данных после вызова процедуры: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            view.RefreshRow(e.RowHandle);
                        }
                    }
                }
            }
        }

        //private void ANNgridView_CellValueChanging(object sender, CellValueChangedEventArgs e)
        //{
        //    if (e.Column.FieldName == "Upd")
        //    {
        //        GridView view = sender as GridView;
        //        if (view != null)
        //        {
        //            ArtNormN row = view.GetRow(e.RowHandle) as ArtNormN;
        //            if (row != null && row.dateUpdate.HasValue && e.Value is bool val && !val)
        //            {
        //                view.SetRowCellValue(e.RowHandle, e.Column, true);
        //                MessageBox.Show("Нельзя снять отметку 'обн.', если дата обновления уже установлена.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }
        //        }
        //    }
        //}

        private void layoutControlGroup8_CustomButtonClick(object sender, BaseButtonEventArgs e)
        {
            int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);

            switch (buttonIndex)
            {
                case 0:
                    ButtonPreliminaryWd_Click_Internal(sender, e);
                    break;
                case 2:
                    EditWd_Internal2(ANNgridView, _bindingList, _bindingSource);
                    break;
                case 4:
                    DuplicateWorkDivision_Click_Internal(sender, e);
                    break;
                case 6:
                    ArchAndCopy(ANNgridView, _bindingList, _bindingSource, false);
                    break;

            }
        }

        private void ANNgridView_CalcPreviewText(object sender, CalcPreviewTextEventArgs e)
        {
            if (e.RowHandle >= 0 && ANNgridView.GetRow(e.RowHandle) is ArtNormN row)
            {
                e.PreviewText = $"Дизайнер: {row.Diz}, Конструктор: {row.Constr}, Особенности: {row.Komment}, Рекомендации: {row.Reco}";
            }
        }

        private void layoutControlGroup6_CustomButtonClick(object sender, BaseButtonEventArgs e)
        {
            simpleButton2_Click_Internal(sender, e); // Четвертая кнопка  создать из артикула
                                                     // break;
        }

        private void layoutControlGroup14_CustomButtonClick(object sender, BaseButtonEventArgs e)
        {
            int buttonIndex = ((DevExpress.XtraLayout.LayoutControlGroup)sender).CustomHeaderButtons.IndexOf(e.Button);

            switch (buttonIndex)
            {
                case 0:
                    BindButton_Click_Internal(sender, e);// увязать
                    break;
                case 2:
                    UnboundWD(sender, e);
                    break;
            }
        }

        private void gridControl_wdToBind_Click(object sender, EventArgs e)
        {

        }

    //    private void ANNgridView_CalcPreviewText_1(object sender, CalcPreviewTextEventArgs e)
    //    {

    //        var row = e.Row as ArtNormN;
    //        if (row == null) return;

    //        var parts = new List<string>();

    //        if (!string.IsNullOrWhiteSpace(row.Komment))
    //            parts.Add(row.Komment);

    //        // выводим всегда
    //        parts.Add($"Дизайнер: {row.Diz}, конструктор: {row.Constr}");

    //        if (!string.IsNullOrWhiteSpace(row.Reco))
    //            parts.Add($"Рекомендация: {row.Reco}");
    //        if (!string.IsNullOrWhiteSpace(row.Komment))
    //            parts.Add($"Комментарий: {row.Komment}");

    //        e.PreviewText = string.Join(Environment.NewLine, parts);


    //    }
    }

}
public static class DemoHelper
{

    public static Image GetDeleteImage()
    {
        return GetImage(Brushes.Red);
    }

    public static Image GetEditImage()
    {
        return GetImage(Brushes.Green);
    }

    public static Image GetImage(Brush b)
    {
        Image img = new Bitmap(16, 16);
        using (Graphics g = Graphics.FromImage(img))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillEllipse(b, new Rectangle(0, 0, img.Width - 1, img.Height - 1));
        }
        return img;
    }
}
   