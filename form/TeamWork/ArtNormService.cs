using System;
using System.Collections.Generic;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraEditors;
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
        #region art_new2024
        public DataTable GetGostUst()
        {
            string query = "SELECT id_gost AS 'ИД' ,name_gost AS 'Имя' ,TRIM(opi_gost) AS 'Описание' FROM gost WHERE ust=1";
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetGostSvPictAndArticulGrup()
        {
            string query = $"SELECT TRIM(ag_naimen) AS 'Наименование' FROM gost_sv_pict,articul_grup  WHERE articul_grup.ag_id=gost_sv_pict.id_art ";
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetViewTovarMarka()
        {
            string query = "SELECT TRIM(kodsp) AS kle, TRIM(m_naimen) AS 'Наименование' FROM dbo.view_tovar_marka WHERE tmOwn = 1 ";
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetViewGrupMen()
        {
            string query = "SELECT men_id, TRIM(name) AS 'Наименование' FROM view_grup_men WHERE men_id >0 order by men_id ";
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetGostSvRazmerAndGostRazmer()
        {
            string query = "SELECT DISTINCT TRIM(razm) AS 'Размер' FROM gost_sv_razmer, gost_razmer WHERE gost_sv_razmer.id_razmer=gost_razmer.id_rost ";
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetTovarCatDynsign()
        {
            string query = "SELECT tcds_name AS 'Признак' FROM TOVAR_CAT_DYNSIGN WHERE tcds_tcat_id in (886,895) ORDER BY TCDS_NAME ";
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetGostSvPictAndArticulGrupWhere(string opiGost)
        {
            string condition = string.IsNullOrWhiteSpace(opiGost) ? "" : $" AND id_gost = (SELECT id_gost FROM gost WHERE ust=1 AND opi_gost = '{opiGost}')";
            string query = $"SELECT TRIM(ag_naimen) AS 'Наименование' FROM gost_sv_pict,articul_grup  WHERE articul_grup.ag_id=gost_sv_pict.id_art" + condition;
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetGostSvRazmerAndGostRazmerWhere(string opiGost)
        {
            string condition = string.IsNullOrWhiteSpace(opiGost) ? "" : $" AND id_gost = (SELECT id_gost FROM gost WHERE ust=1 AND opi_gost = '{opiGost}')";
            string query = $"SELECT DISTINCT TRIM(razm) AS 'Размер' FROM gost_sv_razmer, gost_razmer WHERE gost_sv_razmer.id_razmer=gost_razmer.id_rost" + condition;
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetSpArticulKod(string kodSQL)
        {
            string query = $"SELECT kod, articul, razm AS 'Размер', kle, mod, grup, ag_id, kod_tnved, CAST(grupp AS INT) AS men_id FROM sp_articul WHERE kod = '@kodSQL'";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@kodSQL", kodSQL } });
        }
        #endregion

        #region fio
        public DataTable GetFioAndSpFirmsAndBrigObjectAndSpBrigAndSpisok1c()
        {
            string query = $@"SELECT fio.tab,       fio.fio,       fio.rab,    fio.ved,       fio.ftabn,
                                     fio.ftabnsort, fio.fgrd,      fio.data_p, fio.datau,     fio.bday,
                                     fio.tel_s,     fio.f_fvr_kod, fio.tab1c,  fio.tab_sovm,  fio.tel_r,
                                     fio.tel_d,     fio.mast,      fio.okl,    fio.tab_new,   
                                     fio.sovm,      fio.sdel,      fio.itr,    fio.dekret,
                                     sp_firms.name AS firms_name,
                                     sp_firms.frm_1c_inn,
                                     brig_object.name AS BRIG_object_name,
                                     spbrig.podrname1c AS podr1cname,
                                     spisok1c.id AS spisok1c_id,
                                     spisok1c.inn AS spisok1c_inn,
                                     spisok1c.orgName AS spisok1c_orgName,
                                     spisok1c.podrName AS spisok1c_podrName
                                FROM fio
                                LEFT JOIN sp_firms ON sp_firms.kod = fio.mast
                                LEFT JOIN brig_object ON brig_object.gr = fio.gr
                                LEFT JOIN spbrig ON spbrig.podrid1c = fio.podr_1c_id AND spbrig.podrid1c LIKE '%ЭЙС%'
                                LEFT JOIN spisok1c ON TRY_CAST(REPLACE(spisok1c.tab1c, ' ', '') AS INT) = CAST(fio.tab1c AS INT) AND spisok1c.orgcode = sp_firms.frm_1c_inn
                                ORDER BY fio.tab ASC";
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetBrigVed()
        {
            string query = "SELECT * FROM brig_ved";
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetFioDolgnPodr1c(string selectValue)
        {
            string query;
            switch (selectValue)
            {
                case "Организация":
                    query = "SELECT name AS 'value' FROM fio_dolgn_podr1c  WHERE ISNULL(name,'0') <> '0' group by name order by name";
                    break;
                case "Подразделение 1С":
                    query = "SELECT podrname1c AS 'value' FROM fio_dolgn_podr1c  WHERE ISNULL(podrname1c,'0') <> '0' group by podrname1c order by podrname1c";
                    break;
                case "Табель ШП":
                    query = "SELECT naimen AS 'value' FROM fio_dolgn_podr1c  WHERE ISNULL(naimen,'0') <> '0' group by naimen order by naimen";
                    break;
                default:
                    query = "SELECT * FROM fio_dolgn_podr1c";
                    break;
            }
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetFioAndSpFirmsAndTabN()
        {
            string query = $@"SELECT tss.tab_sovm,
                                    CASE 
                                        WHEN (minDataU IS NOT NULL AND maxDataU IS NOT NULL) THEN 'oaieai' 
                                        ELSE SPACE(6)
                                    END AS status,
                                    f.tab, f.fio, f.rab, f.sovm,
                                    f.datau, f.itr, tn.naimen, frm.name
                                FROM (
                                    SELECT tab_sovm, MIN(datau) as minDataU, MAX(datau) as maxDataU
                                    FROM fio
                                    WHERE tab_sovm <> 0
                                    GROUP BY tab_sovm
                                    HAVING COUNT(*) > 1
                                ) AS tss
                            LEFT JOIN fio f ON tss.tab_sovm = f.tab_sovm
                            LEFT JOIN sp_firms frm ON f.mast = frm.kod
                            LEFT JOIN tab_n tn ON f.ftabn = tn.tnID
                            ORDER BY tss.tab_sovm, f.sovm, f.tab";
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetFioDolgnPogr1cWhere(string selectValue, string selectOtch, bool checkBoxOsnTab)
        {
            string query = $@"SELECT 
                                    fio AS 'Фио',
                                    tab AS 'Таб. №',
                                    rab AS 'Должность',
                                    CASE 
                                        WHEN fio_dolgn_podr1c.sovm = 0 THEN 'Основной'
                                        ELSE ''
                                    END AS 'Основное место работы',
                                    CASE 
                                        WHEN fio_dolgn_podr1c.sdel = 1 THEN 'Сделка'
                                        ELSE 'ИТР'
                                    END AS 'ИТР/Сделка' 
                                FROM  fio_dolgn_podr1c where";
            if (checkBoxOsnTab)
                query += $" sovm = 0 and ";
            switch (selectValue)
            {
                case "Организация":
                    query += $" name = '{selectOtch}' ";
                    break;
                case "Подразделение 1С":
                    query += $" podrname1c = '{selectOtch}' ";
                    break;
                case "Табель ШП":
                    query += $" naimen = '{selectOtch}' ";
                    break;
                default:
                    query = " 1=1 ";
                    break;
            }
            query += $" order by name,podrname1c,naimen,fio,tab";
            return _dbHelper.ExecuteQuery(query);
        }
        public void UpdateFioSovm(string tab)
        {
            string query = "UPDATE fio SET tab_sovm = @tab_sovm WHERE fio.tab=@tab";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@tab_sovm", tab}, { "@tab", tab } });
        }
        /// <summary>
        ///  Получение нового табельного номера
        /// </summary>
        /// <param name="sqlFio">Максимальный табельный</param>
        /// <returns></returns>
        public int GetFioNewTab()
        {
            string query = "SELECT ISNULL(MAX(tab), 0) + 1 AS newTab FROM fio";
            DataTable sqlFio = _dbHelper.ExecuteQuery(query);
            return Convert.ToInt32(sqlFio.Rows[0]["newTab"]);
        }
        public DataTable GetFioForXML(string tab)
        {
            string query = "SELECT * FROM fio WHERE fio.tab=@tab";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@tab", tab } });
        }
        public void SetFioInsertTableXml(string xmlFio)
        {
            string query = "exec Insert_table_xml @xmlp = @xmlFio, @nameTable = 'fio'";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@xmlFio", xmlFio } });
        }
        #endregion
        #region oborudBrig
        public DataTable GetZehListFromOborudBrig()
        {
            string query = $@"SELECT nameZeh AS 'Цех', nameProizv AS 'Вид производства', address AS 'Адрес' 
                                          FROM ZehList
                                          LEFT JOIN spVidProizv ON spVidProizv.idProizv = ZehList.idProizv";
            return _dbHelper.ExecuteQuery(query);
        }
        public DataTable GetSpBrigFromOborudBrig(string nameZeh, int countVievZeh)
        {
            string query = $@"SELECT n_brig AS 'Номер', brig AS 'Бригада' FROM spBrig WHERE idZeh ";
            if (countVievZeh < 1)
                query += " IS NOT NULL";
            else
                if (nameZeh == null)
                query += " IS NOT NULL";
            else
                query += " = (SELECT idZeh FROM ZehList WHERE nameZeh = @nameZeh)";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@nameZeh", nameZeh } });
        }


        #endregion
    }
}
