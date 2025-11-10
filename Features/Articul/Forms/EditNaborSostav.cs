using System;
using System.Diagnostics;
using System.Windows.Forms;
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
            articulNabor = await _articulDataService.GetArtByKodAsync(articulNabor.Kod);
            assortModelBindingSource.DataSource = await _ANSDataService.GetAssortAsync();
            tvnModelBindingSource.DataSource = await _ANSDataService.GetTvnAsync();
            gostModelBindingSource.DataSource = await _ANSDataService.GetGostAsync();
            spArticulNaborSostavBindingSource.DataSource = await _ANSDataService.GetByKodAsync(articulNabor.Kod);

            repositoryItemSearchLookUpEditGrup.DataSource = gostModelBindingSource.DataSource;
            repositoryItemSearchLookUpEditGrup.DataSource = spArticulNaborSostavBindingSource.DataSource;

            currentItem = spArticulNaborSostavBindingSource.Current as SpArticulNaborSostav;
            if (currentItem != null)
            {
                oldAgIdBeforeEdit = currentItem.Ag_id;
                gostGrupIzdViewModelBindingSource.DataSource = await _ANSDataService.GetGostGrupIzdAsync();
            }
            customPictureBoxNabor.ImagePath = await _articulDataService.GetFileEskizForKod(articulNabor.Kod);
            HideTechnicalColumns();
            ArticulNaborColumns();
            SetupSearchLookUpEditGost();
        }

        private async void gridViewNabor_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            currentItem = spArticulNaborSostavBindingSource.Current as SpArticulNaborSostav;
            if (currentItem != null)
            {
                gostGrupIzdViewModelBindingSource.DataSource = await _ANSDataService.GetGostGrupIzdAsync();
                oldAgIdBeforeEdit = currentItem.Ag_id;
            }
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

        private void ArticulNaborColumns()
        {
            customTextBoxArtN_Old.Text = articulNabor.Articul;
            customTextBoxGostN_Old.Text = articulNabor.Gost;
            customTextBoxGrupN_Old.Text = articulNabor.Grup;
            //customTextBoxRazmN.Text = articulNabor.Razm;
        }
        private void HideTechnicalColumns()
        {
            GridViewNabor_Old.BeginUpdate();
            try
            {
                GridViewNabor_Old.Columns["Ans_id"].Visible = false;
                GridViewNabor_Old.Columns["Ta_id"].Visible = false;
                GridViewNabor_Old.Columns["Tk_id"].Visible = false;
                GridViewNabor_Old.Columns["Ag_id"].Visible = false;
                GridViewNabor_Old.Columns["Id_razm_nab"].Visible = false;
                GridViewNabor_Old.Columns["Kod"].Visible = false;
                GridViewNabor_Old.ClearGrouping();
                GridViewNabor_Old.Columns["razm_all"].GroupIndex = 0; // группировка по колонке размера
                GridViewNabor_Old.ExpandAllGroups(); // раскрыть группы при загрузке
                GridViewNabor_Old.OptionsView.ShowGroupPanel = true; //

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
    }
}
