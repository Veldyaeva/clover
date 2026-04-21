using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SewingProduction.Features.TeamWork.Models.UseCases;
using SewingProduction.Models;

namespace SewingProduction.Features.TeamWork.Services
{
    public interface ITeamWorkOrchestrator
    {
        Task<TeamWorkReloadResult> LoadWorkDivisionsWithFocusAsync(int? currentAnnId = null);
        Task<ArtNormN> LoadWorkDivisionAsync(int annId);
        Task<RelatedDataResult> RefreshRelatedDataAsync(int annId);
        Task<DuplicateDraftResult> CreateWorkDivisionDraftAsync(ArtNormN draft);
        Task<DuplicateDraftResult> CreateDuplicateDraftAsync(ArtNormN sourceAnn);
        Task<bool> RollbackDraftAsync(int annId);
        Task<BindArticleResult> BindArticleAsync(MyDataANN selectedAnn, MyDataART selectedArt);
        Task<UnbindArticlesResult> UnbindArticlesAsync(ArtNormN selectedAnn, IEnumerable<NZPByKoddRt> itemsToUnbind);
        Task<ApproveWorkDivisionResult> ApproveWorkDivisionAsync(int annId, string art);
        Task<BatchStatusUpdateResult> ArchiveWorkDivisionsAsync(IEnumerable<int> annIds);
        Task<BatchStatusUpdateResult> RestoreWorkDivisionsFromArchiveAsync(IEnumerable<int> annIds);
        Task<ArchAndCopyFinalizeResult> FinalizeArchAndCopyAsync(int sourceAnnId, int newAnnId, bool hasNzp);
        Task<bool> RollbackArchAndCopyAsync(int sourceAnnId, int? oldStatus, int? newAnnId);
        Task<MarkForDeletionBatchResult> MarkWorkDivisionsForDeletionAsync(IEnumerable<int> annIds, DateTime currentDate, string computerName);
        Task<ApproveBatchResult> ApproveWorkDivisionsBatchAsync(IEnumerable<int> annIds);
        Task<SpArticulArchUpdateResult> UpdateSpArticulArchAsync(int annId, string kod, string articul);
    }
}
