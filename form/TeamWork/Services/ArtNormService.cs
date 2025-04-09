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
using System.Drawing;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace SewingProduction.Services
{
    /// <summary>
    /// класс для обработки SQL
    /// </summary>
    public class ArtNormService
    {
        private readonly DatabaseHelper _dbHelper;
        //    private readonly HybridLogger _logger = new HybridLogger();
        private readonly FileLogger _logger = new FileLogger();
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

        

        private List<ArtNormN> ConvertToList(DataTable table)
        {
            List<ArtNormN> list = new List<ArtNormN>();

            foreach (DataRow row in table.Rows)
            {
                //var dateCreate = row["data_sozd"];
                //var dataUpdate = row["data_obn"];
                
                //_logger.LogEventAsync($"data_sozd value: {dateCreate}, type: {dateCreate?.GetType()}", "ConvertToList");
                //_logger.LogEventAsync($"data_obn value: {dataUpdate}, type: {dataUpdate?.GetType()}", "ConvertToList");

                list.Add(new ArtNormN
                {
                    Kod = row["kod"].ToString(),
                    AnnID = Convert.ToInt32(row["annId"]),
                    Group = row["grup"].ToString(),
                    Articul = row["articul"].ToString(),
                    Mod = row["mod"].ToString(),
                    Sek = row["sek"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek"]),
                    SekVyaz = row["sek_vyaz"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_vyaz"]),
                    dataUpdate = row["data_obn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["data_obn"]),
                    SekShv = row["sek_shv"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_shv"]),
                    StatusText = row["statusText"] == DBNull.Value ? "" : row["statusText"].ToString(),
                    Status = row["status"] == DBNull.Value ? 0 : Convert.ToInt32(row["status"]),
                    preArch = ((int)row["status"] == (int)Status.PreliminaryArchive) ? true : false,
                    SekVyazo = row["sek_vyazo"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_vyazo"]),
                    SekVyaz5 = row["sek_vyaz5"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_vyaz5"]),
                    SekVyaz7 = row["sek_vyaz7"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_vyaz7"]),
                    SekVyaz12 = row["sek_vyaz12"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_vyaz12"]),
                    SekVyaz10 = row["sek_vyaz10"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_vyaz10"]),
                    SekVyaz6 = row["sek_vyaz6"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_vyaz6"]),
                    SekKr = row["sek_kr"] == DBNull.Value ? 0 : Convert.ToInt32(row["sek_kr"]),
                    Slogn = row["slogn"] == DBNull.Value ? 0 : Convert.ToInt32(row["slogn"]),
                    Komment = row["komment"] == DBNull.Value ? "" : row["komment"].ToString(),
                    dateCreate = row["data_sozd"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["data_sozd"]),
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
            data_sozd, diz, constr FROM ArtNormNView JOIN status_ann ON status=status_id WHERE status!=3";

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
        public async Task ResetAnnIdinArticul(int kodd_rt)
        {
            // string query = "UPDATE sp_articul SET annId = NULL WHERE kod = @kod";
            string query = "UPDATE sp_articul SET annId = NULL WHERE kod IN (SELECT kod FROM view_sp_articul WHERE kodd_rt = @kod)";
           await _dbHelper.ExecuteNonQueryAsync(query, new Dictionary<string, object> { { "@kod", kodd_rt } });
        }

        public void UpdateAnnIdinArticul(int kod, int annId)
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
            string query = "SELECT AnnId, N, N1, Razryd as Rasryad, Text, Sek, Kod, kod_o as KodO, kod_ob as KodOb, kod_podr as KodPodr, kod_proizv as KodProizv, Seb, Spec, Obor, nrId FROM norm_rasz WHERE annId = @annId";
            //string query = "SELECT AnnId, N, N1, Razryd, Text, Sek, Kod, kod_o, kod_ob, nrId FROM normRaszView WHERE annId = @annId";
            return _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@annId", annId } });
        }
        public DataTable GetRelatedNormRasz1(int annId)
        {
            //string query = "SELECT AnnId, N, N1, Razryd as Rasryad, Text, Sek, Kod, kod_o as KodO, kod_ob as KodOb, nrId FROM norm_rasz WHERE annId = @annId";
            string query = "SELECT AnnId, N, N1, Razryd, Text, Sek, Kod, kod_o, kod_ob, nrId FROM normRaszView WHERE annId = @annId";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } });
            //.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@annId", annId } });
        }

        public Task<DataTable> GetRelatedNormRask(int annId)
        {
            string query = "SELECT id, AnnId, kod_o as KodO, Text, razryd as Razryad, Sek, Kod, Seb, N, n_ch as NCh, N1, seb_s as SebS, Obor FROM norm_rask WHERE annId = @annId";
            return _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@annId", annId } });
        }

        public Task<DataTable> GetRelatedNormKont(int annId)
        {
            string query = "SELECT AnnId, kod_o as KodO, Text, razryd as Razryad, Sek FROM norm_kont WHERE annId = @annId";
            return _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@annId", annId } });
        }

        public Task<DataTable> GetRelatedNormDopObr(int annId)
        {
            string query = "SELECT AnnId, sek_p as SekP, sek_p_tamp as SekTamp, sek_v as SekV, sek_stra as SekStra FROM norm_dop_obr WHERE annId = @annId";
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
            //try
            //{
            //    string query = "SELECT COUNT(*) FROM norm_rasz WHERE annId = @annId AND kolNZP > 0";
            //    //return true; //
            //    return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } }).Rows.Count > 0;
            //}
            //catch { return false; }
            return false;
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
                {"@data_sozd", row.dateCreate},
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
            {"@data_sozd", newItem.dateCreate},
            {"@diz", newItem.Diz},
            {"@constr", newItem.Constr},
            {"@data_obn", newItem.dataUpdate ?? (object)DBNull.Value},
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
        { "@kod_o", normRasz.Kod_o },
        { "@text", normRasz.Text },
        { "@spec", normRasz.Spec },
        { "@razryd", normRasz.Razryad },
        { "@obor", normRasz.Obor },
        { "@kod_proizv", normRasz.Kod_proizv },
                { "@kod", normRasz.Kod },
                { "@n1", normRasz.N1 },
                { "@sek", normRasz.Sek },
                { "@kod_ob", normRasz.Kod_ob }
            };

            object result = await _dbHelper.ExecuteScalarAsync(query, parameters);
            return Convert.ToInt32(result);
        }

        public async Task<int> InsertNormRaskAsync(NormRask normRask)
        {
            string query = @"INSERT INTO norm_rask (AnnId, Kod_o, Text, Razryd, Sek) 
                           OUTPUT INSERTED.Id 
                           VALUES (@AnnId, @Kod_o, @Text, @Razryd, @Sek)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@AnnId", normRask.AnnId },
                { "@Kod_o", normRask.KodO },
                { "@Text", normRask.Text },
                { "@Razryd", normRask.Razryad },
                { "@Sek", normRask.Sek }
            };

            object result = await _dbHelper.ExecuteScalarAsync(query, parameters);
            return Convert.ToInt32(result);
        }

        public async Task<int> InsertNormKontAsync(NormKont kont)
        {
            string query = "INSERT INTO norm_kont (AnnId, Kod_o, Text, Razryd, Sek) VALUES (@AnnId, @Kod_o, @Text, @Razryd, @Sek)";
            Dictionary<string, object> parametres = new Dictionary<string, object>
                        {
                    {"@AnnId", kont.AnnId },
                    {"@Kod_o", kont.KodO },
                    {"@Text", kont.Text },
                    {"@Razryd", kont.Razryad },
                    {"@Sek", kont.Sek }
                        };
            object result = await _dbHelper.ExecuteScalarAsync(query, parametres);
            return Convert.ToInt32(result);
        }

        public async Task<int> InsertDopObrAsync(NormDopObr dop)
        {
            string query = "INSERT INTO norm_dop_obr (AnnId, sek_p, sek_p_tamp, sek_v, sek_stra) VALUES (@AnnId, @SekP, @SekTamp, @SekV, @SekStra)";
                        Dictionary<string, object> parametres =  new Dictionary<string, object>
                        {
                    { "@AnnId", dop.AnnId },
                    { "@SekP", dop.SekP },
                    { "@SekTamp", dop.SekTamp },
                    { "@SekV", dop.SekV },
                    { "@SekStra", dop.SekStra }
                        };
            object result = await _dbHelper.ExecuteScalarAsync(query, parametres);
            return Convert.ToInt32(result);
        }
        public Task<DataTable> GetNormRask()
        {
            string query = "SELECT Id, annId, kod_o, text, razryd, obor, n1, sek, n, n_ch, seb, seb_s FROM norm_rask";
            return _dbHelper.ExecuteQueryAsync(query);
        }

        public Task<DataTable> GetNormRask(int annId)
        {
            string query = "SELECT * FROM norm_rask WHERE annId = @annId";
            return _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@annId", annId } });
        }
        
        /// <summary>
        /// Получает список объектов NormRasz для указанного annId
        /// </summary>
        /// <param name="annId">Идентификатор разделения труда</param>
        /// <returns>Список объектов NormRasz</returns>
        public async Task<List<NormRasz>> GetNormRaszList(int annId)
        {
            DataTable table = await GetRelatedNormRasz(annId);
            List<NormRasz> result = new List<NormRasz>();
            
            foreach (DataRow row in table.Rows)
            {
                NormRasz item = new NormRasz
                {
                    AnnId = Convert.ToInt32(row["annId"]),
                    N = row["n"] != DBNull.Value ? Convert.ToInt32(row["n"]) : 0,
                    N1 = row["n1"] != DBNull.Value ? Convert.ToInt32(row["n1"]) : 0,
                    Razryad = row["razryd"] != DBNull.Value ? Convert.ToInt32(row["razryd"]) : 0,
                    Text = row["text"] != DBNull.Value ? row["text"].ToString() : string.Empty,
                    Sek = row["sek"] != DBNull.Value ? Convert.ToInt32(row["sek"]) : 0,
                    Kod = row["kod"] != DBNull.Value ? Convert.ToInt32(row["kod"]) : 0,
                    Kod_o = row["kod_o"] != DBNull.Value ? Convert.ToInt32(row["kod_o"]) : 0,
                    Kod_ob = row["kod_ob"] != DBNull.Value ? Convert.ToInt32(row["kod_ob"]) : 0
                };
                result.Add(item);
            }
            
            return result;
        }
        
        /// <summary>
        /// Получает список объектов NormRask для указанного annId
        /// </summary>
        /// <param name="annId">Идентификатор разделения труда</param>
        /// <returns>Список объектов NormRask</returns>
        public async Task<List<NormRask>> GetNormRaskByAnnId(int annId)
        {
            DataTable table = await GetRelatedNormRask(annId);
            List<NormRask> result = new List<NormRask>();
            
            foreach (DataRow row in table.Rows)
            {
                NormRask item = new NormRask
                {
                    AnnId = Convert.ToInt32(row["annId"]),
                    KodO = row["kod_o"] != DBNull.Value ? row["kod_o"].ToString() : string.Empty,
                    Razryad = row["razryd"] != DBNull.Value ? Convert.ToInt32(row["razryd"]) : 0,
                    Text = row["text"] != DBNull.Value ? row["text"].ToString() : string.Empty,
                    Sek = row["sek"] != DBNull.Value ? Convert.ToInt32(row["sek"]) : 0
                };
                result.Add(item);
            }
            
            return result;
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

                DataTable result = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@annId", annId } });
                
                if (result != null && result.Rows.Count > 0)
                {
                    // Возвращаем первую запись
                    List<ArtNormN> list = ConvertToList(result);
                    if (list.Count > 0)
                    {
                        await _logger.LogEventAsync($"Успешно получены данные ArtNormN для ID {annId}", "GetArtNormDataById");
                        return list[0];
                    }
                }
                
                await _logger.LogEventAsync($"Не найдены данные ArtNormN для ID {annId}", "GetArtNormDataById");
                return null;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных ArtNormN для ID {annId}");
                return null;
            }
        }

        /// <summary>
        /// Обновляет значение поля ann для указанного ID
        /// </summary>
        /// <param name="annId">ID записи</param>
        /// <param name="fieldName">Имя поля</param>
        /// <param name="newValue">новое значение</param>
        /// <returns>Задача, представляющая асинхронную операцию</returns>
        /*public async Task UpdateAnnIdField(int annId, string fieldName, object newValue)
        {
            try
            {
                string query = $"UPDATE art_norm_n SET {fieldName} = @newValue WHERE annId = @annId";
                
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@annId", annId },
                    { "@newValue", newValue }
                };

                await _dbHelper.ExecuteNonQueryAsync(query, parameters);
                await _logger.LogEventAsync($"Поле {fieldName} для ID {annId} успешно обновлено значением {newValue}", "UpdateAnnField");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при обновлении поля {fieldName} для ID {annId}");
                throw;
            }
        }*/

        /// <summary>
        /// Обновляет annId в таблицах состава разделения труда
        /// </summary>
        /// <param name="tableName">Таблица</param>
        /// <param name="annId">старый annId</param>
        /// <param name="fieldName">имя поля (annId)</param>
        /// <param name="newValue">новый annId</param>
        /// <returns></returns>
        public async Task UpdateAnnId(string tableName, int annId, string fieldName, object newValue)
        {
            try
            {
                string query = $"UPDATE {tableName} SET {fieldName} = @newValue WHERE annId = @annId";

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@annId", annId },
                    { "@newValue", newValue }
                };

                await _dbHelper.ExecuteNonQueryAsync(query, parameters);
                await _logger.LogEventAsync($"Поле {fieldName} для ID {annId} успешно обновлено значением {newValue}", "UpdateAnnField");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при обновлении поля {fieldName} для ID {annId}");
                throw;
            }
        }

        /// <summary>
        /// Универсально обновляет одно поле в таблице по заданному условию.
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

        public async Task<int> InsertEntityAsync<T>(string tableName, string keyFieldName, T entity)
        {
            try
            {
                var properties = typeof(T).GetProperties()
                    .Where(p => p.CanRead &&
                                p.Name != keyFieldName &&
                                !System.Attribute.IsDefined(p, typeof(NotMappedAttribute))) 
                    .ToList();

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

                    columns.Add(columnName);
                    values.Add(parameterName);
                    parameters[parameterName] = NormalizeValue(prop.GetValue(entity));
                }

                string columnsPart = string.Join(", ", columns);
                string valuesPart = string.Join(", ", values);

                string query = $"INSERT INTO {tableName} ({columnsPart}) VALUES ({valuesPart}); SELECT SCOPE_IDENTITY();";

                object result = await _dbHelper.ExecuteScalarAsync(query, parameters);

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

        public async Task UpdateEntityAsync<T>(string tableName, string keyFieldName, T entity)
        {
            try
            {
                var properties = typeof(T).GetProperties()
                    .Where(p => p.CanRead &&
                                p.Name != keyFieldName &&
                                !System.Attribute.IsDefined(p, typeof(NotMappedAttribute))) // ⬅️ Пропуск NotMapped
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
                    parameters[parameterName] = NormalizeValue(prop.GetValue(entity));
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
        private object NormalizeValue(object value)
        {
            if (value == null)
                return DBNull.Value;

            if (value is DateTime dt)
            {
                if (dt < new DateTime(1753, 1, 1))
                {
                    return DBNull.Value; 
                }
            }

            return value;
        }


        /*public async Task UpdateAnnAsync(ArtNormN annData)
        {
            try
            {
                var query = @"
                    UPDATE art_norm_n 
                    SET articul = @Articul,
                        grup = @Group,
                        mod = @Mod,
                        sek = @Sek,
                        diz = @Diz,
                        constr = @Constr,
                        data_obn = GETDATE()
                    WHERE annid = @AnnID";

                var parameters = new Dictionary<string, object>
                {
                    { "@AnnID", annData.AnnID },
                    { "@Articul", annData.Articul },
                    { "@Group", annData.Group },
                    { "@Mod", annData.Mod },
                    { "@Sek", annData.Sek },
                    { "@Diz", annData.Diz },
                    { "@Constr", annData.Constr }
                };

                await _dbHelper.ExecuteNonQueryAsync(query, parameters);
                await _logger.LogEventAsync($"Данные ANN успешно обновлены для ID {annData.AnnID}", "UpdateAnnAsync");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при обновлении данных ANN для ID {annData.AnnID}");
                throw;
            }
        }*/

        internal async Task<DataTable> GetKod_proizv()
        {
            string query = "select kod_proizv, text_proizv from kod_proizv";
            return await _dbHelper.ExecuteQueryAsync(query);
            
        }

        internal async Task<DataTable> GetPodr_vyaz()
        {
            string query = "select kod_vyaz, text_vyaz from podr_vyaz";
            return await _dbHelper.ExecuteQueryAsync(query);
        }

        internal async Task<DataTable> GetOborud_shv()
        {
            string query = "select kod_ob, text_ob from oborud_shv";
            return await _dbHelper.ExecuteQueryAsync(query);
        }

        public async Task CopyTableRecords(string tableName, int sourceAnnId, int targetAnnId)
        {
            try
            {
                // Сначала получаем список колонок таблицы, исключая identity колонки
                string columnsQuery = $@"
                    SELECT COLUMN_NAME 
                    FROM INFORMATION_SCHEMA.COLUMNS 
                    WHERE TABLE_NAME = @tableName 
                    AND COLUMNPROPERTY(OBJECT_ID(@tableName), COLUMN_NAME, 'IsIdentity') = 0";

                var columnsResult = await _dbHelper.ExecuteQueryAsync(columnsQuery, new Dictionary<string, object> { { "@tableName", tableName } });
                
                var columns = new List<string>();
                foreach (DataRow row in columnsResult.Rows)
                {
                    columns.Add(row["COLUMN_NAME"].ToString());
                }

                string columnsList = string.Join(", ", columns);

                // Затем выполняем INSERT INTO ... SELECT с полученными колонками
                string query = $@"
                    INSERT INTO {tableName} ({columnsList})
                    SELECT {columnsList}
                    FROM {tableName}
                    WHERE annId = @sourceAnnId";

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@sourceAnnId", sourceAnnId }
                };

                await _dbHelper.ExecuteNonQueryAsync(query, parameters);

                // Обновляем annId в новых записях
                string updateQuery = $@"
                    UPDATE {tableName} 
                    SET annId = @targetAnnId 
                    WHERE annId = @sourceAnnId";

                parameters = new Dictionary<string, object>
                {
                    { "@sourceAnnId", sourceAnnId },
                    { "@targetAnnId", targetAnnId }
                };

                await _dbHelper.ExecuteNonQueryAsync(updateQuery, parameters);
                await _logger.LogEventAsync($"Записи из таблицы {tableName} успешно скопированы из ID={sourceAnnId} в ID={targetAnnId}", "CopyTableRecords");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при копировании записей из таблицы {tableName}");
                throw;
            }
        }
    }
}
