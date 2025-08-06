using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SewingProduction.Core.Services.DataCleanup
{
    public class DataCleanupService : IDataCleanupService
    {
        public T CleanupStringFields<T>(T entity) where T : class
        {
            if (entity == null) return null;

            var stringProperties = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(string) && p.CanRead && p.CanWrite);

            foreach (var property in stringProperties)
            {
                var value = property.GetValue(entity) as string;
                if (!string.IsNullOrEmpty(value))
                {
                    property.SetValue(entity, value.TrimEnd());
                }
            }

            return entity;
        }

        public IEnumerable<T> CleanupCollection<T>(IEnumerable<T> entities) where T : class
        {
            return entities?.Select(CleanupStringFields) ?? Enumerable.Empty<T>();
        }
    }
}
