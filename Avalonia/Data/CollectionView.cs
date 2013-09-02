// -----------------------------------------------------------------------
// <copyright file="CollectionView.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Data
{
    using System.Collections;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using Avalonia.Threading;

    public class CollectionView : DispatcherObject, ICollectionView, INotifyPropertyChanged
    {
        public CollectionView(IEnumerable collection)
        {
            this.SourceCollection = collection;
        }

        event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
        {
            add { throw new System.NotImplementedException(); }
            remove { throw new System.NotImplementedException(); }
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { throw new System.NotImplementedException(); }
            remove { throw new System.NotImplementedException(); }
        }

        public virtual IEnumerable SourceCollection
        {
            get;
            private set;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.SourceCollection.GetEnumerator();
        }
    }
}
