using Dapper;
using SewingProduction.Features.CuttingProduction.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using SewingProduction.Features.Articul;
using System.Windows.Forms;
using SewingProduction.form;


namespace SewingProduction.Features.CuttingProduction.Services
{

    public class CuttingService
    {
        private static DatabaseHelper _dbHelper;
        private readonly DbService _dbService;
        //    private readonly HybridLogger _logger = new HybridLogger();
        private readonly FileLogger _logger = new FileLogger();
        public CuttingService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _dbService = new DbService(_dbHelper);
        }
        public async Task<List<raskrZehUpView>> GetRaskrZehUpView()
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = "select * from raskrZehUpView";

                    var result = await connection.QueryAsync<raskrZehUpView>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных raskrZehUp");
                return null;
            }
        }
        public string getFio(int? tab)
        {
            string fio = "";
            using (var connection = _dbHelper.GetConnection())
            {

                string query = $"select fio from fio where tab = {tab}";
                DataTable dt = _dbHelper.ExecuteQuery(query);
                DataRow dr = dt.Rows[0];
                fio = dr["fio"].ToString();
            }

            return fio;
        }
        public void setValue(string fieldName, object value, decimal? nom)
        {
            string stringValue = value?.ToString() ?? "NULL";
            using (var connection = _dbHelper.GetConnection())
            {

                string query = $"update raskr_zeh_up set  {fieldName} = {stringValue} where nom = {nom}";
                _dbHelper.ExecuteNonQuery(query);

            }
        }
        public DataTable getNewDataRzu(string pach_kod)
        {
            using (var connection = _dbHelper.GetConnection())
            {

                string query = $"select max(tab_k) as tab_k,max(data_kk_cd) as data_kk_cd, max(n_zeh) as n_zeh, max(dost_zeh) from raskrZehUpView where nom = {pach_kod}";
                DataTable dt = _dbHelper.ExecuteQuery(query);
                return dt;
            }
            

        }
        public bool checkTabKDataKkCd(decimal? nom)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = $"select * from raskrZehUpView where nom = {nom} and (tab_k = 0 or data_kk_cd is null or n_zeh <>0 or (dost_zeh <>' ' or dost_zeh <> 'раскройный'))";
                DataTable dt = _dbHelper.ExecuteQuery(query);
                if (dt.Rows.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
