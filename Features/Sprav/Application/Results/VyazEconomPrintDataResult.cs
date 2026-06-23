using SewingProduction.Features.Sprav.Application.Models.Print;

namespace SewingProduction.Features.Sprav.Application.Results
{
    public sealed class VyazEconomPrintDataResult
    {
        public bool Success { get; init; }
        public string? ErrorMessage { get; init; }
        public VyazEconomPrintDto? Data { get; init; }

        public static VyazEconomPrintDataResult Ok(VyazEconomPrintDto data) =>
            new() { Success = true, Data = data };

        public static VyazEconomPrintDataResult Fail(string message) =>
            new() { Success = false, ErrorMessage = message };
    }
}
