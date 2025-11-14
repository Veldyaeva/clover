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
        private bool _isLoading = false;
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
            _isLoading = true;
            await LoadNabor();
            await LoadSostav();
            await LoadAllGroupsAsync();
            await SetupPictursBox();
            HideTechnicalColumns();
            ArticulNaborColumns();
            ArticulNaborColumnsOld();
            SetupSearchLookUpEditGost();
            _isLoading = false;
        }
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

        private void customButtonSaveNabor_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_isLoading.ToString(), "Внимание",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        private void gridViewNabor_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            currentItem = spArticulNaborSostavBindingSource.Current as SpArticulNaborSostav;
            if (currentItem == null) return;

            //_bsGostForSostav.DataSource = await _ANSDataService.GetGostSostavAsync(articulNabor.Id_gost);
            oldAgIdBeforeEdit = currentItem.Ag_id;
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

        private async void customSearchLookUpEditGostN_EditValueChanged(object sender, EventArgs e)
        {
            int idGostNabor = Convert.ToInt32(customSearchLookUpEditGostN.EditValue);
            // Группа набора по ГОСТу набора
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
            // ГОСТы состава по ГОСТу набора
            await AutoApplyIdGostAfterChangingNabor(idGostNabor);
            await LoadAllGroupsAsync();
        }

        private void EditNaborSostav_Load(object sender, EventArgs e)
        {

        }
        private async Task AutoApplyIdGostAfterChangingNabor(int idGostNabor)
        {
            if (idGostNabor == 0) return;

            // Грузим ВСЕ gost_sostav для набора (и верх, и низ)
            var allGosts = await _ANSDataService.GetGostSostavAsync(idGostNabor);

            Debug.WriteLine($"[AutoGost] Loaded gost-sostav count={allGosts.Count}");

            // Применяем логику построчно
            var view = GridViewNabor;
            view.BeginDataUpdate();

            try
            {
                for (int i = 0; i < view.DataRowCount; i++)
                {
                    var row = view.GetRow(i) as SpArticulNaborSostav;
                    if (row == null) continue;

                    int tk = row.Tk_id;

                    // Фильтрация в памяти по Tk_id строки
                    var filtered = allGosts
                        .Where(g => g.Tk_id == tk)   // важно: Tk_id_nab из GostModel
                        .ToList();

                    if (filtered.Count == 1)
                    {
                        // 🟢 Автоподстановка
                        int autoGost = filtered[0].Id_gost;
                        view.SetRowCellValue(i, "Id_gost", autoGost);
                        Debug.WriteLine($"[AutoGost] row {i} Autoselected Id_gost={autoGost} (Tk_id={tk})");
                    }
                    else
                    {
                        // 🔵 Автосброс — ждём выбора пользователя
                        view.SetRowCellValue(i, "Id_gost", DBNull.Value);
                        Debug.WriteLine($"[AutoGost] row {i} Cleared Id_gost (filtered={filtered.Count})");
                    }
                }
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
                Debug.WriteLine($"[Popup] Applied filter [Tk_id]={row.Tk_id}, [Id_glav_gost]={idGostNabor}");
            }
            popupView.FocusedRowChanged -= PopupView_FocusedRowChanged;
            popupView.FocusedRowChanged += PopupView_FocusedRowChanged;
        }

        private void repositoryItemSearchLookUpEditGost_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            Debug.WriteLine($"CloseUp");

            if (selectedGostId == null || selectedGostId == 0)
            {
                Debug.WriteLine("[CloseUp] selectedGostId is null or 0 — nothing to apply");
                return;
            }

            var view = GridViewNabor;
            var currentRow = view.GetFocusedRow() as SpArticulNaborSostav;
            if (currentRow == null) return;

            // tk_id и ag_id текущей строки
            int tkId = currentRow.Tk_id;
            int agId = currentRow.Ag_id;

            Debug.WriteLine($"[CloseUp] Apply Id_gost={selectedGostId} to rows with Tk_id={tkId}, Ag_id={agId}");

            // ⚙️ Меняем только те строки, где Tk_id и Ag_id совпадают
            view.BeginDataUpdate();
            try
            {
                for (int i = 0; i < view.DataRowCount; i++)
                {
                    var row = view.GetRow(i) as SpArticulNaborSostav;
                    if (row == null) continue;

                    if (row.Tk_id == tkId && row.Ag_id == agId)
                    {
                        view.SetRowCellValue(i, "Id_gost", selectedGostId.Value);
                        Debug.WriteLine($"  → Row {i}: Ans_id={row.Ans_id}, Tk_id={row.Tk_id}, Ag_id={row.Ag_id}, set Id_gost={selectedGostId}");
                    }
                }
            }
            finally
            {
                view.EndDataUpdate();
            }

            // фиксируем изменения и обновляем интерфейс
            view.PostEditor();
            view.UpdateCurrentRow();
            spArticulNaborSostavBindingSource.EndEdit();
            view.RefreshData();
        }

        private void PopupView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var popupView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (popupView == null) return;

            var rowGost = popupView.GetFocusedRow() as GostModel;
            if (rowGost == null)
            {
                Debug.WriteLine("[PopupView_FocusedRowChanged] rowGost is null — nothing to apply");
                return;
            }
            selectedGostId = rowGost.Id_gost;
            Debug.WriteLine($"[PopupView_FocusedRowChanged] selectedGostId = {selectedGostId}");
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
        private async Task LoadAllGroupsAsync()
        {
            var gostIds = CollectAllGostIds();   // ✔ собираем Id_gost из всех строк

            if (gostIds.Length == 0)
            {
                _allGroups = new List<GostGrupIzdViewModel>();
                repositoryItemSearchLookUpEditGrup.DataSource = _allGroups;
                return;
            }

            // загружаем всё разом
            _allGroups = await _ANSDataService.GetGostGrupIzdNaborAsync(gostIds, null);

            // назначаем один общий список репозиторию
            repositoryItemSearchLookUpEditGrup.DataSource = _allGroups;
            repositoryItemSearchLookUpEditGrup.DisplayMember = "N_i";
            repositoryItemSearchLookUpEditGrup.ValueMember = "Ag_id";
            repositoryItemSearchLookUpEditGrup.NullText = "";
        }
        private async void GridViewNabor_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "Id_gost") return;
            else await LoadAllGroupsAsync();

            var row = GridViewNabor.GetRow(e.RowHandle) as SpArticulNaborSostav;
            if (row == null) return;

            int gost = row.Id_gost;
            int tk = row.Tk_id;

            var groups = _allGroups
                .Where(g => g.Id_gost == gost && g.Tk_id == tk)
                .ToList();

            // Старые значения
            var oldRow = _bsOld.Cast<SpArticulNaborSostav>()
                               .FirstOrDefault(x => x.Ans_id == row.Ans_id);

            if (groups.Count == 1)
            {
                var only = groups[0];

                row.Ag_id = only.Ag_id;
                row.N_i = only.N_i;

                GridViewNabor.SetRowCellValue(e.RowHandle, "Ag_id", only.Ag_id);

                Debug.WriteLine($"[AUTO] group Ag_id={only.Ag_id}");
                return;
            }

            if (oldRow != null)
            {
                var match = groups.FirstOrDefault(g => g.Ag_id == oldRow.Ag_id);
                if (match != null)
                {
                    row.Ag_id = match.Ag_id;
                    row.N_i = match.N_i;
                    GridViewNabor.SetRowCellValue(e.RowHandle, "Ag_id", match.Ag_id);

                    Debug.WriteLine($"[RESTORE] restored Ag_id={match.Ag_id}");
                    return;
                }
            }

            row.Ag_id = 0;
            row.N_i = null; 
            Debug.WriteLine($"[CLEAR] set Ag_id=0 (before) old={row.Ag_id}");
            GridViewNabor.SetRowCellValue(e.RowHandle, "Ag_id", 0);
            Debug.WriteLine($"[CLEAR] set Ag_id=0 (after) model={row.Ag_id}");

            GridViewNabor.RefreshData();
            GridViewNabor.PostEditor();
            GridViewNabor.UpdateCurrentRow();
            spArticulNaborSostavBindingSource.EndEdit();
            GridViewNabor.RefreshRow(e.RowHandle);
            Debug.WriteLine("[CLEAR] multiple groups");

        }

        private void repositoryItemSearchLookUpEditGrup_Popup(object sender, EventArgs e)
        {
            if (GridViewNabor.FocusedColumn == null || GridViewNabor.FocusedColumn.FieldName != "Ag_id")
                return;

            var row = GridViewNabor.GetFocusedRow() as SpArticulNaborSostav;
            if (row == null) return;

            // tk_id текущей строки
            int tkId = row.Tk_id;

            // выбранный гост строки (Id_gost)
            int? selectedGostId = row.Id_gost;

            if (selectedGostId == null || selectedGostId == 0)
            {
                Debug.WriteLine("[Grup_Popup] Id_gost is null — cannot filter");
                return;
            }

            var editor = GridViewNabor.ActiveEditor as DevExpress.XtraEditors.SearchLookUpEdit;
            if (editor == null) return;

            var popupView = editor.Properties.PopupView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (popupView == null) return;

            // 🔥 фильтруем группы состава по tk_id и id_gost
            popupView.ActiveFilterString = $"[Tk_id] = {tkId} AND [Id_gost] = {selectedGostId}";
            Debug.WriteLine($"[Grup_Popup] Apply filter: Tk_id={tkId}, Id_gost={selectedGostId}");

            // подписка на выбор строки
            popupView.FocusedRowChanged -= GrupPopupView_FocusedRowChanged;
            popupView.FocusedRowChanged += GrupPopupView_FocusedRowChanged;
        }
        private GostGrupIzdViewModel selectedGrupId = null;

        private void GrupPopupView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var popupView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (popupView == null) return;

            selectedGrupId = popupView.GetFocusedRow() as GostGrupIzdViewModel;
            if (selectedGrupId == null)
            {
                Debug.WriteLine("[Grup_Focused] rowGr null");
                return;
            }

        }
        private void repositoryItemSearchLookUpEditGrup_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            Debug.WriteLine("[Grup_CloseUp]");

            if (selectedGrupId == null || selectedGrupId.Ag_id == 0)
            {
                Debug.WriteLine("[Grup_CloseUp] selectedGrupId null");
                return;
            }

            var view = GridViewNabor;
            var currentRow = view.GetFocusedRow() as SpArticulNaborSostav;
            if (currentRow == null) return;

            int tkId = currentRow.Tk_id;

            Debug.WriteLine($"[Grup_CloseUp] Apply Ag_id={selectedGrupId.Ag_id} to rows with Tk_id={tkId}");

            view.BeginDataUpdate();
            try
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

    }
}
