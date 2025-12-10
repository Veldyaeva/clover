using Dapper;
using DevExpress.CodeParser;
using SewingProduction.Core.Models;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SewingProduction.Features.Articul.Service
{
    public class ArticulDataService
    {
        private readonly DbService _dbService;

        private readonly DatabaseHelper _dbHelper;

        public ArticulDataService()
        {
            _dbService = new DbService(new DatabaseHelper());
            _dbHelper = new DatabaseHelper(); 
        }

        public async Task<List<ArticulModel>> GetAllAsync()
        {
            string query = "SELECT * FROM sp_articul";
            return await _dbService.GetListAsync<ArticulModel>(query, new { });
        }
        public async Task<ArticulModel> GetArtByKodAsync(string kod)
        {
            string query = "SELECT * FROM sp_articul WHERE kod = @kod";
            return await _dbService.GetEntityAsync<ArticulModel>(query, new { kod });
        }
        public async Task<List<SpArtPreviewModel>> GetArtPreviewAsync()
        {
            string query = "select * from dbo.view_art";
            return await _dbService.GetListAsync<SpArtPreviewModel>(query, new { });
        }
        /*public async Task<List<SpArtPreviewModel>> GetLightweightTableArtPreviewAsync()
        {
            string query = "select * from dbo.view_art";
            return await _dbService.GetListAsync<SpArtPreviewModel>(query, new { });
        }
        */
        public async Task<List<ArticulModel>> GetArtByKoddAsync( string kodd)
        {
            string query = "select * from dbo.view_art WHERE kodd = @kodd";

            return await _dbService.GetListAsync<ArticulModel>(query, new { kodd });

        }
        public async Task<SpArticulPreviewModel> GetByKodAsync(string kod)
        {
            string query = "SELECT * FROM dbo.viewArticul_preview WHERE kod = @kod";

            return await _dbService.GetEntityAsync<SpArticulPreviewModel>(query, new { kod });

        }
        public async Task<List<ArtDrModel>> GetArtDrByKod(string kod)
        {
            string query = $"select * from dbo.view_art_dr where kod = @kod";

            return await _dbService.GetListAsync<ArtDrModel>(query, new { kod });

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
            string query = "SELECT Razm_all FROM Razm WHERE Razm = @razm ORDER BY razm DESC";
            return await _dbService.GetEntityAsync<string>(query, new { razm });
        }
        public async Task<string> GetFileEskizForKod(string kod)
        {
            string query = "SELECT dbo.getFileEskizForKodd(@kod) AS pathpict";
            return await _dbService.GetEntityAsync<string>(query, new { kod });
        }
        public async Task<List<SpArticulKomplSostModel>> GetSostavkomplForKod(string kod)
        {
            string query = "SELECT * from view_KomplSostav where kod_k = @kod";
            return await _dbService.GetListAsync<SpArticulKomplSostModel>(query, new { kod });

        }
        public async Task<List<SpArticulNaborSostav>> GetSostavNaborForKod(string kod)
        {
            string query = "SELECT kod, tk_name, tat_name, id_gost, name_gost, ag_naimen, sostav, razm" +
                " FROM view_articulNaborSostav where kod  = @kod";
            return await _dbService.GetListAsync<SpArticulNaborSostav>(query, new { kod });
        }
                
    }
}
