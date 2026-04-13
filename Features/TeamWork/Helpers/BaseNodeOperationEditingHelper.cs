using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Models;
using System.Collections.Generic;
using System.Linq;

namespace SewingProduction.Features.TeamWork.Helpers
{
    internal static class BaseNodeOperationEditingHelper
    {
        public static bool CanMove(IReadOnlyList<NormRasz> operations, int sourceIndex, int targetIndex)
        {
            if (!IsValidMove(operations?.Count ?? 0, sourceIndex, targetIndex))
            {
                return false;
            }

            return operations[sourceIndex].N == operations[targetIndex].N;
        }

        public static bool CanMove(IReadOnlyList<BaseNodeOperationDefinition> operations, int sourceIndex, int targetIndex)
        {
            if (!IsValidMove(operations?.Count ?? 0, sourceIndex, targetIndex))
            {
                return false;
            }

            return operations[sourceIndex].SourceN == operations[targetIndex].SourceN;
        }

        public static void Move(IList<NormRasz> operations, int sourceIndex, int targetIndex)
        {
            if (!CanMove(operations as IReadOnlyList<NormRasz>, sourceIndex, targetIndex))
            {
                return;
            }

            var item = operations[sourceIndex];
            operations.RemoveAt(sourceIndex);
            operations.Insert(targetIndex, item);
            Renumber(operations);
        }

        public static void Move(IList<BaseNodeOperationDefinition> operations, int sourceIndex, int targetIndex)
        {
            if (!CanMove(operations as IReadOnlyList<BaseNodeOperationDefinition>, sourceIndex, targetIndex))
            {
                return;
            }

            var item = operations[sourceIndex];
            operations.RemoveAt(sourceIndex);
            operations.Insert(targetIndex, item);
            Renumber(operations);
        }

        public static void RemoveAt(IList<NormRasz> operations, int index)
        {
            if (!IsValidIndex(operations?.Count ?? 0, index))
            {
                return;
            }

            operations.RemoveAt(index);
            Renumber(operations);
        }

        public static void RemoveAt(IList<BaseNodeOperationDefinition> operations, int index)
        {
            if (!IsValidIndex(operations?.Count ?? 0, index))
            {
                return;
            }

            operations.RemoveAt(index);
            Renumber(operations);
        }

        private static void Renumber(IList<NormRasz> operations)
        {
            if (operations == null || operations.Count == 0)
            {
                return;
            }

            // Сохраняем текущий порядок глав, но подпункты пересчитываем заново внутри каждой главы.
            var chapterMap = operations
                .Select(x => x.N)
                .Distinct()
                .Select((oldN, index) => new { oldN, newN = index + 1 })
                .ToDictionary(x => x.oldN, x => x.newN);

            foreach (var group in operations.GroupBy(x => x.N).ToList())
            {
                int normalizedN = chapterMap[group.Key];
                var chapterOperations = operations.Where(x => x.N == group.Key).ToList();
                var normalOperations = chapterOperations.Where(x => x.N1 < 100).ToList();
                int normalIndex = 1;

                foreach (var operation in chapterOperations)
                {
                    operation.N = normalizedN;
                    if (operation.N1 < 100)
                    {
                        operation.N1 = normalOperations.Count <= 1 ? 0 : normalIndex++;
                    }
                }
            }
        }

        private static void Renumber(IList<BaseNodeOperationDefinition> operations)
        {
            if (operations == null || operations.Count == 0)
            {
                return;
            }

            var chapterMap = operations
                .Select(x => x.SourceN)
                .Distinct()
                .Select((oldN, index) => new { oldN, newN = index + 1 })
                .ToDictionary(x => x.oldN, x => x.newN);

            foreach (var group in operations.GroupBy(x => x.SourceN).ToList())
            {
                int normalizedN = chapterMap[group.Key];
                var chapterOperations = operations.Where(x => x.SourceN == group.Key).ToList();
                var normalOperations = chapterOperations.Where(x => x.SourceN1 < 100).ToList();
                int normalIndex = 1;

                foreach (var operation in chapterOperations)
                {
                    operation.SourceN = normalizedN;
                    if (operation.SourceN1 < 100)
                    {
                        operation.SourceN1 = normalOperations.Count <= 1 ? 0 : normalIndex++;
                    }
                }
            }
        }

        private static bool IsValidMove(int count, int sourceIndex, int targetIndex)
        {
            return IsValidIndex(count, sourceIndex)
                && IsValidIndex(count, targetIndex)
                && sourceIndex != targetIndex;
        }

        private static bool IsValidIndex(int count, int index)
        {
            return index >= 0 && index < count;
        }
    }
}
