using Dapper;
using SewingProduction.Helpers;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DataTable = System.Data.DataTable;

namespace SewingProduction.Services
{
    /// <summary>
    /// Сервис работы с базой данных для таблиц art_norm, norm_rasz, norm_rask, norm_kont и доп.обработки.
    /// Использует Dapper для ускоренного доступа к данным.
    /// </summary>
    public class ArtNormService
    {
        private readonly DatabaseHelper _dbHelper;
        //    private readonly HybridLogger _logger = new HybridLogger();
        private readonly FileLogger _logger = new FileLogger();
        private readonly DbService _dbService;


        /// <summary>
        /// Инициализирует новый экземпляр сервиса
        /// </summary>
        /// <param name="dbHelper">Помощник для работы с базой данных.</param>
        public ArtNormService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _dbService = new DbService(_dbHelper);

        }
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
            var map = new CustomPropertyTypeMap(
               typeof(ArtNormN),
               (type, columnName) =>
               {
                   var prop = type.GetProperties().FirstOrDefault(p => p.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));
                   if (prop != null && !Attribute.IsDefined(prop, typeof(NotMappedAttribute))) return prop;

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
                   if (columnName.Equals("statusText", StringComparison.OrdinalIgnoreCase)) return type.GetProperty(nameof(ArtNormN.StatusText));

                   return null;
               });

            SqlMapper.SetTypeMap(typeof(ArtNormN), map);

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
            //     SqlMapper.SetTypeMap(typeof(ArtNormN), null);
            //}
        }

        public void UpdateAnnIdinArticul(string kod, int annId)
        {
            string query = "UPDATE sp_articul SET annId = @annId WHERE kod like @kod";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@kod", kod + "%" }, { "@annId", annId } });
        }

        /// <summary>
        /// Сбрасывает annId в таблице sp_articul для всех записей, связанных с указанным kodd_rt.
        /// </summary>
        /// <param name="kod"></param>
        /// <returns></returns>
        public async Task ResetAnnIdinArticul(int kod)
        {
            string query = "UPDATE sp_articul SET annId = NULL WHERE left(kod,7) = @kod";
            await _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { { "@kod", kod } });
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
                return await _dbService.GetEntityAsync<ArtNormN>(query, new { annId });
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных ArtNormN для ID {annId}");
                return null;
            }
        }

        internal async Task<List<KodProizvModel>> GetKod_proizv()
        {
            string query = "select kod_proizv, text_proizv from kod_proizv";
            using (var connection = _dbHelper.GetConnection())
            {
                var result = await connection.QueryAsync<KodProizvModel>(query);
                return result.ToList();
            }
        }

        internal async Task<List<PodrVyazModel>> GetPodr_vyaz()
        {
            string query = "select kod_vyaz, text_vyaz from podr_vyaz";
            using (var connection = _dbHelper.GetConnection())
            {
                var result = await connection.QueryAsync<PodrVyazModel>(query);
                return result.ToList();
            }
        }

        internal async Task<List<OborudShvModel>> GetOborud_shv()
        {
            string query = "select kod_ob, text_ob from oborud_shv";
            using (var connection = _dbHelper.GetConnection())
            {
                var result = await connection.QueryAsync<OborudShvModel>(query);
                return result.ToList();
            }
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
        public async Task<List<MyDataANN>> GetArtNormDataCurrent(int kod, bool all)
        {
            string query = "";
            query = @"SELECT  
                                        SUBSTRING(kod, 1, 7) AS kod, annId, grup, articul, mod, sek, sek_vyaz,
                    data_obn, sek_shv, status_ann.name AS statusText, status, sek_vyazo, sek_vyaz5, 
                    sek_vyaz7, sek_vyaz12, sek_vyaz10, sek_vyaz6, sek_kr, slogn, komment, 
                    data_sozd, diz, constr FROM ArtNormNView JOIN status_ann ON status=status_id WHERE status!=3";
            if (!all)
            {
                query += " AND(annId IN(SELECT annId FROM View_sp_articul WHERE kodd_rt = '@kod'))";
            }
            return await _dbHelper.GetConnection().QueryAsync<MyDataANN>(query).ContinueWith(t => t.Result.ToList());
        }


        public async Task<List<MyDataANN>> GetArtNormDataByArticulPrefix(string artPrefix)
        {
            string query = $"SELECT * FROM artNormNView WHERE status <> @StatusArchive AND articul LIKE @ArtPattern";

            var parameters = new
            {
                StatusArchive = (int)Status.Archive,
                ArtPattern = artPrefix + "%"
            };

            using (var connection = _dbHelper.GetConnection())
            {
                 var result = await connection.QueryAsync<MyDataANN>(query, parameters);
                 return result.ToList();
            }
        }

        /// <summary>
        /// Получение пути к файлу изображения
        /// </summary>
        /// <param name="kod">код</param>
        /// <returns></returns>
        public async Task<string> GetImage(int kod)
        {
            string query = "select dbo.getFileEskizForKodd(@kod) as pathpict ";
            object result = await _dbHelper.ExecuteScalarAsync(query, new Dictionary<string, object> { { "@kod", kod } });
            return result?.ToString(); 
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
        public async Task<List<MyDataART>> GetRelatedSpArt()
        {
            string query = "SELECT DISTINCT SUBSTRING(kod,1,7) as kod, grup, articul, mod, annId FROM sp_articul WHERE annID IS NULL";

            using (var connection = _dbHelper.GetConnection())
            {
                var result = await connection.QueryAsync<MyDataART>(query);
                return result.ToList();
            }
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

        public async Task<List<FioModel>> GetRelDesigner()
        {
            string query = "SELECT fio, tab FROM fio";
            using (var connection = _dbHelper.GetConnection())
            {
                var result = await connection.QueryAsync<FioModel>(query);
                return result.ToList();
            }
        }


        public async Task DeleteRelatedNormTables(int annId)
        {
            await DeleteByAnnId("norm_rasz", annId);
            await DeleteByAnnId("norm_rask", annId);
            await DeleteByAnnId("norm_kont", annId);
            await DeleteByAnnId("norm_dop_obr", annId);
        }
        #endregion


    }
}
 