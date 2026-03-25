using DevExpress.Mvvm.Native;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SewingProduction.Core.Models;
using SewingProduction.Core.Services;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.Articul.Service;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.Articul.Forms
{
    public partial class CreateArticulMatrForm : CustomForm

    {
        private DatabaseHelper _dbHelper;
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

        private bool _lastCompareResult;

        public CreateArticulMatrForm(UserClass user) : base(user)
        {
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);

            InitializeComponent();


            _bindingSourceArtMatr = new BindingSource { };
            _bindingSourceArticulCompare = new BindingSource { };

            //if (gridArtMatr != null) gridArtMatr.DataSource = _bindingSourceArtMatr;
            gridArtMatr.DataSource = _bindingSourceArtMatr;
            gridArtCompare.DataSource = _bindingSourceArticulCompare;

            _matrixService = new MatrixService(_dbHelper);

        }

        private async void CreateArticulMatr_Load(object sender, EventArgs e)
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

            articulControl1.BindTo(_bsDetails);
            articulControl1.IsReadOnly = true;



            InitializeBindings();
            BindGost();
            BindGostGrupp();

            //в зависимости от прав пользователя - разрешаем или запрещаем редактирование грида 
            _isEditing = customSimpleButtonPermissions.Visible;
            SetPermisions();

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
            gcCertAgid.FieldName = nameof(CreateArticulMatrModel.Ag_id);
            gcCertDateCertificationApproval.FieldName = nameof(CreateArticulMatrModel.DateCertificationApproval);
            //запрет редактирования полей, которые не должны редактироваться напрямую пользователем, а заполняются через выбор из справочника и/или автоматически
            gcCertDateCertificationApproval.OptionsColumn.AllowEdit = false;

            gcKoddCompare.FieldName = nameof(SpArtPreviewModel.Kodd);
            gcGrupCompare.FieldName = nameof(SpArtPreviewModel.Grup);
            gcArticulCompare.FieldName = nameof(SpArtPreviewModel.Articul);
            gcModCompare.FieldName = nameof(SpArtPreviewModel.Mod);
            gcTMCompare.FieldName = nameof(SpArtPreviewModel.tmName);
            gcArhCompare.FieldName = nameof(SpArtPreviewModel.Arh);

        }

        private async void BindGost()
        {
            try
            {
                var ri = repositoryItemSearchLookUpEdit1;
                ri.DataSource = await _createArticulMatrService.GetGostAsync();
                ri.DisplayMember = nameof(GostModel.Id_gost);
                ri.ValueMember = nameof(GostModel.Id_gost);
                // Колонки выпадающего списка (по желанию)

                var view = ri.PopupView as DevExpress.XtraGrid.Views.Grid.GridView;
                if (view == null)
                    throw new InvalidOperationException("PopupView не GridView");
                view.OptionsView.ShowColumnHeaders = true;
                view.OptionsView.ShowIndicator = false;
                view.OptionsView.ShowAutoFilterRow = true; // ⭐ фильтр по колонкам
                view.OptionsBehavior.Editable = false;
                view.OptionsSelection.EnableAppearanceFocusedCell = false;
                view.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
                view.Columns.Clear();

                view.Columns.AddVisible(nameof(GostModel.Id_gost), "ID");
                view.Columns.AddVisible(nameof(GostModel.Name_gost), "Название");
                view.Columns.AddVisible(nameof(GostModel.Opi_gost), "Описание");
                //view.BestFitColumns();

                ri.NullText = ""; // что показывать, если значение null
                //ri.ShowHeader = false;
                //ri.ShowFooter = false;
                ri.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor; // запрет ввода, только выбор
                ri.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок ГОСТ");
                throw;
            }
        }
        private async void BindGostGrupp()
        {
            try
            {
                var ri = repositoryItemSearchLookUpEdit2;
                // после выбора госта - фильтрация групп по госту происходит в repositoryItemSearchLookUpEdit2_BeforePopup
                ri.DataSource = _gostGroupAll;
                ri.DisplayMember = nameof(GostGrupIzdViewModel.N_i);
                ri.ValueMember = nameof(GostGrupIzdViewModel.Ag_id);
                // Колонки выпадающего списка (по желанию)
                var view = ri.PopupView as DevExpress.XtraGrid.Views.Grid.GridView;
                if (view == null)
                    throw new InvalidOperationException("PopupView не GridView");
                view.OptionsView.ShowColumnHeaders = true;
                view.OptionsView.ShowIndicator = false;
                view.OptionsView.ShowAutoFilterRow = true; // ⭐ фильтр по колонкам
                view.OptionsBehavior.Editable = false;
                view.OptionsSelection.EnableAppearanceFocusedCell = false;
                view.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
                view.Columns.Clear();

                view.Columns.AddVisible(nameof(GostGrupIzdViewModel.Ag_id), "ID");
                view.Columns.AddVisible(nameof(GostGrupIzdViewModel.N_i), "Название");
                view.Columns.AddVisible(nameof(GostGrupIzdViewModel.Ag_name_sokr), "Сокращенное назв.");

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
        private void repositoryItemSearchLookUpEdit1_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            try
            {
                if (!e.AcceptValue) return; // если пользователь отменил выбор, не обновляем данные

                // сохраняем текущее редактирование, чтобы получить актуальное значение 
                var view = gridViewArtMatrEdit;
                view.PostEditor();
                view.UpdateCurrentRow();

                var currentItem = (CreateArticulMatrModel)_bindingSourceArtMatr.Current;

                //скидываем группу госта при изменении самого госта, чтобы не было "висячих" групп, не относящихся к выбранному госту
                currentItem.Ag_id = 0;

                view.PostEditor();
                view.UpdateCurrentRow();

            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при изменении госта (lookUpGost_EditValueChanged)");
                throw;
            }
        }

        private void repositoryItemSearchLookUpEdit2_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                var editor = gridViewArtMatrEdit.ActiveEditor as DevExpress.XtraEditors.SearchLookUpEdit;
                if (editor == null)
                    return;

                if (sender == null) return;

                var _currentItem = (CreateArticulMatrModel)_bindingSourceArtMatr.Current;
                var idGost = _currentItem.Id_gost;
                editor.Properties.DataSource = _gostGroupAll
                    .Where(x => x.Id_gost == idGost)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при открытии выпадающего списка групп ГОСТ");
                throw;
            }

        }

        private async void gridViewArtMatrEdit_DoubleClick(object sender, EventArgs e)
        {

            try
            {
                var view = sender as GridView;
                if (view == null) return;

                Point pt = view.GridControl.PointToClient(Control.MousePosition);
                GridHitInfo hit = view.CalcHitInfo(pt);
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
                        //_currentItem.DateCertificationApproval = null;
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
                //var view = gridArtMatr.FocusedView as GridView;
                //var currentRow = null as CreateArticulMatrModel ;

                //if (view.SelectedRowsCount == 1)
                //{
                //    int rowHandle = view.GetSelectedRows()[0];
                //    currentRow = view.GetRow(rowHandle) as CreateArticulMatrModel;
                //}

                articulControl1.ClearComparisonHighlight();   //очищаем подсветку сравнения при смене артикула в матрице, чтобы не было "висячей" подсветки от предыдущего сравнения
                _bsDetails.Clear();// очищаем детали от предыдущего сравнения
                var currentRow = _bindingSourceArtMatr.Current as CreateArticulMatrModel;// получаем текущую выбранную строку из матрицы
                if (currentRow == null)// если строка не выбрана, выходим из метода
                    return;

                _bindingSourceArticulCompare.DataSource = await _createArticulMatrService.GetArticulsForCompareAsync(currentRow.Articul);// загружаем данные для сравнения в другой грид

                string imagePath = await _matrixService.GetFileEskizNN(currentRow.Nn);// загружаем эскиз для текущего артикула матрицы
                pictureBoxMatrix.ImageLocation = string.IsNullOrWhiteSpace(imagePath) ? null : imagePath;// отображаем эскиз, если он есть, или очищаем картинку, если эскиза нет

                await fillCompareTable();

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке данных для сравнения артикула");
                throw;
            }
        }

        private async Task fillCompareTable()
        {
            var currentRow1 = _bindingSourceArticulCompare.Current as SpArtPreviewModel;// получаем текущую выбранную строку из грида сравнения
            string kod = currentRow1?.Kod;// извлекаем код артикула для загрузки деталей, если строка выбрана, или null, если строка не выбрана

            gridViewArtCompare.ShowLoadingPanel();// показываем индикатор загрузки, так как загрузка деталей может занять некоторое время
            _bsDetails?.Clear();// очищаем предыдущие детали, чтобы не было "висячих" данных от предыдущего сравнения, пока загружаются новые детали
            //          _bsDetails.DataSource = await _articulDataService.GetByKodAsync(kod);
            await CompareSelectedArticulAsync();// загружаем детали для выбранного артикула сравнения и выполняем сравнение с текущим артикулом матрицы, результат сравнения сохраняем в поле _lastCompareResult, чтобы при сохранении матрицы знать, нужно ли сохранять изменения или нет
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
                //var currentRow = _bindingSourceArticulCompare.Current as SpArticulPreviewModel;// получаем текущую выбранную строку из грида сравнения
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

                //SpArticulPreviewModel comparePreview = await _articulDataService.GetByKodAsync(kod);


                if (matrixRow == null || compareRow == null || string.IsNullOrWhiteSpace(compareRow.Kod))// если не выбрана строка для сравнения или в выбранной строке нет кода артикула для сравнения, очищаем детали и выходим из метода, так как нечего сравнивать
                {
                    _bsDetails.Clear();// очищаем детали, чтобы не было "висячих" данных от предыдущего сравнения, так как нет артикула для сравнения
                    _lastCompareResult = false;
                    return;
                }

                var details = await _articulDataService.GetByKodAsync(compareRow.Kod);// загружаем детали для выбранного артикула сравнения
                _bsDetails.DataSource = details;// устанавливаем источник данных для деталей, которые отображаются в articulControl1, при этом articulControl1 должен автоматически обновить отображение деталей, так как он привязан к _bsDetails
                _bsDetails.ResetBindings(false);// сбрасываем привязки, чтобы гарантировать обновление отображения деталей в articulControl1, так как мы изменили источник данных

                var compareItems = CreateArticulMatrComparisonBuilder.Build(matrixRow);// создаем список полей для сравнения на основе текущей строки матрицы, который будет использоваться в articulControl1 для сравнения и подсветки различий
                var result = articulControl1.CompareAndHighlight(compareItems);// выполняем сравнение и подсветку различий в articulControl1, результат сравнения сохраняем в переменной result, которая содержит информацию о том, совпадают ли артикулы полностью (IsMatch) и какие поля отличаются (Mismatches)
                                                                               // тут можно сохранить флаг в поле формы
                _lastCompareResult = result.IsMatch;// сохраняем результат сравнения в поле формы, чтобы при сохранении матрицы знать, нужно ли сохранять изменения или нет, так как если артикулы совпадают полностью, то сохранять изменения не нужно, так как они не изменились по сравнению с выбранным артикулом сравнения
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() +"213");
                _lastCompareResult = false;
            }

        }
    }
}
