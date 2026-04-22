namespace SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Validation
{
    public sealed class PzvValidationResult
    {
        public bool IsValid { get; init; }
        public string? Message { get; init; }

        public static PzvValidationResult Ok() => new() { IsValid = true };
        public static PzvValidationResult Fail(string message) => new() { IsValid = false, Message = message };
    }
}