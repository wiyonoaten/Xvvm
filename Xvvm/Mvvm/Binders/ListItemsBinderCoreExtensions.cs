using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xvvm.Mvvm.Bindings;

namespace Xvvm.Mvvm.Binders
{
    public static class ListItemsBinderCoreExtensions
    {
        public static ListItemsBinder<T> BindToList<T>(this ListItemsBinder<T> binder,
            List<T> targetList)
        {
            targetList.AddRange(binder.List);

            binder.Bindings.Add(new ListCollectionChangedBinding<T>(binder.List, (sender, args) =>
            {
                switch (args.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (T item in args.NewItems)
                        {
                            targetList.Add(item);
                        }
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        foreach (var item in args.OldItems)
                        {
                            targetList.Remove((T)item);
                        }
                        break;

                    case NotifyCollectionChangedAction.Replace:
                        foreach (var item in args.OldItems)
                        {
                            targetList.Remove((T)item);
                        }
                        for (int i = 0; i < args.NewItems.Count; i++)
                        {
                            targetList.Insert(args.OldStartingIndex + i, (T)args.NewItems[i]);
                        }
                        break;

                    case NotifyCollectionChangedAction.Move:
                        // TODO:
                        throw new NotImplementedException("case " + nameof(NotifyCollectionChangedAction.Move) + " not implemented yet!.");

                    case NotifyCollectionChangedAction.Reset:
                        targetList.Clear();
                        break;
                }
			}));

            return binder;
        }
    }
}
