using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using DevExpress.XtraScheduler.Drawing;
using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Helpers;
using SewingProduction.Models;
using DataTable = System.Data.DataTable;

namespace SewingProduction.Services
{
    /// <summary>
    /// Репозиторий работы с базой данных для таблиц art_norm, norm_rasz, norm_rask, norm_kont и доп.обработки.
    /// Использует Dapper для ускоренного доступа к данным.
    /// </summary>
    // NOTE: Class kept in this file for backward compatibility.
    // Canonical file for further extensions: Services/ArtNormRepository.cs.
    public partial class ArtNormRepository
    {
        private readonly DatabaseHelperSQL _dbHelper;
        //    private readonly HybridLogger _logger = new HybridLogger();
        private readonly FileLogger _logger = new FileLogger();
        private readonly DbService _dbService;


        /// <summary>
        /// Инициализирует новый экземпляр сервиса
        /// </summary>
        /// <param name="dbHelper">Помощник для работы с базой данных.</param>
        public ArtNormRepository(DatabaseHelperSQL dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _dbService = new DbService(_dbHelper);

            var map = new CustomPropertyTypeMap(
                typeof(ArtNormN),
                (type, columnName) =>
                {
                    var prop = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                   .FirstOrDefault(p => p.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));

                    if (prop != null && !System.Attribute.IsDefined(prop, typeof(System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute)))
                        return prop;

                    columnName = StringNormalizer.NormalizeLowerInvariant(columnName);

                    if (columnName == "grup") return type.GetProperty(nameof(ArtNormN.grup));
                    if (columnName == "sek_shv") return type.GetProperty(nameof(ArtNormN.SekShv));
                    if (columnName == "sek_vyaz3") return type.GetProperty(nameof(ArtNormN.SekVyaz3));
                    if (columnName == "sek_vyaz5") return type.GetProperty(nameof(ArtNormN.SekVyaz5));
                    if (columnName == "sek_vyaz6") return type.GetProperty(nameof(ArtNormN.SekVyaz6));
                    if (columnName == "sek_vyaz7") return type.GetProperty(nameof(ArtNormN.SekVyaz7));
                    if (columnName == "sek_vyaz10") return type.GetProperty(nameof(ArtNormN.SekVyaz10));
                    if (columnName == "sek_vyaz12") return type.GetProperty(nameof(ArtNormN.SekVyaz12));
                    if (columnName == "sek_vyazo") return type.GetProperty(nameof(ArtNormN.SekVyazo));
                    if (columnName == "sek_vyaz") return type.GetProperty(nameof(ArtNormN.SekVyaz));
                    if (columnName == "sek_vyaz14") return type.GetProperty(nameof(ArtNormN.SekVyaz14));
                    if (columnName == "sek_vyaz70") return type.GetProperty(nameof(ArtNormN.SekVyaz70));
                    if (columnName == "sek_vyaz71") return type.GetProperty(nameof(ArtNormN.SekVyaz71));
                    if (columnName == "sek_vyaz72") return type.GetProperty(nameof(ArtNormN.SekVyaz72));
                    if (columnName == "sek_vyaz62") return type.GetProperty(nameof(ArtNormN.SekVyaz62));
                    if (columnName == "sek_vyaz57") return type.GetProperty(nameof(ArtNormN.SekVyaz57));
                    if (columnName == "sek_vyaz18") return type.GetProperty(nameof(ArtNormN.SekVyaz18));
                    if (columnName == "data_sozd") return type.GetProperty(nameof(ArtNormN.dateCreate));
                    if (columnName == "data_obn") return type.GetProperty(nameof(ArtNormN.dateUpdate));
                    if (columnName == "sek_kr") return type.GetProperty(nameof(ArtNormN.SekKr));
                    if (columnName == "parentid") return type.GetProperty(nameof(ArtNormN.ParentId));
                    if (columnName == "statustext") return type.GetProperty(nameof(ArtNormN.StatusText));
                    if (columnName == "komment") return type.GetProperty(nameof(ArtNormN.Komment));
                    if (columnName == "annrecommendation") return type.GetProperty(nameof(ArtNormN.Reco));
                    if (columnName == "seb") return type.GetProperty(nameof(ArtNormN.Seb));
                    if (columnName == "anndatedel") return type.GetProperty(nameof(ArtNormN.dateDel));
                    if (columnName == "anncompdel") return type.GetProperty(nameof(ArtNormN.compDel));
                    if (columnName == "anndateadd") return type.GetProperty(nameof(ArtNormN.dateAdd));
                    if (columnName == "anncompadd") return type.GetProperty(nameof(ArtNormN.compAdd));
                    if (columnName == "arh") return type.GetProperty(nameof(ArtNormN.Arh));

                    return null;
                });

            SqlMapper.SetTypeMap(typeof(ArtNormN), map);
        }
        #region мои методы
        /// <summary>
        /// Удалять можно ТОЛЬКО при отмене создания новой строки. Никакие существующие строки нельзя удалять!
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="annId"></param>
        /// <returns></returns>
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
        // ArtNormService.cs
        public async Task<NormRaszSekView> GetCalculatedSekFromViewAsync(int annId)
        {
            string query = "SELECT * FROM dbo.NormRaszSek_view WHERE annId = @annId";
            var parameters = new { annId = annId };

            return await _dbService.GetFirstOrDefaultAsync<NormRaszSekView>(query, parameters);
        }

        public async Task<List<ArtNormN>> GetArtNormData()
        {
            string query = @" select 
                   AnnID, kod, grup, articul, mod, size_label, sek, sek_shv, sek_vyaz5, sek_vyaz6, sek_vyaz7, sek_vyaz10, sek_vyaz12, sek_vyazo,
                    sek_vyaz, sek_vyaz14, sek_vyaz70, sek_vyaz71, sek_vyaz72, sek_vyaz62, sek_vyaz18, sek_vyaz57, sek_kr, seb, 
                    slogn, komment, annRecommendation as Reco, data_sozd, data_obn, diz, constr, status_ann.name AS statusText, status, parentId,
                    annDateDel, annCompDel, annDateAdd, annCompAdd, arh
             FROM ArtNormNView JOIN status_ann ON status = status_id";

            using (var connecion = _dbHelper.GetConnection())
            {
                var res = await connecion.QueryAsync<ArtNormN>(query);
                return res.ToList();
            }
        }

        public void UpdateAnnIdinArticul(int annId, string kodd, string kodd_rt, string art)
        {
            string query = "UPDATE view_sp_articul SET annId = @annId WHERE ko = @kodd and articul = @art and kodd_rt = @kodd_rt";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@kodd_rt", kodd_rt }, { "@annId", annId }, { "@art", art }, { "@kodd", kodd } });
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
        public async Task<ArtNormN> GetArtNormDataById(int annId, CancellationToken ct = default)
        {
            try
            {//            SUBSTRING(kod,1,7) AS kod, 
                string query = @"
        SELECT 
                annId, grup, articul, mod, size_label, sek, seb, sek_vyaz, 
            data_obn, sek_shv, status_ann.name AS statusText, status, sek_vyazo, sek_vyaz5, 
            sek_vyaz7, sek_vyaz12, sek_vyaz10, sek_vyaz6, sek_vyaz18, sek_vyaz57, sek_kr, slogn, komment, annRecommendation as Reco,
            data_sozd, diz, constr, annDateDel, annCompDel, annDateAdd, annCompAdd, arh, parentId
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
            const string sql = "EXEC dbo.pztOperUpdateFast"; //"EXEC dbo.pztOperUpdateFast";
            await _dbHelper.ExecuteNonQueryAsync(sql);
        }

        /// <summary>
        /// загрузка РТ для увязки. Статус != архивное
        /// </summary>
        /// <returns>Возвращает таблицу артикулов</returns>
        public async Task<List<MyDataANN>> GetArtNormDataCurrent(bool includeAll)
        {
            string query = @"
                SELECT  
                    v.annId, v.grup, v.articul, v.mod, v.size_label, v.sek, v.sek_vyaz,
                    v.data_obn, v.sek_shv, sa.name AS statusText, v.status, v.sek_vyazo, v.sek_vyaz5, 
                    v.sek_vyaz7, v.sek_vyaz12, v.sek_vyaz10, v.sek_vyaz6, v.sek_kr, v.slogn, v.komment, v.annRecommendation, 
                    v.data_sozd, v.diz, v.constr, v.data_obn as dateUpdate, v.annDateDel, v.annCompDel, v.annDateAdd, v.annCompAdd, v.arh, v.parentId
                FROM ArtNormNView v
                JOIN status_ann sa ON v.status = sa.status_id 
                WHERE v.status != 3"; // Статус "архивное"

            object parameters = null;

            if (!includeAll)
            {
                query += @" 
                  AND EXISTS (SELECT top 1 *
                              FROM View_sp_articul spa
                              WHERE spa.annId = v.annId)";
                //         parameters = new { KodParam = kod }; // Параметр для Dapper
            }
            else
            {
                // Если all = true, условие по kodd_rt не добавляем
            }

            using (var connection = _dbHelper.GetConnection())
            {
                var result = await connection.QueryAsync<MyDataANN>(query, parameters);
                return result.ToList();
            }
        }

        public async Task<List<MyDataANN>> GetArtNormDataByArticul(string artPrefix)
        {
            string query = @"SELECT 
                annId, grup, articul, mod, size_label, sek, sek_vyaz, data_obn, sek_shv, 
                status, sek_vyazo, sek_vyaz5, sek_vyaz7, sek_vyaz12, sek_vyaz10, sek_vyaz6, 
                sek_kr, slogn, komment, annRecommendation, data_sozd, diz, constr,
                annDateDel, annCompDel, annDateAdd, annCompAdd, arh, parentId
                FROM artNormNView 
                WHERE status <> @StatusArchive AND articul LIKE @ArtPattern";

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
        public async Task<string> GetImage(int? annId = null, int? kod = null)
        {
            string sql;
            var p = new DynamicParameters();

            if (annId != null)        // поиск по AnnID
            {
                sql = @"SELECT TOP (1) 
                       dbo.getFileEskizForKodd_rt(vsa.annId)
                FROM   dbo.View_sp_articul vsa
                WHERE  vsa.annId = @annId";
                p.Add("@annId", annId);
            }
            else                      // прямой поиск по kodd
            {
                sql = "SELECT dbo.getFileEskizForKodd(@kod)";
                p.Add("@kod", kod);
            }

            return await _dbHelper.ExecuteScalarAsync<string>(sql, p);
        }
        internal Task<DataTable> GetNormOper()
        {
            string query = @"SELECT no.*, 
                             kp.text_proizv, kp.kod_proizv,
                             pv.text_vyaz, pv.kod_vyaz,
                             ob.text_ob, ob.kod_ob
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
        public async Task<List<NormRasz>> GetRelatedNormRasz(int annId, CancellationToken ct)
        {
            return await Task.Run(async () =>
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = @"SELECT 
 nr.AnnId, nr.N, nr.N1,nr.razryd, nr.Text text,
    nr.Sek, nr.Seb, nr.Kod, 
    nr.kod_o AS Kod_o,       
    nr.kod_ob AS KodOb,   
    nr.kod_podr AS KodPodr,  
    nr.kod_proizv AS KodProizv,
    nr.Spec, nr.Obor, nr.nrId,
    nr.nrDateAdd, nr.nrCompAdd, nr.nrDateDel, nr.nrCompDel,
    kp.text_proizv as TextProizv,
    pv.text_vyaz as TextVyaz,
    ob.text_ob as TextOb
FROM dbo.norm_rasz nr
LEFT JOIN kod_proizv kp ON nr.kod_proizv = kp.kod_proizv
LEFT JOIN podr_vyaz pv ON nr.kod_podr = pv.kod_vyaz
LEFT JOIN oborud_shv ob ON nr.kod_ob = ob.kod_ob
WHERE nr.annId = @annId";

                    await _logger.LogEventAsync($"GetRelatedNormRasz: Выполняется SQL-запрос для AnnId={annId}: {query}", "GetRelatedNormRasz");
                    List<NormRasz> result = null;
                    try
                    {
                        var queryResult = await connection.QueryAsync<NormRasz>(query, new { annId });
                        result = queryResult.ToList();

                        // Убедимся, что Obor правильно загрузилось (есичо, берём TextOb из джойна)
                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                if (string.IsNullOrWhiteSpace(r.Obor) && !string.IsNullOrWhiteSpace(r.TextOb))
                                {
                                    r.Obor = r.TextOb;
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        await _logger.LogErrorAsync(ex, $"GetRelatedNormRasz: ОШИБКА QueryAsync<NormRasz> для AnnId={annId}. Проверьте типы данных в модели NormRasz и таблице norm_rasz, особенно для свойств, которые должны быть int, но могут приходить как string или decimal из БД.");
                        // Дополнительно логируем информацию о свойствах модели NormRasz
                        var sb = new System.Text.StringBuilder("Свойства модели NormRasz:\n");
                        foreach (var prop in typeof(NormRasz).GetProperties())
                        {
                            sb.AppendLine($" - {prop.Name} (Тип: {prop.PropertyType.Name})");
                        }
                        await _logger.LogEventAsync(sb.ToString(), "GetRelatedNormRasz_ModelProps");
                        throw;
                    }

                    return result ?? new List<NormRasz>();
                }
            }, ct);
        }
        public async Task<List<NormRasz>> GetRelatedNormRasz(int annId)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = @"SELECT 
 nr.AnnId, nr.N, nr.N1,nr.razryd, nr.Text text,
    nr.Sek, nr.Seb, nr.Kod, 
    nr.kod_o AS Kod_o,       
    nr.kod_ob AS KodOb,   
    nr.kod_podr AS KodPodr,  
    nr.kod_proizv AS KodProizv,
    nr.Spec, nr.nrId,
    nr.nrDateAdd, nr.nrCompAdd, nr.nrDateDel, nr.nrCompDel,
    kp.text_proizv as TextProizv,
    pv.text_vyaz as TextVyaz,
    ob.text_ob as TextOb
FROM dbo.normraszview nr
LEFT JOIN kod_proizv kp ON nr.kod_proizv = kp.kod_proizv
LEFT JOIN podr_vyaz pv ON nr.kod_podr = pv.kod_vyaz
LEFT JOIN oborud_shv ob ON nr.kod_ob = ob.kod_ob
WHERE nr.annId = @annId";

                await _logger.LogEventAsync($"GetRelatedNormRasz: Выполняется SQL-запрос для AnnId={annId}: {query}", "GetRelatedNormRasz");
                List<NormRasz> result = null;
                try
                {
                    var queryResult = await connection.QueryAsync<NormRasz>(query, new { annId });
                    result = queryResult.ToList();

                    // Убедимся, что Obor правильно загрузилось (есичо, берём TextOb из вьюхи)
                    if (result != null)
                    {
                        foreach (var r in result)
                        {
                            if (string.IsNullOrWhiteSpace(r.Obor) && !string.IsNullOrWhiteSpace(r.TextOb))
                            {
                                r.Obor = r.TextOb;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    await _logger.LogErrorAsync(ex, $"GetRelatedNormRasz: ОШИБКА QueryAsync<NormRasz> для AnnId={annId}. Проверьте типы данных в модели NormRasz и таблице norm_rasz, особенно для свойств, которые должны быть int, но могут приходить как string или decimal из БД.");
                    // Дополнительно логируем информацию о свойствах модели NormRasz
                    var sb = new System.Text.StringBuilder("Свойства модели NormRasz:\n");
                    foreach (var prop in typeof(NormRasz).GetProperties())
                    {
                        sb.AppendLine($" - {prop.Name} (Тип: {prop.PropertyType.Name})");
                    }
                    await _logger.LogEventAsync(sb.ToString(), "GetRelatedNormRasz_ModelProps");
                    throw;
                }

                return result ?? new List<NormRasz>();
            }
        }
        public async Task<NormRasz> GetRelatedNormRaszByID(int _nrID, CancellationToken ct = default)
        {
            try
            {
                string query = @"SELECT 
                            nr.AnnId, nr.N, nr.N1,nr.razryd, nr.Text text,
                            nr.Sek, nr.Seb, nr.Kod, 
                            nr.kod_o AS Kod_o,       
                            nr.kod_ob AS KodOb,   
                            nr.kod_podr AS KodPodr,  
                            nr.kod_proizv AS KodProizv,
                            nr.Spec, nr.nrId,
                            nr.nrDateAdd, nr.nrCompAdd, nr.nrDateDel, nr.nrCompDel,
                            kp.text_proizv as TextProizv,
                            pv.text_vyaz as TextVyaz,
                            ob.text_ob as TextOb
                        FROM dbo.normraszview nr
                        LEFT JOIN kod_proizv kp ON nr.kod_proizv = kp.kod_proizv
                        LEFT JOIN podr_vyaz pv ON nr.kod_podr = pv.kod_vyaz
                        LEFT JOIN oborud_shv ob ON nr.kod_ob = ob.kod_ob
                        WHERE nr.nrID = @_nrID";

                return await _dbService.GetEntityAsync<NormRasz>(query, new { _nrID });
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных NormRasz для ID {_nrID}");
                return null;
            }
        }
        public async Task<string> GetSpecByOborudKod(int kodOb)
        {
            string query = "SELECT no_spec FROM oborud_shv WHERE kod_ob = @kodOb";
            var result = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "kodOb", kodOb } });
            return result.Rows.Count > 0 ? result.Rows[0]["no_spec"]?.ToString() : null;
        }


        public async Task<List<NormRask>> GetRelatedNormRask(int annId, CancellationToken ct)
        {
            return await Task.Run(async () =>
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = "SELECT id, AnnId, kod_o, Text as TextRask, razryd, Sek, Kod, Seb, N, n_ch as NCh, N1, seb_s as SebS, Obor FROM norm_rask WHERE annId = @annId";
                    //var result = await connection.QueryAsync<NormRask>(query, new Dictionary<string, object> { { "@annId", annId } }, cancellationToken: ct);
                    //return result.ToList();
                    var list = await connection.QueryAsync<NormRask>(
                       query,
                       new { annId },
                       transaction: null,
                       commandTimeout: null,
                       commandType: null
                       );

                    return list.AsList();
                }
            }, ct);
        }
        public async Task<List<NormRask>> GetRelatedNormRask(int annId)
        {
            using (
                var connection = _dbHelper.GetConnection())
            {
                string query = "SELECT id, AnnId, kod_o, Text as TextRask, razryd, Sek, Kod, Seb, N, n_ch as NCh, N1, seb_s as SebS, Obor, spec FROM norm_rask WHERE annId = @annId";
                //var result = await connection.QueryAsync<NormRask>(query, new Dictionary<string, object> { { "@annId", annId } }, cancellationToken: ct);
                //return result.ToList();
                var list = await connection.QueryAsync<NormRask>(
                   query,
                   new { annId },
                   transaction: null,
                   commandTimeout: null,
                   commandType: null
                   );

                return list.AsList();
            }
        }

        public async Task<List<NormKont>> GetRelatedNormKont(int annId, CancellationToken ct)
        {
            return await Task.Run(async () =>
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = "SELECT AnnId, kod_o, Text text, razryd, Sek, nkId FROM norm_kont WHERE annId = @annId";
                    //return _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@annId", annId } });
                    var result = await connection.QueryAsync<NormKont>(query, new Dictionary<string, object> { { "@annId", annId } });
                    return result.ToList();
                }
            }, ct);
        }
        public async Task<List<NormKont>> GetRelatedNormKont(int annId)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "SELECT AnnId, kod_o, Text text, razryd, Sek, nkId FROM norm_kont WHERE annId = @annId";
                //return _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@annId", annId } });
                var result = await connection.QueryAsync<NormKont>(query, new Dictionary<string, object> { { "@annId", annId } });
                return result.ToList();
            }
        }
        /// <summary>
        /// Получение неувязанных артикулов из sp_articul
        /// </summary>
        /// <returns></returns>
        public async Task<List<MyDataART>> GetRelatedSpArt()
        {
            string query = "SELECT DISTINCT SUBSTRING(kod,1,7) as kod, grup, articul, mod, razm, annID FROM sp_articul WHERE annId IS NULL";

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
            List<NZPByKoddRt> nzpList;
            Dictionary<int, int> pztCounts;

            using (var connection = _dbHelper.GetConnection())
            {
                var nzpResult = await connection.QueryAsync<NZPByKoddRt>(
                    "dbo.GetNZPByKoddRT",
                    new { xAnnID = annId },
                    commandType: CommandType.StoredProcedure,
                    commandTimeout: 120);
                nzpList = nzpResult.ToList();

                var pztResult = await connection.QueryAsync<(int annId, int PztCount)>(
                    "dbo.GetPztCountsByKoddRT",
                    new { xAnnID = annId },
                    commandType: CommandType.StoredProcedure,
                    commandTimeout: 120);
                pztCounts = pztResult.ToDictionary(x => x.annId, x => x.PztCount);
            }

            // Объединение результатов
            foreach (var row in nzpList)
            {
                if (pztCounts.TryGetValue(row.annId, out int count))
                    row.PZTCount = count;
            }

            return nzpList;
        }
        public async Task<List<NZPByKoddRt>> GetNzpWithPztCounts(int annId, CancellationToken ct)
        {
            List<NZPByKoddRt> nzpList;
            Dictionary<int, int> pztCounts;
            try
            {
                ct.ThrowIfCancellationRequested();

                var sw = Stopwatch.StartNew();
                using (var connection = _dbHelper.GetConnection())
                {
                    var t1 = Stopwatch.StartNew();
                    var nzpResult = await connection.QueryAsync<NZPByKoddRt>(new CommandDefinition(
                        "dbo.GetNZPByKoddRT", new { xAnnID = annId }, commandType: CommandType.StoredProcedure,
                        commandTimeout: 240, cancellationToken: ct));
                    nzpList = nzpResult.ToList();
                //    await _logger.LogEventAsync($"GetNZPByKoddRT: {t1.ElapsedMilliseconds} ms, rows={nzpList.Count}");
                    Debug.WriteLine($"GetNZPByKoddRT: {t1.ElapsedMilliseconds} ms, rows={nzpList.Count}");
                    ct.ThrowIfCancellationRequested();

                    var t2 = Stopwatch.StartNew();
                    var pztResult = await connection.QueryAsync<(int annId, int PztCount)>(new CommandDefinition(
                        "dbo.GetPztCountsByKoddRT", new { xAnnID = annId }, commandType: CommandType.StoredProcedure,
                        commandTimeout: 240, cancellationToken: ct));
                    pztCounts = pztResult.ToDictionary(x => x.annId, x => x.PztCount);
                //    await _logger.LogEventAsync($"GetPztCountsByKoddRT: {t2.ElapsedMilliseconds} ms, rows={pztCounts.Count}");
                    Debug.WriteLine($"GetPztCountsByKoddRT: {t2.ElapsedMilliseconds} ms, rows={pztCounts.Count}");
                }
              //  await _logger.LogEventAsync($"GetNzpWithPztCounts total: {sw.ElapsedMilliseconds} ms");
                Debug.WriteLine($"GetNzpWithPztCounts total: {sw.ElapsedMilliseconds} ms");
                // Объединение результатов
                foreach (var row in nzpList)
                {
                    if (pztCounts.TryGetValue(row.annId, out int count))
                        row.PZTCount = count;
                }

                return nzpList;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"GetNzpWithPztCounts failed for AnnID={annId}");
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<List<Brig>> GetWorkingBrigs(int annId, CancellationToken ct = default )
        {
            List<Brig> brigs = new List<Brig>();
            try
            {
                ct.ThrowIfCancellationRequested();
                using (var connection = _dbHelper.GetConnection())
                {
                    var nzpResult = await connection.QueryAsync<Brig>(
                        new CommandDefinition(
                            "dbo.GetNZPByKoddRT",
                            new { xAnnID = annId, @xRezType = 1 },
                            commandType: CommandType.StoredProcedure,
                            commandTimeout: 120,
                            cancellationToken: ct));
                    brigs = nzpResult.ToList();

                    ct.ThrowIfCancellationRequested();

                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"GetWorkingBrigs failed for AnnID={annId}");
                Debug.WriteLine(ex.ToString());
                return null;
            }
            return brigs;
        }
        public async Task<decimal> getArtNormnSeb(int annId)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var result = await connection.ExecuteScalarAsync<object>(
                    "SELECT dbo.getArtNormnSeb(@xAnnID)",
                    new { xAnnID = annId });

                if (result == null || result == DBNull.Value)
                    return 0m;

                return Convert.ToDecimal(result);
            }
        }

        public async Task<List<FioModel>> GetRelDesigner()
        {
            string query = "SELECT * FROM fio WHERE rab LIKE '%дизайнер%' OR rab LIKE '%конструктор%'";//"SELECT fio, tab FROM fio";
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
    public interface IJabberSender
    {
        Task SendToBrigsAsync(IEnumerable<int> brigIds, string message, int idType = 14, int tester = 63);
    }

    public sealed class JabberSender : IJabberSender
    {
        private readonly DatabaseHelperSQL _dbHelper;
        //    private readonly HybridLogger _logger = new HybridLogger();
        private readonly FileLogger _logger = new FileLogger();
        private readonly DbService _dbService;
        public JabberSender(DatabaseHelperSQL dbHelper)
       => _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
        public async Task SendToBrigsAsync(IEnumerable<int> brigIds, string message, int idType = 14, int tester = 63)
        {
            var ids = brigIds?
                .Where(id => id > 0)
                .Distinct()
                .ToArray();

            if (ids is null || ids.Length == 0 || string.IsNullOrWhiteSpace(message))
                return;
            //
            const string sql = @"
INSERT INTO [WMSWRITE].planeta.dbo.Jabber_Messager (Jabber_Body, Jabber_To)
SELECT DISTINCT @msg, v.icq
FROM [view_sprav_men] v
WHERE (v.id_type = @idType AND v.id_brig IN @brigIds) 
    OR (v.id_type = @tester)
  AND v.icq IS NOT NULL;";
            await _logger.LogEventAsync(sql);
            Debug.WriteLine(sql);
            try
            {
                using var connection = _dbHelper.GetConnection();
                await connection.ExecuteAsync(sql, new { msg = message, idType, brigIds = ids, tester });
                await _logger.LogEventAsync(
                    $"Jabber: '{message}' отправлено в {ids.Length} бригад(ы). {ids}",
                    "JabberSender");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "JabberSender.SendToBrigsAsync");
                throw;
            }
        }
    }

}
