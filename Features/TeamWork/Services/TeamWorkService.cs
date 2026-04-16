using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.Features.TeamWork.Models.UseCases;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;

namespace SewingProduction.Features.TeamWork.Services
{
    /// <summary>
    /// Сервис уровня приложения для TeamWork: orchestration/координация загрузки и обновления связанных данных.
    /// Не выполняет SQL напрямую (делегирует ArtNormRepository), не содержит UI.
    /// </summary>
    public class TeamWorkOrchestrator : ITeamWorkOrchestrator
    {
        private readonly ArtNormRepository _artNormService;
        private readonly DbService _dbService;
        private readonly DatabaseHelperSQL _dbHelper;
        private readonly IJabberSender _jabberSender;
        private readonly ILogger _logger;

        public TeamWorkOrchestrator(ArtNormRepository artNormService, DbService dbService, DatabaseHelperSQL dbHelper, IJabberSender jabberSender, ILogger logger)
        {
            _artNormService = artNormService;
            _dbService = dbService;
            _dbHelper = dbHelper;
            _jabberSender = jabberSender;
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
                    string articul = StringNormalizer.TrimEndOrEmpty(nzpItem.articul, ' ');

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

        /// <summary>
        /// Утверждает РТ: пересчет, обновление статуса, построение diff и рассылка уведомления бригадам.
        /// </summary>
        public async Task<ApproveWorkDivisionResult> ApproveWorkDivisionAsync(int annId, string art)
        {
            if (annId <= 0)
            {
                return new ApproveWorkDivisionResult { Success = false, Error = "Некорректный AnnID." };
            }

            try
            {
                var parameters = new Dictionary<string, object> { { "@xAnnID", annId } };
                await _dbHelper.ExecuteQueryAsync("dbo.updateSebZArticulPsz", parameters, CommandType.StoredProcedure);
                await _dbService.UpdateFieldAsync(TableNames.Ann, "data_obn", DateTime.Now, TableNames.AnnId, annId);
                await _dbService.UpdateFieldAsync(TableNames.Ann, "status", (int)Status.Actual, TableNames.AnnId, annId);

                string diffText = await TryBuildApprovalDiffAsync(annId);
                string message = ComposeApprovalMessage(art, diffText);
                await SendToBrigadesAsync(annId, message);

                return new ApproveWorkDivisionResult
                {
                    Success = true,
                    ApprovedAt = DateTime.Now,
                    Status = (int)Status.Actual,
                    StatusText = "Актуальное",
                    Message = message
                };
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка утверждения РТ AnnID={annId}");
                return new ApproveWorkDivisionResult { Success = false, Error = ex.Message };
            }
        }

        /// <summary>
        /// Пакетно переводит записи РТ в архивный статус.
        /// </summary>
        public async Task<BatchStatusUpdateResult> ArchiveWorkDivisionsAsync(IEnumerable<int> annIds)
        {
            return await UpdateStatusesAsync(annIds, (int)Status.Archive);
        }

        /// <summary>
        /// Пакетно восстанавливает записи РТ из архива в предварительный статус.
        /// </summary>
        public async Task<BatchStatusUpdateResult> RestoreWorkDivisionsFromArchiveAsync(IEnumerable<int> annIds)
        {
            return await UpdateStatusesAsync(annIds, (int)Status.Preliminary);
        }

        /// <summary>
        /// Завершает сценарий "архив+копия": выставляет статус исходной записи и при необходимости переносит артикула.
        /// </summary>
        public async Task<ArchAndCopyFinalizeResult> FinalizeArchAndCopyAsync(int sourceAnnId, int newAnnId, bool hasNzp)
        {
            if (sourceAnnId <= 0 || newAnnId <= 0)
            {
                return new ArchAndCopyFinalizeResult { Success = false, Error = "Некорректные идентификаторы записей." };
            }

            try
            {
                int newStatus = hasNzp ? (int)Status.PreliminaryArchive : (int)Status.Archive;
                await _dbService.UpdateFieldAsync(TableNames.Ann, "Status", newStatus, TableNames.AnnId, sourceAnnId);
                if (!hasNzp)
                {
                    await _dbService.UpdateFieldAsync("sp_Articul", "annId", newAnnId, "annId", sourceAnnId);
                }

                return new ArchAndCopyFinalizeResult
                {
                    Success = true,
                    NewStatus = newStatus,
                    NewStatusText = StatusHelper.GetStatusText(newStatus)
                };
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка FinalizeArchAndCopyAsync source={sourceAnnId}, new={newAnnId}");
                return new ArchAndCopyFinalizeResult { Success = false, Error = ex.Message };
            }
        }

        /// <summary>
        /// Откатывает сценарий "архив+копия": восстанавливает старый статус и удаляет созданный черновик.
        /// </summary>
        public async Task<bool> RollbackArchAndCopyAsync(int sourceAnnId, int? oldStatus, int? newAnnId)
        {
            try
            {
                if (oldStatus.HasValue && sourceAnnId > 0)
                {
                    await _dbService.UpdateFieldAsync(TableNames.Ann, "Status", oldStatus.Value, TableNames.AnnId, sourceAnnId);
                }

                if (newAnnId.HasValue && newAnnId.Value > 0)
                {
                    await _artNormService.DeleteByAnnId(TableNames.Ann, newAnnId.Value);
                }

                return true;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка RollbackArchAndCopyAsync source={sourceAnnId}, new={newAnnId}");
                return false;
            }
        }

        /// <summary>
        /// Пакетно помечает РТ на удаление (annDateDel/annCompDel).
        /// </summary>
        public async Task<MarkForDeletionBatchResult> MarkWorkDivisionsForDeletionAsync(IEnumerable<int> annIds, DateTime currentDate, string computerName)
        {
            var result = new MarkForDeletionBatchResult();
            var ids = annIds?.Distinct().Where(id => id > 0).ToList() ?? new List<int>();
            if (ids.Count == 0)
            {
                result.Success = true;
                return result;
            }

            if (string.IsNullOrWhiteSpace(computerName))
            {
                result.Errors.Add("Не задано имя компьютера для пометки на удаление.");
                result.Success = false;
                return result;
            }

            const string sqlQuery = "UPDATE art_norm_n SET annDateDel = @currentDate, annCompDel = @computerName WHERE annID = @annID";

            foreach (var annId in ids)
            {
                try
                {
                    var parameters = new Dictionary<string, object>
                    {
                        { "@annID", annId },
                        { "@currentDate", currentDate },
                        { "@computerName", computerName }
                    };

                    int affectedRows = await _dbHelper.ExecuteNonQueryWithRowCountAsync(sqlQuery, parameters);
                    if (affectedRows > 0)
                    {
                        result.UpdatedAnnIds.Add(annId);
                    }
                    else
                    {
                        string error = $"AnnID: {annId} - не удалось обновить в БД";
                        result.Errors.Add(error);
                        await _logger.LogErrorAsync(new Exception("affectedRows = 0"), $"Не удалось пометить РТ на удаление. AnnID: {annId}");
                    }
                }
                catch (Exception ex)
                {
                    string error = $"AnnID: {annId} - {ex.Message}";
                    result.Errors.Add(error);
                    await _logger.LogErrorAsync(ex, $"Ошибка при пометке РТ на удаление. AnnID: {annId}");
                }
            }

            result.Success = result.Errors.Count == 0;
            return result;
        }

        /// <summary>
        /// Пакетно выполняет пересчет/утверждение РТ (эквивалент SetUpdateDate).
        /// </summary>
        public async Task<ApproveBatchResult> ApproveWorkDivisionsBatchAsync(IEnumerable<int> annIds)
        {
            var result = new ApproveBatchResult();
            var ids = annIds?.Distinct().Where(id => id > 0).ToList() ?? new List<int>();
            if (ids.Count == 0)
            {
                result.Success = true;
                return result;
            }

            foreach (var annId in ids)
            {
                try
                {
                    var approval = await ApproveWorkDivisionAsync(annId, string.Empty);
                    if (approval.Success)
                    {
                        result.UpdatedAnnIds.Add(annId);
                        result.Items.Add(new ApproveBatchItem
                        {
                            AnnId = annId,
                            ApprovedAt = approval.ApprovedAt,
                            Status = approval.Status,
                            StatusText = approval.StatusText
                        });
                    }
                    else
                    {
                        result.Errors.Add($"AnnID: {annId} - {approval.Error}");
                    }
                }
                catch (Exception ex)
                {
                    result.Errors.Add($"AnnID: {annId} - {ex.Message}");
                    await _logger.LogErrorAsync(ex, $"Ошибка batch-утверждения для AnnID: {annId}");
                }
            }

            result.Success = result.Errors.Count == 0;
            return result;
        }

        /// <summary>
        /// Обновляет поле arh в sp_articul для артикула, связанного с указанным AnnID.
        /// </summary>
        public async Task<SpArticulArchUpdateResult> UpdateSpArticulArchAsync(int annId, string kod, string articul)
        {
            if (annId <= 0 || string.IsNullOrWhiteSpace(kod) || string.IsNullOrWhiteSpace(articul))
            {
                return new SpArticulArchUpdateResult { Success = false, Error = "Некорректные входные данные для обновления arch." };
            }

            try
            {
                string sqlQuery = @"UPDATE sa
                    SET sa.arh = 1
                    FROM dbo.sp_articul AS sa
                    JOIN dbo.art_norm_n AS ann ON ann.AnnID = sa.AnnID
                    WHERE left(sa.kod, 7) = @kod
                    AND sa.articul = @art
                    AND ann.annID = @annId";

                var parameters = new Dictionary<string, object>
                {
                    { "@annId", annId },
                    { "@art", articul },
                    { "@kod", kod }
                };

                await _dbHelper.ExecuteNonQueryAsync(sqlQuery, parameters);
                await _logger.LogEventAsync($"Обновлено поле arch для записей AnnID: {annId}, Артикул: {articul}", "UpdateSpArticulArchAsync");
                return new SpArticulArchUpdateResult { Success = true };
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка UpdateSpArticulArchAsync для AnnID={annId}, kod={kod}, art={articul}");
                return new SpArticulArchUpdateResult { Success = false, Error = ex.Message };
            }
        }

        private async Task SendToBrigadesAsync(int annId, string message)
        {
            var brigades = await _artNormService.GetWorkingBrigs(annId);
            var brigIds = brigades?.Select(b => b.id_brig).Where(id => id > 0).Distinct().ToArray();
            if (brigIds is { Length: > 0 })
            {
                await _jabberSender.SendToBrigsAsync(brigIds, message);
                await _logger.LogEventAsync($"Отправлено '{message}' в {brigIds.Length} бригад(ы) для annId={annId}", "ApproveWorkDivisionAsync");
            }
            else
            {
                await _logger.LogEventAsync($"Бригад для рассылки не найдено (annId={annId})", "ApproveWorkDivisionAsync");
            }
        }

        private async Task<string> TryBuildApprovalDiffAsync(int annId)
        {
            var snapSvc = new RtSnapshotService(_dbService, _dbHelper, _logger);
            if (!await snapSvc.HasPendingAsync(annId))
                return null;

            var approvedAt = DateTime.Now;
            try
            {
                return await snapSvc.CompareWithCurrentAsync(annId, approvedAt);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка построения diff для AnnID: {annId}");
                return null;
            }
        }

        private static string ComposeApprovalMessage(string art, string diffText, int maxLen = 3800)
        {
            var intro = $"Внимание! Схема разделения {art} утверждена и обновлена технологом. Проверьте операции, прежде чем начать работу!";
            var full = string.IsNullOrWhiteSpace(diffText)
                ? intro
                : intro + Environment.NewLine + Environment.NewLine + diffText;

            if (full.Length <= maxLen) return full;

            const string tail = "\n...(сообщение обрезано)";
            return full.Substring(0, Math.Max(0, maxLen - tail.Length)) + tail;
        }

        private async Task<BatchStatusUpdateResult> UpdateStatusesAsync(IEnumerable<int> annIds, int status)
        {
            var result = new BatchStatusUpdateResult();
            var ids = annIds?.Distinct().Where(id => id > 0).ToList() ?? new List<int>();
            if (ids.Count == 0)
            {
                result.Success = true;
                return result;
            }

            foreach (var annId in ids)
            {
                try
                {
                    await _dbService.UpdateFieldAsync(TableNames.Ann, "Status", status, TableNames.AnnId, annId);
                    result.UpdatedAnnIds.Add(annId);
                }
                catch (Exception ex)
                {
                    string error = $"AnnID: {annId} - {ex.Message}";
                    result.Errors.Add(error);
                    await _logger.LogErrorAsync(ex, $"Ошибка UpdateStatusesAsync для AnnID: {annId}");
                }
            }
            result.Success = result.Errors.Count == 0;
            return result;
        }
    }

}
