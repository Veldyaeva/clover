using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SewingProduction.Features.TeamWork.Helpers
{
    internal static class BaseNodeMetadataSuggester
    {
        private static readonly (string[] Keywords, string Value)[] ProductCategoryRules =
        {
            (new[] { "плать", "сараф" }, "Платье"),
            (new[] { "шорт", "бермуд" }, "Шорты"),
            (new[] { "брюк", "брюч", "штаны", "джоггер", "леггин" }, "Брюки"),
            (new[] { "юбк" }, "Юбка"),
            (new[] { "худи", "hoodie", "свитшот", "толстов" }, "Худи"),
            (new[] { "футбол", "tee", "t-shirt", "лонгслив" }, "Футболка"),
            (new[] { "блуз", "рубаш", "сороч" }, "Блуза")
        };

        private static readonly (string[] Keywords, string Value)[] NodeGroupRules =
        {
            (new[] { "капюш" }, "Капюшон"),
            (new[] { "карман", "листочк", "клапан" }, "Карман"),
            (new[] { "рукав", "манжет" }, "Рукав"),
            (new[] { "горлов", "ворот", "бейк", "стойк" }, "Горловина"),
            (new[] { "пояс", "резинк" }, "Пояс"),
            (new[] { "низ изделия", "подгиб низа", "низ" }, "Низ изделия"),
            (new[] { "полоч" }, "Полочка"),
            (new[] { "спинк" }, "Спинка"),
            (new[] { "боков", "боков ш" }, "Декор"),
            (new[] { "молн", "застеж", "пугов", "петл", "планк" }, "Застежка"),
            (new[] { "отделк", "декор", "кант", "ярлык", "этикет" }, "Маркировка")
        };

        private static readonly (string[] Keywords, string Group, string Value)[] NodeSubgroupRules =
        {
            (new[] { "наклад" }, "Карман", "Накладной"),
            (new[] { "в шве" }, "Карман", "В шве"),
            (new[] { "листочк" }, "Карман", "С листочкой"),
            (new[] { "молн" }, "Карман", "С молнией"),
            (new[] { "клапан" }, "Карман", "С клапаном"),
            (new[] { "втачн" }, "Карман", "Втачной"),
            (new[] { "стойк" }, "Воротник", "Стойка"),
            (new[] { "отлож" }, "Воротник", "Отложной"),
            (new[] { "шальк" }, "Воротник", "Шалька"),
            (new[] { "отрезн" }, "Воротник", "С отрезной стойкой"),
            (new[] { "цельнокро" }, "Воротник", "Цельнокроеный"),
            (new[] { "обтач" }, "Горловина", "С обтачкой"),
            (new[] { "окант" }, "Горловина", "С окантовкой"),
            (new[] { "бейк" }, "Горловина", "С бейкой"),
            (new[] { "капл" }, "Горловина", "С каплей"),
            (new[] { "усил" }, "Горловина", "С усилителем"),
            (new[] { "реглан" }, "Рукав", "Реглан"),
            (new[] { "манжет" }, "Рукав", "С манжетой"),
            (new[] { "резинк" }, "Рукав", "На резинке"),
            (new[] { "втачн" }, "Рукав", "Втачной")
        };

        public static BaseNodeSaveDefaults Suggest(ArtNormN annData, IReadOnlyCollection<NormRasz> operations)
        {
            string articleContext = BuildArticleContext(annData);
            string operationsContext = BuildOperationsContext(operations);

            return new BaseNodeSaveDefaults
            {
                SourceAnnId = annData?.AnnID,
                SourceArticul = StringNormalizer.TrimOrEmpty(annData?.Articul),
                SourceRtCode = StringNormalizer.TrimOrEmpty(annData?.Kod),
                ProductCategory = ResolveAllowedOption(
                    DetectValue(articleContext, ProductCategoryRules),
                    BaseNodeMetadataOptions.ProductCategories,
                    "Универсально"),
                NodeType = "Заготовка",
                // NodeGroup приходит из DB-справочника, поэтому здесь оставляем только подсказку.
                NodeGroup = DetectValue(operationsContext, NodeGroupRules) ?? string.Empty,
                NodeGroupDetail = DetectSubgroup(operationsContext)
            };
        }

        private static string BuildArticleContext(ArtNormN annData)
        {
            if (annData == null)
            {
                return string.Empty;
            }

            return Normalize(string.Join(" ", new[]
            {
                annData.grup,
                annData.Articul,
                annData.Mod,
                annData.Komment,
                annData.Reco
            }.Where(value => !string.IsNullOrWhiteSpace(value))));
        }

        private static string BuildOperationsContext(IReadOnlyCollection<NormRasz> operations)
        {
            if (operations == null || operations.Count == 0)
            {
                return string.Empty;
            }

            // Текст операций и оборудование вместе дают наиболее понятный сигнал о группе узла.
            return Normalize(string.Join(" ", operations.SelectMany(operation => new[]
            {
                operation?.Text,
                operation?.Obor
            }).Where(value => !string.IsNullOrWhiteSpace(value))));
        }

        private static string DetectValue(string context, IEnumerable<(string[] Keywords, string Value)> rules)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                return null;
            }

            foreach (var (keywords, value) in rules)
            {
                if (keywords.Any(keyword => context.Contains(keyword, StringComparison.CurrentCultureIgnoreCase)))
                {
                    return value;
                }
            }

            return null;
        }

        private static string DetectSubgroup(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                return string.Empty;
            }

            string detectedGroup = DetectValue(context, NodeGroupRules);
            if (string.IsNullOrWhiteSpace(detectedGroup))
            {
                return string.Empty;
            }

            foreach (var (keywords, group, value) in NodeSubgroupRules)
            {
                if (!string.Equals(group, detectedGroup, StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }

                if (keywords.Any(keyword => context.Contains(keyword, StringComparison.CurrentCultureIgnoreCase)))
                {
                    return value;
                }
            }

            return string.Empty;
        }

        private static string ResolveAllowedOption(string value, IEnumerable<string> allowedValues, string fallback)
        {
            if (!string.IsNullOrWhiteSpace(value) && allowedValues.Any(option => string.Equals(option, value, StringComparison.CurrentCulture)))
            {
                return value;
            }

            return fallback;
        }

        private static string Normalize(string value)
        {
            return StringNormalizer.NormalizeWhitespaceLowerInvariant(value);
        }
    }
}
