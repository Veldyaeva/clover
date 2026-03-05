using Dapper;
using DevExpress.Xpo.DB.Helpers;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using SewingProduction.Core.Models;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Articul.Service
{
    public class CreateArticulMatrService
    {
        private readonly DbService _dbService = new DbService(new DatabaseHelper());
        private readonly FileLogger _logger = new FileLogger();
        private readonly DatabaseHelper _dbHelper = new DatabaseHelper();

        public CreateArticulMatrService()
        {
            //_dbService = new DbService(new DatabaseHelper());
            //_dbHelper = new DatabaseHelper();
        }
        public async Task<BindingList<CreateArticulMatrModel>> GetMatrForArticulAsync()
        {
            try
            {
                using var connection = _dbHelper.GetConnection();
                var result = await connection.QueryAsync<CreateArticulMatrModel>(
                    "dbo.spCreateArticulMatr",
                    param: null,
                    transaction: null,
                    commandType: CommandType.StoredProcedure
                    );

                return new BindingList<CreateArticulMatrModel>(result.AsList());
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetMatrForArticulAsync");
                return null;
            }
        }
        public async Task<BindingList<GostModel>> GetGostAsync()
        {
            try
            {
                string query = "SELECT  id_gost,name_gost,opi_gost FROM dbo.gost where ust = 0 order by id_gost";
                return new BindingList<GostModel>(await _dbService.GetListAsync<GostModel>(query, new { }));
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetGostAsync");
                return null;
            }
        }

        public async Task<List<GostGrupIzdViewModel>> GetGrupGostAsync()
        {
            try
            {
                string query = "SELECT id_gost, ag_id, ag_name_sokr,n_i FROM View_GostGrupIzd order by id_gost,n_i ";
                return await _dbService.GetListAsync<GostGrupIzdViewModel>(query, new { });

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetGrupGostAsync");
                return null;
            }
        }
    }
}
