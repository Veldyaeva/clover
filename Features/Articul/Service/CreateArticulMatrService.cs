using Dapper;
using DevExpress.CodeParser;
using DevExpress.Utils.Gesture;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraScheduler.Native;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using SewingProduction.Core.Models;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
            catch (SqlException ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetMatrForArticulAsync");
                return null;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetMatrForArticulAsync");
                return null;
            }
        }
        public async Task<CreateArticulMatrModel> GetStatusForArticulAsync(string nn, int idgost, int agid )
        {
            try
            {
                using var connection = _dbHelper.GetConnection();
                var result = await connection.QueryFirstOrDefaultAsync<CreateArticulMatrModel>(
                    "dbo.spSetDateCertificationApproval",
                    new
                    {
                        nn,
                        idgost,
                        agid
                    },
                        commandType: CommandType.StoredProcedure
                    );

                return result;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetStatusForArticulAsync");
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

        public async Task<BindingList<SpArtPreviewModel>> GetArticulsForCompareAsync(string articul)
        {
            try
            {
                articul = articul.Trim();
                string substring = "-";
                int indexOfSubstring = articul.IndexOf(substring);
                if (indexOfSubstring > 0)
                {
                    articul = articul.Substring(0, indexOfSubstring);
                }


                string query = "select vsk.kod, vsk.kodd,vsk.articul,vsk.grup,vsk.mod,va.tmName "+
                    "from dbo.view_spArticulKodd vsk "+
                    "inner join view_art va on vsk.kod = va.kod "+
                    "WHERE vsk.articul like @articul ";

                return new BindingList<SpArtPreviewModel>(await _dbService.GetListAsync<SpArtPreviewModel>(query, new { articul = articul + "%" }));
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetArticulsForCompareAsync");
                return null;
            }


        }

    }
}
