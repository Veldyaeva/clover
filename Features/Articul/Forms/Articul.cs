using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.ButtonsPanelControl;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraReports.UI;
using SewingProduction.Core.Class;
using SewingProduction.Core.Forms;
using SewingProduction.Core.helpers;
using SewingProduction.Core.services;
using SewingProduction.Features.Articul.Forms;
using SewingProduction.Features.Articul.Helpers;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.Articul.Service;
using SewingProduction.Features.CuttingProduction.Models;
using SewingProduction.Features.UserDistribution.Class;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Report;
using SewingProduction.Services;
using BindingSource = System.Windows.Forms.BindingSource;
using DataTable = System.Data.DataTable;

//using DataTable = DevExpress.DataAccess.Native.Data.DataTable;

namespace SewingProduction.Features.Articul
{
    public partial class Articul : CustomForm
    {
        private readonly DatabaseHelperSQL _dbHelperAce;

        private readonly DbService _dbService;
        private UserClass _currentUser;
        private readonly ILogger _logger = new FileLogger();
        private const string LoggerContext = "Articul";
        //все поля таблицы Артикул
        KomplDataService komplService = new KomplDataService();

        private bool _isInitialized;


        private readonly BindingSource bsPreview = new(); // для грида
        private readonly BindingSource bsDetails = new(); // для карточки/деталей
        private readonly BindingList<SpArtPreviewModel> _previewList = new();
        private int _loadVersion = 0;

        ArticulDataService _articulDataService = new ArticulDataService();

        private void LogSuccess(string message, string scope)
        {
            _ = SafeLogAsync(() => _logger.LogEventAsync(message, $"{LoggerContext}.{scope}"));
        }

        private void LogWarning(string message, string scope)
        {
            _ = SafeLogAsync(() => _logger.LogWarningAsync(message, $"{LoggerContext}.{scope}"));
        }

        private void LogError(Exception ex, string scope)
        {
            _ = SafeLogAsync(() => _logger.LogErrorAsync(ex, $"{LoggerContext}.{scope}"));
        }

        private static async Task SafeLogAsync(Func<Task> writeLog)
        {
            try
            {
                await writeLog().ConfigureAwait(false);
            }
            catch
            {
                // Логирование не должно мешать работе формы.
            }
        }

        public Articul(UserClass user) : base(user)
        {
            _dbHelperAce = new DatabaseHelperSQL();
            _dbService = new DbService(_dbHelperAce);
            InitializeComponent();
            InitialiseEmptyZero();
            _currentUser = user;
            InitHeaderButtonTags();
        }
        private async Task RefreshArtPreviewAsync()
        {
            var selectedKod = (bsPreview.Current as SpArtPreviewModel)?.Kod;
            var selectedKodd = (bsPreview.Current as SpArtPreviewModel)?.Kodd;

            await ReloadPreviewAsync();

            if (_previewList.Count == 0)
            {
                ArticulControlBindingHelper.ClearDetails(bsDetails);
                articulControl1.ClearImage();
                bsArtDr.DataSource = null;
                bsSostKompl.DataSource = null;
                bsSostNabor.DataSource = null;
                UpdateTabsVisibility();
                return;
            }

            var index = -1;
            if (!string.IsNullOrWhiteSpace(selectedKod))
            {
                index = _previewList
                    .ToList()
                    .FindIndex(x => x.Kod == selectedKod && x.Kodd == selectedKodd);

                if (index < 0)
                    index = _previewList.ToList().FindIndex(x => x.Kod == selectedKod);
            }

            if (index < 0) index = 0;

            gridControl1.FocusedRowChanged -= gridControl1_FocusedRowChanged;
            try
            {
                bsPreview.Position = index;
            }
            finally
            {
                gridControl1.FocusedRowChanged += gridControl1_FocusedRowChanged;
            }

            if (bsPreview.Current is SpArtPreviewModel current)
                await LoadArticulAsync(current.Kod, current.Kodd);
        }
        private async void Articul_Load(object sender, EventArgs e)
        {
            try
            {
                if (_isInitialized) return;

                gridControl1.GridControl.DataSource = bsPreview;
                bsPreview.DataSource = _previewList;

                // карточка артикула
                articulControl1.BindTo(bsDetails);
                articulControl1.IsReadOnly = true;

                //привязываем правую панель
                InitializeBindings();
                AttachGridCopyContextMenus();

                //обновляем состояние кнопки архива при загрузке
                SyncArchiveButtonCaption();
                _isInitialized = true;

                await RefreshArtPreviewAsync();
                LogSuccess("Форма инициализирована и превью загружено.", nameof(Articul_Load));
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(Articul_Load));
            }
        }

        private void AttachGridCopyContextMenus()
        {
            // Унифицированное ПКМ-меню "Копировать значение ячейки" для гридов.
            gridControl1.PopupMenuShowing -= GridCopyPopupMenuShowing;
            gridControl1.PopupMenuShowing += GridCopyPopupMenuShowing;

            gridView1.PopupMenuShowing -= GridCopyPopupMenuShowing;
            gridView1.PopupMenuShowing += GridCopyPopupMenuShowing;

            // gridArt has 2 views (gridControl1 and gridView3)
            gridView3.PopupMenuShowing -= GridCopyPopupMenuShowing;
            gridView3.PopupMenuShowing += GridCopyPopupMenuShowing;

            gridViewKomplSost.PopupMenuShowing -= GridCopyPopupMenuShowing;
            gridViewKomplSost.PopupMenuShowing += GridCopyPopupMenuShowing;

            // cGridKomplSost has 2 views (gridViewKomplSost and gridView2)
            gridView2.PopupMenuShowing -= GridCopyPopupMenuShowing;
            gridView2.PopupMenuShowing += GridCopyPopupMenuShowing;

            gridViewNaborSost.PopupMenuShowing -= GridCopyPopupMenuShowing;
            gridViewNaborSost.PopupMenuShowing += GridCopyPopupMenuShowing;
        }

        private void GridCopyPopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            GridContextMenuHelper.AddCopyCellMenuItem(sender, e);
        }

        private void InitialiseEmptyZero()
        {
            try
            {

                #region Затраты на изготовление
                txbNormt.ShowEmptyWhenZero();


                // нормы/себестоимость/брак и назначение полотна
                EmptyZeroByRange("txbNorm_t");
                EmptyZeroByRange("txbTkanSeb_t");
                EmptyZeroByRange("txbBrak");
                EmptyZeroByRange("txtBrakPercent");
                EmptyZeroByRange("txbKfKach");
                //void EmptyZeroByPrefix(string prefix)
                //{
                //    var edits = this.Controls
                //        .Find("", true)
                //        .OfType<DevExpress.XtraEditors.TextEdit>()
                //        .Where(c => c.Name.StartsWith(prefix));

                //    foreach (var edit in edits)
                //        edit.ShowEmptyWhenZero();
                //}
                void EmptyZeroByRange(string prefix, int from = 1, int to = 7)
                {
                    for (int i = from; i <= to; i++)
                    {
                        string controlName = $"{prefix}{i}";

                        var edit = this.Controls.Find(controlName, true).FirstOrDefault() as DevExpress.XtraEditors.TextEdit;
                        edit?.ShowEmptyWhenZero();
                    }
                }              
                #endregion

                #region брак
                txbBrakAll.ShowEmptyWhenZero();
                txbSeb.ShowEmptyWhenZero();

                #endregion
                #region Норма/сек + зарплата 

                txbSek.ShowEmptyWhenZero();
                txbSekVyaz.ShowEmptyWhenZero();
                txbSekShv.ShowEmptyWhenZero();
                txbSekKr.ShowEmptyWhenZero();
                //зарплатаShowEmptyWhenZero();
                txbSumZarpl.ShowEmptyWhenZero();
                txbSumDopOpl.ShowEmptyWhenZero();
                txbSumStrVznos.ShowEmptyWhenZero();
                txbSumSebRaskr.ShowEmptyWhenZero();
                txbSumKomplNum.ShowEmptyWhenZero();
                txbSebRecom.ShowEmptyWhenZero();
                txbCalcSebRecom.ShowEmptyWhenZero();
                #endregion

                #region коэфициенты

                txbSebDop.ShowEmptyWhenZero();
                txbKoefPr.ShowEmptyWhenZero();
                txbKoefVedDG.ShowEmptyWhenZero();
                txbSebProizv.ShowEmptyWhenZero();
                txbKoef.ShowEmptyWhenZero();
                #endregion

            }
            catch (Exception ex)
            {
                LogError(ex, nameof(InitializeBindings));
                throw;
            }
        }

        private async Task ReloadPreviewAsync()
        {
            gridControl1.ShowLoadingPanel();
            try
            {
                var list = await _articulDataService.GetArtPreviewAsyncBindingList();
                bsPreview.DataSource = list;

                /*bsPreview.RaiseListChangedEvents = false;
                try
                {
                    _previewList.Clear();
                    foreach (var it in list)
                        _previewList.Add(it);
                }
                finally
                {
                    bsPreview.RaiseListChangedEvents = true;
                    bsPreview.ResetBindings(false);
                }
                */
            }
            finally
            {
                gridControl1.HideLoadingPanel();
            }
        }
        private void InitializeBindings()
        {
            try
            {
                // включить редактирование (если нужно)
                // articulControl1.IsReadOnly = false;

                // (желательно) чтобы при повторном вызове не плодились биндинги
                chbIsUpak.DataBindings.Clear();
                chbIsFurnit.DataBindings.Clear();
                chkP.DataBindings.Clear();
                chkV.DataBindings.Clear();
                chkBus.DataBindings.Clear();
                chkStra.DataBindings.Clear();
                chkPres.DataBindings.Clear();
                txbNormt.DataBindings.Clear();
                txbSek.DataBindings.Clear();
                txbSekVyaz.DataBindings.Clear();
                txbSekShv.DataBindings.Clear();
                txbSekKr.DataBindings.Clear();
                txbSumZarpl.DataBindings.Clear();
                txbSumDopOpl.DataBindings.Clear();
                txbSumStrVznos.DataBindings.Clear();
                txbSumSebRaskr.DataBindings.Clear();
                txbSumKomplNum.DataBindings.Clear();
                txbSebDop.DataBindings.Clear();
                txbKoefPr.DataBindings.Clear();
                txbKoefVedDG.DataBindings.Clear();
                txbSebProizv.DataBindings.Clear();
                txbKoef.DataBindings.Clear();
                txbBrakAll.DataBindings.Clear();

                #region галки с отделками
                //галки вяз отделки
                //архив
                //отделка
                chbIsUpak.DataBindings.Add("Checked", bsDetails, nameof(SpArticulPreviewModel.Is_upak), true);
                //chbIsUpak.DataBindings.Add("Checked", bsDetails, nameof(SpArticulPreviewModel.Is_upak), true, DataSourceUpdateMode.OnPropertyChanged);
                chbIsFurnit.DataBindings.Add("Checked", bsDetails, nameof(SpArticulPreviewModel.Is_furnit), true);

                chkP.DataBindings.Add("Checked", bsDetails, nameof(SpArticulPreviewModel.P), true);
                chkV.DataBindings.Add("Checked", bsDetails, nameof(SpArticulPreviewModel.V), true);
                chkBus.DataBindings.Add("Checked", bsDetails, nameof(SpArticulPreviewModel.Bus), true);
                chkStra.DataBindings.Add("Checked", bsDetails, nameof(SpArticulPreviewModel.Stra), true);
                chkPres.DataBindings.Add("Checked", bsDetails, nameof(SpArticulPreviewModel.P_pres), true);
                #endregion

                #region Затраты на изготовление
                txbNormt.DataBindings.Add("Text", bsDetails, nameof(SpArticulPreviewModel.Norm_t), true, DataSourceUpdateMode.Never);

                // TODO: добавить расчет полной с\ст на изделие по коду 
                //   txbSeb.DataBindings.Add("Text", bsArticul, nameof(SpArticulPreviewModel.Seb), true, DataSourceUpdateMode.Never);

                // нормы/себестоимость/брак и назначение полотна
                BindTextBoxesBySuffix(customLayoutControl1, bsDetails, "txbNorm_t", "Norm_t", "F2", true);
                BindTextBoxesBySuffix(customLayoutControl1, bsDetails, "txbTkanSeb_t", "Seb_t");
                BindTextBoxesBySuffix(customLayoutControl1, bsDetails, "txbBrak", "Brak_t", "F2", true);
                BindTextBoxesBySuffix(customLayoutControl1, bsDetails, "txtBrakPercent", "Brak_percent");
                BindTextBoxesBySuffix(customLayoutControl1, bsDetails, "txbKfKach", "Kf_tkan_kach", "F2");
                BindTextBoxesBySuffix(customLayoutControl1, bsDetails, "txbOpis_t", "Opis_t");
                BindTextBoxesBySuffix(customLayoutControl1, bsDetails, "tkb", "Tkb");

                #endregion

                #region брак
                txbBrakAll.DataBindings.Add("Text", bsDetails, nameof(SpArticulPreviewModel.Brak_avg), true, DataSourceUpdateMode.Never);
                txbSeb.DataBindings.Add("Text", bsDetails, nameof(SpArticulPreviewModel.Cena_prdc), true, DataSourceUpdateMode.Never);

                #endregion
                #region Норма/сек + зарплата 

                txbSek.DataBindings.Add("Text", bsDetails, nameof(SpArticulPreviewModel.Sek), true, DataSourceUpdateMode.Never);
                txbSekVyaz.DataBindings.Add("Text", bsDetails, nameof(SpArticulPreviewModel.Sek_vyaz), true, DataSourceUpdateMode.Never);
                txbSekShv.DataBindings.Add("Text", bsDetails, nameof(SpArticulPreviewModel.Sek_shv), true, DataSourceUpdateMode.Never);
                txbSekKr.DataBindings.Add("Text", bsDetails, nameof(SpArticulPreviewModel.Sek_kr), true, DataSourceUpdateMode.Never);
                //зарплата
                txbSumZarpl.DataBindings.Add("Text", bsDetails, nameof(SpArticulPreviewModel.Sum_zarpl), true, DataSourceUpdateMode.Never);
                txbSumDopOpl.DataBindings.Add("Text", bsDetails, nameof(SpArticulPreviewModel.Sum_dopopl), true, DataSourceUpdateMode.Never);
                txbSumStrVznos.DataBindings.Add("Text", bsDetails, nameof(SpArticulPreviewModel.Sum_strvznos), true, DataSourceUpdateMode.Never);
                txbSumSebRaskr.DataBindings.Add("Text", bsDetails, nameof(SpArticulPreviewModel.Sum_sebraskr), true, DataSourceUpdateMode.Never);
                txbSumKomplNum.DataBindings.Add("Text", bsDetails, nameof(SpArticulPreviewModel.Sum_komplnum), true, DataSourceUpdateMode.Never);
                txbSebRecom.DataBindings.Add("Text", bsDetails, nameof(SpArticulPreviewModel.Seb_rekom), true, DataSourceUpdateMode.Never);
                txbCalcSebRecom.DataBindings.Add("Text", bsDetails, nameof(SpArticulPreviewModel.Calc_seb_rekom), true, DataSourceUpdateMode.Never);
                txbSebz.DataBindings.Add("Text", bsDetails, nameof(SpArticulPreviewModel.Seb_z), true, DataSourceUpdateMode.Never);
                #endregion

                #region коэфициенты

                txbSebDop.DataBindings.Add("Text", bsDetails, nameof(SpArticulPreviewModel.Seb_dop), true, DataSourceUpdateMode.Never);
                txbKoefPr.DataBindings.Add("Text", bsDetails, nameof(SpArticulPreviewModel.Koef_pr), true, DataSourceUpdateMode.Never);
                txbKoefVedDG.DataBindings.Add("Text", bsDetails, nameof(SpArticulPreviewModel.Koef_d), true, DataSourceUpdateMode.Never);
                txbSebProizv.DataBindings.Add("Text", bsDetails, nameof(SpArticulPreviewModel.Seb_proizv), true, DataSourceUpdateMode.Never);
                txbKoef.DataBindings.Add("Text", bsDetails, nameof(SpArticulPreviewModel.Koef), true, DataSourceUpdateMode.Never);
                #endregion

            }
            catch (Exception ex)
            {
                LogError(ex, nameof(InitializeBindings));
                throw;
            }

        }

        private static void BindTextBoxesBySuffix(
            Control container,
            BindingSource source,
            string controlNamePrefix,
            string propertyPrefix,
            string format = null,
            bool formatCurrentText = false)
        {
            var modelType = typeof(SpArticulPreviewModel);
            var candidates = 0;
            var bound = 0;
            var skippedNoSuffix = 0;
            var skippedMissingProperty = 0;

            foreach (Control control in GetAllControls(container))
            {
                if (string.IsNullOrWhiteSpace(control.Name)) continue;
                if (!control.Name.StartsWith(controlNamePrefix, StringComparison.Ordinal)) continue;
                candidates++;

                var suffix = GetNumericSuffix(control.Name);
                if (suffix == null)
                {
                    skippedNoSuffix++;
                    Debug.WriteLine($"[BindTextBoxesBySuffix] No numeric suffix for control: {control.Name}");
                    continue;
                }

                var expectedPropertyName = propertyPrefix + suffix;
                var modelProperty = modelType.GetProperty(
                    expectedPropertyName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);

                if (modelProperty == null)
                {
                    skippedMissingProperty++;
                    Debug.WriteLine($"[BindTextBoxesBySuffix] Property not found: {expectedPropertyName} for control {control.Name}");
                    continue;
                }

                var propertyName = modelProperty.Name;

                var bindProperty = "Text";
                if (control is DevExpress.XtraEditors.BaseEdit)
                    bindProperty = "EditValue";

                control.DataBindings.Clear();
                control.DataBindings.Add(
                    bindProperty,
                    source,
                    propertyName,
                    true,
                    DataSourceUpdateMode.Never,
                    null,
                    format);

                //if (formatCurrentText && control is TextEdit tb &&// || ( formatCurrentText && control is TextEdit tb )&&
                //    decimal.TryParse(tb.Text, out var value))
                //{
                //    tb.Text = value.ToString("F2");
                //}
                if (formatCurrentText && control is CustomTextBox tb &&// || ( formatCurrentText && control is TextEdit tb )&&
                    decimal.TryParse(tb.Text, out var value))
                {
                    tb.Text = value.ToString("F2");
                }

                bound++;
            }

            Debug.WriteLine(
                $"[BindTextBoxesBySuffix] Prefix={controlNamePrefix}; PropertyPrefix={propertyPrefix}; " +
                $"Candidates={candidates}; Bound={bound}; NoSuffix={skippedNoSuffix}; MissingProperty={skippedMissingProperty}");
        }

        private static IEnumerable<Control> GetAllControls(Control root)
        {
            foreach (Control c in root.Controls)
            {
                yield return c;
                foreach (var child in GetAllControls(c))
                    yield return child;
            }
        }

        private static string GetNumericSuffix(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;

            var i = name.Length - 1;
            while (i >= 0 && char.IsDigit(name[i])) i--;

            var start = i + 1;
            if (start >= name.Length) return null;
            return name.Substring(start);
        }


        private async Task LoadArticulAsync(string kod, string kodd)
        {
            var version = ++_loadVersion;

            gridControl1.ShowLoadingPanel();
            try
            {
                var detailsTask = _articulDataService.GetByKodAsync(kod);
                var artDrTask = _articulDataService.GetArtDrByKodAsync(kod);
                var komplTask = _articulDataService.GetSostavkomplForKod(kod);
                var naborTask = _articulDataService.GetSostavNaborForKod(kod);
                var imageTask = articulControl1.LoadImageAsync(kodd);

                await Task.WhenAll(detailsTask, artDrTask, komplTask, naborTask, imageTask);

                if (version != _loadVersion) return;

                var details = await detailsTask;
                ArticulControlBindingHelper.SetDetails(bsDetails, details);

                bsArtDr.DataSource = await artDrTask;
                bsSostKompl.DataSource = await komplTask;
                bsSostNabor.DataSource = await naborTask;
                bsSostKompl.ResetBindings(false);
                bsSostNabor.ResetBindings(false);

                UpdateTabsVisibility();
                LogSuccess($"Детали артикула загружены: Kod={kod}, Kodd={kodd}.", nameof(LoadArticulAsync));
            }
            catch (Exception ex)
            {
                LogError(ex, $"{nameof(LoadArticulAsync)}:{kod}");
            }
            finally
            {
                gridControl1.HideLoadingPanel();
            }
        }
        private void UpdateTabsVisibility()
        {
            cTabPage1.PageVisible = bsSostKompl.Count > 0;
            cTabPage2.PageVisible = bsSostNabor.Count > 0;
        }
        private async void gridControl1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (bsPreview.Current is not SpArtPreviewModel cur) return;
                await LoadArticulAsync(cur.Kod, cur.Kodd);
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(gridControl1_FocusedRowChanged));
            }
        }

        ///// <summary>
        ///// обновлениме данных на форме по коду при перемещении по таблице артикулов
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private async void gridControl1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        //{
        //    var current = bsPreview.Current as SpArtPreviewModel;
        //    if (current == null) return;

        //    var kod = current.Kod;
        //    var kodd = current.Kodd;

        //    gridControl1.ShowLoadingPanel();
        //    try
        //    {
        //        var detailsTask = _articulDataService.GetByKodAsync(kod);
        //        var artDrTask = _articulDataService.GetArtDrByKodAsync(kod);
        //        var komplTask = _articulDataService.GetSostavkomplForKod(kod);
        //        var naborTask = _articulDataService.GetSostavNaborForKod(kod);

        //        await Task.WhenAll(detailsTask, artDrTask, komplTask, naborTask);

        //        var details = await detailsTask;

        //        // ВАЖНО: меняем DataSource у одного bsDetails, ничего не пересоздаём
        //        bsDetails.DataSource = details;
        //        bsDetails.ResetBindings(false);

        //        bsArtDr.DataSource = await artDrTask;
        //        bsSostKompl.DataSource = await komplTask;
        //        bsSostNabor.DataSource = await naborTask;
        //        bsSostNabor.ResetBindings(false);

        //        UpdateTabsVisibility();
        //    }
        //    finally
        //    {
        //        gridControl1.HideLoadingPanel();
        //    } }
        ///// <summary>
        /// вызывает карточку по коду из справочника ШП
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customButtonKart_Click(object sender, EventArgs e)
        {
            string kod = "";
            try
            {
                GetItogVibKartReport report = new GetItogVibKartReport();
                report.RequestParameters = false;

                var currentRow = bsPreview.Current as SpArtPreviewModel;
                if (currentRow != null)
                {
                    kod = currentRow.Kod;

                    report.Parameters["kod"].Value = kod;

                    var ds = report.sqlDataSource1;
                    var query = ds.Queries[0] as DevExpress.DataAccess.Sql.StoredProcQuery;
                    query.Parameters[0].Value = kod;

                    ds.Fill();

                    report.DataSource = ds;
                    report.DataMember = "GetItogVibKart";

                    ReportPrintTool reportPrintTool = new ReportPrintTool(report);
                    reportPrintTool.ShowPreviewDialog();

                    //сокращенный :
                    GetItogVibKartSokrReport reportSokr = new GetItogVibKartSokrReport();
                    reportSokr.RequestParameters = false;
                    reportSokr.Parameters["kod"].Value = kod;
                    reportSokr.DataSource = ds;
                    reportSokr.DataMember = "GetItogVibKart";
                    ReportPrintTool reportSokrPrintTool = new ReportPrintTool(reportSokr);
                    reportSokrPrintTool.ShowPreviewDialog();

                }
            }
            catch (Exception ex)
            {
                LogError(ex, $"{nameof(customButtonKart_Click)}:{kod}");
                kod = "";
            }

        }
        /// <summary>
        /// добавление нового кода копированием
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void customButtonCopy_Click(object sender, EventArgs e)
        {
            //var current = bsArt.Current as SpArtPreviewModel;
            var kodObj = (bsPreview.Current as SpArtPreviewModel).Kod;
            if (kodObj == null)
            {
                MessageBox.Show("Не выбран артикул для копирования.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LogWarning("Не выбран артикул для копирования.", nameof(customButtonCopy_Click));
                return;
            }

            using (EditArticul f = new EditArticul(_currentUser, kodObj.ToString()))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    await RefreshArtPreviewAsync();
                    LogSuccess("Код успешно скопирован через форму EditArticul.", nameof(customButtonCopy_Click));
                }
            }
        }
        /// <summary>
        /// создание состава комплекта kompl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customButtonKompl_Click(object sender, EventArgs e)
        {
            //var kodObj = gridControl1.GetFocusedRowCellValue("Kod");
            var kodObj = (bsPreview.Current as SpArtPreviewModel).Kod;
            if (komplService.CheckNabor(kodObj))
            {
                MessageBox.Show("Комплектовать НАБОРЫ нельзя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LogWarning("Попытка комплектовать артикул, являющийся набором.", nameof(customButtonKompl_Click));
                return;
            }
            if (this.MdiParent is SpMainForm mainForm)
            {
                mainForm.OpenForm(new AddNewKopml(User, kodObj.ToString()));
            }
        }

        private async void csButtonNew_Click(object sender, EventArgs e)
        {
            using (EditArticul f = new EditArticul(_currentUser))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    await RefreshArtPreviewAsync();
                    LogSuccess("Создан новый артикул через форму EditArticul.", nameof(csButtonNew_Click));
                }
            }
        }
        /// <summary>
        /// удаление кода в справочнике
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void sButtodDeleteKod_Click(object sender, EventArgs e)
        {
            try
            {
                var kod = (bsPreview.Current as SpArtPreviewModel).Kod;

                string query = "exec dbo.kodArticulisUsed @kod = @kod";
                DataTable result = await _dbHelperAce.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@kod", kod } });

                var cuRow = (SpArtPreviewModel)bsPreview.Current;

                if (result.Rows.Count > 0)
                {
                    if (result.Rows[0].Field<int>("error") != 0)
                    {
                        MessageBox.Show("Ошибка удаления" + result.Rows[0].Field<string>("messageerror"));
                        LogWarning("Удаление артикула отклонено БД: " + result.Rows[0].Field<string>("messageerror"), nameof(sButtodDeleteKod_Click));
                        return;
                    }
                    //удаление кода 

                    await _dbService.DeleteEntityAsync("sp_articul", "Kod", cuRow);

                    bsPreview.RemoveCurrent();
                    LogSuccess($"Артикул удален: Kod={cuRow?.Kod}", nameof(sButtodDeleteKod_Click));
                }
                result?.Dispose();
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(sButtodDeleteKod_Click));
            }
        }
        /// <summary>
        /// открывает форму редактирования состава набора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customButton3_Click(object sender, EventArgs e)
        {
            var Obj = bsPreview.Current as SpArtPreviewModel;
            //нужно добавить проверку на признак НАБОРА, чтобы можно было открыть только набор.

            ArticulNaborSostavDataService _ANSDataService = new ArticulNaborSostavDataService();
            if (_ANSDataService.CheckOpis(Obj.Kod))
            {
                MessageBox.Show("Набор уже описан, изменения применятся на весь размерный ряд!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LogWarning($"Набор уже описан: Kod={Obj.Kod}. Изменения применятся ко всему размерному ряду.", nameof(customButton3_Click));
            }
            if (this.MdiParent is SpMainForm mainForm)
            {
                mainForm.OpenForm(new EditNaborSostav(User, Obj.Kod));
            }
        }
        /// <summary>
        /// открывает на редактирование карточку артикула
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="bindingSource"></param>
        /// <returns></returns>
        //private async Task EditArtciul (GridView gridView, IList list, BindingSource bindingSource, bool forMyDataAnnView = false)
        private void EditArtciul(GridView gridView, BindingSource bindingSource)
        {
            if (gridView == null || gridView.FocusedRowHandle < 0)
            {
                MessageBox.Show("Выберите артикул для редактирования!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LogWarning("Попытка редактирования без выбранного артикула.", nameof(EditArtciul));
                return;
            }
            var kodd = (bsPreview.Current as SpArtPreviewModel).Kodd;
            var articul = (bsPreview.Current as SpArtPreviewModel).Articul.Trim();

            if (this.MdiParent is SpMainForm mainForm)
            {
                mainForm.OpenForm(new ArticulEditAdvance(CurrentUser.User, kodd, articul));
            }
        }
        //???
        #region Управление немодальной формой ArticulEditAdvance

        private static readonly List<ArticulEditAdvance> _openEditArticulForms = new List<ArticulEditAdvance>();
        private static readonly object _lockObject = new object();
        private static bool HasOpenAdvanceForms()
        {
            lock (_lockObject)
            {
                // Очищаем закрытые формы из списка
                _openEditArticulForms.RemoveAll(form => form == null || form.IsDisposed);
                return _openEditArticulForms.Count > 0;
            }
        }

        /// <summary>
        /// Добавляет экземпляр ArticulEditAdvance в список открытых форм
        /// </summary>
        /// <param name="form">Форма для добавления</param>
        private static void AddOpenAdvanceForm(ArticulEditAdvance form)
        {
            lock (_lockObject)
            {
                if (form != null && !form.IsDisposed && !_openEditArticulForms.Contains(form))
                {
                    _openEditArticulForms.Add(form);
                }

            }
        }
        /// <summary>
        /// Удаляет экземпляр ArticulEditAdvance из списка открытых форм
        /// </summary>
        /// <param name="form">Форма для удаления</param>
        private static void RemoveOpenAdvanceForm(ArticulEditAdvance form)
        {
            lock (_lockObject)
            {
                _openEditArticulForms.Remove(form);
            }
        }
        #endregion

        private void csButtonEdit_Click(object sender, EventArgs e)
        {
            EditArtciul(gridControl1, bsPreview);

        }

        private void Articul_FormClosed(object sender, FormClosedEventArgs e)
        {

            gridControl1.FocusedRowChanged -= gridControl1_FocusedRowChanged;

            // Отвязать BindingSource
            bsPreview.DataSource = null;

            // Dispose DevExpress контролов
            gridControl1?.Dispose();
            gridView1?.Dispose();

            // Dispose автогенерируемых объектов
            components?.Dispose();
        }

        private void customSimpleButton7_Click(object sender, EventArgs e)
        {
			var str = bsPreview.Current as SpArtPreviewModel;
			if (str == null)
				return;

			string kod = str.Kod?.ToString() ?? string.Empty;
			if (string.IsNullOrWhiteSpace(kod))
			{
				MessageBox.Show("Не найден код.");
				return;
			}

			var printSewn = new PrintSewn(kod);
			printSewn.ShowDialog();
			return;
		}

        #region headerButtons
        /// <summary>
        /// Инициализирует теги для кнопок в заголовке групп
        /// </summary>
        private void InitHeaderButtonTags()
        {
            // layoutControlGroup1 — основная группа с гридом
            TagByCaption(layoutControlGroup1, new (string caption, string tag)[] {
                ("Карточка", "articulCard"),
                ("Архив", "arch"),
            });

        }

        /// <summary>
        /// Проставляет теги кнопкам по их подписям
        /// </summary>
        private void TagByCaption(LayoutControlGroup group, IEnumerable<(string caption, string tag)> map)
        {
            if (group == null || group.CustomHeaderButtons == null) return;

            foreach (var (caption, tag) in map)
            {
                var btn = group.CustomHeaderButtons
                               .OfType<GroupBoxButton>()
                               .FirstOrDefault(b => string.Equals(b.Caption, caption, StringComparison.OrdinalIgnoreCase));
                if (btn != null && (btn.Tag == null || string.IsNullOrWhiteSpace(btn.Tag.ToString())))
                {
                    btn.Tag = tag;
                }
            }
        }
        private void layoutControlGroup1_CustomButtonClick_1(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (sender is LayoutControlGroup group && e.Button is GroupBoxButton button)
            {
                string tag = button.Tag?.ToString() ?? string.Empty;

                switch (tag)
                {
                    case "articulCard":
                        customButtonKart_Click(sender, EventArgs.Empty);
                        break;
                    default:
                        // Если тег не установлен, пытаемся определить по Caption
                        string caption = button.Caption ?? string.Empty;
                        if (caption.Contains("Карточка", StringComparison.OrdinalIgnoreCase))
                        {
                            customButtonKart_Click(sender, EventArgs.Empty);
                        }
                        break;
                }
            }

        }

        private void layoutControlGroup1_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (sender is LayoutControlGroup group && e.Button is GroupBoxButton button)
            {
                //string tag = button.Tag?.ToString() ?? string.Empty;

                //switch (tag)
                //{
                //    case "arch":
                //        // архив
                //        button.Caption = button.Checked ? "✔ Архив" : "✖ Архив";
                //        break;
                //    default:
                //        // Если тег не установлен, пытаемся определить по Caption
                //        string caption = button.Caption ?? string.Empty;
                //        if (caption.Contains("Архив", StringComparison.OrdinalIgnoreCase))
                //        {
                //            //архив
                //        }
                //        break;
                //}
                SyncArchiveButtonCaption();
            }

        }

        private void layoutControlGroup1_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (sender is LayoutControlGroup group && e.Button is GroupBoxButton button)
            {
                //string tag = button.Tag?.ToString() ?? string.Empty;

                //switch (tag)
                //{
                //    case "arch":
                //        // архив
                //        button.Caption = button.Checked ? "✔ Архив" : "✖ Архив";

                //        break;
                //    default:
                //        // Если тег не установлен, пытаемся определить по Caption
                //        string caption = button.Caption ?? string.Empty;
                //        if (caption.Contains("Архив", StringComparison.OrdinalIgnoreCase))
                //        {
                //            //архив
                //        }
                //        break;
                //}
                SyncArchiveButtonCaption();
            }

        }
        private void SyncArchiveButtonCaption()
        {
            foreach (var btn in layoutControlGroup1.CustomHeaderButtons)
            {
                if (btn is not GroupBoxButton button) continue;

                // Найти именно чек-кнопку "Архив"
                var isArchiveButton =
                    button.Style == DevExpress.XtraBars.Docking2010.ButtonStyle.CheckButton &&
                    (button.Tag?.ToString() == "arch" ||
                     (button.Caption?.Contains("Архив", StringComparison.OrdinalIgnoreCase) ?? false));

                if (!isArchiveButton) continue;

                button.Tag = "arch"; // зафиксировать идентификатор
                button.Caption = button.Checked ? "✔ Архив" : "✖ Архив";
                break;
            }
        }
        #endregion

        private void btnPublishedArticles_Click(object sender, EventArgs e)
        {
        
        if (this.MdiParent is SpMainForm mainForm)
        {
            mainForm.OpenForm(new CreateArticulMatrForm(CurrentUser.User));
            /*using (CreateArticulMatrForm f = new CreateArticulMatrForm(_user))
            { }*/
        }

    }
    }
}
