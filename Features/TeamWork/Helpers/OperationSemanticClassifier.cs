using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SewingProduction.Models;

namespace SewingProduction.Features.TeamWork.Helpers
{
    internal static class OperationSemanticClassifier
    {
        private static readonly string[] IroningKeywords =
        {
            "утюж", "приутюж", "разутюж", "заутюж", "отутюж", "сутюж", "декатир", "пресс"
        };

        private static readonly string[] ManualKeywords =
        {
            "ручн", "вручн", "намел", "маркир", "сколот", "смет", "вымет", "подшить вруч", "намет"
        };

        private static readonly string[] SpecialMachineKeywords =
        {
            "оверлок", "обмет", "распош", "петель", "пугов", "закреп", "зигзаг", "спецмаш", "автомат"
        };

        private static readonly (string Keyword, string Value)[] ObjectKeywords =
        {
            ("капюшон", "Капюшон"),
            ("карман", "Карман"),
            ("манжет", "Манжета"),
            ("воротник", "Воротник"),
            ("стойк", "Стойка"),
            ("пояс", "Пояс"),
            ("рукав", "Рукав"),
            ("полочк", "Полочка"),
            ("спинк", "Спинка"),
            ("горловин", "Горловина"),
            ("кокетк", "Кокетка"),
            ("планк", "Планка"),
            ("листочк", "Листочка"),
            ("клапан", "Клапан"),
            ("подклад", "Подкладка"),
            ("ярлык", "Ярлык"),
            ("этикет", "Этикетка"),
            ("молни", "Молния"),
            ("резинк", "Резинка"),
            ("бейк", "Бейка"),
            ("брючин", "Брючина"),
            ("низ", "Низ"),
            ("шов", "Шов"),
            ("гульфик", "Гульфик"),
            ("ластовиц", "Ластовица")
        };

        public static string DetectClass(string operationText, string equipment, int kodOb)
        {
            string text = Normalize(operationText);
            string obor = Normalize(equipment);
            string combined = string.Join(" ", new[] { text, obor }.Where(x => !string.IsNullOrWhiteSpace(x)));

            if (ContainsAny(combined, IroningKeywords))
            {
                return "Утюжильная";
            }

            if (ContainsAny(combined, ManualKeywords))
            {
                return "Ручная";
            }

            if (ContainsAny(combined, SpecialMachineKeywords))
            {
                return "Спецмашина";
            }

            if (kodOb > 0 && !string.IsNullOrWhiteSpace(obor))
            {
                return "Машинная";
            }

            return "Машинная";
        }

        public static string DetectObject(string operationText)
        {
            string text = Normalize(operationText);
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            foreach (var (keyword, value) in ObjectKeywords)
            {
                if (text.Contains(keyword, StringComparison.CurrentCultureIgnoreCase))
                {
                    return value;
                }
            }

            var tokens = Regex.Split(text, @"\s+")
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

            if (tokens.Count <= 1)
            {
                return null;
            }

            string candidate = StringNormalizer.TrimOrEmpty(string.Join(" ", tokens.Skip(1).Take(3)));
            return string.IsNullOrWhiteSpace(candidate) ? null : ToSentenceCase(candidate);
        }

        private static bool ContainsAny(string source, IEnumerable<string> keywords)
        {
            return keywords.Any(keyword => source.Contains(keyword, StringComparison.CurrentCultureIgnoreCase));
        }

        private static string Normalize(string value)
        {
            return StringNormalizer.NormalizeWhitespaceLowerInvariant(value);
        }

        private static string ToSentenceCase(string value)
        {
            return StringNormalizer.ToSentenceCase(value);
        }
    }
}
