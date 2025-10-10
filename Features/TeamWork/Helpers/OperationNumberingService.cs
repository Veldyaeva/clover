using System.Collections.Generic;
using System.Linq;
using DevExpress.Mvvm.Native;
using SewingProduction.Models;

namespace SewingProduction.Features.TeamWork.Helpers
{
	public static class OperationNumberingService
	{
        public static void RecalculateAllOperationNumbers(IList<NormRasz> items)
        {
            if (items == null || items.Count == 0) return;

            // Группируем по текущему N, учитывая ВСЕ элементы (включая N1 >= 100),
            // чтобы пересчитать N для всей главы;
            // при этом для элементов с N1 >= 100 не меняем N1, только N.
            var groupedAll = items
                .GroupBy(r => r.N)
                .OrderBy(g => g.Key)
                .ToList();

            int currentN = 1;
            foreach (var group in groupedAll)
            {
                // Обычные операции (N1 < 100) — перенумеровываем внутри главы
                var normal = group.Where(r => r.N1 < 100).OrderBy(r => r.N1).ToList();
                // Специальные операции (N1 >= 100) — только обновляем N, N1 оставляем как есть
                var special = group.Where(r => r.N1 >= 100).ToList();

                // 1) Проставляем N и N1 для обычных операций
                for (int i = 0; i < normal.Count; i++)
                {
                    var op = normal[i];
                    int oldN = op.N;
                    int oldN1 = op.N1;
                    op.N = currentN;
                    op.N1 = normal.Count == 1 ? 0 : i + 1;
                    if (!op.IsNew && (op.N != oldN || op.N1 != oldN1)) op.IsModified = true;
                }

                // 2) Для специальных — меняем только N
                foreach (var op in special)
                {
                    int oldN = op.N;
                    if (op.N != currentN)
                    {
                        op.N = currentN;
                        if (!op.IsNew && op.N != oldN) op.IsModified = true;
                    }
                }

                currentN++;
            }
        }

        public static void MoveBlockWithinSameGroup(IList<NormRasz> items, IList<NormRasz> block, NormRasz target)
        {
            if (items == null || block == null || block.Count == 0 || target == null) return;
            int groupN = target.N;
            var group = items.Where(x => x.N == groupN && x.N1 < 100) // исключаем спецоперации)
                                                                       .OrderBy(x => x.N1).ToList();
            var exclude = new HashSet<NormRasz>(block);
            group = group.Where(x => !exclude.Contains(x)).ToList();
            int insertIndex = System.Math.Max(0, group.IndexOf(target) + 1);
            group.InsertRange(insertIndex, block);
            // Перенумеровываем только обычные операции (N1 < 100)
            var normal = group.Where(g => g.N1 < 100).ToList();
            if (normal.Count == 1)
            {
                int oldN1 = normal[0].N1;
                normal[0].N1 = 0;
                if (!normal[0].IsNew && normal[0].N1 != oldN1) normal[0].IsModified = true;
            }
            else
            {
                for (int i = 0; i < normal.Count; i++)
                {
                    int oldN1 = normal[i].N1;
                    normal[i].N1 = i + 1;
                    if (!normal[i].IsNew && normal[i].N1 != oldN1) normal[i].IsModified = true;
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
            var normalMaxN1 = scope.Where(x => x.N == targetGroupN && !object.ReferenceEquals(x, item) && x.N1 < 100)
                                    .Select(x => (int?)x.N1)
                                    .DefaultIfEmpty(null)
                                    .Max();
            if (item.N1 >= 100)
            {
                // Спецоперации сохраняют свой N1 при перемещении
            }
            else if (groupItems.Count == 0)
            {
                item.N1 = 0;
            }
            else if (desiredAfterN1.HasValue)
            {
                item.N1 = desiredAfterN1.Value + 1;
            }
            else
            {
                if (!normalMaxN1.HasValue)
                {
                    // В целевой главе нет обычных операций — вставляем как главу (N1=0)
                    item.N1 = 0;
                }
                else
                {
                    item.N1 = normalMaxN1.Value + 1;
                }
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
            if (newItem.N1 < 100)
            {
                newItem.N1 = 0;
            }
            //RecalculateAllOperationNumbers(items);
        }

        // Вставка основной операции как новой главы №1: смещение всех существующих глав вниз на 1.
        // Для специальных операций (N1 >= 100) меняется только N (N1 сохраняется).
        public static void InsertMainAtStart(IList<NormRasz> items, NormRasz newItem)
        {
            if (items == null || newItem == null) return;
            var chapterGroups = items
                .GroupBy(r => r.N)
                .OrderByDescending(g => g.Key)
                .ToList();
            foreach (var g in chapterGroups)
            {
                int newN = g.Key + 1;
                foreach (var op in g)
                {
                    int oldN = op.N;
                    if (op.N != newN)
                    {
                        op.N = newN;
                        if (!op.IsNew && op.N != oldN) op.IsModified = true;
                    }
                }
            }
            newItem.N = 1;
            if (newItem.N1 < 100)
            {
                newItem.N1 = 0;
            }
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
            foreach (var op in items.Where(r => r.N == operationN && !object.ReferenceEquals(r, newItem) && r.N1 < 100 && r.N1 >= insertN1).OrderBy(r => r.N1))
            {
                int oldN1 = op.N1;
                op.N1 += 1;
                if (!op.IsNew && op.N1 != oldN1) op.IsModified = true;
            }
            newItem.N = operationN;
            if (newItem.N1 < 100)
            {
                newItem.N1 = insertN1;
            }
            if (!items.Contains(newItem))
            {
                items.Add(newItem);
            }
            RecalculateAllOperationNumbers(items);
        }

    }
}


