using SewingProduction.Core.Models;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Articul.Service
{
    public class ArticulEditAdvanceService
    {
        private readonly DbService _dbService;
        private readonly FileLogger _logger = new FileLogger();
        private readonly DatabaseHelper _dbHelper;

        public ArticulEditAdvanceService()
        {
            _dbService = new DbService(new DatabaseHelper());
            _dbHelper = new DatabaseHelper();
        }
        public async Task<BindingList<ArticulModel>> GetArtByKoddAsync(string kodd)
        {
            try
            {
                string query = "select * from dbo.view_art WHERE kodd = @kodd";
                //var bb = await _dbService.GetListAsync<ArticulModel>(query, new { kodd });
                //var ret = new BindingList<ArticulModel>(bb);
                //bb = null;
                //return ret;
                return new BindingList<ArticulModel>(await _dbService.GetListAsync<ArticulModel>(query, new { kodd }));

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetArtByKoddAsync");
                return null;
            }
        }
        public async Task<BindingList<ArticulModel>> GetCommonArtByKoddAsync(string kodd)
        {
            try
            {
                string query = "select top 1 * from dbo.view_sp_articul_all WHERE kodd = @kodd";
                //var bb = await _dbService.GetListAsync<ArticulModel>(query, new { kodd });
                //var ret = new BindingList<ArticulModel>(bb);
                //bb = null;
                //return ret;
                return new BindingList<ArticulModel>(await _dbService.GetListAsync<ArticulModel>(query, new { kodd }));
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetArtByKoddAsync");
                return null;
            }
        }
        public async Task<BindingList<GostRazmerNabViewModel>> GetGostRazmByIDAsync(int idgost)
        {
            try
            {
                string query = "select id_gost,id_razmer, trim(razm) as razm from gost_sv_razmer sv inner join gost_razmer gr on sv.id_razmer = gr.id_rost WHERE id_gost = @idgost";
                //var bb = await _dbService.GetListAsync<GostRazmerNabViewModel>(query, new { idgost });
                //var ret = new BindingList<GostRazmerNabViewModel>(bb);
                //bb = null;
                //return ret;
                return new BindingList<GostRazmerNabViewModel>(await _dbService.GetListAsync<GostRazmerNabViewModel>(query, new { idgost }));

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetGostRazmByIDAsync");
                return null;
            }
        }


    }
}
