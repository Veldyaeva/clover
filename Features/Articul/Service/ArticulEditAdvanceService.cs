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
        private readonly DatabaseHelperSQL _dbHelper;

        public ArticulEditAdvanceService()
        {
            _dbService = new DbService(new DatabaseHelperSQL());
            _dbHelper = new DatabaseHelperSQL();
        }
        public async Task<BindingList<ArticulModel>> GetArtByKoddAsync(string kodd)
        {
            try
            {
                string query = "select kod, trim(razm) razm , po from dbo.view_art WHERE kodd = @kodd";

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
        /// <summary>
        /// Загрузка карточки артикула (общие данные) по kodd в виде SpArticulPreviewModel
        /// для размещаемого на форме ArticulControl.
        /// </summary>
        public async Task<SpArticulPreviewModel> GetCardPreviewByKoddAsync(string kodd)
        {
            try
            {
                string query = "select top 1 * from dbo.view_sp_articul_all WHERE kodd = @kodd";
                return await _dbService.GetEntityAsync<SpArticulPreviewModel>(query, new { kodd });
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetCardPreviewByKoddAsync");
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
