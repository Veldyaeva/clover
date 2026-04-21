using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using SewingProduction.Features.TeamWork.Interfaces;
using SewingProduction.Features.TeamWork.Models.UseCases;
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

        public async Task<RelatedDataResult> LoadRelatedDataAsync(int annId)
        {
            if (annId <= 0)
            {
                return new RelatedDataResult { Success = false, Error = "Некорректный AnnID" };
            }

            var normRaszTask = _artNormService.GetRelatedNormRasz(annId);
            var normRaskTask = _artNormService.GetRelatedNormRask(annId);
            var normKontTask = _artNormService.GetRelatedNormKont(annId);
            await Task.WhenAll(normRaszTask, normRaskTask, normKontTask);

            return new RelatedDataResult
            {
                Success = true,
                NormRasz = await normRaszTask,
                NormRask = await normRaskTask,
                NormKont = await normKontTask
            };
        }

        public async Task<TeamWorkReferenceDataResult> LoadReferenceDataAsync()
        {
            var kodTask = _dbService.GetListAsync<KodProizvModel>("SELECT kod_proizv, text_proizv FROM kod_proizv", null);
            var podrTask = _dbService.GetListAsync<PodrVyazModel>("SELECT kod_vyaz, text_vyaz, kod_proizv FROM podr_vyaz", null);
            var oborudTask = _dbService.GetListAsync<OborudShvModel>("SELECT kod_ob, text_ob FROM spOborudShv", null);

            await Task.WhenAll(kodTask, podrTask, oborudTask);

            return new TeamWorkReferenceDataResult
            {
                KodProizv = (await kodTask) ?? new(),
                PodrVyaz = (await podrTask) ?? new(),
                OborudShv = (await oborudTask) ?? new()
            };
        }

        public Task<List<FioModel>> LoadDesignersAsync() => _artNormService.GetRelDesigner();

        public Task<string> GetSpecByOborudKodAsync(int kodOb) => _artNormService.GetSpecByOborudKod(kodOb);
    }
}


