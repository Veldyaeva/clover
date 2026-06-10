using Dapper;
using DevExpress.CodeParser;
using SewingProduction.Core.Models;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;


namespace SewingProduction.Features.Articul.Service
{
    public class ArticulDataService
    {
        private readonly DbService _dbService;
        //private readonly DatabaseHelper _dbHelper;
        private readonly FileLogger _logger = new FileLogger();

        public ArticulDataService()
        {
            _dbService = new DbService(new DatabaseHelperSQL());
            //_dbHelper = new DatabaseHelper(); 
        }

        public async Task<List<AddNewKopmlModel>> GetArticulsListAsync()
        {
            try
            {
                string query = "SELECT Kodd,Kod,Grup,Articul,Mod,Razm,Sost,Kle,kod_v FROM view_sp_articul";
                return await _dbService.GetListAsync<AddNewKopmlModel>(query, new {});
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetArtPreviewAsyncBindingList");
                return null;
            }
        }
        public async Task<BindingList<SpArtPreviewModel>> GetArtPreviewAsyncBindingList()
        {
            try
            {
                string query = "select * from dbo.view_art";
                var bb = await _dbService.GetListAsync<SpArtPreviewModel>(query, new { });
                var ret = new BindingList<SpArtPreviewModel>(bb);
                bb = null;
                return ret; 
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetArtPreviewAsyncBindingList");
                return null;
            }
        }
        public async Task<SpArticulPreviewModel> GetByKodAsync(string kod)
        {
            try
            {
                string query = "SELECT * FROM dbo.viewArticul_preview WHERE kod = @kod";

                return await _dbService.GetEntityAsync<SpArticulPreviewModel>(query, new { kod });
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetByKodAsync");
                return null;
            }

        }
        public async Task<BindingList<ArtDrModel>> GetArtDrByKodAsync(string kod)
        {
            try
            {
                string query = $"select * from dbo.view_art_dr where kod = @kod";
                var bb = await _dbService.GetListAsync<ArtDrModel>(query, new { kod });
                var ret = new BindingList<ArtDrModel>(bb);
                bb = null;

                return ret;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetArtDrByKodAsync");
                return null;
            }
        }

        public async Task SaveAsync(ArticulModel model)
        {
            await _dbService.SaveEntityAsync("sp_articul", "Kod", model);
        }

        public async Task DeleteAsync(ArticulModel model)
        {
            await _dbService.DeleteEntityAsync("sp_articul", "Kod", model);
        }
        public async Task<string> GetAllRazmByRazmAsync(string razm)
        {
            try
            {
                string query = "SELECT Razm_all FROM Razm WHERE Razm = @razm ORDER BY razm DESC";
                return await _dbService.GetEntityAsync<string>(query, new { razm });

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetAllRazmByRazmAsync");
                return null;
            }
        }
        public async Task<string> GetFileEskizForKod(string kod)
        {
            try
            {
                string query = "SELECT dbo.getFileEskizForKodd(@kod) AS pathpict";
                return await _dbService.GetEntityAsync<string>(query, new { kod });
            }            
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetFileEskizForKod");
                return null;
            }

        }


        public async Task<BindingList<SpArticulKomplSostModel>> GetSostavkomplForKod(string kod)
        {
            try
            {
                string query = "SELECT * from view_KomplSostav where kod_k = @kod";
            
                var bb = await _dbService.GetListAsync<SpArticulKomplSostModel>(query, new { kod });
                var ret = new BindingList<SpArticulKomplSostModel>(bb);
                bb = null;
                return ret;
            }            
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetSostavkomplForKod");
                return null;
            }

}
        public async Task<BindingList<SpArticulNaborSostav>> GetSostavNaborForKod(string kod)
        {
            try
            {
                string query = "SELECT kod, tk_name, tat_name, id_gost, name_gost, ag_name_sokr, sostav, razm" +
                    " FROM view_articulNaborSostav where kod  = @kod";
            

                var bb = await _dbService.GetListAsync<SpArticulNaborSostav>(query, new { kod });
                var ret = new BindingList<SpArticulNaborSostav>(bb);
                bb = null;
                return ret;
            }            
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetSostavNaborForKod");
                return null;
            }

            

        }
                
    }
}
