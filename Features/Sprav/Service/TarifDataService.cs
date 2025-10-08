using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.Sprav
{
    public class TarifDataService
    {
        private readonly DbService _dbService;
        private readonly DatabaseHelper _dbHelper;

        public TarifDataService(DbService dbService, DatabaseHelper dbHelper)
        {
            _dbService = dbService;
            _dbHelper = dbHelper;
        }
        public async Task<List<TarifModel>> LoadTarifList()
        {
            string query = @$"SELECT * FROM ViewProizvConstants";
            return await _dbService.GetListAsync<TarifModel>(query, new Dictionary<string, object>());
        }
        public async Task<List<TarifRabotModel>> LoadTarifRabotList()
        {
            string query = @$"SELECT * FROM sp_ras_rabot WHERE text IS NOT NULL ";
            return await _dbService.GetListAsync<TarifRabotModel>(query, new Dictionary<string, object>());
        }
        public async Task<List<string>> LoadFirmAsync()
        {
            string query = "SELECT firm FROM proizv_constants WHERE firm is NOT NULL GROUP BY firm";
            return await _dbService.GetListAsync<string>(query, new Dictionary<string, object>());
        }
        public async Task<List<TypeItemModel>> LoadTypeAsync()
        {
            string query = "SELECT * FROM proizv_constant_stores";
            return await _dbService.GetListAsync<TypeItemModel>(query, new Dictionary<string, object>());
        }
        public async Task ArhivTarifAsync(int pcID)
        {
            await _dbService.UpdateFieldAsync("proizv_constants", "arhiv", 1, "pc_id", pcID);
        }
        public async Task<List<TarifModelHistory>> LoadHistoryAsync(int pcid)
        {
            string query = @"
            SELECT 
                a.pc_id,
                a.begin_dt,
                a.event_dt,
                COALESCE(
                    CAST(n.constant_value AS VARCHAR),
                    CAST(i.constant_value AS VARCHAR),
                    CAST(f.constant_value AS VARCHAR),
                    c.constant_value,
                    FORMAT(d.constant_value, 'yyyy-MM-dd')
                ) AS value, 
                u.userName, 
                a.userComp
            FROM proizv_constant_apply_time a
            LEFT JOIN users u ON a.userID = u.userID
            LEFT JOIN proizv_constant_store_numeric n ON a.value_id = n.pcsn_id
            LEFT JOIN proizv_constant_store_integer i ON a.value_id = i.pcsi_id
            LEFT JOIN proizv_constant_store_float f ON a.value_id = f.pcsf_id
            LEFT JOIN proizv_constant_store_character c ON a.value_id = c.pcsc_id
            LEFT JOIN proizv_constant_store_datetime d ON a.value_id = d.pcsdt_id
            WHERE a.pc_id = @pcid
            ORDER BY a.begin_dt DESC";

            return await _dbService.GetListAsync<TarifModelHistory>(query, new { pcid = pcid });
        }


        public void SaveTarifAsync(TarifModel model, bool isEditMode, int userID)
        {
            // 1. Проверка на дубликат
            string checkQuery = "SELECT COUNT(*) FROM proizv_constants WHERE constant_name = @name";
            var checkParams = new Dictionary<string, object>
            {
                { "@name", model.constant_name }
            };

            int count = _dbHelper.ExecuteScalar(checkQuery, checkParams);
            if (count > 0 && !isEditMode)
                throw new InvalidOperationException("Константа с таким именем уже существует!");

            // 2. Формирование XML
            string path = @$"C:\1\ConstNew_xml.txt";
            CreateXmlFileFromModel(model, path, userID);

            // 3. Чтение XML из файла
            string xml = File.ReadAllText(path, Encoding.GetEncoding("utf-16"));

            // 4. Вызов процедуры
            string sql = "exec ACE_backup_new.dbo.Add_Const @xXml";
            var parameters = new Dictionary<string, object>
            {
                { "@xXml", xml }
            };

            _dbHelper.ExecuteNonQuery(sql, parameters);
        }

        private void CreateXmlFileFromModel(TarifModel model, string path, int userID)
        {
            var encoding = Encoding.GetEncoding("utf-16");

            using (var stream = new StreamWriter(path, false, encoding))
            {
                stream.WriteLine(@"<?xml version=""1.0"" encoding=""utf-16"" standalone=""yes""?>");
                stream.WriteLine("<VFPData>");
                stream.WriteLine("  <constnew>");

                void WriteTag(string name, object? value)
                {
                    if (value == null) return;

                    string str = value switch
                    {
                        DateTime dt => dt.ToString("yyyy-MM-dd"),
                        float f => f.ToString(CultureInfo.InvariantCulture),
                        decimal d => d.ToString(CultureInfo.InvariantCulture),
                        _ => value.ToString()
                    };

                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        string escaped = System.Security.SecurityElement.Escape(str);
                        stream.WriteLine($"    <{name}>{escaped}</{name}>");
                    }
                }

                WriteTag("pcid", model.pc_id);
                WriteTag("constname", model.constant_name);
                WriteTag("dimens", model.dimension);
                WriteTag("deskr", model.describe);
                WriteTag("pcstid", model.pcstId);
                WriteTag("begindat", model.begin_dt);
                WriteTag("firm", model.firm);
                WriteTag("typeconst", model.typeConst);
                WriteTag("nametable", model.store_name);
                WriteTag("namefield", model.name_field_id);
                WriteTag("valnum", model.value_numeric);
                WriteTag("valint", model.value_integer);
                WriteTag("valflo", model.value_float);
                WriteTag("valch", model.value_character);
                WriteTag("valdat", model.value_datetime);
                WriteTag("priznsign", model.priznSign);
                WriteTag("whereuses", model.whereUses);
                WriteTag("userid", userID);
                WriteTag("usercomp", Environment.MachineName);

                stream.WriteLine("  </constnew>");
                stream.WriteLine("</VFPData>");
            }
        }


    }
}
