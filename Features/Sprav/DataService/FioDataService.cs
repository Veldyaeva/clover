using System;
using System.Collections.Generic;
using SewingProduction.Helpers;

namespace SewingProduction.Features.Sprav
{
    public class FioDataService
    {
        private readonly DatabaseHelper _dbHelper;
        public FioDataService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        #region fio
        public System.Data.DataTable GetFioTable()
        {
            //string query = $@"SELECT fio.tab,       fio.fio,       fio.rab,    fio.ved,       fio.ftabn,
            //                         fio.ftabnsort, fio.fgrd,      fio.data_p, fio.datau,     fio.bday,
            //                         fio.tel_s,     fio.f_fvr_kod, fio.tab1c,  fio.tab_sovm,  fio.tel_r,
            //                         fio.tel_d,     fio.mast,      fio.okl,    fio.tab_new,   
            //                         fio.sovm,      fio.sdel,      fio.itr,    fio.dekret,
            //                         sp_firms.name AS firms_name,
            //                         sp_firms.frm_1c_inn,
            //                         brig_object.name AS BRIG_object_name,
            //                         spbrig.podrname1c AS podr1cname,
            //                         spisok1c.id AS spisok1c_id,
            //                         spisok1c.inn AS spisok1c_inn,
            //                         spisok1c.orgName AS spisok1c_orgName,
            //                         spisok1c.podrName AS spisok1c_podrName
            //                    FROM fio
            //                    LEFT JOIN sp_firms ON sp_firms.kod = fio.mast
            //                    LEFT JOIN brig_object ON brig_object.gr = fio.gr
            //                    LEFT JOIN spbrig ON spbrig.podrid1c = fio.podr_1c_id AND spbrig.podrid1c LIKE '%ЭЙС%'
            //                    LEFT JOIN spisok1c ON TRY_CAST(REPLACE(spisok1c.tab1c, ' ', '') AS INT) = CAST(fio.tab1c AS INT) AND spisok1c.orgcode = sp_firms.frm_1c_inn
            //                    ORDER BY fio.tab ASC";
            string query = $@"SELECT * FROM fio_spisok1c_view ORDER BY tab ASC";
            return _dbHelper.ExecuteQuery(query);
        }
        public System.Data.DataTable GetBrigVed()
        {
            string query = "SELECT * FROM brig_ved";
            return _dbHelper.ExecuteQuery(query);
        }
        public System.Data.DataTable GetFioDolgnPodr1c(string selectValue)
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
        public System.Data.DataTable GetFioAndSpFirmsAndTabN()
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
        public System.Data.DataTable GetFioDolgnPogr1cWhere(string selectValue, string selectOtch, bool checkBoxOsnTab)
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
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@tab_sovm", tab }, { "@tab", tab } });
        }
        /// <summary>
        ///  Получение нового табельного номера
        /// </summary>
        /// <param name="sqlFio">Максимальный табельный</param>
        /// <returns></returns>
        public int GetFioNewTab()
        {
            string query = "SELECT ISNULL(MAX(tab), 0) + 1 AS newTab FROM fio";
            System.Data.DataTable sqlFio = _dbHelper.ExecuteQuery(query);
            return Convert.ToInt32(sqlFio.Rows[0]["newTab"]);
        }
        public System.Data.DataTable GetFioForXML(string tab)
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
    }
}
