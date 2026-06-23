using SewingProduction.Features.Sprav.Application.Contexts;
using SewingProduction.Features.Sprav.Application.Results;
using SewingProduction.Features.Sprav.Application.Services;
using SewingProduction.Features.Sprav.Application.Validation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SewingProduction.Features.Sprav.Application.UseCases
{
    public sealed class MarkVyazEconomPrintedUseCase
    {
        private readonly VyazEconomPrintValidator _validator;
        private readonly IVyazEconomPrintDataService _dataService;

        public MarkVyazEconomPrintedUseCase(
            VyazEconomPrintValidator validator,
            IVyazEconomPrintDataService dataService)
        {
            _validator = validator;
            _dataService = dataService;
        }

        public async Task<OperationResult> ExecuteAsync(
            VyazEconomPrintContext context,
            CancellationToken ct = default)
        {
            var validation = _validator.Validate(context);
            if (!validation.Success)
            {
                return validation;
            }

            await _dataService.MarkDateEconomAsync(context.Nn, DateTime.Today, ct);
            return OperationResult.Ok();
        }
    }
}
