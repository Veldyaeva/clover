using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.Services;

namespace SewingProduction.Features.UserDistribution.Models
{

    public class AllTableNameModel : INotifyPropertyChanged
    {
        private int _idAtn;
        private string _name;
        private string _nameRus;

        [Column("id_atn")]
        public int IdAtn
        {
            get => _idAtn;
            set { if (_idAtn != value) { _idAtn = value; OnPropertyChanged(nameof(IdAtn)); } }
        }

        [Column("name")]
        public string Name
        {
            get => _name;
            set { if (_name != value) { _name = value; OnPropertyChanged(nameof(Name)); } }
        }

        [Column("name_rus")]
        public string NameRus
        {
            get => _nameRus;
            set { if (_nameRus != value) { _nameRus = value; OnPropertyChanged(nameof(NameRus)); } }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }

    public class AllColumnNameModel : INotifyPropertyChanged
    {
        private int _idAcn;
        private int _idAtn;
        private int _ordinalPosition;
        private string _name;
        private string _nameRus;
        private string _dataType;
        private int _readonly;

        [Column("id_acn")]
        public int IdAcn
        {
            get => _idAcn;
            set { if (_idAcn != value) { _idAcn = value; OnPropertyChanged(nameof(IdAcn)); } }
        }

        [Column("id_atn")]
        public int IdAtn
        {
            get => _idAtn;
            set { if (_idAtn != value) { _idAtn = value; OnPropertyChanged(nameof(IdAtn)); } }
        }

        [Column("ordinal_position")]
        public int OrdinalPosition
        {
            get => _ordinalPosition;
            set { if (_ordinalPosition != value) { _ordinalPosition = value; OnPropertyChanged(nameof(OrdinalPosition)); } }
        }

        [Column("name")]
        public string Name
        {
            get => _name;
            set { if (_name != value) { _name = value; OnPropertyChanged(nameof(Name)); } }
        }

        [Column("name_rus")]
        public string NameRus
        {
            get => _nameRus;
            set { if (_nameRus != value) { _nameRus = value; OnPropertyChanged(nameof(NameRus)); } }
        }

        [Column("data_type")]
        public string DataType
        {
            get => _dataType;
            set { if (_dataType != value) { _dataType = value; OnPropertyChanged(nameof(DataType)); } }
        }

        [Column("readonly")]
        public int Readonly
        {
            get => _readonly;
            set { if (_readonly != value) { _readonly = value; OnPropertyChanged(nameof(Readonly)); } }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }

    public class AllTableNameDataService
    {
        private readonly DbService _dbService;

        public AllTableNameDataService(DbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<List<AllTableNameModel>> GetListTable()
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
    }

    public class AllColumnNameDataService
    {
        private readonly DbService _dbService;

        public AllColumnNameDataService(DbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<List<AllColumnNameModel>> GetListColumnFromTable(int idAtn)
        {
            string query = @"
                SELECT id_acn, id_atn, ordinal_position, name, name_rus, data_type, readonly
                FROM all_column_name
                WHERE id_atn = @IdAtn
                ORDER BY ordinal_position";
            return await _dbService.GetListAsync<AllColumnNameModel>(query, new { IdAtn = idAtn });
        }

        public async Task<int> SaveAsync(AllColumnNameModel column)
        {
            return await _dbService.SaveEntityAsync("all_column_name", "id_acn", column);
        }

        public async Task DeleteAsync(AllColumnNameModel column)
        {
            await _dbService.DeleteEntityAsync("all_column_name", "id_acn", column);
        }
    }
}

