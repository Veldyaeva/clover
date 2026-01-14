using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.Features.UserDistribution.Class;
using SewingProduction.Features.UserDistribution.DataService;
using SewingProduction.Features.UserDistribution;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution.Models;
using SewingProduction.Features.Articul.Service;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.Articul.Models;
using System.Threading;

namespace SewingProduction.Features.Articul.Forms
{
    public partial class EditNaborSostavMatr : CustomForm
    {
        ArticulNaborSostavDataService _ANSDataService = new ArticulNaborSostavDataService();
        private int? _lastNnLoaded = null;
        private CancellationTokenSource _loadCts;
        private int? _kod;
        public EditNaborSostavMatr(UserClass user, int? Kod) : base(user)
        {
            InitializeComponent();
            _kod = Kod;
        }

        #region Initialization
        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            await InitializeFormAsync();
        }
        private async Task InitializeFormAsync()
        {
            customGridControlPlanSezonAll.DataSource = await _ANSDataService.GetPlanSezonAllByKod(_kod);
            EnableEditorsInPSA();
            EnableEditorsInAK();
        }
        #endregion
        private async void gridViewPlanSezonAll_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0) return;

            var view = sender as GridView;
            var row = view?.GetRow(e.FocusedRowHandle) as PlanSezonAllModel;
            if (row == null) return;
            if (row.nn == null) return;
            customGridControlArtKomplekt.DataSource = await _ANSDataService.GetArtKomplektByKod(row.nn);
        }

        private void customButtonSave_Click(object sender, EventArgs e)
        {

        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            EditNaborSostavPart f = new EditNaborSostavPart(_user);
            f.ShowDialog();
        }

        private void EnableEditorsInPSA()
        {
            // 1) Разрешаем редактирование на уровне View
            gridViewPlanSezonAll.OptionsBehavior.Editable = true;
            gridViewPlanSezonAll.OptionsBehavior.ReadOnly = false;

            // 2) Чтобы кнопки/чекбоксы были кликабельны
            gridViewPlanSezonAll.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;

            // 3) Разрешаем редактирование конкретных колонок
            psaGridColumnCheck.OptionsColumn.AllowEdit = true;
            psaGridColumnCheck.OptionsColumn.ReadOnly = false;

            psaGridColumnButton.OptionsColumn.AllowEdit = true;
            psaGridColumnButton.OptionsColumn.ReadOnly = false;

            // 4) задать тип
            psaGridColumnCheck.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
        }
        private void EnableEditorsInAK()
        {
            // 1) Разрешаем редактирование на уровне View
            gridViewArtKomplekt.OptionsBehavior.Editable = true;
            gridViewArtKomplekt.OptionsBehavior.ReadOnly = false;

            // 2) Чтобы кнопки/чекбоксы были кликабельны
            gridViewArtKomplekt.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;

            // 3) Разрешаем редактирование конкретных колонок
            akGridColumnButton.OptionsColumn.AllowEdit = true;
            akGridColumnButton.OptionsColumn.ReadOnly = false;

            // 4) задать тип
            akGridColumnButton.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
        }
    }
}
