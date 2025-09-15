using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace SewingProduction.Features.UserDistribution.Models
{

    public class FormModel : INotifyPropertyChanged
    {
        private int _projectFormsID;
        private string _nameForm;
        private string _nameFormRus;
        private int _creatorID;

        [Column("ProjectFormsID")]
        public int ProjectFormsID
        {
            get => _projectFormsID;
            set { if (_projectFormsID != value) { _projectFormsID = value; OnPropertyChanged(nameof(ProjectFormsID)); } }
        }

        [Column("NameForm")]
        public string NameForm
        {
            get => _nameForm;
            set { if (_nameForm != value) { _nameForm = value; OnPropertyChanged(nameof(NameForm)); } }
        }

        [Column("NameFormRus")]
        public string NameFormRus
        {
            get => _nameFormRus;
            set { if (_nameFormRus != value) { _nameFormRus = value; OnPropertyChanged(nameof(NameFormRus)); } }
        }

        [Column("CreatorID")]
        public int CreatorID
        {
            get => _creatorID;
            set { if (_creatorID != value) { _creatorID = value; OnPropertyChanged(nameof(CreatorID)); } }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }

    public class FormDataService
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelper _dbHelper;
        public FormDataService(DbService dbService, DatabaseHelper dbHelper)
        {
            _dbService = dbService;
            _dbHelper = dbHelper;
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


