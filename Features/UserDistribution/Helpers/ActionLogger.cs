using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SewingProduction.Helpers;

namespace SewingProduction.Features.UserDistribution.Helpers
{
    public static class ActionLogger
    {
        private static readonly DatabaseHelperSQL dbHelper = new DatabaseHelperSQL();

        public static async Task Log(int userId, string eventDescription, string komp = null, string NameForm = null, string NameObject = null)
        {
            komp = Environment.MachineName;

            string query = @"
                INSERT INTO ActionHistory 
                (UserID, NameObject, NameForm, Event, EventDate, Komp)
                VALUES (@UserID, @NameObject, @NameForm, @Event, @EventDate, @Komp)";

            await dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object>
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
