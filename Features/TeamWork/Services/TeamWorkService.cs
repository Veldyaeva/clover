using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;

namespace SewingProduction.Features.TeamWork.Services
{
    /// <summary>
    /// Сервис уровня приложения для TeamWork: orchestration/координация загрузки и обновления связанных данных.
    /// Не выполняет SQL напрямую (делегирует ArtNormRepository), не содержит UI.
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
                await _logger.LogEventAsync($"LoadWorkDivisionsWithFocus: Начало загрузки с сохранением AnnID: {currentAnnId}", "TeamWorkOrchestrator");

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
                await _logger.LogEventAsync($"RefreshRelatedData: Некорректный AnnID: {annId}", "TeamWorkOrchestrator");
                return new RelatedDataResult { Success = false, Error = "Некорректный AnnID" };
            }

            try
            {
                await _logger.LogEventAsync($"RefreshRelatedData: Обновление связанных данных для AnnID: {annId}", "TeamWorkOrchestrator");

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

        /// <summary>
        /// Создает черновик дубля РТ (без UI-редактирования), возвращает новый AnnID и объект-черновик.
        /// </summary>
        public async Task<DuplicateDraftResult> CreateDuplicateDraftAsync(ArtNormN sourceAnn)
        {
            if (sourceAnn == null)
            {
                return new DuplicateDraftResult { Success = false, Error = "Источник для дублирования не задан." };
            }

            try
            {
                var draft = sourceAnn.CloneOperationalData();
                draft.Status = (int)Status.Preliminary;
                draft.StatusText = StatusHelper.GetStatusText(draft.Status);
                draft.AnnID = 0;
                draft.Arh = false;
                draft.dateCreate = DateTime.Now;
                draft.dateUpdate = null;

                int newAnnId = await _dbService.InsertEntityAsync(TableNames.Ann, TableNames.AnnId, draft);
                if (newAnnId <= 0)
                {
                    return new DuplicateDraftResult
                    {
                        Success = false,
                        Error = "Ошибка при создании новой записи РТ в базе данных."
                    };
                }

                return new DuplicateDraftResult
                {
                    Success = true,
                    NewAnnId = newAnnId,
                    DraftAnn = draft
                };
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка в CreateDuplicateDraftAsync");
                return new DuplicateDraftResult { Success = false, Error = ex.Message };
            }
        }

        /// <summary>
        /// Откатывает созданный черновик РТ и связанные с ним нормы.
        /// </summary>
        public async Task<bool> RollbackDraftAsync(int annId)
        {
            if (annId <= 0) return false;

            try
            {
                await _artNormService.DeleteRelatedNormTables(annId);
                await _artNormService.DeleteByAnnId(TableNames.Ann, annId);
                return true;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка в RollbackDraftAsync для AnnID={annId}");
                return false;
            }
        }

        /// <summary>
        /// Отвязывает список артикулов от указанного РТ и возвращает количество успешно обработанных строк.
        /// </summary>
        public async Task<UnbindArticlesResult> UnbindArticlesAsync(ArtNormN selectedAnn, IEnumerable<NZPByKoddRt> itemsToUnbind)
        {
            if (selectedAnn == null)
            {
                return new UnbindArticlesResult { Success = false, Error = "Не удалось определить выбранное разделение труда." };
            }

            var items = itemsToUnbind?.ToList() ?? new List<NZPByKoddRt>();
            if (items.Count == 0)
            {
                return new UnbindArticlesResult { Success = true, AffectedRows = 0 };
            }

            int totalAffectedRows = 0;
            try
            {
                foreach (var nzpItem in items)
                {
                    string kod = nzpItem.kodd.ToString();
                    int annIdNzpRow = nzpItem.annId;
                    string articul = nzpItem.articul?.TrimEnd(' ') ?? string.Empty;

                    await _logger.LogEventAsync($"Отвязка артикула KOD: {kod}, articul: {articul}, AnnID: {annIdNzpRow}", "UnbindArticles");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@annID", annIdNzpRow },
                        { "@kod", kod },
                        { "@art", articul }
                    };

                    await _dbService.UpdateFieldAsync(TableNames.Art, "annId", string.Empty, "left(kod, 7) = @kod AND articul = @art AND annID = @annId", parameters);
                    await _dbService.UpdateFieldAsync(TableNames.Ann, "size_label", null, TableNames.AnnId, annIdNzpRow);
                    await _dbService.UpdateFieldAsync(TableNames.Ann, "status", (int)Status.Actual, "parentId", annIdNzpRow);

                    totalAffectedRows++;
                }

                await _logger.LogEventAsync(
                    $"Отвязано {totalAffectedRows} артикулов от РТ. AnnID: {selectedAnn.AnnID}",
                    "UnbindArticles");

                return new UnbindArticlesResult { Success = true, AffectedRows = totalAffectedRows };
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка в UnbindArticlesAsync");
                return new UnbindArticlesResult { Success = false, Error = ex.Message, AffectedRows = totalAffectedRows };
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

    public class DuplicateDraftResult
    {
        public bool Success { get; set; }
        public int NewAnnId { get; set; }
        public ArtNormN DraftAnn { get; set; }
        public string Error { get; set; }
    }

    public class UnbindArticlesResult
    {
        public bool Success { get; set; }
        public int AffectedRows { get; set; }
        public string Error { get; set; }
    }
}
