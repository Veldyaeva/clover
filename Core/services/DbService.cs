using SewingProduction.Helpers;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using Dapper;
using DevExpress.Mvvm.Native;
using System.Reflection;
using SewingProduction.Interfaces;
using System.ComponentModel;
using System.Diagnostics;
using Z.Dapper.Plus;

namespace SewingProduction.Services
{
    /// <summary>
/// Сервис работы с базой данных для таблиц art_norm, norm_rasz, norm_rask, norm_kont и доп.обработки.
/// Использует Dapper для ускоренного доступа к данным.
/// </summary>
 public class DbService
    {
        private readonly DatabaseHelper _dbHelper;
        //    private readonly HybridLogger _logger = new HybridLogger(); //убрала пока гибридный логгер, не хочу писать в базу
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
        /// Выполняет SQL-запрос и возвращает список объектов типа T
        /// </summary>
        /// <typeparam name="T">Тип модели</typeparam>
        /// <param name="query">запрос</param>
        /// <param name="parameters">объект с параметрами запроса</param>
        /// <returns>Список объектов типа T</returns>
        public async Task<List<T>> GetListAsync<T>(string query, object parameters)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var result = await connection.QueryAsync<T>(query, parameters);
                return result.ToList();
            }
        }

        /// <summary>
        /// Обновляет одно поле в таблице по заданному условию.
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
        /// Вставляет данные в таблицу
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
                    parameters[parameterName] = NormalizeValue(value, updating: false);
                }

                string columnsPart = string.Join(", ", columns);
                string valuesPart = string.Join(", ", values);

                string query = $"INSERT INTO {tableName} ({columnsPart}) VALUES ({valuesPart}); SELECT SCOPE_IDENTITY();";

                await _logger.LogEventAsync($"SQL Query: {query}", "InsertEntityAsync");
                foreach (var param in parameters)
                {
                    await _logger.LogEventAsync($"Parameter {param.Key}: {param.Value} (тип: {param.Value?.GetType()})", "InsertEntityAsync");
                }

                int? result = await _dbHelper.ExecuteScalarAsync<int>(query, parameters);

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
       /// Обновление данных в таблице
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
                    parameters[parameterName] = NormalizeValue(prop.GetValue(entity), updating:true);
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
        /// Метод для обновления данных в таблице, если записи не существует, данные вставляются, если существует, обновляются
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

        public async Task SaveListAsync<T>(BindingList<T> list, string tableName, string keyFieldName, List<int> deletedIds)
    where T : class, INewable, new()
        {
            var stopwatch = Stopwatch.StartNew();
            var bulkStopwatch = new Stopwatch();

            var newItems = list.Where(x => x.IsNew).ToList();
            var existingItems = list.Where(x => !x.IsNew).ToList();

            string itemTypeName = typeof(T).Name;
            await _logger.LogEventAsync($"[{itemTypeName}] Start saving. New: {newItems.Count}, Existing: {existingItems.Count}", "SaveListAsync");

            // 1. Удаление
            if (deletedIds?.Any() == true)
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string deleteSql = $"DELETE FROM {tableName} WHERE {keyFieldName} IN @ids";
                    await connection.ExecuteAsync(deleteSql, new { ids = deletedIds });
                    await _logger.LogEventAsync($"[{itemTypeName}] Удалено записей: {deletedIds.Count}", "SaveListAsync");
                }
            }

            // 2. Обработка новых
            if (newItems.Any())
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    await _logger.LogEventAsync($"[{itemTypeName}] BulkInsert: {newItems.Count}", "SaveListAsync");
                    bulkStopwatch.Restart();
                    await connection.BulkInsertAsync(newItems);
                    bulkStopwatch.Stop();
                }

                foreach (var item in newItems)
                    item.IsNew = false;
            }

            // 3. Обновление
            if (existingItems.Any())
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    await _logger.LogEventAsync($"[{itemTypeName}] BulkUpdate: {existingItems.Count}", "SaveListAsync");
                    bulkStopwatch.Restart();
                    await connection.BulkUpdateAsync(existingItems);
                    bulkStopwatch.Stop();
                }
            }

            stopwatch.Stop();
            await _logger.LogEventAsync($"[{itemTypeName}] Finished saving. Total: {stopwatch.ElapsedMilliseconds} ms", "SaveListAsync");
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
        private object NormalizeValue(object value, bool updating)
        {
            if (value == null)
                return updating? DBNull.Value:null;

            if (value is DateTime dt)
            {
                if (dt == DateTime.MinValue)
                    return updating? DBNull.Value: null;
                return dt;
            }

            if (value is bool boolValue)
                return boolValue ? 1 : 0;

            return value;
        }

        #endregion


        #region convertorToModels


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