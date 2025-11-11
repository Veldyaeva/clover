using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
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
            await SetupPictursBox();
            HideTechnicalColumns();
            ArticulNaborColumns();
            ArticulNaborColumnsOld();
            SetupSearchLookUpEditGost();
            repositoryItemSearchLookUpEditGost.DataSource = await _ANSDataService.GetGostSostavAsync(articulNabor.Id_gost);
            repositoryItemSearchLookUpEditGost.DisplayMember = "Id_gost";
            repositoryItemSearchLookUpEditGost.ValueMember = "Id_gost";

            repositoryItemSearchLookUpEditGrup.DataSource = await _ANSDataService.GetGostGrupIzdNaborAsync(articulNabor.Id_gost);
            repositoryItemSearchLookUpEditGrup.DisplayMember = "N_i";
            repositoryItemSearchLookUpEditGrup.ValueMember = "N_i";
        }
        private async Task LoadNabor()
        {
            articulNabor = await _articulDataService.GetArtByKodAsync(articulNabor.Kod);
            assortModelBindingSource.DataSource = await _ANSDataService.GetAssortAsync();
            tvnModelBindingSource.DataSource = await _ANSDataService.GetTvnAsync();

            _bsGostForNabor.DataSource = await _ANSDataService.GetGostNaborAsync();

            _bsGrupForNabor.DataSource = await _ANSDataService.GetGostGrupIzdNaborAsync(articulNabor.Id_gost, 3);

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

            //_bsGostForSostav.DataSource = await _ANSDataService.GetGostSostavAsync(articulNabor.Id_gost);
            //_bsGrupForSostav.DataSource = await _ANSDataService.GetGostSostavAsync();
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
            //GridViewNabor.ShowEditor();
        }

        private async void gridViewNabor_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            currentItem = spArticulNaborSostavBindingSource.Current as SpArticulNaborSostav;
            if (currentItem == null) return;

            //_bsGostForSostav.DataSource = await _ANSDataService.GetGostSostavAsync(articulNabor.Id_gost, currentItem.Tk_id);
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
            var idGost = customSearchLookUpEditGostN.EditValue.ToIntN();
            _bsGrupForNabor.DataSource = await _ANSDataService.GetGostGrupIzdNaborAsync(idGost, 3);
            //_bsGostForSostav.DataSource = await _ANSDataService.GetGostSostavAsync(idGost);

        }

        private void EditNaborSostav_Load(object sender, EventArgs e)
        {

        }

        private void customGridControlNabor_Click(object sender, EventArgs e)
        {

        }

        private async void GridViewNabor_ShownEditor(object sender, EventArgs e)
        {
            var view = sender as DevExpress.XtraGrid.Views.BandedGrid.BandedGridView;
            if (view == null) return;

            var row = view.GetFocusedRow() as SpArticulNaborSostav;
            Debug.WriteLine($"[ShownEditor] Col={view.FocusedColumn?.FieldName}, Row={view.FocusedRowHandle}, RowId={row?.Ans_id}, Tk_id={row?.Tk_id}");
            if (row == null) return;

            var idGostNabor = customSearchLookUpEditGostN.EditValue.ToIntN();

            // --- Если редактируем ГОСТ ---
            if (view.FocusedColumn.FieldName == "Id_gost")
            {
                var gostList = await _ANSDataService.GetGostSostavAsync(idGostNabor);

                Debug.WriteLine($"[ShownEditor] Loaded gostList Count={gostList?.Count ?? 0} for Tk_id={row.Tk_id}");

                var editorGost = view.ActiveEditor as DevExpress.XtraEditors.SearchLookUpEdit;
                if (editorGost != null)
                {
                    editorGost.Properties.DataSource = gostList;
                    //editorGost.Properties.DisplayMember = "Id_gost";
                    //editorGost.Properties.ValueMember = "Id_gost";
                    //editorGost.Properties.NullText = "";
                }
                return;
            }

            // --- Если редактируем ГРУППУ ---
            if (view.FocusedColumn.FieldName == "N_i")
            {
                // Берём текущий выбранный ГОСТ в этой строке
                var idGost = row.Id_gost;

                // Если ГОСТ ещё не выбран — нечего подгружать
                if (idGost <= 0)
                {
                    Debug.WriteLine("[ShownEditor] Skip groups: Id_gost not selected yet.");
                    return;
                }

                var grupList = await _ANSDataService.GetGostGrupIzdNaborAsync(idGost, row.Tk_id);
                Debug.WriteLine($"[ShownEditor] Loaded grupList Count={grupList?.Count ?? 0} for Id_gost={row.Id_gost}, Tk_id={row.Tk_id}");


                var editorGrup = view.ActiveEditor as DevExpress.XtraEditors.SearchLookUpEdit;
                if (editorGrup != null)
                {
                    editorGrup.Properties.DataSource = grupList;
                    //editorGrup.Properties.DisplayMember = "N_i";
                    //editorGrup.Properties.ValueMember = "N_i";
                    //editorGrup.Properties.NullText = "";
                }
            }
        }
        private void GridViewNabor_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            var view = sender as DevExpress.XtraGrid.Views.BandedGrid.BandedGridView;
            if (view == null) return;

            var row = view.GetFocusedRow() as SpArticulNaborSostav;
            if (row == null) return;
            Debug.WriteLine($"[ValidatingEditor] Col={view?.FocusedColumn?.FieldName}, Old(Id_gost={row?.Id_gost}, N_i={row?.N_i}), NewValue={e.Value}");

            if (row == null || view?.FocusedColumn == null) return;

            if (view.FocusedColumn.FieldName == "Id_gost")
            {
                row.Id_gost = Convert.ToInt32(e.Value ?? 0);
                Debug.WriteLine($"[ValidatingEditor] SET row.Id_gost={row.Id_gost}");
            }
            else if (view.FocusedColumn.FieldName == "N_i")
            {
                row.N_i = e.Value?.ToString();
                Debug.WriteLine($"[ValidatingEditor] SET row.N_i={row.N_i}");
            }
        }

    }
}
