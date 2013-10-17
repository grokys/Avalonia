// -----------------------------------------------------------------------
// <copyright file="CollectionView.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using Avalonia.Threading;

    public class CollectionView : DispatcherObject, ICollectionView, INotifyPropertyChanged
    {
        private List<object> items;

        private NotifyCollectionChangedEventHandler collectionChanged;

        private PropertyChangedEventHandler propertyChanged;

        public CollectionView(IEnumerable collection)
        {
            this.SetSource(collection);
        }

        event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
        {
            add { this.collectionChanged += value; }
            remove { this.collectionChanged -= value; }
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { this.propertyChanged += value; }
            remove { this.propertyChanged -= value; }
        }

        public virtual int Count 
        {
            get { return this.items.Count; }
        }

        public virtual IEnumerable SourceCollection
        {
            get;
            private set;
        }

        internal List<object> Items
        {
            get { return this.items; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.SourceCollection.GetEnumerator();
        }

        internal void SetSource(IEnumerable source)
        {
            this.items = new List<object>(source.Cast<object>());
            this.SourceCollection = source;
        }
    }
}
