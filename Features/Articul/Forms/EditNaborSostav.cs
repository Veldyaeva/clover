using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraDiagram.Base;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Filtering;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraSpreadsheet.Model;
using SewingProduction.Core.Class;
using SewingProduction.Core.Models;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.Articul.Service;
using SewingProduction.Features.UserDistribution.Helpers;
using System.Drawing;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.BandedGrid;
using Org.BouncyCastle.Crypto;
using SewingProduction.Features.UserDistribution.Forms;

namespace SewingProduction.Features.Articul.Forms
{
    public partial class EditNaborSostav : CustomForm
    {
        ArticulNaborSostavDataService _ANSDataService = new ArticulNaborSostavDataService();
        ArticulDataService _articulDataService = new ArticulDataService();
        SpArticulNaborSostav currentItem;
        BindingList<SpArticulNaborSostav> _listCurrent;
        BindingList<SpArticulNaborSostav> _listOld;
        BindingSource _bsOld = new BindingSource();
        int oldAgIdBeforeEdit = 0;
        ArticulModel articulNabor_old = new ArticulModel();
        ArticulModel articulNabor_new = new ArticulModel();
        List<GostRazmerNabViewModel> _razmNaborForGost;
        List<GostRazmerNabViewModel> _razmSostavForGost;
        public EditNaborSostav()
        {
            InitializeComponent();
        }
        public EditNaborSostav(UserClass user, string Kod) : base(user)
        {
            articulNabor_old.Kod = Kod;
            InitializeComponent();
            DisableSearchForGroupEditor();
        }
        #region Загрузка формы
        private async void customGridControl1_Load(object sender, EventArgs e)
        {
            await LoadNabor();
            await LoadSostav();
            await LoadAllGroupsAsync();
            await SetupPictursBox();
            HideTechnicalColumns();
            ConfigureGridViewNaborColumns(GridViewNabor);
            ConfigureGridViewNaborColumns(GridViewNabor_Old);
            ArticulNaborColumns();
            await ArticulNaborColumnsOld(articulNabor_old.Id_gost);
            SetupSearchLookUpEditGost();
            ValidateGridState();
        }
        private void DisableSearchForGroupEditor()
        {
            repositoryItemSearchLookUpEditGrupView.OptionsFind.AlwaysVisible = false;
            repositoryItemSearchLookUpEditGrupView.OptionsFind.ShowFindButton = false;
            repositoryItemSearchLookUpEditGrupView.OptionsFind.ClearFindOnClose = true;
            repositoryItemSearchLookUpEditGrupView.OptionsFind.FindNullPrompt = "";
            repositoryItemSearchLookUpEditGrupView.OptionsView.ShowAutoFilterRow = false;
            repositoryItemSearchLookUpEditGrupView.OptionsCustomization.AllowFilter = false;
            repositoryItemSearchLookUpEditGrupView.OptionsFilter.AllowFilterEditor = false;
        }
        private async Task LoadNabor()
        {
            articulNabor_old = await _articulDataService.GetArtByKodAsync(articulNabor_old?.Kod);
            assortModelBindingSource.DataSource = await _ANSDataService.GetAssortAsync();
            tvnModelBindingSource.DataSource = await _ANSDataService.GetTvnAsync();

            _bsGostForNabor.DataSource = await _ANSDataService.GetGostNaborAsync();

            _bsGrupForNabor.DataSource = await _ANSDataService.GetGostGrupIzdNaborAsync(new int[] { articulNabor_old.Id_gost }, 3);

            currentItem = spArticulNaborSostavBindingSource.Current as SpArticulNaborSostav;
            if (currentItem != null)
            {
                oldAgIdBeforeEdit = currentItem.Ag_id;
            }
        }
        private async Task LoadSostav()
        {
            var list = await _ANSDataService.GetByKodAsync(articulNabor_old.Kod);
            _listCurrent = new BindingList<SpArticulNaborSostav>(list);
            _listOld = new BindingList<SpArticulNaborSostav>(
                list.Select(x => new SpArticulNaborSostav
                {
                    Ans_id = x.Ans_id,
                    Txt_v = x.Txt_v,
                    Kod = x.Kod,
                    Ta_id = x.Ta_id,
                    Tk_id = x.Tk_id,
                    Tk_name = x.Tk_name.Trim(),
                    Id_gost = x.Id_gost,
                    N_i = x.N_i.Trim(),
                    Ag_id = x.Ag_id,
                    Sostav = x.Sostav,
                    Id_razm_nab = x.Id_razm_nab,
                    Razm = x.Razm.Trim(),
                    Razm_all = x.Razm_all
                }).ToList()
            );

            // --- привязки ---
            spArticulNaborSostavBindingSource.DataSource = _listCurrent; // редактируемая часть
            _bsOld.DataSource = _listOld; // снимок
            customGridControlNabor.DataSource = spArticulNaborSostavBindingSource;
            customGridControlNabor_Old.DataSource = _bsOld;

            _bsGostForSostav.DataSource = await _ANSDataService.GetGostSostavAsync();
            //_bsGrupForSostav.DataSource = await _ANSDataService.GetGostGrupIzdNaborAsync(articulNabor.Id_gost);
            //GridViewNabor.ExpandAllGroups();
            //GridViewNabor_Old.ExpandAllGroups();
        }
        private async Task LoadAllGroupsAsync()
        {
            var gostIds = CollectAllGostIds();

            if (gostIds.Length == 0)
            {
                _allGroups = new List<GostGrupIzdViewModel>();
                repositoryItemSearchLookUpEditGrup.DataSource = _allGroups;
                return;
            }

            _allGroups = await _ANSDataService.GetGostGrupIzdNaborAsync(gostIds, null);

            repositoryItemSearchLookUpEditGrup.DataSource = _allGroups;
            repositoryItemSearchLookUpEditGrup.DisplayMember = "N_i";
            repositoryItemSearchLookUpEditGrup.ValueMember = "Ag_id";
            repositoryItemSearchLookUpEditGrup.NullText = "";
        }
        private int[] CollectAllGostIds()
        {
            var view = GridViewNabor;
            var ids = new HashSet<int>();
            for (int i = 0; i < view.DataRowCount; i++)
            {
                var row = view.GetRow(i) as SpArticulNaborSostav;
                if (row == null) continue;

                if (row.Id_gost > 0)
                    ids.Add(row.Id_gost);
            }
            return ids.ToArray();
        }
        private async Task SetupPictursBox()
        {
            //customPictureBoxNabor.ImagePath = await _articulDataService.GetFileEskizForKod(articulNabor_old.Kod);
            customPictureBoxNabor.Image = Image.FromFile(await _articulDataService.GetFileEskizForKod(articulNabor_old.Kod));
        }
        private async Task ArticulNaborColumnsOld(int? Id_gost)
        {
            var gostOld = (await _ANSDataService.GetGostNaborAsync(Id_gost)).FirstOrDefault();
            if (gostOld == null) return;
            customTextBoxArtN_Old.Text = articulNabor_old.Articul;
            customTextBoxKodGost_Old.Text = gostOld.Id_gost.ToString();
            customTextBoxGostN_Old.Text = gostOld.Name_gost;
            customTextBoxOpiGost_Old.Text = gostOld.Opi_gost;
            customTextBoxGrupN_Old.Text = articulNabor_old.Grup;
        }
        private void ArticulNaborColumns()
        {
            customTextBoxArtN.Text = articulNabor_old.Articul;
            customSearchLookUpEditGostN.EditValue = articulNabor_old.Id_gost.ToString();
            customSearchLookUpEditGrupN.EditValue = articulNabor_old.Ag_id.ToString();
        }
        private void HideTechnicalColumns()
        {
            GridViewNabor_Old.BeginUpdate();
            try
            {
                GridViewNabor_Old.Columns["Ans_id"].Visible = false;
                GridViewNabor_Old.Columns["Ta_id"].Visible = false;
                GridViewNabor_Old.Columns["Tk_id"].Visible = false;
                GridViewNabor_Old.Columns["Ag_id"].Visible = true;
                GridViewNabor_Old.Columns["Id_razm_nab"].Visible = false;
                GridViewNabor_Old.Columns["Kod"].Visible = false;
                GridViewNabor_Old.ClearGrouping();
                GridViewNabor_Old.Columns["Razm_all"].GroupIndex = 0; // группировка по колонке размера
                GridViewNabor_Old.ExpandAllGroups(); // раскрыть группы при загрузке
                GridViewNabor_Old.OptionsView.ShowGroupPanel = true; //

                GridViewNabor.Columns["Ag_id"].Visible = true;
                GridViewNabor.ClearGrouping();
                GridViewNabor.Columns["Razm_all"].GroupIndex = 0;
                GridViewNabor.Columns["Razm_all"].Visible = false;
                GridViewNabor.ExpandAllGroups();
                GridViewNabor.OptionsView.ShowGroupPanel = true;
            }
            finally
            {
                GridViewNabor_Old.EndUpdate();

                customButtonSaveNabor.Enabled = false;
                customCheckBoxVerified.Checked = false;

                customListBoxRazm.Visible = false;
            }
        }

        private void ConfigureGridViewNaborColumns(BandedGridView bGridView)
        {
            // Конфигурация колонок задана в Designer.cs
            if (bGridView == null)
                return;
            // 1) Единая скрытая колонка с готовым заголовком группы
            var headerCol = bGridView.Columns.ColumnByFieldName("__Header");
            if (headerCol == null)
            {
                headerCol = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                {
                    FieldName = "__Header",
                    Caption = "Header",
                    UnboundType = DevExpress.Data.UnboundColumnType.String,
                    UnboundExpression = "Concat([Kod], '  |  Размер набора: ', [Razm_all])",
                    Visible = false,
                    OptionsColumn = { ShowInCustomizationForm = false }
                };
                bGridView.Columns.Add(headerCol);
            }

            // 2) Сбрасываем прошлую группировку и группируем только по __Header
            bGridView.BeginUpdate();
            try
            {
                bGridView.ClearGrouping();

                headerCol.GroupIndex = 0;

                // 3) Внешний вид группы — показываем только текст, без имён полей
                bGridView.GroupFormat = "{1}";
                bGridView.OptionsView.ShowGroupedColumns = false;
                bGridView.OptionsView.ShowGroupPanel = false;
                bGridView.OptionsBehavior.AutoExpandAllGroups = true;
            }
            finally
            {
                bGridView.EndUpdate();
            }
        }
        private void SetupSearchLookUpEditGost()
        {
            GridViewNabor.OptionsBehavior.Editable = true;
            GridViewNabor.OptionsBehavior.ReadOnly = false;

            GridViewNabor.RefreshData();
        }
        #endregion

        #region Обработка ГОСТ НАБОРА (верхняя часть формы)
        private async void customSearchLookUpEditGostN_EditValueChanged(object sender, EventArgs e)
        {
            int idGostNabor = Convert.ToInt32(customSearchLookUpEditGostN.EditValue);
            var grupList = await _ANSDataService.GetGostGrupIzdNaborAsync(new int[] { idGostNabor }, 3);
            _bsGrupForNabor.DataSource = grupList;
            if (grupList != null && grupList.Count == 1)
            {
                var only = grupList[0];
                customSearchLookUpEditGrupN.EditValue = only.Ag_id;
                customSearchLookUpEditGrupN.DoValidate();
            }
            else
            {
                customSearchLookUpEditGrupN.EditValue = null;
            }
            await AutoApplyIdGostAfterChangingNabor(idGostNabor);
            //await LoadAllGroupsAsync();
            _razmNaborForGost = await _ANSDataService.GetGostRazmerNaborAsync(idGostNabor);

            customListBoxRazm.DataSource = null;
            if (_razmNaborForGost != null && _razmNaborForGost.Count > 0)
            {
                customListBoxRazm.DataSource = _razmNaborForGost;
                customListBoxRazm.DisplayMember = "Razm";
                customListBoxRazm.ValueMember = "Id_razmer";
            }
        }
        private async Task AutoApplyIdGostAfterChangingNabor(int idGostNabor)
        {
            if (idGostNabor == 0) return;

            var allGosts = await _ANSDataService.GetGostSostavAsync(idGostNabor);

            var view = GridViewNabor;
            WithGridViewUpdate(view =>
            {
                for (int i = 0; i < view.DataRowCount; i++)
                {
                    var row = view.GetRow(i) as SpArticulNaborSostav;
                    if (row == null) continue;

                    int tk = row.Tk_id;

                    var filtered = allGosts
                        .Where(g => g.Tk_id == tk)
                        .ToList();

                    if (filtered.Count == 1)
                    {
                        int autoGost = filtered[0].Id_gost;
                        view.SetRowCellValue(i, "Id_gost", autoGost);
                    }
                    else
                    {
                        view.SetRowCellValue(i, "Id_gost", DBNull.Value);
                    }
                }
            });
            await ApplyRazmByGostAsync(idGostNabor);
        }
        #endregion

        #region Обработка ГОСТ СОСТАВА (Id_gost в гриде)
        private int? selectedGostId = null;
        private List<GostGrupIzdViewModel> _allGroups = new();
        private void repositoryItemSearchLookUpEditGost_Popup(object sender, EventArgs e)
        {
            if (GridViewNabor.FocusedColumn == null || GridViewNabor.FocusedColumn.FieldName != "Id_gost") return;

            var row = GridViewNabor.GetFocusedRow() as SpArticulNaborSostav;
            if (row == null) return;

            var idGostNabor = customSearchLookUpEditGostN.EditValue.ToIntN();
            if (idGostNabor == null) return;

            var editor = GridViewNabor.ActiveEditor as DevExpress.XtraEditors.SearchLookUpEdit;
            if (editor == null) return;

            var popupView = editor.Properties.PopupView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (popupView != null)
            {
                popupView.ActiveFilterString = $"[Tk_id] = {row.Tk_id} AND [Id_glav_gost] = {idGostNabor}";
            }
            popupView.FocusedRowChanged -= PopupView_FocusedRowChanged;
            popupView.FocusedRowChanged += PopupView_FocusedRowChanged;
        }
        private void PopupView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var popupView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (popupView == null) return;

            var rowGost = popupView.GetFocusedRow() as GostModel;
            if (rowGost == null) return;
            selectedGostId = rowGost.Id_gost;
        }
        private void repositoryItemSearchLookUpEditGost_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            if (selectedGostId == null || selectedGostId == 0) return;

            var view = GridViewNabor;
            var currentRow = view.GetFocusedRow() as SpArticulNaborSostav;
            if (currentRow == null) return;

            int tkId = currentRow.Tk_id;
            int agId = currentRow.Ag_id;

            WithGridViewUpdate(view =>
            {
                for (int i = 0; i < view.DataRowCount; i++)
                {
                    var row = view.GetRow(i) as SpArticulNaborSostav;
                    if (row == null) continue;

                    if (row.Tk_id == tkId && row.Ag_id == agId)
                    {
                        view.SetRowCellValue(i, "Id_gost", selectedGostId.Value);
                    }
                }
            });
        }
        #endregion

        #region Обработка ГРУПП СОСТАВА (Ag_id в гриде)
        private GostGrupIzdViewModel selectedGrupId = null;
        private void repositoryItemSearchLookUpEditGrup_Popup(object sender, EventArgs e)
        {
            if (GridViewNabor.FocusedColumn == null || GridViewNabor.FocusedColumn.FieldName != "Ag_id")
                return;

            var row = GridViewNabor.GetFocusedRow() as SpArticulNaborSostav;
            if (row == null) return;

            int tkId = row.Tk_id;

            selectedGostId = row.Id_gost;

            if (selectedGostId == null || selectedGostId == 0) return;

            var editor = GridViewNabor.ActiveEditor as DevExpress.XtraEditors.SearchLookUpEdit;
            if (editor == null) return;

            var popupView = editor.Properties.PopupView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (popupView == null) return;

            popupView.ActiveFilterString = $"[Tk_id] = {tkId} AND [Id_gost] = {selectedGostId}";

            popupView.FocusedRowChanged -= GrupPopupView_FocusedRowChanged;
            popupView.FocusedRowChanged += GrupPopupView_FocusedRowChanged;
        }
        private void GrupPopupView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var popupView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (popupView == null) return;

            selectedGrupId = popupView.GetFocusedRow() as GostGrupIzdViewModel;
            if (selectedGrupId == null) return;
        }
        private void repositoryItemSearchLookUpEditGrup_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            if (selectedGrupId == null || selectedGrupId.Ag_id == 0) return;

            var view = GridViewNabor;
            var currentRow = view.GetFocusedRow() as SpArticulNaborSostav;
            if (currentRow == null) return;

            int tkId = currentRow.Tk_id;

            WithGridViewUpdate(view =>
            {
                for (int i = 0; i < view.DataRowCount; i++)
                {
                    var row = view.GetRow(i) as SpArticulNaborSostav;
                    if (row == null) continue;

                    if (row.Tk_id == tkId && row.Id_gost == currentRow.Id_gost)
                    {
                        view.SetRowCellValue(i, "Ag_id", selectedGrupId.Ag_id);
                        row.Ag_id = selectedGrupId.Ag_id;
                        row.N_i = selectedGrupId.N_i;
                    }
                }
            });
        }
        #endregion

        #region Основной обработчик GridView + позиционирование
        private async void GridViewNabor_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.FieldName)
            {
                case "Sostav":
                    ApplySostavToSameTkGost(e.RowHandle);
                    break;
                case "Id_gost":
                    await ApplyGrupsToSameTkGost(e);
                    break;
            }
        }
        private async Task ApplyGrupsToSameTkGost(DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            await LoadAllGroupsAsync();

            var row = GridViewNabor.GetRow(e.RowHandle) as SpArticulNaborSostav;
            if (row == null) return;

            int gost = row.Id_gost;
            //if (selectedGostId == gost) return;
            int tk = row.Tk_id;

            var groups = GetGroupsForRow(row);

            // Старые значения
            var oldRow = _bsOld.Cast<SpArticulNaborSostav>()
                               .FirstOrDefault(x => x.Ans_id == row.Ans_id);
            WithGridViewUpdate(view =>
            {
                if (TryAutoSelectGroup(groups, row, e.RowHandle)) return;

                if (TryRestoreGroup(groups, oldRow, row, e.RowHandle)) return;

                ClearGroup(row, e.RowHandle);
            });
        }

        private void ApplySostavToSameTkGost(int rowHandle)
        {
            var view = GridViewNabor;
            var row = view.GetRow(rowHandle) as SpArticulNaborSostav;
            if (row == null) return;

            string newSostav = row.Sostav;
            int tk = row.Tk_id;
            int gost = row.Id_gost;

            GridViewNabor.CellValueChanged -= GridViewNabor_CellValueChanged;
            WithGridViewUpdate(v =>
            {
                for (int i = 0; i < v.DataRowCount; i++)
                {
                    var r = v.GetRow(i) as SpArticulNaborSostav;
                    if (r == null) continue;

                    if (r.Tk_id == tk && r.Id_gost == gost)
                    {
                        v.SetRowCellValue(i, "Sostav", newSostav);
                        r.Sostav = newSostav;
                    }
                }
            });
            GridViewNabor.CellValueChanged += GridViewNabor_CellValueChanged;
        }
        private void gridViewNabor_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;

            var other = view == GridViewNabor ? GridViewNabor_Old : GridViewNabor;

            if (view.IsGroupRow(e.FocusedRowHandle))
            {
                string header = view.GetGroupRowValue(e.FocusedRowHandle)?.ToString();
                if (!string.IsNullOrEmpty(header))
                    FocusGroupInOtherGrid(other, header);

                return;
            }

            SyncGridSelection(view, other, "Ans_id", e.FocusedRowHandle);
        }

        public void SyncGridSelection(GridView sourceView, GridView otherView, string fieldName, int focusedRowHandle)
        {
            if ((sourceView == null) || (otherView == null) || (focusedRowHandle < 0)) return;

            object value = sourceView.GetRowCellValue(focusedRowHandle, fieldName);

            if (value == null) return;

            sourceView.BeginSelection();
            sourceView.ClearSelection();

            int srcMatches = 0;
            for (int i = 0; i < sourceView.DataRowCount; i++)
            {
                var v = sourceView.GetRowCellValue(i, fieldName);

                if (v != null && v.Equals(value))
                {
                    srcMatches++;
                    sourceView.SelectRow(i);
                }
            }
            sourceView.EndSelection();

            otherView.BeginSelection();
            otherView.ClearSelection();

            int otherMatches = 0;
            int firstMatchRow = -1;

            for (int i = 0; i < otherView.DataRowCount; i++)
            {
                var v = otherView.GetRowCellValue(i, fieldName);

                if (v != null && v.Equals(value))
                {
                    if (firstMatchRow < 0)
                        firstMatchRow = i;

                    otherMatches++;
                    otherView.SelectRow(i);
                }
            }
            otherView.EndSelection();

            if (firstMatchRow >= 0)
            {
                otherView.FocusedRowHandle = firstMatchRow;
                otherView.MakeRowVisible(firstMatchRow);
            }
        }
        private void GridViewNabor_FocusedGroupChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var view = sender as BandedGridView;

            // Проверяем: мы выделили группу?
            if (!view.IsGroupRow(e.FocusedRowHandle))
                return;

            // Получаем текст группы (__Header)
            string header = view.GetGroupRowValue(e.FocusedRowHandle)?.ToString();
            if (string.IsNullOrEmpty(header))
                return;

            // Позиционируем старый грид
            FocusGroupInOtherGrid(GridViewNabor_Old, header);
        }
        private void FocusGroupInOtherGrid(BandedGridView otherView, string headerValue)
        {
            if (otherView == null || string.IsNullOrEmpty(headerValue))
                return;

            for (int handle = -1; handle >= -100; handle--)
            {
                if (!otherView.IsValidRowHandle(handle))
                    break;

                if (otherView.IsGroupRow(handle))
                {
                    var groupHeader = otherView.GetGroupRowValue(handle)?.ToString();
                    if (groupHeader == headerValue)
                    {
                        otherView.FocusedRowHandle = handle;
                        otherView.MakeRowVisible(handle);
                        return;
                    }
                }
            }
        }


        #endregion

        #region Вспомогательные методы грида
        /// <summary>
        /// Если для строки доступна ровно одна группа — ставим автоматически.
        /// Возвращает true, если авто-выбор выполнен.
        /// </summary>
        private bool TryAutoSelectGroup(List<GostGrupIzdViewModel> groups,
            SpArticulNaborSostav row, int rowHandle)
        {
            if (groups.Count != 1) return false;

            var g = groups[0];
            row.Ag_id = g.Ag_id;
            row.N_i = g.N_i;

            GridViewNabor.SetRowCellValue(rowHandle, "Ag_id", g.Ag_id);
            return true;
        }
        /// <summary>
        /// Пытается восстановить старую группу oldRow, если она есть в текущем списке.
        /// Возвращает true, если восстановили.
        /// </summary>
        private bool TryRestoreGroup(List<GostGrupIzdViewModel> groups,
            SpArticulNaborSostav oldRow, SpArticulNaborSostav row, int rowHandle)
        {
            if (oldRow == null || oldRow.Ag_id <= 0) return false;

            var match = groups.FirstOrDefault(g => g.Ag_id == oldRow.Ag_id);
            if (match == null) return false;

            row.Ag_id = match.Ag_id;
            row.N_i = match.N_i;

            GridViewNabor.SetRowCellValue(rowHandle, "Ag_id", match.Ag_id);
            return true;
        }
        /// <summary>
        /// Полностью очищает Ag_id и N_i в строке.
        /// </summary>
        private void ClearGroup(SpArticulNaborSostav row, int rowHandle)
        {
            row.Ag_id = 0;
            row.N_i = null;
            GridViewNabor.SetRowCellValue(rowHandle, "Ag_id", 0);
        }
        /// <summary>
        /// Возвращает список групп, подходящих для строки (Id_gost + Tk_id).
        /// </summary>
        private List<GostGrupIzdViewModel> GetGroupsForRow(SpArticulNaborSostav row)
        {
            int gost = row.Id_gost;
            int tk = row.Tk_id;

            return _allGroups
                .Where(g => g.Id_gost == gost && g.Tk_id == tk)
                .ToList();
        }
        /// <summary>
        /// Унифицированное обновление GridViewNabor:
        /// Внутрь передаём только логику изменения строк.
        /// </summary>
        private void WithGridViewUpdate(Action<DevExpress.XtraGrid.Views.BandedGrid.BandedGridView> action)
        {
            var view = GridViewNabor;
            if (view == null || action == null) return;

            view.BeginDataUpdate();
            try
            {
                action(view);
            }
            finally
            {
                view.EndDataUpdate();
            }

            view.PostEditor();
            view.UpdateCurrentRow();
            spArticulNaborSostavBindingSource.EndEdit();
            view.RefreshData();
            ValidateGridState();
            GridViewNabor.Columns["Razm_all"].Visible = false;
        }

        #endregion

        #region Размеры по ГОСТу

        private async Task ApplyRazmByGostAsync(int idGostNabor)
        {
            if (!(await IsSameRazmGrid(idGostNabor)))
            {
                MessageBox.Show(
                    "Размерная сетка не совпадает. Выберите размеры вручную.",
                    "Внимание",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            if (_razmSostavForGost == null || _razmSostavForGost.Count == 0) return;

            var view = GridViewNabor;

            WithGridViewUpdate(v =>
            {
                for (int i = 0; i < v.DataRowCount; i++)
                {
                    var row = v.GetRow(i) as SpArticulNaborSostav;
                    if (row == null) continue;

                    int newGost = row.Id_gost;
                    int id_razm_nab = row.Id_razm_nab;

                    // ищем подходящий размер
                    var match = _razmSostavForGost.FirstOrDefault(r =>
                           r.Id_gost == newGost
                        && r.Id_razmer == id_razm_nab);

                    if (match != null)
                    {
                        row.Razm = match.Razm;
                        v.SetRowCellValue(i, "Razm", match.Razm);
                    }
                    else
                    {
                        //row.Razm = null;
                        v.SetRowCellValue(i, "Razm", "Ошибка");
                    }
                }
            });
        }
        private async Task<bool> IsSameRazmGrid(int idGostNabor)
        {
            var oldList = _razmSostavForGost?.ToList();
            _razmSostavForGost = await _ANSDataService.GetGostRazmerSostAsync(idGostNabor);

            if (oldList == null)
                return true;
            if (_razmSostavForGost == null)
                return false;

            var oldIds = oldList
                .Select(x => x.Id_razmer)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            var newIds = _razmSostavForGost
                .Select(x => x.Id_razmer)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            if (oldIds.Count != newIds.Count)
                return false;

            for (int i = 0; i < oldIds.Count; i++)
                if (oldIds[i] != newIds[i])
                    return false;

            return true;
        }

        #endregion

        #region Проверка полей
        bool changedRazmer = false;
        bool changedSostav = false;
        private async void ValidateGridState()
        {
            customCheckBoxVerified.Checked = false;
            customCheckBoxVerified.Enabled = false;
            cLabelInfo.ForeColor = System.Drawing.Color.Red;

            var view = GridViewNabor;
            if (view.DataRowCount == 0)
            {
                cLabelInfo.Text = "Нет строк в составе";
                return;
            }

            int idGostNabor = Convert.ToInt32(customSearchLookUpEditGostN.EditValue);
            int idGrupNabor = Convert.ToInt32(customSearchLookUpEditGrupN.EditValue);
            var allGosts = await _ANSDataService.GetGostSostavAsync(idGostNabor);

            bool invalidRazm = false;
            bool emptySostav = false;
            bool emptyGost = false;
            bool invalidGost = false;
            bool emptyGroupNab = false;
            bool emptyGroupSost = false;
            bool emptyRazmer = false;

            for (int i = 0; i < view.DataRowCount; i++)
            {
                var row = view.GetRow(i) as SpArticulNaborSostav;
                if (row == null) continue;

                if (string.IsNullOrWhiteSpace(row.Sostav))
                    emptySostav = true;

                if (idGrupNabor <= 0)
                    emptyGroupNab = true;

                if (row.Id_gost <= 0)
                {
                    emptyGost = true;
                }
                else
                {
                    bool exists = allGosts.Any(g => g.Id_gost == row.Id_gost);
                    if (!exists)
                        invalidGost = true;
                }

                if (row.Ag_id <= 0)
                    emptyGroupSost = true;

                if (row.Id_razm_nab <= 0 || string.IsNullOrWhiteSpace(row.Razm))
                    emptyRazmer = true;

                bool mismatch = _razmNaborForGost?.Any(r => r.Razm == row.Razm_all) == false;
                if (mismatch)
                    invalidRazm = true;
            }

            if (invalidRazm)
            {
                cLabelInfo.Text = "Размеры не соответсвуют ГОСТу!";
                return;
            }
            if (emptyGost)
            {
                cLabelInfo.Text = "Не выбран ГОСТ состава!";
                return;
            }
            if (invalidGost)
            {
                cLabelInfo.Text = "Выбран недопустимый ГОСТ состава!";
                return;
            }
            if (emptyGroupNab)
            {
                cLabelInfo.Text = "Поле группа набора не заполнено!";
                return;
            }
            if (emptyGroupSost)
            {
                cLabelInfo.Text = "Не выбрана группа по ГОСТу состава!";
                return;
            }
            if (emptySostav)
            {
                cLabelInfo.Text = "Поле Состав не заполнено!";
                return;
            }
            if (emptyRazmer)
            {
                cLabelInfo.Text = "Поле Размер не заполнено!";
                return;
            }
            cLabelInfo.Text = "Готово к изменениям!";
            customCheckBoxVerified.Enabled = true;
            cLabelInfo.ForeColor = System.Drawing.Color.Green;
        }

        #endregion

        #region Обработчики кнопок
        private async void customButtonSaveNabor_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (customCheckBoxVerified.Checked == false) return;
            //    GridViewNabor.PostEditor();
            //    GridViewNabor.UpdateCurrentRow();
            //    spArticulNaborSostavBindingSource.EndEdit();
                currentItem = spArticulNaborSostavBindingSource.Current as SpArticulNaborSostav;
            //    if (currentItem == null)
            //    {
            //        MessageBox.Show("Нет выбранной записи для сохранения!", "Внимание",
            //            MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //    if (_ANSDataService.CheckPovt(currentItem.Kod))
            //    {
            //        MessageBox.Show("Изменение невозможно! дубликаты в описании! обратитесь к администратору!", "Внимание",
            //            MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }

            //    if (_ANSDataService.CheckOpis(currentItem.Kod))
            //    {
            //        var result = MessageBox.Show($"Набор уже описан, изменения применятся на весь размерный ряд!,Вы уверены что хотите продолжить?", "Подтверждение",
            //            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        if (result != DialogResult.Yes)
            //            return;
            //    }

            //    int newGostMain = Convert.ToInt32(customSearchLookUpEditGostN.EditValue);
            //    int? newGroupMain = Convert.ToInt32(customSearchLookUpEditGrupN.EditValue);

            //    await _ANSDataService.UpdateNaborJsonAsync(
            //        currentItem.Kod.Substring(0, 7),
            //        articulNabor_old.Id_gost,
            //        newGostMain,
            //        articulNabor_old.Ag_id,
            //        newGroupMain,
            //        _listCurrent.ToList(),
            //        _listOld.ToList());

            //    customGridControlNabor_Old.DataSource = await _ANSDataService.GetByKodAsync(articulNabor_old.Kod);
            //    GridViewNabor.ExpandAllGroups();
            //    GridViewNabor_Old.ExpandAllGroups();
            //    oldAgIdBeforeEdit = currentItem.Ag_id;
            //    await Task.Delay(50);
            //    await LoadNabor();
            //    await ArticulNaborColumnsOld(articulNabor_old.Id_gost);
            //    await Task.Delay(50);
            //    await LoadSostav();
            //    HideTechnicalColumns();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
            //        MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            EditNaborSostavMatr f = new EditNaborSostavMatr(_user, Convert.ToInt32(currentItem.Kod.Substring(0, 7)));
            f.ShowDialog();
        }
        private void customCheckBoxVerified_CheckedChanged(object sender, EventArgs e)
        {
            if (customCheckBoxVerified.Checked == true)
                customButtonSaveNabor.Enabled = true;
            else
                customButtonSaveNabor.Enabled = false;
        }

        #endregion

        #region Размер для набора (выпадающий список по заголовку)
        private int _clickedGroupHandle = GridControl.InvalidRowHandle;
        private void GridViewNabor_MouseDown(object sender, MouseEventArgs e)
        {
            var view = sender as BandedGridView;
            if (view == null) return;

            var hit = view.CalcHitInfo(e.Location);
            if (hit.InRow && view.IsGroupRow(hit.RowHandle))
            {
                _clickedGroupHandle = hit.RowHandle;

                Point screenPoint = view.GridControl.PointToScreen(e.Location);
                Point clientPoint = this.PointToClient(screenPoint);

                clientPoint.Y += 2;

                customListBoxRazm.Location = clientPoint;
                customListBoxRazm.BringToFront();
                customListBoxRazm.Visible = true;

                return;
            }
            customListBoxRazm.Visible = false;
        }
        private void listRazm_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = customListBoxRazm.SelectedItem as GostRazmerNabViewModel;
            if (_razmSostavForGost == null)
            {
                _razmSostavForGost = new List<GostRazmerNabViewModel>();
            }

            if (item == null)
            {
                return;
            }

            if (_clickedGroupHandle == GridControl.InvalidRowHandle)
            {
                return;
            }

            customListBoxRazm.Visible = false;

            if (_clickedGroupHandle == GridControl.InvalidRowHandle)
                return;

            GridViewNabor.BeginDataUpdate();
            try
            {
                // все строки, входящие в эту группу
                int childCount = GridViewNabor.GetChildRowCount(_clickedGroupHandle);
                for (int i = 0; i < childCount; i++)
                {
                    int rowHandle = GridViewNabor.GetChildRowHandle(_clickedGroupHandle, i);
                    if (rowHandle < 0) continue;

                    var row = GridViewNabor.GetRow(rowHandle) as SpArticulNaborSostav;
                    if (row == null) continue;

                    row.Razm_all = item.Razm;         // для группировки
                    row.Id_razm_nab = item.Id_razmer; // связь с gost_sv_razmer

                    GridViewNabor.SetRowCellValue(rowHandle, "Razm_all", item.Razm);
                    GridViewNabor.SetRowCellValue(rowHandle, "Id_razm_nab", item.Id_razmer);

                    if (_razmSostavForGost == null || _razmSostavForGost.Count == 0)
                    {
                        Debug.WriteLine("[listRazm] Список размеров состава пуст. match = null");
                        row.Razm = "Ошибка";
                        GridViewNabor.SetRowCellValue(rowHandle, "Razm", "Ошибка");
                        continue;
                    }

                    var match = _razmSostavForGost
                        .FirstOrDefault(r =>
                            r.Id_razmer == item.Id_razmer &&    // связь по Id размера
                            r.Id_gost == row.Id_gost);          // связь по ГОСТ состава
                    if (match != null)
                    {
                        row.Razm = match.Razm;
                        GridViewNabor.SetRowCellValue(rowHandle, "Razm", match.Razm);
                    }
                    else
                    {
                        row.Razm = "Ошибка";
                        GridViewNabor.SetRowCellValue(rowHandle, "Razm", "Ошибка");
                    }
                }
            }
            finally
            {
                GridViewNabor.EndDataUpdate();
                ValidateGridState();
            }

            GridViewNabor.RefreshData();
            GridViewNabor.ExpandAllGroups();
        }
        #endregion
    }
}
