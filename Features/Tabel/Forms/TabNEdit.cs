using DevExpress.CodeParser;
using DevExpress.Xpf.Editors;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraDiagram.Bars;
using DevExpress.XtraEditors;
using SewingProduction.Core.Class;
using SewingProduction.Core.helpers;
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
using static SewingProduction.Core.helpers.BindingSourceHelper;

namespace SewingProduction.Features.Tabel.Forms
{
    public partial class TabNEdit : CustomForm
    {
        private static DatabaseHelperSQL _dbHelper;
        private static DbService _dbService;
        private readonly ILogger _logger = new FileLogger();
        private static TabelDataService _tabelDataService;
        public BindingSource _spPodr;
        public BindingSource _brig;
        public BindingSource _spPodrUpdate;
        int _idGroup;
        int IsCheckTnFl = 0;
        public TabNEdit(UserClass User, int idGroup) : base(User)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            _dbHelper = new DatabaseHelperSQL();
            _dbService = new DbService(_dbHelper);
            _tabelDataService = new TabelDataService(_dbHelper);
            _idGroup = idGroup;
        }
        public TabNEdit(UserClass User) : base(User)
        {
            InitializeComponent();
        }

        private async void TabNEdit_Load(object sender, EventArgs e)
        {
            _spPodr = new BindingSource { DataSource = await _tabelDataService.GetSpPodrAsync(_idGroup) };
            if (_idGroup == 19)
            {
                _brig = new BindingSource { DataSource = await _tabelDataService.GetBrigsAsync() };
                layoutControlItem4.ContentVisible = true;
                layoutBrNaimen.ContentVisible = true;
                layoutControlItem2.ContentVisible = true;
            }
   
            lueBrNaimen.Properties.DataSource = _brig;
            lueBrNaimen.Properties.DisplayMember = "brig";
            lueBrNaimen.Properties.ValueMember = "id_brig";
            lueBrNaimen.Properties.Columns.Clear();
            lueBrNaimen.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("brig", "Наименование"));
            gridControl1.DataSource = _spPodr;
            gridColumnId.FieldName = "tnid";
            gridColumnNaimen.FieldName = "naimen";
        }

        private void customSimpleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNaimen.Text))
            {
                MessageBox.Show("Заполните наименование!");
                return;
            }
            string sql;
            string naimen = txtNaimen.Text;
            int? idBrig = null;
            string? brNaimen = null;
            if (_idGroup == 19)
            {
                if (lueBrNaimen.EditValue != null && !string.IsNullOrWhiteSpace(lueBrNaimen.EditValue.ToString()))
                {
                    idBrig = int.Parse(lueBrNaimen.EditValue.ToString());
                    brNaimen = lueBrNaimen.Text.ToString();
                }
                sql = $"insert into tab_n(naimen,tn_fl,br_naimen,id_brig) values(@naimen,@IsCheckTnFl,@brNaimen,@idBrig)";
                _dbHelper.ExecuteNonQuery(sql, new Dictionary<string, object> { {"@naimen",naimen},{ "@IsCheckTnFl", IsCheckTnFl}
                ,{"@brNaimen", string.IsNullOrWhiteSpace(brNaimen)
                ? DBNull.Value
                :brNaimen},{"@idbrig",idBrig == null ? DBNull.Value:idBrig }
            });
                MessageBox.Show("Успешно!");
                _spPodrUpdate = new BindingSource { DataSource = await _tabelDataService.GetSpPodrAsync(_idGroup) };

                var changes = BindingSourceHelper.GetChanges<SpPodr>(
                  _spPodr,
                  _spPodrUpdate,
                  HashMode.All,
                  keyProperties: new[] { "tnid" });
                BindingSourceHelper.ApplyChanges<SpPodr>(
                   _spPodr,
                   changes,
                   UpdateFieldsMode.All,
                   keyProperties: new[] { "tnid" },
                   gridView1);
            }
            if (_idGroup == 20)
            {
                sql = $"insert into zlgr(naimen) values(@naimen)";
                _dbHelper.ExecuteNonQuery(sql, new Dictionary<string, object> { {"@naimen",naimen}});
                MessageBox.Show("Успешно!");
                _spPodrUpdate = new BindingSource { DataSource = await _tabelDataService.GetSpPodrAsync(_idGroup) };

                var changes = BindingSourceHelper.GetChanges<SpPodr>(
                  _spPodr,
                  _spPodrUpdate,
                  HashMode.All,
                  keyProperties: new[] { "tnid" });
                BindingSourceHelper.ApplyChanges<SpPodr>(
                   _spPodr,
                   changes,
                   UpdateFieldsMode.All,
                   keyProperties: new[] { "tnid" },
                   gridView1);

            }
        }

        private void customCheckBoxTnFl_CheckedChanged(object sender, EventArgs e)
        {
            IsCheckTnFl = customCheckBoxTnFl.Checked ? 1 : 0;
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            _spPodrUpdate = new BindingSource { DataSource = await _tabelDataService.GetSpPodrAsync(_idGroup) };
            var changes = BindingSourceHelper.GetChanges<SpPodr>(
              _spPodr,
              _spPodrUpdate,
              HashMode.All,
              keyProperties: new[] { "tnid" });
            BindingSourceHelper.ApplyChanges<SpPodr>(
               _spPodr,
               changes,
               UpdateFieldsMode.All,
               keyProperties: new[] { "tnid" },
               gridView1);
        }
    }
}
