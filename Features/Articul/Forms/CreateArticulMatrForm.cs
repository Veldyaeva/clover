using DevExpress.Mvvm.Native;
using DevExpress.XtraBars.Customization;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SewingProduction.Core.Models;
using SewingProduction.Core.Services;
using SewingProduction.Features.Articul.Helpers;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.Articul.Service;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Features.UserDistribution.Class;
using SewingProduction.Features.UserDistribution.Forms;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.Articul.Forms
{
    public partial class CreateArticulMatrForm : CustomForm

    {
        private DatabaseHelperSQL _dbHelper;
        private DbService _dbService;
        private readonly MatrixService _matrixService;

        private CreateArticulMatrService _createArticulMatrService = new CreateArticulMatrService();
        private ArticulDataService _articulDataService = new ArticulDataService();
        private readonly ILogger _logger = new FileLogger();

        private bool _isEditing = false; // флаг для отслеживания, находится ли грид в режиме редактирования

        private BindingSource _bindingSourceArtMatr;
        private BindingSource _bindingSourceArticulCompare;

        //private BindingSource _bindingSourceGostGrupAll;
        private List<GostGrupIzdViewModel> _gostGroupAll;

        private readonly BindingSource _bsDetails = new(); // источник для деталей

        private ComparisonResult _comparisonResult;
        private readonly Dictionary<string, Image> _imageCache = new();
        public CreateArticulMatrForm(UserClass user) : base(user)
        {
            _dbHelper = new DatabaseHelperSQL();
            _dbService = new DbService(_dbHelper);

            InitializeComponent();

            _user = user;

            _bindingSourceArtMatr = new BindingSource { };
            _bindingSourceArticulCompare = new BindingSource { };

            //if (gridArtMatr != null) gridArtMatr.DataSource = _bindingSourceArtMatr;
            gridArtMatr.DataSource = _bindingSourceArtMatr;
            gridArtCompare.DataSource = _bindingSourceArticulCompare;

            _matrixService = new MatrixService(_dbHelper);

        }

        private async void CreateArticulMatr_Load(object sender, EventArgs e)
        {
            await LoadOrRefreshData();

            articulControl1.BindTo(_bsDetails);
            articulControl1.IsReadOnly = true;

            InitializeBindings();
            //BindGost();
            BindGostGrupp();

            //в зависимости от прав пользователя - разрешаем или запрещаем редактирование грида 
            _isEditing = customSimpleButtonPermissions.Visible;
            //_isEditing = false;

            SetPermisions();

        }
        private async Task LoadOrRefreshData()
        {
            //загрузка данных для отображения в гриде
            gridViewArtMatr.ShowLoadingPanel();
            gridViewArtMatrEdit.ShowLoadingPanel();

            var getArtTask = _createArticulMatrService.GetMatrForArticulAsync();
            var getGostGrupTask = _createArticulMatrService.GetGrupGostAsync();

            await Task.WhenAll(getArtTask, getGostGrupTask);

            _bindingSourceArtMatr.DataSource = getArtTask.Result;
            _gostGroupAll = getGostGrupTask.Result;

            gridViewArtMatr.HideLoadingPanel();
            gridViewArtMatrEdit.HideLoadingPanel();

        }

        /// <summary>
        /// в зависимости от прав пользователя - разрешаем или запрещаем редактирование грида 
        /// </summary>
        private void SetPermisions()
        {
            gridArtMatr.MainView = _isEditing ? gridViewArtMatrEdit : gridViewArtMatr;
            customSimpleButtonPermissions.Visible = false;
        }
        private void InitializeBindings()
        {
            gcGrupmen_name.FieldName = nameof(CreateArticulMatrModel.Grupmen_name);
            gcCertGrupmen_name.FieldName = nameof(CreateArticulMatrModel.Grupmen_name);
            gcTsn_name.FieldName = nameof(CreateArticulMatrModel.Tsn_name);
            gcCertTsn_name.FieldName = nameof(CreateArticulMatrModel.Tsn_name);
            gcTb_id.FieldName = nameof(CreateArticulMatrModel.Tb_id);
            gcCertTb_id.FieldName = nameof(CreateArticulMatrModel.Tb_id);
            gcMod.FieldName = nameof(CreateArticulMatrModel.Mod);
            gcCertMod.FieldName = nameof(CreateArticulMatrModel.Mod);
            gcFoundMod.FieldName = nameof(CreateArticulMatrModel.FoundMod);
            gcCertFoundMod.FieldName = nameof(CreateArticulMatrModel.FoundMod);
            gcArticul.FieldName = nameof(CreateArticulMatrModel.Articul);
            gcCertArticul.FieldName = nameof(CreateArticulMatrModel.Articul);
            gcTm_name.FieldName = nameof(CreateArticulMatrModel.Tm_name);
            gcCertTm_name.FieldName = nameof(CreateArticulMatrModel.Tm_name);
            gcGrup.FieldName = nameof(CreateArticulMatrModel.Grup);
            gcCertGrup.FieldName = nameof(CreateArticulMatrModel.Grup);
            gcText_mo.FieldName = nameof(CreateArticulMatrModel.Text_mo);
            gcCertText_mo.FieldName = nameof(CreateArticulMatrModel.Text_mo);
            gcP.FieldName = nameof(CreateArticulMatrModel.P);
            gcCertP.FieldName = nameof(CreateArticulMatrModel.P);
            gcPrinter.FieldName = nameof(CreateArticulMatrModel.Printer);
            gcCertPrinter.FieldName = nameof(CreateArticulMatrModel.Printer);
            gcBus.FieldName = nameof(CreateArticulMatrModel.Bus);
            gcCertBus.FieldName = nameof(CreateArticulMatrModel.Bus);
            gcStra.FieldName = nameof(CreateArticulMatrModel.Stra);
            gcCertStra.FieldName = nameof(CreateArticulMatrModel.Stra);
            gcV.FieldName = nameof(CreateArticulMatrModel.V);
            gcCertV.FieldName = nameof(CreateArticulMatrModel.V);
            gcKruj.FieldName = nameof(CreateArticulMatrModel.Kruj);
            gcCertKruj.FieldName = nameof(CreateArticulMatrModel.Kruj);
            gcTkan.FieldName = nameof(CreateArticulMatrModel.Tkan);
            gcCertTkan.FieldName = nameof(CreateArticulMatrModel.Tkan);
            gcSost.FieldName = nameof(CreateArticulMatrModel.Sost);
            gcCertSost.FieldName = nameof(CreateArticulMatrModel.Sost);
            gcSost2.FieldName = nameof(CreateArticulMatrModel.Sost2);
            gcCertSost2.FieldName = nameof(CreateArticulMatrModel.Sost2);
            gcSost3.FieldName = nameof(CreateArticulMatrModel.Sost3);
            gcCertSost3.FieldName = nameof(CreateArticulMatrModel.Sost3);
            gcRazmNames.FieldName = nameof(CreateArticulMatrModel.RazmNames);
            gcCertRazmNames.FieldName = nameof(CreateArticulMatrModel.RazmNames);
            gcDatePublic.FieldName = nameof(CreateArticulMatrModel.DatePublic);
            gcCertDatePublic.FieldName = nameof(CreateArticulMatrModel.DatePublic);
            gcModMatrix.FieldName = nameof(CreateArticulMatrModel.ModMatrix);
            gcCertModMatrix.FieldName = nameof(CreateArticulMatrModel.ModMatrix);
            gcRepeatArticle.FieldName = nameof(CreateArticulMatrModel.RepeatArticle);
            gcCertRepeatArticle.FieldName = nameof(CreateArticulMatrModel.RepeatArticle);


            //поля уточнения для отд сертификации
            gcCertidGost.FieldName = nameof(CreateArticulMatrModel.Id_gost);
            gcidGost.FieldName = nameof(CreateArticulMatrModel.Id_gost);
            //CreateArticulMatrModel.Ag_id
            gcCertAgid.FieldName = nameof(CreateArticulMatrModel.Unic_IdGost_idAg);
            gcAgid.FieldName = nameof(CreateArticulMatrModel.Unic_IdGost_idAg);
            
            gcCertDateCertificationApproval.FieldName = nameof(CreateArticulMatrModel.DateCertificationApproval);
            //запрет редактирования полей, которые не должны редактироваться напрямую пользователем, а заполняются через выбор из справочника и/или автоматически
            gcCertDateCertificationApproval.OptionsColumn.AllowEdit = false;
            //перечень моделей для стыковки gridViewArtCompare
            gcKoddCompare.FieldName = nameof(SpArtPreviewModel.Kodd);
            gcGrupCompare.FieldName = nameof(SpArtPreviewModel.Grup);
            gcArticulCompare.FieldName = nameof(SpArtPreviewModel.Articul);
            gcModCompare.FieldName = nameof(SpArtPreviewModel.Mod);
            gcTMCompare.FieldName = nameof(SpArtPreviewModel.tmName);
            gcArhCompare.FieldName = nameof(SpArtPreviewModel.Arh);

        }

        //private async void BindGost()
        //{
        //    try
        //    {
        //        var ri = repositoryItemSearchLookUpEdit1;
        //        ri.DataSource = await _createArticulMatrService.GetGostAsync();
        //        ri.DisplayMember = nameof(GostModel.Id_gost);
        //        ri.ValueMember = nameof(GostModel.Id_gost);
        //        // Колонки выпадающего списка (по желанию)

        //        var view = ri.PopupView as DevExpress.XtraGrid.Views.Grid.GridView;
        //        if (view == null)
        //            throw new InvalidOperationException("PopupView не GridView");
        //        view.OptionsView.ShowColumnHeaders = true;
        //        view.OptionsView.ShowIndicator = false;
        //        view.OptionsView.ShowAutoFilterRow = true; // ⭐ фильтр по колонкам
        //        view.OptionsBehavior.Editable = false;
        //        view.OptionsSelection.EnableAppearanceFocusedCell = false;
        //        view.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
        //        view.Columns.Clear();

        //        view.Columns.AddVisible(nameof(GostModel.Id_gost), "ID");
        //        view.Columns.AddVisible(nameof(GostModel.Name_gost), "Название");
        //        view.Columns.AddVisible(nameof(GostModel.Opi_gost), "Описание");
        //        //view.BestFitColumns();

        //        ri.NullText = ""; // что показывать, если значение null
        //        //ri.ShowHeader = false;
        //        //ri.ShowFooter = false;
        //        ri.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor; // запрет ввода, только выбор
        //        ri.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок ГОСТ");
        //        throw;
        //    }
        //}
        private async void BindGostGrupp()
        {
            try
            {
                var ri = repositoryItemSearchLookUpEdit2;
                // после выбора госта - фильтрация групп по госту происходит в repositoryItemSearchLookUpEdit2_BeforePopup
                ri.DataSource = _gostGroupAll;
                ri.DisplayMember = nameof(GostGrupIzdViewModel.N_i);
                //ri.ValueMember = nameof(GostGrupIzdViewModel.Ag_id);
                ri.ValueMember = nameof(GostGrupIzdViewModel.Unic_IdGost_idAg);

                //колонка с картинкой 
                var pictureEdit = new RepositoryItemPictureEdit
                {
                    SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom,
                    NullText = ""
                };

                // Колонки выпадающего списка (по желанию)
                var view = ri.PopupView as DevExpress.XtraGrid.Views.Grid.GridView;
                if (view == null)
                    throw new InvalidOperationException("PopupView не GridView");
                view.OptionsView.ShowColumnHeaders = true;
                view.OptionsView.ShowIndicator = false;
                view.OptionsView.ShowAutoFilterRow = true; // ⭐ фильтр по колонкам
                view.OptionsBehavior.Editable = false;
                view.OptionsSelection.EnableAppearanceFocusedCell = false;

                //view.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
                // Для картинок в popup лучше отключить focus-рамку:  (Если нужно оставить фокус, но не на ячейке с картинкой: view.FocusRectStyle = DrawFocusRectStyle.RowFocus;)
                view.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
                view.OptionsSelection.EnableAppearanceFocusedCell = false;
                view.OptionsSelection.EnableAppearanceFocusedRow = true;

                view.Columns.Clear();

                view.Columns.AddVisible(nameof(GostGrupIzdViewModel.Id_gost), "Гост");
                view.Columns.AddVisible(nameof(GostGrupIzdViewModel.Ag_id), "Номер группы");
                view.Columns.AddVisible(nameof(GostGrupIzdViewModel.N_i), "Название");
                view.Columns.AddVisible(nameof(GostGrupIzdViewModel.Ag_name_sokr), "Сокращенное назв.");
                view.Columns.AddVisible(nameof(GostGrupIzdViewModel.Care_instructions), "Инструкции по уходу");

                #region описание картинки
                var imageCol = view.Columns.AddVisible("Picture", "Символы по уходы");
                imageCol.UnboundType = DevExpress.Data.UnboundColumnType.Object;
                imageCol.ColumnEdit = pictureEdit;
                imageCol.Width = 190;
                imageCol.OptionsColumn.AllowEdit = false;

                view.RowHeight = 48;
                view.OptionsView.RowAutoHeight = true;

                view.CustomUnboundColumnData -= View_CustomUnboundColumnData;
                view.CustomUnboundColumnData += View_CustomUnboundColumnData;
                #endregion

                view.RefreshData();

                ri.NullText = ""; // что показывать, если значение null

                ri.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor; // запрет ввода, только выбор
                ri.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок группы ГОСТ");
                throw;
            }
        }
        private void View_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (!e.IsGetData || e.Column.FieldName != "Picture")
                return;

            //var view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            //var row = view.GetRow(e.ListSourceRowIndex) as GostGrupIzdViewModel;
            if (e.ListSourceRowIndex < 0)
                return;

            var list = repositoryItemSearchLookUpEdit2.DataSource as IList<GostGrupIzdViewModel>;
            if (list == null || e.ListSourceRowIndex >= list.Count)
                return;

            var row = list[e.ListSourceRowIndex];
            e.Value = GetImage(row?.CareImagePath);
        }
        private Image? GetImage(string? path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                return null;

            if (_imageCache.TryGetValue(path, out var cached))
                return cached;

            byte[] bytes = File.ReadAllBytes(path); // файл не блокируется

            using var ms = new MemoryStream(bytes);
            //Делает полноценную копию картинки, можно безопасно закрыть stream, файл не блокируется
            using var original = Image.FromStream(ms);

            //var image = new Bitmap(original, new Size(160, 32)); -- задавался определенный размер картинки, убрала 
            var image = new Bitmap(original);
            _imageCache[path] = image;
            return image;
        }


        private void repositoryItemSearchLookUpEdit2_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            //try
            //{
            //    if (!e.AcceptValue) return; // если пользователь отменил выбор, не обновляем данные

            //    // сохраняем текущее редактирование, чтобы получить актуальное значение 
            //    var view = gridViewArtMatrEdit;
            //    view.PostEditor();
            //    view.UpdateCurrentRow();

            //    var currentItem = (CreateArticulMatrModel)_bindingSourceArtMatr.Current;

            //    //скидываем гост при изменении группы, чтобы не было "висячих" гостов
            //    //currentItem.Id_gost = 0;

            //    ////обновляем значение ГОСТ из выбранной группы
            //    //var editor = sender as DevExpress.XtraEditors.SearchLookUpEdit;
            //    //if (editor == null)
            //    //    return;

            //    //var viewLookUp = repositoryItemSearchLookUpEdit2.PopupView
            //    //    as DevExpress.XtraGrid.Views.Grid.GridView;

            //    //if (viewLookUp == null)
            //    //    return;

            //    //int rowHandle = viewLookUp.FocusedRowHandle;

            //    //var selectedRow = view.GetRow(rowHandle) as GostGrupIzdViewModel;

            //    //if (selectedRow == null)
            //    //    return;

            //    //currentItem.Id_gost = selectedRow.Id_gost;

            //    //view.PostEditor();
            //    //// когда пользователь изменил значение в гриде
            //    //view.UpdateCurrentRow();

            //    //// говорит привязанным контролам: “текущий объект изменился, перечитайте его”, Использовать, когда сами изменили объект в коде
            //    //_bindingSourceArtMatr.ResetCurrentItem();
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogErrorAsync(ex, "Ошибка при изменении группы ГОСТ (CloseUp)");
            //    throw;
            //}
        }

        //private void repositoryItemSearchLookUpEdit1_BeforePopup(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var editor = gridViewArtMatrEdit.ActiveEditor as DevExpress.XtraEditors.SearchLookUpEdit;
        //        if (editor == null)
        //            return;

        //        if (sender == null) return;

        //        var currentItem = (CreateArticulMatrModel)_bindingSourceArtMatr.Current;
        //        var idGrup = currentItem.Ag_id;
        //        editor.Properties.DataSource = _gostGroupAll
        //            .Where(x => x.Ag_id == idGrup)
        //            .ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogErrorAsync(ex, "Ошибка при открытии выпадающего списка групп ГОСТ");
        //        throw;
        //    }
        //}
        private async void gridViewArtMatrEdit_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var view = sender as GridView;
                if (view == null) return;

                Point pt = view.GridControl.PointToClient(Control.MousePosition);
                GridHitInfo hit = view.CalcHitInfo(pt);
                // при doubleClick на указанных столбцах произойдет установка или снятие даты утверждения
                if (hit.InRowCell && (hit.Column == gcCertidGost || hit.Column == gcCertAgid || hit.Column == gcCertDateCertificationApproval) && hit.RowHandle >= 0)
                {
                    var _currentItem = (CreateArticulMatrModel)_bindingSourceArtMatr.Current;
                    if (_currentItem == null) return;

                    if (_currentItem.DateCertificationApproval != null)
                    {
                        DialogResult msres = MessageBox.Show("Снять дату подтверждения ГОСТ?", "Снять дату", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (msres == DialogResult.No)
                        {
                            return;
                        }
                    }

                    if (_currentItem.Id_gost == 0 && _currentItem.Ag_id == 0)
                    {
                        MessageBox.Show("Для утверждения необходимо выбрать ГОСТ и группу ГОСТ", "Невозможно утвердить", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //currentItem.DateCertificationApproval = null;
                    }
                    else
                    {
                        var res = await _createArticulMatrService.GetStatusForArticulAsync(_currentItem.Nn, _currentItem.Id_gost, _currentItem.Ag_id);
                        if (res != null)
                        {
                            _currentItem.DateCertificationApproval = res.DateCertificationApproval;
                            //обновляем только ячейку с датой утверждения, чтобы не сбрасывать фокус и не уходить из режима редактирования
                            _bindingSourceArtMatr.ResetCurrentItem();
                        }
                    }
                }
            }
            catch (SqlException sqlex)
            {
                MessageBox.Show($"{sqlex.ErrorCode} - {sqlex.Message}", "Ошибка SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка в gridViewArtMatrEdit_DoubleClick при утверждении госта");
                throw;
            }
        }
        /// <summary>
        /// При смене фокусной строки в гриде матрицы загружаем данные для сравнения и эскиз для текущего артикула матрицы,
        /// при этом очищаем предыдущую подсветку сравнения и детали от предыдущего сравнения, чтобы не было "висячих" данных от предыдущего сравнения, 
        /// так как новый артикул может не совпадать с предыдущим артикулом сравнения и по нему могут быть другие детали и другой эскиз,
        /// а также может не быть артикула для сравнения вообще, тогда детали и эскиз должны быть очищены
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void gridArtMatr_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                articulControl1.ClearComparisonHighlight();   //очищаем подсветку сравнения при смене артикула в матрице, чтобы не было "висячей" подсветки от предыдущего сравнения
                ArticulControlBindingHelper.ClearDetails(_bsDetails);// очищаем детали от предыдущего сравнения
                articulControl1.ClearImage();
                var currentRow = _bindingSourceArtMatr.Current as CreateArticulMatrModel;// получаем текущую выбранную строку из матрицы
                if (currentRow == null)// если строка не выбрана, выходим из метода
                    return;

                _bindingSourceArticulCompare.DataSource = await _createArticulMatrService.GetArticulsForCompareAsync(currentRow.Articul);// загружаем данные для сравнения в другой грид

                string imagePath = await _matrixService.GetFileEskizNN(currentRow.Nn);// загружаем эскиз для текущего артикула матрицы
                pictureBoxMatrix.ImageLocation = string.IsNullOrWhiteSpace(imagePath) ? null : imagePath;// отображаем эскиз, если он есть, или очищаем картинку, если эскиза нет

                await fillCompareTable();
                UpdateEditPermissionByApprovalDate();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке данных для сравнения артикула");
                throw;
            }
        }
        private async void gridViewArtMatrEdit_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            try
            {
                UpdateEditPermissionByApprovalDate();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке данных для сравнения артикула");
                throw;
            }
        }
        /// <summary>
        /// установка разрешения редактировать гост и группу по гост при наличии даты утверждения
        /// </summary>
        private void UpdateEditPermissionByApprovalDate()
        {
            var currentRow = _bindingSourceArtMatr.Current as CreateArticulMatrModel;// получаем текущую выбранную строку из матрицы
            if (currentRow == null)
                return;
            if (gridArtMatr.FocusedView == gridViewArtMatrEdit)
            {
                gridViewArtMatrEdit.CloseEditor();
                //запрещаем редактировать, если есть дата утверждения
                if (currentRow.DateCertificationApproval != null)
                {
                    gcCertAgid.OptionsColumn.AllowEdit = false;
                    gcCertAgid.OptionsColumn.ReadOnly = true;
                }
                else
                {
                    gcCertAgid.OptionsColumn.AllowEdit = true;
                    gcCertAgid.OptionsColumn.ReadOnly = false;
                }
                gridViewArtMatrEdit.RefreshData();
                gridArtMatr.Refresh();
            }
        }

        private async Task fillCompareTable()
        {
            var currentRow1 = _bindingSourceArticulCompare.Current as SpArtPreviewModel;// получаем текущую выбранную строку из грида сравнения
            string kod = currentRow1?.Kod;// извлекаем код артикула для загрузки деталей, если строка выбрана, или null, если строка не выбрана

            gridViewArtCompare.ShowLoadingPanel();// показываем индикатор загрузки, так как загрузка деталей может занять некоторое время
            ArticulControlBindingHelper.ClearDetails(_bsDetails);// очищаем предыдущие детали, чтобы не было "висячих" данных от предыдущего сравнения, пока загружаются новые детали
            articulControl1.ClearImage();
            //          _bsDetails.DataSource = await _articulDataService.GetByKodAsync(kod);
            await CompareSelectedArticulAsync();// загружаем детали для выбранного артикула сравнения и выполняем сравнение с текущим артикулом матрицы, результат сравнения сохраняем в поле _comparisonResult, чтобы при сохранении матрицы знать, нужно ли сохранять изменения или нет
            gridViewArtCompare.HideLoadingPanel();// скрываем индикатор загрузки после завершения загрузки деталей и сравнения
        }

        /// <summary>
        /// При смене фокусной строки в гриде сравнения загружаем детали для выбранного артикула сравнения и выполняем сравнение с текущим артикулом матрицы,
        /// результат сравнения сохраняем в поле _lastCompareResult, чтобы при сохранении матрицы знать, нужно ли сохранять изменения или нет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void gridViewArtCompare_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                await fillCompareTable();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке данных детализации артикула сравнения");
                throw;
            }
            finally
            {
                gridViewArtCompare.HideLoadingPanel();
            }

        }

        /// <summary>
        /// Загружает детали для выбранного артикула сравнения и выполняет сравнение с текущим артикулом матрицы, результат сравнения сохраняет в поле _lastCompareResult, 
        /// чтобы при сохранении матрицы знать, нужно ли сохранять изменения или нет
        /// </summary>
        /// <returns></returns>
        private async Task CompareSelectedArticulAsync()
        {
            try
            {
                var matrixRow = _bindingSourceArtMatr.Current as CreateArticulMatrModel;// получаем текущую выбранную строку из матрицы, с которой будем сравнивать
                var compareRow = _bindingSourceArticulCompare.Current as SpArtPreviewModel;// получаем текущую выбранную строку из грида сравнения, с которой будем сравнивать

                articulControl1.ClearComparisonHighlight();// очищаем предыдущую подсветку сравнения, чтобы не было "висячей" подсветки от предыдущего сравнения

                string kod = compareRow?.Kod;// извлекаем код артикула для загрузки деталей, если строка выбрана, или null, если строка не выбрана

                if (matrixRow == null || compareRow == null || string.IsNullOrWhiteSpace(compareRow.Kod))// если не выбрана строка для сравнения или в выбранной строке нет кода артикула для сравнения, очищаем детали и выходим из метода, так как нечего сравнивать
                {
                    ArticulControlBindingHelper.ClearDetails(_bsDetails);// очищаем детали, чтобы не было "висячих" данных от предыдущего сравнения, так как нет артикула для сравнения
                    articulControl1.ClearImage();
                    _comparisonResult = null;
                    return;
                }

                var detailsTask = _articulDataService.GetByKodAsync(compareRow.Kod);// загружаем детали для выбранного артикула сравнения
                var imageTask = articulControl1.LoadImageAsync(compareRow.Kodd);// загружаем изображение артикула так же, как в форме Articul

                await Task.WhenAll(detailsTask, imageTask);

                var details = await detailsTask;
                ArticulControlBindingHelper.SetDetails(_bsDetails, details);// устанавливаем источник данных для деталей, которые отображаются в articulControl1

                var compareItems = BuildComparisonItems(matrixRow);// создаем список полей для сравнения на основе текущей строки матрицы, который будет использоваться в articulControl1 для сравнения и подсветки различий
                var result = await articulControl1.CompareAndHighlight(compareItems);// выполняем сравнение и подсветку различий в articulControl1, результат сравнения сохраняем в переменной result, которая содержит информацию о том, совпадают ли артикулы полностью (IsMatch) и какие поля отличаются (Mismatches)
                                                                                     // тут можно сохранить флаг в поле формы
                _comparisonResult = result;// сохраняем результат сравнения в поле формы, чтобы при сохранении матрицы знать, нужно ли сохранять изменения или нет, так как если артикулы совпадают полностью, то сохранять изменения не нужно, так как они не изменились по сравнению с выбранным артикулом сравнения

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "213");

                _comparisonResult = null;

            }

        }

        private IReadOnlyList<FieldComparisonItem> BuildComparisonItems(CreateArticulMatrModel matrixRow)
        {
            var compareItems = CreateArticulMatrComparisonBuilder.Build(matrixRow);

            if (compareItems.Count == 0)
                return compareItems;

            var expectedGostGroupName = _gostGroupAll?
                .FirstOrDefault(x => x.Ag_id == matrixRow.Ag_id)?
                .N_i;
                //.Ag_name_sokr;

            return compareItems
                .Select(item => string.Equals(item.PropertyName, nameof(SpArticulPreviewModel.Ag_id), StringComparison.OrdinalIgnoreCase)
                    ? new FieldComparisonItem
                    {
                        PropertyName = item.PropertyName,
                        ExpectedValue = item.ExpectedValue,
                        ExpectedDisplayValue = string.IsNullOrWhiteSpace(expectedGostGroupName) ? item.ExpectedValue : expectedGostGroupName,
                        DisplayName = item.DisplayName,
                        FullMatch = item.FullMatch
                    }
                    : item)
                .ToList();
        }

        private async void btnSelectModel_Click(object sender, EventArgs e)
        {
            var curMatr = _bindingSourceArtMatr.Current as CreateArticulMatrModel;// получаем текущую выбранную строку из матрицы
            if (curMatr == null)
                return;


            var curCompareRow = _bindingSourceArticulCompare.Current as SpArtPreviewModel;
            ArticulComparisonValidator objArticulChecks = new ArticulComparisonValidator(curMatr, curCompareRow);

            // проверка  при расхождении в составе
            if (_comparisonResult.ComplicateMismatches.Count > 0)
            {
                var canChSost = await objArticulChecks.canChangeArticulSost();
                if (!canChSost.IsSuccess)
                {
                    MessageBox.Show(@$"Невозможно выбрать эту модель для стыковки: 
                        {canChSost.ErrorMessage}", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    objArticulChecks = null;
                    return;
                }
            }

            //все совпало по выбранной модели
            if (_comparisonResult.IsMatch)
            {
                // проверки перед выбором модели 
                var canLink = await objArticulChecks.canLinkArticul();

                if (canLink.IsSuccess)
                {
                    using (AppendArticul f = new AppendArticul(CurrentUser.User, curMatr.Nn, curCompareRow.Kod, _comparisonResult))
                    {
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            await LoadOrRefreshData();
                        }
                    }
                }
                else
                {
                    MessageBox.Show(@$"Невозможно выбрать эту модель для стыковки: 
                        {canLink.ErrorMessage}", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                objArticulChecks = null;
            }
            else
            {
                MessageBox.Show("Текущий артикул матрицы и выбранный артикул не совпадают. Пожалуйста, выберите другой артикул для стыковки или создайте новый.", "Несовпадение артикулов", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnAddModel_Click(object sender, EventArgs e)
        {
            var curMatr = _bindingSourceArtMatr.Current as CreateArticulMatrModel;// получаем текущую выбранную строку из матрицы
            if (curMatr == null)
                return;
            var currArt = _bindingSourceArticulCompare.Current as SpArtPreviewModel;// получаем текущую выбранную строку из грида сравнения
            if (currArt == null)
                return;


            using (AppendArticul f = new AppendArticul(CurrentUser.User, curMatr.Nn))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    await LoadOrRefreshData();
                }

                //if (f.ShowDialog() == DialogResult.OK)
                //{
                //    await RefreshArtPreviewAsync();
                //    LogSuccess("Создан новый артикул через форму EditArticul.", nameof(csButtonNew_Click));
                //}
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            foreach (var image in _imageCache.Values)
                image.Dispose();

            _imageCache.Clear();

            base.OnFormClosed(e);
        }

        private async void repositoryItemSearchLookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var editor = sender as DevExpress.XtraEditors.SearchLookUpEdit;
                if (editor == null)
                    return;

                var selectedRow = editor.Properties.GetRowByKeyValue(editor.EditValue)
                    as GostGrupIzdViewModel;

                if (selectedRow == null)
                    return;
                var currentItem = (CreateArticulMatrModel)_bindingSourceArtMatr.Current;
                if (currentItem == null)
                    return;

                currentItem.Id_gost = selectedRow.Id_gost;
                currentItem.Ag_id = selectedRow.Ag_id;
                _bindingSourceArtMatr.ResetCurrentItem();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при выборе группы ");
                throw;
            }

        }

        
    }
}
