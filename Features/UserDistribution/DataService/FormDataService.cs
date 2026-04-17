using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.UserDistribution.Models
{
    public class FormDataService
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelper _dbHelper;
        public FormDataService()
        {
            _dbHelper = new DatabaseHelper("ace");
            _dbService = new DbService(_dbHelper);
        }

        public async Task<List<FormModel>> GetListFormsAsync()
        {
            string query = "SELECT ProjectFormsID, NameForm, NameFormRus, CreatorID FROM ProjectForms";
            return await _dbService.GetListAsync<FormModel>(query, new { });
        }

        public async Task<int> SaveAsync(FormModel form)
        {
            return await _dbService.SaveEntityAsync("ProjectForms", "ProjectFormsID", form);
        }

        public async Task DeleteAsync(FormModel form)
        {
            await _dbService.DeleteEntityAsync("ProjectForms", "ProjectFormsID", form);
        }
        public async Task<int?> GetFormIdByNameAsync(string formName)
        {
            string query = "SELECT ProjectFormsID FROM ProjectForms WHERE NameForm = @name";
            object result = await _dbHelper.ExecuteScalarAsync(query, new Dictionary<string, object>
            {
                { "@name", formName }
            });

            return result != null && result != DBNull.Value ? Convert.ToInt32(result) : (int?)null;
        }
    }

}


