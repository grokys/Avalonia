// -----------------------------------------------------------------------
// <copyright file="ItemCollection.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using Avalonia.Data;

    public sealed class ItemCollection : CollectionView, IList, IWeakEventListener
    {
        private List<object> items;
        private bool readOnly;

        public ItemCollection()
            : base(new List<object>())
        {
            this.items = (List<object>)this.SourceCollection;
        }

        internal event EventHandler Clearing;

        internal event NotifyCollectionChangedEventHandler ItemsChanged;

        public int Count
        {
            get { return this.items.Count; }
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public bool IsReadOnly
        {
            get { return this.readOnly; }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return null; }
        }

        public object this[int index]
        {
            get 
            { 
                return this.items[index]; 
            }

            set 
            {
                this.ReadOnlyCheck();
                this.SetItemImpl(index, value);
            }
        }

        public int Add(object value)
        {
            this.ReadOnlyCheck();
            return this.AddImpl(value);
        }

        public void Clear()
        {
            this.ReadOnlyCheck();
            this.ClearImpl();
        }

        public bool Contains(object value)
        {
            return this.items.Contains(value);
        }

        public int IndexOf(object value)
        {
            return this.items.IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            this.ReadOnlyCheck();
            this.InsertImpl(index, value);
        }

        public void Remove(object value)
        {
            this.ReadOnlyCheck();
            this.items.Remove(value);
        }

        public void RemoveAt(int index)
        {
            this.ReadOnlyCheck();
            this.RemoveAtImpl(index);
        }

        public void CopyTo(Array array, int index)
        {
            this.items.CopyTo((object[])array, index);
        }

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        internal int AddImpl(object value)
        {
            this.items.Add(value);

            if (this.ItemsChanged != null)
            {
                this.ItemsChanged(
                    this, 
                    new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Add,
                        value,
                        this.items.Count - 1));
            }

            return this.items.Count - 1;
        }

        internal void ClearImpl()
        {
            if (this.Clearing != null)
            {
                this.Clearing(this, EventArgs.Empty);
            }

            this.items.Clear();

            if (this.ItemsChanged != null)
            {
                this.ItemsChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        internal void InsertImpl(int index, object value)
        {
            this.items.Insert(index, value);

            if (this.ItemsChanged != null)
            {
                this.ItemsChanged(
                    this, 
                    new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Add,
                        value,
                        index));
            }
        }

        internal void RemoveAtImpl(int index)
        {
            object value = this.items[index];

            this.items.RemoveAt(index);

            if (this.ItemsChanged != null)
            {
                this.ItemsChanged(
                    this, 
                    new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Remove,
                        value,
                        index));
            }
        }

        internal void SetItemImpl(int index, object value)
        {
            object old = this.items[index];

            this.items[index] = value;

            if (this.ItemsChanged != null)
            {
                this.ItemsChanged(
                    this, 
                    new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Replace,
                        value,
                        old,
                        index));
            }
        }

        internal void SetIsReadOnly(bool readOnly)
        {
            this.readOnly = readOnly;
        }

        private void ReadOnlyCheck()
        {
            if (this.IsReadOnly)
            {
                throw new InvalidOperationException("The collection is readonly.");
            }
        }
    }
}
