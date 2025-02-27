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

        public DataTable GetArtNormData()
        {
            string query =  $" SELECT SUBSTRING(kod,1,7) as kod, annId, grup, articul, mod, sek, sek_vyaz, data_obn, sek_shv, status_ann.name AS stat," +
                            $" status, sek_vyazo, sek_vyaz5, sek_vyaz7, sek_vyaz12, sek_vyaz10, sek_vyaz6, sek_kr, slogn, komment, data_sozd, diz, constr " +
                            $"    FROM ArtNormNView " +
                            $" JOIN status_ann ON status=status_id";
            return _dbHelper.ExecuteQuery(query);
        }
        /// <summary>
        /// загрузка артикулов для увязки. Статус != архивное
        /// </summary>
        /// <returns>Возвращает таблицу артикулов</returns>
        public DataTable GetArtNormDataCurrent(int kod, bool all)
        {
            string query = "";
            if (all)
            {
                query = $" SELECT annId, kod, grup, articul, mod, sek, sek_vyaz, data_obn, sek_shv, status_ann.name AS stat, status, " +
                        $" sek_vyazo, sek_vyaz5, sek_vyaz7, sek_vyaz12, sek_vyaz10, sek_vyaz6, sek_kr, slogn, komment, data_sozd, diz, constr " +
                        $"     FROM ArtNormNView " +
                        $" JOIN status_ann ON status=status_id WHERE status<3";
            }
            else
            {
                query = $"SELECT annId, kod, grup, articul, mod, sek, sek_vyaz, data_obn, sek_shv, status_ann.name AS stat, status, sek_vyazo, " +
                        $" sek_vyaz5, sek_vyaz7, sek_vyaz12, sek_vyaz10, sek_vyaz6, sek_kr, slogn, komment, data_sozd, diz, constr " +
                        $"     FROM artNormNView " +
                        $" JOIN status_ann ON status=status_id " +
                        $"     WHERE (status<3) " +
                        $" AND (annId IN (SELECT annId FROM View_sp_articul WHERE kodd_rt = '@kod'))";
            }
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "kod", kod } });
        }

        public DataTable GetArtNormDataCurrent(string art)
        {
            string query = $"SELECT * FROM artNormNView WHERE status<{Status.Archive} AND articul IN (SELECT articul FROM View_sp_articul WHERE articul LIKE @art)";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "art", art + "%" } });
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

        public DataTable GetRelatedNormRasz(int annId)
        {
            string query = "SELECT annId, n, n1, razryd, text, sek, kod, kod_o, kod_ob FROM norm_rasz WHERE annId = @annId";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } });
        }
        public DataTable GetRelatedNormRask(int annId)
        {
            string query = "SELECT annId, kod_o, razryd, text, sek  FROM norm_rask WHERE annId = @annId";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } });
        }

        public DataTable GetRelatedNormKont(int annId)
        {
            string query = "SELECT annId, kod_o, razryd, text, sek FROM norm_kont WHERE annId = @annId";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } });
        }

        public DataTable GetRelatedNormDopObr(int annId)
        {
            string query = "SELECT annId, sek_p, sek_p_tamp, sek_v, sek_stra FROM norm_dop_obr WHERE annId = @annId";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } });
        }

        public DataTable GetRelDesigner()
        {

            string query = "SELECT fio, tab FROM fio";
            return _dbHelper.ExecuteQuery(query);
        }

        /// <summary>
        /// Получение связанных данных из sp_articul
        /// </summary>
        /// <param name="annId">annId=null=> загрузка неувязанных артикулов
        /// annId!=null => загрузка артикулов с НЗП процедурой GetNZPByKoddRT 
        /// </param>
        /// <returns></returns>
        public DataTable GetRelatedSpArt(int annId)
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


            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@annId", annId } });
        }
        /// <summary>
        /// Получение пути к файлу изображения
        /// </summary>
        /// <param name="kod">код</param>
        /// <returns></returns>
        public DataTable GetImage(int kod)
        {
            string query = "select dbo.getFileEskizForKodd(@kod) as pathpict ";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@kod", kod } });
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
        }

        internal DataTable GetNormOper(int i)
        {
            string query = "SELECT * FROM dbo.norm_oper";
            return _dbHelper.ExecuteQuery(query);
        }

        internal int InsertANN(DataRow row)
        {
            //kod, grup, articul, mod, po, sek_shv, sek_vyaz3, sek_vyaz5, sek_vyaz6, sek_vyaz7, sek_vyaz10, sek_vyaz12, sek_vyaz62, sek_vyaz71, sek_vyaz72, sek_vyazo, sek_vyaz, sek, seb, st, po1, komment, data_sozd, diz, constr, data_obn, sek_vyaz70, sek_kr, slogn, sek_vyaz14, arh, sql_pr_add, date_add, komp_name, annDateDel, annCompDel, annDateAdd, annCompAdd, status) 
            string query = @"INSERT INTO art_norm_n (kod, grup, articul, mod, sek_shv, sek_vyaz5, sek_vyaz6, sek_vyaz7, sek_vyaz10, sek_vyaz12, sek_vyazo, sek_vyaz, sek, komment, data_sozd, diz, constr, data_obn, sek_kr, slogn, arh, status) 
              
OUTPUT INSERTED.annID 
              VALUES (@kod, @grup, @articul, @mod, @sek_shv, @sek_vyaz5, @sek_vyaz6, @sek_vyaz7, @sek_vyaz10, @sek_vyaz12, @sek_vyazo, @sek_vyaz, @sek, @komment, @data_sozd, @diz, @constr, @data_obn, @sek_kr, @slogn, @arh, @status)";
            Dictionary<string, object> D = new Dictionary<string, object>  {
                {"@kod", row["kod"]},
                {"@grup", row["grup"]},
                {"@articul", row["articul"]},
                {"@mod", row["mod"]},
                {"@sek_shv", row["sek_shv"]},
                {"@sek_vyaz5", row["sek_vyaz5"]},
                {"@sek_vyaz6", row["sek_vyaz6"]},
                {"@sek_vyaz7", row["sek_vyaz7"]},
                {"@sek_vyaz10", row["sek_vyaz10"]},
                {"@sek_vyaz12", row["sek_vyaz12"]},
                {"@sek_vyazo", row["sek_vyazo"]},
                {"@sek_vyaz", row["sek_vyaz"]},
                {"@sek", row["sek"]},
                {"@komment", row["komment"]},
                {"@data_sozd", row["data_sozd"]},
                {"@diz", row["diz"]},
                {"@constr", row["constr"]},
     {"@data_obn", row["data_obn"] },
     {"@sek_kr", row["sek_kr"] },
     {"@slogn", row["slogn"] },
     {"@arh" , row["arh"]},
                {"@status", row["status"] } };
            return _dbHelper.ExecuteScalar(query, D);
            //query = "SELECT * FROM sp_articul WHERE kod = @kod";
            //return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@kod", kod } }).Rows[0];

        }
    }
}
