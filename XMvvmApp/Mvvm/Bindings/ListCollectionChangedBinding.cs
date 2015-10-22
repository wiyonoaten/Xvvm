﻿using System.Collections.Specialized;

namespace XMvvmApp.Mvvm.Bindings
{
    public class ListCollectionChangedBinding<T> : Binding
    {
        private readonly IObservableReadOnlyList<T> _list;

        public ListCollectionChangedBinding(IObservableReadOnlyList<T> list, NotifyCollectionChangedEventHandler evHandler)
            : base(evHandler)
        {
            _list = list;

            _list.CollectionChanged += evHandler;
        }

        public override void Detach()
        {
            base.Detach();

            _list.CollectionChanged -= this.Connection as NotifyCollectionChangedEventHandler;
        }
    }
}
