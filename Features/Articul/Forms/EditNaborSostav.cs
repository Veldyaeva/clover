using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraDiagram.Base;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Filtering;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSpreadsheet.Model;
using SewingProduction.Core.Class;
using SewingProduction.Core.Models;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.Articul.Service;
using SewingProduction.Features.UserDistribution.Helpers;

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
        ArticulModel articulNabor;
        public EditNaborSostav()
        {
            InitializeComponent();
        }
        public EditNaborSostav(UserClass user, ArticulModel Obj) : base(user)
        {
            InitializeComponent();
            articulNabor = Obj;
        }
        private async void customGridControl1_Load(object sender, EventArgs e)
        {
            await LoadNabor();
            await LoadSostav();
            await LoadAllGroupsAsync();
            await SetupPictursBox();
            HideTechnicalColumns();
            ArticulNaborColumns();
            ArticulNaborColumnsOld();
            SetupSearchLookUpEditGost();
        }
        #region Загрузка формы
        private async Task LoadNabor()
        {
            articulNabor = await _articulDataService.GetArtByKodAsync(articulNabor.Kod);
            assortModelBindingSource.DataSource = await _ANSDataService.GetAssortAsync();
            tvnModelBindingSource.DataSource = await _ANSDataService.GetTvnAsync();

            _bsGostForNabor.DataSource = await _ANSDataService.GetGostNaborAsync();

            _bsGrupForNabor.DataSource = await _ANSDataService.GetGostGrupIzdNaborAsync(new int[] { articulNabor.Id_gost }, 3);

            currentItem = spArticulNaborSostavBindingSource.Current as SpArticulNaborSostav;
            if (currentItem != null)
            {
                oldAgIdBeforeEdit = currentItem.Ag_id;
            }
        }
        private async Task LoadSostav()
        {
            // --- основная таблица ---
            var list = await _ANSDataService.GetByKodAsync(articulNabor.Kod);
            _listCurrent = new BindingList<SpArticulNaborSostav>(list);
            _listOld = new BindingList<SpArticulNaborSostav>(
                list.Select(x => new SpArticulNaborSostav
                {
                    Ans_id = x.Ans_id,
                    Txt_v = x.Txt_v,
                    Kod = x.Kod,
                    Ta_id = x.Ta_id,
                    Tk_name = x.Tk_name.Trim(),
                    Id_gost = x.Id_gost,
                    N_i = x.N_i.Trim(),
                    Ag_id = x.Ag_id,
                    Sostav = x.Sostav.Trim(),
                    Id_razm_nab = x.Id_razm_nab,
                    Razm = x.Razm.Trim(),
                    razm_all = x.razm_all
                }).ToList()
            );

            // --- привязки ---
            spArticulNaborSostavBindingSource.DataSource = _listCurrent; // редактируемая часть
            _bsOld.DataSource = _listOld; // снимок
            customGridControlNabor.DataSource = spArticulNaborSostavBindingSource;
            customGridControlNabor_Old.DataSource = _bsOld;

            _bsGostForSostav.DataSource = await _ANSDataService.GetGostSostavAsync();
            //_bsGrupForSostav.DataSource = await _ANSDataService.GetGostGrupIzdNaborAsync(articulNabor.Id_gost);
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
            customPictureBoxNabor.ImagePath = await _articulDataService.GetFileEskizForKod(articulNabor.Kod);
        }
        private void ArticulNaborColumnsOld()
        {
            customTextBoxArtN_Old.Text = articulNabor.Articul;
            customTextBoxGostN_Old.Text = articulNabor.Gost;
            customTextBoxGrupN_Old.Text = articulNabor.Grup;
        }
        private void ArticulNaborColumns()
        {
            customTextBoxArtN.Text = articulNabor.Articul;
            customSearchLookUpEditGostN.EditValue = articulNabor.Id_gost.ToString();
            customSearchLookUpEditGrupN.EditValue = articulNabor.Ag_id.ToString();
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
                GridViewNabor_Old.Columns["razm_all"].GroupIndex = 0; // группировка по колонке размера
                GridViewNabor_Old.ExpandAllGroups(); // раскрыть группы при загрузке
                GridViewNabor_Old.OptionsView.ShowGroupPanel = true; //

                GridViewNabor.Columns["razm_all"].Visible = false;
                GridViewNabor.Columns["Ag_id"].Visible = true;
                GridViewNabor.ClearGrouping();
                GridViewNabor.Columns["razm_all"].GroupIndex = 0;
                GridViewNabor.ExpandAllGroups();
                GridViewNabor.OptionsView.ShowGroupPanel = true;
            }
            finally
            {
                GridViewNabor_Old.EndUpdate();
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

            if (view == GridViewNabor)
            {
                SyncGridSelection(GridViewNabor, GridViewNabor_Old, "Ans_id", e.FocusedRowHandle);
            }
            else if (view == GridViewNabor_Old)
            {
                SyncGridSelection(GridViewNabor_Old, GridViewNabor, "Ans_id", e.FocusedRowHandle);
            }
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
        }

        #endregion
        #region Обработчики кнопок
        private void customButtonSaveNabor_Click(object sender, EventArgs e)
        {
        }
        private async void customButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                currentItem = spArticulNaborSostavBindingSource.Current as SpArticulNaborSostav;
                if (currentItem == null)
                {
                    MessageBox.Show("Нет выбранной записи для сохранения!", "Внимание",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (_ANSDataService.CheckPovt(currentItem.Kod))
                {
                    MessageBox.Show("Изменение невозможно! дубликаты в описании! обратитесь к администратору!", "Внимание",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_ANSDataService.CheckOpis(currentItem.Kod))
                {
                    var result = MessageBox.Show($"Набор уже описан, изменения применятся на весь размерный ряд!,Вы уверены что хотите продолжить?", "Подтверждение",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result != DialogResult.Yes)
                        return;
                }
                await _ANSDataService.UpdateArticulNaborSostavAsync(currentItem, oldAgIdBeforeEdit);
                spArticulNaborSostavBindingSource.DataSource = await _ANSDataService.GetByKodAsync(articulNabor.Kod);
                GridViewNabor_Old.ExpandAllGroups();
                MessageBox.Show("Изменения успешно сохранены!", "Сохранение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                oldAgIdBeforeEdit = currentItem.Ag_id;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #endregion

    }
}
