using SewingProduction.Helpers;
using SewingProduction.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SewingProduction.Features.TeamWork.Services
{
    public sealed class NormRaszReportDataService
    {
        private readonly DatabaseHelper _dbHelper;

        public NormRaszReportDataService(DatabaseHelper dbHelper = null)
        {
            _dbHelper = dbHelper ?? new DatabaseHelper();
        }

        public NormRaszPreparedData Load(int annId)
        {
            var parameters = new Dictionary<string, object>
            {
                ["@annId"] = annId
            };

            var header = _dbHelper.ExecuteQuery(@"
SELECT
    ann.*,
    diz.fio AS fio_diz,
    constr.fio AS fio_constr
FROM artNormNView ann
LEFT JOIN fio diz ON diz.tab = ann.diz
LEFT JOIN fio constr ON constr.tab = ann.constr
WHERE ann.annID = @annId", parameters);

            EnsureColumn(header, "tb_Id", typeof(string));
            EnsureColumn(header, "FileEskizPath", typeof(string));

            var imageInfo = ExecuteStoredProcedure("dbo.getTbAndImageForRaszReport", parameters);
            if (header.Rows.Count > 0 && imageInfo.Rows.Count > 0)
            {
                if (imageInfo.Columns.Contains("tb_Id"))
                    header.Rows[0]["tb_Id"] = imageInfo.Rows[0]["tb_Id"];

                if (imageInfo.Columns.Contains("FileEskizPath"))
                    header.Rows[0]["FileEskizPath"] = imageInfo.Rows[0]["FileEskizPath"];
            }

            var rows = _dbHelper.ExecuteQuery(@"
SELECT *
FROM normRaszView
WHERE annId = @annId
ORDER BY n, n1", parameters);

            var byEquipment = _dbHelper.ExecuteQuery(@"
SELECT *
FROM NormRaszOborud
WHERE annId = @annId
ORDER BY annId, nppGroup, obor", parameters);

            var bySection = _dbHelper.ExecuteQuery(@"
SELECT *
FROM secByKodPodrAnnIdView
WHERE annId = @annId
ORDER BY text_vyaz", parameters);

            return new NormRaszPreparedData
            {
                AnnId = annId,
                Header = header,
                Rows = rows,
                ByEquipment = byEquipment,
                BySection = bySection
            };
        }

        private DataTable ExecuteStoredProcedure(string procedureName, Dictionary<string, object> parameters)
        {
            var table = new DataTable();

            using (var connection = _dbHelper.GetConnection())
            using (var command = new SqlCommand(procedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                AddParameters(command, parameters);

                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(table);
                }
            }

            return table;
        }

        private static void AddParameters(SqlCommand command, Dictionary<string, object> parameters)
        {
            if (parameters == null)
                return;

            foreach (var parameter in parameters)
            {
                command.Parameters.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value);
            }
        }

        private static void EnsureColumn(DataTable table, string columnName, Type type)
        {
            if (!table.Columns.Contains(columnName))
                table.Columns.Add(columnName, type);
        }
    }
}
