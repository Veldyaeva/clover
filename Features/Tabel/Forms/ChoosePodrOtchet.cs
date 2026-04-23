using DevExpress.XtraCharts.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using Newtonsoft.Json;
using SewingProduction.Features.Tabel.Models;
using SewingProduction.Features.Tabel.Services;
using SewingProduction.Helpers;
using SewingProduction.Report;
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
using Formatting = Newtonsoft.Json.Formatting;


namespace SewingProduction.Features.Tabel.Forms
{
    public partial class ChoosePodrOtchet : CustomForm
    {
        private static DatabaseHelperSQL _dbHelper;
        private static DbService _dbService;
        private readonly ILogger _logger = new FileLogger();
        private static TabelDataService _tabelDataService;
        private BindingSource _SpPodrBS;
        int _idGroup;
        string _currentMG;
        public ChoosePodrOtchet(int idGroup, string currentMG)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.ShowInTaskbar = false;
            //this.TopMost = true;
            //this.Deactivate += (s, e) => this.Close();
            this.BackColor = SystemColors.Menu;
            InitializeComponent();
            _dbHelper = new DatabaseHelperSQL();
            _dbService = new DbService(_dbHelper);
            _tabelDataService = new TabelDataService(_dbHelper);
            _idGroup = idGroup;
            _currentMG = currentMG;

        }

        private async void ChoosePodrOtchet_Load(object sender, EventArgs e)
        {
            //_SpPodrBS = new BindingSource { DataSource =  };
            checkedComboBoxEdit1.Properties.DataSource = await _tabelDataService.GetSpPodrNotRuleAsync(_idGroup);
            checkedComboBoxEdit1.Properties.DisplayMember = "naimen";
            checkedComboBoxEdit1.Properties.ValueMember = "tnid";
        }

        private void customSimpleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void customSimpleButton2_Click(object sender, EventArgs e)
        {
            //List<SpPodr> ids = new List<SpPodr>();

            //foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedComboBoxEdit1.Properties.Items)
            //{
            //    if (item.CheckState == CheckState.Checked)
            //    {
            //        ids.Add(Convert.ToInt32(item.Value),1," ");
            //    }
            //}
            var editValue = checkedComboBoxEdit1.EditValue;
            var ids = editValue?.ToString()
                 .Split(',')
                 .Select(id => int.Parse(id))
                 .ToList();

            var result = ids?.Select(id => new { tnid = id }).ToList();
            string jsonString = JsonConvert.SerializeObject(result, Formatting.Indented);
           
           
            TimeSheetReportSkladi report1 = new TimeSheetReportSkladi();
            report1.RequestParameters = true;
            report1.Parameters["groupString"].Value = "zl";
            report1.Parameters["groupString"].Visible = false;
            report1.Parameters["idgr"].Value = 0;
            report1.Parameters["idgr"].Visible = false;
            report1.Parameters["mg"].Value = _currentMG;
            report1.Parameters["mg"].Visible = false;
            report1.Parameters["tnid"].Value = jsonString;
            report1.Parameters["tnid"].Visible = false;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreview();
        }
    }
}
