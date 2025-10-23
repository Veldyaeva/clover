using DevExpress.XtraDataLayout;
using DevExpress.XtraEditors;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service;
using SewingProduction.Helpers;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class KnitterWorkSpace : Form
    {
        private readonly KnitterDataService _dataService;
        private readonly BindingSource _planBindingSource = new BindingSource();
        private readonly PlanZagrVyazService _planService;
        public KnitterWorkSpace()
        {
         
            InitializeComponent();
                dataLayoutControl1.DataSource = new BindingSource
                {
                    DataSource = new PlanZagrVyaz()
                };

            //    dataLayoutControl1.RetrieveFields(new RetrieveFieldsParameters
            //    {
            //        DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
            //    });


            _dataService = new KnitterDataService();
            PlanZagrVyazGridControl.DataSource = _planBindingSource;
            this.Load += async (s, e) => await InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            try
            {
                // Заполняем список ФИО
                List<FioModel> fioList = await _dataService.GetFioListAsync();
                FioGridLookUpEdit.Properties.DisplayMember = nameof(FioModel.Fio);
                FioGridLookUpEdit.Properties.ValueMember = nameof(FioModel.Tab);
                FioGridLookUpEdit.Properties.DataSource = fioList ?? new List<FioModel>();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка загрузки списка сотрудников: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void LoadData(PlanZagrVyaz data)
        {
            _planBindingSource.DataSource = data;

            txtArticul.DataBindings.Add("EditValue", bs, nameof(PlanZagrVyaz.pzvArticul));
            txtModel.DataBindings.Add("EditValue", bs, nameof(PlanZagrVyaz.pzvMod));
            spinKol.DataBindings.Add("EditValue", bs, nameof(PlanZagrVyaz.pzvKol));
            dateStart.DataBindings.Add("EditValue", bs, nameof(PlanZagrVyaz.pzvDateStart));
            dateEnd.DataBindings.Add("EditValue", bs, nameof(PlanZagrVyaz.pzvDateEnd));
            spinNChasi.DataBindings.Add("EditValue", bs, nameof(PlanZagrVyaz.pzvNChasi));
        }

        private async void FioGridLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (FioGridLookUpEdit.EditValue == null)
                {
                    _planBindingSource.DataSource = null;
                    PlanZagrVyazGridControl.RefreshDataSource();
                    return;
                }

                if (!int.TryParse(FioGridLookUpEdit.EditValue.ToString(), out int tab))
                {
                    return;
                }

                List<KnitterPZVModel> plan = await _dataService.GetPlanByTabAsync(999);//(tab);
                await GridHelper.LoadListDataAsync(PlanZagrVyazGridControl, _planBindingSource, plan ?? new List<KnitterPZVModel>());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка загрузки плана: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
