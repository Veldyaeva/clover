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
        public async Task<List<raskrZehUpView>> GetRaskrZehUpViewAsync()
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
        public string GetFio(int? tab)
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
        public void SetValue(string fieldName, object value, decimal? nom)
        {
            
            using (var connection = _dbHelper.GetConnection())
            {

                string query = $"update raskr_zeh_up set  {fieldName} = @value where nom = {nom}";
                _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@value", value ?? DBNull.Value } });

            }
        }
        public DataTable GetNewDataRzu(decimal? nom)
        {
            using (var connection = _dbHelper.GetConnection())
            {

                string query = $"select max(tab_k) as tab_k,max(data_kk_cd) as data_kk_cd, max(n_zeh) as n_zeh, max(dost_zeh) as dost_zeh from raskrZehUpView where nom = {nom}";
                DataTable dt = _dbHelper.ExecuteQuery(query);
                return dt;
            }
            

        }
        
        public string getRecForNomZad(string nomZad)
        {
            string recom = "";
            string query = $"select kodz,recomend,rzu_recom,rzu_nom FROM plan_sezon_zad_recom where nom= {nomZad}";
            DataTable dt = _dbHelper.ExecuteQuery(query);
            foreach (DataRow dr in dt.Rows)
            {
                recom = recom + "Тк. " + dr["kodz"].ToString() + "-" + dr["recomend"];
            }
            return recom;
        }
        public string getRecForNom(decimal? nom)
        {
            string recom = "";
            string query = $"SELECT pszr.id, pszr.nom, pszr.kodz, pszr.recomend, " +
                " pszrr.id_pszr, pszrr.rzu_nom, pszrr.rzu_recom " +
                " FROM [ACE].[dbo].[plan_sezon_zad_recom] pszr " +
                " LEFT JOIN [ACE].[dbo].[plan_sezon_zad_recom_rzu] pszrr ON pszr.id = pszrr.id_pszr " +
                $" where pszrr.rzu_nom= {nom}" +
                " AND ISNULL(TRIM(pszrr.rzu_recom), '') <> '' ";
            DataTable dt = _dbHelper.ExecuteQuery(query);
            foreach (DataRow dr in dt.Rows)
            {
                recom = recom + "Тк. " + dr["kodz"].ToString() + "-" + dr["recomend"];
            }
            return recom;
        }
        public async Task<List<appeZakrNewView>> GetAppeZakrViewAsync()
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    string query = "select * from dbo.appeZakrNZvet() ";

                    var result = await connection.QueryAsync<appeZakrNewView>(query, new Dictionary<string, object> { });
                    return result.ToList();
                }

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных raskrZehUp");
                return null;
            }

        }
    }   
}
