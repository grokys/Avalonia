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
        private object currentItem;

        private int currentPosition;

        private NotifyCollectionChangedEventHandler collectionChanged;

        private PropertyChangedEventHandler propertyChanged;

        public CollectionView(IEnumerable collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            this.SourceCollection = collection;
            this.currentItem = collection.Cast<object>().FirstOrDefault();
            this.currentPosition = (this.currentItem != null) ? 0 : -1;

            INotifyCollectionChanged incc = collection as INotifyCollectionChanged;

            if (incc != null)
            {
                incc.CollectionChanged += OnCollectionChanged;
            }
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

        public virtual bool CanFilter 
        {
            get { return true; }
        }

        public virtual bool CanGroup
        {
            get { return false; }
        }

        public virtual bool CanSort
        {
            get { return false; }
        }

        public virtual int Count 
        {
            get { return this.SourceCollection.Cast<object>().Count(); }
        }

        public virtual object CurrentItem
        {
            get { return this.currentItem; }
        }

        public virtual int CurrentPosition
        {
            get { return this.currentPosition; }
        }

        public virtual Predicate<Object> Filter 
        { 
            get; 
            set; 
        }

        public virtual IEnumerable SourceCollection
        {
            get;
            private set;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (this.Filter == null)
            {
                return this.SourceCollection.GetEnumerator();
            }
            else
            {
                return this.SourceCollection
                    .Cast<object>()
                    .Where(x => this.Filter(x))
                    .GetEnumerator();
            }
        }

        public virtual bool MoveCurrentToFirst()
        {
            if (this.SourceCollection.Cast<object>().Any())
            {
                this.currentPosition = 0;
                this.currentItem = this.SourceCollection.Cast<object>().First();
                return true;
            }
            else
            {
                this.currentPosition = -1;
                this.currentItem = null;
                return false;
            }
        }

        public virtual bool MoveCurrentToLast()
        {
            if (this.SourceCollection.Cast<object>().Any())
            {
                this.currentPosition = this.SourceCollection.Cast<object>().Count() - 1;
                this.currentItem = this.SourceCollection.Cast<object>().Last();
                return true;
            }
            else
            {
                this.currentPosition = -1;
                this.currentItem = null;
                return false;
            }
        }

        public virtual bool MoveCurrentToNext()
        {
            IEnumerator e = this.SourceCollection.GetEnumerator();
            int next = this.currentPosition + 1;

            for (int i = -1; i < next; ++i)
            {
                if (!e.MoveNext())
                {
                    if (i > -1)
                    {
                        // Ummm? According to unit tests, this is what WPF does.
                        this.currentPosition = next;
                    }

                    this.currentItem = null;
                    return false;
                }
            }

            this.currentItem = e.Current;
            this.currentPosition = next;
            return true;
        }

        protected void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            this.ProcessCollectionChanged(args);
            this.OnCollectionChanged(args);
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (this.collectionChanged != null)
            {
                this.collectionChanged(this, args);
            }
        }

        protected virtual void ProcessCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
        }
    }
}
