using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Splat;

namespace Cinephile.Utils
{
    public class ObservableReactiveList<T> :
            ReactiveObject,
            IEnumerable<T>, INotifyCollectionChanged, IDisposable, IEnableLogger
    {
        IReactiveCollection<T> _internalList;
        CompositeDisposable killMe = null;
        bool _ignoreCollectionChangedNotifications = false;
        public ObservableReactiveList() : this(new ReactiveList<T>())
        {

        }

        public ObservableReactiveList(IReactiveCollection<T> sourceList, bool ignoreCollectionChangedNotifications = false)
        {
            _ignoreCollectionChangedNotifications = ignoreCollectionChangedNotifications;
            _internalList = sourceList;
            killMe = new CompositeDisposable();
            killMe.Add(
                sourceList.Changed.ObserveOn(ImmediateScheduler.Instance).Subscribe(args => OnCollectionChanged(args))
            );
        }

        protected void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (!_ignoreCollectionChangedNotifications)
            {
                if (CollectionChanged != null)
                    CollectionChanged(this, args);
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public IEnumerator<T> GetEnumerator()
        {
            return _internalList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalList.GetEnumerator();
        }

        public void Dispose()
        {
            killMe.Dispose();
            killMe = null;
        }
    }
}
