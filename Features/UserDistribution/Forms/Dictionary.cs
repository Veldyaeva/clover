using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution.Models;
using SewingProduction.form.UserDistribution.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.UserDistribution.Forms
{
    public partial class Dictionary : CustomForm
    {
        private readonly AllTableNameDataService _tableService;
        private readonly AllColumnNameDataService _columnService;
        private List<AllTableNameModel> _tables;
        private List<AllColumnNameModel> _columns;
        public Dictionary(UserClass user) : base(user)
        {
            InitializeComponent();
            var dbService = new DbService(new DatabaseHelper("ace"));
            _tableService = new AllTableNameDataService(dbService);
            _columnService = new AllColumnNameDataService(dbService);

            customGridControlTable.DataSource = bindingSourceTable;
            customGridControlColumn.DataSource = bindingSourceColumn;
        }

        private async void customGridControlTable_Load(object sender, EventArgs e)
        {
            await LoadTablesAsync();
        }

        private async Task LoadTablesAsync()
        {
            _tables = await _tableService.GetListTable();
            bindingSourceTable.DataSource = _tables;
        }


        private async Task LoadColumnsFromSelectedTableAsync()
        {
            if (bindingSourceTable.Current is AllTableNameModel selectedTable)
            {
                _columns = await _columnService.GetListColumnFromTable(selectedTable.IdAtn);
                bindingSourceColumn.DataSource = _columns;
            }
        }


        private async void customGridControlTable_Click(object sender, EventArgs e)
        {
            await LoadColumnsFromSelectedTableAsync();
        }

    }
}
