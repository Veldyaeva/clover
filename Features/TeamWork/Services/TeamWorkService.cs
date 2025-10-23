using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SewingProduction.Models;
using SewingProduction.Services;

namespace SewingProduction.Features.TeamWork.Services
{
    /// <summary>
    /// Сервис уровня приложения для TeamWork: orchestration/координация загрузки и обновления связанных данных.
    /// Не выполняет SQL напрямую (делегирует ArtNormService), не содержит UI.
    /// </summary>
    public class TeamWorkOrchestrator
    {
        private readonly ArtNormRepository _artNormService;
        private readonly DbService _dbService;
        private readonly ILogger _logger;

        public TeamWorkOrchestrator(ArtNormRepository artNormService, DbService dbService, ILogger logger)
        {
            _artNormService = artNormService;
            _dbService = dbService;
            _logger = logger;
        }

        /// <summary>
        /// Загружает все РТ и пытается восстановить фокус (для кнопки перезагрузки данных).
        /// </summary>
        public async Task<TeamWorkReloadResult> LoadWorkDivisionsWithFocusAsync(int? currentAnnId = null)
        {
            try
            {
                await _logger.LogEventAsync($"LoadWorkDivisionsWithFocus: Начало загрузки с сохранением AnnID: {currentAnnId}", "TeamWorkService");

                // Загружаем данные
                var workDivisions = await _artNormService.GetArtNormData();

                // Ищем позицию для восстановления фокуса
                int? focusRowHandle = null;
                if (currentAnnId.HasValue && workDivisions?.Count > 0)
                {
                    var targetItem = workDivisions.FirstOrDefault(x => x.AnnID == currentAnnId.Value);
                    if (targetItem != null)
                    {
                        focusRowHandle = workDivisions.IndexOf(targetItem);
                    }
                }

                return new TeamWorkReloadResult
                {
                    Data = workDivisions,
                    FocusAnnId = currentAnnId,
                    FocusRowHandle = focusRowHandle,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке данных с сохранением фокуса");
                return new TeamWorkReloadResult { Success = false, Error = ex.Message };
            }
        }

        /// <summary>
        /// Обновляет связанные таблицы (NormRask/Kont/Rasz) для указанного AnnID.
        /// </summary>
        public async Task<RelatedDataResult> RefreshRelatedDataAsync(int annId)
        {
            if (annId <= 0)
            {
                await _logger.LogEventAsync($"RefreshRelatedData: Некорректный AnnID: {annId}", "TeamWorkService");
                return new RelatedDataResult { Success = false, Error = "Некорректный AnnID" };
            }

            try
            {
                await _logger.LogEventAsync($"RefreshRelatedData: Обновление связанных данных для AnnID: {annId}", "TeamWorkService");

                // Загружаем связанные данные асинхронно
                var normRaskTask = _artNormService.GetRelatedNormRask(annId);
                var normKontTask = _artNormService.GetRelatedNormKont(annId);
                var normRaszTask = _artNormService.GetRelatedNormRasz(annId);

                await Task.WhenAll(normRaskTask, normKontTask, normRaszTask);

                return new RelatedDataResult
                {
                    NormRask = await normRaskTask,
                    NormKont = await normKontTask,
                    NormRasz = await normRaszTask,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при обновлении связанных данных для AnnID: {annId}");
                return new RelatedDataResult { Success = false, Error = ex.Message };
            }
        }
    }

    // Models/TeamWorkReloadResult.cs
    public class TeamWorkReloadResult
    {
        public List<ArtNormN> Data { get; set; }
        public int? FocusAnnId { get; set; }
        public int? FocusRowHandle { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }

    public class RelatedDataResult
    {
        public List<NormRask> NormRask { get; set; }
        public List<NormKont> NormKont { get; set; }
        public List<NormRasz> NormRasz { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
