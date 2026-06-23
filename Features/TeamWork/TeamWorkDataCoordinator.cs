using SewingProduction.Features.TeamWork.Models.UseCases;
using SewingProduction.Features.TeamWork.Services;
using SewingProduction.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Features.TeamWork
{
    public sealed class TeamWorkDataCoordinator
    {
        private readonly ITeamWorkOrchestrator _orchestrator;
        private readonly ILogger _logger;
        private CancellationTokenSource _loadCts = new CancellationTokenSource();

        public TeamWorkDataCoordinator(ITeamWorkOrchestrator orchestrator, ILogger logger)
        {
            _orchestrator = orchestrator ?? throw new ArgumentNullException(nameof(orchestrator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public CancellationToken CurrentToken => _loadCts?.Token ?? CancellationToken.None;

        // ─── CTS management ───────────────────────────────────────────────

        public CancellationToken StartNewLoad()
        {
            var old = Interlocked.Exchange(ref _loadCts, new CancellationTokenSource());
            if (old != null)
            {
                try { old.Cancel(); } catch { }
                finally { old.Dispose(); }
            }
            return _loadCts.Token;
        }

        public void CancelCurrentLoad()
        {
            var old = Interlocked.Exchange(ref _loadCts, null);
            if (old != null)
            {
                try { old.Cancel(); } catch { }
                finally { old.Dispose(); }
            }
        }

        public void CancelAllLoads()
        {
            try
            {
                var old = Interlocked.Exchange(ref _loadCts, new CancellationTokenSource());
                old?.Cancel();
                old?.Dispose();
            }
            catch { }
        }

        // ─── Data loading ──────────────────────────────────────────────────

        public async Task<TeamWorkReloadResult> LoadWorkDivisionsAsync(CancellationToken ct)
        {
            try
            {
                ct.ThrowIfCancellationRequested();
                var result = await _orchestrator.LoadWorkDivisionsWithFocusAsync();
                ct.ThrowIfCancellationRequested();
                return result ?? new TeamWorkReloadResult { Success = false, Error = "Оркестратор вернул null" };
            }
            catch (OperationCanceledException)
            {
                return null;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "TeamWorkDataCoordinator.LoadWorkDivisionsAsync");
                return new TeamWorkReloadResult { Success = false, Error = ex.Message };
            }
        }

        public async Task<RelatedDataResult> LoadRelatedDataAsync(int annId, CancellationToken ct)
        {
            try
            {
                if (annId <= 0)
                    return new RelatedDataResult { Success = false, Error = "Некорректный AnnID" };

                ct.ThrowIfCancellationRequested();
                var result = await _orchestrator.RefreshRelatedDataAsync(annId);
                ct.ThrowIfCancellationRequested();
                return result ?? new RelatedDataResult { Success = false, Error = "Оркестратор вернул null" };
            }
            catch (OperationCanceledException)
            {
                return null;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"TeamWorkDataCoordinator.LoadRelatedDataAsync annId={annId}");
                return new RelatedDataResult { Success = false, Error = ex.Message };
            }
        }
    }
}
