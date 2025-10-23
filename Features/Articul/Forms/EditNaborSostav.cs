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
            currentItem = spArticulNaborSostavBindingSource.Current as SpArticulNaborSostav;
            if (currentItem != null)
            {
                oldAgIdBeforeEdit = currentItem.Ag_id;
                gostGrupIzdViewModelBindingSource.DataSource = await _ANSDataService.GetGostGrupIzdAsync(currentItem.Id_gost, currentItem.Tk_id);
            }
            customPictureBoxNabor.ImagePath = await _articulDataService.GetFileEskizForKod(articulNabor.Kod);
            HideTechnicalColumns();
            ArticulNaborColumns();
        }

        private async void gridViewNabor_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            currentItem = spArticulNaborSostavBindingSource.Current as SpArticulNaborSostav;
            if (currentItem != null) 
            {
                gostGrupIzdViewModelBindingSource.DataSource = await _ANSDataService.GetGostGrupIzdAsync(currentItem.Id_gost, currentItem.Tk_id);
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
                    var result = MessageBox.Show($"Набор уже описан, изменения применятся на весь размерный ряд!,Вы уверены что хотите продолжить?","Подтверждение",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result != DialogResult.Yes)
                        return;
                }
                await _ANSDataService.UpdateArticulNaborSostavAsync(currentItem, oldAgIdBeforeEdit);
                spArticulNaborSostavBindingSource.DataSource = await _ANSDataService.GetByKodAsync(articulNabor.Kod);
                GridViewNabor.ExpandAllGroups();
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
            customTextBoxArtN.Text = articulNabor.Articul;
            //customTextBoxRazmN.Text = articulNabor.Razm;
            customTextBoxGostN.Text = articulNabor.Gost;
            customTextBoxGrupN.Text = articulNabor.Grup;
        }
        private void HideTechnicalColumns()
        {
            GridViewNabor.BeginUpdate();
            try
            {
                GridViewNabor.Columns["Ans_id"].Visible = false;
                GridViewNabor.Columns["Ta_id"].Visible = false;
                GridViewNabor.Columns["Tk_id"].Visible = false;
                GridViewNabor.Columns["Ag_id"].Visible = false;
                GridViewNabor.Columns["Id_razm_nab"].Visible = false;
                GridViewNabor.Columns["Kod"].Visible = false;
                GridViewNabor.ClearGrouping();
                GridViewNabor.Columns["razm_all"].GroupIndex = 0; // группировка по колонке размера
                GridViewNabor.ExpandAllGroups(); // раскрыть группы при загрузке
                GridViewNabor.OptionsView.ShowGroupPanel = true; // отобразить панель группировки
            }
            finally
            {
                GridViewNabor.EndUpdate();
            }
        }
        
    }
}
