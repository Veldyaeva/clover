using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Models;
using System.Collections.Generic;
using System.Linq;

namespace SewingProduction.Features.TeamWork.Helpers
{
    public static class BaseNodeMapper
    {
        public static BaseNodeDefinition CreateDefinition(string name, string description, IEnumerable<NormRasz> operations)
        {
            var ordered = OrderOperations(operations).ToList();
            var normalizedOperations = NormalizeChapterNumbers(ordered.Select(MapOperation).ToList());

            return new BaseNodeDefinition
            {
                Name = name?.Trim() ?? string.Empty,
                Description = description?.Trim() ?? string.Empty,
                Operations = DeduplicateOperations(normalizedOperations).ToList()
            };
        }

        public static List<NormRasz> CreateOperations(BaseNodeDefinition node, int annId)
        {
            if (node?.Operations == null || node.Operations.Count == 0)
            {
                return new List<NormRasz>();
            }

            var ordered = node.Operations
                .OrderBy(x => x.SortOrder == 0 ? int.MaxValue : x.SortOrder)
                .ThenBy(x => x.SourceN)
                .ThenBy(x => x.SourceN1)
                .ToList();

            var groups = ordered
                .GroupBy(x => x.SourceN)
                .OrderBy(g => g.Key)
                .ToList();

            var result = new List<NormRasz>();
            int nextN = 1;

            foreach (var group in groups)
            {
                var groupItems = group
                    .OrderBy(x => x.SourceN1)
                    .ToList();

                var normalItems = groupItems.Where(x => x.SourceN1 < 100).ToList();
                int normalIndex = 1;

                foreach (var item in groupItems)
                {
                    int targetN1 = item.SourceN1;
                    if (item.SourceN1 < 100)
                    {
                        targetN1 = normalItems.Count <= 1 ? 0 : normalIndex++;
                    }

                    result.Add(new NormRasz
                    {
                        annId = annId,
                        nrID = 0,
                        N = nextN,
                        N1 = targetN1,
                        Kod = item.Kod,
                        kod_o = item.KodO,
                        Text = item.Text,
                        razryd = item.Razryd,
                        Sek = item.Sek,
                        Seb = item.Seb,
                        Obor = item.Obor,
                        Spec = item.Spec,
                        KodProizv = item.KodProizv,
                        KodPodr = item.KodPodr,
                        KodOb = item.KodOb,
                        TextOb = item.TextOb,
                        TextVyaz = item.TextVyaz,
                        TextProizv = item.TextProizv,
                        IsNew = true,
                        IsBeingAdded = true,
                        IsModified = false
                    });
                }

                nextN++;
            }

            return result;
        }

        public static List<BaseNodeOperationPreviewRow> CreatePreviewRows(BaseNodeDefinition node)
        {
            if (node?.Operations == null)
            {
                return new List<BaseNodeOperationPreviewRow>();
            }

            return node.Operations
                .OrderBy(x => x.SortOrder == 0 ? int.MaxValue : x.SortOrder)
                .ThenBy(x => x.SourceN)
                .ThenBy(x => x.SourceN1)
                .Select(x => new BaseNodeOperationPreviewRow
                {
                    Number = x.SourceN1 > 0 ? $"{x.SourceN}.{x.SourceN1}" : x.SourceN.ToString(),
                    Operation = x.Text,
                    Razryd = x.Razryd,
                    Sek = x.Sek,
                    Equipment = string.IsNullOrWhiteSpace(x.TextOb) ? x.Obor : x.TextOb
                })
                .ToList();
        }

        public static List<BaseNodeOperationPreviewRow> CreatePreviewRows(IEnumerable<NormRasz> operations)
        {
            return OrderOperations(operations)
                .Select(x => new BaseNodeOperationPreviewRow
                {
                    Number = x.DisplayNumber,
                    Operation = x.Text,
                    Razryd = x.razryd,
                    Sek = x.Sek,
                    Equipment = string.IsNullOrWhiteSpace(x.TextOb) ? x.Obor : x.TextOb
                })
                .ToList();
        }

        private static IEnumerable<NormRasz> OrderOperations(IEnumerable<NormRasz> operations)
        {
            return operations?
                .Where(x => x != null)
                .OrderBy(x => x.N)
                .ThenBy(x => x.N1) ?? Enumerable.Empty<NormRasz>();
        }

        private static BaseNodeOperationDefinition MapOperation(NormRasz operation)
        {
            return new BaseNodeOperationDefinition
            {
                SortOrder = 0,
                SourceN = operation.N,
                SourceN1 = operation.N1,
                Kod = operation.Kod ?? string.Empty,
                KodO = operation.kod_o ?? string.Empty,
                Text = operation.Text ?? string.Empty,
                Razryd = operation.razryd,
                Sek = operation.Sek,
                Seb = operation.Seb,
                Obor = operation.Obor ?? string.Empty,
                Spec = operation.Spec ?? string.Empty,
                KodProizv = operation.KodProizv,
                KodPodr = operation.KodPodr,
                KodOb = operation.KodOb,
                TextOb = operation.TextOb ?? string.Empty,
                TextVyaz = operation.TextVyaz ?? string.Empty,
                TextProizv = operation.TextProizv ?? string.Empty
            };
        }

        private static IReadOnlyList<BaseNodeOperationDefinition> NormalizeChapterNumbers(IReadOnlyList<BaseNodeOperationDefinition> operations)
        {
            if (operations == null || operations.Count == 0)
            {
                return new List<BaseNodeOperationDefinition>();
            }

            var chapterMap = new Dictionary<int, int>();
            int nextChapter = 1;

            foreach (var operation in operations)
            {
                int sourceChapter = operation.SourceN <= 0 ? 1 : operation.SourceN;
                if (!chapterMap.TryGetValue(sourceChapter, out int normalizedChapter))
                {
                    normalizedChapter = nextChapter++;
                    chapterMap[sourceChapter] = normalizedChapter;
                }

                operation.SourceN = normalizedChapter;
            }

            return operations;
        }

        public static IReadOnlyList<BaseNodeOperationDefinition> DeduplicateOperations(IEnumerable<BaseNodeOperationDefinition> operations)
        {
            if (operations == null)
            {
                return new List<BaseNodeOperationDefinition>();
            }

            return operations
                .Where(x => x != null)
                .GroupBy(x => new
                {
                    x.SourceN,
                    x.SourceN1,
                    KodO = x.KodO ?? string.Empty,
                    Text = x.Text ?? string.Empty,
                    x.Razryd,
                    x.Sek,
                    x.KodOb,
                    x.KodPodr,
                    x.KodProizv,
                    Spec = x.Spec ?? string.Empty,
                    Obor = x.Obor ?? string.Empty
                })
                .Select(group => group
                    .OrderBy(x => x.SortOrder == 0 ? int.MaxValue : x.SortOrder)
                    .ThenBy(x => x.BaseNodeOperationId == 0 ? int.MaxValue : x.BaseNodeOperationId)
                    .First())
                .OrderBy(x => x.SortOrder == 0 ? int.MaxValue : x.SortOrder)
                .ThenBy(x => x.SourceN)
                .ThenBy(x => x.SourceN1)
                .ToList();
        }
    }
}
