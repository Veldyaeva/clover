using System.Collections.Generic;
using DataTable = System.Data.DataTable;


namespace SewingProduction.form
{
    /// <summary>
    /// класс для обработки SQL
    /// </summary>
    public class ArtNormService
    {
        private readonly DatabaseHelper _dbHelper;

        public ArtNormService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public DataTable GetArtNormData()
        {
            string query = "SELECT SUBSTRING(kod,1,7) as kod, annId, grup, articul, mod, sek, sek_vyaz, data_obn, sek_shv, status_ann.name AS stat, status, sek_vyazo, sek_vyaz5, sek_vyaz7, sek_vyaz12, sek_vyaz10, sek_vyaz6, sek_kr, slogn, komment, data_sozd, diz, constr FROM ArtNormNView JOIN status_ann ON status=status_id";
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
                query = "SELECT annId, kod, grup, articul, mod, sek, sek_vyaz, data_obn, sek_shv, status_ann.name AS stat, status, sek_vyazo, sek_vyaz5, sek_vyaz7, sek_vyaz12, sek_vyaz10, sek_vyaz6, sek_kr, slogn, komment, data_sozd, diz, constr FROM ArtNormNView JOIN status_ann ON status=status_id WHERE status<3";
            }
            else
            {
                query = "SELECT annId, kod, grup, articul, mod, sek, sek_vyaz, data_obn, sek_shv, status_ann.name AS stat, status, sek_vyazo, sek_vyaz5, sek_vyaz7, sek_vyaz12, sek_vyaz10, sek_vyaz6, sek_kr, slogn, komment, data_sozd, diz, constr FROM artNormNView JOIN status_ann ON status=status_id WHERE (status<3) AND (annId IN (SELECT annId FROM View_sp_articul WHERE kodd_rt = '@kod'))";
            }
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "kod", kod } });
        }

        public DataTable GetArtNormDataCurrent(string art)
        {
            string query = "SELECT * FROM artNormNView WHERE status<3 AND articul IN (SELECT articul FROM View_sp_articul WHERE articul LIKE @art)";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "art", art + "%" } });
        }
        public void ResetAnnId(int spArticul)
        {
            // string query = "UPDATE sp_articul SET annId = NULL WHERE kod = @kod";
            string query = "UPDATE sp_articul SET annId = NULL WHERE kod IN (SELECT kod FROM view_sp_articul WHERE kodd_rt = @kod)";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@kod", spArticul } });
        }

        public void UpdateAnnId(int spArticul, int annId)
        {
            string query = "UPDATE sp_articul SET annId = @annId WHERE kod like @kod";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@kod", spArticul + "%" }, { "@annId", annId } });
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

        public DataTable GetRelDesigner(int tab)
        {

            string query = "SELECT fio, tab FROM fio where tab = @tab";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@tab", tab } });
        }

        /// <summary>
        /// Получение связанных данных из sp_articul
        /// </summary>
        /// <param name="annId">annId=null=> загрузка неувязанных артикулов
        /// annId!=null => загрузка артикулов с НЗП процедурой GetNZPByKoddRT 
        /// </param>
        /// <returns></returns>
        public DataTable GetRelatedspArt(int annId)
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


    }
}
