using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.UserDistribution.DataService
{
    public class ShareUserDataService
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelperSQL _dbHelper;

        public ShareUserDataService()
        {
            _dbHelper = new DatabaseHelperSQL();
            _dbService = new DbService(_dbHelper);
        }

        public async Task<List<DistributionModel>> GetDistribution()
        {
            string query;
            query = @"SELECT DistributionID,ParentID,ChildID FROM Distribution";

            return await _dbService.GetListAsync<DistributionModel>(query, new { });

        }
        public async Task<int> SaveAsync(DistributionModel Distribution)
        {
            var result = await _dbService.SaveEntityAsync("Distribution", "DistributionID", Distribution);
            return result;
        }

        public async Task DeleteAsync(DistributionModel Distribution)
        {
            await _dbService.DeleteEntityAsync("Distribution", "DistributionID", Distribution);
        }
    }
}
