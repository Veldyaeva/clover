using Dapper;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    public class KnitterDataService
    {
        private readonly DbService _dbService;

        public KnitterDataService()
        {
            _dbService = new DbService(new DatabaseHelper());
        }

		public async Task<List<KnitterPZVModel>> GetPlanByTabAsync(int tab)
        {
            string query = "SELECT * FROM dbo.planZagrVyaz WHERE pzvTab = @tab";
			return await _dbService.GetListAsync<KnitterPZVModel>(query, new { tab });
        }

		internal async Task<string> GetFioByTabAsync(int tab)
		{
			string query = "SELECT fio FROM dbo.fio WHERE tab = @tab";
			var fio = await _dbService.GetFirstOrDefaultAsync<string>(query, new { tab });
			return fio ?? string.Empty;
		}

        internal async Task<List<FioModel>> GetFioListAsync()
        {
            // Берем минимально необходимый набор полей для справочника ФИО
            string query = @"SELECT tab AS Tab, fio AS Fio FROM dbo.fio ORDER BY fio";

            return await _dbService.GetListAsync<FioModel>(query, new { });
        }

        internal async Task <IEnumerable<PlanZagrVyazOper>> GetPlanZagrVyazByPachListAsync(string nomListJson, int vyazPodrKod)
        {
            var parameters = new dynamicparameters();
            parameters.add("@xnomzadnomlistjson", nomlistjson, dbtype.string);
            parameters.add("@xvyazpodrkod", vyazpodrkod, dbtype.int32);

            return await _connection.queryasync<planzagrvyazoper>(
                "dbo.getplanzagrvyazbypachlist",
                parameters,
                commandtype: commandtype.storedprocedure
            );
        }


    }
    public class PlanZagrVyazService
    {
        private readonly IDbConnection _connection;

        public PlanZagrVyazService(IDbConnection connection)
        {
            _connection = connection;
        }

    }
}
