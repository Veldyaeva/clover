namespace SewingProduction.Models
{
    internal static class StringNormalizer
    {
        public static string TrimOrNull(string value)
        {
            return value?.Trim();
        }
    }
}
