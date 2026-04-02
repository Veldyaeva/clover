using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.BouncyCastle.Asn1.Ocsp;
using SewingProduction.Features.Tabel.Models;
using SewingProduction.Features.Tabel.Services;
using SewingProduction.Features.UserDistribution.DataService;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution.Models;

namespace SewingProduction.Features.Tabel.Forms
{
    public partial class TabelLock : CustomForm
    {
        private readonly TabelLockDataService _dataService;
        private List<TabelSpLockTypeModel> _lockTypes;
        private List<TabelSpLockModel> _tabelSpLock;
        public TabelLock(UserClass user) : base(user)
        {
            InitializeComponent();
            _dataService = new TabelLockDataService();
        }

        private async void TabelLock_Load(object sender, EventArgs e)
        {
            _lockTypes = await _dataService.GetTabelSpLockType();
            customLookUpEditTB.Properties.DataSource = _lockTypes;
            customLookUpEditTB.Properties.DisplayMember = "Tslt_Name";
            customLookUpEditTB.Properties.ValueMember = "TsltID";
            customLookUpEditTB.Properties.NullText = "";

            _tabelSpLock = await _dataService.GetTabelSpLock();

            if (_lockTypes != null && _lockTypes.Count > 0)
            {
                customLookUpEditTB.EditValue = _lockTypes.First().TsltID;
            }
            else
            {
                customGridControlTL.DataSource = _tabelSpLock;
            }
        }

        private void customLookUpEditTB_EditValueChanged(object sender, EventArgs e)
        {
            if (_tabelSpLock == null)
                return;

            if (customLookUpEditTB.EditValue == null)
            {
                customGridControlTL.DataSource = _tabelSpLock;
                return;
            }

            if (!int.TryParse(customLookUpEditTB.EditValue.ToString(), out int selectedTypeId))
            {
                customGridControlTL.DataSource = _tabelSpLock;
                return;
            }

            customGridControlTL.DataSource = _tabelSpLock
                .Where(x => x.Tsl_TsltID == selectedTypeId)
                .ToList();

            gridViewTL.FocusedRowHandle = gridViewTL.RowCount - 1;
            gridViewTL.MakeRowVisible(gridViewTL.FocusedRowHandle);
        }

        private async void gridViewTL_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            int rowHandle = gridViewTL.FocusedRowHandle;
            try
            {
                var row = e.Row as TabelSpLockModel;
                if (row == null) return;

                await _dataService.UpdateTabelSpLock(row.TslID, row.Tsl_DateTo);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show("Ошибка сохранения: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //gridViewTL.FocusedRowHandle = rowHandle;
        }
    }
}
