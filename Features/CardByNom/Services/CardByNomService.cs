using SewingProduction.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Z.Dapper;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using Dapper;
using DevExpress.Mvvm.Native;
using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Services;

namespace SewingProduction.Features.CardByNom.Services
{
    /// <summary>
    /// Сервис работы с базой данных для таблиц art_norm, norm_rasz, norm_raskr, norm_kont и доп.обработки.
    /// Использует Dapper для ускоренного доступа к данным.
    /// </summary>
    public class CardByNomService
    {
        private static DatabaseHelper _dbHelper;
        private readonly DbService _dbService;
        //    private readonly HybridLogger _logger = new HybridLogger();
        private readonly FileLogger _logger = new FileLogger();
        #region
        /// <summary>
        /// Инициализирует новый экземпляр dbService.
        /// </summary>
        /// <param name="dbHelper">Помощник для работы с базой данных.</param>
        public CardByNomService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _dbService = new DbService(_dbHelper);
        }

        #endregion

        #region
        // Получение связанных данных
        /// <summary>
        /// Получает данные из представления NaklView по pach_kod 
        /// </summary>
        /// <param pachKod="_pachKod">pach_kod</param>
        /// <returns></returns>
        public async Task<List<NaklView>> GetNaklViewByPachKod(string pachKod)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var xPachKod = pachKod + "%";
                string query = $"select * from NaklView where nom = (select nom from raskr_zeh_up where pach_kod like @pachKod )";
                var result = await connection.QueryAsync<NaklView>(query, new Dictionary<string, object> { { "@pachKod", xPachKod } });
                return result.ToList();
            }
        }

        public async Task<List<NaklView>> GetNaklViewByNomZad(string nomZad)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = $"select * from NaklView where nom_zad = @nomZad";
                var result = await connection.QueryAsync<NaklView>(query, new Dictionary<string, object> { { "@nomZad", nomZad } });
                return result.ToList();
            }
        }

        /// <summary>
        /// Получает данные из таблицы View_History_razdel_nakl по iz 
        /// </summary>
        /// <param iz="iz">pach_kod</param>
        /// <returns></returns>
        public async Task<List<HistoryRazdelNaklViewByIz>> GetHistoryRazdelNaklViewByIz(string iz)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                //var xPachKod = pachKod + "%";
                //string query = $"select * from NaklView where nom = (select nom from raskr_zeh_up where left(pach_kod,10) = '{pachKod}' )";
                string query = "";
                if (iz.Length == 0 || iz == null)
                {
                    query = $"SELECT top 0 * FROM View_History_razdel_nakl order by id";
                }
                else
                {
                    query = $"SELECT * FROM View_History_razdel_nakl where iz_b = '{iz}' order by id";
                }
                var result = await connection.QueryAsync<HistoryRazdelNaklViewByIz>(query, new Dictionary<string, object> { { "@pachKod", iz } });
                return result.ToList();
            }
        }

        public async Task<ChipInfo> GetChipInfoByPachKod(string pachKod)
        {
            try
            {
                var xPachKod = pachKod + "%";
                string query = $"SELECT dbo.checkChipNakl('', (SELECT nom_zad FROM raskr_zeh_up WHERE pach_kod LIKE @pachKod)) AS isChip\r\n ";
                return await _dbService.GetEntityAsync<ChipInfo>(query, new { xPachKod });
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных ChipInfo для pach_kod {pachKod}");
                return null;
            }
        }
        public async Task<ChipInfo> GetChipInfoByNomZad(string nomZad)
        {
            try
            {
                string query = $"SELECT dbo.checkChipNakl('', @nomZad) AS isChip ";
                return await _dbService.GetEntityAsync<ChipInfo>(query, new { nomZad });
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных ChipInfo для NomZad {nomZad}");
                return null;
            }
        }
        
        public async Task<List<ProizvCombIzd>> GetProizvCombIzdByPachKod(string pachKod, int vidPr)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                var xPachKod = pachKod + "%";
                string query = $"DECLARE @xPsaIdOsn NVARCHAR(10);" +
                    $" SELECT @xPsaIdOsn = psaIdOsn FROM nomZadAssort_view WHERE nomKombIzd = (SELECT nom_zad FROM raskr_zeh_up WHERE pach_kod LIKE @pachKod);" +
                    $" exec getProizvCombIzd @xPsaIdOsn, @vidPr";
                var result = await connection.QueryAsync<ProizvCombIzd>(query, new Dictionary<string, object> { { "@pachKod", xPachKod }, { "@vidPr", vidPr } });
                return result.ToList();
            }
        }
        #endregion

    }
}