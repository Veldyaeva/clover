using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Helpers;
using DevExpress.XtraReports.UI;
using SewingProduction.Features.Tabel.Models;
using SewingProduction.Features.Tabel.Services;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Report;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.Tabel.Forms
{
    public partial class OtklTabel : CustomForm
    {
        private static DatabaseHelperSQL _dbHelper;
        private static DbService _dbService;
        private readonly ILogger _logger = new FileLogger();
        private static TabelDataService _tabelDataService;
        public int _idGr;
        public string _mg;
        public int _year;
        public int _groupId;
        public int _orionTag;
        public BindingSource _otklTabel;
        public BindingSource _spisokGr;
        public OtklTabel(UserClass User, int idGr, string mg, int groupId, int OrionTag, BindingSource spisokGr) : base(User)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelperSQL();
            _dbService = new DbService(_dbHelper);
            _tabelDataService = new TabelDataService(_dbHelper);
            _idGr = idGr;
            _mg = mg;
            _groupId = groupId;
            _orionTag = OrionTag;
            _spisokGr = spisokGr;
            _year = Convert.ToInt32(_mg.Substring(2, 2));
        }
        public OtklTabel(UserClass User) : base(User)
        {
            InitializeComponent();
        }
        private async void OtklTabel_Load(object sender, EventArgs e)
        {
            FillMonths();
            if (!string.IsNullOrWhiteSpace(_mg) && _mg.Length == 4)
            {
                int month = Convert.ToInt32(_mg.Substring(0, 2));

                lookUpMonth.EditValue = month;
            }
            lookUpPodrazdelenie.EditValue = _idGr;
            gridView1.ShowLoadingPanel();
            _otklTabel = new BindingSource { DataSource = await _tabelDataService.GetOtklParsecOrion(_idGr, _mg, _groupId, _orionTag) };
            gridView1.HideLoadingPanel();
            gridControl1.DataSource = _otklTabel;
            lookUpPodrazdelenie.Properties.DataSource = _spisokGr;
            lookUpPodrazdelenie.Properties.DisplayMember = "naimen";
            lookUpPodrazdelenie.Properties.ValueMember = "tnid";
            colTimeIn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            colTimeIn.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm:ss";
            colTimeOut.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            colTimeOut.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm:ss";
            colPodrazdelenie.FieldName = "naimen";
            colFio.FieldName = "fio";
            colDate.FieldName = "dat";
            colReason.FieldName = "prichina";
            colTimeIn.FieldName = "t_in";
            colTimeOut.FieldName = "t_out";
            colDayInTab.FieldName = "DayValue";
        }
        private void FillMonths()
        {
            var months = new[]
            {
            new { Id = 1, Name = "Январь" },
            new { Id = 2, Name = "Февраль" },
            new { Id = 3, Name = "Март" },
            new { Id = 4, Name = "Апрель" },
            new { Id = 5, Name = "Май" },
            new { Id = 6, Name = "Июнь" },
            new { Id = 7, Name = "Июль" },
            new { Id = 8, Name = "Август" },
            new { Id = 9, Name = "Сентябрь" },
            new { Id = 10, Name = "Октябрь" },
            new { Id = 11, Name = "Ноябрь" },
            new { Id = 12, Name = "Декабрь" }
            };

            lookUpMonth.Properties.DataSource = months;
            lookUpMonth.Properties.DisplayMember = "Name";
            lookUpMonth.Properties.ValueMember = "Id";
            lookUpMonth.Properties.NullText = "";
        }

        private async void btnApply_Click(object sender, EventArgs e)
        {
            int month = Convert.ToInt32(lookUpMonth.EditValue);
            int grId = Convert.ToInt32(lookUpPodrazdelenie.EditValue);
            _mg = month.ToString("00") + _mg.Substring(2, 2);
            gridControl1.DataSource = null;
            gridView1.ShowLoadingPanel();
            _otklTabel = new BindingSource { DataSource = await _tabelDataService.GetOtklParsecOrion(grId, _mg, _groupId, _orionTag) };
            gridView1.HideLoadingPanel();
            gridControl1.DataSource = _otklTabel;
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == colTimeOut || e.Column == colTimeIn)
            {
                if (e.Value == null || e.Value == DBNull.Value)
                    e.DisplayText = "";
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            int grId = Convert.ToInt32(lookUpPodrazdelenie.EditValue);
            TimeSheetOtklReport report1 = new TimeSheetOtklReport();
            report1.RequestParameters = true;
            report1.Parameters["idgr"].Value = grId;
            report1.Parameters["idgr"].Visible = false;
            report1.Parameters["idgroup"].Value = _groupId;
            report1.Parameters["idgroup"].Visible = false;
            report1.Parameters["mg"].Value = _mg;
            report1.Parameters["mg"].Visible = false;
            report1.Parameters["orionTag"].Value = _orionTag;
            report1.Parameters["orionTag"].Visible = false;
            ReportPrintTool reportPrintTool1 = new ReportPrintTool(report1);
            reportPrintTool1.ShowPreview();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }
    }
}
