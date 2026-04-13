using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Features.TeamWork.Services
{
    /// <summary>
    /// Query-oriented сервис вкладки "Работа с артикулами".
    /// Держит логику выборок вне формы и агрегирует повторяющиеся загрузочные сценарии.
    /// </summary>
    public sealed class TeamWorkArticlesQueryService
    {
        private readonly ArtNormRepository _artNormRepository;
        private readonly DbService _dbService;
        private readonly ILogger _logger;

        public TeamWorkArticlesQueryService(
            ArtNormRepository artNormRepository,
            DbService dbService,
            ILogger logger)
        {
            _artNormRepository = artNormRepository;
            _dbService = dbService;
            _logger = logger;
        }

        public Task<List<MyDataART>> LoadUnboundArticlesAsync()
        {
            const string query = "SELECT * FROM articulListGroupBySizeLabel where annId is null or annId = 0";
            return _dbService.GetListAsync<MyDataART>(query, null);
        }

        public Task<List<MyDataART>> SearchUnboundArticlesAsync(string searchText)
        {
            const string query = @"SELECT * FROM articulListGroupBySizeLabel 
                                          WHERE  (annId is null or annId = 0) and
                                          articul LIKE @searchPattern 
                                          ORDER BY articul, row_num";
            var parameters = new Dictionary<string, object>
            {
                { "@searchPattern", $"%{searchText}%" }
            };

            return _dbService.GetListAsync<MyDataART>(query, parameters);
        }

        public async Task<List<MyDataANN>> LoadCurrentWorkDivisionsAsync(bool includeAll)
        {
            var data = await _artNormRepository.GetArtNormDataCurrent(includeAll);
            if (data != null)
            {
                foreach (var item in data)
                    item.Stat = StatusHelper.GetStatusText(item.Status);
            }

            return data ?? new List<MyDataANN>();
        }

        public async Task<List<MyDataANN>> SearchCurrentWorkDivisionsAsync(string searchText)
        {
            const string query = @"SELECT ann.*
                                       FROM artNormNView ann
                                       WHERE ann.annId IN (
                                           SELECT sa.annId 
                                           FROM sp_articul sa 
                                           WHERE sa.articul LIKE @searchPattern)
                                       ORDER BY ann.annId DESC";
            var parameters = new Dictionary<string, object>
            {
                { "@searchPattern", $"%{searchText}%" }
            };

            var data = await _dbService.GetListAsync<MyDataANN>(query, parameters);
            if (data != null)
            {
                foreach (var item in data)
                    item.Stat = StatusHelper.GetStatusText(item.Status);
            }

            return data ?? new List<MyDataANN>();
        }

        public async Task<List<MyDataANN>> LoadWorkDivisionsByArticulAsync(string articul)
        {
            var relatedData = new List<MyDataANN>();
            bool loadAll = string.IsNullOrEmpty(articul);
            if (loadAll)
            {
                return await _artNormRepository.GetArtNormDataCurrent(true);
            }

            var prefixes = new List<string>();
            if (!string.IsNullOrEmpty(articul))
            {
                prefixes.Add(articul);

                string artWithoutDash = articul.Replace("-", "");
                if (artWithoutDash != articul)
                    prefixes.Add(artWithoutDash);

                int dashIndex = articul.IndexOf("-");
                if (dashIndex > 0)
                {
                    string artPrefix = articul.Substring(0, dashIndex);
                    prefixes.Add(artPrefix);
                }
            }

            relatedData = await _artNormRepository.GetArtNormDataByArticulPatterns(prefixes);

            var uniqueData = new Dictionary<int, MyDataANN>();
            foreach (var item in relatedData)
            {
                if (!uniqueData.ContainsKey(item.AnnID))
                    uniqueData[item.AnnID] = item;
            }

            await _logger.LogEventAsync($"Успешная загрузка РТ для артикула {articul}", nameof(LoadWorkDivisionsByArticulAsync));
            return uniqueData.Values.ToList();
        }

        public Task<List<NormRasz>> LoadNormRaszAsync(int annId, CancellationToken cancellationToken = default) =>
            annId > 0
                ? _artNormRepository.GetRelatedNormRasz(annId, cancellationToken)
                : Task.FromResult(new List<NormRasz>());

        public Task<List<NormRask>> LoadNormRaskAsync(int annId, CancellationToken cancellationToken = default) =>
            annId > 0
                ? _artNormRepository.GetRelatedNormRask(annId, cancellationToken)
                : Task.FromResult(new List<NormRask>());

        public Task<List<NormKont>> LoadNormKontAsync(int annId, CancellationToken cancellationToken = default) =>
            annId > 0
                ? _artNormRepository.GetRelatedNormKont(annId, cancellationToken)
                : Task.FromResult(new List<NormKont>());

        public Task<List<NZPByKoddRt>> LoadNzpAsync(int annId, CancellationToken cancellationToken = default) =>
            annId > 0
                ? _artNormRepository.GetNzpWithPztCounts(annId, cancellationToken)
                : Task.FromResult(new List<NZPByKoddRt>());

        public Task<List<MyDataANN>> LoadPreArchiveAsync()
        {
            const string query = "SELECT * FROM artNormNView WHERE status = 4";
            return _dbService.GetListAsync<MyDataANN>(query, null);
        }

        public async Task<List<ArtNormN>> LoadArchiveAsync()
        {
            const string query = "SELECT * FROM artNormNView WHERE status = 3";
            var data = await _dbService.GetListAsync<ArtNormN>(query, null);
            if (data != null)
            {
                foreach (var item in data)
                    item.StatusText = StatusHelper.GetStatusText(item.Status);
            }

            return data ?? new List<ArtNormN>();
        }
    }
}
