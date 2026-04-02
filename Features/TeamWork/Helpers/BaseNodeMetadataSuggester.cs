using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

        private static readonly (string[] Keywords, string Value)[] ProductKindRules =
        {
            (new[] { "трикот", "кулир", "футер", "рибан", "кашкорсе", "интерлок", "лапша" }, "Трикотаж"),
            (new[] { "текст", "ткан", "сороч", "костюм", "плательн", "джинс" }, "Текстиль")
        };

        private static readonly (string[] Keywords, string Value)[] NodeGroupRules =
        {
            (new[] { "капюш" }, "Капюшон"),
            (new[] { "карман", "листочк", "клапан" }, "Карманы"),
            (new[] { "рукав", "манжет" }, "Рукав"),
            (new[] { "горлов", "ворот", "бейк", "стойк" }, "Горловина"),
            (new[] { "пояс", "резинк" }, "Пояс"),
            (new[] { "низ изделия", "подгиб низа", "низ" }, "Низ изделия"),
            (new[] { "полоч" }, "Полочка"),
            (new[] { "спинк" }, "Спинка"),
            (new[] { "боков", "боков ш" }, "Боковые швы"),
            (new[] { "молн", "застеж", "пугов", "петл", "планк" }, "Застежка"),
            (new[] { "отделк", "декор", "кант", "ярлык", "этикет" }, "Отделка")
        };

        public static BaseNodeSaveDefaults Suggest(ArtNormN annData, IReadOnlyCollection<NormRasz> operations)
        {
            string articleContext = BuildArticleContext(annData);
            string operationsContext = BuildOperationsContext(operations);

            return new BaseNodeSaveDefaults
            {
                ProductCategory = ResolveAllowedOption(
                    DetectValue(articleContext, ProductCategoryRules),
                    BaseNodeMetadataOptions.ProductCategories,
                    "Универсально"),
                ProductKind = ResolveAllowedOption(
                    DetectValue(articleContext, ProductKindRules),
                    BaseNodeMetadataOptions.ProductKinds,
                    "Универсальный"),
                NodeGroup = ResolveAllowedOption(
                    DetectValue(operationsContext, NodeGroupRules),
                    BaseNodeMetadataOptions.NodeGroups,
                    string.Empty)
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
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            return Regex.Replace(value.Trim().ToLowerInvariant(), @"\s+", " ");
        }
    }
}
