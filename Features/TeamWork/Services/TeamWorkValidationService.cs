using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SewingProduction.Models;

namespace SewingProduction.Features.TeamWork.Services
{
    /// <summary>
    /// Валидации, связанные с нумерацией операций и прочими бизнес-правилами TeamWork.
    /// </summary>
    public class TeamWorkValidationService : ITeamWorkValidationService
    {
        private readonly HashSet<(int N, int N1)> _seen = new HashSet<(int N, int N1)>();

        public ValidationResult ValidateOperationNumbers(List<NormRasz> operations)
        {
            if (operations == null) return ValidationResult.Success;
            _seen.Clear();
            foreach (var op in operations)
            {
                var key = (op.N, op.N1);
                if (!_seen.Add(key))
                {
                    return new ValidationResult($"Дублирующийся номер операции: {op.N}.{op.N1}");
                }
            }
            return ValidationResult.Success;
        }

        public bool HasDuplicateNumbers(int n, int n1)
        {
            var key = (n, n1);
            return _seen.Contains(key);
        }
    }
}


