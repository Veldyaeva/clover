using SewingProduction.Features.Articul.Models;
using System;
using System.Collections.Generic;

namespace SewingProduction.Features.Articul.Service
{
    public sealed class FieldComparisonService
    {
        public ComparisonResult Compare(
            object? actualModel,
            IEnumerable<FieldComparisonItem> expectedItems)
        {
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

                if (AreEqual(actual, item.ExpectedValue))
                    continue;

                result.Mismatches.Add(new FieldMismatch
                {
                    PropertyName = item.PropertyName,
                    ActualValue = actual,
                    ExpectedValue = item.ExpectedValue
                });
            }

            return result;
        }

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
        public string? DisplayName { get; init; }
    }

    public sealed class FieldMismatch
    {
        public string PropertyName { get; init; } = "";
        public object? ExpectedValue { get; init; }
        public object? ActualValue { get; init; }
        //     public Control? Control { get; init; }
    }

    public sealed class ComparisonResult
    {
        public bool IsMatch => Mismatches.Count == 0;
        public List<FieldMismatch> Mismatches { get; } = new();
    }
    public static class CreateArticulMatrComparisonBuilder
    {
        public static IReadOnlyList<FieldComparisonItem> Build(CreateArticulMatrModel row)
        {
            if (row == null)
                return Array.Empty<FieldComparisonItem>();

            return new List<FieldComparisonItem>
            {
                new()
                {
                    PropertyName = nameof(SpArticulPreviewModel.Articul),
                    ExpectedValue = row.Articul,
                    DisplayName = "Артикул"
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
                    PropertyName = nameof(SpArticulPreviewModel.Grup),
                    ExpectedValue = row.Grup,
                    DisplayName = "Группа"
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
                }
            };
        }
    }
}


