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
        string xKod;
        public EditNaborSostav()
        {
            InitializeComponent();
        }
        public EditNaborSostav(UserClass user, string kod) : base(user)
        {
            if (_ANSDataService.CheckOpis(kod)) 
            { 
                MessageBox.Show("Нельзя редактировать набор!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            InitializeComponent();
            xKod = kod;
        }

        private async void customGridControl1_Load(object sender, EventArgs e)
        {
            assortModelBindingSource.DataSource = await _ANSDataService.GetAssortAsync();
            tvnModelBindingSource.DataSource = await _ANSDataService.GetTvnAsync();
            gostModelBindingSource.DataSource = await _ANSDataService.GetGostAsync();
            spArticulNaborSostavBindingSource.DataSource = await _ANSDataService.GetByKodAsync(xKod);
            currentItem = spArticulNaborSostavBindingSource.Current as SpArticulNaborSostav;
            gostGrupIzdViewModelBindingSource.DataSource = await _ANSDataService.GetGostGrupIzdAsync(currentItem.Id_gost, currentItem.Tk_id);
            customPictureBoxNabor.ImagePath = await _articulDataService.GetFileEskizForKod(xKod);
        }

        private async void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            currentItem = spArticulNaborSostavBindingSource.Current as SpArticulNaborSostav;
            if (currentItem != null)
                gostGrupIzdViewModelBindingSource.DataSource = await _ANSDataService.GetGostGrupIzdAsync(currentItem.Id_gost, currentItem.Tk_id);
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

                await _ANSDataService.SaveAsync(currentItem);
                await _ANSDataService.UpdateArtKoplektAsync(currentItem.Kod);

                MessageBox.Show("Изменения успешно сохранены!", "Сохранение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
