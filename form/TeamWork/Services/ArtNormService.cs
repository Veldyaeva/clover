using System;
using System.Collections.Generic;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DataTable = System.Data.DataTable;
using SewingProduction.Helpers;
using System.Data;
using DevExpress.CodeParser;
using SewingProduction.form;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SewingProduction.Models;
using SewingProduction.BdContext;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Linq;
using System.Data.SqlClient;

namespace SewingProduction.Services
{
    /// <summary>
    /// класс для обработки SQL
    /// </summary>
    public class ArtNormService
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly HybridLogger _logger = new HybridLogger();
        public ArtNormService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            
        }

        //public async Task<List<ArtNormN>> GetArtNormData()
        //{
        //    //string query = "SELECT SUBSTRING(kod,1,7) as kod, annId, grup, articul, mod, sek, sek_vyaz, data_obn, sek_shv, status_ann.name AS stat, status, sek_vyazo, sek_vyaz5, sek_vyaz7, sek_vyaz12, sek_vyaz10, sek_vyaz6, sek_kr, slogn, komment, data_sozd, diz, constr FROM ArtNormNView JOIN status_ann ON status=status_id";
        //    //return await _dbHelper.ExecuteQuery(query);
    //} 
            public async Task<List<ArtNormN>> GetArtNormData()
        {
            string query = @"
        SELECT 
            SUBSTRING(kod,1,7) AS kod, annId, grup, articul, mod, sek, sek_vyaz, 
            data_obn, sek_shv, status_ann.name AS statusText, status, sek_vyazo, sek_vyaz5, 
            sek_vyaz7, sek_vyaz12, sek_vyaz10, sek_vyaz6, sek_kr, slogn, komment, 
            data_sozd, diz, constr 
        FROM ArtNormNView 
        JOIN status_ann ON status = status_id";

            DataTable table = await _dbHelper.ExecuteQueryAsync(query);
            return ConvertToList(table);
        }

        public async Task<ArtNormN> GetArtNormById(int annId)
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

            DataTable table = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@annId", annId } });

            if (table != null && table.Rows.Count > 0)
            {
                return ConvertToList(table).FirstOrDefault();
            }

            return null;
        }
        public async Task<List<NormRask>> GetNormRaskByAnnId(int annId)
        {
            string query = "SELECT * FROM norm_rask WHERE annId = @annId";

            var dataTable = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@annId", annId } });

            var result = new List<NormRask>();

            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new NormRask
                {
                    AnnId = row["annId"] != DBNull.Value ? Convert.ToInt32(row["annId"]) : 0,
                    Kod = (int)row["kod"],
                    KodO = row["kod_o"]?.ToString(),
                    Text = row["text"]?.ToString(),
                    Sek = (int)row["sek"],
                    Razryad = row["razryd"] != DBNull.Value ? Convert.ToInt32(row["razryd"]) : 0,
                    N_ch = row["n_ch"] != DBNull.Value ? Convert.ToInt32(row["n_ch"]) : 0,
                    Obor = row["obor"]?.ToString()
                });
            }

            return result;
        }

        public async Task<string> GetEmployeeFullName(int employeeId)
        {
            try
            {
                string query = "SELECT fio FROM fio WHERE tab = @employeeId";
                DataTable result = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@employeeId", employeeId } });

                // Log the result of the query
                await _logger.LogEventAsync($"Query Result for Employee ID {employeeId}: {result.Rows.Count} rows found.", "GetEmployeeFullName");

                if (result.Rows.Count > 0)
                {
                    return result.Rows[0]["fio"].ToString();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при получении ФИО сотрудника");
            }
            return string.Empty; // Return empty if not found or error occurs
        }
        public async Task<List<NormRasz>> GetNormRaszByAnnId(int annId)
        {
            string query = "SELECT * FROM norm_rask WHERE annId = @annId";

            var dataTable = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@annId", annId } });

            var result = new List<NormRasz>();

            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new NormRasz
                {
                    AnnId = row["annId"] != DBNull.Value ? Convert.ToInt32(row["annId"]) : 0,
                    Kod = row["kod"] != DBNull.Value ? Convert.ToInt32(row["annId"]) : 0,
                    KodO = row["kod_o"] != DBNull.Value ? Convert.ToInt32(row["annId"]) : 0,
                    Text = row["text"]?.ToString(),
                    Sek = row["sek"] != DBNull.Value ? Convert.ToInt32(row["annId"]) : 0,
                    Razryad = row["razryd"] != DBNull.Value ? Convert.ToInt32(row["razryd"]) : 0,
                    Obor = row["obor"]?.ToString(),
        N1 = row["N1"] != DBNull.Value ? Convert.ToInt32(row["annId"]) : 0,
        KodPodr = row["KodPodr"] != DBNull.Value ? Convert.ToInt32(row["annId"]) : 0,
        KodProizv = row["KodProizv"] != DBNull.Value ? Convert.ToInt32(row["annId"]) : 0,
        Spec = row["Spec"]?.ToString(),
        KodOb = row["KodOb"]?.ToString(),
        TextProizv = row["TextProizv"]?.ToString(),
        TextOb = row["TextOb"]?.ToString(),
        TextVyaz = row["TextVyaz"]?.ToString()

                });
            }

            return result;
        }


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
                    DataObn = row["data_obn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["data_obn"]),
                    SekShv = row["sek_shv"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_shv"]),
                    StatusText = row["statusText"] == DBNull.Value? "" : row["statusText"].ToString(),
                    Status = row["status"] == DBNull.Value ? 0 : Convert.ToInt32(row["status"]),
                    SekVyazo = row["sek_vyazo"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_vyazo"]),
                    SekVyaz5 = row["sek_vyaz5"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_vyaz5"]),
                    SekVyaz7 = row["sek_vyaz7"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_vyaz7"]),
                    SekVyaz12 = row["sek_vyaz12"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_vyaz12"]),
                    SekVyaz10 = row["sek_vyaz10"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_vyaz10"]),
                    SekVyaz6 = row["sek_vyaz6"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_vyaz6"]),
                    SekKr = row["sek_kr"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_kr"]),
                    Slogn = row["slogn"] == DBNull.Value ? 0 : Convert.ToInt32(row["slogn"]),
                    Komment = row["komment"] == DBNull.Value ? "" : row["komment"].ToString(),
                    DataSozd = row["data_sozd"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(Convert.ToDateTime(row["data_sozd"])),
                    Diz = row["diz"] == DBNull.Value ? 0 : Convert.ToInt32(row["diz"]),
                    Constr = row["constr"] == DBNull.Value ? 0 : Convert.ToInt32(row["constr"])
                });
            }
            return list;
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
            data_sozd, diz, constr FROM ArtNormNView JOIN status_ann ON status=status_id WHERE status<3";

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
            DataTable result = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "kod", kod } });
            //return (List<ArtNormN>)result;
            //DataTable table = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object>);
            return ConvertToList(result);

            //return (DataTable)result;
        }

        public async Task<List<ArtNormN>> GetArtNormDataCurrent(string art)
        {
            string query = $"SELECT * FROM artNormNView WHERE status<{(int)Status.Archive} AND articul IN (SELECT articul FROM View_sp_articul WHERE articul LIKE @art)";
            object result = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "art", art + "%" } });
            return result as List<ArtNormN>;
        }
        public async Task ResetAnnId(int kodd_rt)
        {
            // string query = "UPDATE sp_articul SET annId = NULL WHERE kod = @kod";
            string query = "UPDATE sp_articul SET annId = NULL WHERE kod IN (SELECT kod FROM view_sp_articul WHERE kodd_rt = @kod)";
           await _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { { "@kod", kodd_rt } });
        }

        public void UpdateAnnId(int kod, int annId)
        {
            string query = "UPDATE sp_articul SET annId = @annId WHERE kod like @kod";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@kod", kod + "%" }, { "@annId", annId } });
        }

        // Получение связанных данных
        /// <summary>
        /// Получает данные из таблицы Norm_rasz (dataTable) 
        /// </summary>
        /// <param name="annId">идентификатор РТ</param>
        /// <returns></returns>
        public Task<DataTable> GetRelatedNormRasz(int annId)
        {
            string query = "SELECT annId, n, n1, razryd, text, sek, kod, kod_o, kod_ob FROM norm_rasz WHERE annId = @annId";
            return _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@annId", annId } });
        }
        public Task<DataTable> GetRelatedNormRask(int annId)
        {
            string query = "SELECT annId, kod_o, razryd, text, sek  FROM norm_rask WHERE annId = @annId";
            return _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@annId", annId } });
        }

        public Task<DataTable> GetRelatedNormKont(int annId)
        {
            string query = "SELECT annId, kod_o, razryd, text, sek FROM norm_kont WHERE annId = @annId";
            return _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@annId", annId } });
        }

        public Task<DataTable> GetRelatedNormDopObr(int annId)
        {
            string query = "SELECT annId, sek_p, sek_p_tamp, sek_v, sek_stra FROM norm_dop_obr WHERE annId = @annId";
            return _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@annId", annId } });
        }

        public Task<DataTable> GetRelDesigner()
        {

            string query = "SELECT fio, tab FROM fio";
            return _dbHelper.ExecuteQueryAsync(query);
        }

        /// <summary>
        /// Получение связанных данных из sp_articul
        /// </summary>
        /// <param name="annId">annId=null=> загрузка неувязанных артикулов
        /// annId!=null => загрузка артикулов с НЗП процедурой GetNZPByKoddRT 
        /// </param>
        /// <returns></returns>
        public async Task<DataTable> GetRelatedSpArt(int annId)
        {
            //string query = "";
            //if (annId == 0)
            //{ query = "SELECT SUBSTRING(kod,1,7) as kod, grup, articul, mod, FROM sp_articul WHERE annID IS NULL"; }
            //else if (annId>0)
            //{ query = $"SELECT SUBSTRING(kod,1,7) as kod, grup, articul, mod FROM sp_articul WHERE annID = @annId"; }
            string query = annId == 0
            //? "SELECT DISTINCT SUBSTRING(kod,1,7) as kod, grup, articul, mod, annId FROM sp_articul WHERE annID IS NULL"
            //: $"SELECT DISTINCT SUBSTRING(kod,1,7) as kod, grup, articul, mod, annId FROM sp_articul WHERE annID = @annId";//kod as trueKod, SUBSTRING(kod,1,7) as kod
            ? "SELECT DISTINCT SUBSTRING(kod,1,7) as kod, grup, articul, mod, annId FROM sp_articul WHERE annID IS NULL" //"EXEC dbo.GetNZPByKoddRT @annId"
            : "EXEC dbo.GetNZPByKoddRT @annId";

            object result = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@annId", annId } });
            return (DataTable)result;
        }

        //public async Task<List<MyDataART>> GetUnboundArt()
        //{

        //}
        /// <summary>
        /// Получение пути к файлу изображения
        /// </summary>
        /// <param name="kod">код</param>
        /// <returns></returns>
        public async Task<DataTable> GetImage(int kod)
        {
            string query = "select dbo.getFileEskizForKodd(@kod) as pathpict ";
            DataTable result =  await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@kod", kod } });
            return result;
        }

        /// <summary>
        /// Проверяет, есть ли незавершенное производство (НЗП) для указанного разделения труда.
        /// </summary>
        /// <param name="annId">Идентификатор разделения труда</param>
        /// <returns>True, если есть НЗП, иначе False</returns>
        public bool CheckNZP(int annId)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM norm_rasz WHERE annId = @annId AND kolNZP > 0";
                //return true; //
                return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } }).Rows.Count > 0;
            }
            catch { return false; }
          //  return false;
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

        internal async Task<int> InsertANN(ArtNormN row)
        {
            //kod, grup, articul, mod, po, sek_shv, sek_vyaz3, sek_vyaz5, sek_vyaz6, sek_vyaz7, sek_vyaz10, sek_vyaz12, sek_vyaz62, sek_vyaz71, sek_vyaz72, sek_vyazo, sek_vyaz, sek, seb, st, po1, komment, data_sozd, diz, constr, data_obn, sek_vyaz70, sek_kr, slogn, sek_vyaz14, arh, sql_pr_add, date_add, komp_name, annDateDel, annCompDel, annDateAdd, annCompAdd, status) 
            string query = @"INSERT INTO art_norm_n (kod, grup, articul, mod, sek_shv, sek_vyaz5, sek_vyaz6, sek_vyaz7, sek_vyaz10, sek_vyaz12, sek_vyazo, sek_vyaz, sek, komment, data_sozd, diz, constr, data_obn, sek_kr, slogn, arh, status) 
              
OUTPUT INSERTED.annID 
              VALUES (@kod, @grup, @articul, @mod, @sek_shv, @sek_vyaz5, @sek_vyaz6, @sek_vyaz7, @sek_vyaz10, @sek_vyaz12, @sek_vyazo, @sek_vyaz, @sek, @komment, @data_sozd, @diz, @constr, @data_obn, @sek_kr, @slogn, @arh, @status)";
            Dictionary<string, object> D = new Dictionary<string, object>  {
                {"@kod", row.Kod},
                {"@grup", row.Group},
                {"@articul", row.Articul},
                {"@mod", row.Mod},
                {"@sek_shv", row.SekShv},
                {"@sek_vyaz5", row.SekVyaz5},
                {"@sek_vyaz6", row.SekVyaz6},
                {"@sek_vyaz7", row.SekVyaz7},
                {"@sek_vyaz10", row.SekVyaz10},
                {"@sek_vyaz12", row.SekVyaz12},
                {"@sek_vyazo", row.SekVyazo},
                {"@sek_vyaz", row.SekVyaz},
                {"@sek", row.Sek},
                {"@komment", row.Komment},
                {"@data_sozd", row.DataSozd},
                {"@diz", row.Diz},
                {"@constr", row.Constr},
     {"@data_obn", DateTime.Now },//row.DataObn },
     {"@sek_kr", row.SekKr },
     {"@slogn", row.Slogn },
     {"@arh" , row.Arh},
                {"@status", row.Status } };
            object result = await _dbHelper.ExecuteScalarAsync(query, D);

            return result != null ? Convert.ToInt32(result) : -1;            //query = "SELECT * FROM sp_articul WHERE kod = @kod";
            //return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@kod", kod } }).Rows[0];

        }

        internal async Task deleteRow(string tableName, int Id)
        {
            string query = $"DELETE FROM {tableName} WHERE annID = {Id}";
            //Dictionary<string, object> parametres = new Dictionary<string, object>{ {"@tableName", tableName }, {"@annID", Id}};
            await _dbHelper.ExecuteNonQueryAsync(query);

        }
        public int SaveCopyToDatabase(ArtNormN newItem)
        {
            try
            {
                string query = @"
            INSERT INTO art_norm_n (kod, grup, articul, mod, sek_shv, sek_vyaz5, sek_vyaz6, sek_vyaz7, sek_vyaz10, sek_vyaz12, sek_vyazo, sek_vyaz, sek, komment, data_sozd, diz, constr, data_obn, sek_kr, slogn, arh, status) 
            OUTPUT INSERTED.annID
            VALUES (@kod, @grup, @articul, @mod, @sek_shv, @sek_vyaz5, @sek_vyaz6, @sek_vyaz7, @sek_vyaz10, @sek_vyaz12, @sek_vyazo, @sek_vyaz, @sek, @komment, @data_sozd, @diz, @constr, @data_obn, @sek_kr, @slogn, @arh, @status)";

                Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            {"@kod", newItem.Kod},
            {"@grup", newItem.Group},
            {"@articul", newItem.Articul},
            {"@mod", newItem.Mod},
            {"@sek_shv", newItem.SekShv},
            {"@sek_vyaz5", newItem.SekVyaz5},
            {"@sek_vyaz6", newItem.SekVyaz6},
            {"@sek_vyaz7", newItem.SekVyaz7},
            {"@sek_vyaz10", newItem.SekVyaz10},
            {"@sek_vyaz12", newItem.SekVyaz12},
            {"@sek_vyazo", newItem.SekVyazo},
            {"@sek_vyaz", newItem.SekVyaz},
            {"@sek", newItem.Sek},
            {"@komment", newItem.Komment},
            {"@data_sozd", newItem.DataSozd},
            {"@diz", newItem.Diz},
            {"@constr", newItem.Constr},
            {"@data_obn", newItem.DataObn ?? (object)DBNull.Value},
            {"@sek_kr", newItem.SekKr},
            {"@slogn", newItem.Slogn},
            {"@arh", newItem.Arh},
            {"@status", newItem.Status}
        };
                object result = _dbHelper.ExecuteScalar(query, parameters);
                return result != null ? Convert.ToInt32(result) : -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        public async Task<int> InsertNormRaszAsync(NormRasz normRasz)
        {
            string query = @"INSERT INTO norm_rasz (annId, kod_o, text, spec, razryd, obor, kod_proizv, kod, n1, sek, kod_ob) 
                           OUTPUT INSERTED.nrId 
                           VALUES (@annId, @kod_o, @text, @spec, @razryd, @obor, @kod_proizv, @kod, @n1, @sek, @kod_ob)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@annId", normRasz.AnnId },
                { "@kod_o", normRasz.KodO },
                { "@text", normRasz.Text },
                { "@spec", normRasz.Spec },
                { "@razryd", normRasz.Razryad },
                { "@obor", normRasz.Obor },
                { "@kod_proizv", normRasz.KodProizv },
                { "@kod", normRasz.Kod },
                { "@n1", normRasz.N1 },
                { "@sek", normRasz.Sek },
                { "@kod_ob", normRasz.KodOb }
            };

            object result = await _dbHelper.ExecuteScalarAsync(query, parameters);
            return Convert.ToInt32(result);
        }

        public async Task<int> InsertNormRaskAsync(NormRask normRask)
        {
            // Ваш код для вставки в базу данных
            // Например, используя ADO.NET или Entity Framework
            string query = @"INSERT INTO norm_rask (annId, kod_o, text, razryd, obor, n1, sek, n, n_ch, seb, seb_s) 
                           OUTPUT INSERTED.Id 
                           VALUES (@annId, @kod_o, @text, @razryd, @obor, @n1, @sek, @n, @n_ch, @seb, @seb_s)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@annId", normRask.AnnId },
                { "@kod_o", normRask.KodO },
                { "@text", normRask.Text },
                { "@razryd", normRask.Razryad },
                { "@obor", normRask.Obor },
                { "@n1", normRask.N1 },
                { "@sek", normRask.Sek },
                { "@n", normRask.N },
                { "@n_ch", normRask.N_ch },
                { "@seb", normRask.Seb },
                { "@seb_s", normRask.Seb_s }
            };

            object result = await _dbHelper.ExecuteScalarAsync(query, parameters);
            return Convert.ToInt32(result);
        }

        public Task<DataTable> GetNormRask()
        {
            string query = "SELECT Id, annId, kod_o, text, razryd, obor, n1, sek, n, n_ch, seb, seb_s FROM norm_rask";
            return _dbHelper.ExecuteQueryAsync(query);
        }

        public Task<DataTable> GetNormRask(int annId)
        {
            string query = "SELECT Id, annId, kod_o, text, razryd, obor, n1, sek, n, n_ch, seb, seb_s FROM norm_rask WHERE annId = @annId";
            return _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@annId", annId } });
        }
        //public async Task<List<ArtNormN>> GetAll()
        //{
        //    string query = "SELECT * FROM art_norm_n";
        //    DataTable dt = _dbHelper.ExecuteQuery(query);
        //    List<ArtNormN> list = new List<ArtNormN>();

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        list.Add(new ArtNormN
        //        {
        //            AnnID = Convert.ToInt32(row["annID"]),
        //            Kod = row["kod"].ToString(),
        //            Grup = row["grup"].ToString(),
        //            Articul = row["articul"].ToString(),
        //            Mod = row["mod"].ToString(),
        //            SekShv = Convert.ToInt32(row["sek_shv"]),
        //            SekVyaz5 = Convert.ToInt32(row["sek_vyaz5"]),
        //            SekVyaz6 = Convert.ToInt32(row["sek_vyaz6"]),
        //            SekVyaz7 = Convert.ToInt32(row["sek_vyaz7"]),
        //            SekVyaz10 = Convert.ToInt32(row["sek_vyaz10"]),
        //            SekVyaz12 = Convert.ToInt32(row["sek_vyaz12"]),
        //            SekVyazo = Convert.ToInt32(row["sek_vyazo"]),
        //            SekVyaz = Convert.ToInt32(row["sek_vyaz"]),
        //            Sek = Convert.ToInt32(row["sek"]),
        //            Komment = row["komment"].ToString(),
        //            DataSozd = Convert.ToDateTime(row["data_sozd"]),
        //            Diz = Convert.ToInt32(row["diz"]),
        //            Constr = Convert.ToInt32(row["constr"]),
        //            DataObn = row["data_obn"] != DBNull.Value ? (DateTime?)row["data_obn"] : null,
        //            SekKr = Convert.ToInt32(row["sek_kr"]),
        //            Slogn = Convert.ToInt32(row["slogn"]),
        //            Arh = Convert.ToBoolean(row["arh"]),
        //            Status = Convert.ToInt32(row["status"])
        //        });
        //    }

        //    return list;
        //}

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

        public async Task SaveRaskroyNorm(RaskroyNorm norm)
        {
            try
            {
                string query = @"
                    UPDATE raskroy_norm 
                    SET dras1 = @dras1,
                        drez1 = @drez1,
                        dpro1 = @dpro1,
                        lras1 = @lras1,
                        lrez1 = @lrez1,
                        lpro1 = @lpro1,
                        dras2 = @dras2,
                        drez2 = @drez2,
                        dpro2 = @dpro2,
                        lras2 = @lras2,
                        lrez2 = @lrez2,
                        lpro2 = @lpro2,
                        dras3 = @dras3,
                        drez3 = @drez3,
                        dpro3 = @dpro3,
                        lras3 = @lras3,
                        lrez3 = @lrez3,
                        lpro3 = @lpro3
                    WHERE gr = @gr AND naimen = @naimen";

                var parameters = new Dictionary<string, object>
                {
                    { "@gr", norm.Gr },
                    { "@naimen", norm.Naimen },
                    { "@dras1", norm.Dras1 },
                    { "@drez1", norm.Drez1 },
                    { "@dpro1", norm.Dpro1 },
                    { "@lras1", norm.Lras1 },
                    { "@lrez1", norm.Lrez1 },
                    { "@lpro1", norm.Lpro1 },
                    { "@dras2", norm.Dras2 },
                    { "@drez2", norm.Drez2 },
                    { "@dpro2", norm.Dpro2 },
                    { "@lras2", norm.Lras2 },
                    { "@lrez2", norm.Lrez2 },
                    { "@lpro2", norm.Lpro2 },
                    { "@dras3", norm.Dras3 },
                    { "@drez3", norm.Drez3 },
                    { "@dpro3", norm.Dpro3 },
                    { "@lras3", norm.Lras3 },
                    { "@lrez3", norm.Lrez3 },
                    { "@lpro3", norm.Lpro3 }
                };

                await _dbHelper.ExecuteNonQueryAsync(query, parameters);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при сохранении данных в таблицу raskroy_norm");
                throw;
            }
        }

    }
}
