using SewingProduction.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork_AdvanceTW
    {
        #region Утилиты: CloneUtils
        public static class CloneUtils
        {
            private sealed class PropertyCache
            {
                public PropertyInfo Id { get; set; }
                public PropertyInfo AnnId { get; set; }
                public PropertyInfo IsNew { get; set; }
                public PropertyInfo IsModified { get; set; }
            }

            private static readonly ConcurrentDictionary<(Type Type, string IdNameKey), PropertyCache> _propCache
                = new ConcurrentDictionary<(Type, string), PropertyCache>();

            private static PropertyCache ResolveProperties(Type type, string idFieldName)
            {
                var key = (type, idFieldName ?? string.Empty);
                return _propCache.GetOrAdd(key, _ =>
                {
                    var flags = BindingFlags.Public | BindingFlags.Instance;
                    var cache = new PropertyCache();

                    // Id property (explicit name first, then alternatives)
                    if (!string.IsNullOrEmpty(idFieldName))
                    {
                        cache.Id = type.GetProperty(idFieldName, flags);
                    }
                    if (cache.Id == null)
                    {
                        var alternativeNames = new[] { "nrID", "nrId", "id", "nkId", "NrID", "Id", "NkId" };
                        foreach (var alt in alternativeNames)
                        {
                            var altProp = type.GetProperty(alt, flags);
                            if (altProp != null)
                            {
                                cache.Id = altProp;
                                break;
                            }
                        }
                    }

                    // Устанавливаем новый AnnId
                    cache.AnnId = type.GetProperty("AnnId", flags) ?? type.GetProperty("annId", flags);

                    // Устанавливаем флаги
                    cache.IsNew = type.GetProperty("IsNew", flags);
                    cache.IsModified = type.GetProperty("IsModified", flags);

                    return cache;
                });
            }

            public static List<T> CloneList<T>(IEnumerable<T> source, int newAnnId, string idFieldName, bool markAsNew = true)
        where T : ICloneable
            {
                var list = new List<T>();
                var props = ResolveProperties(typeof(T), idFieldName);
                foreach (var item in source)
                {
                    var clone = (T)item.Clone();

                    // Принудительно сбрасываем ID в 0
                    props.Id?.SetValue(clone, 0);

                    // Устанавливаем новый AnnId
                    props.AnnId?.SetValue(clone, newAnnId);

                    // Устанавливаем флаги
                    props.IsNew?.SetValue(clone, markAsNew);
                    props.IsModified?.SetValue(clone, markAsNew);

                    list.Add(clone);
                }
                return list;
            }

            public static BindingList<T> DeepCloneBindingList<T>(IEnumerable<T> sourceList) where T : class, ICloneable
            {
                if (sourceList == null) return new BindingList<T>();
                var newList = new BindingList<T>();
                foreach (var item in sourceList)
                {
                    if (item is T clonedItem)
                        newList.Add(clonedItem.Clone() as T);
                }
                return newList;
            }
        }
        #endregion

        #region Оригинальные снимки коллекций
        private BindingList<NormRasz> _originalNormRaszList;
        private BindingList<NormRask> _originalNormRaskList;
        private BindingList<NormKont> _originalNormKontList;
        #endregion
    }
}
