using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using Org.BouncyCastle.Crypto;
using SewingProduction.Core.Models;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.Articul.Service;
using SewingProduction.Features.UserDistribution;
using SewingProduction.Features.UserDistribution.Class;
using SewingProduction.Features.UserDistribution.DataService;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SewingProduction.Features.Articul.Forms
{
    public partial class EditNaborSostavMatr : CustomForm
    {
        ArticulNaborSostavDataService _ANSDataService = new ArticulNaborSostavDataService();
        private List<PlanSezonAllModel> _PlanSezonAll = new();
        private List<ArtKomplektModel> _ArtKomplekt = new();
        private List<TovarCategoryModel> _Categories = new();
        private List<TovarCatDynsignModel> _Dynsigns = new();
        private readonly HashSet<string> _selectedNn = new(StringComparer.OrdinalIgnoreCase);
        //private BindingList<SpArticulNaborSostav> _ArticulNaborSostav = new();
        //private int? _lastNnLoaded = null;
        //private CancellationTokenSource _loadCts;
        private string _Kodd;
        private string _Json;
        public EditNaborSostavMatr(UserClass user, string kodd, string json) : base(user)
        {
            InitializeComponent();
            _Kodd = kodd;
            _Json = json;
        }
        public EditNaborSostavMatr(UserClass user) : base(user)
        {
            InitializeComponent();
        }

        #region Initialization
        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            await InitializeFormAsync();
        }
        private async Task InitializeFormAsync()
        {
            _Categories = await _ANSDataService.GetTovarCategory();
            _Dynsigns = await _ANSDataService.GetTovarCatDynsign();
            LoadPSA();
        }
        #endregion

        #region Загрузка данных
        private void gridViewPlanSezonAll_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0) return;

            var view = sender as GridView;
            var row = view?.GetRow(e.FocusedRowHandle) as PlanSezonAllModel;
            if (row == null) return;
            if (row.Nn == null) return;
            LoadInfoPSA(row);
            LoadInfoAK(row.Nn);
        }
        private void LoadInfoPSA(PlanSezonAllModel row)
        {
            customTextBoxArt.Text = row.Articul;
            customTextBoxMod.Text = row.Mod;
            customTextBoxNN.Text = row.Nn;
            customTextBoxMO.Text = row.Text_mo;
        }
        private async void LoadPSA()
        {
            _PlanSezonAll = await _ANSDataService.GetPlanSezonAllByKod(_Kodd);
            customGridControlPlanSezonAll.DataSource = _PlanSezonAll;
            //EnableEditorsInPSA();
            //EnableEditorsInAK();
            LoadInfoCatDynPSA();
            _selectedNn.Clear();
            foreach (var r in _PlanSezonAll)
            {
                if (!string.IsNullOrWhiteSpace(r.Nn))
                    _selectedNn.Add(r.Nn);
            }
        }
        private void LoadInfoCatDynPSA()
        {
            foreach (var r in _PlanSezonAll)
            {
                // Категория
                var cat = _Categories.FirstOrDefault(x => x.TCAT_ID == r.Tg_id_n);
                r.TCAT_CategoryName = cat?.TCAT_CategoryName;

                // Признак
                var dyn = _Dynsigns.FirstOrDefault(x => x.TCDS_ID == r.Tgm_id_n);
                r.TCDS_Name = dyn?.TCDS_Name;
            }
            gridViewPlanSezonAll.RefreshData();
        }
        private async void LoadInfoAK(string nn)
        {
            _ArtKomplekt = await _ANSDataService.GetArtKomplektByNn(nn);
            foreach (var r in _ArtKomplekt)
            {
                // Категория
                var cat = _Categories.FirstOrDefault(x => x.TCAT_ID == r.Tg_id_n);
                r.TCAT_CategoryName = cat?.TCAT_CategoryName;

                // Признак
                var dyn = _Dynsigns.FirstOrDefault(x => x.TCDS_ID == r.Tgm_id_n);
                r.TCDS_Name = dyn?.TCDS_Name;
            }
            customGridControlArtKomplekt.DataSource = _ArtKomplekt;
            gridViewArtKomplekt.RefreshData();
        }
        private void gridViewPlanSezonAll_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column != psaGridColumnCheck) return;

            var row = e.Row as PlanSezonAllModel;
            if (row == null || string.IsNullOrWhiteSpace(row.Nn))
            {
                if (e.IsGetData) e.Value = false;
                return;
            }

            if (e.IsGetData)
                e.Value = _selectedNn.Contains(row.Nn);

            if (e.IsSetData)
            {
                bool isChecked = e.Value != null && e.Value != DBNull.Value && Convert.ToBoolean(e.Value);
                if (isChecked) _selectedNn.Add(row.Nn);
                else _selectedNn.Remove(row.Nn);
            }
        }
        #endregion

        #region Динамический признак
        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var rowHandle = gridViewPlanSezonAll.FocusedRowHandle;

            var row = gridViewPlanSezonAll.GetRow(rowHandle) as PlanSezonAllModel;
            if (row == null || row.Nn == null) return;

            using var f = new EditNaborSostavPart(_user, row);
            if (f.ShowDialog() != DialogResult.OK) return;

            gridViewPlanSezonAll.RefreshRow(rowHandle);
            LoadPSA();
        }
        private void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var rowHandle = gridViewArtKomplekt.FocusedRowHandle;
            var row = gridViewArtKomplekt.GetRow(rowHandle) as ArtKomplektModel;
            if (row == null || row.Parent_nn == null) return;

            using var f = new EditNaborSostavPart(_user, row);
            if (f.ShowDialog() != DialogResult.OK) return;

            gridViewArtKomplekt.RefreshRow(rowHandle);
            //LoadPSA();
            //LoadInfoAK(rowPSA.Nn);
            //обновление строчки
        }
        #endregion

        #region Вспомогательные методы грида
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
        #endregion

        #region Select and save
        private List<string> GetSelectedNns()
        {
            return _selectedNn
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }
        private async void customButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                var Nns = GetSelectedNns();

                if (Nns.Count == 0)
                {
                    MessageBox.Show("Нет выбранных номеров", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(_Json))
                {
                    MessageBox.Show("Не выполнился предыдущий этап - Нет Json для применения изменений.",
                        "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                await _ANSDataService.UpdateNaborByNnJsonAsync(_Json, Nns);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

    }
}
