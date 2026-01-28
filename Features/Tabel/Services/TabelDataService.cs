using Dapper;
using SewingProduction.Features.CuttingProduction.Models;
using SewingProduction.Features.Tabel.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public async Task<List<SpPodr>> GetSpPodrAsync(int idGroup,int idUser)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    if (idGroup == 19)
                    {

                        string query = $"select tnid,t_n, naimen from tab_n where tnid in(select podrID from userPodr where userId =  {idUser} and podrTableId = {idGroup}) order by tnid";

                        var result = await connection.QueryAsync<SpPodr>(query, new Dictionary<string, object> { });
                        return result.ToList();
                    }
                    if (idGroup == 20)
                    {
                        string query = $"select gr as tnid, naimen from zlgr where gr in(select podrID from userPodr where userId =  {idUser} and podrTableId = {idGroup}) order by gr";

                        var result = await connection.QueryAsync<SpPodr>(query, new Dictionary<string, object> { });
                        return result.ToList();
                    }
                    else
                    {
                        return null;
                    }
                    
                }

            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, $"Ошибка при получении данных spPodr");
                return null;
            }
        }
        public async Task<List<TimeSheet>> GetTabelGrAsync(string mg, int gr, int groupID)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"select *,round((tItogViewChas+0.0)/dl_d,2) as tItogD  from TimeSheetUnion where mg = '{mg}' and ttabn = {gr} and podrTableId = {groupID} order by ttabn,ttabnsort,fio asc ";

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
        public int CheckRecordsExistsGroupGr(int idUser)
        {
            int _isCountGroupGr;
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"select COUNT(*) from (select podrtableid from userPodr where UserId = {idUser} group by podrtableid) s ";

                    _isCountGroupGr = _dbHelper.ExecuteScalar(query, new Dictionary<string, object> { });
                    return _isCountGroupGr;
                }
            }
            catch (SqlException ex)
            {
                 _logger.LogErrorAsync(ex, $"Ошибка SQL при получении данных userPodr");
                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, $"Ошибка при получении данных userPodr");
                return 0;
            }

            return _isCountGroupGr;
        }
        public int GetTabelGroupForUser(int idUser)
        {
            int _valueGroup;
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"select podrtableid from userPodr where UserId = {idUser} group by podrtableid ";

                    _valueGroup = _dbHelper.ExecuteScalar(query, new Dictionary<string, object> { });
                    return _valueGroup;
                }

            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, $"Ошибка при получении данных userPodr");
                return 0;
            }
        }
        public async Task<List<WorkTypes>> GetWorkTypesAsync(int idGroup)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                        string query = $"select * from WorkTypesTabel where podrId = {idGroup} order by id";
                        var result = await connection.QueryAsync<WorkTypes>(query, new Dictionary<string, object> { });
                        return result.ToList();
                }

            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, $"Ошибка при получении данных spPodr");
                return null;
            }
        }
    }
}
