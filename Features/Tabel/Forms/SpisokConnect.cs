using Dapper;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraSpreadsheet.UI;
using SewingProduction.Extensions;
using SewingProduction.Features.CuttingProduction.Models;
using SewingProduction.Features.CuttingProduction.Services;
using SewingProduction.Features.Tabel.Models;
using SewingProduction.Features.Tabel.Services;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.Tabel.Forms
{
    public partial class SpisokConnect : CustomForm
    {
        private static DatabaseHelper _dbHelper;
        private static DbService _dbService;
        private readonly ILogger _logger = new FileLogger();
        private BindingSource _spisokBindingSource;
        private List<ListForLinking> _currentSpisokData = new List<ListForLinking>();
        private BindingList<ListForLinking> _spisokBindingList;
        private static TabelDataService _tabelDataService;
        public BindingSource _scheduleOfWork;
        public BindingSource _zlPodr;
        public SpisokConnect()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);
            _tabelDataService = new TabelDataService(_dbHelper);
        }
        private async Task InitializeBindingsAsync()
        {
            try
            {
                var spisokViewTask = Task.Run(() =>
                {
                    _spisokBindingList = new BindingList<ListForLinking>();
                    _spisokBindingSource = new BindingSource { DataSource = _spisokBindingList };
                });
                await Task.WhenAll(spisokViewTask);
                #region увязка grid со ListForLinking
                customGridSpisokForLinking.DataSource = _spisokBindingSource;
                gridSpisokInn.FieldName = "inn";
                gridSpisokLastName.FieldName = "firstname";
                gridSpisokMiddleName.FieldName = "lastname";
                gridSpisokFirstName.FieldName = "middlename";
                gridSpisokDateP.FieldName = "date_p";
                gridSpisokDateU.FieldName = "date_u";
                gridSpisokUin.FieldName = "s_uin";
                gridSpisokOrgName.FieldName = "orgName";
                gridSpisokGrafik.FieldName = "idSchedule";
                gridSpisokPodr1c.FieldName = "podrName";
                gridSpisokPodr.FieldName = "gr";
                gridSpisokGrafik.FieldName = "scheduleName";
                //var items = await _tabelDataService.GetScheduleOfWorkAsync();
                //_scheduleOfWork = new BindingSource { DataSource = items.ToList() };
                //repositoryItemLookUpEdit1.DataSource = _scheduleOfWork;
                //repositoryItemLookUpEdit1.DisplayMember = "scheduleName";
                //repositoryItemLookUpEdit1.ValueMember = "id";
                var zlPodr = await _tabelDataService.GetzlPodrAsync();
                _zlPodr = new BindingSource { DataSource = zlPodr.ToList() };
                repositoryItemLookUpEdit2.DataSource = _zlPodr;
                repositoryItemLookUpEdit2.DisplayMember = "naimen";
                repositoryItemLookUpEdit2.ValueMember = "gr";
                // 
                //repositoryItemComboBox1.Items
                #endregion

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
        }
        private async void SpisokConnect_Load(object sender, EventArgs e)
        {

            Task bindingsTask = InitializeBindingsAsync();
            await Task.WhenAll(bindingsTask);
            try
            {
                await LoadAsyncSpisok();
            }
            catch { }
        }
        private async Task LoadAsyncSpisok()
        {
            _spisokBindingSource.Clear();
            _spisokBindingSource.ResetBindings(false);
            var spisokData = await _tabelDataService.GetListForLinkingsAsync();
            if (spisokData != null)
            {
                await _logger.LogEventAsync($"Получены данные ListForLinking", "LoadCuttingForm");

                await this.InvokeAsync(() =>
                {
                    _currentSpisokData = spisokData;                // Обновляем текущую модель
                    _spisokBindingSource.DataSource = _currentSpisokData; // Привязываем данные к форме
                });

                await _logger.LogEventAsync($"Данные ListForLinking успешно загружены", "LoadVyazPlanDataAsync");
                //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
                //_spisokBindingList.Add(spisokData[0]);
                _spisokBindingSource.ResetBindings(false);

            }
            else
            {
                await _logger.LogEventAsync($"Не удалось найти данные spisok", "GetListForLinkingsAsync");
            }
        }

        private void customGridSpisokForLinking_Click(object sender, EventArgs e)
        {

        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            using (var connection = _dbHelper.GetConnection())
            {
                string sql;
                sql = $"UPDATE zl_spisok SET {e.Column.FieldName} = N'{e.Value}' WHERE uin = {Convert.ToInt32(gridView1.GetDataRow(e.RowHandle)["uin"])}";
                _dbHelper.ExecuteNonQuery(sql, new Dictionary<string, object> { });
            }
        }
    }
}
