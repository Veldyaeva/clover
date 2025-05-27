using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars.Ribbon;
using SewingProduction.Helpers;

namespace SewingProduction.Features.UserDistribution.Helpers
{
    public static class ActionLogger
    {
        private static readonly DatabaseHelper dbHelper = new DatabaseHelper("ace");

        public static async Task Log(int userId, string eventDescription, string komp = null, string NameForm = null, string NameObject = null)
        {
            komp = Environment.MachineName;

            string query = @"
                INSERT INTO ActionHistory 
                (UserID, NameObject, NameForm, Event, EventDate, Komp)
                VALUES (@UserID, @NameObject, @NameForm, @Event, @EventDate, @Komp)";

            await dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object>
            {
                ["@UserID"] = userId,
                ["@NameObject"] = NameObject ?? (object)DBNull.Value,
                ["@NameForm"] = NameForm ?? (object)DBNull.Value,
                ["@Event"] = eventDescription,
                ["@EventDate"] = DateTime.Now,
                ["@Komp"] = komp
            });
        }

    }
}
