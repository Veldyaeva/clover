using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SewingProduction.Models
{
    internal static class StringNormalizer
    {
        private static readonly Regex MultiWhitespaceRegex = new Regex(@"\s+", RegexOptions.Compiled);
        private static readonly Regex NonCodeCharsRegex = new Regex(@"[^A-Z0-9]+", RegexOptions.Compiled);

        public static string TrimOrNull(string value)
        {
            return value?.Trim();
        }

        public static string TrimToNull(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        public static string TrimOrEmpty(string value)
        {
            return TrimOrNull(value) ?? string.Empty;
        }

        public static string TrimAndLimitOrNull(string value, int maxLength)
        {
            string normalized = TrimOrNull(value);
            if (normalized == null)
            {
                return null;
            }

            return normalized.Length > maxLength ? normalized.Substring(0, maxLength) : normalized;
        }

        public static string TrimEndOrNull(string value, params char[] trimChars)
        {
            if (value == null)
            {
                return null;
            }

            return trimChars == null || trimChars.Length == 0
                ? value.TrimEnd()
                : value.TrimEnd(trimChars);
        }

        public static string TrimEndOrEmpty(string value, params char[] trimChars)
        {
            return TrimEndOrNull(value, trimChars) ?? string.Empty;
        }

        public static string TrimStartOrNull(string value, params char[] trimChars)
        {
            if (value == null)
            {
                return null;
            }

            return trimChars == null || trimChars.Length == 0
                ? value.TrimStart()
                : value.TrimStart(trimChars);
        }

        public static string TrimStartOrEmpty(string value, params char[] trimChars)
        {
            return TrimStartOrNull(value, trimChars) ?? string.Empty;
        }

        public static string NormalizeUpperInvariant(string value)
        {
            return TrimOrEmpty(value).ToUpperInvariant();
        }

        public static string NormalizeLowerInvariant(string value)
        {
            return TrimOrEmpty(value).ToLowerInvariant();
        }

        public static string NormalizeWhitespace(string value)
        {
            string normalized = TrimOrEmpty(value);
            return string.IsNullOrEmpty(normalized) ? string.Empty : MultiWhitespaceRegex.Replace(normalized, " ");
        }

        public static string NormalizeWhitespaceLowerInvariant(string value)
        {
            string normalized = NormalizeLowerInvariant(value);
            return string.IsNullOrEmpty(normalized) ? string.Empty : MultiWhitespaceRegex.Replace(normalized, " ");
        }

        public static string NormalizeSlug(string value)
        {
            string normalized = NormalizeLowerInvariant(value);
            if (string.IsNullOrEmpty(normalized))
            {
                return string.Empty;
            }

            var chars = normalized
                .Select(ch => char.IsLetterOrDigit(ch) ? ch : '-')
                .ToArray();

            string slug = new string(chars);
            while (slug.Contains("--"))
            {
                slug = slug.Replace("--", "-");
            }

            return slug.Trim('-');
        }

        public static string NormalizeCodeToken(string value, string fallback = "NODE", int maxLength = int.MaxValue)
        {
            string source = string.IsNullOrWhiteSpace(value) ? fallback : NormalizeUpperInvariant(value);
            string normalized = NonCodeCharsRegex.Replace(source, "_").Trim('_');

            if (string.IsNullOrWhiteSpace(normalized))
            {
                normalized = fallback;
            }

            return normalized.Length > maxLength ? normalized.Substring(0, maxLength) : normalized;
        }

        public static string ToSentenceCase(string value)
        {
            string normalized = TrimOrEmpty(value);
            if (string.IsNullOrEmpty(normalized))
            {
                return normalized;
            }

            return char.ToUpper(normalized[0]) + normalized.Substring(1);
        }

        public static bool EqualsTrimmed(string left, string right, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return string.Equals(TrimOrEmpty(left), TrimOrEmpty(right), comparison);
        }
    }
}
