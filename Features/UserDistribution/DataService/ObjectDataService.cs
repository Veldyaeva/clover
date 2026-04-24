using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.UserDistribution.Models
{
    public class ObjectDataService
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelperSQL _dbHelper;
        public ObjectDataService(DbService dbService, DatabaseHelperSQL dbHelper)
        {
            _dbService = dbService;
            _dbHelper = dbHelper;
        }

        public async Task<List<ObjectModel>> GetListObjectGridAsync(int formId)
        {
            string query = @"SELECT ObjectID, ObjectName, ObjectNameRus, FormID, GroupID, CreatorID, ObjectType 
                             FROM ObjectForm 
                             WHERE FormID = @FormID and ObjectType like '%grid%'";
            return await _dbService.GetListAsync<ObjectModel>(query, new { FormID = formId });
        }

        public async Task<int> SaveAsync(ObjectModel obj)
        {
            return await _dbService.SaveEntityAsync("ObjectForm", "ObjectID", obj);
        }

        public async Task DeleteAsync(ObjectModel obj)
        {
            await _dbService.DeleteEntityAsync("ObjectForm", "ObjectID", obj);
        }
    }
}
