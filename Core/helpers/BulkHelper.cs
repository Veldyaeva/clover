using Dapper;
using DevExpress.Xpo.DB.Helpers;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

public class BulkHelper
{
    private const string RowNumberColumn = "__RowNumber";

    public void BulkAllDataUpdate<T>(
        SqlConnection connection,
        IEnumerable<T> data,
        string tableName,
        string[] keyColumns,
        SqlTransaction transaction = null,
        int batchSize = 1000,
        IEnumerable<string> excludeColumns = null,
        Dictionary<string, string> columnMappings = null)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (data == null) throw new ArgumentNullException(nameof(data));

        var type = typeof(T);
        var isNewProp = type.GetProperty("IsNew");
        var isModifiedProp = type.GetProperty("IsModified");
        var isDeletedProp = type.GetProperty("IsDeleted");

        if (isNewProp == null || isModifiedProp == null || isDeletedProp == null)
        {
            throw new InvalidOperationException("Тип должен содержать свойства IsNew, IsModified и IsDeleted");
        }

        var dataList = data.ToList();
        var insertList = dataList.Where(d => (bool?)isNewProp.GetValue(d) == true).ToList();
        var updateList = dataList.Where(d => (bool?)isModifiedProp.GetValue(d) == true).ToList();
        var deleteList = dataList.Where(d => (bool?)isDeletedProp.GetValue(d) == true).ToList();

        if (insertList.Any())
        {
            BulkInsert(connection, insertList, tableName, keyColumns, transaction, batchSize, excludeColumns, columnMappings);
        }

        if (updateList.Any())
        {
            BulkUpdate(connection, updateList, tableName, keyColumns, transaction, batchSize, excludeColumns, columnMappings);
        }

        if (deleteList.Any())
        {
            BulkDelete(connection, deleteList, tableName, keyColumns, transaction, excludeColumns, columnMappings);
        }
    }

    public void BulkInsert<T>(
        SqlConnection connection,
        IEnumerable<T> data,
        string tableName,
        string[] keyColumns,
        SqlTransaction transaction = null,
        int batchSize = 1000,
        IEnumerable<string> excludeColumns = null,
        Dictionary<string, string> columnMappings = null)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (data == null) throw new ArgumentNullException(nameof(data));

        var items = (data as IList<T>) ?? data.ToList();
        if (items.Count == 0)
        {
            return;
        }

        var mappings = BuildColumnMappings<T>(columnMappings);
        var excludeSet = new HashSet<string>(excludeColumns ?? Array.Empty<string>(), StringComparer.OrdinalIgnoreCase);

        var identityProperty = ResolveIdentityProperty(typeof(T), keyColumns);
        var shouldGenerateIdentity = identityProperty != null && ShouldGenerateIdentity(identityProperty, items);
        if (shouldGenerateIdentity && identityProperty != null)
        {
            excludeSet.Add(identityProperty.Name);
        }

        var tempTableName = "#TmpInsert" + Guid.NewGuid().ToString("N");
        var dataTable = ToDataTable(items, excludeSet, mappings, shouldGenerateIdentity);
        ExecuteNonQuery(connection, transaction, GenerateCreateTableSql(dataTable, tempTableName));

        try
        {
            using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.FireTriggers, transaction))
            {
                bulkCopy.DestinationTableName = tempTableName;
                bulkCopy.BatchSize = batchSize;

                foreach (DataColumn column in dataTable.Columns)
                {
                    bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                }

                bulkCopy.WriteToServer(dataTable);
            }

            var insertColumns = dataTable.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .Where(c => !string.Equals(c, RowNumberColumn, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (shouldGenerateIdentity && identityProperty != null)
            {
                var identityColumnName = ResolveColumnName(identityProperty, mappings);
                var identitySqlType = GetSqlType(Nullable.GetUnderlyingType(identityProperty.PropertyType) ?? identityProperty.PropertyType);
                var insertSql = BuildMergeInsertWithIdentitySql(tableName, tempTableName, insertColumns, identityColumnName, identitySqlType);

                var insertedValues = new List<(int RowNumber, object Value)>();
                using (var cmd = new SqlCommand(insertSql, connection, transaction))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        insertedValues.Add((reader.GetInt32(0), reader.GetValue(1)));
                    }
                }

                ApplyIdentityValues(items, identityProperty, insertedValues);
            }
            else
            {
                var insertSql = BuildInsertSql(tableName, tempTableName, insertColumns);
                ExecuteNonQuery(connection, transaction, insertSql);
            }
        }
        finally
        {
            TryDropTempTable(connection, transaction, tempTableName);
        }
    }

    public void BulkUpdate<T>(
        SqlConnection connection,
        IEnumerable<T> data,
        string tableName,
        string[] keyColumns,
        SqlTransaction transaction = null,
        int batchSize = 1000,
        IEnumerable<string> excludeColumns = null,
        Dictionary<string, string> columnMappings = null)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (data == null) throw new ArgumentNullException(nameof(data));

        var items = (data as IList<T>) ?? data.ToList();
        if (!items.Any())
        {
            return;
        }

        var mappings = BuildColumnMappings<T>(columnMappings);
        var dataTable = ToDataTable(items, excludeColumns, mappings);

        var tempTableName = "#TmpUpdate" + Guid.NewGuid().ToString("N");
        ExecuteNonQuery(connection, transaction, GenerateCreateTableSql(dataTable, tempTableName));

        try
        {
            using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.FireTriggers, transaction))
            {
                bulkCopy.DestinationTableName = tempTableName;
                bulkCopy.BatchSize = batchSize;

                foreach (DataColumn column in dataTable.Columns)
                {
                    bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                }

                bulkCopy.WriteToServer(dataTable);
            }

            var mergeSql = GenerateMergeSql(tableName, tempTableName, dataTable.Columns.Cast<DataColumn>().ToList(), keyColumns);
            if (!string.IsNullOrEmpty(mergeSql))
            {
                //var ddd = connection.QueryFirstOrDefaultAsync<T>($"select * from {tempTableName} ");

                ExecuteNonQuery(connection, transaction, mergeSql);
            }
        }
        finally
        {
            TryDropTempTable(connection, transaction, tempTableName);
        }
    }

    public void BulkDelete<T>(
        SqlConnection connection,
        IEnumerable<T> data,
        string tableName,
        string[] keyColumns,
        SqlTransaction transaction = null,
        IEnumerable<string> excludeColumns = null,
        Dictionary<string, string> columnMappings = null)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (data == null) throw new ArgumentNullException(nameof(data));

        var type = typeof(T);
        var isDeletedProp = type.GetProperty("IsDeleted");
        if (isDeletedProp == null)
        {
            throw new InvalidOperationException("Тип должен содержать свойство IsDeleted");
        }

        var deleteList = data.Where(d => (bool?)isDeletedProp.GetValue(d) == true).ToList();
        if (!deleteList.Any())
        {
            return;
        }

        var mappings = BuildColumnMappings<T>(columnMappings);
        var tempTableName = "#TmpDelete" + Guid.NewGuid().ToString("N");
        var table = ToDataTable(deleteList, excludeColumns, mappings);

        ExecuteNonQuery(connection, transaction, GenerateCreateTableSql(table, tempTableName));

        try
        {
            using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.FireTriggers, transaction))
            {
                bulkCopy.DestinationTableName = tempTableName;
                foreach (DataColumn column in table.Columns)
                {
                    bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                }

                bulkCopy.WriteToServer(table);
            }

            var joinClause = string.Join(" AND ", keyColumns.Select(k => $"target.[{k}] = source.[{k}]"));
            var deleteSql = $"DELETE target FROM {tableName} target INNER JOIN {tempTableName} source ON {joinClause}";
            ExecuteNonQuery(connection, transaction, deleteSql);
        }
        finally
        {
            TryDropTempTable(connection, transaction, tempTableName);
        }
    }

    private static Dictionary<string, string> BuildColumnMappings<T>(Dictionary<string, string> columnMappings)
    {
        var mappings = columnMappings != null
            ? new Dictionary<string, string>(columnMappings, StringComparer.OrdinalIgnoreCase)
            : new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (var property in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();
            if (columnAttribute != null && !string.IsNullOrWhiteSpace(columnAttribute.Name) && !mappings.ContainsKey(property.Name))
            {
                mappings[property.Name] = columnAttribute.Name;
            }
        }

        return mappings;
    }

    private static PropertyInfo ResolveIdentityProperty(Type type, string[] keyColumns)
    {
        if (keyColumns == null || keyColumns.Length == 0)
        {
            return null;
        }

        foreach (var key in keyColumns)
        {
            var property = type.GetProperty(key, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (property != null)
            {
                return property;
            }
        }

        return null;
    }

    private static bool ShouldGenerateIdentity<T>(PropertyInfo identityProperty, IList<T> items)
    {
        var attr = identityProperty.GetCustomAttribute<DatabaseGeneratedAttribute>();
        if (attr != null && attr.DatabaseGeneratedOption != DatabaseGeneratedOption.None)
        {
            return true;
        }

        return items.All(item => IsDefaultValue(identityProperty.GetValue(item)));
    }

    private static string ResolveColumnName(PropertyInfo property, Dictionary<string, string> columnMappings)
    {
        if (columnMappings != null && columnMappings.TryGetValue(property.Name, out var mappedName))
        {
            return mappedName;
        }

        var attr = property.GetCustomAttribute<ColumnAttribute>();
        if (attr != null && !string.IsNullOrWhiteSpace(attr.Name))
        {
            return attr.Name;
        }

        return property.Name;
    }

    private static string BuildMergeInsertWithIdentitySql(string targetTable, string tempTable, IList<string> columns, string identityColumn, string identitySqlType)
    {
        var columnList = string.Join(", ", columns.Select(c => $"[{c}]"));
        var valueList = string.Join(", ", columns.Select(c => $"source.[{c}]"));

        var innerSelect = string.Join(", ", columns.Select(c => $"temp.[{c}]"));
        if (!string.IsNullOrEmpty(innerSelect))
        {
            innerSelect += ", ";
        }
        innerSelect += $"temp.[{RowNumberColumn}]";

        return $@"
DECLARE @Inserted TABLE(RowNum INT, NewId {identitySqlType});
MERGE INTO {targetTable} AS target
USING (
    SELECT {innerSelect}
    FROM {tempTable} AS temp
) AS source
ON 1 = 0
WHEN NOT MATCHED THEN
    INSERT ({columnList})
    VALUES ({valueList})
OUTPUT source.[{RowNumberColumn}], inserted.[{identityColumn}] INTO @Inserted;

SELECT RowNum, NewId FROM @Inserted ORDER BY RowNum;";
    }

    private static string BuildInsertSql(string targetTable, string tempTable, IEnumerable<string> columns)
    {
        var columnList = string.Join(", ", columns.Select(c => $"[{c}]"));
        var selectList = string.Join(", ", columns.Select(c => $"source.[{c}]"));

        return $@"
INSERT INTO {targetTable} ({columnList})
SELECT {selectList}
FROM {tempTable} AS source;";
    }

    private static void ApplyIdentityValues<T>(IList<T> items, PropertyInfo identityProperty, IEnumerable<(int RowNumber, object Value)> values)
    {
        if (identityProperty == null) return;

        foreach (var (rowNumber, value) in values)
        {
            var index = rowNumber - 1;
            if (index < 0 || index >= items.Count)
            {
                continue;
            }

            var converted = ConvertTo(identityProperty.PropertyType, value);
            identityProperty.SetValue(items[index], converted);
        }
    }

    private static object ConvertTo(Type targetType, object value)
    {
        if (value == null || value is DBNull)
        {
            return targetType.IsValueType && Nullable.GetUnderlyingType(targetType) == null
                ? Activator.CreateInstance(targetType)
                : null;
        }

        var underlying = Nullable.GetUnderlyingType(targetType) ?? targetType;

        if (underlying == typeof(Guid))
        {
            return value is Guid guid ? guid : Guid.Parse(value.ToString());
        }

        return Convert.ChangeType(value, underlying);
    }

    private static bool IsDefaultValue(object value)
    {
        if (value == null || value is DBNull)
        {
            return true;
        }

        var type = value.GetType();
        return type.IsValueType && value.Equals(Activator.CreateInstance(type));
    }

    private static void ExecuteNonQuery(SqlConnection connection, SqlTransaction transaction, string sql)
    {
        using (var command = new SqlCommand(sql, connection, transaction))
        {
            command.ExecuteNonQuery();
        }
    }

    private static void TryDropTempTable(SqlConnection connection, SqlTransaction transaction, string tableName)
    {
        try
        {
            ExecuteNonQuery(connection, transaction, $"DROP TABLE {tableName}");
        }
        catch
        {
            // ignore cleanup errors
        }
    }

    private static string GenerateCreateTableSql(DataTable table, string tableName)
    {
        var columns = table.Columns.Cast<DataColumn>().Select(c => $"[{c.ColumnName}] {GetSqlType(c.DataType)}");
        return $"CREATE TABLE {tableName} ({string.Join(", ", columns)})";
    }

    private static string GetSqlType(Type type)
    {
        if (type == typeof(string)) return "NVARCHAR(MAX)";
        if (type == typeof(int)) return "INT";
        if (type == typeof(long)) return "BIGINT";
        if (type == typeof(short)) return "SMALLINT";
        if (type == typeof(byte)) return "TINYINT";
        if (type == typeof(DateTime)) return "DATETIME";
        if (type == typeof(decimal)) return "DECIMAL(18,5)";
        if (type == typeof(double) || type == typeof(float)) return "FLOAT";
        if (type == typeof(bool)) return "BIT";
        if (type == typeof(Guid)) return "UNIQUEIDENTIFIER";
        return "NVARCHAR(MAX)";
    }

    private static string GenerateMergeSql(string targetTable, string sourceTable, List<DataColumn> columns, string[] keyColumns)
    {
        var keyList = keyColumns ?? Array.Empty<string>();
        var keySet = new HashSet<string>(keyList, StringComparer.OrdinalIgnoreCase);

        var onClause = string.Join(" AND ", keyList.Select(k => $"T.[{k}] = S.[{k}]").ToArray());
        var updateColumns = columns.Where(c => !keySet.Contains(c.ColumnName)).ToList();
        if (!updateColumns.Any())
        {
            return null;
        }

        var updateClause = string.Join(", ", updateColumns.Select(c => $"T.[{c.ColumnName}] = S.[{c.ColumnName}]").ToArray());

        if (string.IsNullOrEmpty(updateClause))
        {
            return $@"
MERGE INTO {targetTable} AS T
USING {sourceTable} AS S
ON {onClause};";
        }

        return $@"
MERGE INTO {targetTable} AS T
USING {sourceTable} AS S
ON {onClause}
WHEN MATCHED THEN
    UPDATE SET {updateClause};";
    }

    private static DataTable ToDataTable<T>(
        IEnumerable<T> data,
        IEnumerable<string> excludeColumns,
        Dictionary<string, string> columnMappings,
        bool includeRowNumber = false)
    {
        var table = new DataTable();
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var excluded = new HashSet<string>(excludeColumns ?? Array.Empty<string>(), StringComparer.OrdinalIgnoreCase);

        if (includeRowNumber)
        {
            table.Columns.Add(RowNumberColumn, typeof(int));
        }

        foreach (var prop in properties)
        {
            if (excluded.Contains(prop.Name)) continue;
            if (prop.GetCustomAttribute<NotMappedAttribute>() != null) continue;

            var columnName = ResolveColumnName(prop, columnMappings);
            var columnType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

            if (!table.Columns.Contains(columnName))
            {
                table.Columns.Add(columnName, columnType);
            }
        }

        var index = 0;
        foreach (var item in data)
        {
            var row = table.NewRow();

            if (includeRowNumber)
            {
                row[RowNumberColumn] = ++index;
            }

            foreach (var prop in properties)
            {
                if (excluded.Contains(prop.Name)) continue;
                if (prop.GetCustomAttribute<NotMappedAttribute>() != null) continue;

                var columnName = ResolveColumnName(prop, columnMappings);
                row[columnName] = prop.GetValue(item) ?? DBNull.Value;
            }

            table.Rows.Add(row);
        }

        return table;
    }
}