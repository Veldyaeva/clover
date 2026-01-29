using DevExpress.XtraEditors;
using SewingProduction.Features.Tabel.Models;
using SewingProduction.Features.Tabel.Services;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.Tabel.Forms
{
    public partial class EmployeeTransfer : CustomForm
    {
        public bool EmployeeResult { get; private set; }
        private static DatabaseHelper _dbHelper;
        private static DbService _dbService;
        private readonly ILogger _logger = new FileLogger();
        private static TabelDataService _tabelDataService;
        public BindingSource _spPodr;
        string _fio;
        string _naimenGr;
        int _currentId;
        int _idGroup;
        string _currentMg;
        int _tab;
        public EmployeeTransfer(string fio, string naimenGr, BindingSource spPodr, int currentId, int idGroup, string currentMg,int tab)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);
            _tabelDataService = new TabelDataService(_dbHelper);
            _fio = fio;
            _naimenGr = naimenGr;
            _spPodr = spPodr;
            _currentId = currentId;
            _idGroup = idGroup;
            _currentMg = currentMg.Trim();
            _tab = tab;

        }

        private void EmployeeTransfer_Load(object sender, EventArgs e)
        {
            fioEmployee.Text = "Работник " + _fio;
            PodrText.Text = "Подразделение " + _naimenGr;
            EditPodrLookUpEdit.Properties.DataSource = _spPodr;
            EditPodrLookUpEdit.Properties.DisplayMember = "naimen";
            EditPodrLookUpEdit.Properties.ValueMember = "tnid";
        }

        private void customButton2_Click(object sender, EventArgs e)
        {
            this.EmployeeResult = true;
            this.Hide();
            this.Close();
        }

        private void customButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (_idGroup == 19)
                {

                }
                if (_idGroup == 20)
                {
                    DateTime selectedDate = customDateTimePicker1.Value;
                    string dateString = selectedDate.ToString("yyyyMMdd");
                    int newIdGr = (int)EditPodrLookUpEdit.EditValue;
                    string query = $"update tabel_zl set gr = {newIdGr} where id = {_currentId} and mg ='{_currentMg}'";
                    _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { });
                    string queryUpdGr = $"update zl_spisok set gr_s = gr, gr = {newIdGr} where tabno = {_tab}";
                    _dbHelper.ExecuteNonQueryAsync(queryUpdGr, new Dictionary<string, object> { });
                    string queryIns = $"insert into dv_pers (tabno, firstname, middlename, lastname, dolg, gr_s, gr_n, dat, pom, podr, dat_f) " +
                    $" select tabno,firstname,middlename,lastname,dolg,gr_s,gr,getdate(),pom,'zl','{dateString}' from zl_spisok where tabno = {_tab} ";
                    _dbHelper.ExecuteNonQueryAsync(queryIns, new Dictionary<string, object> { });
                    MessageBox.Show("Успешно!");
                    this.EmployeeResult = true;
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch(SqlException ex)
            { MessageBox.Show(ex.Message); }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка!");
            }
        }
    }

}
 

