using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;

namespace SewingProduction.Features.UserDistribution.Forms
{
    public partial class ActionHistory : CustomForm
    {
        private readonly ActionHistoryDataService _actionHistoryDataService;
        DatabaseHelperSQL dbHelper = new DatabaseHelperSQL();
        public ActionHistory(UserClass user) : base(user)
        {
            InitializeComponent();
            _actionHistoryDataService = new ActionHistoryDataService(dbHelper);
        }

        private async void ActionHistory_Load(object sender, EventArgs e)
        {
            DataTable historyData = await _actionHistoryDataService.LoadHistory();
            customGridHistory.DataSource = historyData;
        }
    }

    public class ActionHistoryDataService
    {
        private readonly DatabaseHelperSQL _dbHelper;
        public ActionHistoryDataService(DatabaseHelperSQL dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public async Task<DataTable> LoadHistory()
        {
            string query = @"
                SELECT 
                    ah.ActionHistoryID,
                    ah.UserID,
                    u.UserName,
                    ah.NameForm,
                    ah.NameObject,
                    ah.Event,
                    ah.EventDate,
                    ah.Komp
                FROM ActionHistory ah
                LEFT JOIN Users u ON u.UserID = ah.UserID
                ORDER BY ah.EventDate DESC";

            return await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { });
        }
    }
}
