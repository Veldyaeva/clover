using DevExpress.XtraDataLayout;
using DevExpress.XtraEditors;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class KnitterWorkSpace : Form
    {
        private readonly KnitterOrchestrator _orchestrator;
        private readonly BindingSource _planBindingSource = new BindingSource();
        private readonly PlanZagrVyazService _planService;
        public KnitterWorkSpace()
        {
            
            InitializeComponent();
                dataLayoutControl1.DataSource = _planBindingSource;

            //    dataLayoutControl1.RetrieveFields(new RetrieveFieldsParameters
            //    {
            //        DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
            //    });

            var dbHelper = new DatabaseHelper();
            var repo = new KnitterRepository(dbHelper);
            _orchestrator = new KnitterOrchestrator(repo, new FileLogger());

            PlanZagrVyazGridControl.DataSource = _planBindingSource;
            this.Load += async (s, e) => await InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            try
            {
                // Заполняем список ФИО
                List<FioModel> fioList = await _orchestrator.GetFioListAsync();
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

                List<KnitterPZVModel> plan = await _orchestrator.GetPlanByTabAsync(999);//(tab);
                await GridHelper.LoadListDataAsync(PlanZagrVyazGridControl, _planBindingSource, plan ?? new List<KnitterPZVModel>());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка загрузки плана: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
