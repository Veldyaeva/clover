using SewingProduction.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SewingProduction.Helpers
{
    public static class BindingListHelper
    {      /// <summary>
           /// Заменяет элемент в BindingList по совпадению значения указанного свойства (обычно ключа/ID).
           /// </summary>
        public static void ReplaceItemByKey<T>(this BindingList<T> list, T newItem, string keyPropertyName)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (newItem == null) throw new ArgumentNullException(nameof(newItem));
            if (string.IsNullOrWhiteSpace(keyPropertyName)) throw new ArgumentNullException(nameof(keyPropertyName));

            PropertyInfo keyProperty = typeof(T).GetProperty(keyPropertyName);
            if (keyProperty == null)
                throw new ArgumentException($"Тип {typeof(T).Name} не содержит свойства {keyPropertyName}");

            var newKey = keyProperty.GetValue(newItem);

            int index = list.ToList().FindIndex(item =>
            {
                var itemKey = keyProperty.GetValue(item);
                return itemKey != null && itemKey.Equals(newKey);
            });

            if (index >= 0)
            {
                list[index] = newItem;
            }
        }

    }
}
