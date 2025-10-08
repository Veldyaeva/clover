using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.UserDistribution.Models
{
    public class ObjectModel : INotifyPropertyChanged
    {
        private int _objectID;
        private string _objectName;
        private string _objectNameRus;
        private int _formID;
        private int _groupID;
        private int _creatorID;
        private string _objectType;

        [Column("ObjectID")]
        public int ObjectID
        {
            get => _objectID;
            set { if (_objectID != value) { _objectID = value; OnPropertyChanged(nameof(ObjectID)); } }
        }

        [Column("ObjectName")]
        public string ObjectName
        {
            get => _objectName;
            set { if (_objectName != value) { _objectName = value; OnPropertyChanged(nameof(ObjectName)); } }
        }

        [Column("ObjectNameRus")]
        public string ObjectNameRus
        {
            get => _objectNameRus;
            set { if (_objectNameRus != value) { _objectNameRus = value; OnPropertyChanged(nameof(ObjectNameRus)); } }
        }

        [Column("FormID")]
        public int FormID
        {
            get => _formID;
            set { if (_formID != value) { _formID = value; OnPropertyChanged(nameof(FormID)); } }
        }

        [Column("GroupID")]
        public int GroupID
        {
            get => _groupID;
            set { if (_groupID != value) { _groupID = value; OnPropertyChanged(nameof(GroupID)); } }
        }

        [Column("CreatorID")]
        public int CreatorID
        {
            get => _creatorID;
            set { if (_creatorID != value) { _creatorID = value; OnPropertyChanged(nameof(CreatorID)); } }
        }

        [Column("ObjectType")]
        public string ObjectType
        {
            get => _objectType;
            set { if (_objectType != value) { _objectType = value; OnPropertyChanged(nameof(ObjectType)); } }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }

    public class ObjectDataService
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelper _dbHelper;
        public ObjectDataService(DbService dbService, DatabaseHelper dbHelper)
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
