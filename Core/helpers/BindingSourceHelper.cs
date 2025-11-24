using SewingProduction.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
