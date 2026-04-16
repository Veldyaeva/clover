using DevExpress.CodeParser;
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
    public partial class ChooseUin : CustomForm
    {
        public bool ChooseResult = false;
        private static DatabaseHelperSQL _dbHelper;
        private static DbService _dbService;
        private readonly ILogger _logger = new FileLogger();
        private static TabelDataService _tabelDataService;
        public BindingSource _spisok1c;
        string _lastName;
        string _firstName;
        string _middleName;
        string _naimenPodr;
        string _nameGroup;
        int _tabno;

        public ChooseUin(string lastName, string firstName, string middleName, int tabno, string naimenPodr, string nameGroup)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelperSQL();
            _dbService = new DbService(_dbHelper);
            _tabelDataService = new TabelDataService(_dbHelper);
            _lastName = lastName;
            _firstName = firstName;
            _middleName = middleName;
            _naimenPodr = naimenPodr;
            _nameGroup = nameGroup;
            _tabno = tabno;
        }

        private async void ChooseUin_Load(object sender, EventArgs e)
        {
            this.Text = $"{_lastName.Trim()} {_firstName.Trim()} {_middleName.Trim()}  {_naimenPodr.Trim()}";
            _spisok1c = new BindingSource { DataSource = await _tabelDataService.GetSpisok1cAsync(_lastName, _firstName, _middleName) };
            customGridControl1.DataSource = _spisok1c;
            gridSpisokTab1c.FieldName = "tab1c";
            gridSpisokUin.FieldName = "uin";
            gridSpisokFirstName.FieldName = "FirstName";
            gridSpisokLastName.FieldName = "LastName";
            gridSpisokMiddleName.FieldName = "MiddleName";
            gridSpisokBDay.FieldName = "bDay";
            gridSpisokOrgName.FieldName = "orgName";
            gridSpisokPodrName.FieldName = "podrName";
            gridSpisokDateU.FieldName = "date_u";
            gridSpisokDateP.FieldName = "date_p";


        }

        private void gridViewSpisok_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

        }

        private void customSimpleButton1_Click(object sender, EventArgs e)
        {
            int rowHandle = gridViewSpisok.FocusedRowHandle;
            if (rowHandle < 0)
            {
                MessageBox.Show("Ничего не выбрано!");
                return;
            }
            string uin = gridViewSpisok.GetRowCellValue(rowHandle, "uin").ToString(); 
            string tab1c = gridViewSpisok.GetRowCellValue(rowHandle, "tab1c").ToString();
            DialogResult result = MessageBox.Show(
              $"Вы уверенны, что хотите привязать УИН {uin} к {_lastName.Trim()} {_firstName.Trim()} {_middleName.Trim()}?",
              "Подтверждение",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var connection = _dbHelper.GetConnection())
                    {
                        if (_nameGroup == "zl")
                        {
                            string sql;
                            sql = $"UPDATE zl_spisok SET s_uin = '{uin.Trim()}',tab1c = '{tab1c.Trim()}' WHERE tabno = {_tabno}";
                            _dbHelper.ExecuteNonQuery(sql, new Dictionary<string, object> { });
                            MessageBox.Show("Успешно!");
                            this.ChooseResult = true;
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        if (_nameGroup == "shp")
                        {
                            string sql;
                            sql = $"UPDATE fio SET uin = '{uin.Trim()}',tab1c = isnull(tab1c,'{tab1c.Trim()}') WHERE tab = {_tabno}";
                            _dbHelper.ExecuteNonQuery(sql, new Dictionary<string, object> { });
                            MessageBox.Show("Успешно!");
                            this.ChooseResult = true;
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                    }
                }
                catch (Exception ex) 
                {
                    MessageBox.Show("При обновлении произошла ошибка!");
                    
                }
            }
            if (result == DialogResult.No)
            {
                this.ChooseResult = false;
                this.Hide();
                this.Close();
            }
        }
    }
}
