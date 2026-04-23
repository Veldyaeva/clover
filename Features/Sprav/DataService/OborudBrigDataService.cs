using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.Helpers;

namespace SewingProduction.Features.Sprav.DataService
{

    public class OborudBrigDataService
    {
        private readonly DatabaseHelperSQL _dbHelper;
        public OborudBrigDataService(DatabaseHelperSQL dbHelper)
        {
            _dbHelper = dbHelper;
        }
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
        /// <summary>
        /// Запрос на получение швейного оборудования в зависимости от вида производства
        /// </summary>
        /// <param name="getVid">Вид производства (Швейное/Вязальное...)</param>
        /// <param name="getCount">Кол-во строчек</param>
        /// <param name="getZeh">Выбранный цех</param>
        /// <returns></returns>
        public DataTable GetSpOborudShv(string getVid, int getCount, string getZeh)
        {
            switch (getVid)
            {
                case "Швейный":
                    getVid = " vid_shp ";
                    break;
                case "Вязальный":
                    getVid = " vid_vzp ";
                    break;
                case "Носочный":
                    getVid = " vid_np ";
                    break;
                case "Раскройный":
                    getVid = " vid_rz ";
                    break;
                default:
                    getVid = " 3 ";
                    break;
            }
            string query = $@"SELECT spoborudshv.text_ob AS 'Оборудование', COALESCE(OborudBrig.count, 0) AS 'Кол-во', 
                                     CASE 
                                         WHEN {getVid} = 1 THEN 'Основное'
                                         WHEN {getVid} = 2 THEN 'Дополнительное'
                                         WHEN {getVid} = 3 THEN 'Другое'
                                     END AS 'Вид'
                                  FROM spOborudShv 
                                  LEFT JOIN OborudBrig ON spoborudshv.kod_ob = OborudBrig.kod_ob 
                                  AND OborudBrig.idZeh ";
            if (getCount < 1)
                query += " IS NOT NULL";
            else
                if (getZeh == null)
                query += " IS NOT NULL";
            else
                query += " = (SELECT idZeh FROM ZehList WHERE nameZeh = @getZeh)";
            query += $@" WHERE spoborudshv.kod_ob IS NOT NULL AND COALESCE(spoborudshv.arhiv, 0) = 0 
                        AND {getVid} > 0
                        ORDER BY CASE WHEN COALESCE(OborudBrig.count, 0) > 0 THEN 1 ELSE 0 END DESC, text_ob ASC";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@getZeh", getZeh } });
        }
        /// <summary>
        /// Обновление/добавление оборудования в цеху
        /// </summary>
        /// <param name="getOb">Оборудование</param>
        /// <param name="getZeh">Имя цеха</param>
        /// <param name="getValue">Кол-во</param>
        public void UpdateOborudBrig(string getOb, string getZeh, object getValue)
        {
            string get_kod_ob = $" (SELECT kod_ob FROM spoborudshv WHERE text_ob = '{getOb}') ";
            string get_idZeh = $" (SELECT idZeh FROM ZehList WHERE nameZeh = '{getZeh}') ";

            //Обновляем, если такой записи нет то добавляем:
            string query = $@" UPDATE OborudBrig SET count = @getValue
                                  WHERE kod_ob = {get_kod_ob} AND idZeh = {get_idZeh}
                                    IF @@ROWCOUNT = 0
                                    BEGIN
                                        INSERT INTO OborudBrig (kod_ob, idZeh, count)
                                        VALUES ({get_kod_ob}, {get_idZeh}, @getValue);
                                    END";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@getValue", getValue } });
        }
        #endregion
    }
}
