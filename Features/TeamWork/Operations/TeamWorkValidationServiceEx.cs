using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SewingProduction.Models;

namespace SewingProduction.Features.TeamWork.Operations
{
    public static class TeamWorkValidationServiceEx
    {
        public static ValidationResult ValidateOperationNumbers(IEnumerable<NormRasz> rasz)
        {
            if (rasz == null) return ValidationResult.Success;
            var duplicates = rasz
                .GroupBy(r => new { r.N, r.N1 })
                .Where(g => g.Count() > 1)
                .ToList();
            if (!duplicates.Any()) return ValidationResult.Success;

            string msg = string.Join(", ", duplicates.Select(d => $"{d.Key.N}.{d.Key.N1}"));
            return new ValidationResult($"Дублирующаяся нумерация операций: {msg}");
        }

        public static IReadOnlyList<string> GetOperationNumberErrors(IEnumerable<NormRasz> rasz)
        {
            if (rasz == null) return new List<string>();
            var duplicates = rasz
                .GroupBy(r => new { r.N, r.N1 })
                .Where(g => g.Count() > 1)
                .Select(d => $"Дубликат номера {d.Key.N}.{d.Key.N1} (кол-во: {d.Count()})")
                .ToList();
            return duplicates;
        }
    }
}



