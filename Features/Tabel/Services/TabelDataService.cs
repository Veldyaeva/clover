using Dapper;
using System.Data;
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
        public async Task<List<zlPodr>> GetZlComboPodrAsync()
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = "select gr , naimen from zlgr";

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
        public async Task<List<SpPodr>> GetzlPodrAsync()
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = "select gr as tnid, naimen from zlgr";

                    var result = await connection.QueryAsync<SpPodr>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }

            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, $"Ошибка при получении данных zlPodr");
                return null;
            }
        }
        public async Task<List<SpPodr>> GetSpPodrAsync(int idGroup, int idUser)
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
                    string query = $"select *,iif(dl_d = 0 ,0,round((tItogViewChas+0.0)/dl_d,2)) as tItogD  from TimeSheetUnion where mg = '{mg}' and ttabn = {gr} and podrTableId = {groupID} order by ttabn,ttabnsort,fio asc ";

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
                    string query = $"select * from WorkTypesTabel where podrId = {idGroup} order by nameWorkTypes";
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
        public bool CheckRecordsExistsOrionUser(string mg, int gr, int podrTabelId)
        {
            bool _isCountTabel;
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $" select uin from TimeSheetUnion " +
                        $" inner join [192.168.106.119\\SQLSERVER2008].[demobase1_20_3_re].[dbo].[pList] on TimeSheetUnion.uin = pList.TabNumber " +
                        $" where TimeSheetUnion.mg = '{mg}' and TimeSheetUnion.ttabn = {gr} and TimeSheetUnion.podrTableId = {podrTabelId} and TimeSheetUnion.uin is not null ";

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
    
        public async Task<List<Spisok1c>> GetSpisok1cAsync(string lastName, string firstName, string middleName)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {

                    string query = $"select uin,tab1c,LastName,FirstName,MiddleName,bDay,orgName,podrName,date_p,date_u from spisok1c where trim(LastName) = trim('{lastName}') and trim(FirstName) =  trim('{firstName}') and trim(middleName) = trim('{middleName}') and sovm =0 ";

                    var result = await connection.QueryAsync<Spisok1c>(query, new Dictionary<string, object> { });
                    return result.ToList();

                }

            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, $"Ошибка при получении данных spPodr");
                return null;
            }
            
        }
        public DateTime? GetDateReadOnlyDd(string mg)
        {
            DataTable TableResult;
            DateTime? _dateTime;
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"select tsl_dateTo from tabel_sp_lock where tsl_tsltID=1 and tsl_mg='{mg}'";

                    TableResult =  _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { });
                    if (TableResult.Rows.Count == 0)
                    {
                        _dateTime = null;
                    }
                    else
                    {
                       _dateTime = Convert.ToDateTime(TableResult.Rows[0]["tsl_dateTo"]);
                    }
                    return _dateTime;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogErrorAsync(ex, $"Ошибка SQL при получении данных userPodr");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, $"Ошибка при получении данных userPodr");
                return null;
            }

            return null;
        }
        public DateTime? GetDateReadOnlyTarif(string mg)
        {
            DataTable TableResult;
            DateTime? _dateTime;
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"select tsl_dateTo from tabel_sp_lock where tsl_tsltID=2 and tsl_mg='{mg}'";

                    TableResult = _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { });
                    if (TableResult.Rows.Count == 0)
                    {
                        _dateTime = null;
                    }
                    else
                    {
                        _dateTime = Convert.ToDateTime(TableResult.Rows[0]["tsl_dateTo"]);
                    }
                    return _dateTime;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogErrorAsync(ex, $"Ошибка SQL при получении данных userPodr");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, $"Ошибка при получении данных userPodr");
                return null;
            }

            return null;
        }
        public async Task<List<PrichInclude>> GetPrichIncludeAsync()
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = $"select * from tabel_sp_pr order by tsp_id";
                    var result = await connection.QueryAsync<PrichInclude>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }

            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, $"Ошибка при получении данных spPodr");
                return null;
            }
        }
        public async Task<List<zlPodr>> GetSpComboPodrAsync()
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = "select tnid as gr , naimen from tab_n";

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
    }
}