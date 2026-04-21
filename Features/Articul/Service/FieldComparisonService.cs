using DevExpress.CodeParser;
using SewingProduction.Features.Articul.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable enable

namespace SewingProduction.Features.Articul.Service
{
    /// <summary>
    /// Сервис для сравнения полей модели с ожидаемыми значениями.
    /// </summary>
    public sealed class FieldComparisonService
    {
        /// <summary>
        /// Сравнивает поля фактической модели с ожидаемыми значениями и возвращает результат сравнения.
        /// </summary>
        /// <param name="actualModel"></param>
        /// <param name="expectedItems"></param>
        /// <returns></returns>
        public ComparisonResult Compare(
            object? actualModel,
            IEnumerable<FieldComparisonItem> expectedItems)
        {
            try {
                var result = new ComparisonResult();

                if (actualModel == null || expectedItems == null)
                    return result;

                var modelType = actualModel.GetType();

                foreach (var item in expectedItems)
                {
                    var prop = modelType.GetProperty(item.PropertyName);
                    if (prop == null)
                        continue;
                    var actual = prop.GetValue(actualModel);

                    if (AreEqualWithRules(item, actual, item.ExpectedValue))
                        continue;

                    var mismatch = new FieldMismatch
                    {
                        PropertyName = item.PropertyName,
                        ActualValue = actual,
                        ExpectedValue = item.ExpectedValue,
                        ExpectedDisplayValue = item.ExpectedDisplayValue ?? item.ExpectedValue,
                        Control = null // Здесь можно добавить логику для определения связанного UI-контрола, если необходимо
                    };

                    // Для подсветки учитываем все расхождения
                    result.Mismatches.Add(mismatch);

                    // Для итогового совпадения игнорируем "подсветить-но-не-валидировать" поля (например, Сезон)
                    if (!IsHighlightOnlyField(item.PropertyName))
                        result.SignificantMismatches.Add(mismatch);
                }

                return result;
            }
            catch(Exception ex) { MessageBox.Show(ex.Message.ToString()); return new ComparisonResult(); }
            }
        /// <summary>
        /// Сравнивает два значения с учетом нормализации строк и допустимой погрешности для чисел.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private static bool AreEqual(object? left, object? right)
        {
            left = Normalize(left);
            right = Normalize(right);

            if (left == null && right == null) return true;
            if (left == null || right == null) return false;

            if (IsNumeric(left) || IsNumeric(right))
            {
                var l = Convert.ToDecimal(left);
                var r = Convert.ToDecimal(right);
                return Math.Abs(l - r) < 0.01m;
            }

            return Equals(left, right);
        }

        private static bool IsHighlightOnlyField(string propertyName) =>
            string.Equals(propertyName, nameof(SpArticulPreviewModel.SeasonName), StringComparison.OrdinalIgnoreCase);

        private static bool AreEqualWithRules(FieldComparisonItem item, object? left, object? right)
        {
            if (string.Equals(item.PropertyName, nameof(SpArticulPreviewModel.Articul), StringComparison.OrdinalIgnoreCase))
            {
                // в матрице заполнен "повторный артикул" — сравниваем строго 
                if (item.FullMatch)
                    return AreEqualArticulFull(left, right);

                // Иначе сравниваем частично
                return AreEqualArticulPartial(left, right);
            }

            return AreEqual(left, right);
        }

        private static bool AreEqualArticulPartial(object? left, object? right)
        {
            left = Normalize(left);
            right = Normalize(right);

            if (left == null && right == null) return true;
            if (left == null || right == null) return false;

            if (left is string ls && right is string rs)
            {
                // Normalize превращает пустые значения в string.Empty
                if (ls.Length == 0 && rs.Length == 0) return true;

                if (string.Equals(ls, rs, StringComparison.OrdinalIgnoreCase))
                    return true;

                // Частичное совпадение: одно содержит другое.
                return ls.Contains(rs, StringComparison.OrdinalIgnoreCase) ||
                       rs.Contains(ls, StringComparison.OrdinalIgnoreCase);
            }

            return Equals(left, right);
        }

        private static bool AreEqualArticulFull(object? left, object? right)
        {
            left = Normalize(left);
            right = Normalize(right);

            if (left == null && right == null) return true;
            if (left == null || right == null) return false;

            if (left is string ls && right is string rs)
                return string.Equals(ls, rs, StringComparison.OrdinalIgnoreCase);

            //на всякий
            return AreEqual(left, right);
        }

        /// <summary>
        /// Нормализует значение для сравнения: обрезает строки и заменяет null на null, а пустые строки на string.Empty.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static object? Normalize(object? value)
        {
            if (value == null)
                return null;

            if (value is string s)
                return string.IsNullOrWhiteSpace(s) ? string.Empty : s.Trim();

            return value;
        }

        private static bool IsNumeric(object value) =>
            value is byte or sbyte or short or ushort or int or uint or long or ulong
            or float or double or decimal;
    }
    public sealed class FieldComparisonItem
    {
        public string PropertyName { get; init; } = "";
        public object? ExpectedValue { get; init; }
        public object? ExpectedDisplayValue { get; init; }
        public string? DisplayName { get; init; }
        public bool FullMatch { get; init; }
    }

    public sealed class FieldMismatch
    {
        public string PropertyName { get; init; } = "";
        public object? ExpectedValue { get; init; }
        public object? ExpectedDisplayValue { get; init; }
        public object? ActualValue { get; init; }
        public Control? Control { get; init; }
    }

    public sealed class ComparisonResult
    {
        // Подсветка ошибок делается по `Mismatches`,
        // а результат "совпало/не совпало" игнорирует часть полей (сезон).
        public bool IsMatch => SignificantMismatches.Count == 0;
        public List<FieldMismatch> Mismatches { get; } = new();
        public List<FieldMismatch> SignificantMismatches { get; } = new();
    }
    /// <summary>
    /// Построитель списка полей для сравнения модели CreateArticulMatrModel с моделью SpArticulPreviewModel.
    /// </summary>
    public static class CreateArticulMatrComparisonBuilder
    {
        public static IReadOnlyList<FieldComparisonItem> Build(CreateArticulMatrModel row)
        {
            try
            {
                if (row == null)
                    return Array.Empty<FieldComparisonItem>();

                return new List<FieldComparisonItem>
            {
                new()
                {
                    PropertyName = nameof(SpArticulPreviewModel.Articul),
                    ExpectedValue = string.IsNullOrWhiteSpace(row.RepeatArticle) ? row.Articul : row.RepeatArticle,
                    DisplayName = string.IsNullOrWhiteSpace(row.RepeatArticle) ? "Артикул" : "Артикул (повторный)",
                    FullMatch = !string.IsNullOrWhiteSpace(row.RepeatArticle),
                },
                new()
                {
                    PropertyName = nameof(SpArticulPreviewModel.Mod),
                    ExpectedValue = row.Mod,
                    DisplayName = "Модель"
                },
                new()
                {
                    PropertyName = nameof(SpArticulPreviewModel.TmName),
                    ExpectedValue = row.Tm_name,
                    DisplayName = "ТМ"
                },
                new()
                {
                    PropertyName = nameof(SpArticulPreviewModel.Tkb),
                    ExpectedValue = row.Tkb,
                    DisplayName = "ТКБ"
                },
                new()
                {
                    PropertyName = nameof(SpArticulPreviewModel.SeasonName),
                    ExpectedValue = row.Tsn_name,
                    DisplayName = "Сезон"
                },
                new()
                {
                    PropertyName = nameof(SpArticulPreviewModel.AssortName),
                    ExpectedValue = row.Kod_v, 
                    DisplayName = "Ассортимент"
                },
                new()
                {
                    PropertyName = nameof(SpArticulPreviewModel.Grup),//Ag_id),
                    ExpectedValue = row.Grup,//.Ag_id,
                    DisplayName = "Группа"
                },
                new()
                {
                    PropertyName = nameof(SpArticulPreviewModel.GrupMenName),
                    ExpectedValue = row.Grupmen_name,
                    DisplayName = "Менеджер"
                },
                new()
                {
                    PropertyName = nameof(SpArticulPreviewModel.Id_gost),
                    ExpectedValue = row.Id_gost,
                    DisplayName = "ГОСТ"
                },
                new()
                {
                    PropertyName = nameof(SpArticulPreviewModel.Sost),
                    ExpectedValue = row.Sost,
                    DisplayName = "Состав"
                },
                new()
                {
                    PropertyName = nameof(SpArticulPreviewModel.Sost2),
                    ExpectedValue = row.Sost2,
                    DisplayName = "Отделка"
                },
                new()
                {
                    PropertyName = nameof(SpArticulPreviewModel.Sost3),
                    ExpectedValue = row.Sost3,
                    DisplayName = "Подклад / наполнитель"
                },
                new()
                {
                    PropertyName = nameof(SpArticulPreviewModel.KrujFlag),
                    ExpectedValue = row.Kruj == 1,
                    DisplayName = "Кружево"
                }
            };
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); return Array.Empty<FieldComparisonItem>(); }
        }
    }
}

