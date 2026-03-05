using Dapper;
using DevExpress.Mvvm.Native;
using Org.BouncyCastle.Crypto;
using SewingProduction.Core.Models;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.Articul.Service;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.Articul.Forms
{
    public partial class CreateArticulMatrForm : CustomForm

    {
        private DatabaseHelper _dbHelper;
        private DbService _dbService;
        private CreateArticulMatrService _createArticulMatrService = new CreateArticulMatrService();
        private readonly ILogger _logger = new FileLogger();


        private BindingSource _bindingSourceArtMatr;
        //private BindingSource _bindingSourceGostGrupAll;
        private List<GostGrupIzdViewModel> _gostGroupAll;
        private List<GostGrupIzdViewModel> _gostGroup;

        private BindingSource _bindingSourceGroupGost = new BindingSource();

        //private BindingSource _bindingSourceGostGrup;

        public CreateArticulMatrForm(UserClass user)
        {
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);

            InitializeComponent();

            _bindingSourceArtMatr = new BindingSource { };

            //if (gridArtMatr != null) gridArtMatr.DataSource = _bindingSourceArtMatr;
            gridArtMatr.DataSource = _bindingSourceArtMatr;
            gridArtMatrEdit.DataSource = _bindingSourceArtMatr;

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


            InitializeBindings();
            BindGost();
            BindGostGrupp();

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

            //поля уточнения для отд сертификации
            gcCertidGost.FieldName = nameof(CreateArticulMatrModel.idGostAppr);
            gcCertAgid.FieldName = nameof(CreateArticulMatrModel.agIdAppr);
            gcCertDateCertificationApproval.FieldName = nameof(CreateArticulMatrModel.dateCertificationApproval);


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

                //oбновление фильтра группы по госту
                var idGost = currentItem.idGostAppr;
                //if (currentItem.idGostAppr == 0 || currentItem.idGostAppr == null)
                //{
                    currentItem.agIdAppr = 0;
                //    return;
                //}

                view.PostEditor();
                view.UpdateCurrentRow();

                //BindGostGrupp();
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при изменении госта (lookUpGost_EditValueChanged)");
                throw;
            }
        }

        private void repositoryItemSearchLookUpEdit2_BeforePopup(object sender, EventArgs e)
        {
            var editor = gridViewArtMatrEdit.ActiveEditor as DevExpress.XtraEditors.SearchLookUpEdit;
            if (editor == null)
                return;

            if (sender == null) return;
            var _currentItem = (CreateArticulMatrModel)_bindingSourceArtMatr.Current;
            var idGost = _currentItem.idGostAppr;
            editor.Properties.DataSource = _gostGroupAll
                .Where(x => x.Id_gost == idGost)
                .ToList();
            
        }
    }
}
