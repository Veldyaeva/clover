using SewingProduction.Features.TeamWork.Models.UseCases;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SewingProduction.Features.TeamWork.Services
{
    public interface ITeamWorkRepository
    {
        Task<List<FioModel>> GetDesignersAsync();
        Task<List<ArtNormN>> GetWorkDivisionsAsync();
        Task<ArtNormN> GetWorkDivisionAsync(int annId);
        Task<RelatedDataResult> GetRelatedDataAsync(int annId);
        Task<int[]> GetWorkingBrigIdsAsync(int annId);
        Task<string> TryBuildApprovalDiffAsync(int annId, DateTime approvedAt);
    }

    public interface ITeamWorkUnitOfWork
    {
        Task<int> CreateDraftAsync(ArtNormN draft);
        Task BindArticleAsync(MyDataANN selectedAnn, MyDataART selectedArt);
        Task RollbackDraftAsync(int annId);
        Task UnbindArticleAsync(int annIdNzpRow, string kod, string articul);
        Task MarkApprovedAsync(int annId, DateTime approvedAt);
        Task FinalizeArchAndCopyAsync(int sourceAnnId, int newAnnId, bool hasNzp);
        Task RollbackArchAndCopyAsync(int sourceAnnId, int? oldStatus, int? newAnnId);
        Task<int> MarkForDeletionAsync(int annId, DateTime currentDate, string computerName);
        Task UpdateStatusAsync(int annId, int status);
        Task UpdateSpArticulArchAsync(int annId, string kod, string articul);
    }

    public interface ITeamWorkNotificationService
    {
        Task NotifyBrigadesAsync(IEnumerable<int> brigIds, string message);
    }

    public interface ITeamWorkTransactionBoundary
    {
        Task ExecuteInTransactionAsync(Func<Task> operation);
    }

    public sealed class TeamWorkRepositoryAdapter : ITeamWorkRepository
    {
        private readonly ArtNormRepository _artNormRepository;
        private readonly DbService _dbService;
        private readonly DatabaseHelper _dbHelper;
        private readonly ILogger _logger;

        public TeamWorkRepositoryAdapter(
            ArtNormRepository artNormRepository,
            DbService dbService,
            DatabaseHelper dbHelper,
            ILogger logger)
        {
            _artNormRepository = artNormRepository;
            _dbService = dbService;
            _dbHelper = dbHelper;
            _logger = logger;
        }

        public Task<List<FioModel>> GetDesignersAsync() => _artNormRepository.GetRelDesigner();

        public Task<List<ArtNormN>> GetWorkDivisionsAsync() => _artNormRepository.GetArtNormData();

        public Task<ArtNormN> GetWorkDivisionAsync(int annId) => _artNormRepository.GetArtNormDataById(annId);

        public async Task<RelatedDataResult> GetRelatedDataAsync(int annId)
        {
            var normRaszTask = _artNormRepository.GetRelatedNormRasz(annId);
            var normRaskTask = _artNormRepository.GetRelatedNormRask(annId);
            var normKontTask = _artNormRepository.GetRelatedNormKont(annId);
            await Task.WhenAll(normRaszTask, normRaskTask, normKontTask);

            return new RelatedDataResult
            {
                Success = true,
                NormRasz = await normRaszTask,
                NormRask = await normRaskTask,
                NormKont = await normKontTask
            };
        }

        public async Task<int[]> GetWorkingBrigIdsAsync(int annId)
        {
            var brigades = await _artNormRepository.GetWorkingBrigs(annId);
            return brigades?
                .Select(b => b.id_brig)
                .Where(id => id > 0)
                .Distinct()
                .ToArray()
                ?? Array.Empty<int>();
        }

        public async Task<string> TryBuildApprovalDiffAsync(int annId, DateTime approvedAt)
        {
            var snapshotService = new RtSnapshotService(_dbService, _dbHelper, _logger);
            if (!await snapshotService.HasPendingAsync(annId))
                return null;

            return await snapshotService.CompareWithCurrentAsync(annId, approvedAt);
        }
    }

    public sealed class TeamWorkUnitOfWorkAdapter : ITeamWorkUnitOfWork
    {
        private readonly ArtNormRepository _artNormRepository;
        private readonly DbService _dbService;
        private readonly DatabaseHelper _dbHelper;

        public TeamWorkUnitOfWorkAdapter(
            ArtNormRepository artNormRepository,
            DbService dbService,
            DatabaseHelper dbHelper)
        {
            _artNormRepository = artNormRepository;
            _dbService = dbService;
            _dbHelper = dbHelper;
        }

        public Task<int> CreateDraftAsync(ArtNormN draft) =>
            _dbService.InsertEntityAsync(TableNames.Ann, TableNames.AnnId, draft);

        public async Task BindArticleAsync(MyDataANN selectedAnn, MyDataART selectedArt)
        {
            _artNormRepository.UpdateAnnIdinArticul(selectedAnn.AnnID, selectedArt.kodd, selectedArt.kodd_rt, selectedArt.Articul);
            await _dbService.UpdateEntityAsync(TableNames.Ann, TableNames.AnnId, selectedAnn);
            await _dbHelper.ExecuteQueryAsync(
                "dbo.updateSebZArticulPsz",
                new Dictionary<string, object> { { "@xAnnID", selectedAnn.AnnID } },
                CommandType.StoredProcedure);
        }

        public async Task RollbackDraftAsync(int annId)
        {
            await _artNormRepository.DeleteRelatedNormTables(annId);
            await _artNormRepository.DeleteByAnnId(TableNames.Ann, annId);
        }

        public async Task UnbindArticleAsync(int annIdNzpRow, string kod, string articul)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@annID", annIdNzpRow },
                { "@kod", kod },
                { "@art", articul }
            };

            await _dbService.UpdateFieldAsync(TableNames.Art, "annId", string.Empty, "left(kod, 7) = @kod AND articul = @art AND annID = @annId", parameters);
            await _dbService.UpdateFieldAsync(TableNames.Ann, "size_label", null, TableNames.AnnId, annIdNzpRow);
            await _dbService.UpdateFieldAsync(TableNames.Ann, "status", (int)Status.Actual, "parentId", annIdNzpRow);
        }

        public async Task MarkApprovedAsync(int annId, DateTime approvedAt)
        {
            await _dbHelper.ExecuteQueryAsync("dbo.updateSebZArticulPsz", new Dictionary<string, object> { { "@xAnnID", annId } }, CommandType.StoredProcedure);
            await _dbService.UpdateFieldAsync(TableNames.Ann, "data_obn", approvedAt, TableNames.AnnId, annId);
            await _dbService.UpdateFieldAsync(TableNames.Ann, "status", (int)Status.Actual, TableNames.AnnId, annId);
        }

        public async Task FinalizeArchAndCopyAsync(int sourceAnnId, int newAnnId, bool hasNzp)
        {
            int newStatus = hasNzp ? (int)Status.PreliminaryArchive : (int)Status.Archive;
            await _dbService.UpdateFieldAsync(TableNames.Ann, "Status", newStatus, TableNames.AnnId, sourceAnnId);
            if (!hasNzp)
            {
                await _dbService.UpdateFieldAsync("sp_Articul", "annId", newAnnId, "annId", sourceAnnId);
            }
        }

        public async Task RollbackArchAndCopyAsync(int sourceAnnId, int? oldStatus, int? newAnnId)
        {
            if (oldStatus.HasValue && sourceAnnId > 0)
            {
                await _dbService.UpdateFieldAsync(TableNames.Ann, "Status", oldStatus.Value, TableNames.AnnId, sourceAnnId);
            }

            if (newAnnId.HasValue && newAnnId.Value > 0)
            {
                await _artNormRepository.DeleteByAnnId(TableNames.Ann, newAnnId.Value);
            }
        }

        public Task<int> MarkForDeletionAsync(int annId, DateTime currentDate, string computerName) =>
            _dbHelper.ExecuteNonQueryWithRowCountAsync(
                "UPDATE art_norm_n SET annDateDel = @currentDate, annCompDel = @computerName WHERE annID = @annID",
                new Dictionary<string, object>
                {
                    { "@annID", annId },
                    { "@currentDate", currentDate },
                    { "@computerName", computerName }
                });

        public Task UpdateStatusAsync(int annId, int status) =>
            _dbService.UpdateFieldAsync(TableNames.Ann, "Status", status, TableNames.AnnId, annId);

        public Task UpdateSpArticulArchAsync(int annId, string kod, string articul) =>
            _dbHelper.ExecuteNonQueryAsync(
                @"UPDATE sa
                    SET sa.arh = 1
                    FROM dbo.sp_articul AS sa
                    JOIN dbo.art_norm_n AS ann ON ann.AnnID = sa.AnnID
                    WHERE left(sa.kod, 7) = @kod
                    AND sa.articul = @art
                    AND ann.annID = @annId",
                new Dictionary<string, object>
                {
                    { "@annId", annId },
                    { "@art", articul },
                    { "@kod", kod }
                });
    }

    public sealed class TeamWorkNotificationServiceAdapter : ITeamWorkNotificationService
    {
        private readonly IJabberSender _jabberSender;

        public TeamWorkNotificationServiceAdapter(IJabberSender jabberSender)
        {
            _jabberSender = jabberSender;
        }

        public Task NotifyBrigadesAsync(IEnumerable<int> brigIds, string message) =>
            _jabberSender.SendToBrigsAsync(brigIds, message);
    }

    public sealed class TeamWorkTransactionBoundaryAdapter : ITeamWorkTransactionBoundary
    {
        private readonly DatabaseHelper _dbHelper;

        public TeamWorkTransactionBoundaryAdapter(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public Task ExecuteInTransactionAsync(Func<Task> operation) =>
            _dbHelper.ExecuteInTransactionAsync(operation);
    }
}
