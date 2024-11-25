using System;
using System.Collections.Generic;

public class RussianTableName
{
    // (русское имя, тип данных)
    private readonly Dictionary<string, Dictionary<string, (string RussianName, string DataType)>> _russianTableName;

    public RussianTableName()
    {
        _russianTableName = new Dictionary<string, Dictionary<string, (string, string)>>
        {
            {
                "TableName", new Dictionary<string, (string, string)>
                {
                    { "ColumnName1", ("НазваниеКолонки1", "int") },
                    { "ColumnName2", ("НазваниеКолонки2", "string") }
                }
            },
            {
                "oborud_shv_ob", new Dictionary<string, (string, string)>
                {
                    { "ko_ob_all", ("Код", "int") },
                    { "text_ob", ("Наименование", "string") }
                }
            },
            {
                "spOborudMachine", new Dictionary<string, (string, string)>
                {
                    { "id", ("Код", "int") },
                    { "name", ("Наименование", "string") },
                    { "miniName", ("Сокращенное наим-е", "string") }
                }
            },
            {
                "matrix_class", new Dictionary<string, (string, string)>
                {
                    { "id_class", ("Код", "int") },
                    { "name_class", ("Наименование", "int") },
                    { "mc_id", ("МЦ ИД", "int") },
                    { "caption", ("Класс", "string") },
                    { "koef", ("Коэффициент", "float") }
                }
            }
        };
    }

    public (string RussianName, string DataType) GetRussianNameAndType(string tableName, string columnName)
    {
        if (_russianTableName.TryGetValue(tableName, out var columnMappings))
        {
            if (columnMappings.TryGetValue(columnName, out var mapping))
            {
                return mapping;
            }
        }
        return (columnName, "string");
    }

    public string GetRussianName(string tableName, string columnName)
    {
        var mapping = GetRussianNameAndType(tableName, columnName);
        return mapping.RussianName; // Возвращает только русское имя
    }

    public string GetColumnType(string tableName, string columnName)
    {
        var mapping = GetRussianNameAndType(tableName, columnName);
        return mapping.DataType; // Возвращает только тип данных
    }
}
