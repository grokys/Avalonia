// -----------------------------------------------------------------------
// <copyright file="AvaloniaCollection.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    /// <summary>
    /// A general purpose collection class to stop us having to reimplement collections every time.
    /// </summary>
    /// <typeparam name="T">
    /// The type of value held.
    /// </typeparam>
    [AvaloniaSpecific]
    public class AvaloniaCollection<T> : IList<T>, IList
    {
        private List<T> items;

        private bool isReadOnly;

        protected AvaloniaCollection()
        {
            this.items = new List<T>();
        }

        protected AvaloniaCollection(IEnumerable<T> items)
        {
            this.items = new List<T>(items);
        }

        protected AvaloniaCollection(int capacity)
        {
            this.items = new List<T>(capacity);
        }

        internal event EventHandler Clearing;

        internal event NotifyCollectionChangedEventHandler ItemsChanged;

        public int Capacity
        {
            get { return this.items.Capacity; }
            set { this.items.Capacity = value; }
        }

        public int Count
        {
            get { return this.items.Count; }
        }

        public bool IsReadOnly
        {
            get { return this.isReadOnly; }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return this; }
        }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        public T this[int index]
        {
            get 
            { 
                return this.items[index]; 
            }
            
            set 
            {
                this.CheckReadOnly();
                this.SetItemInternal(index, value);
            }
        }

        object IList.this[int index]
        {
            get { return this[index]; }
            set { this[index] = (T)value; }
        }

        public int Add(T value)
        {
            this.CheckReadOnly();
            return this.AddInternal(value);
        }

        public void Clear()
        {
            this.CheckReadOnly();
            this.ClearInternal();
        }

        public bool Contains(T value)
        {
            return this.items.Contains(value);
        }

        public void CopyTo(Array array, int index)
        {
            this.items.ToArray().CopyTo(array, index);
        }

        public void CopyTo(T[] array, int index)
        {
            this.items.CopyTo(array, index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        public int IndexOf(T value)
        {
            return this.items.IndexOf(value);
        }

        public void Insert(int index, T value)
        {
            this.CheckReadOnly();
            this.InsertInternal(index, value);
        }

        public bool Remove(T value)
        {
            this.CheckReadOnly();
            return this.RemoveInternal(value);
        }

        public void RemoveAt(int index)
        {
            this.CheckReadOnly();
            this.RemoveAtInternal(index);
        }

        public void RemoveRange(int index, int count)
        {
            this.items.RemoveRange(index, count);
        }

        void ICollection<T>.Add(T value)
        {
            this.items.Add(value);
        }

        int IList.Add(object value)
        {
            return this.Add((T)value);
        }

        bool IList.Contains(object value)
        {
            return this.Contains((T)value);
        }

        int IList.IndexOf(object value)
        {
            return this.IndexOf((T)value);
        }

        void IList.Insert(int index, object value)
        {
            this.Insert(index, (T)value);
        }

        void IList.Remove(object value)
        {
            this.Remove((T)value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        internal int AddInternal(T value)
        {
            int index = this.items.Count;
            this.items.Add(value);
            this.RaiseItemsChanged(NotifyCollectionChangedAction.Add, value, index);
            return index;
        }

        internal void ClearInternal()
        {
            this.OnClearing();
            this.items.Clear();
            this.RaiseItemsChanged(NotifyCollectionChangedAction.Reset);
        }

        internal void InsertInternal(int index, T value)
        {
            this.items.Insert(index, value);
            this.RaiseItemsChanged(NotifyCollectionChangedAction.Add, value, index);
        }

        internal bool RemoveInternal(T value)
        {
            int index = this.items.IndexOf(value);

            if (index != -1)
            {
                this.items.RemoveAt(index);
                this.RaiseItemsChanged(NotifyCollectionChangedAction.Remove, value, index);
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void RemoveAtInternal(int index)
        {
            object value = this.items[index];
            this.items.RemoveAt(index);
            this.RaiseItemsChanged(NotifyCollectionChangedAction.Remove, value, index);
        }

        internal virtual void SetIsReadOnly(bool readOnly)
        {
            this.isReadOnly = readOnly;
        }

        internal void SetItemInternal(int index, T value)
        {
            T old = this.items[index];

            if (!old.Equals(value))
            {
                this.items[index] = value;
                this.RaiseItemsChanged(NotifyCollectionChangedAction.Replace, value, old, index);
            }
        }

        protected virtual void OnClearing()
        {
            if (this.Clearing != null)
            {
                this.Clearing(this, EventArgs.Empty);
            }
        }

        protected virtual void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this.ItemsChanged != null)
            {
                this.ItemsChanged(this, e);
            }
        }

        private void CheckReadOnly()
        {
            if (this.IsReadOnly)
            {
                throw new InvalidOperationException("The collection is readonly.");
            }
        }

        private void RaiseItemsChanged(NotifyCollectionChangedAction action)
        {
            this.OnItemsChanged(new NotifyCollectionChangedEventArgs(action));
        }

        private void RaiseItemsChanged(NotifyCollectionChangedAction action, object changedItem, int index)
        {
            this.OnItemsChanged(new NotifyCollectionChangedEventArgs(action, changedItem, index));
        }

        private void RaiseItemsChanged(NotifyCollectionChangedAction action, object newItem, object oldItem, int index)
        {
            this.OnItemsChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
        }
    }
}
