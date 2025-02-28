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
using SewingProduction.form.TeamWork.Models;
using SewingProduction.BdContext;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace SewingProduction.Services
{
    /// <summary>
    /// класс для обработки SQL
    /// </summary>
    public class ArtNormService
    {
        private readonly DatabaseHelper _dbHelper;

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
                list.Add(new ArtNormN
                {
                    Kod = row["kod"].ToString(),
                    AnnID = Convert.ToInt32(row["annId"]),
                    Grup = row["grup"].ToString(),
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
        public Task<DataTable> GetArtNormDataCurrent(int kod, bool all)
        {
            string query = "";
            if (all)
            {
                query = "SELECT annId, kod, grup, articul, mod, sek, sek_vyaz, data_obn, sek_shv, status_ann.name AS stat, status, sek_vyazo, sek_vyaz5, sek_vyaz7, sek_vyaz12, sek_vyaz10, sek_vyaz6, sek_kr, slogn, komment, data_sozd, diz, constr FROM ArtNormNView JOIN status_ann ON status=status_id WHERE status<3";
            }
            else
            {
                query = "SELECT annId, kod, grup, articul, mod, sek, sek_vyaz, data_obn, sek_shv, status_ann.name AS stat, status, sek_vyazo, sek_vyaz5, sek_vyaz7, sek_vyaz12, sek_vyaz10, sek_vyaz6, sek_kr, slogn, komment, data_sozd, diz, constr FROM artNormNView JOIN status_ann ON status=status_id WHERE (status<3) AND (annId IN (SELECT annId FROM View_sp_articul WHERE kodd_rt = '@kod'))";
            }
            return _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "kod", kod } });
        }

        public Task<DataTable> GetArtNormDataCurrent(string art)
        {
            string query = $"SELECT * FROM artNormNView WHERE status<{Status.Archive} AND articul IN (SELECT articul FROM View_sp_articul WHERE articul LIKE @art)";
            return _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "art", art + "%" } });
        }
        public void ResetAnnId(int kodd_rt)
        {
            // string query = "UPDATE sp_articul SET annId = NULL WHERE kod = @kod";
            string query = "UPDATE sp_articul SET annId = NULL WHERE kod IN (SELECT kod FROM view_sp_articul WHERE kodd_rt = @kod)";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@kod", kodd_rt } });
        }

        public void UpdateAnnId(int kod, int annId)
        {
            string query = "UPDATE sp_articul SET annId = @annId WHERE kod like @kod";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@kod", kod + "%" }, { "@annId", annId } });
        }

        // Получение связанных данных

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
        public Task<DataTable> GetRelatedSpArt(int annId)
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


            return _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@annId", annId } });
        }
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
            string query = "SELECT * FROM dbo.norm_oper";
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
                {"@grup", row.Grup},
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
     {"@data_obn", row.DataObn },
     {"@sek_kr", row.SekKr },
     {"@slogn", row.Slogn },
     {"@arh" , row.Arh},
                {"@status", row.Status } };
            object result = await _dbHelper.ExecuteScalar(query, D);

            return result != null ? Convert.ToInt32(result) : -1;            //query = "SELECT * FROM sp_articul WHERE kod = @kod";
            //return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@kod", kod } }).Rows[0];

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
            {"@grup", newItem.Grup},
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

    }
}
