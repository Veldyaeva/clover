using System.Threading.Tasks;
using SewingProduction.Models;
using SewingProduction.Services;

namespace SewingProduction.Features.TeamWork.Services
{
    /// <summary>
    /// Сервис доступа к данным РТ для форм TeamWork (загрузка/сохранение одной записи ANN).
    /// Отвечает только за обращение к слоям данных, не содержит UI-логики.
    /// </summary>
    public class TeamWorkDataService : ITeamWorkDataService
    {
        private readonly ArtNormRepository _artNormService;
        private readonly DbService _dbService;

        public TeamWorkDataService(ArtNormRepository artNormService, DbService dbService)
        {
            _artNormService = artNormService;
            _dbService = dbService;
        }

        /// <summary>
        /// Загружает данные ANN по идентификатору.
        /// </summary>
        public async Task<ArtNormN> LoadAnnDataAsync(int annId)
        {
            return await _artNormService.GetArtNormDataById(annId);
        }

        /// <summary>
        /// Сохраняет данные ANN.
        /// </summary>
        public async Task SaveAnnDataAsync(ArtNormN data)
        {
            await _dbService.UpdateEntityAsync(TableNames.Ann, TableNames.AnnId, data);
        }
    }
}


