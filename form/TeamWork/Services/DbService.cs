using SewingProduction.Helpers;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Z.Dapper;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using Dapper;
using DevExpress.Mvvm.Native;
using System.Reflection;

namespace SewingProduction.Services
{
    /// <summary>
/// Сервис работы с базой данных для таблиц art_norm, norm_rasz, norm_rask, norm_kont и доп.обработки.
/// Использует Dapper для ускоренного доступа к данным.
/// </summary>
 public class DbService
    {
        private readonly DatabaseHelper _dbHelper;
        //    private readonly HybridLogger _logger = new HybridLogger();
        private readonly FileLogger _logger = new FileLogger();

        /// <summary>
        /// Инициализирует новый экземпляр dbService.
        /// </summary>
        /// <param name="dbHelper">Помощник для работы с базой данных.</param>
        public DbService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));

        }

        #region        // === УНИВЕРСАЛЬНЫЕ МЕТОДЫ ===
        /// <summary>
        /// Получает объект из БД
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<T> GetEntityAsync<T>(string query, object parameters)
        {
            using (var connection = _dbHelper.GetConnection())
                return await connection.QueryFirstOrDefaultAsync<T>(query, parameters);
        }
        /// <summary>
        /// Универсально обновляет одно поле в таблице по заданному условию.
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="fieldName">Имя обновляемого поля</param>
        /// <param name="newValue">Новое значение</param>
        /// <param name="whereField">Поле условия (например, "AnnId")</param>
        /// <param name="whereValue">Значение условия</param>
        public async Task UpdateFieldAsync(string tableName, string fieldName, object newValue, string whereField, object whereValue)
        {
            try
            {
                string query = $@"UPDATE {tableName}
                        SET {fieldName} = @NewValue
                        WHERE {whereField} = @WhereValue";

                var parameters = new Dictionary<string, object>
                        {
                            { "@NewValue", newValue ?? DBNull.Value },
                            { "@WhereValue", whereValue ?? DBNull.Value }
                        };

                await _dbHelper.ExecuteNonQueryAsync(query, parameters);

                await _logger.LogEventAsync(
                    $"Таблица {tableName}: поле {fieldName} обновлено на {newValue}, где {whereField} = {whereValue}.",
                    "UpdateFieldAsync");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при обновлении {fieldName} в таблице {tableName} по условию {whereField} = {whereValue}");
                throw;
            }
        }
        /// <summary>
        /// универсально вставляет данные в таблицу
        /// </summary>
        /// <typeparam name="T">тип объекта (из модели) для вставки</typeparam>
        /// <param name="tableName">имя таблицы</param>
        /// <param name="keyFieldName">имя ключевого параметра</param>
        /// <param name="entity">объект для вставки</param>
        /// <returns></returns>
        public async Task<int> InsertEntityAsync<T>(string tableName, string keyFieldName, T entity)
        {
            try
            {
                await _logger.LogEventAsync($"Начало вставки в таблицу {tableName}", "InsertEntityAsync");

                var properties = typeof(T).GetProperties()
                    .Where(p => p.CanRead &&
                                p.Name != keyFieldName &&
                                !System.Attribute.IsDefined(p, typeof(NotMappedAttribute)))
                    .ToList();

                await _logger.LogEventAsync($"Найдено {properties.Count} свойств для вставки", "InsertEntityAsync");

                var columns = new List<string>();
                var values = new List<string>();
                var parameters = new Dictionary<string, object>();

                foreach (var prop in properties)
                {
                    string columnName = prop.Name;
                    var columnAttr = prop.GetCustomAttributes(typeof(ColumnAttribute), false)
                     .FirstOrDefault() as ColumnAttribute;
                    if (columnAttr != null)
                    {
                        columnName = columnAttr.Name;
                    }
                    string parameterName = "@" + columnName;

                    var value = prop.GetValue(entity);
                    await _logger.LogEventAsync($"Свойство {prop.Name} (колонка {columnName}): значение = {value}, тип = {value?.GetType()}", "InsertEntityAsync");

                    columns.Add(columnName);
                    values.Add(parameterName);
                    parameters[parameterName] = NormalizeValue(value);
                }

                string columnsPart = string.Join(", ", columns);
                string valuesPart = string.Join(", ", values);

                string query = $"INSERT INTO {tableName} ({columnsPart}) VALUES ({valuesPart}); SELECT SCOPE_IDENTITY();";

                await _logger.LogEventAsync($"SQL Query: {query}", "InsertEntityAsync");
                foreach (var param in parameters)
                {
                    await _logger.LogEventAsync($"Parameter {param.Key}: {param.Value} (тип: {param.Value?.GetType()})", "InsertEntityAsync");
                }

                object result = await _dbHelper.ExecuteScalarAsync(query, parameters);

                if (result != null && int.TryParse(result.ToString(), out int newId))
                {
                    await _logger.LogEventAsync($"Таблица {tableName}: новая запись ID={newId} успешно добавлена", "InsertEntity");
                    return newId;
                }
                else
                {
                    throw new Exception("Ошибка получения нового ID после вставки.");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при добавлении записи в таблицу {tableName}");
                throw;
            }
        }

       /// <summary>
       /// Универсальное обновление данных в таблице
       /// </summary>
       /// <typeparam name="T">тип объекта</typeparam>
       /// <param name="tableName">имя таблицы</param>
       /// <param name="keyFieldName">имя ключевого параметра</param>
       /// <param name="entity">объект обновления</param>
       /// <returns></returns>
        public async Task UpdateEntityAsync<T>(string tableName, string keyFieldName, T entity)
        {
            try
            {
                var properties = typeof(T).GetProperties()
                    .Where(p => p.CanRead &&
                                p.Name != keyFieldName &&
                                !System.Attribute.IsDefined(p, typeof(NotMappedAttribute)))
                    .ToList();

                var setClauses = new List<string>();
                var parameters = new Dictionary<string, object>();

                foreach (var prop in properties)
                {
                    string columnName = prop.Name;
                    var columnAttr = prop.GetCustomAttributes(typeof(ColumnAttribute), false)
                     .FirstOrDefault() as ColumnAttribute;
                    if (columnAttr != null)
                    {
                        columnName = columnAttr.Name;
                    }
                    string parameterName = "@" + columnName;
                    setClauses.Add($"{columnName} = {parameterName}");
                    parameters[parameterName] = NormalizeValue(prop.GetValue(entity));
                }

                // Ключевое поле
                var keyProperty = typeof(T).GetProperty(keyFieldName);
                if (keyProperty == null)
                    throw new Exception($"Ключевое поле {keyFieldName} не найдено в объекте {typeof(T).Name}");

                parameters["@Id"] = keyProperty.GetValue(entity);

                string setClause = string.Join(", ", setClauses);
                string query = $"UPDATE {tableName} SET {setClause} WHERE {keyFieldName} = @Id";

                await _dbHelper.ExecuteNonQueryAsync(query, parameters);
                await _logger.LogEventAsync($"Таблица {tableName}: запись ID={parameters["@Id"]} успешно обновлена", "UpdateEntity");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при обновлении записи в таблице {tableName}");
                throw;
            }
        }
        /// <summary>
        /// универсальный метод для обновления данных в таблице, если записи не существует, данные вставляются, если существует, обновляются
        /// </summary>
        /// <typeparam name="T">тип данных</typeparam>
        /// <param name="tableName">имя таблицы</param>
        /// <param name="keyFieldName">имя ключевого поля</param>
        /// <param name="entity">данные для обновления</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<int> SaveEntityAsync<T>(string tableName, string keyFieldName, T entity)
        {
            var keyProperty = typeof(T).GetProperty(keyFieldName);
            if (keyProperty == null)
                throw new Exception($"Ключевое поле {keyFieldName} не найдено в объекте {typeof(T).Name}");

            var keyValue = keyProperty.GetValue(entity);

            if (keyValue is int id && id > 0)
            {
                await UpdateEntityAsync(tableName, keyFieldName, entity);
                return id;
            }
            else
            {
                return await InsertEntityAsync(tableName, keyFieldName, entity);
            }
        }

        /// <summary>
        /// Удаляет сущность из указанной таблицы по идентификатору.
        /// </summary>
        /// <typeparam name="T">Тип сущности.</typeparam>
        /// <param name="tableName">Имя таблицы.</param>
        /// <param name="keyFieldName">Имя поля идентификатора.</param>
        /// <param name="entity">Объект для удаления.</param>
        public async Task DeleteEntityAsync<T>(string tableName, string keyFieldName, T entity)
        {
            try
            {
                var keyProperty = typeof(T).GetProperty(keyFieldName);
                if (keyProperty == null)
                    throw new Exception($"Ключевое поле {keyFieldName} не найдено в объекте {typeof(T).Name}");

                object keyValue = keyProperty.GetValue(entity);
                if (keyValue == null)
                    throw new Exception($"Значение ключевого поля {keyFieldName} не установлено в объекте {typeof(T).Name}");

                string query = $"DELETE FROM {tableName} WHERE {keyFieldName} = @Id";

                var parameters = new Dictionary<string, object>
                {
                    { "@Id", keyValue }
                };

                await _dbHelper.ExecuteNonQueryAsync(query, parameters);
                await _logger.LogEventAsync($"Таблица {tableName}: запись ID={keyValue} успешно удалена", "DeleteEntity");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при удалении записи в таблице {tableName}");
                throw;
            }
        }
        /// <summary>
        /// Нормализует значение, преобразуя null в 0 для чисел и пустую строку для текстов.
        /// </summary>
        /// <param name="value">Значение для нормализации.</param>
        /// <returns>Корректное значение без null.</returns>
        private object NormalizeValue(object value)
        {
            if (value == null)
                return DBNull.Value;

            if (value is DateTime dt)
            {
                if (dt == DateTime.MinValue)
                    return DBNull.Value;
                return dt;
            }

            if (value is bool boolValue)
                return boolValue ? 1 : 0;

            return value;
        }

        //public async Task<int> InsertEntityAsync<T>(string tableName, string keyFieldName, T entity)
        //{
        //    try
        //    {
        //        var properties = typeof(T).GetProperties();
        //        var columns = new List<string>();
        //        var parameters = new List<string>();
        //        var values = new Dictionary<string, object>();

        //        foreach (var prop in properties)
        //        {
        //            columns.Add(prop.Name);
        //            parameters.Add("@" + prop.Name);
        //            values.Add("@" + prop.Name, prop.GetValue(entity));
        //        }

        //        string query = $@"
        //        INSERT INTO {tableName} ({string.Join(",", columns)})
        //        OUTPUT INSERTED.{keyFieldName}
        //        VALUES ({string.Join(",", parameters)})";

        //        object result = await _dbHelper.ExecuteScalarAsync(query, values);
        //        await _logger.LogEventAsync($"Добавлена запись в таблицу {tableName}", "InsertEntity");
        //        return result != null ? Convert.ToInt32(result) : -1;
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка при вставке записи в {tableName}");
        //        throw;
        //    }
        //}
        //private object NormalizeValue(object value)
        //{
        //    if (value == null)
        //        return DBNull.Value;
        //    if (value is DateTime dt)
        //    {
        //        if (dt == DateTime.MinValue)
        //            return DBNull.Value;
        //        return dt;
        //    }
        //    if (value is bool boolValue)
        //        return boolValue ? 1 : 0;
        //    return value;
        //}
        //public async Task<int> SaveEntityAsync<T>(string tableName, string keyFieldName, T entity)
        //{
        //    var keyProperty = typeof(T).GetProperty(keyFieldName);
        //    if (keyProperty == null)
        //        throw new Exception($"Ключевое поле {keyFieldName} не найдено в объекте {typeof(T).Name}");

        //    var keyValue = keyProperty.GetValue(entity);

        //    if (keyValue is int id && id > 0)
        //    {
        //        await UpdateEntityAsync(tableName, keyFieldName, entity);
        //        return id;
        //    }
        //    else
        //    {
        //        return await InsertEntityAsync(tableName, keyFieldName, entity);
        //    }
        //}


        //public async Task UpdateEntityAsync<T>(string tableName, string keyFieldName, T entity)
        //{
        //    try
        //    {
        //        var keyProperty = typeof(T).GetProperty(keyFieldName);
        //        if (keyProperty == null)
        //            throw new Exception($"Ключевое поле {keyFieldName} не найдено");

        //        object keyValue = keyProperty.GetValue(entity);
        //        if (keyValue == null)
        //            throw new Exception($"Ключевое поле {keyFieldName} не заполнено");

        //        var properties = typeof(T).GetProperties()
        //                    .Where(p => p.GetCustomAttributes(typeof(NotMappedAttribute), true).Length == 0) 
        //                    .ToArray(); 
        //        var setClauses = new List<string>();
        //        var values = new Dictionary<string, object>();

        //        foreach (var prop in properties)
        //        {
        //            if (prop.Name != keyFieldName)
        //            {
        //                setClauses.Add($"{prop.Name} = @{prop.Name}");
        //                values.Add("@" + prop.Name, prop.GetValue(entity));
        //            }
        //        }

        //        values.Add("@Id", keyValue);

        //        string query = $@"
        //        UPDATE {tableName}
        //        SET {string.Join(", ", setClauses)}
        //        WHERE {keyFieldName} = @Id";

        //        await _dbHelper.ExecuteNonQueryAsync(query, values);
        //        await _logger.LogEventAsync($"Обновлена запись в таблице {tableName} (ID={keyValue})", "UpdateEntity");
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка при обновлении записи в {tableName}");
        //        throw;
        //    }
        //}

        //public async Task DeleteEntityAsync<T>(string tableName, string keyFieldName, T entity)
        //{
        //    try
        //    {
        //        var keyProperty = typeof(T).GetProperty(keyFieldName);
        //        if (keyProperty == null)
        //            throw new Exception($"Ключевое поле {keyFieldName} не найдено");

        //        object keyValue = keyProperty.GetValue(entity);
        //        if (keyValue == null)
        //            throw new Exception($"Ключевое поле {keyFieldName} не заполнено");

        //        string query = $"DELETE FROM {tableName} WHERE {keyFieldName} = @Id";

        //        var parameters = new Dictionary<string, object> { { "@Id", keyValue } };

        //        await _dbHelper.ExecuteNonQueryAsync(query, parameters);
        //        await _logger.LogEventAsync($"Удалена запись из таблицы {tableName} (ID={keyValue})", "DeleteEntity");
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка при удалении записи из {tableName}");
        //        throw;
        //    }
        //}

        //public async Task UpdateFieldAsync(string tableName, string fieldName, object fieldValue, string keyFieldName, object keyValue)
        //{
        //    try
        //    {

        //        string query = $@"
        //        UPDATE {tableName}
        //        SET {fieldName} = @FieldValue
        //        WHERE {keyFieldName} = @KeyValue";

        //        var parameters = new Dictionary<string, object>
        //    {
        //        { "@FieldValue", fieldValue },
        //        { "@KeyValue", keyValue }
        //    };

        //        await _dbHelper.ExecuteNonQueryAsync(query, parameters);
        //        await _logger.LogEventAsync($"Поле {fieldName} таблицы {tableName} обновлено (ID={keyValue})", "UpdateField");
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка при обновлении поля {fieldName} в таблице {tableName}");
        //        throw;
        //    }
        //}


        #endregion

        #region мои методы
         public async Task DeleteByAnnId(string tableName, int annId)
        {
            try
            {
                string query = $"DELETE FROM {tableName} WHERE AnnId = @AnnId";
                var parameters = new Dictionary<string, object> { { "@AnnId", annId } };
                await _dbHelper.ExecuteNonQueryAsync(query, parameters);
                await _logger.LogEventAsync($"Удалены записи из {tableName} по AnnId={annId}", "DeleteByAnnId");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при удалении записей по AnnId={annId} из таблицы {tableName}");
                throw;
            }
        }
        public async Task<List<ArtNormN>> GetArtNormData()
        {
            // --- Explicit Mapping for ArtNormN ---
            var map = new CustomPropertyTypeMap(
               typeof(ArtNormN),
               (type, columnName) =>
               {
                   // Standard properties matching column names (case-insensitive)
                   var prop = type.GetProperties().FirstOrDefault(p => p.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));
                   if (prop != null && !Attribute.IsDefined(prop, typeof(NotMappedAttribute))) return prop;

                   // Explicit mapping for properties with different names or needing specific handling
                   if (columnName.Equals("grup", StringComparison.OrdinalIgnoreCase)) return type.GetProperty(nameof(ArtNormN.Group));
                   if (columnName.Equals("sek_shv", StringComparison.OrdinalIgnoreCase)) return type.GetProperty(nameof(ArtNormN.SekShv));
                   if (columnName.Equals("sek_vyaz5", StringComparison.OrdinalIgnoreCase)) return type.GetProperty(nameof(ArtNormN.SekVyaz5));
                   if (columnName.Equals("sek_vyaz6", StringComparison.OrdinalIgnoreCase)) return type.GetProperty(nameof(ArtNormN.SekVyaz6));
                   if (columnName.Equals("sek_vyaz7", StringComparison.OrdinalIgnoreCase)) return type.GetProperty(nameof(ArtNormN.SekVyaz7));
                   if (columnName.Equals("sek_vyaz10", StringComparison.OrdinalIgnoreCase)) return type.GetProperty(nameof(ArtNormN.SekVyaz10));
                   if (columnName.Equals("sek_vyaz12", StringComparison.OrdinalIgnoreCase)) return type.GetProperty(nameof(ArtNormN.SekVyaz12));
                   if (columnName.Equals("sek_vyazo", StringComparison.OrdinalIgnoreCase)) return type.GetProperty(nameof(ArtNormN.SekVyazo));
                   if (columnName.Equals("sek_vyaz", StringComparison.OrdinalIgnoreCase)) return type.GetProperty(nameof(ArtNormN.SekVyaz));
                   if (columnName.Equals("data_sozd", StringComparison.OrdinalIgnoreCase)) return type.GetProperty(nameof(ArtNormN.dateCreate));
                   if (columnName.Equals("data_obn", StringComparison.OrdinalIgnoreCase)) return type.GetProperty(nameof(ArtNormN.dateUpdate));
                   if (columnName.Equals("sek_kr", StringComparison.OrdinalIgnoreCase)) return type.GetProperty(nameof(ArtNormN.SekKr));
                   if (columnName.Equals("parentId", StringComparison.OrdinalIgnoreCase)) return type.GetProperty(nameof(ArtNormN.ParentId));
                   // Explicitly map statusText even though StatusText property is [NotMapped] (Dapper might populate it if mapped)
                   if (columnName.Equals("statusText", StringComparison.OrdinalIgnoreCase)) return type.GetProperty(nameof(ArtNormN.StatusText));
                   // Add other mappings if needed

                   // If no match found by name or explicit rule, ignore the column
                   return null;
               });

            SqlMapper.SetTypeMap(typeof(ArtNormN), map);
            // --- End Explicit Mapping ---

            string query = @"
            SELECT 
                SUBSTRING(kod,1,7) AS kod, annId, grup, articul, mod, sek, sek_vyaz, 
                data_obn, sek_shv, status_ann.name AS statusText, status, sek_vyazo, sek_vyaz5, 
                sek_vyaz7, sek_vyaz12, sek_vyaz10, sek_vyaz6, sek_kr, slogn, komment, 
                data_sozd, diz, constr 
            FROM ArtNormNView 
            JOIN status_ann ON status = status_id";
            using (var connecion = _dbHelper.GetConnection())
            {
                var res = await connecion.QueryAsync<ArtNormN>(query);
                return res.ToList();
            } 
            //finally
            //{
            //    // --- Reset Type Map ---
            //    // Important: Reset to default map to avoid affecting other queries/types
            //     SqlMapper.SetTypeMap(typeof(ArtNormN), null);
            //     // --- End Reset Type Map ---
            //}
        }

        public void UpdateAnnIdinArticul(int kod, int annId)
        {
            string query = "UPDATE sp_articul SET annId = @annId WHERE kod like @kod";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@kod", kod + "%" }, { "@annId", annId } });
        }

        /// <summary>
        /// Сбрасывает annId в таблице sp_articul для всех записей, связанных с указанным kodd_rt.
        /// </summary>
        /// <param name="kodd"></param>
        /// <returns></returns>
        public async Task ResetAnnIdinArticul(int kod)
        {
            string query = "UPDATE sp_articul SET annId = NULL WHERE left(kod,7) = @kod";
            await _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { { "@kod", kod } });
        }



        public async Task<int> GetPztRecordCountByAnnIdsAsync(List<int> annIdList)
        {
            var query = @"
        SELECT COUNT(pzt.idrecord)
        FROM plan_zagr_two pzt
        INNER JOIN norm_rasz nr ON pzt.pztNrID = nr.nrID
        WHERE (pzt.tab <> 0 and pzt.tab IS NOT NULL)
          AND nr.annId IN @AnnIdList";

            using (var connection = _dbHelper.GetConnection())
            {
                return await connection.ExecuteScalarAsync<int>(query, new { AnnIdList = annIdList });
            }
        }

        public async Task<DataTable> GetRaskroyNormGroups()
        {
            string query = @"
                        SELECT DISTINCT gr, naimen 
                        FROM raskroy_norm 
                        ORDER BY gr";

            return await _dbHelper.ExecuteQueryAsync(query);

        }
        public async Task<DataTable> GetRaskroyNormByGroup(int groupId)
        {
            string query = @"
                        SELECT * 
                        FROM raskroy_norm 
                        WHERE gr = @groupId
                        ORDER BY naimen";

            var parameters = new Dictionary<string, object>
                    {
                        { "@groupId", groupId }
                    };

            return await _dbHelper.ExecuteQueryAsync(query, parameters);
        }

        /// <summary>
        /// Получает запись ArtNormN по идентификатору
        /// </summary>
        /// <param name="annId">Идентификатор записи</param>
        /// <returns>Объект ArtNormN или null, если запись не найдена</returns>
        public async Task<ArtNormN> GetArtNormDataById(int annId)
        {
            try
            {
                string query = @"
                            SELECT 
                                SUBSTRING(kod,1,7) AS kod, annId, grup, articul, mod, sek, sek_vyaz, 
                                data_obn, sek_shv, status_ann.name AS statusText, status, sek_vyazo, sek_vyaz5, 
                                sek_vyaz7, sek_vyaz12, sek_vyaz10, sek_vyaz6, sek_kr, slogn, komment, 
                                data_sozd, diz, constr 
                            FROM ArtNormNView 
                            JOIN status_ann ON status = status_id
                            WHERE annId = @annId";
                return await GetEntityAsync<ArtNormN>(query, new { annId });
                //DataTable result = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@annId", annId } });

                //if (result != null && result.Rows.Count > 0)
                //{
                //    // Возвращаем первую запись
                //    List<ArtNormN> list = ConvertToList(result);
                //    if (list.Count > 0)
                //    {
                //        await _logger.LogEventAsync($"Успешно получены данные ArtNormN для ID {annId}", "GetArtNormDataById");
                //        return list[0];
                //    }
                //}

                //await _logger.LogEventAsync($"Не найдены данные ArtNormN для ID {annId}", "GetArtNormDataById");
                //return null;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных ArtNormN для ID {annId}");
                return null;
            }
        }

        internal async Task<DataTable> GetKod_proizv()
        {
            string query = "select kod_proizv, text_proizv from kod_proizv";
            return await _dbHelper.ExecuteQueryAsync(query);

        }

        internal async Task<DataTable> GetPodr_vyaz()
        {
            string query = "select kod_vyaz, text_vyaz from podr_vyaz";
            return await _dbHelper.ExecuteQueryAsync(query);
        }

        internal async Task<DataTable> GetOborud_shv()
        {
            string query = "select kod_ob, text_ob from oborud_shv";
            return await _dbHelper.ExecuteQueryAsync(query);
        }
        public async Task ExecutePztOperUpdateAsync()
        {
            const string sql = "EXEC dbo.pztOperUpdateFast";
            await _dbHelper.ExecuteNonQueryAsync(sql);
        }

        /// <summary>
        /// загрузка артикулов для увязки. Статус != архивное
        /// </summary>
        /// <returns>Возвращает таблицу артикулов</returns>
        public async Task<List<ArtNormN>> GetArtNormDataCurrent(int kod, bool all)
        //public async Task<DataTable> GetArtNormDataCurrent(int kod, bool all)
        {
            string query = "";
            if (all)
            {//"AnnId, Kod, Grup, Articul, Mod, Sek, Sek_vyaz, Data_obn, Sek_shv, Status_ann.name AS Stat, Status, Sek_vyazo, Sek_vyaz5, Sek_vyaz7, Sek_vyaz12, sek_vyaz10, sek_vyaz6, sek_kr, slogn, komment, data_sozd, diz, constr FROM ArtNormNView JOIN status_ann ON status=status_id WHERE status<3";
                query = @"SELECT  
                                        SUBSTRING(kod, 1, 7) AS kod, annId, grup, articul, mod, sek, sek_vyaz,
                    data_obn, sek_shv, status_ann.name AS statusText, status, sek_vyazo, sek_vyaz5, 
                    sek_vyaz7, sek_vyaz12, sek_vyaz10, sek_vyaz6, sek_kr, slogn, komment, 
                    data_sozd, diz, constr FROM ArtNormNView JOIN status_ann ON status=status_id WHERE status!=3";

            }
            else
            {
                query =// "SELECT annId, kod, grup, articul, mod, sek, sek_vyaz, data_obn, sek_shv, status_ann.name AS stat, status, sek_vyazo, sek_vyaz5, sek_vyaz7, sek_vyaz12, sek_vyaz10, sek_vyaz6, sek_kr, slogn, komment, data_sozd, diz, constr FROM artNormNView " +
@"SELECT                        
                                        SUBSTRING(kod, 1, 7) AS kod, annId, grup, articul, mod, sek, sek_vyaz,
                    data_obn, sek_shv, status_ann.name AS statusText, status, sek_vyazo, sek_vyaz5, 
                    sek_vyaz7, sek_vyaz12, sek_vyaz10, sek_vyaz6, sek_kr, slogn, komment, 
                    data_sozd, diz, constr FROM ArtNormNView
        JOIN status_ann ON status=status_id WHERE (status<3) AND (annId IN (SELECT annId FROM View_sp_articul WHERE kodd_rt = '@kod'))";
            }
            //DataTable result = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "kod", kod } });
            //return (List<ArtNormN>)result;
            //DataTable table = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object>);
            // return ConvertToList(result);

            //return (DataTable)result;
            return await _dbHelper.GetConnection().QueryAsync<ArtNormN>(query).ContinueWith(t => t.Result.ToList());
        }

        public async Task<List<ArtNormN>> GetArtNormDataCurrent(string art)
        {
            string query = $"SELECT * FROM artNormNView WHERE status<{(int)Status.Archive} AND articul IN (SELECT articul FROM View_sp_articul WHERE articul LIKE @art)";
            object result = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "art", art + "%" } });
            return result as List<ArtNormN>;
        }

        /// <summary>
        /// Получение пути к файлу изображения
        /// </summary>
        /// <param name="kod">код</param>
        /// <returns></returns>
        public async Task<DataTable> GetImage(int kod)
        {
            string query = "select dbo.getFileEskizForKodd(@kod) as pathpict ";
            DataTable result = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@kod", kod } });
            return result;
        }

        internal Task<DataTable> GetNormOper(int i)
        {
            string query = @"SELECT no.*, 
                             kp.text_proizv,
                             pv.text_vyaz,
                             ob.text_ob
                             FROM dbo.norm_oper no
                             LEFT JOIN kod_proizv kp ON no.kod_proizv = kp.kod_proizv
                             LEFT JOIN podr_vyaz pv ON no.kod_proizv = pv.kod_vyaz
                             LEFT JOIN oborud_shv ob ON no.kod_ob = ob.kod_ob";
            return _dbHelper.ExecuteQueryAsync(query);
        }

        public async Task<string> GetEmployeeFullName(int employeeId)
        {
            try
            {
                string query = "SELECT fio FROM fio WHERE tab = @employeeId";
                DataTable result = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@employeeId", employeeId } });

                if (result.Rows.Count > 0)
                {
                    return result.Rows[0]["fio"].ToString();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при получении ФИО сотрудника");
            }
            return string.Empty;
        }

        ///// <summary>
        ///// Получает список объектов NormRasz для указанного annId
        ///// </summary>
        ///// <param name="annId">Идентификатор разделения труда</param>
        ///// <returns>Список объектов NormRasz</returns>
        //public async Task<List<NormRasz>> GetNormRaszList(int annId)
        //{
        //    DataTable table = await GetRelatedNormRasz(annId);
        //    List<NormRasz> result = new List<NormRasz>();

        //    foreach (DataRow row in table.Rows)
        //    {
        //        NormRasz item = new NormRasz
        //        {
        //            AnnId = Convert.ToInt32(row["annId"]),
        //            N = row["n"] != DBNull.Value ? Convert.ToInt32(row["n"]) : 0,
        //            N1 = row["n1"] != DBNull.Value ? Convert.ToInt32(row["n1"]) : 0,
        //            Razryad = row["razryd"] != DBNull.Value ? Convert.ToInt32(row["razryd"]) : 0,
        //            Text = row["text"] != DBNull.Value ? row["text"].ToString() : string.Empty,
        //            Sek = row["sek"] != DBNull.Value ? Convert.ToInt32(row["sek"]) : 0,
        //            Kod = row["kod"] != DBNull.Value ? Convert.ToInt32(row["kod"]) : 0,
        //            Kod_o = row["kod_o"] != DBNull.Value ? Convert.ToInt32(row["kod_o"]) : 0,
        //            Kod_ob = row["kod_ob"] != DBNull.Value ? Convert.ToInt32(row["kod_ob"]) : 0
        //        };
        //        result.Add(item);
        //    }

        //    return result;
        //}

        #endregion

        #region работа со связанными данными
        // Получение связанных данных
        /// <summary>
        /// Получает данные из таблицы Norm_rasz (dataTable) 
        /// </summary>
        /// <param name="annId">идентификатор РТ</param>
        /// <returns></returns>
        public async Task<List<NormRasz>> GetRelatedNormRasz(int annId)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "SELECT AnnId, N, N1, Razryd as Rasryad, Text, Sek, Kod, kod_o as KodO, kod_ob as KodOb, kod_podr as KodPodr, kod_proizv as KodProizv, Seb, Spec, Obor, nrId FROM norm_rasz WHERE annId = @annId";
                //string query = "SELECT AnnId, N, N1, Razryd, Text, Sek, Kod, kod_o, kod_ob, nrId FROM normRaszView WHERE annId = @annId";
                var result = await connection.QueryAsync<NormRasz>(query, new Dictionary<string, object> { { "@annId", annId } });
                return result.ToList();
            }
        }
        public DataTable GetRelatedNormRasz1(int annId)
        {
            //string query = "SELECT AnnId, N, N1, Razryd as Rasryad, Text, Sek, Kod, kod_o as KodO, kod_ob as KodOb, nrId FROM norm_rasz WHERE annId = @annId";
            string query = "SELECT AnnId, N, N1, Razryd, Text, Sek, Kod, kod_o, kod_ob, nrId FROM normRaszView WHERE annId = @annId";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } });
            //.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@annId", annId } });
        }

        public async Task<List<NormRask>> GetRelatedNormRask(int annId)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "SELECT id, AnnId, kod_o as KodO, Text, razryd as Razryad, Sek, Kod, Seb, N, n_ch as NCh, N1, seb_s as SebS, Obor FROM norm_rask WHERE annId = @annId";
                var result = await connection.QueryAsync<NormRask>(query, new Dictionary<string, object> { { "@annId", annId } });
                return result.ToList();
            }
        }

        public async Task<List<NormKont>> GetRelatedNormKont(int annId)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "SELECT AnnId, kod_o as KodO, Text, razryd as Razryad, Sek FROM norm_kont WHERE annId = @annId";
                //return _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@annId", annId } });
                var result = await connection.QueryAsync<NormKont>(query, new Dictionary<string, object> { { "@annId", annId } });
                return result.ToList();
            }
        }

        public async Task<List<NormDopObr>> GetRelatedNormDopObr(int annId)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "SELECT AnnId, sek_p as SekP, sek_p_tamp as SekTamp, sek_v as SekV, sek_stra as SekStra FROM norm_dop_obr WHERE annId = @annId";
                // return //_dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@annId", annId } });
                var result = await connection.QueryAsync<NormDopObr>(query, new Dictionary<string, object> { { "@annId", annId } }); 
                return result.ToList();
            }
        }
        /// <summary>
        /// Получение неувязанных артикулов из sp_articul
        /// </summary>
        /// <returns></returns>
        public async Task<DataTable> GetRelatedSpArt()
        {
            string query = "SELECT DISTINCT SUBSTRING(kod,1,7) as kod, grup, articul, mod, annId FROM sp_articul WHERE annID IS NULL";

            object result = await _dbHelper.ExecuteQueryAsync(query);
            return (DataTable)result;
        }
        /// <summary>
        /// Получает список NZP и количество назначенных операций по AnnId
        /// </summary>
        /// <param name="annId">Идентификатор изделия (AnnId)</param>
        /// <returns>Список записей из таблицы plan_zagr_two</returns>
        public async Task<List<NZPByKoddRt>> GetNZPByKoddRtAsync(int annId)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var results = await connection.QueryAsync<NZPByKoddRt>(
                    "dbo.GetNZPAndOperByKoddRT",
                    new { xAnnID = annId },
                    commandType: CommandType.StoredProcedure);
                return results.ToList();
            }
        }
        public async Task<List<NZPByKoddRt>> GetNzpWithPztCounts(int annId)
        {
            var nzpList = (await _dbHelper.GetConnection()
                .QueryAsync<NZPByKoddRt>("EXEC dbo.GetNZPByKoddRT @xAnnID", new { xAnnID = annId }))
                .ToList();

            var pztCounts = (await _dbHelper.GetConnection()
                .QueryAsync<(string kod, int PztCount)>("EXEC dbo.GetPztCountsByKoddRT @xAnnID", new { xAnnID = annId }))
                .ToDictionary(x => x.kod, x => x.PztCount);

            // Объединение
            foreach (var row in nzpList)
            {
                if (pztCounts.TryGetValue(row.kodd.ToString(), out int count))
                    row.PZTCount = count;
            }

            return nzpList;
        }

        public Task<DataTable> GetRelDesigner()
        {

            string query = "SELECT fio, tab FROM fio";
            return _dbHelper.ExecuteQueryAsync(query);
        }


        public async Task DeleteRelatedNormTables(int annId)
        {
            await DeleteByAnnId("norm_rasz", annId);
            await DeleteByAnnId("norm_rask", annId);
            await DeleteByAnnId("norm_kont", annId);
            await DeleteByAnnId("norm_dop_obr", annId);
        }
        #endregion


        #region convertorToModels

        private List<ArtNormN> ConvertToList(DataTable table)
        {
            List<ArtNormN> list = new List<ArtNormN>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new ArtNormN
                {
                    Kod = row["kod"].ToString(),
                    AnnID = Convert.ToInt32(row["annId"]),
                    Group = row["grup"].ToString(),
                    Articul = row["articul"].ToString(),
                    Mod = row["mod"].ToString(),
                    Sek = row["sek"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek"]),
                    SekVyaz = row["sek_vyaz"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_vyaz"]),
                    dateUpdate = row["data_obn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["data_obn"]),
                    SekShv = row["sek_shv"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_shv"]),
                    StatusText = row["statusText"] == DBNull.Value ? "" : row["statusText"].ToString(),
                    Status = row["status"] == DBNull.Value ? 0 : Convert.ToInt32(row["status"]),
                    preArch = ((int)row["status"] == (int)Status.PreliminaryArchive) ? true : false,
                    SekVyazo = row["sek_vyazo"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_vyazo"]),
                    SekVyaz5 = row["sek_vyaz5"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_vyaz5"]),
                    SekVyaz7 = row["sek_vyaz7"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_vyaz7"]),
                    SekVyaz12 = row["sek_vyaz12"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_vyaz12"]),
                    SekVyaz10 = row["sek_vyaz10"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_vyaz10"]),
                    SekVyaz6 = row["sek_vyaz6"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_vyaz6"]),
                    SekKr = row["sek_kr"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_kr"]),
                    Slogn = row["slogn"] == DBNull.Value ? 0 : Convert.ToInt32(row["slogn"]),
                    Komment = row["komment"] == DBNull.Value ? "" : row["komment"].ToString(),
                    dateCreate = row["data_sozd"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["data_sozd"]),
                    Diz = row["diz"] == DBNull.Value ? 0 : Convert.ToInt32(row["diz"]),
                    Constr = row["constr"] == DBNull.Value ? 0 : Convert.ToInt32(row["constr"])
                });
            }
            return list;
        }

        #endregion

        public static T MapDataRowToObject<T>(DataRow row) where T : new()
        {
            T obj = new T();

            foreach (var property in typeof(T).GetProperties())
            {
                if (row.Table.Columns.Contains(property.Name) && row[property.Name] != DBNull.Value)
                {
                    try
                    {
                        var value = Convert.ChangeType(row[property.Name], property.PropertyType);
                        property.SetValue(obj, value);
                    }
                    catch
                    {
                        // Ловим исключение, если типы не совпадают, например int vs string
                        // Просто пропускаем это свойство
                    }
                }
            }

            return obj;
        }

    }
}