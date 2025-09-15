using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

public class BulkHelper
{
    public void BulkAllDataUpdate<T>(SqlConnection connection, IEnumerable<T> data, string tableName, string[] keyColumns, SqlTransaction transaction = null, int batchSize = 1000, IEnumerable<string> excludeColumns = null, Dictionary<string, string> columnMappings = null)
    {
        var type = typeof(T);
        var isNewProp = type.GetProperty("IsNew");
        var isModifiedProp = type.GetProperty("IsModified");
        var isDeletedProp = type.GetProperty("IsDeleted");

        if (isNewProp == null || isModifiedProp == null || isDeletedProp == null)
            throw new InvalidOperationException("Тип должен содержать свойства IsNew, IsModified и IsDeleted");

        //var insertList = data.Where(d => (bool?)isNewProp.GetValue(d) == true).ToList();
        //var updateList = data.Where(d => (bool?)isModifiedProp.GetValue(d) == true).ToList();
        var insertList = isNewProp != null ? data.Where(d => (bool?)isNewProp.GetValue(d) == true).ToList() : new List<T>();
        var updateList = isModifiedProp != null ? data.Where(d => (bool?)isModifiedProp.GetValue(d) == true).ToList() : new List<T>();
        var deleteList = isDeletedProp != null ? data.Where(d => (bool?)isDeletedProp.GetValue(d) == true).ToList() : new List<T>();

        if (insertList.Any())
        {
            BulkInsert(connection, insertList, tableName, transaction, batchSize, excludeColumns, columnMappings);
        }

        if (updateList.Any())
        {
            BulkUpdate(connection, updateList, tableName, keyColumns, transaction, batchSize, excludeColumns, columnMappings);
        }
        if (deleteList.Any())
        {
            BulkDelete(connection, deleteList, tableName, keyColumns, transaction);
        }
    }
    public static void BulkInsert<T>(SqlConnection connection, IEnumerable<T> data, string tableName, SqlTransaction transaction = null, int batchSize = 1000, IEnumerable<string> excludeColumns = null, Dictionary<string, string> columnMappings = null)
    {
        // Автоматически заполним маппинг из атрибутов [Column]
        var autoMappings = typeof(T).GetProperties()
            .Where(p => p.GetCustomAttribute<ColumnAttribute>() != null)
            .ToDictionary(p => p.Name, p => ((ColumnAttribute)p.GetCustomAttribute(typeof(ColumnAttribute))).Name);

        if (columnMappings != null)
        {
            foreach (var kv in autoMappings)
            {
                if (!columnMappings.ContainsKey(kv.Key))
                    columnMappings.Add(kv.Key, kv.Value);
            }
        }
        else
        {
            columnMappings = autoMappings;
        }

        using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.FireTriggers, transaction))
        {
            bulkCopy.DestinationTableName = tableName;
            bulkCopy.BatchSize = batchSize;

            var table = ToDataTable(data, excludeColumns, columnMappings);

            foreach (DataColumn col in table.Columns)
            {
                string sourceColumn = col.ColumnName;
                string targetColumn = columnMappings != null && columnMappings.ContainsKey(sourceColumn)
                    ? columnMappings[sourceColumn]
                    : sourceColumn;

                try
                {
                    bulkCopy.ColumnMappings.Add(sourceColumn, targetColumn);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(string.Format("Ошибка маппинга колонки '{0}' → '{1}': {2}", sourceColumn, targetColumn, ex.Message), ex);
                }
            }

            bulkCopy.WriteToServer(table);
        }
    }
    public static void BulkUpdate<T>(SqlConnection connection, IEnumerable<T> data, string tableName, string[] keyColumns, SqlTransaction transaction = null, int batchSize = 1000, IEnumerable<string> excludeColumns = null, Dictionary<string, string> columnMappings = null)
    {
        var tempTableName = "#TmpUpdate" + Guid.NewGuid().ToString("N");
        var dataTable = ToDataTable(data, excludeColumns, columnMappings);

        // Create temp table
        var createTableSql = GenerateCreateTableSql(dataTable, tempTableName);
        using (var cmd = new SqlCommand(createTableSql, connection, transaction))
        {
            cmd.ExecuteNonQuery();
        }

        // Bulk insert into temp table
        using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.FireTriggers, transaction))
        {
            bulkCopy.DestinationTableName = tempTableName;
            bulkCopy.BatchSize = batchSize;

            foreach (DataColumn col in dataTable.Columns)
            {
                bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
            }

            bulkCopy.WriteToServer(dataTable);
        }

        // Generate MERGE
        var mergeSql = GenerateMergeSql(tableName, tempTableName, dataTable.Columns.Cast<DataColumn>().ToList(), keyColumns);
        using (var cmd = new SqlCommand(mergeSql, connection, transaction))
        {
            cmd.ExecuteNonQuery();
        }

        // Drop temp table
        using (var cmd = new SqlCommand("DROP TABLE " + tempTableName, connection, transaction))
        {
            cmd.ExecuteNonQuery();
        }
    }
    public static void BulkDelete<T>(SqlConnection connection, IEnumerable<T> data, string tableName, string[] keyColumns, SqlTransaction transaction = null)
    {
        var type = typeof(T);
        var isDeletedProp = type.GetProperty("IsDeleted");
        if (isDeletedProp == null)
            throw new InvalidOperationException("Тип должен содержать свойство IsDeleted");

        var deleteList = data.Where(d => (bool?)isDeletedProp.GetValue(d) == true).ToList();
        if (!deleteList.Any()) return;

        var tempTableName = "#TmpDelete" + Guid.NewGuid().ToString("N");
        var table = ToDataTable(deleteList, null, null);

        var createSql = GenerateCreateTableSql(table, tempTableName);
        using (var cmd = new SqlCommand(createSql, connection, transaction))
        {
            cmd.ExecuteNonQuery();
        }

        using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.FireTriggers, transaction))
        {
            bulkCopy.DestinationTableName = tempTableName;
            foreach (DataColumn col in table.Columns)
                bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
            bulkCopy.WriteToServer(table);
        }

        var joinClause = string.Join(" AND ", keyColumns.Select(k => $"target.[{k}] = source.[{k}]"));
        var deleteSql = $@"DELETE target FROM {tableName} target INNER JOIN {tempTableName} source ON {joinClause}";

        using (var cmd = new SqlCommand(deleteSql, connection, transaction))
        {
            cmd.ExecuteNonQuery();
        }

        using (var cmd = new SqlCommand("DROP TABLE " + tempTableName, connection, transaction))
        {
            cmd.ExecuteNonQuery();
        }
    }

    private static string GenerateCreateTableSql(DataTable table, string tableName)
    {
        var cols = table.Columns.Cast<DataColumn>().Select(c => $"[{c.ColumnName}] {GetSqlType(c.DataType)}");
        return $"CREATE TABLE {tableName} ({string.Join(", ", cols)})";
    }

    private static string GetSqlType(Type type)
    {
        if (type == typeof(string)) return "NVARCHAR(MAX)";
        if (type == typeof(int)) return "INT";
        if (type == typeof(DateTime)) return "DATETIME";
        if (type == typeof(decimal)) return "DECIMAL(18,2)";
        if (type == typeof(bool)) return "BIT";
        if (type == typeof(double)) return "FLOAT";
        return "NVARCHAR(MAX)";
    }

    private static string GenerateMergeSql(string targetTable, string sourceTable, List<DataColumn> columns, string[] keyColumns)
    {
        var onClause = string.Join(" AND ", keyColumns.Select(k => $"T.[{k}] = S.[{k}]"));
        var updateClause = string.Join(", ", columns.Where(c => !keyColumns.Contains(c.ColumnName)).Select(c => $"T.[{c.ColumnName}] = S.[{c.ColumnName}]"));
        return $@"
MERGE INTO {targetTable} AS T
USING {sourceTable} AS S
ON {onClause}
WHEN MATCHED THEN
    UPDATE SET {updateClause};";
    }
    private static DataTable ToDataTable<T>(IEnumerable<T> data, IEnumerable<string> excludeColumns = null, Dictionary<string, string> columnMappings = null)
    {
        var table = new DataTable();
        var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var excluded = new HashSet<string>(excludeColumns ?? new string[0], StringComparer.OrdinalIgnoreCase);

        foreach (var prop in props)
        {
            if (excluded.Contains(prop.Name)) continue;
            if (prop.GetCustomAttribute(typeof(NotMappedAttribute)) != null) continue;

            var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            var columnName = (columnMappings != null && columnMappings.ContainsKey(prop.Name)) ? columnMappings[prop.Name] : prop.Name;
            if (!table.Columns.Contains(columnName))
                table.Columns.Add(columnName, type);
        }

        foreach (var item in data)
        {
            var row = table.NewRow();
            foreach (var prop in props)
            {
                if (excluded.Contains(prop.Name)) continue;
                if (prop.GetCustomAttribute(typeof(NotMappedAttribute)) != null) continue;

                var columnName = (columnMappings != null && columnMappings.ContainsKey(prop.Name)) ? columnMappings[prop.Name] : prop.Name;
                row[columnName] = prop.GetValue(item) ?? DBNull.Value;
            }
            table.Rows.Add(row);
        }

        return table;
    }
}

