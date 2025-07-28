using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
            string query = @$"SELECT  TOP 1 with TIES * 
                            FROM proizv_view_constants pvc 
                            WHERE describe IS NOT NULL 
                            ORDER BY rank() over(partition by pvc.pc_id order by pvc.pc_id, pvc.begin_dt desc)";
            return await _dbService.GetListAsync<TarifModel>(query, new Dictionary<string, object>());
        }
        public async Task<List<TarifRabotModel>> LoadTarifRabotList()
        {
            string query = @$"SELECT * FROM sp_ras_rabot  
                            WHERE text IS NOT NULL ";
            return await _dbService.GetListAsync<TarifRabotModel>(query, new Dictionary<string, object>());
        }
        public async Task<List<string>> LoadFirmAsync()
        {
            string query = "SELECT firm FROM proizv_constants WHERE firm is NOT NULL GROUP BY firm";
            return await _dbService.GetListAsync<string>(query, new Dictionary<string, object>());
        }
        public async Task<List<TypeItemModel>> LoadTypeAsync()
        {
            string query = "SELECT field_name, pcst_id FROM proizv_constant_stores";
            return await _dbService.GetListAsync<TypeItemModel>(query, new Dictionary<string, object>());
        }


        public async Task SaveTarifAsync(TarifModel model)
        {
            // 1. Проверка на дубликат
            string checkQuery = "SELECT COUNT(*) FROM proizv_constants WHERE constant_name = @name";
            var checkParams = new Dictionary<string, object>
            {
                { "@name", model.constant_name }
            };

            int count = await _dbHelper.ExecuteScalarAsync<int>(checkQuery, checkParams);
            if (count > 0)
                throw new InvalidOperationException("Константа с таким именем уже существует!");

            // 2. Формирование XML
            string xml = CreateXmlFromModel(model);

            string path = @$"C:\1\ConstNew_{DateTime.Now:yyyyMMdd_HHmmss}.xml";
            await File.WriteAllTextAsync(path, xml, Encoding.UTF8);

            // 3. Вызов процедуры
            string sql = "exec Add_Const @xXml";
            var parameters = new Dictionary<string, object>
            {
                { "@xXml", xml }
            };

            //await _dbHelper.ExecuteNonQueryAsync(sql, parameters);
        }

        // Вспомогательный метод
        private string CreateXmlFromModel(TarifModel model)
        {
            var sb = new StringBuilder();
            using var writer = XmlWriter.Create(sb, new XmlWriterSettings { Indent = false });

            writer.WriteStartElement("VFPData");
            writer.WriteStartElement("constnew");

            writer.WriteElementString("pcid", "0"); // всегда 0 при добавлении
            writer.WriteElementString("constname", model.constant_name);
            writer.WriteElementString("dimens", model.dimens);
            writer.WriteElementString("deskr", model.describe);
            writer.WriteElementString("pcstid", model.pcstId.ToString());
            writer.WriteElementString("begindat", model.begin_dt.ToString("yyyy-MM-dd"));
            writer.WriteElementString("firm", model.firm ?? "");
            writer.WriteElementString("typeconst", model.typeConst ?? "");
            writer.WriteElementString("nametable", "proizv_constant_stor");
            writer.WriteElementString("namefield", "pscdt_id");

            writer.WriteElementString("valnum", model.value_numeric?.ToString(CultureInfo.InvariantCulture) ?? "");
            writer.WriteElementString("valint", model.value_integer?.ToString() ?? "");
            writer.WriteElementString("valflo", model.value_float?.ToString(CultureInfo.InvariantCulture) ?? "");
            writer.WriteElementString("valch", model.value_character ?? "");
            writer.WriteElementString("valdat", model.value_datetime?.ToString("yyyy-MM-dd") ?? "");

            writer.WriteEndElement(); // constnew
            writer.WriteEndElement(); // VFPData
            writer.Flush();

            return sb.ToString();
        }
    }
}
