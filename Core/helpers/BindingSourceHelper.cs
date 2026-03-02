using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.helpers;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Reflection.Metadata.BlobBuilder;

namespace SewingProduction.Core.helpers
{
    public static class BindingSourceHelper
    {


        // База: удалить по предикату (универсально)
        //public static int RemoveAll<T>(this BindingSource source, Func<T, bool> predicate)
        //{
        //    if (source == null) throw new ArgumentNullException(nameof(source));
        //    if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        //    if (source.List == null || source.List.Count == 0) return 0;

        //    int removed = 0;
        //    source.SuspendBinding();
        //    try
        //    {
        //        var idx = new List<int>(source.List.Count);
        //        for (int i = 0; i < source.List.Count; i++)
        //            if (source.List[i] is T item && predicate(item))
        //                idx.Add(i);

        //        for (int i = idx.Count - 1; i >= 0; i--)
        //        {
        //            source.RemoveAt(idx[i]);
        //            removed++;
        //        }
        //    }
        //    finally
        //    {
        //        source.ResumeBinding();
        //    }
        //    return removed;
        //}
        public static int RemoveAll<T>(this BindingSource source, Func<T, bool> predicate)
        {
            try
            {
                if (source == null) throw new ArgumentNullException(nameof(source));
                if (predicate == null) throw new ArgumentNullException(nameof(predicate));
                if (source.List == null || source.List.Count == 0) return 0;

                int removed = 0;

                try { source.EndEdit(); } catch { MessageBox.Show("RemoveAll_1"); }
                try { source.CurrencyManager?.EndCurrentEdit(); } catch { MessageBox.Show("RemoveAll_12"); }

                source.SuspendBinding();
                try
                {
                    var idx = new List<int>(source.List.Count);
                    for (int i = 0; i < source.List.Count; i++)
                        if (source.List[i] is T item && predicate(item))
                            idx.Add(i);

                    for (int i = idx.Count - 1; i >= 0; i--)
                    {
                        source.RemoveAt(idx[i]);
                        removed++;
                    }
                }
                finally
                {
                    source.ResumeBinding();
                }
                return removed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка в RemoveAll");
                return 0;
            }
        }
        // --- «Сахар» на основе интерфейсов ---

        // удалить все, где IsModified == true
        public static int RemoveModified<T>(this BindingSource source)
            where T : class, IModifiable
            => source.RemoveAll<T>(x => x.IsModified);

        // удалить все, где IsNew == true
        public static int RemoveNew<T>(this BindingSource source)
            where T : class, INewable
            => source.RemoveAll<T>(x => x.IsNew);

        // удалить все, где IsDeleted == true
        public static int RemoveDeleted<T>(this BindingSource source)
            where T : class, IDeletable
            => source.RemoveAll<T>(x => x.IsDeleted);

        // комбо: IsModified || IsNew || IsDeleted
        public static int RemoveDirty<T>(this BindingSource source)
            where T : class, INewable, IModifiable, IDeletable
            => source.RemoveAll<T>(x => x.IsModified || x.IsNew || x.IsDeleted);

        // произвольное условие (например, SyncSelection == 1)
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static int RemoveWhere<T>(this BindingSource source, Func<T, bool> predicate)
            => source.RemoveAll(predicate);
        //// Дженерики (компилятор проверит тип строго)
        //_pZVOperListByPachListBindingSource.RemoveModified<PZVOperList>();
        //_pZVOperListByPachListBindingSource.RemoveNew<PZVOperList>();
        //_pZVOperListByPachListBindingSource.RemoveDeleted<PZVOperList>();
        //_pZVOperListByPachListBindingSource.RemoveDirty<PZVOperList>();

        //// Произвольное условие (например, SyncSelection == 1)
        //_pZVOperListByPachListBindingSource.RemoveWhere<PZVOperList>(x => x.SyncSelection == 1);

        // ---------------------------
        // 1. Compute hash
        // ---------------------------
        //public static string ComputeHash<T>(T obj, params Func<T, object>[] fields)
        //{
        //    if (obj == null)
        //        return "";

        //    var sb = new StringBuilder();

        //    foreach (var f in fields)
        //    {
        //        var value = f(obj);
        //        sb.Append(value?.ToString() ?? "");
        //        sb.Append("|");
        //    }

        //    using var sha = SHA256.Create();
        //    var bytes = Encoding.UTF8.GetBytes(sb.ToString());
        //    return Convert.ToBase64String(sha.ComputeHash(bytes));
        //}
        public static string ComputeHash<T>(
            T obj,
            HashMode mode,
            params string[] propertyNames)
        {
            if (obj == null)
                return "";

            var type = typeof(T);
            var names = propertyNames ?? Array.Empty<string>();

            IEnumerable<PropertyInfo> props;

            switch (mode)
            {
                case HashMode.All:
                    props = type.GetProperties()
                                .Where(p => p.CanRead);
                    break;

                case HashMode.None:
                    props = Enumerable.Empty<PropertyInfo>();
                    break;

                case HashMode.IncludeOnly:
                    props = names.Length == 0
                        ? Enumerable.Empty<PropertyInfo>()
                        : type.GetProperties()
                              .Where(p => p.CanRead && names.Contains(p.Name));
                    break;

                case HashMode.ExcludeOnly:
                    props = type.GetProperties()
                                .Where(p => p.CanRead && !names.Contains(p.Name));
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }

            var sb = new StringBuilder();

            foreach (var p in props.OrderBy(p => p.Name))
            {
                sb.Append(p.Name);
                sb.Append('=');
                sb.Append(p.GetValue(obj)?.ToString() ?? "");
                sb.Append('|');
            }

            using var sha = SHA256.Create();
            return Convert.ToBase64String(
                sha.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()))
            );
        }




        // ---------------------------
        // 2. Convert BindingSource → Dictionary by key
        // ---------------------------
        public static Dictionary<TKey, T> ToDictionaryByKey<T, TKey>(
            BindingSource bs,
            Func<T, TKey> keySelector)
        {
            return bs.List.Cast<T>().ToDictionary(keySelector, x => x);
        }

        // ---------------------------
        // 3. Changes result container
        // ---------------------------
        public class ChangesResult<T>
        {
            public List<T> Added { get; } = new();
            public List<T> Modified { get; } = new();
            public List<T> Removed { get; } = new();
        }

        // ---------------------------
        // 4. Universal GetChanges
        // ---------------------------
        //public static ChangesResult<T> GetChanges<T, TKey>(
        //    BindingSource oldSource,
        //    BindingSource newSource,
        //    Func<T, TKey> keySelector,
        //    params Func<T, object>[] hashFields)
        //{
        //    var oldDict = ToDictionaryByKey(oldSource, keySelector);
        //    var newDict = ToDictionaryByKey(newSource, keySelector);

        //    var result = new ChangesResult<T>();

        //    // Added + Modified
        //    foreach (var kv in newDict)
        //    {
        //        var key = kv.Key;
        //        var newRow = kv.Value;

        //        if (!oldDict.TryGetValue(key, out var oldRow))
        //        {
        //            result.Added.Add(newRow);
        //        }
        //        else
        //        {
        //            var oldHash = ComputeHash(oldRow, hashFields);
        //            var newHash = ComputeHash(newRow, hashFields);

        //            if (oldHash != newHash)
        //                result.Modified.Add(newRow);
        //        }
        //    }

        //    // Removed
        //    foreach (var kv in oldDict)
        //    {
        //        if (!newDict.ContainsKey(kv.Key))
        //            result.Removed.Add(kv.Value);
        //    }

        //    return result;
        //}
        public enum HashMode
        {
            IncludeOnly,   // только указанные поля
            ExcludeOnly,   // все поля, кроме указанных
            All,           // ВСЕ поля
            None           // НИ ОДНОГО поля
        }

        public static ChangesResult<T> GetChanges<T>(
            BindingSource oldSource,
            BindingSource newSource,
            HashMode hashMode,
            string[] keyProperties)
        {
            if (hashMode != HashMode.All && hashMode != HashMode.None)
                throw new ArgumentException(
                    "This overload is allowed only for HashMode.All or HashMode.None",
                    nameof(hashMode));

            return GetChanges<T>(
                oldSource,
                newSource,
                hashMode,
                keyProperties,
                Array.Empty<string>()
            );
        }

        public static ChangesResult<T> GetChanges<T>(
            BindingSource oldSource,
            BindingSource newSource,
            HashMode hashMode,
            string[] keyProperties,
            string[] hashProperties)
        {
            if (keyProperties == null || keyProperties.Length == 0)
                throw new ArgumentException("Key properties must be specified", nameof(keyProperties));

            var keyProps = keyProperties
                .Select(p => typeof(T).GetProperty(p)
                    ?? throw new ArgumentException($"Property '{p}' not found on {typeof(T).Name}"))
                .ToArray();

            Func<T, string> keySelector = obj =>
            {
                var sb = new StringBuilder();
                foreach (var p in keyProps)
                {
                    sb.Append(p.GetValue(obj)?.ToString() ?? "");
                    sb.Append('|');
                }
                return sb.ToString();
            };

            return GetChanges<T, string>(
                oldSource,
                newSource,
                keySelector,
                hashMode,
                hashProperties
            );
        }



        public static ChangesResult<T> GetChanges<T, TKey>(
            BindingSource oldSource,
            BindingSource newSource,
            Func<T, TKey> keySelector,
            HashMode hashMode,
            params string[] hashProperties)
        {
            var oldDict = ToDictionaryByKey<T, TKey>(oldSource, keySelector);
            var newDict = ToDictionaryByKey<T, TKey>(newSource, keySelector);

            var result = new ChangesResult<T>();

            // Added + Modified
            foreach (var kv in newDict)
            {
                var key = kv.Key;
                var newRow = kv.Value;

                if (!oldDict.TryGetValue(key, out var oldRow))
                {
                    result.Added.Add(newRow);
                }
                else
                {
                    var oldHash = ComputeHash(oldRow, hashMode, hashProperties);
                    var newHash = ComputeHash(newRow, hashMode, hashProperties);

                    if (oldHash != newHash)
                        result.Modified.Add(newRow);
                }
            }

            // Removed
            foreach (var kv in oldDict)
            {
                if (!newDict.ContainsKey(kv.Key))
                    result.Removed.Add(kv.Value);
            }

            return result;
        }

        // ---------------------------
        // 5. Apply modified + added
        // ---------------------------
        //public static void ApplyChanges<T, TKey>(
        //    BindingSource oldSource,
        //    ChangesResult<T> changes,
        //    Func<T, TKey> keySelector)
        //{
        //    var list = oldSource.List.Cast<T>().ToList();
        //    var dict = list.ToDictionary(keySelector, x => x);

        //    // Apply modified rows
        //    foreach (var updated in changes.Modified)
        //    {
        //        var key = keySelector(updated);

        //        if (dict.TryGetValue(key, out var old))
        //        {
        //            foreach (var prop in typeof(T).GetProperties().Where(p => p.CanWrite))
        //            {
        //                prop.SetValue(old, prop.GetValue(updated));
        //            }
        //        }
        //    }

        //    // Add new rows
        //    foreach (var added in changes.Added)
        //    {
        //        oldSource.Add(added);
        //    }

        //    oldSource.ResetBindings(false);
        //}
        public enum UpdateFieldsMode
        {
            IncludeOnly,   // обновлять ТОЛЬКО указанные поля
            ExcludeOnly,   // обновлять ВСЕ, кроме указанных
            All,           // обновлять ВСЕ поля
            None           // не обновлять никаких полей
        }
        //public static void ApplyChanges<T, TKey>(
        //    BindingSource oldSource,
        //    ChangesResult<T> changes,
        //    Func<T, TKey> keySelector,
        //    UpdateFieldsMode mode,
        //    params string[] fields)
        //{
        //    if (oldSource == null) throw new ArgumentNullException(nameof(oldSource));
        //    if (changes == null) throw new ArgumentNullException(nameof(changes));
        //    if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

        //    fields ??= Array.Empty<string>();

        //    var oldDict = oldSource.List.Cast<T>()
        //        .ToDictionary(keySelector, x => x);

        //    var propsAll = typeof(T).GetProperties()
        //        .Where(p => p.CanWrite)
        //        .ToArray();

        //    var propsToCopy = mode switch
        //    {
        //        UpdateFieldsMode.All => propsAll,

        //        UpdateFieldsMode.None => Array.Empty<System.Reflection.PropertyInfo>(),

        //        UpdateFieldsMode.IncludeOnly => propsAll
        //            .Where(p => fields.Contains(p.Name))
        //            .ToArray(),

        //        UpdateFieldsMode.ExcludeOnly => propsAll
        //            .Where(p => !fields.Contains(p.Name))
        //            .ToArray(),

        //        _ => propsAll
        //    };

        //    // Modified: копируем только выбранные поля
        //    foreach (var updated in changes.Modified)
        //    {
        //        var key = keySelector(updated);
        //        if (!oldDict.TryGetValue(key, out var old))
        //            continue;

        //        foreach (var prop in propsToCopy)
        //            prop.SetValue(old, prop.GetValue(updated));
        //    }

        //    // Added: добавляем целиком
        //    //foreach (var added in changes.Added)
        //    //    oldSource.Add(added);

        //    oldSource.SuspendBinding();
        //    try
        //    {
        //        var current = oldSource.List.Cast<T>().ToList();
        //        current.AddRange(changes.Added);

        //        oldSource.DataSource = current;  // один раз
        //    }
        //    finally
        //    {
        //        oldSource.ResumeBinding();
        //        //oldSource.ResetBindings(false);
        //    }

        //    oldSource.ResetBindings(false);
        //}
        //public static void ApplyChanges<T, TKey>(
        //    BindingSource oldSource,
        //    ChangesResult<T> changes,
        //    Func<T, TKey> keySelector,
        //    UpdateFieldsMode mode,
        //    params string[] fields)
        //{
        //    if (oldSource == null) throw new ArgumentNullException(nameof(oldSource));
        //    if (changes == null) throw new ArgumentNullException(nameof(changes));
        //    if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
        //    fields ??= Array.Empty<string>();
        //    var oldDict = oldSource.List.Cast<T>().ToDictionary(keySelector, x => x);
        //    var propsAll = typeof(T).GetProperties().Where(p => p.CanWrite).ToArray();
        //    var propsToCopy = mode switch
        //    {
        //        UpdateFieldsMode.All => propsAll,
        //        UpdateFieldsMode.None => Array.Empty<System.Reflection.PropertyInfo>(),
        //        UpdateFieldsMode.IncludeOnly => propsAll.Where(p => fields.Contains(p.Name)).ToArray(),
        //        UpdateFieldsMode.ExcludeOnly => propsAll.Where(p => !fields.Contains(p.Name)).ToArray(),
        //        _ => propsAll
        //    }; // Modified: копируем только выбранные поля
        //    foreach (var updated in changes.Modified)
        //    {
        //        var key = keySelector(updated);
        //        if (!oldDict.TryGetValue(key, out var old))
        //            continue;
        //        foreach (var prop in propsToCopy)
        //            prop.SetValue(old, prop.GetValue(updated));
        //    } // Added: добавляем целиком 
        //      //foreach (var added in changes.Added) 
        //      // oldSource.Add(added);
        //    oldSource.SuspendBinding();
        //    try
        //    {
        //        var current = oldSource.List.Cast<T>().ToList();
        //        current.AddRange(changes.Added);
        //        oldSource.DataSource = current; // один раз
        //    }
        //    finally
        //    {
        //        oldSource.ResumeBinding(); //oldSource.ResetBindings(false);
        //    }
        //    oldSource.ResetBindings(false);
        //}
        private static void RestoreGridPosition<T, TKey>(
            BindingSource source,
            DevExpress.XtraGrid.Views.Grid.GridView view,
            Func<T, TKey> keySelector,
            TKey key,
            string? focusedColumnFieldName
            //int topRowIndex = -1
            )
        {
            //Debug.WriteLine($"[RestoreGridPosition] key={key}, focusedColumnFieldName={focusedColumnFieldName}, topRowIndex={topRowIndex}");
            // 1. Индекс в BindingSource
            int index = -1;
            for (int i = 0; i < source.Count; i++)
            {
                if (source[i] is T item &&
                    EqualityComparer<TKey>.Default.Equals(keySelector(item), key))
                {
                    index = i;
                    break;
                }
            }

            if (index < 0)
                return;

            // 2. Позиция BindingSource
            source.Position = index;

            // 3. RowHandle
            int rowHandle = view.GetRowHandle(index);
            if (rowHandle < 0)
                return;

            // 4. Раскрываем группы
            ExpandParentGroups(view, rowHandle);

            // 5. Фокус строки
            view.MakeRowVisible(rowHandle);
            view.FocusedRowHandle = rowHandle;

            // 6. Фокус колонки (если задана)
            if (!string.IsNullOrWhiteSpace(focusedColumnFieldName))
            {
                var col = view.Columns.ColumnByFieldName(focusedColumnFieldName);
                if (col != null)
                    view.FocusedColumn = col;
            }
            //Debug.WriteLine($"[RestoreGridPosition] After setting focused row and column: FocusedRowHandle={view.FocusedRowHandle}, FocusedColumn={view.FocusedColumn?.FieldName}, TopRowIndex={view.TopRowIndex}");
            //view.TopRowIndex = topRowIndex >= 0 ? topRowIndex : view.TopRowIndex;
            //Debug.WriteLine($"[RestoreGridPosition] After setting TopRowIndex: TopRowIndex={view.TopRowIndex}");
        }
        private static void ExpandParentGroups(
            DevExpress.XtraGrid.Views.Grid.GridView view,
            int rowHandle)
        {
            int parent = view.GetParentRowHandle(rowHandle);
            while (parent != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                view.SetRowExpanded(parent, true);
                parent = view.GetParentRowHandle(parent);
            }
        }

        public static void ApplyChanges<T, TKey>(
            BindingSource oldSource,
            ChangesResult<T> changes,
            Func<T, TKey> keySelector,
            UpdateFieldsMode mode,
            DevExpress.XtraGrid.Views.Grid.GridView? gridView = null,
            string? focusedColumnFieldName = null,
            params string[] fields)
        {
            Debug.WriteLine($"[ApplyChanges] mode={mode}, gridView={(gridView != null ? gridView.Name : "null")}, focusedColumnFieldName={focusedColumnFieldName}, topRowIndex={gridView?.TopRowIndex}");
            if (oldSource == null) throw new ArgumentNullException(nameof(oldSource));
            if (changes == null) throw new ArgumentNullException(nameof(changes));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            fields ??= Array.Empty<string>();

            // === 1. Запоминаем текущий ключ
            TKey? currentKey = default;
            bool hasCurrent = false;
            //int currentTopRowIndex = -1;

            if (gridView != null && oldSource.Current is T currentItem)
            {
                currentKey = keySelector(currentItem);
                //currentTopRowIndex = gridView.TopRowIndex;
                hasCurrent = true;
                if (gridView.FocusedColumn != null)
                {
                    focusedColumnFieldName = gridView.FocusedColumn.FieldName;
                }
            }
            //Debug.WriteLine($"[ApplyChanges] currentTopRowIndex={currentTopRowIndex}");

            // === 2. Основная логика обновления
            var oldDict = oldSource.List.Cast<T>()
                .ToDictionary(keySelector, x => x);

            var propsAll = typeof(T)
                .GetProperties()
                .Where(p => p.CanWrite)
                .ToArray();

            var propsToCopy = mode switch
            {
                UpdateFieldsMode.All => propsAll,
                UpdateFieldsMode.None => Array.Empty<PropertyInfo>(),
                UpdateFieldsMode.IncludeOnly => propsAll.Where(p => fields.Contains(p.Name)).ToArray(),
                UpdateFieldsMode.ExcludeOnly => propsAll.Where(p => !fields.Contains(p.Name)).ToArray(),
                _ => propsAll
            };

            // Modified
            foreach (var updated in changes.Modified)
            {
                var key = keySelector(updated);
                if (!oldDict.TryGetValue(key, out var old))
                    continue;

                foreach (var prop in propsToCopy)
                    prop.SetValue(old, prop.GetValue(updated));
            }

            // === 3. Перезаписываем DataSource
            oldSource.SuspendBinding();
            try
            {
                var current = oldSource.List.Cast<T>().ToList();
                current.AddRange(changes.Added);
                oldSource.DataSource = current;
            }
            finally
            {
                oldSource.ResumeBinding();
            }

            oldSource.ResetBindings(false);

            // === 4. Восстанавливаем позицию и колонку
            if (gridView != null && hasCurrent)
            {
                RestoreGridPosition(
                    oldSource,
                    gridView,
                    keySelector,
                    currentKey!,
                    focusedColumnFieldName
                    //topRowIndex: currentTopRowIndex
                );
            }
            Debug.WriteLine($"[ApplyChanges] Completed");
        }

        public static void ApplyChanges<T>(
            BindingSource oldSource,
            ChangesResult<T> changes,
            UpdateFieldsMode mode,
            string[] keyProperties,
            params string[] fields)
        {
            if (keyProperties == null || keyProperties.Length == 0)
                throw new ArgumentException("Key properties must be specified", nameof(keyProperties));

            var keyProps = keyProperties
                .Select(p => typeof(T).GetProperty(p)
                    ?? throw new ArgumentException($"Property '{p}' not found on {typeof(T).Name}"))
                .ToArray();

            Func<T, string> keySelector = obj =>
            {
                var sb = new StringBuilder();
                foreach (var p in keyProps)
                {
                    sb.Append(p.GetValue(obj)?.ToString() ?? "");
                    sb.Append('|');
                }
                return sb.ToString();
            };

            ApplyChanges<T, string>(oldSource, changes, keySelector, mode, fields: fields);
        }
        public static void ApplyChanges<T>(
            BindingSource oldSource,
            ChangesResult<T> changes,
            UpdateFieldsMode mode,
            string[] keyProperties,
            DevExpress.XtraGrid.Views.Grid.GridView? gridView = null,
           // string? focusedColumnFieldName = null,
            params string[] fields)
        {
            Debug.WriteLine($"[ApplyChanges overload with GridView] keyProperties={string.Join(", ", keyProperties)}, gridView={(gridView != null ? gridView.Name : "null")}, focusedColumnFieldName={gridView?.FocusedColumn?.FieldName}, topRowIndex={gridView.TopRowIndex}");
            if (keyProperties == null || keyProperties.Length == 0)
                throw new ArgumentException("Key properties must be specified", nameof(keyProperties));

            var keyProps = keyProperties
                .Select(p => typeof(T).GetProperty(p)
                    ?? throw new ArgumentException($"Property '{p}' not found on {typeof(T).Name}"))
                .ToArray();

            Func<T, string> keySelector = obj =>
            {
                var sb = new StringBuilder();
                foreach (var p in keyProps)
                {
                    sb.Append(p.GetValue(obj)?.ToString() ?? "");
                    sb.Append('|');
                }
                return sb.ToString();
            };

            ApplyChanges<T, string>(
                oldSource,
                changes,
                keySelector,
                mode,
                gridView,
               // focusedColumnFieldName,
                fields: fields
            );
            Debug.WriteLine($"[ApplyChanges overload with GridView] Completed");
        }

        //    public static void ApplyChanges<T>(
        //BindingSource oldSource,
        //ChangesResult<T> changes,
        //UpdateFieldsMode mode,
        //string[] keyProperties,
        //params string[] fields)
        //    {
        //        if (keyProperties == null || keyProperties.Length == 0)
        //            throw new ArgumentException("Key properties must be specified", nameof(keyProperties));

        //        var keyProps = keyProperties
        //            .Select(p => typeof(T).GetProperty(p)
        //                ?? throw new ArgumentException($"Property '{p}' not found on {typeof(T).Name}"))
        //            .ToArray();

        //        // 👇 типобезопасный ключ
        //        Func<T, object[]> keySelector = obj =>
        //        {
        //            var values = new object[keyProps.Length];
        //            for (int i = 0; i < keyProps.Length; i++)
        //                values[i] = keyProps[i].GetValue(obj);
        //            return values;
        //        };

        //        // 👇 comparer по значениям массива
        //        ApplyChanges<T, object[]>(
        //            oldSource,
        //            changes,
        //            keySelector,
        //            mode,
        //            fields
        //        );
        //    }



        // ---------------------------
        // 6. Remove missing rows
        // ---------------------------
        //public static void RemoveMissing<T>(
        //    BindingSource oldSource,
        //    List<T> removed)
        //{
        //    foreach (var row in removed)
        //        oldSource.Remove(row);

        //    oldSource.ResetBindings(false);
        //}
        public static void RemoveMissing<T>(
            BindingSource oldSource,
            List<T> removed)
        {
            foreach (var row in removed)
                oldSource.Remove(row);

            oldSource.ResetBindings(false);
        }

        public sealed class RemoveMetrics
        {
            public int TotalCount { get; init; }
            public int RemovedCount { get; init; }
            public double RemovedPercent { get; init; }

            public string Strategy { get; init; } = "";
            public TimeSpan TotalTime { get; init; }
            public TimeSpan RemoveTime { get; init; }

            public override string ToString() =>
                $"{Strategy}: removed {RemovedCount}/{TotalCount} ({RemovedPercent:F1}%), " +
                $"remove={RemoveTime.TotalMilliseconds:F0} ms, total={TotalTime.TotalMilliseconds:F0} ms";
        }

        public static RemoveMetrics RemoveMissingSmart<T>(
            BindingSource oldSource,
            IReadOnlyCollection<T> removed,
            DevExpress.XtraGrid.GridControl grid = null,
            double rebuildThresholdPercent = 30.0,
            double removeAtThresholdPercent = 10.0)
        {
            if (oldSource == null)
                throw new ArgumentNullException(nameof(oldSource));

            if (removed == null || removed.Count == 0)
                return new RemoveMetrics
                {
                    TotalCount = oldSource.Count,
                    RemovedCount = 0,
                    RemovedPercent = 0,
                    Strategy = "NothingToRemove",
                    TotalTime = TimeSpan.Zero,
                    RemoveTime = TimeSpan.Zero
                };

            var swTotal = System.Diagnostics.Stopwatch.StartNew();

            int totalCount = oldSource.Count;
            int removedCount = removed.Count;
            double percent = totalCount == 0 ? 0 : (removedCount * 100.0 / totalCount);

            string strategy;
            
            int topRowIndex = 0;

            try
            {
                topRowIndex = grid?.MainView is DevExpress.XtraGrid.Views.Grid.GridView view
                    ? view.TopRowIndex
                    : -1;

                grid?.BeginUpdate();
                oldSource.SuspendBinding();

                var swRemove = System.Diagnostics.Stopwatch.StartNew();

                // --- STRATEGY 1: FULL REBUILD ---
                if (percent >= rebuildThresholdPercent)
                {
                    strategy = "RebuildDataSource";

                    var toRemove = new HashSet<T>(removed);
                    var newList = oldSource.List.Cast<T>()
                        .Where(x => !toRemove.Contains(x))
                        .ToList();

                    oldSource.DataSource = newList;
                }
                // --- STRATEGY 2: REMOVE AT (fast for small %) ---
                else if (percent <= removeAtThresholdPercent &&
                         oldSource.List is System.Collections.IList list)
                {
                    strategy = "RemoveAtDescending";

                    var indices = removed
                        .Select(r => list.IndexOf(r))
                        .Where(i => i >= 0)
                        .OrderByDescending(i => i)
                        .ToList();

                    foreach (var i in indices)
                        list.RemoveAt(i);
                }
                // --- STRATEGY 3: SIMPLE REMOVE ---
                else
                {
                    strategy = "RemoveByItem";

                    if (oldSource.List is System.Collections.IList ilist)
                    {
                        foreach (var r in removed)
                            ilist.Remove(r);
                    }
                    else
                    {
                        foreach (var r in removed)
                            oldSource.Remove(r);
                    }
                }

                swRemove.Stop();

                return new RemoveMetrics
                {
                    TotalCount = totalCount,
                    RemovedCount = removedCount,
                    RemovedPercent = percent,
                    Strategy = strategy,
                    RemoveTime = swRemove.Elapsed,
                    TotalTime = swTotal.Elapsed
                };
            }
            finally
            {
                oldSource.ResumeBinding();
                oldSource.ResetBindings(false);
                grid?.EndUpdate();
                swTotal.Stop();
                DevExpress.XtraGrid.Views.Grid.GridView gw = (DevExpress.XtraGrid.Views.Grid.GridView)grid?.MainView;
                gw.TopRowIndex = topRowIndex;
            }
        }

        //        var changes = BindingSourceHelper.GetChanges<PZVOperList>(
        //                            _pZVOperListByPachListBindingSource,
        //                            _pZVOperListByPachListNewBindingSource,
        //                            HashMode.ExcludeOnly,
        //                            keyProperties: new[] { "olPzvID" },
        //                            hashProperties: new[] { "IsNew", "IsModified", "IsDeleted", "SyncSelection", "ErrorSelection" }
        //                        );


        //        BindingSourceHelper.ApplyChanges<PZVOperList>(
        //                    _pZVOperListByPachListBindingSource,
        //                    changes,
        //                    UpdateFieldsMode.ExcludeOnly,
        //                    keyProperties: new[] { "olPzvID" },
        //                    fields: new[] { "IsNew", "IsModified", "IsDeleted", "SyncSelection", "ErrorSelection" }
        //                );

        //BindingSourceHelper.RemoveMissing(
        //                    _pZVOperListByPachListBindingSource,
        //                    changes.Removed
        //                );
    }
}
