using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.Xpo.DB.Helpers;
using SewingProduction.Core.Models;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Articul.Service
{
    public class ArticulDataService
    {
        private readonly DbService _dbService;

        public ArticulDataService()
        {
            _dbService = new DbService(new DatabaseHelper());
        }

        public async Task<List<ArticulModel>> GetAllAsync()
        {
            string query = "SELECT * FROM sp_articul";
            return await _dbService.GetListAsync<ArticulModel>(query, new { });
        }
        public async Task<List<ArticulModel>> GetArtPreviewAsync()
        {
            string query = "select * from dbo.view_art";

            return await _dbService.GetListAsync<ArticulModel>(query, new { });

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

        public async Task<List<spArticulNaborSostav>> GetSostavNaborForKod(string kod)
        {
            string query = "SELECT kod, tk_name, tat_name, id_gost, name_gost, ag_naimen, sostav, razm" +
                " FROM view_articulNaborSostav where kod  = @kod";
            return await _dbService.GetListAsync<spArticulNaborSostav>(query, new { kod });
        }

    }
}
