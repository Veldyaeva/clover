namespace SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Results
{
    public sealed class OperationResult
    {
        public bool Success { get; init; }
        public string? ErrorMessage { get; init; }

        public static OperationResult Ok() => new() { Success = true };
        public static OperationResult Fail(string message) => new() { Success = false, ErrorMessage = message };
    }
}