using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace SewingProduction.Helpers
{
    public static class BindingListExtensions
    {
        public static void BulkLoad<T>(this BindingList<T> bindingList, IEnumerable<T> data)
        {
            if (bindingList == null)
                throw new ArgumentNullException(nameof(bindingList));
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            // Отключаем уведомления на время bulk-загрузки
            bindingList.RaiseListChangedEvents = false;

            bindingList.Clear(); // Очищаем список перед загрузкой

            foreach (var item in data)
            {
                bindingList.Add(item);
            }

            // Включаем уведомления обратно
            bindingList.RaiseListChangedEvents = true;

            // Принудительно уведомляем привязки
            bindingList.ResetBindings();
        }
    }
    public static class DataTableExtensions
    {
        public static void BulkLoad(this DataTable table, IEnumerable<DataRow> rows)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));
            if (rows == null)
                throw new ArgumentNullException(nameof(rows));

            table.BeginLoadData();

            table.Clear();

            foreach (var row in rows)
            {
                table.ImportRow(row);
            }

            table.EndLoadData();
        }
    }
    public static class BindingSourceExtensions
    {
        public static void BulkLoad<T>(this BindingSource bindingSource, IEnumerable<T> data)
        {
            if (bindingSource == null)
                throw new ArgumentNullException(nameof(bindingSource));
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (bindingSource.DataSource is BindingList<T> bindingList)
            {
                bindingList.BulkLoad(data);
            }
            else if (bindingSource.DataSource is List<T> list)
            {
                list.Clear();
                list.AddRange(data);
                bindingSource.ResetBindings(false);
            }
            else
            {
                // Заменяем весь источник, если тип неизвестен
                bindingSource.DataSource = new BindingList<T>(new List<T>(data));
            }
        }
    }
}
