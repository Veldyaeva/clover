using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.UserDistribution.Models
{
    public class AllColumnNameDataService
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelperSQL _dbHelper;

        public AllColumnNameDataService(DbService dbService, DatabaseHelperSQL dbHelper)
        {
            _dbService = dbService;
            _dbHelper = dbHelper;
        }

        public async Task<List<AllColumnNameModel>> GetListColumnFromTable(int idAtn)
        {
            string query = @"
                SELECT id_acn, id_atn, ordinal_position, name, name_rus, data_type, Readonly, default_value
                FROM all_column_name
                WHERE id_atn = @IdAtn
                ORDER BY ordinal_position";
            return await _dbService.GetListAsync<AllColumnNameModel>(query, new { IdAtn = idAtn });
        }

        public async Task<List<AllColumnNameModel>> GetListColumnWithModeFromTable(int roleId, int objectId, int tableId)
        {
            string query = @"
                SELECT 
                    c.id_acn, c.id_atn, c.ordinal_position, c.name, c.name_rus, c.data_type, c.readonly, c.default_value,
                    ISNULL(rc.ModeID, 0) AS ModeID,
                    ISNULL(m.ModeName, 'Нет доступа') AS ModeName
                FROM all_column_name c
                LEFT JOIN RoleColumn rc ON rc.ColumnID = c.id_acn 
                    AND rc.RoleID = @RoleID AND rc.ObjectID = @ObjectID
                LEFT JOIN Mode m ON rc.ModeID = m.ModeID
                WHERE c.id_atn = @TableID
                ORDER BY c.ordinal_position"
            ;

            return await _dbService.GetListAsync<AllColumnNameModel>(query, new { RoleID = roleId, ObjectID = objectId, TableID = tableId });
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
                    cols.ORDINAL_POSITION,
                    cols.COLUMN_NAME,
                    cols.COLUMN_NAME,
                    cols.DATA_TYPE,
                    CASE 
                        WHEN tc.CONSTRAINT_TYPE = 'PRIMARY KEY' THEN 1 
                        ELSE 0 
                    END AS readonly
                FROM INFORMATION_SCHEMA.COLUMNS AS cols
                LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS kcu 
                    ON cols.TABLE_NAME = kcu.TABLE_NAME 
                    AND cols.COLUMN_NAME = kcu.COLUMN_NAME 
                    AND cols.TABLE_SCHEMA = kcu.TABLE_SCHEMA
                LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS tc 
                    ON kcu.CONSTRAINT_NAME = tc.CONSTRAINT_NAME 
                    AND tc.CONSTRAINT_TYPE = 'PRIMARY KEY'
                    AND tc.TABLE_NAME = cols.TABLE_NAME
                    AND tc.TABLE_SCHEMA = cols.TABLE_SCHEMA
                WHERE cols.TABLE_NAME = @tableName
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
        public async Task SaveRoleColumnAccessAsync(int roleId, int objectId, int columnId, int modeId)
        {
            // Удалим старую запись
            string deleteQuery = @"
                DELETE FROM RoleColumn 
                WHERE RoleID = @RoleID AND ColumnID = @ColumnID AND ObjectID = @ObjectID";

            await _dbHelper.ExecuteQueryAsync(deleteQuery, new Dictionary<string, object>
            {
                { "@RoleID", roleId },
                { "@ColumnID", columnId },
                { "@ObjectID", objectId }
            });

            // Вставим новую, если задан режим > 0
            if (modeId > 0)
            {
                string insertQuery = @"
                INSERT INTO RoleColumn (RoleID, ColumnID, ObjectID, ModeID)
                VALUES (@RoleID, @ColumnID, @ObjectID, @ModeID)";

                await _dbHelper.ExecuteQueryAsync(insertQuery, new Dictionary<string, object>
                {
                    { "@RoleID", roleId },
                    { "@ColumnID", columnId },
                    { "@ObjectID", objectId },
                    { "@ModeID", modeId }
                });
            }
        }
        public async Task<List<AllColumnNameModel>> GetColumnsWithAccessAsync(List<int> roleIds, string objectName, int formId, int tableId = 0)
        {
            // Преобразуем список ролей в SQL IN (...) строку
            string roleIdList = string.Join(",", roleIds);

            string query = $@"
                SELECT 
                    c.id_acn,
                    c.id_atn,
                    c.ordinal_position,
                    c.name,
                    c.name_rus,
                    c.data_type,
                    c.readonly,
                    c.default_value,
                    ISNULL(MAX(rc.ModeID), 0) AS ModeID,
                    ISNULL(m.ModeName, 'Нет доступа') AS ModeName
                FROM all_column_name c
                LEFT JOIN RoleColumn rc ON rc.ColumnID = c.id_acn 
                    AND rc.ObjectID = (
                        SELECT ObjectID 
                        FROM ObjectForm 
                        WHERE ObjectName = @ObjectName AND FormID = @FormID
                    )
                    AND rc.RoleID IN ({roleIdList})
                LEFT JOIN Mode m ON m.ModeID = ISNULL(rc.ModeID, 0)";
            if (tableId > 0)
                query += " WHERE c.id_atn = @TableID";
            query += $@" GROUP BY 
                    c.id_acn, c.id_atn, c.ordinal_position, c.name, c.name_rus, c.data_type, c.readonly, c.default_value, m.ModeName
                ORDER BY c.ordinal_position;";

            return await _dbService.GetListAsync<AllColumnNameModel>(query, new
            {
                ObjectName = objectName,
                FormID = formId,
                TableID = tableId
            });
        }

        public async Task InsertButtonForSprav(int id_atn)
        {
            string query = @"
            INSERT INTO dbo.all_column_name (id_atn, ordinal_position, name, name_rus, data_type, readonly)
            Values (   @id_atn,    0,    'simpleButtonAdd',    'кнопка добавить запись',    'button',    0),
                  (    @id_atn,    0,    'simpleButtonDel',    'кнопка удалить запись',    'button',    0),
                  (    @id_atn,    0,    'simpleButtonRed',    'кнопка редактировать запись',    'button',    0)";

            await _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object>
            {
                { "@id_atn", id_atn }
            });
        }

    }
}

