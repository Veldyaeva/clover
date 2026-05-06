using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.UserDistribution.Models
{
    public class AllTableNameDataService
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelperSQL _dbHelper;

        public AllTableNameDataService()
        {
            _dbHelper = new DatabaseHelperSQL();
            _dbService = new DbService(_dbHelper);
        }

        public async Task<List<AllTableNameModel>> GetListTableAsync()
        {
            string query = "SELECT id_atn, name, name_rus FROM all_table_name";
            return await _dbService.GetListAsync<AllTableNameModel>(query, new { });
        }

        public async Task<int> SaveAsync(AllTableNameModel table)
        {
            return await _dbService.SaveEntityAsync("all_table_name", "id_atn", table);
        }

        public async Task DeleteAsync(AllTableNameModel table)
        {
            await _dbService.DeleteEntityAsync("all_table_name", "id_atn", table);
        }
        public async Task<int> CheckAsync(string tableName)
        {
            string checkQuery = @"
            SELECT COUNT(*) 
            FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_NAME = @tableName";
            object result = await _dbHelper.ExecuteScalarAsync(checkQuery, new Dictionary<string, object> { { "@tableName", tableName } });
            return Convert.ToInt32(result);
        }
        public async Task<int?> GetTableIdByNameAsync(string tableName)
        {
            string query = "SELECT id_atn FROM all_table_name WHERE name = @name";
            object result = await _dbHelper.ExecuteScalarAsync(query, new Dictionary<string, object>
            {
                { "@name", tableName }
            });

            return result != null && result != DBNull.Value ? Convert.ToInt32(result) : (int?)null;
        }
    }


}

