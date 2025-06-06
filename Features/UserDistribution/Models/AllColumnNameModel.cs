using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraLayout.Customization;
using SewingProduction.Helpers;
using SewingProduction.Services;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace SewingProduction.Features.UserDistribution.Models
{

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
        public int id_acn
        {
            get => _idAcn;
            set { if (_idAcn != value) { _idAcn = value; OnPropertyChanged(nameof(id_acn)); } }
        }

        [Column("id_atn")]
        public int id_atn
        {
            get => _idAtn;
            set { if (_idAtn != value) { _idAtn = value; OnPropertyChanged(nameof(id_atn)); } }
        }

        [Column("ordinal_position")]
        public int ordinal_position
        {
            get => _ordinalPosition;
            set { if (_ordinalPosition != value) { _ordinalPosition = value; OnPropertyChanged(nameof(ordinal_position)); } }
        }

        [Column("name")]
        public string name
        {
            get => _name;
            set { if (_name != value) { _name = value; OnPropertyChanged(nameof(name)); } }
        }

        [Column("name_rus")]
        public string name_rus
        {
            get => _nameRus;
            set { if (_nameRus != value) { _nameRus = value; OnPropertyChanged(nameof(name_rus)); } }
        }

        [Column("data_type")]
        public string data_type
        {
            get => _dataType;
            set { if (_dataType != value) { _dataType = value; OnPropertyChanged(nameof(data_type)); } }
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

    public class AllColumnNameDataService
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelper _dbHelper;

        public AllColumnNameDataService(DbService dbService, DatabaseHelper dbHelper)
        {
            _dbService = dbService;
            _dbHelper = dbHelper;
        }

        public async Task<List<AllColumnNameModel>> GetListColumnFromTable(int idAtn)
        {
            string query = @"
                SELECT id_acn, id_atn, ordinal_position, name, name_rus, data_type, Readonly
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
        public async Task InsertColumnsFromInformationSchema(string tableName, int id_atn)
        {
            string query = @"
                DECLARE @Y NVARCHAR(200) = @tableName
                DECLARE @X INT = 1;
                WHILE @X <= (SELECT MAX(ORDINAL_POSITION) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @Y)
                BEGIN
                    INSERT INTO dbo.all_column_name (id_atn, ordinal_position, name, name_rus, data_type, readonly)
                    SELECT 
                        @id_atn,
                        ORDINAL_POSITION,
                        COLUMN_NAME,
                        COLUMN_NAME,
                        DATA_TYPE,
                        0
                    FROM INFORMATION_SCHEMA.COLUMNS 
                    WHERE TABLE_NAME = @Y AND ORDINAL_POSITION = @X;

                    SET @X = @X + 1;
                END";

            await _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object>
            {
                { "@tableName", tableName },
                { "@id_atn", id_atn }
            });
        }
        public async Task InsertOnlyNewColumnsFromInformationSchema(string tableName, int id_atn)
        {
            string query = @"
            INSERT INTO dbo.all_column_name (id_atn, ordinal_position, name, name_rus, data_type, readonly)
            SELECT 
                @id_atn,
                ORDINAL_POSITION,
                COLUMN_NAME,
                COLUMN_NAME,
                DATA_TYPE,
                0
            FROM INFORMATION_SCHEMA.COLUMNS AS cols
            WHERE TABLE_NAME = @tableName
            AND NOT EXISTS (
                SELECT 1 FROM all_column_name AS acn
                WHERE acn.id_atn = @id_atn AND acn.name = cols.COLUMN_NAME
            )";

            await _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object>
            {
                { "@tableName", tableName },
                { "@id_atn", id_atn }
            });
        }

    }
}

