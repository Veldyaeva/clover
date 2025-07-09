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
using SewingProduction.Features.Sprav;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution;
using SewingProduction.Features.UserDistribution.Models;
using SewingProduction.Help.Form;
using SewingProduction.Helpers;
using SewingProduction.Services;
using SewingProduction.Core.interfaces;

namespace SewingProduction.Features.UserDistribution.Forms
{
    public partial class TestForm1 : CustomForm, IDataUpdatableFormAsync, IDataUpdatableForm
    {
        private readonly UserClass _user;
        private readonly TestModel1DataService _testModel1DataService;
        private readonly ServiceBroker _serviceBrokerForTable1;
        private readonly ServiceBroker _serviceBrokerForTable2;
        public TestForm1(UserClass user) : base(user)
        {
            InitializeComponent();
            _user = user;
            _testModel1DataService = new TestModel1DataService(new DbService(new DatabaseHelper("ace")));
            _serviceBrokerForTable1 = new ServiceBroker(this);
            _serviceBrokerForTable2 = new ServiceBroker(this);
        }
        private async void TestForm1_Load(object sender, EventArgs e)
        {
            _serviceBrokerForTable1.StartBroker();
            _serviceBrokerForTable1.StartListening("TestID, TestName, TestFirst, TestSecond", "testTable1");
            _serviceBrokerForTable2.StartBroker();
            _serviceBrokerForTable2.StartListening("idZeh, nameZeh, address, idProizv", "ZehList");
            await LoadDataAsync();
        }

        public async Task UpdateDataInFormAsync(string _table)
        {
            switch (_table)
            {
                case "testTable1":
                    await LoadDataAsync();
                    break;

                case "ZehList":
                    customTextBox2.Text = $"Обновление в {_table}";
                    break;
            }
        }
        private async Task LoadDataAsync()
        {
            customGridControl1.DataSource = await _testModel1DataService.GetAllAsync();
            customGridControl1.InitializeAccess(_user, this.Name, new List<string> { "testTable1" });
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

    }
}
