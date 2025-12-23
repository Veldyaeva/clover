using Dapper;
using SewingProduction.Features.CuttingProduction.Models;
using SewingProduction.Features.Tabel.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Tabel.Services
{
    public class TabelDataService
    {
        private static DatabaseHelper _dbHelper;
        private readonly DbService _dbService;
        private readonly FileLogger _logger = new FileLogger();
        public TabelDataService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _dbService = new DbService(_dbHelper);
        }
        public async Task<List<ListForLinking>> GetListForLinkingsAsync()
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = "select * from tabelZlSpisok order by date_p desc ";

                    var result = await connection.QueryAsync<ListForLinking>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных spisok");
                return null;
            }
        }
        public async Task<List<ScheduleOfWork>> GetScheduleOfWorkAsync()
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = "select id , scheduleName from ScheduleOfWork";

                    var result = await connection.QueryAsync<ScheduleOfWork>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }

            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, $"Ошибка при получении данных ScheduleOfWork");
                return null;
            }
        }
        public async Task<List<zlPodr>> GetzlPodrAsync()
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = "select gr , naimen from zlgr order by gr";

                    var result = await connection.QueryAsync<zlPodr>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }

            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, $"Ошибка при получении данных zlPodr");
                return null;
            }
        }
        public async Task<List<SpPodr>> GetSpPodrAsync()
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = "select tnid,t_n, naimen from tab_n order by tnid";

                    var result = await connection.QueryAsync<SpPodr>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }

            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, $"Ошибка при получении данных spPodr");
                return null;
            }
        }
        public async Task<List<TimeSheet>> GetTabelGrAsync(string mg, int gr)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"select *,tItogView/dl_d as tItogD  from TimeSheetUnion where mg = '{mg}' and ttabn = {gr} order by ttabn,ttabnsort,fio asc ";

                    var result = await connection.QueryAsync<TimeSheet>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных spisok");
                return null;
            }
        }
        public bool CheckRecordsExistsTabel(string mg, int gr)
        {
            bool _isCountTabel;
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"select * from TimeSheetUnion where mg = '{mg}' and ttabn = {gr}  ";

                    _isCountTabel = _dbHelper.Exists(query, new Dictionary<string, object> { });
                    return _isCountTabel;
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, $"Ошибка при получении данных spisok");
                return false;
            }

            return _isCountTabel;
        }
    }
}
