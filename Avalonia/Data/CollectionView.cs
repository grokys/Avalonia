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
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Globalization;
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

            if (collection.Cast<object>().Any())
            {
                this.currentItem = collection.Cast<object>().First();
                this.currentPosition = 0;
            }
            else
            {
                this.currentPosition = -1;
                this.IsCurrentAfterLast = this.IsCurrentBeforeFirst = true;
            }
            
            INotifyCollectionChanged incc = collection as INotifyCollectionChanged;

            if (incc != null)
            {
                incc.CollectionChanged += OnCollectionChanged;
            }
        }

        public virtual event EventHandler CurrentChanged;

        public virtual event CurrentChangingEventHandler CurrentChanging;

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

        public virtual IComparer Comparer 
        {
            get { return null; }
        }

        public virtual int Count 
        {
            get 
            {
                ICollection collection = this.SourceCollection as ICollection;
                return (collection != null) ? 
                    collection.Count :
                    this.SourceCollection.Cast<object>().Count(); 
            }
        }

        public virtual CultureInfo Culture 
        { 
            get; 
            set; 
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

        public virtual ObservableCollection<GroupDescription> GroupDescriptions 
        {
            get { return null; }
        }

        public virtual ReadOnlyObservableCollection<Object> Groups 
        {
            get { return null; }
        }

        public virtual bool IsCurrentAfterLast
        {
            get;
            private set;
        }

        public virtual bool IsCurrentBeforeFirst
        {
            get;
            private set;
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
            return this.MoveCurrentToPosition(0);
        }

        public virtual bool MoveCurrentToLast()
        {
            return this.MoveCurrentToPosition(this.Count - 1);
        }

        public virtual bool MoveCurrentToNext()
        {
            return this.MoveCurrentToPosition(this.currentPosition + 1);
        }

        public virtual bool MoveCurrentToPosition(int position)
        {
            int count = this.Count;

            if (position < -1 || position > count)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (this.OKToChangeCurrent())
            {
                bool result;

                if (position == -1 || (position == 0 && count == 0))
                {
                    this.IsCurrentBeforeFirst = true;
                    this.IsCurrentAfterLast = false;
                    this.currentPosition = -1;
                    this.currentItem = null;
                    result = false;
                }
                else if (position == count)
                {
                    this.IsCurrentBeforeFirst = false;
                    this.IsCurrentAfterLast = true;
                    this.currentPosition = position;
                    this.currentItem = null;
                    result = false;
                }
                else
                {
                    this.IsCurrentBeforeFirst = this.IsCurrentAfterLast = false;
                    this.currentPosition = position;
                    this.currentItem = this.SourceCollection.Cast<object>().ElementAt(position);
                    result = true;
                }

                this.OnCurrentChanged();

                return result;
            }
            else
            {
                return true;
            }
        }

        public virtual bool MoveCurrentToPrevious()
        {
            if (this.currentPosition > -1)
            {
                return this.MoveCurrentToPosition(this.currentPosition - 1);
            }
            else
            {
                return false;
            }
        }

        protected bool OKToChangeCurrent()
        {
            CurrentChangingEventArgs e = new CurrentChangingEventArgs();
            this.OnCurrentChanging(e);
            return !e.Cancel;
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

        protected virtual void OnCurrentChanged()
        {
            if (this.CurrentChanged != null)
            {
                this.CurrentChanged(this, EventArgs.Empty);
            }
        }

        protected void OnCurrentChanging()
        {
            this.currentPosition = -1;
            this.OnCurrentChanging(new CurrentChangingEventArgs(false));
        }

        protected virtual void OnCurrentChanging(CurrentChangingEventArgs args)
        {
            if (this.CurrentChanging != null)
            {
                this.CurrentChanging(this, args);
            }
        }

        protected virtual void ProcessCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
        }
    }
}
