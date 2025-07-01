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
using SewingProduction.Features.UserDistribution.Models;
using SewingProduction.form.UserDistribution;

namespace SewingProduction.Features.UserDistribution.Forms
{
    public partial class TestForm1 : CustomForm
    {
        private readonly UserClass _user;
        private readonly TestModel1DataService _testModel1DataService;
        private List<TestModel1> _tables;
        public TestForm1(UserClass user) : base(user)
        {
            InitializeComponent();
        }

        private void customGridControl1_Load(object sender, EventArgs e)
        {
            bindingSource1.DataSource = _testModel1DataService.GetAllAsync();
        }
    }
}
