using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using SewingProduction.Features.Tabel.Models;
using SewingProduction.Features.Tabel.Services;
using SewingProduction.Features.UserDistribution.Helpers;

namespace SewingProduction.Features.Tabel.Forms
{
    public partial class OtvlRab : CustomForm
    {
        TabOtvlRDataService tabOtvlRDataService = new TabOtvlRDataService();
        public int Group = 0;
        private BindingList<TabOtvlRModel> _rows = new BindingList<TabOtvlRModel>();
        public OtvlRab(UserClass user, int tnid) : base(user)
        {
            InitializeComponent();
            Group = tnid;
        }

        #region Initialization
        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            await InitializeFormAsync();
        }
        private async Task InitializeFormAsync()
        {
            layoutControlGroupRab.Text = await tabOtvlRDataService.GetNaimenGroupZlAsync(Group);

            customSearchLookUpEditFio.Properties.DataSource = await tabOtvlRDataService.GetZlSpisokAsync(Group);
            customSearchLookUpEditFio.Properties.DisplayMember = "Fio";
            customSearchLookUpEditFio.Properties.ValueMember = "Tabno";
            customSearchLookUpEditFio.Properties.NullText = "";

            customGridControlTabel.DataSource = _rows;
        }
        #endregion
        private void OtvlRab_Load(object sender, EventArgs e)
        {
        }

        private void customGridControlTabel_Load(object sender, EventArgs e)
        {
            TogglePairColumns(Priem_t_s, Priem_t_po, false);
            TogglePairColumns(Sort_t_s, Sort_t_po, false);
            TogglePairColumns(Drug_s_s, Drug_s_po, false);
            TogglePairColumns(Otvl_r_s, Otvl_r_po, false);
        }
        private void TogglePairColumns(
        DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colS,
        DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPo,
        bool needShow)
        {
            bandedGridViewTabel.BeginUpdate();
            try
            {
                colS.Visible = needShow;
                colPo.Visible = needShow;
                bandedGridViewTabel.LayoutChanged();
            }
            finally
            {
                bandedGridViewTabel.EndUpdate();
            }
        }
        private void repositoryItemTimeEditPriem_t_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            TogglePairColumns(Priem_t_s, Priem_t_po, !Priem_t_s.Visible);
        }
        private void repositoryItemTimeEditSort_t_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            TogglePairColumns(Sort_t_s, Sort_t_po, !Sort_t_s.Visible);
        }
        private void repositoryItemTimeEditOtvl_r_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            TogglePairColumns(Otvl_r_s, Otvl_r_po, !Otvl_r_s.Visible);
        }
        private void repositoryItemTimeEditDrug_s_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            TogglePairColumns(Drug_s_s, Drug_s_po, !Drug_s_s.Visible);
        }

        private void customButtonAdd_Click(object? sender, EventArgs e)
        {
            _rows.Add(new TabOtvlRModel());

            bandedGridViewTabel.RefreshData();
            int rowHandle = bandedGridViewTabel.RowCount - 1;
            if (rowHandle < 0) return;

            bandedGridViewTabel.FocusedRowHandle = rowHandle;
        }

    }
}
