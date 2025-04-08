using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Helpers
{
    public static class CloneHelper
    {
        /// <summary>
        /// Клонирует объект и присваивает новый AnnId. Ключевое поле (ID) обнуляется.
        /// </summary>
        public static T CloneAndAssignNewAnnId<T>(T source, int newAnnId, string keyFieldName = "Id") where T : new()
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var target = new T();
            var properties = typeof(T).GetProperties()
                .Where(p => p.CanRead && p.CanWrite)
                .ToList();

            foreach (var prop in properties)
            {
                if (prop.Name == keyFieldName)
                {
                    prop.SetValue(target, 0); // Обнуляем первичный ключ
                }
                else if (prop.Name.Equals("AnnId", StringComparison.OrdinalIgnoreCase))
                {
                    prop.SetValue(target, newAnnId); // Присваиваем новый AnnId
                }
                else
                {
                    prop.SetValue(target, prop.GetValue(source));
                }
            }

            return target;
        }
    }
}
