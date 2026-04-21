using System;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    public class KnitterWorkSpaceService : IKnitterWorkSpaceService
    {
        private readonly IKnitterShiftGateway _shiftGateway;
        private readonly ILogger _logger;
        private readonly StartKnitterShiftUseCase _startShiftUseCase;
        private readonly CloseKnitterShiftUseCase _closeShiftUseCase;

        public KnitterWorkSpaceService(IKnitterShiftGateway shiftGateway, ILogger? logger = null)
        {
            _shiftGateway = shiftGateway ?? throw new ArgumentNullException(nameof(shiftGateway));
            _logger = logger ?? new FileLogger();
            _startShiftUseCase = new StartKnitterShiftUseCase(_shiftGateway, _logger);
            _closeShiftUseCase = new CloseKnitterShiftUseCase(_shiftGateway, _logger);
        }

        public async Task<StartShiftResult> StartShiftAsync(StartShiftCommand command)
        {
            return await _startShiftUseCase.ExecuteAsync(command).ConfigureAwait(false);
        }

        public async Task<CloseShiftResult> CloseShiftAsync(CloseShiftCommand command)
        {
            return await _closeShiftUseCase.ExecuteAsync(command).ConfigureAwait(false);
        }
    }
}
