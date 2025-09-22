using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace SewingProduction.Help.Form
{
    public class TestModel1 : INotifyPropertyChanged
    {
        private int _testID;
        private string _testName;
        private int _testFirst;
        private DateTime _testSecond;

        [Column("TestID")]
        public int TestID
        {
            get => _testID;
            set { if (_testID != value) { _testID = value; OnPropertyChanged(nameof(TestID)); } }
        }

        [Column("TestName")]
        public string TestName
        {
            get => _testName;
            set { if (_testName != value) { _testName = value; OnPropertyChanged(nameof(TestName)); } }
        }

        [Column("TestFirst")]
        public int TestFirst
        {
            get => _testFirst;
            set { if (_testFirst != value) { _testFirst = value; OnPropertyChanged(nameof(TestFirst)); } }
        }

        [Column("TestSecond")]
        public DateTime TestSecond
        {
            get => _testSecond;
            set { if (_testSecond != value) { _testSecond = value; OnPropertyChanged(nameof(TestSecond)); } }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public class TestModel1DataService
    {
        private readonly DbService _dbService;

        public TestModel1DataService(DbService dbService)
        {
            _dbService = dbService;
        }

        // Получение всех записей
        public async Task<List<TestModel1>> GetAllAsync()
        {
            string query = @"SELECT TestID, TestName, TestFirst, TestSecond FROM TestTable1";
            return await _dbService.GetListAsync<TestModel1>(query, new { });
        }

        // Сохранение (вставка или обновление)
        public async Task<int> SaveAsync(TestModel1 model)
        {
            return await _dbService.SaveEntityAsync("TestTable1", "TestID", model);
        }

        // Удаление
        public async Task DeleteAsync(TestModel1 model)
        {
            await _dbService.DeleteEntityAsync("TestTable1", "TestID", model);
        }

        // Поиск по ID
        public async Task<TestModel1> GetByIdAsync(int id)
        {
            string query = "SELECT * FROM TestTable1 WHERE TestID = @id";
            var list = await _dbService.GetListAsync<TestModel1>(query, new { id });
            return list.Count > 0 ? list[0] : null;
        }
    }
}
