using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class ObjectCloneHelper
{
    /// <summary>
    /// Клонирует объект, копируя все свойства кроме указанных в excludedProperties.
    /// </summary>
    public static T CloneWithExclusions<T>(T source, params string[] excludedProperties) where T : class, new()
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        T clone = new T();
        var excluded = new HashSet<string>(excludedProperties ?? Array.Empty<string>(), StringComparer.OrdinalIgnoreCase);

        foreach (PropertyInfo prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (!prop.CanRead || !prop.CanWrite)
                continue;

            if (excluded.Contains(prop.Name))
                continue;

            object? value = prop.GetValue(source);
            prop.SetValue(clone, value);
        }

        return clone;
    }

    /// <summary>
    /// Клонирует объект и выполняет postAction после копирования.
    /// </summary>
    public static T CloneWithExclusions<T>(T source, Action<T> postAction, params string[] excludedProperties) where T : class, new()
    {
        var clone = CloneWithExclusions(source, excludedProperties);
        postAction?.Invoke(clone);
        return clone;
    }
}