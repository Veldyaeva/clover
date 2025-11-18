using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.Core.interfaces;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution.Models;
using SewingProduction.Help.Form;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;

namespace SewingProduction.Features.UserDistribution.Forms
{
    public partial class TestForm1 : CustomForm, IDataUpdatableFormAsync, IDataUpdatableForm
    {
        private readonly UserClass _user;
        private readonly TestModel1DataService _testModel1DataService;
        private readonly AllTableNameDataService _tableDataService;
        private readonly AllColumnNameDataService _columnDataService;
        private readonly ServiceBroker _serviceBrokerForTable1;
        private readonly ServiceBroker _serviceBrokerForTable2;
        int selectTable;
        public TestForm1(UserClass user) : base(user)
        {
            InitializeComponent();
            _user = user;
            _testModel1DataService = new TestModel1DataService(new DbService(new DatabaseHelper()));
            _tableDataService = new AllTableNameDataService(new DbService(new DatabaseHelper()), new DatabaseHelper());
            _columnDataService = new AllColumnNameDataService(new DbService(new DatabaseHelper()), new DatabaseHelper());
            _serviceBrokerForTable1 = new ServiceBroker(this);
            _serviceBrokerForTable2 = new ServiceBroker(this);
            customLabel4.FontSizePermission = customLabel4.Font.Size + 2;
        }
        private async void TestForm1_Load(object sender, EventArgs e)
        {
            //_serviceBrokerForTable1.StartBroker();
            //_serviceBrokerForTable1.StartListening("TestID, TestName, TestFirst, TestSecond", "testTable1");
            //_serviceBrokerForTable2.StartBroker();
            //_serviceBrokerForTable2.StartListening("idZeh, nameZeh, address, idProizv", "ZehList");
            await LoadDataAsync();
            SetupGrid();
        }

        public async Task UpdateDataInFormAsync(string _table)
        {
            //switch (_table)
            //{
            //    case "testTable1":
            //        await LoadDataAsync();
            //        break;

            //    case "ZehList":
            //        customTextBox2.Text = $"Обновление в {_table}";
            //        break;
            //}
        }
        private async Task LoadDataAsync()
        {
            bindingSource1.DataSource = await _tableDataService.GetListTableAsync();
        }

        private void SetupGrid()
        {

            gridViewTable1.OptionsDetail.EnableMasterViewMode = true;
            gridViewTable1.OptionsDetail.ShowDetailTabs = false;
            gridViewTable1.OptionsView.ShowGroupPanel = false;

            bandedGridView1.OptionsView.ShowGroupPanel = false;
            bandedGridView1.OptionsBehavior.Editable = false;


            customGridControl1.LevelTree.Nodes[0].RelationName = "Level1";
            customGridControl1.LevelTree.Nodes[0].LevelTemplate = bandedGridView1;

            gridViewTable1.MasterRowGetRelationCount += (s, e) => e.RelationCount = 1;

            gridViewTable1.MasterRowGetChildList += async (s, e) =>
            {
                var view = s as GridView;
                var row = view.GetDataRow(e.RowHandle);
                if (selectTable <= 0)
                    gridViewTable1_FocusedRow();

                if (e.RelationIndex == 0)
                {
                    var list = await _columnDataService.GetListColumnFromTable(selectTable);
                    var dt = ToDataTable(list);
                    var dv = dt.DefaultView;
                    e.ChildList = dv;
                }
            };
        }

        private void gridViewTable1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridViewTable1_FocusedRow();
        }
        private void gridViewTable1_FocusedRow()
        {
            var Id = gridViewTable1.GetFocusedRowCellValue("id_atn");
            selectTable = (Id != null && Id != DBNull.Value) ? Convert.ToInt32(Id) : 0;
            Debug.WriteLine("selected id_atn: " + selectTable);
        }
        public void UpdateDataInForm(string _table)
        {
            LoadData();
        }
        private void LoadData()
        {
            MessageBox.Show("обновление!");
        }

        private void ProductForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _serviceBrokerForTable1.StopBroker();
            _serviceBrokerForTable2.StopBroker();
        }

        private void customButton1_Click(object sender, EventArgs e)
        {
            customButton2.Visible = !customButton2.Visible;
        }
        private static DataTable ToDataTable<T>(IEnumerable<T> items)
        {
            var table = new DataTable(typeof(T).Name);
            var props = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (var p in props)
                table.Columns.Add(p.Name, Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType);

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                    values[i] = props[i].GetValue(item) ?? DBNull.Value;
                table.Rows.Add(values);
            }
            return table;
        }
    }
}
