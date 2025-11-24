using System;
using System.ComponentModel;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.Models;

namespace SewingProduction.Features.TeamWork.Operations
{
    public static class ContextMenuFactory
    {
        public static EventHandler CreateAddMenuItem(GridView view, Func<NormRasz> getCurrent, Action<NormRasz, bool> addOperation)
        {
            return (s, e) =>
            {
                int rowHandle = view.FocusedRowHandle;
                var current = getCurrent?.Invoke();
                if (current == null)
                {
                    addOperation?.Invoke(null, true);
                }
                else
                {
                    addOperation?.Invoke(current, false);
                }
            };
        }

        public static void AttachGenericDelete<T>(GridView view, BindingList<T> bindingList, Func<T, int> getId, System.Collections.Generic.List<int> deletedIds)
            where T : class
        {
            view.PopupMenuShowing += UIHelper.CreateContextMenu(view, bindingList, getId, deletedIds);
        }

        //public static void InjectAddItem(GridView view, DXMenuEventArgs e, string caption, EventHandler onClick)
        //{
        //    if (e.MenuType != GridMenuType.Row) return;
        //    var menu = e.Menu;
        //    var addItem = new DXMenuItem(caption, onClick);
        //    menu.Items.Add(addItem);
        //}
    }
}



