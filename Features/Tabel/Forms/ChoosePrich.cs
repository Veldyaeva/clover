using SewingProduction.Features.Tabel.Models;
using SewingProduction.Features.Tabel.Services;
using SewingProduction.Features.UserDistribution.Helpers;
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
    public partial class ChoosePrich : CustomForm
    {
        private static DatabaseHelper _dbHelper;
        private static DbService _dbService;
        private readonly ILogger _logger = new FileLogger();
        private static TabelDataService _tabelDataService;
        private List<PrichInclude> _PrichIncludeList = new List<PrichInclude>();
        int _id;
        int _tabno;
        string _fio;
        public ChoosePrich(UserClass user,int id, int tabno, string fio) : base(user)
        {
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);
            _tabelDataService = new TabelDataService(_dbHelper);
            _PrichIncludeList = new List<PrichInclude>();
            InitializeComponent();
            _id = id;
            _tabno = tabno;
            _fio = fio;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
        }
        public ChoosePrich(UserClass User) : base(User)
        {
            InitializeComponent();
        }

        private async void ChoosePrich_Load(object sender, EventArgs e)
        {
            _PrichIncludeList = await _tabelDataService.GetPrichIncludeAsync();
            customLookUpEdit1.Properties.DataSource = _PrichIncludeList;
            customLookUpEdit1.Properties.DisplayMember = "naimen";
            customLookUpEdit1.Properties.ValueMember = "tsp_id";
            customLabelTabno.Text = _tabno.ToString();
            customLabelFio.Text = _fio;

        }

        private void customSimpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (customLookUpEdit1.EditValue != null)
                {
                    string editTsPlanPr = customLookUpEdit1.Text;
                    int editTspId = (int)customLookUpEdit1.EditValue;
                    string sql;
                    sql = $"UPDATE tabel_sp SET ts_plan_pr = '{editTsPlanPr}',ts_tsp_id = {editTspId} WHERE id = {_id}";
                    _dbHelper.ExecuteNonQuery(sql, new Dictionary<string, object> { });
                    MessageBox.Show("Успешно!");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Не выбрана причина!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void customSimpleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }
    }
}
