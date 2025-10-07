using System.Collections.Generic;
using System.Linq;
using SewingProduction.Models;

namespace SewingProduction.Features.TeamWork.Helpers
{
    public static class OperationNumberingService
    {
        public static void RecalculateAllOperationNumbers(IList<NormRasz> items)
        {
            if (items == null || items.Count == 0) return;
            var grouped = items
                .GroupBy(r => r.N)
                .OrderBy(g => g.Key)
                .ToList();
            int currentN = 1;
            foreach (var group in grouped)
            {
                var operations = group.OrderBy(r => r.N1).ToList();
                for (int i = 0; i < operations.Count; i++)
                {
                    var op = operations[i];
                    int oldN = op.N;
                    int oldN1 = op.N1;
                    op.N = currentN;
                    op.N1 = operations.Count == 1 ? 0 : i + 1;
                    if (!op.IsNew && (op.N != oldN || op.N1 != oldN1)) op.IsModified = true;
                }
                currentN++;
            }
        }

        public static void MoveBlockWithinSameGroup(IList<NormRasz> items, IList<NormRasz> block, NormRasz target)
        {
            if (items == null || block == null || block.Count == 0 || target == null) return;
            int groupN = target.N;
            var group = items.Where(x => x.N == groupN).OrderBy(x => x.N1).ToList();
            var exclude = new HashSet<NormRasz>(block);
            group = group.Where(x => !exclude.Contains(x)).ToList();
            int insertIndex = System.Math.Max(0, group.IndexOf(target) + 1);
            group.InsertRange(insertIndex, block);
            if (group.Count == 1)
            {
                int oldN1 = group[0].N1;
                group[0].N1 = 0;
                if (!group[0].IsNew && group[0].N1 != oldN1) group[0].IsModified = true;
            }
            else
            {
                for (int i = 0; i < group.Count; i++)
                {
                    int oldN1 = group[i].N1;
                    group[i].N1 = i + 1;
                    if (!group[i].IsNew && group[i].N1 != oldN1) group[i].IsModified = true;
                }
            }
        }

        public static void MoveRaszIntoGroup(NormRasz item, int targetGroupN, int? desiredAfterN1, IEnumerable<NormRasz> scope)
        {
            if (item == null || scope == null) return;
            int oldN = item.N;
            int oldN1Item = item.N1;
            if (item.N != targetGroupN) item.N = targetGroupN;
            var groupItems = scope.Where(x => x.N == targetGroupN && !object.ReferenceEquals(x, item)).ToList();
            if (groupItems.Count == 0)
            {
                item.N1 = 0;
            }
            else if (desiredAfterN1.HasValue)
            {
                item.N1 = desiredAfterN1.Value + 1;
            }
            else
            {
                item.N1 = groupItems.Max(x => x.N1) + 1;
            }
            if (!item.IsNew && (item.N != oldN || item.N1 != oldN1Item)) item.IsModified = true;
        }

        // Вставка основной операции после указанной главы (сдвиг последующих глав)
        public static void InsertMainAfter(IList<NormRasz> items, int afterN, NormRasz newItem)
        {
            if (items == null || newItem == null) return;
            foreach (var op in items)
            {
                if (op.N > afterN)
                {
                    int oldN = op.N;
                    op.N += 1;
                    if (!op.IsNew && op.N != oldN) op.IsModified = true;
                }
            }
            newItem.N = afterN + 1;
            newItem.N1 = 0;
            //RecalculateAllOperationNumbers(items);
        }

        // Вставка подоперации (с возможным преобразованием основной в первую подоперацию)
        public static void InsertSuboperation(IList<NormRasz> items, int operationN, int insertN1, bool convertMainToSub, NormRasz newItem)
        {
            if (items == null || newItem == null) return;
            if (convertMainToSub)
            {
                var main = items.FirstOrDefault(r => r.N == operationN && r.N1 == 0);
                if (main != null)
                {
                    int oldN1 = main.N1;
                    main.N1 = 1;
                    if (!main.IsNew && main.N1 != oldN1) main.IsModified = true;
                }
            }
            // Сдвигаем подоперации начиная с insertN1
            foreach (var op in items.Where(r => r.N == operationN && r.N1 >= insertN1).OrderByDescending(r => r.N1))
            {
                int oldN1 = op.N1;
                op.N1 += 1;
                if (!op.IsNew && op.N1 != oldN1) op.IsModified = true;
            }
            newItem.N = operationN;
            newItem.N1 = insertN1;
            RecalculateAllOperationNumbers(items);
        }

        // Перенумерация после удаления
        public static void RenumberAfterDeletion(IList<NormRasz> items, int deletedN, int deletedN1)
        {
            if (items == null) return;
            if (deletedN1 == 0)
            {
                foreach (var op in items.Where(r => r.N > deletedN))
                {
                    int oldN = op.N;
                    op.N -= 1;
                    if (!op.IsNew && op.N != oldN) op.IsModified = true;
                }
            }
            else
            {
                foreach (var op in items.Where(r => r.N == deletedN && r.N1 > deletedN1))
                {
                    int oldN1 = op.N1;
                    op.N1 -= 1;
                    if (!op.IsNew && op.N1 != oldN1) op.IsModified = true;
                }
                var remaining = items.Where(r => r.N == deletedN && r.N1 > 0).ToList();
                if (remaining.Count == 1)
                {
                    int oldN1 = remaining[0].N1;
                    remaining[0].N1 = 0;
                    if (!remaining[0].IsNew && remaining[0].N1 != oldN1) remaining[0].IsModified = true;
                }
            }
        }
    }
}


